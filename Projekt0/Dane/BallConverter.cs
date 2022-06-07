using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dane
{
    internal class BallConverter : JsonConverter<BallAbstract>
    {

        public override BallAbstract? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, BallAbstract value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("X", value.Position.X);
            writer.WriteNumber("Y", value.Position.Y);
            writer.WriteEndObject();
        }
    }
}
