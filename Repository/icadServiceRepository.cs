using User_Management_Application.Database;
using User_Management_Application.Models;
using User_Management_Application.Repositroy;
using User_Management_Application.Repositroy.IRepository;

namespace optimus.bank.service.icad.Repositroy
{
    public class icadServiceRepository : GenericRepository<string>, IICADRepository
    {
        public icadServiceRepository(DataContext context, ILogger logger) : base(context, logger)
        {
        }

    }
}
