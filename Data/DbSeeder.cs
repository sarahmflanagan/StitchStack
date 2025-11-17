using StitchStack.Data.InMemory;
using StitchStack.Models;

namespace StitchStack.Data
{
    public class DbSeeder
    {
        public static async Task SeedAsync(InMemoryDBContext context)
        {
            // Check if data already exists
            if (context.Fabrics.Any() || context.Patterns.Any() || context.Projects.Any())
            {
                return; // Database already seeded
            }

            // Seed Fabrics
            var fabrics = new List<Fabric>
            {
                new Fabric
                {
                    Id = 1,
                    Type = "Cotton",
                    Colour = "Navy Blue",
                    Source = "John Lewis",
                    Description = "High quality 100% cotton fabric, perfect for everyday wear",
                    isWoven = true
                },
                new Fabric
                {
                    Id = 2,
                    Type = "Silk",
                    Colour = "Cream",
                    Source = "Liberty London",
                    Description = "Luxurious silk blend, ideal for special occasion garments",
                    isWoven = true
                },
                new Fabric
                {
                    Id = 3,
                    Type = "Linen",
                    Colour = "Natural",
                    Source = "Local Fabric Store",
                    Description = "Pure linen, great for summer clothing",
                    isWoven = true
                },
                new Fabric
                {
                    Id = 4,
                    Type = "Jersey Knit",
                    Colour = "Heather Gray",
                    Source = "Etsy",
                    Description = "Stretchy jersey knit, perfect for t-shirts and casual wear",
                    isWoven = false
                },
                new Fabric
                {
                    Id = 5,
                    Type = "Wool Blend",
                    Colour = "Deep Red",
                    Source = "Waltons Art",
                    Description = "Warm wool blend for winter coats and jumpers",
                    isWoven = true
                }
            };

            await context.Fabrics.AddRangeAsync(fabrics);

            // Seed Patterns
            var patterns = new List<Pattern>
            {
                new Pattern
                {
                    Id = 1,
                    Name = "Simple A-Line Dress",
                    Type = "Dress",
                    Brand = "Simplicity",
                    Description = "Classic A-line dress pattern, great for beginners",
                    isWoven = true,
                    isToilComplete = true
                },
                new Pattern
                {
                    Id = 2,
                    Name = "Fitted Blouse",
                    Type = "Blouse",
                    Brand = "Vogue",
                    Description = "Tailored blouse pattern with darts and collar options",
                    isWoven = true,
                    isToilComplete = false
                },
                new Pattern
                {
                    Id = 3,
                    Name = "Casual T-Shirt",
                    Type = "T-Shirt",
                    Brand = "Indie Designer",
                    Description = "Easy t-shirt pattern with minimal pattern pieces",
                    isWoven = false,
                    isToilComplete = true
                },
                new Pattern
                {
                    Id = 4,
                    Name = "Winter Coat",
                    Type = "Coat",
                    Brand = "Burda",
                    Description = "Structured coat pattern with lining and interfacing",
                    isWoven = true,
                    isToilComplete = false
                },
                new Pattern
                {
                    Id = 5,
                    Name = "Elastic Waist Trousers",
                    Type = "Trousers",
                    Brand = "Simplicity",
                    Description = "Comfortable trousers with elastic waist, great for summer",
                    isWoven = true,
                    isToilComplete = true
                }
            };

            await context.Patterns.AddRangeAsync(patterns);

            // Seed Projects
            var projects = new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Name = "Navy Summer Dress",
                    Description = "A-line dress for summer using navy cotton",
                    FabricId = 1,
                    PatternId = 1,
                    ToilRequired = false
                },
                new Project
                {
                    Id = 2,
                    Name = "Cream Silk Blouse",
                    Description = "Elegant blouse for work occasions",
                    FabricId = 2,
                    PatternId = 2,
                    ToilRequired = true
                },
                new Project
                {
                    Id = 3,
                    Name = "Casual Gray T-Shirt",
                    Description = "Comfortable everyday t-shirt",
                    FabricId = 4,
                    PatternId = 3,
                    ToilRequired = false
                },
                new Project
                {
                    Id = 4,
                    Name = "Red Winter Coat",
                    Description = "Formal winter coat project, needs interfacing and lining",
                    FabricId = 5,
                    PatternId = 4,
                    ToilRequired = true
                },
                new Project
                {
                    Id = 5,
                    Name = "Natural Linen Trousers",
                    Description = "Comfortable summer trousers for holiday",
                    FabricId = 3,
                    PatternId = 5,
                    ToilRequired = false
                }
            };

            await context.Projects.AddRangeAsync(projects);

            // Save all changes
            await context.SaveChangesAsync();
        }
    }
}
