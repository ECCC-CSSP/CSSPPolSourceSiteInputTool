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
        public void ShowMap()
        {
            PanelViewAndEdit.Controls.Clear();

            Label tempLabel = new Label();

            tempLabel.AutoSize = true;
            tempLabel.Location = new Point(30, 30);
            tempLabel.TabIndex = 0;
            tempLabel.Font = new Font(new FontFamily(tempLabel.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
            tempLabel.Text = $"ShowMap is not implemented yet ";

            PanelViewAndEdit.Controls.Add(tempLabel);
        }
    }
}
