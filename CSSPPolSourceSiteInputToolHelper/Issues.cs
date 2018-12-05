using CSSPEnumsDLL.Enums;
using CSSPEnumsDLL.Services;
using CSSPModelsDLL.Models;
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
        public void AddIssue(int IssueID)
        {
            Issue issue = new Issue();
            issue.IssueID = IssueID;
            issue.LastUpdated_UTC = DateTime.Now;
            if (CurrentPSS.PSSObs.IssueList.Count > 0)
            {
                issue.Ordinal = CurrentPSS.PSSObs.IssueList.Max(c => c.Ordinal).Value + 1;
            }
            else
            {
                issue.Ordinal = 0;
            }
            issue.PolSourceObsInfoIntList = new List<int>();
            issue.ToRemove = false;

            CurrentPSS.PSSObs.IssueList.Add(issue);

            ReDrawPolSourceSite();
        }
        public bool IssueWellFormed(Issue issue, bool IsNew)
        {
            if (issue.ToRemove == true)
            {
                return true;
            }

            int ChildStart = 0;
            if (IsNew)
            {
                for (int i = 0, count = issue.PolSourceObsInfoIntListNew.Count - 2; i < count; i++)
                {
                    if (ChildStart != 0)
                    {
                        string obsEnum3Char = issue.PolSourceObsInfoIntListNew[i].ToString().Substring(0, 3);
                        string ChildStart3Char = ChildStart.ToString().Substring(0, 3);
                        if (obsEnum3Char != ChildStart3Char)
                        {
                            return false;
                        }
                    }

                    PolSourceObsInfoChild polSourceObsInfoChild = polSourceObsInfoChildList.Where(c => c.PolSourceObsInfo == ((PolSourceObsInfoEnum)issue.PolSourceObsInfoIntListNew[i])).FirstOrDefault<PolSourceObsInfoChild>();
                    if (polSourceObsInfoChild != null)
                    {
                        ChildStart = ((int)polSourceObsInfoChild.PolSourceObsInfoChildStart);
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                for (int i = 0, count = issue.PolSourceObsInfoIntList.Count - 2; i < count; i++)
                {
                    if (ChildStart != 0)
                    {
                        string obsEnum3Char = issue.PolSourceObsInfoIntList[i].ToString().Substring(0, 3);
                        string ChildStart3Char = ChildStart.ToString().Substring(0, 3);
                        if (obsEnum3Char != ChildStart3Char)
                        {
                            return false;
                        }
                    }

                    PolSourceObsInfoChild polSourceObsInfoChild = polSourceObsInfoChildList.Where(c => c.PolSourceObsInfo == ((PolSourceObsInfoEnum)issue.PolSourceObsInfoIntList[i])).FirstOrDefault<PolSourceObsInfoChild>();
                    if (polSourceObsInfoChild != null)
                    {
                        ChildStart = ((int)polSourceObsInfoChild.PolSourceObsInfoChildStart);
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        public bool IssueCompleted(Issue issue, bool IsNew)
        {
            if (issue.ToRemove == true)
            {
                return true;
            }

            if (IsNew)
            {
                if (issue.PolSourceObsInfoIntListNew.Count > 0)
                {
                    int obsEnumIntLast = issue.PolSourceObsInfoIntListNew[issue.PolSourceObsInfoIntListNew.Count - 1];

                    PolSourceObsInfoChild polSourceObsInfoChild = polSourceObsInfoChildList.Where(c => c.PolSourceObsInfo == ((PolSourceObsInfoEnum)obsEnumIntLast)).FirstOrDefault<PolSourceObsInfoChild>();
                    if (polSourceObsInfoChild == null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (issue.PolSourceObsInfoIntList.Count > 0)
                {
                    int obsEnumIntLast = issue.PolSourceObsInfoIntList[issue.PolSourceObsInfoIntList.Count - 1];

                    PolSourceObsInfoChild polSourceObsInfoChild = polSourceObsInfoChildList.Where(c => c.PolSourceObsInfo == ((PolSourceObsInfoEnum)obsEnumIntLast)).FirstOrDefault<PolSourceObsInfoChild>();
                    if (polSourceObsInfoChild == null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        public void DrawIssuesForViewing()
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

            if (IsEditing || ShowOnlyIssues)
            {
                PanelViewAndEdit.Controls.Clear();
                Y = 4;
            }
            else
            {
                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
            }

            X = 10;

            Label lblIssues = new Label();
            lblIssues.AutoSize = true;
            lblIssues.Location = new Point(X, Y);
            lblIssues.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblIssues.Font = new Font(new FontFamily(lblIssues.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
            lblIssues.Text = $"Issues";

            PanelViewAndEdit.Controls.Add(lblIssues);

            Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
            X = 10;

            if (WrittenDescription)
            {
                Label lblWrittenDesc = new Label();
                lblWrittenDesc.AutoSize = true;
                lblWrittenDesc.Location = new Point(X, Y);
                lblWrittenDesc.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblWrittenDesc.Font = new Font(new FontFamily(lblWrittenDesc.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                lblWrittenDesc.Text = $"Written Description: ";

                PanelViewAndEdit.Controls.Add(lblWrittenDesc);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 8;
                X = 30;

                Label lblWrittenDesc2 = new Label();
                lblWrittenDesc2.AutoSize = true;
                lblWrittenDesc2.Location = new Point(X, Y);
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

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
            }

            if (OldIssueText)
            {
                if (CurrentPSS.OldIssueTextList.Count > 0)
                {
                    X = 10;

                    Label lblOldIssue = new Label();
                    lblOldIssue.AutoSize = true;
                    lblOldIssue.Location = new Point(X, Y);
                    lblOldIssue.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblOldIssue.Font = new Font(new FontFamily(lblOldIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblOldIssue.Text = $"Old Issue Text:";

                    PanelViewAndEdit.Controls.Add(lblOldIssue);

                    Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 6;

                    foreach (string OldIssueText in CurrentPSS.OldIssueTextList)
                    {

                        X = 30;

                        Label lblOldIssueCount = new Label();
                        lblOldIssueCount.AutoSize = true;
                        lblOldIssueCount.Location = new Point(X, Y);
                        lblOldIssueCount.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblOldIssueCount.Font = new Font(new FontFamily(lblOldIssueCount.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                        lblOldIssueCount.Text = $" --- ";

                        PanelViewAndEdit.Controls.Add(lblOldIssueCount);

                        X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 0;

                        Label lblOldIssueText = new Label();
                        lblOldIssueText.AutoSize = true;
                        lblOldIssueText.Location = new Point(X, Y);
                        lblOldIssueText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblOldIssueText.Font = new Font(new FontFamily(lblOldIssueText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblOldIssueText.Text = $"{OldIssueText}";

                        PanelViewAndEdit.Controls.Add(lblOldIssueText);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                    }
                }
                else
                {
                    X = 30;

                    Label lblOldIssue = new Label();
                    lblOldIssue.AutoSize = true;
                    lblOldIssue.Location = new Point(X, Y);
                    lblOldIssue.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblOldIssue.Font = new Font(new FontFamily(lblOldIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    lblOldIssue.Text = $"No Old Issue Text:";

                    PanelViewAndEdit.Controls.Add(lblOldIssue);
                }

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
            }

            if (CurrentPSS.PSSObs.IssueList.Count > 0)
            {
                int IssueCount = 0;
                foreach (Issue issue in CurrentPSS.PSSObs.IssueList.OrderBy(c => c.Ordinal))
                {
                    bool IsWellFormed = IssueWellFormed(issue, false);
                    bool IsCompleted = IssueCompleted(issue, false);

                    IssueCount += 1;

                    X = 10;

                    if (DeletedIssueAndPicture || (DeletedIssueAndPicture == false && issue.ToRemove == false))
                    {
                        Label lblIssue = new Label();
                        lblIssue.AutoSize = true;
                        lblIssue.Location = new Point(X, Y);
                        lblIssue.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        lblIssue.Font = new Font(new FontFamily(lblIssue.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                        string Deleted = issue.ToRemove == true ? " --- (Deleted) " : "";
                        lblIssue.Text = $"Issue ({IssueCount}) {Deleted}";

                        PanelViewAndEdit.Controls.Add(lblIssue);

                        //X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 0;
                        //Label lblIssueLastUpdate = new Label();
                        //lblIssueLastUpdate.AutoSize = true;
                        //lblIssueLastUpdate.Location = new Point(X, Y);
                        //lblIssueLastUpdate.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                        //lblIssueLastUpdate.Font = new Font(new FontFamily(lblIssueLastUpdate.Font.FontFamily.Name).Name, 14f, FontStyle.Regular);
                        //lblIssueLastUpdate.Text = $"{((DateTime)issue.LastUpdated_UTC).ToString("yyyy MMMM dd HH:mm:ss")}";

                        //PanelViewAndEdit.Controls.Add(lblIssueLastUpdate);

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                        if (OldIssue)
                        {
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

                            X = 20;

                            Label lblSelected = new Label();
                            lblSelected.AutoSize = true;
                            lblSelected.Location = new Point(X, Y);
                            lblSelected.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                            if (issue.ToRemove == true)
                            {
                                lblSelected.Font = new Font(new FontFamily(lblSelected.Font.FontFamily.Name).Name, 10f, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            }
                            else
                            {
                                lblSelected.Font = new Font(new FontFamily(lblSelected.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                            }

                            if (!IsWellFormed)
                            {
                                lblSelected.BackColor = BackColorNotWellFormed;
                            }
                            else
                            {
                                if (!IsCompleted)
                                {
                                    lblSelected.BackColor = BackColorNotCompleted;
                                }
                            }

                            lblSelected.Text = $"{TVText}";

                            PanelViewAndEdit.Controls.Add(lblSelected);

                            Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                        }

                        if (NewIssue)
                        {
                            if (issue.PolSourceObsInfoIntListNew.Count > 0)
                            {
                                IsWellFormed = IssueWellFormed(issue, true);
                                IsCompleted = IssueCompleted(issue, true);

                                X = 20;

                                Label lblChangedOrNew = new Label();
                                lblChangedOrNew.AutoSize = true;
                                lblChangedOrNew.Location = new Point(X, Y);
                                lblChangedOrNew.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                                lblChangedOrNew.Font = new Font(new FontFamily(lblChangedOrNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                                lblChangedOrNew.Text = $"----------------------- Changed to ----------------------------------------------------";

                                PanelViewAndEdit.Controls.Add(lblChangedOrNew);

                                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;

                                X = 30;

                                string TVText = "";
                                for (int i = 0, count = issue.PolSourceObsInfoIntListNew.Count; i < count; i++)
                                {
                                    string Temp = _BaseEnumService.GetEnumText_PolSourceObsInfoReportEnum((PolSourceObsInfoEnum)issue.PolSourceObsInfoIntListNew[i]);
                                    switch ((issue.PolSourceObsInfoIntListNew[i].ToString()).Substring(0, 3))
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

                                X = 20;

                                Label lblSelectedNew = new Label();
                                lblSelectedNew.AutoSize = true;
                                lblSelectedNew.Location = new Point(X, Y);
                                lblSelectedNew.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                                if (issue.ToRemove == true)
                                {
                                    lblSelectedNew.Font = new Font(new FontFamily(lblSelectedNew.Font.FontFamily.Name).Name, 10f, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                }
                                else
                                {
                                    lblSelectedNew.Font = new Font(new FontFamily(lblSelectedNew.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                                }
                                lblSelectedNew.ForeColor = ForeColorChangedOrNew;

                                if (!IsWellFormed)
                                {
                                    lblSelectedNew.BackColor = BackColorNotWellFormed;
                                }
                                else
                                {
                                    if (!IsCompleted)
                                    {
                                        lblSelectedNew.BackColor = BackColorNotCompleted;
                                    }
                                }

                                lblSelectedNew.Text = $"{TVText}";

                                PanelViewAndEdit.Controls.Add(lblSelectedNew);

                                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
                            }

                            if (!IsEditing)
                            {
                                #region Extra Comment
                                X = 10;
                                DrawItemTextMultiline(X, Y, issue.ExtraComment, issue.ExtraCommentNew, "Extra Comment", $"textBoxExtraComment_{issue.IssueID}", 500);

                                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
                                #endregion Extra Comment

                            }
                        }
                    }
                }
            }

            if (IsEditing)
            {
                Label lblWarning = new Label();
                lblWarning.AutoSize = true;
                lblWarning.Location = new Point(X, Y);
                lblWarning.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblWarning.Font = new Font(new FontFamily(lblWarning.Font.FontFamily.Name).Name, 14f, FontStyle.Bold);
                lblWarning.ForeColor = Color.Red;
                lblWarning.Text = "Please make sure the observation date is correct";

                PanelViewAndEdit.Controls.Add(lblWarning);

                Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 4;

                DrawIssueButtonsEditing();
                DrawCurrentIssueEditing();
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
        public void DrawIssueButtonsEditing()
        {
            int Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 50;
            int X = 50;

            int countIssue = 0;
            foreach (Issue issue in CurrentPSS.PSSObs.IssueList.OrderBy(c => c.Ordinal))
            {
                if (countIssue == 0)
                {
                    if (IssueID == 0)
                    {
                        IssueID = (int)issue.IssueID;
                    }
                }

                if (DeletedIssueAndPicture || (DeletedIssueAndPicture == false && issue.ToRemove == false))
                {
                    countIssue += 1;
                    Button butIssue = new Button();
                    butIssue.AutoSize = true;
                    butIssue.Location = new Point(X, Y);
                    butIssue.Font = new Font(new FontFamily(butIssue.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                    string Deleted = issue.ToRemove == true ? "   (Deleted)" : "";
                    butIssue.Text = $"Issue {countIssue}{Deleted}";
                    butIssue.Tag = $"{issue.IssueID}";
                    butIssue.Padding = new Padding(5);

                    if (issue.IssueID == IssueID)
                    {
                        butIssue.BackColor = BackColorEditing;
                    }

                    butIssue.Click += butIssueSet_Click;

                    PanelViewAndEdit.Controls.Add(butIssue);

                    X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;

                    if (countIssue > 1)
                    {
                        Button butIssueMoveLeft = new Button();
                        //butIssueMoveLeft.AutoSize = true;
                        butIssueMoveLeft.Location = new Point(butIssue.Left, butIssue.Top - 30);
                        butIssueMoveLeft.Size = new Size(butIssue.Width / 2, 30);
                        butIssueMoveLeft.Font = new Font(new FontFamily(butIssueMoveLeft.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butIssueMoveLeft.Text = $"←";
                        butIssueMoveLeft.Tag = $"{issue.IssueID}";
                        //butIssueMoveLeft.Padding = new Padding(5);
                        butIssueMoveLeft.Click += butIssueMoveLeft_Click;

                        PanelViewAndEdit.Controls.Add(butIssueMoveLeft);

                    }
                    if (countIssue < CurrentPSS.PSSObs.IssueList.Count)
                    {
                        Button butIssueMoveRight = new Button();
                        //butIssueMoveRight.AutoSize = true;
                        butIssueMoveRight.Location = new Point((butIssue.Right + butIssue.Left) / 2, butIssue.Top - 30);
                        butIssueMoveRight.Size = new Size(butIssue.Width / 2, 30);
                        butIssueMoveRight.Font = new Font(new FontFamily(butIssueMoveRight.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        butIssueMoveRight.Text = $"→";
                        butIssueMoveRight.Tag = $"{issue.IssueID}";
                        //butIssueMoveRight.Padding = new Padding(5);
                        butIssueMoveRight.Click += butIssueMoveRight_Click;

                        PanelViewAndEdit.Controls.Add(butIssueMoveRight);
                    }
                }
            }

            int MaxIssueID = 10000000;
            if (CurrentPSS.PSSObs.IssueList.Count > 0)
            {
                int Max = CurrentPSS.PSSObs.IssueList.Max(c => c.IssueID).Value;
                if (Max >= MaxIssueID)
                {
                    MaxIssueID = Max + 1;
                }
            }
            Button butIssueAdd = new Button();
            butIssueAdd.AutoSize = true;
            butIssueAdd.Location = new Point(X, Y);
            butIssueAdd.Font = new Font(new FontFamily(butIssueAdd.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
            butIssueAdd.Text = $"Add New Issue";
            butIssueAdd.Tag = $"{MaxIssueID}";
            butIssueAdd.Padding = new Padding(5);
            butIssueAdd.Click += butIssueAdd_Click;

            PanelViewAndEdit.Controls.Add(butIssueAdd);

            X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;

        }
        public void DrawCurrentIssueEditing()
        {
            int Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
            int X = 10;

            Issue issue = CurrentPSS.PSSObs.IssueList.Where(c => c.IssueID == IssueID).FirstOrDefault();
            if (issue == null)
            {
                Label lblNoIssue = new Label();
                lblNoIssue.AutoSize = true;
                lblNoIssue.Location = new Point(X, Y);
                lblNoIssue.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblNoIssue.Font = new Font(new FontFamily(lblNoIssue.Font.FontFamily.Name).Name, 12f, FontStyle.Regular);
                lblNoIssue.Text = $"No Issue";

                PanelViewAndEdit.Controls.Add(lblNoIssue);
            }

            if (issue != null && issue.PolSourceObsInfoIntList.Count == 0)
            {
                issue.PolSourceObsInfoIntList.Add(10101); /* Humand resource as default */
            }

            if (issue != null)
            {
                DrawCurrentIssueEditingOptions(issue);
                DrawCurrentIssueEditingButtons(issue);
            }
        }
        public void DrawCurrentIssueEditingButtons(Issue issue)
        {
            int Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
            int X = 100;

            #region Extra Comment
            X = 10;
            DrawItemTextMultiline(X, Y, issue.ExtraComment, issue.ExtraCommentNew, "Extra Comment", $"textBoxExtraComment_{issue.IssueID}", 500);

            Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 10;
            #endregion Extra Comment

            if (issue.ToRemove == true)
            {
                // don't show anyting if issue is removed
            }
            else
            {

                Button butIssueSave = new Button();
                butIssueSave.AutoSize = true;
                butIssueSave.Location = new Point(X, Y);
                butIssueSave.Font = new Font(new FontFamily(butIssueSave.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                butIssueSave.Text = $"Save Issue";
                butIssueSave.Tag = $"{issue.IssueID}";
                butIssueSave.Padding = new Padding(5);
                butIssueSave.Click += butIssueSave_Click;

                PanelViewAndEdit.Controls.Add(butIssueSave);

                X = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Right + 20;

                Button butIssueDelete = new Button();
                butIssueDelete.AutoSize = true;
                butIssueDelete.Location = new Point(X, Y);
                butIssueDelete.Font = new Font(new FontFamily(butIssueDelete.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                butIssueDelete.Text = $"Delete Issue";
                butIssueDelete.Tag = $"{issue.IssueID}";
                butIssueDelete.Padding = new Padding(5);
                butIssueDelete.Click += butIssueDelete_Click;

                PanelViewAndEdit.Controls.Add(butIssueDelete);
            }

        }
        public void DrawCurrentIssueEditingOptions(Issue issue)
        {
            int Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;
            int X = 10;

            if (issue.ToRemove == true)
            {
                X = 100;

                Button butIssueDelete = new Button();
                butIssueDelete.AutoSize = true;
                butIssueDelete.Location = new Point(X, Y);
                butIssueDelete.Font = new Font(new FontFamily(butIssueDelete.Font.FontFamily.Name).Name, 10f, FontStyle.Bold);
                butIssueDelete.Text = $"Undelete Issue";
                butIssueDelete.Tag = $"{issue.IssueID}";
                butIssueDelete.Padding = new Padding(5);
                butIssueDelete.Click += butIssueUnDelete_Click;


                PanelViewAndEdit.Controls.Add(butIssueDelete);
            }
            else
            {
                int obsEnumPrev = 0;
                if (issue.PolSourceObsInfoIntListNew.Count > 0)
                {
                    foreach (int obsEnum in issue.PolSourceObsInfoIntListNew)
                    {

                        DrawPanelIssue(issue, obsEnum, obsEnumPrev);
                        obsEnumPrev = obsEnum;

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 15;
                        X = 10;

                    }
                }
                else
                {
                    foreach (int obsEnum in issue.PolSourceObsInfoIntList)
                    {

                        DrawPanelIssue(issue, obsEnum, obsEnumPrev);
                        obsEnumPrev = obsEnum;

                        Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 15;
                        X = 10;

                    }
                }
            }
        }

        private void DrawPanelIssue(Issue issue, int obsEnum, int obsEnumPrev)
        {
            int YPanel = 10;
            int X = 10;
            int Y = PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1].Bottom + 20;

            int StartObsEnum = int.Parse(obsEnum.ToString().Substring(0, 3)) * 100;

            string tempText = _BaseEnumService.GetEnumText_PolSourceObsInfoEnum(((PolSourceObsInfoEnum)StartObsEnum)).Trim();
            string tempTextDesc = _BaseEnumService.GetEnumText_PolSourceObsInfoDescEnum(((PolSourceObsInfoEnum)StartObsEnum)).Trim();
            if (tempText.IndexOf("|") > 0)
            {
                tempText = tempText.Substring(0, tempText.IndexOf("|"));
            }
            List<PolSourceObsInfoEnumTextAndID> polSourceObsInfoEnumTextAndIDSubList = polSourceObsInfoEnumTextAndIDList.Where(c => c.ID > StartObsEnum && c.ID < (StartObsEnum + 99)).OrderBy(c => c.Text).ToList();

            Panel panelOptions = new Panel();
            panelOptions.Location = new Point(0, Y);
            panelOptions.AutoSize = true;
            panelOptions.TabIndex = 0;
            panelOptions.Tag = $"{issue.IssueID},{StartObsEnum}";
            //panelOptions.BorderStyle = BorderStyle.FixedSingle;

            Label lblIssueText = new Label();
            lblIssueText.AutoSize = true;
            lblIssueText.Location = new Point(X, YPanel);
            lblIssueText.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
            lblIssueText.Font = new Font(new FontFamily(lblIssueText.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
            lblIssueText.Tag = tempTextDesc;
            lblIssueText.Text = $"{tempText}";
            lblIssueText.Click += lblIssueText_Click;

            panelOptions.Controls.Add(lblIssueText);

            if (MoreInfo)
            {
                YPanel = panelOptions.Controls[panelOptions.Controls.Count - 1].Bottom + 10;
                X = 20;

                Label lblIssueTextMoreInfo = new Label();
                lblIssueTextMoreInfo.AutoSize = true;
                lblIssueTextMoreInfo.Location = new Point(X, YPanel);
                lblIssueTextMoreInfo.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblIssueTextMoreInfo.Font = new Font(new FontFamily(lblIssueText.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblIssueTextMoreInfo.Text = $"{tempTextDesc}";

                panelOptions.Controls.Add(lblIssueTextMoreInfo);

            }

            YPanel = panelOptions.Controls[panelOptions.Controls.Count - 1].Bottom + 15;
            X = 30;

            PolSourceObsInfoChild polSourceObsInfoChild = polSourceObsInfoChildList.Where(c => c.PolSourceObsInfo == ((PolSourceObsInfoEnum)obsEnumPrev)).FirstOrDefault<PolSourceObsInfoChild>();

            if (polSourceObsInfoChild != null)
            {
                string obsEnum3Char = obsEnum.ToString().Substring(0, 3);
                string ChildStart3Char = ((int)polSourceObsInfoChild.PolSourceObsInfoChildStart).ToString().Substring(0, 3);
                if (obsEnum3Char != ChildStart3Char)
                {
                    Label lblError = new Label();
                    lblError.AutoSize = true;
                    lblError.Location = new Point(X, YPanel);
                    lblError.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblError.Font = new Font(new FontFamily(lblError.Font.FontFamily.Name).Name, 12f, FontStyle.Bold);
                    lblError.Tag = tempTextDesc;
                    lblError.Text = $"ChildStart [{ChildStart3Char}] not equal to obsEnum [{obsEnum3Char}]";

                    panelOptions.Controls.Add(lblError);

                    YPanel = panelOptions.Controls[panelOptions.Controls.Count - 1].Bottom + 15;

                }
            }

            List<int> HideList = new List<int>();
            PolSourceObsInfoEnumHideAndID polSourceObsInfoEnumHideAndID = polSourceObsInfoEnumHideAndIDList.Where(c => c.ID == obsEnumPrev).FirstOrDefault();
            if (polSourceObsInfoEnumHideAndID != null)
            {
                string Hide = polSourceObsInfoEnumHideAndID.Hide;
                if (!string.IsNullOrWhiteSpace(Hide))
                {
                    HideList = Hide.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)).ToList();
                }
            }

            foreach (PolSourceObsInfoEnumTextAndID polSourceObsInfoEnumTextAndIDSub in polSourceObsInfoEnumTextAndIDSubList)
            {
                if (HideList.Contains(polSourceObsInfoEnumTextAndIDSub.ID))
                {
                    continue;
                }

                Label lblIssueText2 = new Label();
                lblIssueText2.AutoSize = true;
                lblIssueText2.Location = new Point(X, YPanel);
                lblIssueText2.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                lblIssueText2.Font = new Font(new FontFamily(lblIssueText2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                lblIssueText2.BorderStyle = BorderStyle.FixedSingle;
                lblIssueText2.Padding = new Padding(5);
                lblIssueText2.Click += lblIssueText2_Click;

                if (issue.PolSourceObsInfoIntListNew.Count > 0)
                {
                    if (issue.PolSourceObsInfoIntListNew.Contains(polSourceObsInfoEnumTextAndIDSub.ID))
                    {
                        lblIssueText2.Font = new Font(new FontFamily(lblIssueText2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblIssueText2.BackColor = BackColorEditing;
                    }
                }
                else
                {
                    if (issue.PolSourceObsInfoIntList.Contains(polSourceObsInfoEnumTextAndIDSub.ID))
                    {
                        lblIssueText2.Font = new Font(new FontFamily(lblIssueText2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                        lblIssueText2.BackColor = BackColorEditing;
                    }
                }

                string DescText = "";
                PolSourceObsInfoEnumTextAndID polSourceObsInfoEnumDescTextAndID = polSourceObsInfoEnumDescTextAndIDList.Where(c => c.ID == polSourceObsInfoEnumTextAndIDSub.ID).FirstOrDefault();
                if (polSourceObsInfoEnumDescTextAndID != null)
                {
                    DescText = (polSourceObsInfoEnumDescTextAndID.Text == "Error" || polSourceObsInfoEnumDescTextAndID.Text == "Erreur" ? "" : polSourceObsInfoEnumDescTextAndID.Text);
                }

                lblIssueText2.Tag = $"{((int)((PolSourceObsInfoEnum)polSourceObsInfoEnumTextAndIDSub.ID))}";
                lblIssueText2.Text = $"{polSourceObsInfoEnumTextAndIDSub.Text.Trim()}";

                panelOptions.Controls.Add(lblIssueText2);
                if (lblIssueText2.Right > (PanelViewAndEdit.Width - 20))
                {
                    YPanel = panelOptions.Controls[panelOptions.Controls.Count - 1].Bottom + 15;
                    X = 30;
                    lblIssueText2.Left = X;
                    lblIssueText2.Top = YPanel;
                }

                if (MoreInfo)
                {
                    X = panelOptions.Controls[panelOptions.Controls.Count - 1].Right + 10;

                    Label lblIssueTextMoreInfo2 = new Label();
                    lblIssueTextMoreInfo2.AutoSize = true;
                    lblIssueTextMoreInfo2.Location = new Point(X, YPanel);
                    lblIssueTextMoreInfo2.MaximumSize = new Size(PanelViewAndEdit.Width * 9 / 10, 0);
                    lblIssueTextMoreInfo2.Font = new Font(new FontFamily(lblIssueTextMoreInfo2.Font.FontFamily.Name).Name, 10f, FontStyle.Regular);
                    lblIssueTextMoreInfo2.Padding = new Padding(5);
                    lblIssueTextMoreInfo2.Text = $"{DescText}";

                    panelOptions.Controls.Add(lblIssueTextMoreInfo2);

                    YPanel = panelOptions.Controls[panelOptions.Controls.Count - 1].Bottom + 15;
                    X = 30;

                }
                else
                {
                    X = panelOptions.Controls[panelOptions.Controls.Count - 1].Right + 5;
                }
            }

            PanelViewAndEdit.Controls.Add(panelOptions);
        }
        private void DrawAfterLabelSelectd(Label labelSelected)
        {
            List<string> tagLabelStrList = labelSelected.Tag.ToString().Split(",".ToCharArray(), StringSplitOptions.None).ToList();
            int IssueEnumID = int.Parse(tagLabelStrList[0]);

            Panel currentPanel = ((Panel)labelSelected.Parent);
            Panel prevPanel = null;

            List<string> tagPanelStrList = currentPanel.Tag.ToString().Split(",".ToCharArray(), StringSplitOptions.None).ToList();
            int IssueID = int.Parse(tagPanelStrList[0]);
            string StartObsEnum = tagPanelStrList[1];

            foreach (Control control in currentPanel.Controls)
            {
                if (control.GetType().Name == "Label")
                {
                    control.BackColor = Color.FromName("Control");
                    if (control == labelSelected)
                    {
                        control.BackColor = BackColorEditing;
                    }
                }
            }

            List<int> controlIntToDeleteList = new List<int>();
            for (int i = 0, count = PanelViewAndEdit.Controls.Count; i < count; i++)
            {
                if (currentPanel.Top > PanelViewAndEdit.Controls[i].Top)
                {
                    if (PanelViewAndEdit.Controls[i].GetType().Name == "Panel")
                    {
                        prevPanel = (Panel)PanelViewAndEdit.Controls[i];
                    }
                }
                if (currentPanel.Top < PanelViewAndEdit.Controls[i].Top)
                {
                    controlIntToDeleteList.Add(i);
                }
            }

            foreach (int i in controlIntToDeleteList.OrderByDescending(c => c))
            {
                PanelViewAndEdit.Controls.RemoveAt(i);
            }

            PolSourceObsInfoChild polSourceObsInfoChild = polSourceObsInfoChildList.Where(c => c.PolSourceObsInfo == ((PolSourceObsInfoEnum)IssueEnumID)).FirstOrDefault<PolSourceObsInfoChild>();

            Issue currentIssue = CurrentPSS.PSSObs.IssueList.Where(c => c.IssueID == IssueID).FirstOrDefault();
            if (currentIssue != null)
            {
                int IssuePrevEnumID = 0;
                if (prevPanel != null)
                {
                    foreach (Control control in prevPanel.Controls)
                    {
                        if (control.GetType().Name == "Label")
                        {
                            if (control.BackColor == BackColorEditing)
                            {
                                Label prevLabel = ((Label)control);
                                List<string> tagPrevLabelStrList = prevLabel.Tag.ToString().Split(",".ToCharArray(), StringSplitOptions.None).ToList();
                                IssuePrevEnumID = int.Parse(tagPrevLabelStrList[0]);

                            }
                        }
                    }
                }

                if (polSourceObsInfoChild != null)
                {
                    DrawPanelIssue(currentIssue, ((int)polSourceObsInfoChild.PolSourceObsInfoChildStart), IssueEnumID);
                }
            }
            DrawCurrentIssueEditingButtons(currentIssue);

            PanelViewAndEdit.ScrollControlIntoView(PanelViewAndEdit.Controls[PanelViewAndEdit.Controls.Count - 1]);

        }
        private void SaveIssue(int IssueID)
        {
            List<int> obsEnumListNew = new List<int>();
            Issue issue = CurrentPSS.PSSObs.IssueList.Where(c => c.IssueID == IssueID).FirstOrDefault();

            if (issue != null)
            {
                List<Label> controlLabelGreenList = new List<Label>();
                foreach (Control control in PanelViewAndEdit.Controls)
                {
                    if (control.GetType().Name == "Panel")
                    {
                        foreach (Control control2 in control.Controls)
                        {
                            if (control2.GetType().Name == "Label")
                            {
                                if (control2.BackColor == BackColorEditing)
                                {
                                    controlLabelGreenList.Add(((Label)control2));
                                }
                            }
                        }
                    }
                }

                obsEnumListNew = (from c in controlLabelGreenList
                                  let top = c.Top + c.Parent.Top
                                  let tagID = int.Parse(c.Tag.ToString())
                                  orderby top
                                  select tagID).ToList();

                issue.PolSourceObsInfoIntListNew = obsEnumListNew;

                foreach (Control control in PanelViewAndEdit.Controls)
                {
                    if (control.Name.StartsWith("textBoxExtraComment_"))
                    {
                        int ID = int.Parse(control.Name.Replace("textBoxExtraComment_", ""));
                        if (ID == issue.IssueID)
                        {
                            if (control.Text == issue.ExtraComment)
                            {
                                issue.ExtraCommentNew = null;
                            }
                            else
                            {
                                issue.ExtraCommentNew = control.Text;
                            }
                        }
                    }
                }
            }

            RecreateTVText();
            SaveSubsectorTextFile();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();


        }
        private void RecreateTVText()
        {
            if (CurrentPSS.PSSObs.IssueList.Count > 0)
            {
                Issue issue = CurrentPSS.PSSObs.IssueList.OrderBy(c => c.Ordinal).Where(c => c.ToRemove == false).FirstOrDefault();

                if (issue != null)
                {
                    string TVText = "";
                    if (issue.PolSourceObsInfoIntListNew.Count > 0)
                    {
                        for (int i = 0, count = issue.PolSourceObsInfoIntListNew.Count; i < count; i++)
                        {
                            string StartTxt = issue.PolSourceObsInfoIntListNew[i].ToString().Substring(0, 3);

                            if (startWithList.Where(c => c.StartsWith(StartTxt)).Any())
                            {
                                TVText = TVText.Trim();
                                string TempText = _BaseEnumService.GetEnumText_PolSourceObsInfoEnum((PolSourceObsInfoEnum)issue.PolSourceObsInfoIntListNew[i]);
                                if (TempText.IndexOf("|") > 0)
                                {
                                    TempText = TempText.Substring(0, TempText.IndexOf("|")).Trim();
                                }
                                TVText = TVText + (TVText.Length == 0 ? "" : ", ") + TempText;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0, count = issue.PolSourceObsInfoIntList.Count; i < count; i++)
                        {
                            string StartTxt = issue.PolSourceObsInfoIntList[i].ToString().Substring(0, 3);

                            if (startWithList.Where(c => c.StartsWith(StartTxt)).Any())
                            {
                                TVText = TVText.Trim();
                                string TempText = _BaseEnumService.GetEnumText_PolSourceObsInfoEnum((PolSourceObsInfoEnum)issue.PolSourceObsInfoIntList[i]);
                                if (TempText.IndexOf("|") > 0)
                                {
                                    TempText = TempText.Substring(0, TempText.IndexOf("|")).Trim();
                                }
                                TVText = TVText + (TVText.Length == 0 ? "" : ", ") + TempText;
                            }
                        }
                    }

                    while (TVText.Contains("  "))
                    {
                        TVText = TVText.Replace("  ", " ");
                    }

                    TVText = "P00000".Substring(0, "P00000".Length - CurrentPSS.SiteNumber.ToString().Length) + CurrentPSS.SiteNumber.ToString() + " - " + TVText;

                    CurrentPSS.TVTextNew = TVText;
                }
                else
                {
                    string TVText = "PSS Empty - " + "P00000".Substring(0, "P00000".Length - CurrentPSS.SiteNumber.ToString().Length) + CurrentPSS.SiteNumber.ToString();

                    CurrentPSS.TVTextNew = TVText;
                }
            }
        }
        private void DeleteIssue(int IssueID)
        {
            List<int> obsEnumListNew = new List<int>();
            Issue issue = CurrentPSS.PSSObs.IssueList.Where(c => c.IssueID == IssueID).FirstOrDefault();

            if (issue != null)
            {
                issue.ToRemove = true;
            }

            RecreateTVText();
            SaveSubsectorTextFile();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void UnDeleteIssue(int IssueID)
        {
            List<int> obsEnumListNew = new List<int>();
            Issue issue = CurrentPSS.PSSObs.IssueList.Where(c => c.IssueID == IssueID).FirstOrDefault();

            if (issue != null)
            {
                issue.ToRemove = false;
            }

            RecreateTVText();
            SaveSubsectorTextFile();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void IssueMoveLeft(int IssueID)
        {
            List<int> obsEnumListNew = new List<int>();
            Issue issue = CurrentPSS.PSSObs.IssueList.Where(c => c.IssueID == IssueID).FirstOrDefault();

            if (issue != null)
            {
                for (int i = 0, count = CurrentPSS.PSSObs.IssueList.Count; i < count; i++)
                {
                    if (CurrentPSS.PSSObs.IssueList[i].IssueID == issue.IssueID)
                    {
                        int tempOrdinal = (int)CurrentPSS.PSSObs.IssueList[i].Ordinal;
                        Issue tempIssue = CurrentPSS.PSSObs.IssueList.OrderByDescending(c => c.Ordinal).Where(c => c.Ordinal < tempOrdinal).FirstOrDefault();
                        if (tempIssue != null)
                        {
                            CurrentPSS.PSSObs.IssueList[i].Ordinal = (int)tempIssue.Ordinal;
                            tempIssue.Ordinal = tempOrdinal;
                        }
                    }
                }
            }

            RecreateTVText();
            SaveSubsectorTextFile();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void IssueMoveRight(int IssueID)
        {
            List<int> obsEnumListNew = new List<int>();
            Issue issue = CurrentPSS.PSSObs.IssueList.Where(c => c.IssueID == IssueID).FirstOrDefault();

            if (issue != null)
            {
                for (int i = 0, count = CurrentPSS.PSSObs.IssueList.Count; i < count; i++)
                {
                    if (CurrentPSS.PSSObs.IssueList[i].IssueID == issue.IssueID)
                    {
                        int tempOrdinal = (int)CurrentPSS.PSSObs.IssueList[i].Ordinal;
                        Issue tempIssue = CurrentPSS.PSSObs.IssueList.OrderBy(c => c.Ordinal).Where(c => c.Ordinal > tempOrdinal).FirstOrDefault();
                        if (tempIssue != null)
                        {
                            CurrentPSS.PSSObs.IssueList[i].Ordinal = (int)tempIssue.Ordinal;
                            tempIssue.Ordinal = tempOrdinal;
                        }
                    }
                }
            }

            RecreateTVText();
            SaveSubsectorTextFile();
            RedrawSinglePanelPSS();
            ReDrawPolSourceSite();
        }
        private void IssuesSaveToCSSPWebTools()
        {
            MessageBox.Show("IssuesSaveToCSSPWebTools " + CurrentPSS.PSSTVItemID.ToString(), PolSourceSiteTVItemID.ToString());
        }

    }
}
