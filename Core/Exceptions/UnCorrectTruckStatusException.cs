namespace Core.Exceptions; 

public class UnCorrectTruckStatusException: Exception {
    public UnCorrectTruckStatusException(string? message) : base(message) {
    }
}