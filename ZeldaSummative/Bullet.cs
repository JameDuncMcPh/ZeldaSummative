using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaSummative
{
    class Bullet
    {
        //variables
        public int x, y, size, speed, direction;

        //create the bullet class
        public Bullet(int _x, int _y, int _size, int _speed, int _direction)
        {
            x = _x;
            y = _y;
            size = _size;
            speed = _speed;
            direction = _direction;
        }

        //mehtoed to move the bullet around
        public void move(Bullet a)
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
    }
}
