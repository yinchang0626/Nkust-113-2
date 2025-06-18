# finalHW – ASP.NET Core MVC 資料管理系統

本專案為使用 ASP.NET Core MVC 搭配 Entity Framework Core 所開發的 Web 應用程式，提供一個基本的人事資料管理平台，具備搜尋、建立、修改與刪除等完整 CRUD 功能。

## 📌 功能介紹

使用者可透過瀏覽器操作下列功能：

- **資料列表頁**（Index）  
  - 顯示所有資料記錄
  - 提供關鍵字搜尋功能，可搜尋姓名、Email、性別或公司名稱等欄位
- **新增資料**（Create）  
  - 提供表單輸入欄位建立新資料（皆為必填）
- **編輯資料**（Edit）  
  - 編輯指定資料紀錄內容
- **刪除資料**（Delete）  
  - 確認後刪除指定資料
- **隱私政策頁面**（Privacy）

## 🗂️ 資料結構（Model）

`DataContent.cs` 定義了本系統主要資料結構：

| 欄位         | 類型     | 必填 | 說明     |
|--------------|----------|------|----------|
| Id           | int      | ✔    | 主鍵     |
| FirstName    | string   | ✔    | 名       |
| LastName     | string   | ✔    | 姓       |
| Email        | string   | ✔    | 電子郵件 |
| Gender       | string   | ✔    | 性別     |
| CompanyName  | string   | ✔    | 公司名稱 |

## 🖥️ 執行畫面簡介

- **Index.cshtml**：提供搜尋欄位與資料表格，點擊每筆資料旁的「編輯」與「刪除」按鈕可進一步操作。
- **Create.cshtml**：輸入資料表單，按下「新增」提交資料。
- **Edit.cshtml**：預填現有資料，可修改後儲存。
- **Delete.cshtml**：顯示資料確認刪除畫面。
- **_ViewImports.cshtml / _ViewStart.cshtml**：設定 Razor 預設引用。
- **Privacy.cshtml**：靜態頁面示意。

## 🛠️ 建置與執行

### ✅ 環境需求

- .NET 6 SDK 或更新版本
- Visual Studio 2022 或以上（或 VS Code 搭配 C# 擴充套件）

### 📦 執行步驟

1. **還原套件**
   ```bash
   dotnet restore
   ```

2. **建置資料庫**
   ```bash
   dotnet ef database update
   ```

3. **執行專案**
   ```bash
   dotnet run
   ```

4. **瀏覽網址**
   ```
   https://localhost:5001/
   ```

## 📑 預設資料

資料庫初始化時自動載入兩筆資料，來自 `DatabaseContext.cs`：

- YAN YAN（yanyan@gmail.com）– Female – TSMC
- YAN LING（yanling@gmail.com）– Female – ASE

## 📁 專案大致結構

```
finalHW/
├── Controllers/
│   ├── HomeController.cs
│   └── DataAccessController.cs
├── Data/
│   └── DatabaseContext.cs
├── Models/
│   ├── DataContent.cs
│   └── ErrorViewModel.cs
├── Views/
│   ├── DataAccess/
│   ├── Shared/
│   └── _ViewStart.cshtml

```
