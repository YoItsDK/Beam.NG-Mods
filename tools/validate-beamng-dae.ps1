param(
    [Parameter(Mandatory = $true, Position = 0)]
    [string]$Path,

    [switch]$Recurse
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function New-Result {
    param(
        [string]$File,
        [string]$Level,
        [string]$Message
    )

    [PSCustomObject]@{
        File = $File
        Level = $Level
        Message = $Message
    }
}

function Get-NodeText {
    param([System.Xml.XmlNode]$Node)

    if ($null -eq $Node) {
        return ''
    }

    if ($null -eq $Node.InnerText) {
        return ''
    }

    return $Node.InnerText.Trim()
}

function Get-LevelRoot {
    param([string]$FilePath)

    $match = [regex]::Match($FilePath, '^(.*?\\levels\\[^\\]+)\\')
    if ($match.Success) {
        return $match.Groups[1].Value
    }

    return $null
}

function Resolve-ImagePath {
    param(
        [string]$DaePath,
        [string]$ImagePath
    )

    if ([string]::IsNullOrWhiteSpace($ImagePath)) {
        return $null
    }

    $normalized = $ImagePath -replace '/', '\\'

    if ($normalized -match '^[A-Za-z]:\\' -or $normalized -match '^\\[A-Za-z]:\\') {
        return $null
    }

    if ($normalized.StartsWith('levels\\')) {
        $levelRoot = Get-LevelRoot -FilePath $DaePath
        if ($null -eq $levelRoot) {
            return $null
        }

        $parts = $normalized.Split('\\', 3)
        if ($parts.Length -lt 3) {
            return $null
        }

        return Join-Path $levelRoot $parts[2]
    }

    $daeDir = Split-Path -Parent $DaePath
    return Join-Path $daeDir $normalized
}

function Test-FloatArrayCount {
    param([System.Xml.XmlNode]$FloatArrayNode)

    $expectedCount = 0
    if ($FloatArrayNode.Attributes['count']) {
        [void][int]::TryParse($FloatArrayNode.Attributes['count'].Value, [ref]$expectedCount)
    }

    $values = (Get-NodeText -Node $FloatArrayNode) -split '\s+' | Where-Object { $_ -ne '' }
    return @($expectedCount, $values.Count)
}

function Test-PolylistCounts {
    param([System.Xml.XmlNode]$PolylistNode)

    $inputCount = @($PolylistNode.SelectNodes('./*[local-name()="input"]')).Count
    if ($inputCount -eq 0) {
        return 'Polylist has no inputs.'
    }

    $vcountNode = $PolylistNode.SelectSingleNode('./*[local-name()="vcount"]')
    $pNode = $PolylistNode.SelectSingleNode('./*[local-name()="p"]')
    if ($null -eq $vcountNode -or $null -eq $pNode) {
        return 'Polylist is missing vcount or p data.'
    }

    $vcounts = (Get-NodeText -Node $vcountNode) -split '\s+' | Where-Object { $_ -ne '' } | ForEach-Object { [int]$_ }
    $pValues = (Get-NodeText -Node $pNode) -split '\s+' | Where-Object { $_ -ne '' }
    $expected = ($vcounts | Measure-Object -Sum).Sum * $inputCount

    if ($expected -ne $pValues.Count) {
        return "Polylist index count mismatch. Expected $expected values, found $($pValues.Count)."
    }

    return $null
}

function Get-CollisionDetails {
    param([xml]$Xml)

    $collisionPattern = '(?i)(^|[_.-])(colmesh|collision|collider|col)([_.-]|$)'
    $geoTriMap = @{}
    $geoPositionMap = @{}
    foreach ($geo in @($Xml.SelectNodes('/*[local-name()="COLLADA"]/*[local-name()="library_geometries"]/*[local-name()="geometry"]'))) {
        $id = if ($geo.Attributes['id']) { $geo.Attributes['id'].Value } else { '' }
        if ([string]::IsNullOrWhiteSpace($id)) {
            continue
        }

        $count = 0
        foreach ($tri in @($geo.SelectNodes('.//*[local-name()="triangles"]'))) {
            if ($tri.Attributes['count']) {
                $count += [int]$tri.Attributes['count'].Value
            }
        }
        foreach ($poly in @($geo.SelectNodes('.//*[local-name()="polylist"]'))) {
            if ($poly.Attributes['count']) {
                $count += [int]$poly.Attributes['count'].Value
            }
        }
        $geoTriMap[$id] = $count

        $positionsNode = $geo.SelectSingleNode('.//*[local-name()="source" and contains(@id,"positions")]/*[local-name()="float_array"]')
        $geoPositionMap[$id] = if ($null -ne $positionsNode -and $positionsNode.Attributes['count']) { [int]$positionsNode.Attributes['count'].Value } else { 0 }
    }

    $collisionNodeRefs = New-Object 'System.Collections.Generic.List[object]'
    $visibleRefs = New-Object 'System.Collections.Generic.List[object]'
    foreach ($node in @($Xml.SelectNodes('//*[local-name()="node"]'))) {
        $nodeId = if ($node.Attributes['id']) { $node.Attributes['id'].Value } else { '' }
        $nodeName = if ($node.Attributes['name']) { $node.Attributes['name'].Value } else { '' }
        $isCollisionNode = $nodeId -match $collisionPattern -or $nodeName -match $collisionPattern

        foreach ($inst in @($node.SelectNodes('./*[local-name()="instance_geometry"]'))) {
            if (-not $inst.Attributes['url']) {
                continue
            }

            $ref = $inst.Attributes['url'].Value.TrimStart('#')
            if ([string]::IsNullOrWhiteSpace($ref)) {
                continue
            }

            $entry = [PSCustomObject]@{
                NodeId = $nodeId
                NodeName = $nodeName
                GeometryId = $ref
                Triangles = if ($geoTriMap.ContainsKey($ref)) { $geoTriMap[$ref] } else { 0 }
                PositionCount = if ($geoPositionMap.ContainsKey($ref)) { $geoPositionMap[$ref] } else { 0 }
            }

            if ($isCollisionNode) {
                $collisionNodeRefs.Add($entry)
            }
            else {
                $visibleRefs.Add($entry)
            }
        }
    }

    $hasCollisionNodes = $collisionNodeRefs.Count -gt 0
    $hasPreciseCollision = @($collisionNodeRefs | Where-Object {
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

    [PSCustomObject]@{
        HasCollisionNodes = $hasCollisionNodes
        HasPreciseCollision = $hasPreciseCollision
        CollisionNodeRefs = $collisionNodeRefs
    }
}

function Test-DaeFile {
    param([string]$FilePath)

    $results = New-Object System.Collections.Generic.List[object]
    $fileName = Split-Path -Leaf $FilePath

    try {
        $xml = New-Object System.Xml.XmlDocument
        $xml.Load($FilePath)
    }
    catch {
        $results.Add((New-Result -File $fileName -Level 'ERROR' -Message "XML parse failed: $($_.Exception.Message)"))
        return $results
    }

    if ($xml.DocumentElement.LocalName -ne 'COLLADA') {
        $results.Add((New-Result -File $fileName -Level 'ERROR' -Message 'Root node is not COLLADA.'))
    }

    $upAxis = Get-NodeText -Node ($xml.SelectSingleNode('/*[local-name()="COLLADA"]/*[local-name()="asset"]/*[local-name()="up_axis"]'))
    if (-not [string]::IsNullOrWhiteSpace($upAxis) -and $upAxis -ne 'Z_UP') {
        $results.Add((New-Result -File $fileName -Level 'WARN' -Message "up_axis is '$upAxis', BeamNG assets are usually Z_UP."))
    }

    $geometryCount = @($xml.SelectNodes('/*[local-name()="COLLADA"]/*[local-name()="library_geometries"]/*[local-name()="geometry"]')).Count
    if ($geometryCount -eq 0) {
        $results.Add((New-Result -File $fileName -Level 'ERROR' -Message 'No geometry entries found.'))
    }

    $collisionDetails = Get-CollisionDetails -Xml $xml
    if (-not $collisionDetails.HasCollisionNodes) {
        $results.Add((New-Result -File $fileName -Level 'WARN' -Message 'No collision nodes reference geometry (expected Colmesh node with instance_geometry).'))
    }
    elseif ($collisionDetails.HasPreciseCollision) {
        $results.Add((New-Result -File $fileName -Level 'OK' -Message 'Precise collision geometry wiring detected.'))
    }
    else {
        $results.Add((New-Result -File $fileName -Level 'WARN' -Message 'Collision nodes exist, but only simple/basic collision geometry was detected.'))
    }

    $polyNodes = @($xml.SelectNodes('//*[local-name()="triangles" or local-name()="polylist" or local-name()="polygons"]'))
    if ($polyNodes.Count -eq 0) {
        $results.Add((New-Result -File $fileName -Level 'WARN' -Message 'No triangles/polylist/polygons nodes found.'))
    }

    foreach ($floatArray in $xml.SelectNodes('//*[local-name()="float_array"]')) {
        $counts = Test-FloatArrayCount -FloatArrayNode $floatArray
        if ($counts[0] -gt 0 -and $counts[0] -ne $counts[1]) {
            $arrayId = if ($floatArray.Attributes['id']) { $floatArray.Attributes['id'].Value } else { '(no id)' }
            $results.Add((New-Result -File $fileName -Level 'ERROR' -Message "float_array '$arrayId' count mismatch. Declared $($counts[0]), found $($counts[1])."))
        }
    }

    foreach ($polylist in $xml.SelectNodes('//*[local-name()="polylist"]')) {
        $message = Test-PolylistCounts -PolylistNode $polylist
        if ($null -ne $message) {
            $results.Add((New-Result -File $fileName -Level 'ERROR' -Message $message))
        }
    }

    foreach ($imageNode in $xml.SelectNodes('/*[local-name()="COLLADA"]/*[local-name()="library_images"]/*[local-name()="image"]/*[local-name()="init_from"]')) {
        $imagePath = Get-NodeText -Node $imageNode
        if ([string]::IsNullOrWhiteSpace($imagePath)) {
            continue
        }

        if ($imagePath -match '^file://') {
            $results.Add((New-Result -File $fileName -Level 'WARN' -Message "Absolute texture URI found: $imagePath"))
            continue
        }

        if ($imagePath -match '^[A-Za-z]:/' -or $imagePath -match '^/[A-Za-z]:/') {
            $results.Add((New-Result -File $fileName -Level 'WARN' -Message "Absolute texture path found: $imagePath"))
            continue
        }

        $resolvedPath = Resolve-ImagePath -DaePath $FilePath -ImagePath $imagePath
        if ($null -eq $resolvedPath) {
            $results.Add((New-Result -File $fileName -Level 'WARN' -Message "Could not resolve texture path: $imagePath"))
            continue
        }

        if (-not (Test-Path $resolvedPath)) {
            $results.Add((New-Result -File $fileName -Level 'ERROR' -Message "Missing texture: $imagePath"))
        }
    }

    if ($results.Count -eq 0) {
        $results.Add((New-Result -File $fileName -Level 'OK' -Message 'No obvious import issues found.'))
    }

    return $results
}

$resolvedPath = Resolve-Path -LiteralPath $Path
$items = New-Object System.Collections.Generic.List[string]

if ((Get-Item -LiteralPath $resolvedPath).PSIsContainer) {
    $searchOption = if ($Recurse) { '-Recurse' } else { '' }
    if ($Recurse) {
        Get-ChildItem -LiteralPath $resolvedPath -Filter '*.dae' -File -Recurse | ForEach-Object { $items.Add($_.FullName) }
        Get-ChildItem -LiteralPath $resolvedPath -Filter '*.DAE' -File -Recurse | ForEach-Object { $items.Add($_.FullName) }
    }
    else {
        Get-ChildItem -LiteralPath $resolvedPath -Filter '*.dae' -File | ForEach-Object { $items.Add($_.FullName) }
        Get-ChildItem -LiteralPath $resolvedPath -Filter '*.DAE' -File | ForEach-Object { $items.Add($_.FullName) }
    }
}
else {
    $items.Add((Get-Item -LiteralPath $resolvedPath).FullName)
}

if ($items.Count -eq 0) {
    Write-Error 'No .dae files found for validation.'
    exit 1
}

$allResults = New-Object System.Collections.Generic.List[object]
foreach ($item in ($items | Sort-Object -Unique)) {
    foreach ($result in (Test-DaeFile -FilePath $item)) {
        $allResults.Add($result)
    }
}

$allResults | Sort-Object File, Level, Message | Format-Table -AutoSize

$errorCount = @($allResults | Where-Object { $_.Level -eq 'ERROR' }).Count
$warnCount = @($allResults | Where-Object { $_.Level -eq 'WARN' }).Count
$okCount = @($allResults | Where-Object { $_.Level -eq 'OK' }).Count

Write-Host ''
Write-Host "Summary: $okCount OK, $warnCount warnings, $errorCount errors"

if ($errorCount -gt 0) {
    exit 1
}

exit 0