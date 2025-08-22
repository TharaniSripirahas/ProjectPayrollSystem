using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Payroll.Common.Converters
{
    //public class DateTimeConverter : JsonConverter<DateTime>
    //{
    //    private readonly string _format = "yyyy-MM-dd HH:mm:ss";

    //    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        return DateTime.Parse(reader.GetString()!);
    //    }

    //    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    //    {
    //        writer.WriteStringValue(value.ToString(_format));
    //    }
    //}

    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string _format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString()!, _format);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}