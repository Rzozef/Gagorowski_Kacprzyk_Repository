using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Dane
{
    internal class DataSerializer
    {
        private DaneAbstractApi Dane { get; }

        public DataSerializer(DaneAbstractApi dane)
        {
            Dane = dane;
        }

        public string Serialize(BallAbstract ball)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            options.Converters.Add(new BallConverter());
            Dane.Lock();

            string output = JsonSerializer.Serialize(new { ball, DateTime.Now }, options);
            Dane.Unlock();
            return output;
        }
    }
}
