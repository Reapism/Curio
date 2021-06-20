Write-Host "Entity Framework Tools is required for running database powershell scripts" -ForegroundColor Yellow -BackgroundColor Gray

Write-Host "Listing all global dotnet tools" -ForegroundColor Blue -BackgroundColor Gray
dotnet tool list -g

$currentVersion = dotnet ef --version
Write-Host "Current version: " -ForegroundColor White -NoNewline
Write-Host "${currentVersion}" -ForegroundColor Green

Write-Host "Updating entity framework tools to the latest version globally if not up-to-date" -ForegroundColor Yellow
dotnet tool update --global dotnet-ef
