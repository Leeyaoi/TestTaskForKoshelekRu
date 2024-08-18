namespace TestTask.API.DTO;

public class MessageDto
{
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int Number { get; set; }
}
