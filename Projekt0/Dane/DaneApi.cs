using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class DaneAbstractApi
    {
        public abstract void CreateBalls(uint count);
        public abstract ObservableCollection<BallAbstract> GetBalls();
        public abstract void MoveBalls();
        public abstract IList<BallAbstract> GetCollidingBalls(BallAbstract ball);
        public abstract event EventHandler<BallEventArgs> ?BallMoved;

        public static DaneAbstractApi CreateApi(uint width, uint height)
        {
            return new DaneApi(width, height);
        }
    }
    internal class DaneApi : DaneAbstractApi
    {
        private Board _board;
        public override event EventHandler<BallEventArgs>? BallMoved;

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
                BallAbstract ball;
                do
                {
                    float random_x = random.Next(0, (int)(_board.Width - 10));
                    float random_y = random.Next(0, (int)(_board.Height - 10));
                    Vector2 randomSpeed = new Vector2(random.Next(-5, 5), random.Next(-5, 5));
                    ball = BallAbstract.CreateBall(random_x, random_y, 15, 0.5f, randomSpeed);
                } while (GetCollidingBalls(ball).Count > 0);
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
        public override IList<BallAbstract> GetCollidingBalls(BallAbstract ball)
        {
            Vector2 ballCenter = new Vector2(ball.X + ball.Size / 2, ball.Y + ball.Size / 2);
            IList<BallAbstract> output = new List<BallAbstract>();
            foreach (var otherBall in GetBalls())
            {
                if (otherBall != ball)
                {
                    Vector2 otherBallCenter = new Vector2(otherBall.X + otherBall.Size / 2, otherBall.Y + otherBall.Size / 2);

                    double distanceOfCenters = Math.Sqrt(Math.Pow(ballCenter.X - otherBallCenter.X, 2) + Math.Pow(ballCenter.Y - otherBallCenter.Y, 2));
                    if ((ball.Size + otherBall.Size) / 2 >= distanceOfCenters)
                    {
                        output.Add(otherBall);
                    }
                }
            }
            return output;
        }

        public override async void MoveBalls()
        {
            while (true)
            {
                IList<Task> tasks = new List<Task>();
                foreach (var ball in Balls)
                {
                    tasks.Add(Task.Factory.StartNew
                    (
                    ball.Move
                    ));
                }
                foreach (Task task in tasks)
                {
                    await task;
                }
                await Task.Delay(10);
            }
        }
    }
}
