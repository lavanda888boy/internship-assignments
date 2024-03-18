namespace RepositoryPattern
{
    internal class Post : Entity
    {
        public string Topic { get; set; }

        public string Content { get; set; }

        public User? Author { get; set; }
    }
}
