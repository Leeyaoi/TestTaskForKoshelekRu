using TestTask.Domain.Interfaces;

namespace TestTask.Domain.Services;

public class DateTimeProvider: IDateTimeProvider
{
    public DateTime GetDateTime() => DateTime.UtcNow;
}
