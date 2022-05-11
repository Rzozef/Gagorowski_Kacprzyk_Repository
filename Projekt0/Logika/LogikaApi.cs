using Dane;
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
        public abstract IList<BallAbstract> GetBalls();
        public abstract void MoveBalls();
        public abstract void UpdateBallsPosition(int interval_ms);
        public abstract INotifyCollectionChanged NotifyCollectionChanged { get; }
        public static LogikaAbstractApi CreateApi(uint width, uint height)
        {
            return new LogikaApi(width, height);
        }
        public static LogikaAbstractApi CreateApi(uint width, uint height, DaneAbstractApi dane)
        {
            return new LogikaApi(width, height, dane);
        }
    }
    internal class LogikaApi : LogikaAbstractApi
    {
        private DaneAbstractApi _dane;
        public override uint screen_width { get; }
        public override uint screen_height { get; }

        public override INotifyCollectionChanged NotifyCollectionChanged
        {
            get {return _dane.GetBalls();}
        }

        internal LogikaApi(uint width, uint height)
            : this(width, height, DaneAbstractApi.CreateApi(width, height))
        {
            
        }

        internal LogikaApi(uint width, uint height, DaneAbstractApi dane)
        {
            screen_width = width;
            screen_height = height;
            _dane = dane;
        }

        public override void CreateBalls(uint count)
        {
            _dane.CreateBalls(count);
        }

        public override IList<BallAbstract> GetBalls()
        {
            IList<BallAbstract> balls = new List<BallAbstract>();
            foreach (Dane.BallAbstract b in _dane.GetBalls())
            {
                balls.Add(BallAbstract.CreateBall(b));
            }
            return balls;
        }

        public override void MoveBalls()
        {
            _dane.MoveBalls();
        }

        public override async void UpdateBallsPosition(int interval_ms)
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

