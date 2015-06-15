using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webscaffolder.Areas.School.Models
{
    public class SchoolLevel 
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int LevelID { get; set; }

        public string LevelName { get; set; }


        public virtual IEnumerable<School> Schools { get; set; }
    }
}