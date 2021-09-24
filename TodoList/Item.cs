using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        
        public Item(Guid id, string name, string desc, bool isDone)
        {
            Id = id;
            Name = name;
            Description = desc;
            IsDone = isDone;
        }
    }
}
