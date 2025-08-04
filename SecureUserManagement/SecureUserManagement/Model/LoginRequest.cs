using System.Text.Json.Serialization;

namespace SecureUserManagement.Model;

public record LoginRequest
(
    [property: JsonPropertyName("userName")] string UserName,
    [property: JsonPropertyName("password")] string Password
);

