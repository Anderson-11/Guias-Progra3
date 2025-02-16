using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reactBackend.Context;
using reactBackend.Models;

namespace reactBackend.Repository
{
    public class ProfesorDao
    {
        public RegistroAlumnoContext context = new RegistroAlumnoContext();

        #region GetById
        //Creamos un metodo que recibe 2 parametros
        // usaurio y pass
        //Creamos una expresion lamda que recibe
        //usuario -> usurio ingresado en el Body
        //Pass -> constraseña descrita en el body
        public Profesor login(string usuario, string pass)
        {
            // creamos una fucnion flecha (x)=>{}
            // where define una condicon logica de comparacion
            // p es un objeto temporal que represnta al modelo profesor
            //FirstOrDefault() llama al primaro que cumpla con la condicon de lo contrario de null
            var prof = context.Profesors.Where(p => p.Usuario == usuario && p.Pass == pass).FirstOrDefault();
            //retornamos el resultado de la consulta Lamda
            return prof;
        }
        #endregion

    }
}
