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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new VMMainPage(Navigation);
        }

        private void Completed_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var toDoItem = checkBox.BindingContext as ToDoModel;
            if (toDoItem != null)
            {
                (BindingContext as VMMainPage).CheckBoxChanged(toDoItem, e.Value);
            }
        }

    }
}