using System;
using System.Windows.Forms;

namespace Test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        Random r = new Random();

        private void button1_Click(object sender, EventArgs e)
        {
            switch (r.Next(8))
            {
                case 0: pictureBox1.Image = Properties.Resources.Chrysanthemum; break;
                case 1: pictureBox1.Image = Properties.Resources.Desert; break;
                case 2: pictureBox1.Image = Properties.Resources.Hydrangeas; break;
                case 3: pictureBox1.Image = Properties.Resources.Jellyfish; break;
                case 4: pictureBox1.Image = Properties.Resources.Koala; break;
                case 5: pictureBox1.Image = Properties.Resources.Lighthouse; break;
                case 6: pictureBox1.Image = Properties.Resources.Penguins; break;
                case 7: pictureBox1.Image = Properties.Resources.Tulips; break;
            }
        }
    }
}
