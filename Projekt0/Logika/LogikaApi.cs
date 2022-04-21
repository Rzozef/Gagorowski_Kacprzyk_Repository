using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Logika
{
    public abstract class LogikaAbstractApi
    {
        public abstract uint screen_width { get; }
        public abstract uint screen_height { get; }
        public abstract void CreateBalls(uint count);
        public abstract ObservableCollection<BallAbstract> GetBalls();
        public abstract void MoveBalls();
        public abstract void UpdateBallPosition(int interval_ms);
        public static LogikaAbstractApi CreateApi(uint width, uint height)
        {
            return new LogikaApi(width, height);
        }
    }
    internal class LogikaApi : LogikaAbstractApi
    {
        private BallsRepository<BallAbstract> _balls;
        public override uint screen_width { get; }
        public override uint screen_height { get; }

        public LogikaApi(uint width, uint height)
        {
            screen_width = width;
            screen_height = height;
            _balls = new BallsRepository<BallAbstract>();
        }

        public override void CreateBalls(uint count)
        {
            Random random = new Random();
            for (uint i = 0; i < count; ++i)
            {
                float random_x = random.Next(10, (int)(screen_width - 10));
                float random_y = random.Next(10, (int)(screen_height- 10));
                _balls.Add(new Ball(random_x, random_y, 10));
            }
        }

        public override ObservableCollection<BallAbstract> GetBalls()
        {
            return _balls;
        }

        public override void MoveBalls()
        {
            foreach (var ball in _balls)
            {
                List<Direction> possibleDirections = new List<Direction> { Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT };
                if (ball.X - ball.Size <= 0)
                {
                    possibleDirections.Remove(Direction.LEFT);
                } if (ball.X + ball.Size >= screen_width)
                {
                    possibleDirections.Remove(Direction.RIGHT);
                } if (ball.Y - ball.Size <= 0)
                {
                    possibleDirections.Remove(Direction.DOWN);
                } if (ball.Y + ball.Size >= screen_height)
                {
                    possibleDirections.Remove(Direction.UP);
                }
                Random random = new Random();
                int selection = random.Next(0, possibleDirections.Count);
                ball.UpdatePosition(possibleDirections[selection]);
            }
        }

        public override async void UpdateBallPosition(int interval_ms)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    MoveBalls();
                    Task.Delay(interval_ms).Wait();
                }
            });
        }
    }
}

