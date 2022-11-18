using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
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
using DocWriter.BusinessLogic;

namespace DocWriter
{
    /// <summary>
    /// Interaction logic for frmLicenseInformation.xaml
    /// </summary>
    public partial class frmLicenseInformation : Window
    {


        public frmLicenseInformation()
        {
            InitializeComponent();

            //txtComCode.Text = GetCupInfo();
            btnGenActiveCode.Visibility = Visibility.Hidden; 

            txtComCode.Text = BusinessLogic.HardwareID.GET_HARDWAREID.ToUpper();
            txtComCode.IsReadOnly = true;

            if (BL.IsCompanyActivated(BL.CompanyId) == true)
            {
                btnActiveRemoveLicense.IsEnabled  = false;
                btnRemoveLicense.IsEnabled = true;
                BL bl = new BL();
                var co = (CompanyInfo)bl.CompanyInfo;
                txtActivationCode.Text = co.ActivateCode; 
            }
            else
            {
                btnActiveRemoveLicense.IsEnabled = true;
                btnRemoveLicense.IsEnabled = false ;
            }



            var id = Guid.NewGuid().ToString();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnGenActiveCode_Click(object sender, RoutedEventArgs e)
        {
            txtActivationCode.Text = BusinessLogic.HardwareID.convert2FormatedKey(StringCipher.Encrypt(txtComCode.Text, "ClearSyntax")).ToUpper();
        }

        private void btnActiveRemoveLicense_Click(object sender, RoutedEventArgs e)
        {

            if (txtActivationCode.Text.Trim() == "") { return; }

            if (BL.CompanyActivationRemovingLicense(BL.CompanyId, txtActivationCode.Text.Trim(),true) == true)
            {
                if (BL.IsCompanyActivated(BL.CompanyId) == true)
                {
                    btnActiveRemoveLicense.IsEnabled = false;
                    btnRemoveLicense.IsEnabled = true;
                    txtComCode.IsReadOnly = true;
                    btnGenActiveCode.Visibility = Visibility.Hidden;
                }

            }
        }

        private void btnRemoveLicense_Click(object sender, RoutedEventArgs e)
        {

            if (BL.IsProductCodeGenuine(txtActivationCode.Text.Trim()) == true)
            {

                if (BL.CompanyActivationRemovingLicense(BL.CompanyId, txtActivationCode.Text.Trim(), false) == true)
                {
                    btnActiveRemoveLicense.IsEnabled = true;
                    btnRemoveLicense.IsEnabled = false;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Activation code not valied or invalid host...!");
            }

        }

        private void txtComCode_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.F11))
            {
                txtComCode.IsReadOnly  = false;
                btnGenActiveCode.Visibility = Visibility.Visible; 
            }
        }

        private void txtActivationCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnActiveRemoveLicense.IsEnabled == false)
            {
                if (txtActivationCode.Text.Trim() == "")
                {
                    btnActiveRemoveLicense.IsEnabled = true;
                    btnRemoveLicense.IsEnabled = false; 
                }
            }
        }
    }


}
