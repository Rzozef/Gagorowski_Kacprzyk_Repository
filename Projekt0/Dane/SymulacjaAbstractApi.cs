using System;
using System.Collections.Generic;
using System.Text;

namespace Dane
{
    public abstract class SymulacjaAbstractApi
    {
        public abstract uint screen_width { get; }
        public abstract uint screen_height { get; }
        public abstract void CreateBall(float x, float y, float size, float x_speed, float y_speed);

        public static SymulacjaAbstractApi CreateApi(uint screen_width, uint screen_height)
        {
            return new SymulacjaApi(screen_width, screen_height);
        }
    }

    internal class SymulacjaApi : SymulacjaAbstractApi
    {
        public override uint screen_width { get; }
        public override uint screen_height { get; }
        public IList<Ball> balls;

        internal SymulacjaApi(uint width, uint height)
        {
            screen_width = width;
            screen_height = height;
            balls = new List<Ball>();
        }

        public override void CreateBall(float x, float y, float size, float x_speed, float y_speed)
        {
            balls.Add(new Ball(x, y, size, x_speed, y_speed));
        }
    }
}
