using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models2
{
    public class Contract
    {
        public int ContractId { get; set; }
        public string ContractName { get; set; }
        public string ContractEmail { get; set; }
        public string ContractPhone { get; set; }
        public string ContractIBAN { get; set; }
        public string ContractStartDate { get; set; }
        public string ContractDueDate { get; set; }
    }
}
