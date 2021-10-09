﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OQM10P_HFT_2021221.Models
{
    [Table("USERS")]
    public class User
    {

        //column names
        private const string ID = "ID";
        private const string NAME = "NAME";
        private const string USERNAME = "USERNAME";
        private const string EMAIL = "EMAIL";
        private const string POSITION = "POSITION";
        private const string SEX = "SEX";

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(ID)] 
        public long Id { get; set; }
        
        [Column(NAME)]
        [MaxLength(255)]
        [Required] 
        public string Name { get; set; }
        
        [Column(USERNAME)]
        [MaxLength(255)]
        [Required] 
        public string Username { get; set; }

        [Column(EMAIL)]
        [MaxLength(255)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Column(POSITION)]
        [EnumDataType(typeof(UserPositionType))]
        [MaxLength(20)]
        [Required] 
        public UserPositionType Position { get; set; }

        [Column(SEX)]
        [EnumDataType(typeof(UserSexType))]
        [MaxLength(6)]
        [Required] 
        public UserSexType Sex { get; set; }
    }
}
