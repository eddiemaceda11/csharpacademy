using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCSA.OOP.LibraryManagementSystem.models
{
    internal abstract class LibraryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        protected LibraryItem(int id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        public abstract void DisplayDetails();
    }
}
