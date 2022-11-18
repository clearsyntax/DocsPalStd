using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using DocWriter.BusinessLogic;




namespace DocWriter
{
    public class BL
    {

        //public static HashSet<HelpItemList> HelpItemList__;
        //string ConStr = "Server=server;Database=kIOSkPOS;Uid=sa;Pwd=sa;Connect Timeout=30";
        //static string ConStr = "Server=server;Database=kIOSkPOS;Uid=sa;Pwd=sa;";
        //public static string ConStr = "server=server;database=KioskPOS; uid=sa;pwd=sa;providerName=System.Data.SqlClient";

        //public static string serverName = System.Configuration.ConfigurationSettings.AppSettings["serverName"].ToString();
        //public static string serverNameGlobel = "220.247.241.228";
        //public static string ConStr = System.Configuration.ConfigurationSettings.AppSettings["connectionStrings"].ToString();

        //----------- ****--------------- HEad Office
        //public static string ConStr = "server=server;database=KioskPos;uid=sa1;pwd=Admin123";
        //public static string ConStr = "server=server\\mssqlserver2012;database=KioskPos;uid=sa2;pwd=sa";
        //public static string ConStr = "server=server\\sqlexpress2012;database=KioskPos;uid=sa3;pwd=sa";
        //----------- ****--------------- Kiosk Outlets
        //public static string ConStr = @"Data Source=.\SQLExpress;Integrated Security=true;
        //                        AttachDbFilename=E:\KioskPOS\DB\KioskPOS.mdf;User Instance=true;";

        //public static string ConStr = "server="+ serverName +";database=KioskPos; uid=sa1;pwd=Admin123";

        public static string _dbpath = getAppConfigDBDirectoryName();  // if requrired to set db to curretn directory put empty, if need to set antother location set directory path eg. server\\db
        

        ///*
        //public static string ConStr = "server="+ serverName +";database=KioskPOS; uid=sa1;pwd=Admin123";
       
        //public static string ConStr = "Data Source=" + GetDBDirectory() + "\\ChequeWriter.db;Version=3;Page Size=1024;Password=123;";
        //public static string ConStr = "Data Source=\\\\192.168.1.97\\DocWrDB\\ChequeWriter.db;Version=3;Password=123;";
        //public static string ConStr = "Data Source=" + GetDBDirectory() + "\\ChequeWriter.db;Version=3;Password=123;Pooling=True;Max Pool Size=100;";

        //public static string ConStr  = "Data Source=+{0}+\\ChequeWriter.db;Version=3;Password=123;Pooling=True;Max Pool Size=100;";
        //public static string ConStr = "Data Source=\\\\nas\\db\\ChequeWriter.db;Version=3;Password=123;Pooling=True;Max Pool Size=100;";
        
        
        //public static string ConStr4Rple = "server=" + serverName + ";database=KioskPOS; uid=sa1;pwd=Admin123";
        //*/
        
        /*
        // for testing connect to rec1-pc
        public static string ConStr = "server=REC1-PC\\MSSQLSERVER1;database=KioskPOS;uid=sa;pwd=sa";
        public static string ConStr4Rple = "server=REC1-PC\\MSSQLSERVER1;database=KioskPOS;uid=sa;pwd=sa";
        */

        //public static string ConStr = @"server=.;database=KioskPOS;uid=sa1;pwd=Admin123;";

        //----------- ****---------------

        //public static string ConStr = "server=.;database=KioskPos; uid=kioskuser;pwd=Admin123";
        //public static string ConStr = "server=server;database=KioskPos; uid=kioskuser;pwd=Admin123";
        //public static string ConStr = "server=srv2012.lankahost.net;database=KioskPos; uid=kioskuser;pwd=revlon2008";

        //Globle connection string
        //public static string ConStrGlob = "server=220.247.241.228,1433\\MSSQLSERVER2012;database=KioskPOS;uid=sa1;pwd=Admin123;";
        //public static string ConStrGlob = "server=192.168.1.100,1433\\MSSQLSERVER2012;database=KioskPOS;uid=sa1;pwd=Admin123;";

        //public static string ConStrGlob = @"Data Source=220.247.241.228,1433;Network Library=DBMSSOCN;
        ////                       Initial Catalog=MSSQLSERVER2012;User ID=sa2;Password=sa;";

        public static string OSDefultPrinter;
        public static string AppDefultPrinter;
        public static string CompanyName;
        public static Int16 CompanyId=0;
        public static string DefaultDrawer;
        public static string DefaultBank;
        public static string DbDirectory;

        //public static string fontFamily;
        //public static string fontStyle;
        //public static int fontSize = 11;



        public static string CurrenDirectory = Directory.GetCurrentDirectory();

        private static string getConStr()
        {
            return "Data Source=" + GetDBDirectory() + "\\ChequeWriter.db;Version=3;Password=Cpk123;Pooling=True;Max Pool Size=100;";
        }
 

        public BL()
        {
            

            CompanyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString();  
            //AppDefultPrinter = (System.IO.File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\DefaultPrinter.txt")).Trim();
            AppDefultPrinter = System.Configuration.ConfigurationManager.AppSettings["AppDefaultPrinter"].ToString();
            DefaultDrawer = System.Configuration.ConfigurationManager.AppSettings["DefaultDrawer"].ToString();
            DefaultBank = System.Configuration.ConfigurationManager.AppSettings["DefaultBank"].ToString();


            //CompanyId = getCompanyId(CompanyName);

            //fontFamily = System.Configuration.ConfigurationManager.AppSettings["FontFamily"].ToString();
            //fontStyle = System.Configuration.ConfigurationManager.AppSettings["FontStyle"].ToString();
            //var fs_=System.Configuration.ConfigurationManager.AppSettings["FontSize"].ToString().Trim();
            //if (fs_ !="")
            //{
            //    fontSize = Convert.ToInt16(IsNumeric(fs_)==true? fs_: "11");
            //}
            
        }


        public static string GetDBDirectory()
        {
            try
            {
                if (_dbpath == "" || _dbpath == null)
                {
                    _dbpath = Directory.GetCurrentDirectory(); //CurrenDirectory; //Directory.GetCurrentDirectory();
                }
                return _dbpath;
            }
            catch (Exception ex) { return CurrenDirectory; }
        }

        public static string getAppConfigDBDirectoryName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["DbDirectory"].ToString();
        }


        public static String Justify(String s, Int32 count)
        {
            if (count <= 0)
                return s;

            Int32 middle = s.Length / 2;
            IDictionary<Int32, Int32> spaceOffsetsToParts = new Dictionary<Int32, Int32>();
            String[] parts = s.Split(' ');
            for (Int32 partIndex = 0, offset = 0; partIndex < parts.Length; partIndex++)
            {
                spaceOffsetsToParts.Add(offset, partIndex);
                offset += parts[partIndex].Length + 1; // +1 to count space that was removed by Split
            }
            foreach (var pair in spaceOffsetsToParts.OrderBy(entry => Math.Abs(middle - entry.Key)))
            {
                count--;
                if (count < 0)
                    break;
                parts[pair.Value] += ' ';
            }
            return String.Join(" ", parts);
        }

       

