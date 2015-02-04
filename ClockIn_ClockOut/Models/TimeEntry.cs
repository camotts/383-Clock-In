using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockIn_ClockOut.Models
{
    public class TimeEntry
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime TimeIn { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime TimeOut { get; set; }
    }
}
