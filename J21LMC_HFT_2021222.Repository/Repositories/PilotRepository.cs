using J21LMC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Repository
{
    public class PilotRepository : Repository<Pilot>, IPilotRepository
    {
        public PilotRepository(F1DbContext ctx) : base(ctx)
        {
        }
        public override Pilot Read(string id)
        {
            return ctx.Pilots.FirstOrDefault(t => t.pilot_Id.ToString() == id);
        }

        public override void Update(Pilot item)
        {
            var old = Read(item.pilot_Id.ToString());
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
