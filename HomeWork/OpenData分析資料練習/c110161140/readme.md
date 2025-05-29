# OpenData 空氣品質分析作業

學號：C110161140  
姓名：張翌珉

---

## 📂 資料來源

- 來源網站：[https://data.gov.tw](https://data.gov.tw)
- 資料集名稱：空氣品質監測資料
- 檔案名稱：`wqx_p_06.csv`

---

## 🛠️ 程式功能說明

本作業使用 C# 撰寫主控台應用程式，讀取並分析政府開放資料（CSV 格式），功能如下：

- 使用 CsvHelper 套件讀取空氣品質 CSV 檔案
- 顯示資料總筆數
- 顯示前 5 筆記錄（測站名稱、縣市、污染物、濃度、時間）
- 使用 LINQ 查詢濃度最高的測站資料

---

## 🔧 使用的套件

- `CsvHelper`  
（安裝方式：`dotnet add package CsvHelper`）

---

## ▶️ 執行方式

1. 確保已安裝 [.NET SDK](https://dotnet.microsoft.com/)
2. 將 `Program.cs`、`data.cs` 和 `wqx_p_06.csv` 放在專案資料夾
3. 開啟終端機（Terminal）並執行：

```bash
dotnet run
