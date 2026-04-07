param(
    [Parameter(Mandatory = $true, Position = 0)]
    [string]$Path,

    [switch]$NoUi,

    [switch]$RequireCollision
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Parse-FloatArray {
    param([string]$Text)

    if ([string]::IsNullOrWhiteSpace($Text)) {
        return @()
    }

    return ($Text -split '\s+' | Where-Object { $_ -ne '' } | ForEach-Object { [double]$_ })
}

function Parse-IntArray {
    param([string]$Text)

    if ([string]::IsNullOrWhiteSpace($Text)) {
        return @()
    }

    return ($Text -split '\s+' | Where-Object { $_ -ne '' } | ForEach-Object { [int]$_ })
}

function Get-GeometryTriangleCountMap {
    param([xml]$Xml)

    $map = @{}
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

        $map[$id] = $count
    }

    return $map
}

function Get-CollisionDetails {
    param([xml]$Xml)

    $collisionPattern = '(?i)(^|[_.-])(colmesh|collision|collider|col)([_.-]|$)'
    $geoTriMap = Get-GeometryTriangleCountMap -Xml $Xml
    $geoPositionMap = @{}
    foreach ($geo in @($Xml.SelectNodes('/*[local-name()="COLLADA"]/*[local-name()="library_geometries"]/*[local-name()="geometry"]'))) {
        $id = if ($geo.Attributes['id']) { $geo.Attributes['id'].Value } else { '' }
        if ([string]::IsNullOrWhiteSpace($id)) {
            continue
        }

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

function Get-CollisionStats {
    param([xml]$Xml)

    $collisionPattern = '(?i)(^|[_.-])(colmesh|collision|collider|col)([_.-]|$)'
    $geoTriMap = Get-GeometryTriangleCountMap -Xml $Xml

    $collisionGeometryIds = New-Object 'System.Collections.Generic.HashSet[string]'

    foreach ($geo in @($Xml.SelectNodes('/*[local-name()="COLLADA"]/*[local-name()="library_geometries"]/*[local-name()="geometry"]'))) {
        $id = if ($geo.Attributes['id']) { $geo.Attributes['id'].Value } else { '' }
        $name = if ($geo.Attributes['name']) { $geo.Attributes['name'].Value } else { '' }
        if ($id -match $collisionPattern -or $name -match $collisionPattern) {
            [void]$collisionGeometryIds.Add($id)
        }
    }

    foreach ($node in @($Xml.SelectNodes('//*[local-name()="node"]'))) {
        $nodeId = if ($node.Attributes['id']) { $node.Attributes['id'].Value } else { '' }
        $nodeName = if ($node.Attributes['name']) { $node.Attributes['name'].Value } else { '' }
        if ($nodeId -notmatch $collisionPattern -and $nodeName -notmatch $collisionPattern) {
            continue
        }

        foreach ($inst in @($node.SelectNodes('.//*[local-name()="instance_geometry"]'))) {
            if ($inst.Attributes['url']) {
                $ref = $inst.Attributes['url'].Value.TrimStart('#')
                if (-not [string]::IsNullOrWhiteSpace($ref)) {
                    [void]$collisionGeometryIds.Add($ref)
                }
            }
        }
    }

    $collisionTriangles = 0
    foreach ($geoId in $collisionGeometryIds) {
        if ($geoTriMap.ContainsKey($geoId)) {
            $collisionTriangles += $geoTriMap[$geoId]
        }
    }

    $collisionDetails = Get-CollisionDetails -Xml $Xml
    $hasCollision = $collisionGeometryIds.Count -gt 0
    $hasPreciseCollision = $collisionDetails.HasPreciseCollision
    $status = if ($hasCollision) { 'GOOD' } else { 'WARN' }
    $message = if ($hasCollision) {
        'Collision mesh naming detected.'
    }
    else {
        'No collision mesh naming found (expected names like Colmesh-1 / collision_*).'
    }

    [PSCustomObject]@{
        Status = $status
        HasCollision = $hasCollision
        HasPreciseCollision = $hasPreciseCollision
        GeometryCount = $collisionGeometryIds.Count
        TriangleCount = $collisionTriangles
        Message = $message
    }
}

function New-PreviewBitmap {
    param(
        [xml]$Xml,
        [int]$Width = 860,
        [int]$Height = 520
    )

    Add-Type -AssemblyName System.Drawing

    $bg = [System.Drawing.Color]::FromArgb(24, 28, 34)
    $gridColor = [System.Drawing.Color]::FromArgb(42, 48, 58)
    $lineColor = [System.Drawing.Color]::FromArgb(132, 212, 255)

    $bmp = New-Object System.Drawing.Bitmap($Width, $Height)
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
    $g.Clear($bg)

    $gridPen = New-Object System.Drawing.Pen($gridColor, 1)
    for ($x = 40; $x -lt $Width; $x += 40) {
        $g.DrawLine($gridPen, $x, 0, $x, $Height)
    }
    for ($y = 40; $y -lt $Height; $y += 40) {
        $g.DrawLine($gridPen, 0, $y, $Width, $y)
    }

    $allPoints = New-Object System.Collections.Generic.List[object]
    $lineSegments = New-Object System.Collections.Generic.List[object]

    $triangleNodes = @($Xml.SelectNodes('//*[local-name()="triangles"]'))
    foreach ($triNode in $triangleNodes) {
        $inputs = @($triNode.SelectNodes('./*[local-name()="input"]'))
        if ($inputs.Count -eq 0) {
            continue
        }

        $vertexInput = $inputs | Where-Object { $_.Attributes['semantic'] -and $_.Attributes['semantic'].Value -eq 'VERTEX' } | Select-Object -First 1
        if ($null -eq $vertexInput) {
            continue
        }

        $vertexOffset = [int]$vertexInput.Attributes['offset'].Value
        $vertexSource = $vertexInput.Attributes['source'].Value.TrimStart('#')

        $verticesNode = $Xml.SelectSingleNode("//*[local-name()='vertices' and @id='$vertexSource']")
        if ($null -eq $verticesNode) {
            continue
        }

        $positionInput = $verticesNode.SelectSingleNode("./*[local-name()='input' and @semantic='POSITION']")
        if ($null -eq $positionInput) {
            continue
        }

        $positionSource = $positionInput.Attributes['source'].Value.TrimStart('#')
        $floatArrayNode = $Xml.SelectSingleNode("//*[local-name()='source' and @id='$positionSource']/*[local-name()='float_array']")
        if ($null -eq $floatArrayNode) {
            continue
        }

        $positionsRaw = Parse-FloatArray -Text $floatArrayNode.InnerText
        if ($positionsRaw.Count -lt 3) {
            continue
        }

        $positions = New-Object System.Collections.Generic.List[object]
        for ($i = 0; $i -le ($positionsRaw.Count - 3); $i += 3) {
            $x = $positionsRaw[$i]
            $y = $positionsRaw[$i + 1]
            $z = $positionsRaw[$i + 2]
            $u = $x - $y
            $v = ($x + $y) * 0.5 - ($z * 1.2)
            $pt = [PSCustomObject]@{ U = $u; V = $v }
            $positions.Add($pt)
            $allPoints.Add($pt)
        }

        $pNode = $triNode.SelectSingleNode('./*[local-name()="p"]')
        if ($null -eq $pNode) {
            continue
        }

        $indices = Parse-IntArray -Text $pNode.InnerText
        if ($indices.Count -eq 0) {
            continue
        }

        $maxOffset = ($inputs | ForEach-Object { [int]$_.Attributes['offset'].Value } | Measure-Object -Maximum).Maximum
        $stride = [int]$maxOffset + 1
        $triangleCount = [int]($indices.Count / (3 * $stride))

        for ($t = 0; $t -lt $triangleCount; $t++) {
            $base = $t * 3 * $stride
            $i0 = $indices[$base + $vertexOffset]
            $i1 = $indices[$base + $stride + $vertexOffset]
            $i2 = $indices[$base + (2 * $stride) + $vertexOffset]

            if ($i0 -lt 0 -or $i1 -lt 0 -or $i2 -lt 0) {
                continue
            }
            if ($i0 -ge $positions.Count -or $i1 -ge $positions.Count -or $i2 -ge $positions.Count) {
                continue
            }

            $p0 = $positions[$i0]
            $p1 = $positions[$i1]
            $p2 = $positions[$i2]

            $lineSegments.Add([PSCustomObject]@{ A = $p0; B = $p1 })
            $lineSegments.Add([PSCustomObject]@{ A = $p1; B = $p2 })
            $lineSegments.Add([PSCustomObject]@{ A = $p2; B = $p0 })
        }
    }

    if ($allPoints.Count -eq 0 -or $lineSegments.Count -eq 0) {
        $font = New-Object System.Drawing.Font('Segoe UI', 16, [System.Drawing.FontStyle]::Bold)
        $brush = New-Object System.Drawing.SolidBrush([System.Drawing.Color]::FromArgb(255, 230, 230, 230))
        $g.DrawString('No mesh preview data found', $font, $brush, 30, 30)
        $g.Dispose()
        return $bmp
    }

    $minU = ($allPoints | Measure-Object -Property U -Minimum).Minimum
    $maxU = ($allPoints | Measure-Object -Property U -Maximum).Maximum
    $minV = ($allPoints | Measure-Object -Property V -Minimum).Minimum
    $maxV = ($allPoints | Measure-Object -Property V -Maximum).Maximum

    $spanU = [Math]::Max(0.0001, ($maxU - $minU))
    $spanV = [Math]::Max(0.0001, ($maxV - $minV))

    $margin = 28
    $sx = ($Width - (2 * $margin)) / $spanU
    $sy = ($Height - (2 * $margin)) / $spanV
    $scale = [Math]::Min($sx, $sy)

    $project = {
        param($pt)
        $px = (($pt.U - $minU) * $scale) + $margin
        $py = (($pt.V - $minV) * $scale) + $margin
        $py = $Height - $py
        return [System.Drawing.PointF]::new([float]$px, [float]$py)
    }

    $meshPen = New-Object System.Drawing.Pen($lineColor, 1.0)
    $drawLimit = [Math]::Min(120000, $lineSegments.Count)
    for ($i = 0; $i -lt $drawLimit; $i++) {
        $seg = $lineSegments[$i]
        $a = & $project $seg.A
        $b = & $project $seg.B
        $g.DrawLine($meshPen, $a, $b)
    }

    $titleBrush = New-Object System.Drawing.SolidBrush([System.Drawing.Color]::FromArgb(200, 230, 235, 245))
    $titleFont = New-Object System.Drawing.Font('Segoe UI', 10, [System.Drawing.FontStyle]::Regular)
    $g.DrawString('Preview (isometric wireframe)', $titleFont, $titleBrush, 12, 10)

    $g.Dispose()
    return $bmp
}

function Get-DaeStats {
    param([xml]$Xml)

    $geometryCount = @($Xml.SelectNodes('/*[local-name()="COLLADA"]/*[local-name()="library_geometries"]/*[local-name()="geometry"]')).Count

    $triangleCount = 0
    foreach ($tri in @($Xml.SelectNodes('//*[local-name()="triangles"]'))) {
        if ($tri.Attributes['count']) {
            $triangleCount += [int]$tri.Attributes['count'].Value
        }
    }

    $vertexCount = 0
    foreach ($fa in @($Xml.SelectNodes("//*[local-name()='source' and contains(@id,'positions')]/*[local-name()='float_array']"))) {
        if ($fa.Attributes['count']) {
            $vertexCount += ([int]$fa.Attributes['count'].Value / 3)
        }
    }

    [PSCustomObject]@{
        GeometryCount = $geometryCount
        TriangleCount = $triangleCount
        VertexCount = [int]$vertexCount
    }
}

$resolvedPath = (Resolve-Path -LiteralPath $Path).Path
$validatorPath = Join-Path $PSScriptRoot 'validate-beamng-dae.ps1'
if (-not (Test-Path $validatorPath)) {
    throw "Missing validator script: $validatorPath"
}

$validatorOutput = & $validatorPath $resolvedPath 6>&1 2>&1 | Out-String
$summaryMatch = [regex]::Match($validatorOutput, 'Summary:\s*(\d+)\s+OK,\s*(\d+)\s+warnings,\s*(\d+)\s+errors')

$okCount = 0
$warnCount = 0
$errorCount = 0
if ($summaryMatch.Success) {
    $okCount = [int]$summaryMatch.Groups[1].Value
    $warnCount = [int]$summaryMatch.Groups[2].Value
    $errorCount = [int]$summaryMatch.Groups[3].Value
}

[xml]$xml = Get-Content -Raw -LiteralPath $resolvedPath
$stats = Get-DaeStats -Xml $xml
$collisionStats = Get-CollisionStats -Xml $xml

$status = 'GOOD'
$statusColor = 'Green'
if ($errorCount -gt 0) {
    $status = 'FAIL'
    $statusColor = 'Red'
}
elseif ($warnCount -gt 0) {
    $status = 'WARN'
    $statusColor = 'DarkOrange'
}

if ($RequireCollision -and -not $collisionStats.HasCollision) {
    $status = 'FAIL'
    $statusColor = 'Red'
}

if ($NoUi) {
    Write-Host "Status: $status"
    Write-Host "File: $resolvedPath"
    Write-Host "Geometry: $($stats.GeometryCount)  Triangles: $($stats.TriangleCount)  Vertices: $($stats.VertexCount)"
    Write-Host "Validator: $okCount OK, $warnCount warnings, $errorCount errors"
    Write-Host "Collision: $($collisionStats.Status)  Geometries: $($collisionStats.GeometryCount)  Triangles: $($collisionStats.TriangleCount)"
    Write-Host "Precise Collision: $(if ($collisionStats.HasPreciseCollision) { 'YES' } else { 'NO' })"
    Write-Host "Collision Note: $($collisionStats.Message)"
    exit $(if ($errorCount -gt 0 -or ($RequireCollision -and -not $collisionStats.HasCollision)) { 1 } else { 0 })
}

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

$bitmap = New-PreviewBitmap -Xml $xml

$form = New-Object System.Windows.Forms.Form
$form.Text = 'BeamNG Asset Pre-Flight Preview'
$form.StartPosition = 'CenterScreen'
$form.Size = New-Object System.Drawing.Size(980, 760)
$form.MinimumSize = New-Object System.Drawing.Size(860, 640)

$layout = New-Object System.Windows.Forms.TableLayoutPanel
$layout.Dock = 'Fill'
$layout.RowCount = 2
$layout.ColumnCount = 1
$layout.RowStyles.Add((New-Object System.Windows.Forms.RowStyle([System.Windows.Forms.SizeType]::Percent, 74)))
$layout.RowStyles.Add((New-Object System.Windows.Forms.RowStyle([System.Windows.Forms.SizeType]::Percent, 26)))

$picture = New-Object System.Windows.Forms.PictureBox
$picture.Dock = 'Fill'
$picture.BackColor = [System.Drawing.Color]::FromArgb(24, 28, 34)
$picture.SizeMode = 'Zoom'
$picture.Image = $bitmap

$panel = New-Object System.Windows.Forms.Panel
$panel.Dock = 'Fill'
$panel.Padding = New-Object System.Windows.Forms.Padding(12)

$statusLabel = New-Object System.Windows.Forms.Label
$statusLabel.AutoSize = $true
$statusLabel.Font = New-Object System.Drawing.Font('Segoe UI', 18, [System.Drawing.FontStyle]::Bold)
$statusLabel.ForeColor = [System.Drawing.Color]::$statusColor
$statusLabel.Text = "Status: $status"
$statusLabel.Location = New-Object System.Drawing.Point(8, 6)

$details = New-Object System.Windows.Forms.Label
$details.AutoSize = $true
$details.Font = New-Object System.Drawing.Font('Segoe UI', 10)
$details.ForeColor = [System.Drawing.Color]::FromArgb(230, 230, 230)
$details.Text = "Geometry: $($stats.GeometryCount)    Triangles: $($stats.TriangleCount)    Vertices: $($stats.VertexCount)"
$details.Location = New-Object System.Drawing.Point(10, 50)

$validatorLabel = New-Object System.Windows.Forms.Label
$validatorLabel.AutoSize = $true
$validatorLabel.Font = New-Object System.Drawing.Font('Segoe UI', 10)
$validatorLabel.ForeColor = [System.Drawing.Color]::FromArgb(220, 220, 220)
$validatorLabel.Text = "Validator: $okCount OK, $warnCount warnings, $errorCount errors"
$validatorLabel.Location = New-Object System.Drawing.Point(10, 74)

$collisionColor = if ($collisionStats.Status -eq 'GOOD') {
    [System.Drawing.Color]::FromArgb(120, 220, 160)
}
else {
    [System.Drawing.Color]::FromArgb(255, 190, 90)
}

$collisionLabel = New-Object System.Windows.Forms.Label
$collisionLabel.AutoSize = $true
$collisionLabel.Font = New-Object System.Drawing.Font('Segoe UI', 10, [System.Drawing.FontStyle]::Bold)
$collisionLabel.ForeColor = $collisionColor
$collisionLabel.Text = "Collision: $($collisionStats.Status)  Geometries: $($collisionStats.GeometryCount)  Triangles: $($collisionStats.TriangleCount)"
$collisionLabel.Location = New-Object System.Drawing.Point(10, 98)

$preciseLabel = New-Object System.Windows.Forms.Label
$preciseLabel.AutoSize = $true
$preciseLabel.Font = New-Object System.Drawing.Font('Segoe UI', 10, [System.Drawing.FontStyle]::Bold)
$preciseLabel.ForeColor = if ($collisionStats.HasPreciseCollision) { [System.Drawing.Color]::FromArgb(120, 220, 160) } else { [System.Drawing.Color]::FromArgb(255, 190, 90) }
$preciseLabel.Text = "Precise Collision: $(if ($collisionStats.HasPreciseCollision) { 'YES' } else { 'NO' })"
$preciseLabel.Location = New-Object System.Drawing.Point(10, 122)

$pathLabel = New-Object System.Windows.Forms.Label
$pathLabel.AutoSize = $false
$pathLabel.Width = 920
$pathLabel.Height = 48
$pathLabel.Font = New-Object System.Drawing.Font('Consolas', 9)
$pathLabel.ForeColor = [System.Drawing.Color]::FromArgb(205, 205, 205)
$pathLabel.Text = $resolvedPath
$pathLabel.Location = New-Object System.Drawing.Point(10, 148)

$hintLabel = New-Object System.Windows.Forms.Label
$hintLabel.AutoSize = $true
$hintLabel.Font = New-Object System.Drawing.Font('Segoe UI', 9)
$hintLabel.ForeColor = [System.Drawing.Color]::FromArgb(190, 190, 190)
$hintLabel.Text = "ShapeEditor flow: open Asset Browser in BeamNG, find this DAE, click Show in ShapeEditor. $($collisionStats.Message)"
$hintLabel.Location = New-Object System.Drawing.Point(10, 198)

$copyBtn = New-Object System.Windows.Forms.Button
$copyBtn.Text = 'Copy Path'
$copyBtn.Size = New-Object System.Drawing.Size(110, 30)
$copyBtn.Location = New-Object System.Drawing.Point(10, 226)
$copyBtn.Add_Click({
    [System.Windows.Forms.Clipboard]::SetText($resolvedPath)
})

$openFolderBtn = New-Object System.Windows.Forms.Button
$openFolderBtn.Text = 'Open Folder'
$openFolderBtn.Size = New-Object System.Drawing.Size(110, 30)
$openFolderBtn.Location = New-Object System.Drawing.Point(130, 226)
$openFolderBtn.Add_Click({
    $dir = Split-Path -Parent $resolvedPath
    Start-Process explorer.exe $dir
})

$panel.Controls.Add($statusLabel)
$panel.Controls.Add($details)
$panel.Controls.Add($validatorLabel)
$panel.Controls.Add($collisionLabel)
$panel.Controls.Add($preciseLabel)
$panel.Controls.Add($pathLabel)
$panel.Controls.Add($hintLabel)
$panel.Controls.Add($copyBtn)
$panel.Controls.Add($openFolderBtn)

$layout.Controls.Add($picture, 0, 0)
$layout.Controls.Add($panel, 0, 1)
$form.Controls.Add($layout)

[void]$form.ShowDialog()