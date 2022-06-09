using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dane
{
    internal class BallConverter : JsonConverter<BallRecord>
    {

        public override BallRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, BallRecord value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("X", value.X);
            writer.WriteNumber("Y", value.Y);
            writer.WriteNumber("SpeedX", value.Speed.X);
            writer.WriteNumber("SpeedY", value.Speed.Y);
            writer.WriteString("Time", value.Time.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture));

            writer.WriteEndObject();
        }
    }
}
