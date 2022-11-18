using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocWriter.BusinessLogic
{
    public class CheuqeDtl
    {
        public int SrNo { get; set; }
        public Int16 CompanyId { get; set; }
            public string CompanyName { get; set; }
        public string ChequeNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime ChequeDate { get; set; }
        public bool IsPrintDate { get; set; }
        public bool IsAcPay { get; set; }
        
        public bool IsPrint { get; set; }
        public DateTime PrintOn { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Month1 { get; set; }
        public string Month2 { get; set; }
        public string Year1 { get; set; }
        public string Year2 { get; set; }
        public int PayeeId { get; set; }
            public string PayeeName { get; set; }
        public int BankId { get; set; }
            public string BankName { get; set; }
        public int DrawerId { get; set; }
            public string DrawerName { get; set; }
        public string PayTo1 { get; set; }
        public string PayTo2 { get; set; }
        public string AmtInWord1 { get; set; }
        public string AmtInWord2 { get; set; }
        public string AmtInWord3 { get; set; }
        public string AmtInWord4 { get; set; }
        public string Amt { get; set; }
        public Decimal ChequeAmt { get; set; }
        public bool IsPrintComName { get; set; }
        public string Remarks { get; set; }
        public DateTime MdfOn { get; set; }
 
    }
}
