using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Controllers;
using WpfApp1.Models;

namespace WpfApp1.Models
{
    public partial class Service
    {
        public Computer Computer
        {
            get =>
                DB.GetComputerManager().
                    SelectComputerByID(IdComputer);
        }
    }
}
