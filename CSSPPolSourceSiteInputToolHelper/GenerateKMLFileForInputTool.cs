using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputToolHelper
{
    public partial class PolSourceSiteInputToolHelper
    {
        public void ViewKMLFileInGoogleEarth()
        {
            if (IsPolSourceSite)
            {
                FileInfo fi = new FileInfo($@"{BasePathPollutionSourceSites}\{CurrentSubsectorName}\{CurrentSubsectorName}.kml");

                if (string.IsNullOrWhiteSpace(CurrentSubsectorName))
                {
                    OnStatus(new StatusEventArgs($"Error: CurrentSubsectorName is empty"));
                    return;
                }

                OnStatus(new StatusEventArgs($"Opening file [{fi.FullName}] with Google Earth"));

                FileInfo fiGE = new FileInfo(@"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe");

                if (fiGE.Exists)
                {
                    Process.Start(@"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe", fi.FullName);
                }
                else
                {
                    Process.Start(@"IExplore.exe", fi.FullName);
                }
                OnStatus(new StatusEventArgs($""));
            }
            else
            {
                FileInfo fi = new FileInfo($@"{BasePathInfrastructures}\{CurrentMunicipalityName}\{CurrentMunicipalityName}.kml");

                if (string.IsNullOrWhiteSpace(CurrentMunicipalityName))
                {
                    OnStatus(new StatusEventArgs($"Error: CurrentMunicipalityName is empty"));
                    return;
                }

                OnStatus(new StatusEventArgs($"Opening file [{fi.FullName}] with Google Earth"));

                FileInfo fiGE = new FileInfo(@"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe");

                if (fiGE.Exists)
                {
                    Process.Start(@"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe", fi.FullName);
                }
                else
                {
                    Process.Start(@"IExplore.exe", fi.FullName);
                }
                OnStatus(new StatusEventArgs($""));
            }
        }
        public void RegenerateSubsectorKMLFile()
        {

            OnStatus(new StatusEventArgs($@"Regenerating subsector KML file for subsector [{CurrentSubsectorName}]"));

            if (Language == LanguageEnum.fr)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
            }


            DirectoryInfo di = new DirectoryInfo($@"{BasePathPollutionSourceSites}\{CurrentSubsectorName}\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    OnStatus(new StatusEventArgs(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n"));
                    return;
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
                //if (Language == LanguageEnum.fr)
                //{
                //    string url = baseURLFR.Replace(@"/PolSource/", $@"#!View/a|||{pss.PSSTVItemID}|||30010000003000000000000000001100");
                //    sbKML.AppendLine($@"                <h1><a href=""{url}"">{pss.TVText}</a> ({pss.PSSTVItemID})</h1>");
                //}
                //else
                //{
                //    string url = baseURLEN.Replace(@"/PolSource/", $@"#!View/a|||{pss.PSSTVItemID}|||30010000003000000000000000001100");
                //    sbKML.AppendLine($@"                <h1><a href=""{url}"">{pss.TVText}</a> ({pss.PSSTVItemID})</h1>");
                //}

                if (pss.PSSAddress != null)
                {
                    sbKML.AppendLine($@"                <br />");
                    string StreetNumber = pss.PSSAddress.StreetNumber == null ? "" : pss.PSSAddress.StreetNumber + " ";
                    string StreetName = pss.PSSAddress.StreetName == null ? "" : pss.PSSAddress.StreetName + " ";
                    string StreetType = pss.PSSAddress.StreetType == null ? "" : _BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)pss.PSSAddress.StreetType) + ", ";
                    string Municipality = pss.PSSAddress.Municipality == null ? "" : pss.PSSAddress.Municipality + ", ";
                    string PostalCode = pss.PSSAddress.PostalCode == null ? "" : pss.PSSAddress.PostalCode;

                    string Address = "{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}";
                    if (string.IsNullOrWhiteSpace(Address))
                    {
                        sbKML.AppendLine($@"                <p>Address: empty</p>");
                    }
                    else
                    {
                        sbKML.AppendLine($@"                <p>Address: {StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}</p>");
                    }
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

            FileInfo fi = new FileInfo($@"{BasePathPollutionSourceSites}\{CurrentSubsectorName}\{CurrentSubsectorName}.kml");

            StreamWriter sw = fi.CreateText();
            sw.Write(sbKML.ToString());
            sw.Close();

            if (!fi.Exists)
            {
                OnStatus(new StatusEventArgs($@"An error happened during the regeneration of the [{fi.FullName}] file"));
                return;
            }

            OnStatus(new StatusEventArgs($@"The file [{fi.FullName}] has been regenerated with new changes"));
            OnStatus(new StatusEventArgs($"Done ... file [{fi.FullName}] has been regenerated"));
            OnStatus(new StatusEventArgs($""));
        }
        public void RegenerateMunicipalityKMLFile()
        {

            OnStatus(new StatusEventArgs($@"Regenerating municipality KML file for subsector [{CurrentMunicipalityName}]"));

            if (Language == LanguageEnum.fr)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
            }


            DirectoryInfo di = new DirectoryInfo($@"{BasePathInfrastructures}\{CurrentMunicipalityName}\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    OnStatus(new StatusEventArgs(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n"));
                    return;
                }
            }

            StringBuilder sbKML = new StringBuilder();

            sbKML.AppendLine($@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            sbKML.AppendLine($@"<kml xmlns=""http://www.opengis.net/kml/2.2"" xmlns:gx=""http://www.google.com/kml/ext/2.2"" xmlns:kml=""http://www.opengis.net/kml/2.2"" xmlns:atom=""http://www.w3.org/2005/Atom"">");
            sbKML.AppendLine($@"<Document>");
            sbKML.AppendLine($@"	<name>{CurrentMunicipalityName} ({municipalityDoc.Municipality.InfrastructureList.Count})</name>");
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

            foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList.OrderBy(c => c.InfrastructureName))
            {
                sbKML.AppendLine($@"		<Placemark>");
                string InfrastructureNameText = string.IsNullOrWhiteSpace(infrastructure.InfrastructureNameNew) ? infrastructure.InfrastructureName : infrastructure.InfrastructureNameNew;
                sbKML.AppendLine($@"			<name>Site: {InfrastructureNameText}</name>");
                sbKML.AppendLine($@"            <description><![CDATA[");
                sbKML.AppendLine($@"            {InfrastructureNameText}<br />");

                if (infrastructure.InfrastructureAddress != null)
                {
                    sbKML.AppendLine($@"                <br />");
                    string StreetNumber = infrastructure.InfrastructureAddress.StreetNumber == null ? "" : infrastructure.InfrastructureAddress.StreetNumber + " ";
                    string StreetName = infrastructure.InfrastructureAddress.StreetName == null ? "" : infrastructure.InfrastructureAddress.StreetName + " ";
                    string StreetType = infrastructure.InfrastructureAddress.StreetType == null ? "" : _BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)infrastructure.InfrastructureAddress.StreetType) + ", ";
                    string Municipality = infrastructure.InfrastructureAddress.Municipality == null ? "" : infrastructure.InfrastructureAddress.Municipality + ", ";
                    string PostalCode = infrastructure.InfrastructureAddress.PostalCode == null ? "" : infrastructure.InfrastructureAddress.PostalCode;

                    string Address = "{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}";
                    if (string.IsNullOrWhiteSpace(Address))
                    {
                        sbKML.AppendLine($@"                <p>Address: empty</p>");
                    }
                    else
                    {
                        sbKML.AppendLine($@"                <p>Address: {StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}</p>");
                    }
                    sbKML.AppendLine($@"                <br />");
                }
                if (infrastructure.InfrastructureAddressNew != null)
                {
                    sbKML.AppendLine($@"                <br />");
                    string StreetNumber = infrastructure.InfrastructureAddressNew.StreetNumber == null ? "" : infrastructure.InfrastructureAddressNew.StreetNumber + " ";
                    string StreetName = infrastructure.InfrastructureAddressNew.StreetName == null ? "" : infrastructure.InfrastructureAddressNew.StreetName + " ";
                    string StreetType = infrastructure.InfrastructureAddressNew.StreetType == null ? "" : _BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)infrastructure.InfrastructureAddressNew.StreetType) + ", ";
                    string Municipality = infrastructure.InfrastructureAddressNew.Municipality == null ? "" : infrastructure.InfrastructureAddressNew.Municipality + ", ";
                    string PostalCode = infrastructure.InfrastructureAddressNew.PostalCode == null ? "" : infrastructure.InfrastructureAddressNew.PostalCode;

                    string Address = "{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}";
                    if (string.IsNullOrWhiteSpace(Address))
                    {
                        sbKML.AppendLine($@"                <p>Address: empty</p>");
                    }
                    else
                    {
                        sbKML.AppendLine($@"                <p>Address: {StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}</p>");
                    }
                    sbKML.AppendLine($@"                <br />");
                }


                sbKML.AppendLine($@"                <h3>Details</h3>");
                sbKML.AppendLine($@"                <blockquote>");
                sbKML.AppendLine($@"                <p><b>Comment:</b> {infrastructure.Comment}</p>");
                if (!string.IsNullOrWhiteSpace(infrastructure.CommentNew))
                {
                    sbKML.AppendLine($@"                <p><b>Comment New:</b> {infrastructure.CommentNew}</p>");
                }
                sbKML.AppendLine($@"                <p><b>Lat:</b> {((float)infrastructure.Lat).ToString("F5")} <b>Lng:</b> {((float)infrastructure.Lng).ToString("F5")}</p>");
                if (infrastructure.LatNew != null || infrastructure.LngNew != null)
                {
                    string LatNewText = infrastructure.LatNew != null ? ((float)infrastructure.LatNew).ToString("F5") : "---";
                    string LngNewText = infrastructure.LngNew != null ? ((float)infrastructure.LngNew).ToString("F5") : "---";
                    sbKML.AppendLine($@"                <p><b>Lat New:</b> {LatNewText} <b>Lng New:</b> {LngNewText}</p>");
                }
                sbKML.AppendLine($@"                <p><b>Last Update Date:</b> {((DateTime)infrastructure.LastUpdateDate_UTC).ToString("yyyy MMMM dd")}</p>");
                sbKML.AppendLine($@"                </blockquote>");


                if (infrastructure.InfrastructurePictureList.Count > 0)
                {
                    sbKML.AppendLine($@"                <h3>Images</h3>");
                    sbKML.AppendLine($@"                <ul>");
                    foreach (Picture picture in infrastructure.InfrastructurePictureList)
                    {
                        string url = @"file:///C:\Infrastructures\" + CurrentMunicipalityName + @"\Pictures\" + picture.PictureTVItemID + ".jpg";

                        sbKML.AppendLine($@"                <li><img style=""max-width:600px;"" src=""{url}"" /></li>");
                    }
                    sbKML.AppendLine($@"                </ul>");
                }

                sbKML.AppendLine($@"            ]]>");
                sbKML.AppendLine($@"            </description>");
                sbKML.AppendLine($@"			<styleUrl>#m_ylw-pushpin</styleUrl>");
                sbKML.AppendLine($@"			<Point>");
                sbKML.AppendLine($@"				<coordinates>{infrastructure.Lng},{infrastructure.Lat},0</coordinates>");
                sbKML.AppendLine($@"			</Point>");
                sbKML.AppendLine($@"		</Placemark>");

            }

            sbKML.AppendLine($@"</Document>");
            sbKML.AppendLine($@"</kml>");

            FileInfo fi = new FileInfo($@"{BasePathInfrastructures}\{CurrentMunicipalityName}\{CurrentMunicipalityName}.kml");

            StreamWriter sw = fi.CreateText();
            sw.Write(sbKML.ToString());
            sw.Close();

            if (!fi.Exists)
            {
                OnStatus(new StatusEventArgs($@"An error happened during the regeneration of the [{fi.FullName}] file"));
                return;
            }

            OnStatus(new StatusEventArgs($@"The file [{fi.FullName}] has been regenerated with new changes"));
            OnStatus(new StatusEventArgs($"Done ... file [{fi.FullName}] has been regenerated"));
            OnStatus(new StatusEventArgs($""));
        }
    }
}
