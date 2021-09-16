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
    public class ComputerListViewModel : NotifyViewModel
    {
        public List<Computer> Computers { get; set; }
        public Computer SelectedComputer { get; set; }
        public string SearchText { get; set; } = "";
        public CommandBinding Search { get; set; }
        public CommandBinding Edit { get; set; }
        public CommandBinding Delete { get; set; }

        public ComputerListViewModel()
        {
            Edit = new CommandBinding(()=>
            {
                if (SelectedComputer != null)
                    Pager.ChangePageTo(
                        new EditComputerPage(SelectedComputer));
            });
            Delete = new CommandBinding(()=> {
                if (SelectedComputer != null)
                {
                    DB.GetComputerManager().Delete(SelectedComputer);
                    Search.Execute(null);
                }
            });
            Search = new CommandBinding(()=> {
                var groupsId = DB.GetGroupManager().
                    Search(SearchText).Select(g => g.ID);
                Computers = DB.GetComputerManager().Search(SearchText, groupsId );
                SignalChanged("Computers");
            });
        }
    }
}
