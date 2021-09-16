
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Controllers;
using WpfApp1.Models;
using WpfApp1.Tools;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public class ServiceListPageViewModel : NotifyViewModel
    {
        ServiceManager serviceManager;
        public List<Service> Services { get; set; }
        public Service SelectedService { get; set; }
        public Tools.CommandBinding AddService { get; set; }
        public Tools.CommandBinding DeleteService { get; set; }

        public ServiceListPageViewModel()
        {
            //AddService = new CommandBinding(() =>
            //{
            //    if (SelectedService != null)
            //    {
            //        Pager.ChangePageTo(new );
            //    }
            //});
        }
    }
}
