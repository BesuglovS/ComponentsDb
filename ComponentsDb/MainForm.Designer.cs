namespace ComponentsDb
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainView = new System.Windows.Forms.TreeView();
            this.MainContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.новыйКомпонентToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewTopLevelComponentButton = new System.Windows.Forms.ToolStripMenuItem();
            this.NewEmbeddedComponentButton = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameComponent = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveComponent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CreateOpenXmlReport = new System.Windows.Forms.ToolStripMenuItem();
            this.imageTreeIcons = new System.Windows.Forms.ImageList(this.components);
            this.NewTopLevelComponent = new System.Windows.Forms.ToolStripMenuItem();
            this.newEmbeddedComponent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainView
            // 
            this.MainView.ContextMenuStrip = this.MainContextMenu;
            this.MainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainView.ImageIndex = 0;
            this.MainView.ImageList = this.imageTreeIcons;
            this.MainView.Location = new System.Drawing.Point(0, 0);
            this.MainView.Name = "MainView";
            this.MainView.SelectedImageIndex = 0;
            this.MainView.Size = new System.Drawing.Size(800, 426);
            this.MainView.TabIndex = 0;
            this.MainView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainView_KeyDown);
            // 
            // MainContextMenu
            // 
            this.MainContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйКомпонентToolStripMenuItem,
            this.RenameComponent,
            this.RemoveComponent,
            this.toolStripMenuItem1,
            this.CreateOpenXmlReport});
            this.MainContextMenu.Name = "MainContextMenu";
            this.MainContextMenu.Size = new System.Drawing.Size(258, 120);
            // 
            // новыйКомпонентToolStripMenuItem
            // 
            this.новыйКомпонентToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTopLevelComponentButton,
            this.NewEmbeddedComponentButton});
            this.новыйКомпонентToolStripMenuItem.Name = "новыйКомпонентToolStripMenuItem";
            this.новыйКомпонентToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.новыйКомпонентToolStripMenuItem.Text = "Новый компонент";
            // 
            // NewTopLevelComponentButton
            // 
            this.NewTopLevelComponentButton.Name = "NewTopLevelComponentButton";
            this.NewTopLevelComponentButton.Size = new System.Drawing.Size(309, 22);
            this.NewTopLevelComponentButton.Text = "Компонент верхнего уровня (Ctrl+Shift+N)";
            this.NewTopLevelComponentButton.Click += new System.EventHandler(this.NewTopLevelComponentButton_Click);
            // 
            // NewEmbeddedComponentButton
            // 
            this.NewEmbeddedComponentButton.Name = "NewEmbeddedComponentButton";
            this.NewEmbeddedComponentButton.Size = new System.Drawing.Size(309, 22);
            this.NewEmbeddedComponentButton.Text = "Вложенный компонент (Ctrl+N)";
            this.NewEmbeddedComponentButton.Click += new System.EventHandler(this.NewEmbeddedComponentButton_Click);
            // 
            // RenameComponent
            // 
            this.RenameComponent.Name = "RenameComponent";
            this.RenameComponent.Size = new System.Drawing.Size(257, 22);
            this.RenameComponent.Text = "Переименовать (F2)";
            this.RenameComponent.Click += new System.EventHandler(this.RenameComponent_Click);
            // 
            // RemoveComponent
            // 
            this.RemoveComponent.Name = "RemoveComponent";
            this.RemoveComponent.Size = new System.Drawing.Size(257, 22);
            this.RemoveComponent.Text = "Удалить (Del)";
            this.RemoveComponent.Click += new System.EventHandler(this.RemoveComponent_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(254, 6);
            // 
            // CreateOpenXmlReport
            // 
            this.CreateOpenXmlReport.Name = "CreateOpenXmlReport";
            this.CreateOpenXmlReport.Size = new System.Drawing.Size(257, 22);
            this.CreateOpenXmlReport.Text = "Отчёт о сводном составе (Ctrl+R)";
            this.CreateOpenXmlReport.Click += new System.EventHandler(this.CreateOpenXmlReport_Click);
            // 
            // imageTreeIcons
            // 
            this.imageTreeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageTreeIcons.ImageStream")));
            this.imageTreeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageTreeIcons.Images.SetKeyName(0, "cog.png");
            // 
            // NewTopLevelComponent
            // 
            this.NewTopLevelComponent.Name = "NewTopLevelComponent";
            this.NewTopLevelComponent.Size = new System.Drawing.Size(230, 22);
            this.NewTopLevelComponent.Text = "Компонент верхнего уровня";
            // 
            // newEmbeddedComponent
            // 
            this.newEmbeddedComponent.Name = "newEmbeddedComponent";
            this.newEmbeddedComponent.Size = new System.Drawing.Size(230, 22);
            this.newEmbeddedComponent.Text = "Вложенный компонент";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem4.Text = "456";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem5.Text = "789";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 426);
            this.Controls.Add(this.MainView);
            this.Name = "MainForm";
            this.Text = "База компонентов";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView MainView;
        private System.Windows.Forms.ContextMenuStrip MainContextMenu;
        private System.Windows.Forms.ToolStripMenuItem новыйКомпонентToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewTopLevelComponent;
        private System.Windows.Forms.ToolStripMenuItem newEmbeddedComponent;
        private System.Windows.Forms.ToolStripMenuItem RenameComponent;
        private System.Windows.Forms.ToolStripMenuItem RemoveComponent;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CreateOpenXmlReport;
        private System.Windows.Forms.ImageList imageTreeIcons;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem NewTopLevelComponentButton;
        private System.Windows.Forms.ToolStripMenuItem NewEmbeddedComponentButton;
    }
}

