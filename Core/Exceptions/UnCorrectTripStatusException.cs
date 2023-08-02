namespace Core.Exceptions; 

public class UnCorrectTripStatusException: Exception {
    public UnCorrectTripStatusException(string? message) : base(message) {
    }
}