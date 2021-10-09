using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OQM10P_HFT_2021221.Models
{
    [Table("ISSUES")]
    public class Issue
    {
        //column names
        private const string ID = "ID";
        private const string TITLE = "TITLE";
        private const string DESCRIPTION = "DESCRIPTION";
        private const string ESTIMATED_TIME = "ESTIMATED_TIME";
        private const string TIME_SPENT = "TIME_SPENT";
        private const string PRIORITY = "PRIORITY";
        private const string STATUS = "STATUS";
        private const string DUE_DATE = "DUE_DATE";
        private const string USER_ID = "USER_ID";
        private const string PROJECT_ID = "PROJECT_ID";
        private const string TYPE = "TYPE";
        private const string CREATED_AT = "CREATED_AT";
        private const string MODIFIED_AT = "MODIFIED_AT";
        private const string CLOSED_AT = "CLOSED_AT";
        public Issue(DateTime createdAt)
        {
            CreatedAt = new DateTime();
            ModifiedAt = CreatedAt;
            Status = IssueStatus.TODO;
            Type = IssueType.TASK;
            Priority = IssuePriorityType.MEDIUM;
            TimeSpent = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(ID)]
        public long Id { get; set; }
        
        [Column(TITLE)]
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        
        [Column(DESCRIPTION)]
        [MaxLength(5000)]
        public string Description { get; set; }
        
        [Column(ESTIMATED_TIME)]
        public int EstimatedTime { get; set; }
        
        [Column(TIME_SPENT)]
        [Required]
        public int TimeSpent { get; set; }
        
        [Column(PRIORITY)]
        [EnumDataType(typeof(IssuePriorityType))]
        [MaxLength(20)]
        [Required]
        public IssuePriorityType Priority { get; set; }
        
        [Column(STATUS)]
        [EnumDataType(typeof(IssueStatus))]
        [MaxLength(20)]
        [Required]
        public IssueStatus Status { get; set; }
        
        [Column(DUE_DATE)]
        public DateTime DueDate { get; set; }
        
        [Column(TYPE)]
        [EnumDataType(typeof(IssueType))]
        [MaxLength(20)]
        [Required]
        public IssueType Type { get; set; }
        
        [Column(CREATED_AT)]
        [Required]
        public DateTime CreatedAt { get; private set; }
        
        [Column(MODIFIED_AT)]
        [Required]
        public DateTime ModifiedAt { get; set; }
        
        [Column(CLOSED_AT)]
        public DateTime ClosedAt { get; set; }

        [Column(PROJECT_ID)]
        [Required]
        public long ProjectId { get; set; }

        [Column(USER_ID)]
        [Required]
        public long UserId { get; set; }

        public Project Project { get; set; }
       
        public User User { get; set; }

    }
}
