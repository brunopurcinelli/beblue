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
    public class SalesController : ApiController
    {
        private readonly ISalesAppService _appService;
        private readonly ISalesLineAppService _appLineService;

        public SalesController(
            ISalesAppService appService,
            ISalesLineAppService appLineService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _appService = appService;
            _appLineService = appLineService;
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
        [Authorize(Policy = "CanWriteSalesData")]
        [Route("sales")]
        public IActionResult Post([FromBody] SalesViewModel salesViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(salesViewModel);
            }
            salesViewModel.SalesDate = DateTime.Now.ToLocalTime();

            _appService.Register(salesViewModel);

            return Ok(new SelfResponse
            {
                Href = $"api/v1/sales/new",
                Rel = new[] { "single" },
                Size = 1,
                Value = salesViewModel
            });
        }

        [HttpPut]
        [Authorize(Policy = "CanWriteSalesData")]
        [Route("sales")]
        public IActionResult Put([FromBody]SalesViewModel salesViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(salesViewModel);
            }

            _appService.Update(salesViewModel);

            return Response(salesViewModel);
        }

        [HttpDelete]
        [Authorize(Policy = "CanRemoveSalesData")]
        [Route("sales")]
        public IActionResult Delete(Guid id)
        {
            _appService.Remove(id);

            return Response();
        }
    }
}