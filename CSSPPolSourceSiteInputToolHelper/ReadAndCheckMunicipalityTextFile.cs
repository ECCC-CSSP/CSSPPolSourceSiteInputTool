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
                if (LineTxt.StartsWith("COMMENTFR"))
                {
                    int selfj = 2;
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
                    case "PROVINCETVITEMID":
                        {
                            try
                            {
                                municipalityDoc.ProvinceTVItemID = int.Parse(LineTxt.Substring("PROVINCETVITEMID\t".Length));
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
                                municipalityDoc.MunicipalityIDNumberList = MunicipalityAndIDList;
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
                    case "MUNICIPALITYLATLNG":
                        {
                            try
                            {
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    municipalityDoc.Municipality.Lat = null;
                                }
                                else
                                {
                                    municipalityDoc.Municipality.Lat = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    municipalityDoc.Municipality.Lng = null;
                                }
                                else
                                {
                                    municipalityDoc.Municipality.Lng = float.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "CONTACT":
                        {
                            try
                            {
                                Contact contact = new Contact();

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    contact.FirstName = null;
                                }
                                else
                                {
                                    contact.FirstName = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    contact.Initial = null;
                                }
                                else
                                {
                                    contact.Initial = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1)))
                                {
                                    contact.LastName = null;
                                }
                                else
                                {
                                    contact.LastName = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                }

                                municipalityDoc.Municipality.ContactList.Add(contact);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "CONTACTNEW":
                        {
                            try
                            {
                                Contact lastContact = municipalityDoc.Municipality.ContactList[municipalityDoc.Municipality.ContactList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastContact.FirstNameNew = null;
                                }
                                else
                                {
                                    lastContact.FirstNameNew = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    lastContact.InitialNew = null;
                                }
                                else
                                {
                                    lastContact.InitialNew = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1)))
                                {
                                    lastContact.LastNameNew = null;
                                }
                                else
                                {
                                    lastContact.LastNameNew = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "CONTACTTELEPHONE":
                        {
                            try
                            {
                                Telephone telephone = new Telephone();
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    telephone.TelTVItemID = null;
                                }
                                else
                                {
                                    telephone.TelTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    telephone.TelType = null;
                                }
                                else
                                {
                                    telephone.TelType = int.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1)))
                                {
                                    telephone.TelNumber = null;
                                }
                                else
                                {
                                    telephone.TelNumber = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                }

                                Contact lastContact = municipalityDoc.Municipality.ContactList[municipalityDoc.Municipality.ContactList.Count - 1];

                                lastContact.TelephoneList.Add(telephone);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "CONTACTTELEPHONENEW":
                        {
                            try
                            {
                                Contact lastContact = municipalityDoc.Municipality.ContactList[municipalityDoc.Municipality.ContactList.Count - 1];
                                Telephone lastTelephone = lastContact.TelephoneList[lastContact.TelephoneList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastTelephone.TelTVItemID = null;
                                }
                                else
                                {
                                    lastTelephone.TelTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    lastTelephone.TelTypeNew = null;
                                }
                                else
                                {
                                    lastTelephone.TelTypeNew = int.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1)))
                                {
                                    lastTelephone.TelNumberNew = null;
                                }
                                else
                                {
                                    lastTelephone.TelNumberNew = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "CONTACTEMAIL":
                        {
                            try
                            {
                                Email email = new Email();
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    email.EmailTVItemID = null;
                                }
                                else
                                {
                                    email.EmailTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    email.EmailType = null;
                                }
                                else
                                {
                                    email.EmailType = int.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1)))
                                {
                                    email.EmailAddress = null;
                                }
                                else
                                {
                                    email.EmailAddress = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                }

                                Contact lastContact = municipalityDoc.Municipality.ContactList[municipalityDoc.Municipality.ContactList.Count - 1];

                                lastContact.EmailList.Add(email);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "CONTACTEMAILNEW":
                        {
                            try
                            {
                                Contact lastContact = municipalityDoc.Municipality.ContactList[municipalityDoc.Municipality.ContactList.Count - 1];
                                Email lastEmail = lastContact.EmailList[lastContact.EmailList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastEmail.EmailTVItemID = null;
                                }
                                else
                                {
                                    lastEmail.EmailTVItemID = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    lastEmail.EmailTypeNew = null;
                                }
                                else
                                {
                                    lastEmail.EmailTypeNew = int.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos3 + 1, pos4 - pos - 1)))
                                {
                                    lastEmail.EmailAddressNew = null;
                                }
                                else
                                {
                                    lastEmail.EmailAddressNew = LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1);
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "CONTACTADDRESS":
                        {
                            try
                            {
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

                                Contact lastContact = municipalityDoc.Municipality.ContactList[municipalityDoc.Municipality.ContactList.Count - 1];
                                lastContact.ContactAddress = address;

                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "CONTACTADDRESSNEW":
                        {
                            try
                            {
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
                                if (string.IsNullOrWhiteSpace(address.Municipality))
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

                                Contact lastContact = municipalityDoc.Municipality.ContactList[municipalityDoc.Municipality.ContactList.Count - 1];
                                lastContact.ContactAddressNew = address;
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

                                infrastructure.IsActive = bool.Parse(LineTxt.Substring(pos3 + 1, pos4 - pos3 - 1));
                                municipalityDoc.Municipality.InfrastructureList.Add(infrastructure);
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LATLNG":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.Lat = null;
                                }
                                else
                                {
                                    lastInfrastructure.Lat = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    lastInfrastructure.Lng = null;
                                }
                                else
                                {
                                    lastInfrastructure.Lng = float.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LATLNGOUTFALL":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.LatOutfall = null;
                                }
                                else
                                {
                                    lastInfrastructure.LatOutfall = float.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    lastInfrastructure.LngOutfall = null;
                                }
                                else
                                {
                                    lastInfrastructure.LngOutfall = float.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LATLNGNEW":
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
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    lastInfrastructure.LngNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.LngNew = float.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "LATLNGOUTFALLNEW":
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
                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    lastInfrastructure.LngOutfallNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.LngOutfallNew = float.Parse(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1));
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
                    case "COMMENTEN":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.CommentEN = LineTxt.Substring(pos + 1, pos2 - pos - 1).Replace("|||", "\r\n");
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "COMMENTENNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.CommentENNew = LineTxt.Substring(pos + 1, pos2 - pos - 1).Replace("|||", "\r\n");
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "COMMENTFR":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.CommentFR = LineTxt.Substring(pos + 1, pos2 - pos - 1).Replace("|||", "\r\n");
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "COMMENTFRNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                lastInfrastructure.CommentFRNew = LineTxt.Substring(pos + 1, pos2 - pos - 1).Replace("|||", "\r\n");
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
                    case "VALVETYPE":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.ValveType = null;
                                }
                                else
                                {
                                    lastInfrastructure.ValveType = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "VALVETYPENEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.ValveTypeNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.ValveTypeNew = int.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "HASBACKUPPOWER":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.HasBackupPower = null;
                                }
                                else
                                {
                                    lastInfrastructure.HasBackupPower = bool.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "HASBACKUPPOWERNEW":
                        {
                            try
                            {
                                Infrastructure lastInfrastructure = municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1];

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos + 1, pos2 - pos - 1)))
                                {
                                    lastInfrastructure.HasBackupPowerNew = null;
                                }
                                else
                                {
                                    lastInfrastructure.HasBackupPowerNew = bool.Parse(LineTxt.Substring(pos + 1, pos2 - pos - 1));
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
                    case "SEEOTHERMUNICIPALITY":
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

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    lastInfrastructure.SeeOtherMunicipalityText = "";
                                }
                                else
                                {
                                    lastInfrastructure.SeeOtherMunicipalityText = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "SEEOTHERMUNICIPALITYNEW":
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

                                if (string.IsNullOrWhiteSpace(LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1)))
                                {
                                    lastInfrastructure.SeeOtherMunicipalityTextNew = "";
                                }
                                else
                                {
                                    lastInfrastructure.SeeOtherMunicipalityTextNew = LineTxt.Substring(pos2 + 1, pos3 - pos2 - 1);
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
                                if (string.IsNullOrWhiteSpace(address.Municipality))
                                {
                                    address.Municipality = null;
                                }
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
                                if (string.IsNullOrWhiteSpace(address.Municipality))
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
                    case "FROMWATER":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];
                                Picture lastPicture = lastPSS.PSSPictureList[lastPSS.PSSPictureList.Count - 1];

                                string FromWaterText = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                                if (!string.IsNullOrWhiteSpace(FromWaterText))
                                {
                                    lastPicture.FromWater = bool.Parse(FromWaterText);
                                }
                            }
                            catch (Exception)
                            {
                                EmitStatus(new StatusEventArgs($"Could not read { LineTxt.Substring(0, pos) } line at line { LineNumb }"));
                                return false;
                            }
                        }
                        break;
                    case "FROMWATERNEW":
                        {
                            try
                            {
                                PSS lastPSS = subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1];
                                Picture lastPicture = lastPSS.PSSPictureList[lastPSS.PSSPictureList.Count - 1];

                                string FromWaterText = LineTxt.Substring(pos + 1, pos2 - pos - 1);
                                if (!string.IsNullOrWhiteSpace(FromWaterText))
                                {
                                    lastPicture.FromWaterNew = bool.Parse(FromWaterText);
                                }
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
