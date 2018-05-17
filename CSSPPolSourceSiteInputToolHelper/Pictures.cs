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
            PanelViewAndEdit.Controls.Clear();

            PSS pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

            int pos = 4;

            Label lblPictures = new Label();
            lblPictures.AutoSize = true;
            lblPictures.Location = new Point(10, pos);
            lblPictures.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblPictures.Font = new Font(new FontFamily(lblPictures.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
            lblPictures.Text = $"Pictures";

            PanelViewAndEdit.Controls.Add(lblPictures);

            pos = lblPictures.Bottom + 10;

            if (pss.PSSPictureList.Count > 0)
            {
                foreach (Picture picture in pss.PSSPictureList)
                {
                    PictureBox tempPictureBox = new PictureBox();

                    tempPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    tempPictureBox.ImageLocation = $@"C:\PollutionSourceSites\{CurrentSubsectorName}\Pictures\{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                    tempPictureBox.Location = new Point(10, pos);
                    tempPictureBox.Size = new Size(600, 500);
                    tempPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    tempPictureBox.TabIndex = 0;
                    tempPictureBox.TabStop = false;

                    PanelViewAndEdit.Controls.Add(tempPictureBox);

                    pos = tempPictureBox.Bottom + 10;

                    if (IsEditing)
                    {
                        Label lblFileName = new Label();
                        lblFileName.AutoSize = true;
                        lblFileName.Location = new Point(20, pos);
                        lblFileName.Tag = $"{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                        lblFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblFileName.Text = $@"Server File Name: ";

                        PanelViewAndEdit.Controls.Add(lblFileName);

                        TextBox textFileName = new TextBox();
                        textFileName.Location = new Point(lblFileName.Right + 5, pos);
                        textFileName.Font = new Font(new FontFamily(lblFileName.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        textFileName.Width = 300;
                        textFileName.Text = picture.FileName;

                        PanelViewAndEdit.Controls.Add(textFileName);

                        pos = textFileName.Bottom + 10;

                        Label lblDescription = new Label();
                        lblDescription.AutoSize = true;
                        lblDescription.Location = new Point(20, pos);
                        lblDescription.Tag = $"{pss.SiteNumberText}_{picture.PictureTVItemID}{picture.Extension}";
                        lblDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblDescription.Text = $@"Description: ";

                        PanelViewAndEdit.Controls.Add(lblDescription);

                        TextBox textDescription = new TextBox();
                        textDescription.Location = new Point(lblDescription.Right + 5, pos);
                        textDescription.Font = new Font(new FontFamily(lblDescription.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        textDescription.Width = 300;
                        textDescription.Height = 100;
                        textDescription.Multiline = true;
                        textDescription.Text = picture.Description;

                        PanelViewAndEdit.Controls.Add(textDescription);

                        pos = textDescription.Bottom + 10;

                        Button butSavePictureFileName = new Button();
                        butSavePictureFileName.Location = new Point(120, pos);
                        butSavePictureFileName.Text = "Save File Name";
                        butSavePictureFileName.Tag = $"{picture.PictureTVItemID}";
                        butSavePictureFileName.Click += butSavePictureFileName_Click;

                        PanelViewAndEdit.Controls.Add(butSavePictureFileName);

                        Button butRemovePicture = new Button();
                        butRemovePicture.Location = new Point(butSavePictureFileName.Right + 20, pos);
                        butRemovePicture.Text = "Remove";
                        butRemovePicture.Tag = $"{picture.PictureTVItemID}";
                        butRemovePicture.Click += butRemovePicture_Click;

                        PanelViewAndEdit.Controls.Add(butRemovePicture);


                        pos = butRemovePicture.Bottom + 50;
                    }
                }
            }
            else
            {
                if (!IsEditing)
                {
                    Label lblEmpty = new Label();
                    lblEmpty.AutoSize = true;
                    lblEmpty.Location = new Point(30, pos);
                    lblEmpty.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblEmpty.Font = new Font(new FontFamily(lblEmpty.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblEmpty.Text = $"No Picture";

                    PanelViewAndEdit.Controls.Add(lblEmpty);
                }
            }

            pos += 20;
            if (IsEditing)
            {
                Button butAddPicture = new Button();
                butAddPicture.Location = new Point(20, pos);
                butAddPicture.Text = "Add Picture";
                butAddPicture.Tag = $"{pss.SiteNumberText}";
                butAddPicture.Click += butAddPicture_Click;

                PanelViewAndEdit.Controls.Add(butAddPicture);
            }
        }
    }
}
