// See https://aka.ms/new-console-template for more information

using reactBackend.Models;
using reactBackend.Repository;

#region Mostrar
// Abstraccion de un objeto Dao
AlumnoDao alumnoDao = new AlumnoDao();
// Llamamos el metodo que creamos en el Dao
var alumno = alumnoDao.SelectALL();
// Recorremos la lista 
foreach (var item in alumno){
    Console.WriteLine(item.Id + " " + item.Nombre);
}

Console.WriteLine("\n--------------------------------------\n");
#endregion

#region SelectById
var selectById = alumnoDao.GetById(3);
Console.WriteLine(selectById?.Nombre);
#endregion

Console.WriteLine("\n--------------------------------------\n");

/*#region addAlumno
var nuevoAlumno = new Alumno
{
    Direccion = "Chalatenango, Nueva Concepcion",
    Dni = "1121",
    Edad = 20,
    Email = "1121@email",
    Nombre = "Ander JR"
};
var resultado = alumnoDao.insertarAlumno(nuevoAlumno);
Console.WriteLine(resultado);
#endregion*/

/*#region updateAlumno
var nuevoAlumno2 = new Alumno
{
    Direccion = "Chalatenango, Barrio el centro",
    Dni = "1345",
    Edad = 30,
    Email = "11@email",
    Nombre = "Wiliam Quijada"
};
var resultado2 = alumnoDao.update(2, nuevoAlumno2);
Console.WriteLine(resultado2);
#endregion*/

/*#region Borrar alumno
var result = alumnoDao.deleteAlumno(1004);
Console.WriteLine("Se elimino " +result);
#endregion*/

#region alumnoAsignatura desde un JOOIN
var alumAsig = alumnoDao.SelectAlumASig();
foreach (AlumnoAsignatura alumAsig2 in alumAsig)
{
    Console.WriteLine(alumAsig2.nombreAlumno + " Asignaruta que cursa " + alumAsig2.nombreAsignatura);
}
#endregion

