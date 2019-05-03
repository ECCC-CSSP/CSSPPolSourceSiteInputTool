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
                                if (string.IsNullOrWhiteSpace(address.Municipality))
                                {
                                    address.Municipality = null;
                                }
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
                                if (string.IsNullOrWhiteSpace(address.StreetNumber))
                                {
                                    address.StreetNumber = null;
                                }
                                address.StreetName = LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1);
                                if (string.IsNullOrWhiteSpace(address.StreetName))
                                {
                                    address.StreetName = null;
                                }
                                if (int.TryParse(LineTxt.Substring(pos6 + 1, pos7 - pos6 - 1), out temp))
                                {
                                    address.StreetType = temp;
                                }
                                else
                                {
                                    address.StreetType = null;
                                }
                                address.PostalCode = LineTxt.Substring(pos7 + 1, pos8 - pos7 - 1);
                                if (string.IsNullOrWhiteSpace(address.PostalCode))
                                {
                                    address.PostalCode = null;
                                }
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
                                if (string.IsNullOrWhiteSpace(address.Municipality))
                                {
                                    address.Municipality = null;
                                }
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
                                if (string.IsNullOrWhiteSpace(address.StreetNumber))
                                {
                                    address.StreetNumber = null;
                                }
                                address.StreetName = LineTxt.Substring(pos5 + 1, pos6 - pos5 - 1);
                                if (string.IsNullOrWhiteSpace(address.StreetName))
                                {
                                    address.StreetName = null;
                                }
                                if (int.TryParse(LineTxt.Substring(pos6 + 1, pos7 - pos6 - 1), out temp))
                                {
                                    address.StreetType = temp;
                                }
                                else
                                {
                                    address.StreetType = null;
                                }
                                address.PostalCode = LineTxt.Substring(pos7 + 1, pos8 - pos7 - 1);
                                if (string.IsNullOrWhiteSpace(address.PostalCode))
                                {
                                    address.PostalCode = null;
                                }
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
