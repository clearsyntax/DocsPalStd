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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Printing;
using System.Drawing.Printing;
using System.Windows.Xps.Packaging;
using System.Windows.Markup;
using System.IO;
using System.Xml;
using DocWriter.BusinessLogic;
using License;
using System.Threading;
using System.Windows.Threading;






namespace DocWriter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        string CompanyName_ = "";
        string Mode_ = "";

        List<PayeeMst> payeeList_ = new List<PayeeMst>();
        List<BankMst> bankList_ = new List<BankMst>();
        List<Drawer> drawerList_ = new List<Drawer>();


        List<PageSetting> pagesettings;
        int fontSize = 11;
        string fontFamily = "Verdana";
        string fontStyle = "Regular";
        string allowToPrintWithoutSaving = "0";

        public String[] WorLine;
        System.Drawing.Font font;
            

        public MainWindow()
        {
            InitializeComponent();
            tbxStatus.Text = "";
            tbxMessage.Text = "Initializing Database...";
            tbxVersion.Text = " |  Version: 2.0.0";

            Mode_ = "Create";


            string err_ = BL.IsDbConnectionOk();
            if (err_ != "")
            {
                tbxMessage.Text = err_;
                cmbPayTo.IsEditable = false;
                cmbBank.IsEditable = false;
                tabItemHome.IsEnabled = false;
                tabMenuTool.IsSelected = true;  

            }
            else
            {

                tbxStatus.Text = "Ready";
                tbxMessage.Text = "";
                BL bl = new BL();
                CompanyName_ = BL.CompanyName;
                BL.CompanyId = BL.getCompanyId(CompanyName_.ToString());
                tbxLastEntryDate.Text = " | App. Date: " + BL.LastEntryUpdatedOn(BL.CompanyId).ToString("dd-MMM-yy");   

                if (BL.IsCompanyActivated(BL.CompanyId)  == true)
                {
                    this.Title = this.Title + " - " + CompanyName_;
                }
                else
                {
                    //MessageBox.Show("Unregistered Company");
                    //CompanyName_ = "Unregistered Company";
                    this.Title = this.Title + " - " + CompanyName_ + " (Free Edition)";
                }


                //CompanyName_ = "COLORIGHT INDUSTRIES (PVT) LTD";
                //CompanyName_ = "BEAUTY PRODUCTS LANKA (PVT) LTD";


                dtpChequeDate.SelectedDate = DateTime.Now.Date;

                //tblockPayTo.LineHeight = 30;
                //string payto__ = (tblockPayTo.Text).Trim();
                //tblockPayTo.Text = new string(' ', 10) + payto__;







                tblockAcPayeeOnly.Text = "----------------------\nA/C PAYEE ONLY\n----------------------";

                tblockForCompanyName.Text = "For \n" + CompanyName_;

                UpdateCombo();

                cmbDrawer.Text = BL.DefaultDrawer;
                
                cmbBank.Text = BL.DefaultBank;


                pagesettings = new List<PageSetting>();
                if (cmbBank.SelectedIndex > -1)
                {
                    BankMst b = new BankMst();
                    b = (BankMst)cmbBank.SelectedItem;
                    pagesettings = BL.getPageSettings(b.BankId);
                }
                else
                {
                    pagesettings = BL.getPageSettings(0);
                }

                AssignFontStyleAndFamilyPageSettings();

                setPrintObjectLocation();


                
            }
        }




        void AssignFontStyleAndFamilyPageSettings()
        {
            
            //--------------------

            if (pagesettings.Count > 0)
            {
                try
                {
                    try { txtAcPayX.Text = pagesettings.Where(x => x.SettingName == "txtAcPayX").FirstOrDefault().SettingValue.ToString(); }
                    catch { };
                    try { txtAcPayY.Text = pagesettings.Where(x => x.SettingName == "txtAcPayY").FirstOrDefault().SettingValue.ToString(); }
                    catch { };
                    try { txtDateX.Text = pagesettings.Where(x => x.SettingName == "txtDateX").FirstOrDefault().SettingValue.ToString(); }
                    catch { }

                    try { txtDateY.Text = pagesettings.Where(x => x.SettingName == "txtDateY").FirstOrDefault().SettingValue.ToString(); }
                    catch { }

                    try { txtPaytoX.Text = pagesettings.Where(x => x.SettingName == "txtPaytoX").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { txtPaytoY.Text = pagesettings.Where(x => x.SettingName == "txtPaytoY").FirstOrDefault().SettingValue.ToString(); }
                    catch { }

                    try { txtAmtInWrdX.Text = pagesettings.Where(x => x.SettingName == "txtAmtInWrdX").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { txtAmtInWrdY.Text = pagesettings.Where(x => x.SettingName == "txtAmtInWrdY").FirstOrDefault().SettingValue.ToString(); }
                    catch { }

                    try { txtAmX.Text = pagesettings.Where(x => x.SettingName == "txtAmX").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { txtAmY.Text = pagesettings.Where(x => x.SettingName == "txtAmY").FirstOrDefault().SettingValue.ToString(); }
                    catch { }

                    try { txtForComNameX.Text = pagesettings.Where(x => x.SettingName == "txtForComNameX").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { txtForComNameY.Text = pagesettings.Where(x => x.SettingName == "txtForComNameY").FirstOrDefault().SettingValue.ToString(); }
                    catch { }

                    try { txtPayToWidth.Text = pagesettings.Where(x => x.SettingName == "txtPayToWidth").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { txtPayToStartSpace.Text = pagesettings.Where(x => x.SettingName == "txtPayToStartSpace").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { txtPaytoLineHeight.Text = pagesettings.Where(x => x.SettingName == "txtPaytoLineHeight").FirstOrDefault().SettingValue.ToString(); }
                    catch { }

                    try { txtAmtInWrdWidth.Text = pagesettings.Where(x => x.SettingName == "txtAmtInWrdWidth").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { txtAreaWidthDrawerName.Text = pagesettings.Where(x => x.SettingName == "txtAreaWidthDrawerName").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { txtAmtInWrdTextPerLine.Text = pagesettings.Where(x => x.SettingName == "txtAmtInWrdTextPerLine").FirstOrDefault().SettingValue.ToString(); }
                    catch { }

                    try { txtAmtInWrdStartSpace.Text = pagesettings.Where(x => x.SettingName == "txtAmtInWrdStartSpace").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { txtAmtInWrdLineHeight.Text = pagesettings.Where(x => x.SettingName == "txtAmtInWrdLineHeight").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { txtDateDigitSpace.Text = pagesettings.Where(x => x.SettingName == "txtDateDigitSpace").FirstOrDefault().SettingValue.ToString(); }
                    catch { }
                    try { chkAcPayeeOnly.IsChecked = pagesettings.Where(x => x.SettingName == "chkAcPayeeOnly").FirstOrDefault().SettingValue == "1" ? true : false; }
                    catch { }

                    try { chkForCompanyName.IsChecked = pagesettings.Where(x => x.SettingName == "chkForCompanyName").FirstOrDefault().SettingValue == "1" ? true : false; }
                    catch { }
                    try { chkCapsLock.IsChecked = pagesettings.Where(x => x.SettingName == "chkCapsLock").FirstOrDefault().SettingValue == "1" ? true : false; }
                    catch { }
                    try { chkWithDate.IsChecked = pagesettings.Where(x => x.SettingName == "chkWithDate").FirstOrDefault().SettingValue == "1" ? true : false; }
                    catch { }

                    try { fontSize = Convert.ToInt16(pagesettings.Where(x => x.SettingName == "FontSize").FirstOrDefault().SettingValue.ToString()); }
                    catch { };
                    try { fontFamily = pagesettings.Where(x => x.SettingName == "FontFamily").FirstOrDefault().SettingValue.ToString(); }
                    catch { };
                    try { fontStyle = (pagesettings.Where(x => x.SettingName == "FontStyle").FirstOrDefault().SettingValue.ToString()); }
                    catch { };
                    try { allowToPrintWithoutSaving = (pagesettings.Where(x => x.SettingName == "AllowToPrintWithoutSaving").FirstOrDefault().SettingValue.ToString()); }
                    catch { };


                    

                }
                catch (Exception e)
                {
                    LoadDefaultPageSttings();
                }
            }
            else
            {
                LoadDefaultPageSttings();
            }

            
            
            
            //----------------------------
            
            //int fontSize_ = BL.fontSize;
            

            try { fontSize = Convert.ToInt16(pagesettings.Where(x => x.SettingName == "FontSize").FirstOrDefault().SettingValue.ToString()); }
            catch { };

            //string fontFamily="Courier New";
            //string fontFamily = "Georgia";
            //string fontFamily = "Verdana";
            
            try { fontFamily = pagesettings.Where(x => x.SettingName == "FontFamily").FirstOrDefault().SettingValue.ToString(); }
            catch { };

            System.Drawing.FontStyle fontStyle_ = System.Drawing.FontStyle.Regular;
            string fontStyleCode = "R";

            try { fontStyleCode = (pagesettings.Where(x => x.SettingName == "FontStyle").FirstOrDefault().SettingValue.ToString()).Substring(0,1) ; }
            catch { };
                
            if (fontStyleCode == "R")
            {
                fontStyle_ = System.Drawing.FontStyle.Regular;
            }
            else if (fontStyleCode == "I")
            {
                fontStyle_ = System.Drawing.FontStyle.Italic;
            }
            else if (fontStyleCode == "U")
            {
                fontStyle_ = System.Drawing.FontStyle.Underline;
            }

            font = new System.Drawing.Font(fontFamily, fontSize, fontStyle_);


            tblockDay1.FontFamily = new FontFamily(fontFamily);
            tblockDay1.FontSize = fontSize;
            tblockDay2.FontFamily = new FontFamily(fontFamily);
            tblockDay2.FontSize = fontSize;
            tblockMonth1.FontFamily = new FontFamily(fontFamily);
            tblockMonth1.FontSize = fontSize;
            tblockMonth2.FontFamily = new FontFamily(fontFamily);
            tblockMonth2.FontSize = fontSize;
            tblockYear1.FontFamily = new FontFamily(fontFamily);
            tblockYear1.FontSize = fontSize;
            tblockYear2.FontFamily = new FontFamily(fontFamily);
            tblockYear2.FontSize = fontSize;


            tblockPayTo.FontFamily = new FontFamily(fontFamily);
            tblockPayTo.FontSize = fontSize;
            tblockAmtInWord.FontFamily = new FontFamily(fontFamily);
            tblockAmtInWord.FontSize = fontSize;

            //tblockAmtInWord.FontFamily = new FontFamily("Courier New");
            //tblockAmtInWord.FontSize = fintSize_;
            tblockAmt.FontFamily = new FontFamily(fontFamily);
            tblockAmt.FontSize = fontSize;
            tblockForCompanyName.FontFamily = new FontFamily(fontFamily);
            tblockForCompanyName.FontSize = fontSize;

        }

        void LoadDefaultPageSttings()
        {
            txtAcPayX.Text = "395";
            txtAcPayY.Text = "50";

            txtDateX.Text = "505";
            txtDateY.Text = "30";

            txtPaytoX.Text = "67";
            txtPaytoY.Text = "83";

            txtAmtInWrdX.Text = "67";
            txtAmtInWrdY.Text = "123";

            txtAmX.Text = "540";
            txtAmY.Text = "150";

            txtForComNameX.Text = "400";
            txtForComNameY.Text = "250";

            txtPayToWidth.Text = "625";
            txtPayToStartSpace.Text = "1";
            txtPaytoLineHeight.Text = "20";
            txtAmtInWrdWidth.Text = "325";
            txtAmtInWrdTextPerLine.Text = "38";
            txtAmtInWrdStartSpace.Text = "2";
            txtAmtInWrdLineHeight.Text = "30";
            txtDateDigitSpace.Text = "2";
            txtAreaWidthDrawerName.Text = "300";

            chkAcPayeeOnly.IsChecked = true;
            chkForCompanyName.IsChecked = true;
            chkCapsLock.IsChecked = true;
            chkWithDate.IsChecked = true;

            fontSize = 11;
            fontStyle = "Verdana";
            fontFamily = "Regular";
            
            setPrintObjectLocation();
        }

        void UpdateCombo()
        {
            try
            {

                BL bl = new BL();
                payeeList_ = bl.Payees;
                bankList_ = bl.Bank;
                drawerList_ = bl.Drawer;

                cmbPayTo.ItemsSource = payeeList_;


                cmbDrawer.ItemsSource = drawerList_;
                cmbBank.ItemsSource = bankList_;

                if (cmbDrawer.Items.Count < 1)
                {
                    cmbDrawer.Items.Add(CompanyName_);
                }
                if (cmbDrawer.Items.Count == 1)
                {
                    cmbDrawer.SelectedIndex = 0;
                    
                }
                else
                {
                    cmbDrawer.SelectedIndex = -1;
                }

                if (cmbBank.Items.Count == 1)
                {
                    cmbBank.SelectedIndex = 0;
                }
                else if (cmbBank.Items.Count == 1)
                {
                    cmbBank.Items.Add("Default");
                    cmbBank.SelectedIndex = 0;
                    
                }
                else
                {
                    cmbBank.SelectedIndex = -1;
                }
               
                


                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }



        private void TabItem_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Click");
        }

        private void tabHdrTboxHome_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //MessageBox.Show("change");
        }

        private void TabControl_GotFocus_1(object sender, RoutedEventArgs e)
        {
            if (tabMenu.SelectedIndex == 0)
            {

            }
        }




        //void setPrintObjectWidthAndSpaces()
        //{
        //    try
        //    {

        //        tblockPayTo.Width = Convert.ToDouble(txtPayToWidth.Text);
        //        string payto__ = (tblockPayTo.Text).Trim();
        //        tblockPayTo.Text = new string(' ', Convert.ToInt16(txtPayToStartSpace.Text)) + payto__;
        //        tblockPayTo.LineHeight = Convert.ToDouble((txtPaytoLineHeight.Text == "0") ? "1" : txtPaytoLineHeight.Text);

        //        tblockAmtInWord.Width = Convert.ToDouble(txtAmtInWrdWidth.Text);
        //        tblockAmtInWord.Text = new string(' ', Convert.ToInt16(txtAmtInWrdStartSpace.Text)) + (tblockAmtInWord.Text).Trim();
        //        tblockAmtInWord.LineHeight = Convert.ToDouble((txtAmtInWrdLineHeight.Text == "0") ? "1" : txtAmtInWrdLineHeight.Text);



        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("Invalid input");
        //    }

        //}


        //private void PrintVisual(Canvas  element, string description)
        //{

        //    PrintDialog printDlg = new System.Windows.Controls.PrintDialog();

        //    if (printDlg.ShowDialog() == true)
        //    {
        //        Size szOrg = new Size(element.ActualWidth, element.ActualHeight);
        //        System.Printing.PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);
        //        double scale = capabilities.PageImageableArea.ExtentWidth / element.ActualWidth;
        //        Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, element.ActualHeight);

        //        element.Measure(sz);
        //        element.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));


        //        //Capture the image of the visual in the same size as Printing page.  
        //        RenderTargetBitmap bmp = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, 96, 96, PixelFormats.Pbgra32);
        //        bmp.Render(element);

        //        FixedDocument myDocument = new FixedDocument();


        //        for (int offset = 0; offset < element.ActualHeight; offset += (int)capabilities.PageImageableArea.ExtentHeight)
        //        {
        //            DrawingVisual drawingVisual = new DrawingVisual();

        //            //create a drawing context so that image can be rendered to print  

        //            DrawingContext dc = drawingVisual.RenderOpen();

        //            dc.PushTransform(new TranslateTransform(0, -offset));
        //            dc.DrawImage(bmp, new System.Windows.Rect(sz));
        //            dc.Close();

        //            //now print the image visual to printer to fit on the one page.  

        //            PageContent pageContent = new PageContent();
        //            FixedPage page = new FixedPage();
        //            page.Width = capabilities.PageImageableArea.ExtentWidth;
        //            page.Height = capabilities.PageImageableArea.ExtentHeight;


        //            //container holds our visual...  
        //            VisualContainer myContainer = new VisualContainer();
        //            myContainer.AddVisual(drawingVisual);

        //            //add container to the page  
        //            page.Children.Add(myContainer);
        //            ((IAddChild)pageContent).AddChild(page);


        //            //add page to document  
        //            myDocument.Pages.Add(pageContent);

        //        }

        //        printDlg.PrintDocument(myDocument.DocumentPaginator, description);


        //        element.Measure(szOrg);

        //    }

        //}  


        private void cmbPayTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F8)
            {
                //PayeeMst p = new PayeeMst();
                //p = (PayeeMst)(cmbPayTo.SelectedValue);


                if (BL.DeletePayee(cmbPayTo.Text.Trim()) == true)
                {
                    UpdateCombo();
                }

            }
        }

        string DataValidation4SaveOrPrint(CheuqeDtl _cheuqeDtl)
        {
            if ((chkForCompanyName.IsChecked == true && cmbDrawer.SelectedIndex < 0))
            {
                return "Invalid Drawer";
            }
            if ((tblockPayTo.Text.Trim() ==""))
            {
                return "Invalid Payee to Name";
            }
            if (cmbBank.SelectedIndex < 0)
            {
                return "Invalid Bank Name";
            }

            try
            {
                if (cmbPayTo.SelectedIndex < 0)
                {
                    if (!BL.IsPayeeExisting(cmbPayTo.Text.Trim()))
                    {
                        PayeeMst p = new PayeeMst();
                        p.PayeeName = cmbPayTo.Text.Trim();
                        _cheuqeDtl.PayeeId= BL.AddPayee(p);
                        if (_cheuqeDtl.PayeeId>0)
                        {
                            UpdateCombo();
                        }
                    }
                }
            }
            catch { }



            
            return "";
        }


        private void menubtuSaveAndPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Thread thred = new Thread(() => SaveDtnPrint4Thr(2));
                thred.Start();


                //manubtuSave_Click(sender, e);
                //menubtuPrint_Click(sender, e);
                

                
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Some error, " + ex.Message.ToString());
            }

        }



        private void manubtuSave_Click(object sender, RoutedEventArgs e)
        {
           

            
            //Console.WriteLine(_IsDataValidate.ToString());
            //Thread.Sleep(5);


            Thread thred = new Thread(() => SaveDtnPrint4Thr(1));
            thred.Start();

            
            


        }

        void SaveDtnPrint4Thr(Int16 _PrintSaveOrBoth)
        {



            CheuqeDtl cheuqeDtl = new CheuqeDtl();
            Dispatcher.Invoke((Action)(()=>{cheuqeDtl = SetChque();})); 
            var _IsSaveData = "";


            //if (_PrintSaveOrBoth != 3 && cheuqeDtl.PayeeId == 0) // if print without saving 
            //{
            //    tbxMessage.Dispatcher.BeginInvoke((Action)(() => { tbxMessage.Text = "Data not save, Incorrect payee ID..."; }));
            //    return;
            //}



            if ((_PrintSaveOrBoth == 1 || _PrintSaveOrBoth == 2 || _PrintSaveOrBoth == 3) && cheuqeDtl.ChequeNo.Trim() != "")
            {
                if (_PrintSaveOrBoth == 3) // if print without saving 
                {
                    cheuqeDtl.PayeeId = 0; // remove the payee 
                    //cheuqeDtl.ChequeAmt = 0; // remove the chque amount
                    //cheuqeDtl.Amt = "";
                }
                Dispatcher.Invoke((Action)(() => { _IsSaveData = SaveData(cheuqeDtl); }));

                if (_IsSaveData == "")
                {
                    tbxMessage.Dispatcher.BeginInvoke((Action)(() => { tbxMessage.Text = "Data saved successfully..."; }));
                    //tbxMessage.Dispatcher.BeginInvoke((Action)(() => { tbxMessage.Text = "1"; }));
                    List<CheuqeDtl> l = new List<CheuqeDtl>();
                    l.Add(new CheuqeDtl {ChequeNo=cheuqeDtl.ChequeNo });
                    foreach(CheuqeDtl item in cmbChqNo.Items)
                    {
                        l.Add(item);
                    }
                    cmbChqNo.Dispatcher.BeginInvoke((Action)(() => { cmbChqNo.ItemsSource=l; }));
                    if (_PrintSaveOrBoth == 2)
                    {
                        //menubtuPrint_Click(sender, e);
                        Dispatcher.Invoke((Action)(() => { printCheque(); }));
                        
                    }

                    Dispatcher.Invoke((Action)(() => { ClearData(); }));

                }
                else
                {
                    tbxMessage.Dispatcher.BeginInvoke((Action)(() => { tbxMessage.Text = "Error: " + _IsSaveData; }));
                }

            }


                Thread.Sleep(2000);
                tbxMessage.Dispatcher.BeginInvoke(new Action(() =>
                {
                    tbxMessage.Text = "";
                }), DispatcherPriority.Background);


        }

        string SaveData(CheuqeDtl _cheuqeDtl)
        {
            //if (DataValidation4SaveOrPrint() == false)
            //{
            //    return false;
            //}
            var _resut="";
            this.Dispatcher.Invoke((Action)(() => { _resut = DataValidation4SaveOrPrint(_cheuqeDtl); }));
            if (_resut != "" )
            {
                

                //tbxMessage.Dispatcher.BeginInvoke((Action)(() => { tbxMessage.Text = _resut; }));
                //Thread.Sleep(1000);
                return _resut;
            }


            if (Mode_ == "Update")
            {
                if (BL.EditChequeEntry(_cheuqeDtl) == true)
                {
                    Mode_ = "Create";
                    return "";
                }

            }
            else
            {
                if (BL.AddCheque(_cheuqeDtl) == true)
                {
                    //ToolTip tooltip = new ToolTip { Content = "Data saved successfully" };
                    //frmMain.ToolTip = tooltip;
                    //tooltip.IsOpen = true;
                    //frmMain.ToolTip = "";
                    Mode_ = "Create";
                    return "";
                }
            }
             return "Some Error..." ;


        }

        void ClearData()
        {
            cmbPayTo.SelectedIndex = -1;
            cmbPayTo.Text = "";  
            txtChequeAmt.Text = "";
            cmbChqNo.SelectedIndex = -1;
            cmbChqNo.Text = "";
            txtRemarks.Text = "";
            cmbPayTo.Focus();
            cmbChqNo.ItemsSource = new List<CheuqeDtl>();
            chkWithDate.IsChecked = true;


            Mode_ = "Create";

            assignDetailsToCanvese();
            
 
 
        }


        private void menubtuPrint_Click(object sender, RoutedEventArgs e)
        {

            if (tblockPayTo.Text == "")
            {
                return;
            }
            
            if (cmbBank.SelectedIndex < 0)
            {
                MessageBox.Show("Invalid banks name...");
                return;
            }
            bool isChqExisting_ = false;
            int bankId = (cmbBank.SelectedIndex < 0) ? 0 : ((BankMst)cmbBank.SelectedItem).BankId;
            isChqExisting_ = BL.IsChequeExisting(BL.CompanyId, bankId, cmbChqNo.Text.Trim());
            if (isChqExisting_ == false)
            {
                if (allowToPrintWithoutSaving == "1") // if allowed print without saving
                {
                    //---------------------
                    try
                    {
                        Thread thred = new Thread(() => SaveDtnPrint4Thr(3));  // 3 is print without payee name and cheque amount
                        thred.Start();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Some error, " + ex.Message.ToString());
                    }
                    //-----------------





                }
                else  // if not allowed print without saving
                {

                    MessageBox.Show("Cheque not Existing .... ");
                    return;
                }
            }
            else
            {
                SetCheque(BL.CompanyId,(Int16)((BankMst)(cmbBank.SelectedItem)).BankId ,cmbChqNo.Text.Trim());
                printCheque();
                ClearData();
            }
        }

        void printCheque()
        {

            
            assignDetailsToCanvese();
            setPrintObjectLocation();
            

            


            string dfp_ = "";
            dfp_ = BL.AppDefultPrinter;
            if (dfp_ == "")
            {
                MessageBox.Show("Please Set a Printer First.");
                frmPrinterSettings p = new frmPrinterSettings();
                p.ShowDialog();
                dfp_ = BL.AppDefultPrinter;
                if (dfp_ == "")
                {
                    return;
                }
            }


            if (DataValidation4SaveOrPrint(SetChque()) != "")
            {
                return;
            }

            try
            {



                CheuqeDtl cheuqeDtl = new CheuqeDtl();
                Dispatcher.Invoke((Action)(() => { cheuqeDtl = SetChque(); }));
                
                Dispatcher.Invoke((Action)(() => { BL.UpdateChequePrintOn(cheuqeDtl.CompanyId, cheuqeDtl.BankId, cheuqeDtl.ChequeNo); }));

                frmPrntCheque pchq = new frmPrntCheque(SetPageSttings(), cheuqeDtl, dfp_, font);

                
                        
            

                //assignDetailsToCanvese();
                //setPrintObjectLocation();






                //canPrintArea.Background = new SolidColorBrush(Colors.Transparent);
                ////canPrintArea.Margin = new Thickness(0, 0, 0, 0);
                ////Canvas canv_ = new Canvas();
                //canv_ = new Canvas();


                //string saved = XamlWriter.Save(canPrintArea);
                //StringReader sReader = new StringReader(saved);
                //XmlReader xReader = XmlReader.Create(sReader);
                //Canvas newCanvas = (Canvas)XamlReader.Load(xReader);
                //canv_.Children.Add(newCanvas);

                
                
                
                //canv_.Background = new SolidColorBrush(Colors.Transparent);



                //Thickness beforeThikness_ = new Thickness();
                //beforeThikness_ = canPrintArea.Margin;



                //PrintDialog printDlg = new System.Windows.Controls.PrintDialog();

                //if (printDlg.ShowDialog() == true)
                //{
                //    //canPrintArea.Background = new SolidColorBrush(Colors.Transparent);
                //    //canPrintArea.Margin = new Thickness(274, 75, 0, 0);


                    
                    
                    
                    
                //    // scale = area.ExtentWidth and area.ExtentHeight and your UIElement's bounds
                //    // margin = area.OriginWidth and area.OriginHeight
                //    // 1. Use the scale in your ScaleTransform
                //    // 2. Use the margin and extent information to Measure and Arrange
                //    // 3. Print the visual


                //    PageMediaSize pageSize = null;
                //    pageSize = new PageMediaSize(PageMediaSizeName.NorthAmericaLetter);  // A4
                    
                //    //pageSize = new PageMediaSize(PageMediaSizeName.ISOA4);    
                //    printDlg.PrintTicket.PageMediaSize = pageSize;
                //    printDlg.PrintTicket.PageOrientation = PageOrientation.Landscape;
                //    canv_.Margin = new Thickness(274, 75, 0, 0);
                    

                    /*
                    //get selected printer capabilities
                    System.Printing.PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);
                    //get scale of the print wrt to screen of WPF visual
                    double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / canv_.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                                   this.ActualHeight);
                    //Transform the Visual to scale
                    this.LayoutTransform = new ScaleTransform((scale ), scale);
                    //get the size of the printer page
                    Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
                    //update the layout of the visual to the printer page size.
                    this.Measure(sz);
                    this.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));
                    //now print the visual to printer to fit on the one page.
                //    */
                //    printDlg.PrintVisual(canv_, "MyDoc_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                //}
                //canPrintArea.Margin = beforeThikness_;
                //canPrintArea.Background = new SolidColorBrush(Colors.LightBlue);

                
                //PrintDialog printDlg = new System.Windows.Controls.PrintDialog();

                //PageMediaSize pageSize = null;
                //bool bA4 = true;

                //if (bA4)
                //{
                //    pageSize = new PageMediaSize(PageMediaSizeName.NorthAmericaLetter);
                //}
                //else
                //{
                //    pageSize = new PageMediaSize(PageMediaSizeName.ISOA4);
                //}

                //printDlg.PrintTicket.PageMediaSize = pageSize;
                //printDlg.ShowDialog();

                //PrintTicket pt = printDlg.PrintTicket;
                //Double printableWidth = pt.PageMediaSize.Width.Value;
                //Double printableHeight = pt.PageMediaSize.Height.Value;

                //var pageSize2 = new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight);
                //if (printDlg.ShowDialog() == true)
                //{
                //    Size pageSize3 = new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight);
                //    canPrintArea.Measure(pageSize3);
                //    canPrintArea.Arrange(new Rect(5, 5, pageSize3.Width, pageSize3.Height));

                //    if (printDlg.ShowDialog() == true)
                //    {
                //        printDlg.PrintVisual(canPrintArea, "Printing Canvas");
                //    }

                //}
                //         PrintDialog dialog2 = new PrintDialog();
                //if (dialog2.ShowDialog() == true)
                //{
                //    dialog2.PrintTicket.PageOrientation = PageOrientation.Landscape;

                //    dialog2.PrintVisual(canPrintArea, "My Canvas"); 
                //}


                //return;
                //canPrintArea.Margin = new Thickness(0, 0, 0, 0);
                ////canPrintArea.Margin = new Thickness(0, 105 - (canPrintArea.Height  / 2), (148.45 * .9 - (canPrintArea.Width / 2)), 0);
                ////canPrintArea.Margin = new Thickness(110,0,0,0);




                //PrintDialog dialog = new PrintDialog();
                ////PageMediaSize pageSize = new PageMediaSize(PageMediaSizeName.Unknown, 210, 296.92);
                //if (dialog.ShowDialog() == true)
                //{
                //    dialog.PrintTicket.PageOrientation = PageOrientation.Landscape;


                //    PageMediaSize pageSize = new PageMediaSize(PageMediaSizeName.Unknown, 210, 296.92);
                //    //dialog.PrintTicket.PageMediaSize = pageSize;

                //    //Console.WriteLine(dialog.PrintableAreaHeight); // 816, good!
                //dialog.PrintQueue = myQueue;                   // selected from a combobox
                //Console.WriteLine(dialog.PrintableAreaHeight); // 1056 :(


                //    dialog.PrintVisual(canPrintArea, "MyDoc_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                //}


                /*
                PrintDialog printDlg = new System.Windows.Controls.PrintDialog();
                Thickness beforeThikness_ = new Thickness();
                beforeThikness_ = canPrintArea.Margin;
            
                //canPrintArea.Background = new SolidColorBrush(Colors.Transparent);


                if (printDlg.ShowDialog() == true)
                {
                    double hight_ = 90;
                    double width_ = 178;


                    printDlg.PrintTicket.PageMediaSize = new PageMediaSize(width_, hight_);
                    printDlg.PrintTicket.PageOrientation = PageOrientation.Landscape;

                    //get selected printer capabilities

                    System.Printing.PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);
                    //get scale of the print wrt to screen of WPF visual
                    //double scale = Math.Min((ActualWidth * .9), ActualHeight);
                    double scale = Math.Min((hight_ * .9), hight_);
                    //get the size of the printer page

                    //Size sz = new Size((Width * .9), hight_);
                    Size sz = new Size((width_), hight_);
                    //update the layout of the visual to the printer page size.

                    this.Measure(sz);
                    this.Arrange(new Rect(new Point(0, 0), sz));
                    //now print the visual to printer to fit on the one page.
                
                    printDlg.PrintVisual(canPrintArea, "Offset Calculations");


                
                
                    printDlg.PrintTicket.PageOrientation = PageOrientation.Landscape;
                    canPrintArea.Measure(new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight));
                    canPrintArea.Arrange(new Rect(new Point(0, 0), canPrintArea.DesiredSize));
                    printDlg.PrintVisual(canPrintArea,  "MyDoc_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                
                }
                canPrintArea.Margin = beforeThikness_;
                canPrintArea.Background = new SolidColorBrush(Colors.LightBlue);
                 */
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some error\n\n",ex.Message.ToString());
            }
        }



        void UpdateSettings()
        {
            try
            {
                if (BL.AddPageSttings(SetPageSttings()) == true)
                {
                    MessageBox.Show("Data Updated Successfully ");
                    setPrintObjectLocation();
                }

            }
            catch (Exception ex)
            {
            }
        }

        List<PageSetting> SetPageSttings()
        {
            if (cmbBank.SelectedIndex < 0)
            {
                return pagesettings;
            }

            List<PageSetting> l = new List<PageSetting>();
            try
            {

                



                BankMst  bank_ = new BankMst();
                bank_=(BankMst)cmbBank.SelectedItem;
                var bankId = bank_.BankId; 
 

                PageSetting p = new PageSetting();
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAcPayX", SettingValue = txtAcPayX.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAcPayY", SettingValue = txtAcPayY.Text.Trim() });

                l.Add(new PageSetting { BankId = bankId, SettingName = "txtDateX", SettingValue = txtDateX.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtDateY", SettingValue = txtDateY.Text.Trim() });

                l.Add(new PageSetting { BankId = bankId, SettingName = "txtPaytoX", SettingValue = txtPaytoX.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtPaytoY", SettingValue = txtPaytoY.Text.Trim() });

                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAmtInWrdX", SettingValue = txtAmtInWrdX.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAmtInWrdY", SettingValue = txtAmtInWrdY.Text.Trim() });

                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAmX", SettingValue = txtAmX.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAmY", SettingValue = txtAmY.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtForComNameX", SettingValue = txtForComNameX.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtForComNameY", SettingValue = txtForComNameY.Text.Trim() });

                l.Add(new PageSetting { BankId = bankId, SettingName = "txtPayToWidth", SettingValue = txtPayToWidth.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtPayToStartSpace", SettingValue = txtPayToStartSpace.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtPaytoLineHeight", SettingValue = txtPaytoLineHeight.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAmtInWrdWidth", SettingValue = txtAmtInWrdWidth.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAreaWidthDrawerName", SettingValue = txtAreaWidthDrawerName.Text.Trim() });

                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAmtInWrdTextPerLine", SettingValue = txtAmtInWrdTextPerLine.Text.Trim() });

                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAmtInWrdStartSpace", SettingValue = txtAmtInWrdStartSpace.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtAmtInWrdLineHeight", SettingValue = txtAmtInWrdLineHeight.Text.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "txtDateDigitSpace", SettingValue = txtDateDigitSpace.Text.Trim() });

                l.Add(new PageSetting { BankId = bankId, SettingName = "chkAcPayeeOnly", SettingValue = chkAcPayeeOnly.IsChecked == true ? "1" : "0" });
                l.Add(new PageSetting { BankId = bankId, SettingName = "chkForCompanyName", SettingValue = chkForCompanyName.IsChecked == true ? "1" : "0" });
                l.Add(new PageSetting { BankId = bankId, SettingName = "chkCapsLock", SettingValue = chkCapsLock.IsChecked == true ? "1" : "0" });
                l.Add(new PageSetting { BankId = bankId, SettingName = "chkWithDate", SettingValue = chkWithDate.IsChecked == true ? "1" : "0" });

                l.Add(new PageSetting { BankId = bankId, SettingName = "FontSize", SettingValue = fontSize.ToString().Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "FontFamily", SettingValue = fontFamily.Trim() });
                l.Add(new PageSetting { BankId = bankId, SettingName = "FontStyle", SettingValue = fontStyle });
                l.Add(new PageSetting { BankId = bankId, SettingName = "AllowToPrintWithoutSaving", SettingValue = allowToPrintWithoutSaving });

                

            }
            catch (Exception ex)
            {
            }
            return l;

        }


        CheuqeDtl SetChque()
        {
            CheuqeDtl cheuqeDtl = new CheuqeDtl();

            cheuqeDtl.CompanyId = BL.CompanyId; 
            cheuqeDtl.CompanyName = CompanyName_;
            cheuqeDtl.ChequeNo = (cmbChqNo.Text.Trim() == "") ? "" : cmbChqNo.Text.Trim();
            cheuqeDtl.Date = DateTime.Now.Date; 
            cheuqeDtl.ChequeDate = dtpChequeDate.SelectedDate.Value;

            cheuqeDtl.IsPrint = false;
            cheuqeDtl.PrintOn = DateTime.Now;
            cheuqeDtl.PayeeId = cmbPayTo.SelectedIndex <0? 0 : Convert.ToInt32(((PayeeMst)cmbPayTo.SelectedItem).PayeeId);
            cheuqeDtl.BankId =cmbBank.SelectedIndex <0?0: Convert.ToInt32(((BankMst)cmbBank.SelectedItem).BankId);
            cheuqeDtl.DrawerId =cmbDrawer.SelectedIndex<0?0:Convert.ToInt32(((Drawer)cmbDrawer.SelectedItem).DrawerId);

            cheuqeDtl.Remarks = txtRemarks.Text;
            cheuqeDtl.MdfOn = DateTime.Now;

            cheuqeDtl.IsAcPay = tblockAcPayeeOnly.Visibility == Visibility.Visible ? true : false;
            cheuqeDtl.IsPrintDate = tblockDay1.Visibility == Visibility.Visible ? true : false;
            cheuqeDtl.Day1 = tblockDay1.Text;
            cheuqeDtl.Day2 = tblockDay2.Text;
            cheuqeDtl.Month1 = tblockMonth1.Text;
            cheuqeDtl.Month2 = tblockMonth2.Text;
            cheuqeDtl.Year1 = tblockYear1.Text;
            cheuqeDtl.Year2 = tblockYear2.Text;

            //cheuqeDtl.PayTo1 = tblockPayTo.Text.TrimEnd();
            //cheuqeDtl.PayTo2 = "";

            int lien__ = 0;
            foreach (var tLine_ in TextUtils.GetLines(tblockPayTo))
            {
                switch (lien__)
                {
                    case 0:
                        cheuqeDtl.PayTo1 = tLine_;
                        lien__++;
                        break;
                    case 1:
                        cheuqeDtl.PayTo2 = tLine_;
                        lien__++;
                        break;

                }

            }


            lien__ = 0;
            foreach (var tLine_ in TextUtils.GetLines(tblockAmtInWord))
            {
                switch (lien__)
                {
                    case 0:
                        cheuqeDtl.AmtInWord1 = tLine_;
                        lien__++;
                        break;
                    case 1:
                        cheuqeDtl.AmtInWord2 = tLine_;
                        lien__++;
                        break;
                    case 2:
                        cheuqeDtl.AmtInWord3 = tLine_;
                        lien__++;
                        break;
                    case 3:
                        cheuqeDtl.AmtInWord4 = tLine_;
                        lien__++;
                        break;
                    case 4:
                        cheuqeDtl.AmtInWord4 +='\n'+ tLine_;
                        lien__++;
                        break;

                }
            }


            /*  ---
            string ss = tblockAmtInWord.Text.TrimEnd();

            int tperline_ =Convert.ToInt32(txtAmtInWrdTextPerLine.Text.Trim());  // defualt 38

            if (ss.Length > tperline_)
            {
                setRowText(ss, tperline_, 0);

            }
            else
            {
                cheuqeDtl.AmtInWord1 = ss;

            }


            if (ss.Length > tperline_)
            {
                for (int x = 0; x < WorLine.Length; x++)
                {
                    switch (x)
                    {
                        case 0:
                            if (WorLine[x] != null)
                            {
                                //cheuqeDtl.AmtInWord1 = BL.Justify(WorLine[x].TrimEnd(), WorLine[x].Length);
                                cheuqeDtl.AmtInWord1 = WorLine[x].ToString();

                            }
                            break;
                        case 1:
                            if (WorLine[x] != null)
                            {
                                cheuqeDtl.AmtInWord2 = BL.Justify(WorLine[x].Trim(), WorLine[x].Length);
                            }
                            break;
                        case 2:
                            if (WorLine[x] != null)
                            {

                                cheuqeDtl.AmtInWord3 = BL.Justify(WorLine[x].Trim(), WorLine[x].Length);
                            }
                            break;

                        case 3:
                            if (WorLine[x] != null)
                            {

                                cheuqeDtl.AmtInWord4 = BL.Justify(WorLine[x].Trim(), WorLine[x].Length);
                                cheuqeDtl.AmtInWord4 = BL.Justify(cheuqeDtl.AmtInWord4.Trim(), cheuqeDtl.AmtInWord4.Length);
                            }
                            break;


                        default:
                            if (WorLine[x] != null)
                            {
                                cheuqeDtl.AmtInWord3 += WorLine[x];
                            }
                            break;

                    }
             
                }
            

            
            }
            */


            if (chkCapsLock.IsChecked == true)
            {
                cheuqeDtl.PayTo1 = cheuqeDtl.PayTo1.ToUpper();
                cheuqeDtl.PayTo2 = cheuqeDtl.PayTo2 == null ? "" : cheuqeDtl.PayTo2.ToUpper();
                cheuqeDtl.AmtInWord1 = cheuqeDtl.AmtInWord1 == null ? "" : cheuqeDtl.AmtInWord1.ToUpper();
                cheuqeDtl.AmtInWord2 = cheuqeDtl.AmtInWord2 == null ? "" : cheuqeDtl.AmtInWord2.ToUpper();
                cheuqeDtl.AmtInWord3 = cheuqeDtl.AmtInWord3 == null ? "" : cheuqeDtl.AmtInWord3.ToUpper();
            }


            cheuqeDtl.Amt = tblockAmt.Text.Trim();
            cheuqeDtl.IsPrintComName = tblockForCompanyName.Visibility == Visibility.Visible ? true : false;
            cheuqeDtl.CompanyName = tblockForCompanyName.Text;

            cheuqeDtl.DrawerName = tblockForCompanyName.Text.Replace(((char)13), '\n');



            //Console.WriteLine(cheuqeDtl.AmtInWord1);
            //Console.WriteLine(cheuqeDtl.AmtInWord2); 

            return cheuqeDtl;
        }

        private void setRowText(String txt, int StdrowLenth, int JustFyRowLenth)
        {
            String RowText = "";
            String BalText = "";
            WorLine = new String[10];
            int x = 0;
            //first row

            while (txt.Length > StdrowLenth)
            {
                //for first line only
                int stdL = StdrowLenth;
                if (x == 0)
                {
                    stdL = StdrowLenth ;
                }
                else
                {
                    stdL = StdrowLenth;
                }
                //------------

                RowText = txt.Substring(0, stdL);

                if (RowText.LastIndexOf(' ') != stdL)
                {
                    JustFyRowLenth = RowText.LastIndexOf(' ');
                }
                else
                {
                    JustFyRowLenth = stdL;
                }



                //    RowText = txt.Substring(0, JustFyRowLenth);
                WorLine[x] = txt.Substring(0, JustFyRowLenth);
                float lgn = txt.Length;
                txt = txt.Substring(JustFyRowLenth);
                //lbl_amtInW1.Text = RowText;

                x++;
                WorLine[x] = txt;
            }



            //return RowText;
            //            return BalText;

        }



        private void txt_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)
            {
                setPrintObjectLocation();
            }
            else if (e.Key == Key.F4)
            {
                UpdateSettings();
            }
            else if (e.Key == Key.F12)
            {
                LoadDefaultPageSttings();
            }
            
            
        }

        private void txt1_KeyUp(object sender, KeyEventArgs e)
        {
            setPrintObjectLocation();
            if (e.Key == Key.F4)
            {
                UpdateSettings();
            }

        }

        private void txtChequeAmt_KeyUp(object sender, KeyEventArgs e)
        {
            assignDetailsToCanvese();
        }


        private void chkAcPayeeOnly_Click(object sender, RoutedEventArgs e)
        {
            if (chkAcPayeeOnly.IsChecked == true)
            {
                tblockAcPayeeOnly.Visibility = Visibility.Visible;

            }
            else
            {
                tblockAcPayeeOnly.Visibility = Visibility.Hidden;
            }
        }

        private void chkForCompanyName_Click(object sender, RoutedEventArgs e)
        {
            if (chkForCompanyName.IsChecked == true)
            {
                tblockForCompanyName.Visibility = Visibility.Visible;

            }
            else
            {
                tblockForCompanyName.Visibility = Visibility.Hidden;
            }

        }



        private void chkWithDate_Click(object sender, RoutedEventArgs e)
        {
            if (chkWithDate.IsChecked == true)
            {
                tblockDay1.Visibility = Visibility.Visible;
                tblockDay2.Visibility = Visibility.Visible;
                tblockMonth1.Visibility = Visibility.Visible;
                tblockMonth2.Visibility = Visibility.Visible;
                tblockYear1.Visibility = Visibility.Visible;
                tblockYear2.Visibility = Visibility.Visible;

            }
            else
            {
                tblockDay1.Visibility = Visibility.Hidden;
                tblockDay2.Visibility = Visibility.Hidden;
                tblockMonth1.Visibility = Visibility.Hidden;
                tblockMonth2.Visibility = Visibility.Hidden;
                tblockYear1.Visibility = Visibility.Hidden;
                tblockYear2.Visibility = Visibility.Hidden;

                dtpChequeDate.SelectedDate = DateTime.Now.Date;


            }
            
            
            
        }

        private void dtpChequeDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            assignDetailsToCanvese();
        }

        private void chkCapsLock_Click(object sender, RoutedEventArgs e)
        {
            assignDetailsToCanvese();
            setPrintObjectLocation();

        }
        



        void assignDetailsToCanvese()
        {

            if (cmbPayTo.Text.Trim() == "")
            {
                tblockDay1.Text = "";
                tblockDay2.Text = "";
                tblockMonth1.Text = "";
                tblockMonth2.Text = "";
                tblockYear1.Text = "";
                tblockYear2.Text = "";
                tblockPayTo.Text = "";
                tblockForCompanyName.Text = "";
                tblockAmtInWord.Text = "";
                tblockAmt.Text = "";
                return; 
            }

            tblockPayTo.Text = cmbPayTo.Text.Trim();

            string amtt = "0";
            NumberToEnglish n = new NumberToEnglish();

            tblockAmt.Text = txtChequeAmt.Text.Trim();
            if (BL.IsNumeric(txtChequeAmt.Text) == false)
            {
                tblockAmtInWord.Text = "";
                tblockAmt.Text = "0";
            }
            else
            {
                amtt = (Math.Round((Convert.ToDouble((BL.IsNumeric(txtChequeAmt.Text) == false || txtChequeAmt.Text == "") ? "0" : txtChequeAmt.Text)),2)).ToString(".00");
                tblockAmtInWord.Text = n.number2WordNew(amtt);

                tblockAmt.Text = (Math.Round((Convert.ToDouble((BL.IsNumeric(tblockAmt.Text) == false) ? "0" : tblockAmt.Text)), 2).ToString("#,###.00")).ToString().PadLeft(22, '*');
            }


            DateTime day_ = new DateTime();
            day_ = dtpChequeDate.SelectedDate.Value;

            tblockDay1.Text = day_.Day.ToString().PadLeft(2, '0').Substring(0, 1);
            tblockDay2.Text = day_.Day.ToString().PadLeft(2, '0').Substring(1, 1);
            tblockMonth1.Text = day_.Month.ToString().PadLeft(2, '0').Substring(0, 1);
            tblockMonth2.Text = day_.Month.ToString().PadLeft(2, '0').Substring(1, 1);
            tblockYear1.Text = day_.Year.ToString().Substring(2, 1);
            tblockYear2.Text = day_.Year.ToString().Substring(3, 1);

            tblockForCompanyName.Text = cmbDrawer.Text.Replace("\\n",((char)13).ToString());
            

            if (chkCapsLock.IsChecked == true)
            {
                tblockPayTo.Text = tblockPayTo.Text.ToUpper();
                tblockAmtInWord.Text = tblockAmtInWord.Text.ToUpper();

            }



        }

        void setPrintObjectLocation()
        {
            try
            {
                double dlftMrg_ = 60;

                tblockAcPayeeOnly.Margin = new Thickness(Convert.ToDouble(txtAcPayX.Text) - dlftMrg_ , Convert.ToDouble(txtAcPayY.Text), 0, 0);

                tblockDay1.Margin = new Thickness(Convert.ToDouble(txtDateX.Text) - dlftMrg_ , Convert.ToDouble(txtDateY.Text), 0, 0);
                tblockDay2.Margin = new Thickness((Convert.ToDouble(txtDateX.Text) + 20) - dlftMrg_ , Convert.ToDouble(txtDateY.Text), 0, 0);
                tblockMonth1.Margin = new Thickness((Convert.ToDouble(txtDateX.Text) + 45) - dlftMrg_, Convert.ToDouble(txtDateY.Text), 0, 0);
                tblockMonth2.Margin = new Thickness((Convert.ToDouble(txtDateX.Text) + 65)-dlftMrg_, Convert.ToDouble(txtDateY.Text), 0, 0);
                tblockYear1.Margin = new Thickness((Convert.ToDouble(txtDateX.Text) + 135)-dlftMrg_, Convert.ToDouble(txtDateY.Text), 0, 0);
                tblockYear2.Margin = new Thickness((Convert.ToDouble(txtDateX.Text) + 155)-dlftMrg_, Convert.ToDouble(txtDateY.Text), 0, 0);

                tblockPayTo.Margin = new Thickness(Convert.ToDouble(txtPaytoX.Text) - dlftMrg_ , Convert.ToDouble(txtPaytoY.Text), 0, 0);

                tblockAmtInWord.Margin = new Thickness(Convert.ToDouble(txtAmtInWrdX.Text) - dlftMrg_ , Convert.ToDouble(txtAmtInWrdY.Text), 0, 0);
                tblockAmt.Margin = new Thickness(Convert.ToDouble(txtAmX.Text) - dlftMrg_ , Convert.ToInt16(txtAmY.Text), 0, 0);
                tblockForCompanyName.Margin = new Thickness(Convert.ToDouble(txtForComNameX.Text) - dlftMrg_ , Convert.ToDouble(txtForComNameY.Text), 0, 0);

                tblockPayTo.Width = Convert.ToDouble(txtPayToWidth.Text);
                string payto__ = (tblockPayTo.Text).Trim();
                tblockPayTo.Text = new string(' ', Convert.ToInt16(Math.Floor(Convert.ToDouble (txtPayToStartSpace.Text)))) + payto__;
                tblockPayTo.LineHeight = Convert.ToDouble((txtPaytoLineHeight.Text == "0") ? "1" : txtPaytoLineHeight.Text);

                tblockAmtInWord.Width = Convert.ToDouble(txtAmtInWrdWidth.Text);
                string amtIwd__=tblockAmtInWord.Text.Trim();


                tblockForCompanyName.Width = Convert.ToDouble(txtAreaWidthDrawerName.Text); 
                
                tblockAmtInWord.Text = new string(' ', Convert.ToInt32(Math.Floor(Convert.ToDouble(txtAmtInWrdStartSpace.Text)))) + amtIwd__;
                tblockAmtInWord.LineHeight = Convert.ToDouble((txtAmtInWrdLineHeight.Text == "0") ? "1" : txtAmtInWrdLineHeight.Text);

                if (chkAcPayeeOnly.IsChecked == true)
                {
                    tblockAcPayeeOnly.Visibility = Visibility.Visible;
                }
                else
                {
                    tblockAcPayeeOnly.Visibility = Visibility.Hidden;
                }
                if (chkForCompanyName.IsChecked == true)
                {
                    tblockForCompanyName.Visibility = Visibility.Visible;
                }
                else
                {
                    tblockForCompanyName.Visibility = Visibility.Hidden;
                }
                if (chkWithDate.IsChecked == true)
                {
                    tblockDay1.Visibility = Visibility.Visible;
                    tblockDay2.Visibility = Visibility.Visible;
                    tblockMonth1.Visibility = Visibility.Visible;
                    tblockMonth2.Visibility = Visibility.Visible;
                    tblockYear1.Visibility = Visibility.Visible;
                    tblockYear1.Visibility = Visibility.Visible;

                }
                else
                {
                    tblockDay1.Visibility = Visibility.Hidden;
                    tblockDay2.Visibility = Visibility.Hidden;
                    tblockMonth1.Visibility = Visibility.Hidden;
                    tblockMonth2.Visibility = Visibility.Hidden;
                    tblockYear1.Visibility = Visibility.Hidden;
                    tblockYear1.Visibility = Visibility.Hidden;
                }



                

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Invalid input");
            }

        }


        private void cmbPayTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            assignDetailsToCanvese();
        }

        private void cmbPayTo_KeyUp(object sender, KeyEventArgs e)
        {
            assignDetailsToCanvese();
        }

        private void menubtuPageSetup_Click(object sender, RoutedEventArgs e)
        {
            frmPageSettings p = new frmPageSettings(SetPageSttings());
            p.Owner = this;
            p.ShowDialog();

            if (p.IsApplyed == true)
            {

                pagesettings = p.pageSetting;

                AssignFontStyleAndFamilyPageSettings();
            }
        }

        private void menubtuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void cmbPayTo_LostFocus(object sender, RoutedEventArgs e)
        {
            assignDetailsToCanvese();
            setPrintObjectLocation();
        }

        private void txtChequeAmt_LostFocus(object sender, RoutedEventArgs e)
        {
            
            assignDetailsToCanvese();
            setPrintObjectLocation();
            
        }

        private void dtpChequeDate_LostFocus(object sender, RoutedEventArgs e)
        {
            assignDetailsToCanvese();
            setPrintObjectLocation();
        }

        private void mnuBtnPrinterSetting_Click(object sender, RoutedEventArgs e)
        {
            frmPrinterSettings p = new frmPrinterSettings();
            p.Owner = this;
            p.ShowDialog(); 
        }

        private void mnuBtnSetting_Click(object sender, RoutedEventArgs e)
        {
            frmGeneralSettings g = new frmGeneralSettings();
            g.Owner = this;
            g.ShowDialog();
            UpdateCombo();
            
        }

        private void cmbDrawer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            assignDetailsToCanvese();
        }


        private void cmbDrawer_LostFocus(object sender, RoutedEventArgs e)
        {
            assignDetailsToCanvese();
            setPrintObjectLocation();
        }

        private void menubtuCounterFoil_Click(object sender, RoutedEventArgs e)
        {
            frmPrintView g = new frmPrintView("CF01", DateTime.Now.Date.AddDays(-30), DateTime.Now.Date);
            g.Owner = this;
            g.ShowDialog();

        }

        private void cmbBank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try{
            
            BankMst bank_ = new BankMst();
            bank_ = (BankMst)cmbBank.SelectedItem;

            List<PageSetting> pg_ = new List<PageSetting>();
            pg_ = BL.getPageSettings(Convert.ToInt16(bank_.BankId));
            if (pg_.Count > 0)
            {
                pagesettings = pg_;
                AssignFontStyleAndFamilyPageSettings();
            }
            }catch{}

        }

        private void frmMain_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }


        private void menubtuRefresh_Click(object sender, RoutedEventArgs e)
        {
            cmbPayTo.IsEnabled  = true ;
            cmbPayTo.MaxDropDownHeight = 300;

            if (bankList_.Count == 1)
            {
                cmbBank.SelectedIndex = 0;
            }
            else
            {
                cmbBank.SelectedIndex = -1;
            }

            //Thread t = new Thread(() => SetCheque());
            //t.Start();
            //t.Abort();
            ClearData();
            tbxMessage.Text = "";
            Mode_ = "Create";
        }



        private void menubtuSearch_Click(object sender, RoutedEventArgs e)
        {

            //cmbPayTo.IsEnabled = false;
            try
            {
                if (cmbChqNo.SelectedIndex > -1)
                {
                    if (cmbChqNo.SelectedItem != null)
                    {
                        CheuqeDtl chedtl__ = new CheuqeDtl();
                        chedtl__=(CheuqeDtl)cmbChqNo.SelectedItem;

                        Thread t = new Thread(() => SetCheque(chedtl__));
                        t.Start();
                        /*
                        Dispatcher.Invoke((Action)(() =>
                        {
                            SetCheque((CheuqeDtl)cmbChqNo.SelectedItem);
                        }));
                        */
                    }

                }

                else if (cmbChqNo.Text.Trim() != "")
                {
                    //Thread t = new Thread(() => SetCheque());
                    //t.Start();
                    //t.Abort(); 

                    //Thread t = new Thread(() => SetCheque(BL.CompanyId, (Int16)((BankMst)(cmbBank.SelectedItem)).BankId, cmbChqNo.Text.Trim()));
                    string chNo_ = cmbChqNo.Text.Trim();
                    Int16 bankId__ = (Int16)((BankMst)(cmbBank.SelectedItem)).BankId;
                    Thread t = new Thread(() => SetCheque(BL.CompanyId, bankId__, chNo_));
                    t.Start();

                    /*
                    Dispatcher.Invoke((Action)(() =>
                    {
                        SetCheque(BL.CompanyId,(Int16)((BankMst)(cmbBank.SelectedItem)).BankId,cmbChqNo.Text.Trim());
                    }));
                    */

                }
                else if (cmbPayTo.SelectedIndex > -1)
                {
                    DateTime dt = DateTime.Now.Date;
                    cmbChqNo.ItemsSource = (BL.getChequeList(BL.CompanyId,((PayeeMst)cmbPayTo.SelectedItem).PayeeId  ,100).ToList());
                }

                else
                {
                    DateTime dt = DateTime.Now.Date;
                    cmbChqNo.ItemsSource = (BL.getChequeList(BL.CompanyId, dt.AddMonths(-1), dt)).OrderByDescending(x => x.ChequeNo).ToList();
                }

                //assignDetailsToCanvese();

            }
            finally { }
        }


        void SetCheque(Int16 companyId_, Int16 bankaId_, string chequNo_)
        {
            CheuqeDtl o = new CheuqeDtl();
            


            //int _b = -1;
            //string chequNo_ = "";
            //Dispatcher.BeginInvoke((Action)(() => { chequNo_ = cmbChqNo.Text.Trim(); }));
            //Dispatcher.Invoke((Action)(() => { _b =((BankMst)cmbBank.SelectedItem).BankId; chequNo_ = cmbChqNo.Text.Trim(); }));


            if (companyId_< 0 || bankaId_ < 0 || chequNo_ == "")
            {
                tbxMessage.Dispatcher.BeginInvoke((Action)(() => tbxMessage.Text="Invalid bank name or Cheque no.." ));
                Thread.Sleep(2000);
                tbxMessage.Dispatcher.BeginInvoke((Action)(() => tbxMessage.Text = ""));
                return;
            }


            //int _b =((BankMst)cmbBank.SelectedItem).BankId; 

            Dispatcher.Invoke((Action)(() => { o = BL.getCheque(companyId_, bankaId_, chequNo_); }));

            //Dispatcher.Invoke((Action)(() => { cmbPayTo.Text = o.PayeeName; }));
            //Dispatcher.Invoke((Action)(() => { txtChequeAmt.Text = o.ChequeAmt.ToString();}));
            //Dispatcher.Invoke((Action)(() => { dtpChequeDate.SelectedDate = o.ChequeDate;}));
            //Dispatcher.Invoke((Action)(() => { cmbBank.Text = o.BankName;}));
            //Dispatcher.Invoke((Action)(() => { cmbDrawer.Text = o.DrawerName;}));
            //Dispatcher.Invoke((Action)(() => { txtRemarks.Text = o.Remarks;}));
            //Dispatcher.Invoke((Action)(() => { chkAcPayeeOnly.IsChecked = (o.IsAcPay == true) ? true : false;}));


            if (o.ChequeNo != null)
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    //cmbPayTo.FindName(o.PayeeName);
                    cmbPayTo.SelectedItem = payeeList_.Where(x => x.PayeeName == o.PayeeName).FirstOrDefault();
                    txtChequeAmt.Text = o.ChequeAmt.ToString();
                    dtpChequeDate.SelectedDate = o.ChequeDate;
                    //cmbBank.Text = o.BankName;
                    cmbBank.SelectedItem = bankList_.Where(x => x.BankName == o.BankName).FirstOrDefault();
                    cmbDrawer.SelectedItem = drawerList_.Where(x => x.DrawerName == o.DrawerName).FirstOrDefault();
                    txtRemarks.Text = o.Remarks;
                    chkAcPayeeOnly.IsChecked = (o.IsAcPay == true) ? true : false;
                    chkWithDate.IsChecked = (o.IsPrintDate == true) ? true : false;
                    tbxMessage.Text = "";
                    Mode_ = "Update";
                }));
            }
            else
            {
                Mode_ = "Create";
                tbxMessage.Dispatcher.BeginInvoke((Action)(() => tbxMessage.Text = "Invalid bank name or Cheque no.."));
                Thread.Sleep(2000);
                tbxMessage.Dispatcher.BeginInvoke((Action)(() => tbxMessage.Text = ""));

                Dispatcher.Invoke((Action)(() => { ClearData(); }));
                
            }

            Dispatcher.Invoke((Action)(()=>{ assignDetailsToCanvese();}));

        }


        void SetCheque(CheuqeDtl o)
        {
            //CheuqeDtl o = new CheuqeDtl();

            if (o == null)
            {
                tbxMessage.Dispatcher.BeginInvoke((Action)(() => tbxMessage.Text = "Invalid bank name or Cheque no.."));
                Thread.Sleep(2000);
                tbxMessage.Dispatcher.BeginInvoke((Action)(() => tbxMessage.Text = ""));
                //Dispatcher.Invoke((Action)(() => { ClearData(); }));
                return;
            }

            /*
            int _b = -1;
            string chequNo_ = "";
            //Dispatcher.Invoke((Action)(() => { _b = ((BankMst)cmbBank.SelectedItem).BankId; chequNo_ = o.ChequeNo.Trim(); }));


            if (_b < 0 || chequNo_ == "")
            {
                tbxMessage.Dispatcher.BeginInvoke((Action)(() => tbxMessage.Text = "Invalid bank name or Cheque no.."));
                Thread.Sleep(2000);
                tbxMessage.Dispatcher.BeginInvoke((Action)(() => tbxMessage.Text = ""));
                return;
            }
            */

            //int _b =((BankMst)cmbBank.SelectedItem).BankId; 

            //Dispatcher.Invoke((Action)(() => { o = BL.getCheque(BL.CompanyId, _b, chequNo_); }));

            //Dispatcher.Invoke((Action)(() => { cmbPayTo.Text = o.PayeeName; }));
            //Dispatcher.Invoke((Action)(() => { txtChequeAmt.Text = o.ChequeAmt.ToString();}));
            //Dispatcher.Invoke((Action)(() => { dtpChequeDate.SelectedDate = o.ChequeDate;}));
            //Dispatcher.Invoke((Action)(() => { cmbBank.Text = o.BankName;}));
            //Dispatcher.Invoke((Action)(() => { cmbDrawer.Text = o.DrawerName;}));
            //Dispatcher.Invoke((Action)(() => { txtRemarks.Text = o.Remarks;}));
            //Dispatcher.Invoke((Action)(() => { chkAcPayeeOnly.IsChecked = (o.IsAcPay == true) ? true : false;}));


            if (o.ChequeNo != null)
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    //cmbPayTo.FindName(o.PayeeName);
                    cmbPayTo.SelectedItem = payeeList_.Where(x => x.PayeeName == o.PayeeName).FirstOrDefault();
                    txtChequeAmt.Text = o.ChequeAmt.ToString();
                    dtpChequeDate.SelectedDate = o.ChequeDate;
                    cmbBank.SelectedItem = bankList_.Where(x => x.BankName == o.BankName).FirstOrDefault();
                    cmbDrawer.SelectedItem = drawerList_.Where(x => x.DrawerName == o.DrawerName).FirstOrDefault();
                    txtRemarks.Text = o.Remarks;
                    chkAcPayeeOnly.IsChecked = (o.IsAcPay == true) ? true : false;
                    chkWithDate.IsChecked = (o.IsPrintDate == true) ? true : false;
                    tbxMessage.Text = "";
                    Mode_ = "Update";
                }));

                
            }
            else
            {
                Mode_ = "Create";
                tbxMessage.Dispatcher.BeginInvoke((Action)(() => tbxMessage.Text = "Invalid bank name or Cheque no.."));
                Thread.Sleep(2000);
                tbxMessage.Dispatcher.BeginInvoke((Action)(() => tbxMessage.Text = ""));

                Dispatcher.Invoke((Action)(() => { ClearData(); }));

            }

            Dispatcher.Invoke((Action)(() => { assignDetailsToCanvese(); }));

        }

        private void menubtuPostDatedCheque_Click(object sender, RoutedEventArgs e)
        {
            frmPrintView g = new frmPrintView("PD01", DateTime.Now.Date, DateTime.Now.Date);
            g.Owner = this;
            g.ShowDialog();

        }

        private void mnuBtnLicenseInfo_Click(object sender, RoutedEventArgs e)
        {
            frmLicenseInformation p = new frmLicenseInformation();
            p.Owner = this;
            p.ShowDialog(); 

        }

        private void mnuBtnDbMaint_Click(object sender, RoutedEventArgs e)
        {
            frmBackupNDbClean p = new frmBackupNDbClean();
            p.Owner = this;
            p.ShowDialog(); 

        }

        private void cmbChqNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            var che_ = ((CheuqeDtl)cmbChqNo.SelectedItem);

            cmbPayTo.SelectedIndex = -1;
            txtChequeAmt.Text = "";
 
            
            //if (cmbPayTo.IsEnabled == false )
            //{
            //if (che_!= null)
            //if (cmbChqNo.SelectedIndex > 0 || cmbChqNo.Text.Trim() != "" )
            //{
                Thread t = new Thread(() => SetCheque(che_));
                t.Start();
            //}
            //}

                assignDetailsToCanvese();
        }


    }
}
