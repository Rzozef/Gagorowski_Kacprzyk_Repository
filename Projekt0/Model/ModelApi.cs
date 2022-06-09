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
            public abstract ObservableCollection<AbstractBall> GetBalls();
            public abstract void CreateBalls(uint count);
            public abstract uint WindowWidth { get; set; }
            public abstract uint WindowHeight { get; set; }
            public abstract string BallsNumber { get; set; }
            public abstract bool BeginSimulationClicked { get; set; }
            public static ModelAbstractApi CreateApi(uint width, uint height)
            {
                return new ModelApi(width, height);
            }
            public static ModelAbstractApi CreateApi(uint width, uint height, LogikaAbstractApi logika)
            {
                return new ModelApi(width, height, logika);
            }
        }
        internal class ModelApi : ModelAbstractApi
        {
            private BallsRepository<AbstractBall> _balls;
            private LogikaAbstractApi _logic;
            private uint _windowWidth;
            private uint _windowHeight;
            private string _ballsNumber;
            private bool _beginSimulationClicked;
            protected override LogikaAbstractApi Logic
            {
                get => _logic;
            }
            public override ObservableCollection<AbstractBall> GetBalls()
            {
                return _balls;
            }
            public override string BallsNumber
            {
                get => _ballsNumber;
                set => _ballsNumber = value;
            }
            public override bool BeginSimulationClicked
            {
                get => _beginSimulationClicked;
                set => _beginSimulationClicked = value;
            }
            public override void CreateBalls(uint count)
            {
                _logic.CreateBalls(count);
                _balls.Clear();
                var logicBalls = _logic.GetBalls();
                foreach (var b in logicBalls)
                {
                    var o = b;
                    _balls.Add(Ball.CreateBall(ref o));
                }
                _balls.RegisterPropertyChanged(_logic.NotifyCollectionChanged);
            }
            public override uint WindowWidth
            {
                get => _windowWidth;
                set => _windowWidth = value;
            }
            public override uint WindowHeight
            {
                get => _windowHeight;
                set => _windowHeight = value;
            }
            internal ModelApi(uint width, uint height)
                : this(width, height, LogikaAbstractApi.CreateApi(width, height))
            {
                
            }

            internal ModelApi(uint width, uint height, LogikaAbstractApi logika)
            {
                WindowWidth = width;
                WindowHeight = height;
                BallsNumber = "0";
                BeginSimulationClicked = false;

                _logic = logika;
                _balls = new BallsRepository<AbstractBall>();
            }
        }
    }
}