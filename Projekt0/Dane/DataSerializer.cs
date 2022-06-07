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

        public Stream Serialize()
        {
            /*            IList<BallAbstract> balls = Dane.GetBalls();


                        var options = new JsonSerializerOptions() { WriteIndented = true };
                        return new MemoryStream
                            (Encoding.ASCII.GetBytes(
                            JsonSerializer.Serialize(new { balls, DateTime.Now }, options)));*/
            return new MemoryStream();
        }
    }
}
