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
        private void DrawItemAddress(int x, int y, Address address, Address addressNew)
        {
            #region Address
            x = 10;

            int AddressPos = 0;
            #region StreetNumber
            // ---------------------------------------------------------------------------------------------------------------
            // start of StreetNumber
            // ---------------------------------------------------------------------------------------------------------------
            Label lblStreetNumberText = new Label();
            lblStreetNumberText.AutoSize = true;
            lblStreetNumberText.Location = new Point(x, y);
            lblStreetNumberText.Font = new Font(new FontFamily(lblStreetNumberText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblStreetNumberText.ForeColor = string.IsNullOrWhiteSpace(addressNew.StreetNumber) ? ForeColorNormal : ForeColorChangedOrNew;
            lblStreetNumberText.Text = $@"Street Number";

            PanelViewAndEdit.Controls.Add(lblStreetNumberText);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            if (IsEditing)
            {
                int currentTop = lblStreetNumberText.Bottom + 4;

                if (!string.IsNullOrWhiteSpace(addressNew.StreetNumber))
                {
                    Label lblStreetNumberOld = new Label();
                    lblStreetNumberOld.AutoSize = true;
                    lblStreetNumberOld.Location = new Point(lblStreetNumberText.Left, currentTop);
                    lblStreetNumberOld.Font = new Font(new FontFamily(lblStreetNumberOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetNumberOld.ForeColor = string.IsNullOrWhiteSpace(addressNew.StreetNumber) ? ForeColorNormal : ForeColorChangedOrNew;
                    lblStreetNumberOld.Text = string.IsNullOrWhiteSpace(address.StreetNumber) ? "(empty)" : $"({address.StreetNumber})";

                    PanelViewAndEdit.Controls.Add(lblStreetNumberOld);

                    currentTop = lblStreetNumberOld.Bottom + 4;

                }

                TextBox textBoxStreetNumber = new TextBox();
                textBoxStreetNumber.Location = new Point(lblStreetNumberText.Left, currentTop);
                textBoxStreetNumber.Name = "textBoxStreetNumber";
                textBoxStreetNumber.Font = new Font(new FontFamily(lblStreetNumberText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textBoxStreetNumber.Width = lblStreetNumberText.Width;
                if (string.IsNullOrWhiteSpace(addressNew.StreetNumber))
                {
                    textBoxStreetNumber.Text = address.StreetNumber;
                }
                else
                {
                    textBoxStreetNumber.Text = addressNew.StreetNumber;
                }

                PanelViewAndEdit.Controls.Add(textBoxStreetNumber);

                AddressPos = textBoxStreetNumber.Bottom + 10;
            }
            else
            {
                int currentTop = lblStreetNumberText.Bottom + 4;

                Label lblStreetNumber = new Label();
                lblStreetNumber.AutoSize = true;
                lblStreetNumber.Location = new Point(lblStreetNumberText.Left, currentTop);
                lblStreetNumber.Font = new Font(new FontFamily(lblStreetNumber.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblStreetNumber.ForeColor = string.IsNullOrWhiteSpace(addressNew.StreetNumber) ? ForeColorNormal : ForeColorChangedOrNew;
                lblStreetNumber.Text = string.IsNullOrWhiteSpace(address.StreetNumber) ? "(empty)" : $"({address.StreetNumber})";

                PanelViewAndEdit.Controls.Add(lblStreetNumber);

                currentTop = lblStreetNumber.Bottom + 4;

                AddressPos = lblStreetNumber.Bottom + 10;

                if (!string.IsNullOrWhiteSpace(addressNew.StreetNumber))
                {
                    Label lblStreetNumberNew = new Label();
                    lblStreetNumberNew.AutoSize = true;
                    lblStreetNumberNew.Location = new Point(lblStreetNumberText.Left, currentTop);
                    lblStreetNumberNew.Font = new Font(new FontFamily(lblStreetNumberNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetNumberNew.ForeColor = string.IsNullOrWhiteSpace(addressNew.StreetNumber) ? ForeColorNormal : ForeColorChangedOrNew;
                    lblStreetNumberNew.Text = string.IsNullOrWhiteSpace(addressNew.StreetNumber) ? "empty" : addressNew.StreetNumber;

                    PanelViewAndEdit.Controls.Add(lblStreetNumberNew);

                    currentTop = lblStreetNumberNew.Bottom + 4;

                    AddressPos = lblStreetNumberNew.Bottom + 10;

                }

            }


            // ---------------------------------------------------------------------------------------------------------------
            // end of StreetNumber
            // ---------------------------------------------------------------------------------------------------------------

            #endregion StreetNumber

            #region StreetName
            // ---------------------------------------------------------------------------------------------------------------
            // start of StreetName
            // ---------------------------------------------------------------------------------------------------------------
            Label lblStreetNameText = new Label();
            lblStreetNameText.AutoSize = true;
            lblStreetNameText.Location = new Point(x, lblStreetNumberText.Top);
            lblStreetNameText.Font = new Font(new FontFamily(lblStreetNameText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblStreetNameText.ForeColor = string.IsNullOrWhiteSpace(addressNew.StreetName) ? ForeColorNormal : ForeColorChangedOrNew;
            lblStreetNameText.Text = $@"Street Name    ";

            PanelViewAndEdit.Controls.Add(lblStreetNameText);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            if (IsEditing)
            {
                int currentTop = lblStreetNameText.Bottom + 4;

                if (!string.IsNullOrWhiteSpace(addressNew.StreetName))
                {
                    Label lblStreetNameOld = new Label();
                    lblStreetNameOld.AutoSize = true;
                    lblStreetNameOld.Location = new Point(lblStreetNameText.Left, currentTop);
                    lblStreetNameOld.Font = new Font(new FontFamily(lblStreetNameOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetNameOld.ForeColor = string.IsNullOrWhiteSpace(addressNew.StreetName) ? ForeColorNormal : ForeColorChangedOrNew;
                    lblStreetNameOld.Text = string.IsNullOrWhiteSpace(address.StreetName) ? "(empty)" : $"({address.StreetName})";

                    PanelViewAndEdit.Controls.Add(lblStreetNameOld);

                    currentTop = lblStreetNameOld.Bottom + 4;

                }

                TextBox textBoxStreetName = new TextBox();
                textBoxStreetName.Location = new Point(lblStreetNameText.Left, currentTop);
                textBoxStreetName.Name = "textBoxStreetName";
                textBoxStreetName.Font = new Font(new FontFamily(lblStreetNameText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textBoxStreetName.Width = lblStreetNameText.Width;
                if (string.IsNullOrWhiteSpace(addressNew.StreetName))
                {
                    textBoxStreetName.Text = address.StreetName;
                }
                else
                {
                    textBoxStreetName.Text = addressNew.StreetName;
                }

                PanelViewAndEdit.Controls.Add(textBoxStreetName);

            }
            else
            {
                int currentTop = lblStreetNameText.Bottom + 4;

                Label lblStreetName = new Label();
                lblStreetName.AutoSize = true;
                lblStreetName.Location = new Point(lblStreetNameText.Left, currentTop);
                lblStreetName.Font = new Font(new FontFamily(lblStreetName.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblStreetName.ForeColor = string.IsNullOrWhiteSpace(addressNew.StreetName) ? ForeColorNormal : ForeColorChangedOrNew;
                lblStreetName.Text = string.IsNullOrWhiteSpace(address.StreetName) ? "(empty)" : $"({address.StreetName})";

                PanelViewAndEdit.Controls.Add(lblStreetName);

                //y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                if (!string.IsNullOrWhiteSpace(addressNew.StreetName))
                {
                    currentTop = lblStreetName.Bottom + 4;

                    Label lblStreetNameNew = new Label();
                    lblStreetNameNew.AutoSize = true;
                    lblStreetNameNew.Location = new Point(lblStreetNameText.Left, lblStreetName.Bottom + 4);
                    lblStreetNameNew.Font = new Font(new FontFamily(lblStreetNameNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetNameNew.ForeColor = string.IsNullOrWhiteSpace(addressNew.StreetName) ? ForeColorNormal : ForeColorChangedOrNew;
                    lblStreetNameNew.Text = string.IsNullOrWhiteSpace(addressNew.StreetName) ? "empty" : addressNew.StreetName;

                    PanelViewAndEdit.Controls.Add(lblStreetNameNew);

                    //y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;
                }

            }


            // ---------------------------------------------------------------------------------------------------------------
            // end of StreetName
            // ---------------------------------------------------------------------------------------------------------------

            #endregion StreetName

            #region StreetType
            // ---------------------------------------------------------------------------------------------------------------
            // start of StreetType
            // ---------------------------------------------------------------------------------------------------------------
            Label lblStreetTypeText = new Label();
            lblStreetTypeText.AutoSize = true;
            lblStreetTypeText.Location = new Point(x, lblStreetNumberText.Top);
            lblStreetTypeText.Font = new Font(new FontFamily(lblStreetTypeText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblStreetTypeText.ForeColor = addressNew.StreetType != null ? ForeColorChangedOrNew : ForeColorNormal;
            lblStreetTypeText.Text = $@"Street Type   ";

            PanelViewAndEdit.Controls.Add(lblStreetTypeText);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            if (IsEditing)
            {
                int currentTop = lblStreetTypeText.Bottom + 4;

                if (addressNew.StreetType != null)
                {
                    Label lblStreetTypeOld = new Label();
                    lblStreetTypeOld.AutoSize = true;
                    lblStreetTypeOld.Location = new Point(lblStreetTypeText.Left, currentTop);
                    lblStreetTypeOld.Font = new Font(new FontFamily(lblStreetTypeOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetTypeOld.ForeColor = addressNew.StreetType != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblStreetTypeOld.Text = address.StreetType == null ? "(empty)" : $"({((StreetTypeEnum)address.StreetType).ToString()})";

                    PanelViewAndEdit.Controls.Add(lblStreetTypeOld);

                    currentTop = lblStreetTypeOld.Bottom + 4;

                }

                ComboBox comboBoxStreetType = new ComboBox();
                comboBoxStreetType.Location = new Point(lblStreetTypeText.Left, currentTop);
                comboBoxStreetType.Name = "comboBoxStreetType";
                comboBoxStreetType.Font = new Font(new FontFamily(lblStreetNameText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                comboBoxStreetType.Width = lblStreetTypeText.Width;

                PanelViewAndEdit.Controls.Add(comboBoxStreetType);

                for (int i = 1, count = Enum.GetNames(typeof(StreetTypeEnum)).Count(); i < count; i++)
                {
                    comboBoxStreetType.Items.Add(((StreetTypeEnum)i).ToString());
                }

                if (addressNew.StreetType != null)
                {
                    comboBoxStreetType.SelectedItem = ((StreetTypeEnum)addressNew.StreetType).ToString();
                }
                else
                {
                    if (address.StreetType != null)
                    {
                        comboBoxStreetType.SelectedItem = ((StreetTypeEnum)address.StreetType).ToString();
                    }
                }

                y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
            }
            else
            {
                int currentTop = lblStreetTypeText.Bottom + 4;

                Label lblStreetType = new Label();
                lblStreetType.AutoSize = true;
                lblStreetType.Location = new Point(lblStreetTypeText.Left, currentTop);
                lblStreetType.Font = new Font(new FontFamily(lblStreetType.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblStreetType.ForeColor = addressNew.StreetType != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblStreetType.Text = address.StreetType == null ? "(empty)" : $"({((StreetTypeEnum)address.StreetType).ToString()})";

                PanelViewAndEdit.Controls.Add(lblStreetType);

                //y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                if (addressNew.StreetType != null)
                {
                    currentTop = lblStreetType.Bottom + 4;

                    Label lblStreetTypeNew = new Label();
                    lblStreetTypeNew.AutoSize = true;
                    lblStreetTypeNew.Location = new Point(lblStreetTypeText.Left, lblStreetType.Bottom + 4);
                    lblStreetTypeNew.Font = new Font(new FontFamily(lblStreetTypeNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblStreetTypeNew.ForeColor = addressNew.StreetType != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblStreetTypeNew.Text = addressNew.StreetType == null ? "empty" : ((StreetTypeEnum)addressNew.StreetType).ToString();

                    PanelViewAndEdit.Controls.Add(lblStreetTypeNew);

                    //y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;
                }

            }

            //x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            // ---------------------------------------------------------------------------------------------------------------
            // end of StreetType
            // ---------------------------------------------------------------------------------------------------------------

            #endregion StreetType

            #region Municipality
            // ---------------------------------------------------------------------------------------------------------------
            // start of Municipality
            // ---------------------------------------------------------------------------------------------------------------
            Label lblMunicipalityText = new Label();
            lblMunicipalityText.AutoSize = true;
            lblMunicipalityText.Location = new Point(x, lblStreetNumberText.Top);
            lblMunicipalityText.Font = new Font(new FontFamily(lblMunicipalityText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblMunicipalityText.ForeColor = string.IsNullOrWhiteSpace(addressNew.Municipality) ? ForeColorNormal : ForeColorChangedOrNew;
            lblMunicipalityText.Text = $@"Municipality        ";

            PanelViewAndEdit.Controls.Add(lblMunicipalityText);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            if (IsEditing)
            {
                int currentTop = lblMunicipalityText.Bottom + 4;

                if (!string.IsNullOrWhiteSpace(addressNew.Municipality))
                {
                    Label lblMunicipalityOld = new Label();
                    lblMunicipalityOld.AutoSize = true;
                    lblMunicipalityOld.Location = new Point(lblMunicipalityText.Left, currentTop);
                    lblMunicipalityOld.Font = new Font(new FontFamily(lblMunicipalityOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblMunicipalityOld.ForeColor = string.IsNullOrWhiteSpace(addressNew.Municipality) ? ForeColorNormal : ForeColorChangedOrNew;
                    lblMunicipalityOld.Text = string.IsNullOrWhiteSpace(address.Municipality) ? "(empty)" : $"({address.Municipality})";

                    PanelViewAndEdit.Controls.Add(lblMunicipalityOld);

                    currentTop = lblMunicipalityOld.Bottom + 4;

                }

                TextBox textBoxMunicipality = new TextBox();
                textBoxMunicipality.Location = new Point(lblMunicipalityText.Left, currentTop);
                textBoxMunicipality.Name = "textBoxMunicipality";
                textBoxMunicipality.Font = new Font(new FontFamily(lblMunicipalityText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textBoxMunicipality.Width = lblMunicipalityText.Width;
                if (string.IsNullOrWhiteSpace(addressNew.Municipality))
                {
                    textBoxMunicipality.Text = address.Municipality;
                }
                else
                {
                    textBoxMunicipality.Text = addressNew.Municipality;
                }

                PanelViewAndEdit.Controls.Add(textBoxMunicipality);

                y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
            }
            else
            {
                int currentTop = lblMunicipalityText.Bottom + 4;

                Label lblMunicipality = new Label();
                lblMunicipality.AutoSize = true;
                lblMunicipality.Location = new Point(lblMunicipalityText.Left, lblMunicipalityText.Bottom + 4);
                lblMunicipality.Font = new Font(new FontFamily(lblMunicipality.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblMunicipality.ForeColor = string.IsNullOrWhiteSpace(addressNew.Municipality) ? ForeColorNormal : ForeColorChangedOrNew;
                lblMunicipality.Text = string.IsNullOrWhiteSpace(address.Municipality) ? "(empty)" : $"({address.Municipality})";

                PanelViewAndEdit.Controls.Add(lblMunicipality);

                //y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                if (!string.IsNullOrWhiteSpace(addressNew.Municipality))
                {
                    currentTop = lblMunicipality.Bottom + 4;

                    Label lblMunicipalityNew = new Label();
                    lblMunicipalityNew.AutoSize = true;
                    lblMunicipalityNew.Location = new Point(lblMunicipalityText.Left, lblMunicipality.Bottom + 4);
                    lblMunicipalityNew.Font = new Font(new FontFamily(lblMunicipalityNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblMunicipalityNew.ForeColor = addressNew.Municipality != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblMunicipalityNew.Text = string.IsNullOrWhiteSpace(addressNew.Municipality) ? "empty" : addressNew.Municipality;

                    PanelViewAndEdit.Controls.Add(lblMunicipalityNew);

                    //y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;
                }

            }

            //x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            // ---------------------------------------------------------------------------------------------------------------
            // end of Municipality
            // ---------------------------------------------------------------------------------------------------------------

            #endregion Municipality

            #region PostalCode
            // ---------------------------------------------------------------------------------------------------------------
            // start of PostalCode
            // ---------------------------------------------------------------------------------------------------------------
            Label lblPostalCodeText = new Label();
            lblPostalCodeText.AutoSize = true;
            lblPostalCodeText.Location = new Point(x, lblStreetNumberText.Top);
            lblPostalCodeText.Font = new Font(new FontFamily(lblPostalCodeText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblPostalCodeText.ForeColor = string.IsNullOrWhiteSpace(addressNew.PostalCode) ? ForeColorNormal : ForeColorChangedOrNew;
            lblPostalCodeText.Text = $@"Postal Code";

            PanelViewAndEdit.Controls.Add(lblPostalCodeText);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            if (IsEditing)
            {
                int currentTop = lblPostalCodeText.Bottom + 4;

                if (!string.IsNullOrWhiteSpace(addressNew.PostalCode))
                {
                    Label lblPostalCodeOld = new Label();
                    lblPostalCodeOld.AutoSize = true;
                    lblPostalCodeOld.Location = new Point(lblPostalCodeText.Left, currentTop);
                    lblPostalCodeOld.Font = new Font(new FontFamily(lblPostalCodeOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblPostalCodeOld.ForeColor = string.IsNullOrWhiteSpace(addressNew.PostalCode) ? ForeColorNormal : ForeColorChangedOrNew;
                    lblPostalCodeOld.Text = string.IsNullOrWhiteSpace(address.PostalCode) ? "(empty)" : $"({address.PostalCode})";

                    PanelViewAndEdit.Controls.Add(lblPostalCodeOld);

                    currentTop = lblPostalCodeOld.Bottom + 4;

                }

                TextBox textBoxPostalCode = new TextBox();
                textBoxPostalCode.Location = new Point(lblPostalCodeText.Left, currentTop);
                textBoxPostalCode.Name = "textBoxPostalCode";
                textBoxPostalCode.Font = new Font(new FontFamily(lblPostalCodeText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textBoxPostalCode.Width = lblPostalCodeText.Width;
                if (string.IsNullOrWhiteSpace(addressNew.PostalCode))
                {
                    textBoxPostalCode.Text = address.PostalCode;
                }
                else
                {
                    textBoxPostalCode.Text = addressNew.PostalCode;
                }

                PanelViewAndEdit.Controls.Add(textBoxPostalCode);

                y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
            }
            else
            {
                int currentTop = lblPostalCodeText.Bottom + 4;

                Label lblPostalCode = new Label();
                lblPostalCode.AutoSize = true;
                lblPostalCode.Location = new Point(lblPostalCodeText.Left, lblPostalCodeText.Bottom + 4);
                lblPostalCode.Font = new Font(new FontFamily(lblPostalCode.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblPostalCode.ForeColor = string.IsNullOrWhiteSpace(addressNew.PostalCode) ? ForeColorNormal : ForeColorChangedOrNew;
                lblPostalCode.Text = string.IsNullOrWhiteSpace(address.PostalCode) ? "(empty)" : $"({address.PostalCode})";

                PanelViewAndEdit.Controls.Add(lblPostalCode);

                //y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                if (!string.IsNullOrWhiteSpace(addressNew.PostalCode))
                {
                    currentTop = lblPostalCode.Bottom + 4;

                    Label lblPostalCodeNew = new Label();
                    lblPostalCodeNew.AutoSize = true;
                    lblPostalCodeNew.Location = new Point(lblPostalCodeText.Left, lblPostalCode.Bottom + 4);
                    lblPostalCodeNew.Font = new Font(new FontFamily(lblPostalCodeNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblPostalCodeNew.ForeColor = addressNew.PostalCode != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblPostalCodeNew.Text = string.IsNullOrWhiteSpace(addressNew.PostalCode) ? "empty" : addressNew.PostalCode;

                    PanelViewAndEdit.Controls.Add(lblPostalCodeNew);

                    //y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;
                }

            }

            //x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            // ---------------------------------------------------------------------------------------------------------------
            // end of PostalCode
            // ---------------------------------------------------------------------------------------------------------------

            #endregion PostalCode

            #endregion Address

        }
        private void DrawItemBool(int x, int y, bool? val, bool? valNew, string lblTxt, string textBoxName)
        {
            Label lblItem = new Label();
            lblItem.AutoSize = true;
            lblItem.Location = new Point(x, y);
            lblItem.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            lblItem.Text = $@"{lblTxt}: " + $@"{(valNew == null ? "" : $" ({(val == null ? "empty" : ((bool)val).ToString())})")}";

            PanelViewAndEdit.Controls.Add(lblItem);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            if (IsEditing)
            {
                TextBox textItem = new TextBox();
                textItem.Location = new Point(x, y);
                textItem.Name = $"{textBoxName}";
                textItem.Font = new Font(new FontFamily(textItem.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textItem.Width = 100;
                textItem.Text = (valNew == null ? (val == null ? "" : ((bool)val).ToString()) : ((bool)valNew).ToString());

                PanelViewAndEdit.Controls.Add(textItem);


            }
            else
            {
                Label lblItemNew = new Label();
                lblItemNew.AutoSize = true;
                lblItemNew.Location = new Point(x, y);
                lblItemNew.Font = new Font(new FontFamily(lblItemNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblItemNew.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((bool)val).ToString()) : ((bool)valNew).ToString());

                PanelViewAndEdit.Controls.Add(lblItemNew);
            }

        }
        private void DrawItemEnum(int x, int y, int? val, int? valNew, string lblTxt, string comboBoxName, Type enumType)
        {
            Label lblItemEnum = new Label();
            lblItemEnum.AutoSize = true;
            lblItemEnum.Location = new Point(x, y);
            lblItemEnum.Font = new Font(new FontFamily(lblItemEnum.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItemEnum.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            switch (enumType.Name)
            {
                case "InfrastructureTypeEnum":
                    {
                        lblItemEnum.Text = $@"Infrastructure Type";
                    }
                    break;
                case "FacilityTypeEnum":
                    {
                        lblItemEnum.Text = $@"Facility Type";
                    }
                    break;
                case "AerationTypeEnum":
                    {
                        lblItemEnum.Text = $@"Aeration Type";
                    }
                    break;
                case "PreliminaryTreatmentTypeEnum":
                    {
                        lblItemEnum.Text = $@"Preliminary Treatment Type";
                    }
                    break;
                case "PrimaryTreatmentTypeEnum":
                    {
                        lblItemEnum.Text = $@"Primary Treatment Type";
                    }
                    break;
                case "SecondaryTreatmentTypeEnum":
                    {
                        lblItemEnum.Text = $@"Secondary Treatment Type";
                    }
                    break;
                case "TertiaryTreatmentTypeEnum":
                    {
                        lblItemEnum.Text = $@"Tertiary Treatment Type";
                    }
                    break;
                case "TreatmentTypeEnum":
                    {
                        lblItemEnum.Text = $@"Treatment Type";
                    }
                    break;
                case "DisinfectionTypeEnum":
                    {
                        lblItemEnum.Text = $@"Disinfection Type";
                    }
                    break;
                case "CollectionSystemTypeEnum":
                    {
                        lblItemEnum.Text = $@"Collection System Type";
                    }
                    break;
                case "AlarmSystemTypeEnum":
                    {
                        lblItemEnum.Text = $@"Alarm System Type";
                    }
                    break;
                default:
                    break;
            }

            PanelViewAndEdit.Controls.Add(lblItemEnum);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            if (IsEditing)
            {
                if (valNew != null)
                {
                    Label lblItemEnumOld = new Label();
                    lblItemEnumOld.AutoSize = true;
                    lblItemEnumOld.Location = new Point(lblItemEnum.Right + 4, lblItemEnum.Top);
                    lblItemEnumOld.Font = new Font(new FontFamily(lblItemEnumOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblItemEnumOld.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    switch (enumType.Name)
                    {
                        case "InfrastructureTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((InfrastructureTypeEnum)val).ToString()})";
                            }
                            break;
                        case "FacilityTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((FacilityTypeEnum)val).ToString()})";
                            }
                            break;
                        case "AerationTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((AerationTypeEnum)val).ToString()})";
                            }
                            break;
                        case "PreliminaryTreatmentTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((PreliminaryTreatmentTypeEnum)val).ToString()})";
                            }
                            break;
                        case "PrimaryTreatmentTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((PrimaryTreatmentTypeEnum)val).ToString()})";
                            }
                            break;
                        case "SecondaryTreatmentTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((SecondaryTreatmentTypeEnum)val).ToString()})";
                            }
                            break;
                        case "TertiaryTreatmentTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((TertiaryTreatmentTypeEnum)val).ToString()})";
                            }
                            break;
                        case "TreatmentTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((TreatmentTypeEnum)val).ToString()})";
                            }
                            break;
                        case "DisinfectionTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((DisinfectionTypeEnum)val).ToString()})";
                            }
                            break;
                        case "CollectionSystemTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((CollectionSystemTypeEnum)val).ToString()})";
                            }
                            break;
                        case "AlarmSystemTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({((AlarmSystemTypeEnum)val).ToString()})";
                            }
                            break;
                        default:
                            break;
                    }

                    PanelViewAndEdit.Controls.Add(lblItemEnumOld);

                    x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                }

                ComboBox comboBoxItem = new ComboBox();
                comboBoxItem.Location = new Point(x, lblItemEnum.Top);
                comboBoxItem.Name = comboBoxName;
                comboBoxItem.Font = new Font(new FontFamily(lblItemEnum.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                //comboBoxStreetType.Width = lblItemEnum.Width;

                PanelViewAndEdit.Controls.Add(comboBoxItem);

                switch (enumType.Name)
                {
                    case "InfrastructureTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(InfrastructureTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((InfrastructureTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((InfrastructureTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((InfrastructureTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    case "FacilityTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(FacilityTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((FacilityTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((FacilityTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((FacilityTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    case "AerationTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(AerationTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((AerationTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((AerationTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((AerationTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    case "PreliminaryTreatmentTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(PreliminaryTreatmentTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((PreliminaryTreatmentTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((PreliminaryTreatmentTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((PreliminaryTreatmentTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    case "PrimaryTreatmentTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(PrimaryTreatmentTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((PrimaryTreatmentTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((PrimaryTreatmentTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((PrimaryTreatmentTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    case "SecondaryTreatmentTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(SecondaryTreatmentTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((SecondaryTreatmentTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((SecondaryTreatmentTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((SecondaryTreatmentTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    case "TertiaryTreatmentTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(TertiaryTreatmentTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((TertiaryTreatmentTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((TertiaryTreatmentTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((TertiaryTreatmentTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    case "TreatmentTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(TreatmentTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((TreatmentTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((TreatmentTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((TreatmentTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    case "DisinfectionTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(DisinfectionTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((DisinfectionTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((DisinfectionTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((DisinfectionTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    case "CollectionSystemTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(CollectionSystemTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((CollectionSystemTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((CollectionSystemTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((CollectionSystemTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    case "AlarmSystemTypeEnum":
                        {
                            for (int i = 1, count = Enum.GetNames(typeof(AlarmSystemTypeEnum)).Count(); i < count; i++)
                            {
                                comboBoxItem.Items.Add(((AlarmSystemTypeEnum)i).ToString());
                            }

                            if (valNew != null)
                            {
                                comboBoxItem.SelectedItem = ((AlarmSystemTypeEnum)valNew).ToString();
                            }
                            else
                            {
                                if (val != null)
                                {
                                    comboBoxItem.SelectedItem = ((AlarmSystemTypeEnum)val).ToString();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

                y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
            }
            else
            {
                Label lblItemEnum2 = new Label();
                lblItemEnum2.AutoSize = true;
                lblItemEnum2.Location = new Point(lblItemEnum.Right + 4, lblItemEnum.Top);
                lblItemEnum2.Font = new Font(new FontFamily(lblItemEnum2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblItemEnum2.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                switch (enumType.Name)
                {
                    case "InfrastructureTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((InfrastructureTypeEnum)val).ToString()})";
                        }
                        break;
                    case "FacilityTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((FacilityTypeEnum)val).ToString()})";
                        }
                        break;
                    case "AerationTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((AerationTypeEnum)val).ToString()})";
                        }
                        break;
                    case "PreliminaryTreatmentTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((PreliminaryTreatmentTypeEnum)val).ToString()})";
                        }
                        break;
                    case "PrimaryTreatmentTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((PrimaryTreatmentTypeEnum)val).ToString()})";
                        }
                        break;
                    case "SecondaryTreatmentTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((SecondaryTreatmentTypeEnum)val).ToString()})";
                        }
                        break;
                    case "TertiaryTreatmentTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((TertiaryTreatmentTypeEnum)val).ToString()})";
                        }
                        break;
                    case "TreatmentTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((TreatmentTypeEnum)val).ToString()})";
                        }
                        break;
                    case "DisinfectionTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((DisinfectionTypeEnum)val).ToString()})";
                        }
                        break;
                    case "CollectionSystemTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((CollectionSystemTypeEnum)val).ToString()})";
                        }
                        break;
                    case "AlarmSystemTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({((AlarmSystemTypeEnum)val).ToString()})";
                        }
                        break;
                    default:
                        break;
                }

                PanelViewAndEdit.Controls.Add(lblItemEnum2);

                //y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                if (valNew != null)
                {
                    Label lblItemEnumNew = new Label();
                    lblItemEnumNew.AutoSize = true;
                    lblItemEnumNew.Location = new Point(lblItemEnum2.Right + 4, lblItemEnum2.Top);
                    lblItemEnumNew.Font = new Font(new FontFamily(lblItemEnumNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblItemEnumNew.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    switch (enumType.Name)
                    {
                        case "InfrastructureTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((InfrastructureTypeEnum)valNew).ToString();
                            }
                            break;
                        case "FacilityTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((FacilityTypeEnum)valNew).ToString();
                            }
                            break;
                        case "AerationTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((AerationTypeEnum)valNew).ToString();
                            }
                            break;
                        case "PreliminaryTreatmentTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((PreliminaryTreatmentTypeEnum)valNew).ToString();
                            }
                            break;
                        case "PrimaryTreatmentTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((PrimaryTreatmentTypeEnum)valNew).ToString();
                            }
                            break;
                        case "SecondaryTreatmentTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((SecondaryTreatmentTypeEnum)valNew).ToString();
                            }
                            break;
                        case "TertiaryTreatmentTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((TertiaryTreatmentTypeEnum)valNew).ToString();
                            }
                            break;
                        case "TreatmentTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((TreatmentTypeEnum)valNew).ToString();
                            }
                            break;
                        case "DisinfectionTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((DisinfectionTypeEnum)valNew).ToString();
                            }
                            break;
                        case "CollectionSystemTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((CollectionSystemTypeEnum)valNew).ToString();
                            }
                            break;
                        case "AlarmSystemTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : ((AlarmSystemTypeEnum)valNew).ToString();
                            }
                            break;
                        default:
                            break;
                    }

                    PanelViewAndEdit.Controls.Add(lblItemEnumNew);
                }

            }
        }
        private void DrawItemFloat(int x, int y, float? val, float? valNew, string lblTxt, int fix, string textBoxName)
        {
            Label lblItem = new Label();
            lblItem.AutoSize = true;
            lblItem.Location = new Point(x, y);
            lblItem.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            lblItem.Text = $@"{lblTxt}: " + $@"{(valNew == null ? "" : $" ({(val == null ? "empty" : ((float)val).ToString("F" + fix))})")}";

            PanelViewAndEdit.Controls.Add(lblItem);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            if (IsEditing)
            {
                TextBox textItem = new TextBox();
                textItem.Location = new Point(x, y);
                textItem.Name = $"{textBoxName}";
                textItem.Font = new Font(new FontFamily(textItem.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textItem.Width = 100;
                textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));

                PanelViewAndEdit.Controls.Add(textItem);


            }
            else
            {
                Label lblItemNew = new Label();
                lblItemNew.AutoSize = true;
                lblItemNew.Location = new Point(x, y);
                lblItemNew.Font = new Font(new FontFamily(lblItemNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblItemNew.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));

                PanelViewAndEdit.Controls.Add(lblItemNew);
            }

        }
        private void DrawItemInt(int x, int y, int? val, int? valNew, string lblTxt, string textBoxName)
        {
            Label lblItem = new Label();
            lblItem.AutoSize = true;
            lblItem.Location = new Point(x, y);
            lblItem.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            lblItem.Text = $@"{lblTxt}: " + $@"{(valNew == null ? "" : $" ({(val == null ? "empty" : ((int)val).ToString())})")}";

            PanelViewAndEdit.Controls.Add(lblItem);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            if (IsEditing)
            {
                TextBox textItem = new TextBox();
                textItem.Location = new Point(x, y);
                textItem.Name = $"{textBoxName}";
                textItem.Font = new Font(new FontFamily(textItem.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textItem.Width = 100;
                textItem.Text = (valNew == null ? (val == null ? "" : ((int)val).ToString()) : ((int)valNew).ToString());

                PanelViewAndEdit.Controls.Add(textItem);


            }
            else
            {
                Label lblItemNew = new Label();
                lblItemNew.AutoSize = true;
                lblItemNew.Location = new Point(x, y);
                lblItemNew.Font = new Font(new FontFamily(lblItemNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblItemNew.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((int)val).ToString()) : ((int)valNew).ToString());

                PanelViewAndEdit.Controls.Add(lblItemNew);
            }

        }
        private void DrawItemText(int x, int y, string val, string valNew, string lblTxt, string textBoxName, int width)
        {
            Label lblItem = new Label();
            lblItem.AutoSize = true;
            lblItem.Location = new Point(x, y);
            lblItem.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            lblItem.Text = $@"{lblTxt}: " + (string.IsNullOrWhiteSpace(valNew) ? "" : $" ({(string.IsNullOrWhiteSpace(val) ? "empty" : val)})");

            PanelViewAndEdit.Controls.Add(lblItem);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            if (IsEditing)
            {
                TextBox textItem = new TextBox();
                textItem.Width = width;
                textItem.Location = new Point(x, y);
                textItem.Name = $"{textBoxName}";
                textItem.Font = new Font(new FontFamily(textItem.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textItem.Text = (string.IsNullOrWhiteSpace(valNew) ? (string.IsNullOrWhiteSpace(val) ? "" : val) : valNew);

                PanelViewAndEdit.Controls.Add(textItem);


            }
            else
            {
                Label lblItemNew = new Label();
                lblItemNew.AutoSize = true;
                lblItemNew.Location = new Point(x, y);
                lblItemNew.Font = new Font(new FontFamily(lblItemNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblItemNew.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblItemNew.Text = (string.IsNullOrWhiteSpace(valNew) ? (string.IsNullOrWhiteSpace(val) ? "" : val) : valNew);

                PanelViewAndEdit.Controls.Add(lblItemNew);
            }

        }
        private void DrawItemTextMultiline(int x, int y, string val, string valNew, string lblTxt, string textBoxName, int width)
        {
            Label lblItem = new Label();
            lblItem.AutoSize = true;
            lblItem.Location = new Point(x, y);
            lblItem.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            lblItem.Text = $@"{lblTxt}: " + (string.IsNullOrWhiteSpace(valNew) ? "" : $" ({(string.IsNullOrWhiteSpace(val) ? "empty" : val)})");

            PanelViewAndEdit.Controls.Add(lblItem);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            if (IsEditing)
            {
                TextBox textItem = new TextBox();
                textItem.Width = width;
                textItem.Height = 100;
                textItem.ScrollBars = ScrollBars.Both;
                textItem.Multiline = true;
                textItem.Location = new Point(x, y);
                textItem.Name = $"{textBoxName}";
                textItem.Font = new Font(new FontFamily(textItem.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textItem.Text = (string.IsNullOrWhiteSpace(valNew) ? (string.IsNullOrWhiteSpace(val) ? "" : val) : valNew);

                PanelViewAndEdit.Controls.Add(textItem);


            }
            else
            {
                Label lblItemNew = new Label();
                lblItemNew.AutoSize = true;
                lblItemNew.Location = new Point(x, y);
                lblItemNew.Font = new Font(new FontFamily(lblItemNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblItemNew.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblItemNew.Text = (string.IsNullOrWhiteSpace(valNew) ? (string.IsNullOrWhiteSpace(val) ? "" : val) : valNew);

                PanelViewAndEdit.Controls.Add(lblItemNew);
            }

        }
        public void DrawPanelInfrastructures()
        {
            PanelPolSourceSite.Controls.Clear();

            if (municipalityDoc.Municipality == null)
            {
                Label lblTVText = new Label();

                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.TabIndex = 0;
                lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblTVText.Text = $"Selected municipality has no local pollution source sites";

                PanelPolSourceSite.Controls.Add(lblTVText);
            }
            else
            {
                int countInfrastructure = 0;
                foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList.OrderBy(c => c.TVText))
                {
                    Panel panelInfrastructure = new Panel();

                    panelInfrastructure.BorderStyle = BorderStyle.FixedSingle;
                    panelInfrastructure.Location = new Point(0, countInfrastructure * 44);
                    panelInfrastructure.Size = new Size(PanelPolSourceSite.Width, 44);
                    panelInfrastructure.TabIndex = 0;
                    panelInfrastructure.Tag = infrastructure.InfrastructureTVItemID;
                    panelInfrastructure.Click += ShowMunicipality_Click;

                    Label lblTVText = new Label();

                    lblTVText.AutoSize = true;
                    lblTVText.Location = new Point(10, 4);
                    lblTVText.TabIndex = 0;
                    lblTVText.Tag = infrastructure.InfrastructureTVItemID;
                    if (infrastructure.IsActive == false)
                    {
                        lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    }
                    if (!string.IsNullOrWhiteSpace(infrastructure.TVTextNew))
                    {
                        lblTVText.Text = $"{infrastructure.TVTextNew}";
                    }
                    else
                    {
                        lblTVText.Text = $"{infrastructure.TVText}";
                    }
                    lblTVText.Click += ShowMunicipality_Click;

                    panelInfrastructure.Controls.Add(lblTVText);


                    Label lbInfrastructureStatus = new Label();

                    bool NeedDetailsUpdate = false;
                    bool NeedIssuesUpdate = false;
                    bool NeedPicturesUpdate = false;
                    if (IsAdmin)
                    {
                        if (infrastructure.LatNew != null
                           || infrastructure.LngNew != null
                           || infrastructure.IsActiveNew != null
                           || infrastructure.InfrastructureAddressNew.AddressTVItemID != null
                           || infrastructure.InfrastructureAddressNew.AddressType != null
                           || infrastructure.InfrastructureAddressNew.Municipality != null
                           || infrastructure.InfrastructureAddressNew.PostalCode != null
                           || infrastructure.InfrastructureAddressNew.StreetName != null
                           || infrastructure.InfrastructureAddressNew.StreetNumber != null
                           || infrastructure.InfrastructureAddressNew.StreetType != null)
                        {
                            NeedDetailsUpdate = true;
                        }

                        foreach (Picture picture in infrastructure.InfrastructurePictureList)
                        {
                            if (picture.DescriptionNew != null
                                || picture.ExtensionNew != null
                                || picture.FileNameNew != null
                                || picture.ToRemove != null)
                            {
                                NeedPicturesUpdate = true;
                                break;
                            }
                        }

                        lbInfrastructureStatus.AutoSize = true;
                        lbInfrastructureStatus.Location = new Point(40, lblTVText.Bottom + 4);
                        lbInfrastructureStatus.TabIndex = 0;
                        lbInfrastructureStatus.Tag = infrastructure.InfrastructureTVItemID;
                        if (infrastructure.IsActive == false)
                        {
                            lbInfrastructureStatus.Font = new Font(new FontFamily(lbInfrastructureStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                        }
                        else
                        {
                            lbInfrastructureStatus.Font = new Font(new FontFamily(lbInfrastructureStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        }
                        //string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                        //string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                        //string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                        //if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                        //{
                        //    lbInfrastructureStatus.Text = $"Good --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                        //}
                        //else
                        //{
                        lbInfrastructureStatus.Text = $"Good";
                        //}
                        lbInfrastructureStatus.Click += ShowMunicipality_Click;


                        panelInfrastructure.Controls.Add(lbInfrastructureStatus);

                    }
                    else
                    {

                        lbInfrastructureStatus.AutoSize = true;
                        lbInfrastructureStatus.Location = new Point(40, lblTVText.Bottom + 4);
                        lbInfrastructureStatus.TabIndex = 0;
                        lbInfrastructureStatus.Tag = infrastructure.InfrastructureTVItemID;
                        if (infrastructure.IsActive == false)
                        {
                            lbInfrastructureStatus.Font = new Font(new FontFamily(lbInfrastructureStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                        }
                        else
                        {
                            lbInfrastructureStatus.Font = new Font(new FontFamily(lbInfrastructureStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        }
                        lbInfrastructureStatus.Text = $"Good";
                        lbInfrastructureStatus.Click += ShowMunicipality_Click;


                        panelInfrastructure.Controls.Add(lbInfrastructureStatus);
                    }


                    PanelPolSourceSite.Controls.Add(panelInfrastructure);

                    countInfrastructure += 1;
                }
            }
        }
        public void DrawPanelPSS()
        {
            PanelPolSourceSite.Controls.Clear();

            if (subsectorDoc.Subsector == null)
            {
                Label lblTVText = new Label();

                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.TabIndex = 0;
                lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblTVText.Text = $"Selected subsector has no local pollution source sites";

                PanelPolSourceSite.Controls.Add(lblTVText);
            }
            else
            {
                int countPSS = 0;
                foreach (PSS pss in subsectorDoc.Subsector.PSSList.OrderBy(c => c.SiteNumber))
                {
                    Panel panelpss = new Panel();

                    panelpss.BorderStyle = BorderStyle.FixedSingle;
                    panelpss.Location = new Point(0, countPSS * 44);
                    panelpss.Size = new Size(PanelPolSourceSite.Width, 44);
                    panelpss.TabIndex = 0;
                    panelpss.Tag = pss.PSSTVItemID;
                    panelpss.Click += ShowPolSourceSite_Click;

                    Label lblTVText = new Label();

                    lblTVText.AutoSize = true;
                    lblTVText.Location = new Point(10, 4);
                    lblTVText.TabIndex = 0;
                    lblTVText.Tag = pss.PSSTVItemID;
                    if (pss.IsActive == false)
                    {
                        lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    }
                    if (!string.IsNullOrWhiteSpace(pss.TVTextNew))
                    {
                        lblTVText.Text = $"{pss.SiteNumber}    {pss.TVTextNew}";
                    }
                    else
                    {
                        lblTVText.Text = $"{pss.SiteNumber}    {pss.TVText}";
                    }
                    lblTVText.Click += ShowPolSourceSite_Click;

                    panelpss.Controls.Add(lblTVText);


                    Label lblPSSStatus = new Label();

                    bool NeedDetailsUpdate = false;
                    bool NeedIssuesUpdate = false;
                    bool NeedPicturesUpdate = false;
                    if (IsAdmin)
                    {
                        if (pss.LatNew != null
                           || pss.LngNew != null
                           || pss.IsActiveNew != null
                           || pss.IsPointSourceNew != null
                           || pss.PSSAddressNew.AddressTVItemID != null
                           || pss.PSSAddressNew.AddressType != null
                           || pss.PSSAddressNew.Municipality != null
                           || pss.PSSAddressNew.PostalCode != null
                           || pss.PSSAddressNew.StreetName != null
                           || pss.PSSAddressNew.StreetNumber != null
                           || pss.PSSAddressNew.StreetType != null
                           || pss.PSSObs.ObsDateNew != null)
                        {
                            NeedDetailsUpdate = true;
                        }

                        foreach (Issue issue in pss.PSSObs.IssueList)
                        {
                            if (issue.PolSourceObsInfoIntListNew.Count > 0 || issue.ToRemove == true)
                            {
                                NeedIssuesUpdate = true;
                                break;
                            }
                        }
                        foreach (Picture picture in pss.PSSPictureList)
                        {
                            if (picture.DescriptionNew != null
                                || picture.ExtensionNew != null
                                || picture.FileNameNew != null
                                || picture.ToRemove != null)
                            {
                                NeedPicturesUpdate = true;
                                break;
                            }
                        }

                        lblPSSStatus.AutoSize = true;
                        lblPSSStatus.Location = new Point(40, lblTVText.Bottom + 4);
                        lblPSSStatus.TabIndex = 0;
                        lblPSSStatus.Tag = pss.PSSTVItemID;
                        if (pss.IsActive == false)
                        {
                            lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                        }
                        else
                        {
                            lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        }
                        string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                        string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                        string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                        if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                        {
                            lblPSSStatus.Text = $"Good --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                        }
                        else
                        {
                            lblPSSStatus.Text = $"Good";
                        }
                        lblPSSStatus.Click += ShowPolSourceSite_Click;


                        panelpss.Controls.Add(lblPSSStatus);

                    }
                    else
                    {

                        lblPSSStatus.AutoSize = true;
                        lblPSSStatus.Location = new Point(40, lblTVText.Bottom + 4);
                        lblPSSStatus.TabIndex = 0;
                        lblPSSStatus.Tag = pss.PSSTVItemID;
                        if (pss.IsActive == false)
                        {
                            lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                        }
                        else
                        {
                            lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        }
                        lblPSSStatus.Text = $"Good";
                        lblPSSStatus.Click += ShowPolSourceSite_Click;


                        panelpss.Controls.Add(lblPSSStatus);
                    }


                    foreach (Issue issue in pss.PSSObs.IssueList)
                    {
                        if (issue.PolSourceObsInfoIntListNew.Count > 0)
                        {
                            issue.IsWellFormed = IssueWellFormed(issue, true);
                            issue.IsCompleted = IssueCompleted(issue, true);
                        }
                        else
                        {
                            issue.IsWellFormed = IssueWellFormed(issue, false);
                            issue.IsCompleted = IssueCompleted(issue, false);
                        }

                        if (issue.IsWellFormed == false)
                        {
                            panelpss.BackColor = BackColorNotWellFormed;
                            if (IsAdmin)
                            {
                                string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                                string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                                string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                                if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                                {
                                    lblPSSStatus.Text = $"Not Well Formed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                                }
                                else
                                {
                                    lblPSSStatus.Text = $"Not Well Formed";
                                }
                            }
                            else
                            {
                                lblPSSStatus.Text = $"Not Well Formed";
                            }
                            break;
                        }
                        if (issue.IsCompleted == false)
                        {
                            panelpss.BackColor = BackColorNotCompleted;
                            if (IsAdmin)
                            {
                                string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                                string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                                string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                                if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                                {
                                    lblPSSStatus.Text = $"Not Completed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                                }
                                else
                                {
                                    lblPSSStatus.Text = $"Not Completed";
                                }
                            }
                            else
                            {
                                lblPSSStatus.Text = $"Not Completed";
                            }
                            break;
                        }
                    }

                    PanelPolSourceSite.Controls.Add(panelpss);

                    countPSS += 1;
                }
            }
        }
        public void InfrastructureAdd()
        {
            if (municipalityDoc.Municipality.MunicipalityName != null)
            {
                Infrastructure infrastructure = new Infrastructure();
                float LastLat = 0.0f;
                float LastLng = 0.0f;
                int MaxInfrastructureTVItemID = 10000000;
                if (municipalityDoc.Municipality.InfrastructureList.Count > 0)
                {
                    int Max = municipalityDoc.Municipality.InfrastructureList.Max(c => c.InfrastructureTVItemID).Value;
                    if (Max >= MaxInfrastructureTVItemID)
                    {
                        MaxInfrastructureTVItemID = Max + 1;
                    }
                    LastLat = ((float)municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1].Lat);
                    LastLng = ((float)municipalityDoc.Municipality.InfrastructureList[municipalityDoc.Municipality.InfrastructureList.Count - 1].Lng);
                }
                infrastructure.InfrastructureTVItemID = MaxInfrastructureTVItemID;
                infrastructure.Lat = LastLat + 0.1f;
                infrastructure.Lng = LastLng + 0.1f;
                infrastructure.IsActive = true;
                infrastructure.TVText = "New Infrastructure";
                infrastructure.LastUpdateDate_UTC = DateTime.Now;

                municipalityDoc.Municipality.InfrastructureList.Add(infrastructure);
            }
        }
        public void PSSAdd()
        {
            if (subsectorDoc.Subsector.SubsectorName != null)
            {
                PSS pss = new PSS();
                float LastLat = 0.0f;
                float LastLng = 0.0f;
                int MaxPSSTVItemID = 10000000;
                if (subsectorDoc.Subsector.PSSList.Count > 0)
                {
                    int Max = subsectorDoc.Subsector.PSSList.Max(c => c.PSSTVItemID).Value;
                    if (Max >= MaxPSSTVItemID)
                    {
                        MaxPSSTVItemID = Max + 1;
                    }
                    LastLat = ((float)subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1].Lat);
                    LastLng = ((float)subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1].Lng);
                }
                pss.PSSTVItemID = MaxPSSTVItemID;
                pss.SiteNumber = subsectorDoc.Subsector.PSSList.Max(c => c.SiteNumber).Value + 1;
                pss.SiteNumberText = "00000".Substring(0, "00000".Length - pss.SiteNumber.ToString().Length) + pss.SiteNumber.ToString();
                pss.Lat = LastLat + 0.1f;
                pss.Lng = LastLng + 0.1f;
                pss.IsActive = true;
                pss.IsPointSource = false;
                pss.TVText = "New PSS";

                subsectorDoc.Subsector.PSSList.Add(pss);

                Obs obs = new Obs();
                obs.ObsID = 10000000;
                obs.OldWrittenDescription = "";
                obs.LastUpdated_UTC = DateTime.Now;
                obs.ObsDate = DateTime.Now;

                pss.PSSObs = obs;

                Issue issue = new Issue();
                issue.IssueID = 10000000;
                issue.Ordinal = 0;
                issue.LastUpdated_UTC = DateTime.Now;

                pss.PSSObs.IssueList.Add(issue);
            }
        }
        public void InfrastructureSaveToCSSPWebTools()
        {
            //MessageBox.Show("PSSSaveToCSSPWebTools " + CurrentPSS.PSSTVItemID.ToString(), PolSourceSiteTVItemID.ToString());
            MessageBox.Show("This functionality has not been implemented yet.");
        }
        public void PSSSaveToCSSPWebTools()
        {
            //MessageBox.Show("PSSSaveToCSSPWebTools " + CurrentPSS.PSSTVItemID.ToString(), PolSourceSiteTVItemID.ToString());
            MessageBox.Show("This functionality has not been implemented yet.");
        }
        public void RedrawSinglePanelInfrastructure()
        {
            Panel panel = new Panel();
            Infrastructure infrastructure = new Infrastructure();

            if (InfrastructureTVItemID > 0)
            {
                foreach (Panel panel2 in PanelPolSourceSite.Controls)
                {
                    int InfTVItemID = int.Parse(panel2.Tag.ToString());
                    if (InfrastructureTVItemID == InfTVItemID)
                    {
                        panel = panel2;
                        infrastructure = municipalityDoc.Municipality.InfrastructureList.Where(c => c.InfrastructureTVItemID == InfrastructureTVItemID).FirstOrDefault();
                        break;
                    }
                }
            }
            panel.Controls.Clear();

            if (infrastructure != null)
            {
                Label lblTVText = new Label();

                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.TabIndex = 0;
                lblTVText.Tag = infrastructure.InfrastructureTVItemID;
                if (infrastructure.IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }
                if (!string.IsNullOrWhiteSpace(infrastructure.TVTextNew))
                {
                    lblTVText.Text = $"{infrastructure.TVTextNew}";
                }
                else
                {
                    lblTVText.Text = $"{infrastructure.TVText}";
                }
                lblTVText.Click += ShowPolSourceSite_Click;

                panel.Controls.Add(lblTVText);

                Label lblInfrastructureStatus = new Label();

                bool NeedDetailsUpdate = false;
                bool NeedIssuesUpdate = false;
                bool NeedPicturesUpdate = false;
                if (IsAdmin)
                {
                    if (infrastructure.LatNew != null
                       || infrastructure.LngNew != null
                       || infrastructure.IsActiveNew != null
                       || infrastructure.InfrastructureAddressNew.AddressTVItemID != null
                       || infrastructure.InfrastructureAddressNew.AddressType != null
                       || infrastructure.InfrastructureAddressNew.Municipality != null
                       || infrastructure.InfrastructureAddressNew.PostalCode != null
                       || infrastructure.InfrastructureAddressNew.StreetName != null
                       || infrastructure.InfrastructureAddressNew.StreetNumber != null
                       || infrastructure.InfrastructureAddressNew.StreetType != null)
                    {
                        NeedDetailsUpdate = true;
                    }

                    foreach (Picture picture in infrastructure.InfrastructurePictureList)
                    {
                        if (picture.DescriptionNew != null
                            || picture.ExtensionNew != null
                            || picture.FileNameNew != null
                            || picture.ToRemove != null)
                        {
                            NeedPicturesUpdate = true;
                            break;
                        }
                    }

                    lblInfrastructureStatus.AutoSize = true;
                    lblInfrastructureStatus.Location = new Point(40, lblTVText.Bottom + 4);
                    lblInfrastructureStatus.TabIndex = 0;
                    lblInfrastructureStatus.Tag = infrastructure.InfrastructureTVItemID;
                    if (infrastructure.IsActive == false)
                    {
                        lblInfrastructureStatus.Font = new Font(new FontFamily(lblInfrastructureStatus.Font.FontFamily.Name).Name, 10f, System.Drawing.FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblInfrastructureStatus.Font = new Font(new FontFamily(lblInfrastructureStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    }
                    string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                    string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                    string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                    if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                    {
                        lblInfrastructureStatus.Text = $"Good --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                    }
                    else
                    {
                        lblInfrastructureStatus.Text = $"Good";
                    }
                    lblInfrastructureStatus.Click += ShowPolSourceSite_Click;

                    panel.Controls.Add(lblInfrastructureStatus);
                }
                else
                {
                    lblInfrastructureStatus.AutoSize = true;
                    lblInfrastructureStatus.Location = new Point(40, lblTVText.Bottom + 4);
                    lblInfrastructureStatus.TabIndex = 0;
                    lblInfrastructureStatus.Tag = infrastructure.InfrastructureTVItemID;
                    if (infrastructure.IsActive == false)
                    {
                        lblInfrastructureStatus.Font = new Font(new FontFamily(lblInfrastructureStatus.Font.FontFamily.Name).Name, 10f, System.Drawing.FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblInfrastructureStatus.Font = new Font(new FontFamily(lblInfrastructureStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    }
                    lblInfrastructureStatus.Text = $"Good";
                    lblInfrastructureStatus.Click += ShowPolSourceSite_Click;

                    panel.Controls.Add(lblInfrastructureStatus);
                }
            }
        }
        public void RedrawSinglePanelPSS()
        {
            Panel panel = new Panel();
            PSS pss = new PSS();

            if (PolSourceSiteTVItemID > 0)
            {
                foreach (Panel panel2 in PanelPolSourceSite.Controls)
                {
                    int PSSTVItemID = int.Parse(panel2.Tag.ToString());
                    if (PolSourceSiteTVItemID == PSSTVItemID)
                    {
                        panel = panel2;
                        pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();
                        break;
                    }
                }
            }
            panel.Controls.Clear();

            if (pss != null)
            {
                Label lblTVText = new Label();

                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.TabIndex = 0;
                lblTVText.Tag = pss.PSSTVItemID;
                if (pss.IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }
                if (!string.IsNullOrWhiteSpace(pss.TVTextNew))
                {
                    lblTVText.Text = $"{pss.SiteNumber}    {pss.TVTextNew}";
                }
                else
                {
                    lblTVText.Text = $"{pss.SiteNumber}    {pss.TVText}";
                }
                lblTVText.Click += ShowPolSourceSite_Click;

                panel.Controls.Add(lblTVText);

                Label lblPSSStatus = new Label();

                bool NeedDetailsUpdate = false;
                bool NeedIssuesUpdate = false;
                bool NeedPicturesUpdate = false;
                if (IsAdmin)
                {
                    if (pss.LatNew != null
                       || pss.LngNew != null
                       || pss.IsActiveNew != null
                       || pss.IsPointSourceNew != null
                       || pss.PSSAddressNew.AddressTVItemID != null
                       || pss.PSSAddressNew.AddressType != null
                       || pss.PSSAddressNew.Municipality != null
                       || pss.PSSAddressNew.PostalCode != null
                       || pss.PSSAddressNew.StreetName != null
                       || pss.PSSAddressNew.StreetNumber != null
                       || pss.PSSAddressNew.StreetType != null
                       || pss.PSSObs.ObsDateNew != null)
                    {
                        NeedDetailsUpdate = true;
                    }

                    foreach (Issue issue in pss.PSSObs.IssueList)
                    {
                        if (issue.PolSourceObsInfoIntListNew.Count > 0 || issue.ToRemove == true)
                        {
                            NeedIssuesUpdate = true;
                            break;
                        }
                    }
                    foreach (Picture picture in pss.PSSPictureList)
                    {
                        if (picture.DescriptionNew != null
                            || picture.ExtensionNew != null
                            || picture.FileNameNew != null
                            || picture.ToRemove != null)
                        {
                            NeedPicturesUpdate = true;
                            break;
                        }
                    }

                    lblPSSStatus.AutoSize = true;
                    lblPSSStatus.Location = new Point(40, lblTVText.Bottom + 4);
                    lblPSSStatus.TabIndex = 0;
                    lblPSSStatus.Tag = pss.PSSTVItemID;
                    if (pss.IsActive == false)
                    {
                        lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, System.Drawing.FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    }
                    string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                    string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                    string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                    if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                    {
                        lblPSSStatus.Text = $"Good --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                    }
                    else
                    {
                        lblPSSStatus.Text = $"Good";
                    }
                    lblPSSStatus.Click += ShowPolSourceSite_Click;

                    panel.Controls.Add(lblPSSStatus);
                }
                else
                {
                    lblPSSStatus.AutoSize = true;
                    lblPSSStatus.Location = new Point(40, lblTVText.Bottom + 4);
                    lblPSSStatus.TabIndex = 0;
                    lblPSSStatus.Tag = pss.PSSTVItemID;
                    if (pss.IsActive == false)
                    {
                        lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, System.Drawing.FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblPSSStatus.Font = new Font(new FontFamily(lblPSSStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    }
                    lblPSSStatus.Text = $"Good";
                    lblPSSStatus.Click += ShowPolSourceSite_Click;

                    panel.Controls.Add(lblPSSStatus);
                }



                foreach (Issue issue in pss.PSSObs.IssueList)
                {
                    if (issue.PolSourceObsInfoIntListNew.Count > 0)
                    {
                        issue.IsWellFormed = IssueWellFormed(issue, true);
                        issue.IsCompleted = IssueCompleted(issue, true);
                    }
                    else
                    {
                        issue.IsWellFormed = IssueWellFormed(issue, false);
                        issue.IsCompleted = IssueCompleted(issue, false);
                    }
                    if (issue.IsWellFormed == false)
                    {
                        panel.BackColor = BackColorNotWellFormed;
                        if (IsAdmin)
                        {
                            string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                            string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                            string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                            if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                            {
                                lblPSSStatus.Text = $"Not Well Formed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                            }
                            else
                            {
                                lblPSSStatus.Text = $"Not Well Formed";
                            }
                        }
                        else
                        {
                            lblPSSStatus.Text = $"Not Well Formed";
                        }
                        break;
                    }
                    if (issue.IsCompleted == false)
                    {
                        panel.BackColor = BackColorNotCompleted;
                        if (IsAdmin)
                        {
                            string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                            string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                            string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                            if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                            {
                                lblPSSStatus.Text = $"Not Completed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText}";
                            }
                            else
                            {
                                lblPSSStatus.Text = $"Not Completed";
                            }
                        }
                        else
                        {
                            lblPSSStatus.Text = $"Not Completed";
                        }
                        break;
                    }
                }

            }
        }
        public void RedrawInfrastructureList()
        {
            IsReading = true;
            if (!ReadInfrastructuresMunicipalityFile())
            {
                return;
            }
            IsReading = false;
            if (!CheckAllReadDataMunicipalityOK())
            {
                return;
            }

            DrawPanelInfrastructures();
        }
        public void RedrawPolSourceSiteList()
        {
            IsReading = true;
            if (!ReadPollutionSourceSitesSubsectorFile())
            {
                return;
            }
            IsReading = false;
            if (!CheckAllReadDataPollutionSourceSiteOK())
            {
                return;
            }

            DrawPanelPSS();
        }
        public void ShowInfrastructure()
        {
            PanelViewAndEdit.Controls.Clear();

            if (CurrentInfrastructure == null)
            {
                Label lblMessage = new Label();
                lblMessage.AutoSize = true;
                lblMessage.Location = new Point(30, 30);
                lblMessage.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblMessage.Font = new Font(new FontFamily(lblMessage.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblMessage.Text = $"Please select an infrastructure items for {(IsEditing ? "editing" : "viewing")} {(ShowOnlyIssues ? "issues" : (ShowOnlyPictures ? "pictures" : "pollution source site"))}";

                PanelViewAndEdit.Controls.Add(lblMessage);

                return;
            }

            if (Language == LanguageEnum.fr)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
            }

            int Y = 0;
            int X = 10;
            if (CurrentInfrastructure != null)
            {
                #region Title and Active button
                Label lblTVText = new Label();
                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                if (CurrentInfrastructure.IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }
                lblTVText.Text = $"{(CurrentInfrastructure.TVTextNew != null ? CurrentInfrastructure.TVTextNew : CurrentInfrastructure.TVText)}";

                PanelViewAndEdit.Controls.Add(lblTVText);

                if (IsEditing)
                {
                    if ((bool)CurrentInfrastructure.IsActive)
                    {
                        Button butChangeToIsNotActive = new Button();
                        butChangeToIsNotActive.AutoSize = true;
                        butChangeToIsNotActive.Location = new Point(lblTVText.Right + 10, lblTVText.Top);
                        butChangeToIsNotActive.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsNotActive.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsNotActive.Padding = new Padding(5);
                        butChangeToIsNotActive.Tag = CurrentInfrastructure.InfrastructureTVItemID.ToString();
                        butChangeToIsNotActive.Text = $"Set as non active";
                        butChangeToIsNotActive.Click += butChangeToIsNotActive_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsNotActive);
                    }
                    else
                    {
                        Button butChangeToIsActive = new Button();
                        butChangeToIsActive.AutoSize = true;
                        butChangeToIsActive.Location = new Point(lblTVText.Right + 10, lblTVText.Top);
                        butChangeToIsActive.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsActive.Font = new Font(new FontFamily(butChangeToIsActive.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsActive.Padding = new Padding(5);
                        butChangeToIsActive.Tag = CurrentInfrastructure.InfrastructureTVItemID.ToString();
                        butChangeToIsActive.Text = $"Set as active";
                        butChangeToIsActive.Click += butChangeToIsActive_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsActive);
                    }
                }
                else
                {
                    Label lblIsActive = new Label();
                    lblIsActive.AutoSize = true;
                    lblIsActive.Location = new Point(lblTVText.Right + 10, lblTVText.Top);
                    lblIsActive.Font = new Font(new FontFamily(lblIsActive.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblIsActive.ForeColor = CurrentInfrastructure.IsActiveNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblIsActive.Text = (CurrentInfrastructure.IsActiveNew != null ? ((bool)CurrentInfrastructure.IsActiveNew ? "Is Active" : "Not Active") : ((bool)CurrentInfrastructure.IsActive ? "Is Active" : "Not Active"));

                    PanelViewAndEdit.Controls.Add(lblIsActive);
                }

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                #endregion Title and Active button

                #region TVText
                X = 10;
                DrawItemText(X, Y, CurrentInfrastructure.TVText, CurrentInfrastructure.TVTextNew, "Infrastructure Name", "textBoxTVText", 300);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion TVText

                #region Lat and Lng
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.Lat, CurrentInfrastructure.LatNew, "Lat", 5, "textBoxLat");

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

                DrawItemFloat(X, Y, CurrentInfrastructure.Lng, CurrentInfrastructure.LngNew, "Lng", 5, "textBoxLng");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion Lat and Lng

                #region LatOutfall and LngOutfall
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.LatOutfall, CurrentInfrastructure.LatOutfallNew, "Lat Outfall", 5, "textBoxLatOutfall");

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

                DrawItemFloat(X, Y, CurrentInfrastructure.LngOutfall, CurrentInfrastructure.LngOutfallNew, "Lng Outfall", 5, "textBoxLngOutfall");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion LatOutfall and LngOutfall

                #region Address
                DrawItemAddress(X, Y, CurrentInfrastructure.InfrastructureAddress, CurrentInfrastructure.InfrastructureAddressNew);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                #endregion Address

                #region Comment
                X = 10;
                DrawItemTextMultiline(X, Y, CurrentInfrastructure.Comment, CurrentInfrastructure.CommentNew, "Comment", "textBoxComment", 300);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion Comment

                #region PrismID
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.PrismID, CurrentInfrastructure.PrismIDNew, "PrismID", "textBoxPrismID");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion PrismID

                #region TPID
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.TPID, CurrentInfrastructure.TPIDNew, "TPID", "textBoxTPID");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion TPID

                #region LSID
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.LSID, CurrentInfrastructure.LSIDNew, "LSID", "textBoxLSID");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion LSID

                #region SiteID
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.SiteID, CurrentInfrastructure.SiteIDNew, "SiteID", "textBoxSiteID");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion SiteID

                #region Site
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.Site, CurrentInfrastructure.SiteNew, "Site", "textBoxSite");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion Site

                #region InfrastructureCategory
                X = 10;
                DrawItemText(X, Y, CurrentInfrastructure.InfrastructureCategory, CurrentInfrastructure.InfrastructureCategoryNew, "Infrastructure Category", "textBoxInfrastructureCategory", 300);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion InfrastructureCategory

                #region InfrastructureType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.InfrastructureType, CurrentInfrastructure.InfrastructureTypeNew, "Infrastructure Type", "comboBoxInfrastructureType", typeof(InfrastructureTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion InfrastructureType

                #region FacilityType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.FacilityType, CurrentInfrastructure.FacilityTypeNew, "Facility Type", "comboBoxFacilityType", typeof(FacilityTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion FacilityType

                #region IsMechanicallyAerated
                X = 10;
                DrawItemBool(X, Y, CurrentInfrastructure.IsMechanicallyAerated, CurrentInfrastructure.IsMechanicallyAeratedNew, "Is Mechanically Aerated", "textBoxIsMechanicallyAerated");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion IsMechanicallyAerated

                #region NumberOfCells
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.NumberOfCells, CurrentInfrastructure.NumberOfCellsNew, "Number Of Cells", "textBoxNumberOfCells");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion NumberOfCells

                #region NumberOfAeratedCells
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.NumberOfAeratedCells, CurrentInfrastructure.NumberOfAeratedCellsNew, "Number Of Aerated Cells", "textBoxNumberOfAeratedCells");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion NumberOfAeratedCells

                #region AerationType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.AerationType, CurrentInfrastructure.AerationTypeNew, "Aeration Type", "comboBoxAerationType", typeof(AerationTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion AerationType

                #region PreliminaryTreatmentType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.PreliminaryTreatmentType, CurrentInfrastructure.PreliminaryTreatmentTypeNew, "Preliminary Treatment Type", "comboBoxPreliminaryTreatmentType", typeof(PreliminaryTreatmentTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion PreliminaryTreatmentType

                #region PrimaryTreatmentType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.PrimaryTreatmentType, CurrentInfrastructure.PrimaryTreatmentTypeNew, "Primary Treatment Type", "comboBoxPrimaryTreatmentType", typeof(PrimaryTreatmentTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion PrimaryTreatmentType

                #region SecondaryTreatmentType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.SecondaryTreatmentType, CurrentInfrastructure.SecondaryTreatmentTypeNew, "Secondary Treatment Type", "comboBoxPrimarySecondaryTreatmentType", typeof(SecondaryTreatmentTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion SecondaryTreatmentType

                #region TertiaryTreatmentType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.TertiaryTreatmentType, CurrentInfrastructure.TertiaryTreatmentTypeNew, "Tertiary Treatment Type", "comboBoxTertiaryTreatmentType", typeof(TertiaryTreatmentTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion TertiaryTreatmentType

                #region TreatmentType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.TreatmentType, CurrentInfrastructure.TreatmentTypeNew, "Treatment Type", "comboBoxTreatmentType", typeof(TreatmentTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion TreatmentType

                #region DisinfectionType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.DisinfectionType, CurrentInfrastructure.DisinfectionTypeNew, "Disinfection Type", "comboBoxDisinfectionType", typeof(DisinfectionTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion DisinfectionType

                #region CollectionSystemType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.CollectionSystemType, CurrentInfrastructure.CollectionSystemTypeNew, "Collection System Type", "comboBoxCollectionSystemType", typeof(CollectionSystemTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion CollectionSystemType

                #region AlarmSystemType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.AlarmSystemType, CurrentInfrastructure.AlarmSystemTypeNew, "Alarm System Type", "comboBoxAlarmSystemType", typeof(AlarmSystemTypeEnum));

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion AlarmSystemType

                #region DesignFlow_m3_day
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.DesignFlow_m3_day, CurrentInfrastructure.DesignFlow_m3_dayNew, "Design Flow (m3/day)", 5, "textBoxDesignFlow_m3_day");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion DesignFlow_m3_day

                #region AverageFlow_m3_day
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.AverageFlow_m3_day, CurrentInfrastructure.AverageFlow_m3_dayNew, "Average Flow (m3/day)", 5, "textBoxAverageFlow_m3_day");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion AverageFlow_m3_day

                #region PeakFlow_m3_day
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.PeakFlow_m3_day, CurrentInfrastructure.PeakFlow_m3_dayNew, "Peak Flow (m3/day)", 5, "textBoxPeakFlow_m3_day");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion PeakFlow_m3_day

                #region PopServed
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.PopServed, CurrentInfrastructure.PopServedNew, "Population Served", "textBoxPopServed");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion PopServed

                #region CanOverflow
                X = 10;
                DrawItemBool(X, Y, CurrentInfrastructure.CanOverflow, CurrentInfrastructure.CanOverflowNew, "Can Overflow", "textBoxCanOverflow");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion CanOverflow

                #region PercFlowOfTotal
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.PercFlowOfTotal, CurrentInfrastructure.PercFlowOfTotalNew, "Percentage Flow Of Total", 5, "textBoxPercFlowOfTotal");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion PercFlowOfTotal

                #region TimeOffset_hour
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.TimeOffset_hour, CurrentInfrastructure.TimeOffset_hourNew, "Time Offset (hour)", 5, "textBoxTimeOffset_hour");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion TimeOffset_hour

                #region TempCatchAllRemoveLater
                X = 10;
                DrawItemTextMultiline(X, Y, CurrentInfrastructure.TempCatchAllRemoveLater, CurrentInfrastructure.TempCatchAllRemoveLaterNew, "Temp Catch All Remove Later", "textBoxTempCatchAllRemoveLater", 400);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion TempCatchAllRemoveLater

                #region AverageDepth_m
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.AverageDepth_m, CurrentInfrastructure.AverageDepth_mNew, "Average Depth (m)", 5, "textBoxAverageDepth_m");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion AverageDepth_m

                #region NumberOfPorts
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.NumberOfPorts, CurrentInfrastructure.NumberOfPortsNew, "Number Of Ports", "textBoxNumberOfPorts");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion NumberOfPorts

                #region PortDiameter_m
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.PortDiameter_m, CurrentInfrastructure.PortDiameter_mNew, "Port Diameter (m)", 5, "textBoxPortDiameter_m");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion PortDiameter_m

                #region PortSpacing_m
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.PortSpacing_m, CurrentInfrastructure.PortSpacing_mNew, "Port Spacing (m)", 5, "textBoxPortSpacing_m");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion PortSpacing_m

                #region PortElevation_m
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.PortElevation_m, CurrentInfrastructure.PortElevation_mNew, "Port Elevation (m)", 5, "textBoxPortElevation_m");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion PortElevation_m

                #region VerticalAngle_deg
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.VerticalAngle_deg, CurrentInfrastructure.VerticalAngle_degNew, "Vertical Angle (deg)", 5, "textBoxVerticalAngle_deg");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion VerticalAngle_deg

                #region HorizontalAngle_deg
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.HorizontalAngle_deg, CurrentInfrastructure.HorizontalAngle_degNew, "Horizontal Angle (deg)", 5, "textBoxHorizontalAngle_deg");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion HorizontalAngle_deg

                #region DecayRate_per_day
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.DecayRate_per_day, CurrentInfrastructure.DecayRate_per_dayNew, "Decay Rate (/day)", 5, "textBoxDecayRate_per_day");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion DecayRate_per_day

                #region NearFieldVelocity_m_s
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.NearFieldVelocity_m_s, CurrentInfrastructure.NearFieldVelocity_m_sNew, "Near Field Velocity (m/s)", 5, "textBoxNearFieldVelocity_m_s");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion NearFieldVelocity_m_s

                #region FarFieldVelocity_m_s
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.FarFieldVelocity_m_s, CurrentInfrastructure.FarFieldVelocity_m_sNew, "Far Field Velocity (m/s)", 5, "textBoxFarFieldVelocity_m_s");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion FarFieldVelocity_m_s

                #region ReceivingWaterSalinity_PSU
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.ReceivingWaterSalinity_PSU, CurrentInfrastructure.ReceivingWaterSalinity_PSUNew, "Receiving Water Salinity (PSU)", 5, "textBoxReceivingWaterSalinity_PSU");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion ReceivingWaterSalinity_PSU

                #region ReceivingWaterTemperature_C
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.ReceivingWaterTemperature_C, CurrentInfrastructure.ReceivingWaterTemperature_CNew, "Receiving Water Temperature (C)", 5, "textBoxReceivingWaterTemperature_C");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion ReceivingWaterTemperature_C

                #region ReceivingWater_MPN_per_100ml
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.ReceivingWater_MPN_per_100ml, CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew, "Receiving Water (MPN /100 mL)", "textBoxReceivingWater_MPN_per_100ml");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion ReceivingWater_MPN_per_100ml

                #region DistanceFromShore_m
                X = 10;
                DrawItemFloat(X, Y, CurrentInfrastructure.DistanceFromShore_m, CurrentInfrastructure.DistanceFromShore_mNew, "Distance From Shore (m)", 5, "textBoxDistanceFromShore_m");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion DistanceFromShore_m

                #region SeeOtherTVItemID
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.SeeOtherTVItemID, CurrentInfrastructure.SeeOtherTVItemIDNew, "See Other TVItemID", "textBoxSeeOtherTVItemID");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion SeeOtherTVItemID

                #region PumpsToTVItemID
                X = 10;
                DrawItemInt(X, Y, CurrentInfrastructure.PumpsToTVItemID, CurrentInfrastructure.PumpsToTVItemID, "Pumps To TVItemID", "textBoxPumpsToTVItemID");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion PumpsToTVItemID

                #region Save button
                if (IsEditing)
                {
                    Button butSaveLatLngObsAndAddress = new Button();
                    butSaveLatLngObsAndAddress.AutoSize = true;
                    butSaveLatLngObsAndAddress.Location = new Point(200, Y);
                    butSaveLatLngObsAndAddress.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butSaveLatLngObsAndAddress.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butSaveLatLngObsAndAddress.Padding = new Padding(5);
                    butSaveLatLngObsAndAddress.Text = $"Save";
                    butSaveLatLngObsAndAddress.Click += butSaveLatLngAndObsAndAddress_Click;

                    PanelViewAndEdit.Controls.Add(butSaveLatLngObsAndAddress);

                }
                #endregion Save button

                if (!IsEditing)
                {
                    ShowPictures();
                }


            }
            IsReading = false;
        }
        public void ShowPolSourceSite()
        {
            PanelViewAndEdit.Controls.Clear();

            if (CurrentPSS == null)
            {
                Label lblMessage = new Label();
                lblMessage.AutoSize = true;
                lblMessage.Location = new Point(30, 30);
                lblMessage.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblMessage.Font = new Font(new FontFamily(lblMessage.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblMessage.Text = $"Please select a pollution source site items for {(IsEditing ? "editing" : "viewing")} {(ShowOnlyIssues ? "issues" : (ShowOnlyPictures ? "pictures" : "pollution source site"))}";

                PanelViewAndEdit.Controls.Add(lblMessage);

                return;
            }

            if (Language == LanguageEnum.fr)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
            }

            int Y = 0;
            int X = 10;
            if (CurrentPSS != null)
            {
                #region TVText
                Label lblTVText = new Label();
                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                if (CurrentPSS.IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }
                //lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblTVText.Text = $"{CurrentPSS.SiteNumber} {(CurrentPSS.TVTextNew != null ? CurrentPSS.TVTextNew : CurrentPSS.TVText)}";

                PanelViewAndEdit.Controls.Add(lblTVText);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                #endregion TVText

                #region Lat and Lng
                X = 10;
                DrawItemFloat(X, Y, CurrentPSS.Lat, CurrentPSS.LatNew, "Lat", 5, "textBoxLat");

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

                DrawItemFloat(X, Y, CurrentPSS.Lng, CurrentPSS.LngNew, "Lng", 5, "textBoxLng");

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion Lat and Lng

                #region IsActive and IsPointSource
                if (IsEditing)
                {
                    if ((bool)CurrentPSS.IsActive)
                    {
                        Button butChangeToIsNotActive = new Button();
                        butChangeToIsNotActive.AutoSize = true;
                        butChangeToIsNotActive.Location = new Point(X, Y);
                        butChangeToIsNotActive.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsNotActive.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsNotActive.Padding = new Padding(5);
                        butChangeToIsNotActive.Tag = CurrentPSS.PSSTVItemID.ToString();
                        butChangeToIsNotActive.Text = $"Set as non active";
                        butChangeToIsNotActive.Click += butChangeToIsNotActive_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsNotActive);
                    }
                    else
                    {
                        Button butChangeToIsActive = new Button();
                        butChangeToIsActive.AutoSize = true;
                        butChangeToIsActive.Location = new Point(X, Y);
                        butChangeToIsActive.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsActive.Font = new Font(new FontFamily(butChangeToIsActive.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsActive.Padding = new Padding(5);
                        butChangeToIsActive.Tag = CurrentPSS.PSSTVItemID.ToString();
                        butChangeToIsActive.Text = $"Set as active";
                        butChangeToIsActive.Click += butChangeToIsActive_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsActive);
                    }

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;
                }
                else
                {
                    Label lblIsActive = new Label();
                    lblIsActive.AutoSize = true;
                    lblIsActive.Location = new Point(X, Y);
                    lblIsActive.Font = new Font(new FontFamily(lblIsActive.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblIsActive.ForeColor = CurrentPSS.IsActiveNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblIsActive.Text = (CurrentPSS.IsActiveNew != null ? ((bool)CurrentPSS.IsActiveNew ? "Is Active" : "Not Active") : ((bool)CurrentPSS.IsActive ? "Is Active" : "Not Active"));

                    PanelViewAndEdit.Controls.Add(lblIsActive);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;
                }

                if (IsEditing)
                {
                    if ((bool)CurrentPSS.IsPointSource)
                    {
                        Button butChangeToIsNonPointSource = new Button();
                        butChangeToIsNonPointSource.AutoSize = true;
                        butChangeToIsNonPointSource.Location = new Point(X, Y);
                        butChangeToIsNonPointSource.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsNonPointSource.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsNonPointSource.Padding = new Padding(5);
                        butChangeToIsNonPointSource.Tag = CurrentPSS.PSSTVItemID.ToString();
                        butChangeToIsNonPointSource.Text = $"Set as non point source";
                        butChangeToIsNonPointSource.Click += butChangeToIsNonPointSource_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsNonPointSource);
                    }
                    else
                    {
                        Button butChangeToIsPointSource = new Button();
                        butChangeToIsPointSource.AutoSize = true;
                        butChangeToIsPointSource.Location = new Point(X, Y);
                        butChangeToIsPointSource.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsPointSource.Font = new Font(new FontFamily(butChangeToIsPointSource.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsPointSource.Padding = new Padding(5);
                        butChangeToIsPointSource.Tag = CurrentPSS.PSSTVItemID.ToString();
                        butChangeToIsPointSource.Text = $"Set as point source";
                        butChangeToIsPointSource.Click += butChangeToIsPointSource_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsPointSource);
                    }

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                }
                else
                {
                    Label lblIsPointSource = new Label();
                    lblIsPointSource.AutoSize = true;
                    lblIsPointSource.Location = new Point(X, Y);
                    lblIsPointSource.Font = new Font(new FontFamily(lblIsPointSource.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblIsPointSource.ForeColor = CurrentPSS.IsPointSourceNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblIsPointSource.Text = (CurrentPSS.IsPointSourceNew != null ? ((bool)CurrentPSS.IsPointSourceNew ? "Is Point Source" : "Not Point Source") : ((bool)CurrentPSS.IsPointSource ? "Is Point Source" : "Not Point Source"));

                    PanelViewAndEdit.Controls.Add(lblIsPointSource);


                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                }
                #endregion IsActive and IsPointSource

                #region Address
                DrawItemAddress(X, Y, CurrentPSS.PSSAddress, CurrentPSS.PSSAddressNew);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                #endregion Address

                #region Observation
                Label lblLastObsText = new Label();
                lblLastObsText.AutoSize = true;
                lblLastObsText.Location = new Point(10, Y);
                lblLastObsText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblLastObsText.Font = new Font(new FontFamily(lblLastObsText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblLastObsText.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                lblLastObsText.Text = $"Observation Date: ";

                PanelViewAndEdit.Controls.Add(lblLastObsText);

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                if (IsEditing)
                {
                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        Label lblLastObsOld = new Label();
                        lblLastObsOld.AutoSize = true;
                        lblLastObsOld.Location = new Point(X, lblLastObsText.Top);
                        lblLastObsOld.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblLastObsOld.Font = new Font(new FontFamily(lblLastObsOld.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblLastObsOld.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                        if (CurrentPSS.PSSObs.ObsDate != null)
                        {
                            lblLastObsOld.Text = $" ({CurrentPSS.PSSObs.ObsDate})";
                        }
                        else
                        {
                            lblLastObsOld.Text = $" (empty)";
                        }

                        PanelViewAndEdit.Controls.Add(lblLastObsOld);

                        X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                    }

                    DateTimePicker dateTimePickerObsDate = new DateTimePicker();
                    dateTimePickerObsDate.Location = new Point(X, lblLastObsText.Top);
                    dateTimePickerObsDate.Name = "dateTimePickerObsDate";
                    dateTimePickerObsDate.Format = DateTimePickerFormat.Custom;
                    dateTimePickerObsDate.CustomFormat = "yyyy MMMM dd";
                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        dateTimePickerObsDate.Value = ((DateTime)CurrentPSS.PSSObs.ObsDateNew);
                    }
                    else
                    {
                        dateTimePickerObsDate.Value = ((DateTime)CurrentPSS.PSSObs.ObsDate);
                    }
                    dateTimePickerObsDate.Tag = CurrentPSS.PSSObs.ObsID.ToString();

                    PanelViewAndEdit.Controls.Add(dateTimePickerObsDate);
                }
                else
                {
                    Label lblLastObsOld = new Label();
                    lblLastObsOld.AutoSize = true;
                    lblLastObsOld.Location = new Point(X, lblLastObsText.Top);
                    lblLastObsOld.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblLastObsOld.Font = new Font(new FontFamily(lblLastObsOld.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblLastObsOld.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        lblLastObsOld.Text = $" ({((DateTime)CurrentPSS.PSSObs.ObsDate).ToString("yyyy MMMM dd")})";
                    }
                    else
                    {
                        lblLastObsOld.Text = $" {((DateTime)CurrentPSS.PSSObs.ObsDate).ToString("yyyy MMMM dd")}";
                    }

                    PanelViewAndEdit.Controls.Add(lblLastObsOld);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    if (CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        Label lblLastObsNew = new Label();
                        lblLastObsNew.AutoSize = true;
                        lblLastObsNew.Location = new Point(X, lblLastObsText.Top);
                        lblLastObsNew.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblLastObsNew.Font = new Font(new FontFamily(lblLastObsNew.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblLastObsNew.ForeColor = CurrentPSS.PSSObs.ObsDateNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblLastObsNew.Text = $" {((DateTime)CurrentPSS.PSSObs.ObsDateNew).ToString("yyyy MMMM dd")}";

                        PanelViewAndEdit.Controls.Add(lblLastObsNew);
                    }
                }

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                #endregion Observation

                if (IsEditing)
                {
                    Button butSaveLatLngObsAndAddress = new Button();
                    butSaveLatLngObsAndAddress.AutoSize = true;
                    butSaveLatLngObsAndAddress.Location = new Point(200, Y);
                    butSaveLatLngObsAndAddress.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butSaveLatLngObsAndAddress.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butSaveLatLngObsAndAddress.Padding = new Padding(5);
                    butSaveLatLngObsAndAddress.Text = $"Save";
                    butSaveLatLngObsAndAddress.Click += butSaveLatLngAndObsAndAddress_Click;

                    PanelViewAndEdit.Controls.Add(butSaveLatLngObsAndAddress);

                }

                if (!IsEditing)
                {
                    DrawIssuesForViewing();
                    ShowPictures();
                }

                if (IsAdmin)
                {
                    bool NeedDetailsUpdate = false;
                    bool NeedIssuesUpdate = false;
                    bool NeedPicturesUpdate = false;
                    if (CurrentPSS.LatNew != null
                       || CurrentPSS.LngNew != null
                       || CurrentPSS.IsActiveNew != null
                       || CurrentPSS.IsPointSourceNew != null
                       || CurrentPSS.PSSAddressNew.AddressTVItemID != null
                       || CurrentPSS.PSSAddressNew.AddressType != null
                       || CurrentPSS.PSSAddressNew.Municipality != null
                       || CurrentPSS.PSSAddressNew.PostalCode != null
                       || CurrentPSS.PSSAddressNew.StreetName != null
                       || CurrentPSS.PSSAddressNew.StreetNumber != null
                       || CurrentPSS.PSSAddressNew.StreetType != null
                       || CurrentPSS.PSSObs.ObsDateNew != null)
                    {
                        NeedDetailsUpdate = true;
                    }

                    foreach (Issue issue in CurrentPSS.PSSObs.IssueList)
                    {
                        if (issue.PolSourceObsInfoIntListNew.Count > 0 || issue.ToRemove == true)
                        {
                            NeedIssuesUpdate = true;
                            break;
                        }
                    }
                    foreach (Picture picture in CurrentPSS.PSSPictureList)
                    {
                        if (picture.DescriptionNew != null
                            || picture.ExtensionNew != null
                            || picture.FileNameNew != null
                            || picture.ToRemove != null)
                        {
                            NeedPicturesUpdate = true;
                            break;
                        }
                    }

                    string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                    string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                    string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                    if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate)
                    {
                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                        Button butPSSSaveToCSSPWebTools = new Button();
                        butPSSSaveToCSSPWebTools.AutoSize = true;
                        butPSSSaveToCSSPWebTools.Location = new Point(20, Y);
                        butPSSSaveToCSSPWebTools.Text = "Update All Pollution Source Site Related Information To CSSPWebTools";
                        butPSSSaveToCSSPWebTools.Tag = $"{CurrentPSS.PSSTVItemID}";
                        butPSSSaveToCSSPWebTools.Font = new Font(new FontFamily(butPSSSaveToCSSPWebTools.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        butPSSSaveToCSSPWebTools.Padding = new Padding(5);
                        butPSSSaveToCSSPWebTools.Click += butPSSSaveToCSSPWebTools_Click;

                        PanelViewAndEdit.Controls.Add(butPSSSaveToCSSPWebTools);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                        Label lblReturns = new Label();
                        lblReturns.AutoSize = true;
                        lblReturns.Location = new Point(30, Y);
                        lblReturns.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblReturns.Font = new Font(new FontFamily(lblReturns.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        lblReturns.Text = "\r\n\r\n\r\n\r\n";

                        PanelViewAndEdit.Controls.Add(lblReturns);
                    }
                }

            }
            IsReading = false;
        }
        public void SaveInfrastructureInfo()
        {
            foreach (Control control in PanelViewAndEdit.Controls)
            {
                switch (control.Name)
                {
                    case "comboBoxStreetType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.InfrastructureAddressNew.StreetType = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(StreetTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((StreetTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.InfrastructureAddress.StreetType == i)
                                            {
                                                CurrentInfrastructure.InfrastructureAddressNew.StreetType = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                                CurrentInfrastructure.InfrastructureAddressNew.StreetType = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "textBoxPostalCode":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentInfrastructure.InfrastructureAddress.PostalCode == tb.Text)
                                {
                                    CurrentInfrastructure.InfrastructureAddressNew.PostalCode = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                    CurrentInfrastructure.InfrastructureAddressNew.PostalCode = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxLat":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.Lat)
                                {
                                    CurrentInfrastructure.LatNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.LatNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.LatNew = null;
                                tb.Text = CurrentInfrastructure.Lat.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxLatOutfall":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.LatOutfall)
                                {
                                    CurrentInfrastructure.LatOutfallNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.LatOutfallNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.LatOutfallNew = null;
                                tb.Text = CurrentInfrastructure.LatOutfall.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for LatOutfall"));
                            }
                        }
                        break;
                    case "textBoxLng":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.Lng)
                                {
                                    CurrentInfrastructure.LngNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.LngNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.LngNew = null;
                                tb.Text = CurrentInfrastructure.Lng.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxLngOutfall":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.LngOutfall)
                                {
                                    CurrentInfrastructure.LngOutfallNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.LngOutfallNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.LngOutfallNew = null;
                                tb.Text = CurrentInfrastructure.LngOutfall.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxMunicipality":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentInfrastructure.InfrastructureAddress.Municipality == tb.Text)
                                {
                                    CurrentInfrastructure.InfrastructureAddressNew.Municipality = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                    CurrentInfrastructure.InfrastructureAddressNew.Municipality = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxStreetName":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentInfrastructure.InfrastructureAddress.StreetName == tb.Text)
                                {
                                    CurrentInfrastructure.InfrastructureAddressNew.StreetName = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                    CurrentInfrastructure.InfrastructureAddressNew.StreetName = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxStreetNumber":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentInfrastructure.InfrastructureAddress.StreetNumber == tb.Text)
                                {
                                    CurrentInfrastructure.InfrastructureAddressNew.StreetNumber = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                    CurrentInfrastructure.InfrastructureAddressNew.StreetNumber = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxTVText":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentInfrastructure.TVText == tb.Text)
                                {
                                    CurrentInfrastructure.TVTextNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.TVTextNew = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxComment":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentInfrastructure.Comment == tb.Text)
                                {
                                    CurrentInfrastructure.CommentNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.CommentNew = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxPrismID":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.PrismID)
                                {
                                    CurrentInfrastructure.PrismIDNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.PrismIDNew = TempInt;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PrismIDNew = null;
                                tb.Text = CurrentInfrastructure.PrismID.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for PrismID"));
                            }
                        }
                        break;
                    case "textBoxTPID":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.TPID)
                                {
                                    CurrentInfrastructure.TPIDNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.TPIDNew = TempInt;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.TPIDNew = null;
                                tb.Text = CurrentInfrastructure.TPID.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for TPID"));
                            }
                        }
                        break;
                    case "textBoxLSID":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.LSID)
                                {
                                    CurrentInfrastructure.LSIDNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.LSIDNew = TempInt;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.LSIDNew = null;
                                tb.Text = CurrentInfrastructure.LSID.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for LSID"));
                            }
                        }
                        break;
                    case "textBoxSiteID":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.SiteID)
                                {
                                    CurrentInfrastructure.SiteIDNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.SiteIDNew = TempInt;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.SiteIDNew = null;
                                tb.Text = CurrentInfrastructure.SiteID.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for SiteID"));
                            }
                        }
                        break;
                    case "textBoxSite":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.Site)
                                {
                                    CurrentInfrastructure.SiteNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.SiteNew = TempInt;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.SiteNew = null;
                                tb.Text = CurrentInfrastructure.Site.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Site"));
                            }
                        }
                        break;
                    case "textBoxInfrastructureCategory":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentInfrastructure.InfrastructureCategory == tb.Text)
                                {
                                    CurrentInfrastructure.InfrastructureCategoryNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.InfrastructureCategoryNew = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "comboBoxInfrastructureType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.InfrastructureTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(InfrastructureTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((InfrastructureTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.InfrastructureType == i)
                                            {
                                                CurrentInfrastructure.InfrastructureTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.InfrastructureTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxFacilityType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.FacilityTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(FacilityTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((FacilityTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.FacilityType == i)
                                            {
                                                CurrentInfrastructure.FacilityTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.FacilityTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxAerationType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.AerationTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(AerationTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((AerationTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.AerationType == i)
                                            {
                                                CurrentInfrastructure.AerationTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.AerationTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxPreliminaryTreatmentType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.PreliminaryTreatmentTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(PreliminaryTreatmentTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((PreliminaryTreatmentTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.PreliminaryTreatmentType == i)
                                            {
                                                CurrentInfrastructure.PreliminaryTreatmentTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.PreliminaryTreatmentTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxPrimaryTreatmentType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.PrimaryTreatmentTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(PrimaryTreatmentTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((PrimaryTreatmentTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.PrimaryTreatmentType == i)
                                            {
                                                CurrentInfrastructure.PrimaryTreatmentTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.PrimaryTreatmentTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxSecondaryTreatmentType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.SecondaryTreatmentTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(SecondaryTreatmentTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((SecondaryTreatmentTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.SecondaryTreatmentType == i)
                                            {
                                                CurrentInfrastructure.SecondaryTreatmentTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.SecondaryTreatmentTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxTertiaryTreatmentType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.TertiaryTreatmentTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(TertiaryTreatmentTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((TertiaryTreatmentTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.TertiaryTreatmentType == i)
                                            {
                                                CurrentInfrastructure.TertiaryTreatmentTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.TertiaryTreatmentTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxTreatmentType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.TreatmentTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(TreatmentTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((TreatmentTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.TreatmentType == i)
                                            {
                                                CurrentInfrastructure.TreatmentTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.TreatmentTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxDisinfectionType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.DisinfectionTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(DisinfectionTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((DisinfectionTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.DisinfectionType == i)
                                            {
                                                CurrentInfrastructure.DisinfectionTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.DisinfectionTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxCollectionSystemType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.CollectionSystemTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(CollectionSystemTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((CollectionSystemTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.CollectionSystemType == i)
                                            {
                                                CurrentInfrastructure.CollectionSystemTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.CollectionSystemTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxAlarmSystemType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.AlarmSystemTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(AlarmSystemTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((AlarmSystemTypeEnum)i).ToString())
                                        {
                                            if (CurrentInfrastructure.AlarmSystemType == i)
                                            {
                                                CurrentInfrastructure.AlarmSystemTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.AlarmSystemTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "textBoxDesignFlow_m3_day":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.DesignFlow_m3_day)
                                {
                                    CurrentInfrastructure.DesignFlow_m3_dayNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.DesignFlow_m3_dayNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.DesignFlow_m3_dayNew = null;
                                tb.Text = CurrentInfrastructure.DesignFlow_m3_day.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxAverageFlow_m3_day":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.AverageFlow_m3_day)
                                {
                                    CurrentInfrastructure.AverageFlow_m3_dayNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.AverageFlow_m3_dayNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.AverageFlow_m3_dayNew = null;
                                tb.Text = CurrentInfrastructure.AverageFlow_m3_day.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxPeakFlow_m3_day":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.PeakFlow_m3_day)
                                {
                                    CurrentInfrastructure.PeakFlow_m3_dayNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.PeakFlow_m3_dayNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PeakFlow_m3_dayNew = null;
                                tb.Text = CurrentInfrastructure.PeakFlow_m3_day.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxPopServed":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.PopServed)
                                {
                                    CurrentInfrastructure.PopServedNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.PopServedNew = TempInt;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PopServedNew = null;
                                tb.Text = CurrentInfrastructure.PopServed.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxCanOverflow":
                        {
                            TextBox tb = (TextBox)control;

                            if (bool.TryParse(tb.Text, out bool TempBool))
                            {
                                if (TempBool == CurrentInfrastructure.CanOverflow)
                                {
                                    CurrentInfrastructure.CanOverflowNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.CanOverflowNew = TempBool;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.CanOverflowNew = null;
                                tb.Text = CurrentInfrastructure.CanOverflow.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxPercFlowOfTotal":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.PercFlowOfTotal)
                                {
                                    CurrentInfrastructure.PercFlowOfTotalNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.PercFlowOfTotalNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PercFlowOfTotalNew = null;
                                tb.Text = CurrentInfrastructure.PercFlowOfTotal.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxTimeOffset_hour":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.TimeOffset_hour)
                                {
                                    CurrentInfrastructure.TimeOffset_hourNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.TimeOffset_hourNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.TimeOffset_hourNew = null;
                                tb.Text = CurrentInfrastructure.TimeOffset_hour.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxTempCatchAllRemoveLater":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentInfrastructure.TempCatchAllRemoveLater == tb.Text)
                                {
                                    CurrentInfrastructure.TempCatchAllRemoveLaterNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.TempCatchAllRemoveLaterNew = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxAverageDepth_m":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.AverageDepth_m)
                                {
                                    CurrentInfrastructure.AverageDepth_mNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.AverageDepth_mNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.AverageDepth_mNew = null;
                                tb.Text = CurrentInfrastructure.AverageDepth_m.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxNumberOfPorts":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.NumberOfPorts)
                                {
                                    CurrentInfrastructure.NumberOfPortsNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.NumberOfPortsNew = TempInt;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.NumberOfPortsNew = null;
                                tb.Text = CurrentInfrastructure.NumberOfPorts.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxPortDiameter_m":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.PortDiameter_m)
                                {
                                    CurrentInfrastructure.PortDiameter_mNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.PortDiameter_mNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PortDiameter_mNew = null;
                                tb.Text = CurrentInfrastructure.PortDiameter_m.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxPortSpacing_m":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.PortSpacing_m)
                                {
                                    CurrentInfrastructure.PortSpacing_mNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.PortSpacing_mNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PortSpacing_mNew = null;
                                tb.Text = CurrentInfrastructure.PortSpacing_m.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxPortElevation_m":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.PortElevation_m)
                                {
                                    CurrentInfrastructure.PortElevation_mNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.PortElevation_mNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PortElevation_mNew = null;
                                tb.Text = CurrentInfrastructure.PortElevation_m.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxVerticalAngle_deg":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.VerticalAngle_deg)
                                {
                                    CurrentInfrastructure.VerticalAngle_degNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.VerticalAngle_degNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.VerticalAngle_degNew = null;
                                tb.Text = CurrentInfrastructure.VerticalAngle_deg.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxHorizontalAngle_deg":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.HorizontalAngle_deg)
                                {
                                    CurrentInfrastructure.HorizontalAngle_degNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.HorizontalAngle_degNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.HorizontalAngle_degNew = null;
                                tb.Text = CurrentInfrastructure.HorizontalAngle_deg.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxDecayRate_per_day":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.DecayRate_per_day)
                                {
                                    CurrentInfrastructure.DecayRate_per_dayNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.DecayRate_per_dayNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.DecayRate_per_dayNew = null;
                                tb.Text = CurrentInfrastructure.DecayRate_per_day.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxNearFieldVelocity_m_s":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.NearFieldVelocity_m_s)
                                {
                                    CurrentInfrastructure.NearFieldVelocity_m_sNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.NearFieldVelocity_m_sNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.NearFieldVelocity_m_sNew = null;
                                tb.Text = CurrentInfrastructure.NearFieldVelocity_m_s.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxFarFieldVelocity_m_s":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.FarFieldVelocity_m_s)
                                {
                                    CurrentInfrastructure.FarFieldVelocity_m_sNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.FarFieldVelocity_m_sNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.FarFieldVelocity_m_sNew = null;
                                tb.Text = CurrentInfrastructure.FarFieldVelocity_m_s.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxReceivingWaterSalinity_PSU":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.ReceivingWaterSalinity_PSU)
                                {
                                    CurrentInfrastructure.ReceivingWaterSalinity_PSUNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.ReceivingWaterSalinity_PSUNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.ReceivingWaterSalinity_PSUNew = null;
                                tb.Text = CurrentInfrastructure.ReceivingWaterSalinity_PSU.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxReceivingWaterTemperature_C":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.ReceivingWaterTemperature_C)
                                {
                                    CurrentInfrastructure.ReceivingWaterTemperature_CNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.ReceivingWaterTemperature_CNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.ReceivingWaterTemperature_CNew = null;
                                tb.Text = CurrentInfrastructure.ReceivingWaterTemperature_C.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxReceivingWater_MPN_per_100ml":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.ReceivingWater_MPN_per_100ml)
                                {
                                    CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew = TempInt;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew = null;
                                tb.Text = CurrentInfrastructure.ReceivingWater_MPN_per_100ml.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxDistanceFromShore_m":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentInfrastructure.DistanceFromShore_m)
                                {
                                    CurrentInfrastructure.DistanceFromShore_mNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.DistanceFromShore_mNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.DistanceFromShore_mNew = null;
                                tb.Text = CurrentInfrastructure.DistanceFromShore_m.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxSeeOtherTVItemID":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.SeeOtherTVItemID)
                                {
                                    CurrentInfrastructure.SeeOtherTVItemIDNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.SeeOtherTVItemIDNew = TempInt;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.SeeOtherTVItemIDNew = null;
                                tb.Text = CurrentInfrastructure.SeeOtherTVItemID.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxPumpsToTVItemID":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.PumpsToTVItemID)
                                {
                                    CurrentInfrastructure.PumpsToTVItemIDNew = null;
                                }
                                else
                                {
                                    CurrentInfrastructure.PumpsToTVItemIDNew = TempInt;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PumpsToTVItemIDNew = null;
                                tb.Text = CurrentInfrastructure.PumpsToTVItemID.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            SaveMunicipalityTextFile();
        }
        public void SavePolSourceSiteInfo()
        {
            foreach (Control control in PanelViewAndEdit.Controls)
            {
                switch (control.Name)
                {
                    case "comboBoxStreetType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentPSS.PSSAddressNew.StreetType = null;
                                }
                                else
                                {
                                    for (int i = 1, count = Enum.GetNames(typeof(StreetTypeEnum)).Count(); i < count; i++)
                                    {
                                        if ((string)cb.SelectedItem == ((StreetTypeEnum)i).ToString())
                                        {
                                            if (CurrentPSS.PSSAddress.StreetType == i)
                                            {
                                                CurrentPSS.PSSAddressNew.StreetType = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                                CurrentPSS.PSSAddressNew.StreetType = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "textBoxPostalCode":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentPSS.PSSAddress.PostalCode == tb.Text)
                                {
                                    CurrentPSS.PSSAddressNew.PostalCode = null;
                                }
                                else
                                {
                                    CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                    CurrentPSS.PSSAddressNew.PostalCode = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxLat":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentPSS.Lat)
                                {
                                    CurrentPSS.LatNew = null;
                                }
                                else
                                {
                                    CurrentPSS.LatNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentPSS.LatNew = null;
                                tb.Text = CurrentPSS.Lat.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxLng":
                        {
                            TextBox tb = (TextBox)control;

                            if (float.TryParse(tb.Text, out float TempFloat))
                            {
                                if (TempFloat == CurrentPSS.Lng)
                                {
                                    CurrentPSS.LngNew = null;
                                }
                                else
                                {
                                    CurrentPSS.LngNew = TempFloat;
                                    IsDirty = true;
                                }
                            }
                            else
                            {
                                CurrentPSS.LngNew = null;
                                tb.Text = CurrentPSS.Lng.ToString();
                                EmitStatus(new StatusEventArgs("Please enter a valid number for Lat"));
                            }
                        }
                        break;
                    case "textBoxMunicipality":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentPSS.PSSAddress.Municipality == tb.Text)
                                {
                                    CurrentPSS.PSSAddressNew.Municipality = null;
                                }
                                else
                                {
                                    CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                    CurrentPSS.PSSAddressNew.Municipality = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxStreetName":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentPSS.PSSAddress.StreetName == tb.Text)
                                {
                                    CurrentPSS.PSSAddressNew.StreetName = null;
                                }
                                else
                                {
                                    CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                    CurrentPSS.PSSAddressNew.StreetName = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxStreetNumber":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentPSS.PSSAddress.StreetNumber == tb.Text)
                                {
                                    CurrentPSS.PSSAddressNew.StreetNumber = null;
                                }
                                else
                                {
                                    CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                    CurrentPSS.PSSAddressNew.StreetNumber = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxTVText":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentPSS.TVText == tb.Text)
                                {
                                    CurrentPSS.TVTextNew = null;
                                }
                                else
                                {
                                    CurrentPSS.TVTextNew = tb.Text;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            SaveSubsectorTextFile();
        }
        private void UpdatePolSourceSitePanelColor()
        {
            if (IsPolSourceSite)
            {
                foreach (var a in PanelPolSourceSite.Controls)
                {
                    if (a.GetType().Name == "Panel")
                    {
                        ((Panel)a).BackColor = BackColorNormal;

                        int PSSTVItemID = int.Parse(((Panel)a).Tag.ToString());
                        PSS pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PSSTVItemID).FirstOrDefault();
                        if (pss != null)
                        {
                            foreach (Issue issue in pss.PSSObs.IssueList)
                            {
                                if (issue.IsWellFormed == false)
                                {
                                    ((Panel)a).BackColor = BackColorNotWellFormed;
                                    break;
                                }
                                if (issue.IsCompleted == false)
                                {
                                    ((Panel)a).BackColor = BackColorNotCompleted;
                                    break;
                                }
                            }
                        }

                        if (((Panel)a).Tag.ToString() == PolSourceSiteTVItemID.ToString())
                        {
                            ((Panel)a).BackColor = BackColorEditing;
                        }
                    }
                }
            }
            else
            {
                foreach (var a in PanelPolSourceSite.Controls)
                {
                    if (a.GetType().Name == "Panel")
                    {
                        ((Panel)a).BackColor = BackColorNormal;

                        if (((Panel)a).Tag.ToString() == InfrastructureTVItemID.ToString())
                        {
                            ((Panel)a).BackColor = BackColorEditing;
                        }
                    }
                }
            }

        }
    }
}
