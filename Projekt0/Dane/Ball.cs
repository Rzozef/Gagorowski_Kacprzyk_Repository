using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Dane
{
    public abstract class BallAbstract : INotifyPropertyChanged
    {
        public abstract float X { get; set; }
        public abstract float Y { get; set; }
        public abstract float Size { get; set; }
        public abstract float Mass { get; set; }

        public abstract event EventHandler<BallEventArgs> ?Moved;

        public abstract Vector2 Speed { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;

        public abstract void Move();
        public static BallAbstract CreateBall(float x, float y, float size, float mass, Vector2 speed, DaneAbstractApi dane)
        {
            return new Ball(x, y, size, mass, speed, dane);
        }
    }

    internal class Ball : BallAbstract
    {
        private float _x { get; set; }
        private float _y { get; set; }
        private float _size { get; set; }
        private float _mass { get; set; }
        private Vector2 _position { get; set; }
        private DaneAbstractApi _dane;
        public override event EventHandler<BallEventArgs>? Moved;

        public override float X
        {
            get => _x;
            set
            {
                if (value != _x)
                {
                    _x = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public override float Y
        {
            get => _y;
            set
            {
                if (value != _y)
                {
                    _y = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public override float Size
        {
            get => _size;
            set
            {
                if (value != _size)
                {
                    _size = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public override float Mass
        {
            get => _mass;
            set => _mass = value;
        }
        public override Vector2 Speed
        {
            get => _position;
            set => _position = value;
        }

        public override event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal Ball(float x, float y, float size, float mass, Vector2 speed, DaneAbstractApi dane)
        {
            X = x;
            Y = y;
            Size = size;
            Mass = mass;
            Speed = speed;
            _dane = dane;
        }

        public override void Move()
        {
            X += Speed.X;
            Y += Speed.Y;
            BallEventArgs args = new BallEventArgs(this);
            Moved?.Invoke(this, args);
        }
    }
}
