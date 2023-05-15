using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLESS.TransportSystem.Models
{
    public class TransportCard
    {
        public int? Id { get; set; }
        public decimal? LoadAmount { get; set; }
        public decimal? AddLoadAmount { get; set; }
        public bool? IsDiscounted { get; set; }
        public bool? IsInside { get; set; }
        public string? SeniorCitizenId { get; set; }
        public string? PwdId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? EntryCount { get; set; }
        public decimal? CustomerMoney { get; set; }
    }
}
