using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputToolHelper
{
    public partial class PolSourceSiteInputToolHelper
    {
        public void SaveSubsectorTextFile()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"VERSION\t{subsectorDoc.Version}\t");
            sb.AppendLine($"DOCDATE\t{((DateTime)subsectorDoc.DocDate).Year}|{((DateTime)subsectorDoc.DocDate).Month.ToString("0#")}|{((DateTime)subsectorDoc.DocDate).Day.ToString("0#")}|{((DateTime)subsectorDoc.DocDate).Hour.ToString("0#")}|{((DateTime)subsectorDoc.DocDate).Minute.ToString("0#")}|{((DateTime)subsectorDoc.DocDate).Second.ToString("0#")}\t");
            sb.AppendLine($"SUBSECTOR\t{subsectorDoc.Subsector.SubsectorTVItemID}\t{subsectorDoc.Subsector.SubsectorName}\t");
            foreach (PSS pss in subsectorDoc.Subsector.PSSList)
            {
                sb.AppendLine($"-----\t-------------------------------------------------\t");
                sb.AppendLine($"PSS\t{pss.PSSTVItemID}\t{((float)pss.Lat).ToString("F5")}\t{((float)pss.Lng).ToString("F5")}\t{(((bool)pss.IsActive) ? "true" : "false")}\t{(((bool)pss.IsPointSource) ? "true" : "false")}\t");
                if (pss.LatNew != null)
                {
                    sb.AppendLine($"LATNEW\t{((float)pss.LatNew).ToString("F5")}\t");
                }
                if (pss.LngNew != null)
                {
                    sb.AppendLine($"LNGNEW\t{((float)pss.LngNew).ToString("F5")}\t");
                }
                if (pss.IsActiveNew != null)
                {
                    sb.AppendLine($"ISACTIVENEW\t{(((bool)pss.IsActiveNew) ? "true" : "false")}\t");
                }
                if (pss.IsPointSourceNew != null)
                {
                    sb.AppendLine($"ISPOINTSOURCENEW\t{(((bool)pss.IsPointSourceNew) ? "true" : "false")}\t");
                }
                sb.AppendLine($"SITENUMB\t{pss.SiteNumber}\t");
                sb.AppendLine($"TVTEXT\t{pss.TVText}\t");

                if (pss.PSSAddress != null)
                {
                    if (pss.PSSAddress.AddressTVItemID != null)
                    {
                        string AddressTVItemID = pss.PSSAddress.AddressTVItemID == null ? "-999999999" : pss.PSSAddress.AddressTVItemID.ToString();
                        string Municipality = pss.PSSAddress.Municipality == null ? "" : pss.PSSAddress.Municipality;
                        string AddressType = pss.PSSAddress.AddressType == null ? "" : ((int)pss.PSSAddress.AddressType).ToString();
                        string StreetNumber = pss.PSSAddress.StreetNumber == null ? "" : pss.PSSAddress.StreetNumber;
                        string StreetName = pss.PSSAddress.StreetName == null ? "" : pss.PSSAddress.StreetName;
                        string StreetType = pss.PSSAddress.StreetType == null ? "" : ((int)pss.PSSAddress.StreetType).ToString();
                        string PostalCode = pss.PSSAddress.PostalCode == null ? "" : pss.PSSAddress.PostalCode;

                        sb.AppendLine($"ADDRESS\t{pss.PSSAddress.AddressTVItemID}\t{pss.PSSAddress.Municipality}\t{((int)pss.PSSAddress.AddressType).ToString()}\t{pss.PSSAddress.StreetNumber}\t{pss.PSSAddress.StreetName}\t{((int)pss.PSSAddress.StreetType).ToString()}\t{pss.PSSAddress.PostalCode}\t");
                    }
                    if (pss.PSSAddress.AddressTVItemID != null)
                    {
                        string AddressTVItemID = pss.PSSAddressNew.AddressTVItemID == null ? "-999999999" : pss.PSSAddressNew.AddressTVItemID.ToString();
                        string Municipality = pss.PSSAddressNew.Municipality == null ? "" : pss.PSSAddressNew.Municipality;
                        string AddressType = pss.PSSAddressNew.AddressType == null ? "" : ((int)pss.PSSAddressNew.AddressType).ToString();
                        string StreetNumber = pss.PSSAddressNew.StreetNumber == null ? "" : pss.PSSAddressNew.StreetNumber;
                        string StreetName = pss.PSSAddressNew.StreetName == null ? "" : pss.PSSAddressNew.StreetName;
                        string StreetType = pss.PSSAddressNew.StreetType == null ? "" : ((int)pss.PSSAddressNew.StreetType).ToString();
                        string PostalCode = pss.PSSAddressNew.PostalCode == null ? "" : pss.PSSAddressNew.PostalCode;
                        sb.AppendLine($"ADDRESSNEW\t{AddressTVItemID}\t{Municipality}\t{AddressType}\t{StreetNumber}\t{StreetName}\t{StreetType}\t{PostalCode}\t");
                    }
                }

                foreach (Picture picture in pss.PSSPictureList)
                {
                    if (picture.IsNew != null && picture.ToRemove != null)
                    {
                        OnStatus(new StatusEventArgs("Picture.IsNew and Picture.ToRemove cannot be true at the same time."));
                        return;
                    }
                    else if (picture.IsNew != null)
                    {
                        sb.AppendLine($"PICTURENEW\t{picture.PictureTVItemID}\t{picture.FileName}\t{picture.Extension}\t{picture.Description}\t");
                    }
                    else if (picture.ToRemove != null)
                    {
                        sb.AppendLine($"PICTURETOREMOVE\t{picture.PictureTVItemID}\t{picture.FileName}\t{picture.Extension}\t{picture.Description}\t");
                    }
                    else
                    {
                        sb.AppendLine($"PICTURE\t{picture.PictureTVItemID}\t{picture.FileName}\t{picture.Extension}\t{picture.Description}\t");
                        if (picture.FileNameNew != null)
                        {
                            sb.AppendLine($"PICTUREFILENAMENEW\t{picture.FileNameNew}\t");
                        }
                        if (picture.ExtensionNew != null)
                        {
                            sb.AppendLine($"PICTUREEXTENSIONNEW\t{picture.ExtensionNew}\t");
                        }
                        if (picture.DescriptionNew != null)
                        {
                            sb.AppendLine($"PICTUREDESCRIPTIONNEW\t{picture.DescriptionNew}\t");
                        }
                    }
                }

                sb.AppendLine($"OBS\t{pss.PSSObs.ObsID}\t" +
                    $"{((DateTime)pss.PSSObs.LastUpdated_UTC).Year}|{((DateTime)pss.PSSObs.LastUpdated_UTC).Month.ToString("0#")}|" +
                    $"{((DateTime)pss.PSSObs.LastUpdated_UTC).Day.ToString("0#")}|{((DateTime)pss.PSSObs.LastUpdated_UTC).Hour.ToString("0#")}|" +
                    $"{((DateTime)pss.PSSObs.LastUpdated_UTC).Minute.ToString("0#")}|{((DateTime)pss.PSSObs.LastUpdated_UTC).Second.ToString("0#")}" +
                    $"\t{((DateTime)pss.PSSObs.ObsDate).Year}|{((DateTime)pss.PSSObs.ObsDate).Month.ToString("0#")}|" +
                    $"{((DateTime)pss.PSSObs.ObsDate).Day.ToString("0#")}\t");

                if (pss.PSSObs.ObsDateNew != null)
                {
                    sb.AppendLine($"OBSDATENEW\t{((DateTime)pss.PSSObs.ObsDateNew).Year}|" +
                        $"{((DateTime)pss.PSSObs.ObsDateNew).Month.ToString("0#")}|" +
                        $"{((DateTime)pss.PSSObs.ObsDateNew).Day.ToString("0#")}\t");
                }

                if (!string.IsNullOrWhiteSpace(pss.PSSObs.OldWrittenDescription))
                {
                    sb.AppendLine($"OLDWRITTENDESC\t{pss.PSSObs.OldWrittenDescription}\t");
                }

                foreach (string oldIssueText in pss.OldIssueTextList)
                {
                    sb.AppendLine($"OLDISSUETEXT\t{oldIssueText}\t");
                }

                foreach (Issue issue in pss.PSSObs.IssueList)
                {
                    if (issue.IsNew != null && issue.ToRemove != null)
                    {
                    }
                    else if (issue.IsNew != null)
                    {
                        sb.AppendLine($"ISSUENEW\t{issue.IssueID}\t{issue.Ordinal}\t{String.Join(",", issue.PolSourceObsInfoIntList)},\t");
                    }
                    else if (issue.ToRemove != null)
                    {
                        sb.AppendLine($"ISSUETOREMOVE\t{issue.IssueID}\t{issue.Ordinal}\t{String.Join(",", issue.PolSourceObsInfoIntList)},\t");
                    }
                    else
                    {
                        sb.AppendLine($"ISSUE\t{issue.IssueID}\t{issue.Ordinal}\t{((DateTime)issue.LastUpdated_UTC).Year}|{((DateTime)issue.LastUpdated_UTC).Month.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Day.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Hour.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Minute.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Second.ToString("0#")}\t{String.Join(",", issue.PolSourceObsInfoIntList)},\t");
                    }
                }
            }

            DirectoryInfo di = new DirectoryInfo($@"C:\PollutionSourceSites\{CurrentSubsectorName}\{CurrentSubsectorName}\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception)
                {
                    OnStatus(new StatusEventArgs("Could not create directory [" + di.FullName + "]"));
                }
            }

            FileInfo fi = new FileInfo($@"C:\PollutionSourceSites\{CurrentSubsectorName}\{CurrentSubsectorName}.txt");

            StreamWriter sw = fi.CreateText();
            sw.Write(sb.ToString());
            sw.Close();
        }
    }
}
