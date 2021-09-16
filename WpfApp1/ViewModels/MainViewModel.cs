using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Tools;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : NotifyViewModel
    {
        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set {
                currentPage = value;
                SignalChanged("CurrentPage");
            }
        }

        public CommandBinding AddGroup { get; set; }
        public CommandBinding ListGroup { get; set; }
        public CommandBinding AddComp { get; set; }
        public CommandBinding ListComp { get; set; }
        public CommandBinding ListService { get; set; }

        public MainViewModel()
        {
            
            Pager.ChangePage += (o, e) => CurrentPage = e;
            ListGroup = new CommandBinding(()=>
            {
                Pager.ChangePageTo(new GroupListPage());
            });
            AddGroup = new CommandBinding(()=>
            {
                Pager.ChangePageTo(new EditGroupPage());
            });
            AddComp = new CommandBinding(()=> {
                Pager.ChangePageTo(new EditComputerPage());
            });
            ListComp = new CommandBinding(()=> {
                Pager.ChangePageTo(new ComputerListPage());
            });
            ListService = new CommandBinding(() => {
                Pager.ChangePageTo(new ServiceListPage());
            });

        }
    }
}
