using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Domain.Common;

namespace UserManipulation.Domain.Entities
{
    [Table("User")]
    public class User : BaseDomainEntity
    {
        [Column("UserName")]
        public string UserName { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("ModifiedAt")]
        public DateTime? ModifiedAt { get; set; }
    }
}
