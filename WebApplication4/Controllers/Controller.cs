using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using ShoppingCart.BusinessLogic;
using ShoppingCart.Model;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    public class Controller : ControllerBase
    {
        private readonly ILogger<Controller> _logger;

        public Controller(ILogger<Controller> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ActionName("InsertData")]
        public IActionResult InsertData(EncryptModel model)
        {
            try
            {
                var responseText = CriptoServiceType.AES.Encrypt(model.ProductName, model.InfoProduct, model.ClientName);
                return Ok(new EncryptResponse()
                {
                    Response = responseText
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
