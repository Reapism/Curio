
Write-Host "Running the mini build script" -ForegroundColor Green

Write-Host "This builds the solution and does not run tests" -ForegroundColor Yellow

# Go back a directory

dotnet build ..\Curio.sln -c DEBUG