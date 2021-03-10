using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

namespace core_crud_js.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("permitir")]
    public class PersonaController : ControllerBase
    {


        private readonly ILogger<PersonaController> _logger;

        public PersonaController(ILogger<PersonaController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        public ActionResult Get()
        {
            using (core_crud_js.Models.CrudpocContext db = new core_crud_js.Models.CrudpocContext())
            {
                var lst = (from d in db.Personas
                           select d).ToList();
                return Ok(lst);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Models.Request.PersonaRequest model)
        {
            using (core_crud_js.Models.CrudpocContext db = new core_crud_js.Models.CrudpocContext())
            {
                core_crud_js.Models.Persona oPersona = new core_crud_js.Models.Persona();
                oPersona.Nombre = model.Nombre;
                oPersona.Edad = model.Edad;
                db.Personas.Add(oPersona);
                db.SaveChanges();
            }

            return Ok();
        }


        [HttpPut]
        public ActionResult Put([FromBody] Models.Request.PersonaEditRequest model)
        {
            using (core_crud_js.Models.CrudpocContext db = new core_crud_js.Models.CrudpocContext())
            {
                core_crud_js.Models.Persona oPersona = db.Personas.Find(model.Id);
                oPersona.Nombre = model.Nombre;
                oPersona.Edad = model.Edad;
                db.Entry(oPersona).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] Models.Request.PersonaEditRequest model)
        {
            using (core_crud_js.Models.CrudpocContext db = new core_crud_js.Models.CrudpocContext())
            {
                core_crud_js.Models.Persona oPersona = db.Personas.Find(model.Id);
                db.Personas.Remove(oPersona);
                db.SaveChanges();
            }

            return Ok();
        }



    }
}
