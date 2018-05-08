using CSSPEnumsDLL.Enums;
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
        #endregion Properties

        #region Constructors
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
                            catch (Exception ex)
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
                                // 0123456789012345678
                                // 2018|03|02|08|23|12
                                string TempStr = LineTxt.Substring("DOCDATE\t".Length).Trim();
                                int Year = int.Parse(TempStr.Substring(0, 4));
                                int Month = int.Parse(TempStr.Substring(5, 2));
                                int Day = int.Parse(TempStr.Substring(8, 2));
                                int Hour = int.Parse(TempStr.Substring(11, 2));
                                int Minute = int.Parse(TempStr.Substring(14, 2));
                                int Second = int.Parse(TempStr.Substring(17, 2));
                                subsectorDoc.DocDate = new DateTime(Year, Month, Day, Hour, Minute, Second);
                            }
                            catch (Exception ex)
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
                            catch (Exception ex)
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
                            catch (Exception ex)
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
                            catch (Exception ex)
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
                                lastPSS.PSSPictureList.Add(picture);
                            }
                            catch (Exception ex)
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

                                lastPSS.PSSObsList.Add(obs);
                            }
                            catch (Exception ex)
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
                                Obs lastObs = lastPSS.PSSObsList[lastPSS.PSSObsList.Count - 1];

                                lastObs.Description = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                            }
                            catch (Exception ex)
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
                                Obs lastObs = lastPSS.PSSObsList[lastPSS.PSSObsList.Count - 1];

                                Issue issue = new Issue();
                                issue.IssueID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));

                                string TempStr = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                int Year = int.Parse(TempStr.Substring(0, 4));
                                int Month = int.Parse(TempStr.Substring(5, 2));
                                int Day = int.Parse(TempStr.Substring(8, 2));
                                int Hour = int.Parse(TempStr.Substring(11, 2));
                                int Minute = int.Parse(TempStr.Substring(14, 2));
                                int Second = int.Parse(TempStr.Substring(17, 2));
                                issue.LastUpdated_UTC = new DateTime(Year, Month, Day, Hour, Minute, Second);

                                string PolSourceObsInfoEnumTxt = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                issue.PolSourceObsInfoIntList = PolSourceObsInfoEnumTxt.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)).ToList();
                                lastObs.IssueList.Add(issue);
                            }
                            catch (Exception ex)
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
        public SubsectorDoc()
        {
            Version = 0;
            DocDate = new DateTime();
        }

        public int Version { get; set; }
        public DateTime DocDate { get; set; }
        public Subsector Subsector { get; set; }
    }
    public class Subsector
    {
        public Subsector()
        {
            SubsectorTVItemID = 0;
            PSSList = new List<PSS>();
        }

        public int SubsectorTVItemID { get; set; }
        public string SubsectorName { get; set; }
        public List<PSS> PSSList { get; set; }
    }
    public class PSS
    {
        public PSS()
        {
            PSSTVItemID = 0;
            Lat = 0.0f;
            Lng = 0.0f;
            IsActive = false;
            IsPointSource = false;
            PSSAddress = null;
            PSSPictureList = new List<Picture>();
            PSSObsList = new List<Obs>();
        }

        public int PSSTVItemID { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public bool IsActive { get; set; }
        public bool IsPointSource { get; set; }
        public Address PSSAddress { get; set; }
        public List<Picture> PSSPictureList { get; set; }
        public List<Obs> PSSObsList { get; set; }
    }
    public class Address
    {
        public Address()
        {
            Municipality = "";
            AddressType = 0;
            StreetNumber = "";
            StreetName = "";
            StreetType = 0;
            PostalCode = "";
        }

        public int AddressTVItemID { get; set; }
        public string Municipality { get; set; }
        public int AddressType { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public int StreetType { get; set; }
        public string PostalCode { get; set; }
    }
    public class Picture
    {
        public Picture()
        {
            PictureTVItemID = 0;
            FileName = "";
        }

        public int PictureTVItemID { get; set; }
        public string FileName { get; set; }
    }
    public class Obs
    {
        public Obs()
        {
            ObsID = 0;
            Description = "";
            LastUpdated_UTC = new DateTime();
            ObsDate = new DateTime();
            IssueList = new List<Issue>();
        }

        public int ObsID { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated_UTC { get; set; }
        public DateTime ObsDate { get; set; }
        public List<Issue> IssueList { get; set; }
    }
    public class Issue
    {
        public Issue()
        {
            IssueID = 0;
            LastUpdated_UTC = DateTime.UtcNow;
            PolSourceObsInfoIntList = new List<int>();
            PolSourceObsInfoEnumList = new List<PolSourceObsInfoEnum>();
        }

        public int IssueID { get; set; }
        public DateTime LastUpdated_UTC { get; set; }
        public List<int> PolSourceObsInfoIntList { get; set; }
        public List<PolSourceObsInfoEnum> PolSourceObsInfoEnumList { get; set; }
    }
    #endregion Sub Classes


}
