using System.Text.Json;
using System.Text.Json.Serialization;

public class TolerantEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        if (string.IsNullOrEmpty(value) || !Enum.TryParse<T>(value, true, out var result))
        {
            return (T)(object)-1; // Trả về giá trị mặc định nếu không parse được
        }
        return result;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}