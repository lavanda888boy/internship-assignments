using SOLID.entity;
using SOLID.exception;

namespace SOLID.repository
{
    internal class UserRepository : IRepository<User>
    {
        private List<User> _users;

        public UserRepository() 
        {
            _users = new List<User>();
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void DeleteById(int id)
        {
            var u = _users.Find(u => u.Id == id);
            if (u is null)
            {
                throw new UserDoesNotExistException($"User with id = {id} cannot be deleted, it does not exist");
            }
            else
            {
                _users.Remove(u);
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            var u = _users.Find(u => u.Id == id);
            if (u is null)
            {
                throw new UserDoesNotExistException($"User with id = {id} cannot be extracted, it does not exist");
            }
            else
            {
                return u;
            }
        }

        public void Update(int id, User user)
        {
            var u = _users.Find(u => u.Id == id);
            if (u is null)
            {
                throw new UserDoesNotExistException("User cannot be updated, it does not exist", id);
            }
            else
            {
                int index = _users.IndexOf(u);
                _users[index] = user;
            }
        }
    }
}
