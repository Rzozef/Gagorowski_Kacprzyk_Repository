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

        private DaneAbstractApi Dane { get; set; }

        public CollisionHandler(uint width, uint height, DaneAbstractApi _dane)
        {
            Width = width;
            Height = height;
            Dane = _dane;
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
            //float interval = 0.003f;
            //float nextBall1X = ball1.X + ball1.Speed.X * interval;
            //float nextBall1Y = ball1.Y + ball1.Speed.Y * interval;
            float nextBall1X = ball1.X;
            float nextBall1Y = ball1.Y;
            IList<Dane.BallAbstract> collidingBalls = Dane.GetCollidingBalls(ball1);
            Vector2 ball1Center = new Vector2(nextBall1X + ball1.Size / 2, nextBall1Y+ ball1.Size / 2);

            foreach (var ball2 in collidingBalls)
            {
                //float nextBall2X = ball2.X + ball2.Speed.X * interval;
                //float nextBall2Y = ball2.Y + ball2.Speed.Y * interval;
                float nextBall2X = ball2.X;
                float nextBall2Y = ball2.Y;
                Vector2 ball2Center = new Vector2(nextBall2X + ball2.Size / 2, nextBall2Y + ball2.Size / 2);

                double powDistanceOfCenters = Math.Pow(ball1Center.X - ball2Center.X, 2) + Math.Pow(ball1Center.Y - ball2Center.Y, 2);

                double massDiv1 = (2 * ball2.Mass) / (ball1.Mass + ball2.Mass);
                double dot1 = Vector2.Dot(ball1.Speed - ball2.Speed, ball1Center - ball2Center);
                Vector2 diff1 = ball1Center - ball2Center;

                double massDiv2 = (2 * ball1.Mass) / (ball1.Mass + ball2.Mass);
                double dot2 = Vector2.Dot(ball2.Speed - ball1.Speed, ball2Center - ball1Center);
                Vector2 diff2 = ball2Center - ball1Center;

                ball1.Speed -= (float)(massDiv1 * dot1) / (float)powDistanceOfCenters * diff1;
                ball2.Speed -= (float)(massDiv2 * dot2) / (float)powDistanceOfCenters * diff2;
            }
        }
    }
}
