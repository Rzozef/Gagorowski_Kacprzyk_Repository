using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;

using Dane;

namespace Logika
{
    internal class CollisionHandler
    {
        private uint Width { get; set; }
        private uint Height { get; set; }

        private ObservableCollection<Dane.BallAbstract> BallsRepositoryApi { get; set; }

        public CollisionHandler(uint width, uint height, ObservableCollection<Dane.BallAbstract> _ballsRepositoryApi)
        {
            Width = width;
            Height = height;
            BallsRepositoryApi = _ballsRepositoryApi;
        }

        public void HandleBorderCollision(Dane.BallAbstract ball)
        {
            if (ball.X <= 0 || ball.X + ball.Size >= Width)
            {
                ball.Speed = new Vector2(-ball.Speed.X, ball.Speed.Y);
                if (ball.X <= 0)
                {
                    ball.X *= -1;
                }
                else
                {
                    ball.X -= ball.X + ball.Size - Width;
                }
            }
            if (ball.Y <= 0 || ball.Y + ball.Size >= Height)
            {
                ball.Speed = new Vector2(ball.Speed.X, -ball.Speed.Y);
                if (ball.Y <= 0)
                {
                    ball.Y *= -1;
                }
                else
                {
                    ball.Y -= ball.Y + ball.Size - Height;
                }
            }
        }

        public void HandleBallsCollision(Dane.BallAbstract ball1)
        {
            IList<Dane.BallAbstract> collidingBalls = GetCollidingBalls(ball1);
            Vector2 ball1Center = new Vector2(ball1.X + ball1.Size / 2, ball1.Y + ball1.Size / 2);

            foreach (var ball2 in collidingBalls)
            {
                Vector2 ball2Center = new Vector2(ball2.X + ball2.Size / 2, ball2.Y + ball2.Size / 2);
                double distanceOfCenters = Math.Pow(ball1Center.X - ball2Center.X, 2) + Math.Pow(ball1Center.Y - ball2Center.Y, 2);

                double massDiv1 = (2 * ball2.Mass) / (ball1.Mass + ball2.Mass);
                double dot1 = Vector2.Dot(ball1.Speed - ball2.Speed, ball1Center - ball2Center);
                Vector2 diff1 = ball1Center - ball2Center;

                double massDiv2 = (2 * ball1.Mass) / (ball1.Mass + ball2.Mass);
                double dot2 = Vector2.Dot(ball2.Speed - ball1.Speed, ball2Center - ball1Center);
                Vector2 diff2 = ball2Center - ball1Center;

                ball1.Speed -= (float)(massDiv1 * dot1) / (float)distanceOfCenters * diff1;
                ball2.Speed -= (float)(massDiv2 * dot2) / (float)distanceOfCenters * diff2;
            }
        }

        private IList<Dane.BallAbstract> GetCollidingBalls(Dane.BallAbstract ball)
        {
            Vector2 ballCenter = new Vector2(ball.X + ball.Size / 2, ball.Y + ball.Size / 2);
            IList<Dane.BallAbstract> output = new List<Dane.BallAbstract>();
            foreach (var otherBall in BallsRepositoryApi)
            {
                if (otherBall != ball)
                {
                    Vector2 otherBallCenter = new Vector2(otherBall.X + otherBall.Size / 2, otherBall.Y + otherBall.Size / 2);

                    double distanceOfCenters = Math.Sqrt(Math.Pow(ballCenter.X - otherBallCenter.X, 2) + Math.Pow(ballCenter.Y - otherBallCenter.Y, 2));
                    if ((ball.Size + otherBall.Size)/2 >= distanceOfCenters)
                    {
                        output.Add(otherBall);
                    }
                }
            }
            return output;
        }
    }
}
