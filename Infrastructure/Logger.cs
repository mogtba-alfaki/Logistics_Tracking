
using Core.Interfaces;

namespace Infrastructure; 

public class Logger: ILogger {
    public void LogInfo(string message) => Log(message, "INFO");
    public void LogDebug(string message) => Log(message, "DEBUG");
    public void LogError(string message) => Log(message, "ERROR");
    public void LogWarn(string message) => Log(message, "WARN"); 

    private void Log(string message, string level) {
        string logMessage = $""" 
        |{level}: 
            {message}
        """;
        Console.WriteLine(logMessage);
    }
}