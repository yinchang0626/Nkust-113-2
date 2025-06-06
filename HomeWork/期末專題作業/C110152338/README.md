# 簡易線上課程平台（期末專題）

## 專案簡介
本專案為一個結合 ASP.NET Core MVC 後端與 Angular 前端的線上課程平台，支援課程瀏覽、模擬選課、課程管理等功能。前端與後端完全分離，資料庫採用 Entity Framework Core ORM。

---

## 作品展示影片
[https://youtu.be/9waIIIegZAk](https://youtu.be/9waIIIegZAk)

---

## 使用心得
自從有了 AI 工具，我們稱 VSCode + AI 為「VSCode vibe」，但距離真正的 100% 自動化還是有些距離。不過有 AI 的幫助，開發效率提升很多，就像這部影片所說的：
https://www.youtube.com/shorts/1_rSrkXovOk

這次使用的工具有 VSCode Copilot（GPT-4.1）、ROO CODE、Google Gemini 2.0 exp。前前後後花了 30~40 小時，大約 10 小時設計，20~30 小時都在除錯。AI 幫忙真的很方便，但有時候還是會花很多時間在修 bug。

AI enthusiasts are creating cobbled together apps using ai programming tools and they have little to no knowledge of actual coding. And they are doing it off of “vibes”。

---

## 專案架構

- **Frontend/**：Angular 前端專案
- **Backend/**：ASP.NET Core MVC 後端專案
- **nkust/**：資料庫 SQL、ERD、設計文件
- **start-all.ps1**：一鍵啟動前後端 PowerShell 腳本

---

## 安裝與執行步驟

1. **安裝環境**：
   - Node.js、npm
   - .NET 8 SDK
   - SQL Server 或 SQLite
2. **資料庫建立**：
   - 進入 `Backend/`，執行：
     ```shell
     dotnet ef database update
     ```
   - 或使用 `nkust/database.sql` 直接建立資料表
3. **啟動專案**：
   - 在專案根目錄執行：
     ```shell
     ./start-all.ps1
     ```
   - 或手動分別啟動：
     - `dotnet run --project Backend/OnlineCoursePlatform.csproj`
     - `cd Frontend && npm install && npx ng serve`
4. **瀏覽**：
   - 前端：http://localhost:4200/
   - 後端 API：http://localhost:5100/

---

## 資料庫結構

- 參見 `nkust/database.sql` 或 `nkust/期末.md` 之 ERD/表格設計
- 主要資料表：
  - Courses（課程）
  - Users（使用者）
  - Enrollments（選課紀錄）

---

## 已完成功能對照

### 使用者端（Angular）
- [x] 瀏覽課程列表
- [x] 查看課程詳情
- [x] 模擬註冊/選課（與帳號綁定，資料存於後端）
- [x] 我的選課清單、時程表
- [x] 登入/登出

### 管理員端（ASP.NET Core MVC）
- [x] 管理員登入
- [x] 課程管理（CRUD）
- [ ] 使用者註冊列表（可擴充）

---

## 開發過程困難與解法（摘要）
- 前後端資料同步與 API 設計需多次調整
- Entity Framework Core 導航屬性 required 導致建置錯誤，需改為 nullable
- Angular 與 .NET 資料型別對應、localStorage 與後端同步
- 除錯花費大量時間，AI 工具雖然方便但仍需人工判斷

---

## 未來可擴充方向
- 完整的使用者註冊/權限管理
- 課程搜尋/篩選、圖片上傳
- 單元測試、自動化部署
- 管理員後台更多功能

---

## 其他
- 專案已用 Git 進行版本控制
- 詳細進度與紀錄請見 `daily/readme.md`

---

祝您專案順利！