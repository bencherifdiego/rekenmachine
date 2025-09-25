using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace RekenMachineBackEnd
{
    [ApiController]
    [Route("api/calculate")]
    public class Controller : ControllerBase
    {
        private readonly Service service;

        public Controller(Service service)
        {
            this.service = service;
        }

        [HttpPost]
        public ActionResult<string> Get([FromBody] EquationModel equation)
        {
            return Ok(service.ProcessCalculation(equation.equation));
        }
    }
}
