using System;
using System.Collections.Generic;
using System.Text;

namespace Logika
{
    public class Ball
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public float size { get; private set; }

        private float x_speed;
        private float y_speed;

        public Ball(float x, float y, float size) : this(x, y, size, 0, 0)
        {

        }

        public Ball(float x, float y, float size, float x_speed, float y_speed)
        {
            this.x = x;
            this.y = y;
            this.size = size;
            this.x_speed = x_speed;
            this.y_speed = y_speed;
        }

        public void UpdatePosition()
        {
            this.x += x_speed;
            this.y += y_speed;
        }

        public void ChangeDirection(char axis)
        {
            if(axis == 'x')
            {
                this.x_speed *= -1;
            }
            else if(axis == 'y')
            {
                this.y_speed *= -1;
            }
        }
    }
}
