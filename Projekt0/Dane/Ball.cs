using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class BallAbstract : INotifyPropertyChanged
    {
        public abstract float Size { get; }
        public abstract float Mass { get; }
        public abstract Vector2 Position { get; set; }

        public abstract event EventHandler<BallEventArgs> ?Moved;

        public abstract Vector2 Speed { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;

        public abstract void Move();
        public static BallAbstract CreateBall(float x, float y, float size, float mass, Vector2 speed, DaneAbstractApi dane)
        {
            return new Ball(x, y, size, mass, speed, dane);
        }
    }

    [Serializable]
    internal class Ball : BallAbstract
    {
        private readonly DaneAbstractApi _dane;
        private Vector2 _position { get; set; }
        private float _size { get; set; }
        private float _mass { get; set; }
        private Vector2 _speed { get; set; }
        public override event EventHandler<BallEventArgs>? Moved;
        public override Vector2 Position
        {
            get => _position;
            set
            {
                if (!value.Equals(_position))
                {
                    _position = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public override float Size
        {
            get => _size;
        }
        public override float Mass
        {
            get => _mass;
        }
        public override Vector2 Speed
        {
            get => _speed;
            set
            {
                _speed = value;
            }
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
            _position = new Vector2(x, y);
            _dane = dane;

            _size = size;
            _mass = mass;
            Speed = speed;

            BallEventArgs args = new BallEventArgs(this);
            Moved?.Invoke(this, args);
        }

        public async override void Move()
        {
            long previousTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            while (true)
            {
                _dane.Lock();
                long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                long delta = currentTime - previousTime;
                previousTime = currentTime;
                Position = new Vector2(Position.X + (Speed.X * delta / 50), Position.Y + (Speed.Y * delta / 50));
                _dane.WriteBall(this);
                _dane.Unlock();

                BallEventArgs args = new BallEventArgs(this);
                Moved?.Invoke(this, args);
                await Task.Delay(10);
            }
        }
    }
}
