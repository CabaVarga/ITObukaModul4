using Homework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Utilities.JSONConverters
{
    public class UserConverter : JsonConverter<User>
    {
        public override User ReadJson(JsonReader reader, Type objectType, User existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return new User()
            {
                Name = "ReaderName"
            };
        }

        public override void WriteJson(JsonWriter writer, User value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Name.ToUpper()); 
        }
    }
}