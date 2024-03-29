namespace CleanCode
{
    internal class CompactRepository : IRepository
    {
        public void SaveSpeaker(Speaker speaker)
        {
            Console.WriteLine($"{speaker.FirstName} is saved to the db");
        }
    }
}
