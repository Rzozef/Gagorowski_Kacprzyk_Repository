using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class DaneAbstractApi
    {
        
        public abstract event EventHandler<BallEventArgs> ?BallMoved;
        public abstract BallAbstract CreateBall();

        public static DaneAbstractApi CreateApi(uint width, uint height)
        {
            return new DaneApi(width, height);
        }
    }

    internal class DaneApi : DaneAbstractApi
    {
        public override event EventHandler<BallEventArgs>? BallMoved;
        private uint Width { get; }
        private uint Height { get; }
        public DaneApi(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public override BallAbstract CreateBall()
        {
            Random random = new Random();
            float randomSize = random.Next(5, 20);
            float random_x = random.Next(0, (int)(Width - randomSize));
            float random_y = random.Next(0, (int)(Height - randomSize));
            Vector2 randomSpeed = new Vector2(random.Next(-3, 3), random.Next(-3, 3));
            BallAbstract ball = BallAbstract.CreateBall(random_x, random_y, randomSize, (uint)(Math.Pow(randomSize / 2, 2) * Math.PI), randomSpeed);
            ball.Moved += (sender, argv) =>
            {
                var args = new BallEventArgs(argv.Ball);
                BallMoved?.Invoke(this, args);
            };
            return ball;
        }
    }
}
