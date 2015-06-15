using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webscaffolder.Areas.School.Models
{
    public partial class School 
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int Number { get; set; }

        [Required(ErrorMessage="حقل اجباري")]
        [Display(Name="اسم المدرسة")]
        public string Name { get; set; }

        [Display(Name = "اسم مدير/ة المدرسة")]
        public string ManagerName { get; set; }

        [Display(Name = "اسم وكيلـ/ـة  شؤون الطلاب / الطالبات ")]
        public string ManagerAasistName { get; set; }

        [Required(ErrorMessage = "حقل اجباري")]
        public int GenderId { get; set; }

        [ForeignKey("GenderId")]
        public virtual SchoolGender Gender { get; set; }

        public int EductionOfficeId { get; set; }

        [ForeignKey("EductionOfficeId")]
        public virtual EductionOffice EductionOffice { get; set; }


        public int SchoolTypeId { get; set; }

        [ForeignKey("SchoolTypeId")]
        public virtual SchoolType SchoolType { get; set; }

        public int levelId { get; set; }

        [ForeignKey("levelId")]
        public virtual SchoolLevel Level { get; set; }

        public string Email { get; set; }

       

    }
}