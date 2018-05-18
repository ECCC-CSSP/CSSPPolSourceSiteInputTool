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
        public void ShowIssues()
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

            //PSS pss = subsectorDoc.Subsector.PSSList.Where(c => c.PSSTVItemID == PolSourceSiteTVItemID).FirstOrDefault();

            int pos = 4;

            Label lblIssues = new Label();
            lblIssues.AutoSize = true;
            lblIssues.Location = new Point(10, pos);
            lblIssues.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblIssues.Font = new Font(new FontFamily(lblIssues.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
            lblIssues.Text = $"Issues";

            PanelViewAndEdit.Controls.Add(lblIssues);

            pos = lblIssues.Bottom + 20;

            Label lblWrittenDesc = new Label();
            lblWrittenDesc.AutoSize = true;
            lblWrittenDesc.Location = new Point(10, pos);
            lblWrittenDesc.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblWrittenDesc.Font = new Font(new FontFamily(lblWrittenDesc.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            lblWrittenDesc.Text = $"Written Description: ";

            PanelViewAndEdit.Controls.Add(lblWrittenDesc);

            pos = lblWrittenDesc.Bottom + 8;

            Label lblWrittenDesc2 = new Label();
            lblWrittenDesc2.AutoSize = true;
            lblWrittenDesc2.Location = new Point(30, pos);
            lblWrittenDesc2.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblWrittenDesc2.Font = new Font(new FontFamily(lblWrittenDesc2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
            if (string.IsNullOrWhiteSpace(CurrentPSS.PSSObs.OldWrittenDescription))
            {
                lblWrittenDesc2.Text = $"No written description";
            }
            else
            {
                lblWrittenDesc2.Text = $"{CurrentPSS.PSSObs.OldWrittenDescription}";
            }

            PanelViewAndEdit.Controls.Add(lblWrittenDesc2);

            pos = lblWrittenDesc2.Bottom + 10;

            if (CurrentPSS.OldIssueTextList.Count > 0)
            {
                Label lblOldIssue = new Label();
                lblOldIssue.AutoSize = true;
                lblOldIssue.Location = new Point(10, pos);
                lblOldIssue.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblOldIssue.Font = new Font(new FontFamily(lblOldIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblOldIssue.Text = $"Old Issue Text:";

                PanelViewAndEdit.Controls.Add(lblOldIssue);

                pos = lblOldIssue.Bottom + 6;

                int OldIssueCount = 0;
                foreach (string OldIssueText in CurrentPSS.OldIssueTextList)
                {
                    OldIssueCount += 1;

                    Label lblOldIssueCount = new Label();
                    lblOldIssueCount.AutoSize = true;
                    lblOldIssueCount.Location = new Point(30, pos);
                    lblOldIssueCount.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblOldIssueCount.Font = new Font(new FontFamily(lblOldIssueCount.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblOldIssueCount.Text = $"{OldIssueCount}) - ";

                    PanelViewAndEdit.Controls.Add(lblOldIssueCount);

                    Label lblOldIssueText = new Label();
                    lblOldIssueText.AutoSize = true;
                    lblOldIssueText.Location = new Point(lblOldIssueCount.Right, pos);
                    lblOldIssueText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblOldIssueText.Font = new Font(new FontFamily(lblOldIssueText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblOldIssueText.Text = $"{OldIssueText}";

                    PanelViewAndEdit.Controls.Add(lblOldIssueText);

                    pos = lblOldIssueText.Bottom + 10;
                }
            }
            else
            {
                Label lblOldIssue = new Label();
                lblOldIssue.AutoSize = true;
                lblOldIssue.Location = new Point(30, pos);
                lblOldIssue.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblOldIssue.Font = new Font(new FontFamily(lblOldIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblOldIssue.Text = $"No Old Issue Text:";

                PanelViewAndEdit.Controls.Add(lblOldIssue);

                pos = lblOldIssue.Bottom + 10;
            }

            if (CurrentPSS.PSSObs.IssueList.Count > 0)
            {
                int IssueCount = 0;
                foreach (Issue issue in CurrentPSS.PSSObs.IssueList)
                {
                    IssueCount += 1;

                    Label lblIssue = new Label();
                    lblIssue.AutoSize = true;
                    lblIssue.Location = new Point(10, pos);
                    lblIssue.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblIssue.Font = new Font(new FontFamily(lblIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblIssue.Text = $"Issue ({IssueCount}) --- Last Update (UTC): ";

                    PanelViewAndEdit.Controls.Add(lblIssue);

                    Label lblIssueLastUpdate = new Label();
                    lblIssueLastUpdate.AutoSize = true;
                    lblIssueLastUpdate.Location = new Point(lblIssue.Right, lblIssue.Top);
                    lblIssueLastUpdate.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblIssueLastUpdate.Font = new Font(new FontFamily(lblIssueLastUpdate.Font.FontFamily.Name).Name, 12f, FontStyle.Regular);
                    lblIssueLastUpdate.Text = $"{((DateTime)issue.LastUpdated_UTC).ToString("yyyy MMMM dd HH:mm:ss")}";

                    PanelViewAndEdit.Controls.Add(lblIssueLastUpdate);

                    pos = lblIssue.Bottom + 4;

                    string TVText = "";
                    for (int i = 0, count = issue.PolSourceObsInfoIntList.Count; i < count; i++)
                    {
                        string Temp = _BaseEnumService.GetEnumText_PolSourceObsInfoReportEnum((PolSourceObsInfoEnum)issue.PolSourceObsInfoIntList[i]);
                        switch ((issue.PolSourceObsInfoIntList[i].ToString()).Substring(0, 3))
                        {
                            case "101":
                                {
                                    Temp = Temp.Replace("Source", "     Source");
                                }
                                break;
                            case "250":
                                {
                                    Temp = Temp.Replace("Pathway", "\r\n     Pathway");
                                }
                                break;
                            case "900":
                                {
                                    Temp = Temp.Replace("Status", "\r\n     Status");
                                }
                                break;
                            case "910":
                                {
                                    Temp = Temp.Replace("Risk", "\r\n     Risk");
                                }
                                break;
                            case "110":
                            case "120":
                            case "122":
                            case "151":
                            case "152":
                            case "153":
                            case "155":
                            case "156":
                            case "157":
                            case "163":
                            case "166":
                            case "167":
                            case "170":
                            case "171":
                            case "172":
                            case "173":
                            case "176":
                            case "178":
                            case "181":
                            case "182":
                            case "183":
                            case "185":
                            case "186":
                            case "187":
                            case "190":
                            case "191":
                            case "192":
                            case "193":
                            case "194":
                            case "196":
                            case "198":
                            case "199":
                            case "220":
                            case "930":
                                {
                                    //Temp = Temp;
                                }
                                break;
                            default:
                                break;
                        }
                        TVText = TVText + Temp;
                    }

                    Label lblSelected = new Label();
                    lblSelected.AutoSize = true;
                    lblSelected.Location = new Point(20, pos);
                    lblSelected.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblSelected.Font = new Font(new FontFamily(lblSelected.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblSelected.Text = $"{TVText}";

                    PanelViewAndEdit.Controls.Add(lblSelected);

                    pos = lblSelected.Bottom + 10;
                }
            }

        }
    }
}
