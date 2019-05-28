using CSSPEnumsDLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSSPPolSourceSiteInputToolHelper.PolSourceSiteInputToolHelper;

namespace CSSPPolSourceSiteInputToolHelper
{

    public class SubsectorDoc
    {
        public int? Version { get; set; } = null;
        public DateTime? DocDate { get; set; } = null;
        public int ProvinceTVItemID { get; set; } = 0;
        public List<MunicipalityIDNumber> MunicipalityIDNumberList { get; set; } = new List<MunicipalityIDNumber>();
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
        public bool? FromWater { get; set; } = null;
        public bool? FromWaterNew { get; set; } = null;
    }
    public class Obs
    {
        public int? ObsID { get; set; } = null;
        public string OldWrittenDescription { get; set; } = null;
        public DateTime? LastUpdated_UTC { get; set; } = null;
        public DateTime? ObsDate { get; set; } = null;
        public DateTime? ObsDateNew { get; set; } = null;
        public List<Issue> IssueList { get; set; } = new List<Issue>();
        public bool? ToRemove { get; set; } = null;
    }
    public class Issue
    {
        public int? IssueID { get; set; } = null;
        public int? Ordinal { get; set; } = null;
        public DateTime? LastUpdated_UTC { get; set; } = null;
        public List<int> PolSourceObsInfoIntList { get; set; } = new List<int>();
        public List<int> PolSourceObsInfoIntListNew { get; set; } = new List<int>();
        public bool? ToRemove { get; set; } = null;
        public bool? IsWellFormed { get; set; } = null;
        public bool? IsCompleted { get; set; } = null;
        public string ExtraComment { get; set; } = null;
        public string ExtraCommentNew { get; set; } = null;
    }

}
