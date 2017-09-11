namespace SmartPaster2013
{
    partial class ReplaceForm
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
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.replaceWithTextBox = new System.Windows.Forms.TextBox();
            this.FindLabel = new System.Windows.Forms.Label();
            this.ReplaceWithLabel = new System.Windows.Forms.Label();
            this.pasteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // findTextBox
            // 
            this.findTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findTextBox.Location = new System.Drawing.Point(110, 13);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(459, 22);
            this.findTextBox.TabIndex = 0;
            // 
            // replaceWithTextBox
            // 
            this.replaceWithTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceWithTextBox.Location = new System.Drawing.Point(110, 41);
            this.replaceWithTextBox.Name = "replaceWithTextBox";
            this.replaceWithTextBox.Size = new System.Drawing.Size(459, 22);
            this.replaceWithTextBox.TabIndex = 1;
            // 
            // FindLabel
            // 
            this.FindLabel.AutoSize = true;
            this.FindLabel.Location = new System.Drawing.Point(12, 16);
            this.FindLabel.Name = "FindLabel";
            this.FindLabel.Size = new System.Drawing.Size(39, 17);
            this.FindLabel.TabIndex = 2;
            this.FindLabel.Text = "Find:";
            // 
            // ReplaceWithLabel
            // 
            this.ReplaceWithLabel.AutoSize = true;
            this.ReplaceWithLabel.Location = new System.Drawing.Point(12, 46);
            this.ReplaceWithLabel.Name = "ReplaceWithLabel";
            this.ReplaceWithLabel.Size = new System.Drawing.Size(92, 17);
            this.ReplaceWithLabel.TabIndex = 3;
            this.ReplaceWithLabel.Text = "Replace with:";
            // 
            // pasteButton
            // 
            this.pasteButton.Location = new System.Drawing.Point(485, 70);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(84, 23);
            this.pasteButton.TabIndex = 4;
            this.pasteButton.Text = "Paste";
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // ReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 101);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.ReplaceWithLabel);
            this.Controls.Add(this.FindLabel);
            this.Controls.Add(this.replaceWithTextBox);
            this.Controls.Add(this.findTextBox);
            this.Name = "ReplaceForm";
            this.Text = "Find/Replace in paste text";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.TextBox replaceWithTextBox;
        private System.Windows.Forms.Label FindLabel;
        private System.Windows.Forms.Label ReplaceWithLabel;
        private System.Windows.Forms.Button pasteButton;
    }
}