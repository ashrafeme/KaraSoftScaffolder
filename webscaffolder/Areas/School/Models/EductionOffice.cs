using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace webscaffolder.Areas.School.Models
{
    public partial class EductionOffice 
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int EductionOfficeID { get; set; }

        public string EductionOfficeName { get; set; }
        
        public virtual IEnumerable<School> Schools { get; set; }
    }
}