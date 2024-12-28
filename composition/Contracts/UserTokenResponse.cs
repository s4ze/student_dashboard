namespace composition.Contracts;

/// <summary>
/// Модель для возвращения данных о пользователе и токена доступа
/// </summary>
public record class UserTokenResponse
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public required UserModel User { get; set; }
    /// <summary>
    /// Токен доступа для авторизации
    /// </summary>
    public required string AccessToken { get; set; }
    /// <summary>
    /// Модель пользователя для возврата без конфиденциальных данных 
    /// </summary>
    public class UserModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string PhotoUrl { get; set; }
        public string Contact { get; set; }
        public string Group { get; set; }
        public string CreatedAt { get; set; }
    }
}
