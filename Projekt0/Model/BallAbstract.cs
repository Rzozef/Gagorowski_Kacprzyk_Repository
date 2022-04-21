﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Prezentacja
{
    namespace Model
    {
        public abstract class BallAbstract
        {
            public abstract float BallX { get; }
            public abstract float BallY { get; }
            public abstract float BallSize { get; }
            public static BallAbstract CreateBall(ref Logika.BallAbstract parent)
            {
                return new Ball(ref parent);
            }
        }
        internal class Ball : BallAbstract
        {
            private Logika.BallAbstract _parent;
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

            public Ball(ref Logika.BallAbstract parent)
            {
                _parent = parent;
            }
        }
    }
}
