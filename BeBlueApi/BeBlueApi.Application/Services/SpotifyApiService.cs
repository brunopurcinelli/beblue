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

        public SpotifyApiService(IHttpClientFactory httpClientFactory,
                                ILogger<SpotifyApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<bool> ConnectSpotifyApi()
        {            
            try
            {
                using (var client = _httpClientFactory.CreateClient("Spotify_Auth"))
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
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        } 
    }
}
