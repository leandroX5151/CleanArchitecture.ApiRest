using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    /// <summary>
    /// Interface personalizada para implementar métodos genéricos
    /// de IAsyncRepository para el mantenimiento de Videos
    /// </summary>
    public interface IVideoRepository : IAsyncRepository<Video>
    {
        Task<Video> GetVideoByName(string name);
        Task<IEnumerable<Video>> GetVideoByUserName(string userName);
    }
}
