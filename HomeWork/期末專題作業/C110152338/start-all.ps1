# PowerShell 自動化腳本：一鍵重建資料庫並啟動 ASP.NET Core 後端與 Angular 前端
# 請在專案根目錄執行本腳本

[Console]::OutputEncoding = [Text.Encoding]::UTF8

# # 關閉舊的 OnlineCoursePlatform 執行個體
# Get-Process OnlineCoursePlatform -ErrorAction SilentlyContinue | Stop-Process -Force

# # 重新建立 migration 並更新資料庫
# cd Backend
# Remove-Item -Recurse -Force .\Migrations
# mkdir .\Migrations
# cd ..
# dotnet ef migrations add InitialCreate --project Backend

# # 強制重建資料庫
#  dotnet ef database drop -f --project Backend
#  dotnet ef database update --project Backend

# 啟動後端與前端
Start-Process powershell -ArgumentList 'dotnet run --project Backend/OnlineCoursePlatform.csproj' -WindowStyle Normal
Start-Process powershell -ArgumentList 'cd Frontend; npx ng serve' -WindowStyle Normal

Write-Host "Web: http://localhost:4200/" -ForegroundColor Green
