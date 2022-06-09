using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Dane
{
    internal class BallRecord
    {
        private float _x { get; }
        private float _y { get; }
        private Vector2 _speed { get; }
        private DateTime _time { get; }
        private string _serializedData { get; }

        internal float X { get => _x; }
        internal float Y { get => _y; }
        internal Vector2 Speed { get => _speed; }
        internal DateTime Time { get => _time; }
        internal string SerializedData { get => _serializedData; }

        public BallRecord(BallAbstract ball, DateTime time)
        {
            _x = ball.Position.X;
            _y = ball.Position.Y;
            _speed = new Vector2(ball.Speed.X, ball.Speed.Y);
            _time = time;
            _serializedData = DataSerializer.Instance.Serialize(this, ball);
        }
    }
}
