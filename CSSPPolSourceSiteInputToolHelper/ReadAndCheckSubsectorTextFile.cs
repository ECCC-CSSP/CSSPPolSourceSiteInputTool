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
                    case "LATNEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                lastPSS.LatNew = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                int temp = 0;
                                if (int.TryParse(LineTxt.Substring(pos + 1, pos2 - pos - 1), out temp))
                                {
                                    address.AddressTVItemID = temp;
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
                                lastPSS.PSSAddress = address;
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                int temp = 0;
                                if (int.TryParse(LineTxt.Substring(pos + 1, pos2 - pos - 1), out temp))
                                {
                                    address.AddressTVItemID = temp;
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
                                lastPSS.PSSAddressNew = address;
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
                                picture.IsNew = null;
                                picture.ToRemove = null;
                                lastPSS.PSSPictureList.Add(picture);
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                picture.PictureTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                picture.FileName = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                picture.Extension = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                picture.Description = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                picture.IsNew = true;
                                picture.ToRemove = null;
                                lastPSS.PSSPictureList.Add(picture);
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "PICTURETOREMOVE":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                Picture picture = new Picture();
                                picture.PictureTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                picture.FileName = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                picture.Extension = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                picture.Description = LineTxt.Substring(pos4 + 1, pos5 - pos4 - 1);
                                picture.IsNew = null;
                                picture.ToRemove = true;
                                lastPSS.PSSPictureList.Add(picture);
                            }
                            catch (Exception)
                            {
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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
                                OnStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
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

                                issue.IsNew = false;
                                issue.ToRemove = false;

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
                    case "ISSUENEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                Issue issue = new Issue();
                                issue.IssueID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                issue.Ordinal = int.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                issue.LastUpdated_UTC = null;
                                issue.IsNew = true;
                                issue.ToRemove = false;

                                string PolSourceObsInfoEnumTxt = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1).Trim();
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
                    case "ISSUETOREMOVE":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];

                                Issue issue = new Issue();
                                issue.IssueID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                issue.Ordinal = int.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                issue.LastUpdated_UTC = null;
                                issue.IsNew = false;
                                issue.ToRemove = true;

                                string PolSourceObsInfoEnumTxt = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1).Trim();
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
    }
}
