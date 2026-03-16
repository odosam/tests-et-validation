using TestingTP3;
namespace TestingTP3.Tests;
public class FakeUserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    public void Add(User user) => _users.Add(user);
    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);
    public List<User> GetAll() => _users.ToList();
}