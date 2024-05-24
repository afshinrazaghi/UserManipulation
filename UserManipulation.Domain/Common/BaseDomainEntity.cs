using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManipulation.Domain.Common
{
    public class BaseDomainEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
    }
}
