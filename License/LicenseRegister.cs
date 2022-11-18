using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace License
{
    public class LicenseRegister
    {
        public static List<string> GetRegistredCompanyList
        {
            get
            {
                List<string> l = new List<string>();
                l.Add("BEAUTY PRODUCTS LANKA (PVT) LTD");
                l.Add("HIGH PERFORMANCE INDUSTRIAL COATING LANKA(PVT)LTD");
                

                return l;
            }
        }


        public static bool CompanyNameValidation(string RegistredCompanyName)
        {
            foreach (string a in GetRegistredCompanyList)
            {
                if ((RemoveUnwantedChar(a)).ToUpper() == (RemoveUnwantedChar(RegistredCompanyName).ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }
        private const string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string RemoveUnwantedChar(string input)
        {
            input = input.ToUpper();
            StringBuilder builder = new StringBuilder(input.Length);

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]) || allowedCharacters.Contains(input[i]))
                {
                    builder.Append(input[i]);
                }
            }
            return builder.ToString();
        }

    }



}
