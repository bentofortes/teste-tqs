@echo off

echo Installing frontend dependencies...
start cmd /k "cd frontend && npm install && ng serve --open"

echo Starting backend...
start cmd /k "cd backend && dotnet restore && dotnet run"

echo Done.