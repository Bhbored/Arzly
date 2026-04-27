using Arzly.Api.Application.Contracts;
using Arzly.Api.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public abstract class BaseController<TEntity, TDto, TCreateDto, TUpdateDto, TKey> : ControllerBase
    where TEntity : class
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
    {
        protected readonly IBaseService<TEntity, TDto, TCreateDto, TUpdateDto, TKey> _service;
        protected readonly ILogger _logger;

        protected BaseController(IBaseService<TEntity, TDto, TCreateDto, TUpdateDto, TKey> service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TDto>>> GetAll()
        {
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllAsync();

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDto>> GetById(TKey id)
        {
            _logger.LogInformation("{Controller}.GetById({Id}) - Before",
                GetType().Name, id);

            var result = await _service.GetByIdAsync(id);

            _logger.LogInformation("{Controller}.GetById({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public virtual async Task<ActionResult<TDto>> Create(TCreateDto createDto)
        {
            _logger.LogInformation("{Controller}.Create - Before",
                GetType().Name);

            var result = await _service.CreateAsync(createDto);

            _logger.LogInformation("{Controller}.Create - After",
                GetType().Name);
            return CreatedAtAction(nameof(GetById), new
            {
                id = result?
                .GetType()
                .GetProperty("Id")?
                .GetValue(result)
            },
                result);
        }

        [HttpPut("[action]/{id}")]
        public virtual async Task<ActionResult<TDto>> Update(TKey id, TUpdateDto updateDto)
        {
            _logger.LogInformation("{Controller}.Update({Id}) - Before",
                GetType().Name, id);

            var result = await _service.UpdateAsync(updateDto);

            _logger.LogInformation("{Controller}.Update({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(TKey id)
        {
            _logger.LogInformation("{Controller}.Delete({Id}) - Before",
                GetType().Name, id);

            await _service.DeleteAsync(id);

            _logger.LogInformation("{Controller}.Delete({Id}) - After",
                GetType().Name, id);
            return NoContent();
        }
    }
}
