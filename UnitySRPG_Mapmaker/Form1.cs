using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UnitySRPG_Mapmaker
{
    public struct Data
    {
        public int high;
        public int texture;
        public int unit;
        public int direction;
        public int putable;
    }
    public partial class Form1 : Form
    {
        const int space_x = 20;
        const int space_y = 20;
        const int Box_size = 40;
        int x = 0;
        int y = 0;
        public static Data[,] matrix;
        Button[,] array;
        Button[] Column;
        Button[] Row;
        string global_filename = "Empty";
        public Form1()
        {
            InitializeComponent();
            this.Text = global_filename;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int t = 0;
            if (!(int.TryParse(textBox1.Text,out t) && int.TryParse(textBox2.Text,out t)))
            {
                MessageBox.Show("入力してください");
                return;
            }
            x = int.Parse(textBox1.Text);
            y = int.Parse(textBox2.Text);
            if(array == null)
            {
                array = new Button[x,y];
                Row = new Button[x];
                Column = new Button[y];
            }
            else
            {
                for(int i = 0; i < Row.Length; i++)
                {
                    Row[i].Dispose();
                }
                for(int i = 0; i < Column.Length; i++)
                {
                    Column[i].Dispose();
                }
                for(int i = 0; i < array.GetLength(0); i++)
                {
                    for(int j = 0; j < array.GetLength(1); j++)
                    {
                        array[i, j].Dispose();
                    }
                }
                array = new Button[x,y];
                Row = new Button[x];
                Column = new Button[y];
            }
            matrix = new Data[x,y];
            for(int i = 0; i < y; i++)
            {
                Column[i] = new Button();
                Column[i].Text = string.Format("C{0}", i);
                Column[i].Size = new Size(Box_size, space_y);
                Column[i].Location = new Point(space_x + i * Box_size, 0);
                Column[i].Name = Column[i].Text;
                Column[i].Click += ClickFunc_Column;
                this.Controls.Add(Column[i]);
            }
            for (int i = 0; i < x; i++)
            {
                Row[i] = new Button();
                Row[i].Text = string.Format("R{0}", i);
                Row[i].Size = new Size(space_x,Box_size);
                Row[i].Location = new Point(0,space_y + i * Box_size);
                Row[i].Name = Row[i].Text;
                Row[i].Click += ClickFunc_Row;
                this.Controls.Add(Row[i]);
            }
            for (int i = 0;i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Button temp = new Button();
                    matrix[i, j] = new Data();
                    temp.Name = string.Format("{0},{1}",i, j);
                    temp.Size = new Size(Box_size, Box_size);
                    temp.Location = new Point(space_x + j * temp.Size.Height, space_y + i * temp.Size.Width);
                    temp.Text = string.Format("H{0}\nT{1}",1,0);
                    matrix[i, j].high = 1;
                    matrix[i, j].texture = 0;
                    temp.Click += ClickFunc;
                    temp.MouseEnter += MouseOverFunc;
                    this.Controls.Add(temp);
                    array[i, j]=(temp);
                }
            }
        }
        private void ClickFunc(Object sender,EventArgs e)
        {
            Form2 f = new Form2((sender as Button).Name);
            f.Show();
        }
        private void ClickFunc_Row(Object sender,EventArgs e)
        {
            string a = (sender as Button).Name;
            string[] b = a.Split('R');
            Form2 f = new Form2("-1," + b[1]);
            f.Show();
        }
        private void ClickFunc_Column(Object sender, EventArgs e)
        {
            string a = (sender as Button).Name;
            string[] b = a.Split('C');
            Form2 f = new Form2(b[1] + ",-1");
            f.Show();
        }
        private void MouseOverFunc(Object sender,EventArgs e)
        {
            string[] s = (sender as Button).Name.Split(',');
            int x = int.Parse(s[1]);
            int y = int.Parse(s[0]);
            label4.Text = (sender as Button).Name;
            label6.Text = string.Format("{0}",matrix[y, x].high);
            label8.Text = string.Format("{0}", matrix[y, x].texture);
            label9.Text = string.Format("{0}", matrix[y, x].unit);
            label11.Text = string.Format("{0}",matrix[y,x].direction);
            label13.Text = string.Format("{0}",matrix[y,x].putable);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = global_filename;
            if (matrix == null) return;
            int l_0 = matrix.GetLength(0);
            int l_1 = matrix.GetLength(1);
            for(int i = 0; i < l_0; i++)
            {
                for(int j = 0; j < l_1; j++)
                {
                    array[i, j].Text = string.Format("H{0}\nT{1}", matrix[i, j].high, matrix[i, j].texture);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "ファイルを保存する";
            saveFileDialog1.InitialDirectory = System.Environment.CurrentDirectory;
            saveFileDialog1.FileName = "新しいマップファイル.csv";
            saveFileDialog1.Filter = "csvファイル|*.csv";
            saveFileDialog1.FilterIndex = 1;
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                global_filename = filename;
                StreamWriter sw = new StreamWriter(filename);
                for(int i = 0; i < matrix.GetLength(0); i++)
                {
                    for(int j = 0; j < matrix.GetLength(1); j++)
                    {
                        sw.Write(matrix[i,j].high + matrix[i,j].texture * 100);
                        if(j!=matrix.GetLength(1)-1)sw.Write(",");
                    }
                    sw.WriteLine();
                }
                sw.Flush();
                sw.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "ファイルを開く";
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.Filter = "csvファイル|*.csv";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                global_filename = filename;
                StreamReader sr = new StreamReader(filename);
                string stream = sr.ReadToEnd();
                sr.Close();
                string[] line = stream.Split('\n');
                x = line.Length;
                y = line[0].Split(',').Length;
                if (line[0].Split(',')[y - 1] == "\r")
                {
                    y--;
                }
                if (line[x - 1].Length < 1)
                {
                    x--;
                }
                if (array == null)
                {
                    array = new Button[x,y];
                    Row = new Button[x];
                    Column = new Button[y];
                }
                else
                {
                    for (int i = 0; i < Row.Length; i++)
                    {
                        Row[i].Dispose();
                    }
                    for (int i = 0; i < Column.Length; i++)
                    {
                        Column[i].Dispose();
                    }
                    for (int i = 0; i < array.GetLength(0); i++)
                    {
                        for (int j = 0; j < array.GetLength(1); j++)
                        {
                            array[i, j].Dispose();
                        }
                    }
                    array = new Button[x,y];
                    Row = new Button[x];
                    Column = new Button[y];
                }
                matrix = new Data[x,y];
                for (int i = 0; i < y; i++)
                {
                    Column[i] = new Button();
                    Column[i].Text = string.Format("C{0}", i);
                    Column[i].Size = new Size(Box_size, space_y);
                    Column[i].Location = new Point(space_x + i * Box_size, 0);
                    Column[i].Name = Column[i].Text;
                    Column[i].Click += ClickFunc_Column;
                    this.Controls.Add(Column[i]);
                }
                for (int i = 0; i < x; i++)
                {
                    Row[i] = new Button();
                    Row[i].Text = string.Format("R{0}", i);
                    Row[i].Size = new Size(space_x, Box_size);
                    Row[i].Location = new Point(0, space_y + i * Box_size);
                    Row[i].Name = Row[i].Text;
                    Row[i].Click += ClickFunc_Row;
                    this.Controls.Add(Row[i]);
                }
                for (int i = 0; i < x; i++)
                {
                    string[] row_stream = line[i].Split(',');
                    for (int j = 0; j < y; j++)
                    {
                        int d = int.Parse(row_stream[j].Split('\r')[0]);
                        Button temp = new Button();
                        matrix[i, j] = new Data();
                        temp.Name = string.Format("{0},{1}", i, j);
                        temp.Size = new Size(Box_size, Box_size);
                        temp.Location = new Point(space_x + j * temp.Size.Height, space_y + i * temp.Size.Width);
                        temp.Text = string.Format("H{0}\nT{1}", d % 100, (d - (d % 100)) / 100);
                        matrix[i, j].high = d%100;
                        matrix[i, j].texture = (d - (d % 100)) / 100;
                        temp.Click += ClickFunc;
                        temp.MouseEnter += MouseOverFunc;
                        this.Controls.Add(temp);
                        array[i, j] = (temp);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string filename = global_filename;
            StreamWriter sw = new StreamWriter(filename);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sw.Write(matrix[i, j].high + matrix[i, j].texture * 100);
                    if (j != matrix.GetLength(1) - 1) sw.Write(",");
                }
                sw.WriteLine();
            }
            sw.Flush();
            sw.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
