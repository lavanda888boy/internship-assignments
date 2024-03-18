namespace RepositoryPattern
{
    internal class SocialMedia
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Post> _postRepository;

        public SocialMedia(IRepository<User> userRepository, IRepository<Post> postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public bool publishPost(int userId, Post post)
        {
            User user = _userRepository.GetById(userId);

            if (user.Nickname is null)
            {
                Console.WriteLine("User does not exist");
            }

            post.Author = user;
            _postRepository.Add(post);
            user.Posts.Add(post);
            _userRepository.Update(user);

            return true;
        }
    }
}
