using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    [TableName("service")]

    public partial class Service : DBObject
    {
        [TableColumn("idcomputer")]
        public int IdComputer { get; set; }
        [TableColumn("date")]
        public DateTime DateService { get; set; }
        [TableColumn("problem")]
        public string Problem { get; set; }
        [TableColumn("status")]
        public string Status { get; set; }
        [TableColumn("computerid")]
        public int ComputerID { get; set; }
    }
}
