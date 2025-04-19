using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp1.Program;
using System.Windows.Forms.DataVisualization.Charting;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Changhua changhua;
        private Yunlin yunlin;
        public Form1(Data_count[] changhua_month_G42, Data_count[] changhua_month_G21, Data_count[] changhua_month_G41, Data_count[] changhua_month_G32,
                     Data_count[] yunlin_month_G42, Data_count[] yunlin_month_G21, Data_count[] yunlin_month_G41, Data_count[] yunlin_month_G32,
                     Data_cal changhua_num, Data_cal yunlin_num, Data_cal changhua_price, Data_cal yunlin_price)
        {
            InitializeComponent();
            changhua = new Changhua(changhua_month_G42, changhua_month_G21, changhua_month_G41, changhua_month_G32, changhua_num, changhua_price);
            yunlin = new Yunlin(yunlin_month_G42, yunlin_month_G21, yunlin_month_G41, yunlin_month_G32, yunlin_num, yunlin_price);
            this.panel1.Controls.Add(changhua);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(changhua);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(yunlin);
        }
    }
}
