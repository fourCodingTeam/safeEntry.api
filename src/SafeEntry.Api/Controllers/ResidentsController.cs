using Microsoft.AspNetCore.Mvc;
using SafeEntry.Application.UseCases.ListResidents;
using SafeEntry.Application.UseCases.Residents;
using SafeEntry.Contracts.Request;
using SafeEntry.Contracts.Responses;

namespace SafeEntry.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentsController : ControllerBase
    {
        private readonly CreateResidentHandler _create;
        private readonly ListResidentsHandler _list;
        private readonly UpdateResidentHandler _update;
        private readonly DeleteResidentHandler _delete;
        private readonly ListResidentsByAddressIdHandler _listByAddressId;

        public ResidentsController(
            CreateResidentHandler create,
            ListResidentsHandler list,
            UpdateResidentHandler update,
            DeleteResidentHandler delete,
            ListResidentsByAddressIdHandler listByAddressId)
        {
            _create = create;
            _list = list;
            _update = update;
            _delete = delete;
            _listByAddressId = listByAddressId;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResidentResponse>>> Get()
            => Ok(await _list.Handle());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResidentResponse>> Get(int id)
        {
            var res = await _list.Handle()
                                 .ContinueWith(t => t.Result.FirstOrDefault(r => r.Id == id));
            return res is not null ? Ok(res) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ResidentResponse>> Post([FromBody] CreateResidentRequest req)
        {
            var res = await _create.Handle(req);
            return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ResidentResponse>> Put(int id, [FromBody] UpdateResidentRequest req)
        {
            if (id != req.Id) return BadRequest();
            var res = await _update.Handle(req);
            return res is not null ? Ok(res) : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => await _delete.Handle(id) ? NoContent() : NotFound();

        [HttpGet("address/{addressId:int}")]
        public async Task<ActionResult<IEnumerable<ResidentResponse>>> GetByAddressId([FromRoute] int addressId)
            => Ok(await _listByAddressId.Handle(addressId));
    }
}
