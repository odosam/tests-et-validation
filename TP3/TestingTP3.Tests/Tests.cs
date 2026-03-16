using TestingTP3;
namespace TestingTP3.Tests;

public class NotificationServiceTests
{
    [Fact]
    public void NotifyUser_ExistingUser_SendsEmail()
    {
        // Arrange
        var repo = new FakeUserRepository();
        repo.Add(new User
        {
            Id = 1,
            Email = "alice@test.com",
            Phone = "0600000001"
        });
        var emailSpy = new SpyEmailSender();
        var smsSpy = new SpySmsSender();
        var clock = new StubClock(new DateTime(2025, 6, 15));
        var service = new NotificationService(repo, emailSpy, smsSpy, clock);
        // Act
        service.NotifyUser(1, "Bienvenue !");
        // Assert
        Assert.Single(emailSpy.SentEmails);
        Assert.Equal("alice@test.com", emailSpy.SentEmails[0].To);
        Assert.Contains("Bienvenue !", emailSpy.SentEmails[0].Body);
    }

    // Enregistre les sms envoyes pour les verifier dans les tests
    public class SpySmsSender : ISmsSender
    {
        public List<(string PhoneNumber, string Message)> SentMessages { get; }
        = new();
        public void Send(string phoneNumber, string message)
        {
            SentMessages.Add((phoneNumber, message));
        }
    }

    // Fournit une date fixe pour les tests
    public class StubClock : IClock
    {
        private readonly DateTime _now;
        public StubClock(DateTime now) => _now = now;
        public DateTime Now => _now;
    }


    [Fact]
    public void NotifyUser_UserNotFound_DoesNotSendEmail()
    {
        // Arrange — repo vide, pas d'utilisateur avec l'id 999
        var repo = new FakeUserRepository();
        var emailSpy = new SpyEmailSender();
        var smsSpy = new SpySmsSender();
        var clock = new StubClock(new DateTime(2025, 6, 15));
        var service = new NotificationService(repo, emailSpy, smsSpy, clock);

        // Act — NotifyUser(999, ...)
        service.NotifyUser(999, "Bienvenue !");

        // Assert — emailSpy.SentEmails doit etre vide
        Assert.Empty(emailSpy.SentEmails);
    }
    [Fact]
    public void NotifyUser_UserOptedOut_DoesNotSendEmail()
    {
        // Arrange — ajouter un utilisateur avec HasOptedOut = true
        var repo = new FakeUserRepository();
        repo.Add(new User
        {
            Id = 1,
            Email = "alice@test.com",
            Phone = "0600000001",
            HasOptedOut = true
        });
        var emailSpy = new SpyEmailSender();
        var smsSpy = new SpySmsSender();
        var clock = new StubClock(new DateTime(2025, 6, 15));
        var service = new NotificationService(repo, emailSpy, smsSpy, clock);

        // Act — NotifyUser(...)
        service.NotifyUser(1, "Bienvenue !");

        // Assert — emailSpy.SentEmails doit etre vide
        Assert.Empty(emailSpy.SentEmails);
    }
    [Fact]
    public void SendUrgentNotification_ExistingUser_SendsEmailAndSms()
    {
        // Arrange — ajouter un utilisateur
        var repo = new FakeUserRepository();
        repo.Add(new User
        {
            Id = 1,
            Email = "alice@test.com",
            Phone = "0600000001",
            HasOptedOut = false
        });
        var emailSpy = new SpyEmailSender();
        var smsSpy = new SpySmsSender();
        var clock = new StubClock(new DateTime(2025, 6, 15));
        var service = new NotificationService(repo, emailSpy, smsSpy, clock);

        // Act — SendUrgentNotification(...)
        service.SendUrgentNotification(1, "Alerte !");

        // Assert — emailSpy ET smsSpy doivent avoir recu un message
        Assert.Single(emailSpy.SentEmails);
        Assert.Single(smsSpy.SentMessages);
    }
    [Fact]
    public void SendUrgentNotification_UserOptedOut_SendsAnyway()
    {
        // Arrange — ajouter un utilisateur avec HasOptedOut = true
        var repo = new FakeUserRepository();
        repo.Add(new User
        {
            Id = 1,
            Email = "alice@test.com",
            Phone = "0600000001",
            HasOptedOut = true
        });
        var emailSpy = new SpyEmailSender();
        var smsSpy = new SpySmsSender();
        var clock = new StubClock(new DateTime(2025, 6, 15));
        var service = new NotificationService(repo, emailSpy, smsSpy, clock);

        // Act — SendUrgentNotification(...)
        service.SendUrgentNotification(1, "Alerte !");

        // Assert — les notifications urgentes ignorent l'opt-out
        Assert.Single(emailSpy.SentEmails);
        Assert.Single(smsSpy.SentMessages);

    }

    [Fact]
    public void NotifyAllUsers_SendsEmailOnlyToActiveUsers()
    {
        // Arrange — 3 utilisateurs : Alice (active), Bob (active), Charlie (opted out)
        var repo = new FakeUserRepository();
        repo.Add(new User
        {
            Id = 1,
            Email = "alice@gmail.com",
            Phone = "0600000001"
        });

        repo.Add(new User
        {
            Id = 2  ,
            Email = "bob@gmail.com",
            Phone = "0600000002"
        });

        repo.Add(new User
        {
            Id = 2,
            Email = "bob@gmail.com",
            Phone = "0600000001",
            HasOptedOut = true
        });
        var emailSpy = new SpyEmailSender();
        var smsSpy = new SpySmsSender();
        var clock = new StubClock(new DateTime(2025, 6, 15));
        var service = new NotificationService(repo, emailSpy, smsSpy, clock);

        // Act — NotifyAllUsers("Mise a jour")
        service.NotifyAllUsers("Mise a jour");

        // Assert — emailSpy.SentEmails contient 2 emails (Alice et Bob, pas Charlie)
        Assert.Equal(2, emailSpy.SentEmails.Count);

    }

}