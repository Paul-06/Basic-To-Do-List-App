using ListaTareas.Data;
using ListaTareas.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListaTareas
{
    public partial class App : Application
    {

        public static DatabaseContext Context { get; set; }

        public App()
        {
            InitializeComponent();
            InitializeDatabase();
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#363636")
            };
        }

        private void InitializeDatabase()
        {
            var folderApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = System.IO.Path.Combine(folderApp, "ToDoList.db3");
            Context = new DatabaseContext(dbPath);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
