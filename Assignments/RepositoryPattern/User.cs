namespace RepositoryPattern
{
    internal class User : Entity
    {
        public string Nickname { get; set; }

        public string Email { get; set; }

        public List<Post> Posts { get; set; }
    }
}
