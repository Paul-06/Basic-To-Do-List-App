using ListaTareas.Model;
using ListaTareas.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListaTareas.ViewModel
{
    public class VMAddToDo : VMBase
    {
        #region VARIABLES
        private string _tarea;
        private bool _toEdit = false; // Bandera para verificar si se trata de una edición
        private int _toDoId; // Agregamos un campo para el id de la tarea a editar
        #endregion

        #region CONSTRUCTOR
        // Modificamos el constructor para aceptar un parámetro opcional que es la tarea a editar.
        public VMAddToDo(INavigation navigation, ToDoModel toDoToEdit = null)
        {
            Navigation = navigation;

            // Usamos un operador ternario para asignar el valor de _toEdit y _toDoId según si se pasó una tarea a editar o no.
            _toEdit = toDoToEdit != null;
            _toDoId = _toEdit ? toDoToEdit.Id : 0;
            Tarea = _toEdit ? toDoToEdit.Description : "";
        }
        #endregion

        #region OBJETOS
        public string Tarea
        {
            get { return _tarea; }
            set { SetProperty(ref _tarea, value); }
        }
        #endregion

        #region PROCESOS
        public async Task SaveToDo(bool validate)
        {
            try
            {
                if (validate)
                {
                    // Creamos un objeto ToDoModel con el id y la descripción de la tarea.
                    ToDoModel toDo = new ToDoModel
                    {
                        Id = _toDoId,
                        Description = Tarea
                    };

                    // Usamos un método genérico para guardar o actualizar la tarea en la base de datos, según el valor de _toEdit.
                    int result = await App.Context.SaveOrUpdateToDoAsync(toDo, _toEdit);

                    if (result == 1)
                    {
                        MessagingCenter.Send(this, "SaveTask");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo guardar o actualizar la tarea.", "Aceptar");
                    }
                }
                else
                {
                    await DisplayAlert("Advertencia", "Debe ingresar una tarea.", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrEmpty(_tarea);
        }
        #endregion

        #region COMANDOS
        public ICommand SaveToDoCommand => new Command(async () => await SaveToDo(ValidateSave()));
        #endregion
    }
}
