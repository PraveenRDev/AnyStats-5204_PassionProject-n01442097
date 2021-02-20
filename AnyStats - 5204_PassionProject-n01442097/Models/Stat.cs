using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnyStats___5204_PassionProject_n01442097.Models
{
    // class used to generate database table
    public class Stat
    {
        [Key]
        public int StatId { get; set; }
        public string StatName { get; set; }
        public string StatDescription { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public bool isPublic { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("User")]
        public string AuthorId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Coordinate> Coordinates { get; set; }
    }

    // class used to transfer information about a stat
    public class StatDto
    {
        public int StatId { get; set; }

        [Required]
        [DisplayName("Chart Name")]
        public string StatName { get; set; }

        [Required]
        [DisplayName("Description")]
        public string StatDescription { get; set; }

        [Required]
        [DisplayName("X-Axis")]
        public string XAxis { get; set; }

        [Required]
        [DisplayName("Y-Axis")]
        public string YAxis { get; set; }

        [DisplayName("Public?")]
        public bool isPublic { get; set; }
        
        [DisplayName("Author Id")]
        public string AuthorId { get; set; }

        [DisplayName("Author Name")]
        public string AuthorName { get; set; }

        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        public List<string> XValue { get; set; }
        public List<double> YValue { get; set; }

        public ICollection<Coordinate> Coordinates { get; set; }
    }
}