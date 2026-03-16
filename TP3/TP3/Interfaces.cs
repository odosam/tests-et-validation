namespace TestingTP3;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool HasOptedOut { get; set; }
}
public interface IUserRepository
{
    User? GetById(int id);
    List<User> GetAll();
}
public interface IEmailSender
{
    void Send(string to, string subject, string body);
}
public interface ISmsSender
{
    void Send(string phoneNumber, string message);
}
public interface IClock
{
    DateTime Now { get; }
}


// Exercice 2 : réponses
// 3 dépendances. Compliqué de tester les 3 car les données sont crées en dur dans la fonction.
// Le risque de ce code en prod est d'instancier la base de prod pour les tests. 
// Pour la requête, il y a un risque d'injection SQL. 

