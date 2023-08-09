using Microsoft.AspNetCore.Http;
namespace Infrastructure.Util; 

public class MultiPartFileHandler {
    private readonly string StorageDir = Directory.GetCurrentDirectory() + "/Storage"; 

    public async Task<string> UploadAsync(IFormFile file) {
        var dir = Directory.Exists(StorageDir);
        if (!dir) {
            Directory.CreateDirectory(StorageDir); 
        }
        var fileName = IdGenerator.Generate() + System.IO.Path.GetExtension(file.FileName);
        var savePath = System.IO.Path.Combine(StorageDir, fileName); 
        if (file.Length > 0) {
            Console.WriteLine($"Uploading to Path: {savePath}.............. ");
            await using (var stream = File.Create(savePath)) { 
                await file.CopyToAsync(stream);
            }
        }
        return savePath; 
    }
}