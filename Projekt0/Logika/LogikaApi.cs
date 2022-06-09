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
        public abstract void CreateBalls(uint count);
        public abstract IList<BallAbstract> GetBalls();
        public abstract INotifyCollectionChanged NotifyCollectionChanged { get; }
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
        private BallsRepositoryApi<Dane.BallAbstract> Balls { get; set; }
        private CollisionHandler CollisionHandler { get; set; }

        public override INotifyCollectionChanged NotifyCollectionChanged
        {
            get {return Balls;}
        }

        internal LogikaApi(uint width, uint height)
            : this(width, height, DaneAbstractApi.CreateApi(width, height))
        {

        }

        internal LogikaApi(uint width, uint height, DaneAbstractApi dane)
        {
            _dane = dane;
            CollisionHandler = new CollisionHandler(width, height, this);
            _dane.BallMoved += BallMoveEnd;
            Balls = new BallsRepository<Dane.BallAbstract>();
        }

        public override void CreateBalls(uint count)
        {
            for (uint i = 0; i < count; ++i)
            {
                Dane.BallAbstract ball = _dane.CreateBall();
                Balls.Add(ball);
            }
        }

        internal IList<Dane.BallAbstract> GetCollidingBalls(Dane.BallAbstract ball)
        {
            float interval = 0.5f;
            float newBallX = ball.Position.X + ball.Speed.X * interval;
            float newBallY = ball.Position.Y + ball.Speed.Y * interval;
            Vector2 ballCenter = new Vector2(newBallX + ball.Size / 2, newBallY + ball.Size / 2);
            IList<Dane.BallAbstract> output = new List<Dane.BallAbstract>();
            foreach (Dane.BallAbstract otherBall in Balls)
            {
                if (otherBall != ball)
                {
                    float newOtherBallX = otherBall.Position.X + otherBall.Speed.X * interval;
                    float newOtherBallY = otherBall.Position.Y + otherBall.Speed.Y * interval;
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
            foreach (Dane.BallAbstract b in Balls)
            {
                balls.Add(BallAbstract.CreateBall(b));
            }
            return balls;
        }

        private void BallMoveEnd(object obj, BallEventArgs args)
        {
            Dane.BallAbstract ball = args.Ball;
            ball.Lock();
            CollisionHandler.HandleBallsCollision(ball);
            CollisionHandler.HandleBorderCollision(ball);
            ball.Unlock();
        }
    }
}

