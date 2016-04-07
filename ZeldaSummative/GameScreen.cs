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
        /// <summary>
        /// Made by Duncan McPHerson
        /// April 7 2016
        /// A simple game using classes
        /// </summary>
        
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
        public static int score = 0;
        public static int highscore = 0;
        #endregion

        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            //reset the score and save a copy of the highscore when the menu closes to make sure not to lose it
            score = 0;
            highscore = MainScreen.highscore;

            //set this screen as the focus
            this.Focus();

            //make the player
            ari = new Player(250, 250, 20, 2, 0, imagePlayer);
            
            //make the first monster
            Monster m = new Monster(r.Next(this.Width), r.Next(this.Height), 10, 1, 0, imageMonster);
            horde.Add(m);

            //add the button array
            buttons = new bool[] { downArrowDown, leftArrowDown, rightArrowDown , upArrowDown };
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //set the current postion ari to the varialbes
            currentX = ari.x;
            currentY = ari.y;

            //check if a button is pressed 
            for (int i = 0; i < 4; i++)
            {
                if (buttons[i] == true)
                { 
                    //then move ari
                    ari.direction = i;
                    ari.move(ari);

                    if (ari.x + ari.size > this.Width || ari.x < 0 || ari.y + ari.size > this.Height || ari.y < 0)
                    {
                        //but if he goes to far then return him to previous postions
                        ari.x = currentX;
                        ari.y = currentY;
                    }
                }
            }

            //move each bullet
            foreach (Bullet b in mag)
            {
                b.move(b);

                if (b.x > this.Width || b.x < 0 || b.y > this.Height || b.y < 0)
                {
                    //and remove it if it goes to far
                    mag.Remove(b);
                    break;
                }
            }

            //check if you can fire a bullet
            if (spaceDown == true && mag.Count() < 4)
            {
                //fire one
                Bullet b = new Bullet(ari.x + (ari.size/2), ari.y + (ari.size/2), 2, 10, ari.direction);
                mag.Add(b);
            }

            //see if you can create a new monster
            if (monsterTimer == 0)
            {
                //then make one and up the score
                Monster m = new Monster(r.Next(this.Width), r.Next(this.Height), 10, 1 + Convert.ToInt16(score * .5), 0, imageMonster);
                horde.Add(m);
                monsterTimer = 250;
                score++;
            }
            else
            {
                //or reduce the timer
                monsterTimer--;
            }

            foreach (Monster m in horde)
            {                
                foreach (Bullet b in mag)
                {
                    //check to see if a monster was killed by a bullet
                    if (m.collsion(m, b))
                    {
                        //then remove both and increase the score
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

            //check to see if ari hits a monster
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
                    //else move towards ari
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

            //show score
            scoreLabel.Text = Convert.ToString(score);

            //refresh
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //paint everthing
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
            //check if key is pressed and then set true
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
            //check if key is not being pressed anf set it false
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
