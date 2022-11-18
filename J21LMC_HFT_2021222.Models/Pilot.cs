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
    [Table("pilots")]
    public class Pilot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pilot_Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int dateofbirth { get; set; }     

        [ForeignKey(nameof(Team))]
        public int team_id { get; set; }

        [NotMapped]
        public virtual Team Team { get; set; }

        [JsonIgnore]
        public virtual ICollection<Result> Results { get; set; }
        public Pilot()
        {
            this.Results = new HashSet<Result>();
        }

    }
}
