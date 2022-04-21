using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Logika
{
    public class Ball : INotifyPropertyChanged
    {
        private float x { get; set; }
        public float y { get; set; }
        public float size { get; set; }

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
        {

        }

        public Ball(float x, float y, float size, float x_speed, float y_speed)
        {
            this.x = x;
            this.y = y;
            this.size = size;
            this.x_speed = x_speed;
            this.y_speed = y_speed;
        }

        public void UpdatePosition()
        {
            this.x += x_speed;
            this.y += y_speed;
        }

        public void ChangeDirection(char axis)
        {
            if(axis == 'x')
            {
                this.x_speed *= -1;
            }
            else if(axis == 'y')
            {
                this.y_speed *= -1;
            }
        }
    }
}
