using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities; 

public class Truck: BaseEntity {
    public string Model { get; set; }
    public string Color { get; set; }
    public int Status { get; set; }
    
    public Truck(string model, string color, int status) {
        Model = model;
        Color = color;
        Status = status;
    }

    public Truck() {
    }

    public override string ToString() {
        return $""" 
        Id: {Id},
        Model: {Model},
        Color: {Color},
        Status: {Status},
        """;
    }
}