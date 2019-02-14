using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeblueApi.Domain.Core.Bus;
using BeblueApi.Domain.Core.Notifications;
using BeBlueApi.Application.Interfaces;
using BeBlueApi.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeBlueApi.WebApi.Controllers
{
    [Authorize]
    public class DiscMusicController : BaseController
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