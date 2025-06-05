# PowerShell 自動化腳本：一鍵啟動 ASP.NET Core 後端與 Angular 前端
# 請在專案根目錄執行本腳本

[Console]::OutputEncoding = [Text.Encoding]::UTF8

Start-Process powershell -ArgumentList 'dotnet run --project Backend/OnlineCoursePlatform.csproj' -WindowStyle Normal
Start-Process powershell -ArgumentList 'cd Frontend; npx ng serve' -WindowStyle Normal

Write-Host "Web:http://localhost:4200/" -ForegroundColor Green
