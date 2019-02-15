using BeBlueApi.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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

        public void ConnectSpotifyAPi()
        {
            using (var client = _httpClientFactory.CreateClient("Spotify_API"))
            {
                
            }
        } 
    }
}
