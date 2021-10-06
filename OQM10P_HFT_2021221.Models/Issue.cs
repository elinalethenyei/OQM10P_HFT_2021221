using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OQM10P_HFT_2021221.Models
{
    class Issue
    {
        public Issue(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int EstimatedTime { get; set; }
        public int TimeSpent { get; set; }
        public IssuePriorityType Priority { get; set; }
        public IssueStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }
        public IssueType type { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime ClosedAt { get; set; }

    }
}
