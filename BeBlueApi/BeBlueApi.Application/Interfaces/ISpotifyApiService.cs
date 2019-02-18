using System.Threading.Tasks;

namespace BeBlueApi.Application.Interfaces
{
    public interface ISpotifyApiService
    {
        Task<bool> ConnectSpotifyApi();
    }
}
