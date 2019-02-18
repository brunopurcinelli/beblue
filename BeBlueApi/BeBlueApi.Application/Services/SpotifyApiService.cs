using BeBlueApi.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeBlueApi.Application.Services
{
    public class SpotifyApiService : ISpotifyApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<SpotifyApiService> _logger;
        private readonly ISpotifyApiService _spotifyApi;

        public SpotifyApiService(IHttpClientFactory httpClientFactory,
                                ISpotifyApiService spotifyApi,
                                ILogger<SpotifyApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _spotifyApi = spotifyApi;
        }

        public async Task<bool> ConnectSpotifyApi()
        {            
            try
            {
                using (var client = _httpClientFactory.CreateClient("Spotify_API"))
                {
                    var response = await client.GetAsync(client.BaseAddress);

                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                    //    var resultApi = JsonConvert.DeserializeObject<RequestResult>(content);

                    //    if (resultApi.Data != null)
                    //    {
                    //        result.Data = JsonConvert.DeserializeObject<List<MoedaResult>>(resultApi.Data.ToString());
                    //    }

                    //    result.Messages = resultApi.Messages;
                    //    result.Status = resultApi.Status;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            //    _logger.LogError(LoggingEvents.GET_ENTITY, ex, _localizer["UnexpectedError"]);
            //    result.Status = StatusResult.Danger;
            //    result.Messages.Add(new Message(string.Format(_localizer["UnexpectedError"], LoggingEvents.GET_ENTITY)));
            }
        } 
    }
}
