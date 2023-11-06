namespace StudentManager.Models.Json;

using System;
using System.Collections.Generic;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public class MicrosoftAccessTokenError
{
    [JsonPropertyName("error")]
    public string Error { get; set; }

    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; set; }

    [JsonPropertyName("error_codes")]
    public long[] ErrorCodes { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; set; }

    [JsonPropertyName("trace_id")]
    public Guid TraceId { get; set; }

    [JsonPropertyName("correlation_id")]
    public Guid CorrelationId { get; set; }
}