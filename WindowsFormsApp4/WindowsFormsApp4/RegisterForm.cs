using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public class RegisterForm : Form
    {
        private TextBox txtName;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private CheckBox chkVIP;
        private Button btnConfirm;
        private Label lblMessage;

        public RegisterForm()
        {
            this.Text = "會員註冊";
            this.ClientSize = new Size(420, 380);
            this.BackColor = Color.FromArgb(245, 240, 224);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            var lblTitle = new Label
            {
                Text = "註冊新會員",
                Font = new Font("微軟正黑體", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 42, 74),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60
            };

            var lblName = new Label { Text = "姓名", Font = new Font("微軟正黑體", 11F), Location = new Point(30, 75), AutoSize = true };
            txtName = new TextBox { Font = new Font("微軟正黑體", 11F), Location = new Point(30, 100), Size = new Size(350, 30) };

            var lblPhone = new Label { Text = "電話", Font = new Font("微軟正黑體", 11F), Location = new Point(30, 140), AutoSize = true };
            txtPhone = new TextBox { Font = new Font("微軟正黑體", 11F), Location = new Point(30, 165), Size = new Size(350, 30) };

            var lblEmail = new Label { Text = "Email", Font = new Font("微軟正黑體", 11F), Location = new Point(30, 205), AutoSize = true };
            txtEmail = new TextBox { Font = new Font("微軟正黑體", 11F), Location = new Point(30, 230), Size = new Size(350, 30) };

            chkVIP = new CheckBox { Text = "VIP 會員 (享 15% 折扣)", Font = new Font("微軟正黑體", 10F), Location = new Point(30, 275), AutoSize = true };

            lblMessage = new Label
            {
                Text = "",
                ForeColor = Color.Red,
                Location = new Point(30, 305),
                Size = new Size(350, 24),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnConfirm = new Button
            {
                Text = "確認註冊",
                BackColor = Color.FromArgb(28, 42, 74),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(350, 40),
                Location = new Point(30, 330),
                Font = new Font("微軟正黑體", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += BtnConfirm_Click;

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, lblName, txtName,
                lblPhone, txtPhone,
                lblEmail, txtEmail,
                chkVIP, lblMessage, btnConfirm
            });
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            var name = txtName.Text?.Trim();
            var phone = txtPhone.Text?.Trim();
            var email = txtEmail.Text?.Trim();
            var isVIP = chkVIP.Checked;

            if (string.IsNullOrWhiteSpace(name)) { lblMessage.Text = "請輸入姓名"; return; }
            if (string.IsNullOrWhiteSpace(phone)) { lblMessage.Text = "請輸入電話"; return; }

            var member = new Member
            {
                Name = name,
                Phone = phone,
                Email = email,
                IsVIP = isVIP,
                DiscountRate = isVIP ? 0.15m : 0.05m
            };

            string err;
            if (DatabaseHelper.AddMember(member, out err))
            {
                MessageBox.Show("✓ 註冊成功！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblMessage.Text = err ?? "註冊失敗";
            }
        }
    }
}