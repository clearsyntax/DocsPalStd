using DocWriter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DocWriter
{
    /// <summary>
    /// Interaction logic for frmGeneralSettings.xaml
    /// </summary>
    public partial class frmGeneralSettings : Window
    {
 

        public frmGeneralSettings()
        {
            InitializeComponent();
            txtCompanyName.Text = BL.CompanyName;
            UpdateCombo();
            btnTextConnection.IsEnabled = false;

            txtDbFolderPath.Text = BL.getAppConfigDBDirectoryName();
            if (txtDbFolderPath.Text == "")
            {
                rbtnLocalFolder.IsChecked = true;
            }
            else
            {
                rbtnSharedLocation.IsChecked = true; 
            }

        }

        void UpdateCombo()
        {
            try
            {

                BL bl = new BL();
                cmbDrawerName.ItemsSource = bl.Drawer;
                
                cmbBank.ItemsSource = bl.Bank;
                

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }

        private void cmbDrawerName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F8)
            {
                Drawer o = new Drawer();
                o =(Drawer) cmbDrawerName.SelectedValue;  

                if (BL.DeleteDrawer(o) == true)
                {
                    UpdateCombo();
                }

            }
        }

        private void cmbBank_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F8)
            {
                BankMst o = new BankMst();
                o = (BankMst)cmbBank.SelectedValue; 
                if (BL.DeleteBank(o) == true)
                {
                    UpdateCombo();
                }
            }
        }

        private void btuSave_Click(object sender, RoutedEventArgs e)
        {
            if (BL.IsCompanyActivated(BL.CompanyId) == false)
            {
                System.Windows.MessageBox.Show("The product not activated, please activate first...!");
                return;
            }

            //--------- update Company Name
            if (tabGeneral.IsSelected == true)
            {
                try
                {
                    if (txtCompanyName.Text.Trim() != "" )
                    {
                        string text = txtCompanyName.Text.Trim();
                        BL.EditCompanyName(BL.CompanyId, text);

                        var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                        config.AppSettings.Settings["CompanyName"].Value = text;
                        config.Save(System.Configuration.ConfigurationSaveMode.Modified);
                        System.Configuration.ConfigurationManager.RefreshSection("appSettings");

                        //System.Windows.MessageBox.Show("Company Name Validation Success");

                    }
                    //-------------
                }
                catch
                {
                    System.Windows.MessageBox.Show("Error in Company Name Updation");
                }
            }

            //--------- update Drawer
            if (tabDrawer.IsSelected == true)
            {
                try
                {
                    if (cmbDrawerName.SelectedIndex < 0 && cmbDrawerName.Text.Trim() != "")
                    {
                        Drawer drawer = new Drawer();
                        drawer.DrawerName = cmbDrawerName.Text.Trim();
                        if (BL.AddDrawer(drawer) == true)
                        {
                            UpdateCombo();
                        }
                    }
                    if (chkDefaultDrawer.IsChecked == true)
                    {
                        var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                        config.AppSettings.Settings["DefaultDrawer"].Value = cmbDrawerName.Text.Trim();
                        config.Save(System.Configuration.ConfigurationSaveMode.Modified);
                        System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                        chkDefaultDrawer.IsChecked = false;
                    }



                }
                catch
                {
                    System.Windows.MessageBox.Show("Error in Drawer Name Updation");
                }
            }
            //----------------

            //--------- update Bank
            if (tabBank.IsSelected == true)
            {
                try
                {
                    if (cmbBank.Text.Trim() != "")
                    {
                        BankMst o = new BankMst();
                        o.BankName = cmbBank.Text.Trim();
                        o.BankAddress = txtBankAddress.Text.Trim();
                        if (BL.AddBank(o) == true)
                        {
                            UpdateCombo();
                        }
                        if (chkSetDefaultBank.IsChecked == true)
                        {
                            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                            config.AppSettings.Settings["DefaultBank"].Value = cmbBank.Text.Trim();
                            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
                            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                            chkSetDefaultBank.IsChecked = false;
                        }



                    }
                }
                catch
                {
                    System.Windows.MessageBox.Show("Error in Bank Updation");
                }
            }
            //----------------



            //--------- Update DB Connections
            try
            {
                if (tabDBConnection.IsSelected==true )
                {
                    var DBFolder_ = "";

                    if (rbtnLocalFolder.IsChecked == true)
                    {
                        DBFolder_ = "";
                    }
                    else
                    {
                        DBFolder_ = txtDbFolderPath.Text.Trim();  
                    }
                        var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                        config.AppSettings.Settings["DbDirectory"].Value = DBFolder_;
                        config.Save(System.Configuration.ConfigurationSaveMode.Modified);
                        System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                        BL._dbpath = DBFolder_;
                        btnTextConnection.IsEnabled = true;
                        

                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Error in Bank Updation");
            }
            //----------------


            //this.Hide();
        }

        private void btuCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void cmbBank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BankMst bm_=new BankMst();
            bm_=(BankMst)cmbBank.SelectedItem;
            if (bm_!=null)
            {
                txtBankAddress.Text = bm_.BankAddress;
            }
        }

        private void rbtnSharedLocation_Checked(object sender, RoutedEventArgs e)
        {
            txtDbFolderPath.IsReadOnly = false;
            btnSelectFolder.IsEnabled = true;
            txtDbFolderPath.Text = BL.getAppConfigDBDirectoryName(); 
        }

        private void rbtnLocalFolder_Checked(object sender, RoutedEventArgs e)
        {
            txtDbFolderPath.Text = "";
            txtDbFolderPath.IsReadOnly = true;
            //btnSelectFolder.IsEnabled = false;

  
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {


            //// Create OpenFileDialog
            //Microsoft.Win32.FileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            //// Launch OpenFileDialog by calling ShowDialog method
            //Nullable<bool> result = openFileDlg.ShowDialog();
            //// Get the selected file name and display in a TextBox.
            //// Load content of file in a TextBlock
            //if (result == true)
            //{
            //    txtDbFolderPath.Text = openFileDlg.FileName;
            //    //TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            //}


            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtDbFolderPath.Text = dialog.SelectedPath;
            }

        
        }

        private void btnTextConnection_Click(object sender, RoutedEventArgs e)
        {
            var msg=BL.IsDbConnectionOk();
            tblkTestConnMsg.Text = msg == "" ? "Connection Success.." : msg;
            btnTextConnection.IsEnabled = false;
        }


    }
}
