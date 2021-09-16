using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Controllers
{
    public class ServiceManager : Manager<Service>
    {
        public ServiceManager(IDB<Service> iDBService) : base(iDBService)
        {

        }
        public List<Service> GetServiceByComputer(Computer computer)
        {
            return db.GetAll().
                Where(s => s.IdComputer == computer.ID).ToList();
        }

        public List<Service> GetAllServices() =>
           db.GetAll().ToList();

        public Service SelectServiceByID(int serviceid)
        {
            Service service = db.GetAll().
                FirstOrDefault(g => g.ID == serviceid);
            if (service == null)
                service = new Service
                {
                    Status = "Не найден статус"
                };
            return service;
        }
    }
}
