using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OQM10P_HFT_2021221.Models
{
    [Table("PROJECTS")]
    public class Project
    {
        //column names
        private const string ID = "ID";
        private const string NAME = "NAME";
        private const string GOAL_DESCRIPTION = "GOAL_DESCRIPTION";
        private const string ESTIMATED_TIME = "ESTIMATED_TIME";
        private const string IS_OPENED = "IS_OPENED";
        private const string OWNER_ID = "OWNER_ID";
        private const string CREATED_AT = "CREATED_AT";
        private const string MODIFIED_AT = "MODIFIED_AT";
        private const string CLOSED_AT = "CLOSED_AT";

        public Project(string name, long ownerId)
        {
            CreatedAt = new DateTime();
            ModifiedAt = CreatedAt;
            Name = name;
            OwnerId = ownerId;
            IsOpened = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(ID)]
        public long Id { get; set; }

        [Column(NAME)]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Column(GOAL_DESCRIPTION)]
        [MaxLength(5000)]
        public string GoalDescription { get; set; }

        [Column(ESTIMATED_TIME)]
        public int EstimatedTime { get; set; }

        [Column(IS_OPENED)]
        [Required] 
        public bool IsOpened { get; set; }

        [Column(OWNER_ID)]
        [Required] 
        public long OwnerId { get; set; }

        [Column(CREATED_AT)]
        [Required] 
        public DateTime CreatedAt { get; private set; }
        [Column(MODIFIED_AT)]
        [Required] 
        public DateTime ModifiedAt { get; set; }
        
        [Column(CLOSED_AT)]
        public DateTime ClosedAt { get; set; }

        [NotMapped]
        public virtual ICollection<User> Users { get; set; }

        public virtual User Owner { get; set; }
    }
}
