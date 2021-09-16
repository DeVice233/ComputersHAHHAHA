using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Controllers
{
    public class ComputerManager: Manager<Computer>
    {
        public ComputerManager(IDB<Computer> iDBComputer) 
            : base(iDBComputer)
        {
        }

        public List<Computer> GetComputersByGroup(GroupComputer group)
        {
            return db.GetAll().
                Where(s => s.GroupID == group.ID).ToList();
        }

        public Computer SelectComputerByID(int computerId)
        {
            Computer computer = db.GetAll().
                FirstOrDefault(g => g.ID == computerId);
            if (computer == null)
                computer = new Computer
                {
                    INumber = "Компьютер не найден"
                };
            return computer;
        }

        internal List<Computer> Search(string searchText, IEnumerable<int> groupsId)
        {
            List<Computer> result = new List<Computer>();
            var all = db.GetAll();
            foreach (var groupId in groupsId)
                result.AddRange(all.Where(c=>c.GroupID == groupId));
            var filter = all.Where(c =>
                c.DomainName.Contains(searchText) ||
                c.INumber.Contains(searchText)    ||
                c.IPAddress.Contains(searchText)  ||
                c.MACAddress.Contains(searchText));
            foreach (var comp in filter)
                if (!result.Contains(comp))
                    result.Add(comp);
            return result;
        }
    }
}
