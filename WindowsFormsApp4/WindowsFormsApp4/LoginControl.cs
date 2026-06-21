using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public class LoginControl : UserControl
    {
        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        private DoubleBufferedPanel heroPanel;
        private Panel cardPanel;
        private Label lblTitle;
        private Label lblSubTitle;
        private Label lblPhone;
        private TextBox txtPhone;
        private Button btnLogin;
        private Label lblSeparator;
        private Button btnRegister;
        private Label lblMessage;
        private Timer switchTimer;
        private Timer starTimer;

        private const int CardWidth = 900;
        private const int CardHeight = 500;
        private const int HeroWidth = 320;

        private static readonly Color Background = Color.FromArgb(255, 248, 220); // 左上淡金
        private static readonly Color BackgroundEnd = Color.FromArgb(245, 240, 224); // 右下米白
        private static readonly Color DarkBlue = Color.FromArgb(31, 48, 94);
        private static readonly Color Gold = Color.FromArgb(212, 175, 55);

        private struct Star { public float X, Y, Size; public int Alpha; }
        private Star[] stars;
        private Random rng = new Random();
        private bool starToggle = false;

        public LoginControl()
        {
            InitializeComponents();
            InitStars();

            switchTimer = new Timer { Interval = 1000 };
            switchTimer.Tick += (s, e) => { switchTimer.Stop();  };

            starTimer = new Timer { Interval = 500 };
            starTimer.Tick += (s, e) =>
            {
                starToggle = !starToggle;
                heroPanel.Invalidate(new Rectangle(0, (int)(CardHeight * 0.60f), HeroWidth, (int)(CardHeight * 0.40f)));
            };
            starTimer.Start();
        }

        private void InitStars()
        {
            stars = new Star[10];
            for (int i = 0; i < stars.Length; i++)
                stars[i] = new Star { X = rng.Next(20, HeroWidth - 20), Y = rng.Next((int)(CardHeight * 0.65f), CardHeight - 20), Size = rng.Next(2, 5), Alpha = 180 };
        }

        private void InitializeComponents()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Background;
            this.Paint += (s, e) => DrawGradientBackground(e);

            cardPanel = new Panel
            {
                Size = new Size(CardWidth, CardHeight),
                BackColor = Color.FromArgb(248, 248, 248),
                Location = new Point((this.Width - CardWidth) / 2, (this.Height - CardHeight) / 2),
                Anchor = AnchorStyles.None
            };
            cardPanel.HandleCreated += (s, e) => ApplyCardRegion();
            this.Resize += (s, e) => UpdateCardLayout();

            heroPanel = new DoubleBufferedPanel
            {
                Size = new Size(HeroWidth, CardHeight),
                Location = new Point(CardWidth - HeroWidth, 0),
                BackColor = Color.Transparent
            };
            heroPanel.Paint += HeroPanel_Paint;
            heroPanel.MouseEnter += (s, e) => heroPanel.Cursor = MakeStarCursor();
            heroPanel.MouseLeave += (s, e) => heroPanel.Cursor = Cursors.Default;

            int formW = CardWidth - HeroWidth - 60;

            lblTitle = new Label
            {
                Text = "L'Étoile",
                Font = new Font("微軟正黑體", 40F, FontStyle.Bold),
                ForeColor = DarkBlue,
                Location = new Point(40, 30),
                Size = new Size(formW, 70)
            };

            lblSubTitle = new Label
            {
                Text = "法式餐廳智慧點餐系統",
                Font = new Font("微軟正黑體", 12F),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(40, lblTitle.Bottom + 5),
                Size = new Size(formW, 28)
            };

            var divider = new Label
            {
                BackColor = Gold,
                Location = new Point(40, lblSubTitle.Bottom + 15),
                Size = new Size(60, 4)
            };

            lblPhone = new Label
            {
                Text = "📞 電話號碼",
                Font = new Font("微軟正黑體", 11F),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(40, divider.Bottom + 20),
                AutoSize = true
            };

            txtPhone = new TextBox
            {
                Font = new Font("微軟正黑體", 14F),
                ForeColor = Color.Gray,
                Text = "0912-345-678",
                Location = new Point(40, lblPhone.Bottom + 10),
                Size = new Size(formW, 50),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtPhone.GotFocus += (s, e) =>
            {
                if (txtPhone.Text == "0912-345-678") { txtPhone.Text = ""; txtPhone.ForeColor = Color.Black; }
            };
            txtPhone.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPhone.Text)) { txtPhone.Text = "0912-345-678"; txtPhone.ForeColor = Color.Gray; }
            };

            btnLogin = new Button
            {
                Text = "登　入",
                BackColor = DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(40, txtPhone.Bottom + 20),
                Size = new Size(formW, 56),
                Font = new Font("微軟正黑體", 14F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;
            btnLogin.MouseEnter += (s, e) => btnLogin.BackColor = Color.FromArgb(42, 63, 111);
            btnLogin.MouseLeave += (s, e) => btnLogin.BackColor = DarkBlue;
            ApplyButtonRegion(btnLogin);

            lblSeparator = new Label
            {
                Text = "─────── 或 ───────",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(160, 160, 160),
                Font = new Font("微軟正黑體", 10F),
                Location = new Point(40, btnLogin.Bottom + 15),
                Size = new Size(formW, 30)
            };

            btnRegister = new Button
            {
                Text = "新會員註冊",
                BackColor = Gold,
                ForeColor = Color.FromArgb(40, 30, 0),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(40, lblSeparator.Bottom + 12),
                Size = new Size(formW, 56),
                Font = new Font("微軟正黑體", 14F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;
            btnRegister.MouseEnter += (s, e) => btnRegister.BackColor = Color.FromArgb(255, 210, 80);
            btnRegister.MouseLeave += (s, e) => btnRegister.BackColor = Gold;
            ApplyButtonRegion(btnRegister);

            lblMessage = new Label
            {
                Text = "",
                ForeColor = Color.Green,
                Location = new Point(40, btnRegister.Bottom + 10),
                Size = new Size(formW, 30),
                Font = new Font("微軟正黑體", 11F)
            };

            cardPanel.Controls.Add(heroPanel);
            cardPanel.Controls.Add(lblTitle);
            cardPanel.Controls.Add(lblSubTitle);
            cardPanel.Controls.Add(divider);
            cardPanel.Controls.Add(lblPhone);
            cardPanel.Controls.Add(txtPhone);
            cardPanel.Controls.Add(btnLogin);
            cardPanel.Controls.Add(lblSeparator);
            cardPanel.Controls.Add(btnRegister);
            cardPanel.Controls.Add(lblMessage);

            this.Controls.Add(cardPanel);
        }

        private void DrawGradientBackground(PaintEventArgs e)
        {
            var rect = this.ClientRectangle;
            using (var br = new LinearGradientBrush(rect, Background, BackgroundEnd, LinearGradientMode.ForwardDiagonal))
                e.Graphics.FillRectangle(br, rect);
        }

        private void HeroPanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var rc = heroPanel.ClientRectangle;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var br = new LinearGradientBrush(rc, DarkBlue, Color.FromArgb(58, 84, 140), LinearGradientMode.Vertical))
                g.FillRectangle(br, rc);

            float cx = rc.Width / 2f;

            // 五角星
            DrawStar(g, cx, rc.Height * 0.12f, 50f, 22f, Gold);

            // L'Étoile
            using (var f = new Font("微軟正黑體", 18F, FontStyle.Bold))
            using (var br = new SolidBrush(Color.White))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString("L'Étoile", f, br, new RectangleF(0, rc.Height * 0.28f, rc.Width, 40), sf);

            // Fine French Dining
            using (var f = new Font("微軟正黑體", 10F, FontStyle.Italic))
            using (var br = new SolidBrush(Color.FromArgb(200, 220, 220)))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString("Fine French Dining", f, br, new RectangleF(0, rc.Height * 0.38f, rc.Width, 26), sf);

            // Paris Inspired Cuisine
            using (var f = new Font("微軟正黑體", 9F))
            using (var br = new SolidBrush(Color.FromArgb(170, 210, 245)))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString("Paris Inspired Cuisine", f, br, new RectangleF(0, rc.Height * 0.46f, rc.Width, 22), sf);

            // 五顆星
            using (var f = new Font("微軟正黑體", 18F, FontStyle.Bold))
            using (var br = new SolidBrush(Gold))
                g.DrawString("★★★★★", f, br, new RectangleF(0, rc.Height * 0.54f, rc.Width, 36), new StringFormat { Alignment = StringAlignment.Center });

            // Since 2025
            using (var f = new Font("微軟正黑體", 9F))
            using (var br = new SolidBrush(Color.FromArgb(130, 200, 230)))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString("Since 2025", f, br, new RectangleF(0, rc.Height * 0.68f, rc.Width, 20), sf);

            // 閃爍小星星
            foreach (var st in stars)
            {
                int alpha = starToggle ? st.Alpha : Math.Max(30, st.Alpha - 100);
                using (var br = new SolidBrush(Color.FromArgb(alpha, Gold)))
                    g.FillEllipse(br, st.X - st.Size / 2, st.Y - st.Size / 2, st.Size, st.Size);
            }

            // 底部金色條紋
            using (var br = new LinearGradientBrush(new Rectangle(0, rc.Height - 8, rc.Width, 8), Color.Transparent, Gold, LinearGradientMode.Horizontal))
                g.FillRectangle(br, 0, rc.Height - 8, rc.Width, 8);
        }

        private void DrawStar(Graphics g, float cx, float cy, float outer, float inner, Color color)
        {
            var pts = new PointF[10];
            for (int i = 0; i < 10; i++)
            {
                double angle = Math.PI / 5 * i - Math.PI / 2;
                float r = (i % 2 == 0) ? outer : inner;
                pts[i] = new PointF(cx + r * (float)Math.Cos(angle), cy + r * (float)Math.Sin(angle));
            }
            using (var br = new SolidBrush(color))
                g.FillPolygon(br, pts);
        }

        private Cursor MakeStarCursor()
        {
            var bmp = new Bitmap(32, 32);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var pts = new PointF[10];
                for (int i = 0; i < 10; i++)
                {
                    double angle = Math.PI / 5 * i - Math.PI / 2;
                    float r = (i % 2 == 0) ? 14f : 6f;
                    pts[i] = new PointF(16 + r * (float)Math.Cos(angle), 16 + r * (float)Math.Sin(angle));
                }
                using (var br = new SolidBrush(Gold))
                    g.FillPolygon(br, pts);
            }
            var icon = Icon.FromHandle(bmp.GetHicon());
            return new Cursor(icon.Handle);
        }

        private void ApplyCardRegion()
        {
            var path = new GraphicsPath();
            int r = 20;
            path.AddArc(0, 0, r, r, 180, 90);
            path.AddArc(cardPanel.Width - r, 0, r, r, 270, 90);
            path.AddArc(cardPanel.Width - r, cardPanel.Height - r, r, r, 0, 90);
            path.AddArc(0, cardPanel.Height - r, r, r, 90, 90);
            path.CloseAllFigures();
            cardPanel.Region = new Region(path);
        }

        private void ApplyButtonRegion(Button btn)
        {
            btn.HandleCreated += (s, e) =>
            {
                var rgn = CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 10, 10);
                btn.Region = Region.FromHrgn(rgn);
            };
        }

        private void UpdateCardLayout()
        {
            cardPanel.Location = new Point((this.Width - CardWidth) / 2, (this.Height - CardHeight) / 2);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var phone = txtPhone.Text?.Trim();
            if (string.IsNullOrWhiteSpace(phone) || phone == "0912-345-678")
            {
                lblMessage.Text = "請輸入電話號碼";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            var member = DatabaseHelper.GetMemberByPhone(phone);
            if (member != null)
            {
                lblMessage.Text = $"✓ 歡迎回來，{member.Name}！";
                lblMessage.ForeColor = Color.Green;
                switchTimer.Start();
            }
            else
            {
                lblMessage.Text = "✗ 會員不存在，請先註冊";
                lblMessage.ForeColor = Color.Red;
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            using (var form = new RegisterForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    lblMessage.Text = "✓ 註冊成功，請登入";
                    lblMessage.ForeColor = Color.Green;
                }
            }
        }
    
    }

    internal class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}