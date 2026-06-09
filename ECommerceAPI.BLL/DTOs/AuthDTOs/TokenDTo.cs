namespace ECommerceAPI.BLL.DTOs.Auth
{
    public record TokenDTo(string AccessToken, int DurationInMinutes, string TokenType = "Bearer");
}
