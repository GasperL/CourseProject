Set-Location -Path "./WebApp"
& dotnet ef database update
Set-Location -Path "./../"