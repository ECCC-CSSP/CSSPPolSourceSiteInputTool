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
        public int ContactTVItemID = 0;
        public int IssueID = 0;
        public bool IsEditing = false;
        public bool MoreInfo = false;
        public bool WrittenDescription = true;
        public bool OldIssueText = true;
        public bool OldIssue = true;
        public bool NewIssue = true;
        public bool DeletedIssueAndPicture = true;
        public bool IsDirty = false;
        public bool IsReading = false;
        public bool IsAdmin = false;
        public bool IsPolSourceSite = true;
        public bool IsContact = false;
        public bool ShowPolSourceSiteDetails = true;
        public bool ShowOnlyPictures = false;
        public bool ShowOnlyIssues = false;
        public bool ShowOnlyMap = false;
        public Color BackColorNotWellFormed = Color.LightYellow;
        public Color BackColorNotCompleted = Color.LightPink;
        public Color BackColorEditing = Color.LightGreen;
        public Color BackColorDefault = Color.LightGray;
        public Color BackColorNormal = Color.White;
        public Color ForeColorChangedOrNew = Color.Green;
        public Color ForeColorNormal = Color.Black;
        public string baseURLFR = "http://wmon01dtchlebl2/csspwebtools/fr-CA/PolSource/";
        public string baseURLEN = "http://wmon01dtchlebl2/csspwebtools/en-CA/PolSource/";
        //public string baseURLEN = "http://localhost:11562/en-CA/PolSource/";
        //public string baseURLFR = "http://localhost:11562/fr-CA/PolSource/";
        public string BasePathPollutionSourceSites = @"C:\PollutionSourceSites\Subsectors\";
        public string BasePathInfrastructures = @"C:\PollutionSourceSites\Infrastructures\";
        public List<PolSourceObsInfoEnumTextAndID> polSourceObsInfoEnumTextAndIDList = new List<PolSourceObsInfoEnumTextAndID>();
        public List<PolSourceObsInfoEnumHideAndID> polSourceObsInfoEnumHideAndIDList = new List<PolSourceObsInfoEnumHideAndID>();
        public List<PolSourceObsInfoEnumTextAndID> polSourceObsInfoEnumDescTextAndIDList = new List<PolSourceObsInfoEnumTextAndID>();
        public List<PolSourceObsInfoChild> polSourceObsInfoChildList = new List<PolSourceObsInfoChild>();
        //public bool CreateMunicipality = false;
        public string AdminEmail = "";
        //public bool MunicipalityExist = false;
        //public bool AutoCreateMunicipality = false;
        public string InitialDirectorySubsectorPictures = $@"C:\";
        public string InitialDirectoryInfrastructurePictures = $@"C:\";
        public bool IsTryingToMoveUnderItself = false;
        public bool OnDetailPage = false;
        public bool OnIssuePage = false;
        public bool OnPicturePage = false;
        public bool OnMapPage = false;
        public string KMLFileName = "";
        public DateTime? KMLFileLastWriteTime = null;
        #endregion Variables

        #region Properties
        public SubsectorDoc subsectorDoc { get; set; }
        public MunicipalityDoc municipalityDoc { get; set; }
        public PSS CurrentPSS { get; set; }
        public Contact CurrentContact { get; set; }
        public Infrastructure CurrentInfrastructure { get; set; }
        public BaseEnumService _BaseEnumService { get; set; }
        public BaseModelService _BaseModelService { get; set; }
        public Panel PanelViewAndEdit { get; set; }
        public Panel PanelMunicipalities { get; set; }
        public Panel PanelPolSourceSite { get; set; }
        public Panel PanelStreetType { get; set; }
        public Panel PanelShowInputOptions { get; set; }
        public Panel PanelSubsectorOrMunicipality { get; set; }
        public LanguageEnum Language { get; set; }

        #endregion Properties

        #region Constructors
        public PolSourceSiteInputToolHelper(Panel panelViewAndEdit, Panel panelPolSourceSite, Panel panelMunicipalities, Panel panelStreetType, Panel panelShowInputOptions, Panel panelSubsectorOrMunicipality, LanguageEnum language)
        {
            PanelViewAndEdit = panelViewAndEdit;
            PanelPolSourceSite = panelPolSourceSite;
            PanelMunicipalities = panelMunicipalities;
            PanelStreetType = panelStreetType;
            PanelShowInputOptions = panelShowInputOptions;
            PanelSubsectorOrMunicipality = panelSubsectorOrMunicipality;

            BackColorDefault = panelViewAndEdit.BackColor;

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
        #endregion Constructors

        #region Functions private
        private List<MunicipalityIDNumber> GetMunicipalitiesAndIDNumber()
        {
            List<MunicipalityIDNumber> MunicipalityIDNumberList = new List<MunicipalityIDNumber>();
            MunicipalityIDNumberList.Add(new MunicipalityIDNumber() { Municipality = "None", IDNumber = 0.ToString() });

            if (IsPolSourceSite)
            {
                foreach (PSS pss in subsectorDoc.Subsector.PSSList)
                {
                    if (pss.PSSAddressNew != null)
                    {
                        if (!string.IsNullOrWhiteSpace(pss.PSSAddressNew.Municipality))
                        {
                            if (!MunicipalityIDNumberList.Where(c => c.Municipality.Trim() == pss.PSSAddressNew.Municipality.Trim()).Any())
                            {
                                MunicipalityIDNumberList.Add(new MunicipalityIDNumber { Municipality = pss.PSSAddressNew.Municipality, IDNumber = pss.SiteNumberText });
                            }
                        }
                    }
                    if (pss.PSSAddress != null)
                    {
                        if (!string.IsNullOrWhiteSpace(pss.PSSAddress.Municipality))
                        {
                            if (!MunicipalityIDNumberList.Where(c => c.Municipality.Trim() == pss.PSSAddress.Municipality.Trim()).Any())
                            {
                                MunicipalityIDNumberList.Add(new MunicipalityIDNumber { Municipality = pss.PSSAddress.Municipality, IDNumber = pss.SiteNumberText });
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList)
                {
                    if (infrastructure.InfrastructureAddressNew != null)
                    {
                        if (!string.IsNullOrWhiteSpace(infrastructure.InfrastructureAddressNew.Municipality))
                        {
                            if (!MunicipalityIDNumberList.Where(c => c.Municipality.Trim() == infrastructure.InfrastructureAddressNew.Municipality.Trim()).Any())
                            {
                                MunicipalityIDNumberList.Add(new MunicipalityIDNumber { Municipality = infrastructure.InfrastructureAddressNew.Municipality, IDNumber = infrastructure.InfrastructureTVItemID.ToString() });
                            }
                        }
                    }
                    if (infrastructure.InfrastructureAddress != null)
                    {
                        if (!string.IsNullOrWhiteSpace(infrastructure.InfrastructureAddress.Municipality))
                        {
                            if (!MunicipalityIDNumberList.Where(c => c.Municipality.Trim() == infrastructure.InfrastructureAddress.Municipality.Trim()).Any())
                            {
                                MunicipalityIDNumberList.Add(new MunicipalityIDNumber { Municipality = infrastructure.InfrastructureAddress.Municipality, IDNumber = infrastructure.InfrastructureTVItemID.ToString() });
                            }
                        }
                    }
                }
            }

            return MunicipalityIDNumberList;
        }
        private List<StreetTypeIDNumber> GetStreetTypeAndIDNumber()
        {
            List<StreetTypeIDNumber> StreetTypeIDNumberList = new List<StreetTypeIDNumber>();

            if (IsPolSourceSite)
            {
                StreetTypeIDNumberList.Add(new StreetTypeIDNumber() { StreetType = "None", IDNumber = 0.ToString() });

                for (int i = 0, count = Enum.GetNames(typeof(StreetTypeEnum)).Length; i < count; i++)
                {
                    if (i != 0)
                    {
                        StreetTypeIDNumberList.Add(new StreetTypeIDNumber() { StreetType = ((StreetTypeEnum)i).ToString(), IDNumber = i.ToString() });
                    }
                }
            }
            else
            {
                StreetTypeIDNumberList.Add(new StreetTypeIDNumber() { StreetType = "None", IDNumber = 0.ToString() });

                for (int i = 0, count = Enum.GetNames(typeof(StreetTypeEnum)).Length; i < count; i++)
                {
                    if (i != 0)
                    {
                        StreetTypeIDNumberList.Add(new StreetTypeIDNumber() { StreetType = ((StreetTypeEnum)i).ToString(), IDNumber = i.ToString() });
                    }
                }
            }

            return StreetTypeIDNumberList;
        }
        private List<ObsDateIDNumber> GetObsDateAndIDNumber()
        {
            List<ObsDateIDNumber> ObsDateIDNumberList = new List<ObsDateIDNumber>();

            foreach (PSS pss in subsectorDoc.Subsector.PSSList)
            {
                if (pss.PSSObs.ObsDateNew == null)
                {
                    ObsDateIDNumberList.Add(new ObsDateIDNumber { ObsDate = (DateTime)pss.PSSObs.ObsDate, IDNumber = pss.SiteNumberText });
                }
            }

            return ObsDateIDNumberList;
        }
        public void ReDrawContactAndInfrastructure()
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
                ShowContactOrInfrastructure();
            }
            UpdatePolSourceSitePanelColor();
            IsReading = false;

            PanelViewAndEdit.Focus();
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
        #endregion Functions private

        public class MunicipalityIDNumber
        {
            public MunicipalityIDNumber()
            {

            }

            public string Municipality { get; set; }
            public string IDNumber { get; set; }
        }
        public class StreetTypeIDNumber
        {
            public StreetTypeIDNumber()
            {

            }

            public string StreetType { get; set; }
            public string IDNumber { get; set; }
        }
        private class ObsDateIDNumber
        {
            public ObsDateIDNumber()
            {

            }

            public DateTime ObsDate { get; set; }
            public string IDNumber { get; set; }
        }

    }
}
