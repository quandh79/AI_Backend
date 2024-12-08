using Repository.BCC01_EF;
using Repository.Repositories.Abtracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories
{
    public class TeamRepository : BaseRepositorySql<BCC01_Teams>, ITeamRepository
    {
    }
}
