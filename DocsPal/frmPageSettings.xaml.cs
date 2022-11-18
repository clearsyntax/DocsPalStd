using DocWriter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DocWriter
{
    /// <summary>
    /// Interaction logic for frmPageSettings.xaml
    /// </summary>
    /// 
    
    public partial class frmPageSettings : Window
    {
        public bool IsApplyed=false;
        public List<PageSetting> pageSetting = new List<PageSetting>(); 
        public frmPageSettings(List<PageSetting> pageSetting_)
        {
            InitializeComponent();
            pageSetting = pageSetting_;

            cmbFontFamily.Items.Add("Verdana");
            cmbFontFamily.Items.Add("Georgia");
            cmbFontFamily.Items.Add("Courier New");


            cmbFontStyle.Items.Add("Regular");
            cmbFontStyle.Items.Add("Italic");
            cmbFontStyle.Items.Add("Underline");


            try { txtFontSize.Text = (pageSetting.Where(x => x.SettingName == "FontSize").FirstOrDefault().SettingValue.ToString()); }
            catch { };
            try { cmbFontFamily.Text = pageSetting.Where(x => x.SettingName == "FontFamily").FirstOrDefault().SettingValue.ToString(); }
            catch { };
            try { cmbFontStyle.Text = (pageSetting.Where(x => x.SettingName == "FontStyle").FirstOrDefault().SettingValue.ToString()); }
            catch { };
            try { chkAllo2PrintWithoutSave.IsChecked = (pageSetting.Where(x => x.SettingName == "AllowToPrintWithoutSaving").FirstOrDefault().SettingValue.ToString())=="0"?false :true; }
            catch { };


            


            StringBuilder alignObjList = new StringBuilder();
            foreach (var x in pageSetting)
            {
                alignObjList.AppendLine(x.SettingName + " :\t " + x.SettingValue.ToString());
            }
            tblokPrintObjAlign.Text = alignObjList.ToString(); ; 

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string text = cmbFontFamily.Text;





                //BL.fontFamily = cmbFontFamily.Text.Trim();
                //BL.fontStyle = cmbFontStyle.Text.Trim();
                //BL.fontSize = Convert.ToInt16(txtFontSize.Text.Trim());

                
                //var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //config.AppSettings.Settings["FontFamily"].Value = cmbFontFamily.Text.Trim();
                //config.AppSettings.Settings["FontStyle"].Value = cmbFontStyle.Text.Trim();
                //config.AppSettings.Settings["FontSize"].Value = txtFontSize.Text.Trim();
                //config.Save(ConfigurationSaveMode.Modified);

                //ConfigurationManager.RefreshSection("appSettings");


                if (txtFontSize.Text.Trim().Length > 0)
                {
                    pageSetting.Where(w => w.SettingName == "FontSize").ToList().ForEach(s => s.SettingValue = txtFontSize.Text.Trim());
                }
                if (cmbFontFamily.Text.Trim().Length > 0)
                {
                    pageSetting.Where(w => w.SettingName == "FontFamily").ToList().ForEach(s => s.SettingValue = cmbFontFamily.Text.Trim());
                }
                if (cmbFontStyle.Text.Trim().Length > 0)
                {
                    pageSetting.Where(w => w.SettingName == "FontStyle").ToList().ForEach(s => s.SettingValue = cmbFontStyle.Text.Trim());
                }
                
                pageSetting.Where(w => w.SettingName == "AllowToPrintWithoutSaving").ToList().ForEach(s => s.SettingValue = (chkAllo2PrintWithoutSave.IsChecked == true)?"1":"0");
                    



                if (BL.AddPageSttings(pageSetting) == true)
                {
                    //MessageBox.Show("Data Updated Successfully ");
                    IsApplyed = true;
                }

                //System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\DefaultPrinter.txt", text);



                //this.Title = "Default Printer    :   " + cmbPrinterList.Text;

                //if (BL.fontFamily.Length > 2 && BL.fontStyle.Length > 0 && BL.fontSize >0)
                //{
                //    IsApplyed = true;
                //}
                this.Hide();
            }
            catch { }

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
