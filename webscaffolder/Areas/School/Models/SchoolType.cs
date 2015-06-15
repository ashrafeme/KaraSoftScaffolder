using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webscaffolder.Areas.School.Models
{
    public class SchoolType 
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int SchoolTypeID { get; set; }

        public string SchoolTypeName { get; set; }

        public virtual IEnumerable<School> Schools { get; set; }
    }
}