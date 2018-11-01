using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

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
        public DateTime? ModifiedDate { get; set; }

        [NotMapped]
        public string PrettyCreatedDate
        {
            get
            {
                return CreatedDate.ToLocalTime().ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);
            }
        }

        [NotMapped]
        public string PrettyModifiedDate
        {
            get
            {
                if (ModifiedDate.HasValue)
                    return ModifiedDate.Value.ToLocalTime().ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);
                else
                    return "N/A";
            }
        }
    }
}
