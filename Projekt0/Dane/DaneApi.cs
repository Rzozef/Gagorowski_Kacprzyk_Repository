using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class DaneAbstractApi
    {
        public abstract void CreateBalls(uint count);
        public abstract ObservableCollection<BallAbstract> GetBalls();
        public abstract void MoveBalls();
        
        public abstract event EventHandler<BallEventArgs> ?BallMoved;

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
        public override event EventHandler<BallEventArgs>? BallMoved;

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
                writer.WriteBallsPosition(serializer.Serialize());
                await Task.Delay(10);
            }
        }
    }
}
