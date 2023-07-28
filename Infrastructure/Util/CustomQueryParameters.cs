
namespace Infrastructure.Util; 

public class CustomQueryParameters {
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string? OrderBy { get; set; } = "CreatedAt"; 
    public string OrderAS { get; set; } = "DESC"; 
}