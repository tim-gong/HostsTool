using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HostsTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BindTree()
        {
            this.treeView.Nodes.Clear();
            StreamReader reader = new StreamReader(this.hostsFilePath, Encoding.GetEncoding("gb2312"));
            string s = "";
            TreeNode node = null;
            TreeNode node2 = null;
            while ((s = reader.ReadLine()) != null)
            {
                Match match = this.CheckIsGroupLine(s);
                if (match != null)
                {
                    node = new TreeNode
                    {
                        Text = this.ProcessLineStr(match.Groups[0].Value),
                        Tag = match.Groups[1].Value
                    };
                    this.treeView.Nodes.Add(node);
                }
                else
                {
                    node2 = new TreeNode
                    {
                        Text = this.ProcessLineStr(s)
                    };
                    if (node != null)
                    {
                        node2.Tag = node.Tag.ToString() + "_child";
                        node.Nodes.Add(node2);
                        continue;
                    }
                    this.treeView.Nodes.Add(node2);
                }
            }
            reader.Close();
            reader.Dispose();
            foreach (TreeNode node3 in this.treeView.Nodes)
            {
                node3.ExpandAll();
            }
            this.treeView.SelectedNode = this.treeView.Nodes[0];
        }

        private void btnOpenHosts_Click(object sender, EventArgs e)
        {
            new Process { StartInfo = { FileName = "notepad.exe", Arguments = this.hostsFilePath } }.Start();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.BindTree();
        }

        private void btnSetDisable_Click(object sender, EventArgs e)
        {
            this.SetEnableOrDisable("disable", this.treeView.SelectedNode);
            this.SaveHost();
        }

        private void btnSetEnable_Click(object sender, EventArgs e)
        {
            this.SetEnableOrDisable("enable", this.treeView.SelectedNode);
            this.SaveHost();
        }

        private void btnSetGroupNum_Click(object sender, EventArgs e)
        {

        }

        private void btnSetNotTogether_Click(object sender, EventArgs e)
        {

        }

        private bool CheckGroupIsDisable(TreeNodeCollection treeNodes, ref int disableNum, ref int nullNum)
        {
            int num = 0;
            int num2 = 0;
            Regex regex = new Regex(@"^\s*$");
            Match match = null;
            Regex regex2 = new Regex(@"^\s*#+");
            Match match2 = null;
            foreach (TreeNode node in treeNodes)
            {
                match2 = regex2.Match(node.Text);
                match = regex.Match(node.Text);
                if (match2.Success)
                {
                    num++;
                }
                else if (match.Success)
                {
                    num2++;
                }
            }
            disableNum = num;
            nullNum = num2;
            return ((num + num2) == treeNodes.Count);
        }

        private Match CheckIsGroupLine(string s)
        {
            Match match = new Regex(@"#Group_(.*?)\s(.*)").Match(s);
            if (match.Success)
            {
                return match;
            }
            return null;
        }

        private string GetGroupNotTogether(string groupNum)
        {
            Match match = new Regex(@"#(\[.*\])+").Match(this.GetHostText());
            if (!match.Success)
            {
                return "";
            }
            MatchCollection matchs = Regex.Matches(match.Groups[1].Value, @"\[(.*?)\]");
            string str = "";
            foreach (Match match2 in matchs)
            {
                str = match2.Groups[1].Value;
                string[] strArray = str.Split(new char[] { ',' });
                bool flag = false;
                foreach (string str2 in strArray)
                {
                    if (str2 == groupNum)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    return str;
                }
                str = "";
            }
            return str;
        }

        private string GetHostText()
        {
            string str = "";
            StreamReader reader = new StreamReader(this.hostsFilePath, Encoding.GetEncoding("gb2312"));
            str = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
            return str;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.BindTree();
            this.btnSetGroupNum.Visible = false;
            this.btnSetNotTogether.Visible = false;
        }

        private string ProcessLineStr(string s)
        {
            return Regex.Replace(s, @"\t+", "     ");
        }

        private void SaveHost()
        {
            try
            {
                StreamWriter writer = new StreamWriter(this.hostsFilePath, false, Encoding.GetEncoding("gb2312"));
                foreach (TreeNode node in this.treeView.Nodes)
                {
                    writer.WriteLine(node.Text);
                    if (node.Nodes.Count > 0)
                    {
                        foreach (TreeNode node2 in node.Nodes)
                        {
                            writer.WriteLine(node2.Text);
                        }
                        continue;
                    }
                }
                writer.Close();
                writer.Dispose();
            }
            catch
            {
                MessageBox.Show("没有保存权限，保存失败");
                this.BindTree();
            }
        }

        private void SetEnableOrDisable(string opType, TreeNode node)
        {
            if (this.CheckIsGroupLine(node.Text) != null)
            {
                if (opType == "disable")
                {
                    foreach (TreeNode node2 in node.Nodes)
                    {
                        if (!(node2.Text.Trim() == ""))
                        {
                            node2.Text = Regex.Replace(node2.Text, @"^\s*#*", "#");
                        }
                    }
                }
                else if (opType == "enable")
                {
                    foreach (TreeNode node3 in node.Nodes)
                    {
                        if (!(node3.Text.Trim() == ""))
                        {
                            node3.Text = Regex.Replace(node3.Text, @"^\s*#+", "");
                        }
                    }
                    foreach (string str2 in this.GetGroupNotTogether(node.Tag.ToString()).Split(new char[] { ',' }))
                    {
                        if (str2 != node.Tag.ToString())
                        {
                            foreach (TreeNode node4 in this.treeView.Nodes)
                            {
                                if (node4.Nodes.Count > 0)
                                {
                                    foreach (TreeNode node5 in node4.Nodes)
                                    {
                                        if ((node5.Tag != null) && (node5.Tag.ToString() == (str2 + "_child")))
                                        {
                                            this.SetEnableOrDisable("disable", node5);
                                        }
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (node.Text.Trim() == "")
                {
                    return;
                }
                if (opType == "disable")
                {
                    node.Text = Regex.Replace(node.Text, @"^\s*#*", "#");
                }
                else if (opType == "enable")
                {
                    node.Text = Regex.Replace(node.Text, @"^\s*#+", "");
                }
            }
            this.treeView.Refresh();
        }

        private void treeView_DoubleClick(object sender, EventArgs e)
        {
            if (this.CheckIsGroupLine(this.treeView.SelectedNode.Text) == null)
            {
                if (Regex.Match(this.treeView.SelectedNode.Text, @"^\s*#+").Success)
                {
                    this.btnSetEnable_Click(sender, e);
                }
                else
                {
                    this.btnSetDisable_Click(sender, e);
                }
            }
            else
            {
                int disableNum = 0;
                int nullNum = 0;
                if (this.CheckGroupIsDisable(this.treeView.SelectedNode.Nodes, ref disableNum, ref nullNum))
                {
                    this.btnSetEnable_Click(sender, e);
                }
                else
                {
                    this.btnSetDisable_Click(sender, e);
                }
                this.treeView.SelectedNode.Expand();
            }
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point point2 = new Point
                {
                    X = e.X,
                    Y = e.Y
                };
                Point pt = point2;
                TreeNode nodeAt = this.treeView.GetNodeAt(pt);
                if (nodeAt != null)
                {
                    this.treeView.SelectedNode = nodeAt;
                    Regex regex = new Regex(@"^\s*#+");
                    if (nodeAt.Nodes.Count > 0)
                    {
                        int disableNum = 0;
                        int nullNum = 0;
                        if (this.CheckGroupIsDisable(nodeAt.Nodes, ref disableNum, ref nullNum))
                        {
                            this.btnSetEnable.Visible = true;
                            this.btnSetDisable.Visible = false;
                        }
                        else if (disableNum == 0)
                        {
                            this.btnSetEnable.Visible = false;
                            this.btnSetDisable.Visible = true;
                        }
                        else
                        {
                            this.btnSetEnable.Visible = true;
                            this.btnSetDisable.Visible = true;
                        }
                    }
                    else if (nodeAt.Text.Trim() == "")
                    {
                        this.btnSetEnable.Visible = false;
                        this.btnSetDisable.Visible = false;
                    }
                    else if (regex.Match(nodeAt.Text).Success)
                    {
                        this.btnSetEnable.Visible = true;
                        this.btnSetDisable.Visible = false;
                    }
                    else
                    {
                        this.btnSetEnable.Visible = false;
                        this.btnSetDisable.Visible = true;
                    }
                }
            }
        }
    }
}
