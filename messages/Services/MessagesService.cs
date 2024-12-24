using messages.Contracts;
using messages.Data;
using messages.Models;

namespace messages.Services;

public class MessagesService(DataContext context)
{
    private readonly DataContext _context = context;
    public Message GetMessage(Guid messageId)
    {
        var message = _context.Messages.First(u => u.MessageId == messageId);
        return message;
    }
    public List<Message> GetMessages(Guid userId)
    {
        var messages = _context.Messages.Where(u => u.ReceiverId == userId);
        return (List<Message>)messages;
    }
    public Message CreateMessage(string receiverId, MessageRequest data)
    {
        var message = new Message()
        {
            MessageId = Guid.NewGuid(),
            SenderId = new Guid(data.SenderId),
            ReceiverId = new Guid(receiverId),
            Content = data.Content,
            CreatedAt = DateTime.Now.ToString("MM-dd-yyyy HH:mmK"),
        };
        _context.Messages.Add(message);
        _context.SaveChanges();
        /* var result = new
        {
            MessageId = message.MessageId.ToString(),
            data.SenderId,
            ReceiverId = receiverId,
            data.Content,
            message.CreatedAt
        };
        return result; */
        return message;
    }
    public Message EditMessage(Guid messageId, string content)
    {
        var message = GetMessage(messageId);
        message.Content = content;
        _context.SaveChanges();
        return message;
    }
    public bool RemoveMessage(Guid messageId)
    {
        if (CheckIfMessageExists(messageId))
        {
            var message = GetMessage(messageId);
            _context.Messages.Remove(message);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
    public bool CheckIfMessageExists(Guid messageId)
    {
        return _context.Messages.Any(u => u.MessageId == messageId);
    }
}
