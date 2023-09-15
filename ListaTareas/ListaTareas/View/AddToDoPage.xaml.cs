using ListaTareas.Model;
using ListaTareas.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListaTareas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddToDoPage : ContentPage
    {
        // Modificamos el constructor para aceptar un parámetro opcional que es la tarea a editar.
        public AddToDoPage(ToDoModel toDoToEdit = null)
        {
            InitializeComponent();

            // Pasamos la tarea a editar al constructor de VMAddToDo.
            BindingContext = new VMAddToDo(Navigation, toDoToEdit);
        }
    }
}