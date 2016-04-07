using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeldaSummative
{
    public partial class MainScreen : UserControl
    {
        public static int highscore = 0;

        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            this.Focus();

            label1.Text = "Your Score is: " + Convert.ToString(GameScreen.score);

            if (GameScreen.score > highscore)
            {
                highscore = GameScreen.score;
            }

            scoreLabel.Text = "HIGHSCORE: " + Convert.ToString(highscore);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // f is the form that this control is on - ("this" is the current User Control)
            Form f = this.FindForm();
            f.Controls.Remove(this);

            //if there is a wrong press then game over
            GameScreen gs = new GameScreen();
            f.Controls.Add(gs);
        }
    }
}
