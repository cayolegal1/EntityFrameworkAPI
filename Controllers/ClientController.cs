
using Microsoft.AspNetCore.Mvc;

namespace APITransferencias.Models
{
    //decoradores
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;

        APIContext context;
        public ClientsController(IClientRepository clientRepository, APIContext db)
        {
            _clientRepository = clientRepository;
            context = db;
        }

        [HttpGet]
        [Route("createDB")]
        public IActionResult CreateDB()
        {
            context.Database.EnsureCreated();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {

            return Ok(await _clientRepository.getAllClients());
        }


        [HttpGet("{cedula}")]
        public async Task<IActionResult> GetClientByCedula(string cedula)
        {   
            try
            {
            return Ok(await _clientRepository.getClientbyCedula(cedula));
            } catch (Exception ex)
            {
                return BadRequest($"Error en el servicio: {ex.Message}.\n{ex.StackTrace}.\n{ex.GetType()}");  
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client clientParam)
        {

            if (clientParam == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = new Client()
            {
                cedula = clientParam.cedula,
                tipo_doc = clientParam.tipo_doc,
                nombre_apellido = clientParam.nombre_apellido,

            };

            try
            {

            var newClient = await _clientRepository.createClient(client);

            return Created("Client created", clientParam);

            } catch(Exception ex)
            {
                return BadRequest($"Error en la creación de cliente: {ex.Message}.\n{ex.StackTrace}");
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] Client clientParam)
        {

            if (clientParam == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newClient = await _clientRepository.updateClient(clientParam);
            return Created("Client information up to date", clientParam);

        }

        [HttpDelete("{cedula}")]
        public async Task<IActionResult> DeleteClient(string cedula)
        {   
            try
            {
            if (cedula == null)
            {
                return BadRequest();
            }

            var newClient = await _clientRepository.deleteClient(cedula);

            return NoContent();

            } catch(Exception ex)
            {
                return BadRequest($"Error en el servicio: {ex.Message}.\n{ex.StackTrace}");
            }

        }
    }
}