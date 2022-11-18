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
using System.IO;
using System.Diagnostics;
using System.Threading;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace DocWriter
{
    /// <summary>
    /// Interaction logic for frmPrintView.xaml
    /// </summary>
    public partial class frmPrintView : Window
    {
        string ReportCode = "";
        public frmPrintView()
        {
            InitializeComponent();
        }

        public frmPrintView(string _ReportCode,DateTime _FromDate,DateTime _ToDate)
        {
            InitializeComponent();
            ReportCode = _ReportCode;
            dtpFromDate.SelectedDate  = _FromDate;
            dtpToDate.SelectedDate = _ToDate;

            rTextBox.Document.Blocks.Clear();
            string _s = GetReport(ReportCode, dtpFromDate.SelectedDate.Value, dtpToDate.SelectedDate.Value ).Replace("##","") ;
            rTextBox.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(_s.Replace("##",""))));

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }


        string GetReport(string rptCode, DateTime fromDt_, DateTime toDt_)
        {
            fromDt_ = fromDt_.Date;
            toDt_ = toDt_.Date;
            string RunOnAndDataR_ = "Run on : " + DateTime.Now.ToString() ;

            string PrintRptName_ = "";

            // report wedth 81 if fint size 10
            // report wedth 98 if fint size 9
            string s_ = "";
            switch (rptCode)
            {
                case "CF01":  // Counter foil
                    PrintRptName_ = "Report Name: Counterfoil";
                    s_ += BL.CompanyName + "\n";
                    s_ += PrintRptName_ + "\n";
                    s_ += "Period : " + fromDt_.ToString("dd-MMM-yyyy") + "  to  " + toDt_.ToString("dd-MMM-yyyy") + "\n";


                    s_ += RunOnAndDataR_ + "\n";
                    s_ += "".PadRight(91, '-') + "\n";
                    s_ += @"Sr".PadRight(4) + "##" + "ChequeNo".PadRight(10) + "##" + "Date".PadRight(10) 
                        + "##" + "CheqDate".PadRight(10) + "##"
                         + "PayeeName".PadRight(44) + "##" + "ChequeAmt".PadLeft(14) + "##"  ;
                    s_ += "  IsPrint".PadRight(10) + "##" + " AC".PadRight(5) + "##" + "Remarks".PadRight(10) + "##" + "\n";
                    s_ += "".PadRight(91, '-') + "\n";

                    List<CheuqeDtl> dm_ = new List<CheuqeDtl>();
                    dm_ = BL.getChequeList(BL.CompanyId , fromDt_, toDt_).ToList();
                    int bId_=-1;
                    int drId_ = -1;
                    int slNo_ = 0;
                    foreach (CheuqeDtl q in dm_.OrderBy(c => c.BankId).ThenBy(c => c.ChequeNo))
                    {
                        if (q.BankId != bId_ || drId_ != q.DrawerId)
                        {
                            if (drId_ != -1)
                            {
                                s_ += "".PadRight(91, '-') + "\n";
                            }
                            s_ += "##".PadRight(4) +"Bank and D. Name: "+ q.BankName.ToString() +" (" + q.DrawerName + ")\n";
                            s_ += "".PadRight(91, '-') + "\n";
                            bId_ = q.BankId;
                            drId_ = q.DrawerId;
                            slNo_ = 0;

                        }
                        slNo_++;
                        s_ += (slNo_.ToString() + ")").PadRight(4) + "##";
                        s_ += q.ChequeNo.ToString().PadRight(10) + "##";
                        s_ += q.Date.ToString("dd-MMM-yy").PadRight(10) + "##";
                        s_ += q.ChequeDate.ToString("dd-MMM-yy").PadRight(10) + "##";
                        s_ += q.PayeeName.Substring(0, q.PayeeName.Length > 44 ? 44 : q.PayeeName.Length).PadRight(44)+"##";
                        s_ += q.ChequeAmt.ToString("#,###.00").PadLeft(14) + "##" ;
                        s_ +="  "+ ((q.IsPrint == true || q.PayeeId == 0) ? q.PrintOn.ToString("dd-MMM-yy") : "N").PadRight(10) + "##";
                        s_ += (q.IsAcPay == true ? "Y" : "N").PadRight(5) + "##";
                        s_ +=  (q.Remarks.Length> 45? q.Remarks.Substring(0, 45) :q.Remarks) + "\n"; 

                    }
                    //s_ += "".PadRight(110, '-') + "\n";
                    //s_ += "Total STI Qty :".PadRight(25) + "##" + dm_.Select(c => c.vRefDocTypeCode.ToUpper() == "STI" ? c.StockIn : 0).Sum().ToString("#,###.00").PadLeft(12) + "\n" + "\n";
                    //s_ += "Total Other In Qty :".PadRight(25) + "##" + dm_.Select(c => c.vRefDocTypeCode.ToUpper() != "STI" ? c.StockIn : 0).Sum().ToString("#,###.00").PadLeft(12) + "\n";
                    //s_ += "Total Invoice Qty :".PadRight(25) + "##" + dm_.Select(c => c.vRefDocTypeCode.ToUpper() == "INV" ? c.StockOut : 0).Sum().ToString("#,###.00").PadLeft(12) + "\n";
                    //s_ += "Total Other Out Qty :".PadRight(25) + "##" + dm_.Select(c => c.vRefDocTypeCode.ToUpper() != "INV" ? c.StockOut : 0).Sum().ToString("#,###.00").PadLeft(12) + "\n";
                    //s_ += "Total Gross Invoice Amt :".PadRight(25) + "##" + dm_.Select(c => c.nGrossTotal).Sum().ToString("#,###.00").PadLeft(12) + "\n";
                    //s_ += "Total Discount Amt :".PadRight(25) + "##" + dm_.Select(c => c.nDiscAmt).Sum().ToString("#,###.00").PadLeft(12) + "\n";
                    //s_ += "Total Net Invoice Amt :".PadRight(25) + "##" + dm_.Select(c => c.vRefDocTypeCode.ToUpper() == "INV" ? c.nTotalAmount : 0).Sum().ToString("#,###.00").PadLeft(12) + "\n";

                    ////var sss = dm_.Where(c => c.vRefDocTypeCode.ToUpper() == "INV").Select(c => c.StockOut).Sum().ToString();
                    ////var sss1 = dm_.Select(c => c.StockOut).Sum().ToString();
                    ////var sss2 = dm_.Select(c => c.vRefDocTypeCode.ToUpper() == "INV" ? c.StockOut : 0).Sum().ToString();


                    //var CashTot_D001 = (from c in dm_
                    //                    where c.vPaymentModeCode == "00" || c.vPaymentModeCode == "01" // cash payments
                    //                    select c.nTotalAmount).Sum();

                    //var CardTot_D001 = (from c in dm_
                    //                    where c.vPaymentModeCode == "02" // card payments
                    //                    select c.nTotalAmount).Sum();

                    //var OtherTot_D001 = (from c in dm_
                    //                     where c.vPaymentModeCode != "00" && c.vPaymentModeCode != "01" && c.vPaymentModeCode != "02"    // other payments
                    //                     select c.nTotalAmount).Sum();

                    //s_ += "Collection mode summary:\n(Cash:" + CashTot_D001.ToString("#,###.00").PadLeft(14);
                    //s_ += "##  | Card:" + CardTot_D001.ToString("#,###.00").PadLeft(14);
                    //s_ += "##  | Other:" + OtherTot_D001.ToString("#,###.00").PadLeft(14) + ")\n";



                    break;


                case "PD01":  // Counter foil
                    PrintRptName_ = "Report Name: Postdated Cheque";
                    s_ += BL.CompanyName + "\n";
                    s_ += PrintRptName_ + "\n";
                    s_ += "   Period : " + fromDt_.ToString("dd-MMM-yyyy") + "  to  " + toDt_.ToString("dd-MMM-yyyy") + "\n";
                    s_ += RunOnAndDataR_ + "\n";
                    s_ +=@"ChequeNo".PadRight(10) +  "Date".PadRight(10) + "CheqDate".PadRight(10) 
                        + "PayeeName".PadRight(45) + "ChequeAmt".PadRight(15)+ "\n";


                    dm_ = new List<CheuqeDtl>();
                    dm_ = BL.getPostdatedChequeList(BL.CompanyId, toDt_).ToList();
                    bId_=-1;
                    drId_ = -1;
                    foreach (CheuqeDtl q in dm_.OrderBy(c => c.BankId).ThenBy(c => c.ChequeNo))
                    {
                        if (q.BankId != bId_ || drId_ != q.DrawerId)
                        {
                            s_ += "Bank and D. Name: " + q.BankName.ToString() + " (" + q.DrawerName + ")\n";
                            bId_ = q.BankId;
                            drId_ = q.DrawerId;

                        }
                        s_ += q.ChequeNo.ToString().PadRight(10);
                        s_ +=  q.Date.ToString("dd-MMM-yy").PadRight(10);
                        s_ +=  q.ChequeDate.ToString("dd-MMM-yy").PadRight(10);
                        s_ +=  q.PayeeName.Substring(0, q.PayeeName.Length > 45 ? 45 : q.PayeeName.Length).PadRight(45);
                        s_ +=  q.ChequeAmt.ToString("#,###.00").PadLeft(15) + "\n"; 

                    }

                    break;
            }

            return s_;
        }

        private void btnVewReport_Click(object sender, RoutedEventArgs e)
        {
            rTextBox.IsReadOnly = true;
            rTextBox.Document.Blocks.Clear();
            string _s=GetReport(ReportCode, dtpFromDate.SelectedDate.Value  , dtpToDate.SelectedDate.Value);
            rTextBox.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(_s.Replace("##",""))));

        }


        private void ExtractDataToCSV(string sb)
        {
            try
            {
                lblMessage.Content = "";

                sb = sb.Replace(",", "");
                sb = sb.Replace("##", ",");

                string fileName__ = "";
                fileName__ = BL.CurrenDirectory + "\\" + ReportCode + ".csv";

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName__, false))
                {
                    // Write the stringbuilder text to the the file.
                    sw.WriteLine(sb.ToString());
                }

                lblMessage.Content = fileName__;


                //frmMassageBox msg = new frmMassageBox();
                //msg.tbxFilePath.Text = fileName__;
                //msg.Title = "CSV file saved, path is";
                //msg.ShowDialog();
            }
            catch { }

        }


        private void SavePDFFile(string ps_,bool IsNeedPrint)
        {
            try
            {
                lblMessage.Content = "";
                //----- save in a PDF file
                ps_ = ps_.Replace("##", "");
                string fileName__ = "";
                fileName__ = BL.CurrenDirectory + "\\" + ReportCode + ".pdf";

                //Document doc = new Document(iTextSharp.text.PageSize.A4, 50, 10, 10, 10);
                //iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 50f, 10f, 20f, 20f);

                Document doc = new Document(PageSize.A4, 50f, 10f, 20f, 20f);

                //------ for testing
                /*var table = new PdfPTable(3);
                for (var i = 0; i < 6; i++)
                {
                    var cell = new PdfPCell(new Phrase(i.ToString()));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    //cell.BackgroundColor = new iTextSharp.text.BaseColor(220, 220, 220);
                    cell.Border = 0;
                    //cell.BorderColorLeft = BaseColor.BLACK;
                    cell.BorderWidthLeft = .5f;
                    //cell.BorderColorRight = BaseColor.BLACK;
                    cell.BorderWidthRight = .5f;
                    table.AddCell(cell);
                }
                doc.Add(table);
                 */
                // --- end testing

                //if (File.Exists(fileName__))
                //{
                //    File.Delete(fileName__);
                //PdfWriter i = PdfWriter.GetInstance(doc, new FileStream(fileName__, FileMode.Append));

                //}

                iTextSharp.text.pdf.PdfWriter i1 = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(fileName__, FileMode.Create));
                //i.PageEvent = new BusinessLayer.cs.ITextEvents();
                doc.Open();  // open documnet for the write

                iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph();
                iTextSharp.text.FontFactory.RegisterDirectories();
                iTextSharp.text.Font fontNormal = new iTextSharp.text.Font(iTextSharp.text.FontFactory.GetFont("Lucida Console", 8, iTextSharp.text.Font.NORMAL));

                para.Font = fontNormal;
                para.Leading = 11;
                para.Add(ps_);
                doc.Add(para);


                doc.Close();

                //------------------------
                

                if (IsNeedPrint == true)
                {
                    //PrintDialog printDialog = new PrintDialog();
                    //printDialog.PageRangeSelection = PageRangeSelection.AllPages;
                    //printDialog.UserPageRangeEnabled = true;
                    //bool? doPrint = printDialog.ShowDialog();
                    //if (doPrint != true)
                    //{
                    //    return;
                    //}
                    ProcessStartInfo printProcessInfo = new ProcessStartInfo()
                    {
                        Verb = "print",
                        CreateNoWindow = true,
                        FileName = fileName__,
                        WindowStyle = ProcessWindowStyle.Hidden

                    };

                    Process printProcess = new Process();
                    printProcess.StartInfo = printProcessInfo;
                    printProcess.Start();
                    printProcess.WaitForInputIdle();
                    Thread.Sleep(3000);
                    if (false == printProcess.CloseMainWindow())
                    {
                        printProcess.Kill();
                    }

                }
                else
                {
                    lblMessage.Content = fileName__;
                }



                //linkFileSavePath.Links.Add(0, 4, txtExportLocation.Text + "\\" + txtReportName.Text + ".pdf");

                //linkFileSavePath.Links.Add(0, linkFileSavePath.Text.Length, fileName__);
            }
            catch { }
        }


        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            SavePDFFile((GetReport(ReportCode, dtpFromDate.SelectedDate.Value, dtpToDate.SelectedDate.Value)), true);

        }

        private void btnSavetoCSV_Click(object sender, RoutedEventArgs e)
        {
            ExtractDataToCSV(GetReport(ReportCode, dtpFromDate.SelectedDate.Value, dtpToDate.SelectedDate.Value ));
            lblMessage.Foreground = Brushes.Navy;
        }

        private void btnSave2Pdf_Click(object sender, RoutedEventArgs e)
        {
            SavePDFFile((GetReport(ReportCode, dtpFromDate.SelectedDate.Value, dtpToDate.SelectedDate.Value)),false);
            lblMessage.Foreground = Brushes.Navy;
        }

        private void lblMessage_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lblMessage.Foreground == Brushes.Navy)
            {
                System.Diagnostics.Process.Start(lblMessage.Content.ToString());
                lblMessage.Foreground = Brushes.Black;
            }

        }

        private void btnCopy2Clipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(GetReport(ReportCode, dtpFromDate.SelectedDate.Value, dtpToDate.SelectedDate.Value).Replace("##","\t"));
        }
        //----------- end report






    }
}
