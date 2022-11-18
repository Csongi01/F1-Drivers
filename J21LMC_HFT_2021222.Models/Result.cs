using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Models
{
    [Table("results")]
    public class Result 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int result_Id { get; set; }

        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Pilot))]
        public int pilot_Id { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Race))]
        public string race_code { get; set; }
        public int? start_pos { get; set; }
        public int? finish_pos { get; set; }

        [NotMapped]
        public virtual Pilot Pilot { get; set; }
        [NotMapped]
        public virtual Race Race { get; set; }

    }
}
