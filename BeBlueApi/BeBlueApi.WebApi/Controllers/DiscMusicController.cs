using BeblueApi.Domain.Core.Bus;
using BeblueApi.Domain.Core.Notifications;
using BeBlueApi.Application.Interfaces;
using BeBlueApi.Application.ViewModels;
using FluentSpotifyApi;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web; //Base Namespace
using SpotifyAPI.Web.Auth; //All Authentication-related classes
using SpotifyAPI.Web.Enums; //Enums
using SpotifyAPI.Web.Models; //Models for the JSON-responses

namespace BeBlueApi.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class DiscMusicController : ApiController
    {
        private readonly IDiscMusicAppService _appService;

        public DiscMusicController(
            IDiscMusicAppService appService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {

            _appService = appService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("disc-music/{page:int}/{size:int}")]
        public IActionResult Get(int page, int size)
        {
            var response = _appService.GetAll(page, size);
            return Ok(new SelfResponse
            {
                Href = $"api/v1/disc-music/{page}/{size}",
                Rel = new[] { "collection" },
                Size = response.Count(),
                Page = page,
                Value = response
            });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("disc-music/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var response = _appService.GetById(id);

            return Ok(new SelfResponse
            {
                Href = $"api/v1/disc-music/{id}",
                Rel = new[] { "collection" },
                Size = 1,
                Value = response
            });
        }
    }
}