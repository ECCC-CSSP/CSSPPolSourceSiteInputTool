using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSPPolSourceSiteReadSubsectorFile
{
    public class ReadSubsectorFile
    {
        #region Variables
        public string BasePath = @"C:\PollutionSourceSites\";
        public string CurrentSubsectorName = "";
        public bool IsSaving = false;
        #endregion Variables

        #region Properties
        public SubsectorDoc subsectorDoc { get; set; }
        public PSS CurrentPSS { get; set; }
        public Obs CurrentObs { get; set; }
        public Issue CurrentIssue { get; set; }
        public Address CurrentAddress { get; set; }
        public Picture CurrentPicture { get; set; }
        private BaseEnumService _BaseEnumService { get; set; }

        #endregion Properties

        #region Constructors
        public ReadSubsectorFile()
        {
        }
        #endregion Constructors

        #region Events
        public class StatusEventArgs : EventArgs
        {
            public StatusEventArgs(string Status)
            {
                this.Status = Status;
            }
            public string Status { get; set; }
        }
        protected virtual void OnStatus(StatusEventArgs e)
        {
            UpdateStatus?.Invoke(this, e);
        }

        public event EventHandler<StatusEventArgs> UpdateStatus;
        #endregion Events

        #region Functions private
        #endregion Functions private

        #region Functions public
        public bool CheckAllReadDataOK()
        {
            if (subsectorDoc.Version == null)
            {
                OnStatus(new StatusEventArgs("VERSION line could not be found"));
                return false;
            }
            if (subsectorDoc.Version != 1)
            {
                OnStatus(new StatusEventArgs($"VERSION is not equal to 1 it is [{subsectorDoc.Version}]"));
                return false;
            }
            if (subsectorDoc.DocDate == null)
            {
                OnStatus(new StatusEventArgs("DOCDATE line could not be found"));
                return false;
            }
            if (subsectorDoc.DocDate < new DateTime(1980, 1, 1))
            {
                OnStatus(new StatusEventArgs($"DOCDATE does not have the right date it is [{subsectorDoc.DocDate}]"));
                return false;
            }
            if (subsectorDoc.Subsector == null)
            {
                OnStatus(new StatusEventArgs("SUBSECTOR line could not be found"));
                return false;
            }
            if (subsectorDoc.Subsector.SubsectorTVItemID == null)
            {
                OnStatus(new StatusEventArgs($"SubsectorTVItemID could not be set [{subsectorDoc.Subsector.SubsectorTVItemID}]"));
                return false;
            }
            if (subsectorDoc.Subsector.SubsectorTVItemID < 1)
            {
                OnStatus(new StatusEventArgs($"SubsectorTVItemID does not have a valid value it is [{subsectorDoc.Subsector.SubsectorTVItemID}]"));
                return false;
            }
            if (string.IsNullOrWhiteSpace(subsectorDoc.Subsector.SubsectorName))
            {
                OnStatus(new StatusEventArgs($"SubsectorName could not be set it is null or empty"));
                return false;
            }
            foreach (PSS pss in subsectorDoc.Subsector.PSSList)
            {
                if (pss.PSSTVItemID == null)
                {
                    OnStatus(new StatusEventArgs($"PSSTVItemID could not be set it is null or empty"));
                    return false;
                }
                if (pss.PSSTVItemID < 1)
                {
                    OnStatus(new StatusEventArgs($"PSSTVItemID does not have a valid value it is [{pss.PSSTVItemID}]"));
                    return false;
                }
                if (pss.SiteNumber == null)
                {
                    OnStatus(new StatusEventArgs($"SiteNumber could not be set it is null or empty"));
                    return false;
                }
                if (pss.SiteNumber < 0)
                {
                    OnStatus(new StatusEventArgs($"SiteNumber does not have a valid value it is [{pss.SiteNumber}]"));
                    return false;
                }
                if (string.IsNullOrWhiteSpace(pss.SiteNumberText))
                {
                    OnStatus(new StatusEventArgs($"SiteNumberText could not be set it is null or empty"));
                    return false;
                }
                if (pss.Lat == null)
                {
                    OnStatus(new StatusEventArgs($"Lat could not be set it is null or empty"));
                    return false;
                }
                if (!(pss.Lat < 90.0f && pss.Lat >= -90.0f))
                {
                    OnStatus(new StatusEventArgs($"Lat does not have a valid value it is [{pss.Lat}]"));
                    return false;
                }
                // LatNew can be null and it is ok
                if (pss.Lng == null)
                {
                    OnStatus(new StatusEventArgs($"Lng could not be set it is null or empty"));
                    return false;
                }
                if (!(pss.Lng < 180.0f && pss.Lng >= -180.0f))
                {
                    OnStatus(new StatusEventArgs($"Lng does not have a valid value it is [{pss.Lng}]"));
                    return false;
                }
                // LngNew can be null and it is ok
                if (pss.IsActive == null)
                {
                    OnStatus(new StatusEventArgs($"IsActive could not be set it is null or empty"));
                    return false;
                }
                // IsActiveNew can be null and it is ok
                if (pss.IsPointSource == null)
                {
                    OnStatus(new StatusEventArgs($"IsPointSource could not be set it is null or empty"));
                    return false;
                }
                // IsPointSourceNew can be null and it is ok
                if (string.IsNullOrWhiteSpace(pss.TVText))
                {
                    OnStatus(new StatusEventArgs($"TVText could not be set it is null or empty"));
                    return false;
                }
                // TVTextNew can be null and it is ok
                // PSSAddress can be null and it is ok
                // AddressTVItemID can be null and it is ok
                // Municipality can be null
                // AddressType can be null
                // StreetNumber can be null
                // StreetName can be null
                // StreetType can be null
                // PostalCode can be null
                // PSSAddressNew can be null and it is ok

                foreach (Picture picture in pss.PSSPictureList)
                {
                    if (picture.PictureTVItemID == null)
                    {
                        OnStatus(new StatusEventArgs($"PSSPictureList could not be set it is null or empty"));
                        return false;
                    }
                    if (picture.PictureTVItemID < 1)
                    {
                        OnStatus(new StatusEventArgs($"PSSPictureList does not have a valid value it is [{picture.PictureTVItemID}]"));
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(picture.FileName))
                    {
                        OnStatus(new StatusEventArgs($"FileName could not be set it is null or empty"));
                        return false;
                    }
                    // FileNameNew ca be null and it is ok
                    if (string.IsNullOrWhiteSpace(picture.Extension))
                    {
                        OnStatus(new StatusEventArgs($"Extension could not be set it is null or empty"));
                        return false;
                    }
                    // ExtensionNew can be null and it is ok
                    if (string.IsNullOrWhiteSpace(picture.Description))
                    {
                        OnStatus(new StatusEventArgs($"Description could not be set it is null or empty"));
                        return false;
                    }
                    // DescriptionNew can be null and it is ok
                    // ToRemove can be null and it is ok
                    // IsNew can be null and it is ok
                }

                if (pss.PSSObs.ObsID == null)
                {
                    OnStatus(new StatusEventArgs($"ObsID could not be set it is null or empty"));
                    return false;
                }
                if (pss.PSSObs.ObsID < 1)
                {
                    OnStatus(new StatusEventArgs($"ObsID does not have a valid value it is [{pss.PSSObs.ObsID}]"));
                    return false;
                }
                // OldWrittenDescription can be null and it is ok
                if (pss.PSSObs.LastUpdated_UTC == null)
                {
                    OnStatus(new StatusEventArgs($"LastUpdated_UTC could not be set it is null or empty"));
                    return false;
                }
                if (pss.PSSObs.LastUpdated_UTC < new DateTime(1980, 1, 1))
                {
                    OnStatus(new StatusEventArgs($"LastUpdated_UTC does not have a valid value it is [{pss.PSSObs.LastUpdated_UTC}]"));
                    return false;
                }
                // LastUpdate_UTCNew can be null and it is ok
                if (pss.PSSObs.ObsDate == null)
                {
                    OnStatus(new StatusEventArgs($"ObsDate could not be set it is null or empty"));
                    return false;
                }
                if (pss.PSSObs.ObsDate < new DateTime(1980, 1, 1))
                {
                    OnStatus(new StatusEventArgs($"ObsDate does not have a valid value it is [{pss.PSSObs.ObsDate}]"));
                    return false;
                }
                // ObsDateNew can be null and it is ok
                // ToRemove can be null and it is ok
                // IsNew can be null and it is ok

                foreach (Issue issue in pss.PSSObs.IssueList)
                {
                    if (issue.IssueID == null)
                    {
                        OnStatus(new StatusEventArgs($"IssueID could not be set it is null or empty"));
                        return false;
                    }
                    if (issue.IssueID < 0)
                    {
                        OnStatus(new StatusEventArgs($"IssueID does not have a valid value it is [{issue.IssueID}]"));
                        return false;
                    }
                    if (issue.Ordinal == null)
                    {
                        OnStatus(new StatusEventArgs($"Ordinal could not be set it is null or empty"));
                        return false;
                    }
                    if (issue.Ordinal < 0)
                    {
                        OnStatus(new StatusEventArgs($"Ordinal does not have a valid value it is [{issue.Ordinal}]"));
                        return false;
                    }
                    if (issue.LastUpdated_UTC == null)
                    {
                        OnStatus(new StatusEventArgs($"LastUpdated_UTC could not be set it is null or empty"));
                        return false;
                    }
                    if (issue.LastUpdated_UTC < new DateTime(1980, 1, 1))
                    {
                        OnStatus(new StatusEventArgs($"LastUpdated_UTC does not have a valid value it is [{issue.LastUpdated_UTC}]"));
                        return false;
                    }
                    // LastUpdated_UTCNew can be null and it is ok
                    // PolSourceObsInfoIntList can have 0 items
                    // PolSourceObsInfoIntListNew can have 0 items
                    // PolSourceObsInfoEnumList can have 0 items
                    // PolSourceObsInfoEnumListNew can have 0 items
                    // ToRemove can be null and it is ok
                    // Isnew can be null and it is ok
                }
            }
            return true;
        }
        public bool GenerateKMLFileForInputTool(string CurrentSubsectorName, string baseURLEN, string baseURLFR, LanguageEnum language)
        {
            if (language == LanguageEnum.fr)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
            }


            DirectoryInfo di = new DirectoryInfo($@"{BasePath}\{CurrentSubsectorName}\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    OnStatus(new StatusEventArgs(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n"));
                    return false;
                }
            }

            StringBuilder sbKML = new StringBuilder();

            sbKML.AppendLine($@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            sbKML.AppendLine($@"<kml xmlns=""http://www.opengis.net/kml/2.2"" xmlns:gx=""http://www.google.com/kml/ext/2.2"" xmlns:kml=""http://www.opengis.net/kml/2.2"" xmlns:atom=""http://www.w3.org/2005/Atom"">");
            sbKML.AppendLine($@"<Document>");
            sbKML.AppendLine($@"	<name>{CurrentSubsectorName} ({subsectorDoc.Subsector.PSSList.Count})</name>");
            sbKML.AppendLine($@"	<Style id=""s_ylw-pushpin_hl"">");
            sbKML.AppendLine($@"		<IconStyle>");
            sbKML.AppendLine($@"			<scale>1.2</scale>");
            sbKML.AppendLine($@"			<Icon>");
            sbKML.AppendLine($@"				<href>http://maps.google.com/mapfiles/kml/shapes/placemark_square_highlight.png</href>");
            sbKML.AppendLine($@"			</Icon>");
            sbKML.AppendLine($@"		</IconStyle>");
            sbKML.AppendLine($@"		<ListStyle>");
            sbKML.AppendLine($@"		</ListStyle>");
            sbKML.AppendLine($@"	</Style>");
            sbKML.AppendLine($@"	<Style id=""s_ylw-pushpin"">");
            sbKML.AppendLine($@"		<IconStyle>");
            sbKML.AppendLine($@"			<scale>1.2</scale>");
            sbKML.AppendLine($@"			<Icon>");
            sbKML.AppendLine($@"				<href>http://maps.google.com/mapfiles/kml/shapes/placemark_square.png</href>");
            sbKML.AppendLine($@"			</Icon>");
            sbKML.AppendLine($@"		</IconStyle>");
            sbKML.AppendLine($@"		<ListStyle>");
            sbKML.AppendLine($@"		</ListStyle>");
            sbKML.AppendLine($@"	</Style>");
            sbKML.AppendLine($@"	<StyleMap id=""m_ylw-pushpin"">");
            sbKML.AppendLine($@"		<Pair>");
            sbKML.AppendLine($@"			<key>normal</key>");
            sbKML.AppendLine($@"			<styleUrl>#s_ylw-pushpin</styleUrl>");
            sbKML.AppendLine($@"		</Pair>");
            sbKML.AppendLine($@"		<Pair>");
            sbKML.AppendLine($@"			<key>highlight</key>");
            sbKML.AppendLine($@"			<styleUrl>#s_ylw-pushpin_hl</styleUrl>");
            sbKML.AppendLine($@"		</Pair>");
            sbKML.AppendLine($@"	</StyleMap>");

            foreach (PSS pss in subsectorDoc.Subsector.PSSList.OrderBy(c => c.SiteNumberText))
            {
                sbKML.AppendLine($@"		<Placemark>");
                string ActiveText = pss.IsActive == true ? "Active" : "Inactive";
                string PointSourceText = pss.IsPointSource == true ? "Point Source" : "Non Point Source";
                sbKML.AppendLine($@"			<name>Site: {pss.SiteNumber}</name>");
                sbKML.AppendLine($@"            <description><![CDATA[");
                sbKML.AppendLine($@"            {pss.TVText}<br />");
                if (language == LanguageEnum.fr)
                {
                    string url = baseURLFR.Replace(@"/PolSource/", $@"#!View/a|||{pss.PSSTVItemID}|||30010000003000000000000000001100");
                    sbKML.AppendLine($@"                <h1><a href=""{url}"">{pss.TVText}</a> ({pss.PSSTVItemID})</h1>");
                }
                else
                {
                    string url = baseURLEN.Replace(@"/PolSource/", $@"#!View/a|||{pss.PSSTVItemID}|||30010000003000000000000000001100");
                    sbKML.AppendLine($@"                <h1><a href=""{url}"">{pss.TVText}</a> ({pss.PSSTVItemID})</h1>");
                }

                if (pss.PSSAddress != null)
                {
                    sbKML.AppendLine($@"                <br />");
                    sbKML.AppendLine($@"                <p>Address: {pss.PSSAddress.StreetNumber} {pss.PSSAddress.StreetName} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)pss.PSSAddress.StreetType)}, {pss.PSSAddress.Municipality}, {pss.PSSAddress.PostalCode}</p>");
                    sbKML.AppendLine($@"                <br />");
                }

                if (pss.PSSObs.ObsID != null)
                {
                    sbKML.AppendLine($@"                <h3>Last Observation</h3>");
                    sbKML.AppendLine($@"                <blockquote>");
                    sbKML.AppendLine($@"                <p>{ActiveText} --- {PointSourceText}</p>");
                    sbKML.AppendLine($@"                <p><b>Date:</b> {((DateTime)pss.PSSObs.ObsDate).ToString("yyyy MMMM dd")}</p>");
                    sbKML.AppendLine($@"                <p><b>Observation Last Update (UTC):</b> {((DateTime)pss.PSSObs.LastUpdated_UTC).ToString("yyyy MMMM dd HH:mm:ss")}</p>");
                    sbKML.AppendLine($@"                <p><b>Old Written Description:</b> {pss.PSSObs.OldWrittenDescription}</p>");

                    if (pss.PSSObs.IssueList.Count > 0)
                    {
                        sbKML.AppendLine($@"                <blockquote>");
                        sbKML.AppendLine($@"                <ol>");
                        foreach (Issue issue in pss.PSSObs.IssueList)
                        {
                            sbKML.AppendLine($@"                <li>");
                            sbKML.AppendLine($@"                <p><b>Issue Last Update (UTC):</b> {((DateTime)issue.LastUpdated_UTC).ToString("yyyy MMMM dd HH:mm:ss")}</p>");

                            string TVText = "";
                            for (int i = 0, count = issue.PolSourceObsInfoIntList.Count; i < count; i++)
                            {
                                string Temp = _BaseEnumService.GetEnumText_PolSourceObsInfoReportEnum((PolSourceObsInfoEnum)issue.PolSourceObsInfoIntList[i]);
                                switch ((issue.PolSourceObsInfoIntList[i].ToString()).Substring(0, 3))
                                {
                                    case "101":
                                        {
                                            Temp = Temp.Replace("Source", "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Source</strong>");
                                        }
                                        break;
                                    //case "153":
                                    //    {
                                    //        Temp = Temp.Replace("Dilution Analyses", "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Dilution Analyses</strong>");
                                    //    }
                                    //    break;
                                    case "250":
                                        {
                                            Temp = Temp.Replace("Pathway", "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Pathway</strong>");
                                        }
                                        break;
                                    case "900":
                                        {
                                            Temp = Temp.Replace("Status", "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>Status</strong>");
                                        }
                                        break;
                                    case "910":
                                        {
                                            Temp = Temp.Replace("Risk", "<strong>Risk</strong>");
                                        }
                                        break;
                                    case "110":
                                    case "120":
                                    case "122":
                                    case "151":
                                    case "152":
                                    case "153":
                                    case "155":
                                    case "156":
                                    case "157":
                                    case "163":
                                    case "166":
                                    case "167":
                                    case "170":
                                    case "171":
                                    case "172":
                                    case "173":
                                    case "176":
                                    case "178":
                                    case "181":
                                    case "182":
                                    case "183":
                                    case "185":
                                    case "186":
                                    case "187":
                                    case "190":
                                    case "191":
                                    case "192":
                                    case "193":
                                    case "194":
                                    case "196":
                                    case "198":
                                    case "199":
                                    case "220":
                                    case "930":
                                        {
                                            Temp = @"<span>" + Temp + "</span>";
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                TVText = TVText + Temp;
                            }
                            sbKML.AppendLine($@"                <p><b>Selected:</b> {TVText}</p>");
                            sbKML.AppendLine($@"                </li>");
                        }
                        sbKML.AppendLine($@"                </ol>");
                        sbKML.AppendLine($@"                </blockquote>");
                    }
                    sbKML.AppendLine($@"                </blockquote>");


                    if (pss.PSSPictureList.Count > 0)
                    {
                        sbKML.AppendLine($@"                <h3>Images</h3>");
                        sbKML.AppendLine($@"                <ul>");
                        foreach (Picture picture in pss.PSSPictureList)
                        {
                            string url = @"file:///C:\PollutionSourceSites\" + CurrentSubsectorName + @"\Pictures\" + pss.SiteNumberText + "_" + picture.PictureTVItemID + ".jpg";

                            sbKML.AppendLine($@"                <li><img style=""max-width:600px;"" src=""{url}"" /></li>");
                        }
                        sbKML.AppendLine($@"                </ul>");
                    }
                }

                sbKML.AppendLine($@"            ]]>");
                sbKML.AppendLine($@"            </description>");
                sbKML.AppendLine($@"			<styleUrl>#m_ylw-pushpin</styleUrl>");
                sbKML.AppendLine($@"			<Point>");
                sbKML.AppendLine($@"				<coordinates>{pss.Lng},{pss.Lat},0</coordinates>");
                sbKML.AppendLine($@"			</Point>");
                sbKML.AppendLine($@"		</Placemark>");

            }

            sbKML.AppendLine($@"</Document>");
            sbKML.AppendLine($@"</kml>");

            FileInfo fi = new FileInfo($@"{BasePath}\{CurrentSubsectorName}\{CurrentSubsectorName}.kml");

            StreamWriter sw = fi.CreateText();
            sw.Write(sbKML.ToString());
            sw.Close();

            return true;
        }
        public bool ReadPollutionSourceSitesSubsectorFile()
        {
            FileInfo fi = new FileInfo($@"{BasePath}{CurrentSubsectorName}\{CurrentSubsectorName}.txt");

            if (!fi.Exists)
            {
                OnStatus(new StatusEventArgs($"{fi.FullName} does not exist."));
                return false;
            }

            string OldLineObj = "";
            StreamReader sr = fi.OpenText();
            int LineNumb = 0;
            while (!sr.EndOfStream)
            {
                LineNumb += 1;
                string LineTxt = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(LineTxt))
                {
                    OnStatus(new StatusEventArgs("Pollution Source Sites File was not read properly. We found an empty line. The Sampling Plan file should not have empty lines."));
                    return false;
                }
                int pos = LineTxt.IndexOf("\t");
                int pos2 = LineTxt.IndexOf("\t", pos + 1);
                int pos3 = LineTxt.IndexOf("\t", pos2 + 1);
                int pos4 = LineTxt.IndexOf("\t", pos3 + 1);
                int pos5 = LineTxt.IndexOf("\t", pos4 + 1);
                int pos6 = LineTxt.IndexOf("\t", pos5 + 1);
                int pos7 = LineTxt.IndexOf("\t", pos6 + 1);
                int pos8 = LineTxt.IndexOf("\t", pos7 + 1);
                int pos9 = LineTxt.IndexOf("\t", pos8 + 1);
                int pos10 = LineTxt.IndexOf("\t", pos9 + 1);
                switch (LineTxt.Substring(0, pos))
                {
                    case "VERSION":
                        {
                            try
                            {
                                subsectorDoc.Version = int.Parse(LineTxt.Substring("Version\t".Length));
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "DOCDATE":
                        {
                            try
                            {
                                //0123456789012345678
                                //2018|03|02|08|23|12
                                string TempStr = LineTxt.Substring("DOCDATE\t".Length).Trim();
                                int Year = int.Parse(TempStr.Substring(0, 4));
                                int Month = int.Parse(TempStr.Substring(5, 2));
                                int Day = int.Parse(TempStr.Substring(8, 2));
                                int Hour = int.Parse(TempStr.Substring(11, 2));
                                int Minute = int.Parse(TempStr.Substring(14, 2));
                                int Second = int.Parse(TempStr.Substring(17, 2));
                                subsectorDoc.DocDate = new DateTime(Year, Month, Day, Hour, Minute, Second);
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SUBSECTOR":
                        {
                            try
                            {
                                Subsector subsector = new Subsector();
                                subsector.SubsectorTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                subsector.SubsectorName = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                subsectorDoc.Subsector = subsector;
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "-----":
                        {
                            try
                            {
                                string tempNotUsed = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PSS":
                        {
                            try
                            {
                                PSS pss = new PSS();
                                pss.PSSTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                pss.Lat = float.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                pss.Lng = float.Parse(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1));
                                pss.IsActive = bool.Parse(LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1));
                                pss.IsPointSource = bool.Parse(LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1));
                                subsectorDoc.Subsector.PSSList.Add(pss);
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SITENUMB":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.SiteNumber = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                lastPSS.SiteNumberText = "00000".Substring(0, "00000".Length - lastPSS.SiteNumber.ToString().Length) + lastPSS.SiteNumber.ToString();
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TVTEXT":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.TVText = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ADDRESS":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                Address address = new Address();
                                address.AddressTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                address.Municipality = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                address.AddressType = int.Parse(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1));
                                address.StreetNumber = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                address.StreetName = LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1);
                                address.StreetType = int.Parse(LineTxt.Substring(pos6 + 1, pos7 - pos6 - 1));
                                address.PostalCode = LineTxt.Substring(pos7 + 1, pos8 - pos7 - 1);
                                lastPSS.PSSAddress = address;
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTURE":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                Picture picture = new Picture();
                                picture.PictureTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                picture.FileName = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                picture.Extension = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                picture.Description = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                lastPSS.PSSPictureList.Add(picture);
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "OBS":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                Obs obs = new Obs();
                                obs.ObsID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));

                                string TempStr = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                int Year = int.Parse(TempStr.Substring(0, 4));
                                int Month = int.Parse(TempStr.Substring(5, 2));
                                int Day = int.Parse(TempStr.Substring(8, 2));
                                int Hour = int.Parse(TempStr.Substring(11, 2));
                                int Minute = int.Parse(TempStr.Substring(14, 2));
                                int Second = int.Parse(TempStr.Substring(17, 2));
                                obs.LastUpdated_UTC = new DateTime(Year, Month, Day, Hour, Minute, Second);

                                TempStr = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                Year = int.Parse(TempStr.Substring(0, 4));
                                Month = int.Parse(TempStr.Substring(5, 2));
                                Day = int.Parse(TempStr.Substring(8, 2));
                                obs.ObsDate = new DateTime(Year, Month, Day);

                                lastPSS.PSSObs = obs;
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "DESC":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.PSSObs.OldWrittenDescription = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "OLDISSUETEXT":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.OldIssueTextList.Add(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ISSUE":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                Issue issue = new Issue();
                                issue.IssueID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                issue.Ordinal = int.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));

                                string TempStr = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                int Year = int.Parse(TempStr.Substring(0, 4));
                                int Month = int.Parse(TempStr.Substring(5, 2));
                                int Day = int.Parse(TempStr.Substring(8, 2));
                                int Hour = int.Parse(TempStr.Substring(11, 2));
                                int Minute = int.Parse(TempStr.Substring(14, 2));
                                int Second = int.Parse(TempStr.Substring(17, 2));
                                issue.LastUpdated_UTC = new DateTime(Year, Month, Day, Hour, Minute, Second);

                                string PolSourceObsInfoEnumTxt = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1).Trim();
                                issue.PolSourceObsInfoIntList = PolSourceObsInfoEnumTxt.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)).ToList();
                                lastPSS.PSSObs.IssueList.Add(issue);
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    default:
                        {
                            string lineTxt = LineTxt.Substring(0, LineTxt.IndexOf("\t"));
                            OnStatus(new StatusEventArgs($"First item in line { LineNumb } not recognized [{ lineTxt }]"));
                            return false;
                        }
                }

                OldLineObj = LineTxt.Substring(0, pos);
            }
            sr.Close();

            OnStatus(new StatusEventArgs("Pollution Source Sites File OK."));
            return true;
        }
        #endregion Functions public
    }

    #region Sub Classes
    public class SubsectorDoc
    {
        public int? Version { get; set; } = null;
        public DateTime? DocDate { get; set; } = null;
        public Subsector Subsector { get; set; } = null;
    }
    public class Subsector
    {
        public int? SubsectorTVItemID { get; set; } = null;
        public string SubsectorName { get; set; } = null;
        public List<PSS> PSSList { get; set; } = new List<PSS>();
    }
    public class PSS
    {
        public int? PSSTVItemID { get; set; } = null;
        public int? SiteNumber { get; set; } = null;
        public string SiteNumberText { get; set; } = null;
        public float? Lat { get; set; } = null;
        public float? LatNew { get; set; } = null;
        public float? Lng { get; set; } = null;
        public float? LngNew { get; set; } = null;
        public bool? IsActive { get; set; } = null;
        public bool? IsActiveNew { get; set; } = null;
        public bool? IsPointSource { get; set; } = null;
        public bool? IsPointSourceNew { get; set; } = null;
        public string TVText { get; set; } = null;
        public string TVTextNew { get; set; } = null;
        public Address PSSAddress { get; set; } = new Address();
        public Address PSSAddressNew { get; set; } = new Address();
        public Obs PSSObs { get; set; } = new Obs();
        public List<Picture> PSSPictureList { get; set; } = new List<Picture>();
        public List<string> OldIssueTextList { get; set; } = new List<string>();
    }
    public class Address
    {
        public int? AddressTVItemID { get; set; } = null;
        public string Municipality { get; set; } = null;
        public int? AddressType { get; set; } = null;
        public string StreetNumber { get; set; } = null;
        public string StreetName { get; set; } = null;
        public int? StreetType { get; set; } = null;
        public string PostalCode { get; set; } = null;
    }
    public class Picture
    {
        public int? PictureTVItemID { get; set; } = null;
        public string FileName { get; set; } = null;
        public string FileNameNew { get; set; } = null;
        public string Extension { get; set; } = null;
        public string ExtensionNew { get; set; } = null;
        public string Description { get; set; } = null;
        public string DescriptionNew { get; set; } = null;
        public bool? ToRemove { get; set; } = null;
        public bool? IsNew { get; set; } = null;
    }
    public class Obs
    {
        public int? ObsID { get; set; } = null;
        public string OldWrittenDescription { get; set; } = null;
        public DateTime? LastUpdated_UTC { get; set; } = null;
        public DateTime? LastUpdated_UTCNew { get; set; } = null;
        public DateTime? ObsDate { get; set; } = null;
        public DateTime? ObsDateNew { get; set; } = null;
        public List<Issue> IssueList { get; set; } = new List<Issue>();
        public bool? ToRemove { get; set; } = null;
        public bool? IsNew { get; set; } = null;
    }
    public class Issue
    {
        public int? IssueID { get; set; } = null;
        public int? Ordinal { get; set; } = null;
        public DateTime? LastUpdated_UTC { get; set; } = null;
        public DateTime? LastUpdated_UTCNew { get; set; } = null;
        public List<int> PolSourceObsInfoIntList { get; set; } = new List<int>();
        public List<int> PolSourceObsInfoIntListNew { get; set; } = new List<int>();
        public List<PolSourceObsInfoEnum> PolSourceObsInfoEnumList { get; set; } = new List<PolSourceObsInfoEnum>();
        public List<PolSourceObsInfoEnum> PolSourceObsInfoEnumListNew { get; set; } = new List<PolSourceObsInfoEnum>();
        public bool? ToRemove { get; set; } = null;
        public bool? IsNew { get; set; } = null;
    }
    #endregion Sub Classes


}
