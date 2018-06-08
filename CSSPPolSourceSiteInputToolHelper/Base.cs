using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using CSSPModelsDLL.Models;
using CSSPModelsDLL.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputToolHelper
{
    public partial class PolSourceSiteInputToolHelper
    {
        #region Variables
        private List<string> startWithList = new List<string>() { "101", "143", "910" };
        public string CurrentSubsectorName = "";
        public string CurrentMunicipalityName = "";
        public bool IsSaving = false;
        public List<TVItemModel> tvItemModelProvinceList { get; set; }
        public List<TVItemModel> tvItemModelSubsectorList { get; set; }
        public List<TVItemModel> tvItemModelMunicipalityList { get; set; }
        public int PolSourceSiteTVItemID = 0;
        public int InfrastructureTVItemID = 0;
        public int IssueID = 0;
        public bool IsEditing = false;
        public bool MoreInfo = false;
        public bool IsDirty = false;
        public bool IsReading = false;
        public bool IsAdmin = false;
        public bool IsPolSourceSite = true;
        public bool ShowPolSourceSiteDetails = true;
        public bool ShowOnlyPictures = false;
        public bool ShowOnlyIssues = false;
        public bool ShowOnlyMap = false;
        public Color BackColorNotWellFormed = Color.LightYellow;
        public Color BackColorNotCompleted = Color.LightPink;
        public Color BackColorEditing = Color.LightGreen;
        public Color BackColorNormal = Color.White;
        public Color ForeColorChangedOrNew = Color.Green;
        public Color ForeColorNormal = Color.Black;
        public string baseURLEN = "http://wmon01dtchlebl2/csspwebtools/en-CA/PolSource/";
        public string baseURLFR = "http://wmon01dtchlebl2/csspwebtools/fr-CA/PolSource/";
        //public string baseURLEN = "http://localhost:11561/en-CA/PolSource/";
        //public string baseURLFR = "http://localhost:11561/fr-CA/PolSource/";
        public string BasePathPollutionSourceSites = @"C:\PollutionSourceSites\Subsectors\";
        public string BasePathInfrastructures = @"C:\PollutionSourceSites\Infrastructures\";
        public List<PolSourceObsInfoEnumTextAndID> polSourceObsInfoEnumTextAndIDList = new List<PolSourceObsInfoEnumTextAndID>();
        public List<PolSourceObsInfoEnumHideAndID> polSourceObsInfoEnumHideAndIDList = new List<PolSourceObsInfoEnumHideAndID>();
        public List<PolSourceObsInfoEnumTextAndID> polSourceObsInfoEnumDescTextAndIDList = new List<PolSourceObsInfoEnumTextAndID>();
        public List<PolSourceObsInfoChild> polSourceObsInfoChildList = new List<PolSourceObsInfoChild>();
        public bool CreateMunicipality = false;
        public string AdminEmail = "";
        public bool MunicipalityExist = false;
        public bool AutoCreateMunicipality = false;
        #endregion Variables

        #region Properties
        public SubsectorDoc subsectorDoc { get; set; }
        public MunicipalityDoc municipalityDoc { get; set; }
        public PSS CurrentPSS { get; set; }
        public Infrastructure CurrentInfrastructure { get; set; }
        public BaseEnumService _BaseEnumService { get; set; }
        public BaseModelService _BaseModelService { get; set; }
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
            subsectorDoc = new SubsectorDoc();
            municipalityDoc = new MunicipalityDoc();

            if (Language == LanguageEnum.fr)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
                _BaseModelService = new BaseModelService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
                _BaseModelService = new BaseModelService(LanguageEnum.en);
            }

            foreach (int id in Enum.GetValues(typeof(PolSourceObsInfoEnum)))
            {
                if (id == 0)
                    continue;

                string tempText = _BaseEnumService.GetEnumText_PolSourceObsInfoEnum((PolSourceObsInfoEnum)id);
                if (tempText.IndexOf("|") > 0)
                {
                    tempText = tempText.Substring(0, tempText.IndexOf("|"));
                }
                polSourceObsInfoEnumTextAndIDList.Add(new PolSourceObsInfoEnumTextAndID() { Text = tempText, ID = id });
                polSourceObsInfoEnumHideAndIDList.Add(new PolSourceObsInfoEnumHideAndID() { Hide = _BaseEnumService.GetEnumText_PolSourceObsInfoHideEnum((PolSourceObsInfoEnum)id), ID = id });
                polSourceObsInfoEnumDescTextAndIDList.Add(new PolSourceObsInfoEnumTextAndID() { Text = _BaseEnumService.GetEnumText_PolSourceObsInfoDescEnum((PolSourceObsInfoEnum)id), ID = id });
            }

            _BaseModelService.FillPolSourceObsInfoChild(polSourceObsInfoChildList);
        }
        public void ReDrawInfrastructure()
        {
            IsReading = true;
            if (ShowOnlyPictures)
            {
                ShowPictures();
            }
            else if (ShowOnlyMap)
            {
                ShowMap();
            }
            else
            {
                ShowInfrastructure();
            }
            UpdatePolSourceSitePanelColor();
            IsReading = false;
        }
        public void ReDrawPolSourceSite()
        {
            if (ShowOnlyPictures)
            {
                ShowPictures();
            }
            else if (ShowOnlyIssues)
            {
                DrawIssuesForViewing();
            }
            else if (ShowOnlyMap)
            {
                ShowMap();
            }
            else
            {
                ShowPolSourceSite();
            }
            UpdatePolSourceSitePanelColor();
        }
        public string UserExistInCSSPWebTools(string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}UserExistJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}UserExistJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }
        }
        #endregion Constructors

        #region Functions private
        #endregion Functions private
    }
}
