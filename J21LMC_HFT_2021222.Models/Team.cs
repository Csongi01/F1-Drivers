using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Models
{
    [Table("teames")]
    public class Team 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int team_id { get; set; }
        [Required]
        public string team_name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Pilot> Pilots { get; set; }
        public Team()
        {
            this.Pilots = new HashSet<Pilot>();
        }

    }
}
