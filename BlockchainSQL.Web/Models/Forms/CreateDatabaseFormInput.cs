using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BlockchainSQL.Web.DataAccess;
using Sphere10.Framework.Web;

namespace BlockchainSQL.Web.Models
{

    public class CreateDatabaseFormInput : FormModelBase {

        public override string FormName => "CreateDatabase";

        [Required]
        [DisplayName("Server")]
        public string Server { get; set; }

        [Required]
        [DisplayName("Database")]
        public string Database { get; set; }

        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }


        [DisplayName("Port")]
        [Integer]
        public int? Port { get; set; }


        [Required]
        [DisplayName("Overwrite Policy")]
        
        public DatabaseGenerationAlreadyExistsPolicy OverwritePolicy { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Config Password")]
        public string ConfigPassword { get; set; }
        
    }
}