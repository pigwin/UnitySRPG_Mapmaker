using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitySRPG_Mapmaker
{
    public partial class Form2 : Form
    {
        int x, y;
        public Form2(string s)
        {
            string[] point = s.Split(',');
            x = int.Parse(point[1]);
            y = int.Parse(point[0]);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int temp_high = int.Parse(numericUpDown1.Value.ToString());
            int temp_texture = int.Parse(numericUpDown2.Value.ToString());
            if (x != -1 && y != -1)
            {
                Form1.matrix[y, x].high = temp_high;
                Form1.matrix[y, x].texture = temp_texture;
            }
            else
            {
                int length = 0;
                if (x == -1)
                {
                    length = Form1.matrix.GetLength(0);
                    for(int i = 0; i < length; i++)
                    {
                        Form1.matrix[i, y].high = temp_high;
                        Form1.matrix[i, y].texture = temp_texture;
                    }
                }
                else
                {
                    length = Form1.matrix.GetLength(1);
                    for (int i = 0; i < length; i++)
                    {
                        Form1.matrix[x, i].high = temp_high;
                        Form1.matrix[x, i].texture = temp_texture;
                    }
                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
