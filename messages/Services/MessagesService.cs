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
        var messages = _context.Messages.Where(u => u.ReceiverId == userId).ToList<Message>();
        return messages;
    }
    public Message CreateMessage(Guid receiverId, MessageRequest data)
    {
        var message = new Message()
        {
            MessageId = Guid.NewGuid(),
            SenderId = new Guid(data.SenderId),
            ReceiverId = receiverId,
            Content = data.Content,
            CreatedAt = DateTime.Now.ToString("MM-dd-yyyy HH:mmK"),
        };
        _context.Messages.Add(message);
        _context.SaveChanges();

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
