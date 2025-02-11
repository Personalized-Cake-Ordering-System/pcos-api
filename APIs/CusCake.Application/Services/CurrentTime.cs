namespace CusCake.Application.Services
{

    public interface ICurrentTime
    {
        DateTime GetCurrentTime();
    }
    public class CurrentTime : ICurrentTime
    {
        public DateTime GetCurrentTime() => DateTime.Now;
    }
}


//Test commit