﻿using System.Threading.Tasks;

using Kit.Application.Handlers;
using Kit.Application.Models.Responses;
using Kit.Presentation;

using Microsoft.AspNetCore.Mvc;

using Template.Application.Commands.Sample;
using Template.Application.Commands.Sample.Delete;
using Template.Application.Commands.Sample.Find;
using Template.Application.Commands.Sample.GetById;
using Template.Application.Commands.Sample.Insert;
using Template.Application.Commands.Sample.Update;

namespace Template.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ApiControllerBase
    {
        public SamplesController(IEventPublisher eventPublisher,
            Presenter presenter) : base(eventPublisher, presenter)
        {
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string id,
            [FromQuery] string description,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 3)
        {
            QueryResult<SampleDto> result =
                await EventPublisher.SendAsync(new FindRequest(id,
                    description,
                    pageIndex,
                    pageSize));
            return Presenter.GetListResult(Response, result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            EntityResult<SampleDto> result =
                await EventPublisher.SendAsync(new GetByIdRequest(id));
            return Presenter.GetOkObjectResult(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SampleDto item)
        {
            EntityResult<SampleDto> result =
                await EventPublisher.SendAsync(new InsertRequest(item));
            return Presenter.GetCreatedResult(result,
                Request,
                $"{Request.Path.Value}/{item.Id}");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] SampleDto item)
        {
            Result result =
                await EventPublisher.SendAsync(new UpdateRequest(id, item.Description));
            return Presenter.GetNoContentResult(result);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Result result = await EventPublisher.SendAsync(new DeleteRequest(id));
            return Presenter.GetOkResult(result);
        }
    }
}