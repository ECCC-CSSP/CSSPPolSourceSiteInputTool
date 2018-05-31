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
                    EmitStatus(new StatusEventArgs($"Error: CurrentSubsectorName is empty"));
                    return;
                }

                EmitStatus(new StatusEventArgs($"Opening file [{fi.FullName}] with Google Earth"));

                FileInfo fiGE = new FileInfo(@"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe");

                if (fiGE.Exists)
                {
                    Process.Start(@"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe", fi.FullName);
                }
                else
                {
                    Process.Start(@"IExplore.exe", fi.FullName);
                }
                EmitStatus(new StatusEventArgs($""));
            }
            else
            {
                FileInfo fi = new FileInfo($@"{BasePathInfrastructures}\{CurrentMunicipalityName}\{CurrentMunicipalityName}.kml");

                if (string.IsNullOrWhiteSpace(CurrentMunicipalityName))
                {
                    EmitStatus(new StatusEventArgs($"Error: CurrentMunicipalityName is empty"));
                    return;
                }

                EmitStatus(new StatusEventArgs($"Opening file [{fi.FullName}] with Google Earth"));

                FileInfo fiGE = new FileInfo(@"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe");

                if (fiGE.Exists)
                {
                    Process.Start(@"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe", fi.FullName);
                }
                else
                {
                    Process.Start(@"IExplore.exe", fi.FullName);
                }
                EmitStatus(new StatusEventArgs($""));
            }
        }
        public void RegenerateMunicipalityKMLFile()
        {

            EmitStatus(new StatusEventArgs($@"Regenerating municipality KML file for subsector [{CurrentMunicipalityName}]"));

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
                    EmitStatus(new StatusEventArgs(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n"));
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

            foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList.OrderBy(c => c.TVText))
            {
                sbKML.AppendLine($@"		<Placemark>");
                string InfrastructureNameText = string.IsNullOrWhiteSpace(infrastructure.TVTextNew) ? infrastructure.TVText : infrastructure.TVTextNew;
                sbKML.AppendLine($@"			<name>{InfrastructureNameText}</name>");
                sbKML.AppendLine($@"            <description><![CDATA[");
                sbKML.AppendLine($@"            {InfrastructureNameText} --- ({infrastructure.InfrastructureTVItemID.ToString()})<br />");
                if (!string.IsNullOrWhiteSpace(infrastructure.TVTextNew))
                {
                    sbKML.AppendLine($@"            Old Name: {infrastructure.TVTextNew}<br />");
                }

                if (infrastructure.InfrastructureAddress != null)
                {
                    string StreetNumber = infrastructure.InfrastructureAddress.StreetNumber == null ? "" : infrastructure.InfrastructureAddress.StreetNumber + " ";
                    string StreetName = infrastructure.InfrastructureAddress.StreetName == null ? "" : infrastructure.InfrastructureAddress.StreetName + " ";
                    string StreetType = infrastructure.InfrastructureAddress.StreetType == null ? "" : _BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)infrastructure.InfrastructureAddress.StreetType) + ", ";
                    string Municipality = infrastructure.InfrastructureAddress.Municipality == null ? "" : infrastructure.InfrastructureAddress.Municipality + ", ";
                    string PostalCode = infrastructure.InfrastructureAddress.PostalCode == null ? "" : infrastructure.InfrastructureAddress.PostalCode;

                    string Address = $"{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}".Trim();
                    if (string.IsNullOrWhiteSpace(Address))
                    {
                        sbKML.AppendLine($@"                <p>Address: ---</p>");
                    }
                    else
                    {
                        sbKML.AppendLine($@"                <p>Address: {StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}</p>");
                    }
                }
                if (infrastructure.InfrastructureAddressNew != null)
                {
                    string StreetNumber = infrastructure.InfrastructureAddressNew.StreetNumber == null ? "" : infrastructure.InfrastructureAddressNew.StreetNumber + " ";
                    string StreetName = infrastructure.InfrastructureAddressNew.StreetName == null ? "" : infrastructure.InfrastructureAddressNew.StreetName + " ";
                    string StreetType = infrastructure.InfrastructureAddressNew.StreetType == null ? "" : _BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)infrastructure.InfrastructureAddressNew.StreetType) + ", ";
                    string Municipality = infrastructure.InfrastructureAddressNew.Municipality == null ? "" : infrastructure.InfrastructureAddressNew.Municipality + ", ";
                    string PostalCode = infrastructure.InfrastructureAddressNew.PostalCode == null ? "" : infrastructure.InfrastructureAddressNew.PostalCode;

                    string Address = $"{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}".Trim();
                    if (string.IsNullOrWhiteSpace(Address))
                    {
                        sbKML.AppendLine($@"                <p>New Address: ---</p>");
                    }
                    else
                    {
                        sbKML.AppendLine($@"                <p>New Address: {StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}</p>");
                    }
                }

                sbKML.AppendLine($@"                <h3>Details</h3>");
                sbKML.AppendLine($@"                <blockquote>");
                
                // doing IsActive
                string IsActive = infrastructure.IsActive != null && infrastructure.IsActive == true ? "true" : "false";
                sbKML.Append($@"                <p><b>IsActive:</b> {IsActive}</p>");
                if (infrastructure.IsActiveNew != null)
                {
                    string IsActiveNew = infrastructure.IsActiveNew == true ? "true" : "false";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>Is Active New:</b> {IsActiveNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing LastUpdateDate_UTC
                string LastUpdateDate_UTC = infrastructure.LastUpdateDate_UTC != null ? ((DateTime)infrastructure.LastUpdateDate_UTC).ToString("yyyy MMMM dd") : "---";
                sbKML.AppendLine($@"                <p><b>Last Update Date:</b> {LastUpdateDate_UTC}</p>");

                // doing Comment
                sbKML.AppendLine($@"                <p><b>Comment:</b>{infrastructure.Comment.Replace("\r\n", "<br />")}</p>");
                if (!string.IsNullOrWhiteSpace(infrastructure.CommentNew))
                {
                    sbKML.AppendLine($@"                <p><b>Comment New:</b>{infrastructure.CommentNew.Replace("\r\n", "<br />")}</p>");
                }

                // doing Lat and Lng
                string LatText = infrastructure.Lat != null ? ((float)infrastructure.Lat).ToString("F5") : "---";
                string LngText = infrastructure.Lng != null ? ((float)infrastructure.Lng).ToString("F5") : "---";
                sbKML.AppendLine($@"                <p><b>Lat:</b> {LatText} <b>Lng:</b> {LngText}</p>");
                if (infrastructure.LatNew != null || infrastructure.LngNew != null)
                {
                    string LatNewText = infrastructure.LatNew != null ? ((float)infrastructure.LatNew).ToString("F5") : "---";
                    string LngNewText = infrastructure.LngNew != null ? ((float)infrastructure.LngNew).ToString("F5") : "---";
                    sbKML.AppendLine($@"                <p><b>Lat New:</b> {LatNewText} <b>Lng New:</b> {LngNewText}</p>");
                }
                // doing Outfall Lat and Outfall Lng
                string LatOutfallText = infrastructure.LatOutfall != null ? ((float)infrastructure.LatOutfall).ToString("F5") : "---";
                string LngOutfallText = infrastructure.LngOutfall != null ? ((float)infrastructure.LngOutfall).ToString("F5") : "---";
                sbKML.AppendLine($@"                <p><b>Lat Outfall:</b> {LatOutfallText} <b>Lng Outfall:</b> {LngOutfallText}</p>");
                if (infrastructure.LatOutfallNew != null || infrastructure.LngOutfallNew != null)
                {
                    string LatOutfallNewText = infrastructure.LatOutfallNew != null ? ((float)infrastructure.LatOutfallNew).ToString("F5") : "---";
                    string LngOutfallNewText = infrastructure.LngOutfallNew != null ? ((float)infrastructure.LngOutfallNew).ToString("F5") : "---";
                    sbKML.AppendLine($@"                <p><b>Lat Outfall New:</b> {LatOutfallNewText} <b>Lng Outfall New:</b> {LngOutfallNewText}</p>");
                }

                // doing Prism
                string PrismText = infrastructure.PrismID != null ? infrastructure.PrismID.ToString() : "---";
                sbKML.Append($@"                <p><b>Prism:</b> {PrismText}");
                if (infrastructure.PrismIDNew != null)
                {
                    string PrismNewText = infrastructure.PrismIDNew != null ? infrastructure.PrismIDNew.ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Prism New:</b> {PrismNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing TPID
                string TPIDText = infrastructure.TPID != null ? infrastructure.TPID.ToString() : "---";
                sbKML.Append($@"                <p><b>TPID:</b> {TPIDText}");
                if (infrastructure.TPIDNew != null)
                {
                    string TPIDNewText = infrastructure.TPIDNew != null ? infrastructure.TPIDNew.ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>TPID New:</b> {TPIDNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing LSID
                string LSIDText = infrastructure.LSID != null ? infrastructure.LSID.ToString() : "---";
                sbKML.Append($@"                <p><b>LSID:</b> {LSIDText}");
                if (infrastructure.LSIDNew != null)
                {
                    string LSIDNewText = infrastructure.LSIDNew != null ? infrastructure.LSIDNew.ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>LSID New:</b> {LSIDNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing SiteID
                string SiteIDText = infrastructure.SiteID != null ? infrastructure.SiteID.ToString() : "---";
                sbKML.Append($@"                <p><b>SiteID:</b> {SiteIDText}");
                if (infrastructure.SiteIDNew != null)
                {
                    string SiteIDNewText = infrastructure.SiteIDNew != null ? infrastructure.SiteIDNew.ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>SiteID New:</b> {SiteIDNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing Site
                string SiteText = infrastructure.Site != null ? infrastructure.Site.ToString() : "---";
                sbKML.Append($@"                <p><b>Site:</b> {SiteText}");
                if (infrastructure.SiteNew != null)
                {
                    string SiteNewText = infrastructure.SiteNew != null ? infrastructure.SiteNew.ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Site New:</b> {SiteNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing InfrastructureCategory
                string InfrastructureCategoryText = infrastructure.InfrastructureCategory != null ? infrastructure.InfrastructureCategory.ToString() : "---";
                sbKML.Append($@"                <p><b>InfrastructureCategory:</b> {InfrastructureCategoryText}");
                if (infrastructure.InfrastructureCategoryNew != null)
                {
                    string InfrastructureCategoryNewText = infrastructure.InfrastructureCategoryNew != null ? infrastructure.InfrastructureCategoryNew.ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>InfrastructureCategory New:</b> {InfrastructureCategoryNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing InfrastructureType
                string InfrastructureTypeText = infrastructure.InfrastructureType != null ? _BaseEnumService.GetEnumText_InfrastructureTypeEnum((InfrastructureTypeEnum)infrastructure.InfrastructureType) : "---";
                sbKML.Append($@"                <p><b>InfrastructureType:</b> {InfrastructureTypeText}");
                if (infrastructure.InfrastructureTypeNew != null)
                {
                    string InfrastructureTypeNewText = infrastructure.InfrastructureTypeNew != null ? _BaseEnumService.GetEnumText_InfrastructureTypeEnum((InfrastructureTypeEnum)infrastructure.InfrastructureTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>InfrastructureType New:</b> {InfrastructureTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing FacilityType
                string FacilityTypeText = infrastructure.FacilityType != null ? _BaseEnumService.GetEnumText_FacilityTypeEnum((FacilityTypeEnum)infrastructure.FacilityType) : "---";
                sbKML.Append($@"                <p><b>FacilityType:</b> {FacilityTypeText}");
                if (infrastructure.FacilityTypeNew != null)
                {
                    string FacilityTypeNewText = infrastructure.FacilityTypeNew != null ? _BaseEnumService.GetEnumText_FacilityTypeEnum((FacilityTypeEnum)infrastructure.FacilityTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>FacilityType New:</b> {FacilityTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing IsMechanicallyAerated
                string IsMechanicallyAerated = infrastructure.IsMechanicallyAerated != null && infrastructure.IsMechanicallyAerated == true ? "true" : "false";
                sbKML.Append($@"                <p><b>IsMechanicallyAerated:</b> {IsMechanicallyAerated}</p>");
                if (infrastructure.IsMechanicallyAeratedNew != null)
                {
                    string IsMechanicallyAeratedNew = infrastructure.IsMechanicallyAeratedNew == true ? "true" : "false";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>Is Active New:</b> {IsMechanicallyAeratedNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing NumberOfCells
                string NumberOfCells = infrastructure.NumberOfCells != null ? infrastructure.NumberOfCells.ToString() : "---";
                sbKML.Append($@"                <p><b>NumberOfCells:</b> {NumberOfCells}</p>");
                if (infrastructure.NumberOfCellsNew != null)
                {
                    string NumberOfCellsNew = infrastructure.NumberOfCellsNew != null ? infrastructure.NumberOfCellsNew.ToString() : "---";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>NumberOfCells New:</b> {NumberOfCellsNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing NumberOfAeratedCells
                string NumberOfAeratedCells = infrastructure.NumberOfAeratedCells != null ? infrastructure.NumberOfAeratedCells.ToString() : "---";
                sbKML.Append($@"                <p><b>NumberOfAeratedCells:</b> {NumberOfAeratedCells}</p>");
                if (infrastructure.NumberOfAeratedCellsNew != null)
                {
                    string NumberOfAeratedCellsNew = infrastructure.NumberOfAeratedCellsNew != null ? infrastructure.NumberOfAeratedCellsNew.ToString() : "---";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>NumberOfAeratedCells New:</b> {NumberOfAeratedCellsNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing AerationType
                string AerationTypeText = infrastructure.AerationType != null ? _BaseEnumService.GetEnumText_AerationTypeEnum((AerationTypeEnum)infrastructure.AerationType) : "---";
                sbKML.Append($@"                <p><b>AerationType:</b> {AerationTypeText}");
                if (infrastructure.AerationTypeNew != null)
                {
                    string AerationTypeNewText = infrastructure.AerationTypeNew != null ? _BaseEnumService.GetEnumText_AerationTypeEnum((AerationTypeEnum)infrastructure.AerationTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>AerationType New:</b> {AerationTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PreliminaryTreatmentType
                string PreliminaryTreatmentTypeText = infrastructure.PreliminaryTreatmentType != null ? _BaseEnumService.GetEnumText_PreliminaryTreatmentTypeEnum((PreliminaryTreatmentTypeEnum)infrastructure.PreliminaryTreatmentType) : "---";
                sbKML.Append($@"                <p><b>PreliminaryTreatmentType:</b> {PreliminaryTreatmentTypeText}");
                if (infrastructure.PreliminaryTreatmentTypeNew != null)
                {
                    string PreliminaryTreatmentTypeNewText = infrastructure.PreliminaryTreatmentTypeNew != null ? _BaseEnumService.GetEnumText_PreliminaryTreatmentTypeEnum((PreliminaryTreatmentTypeEnum)infrastructure.PreliminaryTreatmentTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>PreliminaryTreatmentType New:</b> {PreliminaryTreatmentTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PrimaryTreatmentType
                string PrimaryTreatmentTypeText = infrastructure.PrimaryTreatmentType != null ? _BaseEnumService.GetEnumText_PrimaryTreatmentTypeEnum((PrimaryTreatmentTypeEnum)infrastructure.PrimaryTreatmentType) : "---";
                sbKML.Append($@"                <p><b>PrimaryTreatmentType:</b> {PrimaryTreatmentTypeText}");
                if (infrastructure.PrimaryTreatmentTypeNew != null)
                {
                    string PrimaryTreatmentTypeNewText = infrastructure.PrimaryTreatmentTypeNew != null ? _BaseEnumService.GetEnumText_PrimaryTreatmentTypeEnum((PrimaryTreatmentTypeEnum)infrastructure.PrimaryTreatmentTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>PrimaryTreatmentType New:</b> {PrimaryTreatmentTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing SecondaryTreatmentType
                string SecondaryTreatmentTypeText = infrastructure.SecondaryTreatmentType != null ? _BaseEnumService.GetEnumText_SecondaryTreatmentTypeEnum((SecondaryTreatmentTypeEnum)infrastructure.SecondaryTreatmentType) : "---";
                sbKML.Append($@"                <p><b>SecondaryTreatmentType:</b> {SecondaryTreatmentTypeText}");
                if (infrastructure.SecondaryTreatmentTypeNew != null)
                {
                    string SecondaryTreatmentTypeNewText = infrastructure.SecondaryTreatmentTypeNew != null ? _BaseEnumService.GetEnumText_SecondaryTreatmentTypeEnum((SecondaryTreatmentTypeEnum)infrastructure.SecondaryTreatmentTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>SecondaryTreatmentType New:</b> {SecondaryTreatmentTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing TertiaryTreatmentType
                string TertiaryTreatmentTypeText = infrastructure.TertiaryTreatmentType != null ? _BaseEnumService.GetEnumText_TertiaryTreatmentTypeEnum((TertiaryTreatmentTypeEnum)infrastructure.TertiaryTreatmentType) : "---";
                sbKML.Append($@"                <p><b>TertiaryTreatmentType:</b> {TertiaryTreatmentTypeText}");
                if (infrastructure.TertiaryTreatmentTypeNew != null)
                {
                    string TertiaryTreatmentTypeNewText = infrastructure.TertiaryTreatmentTypeNew != null ? _BaseEnumService.GetEnumText_TertiaryTreatmentTypeEnum((TertiaryTreatmentTypeEnum)infrastructure.TertiaryTreatmentTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>TertiaryTreatmentType New:</b> {TertiaryTreatmentTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing TreatmentType
                string TreatmentTypeText = infrastructure.TreatmentType != null ? _BaseEnumService.GetEnumText_TreatmentTypeEnum((TreatmentTypeEnum)infrastructure.TreatmentType) : "---";
                sbKML.Append($@"                <p><b>TreatmentType:</b> {TreatmentTypeText}");
                if (infrastructure.TreatmentTypeNew != null)
                {
                    string TreatmentTypeNewText = infrastructure.TreatmentTypeNew != null ? _BaseEnumService.GetEnumText_TreatmentTypeEnum((TreatmentTypeEnum)infrastructure.TreatmentTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>TreatmentType New:</b> {TreatmentTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing DisinfectionType
                string DisinfectionTypeText = infrastructure.DisinfectionType != null ? _BaseEnumService.GetEnumText_DisinfectionTypeEnum((DisinfectionTypeEnum)infrastructure.DisinfectionType) : "---";
                sbKML.Append($@"                <p><b>DisinfectionType:</b> {DisinfectionTypeText}");
                if (infrastructure.DisinfectionTypeNew != null)
                {
                    string DisinfectionTypeNewText = infrastructure.DisinfectionTypeNew != null ? _BaseEnumService.GetEnumText_DisinfectionTypeEnum((DisinfectionTypeEnum)infrastructure.DisinfectionTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>DisinfectionType New:</b> {DisinfectionTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing CollectionSystemType
                string CollectionSystemTypeText = infrastructure.CollectionSystemType != null ? _BaseEnumService.GetEnumText_CollectionSystemTypeEnum((CollectionSystemTypeEnum)infrastructure.CollectionSystemType) : "---";
                sbKML.Append($@"                <p><b>CollectionSystemType:</b> {CollectionSystemTypeText}");
                if (infrastructure.CollectionSystemTypeNew != null)
                {
                    string CollectionSystemTypeNewText = infrastructure.CollectionSystemTypeNew != null ? _BaseEnumService.GetEnumText_CollectionSystemTypeEnum((CollectionSystemTypeEnum)infrastructure.CollectionSystemTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>CollectionSystemType New:</b> {CollectionSystemTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing AlarmSystemType
                string AlarmSystemTypeText = infrastructure.AlarmSystemType != null ? _BaseEnumService.GetEnumText_AlarmSystemTypeEnum((AlarmSystemTypeEnum)infrastructure.AlarmSystemType) : "---";
                sbKML.Append($@"                <p><b>AlarmSystemType:</b> {AlarmSystemTypeText}");
                if (infrastructure.AlarmSystemTypeNew != null)
                {
                    string AlarmSystemTypeNewText = infrastructure.AlarmSystemTypeNew != null ? _BaseEnumService.GetEnumText_AlarmSystemTypeEnum((AlarmSystemTypeEnum)infrastructure.AlarmSystemTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>AlarmSystemType New:</b> {AlarmSystemTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing DesignFlow_m3_day
                string DesignFlow_m3_dayText = infrastructure.DesignFlow_m3_day != null ? ((float)infrastructure.DesignFlow_m3_day).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>DesignFlow_m3_day:</b> {DesignFlow_m3_dayText}");
                if (infrastructure.DesignFlow_m3_dayNew != null)
                {
                    string DesignFlow_m3_dayNewText = infrastructure.DesignFlow_m3_dayNew != null ? ((float)infrastructure.DesignFlow_m3_dayNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>DesignFlow_m3_day New:</b> {DesignFlow_m3_dayNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing AverageFlow_m3_day
                string AverageFlow_m3_dayText = infrastructure.AverageFlow_m3_day != null ? ((float)infrastructure.AverageFlow_m3_day).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>AverageFlow_m3_day:</b> {AverageFlow_m3_dayText}");
                if (infrastructure.AverageFlow_m3_dayNew != null)
                {
                    string AverageFlow_m3_dayNewText = infrastructure.AverageFlow_m3_dayNew != null ? ((float)infrastructure.AverageFlow_m3_dayNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>AverageFlow_m3_day New:</b> {AverageFlow_m3_dayNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PeakFlow_m3_day
                string PeakFlow_m3_dayText = infrastructure.PeakFlow_m3_day != null ? ((float)infrastructure.PeakFlow_m3_day).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>PeakFlow_m3_day:</b> {PeakFlow_m3_dayText}");
                if (infrastructure.PeakFlow_m3_dayNew != null)
                {
                    string PeakFlow_m3_dayNewText = infrastructure.PeakFlow_m3_dayNew != null ? ((float)infrastructure.PeakFlow_m3_dayNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>PeakFlow_m3_day New:</b> {PeakFlow_m3_dayNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PopServed
                string PopServedText = infrastructure.PopServed != null ? ((int)infrastructure.PopServed).ToString() : "---";
                sbKML.Append($@"                <p><b>PopServed:</b> {PopServedText}");
                if (infrastructure.PopServedNew != null)
                {
                    string PopServedNewText = infrastructure.PopServedNew != null ? ((int)infrastructure.PopServedNew).ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>PopServed New:</b> {PopServedNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing CanOverflow
                string CanOverflow = infrastructure.CanOverflow != null && infrastructure.CanOverflow == true ? "true" : "false";
                sbKML.Append($@"                <p><b>CanOverflow:</b> {CanOverflow}</p>");
                if (infrastructure.CanOverflowNew != null)
                {
                    string CanOverflowNew = infrastructure.CanOverflowNew == true ? "true" : "false";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>Is Active New:</b> {CanOverflowNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PercFlowOfTotal
                string PercFlowOfTotalText = infrastructure.PercFlowOfTotal != null ? ((float)infrastructure.PercFlowOfTotal).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>PercFlowOfTotal:</b> {PercFlowOfTotalText}");
                if (infrastructure.PercFlowOfTotalNew != null)
                {
                    string PercFlowOfTotalNewText = infrastructure.PercFlowOfTotalNew != null ? ((float)infrastructure.PercFlowOfTotalNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>PercFlowOfTotal New:</b> {PercFlowOfTotalNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing TimeOffset_hour
                string TimeOffset_hourText = infrastructure.TimeOffset_hour != null ? ((float)infrastructure.TimeOffset_hour).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>TimeOffset_hour:</b> {TimeOffset_hourText}");
                if (infrastructure.TimeOffset_hourNew != null)
                {
                    string TimeOffset_hourNewText = infrastructure.TimeOffset_hourNew != null ? ((float)infrastructure.TimeOffset_hourNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>TimeOffset_hour New:</b> {TimeOffset_hourNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing TempCatchAllRemoveLater
                sbKML.AppendLine($@"                <p><b>TempCatchAllRemoveLater:</b>{infrastructure.TempCatchAllRemoveLater.Replace("\r\n", "<br />")}</p>");
                if (!string.IsNullOrWhiteSpace(infrastructure.TempCatchAllRemoveLaterNew))
                {
                    sbKML.AppendLine($@"                <p><b>TempCatchAllRemoveLater New:</b>{infrastructure.TempCatchAllRemoveLaterNew.Replace("\r\n", "<br />")}</p>");
                }

                // doing AverageDepth_m
                string AverageDepth_mText = infrastructure.AverageDepth_m != null ? ((float)infrastructure.AverageDepth_m).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>AverageDepth_m:</b> {AverageDepth_mText}");
                if (infrastructure.AverageDepth_mNew != null)
                {
                    string AverageDepth_mNewText = infrastructure.AverageDepth_mNew != null ? ((float)infrastructure.AverageDepth_mNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>AverageDepth_m New:</b> {AverageDepth_mNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing NumberOfPorts
                string NumberOfPortsText = infrastructure.NumberOfPorts != null ? ((int)infrastructure.NumberOfPorts).ToString() : "---";
                sbKML.Append($@"                <p><b>NumberOfPorts:</b> {NumberOfPortsText}");
                if (infrastructure.NumberOfPortsNew != null)
                {
                    string NumberOfPortsNewText = infrastructure.NumberOfPortsNew != null ? ((int)infrastructure.NumberOfPortsNew).ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>NumberOfPorts New:</b> {NumberOfPortsNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PortDiameter_m
                string PortDiameter_mText = infrastructure.PortDiameter_m != null ? ((float)infrastructure.PortDiameter_m).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>PortDiameter_m:</b> {PortDiameter_mText}");
                if (infrastructure.PortDiameter_mNew != null)
                {
                    string PortDiameter_mNewText = infrastructure.PortDiameter_mNew != null ? ((float)infrastructure.PortDiameter_mNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>PortDiameter_m New:</b> {PortDiameter_mNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PortSpacing_m
                string PortSpacing_mText = infrastructure.PortSpacing_m != null ? ((float)infrastructure.PortSpacing_m).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>PortSpacing_m:</b> {PortSpacing_mText}");
                if (infrastructure.PortSpacing_mNew != null)
                {
                    string PortSpacing_mNewText = infrastructure.PortSpacing_mNew != null ? ((float)infrastructure.PortSpacing_mNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>PortSpacing_m New:</b> {PortSpacing_mNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PortElevation_m
                string PortElevation_mText = infrastructure.PortElevation_m != null ? ((float)infrastructure.PortElevation_m).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>PortElevation_m:</b> {PortElevation_mText}");
                if (infrastructure.PortElevation_mNew != null)
                {
                    string PortElevation_mNewText = infrastructure.PortElevation_mNew != null ? ((float)infrastructure.PortElevation_mNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>PortElevation_m New:</b> {PortElevation_mNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing VerticalAngle_deg
                string VerticalAngle_degText = infrastructure.VerticalAngle_deg != null ? ((float)infrastructure.VerticalAngle_deg).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>VerticalAngle_deg:</b> {VerticalAngle_degText}");
                if (infrastructure.VerticalAngle_degNew != null)
                {
                    string VerticalAngle_degNewText = infrastructure.VerticalAngle_degNew != null ? ((float)infrastructure.VerticalAngle_degNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>VerticalAngle_deg New:</b> {VerticalAngle_degNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing HorizontalAngle_deg
                string HorizontalAngle_degText = infrastructure.HorizontalAngle_deg != null ? ((float)infrastructure.HorizontalAngle_deg).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>HorizontalAngle_deg:</b> {HorizontalAngle_degText}");
                if (infrastructure.HorizontalAngle_degNew != null)
                {
                    string HorizontalAngle_degNewText = infrastructure.HorizontalAngle_degNew != null ? ((float)infrastructure.HorizontalAngle_degNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>HorizontalAngle_deg New:</b> {HorizontalAngle_degNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing DecayRate_per_day
                string DecayRate_per_dayText = infrastructure.DecayRate_per_day != null ? ((float)infrastructure.DecayRate_per_day).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>DecayRate_per_day:</b> {DecayRate_per_dayText}");
                if (infrastructure.DecayRate_per_dayNew != null)
                {
                    string DecayRate_per_dayNewText = infrastructure.DecayRate_per_dayNew != null ? ((float)infrastructure.DecayRate_per_dayNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>DecayRate_per_day New:</b> {DecayRate_per_dayNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing NearFieldVelocity_m_s
                string NearFieldVelocity_m_sText = infrastructure.NearFieldVelocity_m_s != null ? ((float)infrastructure.NearFieldVelocity_m_s).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>NearFieldVelocity_m_s:</b> {NearFieldVelocity_m_sText}");
                if (infrastructure.NearFieldVelocity_m_sNew != null)
                {
                    string NearFieldVelocity_m_sNewText = infrastructure.NearFieldVelocity_m_sNew != null ? ((float)infrastructure.NearFieldVelocity_m_sNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>NearFieldVelocity_m_s New:</b> {NearFieldVelocity_m_sNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing FarFieldVelocity_m_s
                string FarFieldVelocity_m_sText = infrastructure.FarFieldVelocity_m_s != null ? ((float)infrastructure.FarFieldVelocity_m_s).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>FarFieldVelocity_m_s:</b> {FarFieldVelocity_m_sText}");
                if (infrastructure.FarFieldVelocity_m_sNew != null)
                {
                    string FarFieldVelocity_m_sNewText = infrastructure.FarFieldVelocity_m_sNew != null ? ((float)infrastructure.FarFieldVelocity_m_sNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>FarFieldVelocity_m_s New:</b> {FarFieldVelocity_m_sNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing ReceivingWaterSalinity_PSU
                string ReceivingWaterSalinity_PSUText = infrastructure.ReceivingWaterSalinity_PSU != null ? ((float)infrastructure.ReceivingWaterSalinity_PSU).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>ReceivingWaterSalinity_PSU:</b> {ReceivingWaterSalinity_PSUText}");
                if (infrastructure.ReceivingWaterSalinity_PSUNew != null)
                {
                    string ReceivingWaterSalinity_PSUNewText = infrastructure.ReceivingWaterSalinity_PSUNew != null ? ((float)infrastructure.ReceivingWaterSalinity_PSUNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>ReceivingWaterSalinity_PSU New:</b> {ReceivingWaterSalinity_PSUNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing ReceivingWaterTemperature_C
                string ReceivingWaterTemperature_CText = infrastructure.ReceivingWaterTemperature_C != null ? ((float)infrastructure.ReceivingWaterTemperature_C).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>ReceivingWaterTemperature_C:</b> {ReceivingWaterTemperature_CText}");
                if (infrastructure.ReceivingWaterTemperature_CNew != null)
                {
                    string ReceivingWaterTemperature_CNewText = infrastructure.ReceivingWaterTemperature_CNew != null ? ((float)infrastructure.ReceivingWaterTemperature_CNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>ReceivingWaterTemperature_C New:</b> {ReceivingWaterTemperature_CNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing ReceivingWater_MPN_per_100ml
                string ReceivingWater_MPN_per_100mlText = infrastructure.ReceivingWater_MPN_per_100ml != null ? ((int)infrastructure.ReceivingWater_MPN_per_100ml).ToString() : "---";
                sbKML.Append($@"                <p><b>ReceivingWater_MPN_per_100ml:</b> {ReceivingWater_MPN_per_100mlText}");
                if (infrastructure.ReceivingWater_MPN_per_100mlNew != null)
                {
                    string ReceivingWater_MPN_per_100mlNewText = infrastructure.ReceivingWater_MPN_per_100mlNew != null ? ((int)infrastructure.ReceivingWater_MPN_per_100mlNew).ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>ReceivingWater_MPN_per_100ml New:</b> {ReceivingWater_MPN_per_100mlNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing DistanceFromShore_m
                string DistanceFromShore_mText = infrastructure.DistanceFromShore_m != null ? ((float)infrastructure.DistanceFromShore_m).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>DistanceFromShore_m:</b> {DistanceFromShore_mText}");
                if (infrastructure.DistanceFromShore_mNew != null)
                {
                    string DistanceFromShore_mNewText = infrastructure.DistanceFromShore_mNew != null ? ((float)infrastructure.DistanceFromShore_mNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>DistanceFromShore_m New:</b> {DistanceFromShore_mNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing SeeOtherTVItemID
                string SeeOtherTVItemIDText = infrastructure.SeeOtherTVItemID != null ? ((int)infrastructure.SeeOtherTVItemID).ToString() : "---";
                sbKML.Append($@"                <p><b>SeeOtherTVItemID:</b> {SeeOtherTVItemIDText}");
                if (infrastructure.SeeOtherTVItemIDNew != null)
                {
                    string SeeOtherTVItemIDNewText = infrastructure.SeeOtherTVItemIDNew != null ? ((int)infrastructure.SeeOtherTVItemIDNew).ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>SeeOtherTVItemID New:</b> {SeeOtherTVItemIDNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PumpsToTVItemID
                Infrastructure infPumpTo = new Infrastructure();
                if (infrastructure.PumpsToTVItemID != null)
                {
                    infPumpTo = municipalityDoc.Municipality.InfrastructureList.Where(c => c.InfrastructureTVItemID == infrastructure.PumpsToTVItemID).FirstOrDefault();
                }
                string PumpsToTVItemIDText = infrastructure.PumpsToTVItemID != null ? (infPumpTo != null ? (infPumpTo.TVTextNew != null ? infPumpTo.TVTextNew : infPumpTo.TVText) : "---")  : "---";
                sbKML.Append($@"                <p><b>PumpsToTVItemID:</b> {PumpsToTVItemIDText}");
                if (infrastructure.PumpsToTVItemIDNew != null)
                {
                    Infrastructure infPumpToNew = new Infrastructure();
                    if (infrastructure.PumpsToTVItemIDNew != null)
                    {
                        infPumpToNew = municipalityDoc.Municipality.InfrastructureList.Where(c => c.InfrastructureTVItemID == infrastructure.PumpsToTVItemIDNew).FirstOrDefault();
                    }
                    string PumpsToTVItemIDNewText = infrastructure.PumpsToTVItemIDNew != null ? (infPumpToNew != null ? (infPumpToNew.TVTextNew != null ? infPumpToNew.TVTextNew : infPumpToNew.TVText) : "---") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>PumpsToTVItemID New:</b> {PumpsToTVItemIDNewText}");
                }
                sbKML.AppendLine($@"</p>");



                sbKML.AppendLine($@"                </blockquote>");


                if (infrastructure.InfrastructurePictureList.Count > 0)
                {
                    sbKML.AppendLine($@"                <h3>Images</h3>");
                    sbKML.AppendLine($@"                <ul>");
                    foreach (Picture picture in infrastructure.InfrastructurePictureList)
                    {
                        string url = @"file:///C:\Infrastructures\" + CurrentMunicipalityName + @"\Pictures\" + infrastructure.InfrastructureTVItemID + "_" + picture.PictureTVItemID + ".jpg";

                        sbKML.AppendLine($@"                <li><img style=""max-width:600px;"" src=""{url}"" /></li>");
                    }
                    sbKML.AppendLine($@"                </ul>");
                }

                sbKML.AppendLine($@"            ]]>");
                sbKML.AppendLine($@"            </description>");
                sbKML.AppendLine($@"			<styleUrl>#m_ylw-pushpin</styleUrl>");
                sbKML.AppendLine($@"			<Point>");
                if (infrastructure.LatNew != null || infrastructure.LngNew != null)
                {
                    sbKML.AppendLine($@"				<coordinates>{infrastructure.LngNew},{infrastructure.LatNew},0</coordinates>");
                }
                else
                {
                    sbKML.AppendLine($@"				<coordinates>{infrastructure.Lng},{infrastructure.Lat},0</coordinates>");
                }
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
                EmitStatus(new StatusEventArgs($@"An error happened during the regeneration of the [{fi.FullName}] file"));
                return;
            }

            EmitStatus(new StatusEventArgs($@"The file [{fi.FullName}] has been regenerated with new changes"));
            EmitStatus(new StatusEventArgs($"Done ... file [{fi.FullName}] has been regenerated"));
            EmitStatus(new StatusEventArgs($""));
        }
        public void RegenerateSubsectorKMLFile()
        {

            EmitStatus(new StatusEventArgs($@"Regenerating subsector KML file for subsector [{CurrentSubsectorName}]"));

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
                    EmitStatus(new StatusEventArgs(ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "") + "\r\n"));
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
                EmitStatus(new StatusEventArgs($@"An error happened during the regeneration of the [{fi.FullName}] file"));
                return;
            }

            EmitStatus(new StatusEventArgs($@"The file [{fi.FullName}] has been regenerated with new changes"));
            EmitStatus(new StatusEventArgs($"Done ... file [{fi.FullName}] has been regenerated"));
            EmitStatus(new StatusEventArgs($""));
        }
    }
}
