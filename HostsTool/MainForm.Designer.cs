using System;
using System.IO;
using System.Windows.Forms;

namespace HostsTool
{
    partial class MainForm
    {
        private System.Windows.Forms.Button btnOpenHosts;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ToolStripMenuItem btnSetDisable;
        private System.Windows.Forms.ToolStripMenuItem btnSetEnable;
        private System.Windows.Forms.ToolStripMenuItem btnSetGroupNum;
        private System.Windows.Forms.Button btnSetNotTogether;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private string hostsFilePath = Path.Combine(Environment.SystemDirectory, @"drivers\etc\hosts");
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.TreeView treeView;

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSetDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSetEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSetGroupNum = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnOpenHosts = new System.Windows.Forms.Button();
            this.btnSetNotTogether = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.contextMenuStrip.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.AccessibleDescription = global::HostsTool.Properties.Resources.Icon;
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.ContextMenuStrip = this.contextMenuStrip;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ItemHeight = 17;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(542, 666);
            this.treeView.TabIndex = 0;
            this.treeView.DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSetDisable,
            this.btnSetEnable,
            this.btnSetGroupNum});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(137, 70);
            // 
            // btnSetDisable
            // 
            this.btnSetDisable.Name = "btnSetDisable";
            this.btnSetDisable.Size = new System.Drawing.Size(136, 22);
            this.btnSetDisable.Text = "失效";
            this.btnSetDisable.Click += new System.EventHandler(this.btnSetDisable_Click);
            // 
            // btnSetEnable
            // 
            this.btnSetEnable.Name = "btnSetEnable";
            this.btnSetEnable.Size = new System.Drawing.Size(136, 22);
            this.btnSetEnable.Text = "可用";
            this.btnSetEnable.Click += new System.EventHandler(this.btnSetEnable_Click);
            // 
            // btnSetGroupNum
            // 
            this.btnSetGroupNum.Name = "btnSetGroupNum";
            this.btnSetGroupNum.Size = new System.Drawing.Size(136, 22);
            this.btnSetGroupNum.Text = "设置组编号";
            this.btnSetGroupNum.Click += new System.EventHandler(this.btnSetGroupNum_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(139, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(118, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新显示";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnOpenHosts);
            this.panelBottom.Controls.Add(this.btnSetNotTogether);
            this.panelBottom.Controls.Add(this.btnRefresh);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 630);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(542, 36);
            this.panelBottom.TabIndex = 2;
            // 
            // btnOpenHosts
            // 
            this.btnOpenHosts.Location = new System.Drawing.Point(412, 6);
            this.btnOpenHosts.Name = "btnOpenHosts";
            this.btnOpenHosts.Size = new System.Drawing.Size(118, 23);
            this.btnOpenHosts.TabIndex = 2;
            this.btnOpenHosts.Text = "打开Hosts文件";
            this.btnOpenHosts.UseVisualStyleBackColor = true;
            this.btnOpenHosts.Click += new System.EventHandler(this.btnOpenHosts_Click);
            // 
            // btnSetNotTogether
            // 
            this.btnSetNotTogether.Location = new System.Drawing.Point(273, 6);
            this.btnSetNotTogether.Name = "btnSetNotTogether";
            this.btnSetNotTogether.Size = new System.Drawing.Size(118, 23);
            this.btnSetNotTogether.TabIndex = 2;
            this.btnSetNotTogether.Text = "设置互斥组";
            this.btnSetNotTogether.UseVisualStyleBackColor = true;
            this.btnSetNotTogether.Click += new System.EventHandler(this.btnSetNotTogether_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.treeView);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(542, 666);
            this.panelButtons.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 666);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelButtons);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HostsTool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
    }
}

