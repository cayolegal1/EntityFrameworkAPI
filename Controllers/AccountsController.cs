﻿using Microsoft.AspNetCore.Mvc;

namespace APITransferencias.Models
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            return Ok(await _accountRepository.getAllAccounts());
        }


        [HttpGet("{id_cta}")]
        public async Task<IActionResult> GetAccountByID(string id_cta)
        {
            try
            {
                return Ok(await _accountRepository.getAccountByID(id_cta));

            }
            catch (Exception ex)
            {

                return BadRequest("Error en el servicio: " + ex.Message + "\n" + ex.StackTrace);

            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Account accountInfo)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var newClient = await _accountRepository.createAccount(accountInfo);

                return Created("Client created", accountInfo);

            }
            catch (Exception ex)
            {
                return BadRequest("Error en el servicio: " + ex.Message + "\n" + ex.StackTrace);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAccountInfo([FromBody] Account accountInfo)
        {
            try
            {

                if (accountInfo == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newClient = await _accountRepository.updateAccount(accountInfo);

                return Created("Client information up to date", accountInfo);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error en el servicio: {ex.Message}.\n{ex.StackTrace}.");
            }
        }

        //[HttpDelete("{id_cta}")]
        //public async Task<IActionResult> DeleteAccount(string id_cta)
        //{
        //    try
        //    {
        //        if (id_cta == null)
        //        {
        //            return BadRequest();
        //        }

        //        var newClient = await _accountRepository.deleteAccount(new Account { num_cta = id_cta });

        //        return NoContent();

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error en el servicio: {ex.Message}.\n{ex.StackTrace}.");
        //    }
        //}
    }
}
