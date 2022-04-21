using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Logika;
// model ma sie komunikować z logiką, dopisać testy
namespace Model
{
    public abstract class BallAbstract // TODO przemyśleć gdzie te klasy powinny się znaleźć
    {
        public abstract float BallX { get; }
        public abstract float BallY { get; }
        public abstract float BallSize { get; }
    }
    public class Ball : BallAbstract
    {
        private Logika.Ball _parent;
        public override float BallX
        {
            get => _parent.x;
        }
        public override float BallY
        {
            get => _parent.y;
        }
        public override float BallSize
        {
            get => _parent.size;
        }

        public Ball(Logika.Ball parent)
        {
            _parent = parent;
        }
    }

    public abstract class ModelAbstractApi
    {
        protected LogikaAbstractApi _logic;
        public abstract ObservableCollection<Ball> GetBalls();
        public abstract void CreateBalls(uint count);
        public static ModelAbstractApi CreateApi(uint width, uint height)
        {
            return new ModelApi(width, height);
        }
    }
    internal class ModelApi : ModelAbstractApi
    {
        private ObservableCollection<Ball> _balls;
        public override ObservableCollection<Ball> GetBalls()
        {
            return _balls;
        }
        public override void CreateBalls(uint count)
        {
            _logic.CreateBalls(count);
            _balls.Clear();
            var convertedBalls = _logic.GetBalls().ConvertAll(ball => new Ball(ball));
            foreach (var ball in convertedBalls)
            {
                _balls.Add(ball);
            }
        }
        internal ModelApi(uint width, uint height)
        {
            _logic = LogikaAbstractApi.CreateApi(width, height);
            _balls = new ObservableCollection<Ball>();
        }
    }
}
