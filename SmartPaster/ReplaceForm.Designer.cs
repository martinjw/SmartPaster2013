namespace SmartPaster
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
            this.findTextBox.Location = new System.Drawing.Point(82, 11);
            this.findTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(345, 20);
            this.findTextBox.TabIndex = 0;
            // 
            // replaceWithTextBox
            // 
            this.replaceWithTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceWithTextBox.Location = new System.Drawing.Point(82, 33);
            this.replaceWithTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.replaceWithTextBox.Name = "replaceWithTextBox";
            this.replaceWithTextBox.Size = new System.Drawing.Size(345, 20);
            this.replaceWithTextBox.TabIndex = 1;
            // 
            // FindLabel
            // 
            this.FindLabel.AutoSize = true;
            this.FindLabel.Location = new System.Drawing.Point(9, 13);
            this.FindLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FindLabel.Name = "FindLabel";
            this.FindLabel.Size = new System.Drawing.Size(30, 13);
            this.FindLabel.TabIndex = 2;
            this.FindLabel.Text = "Find:";
            // 
            // ReplaceWithLabel
            // 
            this.ReplaceWithLabel.AutoSize = true;
            this.ReplaceWithLabel.Location = new System.Drawing.Point(9, 37);
            this.ReplaceWithLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ReplaceWithLabel.Name = "ReplaceWithLabel";
            this.ReplaceWithLabel.Size = new System.Drawing.Size(72, 13);
            this.ReplaceWithLabel.TabIndex = 3;
            this.ReplaceWithLabel.Text = "Replace with:";
            // 
            // pasteButton
            // 
            this.pasteButton.Location = new System.Drawing.Point(82, 57);
            this.pasteButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(345, 19);
            this.pasteButton.TabIndex = 4;
            this.pasteButton.Text = "Paste";
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // ReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 82);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.ReplaceWithLabel);
            this.Controls.Add(this.FindLabel);
            this.Controls.Add(this.replaceWithTextBox);
            this.Controls.Add(this.findTextBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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