using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Dane
{
    public abstract class DaneAbstractApi
    {
        public abstract void CreateBalls(uint count);
        public abstract ObservableCollection<BallAbstract> GetBalls();
        public abstract void MoveBalls();

        public static DaneAbstractApi CreateApi(uint width, uint height)
        {
            return new DaneApi(width, height);
        }
    }
    internal class DaneApi : DaneAbstractApi
    {
        private Board _board;
        
        private BallsRepository<BallAbstract> Balls
        {
            get { return _board.Balls; }
            set { _board.Balls = value; }
        }

        public DaneApi(uint width, uint height)
        {
            _board = new Board(width, height);
        }

        public override void CreateBalls(uint count)
        {
            Random random = new Random();
            for (uint i = 0; i < count; ++i)
            {
                float random_x = random.Next(10, (int)(_board.Width - 10));
                float random_y = random.Next(10, (int)(_board.Height - 10));
                Balls.Add(BallAbstract.CreateBall(random_x, random_y, 10));
            }
        }

        public override ObservableCollection<BallAbstract> GetBalls()
        {
            return Balls;
        }

        public override void MoveBalls()
        {
            foreach (var ball in Balls)
            {
                List<Direction> possibleDirections = new List<Direction> { Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT };
                if (ball.X - ball.Size <= 0)
                {
                    possibleDirections.Remove(Direction.LEFT);
                }
                if (ball.X + ball.Size >= _board.Width)
                {
                    possibleDirections.Remove(Direction.RIGHT);
                }
                if (ball.Y - ball.Size <= 0)
                {
                    possibleDirections.Remove(Direction.DOWN);
                }
                if (ball.Y + ball.Size >= _board.Height)
                {
                    possibleDirections.Remove(Direction.UP);
                }
                Random random = new Random();
                int selection = random.Next(0, possibleDirections.Count);
                ball.UpdatePosition(possibleDirections[selection]);
            }
        }
    }
}
