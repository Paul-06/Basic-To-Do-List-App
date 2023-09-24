using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ListaTareas.Model
{
    // La clase ToDoModel implementa INotifyPropertyChanged para notificar a la vista cuando cambia una propiedad.
    public class ToDoModel : INotifyPropertyChanged
    {
        // Id es la clave primaria para la base de datos.
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Description y IsCompleted son propiedades con notificación de cambio.
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(); // Notifica a la vista del cambio
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                OnPropertyChanged(); // Notifica a la vista del cambio
            }
        }

        // Constructor que establece IsCompleted en false por defecto.
        public ToDoModel()
        {
            IsCompleted = false;
        }

        // Evento que se dispara cuando una propiedad cambia.
        public event PropertyChangedEventHandler PropertyChanged;

        // Método para disparar el evento PropertyChanged.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
