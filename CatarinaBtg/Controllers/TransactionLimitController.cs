using CatarinaBtg.Interfaces;
using CatarinaBtg.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatarinaBtg.Controllers{


    [Route("api/[controller]")]
    [ApiController]
    public class TransactionLimitController : ControllerBase
    {
        private readonly ITransactionLimitRepository _repository;

        public TransactionLimitController(ITransactionLimitRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<TransactionLimit>> Get(string cpf)
        {
            var transactionLimit = await _repository.GetTransactionLimitAsync(cpf);
            if (transactionLimit == null)
                return NotFound();

            return transactionLimit;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TransactionLimit transactionLimit)
        {
            await _repository.AddOrUpdateTransactionLimitAsync(transactionLimit);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(TransactionLimit transactionLimit)
        {
            await _repository.RemoveTransactionLimitAsync(transactionLimit);
            return NoContent();
        }
    }

}
