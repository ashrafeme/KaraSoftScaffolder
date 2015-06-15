using System;
using System.ComponentModel.DataAnnotations;

namespace webscaffolder.Areas.School.Models
{
    [MetadataType(typeof(SchoolMetadata))]
    public partial class School
    {
    }

    public partial class SchoolMetadata
    {
        [Display(Name = "EductionOffice")]
        public EductionOffice EductionOffice { get; set; }

        [Display(Name = "Gender")]
        public SchoolGender Gender { get; set; }

        [Display(Name = "Level")]
        public SchoolLevel Level { get; set; }

        [Display(Name = "SchoolType")]
        public SchoolType SchoolType { get; set; }

        [Required(ErrorMessage = "Please enter : Number")]
        [Display(Name = "Number")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Please enter : Name")]
        [Display(Name = "Name")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Display(Name = "ManagerName")]
        [MaxLength(255)]
        public string ManagerName { get; set; }

        [Display(Name = "ManagerAasistName")]
        [MaxLength(255)]
        public string ManagerAasistName { get; set; }

        [Display(Name = "GenderId")]
        public int GenderId { get; set; }

        [Display(Name = "EductionOfficeId")]
        public int EductionOfficeId { get; set; }

        [Display(Name = "SchoolTypeId")]
        public int SchoolTypeId { get; set; }

        [Display(Name = "levelId")]
        public int levelId { get; set; }

        [Required(ErrorMessage = "Please enter : Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}
