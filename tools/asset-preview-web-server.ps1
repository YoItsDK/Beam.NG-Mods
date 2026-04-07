param(
    [int]$Port = 8765,
    [string]$ShapesDir
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$workspaceRoot = Split-Path -Parent $PSScriptRoot
if ([string]::IsNullOrWhiteSpace($ShapesDir)) {
    $ShapesDir = Join-Path $workspaceRoot 'unpacked/jurassic_beam_park_localfix.disabled/levels/jurassic_beam_park/art/shapes'
}

$shapesPath = (Resolve-Path -LiteralPath $ShapesDir).Path
$htmlPath = Join-Path $PSScriptRoot 'asset-preview-web.html'
if (-not (Test-Path -LiteralPath $htmlPath)) {
    throw "Missing HTML UI: $htmlPath"
}

function Write-TextResponse {
    param(
        [Parameter(Mandatory = $true)]$Response,
        [Parameter(Mandatory = $true)][string]$Content,
        [Parameter(Mandatory = $true)][string]$ContentType,
        [int]$StatusCode = 200
    )

    $bytes = [System.Text.Encoding]::UTF8.GetBytes($Content)
    $Response.StatusCode = $StatusCode
    $Response.ContentType = "$ContentType; charset=utf-8"
    $Response.ContentLength64 = $bytes.Length
    $Response.OutputStream.Write($bytes, 0, $bytes.Length)
    $Response.OutputStream.Close()
}

function Write-JsonResponse {
    param(
        [Parameter(Mandatory = $true)]$Response,
        [Parameter(Mandatory = $true)]$Payload,
        [int]$StatusCode = 200
    )

    $json = $Payload | ConvertTo-Json -Depth 6
    Write-TextResponse -Response $Response -Content $json -ContentType 'application/json' -StatusCode $StatusCode
}

function Get-WorkspaceRelativePath {
    param(
        [Parameter(Mandatory = $true)][string]$Path,
        [Parameter(Mandatory = $true)][string]$BaseDir
    )

    $fullPath = (Resolve-Path -LiteralPath $Path).Path
    if ($fullPath.StartsWith($BaseDir, [System.StringComparison]::OrdinalIgnoreCase)) {
        return ($fullPath.Substring($BaseDir.Length).TrimStart('\') -replace '\\', '/')
    }

    return ($fullPath -replace '\\', '/')
}

function Get-GameAssetPath {
    param([string]$WorkspaceRelativePath)

    $normalized = ($WorkspaceRelativePath -replace '\\', '/').TrimStart('/')
    if ($normalized -match '^(?:unpacked/[^/]+/|repo/[^/]+/)?((?:levels|vehicles)/.+)$') {
        return $Matches[1]
    }

    return ''
}

function Get-Models {
    param([string]$BaseDir)

    $items = New-Object System.Collections.Generic.List[object]

    Get-ChildItem -LiteralPath $BaseDir -File -Recurse | Where-Object { $_.Extension -match '^\.dae$' } | ForEach-Object {
        $fullPath = $_.FullName
        $relativePath = $fullPath.Substring($BaseDir.Length).TrimStart('\\') -replace '\\', '/'
        $workspacePath = Get-WorkspaceRelativePath -Path $fullPath -BaseDir $workspaceRoot
        $items.Add([PSCustomObject]@{
            relativePath = $relativePath
            fullPath = $fullPath
            workspacePath = $workspacePath
            beamngPath = Get-GameAssetPath -WorkspaceRelativePath $workspacePath
        })
    }

    return $items | Sort-Object relativePath -Unique
}

$listener = New-Object System.Net.HttpListener
$prefix = "http://localhost:$Port/"
$listener.Prefixes.Add($prefix)
$listener.Start()

Write-Host "BeamNG asset web preview server running at $prefix"
Write-Host "Shapes dir: $shapesPath"
Write-Host 'Press Ctrl+C to stop.'

try {
    while ($listener.IsListening) {
        $context = $listener.GetContext()
        $request = $context.Request
        $response = $context.Response
        $path = $request.Url.AbsolutePath

        try {
            switch ($path) {
                '/' {
                    $html = Get-Content -LiteralPath $htmlPath -Raw
                    Write-TextResponse -Response $response -Content $html -ContentType 'text/html'
                    continue
                }
                '/api/health' {
                    Write-JsonResponse -Response $response -Payload @{
                        ok = $true
                        shapesDir = $shapesPath
                        workspaceRoot = $workspaceRoot
                    }
                    continue
                }
                '/api/models' {
                    $models = Get-Models -BaseDir $shapesPath
                    Write-JsonResponse -Response $response -Payload @{
                        ok = $true
                        shapesDir = $shapesPath
                        count = @($models).Count
                        models = @($models)
                    }
                    continue
                }
                '/api/model' {
                    $relPath = [System.Uri]::UnescapeDataString($request.QueryString['path'])
                    if ([string]::IsNullOrWhiteSpace($relPath)) {
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = 'Missing query string: path' } -StatusCode 400
                        continue
                    }

                    if ($relPath.Contains('..') -or $relPath.Contains(':')) {
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = 'Invalid path value.' } -StatusCode 400
                        continue
                    }

                    $combined = Join-Path $shapesPath ($relPath -replace '/', '\\')
                    if (-not (Test-Path -LiteralPath $combined)) {
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = 'Model file not found.' } -StatusCode 404
                        continue
                    }

                    $fullModelPath = (Resolve-Path -LiteralPath $combined).Path
                    if (-not $fullModelPath.StartsWith($shapesPath, [System.StringComparison]::OrdinalIgnoreCase)) {
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = 'Path outside shapes root.' } -StatusCode 400
                        continue
                    }

                    $xml = Get-Content -LiteralPath $fullModelPath -Raw
                    Write-TextResponse -Response $response -Content $xml -ContentType 'application/xml'
                    continue
                }
                default {
                    Write-JsonResponse -Response $response -Payload @{ ok = $false; error = 'Not found.' } -StatusCode 404
                    continue
                }
            }
        }
        catch {
            Write-JsonResponse -Response $response -Payload @{ ok = $false; error = $_.Exception.Message } -StatusCode 500
        }
    }
}
finally {
    if ($listener.IsListening) {
        $listener.Stop()
    }
    $listener.Close()
}
