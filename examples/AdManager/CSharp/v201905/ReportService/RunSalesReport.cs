// Copyright 2019 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Api.Ads.Common.Util.Reports;
using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v201905;
using Google.Api.Ads.AdManager.Util.v201905;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201905
{
    /// <summary>
    /// This code example runs a report equal to the "Sales by salespersons
    /// report" in the Ad Manager UI and downloads it to the specified file path.
    /// </summary>
    public class RunSalesReport : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example runs a report equal to the \"Sales by salespersons " +
                    "report\" in the Ad Manager UI and downloads it to the specified file path.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            RunSalesReport codeExample = new RunSalesReport();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ReportService reportService = user.GetService<ReportService>())
            {
                // Set the file path where the report will be saved.
                String filePath = _T("INSERT_FILE_PATH_HERE");

                // Create report job.
                ReportJob reportJob = new ReportJob();
                reportJob.reportQuery = new ReportQuery();
                reportJob.reportQuery.dimensions = new Dimension[]
                {
                    Dimension.SALESPERSON_ID,
                    Dimension.SALESPERSON_NAME
                };
                reportJob.reportQuery.columns = new Column[]
                {
                    Column.AD_SERVER_IMPRESSIONS,
                    Column.AD_SERVER_CPM_AND_CPC_REVENUE,
                    Column.AD_SERVER_WITHOUT_CPD_AVERAGE_ECPM
                };
                reportJob.reportQuery.dateRangeType = DateRangeType.LAST_MONTH;

                try
                {
                    // Run report.
                    reportJob = reportService.runReportJob(reportJob);

                    ReportUtilities reportUtilities =
                        new ReportUtilities(reportService, reportJob.id);

                    // Set download options.
                    ReportDownloadOptions options = new ReportDownloadOptions();
                    options.exportFormat = ExportFormat.CSV_DUMP;
                    options.useGzipCompression = true;
                    reportUtilities.reportDownloadOptions = options;

                    // Download the report.
                    using (ReportResponse reportResponse = reportUtilities.GetResponse())
                    {
                        reportResponse.Save(filePath);
                    }

                    Console.WriteLine("Report saved to \"{0}\".", filePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to run sales report. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
