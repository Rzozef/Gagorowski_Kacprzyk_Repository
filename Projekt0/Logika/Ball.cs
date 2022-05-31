using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Logika
{
    public abstract class BallAbstract : INotifyPropertyChanged
    {
        public abstract float X { get; set; }
        public abstract float Y { get; set; }
        public abstract float Size { get; set; }
        public abstract Vector2 Speed { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;

        public static BallAbstract CreateBall(Dane.BallAbstract ball)
        {
            return new Ball(ball);
        }
    }

    internal class Ball : BallAbstract
    {
        private Dane.BallAbstract _parent;

        public override float X
        {
            get => _parent.X;
            set
            {
                if (value != _parent.X)
                {
                    _parent.X = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public override float Y
        {
            get => _parent.Y;
            set
            {
                if (value != _parent.Y)
                {
                    _parent.Y = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public override float Size
        {
            get => _parent.Size;
            set
            {
                if (value != _parent.Size)
                {
                    _parent.Size = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public override Vector2 Speed
        {
            get => _parent.Speed;
            set => _parent.Speed = value;
        }

        public override event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal Ball(Dane.BallAbstract ball)
        {
            _parent = ball;
        }
    }
}
