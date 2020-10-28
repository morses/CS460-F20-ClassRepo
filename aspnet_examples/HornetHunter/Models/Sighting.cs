using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HornetHunter.Models
{
    public partial class Sighting
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(64)]
        public string FullName { get; set; }
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SightingTime { get; set; }
        [StringLength(512)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReportTime { get; set; }
        [Column("IPAddress")]
        [StringLength(25)]
        public string Ipaddress { get; set; }
    }
}
