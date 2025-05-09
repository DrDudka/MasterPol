namespace УП_PR1
{
    partial class MenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.buttonPartnersView = new System.Windows.Forms.Button();
            this.buttonAddPartner = new System.Windows.Forms.Button();
            this.buttonEditPartner = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPartnersView
            // 
            this.buttonPartnersView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonPartnersView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(186)))), ((int)(((byte)(128)))));
            this.buttonPartnersView.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPartnersView.Location = new System.Drawing.Point(201, 45);
            this.buttonPartnersView.Name = "buttonPartnersView";
            this.buttonPartnersView.Size = new System.Drawing.Size(187, 53);
            this.buttonPartnersView.TabIndex = 0;
            this.buttonPartnersView.Text = "Список партнеров";
            this.buttonPartnersView.UseVisualStyleBackColor = false;
            this.buttonPartnersView.Click += new System.EventHandler(this.buttonPartnersView_Click);
            // 
            // buttonAddPartner
            // 
            this.buttonAddPartner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAddPartner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(186)))), ((int)(((byte)(128)))));
            this.buttonAddPartner.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddPartner.Location = new System.Drawing.Point(201, 104);
            this.buttonAddPartner.Name = "buttonAddPartner";
            this.buttonAddPartner.Size = new System.Drawing.Size(187, 53);
            this.buttonAddPartner.TabIndex = 1;
            this.buttonAddPartner.Text = "Добавить";
            this.buttonAddPartner.UseVisualStyleBackColor = false;
            this.buttonAddPartner.Click += new System.EventHandler(this.buttonAddPartner_Click);
            // 
            // buttonEditPartner
            // 
            this.buttonEditPartner.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonEditPartner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(186)))), ((int)(((byte)(128)))));
            this.buttonEditPartner.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEditPartner.Location = new System.Drawing.Point(201, 163);
            this.buttonEditPartner.Name = "buttonEditPartner";
            this.buttonEditPartner.Size = new System.Drawing.Size(187, 53);
            this.buttonEditPartner.TabIndex = 2;
            this.buttonEditPartner.Text = "Редактировать";
            this.buttonEditPartner.UseVisualStyleBackColor = false;
            this.buttonEditPartner.Click += new System.EventHandler(this.buttonEditPartner_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::УП_PR1.Properties.Resources.Мастер_пол;
            this.pictureBox1.Location = new System.Drawing.Point(523, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 51);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonEditPartner);
            this.Controls.Add(this.buttonAddPartner);
            this.Controls.Add(this.buttonPartnersView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MenuForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPartnersView;
        private System.Windows.Forms.Button buttonAddPartner;
        private System.Windows.Forms.Button buttonEditPartner;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}