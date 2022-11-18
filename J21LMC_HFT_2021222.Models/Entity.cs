using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Models
{
    public abstract class Entity
    {
        [Key]        
        public virtual int Id { get; set; }
    }
}
