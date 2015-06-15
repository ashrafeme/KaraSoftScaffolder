using System;
using System.ComponentModel.DataAnnotations;

namespace webscaffolder.Areas.School.Models
{
    [MetadataType(typeof(EductionOfficeMetadata))]
    public partial class EductionOffice
    {
    }

    public partial class EductionOfficeMetadata
    {
        [Required(ErrorMessage = "Please enter : EductionOfficeID")]
        [Display(Name = "EductionOfficeID")]
        public int EductionOfficeID { get; set; }

        [Display(Name = "EductionOfficeName")]
        public string EductionOfficeName { get; set; }

    }
}
