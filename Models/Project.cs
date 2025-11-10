namespace StitchStack.Models
{
    public class Project
    {
        public required int Id { get; set; }
        
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Pattern? PatternChoice { get; set; }
        public Fabric? FabricChoice { get; set; }
        public bool ToilRequired { get; set; } = false;


    }
}
