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
        //Lists 
        Bullet[] mag = new Bullet[4];
        List<Monster> horde = new List<Monster>();

        //Our hero
        Player aeri;

        //images
        Image[] imagePlayer = { Properties.Resources._0, Properties.Resources._1, Properties.Resources._2, Properties.Resources._3 };
        Image[] imageMonster = { Properties.Resources._4, Properties.Resources._5, Properties.Resources._6, Properties.Resources._7 };

        //bool
        bool leftArrowDown, downArrowDown, rightArrowDown, upArrowDown = false;

        //Randoms
        Random r = new Random();
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
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
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
                default:
                    break;
            }
        }
    }
}
