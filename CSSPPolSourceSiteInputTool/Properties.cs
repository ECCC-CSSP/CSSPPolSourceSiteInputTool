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
        private CultureInfo currentCulture { get; set; }
        private CultureInfo currentUICulture { get; set; }
        private List<Label> PSSLabelList { get; set; }
        private ReadSubsectorFile readSubsectorFile { get; set; }
        private BaseEnumService _BaseEnumService { get; set; }
    }
}
