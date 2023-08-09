
namespace Domain.Entities; 

public class Truck: BaseEntity {
    public string Model { get; set; }
    public string Color { get; set; }
    public int Status { get; set; }
    
    public string ImageStorageId { get; set; }
    
    public Truck(string model, string color, int status, string imageStorageId) {
        Model = model;
        Color = color;
        Status = status;
        ImageStorageId = imageStorageId;
    }

    public Truck() {
    }

    public override string ToString() {
        return $""" 
        Id: {Id},
        Model: {Model},
        Color: {Color},
        Status: {Status},
        ImageStorageID: {ImageStorageId},
        """;
    }
}