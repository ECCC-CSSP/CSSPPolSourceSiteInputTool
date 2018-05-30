using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using CSSPModelsDLL.Models;
using CSSPModelsDLL.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        //public string baseURLEN = "http://wmon01dtchlebl2/csspwebtools/en-CA/PolSource/";
        //public string baseURLFR = "http://wmon01dtchlebl2/csspwebtools/fr-CA/PolSource/";
        public string baseURLEN = "http://localhost:11561/en-CA/PolSource/";
        public string baseURLFR = "http://localhost:11561/fr-CA/PolSource/";
        public string BasePathPollutionSourceSites = @"C:\PollutionSourceSites\";
        public string BasePathInfrastructures = @"C:\Infrastructures\";
        public List<PolSourceObsInfoEnumTextAndID> polSourceObsInfoEnumTextAndIDList = new List<PolSourceObsInfoEnumTextAndID>();
        public List<PolSourceObsInfoEnumHideAndID> polSourceObsInfoEnumHideAndIDList = new List<PolSourceObsInfoEnumHideAndID>();
        public List<PolSourceObsInfoEnumTextAndID> polSourceObsInfoEnumDescTextAndIDList = new List<PolSourceObsInfoEnumTextAndID>();
        public List<PolSourceObsInfoChild> polSourceObsInfoChildList = new List<PolSourceObsInfoChild>();
        #endregion Variables

        #region Properties
        public SubsectorDoc subsectorDoc { get; set; }
        public MunicipalityDoc municipalityDoc { get; set; }
        public PSS CurrentPSS { get; set; }
        public PSS CurrentInfrastructure { get; set; }
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
        public void ReDrawMunicipality()
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
        #endregion Constructors

        #region Functions private
        #endregion Functions private
    }
}
