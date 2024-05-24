using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManipulation.Domain.Common;

namespace UserManipulation.Domain.Entities
{
    [Table("UserToken")]
    public class UserToken : BaseDomainEntity
    {
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("AccessToken")]
        public string AccessToken { get; set; }
        [Column("RefreshToken")]
        public string RefreshToken { get; set; }
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("AccessTokenExpiration")]
        public DateTime AccessTokenExpiration { get; set; }
        [Column("RefreshTokenExpiration")]
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
