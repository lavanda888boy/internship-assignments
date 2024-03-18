namespace RepositoryPattern
{
    internal class UserRepository : IRepository<User>
    {
        private List<User> _users = new List<User>();

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public IEnumerable<User> FindAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.Find(user => user.Id == id) ?? new User();
        }

        public void Update(User user)
        {
            int index = _users.FindIndex(u => u.Id == user.Id);
            if (index != -1)
            {
                _users[index] = user;
            } else
            {
                Console.WriteLine("User does not exist and cannot be updated");
            }
        }
    }
}
