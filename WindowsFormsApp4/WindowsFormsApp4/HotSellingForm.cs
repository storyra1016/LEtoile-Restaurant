using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp4
{
    public class HotSellingForm : Form
    {
        private Chart pieChart;

        public HotSellingForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Form 基本設定
            this.Text = "熱銷排行榜";
            this.ClientSize = new Size(600, 500);
            this.BackColor = Color.FromArgb(245, 240, 224);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // 標題
            var lblTitle = new Label
            {
                Text = "熱銷排行榜",
                Font = new Font("微軟正黑體", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 42, 74),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50
            };

            // Chart
            pieChart = new Chart
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 240, 224)
            };

            // ChartArea
            var chartArea = new ChartArea("PieArea")
            {
                BackColor = Color.Transparent
            };
            // 禁用軸
            chartArea.AxisX.Enabled = AxisEnabled.False;
            chartArea.AxisY.Enabled = AxisEnabled.False;
            pieChart.ChartAreas.Add(chartArea);

            // Legend
            var legend = new Legend("Legend")
            {
                Docking = Docking.Right,
                BackColor = Color.Transparent,
                LegendStyle = LegendStyle.Table
            };
            pieChart.Legends.Add(legend);

            // Series - Pie
            var series = new Series("HotSelling")
            {
                ChartType = SeriesChartType.Pie,
                ChartArea = "PieArea",
                IsValueShownAsLabel = true,
                Font = new Font("微軟正黑體", 10F),
                LabelForeColor = Color.White
            };

            // 資料： (使用題目指定百分比)
            var data = new[]
            {
                new { Name = "紅酒燉牛肉", Value = 28.0 },
                new { Name = "紅酒",       Value = 18.0 },
                new { Name = "焦糖布蕾",   Value = 15.0 },
                new { Name = "法式羊排",   Value = 12.0 },
                new { Name = "香煎鴨胸",   Value = 10.0 },
                new { Name = "香煎干貝",   Value = 8.0 },
                new { Name = "法式洋蔥湯", Value = 5.0 },
                new { Name = "馬卡龍",     Value = 3.0 },
                new { Name = "氣泡水",     Value = 1.0 }
            };

            // 顏色陣列（可依喜好調整）
            var colors = new[]
            {
                Color.FromArgb(192, 0, 0),
                Color.FromArgb(153, 0, 51),
                Color.FromArgb(204, 102, 0),
                Color.FromArgb(153, 51, 0),
                Color.FromArgb(255, 153, 51),
                Color.FromArgb(255, 204, 102),
                Color.FromArgb(102, 153, 204),
                Color.FromArgb(153, 204, 153),
                Color.FromArgb(180, 180, 180)
            };

            for (int i = 0; i < data.Length; i++)
            {
                var pt = new DataPoint
                {
                    AxisLabel = data[i].Name,
                    YValues = new double[] { data[i].Value },
                    LegendText = data[i].Name,
                    Label = string.Format("{0} {1:P0}", data[i].Name, data[i].Value / 100.0),
                    Color = colors[i % colors.Length]
                };
                series.Points.Add(pt);
            }

            // 讓圖例顯示品項與百分比
            series["PieLabelStyle"] = "Outside";
            series["PieDrawingStyle"] = "SoftEdge";
            series["PieStartAngle"] = "270";
            pieChart.Series.Add(series);

            // 加入控制項
            this.Controls.Add(pieChart);
            this.Controls.Add(lblTitle);
        }
    }
}