using System;
using System.Collections.Generic;
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
using DocWriter.BusinessLogic;
using System.Threading;

namespace DocWriter
{
    /// <summary>
    /// Interaction logic for frmBackupNDbClean.xaml
    /// </summary>
    public partial class frmBackupNDbClean : Window
    {
        List<CheuqeDtl> listCheuqeDtl = new List<CheuqeDtl>();
        static bool IsAbort_=false;
        bool delmsterData_ = false;

        public frmBackupNDbClean()
        {
            InitializeComponent();
            txtBackupLocation.Text = BL.getAppConfigDBDirectoryName();
            btnDelete.IsEnabled = false;
            //btnGetData.IsEnabled = false;
            dtpDelDtaOlderThanDt.SelectedDate = BL.LastEntryUpdatedOn(BL.CompanyId).AddYears(-1) ;

            btnAbortDelete.Visibility = Visibility.Hidden; 

            BL.LastEntryUpdatedOn(BL.CompanyId).ToString("dd-MMM-yy");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            if (txtBackupLocation.Text.Trim() == "")
            {
                MessageBox.Show("Invalid destination location...");
                return;
            }

            if (BL.BackupDB(txtBackupLocation.Text.Trim()) == true)
            {
                //btnDelete.IsEnabled = true;
                btnBackup.IsEnabled = false;
                btnGetData.IsEnabled = true; 
                lblMassage.Content = "Backup successfully...";
            }
            else
            {
                btnDelete.IsEnabled = false ;
                btnBackup.IsEnabled = true ;
                lblMassage.Content = "Error, Backup no successfully...";
            }
        }

        private void btnFindBackupLocation_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                txtBackupLocation.Text = dialog.SelectedPath;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            IsAbort_ = false;
            btnDelete.Visibility = Visibility.Hidden;
            btnAbortDelete.Visibility = Visibility.Visible;


            Thread thread = new Thread(DeleteRecords);
            thread.Start();


        }


        void DeleteRecords()
        {
            foreach (CheuqeDtl o in listCheuqeDtl.OrderBy(x=> x.ChequeDate))
            {
                if (BL.DeleteChequeEntry(o.CompanyId, o.SrNo) == true)
                {
                    listCheuqeDtl.Remove(o);
                    if (IsAbort_ == true)
                    {
                        return;
                    }
                    lblMassage.Dispatcher.BeginInvoke((Action)(() => lblMassage.Content = "Deleting....  Cheue No:" + o.ChequeNo.ToString()+" | Date: "+o.ChequeDate.ToString("dd-MMM-yyyy") ));
                    //Thread.Sleep(2);
                }
            }


            //------ remove matster data 
            
            if (delmsterData_ == true)
            {
                List<PayeeMst> listpayeemst = new List<PayeeMst>();
                BL bl = new BL();
                listpayeemst = bl.Payees;

                foreach (PayeeMst o in listpayeemst)
                {
                    if(BL.DeletePayeeIfNotExistingInCB(o.PayeeId)==true)
                    {
                        //listpayeemst.Remove(o);
                        if (IsAbort_ == true)
                        {
                            return;
                        }
                        lblMassage.Dispatcher.BeginInvoke((Action)(() => lblMassage.Content = "Deleting....  Payee :" + o.PayeeName));
                        Thread.Sleep(20);

                        
                    }
                }

            }

            lblMassage.Dispatcher.BeginInvoke((Action)(() => lblMassage.Content = "Process completed… "));

            btnDelete.Dispatcher.BeginInvoke((Action)(() => { btnDelete.Visibility = Visibility.Visible; }));
            btnAbortDelete.Dispatcher.BeginInvoke((Action)(() => { btnAbortDelete.Visibility = Visibility.Hidden; }));


        }





        

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            listCheuqeDtl = new List<CheuqeDtl>();
            //Thread t = new Thread(() => { lblMassage.Dispatcher.BeginInvoke((Action)(() => lblMassage.Content = GetData4Delete())); });
            //Thread t = new Thread(GetData4Delete);
            //t.Start();

            btnGetData.IsEnabled = false;
            lblMassage.Content = "Please wait...!";
            listCheuqeDtl = BL.getChequeList(BL.CompanyId, new DateTime(1900, 1, 1), dtpDelDtaOlderThanDt.SelectedDate.Value);
            lblMassage.Content = "List item(s) : " + listCheuqeDtl.Count().ToString();
            btnDelete.IsEnabled = true;
        }
        private string GetData4Delete()
        {
            lblMassage.Dispatcher.BeginInvoke((Action)(() => lblMassage.Content = "Please wait...!"));
            Thread.Sleep(2);
            DateTime stdt = new DateTime (1999,1,1);
            dtpDelDtaOlderThanDt.Dispatcher.BeginInvoke((Action)(() => stdt = dtpDelDtaOlderThanDt.SelectedDate.Value));
            
            listCheuqeDtl = BL.getChequeList(BL.CompanyId, new DateTime(1900, 1, 1), stdt);
            
            lblMassage.Dispatcher.BeginInvoke((Action)(() => lblMassage.Content = "List item(s) : " + listCheuqeDtl.Count().ToString()));
            Thread.Sleep(2);
            btnGetData.Dispatcher.BeginInvoke((Action)(() => btnGetData.IsEnabled =false));
            btnDelete.Dispatcher.BeginInvoke((Action)(() => btnDelete.IsEnabled = true));

            
            return listCheuqeDtl.Count().ToString();
        }

        private void btnAbortDelete_Click(object sender, RoutedEventArgs e)
        {
            
            btnDelete.Visibility = Visibility.Visible ;
            btnAbortDelete.Visibility = Visibility.Hidden;
            IsAbort_ = true;

        }

        private void chkIncNrMstDt_Checked(object sender, RoutedEventArgs e)
        {
            delmsterData_ = chkIncNrMstDt.IsChecked.Value;
        }
    }
}
