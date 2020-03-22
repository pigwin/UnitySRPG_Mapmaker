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
        bool Hani = false;
        int start_x, start_y;
        int finish_x, finish_y;
        public Form2(string s)
        {
            InitializeComponent();
            if (s.Equals("hani"))
            {
                start_x = Form1.start_x;
                start_y = Form1.start_y;
                finish_x = Form1.finish_x;
                finish_y = Form1.finish_y;
                Form1.start_x = -1;
                Form1.start_y = -1;
                Form1.finish_x = -1;
                Form1.finish_y = -1;
                Hani = true;
                return;
            }
            string[] point = s.Split(',');
            x = int.Parse(point[1]);
            y = int.Parse(point[0]);
            if (x != -1 && y != -1)
            {
                numericUpDown1.Value = Form1.matrix[y, x].high;
                numericUpDown2.Value = Form1.matrix[y, x].texture;
                numericUpDown3.Value = Form1.matrix[y, x].unit;
                if(Form1.matrix[y,x].force == 1)
                {
                    checkBox3.Checked = true;
                }
                else
                {
                    checkBox3.Checked = false;
                }
                if(Form1.matrix[y,x].putable == 1)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                if(Form1.matrix[y,x].partymember == 1)
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int temp_high = int.Parse(numericUpDown1.Value.ToString());
            int temp_texture = int.Parse(numericUpDown2.Value.ToString());
            int temp_unit = int.Parse(numericUpDown3.Value.ToString());
            int temp_direction = 0;
            if (radioButton1.Checked)
            {
                temp_direction = 1;
            }
            else if (radioButton2.Checked)
            {
                temp_direction = 2;
            }
            else if (radioButton3.Checked)
            {
                temp_direction = 3;
            }
            else if (radioButton4.Checked)
            {
                temp_direction = 4;
            }
            else if (radioButton5.Checked)
            {
                temp_direction = 0;
            }
            int temp_putable = 0;
            if (checkBox1.Checked)
            {
                temp_putable = 1;
            }
            int temp_partymember = 0;
            if (checkBox2.Checked)
            {
                temp_partymember = 1;
            }
            int temp_force = 0;
            if (checkBox3.Checked)
            {
                temp_force = 1;
            }
            if (Hani)
            {
                
                for(int i=start_x;i<= finish_x; i++)
                {
                    for(int j = start_y; j <= finish_y; j++)
                    {
                        Form1.matrix[i, j].high = temp_high;
                        Form1.matrix[i, j].texture = temp_texture;
                        Form1.matrix[i, j].unit = temp_unit;
                        Form1.matrix[i, j].direction = temp_direction;
                        Form1.matrix[i, j].putable = temp_putable;
                        Form1.matrix[i, j].partymember = temp_partymember;
                        Form1.matrix[i, j].force = temp_force;

                        Form1.array[i, j].Text = string.Format("H{0}\nU{1}", Form1.matrix[i, j].high, Form1.matrix[i, j].unit);
                        Form1.ColoredButton(i, j);
                    }
                }
                this.Close();
                return;
            }
            if (x != -1 && y != -1)
            {
                Form1.matrix[y, x].high = temp_high;
                Form1.matrix[y, x].texture = temp_texture;
                Form1.matrix[y, x].unit = temp_unit;
                Form1.matrix[y, x].direction = temp_direction;
                Form1.matrix[y, x].putable = temp_putable;
                Form1.matrix[y, x].partymember = temp_partymember;
                Form1.matrix[y, x].force = temp_force;
                Form1.array[y, x].Text = string.Format("H{0}\nU{1}", Form1.matrix[y, x].high, Form1.matrix[y, x].unit);
                Form1.ColoredButton(y, x);

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
                        Form1.matrix[i, y].unit = temp_unit;
                        Form1.matrix[i, y].direction = temp_direction;
                        Form1.matrix[i, y].putable = temp_putable;
                        Form1.matrix[i, y].partymember = temp_partymember;
                        Form1.matrix[i, y].force = temp_force;
                        Form1.array[i, y].Text = string.Format("H{0}\nU{1}", Form1.matrix[i, y].high, Form1.matrix[i, y].unit);
                        Form1.ColoredButton(i, y);
                    }
                }
                else
                {
                    length = Form1.matrix.GetLength(1);
                    for (int i = 0; i < length; i++)
                    {
                        Form1.matrix[x, i].high = temp_high;
                        Form1.matrix[x, i].texture = temp_texture;
                        Form1.matrix[x, i].unit = temp_unit;
                        Form1.matrix[x, i].direction = temp_direction;
                        Form1.matrix[x, i].putable = temp_putable;
                        Form1.matrix[x, i].partymember = temp_partymember;
                        Form1.matrix[x, i].force = temp_force;
                        Form1.array[x, i].Text = string.Format("H{0}\nU{1}", Form1.matrix[x, i].high, Form1.matrix[x, i].unit);
                        Form1.ColoredButton(x,i);
                    }
                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = start_x; i <= finish_x; i++)
            {
                for (int j = start_y; j <= finish_y; j++)
                {
                    Form1.array[i, j].BackColor = Color.White;
                }
            }
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (x > 0 && y > 0)
            {
                numericUpDown1.Value = Form1.matrix[y, x].high;
                numericUpDown2.Value = Form1.matrix[y, x].texture;
                numericUpDown3.Value = Form1.matrix[y, x].unit;
                switch (Form1.matrix[y, x].direction)
                {
                    case 0:
                        radioButton5.Checked = true;
                        break;
                    case 1:
                        radioButton1.Checked = true;
                        break;
                    case 2:
                        radioButton2.Checked = true;
                        break;
                    case 3:
                        radioButton3.Checked = true;
                        break;
                    case 4:
                        radioButton4.Checked = true;
                        break;
                }
                if(Form1.matrix[y,x].putable == 1)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                if(Form1.matrix[y,x].partymember == 1)
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
                if (Form1.matrix[y, x].force == 1)
                {
                    checkBox3.Checked = true;
                }
                else
                {
                    checkBox3.Checked = false;
                }
            }

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                for (int i = start_x; i <= finish_x; i++)
                {
                    for (int j = start_y; j <= finish_y; j++)
                    {
                        Form1.array[i, j].BackColor = Color.White;
                    }
                }
            }

        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
