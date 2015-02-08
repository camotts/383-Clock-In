using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockIn_ClockOut.Models
{
    public class TimeEntry
    {
        public int ID { get; set; }
        
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        
        [Display(Name= "Time In")]
        [Column(TypeName = "DateTime2")]
        public DateTime TimeIn { get; set; }
        
        [Display(Name = "Time Out")]
        [Column(TypeName = "DateTime2")]
        public DateTime TimeOut { get; set; }

        [Display(Name = "Work Hours")]
        public int timeMinutes { get; set; }
    }
}
