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
            }
            if (ball.Y <= 0 || ball.Y + ball.Size >= Height)
            {
                ball.Speed = new Vector2(ball.Speed.X, -ball.Speed.Y);
            }
        }

        public void HandleBallsCollision(Dane.BallAbstract ball)
        {

        }
    }
}
