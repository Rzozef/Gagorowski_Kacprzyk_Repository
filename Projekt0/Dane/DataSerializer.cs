using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Dane
{
    internal class DataSerializer
    {
        DataSerializer() { }
        private static readonly object _lock = new object();
        private static DataSerializer? _instance = null;
        public static DataSerializer Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataSerializer();
                    }
                    return _instance;
                }
            }
        }

        public string Serialize(BallAbstract ball)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            options.Converters.Add(new BallConverter());
            ball.Lock();

            string output = JsonSerializer.Serialize(new { ball, DateTime.Now }, options);
            ball.Unlock();
            return output;
        }
    }
}
