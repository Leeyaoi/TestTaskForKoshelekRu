namespace TestTask.DAL.Entities;

public class MessageEntity
{
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int Number { get; set; }
}
