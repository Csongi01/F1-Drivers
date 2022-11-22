using J21LMC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Repository
{
    public class TeamRepository: Repository<Team>, ITeamRepository
    {
        public TeamRepository(F1DbContext ctx) : base(ctx)
        {
        }

        public override Team Read(string id)
        {
            return ctx.Teams.FirstOrDefault(t => t.team_id.ToString() == id);
        }

        public override void Update(Team item)
        {
            var old = Read(item.team_id.ToString());
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
