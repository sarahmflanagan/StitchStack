namespace StitchStack.Models
{
    public class Fabric
    {
        public required int Id { get; set; }
        public required string Type { get; set; }
        public string? Description { get; set; }
        public string? Colour { get; set; }
        public string? Source { get; set; } //TODO add source table?
        public bool? isWoven { get; set; }

        public virtual ICollection<Project> Projects { get; } = new List<Project>();
    }
}
