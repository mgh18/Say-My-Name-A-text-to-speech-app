using System;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.IO;
using System.Drawing;

namespace Say_My_Words
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer voice = new SpeechSynthesizer();
        int rate = 0;

        public Form1()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.DefaultExt = "txt";
            op.Filter = "text files (*.txt)|*.txt";
            op.Title = "select a files";
            if (op.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(op.OpenFile());
                textBox1.Text = sr.ReadToEnd();


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            voice.SpeakAsyncCancelAll();
            string str = textBox1.Text.Trim();
            if (str != "")
            {
                voice.Volume = 100;
                voice.Rate = rate;
                voice.SpeakAsync(str);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            voice.SpeakAsyncCancelAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            voice.Pause();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            voice.Resume();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "all files(*.*)|*.*|wav files (*.wav)|*.wav";
            save.Title = "save to wav files";
            save.FilterIndex = 2;
            if (save.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(save.FileName,FileMode.Create,FileAccess.Write);
                voice.SetOutputToWaveStream(fs);
                voice.Speak(textBox1.Text);
                fs.Close();

            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            rate = trackBar1.Value;
            voice.Rate = rate;
            button2_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
            voice.SpeakAsyncCancelAll();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(voice.State == SynthesizerState.Ready) { this.Opacity = 100.0; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            voice = new SpeechSynthesizer();
            textBox1.ScrollBars = ScrollBars.Both;
            voice.Rate = -1;
            //this.Opacity = 0.0;
            trackBar1.Value = 0;
            voice.SpeakAsync("Say My Name");
            string input = Microsoft.VisualBasic.Interaction.InputBox("say my name:", "Title");
            if (input != "heisenberg")
            {
                {
                    
                    this.Close();
                   
                }
            }
        }
    }
}
