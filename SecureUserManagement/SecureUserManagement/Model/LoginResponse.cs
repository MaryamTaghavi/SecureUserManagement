using System.Text.Json.Serialization;

namespace SecureUserManagement.Model; 

public record LoginResponse
(
    [property: JsonPropertyName("token")] string Token,
    [property: JsonPropertyName("refreshToken")] string RefreshToken
);