        public List<PayeeMst> Payees
        {
            get
            {
                List<PayeeMst> l_ = new List<PayeeMst>();

                using (SQLiteConnection con = new SQLiteConnection(getConStr()))
                {

                    SQLiteCommand cmd = new SQLiteCommand("select * from PayeeMst where IsDeleted=0 order by PayeeName", con);
                    con.Open();
                    //-------- for change the DB password only
                    //con.ChangePassword("Cpk123"); 
                    //--------------
                    SQLiteDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        PayeeMst  k = new PayeeMst();
                        k.PayeeId = Convert.ToInt16(r["PayeeId"] == DBNull.Value ? "0" : r["PayeeId"].ToString());
                        k.PayeeName = r["PayeeName"] == DBNull.Value ? "" : r["PayeeName"].ToString();
                        l_.Add(k);
                    }
                    r.Close();

                }

                return l_;
            }
        }
        public List<Drawer> Drawer
        {
            get
            {
                List<Drawer> l_ = new List<Drawer>();

                using (SQLiteConnection con = new SQLiteConnection(getConStr()))
                {

                    SQLiteCommand cmd = new SQLiteCommand("select * from DrawerInfo where IsDeleted=0 order by DrawerName", con);
                    con.Open();
                    SQLiteDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        Drawer k = new Drawer();
                        k.DrawerId = Convert.ToInt16(r["DrawerId"] == DBNull.Value ? "0" : r["DrawerId"].ToString());
                        k.DrawerName = r["DrawerName"] == DBNull.Value ? "" : r["DrawerName"].ToString();
                        l_.Add(k);
                    }
                    r.Close();

                }

                return l_;
            }
        }
        public CompanyInfo CompanyInfo
        {
            get
            {
                CompanyInfo  l_ = new CompanyInfo();

                using (SQLiteConnection con = new SQLiteConnection(getConStr()))
                {

                    SQLiteCommand cmd = new SQLiteCommand("select * from CompanyInfo where IsActive=1 order by MdfOn desc", con);
                    con.Open();
                    SQLiteDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {

                        l_.CompanyId = Convert.ToInt16(r["CompanyId"] == DBNull.Value ? "0" : r["CompanyId"].ToString());
                        l_.CompanyName = r["CompanyName"] == DBNull.Value ? "" : r["CompanyName"].ToString();
                        l_.IsActive = Convert.ToInt16(r["IsActive"] == DBNull.Value ? "0" : r["IsActive"].ToString());
                        l_.MdfOn = Convert.ToDateTime(r["MdfOn"] == DBNull.Value ? "2000-1-1" : r["MdfOn"].ToString());
                        l_.ActivateCode = r["ActivateCode"] == DBNull.Value ? "" : r["ActivateCode"].ToString();
                        l_.IsProductActivated = Convert.ToInt16(r["IsProductActivated"] == DBNull.Value ? "0" : r["IsProductActivated"].ToString());

                        
                    }
                    r.Close();

                }

                return l_;
            }
        }

        public static bool CompanyActivationRemovingLicense(Int16 CompanyId_, string ActivateCode,bool ActiveRemove_)
        {
            try
            {
                //System.Windows.MessageBox.Show("Product code not valied  Company Id:" + CompanyId_.ToString() + " / Prod Code:" + ActivateCode);
                if ((IsProductCodeGenuine(ActivateCode) == true) && ActivateCode != "")
                {
                    
                    SQLiteCommand cmd;
                    using (SQLiteConnection con = new SQLiteConnection(getConStr()))
                    {
                        con.Open();

                        cmd = new SQLiteCommand("update CompanyInfo set ActivateCode=@ActivateCode,IsProductActivated=@IsProductActivated where CompanyId=@CompanyId", con);
                        cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = CompanyId_;
                        cmd.Parameters.Add("@ActivateCode", System.Data.DbType.String).Value = ActiveRemove_==true? ActivateCode :"";
                        cmd.Parameters.Add("@IsProductActivated", System.Data.DbType.Int16).Value = ActiveRemove_==true? 1:0;

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Activation code not valied  \nCompany Id : " + CompanyId_.ToString() + "\nProd Code : " + ActivateCode.ToString());
                    return false;
                }
                
            }
            catch { return false; }
        }

        public static bool EditCompanyName(Int16 CompanyId_, string CompanyName)
        {
            try
            {

                SQLiteCommand cmd;
                using (SQLiteConnection con = new SQLiteConnection(getConStr()))
                {
                    con.Open();

                    cmd = new SQLiteCommand("update CompanyInfo set CompanyName=@CompanyName where CompanyId=@CompanyId", con);
                    cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = CompanyId_;
                    cmd.Parameters.Add("@CompanyName", System.Data.DbType.String).Value = CompanyName;

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch { return false; }
        }


        public static bool IsCompanyActivated(Int16 CompanyId)
        {
            SQLiteCommand cmd;
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                con.Open();
                cmd = new SQLiteCommand("select * from CompanyInfo where CompanyId=@CompanyId and IsProductActivated=1", con);
                cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = CompanyId;
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    return true;

                }
                r_.Close();
            }

            return false;

        }

        public static bool IsProductCodeGenuine(string _ProductCode)
        {
            try
            {
                string comCd_ = BusinessLogic.HardwareID.GET_HARDWAREID.ToUpper();
                if (_ProductCode ==(BusinessLogic.HardwareID.convert2FormatedKey(StringCipher.Encrypt(comCd_, "ClearSyntax")).ToUpper()))
                {
                    return true;
                }
            }
            catch { return false; }
            return false;

        }


        private static string IsFileLocked(Exception exception)
        {
            int errorCode = System.Runtime.InteropServices.Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            //return errorCode == ERROR_SHARING_VIOLATION || errorCode == ERROR_LOCK_VIOLATION;
            //return false;
            switch (errorCode)
            {
                case 2:
                    return "Invalid file name";
                    break;
                case 1326:
                    return "Shared DB file cannot access..("+errorCode.ToString()+")" ;
                    break;

            }
            if (errorCode > 0)
            {
                return "Error Code : " +errorCode.ToString();
            }
            return "";
        }
        internal static string CanReadFile(string filePath)
        {
            //Try-Catch so we dont crash the program and can check the exception
            try
            {
                //The "using" is important because FileStream implements IDisposable and
                //"using" will avoid a heap exhaustion situation when too many handles  
                //are left undisposed.
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    if (fileStream != null) fileStream.Close();  //This line is me being overly cautious, fileStream will never be null unless an exception occurs... and I know the "using" does it but its helpful to be explicit - especially when we encounter errors - at least for me anyway!
                }
            }
            catch (IOException ex)
            {
                return IsFileLocked(ex);

                //THE FUNKY MAGIC - TO SEE IF THIS FILE REALLY IS LOCKED!!!
                //if (IsFileLocked(ex))
                //{
                //    // do something, eg File.Copy or present the user with a MsgBox - I do not recommend Killing the process that is locking the file
                //    return false;
                //}
            }
            finally
            { }
            return "";
        }



        public static string IsDbConnectionOk()
        {
            try
            {
                //var ms_ = CanReadFile(_dbpath + "\\ChequeWriter.db");
                //if (ms_ != "")
                //{
                //    return "Error, " + ms_;
                //}


                using (SQLiteConnection con = new SQLiteConnection(getConStr()))
                {
                    con.Open();
                    con.Close();
                    return "";
                }
                return "Error";
            }
            catch (Exception ex)
            {
                return ("Error, " + ex.Message.ToString() + " [ " + _dbpath + " ]");
            }
            
        }



        public static Int16 getCompanyId(string CompanyName)
        {
            Int16 comId_ = 0;

            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                SQLiteCommand cmd = new SQLiteCommand("select * from CompanyInfo where IsActive=1 and CompanyName=@CompanyName", con);
                con.Open();
                cmd.Parameters.Add("@CompanyName", System.Data.DbType.String, 100).Value = CompanyName;
                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    comId_ = Convert.ToInt16(r["CompanyId"] == DBNull.Value ? "0" : r["CompanyId"].ToString());
                    return comId_;
                }
                r.Close();

            }

            if (comId_ == 0)
            {
                using (SQLiteConnection con = new SQLiteConnection(getConStr()))
                {
                    SQLiteCommand cmd = new SQLiteCommand("select * from CompanyInfo where IsActive=1 order by mdfon desc", con);
                    con.Open();
                    cmd.Parameters.Add("@CompanyName", System.Data.DbType.String, 100).Value = CompanyName;
                    SQLiteDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        comId_ = Convert.ToInt16(r["CompanyId"] == DBNull.Value ? "0" : r["CompanyId"].ToString());
                        return comId_;
                    }
                    r.Close();

                }
            }
            return comId_;
        }

        public static Int32 getPayeeId(string _payeeName)
        {
            Int32 comId_ = 0;

            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                SQLiteCommand cmd = new SQLiteCommand("select * from PayeeMst where PayeeName=@PayeeName", con);
                con.Open();
                cmd.Parameters.Add("@PayeeName", System.Data.DbType.String, 100).Value = _payeeName;
                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    comId_ = Convert.ToInt16(r["PayeeId"] == DBNull.Value ? "0" : r["PayeeId"].ToString());
                }
                r.Close();

            }

            return comId_;
        }

        public List<BankMst> Bank
        {
            get
            {
                List<BankMst> l_ = new List<BankMst>();

                using (SQLiteConnection con = new SQLiteConnection(getConStr()))
                {

                    SQLiteCommand cmd = new SQLiteCommand("select * from BankMst where IsDeleted=0 order by BankName", con);
                    con.Open();
                    SQLiteDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        BankMst k = new BankMst();
                        k.BankId  = Convert.ToInt16(r["BankId"] == DBNull.Value ? "0" : r["BankId"].ToString());
                        k.BankName = r["BankName"] == DBNull.Value ? "" : r["BankName"].ToString();
                        k.BankAddress = r["BankAddress"] == DBNull.Value ? "" : r["BankAddress"].ToString();
                        l_.Add(k);
                    }
                    r.Close();

                }

                return l_;
            }
        }
        
        public static bool IsPayeeExisting(string PayeeName)
        {
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                SQLiteCommand cmd = new SQLiteCommand("select 1 from PayeeMst where PayeeName=@PayeeName and IsDeleted=0", con);
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@PayeeName", System.Data.DbType.String, 100).Value = PayeeName;
                con.Open();
                SQLiteDataReader r_ = cmd.ExecuteReader();
            r_.Read();
            if (r_.HasRows)
            {
                r_.Close();
                return true;
            }
            r_.Close();
            }
            return false;
        }
        public static bool IsPayeeExistingInCB(int PayeeId)
        {
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                SQLiteCommand cmd = new SQLiteCommand("select 1 from ChequeBook where PayeeId=@PayeeId", con);
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@PayeeId", System.Data.DbType.Int32).Value = PayeeId;
                con.Open();
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    r_.Close();
                    return true;
                }
                r_.Close();
            }
            return false;
        }

        public static bool IsPayeeExistingInCB(int PayeeId,SQLiteConnection _con)
        {
            //using (SQLiteConnection con = new SQLiteConnection(ConStr))
            //{
                SQLiteCommand cmd = new SQLiteCommand("select 1 from ChequeBook where PayeeId=@PayeeId", _con);
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@PayeeId", System.Data.DbType.Int32).Value = PayeeId;
                //con.Open();
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    r_.Close();
                    return true;
                }
                r_.Close();
            //}
            return false;
        }

        
        public static bool IsDrawerExisting(string DrawerName)
        {
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                SQLiteCommand cmd = new SQLiteCommand("select 1 from DrawerInfo where DrawerName=@DrawerName", con);
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@DrawerName", System.Data.DbType.String, 100).Value = DrawerName;
                con.Open();
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    r_.Close();
                    return true;
                }
                r_.Close();
            }
            return false;
        }
        public static bool IsDrawerExistingInCB(int DrawerId)
        {
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                SQLiteCommand cmd = new SQLiteCommand("select 1 from ChequeBook where DrawerId=@DrawerId", con);
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@DrawerId", System.Data.DbType.Int32).Value = DrawerId;
                con.Open();
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    r_.Close();
                    return true;
                }
                r_.Close();
            }
            return false;
        }
        public static bool IsDrawerNameExisting(string DrawerName)
        {
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                SQLiteCommand cmd = new SQLiteCommand("select 1 from DrawerMst where DrawerName=@DrawerName", con);
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@DrawerName", System.Data.DbType.String,100 ).Value = DrawerName;
                con.Open();
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    r_.Close();
                    return true;
                }
                r_.Close();
            }
            return false;
        }

        public static bool IsChequeExisting(Int16 _CompanyId,int _BankId,string _ChequeNo)
        {
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                SQLiteCommand cmd = new SQLiteCommand("select 1 from ChequeBook where CompanyId=@CompanyId and ChequeNo=@ChequeNo and BankId=@BankId", con);
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = _CompanyId;
                cmd.Parameters.Add("@ChequeNo", System.Data.DbType.String, 20).Value = _ChequeNo;
                cmd.Parameters.Add("@BankId", System.Data.DbType.Int16).Value = _BankId;
                con.Open();
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    r_.Close();
                    return true;
                }
                r_.Close();
            }
            return false;
        }

        public static bool IsChequePrinted(Int16 _CompanyId, int _BankId, string _ChequeNo)
        {
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                SQLiteCommand cmd = new SQLiteCommand("select 1 from ChequeBook where CompanyId=@CompanyId and ChequeNo=@ChequeNo and BankId=@BankId and IsPrint=1", con);
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = _CompanyId;
                cmd.Parameters.Add("@ChequeNo", System.Data.DbType.String, 20).Value = _ChequeNo;
                cmd.Parameters.Add("@BankId", System.Data.DbType.Int16).Value = _BankId;
                con.Open();
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    r_.Close();
                    return true;
                }
                r_.Close();
            }
            return false;
        }



        public static bool IsBankExistingInCB(int BankId)
        {
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                SQLiteCommand cmd = new SQLiteCommand("select 1 from ChequeBook where BankId=@BankId", con);
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@BankId", System.Data.DbType.Int32).Value = BankId;
                con.Open();
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    r_.Close();
                    return true;
                }
                r_.Close();
            }
            return false;
        }
        public static bool IsBankNameExisting(string BankName)
        {
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                SQLiteCommand cmd = new SQLiteCommand("select 1 from BankMst where BankName=@BankName", con);
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@BankName", System.Data.DbType.String,100).Value = BankName;
                con.Open();
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    r_.Close();
                    return true;
                }
                r_.Close();
            }
            return false;
        }


        public static bool DeletePayee(string _PayeeName)
        {
            var pid_ = getPayeeId(_PayeeName);

            bool isExitingData = IsPayeeExistingInCB(pid_);
            SQLiteCommand cmd;
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                
                if(isExitingData==true)
                {
                    cmd = new SQLiteCommand("update PayeeMst set IsDeleted=1 where PayeeName=@PayeeName", con);
                }else
                {
                    cmd = new SQLiteCommand("delete from PayeeMst where PayeeName=@PayeeName", con);
                }
                //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                cmd.Parameters.Add("@PayeeName", System.Data.DbType.String, 100).Value = _PayeeName;
                con.Open();
                cmd.ExecuteNonQuery(); 
                return true;
            }
            return false;
        }
        public static bool DeletePayeeIfNotExistingInCB(Int32 _PayeeId)
        {
            bool isExitingData = IsPayeeExistingInCB(_PayeeId);
            SQLiteCommand cmd;
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                if (isExitingData == false)
                {
                    cmd = new SQLiteCommand("delete from PayeeMst where PayeeId=@PayeeId", con);
                    //cmd.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode;
                    cmd.Parameters.Add("@PayeeId", System.Data.DbType.Int32).Value = _PayeeId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            return false;
        }
        public static bool DeleteDrawer(Drawer o)
        {
            bool isExitingData = IsDrawerExistingInCB(o.DrawerId);
            SQLiteCommand cmd;

            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                if (isExitingData == true)
                {
                    cmd = new SQLiteCommand("update DrawerInfo set IsDeleted=1 where DrawerName=@DrawerName", con);
                }
                else
                {
                    cmd = new SQLiteCommand("delete from DrawerInfo where DrawerName=@DrawerName", con);
                }
                cmd.Parameters.Add("@DrawerName", System.Data.DbType.String, 100).Value = o.DrawerName;
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            return false;
        }
        public static bool DeleteBank(BankMst o)
        {
            bool isExitingData = IsBankExistingInCB(o.BankId);
            SQLiteCommand cmd;

            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                if (isExitingData == true)
                {
                    cmd = new SQLiteCommand("update BankMst set IsDeleted=1 where BankName=@BankName", con);
                }
                else
                {
                    cmd = new SQLiteCommand("delete from BankMst where BankName=@BankName", con);
                }
                cmd.Parameters.Add("@BankName", System.Data.DbType.String, 100).Value = o.BankName;
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            return false;
        }


        public static bool SysDateUpdate(Int16 _CompanyId,DateTime _lastUpdateOn, SQLiteConnection con_, SQLiteTransaction trn_)
        {
            SQLiteCommand cmd;

            //using (SQLiteConnection con = new SQLiteConnection(ConStr))
            //{
            //    con.Open();

            cmd = new SQLiteCommand("delete from SysDate where CompanyId=@CompanyId", con_, trn_);
            cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = _CompanyId;        
                    cmd.ExecuteNonQuery();
                    cmd = new SQLiteCommand("insert into SysDate(CompanyId,CurrDate,MdfOn) values(@CompanyId,@CurrDate,@MdfOn)", con_, trn_);
                    cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = _CompanyId;        
                    cmd.Parameters.Add("@CurrDate", System.Data.DbType.DateTime).Value = _lastUpdateOn;
                    cmd.Parameters.Add("@MdfOn", System.Data.DbType.DateTime).Value = DateTime.Now;
                
                cmd.ExecuteNonQuery();
                return true;
            //}
            return false;
        }
        public static DateTime LastEntryUpdatedOn(Int16 CompanyId)
        {
            SQLiteCommand cmd;
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {
                con.Open();

                cmd = new SQLiteCommand("select * from SysDate where CompanyId=@CompanyId", con);
                cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = CompanyId;
                SQLiteDataReader r_ = cmd.ExecuteReader();
                r_.Read();
                if (r_.HasRows)
                {
                    return   (Convert.ToDateTime(r_["CurrDate"] == DBNull.Value ? DateTime.Now.ToString() : r_["CurrDate"].ToString()));

                }
                r_.Close();
                return DateTime.Now ;

            }
            
        }


        public static bool IsSysDateValidated(SQLiteConnection con_, SQLiteTransaction trn_)
        {
            //bool LastId = true ;
            SQLiteCommand cmd_;
            cmd_ = new SQLiteCommand(@"select CurrDate from SysDate", con_, trn_);
            //cmd_.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode_;
            ////cmd_.Parameters.Add("@vYear", System.Data.DbType.String, 4).Value = year_;
            ////cmd_.Parameters.Add("@vMonth", System.Data.DbType.String, 2).Value = month_;
            ////cmd_.Parameters.Add("@vDate", System.Data.DbType.String, 2).Value = date_;
            //cmd_.Parameters.Add("@vDocTypeCode", System.Data.DbType.String, 3).Value = docTypeCode_;
            //con.Open();
            SQLiteDataReader r_ = cmd_.ExecuteReader();
            r_.Read();
            if (r_.HasRows)
            {
                if(DateTime.Now.Date <  (Convert.ToDateTime(r_["CurrDate"] == DBNull.Value ? DateTime.Now.ToString() : r_["CurrDate"].ToString())))
                {
                    return false;
                }

            }
            r_.Close();
            return true;
        }

        

        public static int AddPayee(PayeeMst z_)
        {
            int id_ = 0;
            
            if (z_.PayeeName.Trim() == "")
            {
                return id_;
            }

            id_ = getPayeeId(z_.PayeeName);

            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                con.Open();
                SQLiteTransaction trn = con.BeginTransaction();
                try
                {
                    SQLiteCommand cmd;
                    if (id_ > 0)
                    {
                        cmd = new SQLiteCommand(@"update PayeeMst set IsDeleted=0 where PayeeId=@PayeeId", con, trn);
                        cmd.Parameters.Add("@PayeeId", System.Data.DbType.Int32).Value = id_;
                        id_ = z_.PayeeId;
                    }
                    else
                    {
                        z_.PayeeId = GeLastPayeeId(con, trn) + 1;
                        cmd = new SQLiteCommand(@"insert into PayeeMst (GuID,PayeeId,PayeeName)values(@GuID,@PayeeId,@PayeeName)", con, trn);
                        cmd.Parameters.Add("@GuID", System.Data.DbType.String,36).Value = Guid.NewGuid().ToString();
                        cmd.Parameters.Add("@PayeeId", System.Data.DbType.Int32).Value = z_.PayeeId;
                        cmd.Parameters.Add("@PayeeName", System.Data.DbType.String, 100).Value = z_.PayeeName;
                        id_ = z_.PayeeId;
                        //-----------
                    }
                    cmd.ExecuteNonQuery();
                    trn.Commit();
                    trn.Dispose();
                    


                }
                catch (Exception ex)
                {
                    try
                    {

                        trn.Rollback();
                        id_ = -1;
                        //Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        //Console.WriteLine("  Message: {0}", ex.Message);
                        //System.Windows.Forms.MessageBox.Show(ex.ToString());
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(@"Sorry, Record can not be updated.
                                    ***** There are may be few reasons *****
                                    1) Record existing
                                    2) Incorreect information
                                    
                                    ** or please contact System Administrator");

                    }
                    catch (Exception ex1)
                    {
                        id_ = -1;
                        System.Windows.Forms.MessageBox.Show("Sorry, Record can not be updated.\n" + ex.Message.ToString());
                    }
                }

            }
            return id_;

        }
        public static bool AddDrawer(Drawer z_)
        {
            bool entrySaved_ = false;
            if (z_.DrawerName.Trim() == "")
            {
                return entrySaved_;
            }
            if (IsDrawerNameExisting(z_.DrawerName) == true)
            {
                return entrySaved_;
            }
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                con.Open();
                SQLiteTransaction trn = con.BeginTransaction();
                try
                {
                    z_.DrawerId = GeLastDrawerId(con, trn) + 1;

                    SQLiteCommand cmd = new SQLiteCommand(@"insert into DrawerInfo (DrawerId,DrawerName)values(@DrawerId,@DrawerName)", con, trn);
                    cmd.Parameters.Add("@DrawerId", System.Data.DbType.Int32).Value = z_.DrawerId;
                    cmd.Parameters.Add("@DrawerName", System.Data.DbType.String, 100).Value = z_.DrawerName;
                    //-----------
                    cmd.ExecuteNonQuery();
                    trn.Commit();

                    trn.Dispose();
                    entrySaved_ = true;

                }
                catch (Exception ex)
                {
                    try
                    {

                        trn.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(@"Sorry, Record can not be updated.
                                    ***** There are may be few reasons *****
                                    1) Record existing
                                    2) Incorreect information
                                    
                                    ** or please contact System Administrator");

                    }
                    catch (Exception ex1)
                    {
                        System.Windows.Forms.MessageBox.Show("Sorry, Record can not be updated.\n" + ex.Message.ToString());
                    }
                }

            }
            return entrySaved_;

        }
        public static bool AddBank(BankMst z_)
        {
            bool entrySaved_ = false;
            if (z_.BankName.Trim() == "")
            {
                return entrySaved_;
            }
            if (IsBankNameExisting(z_.BankName) == true )
            {
                return entrySaved_;
            }


            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                con.Open();
                SQLiteTransaction trn = con.BeginTransaction();
                try
                {
                    z_.BankId = GeLastBankId(con, trn) + 1;

                    SQLiteCommand cmd = new SQLiteCommand(@"insert into BankMst (BankId,BankName,BankAddress)values(@BankId,@BankName,@BankAddress)", con, trn);
                    cmd.Parameters.Add("@BankId", System.Data.DbType.Int32).Value = z_.BankId;
                    cmd.Parameters.Add("@BankName", System.Data.DbType.String, 100).Value = z_.BankName;
                    cmd.Parameters.Add("@BankAddress", System.Data.DbType.String, 100).Value = z_.BankAddress;


                    //-----------
                    cmd.ExecuteNonQuery();
                    trn.Commit();
                    trn.Dispose();
                    entrySaved_ = true;


                }
                catch (Exception ex)
                {
                    try
                    {

                        trn.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(@"Sorry, Record can not be updated.
                                    ***** There are may be few reasons *****
                                    1) Record existing
                                    2) Incorreect information
                                    
                                    ** or please contact System Administrator");

                    }
                    catch (Exception ex1)
                    {
                        System.Windows.Forms.MessageBox.Show("Sorry, Record can not be updated.\n" + ex.Message.ToString());
                    }
                }

            }
            return entrySaved_;

        }





        public static bool AddPageSttings(List<PageSetting> z_)
        {
            bool entrySaved_ = false;
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                con.Open();
                SQLiteTransaction trn = con.BeginTransaction();
                SQLiteCommand cmd;
                try
                {
                    cmd = new SQLiteCommand(@"delete from PageSettings where BankId=@BankId", con, trn);
                    cmd.Parameters.Add("@BankId", System.Data.DbType.Int32).Value = z_.FirstOrDefault().BankId;
                    cmd.ExecuteNonQuery();
                    foreach (PageSetting o in z_)
                    {
                        cmd = new SQLiteCommand(@"insert into PageSettings (BankId,SettingName,SettingValue)values(@BankId1,@SettingName,@SettingValue)", con, trn);
                        cmd.Parameters.Add("@BankId1", System.Data.DbType.Int16).Value = o.BankId;
                        cmd.Parameters.Add("@SettingName", System.Data.DbType.String, 50).Value = o.SettingName;
                        cmd.Parameters.Add("@SettingValue", System.Data.DbType.String, 50).Value = o.SettingValue;
                        cmd.ExecuteNonQuery();
                    }
                    trn.Commit();
                    trn.Dispose();
                    entrySaved_ = true;


                }
                catch (Exception ex)
                {
                    try
                    {

                        trn.Rollback();
                        //Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        //Console.WriteLine("  Message: {0}", ex.Message);
                        //System.Windows.Forms.MessageBox.Show(ex.ToString());
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(@"Sorry, Record can not be updated.
                                    ***** There are may be few reasons *****
                                    1) Record existing
                                    2) Incorreect information
                                    
                                    ** or please contact System Administrator");

                    }
                    catch (Exception ex1)
                    {
                        System.Windows.Forms.MessageBox.Show("Sorry, Record can not be updated.\n" + ex.Message.ToString());
                    }
                }

            }
            return entrySaved_;

        }
        public static bool AddCheque(CheuqeDtl z_)
        {
            bool entrySaved_ = false;
            if (z_.PayeeId < 0)
            {
                return entrySaved_;
            }

            if ((z_.Amt.ToString().Trim() == "" || z_.BankId==0) && z_.PayeeId!=0   )
            {
                return entrySaved_;
            }
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {


                con.Open();
                SQLiteTransaction trn = con.BeginTransaction();
                try
                {

                    if (IsSysDateValidated(con, trn) == false)
                    {
                        trn.Rollback();
                        return false;
                    }

                    BL.SysDateUpdate(BL.CompanyId,DateTime.Now.Date, con, trn);

                    z_.SrNo = GeLastChequeBookSrNo (con, trn) + 1;

                    SQLiteCommand cmd = new SQLiteCommand(@"insert into ChequeBook (SrNo,CompanyId,ChequeNo,Date,ChequeDate
                                ,IsPrintDate,ChequeAmt,IsAcPayeeOnly,PayeeId
                                ,BankId,DrawerId,IsPrint,PrintOn,Remarks,MdfOn
                                )values(@SrNo,@CompanyId,@ChequeNo,@Date,@ChequeDate
                                ,@IsPrintDate,@ChequeAmt,@IsAcPayeeOnly,@PayeeId
                                ,@BankId,@DrawerId,@IsPrint,@PrintOn,@Remarks,@MdfOn)", con, trn);


                    cmd.Parameters.Add("@SrNo", System.Data.DbType.Int32).Value = z_.SrNo;
                    cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = z_.CompanyId;
                    cmd.Parameters.Add("@ChequeNo", System.Data.DbType.String, 20).Value = z_.ChequeNo;
                    cmd.Parameters.Add("@Date", System.Data.DbType.Date).Value = z_.Date.Date ;
                    cmd.Parameters.Add("@ChequeDate", System.Data.DbType.Date).Value = z_.ChequeDate.Date;
                    cmd.Parameters.Add("@IsPrintDate", System.Data.DbType.SByte).Value = Convert.ToSByte(z_.IsPrintDate);
                    cmd.Parameters.Add("@ChequeAmt", System.Data.DbType.Double).Value = (z_.Amt=="")?0: Convert.ToDouble((z_.Amt.Replace("*","").Trim()));
                    cmd.Parameters.Add("@IsAcPayeeOnly", System.Data.DbType.SByte).Value = Convert.ToSByte(z_.IsAcPay);
                    cmd.Parameters.Add("@PayeeId", System.Data.DbType.Int32).Value = Convert.ToInt32(z_.PayeeId);
                    cmd.Parameters.Add("@BankId", System.Data.DbType.Int32).Value = Convert.ToInt32(z_.BankId);
                    cmd.Parameters.Add("@DrawerId", System.Data.DbType.Int32).Value = Convert.ToInt32(z_.DrawerId);
                    cmd.Parameters.Add("@IsPrint", System.Data.DbType.SByte).Value = Convert.ToSByte(z_.IsPrint == false ? 0 : 1);
                    cmd.Parameters.Add("@PrintOn", System.Data.DbType.DateTime).Value = z_.PrintOn;
                    cmd.Parameters.Add("@Remarks", System.Data.DbType.String, 20).Value = z_.Remarks;
                    cmd.Parameters.Add("@MdfOn", System.Data.DbType.DateTime).Value = z_.MdfOn;

                    //-----------
                    cmd.ExecuteNonQuery();
                    trn.Commit();
                    trn.Dispose();
                    entrySaved_ = true;


                }
                catch (Exception ex)
                {
                    try
                    {

                        trn.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(@"Sorry, Record can not be updated.
                                    ***** There are may be few reasons *****
                                    1) Record existing
                                    2) Incorreect information
                                    
                                    ** or please contact System Administrator");

                    }
                    catch (Exception ex1)
                    {
                        System.Windows.Forms.MessageBox.Show("Sorry, Record can not be updated.\n" + ex.Message.ToString());
                    }
                }

            }
            return entrySaved_;

        }
        public static bool EditChequeEntry(CheuqeDtl z_)
        {
            bool entrySaved_ = false;
            if (z_.PayeeId < 0)
            {
                return entrySaved_;
            }

            if ((z_.Amt.ToString().Trim() == "" || z_.BankId == 0) && z_.PayeeId != 0)
            {
                return entrySaved_;
            }

            if (IsChequePrinted(z_.CompanyId,z_.BankId,z_.ChequeNo) ==true)
            {
                System.Windows.Forms.MessageBox.Show(@"Sorry, Record can not be updated.\n
                                    Cheque already printed, update not possible...!");

                return false;
            }


            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {


                con.Open();
                SQLiteTransaction trn = con.BeginTransaction();
                try
                {

                    if (IsSysDateValidated(con, trn) == false)
                    {
                        trn.Rollback();
                        return false;
                    }

                    BL.SysDateUpdate(BL.CompanyId, DateTime.Now.Date, con, trn);

                    //z_.SrNo = GeLastChequeBookSrNo(con, trn) + 1;

                    SQLiteCommand cmd = new SQLiteCommand(@"update ChequeBook set
                                 Date=@Date
                                ,ChequeDate=@ChequeDate
                                ,IsPrintDate=@IsPrintDate
                                ,ChequeAmt=@ChequeAmt
                                ,IsAcPayeeOnly=@IsAcPayeeOnly
                                ,PayeeId=@PayeeId
                                ,DrawerId=@DrawerId
                                ,IsPrint=@IsPrint
                                ,PrintOn=@PrintOn
                                ,Remarks=@Remarks
                                ,MdfOn=@MdfOn
                                where CompanyId=@CompanyId
                                and ChequeNo=@ChequeNo
                                and BankId=@BankId", con, trn);


                    //cmd.Parameters.Add("@SrNo", System.Data.DbType.Int32).Value = z_.SrNo;
                    cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = z_.CompanyId;
                    cmd.Parameters.Add("@ChequeNo", System.Data.DbType.String, 20).Value = z_.ChequeNo;
                    cmd.Parameters.Add("@Date", System.Data.DbType.Date).Value = z_.Date.Date;
                    cmd.Parameters.Add("@ChequeDate", System.Data.DbType.Date).Value = z_.ChequeDate.Date;
                    cmd.Parameters.Add("@IsPrintDate", System.Data.DbType.SByte).Value = Convert.ToSByte(z_.IsPrintDate);
                    cmd.Parameters.Add("@ChequeAmt", System.Data.DbType.Double).Value = (z_.Amt == "") ? 0 : Convert.ToDouble((z_.Amt.Replace("*", "").Trim()));
                    cmd.Parameters.Add("@IsAcPayeeOnly", System.Data.DbType.SByte).Value = Convert.ToSByte(z_.IsAcPay);
                    cmd.Parameters.Add("@PayeeId", System.Data.DbType.Int32).Value = Convert.ToInt32(z_.PayeeId);
                    cmd.Parameters.Add("@BankId", System.Data.DbType.Int32).Value = Convert.ToInt32(z_.BankId);
                    cmd.Parameters.Add("@DrawerId", System.Data.DbType.Int32).Value = Convert.ToInt32(z_.DrawerId);
                    cmd.Parameters.Add("@IsPrint", System.Data.DbType.SByte).Value = Convert.ToSByte(z_.IsPrint == false ? 0 : 1);
                    cmd.Parameters.Add("@PrintOn", System.Data.DbType.DateTime).Value = z_.PrintOn;
                    cmd.Parameters.Add("@Remarks", System.Data.DbType.String, 20).Value = z_.Remarks;
                    cmd.Parameters.Add("@MdfOn", System.Data.DbType.DateTime).Value = z_.MdfOn;

                    //-----------
                    cmd.ExecuteNonQuery();
                    trn.Commit();
                    trn.Dispose();
                    entrySaved_ = true;


                }
                catch (Exception ex)
                {
                    try
                    {

                        trn.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(@"Sorry, Record can not be updated.
                                    ***** There are may be few reasons *****
                                    1) Record existing
                                    2) Incorreect information
                                    
                                    ** or please contact System Administrator");

                    }
                    catch (Exception ex1)
                    {
                        System.Windows.Forms.MessageBox.Show("Sorry, Record can not be updated.\n" + ex.Message.ToString());
                    }
                }

            }
            return entrySaved_;

        }

        public static bool DeleteChequeEntry(Int32 _CompanyId ,Int32 _SrNo)
        {
            bool result_ = false;
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {


                con.Open();
                SQLiteTransaction trn = con.BeginTransaction();
                try
                {

                    SQLiteCommand cmd = new SQLiteCommand(@"delete  from ChequeBook where CompanyId=@CompanyId and SrNo=@SrNo", con, trn);

                    cmd.Parameters.Add("@SrNo", System.Data.DbType.Int32).Value = _SrNo;
                    cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = _CompanyId;

                    //-----------
                    cmd.ExecuteNonQuery();
                    trn.Commit();
                    trn.Dispose();
                    result_ = true;


                }
                catch (Exception ex)
                {
                    try
                    {

                        trn.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(@"Sorry, Record can not deleted.
                                    ***** There are may be few reasons *****
                                    1) Record existing
                                    2) Incorreect information
                                    
                                    ** or please contact System Administrator");

                    }
                    catch (Exception ex1)
                    {
                        System.Windows.Forms.MessageBox.Show("Sorry, Record can not be updated.\n" + ex.Message.ToString());
                    }
                }

            }
            return result_;

        }

        public static bool DeleteNotrequredPayees(Int32 _PayeeId)
        {
            bool result_ = false;
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {


                con.Open();
                SQLiteTransaction trn = con.BeginTransaction();
                try
                {

                    if (IsPayeeExistingInCB(_PayeeId,con) == false)
                    {

                        SQLiteCommand cmd = new SQLiteCommand(@"delete from PayeeMst where PayeeId=@PayeeId", con, trn);
                        cmd.Parameters.Add("@PayeeId", System.Data.DbType.Int32).Value = _PayeeId;
                        //-----------
                        cmd.ExecuteNonQuery();
                        trn.Commit();
                        trn.Dispose();
                    }
                    result_ = true;

                }
                catch (Exception ex)
                {
                    try
                    {

                        trn.Rollback();
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        System.Windows.Forms.MessageBox.Show(@"Sorry, Record can not deleted.
                                    ***** There are may be few reasons *****
                                    1) Record existing
                                    2) Incorreect information
                                    
                                    ** or please contact System Administrator");

                    }
                    catch (Exception ex1)
                    {
                        System.Windows.Forms.MessageBox.Show("Sorry, Record can not be updated.\n" + ex.Message.ToString());
                    }
                }

            }
            return result_;

        }



        public static List<CheuqeDtl> getChequeList(Int16 companyId, DateTime fromDate, DateTime toDate)
        {
            List<CheuqeDtl> l_ = new List<CheuqeDtl>();

            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
                {

                    CheuqeDtl k;
                    SQLiteCommand cmd = new SQLiteCommand(@"select b.BankName,c.DrawerName,d.PayeeName
                    ,e.CompanyName,a.* from ChequeBook as a left outer join BankMst as b 
                    on b.BankId=a.BankId
                    left outer join DrawerInfo as c on c.DrawerId=a.DrawerId
                    left outer join PayeeMst as d on d.PayeeId=a.PayeeId
                    left outer join CompanyInfo as e on e.CompanyId=a.CompanyId
                    where a.CompanyId=@CompanyId
                    and strftime('%Y-%m-%d',a.Date) >= @FromDate
                    and strftime('%Y-%m-%d',a.Date) < date(@ToDate,'1 day')", con);
                    con.Open();
                    cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = companyId;
                    cmd.Parameters.Add("@FromDate", System.Data.DbType.String, 15).Value = fromDate.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@ToDate", System.Data.DbType.String, 15).Value = toDate.ToString("yyyy-MM-dd");

                    SQLiteDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        k = new CheuqeDtl();

                        k.SrNo = Convert.ToInt32(r["SrNo"] == DBNull.Value ? "0" : r["SrNo"].ToString());
                        k.CompanyId = Convert.ToInt16(r["CompanyId"] == DBNull.Value ? "0" : r["CompanyId"].ToString());
                        k.CompanyName = r["CompanyName"] == DBNull.Value ? "" : r["CompanyName"].ToString();
                        k.ChequeNo = r["ChequeNo"] == DBNull.Value ? "" : r["ChequeNo"].ToString();
                        k.Date = Convert.ToDateTime(r["Date"] == DBNull.Value ? "1/1/2000" : r["Date"].ToString());
                        k.ChequeDate = Convert.ToDateTime(r["ChequeDate"] == DBNull.Value ? "1/1/2000" : r["ChequeDate"].ToString());
                        k.IsAcPay = (r["IsAcPayeeOnly"] == DBNull.Value ? "0" : r["IsAcPayeeOnly"].ToString()) == "1" ? true : false;
                        k.IsPrintDate = (r["IsPrintDate"] == DBNull.Value ? "0" : r["IsPrintDate"].ToString()) == "1" ? true : false;
                        k.IsPrint = (r["IsPrint"] == DBNull.Value ? "0" : r["IsPrint"].ToString()) == "1" ? true : false;  
                        k.PrintOn = Convert.ToDateTime(r["PrintOn"] == DBNull.Value ? "1/1/2000" : r["PrintOn"].ToString());

                        k.Day1 = k.ChequeDate.ToString("dd").Substring(0, 1);
                        k.Day2 = k.ChequeDate.ToString("dd").Substring(1, 1);
                        k.Month1 = k.ChequeDate.ToString("MM").Substring(0, 1);
                        k.Month2 = k.ChequeDate.ToString("MM").Substring(1, 1);
                        k.Year1 = k.ChequeDate.ToString("yy").Substring(0, 1);
                        k.Year2 = k.ChequeDate.ToString("yy").Substring(1, 1);  
                        k.PayeeId = Convert.ToInt32(r["PayeeId"] == DBNull.Value ? "0" : r["PayeeId"].ToString());
                        k.PayeeName = r["PayeeName"] == DBNull.Value ? "" : r["PayeeName"].ToString();
                        if (k.PayeeId == 0)
                        {
                            k.PayeeName = "***********  Print only  ***********";
                        }

                        k.BankId = Convert.ToInt16(r["BankId"] == DBNull.Value ? "0" : r["BankId"].ToString());
                        k.BankName = r["BankName"] == DBNull.Value ? "" : r["BankName"].ToString();
                        k.DrawerId = Convert.ToInt16(r["DrawerId"] == DBNull.Value ? "0" : r["DrawerId"].ToString());
                        k.DrawerName = r["DrawerName"] == DBNull.Value ? "" : r["DrawerName"].ToString();
                        k.PayTo1 = r["PayeeName"] == DBNull.Value ? "" : r["PayeeName"].ToString();
                        //k.PayTo2 = r["PayTo2"] == DBNull.Value ? "" : r["PayTo2"].ToString();
                        //public string AmtInWord1 { get; set; }
                        //public string AmtInWord2 { get; set; }
                        //public string AmtInWord3 { get; set; }
                        //public string AmtInWord4 { get; set; }
                        k.ChequeAmt = Convert.ToDecimal(r["ChequeAmt"] == DBNull.Value ? "0" : r["ChequeAmt"].ToString());
                        //k.IsPrintComName = (r["IsPrintComName"] == DBNull.Value ? "0" : r["IsPrintComName"].ToString()) == "1" ? true : false; 
                        k.Remarks = r["Remarks"] == DBNull.Value ? "" : r["Remarks"].ToString();
                        k.MdfOn = Convert.ToDateTime(r["MdfOn"] == DBNull.Value ? "1/1/2000" : r["MdfOn"].ToString());

                        l_.Add(k);
                    }
                    r.Close();

                }

                return l_;
            
        }
        public static List<CheuqeDtl> getChequeList(Int16 companyId, int payeeId,int lastNumOfEntrys)
        {
            List<CheuqeDtl> l_ = new List<CheuqeDtl>();

            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                CheuqeDtl k;
                SQLiteCommand cmd = new SQLiteCommand(@"select b.BankName,c.DrawerName,d.PayeeName
                    ,e.CompanyName,a.* from ChequeBook as a left outer join BankMst as b 
                    on b.BankId=a.BankId
                    left outer join DrawerInfo as c on c.DrawerId=a.DrawerId
                    left outer join PayeeMst as d on d.PayeeId=a.PayeeId
                    left outer join CompanyInfo as e on e.CompanyId=a.CompanyId
                    where a.CompanyId=@CompanyId
                    and a.PayeeId=@PayeeId
                    order by a.Date desc limit "+ lastNumOfEntrys , con);
                con.Open();
                cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = companyId;
                cmd.Parameters.Add("@PayeeId", System.Data.DbType.Int32).Value = payeeId;
                //cmd.Parameters.Add("@FromDate", System.Data.DbType.String, 15).Value = fromDate.ToString("yyyy-MM-dd");
                //cmd.Parameters.Add("@ToDate", System.Data.DbType.String, 15).Value = toDate.ToString("yyyy-MM-dd");

                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    k = new CheuqeDtl();

                    k.SrNo = Convert.ToInt32(r["SrNo"] == DBNull.Value ? "0" : r["SrNo"].ToString());
                    k.CompanyId = Convert.ToInt16(r["CompanyId"] == DBNull.Value ? "0" : r["CompanyId"].ToString());
                    k.CompanyName = r["CompanyName"] == DBNull.Value ? "" : r["CompanyName"].ToString();
                    k.ChequeNo = r["ChequeNo"] == DBNull.Value ? "" : r["ChequeNo"].ToString();
                    k.Date = Convert.ToDateTime(r["Date"] == DBNull.Value ? "1/1/2000" : r["Date"].ToString());
                    k.ChequeDate = Convert.ToDateTime(r["ChequeDate"] == DBNull.Value ? "1/1/2000" : r["ChequeDate"].ToString());
                    k.IsAcPay = (r["IsAcPayeeOnly"] == DBNull.Value ? "0" : r["IsAcPayeeOnly"].ToString()) == "1" ? true : false;
                    k.IsPrintDate = (r["IsPrintDate"] == DBNull.Value ? "0" : r["IsPrintDate"].ToString()) == "1" ? true : false;
                    k.IsPrint = (r["IsPrint"] == DBNull.Value ? "0" : r["IsPrint"].ToString()) == "1" ? true : false;
                    k.PrintOn = Convert.ToDateTime(r["PrintOn"] == DBNull.Value ? "1/1/2000" : r["PrintOn"].ToString());

                    k.Day1 = k.ChequeDate.ToString("dd").Substring(0, 1);
                    k.Day2 = k.ChequeDate.ToString("dd").Substring(1, 1);
                    k.Month1 = k.ChequeDate.ToString("MM").Substring(0, 1);
                    k.Month2 = k.ChequeDate.ToString("MM").Substring(1, 1);
                    k.Year1 = k.ChequeDate.ToString("yy").Substring(0, 1);
                    k.Year2 = k.ChequeDate.ToString("yy").Substring(1, 1);
                    k.PayeeId = Convert.ToInt32(r["PayeeId"] == DBNull.Value ? "0" : r["PayeeId"].ToString());
                    k.PayeeName = r["PayeeName"] == DBNull.Value ? "" : r["PayeeName"].ToString();
                    if (k.PayeeId == 0)
                    {
                        k.PayeeName = "***********  Print only  ***********";
                    }

                    k.BankId = Convert.ToInt16(r["BankId"] == DBNull.Value ? "0" : r["BankId"].ToString());
                    k.BankName = r["BankName"] == DBNull.Value ? "" : r["BankName"].ToString();
                    k.DrawerId = Convert.ToInt16(r["DrawerId"] == DBNull.Value ? "0" : r["DrawerId"].ToString());
                    k.DrawerName = r["DrawerName"] == DBNull.Value ? "" : r["DrawerName"].ToString();
                    k.PayTo1 = r["PayeeName"] == DBNull.Value ? "" : r["PayeeName"].ToString();
                    //k.PayTo2 = r["PayTo2"] == DBNull.Value ? "" : r["PayTo2"].ToString();
                    //public string AmtInWord1 { get; set; }
                    //public string AmtInWord2 { get; set; }
                    //public string AmtInWord3 { get; set; }
                    //public string AmtInWord4 { get; set; }
                    k.ChequeAmt = Convert.ToDecimal(r["ChequeAmt"] == DBNull.Value ? "0" : r["ChequeAmt"].ToString());
                    //k.IsPrintComName = (r["IsPrintComName"] == DBNull.Value ? "0" : r["IsPrintComName"].ToString()) == "1" ? true : false; 
                    k.Remarks = r["Remarks"] == DBNull.Value ? "" : r["Remarks"].ToString();
                    k.MdfOn = Convert.ToDateTime(r["MdfOn"] == DBNull.Value ? "1/1/2000" : r["MdfOn"].ToString());

                    l_.Add(k);
                }
                r.Close();

            }

            return l_;

        }
        public static List<CheuqeDtl> getPostdatedChequeList(Int16 companyId,DateTime toDate)
        {
            List<CheuqeDtl> l_ = new List<CheuqeDtl>();

            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                CheuqeDtl k;
                SQLiteCommand cmd = new SQLiteCommand(@"select b.BankName,c.DrawerName,d.PayeeName
                    ,e.CompanyName,a.* from ChequeBook as a left outer join BankMst as b 
                    on b.BankId=a.BankId
                    left outer join DrawerInfo as c on c.DrawerId=a.DrawerId
                    left outer join PayeeMst as d on d.PayeeId=a.PayeeId
                    left outer join CompanyInfo as e on e.CompanyId=a.CompanyId
                    where a.CompanyId=@CompanyId
                    and strftime('%Y-%m-%d',a.ChequeDate) > @ToDate", con);
                con.Open();
                cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = companyId;
                cmd.Parameters.Add("@ToDate", System.Data.DbType.String, 15).Value = toDate.ToString("yyyy-MM-dd");

                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    k = new CheuqeDtl();

                    k.SrNo = Convert.ToInt32(r["SrNo"] == DBNull.Value ? "0" : r["SrNo"].ToString());
                    k.CompanyId = Convert.ToInt16(r["CompanyId"] == DBNull.Value ? "0" : r["CompanyId"].ToString());
                    k.CompanyName = r["CompanyName"] == DBNull.Value ? "" : r["CompanyName"].ToString();
                    k.ChequeNo = r["ChequeNo"] == DBNull.Value ? "" : r["ChequeNo"].ToString();
                    k.Date = Convert.ToDateTime(r["Date"] == DBNull.Value ? "1/1/2000" : r["Date"].ToString());
                    k.ChequeDate = Convert.ToDateTime(r["ChequeDate"] == DBNull.Value ? "1/1/2000" : r["ChequeDate"].ToString());
                    k.IsAcPay = (r["IsAcPayeeOnly"] == DBNull.Value ? "0" : r["IsAcPayeeOnly"].ToString()) == "1" ? true : false;
                    k.IsPrintDate = (r["IsPrintDate"] == DBNull.Value ? "0" : r["IsPrintDate"].ToString()) == "1" ? true : false;
                    k.IsPrint = (r["IsPrint"] == DBNull.Value ? "0" : r["IsPrint"].ToString()) == "1" ? true : false;
                    k.PrintOn = Convert.ToDateTime(r["PrintOn"] == DBNull.Value ? "1/1/2000" : r["PrintOn"].ToString());

                    k.Day1 = k.ChequeDate.ToString("dd").Substring(0, 1);
                    k.Day2 = k.ChequeDate.ToString("dd").Substring(1, 1);
                    k.Month1 = k.ChequeDate.ToString("MM").Substring(0, 1);
                    k.Month2 = k.ChequeDate.ToString("MM").Substring(1, 1);
                    k.Year1 = k.ChequeDate.ToString("yy").Substring(0, 1);
                    k.Year2 = k.ChequeDate.ToString("yy").Substring(1, 1);
                    k.PayeeId = Convert.ToInt32(r["PayeeId"] == DBNull.Value ? "0" : r["PayeeId"].ToString());
                    k.PayeeName = r["PayeeName"] == DBNull.Value ? "" : r["PayeeName"].ToString();
                    k.BankId = Convert.ToInt16(r["BankId"] == DBNull.Value ? "0" : r["BankId"].ToString());
                    k.BankName = r["BankName"] == DBNull.Value ? "" : r["BankName"].ToString();
                    k.DrawerId = Convert.ToInt16(r["DrawerId"] == DBNull.Value ? "0" : r["DrawerId"].ToString());
                    k.DrawerName = r["DrawerName"] == DBNull.Value ? "" : r["DrawerName"].ToString();
                    k.PayTo1 = r["PayeeName"] == DBNull.Value ? "" : r["PayeeName"].ToString();
                    //k.PayTo2 = r["PayTo2"] == DBNull.Value ? "" : r["PayTo2"].ToString();
                    //public string AmtInWord1 { get; set; }
                    //public string AmtInWord2 { get; set; }
                    //public string AmtInWord3 { get; set; }
                    //public string AmtInWord4 { get; set; }
                    k.ChequeAmt = Convert.ToDecimal(r["ChequeAmt"] == DBNull.Value ? "0" : r["ChequeAmt"].ToString());
                    //k.IsPrintComName = (r["IsPrintComName"] == DBNull.Value ? "0" : r["IsPrintComName"].ToString()) == "1" ? true : false; 
                    k.Remarks = r["Remarks"] == DBNull.Value ? "" : r["Remarks"].ToString();
                    k.MdfOn = Convert.ToDateTime(r["MdfOn"] == DBNull.Value ? "1/1/2000" : r["MdfOn"].ToString());

                    l_.Add(k);
                }
                r.Close();

            }

            return l_;

        }

        public static CheuqeDtl getCheque(Int16 companyId,int BankId, string chequeNo)
        {
            CheuqeDtl l_ = new CheuqeDtl();

            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                CheuqeDtl k;
                SQLiteCommand cmd = new SQLiteCommand(@"select b.BankName,c.DrawerName,d.PayeeName
                    ,e.CompanyName,a.* from ChequeBook as a left outer join BankMst as b 
                    on b.BankId=a.BankId
                    left outer join DrawerInfo as c on c.DrawerId=a.DrawerId
                    left outer join PayeeMst as d on d.PayeeId=a.PayeeId
                    left outer join CompanyInfo as e on e.CompanyId=a.CompanyId
                    where a.CompanyId=@CompanyId and a.ChequeNo=@ChequeNo and a.BankId=@BankId", con);
                con.Open();
                cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = companyId;
                cmd.Parameters.Add("@ChequeNo", System.Data.DbType.String, 20).Value = chequeNo;
                cmd.Parameters.Add("@BankId", System.Data.DbType.Int32).Value = BankId;


                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    k = new CheuqeDtl();

                    k.SrNo = Convert.ToInt32(r["SrNo"] == DBNull.Value ? "0" : r["SrNo"].ToString());
                    k.CompanyId = Convert.ToInt16(r["CompanyId"] == DBNull.Value ? "0" : r["CompanyId"].ToString());
                    k.CompanyName = r["CompanyName"] == DBNull.Value ? "" : r["CompanyName"].ToString();
                    k.ChequeNo = r["ChequeNo"] == DBNull.Value ? "" : r["ChequeNo"].ToString();
                    k.Date = Convert.ToDateTime(r["Date"] == DBNull.Value ? "1/1/2000" : r["Date"].ToString());
                    k.ChequeDate = Convert.ToDateTime(r["ChequeDate"] == DBNull.Value ? "1/1/2000" : r["ChequeDate"].ToString());
                    k.IsAcPay = (r["IsAcPayeeOnly"] == DBNull.Value ? "0" : r["IsAcPayeeOnly"].ToString()) == "1" ? true : false;
                    
                    k.IsPrintDate = (r["IsPrintDate"] == DBNull.Value ? "0" : r["IsPrintDate"].ToString()) == "1" ? true : false;
                    k.IsPrint = (r["IsPrint"] == DBNull.Value ? "0" : r["IsPrint"].ToString()) == "1" ? true : false;
                    k.PrintOn = Convert.ToDateTime(r["PrintOn"] == DBNull.Value ? "1/1/2000" : r["PrintOn"].ToString());

                    k.Day1 = k.ChequeDate.ToString("dd").Substring(0, 1);
                    k.Day2 = k.ChequeDate.ToString("dd").Substring(1, 1);
                    k.Month1 = k.ChequeDate.ToString("MM").Substring(0, 1);
                    k.Month2 = k.ChequeDate.ToString("MM").Substring(1, 1);
                    k.Year1 = k.ChequeDate.ToString("yy").Substring(0, 1);
                    k.Year2 = k.ChequeDate.ToString("yy").Substring(1, 1);
                    k.PayeeId = Convert.ToInt32(r["PayeeId"] == DBNull.Value ? "0" : r["PayeeId"].ToString());
                    k.PayeeName = r["PayeeName"] == DBNull.Value ? "" : r["PayeeName"].ToString();
                    k.BankId = Convert.ToInt16(r["BankId"] == DBNull.Value ? "0" : r["BankId"].ToString());
                    k.BankName = r["BankName"] == DBNull.Value ? "" : r["BankName"].ToString();
                    k.DrawerId = Convert.ToInt16(r["DrawerId"] == DBNull.Value ? "0" : r["DrawerId"].ToString());
                    k.DrawerName = r["DrawerName"] == DBNull.Value ? "" : r["DrawerName"].ToString();
                    k.PayTo1 = r["PayeeName"] == DBNull.Value ? "" : r["PayeeName"].ToString();
                    //k.PayTo2 = r["PayTo2"] == DBNull.Value ? "" : r["PayTo2"].ToString();
                    //public string AmtInWord1 { get; set; }
                    //public string AmtInWord2 { get; set; }
                    //public string AmtInWord3 { get; set; }
                    //public string AmtInWord4 { get; set; }
                    k.ChequeAmt = Convert.ToDecimal(r["ChequeAmt"] == DBNull.Value ? "0" : r["ChequeAmt"].ToString());
                    //k.IsPrintComName = (r["IsPrintComName"] == DBNull.Value ? "0" : r["IsPrintComName"].ToString()) == "1" ? true : false; 
                    k.Remarks = r["Remarks"] == DBNull.Value ? "" : r["Remarks"].ToString();
                    k.MdfOn = Convert.ToDateTime(r["MdfOn"] == DBNull.Value ? "1/1/2000" : r["MdfOn"].ToString());

                    l_=k;
                }
                r.Close();

            }

            return l_;

        }

        public static bool UpdateChequePrintOn(Int16 companyId,int BankId, string chequeNo)
        {
            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                SQLiteCommand cmd = new SQLiteCommand(@"update ChequeBook set IsPrint=1,PrintOn=@PrintOn
                    where CompanyId=@CompanyId and ChequeNo=@ChequeNo and BankId=@BankId", con);
                con.Open();
                cmd.Parameters.Add("@CompanyId", System.Data.DbType.Int16).Value = companyId;
                cmd.Parameters.Add("@ChequeNo", System.Data.DbType.String, 20).Value = chequeNo;
                cmd.Parameters.Add("@BankId", System.Data.DbType.Int32).Value = BankId;
                cmd.Parameters.Add("@PrintOn", System.Data.DbType.DateTime).Value = DateTime.Now;


                cmd.ExecuteNonQuery(); 
            }

            return true;

        }


        public static List<PageSetting> getPageSettings(int BankId)
        {
            List<PageSetting> l_ = new List<PageSetting>();

            using (SQLiteConnection con = new SQLiteConnection(getConStr()))
            {

                SQLiteCommand cmd = new SQLiteCommand("select * from PageSettings where bankid=@bankid order by SettingName", con);
                con.Open();
                cmd.Parameters.Add("@BankId", System.Data.DbType.Int32).Value = BankId;
                SQLiteDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    PageSetting k = new PageSetting();
                    k.BankId = Convert.ToInt16(r["BankId"] == DBNull.Value ? "0" : r["BankId"].ToString());
                    k.SettingName = r["SettingName"] == DBNull.Value ? "" : r["SettingName"].ToString();
                    k.SettingValue = (r["SettingValue"] == DBNull.Value ? "0" : r["SettingValue"].ToString());
                    l_.Add(k);
                }
                r.Close();

            }

            return l_;
        }

        public static Int32 GeLastPayeeId( SQLiteConnection con_, SQLiteTransaction trn_)
        {
            int LastId = 0;
            SQLiteCommand cmd_;
            cmd_ = new SQLiteCommand(@"select max(payeeId) as LastId from PayeeMst", con_, trn_);
            //cmd_.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode_;
            ////cmd_.Parameters.Add("@vYear", System.Data.DbType.String, 4).Value = year_;
            ////cmd_.Parameters.Add("@vMonth", System.Data.DbType.String, 2).Value = month_;
            ////cmd_.Parameters.Add("@vDate", System.Data.DbType.String, 2).Value = date_;
            //cmd_.Parameters.Add("@vDocTypeCode", System.Data.DbType.String, 3).Value = docTypeCode_;
            //con.Open();
            SQLiteDataReader r_ = cmd_.ExecuteReader();
            r_.Read();
            if (r_.HasRows)
            {
                LastId = Convert.ToInt32(r_["LastId"] == DBNull.Value ? "0" : r_["LastId"].ToString());

            }
            r_.Close();
            return LastId;
        }
        public static Int32 GeLastDrawerId(SQLiteConnection con_, SQLiteTransaction trn_)
        {
            int LastId = 0;
            SQLiteCommand cmd_;
            cmd_ = new SQLiteCommand(@"select max(DrawerId) as LastId from DrawerInfo", con_, trn_);
            //cmd_.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode_;
            ////cmd_.Parameters.Add("@vYear", System.Data.DbType.String, 4).Value = year_;
            ////cmd_.Parameters.Add("@vMonth", System.Data.DbType.String, 2).Value = month_;
            ////cmd_.Parameters.Add("@vDate", System.Data.DbType.String, 2).Value = date_;
            //cmd_.Parameters.Add("@vDocTypeCode", System.Data.DbType.String, 3).Value = docTypeCode_;
            //con.Open();
            SQLiteDataReader r_ = cmd_.ExecuteReader();
            r_.Read();
            if (r_.HasRows)
            {
                LastId = Convert.ToInt32(r_["LastId"] == DBNull.Value ? "0" : r_["LastId"].ToString());

            }
            r_.Close();
            return LastId;
        }
        public static Int32 GeLastBankId(SQLiteConnection con_, SQLiteTransaction trn_)
        {
            int LastId = 0;
            SQLiteCommand cmd_;
            cmd_ = new SQLiteCommand(@"select max(BankId) as LastId from BankMst", con_, trn_);
            //cmd_.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode_;
            ////cmd_.Parameters.Add("@vYear", System.Data.DbType.String, 4).Value = year_;
            ////cmd_.Parameters.Add("@vMonth", System.Data.DbType.String, 2).Value = month_;
            ////cmd_.Parameters.Add("@vDate", System.Data.DbType.String, 2).Value = date_;
            //cmd_.Parameters.Add("@vDocTypeCode", System.Data.DbType.String, 3).Value = docTypeCode_;
            //con.Open();
            SQLiteDataReader r_ = cmd_.ExecuteReader();
            r_.Read();
            if (r_.HasRows)
            {
                LastId = Convert.ToInt32(r_["LastId"] == DBNull.Value ? "0" : r_["LastId"].ToString());

            }
            r_.Close();
            return LastId;
        }
        public static Int32 GeLastChequeBookSrNo(SQLiteConnection con_, SQLiteTransaction trn_)
        {
            Int32 LastId = 0;
            SQLiteCommand cmd_;
            cmd_ = new SQLiteCommand(@"select max(SrNo) as LastId from ChequeBook", con_, trn_);
            //cmd_.Parameters.Add("@vKioskCode", System.Data.DbType.String, 4).Value = KioskCode_;
            ////cmd_.Parameters.Add("@vYear", System.Data.DbType.String, 4).Value = year_;
            ////cmd_.Parameters.Add("@vMonth", System.Data.DbType.String, 2).Value = month_;
            ////cmd_.Parameters.Add("@vDate", System.Data.DbType.String, 2).Value = date_;
            //cmd_.Parameters.Add("@vDocTypeCode", System.Data.DbType.String, 3).Value = docTypeCode_;
            //con.Open();
            SQLiteDataReader r_ = cmd_.ExecuteReader();
            r_.Read();
            if (r_.HasRows)
            {
                LastId = Convert.ToInt32(r_["LastId"] == DBNull.Value ? "0" : r_["LastId"].ToString());

            }
            r_.Close();
            return LastId;
        }


        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;
            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }


        public void AddPayee()
        {

        }



        public static bool BackupDB(string _bakupLocation)
        {
            try
            {
                string filename_ = "ChequeWriter_" + DateTime.Now.ToString("ddMMMyy_HHmmss") + ".cpk";

                string bkpStr = "Data Source=" + _bakupLocation + "\\" + filename_ + ";Version=3;";


                using (var location = new SQLiteConnection(getConStr()))
                //using (var destination = new SQLiteConnection(string.Format(@"Data Source={0}:\BackupDb.db;Password=123; Version=3;", _bakupLocation)))
                using (var destination = new SQLiteConnection(bkpStr))
                {
                    location.Open();
                    destination.Open();
                    location.BackupDatabase(destination, "main", "main", -1, null, 0);
                }

                using (var destination = new SQLiteConnection(bkpStr))
                {
                    //destination.SetPassword("123");
                    destination.Open();
                    destination.ChangePassword("Cpk123");
                }



                return true;
            }catch(Exception ex)
            {return false; }
        }


    }
}
