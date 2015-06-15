using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webscaffolder.Areas.School.Models
{
    public class SchoolGender 
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int GenderID { get; set; }

        public string GenderName { get; set; }

        public virtual IEnumerable<School> Schools { get; set; }
    }
}