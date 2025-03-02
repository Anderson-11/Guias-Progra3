using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reactBackend.Context;
using reactBackend.Models;

namespace reactBackend.Repository
{
    public class AlumnoDao
    {
        #region Contex
        // para hacer cualquier operacion con base de datos debemos llamar al contexto
        // -> La pericion llama al contexto
        // -> contexto verifica el dataset
        // -> El data set mediante su dataTable se actualiza
        // -> el contexto mediante su metodo Save guarda las actualizaciones, delete o insert.
        // -> devuvelve el tipo de correspondinete de error o peticion.
        public RegistroAlumnoContext contexto = new RegistroAlumnoContext();
        #endregion

        #region Select All
        /// <summary>
        /// Se utiliza para selecionar un elemento alumno de la base de datos.
        /// </summary>
        /// <param name="T"> T es un modelo de Sql </param>
        /// <returns> Lista de elementos del modelo que se ingrese</returns>
        public List<Alumno> SelectALL()
        {
            // -> creamos una variable var que es generica
            // -> el contexto tiene referecniada todos los modelos.
            // -> dentro den EF tenemos el metodo modelo. ToList<Modelo>
            var alumno = contexto.Alumnos.ToList<Alumno>();
            return alumno;
        }
        #endregion

        #region Seleccionamos por ID
        public Alumno? GetById(int id)
        {
            var alumno = contexto.Alumnos.Where(x => x.Id == id).FirstOrDefault();
            return alumno == null ? null : alumno;
        }
        #endregion

        #region Insert Alumno
        public bool insertarAlumno(Alumno alumno)
        {
            try
            {
                var alum = new Alumno
                {
                    Direccion = alumno.Direccion,
                    Edad = alumno.Edad,
                    Email = alumno.Email,
                    Dni = alumno.Dni,
                    Nombre = alumno.Nombre
                };
                // añadimos al contexto de (Dataset) que representa la base de datos
                // el metodo add
                contexto.Alumnos.Add(alum);
                // este elemento en si no nos guardara los datos para ello debemos utilizar el metodo Sabe
                contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region Update Alumno
        public bool update(int id, Alumno actualizar)
        {
            try
            {
                var alumnoUpdate = GetById(id);
                if (alumnoUpdate == null)
                {
                    Console.WriteLine("Alumno es null");
                    return false;
                }
                alumnoUpdate.Direccion = actualizar.Direccion;
                alumnoUpdate.Dni = actualizar.Dni;
                alumnoUpdate.Nombre = actualizar.Nombre;
                alumnoUpdate.Email = actualizar.Email;
                alumnoUpdate.Edad = actualizar.Edad;

                contexto.Alumnos.Update(alumnoUpdate);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
        #endregion

        #region delete Alumno
        public bool deleteAlumno(int id)
        {
            var borrar = GetById(id);
            try
            {
                if (borrar == null)
                {
                    return false;
                }
                else
                {
                    contexto.Alumnos.Remove(borrar);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
        #endregion

        #region Left Join
        public List<AlumnoAsignatura> SelectAlumASig()
        {
            var consulta = from a in contexto.Alumnos
                           join m in contexto.Matriculas on a.Id equals m.AlumnoId
                           join asig in contexto.Asignaturas on m.AsignaturaId equals asig.Id
                           select new AlumnoAsignatura
                           {
                               nombreAlumno = a.Nombre,
                               nombreAsignatura = asig.Nombre
                           };
            return consulta.ToList();
        }
        #endregion

        #region leftjoinAlumnoMatriculaMateria
        public List<AlumnoProfesor> alumnoProfesors(string nombreProfesor)
        {
            var listadoALumno = from a in contexto.Alumnos
                                join m in contexto.Matriculas on a.Id equals m.AlumnoId
                                join asig in contexto.Asignaturas on m.AsignaturaId equals asig.Id
                                where asig.Profesor == nombreProfesor
                                select new AlumnoProfesor
                                {
                                    Id = a.Id,
                                    Dni = a.Dni,
                                    Nombre = a.Nombre,
                                    Direccion = a.Direccion,
                                    Edad = a.Edad,
                                    Email = a.Email,
                                    Asignatura = asig.Nombre
                                };

            return listadoALumno.ToList();
        }
        #endregion

        #region SelccionarPorDni
        public Alumno DNIAlumno(Alumno alumno)
        {
            var alumnos = contexto.Alumnos.Where(x => x.Dni == alumno.Dni).FirstOrDefault();
            return alumnos == null ? null : alumnos;
        }
        #endregion

        #region AlumnoMatricula
        public bool InsertarMatricula(Alumno alumno, int idAsing)
        {

            try
            {
                var alumnoDNI = DNIAlumno(alumno);
                //si existe solo lo añadimos pero si no lo debemos de insertar
                if (alumnoDNI == null)
                {
                    insertarAlumno(alumno);
                    // si en null creamos el alumno pero ahora debemos de matricular el alumno con el Dni que corresponda
                    var alumnoInsertado = DNIAlumno(alumno);
                    // ahora debemos crear un objeto matricula para poder hacer la insercion de ambas llaves
                    var unirAlumnoMatricula = matriculaAsignaturaALumno(alumno, idAsing);
                    if (unirAlumnoMatricula == false)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    matriculaAsignaturaALumno(alumnoDNI, idAsing);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Matriucla
        public bool matriculaAsignaturaALumno(Alumno alumno, int idAsignatura)
        {
            try
            {
                Matricula matricula = new Matricula();
                matricula.AlumnoId = alumno.Id;
                matricula.AsignaturaId = idAsignatura;
                contexto.Matriculas.Add(matricula);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Eliminar
        public bool eliminarAlumno(int id)
        {
            try
            {
                // Debemos verificar el id del alumno
                var alumno = contexto.Alumnos.Where(x => x.Id == id).FirstOrDefault();
                if (alumno != null)
                {
                    // matriculaAlumno == idALumno
                    var matriculaA = contexto.Matriculas.Where(x => x.AlumnoId == alumno.Id).ToList();
                    Console.WriteLine("Alumno  encontrado");
                    //Traemos la calificaciones asociadas a esa matricula 
                    foreach (Matricula m in matriculaA)
                    {
                        var calificacion = contexto.Calificacions.Where(x => x.MatriculaId == m.Id).ToList();
                        Console.WriteLine("Matricula encontrada");
                        contexto.Calificacions.RemoveRange(calificacion);

                    }
                    contexto.Matriculas.RemoveRange(matriculaA);
                    contexto.Alumnos.Remove(alumno);
                    contexto.SaveChanges();
                    return true;
                }
                else
                {
                    Console.WriteLine("Alumno no encontrado");
                    return false;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
        }
        #endregion
    }
}

