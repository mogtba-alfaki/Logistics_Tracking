using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers; 

public static class HashHelper {
    private static readonly HashAlgorithm _algorithm = SHA256.Create();
    private static string SALT = Environment.GetEnvironmentVariable("HASHING_SALT"); 
    public static async  Task<string> ComputeHash(string str) {
        var text = Encoding.UTF8.GetBytes(str + SALT);
        byte[] bytes = _algorithm.ComputeHash(text);
        return BitConverter.ToString(bytes); 
    }
}