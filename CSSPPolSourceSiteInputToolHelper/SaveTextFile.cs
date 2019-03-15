using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using CSSPModelsDLL.Models;
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
        public void SaveMunicipalityTextFile()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"VERSION\t{municipalityDoc.Version}\t");
            sb.AppendLine($"DOCDATE\t{((DateTime)municipalityDoc.DocDate).Year}|{((DateTime)municipalityDoc.DocDate).Month.ToString("0#")}|{((DateTime)municipalityDoc.DocDate).Day.ToString("0#")}|{((DateTime)municipalityDoc.DocDate).Hour.ToString("0#")}|{((DateTime)municipalityDoc.DocDate).Minute.ToString("0#")}|{((DateTime)municipalityDoc.DocDate).Second.ToString("0#")}\t");

            sb.AppendLine($"PROVINCETVITEMID\t{ subsectorDoc.ProvinceTVItemID }");

            sb.AppendLine($"MUNICIPALITY\t{municipalityDoc.Municipality.MunicipalityTVItemID}\t{municipalityDoc.Municipality.MunicipalityName}\t");
            foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList)
            {
                sb.AppendLine($"-----\t-------------------------------------------------\t");
                string LatText = infrastructure.Lat == null ? "" : ((float)infrastructure.Lat).ToString("F5");
                string LngText = infrastructure.Lng == null ? "" : ((float)infrastructure.Lng).ToString("F5");
                string LatOutfallText = infrastructure.LatOutfall == null ? "" : ((float)infrastructure.LatOutfall).ToString("F5");
                string LngOutfallText = infrastructure.LngOutfall == null ? "" : ((float)infrastructure.LngOutfall).ToString("F5");
                string IsActiveText = infrastructure.IsActive == null ? "false" : (((bool)infrastructure.IsActive) ? "true" : "false");

                sb.AppendLine($"INFRASTRUCTURE\t{((double)infrastructure.InfrastructureTVItemID).ToString("F0")}\t{((DateTime)infrastructure.LastUpdateDate_UTC).Year}|" +
                    $"{((DateTime)infrastructure.LastUpdateDate_UTC).Month.ToString("0#")}|" +
                    $"{((DateTime)infrastructure.LastUpdateDate_UTC).Day.ToString("0#")}|{((DateTime)infrastructure.LastUpdateDate_UTC).Hour.ToString("0#")}|" +
                    $"{((DateTime)infrastructure.LastUpdateDate_UTC).Minute.ToString("0#")}|{((DateTime)infrastructure.LastUpdateDate_UTC).Second.ToString("0#")}\t" +
                    $"{LatText}\t{LngText}\t{LatOutfallText}\t{LngOutfallText}\t{IsActiveText}\t");

                sb.AppendLine($"TVTEXT\t{infrastructure.TVText.Replace(",", "_").Replace("\t", "_").Replace("\r", "_").Replace("\n", "_")}\t");

                if (!string.IsNullOrWhiteSpace(infrastructure.TVTextNew))
                {
                    sb.AppendLine($"TVTEXTNEW\t{infrastructure.TVTextNew.Replace(",", "_").Replace("\t", "_").Replace("\r", "_").Replace("\n", "_")}\t");
                }
                if (infrastructure.LatNew != null)
                {
                    sb.AppendLine($"LATNEW\t{((float)infrastructure.LatNew).ToString("F5")}\t");
                }
                if (infrastructure.LngNew != null)
                {
                    sb.AppendLine($"LNGNEW\t{((float)infrastructure.LngNew).ToString("F5")}\t");
                }
                if (infrastructure.LatOutfallNew != null)
                {
                    sb.AppendLine($"LATOUTFALLNEW\t{((float)infrastructure.LatOutfallNew).ToString("F5")}\t");
                }
                if (infrastructure.LngOutfallNew != null)
                {
                    sb.AppendLine($"LNGOUTFALLNEW\t{((float)infrastructure.LngOutfallNew).ToString("F5")}\t");
                }
                if (infrastructure.IsActiveNew != null)
                {
                    sb.AppendLine($"ISACTIVENEW\t{(((bool)infrastructure.IsActiveNew) ? "true" : "false")}\t");
                }
                sb.AppendLine($"TVTEXT\t{infrastructure.TVText}\t");
                if (!string.IsNullOrWhiteSpace(infrastructure.TVTextNew))
                {
                    sb.AppendLine($"TVTEXTNEW\t{infrastructure.TVTextNew}\t");
                }
                string Comment = string.IsNullOrWhiteSpace(infrastructure.Comment) ? "" : infrastructure.Comment.Replace("\r\n", "|||");
                sb.AppendLine($"COMMENT\t{Comment}\t");
                if (!string.IsNullOrWhiteSpace(infrastructure.CommentNew))
                {
                    string CommentNew = string.IsNullOrWhiteSpace(infrastructure.CommentNew) ? "" : infrastructure.CommentNew.Replace("\r\n", "|||");
                    sb.AppendLine($"COMMENTNEW\t{CommentNew}\t");
                }
                sb.AppendLine($"PRISMID\t{infrastructure.PrismID}\t");
                if (infrastructure.PrismIDNew != null)
                {
                    sb.AppendLine($"PRISMIDNEW\t{infrastructure.PrismIDNew}\t");
                }
                sb.AppendLine($"TPID\t{infrastructure.TPID}\t");
                if (infrastructure.TPIDNew != null)
                {
                    sb.AppendLine($"TPIDNEW\t{infrastructure.TPIDNew}\t");
                }
                sb.AppendLine($"LSID\t{infrastructure.LSID}\t");
                if (infrastructure.LSIDNew != null)
                {
                    sb.AppendLine($"LSIDNEW\t{infrastructure.LSIDNew}\t");
                }
                sb.AppendLine($"SITEID\t{infrastructure.SiteID}\t");
                if (infrastructure.SiteIDNew != null)
                {
                    sb.AppendLine($"SITEIDNEW\t{infrastructure.SiteIDNew}\t");
                }
                sb.AppendLine($"SITE\t{infrastructure.Site}\t");
                if (infrastructure.SiteNew != null)
                {
                    sb.AppendLine($"SITENEW\t{infrastructure.SiteNew}\t");
                }
                sb.AppendLine($"INFRASTRUCTURECATEGORY\t{infrastructure.InfrastructureCategory}\t");
                if (infrastructure.InfrastructureCategoryNew != null)
                {
                    sb.AppendLine($"INFRASTRUCTURECATEGORYNEW\t{infrastructure.InfrastructureCategoryNew}\t");
                }
                sb.AppendLine($"INFRASTRUCTURETYPE\t{infrastructure.InfrastructureType}\t");
                if (infrastructure.InfrastructureTypeNew != null)
                {
                    sb.AppendLine($"INFRASTRUCTURETYPENEW\t{infrastructure.InfrastructureTypeNew}\t");
                }
                sb.AppendLine($"FACILITYTYPE\t{infrastructure.FacilityType}\t");
                if (infrastructure.FacilityTypeNew != null)
                {
                    sb.AppendLine($"FACILITYTYPENEW\t{infrastructure.FacilityTypeNew}\t");
                }
                string IsMechanicallyAerated = infrastructure.IsMechanicallyAerated == true ? "true" : "false";
                sb.AppendLine($"ISMECHANICALLYAERATED\t{IsMechanicallyAerated}\t");
                if (infrastructure.IsMechanicallyAeratedNew != null)
                {
                    string IsMechanicallyAeratedNew = infrastructure.IsMechanicallyAeratedNew == true ? "true" : "false";
                    sb.AppendLine($"ISMECHANICALLYAERATEDNEW\t{IsMechanicallyAeratedNew}\t");
                }
                sb.AppendLine($"NUMBEROFCELLS\t{infrastructure.NumberOfCells}\t");
                if (infrastructure.NumberOfCellsNew != null)
                {
                    sb.AppendLine($"NUMBEROFCELLSNEW\t{infrastructure.NumberOfCellsNew}\t");
                }
                sb.AppendLine($"NUMBEROFAERATEDCELLS\t{infrastructure.NumberOfAeratedCells}\t");
                if (infrastructure.NumberOfAeratedCellsNew != null)
                {
                    sb.AppendLine($"NUMBEROFAERATEDCELLSNEW\t{infrastructure.NumberOfAeratedCellsNew}\t");
                }
                sb.AppendLine($"AERATIONTYPE\t{infrastructure.AerationType}\t");
                if (infrastructure.AerationTypeNew != null)
                {
                    sb.AppendLine($"AERATIONTYPENEW\t{infrastructure.AerationTypeNew}\t");
                }
                sb.AppendLine($"PRELIMINARYTREATMENTTYPE\t{infrastructure.PreliminaryTreatmentType}\t");
                if (infrastructure.PreliminaryTreatmentTypeNew != null)
                {
                    sb.AppendLine($"PRELIMINARYTREATMENTTYPENEW\t{infrastructure.PreliminaryTreatmentTypeNew}\t");
                }
                sb.AppendLine($"PRIMARYTREATMENTTYPE\t{infrastructure.PrimaryTreatmentType}\t");
                if (infrastructure.PrimaryTreatmentTypeNew != null)
                {
                    sb.AppendLine($"PRIMARYTREATMENTTYPENEW\t{infrastructure.PrimaryTreatmentTypeNew}\t");
                }
                sb.AppendLine($"SECONDARYTREATMENTTYPE\t{infrastructure.SecondaryTreatmentType}\t");
                if (infrastructure.SecondaryTreatmentTypeNew != null)
                {
                    sb.AppendLine($"SECONDARYTREATMENTTYPENEW\t{infrastructure.SecondaryTreatmentTypeNew}\t");
                }
                sb.AppendLine($"TERTIARYTREATMENTTYPE\t{infrastructure.TertiaryTreatmentType}\t");
                if (infrastructure.TertiaryTreatmentTypeNew != null)
                {
                    sb.AppendLine($"TERTIARYTREATMENTTYPENEW\t{infrastructure.TertiaryTreatmentTypeNew}\t");
                }
                sb.AppendLine($"TREATMENTTYPE\t{infrastructure.TreatmentType}\t");
                if (infrastructure.TreatmentTypeNew != null)
                {
                    sb.AppendLine($"TREATMENTTYPENEW\t{infrastructure.TreatmentTypeNew}\t");
                }
                sb.AppendLine($"DISINFECTIONTYPE\t{infrastructure.DisinfectionType}\t");
                if (infrastructure.DisinfectionTypeNew != null)
                {
                    sb.AppendLine($"DISINFECTIONTYPENEW\t{infrastructure.DisinfectionTypeNew}\t");
                }
                sb.AppendLine($"COLLECTIONSYSTEMTYPE\t{infrastructure.CollectionSystemType}\t");
                if (infrastructure.CollectionSystemTypeNew != null)
                {
                    sb.AppendLine($"COLLECTIONSYSTEMTYPENEW\t{infrastructure.CollectionSystemTypeNew}\t");
                }
                sb.AppendLine($"ALARMSYSTEMTYPE\t{infrastructure.AlarmSystemType}\t");
                if (infrastructure.AlarmSystemTypeNew != null)
                {
                    sb.AppendLine($"ALARMSYSTEMTYPENEW\t{infrastructure.AlarmSystemTypeNew}\t");
                }
                string DesignFlow_m3_day = infrastructure.DesignFlow_m3_day != null ? ((float)infrastructure.DesignFlow_m3_day).ToString("F1") : "";
                sb.AppendLine($"DESIGNFLOW_M3_DAY\t{DesignFlow_m3_day}\t");
                if (infrastructure.DesignFlow_m3_dayNew != null)
                {
                    string DesignFlow_m3_dayNew = infrastructure.DesignFlow_m3_dayNew != null ? ((float)infrastructure.DesignFlow_m3_dayNew).ToString("F1") : "";
                    sb.AppendLine($"DESIGNFLOW_M3_DAYNEW\t{DesignFlow_m3_dayNew}\t");
                }
                string AverageFlow_m3_day = infrastructure.AverageFlow_m3_day != null ? ((float)infrastructure.AverageFlow_m3_day).ToString("F1") : "";
                sb.AppendLine($"AVERAGEFLOW_M3_DAY\t{DesignFlow_m3_day}\t");
                if (infrastructure.AverageFlow_m3_dayNew != null)
                {
                    string AverageFlow_m3_dayNew = infrastructure.AverageFlow_m3_dayNew != null ? ((float)infrastructure.AverageFlow_m3_dayNew).ToString("F1") : "";
                    sb.AppendLine($"AVERAGEFLOW_M3_DAYNEW\t{AverageFlow_m3_dayNew}\t");
                }
                string PeakFlow_m3_day = infrastructure.PeakFlow_m3_day != null ? ((float)infrastructure.PeakFlow_m3_day).ToString("F1") : "";
                sb.AppendLine($"PEAKFLOW_M3_DAY\t{PeakFlow_m3_day}\t");
                if (infrastructure.PeakFlow_m3_dayNew != null)
                {
                    string PeakFlow_m3_dayNew = infrastructure.PeakFlow_m3_dayNew != null ? ((float)infrastructure.PeakFlow_m3_dayNew).ToString("F1") : "";
                    sb.AppendLine($"PEAKFLOW_M3_DAYNEW\t{PeakFlow_m3_dayNew}\t");
                }
                string PopServed = infrastructure.PopServed != null ? ((int)infrastructure.PopServed).ToString() : "";
                sb.AppendLine($"POPSERVED\t{PopServed}\t");
                if (infrastructure.PopServedNew != null)
                {
                    string PopServedNew = infrastructure.PopServedNew != null ? ((int)infrastructure.PopServedNew).ToString() : "";
                    sb.AppendLine($"POPSERVEDNEW\t{PopServedNew}\t");
                }
                string CanOverflow = infrastructure.CanOverflow != null ? ((bool)infrastructure.CanOverflow) == true ? "true" : "false" : "";
                sb.AppendLine($"CANOVERFLOW\t{CanOverflow}\t");
                if (infrastructure.CanOverflowNew != null)
                {
                    string CanOverflowNew = infrastructure.CanOverflowNew != null ? ((bool)infrastructure.CanOverflowNew) == true ? "true" : "false" : "";
                    sb.AppendLine($"CANOVERFLOWNEW\t{CanOverflowNew}\t");
                }
                string PercFlowOfTotal = infrastructure.PercFlowOfTotal != null ? ((float)infrastructure.PercFlowOfTotal).ToString("F1") : "";
                sb.AppendLine($"PERCFLOWOFTOTAL\t{PercFlowOfTotal}\t");
                if (infrastructure.PercFlowOfTotalNew != null)
                {
                    string PercFlowOfTotalNew = infrastructure.PercFlowOfTotalNew != null ? ((float)infrastructure.PercFlowOfTotalNew).ToString("F1") : "";
                    sb.AppendLine($"PERCFLOWOFTOTALNEW\t{PercFlowOfTotalNew}\t");
                }
                string TimeOffset_hour = infrastructure.TimeOffset_hour != null ? ((float)infrastructure.TimeOffset_hour).ToString("F1") : "";
                sb.AppendLine($"TIMEOFFSET_HOUR\t{TimeOffset_hour}\t");
                if (infrastructure.TimeOffset_hourNew != null)
                {
                    string TimeOffset_hourNew = infrastructure.TimeOffset_hourNew != null ? ((float)infrastructure.TimeOffset_hourNew).ToString("F1") : "";
                    sb.AppendLine($"TIMEOFFSET_HOURNEW\t{TimeOffset_hourNew}\t");
                }
                string TempCatchAllRemoveLater = string.IsNullOrWhiteSpace(infrastructure.TempCatchAllRemoveLater) ? "" : infrastructure.TempCatchAllRemoveLater.Replace("\r\n", "|||");
                sb.AppendLine($"TEMPCATCHALLREMOVELATER\t{TempCatchAllRemoveLater}\t");
                if (!string.IsNullOrWhiteSpace(infrastructure.TempCatchAllRemoveLaterNew))
                {
                    string TempCatchAllRemoveLaterNew = string.IsNullOrWhiteSpace(infrastructure.TempCatchAllRemoveLaterNew) ? "" : infrastructure.TempCatchAllRemoveLaterNew.Replace("\r\n", "|||");
                    sb.AppendLine($"TEMPCATCHALLREMOVELATER\t{TempCatchAllRemoveLaterNew}\t");
                }
                string AverageDepth_m = infrastructure.AverageDepth_m != null ? ((float)infrastructure.AverageDepth_m).ToString("F1") : "";
                sb.AppendLine($"AVERAGEDEPTH_M\t{AverageDepth_m}\t");
                if (infrastructure.AverageDepth_mNew != null)
                {
                    string AverageDepth_mNew = infrastructure.AverageDepth_mNew != null ? ((float)infrastructure.AverageDepth_mNew).ToString("F1") : "";
                    sb.AppendLine($"AVERAGEDEPTH_MNEW\t{AverageDepth_mNew}\t");
                }
                string NumberOfPorts = infrastructure.NumberOfPorts != null ? ((int)infrastructure.NumberOfPorts).ToString() : "";
                sb.AppendLine($"NUMBEROFPORTS\t{NumberOfPorts}\t");
                if (infrastructure.NumberOfPortsNew != null)
                {
                    string NumberOfPortsNew = infrastructure.NumberOfPortsNew != null ? ((int)infrastructure.NumberOfPortsNew).ToString() : "";
                    sb.AppendLine($"NUMBEROFPORTSNEW\t{NumberOfPortsNew}\t");
                }
                string PortDiameter_m = infrastructure.PortDiameter_m != null ? ((float)infrastructure.PortDiameter_m).ToString("F1") : "";
                sb.AppendLine($"PORTDIAMETER_M\t{PortDiameter_m}\t");
                if (infrastructure.PortDiameter_mNew != null)
                {
                    string PortDiameter_mNew = infrastructure.PortDiameter_mNew != null ? ((float)infrastructure.PortDiameter_mNew).ToString("F1") : "";
                    sb.AppendLine($"PORTDIAMETER_MNEW\t{PortDiameter_mNew}\t");
                }
                string PortSpacing_m = infrastructure.PortSpacing_m != null ? ((float)infrastructure.PortSpacing_m).ToString("F1") : "";
                sb.AppendLine($"PORTSPACING_M\t{PortSpacing_m}\t");
                if (infrastructure.PortSpacing_mNew != null)
                {
                    string PortSpacing_mNew = infrastructure.PortSpacing_mNew != null ? ((float)infrastructure.PortSpacing_mNew).ToString("F1") : "";
                    sb.AppendLine($"PORTSPACING_MNEW\t{PortSpacing_mNew}\t");
                }
                string PortElevation_m = infrastructure.PortElevation_m != null ? ((float)infrastructure.PortElevation_m).ToString("F1") : "";
                sb.AppendLine($"PORTELEVATION_M\t{PortElevation_m}\t");
                if (infrastructure.PortElevation_mNew != null)
                {
                    string PortElevation_mNew = infrastructure.PortElevation_mNew != null ? ((float)infrastructure.PortElevation_mNew).ToString("F1") : "";
                    sb.AppendLine($"PORTELEVATION_MNEW\t{PortElevation_mNew}\t");
                }
                string VerticalAngle_deg = infrastructure.VerticalAngle_deg != null ? ((float)infrastructure.VerticalAngle_deg).ToString("F1") : "";
                sb.AppendLine($"VERTICALANGLE_DEG\t{VerticalAngle_deg}\t");
                if (infrastructure.VerticalAngle_degNew != null)
                {
                    string VerticalAngle_degNew = infrastructure.VerticalAngle_degNew != null ? ((float)infrastructure.VerticalAngle_degNew).ToString("F1") : "";
                    sb.AppendLine($"VERTICALANGLE_DEGNEW\t{VerticalAngle_degNew}\t");
                }
                string HorizontalAngle_deg = infrastructure.HorizontalAngle_deg != null ? ((float)infrastructure.HorizontalAngle_deg).ToString("F1") : "";
                sb.AppendLine($"HORIZONTALANGLE_DEG\t{HorizontalAngle_deg}\t");
                if (infrastructure.HorizontalAngle_degNew != null)
                {
                    string HorizontalAngle_degNew = infrastructure.HorizontalAngle_degNew != null ? ((float)infrastructure.HorizontalAngle_degNew).ToString("F1") : "";
                    sb.AppendLine($"HORIZONTALANGLE_DEGNEW\t{HorizontalAngle_degNew}\t");
                }
                string DecayRate_per_day = infrastructure.DecayRate_per_day != null ? ((float)infrastructure.DecayRate_per_day).ToString("F1") : "";
                sb.AppendLine($"DECAYRATE_PER_DAY\t{DecayRate_per_day}\t");
                if (infrastructure.DecayRate_per_dayNew != null)
                {
                    string DecayRate_per_dayNew = infrastructure.DecayRate_per_dayNew != null ? ((float)infrastructure.DecayRate_per_dayNew).ToString("F1") : "";
                    sb.AppendLine($"DECAYRATE_PER_DAYNEW\t{DecayRate_per_dayNew}\t");
                }
                string NearFieldVelocity_m_s = infrastructure.NearFieldVelocity_m_s != null ? ((float)infrastructure.NearFieldVelocity_m_s).ToString("F1") : "";
                sb.AppendLine($"NEARFIELDVELOCITY_M_S\t{NearFieldVelocity_m_s}\t");
                if (infrastructure.NearFieldVelocity_m_sNew != null)
                {
                    string NearFieldVelocity_m_sNew = infrastructure.NearFieldVelocity_m_sNew != null ? ((float)infrastructure.NearFieldVelocity_m_sNew).ToString("F1") : "";
                    sb.AppendLine($"NEARFIELDVELOCITY_M_SNEW\t{NearFieldVelocity_m_sNew}\t");
                }
                string FarFieldVelocity_m_s = infrastructure.FarFieldVelocity_m_s != null ? ((float)infrastructure.FarFieldVelocity_m_s).ToString("F1") : "";
                sb.AppendLine($"FARFIELDVELOCITY_M_S\t{FarFieldVelocity_m_s}\t");
                if (infrastructure.FarFieldVelocity_m_sNew != null)
                {
                    string FarFieldVelocity_m_sNew = infrastructure.FarFieldVelocity_m_sNew != null ? ((float)infrastructure.FarFieldVelocity_m_sNew).ToString("F1") : "";
                    sb.AppendLine($"FARFIELDVELOCITY_M_SNEW\t{FarFieldVelocity_m_sNew}\t");
                }
                string ReceivingWaterSalinity_PSU = infrastructure.ReceivingWaterSalinity_PSU != null ? ((float)infrastructure.ReceivingWaterSalinity_PSU).ToString("F1") : "";
                sb.AppendLine($"RECEIVINGWATERSALINITY_PSU\t{ReceivingWaterSalinity_PSU}\t");
                if (infrastructure.ReceivingWaterSalinity_PSUNew != null)
                {
                    string ReceivingWaterSalinity_PSUNew = infrastructure.ReceivingWaterSalinity_PSUNew != null ? ((float)infrastructure.ReceivingWaterSalinity_PSUNew).ToString("F1") : "";
                    sb.AppendLine($"RECEIVINGWATERSALINITY_PSUNEW\t{ReceivingWaterSalinity_PSUNew}\t");
                }
                string ReceivingWaterTemperature_C = infrastructure.ReceivingWaterTemperature_C != null ? ((float)infrastructure.ReceivingWaterTemperature_C).ToString("F1") : "";
                sb.AppendLine($"RECEIVINGWATERTEMPERATURE_C\t{ReceivingWaterTemperature_C}\t");
                if (infrastructure.ReceivingWaterTemperature_CNew != null)
                {
                    string ReceivingWaterTemperature_CNew = infrastructure.ReceivingWaterTemperature_CNew != null ? ((float)infrastructure.ReceivingWaterTemperature_CNew).ToString("F1") : "";
                    sb.AppendLine($"RECEIVINGWATERTEMPERATURE_CNEW\t{ReceivingWaterTemperature_CNew}\t");
                }
                string ReceivingWater_MPN_per_100ml = infrastructure.ReceivingWater_MPN_per_100ml != null ? ((int)infrastructure.ReceivingWater_MPN_per_100ml).ToString() : "";
                sb.AppendLine($"RECEIVINGWATER_MPN_PER_100ML\t{ReceivingWater_MPN_per_100ml}\t");
                if (infrastructure.ReceivingWater_MPN_per_100mlNew != null)
                {
                    string ReceivingWater_MPN_per_100mlNew = infrastructure.ReceivingWater_MPN_per_100mlNew != null ? ((float)infrastructure.ReceivingWater_MPN_per_100mlNew).ToString() : "";
                    sb.AppendLine($"RECEIVINGWATER_MPN_PER_100MLNEW\t{ReceivingWater_MPN_per_100mlNew}\t");
                }
                string DistanceFromShore_m = infrastructure.DistanceFromShore_m != null ? ((int)infrastructure.DistanceFromShore_m).ToString("F1") : "";
                sb.AppendLine($"DISTANCEFROMSHORE_M\t{DistanceFromShore_m}\t");
                if (infrastructure.DistanceFromShore_mNew != null)
                {
                    string DistanceFromShore_mNew = infrastructure.DistanceFromShore_mNew != null ? ((float)infrastructure.DistanceFromShore_mNew).ToString("F1") : "";
                    sb.AppendLine($"DISTANCEFROMSHORE_MNEW\t{DistanceFromShore_mNew}\t");
                }
                string SeeOtherMunicipalityTVItemID = infrastructure.SeeOtherMunicipalityTVItemID != null ? ((int)infrastructure.SeeOtherMunicipalityTVItemID).ToString() : "";
                sb.AppendLine($"SEEOTHERMUNICIPALITYTVITEMID\t{SeeOtherMunicipalityTVItemID}\t");
                if (infrastructure.SeeOtherMunicipalityTVItemIDNew != null)
                {
                    string SeeOtherMunicipalityTVItemIDNew = infrastructure.SeeOtherMunicipalityTVItemIDNew != null ? ((float)infrastructure.SeeOtherMunicipalityTVItemIDNew).ToString() : "";
                    sb.AppendLine($"SEEOTHERMUNICIPALITYTVITEMIDNEW\t{SeeOtherMunicipalityTVItemIDNew}\t");
                }
                string PumpsToTVItemID = infrastructure.PumpsToTVItemID != null ? ((int)infrastructure.PumpsToTVItemID).ToString() : "";
                sb.AppendLine($"PUMPSTOTVITEMID\t{PumpsToTVItemID}\t");
                if (infrastructure.PumpsToTVItemIDNew != null)
                {
                    string PumpsToTVItemIDNew = infrastructure.PumpsToTVItemIDNew != null ? ((double)infrastructure.PumpsToTVItemIDNew).ToString("F0") : "";
                    sb.AppendLine($"PUMPSTOTVITEMIDNEW\t{PumpsToTVItemIDNew}\t");
                }
                string PathCoordList = "";
                foreach (Coord coord in infrastructure.PathCoordList)
                {
                    PathCoordList = PathCoordList + ((float)coord.Lat).ToString("F5") + "|" + ((float)coord.Lng).ToString("F5") + " ";
                }
                sb.AppendLine($"PATHCOORDLIST\t{PathCoordList}\t");
                if (infrastructure.PathCoordListNew.Count > 0)
                {
                    string PathCoordListNew = "";
                    foreach (Coord coord in infrastructure.PathCoordListNew)
                    {
                        PathCoordListNew = PathCoordListNew + ((float)coord.Lat).ToString("F5") + "|" + ((float)coord.Lng).ToString("F5") + " ";
                    }
                    sb.AppendLine($"PATHCOORDLISTNEW\t{PathCoordListNew}\t");
                }

                if (infrastructure.InfrastructureAddress != null)
                {
                    if (infrastructure.InfrastructureAddress.AddressTVItemID != null)
                    {
                        string AddressTVItemID = infrastructure.InfrastructureAddress.AddressTVItemID == null ? "-999999999" : infrastructure.InfrastructureAddress.AddressTVItemID.ToString();
                        string Municipality = infrastructure.InfrastructureAddress.Municipality == null ? "" : infrastructure.InfrastructureAddress.Municipality;
                        string AddressType = infrastructure.InfrastructureAddress.AddressType == null ? "" : ((int)infrastructure.InfrastructureAddress.AddressType).ToString();
                        string StreetNumber = infrastructure.InfrastructureAddress.StreetNumber == null ? "" : infrastructure.InfrastructureAddress.StreetNumber;
                        string StreetName = infrastructure.InfrastructureAddress.StreetName == null ? "" : infrastructure.InfrastructureAddress.StreetName;
                        string StreetType = infrastructure.InfrastructureAddress.StreetType == null ? "" : ((int)infrastructure.InfrastructureAddress.StreetType).ToString();
                        string PostalCode = infrastructure.InfrastructureAddress.PostalCode == null ? "" : infrastructure.InfrastructureAddress.PostalCode;

                        sb.AppendLine($"ADDRESS\t{infrastructure.InfrastructureAddress.AddressTVItemID}\t{infrastructure.InfrastructureAddress.Municipality}\t{((int)infrastructure.InfrastructureAddress.AddressType).ToString()}\t{infrastructure.InfrastructureAddress.StreetNumber}\t{infrastructure.InfrastructureAddress.StreetName}\t{((int)infrastructure.InfrastructureAddress.StreetType).ToString()}\t{infrastructure.InfrastructureAddress.PostalCode}\t");
                    }
                }

                if (infrastructure.InfrastructureAddressNew.AddressTVItemID != null)
                {
                    string AddressTVItemID = infrastructure.InfrastructureAddressNew.AddressTVItemID.ToString();
                    string Municipality = infrastructure.InfrastructureAddressNew.Municipality == null ? "" : infrastructure.InfrastructureAddressNew.Municipality;
                    string AddressType = infrastructure.InfrastructureAddressNew.AddressType == null ? "" : ((int)infrastructure.InfrastructureAddressNew.AddressType).ToString();
                    string StreetNumber = infrastructure.InfrastructureAddressNew.StreetNumber == null ? "" : infrastructure.InfrastructureAddressNew.StreetNumber;
                    string StreetName = infrastructure.InfrastructureAddressNew.StreetName == null ? "" : infrastructure.InfrastructureAddressNew.StreetName;
                    string StreetType = infrastructure.InfrastructureAddressNew.StreetType == null ? "" : ((int)infrastructure.InfrastructureAddressNew.StreetType).ToString();
                    string PostalCode = infrastructure.InfrastructureAddressNew.PostalCode == null ? "" : infrastructure.InfrastructureAddressNew.PostalCode;
                    sb.AppendLine($"ADDRESSNEW\t{AddressTVItemID}\t{Municipality}\t{AddressType}\t{StreetNumber}\t{StreetName}\t{StreetType}\t{PostalCode}\t");
                }

                foreach (Picture picture in infrastructure.InfrastructurePictureList)
                {
                    sb.AppendLine($"PICTURE\t{picture.PictureTVItemID}\t{picture.FileName.Replace("\r", "_").Replace("\n", "_").Replace("\t", "_")}\t{picture.Extension}\t{picture.Description.Replace("\r", "_").Replace("\n", "_").Replace("\t", "_")}\t");
                    if (picture.ToRemove != null && picture.ToRemove == true)
                    {
                        sb.AppendLine($"PICTURETOREMOVE\t");
                    }
                    if (!string.IsNullOrWhiteSpace(picture.FileNameNew))
                    {
                        sb.AppendLine($"PICTUREFILENAMENEW\t{picture.FileNameNew}\t");
                    }
                    if (!string.IsNullOrWhiteSpace(picture.ExtensionNew))
                    {
                        sb.AppendLine($"PICTUREEXTENSIONNEW\t{picture.ExtensionNew}\t");
                    }
                    if (!string.IsNullOrWhiteSpace(picture.DescriptionNew))
                    {
                        sb.AppendLine($"PICTUREDESCRIPTIONNEW\t{picture.DescriptionNew.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("  ", " ")}\t");
                    }
                }

            }

            DirectoryInfo di = new DirectoryInfo($@"C:\PollutionSourceSites\Infrastructures\{CurrentMunicipalityName}\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception)
                {
                    EmitStatus(new StatusEventArgs("Could not create directory [" + di.FullName + "]"));
                }
            }

            FileInfo fi = new FileInfo($@"C:\PollutionSourceSites\Infrastructures\{CurrentMunicipalityName}\{CurrentMunicipalityName}.txt");

            StreamWriter sw = fi.CreateText();
            sw.Write(sb.ToString());
            sw.Close();
        }
        public void SaveSubsectorTextFile()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            sb.AppendLine($"VERSION\t{subsectorDoc.Version}\t");
            sb.AppendLine($"DOCDATE\t{((DateTime)subsectorDoc.DocDate).Year}|{((DateTime)subsectorDoc.DocDate).Month.ToString("0#")}|{((DateTime)subsectorDoc.DocDate).Day.ToString("0#")}|{((DateTime)subsectorDoc.DocDate).Hour.ToString("0#")}|{((DateTime)subsectorDoc.DocDate).Minute.ToString("0#")}|{((DateTime)subsectorDoc.DocDate).Second.ToString("0#")}\t");

            List<MunicipalityIDNumber> municipalityIDNumberList = subsectorDoc.MunicipalityIDNumberList;

            string MunicipalityListText = "";
            foreach (MunicipalityIDNumber municipalityIDNumber in municipalityIDNumberList)
            {
                MunicipalityListText = MunicipalityListText + municipalityIDNumber.Municipality.Replace("\t", "_").Replace("|", "_").Replace("[", "_").Replace("]", "_") + "[" + municipalityIDNumber.IDNumber + "]" + "|";
            }

            sb.AppendLine($"PROVINCETVITEMID\t{ subsectorDoc.ProvinceTVItemID }");
            sb.AppendLine($"PROVINCEMUNICIPALITIES\t{ MunicipalityListText }");


            sb.AppendLine($"SUBSECTOR\t{subsectorDoc.Subsector.SubsectorTVItemID}\t{subsectorDoc.Subsector.SubsectorName}\t");
            foreach (PSS pss in subsectorDoc.Subsector.PSSList)
            {
                sb.AppendLine($"-----\t-------------------------------------------------\t");
                string LatText = pss.Lat == null ? "0.0" : ((float)pss.Lat).ToString("F5");
                string LngText = pss.Lng == null ? "0.0" : ((float)pss.Lng).ToString("F5");
                string IsActiveText = pss.IsActive == null ? "false" : (((bool)pss.IsActive) ? "true" : "false");
                string IsPointSourceText = pss.IsPointSource == null ? "false" : (((bool)pss.IsPointSource) ? "true" : "false");
                sb.AppendLine($"PSS\t{((double)pss.PSSTVItemID).ToString("F0")}\t{LatText}\t{LngText}\t{IsActiveText}\t{IsPointSourceText}\t");
                sb.AppendLine($"SITENUMB\t{pss.SiteNumber}\t");
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
                sb.AppendLine($"TVTEXT\t{pss.TVText}\t");
                if (!string.IsNullOrWhiteSpace(pss.TVTextNew))
                {
                    sb.AppendLine($"TVTEXTNEW\t{pss.TVTextNew}\t");
                }

                if (pss.PSSAddress != null)
                {
                    if (pss.PSSAddress.AddressTVItemID != null)
                    {
                        string AddressTVItemID = pss.PSSAddress.AddressTVItemID == null ? "-999999999" : ((double)pss.PSSAddress.AddressTVItemID).ToString("F0");
                        string Municipality = pss.PSSAddress.Municipality == null ? "" : pss.PSSAddress.Municipality;
                        string AddressType = pss.PSSAddress.AddressType == null ? "" : ((int)pss.PSSAddress.AddressType).ToString();
                        string StreetNumber = pss.PSSAddress.StreetNumber == null ? "" : pss.PSSAddress.StreetNumber;
                        string StreetName = pss.PSSAddress.StreetName == null ? "" : pss.PSSAddress.StreetName;
                        string StreetType = pss.PSSAddress.StreetType == null ? "" : ((int)pss.PSSAddress.StreetType).ToString();
                        string PostalCode = pss.PSSAddress.PostalCode == null ? "" : pss.PSSAddress.PostalCode;

                        sb.AppendLine($"ADDRESS\t{AddressTVItemID}\t{Municipality}\t{AddressType}\t{StreetNumber}\t{StreetName}\t{StreetType}\t{PostalCode}\t");
                    }
                }

                if (pss.PSSAddressNew.AddressTVItemID != null)
                {
                    string AddressTVItemID = ((double)pss.PSSAddressNew.AddressTVItemID).ToString("F0");
                    string Municipality = pss.PSSAddressNew.Municipality == null ? "" : pss.PSSAddressNew.Municipality;
                    string AddressType = pss.PSSAddressNew.AddressType == null ? "" : ((int)pss.PSSAddressNew.AddressType).ToString();
                    string StreetNumber = pss.PSSAddressNew.StreetNumber == null ? "" : pss.PSSAddressNew.StreetNumber;
                    string StreetName = pss.PSSAddressNew.StreetName == null ? "" : pss.PSSAddressNew.StreetName;
                    string StreetType = pss.PSSAddressNew.StreetType == null ? "" : ((int)pss.PSSAddressNew.StreetType).ToString();
                    string PostalCode = pss.PSSAddressNew.PostalCode == null ? "" : pss.PSSAddressNew.PostalCode;
                    sb.AppendLine($"ADDRESSNEW\t{AddressTVItemID}\t{Municipality}\t{AddressType}\t{StreetNumber}\t{StreetName}\t{StreetType}\t{PostalCode}\t");
                }

                foreach (Picture picture in pss.PSSPictureList)
                {
                    sb.AppendLine($"PICTURE\t{((double)picture.PictureTVItemID).ToString("F0")}\t{picture.FileName.Replace("\r", "_").Replace("\n", "_").Replace("\t", "_")}\t{picture.Extension}\t{picture.Description.Replace("\r", "_").Replace("\n", "_").Replace("\t", "_")}\t");
                    if (picture.ToRemove != null && picture.ToRemove == true)
                    {
                        sb.AppendLine($"PICTURETOREMOVE\t");
                    }
                    if (!string.IsNullOrWhiteSpace(picture.FileNameNew))
                    {
                        sb.AppendLine($"PICTUREFILENAMENEW\t{picture.FileNameNew}\t");
                    }
                    if (!string.IsNullOrWhiteSpace(picture.ExtensionNew))
                    {
                        sb.AppendLine($"PICTUREEXTENSIONNEW\t{picture.ExtensionNew}\t");
                    }
                    if (!string.IsNullOrWhiteSpace(picture.DescriptionNew))
                    {
                        sb.AppendLine($"PICTUREDESCRIPTIONNEW\t{picture.DescriptionNew.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ").Replace("  ", " ")}\t");
                    }
                }

                sb.AppendLine($"OBS\t{((double)pss.PSSObs.ObsID).ToString("F0")}\t" +
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

                int count = 0;
                foreach (Issue issue in pss.PSSObs.IssueList.OrderBy(c => c.Ordinal))
                {
                    issue.Ordinal = count;
                    count += 1;
                }

                foreach (Issue issue in pss.PSSObs.IssueList.OrderBy(c => c.Ordinal))
                {
                    sb.AppendLine($"ISSUE\t{((double)issue.IssueID).ToString("F0")}\t{issue.Ordinal}\t{((DateTime)issue.LastUpdated_UTC).Year}|{((DateTime)issue.LastUpdated_UTC).Month.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Day.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Hour.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Minute.ToString("0#")}|{((DateTime)issue.LastUpdated_UTC).Second.ToString("0#")}\t{String.Join(",", issue.PolSourceObsInfoIntList)},\t");
                    if (issue.PolSourceObsInfoIntListNew.Count > 0)
                    {
                        sb.AppendLine($"ISSUENEW\t{String.Join(",", issue.PolSourceObsInfoIntListNew)},\t");
                    }
                    if (issue.ToRemove != null && issue.ToRemove == true)
                    {
                        sb.AppendLine($"ISSUETOREMOVE\t");
                    }
                    if (issue.ExtraComment != null)
                    {
                        sb.AppendLine($"EXTRACOMMENT\t{issue.ExtraComment.Replace("\r\n", "|||").Replace("\t", "    ")}\t");
                    }
                    if (issue.ExtraCommentNew != null)
                    {
                        sb.AppendLine($"EXTRACOMMENTNEW\t{issue.ExtraCommentNew.Replace("\r\n", "|||").Replace("\t", "    ")}\t");
                    }
                }
            }

            DirectoryInfo di = new DirectoryInfo($@"C:\PollutionSourceSites\Subsectors\{CurrentSubsectorName}\");

            if (!di.Exists)
            {
                try
                {
                    di.Create();
                }
                catch (Exception)
                {
                    EmitStatus(new StatusEventArgs("Could not create directory [" + di.FullName + "]"));
                }
            }

            FileInfo fi = new FileInfo($@"C:\PollutionSourceSites\Subsectors\{CurrentSubsectorName}\{CurrentSubsectorName}.txt");

            StreamWriter sw = fi.CreateText();
            sw.Write(sb.ToString());
            sw.Close();
        }

        public void Fix()
        {
            foreach (PSS pss in subsectorDoc.Subsector.PSSList)
            {
                foreach (Issue issue in pss.PSSObs.IssueList.OrderBy(c => c.Ordinal))
                {
                    if (issue.PolSourceObsInfoIntListNew.Count > 0)
                    {
                        List<int> TempPolSourceObsInfoIntList = new List<int>();
                        foreach (int i in issue.PolSourceObsInfoIntListNew)
                        {
                            TempPolSourceObsInfoIntList.Add(i);
                        }

                        List<int> NewTempPolSourceObsInfoIntList = new List<int>();
                        DoFix(TempPolSourceObsInfoIntList, NewTempPolSourceObsInfoIntList, new List<string>() { "101" });

                        issue.PolSourceObsInfoIntListNew = new List<int>();

                        foreach (int NewTempPolSourceObsInfoInt in NewTempPolSourceObsInfoIntList)
                        {
                            issue.PolSourceObsInfoIntListNew.Add(NewTempPolSourceObsInfoInt);
                        }
                    }
                }
            }

            SaveSubsectorTextFile();
        }

        private void DoFix(List<int> TempPolSourceObsInfoIntList, List<int> NewTempPolSourceObsInfoIntList, List<string> ChildList)
        {
            List<PolSourceObsInfoChild> PolSourceObsInfoChildList = new List<PolSourceObsInfoChild>();
            foreach (string ChildStart3Char in ChildList)
            {
                bool ShouldExistForEach = false;
                for (int i = 0, count = TempPolSourceObsInfoIntList.Count; i < count; i++)
                {
                    string obsEnum3Char = TempPolSourceObsInfoIntList[i].ToString().Substring(0, 3);

                    if (obsEnum3Char == ChildStart3Char)
                    {
                        NewTempPolSourceObsInfoIntList.Add(TempPolSourceObsInfoIntList[i]);
                        PolSourceObsInfoChildList = polSourceObsInfoChildList.Where(c => c.PolSourceObsInfo == ((PolSourceObsInfoEnum)TempPolSourceObsInfoIntList[i])).ToList();

                        TempPolSourceObsInfoIntList.RemoveAt(i);
                        ShouldExistForEach = true;
                        break;
                    }
                }

                if (ShouldExistForEach)
                {
                    break;
                }
            }

            List<string> ChildList2 = new List<string>();
            if (PolSourceObsInfoChildList.Count == 0)
            {
                if (TempPolSourceObsInfoIntList.Count > 0)
                {
                    ChildList2.Add(((int)TempPolSourceObsInfoIntList[0]).ToString().Substring(0, 3));
                }
            }
            else
            {
                foreach (PolSourceObsInfoChild polSourceObsInfoChild in PolSourceObsInfoChildList)
                {
                    ChildList2.Add(((int)polSourceObsInfoChild.PolSourceObsInfoChildStart).ToString().Substring(0, 3));
                }
            }

            if (ChildList2.Count > 0)
            {
                DoFix(TempPolSourceObsInfoIntList, NewTempPolSourceObsInfoIntList, ChildList2);
            }
        }
    }
}
