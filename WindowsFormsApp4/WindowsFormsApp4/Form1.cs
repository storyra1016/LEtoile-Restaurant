using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private Dictionary<string, MenuItem> menuItems;
        private Dictionary<string, List<string>> recommendations;

        // 購物車頁面控制項
        private DataGridView dgvCart;
        private Label lblSubtotal;
        private Label lblDiscount;
        private Label lblTotal;
        private ComboBox cmbMemberType;
        private Button btnClearCartTab;
        private Button btnCheckout;
       

        public class MenuItem
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public int HotScore { get; set; }
        }

        public Form1()
        {
            InitializeComponent();

            // ★ 強制修正 listBoxRecommendation 尺寸，避免蓋到左側
            listBoxRecommendation.Dock = DockStyle.Top;
            listBoxRecommendation.Height = 70;

            // ★ 強制修正 panelCenter Controls 加入順序
            panelCenter.Controls.Clear();
            panelCenter.Controls.Add(panelBottom);
            panelCenter.Controls.Add(listBoxRecommendation);
            panelCenter.Controls.Add(labelRecommendation);
            panelCenter.Controls.Add(textBoxDescription);
            panelCenter.Controls.Add(labelPrice);
            panelCenter.Controls.Add(pictureBoxDish);

            // 其他原有初始化...
            var loginControl = new LoginControl();
            loginControl.Dock = DockStyle.Fill;
            panelLogin.Controls.Add(loginControl);

            InitializeData();
            LoadMenuItems("全部");
            InitializeEmbeddedHotChart();
            InitializeHotSellingPage();
            InitializeCartTab();
            RefreshCartSummaryList();
        }
        private void BtnNavHome_Click(object sender, EventArgs e) => SwitchPanel(0);
        private void BtnNavMenu_Click(object sender, EventArgs e) => SwitchPanel(1);
        private void BtnNavHot_Click(object sender, EventArgs e) => SwitchPanel(2);
        private void BtnNavCart_Click(object sender, EventArgs e) => SwitchPanel(3);
       

        private void SwitchPanel(int index)
        {
            panelLogin.Visible = (index == 0);
            panelMenu.Visible = (index == 1);
            panelHotSelling.Visible = (index == 2);
            panelCart.Visible = (index == 3);
        }


        private void InitializeData()
        {
            menuItems = new Dictionary<string, MenuItem>
            {
                { "法式洋蔥湯", new MenuItem { Name="法式洋蔥湯", Category="前菜", Price=280, Description="精選洋蔥經過長時間焗烤，搭配馬鈴薯湯底，香濃濃郁。", HotScore=8 } },
                { "香煎干貝",   new MenuItem { Name="香煎干貝",   Category="前菜", Price=380, Description="新鮮干貝快速香煎至金黃，外香內嫩，口感絕佳。",         HotScore=9 } },
                { "紅酒燉牛肉", new MenuItem { Name="紅酒燉牛肉", Category="主餐", Price=520, Description="使用上等牛肉，搭配精選紅酒與蔬菜慢火燉煮，風味深厚。", HotScore=10 } },
                { "法式羊排",   new MenuItem { Name="法式羊排",   Category="主餐", Price=580, Description="羊排選用最嫩的部位，佐以普羅旺斯香草，肉質鮮嫩。",     HotScore=9 } },
                { "香煎鴨胸",   new MenuItem { Name="香煎鴨胸",   Category="主餐", Price=450, Description="鴨胸以特殊手法香煎至外酥內嫩，搭配櫻桃醬。",           HotScore=8 } },
                { "焦糖布蕾",   new MenuItem { Name="焦糖布蕾",   Category="甜點", Price=180, Description="法式經典甜點，焦糖脆皮搭配滑順蛋奶液，入口即化。",     HotScore=9 } },
                { "馬卡龍",     new MenuItem { Name="馬卡龍",     Category="甜點", Price=120, Description="細緻杏仁粉製作，外脆內軟，多種口味可選。",             HotScore=7 } },
                { "紅酒",       new MenuItem { Name="紅酒",       Category="飲品", Price=380, Description="精選法國波爾多紅酒，香氣濃郁，搭配紅肉菜餚最佳。",     HotScore=8 } },
                { "氣泡水",     new MenuItem { Name="氣泡水",     Category="飲品", Price=80,  Description="清爽氣泡水，幫助消化，清潔口腔，最佳餐點搭配。",       HotScore=6 } }
            };

            recommendations = new Dictionary<string, List<string>>
            {
                { "法式洋蔥湯", new List<string> { "✦ 適合搭配紅酒，洋蔥甜味與單寧完美融合", "✦ 推薦後續品嚐主餐，暖胃開場首選" } },
                { "香煎干貝",   new List<string> { "✦ 搭配白酒更佳，海鮮鮮甜更突出", "✦ 開胃菜首選，份量輕盈" } },
                { "紅酒燉牛肉", new List<string> { "✦ 搭配紅酒，同款入菜再入杯層次更立體", "✦ 推薦焦糖布蕾作為甜點，甜鹹平衡收尾" } },
                { "法式羊排",   new List<string> { "✦ 搭配紅酒，草本香氣與果味相輔相成", "✦ 推薦馬卡龍作為甜點，法式風情完整呈現" } },
                { "香煎鴨胸",   new List<string> { "✦ 搭配紅酒，鴨肉細膩脂香口感柔順", "✦ 推薦馬卡龍，甜鹹對比令人驚艷" } },
                { "焦糖布蕾",   new List<string> { "✦ 完美的用餐尾聲，法式經典甜點", "✦ 搭配氣泡水，清爽中和甜膩感" } },
                { "馬卡龍",     new List<string> { "✦ 精緻小食，多種口味可選", "✦ 送禮佳品，包裝精美" } },
                { "紅酒",       new List<string> { "✦ 搭配紅肉菜餚效果最佳", "✦ 陳年佳釀，單寧豐富風味深厚" } },
                { "氣泡水",     new List<string> { "✦ 所有餐點通用，清新口腔", "✦ 幫助消化，每口都像重新開始" } }
            };
        }

        private void LoadMenuItems(string category)
        {
            listBoxMenuItems.Items.Clear();

            var items = category == "全部"
                ? menuItems.Values.OrderByDescending(x => x.HotScore).ToList()
                : menuItems.Values.Where(x => x.Category == category).ToList();

            foreach (var item in items)
                listBoxMenuItems.Items.Add(item.Name);

            if (listBoxMenuItems.Items.Count > 0)
                listBoxMenuItems.SelectedIndex = 0;
            else
                ClearDisplay();
        }

        private void BtnCategory_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            LoadMenuItems(btn.Text);
        }

        // 修改：按下熱銷排行按鈕 -> 將左側清單以熱銷排序並 Refresh 圓餅（chart 已嵌入）
        private void InitializeHotSellingPage()
        {
            panelHotSelling.Controls.Clear();
            panelHotSelling.BackColor = Color.FromArgb(245, 240, 224);

            var lblTitle = new Label
            {
                Text = "📊 熱銷分析",
                Dock = DockStyle.Top,
                Height = 50,
                Font = new Font("微軟正黑體", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 42, 74),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0)
            };

            var data = new[]
            {
        new { Name = "紅酒燉牛肉", Value = 28.0 },
        new { Name = "紅酒",       Value = 18.0 },
        new { Name = "焦糖布蕾",   Value = 15.0 },
        new { Name = "法式羊排",   Value = 12.0 },
        new { Name = "香煎鴨胸",   Value = 10.0 },
        new { Name = "香煎干貝",   Value = 8.0  },
        new { Name = "法式洋蔥湯", Value = 5.0  },
        new { Name = "馬卡龍",     Value = 3.0  },
        new { Name = "氣泡水",     Value = 1.0  }
    };

            var colors = new[]
            {
        Color.FromArgb(192,0,0),    Color.FromArgb(153,0,51),
        Color.FromArgb(204,102,0),  Color.FromArgb(153,51,0),
        Color.FromArgb(255,153,51), Color.FromArgb(255,204,102),
        Color.FromArgb(102,153,204),Color.FromArgb(153,204,153),
        Color.FromArgb(180,180,180)
    };

            var chartBar = new Chart { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 240, 224) };
            var barArea = new ChartArea("barArea");
            barArea.BackColor = Color.Transparent;
            barArea.AxisX.LabelStyle.Font = new Font("微軟正黑體", 9F);
            barArea.AxisY.LabelStyle.Font = new Font("微軟正黑體", 9F);
            barArea.AxisX.MajorGrid.Enabled = false;
            barArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            barArea.AxisY.Title = "銷售佔比 (%)";
            barArea.AxisY.TitleFont = new Font("微軟正黑體", 9F);
            chartBar.ChartAreas.Add(barArea);

            var barSeries = new Series("sales")
            {
                ChartType = SeriesChartType.Bar,
                ChartArea = "barArea",
                IsValueShownAsLabel = true,
                Font = new Font("微軟正黑體", 9F),
                LabelForeColor = Color.Black
            };
            for (int i = 0; i < data.Length; i++)
            {
                var pt = new DataPoint();
                pt.AxisLabel = data[i].Name;
                pt.YValues = new double[] { data[i].Value };
                pt.Label = $"{data[i].Value}%";
                pt.Color = colors[i];
                barSeries.Points.Add(pt);
            }
            chartBar.Series.Add(barSeries);

            var chartPie = new Chart { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 240, 224) };
            var pieArea = new ChartArea("pieArea");
            pieArea.BackColor = Color.Transparent;
            chartPie.ChartAreas.Add(pieArea);

            var legend = new Legend("pieLegend");
            legend.Docking = Docking.Bottom;
            legend.BackColor = Color.Transparent;
            legend.Font = new Font("微軟正黑體", 9F);
            chartPie.Legends.Add(legend);

            var pieSeries = new Series("pie")
            {
                ChartType = SeriesChartType.Pie,
                ChartArea = "pieArea",
                IsValueShownAsLabel = true,
                Font = new Font("微軟正黑體", 10F),
                LabelForeColor = Color.Black
            };
            for (int i = 0; i < data.Length; i++)
            {
                var pt = new DataPoint();
                pt.YValues = new double[] { data[i].Value };
                pt.LegendText = data[i].Name;
                pt.Label = $"{data[i].Value}%";
                pt.Color = colors[i];
                pieSeries.Points.Add(pt);
            }
            pieSeries["PieLabelStyle"] = "Outside";
            pieSeries["PieDrawingStyle"] = "SoftEdge";
            pieSeries["PieStartAngle"] = "270";
            chartPie.Series.Add(pieSeries);

     var split = new SplitContainer
{
    Dock = DockStyle.Fill,
    BackColor = Color.FromArgb(245, 240, 224),
    SplitterDistance = 400
};

