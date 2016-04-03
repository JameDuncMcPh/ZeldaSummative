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
    public partial class GameScreen : UserControl
    {
        #region Variables
        //Lists /arrays
        bool[] buttons = new bool[4];
        List<Bullet> mag = new List<Bullet>();
        List<Monster> horde = new List<Monster>();

        //Our hero
        Player aeri;

        //images array
        Image[] imagePlayer = { Properties.Resources._0, Properties.Resources._1, Properties.Resources._2, Properties.Resources._3 };
        Image[] imageMonster = { Properties.Resources._4, Properties.Resources._5, Properties.Resources._6, Properties.Resources._7 };
        

        //bool
        bool leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, spaceDown = false;

        //Randoms
        Random r = new Random();

        //ints
        int monsterTimer = 500;
        #endregion

        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            this.Focus();

            aeri = new Player(250, 250, 15, 25, 0, imagePlayer);

            Monster m = new Monster(r.Next(this.Width), r.Next(this.Height), 25, 20, 0, imageMonster);
            horde.Add(m);

            buttons = new bool[] { downArrowDown, leftArrowDown, rightArrowDown , upArrowDown };
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(aeri.images[aeri.direction], aeri.x, aeri.y);

            foreach (Monster m in horde)
            {
                e.Graphics.DrawImage(m.images[m.direction], m.x, m.y);
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 0; i++)
            {
                if (buttons[i] == true)
                {
                    aeri.direction = i;
                    aeri.move(aeri);
                }
            }

            if (spaceDown == true && mag.Count() <= 4)
            {
                Bullet b = new Bullet(aeri.x, aeri.y, 2, 30, aeri.direction);
            }

            if (monsterTimer == 0)
            {
                Monster m = new Monster(r.Next(this.Width), r.Next(this.Height), 25, 20, 0, imageMonster);
                horde.Add(m);
                monsterTimer = 250;
            }
            else
            {
                monsterTimer--;
            }

            foreach (Monster m in horde)
            {
                if (aeri.collsion(aeri, m))
                {
                    // f is the form that this control is on - ("this" is the current User Control)
                    Form f = this.FindForm();
                    f.Controls.Remove(this);

                    //if there is a wrong press then game over
                    MainScreen ms = new MainScreen();
                    f.Controls.Add(ms);
                }

                foreach (Bullet b in mag)
                {
                    if (m.collsion(m, b))
                    {
                        horde.Remove(m);
                        mag.Remove(b);
                        break;
                    }
                }
            }

        }

        private void GameScreen_Keys(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                default:
                    break;
            }
        }
    }
}
