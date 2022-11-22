using J21LMC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Repository
{
    public class RaceRepository: Repository<Race>, IRaceRepository
    {
        public RaceRepository(F1DbContext ctx) : base(ctx)
        {
        }
        public override Race Read(string id)
        {
            return ctx.Races.FirstOrDefault(t => t.race_code == id);
        }

        public override void Update(Race item)
        {
            var old = Read(item.race_code);
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
