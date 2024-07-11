using Microsoft.EntityFrameworkCore;

namespace Tamagotchi.Data
{
    public static class PetsIniciator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Pets.Any())
                {
                    return;
                }

                var statistics = new Statistics
                {
                    Max_Energy = 200,
                    Max_Fun = 200,
                    Max_Health = 200,
                    Max_Hunger = 200,
                    Max_Hygiene = 200,
                    Energy = 100,
                    Fun = 100,
                    Health = 100,
                    Hunger = 100,
                    Hygiene = 100
                };

                context.Statistics.Add(statistics);
                context.SaveChanges();

                var statistics2 = new Statistics
                {
                    Max_Energy = 300,
                    Max_Fun = 200,
                    Max_Health = 200,
                    Max_Hunger = 100,
                    Max_Hygiene = 200,
                    Energy = 100,
                    Fun = 100,
                    Health = 100,
                    Hunger = 50,
                    Hygiene = 100
                };

                context.Statistics.Add(statistics2);
                context.SaveChanges();

                var pet = new Pets
                {
                    Name = "Chomik",
                    Image = "../Image/Chomik.png",
                    Id_Stat = statistics.Id,
                    Statistics = statistics
                };

                context.Pets.Add(pet);
                context.SaveChanges();

                var pet2 = new Pets
                {
                    Name = "Krolik",
                    Image = "../Image/Krolik.png",
                    Id_Stat = statistics2.Id,
                    Statistics = statistics2
                };

                context.Pets.Add(pet2);
                context.SaveChanges();
            }
        }
    }
}