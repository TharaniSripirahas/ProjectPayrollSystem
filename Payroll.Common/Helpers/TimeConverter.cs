using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Payroll.Common.Helpers
{
    public class TimeConverter : JsonConverter<TimeOnly>
    {
        private readonly string _format = "HH:mm:ss";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
                return default;

            var formats = new[] { "HH:mm:ss", "HH:mm:ss.fff", "HH:mm:ss.fffK" };

            if (TimeOnly.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
                return time;

           
            if (DateTime.TryParse(value, null, DateTimeStyles.RoundtripKind, out var dt))
                return TimeOnly.FromDateTime(dt);

            throw new FormatException($"Invalid time format: {value}");
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
