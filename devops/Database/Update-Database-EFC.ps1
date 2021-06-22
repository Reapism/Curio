# The powershell script to run migrations on a specific db context.
# Example usage
# Create autogenerated migration on Curio Client Db Context
# .\Update-Database-EFC.ps1 --projectPath '..\..\src\Curio.Persistence' --startupProjectPath '..\..\src\Curio.Persistence.Tools' --contextName 'CurioClientDbContext'
param
( 
    [Alias('p')]
    [Parameter(Mandatory)]
    $projectPath,

    [Alias('sp')]
    [Parameter(Mandatory)]
    $startupProjectPath,

    [Alias('c')]
    [Parameter(Mandatory)]
    $contextName,
    
    [Alias('m')]
    $migrationName
)

if ($null -eq $migrationName) {
    $today = Get-Date -Format "yyyyMMddHHmmss"
    $migrationName = "${today}_Migration";
}

Write-Host "Updating the database via dotnet CLI for Entity Framework Core" -ForegroundColor Green -BackgroundColor Gray
Write-Host ""

Write-Host "==============================================" -ForegroundColor Gray

$projectPath = Resolve-Path $projectPath
$projectPathExists = Test-Path $projectPath
$isProjectPathInvalid = ($null -eq $projectPath) -Or ($true -ne $projectPathExists)
if ($isProjectPathInvalid) {
    Write-Host "The project path ${projectPath} must exist!"
}

Write-Host "Project Path: " -ForegroundColor Blue -NoNewline
Write-Host ${projectPath} -ForegroundColor Green -NoNewline

$startupProjectPath = Resolve-Path $startupProjectPath
$startupProjectPathExists = Test-Path $startupProjectPath
$isStartupProjectPathInvalid = ($null -eq $startupProjectPath) -Or ($true -ne $startupProjectPathExists)
if ($isStartupProjectPathInvalid) {
    Write-Host "The startup project path ${startupProjectPath} must exist!"
}

Write-Host "Startup Project Path: " -ForegroundColor Blue -NoNewline
Write-Host ${startupProjectPath} -ForegroundColor Green -NoNewline

$isContextNameInvalid = ($null -eq $contextName)
if ($isContextNameInvalid) {
    Write-Error "The context name cannot be null or empty!"
}

Write-Host "DbContext: " -ForegroundColor Blue -NoNewline
Write-Host "${contextName}" -ForegroundColor Blue -BackgroundColor White
Write-Host "==============================================" -ForegroundColor Gray

dotnet ef migrations add $migrationName --project $projectPath --startup-project $startupProjectPath --context $contextName --verbose