using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Utilities.JSONConverters
{
    // https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Converters_IsoDateTimeConverter.htm
    // https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Converters_JavaScriptDateTimeConverter.htm
    // https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Converters_UnixDateTimeConverter.htm
    // https://www.newtonsoft.com/json/help/html/M_Newtonsoft_Json_JsonConvert_ToString_3.htm


    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = reader.DateFormatString = "dd-MM-yyyy";
            DateTime dt;

            bool parse = DateTime.TryParse(s, out dt);
            if (parse)
            {
                return dt;
            }

            return new DateTime(2000, 1, 1);
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            // writer.WriteValue(value.ToShortDateString());
            writer.DateFormatString = "dd-MM-yyyy";
            writer.WriteValue(value.Date);
        }
    }
}