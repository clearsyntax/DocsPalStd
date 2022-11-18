using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
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
using System.Windows.Xps.Packaging;

namespace DocWriter
{
    /// <summary>
    /// Interaction logic for frmPrinterSettings.xaml
    /// </summary>
    public partial class frmPrinterSettings : Window
    {
        public frmPrinterSettings()
        {
            InitializeComponent();

            
            //tblockOsDefPrinter.Text =BL.OSDefultPrinter;

            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                cmbPrinterList.Items.Add(strPrinter);
                if (strPrinter == BL.AppDefultPrinter)
                {
                    cmbPrinterList.SelectedIndex = cmbPrinterList.Items.IndexOf(strPrinter);
                    //txtSystemDefultPrinter.Text = cmbDefaultPrinter.Text;
                }
            }

            if (BL.AppDefultPrinter == "")
            {
                cmbPrinterList.Text = strDefaultPrinter;
            }
            else
            {
                cmbPrinterList.Text = BL.AppDefultPrinter;
            }

            tblockAppDefPrinter.Text = cmbPrinterList.Text;
            //this.Title = "Default Printer    :   " + cmbPrinterList.Text;



        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {





            string text = cmbPrinterList.Text;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["AppDefaultPrinter"].Value = text;
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
            
            
            
            //System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\DefaultPrinter.txt", text);

            BL.AppDefultPrinter = text;


            //this.Title = "Default Printer    :   " + cmbPrinterList.Text;
            tblockAppDefPrinter.Text = cmbPrinterList.Text;
            //this.Hide();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
