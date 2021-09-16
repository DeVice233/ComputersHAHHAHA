using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    [TableName("groups")]
    public class GroupComputer : DBObject
    {
        [TableColumn("nameGroup")]
        public string Name { get; set; } = "";
    }
}
