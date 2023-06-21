using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    internal class Message
    {
        [Column("MSN_ID")]
        public int id { get; set; }
        [Column("MSN_TITLE")]
        [StringLength(255)]
        public string title { get; set; }
        [Column("MSN_ISACTIVATED")]
        public bool isActivated { get; set; }
        [Column("MSN_REGISTERTIME")]
        public DateTime registerDate { get; set; }
        [Column("MSN_ALTTIME")]
        public DateTime altTime { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public int userId { get; set; }
        public ApplicationUser applicationUser { get; set; }
    }
}
