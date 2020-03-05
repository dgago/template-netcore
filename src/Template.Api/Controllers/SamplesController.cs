using System.Collections.Generic;
using System.Threading.Tasks;

using Application.Commands;
using Application.Models.Result;

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
        public SamplesController(IEventPublisher eventPublisher, Presenter presenter) :
            base(eventPublisher, presenter)
        {
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string id, [FromQuery]string description)
        {
            QueryResult<SampleDto> result =
                await this.EventPublisher.Send(new FindRequest(id, description, 1, 10));
            return this.Presenter.GetListResult(this.Response, result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            EntityResult<SampleDto> result =
                await this.EventPublisher.Send(new GetByIdRequest(id)); 
            return this.Presenter.GetOkObjectResult(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SampleDto item)
        {
            EntityResult<SampleDto> result =
                await this.EventPublisher.Send(new InsertRequest(item));
            return this.Presenter.GetCreatedResult(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] SampleDto item)
        {
            Result result =
                await this.EventPublisher.Send(new UpdateRequest(id, item.Description));
            return this.Presenter.GetNoContentResult(result);
        }

        // DELETE api/values/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Result result = await this.EventPublisher.Send(new DeleteRequest(id));
            return this.Presenter.GetOkResult(result);
        }
    }
}