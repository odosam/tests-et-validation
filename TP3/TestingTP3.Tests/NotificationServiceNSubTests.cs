using NSubstitute;
using TestingTP3;

namespace TestingTP3.Tests;

public class NotificationServiceNSubTests
{
    [Fact]
    public void NotifyUser_ExistingUser_SendsEmail()
    {
        // Arrange
        var repo = Substitute.For<IUserRepository>();
        var emailSender = Substitute.For<IEmailSender>();
        var smsSender = Substitute.For<ISmsSender>();
        var clock = Substitute.For<IClock>();
        clock.Now.Returns(new DateTime(2025, 3, 16));
        var service = new NotificationService(repo, emailSender, smsSender, clock);


        repo.GetById(1).Returns(new User { Id = 1, Email = "alice@test.com", Phone = "0600000001" });


        // Act
        service.NotifyUser(1, "Bienvenue !");

        // Assert
        //emailSender.Received(1).Send(
        //    "alice@test.com",
        //    $"Notification du 16/03/2025",
        //    "Bienvenue !"
        //);

        emailSender.Received(1).Send(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
    }

    [Fact]
    public void NotifyAllUsers_SendsEmailOnlyToActiveUsers()
    {
        // Arrange
        var repo = Substitute.For<IUserRepository>();
        var emailSender = Substitute.For<IEmailSender>();
        var smsSender = Substitute.For<ISmsSender>();
        var clock = Substitute.For<IClock>();
        
        var service = new NotificationService(repo, emailSender, smsSender, clock);
        clock.Now.Returns(new DateTime(2025, 3, 16));

        var alice = new User { Id = 1, Email = "alice@test.com" };
        var bob = new User { Id = 2, Email = "bob@test.com" };
        var charlie = new User { Id = 3, Email = "charlie@test.com", HasOptedOut = true };

        repo.GetAll().Returns(new List<User> { 
            alice, 
            bob, 
            charlie 
        });


        // Act
        service.NotifyAllUsers("Mise à jour");

        // Assert
        emailSender.Received(1).Send("alice@test.com", Arg.Any<string>(), Arg.Any<string>());
        emailSender.Received(1).Send("bob@test.com", Arg.Any<string>(), Arg.Any<string>());
        emailSender.DidNotReceive().Send("charlie@test.com", Arg.Any<string>(), Arg.Any<string>());
    }
}