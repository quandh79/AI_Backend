using Repository.BCC01_EF;
using Repository.EF;

namespace Repository.Repositories
{
    public class MapProfileUserRepository : BaseRepositorySql<BCC01_MapProfileUser>, IMapProfileUserRepository
    {
        public MapProfileUserRepository() : base() { }

    }
}