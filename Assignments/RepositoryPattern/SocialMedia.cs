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

        public bool publishPost(int userId)
        {
            return true;
        }
    }
}
