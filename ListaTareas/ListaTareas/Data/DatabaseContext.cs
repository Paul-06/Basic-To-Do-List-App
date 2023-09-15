using ListaTareas.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ListaTareas.Data
{
    public class DatabaseContext
    {
        public SQLiteAsyncConnection Connection { get; set; } // Propiedad para la conexion a la base de datos SQLite

        public DatabaseContext(string path) 
        {
            Connection = new SQLiteAsyncConnection(path); // Inicializa una conexion SQLiteAsyncConnection con la ruta especificada
            Connection.CreateTableAsync<ToDoModel>().Wait(); // Crea la tabla de tareas en la base de datos si no existe
        }

        // Add To-Do
        public async Task<int> InsertToDoAsync(ToDoModel model)
        {
            return await Connection.InsertAsync(model); // Inserta un objeto ToDoModel en la tabla de tareas y devuelve el número de filas afectadas (normalmente 1)
        }

        // Get To-Do
        public async Task<List<ToDoModel>> GetToDoAsync()
        {
            return await Connection.Table<ToDoModel>().ToListAsync(); // Recupera todas las tareas de la tabla y las devuelve como una lista de objetos ToDoModel
        }

        // Edit To-Do
        public async Task<int> UpdateToDoAsync(ToDoModel model)
        {
            return await Connection.UpdateAsync(model); // Actualiza un objeto ToDoModel en la tabla de tareas y devuelve el número de filas afectadas (normalmente 1)
        }

        // Método genérico para guardar o actualizar una tarea
        public async Task<int> SaveOrUpdateToDoAsync(ToDoModel model, bool toUpdate)
        {
            // Si toUpdate es verdadero, actualizamos la tarea, de lo contrario, la guardamos.
            return toUpdate ? await UpdateToDoAsync(model) : await InsertToDoAsync(model);
        }


        // Delete To-Do
        public async Task<int> DeletToDoAsync(ToDoModel model)
        {
            return await Connection.DeleteAsync(model); // Borra un objeto ToDoModel de la tabla de tareas y devuelve el número de filas afectadas (normalmente 1)
        }
    }
}
