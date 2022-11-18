using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for frmPrint.xaml
    /// </summary>
    public partial class frmPrint : Window
    {
        public frmPrint()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();

            flowDocument.PageHeight = 64.36;
            flowDocument.PageWidth = 127.27;
            flowDocument.OverridesDefaultStyle = true;

            PageMediaSize pageSize = new PageMediaSize(PageMediaSizeName.Unknown, 127.27,64.36);
            printDialog.PrintTicket.PageMediaSize = pageSize;
            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

            if (printDialog.ShowDialog() == true)
            {

                printDialog.PrintDocument(((IDocumentPaginatorSource)flowDocument).DocumentPaginator, "Flow Document Print Job");

            }
        }
    }
}
