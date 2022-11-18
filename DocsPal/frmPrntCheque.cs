using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocWriter
{
    public partial class frmPrntCheque : Form
    {
        
        List<BusinessLogic.PageSetting> pageSettings;
        BusinessLogic.CheuqeDtl cheuqeDtl;
        string printerName;
        Font font;
        string printString_;

        public frmPrntCheque(List<BusinessLogic.PageSetting> pageSettings_, BusinessLogic.CheuqeDtl cheuqeDtl_, string printerName_,Font font_)
        {
            InitializeComponent();

            printerName = printerName_;
            font = font_;

            pageSettings = new List<BusinessLogic.PageSetting>();
            pageSettings = pageSettings_;
            cheuqeDtl = new BusinessLogic.CheuqeDtl();
            cheuqeDtl = cheuqeDtl_;

            printDocument1.PrinterSettings.PrinterName = printerName;

            printDocument1.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(100,100, 100, 100);
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Envelope Monarch", 387, 750);
            printDocument1.DefaultPageSettings.PaperSize.PaperName = System.Drawing.Printing.PaperKind.MonarchEnvelope.ToString();
            printDocument1.DefaultPageSettings.Landscape = true;
            printDocument1.Print();

            

        }
        public frmPrntCheque(string _RptName, string printerName_, string _RptString)
        {
            InitializeComponent();

            printerName = printerName_;
            printDocument2.PrinterSettings.PrinterName = printerName;
            printString_ = _RptString;
            printDocument2.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(10, 10, 20, 10);
            printDocument2.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize();
            printDocument2.DefaultPageSettings.PaperSize.PaperName = System.Drawing.Printing.PaperKind.A4.ToString();
            printDocument2.DefaultPageSettings.Landscape = false;
            printDocument2.Print();

        }



        private void frmPrntCheque_Load(object sender, EventArgs e)
        {
            
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            Font ft3 = new Font("Courier New", 10, FontStyle.Regular);
            int y_ = 0;
            foreach(var s in printString_.Split('\n').ToList())
            {
                e.Graphics.DrawString(s, ft3, Brushes.Black, 0, y_);
                y_+=15;
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Font ft = new Font("Georgia", 12, FontStyle.Regular);
            Font ft1 = new Font("Georgia", 9, FontStyle.Regular);
            Font ft2 = new Font("Verdana", 14, FontStyle.Regular);
            Font ft3 = new Font("Courier New", 12, FontStyle.Regular);
            Font ft3B = new Font("Courier New", 12, FontStyle.Bold);
            Font ft4 = new Font("Verdana", 9, FontStyle.Regular);  // FOR acc payee

            //e.Graphics.DrawString("--", ft3, Brushes.Black,100 ,100);



            if (cheuqeDtl.IsAcPay == true)
            {
                e.Graphics.DrawString("_____________", ft4,
        Brushes.Black, (float) Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAcPayX").FirstOrDefault().SettingValue.ToString())
                , (float) Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAcPayY").FirstOrDefault().SettingValue.ToString()) - 13);
                e.Graphics.DrawString("A/C PAYEE ONLY", ft4,
            Brushes.Black, (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAcPayX").FirstOrDefault().SettingValue.ToString())
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAcPayY").FirstOrDefault().SettingValue.ToString()));
                e.Graphics.DrawString("_____________", ft4,
            Brushes.Black, (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAcPayX").FirstOrDefault().SettingValue.ToString())
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAcPayY").FirstOrDefault().SettingValue.ToString()) + 4);
            }

            if (cheuqeDtl.IsPrintDate == true)
            {
                e.Graphics.DrawString(cheuqeDtl.Day1, font,
                    Brushes.Black, (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateX").FirstOrDefault().SettingValue.ToString())
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateY").FirstOrDefault().SettingValue.ToString()));
                e.Graphics.DrawString(cheuqeDtl.Day2, font,
                    Brushes.Black, (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateX").FirstOrDefault().SettingValue.ToString()) + 25
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateY").FirstOrDefault().SettingValue.ToString()));
                e.Graphics.DrawString(cheuqeDtl.Month1, font, Brushes.Black
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateX").FirstOrDefault().SettingValue.ToString()) + 50
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateY").FirstOrDefault().SettingValue.ToString()));
                e.Graphics.DrawString(cheuqeDtl.Month2, font, Brushes.Black
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateX").FirstOrDefault().SettingValue.ToString()) + 75
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateY").FirstOrDefault().SettingValue.ToString()));
                e.Graphics.DrawString(cheuqeDtl.Year1, font, Brushes.Black
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateX").FirstOrDefault().SettingValue.ToString()) + 155
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateY").FirstOrDefault().SettingValue.ToString()));
                e.Graphics.DrawString(cheuqeDtl.Year2, font, Brushes.Black
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateX").FirstOrDefault().SettingValue.ToString()) + 155 + 25
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtDateY").FirstOrDefault().SettingValue.ToString()));

            }


            e.Graphics.DrawString(cheuqeDtl.PayTo1, font, Brushes.Black
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtPaytoX").FirstOrDefault().SettingValue.ToString())
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtPaytoY").FirstOrDefault().SettingValue.ToString()));

            e.Graphics.DrawString(cheuqeDtl.PayTo2, font, Brushes.Black
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtPaytoX").FirstOrDefault().SettingValue.ToString())
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtPaytoY").FirstOrDefault().SettingValue.ToString()) + Convert.ToInt64(pageSettings.Where(x => x.SettingName == "txtPaytoLineHeight").FirstOrDefault().SettingValue.ToString()));

            e.Graphics.DrawString(cheuqeDtl.AmtInWord1, font, Brushes.Black
                ,(float) Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAmtInWrdX").FirstOrDefault().SettingValue.ToString())
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAmtInWrdY").FirstOrDefault().SettingValue.ToString()));
            e.Graphics.DrawString(cheuqeDtl.AmtInWord2, font, Brushes.Black
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAmtInWrdX").FirstOrDefault().SettingValue.ToString())
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAmtInWrdY").FirstOrDefault().SettingValue.ToString()) + Convert.ToInt64(pageSettings.Where(x => x.SettingName == "txtAmtInWrdLineHeight").FirstOrDefault().SettingValue.ToString()));
            e.Graphics.DrawString(cheuqeDtl.AmtInWord3, font, Brushes.Black, Convert.ToInt64(pageSettings.Where(x => x.SettingName == "txtAmtInWrdX").FirstOrDefault().SettingValue.ToString())
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAmtInWrdY").FirstOrDefault().SettingValue.ToString()) + Convert.ToInt64(pageSettings.Where(x => x.SettingName == "txtAmtInWrdLineHeight").FirstOrDefault().SettingValue.ToString()) * 2);
            e.Graphics.DrawString(cheuqeDtl.AmtInWord4, font, Brushes.Black, Convert.ToInt64(pageSettings.Where(x => x.SettingName == "txtAmtInWrdX").FirstOrDefault().SettingValue.ToString())
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAmtInWrdY").FirstOrDefault().SettingValue.ToString()) + Convert.ToInt64(pageSettings.Where(x => x.SettingName == "txtAmtInWrdLineHeight").FirstOrDefault().SettingValue.ToString()) * 3);
            e.Graphics.DrawString(cheuqeDtl.Amt, font
                , Brushes.Black, (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAmX").FirstOrDefault().SettingValue.ToString())
                , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtAmY").FirstOrDefault().SettingValue.ToString()));


            //e.Graphics.DrawString("--", ft3, Brushes.Black, 387, 750);

            // for revlon lanka 
            if (cheuqeDtl.IsPrintComName == true)
            {
                //e.Graphics.DrawString(cheuqeDtl.DrawerName1, ft4,
                //    Brushes.Black, Convert.ToInt64(pageSettings.Where(x => x.SettingName == "txtForComNameX").FirstOrDefault().SettingValue.ToString())
                //    , Convert.ToInt64(pageSettings.Where(x => x.SettingName == "txtForComNameY").FirstOrDefault().SettingValue.ToString()));
                //e.Graphics.DrawString("REVLON LANKA (PVT) LTD", ft4,
                Font f_ = new Font(font.FontFamily, font.Size - 1);
                e.Graphics.DrawString(cheuqeDtl.DrawerName, f_,
                    Brushes.Black, (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtForComNameX").FirstOrDefault().SettingValue.ToString())
                    , (float)Convert.ToDouble(pageSettings.Where(x => x.SettingName == "txtForComNameY").FirstOrDefault().SettingValue.ToString()));



                //-------------
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }



    }
}
