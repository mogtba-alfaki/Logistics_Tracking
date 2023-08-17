using Microsoft.AspNetCore.Http;

namespace Core.Helpers; 

public static class MultiPartFileHandler {
    private static readonly string StorageDir = Directory.GetCurrentDirectory() + "/Storage"; 

    public static async Task<string> UploadAsync(IFormFile file) {
        if (!Directory.Exists(StorageDir)) {
            Directory.CreateDirectory(StorageDir); 
        }
        var fileName = IdGenerator.Generate() + Path.GetExtension(file.FileName);
        var savePath = Path.Combine(StorageDir, fileName); 
        if (file.Length > 0) {
            Console.WriteLine($"Uploading to Path: {savePath}.............. ");
            await using (var stream = File.Create(savePath)) { 
                await file.CopyToAsync(stream);
            }
        }
        return savePath; 
    }
}