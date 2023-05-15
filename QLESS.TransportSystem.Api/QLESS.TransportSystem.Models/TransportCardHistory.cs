using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLESS.TransportSystem.Models
{
    public class TransportCardHistory
    {
        public int? Id { get; set; }
        public int? TransportCardId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
