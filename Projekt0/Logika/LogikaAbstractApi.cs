using Dane;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Logika
{
    public abstract class LogikaAbstractApi
    {
        public abstract void CreateBalls(uint count);
        public static LogikaAbstractApi CreateApi(uint screen_width, uint screen_height)
        {
            return new LogikaApi(screen_width, screen_height);
        }
    }

    internal class LogikaApi : LogikaAbstractApi
    {
        private SymulacjaAbstractApi symulacja;
        internal LogikaApi(uint screen_width, uint screen_height)
        {
            symulacja = SymulacjaAbstractApi.CreateApi(screen_width, screen_height);
        }

        public override void CreateBalls(uint count)
        {
            Random random = new Random();
            for (uint i = 0; i < count; ++i)
            {
                float random_x = random.Next((int)symulacja.screen_width);
                float random_y = random.Next((int)symulacja.screen_height);

                float random_x_speed = random.Next(10);
                float random_y_speed = random.Next(10);

                symulacja.CreateBall(random_x, random_y, 10, random_x_speed, random_y_speed);
            }
        }
    }
}
