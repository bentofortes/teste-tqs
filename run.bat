@echo off

echo Starting frontend...
start cmd /k "cd frontend && npm install && npx ng serve --open"

echo Starting backend...
start cmd /k "cd backend && dotnet restore && dotnet run"

echo Done.