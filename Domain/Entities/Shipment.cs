namespace Domain.Entities; 

public class Shipment: BaseEntity {
    public decimal Weight { get; set; }  
    public double? StorageTemperature { get; set; }
    public bool Breakable { get; set; } = false; 
    public int Type { get; set; } 
    public int Quantity { get; set; }
    public int QuantityMeasure { get; set; } 
    
    public Trip Trip { get; set; } 
    
    public Shipment(decimal weight, double? storageTemperature, bool breakable, int type, int quantity,
        int quantityMeasure) {
        Weight = weight;
        StorageTemperature = storageTemperature;
        Breakable = breakable;
        Type = type;
        Quantity = quantity;
        QuantityMeasure = quantityMeasure;
    }

    public Shipment() {
    }
}