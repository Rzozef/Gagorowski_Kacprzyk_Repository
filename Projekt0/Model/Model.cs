using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Logika;

namespace Prezentacja
{
    namespace Model
    {
        public abstract class ModelAbstractApi
        {
            protected abstract LogikaAbstractApi Logic { get; }
            public abstract ObservableCollection<BallAbstract> GetBalls();
            public abstract void CreateBalls(uint count);
            public static ModelAbstractApi CreateApi(uint width, uint height)
            {
                return new ModelApi(width, height);
            }
        }
        internal class ModelApi : ModelAbstractApi
        {
            private BallsRepository<BallAbstract> _balls;
            private LogikaAbstractApi _logic;
            protected override LogikaAbstractApi Logic
            {
                get => _logic;
            }
            public override ObservableCollection<BallAbstract> GetBalls()
            {
                return _balls;
            }
            public override void CreateBalls(uint count)
            {
                _logic.CreateBalls(count);
                _balls.Clear();
                var logicBalls = _logic.GetBalls();
                foreach (var b in logicBalls)
                {
                    var o = b;
                    _balls.Add(new Ball(ref o));
                }
                _balls.RegisterPropertyChanged(_logic.GetBalls());

                _logic.UpdateBallPosition(1);
            }
            internal ModelApi(uint width, uint height)
            {
                _logic = LogikaAbstractApi.CreateApi(width, height);
                _balls = new BallsRepository<BallAbstract>();
            }
        }
    }
}