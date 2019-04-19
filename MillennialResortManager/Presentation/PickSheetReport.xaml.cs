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
                MillennialResort_DBDataSet dataset = new MillennialResort_DBDataSet();
                
                dataset.BeginInit();

                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = dataset.sp_select_picksheet_by_picksheetid;
                _reportViewer.LocalReport.DataSources.Add(reportDataSource1);

               
                _reportViewer.LocalReport.ReportEmbeddedResource = "Presentation.PickSheet.rdlc";
                
                //_reportViewer.LocalReport.ReportPath = @"C:\Users\Eric\Desktop\Git_419\Capstone-2019\MillennialResortManager\Presentation\PickSheet.rdlc";        
                dataset.EndInit();

                //fill data into The DataSet          

                MillennialResort_DBDataSetTableAdapters.sp_select_picksheet_by_picksheetidTableAdapter pickSheetDataTableAdaptor = new MillennialResort_DBDataSetTableAdapters.sp_select_picksheet_by_picksheetidTableAdapter();

                pickSheetDataTableAdaptor.ClearBeforeFill = true;
                pickSheetDataTableAdaptor.Fill(dataTable: dataset.sp_select_picksheet_by_picksheetid, PickSheetID: _picksheetID);

                _reportViewer.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }
    }
}
