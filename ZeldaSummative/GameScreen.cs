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
        Player ari;

        //images array
        Image[] imagePlayer = { Properties.Resources._0, Properties.Resources._1, Properties.Resources._2, Properties.Resources._3 };
        Image[] imageMonster = { Properties.Resources._4, Properties.Resources._5, Properties.Resources._6, Properties.Resources._7 };
        

        //bool
        bool leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, spaceDown = false;
        bool broken = false;

        //Randoms
        Random r = new Random();

        //ints
        int monsterTimer = 500;
        int currentX, currentY = 0;
        int score = 0;
        #endregion

        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            this.Focus();

            ari = new Player(250, 250, 20, 2, 0, imagePlayer);
            
            Monster m = new Monster(r.Next(this.Width), r.Next(this.Height), 10, 1, 0, imageMonster);
            horde.Add(m);

            buttons = new bool[] { downArrowDown, leftArrowDown, rightArrowDown , upArrowDown };
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            currentX = ari.x;
            currentY = ari.y;

            for (int i = 0; i < 4; i++)
            {
                if (buttons[i] == true)
                { 
                    ari.direction = i;
                    ari.move(ari);

                    if (ari.x + ari.size > this.Width || ari.x < 0 || ari.y + ari.size > this.Height || ari.y < 0)
                    {
                        ari.x = currentX;
                        ari.y = currentY;
                    }
                }
            }

            foreach (Bullet b in mag)
            {
                b.move(b);

                if (b.x > this.Width || b.x < 0 || b.y > this.Height || b.y < 0)
                {
                    mag.Remove(b);
                    break;
                }
            }

            if (spaceDown == true && mag.Count() < 4)
            {
                Bullet b = new Bullet(ari.x, ari.y, 2, 10, ari.direction);
                mag.Add(b);
            }

            if (monsterTimer == 0)
            {
                Monster m = new Monster(r.Next(this.Width), r.Next(this.Height), 10, 1 * Convert.ToInt16(score * .5), 0, imageMonster);
                horde.Add(m);
                monsterTimer = 250;
                score++;
            }
            else
            {
                monsterTimer--;
            }

            foreach (Monster m in horde)
            {                
                foreach (Bullet b in mag)
                {
                    if (m.collsion(m, b))
                    {
                        horde.Remove(m);
                        mag.Remove(b);
                        broken = true;
                        score++;
                        break;
                    }
                }
                if (broken)
                {
                    broken = false;
                    break;
                }
            }

            foreach (Monster m in horde)
            {
                if (ari.collsion(ari, m))
                {
                    gameTimer.Stop();
                    // f is the form that this control is on - ("this" is the current User Control)
                    Form f = this.FindForm();
                    f.Controls.Remove(this);

                    //if there is a wrong press then game over
                    MainScreen ms = new MainScreen();
                    f.Controls.Add(ms);
                    break;
                }
                else
                {
                    if (ari.x - m.x > 0)
                    {
                        m.direction = 2;
                        m.move(m);
                    }
                    else if (ari.x - m.x < 0)
                    {
                        m.direction = 1;
                        m.move(m);
                    }

                    if (ari.y - m.y > 0)
                    {
                        m.direction = 0;
                        m.move(m);
                    }
                    else if (ari.y - m.y < 0)
                    {
                        m.direction = 3;
                        m.move(m);
                    }
                }
            }
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush bulletbrush = new SolidBrush(Color.DarkOrange);

            e.Graphics.DrawImage(ari.images[ari.direction], ari.x, ari.y, ari.size, ari.size);

            foreach (Monster m in horde)
            {
                e.Graphics.DrawImage(m.images[m.direction], m.x, m.y, m.size, m.size);
            }

            foreach (Bullet b in mag)
            {
                e.Graphics.FillRectangle(bulletbrush, b.x, b.y, b.size, b.size);
            }

        }

        private void GameScreen_Keys(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    buttons[1] = true;
                    break;
                case Keys.Down:
                    buttons[0] = true;
                    break;
                case Keys.Right:
                    buttons[2] = true;
                    break;
                case Keys.Up:
                    buttons[3] = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    buttons[1] = false;
                    break;
                case Keys.Down:
                    buttons[0] = false;
                    break;
                case Keys.Right:
                    buttons[2] = false;
                    break;
                case Keys.Up:
                    buttons[3] = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                default:
                    break;
            }
        }
    }
}
