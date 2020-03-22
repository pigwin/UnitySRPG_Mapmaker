﻿using System;
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
            if (x != -1 && y != -1)
            {
                numericUpDown1.Value = Form1.matrix[y, x].high;
                numericUpDown2.Value = Form1.matrix[y, x].texture;
                numericUpDown3.Value = Form1.matrix[y, x].unit;
                numericUpDown4.Value = Form1.matrix[y, x].direction;
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
            int temp_direction = int.Parse(numericUpDown4.Value.ToString());
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
            if (x != -1 && y != -1)
            {
                Form1.matrix[y, x].high = temp_high;
                Form1.matrix[y, x].texture = temp_texture;
                Form1.matrix[y, x].unit = temp_unit;
                Form1.matrix[y, x].direction = temp_direction;
                Form1.matrix[y, x].putable = temp_putable;
                Form1.matrix[y, x].partymember = temp_partymember;
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
                    }
                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (x > 0 && y > 0)
            {
                numericUpDown1.Value = Form1.matrix[y, x].high;
                numericUpDown2.Value = Form1.matrix[y, x].texture;
                numericUpDown3.Value = Form1.matrix[y, x].unit;
                numericUpDown4.Value = Form1.matrix[y, x].direction;
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
