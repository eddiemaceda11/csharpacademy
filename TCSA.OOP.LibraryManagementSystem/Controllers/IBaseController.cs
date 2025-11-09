using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCSA.OOP.LibraryManagementSystem.Controllers
{
    internal interface IBaseController
    {
        void ViewItems();
        void AddItem();
        void DeleteItem();
    }
}
