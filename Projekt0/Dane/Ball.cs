using System;
using System.Collections.Generic;
using System.Text;

namespace Dane
{
    internal class Ball
    {
        float x;
        float y;
        float size;

        float x_speed;
        float y_speed;

        internal Ball(float x, float y, float size) : this(x, y, size, 0, 0)
        {

        }

        internal Ball(float x, float y, float size, float x_speed, float y_speed)
        {
            this.x = x;
            this.y = y;
            this.size = size;
            this.x_speed = x_speed;
            this.y_speed = y_speed;
        }
    }
}
