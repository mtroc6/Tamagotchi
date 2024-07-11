namespace Tamagotchi.Services
{
    public class ServiceManagement : IServiceManagement
    {
        public void PetCreated() 
        {
            Console.WriteLine($"Pet Created: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
        public void PetDeleted()
        {
            Console.WriteLine($"Pet Deleted: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}
