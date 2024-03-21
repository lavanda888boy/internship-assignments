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

        public void PublishPost(int userId, Post post)
        {
            User user = _userRepository.GetById(userId);
            if (user is null)
            {
                throw new UserNotFoundException("User was not found by id");
            }
            else
            {
                post.Author = user;
                _postRepository.Add(post);
                user.Posts.Add(post);
                _userRepository.Update(user);
            }
        }
    }
}
