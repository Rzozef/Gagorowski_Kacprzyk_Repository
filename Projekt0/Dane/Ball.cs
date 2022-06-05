using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class BallAbstract : INotifyPropertyChanged
    {
        public abstract float X { get; set; }
        public abstract float Y { get; set; }
        public abstract float Size { get; set; }
        public abstract float Mass { get; set; }

        public abstract event EventHandler<BallEventArgs> ?Moved;

        [JsonIgnore]
        public abstract Vector2 Speed { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;

        public abstract void Move();
        public static BallAbstract CreateBall(float x, float y, float size, float mass, Vector2 speed, DaneAbstractApi dane)
        {
            return new Ball(x, y, size, mass, speed, dane);
        }
    }

    [Serializable]
    internal class Ball : BallAbstract, ISerializable
    {
        private readonly DaneAbstractApi _dane;
        private float _x { get; set; }
        private float _y { get; set; }
        private float _size { get; set; }
        private float _mass { get; set; }
        private Vector2 _position { get; set; }
        public override event EventHandler<BallEventArgs>? Moved;

        public override float X
        {
            get => _x;
            set
            {
                if (value != _x)
                {
                    _dane.Lock();
                    _x = value;
                    _dane.Unlock();
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
                    _dane.Lock();
                    _y = value;
                    _dane.Unlock();
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
                    _dane.Lock();
                    _size = value;
                    _dane.Unlock();
                    NotifyPropertyChanged();
                }
            }
        }
        public override float Mass
        {
            get => _mass;
            set
            {
                _dane.Lock();
                _mass = value;
                _dane.Unlock();
            }
            
        }
        public override Vector2 Speed
        {
            get => _position;
            set
            {
                _dane.Lock();
                _position = value;
                _dane.Unlock();
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
            _dane = dane;

            X = x;
            Y = y;
            Size = size;
            Mass = mass;
            Speed = speed;

            BallEventArgs args = new BallEventArgs(this);
            Moved?.Invoke(this, args);
        }

        public async override void Move()
        {
            long previousTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            while (true)
            {
                long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                long delta = currentTime - previousTime;
                previousTime = currentTime;

                X += Speed.X * delta / 50;
                Y += Speed.Y * delta / 50;

                BallEventArgs args = new BallEventArgs(this);
                Moved?.Invoke(this, args);

                //writer.WriteBallsPosition(serializer.Serialize());
                await Task.Delay(10);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("X", X);
            info.AddValue("Y", Y);
            info.AddValue("Size", Size);
            info.AddValue("Mass", Mass);
            info.AddValue("SpeedX", Speed.X);
            info.AddValue("SpeedY", Speed.Y);
        }
    }
}
