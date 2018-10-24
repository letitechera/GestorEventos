using System;
using System.ComponentModel.DataAnnotations;

namespace GestorEventos.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedByName { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
