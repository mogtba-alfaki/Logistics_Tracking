namespace Core.Exceptions; 

public class UnCorrectTripStatusException: Exception {
    public UnCorrectTripStatusException(string? message) : base(message) {
        if (message is null) {
            message = "UnCorrect Trip Status"; 
        }
    }
}