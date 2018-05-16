using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using CSSPPolSourceSiteReadSubsectorFile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CSSPPolSourceSiteInputTool
{
    public partial class CSSPPolSourceSiteInputToolForm : Form
    {
        private void butAddPicture_Click(object sender, EventArgs e)
        {
            AddPicture();
        }
        private void butEdit_Click(object sender, EventArgs e)
        {
            IsEditing = true;
            ShowPolSourceSite();
        }
        private void butIssues_Click(object sender, EventArgs e)
        {
            ShowIssues();
        }
        private void butMap_Click(object sender, EventArgs e)
        {
            ShowMap();
        }
        private void butPictures_Click(object sender, EventArgs e)
        {
            ShowPictures();
        }
        private void butRegenerateKMLFile_Click(object sender, EventArgs e)
        {
            RegenerateKMLFile();
        }
        private void butRemovePicture_Click(object sender, EventArgs e)
        {
            int PictureTVItemID = int.Parse(((Button)sender).Tag.ToString());
            RemovePicture(PictureTVItemID);
        }
        private void butSaveLatLngAndObsAndAddress_Click(object sender, EventArgs e)
        {
            SaveLatLngAndObsAndAddress();
            IsEditing = false;
            ShowPolSourceSite();
        }
        private void butSavePictureFileName_Click(object sender, EventArgs e)
        {
            SavePictureInfo();
        }
        private void comboBoxSubsectorNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawPolSourceSiteList();
            panelViewAndEdit.Controls.Clear();
        }
        private void readSubsectorFile_UpdateStatus(object sender, ReadSubsectorFile.StatusEventArgs e)
        {
            lblStatus.Text = e.Status;
        }
        private void showDetailsViaLabel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Label)sender).Tag.ToString());
            IsEditing = false;
            ShowPolSourceSite();
        }
        private void showDetailsViaPanel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Panel)sender).Tag.ToString());
            IsEditing = false;
            ShowPolSourceSite();
        }
        private void showEditViaLabel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Label)sender).Tag.ToString());
            IsEditing = true;
            ShowPolSourceSite();
        }
        private void showEditViaPanel(object sender, EventArgs e)
        {
            PolSourceSiteTVItemID = int.Parse(((Panel)sender).Tag.ToString());
            IsEditing = true;
            ShowPolSourceSite();
        }
        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            if (!IsDirty && PolSourceSiteTVItemID != 0)
            {
                ShowPolSourceSite();
            }
        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!IsDirty && PolSourceSiteTVItemID != 0)
            {
                    ShowPolSourceSite();
            }
        }
    }
}
