
namespace RepositoryPattern
{
    internal class PostRepository : IRepository<Post>
    {
        private List<Post> _posts = new List<Post>();

        public void Add(Post post)
        {
            _posts.Add(post);
        }

        public void Delete(Post post)
        {
            _posts.Remove(post);
        }

        public IEnumerable<Post> FindAll()
        {
            return _posts;
        }

        public Post GetById(int id)
        {
            return _posts.Find(post => post.Id == id) ?? new Post();
        }

        public void Update(Post post)
        {
            int index = _posts.FindIndex(p => p.Id == post.Id);
            if (index != -1)
            {
                _posts[index] = post;
            }
            else
            {
                Console.WriteLine("Post does not exist");
            }
        }
    }
}
