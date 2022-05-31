using Dane;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Logika
{
    public abstract class LogikaAbstractApi
    {
        public abstract uint screen_width { get; }
        public abstract uint screen_height { get; }
        public abstract void CreateBalls(uint count);
        public abstract IList<BallAbstract> GetBalls();
        public abstract void MoveBalls();
        public abstract INotifyCollectionChanged NotifyCollectionChanged { get; }
        public abstract IList<Dane.BallAbstract> GetCollidingBalls(Dane.BallAbstract ball);
        public static LogikaAbstractApi CreateApi(uint width, uint height)
        {
            return new LogikaApi(width, height);
        }
        public static LogikaAbstractApi CreateApi(uint width, uint height, DaneAbstractApi dane)
        {
            return new LogikaApi(width, height, dane);
        }
    }

    internal class LogikaApi : LogikaAbstractApi
    {
        private DaneAbstractApi _dane;
        private Mutex _mutex;
        private bool _areBallsMoving;
        private CollisionHandler CollisionHandler { get; set; }
        public override uint screen_width { get; }
        public override uint screen_height { get; }

        public override INotifyCollectionChanged NotifyCollectionChanged
        {
            get {return _dane.GetBalls();}
        }

        internal LogikaApi(uint width, uint height)
            : this(width, height, DaneAbstractApi.CreateApi(width, height))
        {
            
        }

        internal LogikaApi(uint width, uint height, DaneAbstractApi dane)
        {
            screen_width = width;
            screen_height = height;
            _dane = dane;
            CollisionHandler = new CollisionHandler(width, height, this);
            _dane.BallMoved += BallMoveEnd;
            _mutex = new Mutex();
            _areBallsMoving = false;
        }

        public override void CreateBalls(uint count)
        {
            _dane.CreateBalls(count);
        }

        public override IList<Dane.BallAbstract> GetCollidingBalls(Dane.BallAbstract ball)
        {
            float interval = 0.5f;
            float newBallX = ball.X + ball.Speed.X * interval;
            float newBallY = ball.Y + ball.Speed.Y * interval;
            Vector2 ballCenter = new Vector2(newBallX + ball.Size / 2, newBallY + ball.Size / 2);
            IList<Dane.BallAbstract> output = new List<Dane.BallAbstract>();
            foreach (var otherBall in _dane.GetBalls())
            {
                if (otherBall != ball)
                {
                    float newOtherBallX = otherBall.X + otherBall.Speed.X * interval;
                    float newOtherBallY = otherBall.Y + otherBall.Speed.Y * interval;
                    Vector2 otherBallCenter = new Vector2(newOtherBallX + otherBall.Size / 2, newOtherBallY + otherBall.Size / 2);

                    double distanceOfCenters = Math.Sqrt(Math.Pow(ballCenter.X - otherBallCenter.X, 2) + Math.Pow(ballCenter.Y - otherBallCenter.Y, 2));
                    if ((ball.Size + otherBall.Size) / 2 >= distanceOfCenters)
                    {
                        output.Add(otherBall);
                    }
                }
            }
            return output;
        }

        public override IList<BallAbstract> GetBalls()
        {
            IList<BallAbstract> balls = new List<BallAbstract>();
            foreach (Dane.BallAbstract b in _dane.GetBalls())
            {
                balls.Add(BallAbstract.CreateBall(b));
            }
            return balls;
        }

        public override void MoveBalls()
        {
            if (!_areBallsMoving)
            {
                _areBallsMoving = true;
                _dane.MoveBalls();
            }
        }

        private void BallMoveEnd(object obj, BallEventArgs args)
        {
            Dane.BallAbstract ball = args.Ball;
            _mutex.WaitOne();
            _dane.Lock();
            CollisionHandler.HandleBallsCollision(ball);
            CollisionHandler.HandleBorderCollision(ball);
            _dane.Unlock();
            _mutex.ReleaseMutex();
        }
    }
}

