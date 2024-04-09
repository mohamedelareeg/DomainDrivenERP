using System;
using System.Reflection;
using System.Runtime.Serialization;
using CleanArchitectureWithDDD.Domain.Primitives;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Repositories
{
    internal class ValueObjectJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ValueObject).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            object valueObject = FormatterServices.GetUninitializedObject(objectType);

            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string? propertyName = (string)reader.Value;
                    PropertyInfo? property = objectType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (property != null)
                    {
                        reader.Read();
                        if (property.PropertyType.IsSubclassOf(typeof(ValueObject)))
                        {
                            object? nestedObject = serializer.Deserialize(reader, property.PropertyType);
                            MethodInfo? setter = property.GetSetMethod(nonPublic: true);
                            setter?.Invoke(valueObject, new[] { nestedObject });
                        }
                        else
                        {
                            object? propertyValue = Convert.ChangeType(reader.Value, property.PropertyType);
                            SetPrivatePropertyValue(property, valueObject, propertyValue);
                        }
                    }
                    else
                    {
                        reader.Skip();
                    }
                }
            }

            return valueObject;
        }

        private void SetPrivatePropertyValue(PropertyInfo property, object targetObject, object value)
        {
            if (property.CanWrite)
            {
                property.SetValue(targetObject, value);
            }
            else
            {
                FieldInfo? backingField = targetObject.GetType().GetField($"<{property.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                if (backingField != null)
                {
                    backingField.SetValue(targetObject, value);
                }
                else
                {
                    throw new JsonSerializationException($"Property '{property.Name}' does not have a visible setter or backing field.");
                }
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (PropertyInfo property in value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanRead && property.CanWrite)
                {
                    writer.WritePropertyName(property.Name);
                    serializer.Serialize(writer, property.GetValue(value));
                }
            }
            writer.WriteEndObject();
        }
    }
}
