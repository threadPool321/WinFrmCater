﻿namespace WinUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMember = new System.Windows.Forms.ToolStripMenuItem();
            this.menudish = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManager,
            this.menuMember,
            this.menudish,
            this.menuTable,
            this.menuOrder,
            this.menuQuit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1059, 92);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuManager
            // 
            this.menuManager.Image = global::WinUI.Properties.Resources.menuManager;
            this.menuManager.Name = "menuManager";
            this.menuManager.Size = new System.Drawing.Size(76, 88);
            this.menuManager.Text = "管理员";
            this.menuManager.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.menuManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuManager.Click += new System.EventHandler(this.menuManager_Click);
            // 
            // menuMember
            // 
            this.menuMember.Image = global::WinUI.Properties.Resources.menuMember;
            this.menuMember.Name = "menuMember";
            this.menuMember.Size = new System.Drawing.Size(76, 88);
            this.menuMember.Text = "会员";
            this.menuMember.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuMember.Click += new System.EventHandler(this.menuMember_Click);
            // 
            // menudish
            // 
            this.menudish.Image = global::WinUI.Properties.Resources.menuDish;
            this.menudish.Name = "menudish";
            this.menudish.Size = new System.Drawing.Size(76, 88);
            this.menudish.Text = "dis";
            this.menudish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menudish.Click += new System.EventHandler(this.menudish_Click);
            // 
            // menuTable
            // 
            this.menuTable.Image = global::WinUI.Properties.Resources.menuTable;
            this.menuTable.Name = "menuTable";
            this.menuTable.Size = new System.Drawing.Size(76, 88);
            this.menuTable.Text = "餐桌";
            this.menuTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuTable.Click += new System.EventHandler(this.menuTable_Click);
            // 
            // menuOrder
            // 
            this.menuOrder.Image = global::WinUI.Properties.Resources.menuOrder;
            this.menuOrder.Name = "menuOrder";
            this.menuOrder.Size = new System.Drawing.Size(76, 88);
            this.menuOrder.Text = "订单";
            this.menuOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menuQuit
            // 
            this.menuQuit.Image = global::WinUI.Properties.Resources.menuQuit;
            this.menuQuit.Name = "menuQuit";
            this.menuQuit.Size = new System.Drawing.Size(76, 88);
            this.menuQuit.Text = "退出";
            this.menuQuit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuQuit.Click += new System.EventHandler(this.menuQuit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 555);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "菜单";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuManager;
        private System.Windows.Forms.ToolStripMenuItem menuMember;
        private System.Windows.Forms.ToolStripMenuItem menudish;
        private System.Windows.Forms.ToolStripMenuItem menuTable;
        private System.Windows.Forms.ToolStripMenuItem menuOrder;
        private System.Windows.Forms.ToolStripMenuItem menuQuit;
    }
}