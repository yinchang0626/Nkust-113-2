using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static 政府公開資料分析.Program;
using System.Windows.Forms.DataVisualization.Charting;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Changhua changhua;
        private Yunlin yunlin;
        public Form1()
        {
            InitializeComponent();
            changhua = new Changhua(input_data, input_all, input_male, input_female);
            yunlin = new Yunlin(input_data, input_all, input_male, input_female);
            this.panel1.Controls.Add(changhua);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
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