split.Panel1.Controls.Add(chartBar);
split.Panel2.Controls.Add(chartPie);
panelHotSelling.Controls.Add(split);

// Set MinSize after adding (when Width is valid)
split.Panel1MinSize = 200;
split.Panel2MinSize = 200;

split.SizeChanged += (s, ev) =>
{
    if (split.Width > 400)
        split.SplitterDistance = split.Width / 2;
};

            panelHotSelling.Controls.Add(lblTitle);
        }

        private void ListBoxMenuItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxMenuItems.SelectedIndex == -1) return;

            var name = listBoxMenuItems.SelectedItem.ToString();
            if (menuItems.TryGetValue(name, out var item))
                DisplayDishDetails(item);
        }

     

        private void ClearDisplay()
        {
            labelPrice.Text = "價格: NT$0";
            textBoxDescription.Text = "";
            listBoxRecommendation.Items.Clear();
            pictureBoxDish.Image = null;
            pictureBoxDish.BackColor = Color.LightGray;
        }

        // 加入購物車，並更新購物車摘要 ListBox（panelRecommendation 上方）與購物車頁面
        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            if (listBoxMenuItems.SelectedIndex == -1)
            {
                MessageBox.Show("請先選擇餐點");
                return;
            }

            var name = listBoxMenuItems.SelectedItem.ToString();
            if (!menuItems.TryGetValue(name, out var item))
            {
                MessageBox.Show("選取餐點資料異常");
                return;
            }

            CartService.AddItem(item.Name, item.Price, 1);
            RefreshCartSummaryList();
            RefreshCartGrid();

            MessageBox.Show($"✓ 已將「{item.Name}」加入購物車");
        }

        private void BtnAddToBundle_Click(object sender, EventArgs e)
        {
            if (listBoxMenuItems.SelectedIndex == -1)
            { MessageBox.Show("請先選擇餐點"); return; }

            var name = listBoxMenuItems.SelectedItem.ToString();

            // 加入主餐點本身
            if (menuItems.TryGetValue(name, out var mainItem))
                CartService.AddItem(mainItem.Name, mainItem.Price, 1);

            // 從推薦原因抓出推薦品項名稱，若存在於 menuItems 則一併加入
            if (recommendations.TryGetValue(name, out var recs))
            {
                foreach (var rec in recs)
                {
                    foreach (var kvp in menuItems)
                    {
                        if (rec.Contains(kvp.Key))
                        {
                            CartService.AddItem(kvp.Value.Name, kvp.Value.Price, 1);
                            break;
                        }
                    }
                }
            }

            RefreshCartSummaryList();
            RefreshCartGrid();
            MessageBox.Show($"✓ 已將「{name}」及推薦套餐品項加入購物車");
        }

        // 初始化嵌入圓餅圖（left panel 的 chartHotSelling）
        private void InitializeEmbeddedHotChart()
        {
            try
            {
                chartHotSelling.Series.Clear();
                chartHotSelling.ChartAreas.Clear();
                chartHotSelling.Legends.Clear();

                var area = new ChartArea("hotArea");
                area.BackColor = Color.Transparent;
                area.AxisX.Enabled = AxisEnabled.False;
                area.AxisY.Enabled = AxisEnabled.False;
                chartHotSelling.ChartAreas.Add(area);

                var legend = new Legend("hotLegend");
                legend.Docking = Docking.Right;
                legend.BackColor = Color.Transparent;
                chartHotSelling.Legends.Add(legend);

                var series = new Series("hot")
                {
                    ChartType = SeriesChartType.Pie,
                    ChartArea = "hotArea",
                    IsValueShownAsLabel = true,
                    Font = new Font("微軟正黑體", 9F),
                    LabelForeColor = Color.Black
                };

                // 資料（依你指定的百分比）
                var data = new[]
                {
                    new { Name = "紅酒燉牛肉", Value = 28.0 },
                    new { Name = "紅酒", Value = 18.0 },
                    new { Name = "焦糖布蕾", Value = 15.0 },
                    new { Name = "法式羊排", Value = 12.0 },
                    new { Name = "香煎鴨胸", Value = 10.0 },
                    new { Name = "香煎干貝", Value = 8.0 },
                    new { Name = "法式洋蔥湯", Value = 5.0 },
                    new { Name = "馬卡龍", Value = 3.0 },
                    new { Name = "氣泡水", Value = 1.0 }
                };

                var colors = new[]
                {
                    Color.FromArgb(192,0,0),
                    Color.FromArgb(153,0,51),
                    Color.FromArgb(204,102,0),
                    Color.FromArgb(153,51,0),
                    Color.FromArgb(255,153,51),
                    Color.FromArgb(255,204,102),
                    Color.FromArgb(102,153,204),
                    Color.FromArgb(153,204,153),
                    Color.FromArgb(180,180,180)
                };

                for (int i = 0; i < data.Length; i++)
                {
                    var pt = new DataPoint();
                    pt.YValues = new double[] { data[i].Value };
                    pt.LegendText = data[i].Name;
                    pt.Label = $"{data[i].Value}%";
                    pt.Color = colors[i % colors.Length];
                    series.Points.Add(pt);
                }

                // 標示在外側
                series["PieLabelStyle"] = "Inside";
                series["PieDrawingStyle"] = "SoftEdge";
                series["PieStartAngle"] = "270";

                chartHotSelling.Series.Add(series);
                chartHotSelling.Invalidate();
            }
            catch (Exception ex)
            {
                // 若 Chart 無法載入不致命
                Console.WriteLine("InitializeEmbeddedHotChart error: " + ex.Message);
            }
        }

        // 購物車頁面初始化（tabPage3）
        private void InitializeCartTab()
        {
            // 若 designer 已經提供 tabPage3，我們在此動態加入需要的控制項以避免改 Designer
            panelCart.Controls.Clear();
            panelCart.BackColor = Color.FromArgb(245, 240, 224);

            dgvCart = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 300,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvCart.Columns.Clear();
            dgvCart.Columns.Add("ColName", "品項");
            dgvCart.Columns.Add("ColUnitPrice", "單價");
            dgvCart.Columns.Add("ColQuantity", "數量");
            dgvCart.Columns.Add("ColSubtotal", "小計");

            cmbMemberType = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 220,
                Left = 20,
                Top = dgvCart.Bottom + 12
            };
            cmbMemberType.Items.Add("一般會員 (95折)");
            cmbMemberType.Items.Add("VIP 會員 (85折)");
            cmbMemberType.SelectedIndex = 0;
            cmbMemberType.SelectedIndexChanged += (s,e)=> RefreshCartGrid();

            lblSubtotal = new Label
            {
                AutoSize = false,
                Width = 300,
                Height = 24,
                Left = 20,
                Top = cmbMemberType.Bottom + 12,
                Text = "小計: NT$0",
                Font = new Font("微軟正黑體", 10F)
            };
            lblDiscount = new Label
            {
                AutoSize = false,
                Width = 300,
                Height = 24,
                Left = 20,
                Top = lblSubtotal.Bottom + 6,
                Text = "折扣: NT$0",
                Font = new Font("微軟正黑體", 10F)
            };
            lblTotal = new Label
            {
                AutoSize = false,
                Width = 300,
                Height = 28,
                Left = 20,
                Top = lblDiscount.Bottom + 6,
                Text = "實付: NT$0",
                Font = new Font("微軟正黑體", 12F, FontStyle.Bold)
            };

            btnClearCartTab = new Button
            {
                Text = "清空購物車",
                BackColor = Color.FromArgb(200, 40, 40),
                ForeColor = Color.White,
                Width = 140,
                Height = 36,
                Left = 360,
                Top = cmbMemberType.Bottom + 10,
                FlatStyle = FlatStyle.Flat
            };
            btnClearCartTab.FlatAppearance.BorderSize = 0;
            btnClearCartTab.Click += BtnClearCart_Click;

            // ★ 結帳按鈕
            var btnCheckoutTab = new Button
            {
                Text = "💳 結帳",
                BackColor = Color.FromArgb(28, 42, 74),
                ForeColor = Color.White,
                Width = 140,
                Height = 36,
                Left = 520,
                Top = cmbMemberType.Bottom + 10,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微軟正黑體", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCheckoutTab.FlatAppearance.BorderSize = 0;
            btnCheckoutTab.Click += BtnCheckout_Click;

            // ★ 標題
            var lblCartTitle = new Label
            {
                Text = "🛒 購物車結帳",
                Dock = DockStyle.Top,
                Height = 50,
                Font = new Font("微軟正黑體", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 42, 74),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0)
            };

            // ★ 全部加入 panelCart
            panelCart.Controls.Add(lblTotal);
            panelCart.Controls.Add(lblDiscount);
            panelCart.Controls.Add(lblSubtotal);
            panelCart.Controls.Add(btnCheckoutTab);
            panelCart.Controls.Add(btnClearCartTab);
            panelCart.Controls.Add(cmbMemberType);
            panelCart.Controls.Add(dgvCart);
            panelCart.Controls.Add(lblCartTitle);


            RefreshCartGrid();
        }

        private void RefreshCartGrid()
        {
            if (dgvCart == null) return;

            dgvCart.Rows.Clear();
            foreach (var it in CartService.GetItems())
            {
                dgvCart.Rows.Add(it.Name, $"NT${it.UnitPrice:N0}", it.Quantity, $"NT${it.Subtotal:N0}");
            }

            var subtotal = CartService.GetSubtotal();
            bool isVIP = (cmbMemberType != null && cmbMemberType.SelectedIndex == 1);
            var totalAfter = CartService.GetTotalAfterDiscount(isVIP);
            var discount = CartService.GetDiscountAmount(isVIP);

            lblSubtotal.Text = $"小計: NT${subtotal:N0}";
            lblDiscount.Text = $"折扣: NT${discount:N0}  ({(isVIP ? "VIP 85折" : "一般 95折")})";
            lblTotal.Text = $"實付: NT${totalAfter:N0}";
        }

        private void BtnClearCart_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("確定要清空購物車嗎？", "清空購物車", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                CartService.Clear();
                RefreshCartGrid();
                RefreshCartSummaryList();
            }
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (CartService.GetItems().Count == 0)
            {
                MessageBox.Show("購物車為空");
                return;
            }

            bool isVIP = (cmbMemberType != null && cmbMemberType.SelectedIndex == 1);
            var subtotal = CartService.GetSubtotal();
            var discount = CartService.GetDiscountAmount(isVIP);
            var total = CartService.GetTotalAfterDiscount(isVIP);

            var msg = $"小計: NT${subtotal:N0}\n折扣: NT${discount:N0}\n應付: NT${total:N0}\n\n確定要結帳？";
            var r = MessageBox.Show(msg, "結帳確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                MessageBox.Show(
            "🎉 結帳完成！\n\n感謝您蒞臨 L'Étoile 法式餐廳\n祝您有美好的一天，期待再次為您服務！",
            "感謝惠顧",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
                CartService.Clear();
                RefreshCartGrid();
                RefreshCartSummaryList();
            }
        }

        // 更新 panelRecommendation 的購物車摘要清單
        private void RefreshCartSummaryList()
        {
            if (listBoxCartSummary == null) return;
            listBoxCartSummary.Items.Clear();

            var items = CartService.GetItems();
            if (items.Count == 0)
            {
                listBoxCartSummary.Items.Add("(購物車空)");
                return;
            }

            foreach (var it in items)
            {
                listBoxCartSummary.Items.Add($"{it.Name} x{it.Quantity} NT${it.Subtotal:N0}");
            }
        }
        private void DisplayDishDetails(MenuItem item)
        {
            labelPrice.Text = $"價格: NT${item.Price}";
            textBoxDescription.Text = item.Description;

            var imageMap = new Dictionary<string, string>
    {
        { "馬卡龍",     "1.jpg" },
        { "焦糖布蕾",   "2.jpg" },
        { "法式羊排",   "3.jpg" },
        { "香煎鴨胸",   "4.jpg" },
        { "紅酒燉牛肉", "5.jpg" },
        { "香煎干貝",   "6.jpg" },
        { "法式洋蔥湯", "7.jpg" }
    };

            if (imageMap.TryGetValue(item.Name, out var fileName))
            {
                var path = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, fileName);
                if (System.IO.File.Exists(path))
                {
                    pictureBoxDish.Image = Image.FromFile(path);
                    pictureBoxDish.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pictureBoxDish.Image = null;
                    pictureBoxDish.BackColor = Color.LightGray;
                }
            }
            else
            {
                pictureBoxDish.Image = null;
                pictureBoxDish.BackColor = Color.LightGray;
            }

            listBoxRecommendation.Items.Clear();
            if (recommendations.TryGetValue(item.Name, out var recs))
                foreach (var rec in recs)
                    listBoxRecommendation.Items.Add(rec);
        }

        private void BtnHotSelling_Click(object sender, EventArgs e)
        {
            SwitchPanel(2);
        }
   
    }
}
