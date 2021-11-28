using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        private const string OWNER_ID = "OWNER_ID";
        private const string CREATED_AT = "CREATED_AT";
        private const string MODIFIED_AT = "MODIFIED_AT";
        private const string CLOSED_AT = "CLOSED_AT";

        public Project()
        {
            CreatedAt = new DateTime();
            ModifiedAt = CreatedAt;
        }

        public Project(string name, int ownerId)
        {
            CreatedAt = new DateTime();
            ModifiedAt = CreatedAt;
            Name = name;
            OwnerId = ownerId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(ID)]
        public int? Id { get; set; }

        [Column(NAME)]
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [Column(GOAL_DESCRIPTION)]
        [MaxLength(5000)]
        public string? GoalDescription { get; set; }

        [Column(ESTIMATED_TIME)]
        public int? EstimatedTime { get; set; }

        //A Required nem működött, ha nem nullable az id, mert akkor 0 értéket tesz bele, emiatt átmegy a validáláson
        [Column(OWNER_ID)]
        [Required]
        [ForeignKey(nameof(Owner))]
        public int? OwnerId { get; set; }

        [Column(CREATED_AT)]
        [Required]
        public DateTime? CreatedAt { get; private set; }
        [Column(MODIFIED_AT)]
        [Required]
        public DateTime? ModifiedAt { get; set; }

        [Column(CLOSED_AT)]
        public DateTime? ClosedAt { get; set; }

        public virtual User Owner { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Issue> Issues { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, EstimatedTime: {EstimatedTime}, IsOpened: {ClosedAt == null}, Owner: {Owner.Name}";
        }
    }
}
