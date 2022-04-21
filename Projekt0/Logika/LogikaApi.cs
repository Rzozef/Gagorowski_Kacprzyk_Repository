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
        public BallsRepository<Ball> _balls; // TODO private, abstract
        public abstract uint screen_width { get; }
        public abstract uint screen_height { get; }
        public abstract void CreateBalls(uint count);
        public abstract ObservableCollection<Ball> GetBalls();
        public abstract void MoveBalls();
        public abstract void UpdateBallPosition(int interval_ms);
        public static LogikaAbstractApi CreateApi(uint width, uint height)
        {
            return new LogikaApi(width, height);
        }
    }
    internal class LogikaApi : LogikaAbstractApi
    {   
        public override uint screen_width { get; }
        public override uint screen_height { get; }

        public ObservableCollection<Ball> Balls
        {
            get { return _balls; }
        }

        public LogikaApi(uint width, uint height)
        {
            screen_width = width;
            screen_height = height;
            _balls = new BallsRepository<Ball>();
        }

        public override void CreateBalls(uint count)
        {
            Random random = new Random();
            for (uint i = 0; i < count; ++i)
            {
//przedział losowania ograniczony tak, zeby okrąg nie wyszedł poza ekran
//przyjmuje ze size to promien a punktem x i y odnosi sie do srodka okregu
//poki co promien jest staly i wynosi 10
//trzeba sie zastanowic czy krok 10 wzdluz osi nie jest za duzy(a raczej jest)
                float random_x = random.Next(10, (int)(screen_width - 10));
                float random_y = random.Next(10, (int)(screen_height- 10));

                _balls.Add(new Ball(random_x, random_y, 10));
            }
        }

        public override ObservableCollection<Ball> GetBalls()
        {
            return _balls;
        }

        public override void MoveBalls()
        {
            foreach (var ball in Balls)
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

