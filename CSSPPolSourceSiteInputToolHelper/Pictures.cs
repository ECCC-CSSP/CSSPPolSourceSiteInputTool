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
        public void AddPicture()
        {
            if (IsPolSourceSite)
            {
                OpenFileDialog openFileDialogPictures = new OpenFileDialog();
                openFileDialogPictures.InitialDirectory = $@"C:\";
                openFileDialogPictures.Title = "Browse Pictures Files";
                openFileDialogPictures.DefaultExt = @"jpg";
                openFileDialogPictures.Filter = @"jpg files (*.jpg)|*.jpg";
                if (openFileDialogPictures.ShowDialog() == DialogResult.OK)
                {
                    PSS pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

                    Picture pictureNew = new Picture();

                    pictureNew.PictureTVItemID = 10000000;
                    pictureNew.FileName = "Change File Name " + pictureNew.PictureTVItemID.ToString();
                    pss.PSSPictureList.Add(pictureNew);

                    FileInfo fi = new FileInfo(openFileDialogPictures.FileName);

                    FileInfo fiCheck = new FileInfo($@"C:\PollutionSourceSites\Subsectors\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");
                    pictureNew.Extension = fi.Extension;
                    pictureNew.Description = "Insert Required Description";
                    while (fiCheck.Exists)
                    {
                        pictureNew.PictureTVItemID += 1;
                        pictureNew.FileName = "Change File Name " + pictureNew.PictureTVItemID.ToString();
                        fiCheck = new FileInfo($@"C:\PollutionSourceSites\Subsectors\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");
                    }

                    File.Copy(openFileDialogPictures.FileName, $@"C:\PollutionSourceSites\Subsectors\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");

                    SaveSubsectorTextFile();

                    ShowPictures();
                }
            }
            else
            {
                OpenFileDialog openFileDialogPictures = new OpenFileDialog();
                openFileDialogPictures.InitialDirectory = $@"C:\";
                openFileDialogPictures.Title = "Browse Pictures Files";
                openFileDialogPictures.DefaultExt = @"jpg";
                openFileDialogPictures.Filter = @"jpg files (*.jpg)|*.jpg";
                if (openFileDialogPictures.ShowDialog() == DialogResult.OK)
                {
                    Infrastructure infrastructure = municipalityDoc.Municipality.InfrastructureList.Where(c => c.InfrastructureTVItemID == InfrastructureTVItemID).FirstOrDefault();

                    Picture pictureNew = new Picture();

                    pictureNew.PictureTVItemID = 10000000;
                    pictureNew.FileName = "Change File Name " + pictureNew.PictureTVItemID.ToString();
                    infrastructure.InfrastructurePictureList.Add(pictureNew);

                    FileInfo fi = new FileInfo(openFileDialogPictures.FileName);

                    FileInfo fiCheck = new FileInfo($@"C:\PollutionSourceSites\Infrastructures\{CurrentMunicipalityName}\Pictures\{infrastructure.InfrastructureTVItemID}_{pictureNew.PictureTVItemID}{fi.Extension}");
                    pictureNew.Extension = fi.Extension;
                    pictureNew.Description = "Insert Required Description";
                    while (fiCheck.Exists)
                    {
                        pictureNew.PictureTVItemID += 1;
                        pictureNew.FileName = "Change File Name " + pictureNew.PictureTVItemID.ToString();
                        fiCheck = new FileInfo($@"C:\PollutionSourceSites\Infrastructures\{CurrentMunicipalityName}\Pictures\{infrastructure.InfrastructureTVItemID}_{pictureNew.PictureTVItemID}{fi.Extension}");
                    }

                    File.Copy(openFileDialogPictures.FileName, $@"C:\PollutionSourceSites\Infrastructures\{CurrentMunicipalityName}\Pictures\{infrastructure.InfrastructureTVItemID}_{pictureNew.PictureTVItemID}{fi.Extension}");

                    SaveMunicipalityTextFile();
                    RedrawInfrastructureList();
                    ShowPictures();
                }
            }
        }
        public void RemovePicture(int PictureTVItemID)
        {
            if (IsPolSourceSite)
            {
                Picture pictureToRemove = CurrentPSS.PSSPictureList.Where(c => c.PictureTVItemID == PictureTVItemID).FirstOrDefault();
                if (pictureToRemove != null)
                {
                    pictureToRemove.ToRemove = true;
                }

                SaveSubsectorTextFile();
                ShowPictures();
            }
            else
            {
                Picture pictureToRemove = CurrentInfrastructure.InfrastructurePictureList.Where(c => c.PictureTVItemID == PictureTVItemID).FirstOrDefault();
                if (pictureToRemove != null)
                {
                    pictureToRemove.ToRemove = true;
                }

                SaveMunicipalityTextFile();
                RedrawInfrastructureList();
                ShowPictures();
            }
        }
        public void UnRemovePicture(int PictureTVItemID)
        {
            if (IsPolSourceSite)
            {
                Picture pictureToRemove = CurrentPSS.PSSPictureList.Where(c => c.PictureTVItemID == PictureTVItemID).FirstOrDefault();
                if (pictureToRemove != null)
                {
                    pictureToRemove.ToRemove = false;
                }

                SaveSubsectorTextFile();
                ShowPictures();
            }
            else
            {
                Picture pictureToRemove = CurrentInfrastructure.InfrastructurePictureList.Where(c => c.PictureTVItemID == PictureTVItemID).FirstOrDefault();
                if (pictureToRemove != null)
                {
                    pictureToRemove.ToRemove = false;
                }

                SaveMunicipalityTextFile();
                RedrawInfrastructureList();
                ShowPictures();
            }
        }
        public void SavePictureInfo(int PictureTVItemID)
        {
            if (IsPolSourceSite)
            {
                Picture picture = CurrentPSS.PSSPictureList.Where(c => c.PictureTVItemID == PictureTVItemID).FirstOrDefault();
                if (picture != null)
                {
                    foreach (Control control in PanelViewAndEdit.Controls)
                    {
                        if (control.GetType().Name == "Panel")
                        {
                            int PictureTVItemID2 = int.Parse(control.Tag.ToString());
                            if (PictureTVItemID2 == PictureTVItemID)
                            {
                                TextBox tbFileName = new TextBox();
                                TextBox tbDesc = new TextBox();
                                PictureBox pbFileName = new PictureBox();
                                foreach (Control control2 in control.Controls)
                                {
                                    if (control2.GetType().Name == "TextBox" && control2.Name == "textBoxFileName")
                                    {
                                        tbFileName = (TextBox)control2;
                                        if (tbFileName.Text.Trim() != picture.FileName)
                                        {
                                            picture.FileNameNew = tbFileName.Text.Trim();
                                        }
                                    }
                                    if (control2.GetType().Name == "TextBox" && control2.Name == "textBoxDescription")
                                    {
                                        tbDesc = (TextBox)control2;
                                        if (tbDesc.Text.Trim() != picture.Description)
                                        {
                                            picture.DescriptionNew = tbDesc.Text.Trim();
                                        }
                                    }
                                    if (control2.GetType().Name == "pictureBox" && control2.Name == "pictureBoxPicture")
                                    {
                                        pbFileName = (PictureBox)control2;
                                        FileInfo fiPicture = new FileInfo(pbFileName.ImageLocation);
                                        if (fiPicture.Extension != picture.Extension)
                                        {
                                            picture.ExtensionNew = fiPicture.Extension;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                SaveSubsectorTextFile();
                ShowPictures();
            }
            else
            {
                Picture picture = CurrentInfrastructure.InfrastructurePictureList.Where(c => c.PictureTVItemID == PictureTVItemID).FirstOrDefault();
                if (picture != null)
                {
                    foreach (Control control in PanelViewAndEdit.Controls)
                    {
                        if (control.GetType().Name == "Panel")
                        {
                            int PictureTVItemID2 = int.Parse(control.Tag.ToString());
                            if (PictureTVItemID2 == PictureTVItemID)
                            {
                                TextBox tbFileName = new TextBox();
                                TextBox tbDesc = new TextBox();
                                PictureBox pbFileName = new PictureBox();
                                foreach (Control control2 in control.Controls)
                                {
                                    if (control2.GetType().Name == "TextBox" && control2.Name == "textBoxFileName")
                                    {
                                        tbFileName = (TextBox)control2;
                                        if (tbFileName.Text.Trim() != picture.FileName)
                                        {
                                            picture.FileNameNew = tbFileName.Text.Trim();
                                        }
                                    }
                                    if (control2.GetType().Name == "TextBox" && control2.Name == "textBoxDescription")
                                    {
                                        tbDesc = (TextBox)control2;
                                        if (tbDesc.Text.Trim() != picture.Description)
                                        {
                                            picture.DescriptionNew = tbDesc.Text.Trim();
                                        }
                                    }
                                    if (control2.GetType().Name == "pictureBox" && control2.Name == "pictureBoxPicture")
                                    {
                                        pbFileName = (PictureBox)control2;
                                        FileInfo fiPicture = new FileInfo(pbFileName.ImageLocation);
                                        if (fiPicture.Extension != picture.Extension)
                                        {
                                            picture.ExtensionNew = fiPicture.Extension;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                SaveMunicipalityTextFile();
                RedrawInfrastructureList();
                ShowPictures();
            }
        }
        public void ShowPictures()
        {
            int Y = 0;
            int X = 0;

            if (IsPolSourceSite)
            {
                if (CurrentPSS == null)
                {
                    PanelViewAndEdit.Controls.Clear();

                    Label lblMessage = new Label();
                    lblMessage.AutoSize = true;
                    lblMessage.Location = new Point(30, 30);
                    lblMessage.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblMessage.Font = new Font(new FontFamily(lblMessage.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                    lblMessage.Text = $"Please select a pollution source site items for {(IsEditing ? "editing" : "viewing")} {(ShowOnlyIssues ? "issues" : (ShowOnlyPictures ? "pictures" : "pollution source site"))}";

                    PanelViewAndEdit.Controls.Add(lblMessage);

                    return;
                }
            }
            else
            {
                if (CurrentInfrastructure == null)
                {
                    PanelViewAndEdit.Controls.Clear();

                    Label lblMessage = new Label();
                    lblMessage.AutoSize = true;
                    lblMessage.Location = new Point(30, 30);
                    lblMessage.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblMessage.Font = new Font(new FontFamily(lblMessage.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                    lblMessage.Text = $"Please select an infrastructure items for {(IsEditing ? "editing" : "viewing")} {(ShowOnlyIssues ? "issues" : (ShowOnlyPictures ? "pictures" : "infrastructure"))}";

                    PanelViewAndEdit.Controls.Add(lblMessage);

                    return;
                }
            }

            if (Language == LanguageEnum.fr)
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.fr);
            }
            else
            {
                _BaseEnumService = new BaseEnumService(LanguageEnum.en);
            }

            if (IsEditing || ShowOnlyPictures)
            {
                PanelViewAndEdit.Controls.Clear();
                Y = 4;
            }
            else
            {
                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
            }

            X = 10;

            Label lblPictures = new Label();
            lblPictures.AutoSize = true;
            lblPictures.Location = new Point(X, Y);
            lblPictures.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblPictures.Font = new Font(new FontFamily(lblPictures.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
            lblPictures.Text = $"Pictures";

            PanelViewAndEdit.Controls.Add(lblPictures);

            Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

            List<Picture> pictureList = new List<Picture>();

            if (IsPolSourceSite)
            {
                pictureList = CurrentPSS.PSSPictureList;
            }
            else
            {
                pictureList = CurrentInfrastructure.InfrastructurePictureList;
            }
            if (pictureList.Count > 0)
            {
                foreach (Picture picture in pictureList)
                {
                    X = 10;

                    Panel panelPicture = new Panel();
                    panelPicture.AutoSize = true;
                    panelPicture.Location = new Point(X, Y);
                    panelPicture.Tag = $"{picture.PictureTVItemID}";
                    panelPicture.BorderStyle = BorderStyle.FixedSingle;
                    panelPicture.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);

                    PanelViewAndEdit.Controls.Add(panelPicture);

                    //Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                    X = 10;
                    Y = 10;

                    PictureBox pictureBoxPicture = new PictureBox();

                    pictureBoxPicture.BorderStyle = BorderStyle.FixedSingle;
                    if (IsPolSourceSite)
                    {
                        pictureBoxPicture.ImageLocation = $@"C:\PollutionSourceSites\Subsectors\{CurrentSubsectorName}\Pictures\{CurrentPSS.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                    }
                    else
                    {
                        pictureBoxPicture.ImageLocation = $@"C:\PollutionSourceSites\Infrastructures\{CurrentMunicipalityName}\Pictures\{CurrentInfrastructure.InfrastructureTVItemID}_{picture.PictureTVItemID}{picture.Extension}";
                    }
                    pictureBoxPicture.Location = new Point(X, Y);
                    pictureBoxPicture.Size = new Size(600, 500);
                    pictureBoxPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBoxPicture.TabIndex = 0;
                    pictureBoxPicture.TabStop = false;
                    pictureBoxPicture.Name = "pictureBoxPicture";
                    pictureBoxPicture.Tag = $"{picture.PictureTVItemID}";

                    if (picture.ToRemove == true)
                    {
                        pictureBoxPicture.Paint += PictureBoxPicture_Paint;
                    }

                    panelPicture.Controls.Add(pictureBoxPicture);

                    Y = panelPicture.Controls[panelPicture.Controls.Count - 1].Bottom + 10;

                    X = 20;
                    Label lblFileName = new Label();
                    lblFileName.AutoSize = true;
                    lblFileName.Location = new Point(X, Y);
                    lblFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblFileName.Tag = $"{picture.PictureTVItemID}";
                    lblFileName.Text = $@"Server File Name: ";

                    panelPicture.Controls.Add(lblFileName);

                    X = panelPicture.Controls[panelPicture.Controls.Count - 1].Right + 5;

                    if (picture.FileNameNew != null)
                    {
                        Label lblFileNameOld = new Label();
                        lblFileNameOld.AutoSize = true;
                        lblFileNameOld.Location = new Point(X, Y);
                        lblFileNameOld.Font = new Font(new FontFamily(lblFileNameOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblFileNameOld.Tag = $"{picture.PictureTVItemID}";
                        lblFileNameOld.Text = $@" ({picture.FileName})";

                        panelPicture.Controls.Add(lblFileNameOld);

                        Y = panelPicture.Controls[panelPicture.Controls.Count - 1].Bottom + 10;
                    }

                    if (IsEditing)
                    {

                        TextBox textBoxFileName = new TextBox();
                        textBoxFileName.Location = new Point(X, Y);
                        textBoxFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        textBoxFileName.Width = 300;
                        textBoxFileName.Name = "textBoxFileName";
                        textBoxFileName.Tag = $"{picture.PictureTVItemID}";
                        if (picture.FileNameNew != null)
                        {
                            textBoxFileName.Text = picture.FileNameNew;
                        }
                        else
                        {
                            textBoxFileName.Text = picture.FileName;
                        }

                        panelPicture.Controls.Add(textBoxFileName);
                    }
                    else
                    {
                        Label lblFileName2 = new Label();
                        lblFileName2.Location = new Point(X, Y);
                        lblFileName2.Font = new Font(new FontFamily(lblFileName2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblFileName2.Width = 300;
                        lblFileName2.Tag = $"{picture.PictureTVItemID}";
                        if (picture.FileNameNew != null)
                        {
                            lblFileName2.Text = picture.FileNameNew;
                        }
                        else
                        {
                            lblFileName2.Text = picture.FileName;
                        }

                        panelPicture.Controls.Add(lblFileName2);
                    }

                    Y = panelPicture.Controls[panelPicture.Controls.Count - 1].Bottom + 10;
                    X = 20;

                    Label lblDescription = new Label();
                    lblDescription.AutoSize = true;
                    lblDescription.Location = new Point(20, Y);
                    lblDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblDescription.Tag = $"{picture.PictureTVItemID}";
                    lblDescription.Text = $@"Description: ";

                    panelPicture.Controls.Add(lblDescription);

                    X = panelPicture.Controls[panelPicture.Controls.Count - 1].Right + 5;

                    if (picture.DescriptionNew != null)
                    {
                        Label lblDescriptionOld = new Label();
                        lblDescriptionOld.AutoSize = true;
                        lblDescriptionOld.Location = new Point(X, Y);
                        lblDescriptionOld.Font = new Font(new FontFamily(lblDescriptionOld.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblDescriptionOld.Tag = $"{picture.PictureTVItemID}";
                        lblDescriptionOld.Text = $@" ({picture.Description})";

                        panelPicture.Controls.Add(lblDescriptionOld);

                        Y = panelPicture.Controls[panelPicture.Controls.Count - 1].Bottom + 10;
                    }

                    if (IsEditing)
                    {
                        TextBox textBoxDescription = new TextBox();
                        textBoxDescription.Location = new Point(X, Y);
                        textBoxDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        textBoxDescription.Width = 300;
                        textBoxDescription.Height = 100;
                        textBoxDescription.Multiline = true;
                        textBoxDescription.Name = "textBoxDescription";
                        textBoxDescription.Tag = $"{picture.PictureTVItemID}";
                        if (picture.DescriptionNew != null)
                        {
                            textBoxDescription.Text = picture.DescriptionNew;
                        }
                        else
                        {
                            textBoxDescription.Text = picture.Description;
                        }

                        panelPicture.Controls.Add(textBoxDescription);
                    }
                    else
                    {
                        Label lblDescription2 = new Label();
                        lblDescription2.AutoSize = true;
                        lblDescription2.Location = new Point(X, Y);
                        lblDescription2.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblDescription2.Width = 300;
                        lblDescription2.Height = 100;
                        lblDescription2.Tag = $"{picture.PictureTVItemID}";
                        if (picture.DescriptionNew != null)
                        {
                            lblDescription2.Text = picture.DescriptionNew;
                        }
                        else
                        {
                            lblDescription2.Text = picture.Description;
                        }

                        panelPicture.Controls.Add(lblDescription2);
                    }

                    Y = panelPicture.Controls[panelPicture.Controls.Count - 1].Bottom + 10;
                    X = 120;

                    if (IsEditing)
                    {
                        Button butSavePictureInfo = new Button();
                        butSavePictureInfo.AutoSize = true;
                        butSavePictureInfo.Location = new Point(X, Y);
                        butSavePictureInfo.Text = "Save File Name And Description";
                        butSavePictureInfo.Tag = $"{picture.PictureTVItemID}";
                        butSavePictureInfo.Padding = new Padding(5);
                        butSavePictureInfo.Click += butSavePictureInfo_Click;

                        panelPicture.Controls.Add(butSavePictureInfo);

                        X = butSavePictureInfo.Right + 20;

                        if (picture.ToRemove == true)
                        {
                            Button butUnRemovePicture = new Button();
                            butUnRemovePicture.AutoSize = true;
                            butUnRemovePicture.Location = new Point(X, Y);
                            butUnRemovePicture.Text = "Unremove";
                            butUnRemovePicture.Tag = $"{picture.PictureTVItemID}";
                            butUnRemovePicture.Padding = new Padding(5);
                            butUnRemovePicture.Click += butUnRemovePicture_Click;

                            panelPicture.Controls.Add(butUnRemovePicture);
                        }
                        else
                        {
                            Button butRemovePicture = new Button();
                            butRemovePicture.AutoSize = true;
                            butRemovePicture.Location = new Point(X, Y);
                            butRemovePicture.Text = "Remove";
                            butRemovePicture.Tag = $"{picture.PictureTVItemID}";
                            butRemovePicture.Padding = new Padding(5);
                            butRemovePicture.Click += butRemovePicture_Click;

                            panelPicture.Controls.Add(butRemovePicture);

                        }
                    }

                    //Y = panelPicture.Controls[panelPicture.Controls.Count - 1].Bottom + 50;

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                }
            }
            else
            {
                if (!IsEditing)
                {
                    Label lblEmpty = new Label();
                    lblEmpty.AutoSize = true;
                    lblEmpty.Location = new Point(30, Y);
                    lblEmpty.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblEmpty.Font = new Font(new FontFamily(lblEmpty.Font.FontFamily.Name).Name, 12f, FontStyle.Regular);
                    lblEmpty.Text = $"No Picture";

                    PanelViewAndEdit.Controls.Add(lblEmpty);
                }
            }

            Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
            X = 200;
            if (IsEditing)
            {
                Button butAddPicture = new Button();
                butAddPicture.AutoSize = true;
                butAddPicture.Location = new Point(20, Y);
                if (IsPolSourceSite)
                {
                    butAddPicture.Tag = $"{CurrentPSS.PSSTVItemID}";
                }
                else
                {
                    butAddPicture.Tag = $"{CurrentInfrastructure.InfrastructureTVItemID}";
                }
                butAddPicture.Font = new Font(new FontFamily(butAddPicture.Font.FontFamily.Name).Name, 14f, FontStyle.Regular);
                butAddPicture.Padding = new Padding(5);
                butAddPicture.Text = "Add Picture";
                butAddPicture.Click += butAddPicture_Click;

                PanelViewAndEdit.Controls.Add(butAddPicture);
            }

            Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

            Label lblReturns = new Label();
            lblReturns.AutoSize = true;
            lblReturns.Location = new Point(30, Y);
            lblReturns.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblReturns.Font = new Font(new FontFamily(lblReturns.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
            lblReturns.Text = "\r\n\r\n\r\n\r\n";

            PanelViewAndEdit.Controls.Add(lblReturns);

        }

        private void PictureBoxPicture_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pictureBoxPicture = (PictureBox)sender;
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Red), 5), pictureBoxPicture.Left + 100, pictureBoxPicture.Top + 100, pictureBoxPicture.Right - 100, pictureBoxPicture.Bottom - 100);
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Red), 5), pictureBoxPicture.Left + 100, pictureBoxPicture.Bottom - 100, pictureBoxPicture.Right - 100, pictureBoxPicture.Top + 100);
        }
        private void PicturesSaveToCSSPWebTools()
        {
            MessageBox.Show("PicturesSaveToCSSPWebTools " + CurrentPSS.PSSTVItemID.ToString(), PolSourceSiteTVItemID.ToString());
        }
    }
}
