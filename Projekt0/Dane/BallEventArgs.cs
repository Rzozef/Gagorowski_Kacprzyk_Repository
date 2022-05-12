using System;

namespace Dane
{
    public class BallEventArgs : EventArgs
    {
        public readonly BallAbstract Ball;
        public BallEventArgs(BallAbstract ball)
        {
            this.Ball = ball;
        }
    }
}
