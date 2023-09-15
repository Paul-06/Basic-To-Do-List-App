using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListaTareas.Model
{
    public class ToDoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
