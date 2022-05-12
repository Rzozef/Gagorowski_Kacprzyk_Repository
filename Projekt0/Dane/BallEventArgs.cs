using System;
using System.Collections.Generic;
using System.Text;

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
