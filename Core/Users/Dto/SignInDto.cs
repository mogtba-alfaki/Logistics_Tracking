namespace Core.Users.UseCases.Dto; 

public class SignInDto {
    public string Username { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
}