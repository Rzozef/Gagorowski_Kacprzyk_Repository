using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Logika;

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
            get => _parent.X;
        }
        public override float BallY
        {
            get => _parent.Y;
        }
        public override float BallSize
        {
            get => _parent.Size;
        }

        public Ball(ref Logika.Ball parent)
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
            //var convertedBalls = _logic.GetBalls().ConvertAll(ball => new Ball(ref ball));
            var logicBalls = _logic.GetBalls();
            foreach (var b in logicBalls)
            {
                var o = b;
                _balls.Add(new Ball(ref o));
            }
            _logic._balls.CollectionChanged += CollectionChanged;
            _logic.UpdateBallPosition(100);
        }
        internal ModelApi(uint width, uint height)
        {
            _logic = LogikaAbstractApi.CreateApi(width, height);
            _balls = new ObservableCollection<Ball>();
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            //_balls.Clear();
        }
    }
}
