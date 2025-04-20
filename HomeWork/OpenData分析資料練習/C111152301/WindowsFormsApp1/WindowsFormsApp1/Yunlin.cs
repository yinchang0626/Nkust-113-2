using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Yunlin : UserControl
    {

        private Data_count[] yunlin_month_G42;
        private Data_count[] yunlin_month_G21;
        private Data_count[] yunlin_month_G41;
        private Data_count[] yunlin_month_G32;
        private Data_cal yunlin_num;
        private Data_cal yunlin_price;
        public Yunlin(Data_count[] input_yunlin_month_G42, Data_count[] input_yunlin_month_G21, Data_count[] input_yunlin_month_G41, Data_count[] input_yunlin_month_G32,
                        Data_cal input_yunlin_num, Data_cal input_yunlin_price)
        {
            InitializeComponent();
            yunlin_month_G42 = input_yunlin_month_G42;
            yunlin_month_G21 = input_yunlin_month_G21;
            yunlin_month_G41 = input_yunlin_month_G41;
            yunlin_month_G32 = input_yunlin_month_G32;
            yunlin_num = input_yunlin_num;
            yunlin_price = input_yunlin_price;
            this.chart1.Width = 500;
            chart1.ChartAreas[0].AxisX.Interval = 7;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            chart1.ChartAreas[0].AxisX.Title = "月份";
            chart1.ChartAreas[0].AxisY.Title = "羊隻數量";
            chart1.Legends[0].Docking = Docking.Bottom;

            this.chart2.Width = 500;
            chart2.ChartAreas[0].AxisX.Interval = 7;
            chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -90;
            chart2.ChartAreas[0].AxisX.Title = "月份";
            chart2.ChartAreas[0].AxisY.Title = "羊隻售賣價";
            chart2.Legends[0].Docking = Docking.Bottom;
        }
        private void Yunlin_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 120; i++)
            {
                string xLabel = yunlin_month_G21[i].year + "/" + yunlin_month_G21[i].month;
                chart1.Series[0].Points.AddXY(xLabel, yunlin_month_G42[i].avenum);
                chart1.Series[1].Points.AddXY(xLabel, yunlin_month_G21[i].avenum);
                chart1.Series[2].Points.AddXY(xLabel, yunlin_month_G41[i].avenum);
                chart1.Series[3].Points.AddXY(xLabel, yunlin_month_G32[i].avenum);

                chart2.Series[0].Points.AddXY(xLabel, yunlin_month_G42[i].avenum);
                chart2.Series[1].Points.AddXY(xLabel, yunlin_month_G21[i].avenum);
                chart2.Series[2].Points.AddXY(xLabel, yunlin_month_G41[i].avenum);
                chart2.Series[3].Points.AddXY(xLabel, yunlin_month_G32[i].avenum);
            }
            label1.Text = $"閹公羊10年平均售賣: {yunlin_num.average_G42}隻, 最大值: {yunlin_num.G42_max_year}/{yunlin_num.G42_max_month} {yunlin_num.G42_max}隻, 最小值: {yunlin_num.G42_min_year}/{yunlin_num.G21_min_month} {yunlin_num.G42_min}隻\n" +
                          $"女羊10年平均售賣: {yunlin_num.average_G21}隻, 最大值: {yunlin_num.G21_max_year}/{yunlin_num.G21_max_month} {yunlin_num.G21_max}隻, 最小值: {yunlin_num.G21_min_year}/{yunlin_num.G21_min_month}  {yunlin_num.G21_min}隻\n" +
                          $"努比亞雜交羊10年平均售賣: {yunlin_num.average_G41}隻, 最大值: {yunlin_num.G41_max_year}/{yunlin_num.G41_max_month} {yunlin_num.G41_max}隻, 最小值: {yunlin_num.G41_min_year}/{yunlin_num.G41_min_month} {yunlin_num.G41_min}隻\n" +
                          $"規格外羊10年平均售賣: {yunlin_num.average_G32}隻, 最大值: {yunlin_num.G32_max_year}/{yunlin_num.G32_max_month} {yunlin_num.G32_max}隻, 最小值: {yunlin_num.G32_min_year}/{yunlin_num.G32_min_month} {yunlin_num.G32_min}隻\n";
            label2.Text = $"閹公羊10年平均售賣: {yunlin_price.average_G42}元, 最大值: {yunlin_price.G42_max_year}/{yunlin_price.G42_max_month} {yunlin_price.G42_max}元, 最小值: {yunlin_price.G32_min_year}/{yunlin_price.G32_min_month} {yunlin_price.G32_min}元\n" +
                          $"女羊10年平均售賣: {yunlin_price.average_G21}元, 最大值: {yunlin_price.G21_max_year}/{yunlin_price.G42_max_month} {yunlin_price.G42_max}元, 最小值: {yunlin_price.G32_min_year}/{yunlin_price.G32_min_month} {yunlin_price.G32_min}元\n" +
                          $"努比亞雜交羊10年平均售賣: {yunlin_price.average_G41}元, 最大值: {yunlin_price.G41_max_year}/{yunlin_price.G42_max_month} {yunlin_price.G42_max}元, 最小值: {yunlin_price.G32_min_year}/{yunlin_price.G32_min_month} {yunlin_price.G32_min}元\n" +
                          $"規格外羊10年平均售賣: {yunlin_price.average_G32}元, 最大值: {yunlin_price.G42_max_year}/{yunlin_price.G42_max_month} {yunlin_price.G42_max}元, 最小值: {yunlin_price.G32_min_year}/{yunlin_price.G32_min_month} {yunlin_price.G32_min}元\n";
        }
    }
}
