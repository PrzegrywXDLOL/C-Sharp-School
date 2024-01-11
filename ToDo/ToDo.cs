using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public static ObservableCollection<ToDo> toDos = new ObservableCollection<ToDo>();
        public ToDo(string subject, string description) 
        {
            Id = toDos.Count + 1;
            Subject = subject;
            Description = description;
        }
    }
}
