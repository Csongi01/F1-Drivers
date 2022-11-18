using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace J21LMC_HFT_2021222.Models
{
    [Table("races")]
    public class Race 
    {
        [Key]
        [Required]
        public string race_code { get; set; }        
        public DateTime date { get; set; }
        [Required]
        public string racename { get; set; }
        [Required]
        public string location { get; set; }
        public int? laps { get; set; }
        public int? length { get; set; }

        [JsonIgnore]
        public virtual ICollection<Result> Results { get; set; }
        public Race()
        {
            this.Results = new HashSet<Result>();
        }
    }
}
