using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp4
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private Panel panelCenter;
        private Panel panelLeft;
       

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Form
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Text = "L'Étoile Restaurant";
            this.Font = new Font("微軟正黑體", 9F);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 240, 224);

            // Colors used
            var darkBlue = Color.FromArgb(28, 42, 74);
            var gold = Color.FromArgb(240, 192, 64);

            // ─────────────────────────────
            // 左側導覽列 panelNav
            // ─────────────────────────────
            this.panelNav = new Panel();
            this.panelNav.Dock = DockStyle.Left;
            this.panelNav.Width = 180;
            this.panelNav.BackColor = darkBlue;
            this.panelNav.Padding = new Padding(0);

            // nav item - Home
            this.panelNavItemHome = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = darkBlue };
            this.panelIndicatorHome = new Panel { Dock = DockStyle.Left, Width = 4, BackColor = gold, Visible = false };
            this.btnNavHome = new Button
            {
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Text = "🏠 會員登入",
                ForeColor = Color.White,
                BackColor = darkBlue,
                Font = new Font("微軟正黑體", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            this.btnNavHome.FlatAppearance.BorderSize = 0;
            this.btnNavHome.Click += new EventHandler(this.BtnNavHome_Click);
            this.panelNavItemHome.Controls.Add(this.btnNavHome);
            this.panelNavItemHome.Controls.Add(this.panelIndicatorHome);

            // nav item - Menu
            this.panelNavItemMenu = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = darkBlue };
            this.panelIndicatorMenu = new Panel { Dock = DockStyle.Left, Width = 4, BackColor = gold, Visible = false };
            this.btnNavMenu = new Button
            {
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Text = "🍽 餐點選購",
                ForeColor = Color.White,
                BackColor = darkBlue,
                Font = new Font("微軟正黑體", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            this.btnNavMenu.FlatAppearance.BorderSize = 0;
            this.btnNavMenu.Click += new EventHandler(this.BtnNavMenu_Click);
            this.panelNavItemMenu.Controls.Add(this.btnNavMenu);
            this.panelNavItemMenu.Controls.Add(this.panelIndicatorMenu);

            // nav item - HotSelling
            this.panelNavItemHot = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = darkBlue };
            this.panelIndicatorHot = new Panel { Dock = DockStyle.Left, Width = 4, BackColor = gold, Visible = false };
            this.btnNavHot = new Button
            {
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Text = "📊 熱銷分析",
                ForeColor = Color.White,
                BackColor = darkBlue,
                Font = new Font("微軟正黑體", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            this.btnNavHot.FlatAppearance.BorderSize = 0;
            this.btnNavHot.Click += new EventHandler(this.BtnNavHot_Click);
            this.panelNavItemHot.Controls.Add(this.btnNavHot);
            this.panelNavItemHot.Controls.Add(this.panelIndicatorHot);

            // nav item - Cart
            this.panelNavItemCart = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = darkBlue };
            this.panelIndicatorCart = new Panel { Dock = DockStyle.Left, Width = 4, BackColor = gold, Visible = false };
            this.btnNavCart = new Button
            {
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Text = "🛒 購物車結帳",
                ForeColor = Color.White,
                BackColor = darkBlue,
                Font = new Font("微軟正黑體", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(16, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            this.btnNavCart.FlatAppearance.BorderSize = 0;
            this.btnNavCart.Click += new EventHandler(this.BtnNavCart_Click);
            this.panelNavItemCart.Controls.Add(this.btnNavCart);
            this.panelNavItemCart.Controls.Add(this.panelIndicatorCart);

            // Assemble nav (top to bottom)
            this.panelNav.Controls.Add(this.panelNavItemCart);
            this.panelNav.Controls.Add(this.panelNavItemHot);
            this.panelNav.Controls.Add(this.panelNavItemMenu);
            this.panelNav.Controls.Add(this.panelNavItemHome);

            // ─────────────────────────────
            // 內容區 panelContent（右側）
            // ─────────────────────────────
            this.panelContent = new Panel();
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.FromArgb(245, 240, 224);

            // panelLogin
            this.panelLogin = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 240, 224) };

            // panelMenu (餐點選購)
            this.panelMenu = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 240, 224) };

            // panelHotSelling
            this.panelHotSelling = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(234, 230, 216) };

            // panelCart
            this.panelCart = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 240, 224) };

            // ─────────────────────────────
            // 建構 panelMenu 的三欄（left/center/right）
            // ─────────────────────────────

            // left column (categories + embedded pie chart)
            this.panelLeft = new Panel { Dock = DockStyle.Left, Width = 340, BackColor = Color.FromArgb(245, 240, 224) }; // Width 340
            this.flowCategories = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, Dock = DockStyle.Top, AutoSize = true, WrapContents = false, Padding = new Padding(8) };

            var btnFont = new Font("微軟正黑體", 13F, FontStyle.Bold);
            this.btnAll = new Button { Text = "全部", Width = 124, Height = 36, BackColor = Color.FromArgb(31, 48, 94), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = btnFont, Cursor = Cursors.Hand };
            this.btnAll.FlatAppearance.BorderSize = 0;
            this.btnAppetizer = new Button { Text = "前菜", Width = 124, Height = 36, BackColor = Color.FromArgb(31, 48, 94), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = btnFont, Cursor = Cursors.Hand };
            this.btnAppetizer.FlatAppearance.BorderSize = 0;
            this.btnMainCourse = new Button { Text = "主餐", Width = 124, Height = 36, BackColor = Color.FromArgb(31, 48, 94), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = btnFont, Cursor = Cursors.Hand };
            this.btnMainCourse.FlatAppearance.BorderSize = 0;
            this.btnDessert = new Button { Text = "甜點", Width = 124, Height = 36, BackColor = Color.FromArgb(31, 48, 94), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = btnFont, Cursor = Cursors.Hand };
            this.btnDessert.FlatAppearance.BorderSize = 0;
            this.btnBeverage = new Button { Text = "飲品", Width = 124, Height = 36, BackColor = Color.FromArgb(31, 48, 94), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = btnFont, Cursor = Cursors.Hand };
            this.btnBeverage.FlatAppearance.BorderSize = 0;
            this.btnAll.Click += new EventHandler(this.BtnCategory_Click);
            this.btnAppetizer.Click += new EventHandler(this.BtnCategory_Click);
            this.btnMainCourse.Click += new EventHandler(this.BtnCategory_Click);
            this.btnDessert.Click += new EventHandler(this.BtnCategory_Click);
            this.btnBeverage.Click += new EventHandler(this.BtnCategory_Click);
            this.flowCategories.Controls.AddRange(new Control[] { this.btnAll, this.btnAppetizer, this.btnMainCourse, this.btnDessert, this.btnBeverage });
            

            // 餐點清單（高度縮小為 180px）
            this.listBoxMenuItems = new ListBox { Dock = DockStyle.Top, Height = 180, Font = new Font("微軟正黑體", 13F) };
            this.listBoxMenuItems.SelectedIndexChanged += new EventHandler(this.ListBoxMenuItems_SelectedIndexChanged);

            // 熱銷排行按鈕（放在餐點清單下方）
            this.btnHotSelling = new Button { Text = "熱銷排行", Height = 44, Dock = DockStyle.Top, BackColor = Color.FromArgb(31, 48, 94), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = btnFont, Cursor = Cursors.Hand };
            this.btnHotSelling.FlatAppearance.BorderSize = 0;
            this.btnHotSelling.Click += new EventHandler(this.BtnHotSelling_Click);

            // 圓餅圖（高度 320px，DockStyle.Top）
            this.chartHotSelling = new Chart
            {
                Dock = DockStyle.Top,
                Height = 320,
                BackColor = Color.FromArgb(245, 240, 224)
            };

            // 將左側控制項依照上->下順序加入：分類按鈕 -> 餐點清單(180px) -> 熱銷按鈕 -> 圓餅圖(320px)
            this.panelLeft.Controls.Add(this.flowCategories);
            this.panelLeft.Controls.Add(this.listBoxMenuItems);
            this.panelLeft.Controls.Add(this.btnHotSelling);
            this.panelLeft.Controls.Add(this.chartHotSelling);

            // center column
            this.panelCenter = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12), BackColor = Color.FromArgb(245, 240, 224) };
            // 底部區域 Panel
            this.panelBottom = new Panel { Dock = DockStyle.Top, Height = 160, BackColor = Color.FromArgb(245, 240, 224) };
            // 按鈕列
            this.panelButtons = new Panel { Dock = DockStyle.Top, Height = 50 };
            this.btnAddToCart = new Button
            {
                Text = "🛒 加入購物車",
                Dock = DockStyle.Left,
                Width = 200,
                BackColor = Color.FromArgb(28, 42, 74),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微軟正黑體", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            this.btnAddToCart.FlatAppearance.BorderSize = 0;
            this.btnAddToCart.Click += new EventHandler(this.BtnAddToCart_Click);

            this.btnAddToBundle = new Button
            {
                Text = "✦ 加入推薦套餐",
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 192, 64),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微軟正黑體", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            this.btnAddToBundle.FlatAppearance.BorderSize = 0;
            this.btnAddToBundle.Click += new EventHandler(this.BtnAddToBundle_Click);
            this.panelButtons.Controls.Add(this.btnAddToBundle);
            this.panelButtons.Controls.Add(this.btnAddToCart);

            // 購物車摘要 ListBox
            this.lblCartSummaryTitle = new Label
            {
                Text = "🛒 目前已點：",
                Dock = DockStyle.Top,
                Height = 24,
                Font = new Font("微軟正黑體", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 42, 74)
            };
            this.listBoxCartSummary = new ListBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(255, 255, 220),
                Font = new Font("微軟正黑體", 10F),
                BorderStyle = BorderStyle.None
            };

            this.panelBottom.Controls.Add(this.listBoxCartSummary);
            this.panelBottom.Controls.Add(this.lblCartSummaryTitle);
            this.panelBottom.Controls.Add(this.panelButtons);
            this.panelCenter.Controls.Add(this.panelBottom);
            this.pictureBoxDish = new PictureBox { Dock = DockStyle.Top, Height = 180, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.LightGray, BorderStyle = BorderStyle.FixedSingle }; // Height 180
            this.labelPrice = new Label { Text = "價格: NT$0", Dock = DockStyle.Top, Height = 36, Font = new Font("微軟正黑體", 13F, FontStyle.Bold), ForeColor = Color.FromArgb(31, 48, 94) };
            this.textBoxDescription = new TextBox { Dock = DockStyle.Top, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, Height = 120, Font = new Font("微軟正黑體", 11F), BackColor = Color.White };
            this.labelRecommendation = new Label { Text = "推薦原因:", Dock = DockStyle.Top, Height = 28, Font = new Font("微軟正黑體", 12F, FontStyle.Bold) };
            this.listBoxRecommendation = new ListBox { Dock = DockStyle.Fill, Font = new Font("微軟正黑體", 11F), BackColor = Color.FromArgb(230, 245, 255) };

            // right column (kept created but NOT added to panelMenu to effectively remove right column)
            this.panelRight = new Panel { Dock = DockStyle.Right, Width = 220, Padding = new Padding(10), BackColor = Color.FromArgb(245, 240, 224) };
            this.labelCartTitle = new Label { Text = "🛒 購物車", Dock = DockStyle.Top, Height = 28, Font = new Font("微軟正黑體", 12F, FontStyle.Bold) };
            this.listBoxCart = new ListBox { Dock = DockStyle.Top, Height = 300, Font = new Font("微軟正黑體", 11F), BackColor = Color.FromArgb(255, 250, 240) };
            this.labelCartSubtotal = new Label { Text = "小計: NT$0", Dock = DockStyle.Top, Height = 28, Font = new Font("微軟正黑體", 11F, FontStyle.Bold) };
            this.btnCheckout = new Button { Text = "結帳", Height = 40, Dock = DockStyle.Bottom, BackColor = darkBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = btnFont, Cursor = Cursors.Hand };
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.Click += new EventHandler(this.BtnCheckout_Click);

            this.panelRight.Controls.Add(this.btnCheckout);
            this.panelRight.Controls.Add(this.labelCartSubtotal);
            this.panelRight.Controls.Add(this.listBoxCart);
            this.panelRight.Controls.Add(this.labelCartTitle);

            // assemble center
            this.panelCenter.Controls.Add(this.listBoxRecommendation);
            this.panelCenter.Controls.Add(this.labelRecommendation);
            this.panelCenter.Controls.Add(this.textBoxDescription);
            this.panelCenter.Controls.Add(this.labelPrice);
            this.panelCenter.Controls.Add(this.pictureBoxDish);

            // assemble panelMenu
            this.panelMenu.Controls.Add(this.panelCenter);
            this.panelMenu.Controls.Add(this.panelLeft);
            // note: panelRight intentionally NOT added to panelMenu -> right column removed from visual layout

            // add content panels to panelContent (stacked, visibility controlled in code)
            this.panelContent.Controls.Add(this.panelCart);
            this.panelContent.Controls.Add(this.panelHotSelling);
            this.panelContent.Controls.Add(this.panelMenu);
            this.panelContent.Controls.Add(this.panelLogin);

            // final assemble form
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelNav);
        }

        #endregion

        // fields (controls)
        private Panel panelNav;
        private Panel panelNavItemHome;
        private Panel panelIndicatorHome;
        private Button btnNavHome;
        private Panel panelNavItemMenu;
        private Panel panelIndicatorMenu;
        private Button btnNavMenu;
        private Panel panelNavItemHot;
        private Panel panelIndicatorHot;
        private Button btnNavHot;
        private Panel panelNavItemCart;
        private Panel panelIndicatorCart;
        private Button btnNavCart;

        private Panel panelContent;
        public Panel panelLogin;
        public Panel panelMenu;
        public Panel panelHotSelling;
        public Panel panelCart;

        // menu page controls

        private FlowLayoutPanel flowCategories;
        private Button btnAll;
        private Button btnAppetizer;
        private Button btnMainCourse;
        private Button btnDessert;
        private Button btnBeverage;
        private ListBox listBoxMenuItems;
        private Button btnHotSelling;
        private Chart chartHotSelling;
        private Panel panelBottom;
        private Panel panelButtons;
        private Button btnAddToCart;
        private Button btnAddToBundle;
        private Label lblCartSummaryTitle;
        private ListBox listBoxCartSummary;
        private PictureBox pictureBoxDish;
        private Label labelPrice;
        private TextBox textBoxDescription;
        private Label labelRecommendation;
        private ListBox listBoxRecommendation;

        private Panel panelRight;
        private Label labelCartTitle;
        private ListBox listBoxCart;
        private Label labelCartSubtotal;
  

    }
}