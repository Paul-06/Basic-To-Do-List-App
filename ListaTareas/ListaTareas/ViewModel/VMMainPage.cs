using ListaTareas.Model;
using ListaTareas.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ListaTareas.ViewModel
{
    public class VMMainPage : VMBase
    {
        #region VARIABLES
        private string _tarea;
        private ObservableCollection<ToDoModel> _toDoItems;
        #endregion

        #region CONSTRUCTOR
        public VMMainPage(INavigation navigation)
        {
            Navigation = navigation;
            LoadToDoItems();
            MessagingCenter.Subscribe<VMAddToDo>(this, "SaveTask", sender => LoadToDoItems()); // Se suscribe al mensaje que envía VMAddToDo cuando guarda una tarea
        }
        #endregion

        #region OBJETOS
        public string Tarea
        {
            get { return _tarea; }
            set { SetProperty(ref _tarea, value); }
        }

        public ObservableCollection<ToDoModel> ToDoItems
        {
            get { return _toDoItems; }
            set
            {
                _toDoItems = value;
                OnPropertyChanged();
                // Actualiza la propiedad IsListEmpty cada vez que cambia la lista
                OnPropertyChanged(nameof(IsListEmpty));
            }
        }

        // Esta propiedad se vincula al Label
        public bool IsListEmpty => (ToDoItems == null || !ToDoItems.Any());
        #endregion

        #region PROCESOS
        // Modificamos el método para abrir la página de agregar tareas para que acepte un parámetro opcional que es la tarea a editar.
        public async Task OpenAddToDoPage(ToDoModel toDoToEdit = null)
        {
            // Pasamos la tarea a editar al constructor de AddToDoPage.
            await Navigation.PushAsync(new AddToDoPage(toDoToEdit));
        }

        public async void LoadToDoItems()
        {
            try
            {
                var toDoItems = await App.Context.GetToDoAsync(); // Obtiene las tareas desde la base de datos utilizando el contexto de la base de datos
                ToDoItems = new ObservableCollection<ToDoModel>(toDoItems); // Asigna las tareas a ToDoItems
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
            
        }

        public async Task DeleteToDo(ToDoModel toDoToDelete)
        {
            try
            {
                bool confirm = await DisplayAlert("Confirmación", $"¿Desea eliminar la tarea '{toDoToDelete.Description}'?", "Sí", "No");

                if (confirm)
                {
                    int result = await App.Context.DeletToDoAsync(toDoToDelete);

                    if (result == 1)
                    {
                        LoadToDoItems();
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo eliminar la tarea.", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
            }
           
        }
        #endregion

        #region COMANDOS
        public ICommand AddToDoCommand => new Command(async () => await OpenAddToDoPage());
        // Creamos un nuevo comando para el botón de editar que llama al método OpenAddToDoPage con la tarea a editar.
        public ICommand EditToDoCommand => new Command<ToDoModel>(async (toDoToEdit) => await OpenAddToDoPage(toDoToEdit));
        public ICommand DeleteToDoCommand => new Command<ToDoModel>(async (toDoToDelete) => await DeleteToDo(toDoToDelete));
        #endregion
    }
}
