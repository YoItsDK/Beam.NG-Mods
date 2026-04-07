param(
    [Parameter(Mandatory = $true, Position = 0)]
    [string]$Path,

    [string]$VisibleNodeId,

    [string]$CollisionName = 'Colmesh-1',

    [switch]$Recurse,

    [switch]$AutoFixTypos,

    [switch]$OnlyIfMissingPrecise,

    [switch]$ScanOnly
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Clone-GeometryXml {
    param(
        [System.Xml.XmlDocument]$Xml,
        [System.Xml.XmlElement]$Geometry,
        [string]$NewGeometryId,
        [string]$NewGeometryName
    )

    $oldGeometryId = $Geometry.GetAttribute('id')
    $outer = $Geometry.OuterXml -replace [regex]::Escape($oldGeometryId), $NewGeometryId
    $fragment = $Xml.CreateDocumentFragment()
    $fragment.InnerXml = $outer
    $node = $fragment.FirstChild
    $node.SetAttribute('id', $NewGeometryId)
    $node.SetAttribute('name', $NewGeometryName)
    return $node
}

function Get-GeometryMaps {
    param([xml]$Xml)

    $triangleMap = @{}
    $positionMap = @{}
    foreach ($geo in @($Xml.SelectNodes('/*[local-name()="COLLADA"]/*[local-name()="library_geometries"]/*[local-name()="geometry"]'))) {
        $id = if ($geo.Attributes['id']) { $geo.Attributes['id'].Value } else { '' }
        if ([string]::IsNullOrWhiteSpace($id)) {
            continue
        }

        $triangleCount = 0
        foreach ($tri in @($geo.SelectNodes('.//*[local-name()="triangles"]'))) {
            if ($tri.Attributes['count']) {
                $triangleCount += [int]$tri.Attributes['count'].Value
            }
        }
        foreach ($poly in @($geo.SelectNodes('.//*[local-name()="polylist"]'))) {
            if ($poly.Attributes['count']) {
                $triangleCount += [int]$poly.Attributes['count'].Value
            }
        }
        $triangleMap[$id] = $triangleCount

        $positionsNode = $geo.SelectSingleNode('.//*[local-name()="source" and contains(@id,"positions")]/*[local-name()="float_array"]')
        $positionMap[$id] = if ($null -ne $positionsNode -and $positionsNode.Attributes['count']) { [int]$positionsNode.Attributes['count'].Value } else { 0 }
    }

    return [PSCustomObject]@{
        TriangleMap = $triangleMap
        PositionMap = $positionMap
    }
}

function Get-CollisionReport {
    param([xml]$Xml)

    $collisionPattern = '(?i)(^|[_.-])(colmesh|collision|collider|col)([_.-]|$)'
    $typoPattern = '(?i)(^|[_.-])(comesh|colmseh|colision|collsion)([_.-]|$)'
    $maps = Get-GeometryMaps -Xml $Xml
    $visibleRefs = New-Object 'System.Collections.Generic.List[object]'
    $collisionRefs = New-Object 'System.Collections.Generic.List[object]'
    $typoRefs = New-Object 'System.Collections.Generic.List[object]'

    foreach ($node in @($Xml.SelectNodes('//*[local-name()="node"]'))) {
        $nodeId = if ($node.Attributes['id']) { $node.Attributes['id'].Value } else { '' }
        $nodeName = if ($node.Attributes['name']) { $node.Attributes['name'].Value } else { '' }
        $isCollisionNode = $nodeId -match $collisionPattern -or $nodeName -match $collisionPattern
        $isTypoNode = -not $isCollisionNode -and ($nodeId -match $typoPattern -or $nodeName -match $typoPattern)

        foreach ($inst in @($node.SelectNodes('./*[local-name()="instance_geometry"]'))) {
            if (-not $inst.Attributes['url']) {
                continue
            }

            $geometryId = $inst.Attributes['url'].Value.TrimStart('#')
            if ([string]::IsNullOrWhiteSpace($geometryId)) {
                continue
            }

            $entry = [PSCustomObject]@{
                Node = $node
                NodeId = $nodeId
                NodeName = $nodeName
                GeometryId = $geometryId
                Triangles = if ($maps.TriangleMap.ContainsKey($geometryId)) { $maps.TriangleMap[$geometryId] } else { 0 }
                PositionCount = if ($maps.PositionMap.ContainsKey($geometryId)) { $maps.PositionMap[$geometryId] } else { 0 }
            }

            if ($isCollisionNode) {
                $collisionRefs.Add($entry)
            }
            elseif ($isTypoNode) {
                $typoRefs.Add($entry)
            }
            else {
                $visibleRefs.Add($entry)
            }
        }
    }

    $hasPreciseCollision = @($collisionRefs | Where-Object {
        if ($_.GeometryId -match '(?i)^CollisionBox-mesh$') {
            return $false
        }

        if ($_.Triangles -gt 12) {
            return $true
        }

        $collisionRef = $_
        return @($visibleRefs | Where-Object {
            $_.GeometryId -ne $collisionRef.GeometryId -and $_.Triangles -eq $collisionRef.Triangles -and $_.PositionCount -eq $collisionRef.PositionCount -and $_.PositionCount -gt 0
        }).Count -gt 0
    }).Count -gt 0

    $status = 'missing'
    if ($hasPreciseCollision) {
        $status = 'precise'
    }
    elseif ($collisionRefs.Count -gt 0) {
        $status = 'basic'
    }
    elseif ($typoRefs.Count -gt 0) {
        $status = 'typo-only'
    }

    return [PSCustomObject]@{
        Status = $status
        VisibleRefs = $visibleRefs
        CollisionRefs = $collisionRefs
        TypoRefs = $typoRefs
        HasPreciseCollision = $hasPreciseCollision
        VisibleCount = $visibleRefs.Count
    }
}

function Get-TargetFiles {
    param(
        [string]$InputPath,
        [switch]$Recurse
    )

    $resolved = (Resolve-Path -LiteralPath $InputPath).Path
    $item = Get-Item -LiteralPath $resolved
    if ($item.PSIsContainer) {
        return @(Get-ChildItem -LiteralPath $resolved -Filter *.dae -File -Recurse:$Recurse | Sort-Object FullName)
    }

    return @($item)
}

function Get-VisibleGeometryNode {
    param(
        [xml]$Xml,
        [string]$ExplicitNodeId,
        [string]$CollisionName
    )

    if (-not [string]::IsNullOrWhiteSpace($ExplicitNodeId)) {
        $explicit = $Xml.SelectSingleNode("//*[local-name()='node' and @id='$ExplicitNodeId']")
        if ($null -eq $explicit) {
            throw "Visible node '$ExplicitNodeId' not found."
        }
        return $explicit
    }

    $report = Get-CollisionReport -Xml $Xml
    if ($report.VisibleCount -ne 1) {
        throw "Expected exactly one visible geometry node, found $($report.VisibleCount)."
    }

    return $report.VisibleRefs[0].Node
}

function Ensure-CollisionNode {
    param(
        [xml]$Xml,
        [System.Xml.XmlNode]$ParentNode,
        [string]$CollisionName,
        [System.Collections.Generic.List[object]]$TypoRefs,
        [switch]$AutoFixTypos
    )

    $existing = $ParentNode.SelectSingleNode("./*[local-name()='node' and (@id='$CollisionName' or @name='$CollisionName')]")
    if ($null -ne $existing) {
        return $existing
    }

    if ($AutoFixTypos) {
        foreach ($typoRef in @($TypoRefs)) {
            if ($typoRef.Node.ParentNode -ne $ParentNode) {
                continue
            }

            if ($null -ne $typoRef.Node.Attributes['id']) {
                $typoRef.Node.Attributes['id'].Value = $CollisionName
            }
            if ($null -ne $typoRef.Node.Attributes['name']) {
                $typoRef.Node.Attributes['name'].Value = $CollisionName
            }
            return $typoRef.Node
        }
    }

    $collisionNode = $Xml.CreateElement('node', $Xml.DocumentElement.NamespaceURI)
    $collisionNode.SetAttribute('id', $CollisionName)
    $collisionNode.SetAttribute('name', $CollisionName)
    $collisionNode.SetAttribute('type', 'NODE')

    $matrix = $Xml.CreateElement('matrix', $Xml.DocumentElement.NamespaceURI)
    $matrix.SetAttribute('sid', 'transform')
    $matrix.InnerText = '1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1'
    [void]$collisionNode.AppendChild($matrix)
    [void]$ParentNode.AppendChild($collisionNode)
    return $collisionNode
}

function Update-InstanceGeometry {
    param(
        [System.Xml.XmlNode]$CollisionNode,
        [string]$CollisionName,
        [string]$CollisionGeometryId,
        [xml]$Xml
    )

    $instance = $CollisionNode.SelectSingleNode('./*[local-name()="instance_geometry"]')
    if ($null -eq $instance) {
        $instance = $Xml.CreateElement('instance_geometry', $Xml.DocumentElement.NamespaceURI)
        [void]$CollisionNode.AppendChild($instance)
    }

    $instance.SetAttribute('url', "#$CollisionGeometryId")
    $instance.SetAttribute('name', $CollisionName)
}

function Invoke-PreciseCollisionPrep {
    param(
        [System.IO.FileInfo]$File,
        [string]$VisibleNodeId,
        [string]$CollisionName,
        [switch]$AutoFixTypos,
        [switch]$OnlyIfMissingPrecise,
        [switch]$ScanOnly
    )

    [xml]$xml = Get-Content -Raw -LiteralPath $File.FullName
    $before = Get-CollisionReport -Xml $xml

    if ($ScanOnly) {
        return [PSCustomObject]@{
            File = $File.FullName
            Status = $before.Status
            Action = 'scanned'
            VisibleCount = $before.VisibleCount
            CollisionCount = $before.CollisionRefs.Count
            TypoCount = $before.TypoRefs.Count
            Note = ''
        }
    }

    if ($OnlyIfMissingPrecise -and $before.HasPreciseCollision) {
        return [PSCustomObject]@{
            File = $File.FullName
            Status = 'precise'
            Action = 'skipped-existing'
            VisibleCount = $before.VisibleCount
            CollisionCount = $before.CollisionRefs.Count
            TypoCount = $before.TypoRefs.Count
            Note = ''
        }
    }

    try {
        $visibleNode = Get-VisibleGeometryNode -Xml $xml -ExplicitNodeId $VisibleNodeId -CollisionName $CollisionName
    }
    catch {
        return [PSCustomObject]@{
            File = $File.FullName
            Status = $before.Status
            Action = 'skipped-complex'
            VisibleCount = $before.VisibleCount
            CollisionCount = $before.CollisionRefs.Count
            TypoCount = $before.TypoRefs.Count
            Note = $_.Exception.Message
        }
    }

    $visibleInstance = $visibleNode.SelectSingleNode('./*[local-name()="instance_geometry"]')
    if ($null -eq $visibleInstance -or -not $visibleInstance.Attributes['url']) {
        return [PSCustomObject]@{
            File = $File.FullName
            Status = $before.Status
            Action = 'skipped-no-visible-geometry'
            VisibleCount = $before.VisibleCount
            CollisionCount = $before.CollisionRefs.Count
            TypoCount = $before.TypoRefs.Count
            Note = ''
        }
    }

    $sourceGeometryId = $visibleInstance.Attributes['url'].Value.TrimStart('#')
    $sourceGeometry = $xml.SelectSingleNode("/*[local-name()='COLLADA']/*[local-name()='library_geometries']/*[local-name()='geometry'][@id='$sourceGeometryId']")
    if ($null -eq $sourceGeometry) {
        return [PSCustomObject]@{
            File = $File.FullName
            Status = $before.Status
            Action = 'skipped-missing-source-geometry'
            VisibleCount = $before.VisibleCount
            CollisionCount = $before.CollisionRefs.Count
            TypoCount = $before.TypoRefs.Count
            Note = ''
        }
    }

    $collisionGeometryId = "$CollisionName-mesh"
    $existingCollisionGeometry = $xml.SelectSingleNode("/*[local-name()='COLLADA']/*[local-name()='library_geometries']/*[local-name()='geometry'][@id='$collisionGeometryId']")
    if ($null -ne $existingCollisionGeometry) {
        [void]$existingCollisionGeometry.ParentNode.RemoveChild($existingCollisionGeometry)
    }

    $clonedGeometry = Clone-GeometryXml -Xml $xml -Geometry $sourceGeometry -NewGeometryId $collisionGeometryId -NewGeometryName $CollisionName
    [void]$sourceGeometry.ParentNode.AppendChild($clonedGeometry)

    $collisionNode = Ensure-CollisionNode -Xml $xml -ParentNode $visibleNode.ParentNode -CollisionName $CollisionName -TypoRefs $before.TypoRefs -AutoFixTypos:$AutoFixTypos
    Update-InstanceGeometry -CollisionNode $collisionNode -CollisionName $CollisionName -CollisionGeometryId $collisionGeometryId -Xml $xml

    $xml.Save($File.FullName)
    $after = Get-CollisionReport -Xml $xml

    return [PSCustomObject]@{
        File = $File.FullName
        Status = $after.Status
        Action = if ($before.TypoRefs.Count -gt 0 -and $AutoFixTypos) { 'updated-fixed-typo' } else { 'updated' }
        VisibleCount = $after.VisibleCount
        CollisionCount = $after.CollisionRefs.Count
        TypoCount = $after.TypoRefs.Count
        Note = ''
    }
}

$files = Get-TargetFiles -InputPath $Path -Recurse:$Recurse
$results = foreach ($file in $files) {
    Invoke-PreciseCollisionPrep -File $file -VisibleNodeId $VisibleNodeId -CollisionName $CollisionName -AutoFixTypos:$AutoFixTypos -OnlyIfMissingPrecise:$OnlyIfMissingPrecise -ScanOnly:$ScanOnly
}

$results | Select-Object File, Status, Action, VisibleCount, CollisionCount, TypoCount, Note | Format-Table -AutoSize

Write-Host ''
Write-Host 'Summary:'
foreach ($group in ($results | Group-Object Action | Sort-Object Name)) {
    Write-Host ('{0}: {1}' -f $group.Name, $group.Count)
}

Write-Host ''
Write-Host 'Status Summary:'
foreach ($group in ($results | Group-Object Status | Sort-Object Name)) {
    Write-Host ('{0}: {1}' -f $group.Name, $group.Count)
}