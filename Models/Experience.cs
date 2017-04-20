using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBlog.Models
{
    [Table("Experiences")]
    public class Experience
    {
        [Key]
        public int ExperienceId { get; set; }
        public string Name { get; set; }
        public string Story { get; set; }
        public int PlaceId { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public virtual Place Place { get; set; }
    }
}
