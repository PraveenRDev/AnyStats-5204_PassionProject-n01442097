using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnyStats___5204_PassionProject_n01442097.Models
{
    public class Coordinate
    {
        [Key]
        public int CoordinateId { get; set; }
        public string XValue { get; set; }
        public double YValue { get; set; }

        [ForeignKey("Statistic")]
        public int StatId { get; set; }
        public virtual Stat Statistic { get; set; }

        public class CoordinateDto
        {
            public List<string> XValues { get; set; }
            public List<double> YValues { get; set; }
            public int StatId { get; set; }
        }
    }
}