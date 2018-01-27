using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace NAVmobProgress2
{
    public partial class Form1 : Form
    {
        string file = "progressstatus.txt";
        int pvalue = 0;

        public Form1()
        {
            InitializeComponent();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 1;
            label1.Text = "";
            label3.Text = "";
            timer1.Enabled = true;


        }



        private void Form1_Load(object sender, EventArgs e)
        {
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            var lines = File.ReadAllLines(file);

            foreach (string line in lines)
            {
                var line2 = line.Split(';').ToArray();
                if (!(pvalue == Int32.Parse(line2[0])))
                {
                    pvalue = Int32.Parse(line2[0]);
                    progressBar1.Value = pvalue;
                    label3.Text = line2[0] + " %";
                    label1.Text = line2[1];
                }

            }

            if (pvalue == 100)
            {
                Application.Exit();
            }


            bool connection = NetworkInterface.GetIsNetworkAvailable();
            if (connection == true)
            {
                button1.BackColor = Color.Green;
            }
            else
            {
                button1.BackColor = Color.Red;
            }


            var battery = BatteryChargeStatus.Charging;
            if(battery.ToString() == "Charging")
            {
                button2.BackColor = Color.Green;
            }
            else
            {
                button2.BackColor = Color.Red;
            }


        }


    }
}
