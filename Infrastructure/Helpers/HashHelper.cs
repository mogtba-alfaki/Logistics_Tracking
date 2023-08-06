using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Util; 

public static class HashHelper {
    private static HashAlgorithm _algorithm = SHA256.Create();
    private static string SALT = Environment.GetEnvironmentVariable("HASHING_SALT"); 
    public static string ComputeHash(string str) {
        // TODO REFACTOR TO ASYNCHORUNS FUNCTION LATER
        // TODO ADD SALT TO STRING BEFORE HASHING  
        var text = Encoding.UTF8.GetBytes(str + SALT);
        byte[] bytes = _algorithm.ComputeHash(text);
        return BitConverter.ToString(bytes); 
    }
}