using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private AlumnoDao _dao = new AlumnoDao();

        #region endPonitAlumnoProfesor
        //nombre al cual debe acceder la ruta del endpoint
        [HttpGet("alumnoProfesor")]
        public List<AlumnoProfesor> GetAlumnoProfesor(string usuario)
        {
            return _dao.alumnoProfesors(usuario);
        }
        #endregion

        #region SelectByID 
        [HttpGet("alumno")]
        public Alumno seletById(int id)
        {
            var alumno = _dao.GetById(id);
            return alumno;
        }
        #endregion

        #region ActualizarDatos
        //Metodo put para actualizar 
        [HttpPut("alumno")]
        // Puede tener el mismo nombre que el endpoint anterior ya que es un metodo diferente el que se esta utilizando
        public bool actualizarAlumno([FromBody] Alumno alumno)
        {
            // [FromBody] indica que se obtendra desde el navagador el objeto
            // Alumno es el objeto
            // alumno es el nombre de la instancia de ese objeto
            return _dao.update(alumno.Id, alumno);
        }

        #endregion

        #region AlumnoMatricula
        [HttpPost("alumno")]
        public bool insertarMatricula([FromBody] Alumno alumno, int idAsignatura)
        {
            return _dao.InsertarMatricula(alumno, idAsignatura);
        }
        #endregion

        #region Eliminar
        [HttpDelete("alumno")]
        public bool eliminarALumno(int id)
        {
            return _dao.eliminarAlumno(id);
        }
        #endregion
    }
}
