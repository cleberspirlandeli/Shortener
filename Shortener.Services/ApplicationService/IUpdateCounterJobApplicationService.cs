using System.Threading.Tasks;

namespace Shortener.Services.ApplicationService
{
    public interface IUpdateCounterJobApplicationService
    {
        Task UpdateDailyCounter();
    }
}
