namespace Core.Exceptions; 

public class InvalidLoginCredentials: Exception {
    public InvalidLoginCredentials(string? message) : base(message) {
    }
}