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
using LogicLayer;

namespace Presentation
{
    /// <summary>
    /// Eric Bostwick
    /// 4/8/2019
    /// Interaction logic for PickSheetReport.xaml
    /// </summary>
    public partial class PickSheetReport : Window
    {
        private string _picksheetID;  //passed in picksheet id to run the report
        private bool _isReportViewerLoaded;
        public PickSheetReport(string picksheetID)
        {
            InitializeComponent();
            _picksheetID = picksheetID;
            _reportViewer.Load += ReportViewer_Load;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();                
                PickManager _pickManager = new PickManager();

                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file               
                reportDataSource1.Value = _pickManager.Select_PickSheet_By_PickSheetID(_picksheetID);
                _reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                
               
                _reportViewer.LocalReport.ReportEmbeddedResource = "Presentation.PickSheet.rdlc";              
               

                _reportViewer.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }
    }
}
