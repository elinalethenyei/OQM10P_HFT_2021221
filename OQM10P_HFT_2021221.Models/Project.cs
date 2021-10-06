using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OQM10P_HFT_2021221.Models
{
    class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string GoalDescription { get; set; }
        public int EstimatedTime { get; set; }
        public bool IsOpened { get; set; }
        public User Owner { get; set; }
        public HashSet<User> Users { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime ClosedAt { get; set; }
    }
}
