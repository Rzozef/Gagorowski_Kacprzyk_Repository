using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dane
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public abstract class BallAbstract : INotifyPropertyChanged
    {
        public abstract float X { get; set; }
        public abstract float Y { get; set; }
        public abstract float Size { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;

        public abstract void UpdatePosition(Direction direction);
        public static BallAbstract CreateBall(float x, float y, float size)
        {
            return new Ball(x, y, size);
        }
    }

    internal class Ball : BallAbstract
    {
        private float x { get; set; }
        private float y { get; set; }
        private float size { get; set; }

        public override float X
        {
            get => x;
            set
            {
                if (value != x)
                {
                    x = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public override float Y
        {
            get => y;
            set
            {
                if (value != y)
                {
                    y = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public override float Size
        {
            get => size;
            set
            {
                if (value != size)
                {
                    size = value;
                    NotifyPropertyChanged();
                }
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

        internal Ball(float x, float y, float size)
        {
            this.x = x;
            this.y = y;
            this.size = size;
        }

        public override void UpdatePosition(Direction direction)
        {
            switch(direction)
            {
                case Direction.UP:
                    Y++;
                    break;
                case Direction.DOWN:
                    Y--;
                    break;
                case Direction.LEFT:
                    X--;
                    break;
                case Direction.RIGHT:
                    X++;
                    break;
            }
        }
    }
}
