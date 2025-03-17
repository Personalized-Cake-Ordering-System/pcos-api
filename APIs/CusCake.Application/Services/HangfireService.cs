using System.Linq.Expressions;
using Hangfire;

namespace CusCake.Application.Services;

public class JobRequest
{
    public Action Action { get; set; } = default!;
    public DateTime ExecuteTime { get; set; }
}

public interface IHangfireService
{
    void ScheduleJob(JobRequest request);
}

public class HangfireService(IBackgroundJobClient backgroundJobClient) : IHangfireService
{
    private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;

    public void ScheduleJob(JobRequest request)
    {
        var localExecuteTime = request.ExecuteTime.AddHours(7);
        var delay = localExecuteTime - DateTime.UtcNow;

        Expression<Action> expression = () => request.Action.Invoke();

        _backgroundJobClient.Schedule(expression, delay);

    }
}
