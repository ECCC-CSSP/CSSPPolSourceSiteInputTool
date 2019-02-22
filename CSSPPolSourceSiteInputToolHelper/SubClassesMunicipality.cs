using CSSPEnumsDLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSPPolSourceSiteInputToolHelper
{
    public class MunicipalityDoc
    {
        public int? Version { get; set; } = null;
        public DateTime? DocDate { get; set; } = null;
        public Municipality Municipality { get; set; } = null;
    }
    public class Municipality
    {
        public int? MunicipalityTVItemID { get; set; } = null;
        public string MunicipalityName { get; set; } = null;
        public List<Infrastructure> InfrastructureList { get; set; } = new List<Infrastructure>();
    }
    public class Infrastructure
    {
        public int? InfrastructureTVItemID { get; set; } = null;
        public bool? IsActive { get; set; } = null;
        public bool? IsActiveNew { get; set; } = null;
        public string TVText { get; set; } = null;
        public string TVTextNew { get; set; } = null;
        public string Comment { get; set; } = null;
        public string CommentNew { get; set; } = null;
        public float? Lat { get; set; } = null;
        public float? LatNew { get; set; } = null;
        public float? Lng { get; set; } = null;
        public float? LngNew { get; set; } = null;
        public float? LatOutfall { get; set; } = null;
        public float? LatOutfallNew { get; set; } = null;
        public float? LngOutfall { get; set; } = null;
        public float? LngOutfallNew { get; set; } = null;
        public int? PrismID { get; set; } = null;
        public int? PrismIDNew { get; set; } = null;
        public int? TPID { get; set; } = null;
        public int? TPIDNew { get; set; } = null;
        public int? LSID { get; set; } = null;
        public int? LSIDNew { get; set; } = null;
        public int? SiteID { get; set; } = null;
        public int? SiteIDNew { get; set; } = null;
        public int? Site { get; set; } = null;
        public int? SiteNew { get; set; } = null;
        public string InfrastructureCategory { get; set; } = null;
        public string InfrastructureCategoryNew { get; set; } = null;
        public int? InfrastructureType { get; set; } = null;
        public int? InfrastructureTypeNew { get; set; } = null;
        public int? FacilityType { get; set; } = null;
        public int? FacilityTypeNew { get; set; } = null;
        public bool? IsMechanicallyAerated { get; set; } = null;
        public bool? IsMechanicallyAeratedNew { get; set; } = null;
        public int? NumberOfCells { get; set; } = null;
        public int? NumberOfCellsNew { get; set; } = null;
        public int? NumberOfAeratedCells { get; set; } = null;
        public int? NumberOfAeratedCellsNew { get; set; } = null;
        public int? AerationType { get; set; } = null;
        public int? AerationTypeNew { get; set; } = null;
        public int? PreliminaryTreatmentType { get; set; } = null;
        public int? PreliminaryTreatmentTypeNew { get; set; } = null;
        public int? PrimaryTreatmentType { get; set; } = null;
        public int? PrimaryTreatmentTypeNew { get; set; } = null;
        public int? SecondaryTreatmentType { get; set; } = null;
        public int? SecondaryTreatmentTypeNew { get; set; } = null;
        public int? TertiaryTreatmentType { get; set; } = null;
        public int? TertiaryTreatmentTypeNew { get; set; } = null;
        public int? TreatmentType { get; set; } = null;
        public int? TreatmentTypeNew { get; set; } = null;
        public int? DisinfectionType { get; set; } = null;
        public int? DisinfectionTypeNew { get; set; } = null;
        public int? CollectionSystemType { get; set; } = null;
        public int? CollectionSystemTypeNew { get; set; } = null;
        public int? AlarmSystemType { get; set; } = null;
        public int? AlarmSystemTypeNew { get; set; } = null;
        public float? DesignFlow_m3_day { get; set; } = null;
        public float? DesignFlow_m3_dayNew { get; set; } = null;
        public float? AverageFlow_m3_day { get; set; } = null;
        public float? AverageFlow_m3_dayNew { get; set; } = null;
        public float? PeakFlow_m3_day { get; set; } = null;
        public float? PeakFlow_m3_dayNew { get; set; } = null;
        public int? PopServed { get; set; } = null;
        public int? PopServedNew { get; set; } = null;
        public bool? CanOverflow { get; set; } = null;
        public bool? CanOverflowNew { get; set; } = null;
        public float? PercFlowOfTotal { get; set; } = null;
        public float? PercFlowOfTotalNew { get; set; } = null;
        public float? TimeOffset_hour { get; set; } = null;
        public float? TimeOffset_hourNew { get; set; } = null;
        public string TempCatchAllRemoveLater { get; set; } = null;
        public string TempCatchAllRemoveLaterNew { get; set; } = null;
        public float? AverageDepth_m { get; set; } = null;
        public float? AverageDepth_mNew { get; set; } = null;
        public int? NumberOfPorts { get; set; } = null;
        public int? NumberOfPortsNew { get; set; } = null;
        public float? PortDiameter_m { get; set; } = null;
        public float? PortDiameter_mNew { get; set; } = null;
        public float? PortSpacing_m { get; set; } = null;
        public float? PortSpacing_mNew { get; set; } = null;
        public float? PortElevation_m { get; set; } = null;
        public float? PortElevation_mNew { get; set; } = null;
        public float? VerticalAngle_deg { get; set; } = null;
        public float? VerticalAngle_degNew { get; set; } = null;
        public float? HorizontalAngle_deg { get; set; } = null;
        public float? HorizontalAngle_degNew { get; set; } = null;
        public float? DecayRate_per_day { get; set; } = null;
        public float? DecayRate_per_dayNew { get; set; } = null;
        public float? NearFieldVelocity_m_s { get; set; } = null;
        public float? NearFieldVelocity_m_sNew { get; set; } = null;
        public float? FarFieldVelocity_m_s { get; set; } = null;
        public float? FarFieldVelocity_m_sNew { get; set; } = null;
        public float? ReceivingWaterSalinity_PSU { get; set; } = null;
        public float? ReceivingWaterSalinity_PSUNew { get; set; } = null;
        public float? ReceivingWaterTemperature_C { get; set; } = null;
        public float? ReceivingWaterTemperature_CNew { get; set; } = null;
        public int? ReceivingWater_MPN_per_100ml { get; set; } = null;
        public int? ReceivingWater_MPN_per_100mlNew { get; set; } = null;
        public float? DistanceFromShore_m { get; set; } = null;
        public float? DistanceFromShore_mNew { get; set; } = null;
        public int? SeeOtherMunicipalityTVItemID { get; set; } = null;
        public int? SeeOtherMunicipalityTVItemIDNew { get; set; } = null;
        public Address InfrastructureAddress { get; set; } = new Address();
        public Address InfrastructureAddressNew { get; set; } = new Address();
        public DateTime? LastUpdateDate_UTC { get; set; } = null;
        public List<Picture> InfrastructurePictureList { get; set; } = new List<Picture>();
        public int? PumpsToTVItemID { get; set; } = null;
        public int? PumpsToTVItemIDNew { get; set; } = null;
        public List<Coord> PathCoordList { get; set; } = new List<Coord>();
        public List<Coord> PathCoordListNew { get; set; } = new List<Coord>();
        public bool Shown { get; set; } = false;
        public int Ordinal { get; set; } = 0;
    }
    public class Coord
    {
        public float? Lat { get; set; } = null;
        public float? Lng { get; set; } = null;
    }
    public class EnumTextAndID
    {
        public string EnumText { get; set; }
        public int EnumID { get; set; }
    }
}
