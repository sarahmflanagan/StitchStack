namespace StitchStack.Models
{
    public class Project
    {
        public required int Id { get; set; }
        
        public string? Name { get; set; }
        public string? Description { get; set; }
        
        public bool ToilRequired { get; set; } = false;

        public int? FabricId { get; set; }
        public Fabric? Fabric { get; set; }
        public int? PatternId  { get; set; } 
        public Pattern? Pattern { get; set; }
    }
}
