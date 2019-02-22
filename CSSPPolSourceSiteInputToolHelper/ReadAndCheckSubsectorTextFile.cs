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
        public object MunicipalityAndIDText { get; private set; }

        public bool CheckAllReadDataMunicipalityOK()
        {
            if (municipalityDoc.Version == null)
            {
                EmitStatus(new StatusEventArgs("VERSION line could not be found"));
                return false;
            }
            if (municipalityDoc.Version != 1)
            {
                EmitStatus(new StatusEventArgs($"VERSION is not equal to 1 it is [{municipalityDoc.Version}]"));
                return false;
            }
            if (municipalityDoc.DocDate == null)
            {
                EmitStatus(new StatusEventArgs("DOCDATE line could not be found"));
                return false;
            }
            if (municipalityDoc.DocDate < new DateTime(1980, 1, 1))
            {
                EmitStatus(new StatusEventArgs($"DOCDATE does not have the right date it is [{municipalityDoc.DocDate}]"));
                return false;
            }
            if (municipalityDoc.Municipality == null)
            {
                EmitStatus(new StatusEventArgs("SUBSECTOR line could not be found"));
                return false;
            }
            if (municipalityDoc.Municipality.MunicipalityTVItemID == null)
            {
                EmitStatus(new StatusEventArgs($"MunicipalityTVItemID could not be set [{municipalityDoc.Municipality.MunicipalityTVItemID}]"));
                return false;
            }
            if (municipalityDoc.Municipality.MunicipalityTVItemID < 1)
            {
                EmitStatus(new StatusEventArgs($"MunicipalityTVItemID does not have a valid value it is [{municipalityDoc.Municipality.MunicipalityTVItemID}]"));
                return false;
            }
            if (string.IsNullOrWhiteSpace(municipalityDoc.Municipality.MunicipalityName))
            {
                EmitStatus(new StatusEventArgs($"MunicipalityName is empty"));
                return false;
            }

            foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList)
            {
                if (infrastructure.InfrastructureTVItemID == null)
                {
                    EmitStatus(new StatusEventArgs($"InfrastructureTVItemID could not be set [{infrastructure.InfrastructureTVItemID}]"));
                    return false;
                }
                if (infrastructure.InfrastructureTVItemID < 1)
                {
                    EmitStatus(new StatusEventArgs($"InfrastructureTVItemID does not have a valid value it is [{infrastructure.InfrastructureTVItemID}]"));
                    return false;
                }
            }

            return true;
        }
        public bool CheckAllReadDataPollutionSourceSiteOK()
        {
            if (subsectorDoc.Version == null)
            {
                EmitStatus(new StatusEventArgs("VERSION line could not be found"));
                return false;
            }
            if (subsectorDoc.Version != 1)
            {
                EmitStatus(new StatusEventArgs($"VERSION is not equal to 1 it is [{subsectorDoc.Version}]"));
                return false;
            }
            if (subsectorDoc.DocDate == null)
            {
                EmitStatus(new StatusEventArgs("DOCDATE line could not be found"));
                return false;
            }
            if (subsectorDoc.DocDate < new DateTime(1980, 1, 1))
            {
                EmitStatus(new StatusEventArgs($"DOCDATE does not have the right date it is [{subsectorDoc.DocDate}]"));
                return false;
            }
            if (subsectorDoc.Subsector == null)
            {
                EmitStatus(new StatusEventArgs("SUBSECTOR line could not be found"));
                return false;
            }
            if (subsectorDoc.Subsector.SubsectorTVItemID == null)
            {
                EmitStatus(new StatusEventArgs($"SubsectorTVItemID could not be set [{subsectorDoc.Subsector.SubsectorTVItemID}]"));
                return false;
            }
            if (subsectorDoc.Subsector.SubsectorTVItemID < 1)
            {
                EmitStatus(new StatusEventArgs($"SubsectorTVItemID does not have a valid value it is [{subsectorDoc.Subsector.SubsectorTVItemID}]"));
                return false;
            }
            if (string.IsNullOrWhiteSpace(subsectorDoc.Subsector.SubsectorName))
            {
                EmitStatus(new StatusEventArgs($"SubsectorName could not be set it is null or empty"));
                return false;
            }
            foreach (PSS pss in subsectorDoc.Subsector.PSSList)
            {
                if (pss.PSSTVItemID == null)
                {
                    EmitStatus(new StatusEventArgs($"PSSTVItemID could not be set it is null or empty"));
                    return false;
                }
                if (pss.PSSTVItemID < 1)
                {
                    EmitStatus(new StatusEventArgs($"PSSTVItemID does not have a valid value it is [{pss.PSSTVItemID}]"));
                    return false;
                }
                if (pss.SiteNumber == null)
                {
                    EmitStatus(new StatusEventArgs($"SiteNumber could not be set it is null or empty"));
                    return false;
                }
                if (pss.SiteNumber < 0)
                {
                    EmitStatus(new StatusEventArgs($"SiteNumber does not have a valid value it is [{pss.SiteNumber}]"));
                    return false;
                }
                if (string.IsNullOrWhiteSpace(pss.SiteNumberText))
                {
                    EmitStatus(new StatusEventArgs($"SiteNumberText could not be set it is null or empty"));
                    return false;
                }
                if (pss.Lat == null)
                {
                    EmitStatus(new StatusEventArgs($"Lat could not be set it is null or empty"));
                    return false;
                }
                if (!(pss.Lat < 90.0f && pss.Lat >= -90.0f))
                {
                    EmitStatus(new StatusEventArgs($"Lat does not have a valid value it is [{pss.Lat}]"));
                    return false;
                }
                // LatNew can be null and it is ok
                if (pss.Lng == null)
                {
                    EmitStatus(new StatusEventArgs($"Lng could not be set it is null or empty"));
                    return false;
                }
                if (!(pss.Lng < 180.0f && pss.Lng >= -180.0f))
                {
                    EmitStatus(new StatusEventArgs($"Lng does not have a valid value it is [{pss.Lng}]"));
                    return false;
                }
                // LngNew can be null and it is ok
                if (pss.IsActive == null)
                {
                    EmitStatus(new StatusEventArgs($"IsActive could not be set it is null or empty"));
                    return false;
                }
                // IsActiveNew can be null and it is ok
                if (pss.IsPointSource == null)
                {
                    EmitStatus(new StatusEventArgs($"IsPointSource could not be set it is null or empty"));
                    return false;
                }
                // IsPointSourceNew can be null and it is ok
                if (string.IsNullOrWhiteSpace(pss.TVText))
                {
                    EmitStatus(new StatusEventArgs($"TVText could not be set it is null or empty"));
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
                        EmitStatus(new StatusEventArgs($"PSSPictureList could not be set it is null or empty"));
                        return false;
                    }
                    if (picture.PictureTVItemID < 1)
                    {
                        EmitStatus(new StatusEventArgs($"PSSPictureList does not have a valid value it is [{picture.PictureTVItemID}]"));
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(picture.FileName))
                    {
                        EmitStatus(new StatusEventArgs($"FileName could not be set it is null or empty"));
                        return false;
                    }
                    // FileNameNew ca be null and it is ok
                    if (string.IsNullOrWhiteSpace(picture.Extension))
                    {
                        EmitStatus(new StatusEventArgs($"Extension could not be set it is null or empty"));
                        return false;
                    }
                    // ExtensionNew can be null and it is ok
                    if (string.IsNullOrWhiteSpace(picture.Description))
                    {
                        EmitStatus(new StatusEventArgs($"Description could not be set it is null or empty"));
                        return false;
                    }
                    // DescriptionNew can be null and it is ok
                    // ToRemove can be null and it is ok
                    // IsNew can be null and it is ok
                }

                if (pss.PSSObs.ObsID == null)
                {
                    EmitStatus(new StatusEventArgs($"ObsID could not be set it is null or empty"));
                    return false;
                }
                if (pss.PSSObs.ObsID < 1)
                {
                    EmitStatus(new StatusEventArgs($"ObsID does not have a valid value it is [{pss.PSSObs.ObsID}]"));
                    return false;
                }
                // OldWrittenDescription can be null and it is ok
                if (pss.PSSObs.LastUpdated_UTC == null)
                {
                    EmitStatus(new StatusEventArgs($"LastUpdated_UTC could not be set it is null or empty"));
                    return false;
                }
                if (pss.PSSObs.LastUpdated_UTC < new DateTime(1980, 1, 1))
                {
                    EmitStatus(new StatusEventArgs($"LastUpdated_UTC does not have a valid value it is [{pss.PSSObs.LastUpdated_UTC}]"));
                    return false;
                }
                // LastUpdate_UTCNew can be null and it is ok
                if (pss.PSSObs.ObsDate == null)
                {
                    EmitStatus(new StatusEventArgs($"ObsDate could not be set it is null or empty"));
                    return false;
                }
                if (pss.PSSObs.ObsDate < new DateTime(1980, 1, 1))
                {
                    EmitStatus(new StatusEventArgs($"ObsDate does not have a valid value it is [{pss.PSSObs.ObsDate}]"));
                    return false;
                }
                // ObsDateNew can be null and it is ok
                // ToRemove can be null and it is ok
                // IsNew can be null and it is ok

                foreach (Issue issue in pss.PSSObs.IssueList)
                {
                    if (issue.IssueID == null)
                    {
                        EmitStatus(new StatusEventArgs($"IssueID could not be set it is null or empty"));
                        return false;
                    }
                    if (issue.IssueID < 0)
                    {
                        EmitStatus(new StatusEventArgs($"IssueID does not have a valid value it is [{issue.IssueID}]"));
                        return false;
                    }
                    if (issue.Ordinal == null)
                    {
                        EmitStatus(new StatusEventArgs($"Ordinal could not be set it is null or empty"));
                        return false;
                    }
                    if (issue.Ordinal < 0)
                    {
                        EmitStatus(new StatusEventArgs($"Ordinal does not have a valid value it is [{issue.Ordinal}]"));
                        return false;
                    }
                    if (issue.LastUpdated_UTC == null)
                    {
                        EmitStatus(new StatusEventArgs($"LastUpdated_UTC could not be set it is null or empty"));
                        return false;
                    }
                    if (issue.LastUpdated_UTC < new DateTime(1980, 1, 1))
                    {
                        EmitStatus(new StatusEventArgs($"LastUpdated_UTC does not have a valid value it is [{issue.LastUpdated_UTC}]"));
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
        public bool ReadInfrastructuresMunicipalityFile()
        {
            FileInfo fi = new FileInfo($@"{BasePathInfrastructures}{CurrentMunicipalityName}\{CurrentMunicipalityName}.txt");

            if (!fi.Exists)
            {
                EmitStatus(new StatusEventArgs($"{fi.FullName} does not exist."));
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
                    EmitStatus(new StatusEventArgs("Municipality File was not read properly. We found an empty line. The Municipality file should not have empty lines."));
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
                                municipalityDoc.Version = int.Parse(LineTxt.Substring("Version\t".Length));
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                municipalityDoc.DocDate = new DateTime(Year, Month, Day, Hour, Minute, Second);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "MUNICIPALITY":
                        {
                            try
                            {
                                Municipality municipality = new Municipality();
                                municipality.MunicipalityTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                municipality.MunicipalityName = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                if (string.IsNullOrWhiteSpace(municipality.MunicipalityName))
                                {
                                    municipality.MunicipalityName = municipality.MunicipalityName.Trim();
                                }
                                municipalityDoc.Municipality = municipality;
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "INFRASTRUCTURE":
                        {
                            try
                            {
                                Infrastructure infrastructure = new Infrastructure();
                                infrastructure.InfrastructureTVItemID = (int)(double.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                //0123456789012345678
                                //2018|03|02|08|23|12
                                string TempStr = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1).Trim();
                                int Year = int.Parse(TempStr.Substring(0, 4));
                                int Month = int.Parse(TempStr.Substring(5, 2));
                                int Day = int.Parse(TempStr.Substring(8, 2));
                                int Hour = int.Parse(TempStr.Substring(11, 2));
                                int Minute = int.Parse(TempStr.Substring(14, 2));
                                int Second = int.Parse(TempStr.Substring(17, 2));
                                infrastructure.LastUpdateDate_UTC = new DateTime(Year, Month, Day, Hour, Minute, Second);

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1)))
                                {
                                    infrastructure.Lat = null;
                                }
                                else
                                {
                                    infrastructure.Lat = float.Parse(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1)))
                                {
                                    infrastructure.Lng = null;
                                }
                                else
                                {
                                    infrastructure.Lng = float.Parse(LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1)))
                                {
                                    infrastructure.LatOutfall = null;
                                }
                                else
                                {
                                    infrastructure.LatOutfall = float.Parse(LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos6 + 1, pos7 - pos6 - 1)))
                                {
                                    infrastructure.LngOutfall = null;
                                }
                                else
                                {
                                    infrastructure.LngOutfall = float.Parse(LineTxt.Substring(pos6 + 1, pos7 - pos6 - 1));
                                }
                                infrastructure.IsActive = bool.Parse(LineTxt.Substring(pos7 + 1, pos8 - pos7 - 1));
                                municipalityDoc.Municipality.InfrastructureList.Add(infrastructure);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LATNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.LatNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.LatNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LNGNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.LngNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.LngNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LATOUTFALLNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.LatOutfallNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.LatOutfallNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LNGOUTFALLNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.LngOutfallNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.LngOutfallNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ISACTIVENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.IsActiveNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.IsActiveNew = bool.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TVTEXT":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.TVText = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TVTEXTNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.TVTextNew = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "COMMENT":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.Comment = LineTxt.Substring(pos + 1, pos2 - pos - 1).Replace("|||", "\r\n");
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "COMMENTNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.CommentNew = LineTxt.Substring(pos + 1, pos2 - pos - 1).Replace("|||", "\r\n");
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PRISMID":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PrismID = null;
                                }
                                else
                                {
                                    lastInfrastructure.PrismID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PRISMIDNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PrismIDNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.PrismIDNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TPID":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.TPID = null;
                                }
                                else
                                {
                                    lastInfrastructure.TPID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TPIDNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.TPIDNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.TPIDNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LSID":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.LSID = null;
                                }
                                else
                                {
                                    lastInfrastructure.LSID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LSIDNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.LSIDNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.LSIDNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SITEID":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.SiteID = null;
                                }
                                else
                                {
                                    lastInfrastructure.SiteID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SITEIDNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.SiteIDNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.SiteIDNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SITE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.Site = null;
                                }
                                else
                                {
                                    lastInfrastructure.Site = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SITENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.SiteNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.SiteNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "INFRASTRUCTURECATEGORY":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.InfrastructureCategory = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "INFRASTRUCTURECATEGORYNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.InfrastructureCategoryNew = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "INFRASTRUCTURETYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.InfrastructureType = null;
                                }
                                else
                                {
                                    lastInfrastructure.InfrastructureType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "INFRASTRUCTURETYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.InfrastructureTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.InfrastructureTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "FACILITYTYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.FacilityType = null;
                                }
                                else
                                {
                                    lastInfrastructure.FacilityType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "FACILITYTYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.FacilityTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.FacilityTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ISMECHANICALLYAERATED":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.IsMechanicallyAerated = null;
                                }
                                else
                                {
                                    lastInfrastructure.IsMechanicallyAerated = bool.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ISMECHANICALLYAERATEDNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.IsMechanicallyAeratedNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.IsMechanicallyAeratedNew = bool.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "NUMBEROFCELLS":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.NumberOfCells = null;
                                }
                                else
                                {
                                    lastInfrastructure.NumberOfCells = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "NUMBEROFCELLSNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.NumberOfCellsNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.NumberOfCellsNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "NUMBEROFAERATEDCELLS":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.NumberOfAeratedCells = null;
                                }
                                else
                                {
                                    lastInfrastructure.NumberOfAeratedCells = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "NUMBEROFAERATEDCELLSNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.NumberOfAeratedCellsNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.NumberOfAeratedCellsNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "AERATIONTYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.AerationType = null;
                                }
                                else
                                {
                                    lastInfrastructure.AerationType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "AERATIONTYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.AerationTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.AerationTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PRELIMINARYTREATMENTTYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PreliminaryTreatmentType = null;
                                }
                                else
                                {
                                    lastInfrastructure.PreliminaryTreatmentType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PRELIMINARYTREATMENTTYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PreliminaryTreatmentTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.PreliminaryTreatmentTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PRIMARYTREATMENTTYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PrimaryTreatmentType = null;
                                }
                                else
                                {
                                    lastInfrastructure.PrimaryTreatmentType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PRIMARYTREATMENTTYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PrimaryTreatmentTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.PrimaryTreatmentTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SECONDARYTREATMENTTYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.SecondaryTreatmentType = null;
                                }
                                else
                                {
                                    lastInfrastructure.SecondaryTreatmentType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SECONDARYTREATMENTTYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.SecondaryTreatmentTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.SecondaryTreatmentTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TERTIARYTREATMENTTYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.TertiaryTreatmentType = null;
                                }
                                else
                                {
                                    lastInfrastructure.TertiaryTreatmentType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TERTIARYTREATMENTTYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.TertiaryTreatmentTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.TertiaryTreatmentTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TREATMENTTYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.TreatmentType = null;
                                }
                                else
                                {
                                    lastInfrastructure.TreatmentType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TREATMENTTYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.TreatmentTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.TreatmentTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "DISINFECTIONTYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.DisinfectionType = null;
                                }
                                else
                                {
                                    lastInfrastructure.DisinfectionType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "DISINFECTIONTYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.DisinfectionTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.DisinfectionTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "COLLECTIONSYSTEMTYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.CollectionSystemType = null;
                                }
                                else
                                {
                                    lastInfrastructure.CollectionSystemType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "COLLECTIONSYSTEMTYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.CollectionSystemTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.CollectionSystemTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ALARMSYSTEMTYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.AlarmSystemType = null;
                                }
                                else
                                {
                                    lastInfrastructure.AlarmSystemType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ALARMSYSTEMTYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.AlarmSystemTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.AlarmSystemTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "DESIGNFLOW_M3_DAY":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.DesignFlow_m3_day = null;
                                }
                                else
                                {
                                    lastInfrastructure.DesignFlow_m3_day = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "DESIGNFLOW_M3_DAYNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.DesignFlow_m3_dayNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.DesignFlow_m3_dayNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "AVERAGEFLOW_M3_DAY":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.AverageFlow_m3_day = null;
                                }
                                else
                                {
                                    lastInfrastructure.AverageFlow_m3_day = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "AVERAGEFLOW_M3_DAYNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.AverageFlow_m3_dayNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.AverageFlow_m3_dayNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PEAKFLOW_M3_DAY":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PeakFlow_m3_day = null;
                                }
                                else
                                {
                                    lastInfrastructure.PeakFlow_m3_day = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PEAKFLOW_M3_DAYNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PeakFlow_m3_dayNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.PeakFlow_m3_dayNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "POPSERVED":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PopServed = null;
                                }
                                else
                                {
                                    lastInfrastructure.PopServed = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "POPSERVEDNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PopServedNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.PopServedNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "CANOVERFLOW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.CanOverflow = null;
                                }
                                else
                                {
                                    lastInfrastructure.CanOverflow = bool.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "CANOVERFLOWNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.CanOverflowNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.CanOverflowNew = bool.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PERCFLOWOFTOTAL":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PercFlowOfTotal = null;
                                }
                                else
                                {
                                    lastInfrastructure.PercFlowOfTotal = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PERCFLOWOFTOTALNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PercFlowOfTotalNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.PercFlowOfTotalNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TIMEOFFSET_HOUR":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.TimeOffset_hour = null;
                                }
                                else
                                {
                                    lastInfrastructure.TimeOffset_hour = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TIMEOFFSET_HOURNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.TimeOffset_hourNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.TimeOffset_hourNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TEMPCATCHALLREMOVELATER":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.TempCatchAllRemoveLater = LineTxt.Substring(pos + 1, pos2 - pos - 1).Replace("|||", "\r\n");
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TEMPCATCHALLREMOVELATERNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.TempCatchAllRemoveLaterNew = LineTxt.Substring(pos + 1, pos2 - pos - 1).Replace("|||", "\r\n");
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "AVERAGEDEPTH_M":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.AverageDepth_m = null;
                                }
                                else
                                {
                                    lastInfrastructure.AverageDepth_m = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "AVERAGEDEPTH_MNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.AverageDepth_mNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.AverageDepth_mNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "NUMBEROFPORTS":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.NumberOfPorts = null;
                                }
                                else
                                {
                                    lastInfrastructure.NumberOfPorts = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "NUMBEROFPORTSNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.NumberOfPortsNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.NumberOfPortsNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PORTDIAMETER_M":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PortDiameter_m = null;
                                }
                                else
                                {
                                    lastInfrastructure.PortDiameter_m = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PORTDIAMETER_MNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PortDiameter_mNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.PortDiameter_mNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PORTSPACING_M":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PortSpacing_m = null;
                                }
                                else
                                {
                                    lastInfrastructure.PortSpacing_m = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PORTSPACING_MNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PortSpacing_mNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.PortSpacing_mNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PORTELEVATION_M":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PortElevation_m = null;
                                }
                                else
                                {
                                    lastInfrastructure.PortElevation_m = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PORTELEVATION_MNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PortElevation_mNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.PortElevation_mNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "VERTICALANGLE_DEG":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.VerticalAngle_deg = null;
                                }
                                else
                                {
                                    lastInfrastructure.VerticalAngle_deg = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "VERTICALANGLE_DEGNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.VerticalAngle_degNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.VerticalAngle_degNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "HORIZONTALANGLE_DEG":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.HorizontalAngle_deg = null;
                                }
                                else
                                {
                                    lastInfrastructure.HorizontalAngle_deg = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "HORIZONTALANGLE_DEGNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.HorizontalAngle_degNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.HorizontalAngle_degNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "DECAYRATE_PER_DAY":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.DecayRate_per_day = null;
                                }
                                else
                                {
                                    lastInfrastructure.DecayRate_per_day = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "DECAYRATE_PER_DAYNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.DecayRate_per_dayNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.DecayRate_per_dayNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "NEARFIELDVELOCITY_M_S":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.NearFieldVelocity_m_s = null;
                                }
                                else
                                {
                                    lastInfrastructure.NearFieldVelocity_m_s = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "NEARFIELDVELOCITY_M_SNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.NearFieldVelocity_m_sNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.NearFieldVelocity_m_sNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "FARFIELDVELOCITY_M_S":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.FarFieldVelocity_m_s = null;
                                }
                                else
                                {
                                    lastInfrastructure.FarFieldVelocity_m_s = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "FARFIELDVELOCITY_M_SNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.FarFieldVelocity_m_sNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.FarFieldVelocity_m_sNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "RECEIVINGWATERSALINITY_PSU":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.ReceivingWaterSalinity_PSU = null;
                                }
                                else
                                {
                                    lastInfrastructure.ReceivingWaterSalinity_PSU = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "RECEIVINGWATERSALINITY_PSUNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.ReceivingWaterSalinity_PSUNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.ReceivingWaterSalinity_PSUNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "RECEIVINGWATERTEMPERATURE_C":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.ReceivingWaterTemperature_C = null;
                                }
                                else
                                {
                                    lastInfrastructure.ReceivingWaterTemperature_C = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "RECEIVINGWATERTEMPERATURE_CNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.ReceivingWaterTemperature_CNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.ReceivingWaterTemperature_CNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "RECEIVINGWATER_MPN_PER_100ML":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.ReceivingWater_MPN_per_100ml = null;
                                }
                                else
                                {
                                    lastInfrastructure.ReceivingWater_MPN_per_100ml = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "RECEIVINGWATER_MPN_PER_100MLNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.ReceivingWater_MPN_per_100mlNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.ReceivingWater_MPN_per_100mlNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "DISTANCEFROMSHORE_M":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.DistanceFromShore_m = null;
                                }
                                else
                                {
                                    lastInfrastructure.DistanceFromShore_m = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "DISTANCEFROMSHORE_MNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.DistanceFromShore_mNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.DistanceFromShore_mNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SEEOTHERMUNICIPALITYTVITEMID":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.SeeOtherMunicipalityTVItemID = null;
                                }
                                else
                                {
                                    lastInfrastructure.SeeOtherMunicipalityTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SEEOTHERMUNICIPALITYTVITEMIDNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.SeeOtherMunicipalityTVItemIDNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.SeeOtherMunicipalityTVItemIDNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ADDRESS":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                Address address = new Address();
                                double tempDouble = 0;
                                int temp = 0;
                                if (double.TryParse(LineTxt.Substring(pos + 1, pos2 - pos - 1), out tempDouble))
                                {
                                    address.AddressTVItemID = (int)tempDouble;
                                }
                                else
                                {
                                    address.AddressTVItemID = null;
                                }
                                address.Municipality = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                if (!string.IsNullOrWhiteSpace(address.Municipality))
                                {
                                    address.Municipality = address.Municipality.Trim();
                                }
                                if (int.TryParse(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1), out temp))
                                {
                                    address.AddressType = temp;
                                }
                                else
                                {
                                    address.AddressType = null;
                                }
                                address.StreetNumber = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                address.StreetName = LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1);
                                if (int.TryParse(LineTxt.Substring(pos6 + 1, pos7 - pos6 - 1), out temp))
                                {
                                    address.StreetType = temp;
                                }
                                else
                                {
                                    address.StreetType = null;
                                }
                                address.PostalCode = LineTxt.Substring(pos7 + 1, pos8 - pos7 - 1);
                                lastInfrastructure.InfrastructureAddress = address;
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ADDRESSNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                Address address = new Address();
                                double tempDouble = 0;
                                int temp = 0;
                                if (double.TryParse(LineTxt.Substring(pos + 1, pos2 - pos - 1), out tempDouble))
                                {
                                    address.AddressTVItemID = (int)temp;
                                }
                                else
                                {
                                    address.AddressTVItemID = null;
                                }
                                address.Municipality = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                if (int.TryParse(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1), out temp))
                                {
                                    address.AddressType = temp;
                                }
                                else
                                {
                                    address.AddressType = null;
                                }
                                address.StreetNumber = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                address.StreetName = LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1);
                                if (int.TryParse(LineTxt.Substring(pos6 + 1, pos7 - pos6 - 1), out temp))
                                {
                                    address.StreetType = temp;
                                }
                                else
                                {
                                    address.StreetType = null;
                                }
                                address.PostalCode = LineTxt.Substring(pos7 + 1, pos8 - pos7 - 1);
                                lastInfrastructure.InfrastructureAddressNew = address;
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTURE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                Picture picture = new Picture();
                                picture.PictureTVItemID = (int)(double.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                picture.FileName = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                picture.Extension = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                picture.Description = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                picture.ToRemove = null;
                                lastInfrastructure.InfrastructurePictureList.Add(picture);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTURENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                Picture picture = new Picture();
                                picture.PictureTVItemID = (int)(double.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                picture.FileName = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                picture.Extension = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                picture.Description = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                picture.ToRemove = null;
                                lastInfrastructure.InfrastructurePictureList.Add(picture);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTURETOREMOVE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];
                                Picture lastPicture = lastInfrastructure.InfrastructurePictureList[lastInfrastructure.InfrastructurePictureList.Count - 1];

                                lastPicture.ToRemove = true;
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTUREFILENAMENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];
                                Picture lastPicture = lastInfrastructure.InfrastructurePictureList[lastInfrastructure.InfrastructurePictureList.Count - 1];

                                lastPicture.FileNameNew = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTUREEXTENSIONNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];
                                Picture lastPicture = lastInfrastructure.InfrastructurePictureList[lastInfrastructure.InfrastructurePictureList.Count - 1];

                                lastPicture.ExtensionNew = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTUREDESCRIPTIONNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];
                                Picture lastPicture = lastInfrastructure.InfrastructurePictureList[lastInfrastructure.InfrastructurePictureList.Count - 1];

                                lastPicture.DescriptionNew = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PUMPSTOTVITEMID":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PumpsToTVItemID = null;
                                }
                                else
                                {
                                    lastInfrastructure.PumpsToTVItemID = (int)(double.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PUMPSTOTVITEMIDNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.PumpsToTVItemIDNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.PumpsToTVItemIDNew = (int)(double.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PATHCOORDLIST":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                string tempPath = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                                List<string> pathListTextList = tempPath.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                                foreach (string s in pathListTextList)
                                {
                                    List<string> pathValueText = s.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

                                    if (pathValueText.Count != 2)
                                    {
                                        EmitStatus(new StatusEventArgs($"Path coord does not have 2 values { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                        return false;
                                    }

                                    Coord coord = new Coord();
                                    coord.Lat = float.Parse(pathValueText[0]);
                                    coord.Lng = float.Parse(pathValueText[1]);

                                    lastInfrastructure.PathCoordList.Add(coord);
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PATHCOORDLISTNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                string tempPath = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                                List<string> pathListTextList = tempPath.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                                foreach (string s in pathListTextList)
                                {
                                    List<string> pathValueText = s.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

                                    if (pathValueText.Count != 2)
                                    {
                                        EmitStatus(new StatusEventArgs($"Path coord does not have 2 values { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                        return false;
                                    }

                                    Coord coord = new Coord();
                                    coord.Lat = float.Parse(pathValueText[0]);
                                    coord.Lng = float.Parse(pathValueText[1]);

                                    lastInfrastructure.PathCoordList.Add(coord);
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    default:
                        {
                            string lineTxt = LineTxt.Substring(0, LineTxt.IndexOf("\t"));
                            EmitStatus(new StatusEventArgs($"First item in line { LineNumb } not recognized [{ lineTxt }]"));
                            return false;
                        }
                }

                OldLineObj = LineTxt.Substring(0, pos);
            }
            sr.Close();

            EmitStatus(new StatusEventArgs("Pollution Source Sites File OK."));
            return true;
        }
        public bool ReadPollutionSourceSitesSubsectorFile()
        {
            FileInfo fi = new FileInfo($@"{BasePathPollutionSourceSites}{CurrentSubsectorName}\{CurrentSubsectorName}.txt");

            if (!fi.Exists)
            {
                EmitStatus(new StatusEventArgs($"{fi.FullName} does not exist."));
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
                    EmitStatus(new StatusEventArgs("Pollution Source Sites File was not read properly. We found an empty line. The Pollution Source Sites file should not have empty lines."));
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
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PROVINCETVITEMID":
                        {
                            try
                            {
                                subsectorDoc.ProvinceTVItemID = int.Parse(LineTxt.Substring("PROVINCETVITEMID\t".Length));
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PROVINCEMUNICIPALITIES":
                        {
                            try
                            {
                                List<MunicipalityIDNumber> MunicipalityAndIDList = new List<MunicipalityIDNumber>();
                                string TempStr = LineTxt.Substring("PROVINCEMUNICIPALITIES\t".Length).Trim();
                                List<string> MunicipalityAndIDTextList = TempStr.Split("|".ToCharArray(), StringSplitOptions.None).Select(c => c.Trim()).ToList();
                                foreach (string s in MunicipalityAndIDTextList)
                                {
                                    if (string.IsNullOrWhiteSpace(s.Trim()))
                                    {
                                        continue;
                                    }
                                    string Municipality = s.Substring(0, s.IndexOf("[")).Trim();
                                    if (string.IsNullOrWhiteSpace(Municipality))
                                    {
                                        EmitStatus(new StatusEventArgs($"Could not read and parse { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                        return false;
                                    }
                                    string IDNumber = s.Substring(s.IndexOf("[") + 1);
                                    if (string.IsNullOrWhiteSpace(IDNumber))
                                    {
                                        EmitStatus(new StatusEventArgs($"Could not read and parse { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                        return false;
                                    }
                                    IDNumber = IDNumber.Replace("]", "");
                                    if (string.IsNullOrWhiteSpace(IDNumber))
                                    {
                                        EmitStatus(new StatusEventArgs($"Could not read and parse { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                        return false;
                                    }
                                    MunicipalityAndIDList.Add(new MunicipalityIDNumber() { Municipality = Municipality, IDNumber = IDNumber });
                                }
                                subsectorDoc.MunicipalityIDNumberList = MunicipalityAndIDList;
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SUBSECTOR":
                        {
                            try
                            {
                                Subsector subsector = new Subsector();
                                subsector.SubsectorTVItemID = (int)(float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                subsector.SubsectorName = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                subsectorDoc.Subsector = subsector;
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PSS":
                        {
                            try
                            {
                                PSS pss = new PSS();
                                pss.PSSTVItemID = (int)(double.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                pss.Lat = float.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                pss.Lng = float.Parse(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1));
                                pss.IsActive = bool.Parse(LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1));
                                pss.IsPointSource = bool.Parse(LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1));
                                subsectorDoc.Subsector.PSSList.Add(pss);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LATNEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.LatNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LNGNEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.LngNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ISACTIVENEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.IsActiveNew = bool.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ISPOINTSOURCENEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.IsPointSourceNew = bool.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SITENUMB":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.SiteNumber = (int)(float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                lastPSS.SiteNumberText = "00000".Substring(0, "00000".Length - lastPSS.SiteNumber.ToString().Length) + lastPSS.SiteNumber.ToString();
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "TVTEXTNEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.TVTextNew = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                double tempDouble = 0;
                                int temp = 0;
                                if (double.TryParse(LineTxt.Substring(pos + 1, pos2 - pos - 1), out tempDouble))
                                {
                                    address.AddressTVItemID = (int)tempDouble;
                                }
                                else
                                {
                                    address.AddressTVItemID = null;
                                }
                                address.Municipality = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                if (!string.IsNullOrWhiteSpace(address.Municipality))
                                {
                                    address.Municipality = address.Municipality.Trim();
                                }
                                else
                                {
                                    address.Municipality = null;
                                }
                                if (int.TryParse(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1), out temp))
                                {
                                    address.AddressType = temp;
                                }
                                else
                                {
                                    address.AddressType = null;
                                }
                                address.StreetNumber = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                address.StreetName = LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1);
                                if (int.TryParse(LineTxt.Substring(pos6 + 1, pos7 - pos6 - 1), out temp))
                                {
                                    address.StreetType = temp;
                                }
                                else
                                {
                                    address.StreetType = null;
                                }
                                address.PostalCode = LineTxt.Substring(pos7 + 1, pos8 - pos7 - 1);
                                lastPSS.PSSAddress = address;
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ADDRESSNEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                Address address = new Address();
                                double tempDouble = 0;
                                int temp = 0;
                                if (double.TryParse(LineTxt.Substring(pos + 1, pos2 - pos - 1), out tempDouble))
                                {
                                    address.AddressTVItemID = (int)tempDouble;
                                }
                                else
                                {
                                    address.AddressTVItemID = null;
                                }
                                address.Municipality = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                if (!string.IsNullOrWhiteSpace(address.Municipality))
                                {
                                    address.Municipality = address.Municipality.Trim();
                                }
                                else
                                {
                                    address.Municipality = null;
                                }
                                if (int.TryParse(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1), out temp))
                                {
                                    address.AddressType = temp;
                                }
                                else
                                {
                                    address.AddressType = null;
                                }
                                address.StreetNumber = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                address.StreetName = LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1);
                                if (int.TryParse(LineTxt.Substring(pos6 + 1, pos7 - pos6 - 1), out temp))
                                {
                                    address.StreetType = temp;
                                }
                                else
                                {
                                    address.StreetType = null;
                                }
                                address.PostalCode = LineTxt.Substring(pos7 + 1, pos8 - pos7 - 1);
                                lastPSS.PSSAddressNew = address;
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                picture.PictureTVItemID = (int)(double.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                picture.FileName = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                picture.Extension = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                picture.Description = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                picture.ToRemove = null;
                                lastPSS.PSSPictureList.Add(picture);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTURENEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                Picture picture = new Picture();
                                picture.PictureTVItemID = (int)(double.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                picture.FileName = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                picture.Extension = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                picture.Description = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                picture.ToRemove = null;
                                lastPSS.PSSPictureList.Add(picture);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTURETOREMOVE":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];
                                Picture lastPicture = lastPSS.PSSPictureList[lastPSS.PSSPictureList.Count - 1];

                                lastPicture.ToRemove = true;
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTUREFILENAMENEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];
                                Picture lastPicture = lastPSS.PSSPictureList[lastPSS.PSSPictureList.Count - 1];

                                lastPicture.FileNameNew = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTUREEXTENSIONNEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];
                                Picture lastPicture = lastPSS.PSSPictureList[lastPSS.PSSPictureList.Count - 1];

                                lastPicture.ExtensionNew = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTUREDESCRIPTIONNEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];
                                Picture lastPicture = lastPSS.PSSPictureList[lastPSS.PSSPictureList.Count - 1];

                                lastPicture.DescriptionNew = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                obs.ObsID = (int)(double.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));

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
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "OBSDATENEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                string TempStr = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                                int Year = int.Parse(TempStr.Substring(0, 4));
                                int Month = int.Parse(TempStr.Substring(5, 2));
                                int Day = int.Parse(TempStr.Substring(8, 2));
                                lastPSS.PSSObs.ObsDateNew = new DateTime(Year, Month, Day);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "OLDWRITTENDESC":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.PSSObs.OldWrittenDescription = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                issue.IssueID = (int)(double.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1)));
                                issue.Ordinal = int.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));

                                string TempStr = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                int Year = int.Parse(TempStr.Substring(0, 4));
                                int Month = int.Parse(TempStr.Substring(5, 2));
                                int Day = int.Parse(TempStr.Substring(8, 2));
                                int Hour = int.Parse(TempStr.Substring(11, 2));
                                int Minute = int.Parse(TempStr.Substring(14, 2));
                                int Second = int.Parse(TempStr.Substring(17, 2));
                                issue.LastUpdated_UTC = new DateTime(Year, Month, Day, Hour, Minute, Second);

                                issue.ToRemove = false;

                                string PolSourceObsInfoEnumTxt = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1).Trim();
                                issue.PolSourceObsInfoIntList = PolSourceObsInfoEnumTxt.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)).ToList();

                                issue.IsWellFormed = IssueWellFormed(issue, false);
                                issue.IsCompleted = IssueCompleted(issue, false);

                                lastPSS.PSSObs.IssueList.Add(issue);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ISSUENEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];
                                Issue lastIssue = lastPSS.PSSObs.IssueList[lastPSS.PSSObs.IssueList.Count - 1];

                                string PolSourceObsInfoEnumTxt = LineTxt.Substring(pos + 1, pos2 - pos - 1).Trim();
                                lastIssue.PolSourceObsInfoIntListNew = PolSourceObsInfoEnumTxt.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)).ToList();

                                lastIssue.IsWellFormed = IssueWellFormed(lastIssue, true);
                                lastIssue.IsCompleted = IssueCompleted(lastIssue, true);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "ISSUETOREMOVE":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];
                                Issue lastIssue = lastPSS.PSSObs.IssueList[lastPSS.PSSObs.IssueList.Count - 1];

                                lastIssue.ToRemove = true;
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "EXTRACOMMENT":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];
                                Issue lastIssue = lastPSS.PSSObs.IssueList[lastPSS.PSSObs.IssueList.Count - 1];

                                lastIssue.ExtraComment = LineTxt.Substring(pos + 1, pos2 - pos - 1).Replace("|||", "\r\n");
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "EXTRACOMMENTNEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];
                                Issue lastIssue = lastPSS.PSSObs.IssueList[lastPSS.PSSObs.IssueList.Count - 1];

                                lastIssue.ExtraCommentNew = LineTxt.Substring(pos + 1, pos2 - pos - 1).Replace("|||", "\r\n");
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    default:
                        {
                            string lineTxt = LineTxt.Substring(0, LineTxt.IndexOf("\t"));
                            EmitStatus(new StatusEventArgs($"First item in line { LineNumb } not recognized [{ lineTxt }]"));
                            return false;
                        }
                }

                OldLineObj = LineTxt.Substring(0, pos);
            }
            sr.Close();

            EmitStatus(new StatusEventArgs("Pollution Source Sites File OK."));
            return true;
        }
    }
}
