namespace Core.Users.UseCases.Dto; 

public class UserDto {
    public string Id { get; set; }
    public string Username { get; set; }
    
    public int RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public UserDto(string id, string username, int roleId, DateTime createdAt, DateTime updatedAt) {
        Id = id;
        Username = username;
        RoleId = roleId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}