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
                lblItemNew.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));

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

                        Label lblUndersandWhatWillBeSentToDB = new Label();
                        lblUndersandWhatWillBeSentToDB.AutoSize = true;
                        lblUndersandWhatWillBeSentToDB.Location = new Point(30, Y);
                        lblUndersandWhatWillBeSentToDB.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblUndersandWhatWillBeSentToDB.Font = new Font(new FontFamily(lblUndersandWhatWillBeSentToDB.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);

                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"Here are all the thing that will be sent to the CSSPWebTools and stored in it's DB");
                        sb.AppendLine("");
                        sb.AppendLine($"{NeedDetailsUpdateText}");
                        sb.AppendLine("");
                        if (CurrentPSS.LatNew != null)
                        {
                            sb.AppendLine("     Lat       (changed)");
                        }
                        if (CurrentPSS.LngNew != null)
                        {
                            sb.AppendLine("     Lng       (changed)");
                        }
                        if (CurrentPSS.IsActiveNew != null)
                        {
                            sb.AppendLine("     IsActive       (changed)");
                        }
                        if (CurrentPSS.IsPointSourceNew != null)
                        {
                            sb.AppendLine("     IsPointSource       (changed)");
                        }
                        if (CurrentPSS.PSSAddressNew.AddressType != null
                            || CurrentPSS.PSSAddressNew.Municipality != null
                            || CurrentPSS.PSSAddressNew.PostalCode != null
                            || CurrentPSS.PSSAddressNew.StreetName != null
                            || CurrentPSS.PSSAddressNew.StreetNumber != null
                            || CurrentPSS.PSSAddressNew.StreetType != null)
                        {
                            NeedDetailsUpdate = true;
                        }
                        if (CurrentPSS.PSSAddressNew.AddressTVItemID != null)
                        {
                            sb.AppendLine("     Address       (changed)");
                        }
                        if (CurrentPSS.PSSObs.ObsDateNew != null)
                        {
                            sb.AppendLine("     ObsDate       (changed)");
                        }

                        sb.AppendLine("");
                        sb.AppendLine($"{NeedIssuesUpdateText}");
                        sb.AppendLine("");

                        int count = 0;
                        foreach (Issue issue in CurrentPSS.PSSObs.IssueList.OrderBy(c => c.Ordinal))
                        {
                            count += 1;
                            if (issue.PolSourceObsInfoIntListNew.Count > 0 || issue.ToRemove == true)
                            {
                                if (issue.ToRemove == true)
                                {
                                    sb.AppendLine($"     Issue {count}          (to be removed)");
                                }
                                else
                                {
                                    sb.AppendLine($"     Issue {count}          (changed)");
                                }
                            }
                        }

                        sb.AppendLine("");
                        sb.AppendLine($"{NeedPictuesUpdateText}");
                        sb.AppendLine("");

                        count = 0;
                        foreach (Picture picture in CurrentPSS.PSSPictureList)
                        {
                            count += 1;
                            if (picture.DescriptionNew != null
                                || picture.ExtensionNew != null
                                || picture.FileNameNew != null
                                || picture.ToRemove != null)
                            {
                                if (picture.ToRemove != null && picture.ToRemove == true)
                                {
                                    sb.AppendLine($"     Picture {count}          (to be removed)");
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(picture.FileNameNew)
                                        || !string.IsNullOrWhiteSpace(picture.ExtensionNew)
                                        || !string.IsNullOrWhiteSpace(picture.DescriptionNew))
                                    {
                                        sb.AppendLine($"     Picture {count}          (filename and/or description changed)");
                                    }
                                }
                            }
                        }

                        lblUndersandWhatWillBeSentToDB.Text = sb.ToString();

                        PanelViewAndEdit.Controls.Add(lblUndersandWhatWillBeSentToDB);

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
                        lblReturns.Font = new Font(new FontFamily(lblUndersandWhatWillBeSentToDB.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
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
                                            CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                            CurrentInfrastructure.InfrastructureAddressNew.StreetType = i;
                                            IsDirty = true;
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
                                            CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                            CurrentPSS.PSSAddressNew.StreetType = i;
                                            IsDirty = true;
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
