using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Trucks.Dto; 

public class TruckDto {
    public string? Id { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public int  Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    [FromForm(Name = "TruckImage")]
    public IFormFile? TruckImage { get; set; }

    public TruckDto(string id, string model, string color, int status, DateTime createdAt, DateTime updatedAt) {
        Id = id;
        Model = model;
        Color = color;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public TruckDto() {
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