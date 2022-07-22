using Microsoft.AspNetCore.Mvc;


namespace APITransferencias.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : Controller
    {
        private readonly IBankRepository _bankRepository;
        public BanksController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        BankValidator Bankvalidator = new BankValidator();

        [HttpGet]
        public async Task<IActionResult> GetAllBanks()
        {
            return Ok(await _bankRepository.getAllBanks());
        }



        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetBankbyCode(string codigo)
        {
            return Ok(await _bankRepository.getBankbyCode(codigo));
        }




        [HttpPost]
        public async Task<IActionResult> CreateBank([FromBody] Bank bankInfo)
        {

            if (bankInfo == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newBankValidate = new Bank()
            {
                codigo_banco = bankInfo.codigo_banco,
                nombre_banco = bankInfo.nombre_banco,
                direccion = bankInfo.direccion
            };

            var validator = Bankvalidator.Validate(newBankValidate);

            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    return BadRequest($"Error en el servicio: {error.ErrorMessage}. Campo: {error.PropertyName}");
                }
            }

            var newClient = await _bankRepository.createBank(bankInfo);

            return Created("Client created", bankInfo);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateBankInfo([FromBody] Bank bankInfo)
        {
            var newBankValidate = new Bank()
            {
                codigo_banco = bankInfo.codigo_banco,
                nombre_banco = bankInfo.nombre_banco,
                direccion = bankInfo.direccion
            };

            var validator = Bankvalidator.Validate(newBankValidate);

            if (!validator.IsValid)
            {
                foreach (var error in validator.Errors)
                {
                    return BadRequest($"Error en el servicio: {error.ErrorMessage}. Campo: {error.PropertyName}");
                }
            }

            if (bankInfo == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {

            var newClient = await _bankRepository.updateBank(bankInfo);

            return Created("Client information up to date", bankInfo);

            } catch(Exception ex)

            {
                return BadRequest($"Error en el servicio {ex.Message}.\n{ex.StackTrace}");
            }
        }

        [HttpDelete("{codigo_banco}")]
        public async Task<IActionResult> DeleteBank(string codigo_banco)
        {
            try
            {
                var newClient = await _bankRepository.deleteBank(new Bank { codigo_banco = codigo_banco });

                return NoContent();

            } catch(Exception ex)

            {
                return BadRequest($"Error en el servicio: {ex.Message}");
            }

        }
    }
}


