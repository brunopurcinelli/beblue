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
    public class SalesController : BaseController
    {
        private readonly ISalesAppService _appService;

        public SalesController(
            ISalesAppService appService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _appService = appService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("sales/{page:int}/{size:int}/{dateInitial:datetime}/{dateFinal:datetime}")]
        public IActionResult Get(int page, int size, DateTime dateInitial, DateTime dateFinal)
        {
            var response = _appService.GetAll(page, size, dateInitial, dateFinal);
            return Ok(new SelfResponse
            {
                Href = $"api/v1/sales/{page}/{size}/{dateInitial}/{dateFinal}",
                Rel = new[] { "collection" },
                Size = response.Count(),
                Page = page,
                Value = response
            });    
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("sales/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var response = _appService.GetById(id);
            return Ok(new SelfResponse
            {
                Href = $"api/v1/sales/{id}",
                Rel = new[] { "single" },
                Size = 1,
                Value = response
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sales/new")]
        public IActionResult Post([FromBody] SalesRequest request)
        {
            _appService.Register(request);
            return Ok(new SelfResponse
            {
                Href = $"api/v1/sales/new",
                Rel = new[] { "single" },
                Size = 1,
                Value = null
            });
        }
    }
}