# L'Étoile Restaurant

## 法式餐廳智慧會員點餐推薦系統

本專案為 C# Windows Forms 開發之法式餐廳智慧會員點餐推薦系統，以高級法式餐廳為情境，模擬實際餐廳 POS 系統從會員登入、餐點選購、購物車結帳、會員註冊、熱銷分析到推薦系統的完整流程。

---

## 專案目的

本系統作為轉學申請成果作品，透過實作餐廳點餐與營運管理流程，練習 Windows Forms 介面設計、SQL Server 會員資料建置、購物車金額計算、熱銷分析圖表與推薦邏輯整合。

---

## 開發工具與技術

* Visual Studio 2022
* C# Windows Forms
* .NET Framework
* SQL Server
* T-SQL
* DataGridView
* Chart 圖表
* Windows Forms 事件驅動設計

---

## 系統功能

### 1. 會員登入與註冊

系統可依會員電話號碼登入，並判斷會員狀態，作為後續折扣與結帳流程依據。會員資料建置於 SQL Server Members 資料表中，包含姓名、電話、Email、VIP 狀態、折扣率、註冊日期與累積消費金額。

### 2. 餐點選購

依前菜、主餐、甜點、飲品分類選餐，並顯示餐點圖片、價格與餐點介紹。

### 3. 購物車與結帳

整合已點餐點、數量、小計、會員折扣與實付金額，模擬完整餐廳 POS 結帳流程。

### 4. 推薦系統

依據主餐內容推薦適合搭配的飲品與甜點，並顯示推薦原因，提升點餐體驗。

### 5. 熱銷分析

統計餐點銷售比例，使用長條圖與圓餅圖呈現熱門餐點分析結果，協助了解餐點銷售狀況。

### 6. 多模組程式架構

本專案以 Form1 作為主要操作介面，並透過 RegisterForm、HotSellingForm、CartService、DatabaseHelper 等檔案分工處理會員註冊、熱銷分析、購物車邏輯與資料庫連線，使程式架構更清楚，也提升後續維護與擴充性。

---

## 主要檔案說明

| 檔案名稱              | 說明                            |
| ----------------- | ----------------------------- |
| Form1.cs          | 主要操作介面，負責餐點選購、購物車、結帳與推薦功能     |
| Form1.Designer.cs | Windows Forms 介面控制項配置         |
| RegisterForm.cs   | 會員註冊功能                        |
| HotSellingForm.cs | 熱銷分析圖表頁面                      |
| DatabaseHelper.cs | SQL Server 資料庫連線與查詢操作         |
| CartService.cs    | 購物車邏輯與金額計算                    |
| CartItem.cs       | 購物車項目資料結構                     |
| CreateTable.sql   | 建立 RestaurantDB 與 Members 資料表 |
| App.config        | 資料庫連線設定                       |

---

## SQL Server 資料表

本專案使用 Members 會員資料表，欄位包含：

* Id
* Name
* Phone
* Email
* IsVIP
* DiscountRate
* RegisterDate
* TotalPurchases

---

## 系統流程

```text
會員登入 / 註冊
↓
選擇餐點
↓
系統推薦飲品與甜點
↓
加入購物車
↓
套用會員折扣
↓
完成結帳
↓
更新會員消費紀錄與熱銷分析
```

---

## 如何執行本專案

1. 下載本專案或使用 Git clone。
2. 使用 Visual Studio 2022 開啟 `WindowsFormsApp4.sln`。
3. 開啟 SQL Server，執行 `CreateTable.sql` 建立 `RestaurantDB` 與 `Members` 資料表。
4. 確認 `App.config` 中的資料庫連線字串是否符合本機 SQL Server 設定。
5. 將餐點圖片依照程式設定路徑放置；若更換電腦執行，需確認圖片路徑是否正確。
6. 在 Visual Studio 中按下 F5 執行系統。

注意：本專案使用 SQL Server，因此若執行環境未安裝 SQL Server，需先完成資料庫環境設定。若未實際執行，可搭配書審資料中的系統截圖了解完整功能流程。

---

## 學習收穫

透過本專題，我學習到系統開發不只是完成操作畫面，而是需要整合資料設計、介面操作、邏輯判斷、圖表呈現與測試修正。在開發過程中，我練習 SQL 會員資料表規劃、購物車金額計算、會員折扣判斷與熱銷分析圖表呈現，也更理解多模組程式架構對於後續維護與擴充的重要性。

---

## 備註

本專案為學習與轉學申請成果作品，主要用於展示 C# Windows Forms、SQL Server、資料分析與系統整合能力。

