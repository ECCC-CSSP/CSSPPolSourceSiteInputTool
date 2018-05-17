using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputToolHelper
{
    public partial class PolSourceSiteInputToolHelper
    {
        #region Variables
        public string CurrentSubsectorName = "";
        public bool IsSaving = false;
        public List<string> SubDirectoryList = new List<string>();
        public int PolSourceSiteTVItemID = 0;
        public bool IsEditing = false;
        public bool IsDirty = false;
        public bool IsReading = false;
        //public string baseURLEN = "http://wmon01dtchlebl2/csspwebtools/en-CA/PolSource/";
        //publicstring baseURLFR = "http://wmon01dtchlebl2/csspwebtools/fr-CA/PolSource/";
        public string baseURLEN = "http://localhost:11562/en-CA/PolSource/";
        public string baseURLFR = "http://localhost:11562/fr-CA/PolSource/";
        public string BasePath = @"C:\PollutionSourceSites\";
        #endregion Variables

        #region Properties
        public SubsectorDoc subsectorDoc { get; set; }
        public PSS CurrentPSS { get; set; }
        public Obs CurrentObs { get; set; }
        public Issue CurrentIssue { get; set; }
        public Address CurrentAddress { get; set; }
        public Picture CurrentPicture { get; set; }
        public BaseEnumService _BaseEnumService { get; set; }
        public Panel PanelViewAndEdit { get; set; }
        public Panel PanelPolSourceSite { get; set; }
        public LanguageEnum Language { get; set; }
        #endregion Properties

        #region Constructors
        public PolSourceSiteInputToolHelper(Panel panelViewAndEdit, Panel panelPolSourceSite, LanguageEnum language)
        {
            PanelViewAndEdit = panelViewAndEdit;
            PanelPolSourceSite = panelPolSourceSite;
            Language = language;
        }
        #endregion Constructors
    }
}
