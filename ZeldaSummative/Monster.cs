using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ZeldaSummative
{
    class Monster
    {

        //Variables
        public int x, y, size, speed, direction;
        public Image[] images = new Image[4];

        //creating the monster class
        public Monster(int _x, int _y, int _size, int _speed, int _direction, Image[] _images)
        {
            x = _x;
            y = _y;
            size = _size;
            speed = _speed;
            direction = _direction;
            images = _images;
        }

        //methoed to automattically move the 
        public void move(Monster a)
        {
            switch (a.direction)
            {
                case 0:
                    a.y += a.speed;
                    break;

                case 1:
                    a.x -= a.speed;
                    break;

                case 2:
                    a.x += a.speed;
                    break;

                case 3:
                    a.y -= a.speed;
                    break;

                default:
                    break;

            }
        }

        //methoed to check if there is a collsion between a bullet and a monster
        public bool collsion(Monster a, Bullet b)
        {
            Rectangle pa = new Rectangle(a.x, a.y, a.size, a.size);
            Rectangle mb = new Rectangle(b.x, b.y, b.size, b.size);

            if (pa.IntersectsWith(mb) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
