using System.Text.Json.Serialization;

namespace CusCake.Application.ViewModels;

public class ResponseModel<TMetaData, TPayload>
{
    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; } = 200;

    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; } = new();

    [JsonPropertyName("meta_data")]
    public TMetaData? MetaData { get; set; } = default!;
    [JsonPropertyName("payload")]
    public TPayload? Payload { get; set; } = default!;

    public static ResponseModel<TMetaData, TPayload> Success(TPayload payload, TMetaData? metaData = default)
    {
        return new ResponseModel<TMetaData, TPayload>
        {
            StatusCode = 200,
            Errors = [],
            MetaData = metaData,
            Payload = payload
        };
    }

    public static ResponseModel<TMetaData, TPayload> Fail(int statusCode = 400, List<string> errors = default!, TMetaData? metaData = default)
    {
        return new ResponseModel<TMetaData, TPayload>
        {
            StatusCode = statusCode,
            Errors = errors,
            MetaData = metaData,
            Payload = default
        };
    }
}
