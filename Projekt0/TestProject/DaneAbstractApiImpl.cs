using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dane
{
    class DaneAbstractApiImpl : DaneAbstractApi
    {
        public override void CreateBalls(uint count)
        {
            throw new NotImplementedException();
        }

        public override ObservableCollection<BallAbstract> GetBalls()
        {
            throw new NotImplementedException();
        }

        public override void MoveBalls()
        {
            throw new NotImplementedException();
        }

        public override IList<BallAbstract> GetCollidingBalls(BallAbstract ball)
        {
            IList<BallAbstract> list = new List<BallAbstract>();
            list.Add(ball);
            return list;
        }

        public override event EventHandler<BallEventArgs>? BallMoved;
    }
}