using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocWriter.BusinessLogic
{
    public class CompanyInfo
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int IsActive { get; set; }
        public DateTime MdfOn { get; set; }
        public string ActivateCode { get; set; }
        public int IsProductActivated { get; set; }
        

    }
}
