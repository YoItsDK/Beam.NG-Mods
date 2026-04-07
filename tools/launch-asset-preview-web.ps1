param(
    [string]$Preset = 'default',
    [int]$Port = 8765,
    [string]$ShapesDir,
    [string]$Search,
    [string]$ModelContains,
    [switch]$NoBrowser
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$scriptDir = $PSScriptRoot
$workspaceRoot = Split-Path -Parent $scriptDir
$serverScript = Join-Path $scriptDir 'asset-preview-web-server.ps1'
$profilesPath = Join-Path $scriptDir 'asset-preview-web.profiles.json'

if (-not (Test-Path -LiteralPath $serverScript)) {
    throw "Missing server script: $serverScript"
}

$profileData = $null
if (Test-Path -LiteralPath $profilesPath) {
    $json = Get-Content -LiteralPath $profilesPath -Raw
    $profileData = $json | ConvertFrom-Json
}

$selectedProfile = $null
if ($profileData -and $Preset) {
    $selectedProfile = $profileData.PSObject.Properties[$Preset]
    if (-not $selectedProfile) {
        $available = ($profileData.PSObject.Properties.Name | Sort-Object) -join ', '
        throw "Unknown preset '$Preset'. Available presets: $available"
    }
    $selectedProfile = $selectedProfile.Value
}

if ([string]::IsNullOrWhiteSpace($ShapesDir) -and $selectedProfile -and -not [string]::IsNullOrWhiteSpace([string]$selectedProfile.shapesDir)) {
    $ShapesDir = [string]$selectedProfile.shapesDir
}

if ([string]::IsNullOrWhiteSpace($Search) -and $selectedProfile) {
    $Search = [string]$selectedProfile.search
}

if ([string]::IsNullOrWhiteSpace($ModelContains) -and $selectedProfile) {
    $ModelContains = [string]$selectedProfile.modelContains
}

$source = 'server_source'
if ($selectedProfile -and -not [string]::IsNullOrWhiteSpace([string]$selectedProfile.source)) {
    $source = [string]$selectedProfile.source
}

$autoLoadFirst = $true
if ($selectedProfile -and $null -ne $selectedProfile.autoLoadFirst) {
    $autoLoadFirst = [bool]$selectedProfile.autoLoadFirst
}

$resolvedShapes = ''
if (-not [string]::IsNullOrWhiteSpace($ShapesDir)) {
    $candidate = $ShapesDir
    if (-not [System.IO.Path]::IsPathRooted($candidate)) {
        $candidate = Join-Path $workspaceRoot $candidate
    }
    $resolvedShapes = (Resolve-Path -LiteralPath $candidate).Path
}

$serverArgs = @('-NoProfile', '-ExecutionPolicy', 'Bypass', '-File', ('"{0}"' -f $serverScript), '-Port', $Port)
if (-not [string]::IsNullOrWhiteSpace($resolvedShapes)) {
    $serverArgs += @('-ShapesDir', ('"{0}"' -f $resolvedShapes))
}

$command = 'powershell ' + ($serverArgs -join ' ')
Start-Process -FilePath 'cmd.exe' -ArgumentList '/c', 'start', '"BeamNG Asset Web Server"', $command | Out-Null

$query = [System.Collections.Generic.List[string]]::new()
$query.Add('source=' + [System.Uri]::EscapeDataString($source))
$query.Add('autoLoadFirst=' + [System.Uri]::EscapeDataString($autoLoadFirst.ToString().ToLowerInvariant()))
if (-not [string]::IsNullOrWhiteSpace($Search)) {
    $query.Add('search=' + [System.Uri]::EscapeDataString($Search))
}
if (-not [string]::IsNullOrWhiteSpace($ModelContains)) {
    $query.Add('modelContains=' + [System.Uri]::EscapeDataString($ModelContains))
}

$url = "http://localhost:$Port/?$($query -join '&')"

if (-not $NoBrowser) {
    Start-Process $url | Out-Null
}

Write-Host "Launcher preset: $Preset"
Write-Host "Server URL: $url"
if (-not [string]::IsNullOrWhiteSpace($resolvedShapes)) {
    Write-Host "Shapes root: $resolvedShapes"
}
