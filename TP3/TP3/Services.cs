using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingTP3;
public class NotificationService
{
    private readonly IUserRepository _users;
    private readonly IEmailSender _emailSender;
    private readonly ISmsSender _smsSender;
    private readonly IClock _clock;

    public NotificationService(IUserRepository users, IEmailSender emailSender, ISmsSender smsSender, IClock clock)
    {
        _users = users;
        _emailSender = emailSender;
        _smsSender = smsSender;
        _clock = clock;
    }

    public void NotifyUser(int userId, string message)
    {
        var user = _users.GetById(userId);
        if (user is null) return;
        if (user.HasOptedOut) return;

        var body = $"Bonjour {user.Name},\n\n{message}\n\nCordialement,\nL'équipe";

        //_emailSender.Send(
        //    user.Email,
        //    //$"Notification du {_clock.Now:dd/MM/yyyy}",
        //    $"[{_clock.Now:dd/MM/yyyy}] Notification",
        //    message
        //);

        _emailSender.Send(user.Email, $"[{_clock.Now:dd/MM/yyyy}] Notification", body);
    }

    public void SendUrgentNotification(int userId, string message)
    {
        var user = _users.GetById(userId);
        if (user is null) return;
        // Les notifications urgentes ignorent l'opt-out
        _emailSender.Send(user.Email, "URGENT", message);
        _smsSender.Send(user.Phone, $"URGENT: {message}");
    }
    public void NotifyAllUsers(string message)
    {
        var users = _users.GetAll();
        foreach (var user in users)
        {
            if (!user.HasOptedOut)
            {
                _emailSender.Send(
                user.Email,
                $"Notification du {_clock.Now:dd/MM/yyyy}",
                message);
            }
        }
    }
}

