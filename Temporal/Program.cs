// See https://aka.ms/new-console-template for more information

using reactBackend.Repository;

// Abstraccion de un objeto Dao
AlumnoDao alumnoDao = new AlumnoDao();
// Llamamos el metodo que creamos en el Dao
var alumno = alumnoDao.SelectALL();
// Recorremos la lista 
foreach (var item in alumno){
    Console.WriteLine(item.Nombre);
}
Console.WriteLine("\n--------------------------------------\n");
#region SelectById
var selectById = alumnoDao.GetById(3);
Console.WriteLine(selectById?.Nombre);
#endregion