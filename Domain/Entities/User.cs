namespace Domain.Entities; 

public class User: BaseEntity {
    public string Username { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
    public string token { get; set; }

    public User(string username, string password, int roleId, string token) {
        Username = username;
        Password = password;
        RoleId = roleId;
        this.token = token;
    }

    public User() {
    }

    public override string ToString() {
        return $""" 
        Username: {Username}
        RoleId: {RoleId}
        """;
    }
}