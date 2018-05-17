using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSPPolSourceSiteReadSubsectorFile
{
    public partial class ReadSubsectorFile
    {
        private void butAddPicture_Click(object sender, EventArgs e)
        {
            AddPicture();
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

    }
}
