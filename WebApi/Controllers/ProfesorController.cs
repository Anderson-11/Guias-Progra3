using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private ProfesorDao _proDao = new ProfesorDao(); // Se crea un endPoint con el metodo http post
                                                         //api/autentitacion
        [HttpPost("autentificacion")]
        //aobtendremos los datos desde el body
        // Profesor tipoDato
        //prof dato asignado desde el body 0 referencias
        public string loginProfesor([FromBody] Profesor prof)
        {
            var prof1 = _proDao.login(prof.Usuario, prof.Pass);
            if (prof1 != null)
            {
                return prof1.Usuario;
            }
            return "✕ Elemento no encontrado ✕";
        }

    }
}
