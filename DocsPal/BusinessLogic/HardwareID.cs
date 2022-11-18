using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Management;

namespace DocWriter.BusinessLogic
{
    class HardwareID
    {
        public static string GET_HARDWAREID = RetuenHardwareID().Result;  
        private static async Task<string> RetuenHardwareID()
        {
          
            StringBuilder sb = new StringBuilder();

            Task task = Task.Run(() =>
                {
                    ManagementObjectSearcher cpu = new ManagementObjectSearcher("select * from Win32_Processor");
                    ManagementObjectCollection cpu_collection = cpu.Get();

                    
                    foreach (ManagementObject obj in cpu_collection)
                    {
                        //string n =StringCipher.Encrypt( obj["ProcessorId"].ToString(),"Cs");

                        sb.Append(obj["ProcessorId"].ToString());
                        break;
                    }

                    

                    //string cpuInfo = string.Empty;
                    //ManagementClass mc = new ManagementClass("win32_processor");
                    //ManagementObjectCollection moc = mc.GetInstances();

                    //foreach (ManagementObject mo in moc)
                    //{
                    //    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    //    break;
                    //}



                    //ManagementObjectSearcher bios = new ManagementObjectSearcher("select * from Win32_BIOS");
                    //ManagementObjectCollection bios_collection = cpu.Get();

                    //foreach (ManagementObject obj in bios_collection)
                    //{
                    //    sb.Append(obj["Version"].ToString().Substring(0,4));
                    //    break;
                    //}

                    //ManagementObjectSearcher hdd = new ManagementObjectSearcher("select * from Win32_Diskdrive");
                    //ManagementObjectCollection hdd_collection = hdd.Get();

                    //foreach (ManagementObject obj in hdd_collection)
                    //{
                    //    sb.Append(obj["Signature"].ToString().Substring(0,4));
                    //    break;
                    //}

                });
            //----- end task
            Task.WaitAll(task);



            return await Task.FromResult(convert2FormatedKey(sb.ToString()));
        }


        public static string convert2FormatedKey(string st_)
        {
            byte[] bytes;
            byte[] hasedbytes;

            bytes = System.Text.Encoding.UTF8.GetBytes(st_.ToString());
            hasedbytes = System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            var key = Convert.ToBase64String(hasedbytes).Substring(24);
            var formatKey_ = System.Text.RegularExpressions.Regex.Replace(key, "[^a-zA-Z^0-9]", "0");
            var finalformatKey_ = System.Text.RegularExpressions.Regex.Replace(formatKey_, @".{5}(?!$)", "$0-");

            return finalformatKey_;

        }


    }
}
