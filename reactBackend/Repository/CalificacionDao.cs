using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reactBackend.Context;
using reactBackend.Models;

namespace reactBackend.Repository
{
    public class CalificacionDao
    {
        private RegistroAlumnoContext _contexto = new RegistroAlumnoContext();

        public List<Calificacion> seleccion(int matriculaID)
        {
            var matricula = _contexto.Matriculas.Where(s => s.Id == matriculaID).ToList();

            try
            {
                if (matricula != null)
                {
                    var calificacion = _contexto.Calificacions.Where(c => c.Id == matriculaID).ToList();
                    return calificacion;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public bool insertar(Calificacion calificacion)
        {
            try
            {
                if (calificacion == null)
                {
                    return false;
                }

                var addCalificacion = _contexto.Calificacions.Add(calificacion);
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
