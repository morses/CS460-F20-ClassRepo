using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvites.Models
{
    public partial class GuestResponses
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string FullName { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Phone { get; set; }
        [Column(TypeName = "BOOLEAN")]
        public bool? WillAttend { get; set; }
        public long? Guests { get; set; }
    }
}
