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
        public int partymember;
        public int force;
    }
    public partial class Form1 : Form
    {
        const int space_x = 35;
        const int space_y = 35;
        const int Box_size = 40;
        int x = 0;
        int y = 0;
        public static Data[,] matrix;
        static Button[,] array;
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
        //Newを押したときの処理
        private void button4_Click(object sender, EventArgs e)
        {
            int t = 0;
            if (!(int.TryParse(textBox1.Text,out t) && int.TryParse(textBox2.Text,out t)))
            {
                MessageBox.Show("入力してください");
                return;
            }
            global_filename = "NewFile*";
            x = int.Parse(textBox1.Text);
            y = int.Parse(textBox2.Text);
            int width = panel2.Size.Width;
            if (x < 30)
            {
                this.Width = space_x + (Box_size + 3) * (x + 1) + width;
            }
            else
            {
                this.Width = space_x + (Box_size + 3) * 30 + width;
            }
            int height = panel2.Size.Height;
            if(y < 20)
            {
                this.Height = space_y + (Box_size + 3) * (y + 1);
                if(this.Height < height)
                {
                    this.Height = height;
                }
            }
            else
            {
                this.Height = space_y + (Box_size + 3) * 20;
            }
            if (array == null)
            {
                array = new Button[y,x];
                Row = new Button[y];
                Column = new Button[x];
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
                array = new Button[y,x];
                Row = new Button[y];
                Column = new Button[x];
            }
            matrix = new Data[y,x];
            for(int i = 0; i < x; i++)
            {
                Column[i] = new Button();
                Column[i].Text = string.Format("C\n{0}", i);
                Column[i].Size = new Size(Box_size, space_y);
                Column[i].Location = new Point(space_x + i * Box_size, 0);
                Column[i].Name = Column[i].Text;
                Column[i].Click += ClickFunc_Column;
                this.Controls.Add(Column[i]);
            }
            for (int i = 0; i < y; i++)
            {
                Row[i] = new Button();
                Row[i].Text = string.Format("R\n{0}", i);
                Row[i].Size = new Size(space_x,Box_size);
                Row[i].Location = new Point(0,space_y + i * Box_size);
                Row[i].Name = Row[i].Text;
                Row[i].Click += ClickFunc_Row;
                this.Controls.Add(Row[i]);
            }
            for (int i = 0;i < y; i++)
            {
                for (int j = 0; j < x; j++)
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
                    ColoredButton(i, j);
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
            switch (matrix[y, x].direction)
            {
                case 0:
                    label11.Text = "指定なし";
                    break;
                case 1:
                    label11.Text = "上";
                    break;
                case 2:
                    label11.Text = "下";
                    break;
                case 3:
                    label11.Text = "左";
                    break;
                case 4:
                    label11.Text = "右";
                    break;
            }
            label13.Text = string.Format("{0}",matrix[y,x].putable);
            label16.Text = string.Format("{0}", matrix[y, x].partymember);
            label18.Text = string.Format("{0}", matrix[y, x].force);
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
            /*int l_0 = matrix.GetLength(0);
            int l_1 = matrix.GetLength(1);
            for(int i = 0; i < l_0; i++)
            {
                for(int j = 0; j < l_1; j++)
                {
                    array[i,j].Text = string.Format("H{0}\nT{1}", matrix[i,j].high, matrix[i,j].texture);
                }
            }*/
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
                        int pm = 1;
                        if(matrix[i,j].force == 1)
                        {
                            pm = -1;
                        }
                        sw.Write(pm*(matrix[i,j].high + matrix[i,j].texture * 100 + matrix[i,j].unit * 100000 + matrix[i,j].direction * 10000000 + matrix[i,j].putable * 100000000 + matrix[i,j].partymember * 1000000000));
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
            if (global_filename[global_filename.Length - 1] == '*')
            {
                DialogResult temp_result = MessageBox.Show("以前のデータが消えますがよろしいですか?", "注意", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
                if (temp_result != DialogResult.Yes) return;
            }
            openFileDialog1.Title = "ファイルを開く";
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.Filter = "csvファイル|*.csv";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                global_filename = filename;
                this.Text = filename;
                StreamReader sr = new StreamReader(filename);
                List<string> line = new List<string>();
                for(x=0; ; x++)
                {
                    if (sr.EndOfStream) break;
                    line.Add(sr.ReadLine());
                }
                y = line[0].Split(',').Length;
                int width = panel2.Size.Width;
                if (x < 30)
                {
                    this.Width = space_x * 2 + Box_size * (x + 1) + width;
                }
                else
                {
                    this.Width = space_x * 2 + Box_size * 30 + width;
                }
                int height = panel2.Size.Height;
                if (y < 20)
                {
                    this.Height = space_y * 2 + Box_size * (y + 1) + height;
                }
                else
                {
                    this.Height = space_y * 2 + Box_size * 20 + height;
                }
                string stream = sr.ReadToEnd();
                sr.Close();
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
                    Column[i].Text = string.Format("C\n{0}", i);
                    Column[i].Size = new Size(Box_size, space_y);
                    Column[i].Location = new Point(space_x + i * Box_size, 0);
                    Column[i].Name = Column[i].Text;
                    Column[i].Click += ClickFunc_Column;
                    this.Controls.Add(Column[i]);
                }
                for (int i = 0; i < x; i++)
                {
                    Row[i] = new Button();
                    Row[i].Text = string.Format("R\n{0}", i);
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
                        Button temp = new Button();
                        matrix[i, j] = new Data();
                        temp.Name = string.Format("{0},{1}", i, j);
                        temp.Size = new Size(Box_size, Box_size);
                        temp.Location = new Point(space_x + j * temp.Size.Height, space_y + i * temp.Size.Width);
                        int d = int.Parse(row_stream[j].Split('\r')[0]);
                        if (d < 0)
                        {
                            matrix[i, j].force = 1;
                            d *= -1;
                        }
                        else
                        {
                            matrix[i, j].force = 0;
                        }
                        temp.Text = string.Format("H{0}\nT{1}", d % 100, (d - (d % 100)) / 100);
                        matrix[i, j].high = d % 100;
                        d = d / 100;
                        matrix[i, j].texture = d % 1000;
                        d /= 1000;
                        matrix[i, j].unit = d % 100;
                        d /= 100;
                        matrix[i, j].direction = d % 10;
                        d /= 10;
                        matrix[i, j].putable = d % 10;
                        d /= 10;
                        matrix[i, j].partymember = d % 10;
                        temp.Click += ClickFunc;
                        temp.MouseEnter += MouseOverFunc;
                        this.Controls.Add(temp);
                        array[i, j] = (temp);
                        ColoredButton(i, j);
                    }
                }
            }
        }
        public static void ColoredButton(int i, int j)
        {
            if (matrix[i, j].putable == 1)
            {
                array[i, j].BackColor = Color.SkyBlue;  
                if(matrix[i,j].unit != 0)
                {
                    array[i, j].BackColor = Color.MediumPurple;

                }
            }
            else if (matrix[i, j].high == 0)
            {
                array[i, j].BackColor = Color.Tan;
            }
            else if (matrix[i, j].unit != 0)
            {
                array[i, j].BackColor = Color.HotPink;
            }
            else
            {
                array[i, j].BackColor = SystemColors.Window;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (global_filename[global_filename.Length - 1] == '*')
            {
                button1_Click(sender,e);
                return;
            }
            DialogResult result = MessageBox.Show("保存しますか?","",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
            if (result != DialogResult.Yes) return;
            string filename = global_filename;
            StreamWriter sw = new StreamWriter(filename);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int pm = 1;
                    if(matrix[i,j].force == 1)
                    {
                        pm = -1;
                    }
                    sw.Write(pm*(matrix[i, j].high + matrix[i, j].texture * 100 + matrix[i, j].unit * 100000 + matrix[i, j].direction * 10000000 + matrix[i, j].putable * 100000000 + matrix[i,j].partymember * 1000000000));
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
