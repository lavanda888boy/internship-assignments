namespace RepositoryPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRepository<User> userRepository = new UserRepository();
            IRepository<Post> postRepository = new PostRepository();

            User user = new User { Id = 1, Nickname = "seva", Email = "seva@mail.com", Posts = new List<Post>() };
            userRepository.Add(user);

            SocialMedia sm = new SocialMedia(userRepository, postRepository);
            Post newPost = new Post { Id = 1, Author = null, Topic = "Greeting", Content = "Hello world!" };
            
            if (sm.publishPost(user.Id, newPost))
            {
                Post publishedPost = postRepository.GetById(newPost.Id);
                Console.WriteLine("Post was succesfully created");
                Console.WriteLine($"Topic: {publishedPost.Topic}\nContent: {publishedPost.Content}\nAuthor: {publishedPost.Author.Nickname}");
            }
        }
    }
}
