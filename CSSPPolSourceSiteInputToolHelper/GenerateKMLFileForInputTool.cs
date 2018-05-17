using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputToolHelper
{
    public partial class PolSourceSiteInputToolHelper
    {
        public void RegenerateSubsectorKMLFile()
        {
            if (Language == LanguageEnum.fr)
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
                if (Language == LanguageEnum.fr)
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

            FileInfo fi = new FileInfo($@"{BasePath}\{CurrentSubsectorName}\{CurrentSubsectorName}.kml");

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
        }
    }
}
