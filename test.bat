@echo off

cd H:\PROJECTS\skyra_api\TestProject1\bin\Debug\net8.0

dotnet test H:\PROJECTS\skyra_api\TestProject1\bin\Debug\net8.0\skyra.dll --filter TestCategory=%1

pause