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
            OpenFileDialog openFileDialogPictures = new OpenFileDialog();
            openFileDialogPictures.InitialDirectory = $@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\";
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

                FileInfo fiCheck = new FileInfo($@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");
                pictureNew.Extension = fi.Extension;
                pictureNew.Description = "Insert Required Description";
                while (fiCheck.Exists)
                {
                    pictureNew.PictureTVItemID += 1;
                    pictureNew.FileName = "Change File Name " + pictureNew.PictureTVItemID.ToString();
                    fiCheck = new FileInfo($@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");
                }

                File.Copy(openFileDialogPictures.FileName, $@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{pictureNew.PictureTVItemID}{fi.Extension}");

                ShowPictures();
            }
        }
        public void RemovePicture(int PictureTVItemID)
        {
            PSS pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

            Picture pictureToRemove = pss.PSSPictureList.Where(c => c.PictureTVItemID == PictureTVItemID).FirstOrDefault();
            if (pictureToRemove != null)
            {
                pss.PSSPictureList.Remove(pictureToRemove);
            }

            ShowPictures();
        }
        public void SavePictureInfo()
        {
        }
        public void ShowPictures()
        {
            int Y = 0;
            int X = 0;

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

            if (CurrentPSS.PSSPictureList.Count > 0)
            {
                foreach (Picture picture in CurrentPSS.PSSPictureList)
                {
                    X = 10;

                    PictureBox tempPictureBox = new PictureBox();

                    tempPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    tempPictureBox.ImageLocation = $@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\{CurrentPSS.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                    tempPictureBox.Location = new Point(X, Y);
                    tempPictureBox.Size = new Size(600, 500);
                    tempPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    tempPictureBox.TabIndex = 0;
                    tempPictureBox.TabStop = false;

                    PanelViewAndEdit.Controls.Add(tempPictureBox);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                    if (IsEditing)
                    {
                        X = 20;
                        Label lblFileName = new Label();
                        lblFileName.AutoSize = true;
                        lblFileName.Location = new Point(X, Y);
                        lblFileName.Tag = $"{CurrentPSS.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                        lblFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblFileName.Text = $@"Server File Name: ";

                        PanelViewAndEdit.Controls.Add(lblFileName);

                        X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

                        TextBox textFileName = new TextBox();
                        textFileName.Location = new Point(X, Y);
                        textFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        textFileName.Width = 300;
                        textFileName.Text = picture.FileName;

                        PanelViewAndEdit.Controls.Add(textFileName);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        X = 20;

                        Label lblDescription = new Label();
                        lblDescription.AutoSize = true;
                        lblDescription.Location = new Point(20, Y);
                        lblDescription.Tag = $"{CurrentPSS.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                        lblDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblDescription.Text = $@"Description: ";

                        PanelViewAndEdit.Controls.Add(lblDescription);

                        X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 5;

                        TextBox textDescription = new TextBox();
                        textDescription.Location = new Point(X, Y);
                        textDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        textDescription.Width = 300;
                        textDescription.Height = 100;
                        textDescription.Multiline = true;
                        textDescription.Text = picture.Description;

                        PanelViewAndEdit.Controls.Add(textDescription);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                        X = 120;

                        Button butSavePictureFileName = new Button();
                        butSavePictureFileName.Location = new Point(X, Y);
                        butSavePictureFileName.Text = "Save File Name";
                        butSavePictureFileName.Tag = $"{picture.PictureTVItemID}";
                        butSavePictureFileName.Click += butSavePictureFileName_Click;

                        PanelViewAndEdit.Controls.Add(butSavePictureFileName);

                        X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;

                        Button butRemovePicture = new Button();
                        butRemovePicture.Location = new Point(X, Y);
                        butRemovePicture.Text = "Remove";
                        butRemovePicture.Tag = $"{picture.PictureTVItemID}";
                        butRemovePicture.Click += butRemovePicture_Click;

                        PanelViewAndEdit.Controls.Add(butRemovePicture);


                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 50;
                    }
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
                    lblEmpty.Font = new Font(new FontFamily(lblEmpty.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblEmpty.Text = $"No Picture";

                    PanelViewAndEdit.Controls.Add(lblEmpty);
                }
            }

            Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
            if (IsEditing)
            {
                Button butAddPicture = new Button();
                butAddPicture.Location = new Point(20, Y);
                butAddPicture.Text = "Add Picture";
                butAddPicture.Tag = $"{CurrentPSS.SiteNumberText}";
                butAddPicture.Click += butAddPicture_Click;

                PanelViewAndEdit.Controls.Add(butAddPicture);
            }
        }
    }
}
