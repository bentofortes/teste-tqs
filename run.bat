@echo off

start "Backend API" cmd /k "cd backend && dotnet run"
start "Frontend App" cmd /k "cd frontend && ng serve --open"

echo Application starting...