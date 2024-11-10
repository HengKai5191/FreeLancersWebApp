using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Freelancer_Management_API.Model
{
    [Table("FreeLancers", Schema ="public")]
    public class MC_FreeLancer
    {
        [Key]
        //[JsonIgnore]
        public int FL_Id { get; set; }
        public string FL_Username { get; set; } = "";
        public string FL_Email { get; set; } = "";
        public string FL_PhoneNumber { get; set; } = "";
        public List<string> FL_Skillsets { get; set; } =new List<string>();
        public List<string> FL_Hobbies { get; set; } = new List<string>();
    }
}
