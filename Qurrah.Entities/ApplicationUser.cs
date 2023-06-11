using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="الحقل مطلوب")]
        [DisplayName("الاسم بالكامل")]
        public string FullName { get; set; }
        
        [DisplayName("المدينة")]
        public string? City { get; set; }
        
        [DisplayName("الحي")]
        public string? State { get; set; }
       
        [DisplayName("الرقم البريدي")]
        public string? PostalCode { get; set; }
        
        [DisplayName("العنوان بالتفصيل")]
        public string? StreetAddress { get; set; }

        //[ForeignKey("Company")]
        //[DisplayName("الشركة")]
        //public int? CompanyId { get; set; }
        
        //[ValidateNever]
        //public Company Company { get; set; }
    }
}