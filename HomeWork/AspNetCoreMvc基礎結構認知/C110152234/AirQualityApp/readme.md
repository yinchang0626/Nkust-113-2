# 空氣品質資料查詢系統（Air Quality Web App）

本系統為使用 ASP.NET Core MVC 框架開發的網頁應用程式，目的為解析環保署提供的空氣品質開放資料（JSON 格式），並以表格與圖表的方式呈現 96 年至 105 年的測站資料。

---

## 🔧 專案技術

- ASP.NET Core MVC (.NET 8)
- Razor View
- C#
- Chart.js（繪製圖表）
- Bootstrap（表格排版）

---

## 📁 功能簡介

### ✅ JSON 資料解析
- 從 `wwwroot/data.json` 載入資料
- 對應欄位如：測站、項目、值、各年份數據

### ✅ 資料表格顯示
- 呈現每筆資料包含 96～105 年數值
- 支援不同測站與項目的比對

### ✅ 年度圖表視覺化
- 每筆資料附一張折線圖（使用 Chart.js）
- 讓使用者清楚觀察年度變化趨勢

---

## 📦 檔案結構

AirQualityMvcApp/
├── Controllers/
│ └── AirQualityController.cs
├── Models/
│ └── AirQualityRecord.cs
├── Services/
│ └── AirQualityService.cs
├── Views/
│ └── AirQuality/
│ └── Index.cshtml
├── wwwroot/
│ └── data.json
└── Program.cs / *.csproj


## 🚀 執行方式

1. 開啟專案資料夾  
2. 執行指令：

   ```bash
   dotnet run
