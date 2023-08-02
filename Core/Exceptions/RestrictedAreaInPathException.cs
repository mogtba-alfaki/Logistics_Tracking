namespace Core.Exceptions; 

public class RestrictedAreaInPathException: Exception{
    public RestrictedAreaInPathException(string? message) : base(message) {
    }
}