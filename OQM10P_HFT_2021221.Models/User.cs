using System.ComponentModel.DataAnnotations;
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

        public User()
        {
            Position = UserPositionType.JUNIOR_DEV;
            Sex = UserSexType.MALE;
        }

        public User(string name, string username, string email)
        {
            Name = name;
            Username = username;
            Email = email;
            Position = UserPositionType.JUNIOR_DEV;
            Sex = UserSexType.MALE;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(ID)]
        public int? Id { get; set; }

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
        [Required] 
        public UserPositionType Position { get; set; }

        [Column(SEX)]
        [EnumDataType(typeof(UserSexType))]
        [Required] 
        public UserSexType Sex { get; set; }

        public override string ToString()
        {
            //return $"Id: {Id}, Name: {Name}, Username: {Username}, Email: {Email}, Position: {Position}, Sex: {Sex}";
            return $"Id: {Id}, Name: {Name}, Position: {Position}, Sex: {Sex}, Username: {Username}, Email: {Email}";
        }
    }
}
