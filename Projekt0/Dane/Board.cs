using System;
using System.Collections.Generic;
using System.Text;

namespace Dane
{
    internal class Board
    {
        private uint _width;
        private uint _height;
        private BallsRepository<BallAbstract> _balls;

        internal uint Width {get; set;}
        internal uint Height {get; set;}
        internal BallsRepository<BallAbstract> Balls
        {
            get { return _balls; }
            set { _balls = value; }
        }

        internal Board(uint width, uint height)
        {
            Width = width;
            Height = height;
            _balls = new BallsRepository<BallAbstract>();
        }
    }
}
