using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IGameRepository
    {
        Task<object> PlayGameAsync(MontyHallGame game);
    }
}
