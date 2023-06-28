using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("TB_MESSAGE")]
    public class Message
    {
        [Column("MSN_ID")]
        public int Id { get; set; }
        [Column("MSN_TITLE")]
        [StringLength(255)]
        public string Title { get; set; }
        [Column("MSN_ISACTIVATED")]
        public bool IsActivated { get; set; }
        [Column("MSN_REGISTERTIME")]
        public DateTime RegisterDate { get; set; }
        [Column("MSN_ALTTIME")]
        public DateTime AltTime { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
