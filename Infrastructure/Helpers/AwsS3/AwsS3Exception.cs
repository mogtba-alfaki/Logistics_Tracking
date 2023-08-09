namespace Infrastructure.Helpers.AwsS3; 

public class AwsS3Exception: Exception {
    public AwsS3Exception(string? message) : base(message) {
    }
}