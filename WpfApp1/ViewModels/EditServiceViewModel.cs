using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Controllers;
using WpfApp1.Models;
using WpfApp1.Tools;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public class EditServiceViewModel : NotifyViewModel
    {
        public Computer SelectedComputer { get; set; }
        public Service SelectedService { get; set; }
        public List<Service> Services { get; set; }
        public List<Computer> Computers { get; set; }
        ServiceManager serviceManager;
        ComputerManager computerManager;
        public IEnumerable<string> StatusList { get; set; }
        public CommandBinding Save { get; set; }

        public EditServiceViewModel()
        {
            Services = DB.GetServiceManager().GetAllServices();
            SelectedService = new Service();
            InitSave();
        }

        public EditServiceViewModel(Computer edit)
        {
            Services = DB.GetServiceManager().GetAllServices();
            SelectedService = new Service();
            SelectedComputer = edit;
            InitSave();
        }

        private void InitSave()
        {
            Save = new CommandBinding(() => {
                SelectedService.IdComputer = SelectedComputer?.ID ?? 0;
                if (SelectedService.ID == 0)
                    DB.GetServiceManager().
                        Add(SelectedService);
                else
                    DB.GetServiceManager().Update(SelectedService);
                Pager.ChangePageTo(new ServiceListPage());
            });
        }
    }
}
