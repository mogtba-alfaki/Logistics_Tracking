namespace Core.Helpers; 

public class IdGenerator {
    public static string Generate() {
        var random = new Random().Next(Int32.MinValue, Int32.MaxValue);
        return Math.Abs(random).ToString(); 
    }
}