using Shared.Filters;
using Microsoft.AspNetCore.Mvc;
using Business.Contracts.Requests;
using Business.Contracts.Interfaces;

namespace WebAPI.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class DogsController : ControllerBase {
        private readonly IDogService _service;

        public DogsController(IDogService service) {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id) {
            var result = await _service.Get(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] DogFilter filter) {
            var result = await _service.GetAll(filter);
            return Ok(result);
        }

        [HttpPost("/Dog")]
        public async Task<ActionResult> Add([FromBody] DogAddRequest request) {
            var result = await _service.Add(request);
            return Ok(result);
        }
    }
}
