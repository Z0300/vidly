using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.App.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }
        [Display(Name = "Membership Type")]
        public int MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsAMember]
        public DateTime? Birthdate { get; set; }
    }
}