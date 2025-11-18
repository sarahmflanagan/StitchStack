namespace StitchStack.Models
{
    public class Pattern
    {
        public required int Id { get; set; }
        public required string  Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string? Brand { get; set; } //TODO add brand table?
        public bool? isWoven { get; set; }
        public bool? isToilComplete { get; set; }
        
        public virtual ICollection<Project> Projects { get; } = new List<Project>();
    }
}
