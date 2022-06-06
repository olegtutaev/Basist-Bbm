using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace PianoTiles
{
    public partial class Form1 : Form
    {
        static public int visibleNotes = 4;
        static public int keysAmount = 17;
        public int[,] map = new int[visibleNotes, keysAmount];
        public float cellWidth = 29.8f;
        public int cellHeight = 85;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Piano";
            this.Width = 523;
            this.Height = 720;
            this.Paint += new PaintEventHandler(Repaint);
            this.KeyUp += new KeyEventHandler(OnKeyboardPressed);
            Init();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Q":
                    CheckForPressedButton(0);
                    break;
                case "D2":
                    CheckForPressedButton(1);
                    break;
                case "D3":
                    CheckForPressedButton(3);
                    break;
                case "R":
                    CheckForPressedButton(5);
                    break;
                case "D5":
                    CheckForPressedButton(6);
                    break;
                case "D6":
                    CheckForPressedButton(8);
                    break;
                case "D7":
                    CheckForPressedButton(10);
                    break;
                case "I":
                    CheckForPressedButton(12);
                    break;
                case "D9":
                    CheckForPressedButton(13);
                    break;
                case "D0":
                    CheckForPressedButton(15);
                    break;
            }
        }

        public void CheckForPressedButton(int i)
        {
            if (map[3, i] != 0)
            {
                MoveMap();
                PlaySound(i);
            }
            else
            {
                Random random = new Random();
                int caseNumber = random.Next(0, 4);

                switch (caseNumber)
                {
                    case 0:
                MessageBox.Show("Моргенштерн играет лучше!");
                Init();
                        break;
                    case 1:
                        MessageBox.Show("Посмотри Тик-Ток Паши");
                        Init();
                        break;
                    case 2:
                        MessageBox.Show("Федук жадно смотрит на тебя");
                        Init();
                        break;
                    case 3:
                        MessageBox.Show("На сегодня хватит хитов");
                        Init();
                        break;
                }
            }
        }

        public void PlaySound(int sound)
        {
            Stream str = null;
            switch (sound)
            {
                case 0:
                    str = Properties.Resources._0_C2;
                    break;
                case 1:
                    str = Properties.Resources._1_С_2;
                    break;
                case 3:
                    str = Properties.Resources._3_D_2;
                    break;
                case 5:
                    str = Properties.Resources._5_F2;
                    break;
                case 6:
                    str = Properties.Resources._6_F_2;
                    break;
                case 8:
                    str = Properties.Resources._8_G_2;
                    break;
                case 10:
                    str = Properties.Resources._10_A_2;
                    break;
                case 12:
                    str = Properties.Resources._12_C3;
                    break;
                case 13:
                    str = Properties.Resources._13_C_3;
                    break;
                case 15:
                    str = Properties.Resources._15_D_3;
                    break;
            }
            SoundPlayer snd = new SoundPlayer(str);
            snd.Play();
        }

        public void MoveMap()
        {
            for(var i = 3; i > 0; i--)
            {
                for(var j = 0; j < keysAmount; j++)
                {
                    map[i, j] = map[i - 1, j];
                }
            }
            AddNewLine();
            Invalidate();
        }

        public void AddNewLine()
        {
            var exclude = new HashSet<int>() { 2, 4, 7, 9, 11, 14, 16 };
            var range = Enumerable.Range(0, keysAmount).Where(i => !exclude.Contains(i));
            Random r = new Random();
            var index = r.Next(0, keysAmount - exclude.Count);
            var j = range.ElementAt(index);
            for (var k = 0; k < keysAmount; k++)
                map[0, k] = 0;
            map[0, j] = 1;
        }

        public void Init()
        {
            ClearMap();
            GenerateMap();
            Invalidate();
        }

        public void ClearMap()
        {
            for(var i = 0; i < visibleNotes; i++)
            {
                for(var j = 0; j < keysAmount; j++)
                {
                    map[i, j] = 0;
                }
            }
        }

        public void GenerateMap()
        {
            var exclude = new HashSet<int>() { 2, 4, 7, 9, 11, 14, 16 };
            var range = Enumerable.Range(0, keysAmount).Where(i => !exclude.Contains(i));
            Random r = new Random();
            for(var i = 0; i < visibleNotes; i++)
            {
                var index = r.Next(0, keysAmount - exclude.Count);
                var j = range.ElementAt(index);
                map[i, j] = 1;
            }
        }

        public void DrawMap(Graphics g)
        {
            for(var i = 0; i < visibleNotes; i++)
            {
                for(var j = 0; j < keysAmount; j++)
                {
                    //if(map[i,j] == 0)
                    //{
                    //    g.FillRectangle(new SolidBrush(Color.Black), cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    //}
                    

                    if (map[i,j] == 1)
                    {
                        g.FillRectangle(new SolidBrush(Color.DeepSkyBlue), cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    }
                }
            }
            //for(int i = 0; i < 4; i++)
            //{
            //    g.DrawLine(new Pen(new SolidBrush(Color.White)), 0, i * cellHeight, 4 * cellWidth, i * cellHeight);
            //}
            //for(int i = 0; i < keysAmount; i++)
            //{
            //    g.DrawLine(new Pen(new SolidBrush(Color.Black)), i * cellWidth, 0, i * cellWidth, 8 * cellHeight);
            //}
        }

        private void Repaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawMap(g);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        //{

        //}

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
