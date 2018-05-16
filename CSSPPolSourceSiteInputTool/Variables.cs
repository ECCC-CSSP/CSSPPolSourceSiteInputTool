using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using CSSPPolSourceSiteReadSubsectorFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputTool
{
    public partial class CSSPPolSourceSiteInputToolForm : Form
    {
        private List<string> SubDirectoryList = new List<string>();
        private int PolSourceSiteTVItemID = 0;
        private PSS CurrentPSS = null;
        private Issue CurrentIssue = null;
        private bool IsEditing = false;
        private bool IsDirty = false;
        private bool IsReading = false;
        //string baseURLEN = "http://wmon01dtchlebl2/csspwebtools/en-CA/PolSource/";
        //string baseURLFR = "http://wmon01dtchlebl2/csspwebtools/fr-CA/PolSource/";
        string baseURLEN = "http://localhost:11562/en-CA/PolSource/";
        string baseURLFR = "http://localhost:11562/fr-CA/PolSource/";
        public string BasePath = @"C:\PollutionSourceSites\";
    }
}
