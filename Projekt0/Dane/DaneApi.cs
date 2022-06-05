﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class DaneAbstractApi
    {
        public abstract void CreateBalls(uint count);
        public abstract ObservableCollection<BallAbstract> GetBalls();
        public abstract void MoveBalls();
        
        public abstract event EventHandler<BallEventArgs> ?BallMoved;
        public abstract void Lock();
        public abstract void Unlock();
        public abstract uint Width { get; }
        public abstract uint Height { get; }

        public static DaneAbstractApi CreateApi(uint width, uint height)
        {
            return new DaneApi(width, height);
        }
    }

    internal class DaneApi : DaneAbstractApi
    {
        private Board _board;
        private DataSerializer serializer;
        private DataWriter writer;
        private Mutex _lock;
        public override event EventHandler<BallEventArgs>? BallMoved;

        public override uint Width { get; }
        public override uint Height { get; }

        private BallsRepository<BallAbstract> Balls
        {
            get { return _board.Balls; }
            set { _board.Balls = value; }
        }

        public DaneApi(uint width, uint height)
        {
            _board = new Board(width, height);
            serializer = new DataSerializer(this);
            writer = new DataWriter("logs", "balls");
            _lock = new Mutex();
        }

        public override void CreateBalls(uint count)
        {
            Random random = new Random();
            for (uint i = 0; i < count; ++i)
            {
                float randomSize = random.Next(5, 20);
                float random_x = random.Next(0, (int)(_board.Width - randomSize));
                float random_y = random.Next(0, (int)(_board.Height - randomSize));
                Vector2 randomSpeed = new Vector2(random.Next(-3, 3), random.Next(-3, 3));
                BallAbstract ball = BallAbstract.CreateBall(random_x, random_y, randomSize, (uint)(Math.Pow(randomSize/2, 2) * Math.PI), randomSpeed, this);
                ball.Moved += (sender, argv) =>
                {
                    var args = new BallEventArgs(argv.Ball);
                    BallMoved?.Invoke(this, args);
                };
                Balls.Add(ball);
            }
        }

        public override ObservableCollection<BallAbstract> GetBalls()
        {
            return Balls;
        }

        public override void MoveBalls()
        {
            foreach (Ball ball in Balls)
            {
                Task.Factory.StartNew(ball.Move);
            }
        }

        public override void Lock()
        {
            _lock.WaitOne();
        }
        public override void Unlock()
        {
            _lock.ReleaseMutex();
        }
    }
}
