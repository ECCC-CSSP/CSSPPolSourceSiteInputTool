using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputToolHelper
{
    public partial class PolSourceSiteInputToolHelper
    {
        private void ChangeTextValue(string TextBoxName, string NewText)
        {
            foreach (Control control in PanelViewAndEdit.Controls)
            {
                if (control.Name == TextBoxName)
                {
                    control.Text = NewText;
                }
            }
        }
        private void DrawItemAddress(int x, int y, Address address, Address addressNew, bool IsMunicipality)
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

                textBoxStreetNumber.TextChanged += textBoxStreetNumber_TextChanged;

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

                textBoxStreetName.TextChanged += textBoxStreetName_TextChanged;

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
            lblStreetTypeText.Text = $@"Street Type   ";
            if (IsEditing)
            {
                lblStreetTypeText.Font = new Font(new FontFamily(lblStreetTypeText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold | FontStyle.Underline);
                lblStreetTypeText.ForeColor = Color.Blue;
                lblStreetTypeText.Click += lblStreetTypeText_Click;
            }
            else
            {
                lblStreetTypeText.Font = new Font(new FontFamily(lblStreetTypeText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblStreetTypeText.ForeColor = Color.Black;
            }

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

                TextBox textBoxStreetType = new TextBox();
                textBoxStreetType.Location = new Point(lblStreetTypeText.Left, currentTop);
                textBoxStreetType.Name = "textBoxStreetType";
                textBoxStreetType.Font = new Font(new FontFamily(lblStreetTypeText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textBoxStreetType.Width = lblStreetTypeText.Width;
                textBoxStreetType.Enabled = false;

                if (addressNew.StreetType != null)
                {
                    textBoxStreetType.Text = ((StreetTypeEnum)addressNew.StreetType).ToString();
                }
                else
                {
                    if (address.StreetType != null)
                    {
                        textBoxStreetType.Text = ((StreetTypeEnum)address.StreetType).ToString();
                    }
                    else
                    {
                        textBoxStreetType.Text = "";
                    }
                }

                textBoxStreetType.TextChanged += textBoxStreetType_TextChanged;

                PanelViewAndEdit.Controls.Add(textBoxStreetType);

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
            lblMunicipalityText.Text = $@"Municipality        ";
            if (IsMunicipality)
            {
                lblMunicipalityText.Font = new Font(new FontFamily(lblMunicipalityText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblMunicipalityText.ForeColor = Color.Black;
            }
            else
            {
                if (IsEditing)
                {
                    lblMunicipalityText.Font = new Font(new FontFamily(lblMunicipalityText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold | FontStyle.Underline);
                    lblMunicipalityText.ForeColor = Color.Blue;
                    lblMunicipalityText.Click += lblMunicipalityText_Click;
                }
                else
                {
                    lblMunicipalityText.Font = new Font(new FontFamily(lblMunicipalityText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblMunicipalityText.ForeColor = Color.Black;
                }
            }

            PanelViewAndEdit.Controls.Add(lblMunicipalityText);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            if (IsEditing)
            {
                int currentTop = lblMunicipalityText.Bottom + 4;

                if (!IsMunicipality)
                {
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
                }

                TextBox textBoxMunicipality = new TextBox();
                textBoxMunicipality.Location = new Point(lblMunicipalityText.Left, currentTop);
                textBoxMunicipality.Name = "textBoxMunicipality";
                textBoxMunicipality.Font = new Font(new FontFamily(lblMunicipalityText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textBoxMunicipality.Width = lblMunicipalityText.Width;
                textBoxMunicipality.Enabled = true;
                if (IsMunicipality)
                {
                    textBoxMunicipality.Text = municipalityDoc.Municipality.MunicipalityName;
                    textBoxMunicipality.Enabled = false;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(addressNew.Municipality))
                    {
                        textBoxMunicipality.Text = address.Municipality;
                    }
                    else
                    {
                        textBoxMunicipality.Text = addressNew.Municipality;
                    }
                }

                textBoxMunicipality.TextChanged += textBoxMunicipality_TextChanged;

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
                if (IsMunicipality)
                {
                    lblMunicipality.Text = string.IsNullOrWhiteSpace(municipalityDoc.Municipality.MunicipalityName) ? "(empty)" : $"({municipalityDoc.Municipality.MunicipalityName})";
                }
                else
                {
                    lblMunicipality.Text = string.IsNullOrWhiteSpace(address.Municipality) ? "(empty)" : $"({address.Municipality})";
                }

                PanelViewAndEdit.Controls.Add(lblMunicipality);

                //y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                if (!IsMunicipality)
                {
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

                textBoxPostalCode.TextChanged += textBoxPostalCode_TextChanged;

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

            int MaxY = 0;
            foreach (Control control in PanelViewAndEdit.Controls)
            {
                if (control.Bottom > MaxY)
                {
                    MaxY = control.Bottom;
                }
            }

            y = MaxY + 20;
            //if (!MunicipalityExist)
            //{
            //    string TheMunicipality = addressNew.Municipality == null ? address.Municipality : addressNew.Municipality;

            //    Label lblCreateMunicipality = new Label();
            //    lblCreateMunicipality.AutoSize = true;
            //    lblCreateMunicipality.Location = new Point(20, y);
            //    lblCreateMunicipality.Font = new Font(new FontFamily(lblCreateMunicipality.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            //    lblCreateMunicipality.Text = $@"Municipality [{TheMunicipality}] does not exist. Create Municipality ?";

            //    PanelViewAndEdit.Controls.Add(lblCreateMunicipality);

            //    CheckBox checkBoxCreateMunicipality = new CheckBox();
            //    checkBoxCreateMunicipality.AutoSize = true;
            //    checkBoxCreateMunicipality.Location = new Point(lblCreateMunicipality.Right + 5, y);
            //    checkBoxCreateMunicipality.Name = "checkBoxCreateMunicipality";
            //    checkBoxCreateMunicipality.Font = new Font(new FontFamily(lblPostalCodeText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
            //    checkBoxCreateMunicipality.CheckedChanged += checkBoxCreateMunicipality_CheckedChanged;
            //    checkBoxCreateMunicipality.Checked = CreateMunicipality;

            //    PanelViewAndEdit.Controls.Add(checkBoxCreateMunicipality);

            //    y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
            //}

            #endregion Address
        }
        private void DrawItemBool(int x, int y, bool? val, bool? valNew, string lblTxt, string checkBoxName)
        {
            Label lblItem = new Label();
            lblItem.AutoSize = true;
            lblItem.Location = new Point(x, y);
            lblItem.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem.ForeColor = Color.Blue;
            lblItem.Text = $@"{lblTxt}: ";
            lblItem.Click += ShowHelpDocument;
            lblItem.Tag = lblTxt.Replace(" ", "_");

            PanelViewAndEdit.Controls.Add(lblItem);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            string oldVal = "";
            if (valNew != null)
            {
                if (val != valNew)
                {
                    if (val == null)
                    {
                        oldVal = $"(empty)";
                    }
                    else
                    {
                        oldVal = $"({((bool)val).ToString()})";
                    }
                }
            }


            Label lblItem2 = new Label();
            lblItem2.AutoSize = true;
            lblItem2.Location = new Point(x, y);
            lblItem2.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem2.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            lblItem2.Text = $"{oldVal}";

            PanelViewAndEdit.Controls.Add(lblItem2);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            if (IsEditing)
            {
                CheckBox checkBoxItem = new CheckBox();
                checkBoxItem.Location = new Point(x, y);
                checkBoxItem.Name = $"{checkBoxName}";
                checkBoxItem.Font = new Font(new FontFamily(checkBoxItem.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                checkBoxItem.Width = 100;

                if (!(valNew == null && val == null))
                {
                    if (valNew == null)
                    {
                        checkBoxItem.Checked = (bool)val;
                    }
                    else
                    {
                        checkBoxItem.Checked = (bool)valNew;
                    }
                }
                checkBoxItem.Text = "";

                if (checkBoxItem.Name == "checkBoxIsMechanicallyAerated")
                {
                    checkBoxItem.CheckedChanged += SaveAndRedraw;
                }
                if (checkBoxItem.Name == "checkBoxCanOverflow")
                {
                    checkBoxItem.CheckedChanged += checkBoxCanOverFlow_CheckedChanged;
                }
                if (checkBoxItem.Name == "checkBoxHasBackupPower")
                {
                    checkBoxItem.CheckedChanged += checkBoxHasBackupPower_CheckedChanged;
                }

                PanelViewAndEdit.Controls.Add(checkBoxItem);
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
        private void DrawItemEnum(int x, int y, int? val, int? valNew, string lblTxt, string comboBoxName, Type enumType, int item)
        {
            Label lblItemEnum = new Label();
            lblItemEnum.AutoSize = true;
            lblItemEnum.Location = new Point(x, y);
            lblItemEnum.Font = new Font(new FontFamily(lblItemEnum.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItemEnum.ForeColor = Color.Blue;
            lblItemEnum.Click += ShowHelpDocument;
            lblItemEnum.Tag = lblTxt.Replace(" ", "_");

            switch (enumType.Name)
            {
                case "InfrastructureTypeEnum":
                    {
                        lblItemEnum.Text = $@"Infrastructure Type: ";
                    }
                    break;
                case "FacilityTypeEnum":
                    {
                        lblItemEnum.Text = $@"Facility Type: ";
                    }
                    break;
                case "AerationTypeEnum":
                    {
                        lblItemEnum.Text = $@"Aeration Type: ";
                    }
                    break;
                case "PreliminaryTreatmentTypeEnum":
                    {
                        lblItemEnum.Text = $@"Preliminary Treatment Type: ";
                    }
                    break;
                case "PrimaryTreatmentTypeEnum":
                    {
                        lblItemEnum.Text = $@"Primary Treatment Type: ";
                    }
                    break;
                case "SecondaryTreatmentTypeEnum":
                    {
                        lblItemEnum.Text = $@"Secondary Treatment Type: ";
                    }
                    break;
                case "TertiaryTreatmentTypeEnum":
                    {
                        lblItemEnum.Text = $@"Tertiary Treatment Type: ";
                    }
                    break;
                case "TreatmentTypeEnum":
                    {
                        lblItemEnum.Text = $@"Treatment Type: ";
                    }
                    break;
                case "DisinfectionTypeEnum":
                    {
                        lblItemEnum.Text = $@"Disinfection Type: ";
                    }
                    break;
                case "CollectionSystemTypeEnum":
                    {
                        lblItemEnum.Text = $@"Collection System Type: ";
                    }
                    break;
                case "AlarmSystemTypeEnum":
                    {
                        lblItemEnum.Text = $@"Alarm System Type: ";
                    }
                    break;
                case "CanOverflowTypeEnum":
                    {
                        lblItemEnum.Text = $@"Can Overflow: ";
                    }
                    break;
                case "ValveTypeEnum":
                    {
                        lblItemEnum.Text = $@"Valve Type: ";
                    }
                    break;
                case "ContactTitleEnum":
                    {
                        lblItemEnum.Text = $@"Contact Title: ";
                    }
                    break;
                case "TelTypeEnum":
                    {
                        lblItemEnum.Text = $@"Telephone Type: ";
                    }
                    break;
                case "EmailTypeEnum":
                    {
                        lblItemEnum.Text = $@"Email Type: ";
                    }
                    break;
                default:
                    break;
            }

            PanelViewAndEdit.Controls.Add(lblItemEnum);

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
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_InfrastructureTypeEnum((InfrastructureTypeEnum)val).ToString()})";
                            }
                            break;
                        case "FacilityTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_FacilityTypeEnum((FacilityTypeEnum)val).ToString()})";
                            }
                            break;
                        case "AerationTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_AerationTypeEnum((AerationTypeEnum)val).ToString()})";
                            }
                            break;
                        case "PreliminaryTreatmentTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_PreliminaryTreatmentTypeEnum((PreliminaryTreatmentTypeEnum)val).ToString()})";
                            }
                            break;
                        case "PrimaryTreatmentTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_PrimaryTreatmentTypeEnum((PrimaryTreatmentTypeEnum)val).ToString()})";
                            }
                            break;
                        case "SecondaryTreatmentTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_SecondaryTreatmentTypeEnum((SecondaryTreatmentTypeEnum)val).ToString()})";
                            }
                            break;
                        case "TertiaryTreatmentTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_TertiaryTreatmentTypeEnum((TertiaryTreatmentTypeEnum)val).ToString()})";
                            }
                            break;
                        case "TreatmentTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_TreatmentTypeEnum((TreatmentTypeEnum)val).ToString()})";
                            }
                            break;
                        case "DisinfectionTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_DisinfectionTypeEnum((DisinfectionTypeEnum)val).ToString()})";
                            }
                            break;
                        case "CollectionSystemTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_CollectionSystemTypeEnum((CollectionSystemTypeEnum)val).ToString()})";
                            }
                            break;
                        case "AlarmSystemTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_AlarmSystemTypeEnum((AlarmSystemTypeEnum)val).ToString()})";
                            }
                            break;
                        case "CanOverflowTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_CanOverflowTypeEnum((CanOverflowTypeEnum)val).ToString()})";
                            }
                            break;
                        case "ValveTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_ValveTypeEnum((ValveTypeEnum)val).ToString()})";
                            }
                            break;
                        case "ContactTitleEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_ContactTitleEnum((ContactTitleEnum)val).ToString()})";
                            }
                            break;
                        case "TelTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_TelTypeEnum((TelTypeEnum)val).ToString()})";
                            }
                            break;
                        case "EmailTypeEnum":
                            {
                                lblItemEnumOld.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_EmailTypeEnum((EmailTypeEnum)val).ToString()})";
                            }
                            break;
                        default:
                            break;
                    }

                    PanelViewAndEdit.Controls.Add(lblItemEnumOld);

                    x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                }

                x = 30;

                y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                string fontFamilyName = lblItemEnum.Font.FontFamily.Name;
                switch (enumType.Name)
                {
                    case "InfrastructureTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)InfrastructureTypeEnum.WWTP),
                                "butInfrastructureTypeWWTP", "WWTP", fontFamilyName, "WWTP", butInfrastructureTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)InfrastructureTypeEnum.LiftStation),
                                "butInfrastructureTypeLiftStation", "Lift Station", fontFamilyName, "LiftStation", butInfrastructureTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)InfrastructureTypeEnum.LineOverflow),
                                "butInfrastructureTypeLineOverflow", "Line Overflow", fontFamilyName, "LineOverflow", butInfrastructureTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)InfrastructureTypeEnum.SeeOtherMunicipality),
                                "butInfrastructureTypeSeeOtherMunicipality", "See Other Municipality", fontFamilyName, "SeeOtherMunicipality", butInfrastructureTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)InfrastructureTypeEnum.Other),
                                "butInfrastructureTypeOther", "Other", fontFamilyName, "Other", butInfrastructureTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butInfrastructureTypeNull", "None", fontFamilyName, "None", butInfrastructureTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                        }
                        break;
                    case "FacilityTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)FacilityTypeEnum.Lagoon),
                                "butFacilityTypeLagoon", "Lagoon", fontFamilyName, "Lagoon", butFacilityTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)FacilityTypeEnum.Plant),
                                "butFacilityTypePlant", "Plant", fontFamilyName, "Plant", butFacilityTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butFacilityTypeNull", "None", fontFamilyName, "None", butFacilityTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                        }
                        break;
                    case "AerationTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)AerationTypeEnum.MechanicalAirLines),
                                "butAerationTypeMechanicalAirLines", "Mechanical Air Lines", fontFamilyName, "MechanicalAirLines", butAerationTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)AerationTypeEnum.MechanicalSurfaceMixers),
                                "butAerationTypeMechanicalSurfaceMixers", "MechanicalSurfaceMixers", fontFamilyName, "MechanicalSurfaceMixers", butAerationTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butAerationTypeNull", "None", fontFamilyName, "None", butAerationTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                        }
                        break;
                    case "PreliminaryTreatmentTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)PreliminaryTreatmentTypeEnum.BarScreen),
                                "butPreliminaryTreatmentTypeBarScreen", "Bar Screen", fontFamilyName, "BarScreen", butPreliminaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)PreliminaryTreatmentTypeEnum.Grinder),
                                "butPreliminaryTreatmentTypeGrinder", "Grinder", fontFamilyName, "Grinder", butPreliminaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)PreliminaryTreatmentTypeEnum.MechanicalScreening),
                                "butPreliminaryTreatmentTypeMechanicalScreening", "Mechanical Screening", fontFamilyName, "MechanicalScreening", butPreliminaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butPreliminaryTreatmentTypeNotApplicable", "Not Applicable", fontFamilyName, "NotApplicable", butPreliminaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                        }
                        break;
                    case "PrimaryTreatmentTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)PrimaryTreatmentTypeEnum.ChemicalCoagulation),
                                "butPrimaryTreatmentTypeChemicalCoagulation", "Chemical Coagulation", fontFamilyName, "ChemicalCoagulation", butPrimaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)PrimaryTreatmentTypeEnum.Filtration),
                                "butPrimaryTreatmentTypeFiltration", "Filtration", fontFamilyName, "Filtration", butPrimaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)PrimaryTreatmentTypeEnum.PrimaryClarification),
                                "butPrimaryTreatmentTypePrimaryClarification", "Primary Clarification", fontFamilyName, "PrimaryClarification", butPrimaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)PrimaryTreatmentTypeEnum.Sedimentation),
                                "butPrimaryTreatmentTypeSedimentation", "Sedimentation", fontFamilyName, "Sedimentation", butPrimaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butPrimaryTreatmentTypeNotApplicable", "Not Applicable", fontFamilyName, "NotApplicable", butPrimaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                        }
                        break;
                    case "SecondaryTreatmentTypeEnum":
                        {
                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butSecondaryTreatmentTypeNotApplicable", "Not Applicable", fontFamilyName, "NotApplicable", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 20;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            Label lblAttachedGrowthGroup = new Label();
                            lblAttachedGrowthGroup.AutoSize = true;
                            lblAttachedGrowthGroup.Location = new Point(x, y);
                            lblAttachedGrowthGroup.Font = new Font(new FontFamily(fontFamilyName).Name, 10f, FontStyle.Bold);
                            lblAttachedGrowthGroup.Text = "Attached Growth Group";

                            PanelViewAndEdit.Controls.Add(lblAttachedGrowthGroup);

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.AeratedSubmergedBioFilmReactor),
                                "butSecondaryTreatmentTypeAeratedSubmergedBioFilmReactor", "Aerated Submerged Bio Film Reactor", fontFamilyName, "AeratedSubmergedBioFilmReactor", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.BiologicalAearatedFilters),
                                "butSecondaryTreatmentTypeBiologicalAearatedFilters", "Biological Aearated Filters", fontFamilyName, "BiologicalAearatedFilters", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.IntegratedFixedFilmActivatedSludge),
                                "butSecondaryTreatmentTypeIntegratedFixedFilmActivatedSludge", "Integrated Fixed Film Activated Sludge", fontFamilyName, "IntegratedFixedFilmActivatedSludge", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.MovingBedBioReactor),
                                "butSecondaryTreatmentTypeMovingBedBioReactor", "Moving Bed BioReactor", fontFamilyName, "MovingBedBioReactor", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.RotatingBiologicalContactor),
                                "butSecondaryTreatmentTypeRotatingBiologicalContactor", "Rotating Biological Contactor (RBC)", fontFamilyName, "RotatingBiologicalContactor", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.TricklingFilters),
                                "butSecondaryTreatmentTypeTricklingFilters", "Trickling Filters", fontFamilyName, "TricklingFilters", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 20;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            Label lblSuspendedGrowthGroup = new Label();
                            lblSuspendedGrowthGroup.AutoSize = true;
                            lblSuspendedGrowthGroup.Location = new Point(x, y);
                            lblSuspendedGrowthGroup.Font = new Font(new FontFamily(fontFamilyName).Name, 10f, FontStyle.Bold);
                            lblSuspendedGrowthGroup.Text = "Suspended Growth Group";

                            PanelViewAndEdit.Controls.Add(lblSuspendedGrowthGroup);

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.ActivatedSludge),
                                "butSecondaryTreatmentTypeActivatedSludge", "Activated Sludge", fontFamilyName, "ActivatedSludge", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.ContactStabilization),
                                "butSecondaryTreatmentTypeContactStabilization", "Contact Stabilization", fontFamilyName, "ContactStabilization", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.ExtendedAeration),
                                "butSecondaryTreatmentTypeExtendedAeration", "Extended Aeration", fontFamilyName, "ExtendedAeration", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.ExtendedActivatedSludge),
                                "butSecondaryTreatmentTypeExtendedActivatedSludge", "Extended Activated Sludge", fontFamilyName, "ExtendedActivatedSludge", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.OxidationDitch),
                                "butSecondaryTreatmentTypeOxidationDitch", "Oxidation Ditch", fontFamilyName, "OxidationDitch", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.PhysicalChemicalProcesses),
                                "butSecondaryTreatmentTypePhysicalChemicalProcesses", "Physical Chemical Processes", fontFamilyName, "PhysicalChemicalProcesses", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)SecondaryTreatmentTypeEnum.SequencingBatchReactor),
                                "butSecondaryTreatmentTypeSequencingBatchReactor", "Sequencing Batch Reactor", fontFamilyName, "SequencingBatchReactor", butSecondaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                        }
                        break;
                    case "TertiaryTreatmentTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butTertiaryTreatmentTypeNotApplicable", "Not Applicable", fontFamilyName, "NotApplicable", butTertiaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TertiaryTreatmentTypeEnum.Adsorption),
                                "butTertiaryTreatmentTypeAdsorption", "Adsorption", fontFamilyName, "Adsorption", butTertiaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TertiaryTreatmentTypeEnum.BiologicalNutrientRemoval),
                                "butTertiaryTreatmentTypeBiologicalNutrientRemoval", "Biological Nutrient Removal", fontFamilyName, "BiologicalNutrientRemoval", butTertiaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TertiaryTreatmentTypeEnum.Flocculation),
                                "butTertiaryTreatmentTypeFlocculation", "Flocculation", fontFamilyName, "Flocculation", butTertiaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TertiaryTreatmentTypeEnum.IonExchange),
                                "butTertiaryTreatmentTypeIonExchange", "Ion Exchange", fontFamilyName, "IonExchange", butTertiaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TertiaryTreatmentTypeEnum.MembraneFiltration),
                                "butTertiaryTreatmentTypeMembraneFiltration", "Membrane Filtration", fontFamilyName, "MembraneFiltration", butTertiaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TertiaryTreatmentTypeEnum.ReverseOsmosis),
                                "butTertiaryTreatmentTypeReverseOsmosis", "Reverse Osmosis", fontFamilyName, "ReverseOsmosis", butTertiaryTreatmentTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                        }
                        break;
                    case "DisinfectionTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butDisinfectionTypeNone", "None", fontFamilyName, "None", butDisinfectionTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)DisinfectionTypeEnum.UV),
                                "butDisinfectionTypeUV", "UV", fontFamilyName, "UV", butDisinfectionTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)DisinfectionTypeEnum.UVSeasonal),
                                "butDisinfectionTypeUVSeasonal", "UV Seasonal", fontFamilyName, "UVSeasonal", butDisinfectionTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)DisinfectionTypeEnum.ChlorinationNoDechlorination),
                                "butDisinfectionTypeChlorinationNoDechlorination", "Chlorination No Dechlorination", fontFamilyName, "ChlorinationNoDechlorination", butDisinfectionTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)DisinfectionTypeEnum.ChlorinationNoDechlorinationSeasonal),
                                "butDisinfectionTypeChlorinationNoDechlorinationSeasonal", "Chlorination No Dechlorination Seasonal", fontFamilyName, "ChlorinationNoDechlorinationSeasonal", butDisinfectionTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)DisinfectionTypeEnum.ChlorinationWithDechlorination),
                                "butDisinfectionTypeChlorinationWithDechlorination", "Chlorination With Dechlorination", fontFamilyName, "ChlorinationWithDechlorination", butDisinfectionTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)DisinfectionTypeEnum.ChlorinationWithDechlorinationSeasonal),
                                "butDisinfectionTypeChlorinationWithDechlorinationSeasonal", "Chlorination With Dechlorination Seasonal", fontFamilyName, "ChlorinationWithDechlorinationSeasonal", butDisinfectionTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                        }
                        break;
                    case "CollectionSystemTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butCollectionSystemTypeNone", "None", fontFamilyName, "None", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.CompletelyCombined),
                                "butCollectionSystemTypeCompletelyCombined", "Completely Combined", fontFamilyName, "CompletelyCombined", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.CompletelySeparated),
                                "butCollectionSystemTypeCompletelySeparated", "Completely Separated", fontFamilyName, "CompletelySeparated", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.Combined10Separated90),
                                "butCollectionSystemTypeCombined10Separated90", "Combined 10% Separated 90%", fontFamilyName, "Combined10Separated90", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.Combined20Separated80),
                                "butCollectionSystemTypeCombined20Separated80", "Combined 20% Separated 80%", fontFamilyName, "Combined20Separated80", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.Combined30Separated70),
                                "butCollectionSystemTypeCombined20Separated80", "Combined 20% Separated 80%", fontFamilyName, "Combined20Separated80", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.Combined30Separated70),
                                "butCollectionSystemTypeCombined30Separated70", "Combined 30% Separated 70%", fontFamilyName, "Combined20Separated80", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.Combined40Separated60),
                                "butCollectionSystemTypeCombined40Separated60", "Combined 40% Separated 60%", fontFamilyName, "Combined40Separated60", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.Combined50Separated50),
                                "butCollectionSystemTypeCombined50Separated50", "Combined 50% Separated 50%", fontFamilyName, "Combined50Separated50", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.Combined60Separated40),
                                "butCollectionSystemTypeCombined60Separated40", "Combined 60% Separated 40%", fontFamilyName, "Combined60Separated40", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.Combined70Separated30),
                                "butCollectionSystemTypeCombined70Separated30", "Combined 70% Separated 30%", fontFamilyName, "Combined70Separated30", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.Combined80Separated20),
                                "butCollectionSystemTypeCombined80Separated20", "Combined 80% Separated 20%", fontFamilyName, "Combined80Separated20", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CollectionSystemTypeEnum.Combined90Separated10),
                                "butCollectionSystemTypeCombined80Separated20", "Combined 90% Separated 10%", fontFamilyName, "Combined90Separated10", butCollectionSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                        }
                        break;
                    case "AlarmSystemTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)AlarmSystemTypeEnum.None),
                                "butAlarmSystemTypeNone", "None", fontFamilyName, "None", butAlarmSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)AlarmSystemTypeEnum.OnlyVisualLight),
                                "butAlarmSystemTypeOnlyVisualLight", "Only Visual Light", fontFamilyName, "OnlyVisualLight", butAlarmSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)AlarmSystemTypeEnum.PagerAndLight),
                                "butAlarmSystemTypePagerAndLight", "Pager And Light", fontFamilyName, "PagerAndLight", butAlarmSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)AlarmSystemTypeEnum.SCADA),
                                "butAlarmSystemTypeSCADA", "SCADA", fontFamilyName, "SCADA", butAlarmSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)AlarmSystemTypeEnum.SCADAAndLight),
                                "butAlarmSystemTypeSCADAAndLight", "SCADAAndLight", fontFamilyName, "SCADAAndLight", butAlarmSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butAlarmSystemTypeNone", "Unknown", fontFamilyName, "Unknown", butAlarmSystemTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                        }
                        break;
                    case "CanOverflowTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CanOverflowTypeEnum.Yes),
                                "butCanOverflowTypeYes", "Yes", fontFamilyName, "Yes", butCanOverflowTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CanOverflowTypeEnum.No),
                                "butCanOverflowTypeNo", "No", fontFamilyName, "No", butCanOverflowTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)CanOverflowTypeEnum.Unknown),
                                "butCanOverflowTypeUnknown", "Unknown", fontFamilyName, "Unknown", butCanOverflowTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                        }
                        break;
                    case "ValveTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ValveTypeEnum.Manually),
                                "butValveTypeManually", "Manually", fontFamilyName, "Manually", butValveTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ValveTypeEnum.Automatically),
                                "butValveTypeAutomatically", "Automatically", fontFamilyName, "Automatically", butValveTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ValveTypeEnum.None),
                                "butValveTypeManually", "None", fontFamilyName, "None", butValveTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, null,
                                "butValveTypeUnknown", "Unknown", fontFamilyName, "Unknown", butValveTypeSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                        }
                        break;
                    case "ContactTitleEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ContactTitleEnum.DirectorGeneral),
                                "butContactTitleDirectorGeneral", "Director General", fontFamilyName, "Director General", butContactTitleSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ContactTitleEnum.DirectorPublicWorks),
                                "butContactTitleDirectorPublicWorks", "Director Public Works", fontFamilyName, "Director Public Works", butContactTitleSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ContactTitleEnum.Engineer),
                                "butContactTitleEngineer", "Engineer", fontFamilyName, "Engineer", butContactTitleSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ContactTitleEnum.FacilitiesManager),
                                "butContactTitleFacilitiesManager", "Facilities Manager", fontFamilyName, "Facilities Manager", butContactTitleSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ContactTitleEnum.Foreman),
                                "butContactTitleForeman", "Foreman", fontFamilyName, "Foreman", butContactTitleSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            x = 30;
                            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ContactTitleEnum.Operator),
                                "butContactTitleOperator", "Operator", fontFamilyName, "Operator", butContactTitleSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ContactTitleEnum.Superintendent),
                                "butContactTitleSuperintendent", "Superintendent", fontFamilyName, "Superintendent", butContactTitleSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ContactTitleEnum.Supervisor),
                                "butContactTitleSupervisor", "Supervisor", fontFamilyName, "Supervisor", butContactTitleSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ContactTitleEnum.Technician),
                                "butContactTitleTechnician", "Technician", fontFamilyName, "Technician", butContactTitleSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)ContactTitleEnum.Error),
                                "butContactTitleUnknown", "Unknown", fontFamilyName, "Unknown", butContactTitleSelect_Clicked, 0);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                        }
                        break;
                    case "TelTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TelTypeEnum.Mobile),
                                "butTelTypeMobile", "Mobile", fontFamilyName, "Mobile", butTelTypeSelect_Clicked, item);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TelTypeEnum.Mobile2),
                                "butTelTypeMobile2", "Mobile 2", fontFamilyName, "Mobile 2", butTelTypeSelect_Clicked, item);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TelTypeEnum.Personal),
                                "butTelTypePersonal", "Personal", fontFamilyName, "Personal", butTelTypeSelect_Clicked, item);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TelTypeEnum.Personal2),
                                "butTelTypePersonal2", "Personal 2", fontFamilyName, "Personal 2", butTelTypeSelect_Clicked, item);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TelTypeEnum.Work),
                                "butTelTypeWork", "Work", fontFamilyName, "Work", butTelTypeSelect_Clicked, item);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)TelTypeEnum.Work2),
                                "butTelTypeWork2", "Work 2", fontFamilyName, "Work 2", butTelTypeSelect_Clicked, item);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                        }
                        break;
                    case "EmailTypeEnum":
                        {
                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)EmailTypeEnum.Personal),
                                "butEmailTypePersonal", "Personal", fontFamilyName, "Personal", butEmailTypeSelect_Clicked, item);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)EmailTypeEnum.Personal2),
                                "butEmailTypePersonal2", "Personal 2", fontFamilyName, "Personal 2", butEmailTypeSelect_Clicked, item);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)EmailTypeEnum.Work),
                                "butEmailTypeWork", "Work", fontFamilyName, "Work", butEmailTypeSelect_Clicked, item);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                            CreateChoiceButton(x, y, val, valNew, enumType.Name, ((int)EmailTypeEnum.Work2),
                                "butEmailTypeWork2", "Work 2", fontFamilyName, "Work 2", butEmailTypeSelect_Clicked, item);
                            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
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
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_InfrastructureTypeEnum((InfrastructureTypeEnum)val).ToString()})";
                        }
                        break;
                    case "FacilityTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_FacilityTypeEnum((FacilityTypeEnum)val).ToString()})";
                        }
                        break;
                    case "AerationTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_AerationTypeEnum((AerationTypeEnum)val).ToString()})";
                        }
                        break;
                    case "PreliminaryTreatmentTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_PreliminaryTreatmentTypeEnum((PreliminaryTreatmentTypeEnum)val).ToString()})";
                        }
                        break;
                    case "PrimaryTreatmentTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_PrimaryTreatmentTypeEnum((PrimaryTreatmentTypeEnum)val).ToString()})";
                        }
                        break;
                    case "SecondaryTreatmentTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_SecondaryTreatmentTypeEnum((SecondaryTreatmentTypeEnum)val).ToString()})";
                        }
                        break;
                    case "TertiaryTreatmentTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_TertiaryTreatmentTypeEnum((TertiaryTreatmentTypeEnum)val).ToString()})";
                        }
                        break;
                    case "TreatmentTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_TreatmentTypeEnum((TreatmentTypeEnum)val).ToString()})";
                        }
                        break;
                    case "DisinfectionTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_DisinfectionTypeEnum((DisinfectionTypeEnum)val).ToString()})";
                        }
                        break;
                    case "CollectionSystemTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_CollectionSystemTypeEnum((CollectionSystemTypeEnum)val).ToString()})";
                        }
                        break;
                    case "AlarmSystemTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_AlarmSystemTypeEnum((AlarmSystemTypeEnum)val).ToString()})";
                        }
                        break;
                    case "ValveTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_ValveTypeEnum((ValveTypeEnum)val).ToString()})";
                        }
                        break;
                    case "ContactTitleEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_ContactTitleEnum((ContactTitleEnum)val).ToString()})";
                        }
                        break;
                    case "TelTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_TelTypeEnum((TelTypeEnum)val).ToString()})";
                        }
                        break;
                    case "EmailTypeEnum":
                        {
                            lblItemEnum2.Text = val == null ? "(empty)" : $"({_BaseEnumService.GetEnumText_EmailTypeEnum((EmailTypeEnum)val).ToString()})";
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
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_InfrastructureTypeEnum((InfrastructureTypeEnum)valNew).ToString();
                            }
                            break;
                        case "FacilityTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_FacilityTypeEnum((FacilityTypeEnum)valNew).ToString();
                            }
                            break;
                        case "AerationTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_AerationTypeEnum((AerationTypeEnum)valNew).ToString();
                            }
                            break;
                        case "PreliminaryTreatmentTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_PreliminaryTreatmentTypeEnum((PreliminaryTreatmentTypeEnum)valNew).ToString();
                            }
                            break;
                        case "PrimaryTreatmentTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_PrimaryTreatmentTypeEnum((PrimaryTreatmentTypeEnum)valNew).ToString();
                            }
                            break;
                        case "SecondaryTreatmentTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_SecondaryTreatmentTypeEnum((SecondaryTreatmentTypeEnum)valNew).ToString();
                            }
                            break;
                        case "TertiaryTreatmentTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_TertiaryTreatmentTypeEnum((TertiaryTreatmentTypeEnum)valNew).ToString();
                            }
                            break;
                        case "TreatmentTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_TreatmentTypeEnum((TreatmentTypeEnum)valNew).ToString();
                            }
                            break;
                        case "DisinfectionTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_DisinfectionTypeEnum((DisinfectionTypeEnum)valNew).ToString();
                            }
                            break;
                        case "CollectionSystemTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_CollectionSystemTypeEnum((CollectionSystemTypeEnum)valNew).ToString();
                            }
                            break;
                        case "AlarmSystemTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_AlarmSystemTypeEnum((AlarmSystemTypeEnum)valNew).ToString();
                            }
                            break;
                        case "ValveTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_ValveTypeEnum((ValveTypeEnum)valNew).ToString();
                            }
                            break;
                        case "ContactTitleEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_ContactTitleEnum((ContactTitleEnum)valNew).ToString();
                            }
                            break;
                        case "TelTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_TelTypeEnum((TelTypeEnum)valNew).ToString();
                            }
                            break;
                        case "EmailTypeEnum":
                            {
                                lblItemEnumNew.Text = valNew == null ? "empty" : _BaseEnumService.GetEnumText_EmailTypeEnum((EmailTypeEnum)valNew).ToString();
                            }
                            break;
                        default:
                            break;
                    }

                    PanelViewAndEdit.Controls.Add(lblItemEnumNew);
                }

            }
        }

        private void CreateChoiceButton(int x, int top, int? val, int? valNew, string elemName, int? elemEnumInt, string butName, string butText, string butFontFamilyName, string tag, EventHandler but_Clicked, int item)
        {
            Button but = new Button();
            but.Location = new Point(x, top);
            but.AutoSize = true;
            //but.Tag = item.ToString();

            if (elemEnumInt == null)
            {
                switch (elemName)
                {
                    case "InfrastructureTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.InfrastructureTypeNew = null;
                                CurrentInfrastructure.InfrastructureType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "FacilityTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.FacilityTypeNew = null;
                                CurrentInfrastructure.FacilityType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "AerationTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.AerationTypeNew = null;
                                CurrentInfrastructure.AerationType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "PreliminaryTreatmentTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.PreliminaryTreatmentTypeNew = null;
                                CurrentInfrastructure.PreliminaryTreatmentType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "PrimaryTreatmentTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.PrimaryTreatmentTypeNew = null;
                                CurrentInfrastructure.PrimaryTreatmentType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "SecondaryTreatmentTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.SecondaryTreatmentTypeNew = null;
                                CurrentInfrastructure.SecondaryTreatmentType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "TertiaryTreatmentTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.TertiaryTreatmentTypeNew = null;
                                CurrentInfrastructure.TertiaryTreatmentType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "DisinfectionTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.DisinfectionTypeNew = null;
                                CurrentInfrastructure.DisinfectionType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "CollectionSystemTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.CollectionSystemTypeNew = null;
                                CurrentInfrastructure.CollectionSystemType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "AlarmSystemTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.AlarmSystemTypeNew = null;
                                CurrentInfrastructure.AlarmSystemType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "CanOverflowTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.CanOverflowNew = null;
                                CurrentInfrastructure.CanOverflow = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "ValveTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentInfrastructure.ValveTypeNew = null;
                                CurrentInfrastructure.ValveType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "ContactTitleEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentContact.ContactTitleNew = null;
                                CurrentContact.ContactTitle = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "TelTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentContact.TelephoneList[item].TelTypeNew = null;
                                CurrentContact.TelephoneList[item].TelType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    case "EmailTypeEnum":
                        {
                            if (val == null && valNew == null)
                            {
                                CurrentContact.EmailList[item].EmailTypeNew = null;
                                CurrentContact.EmailList[item].EmailType = null;
                                but.BackColor = Color.Green;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (valNew != null)
                {
                    switch (elemName)
                    {
                        case "InfrastructureTypeEnum":
                            {
                                CurrentInfrastructure.InfrastructureTypeNew = valNew;
                                if (CurrentInfrastructure.InfrastructureTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "FacilityTypeEnum":
                            {
                                CurrentInfrastructure.FacilityTypeNew = valNew;
                                if (CurrentInfrastructure.FacilityTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "AerationTypeEnum":
                            {
                                CurrentInfrastructure.AerationTypeNew = valNew;
                                if (CurrentInfrastructure.AerationTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "PreliminaryTreatmentTypeEnum":
                            {
                                CurrentInfrastructure.PreliminaryTreatmentTypeNew = valNew;
                                if (CurrentInfrastructure.PreliminaryTreatmentTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "PrimaryTreatmentTypeEnum":
                            {
                                CurrentInfrastructure.PrimaryTreatmentTypeNew = valNew;
                                if (CurrentInfrastructure.PrimaryTreatmentTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "SecondaryTreatmentTypeEnum":
                            {
                                CurrentInfrastructure.SecondaryTreatmentTypeNew = valNew;
                                if (CurrentInfrastructure.SecondaryTreatmentTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "TertiaryTreatmentTypeEnum":
                            {
                                CurrentInfrastructure.TertiaryTreatmentTypeNew = valNew;
                                if (CurrentInfrastructure.TertiaryTreatmentTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "DisinfectionTypeEnum":
                            {
                                CurrentInfrastructure.DisinfectionTypeNew = valNew;
                                if (CurrentInfrastructure.DisinfectionTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "CollectionSystemTypeEnum":
                            {
                                CurrentInfrastructure.CollectionSystemTypeNew = valNew;
                                if (CurrentInfrastructure.CollectionSystemTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "AlarmSystemTypeEnum":
                            {
                                CurrentInfrastructure.AlarmSystemTypeNew = valNew;
                                if (CurrentInfrastructure.AlarmSystemTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "CanOverflowTypeEnum":
                            {
                                CurrentInfrastructure.CanOverflowNew = valNew;
                                if (CurrentInfrastructure.CanOverflowNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "ValveTypeEnum":
                            {
                                CurrentInfrastructure.ValveTypeNew = valNew;
                                if (CurrentInfrastructure.ValveTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "ContactTitleEnum":
                            {
                                CurrentContact.ContactTitleNew = valNew;
                                if (CurrentContact.ContactTitleNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "TelTypeEnum":
                            {
                                CurrentContact.TelephoneList[item].TelTypeNew = valNew;
                                if (CurrentContact.TelephoneList[item].TelTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        case "EmailTypeEnum":
                            {
                                CurrentContact.EmailList[item].EmailTypeNew = valNew;
                                if (CurrentContact.EmailList[item].EmailTypeNew == elemEnumInt)
                                {
                                    but.BackColor = Color.Green;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (val != null)
                    {
                        switch (elemName)
                        {
                            case "InfrastructureTypeEnum":
                                {
                                    CurrentInfrastructure.InfrastructureType = val;
                                    if (CurrentInfrastructure.InfrastructureType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "FacilityTypeEnum":
                                {
                                    CurrentInfrastructure.FacilityType = val;
                                    if (CurrentInfrastructure.FacilityType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "AerationTypeEnum":
                                {
                                    CurrentInfrastructure.AerationType = val;
                                    if (CurrentInfrastructure.AerationType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "PreliminaryTreatmentTypeEnum":
                                {
                                    CurrentInfrastructure.PreliminaryTreatmentType = val;
                                    if (CurrentInfrastructure.PreliminaryTreatmentType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "PrimaryTreatmentTypeEnum":
                                {
                                    CurrentInfrastructure.PrimaryTreatmentType = val;
                                    if (CurrentInfrastructure.PrimaryTreatmentType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "SecondaryTreatmentTypeEnum":
                                {
                                    CurrentInfrastructure.SecondaryTreatmentType = val;
                                    if (CurrentInfrastructure.SecondaryTreatmentType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "TertiaryTreatmentTypeEnum":
                                {
                                    CurrentInfrastructure.TertiaryTreatmentType = val;
                                    if (CurrentInfrastructure.TertiaryTreatmentType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "DisinfectionTypeEnum":
                                {
                                    CurrentInfrastructure.DisinfectionType = val;
                                    if (CurrentInfrastructure.DisinfectionType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "TCollectionSystemTypeEnum":
                                {
                                    CurrentInfrastructure.CollectionSystemType = val;
                                    if (CurrentInfrastructure.CollectionSystemType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "AlarmSystemTypeEnum":
                                {
                                    CurrentInfrastructure.AlarmSystemType = val;
                                    if (CurrentInfrastructure.AlarmSystemType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "CanOverflowTypeEnum":
                                {
                                    CurrentInfrastructure.CanOverflow = val;
                                    if (CurrentInfrastructure.CanOverflow == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "ValveTypeEnum":
                                {
                                    CurrentInfrastructure.ValveType = val;
                                    if (CurrentInfrastructure.ValveType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "ContactTitleEnum":
                                {
                                    CurrentContact.ContactTitle = val;
                                    if (CurrentContact.ContactTitle == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "TelTypeEnum":
                                {
                                    CurrentContact.TelephoneList[item].TelType = val;
                                    if (CurrentContact.TelephoneList[item].TelType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            case "EmailTypeEnum":
                                {
                                    CurrentContact.EmailList[item].EmailType = val;
                                    if (CurrentContact.EmailList[item].EmailType == elemEnumInt)
                                    {
                                        but.BackColor = Color.Green;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            but.Font = new Font(new FontFamily(butFontFamilyName).Name, 10f, FontStyle.Regular);
            but.Name = butName;
            but.Text = butText;
            but.Tag = tag + "|" + item.ToString();
            but.Click += but_Clicked;

            PanelViewAndEdit.Controls.Add(but);
        }

        private void butInfrastructureTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "None":
                    {
                        CurrentInfrastructure.InfrastructureTypeNew = null;
                        CurrentInfrastructure.InfrastructureType = null;
                    }
                    break;
                case "WWTP":
                    {
                        CurrentInfrastructure.InfrastructureTypeNew = ((int)InfrastructureTypeEnum.WWTP);
                    }
                    break;
                case "LiftStation":
                    {
                        CurrentInfrastructure.InfrastructureTypeNew = ((int)InfrastructureTypeEnum.LiftStation);
                    }
                    break;
                case "LineOverflow":
                    {
                        CurrentInfrastructure.InfrastructureTypeNew = ((int)InfrastructureTypeEnum.LineOverflow);
                    }
                    break;
                case "SeeOtherMunicipality":
                    {
                        CurrentInfrastructure.InfrastructureTypeNew = ((int)InfrastructureTypeEnum.SeeOtherMunicipality);
                    }
                    break;
                case "Other":
                    {
                        CurrentInfrastructure.InfrastructureTypeNew = ((int)InfrastructureTypeEnum.Other);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();
            }
        }

        private void butFacilityTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "None":
                    {
                        CurrentInfrastructure.FacilityTypeNew = null;
                        CurrentInfrastructure.FacilityType = null;
                    }
                    break;
                case "Lagoon":
                    {
                        CurrentInfrastructure.FacilityTypeNew = ((int)FacilityTypeEnum.Lagoon);
                    }
                    break;
                case "Plant":
                    {
                        CurrentInfrastructure.FacilityTypeNew = ((int)FacilityTypeEnum.Plant);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();
            }
        }

        private void butAerationTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "None":
                    {
                        CurrentInfrastructure.AerationTypeNew = null;
                        CurrentInfrastructure.AerationType = null;
                    }
                    break;
                case "MechanicalAirLines":
                    {
                        CurrentInfrastructure.AerationTypeNew = ((int)AerationTypeEnum.MechanicalAirLines);
                    }
                    break;
                case "MechanicalSurfaceMixers":
                    {
                        CurrentInfrastructure.AerationTypeNew = ((int)AerationTypeEnum.MechanicalSurfaceMixers);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butPreliminaryTreatmentTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "NotApplicable":
                    {
                        CurrentInfrastructure.PreliminaryTreatmentTypeNew = null;
                        CurrentInfrastructure.PreliminaryTreatmentType = null;
                    }
                    break;
                case "BarScreen":
                    {
                        CurrentInfrastructure.PreliminaryTreatmentTypeNew = ((int)PreliminaryTreatmentTypeEnum.BarScreen);
                    }
                    break;
                case "Grinder":
                    {
                        CurrentInfrastructure.PreliminaryTreatmentTypeNew = ((int)PreliminaryTreatmentTypeEnum.Grinder);
                    }
                    break;
                case "MechanicalScreening":
                    {
                        CurrentInfrastructure.PreliminaryTreatmentTypeNew = ((int)PreliminaryTreatmentTypeEnum.MechanicalScreening);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butPrimaryTreatmentTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "NotApplicable":
                    {
                        CurrentInfrastructure.PrimaryTreatmentTypeNew = null;
                        CurrentInfrastructure.PrimaryTreatmentType = null;
                    }
                    break;
                case "ChemicalCoagulation":
                    {
                        CurrentInfrastructure.PrimaryTreatmentTypeNew = ((int)PrimaryTreatmentTypeEnum.ChemicalCoagulation);
                    }
                    break;
                case "Filtration":
                    {
                        CurrentInfrastructure.PrimaryTreatmentTypeNew = ((int)PrimaryTreatmentTypeEnum.Filtration);
                    }
                    break;
                case "PrimaryClarification":
                    {
                        CurrentInfrastructure.PrimaryTreatmentTypeNew = ((int)PrimaryTreatmentTypeEnum.PrimaryClarification);
                    }
                    break;
                case "Sedimentation":
                    {
                        CurrentInfrastructure.PrimaryTreatmentTypeNew = ((int)PrimaryTreatmentTypeEnum.Sedimentation);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butSecondaryTreatmentTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "NotApplicable":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = null;
                        CurrentInfrastructure.SecondaryTreatmentType = null;
                    }
                    break;
                case "ActivatedSludge":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.ActivatedSludge);
                    }
                    break;
                case "AeratedSubmergedBioFilmReactor":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.AeratedSubmergedBioFilmReactor);
                    }
                    break;
                case "BiologicalAearatedFilters":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.BiologicalAearatedFilters);
                    }
                    break;
                case "ContactStabilization":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.ContactStabilization);
                    }
                    break;
                case "ExtendedActivatedSludge":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.ExtendedActivatedSludge);
                    }
                    break;
                case "IntegratedFixedFilmActivatedSludge":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.IntegratedFixedFilmActivatedSludge);
                    }
                    break;
                case "MovingBedBioReactor":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.MovingBedBioReactor);
                    }
                    break;
                case "OxidationDitch":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.OxidationDitch);
                    }
                    break;
                case "PhysicalChemicalProcesses":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.PhysicalChemicalProcesses);
                    }
                    break;
                case "RotatingBiologicalContactor":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.RotatingBiologicalContactor);
                    }
                    break;
                case "SequencingBatchReactor":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.SequencingBatchReactor);
                    }
                    break;
                case "TricklingFilters":
                    {
                        CurrentInfrastructure.SecondaryTreatmentTypeNew = ((int)SecondaryTreatmentTypeEnum.TricklingFilters);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butTertiaryTreatmentTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "NotApplicable":
                    {
                        CurrentInfrastructure.TertiaryTreatmentTypeNew = null;
                        CurrentInfrastructure.TertiaryTreatmentType = null;
                    }
                    break;
                case "Adsorption":
                    {
                        CurrentInfrastructure.TertiaryTreatmentTypeNew = ((int)TertiaryTreatmentTypeEnum.Adsorption);
                    }
                    break;
                case "BiologicalNutrientRemoval":
                    {
                        CurrentInfrastructure.TertiaryTreatmentTypeNew = ((int)TertiaryTreatmentTypeEnum.BiologicalNutrientRemoval);
                    }
                    break;
                case "Flocculation":
                    {
                        CurrentInfrastructure.TertiaryTreatmentTypeNew = ((int)TertiaryTreatmentTypeEnum.Flocculation);
                    }
                    break;
                case "IonExchange":
                    {
                        CurrentInfrastructure.TertiaryTreatmentTypeNew = ((int)TertiaryTreatmentTypeEnum.IonExchange);
                    }
                    break;
                case "MembraneFiltration":
                    {
                        CurrentInfrastructure.TertiaryTreatmentTypeNew = ((int)TertiaryTreatmentTypeEnum.MembraneFiltration);
                    }
                    break;
                case "ReverseOsmosis":
                    {
                        CurrentInfrastructure.TertiaryTreatmentTypeNew = ((int)TertiaryTreatmentTypeEnum.ReverseOsmosis);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butDisinfectionTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "None":
                    {
                        CurrentInfrastructure.DisinfectionTypeNew = null;
                        CurrentInfrastructure.DisinfectionType = null;
                    }
                    break;
                case "ChlorinationNoDechlorination":
                    {
                        CurrentInfrastructure.DisinfectionTypeNew = ((int)DisinfectionTypeEnum.ChlorinationNoDechlorination);
                    }
                    break;
                case "ChlorinationNoDechlorinationSeasonal":
                    {
                        CurrentInfrastructure.DisinfectionTypeNew = ((int)DisinfectionTypeEnum.ChlorinationNoDechlorinationSeasonal);
                    }
                    break;
                case "ChlorinationWithDechlorination":
                    {
                        CurrentInfrastructure.DisinfectionTypeNew = ((int)DisinfectionTypeEnum.ChlorinationWithDechlorination);
                    }
                    break;
                case "ChlorinationWithDechlorinationSeasonal":
                    {
                        CurrentInfrastructure.DisinfectionTypeNew = ((int)DisinfectionTypeEnum.ChlorinationWithDechlorinationSeasonal);
                    }
                    break;
                case "UV":
                    {
                        CurrentInfrastructure.DisinfectionTypeNew = ((int)DisinfectionTypeEnum.UV);
                    }
                    break;
                case "UVSeasonal":
                    {
                        CurrentInfrastructure.DisinfectionTypeNew = ((int)DisinfectionTypeEnum.UVSeasonal);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butCollectionSystemTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "None":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = null;
                        CurrentInfrastructure.CollectionSystemType = null;
                    }
                    break;
                case "Combined10Separated90":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.Combined10Separated90);
                    }
                    break;
                case "Combined20Separated80":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.Combined20Separated80);
                    }
                    break;
                case "Combined30Separated70":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.Combined30Separated70);
                    }
                    break;
                case "Combined40Separated60":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.Combined40Separated60);
                    }
                    break;
                case "Combined50Separated50":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.Combined50Separated50);
                    }
                    break;
                case "Combined60Separated40":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.Combined60Separated40);
                    }
                    break;
                case "Combined70Separated30":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.Combined70Separated30);
                    }
                    break;
                case "Combined80Separated20":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.Combined80Separated20);
                    }
                    break;
                case "Combined90Separated10":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.Combined90Separated10);
                    }
                    break;
                case "CompletelyCombined":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.CompletelyCombined);
                    }
                    break;
                case "CompletelySeparated":
                    {
                        CurrentInfrastructure.CollectionSystemTypeNew = ((int)CollectionSystemTypeEnum.CompletelySeparated);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butAlarmSystemTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "Unknown":
                    {
                        CurrentInfrastructure.AlarmSystemTypeNew = null;
                        CurrentInfrastructure.AlarmSystemType = null;
                    }
                    break;
                case "None":
                    {
                        CurrentInfrastructure.AlarmSystemTypeNew = ((int)AlarmSystemTypeEnum.None);
                    }
                    break;
                case "OnlyVisualLight":
                    {
                        CurrentInfrastructure.AlarmSystemTypeNew = ((int)AlarmSystemTypeEnum.OnlyVisualLight);
                    }
                    break;
                case "PagerAndLight":
                    {
                        CurrentInfrastructure.AlarmSystemTypeNew = ((int)AlarmSystemTypeEnum.PagerAndLight);
                    }
                    break;
                case "SCADA":
                    {
                        CurrentInfrastructure.AlarmSystemTypeNew = ((int)AlarmSystemTypeEnum.SCADA);
                    }
                    break;
                case "SCADAAndLight":
                    {
                        CurrentInfrastructure.AlarmSystemTypeNew = ((int)AlarmSystemTypeEnum.SCADAAndLight);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butCanOverflowTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "Unknown":
                    {
                        CurrentInfrastructure.CanOverflowNew = ((int)CanOverflowTypeEnum.Unknown);
                    }
                    break;
                case "Yes":
                    {
                        CurrentInfrastructure.CanOverflowNew = ((int)CanOverflowTypeEnum.Yes);
                    }
                    break;
                case "No":
                    {
                        CurrentInfrastructure.CanOverflowNew = ((int)CanOverflowTypeEnum.No);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butValveTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "Unknown":
                    {
                        CurrentInfrastructure.ValveTypeNew = null;
                        CurrentInfrastructure.ValveType = null;
                    }
                    break;
                case "Manually":
                    {
                        CurrentInfrastructure.ValveTypeNew = ((int)ValveTypeEnum.Manually);
                    }
                    break;
                case "Automatically":
                    {
                        CurrentInfrastructure.ValveTypeNew = ((int)ValveTypeEnum.Automatically);
                    }
                    break;
                case "None":
                    {
                        CurrentInfrastructure.ValveTypeNew = ((int)ValveTypeEnum.None);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butContactTitleSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "Unknown":
                    {
                        CurrentContact.ContactTitleNew = ((int)ContactTitleEnum.Error);
                    }
                    break;
                case "Director General":
                    {
                        CurrentContact.ContactTitleNew = ((int)ContactTitleEnum.DirectorGeneral);
                    }
                    break;
                case "Director Public Works":
                    {
                        CurrentContact.ContactTitleNew = ((int)ContactTitleEnum.DirectorPublicWorks);
                    }
                    break;
                case "Engineer":
                    {
                        CurrentContact.ContactTitleNew = ((int)ContactTitleEnum.Engineer);
                    }
                    break;
                case "Facilities Manager":
                    {
                        CurrentContact.ContactTitleNew = ((int)ContactTitleEnum.FacilitiesManager);
                    }
                    break;
                case "Foreman":
                    {
                        CurrentContact.ContactTitleNew = ((int)ContactTitleEnum.Foreman);
                    }
                    break;
                case "Operator":
                    {
                        CurrentContact.ContactTitleNew = ((int)ContactTitleEnum.Operator);
                    }
                    break;
                case "Superintendent":
                    {
                        CurrentContact.ContactTitleNew = ((int)ContactTitleEnum.Superintendent);
                    }
                    break;
                case "Supervisor":
                    {
                        CurrentContact.ContactTitleNew = ((int)ContactTitleEnum.Supervisor);
                    }
                    break;
                case "Technician":
                    {
                        CurrentContact.ContactTitleNew = ((int)ContactTitleEnum.Technician);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }


        private void butTelTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            int item = int.Parse(tagText.Substring(tagText.IndexOf("|") + 1));
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "Mobile":
                    {
                        CurrentContact.TelephoneList[item].TelTypeNew = ((int)TelTypeEnum.Mobile);
                    }
                    break;
                case "Mobile 2":
                    {
                        CurrentContact.TelephoneList[item].TelTypeNew = ((int)TelTypeEnum.Mobile2);
                    }
                    break;
                case "Personal":
                    {
                        CurrentContact.TelephoneList[item].TelTypeNew = ((int)TelTypeEnum.Personal);
                    }
                    break;
                case "Personal 2":
                    {
                        CurrentContact.TelephoneList[item].TelTypeNew = ((int)TelTypeEnum.Personal2);
                    }
                    break;
                case "Work":
                    {
                        CurrentContact.TelephoneList[item].TelTypeNew = ((int)TelTypeEnum.Work);
                    }
                    break;
                case "Work 2":
                    {
                        CurrentContact.TelephoneList[item].TelTypeNew = ((int)TelTypeEnum.Work2);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void butEmailTypeSelect_Clicked(object sender, EventArgs e)
        {
            string tagText = (string)((Button)sender).Tag;
            int item = int.Parse(tagText.Substring(tagText.IndexOf("|") + 1));
            tagText = tagText.Substring(0, tagText.IndexOf("|"));
            switch (tagText)
            {
                case "Personal":
                    {
                        CurrentContact.EmailList[item].EmailTypeNew = ((int)EmailTypeEnum.Personal);
                    }
                    break;
                case "Personal 2":
                    {
                        CurrentContact.EmailList[item].EmailTypeNew = ((int)EmailTypeEnum.Personal2);
                    }
                    break;
                case "Work":
                    {
                        CurrentContact.EmailList[item].EmailTypeNew = ((int)EmailTypeEnum.Work);
                    }
                    break;
                case "Work 2":
                    {
                        CurrentContact.EmailList[item].EmailTypeNew = ((int)EmailTypeEnum.Work2);
                    }
                    break;
                default:
                    break;
            }

            if (!IsReading)
            {
                int AutoScrollPos = PanelViewAndEdit.VerticalScroll.Value;

                SaveInfrastructureInfo();
                DrawPanelContactsAndInfrastructures();
                //RedrawSinglePanelInfrastructure();
                ReDrawContactAndInfrastructure();

                PanelViewAndEdit.VerticalScroll.Value = AutoScrollPos;
            }
        }

        private void DrawItemFloat(int x, int y, float? val, float? valNew, string lblTxt, int fix, string textBoxName)
        {
            Label lblItem = new Label();
            lblItem.AutoSize = true;
            lblItem.Location = new Point(x, y);
            if (textBoxName.Contains("CanGal_day") || textBoxName.Contains("USGal_day"))
            {
                lblItem.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblItem.ForeColor = Color.Black;
                lblItem.Text = $@"{lblTxt}: ";
            }
            else
            {
                lblItem.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblItem.ForeColor = Color.Blue;
                lblItem.Text = $@"{lblTxt}: ";
                lblItem.Click += ShowHelpDocument;
            }
            lblItem.Tag = lblTxt.Replace(" ", "_");

            PanelViewAndEdit.Controls.Add(lblItem);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            Label lblItem2 = new Label();
            lblItem2.AutoSize = true;
            lblItem2.Location = new Point(x, y);
            lblItem2.Font = new Font(new FontFamily(lblItem2.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem2.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            // lblItem2.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
            // lblItem2.Text = $@"{(valNew == null ? "" : $" ({(val == null ? "empty" : ((float)val).ToString("F" + fix))})")}";

            if (true)
            {
                bool TextCovered = false;

                // Design
                if (textBoxName == "textBoxDesignFlow_m3_day")
                {
                    lblItem2.Text = $@"{(valNew == null ? "" : $" ({(val == null ? "empty" : ((float)val).ToString("F" + fix))})")}";
                    TextCovered = true;
                }
                if (textBoxName == "textBoxDesignFlow_CanGal_day")
                {
                    TextCovered = true;
                }
                if (textBoxName == "textBoxDesignFlow_USGal_day")
                {
                    TextCovered = true;
                }

                // Average 
                if (textBoxName == "textBoxAverageFlow_m3_day")
                {
                    lblItem2.Text = $@"{(valNew == null ? "" : $" ({(val == null ? "empty" : ((float)val).ToString("F" + fix))})")}";
                    TextCovered = true;
                }
                if (textBoxName == "textBoxAverageFlow_CanGal_day")
                {
                    TextCovered = true;
                }
                if (textBoxName == "textBoxAverageFlow_USGal_day")
                {
                    TextCovered = true;
                }

                // Peak
                if (textBoxName == "textBoxPeakFlow_m3_day")
                {
                    lblItem2.Text = $@"{(valNew == null ? "" : $" ({(val == null ? "empty" : ((float)val).ToString("F" + fix))})")}";
                    TextCovered = true;
                }
                if (textBoxName == "textBoxPeakFlow_CanGal_day")
                {
                    TextCovered = true;
                }
                if (textBoxName == "textBoxPeakFlow_USGal_day")
                {
                    TextCovered = true;
                }

                if (!TextCovered)
                {
                    lblItem2.Text = $@"{(valNew == null ? "" : $" ({(val == null ? "empty" : ((float)val).ToString("F" + fix))})")}";
                }
            }

            PanelViewAndEdit.Controls.Add(lblItem2);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            if (IsEditing)
            {
                bool TextCovered = false;
                TextBox textItem = new TextBox();
                textItem.Location = new Point(x, y);
                textItem.Name = $"{textBoxName}";
                textItem.Font = new Font(new FontFamily(textItem.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textItem.Width = 100;

                // DesignFlow
                if (textBoxName == "textBoxDesignFlow_m3_day")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxDesignFlow_m3_day_TextChanged;
                }
                if (textBoxName == "textBoxDesignFlow_CanGal_day")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val * 219.969248f).ToString("F" + fix)) : ((float)valNew * 219.969248f).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxDesignFlow_CanGal_day_TextChanged;
                }
                if (textBoxName == "textBoxDesignFlow_USGal_day")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val * 264.172f).ToString("F" + fix)) : ((float)valNew * 264.172f).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxDesignFlow_USGal_day_TextChanged;
                }

                // AverageFlow
                if (textBoxName == "textBoxAverageFlow_m3_day")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxAverageFlow_m3_day_TextChanged;
                }
                if (textBoxName == "textBoxAverageFlow_CanGal_day")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val * 219.969248f).ToString("F" + fix)) : ((float)valNew * 219.969248f).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxAverageFlow_CanGal_day_TextChanged;
                }
                if (textBoxName == "textBoxAverageFlow_USGal_day")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val * 264.172f).ToString("F" + fix)) : ((float)valNew * 264.172f).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxAverageFlow_USGal_day_TextChanged;
                }

                // PeakFlow
                if (textBoxName == "textBoxPeakFlow_m3_day")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxPeakFlow_m3_day_TextChanged;
                }
                if (textBoxName == "textBoxPeakFlow_CanGal_day")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val * 219.969248f).ToString("F" + fix)) : ((float)valNew * 219.969248f).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxPeakFlow_CanGal_day_TextChanged;
                }
                if (textBoxName == "textBoxPeakFlow_USGal_day")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val * 264.172f).ToString("F" + fix)) : ((float)valNew * 264.172f).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxPeakFlow_USGal_day_TextChanged;
                }

                // Lat
                if (textBoxName == "textBoxLat")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxLat_TextChanged;
                }

                // Lng
                if (textBoxName == "textBoxLng")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxLng_TextChanged;
                }

                // LatOutfall
                if (textBoxName == "textBoxLatOutfall")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxLatOutfall_TextChanged;
                }

                // LngOutfall
                if (textBoxName == "textBoxLngOutfall")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxLngOutfall_TextChanged;
                }

                // PercFlowOfTotal
                if (textBoxName == "textBoxPercFlowOfTotal")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxPercFlowOfTotal_TextChanged;
                }

                // PortDiameter_m
                if (textBoxName == "textBoxPortDiameter_m")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxPortDiameter_m_TextChanged;
                }

                // PortSpacing_m
                if (textBoxName == "textBoxPortSpacing_m")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxPortSpacing_m_TextChanged;
                }

                // PortElevation_m
                if (textBoxName == "textBoxPortElevation_m")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxPortElevation_m_TextChanged;
                }

                // VerticalAngle_deg
                if (textBoxName == "textBoxVerticalAngle_deg")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxVerticalAngle_deg_TextChanged;
                }

                // HorizontalAngle_deg
                if (textBoxName == "textBoxHorizontalAngle_deg")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxHorizontalAngle_deg_TextChanged;
                }

                // DistanceFromShore_m
                if (textBoxName == "textBoxDistanceFromShore_m")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxDistanceFromShore_m_TextChanged;
                }

                // AverageDepth_m
                if (textBoxName == "textBoxAverageDepth_m")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxAverageDepth_m_TextChanged;
                }

                // DecayRate_per_day
                if (textBoxName == "textBoxDecayRate_per_day")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxDecayRate_per_day_TextChanged;
                }

                // NearFieldVelocity_m_s
                if (textBoxName == "textBoxNearFieldVelocity_m_s")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxNearFieldVelocity_m_s_TextChanged;
                }

                // FarFieldVelocity_m_s
                if (textBoxName == "textBoxFarFieldVelocity_m_s")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxFarFieldVelocity_m_s_TextChanged;
                }

                // ReceivingWaterSalinity_PSU
                if (textBoxName == "textBoxReceivingWaterSalinity_PSU")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxReceivingWaterSalinity_PSU_TextChanged;
                }

                // ReceivingWaterTemperature_C
                if (textBoxName == "textBoxReceivingWaterTemperature_C")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxReceivingWaterTemperature_C_TextChanged;
                }

                // ReceivingWaterTemperature_C
                if (textBoxName == "textBoxReceivingWaterTemperature_C")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                    textItem.TextChanged += textBoxReceivingWaterTemperature_C_TextChanged;
                }

                if (!TextCovered)
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                }

                PanelViewAndEdit.Controls.Add(textItem);
            }
            else
            {
                bool TextCovered = false;
                Label lblItemNew = new Label();
                lblItemNew.AutoSize = true;
                lblItemNew.Location = new Point(x, y);
                lblItemNew.Font = new Font(new FontFamily(lblItemNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblItemNew.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;

                // DesignFlow
                if (textBoxName == "textBoxDesignFlow_m3_day")
                {
                    lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                }
                if (textBoxName == "textBoxDesignFlow_CanGal_day")
                {
                    lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val * 219.969248f).ToString("F" + fix)) : ((float)valNew * 219.969248f).ToString("F" + fix));
                    TextCovered = true;
                }
                if (textBoxName == "textBoxDesignFlow_USGal_day")
                {
                    lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val * 264.172f).ToString("F" + fix)) : ((float)valNew * 264.172f).ToString("F" + fix));
                    TextCovered = true;
                }

                // AverageFlow
                if (textBoxName == "textBoxAverageFlow_m3_day")
                {
                    lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                }
                if (textBoxName == "textBoxAverageFlow_CanGal_day")
                {
                    lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val * 219.969248f).ToString("F" + fix)) : ((float)valNew * 219.969248f).ToString("F" + fix));
                    TextCovered = true;
                }
                if (textBoxName == "textBoxAverageFlow_USGal_day")
                {
                    lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val * 264.172f).ToString("F" + fix)) : ((float)valNew * 264.172f).ToString("F" + fix));
                    TextCovered = true;
                }

                // PeakFlow
                if (textBoxName == "textBoxPeakFlow_m3_day")
                {
                    lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                    TextCovered = true;
                }
                if (textBoxName == "textBoxPeakFlow_CanGal_day")
                {
                    lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val * 219.969248f).ToString("F" + fix)) : ((float)valNew * 219.969248f).ToString("F" + fix));
                    TextCovered = true;
                }
                if (textBoxName == "textBoxPeakFlow_USGal_day")
                {
                    lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val * 264.172f).ToString("F" + fix)) : ((float)valNew * 264.172f).ToString("F" + fix));
                    TextCovered = true;
                }

                if (!TextCovered)
                {
                    lblItemNew.Text = (valNew == null ? (val == null ? "---" : ((float)val).ToString("F" + fix)) : ((float)valNew).ToString("F" + fix));
                }
                PanelViewAndEdit.Controls.Add(lblItemNew);
            }

        }
        private void DrawItemInt(int x, int y, int? val, int? valNew, string lblTxt, string textBoxName)
        {
            Label lblItem = new Label();
            lblItem.AutoSize = true;
            lblItem.Location = new Point(x, y);
            lblItem.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem.ForeColor = Color.Blue;
            lblItem.Text = $@"{lblTxt}: ";
            lblItem.Click += ShowHelpDocument;
            lblItem.Tag = lblTxt.Replace(" ", "_");

            PanelViewAndEdit.Controls.Add(lblItem);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            Label lblItem2 = new Label();
            lblItem2.AutoSize = true;
            lblItem2.Location = new Point(x, y);
            lblItem2.Font = new Font(new FontFamily(lblItem2.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem2.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            lblItem2.Text = $@"{(valNew == null ? "" : $" ({(val == null ? "empty" : ((int)val).ToString())})")}";

            PanelViewAndEdit.Controls.Add(lblItem2);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            if (IsEditing)
            {
                bool TextCovered = false;
                TextBox textItem = new TextBox();
                textItem.Location = new Point(x, y);
                textItem.Name = $"{textBoxName}";
                textItem.Font = new Font(new FontFamily(textItem.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textItem.Width = 100;

                // NumberOfCells
                if (textBoxName == "textBoxNumberOfCells")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((int)val).ToString()) : ((int)valNew).ToString());
                    TextCovered = true;
                    textItem.TextChanged += textBoxNumberOfCells_TextChanged;
                }

                // NumberOfAeratedCells
                if (textBoxName == "textBoxNumberOfAeratedCells")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((int)val).ToString()) : ((int)valNew).ToString());
                    TextCovered = true;
                    textItem.TextChanged += textBoxNumberOfAeratedCells_TextChanged;
                }

                // PopServed
                if (textBoxName == "textBoxPopServed")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((int)val).ToString()) : ((int)valNew).ToString());
                    TextCovered = true;
                    textItem.TextChanged += textBoxPopServed_TextChanged;
                }

                // PumpsToTVItemID
                if (textBoxName == "textBoxPumpsToTVItemID")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((int)val).ToString()) : ((int)valNew).ToString());
                    TextCovered = true;
                    textItem.TextChanged += textBoxPumpsToTVItemID_TextChanged;
                }

                // NumberOfPorts
                if (textBoxName == "textBoxNumberOfPorts")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((int)val).ToString()) : ((int)valNew).ToString());
                    TextCovered = true;
                    textItem.TextChanged += textBoxNumberOfPorts_TextChanged;
                }

                // ReceivingWater_MPN_per_100ml
                if (textBoxName == "textBoxReceivingWater_MPN_per_100ml")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((int)val).ToString()) : ((int)valNew).ToString());
                    TextCovered = true;
                    textItem.TextChanged += textBoxReceivingWater_MPN_per_100ml_TextChanged;
                }

                // SeeOtherMunicipalityTVItemID
                if (textBoxName == "textBoxSeeOtherMunicipalityTVItemID")
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((int)val).ToString()) : ((int)valNew).ToString());
                    TextCovered = true;
                    textItem.TextChanged += textBoxSeeOtherMunicipalityTVItemID_TextChanged;
                }

                if (!TextCovered)
                {
                    textItem.Text = (valNew == null ? (val == null ? "" : ((int)val).ToString()) : ((int)valNew).ToString());
                }

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


        private void DrawSeeOtherMunicipality(int x, int y, string comboBoxName)
        {
            Label lblItemEnum = new Label();
            lblItemEnum.AutoSize = true;
            lblItemEnum.Location = new Point(x, y);
            lblItemEnum.Font = new Font(new FontFamily(lblItemEnum.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItemEnum.ForeColor = Color.Blue;
            lblItemEnum.Click += ShowHelpDocument;
            lblItemEnum.Tag = "SeeOtherMunicipality";
            lblItemEnum.Text = $@"See Other Municipalities: ";

            PanelViewAndEdit.Controls.Add(lblItemEnum);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

            if (IsEditing)
            {
                if (CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew != null)
                {
                    Label lblItemEnumOld = new Label();
                    lblItemEnumOld.AutoSize = true;
                    lblItemEnumOld.Location = new Point(lblItemEnum.Right + 4, lblItemEnum.Top);
                    lblItemEnumOld.Font = new Font(new FontFamily(lblItemEnumOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblItemEnumOld.ForeColor = CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew != null ? ForeColorChangedOrNew : ForeColorNormal;

                    string Municipality = "";
                    foreach (MunicipalityIDNumber municipalityIDNumber in municipalityDoc.MunicipalityIDNumberList)
                    {
                        if (CurrentInfrastructure.SeeOtherMunicipalityTVItemID.ToString() == municipalityIDNumber.IDNumber)
                        {
                            Municipality = municipalityIDNumber.Municipality;
                        }
                    }

                    lblItemEnumOld.Text = CurrentInfrastructure.SeeOtherMunicipalityTVItemID == null ? "(empty)" : $"({ Municipality })";

                    PanelViewAndEdit.Controls.Add(lblItemEnumOld);

                    x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;
                }

                ComboBox comboBoxItem = new ComboBox();
                comboBoxItem.Location = new Point(x, lblItemEnum.Top);
                comboBoxItem.Name = comboBoxName;
                comboBoxItem.Font = new Font(new FontFamily(lblItemEnum.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                comboBoxItem.Width = 400;
                comboBoxItem.DisplayMember = "Municipality";
                comboBoxItem.ValueMember = "IDNumber";
                comboBoxItem.SelectedValueChanged += SeeOtherMunicipalityChanged;

                PanelViewAndEdit.Controls.Add(comboBoxItem);

                MunicipalityIDNumber selectedMunicipality = new MunicipalityIDNumber();
                foreach (MunicipalityIDNumber municipalityIDNumber in municipalityDoc.MunicipalityIDNumberList)
                {
                    if (CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew != null)
                    {
                        if (municipalityIDNumber.IDNumber == CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew.ToString())
                        {
                            selectedMunicipality = municipalityIDNumber;
                        }
                    }
                    else
                    {
                        if (municipalityIDNumber.IDNumber == CurrentInfrastructure.SeeOtherMunicipalityTVItemID.ToString())
                        {
                            selectedMunicipality = municipalityIDNumber;
                        }
                    }
                    comboBoxItem.Items.Add(municipalityIDNumber);
                }

                if (selectedMunicipality != null)
                {
                    comboBoxItem.SelectedItem = selectedMunicipality;
                }
                else
                {
                    if (comboBoxItem.Items.Count > 0)
                    {
                        comboBoxItem.SelectedIndex = 0;
                    }
                }

                y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
            }
            else
            {
                Label lblItemEnum2 = new Label();
                lblItemEnum2.AutoSize = true;
                lblItemEnum2.Location = new Point(lblItemEnum.Right + 4, lblItemEnum.Top);
                lblItemEnum2.Font = new Font(new FontFamily(lblItemEnum2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblItemEnum2.ForeColor = CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew != null ? ForeColorChangedOrNew : ForeColorNormal;

                string Municipality = "";
                foreach (MunicipalityIDNumber municipalityIDNumber in municipalityDoc.MunicipalityIDNumberList)
                {
                    if (CurrentInfrastructure.SeeOtherMunicipalityTVItemID.ToString() == municipalityIDNumber.IDNumber)
                    {
                        Municipality = municipalityIDNumber.Municipality;
                    }
                }

                lblItemEnum2.Text = CurrentInfrastructure.SeeOtherMunicipalityTVItemID == null ? "(empty)" : $"({ Municipality })";

                PanelViewAndEdit.Controls.Add(lblItemEnum2);

                y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                if (CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew != null)
                {
                    Label lblItemEnumNew = new Label();
                    lblItemEnumNew.AutoSize = true;
                    lblItemEnumNew.Location = new Point(lblItemEnum2.Right + 4, lblItemEnum2.Top);
                    lblItemEnumNew.Font = new Font(new FontFamily(lblItemEnumNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblItemEnumNew.ForeColor = CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew != null ? ForeColorChangedOrNew : ForeColorNormal;

                    string MunicipalityNew = "";
                    foreach (MunicipalityIDNumber municipalityIDNumber in municipalityDoc.MunicipalityIDNumberList)
                    {
                        if (CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew.ToString() == municipalityIDNumber.IDNumber)
                        {
                            MunicipalityNew = municipalityIDNumber.Municipality;
                        }
                    }

                    lblItemEnumNew.Text = MunicipalityNew;

                    PanelViewAndEdit.Controls.Add(lblItemEnumNew);
                }

            }
        }
        private void DrawItemText(int x, int y, string val, string valNew, string lblTxt, string textBoxName, int width, int item)
        {
            Label lblItem = new Label();
            lblItem.AutoSize = true;
            lblItem.Location = new Point(x, y);
            lblItem.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem.ForeColor = Color.Blue;
            lblItem.Text = $@"{lblTxt}: ";
            lblItem.Click += ShowHelpDocument;
            lblItem.Tag = lblTxt.Replace(" ", "_");

            PanelViewAndEdit.Controls.Add(lblItem);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            Label lblItem2 = new Label();
            lblItem2.AutoSize = true;
            lblItem2.Location = new Point(x, y);
            lblItem2.Font = new Font(new FontFamily(lblItem2.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem2.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            lblItem2.Text = (string.IsNullOrWhiteSpace(valNew) ? "" : $" ({(string.IsNullOrWhiteSpace(val) ? "empty" : val)})");

            PanelViewAndEdit.Controls.Add(lblItem2);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            if (IsEditing)
            {
                TextBox textItem = new TextBox();
                textItem.Width = width;
                textItem.Location = new Point(x, y);
                textItem.Name = $"{textBoxName}";
                textItem.Font = new Font(new FontFamily(textItem.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                textItem.Text = (string.IsNullOrWhiteSpace(valNew) ? (string.IsNullOrWhiteSpace(val) ? "" : val) : valNew);
                textItem.Tag = item;

                if (textBoxName == "textBoxTVText")
                {
                    textItem.TextChanged += textBoxTVText_TextChanged;
                }
                if (textBoxName == "textBoxFirstName")
                {
                    textItem.TextChanged += textBoxFirstName_TextChanged;
                }
                if (textBoxName == "textBoxInitial")
                {
                    textItem.TextChanged += textBoxInitial_TextChanged;
                }
                if (textBoxName == "textBoxLastName")
                {
                    textItem.TextChanged += textBoxLastName_TextChanged;
                }
                if (textBoxName == "textBoxEmail")
                {
                    textItem.TextChanged += textBoxEmail_TextChanged;
                }
                if (textBoxName == "textBoxTelNumber")
                {
                    textItem.TextChanged += textBoxTelNumber_TextChanged;
                }
                if (textBoxName == "textBoxEmailAddress")
                {
                    textItem.TextChanged += textBoxEmailAddress_TextChanged;
                }
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
            lblItem.ForeColor = Color.Blue;
            lblItem.Text = $@"{lblTxt}: ";
            lblItem.Click += ShowHelpDocument;
            lblItem.Tag = lblTxt.Replace(" ", "_");

            PanelViewAndEdit.Controls.Add(lblItem);

            x = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

            Label lblItem2 = new Label();
            lblItem2.AutoSize = true;
            lblItem2.Location = new Point(x, y);
            lblItem2.Font = new Font(new FontFamily(lblItem.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblItem2.ForeColor = valNew != null ? ForeColorChangedOrNew : ForeColorNormal;
            lblItem2.Text = (string.IsNullOrWhiteSpace(valNew) ? "" : $" ({(string.IsNullOrWhiteSpace(val) ? "empty" : val)})");

            PanelViewAndEdit.Controls.Add(lblItem2);

            y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

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

                if (textBoxName.StartsWith("textBoxExtraComment_"))
                {
                    textItem.TextChanged += textItemExtraComment_TextChanged;
                }
                if (textBoxName == "textBoxCommentEN")
                {
                    textItem.TextChanged += textBoxCommentEN_TextChanged;
                }
                if (textBoxName == "textBoxCommentFR")
                {
                    textItem.TextChanged += textBoxCommentFR_TextChanged;
                }
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
        public void DrawPanelContactsAndInfrastructures()
        {
            PanelPolSourceSite.Controls.Clear();

            if (municipalityDoc.Municipality == null)
            {
                Label lblTVText = new Label();

                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.TabIndex = 0;
                lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblTVText.Text = $"Selected municipality has no contact or infrastructure";

                PanelPolSourceSite.Controls.Add(lblTVText);
            }
            else
            {


                List<Contact> ContactList = municipalityDoc.Municipality.ContactList;
                ShowPanelContact();

                List<Infrastructure> InfNextList = municipalityDoc.Municipality.InfrastructureList.Where(c => c.PumpsToTVItemID == null && c.PumpsToTVItemIDNew == null).ToList();
                int Level = 0;
                ShowRecursivePanelInfrastructure(InfNextList, Level);

                InfNextList = municipalityDoc.Municipality.InfrastructureList.Where(c => c.Shown == false).ToList();
                ShowRecursivePanelInfrastructure(InfNextList, Level);
            }
        }
        public void ShowPanelContact()
        {
            int Y = 0;
            foreach (Contact contact in municipalityDoc.Municipality.ContactList)
            {
                if (PanelPolSourceSite.Controls.Count > 0)
                {
                    Y = PanelPolSourceSite.Controls[PanelPolSourceSite.Controls.Count - 1].Bottom;
                }
                else
                {
                    Y = 0;
                }
                Panel panelContact = new Panel();

                panelContact.BorderStyle = BorderStyle.FixedSingle;
                panelContact.Location = new Point(0, Y);
                panelContact.Size = new Size(PanelPolSourceSite.Width, 44);
                panelContact.TabIndex = 0;
                panelContact.Tag = contact.ContactTVItemID;
                panelContact.Click += ShowContact_Click;

                Label lblTVText = new Label();

                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.TabIndex = 0;
                lblTVText.Tag = contact.ContactTVItemID;

                bool IsActive = false;
                if (contact.IsActiveNew != null)
                {
                    IsActive = (bool)contact.IsActiveNew;
                }
                else
                {
                    IsActive = (bool)contact.IsActive;
                }

                if (IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }
                string FirstName = string.IsNullOrWhiteSpace(contact.FirstNameNew) == false ? contact.FirstNameNew : contact.FirstName;
                string Initial = string.IsNullOrWhiteSpace(contact.InitialNew) == false ? contact.InitialNew : contact.Initial;
                //if (!string.IsNullOrWhiteSpace(Initial))
                //{
                //    Initial = Initial + ", ";
                //}
                string LastName = string.IsNullOrWhiteSpace(contact.LastNameNew) == false ? contact.LastNameNew : contact.LastName;
                string FullName = $"{FirstName} {Initial} {LastName}";
                lblTVText.Text = $"{contact.ContactTVItemID}    {FullName} (Contact)";
                lblTVText.Click += ShowContact_Click;

                panelContact.Controls.Add(lblTVText);

                Label lblContactStatus = new Label();

                bool NeedsUpdate = false;

                if (IsAdmin)
                {
                    if (contact.IsActiveNew != null && contact.IsActiveNew != contact.IsActive)
                    {
                        NeedsUpdate = true;
                    }

                    if (!NeedsUpdate)
                    {
                        if (contact.FirstNameNew != null
                            || contact.InitialNew != null
                            || contact.LastNameNew != null
                            || contact.EmailNew != null)
                        {
                            NeedsUpdate = true;
                        }
                    }

                    if (!NeedsUpdate)
                    {
                        if (contact.IsActiveNew != null
                            || contact.ContactAddressNew.AddressTVItemID != null
                            || contact.ContactAddressNew.AddressType != null
                            || contact.ContactAddressNew.Municipality != null
                            || contact.ContactAddressNew.PostalCode != null
                            || contact.ContactAddressNew.StreetName != null
                            || contact.ContactAddressNew.StreetNumber != null
                            || contact.ContactAddressNew.StreetType != null)
                        {
                            NeedsUpdate = true;
                        }
                    }

                    if (!NeedsUpdate)
                    {
                        foreach (Telephone tel in contact.TelephoneList)
                        {
                            if (tel.TelTypeNew != null
                                || tel.TelNumberNew != null)
                            {
                                NeedsUpdate = true;
                                break;
                            }
                        }
                    }

                    if (!NeedsUpdate)
                    {
                        foreach (Email email in contact.EmailList)
                        {
                            if (email.EmailTypeNew != null
                                || email.EmailAddressNew != null)
                            {
                                NeedsUpdate = true;
                                break;
                            }
                        }
                    }

                    string NeedsUpdateText = NeedsUpdate ? "Needs Update" : "";

                    lblContactStatus.AutoSize = true;
                    lblContactStatus.Location = new Point(40, lblTVText.Bottom + 4);
                    lblContactStatus.TabIndex = 0;
                    lblContactStatus.Tag = contact.ContactTVItemID;

                    bool IsActive2 = false;
                    if (contact.IsActiveNew != null)
                    {
                        IsActive2 = (bool)contact.IsActiveNew;
                    }
                    else
                    {
                        IsActive2 = (bool)contact.IsActive;
                    }

                    if (IsActive2 == false)
                    {
                        lblContactStatus.Font = new Font(new FontFamily(lblContactStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblContactStatus.Font = new Font(new FontFamily(lblContactStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    }
                    if (NeedsUpdate)
                    {
                        lblContactStatus.Text = $"{NeedsUpdateText}";
                    }
                    else
                    {
                        lblContactStatus.Text = $"";
                    }
                    lblContactStatus.Click += ShowContact_Click;


                    panelContact.Controls.Add(lblContactStatus);

                }
                else
                {

                    lblContactStatus.AutoSize = true;
                    lblContactStatus.Location = new Point(40, lblTVText.Bottom + 4);
                    lblContactStatus.TabIndex = 0;
                    lblContactStatus.Tag = contact.ContactTVItemID;

                    bool IsActive2 = false;
                    if (contact.IsActiveNew != null)
                    {
                        IsActive2 = (bool)contact.IsActiveNew;
                    }
                    else
                    {
                        IsActive2 = (bool)contact.IsActive;
                    }

                    if (IsActive2 == false)
                    {
                        lblContactStatus.Font = new Font(new FontFamily(lblContactStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblContactStatus.Font = new Font(new FontFamily(lblContactStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    }
                    lblContactStatus.Text = $"";
                    lblContactStatus.Click += ShowContact_Click;


                    panelContact.Controls.Add(lblContactStatus);
                }

                PanelPolSourceSite.Controls.Add(panelContact);

                contact.Shown = true;
            }

            if (municipalityDoc.Municipality.ContactList.Count > 0)
            {
                Y = PanelPolSourceSite.Controls[PanelPolSourceSite.Controls.Count - 1].Bottom;

                Panel panelContact = new Panel();

                panelContact.BorderStyle = BorderStyle.FixedSingle;
                panelContact.Location = new Point(0, Y);
                panelContact.Size = new Size(PanelPolSourceSite.Width, 10);
                panelContact.BackColor = Color.Blue;

                PanelPolSourceSite.Controls.Add(panelContact);

            }
        }
        public void ShowRecursivePanelInfrastructure(List<Infrastructure> InfNextList, int Level)
        {
            int Y = 0;
            foreach (Infrastructure infrastructure in InfNextList)
            {
                if (PanelPolSourceSite.Controls.Count > 0)
                {
                    Y = PanelPolSourceSite.Controls[PanelPolSourceSite.Controls.Count - 1].Bottom;
                }
                else
                {
                    Y = 0;
                }
                Panel panelInfrastructure = new Panel();

                panelInfrastructure.BorderStyle = BorderStyle.FixedSingle;
                panelInfrastructure.Location = new Point(Level * 10, Y);
                panelInfrastructure.Size = new Size(PanelPolSourceSite.Width - (Level * 10), 44);
                panelInfrastructure.TabIndex = 0;
                panelInfrastructure.Tag = infrastructure.InfrastructureTVItemID;
                panelInfrastructure.Click += ShowInfrastructure_Click;

                Label lblTVText = new Label();

                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.TabIndex = 0;
                lblTVText.Tag = infrastructure.InfrastructureTVItemID;

                bool IsActive = false;
                if (infrastructure.IsActiveNew != null)
                {
                    IsActive = (bool)infrastructure.IsActiveNew;
                }
                else
                {
                    IsActive = (bool)infrastructure.IsActive;
                }

                if (IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }
                if (!string.IsNullOrWhiteSpace(infrastructure.TVTextNew))
                {
                    lblTVText.Text = $"{infrastructure.InfrastructureTVItemID}    {infrastructure.TVTextNew}";
                }
                else
                {
                    lblTVText.Text = $"{infrastructure.InfrastructureTVItemID}    {infrastructure.TVText}";
                }
                lblTVText.Click += ShowInfrastructure_Click;

                panelInfrastructure.Controls.Add(lblTVText);


                Label lbInfrastructureStatus = new Label();

                bool NeedDetailsUpdate = false;
                bool NeedPicturesUpdate = false;
                bool NeedActiveUpdate = false;

                if (IsAdmin)
                {
                    if (infrastructure.IsActiveNew != null && infrastructure.IsActiveNew != infrastructure.IsActive)
                    {
                        NeedActiveUpdate = true;
                    }

                    if (infrastructure.LatNew != null
                        || infrastructure.LngNew != null
                        || infrastructure.LatOutfallNew != null
                        || infrastructure.LngOutfallNew != null
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

                    if (!NeedDetailsUpdate)
                    {
                        if (infrastructure.AerationTypeNew != null
                            || infrastructure.AlarmSystemTypeNew != null
                            || infrastructure.AverageDepth_mNew != null
                            || infrastructure.AverageFlow_m3_dayNew != null
                            || infrastructure.CanOverflowNew != null
                            || infrastructure.CollectionSystemTypeNew != null
                            || infrastructure.CommentENNew != null
                            || infrastructure.CommentFRNew != null
                            || infrastructure.DecayRate_per_dayNew != null
                            || infrastructure.DesignFlow_m3_dayNew != null
                            || infrastructure.DisinfectionTypeNew != null
                            || infrastructure.DistanceFromShore_mNew != null
                            || infrastructure.FacilityTypeNew != null
                            || infrastructure.FarFieldVelocity_m_sNew != null
                            || infrastructure.HorizontalAngle_degNew != null
                            || infrastructure.InfrastructureTypeNew != null
                            || infrastructure.IsActiveNew != null
                            || infrastructure.IsMechanicallyAeratedNew != null
                            || infrastructure.NearFieldVelocity_m_sNew != null
                            || infrastructure.NumberOfAeratedCellsNew != null
                            || infrastructure.NumberOfCellsNew != null
                            || infrastructure.NumberOfPortsNew != null
                            || infrastructure.PeakFlow_m3_dayNew != null
                            || infrastructure.PercFlowOfTotalNew != null
                            || infrastructure.PopServedNew != null
                            || infrastructure.PortDiameter_mNew != null
                            || infrastructure.PortElevation_mNew != null
                            || infrastructure.PortSpacing_mNew != null
                            || infrastructure.PreliminaryTreatmentTypeNew != null
                            || infrastructure.PrimaryTreatmentTypeNew != null
                            || infrastructure.PumpsToTVItemIDNew != null
                            || infrastructure.ReceivingWaterSalinity_PSUNew != null
                            || infrastructure.ReceivingWaterTemperature_CNew != null
                            || infrastructure.ReceivingWater_MPN_per_100mlNew != null
                            || infrastructure.SecondaryTreatmentTypeNew != null
                            || infrastructure.SeeOtherMunicipalityTVItemIDNew != null
                            || infrastructure.TertiaryTreatmentTypeNew != null
                            || infrastructure.TVTextNew != null
                            || infrastructure.VerticalAngle_degNew != null)
                        {
                            NeedDetailsUpdate = true;
                        }
                    }
                    foreach (Picture picture in infrastructure.InfrastructurePictureList)
                    {
                        if (picture.DescriptionNew != null
                            || picture.ExtensionNew != null
                            || picture.FileNameNew != null
                            || picture.ToRemove != null
                            || picture.FromWaterNew != null
                            || picture.PictureTVItemID >= 10000000)
                        {
                            NeedPicturesUpdate = true;
                            break;
                        }
                    }

                    string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                    string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                    string NeedAcitveUpdateText = NeedActiveUpdate ? "Active" : "";

                    lbInfrastructureStatus.AutoSize = true;
                    lbInfrastructureStatus.Location = new Point(40, lblTVText.Bottom + 4);
                    lbInfrastructureStatus.TabIndex = 0;
                    lbInfrastructureStatus.Tag = infrastructure.InfrastructureTVItemID;

                    bool IsActive2 = false;
                    if (infrastructure.IsActiveNew != null)
                    {
                        IsActive2 = (bool)infrastructure.IsActiveNew;
                    }
                    else
                    {
                        IsActive2 = (bool)infrastructure.IsActive;
                    }

                    if (IsActive2 == false)
                    {
                        lbInfrastructureStatus.Font = new Font(new FontFamily(lbInfrastructureStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lbInfrastructureStatus.Font = new Font(new FontFamily(lbInfrastructureStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    }
                    if (NeedDetailsUpdate || NeedPicturesUpdate || NeedActiveUpdate)
                    {
                        lbInfrastructureStatus.Text = $"Needs update for      {NeedDetailsUpdateText}     {NeedPictuesUpdateText} {NeedAcitveUpdateText}";
                    }
                    else
                    {
                        lbInfrastructureStatus.Text = $"";
                    }
                    lbInfrastructureStatus.Click += ShowInfrastructure_Click;


                    panelInfrastructure.Controls.Add(lbInfrastructureStatus);

                }
                else
                {

                    lbInfrastructureStatus.AutoSize = true;
                    lbInfrastructureStatus.Location = new Point(40, lblTVText.Bottom + 4);
                    lbInfrastructureStatus.TabIndex = 0;
                    lbInfrastructureStatus.Tag = infrastructure.InfrastructureTVItemID;

                    bool IsActive2 = false;
                    if (infrastructure.IsActiveNew != null)
                    {
                        IsActive2 = (bool)infrastructure.IsActiveNew;
                    }
                    else
                    {
                        IsActive2 = (bool)infrastructure.IsActive;
                    }

                    if (IsActive2 == false)
                    {
                        lbInfrastructureStatus.Font = new Font(new FontFamily(lbInfrastructureStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lbInfrastructureStatus.Font = new Font(new FontFamily(lbInfrastructureStatus.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    }
                    lbInfrastructureStatus.Text = $"";
                    lbInfrastructureStatus.Click += ShowInfrastructure_Click;


                    panelInfrastructure.Controls.Add(lbInfrastructureStatus);
                }


                PanelPolSourceSite.Controls.Add(panelInfrastructure);

                infrastructure.Shown = true;

                List<Infrastructure> InfNextListChild = new List<Infrastructure>();

                foreach (Infrastructure inf in municipalityDoc.Municipality.InfrastructureList)
                {
                    if (inf.PumpsToTVItemIDNew != null)
                    {
                        if (inf.PumpsToTVItemIDNew == infrastructure.InfrastructureTVItemID)
                        {
                            InfNextListChild.Add(inf);
                        }
                    }
                    else
                    {
                        if (inf.PumpsToTVItemID == infrastructure.InfrastructureTVItemID)
                        {
                            InfNextListChild.Add(inf);
                        }
                    }
                }
                if (InfNextListChild.Count > 0)
                {
                    ShowRecursivePanelInfrastructure(InfNextListChild, Level + 1);
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
                foreach (PSS pss in subsectorDoc.Subsector.PSSList.OrderByDescending(c => c.SiteNumber))
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
                    lblTVText.Location = new Point(5, 4);
                    lblTVText.TabIndex = 0;
                    lblTVText.Tag = pss.PSSTVItemID;

                    bool IsActive = false;
                    if (pss.IsActiveNew != null)
                    {
                        IsActive = (bool)pss.IsActiveNew;
                    }
                    else
                    {
                        IsActive = (bool)pss.IsActive;
                    }

                    if (IsActive == false)
                    {
                        lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    }
                    if (!string.IsNullOrWhiteSpace(pss.TVTextNew))
                    {
                        lblTVText.Text = $"{pss.TVTextNew}";
                    }
                    else
                    {
                        lblTVText.Text = $"{pss.TVText}";
                    }
                    lblTVText.Click += ShowPolSourceSite_Click;

                    panelpss.Controls.Add(lblTVText);


                    Label lblPSSStatus = new Label();

                    bool NeedDetailsUpdate = false;
                    bool NeedIssuesUpdate = false;
                    bool NeedPicturesUpdate = false;
                    bool NeedActiveUpdate = false;
                    bool NeedPointSourceUpdate = false;
                    if (IsAdmin)
                    {
                        if (pss.IsActiveNew != null && pss.IsActiveNew != pss.IsActive)
                        {
                            NeedActiveUpdate = false;
                        }

                        if (pss.IsPointSourceNew != null && pss.IsPointSourceNew != pss.IsPointSource)
                        {
                            NeedPointSourceUpdate = false;
                        }

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
                                || picture.ToRemove != null
                                || picture.FromWaterNew != null
                                || picture.PictureTVItemID >= 10000000)
                            {
                                NeedPicturesUpdate = true;
                                break;
                            }
                        }

                        lblPSSStatus.AutoSize = true;
                        lblPSSStatus.Location = new Point(5, lblTVText.Bottom + 4);
                        lblPSSStatus.TabIndex = 0;
                        lblPSSStatus.Tag = pss.PSSTVItemID;

                        bool IsActive2 = false;
                        if (pss.IsActiveNew != null)
                        {
                            IsActive2 = (bool)pss.IsActiveNew;
                        }
                        else
                        {
                            IsActive2 = (bool)pss.IsActive;
                        }

                        if (IsActive2 == false)
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
                        string NeedActiveUpdateText = NeedActiveUpdate ? "Active" : "";
                        string NeedPointSourceUpdateText = NeedPointSourceUpdate ? "Point Source" : "";
                        if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate || NeedActiveUpdate || NeedPointSourceUpdate)
                        {
                            lblPSSStatus.Text = $"Good --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText} {NeedActiveUpdateText} {NeedPointSourceUpdateText}";
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
                        lblPSSStatus.Location = new Point(5, lblTVText.Bottom + 4);
                        lblPSSStatus.TabIndex = 0;
                        lblPSSStatus.Tag = pss.PSSTVItemID;

                        bool IsActive2 = false;
                        if (pss.IsActiveNew != null)
                        {
                            IsActive2 = (bool)pss.IsActiveNew;
                        }
                        else
                        {
                            IsActive2 = (bool)pss.IsActive;
                        }

                        if (IsActive2 == false)
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
                                string NeedActiveUpdateText = NeedActiveUpdate ? "Active" : "";
                                string NeedPointSourceUpdateText = NeedPointSourceUpdate ? "Point Source" : "";
                                if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate || NeedActiveUpdate || NeedPointSourceUpdate)
                                {
                                    lblPSSStatus.Text = $"Not Well Formed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText} {NeedActiveUpdateText} {NeedPointSourceUpdateText}";
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
                                string NeedActiveUpdateText = NeedActiveUpdate ? "Active" : "";
                                string NeedPointSourceUpdateText = NeedPointSourceUpdate ? "Point Source" : "";
                                if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate || NeedActiveUpdate || NeedPointSourceUpdate)
                                {
                                    lblPSSStatus.Text = $"Not Completed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText} {NeedActiveUpdateText} {NeedPointSourceUpdateText}";
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
        public void EmailAdd()
        {
            if (municipalityDoc.Municipality.MunicipalityName != null)
            {
                Email email = new Email();
                int MaxEmailTVItemID = 40000000;
                if (CurrentContact.EmailList.Count > 0)
                {
                    int Max = CurrentContact.EmailList.Max(c => c.EmailTVItemID).Value;
                    if (Max >= MaxEmailTVItemID)
                    {
                        MaxEmailTVItemID = Max + 1;
                    }
                }
                email.EmailTVItemID = MaxEmailTVItemID;
                email.EmailType = (int)EmailTypeEnum.Work;
                email.EmailAddress = "john.doe@gmail.com";

                CurrentContact.EmailList.Add(email);
            }
        }
        public void TelAdd()
        {
            if (municipalityDoc.Municipality.MunicipalityName != null)
            {
                Telephone tel = new Telephone();
                int MaxTelTVItemID = 30000000;
                if (CurrentContact.TelephoneList.Count > 0)
                {
                    int Max = CurrentContact.TelephoneList.Max(c => c.TelTVItemID).Value;
                    if (Max >= MaxTelTVItemID)
                    {
                        MaxTelTVItemID = Max + 1;
                    }
                }
                tel.TelTVItemID = MaxTelTVItemID;
                tel.TelType = (int)TelTypeEnum.Mobile;
                tel.TelNumber = "1(506)555-1212";

                CurrentContact.TelephoneList.Add(tel);
            }
        }
        public void ContactAdd()
        {
            if (municipalityDoc.Municipality.MunicipalityName != null)
            {
                Contact contact = new Contact();
                int MaxContactTVItemID = 20000000;
                if (municipalityDoc.Municipality.ContactList.Count > 0)
                {
                    int Max = municipalityDoc.Municipality.ContactList.Max(c => c.ContactTVItemID).Value;
                    if (Max >= MaxContactTVItemID)
                    {
                        MaxContactTVItemID = Max + 1;
                    }
                }

                string FirstName = "Jane";
                string Initial = "A";
                string LastName = "Doe";
                string Email = "Jane.A.Doe@gmail.com";
                contact.ContactTVItemID = MaxContactTVItemID;
                contact.IsActive = true;
                contact.IsActiveNew = true;
                contact.FirstName = FirstName;
                contact.Initial = Initial;
                contact.LastName = LastName;
                contact.Email = Email;
                contact.ContactTitle = (int)ContactTitleEnum.Error;

                Contact contactFound = (from c in municipalityDoc.Municipality.ContactList
                                        where c.FirstName == FirstName
                                        && c.Initial == Initial
                                        && c.LastName == LastName
                                        select c).FirstOrDefault();

                if (contactFound != null)
                {
                    int count = 2;
                    while (contactFound != null)
                    {
                        FirstName = "Jane" + count;
                        Initial = "A";
                        LastName = "Doe";
                        Email = "Jane" + count + ".A.Doe@gmail.com";
                        contact.FirstName = FirstName;
                        contact.Initial = Initial;
                        contact.LastName = LastName;
                        contact.Email = Email;

                        contactFound = (from c in municipalityDoc.Municipality.ContactList
                                        where c.FirstName == FirstName
                                        && c.Initial == Initial
                                        && c.LastName == LastName
                                        select c).FirstOrDefault();

                        count++;
                    }
                }
                municipalityDoc.Municipality.ContactList.Add(contact);
            }
        }
        public void InfrastructureAdd()
        {
            if (municipalityDoc.Municipality.MunicipalityName != null)
            {
                Infrastructure infrastructure = new Infrastructure();
                int MaxInfrastructureTVItemID = 10000000;
                if (municipalityDoc.Municipality.InfrastructureList.Count > 0)
                {
                    int Max = municipalityDoc.Municipality.InfrastructureList.Max(c => c.InfrastructureTVItemID).Value;
                    if (Max >= MaxInfrastructureTVItemID)
                    {
                        MaxInfrastructureTVItemID = Max + 1;
                    }
                }
                infrastructure.InfrastructureTVItemID = MaxInfrastructureTVItemID;
                infrastructure.Lat = null;
                infrastructure.Lng = null;
                infrastructure.IsActive = true;
                infrastructure.IsActiveNew = true;
                infrastructure.InfrastructureType = (int)InfrastructureTypeEnum.WWTP;
                int Count = 0;
                string NewInfrastructureName = "";
                while (true)
                {
                    Count += 1;
                    NewInfrastructureName = "New Infrastructure " + Count;
                    bool Exist = false;
                    foreach (Infrastructure inf in municipalityDoc.Municipality.InfrastructureList)
                    {
                        if (inf.TVTextNew != null)
                        {
                            if (inf.TVTextNew == NewInfrastructureName)
                            {
                                Exist = true;
                                break;
                            }
                        }
                        else
                        {
                            if (inf.TVText == NewInfrastructureName)
                            {
                                Exist = true;
                                break;
                            }
                        }
                    }

                    if (!Exist)
                    {
                        break;
                    }
                }
                infrastructure.TVText = NewInfrastructureName;
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
                int SiteNumber = 1;
                if (subsectorDoc.Subsector.PSSList.Count > 0)
                {
                    int Max = subsectorDoc.Subsector.PSSList.Max(c => c.PSSTVItemID).Value;
                    if (Max >= MaxPSSTVItemID)
                    {
                        MaxPSSTVItemID = Max + 1;
                    }
                    LastLat = ((float)subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1].Lat);
                    LastLng = ((float)subsectorDoc.Subsector.PSSList[subsectorDoc.Subsector.PSSList.Count - 1].Lng);

                    SiteNumber = subsectorDoc.Subsector.PSSList.Max(c => c.SiteNumber).Value + 1;
                }
                pss.PSSTVItemID = MaxPSSTVItemID;
                pss.SiteNumber = SiteNumber;
                pss.SiteNumberText = "00000".Substring(0, "00000".Length - pss.SiteNumber.ToString().Length) + pss.SiteNumber.ToString();
                pss.Lat = LastLat + 0.1f;
                pss.Lng = LastLng + 0.1f;
                pss.IsActive = true;
                pss.IsPointSource = false;
                pss.TVText = $"P{ pss.SiteNumberText } - New PSS";

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
                issue.PolSourceObsInfoIntList = new List<int>() { (int)PolSourceObsInfoEnum.SourceHumanLand };
                issue.PolSourceObsInfoIntListNew = new List<int>() { (int)PolSourceObsInfoEnum.SourceHumanLand };

                pss.PSSObs.IssueList.Add(issue);
            }
        }
        public void InfrastructureSaveToCSSPWebTools()
        {
            string ret = "";
            bool NeedToSave = false;

            string InfrastructureName = CurrentInfrastructure.TVText;
            if (CurrentInfrastructure.TVTextNew != null)
            {
                InfrastructureName = CurrentInfrastructure.TVTextNew;
            }

            EmitRTBMessage(new RTBMessageEventArgs($"Checking if Infrastructure [{InfrastructureName}] already exist in CSSPWebTools\r\n"));

            ret = InfrastructureExistInCSSPWebTools((int)CurrentInfrastructure.InfrastructureTVItemID, AdminEmail);
            ret = ret.Replace("\"", "");

            if (ret.StartsWith("ERROR:"))
            {
                EmitRTBMessage(new RTBMessageEventArgs($"Infrastructure [{InfrastructureName}] does not exist in CSSPWebTools\r\n"));
            }
            else
            {
                EmitRTBMessage(new RTBMessageEventArgs($"Infrastructure [{InfrastructureName}] already exist in CSSPWebTools\r\n"));
            }

            #region Load Variables
            bool IsActive = true;
            float? Lat = null;
            float? Lng = null;
            float? LatOutfall = null;
            float? LngOutfall = null;
            string CommentEN = null;
            string CommentFR = null;
            InfrastructureTypeEnum? InfrastructureType = null;
            FacilityTypeEnum? FacilityType = null;
            bool? IsMechanicallyAerated = null;
            int? NumberOfCells = null;
            int? NumberOfAeratedCells = null;
            AerationTypeEnum? AerationType = null;
            PreliminaryTreatmentTypeEnum? PreliminaryTreatmentType = null;
            PrimaryTreatmentTypeEnum? PrimaryTreatmentType = null;
            SecondaryTreatmentTypeEnum? SecondaryTreatmentType = null;
            TertiaryTreatmentTypeEnum? TertiaryTreatmentType = null;
            DisinfectionTypeEnum? DisinfectionType = null;
            CollectionSystemTypeEnum? CollectionSystemType = null;
            AlarmSystemTypeEnum? AlarmSystemType = null;
            float? DesignFlow_m3_day = null;
            float? AverageFlow_m3_day = null;
            float? PeakFlow_m3_day = null;
            int? PopServed = null;
            CanOverflowTypeEnum? CanOverflow = null;
            ValveTypeEnum? ValveType = null;
            bool? HasBackupPower = null;
            float? PercFlowOfTotal = null;
            float? AverageDepth_m = null;
            int? NumberOfPorts = null;
            float? PortDiameter_m = null;
            float? PortSpacing_m = null;
            float? PortElevation_m = null;
            float? VerticalAngle_deg = null;
            float? HorizontalAngle_deg = null;
            float? DecayRate_per_day = null;
            float? NearFieldVelocity_m_s = null;
            float? FarFieldVelocity_m_s = null;
            float? ReceivingWaterSalinity_PSU = null;
            float? ReceivingWaterTemperature_C = null;
            int? ReceivingWater_MPN_per_100ml = null;
            float? DistanceFromShore_m = null;
            int? SeeOtherMunicipalityTVItemID = null;
            string SeeOtherMunicipalityText = null;
            int? PumpsToTVItemID = null;

            // IsActive
            if (CurrentInfrastructure.IsActiveNew == null)
            {
                if (CurrentInfrastructure.IsActive != null)
                {
                    IsActive = (bool)CurrentInfrastructure.IsActive;
                }
            }
            else
            {
                IsActive = (bool)CurrentInfrastructure.IsActiveNew;
            }

            // Lat, Lng
            if (CurrentInfrastructure.LatNew == null)
            {
                if (CurrentInfrastructure.Lat != null)
                {
                    Lat = (float)CurrentInfrastructure.Lat;
                }
            }
            else
            {
                Lat = (float)CurrentInfrastructure.LatNew;
            }

            if (CurrentInfrastructure.LngNew == null)
            {
                if (CurrentInfrastructure.Lng != null)
                {
                    Lng = (float)CurrentInfrastructure.Lng;
                }
            }
            else
            {
                Lng = (float)CurrentInfrastructure.LngNew;
            }

            // LatOutfall, LngOutfall
            if (CurrentInfrastructure.LatOutfallNew == null)
            {
                if (CurrentInfrastructure.LatOutfall != null)
                {
                    LatOutfall = (float)CurrentInfrastructure.LatOutfall;
                }
            }
            else
            {
                LatOutfall = (float)CurrentInfrastructure.LatOutfallNew;
            }
            if (CurrentInfrastructure.LngOutfallNew == null)
            {
                if (CurrentInfrastructure.LngOutfall != null)
                {
                    LngOutfall = (float)CurrentInfrastructure.LngOutfall;
                }
            }
            else
            {
                LngOutfall = (float)CurrentInfrastructure.LngOutfallNew;
            }

            // CommentEN
            if (CurrentInfrastructure.CommentENNew == null)
            {
                if (CurrentInfrastructure.CommentEN != null)
                {
                    CommentEN = CurrentInfrastructure.CommentEN;
                }
            }
            else
            {
                CommentEN = CurrentInfrastructure.CommentENNew;
            }

            // CommentFR
            if (CurrentInfrastructure.CommentFRNew == null)
            {
                if (CurrentInfrastructure.CommentFR != null)
                {
                    CommentFR = CurrentInfrastructure.CommentFR;
                }
            }
            else
            {
                CommentFR = CurrentInfrastructure.CommentFRNew;
            }

            // InfrastructureType
            if (CurrentInfrastructure.InfrastructureTypeNew == null)
            {
                if (CurrentInfrastructure.InfrastructureType != null)
                {
                    InfrastructureType = (InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureType;
                }
            }
            else
            {
                InfrastructureType = (InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureTypeNew;
            }

            // FacilityType
            if (CurrentInfrastructure.FacilityTypeNew == null)
            {
                if (CurrentInfrastructure.FacilityType != null)
                {
                    FacilityType = (FacilityTypeEnum)CurrentInfrastructure.FacilityType;
                }
            }
            else
            {
                FacilityType = (FacilityTypeEnum)CurrentInfrastructure.FacilityTypeNew;
            }

            // IsMechanicallyAerated
            if (CurrentInfrastructure.IsMechanicallyAeratedNew == null)
            {
                if (CurrentInfrastructure.IsMechanicallyAerated != null)
                {
                    IsMechanicallyAerated = (bool)CurrentInfrastructure.IsMechanicallyAerated;
                }
            }
            else
            {
                IsMechanicallyAerated = (bool)CurrentInfrastructure.IsMechanicallyAeratedNew;
            }

            // NumberOfCells
            if (CurrentInfrastructure.NumberOfCellsNew == null)
            {
                if (CurrentInfrastructure.NumberOfCells != null)
                {
                    NumberOfCells = (int)CurrentInfrastructure.NumberOfCells;
                }
            }
            else
            {
                NumberOfCells = (int)CurrentInfrastructure.NumberOfCellsNew;
            }

            // NumberOfAeratedCells
            if (CurrentInfrastructure.NumberOfAeratedCellsNew == null)
            {
                if (CurrentInfrastructure.NumberOfAeratedCells != null)
                {
                    NumberOfAeratedCells = (int)CurrentInfrastructure.NumberOfAeratedCells;
                }
            }
            else
            {
                NumberOfAeratedCells = (int)CurrentInfrastructure.NumberOfAeratedCellsNew;
            }

            // AerationType
            if (CurrentInfrastructure.AerationTypeNew == null)
            {
                if (CurrentInfrastructure.AerationType != null)
                {
                    AerationType = (AerationTypeEnum)CurrentInfrastructure.AerationType;
                }
            }
            else
            {
                AerationType = (AerationTypeEnum)CurrentInfrastructure.AerationTypeNew;
            }

            // PreliminaryTreatmentType
            if (CurrentInfrastructure.PreliminaryTreatmentTypeNew == null)
            {
                if (CurrentInfrastructure.PreliminaryTreatmentType != null)
                {
                    PreliminaryTreatmentType = (PreliminaryTreatmentTypeEnum)CurrentInfrastructure.PreliminaryTreatmentType;
                }
            }
            else
            {
                PreliminaryTreatmentType = (PreliminaryTreatmentTypeEnum)CurrentInfrastructure.PreliminaryTreatmentTypeNew;
            }

            // PrimaryTreatmentType
            if (CurrentInfrastructure.PrimaryTreatmentTypeNew == null)
            {
                if (CurrentInfrastructure.PrimaryTreatmentType != null)
                {
                    PrimaryTreatmentType = (PrimaryTreatmentTypeEnum)CurrentInfrastructure.PrimaryTreatmentType;
                }
            }
            else
            {
                PrimaryTreatmentType = (PrimaryTreatmentTypeEnum)CurrentInfrastructure.PrimaryTreatmentTypeNew;
            }

            // SecondaryTreatmentType
            if (CurrentInfrastructure.SecondaryTreatmentTypeNew == null)
            {
                if (CurrentInfrastructure.SecondaryTreatmentType != null)
                {
                    SecondaryTreatmentType = (SecondaryTreatmentTypeEnum)CurrentInfrastructure.SecondaryTreatmentType;
                }
            }
            else
            {
                SecondaryTreatmentType = (SecondaryTreatmentTypeEnum)CurrentInfrastructure.SecondaryTreatmentTypeNew;
            }

            // TertiaryTreatemntType
            if (CurrentInfrastructure.TertiaryTreatmentTypeNew == null)
            {
                if (CurrentInfrastructure.TertiaryTreatmentType != null)
                {
                    TertiaryTreatmentType = (TertiaryTreatmentTypeEnum)CurrentInfrastructure.TertiaryTreatmentType;
                }
            }
            else
            {
                TertiaryTreatmentType = (TertiaryTreatmentTypeEnum)CurrentInfrastructure.TertiaryTreatmentTypeNew;
            }

            // DisinfectionType
            if (CurrentInfrastructure.DisinfectionTypeNew == null)
            {
                if (CurrentInfrastructure.DisinfectionType != null)
                {
                    DisinfectionType = (DisinfectionTypeEnum)CurrentInfrastructure.DisinfectionType;
                }
            }
            else
            {
                DisinfectionType = (DisinfectionTypeEnum)CurrentInfrastructure.DisinfectionTypeNew;
            }

            // CollectionSystemType
            if (CurrentInfrastructure.CollectionSystemTypeNew == null)
            {
                if (CurrentInfrastructure.CollectionSystemType != null)
                {
                    CollectionSystemType = (CollectionSystemTypeEnum)CurrentInfrastructure.CollectionSystemType;
                }
            }
            else
            {
                CollectionSystemType = (CollectionSystemTypeEnum)CurrentInfrastructure.CollectionSystemTypeNew;
            }

            // AlarmSystemType
            if (CurrentInfrastructure.AlarmSystemTypeNew == null)
            {
                if (CurrentInfrastructure.AlarmSystemType != null)
                {
                    AlarmSystemType = (AlarmSystemTypeEnum)CurrentInfrastructure.AlarmSystemType;
                }
            }
            else
            {
                AlarmSystemType = (AlarmSystemTypeEnum)CurrentInfrastructure.AlarmSystemTypeNew;
            }

            // DesignFlow_m3_day
            if (CurrentInfrastructure.DesignFlow_m3_dayNew == null)
            {
                if (CurrentInfrastructure.DesignFlow_m3_day != null)
                {
                    DesignFlow_m3_day = (float)CurrentInfrastructure.DesignFlow_m3_day;
                }
            }
            else
            {
                DesignFlow_m3_day = (float)CurrentInfrastructure.DesignFlow_m3_dayNew;
            }

            // AverageFlow_m3_day
            if (CurrentInfrastructure.AverageFlow_m3_dayNew == null)
            {
                if (CurrentInfrastructure.AverageFlow_m3_day != null)
                {
                    AverageFlow_m3_day = (float)CurrentInfrastructure.AverageFlow_m3_day;
                }
            }
            else
            {
                AverageFlow_m3_day = (float)CurrentInfrastructure.AverageFlow_m3_dayNew;
            }

            // PeakFlow_m3_day
            if (CurrentInfrastructure.PeakFlow_m3_dayNew == null)
            {
                if (CurrentInfrastructure.PeakFlow_m3_day != null)
                {
                    PeakFlow_m3_day = (float)CurrentInfrastructure.PeakFlow_m3_day;
                }
            }
            else
            {
                PeakFlow_m3_day = (float)CurrentInfrastructure.PeakFlow_m3_dayNew;
            }

            // PopServed
            if (CurrentInfrastructure.PopServedNew == null)
            {
                if (CurrentInfrastructure.PopServed != null)
                {
                    PopServed = (int)CurrentInfrastructure.PopServed;
                }
            }
            else
            {
                PopServed = (int)CurrentInfrastructure.PopServedNew;
            }

            // CanOverflowType
            if (CurrentInfrastructure.CanOverflowNew == null)
            {
                if (CurrentInfrastructure.CanOverflow != null)
                {
                    CanOverflow = (CanOverflowTypeEnum)CurrentInfrastructure.CanOverflow;
                }
            }
            else
            {
                CanOverflow = (CanOverflowTypeEnum)CurrentInfrastructure.CanOverflowNew;
            }

            // ValveType
            if (CurrentInfrastructure.ValveTypeNew == null)
            {
                if (CurrentInfrastructure.ValveType != null)
                {
                    ValveType = (ValveTypeEnum)CurrentInfrastructure.ValveType;
                }
            }
            else
            {
                ValveType = (ValveTypeEnum)CurrentInfrastructure.ValveTypeNew;
            }

            // HasBackupPower
            if (CurrentInfrastructure.HasBackupPowerNew == null)
            {
                if (CurrentInfrastructure.HasBackupPower != null)
                {
                    HasBackupPower = (bool)CurrentInfrastructure.HasBackupPower;
                }
            }
            else
            {
                HasBackupPower = (bool)CurrentInfrastructure.HasBackupPowerNew;
            }

            // PercFlowOfTotal
            if (CurrentInfrastructure.PercFlowOfTotalNew == null)
            {
                if (CurrentInfrastructure.PercFlowOfTotal != null)
                {
                    PercFlowOfTotal = (float)CurrentInfrastructure.PercFlowOfTotal;
                }
            }
            else
            {
                PercFlowOfTotal = (float)CurrentInfrastructure.PercFlowOfTotalNew;
            }

            // AverageDepth_m
            if (CurrentInfrastructure.AverageDepth_mNew == null)
            {
                if (CurrentInfrastructure.AverageDepth_m != null)
                {
                    AverageDepth_m = (float)CurrentInfrastructure.AverageDepth_m;
                }
            }
            else
            {
                AverageDepth_m = (float)CurrentInfrastructure.AverageDepth_mNew;
            }

            // NumberOfPorts
            if (CurrentInfrastructure.NumberOfPortsNew == null)
            {
                if (CurrentInfrastructure.NumberOfPorts != null)
                {
                    NumberOfPorts = (int)CurrentInfrastructure.NumberOfPorts;
                }
            }
            else
            {
                NumberOfPorts = (int)CurrentInfrastructure.NumberOfPortsNew;
            }

            // PortDiameter_m
            if (CurrentInfrastructure.PortDiameter_mNew == null)
            {
                if (CurrentInfrastructure.PortDiameter_m != null)
                {
                    PortDiameter_m = (float)CurrentInfrastructure.PortDiameter_m;
                }
            }
            else
            {
                PortDiameter_m = (float)CurrentInfrastructure.PortDiameter_mNew;
            }

            // PortSpacing_m
            if (CurrentInfrastructure.PortSpacing_mNew == null)
            {
                if (CurrentInfrastructure.PortSpacing_m != null)
                {
                    PortSpacing_m = (float)CurrentInfrastructure.PortSpacing_m;
                }
            }
            else
            {
                PortSpacing_m = (float)CurrentInfrastructure.PortSpacing_mNew;
            }

            // PortElevation_m
            if (CurrentInfrastructure.PortElevation_mNew == null)
            {
                if (CurrentInfrastructure.PortElevation_m != null)
                {
                    PortElevation_m = (float)CurrentInfrastructure.PortElevation_m;
                }
            }
            else
            {
                PortElevation_m = (float)CurrentInfrastructure.PortElevation_mNew;
            }

            // VerticalAngle_deg
            if (CurrentInfrastructure.VerticalAngle_degNew == null)
            {
                if (CurrentInfrastructure.VerticalAngle_deg != null)
                {
                    VerticalAngle_deg = (float)CurrentInfrastructure.VerticalAngle_deg;
                }
            }
            else
            {
                VerticalAngle_deg = (float)CurrentInfrastructure.VerticalAngle_degNew;
            }

            // HorizontalAngle_deg
            if (CurrentInfrastructure.HorizontalAngle_degNew == null)
            {
                if (CurrentInfrastructure.HorizontalAngle_deg != null)
                {
                    HorizontalAngle_deg = (float)CurrentInfrastructure.HorizontalAngle_deg;
                }
            }
            else
            {
                HorizontalAngle_deg = (float)CurrentInfrastructure.HorizontalAngle_degNew;
            }

            // DecayRate_per_day
            if (CurrentInfrastructure.DecayRate_per_dayNew == null)
            {
                if (CurrentInfrastructure.DecayRate_per_day != null)
                {
                    DecayRate_per_day = (float)CurrentInfrastructure.DecayRate_per_day;
                }
            }
            else
            {
                DecayRate_per_day = (float)CurrentInfrastructure.DecayRate_per_dayNew;
            }

            // NearFieldVelocity_m_s
            if (CurrentInfrastructure.NearFieldVelocity_m_sNew == null)
            {
                if (CurrentInfrastructure.NearFieldVelocity_m_s != null)
                {
                    NearFieldVelocity_m_s = (float)CurrentInfrastructure.NearFieldVelocity_m_s;
                }
            }
            else
            {
                NearFieldVelocity_m_s = (float)CurrentInfrastructure.NearFieldVelocity_m_sNew;
            }

            // FarFieldVelocity_m_s
            if (CurrentInfrastructure.FarFieldVelocity_m_sNew == null)
            {
                if (CurrentInfrastructure.FarFieldVelocity_m_s != null)
                {
                    FarFieldVelocity_m_s = (float)CurrentInfrastructure.FarFieldVelocity_m_s;
                }
            }
            else
            {
                FarFieldVelocity_m_s = (float)CurrentInfrastructure.FarFieldVelocity_m_sNew;
            }

            // ReceivingWaterSalinity_PSU
            if (CurrentInfrastructure.ReceivingWaterSalinity_PSUNew == null)
            {
                if (CurrentInfrastructure.ReceivingWaterSalinity_PSU != null)
                {
                    ReceivingWaterSalinity_PSU = (float)CurrentInfrastructure.ReceivingWaterSalinity_PSU;
                }
            }
            else
            {
                ReceivingWaterSalinity_PSU = (float)CurrentInfrastructure.ReceivingWaterSalinity_PSUNew;
            }

            // ReceivingWaterTemperature_C
            if (CurrentInfrastructure.ReceivingWaterTemperature_CNew == null)
            {
                if (CurrentInfrastructure.ReceivingWaterTemperature_C != null)
                {
                    ReceivingWaterTemperature_C = (float)CurrentInfrastructure.ReceivingWaterTemperature_C;
                }
            }
            else
            {
                ReceivingWaterTemperature_C = (float)CurrentInfrastructure.ReceivingWaterTemperature_CNew;
            }

            // ReceivingWater_MPN_per_100ml
            if (CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew == null)
            {
                if (CurrentInfrastructure.ReceivingWater_MPN_per_100ml != null)
                {
                    ReceivingWater_MPN_per_100ml = (int)CurrentInfrastructure.ReceivingWater_MPN_per_100ml;
                }
            }
            else
            {
                ReceivingWater_MPN_per_100ml = (int)CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew;
            }

            // DistanceFromShore_m
            if (CurrentInfrastructure.DistanceFromShore_mNew == null)
            {
                if (CurrentInfrastructure.DistanceFromShore_m != null)
                {
                    DistanceFromShore_m = (float)CurrentInfrastructure.DistanceFromShore_m;
                }
            }
            else
            {
                DistanceFromShore_m = (float)CurrentInfrastructure.DistanceFromShore_mNew;
            }

            // SeeOtherMunicipalityTVItemID
            if (CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew == null)
            {
                if (CurrentInfrastructure.SeeOtherMunicipalityTVItemID != null)
                {
                    SeeOtherMunicipalityTVItemID = (int)CurrentInfrastructure.SeeOtherMunicipalityTVItemID;
                }
            }
            else
            {
                SeeOtherMunicipalityTVItemID = (int)CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew;
            }

            // SeeOtherMunicipalityText
            if (CurrentInfrastructure.SeeOtherMunicipalityTextNew == null)
            {
                if (CurrentInfrastructure.SeeOtherMunicipalityText != null)
                {
                    SeeOtherMunicipalityText = CurrentInfrastructure.SeeOtherMunicipalityText;
                }
            }
            else
            {
                SeeOtherMunicipalityText = CurrentInfrastructure.SeeOtherMunicipalityTextNew;
            }

            // PumpsToTVItemID
            if (CurrentInfrastructure.PumpsToTVItemIDNew == null)
            {
                if (CurrentInfrastructure.PumpsToTVItemID != null)
                {
                    PumpsToTVItemID = (int)CurrentInfrastructure.PumpsToTVItemID;
                }
            }
            else
            {
                PumpsToTVItemID = (int)CurrentInfrastructure.PumpsToTVItemIDNew;
            }

            string MessageText = $"Trying to save all information for Infrastructure [{InfrastructureName}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tMunicipality TVItemID\t[{(int)municipalityDoc.Municipality.MunicipalityTVItemID}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tInfrastructure TVItemID\t[{(int)CurrentInfrastructure.InfrastructureTVItemID}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tInfrastructure Name\t[{InfrastructureName}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tIsActive[{IsActive}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tLat[{Lat}]\tLong[{Lng}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tLat Outfall[{LatOutfall}]\tLong Outfall[{LngOutfall}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tComment (EN)\t[{CommentEN}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tComment (FR)\t[{CommentFR}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tInfrastructure Type\t[{InfrastructureType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tFacility Type\t[{FacilityType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tIs Mechanically Aerated\t[{IsMechanicallyAerated.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tNumber of Cells\t[{NumberOfCells.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tNumber of Aerated Cells\t[{NumberOfAeratedCells.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tAerationType\t[{AerationType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tAeration Type\t[{AerationType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tPreliminary Treatment Type\t[{PreliminaryTreatmentType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tPrimary Treatment Type\t[{PrimaryTreatmentType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tSecondary Treatment Type\t[{SecondaryTreatmentType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tTertiary Treatment Type\t[{TertiaryTreatmentType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tDisinfection Type\t[{DisinfectionType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tCollection System Type\t[{CollectionSystemType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tAlarm System Type\t[{AlarmSystemType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tDesignFlow (m3/day)\t[{DesignFlow_m3_day.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tAverageFlow (m3/day)\t[{AverageFlow_m3_day.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tPeakFlow (m3/day)\t[{PeakFlow_m3_day.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tPopulation Served\t[{PopServed.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tCan Overflow\t[{CanOverflow.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tValve Type\t[{ValveType.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tHas Backup Power\t[{HasBackupPower.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tPercentage of total flow (%)\t[{PercFlowOfTotal.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tAverage Depth (m)\t[{AverageDepth_m.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tNumber Of Ports\t[{NumberOfPorts.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tPort Diameter (m)\t[{PortDiameter_m.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tPort Spacing (m)\t[{PortSpacing_m.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tPort Elevation (m)\t[{PortElevation_m.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tVertical Angle (deg)\t[{VerticalAngle_deg.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tHorizontal Angle (deg)\t[{HorizontalAngle_deg.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tDecay Rate (/day)\t[{DecayRate_per_day.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tNear Field Velocity (m/s)\t[{NearFieldVelocity_m_s.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tFar Field Velocity (m/s)\t[{FarFieldVelocity_m_s.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tReceiving Water Salinity (PSU)\t[{ReceivingWaterSalinity_PSU.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tReceiving Water Temperature (C)\t[{ReceivingWaterTemperature_C.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tReceiving Water (MPN/100mL)\t[{ReceivingWater_MPN_per_100ml.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tDistance From Shore (m)\t[{DistanceFromShore_m.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tSee Other Municipality TVItemID\t[{SeeOtherMunicipalityTVItemID.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tSee Other Municipality Text\t[{SeeOtherMunicipalityText.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tPumps To TVItemID\t[{PumpsToTVItemID.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            #endregion Load Variables

            ret = SaveToCSSPWebToolsCreateOrModifyInfrastructure((int)municipalityDoc.Municipality.MunicipalityTVItemID,
                (int)CurrentInfrastructure.InfrastructureTVItemID, InfrastructureName, IsActive,
                Lat, Lng, LatOutfall, LngOutfall, CommentEN, CommentFR, InfrastructureType, FacilityType,
                IsMechanicallyAerated, NumberOfCells, NumberOfAeratedCells, AerationType, PreliminaryTreatmentType, PrimaryTreatmentType,
                SecondaryTreatmentType, TertiaryTreatmentType, DisinfectionType, CollectionSystemType, AlarmSystemType,
                DesignFlow_m3_day, AverageFlow_m3_day, PeakFlow_m3_day, PopServed, CanOverflow, ValveType, HasBackupPower, PercFlowOfTotal,
                AverageDepth_m, NumberOfPorts, PortDiameter_m, PortSpacing_m, PortElevation_m, VerticalAngle_deg,
                HorizontalAngle_deg, DecayRate_per_day, NearFieldVelocity_m_s, FarFieldVelocity_m_s,
                ReceivingWaterSalinity_PSU, ReceivingWaterTemperature_C, ReceivingWater_MPN_per_100ml, DistanceFromShore_m,
                SeeOtherMunicipalityTVItemID, SeeOtherMunicipalityText, PumpsToTVItemID, AdminEmail);
            ret = ret.Replace("\"", "");
            if (ret.StartsWith("ERROR:"))
            {
                EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                if (NeedToSave)
                {
                    SaveMunicipalityTextFile();
                }
            }
            else
            {
                EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                #region reset variables
                int NewTVItemID = int.Parse(ret);
                if (CurrentInfrastructure.InfrastructureTVItemID != NewTVItemID)
                {
                    // need to change the pump to from InfrastructureTVItemID to NewTVItemID
                    // do the same for the pictures

                    foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList)
                    {
                        if (infrastructure.PumpsToTVItemIDNew != null)
                        {
                            if (infrastructure.PumpsToTVItemIDNew == CurrentInfrastructure.InfrastructureTVItemID)
                            {
                                infrastructure.PumpsToTVItemIDNew = NewTVItemID;
                            }
                        }
                        else
                        {
                            if (infrastructure.PumpsToTVItemID == CurrentInfrastructure.InfrastructureTVItemID)
                            {
                                infrastructure.PumpsToTVItemID = NewTVItemID;
                            }
                        }
                    }

                    foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList)
                    {
                        foreach (Picture picture in infrastructure.InfrastructurePictureList)
                        {
                            FileInfo fiPicture = new FileInfo($@"C:\PollutionSourceSites\Infrastructures\{CurrentMunicipalityName}\Pictures\{CurrentInfrastructure.InfrastructureTVItemID}_{picture.PictureTVItemID}{picture.Extension}");

                            if (fiPicture.Exists)
                            {
                                try
                                {
                                    File.Copy(fiPicture.FullName, fiPicture.FullName.Replace(CurrentInfrastructure.InfrastructureTVItemID.ToString() + "_", NewTVItemID.ToString() + "_"));
                                }
                                catch (Exception)
                                {
                                    MessageText = $"Could not copy [{ fiPicture.FullName }] to [{ fiPicture.FullName.Replace(CurrentInfrastructure.InfrastructureTVItemID.ToString(), NewTVItemID.ToString()) }]\r\n";
                                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));
                                    return;
                                }

                                try
                                {
                                    fiPicture.Delete();
                                }
                                catch (Exception)
                                {
                                    MessageText = $"Could not remove [{ fiPicture.FullName }] after it was copied\r\n";
                                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));
                                    return;
                                }
                            }
                        }
                    }
                }

                CurrentInfrastructure.InfrastructureTVItemID = NewTVItemID;
                CurrentInfrastructure.TVText = InfrastructureName;
                CurrentInfrastructure.IsActive = IsActive;
                CurrentInfrastructure.IsActiveNew = null;
                CurrentInfrastructure.TVTextNew = null;
                CurrentInfrastructure.Lat = Lat;
                CurrentInfrastructure.Lng = Lng;
                CurrentInfrastructure.LatNew = null;
                CurrentInfrastructure.LngNew = null;
                CurrentInfrastructure.LatOutfall = LatOutfall;
                CurrentInfrastructure.LngOutfall = LngOutfall;
                CurrentInfrastructure.LatOutfallNew = null;
                CurrentInfrastructure.LngOutfallNew = null;
                CurrentInfrastructure.CommentEN = CommentEN;
                CurrentInfrastructure.CommentENNew = null;
                CurrentInfrastructure.CommentFR = CommentFR;
                CurrentInfrastructure.CommentFRNew = null;
                CurrentInfrastructure.InfrastructureType = (int?)InfrastructureType;
                CurrentInfrastructure.InfrastructureTypeNew = null;
                CurrentInfrastructure.FacilityType = (int?)FacilityType;
                CurrentInfrastructure.FacilityTypeNew = null;
                CurrentInfrastructure.IsMechanicallyAerated = IsMechanicallyAerated;
                CurrentInfrastructure.IsMechanicallyAeratedNew = null;
                CurrentInfrastructure.NumberOfCells = NumberOfCells;
                CurrentInfrastructure.NumberOfCellsNew = null;
                CurrentInfrastructure.NumberOfAeratedCells = NumberOfAeratedCells;
                CurrentInfrastructure.NumberOfAeratedCellsNew = null;
                CurrentInfrastructure.AerationType = (int?)AerationType;
                CurrentInfrastructure.AerationTypeNew = null;
                CurrentInfrastructure.PreliminaryTreatmentType = (int?)PreliminaryTreatmentType;
                CurrentInfrastructure.PreliminaryTreatmentTypeNew = null;
                CurrentInfrastructure.PrimaryTreatmentType = (int?)PrimaryTreatmentType;
                CurrentInfrastructure.PrimaryTreatmentTypeNew = null;
                CurrentInfrastructure.SecondaryTreatmentType = (int?)SecondaryTreatmentType;
                CurrentInfrastructure.SecondaryTreatmentTypeNew = null;
                CurrentInfrastructure.TertiaryTreatmentType = (int?)TertiaryTreatmentType;
                CurrentInfrastructure.TertiaryTreatmentTypeNew = null;
                CurrentInfrastructure.DisinfectionType = (int?)DisinfectionType;
                CurrentInfrastructure.DisinfectionTypeNew = null;
                CurrentInfrastructure.CollectionSystemType = (int?)CollectionSystemType;
                CurrentInfrastructure.CollectionSystemTypeNew = null;
                CurrentInfrastructure.AlarmSystemType = (int?)AlarmSystemType;
                CurrentInfrastructure.AlarmSystemTypeNew = null;
                CurrentInfrastructure.DesignFlow_m3_day = DesignFlow_m3_day;
                CurrentInfrastructure.DesignFlow_m3_dayNew = null;
                CurrentInfrastructure.AverageFlow_m3_day = AverageFlow_m3_day;
                CurrentInfrastructure.AverageFlow_m3_dayNew = null;
                CurrentInfrastructure.PeakFlow_m3_day = PeakFlow_m3_day;
                CurrentInfrastructure.PeakFlow_m3_dayNew = null;
                CurrentInfrastructure.PopServed = PopServed;
                CurrentInfrastructure.PopServedNew = null;
                CurrentInfrastructure.CanOverflow = (int?)CanOverflow;
                CurrentInfrastructure.CanOverflowNew = null;
                CurrentInfrastructure.ValveType = (int?)ValveType;
                CurrentInfrastructure.ValveTypeNew = null;
                CurrentInfrastructure.HasBackupPower = HasBackupPower;
                CurrentInfrastructure.HasBackupPowerNew = null;
                CurrentInfrastructure.PercFlowOfTotal = PercFlowOfTotal;
                CurrentInfrastructure.PercFlowOfTotalNew = null;
                CurrentInfrastructure.AverageDepth_m = AverageDepth_m;
                CurrentInfrastructure.AverageDepth_mNew = null;
                CurrentInfrastructure.NumberOfPorts = NumberOfPorts;
                CurrentInfrastructure.NumberOfPortsNew = null;
                CurrentInfrastructure.PortDiameter_m = PortDiameter_m;
                CurrentInfrastructure.PortDiameter_mNew = null;
                CurrentInfrastructure.PortSpacing_m = PortSpacing_m;
                CurrentInfrastructure.PortSpacing_mNew = null;
                CurrentInfrastructure.PortElevation_m = PortElevation_m;
                CurrentInfrastructure.PortElevation_mNew = null;
                CurrentInfrastructure.VerticalAngle_deg = VerticalAngle_deg;
                CurrentInfrastructure.VerticalAngle_degNew = null;
                CurrentInfrastructure.HorizontalAngle_deg = HorizontalAngle_deg;
                CurrentInfrastructure.HorizontalAngle_degNew = null;
                CurrentInfrastructure.DecayRate_per_day = DecayRate_per_day;
                CurrentInfrastructure.DecayRate_per_dayNew = null;
                CurrentInfrastructure.NearFieldVelocity_m_s = NearFieldVelocity_m_s;
                CurrentInfrastructure.NearFieldVelocity_m_sNew = null;
                CurrentInfrastructure.FarFieldVelocity_m_s = FarFieldVelocity_m_s;
                CurrentInfrastructure.FarFieldVelocity_m_sNew = null;
                CurrentInfrastructure.ReceivingWaterSalinity_PSU = ReceivingWaterSalinity_PSU;
                CurrentInfrastructure.ReceivingWaterSalinity_PSUNew = null;
                CurrentInfrastructure.ReceivingWaterTemperature_C = ReceivingWaterTemperature_C;
                CurrentInfrastructure.ReceivingWaterTemperature_CNew = null;
                CurrentInfrastructure.ReceivingWater_MPN_per_100ml = ReceivingWater_MPN_per_100ml;
                CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew = null;
                CurrentInfrastructure.DistanceFromShore_m = DistanceFromShore_m;
                CurrentInfrastructure.DistanceFromShore_mNew = null;
                CurrentInfrastructure.SeeOtherMunicipalityTVItemID = SeeOtherMunicipalityTVItemID;
                CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew = null;
                CurrentInfrastructure.SeeOtherMunicipalityText = SeeOtherMunicipalityText;
                CurrentInfrastructure.SeeOtherMunicipalityTextNew = null;
                CurrentInfrastructure.PumpsToTVItemID = PumpsToTVItemID;
                CurrentInfrastructure.PumpsToTVItemIDNew = null;
                InfrastructureTVItemID = (int)CurrentInfrastructure.InfrastructureTVItemID;
                #endregion reset variables

                NeedToSave = true;
            }

            if (CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID != null)
            {
                if (!(CurrentInfrastructure.InfrastructureAddress.StreetNumber == null
                    && CurrentInfrastructure.InfrastructureAddress.StreetName == null
                    && CurrentInfrastructure.InfrastructureAddress.StreetType == null
                    // && CurrentInfrastructure.InfrastructureAddress.Municipality == null
                    && CurrentInfrastructure.InfrastructureAddress.PostalCode == null
                    && CurrentInfrastructure.InfrastructureAddressNew.StreetNumber == null
                    && CurrentInfrastructure.InfrastructureAddressNew.StreetName == null
                    && CurrentInfrastructure.InfrastructureAddressNew.StreetType == null
                    // && CurrentInfrastructure.InfrastructureAddressNew.Municipality == null
                    && CurrentInfrastructure.InfrastructureAddressNew.PostalCode == null
                    && CurrentInfrastructure.InfrastructureAddress.StreetName == null))
                {
                    string StreetNumberText = CurrentInfrastructure.InfrastructureAddress.StreetNumber == null ? "" : CurrentInfrastructure.InfrastructureAddress.StreetNumber;
                    string StreetNameText = CurrentInfrastructure.InfrastructureAddress.StreetName == null ? "" : CurrentInfrastructure.InfrastructureAddress.StreetName;
                    int StreetType = CurrentInfrastructure.InfrastructureAddress.StreetType == null ? 0 : (int)CurrentInfrastructure.InfrastructureAddress.StreetType;
                    string MunicipalityText = CurrentInfrastructure.InfrastructureAddress.Municipality == null ? "" : CurrentInfrastructure.InfrastructureAddress.Municipality;
                    string PostalCodeText = CurrentInfrastructure.InfrastructureAddress.PostalCode == null ? "" : CurrentInfrastructure.InfrastructureAddress.PostalCode;
                    string StreetNumberNewText = CurrentInfrastructure.InfrastructureAddressNew.StreetNumber == null ? "" : CurrentInfrastructure.InfrastructureAddressNew.StreetNumber;
                    string StreetNameNewText = CurrentInfrastructure.InfrastructureAddressNew.StreetName == null ? "" : CurrentInfrastructure.InfrastructureAddressNew.StreetName;
                    int StreetTypeNew = CurrentInfrastructure.InfrastructureAddressNew.StreetType == null ? 0 : (int)CurrentInfrastructure.InfrastructureAddressNew.StreetType;
                    string MunicipalityNewText = CurrentInfrastructure.InfrastructureAddressNew.Municipality == null ? "" : CurrentInfrastructure.InfrastructureAddressNew.Municipality;
                    string PostalCodeNewText = CurrentInfrastructure.InfrastructureAddressNew.PostalCode == null ? "" : CurrentInfrastructure.InfrastructureAddressNew.PostalCode;

                    if (!string.IsNullOrWhiteSpace(MunicipalityText))
                    {
                        MunicipalityText = MunicipalityText.Trim();
                    }

                    if (!string.IsNullOrWhiteSpace(MunicipalityNewText))
                    {
                        MunicipalityNewText = MunicipalityNewText.Trim();
                    }

                    if (string.IsNullOrWhiteSpace(MunicipalityNewText))
                    {
                        string NoMunicipality = $"ERROR: To add an address you need to have a municipality name\r\n";
                        EmitRTBMessage(new RTBMessageEventArgs(NoMunicipality));
                        return;
                    }

                    MessageText = $"Checking if municipality exist [{MunicipalityNewText}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                    bool MunicipalityExist = false;
                    ret = MunicipalityExistUnderProvinceInCSSPWebTools((int)subsectorDoc.ProvinceTVItemID, MunicipalityNewText, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        MunicipalityExist = false;
                        NeedToSave = true;
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));
                        MunicipalityExist = true;
                    }

                    if (!MunicipalityExist)
                    {
                        MessageText = $"Trying to create address and municipality --- old [{StreetNumberText} {StreetNameText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetType)} {MunicipalityText} {PostalCodeText}] -- - new [{StreetNumberNewText} {StreetNameNewText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetTypeNew)} {MunicipalityNewText} {PostalCodeNewText}]\r\n";
                        EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                        ret = SaveToCSSPWebToolsAddress((int)subsectorDoc.ProvinceTVItemID, (int)CurrentInfrastructure.InfrastructureTVItemID, StreetNumberNewText, StreetNameNewText, StreetTypeNew, MunicipalityNewText, PostalCodeNewText, true, false, true, AdminEmail);
                        ret = ret.Replace("\"", "");
                        if (ret.StartsWith("ERROR"))
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                            if (NeedToSave)
                            {
                                SaveMunicipalityTextFile();
                            }
                        }
                        else
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                            CurrentInfrastructure.InfrastructureAddress.AddressType = (int)AddressTypeEnum.Civic;
                            CurrentInfrastructure.InfrastructureAddress.AddressTVItemID = int.Parse(ret);
                            CurrentInfrastructure.InfrastructureAddress.StreetNumber = CurrentInfrastructure.InfrastructureAddressNew.StreetNumber;
                            CurrentInfrastructure.InfrastructureAddress.StreetName = CurrentInfrastructure.InfrastructureAddressNew.StreetName;
                            CurrentInfrastructure.InfrastructureAddress.StreetType = CurrentInfrastructure.InfrastructureAddressNew.StreetType;
                            CurrentInfrastructure.InfrastructureAddress.Municipality = CurrentInfrastructure.InfrastructureAddressNew.Municipality;
                            CurrentInfrastructure.InfrastructureAddress.PostalCode = CurrentInfrastructure.InfrastructureAddressNew.PostalCode;
                            CurrentInfrastructure.InfrastructureAddressNew.StreetNumber = null;
                            CurrentInfrastructure.InfrastructureAddressNew.StreetName = null;
                            CurrentInfrastructure.InfrastructureAddressNew.StreetType = null;
                            CurrentInfrastructure.InfrastructureAddressNew.Municipality = null;
                            CurrentInfrastructure.InfrastructureAddressNew.PostalCode = null;
                            CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = null;
                            CurrentInfrastructure.InfrastructureAddressNew.AddressType = null;
                            NeedToSave = true;
                        }
                    }
                    else
                    {
                        MessageText = $"Trying to create address --- old [{StreetNumberText} {StreetNameText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetType)} {MunicipalityText} {PostalCodeText}] -- - new [{StreetNumberNewText} {StreetNameNewText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetTypeNew)} {MunicipalityNewText} {PostalCodeNewText}]\r\n";
                        EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                        ret = SaveToCSSPWebToolsAddress((int)subsectorDoc.ProvinceTVItemID, (int)CurrentInfrastructure.InfrastructureTVItemID, StreetNumberNewText, StreetNameNewText, StreetTypeNew, MunicipalityNewText, PostalCodeNewText, false, false, true, AdminEmail);
                        ret = ret.Replace("\"", "");
                        if (ret.StartsWith("ERROR"))
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                            if (NeedToSave)
                            {
                                SaveMunicipalityTextFile();
                            }
                        }
                        else
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                            CurrentInfrastructure.InfrastructureAddress.AddressType = (int)AddressTypeEnum.Civic;
                            CurrentInfrastructure.InfrastructureAddress.AddressTVItemID = int.Parse(ret);
                            CurrentInfrastructure.InfrastructureAddress.StreetNumber = CurrentInfrastructure.InfrastructureAddressNew.StreetNumber;
                            CurrentInfrastructure.InfrastructureAddress.StreetName = CurrentInfrastructure.InfrastructureAddressNew.StreetName;
                            CurrentInfrastructure.InfrastructureAddress.StreetType = CurrentInfrastructure.InfrastructureAddressNew.StreetType;
                            CurrentInfrastructure.InfrastructureAddress.Municipality = CurrentInfrastructure.InfrastructureAddressNew.Municipality;
                            CurrentInfrastructure.InfrastructureAddress.PostalCode = CurrentInfrastructure.InfrastructureAddressNew.PostalCode;
                            CurrentInfrastructure.InfrastructureAddressNew.StreetNumber = null;
                            CurrentInfrastructure.InfrastructureAddressNew.StreetName = null;
                            CurrentInfrastructure.InfrastructureAddressNew.StreetType = null;
                            CurrentInfrastructure.InfrastructureAddressNew.Municipality = null;
                            CurrentInfrastructure.InfrastructureAddressNew.PostalCode = null;
                            CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = null;
                            CurrentInfrastructure.InfrastructureAddressNew.AddressType = null;
                            NeedToSave = true;
                        }
                    }
                }
                else
                {
                    CurrentInfrastructure.InfrastructureAddressNew.StreetNumber = null;
                    CurrentInfrastructure.InfrastructureAddressNew.StreetName = null;
                    CurrentInfrastructure.InfrastructureAddressNew.StreetType = null;
                    CurrentInfrastructure.InfrastructureAddressNew.Municipality = null;
                    CurrentInfrastructure.InfrastructureAddressNew.PostalCode = null;
                    CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = null;
                    CurrentInfrastructure.InfrastructureAddressNew.AddressType = null;
                }
            }

            List<int> PictureTVItemIDListToRemove = new List<int>();

            foreach (Picture picture in CurrentInfrastructure.InfrastructurePictureList)
            {
                FileInfo fiPictureToDelete = new FileInfo($@"C:\PollutionSourceSites\Infrastructures\{CurrentMunicipalityName}\Pictures\{CurrentInfrastructure.InfrastructureTVItemID}_{picture.PictureTVItemID}{picture.Extension}");

                if (picture.ToRemove == true)
                {
                    if (picture.PictureTVItemID >= 10000000)
                    {
                        if (fiPictureToDelete.Exists)
                        {
                            try
                            {
                                fiPictureToDelete.Delete();
                            }
                            catch (Exception ex)
                            {
                                string ErrMessage = ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
                                EmitRTBMessage(new RTBMessageEventArgs($"{ErrMessage}"));
                            }
                        }

                        continue;
                    }

                    MessageText = $"Removing picture --- [{picture.FileNameNew}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                    ret = SaveToCSSPWebToolsPictureToRemove((int)CurrentInfrastructure.InfrastructureTVItemID, (int)picture.PictureTVItemID, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR:"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                        if (NeedToSave)
                        {
                            SaveMunicipalityTextFile();
                        }
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                        PictureTVItemIDListToRemove.Add((int)picture.PictureTVItemID);

                        if (fiPictureToDelete.Exists)
                        {
                            try
                            {
                                fiPictureToDelete.Delete();
                            }
                            catch (Exception ex)
                            {
                                EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                                string ErrMessage = ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
                                EmitRTBMessage(new RTBMessageEventArgs($"{ErrMessage}"));
                            }
                        }

                        NeedToSave = true;
                    }

                }
            }

            foreach (int pictureTVItemIDToDelete in PictureTVItemIDListToRemove)
            {
                Picture picture = CurrentInfrastructure.InfrastructurePictureList.Where(c => c.PictureTVItemID == pictureTVItemIDToDelete).FirstOrDefault();

                if (picture != null)
                {
                    CurrentInfrastructure.InfrastructurePictureList.Remove(picture);
                }
            }

            foreach (Picture picture in CurrentInfrastructure.InfrastructurePictureList)
            {
                bool IsNew = false;
                if (picture.PictureTVItemID >= 10000000)
                {
                    IsNew = true;

                    MessageText = $"Adding picture --- [{picture.FileNameNew}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                    FileInfo fiPicture = new FileInfo($@"C:\PollutionSourceSites\Infrastructures\{CurrentMunicipalityName}\Pictures\{CurrentInfrastructure.InfrastructureTVItemID}_{picture.PictureTVItemID}{picture.Extension}");

                    if (!fiPicture.Exists)
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        continue;
                    }

                    ret = SaveToCSSPWebToolsPicture(fiPicture, (int)CurrentInfrastructure.InfrastructureTVItemID, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR:"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                        if (NeedToSave)
                        {
                            SaveMunicipalityTextFile();
                        }
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                        picture.PictureTVItemID = int.Parse(ret);

                        FileInfo fiPictureNew = new FileInfo($@"C:\PollutionSourceSites\Infrastructures\{CurrentMunicipalityName}\Pictures\{CurrentInfrastructure.InfrastructureTVItemID}_{picture.PictureTVItemID}{picture.Extension}");

                        if (fiPicture.Exists)
                        {
                            try
                            {
                                File.Copy(fiPicture.FullName, fiPictureNew.FullName);
                                fiPicture.Delete();
                            }
                            catch (Exception ex)
                            {
                                EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                                string ErrMessage = ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
                                EmitRTBMessage(new RTBMessageEventArgs($"{ErrMessage}"));
                            }
                        }

                        NeedToSave = true;
                    }
                }

                if (picture.FileNameNew != null
                    || picture.DescriptionNew != null
                    || picture.ExtensionNew != null
                    || IsNew)
                {
                    string FileNameText = picture.FileNameNew != null ? picture.FileNameNew : picture.FileName;
                    string DescriptionText = picture.DescriptionNew != null ? picture.DescriptionNew : picture.Description;
                    string ExtensionText = picture.ExtensionNew != null ? picture.ExtensionNew : picture.Extension;
                    bool FromWater = picture.FromWaterNew != null ? (bool)picture.FromWaterNew : (picture.FromWater != null ? (bool)picture.FromWater : false);

                    MessageText = $"Changing properties of picture --- [{picture.FileNameNew}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));


                    ret = SaveToCSSPWebToolsPictureInfo((int)CurrentInfrastructure.InfrastructureTVItemID, (int)picture.PictureTVItemID, FileNameText, DescriptionText, ExtensionText, FromWater, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR:"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                        if (NeedToSave)
                        {
                            SaveMunicipalityTextFile();
                        }
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                        picture.PictureTVItemID = int.Parse(ret);
                        picture.FileName = FileNameText;
                        picture.Description = DescriptionText;
                        picture.Extension = ExtensionText;
                        picture.FileNameNew = null;
                        picture.DescriptionNew = null;
                        picture.ExtensionNew = null;
                        NeedToSave = true;
                    }
                }
            }

            if (NeedToSave)
            {
                // SaveMunicipalityTextFile();
            }

            EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: completed"));

        }
        public void ContactSaveToCSSPWebTools()
        {
            string ret = "";
            bool NeedToSave = false;

            string FirstName = string.IsNullOrEmpty(CurrentContact.FirstNameNew) == false ? CurrentContact.FirstNameNew : CurrentContact.FirstName;
            string Initial = string.IsNullOrEmpty(CurrentContact.InitialNew) == false ? CurrentContact.InitialNew : CurrentContact.Initial;
            if (!string.IsNullOrWhiteSpace(Initial))
            {
                Initial = Initial + ", ";
            }
            string LastName = string.IsNullOrEmpty(CurrentContact.LastNameNew) == false ? CurrentContact.LastNameNew : CurrentContact.LastName;
            string FullName = $"{FirstName} {Initial} {LastName}";

            foreach (Contact contact in municipalityDoc.Municipality.ContactList)
            {
                if (contact.ContactTVItemID != CurrentContact.ContactTVItemID)
                {
                    string FirstName2 = contact.FirstNameNew != null ? contact.FirstNameNew : contact.FirstName;
                    string Initial2 = contact.InitialNew != null ? contact.InitialNew : contact.Initial;
                    string LastName2 = contact.LastNameNew != null ? contact.LastNameNew : contact.LastName;

                    if (FirstName == FirstName2 && Initial == Initial2 && LastName == LastName2)
                    {
                        MessageBox.Show("At least 2 contacts have the same First Name, Initial and Last Name\r\nPlease change one of them", "Error: Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            EmitRTBMessage(new RTBMessageEventArgs($"Checking if contact [{FullName}] already exist in CSSPWebTools\r\n"));

            ret = ContactExistInCSSPWebTools((int)CurrentContact.ContactTVItemID, AdminEmail);
            ret = ret.Replace("\"", "");

            if (ret.StartsWith("ERROR:"))
            {
                EmitRTBMessage(new RTBMessageEventArgs($"Contact [{FullName}] does not exist in CSSPWebTools\r\n"));
            }
            else
            {
                EmitRTBMessage(new RTBMessageEventArgs($"Contact [{FullName}] already exist in CSSPWebTools\r\n"));
            }

            #region Load Variables
            bool IsActive = true;
            // FirstName already loaded
            // Inital already loaded
            // LastName already loaded
            string Email = "";
            ContactTitleEnum? Title = null;

            // IsActive
            if (CurrentContact.IsActiveNew == null)
            {
                if (CurrentContact.IsActive != null)
                {
                    IsActive = (bool)CurrentContact.IsActive;
                }
            }
            else
            {
                IsActive = (bool)CurrentContact.IsActiveNew;
            }

            // Email
            if (CurrentContact.EmailNew == null)
            {
                if (CurrentContact.Email != null)
                {
                    Email = CurrentContact.Email;
                }
            }
            else
            {
                Email = CurrentContact.EmailNew;
            }

            // Title
            if (CurrentContact.ContactTitleNew == null)
            {
                if (CurrentContact.ContactTitle != null)
                {
                    Title = (ContactTitleEnum)CurrentContact.ContactTitle;
                }
            }
            else
            {
                Title = (ContactTitleEnum)CurrentContact.ContactTitleNew;
            }


            string MessageText = $"Trying to save all information for Contact [{FullName}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tMunicipality TVItemID\t[{(int)municipalityDoc.Municipality.MunicipalityTVItemID}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tContact TVItemID\t[{(int)CurrentContact.ContactTVItemID}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tIsActive[{IsActive}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tFirst Name\t[{FirstName}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tInitial\t[{Initial}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tLast Name\t[{LastName}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tEmail\t[{Email}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            MessageText = $"\t\tTitle\t[{Title.ToString()}]\r\n";
            EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));
            #endregion Load Variables

            ret = SaveToCSSPWebToolsCreateOrModifyContact((int)municipalityDoc.Municipality.MunicipalityTVItemID,
                (int)CurrentContact.ContactTVItemID, FirstName, Initial, LastName, Email, Title, IsActive, AdminEmail);
            ret = ret.Replace("\"", "");
            if (ret.StartsWith("ERROR:"))
            {
                EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                if (NeedToSave)
                {
                    SaveMunicipalityTextFile();
                }
            }
            else
            {
                EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                #region reset variables
                int NewTVItemID = int.Parse(ret);
                if (CurrentContact.ContactTVItemID != NewTVItemID)
                {
                    CurrentContact.ContactTVItemID = NewTVItemID;
                }

                CurrentContact.ContactTVItemID = NewTVItemID;
                CurrentContact.IsActive = IsActive;
                CurrentContact.IsActiveNew = null;
                CurrentContact.FirstName = FirstName;
                CurrentContact.FirstNameNew = null;
                CurrentContact.Initial = Initial;
                CurrentContact.InitialNew = null;
                CurrentContact.LastName = LastName;
                CurrentContact.LastNameNew = null;
                CurrentContact.Email = Email;
                CurrentContact.EmailNew = null;
                ContactTVItemID = (int)CurrentContact.ContactTVItemID;
                #endregion reset variables

                NeedToSave = true;
            }

            if (CurrentContact.ContactAddressNew.AddressTVItemID != null)
            {
                if (!(CurrentContact.ContactAddress.StreetNumber == null
                    && CurrentContact.ContactAddress.StreetName == null
                    && CurrentContact.ContactAddress.StreetType == null
                    // && CurrentContact.ContactAddress.Municipality == null
                    && CurrentContact.ContactAddress.PostalCode == null
                    && CurrentContact.ContactAddressNew.StreetNumber == null
                    && CurrentContact.ContactAddressNew.StreetName == null
                    && CurrentContact.ContactAddressNew.StreetType == null
                    // && CurrentContact.ContactAddressNew.Municipality == null
                    && CurrentContact.ContactAddressNew.PostalCode == null
                    && CurrentContact.ContactAddress.StreetName == null))
                {
                    string StreetNumberText = CurrentContact.ContactAddress.StreetNumber == null ? "" : CurrentContact.ContactAddress.StreetNumber;
                    string StreetNameText = CurrentContact.ContactAddress.StreetName == null ? "" : CurrentContact.ContactAddress.StreetName;
                    int StreetType = CurrentContact.ContactAddress.StreetType == null ? 0 : (int)CurrentContact.ContactAddress.StreetType;
                    string MunicipalityText = CurrentContact.ContactAddress.Municipality == null ? "" : CurrentContact.ContactAddress.Municipality;
                    string PostalCodeText = CurrentContact.ContactAddress.PostalCode == null ? "" : CurrentContact.ContactAddress.PostalCode;
                    string StreetNumberNewText = CurrentContact.ContactAddressNew.StreetNumber == null ? "" : CurrentContact.ContactAddressNew.StreetNumber;
                    string StreetNameNewText = CurrentContact.ContactAddressNew.StreetName == null ? "" : CurrentContact.ContactAddressNew.StreetName;
                    int StreetTypeNew = CurrentContact.ContactAddressNew.StreetType == null ? 0 : (int)CurrentContact.ContactAddressNew.StreetType;
                    string MunicipalityNewText = CurrentContact.ContactAddressNew.Municipality == null ? "" : CurrentContact.ContactAddressNew.Municipality;
                    string PostalCodeNewText = CurrentContact.ContactAddressNew.PostalCode == null ? "" : CurrentContact.ContactAddressNew.PostalCode;

                    if (!string.IsNullOrWhiteSpace(MunicipalityText))
                    {
                        MunicipalityText = MunicipalityText.Trim();
                    }

                    if (!string.IsNullOrWhiteSpace(MunicipalityNewText))
                    {
                        MunicipalityNewText = MunicipalityNewText.Trim();
                    }

                    if (string.IsNullOrWhiteSpace(MunicipalityNewText))
                    {
                        string NoMunicipality = $"ERROR: To add an address you need to have a municipality name\r\n";
                        EmitRTBMessage(new RTBMessageEventArgs(NoMunicipality));
                        return;
                    }

                    MessageText = $"Checking if municipality exist [{MunicipalityNewText}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                    bool MunicipalityExist = false;
                    ret = MunicipalityExistUnderProvinceInCSSPWebTools((int)subsectorDoc.ProvinceTVItemID, MunicipalityNewText, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        MunicipalityExist = false;
                        NeedToSave = true;
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));
                        MunicipalityExist = true;
                    }

                    if (!MunicipalityExist)
                    {
                        MessageText = $"Trying to create address and municipality --- old [{StreetNumberText} {StreetNameText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetType)} {MunicipalityText} {PostalCodeText}] -- - new [{StreetNumberNewText} {StreetNameNewText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetTypeNew)} {MunicipalityNewText} {PostalCodeNewText}]\r\n";
                        EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                        ret = SaveToCSSPWebToolsAddress((int)subsectorDoc.ProvinceTVItemID, (int)CurrentContact.ContactTVItemID, StreetNumberNewText, StreetNameNewText, StreetTypeNew, MunicipalityNewText, PostalCodeNewText, true, false, true, AdminEmail);
                        ret = ret.Replace("\"", "");
                        if (ret.StartsWith("ERROR"))
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                            if (NeedToSave)
                            {
                                SaveMunicipalityTextFile();
                            }
                        }
                        else
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                            CurrentContact.ContactAddress.AddressType = (int)AddressTypeEnum.Civic;
                            CurrentContact.ContactAddress.AddressTVItemID = int.Parse(ret);
                            CurrentContact.ContactAddress.StreetNumber = CurrentContact.ContactAddressNew.StreetNumber;
                            CurrentContact.ContactAddress.StreetName = CurrentContact.ContactAddressNew.StreetName;
                            CurrentContact.ContactAddress.StreetType = CurrentContact.ContactAddressNew.StreetType;
                            CurrentContact.ContactAddress.Municipality = CurrentContact.ContactAddressNew.Municipality;
                            CurrentContact.ContactAddress.PostalCode = CurrentContact.ContactAddressNew.PostalCode;
                            CurrentContact.ContactAddressNew.StreetNumber = null;
                            CurrentContact.ContactAddressNew.StreetName = null;
                            CurrentContact.ContactAddressNew.StreetType = null;
                            CurrentContact.ContactAddressNew.Municipality = null;
                            CurrentContact.ContactAddressNew.PostalCode = null;
                            CurrentContact.ContactAddressNew.AddressTVItemID = null;
                            CurrentContact.ContactAddressNew.AddressType = null;
                            NeedToSave = true;
                        }
                    }
                    else
                    {
                        MessageText = $"Trying to create address --- old [{StreetNumberText} {StreetNameText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetType)} {MunicipalityText} {PostalCodeText}] -- - new [{StreetNumberNewText} {StreetNameNewText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetTypeNew)} {MunicipalityNewText} {PostalCodeNewText}]\r\n";
                        EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                        ret = SaveToCSSPWebToolsAddress((int)subsectorDoc.ProvinceTVItemID, (int)CurrentContact.ContactTVItemID, StreetNumberNewText, StreetNameNewText, StreetTypeNew, MunicipalityNewText, PostalCodeNewText, false, false, true, AdminEmail);
                        ret = ret.Replace("\"", "");
                        if (ret.StartsWith("ERROR"))
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                            if (NeedToSave)
                            {
                                SaveMunicipalityTextFile();
                            }
                        }
                        else
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                            CurrentContact.ContactAddress.AddressType = (int)AddressTypeEnum.Civic;
                            CurrentContact.ContactAddress.AddressTVItemID = int.Parse(ret);
                            CurrentContact.ContactAddress.StreetNumber = CurrentContact.ContactAddressNew.StreetNumber;
                            CurrentContact.ContactAddress.StreetName = CurrentContact.ContactAddressNew.StreetName;
                            CurrentContact.ContactAddress.StreetType = CurrentContact.ContactAddressNew.StreetType;
                            CurrentContact.ContactAddress.Municipality = CurrentContact.ContactAddressNew.Municipality;
                            CurrentContact.ContactAddress.PostalCode = CurrentContact.ContactAddressNew.PostalCode;
                            CurrentContact.ContactAddressNew.StreetNumber = null;
                            CurrentContact.ContactAddressNew.StreetName = null;
                            CurrentContact.ContactAddressNew.StreetType = null;
                            CurrentContact.ContactAddressNew.Municipality = null;
                            CurrentContact.ContactAddressNew.PostalCode = null;
                            CurrentContact.ContactAddressNew.AddressTVItemID = null;
                            CurrentContact.ContactAddressNew.AddressType = null;
                            NeedToSave = true;
                        }
                    }
                }
                else
                {
                    CurrentContact.ContactAddressNew.StreetNumber = null;
                    CurrentContact.ContactAddressNew.StreetName = null;
                    CurrentContact.ContactAddressNew.StreetType = null;
                    CurrentContact.ContactAddressNew.Municipality = null;
                    CurrentContact.ContactAddressNew.PostalCode = null;
                    CurrentContact.ContactAddressNew.AddressTVItemID = null;
                    CurrentContact.ContactAddressNew.AddressType = null;
                }
            }

            //foreach (Telephone tel in CurrentContact.TelephoneList)
            //{
            //    EmitRTBMessage(new RTBMessageEventArgs($"Checking if telephone [{tel.TelNumber}] already exist in CSSPWebTools\r\n"));

            //    ret = TelExistInCSSPWebTools((int)tel.TelTVItemID, AdminEmail);
            //    ret = ret.Replace("\"", "");

            //    if (ret.StartsWith("ERROR:"))
            //    {
            //        EmitRTBMessage(new RTBMessageEventArgs($"Telephone [{tel.TelNumber}] does not exist in CSSPWebTools\r\n"));
            //    }
            //    else
            //    {
            //        EmitRTBMessage(new RTBMessageEventArgs($"Telephone [{tel.TelNumber}] already exist in CSSPWebTools\r\n"));
            //    }

            //    int ContactTVItemID = (int)CurrentContact.ContactTVItemID;
            //    int TelTVItemID = (int)tel.TelTVItemID;
            //    int? TelType = tel.TelTypeNew != null ? tel.TelTypeNew : tel.TelType;
            //    string TelNumber = tel.TelNumberNew != null ? tel.TelNumberNew : tel.TelNumber;
            //    bool ShouldDelete = tel.ShouldDelete;


            //    MessageText = $"\t\tContact TVItemID\t[{(int)CurrentContact.ContactTVItemID}]\r\n";
            //    EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            //    MessageText = $"\t\tTelephone Type\t[{TelType}]\r\n";
            //    EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            //    MessageText = $"\t\tTelephone Number\t[{TelNumber}]\r\n";
            //    EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            //    MessageText = $"\t\tShould Delete\t[{ShouldDelete}]\r\n";
            //    EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            //    ret = SaveToCSSPWebToolsCreateOrModifyTel(ContactTVItemID, TelTVItemID, TelType, TelNumber, ShouldDelete, AdminEmail);
            //    ret = ret.Replace("\"", "");
            //    if (ret.StartsWith("ERROR:"))
            //    {
            //        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
            //        if (NeedToSave)
            //        {
            //            SaveMunicipalityTextFile();
            //        }
            //    }
            //    else
            //    {
            //        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

            //        #region reset variables
            //        int NewTVItemID = int.Parse(ret);

            //        tel.TelTVItemID = NewTVItemID;
            //        tel.TelType = TelType;
            //        tel.TelTypeNew = null;
            //        tel.TelNumber = TelNumber;
            //        tel.TelNumberNew = null;
            //        #endregion reset variables

            //        NeedToSave = true;
            //    }
            //}

            //foreach (Email email in CurrentContact.EmailList)
            //{
            //    EmitRTBMessage(new RTBMessageEventArgs($"Checking if email [{email.EmailAddress}] already exist in CSSPWebTools\r\n"));

            //    ret = EmailExistInCSSPWebTools((int)email.EmailTVItemID, AdminEmail);
            //    ret = ret.Replace("\"", "");

            //    if (ret.StartsWith("ERROR:"))
            //    {
            //        EmitRTBMessage(new RTBMessageEventArgs($"Email [{email.EmailAddress}] does not exist in CSSPWebTools\r\n"));
            //    }
            //    else
            //    {
            //        EmitRTBMessage(new RTBMessageEventArgs($"Email [{email.EmailAddress}] already exist in CSSPWebTools\r\n"));
            //    }

            //    int ContactTVItemID = (int)CurrentContact.ContactTVItemID;
            //    int EmailTVItemID = (int)email.EmailTVItemID;
            //    int? EmailType = email.EmailTypeNew != null ? email.EmailTypeNew : email.EmailType;
            //    string EmailAddress = email.EmailAddressNew != null ? email.EmailAddressNew : email.EmailAddress;
            //    bool ShouldDelete = email.ShouldDelete;


            //    MessageText = $"\t\tContact TVItemID\t[{(int)CurrentContact.ContactTVItemID}]\r\n";
            //    EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            //    MessageText = $"\t\tEmail Type\t[{EmailType}]\r\n";
            //    EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            //    MessageText = $"\t\tEmail Address\t[{EmailAddress}]\r\n";
            //    EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            //    MessageText = $"\t\tShould Delete\t[{ShouldDelete}]\r\n";
            //    EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

            //    ret = SaveToCSSPWebToolsCreateOrModifyEmail(ContactTVItemID, EmailTVItemID, EmailType, EmailAddress, ShouldDelete, AdminEmail);
            //    ret = ret.Replace("\"", "");
            //    if (ret.StartsWith("ERROR:"))
            //    {
            //        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
            //        if (NeedToSave)
            //        {
            //            SaveMunicipalityTextFile();
            //        }
            //    }
            //    else
            //    {
            //        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

            //        #region reset variables
            //        int NewTVItemID = int.Parse(ret);

            //        email.EmailTVItemID = NewTVItemID;
            //        email.EmailType = EmailType;
            //        email.EmailTypeNew = null;
            //        email.EmailAddress = EmailAddress;
            //        email.EmailAddressNew = null;
            //        #endregion reset variables

            //        NeedToSave = true;
            //    }
            //}


            if (NeedToSave)
            {
                // SaveMunicipalityTextFile();
            }

            EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: completed"));

        }
        public void InfrastructureSaveAllToCSSPWebTools()
        {
            foreach (Contact contact in municipalityDoc.Municipality.ContactList)
            {
                CurrentContact = contact;
                ContactSaveToCSSPWebTools();
            }


            foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList)
            {
                int PumpToTVItemID = 0;
                if (infrastructure.PumpsToTVItemIDNew != null)
                {
                    PumpToTVItemID = (int)infrastructure.PumpsToTVItemIDNew;
                }
                else
                {
                    if (infrastructure.PumpsToTVItemID != null)
                    {
                        PumpToTVItemID = (int)infrastructure.PumpsToTVItemID;
                    }
                }
                if (PumpToTVItemID == 0)
                {
                    InfrastructureSaveAllToCSSPWebToolsRecursive(infrastructure);
                }
            }

            SaveMunicipalityTextFile();
            RedrawContactAndInfrastructureList();
            ReDrawContactAndInfrastructure();

        }
        public void InfrastructureSaveAllToCSSPWebToolsRecursive(Infrastructure infrastructure)
        {
            CurrentInfrastructure = infrastructure;
            InfrastructureSaveToCSSPWebTools();

            foreach (Infrastructure infrastructureChild in municipalityDoc.Municipality.InfrastructureList)
            {
                if (infrastructureChild.PumpsToTVItemIDNew != null)
                {
                    if (infrastructureChild.PumpsToTVItemIDNew == infrastructure.InfrastructureTVItemID)
                    {
                        InfrastructureSaveAllToCSSPWebToolsRecursive(infrastructureChild);
                    }
                }
                else
                {
                    if (infrastructureChild.PumpsToTVItemID == infrastructure.InfrastructureTVItemID)
                    {
                        InfrastructureSaveAllToCSSPWebToolsRecursive(infrastructureChild);
                    }
                }
            }
        }
        public void PSSSaveToCSSPWebTools()
        {
            string ret = "";
            bool NeedToSave = false;
            bool IsNewPSS = false;

            string PSSName = CurrentPSS.TVText;
            if (CurrentPSS.TVTextNew != null)
            {
                PSSName = CurrentPSS.TVTextNew;
            }

            EmitRTBMessage(new RTBMessageEventArgs($"Trying to save all information for PSS [{PSSName}]\r\n"));

            EmitRTBMessage(new RTBMessageEventArgs($"Checking if PSS [{PSSName}] already exist in CSSPWebTools\r\n"));

            ret = PSSExistInCSSPWebTools((int)CurrentPSS.PSSTVItemID, AdminEmail);
            ret = ret.Replace("\"", "");

            if (ret.StartsWith("ERROR:"))
            {
                EmitRTBMessage(new RTBMessageEventArgs($"PSS [{PSSName}] does not exist in CSSPWebTools\r\n"));
            }
            else
            {
                EmitRTBMessage(new RTBMessageEventArgs($"PSS [{PSSName}] already exist in CSSPWebTools\r\n"));
            }

            if (CurrentPSS.PSSTVItemID >= 10000000 || ret.StartsWith("ERROR:"))
            {
                float Lat = 0.0f;
                float Lng = 0.0f;
                if (CurrentPSS.LatNew == null)
                {
                    if (CurrentPSS.Lat != null)
                    {
                        Lat = (float)CurrentPSS.Lat;
                    }
                }
                else
                {
                    Lat = (float)CurrentPSS.LatNew;
                }
                if (CurrentPSS.LngNew == null)
                {
                    if (CurrentPSS.Lng != null)
                    {
                        Lng = (float)CurrentPSS.Lng;
                    }
                }
                else
                {
                    Lng = (float)CurrentPSS.LngNew;
                }

                string MessageText = $"Trying to creating new PSS [{PSSName}]\tSite Number [{(int)CurrentPSS.SiteNumber}]\tLat[{Lat}]\tLong[{Lng}]\r\n";
                EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

                ret = SaveToCSSPWebToolsCreateNewPSS((int)subsectorDoc.Subsector.SubsectorTVItemID, (int)CurrentPSS.PSSTVItemID, PSSName, (int)CurrentPSS.SiteNumber, Lat, Lng, AdminEmail);
                ret = ret.Replace("\"", "");
                if (ret.StartsWith("ERROR:"))
                {
                    EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                    EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                    if (NeedToSave)
                    {
                        SaveSubsectorTextFile();
                    }
                }
                else
                {
                    EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                    CurrentPSS.PSSTVItemID = int.Parse(ret);
                    CurrentPSS.TVText = PSSName;
                    CurrentPSS.TVTextNew = null;
                    CurrentPSS.Lat = Lat;
                    CurrentPSS.Lng = Lng;
                    CurrentPSS.LatNew = null;
                    CurrentPSS.LngNew = null;
                    PolSourceSiteTVItemID = (int)CurrentPSS.PSSTVItemID;
                    NeedToSave = true;
                    IsNewPSS = true;
                }
            }

            //if (!IsNewPSS)
            //{

            if (CurrentPSS.TVTextNew != null || CurrentPSS.IsActiveNew != null || CurrentPSS.IsPointSourceNew != null)
            {
                bool IsActive = false;
                bool IsPointSource = false;

                string TVText = CurrentPSS.TVText.Trim();
                if (CurrentPSS.TVTextNew != null)
                {
                    if (CurrentPSS.TVTextNew != CurrentPSS.TVText)
                    {
                        TVText = CurrentPSS.TVTextNew.Trim();
                        string MessageText = $"Trying To change PSS Name --- old [{CurrentPSS.TVText}] --- new [{CurrentPSS.TVTextNew}]\r\n";
                        EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));
                    }
                }

                if (CurrentPSS.IsActiveNew != null && CurrentPSS.IsActiveNew != CurrentPSS.IsActive)
                {
                    IsActive = (bool)CurrentPSS.IsActiveNew;
                    string MessageText = $"Trying To change PSS IsActive --- old [{CurrentPSS.IsActive}] --- new [{CurrentPSS.IsActiveNew}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));
                }
                else
                {
                    IsActive = (bool)CurrentPSS.IsActive;
                }

                if (CurrentPSS.IsPointSourceNew != null && CurrentPSS.IsPointSourceNew != CurrentPSS.IsPointSource)
                {
                    IsPointSource = (bool)CurrentPSS.IsPointSourceNew;
                    string MessageText = $"Trying To change PSS IsPointSource --- old [{CurrentPSS.IsPointSource}] --- new [{CurrentPSS.IsPointSource}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));
                }
                else
                {
                    IsPointSource = (bool)CurrentPSS.IsPointSource;
                }


                ret = SaveToCSSPWebToolsTVText((int)CurrentPSS.PSSTVItemID, TVText, IsActive, IsPointSource, AdminEmail);
                ret = ret.Replace("\"", "");
                if (ret.StartsWith("ERROR:"))
                {
                    string MessageText = $"Trying to change PSS Name and/or IsActive and/or IsPointSource\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                    EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                    if (NeedToSave)
                    {
                        SaveSubsectorTextFile();
                    }
                }
                else
                {
                    string MessageText = $"Trying to change PSS Name and/or IsActive and/or IsPointSource\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                    CurrentPSS.TVText = TVText;
                    CurrentPSS.IsActive = IsActive;
                    CurrentPSS.IsPointSource = IsPointSource;

                    CurrentPSS.TVTextNew = null;
                    CurrentPSS.IsActiveNew = null;
                    CurrentPSS.IsPointSourceNew = null;
                    NeedToSave = true;
                }
            }

            if (CurrentPSS.LatNew != null || CurrentPSS.LngNew != null)
            {
                float Lat = 0.0f;
                float Lng = 0.0f;
                if (CurrentPSS.LatNew == null)
                {
                    if (CurrentPSS.Lat != null)
                    {
                        Lat = (float)CurrentPSS.Lat;
                    }
                }
                else
                {
                    Lat = (float)CurrentPSS.LatNew;
                }
                if (CurrentPSS.LngNew == null)
                {
                    if (CurrentPSS.Lng != null)
                    {
                        Lng = (float)CurrentPSS.Lng;
                    }
                }
                else
                {
                    Lng = (float)CurrentPSS.LngNew;
                }


                string LatText = CurrentPSS.Lat == null ? "(empty)" : ((float)CurrentPSS.Lat).ToString("F5");
                string LngText = CurrentPSS.Lng == null ? "(empty)" : ((float)CurrentPSS.Lng).ToString("F5");

                string MessageText = $"Trying to Change Lat and Lng --- old [{LatText} {LngText}] --- new [{Lat.ToString("F5")} {Lng.ToString("F5")}]\r\n";
                EmitRTBMessage(new RTBMessageEventArgs($"{MessageText}"));

                ret = SaveToCSSPWebToolsLatLng((int)CurrentPSS.PSSTVItemID, Lat, Lng, TVTypeEnum.PolSourceSite, AdminEmail);
                ret = ret.Replace("\"", "");
                if (ret.StartsWith("ERROR:"))
                {
                    EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                    EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                    if (NeedToSave)
                    {
                        SaveSubsectorTextFile();
                    }
                }
                else
                {
                    EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                    CurrentPSS.Lat = Lat;
                    CurrentPSS.Lng = Lng;
                    CurrentPSS.LatNew = null;
                    CurrentPSS.LngNew = null;
                    NeedToSave = true;
                }
            }
            //}

            if (CurrentPSS.PSSAddressNew.AddressTVItemID != null || IsNewPSS)
            {
                if (!(CurrentPSS.PSSAddress.StreetNumber == null
                    && CurrentPSS.PSSAddress.StreetName == null
                    && CurrentPSS.PSSAddress.StreetType == null
                    && CurrentPSS.PSSAddress.Municipality == null
                    && CurrentPSS.PSSAddress.PostalCode == null
                    && CurrentPSS.PSSAddressNew.StreetNumber == null
                    && CurrentPSS.PSSAddressNew.StreetName == null
                    && CurrentPSS.PSSAddressNew.StreetType == null
                    && CurrentPSS.PSSAddressNew.Municipality == null
                    && CurrentPSS.PSSAddressNew.PostalCode == null
                    && CurrentPSS.PSSAddress.StreetName == null))
                {
                    string StreetNumberText = CurrentPSS.PSSAddress.StreetNumber == null ? "" : CurrentPSS.PSSAddress.StreetNumber;
                    string StreetNameText = CurrentPSS.PSSAddress.StreetName == null ? "" : CurrentPSS.PSSAddress.StreetName;
                    int StreetType = CurrentPSS.PSSAddress.StreetType == null ? 0 : (int)CurrentPSS.PSSAddress.StreetType;
                    string MunicipalityText = CurrentPSS.PSSAddress.Municipality == null ? "" : CurrentPSS.PSSAddress.Municipality;
                    string PostalCodeText = CurrentPSS.PSSAddress.PostalCode == null ? "" : CurrentPSS.PSSAddress.PostalCode;
                    string StreetNumberNewText = CurrentPSS.PSSAddressNew.StreetNumber == null ? "" : CurrentPSS.PSSAddressNew.StreetNumber;
                    string StreetNameNewText = CurrentPSS.PSSAddressNew.StreetName == null ? "" : CurrentPSS.PSSAddressNew.StreetName;
                    int StreetTypeNew = CurrentPSS.PSSAddressNew.StreetType == null ? 0 : (int)CurrentPSS.PSSAddressNew.StreetType;
                    string MunicipalityNewText = CurrentPSS.PSSAddressNew.Municipality == null ? "" : CurrentPSS.PSSAddressNew.Municipality;
                    string PostalCodeNewText = CurrentPSS.PSSAddressNew.PostalCode == null ? "" : CurrentPSS.PSSAddressNew.PostalCode;

                    if (!string.IsNullOrWhiteSpace(MunicipalityText))
                    {
                        MunicipalityText = MunicipalityText.Trim();
                    }

                    if (!string.IsNullOrWhiteSpace(MunicipalityNewText))
                    {
                        MunicipalityNewText = MunicipalityNewText.Trim();
                    }

                    if (string.IsNullOrWhiteSpace(MunicipalityNewText))
                    {
                        string NoMunicipality = $"ERROR: To add an address you need to have a municipality name\r\n";
                        EmitRTBMessage(new RTBMessageEventArgs(NoMunicipality));
                        return;
                    }

                    string MessageText = $"Checking if municipality exist [{MunicipalityNewText}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                    bool MunicipalityExist = false;
                    ret = MunicipalityExistUnderProvinceInCSSPWebTools((int)subsectorDoc.ProvinceTVItemID, MunicipalityNewText, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        MunicipalityExist = false;
                        NeedToSave = true;
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));
                        MunicipalityExist = true;
                    }

                    if (!MunicipalityExist)
                    {
                        MessageText = $"Trying to create address and municipality --- old [{StreetNumberText} {StreetNameText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetType)} {MunicipalityText} {PostalCodeText}] -- - new [{StreetNumberNewText} {StreetNameNewText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetTypeNew)} {MunicipalityNewText} {PostalCodeNewText}]\r\n";
                        EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                        ret = SaveToCSSPWebToolsAddress((int)subsectorDoc.ProvinceTVItemID, (int)CurrentPSS.PSSTVItemID, StreetNumberNewText, StreetNameNewText, StreetTypeNew, MunicipalityNewText, PostalCodeNewText, true, true, false, AdminEmail);
                        ret = ret.Replace("\"", "");
                        if (ret.StartsWith("ERROR"))
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                            EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                            if (NeedToSave)
                            {
                                SaveSubsectorTextFile();
                            }
                        }
                        else
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                            CurrentPSS.PSSAddress.AddressType = (int)AddressTypeEnum.Civic;
                            CurrentPSS.PSSAddress.AddressTVItemID = int.Parse(ret);
                            CurrentPSS.PSSAddress.StreetNumber = CurrentPSS.PSSAddressNew.StreetNumber;
                            CurrentPSS.PSSAddress.StreetName = CurrentPSS.PSSAddressNew.StreetName;
                            CurrentPSS.PSSAddress.StreetType = CurrentPSS.PSSAddressNew.StreetType;
                            CurrentPSS.PSSAddress.Municipality = CurrentPSS.PSSAddressNew.Municipality;
                            CurrentPSS.PSSAddress.PostalCode = CurrentPSS.PSSAddressNew.PostalCode;
                            CurrentPSS.PSSAddressNew.StreetNumber = null;
                            CurrentPSS.PSSAddressNew.StreetName = null;
                            CurrentPSS.PSSAddressNew.StreetType = null;
                            CurrentPSS.PSSAddressNew.Municipality = null;
                            CurrentPSS.PSSAddressNew.PostalCode = null;
                            CurrentPSS.PSSAddressNew.AddressTVItemID = null;
                            CurrentPSS.PSSAddressNew.AddressType = null;
                            NeedToSave = true;
                        }
                    }
                    else
                    {
                        MessageText = $"Trying to create address --- old [{StreetNumberText} {StreetNameText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetType)} {MunicipalityText} {PostalCodeText}] -- - new [{StreetNumberNewText} {StreetNameNewText} {_BaseEnumService.GetEnumText_StreetTypeEnum((StreetTypeEnum)StreetTypeNew)} {MunicipalityNewText} {PostalCodeNewText}]\r\n";
                        EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                        ret = SaveToCSSPWebToolsAddress((int)subsectorDoc.ProvinceTVItemID, (int)CurrentPSS.PSSTVItemID, StreetNumberNewText, StreetNameNewText, StreetTypeNew, MunicipalityNewText, PostalCodeNewText, false, true, false, AdminEmail);
                        ret = ret.Replace("\"", "");
                        if (ret.StartsWith("ERROR"))
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                            EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                            if (NeedToSave)
                            {
                                SaveSubsectorTextFile();
                            }
                        }
                        else
                        {
                            EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                            CurrentPSS.PSSAddress.AddressType = (int)AddressTypeEnum.Civic;
                            CurrentPSS.PSSAddress.AddressTVItemID = int.Parse(ret);
                            CurrentPSS.PSSAddress.StreetNumber = CurrentPSS.PSSAddressNew.StreetNumber;
                            CurrentPSS.PSSAddress.StreetName = CurrentPSS.PSSAddressNew.StreetName;
                            CurrentPSS.PSSAddress.StreetType = CurrentPSS.PSSAddressNew.StreetType;
                            CurrentPSS.PSSAddress.Municipality = CurrentPSS.PSSAddressNew.Municipality;
                            CurrentPSS.PSSAddress.PostalCode = CurrentPSS.PSSAddressNew.PostalCode;
                            CurrentPSS.PSSAddressNew.StreetNumber = null;
                            CurrentPSS.PSSAddressNew.StreetName = null;
                            CurrentPSS.PSSAddressNew.StreetType = null;
                            CurrentPSS.PSSAddressNew.Municipality = null;
                            CurrentPSS.PSSAddressNew.PostalCode = null;
                            CurrentPSS.PSSAddressNew.AddressTVItemID = null;
                            CurrentPSS.PSSAddressNew.AddressType = null;
                            NeedToSave = true;
                        }
                    }
                }
            }

            if (CurrentPSS.PSSObs.ObsDateNew != null || CurrentPSS.PSSObs.ObsID >= 10000000 || IsNewPSS)
            {
                if (CurrentPSS.PSSObs.ObsDateNew == null)
                {
                    CurrentPSS.PSSObs.ObsDateNew = CurrentPSS.PSSObs.ObsDate;
                }

                string MessageText = $"Trying to create Obs Date --- [{((DateTime)CurrentPSS.PSSObs.ObsDateNew).ToString("yyyy MMMM dd")}]\r\n";
                EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                DateTime ObsDate = CurrentPSS.PSSObs.ObsDateNew != null ? (DateTime)CurrentPSS.PSSObs.ObsDateNew : (DateTime)CurrentPSS.PSSObs.ObsDate;

                ret = SaveToCSSPWebToolsCreateNewObsDate((int)CurrentPSS.PSSTVItemID, ObsDate, AdminEmail);
                ret = ret.Replace("\"", "");
                if (ret.StartsWith("ERROR"))
                {
                    EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                    EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                    if (NeedToSave)
                    {
                        SaveSubsectorTextFile();
                    }
                }
                else
                {
                    EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                    CurrentPSS.PSSObs.ObsID = int.Parse(ret);
                    CurrentPSS.PSSObs.ObsDate = CurrentPSS.PSSObs.ObsDateNew;
                    CurrentPSS.PSSObs.ObsDateNew = null;
                    foreach (Issue issue in CurrentPSS.PSSObs.IssueList)
                    {
                        if (issue.PolSourceObsInfoIntListNew == null || issue.PolSourceObsInfoIntListNew.Count == 0)
                        {
                            issue.PolSourceObsInfoIntListNew = issue.PolSourceObsInfoIntList;
                        }
                    }
                    NeedToSave = true;
                }
            }

            foreach (Issue issue in CurrentPSS.PSSObs.IssueList.Where(c => c.ToRemove == true).OrderBy(c => c.Ordinal))
            {
                if (issue.IssueID >= 10000000)
                {
                    CurrentPSS.PSSObs.IssueList.Remove(issue);
                    NeedToSave = true;
                }
                else
                {
                    string MessageText = $"Removing issue # --- [{issue.Ordinal}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                    ret = RemoveFromCSSPWebToolsIssue((int)CurrentPSS.PSSObs.ObsID, (int)issue.IssueID, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR:"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                        if (NeedToSave)
                        {
                            SaveSubsectorTextFile();
                        }
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                        CurrentPSS.PSSObs.IssueList.Remove(issue);
                        NeedToSave = true;
                    }
                }

                if (CurrentPSS.PSSObs.IssueList.Count == 0)
                {
                    Issue issue2 = new Issue();
                    issue2.IssueID = 10000000;
                    issue2.Ordinal = 0;
                    issue2.LastUpdated_UTC = DateTime.Now;
                    issue2.PolSourceObsInfoIntList = new List<int>() { (int)PolSourceObsInfoEnum.SourceHumanLand };
                    issue2.PolSourceObsInfoIntListNew = new List<int>() { (int)PolSourceObsInfoEnum.SourceHumanLand };

                    CurrentPSS.PSSObs.IssueList.Add(issue2);
                    NeedToSave = true;
                }
            }

            foreach (Issue issue in CurrentPSS.PSSObs.IssueList.OrderBy(c => c.Ordinal))
            {
                if (issue.PolSourceObsInfoIntListNew.Count > 0 || IsNewPSS)
                {
                    string MessageText = $"Changing or adding issue # --- [{issue.Ordinal}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                    ret = SaveToCSSPWebToolsIssue((int)CurrentPSS.PSSObs.ObsID, (int)issue.IssueID, (int)issue.Ordinal, String.Join(",", issue.PolSourceObsInfoIntListNew) + ",", AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR:"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                        if (NeedToSave)
                        {
                            SaveSubsectorTextFile();
                        }
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                        issue.IssueID = int.Parse(ret);
                        issue.PolSourceObsInfoIntList = issue.PolSourceObsInfoIntListNew;
                        issue.PolSourceObsInfoIntListNew = new List<int>();
                        NeedToSave = true;
                    }
                }
            }

            // need to do ExtraComment
            foreach (Issue issue in CurrentPSS.PSSObs.IssueList.OrderBy(c => c.Ordinal))
            {
                if (issue.PolSourceObsInfoIntListNew.Count > 0 || IsNewPSS)
                {
                    string MessageText = $"Changing Extra Comment of issue # --- [{issue.Ordinal}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                    string ExtraComment = (string.IsNullOrWhiteSpace(issue.ExtraCommentNew) ? (string.IsNullOrWhiteSpace(issue.ExtraComment) ? "" : issue.ExtraComment) : "");

                    ret = SaveToCSSPWebToolsIssueExtraComment((int)CurrentPSS.PSSObs.ObsID, (int)issue.IssueID, ExtraComment, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR:"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                        if (NeedToSave)
                        {
                            SaveSubsectorTextFile();
                        }
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                        issue.ExtraComment = issue.ExtraCommentNew;
                        issue.ExtraCommentNew = null;
                        NeedToSave = true;
                    }
                }
            }

            List<int> PictureTVItemIDListToRemove = new List<int>();

            foreach (Picture picture in CurrentPSS.PSSPictureList)
            {
                FileInfo fiPictureToDelete = new FileInfo($@"C:\PollutionSourceSites\Subsectors\{CurrentSubsectorName}\Pictures\{CurrentPSS.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}");

                if (picture.ToRemove == true)
                {
                    if (picture.PictureTVItemID >= 10000000)
                    {
                        if (fiPictureToDelete.Exists)
                        {
                            try
                            {
                                fiPictureToDelete.Delete();
                            }
                            catch (Exception ex)
                            {
                                string ErrMessage = ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
                                EmitRTBMessage(new RTBMessageEventArgs($"{ErrMessage}"));
                            }
                        }

                        continue;
                    }

                    string MessageText = $"Removing picture --- [{picture.FileNameNew}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                    ret = SaveToCSSPWebToolsPictureToRemove((int)CurrentPSS.PSSTVItemID, (int)picture.PictureTVItemID, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR:"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                        if (NeedToSave)
                        {
                            SaveSubsectorTextFile();
                        }
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                        PictureTVItemIDListToRemove.Add((int)picture.PictureTVItemID);

                        if (fiPictureToDelete.Exists)
                        {
                            try
                            {
                                fiPictureToDelete.Delete();
                            }
                            catch (Exception ex)
                            {
                                EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                                string ErrMessage = ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
                                EmitRTBMessage(new RTBMessageEventArgs($"{ErrMessage}"));
                            }
                        }

                        NeedToSave = true;
                    }

                }
            }

            foreach (int pictureTVItemIDToDelete in PictureTVItemIDListToRemove)
            {
                Picture picture = CurrentPSS.PSSPictureList.Where(c => c.PictureTVItemID == pictureTVItemIDToDelete).FirstOrDefault();

                if (picture != null)
                {
                    CurrentPSS.PSSPictureList.Remove(picture);
                }
            }

            foreach (Picture picture in CurrentPSS.PSSPictureList)
            {
                bool IsNew = false;
                if (picture.PictureTVItemID >= 10000000)
                {
                    IsNew = true;

                    string MessageText = $"Adding picture --- [{picture.FileNameNew}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));

                    FileInfo fiPicture = new FileInfo($@"C:\PollutionSourceSites\Subsectors\{CurrentSubsectorName}\Pictures\{CurrentPSS.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}");

                    if (!fiPicture.Exists)
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        continue;
                    }

                    ret = SaveToCSSPWebToolsPicture(fiPicture, (int)CurrentPSS.PSSTVItemID, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR:"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                        if (NeedToSave)
                        {
                            SaveSubsectorTextFile();
                        }
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                        picture.PictureTVItemID = int.Parse(ret);

                        FileInfo fiPictureNew = new FileInfo($@"C:\PollutionSourceSites\Subsectors\{CurrentSubsectorName}\Pictures\{CurrentPSS.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}");

                        if (fiPicture.Exists)
                        {
                            try
                            {
                                File.Copy(fiPicture.FullName, fiPictureNew.FullName);
                                fiPicture.Delete();
                            }
                            catch (Exception ex)
                            {
                                EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                                string ErrMessage = ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
                                EmitRTBMessage(new RTBMessageEventArgs($"{ErrMessage}"));
                            }
                        }

                        NeedToSave = true;
                    }
                }

                if (picture.FileNameNew != null
                    || picture.DescriptionNew != null
                    || picture.ExtensionNew != null
                    || picture.FromWaterNew != null
                    || IsNew)
                {
                    string FileNameText = picture.FileNameNew != null ? picture.FileNameNew : picture.FileName;
                    string DescriptionText = picture.DescriptionNew != null ? picture.DescriptionNew : picture.Description;
                    string ExtensionText = picture.ExtensionNew != null ? picture.ExtensionNew : picture.Extension;
                    bool FromWater = picture.FromWaterNew != null ? (bool)picture.FromWaterNew : (picture.FromWater != null ? (bool)picture.FromWater : false);

                    string MessageText = $"Changing properties of picture --- [{picture.FileNameNew}]\r\n";
                    EmitRTBMessage(new RTBMessageEventArgs(MessageText));


                    ret = SaveToCSSPWebToolsPictureInfo((int)CurrentPSS.PSSTVItemID, (int)picture.PictureTVItemID, FileNameText, DescriptionText, ExtensionText, FromWater, AdminEmail);
                    ret = ret.Replace("\"", "");
                    if (ret.StartsWith("ERROR:"))
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"ERROR: {MessageText}"));
                        EmitRTBMessage(new RTBMessageEventArgs($"{ret}\r\n"));
                        if (NeedToSave)
                        {
                            SaveSubsectorTextFile();
                        }
                    }
                    else
                    {
                        EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: {MessageText}"));

                        picture.PictureTVItemID = int.Parse(ret);
                        picture.FileName = FileNameText;
                        picture.Description = DescriptionText;
                        picture.Extension = ExtensionText;
                        picture.FromWater = FromWater;
                        picture.FileNameNew = null;
                        picture.DescriptionNew = null;
                        picture.ExtensionNew = null;
                        picture.FromWaterNew = null;
                        NeedToSave = true;
                    }
                }
            }

            if (NeedToSave)
            {
                SaveSubsectorTextFile();
                DrawPanelPSS();
                //RedrawSinglePanelPSS();
                ReDrawPolSourceSite();
            }

            EmitRTBMessage(new RTBMessageEventArgs($"SUCCESS: completed"));

        }
        public void PSSSaveAllToCSSPWebTools()
        {
            foreach (PSS pss in subsectorDoc.Subsector.PSSList)
            {
                CurrentPSS = pss;
                PSSSaveToCSSPWebTools();
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
                lblTVText.Location = new Point(5, 4);
                lblTVText.TabIndex = 0;
                lblTVText.Tag = pss.PSSTVItemID;

                bool IsActive = false;
                if (pss.IsActiveNew != null)
                {
                    IsActive = (bool)pss.IsActiveNew;
                }
                else
                {
                    IsActive = (bool)pss.IsActive;
                }

                if (IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }
                if (!string.IsNullOrWhiteSpace(pss.TVTextNew))
                {
                    lblTVText.Text = $"{pss.TVTextNew}";
                }
                else
                {
                    lblTVText.Text = $"{pss.TVText}";
                }
                lblTVText.Click += ShowPolSourceSite_Click;

                panel.Controls.Add(lblTVText);

                Label lblPSSStatus = new Label();

                bool NeedDetailsUpdate = false;
                bool NeedIssuesUpdate = false;
                bool NeedPicturesUpdate = false;
                bool NeedActiveUpdate = false;
                bool NeedPointSourceUpdate = false;
                if (IsAdmin)
                {
                    if (pss.IsActiveNew != null && pss.IsActiveNew != pss.IsActive)
                    {
                        NeedActiveUpdate = true;
                    }

                    if (pss.IsPointSourceNew != null && pss.IsPointSourceNew != pss.IsPointSource)
                    {
                        NeedPointSourceUpdate = false;
                    }

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
                            || picture.ToRemove != null
                            || picture.FromWaterNew != null
                            || picture.PictureTVItemID >= 10000000)
                        {
                            NeedPicturesUpdate = true;
                            break;
                        }
                    }

                    lblPSSStatus.AutoSize = true;
                    lblPSSStatus.Location = new Point(5, lblTVText.Bottom + 4);
                    lblPSSStatus.TabIndex = 0;
                    lblPSSStatus.Tag = pss.PSSTVItemID;

                    bool IsActive2 = false;
                    if (pss.IsActiveNew != null)
                    {
                        IsActive2 = (bool)pss.IsActiveNew;
                    }
                    else
                    {
                        IsActive2 = (bool)pss.IsActive;
                    }

                    if (IsActive2 == false)
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
                    string NeedActiveUpdateText = NeedActiveUpdate ? "Active" : "";
                    string NeedPointSourceUpdateText = NeedPointSourceUpdate ? "Point Source" : "";
                    if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate || NeedActiveUpdate || NeedPointSourceUpdate)
                    {
                        lblPSSStatus.Text = $"Good --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText} {NeedActiveUpdateText} {NeedPointSourceUpdateText}";
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
                    lblPSSStatus.Location = new Point(5, lblTVText.Bottom + 4);
                    lblPSSStatus.TabIndex = 0;
                    lblPSSStatus.Tag = pss.PSSTVItemID;

                    bool IsActive2 = false;
                    if (pss.IsActiveNew != null)
                    {
                        IsActive2 = (bool)pss.IsActiveNew;
                    }
                    else
                    {
                        IsActive2 = (bool)pss.IsActive;
                    }

                    if (IsActive2 == false)
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
                            string NeedActiveUpdateText = NeedActiveUpdate ? "Active" : "";
                            string NeedPointSourceUpdateText = NeedPointSourceUpdate ? "Point Source" : "";
                            if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate || NeedActiveUpdate || NeedPointSourceUpdate)
                            {
                                lblPSSStatus.Text = $"Not Well Formed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText} {NeedActiveUpdateText} {NeedPointSourceUpdateText}";
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
                            string NeedActiveUpdateText = NeedActiveUpdate ? "Active" : "";
                            string NeedPointSourceUpdateText = NeedPointSourceUpdate ? "Point Source" : "";
                            if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate || NeedActiveUpdate || NeedPointSourceUpdate)
                            {
                                lblPSSStatus.Text = $"Not Completed --- Needs update for {NeedDetailsUpdateText} {NeedIssuesUpdateText} {NeedPictuesUpdateText} {NeedActiveUpdateText} {NeedPointSourceUpdateText}";
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
        public void RedrawContactAndInfrastructureList()
        {
            IsReading = true;
            if (!ReadMunicipalityFile())
            {
                return;
            }
            IsReading = false;
            if (!CheckAllReadDataMunicipalityOK())
            {
                return;
            }

            DrawPanelContactsAndInfrastructures();
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
        public string RemoveFromCSSPWebToolsIssue(int ObsID, int IssueID, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("ObsID", ObsID.ToString());
                paramList.Add("IssueID", IssueID.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}RemoveIssueJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}RemoveIssueJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }
        }
        private void ShowContact()
        {
            PanelViewAndEdit.Controls.Clear();

            if (CurrentContact == null)
            {
                Label lblMessage = new Label();
                lblMessage.AutoSize = true;
                lblMessage.Location = new Point(30, 30);
                lblMessage.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblMessage.Font = new Font(new FontFamily(lblMessage.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblMessage.Text = $"Please select a contact items for {(IsEditing ? "editing" : "viewing")} {(ShowOnlyIssues ? "issues" : (ShowOnlyPictures ? "pictures" : "pollution source site"))}";

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
            if (CurrentContact != null)
            {
                #region Title and Active button
                Label lblTVText = new Label();
                lblTVText.AutoSize = true;
                lblTVText.Location = new Point(10, 4);
                lblTVText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);

                bool IsActive = false;
                if (CurrentContact.IsActiveNew != null)
                {
                    IsActive = (bool)CurrentContact.IsActiveNew;
                }
                else
                {
                    if (CurrentContact.IsActive == null)
                    {
                        IsActive = true;
                    }
                    else
                    {
                        IsActive = (bool)CurrentContact.IsActive;
                    }
                }

                if (IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }

                string FirstName = string.IsNullOrEmpty(CurrentContact.FirstNameNew) == false ? CurrentContact.FirstNameNew : CurrentContact.FirstName;
                string Inital = string.IsNullOrEmpty(CurrentContact.InitialNew) == false ? CurrentContact.InitialNew : CurrentContact.Initial;
                if (!string.IsNullOrWhiteSpace(Inital))
                {
                    Inital = Inital + ", ";
                }
                string LastName = string.IsNullOrEmpty(CurrentContact.LastNameNew) == false ? CurrentContact.LastNameNew : CurrentContact.LastName;
                string FullName = $"{FirstName} {Inital} {LastName}";
                lblTVText.Text = $"{FullName}";

                PanelViewAndEdit.Controls.Add(lblTVText);

                if (IsEditing)
                {
                    bool IsActive2 = false;
                    if (CurrentContact.IsActiveNew != null)
                    {
                        IsActive2 = (bool)CurrentContact.IsActiveNew;
                    }
                    else
                    {
                        if (CurrentContact.IsActive == null)
                        {
                            IsActive2 = true;
                        }
                        else
                        {
                            IsActive2 = (bool)CurrentContact.IsActive;
                        }
                    }
                    if (IsActive2)
                    {
                        Button butChangeToIsNotActive = new Button();
                        butChangeToIsNotActive.AutoSize = true;
                        butChangeToIsNotActive.Location = new Point(lblTVText.Right + 10, lblTVText.Top);
                        butChangeToIsNotActive.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsNotActive.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsNotActive.Padding = new Padding(5);
                        butChangeToIsNotActive.Tag = CurrentContact.ContactTVItemID.ToString();
                        butChangeToIsNotActive.Text = $"Set as non active";
                        butChangeToIsNotActive.Click += butChangeToIsNotActive_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsNotActive);

                        if (CurrentContact.ContactTVItemID >= 20000000)
                        {
                            Button butDeleteContact = new Button();
                            butDeleteContact.AutoSize = true;
                            butDeleteContact.Location = new Point(butChangeToIsNotActive.Right + 10, butChangeToIsNotActive.Top);
                            butDeleteContact.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                            butDeleteContact.Font = new Font(new FontFamily(butDeleteContact.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                            butDeleteContact.Padding = new Padding(5);
                            butDeleteContact.Tag = CurrentContact.ContactTVItemID.ToString();
                            butDeleteContact.Text = $"Delete Contact";
                            butDeleteContact.Click += butDeleteContact_Click;

                            PanelViewAndEdit.Controls.Add(butDeleteContact);
                        }
                    }
                    else
                    {
                        Button butChangeToIsActive = new Button();
                        butChangeToIsActive.AutoSize = true;
                        butChangeToIsActive.Location = new Point(lblTVText.Right + 10, lblTVText.Top);
                        butChangeToIsActive.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butChangeToIsActive.Font = new Font(new FontFamily(butChangeToIsActive.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butChangeToIsActive.Padding = new Padding(5);
                        butChangeToIsActive.Tag = CurrentContact.ContactTVItemID.ToString();
                        butChangeToIsActive.Text = $"Set as active";
                        butChangeToIsActive.Click += butChangeToIsActive_Click;

                        PanelViewAndEdit.Controls.Add(butChangeToIsActive);

                        if (CurrentContact.ContactTVItemID >= 20000000)
                        {
                            Button butDeleteContact = new Button();
                            butDeleteContact.AutoSize = true;
                            butDeleteContact.Location = new Point(butChangeToIsActive.Right + 10, butChangeToIsActive.Top);
                            butDeleteContact.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                            butDeleteContact.Font = new Font(new FontFamily(butDeleteContact.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                            butDeleteContact.Padding = new Padding(5);
                            butDeleteContact.Tag = CurrentContact.ContactTVItemID.ToString();
                            butDeleteContact.Text = $"Delete Contact";
                            butDeleteContact.Click += butDeleteContact_Click;

                            PanelViewAndEdit.Controls.Add(butDeleteContact);
                        }
                    }
                }
                else
                {
                    Label lblIsActive = new Label();
                    lblIsActive.AutoSize = true;
                    lblIsActive.Location = new Point(lblTVText.Right + 10, lblTVText.Top);
                    lblIsActive.Font = new Font(new FontFamily(lblIsActive.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblIsActive.ForeColor = CurrentContact.IsActiveNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblIsActive.Text = (CurrentContact.IsActiveNew != null ? ((bool)CurrentContact.IsActiveNew ? "Is Active" : "Not Active") : ((bool)CurrentContact.IsActive ? "Is Active" : "Not Active"));

                    PanelViewAndEdit.Controls.Add(lblIsActive);
                }

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                #endregion Title and Active button

                #region FirstName
                X = 10;
                DrawItemText(X, Y, CurrentContact.FirstName, CurrentContact.FirstNameNew, "First Name", "textBoxFirstName", 300, 0);
                #endregion FirstName

                if (IsEditing)
                {
                    int right = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    Label lblRequired = new Label();
                    lblRequired.AutoSize = true;
                    lblRequired.Location = new Point(right, Y);
                    lblRequired.Font = new Font(new FontFamily(lblRequired.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblRequired.ForeColor = CurrentContact.IsActiveNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblRequired.Text = "(required)";

                    PanelViewAndEdit.Controls.Add(lblRequired);
                }

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                #region Initial
                X = 10;
                DrawItemText(X, Y, CurrentContact.Initial, CurrentContact.InitialNew, "Initial", "textBoxInitial", 300, 0);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion Initial

                #region LastName
                X = 10;
                DrawItemText(X, Y, CurrentContact.LastName, CurrentContact.LastNameNew, "Last Name", "textBoxLastName", 300, 0);
                #endregion LastName

                if (IsEditing)
                {
                    int right2 = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    Label lblRequired2 = new Label();
                    lblRequired2.AutoSize = true;
                    lblRequired2.Location = new Point(right2, Y);
                    lblRequired2.Font = new Font(new FontFamily(lblRequired2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblRequired2.ForeColor = CurrentContact.IsActiveNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblRequired2.Text = "(required)";

                    PanelViewAndEdit.Controls.Add(lblRequired2);
                }

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                #region Email
                X = 10;
                DrawItemText(X, Y, CurrentContact.Email, CurrentContact.EmailNew, "Email", "textBoxEmail", 300, 0);
                #endregion Email

                if (IsEditing)
                {
                    int right3 = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    Label lblRequired3 = new Label();
                    lblRequired3.AutoSize = true;
                    lblRequired3.Location = new Point(right3, Y);
                    lblRequired3.Font = new Font(new FontFamily(lblRequired3.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblRequired3.ForeColor = CurrentContact.IsActiveNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                    lblRequired3.Text = "(required)";

                    PanelViewAndEdit.Controls.Add(lblRequired3);
                }

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                #region ContactTitle
                DrawItemEnum(X, Y, CurrentContact.ContactTitle, CurrentContact.ContactTitleNew, "Contact Title", "comboBoxContactTitle", typeof(ContactTitleEnum), 0);
                #endregion ContactTitle

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                #region Save button
                if (IsEditing)
                {
                    Button butSaveContactAll = new Button();
                    butSaveContactAll.AutoSize = true;
                    butSaveContactAll.Location = new Point(200, Y);
                    butSaveContactAll.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butSaveContactAll.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butSaveContactAll.Padding = new Padding(5);
                    butSaveContactAll.Text = $"Save";
                    butSaveContactAll.Click += butSaves_Click;

                    PanelViewAndEdit.Controls.Add(butSaveContactAll);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;

                    Button butCancelContactAll = new Button();
                    butCancelContactAll.AutoSize = true;
                    butCancelContactAll.Location = new Point(X, Y);
                    butCancelContactAll.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butCancelContactAll.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butCancelContactAll.Padding = new Padding(5);
                    butCancelContactAll.Text = $"Cancel";
                    butCancelContactAll.Click += butCancel_Click;

                    PanelViewAndEdit.Controls.Add(butCancelContactAll);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                }
                #endregion Save button

                Label lblTel = new Label();
                lblTel.AutoSize = true;
                lblTel.Location = new Point(10, Y);
                lblTel.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblTel.Font = new Font(new FontFamily(lblTel.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblTel.ForeColor = Color.Black;
                lblTel.Text = $"Telephones";

                PanelViewAndEdit.Controls.Add(lblTel);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                X = 40;

                int CountTel = 0;
                foreach (Telephone tel in CurrentContact.TelephoneList)
                {
                    #region TelType
                    DrawItemEnum(X, Y, tel.TelType, tel.TelTypeNew, "Telephone Type", "comboBoxTelType", typeof(TelTypeEnum), CountTel);
                    #endregion TelType

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                    #region TelNumber
                    DrawItemText(X, Y, tel.TelNumber, tel.TelNumberNew, "Telephone Number", "textBoxTelNumber", 300, CountTel);
                    #endregion TelNumber

                    if (IsEditing)
                    {
                        int right4 = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                        Label lblRequired4 = new Label();
                        lblRequired4.AutoSize = true;
                        lblRequired4.Location = new Point(right4, Y);
                        lblRequired4.Font = new Font(new FontFamily(lblRequired4.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblRequired4.ForeColor = CurrentContact.IsActiveNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblRequired4.Text = "(required)";

                        PanelViewAndEdit.Controls.Add(lblRequired4);
                    }

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                    Label lblTelSeparator = new Label();
                    lblTelSeparator.AutoSize = true;
                    lblTelSeparator.Location = new Point(10, Y);
                    lblTelSeparator.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblTelSeparator.Font = new Font(new FontFamily(lblTelSeparator.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                    lblTelSeparator.ForeColor = Color.Black;
                    lblTelSeparator.Text = $"          - - - - - - - - - - - - - - - - - - - - -          ";

                    PanelViewAndEdit.Controls.Add(lblTelSeparator);

                    if (IsEditing && tel.TelTVItemID >= 30000000)
                    {
                        Button butDeleteTel = new Button();
                        butDeleteTel.AutoSize = true;
                        butDeleteTel.Location = new Point(lblTelSeparator.Right + 10, lblTelSeparator.Top);
                        butDeleteTel.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butDeleteTel.Font = new Font(new FontFamily(butDeleteTel.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butDeleteTel.Padding = new Padding(5);
                        butDeleteTel.Tag = tel.TelTVItemID.ToString();
                        butDeleteTel.Text = $"Delete Telephone";
                        butDeleteTel.Click += butDeleteTel_Click;

                        PanelViewAndEdit.Controls.Add(butDeleteTel);
                    }


                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                    CountTel++;
                }

                if (IsEditing)
                {
                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                    X = 100;

                    Button butTelAdd = new Button();
                    butTelAdd.AutoSize = true;
                    butTelAdd.Location = new Point(20, Y);
                    butTelAdd.Text = $"Add Another Telephone for {FullName}";
                    butTelAdd.Font = new Font(new FontFamily(butTelAdd.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                    butTelAdd.Padding = new Padding(5);
                    butTelAdd.Click += butTelAdd_Click;

                    PanelViewAndEdit.Controls.Add(butTelAdd);
                }

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                X = 10;

                Label lblEmail = new Label();
                lblEmail.AutoSize = true;
                lblEmail.Location = new Point(10, Y);
                lblEmail.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblEmail.Font = new Font(new FontFamily(lblEmail.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblEmail.ForeColor = Color.Black;
                lblEmail.Text = $"Other Emails";

                PanelViewAndEdit.Controls.Add(lblEmail);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                X = 40;

                int CountEmail = 0;
                foreach (Email email in CurrentContact.EmailList)
                {
                    #region EmailType
                    DrawItemEnum(X, Y, email.EmailType, email.EmailTypeNew, "Email Type", "comboBoxEmailType", typeof(EmailTypeEnum), CountEmail);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion EmailType

                    #region EmailNumber
                    DrawItemText(X, Y, email.EmailAddress, email.EmailAddressNew, "Email Address", "textBoxEmailAddress", 300, CountEmail);
                    #endregion EmailNumber

                    if (IsEditing)
                    {
                        int right5 = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                        Label lblRequired5 = new Label();
                        lblRequired5.AutoSize = true;
                        lblRequired5.Location = new Point(right5, Y);
                        lblRequired5.Font = new Font(new FontFamily(lblRequired5.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblRequired5.ForeColor = CurrentContact.IsActiveNew != null ? ForeColorChangedOrNew : ForeColorNormal;
                        lblRequired5.Text = "(required)";

                        PanelViewAndEdit.Controls.Add(lblRequired5);
                    }

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                    Label lblEmailSeparator = new Label();
                    lblEmailSeparator.AutoSize = true;
                    lblEmailSeparator.Location = new Point(10, Y);
                    lblEmailSeparator.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblEmailSeparator.Font = new Font(new FontFamily(lblEmailSeparator.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                    lblEmailSeparator.ForeColor = Color.Black;
                    lblEmailSeparator.Text = $"          - - - - - - - - - - - - - - - - - - - - -          ";

                    PanelViewAndEdit.Controls.Add(lblEmailSeparator);

                    if (IsEditing && email.EmailTVItemID >= 40000000)
                    {
                        Button butDeleteEmail = new Button();
                        butDeleteEmail.AutoSize = true;
                        butDeleteEmail.Location = new Point(lblEmailSeparator.Right + 10, lblEmailSeparator.Top);
                        butDeleteEmail.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butDeleteEmail.Font = new Font(new FontFamily(butDeleteEmail.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butDeleteEmail.Padding = new Padding(5);
                        butDeleteEmail.Tag = email.EmailTVItemID.ToString();
                        butDeleteEmail.Text = $"Delete Email";
                        butDeleteEmail.Click += butDeleteEmail_Click;

                        PanelViewAndEdit.Controls.Add(butDeleteEmail);
                    }

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                    CountEmail++;
                }

                if (IsEditing)
                {
                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                    X = 100;

                    Button butEmailAdd = new Button();
                    butEmailAdd.AutoSize = true;
                    butEmailAdd.Location = new Point(20, Y);
                    butEmailAdd.Text = $"Add Another Email for {FullName}";
                    butEmailAdd.Font = new Font(new FontFamily(butEmailAdd.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                    butEmailAdd.Padding = new Padding(5);
                    butEmailAdd.Click += butEmailAdd_Click;

                    PanelViewAndEdit.Controls.Add(butEmailAdd);
                }

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                X = 10;
                Label lblAddress = new Label();
                lblAddress.AutoSize = true;
                lblAddress.Location = new Point(10, Y);
                lblAddress.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblAddress.Font = new Font(new FontFamily(lblTel.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblAddress.ForeColor = Color.Black;
                lblAddress.Text = $"Address";

                PanelViewAndEdit.Controls.Add(lblAddress);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                X = 40;

                #region Address
                DrawItemAddress(X, Y, CurrentContact.ContactAddress, CurrentContact.ContactAddressNew, true);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                #endregion Address

                #region Save button
                if (IsEditing)
                {
                    Button butSaveContactAll = new Button();
                    butSaveContactAll.AutoSize = true;
                    butSaveContactAll.Location = new Point(200, Y);
                    butSaveContactAll.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butSaveContactAll.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butSaveContactAll.Padding = new Padding(5);
                    butSaveContactAll.Text = $"Save";
                    butSaveContactAll.Click += butSaves_Click;

                    PanelViewAndEdit.Controls.Add(butSaveContactAll);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;

                    Button butCancelContactAll = new Button();
                    butCancelContactAll.AutoSize = true;
                    butCancelContactAll.Location = new Point(X, Y);
                    butCancelContactAll.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butCancelContactAll.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butCancelContactAll.Padding = new Padding(5);
                    butCancelContactAll.Text = $"Cancel";
                    butCancelContactAll.Click += butCancel_Click;

                    PanelViewAndEdit.Controls.Add(butCancelContactAll);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                }
                #endregion Save button

                if (IsAdmin)
                {
                    bool NeedsUpdate = false;

                    if (CurrentContact.IsActiveNew != null && CurrentContact.IsActiveNew != CurrentContact.IsActive)
                    {
                        NeedsUpdate = true;
                    }

                    if (!NeedsUpdate)
                    {
                        if (CurrentContact.FirstNameNew != null
                            || CurrentContact.InitialNew != null
                            || CurrentContact.LastNameNew != null
                            || CurrentContact.EmailNew != null)
                        {
                            NeedsUpdate = true;
                        }
                    }

                    if (!NeedsUpdate)
                    {
                        if (CurrentContact.IsActiveNew != null
                            || CurrentContact.ContactAddressNew.AddressTVItemID != null
                            || CurrentContact.ContactAddressNew.AddressType != null
                            || CurrentContact.ContactAddressNew.Municipality != null
                            || CurrentContact.ContactAddressNew.PostalCode != null
                            || CurrentContact.ContactAddressNew.StreetName != null
                            || CurrentContact.ContactAddressNew.StreetNumber != null
                            || CurrentContact.ContactAddressNew.StreetType != null)
                        {
                            NeedsUpdate = true;
                        }
                    }

                    if (!NeedsUpdate)
                    {
                        foreach (Telephone tel in CurrentContact.TelephoneList)
                        {
                            if (tel.TelTypeNew != null
                                || tel.TelNumberNew != null)
                            {
                                NeedsUpdate = true;
                                break;
                            }
                        }
                    }

                    if (!NeedsUpdate)
                    {
                        foreach (Email email in CurrentContact.EmailList)
                        {
                            if (email.EmailTypeNew != null
                                || email.EmailAddressNew != null)
                            {
                                NeedsUpdate = true;
                                break;
                            }
                        }
                    }

                    if (NeedsUpdate)
                    {
                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                        Button butContactAndInfrastructureSaveToCSSPWebTools = new Button();
                        butContactAndInfrastructureSaveToCSSPWebTools.AutoSize = true;
                        butContactAndInfrastructureSaveToCSSPWebTools.Location = new Point(20, Y);
                        butContactAndInfrastructureSaveToCSSPWebTools.Text = "Update All Contact and Infrastructure Related Information To CSSPWebTools";
                        butContactAndInfrastructureSaveToCSSPWebTools.Tag = $"{CurrentContact.ContactTVItemID}";
                        butContactAndInfrastructureSaveToCSSPWebTools.Font = new Font(new FontFamily(butContactAndInfrastructureSaveToCSSPWebTools.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        butContactAndInfrastructureSaveToCSSPWebTools.Padding = new Padding(5);
                        butContactAndInfrastructureSaveToCSSPWebTools.Click += butSaveAllToCSSPWebTools_Click;

                        PanelViewAndEdit.Controls.Add(butContactAndInfrastructureSaveToCSSPWebTools);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                    }
                }

            }

            Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

            Label lblReturns = new Label();
            lblReturns.AutoSize = true;
            lblReturns.Location = new Point(30, Y);
            lblReturns.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblReturns.Font = new Font(new FontFamily(lblReturns.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
            lblReturns.Text = "\r\n\r\n\r\n\r\n";

            PanelViewAndEdit.Controls.Add(lblReturns);

            IsReading = false;
        }
        private void ShowInfrastructure()
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

                bool IsActive = false;
                if (CurrentInfrastructure.IsActiveNew != null)
                {
                    IsActive = (bool)CurrentInfrastructure.IsActiveNew;
                }
                else
                {
                    IsActive = (bool)CurrentInfrastructure.IsActive;
                }

                if (IsActive == false)
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
                    bool IsActive2 = false;
                    if (CurrentInfrastructure.IsActiveNew != null)
                    {
                        IsActive2 = (bool)CurrentInfrastructure.IsActiveNew;
                    }
                    else
                    {
                        IsActive2 = (bool)CurrentInfrastructure.IsActive;
                    }
                    if (IsActive2)
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
                DrawItemText(X, Y, CurrentInfrastructure.TVText, CurrentInfrastructure.TVTextNew, "Infrastructure Name", "textBoxTVText", 300, 0);

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

                if (IsEditing)
                {
                    Label lblWGS84Decimal = new Label();
                    lblWGS84Decimal.AutoSize = true;
                    lblWGS84Decimal.Location = new Point(10, Y);
                    lblWGS84Decimal.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblWGS84Decimal.Font = new Font(new FontFamily(lblWGS84Decimal.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                    lblWGS84Decimal.ForeColor = Color.Red;
                    lblWGS84Decimal.Text = $"Lat and Lng should be entered as WGS84 decimal degrees";

                    PanelViewAndEdit.Controls.Add(lblWGS84Decimal);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                }

                #region Address
                DrawItemAddress(X, Y, CurrentInfrastructure.InfrastructureAddress, CurrentInfrastructure.InfrastructureAddressNew, true);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                #endregion Address

                #region PrismID, TPID, LSID etc...
                //#region PrismID
                //X = 10;
                //DrawItemInt(X, Y, CurrentInfrastructure.PrismID, CurrentInfrastructure.PrismIDNew, "PrismID", "textBoxPrismID");

                //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                //#endregion PrismID

                //#region TPID
                //X = 10;
                //DrawItemInt(X, Y, CurrentInfrastructure.TPID, CurrentInfrastructure.TPIDNew, "TPID", "textBoxTPID");

                //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                //#endregion TPID

                //#region LSID
                //X = 10;
                //DrawItemInt(X, Y, CurrentInfrastructure.LSID, CurrentInfrastructure.LSIDNew, "LSID", "textBoxLSID");

                //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                //#endregion LSID

                //#region SiteID
                //X = 10;
                //DrawItemInt(X, Y, CurrentInfrastructure.SiteID, CurrentInfrastructure.SiteIDNew, "SiteID", "textBoxSiteID");

                //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                //#endregion SiteID

                //#region Site
                //X = 10;
                //DrawItemInt(X, Y, CurrentInfrastructure.Site, CurrentInfrastructure.SiteNew, "Site", "textBoxSite");

                //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                //#endregion Site

                //#region InfrastructureCategory
                //X = 10;
                //DrawItemText(X, Y, CurrentInfrastructure.InfrastructureCategory, CurrentInfrastructure.InfrastructureCategoryNew, "Infrastructure Category", "textBoxInfrastructureCategory", 300, 0);

                //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                //#endregion InfrastructureCategory

                #endregion  PrismID, TPID, LSID etc...

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
                    butSaveLatLngObsAndAddress.Click += butSaves_Click;

                    PanelViewAndEdit.Controls.Add(butSaveLatLngObsAndAddress);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;

                    Button butCancelLatLngObsAndAddress = new Button();
                    butCancelLatLngObsAndAddress.AutoSize = true;
                    butCancelLatLngObsAndAddress.Location = new Point(X, Y);
                    butCancelLatLngObsAndAddress.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butCancelLatLngObsAndAddress.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butCancelLatLngObsAndAddress.Padding = new Padding(5);
                    butCancelLatLngObsAndAddress.Text = $"Cancel";
                    butCancelLatLngObsAndAddress.Click += butCancel_Click;

                    PanelViewAndEdit.Controls.Add(butCancelLatLngObsAndAddress);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                }
                #endregion Save button

                #region InfrastructureType
                X = 10;
                DrawItemEnum(X, Y, CurrentInfrastructure.InfrastructureType, CurrentInfrastructure.InfrastructureTypeNew, "Infrastructure Type", "comboBoxInfrastructureType", typeof(InfrastructureTypeEnum), 0);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                bool IsWWTP = false;
                bool IsLS = false;
                bool IsLineOverflow = false;
                bool IsOther = false;
                bool IsSeeOtherMunicipality = false;
                if (CurrentInfrastructure.InfrastructureTypeNew != null)
                {
                    if ((InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureTypeNew == InfrastructureTypeEnum.WWTP)
                    {
                        IsWWTP = true;
                    }
                    if ((InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureTypeNew == InfrastructureTypeEnum.LiftStation)
                    {
                        IsLS = true;
                    }
                    if ((InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureTypeNew == InfrastructureTypeEnum.LineOverflow)
                    {
                        IsLineOverflow = true;
                    }
                    if ((InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureTypeNew == InfrastructureTypeEnum.Other)
                    {
                        IsOther = true;
                    }
                    if ((InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureTypeNew == InfrastructureTypeEnum.SeeOtherMunicipality)
                    {
                        IsSeeOtherMunicipality = true;
                    }
                }
                else
                {
                    if (CurrentInfrastructure.InfrastructureType != null)
                    {
                        if ((InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureType == InfrastructureTypeEnum.WWTP)
                        {
                            IsWWTP = true;
                        }
                        if ((InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureType == InfrastructureTypeEnum.LiftStation)
                        {
                            IsLS = true;
                        }
                        if ((InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureType == InfrastructureTypeEnum.LineOverflow)
                        {
                            IsLineOverflow = true;
                        }
                        if ((InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureType == InfrastructureTypeEnum.Other)
                        {
                            IsOther = true;
                        }
                        if ((InfrastructureTypeEnum)CurrentInfrastructure.InfrastructureType == InfrastructureTypeEnum.SeeOtherMunicipality)
                        {
                            IsSeeOtherMunicipality = true;
                        }
                    }
                }
                #endregion InfrastructureType

                #region FacilityType
                bool IsLagoon = false;
                bool IsPlant = false;
                if (IsWWTP)
                {
                    X = 10;
                    DrawItemEnum(X, Y, CurrentInfrastructure.FacilityType, CurrentInfrastructure.FacilityTypeNew, "Facility Type", "comboBoxFacilityType", typeof(FacilityTypeEnum), 0);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                    if (CurrentInfrastructure.FacilityTypeNew != null)
                    {
                        if ((FacilityTypeEnum)CurrentInfrastructure.FacilityTypeNew == FacilityTypeEnum.Lagoon)
                        {
                            IsLagoon = true;
                        }
                        if ((FacilityTypeEnum)CurrentInfrastructure.FacilityTypeNew == FacilityTypeEnum.Plant)
                        {
                            IsPlant = true;
                        }
                    }
                    else
                    {
                        if (CurrentInfrastructure.FacilityType != null)
                        {
                            if ((FacilityTypeEnum)CurrentInfrastructure.FacilityType == FacilityTypeEnum.Lagoon)
                            {
                                IsLagoon = true;
                            }
                            if ((FacilityTypeEnum)CurrentInfrastructure.FacilityType == FacilityTypeEnum.Plant)
                            {
                                IsPlant = true;
                            }
                        }
                    }
                }
                #endregion FacilityType


                if (IsWWTP)
                {
                    if (IsLagoon)
                    {
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

                        #region IsMechanicallyAerated
                        X = 10;
                        DrawItemBool(X, Y, CurrentInfrastructure.IsMechanicallyAerated, CurrentInfrastructure.IsMechanicallyAeratedNew, "Is Mechanically Aerated", "checkBoxIsMechanicallyAerated");

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        #endregion IsMechanicallyAerated


                        if (CurrentInfrastructure.IsMechanicallyAeratedNew != null)
                        {
                            if (CurrentInfrastructure.IsMechanicallyAeratedNew == true)
                            {
                                #region AerationType
                                X = 40;
                                DrawItemEnum(X, Y, CurrentInfrastructure.AerationType, CurrentInfrastructure.AerationTypeNew, "Aeration Type", "comboBoxAerationType", typeof(AerationTypeEnum), 0);

                                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                                #endregion AerationType
                            }
                        }
                        else
                        {
                            if (CurrentInfrastructure.IsMechanicallyAerated == true)
                            {
                                #region AerationType
                                X = 40;
                                DrawItemEnum(X, Y, CurrentInfrastructure.AerationType, CurrentInfrastructure.AerationTypeNew, "Aeration Type", "comboBoxAerationType", typeof(AerationTypeEnum), 0);

                                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                                #endregion AerationType
                            }
                        }

                        #region DisinfectionType
                        X = 10;
                        DrawItemEnum(X, Y, CurrentInfrastructure.DisinfectionType, CurrentInfrastructure.DisinfectionTypeNew, "Disinfection Type", "comboBoxDisinfectionType", typeof(DisinfectionTypeEnum), 0);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        #endregion DisinfectionType
                    }

                    if (IsPlant)
                    {

                        #region PreliminaryTreatmentType
                        X = 10;
                        DrawItemEnum(X, Y, CurrentInfrastructure.PreliminaryTreatmentType, CurrentInfrastructure.PreliminaryTreatmentTypeNew, "Preliminary Treatment Type", "comboBoxPreliminaryTreatmentType", typeof(PreliminaryTreatmentTypeEnum), 0);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        #endregion PreliminaryTreatmentType

                        #region PrimaryTreatmentType
                        X = 10;
                        DrawItemEnum(X, Y, CurrentInfrastructure.PrimaryTreatmentType, CurrentInfrastructure.PrimaryTreatmentTypeNew, "Primary Treatment Type", "comboBoxPrimaryTreatmentType", typeof(PrimaryTreatmentTypeEnum), 0);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        #endregion PrimaryTreatmentType

                        #region SecondaryTreatmentType
                        X = 10;
                        DrawItemEnum(X, Y, CurrentInfrastructure.SecondaryTreatmentType, CurrentInfrastructure.SecondaryTreatmentTypeNew, "Secondary Treatment Type", "comboBoxSecondaryTreatmentType", typeof(SecondaryTreatmentTypeEnum), 0);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        #endregion SecondaryTreatmentType

                        #region TertiaryTreatmentType
                        X = 10;
                        DrawItemEnum(X, Y, CurrentInfrastructure.TertiaryTreatmentType, CurrentInfrastructure.TertiaryTreatmentTypeNew, "Tertiary Treatment Type", "comboBoxTertiaryTreatmentType", typeof(TertiaryTreatmentTypeEnum), 0);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        #endregion TertiaryTreatmentType

                        #region DisinfectionType
                        X = 10;
                        DrawItemEnum(X, Y, CurrentInfrastructure.DisinfectionType, CurrentInfrastructure.DisinfectionTypeNew, "Disinfection Type", "comboBoxDisinfectionType", typeof(DisinfectionTypeEnum), 0);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        #endregion DisinfectionType
                    }

                    #region AlarmSystemType
                    X = 10;
                    DrawItemEnum(X, Y, CurrentInfrastructure.AlarmSystemType, CurrentInfrastructure.AlarmSystemTypeNew, "Alarm System Type", "comboBoxAlarmSystemType", typeof(AlarmSystemTypeEnum), 0);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion AlarmSystemType

                    #region CollectionSystemType
                    X = 10;
                    DrawItemEnum(X, Y, CurrentInfrastructure.CollectionSystemType, CurrentInfrastructure.CollectionSystemTypeNew, "Collection System Type", "comboBoxCollectionSystemType", typeof(CollectionSystemTypeEnum), 0);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion CollectionSystemType

                    #region DesignFlow_m3_day
                    X = 10;
                    DrawItemFloat(X, Y, CurrentInfrastructure.DesignFlow_m3_day, CurrentInfrastructure.DesignFlow_m3_dayNew, "Design Flow (m3/day)", 0, "textBoxDesignFlow_m3_day");

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    DrawItemFloat(X, Y, CurrentInfrastructure.DesignFlow_m3_day, CurrentInfrastructure.DesignFlow_m3_dayNew, "(Can. Gal./day)", 0, "textBoxDesignFlow_CanGal_day");

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    DrawItemFloat(X, Y, CurrentInfrastructure.DesignFlow_m3_day, CurrentInfrastructure.DesignFlow_m3_dayNew, "(US. Gal./day)", 0, "textBoxDesignFlow_USGal_day");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion DesignFlow_m3_day

                    #region AverageFlow_m3_day
                    X = 10;
                    DrawItemFloat(X, Y, CurrentInfrastructure.AverageFlow_m3_day, CurrentInfrastructure.AverageFlow_m3_dayNew, "Average Flow (m3/day)", 0, "textBoxAverageFlow_m3_day");

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    DrawItemFloat(X, Y, CurrentInfrastructure.AverageFlow_m3_day, CurrentInfrastructure.AverageFlow_m3_dayNew, "(Can. Gal./day)", 0, "textBoxAverageFlow_CanGal_day");

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    DrawItemFloat(X, Y, CurrentInfrastructure.AverageFlow_m3_day, CurrentInfrastructure.AverageFlow_m3_dayNew, "(US. Gal./day)", 0, "textBoxAverageFlow_USGal_day");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion AverageFlow_m3_day

                    #region PeakFlow_m3_day
                    X = 10;
                    DrawItemFloat(X, Y, CurrentInfrastructure.PeakFlow_m3_day, CurrentInfrastructure.PeakFlow_m3_dayNew, "Peak Flow (m3/day)", 0, "textBoxPeakFlow_m3_day");

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    DrawItemFloat(X, Y, CurrentInfrastructure.PeakFlow_m3_day, CurrentInfrastructure.PeakFlow_m3_dayNew, "(Can. Gal./day)", 0, "textBoxPeakFlow_CanGal_day");

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    DrawItemFloat(X, Y, CurrentInfrastructure.PeakFlow_m3_day, CurrentInfrastructure.PeakFlow_m3_dayNew, "(US. Gal./day)", 0, "textBoxPeakFlow_USGal_day");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion PeakFlow_m3_day

                    #region PopServed
                    X = 10;
                    DrawItemInt(X, Y, CurrentInfrastructure.PopServed, CurrentInfrastructure.PopServedNew, "Population Served", "textBoxPopServed");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion PopServed

                    #region CanOverflow
                    X = 10;
                    DrawItemEnum(X, Y, CurrentInfrastructure.CanOverflow, CurrentInfrastructure.CanOverflowNew, "Can Overflow", "comboBoxCanOverflowType", typeof(CanOverflowTypeEnum), 0);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion CanOverflow

                    bool ShowValve = false;
                    if (CurrentInfrastructure.CanOverflowNew != null)
                    {
                        if (CurrentInfrastructure.CanOverflowNew == (int)CanOverflowTypeEnum.Yes)
                        {
                            ShowValve = true;
                        }
                    }
                    else
                    {
                        if (CurrentInfrastructure.CanOverflow == (int)CanOverflowTypeEnum.Yes)
                        {
                            ShowValve = true;
                        }
                    }

                    if (ShowValve)
                    {
                        #region ValveType
                        X = 10;
                        DrawItemEnum(X, Y, CurrentInfrastructure.ValveType, CurrentInfrastructure.ValveTypeNew, "Valve Type", "comboBoxValveType", typeof(ValveTypeEnum), 0);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        #endregion ValveType
                    }

                    #region HasBackupPower
                    X = 10;
                    DrawItemBool(X, Y, CurrentInfrastructure.HasBackupPower, CurrentInfrastructure.HasBackupPowerNew, "Has Backup Power", "checkBoxHasBackupPower");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion HasBackupPower

                    #region PercFlowOfTotal
                    X = 10;
                    DrawItemFloat(X, Y, CurrentInfrastructure.PercFlowOfTotal, CurrentInfrastructure.PercFlowOfTotalNew, "Percentage Flow Of Total", 1, "textBoxPercFlowOfTotal");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion PercFlowOfTotal

                    #region PumpsToTVItemID
                    X = 10;
                    DrawItemInt(X, Y, CurrentInfrastructure.PumpsToTVItemID, CurrentInfrastructure.PumpsToTVItemIDNew, "Pumps To Infrastructure", "textBoxPumpsToTVItemID");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion PumpsToTVItemID
                }

                if (IsLS || IsLineOverflow)
                {
                    #region AlarmSystemType
                    X = 10;
                    DrawItemEnum(X, Y, CurrentInfrastructure.AlarmSystemType, CurrentInfrastructure.AlarmSystemTypeNew, "Alarm System Type", "comboBoxAlarmSystemType", typeof(AlarmSystemTypeEnum), 0);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion AlarmSystemType

                    #region CanOverflow
                    X = 10;
                    DrawItemEnum(X, Y, CurrentInfrastructure.CanOverflow, CurrentInfrastructure.CanOverflowNew, "Can Overflow", "comboBoxCanOverflowType", typeof(CanOverflowTypeEnum), 0);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion CanOverflow

                    bool ShowValve = false;
                    if (CurrentInfrastructure.CanOverflowNew != null)
                    {
                        if (CurrentInfrastructure.CanOverflowNew == (int)CanOverflowTypeEnum.Yes)
                        {
                            ShowValve = true;
                        }
                    }
                    else
                    {
                        if (CurrentInfrastructure.CanOverflow == (int)CanOverflowTypeEnum.Yes)
                        {
                            ShowValve = true;
                        }
                    }

                    if (ShowValve)
                    {
                        #region ValveType
                        X = 10;
                        DrawItemEnum(X, Y, CurrentInfrastructure.ValveType, CurrentInfrastructure.ValveTypeNew, "Valve Type", "comboBoxValveType", typeof(ValveTypeEnum), 0);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        #endregion ValveType
                    }

                    #region HasBackupPower
                    X = 10;
                    DrawItemBool(X, Y, CurrentInfrastructure.HasBackupPower, CurrentInfrastructure.HasBackupPowerNew, "Has Backup Power", "checkBoxHasBackupPower");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion HasBackupPower

                    #region PercFlowOfTotal
                    X = 10;
                    DrawItemFloat(X, Y, CurrentInfrastructure.PercFlowOfTotal, CurrentInfrastructure.PercFlowOfTotalNew, "Percentage Flow Of Total", 1, "textBoxPercFlowOfTotal");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion PercFlowOfTotal

                    #region PumpsToTVItemID
                    X = 10;
                    DrawItemInt(X, Y, CurrentInfrastructure.PumpsToTVItemID, CurrentInfrastructure.PumpsToTVItemIDNew, "Pumps To Infrastructure", "textBoxPumpsToTVItemID");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion PumpsToTVItemID
                }

                if (IsOther)
                {
                    // nothing to draw
                }

                if (IsSeeOtherMunicipality)
                {
                    #region List Municipalities
                    X = 10;
                    DrawSeeOtherMunicipality(X, Y, "comboBoxSeeOtherMunicipality");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion List Municipalities

                }

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
                    butSaveLatLngObsAndAddress.Click += butSaves_Click;

                    PanelViewAndEdit.Controls.Add(butSaveLatLngObsAndAddress);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;

                    Button butCancelLatLngObsAndAddress = new Button();
                    butCancelLatLngObsAndAddress.AutoSize = true;
                    butCancelLatLngObsAndAddress.Location = new Point(X, Y);
                    butCancelLatLngObsAndAddress.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butCancelLatLngObsAndAddress.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butCancelLatLngObsAndAddress.Padding = new Padding(5);
                    butCancelLatLngObsAndAddress.Text = $"Cancel";
                    butCancelLatLngObsAndAddress.Click += butCancel_Click;

                    PanelViewAndEdit.Controls.Add(butCancelLatLngObsAndAddress);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                }
                #endregion Save button


                #region CommentEN
                X = 10;
                DrawItemTextMultiline(X, Y, CurrentInfrastructure.CommentEN, CurrentInfrastructure.CommentENNew, "Comment (EN)", "textBoxCommentEN", 500);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion CommentEN

                #region CommentFR
                X = 10;
                DrawItemTextMultiline(X, Y, CurrentInfrastructure.CommentFR, CurrentInfrastructure.CommentFRNew, "Comment (FR)", "textBoxCommentFR", 500);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                #endregion CommentFR

                if (IsLS || IsWWTP || IsLineOverflow)
                {
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
                        butSaveLatLngObsAndAddress.Click += butSaves_Click;

                        PanelViewAndEdit.Controls.Add(butSaveLatLngObsAndAddress);

                        X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;

                        Button butCancelLatLngObsAndAddress = new Button();
                        butCancelLatLngObsAndAddress.AutoSize = true;
                        butCancelLatLngObsAndAddress.Location = new Point(X, Y);
                        butCancelLatLngObsAndAddress.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        butCancelLatLngObsAndAddress.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butCancelLatLngObsAndAddress.Padding = new Padding(5);
                        butCancelLatLngObsAndAddress.Text = $"Cancel";
                        butCancelLatLngObsAndAddress.Click += butCancel_Click;

                        PanelViewAndEdit.Controls.Add(butCancelLatLngObsAndAddress);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                    }
                    #endregion Save button

                    #region Optional values for Visual Plumes and Box Model 
                    Label lblOptional = new Label();
                    lblOptional.AutoSize = true;
                    lblOptional.Location = new Point(X, Y);
                    lblOptional.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblOptional.Font = new Font(new FontFamily(lblOptional.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                    lblOptional.Text = $"\r\n\r\nOptional information for Visual Plumes and Box Model";

                    PanelViewAndEdit.Controls.Add(lblOptional);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                    #endregion Optional values for Visual Plumes and Box Model 


                    #region End of Pipe 
                    Label lblEndOfPipe = new Label();
                    lblEndOfPipe.AutoSize = true;
                    lblEndOfPipe.Location = new Point(X, Y);
                    lblEndOfPipe.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblEndOfPipe.Font = new Font(new FontFamily(lblEndOfPipe.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                    lblEndOfPipe.Text = $"End of Pipe";

                    PanelViewAndEdit.Controls.Add(lblEndOfPipe);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                    #endregion End of Pipe 

                    X = 20;

                    #region NumberOfPorts
                    DrawItemInt(X, Y, CurrentInfrastructure.NumberOfPorts, CurrentInfrastructure.NumberOfPortsNew, "Number Of Ports", "textBoxNumberOfPorts");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion NumberOfPorts

                    #region PortDiameter_m
                    DrawItemFloat(X, Y, CurrentInfrastructure.PortDiameter_m, CurrentInfrastructure.PortDiameter_mNew, "Port Diameter (m)", 5, "textBoxPortDiameter_m");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion PortDiameter_m

                    #region PortSpacing_m
                    DrawItemFloat(X, Y, CurrentInfrastructure.PortSpacing_m, CurrentInfrastructure.PortSpacing_mNew, "Port Spacing (m)", 5, "textBoxPortSpacing_m");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion PortSpacing_m

                    #region PortElevation_m
                    DrawItemFloat(X, Y, CurrentInfrastructure.PortElevation_m, CurrentInfrastructure.PortElevation_mNew, "Port Elevation (m)", 5, "textBoxPortElevation_m");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion PortElevation_m

                    #region VerticalAngle_deg
                    DrawItemFloat(X, Y, CurrentInfrastructure.VerticalAngle_deg, CurrentInfrastructure.VerticalAngle_degNew, "Vertical Angle (deg)", 5, "textBoxVerticalAngle_deg");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion VerticalAngle_deg

                    #region HorizontalAngle_deg
                    DrawItemFloat(X, Y, CurrentInfrastructure.HorizontalAngle_deg, CurrentInfrastructure.HorizontalAngle_degNew, "Horizontal Angle (deg)", 5, "textBoxHorizontalAngle_deg");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion HorizontalAngle_deg

                    #region DistanceFromShore_m
                    DrawItemFloat(X, Y, CurrentInfrastructure.DistanceFromShore_m, CurrentInfrastructure.DistanceFromShore_mNew, "Distance From Shore (m)", 5, "textBoxDistanceFromShore_m");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                    #endregion DistanceFromShore_m

                    X = 10;

                    #region Surrounding water conditions 
                    Label lblWaterConditions = new Label();
                    lblWaterConditions.AutoSize = true;
                    lblWaterConditions.Location = new Point(X, Y);
                    lblWaterConditions.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblWaterConditions.Font = new Font(new FontFamily(lblWaterConditions.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                    lblWaterConditions.Text = $"Surrounding water conditions";

                    PanelViewAndEdit.Controls.Add(lblWaterConditions);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                    #endregion Surrounding water conditions

                    X = 20;
                    #region AverageDepth_m
                    DrawItemFloat(X, Y, CurrentInfrastructure.AverageDepth_m, CurrentInfrastructure.AverageDepth_mNew, "Average Depth (m)", 5, "textBoxAverageDepth_m");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion AverageDepth_m

                    #region DecayRate_per_day
                    DrawItemFloat(X, Y, CurrentInfrastructure.DecayRate_per_day, CurrentInfrastructure.DecayRate_per_dayNew, "Decay Rate (/day)", 5, "textBoxDecayRate_per_day");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion DecayRate_per_day

                    #region NearFieldVelocity_m_s
                    DrawItemFloat(X, Y, CurrentInfrastructure.NearFieldVelocity_m_s, CurrentInfrastructure.NearFieldVelocity_m_sNew, "Near Field Velocity (m/s)", 5, "textBoxNearFieldVelocity_m_s");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion NearFieldVelocity_m_s

                    #region FarFieldVelocity_m_s
                    DrawItemFloat(X, Y, CurrentInfrastructure.FarFieldVelocity_m_s, CurrentInfrastructure.FarFieldVelocity_m_sNew, "Far Field Velocity (m/s)", 5, "textBoxFarFieldVelocity_m_s");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion FarFieldVelocity_m_s

                    #region ReceivingWaterSalinity_PSU
                    DrawItemFloat(X, Y, CurrentInfrastructure.ReceivingWaterSalinity_PSU, CurrentInfrastructure.ReceivingWaterSalinity_PSUNew, "Receiving Water Salinity (PSU)", 5, "textBoxReceivingWaterSalinity_PSU");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion ReceivingWaterSalinity_PSU

                    #region ReceivingWaterTemperature_C
                    DrawItemFloat(X, Y, CurrentInfrastructure.ReceivingWaterTemperature_C, CurrentInfrastructure.ReceivingWaterTemperature_CNew, "Receiving Water Temperature (C)", 5, "textBoxReceivingWaterTemperature_C");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion ReceivingWaterTemperature_C

                    #region ReceivingWater_MPN_per_100ml
                    DrawItemInt(X, Y, CurrentInfrastructure.ReceivingWater_MPN_per_100ml, CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew, "Receiving Water (MPN /100 mL)", "textBoxReceivingWater_MPN_per_100ml");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion ReceivingWater_MPN_per_100ml

                }


                if (false)
                {
                    #region SeeOtherMunicipalityTVItemID
                    X = 10;
                    DrawItemInt(X, Y, CurrentInfrastructure.SeeOtherMunicipalityTVItemID, CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew, "See Other TVItemID", "textBoxSeeOtherMunicipalityTVItemID");

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    #endregion SeeOtherMunicipalityTVItemID
                }

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
                    butSaveLatLngObsAndAddress.Click += butSaves_Click;

                    PanelViewAndEdit.Controls.Add(butSaveLatLngObsAndAddress);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;

                    Button butCancelLatLngObsAndAddress = new Button();
                    butCancelLatLngObsAndAddress.AutoSize = true;
                    butCancelLatLngObsAndAddress.Location = new Point(X, Y);
                    butCancelLatLngObsAndAddress.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butCancelLatLngObsAndAddress.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butCancelLatLngObsAndAddress.Padding = new Padding(5);
                    butCancelLatLngObsAndAddress.Text = $"Cancel";
                    butCancelLatLngObsAndAddress.Click += butCancel_Click;

                    PanelViewAndEdit.Controls.Add(butCancelLatLngObsAndAddress);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                }
                #endregion Save button

                if (!IsEditing)
                {
                    ShowPictures();
                }

                if (IsAdmin)
                {
                    bool NeedDetailsUpdate = false;
                    bool NeedPicturesUpdate = false;
                    bool NeedActiveUpdate = false;

                    if (CurrentInfrastructure.IsActiveNew != null && CurrentInfrastructure.IsActiveNew != CurrentInfrastructure.IsActive)
                    {
                        NeedActiveUpdate = true;
                    }

                    if (CurrentInfrastructure.LatNew != null
                       || CurrentInfrastructure.LngNew != null
                       || CurrentInfrastructure.LatOutfallNew != null
                       || CurrentInfrastructure.LngOutfallNew != null
                       || CurrentInfrastructure.IsActiveNew != null
                       || CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID != null
                       || CurrentInfrastructure.InfrastructureAddressNew.AddressType != null
                       // || CurrentInfrastructure.InfrastructureAddressNew.Municipality != null
                       || CurrentInfrastructure.InfrastructureAddressNew.PostalCode != null
                       || CurrentInfrastructure.InfrastructureAddressNew.StreetName != null
                       || CurrentInfrastructure.InfrastructureAddressNew.StreetNumber != null
                       || CurrentInfrastructure.InfrastructureAddressNew.StreetType != null)
                    {
                        NeedDetailsUpdate = true;
                    }

                    if (!NeedDetailsUpdate)
                    {
                        if (CurrentInfrastructure.AerationTypeNew != null
                            || CurrentInfrastructure.AlarmSystemTypeNew != null
                            || CurrentInfrastructure.AverageDepth_mNew != null
                            || CurrentInfrastructure.AverageFlow_m3_dayNew != null
                            || CurrentInfrastructure.CanOverflowNew != null
                            || CurrentInfrastructure.CollectionSystemTypeNew != null
                            || CurrentInfrastructure.CommentENNew != null
                            || CurrentInfrastructure.CommentFRNew != null
                            || CurrentInfrastructure.DecayRate_per_dayNew != null
                            || CurrentInfrastructure.DesignFlow_m3_dayNew != null
                            || CurrentInfrastructure.DisinfectionTypeNew != null
                            || CurrentInfrastructure.DistanceFromShore_mNew != null
                            || CurrentInfrastructure.FacilityTypeNew != null
                            || CurrentInfrastructure.FarFieldVelocity_m_sNew != null
                            || CurrentInfrastructure.HorizontalAngle_degNew != null
                            || CurrentInfrastructure.InfrastructureTypeNew != null
                            || CurrentInfrastructure.IsActiveNew != null
                            || CurrentInfrastructure.IsMechanicallyAeratedNew != null
                            || CurrentInfrastructure.NearFieldVelocity_m_sNew != null
                            || CurrentInfrastructure.NumberOfAeratedCellsNew != null
                            || CurrentInfrastructure.NumberOfCellsNew != null
                            || CurrentInfrastructure.NumberOfPortsNew != null
                            || CurrentInfrastructure.PeakFlow_m3_dayNew != null
                            || CurrentInfrastructure.PercFlowOfTotalNew != null
                            || CurrentInfrastructure.PopServedNew != null
                            || CurrentInfrastructure.PortDiameter_mNew != null
                            || CurrentInfrastructure.PortElevation_mNew != null
                            || CurrentInfrastructure.PortSpacing_mNew != null
                            || CurrentInfrastructure.PreliminaryTreatmentTypeNew != null
                            || CurrentInfrastructure.PrimaryTreatmentTypeNew != null
                            || CurrentInfrastructure.PumpsToTVItemIDNew != null
                            || CurrentInfrastructure.ReceivingWaterSalinity_PSUNew != null
                            || CurrentInfrastructure.ReceivingWaterTemperature_CNew != null
                            || CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew != null
                            || CurrentInfrastructure.SecondaryTreatmentTypeNew != null
                            || CurrentInfrastructure.SeeOtherMunicipalityTVItemIDNew != null
                            || CurrentInfrastructure.TertiaryTreatmentTypeNew != null
                            || CurrentInfrastructure.TVTextNew != null
                            || CurrentInfrastructure.VerticalAngle_degNew != null)
                        {
                            NeedDetailsUpdate = true;
                        }
                    }
                    foreach (Picture picture in CurrentInfrastructure.InfrastructurePictureList)
                    {
                        if (picture.DescriptionNew != null
                            || picture.ExtensionNew != null
                            || picture.FileNameNew != null
                            || picture.ToRemove != null
                            || picture.FromWaterNew != null
                            || picture.PictureTVItemID >= 10000000)
                        {
                            NeedPicturesUpdate = true;
                            break;
                        }
                    }

                    string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                    string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                    string NeedActiveUpdateText = NeedActiveUpdate ? "Active" : "";

                    if (NeedDetailsUpdate || NeedPicturesUpdate || NeedActiveUpdate)
                    {
                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                        Button butInfrastructureSaveToCSSPWebTools = new Button();
                        butInfrastructureSaveToCSSPWebTools.AutoSize = true;
                        butInfrastructureSaveToCSSPWebTools.Location = new Point(20, Y);
                        butInfrastructureSaveToCSSPWebTools.Text = "Update All Contact and Infrastructure Related Information To CSSPWebTools";
                        butInfrastructureSaveToCSSPWebTools.Tag = $"{CurrentInfrastructure.InfrastructureTVItemID}";
                        butInfrastructureSaveToCSSPWebTools.Font = new Font(new FontFamily(butInfrastructureSaveToCSSPWebTools.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        butInfrastructureSaveToCSSPWebTools.Padding = new Padding(5);
                        butInfrastructureSaveToCSSPWebTools.Click += butSaveAllToCSSPWebTools_Click;

                        PanelViewAndEdit.Controls.Add(butInfrastructureSaveToCSSPWebTools);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                    }
                }

            }

            Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

            Label lblReturns = new Label();
            lblReturns.AutoSize = true;
            lblReturns.Location = new Point(30, Y);
            lblReturns.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblReturns.Font = new Font(new FontFamily(lblReturns.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
            lblReturns.Text = "\r\n\r\n\r\n\r\n";

            PanelViewAndEdit.Controls.Add(lblReturns);

            IsReading = false;
        }
        public void ShowContactOrInfrastructure()
        {
            if (ContactTVItemID > 0 && InfrastructureTVItemID == 0)
            {
                ShowContact();
            }
            else if (InfrastructureTVItemID > 0)
            {
                ShowInfrastructure();
            }
            else
            {
                // nothing
            }
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

                bool IsActive = false;
                if (CurrentPSS.IsActiveNew != null)
                {
                    IsActive = (bool)CurrentPSS.IsActiveNew;
                }
                else
                {
                    IsActive = (bool)CurrentPSS.IsActive;
                }

                if (IsActive == false)
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Strikeout, GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                }
                //lblTVText.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblTVText.Text = $"{(CurrentPSS.TVTextNew != null ? CurrentPSS.TVTextNew : CurrentPSS.TVText)}";

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
                    Label lblWGS84Decimal = new Label();
                    lblWGS84Decimal.AutoSize = true;
                    lblWGS84Decimal.Location = new Point(10, Y);
                    lblWGS84Decimal.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblWGS84Decimal.Font = new Font(new FontFamily(lblWGS84Decimal.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                    lblWGS84Decimal.ForeColor = Color.Red;
                    lblWGS84Decimal.Text = $"Lat and Lng should be entered as WGS84 decimal degrees";

                    PanelViewAndEdit.Controls.Add(lblWGS84Decimal);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                    bool IsActive2 = false;
                    if (CurrentPSS.IsActiveNew != null)
                    {
                        IsActive2 = (bool)CurrentPSS.IsActiveNew;
                    }
                    else
                    {
                        IsActive2 = (bool)CurrentPSS.IsActive;
                    }

                    if (IsActive2)
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
                    bool IsPointSource = false;
                    if (CurrentPSS.IsPointSourceNew != null)
                    {
                        IsPointSource = (bool)CurrentPSS.IsPointSourceNew;
                    }
                    else
                    {
                        IsPointSource = (bool)CurrentPSS.IsPointSource;
                    }
                    if (IsPointSource)
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
                DrawItemAddress(X, Y, CurrentPSS.PSSAddress, CurrentPSS.PSSAddressNew, false);

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
                            lblLastObsOld.Text = $" ({((DateTime)CurrentPSS.PSSObs.ObsDate).ToString("yyyy MMMM dd")})";
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
                    dateTimePickerObsDate.ValueChanged += dateTimePickerObsDate_ValueChanged;

                    PanelViewAndEdit.Controls.Add(dateTimePickerObsDate);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

                    Label lblObsMakeChange = new Label();
                    lblObsMakeChange.AutoSize = true;
                    lblObsMakeChange.Location = new Point(10, Y);
                    lblObsMakeChange.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblObsMakeChange.Font = new Font(new FontFamily(lblObsMakeChange.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                    lblObsMakeChange.ForeColor = Color.Red;
                    lblObsMakeChange.Text = $"Observation date should be changed for new observation and issues";

                    PanelViewAndEdit.Controls.Add(lblObsMakeChange);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

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
                    butSaveLatLngObsAndAddress.Click += butSaves_Click;

                    PanelViewAndEdit.Controls.Add(butSaveLatLngObsAndAddress);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 10;

                    Button butCancelLatLngObsAndAddress = new Button();
                    butCancelLatLngObsAndAddress.AutoSize = true;
                    butCancelLatLngObsAndAddress.Location = new Point(X, Y);
                    butCancelLatLngObsAndAddress.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    butCancelLatLngObsAndAddress.Font = new Font(new FontFamily(lblTVText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    butCancelLatLngObsAndAddress.Padding = new Padding(5);
                    butCancelLatLngObsAndAddress.Text = $"Cancel";
                    butCancelLatLngObsAndAddress.Click += butCancel_Click;

                    PanelViewAndEdit.Controls.Add(butCancelLatLngObsAndAddress);
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
                    bool NeedActiveUpdate = false;
                    bool NeedPointSourceUpdate = false;

                    if (CurrentPSS.IsActiveNew != null && CurrentPSS.IsActiveNew != CurrentPSS.IsActive)
                    {
                        NeedActiveUpdate = true;
                    }

                    if (CurrentPSS.IsPointSourceNew != null && CurrentPSS.IsPointSourceNew != CurrentPSS.IsPointSource)
                    {
                        NeedPointSourceUpdate = true;
                    }

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
                        if (issue.PolSourceObsInfoIntListNew.Count > 0 || issue.ExtraCommentNew != null || issue.ToRemove == true)
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
                            || picture.ToRemove != null
                            || picture.FromWaterNew != null
                            || picture.PictureTVItemID >= 10000000)
                        {
                            NeedPicturesUpdate = true;
                            break;
                        }
                    }

                    string NeedDetailsUpdateText = NeedDetailsUpdate ? "Details" : "";
                    string NeedIssuesUpdateText = NeedIssuesUpdate ? "Issues" : "";
                    string NeedPictuesUpdateText = NeedPicturesUpdate ? "Pictures" : "";
                    string NeedActiveUpdateText = NeedActiveUpdate ? "Active" : "";
                    string NeedPointSourceUpdateText = NeedPointSourceUpdate ? "Point Source" : "";
                    if (NeedDetailsUpdate || NeedIssuesUpdate || NeedPicturesUpdate || NeedActiveUpdate || NeedPointSourceUpdate)
                    {
                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                        Button butPSSSaveToCSSPWebTools = new Button();
                        butPSSSaveToCSSPWebTools.AutoSize = true;
                        butPSSSaveToCSSPWebTools.Location = new Point(20, Y);
                        butPSSSaveToCSSPWebTools.Text = "Update this Pollution Source Site Related Information To CSSPWebTools";
                        butPSSSaveToCSSPWebTools.Tag = $"{CurrentPSS.PSSTVItemID}";
                        butPSSSaveToCSSPWebTools.Font = new Font(new FontFamily(butPSSSaveToCSSPWebTools.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        butPSSSaveToCSSPWebTools.Padding = new Padding(5);
                        butPSSSaveToCSSPWebTools.Click += butSaveToCSSPWebTools_Click;

                        PanelViewAndEdit.Controls.Add(butPSSSaveToCSSPWebTools);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                        Button butPSSSaveAllToCSSPWebTools = new Button();
                        butPSSSaveAllToCSSPWebTools.AutoSize = true;
                        butPSSSaveAllToCSSPWebTools.Location = new Point(20, Y);
                        butPSSSaveAllToCSSPWebTools.Text = "Update All Pollution Source Site Related Information To CSSPWebTools";
                        butPSSSaveAllToCSSPWebTools.Tag = $"{CurrentPSS.PSSTVItemID}";
                        butPSSSaveAllToCSSPWebTools.Font = new Font(new FontFamily(butPSSSaveToCSSPWebTools.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                        butPSSSaveAllToCSSPWebTools.Padding = new Padding(5);
                        butPSSSaveAllToCSSPWebTools.Click += butSaveAllToCSSPWebTools_Click;

                        PanelViewAndEdit.Controls.Add(butPSSSaveAllToCSSPWebTools);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        X = 20;

                    }
                }
            }

            Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

            Label lblReturns = new Label();
            lblReturns.AutoSize = true;
            lblReturns.Location = new Point(30, Y);
            lblReturns.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblReturns.Font = new Font(new FontFamily(lblReturns.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
            lblReturns.Text = "\r\n\r\n\r\n\r\n";

            PanelViewAndEdit.Controls.Add(lblReturns);

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
                                if (ContactTVItemID > 0)
                                {
                                    if ("" + CurrentContact.ContactAddress.PostalCode == tb.Text)
                                    {
                                        CurrentContact.ContactAddressNew.PostalCode = null;
                                    }
                                    else
                                    {
                                        CurrentContact.ContactAddressNew.AddressTVItemID = 10000000;
                                        if (!string.IsNullOrWhiteSpace(tb.Text))
                                        {
                                            if (tb.Text.Trim().Length > 7)
                                            {
                                                MessageBox.Show("Postal Code maximum length is 7 characters", "Error");
                                            }
                                            CurrentContact.ContactAddressNew.PostalCode = tb.Text;
                                        }
                                        else
                                        {
                                            CurrentContact.ContactAddressNew.PostalCode = null;
                                            CurrentContact.ContactAddress.PostalCode = null;
                                        }
                                        IsDirty = true;
                                    }
                                }
                                else
                                {
                                    if ("" + CurrentInfrastructure.InfrastructureAddress.PostalCode == tb.Text)
                                    {
                                        CurrentInfrastructure.InfrastructureAddressNew.PostalCode = null;
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                        if (!string.IsNullOrWhiteSpace(tb.Text))
                                        {
                                            if (tb.Text.Trim().Length > 7)
                                            {
                                                MessageBox.Show("Postal Code maximum length is 7 characters", "Error");
                                            }
                                            CurrentInfrastructure.InfrastructureAddressNew.PostalCode = tb.Text;
                                        }
                                        else
                                        {
                                            CurrentInfrastructure.InfrastructureAddressNew.PostalCode = null;
                                            CurrentInfrastructure.InfrastructureAddress.PostalCode = null;
                                        }
                                        IsDirty = true;
                                    }
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
                                    if (TempFloat < -90.0f || TempFloat > 90.0f)
                                    {
                                        MessageBox.Show("Latitude should be between -90 and 90", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.LatNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.LatNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.Lat.ToString();
                                    MessageBox.Show("Please enter a valid number for Latitute", "Error");
                                }
                                else
                                {
                                    CurrentInfrastructure.Lat = null;
                                }
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
                                    if (TempFloat < -90.0f || TempFloat > 90.0f)
                                    {
                                        MessageBox.Show("Latitude Outfall should be between -90 and 90", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.LatOutfallNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.LatOutfallNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.LatOutfall.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Latitude Outfall"));
                                }
                                else
                                {
                                    CurrentInfrastructure.LatOutfall = null;
                                }
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
                                    if (TempFloat < -180.0f || TempFloat > 180.0f)
                                    {
                                        MessageBox.Show("Longitude should be between -180 and 180", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.LngNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.LngNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.Lng.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Longitude"));
                                }
                                else
                                {
                                    CurrentInfrastructure.Lng = null;
                                }
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
                                    if (TempFloat < -180.0f || TempFloat > 180.0f)
                                    {
                                        MessageBox.Show("Longitude Outfall should be between -180 and 180", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.LngOutfallNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.LngOutfallNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.LngOutfall.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Longitude Outfall"));
                                }
                                else
                                {
                                    CurrentInfrastructure.LngOutfall = null;
                                }
                            }
                        }
                        break;
                    case "textBoxMunicipality":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (ContactTVItemID > 0)
                                {
                                    if ("" + CurrentContact.ContactAddress.Municipality == tb.Text)
                                    {
                                        CurrentContact.ContactAddressNew.Municipality = null;
                                    }
                                    else
                                    {
                                        CurrentContact.ContactAddressNew.AddressTVItemID = 20000000;
                                        if (!string.IsNullOrWhiteSpace(tb.Text))
                                        {
                                            if (tb.Text.Trim().Length > 200)
                                            {
                                                MessageBox.Show("Municipality maximum length is 200 characters", "Error");
                                            }
                                            CurrentContact.ContactAddressNew.Municipality = tb.Text.Trim();
                                        }
                                        else
                                        {
                                            CurrentContact.ContactAddressNew.Municipality = null;
                                            CurrentContact.ContactAddress.Municipality = null;
                                        }
                                        SaveRestOfAddressNewContact();
                                        IsDirty = true;
                                    }
                                }
                                else
                                {
                                    if ("" + CurrentInfrastructure.InfrastructureAddress.Municipality == tb.Text)
                                    {
                                        CurrentInfrastructure.InfrastructureAddressNew.Municipality = null;
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                        if (!string.IsNullOrWhiteSpace(tb.Text))
                                        {
                                            if (tb.Text.Trim().Length > 200)
                                            {
                                                MessageBox.Show("Municipality maximum length is 200 characters", "Error");
                                            }
                                            CurrentInfrastructure.InfrastructureAddressNew.Municipality = tb.Text.Trim();
                                        }
                                        else
                                        {
                                            CurrentInfrastructure.InfrastructureAddressNew.Municipality = null;
                                            CurrentInfrastructure.InfrastructureAddress.Municipality = null;
                                        }
                                        SaveRestOfAddressNewInfrastructure();
                                        IsDirty = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "textBoxStreetType":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                string StreetTypeText = "";
                                StreetTypeEnum streetType = StreetTypeEnum.Error;

                                if (tb.Text != null)
                                {
                                    StreetTypeText = tb.Text.Trim();
                                    for (int i = 1, count = Enum.GetNames(typeof(StreetTypeEnum)).Length; i < count; i++)
                                    {
                                        if (StreetTypeText == ((StreetTypeEnum)i).ToString())
                                        {
                                            streetType = (StreetTypeEnum)i;
                                        }
                                    }
                                }

                                if (ContactTVItemID > 0)
                                {
                                    if (CurrentContact.ContactAddress.StreetType != null && (StreetTypeEnum)CurrentContact.ContactAddress.StreetType == streetType)
                                    {
                                        CurrentContact.ContactAddressNew.StreetType = null;
                                    }
                                    else
                                    {
                                        CurrentContact.ContactAddressNew.AddressTVItemID = 20000000;
                                        if (!string.IsNullOrWhiteSpace(tb.Text))
                                        {
                                            CurrentContact.ContactAddressNew.StreetType = (int)streetType;
                                        }
                                        else
                                        {
                                            CurrentContact.ContactAddressNew.StreetType = null;
                                            CurrentContact.ContactAddress.StreetType = null;
                                        }
                                        SaveRestOfAddressNewContact();
                                        IsDirty = true;
                                    }
                                }
                                else
                                {
                                    if (CurrentInfrastructure.InfrastructureAddress.StreetType != null && (StreetTypeEnum)CurrentInfrastructure.InfrastructureAddress.StreetType == streetType)
                                    {
                                        CurrentInfrastructure.InfrastructureAddressNew.StreetType = null;
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                        if (!string.IsNullOrWhiteSpace(tb.Text))
                                        {
                                            CurrentInfrastructure.InfrastructureAddressNew.StreetType = (int)streetType;
                                        }
                                        else
                                        {
                                            CurrentInfrastructure.InfrastructureAddressNew.StreetType = null;
                                            CurrentInfrastructure.InfrastructureAddress.StreetType = null;
                                        }
                                        SaveRestOfAddressNewInfrastructure();
                                        IsDirty = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "textBoxStreetName":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (ContactTVItemID > 0)
                                {
                                    if ("" + CurrentContact.ContactAddress.StreetName == tb.Text)
                                    {
                                        CurrentContact.ContactAddressNew.StreetName = null;
                                    }
                                    else
                                    {
                                        CurrentContact.ContactAddressNew.AddressTVItemID = 20000000;
                                        if (!string.IsNullOrWhiteSpace(tb.Text))
                                        {
                                            if (tb.Text.Trim().Length > 200)
                                            {
                                                MessageBox.Show("Street Name maximum length is 200 characters", "Error");
                                            }
                                            CurrentContact.ContactAddressNew.StreetName = tb.Text.Trim();
                                        }
                                        else
                                        {
                                            CurrentContact.ContactAddressNew.StreetName = null;
                                            CurrentContact.ContactAddress.StreetName = null;
                                        }
                                        SaveRestOfAddressNewContact();
                                        IsDirty = true;
                                    }
                                }
                                else
                                {
                                    if ("" + CurrentInfrastructure.InfrastructureAddress.StreetName == tb.Text)
                                    {
                                        CurrentInfrastructure.InfrastructureAddressNew.StreetName = null;
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                        if (!string.IsNullOrWhiteSpace(tb.Text))
                                        {
                                            if (tb.Text.Trim().Length > 200)
                                            {
                                                MessageBox.Show("Street Name maximum length is 200 characters", "Error");
                                            }
                                            CurrentInfrastructure.InfrastructureAddressNew.StreetName = tb.Text.Trim();
                                        }
                                        else
                                        {
                                            CurrentInfrastructure.InfrastructureAddressNew.StreetName = null;
                                            CurrentInfrastructure.InfrastructureAddress.StreetName = null;
                                        }
                                        SaveRestOfAddressNewInfrastructure();
                                        IsDirty = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "textBoxStreetNumber":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (ContactTVItemID > 0)
                                {
                                    if ("" + CurrentContact.ContactAddress.StreetNumber == tb.Text)
                                    {
                                        CurrentContact.ContactAddressNew.StreetNumber = null;
                                    }
                                    else
                                    {
                                        CurrentContact.ContactAddressNew.AddressTVItemID = 20000000;
                                        if (!string.IsNullOrWhiteSpace(tb.Text))
                                        {
                                            if (tb.Text.Trim().Length > 200)
                                            {
                                                MessageBox.Show("Street Number maximum length is 200 characters", "Error");
                                            }
                                            CurrentContact.ContactAddressNew.StreetNumber = tb.Text.Trim();
                                        }
                                        else
                                        {
                                            CurrentContact.ContactAddressNew.StreetNumber = null;
                                            CurrentContact.ContactAddressNew.StreetNumber = null;
                                        }
                                        SaveRestOfAddressNewContact();
                                        IsDirty = true;
                                    }
                                }
                                else
                                {
                                    if ("" + CurrentInfrastructure.InfrastructureAddress.StreetNumber == tb.Text)
                                    {
                                        CurrentInfrastructure.InfrastructureAddressNew.StreetNumber = null;
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.InfrastructureAddressNew.AddressTVItemID = 10000000;
                                        if (!string.IsNullOrWhiteSpace(tb.Text))
                                        {
                                            if (tb.Text.Trim().Length > 200)
                                            {
                                                MessageBox.Show("Street Number maximum length is 200 characters", "Error");
                                            }
                                            CurrentInfrastructure.InfrastructureAddressNew.StreetNumber = tb.Text.Trim();
                                        }
                                        else
                                        {
                                            CurrentInfrastructure.InfrastructureAddressNew.StreetNumber = null;
                                            CurrentInfrastructure.InfrastructureAddress.StreetNumber = null;
                                        }
                                        SaveRestOfAddressNewInfrastructure();
                                        IsDirty = true;
                                    }
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
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 200)
                                        {
                                            MessageBox.Show("Infrastructure Name maximum length is 200 characters", "Error");
                                        }
                                        bool Exist = false;
                                        foreach (Infrastructure inf in municipalityDoc.Municipality.InfrastructureList)
                                        {
                                            if (inf.TVTextNew != null)
                                            {
                                                if (inf.TVTextNew == tb.Text.Trim())
                                                {
                                                    if (inf.InfrastructureTVItemID != CurrentInfrastructure.InfrastructureTVItemID)
                                                    {
                                                        Exist = true;
                                                        MessageBox.Show("Infrastructure Name already exist", "Error");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (inf.TVText == tb.Text.Trim())
                                                {
                                                    if (inf.InfrastructureTVItemID != CurrentInfrastructure.InfrastructureTVItemID)
                                                    {
                                                        Exist = true;
                                                        MessageBox.Show("Infrastructure Name already exist", "Error");
                                                    }
                                                }
                                            }
                                        }

                                        if (!Exist)
                                        {
                                            CurrentInfrastructure.TVTextNew = tb.Text.Trim();
                                        }
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.TVTextNew = null;
                                        CurrentInfrastructure.TVText = null;
                                    }
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxFirstName":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    MessageBox.Show("First name is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else if ("" + CurrentContact.FirstName == tb.Text)
                                {
                                    CurrentContact.FirstNameNew = null;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 200)
                                        {
                                            MessageBox.Show("First Name maximum length is 200 characters", "Error");
                                            return;
                                        }
                                        CurrentContact.FirstNameNew = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentContact.FirstNameNew = null;
                                        CurrentContact.FirstName = null;
                                    }
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxInitial":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentContact.Initial == tb.Text)
                                {
                                    CurrentContact.InitialNew = null;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 50)
                                        {
                                            MessageBox.Show("Initial maximum length is 50 characters", "Error");
                                            return;
                                        }
                                        CurrentContact.InitialNew = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentContact.InitialNew = null;
                                        CurrentContact.Initial = null;
                                    }
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxLastName":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    MessageBox.Show("Last name is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else if ("" + CurrentContact.LastName == tb.Text)
                                {
                                    CurrentContact.LastNameNew = null;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 200)
                                        {
                                            MessageBox.Show("Last Name maximum length is 200 characters", "Error");
                                            return;
                                        }
                                        CurrentContact.LastNameNew = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentContact.LastNameNew = null;
                                        CurrentContact.LastName = null;
                                    }
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxEmail":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if (string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    MessageBox.Show("Email is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else if ("" + CurrentContact.Email == tb.Text)
                                {
                                    CurrentContact.EmailNew = null;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 200)
                                        {
                                            MessageBox.Show("Email maximum length is 200 characters", "Error");
                                            return;
                                        }
                                        CurrentContact.EmailNew = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentContact.EmailNew = null;
                                        CurrentContact.Email = null;
                                    }
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxTelNumber":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                string Tag = tb.Tag.ToString();
                                int item = int.Parse(Tag);

                                if (string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    MessageBox.Show("Telephone number is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else if ("" + CurrentContact.TelephoneList[item].TelNumber == tb.Text)
                                {
                                    CurrentContact.TelephoneList[item].TelNumberNew = null;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 50)
                                        {
                                            MessageBox.Show("Tel Number maximum length is 50 characters", "Error");
                                            return;
                                        }
                                        CurrentContact.TelephoneList[item].TelNumberNew = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentContact.TelephoneList[item].TelNumberNew = null;
                                        CurrentContact.TelephoneList[item].TelNumber = null;
                                    }
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxEmailAddress":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                string Tag = tb.Tag.ToString();
                                int item = int.Parse(Tag);

                                if (string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    MessageBox.Show("Email address is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else if ("" + CurrentContact.EmailList[item].EmailAddress == tb.Text)
                                {
                                    CurrentContact.EmailList[item].EmailAddressNew = null;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 100)
                                        {
                                            MessageBox.Show("Tel Number maximum length is 100 characters", "Error");
                                            return;
                                        }
                                        CurrentContact.EmailList[item].EmailAddressNew = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentContact.EmailList[item].EmailAddressNew = null;
                                        CurrentContact.EmailList[item].EmailAddress = null;
                                    }
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxCommentEN":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentInfrastructure.CommentEN == tb.Text)
                                {
                                    CurrentInfrastructure.CommentENNew = null;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 100000)
                                        {
                                            MessageBox.Show("Infrastructure Comment En maximum length is 100000 characters", "Error");
                                        }
                                        CurrentInfrastructure.CommentENNew = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.CommentENNew = null;
                                        CurrentInfrastructure.CommentEN = null;
                                    }
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxCommentFR":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                if ("" + CurrentInfrastructure.CommentFR == tb.Text)
                                {
                                    CurrentInfrastructure.CommentFRNew = null;
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 100000)
                                        {
                                            MessageBox.Show("Infrastructure Comment FR maximum length is 100000 characters", "Error");
                                        }
                                        CurrentInfrastructure.CommentFRNew = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.CommentFRNew = null;
                                        CurrentInfrastructure.CommentFR = null;
                                    }
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
                                    for (int i = 0, count = Enum.GetNames(typeof(InfrastructureTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
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
                                    for (int i = 0, count = Enum.GetNames(typeof(FacilityTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
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
                    case "checkBoxIsMechanicallyAerated":
                        {
                            CheckBox cb = (CheckBox)control;
                            if (cb != null)
                            {
                                if (cb.Checked)
                                {
                                    if (CurrentInfrastructure.IsMechanicallyAerated == CurrentInfrastructure.IsMechanicallyAeratedNew)
                                    {
                                        CurrentInfrastructure.IsMechanicallyAeratedNew = true;
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.IsMechanicallyAeratedNew = true;
                                    }
                                }
                                else
                                {
                                    if (CurrentInfrastructure.IsMechanicallyAerated == CurrentInfrastructure.IsMechanicallyAeratedNew)
                                    {
                                        CurrentInfrastructure.IsMechanicallyAeratedNew = false;
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.IsMechanicallyAeratedNew = false;
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
                                    for (int i = 0, count = Enum.GetNames(typeof(AerationTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
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
                                    for (int i = 0, count = Enum.GetNames(typeof(PreliminaryTreatmentTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
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
                                    for (int i = 0, count = Enum.GetNames(typeof(PrimaryTreatmentTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
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
                                    for (int i = 0, count = Enum.GetNames(typeof(SecondaryTreatmentTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
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
                                    for (int i = 0, count = Enum.GetNames(typeof(TertiaryTreatmentTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
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
                                    for (int i = 0, count = Enum.GetNames(typeof(DisinfectionTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
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
                                    for (int i = 0, count = Enum.GetNames(typeof(CollectionSystemTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
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
                                    for (int i = 0, count = Enum.GetNames(typeof(AlarmSystemTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
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
                    case "comboBoxCanOverflowType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.CanOverflowNew = null;
                                }
                                else
                                {
                                    for (int i = 0, count = Enum.GetNames(typeof(CanOverflowTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
                                        {
                                            if (CurrentInfrastructure.CanOverflow == i)
                                            {
                                                CurrentInfrastructure.CanOverflowNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.CanOverflowNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxValveType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                if (cb.SelectedItem == null)
                                {
                                    CurrentInfrastructure.ValveTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 0, count = Enum.GetNames(typeof(ValveTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
                                        {
                                            if (CurrentInfrastructure.ValveType == i)
                                            {
                                                CurrentInfrastructure.ValveTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentInfrastructure.ValveTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxTelType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                string TagText = cb.Tag.ToString();
                                int item = int.Parse(TagText.Substring(TagText.IndexOf("|") + 1));
                                if (cb.SelectedItem == null)
                                {
                                    CurrentContact.TelephoneList[item].TelTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 0, count = Enum.GetNames(typeof(TelTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
                                        {
                                            if (CurrentContact.TelephoneList[item].TelType == i)
                                            {
                                                CurrentContact.TelephoneList[item].TelTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentContact.TelephoneList[item].TelTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "comboBoxEmailType":
                        {
                            ComboBox cb = (ComboBox)control;
                            if (cb != null)
                            {
                                string TagText = cb.Tag.ToString();
                                int item = int.Parse(TagText.Substring(TagText.IndexOf("|") + 1));
                                if (cb.SelectedItem == null)
                                {
                                    CurrentContact.EmailList[item].EmailTypeNew = null;
                                }
                                else
                                {
                                    for (int i = 0, count = Enum.GetNames(typeof(EmailTypeEnum)).Count(); i < count; i++)
                                    {
                                        if (((EnumTextAndID)cb.SelectedItem).EnumID == i)
                                        {
                                            if (CurrentContact.EmailList[item].EmailType == i)
                                            {
                                                CurrentContact.EmailList[item].EmailTypeNew = null;
                                                IsDirty = true;
                                            }
                                            else
                                            {
                                                CurrentContact.EmailList[item].EmailTypeNew = i;
                                                IsDirty = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "textBoxNumberOfCells":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.NumberOfCells)
                                {
                                    CurrentInfrastructure.NumberOfCellsNew = null;
                                }
                                else
                                {
                                    if (TempInt < 0 || TempInt > 20)
                                    {
                                        MessageBox.Show("Number Of Cells should be between 0 and 20", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.NumberOfCellsNew = TempInt;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.NumberOfCellsNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.NumberOfCells.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Number of Cells"));
                                }
                                else
                                {
                                    CurrentInfrastructure.NumberOfCells = null;
                                }
                            }
                        }
                        break;
                    case "textBoxNumberOfAeratedCells":
                        {
                            TextBox tb = (TextBox)control;

                            if (int.TryParse(tb.Text, out int TempInt))
                            {
                                if (TempInt == CurrentInfrastructure.NumberOfAeratedCells)
                                {
                                    CurrentInfrastructure.NumberOfAeratedCellsNew = null;
                                }
                                else
                                {
                                    if (TempInt < 0 || TempInt > 20)
                                    {
                                        MessageBox.Show("Number Of Aerated Cells should be between 0 and 20", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.NumberOfAeratedCellsNew = TempInt;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.NumberOfAeratedCellsNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.NumberOfAeratedCells.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Number of Aerated Cells"));
                                }
                                else
                                {
                                    CurrentInfrastructure.NumberOfAeratedCells = null;
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
                                    if (TempFloat < 0 || TempFloat > 10000000)
                                    {
                                        MessageBox.Show("Design Flow (m3/day) should be between 0 and 10000000", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.DesignFlow_m3_dayNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.DesignFlow_m3_dayNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.DesignFlow_m3_day.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Design Flow (m3/day)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.DesignFlow_m3_day = null;
                                }
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
                                    if (TempFloat < 0 || TempFloat > 10000000)
                                    {
                                        MessageBox.Show("Average Flow (m3/day) should be between 0 and 10000000", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.AverageFlow_m3_dayNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.AverageFlow_m3_dayNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.AverageFlow_m3_day.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Average Flow (m3/day)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.AverageFlow_m3_day = null;
                                }
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
                                    if (TempFloat < 0 || TempFloat > 10000000)
                                    {
                                        MessageBox.Show("Peak Flow (m3/day) should be between 0 and 10000000", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.PeakFlow_m3_dayNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PeakFlow_m3_dayNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.PeakFlow_m3_day.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Peak Flow (m3/day)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.PeakFlow_m3_day = null;
                                }
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
                                    if (TempInt < 0 || TempInt > 10000000)
                                    {
                                        MessageBox.Show("Population Served should be between 0 and 10000000", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.PopServedNew = TempInt;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PopServedNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.PopServed.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Population Served"));
                                }
                                else
                                {
                                    CurrentInfrastructure.PopServed = null;
                                }
                            }
                        }
                        break;
                    case "checkBoxHasBackupPower":
                        {
                            CheckBox cb = (CheckBox)control;
                            if (cb != null)
                            {
                                if (cb.Checked)
                                {
                                    if (CurrentInfrastructure.HasBackupPower == CurrentInfrastructure.HasBackupPowerNew)
                                    {
                                        CurrentInfrastructure.HasBackupPowerNew = true;
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.HasBackupPowerNew = true;
                                    }
                                }
                                else
                                {
                                    if (CurrentInfrastructure.HasBackupPower == CurrentInfrastructure.HasBackupPowerNew)
                                    {
                                        CurrentInfrastructure.HasBackupPowerNew = false;
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.HasBackupPowerNew = false;
                                    }
                                }
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
                                    if (TempFloat < 0 || TempFloat > 100)
                                    {
                                        MessageBox.Show("Percent of total flow should be between 0 and 100", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.PercFlowOfTotalNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PercFlowOfTotalNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.PercFlowOfTotal.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Percent of total flow"));
                                }
                                else
                                {
                                    CurrentInfrastructure.PercFlowOfTotal = null;
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
                                    if (TempFloat < 0 || TempFloat > 10000)
                                    {
                                        MessageBox.Show("Average Depth (m) should be between 0 and 10000", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.AverageDepth_mNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.AverageDepth_mNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.AverageDepth_m.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Average Depth (m)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.AverageDepth_m = null;
                                }
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
                                    if (TempInt < 0 || TempInt > 100)
                                    {
                                        MessageBox.Show("Number of Port should be between 0 and 10000", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.NumberOfPortsNew = TempInt;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.NumberOfPortsNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.NumberOfPorts.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Number of Ports"));
                                }
                                else
                                {
                                    CurrentInfrastructure.NumberOfPorts = null;
                                }
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
                                    if (TempFloat < 0 || TempFloat > 10)
                                    {
                                        MessageBox.Show("Port Diameter (m) should be between 0 and 10", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.PortDiameter_mNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PortDiameter_mNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.PortDiameter_m.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Port Diameter (m)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.PortDiameter_m = null;
                                }
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
                                    if (TempFloat < 0 || TempFloat > 1000)
                                    {
                                        MessageBox.Show("Port Spacing (m) should be between 0 and 1000", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.PortSpacing_mNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PortSpacing_mNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.PortSpacing_m.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Port Spacing (m)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.PortSpacing_m = null;
                                }
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
                                    if (TempFloat < 0 || TempFloat > 10000)
                                    {
                                        MessageBox.Show("Port Elevation (m) should be between 0 and 10000", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.PortElevation_mNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PortElevation_mNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.PortElevation_m.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Port Elevation (m)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.PortElevation_m = null;
                                }
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
                                    if (TempFloat < -90 || TempFloat > 90)
                                    {
                                        MessageBox.Show("Vertical Angle (deg) should be between -90 and 90", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.VerticalAngle_degNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.VerticalAngle_degNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.VerticalAngle_deg.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Vertical Angle (deg)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.VerticalAngle_deg = null;
                                }
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
                                    if (TempFloat < -180 || TempFloat > 180)
                                    {
                                        MessageBox.Show("Horizontal Angle (deg) should be between -180 and 180", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.HorizontalAngle_degNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.HorizontalAngle_degNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.HorizontalAngle_deg.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Horizontal Angle (deg)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.HorizontalAngle_deg = null;
                                }
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
                                    if (TempFloat < 0 || TempFloat > 100)
                                    {
                                        MessageBox.Show("Decay Rate (/day) should be between 0 and 100", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.DecayRate_per_dayNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.DecayRate_per_dayNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.DecayRate_per_day.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Decay Rate (/day)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.DecayRate_per_day = null;
                                }
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
                                    if (TempFloat < 0 || TempFloat > 25)
                                    {
                                        MessageBox.Show("Near Field Velocity (m/s) should be between 0 and 25", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.NearFieldVelocity_m_sNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.NearFieldVelocity_m_sNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.NearFieldVelocity_m_s.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Near Field Velocity (m/s)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.NearFieldVelocity_m_s = null;
                                }
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
                                    if (TempFloat < 0 || TempFloat > 25)
                                    {
                                        MessageBox.Show("Far Field Velocity (m/s) should be between 0 and 25", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.FarFieldVelocity_m_sNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.FarFieldVelocity_m_sNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.FarFieldVelocity_m_s.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Far Field Velocity (m/s)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.FarFieldVelocity_m_s = null;
                                }
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
                                    if (TempFloat < 0 || TempFloat > 40)
                                    {
                                        MessageBox.Show("Receiving Water Salinity (PSU) should be between 0 and 40", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.ReceivingWaterSalinity_PSUNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.ReceivingWaterSalinity_PSUNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.ReceivingWaterSalinity_PSU.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Receiving Water Salinity (PSU)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.ReceivingWaterSalinity_PSU = null;
                                }
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
                                    if (TempFloat < -10 || TempFloat > 40)
                                    {
                                        MessageBox.Show("Receiving Water Temperature (˚C) should be between -10 and 40", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.ReceivingWaterTemperature_CNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.ReceivingWaterTemperature_CNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.ReceivingWaterTemperature_C.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Receiving Water Temperature (˚C)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.ReceivingWaterTemperature_C = null;
                                }
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
                                    if (TempInt < 0 || TempInt > 20000000)
                                    {
                                        MessageBox.Show("Receiving Water (MPN / 100 mL) should be between 0 and 20000000", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew = TempInt;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.ReceivingWater_MPN_per_100mlNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.ReceivingWater_MPN_per_100ml.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Receiving Water (MPN / 100 mL)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.ReceivingWater_MPN_per_100ml = null;
                                }
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
                                    if (TempFloat < 0 || TempFloat > 2000)
                                    {
                                        MessageBox.Show("Distance from Shore (m) should be between 0 and 2000", "Error");
                                    }
                                    else
                                    {
                                        CurrentInfrastructure.DistanceFromShore_mNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.DistanceFromShore_mNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.DistanceFromShore_m.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Distance from Shore (m)"));
                                }
                                else
                                {
                                    CurrentInfrastructure.DistanceFromShore_m = null;
                                }
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
                                    if (CurrentInfrastructure.InfrastructureTVItemID == TempInt)
                                    {
                                        MessageBox.Show("Cannot pump to itself", "Erorr");
                                        return;
                                    }
                                    else
                                    {
                                        bool Exist = false;
                                        foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList)
                                        {
                                            if (infrastructure.InfrastructureTVItemID == TempInt)
                                            {
                                                Exist = true;
                                            }
                                        }

                                        if (!Exist)
                                        {
                                            MessageBox.Show("Cannot find the WWTP or LS that it is suppose to pump to", "Erorr");
                                            return;
                                        }

                                        foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList)
                                        {
                                            foreach (Infrastructure infrastructure2 in municipalityDoc.Municipality.InfrastructureList)
                                            {
                                                if (infrastructure2.PumpsToTVItemID == infrastructure.InfrastructureTVItemID)
                                                {
                                                    infrastructure.PumpsFromTVItemIDList.Add(infrastructure2.InfrastructureTVItemID);
                                                }
                                            }
                                        }

                                        IsTryingToMoveUnderItself = false;
                                        GetFromTVItemID(CurrentInfrastructure, TempInt);

                                        if (IsTryingToMoveUnderItself)
                                        {
                                            MessageBox.Show("Cannot pump under itself", "Erorr");
                                            return;
                                        }

                                        CurrentInfrastructure.PumpsToTVItemIDNew = TempInt;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentInfrastructure.PumpsToTVItemIDNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentInfrastructure.PumpsToTVItemID.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Pumps to TVItemID"));
                                }
                                else
                                {
                                    CurrentInfrastructure.PumpsToTVItemID = null;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            if (ContactTVItemID > 0)
            {
                if (CurrentContact.ContactAddressNew.StreetNumber != null
                    || CurrentContact.ContactAddressNew.StreetName != null
                    || CurrentContact.ContactAddressNew.StreetType != null
                    || CurrentContact.ContactAddressNew.Municipality != null
                    || CurrentContact.ContactAddressNew.PostalCode != null)
                {
                    SaveRestOfAddressNewContact();
                }
            }
            else
            {
                if (CurrentInfrastructure.InfrastructureAddressNew.StreetNumber != null
                    || CurrentInfrastructure.InfrastructureAddressNew.StreetName != null
                    || CurrentInfrastructure.InfrastructureAddressNew.StreetType != null
                    || CurrentInfrastructure.InfrastructureAddressNew.Municipality != null
                    || CurrentInfrastructure.InfrastructureAddressNew.PostalCode != null)
                {
                    SaveRestOfAddressNewInfrastructure();
                }
            }

            SaveMunicipalityTextFile();

            IsDirty = false;
            PanelShowInputOptions.BackColor = BackColorDefault;
            PanelSubsectorOrMunicipality.Enabled = true;
        }

        private void GetFromTVItemID(Infrastructure currentInfrastructure, int OriginalTVItemID)
        {
            foreach (int FromTVItemID in currentInfrastructure.PumpsFromTVItemIDList)
            {
                if (FromTVItemID == OriginalTVItemID)
                {
                    IsTryingToMoveUnderItself = true;
                }
                foreach (Infrastructure infrastructure in municipalityDoc.Municipality.InfrastructureList)
                {
                    if (infrastructure.InfrastructureTVItemID == FromTVItemID)
                    {
                        GetFromTVItemID(infrastructure, OriginalTVItemID);
                    }
                }

            }
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
                                                SaveRestOfAddressNew();
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
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 200)
                                        {
                                            MessageBox.Show("Postal Code maximum length is 200 characters", "Error");
                                        }
                                        CurrentPSS.PSSAddressNew.PostalCode = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.PostalCode = null;
                                        CurrentPSS.PSSAddress.PostalCode = null;
                                    }
                                    SaveRestOfAddressNew();
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
                                    if (TempFloat < -90.0f || TempFloat > 90.0f)
                                    {
                                        MessageBox.Show("Lat should be between -90 and 90", "Error");
                                    }
                                    else
                                    {
                                        CurrentPSS.LatNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentPSS.LatNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentPSS.Lat.ToString();
                                    MessageBox.Show("Please enter a valid number for Latitute", "Error");
                                }
                                else
                                {
                                    CurrentPSS.Lat = null;
                                }
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
                                    if (TempFloat < -180.0f || TempFloat > 180.0f)
                                    {
                                        MessageBox.Show("Longitude should be between -180 and 180", "Error");
                                    }
                                    else
                                    {
                                        CurrentPSS.LngNew = TempFloat;
                                        IsDirty = true;
                                    }
                                }
                            }
                            else
                            {
                                CurrentPSS.LngNew = null;
                                if (!string.IsNullOrWhiteSpace(tb.Text))
                                {
                                    tb.Text = CurrentPSS.Lng.ToString();
                                    EmitStatus(new StatusEventArgs("Please enter a valid number for Longitude"));
                                }
                                else
                                {
                                    CurrentPSS.Lng = null;
                                }
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
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 200)
                                        {
                                            MessageBox.Show("Municipality maximum length is 200 characters", "Error");
                                        }
                                        CurrentPSS.PSSAddressNew.Municipality = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.Municipality = null;
                                        CurrentPSS.PSSAddress.Municipality = null;
                                    }
                                    SaveRestOfAddressNew();
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "textBoxStreetType":
                        {
                            TextBox tb = (TextBox)control;
                            if (tb != null)
                            {
                                string StreetTypeText = "";
                                StreetTypeEnum streetType = StreetTypeEnum.Error;

                                if (tb.Text != null)
                                {
                                    StreetTypeText = tb.Text.Trim();
                                    for (int i = 1, count = Enum.GetNames(typeof(StreetTypeEnum)).Length; i < count; i++)
                                    {
                                        if (StreetTypeText == ((StreetTypeEnum)i).ToString())
                                        {
                                            streetType = (StreetTypeEnum)i;
                                        }
                                    }
                                }

                                if (CurrentPSS.PSSAddress.StreetType != null && (StreetTypeEnum)CurrentPSS.PSSAddress.StreetType == streetType)
                                {
                                    CurrentPSS.PSSAddressNew.StreetType = null;
                                }
                                else
                                {
                                    CurrentPSS.PSSAddressNew.AddressTVItemID = 10000000;
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        CurrentPSS.PSSAddressNew.StreetType = (int)streetType;
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.StreetType = null;
                                        CurrentPSS.PSSAddress.StreetType = null;
                                    }
                                    SaveRestOfAddressNew();
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
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 200)
                                        {
                                            MessageBox.Show("Street Name maximum length is 200 characters", "Error");
                                        }
                                        CurrentPSS.PSSAddressNew.StreetName = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.StreetName = null;
                                        CurrentPSS.PSSAddress.StreetName = null;
                                    }
                                    SaveRestOfAddressNew();
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
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 200)
                                        {
                                            MessageBox.Show("Street Number maximum length is 200 characters", "Error");
                                        }
                                        CurrentPSS.PSSAddressNew.StreetNumber = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentPSS.PSSAddressNew.StreetNumber = null;
                                        CurrentPSS.PSSAddress.StreetNumber = null;
                                    }
                                    SaveRestOfAddressNew();
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
                                    if (!string.IsNullOrWhiteSpace(tb.Text))
                                    {
                                        if (tb.Text.Trim().Length > 200)
                                        {
                                            MessageBox.Show("Pollution Source Site Name maximum length is 200 characters", "Error");
                                        }
                                        CurrentPSS.TVTextNew = tb.Text.Trim();
                                    }
                                    else
                                    {
                                        CurrentPSS.TVTextNew = null;
                                        CurrentPSS.TVText = null;
                                    }
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    case "dateTimePickerObsDate":
                        {
                            DateTimePicker dtp = (DateTimePicker)control;
                            if (dtp != null)
                            {
                                if (CurrentPSS.PSSObs.ObsDate == dtp.Value)
                                {
                                    CurrentPSS.PSSObs.ObsDateNew = null;
                                }
                                else
                                {
                                    CurrentPSS.PSSObs.ObsDateNew = dtp.Value;
                                    IsDirty = true;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            if (CurrentPSS.PSSAddressNew.StreetNumber != null
                || CurrentPSS.PSSAddressNew.StreetName != null
                || CurrentPSS.PSSAddressNew.StreetType != null
                || CurrentPSS.PSSAddressNew.Municipality != null
                || CurrentPSS.PSSAddressNew.PostalCode != null)
            {
                SaveRestOfAddressNew();
            }

            SaveSubsectorTextFile();

            IsDirty = false;
            PanelShowInputOptions.BackColor = BackColorDefault;
            PanelSubsectorOrMunicipality.Enabled = true;
        }
        public void SaveRestOfAddressNew()
        {
            if (CurrentPSS.PSSAddressNew.StreetNumber == null)
            {
                CurrentPSS.PSSAddressNew.StreetNumber = CurrentPSS.PSSAddress.StreetNumber;
            }
            if (CurrentPSS.PSSAddressNew.StreetName == null)
            {
                CurrentPSS.PSSAddressNew.StreetName = CurrentPSS.PSSAddress.StreetName;
            }
            if (CurrentPSS.PSSAddressNew.StreetType == null)
            {
                CurrentPSS.PSSAddressNew.StreetType = CurrentPSS.PSSAddress.StreetType;
            }
            if (CurrentPSS.PSSAddressNew.Municipality == null)
            {
                CurrentPSS.PSSAddressNew.Municipality = CurrentPSS.PSSAddress.Municipality;
            }
            if (CurrentPSS.PSSAddressNew.PostalCode == null)
            {
                CurrentPSS.PSSAddressNew.PostalCode = CurrentPSS.PSSAddress.PostalCode;
            }
        }
        public void SaveRestOfAddressNewContact()
        {
            if (CurrentContact.ContactAddressNew.StreetNumber == null)
            {
                CurrentContact.ContactAddressNew.StreetNumber = CurrentContact.ContactAddress.StreetNumber;
            }
            if (CurrentContact.ContactAddressNew.StreetName == null)
            {
                CurrentContact.ContactAddressNew.StreetName = CurrentContact.ContactAddress.StreetName;
            }
            if (CurrentContact.ContactAddressNew.StreetType == null)
            {
                CurrentContact.ContactAddressNew.StreetType = CurrentContact.ContactAddress.StreetType;
            }
            if (CurrentContact.ContactAddressNew.Municipality == null)
            {
                CurrentContact.ContactAddressNew.Municipality = CurrentContact.ContactAddress.Municipality;
            }
            if (CurrentContact.ContactAddressNew.PostalCode == null)
            {
                CurrentContact.ContactAddressNew.PostalCode = CurrentContact.ContactAddress.PostalCode;
            }
        }
        public void SaveRestOfAddressNewInfrastructure()
        {
            if (CurrentInfrastructure.InfrastructureAddressNew.StreetNumber == null)
            {
                CurrentInfrastructure.InfrastructureAddressNew.StreetNumber = CurrentInfrastructure.InfrastructureAddress.StreetNumber;
            }
            if (CurrentInfrastructure.InfrastructureAddressNew.StreetName == null)
            {
                CurrentInfrastructure.InfrastructureAddressNew.StreetName = CurrentInfrastructure.InfrastructureAddress.StreetName;
            }
            if (CurrentInfrastructure.InfrastructureAddressNew.StreetType == null)
            {
                CurrentInfrastructure.InfrastructureAddressNew.StreetType = CurrentInfrastructure.InfrastructureAddress.StreetType;
            }
            if (CurrentInfrastructure.InfrastructureAddressNew.Municipality == null)
            {
                CurrentInfrastructure.InfrastructureAddressNew.Municipality = CurrentInfrastructure.InfrastructureAddress.Municipality;
            }
            if (CurrentInfrastructure.InfrastructureAddressNew.PostalCode == null)
            {
                CurrentInfrastructure.InfrastructureAddressNew.PostalCode = CurrentInfrastructure.InfrastructureAddress.PostalCode;
            }
        }
        private string SaveToCSSPWebToolsAddress(int ProvinceTVItemID, int TVItemID, string StreetNumber, string StreetName, int StreetType, string Municipality, string PostalCode, bool CreateMunicipality, bool IsPSS, bool IsInfrastructure, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("ProvinceTVItemID", ProvinceTVItemID.ToString());
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("StreetNumber", StreetNumber);
                paramList.Add("StreetName", StreetName);
                paramList.Add("StreetType", StreetType.ToString());
                paramList.Add("Municipality", Municipality);
                paramList.Add("PostalCode", PostalCode);
                paramList.Add("CreateMunicipality", CreateMunicipality.ToString());
                paramList.Add("IsPSS", IsPSS.ToString());
                paramList.Add("IsInfrastructure", IsInfrastructure.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}SaveAddressJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}SaveAddressJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }
        }
        private string SaveToCSSPWebToolsCreateOrModifyEmail(int ContactTVItemID, int EmailTVItemID, int? EmailType, string EmailAddress, bool ShouldDelete, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("ContactTVItemID", ContactTVItemID.ToString());
                paramList.Add("EmailTVItemID", EmailTVItemID.ToString());
                paramList.Add("EmailType", EmailType == null ? "" : EmailType.ToString());
                paramList.Add("EmailAddress", EmailAddress);
                paramList.Add("ShouldDelete", ShouldDelete.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}CreateOrModifyEmailJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}CreateOrModifyEmailJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsCreateOrModifyTel(int ContactTVItemID, int TelTVItemID, int? TelType, string TelNumber, bool ShouldDelete, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("ContactTVItemID", ContactTVItemID.ToString());
                paramList.Add("TelTVItemID", TelTVItemID.ToString());
                paramList.Add("TelType", TelType == null ? "" : TelType.ToString());
                paramList.Add("TelNumber", TelNumber);
                paramList.Add("ShouldDelete", ShouldDelete.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}CreateOrModifyTelephoneJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}CreateOrModifyTelephoneJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsCreateOrModifyContact(int MunicipalityTVItemID, int ContactTVItemID, string FirstName, string Initial,
            string LastName, string Email, ContactTitleEnum? Title, bool IsActive, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("MunicipalityTVItemID", MunicipalityTVItemID.ToString());
                paramList.Add("ContactTVItemID", ContactTVItemID.ToString());
                paramList.Add("FirstName", FirstName);
                paramList.Add("Initial", Initial);
                paramList.Add("LastName", LastName);
                paramList.Add("Email", Email);
                paramList.Add("Title", Title == null ? "" : ((int)Title).ToString());
                paramList.Add("IsActive", IsActive.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}CreateOrModifyContactJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}CreateOrModifyContactJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsCreateOrModifyInfrastructure(int MunicipalityTVItemID, int TVItemID, string TVText, bool IsActive,
            float? Lat, float? Lng, float? LatOutfall, float? LngOutfall, string CommentEN, string CommentFR, InfrastructureTypeEnum? InfrastructureType,
            FacilityTypeEnum? FacilityType, bool? IsMechanicallyAerated, int? NumberOfCells, int? NumberOfAeratedCells, AerationTypeEnum? AerationType,
            PreliminaryTreatmentTypeEnum? PreliminaryTreatmentType, PrimaryTreatmentTypeEnum? PrimaryTreatmentType,
            SecondaryTreatmentTypeEnum? SecondaryTreatmentType, TertiaryTreatmentTypeEnum? TertiaryTreatmentType,
            DisinfectionTypeEnum? DisinfectionType, CollectionSystemTypeEnum? CollectionSystemType, AlarmSystemTypeEnum? AlarmSystemType,
            float? DesignFlow_m3_day, float? AverageFlow_m3_day, float? PeakFlow_m3_day, int? PopServed,
            CanOverflowTypeEnum? CanOverflow, ValveTypeEnum? ValveType, bool? HasBackupPower,
            float? PercFlowOfTotal, float? AverageDepth_m, int? NumberOfPorts,
            float? PortDiameter_m, float? PortSpacing_m, float? PortElevation_m, float? VerticalAngle_deg, float? HorizontalAngle_deg,
            float? DecayRate_per_day, float? NearFieldVelocity_m_s, float? FarFieldVelocity_m_s, float? ReceivingWaterSalinity_PSU,
            float? ReceivingWaterTemperature_C, int? ReceivingWater_MPN_per_100ml, float? DistanceFromShore_m,
            int? SeeOtherMunicipalityTVItemID, string SeeOtherMunicipalityText, int? PumpsToTVItemID, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("MunicipalityTVItemID", MunicipalityTVItemID.ToString());
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("TVText", TVText);
                paramList.Add("IsActive", IsActive.ToString());
                paramList.Add("Lat", Lat.ToString());
                paramList.Add("Lng", Lng.ToString());
                paramList.Add("LatOutfall", LatOutfall.ToString());
                paramList.Add("LngOutfall", LngOutfall.ToString());
                paramList.Add("CommentEN", CommentEN);
                paramList.Add("CommentFR", CommentFR);
                paramList.Add("InfrastructureType", ((int?)InfrastructureType).ToString());
                paramList.Add("FacilityType", ((int?)FacilityType).ToString());
                paramList.Add("IsMechanicallyAerated", IsMechanicallyAerated.ToString());
                paramList.Add("NumberOfCells", NumberOfCells.ToString());
                paramList.Add("NumberOfAeratedCells", NumberOfAeratedCells.ToString());
                paramList.Add("AerationType", ((int?)AerationType).ToString());
                paramList.Add("PreliminaryTreatmentType", ((int?)PreliminaryTreatmentType).ToString());
                paramList.Add("PrimaryTreatmentType", ((int?)PrimaryTreatmentType).ToString());
                paramList.Add("SecondaryTreatmentType", ((int?)SecondaryTreatmentType).ToString());
                paramList.Add("TertiaryTreatmentType", ((int?)TertiaryTreatmentType).ToString());
                paramList.Add("DisinfectionType", ((int?)DisinfectionType).ToString());
                paramList.Add("CollectionSystemType", ((int?)CollectionSystemType).ToString());
                paramList.Add("AlarmSystemType", ((int?)AlarmSystemType).ToString());
                paramList.Add("DesignFlow_m3_day", DesignFlow_m3_day.ToString());
                paramList.Add("AverageFlow_m3_day", AverageFlow_m3_day.ToString());
                paramList.Add("PeakFlow_m3_day", PeakFlow_m3_day.ToString());
                paramList.Add("PopServed", PopServed.ToString());
                paramList.Add("CanOverflow", ((int?)CanOverflow).ToString());
                paramList.Add("ValveType", ((int?)ValveType).ToString());
                paramList.Add("HasBackupPower", HasBackupPower.ToString());
                paramList.Add("PercFlowOfTotal", PercFlowOfTotal.ToString());
                paramList.Add("AverageDepth_m", AverageDepth_m.ToString());
                paramList.Add("NumberOfPorts", NumberOfPorts.ToString());
                paramList.Add("PortDiameter_m", PortDiameter_m.ToString());
                paramList.Add("PortSpacing_m", PortSpacing_m.ToString());
                paramList.Add("PortElevation_m", PortElevation_m.ToString());
                paramList.Add("VerticalAngle_deg", VerticalAngle_deg.ToString());
                paramList.Add("HorizontalAngle_deg", HorizontalAngle_deg.ToString());
                paramList.Add("DecayRate_per_day", DecayRate_per_day.ToString());
                paramList.Add("NearFieldVelocity_m_s", NearFieldVelocity_m_s.ToString());
                paramList.Add("FarFieldVelocity_m_s", FarFieldVelocity_m_s.ToString());
                paramList.Add("ReceivingWaterSalinity_PSU", ReceivingWaterSalinity_PSU.ToString());
                paramList.Add("ReceivingWaterTemperature_C", ReceivingWaterTemperature_C.ToString());
                paramList.Add("ReceivingWater_MPN_per_100ml", ReceivingWater_MPN_per_100ml.ToString());
                paramList.Add("DistanceFromShore_m", DistanceFromShore_m.ToString());
                paramList.Add("SeeOtherMunicipalityTVItemID", SeeOtherMunicipalityTVItemID.ToString());
                paramList.Add("SeeOtherMunicipalityText", SeeOtherMunicipalityText.ToString());
                paramList.Add("PumpsToTVItemID", PumpsToTVItemID.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}CreateOrModifyInfrastructureJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}CreateOrModifyInfrastructureJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsCreateNewPSS(int SubsectorTVItemID, int TVItemID, string TVText, int SiteNumber, float Lat, float Lng, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("SubsectorTVItemID", SubsectorTVItemID.ToString());
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("TVText", TVText);
                paramList.Add("SiteNumber", SiteNumber.ToString());
                paramList.Add("Lat", Lat.ToString());
                paramList.Add("Lng", Lng.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}CreateNewPollutionSourceSiteJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}CreateNewPollutionSourceSiteJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsCreateNewObsDate(int PSSTVItemID, DateTime NewObsDate, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("PSSTVItemID", PSSTVItemID.ToString());
                paramList.Add("NewObsDate", NewObsDate.ToString("yyyy-MM-dd"));
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}CreateNewObsDateJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}CreateNewObsDateJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsIssue(int ObsID, int IssueID, int Ordinal, string ObservationInfo, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("ObsID", ObsID.ToString());
                paramList.Add("IssueID", IssueID.ToString());
                paramList.Add("Ordinal", Ordinal.ToString());
                paramList.Add("ObservationInfo", ObservationInfo);
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}SavePSSObsIssueJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}SavePSSObsIssueJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsIssueExtraComment(int ObsID, int IssueID, string ExtraComment, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("ObsID", ObsID.ToString());
                paramList.Add("IssueID", IssueID.ToString());
                paramList.Add("ExtraComment", ExtraComment.Replace("\r\n", "|||"));
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}SavePSSObsIssueExtraCommentJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}SavePSSObsIssueExtraCommentJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsLatLng(int TVItemID, float Lat, float Lng, TVTypeEnum TVType, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("Lat", ((float)Lat).ToString("F5"));
                paramList.Add("Lng", ((float)Lng).ToString("F5"));
                paramList.Add("TVType", ((int)TVType).ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}SaveLatLngWithTVTypeJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}SaveLatLngWithTVTypeJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsTVText(int TVItemID, string TVText, bool IsActive, bool IsPointSource, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("TVText", TVText);
                paramList.Add("IsActive", IsActive.ToString());
                paramList.Add("IsPointSource", IsPointSource.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}SavePSSTVTextAndIsActiveJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}SavePSSTVTextAndIsActiveJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsPicture(FileInfo fiPicture, int TVItemID, string AdminEmail)
        {
            try
            {
                string retStr = "";

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    //webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}SavePSSPictureJSON?e={AdminEmail}&t={TVItemID}");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}SavePSSPictureJSON?e={AdminEmail}&t={TVItemID}");
                    }

                    byte[] ret = webClient.UploadFile(uri, "POST", fiPicture.FullName);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsPictureInfo(int TVItemID, int PictureTVItemID, string FileName, string Description, string Extension, bool FromWater, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("PictureTVItemID", PictureTVItemID.ToString());
                paramList.Add("FileName", FileName);
                paramList.Add("Description", Description);
                paramList.Add("Extension", Extension);
                paramList.Add("FromWater", FromWater.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}SavePictureInfoJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}SavePictureInfoJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

        }
        private string SaveToCSSPWebToolsPictureToRemove(int TVItemID, int PictureTVItemID, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("PictureTVItemID", PictureTVItemID.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}RemovePictureJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}RemovePictureJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }

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

                        if (((Panel)a).Tag != null)
                        {
                            if (ContactTVItemID > 0)
                            {
                                if (((Panel)a).Tag.ToString() == ContactTVItemID.ToString())
                                {
                                    ((Panel)a).BackColor = BackColorEditing;
                                }
                            }
                            else
                            {
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
        public string ContactExistInCSSPWebTools(int TVItemID, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}ContactExistJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}ContactExistJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }
        }
        public string InfrastructureExistInCSSPWebTools(int TVItemID, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}InfrastructureExistJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}InfrastructureExistJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }
        }
        public string EmailExistInCSSPWebTools(int TVItemID, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}EmailExistJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}EmailExistJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }
        }
        public string TelExistInCSSPWebTools(int TVItemID, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}TelExistJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}TelExistJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }
        }
        public string PSSExistInCSSPWebTools(int TVItemID, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("TVItemID", TVItemID.ToString());
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}PSSExistJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}PSSExistJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }
        }
        public string MunicipalityExistUnderProvinceInCSSPWebTools(int ProvinceTVItemID, string Municipality, string AdminEmail)
        {
            try
            {
                string retStr = "";

                NameValueCollection paramList = new NameValueCollection();
                paramList.Add("ProvinceTVItemID", ProvinceTVItemID.ToString());
                paramList.Add("Municipality", Municipality);
                paramList.Add("AdminEmail", AdminEmail);

                using (WebClient webClient = new WebClient())
                {
                    WebProxy webProxy = new WebProxy();
                    webClient.Proxy = webProxy;

                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    Uri uri = new Uri($"{baseURLEN}MunicipalityExistJSON");
                    if (Language == LanguageEnum.fr)
                    {
                        uri = new Uri($"{baseURLFR}MunicipalityExistJSON");
                    }

                    byte[] ret = webClient.UploadValues(uri, "POST", paramList);

                    retStr = System.Text.Encoding.Default.GetString(ret);
                }

                return retStr;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message + (ex.InnerException != null ? " InnerException: " + ex.InnerException.Message : "");
            }
        }
    }
}
