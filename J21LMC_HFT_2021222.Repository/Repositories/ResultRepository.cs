using J21LMC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Repository
{
    public class ResultRepository: Repository<Result>, IResultRepository
    {
        public ResultRepository(F1DbContext ctx) : base(ctx)
        {
        }

        public override Result Read(string id)
        {
            return ctx.Results.FirstOrDefault(t => t.result_Id.ToString() == id);
        }
        
        public override void Update(Result item)
        {
            var old = Read(item.result_Id.ToString());
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
