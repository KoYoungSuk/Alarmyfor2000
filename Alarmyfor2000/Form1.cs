using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace Alarmyfor2000
{
    public partial class Form1 : Form
    {
        Boolean setting = false;
        SoundPlayer alarmp;
        int time = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        { 
            label4.Text = DateTime.Now.ToString();
            if (setting == true)
            {
                time = time - 1;
                label10.Text = time + " SECONDS";
                if (time <= 0)
                {
                    setting = false;
                    time = 0;
                    if (label8.Text == "Beep Sound")
                    {
                        Console.Beep();
                        Console.Beep();
                        Console.Beep();
                        Console.Beep();
                        Console.Beep();
                        Console.Beep();
                        Console.Beep();
                    }
                    else
                    {
                        alarmp = new SoundPlayer(label8.Text);
                        alarmp.PlayLooping();
                    }
                    
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String file = null;
            if(openFileDialog1.ShowDialog() == DialogResult.OK )
            {
                file = openFileDialog1.FileName;
                label8.Text = file;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            String file_path = label8.Text;
            if(file_path == "Beep Sound")
            {
                Console.Beep();
            }
            else
            {
                SoundPlayer sp = new SoundPlayer(file_path);
                sp.Play();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            String file_path = label8.Text;
            SoundPlayer sp = new SoundPlayer(file_path);
            sp.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int hour;
            int min;
            hour = (int)numericUpDown1.Value;
            min = (int)numericUpDown2.Value;
            for (; hour >= 1; hour--)
            {
                time = time + 3600;
            }
            for (; min >= 1; min--)
            {
                time = time + 60;
            }
            setting = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            alarmp.Stop();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            setting = false;
            label8.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 help = new Form2();
            help.Show();
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
