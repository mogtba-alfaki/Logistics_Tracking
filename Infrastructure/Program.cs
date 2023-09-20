using Infrastructure.MessageBroker;

namespace Infrastructure;

class Program {
    public static void Main(string[] args) {
        LoadEnvVariables(); 
        var worker = new Worker(); 
        worker.Run();
    }


    private static void LoadEnvVariables() {
        foreach(string line in File.ReadAllLines("./.env")) {
            var parts = line.Split("=");
            if (parts.Length != 2)
                continue;
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}
