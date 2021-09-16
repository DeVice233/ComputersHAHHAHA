using System;
using WpfApp1.Models;

namespace WpfApp1.Controllers
{
    public class DB
    {
        static GroupManager groupManager;
        internal static GroupManager GetGroupManager()
        {
            if (groupManager == null)
                groupManager = new GroupManager(
                    new MysqlDB<GroupComputer>("192.168.1.14", "db_floppa", "student", "student"));
            return groupManager;
        }

        static ComputerManager computerManager;
        internal static ComputerManager GetComputerManager()
        {
            if (computerManager == null)
                computerManager = new ComputerManager(
                    new MysqlDB<Computer>("192.168.1.14", "db_floppa", "student", "student"));
            return computerManager;
        }

        static ServiceManager serviceManager;
        internal static ServiceManager GetServiceManager()
        {
            if(serviceManager == null)
            {
                serviceManager = new ServiceManager(
                    new MysqlDB<Service>("192.168.1.14", "db_floppa", "student", "student"));
                
            }
            return serviceManager;
        }
    }
}