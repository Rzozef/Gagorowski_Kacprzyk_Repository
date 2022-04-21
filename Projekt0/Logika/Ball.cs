using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Logika
{
<<<<<<< HEAD
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public class Ball : INotifyPropertyChanged
    {
=======
    public class Ball : INotifyPropertyChanged
    {
>>>>>>> a5a26aea1d5f590702944712aa4fcb96abadf095
        private float x { get; set; }
        public float y { get; set; }
        public float size { get; set; }

<<<<<<< HEAD
        public float X
=======
        private float x_speed;
        private float y_speed;

        public float X
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
        public float Y
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
        public float Size // TODO dopisz pozostale get/set
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Ball(float x, float y, float size) : this(x, y, size, 0, 0)
>>>>>>> a5a26aea1d5f590702944712aa4fcb96abadf095
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
        public float Y
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
        public float Size // TODO dopisz pozostale get/set
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Ball(float x, float y, float size)
        {
            this.x = x;
            this.y = y;
            this.size = size;
        }

        public void UpdatePosition(Direction direction)
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
