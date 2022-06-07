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
        public abstract void Lock();
        public abstract void Unlock();
        public abstract uint Width { get; }
        public abstract uint Height { get; }
        public abstract void InitializeWriter(IList<BallAbstract> balls);
        public abstract void WriteBall(BallAbstract ball);
        public abstract BallAbstract CreateBall();

        public static DaneAbstractApi CreateApi(uint width, uint height)
        {
            return new DaneApi(width, height);
        }
    }

    internal class DaneApi : DaneAbstractApi
    {
        private DataSerializer serializer;
        private DataWriter writer;
        private Mutex _lock;
        public override event EventHandler<BallEventArgs>? BallMoved;
        public override uint Width { get; }
        public override uint Height { get; }
        public override void InitializeWriter(IList<BallAbstract> balls)
        {
            writer = new DataWriter("logs", "ball", balls);
        }
        public override void WriteBall(BallAbstract ball)
        {
            writer.WriteBallsPosition(serializer.Serialize(ball), ball);
        }
        public DaneApi(uint width, uint height)
        {
            Width = width;
            Height = height;
            serializer = new DataSerializer(this);
            _lock = new Mutex();
        }

        public override void Lock()
        {
            _lock.WaitOne();
        }
        public override void Unlock()
        {
            _lock.ReleaseMutex();
        }

        public override BallAbstract CreateBall()
        {
            Random random = new Random();
            float randomSize = random.Next(5, 20);
            float random_x = random.Next(0, (int)(Width - randomSize));
            float random_y = random.Next(0, (int)(Height - randomSize));
            Vector2 randomSpeed = new Vector2(random.Next(-3, 3), random.Next(-3, 3));
            BallAbstract ball = BallAbstract.CreateBall(random_x, random_y, randomSize, (uint)(Math.Pow(randomSize / 2, 2) * Math.PI), randomSpeed, this);
            ball.Moved += (sender, argv) =>
            {
                var args = new BallEventArgs(argv.Ball);
                BallMoved?.Invoke(this, args);
            };
            return ball;
        }
    }
}
