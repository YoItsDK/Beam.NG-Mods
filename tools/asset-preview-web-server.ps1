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

function Write-BinaryResponse {
    param(
        [Parameter(Mandatory = $true)]$Response,
        [Parameter(Mandatory = $true)][byte[]]$Content,
        [Parameter(Mandatory = $true)][string]$ContentType,
        [int]$StatusCode = 200
    )

    $Response.StatusCode = $StatusCode
    $Response.ContentType = $ContentType
    $Response.ContentLength64 = $Content.Length
    $Response.OutputStream.Write($Content, 0, $Content.Length)
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

function Get-ContentTypeForPath {
    param([Parameter(Mandatory = $true)][string]$Path)

    switch -Regex ([System.IO.Path]::GetExtension($Path).ToLowerInvariant()) {
        '^\.dae$' { return 'application/xml; charset=utf-8' }
        '^\.xml$' { return 'application/xml; charset=utf-8' }
        '^\.json$' { return 'application/json; charset=utf-8' }
        '^\.png$' { return 'image/png' }
        '^\.jpe?g$' { return 'image/jpeg' }
        '^\.gif$' { return 'image/gif' }
        '^\.webp$' { return 'image/webp' }
        '^\.bmp$' { return 'image/bmp' }
        '^\.svg$' { return 'image/svg+xml; charset=utf-8' }
        '^\.dds$' { return 'image/vnd-ms.dds' }
        '^\.tga$' { return 'image/x-tga' }
        '^\.bin$' { return 'application/octet-stream' }
        default { return 'application/octet-stream' }
    }
}

function Resolve-AssetPath {
    param(
        [Parameter(Mandatory = $true)][string]$RootPath,
        [Parameter(Mandatory = $true)][string]$RelativePath
    )

    if ([string]::IsNullOrWhiteSpace($RelativePath)) {
        throw 'Missing asset path.'
    }

    if ($RelativePath.Contains('..') -or $RelativePath.Contains(':')) {
        throw 'Invalid path value.'
    }

    $combined = Join-Path $RootPath ($RelativePath -replace '/', '\\')
    if (-not (Test-Path -LiteralPath $combined)) {
        throw 'Asset file not found.'
    }

    $resolved = (Resolve-Path -LiteralPath $combined).Path
    if (-not $resolved.StartsWith($RootPath, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw 'Path outside shapes root.'
    }

    return $resolved
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

                    try {
                        $fullModelPath = Resolve-AssetPath -RootPath $shapesPath -RelativePath $relPath
                    }
                    catch {
                        $message = $_.Exception.Message
                        $code = if ($message -eq 'Asset file not found.') { 404 } else { 400 }
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = $message } -StatusCode $code
                        continue
                    }

                    $xml = Get-Content -LiteralPath $fullModelPath -Raw
                    Write-TextResponse -Response $response -Content $xml -ContentType 'application/xml'
                    continue
                }
                '/api/save-model' {
                    if ($request.HttpMethod -ne 'POST') {
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = 'Method not allowed. Use POST.' } -StatusCode 405
                        continue
                    }

                    $reader = New-Object System.IO.StreamReader($request.InputStream, $request.ContentEncoding)
                    $bodyText = $reader.ReadToEnd()
                    $reader.Dispose()

                    if ([string]::IsNullOrWhiteSpace($bodyText)) {
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = 'Missing JSON payload.' } -StatusCode 400
                        continue
                    }

                    try {
                        $payload = $bodyText | ConvertFrom-Json
                    }
                    catch {
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = 'Invalid JSON payload.' } -StatusCode 400
                        continue
                    }

                    $relPath = [string]$payload.path
                    $xml = [string]$payload.xml
                    if ([string]::IsNullOrWhiteSpace($relPath)) {
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = 'Missing payload.path.' } -StatusCode 400
                        continue
                    }
                    if ([string]::IsNullOrWhiteSpace($xml)) {
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = 'Missing payload.xml.' } -StatusCode 400
                        continue
                    }

                    try {
                        $fullModelPath = Resolve-AssetPath -RootPath $shapesPath -RelativePath $relPath
                    }
                    catch {
                        $message = $_.Exception.Message
                        $code = if ($message -eq 'Asset file not found.') { 404 } else { 400 }
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = $message } -StatusCode $code
                        continue
                    }

                    try {
                        [System.IO.File]::WriteAllText($fullModelPath, $xml, [System.Text.UTF8Encoding]::new($false))
                    }
                    catch {
                        Write-JsonResponse -Response $response -Payload @{ ok = $false; error = "Failed to write file: $($_.Exception.Message)" } -StatusCode 500
                        continue
                    }

                    Write-JsonResponse -Response $response -Payload @{
                        ok = $true
                        path = $relPath
                        fullPath = $fullModelPath
                        bytes = ([System.Text.Encoding]::UTF8.GetByteCount($xml))
                    }
                    continue
                }
                default {
                    if ($path.StartsWith('/files/', [System.StringComparison]::OrdinalIgnoreCase)) {
                        $relPath = [System.Uri]::UnescapeDataString($path.Substring(7))
                        try {
                            $fullAssetPath = Resolve-AssetPath -RootPath $shapesPath -RelativePath $relPath
                        }
                        catch {
                            $message = $_.Exception.Message
                            $code = if ($message -eq 'Asset file not found.') { 404 } else { 400 }
                            Write-JsonResponse -Response $response -Payload @{ ok = $false; error = $message } -StatusCode $code
                            continue
                        }

                        $bytes = [System.IO.File]::ReadAllBytes($fullAssetPath)
                        $contentType = Get-ContentTypeForPath -Path $fullAssetPath
                        Write-BinaryResponse -Response $response -Content $bytes -ContentType $contentType
                        continue
                    }

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
