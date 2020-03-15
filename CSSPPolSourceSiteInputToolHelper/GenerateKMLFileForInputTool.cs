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
                    Process.Start(@"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe", @"""" + fi.FullName + @"""");
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

            sbKML.AppendLine($@"		<Placemark>");
            sbKML.AppendLine($@"			<name>{municipalityDoc.Municipality.MunicipalityName} --- Contact(s)</name>");
            sbKML.AppendLine($@"            <description><![CDATA[");
            foreach (Contact contact in municipalityDoc.Municipality.ContactList.OrderBy(c => c.LastName).ThenBy(c => c.FirstName))
            {
                string FirstName = contact.FirstNameNew != null ? contact.FirstNameNew : contact.FirstName;
                string Initial = contact.InitialNew != null ? contact.InitialNew : contact.Initial;
                string LastName = contact.LastNameNew != null ? contact.LastNameNew : contact.LastName;

                string IsActive2 = contact.IsActive != null && contact.IsActive == true ? "Active" : "Inactive";
                string IsActiveNew2 = contact.IsActiveNew == null ? "" : contact.IsActiveNew  == true ? "Active" : "Inactive";

                string IsActiveText = $"({IsActive2})"; ;
                if (!string.IsNullOrWhiteSpace(IsActiveNew2))
                {
                    IsActiveText = $"({IsActive2} -> {IsActiveNew2})";
                }

                string Title = "empty";
                string TitleNew = "";
                if (contact.ContactTitle != (int)ContactTitleEnum.Error)
                {
                    Title = _BaseEnumService.GetEnumText_ContactTitleEnum((ContactTitleEnum)contact.ContactTitle);
                }
                if (contact.ContactTitleNew != null)
                {
                    if (contact.ContactTitleNew != (int)ContactTitleEnum.Error)
                    {
                        TitleNew = _BaseEnumService.GetEnumText_ContactTitleEnum((ContactTitleEnum)contact.ContactTitleNew);
                    }
                }
               
                string TitleText = $"({Title})"; ;
                if (!string.IsNullOrWhiteSpace(TitleNew))
                {
                    TitleText = $"({Title} -> {TitleNew})";
                }

                sbKML.AppendLine($@"                <h3>{FirstName} {Initial}, {LastName} ({TitleText}) ({IsActiveText})</h3>");

                if (contact.FirstNameNew != null || contact.InitialNew != null || contact.LastNameNew != null)
                {
                    string FirstNameNew = contact.FirstNameNew != null ? contact.FirstNameNew : contact.FirstName;
                    string InitialNew = contact.InitialNew != null ? contact.InitialNew : contact.Initial;
                    string LastNameNew = contact.LastNameNew != null ? contact.LastNameNew : contact.LastName;
                    sbKML.AppendLine($@"                <b>New name:</b> {FirstNameNew} {InitialNew}, {LastNameNew}<br />");
                }

                // doing Telephones
                sbKML.AppendLine($@"                <h4>Telephone(s)</h3>");

                foreach (Telephone telephone in contact.TelephoneList)
                {
                    string TelNumber = $"{telephone.TelNumber}";
                    string TelNumberNew = telephone.TelNumberNew == null ? "" : telephone.TelNumberNew;

                    string TelNumberText = TelNumber;
                    if (!string.IsNullOrWhiteSpace(TelNumberNew))
                    {
                        TelNumberText = $"({TelNumber} -> {TelNumberNew})";
                    }

                    string TelType = $"{_BaseEnumService.GetEnumText_TelTypeEnum((TelTypeEnum)telephone.TelType)}";
                    string TelTypeNew = telephone.TelTypeNew == null ? "" : _BaseEnumService.GetEnumText_TelTypeEnum((TelTypeEnum)telephone.TelTypeNew);

                    string TelTypeText = TelType;
                    if (!string.IsNullOrWhiteSpace(TelTypeNew))
                    {
                        TelTypeText = $"({TelType} -> {TelTypeNew})";
                    }

                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;{TelNumberText} ({TelTypeText})<br />");
                }

                // doing Emails
                sbKML.AppendLine($@"                <h4>Email(s)</h3>");

                foreach (Email email in contact.EmailList)
                {
                    string EmailAddress = $"{email.EmailAddress}";
                    string EmailAddressNew = email.EmailAddressNew == null ? "" : email.EmailAddressNew;

                    string EmailAddressText = EmailAddress;
                    if (!string.IsNullOrWhiteSpace(EmailAddressNew))
                    {
                        EmailAddressText = $"({EmailAddress} -> {EmailAddressNew})";
                    }

                    string EmailType = $"{_BaseEnumService.GetEnumText_EmailTypeEnum((EmailTypeEnum)email.EmailType)}";
                    string EmailTypeNew = email.EmailTypeNew == null ? "" : _BaseEnumService.GetEnumText_EmailTypeEnum((EmailTypeEnum)email.EmailTypeNew);

                    string EmailTypeText = EmailType;
                    if (!string.IsNullOrWhiteSpace(EmailTypeNew))
                    {
                        EmailTypeText = $"({EmailType} -> {EmailTypeNew})";
                    }

                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;{EmailAddressText} ({EmailTypeText})<br />");
                }

                sbKML.AppendLine($@"                <h4>Addresse</h3>");

                if (contact.ContactAddress != null)
                {
                    string StreetNumber = contact.ContactAddress.StreetNumber == null ? "" : contact.ContactAddress.StreetNumber + " ";
                    string StreetName = contact.ContactAddress.StreetName == null ? "" : contact.ContactAddress.StreetName + " ";
                    string StreetType = contact.ContactAddress.StreetType == null ? "" : _BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)contact.ContactAddress.StreetType) + ", ";
                    string Municipality = contact.ContactAddress.Municipality == null ? "" : contact.ContactAddress.Municipality + ", ";
                    string PostalCode = contact.ContactAddress.PostalCode == null ? "" : contact.ContactAddress.PostalCode;

                    string Address = $"{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}".Trim();
                    if (!string.IsNullOrWhiteSpace(Address))
                    {
                        sbKML.AppendLine($@"                &nbsp;&nbsp;&nbsp;{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}<br />");
                    }
                }
                if (contact.ContactAddressNew != null)
                {
                    string StreetNumber = contact.ContactAddressNew.StreetNumber == null ? "" : contact.ContactAddressNew.StreetNumber + " ";
                    string StreetName = contact.ContactAddressNew.StreetName == null ? "" : contact.ContactAddressNew.StreetName + " ";
                    string StreetType = contact.ContactAddressNew.StreetType == null ? "" : _BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)contact.ContactAddressNew.StreetType) + ", ";
                    string Municipality = contact.ContactAddressNew.Municipality == null ? "" : contact.ContactAddressNew.Municipality + ", ";
                    string PostalCode = contact.ContactAddressNew.PostalCode == null ? "" : contact.ContactAddressNew.PostalCode;

                    string Address = $"{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}".Trim();
                    if (!string.IsNullOrWhiteSpace(Address))
                    {
                        sbKML.AppendLine($@"                &nbsp;&nbsp;&nbsp;New Address: {StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}<br />");
                    }
                }

            }
            sbKML.AppendLine($@"");
            sbKML.AppendLine($@"");
            sbKML.AppendLine($@"");
            sbKML.AppendLine($@"            ]]>");
            sbKML.AppendLine($@"            </description>");
            sbKML.AppendLine($@"			<Point>");
            sbKML.AppendLine($@"				<coordinates>{municipalityDoc.Municipality.Lng},{municipalityDoc.Municipality.Lat},0</coordinates>");
            sbKML.AppendLine($@"			</Point>");
            sbKML.AppendLine($@"		</Placemark>");

            foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList.OrderBy(c => c.TVText))
            {
                sbKML.AppendLine($@"        <Folder>");
                sbKML.AppendLine($@"        <name>{infrastructure.TVText}</name>");
                sbKML.AppendLine($@"        <open>1</open>");
                
                // doing Line Path Infrastructure
                if (infrastructure.LinePathInfNew != null && infrastructure.LinePathInfNew.MapInfoID > 0)
                {
                    sbKML.AppendLine($@"		<Placemark>");
                    sbKML.AppendLine($@"			<name>Line Path (Pumping to)</name>");
                    sbKML.AppendLine($@"            <description><![CDATA[<span style=""display: hidden"">{infrastructure.LinePathInfNew.MapInfoID}</span>]]></description>");
                    sbKML.AppendLine($@"		    <LineString>");
                    sbKML.AppendLine($@"		    <coordinates>");
                    foreach (Coord coord in infrastructure.LinePathInfNew.CoordList)
                    {
                        sbKML.Append($"{coord.Lng},{coord.Lat},0 ");
                    }
                    sbKML.AppendLine($@"");
                    sbKML.AppendLine($@"		    </coordinates>");
                    sbKML.AppendLine($@"		    </LineString>");
                    sbKML.AppendLine($@"		</Placemark>");
                }
                else if (infrastructure.LinePathInf != null && infrastructure.LinePathInf.MapInfoID > 0)
                {
                    sbKML.AppendLine($@"		<Placemark>");
                    sbKML.AppendLine($@"			<name>Line Path (Pumping to)</name>");
                    sbKML.AppendLine($@"            <description><![CDATA[<span style=""display: hidden"">{infrastructure.LinePathInf.MapInfoID}</span>]]></description>");
                    sbKML.AppendLine($@"		    <LineString>");
                    sbKML.AppendLine($@"		    <coordinates>");
                    foreach (Coord coord in infrastructure.LinePathInf.CoordList)
                    {
                        sbKML.Append($"{coord.Lng},{coord.Lat},0 ");
                    }
                    sbKML.AppendLine($@"");
                    sbKML.AppendLine($@"		    </coordinates>");
                    sbKML.AppendLine($@"		    </LineString>");
                    sbKML.AppendLine($@"		</Placemark>");
                }
                else
                {
                    sbKML.AppendLine($@"		<Placemark>");
                    sbKML.AppendLine($@"			<name>Line Path (Pumping to)</name>");
                    sbKML.AppendLine($@"            <description><![CDATA[<span style=""display: hidden"">0</span>]]></description>");
                    sbKML.AppendLine($@"		    <LineString>");
                    sbKML.AppendLine($@"		    <coordinates>");
                    if (infrastructure.LatNew != null && infrastructure.LngNew != null)
                    {
                        int? pumpsToTVItemID = infrastructure.PumpsToTVItemIDNew != null ? infrastructure.PumpsToTVItemIDNew : infrastructure.PumpsToTVItemID != null ? infrastructure.PumpsToTVItemID : null;
                        if (pumpsToTVItemID != null)
                        {
                            Infrastructure infPumpsTo = (from c in municipalityDoc.Municipality.InfrastructureList
                                                         where c.InfrastructureTVItemID == pumpsToTVItemID
                                                         select c).FirstOrDefault();

                            if (infPumpsTo != null)
                            {
                                if (infPumpsTo.LatNew != null && infPumpsTo.LngNew != null)
                                {
                                    sbKML.Append($"{infrastructure.LngNew},{infrastructure.LatNew},0 {infPumpsTo.LngNew},{infPumpsTo.LatNew},0 ");
                                }
                                else if (infPumpsTo.Lat != null && infPumpsTo.Lng != null)
                                {
                                    sbKML.Append($"{infrastructure.LngNew},{infrastructure.LatNew},0 {infPumpsTo.Lng},{infPumpsTo.Lat},0 ");
                                }
                                else
                                {
                                    sbKML.Append($"{infrastructure.LngNew},{infrastructure.LatNew},0 {infrastructure.LngNew},{infrastructure.LatNew},0 ");
                                }
                            }
                            else
                            {
                                sbKML.Append($"{infrastructure.LngNew},{infrastructure.LatNew},0 {infrastructure.LngNew},{infrastructure.LatNew},0 ");
                            }
                        }
                        else if (infrastructure.PumpsToTVItemID != null)
                        {
                            sbKML.Append($"{infrastructure.LngNew},{infrastructure.LatNew},0 {infrastructure.LngNew},{infrastructure.LatNew},0 ");
                        }
                    }
                    else if (infrastructure.Lat != null && infrastructure.Lng != null)
                    {
                        int? pumpsToTVItemID = infrastructure.PumpsToTVItemIDNew != null ? infrastructure.PumpsToTVItemIDNew : infrastructure.PumpsToTVItemID != null ? infrastructure.PumpsToTVItemID : null;
                        if (pumpsToTVItemID != null)
                        {
                            Infrastructure infPumpsTo = (from c in municipalityDoc.Municipality.InfrastructureList
                                                         where c.InfrastructureTVItemID == pumpsToTVItemID
                                                         select c).FirstOrDefault();

                            if (infPumpsTo != null)
                            {
                                if (infPumpsTo.LatNew != null && infPumpsTo.LngNew != null)
                                {
                                    sbKML.Append($"{infrastructure.Lng},{infrastructure.Lat},0 {infPumpsTo.LngNew},{infPumpsTo.LatNew},0 ");
                                }
                                else if (infPumpsTo.Lat != null && infPumpsTo.Lng != null)
                                {
                                    sbKML.Append($"{infrastructure.Lng},{infrastructure.Lat},0 {infPumpsTo.Lng},{infPumpsTo.Lat},0 ");
                                }
                                else
                                {
                                    sbKML.Append($"{infrastructure.Lng},{infrastructure.Lat},0 {infrastructure.LngNew},{infrastructure.LatNew},0 ");
                                }
                            }
                            else
                            {
                                sbKML.Append($"{infrastructure.Lng},{infrastructure.Lat},0 {infrastructure.LngNew},{infrastructure.LatNew},0 ");
                            }
                        }
                        else if (infrastructure.PumpsToTVItemID != null)
                        {
                            sbKML.Append($"{infrastructure.Lng},{infrastructure.Lat},0 {infrastructure.LngNew},{infrastructure.LatNew},0 ");
                        }
                    }
                    sbKML.AppendLine($@"");
                    sbKML.AppendLine($@"		    </coordinates>");
                    sbKML.AppendLine($@"		    </LineString>");
                    sbKML.AppendLine($@"		</Placemark>");
                }
                
                // doing Line Path Infrastructure Outfall
                if (infrastructure.LinePathInfOutfallNew != null && infrastructure.LinePathInfOutfallNew.MapInfoID > 0)
                {
                    sbKML.AppendLine($@"		<Placemark>");
                    sbKML.AppendLine($@"			<name>Line Path (Outfall)</name>");
                    sbKML.AppendLine($@"            <description><![CDATA[<span style=""display: hidden"">{infrastructure.LinePathInfOutfallNew.MapInfoID}</span>]]></description>");
                    sbKML.AppendLine($@"		    <LineString>");
                    sbKML.AppendLine($@"		    <coordinates>");
                    foreach (Coord coord in infrastructure.LinePathInfOutfallNew.CoordList)
                    {
                        sbKML.Append($"{coord.Lng},{coord.Lat},0 ");
                    }
                    sbKML.AppendLine($@"");
                    sbKML.AppendLine($@"		    </coordinates>");
                    sbKML.AppendLine($@"		    </LineString>");
                    sbKML.AppendLine($@"		</Placemark>");
                }
                else if (infrastructure.LinePathInfOutfall != null && infrastructure.LinePathInfOutfall.MapInfoID > 0)
                {
                    sbKML.AppendLine($@"		<Placemark>");
                    sbKML.AppendLine($@"			<name>Line Path (Outfall)</name>");
                    sbKML.AppendLine($@"            <description><![CDATA[<span style=""display: hidden"">{infrastructure.LinePathInfOutfall.MapInfoID}</span>]]></description>");
                    sbKML.AppendLine($@"		    <LineString>");
                    sbKML.AppendLine($@"		    <coordinates>");
                    foreach (Coord coord in infrastructure.LinePathInfOutfall.CoordList)
                    {
                        sbKML.Append($"{coord.Lng},{coord.Lat},0 ");
                    }
                    sbKML.AppendLine($@"");
                    sbKML.AppendLine($@"		    </coordinates>");
                    sbKML.AppendLine($@"		    </LineString>");
                    sbKML.AppendLine($@"		</Placemark>");
                }
                else
                {
                    sbKML.AppendLine($@"		<Placemark>");
                    sbKML.AppendLine($@"			<name>Line Path (Outfall)</name>");
                    sbKML.AppendLine($@"            <description><![CDATA[<span style=""display: hidden"">0</span>]]></description>");
                    sbKML.AppendLine($@"		    <LineString>");
                    sbKML.AppendLine($@"		    <coordinates>");
                    if (infrastructure.LatNew != null && infrastructure.LngNew != null)
                    {
                        if (infrastructure.LatOutfallNew != null && infrastructure.LngOutfallNew != null)
                        {
                            sbKML.Append($"{infrastructure.LngNew},{infrastructure.LatNew},0 {infrastructure.LngOutfallNew},{infrastructure.LatOutfallNew},0 ");
                        }
                        else if (infrastructure.LatOutfall != null && infrastructure.LngOutfall != null)
                        {
                            sbKML.Append($"{infrastructure.LngNew},{infrastructure.LatNew},0 {infrastructure.LngOutfall},{infrastructure.LatOutfall},0 ");
                        }
                        else
                        {
                            sbKML.Append($"{infrastructure.LngNew},{infrastructure.LatNew},0 {infrastructure.LngNew},{infrastructure.LatNew},0 ");
                        }
                    }
                    else if (infrastructure.Lat != null && infrastructure.Lng != null)
                    {
                        if (infrastructure.LatOutfallNew != null && infrastructure.LngOutfallNew != null)
                        {
                            sbKML.Append($"{infrastructure.Lng},{infrastructure.Lat},0 {infrastructure.LngOutfallNew},{infrastructure.LatOutfallNew},0 ");
                        }
                        else if (infrastructure.LatOutfall != null && infrastructure.LngOutfall != null)
                        {
                            sbKML.Append($"{infrastructure.Lng},{infrastructure.Lat},0 {infrastructure.LngOutfall},{infrastructure.LatOutfall},0 ");
                        }
                        else
                        {
                            sbKML.Append($"{infrastructure.Lng},{infrastructure.Lat},0 {infrastructure.LngNew},{infrastructure.LatNew},0 ");
                        }
                    }
                    sbKML.AppendLine($@"");
                    sbKML.AppendLine($@"		    </coordinates>");
                    sbKML.AppendLine($@"		    </LineString>");
                    sbKML.AppendLine($@"		</Placemark>");
                }
                sbKML.AppendLine($@"		<Placemark>");
                string InfrastructureNameText = string.IsNullOrWhiteSpace(infrastructure.TVTextNew) ? infrastructure.TVText : infrastructure.TVTextNew;
                sbKML.AppendLine($@"			<name>{InfrastructureNameText} --- ({infrastructure.InfrastructureTVItemID.ToString()})</name>");
                sbKML.AppendLine($@"            <description><![CDATA[");
                if (!string.IsNullOrWhiteSpace(infrastructure.TVTextNew))
                {
                    sbKML.AppendLine($@"            New name: {infrastructure.TVTextNew}<br />");
                }

                sbKML.AppendLine($@"            <h3>Address</h3>");

                if (infrastructure.InfrastructureAddress != null)
                {
                    string StreetNumber = infrastructure.InfrastructureAddress.StreetNumber == null ? "" : infrastructure.InfrastructureAddress.StreetNumber + " ";
                    string StreetName = infrastructure.InfrastructureAddress.StreetName == null ? "" : infrastructure.InfrastructureAddress.StreetName + " ";
                    string StreetType = infrastructure.InfrastructureAddress.StreetType == null ? "" : _BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)infrastructure.InfrastructureAddress.StreetType) + ", ";
                    string Municipality = infrastructure.InfrastructureAddress.Municipality == null ? "" : infrastructure.InfrastructureAddress.Municipality + ", ";
                    string PostalCode = infrastructure.InfrastructureAddress.PostalCode == null ? "" : infrastructure.InfrastructureAddress.PostalCode;

                    string Address = $"{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}".Trim();
                    if (!string.IsNullOrWhiteSpace(Address))
                    {
                        sbKML.AppendLine($@"                &nbsp;&nbsp;&nbsp;{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}<br />");
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
                    if (!string.IsNullOrWhiteSpace(Address))
                    {
                        sbKML.AppendLine($@"                &nbsp;&nbsp;&nbsp;{StreetNumber}{StreetName}{StreetType}{Municipality}{PostalCode}<br />");
                    }
                }

                sbKML.AppendLine($@"                <h3>Details</h3>");
                sbKML.AppendLine($@"                <blockquote>");

                // doing IsActive
                string IsActive = infrastructure.IsActive != null && infrastructure.IsActive == true ? "true" : "false";
                sbKML.Append($@"                <p><b>IsActive:</b>  {IsActive}</p>");
                if (infrastructure.IsActiveNew != null)
                {
                    string IsActiveNew = infrastructure.IsActiveNew == true ? "true" : "false";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>Is Active New:</b>  {IsActiveNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing LastUpdateDate_UTC
                string LastUpdateDate_UTC = infrastructure.LastUpdateDate_UTC != null ? ((DateTime)infrastructure.LastUpdateDate_UTC).ToString("yyyy MMMM dd") : "---";
                sbKML.AppendLine($@"                <p><b>Last Update Date:</b>  {LastUpdateDate_UTC}</p>");

                // doing CommentEN
                sbKML.AppendLine($@"                <p><b>Comment (EN):</b>  {infrastructure.CommentEN.Replace("\r\n", "<br />")}</p>");
                if (!string.IsNullOrWhiteSpace(infrastructure.CommentENNew))
                {
                    sbKML.AppendLine($@"                <p><b>Comment New (EN):</b>  {infrastructure.CommentENNew.Replace("\r\n", "<br />")}</p>");
                }

                // doing CommentFR
                sbKML.AppendLine($@"                <p><b>Comment (FR):</b>  {infrastructure.CommentFR.Replace("\r\n", "<br />")}</p>");
                if (!string.IsNullOrWhiteSpace(infrastructure.CommentFRNew))
                {
                    sbKML.AppendLine($@"                <p><b>Comment New (FR):</b>  {infrastructure.CommentFRNew.Replace("\r\n", "<br />")}</p>");
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

                //// doing Prism
                //string PrismText = infrastructure.PrismID != null ? infrastructure.PrismID.ToString() : "---";
                //sbKML.Append($@"                <p><b>Prism:</b> {PrismText}");
                //if (infrastructure.PrismIDNew != null)
                //{
                //    string PrismNewText = infrastructure.PrismIDNew != null ? infrastructure.PrismIDNew.ToString() : "---";
                //    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Prism New:</b> {PrismNewText}");
                //}
                //sbKML.AppendLine($@"</p>");

                //// doing TPID
                //string TPIDText = infrastructure.TPID != null ? infrastructure.TPID.ToString() : "---";
                //sbKML.Append($@"                <p><b>TPID:</b> {TPIDText}");
                //if (infrastructure.TPIDNew != null)
                //{
                //    string TPIDNewText = infrastructure.TPIDNew != null ? infrastructure.TPIDNew.ToString() : "---";
                //    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>TPID New:</b> {TPIDNewText}");
                //}
                //sbKML.AppendLine($@"</p>");

                //// doing LSID
                //string LSIDText = infrastructure.LSID != null ? infrastructure.LSID.ToString() : "---";
                //sbKML.Append($@"                <p><b>LSID:</b> {LSIDText}");
                //if (infrastructure.LSIDNew != null)
                //{
                //    string LSIDNewText = infrastructure.LSIDNew != null ? infrastructure.LSIDNew.ToString() : "---";
                //    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>LSID New:</b> {LSIDNewText}");
                //}
                //sbKML.AppendLine($@"</p>");

                //// doing SiteID
                //string SiteIDText = infrastructure.SiteID != null ? infrastructure.SiteID.ToString() : "---";
                //sbKML.Append($@"                <p><b>SiteID:</b> {SiteIDText}");
                //if (infrastructure.SiteIDNew != null)
                //{
                //    string SiteIDNewText = infrastructure.SiteIDNew != null ? infrastructure.SiteIDNew.ToString() : "---";
                //    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>SiteID New:</b> {SiteIDNewText}");
                //}
                //sbKML.AppendLine($@"</p>");

                //// doing Site
                //string SiteText = infrastructure.Site != null ? infrastructure.Site.ToString() : "---";
                //sbKML.Append($@"                <p><b>Site:</b> {SiteText}");
                //if (infrastructure.SiteNew != null)
                //{
                //    string SiteNewText = infrastructure.SiteNew != null ? infrastructure.SiteNew.ToString() : "---";
                //    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Site New:</b> {SiteNewText}");
                //}
                //sbKML.AppendLine($@"</p>");

                //// doing InfrastructureCategory
                //string InfrastructureCategoryText = infrastructure.InfrastructureCategory != null ? infrastructure.InfrastructureCategory.ToString() : "---";
                //sbKML.Append($@"                <p><b>InfrastructureCategory:</b> {InfrastructureCategoryText}");
                //if (infrastructure.InfrastructureCategoryNew != null)
                //{
                //    string InfrastructureCategoryNewText = infrastructure.InfrastructureCategoryNew != null ? infrastructure.InfrastructureCategoryNew.ToString() : "---";
                //    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>InfrastructureCategory New:</b> {InfrastructureCategoryNewText}");
                //}
                //sbKML.AppendLine($@"</p>");

                // doing InfrastructureType
                string InfrastructureTypeText = infrastructure.InfrastructureType != null ? _BaseEnumService.GetEnumText_InfrastructureTypeEnum((InfrastructureTypeEnum)infrastructure.InfrastructureType) : "---";
                sbKML.Append($@"                <p><b>Infrastructure Type:</b> {InfrastructureTypeText}");
                if (infrastructure.InfrastructureTypeNew != null)
                {
                    string InfrastructureTypeNewText = infrastructure.InfrastructureTypeNew != null ? _BaseEnumService.GetEnumText_InfrastructureTypeEnum((InfrastructureTypeEnum)infrastructure.InfrastructureTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Infrastructure Type New:</b> {InfrastructureTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing FacilityType
                string FacilityTypeText = infrastructure.FacilityType != null ? _BaseEnumService.GetEnumText_FacilityTypeEnum((FacilityTypeEnum)infrastructure.FacilityType) : "---";
                sbKML.Append($@"                <p><b>Facility Type:</b> {FacilityTypeText}");
                if (infrastructure.FacilityTypeNew != null)
                {
                    string FacilityTypeNewText = infrastructure.FacilityTypeNew != null ? _BaseEnumService.GetEnumText_FacilityTypeEnum((FacilityTypeEnum)infrastructure.FacilityTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Facility Type New:</b> {FacilityTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing IsMechanicallyAerated
                string IsMechanicallyAerated = infrastructure.IsMechanicallyAerated != null && infrastructure.IsMechanicallyAerated == true ? "true" : "false";
                sbKML.Append($@"                <p><b>Is Mechanically Aerated:</b> {IsMechanicallyAerated}</p>");
                if (infrastructure.IsMechanicallyAeratedNew != null)
                {
                    string IsMechanicallyAeratedNew = infrastructure.IsMechanicallyAeratedNew == true ? "true" : "false";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>Is Mechanically Aerated New:</b> {IsMechanicallyAeratedNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing NumberOfCells
                string NumberOfCells = infrastructure.NumberOfCells != null ? infrastructure.NumberOfCells.ToString() : "---";
                sbKML.Append($@"                <p><b>Number Of Cells:</b> {NumberOfCells}</p>");
                if (infrastructure.NumberOfCellsNew != null)
                {
                    string NumberOfCellsNew = infrastructure.NumberOfCellsNew != null ? infrastructure.NumberOfCellsNew.ToString() : "---";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>Number Of Cells New:</b> {NumberOfCellsNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing NumberOfAeratedCells
                string NumberOfAeratedCells = infrastructure.NumberOfAeratedCells != null ? infrastructure.NumberOfAeratedCells.ToString() : "---";
                sbKML.Append($@"                <p><b>Number Of Aerated Cells:</b> {NumberOfAeratedCells}</p>");
                if (infrastructure.NumberOfAeratedCellsNew != null)
                {
                    string NumberOfAeratedCellsNew = infrastructure.NumberOfAeratedCellsNew != null ? infrastructure.NumberOfAeratedCellsNew.ToString() : "---";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>Number Of Aerated Cells New:</b> {NumberOfAeratedCellsNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing AerationType
                string AerationTypeText = infrastructure.AerationType != null ? _BaseEnumService.GetEnumText_AerationTypeEnum((AerationTypeEnum)infrastructure.AerationType) : "---";
                sbKML.Append($@"                <p><b>Aeration Type:</b> {AerationTypeText}");
                if (infrastructure.AerationTypeNew != null)
                {
                    string AerationTypeNewText = infrastructure.AerationTypeNew != null ? _BaseEnumService.GetEnumText_AerationTypeEnum((AerationTypeEnum)infrastructure.AerationTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Aeration Type New:</b> {AerationTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PreliminaryTreatmentType
                string PreliminaryTreatmentTypeText = infrastructure.PreliminaryTreatmentType != null ? _BaseEnumService.GetEnumText_PreliminaryTreatmentTypeEnum((PreliminaryTreatmentTypeEnum)infrastructure.PreliminaryTreatmentType) : "---";
                sbKML.Append($@"                <p><b>Preliminary Treatment Type:</b> {PreliminaryTreatmentTypeText}");
                if (infrastructure.PreliminaryTreatmentTypeNew != null)
                {
                    string PreliminaryTreatmentTypeNewText = infrastructure.PreliminaryTreatmentTypeNew != null ? _BaseEnumService.GetEnumText_PreliminaryTreatmentTypeEnum((PreliminaryTreatmentTypeEnum)infrastructure.PreliminaryTreatmentTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Preliminary Treatment Type New:</b> {PreliminaryTreatmentTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PrimaryTreatmentType
                string PrimaryTreatmentTypeText = infrastructure.PrimaryTreatmentType != null ? _BaseEnumService.GetEnumText_PrimaryTreatmentTypeEnum((PrimaryTreatmentTypeEnum)infrastructure.PrimaryTreatmentType) : "---";
                sbKML.Append($@"                <p><b>Primary Treatment Type:</b> {PrimaryTreatmentTypeText}");
                if (infrastructure.PrimaryTreatmentTypeNew != null)
                {
                    string PrimaryTreatmentTypeNewText = infrastructure.PrimaryTreatmentTypeNew != null ? _BaseEnumService.GetEnumText_PrimaryTreatmentTypeEnum((PrimaryTreatmentTypeEnum)infrastructure.PrimaryTreatmentTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Primary Treatment Type New:</b> {PrimaryTreatmentTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing SecondaryTreatmentType
                string SecondaryTreatmentTypeText = infrastructure.SecondaryTreatmentType != null ? _BaseEnumService.GetEnumText_SecondaryTreatmentTypeEnum((SecondaryTreatmentTypeEnum)infrastructure.SecondaryTreatmentType) : "---";
                sbKML.Append($@"                <p><b>Secondary Treatment Type:</b> {SecondaryTreatmentTypeText}");
                if (infrastructure.SecondaryTreatmentTypeNew != null)
                {
                    string SecondaryTreatmentTypeNewText = infrastructure.SecondaryTreatmentTypeNew != null ? _BaseEnumService.GetEnumText_SecondaryTreatmentTypeEnum((SecondaryTreatmentTypeEnum)infrastructure.SecondaryTreatmentTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Secondary Treatment Type New:</b> {SecondaryTreatmentTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing TertiaryTreatmentType
                string TertiaryTreatmentTypeText = infrastructure.TertiaryTreatmentType != null ? _BaseEnumService.GetEnumText_TertiaryTreatmentTypeEnum((TertiaryTreatmentTypeEnum)infrastructure.TertiaryTreatmentType) : "---";
                sbKML.Append($@"                <p><b>Tertiary Treatment Type:</b> {TertiaryTreatmentTypeText}");
                if (infrastructure.TertiaryTreatmentTypeNew != null)
                {
                    string TertiaryTreatmentTypeNewText = infrastructure.TertiaryTreatmentTypeNew != null ? _BaseEnumService.GetEnumText_TertiaryTreatmentTypeEnum((TertiaryTreatmentTypeEnum)infrastructure.TertiaryTreatmentTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Tertiary Treatment Type New:</b> {TertiaryTreatmentTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing DisinfectionType
                string DisinfectionTypeText = infrastructure.DisinfectionType != null ? _BaseEnumService.GetEnumText_DisinfectionTypeEnum((DisinfectionTypeEnum)infrastructure.DisinfectionType) : "---";
                sbKML.Append($@"                <p><b>Disinfection Type:</b> {DisinfectionTypeText}");
                if (infrastructure.DisinfectionTypeNew != null)
                {
                    string DisinfectionTypeNewText = infrastructure.DisinfectionTypeNew != null ? _BaseEnumService.GetEnumText_DisinfectionTypeEnum((DisinfectionTypeEnum)infrastructure.DisinfectionTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Disinfection Type New:</b> {DisinfectionTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing CollectionSystemType
                string CollectionSystemTypeText = infrastructure.CollectionSystemType != null ? _BaseEnumService.GetEnumText_CollectionSystemTypeEnum((CollectionSystemTypeEnum)infrastructure.CollectionSystemType) : "---";
                sbKML.Append($@"                <p><b>Collection System Type:</b> {CollectionSystemTypeText}");
                if (infrastructure.CollectionSystemTypeNew != null)
                {
                    string CollectionSystemTypeNewText = infrastructure.CollectionSystemTypeNew != null ? _BaseEnumService.GetEnumText_CollectionSystemTypeEnum((CollectionSystemTypeEnum)infrastructure.CollectionSystemTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Collection System Type New:</b> {CollectionSystemTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing AlarmSystemType
                string AlarmSystemTypeText = infrastructure.AlarmSystemType != null ? _BaseEnumService.GetEnumText_AlarmSystemTypeEnum((AlarmSystemTypeEnum)infrastructure.AlarmSystemType) : "---";
                sbKML.Append($@"                <p><b>Alarm System Type:</b> {AlarmSystemTypeText}");
                if (infrastructure.AlarmSystemTypeNew != null)
                {
                    string AlarmSystemTypeNewText = infrastructure.AlarmSystemTypeNew != null ? _BaseEnumService.GetEnumText_AlarmSystemTypeEnum((AlarmSystemTypeEnum)infrastructure.AlarmSystemTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Alarm System Type New:</b> {AlarmSystemTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing DesignFlow_m3_day
                string DesignFlow_m3_dayText = infrastructure.DesignFlow_m3_day != null ? ((float)infrastructure.DesignFlow_m3_day).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Design Flow (m3/day):</b> {DesignFlow_m3_dayText}");
                if (infrastructure.DesignFlow_m3_dayNew != null)
                {
                    string DesignFlow_m3_dayNewText = infrastructure.DesignFlow_m3_dayNew != null ? ((float)infrastructure.DesignFlow_m3_dayNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Design Flow (m3/day) New:</b> {DesignFlow_m3_dayNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing AverageFlow_m3_day
                string AverageFlow_m3_dayText = infrastructure.AverageFlow_m3_day != null ? ((float)infrastructure.AverageFlow_m3_day).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Average Flow (m3/day):</b> {AverageFlow_m3_dayText}");
                if (infrastructure.AverageFlow_m3_dayNew != null)
                {
                    string AverageFlow_m3_dayNewText = infrastructure.AverageFlow_m3_dayNew != null ? ((float)infrastructure.AverageFlow_m3_dayNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Average Flow (m3/day) New:</b> {AverageFlow_m3_dayNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PeakFlow_m3_day
                string PeakFlow_m3_dayText = infrastructure.PeakFlow_m3_day != null ? ((float)infrastructure.PeakFlow_m3_day).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Peak Flow (m3/day):</b> {PeakFlow_m3_dayText}");
                if (infrastructure.PeakFlow_m3_dayNew != null)
                {
                    string PeakFlow_m3_dayNewText = infrastructure.PeakFlow_m3_dayNew != null ? ((float)infrastructure.PeakFlow_m3_dayNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Peak Flow (m3/day) New:</b> {PeakFlow_m3_dayNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PopServed
                string PopServedText = infrastructure.PopServed != null ? ((int)infrastructure.PopServed).ToString() : "---";
                sbKML.Append($@"                <p><b>Pop Served:</b> {PopServedText}");
                if (infrastructure.PopServedNew != null)
                {
                    string PopServedNewText = infrastructure.PopServedNew != null ? ((int)infrastructure.PopServedNew).ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Pop Served New:</b> {PopServedNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing CanOverflow
                string CanOverflow = infrastructure.CanOverflow != null && infrastructure.CanOverflow == (int)CanOverflowTypeEnum.Yes ? "true" : "false";
                sbKML.Append($@"                <p><b>Can Over flow:</b> {CanOverflow}</p>");
                if (infrastructure.CanOverflowNew != null)
                {
                    string CanOverflowNew = infrastructure.CanOverflowNew == (int)CanOverflowTypeEnum.Yes ? "true" : "false";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>Can Overflow New:</b> {CanOverflowNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing ValveType
                string ValveTypeText = infrastructure.ValveType != null ? _BaseEnumService.GetEnumText_ValveTypeEnum((ValveTypeEnum)infrastructure.ValveType) : "---";
                sbKML.Append($@"                <p><b>Valve Type:</b> {ValveTypeText}");
                if (infrastructure.ValveTypeNew != null)
                {
                    string ValveTypeNewText = infrastructure.ValveTypeNew != null ? _BaseEnumService.GetEnumText_ValveTypeEnum((ValveTypeEnum)infrastructure.ValveTypeNew) : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Valve Type New:</b> {ValveTypeNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing HasBackupPower
                string HasBackupPower = infrastructure.HasBackupPower != null && infrastructure.HasBackupPower == true ? "true" : "false";
                sbKML.Append($@"                <p><b>Has Backup Power:</b> {HasBackupPower}</p>");
                if (infrastructure.HasBackupPowerNew != null)
                {
                    string HasBackupPowerNew = infrastructure.HasBackupPowerNew == true ? "true" : "false";
                    sbKML.Append($@"                &nbsp;&nbsp;&nbsp;&nbsp;<b>Has Backup Power New:</b> {HasBackupPowerNew}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PercFlowOfTotal
                string PercFlowOfTotalText = infrastructure.PercFlowOfTotal != null ? ((float)infrastructure.PercFlowOfTotal).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Percentage Flow Of Total:</b> {PercFlowOfTotalText} %");
                if (infrastructure.PercFlowOfTotalNew != null)
                {
                    string PercFlowOfTotalNewText = infrastructure.PercFlowOfTotalNew != null ? ((float)infrastructure.PercFlowOfTotalNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Percentage Flow Of Total New:</b> {PercFlowOfTotalNewText} %");
                }
                sbKML.AppendLine($@"</p>");

                // doing AverageDepth_m
                string AverageDepth_mText = infrastructure.AverageDepth_m != null ? ((float)infrastructure.AverageDepth_m).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Average Depth (m):</b> {AverageDepth_mText}");
                if (infrastructure.AverageDepth_mNew != null)
                {
                    string AverageDepth_mNewText = infrastructure.AverageDepth_mNew != null ? ((float)infrastructure.AverageDepth_mNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Average Depth (m) New:</b> {AverageDepth_mNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing NumberOfPorts
                string NumberOfPortsText = infrastructure.NumberOfPorts != null ? ((int)infrastructure.NumberOfPorts).ToString() : "---";
                sbKML.Append($@"                <p><b>Number Of Ports:</b> {NumberOfPortsText}");
                if (infrastructure.NumberOfPortsNew != null)
                {
                    string NumberOfPortsNewText = infrastructure.NumberOfPortsNew != null ? ((int)infrastructure.NumberOfPortsNew).ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Number Of Ports New:</b> {NumberOfPortsNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PortDiameter_m
                string PortDiameter_mText = infrastructure.PortDiameter_m != null ? ((float)infrastructure.PortDiameter_m).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Port Diameter (m):</b> {PortDiameter_mText}");
                if (infrastructure.PortDiameter_mNew != null)
                {
                    string PortDiameter_mNewText = infrastructure.PortDiameter_mNew != null ? ((float)infrastructure.PortDiameter_mNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Port Diameter (m) New:</b> {PortDiameter_mNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PortSpacing_m
                string PortSpacing_mText = infrastructure.PortSpacing_m != null ? ((float)infrastructure.PortSpacing_m).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>PortSpacing (m):</b> {PortSpacing_mText}");
                if (infrastructure.PortSpacing_mNew != null)
                {
                    string PortSpacing_mNewText = infrastructure.PortSpacing_mNew != null ? ((float)infrastructure.PortSpacing_mNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Port Spacing (m) New:</b> {PortSpacing_mNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PortElevation_m
                string PortElevation_mText = infrastructure.PortElevation_m != null ? ((float)infrastructure.PortElevation_m).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Port Elevation (m):</b> {PortElevation_mText}");
                if (infrastructure.PortElevation_mNew != null)
                {
                    string PortElevation_mNewText = infrastructure.PortElevation_mNew != null ? ((float)infrastructure.PortElevation_mNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Port Elevation (m) New:</b> {PortElevation_mNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing VerticalAngle_deg
                string VerticalAngle_degText = infrastructure.VerticalAngle_deg != null ? ((float)infrastructure.VerticalAngle_deg).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Vertical Angle (deg):</b> {VerticalAngle_degText}");
                if (infrastructure.VerticalAngle_degNew != null)
                {
                    string VerticalAngle_degNewText = infrastructure.VerticalAngle_degNew != null ? ((float)infrastructure.VerticalAngle_degNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Vertical Angle (deg) New:</b> {VerticalAngle_degNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing HorizontalAngle_deg
                string HorizontalAngle_degText = infrastructure.HorizontalAngle_deg != null ? ((float)infrastructure.HorizontalAngle_deg).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Horizontal Angle (deg):</b> {HorizontalAngle_degText}");
                if (infrastructure.HorizontalAngle_degNew != null)
                {
                    string HorizontalAngle_degNewText = infrastructure.HorizontalAngle_degNew != null ? ((float)infrastructure.HorizontalAngle_degNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Horizontal Angle (deg) New:</b> {HorizontalAngle_degNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing DecayRate_per_day
                string DecayRate_per_dayText = infrastructure.DecayRate_per_day != null ? ((float)infrastructure.DecayRate_per_day).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Decay Rate (/day):</b> {DecayRate_per_dayText}");
                if (infrastructure.DecayRate_per_dayNew != null)
                {
                    string DecayRate_per_dayNewText = infrastructure.DecayRate_per_dayNew != null ? ((float)infrastructure.DecayRate_per_dayNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Decay Rate (/day) New:</b> {DecayRate_per_dayNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing NearFieldVelocity_m_s
                string NearFieldVelocity_m_sText = infrastructure.NearFieldVelocity_m_s != null ? ((float)infrastructure.NearFieldVelocity_m_s).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Near Field Velocity (m/s):</b> {NearFieldVelocity_m_sText}");
                if (infrastructure.NearFieldVelocity_m_sNew != null)
                {
                    string NearFieldVelocity_m_sNewText = infrastructure.NearFieldVelocity_m_sNew != null ? ((float)infrastructure.NearFieldVelocity_m_sNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Near Field Velocity (m/s) New:</b> {NearFieldVelocity_m_sNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing FarFieldVelocity_m_s
                string FarFieldVelocity_m_sText = infrastructure.FarFieldVelocity_m_s != null ? ((float)infrastructure.FarFieldVelocity_m_s).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Far Field Velocity (m/s):</b> {FarFieldVelocity_m_sText}");
                if (infrastructure.FarFieldVelocity_m_sNew != null)
                {
                    string FarFieldVelocity_m_sNewText = infrastructure.FarFieldVelocity_m_sNew != null ? ((float)infrastructure.FarFieldVelocity_m_sNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Far Field Velocity (m/s) New:</b> {FarFieldVelocity_m_sNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing ReceivingWaterSalinity_PSU
                string ReceivingWaterSalinity_PSUText = infrastructure.ReceivingWaterSalinity_PSU != null ? ((float)infrastructure.ReceivingWaterSalinity_PSU).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>ReceivingWater Salinity (PSU):</b> {ReceivingWaterSalinity_PSUText}");
                if (infrastructure.ReceivingWaterSalinity_PSUNew != null)
                {
                    string ReceivingWaterSalinity_PSUNewText = infrastructure.ReceivingWaterSalinity_PSUNew != null ? ((float)infrastructure.ReceivingWaterSalinity_PSUNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Receiving Water Salinity (PSU) New:</b> {ReceivingWaterSalinity_PSUNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing ReceivingWaterTemperature_C
                string ReceivingWaterTemperature_CText = infrastructure.ReceivingWaterTemperature_C != null ? ((float)infrastructure.ReceivingWaterTemperature_C).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Receiving Water Temperature (C):</b> {ReceivingWaterTemperature_CText}");
                if (infrastructure.ReceivingWaterTemperature_CNew != null)
                {
                    string ReceivingWaterTemperature_CNewText = infrastructure.ReceivingWaterTemperature_CNew != null ? ((float)infrastructure.ReceivingWaterTemperature_CNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Receiving Water Temperature (C) New:</b> {ReceivingWaterTemperature_CNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing ReceivingWater_MPN_per_100ml
                string ReceivingWater_MPN_per_100mlText = infrastructure.ReceivingWater_MPN_per_100ml != null ? ((int)infrastructure.ReceivingWater_MPN_per_100ml).ToString() : "---";
                sbKML.Append($@"                <p><b>Receiving Water (MPN / 100mL):</b> {ReceivingWater_MPN_per_100mlText}");
                if (infrastructure.ReceivingWater_MPN_per_100mlNew != null)
                {
                    string ReceivingWater_MPN_per_100mlNewText = infrastructure.ReceivingWater_MPN_per_100mlNew != null ? ((int)infrastructure.ReceivingWater_MPN_per_100mlNew).ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Receiving Water (MPN / 100mL) New:</b> {ReceivingWater_MPN_per_100mlNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing DistanceFromShore_m
                string DistanceFromShore_mText = infrastructure.DistanceFromShore_m != null ? ((float)infrastructure.DistanceFromShore_m).ToString("F1") : "---";
                sbKML.Append($@"                <p><b>Distance From Shore (m):</b> {DistanceFromShore_mText}");
                if (infrastructure.DistanceFromShore_mNew != null)
                {
                    string DistanceFromShore_mNewText = infrastructure.DistanceFromShore_mNew != null ? ((float)infrastructure.DistanceFromShore_mNew).ToString("F1") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Distance From Shore (m) New:</b> {DistanceFromShore_mNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing SeeOtherMunicipalityTVItemID
                string SeeOtherMunicipalityTVItemIDText = infrastructure.SeeOtherMunicipalityTVItemID != null ? ((int)infrastructure.SeeOtherMunicipalityTVItemID).ToString() : "---";
                sbKML.Append($@"                <p><b>See Other Municipality TVItemID:</b> {SeeOtherMunicipalityTVItemIDText}");
                if (infrastructure.SeeOtherMunicipalityTVItemIDNew != null)
                {
                    string SeeOtherMunicipalityTVItemIDNewText = infrastructure.SeeOtherMunicipalityTVItemIDNew != null ? ((int)infrastructure.SeeOtherMunicipalityTVItemIDNew).ToString() : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>See Other Municipality TVItemID New:</b> {SeeOtherMunicipalityTVItemIDNewText}");
                }
                sbKML.AppendLine($@"</p>");

                // doing PumpsToTVItemID
                Infrastructure infPumpTo = new Infrastructure();
                if (infrastructure.PumpsToTVItemID != null)
                {
                    infPumpTo = municipalityDoc.Municipality.InfrastructureList.Where(c => c.InfrastructureTVItemID == infrastructure.PumpsToTVItemID).FirstOrDefault();
                }
                string PumpsToTVItemIDText = infrastructure.PumpsToTVItemID != null ? (infPumpTo != null ? (infPumpTo.TVTextNew != null ? infPumpTo.TVTextNew : infPumpTo.TVText) : "---") : "---";
                sbKML.Append($@"                <p><b>Pumps To TVItemID:</b> {PumpsToTVItemIDText}");
                if (infrastructure.PumpsToTVItemIDNew != null)
                {
                    Infrastructure infPumpToNew = new Infrastructure();
                    if (infrastructure.PumpsToTVItemIDNew != null)
                    {
                        infPumpToNew = municipalityDoc.Municipality.InfrastructureList.Where(c => c.InfrastructureTVItemID == infrastructure.PumpsToTVItemIDNew).FirstOrDefault();
                    }
                    string PumpsToTVItemIDNewText = infrastructure.PumpsToTVItemIDNew != null ? (infPumpToNew != null ? (infPumpToNew.TVTextNew != null ? infPumpToNew.TVTextNew : infPumpToNew.TVText) : "---") : "---";
                    sbKML.Append($@"                nbsp;nbsp;nbsp;<b>Pumps To TVItemID New:</b> {PumpsToTVItemIDNewText}");
                }
                sbKML.AppendLine($@"</p>");



                sbKML.AppendLine($@"                </blockquote>");


                if (infrastructure.InfrastructurePictureList.Count > 0)
                {
                    sbKML.AppendLine($@"                <h3>Images</h3>");
                    sbKML.AppendLine($@"                <ul>");
                    foreach (Picture picture in infrastructure.InfrastructurePictureList)
                    {
                        string url = @"file:///C:\PollutionSourceSites\Infrastructures\" + CurrentMunicipalityName + @"\Pictures\" + infrastructure.InfrastructureTVItemID + "_" + picture.PictureTVItemID + ".jpg";

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

                sbKML.AppendLine($@"        </Folder>");
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
                            string url = @"file:///C:\PollutionSourceSites\Subsectors\" + CurrentSubsectorName + @"\Pictures\" + pss.SiteNumberText + "_" + picture.PictureTVItemID + ".jpg";

                            sbKML.AppendLine($@"                <li><img style=""max-width:600px;"" src=""{url}"" /></li>");
                        }
                        sbKML.AppendLine($@"                </ul>");
                    }
                }

                sbKML.AppendLine($@"            ]]>");
                sbKML.AppendLine($@"            </description>");
                sbKML.AppendLine($@"			<styleUrl>#m_ylw-pushpin</styleUrl>");
                sbKML.AppendLine($@"			<Point>");
                if (pss.LatNew != null && pss.LngNew != null)
                {
                    sbKML.AppendLine($@"				<coordinates>{pss.LngNew},{pss.LatNew},0</coordinates>");
                }
                else
                {
                    sbKML.AppendLine($@"				<coordinates>{pss.Lng},{pss.Lat},0</coordinates>");
                }
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
