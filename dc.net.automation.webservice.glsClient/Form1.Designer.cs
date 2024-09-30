namespace dc.net.automation.webservice.glsClient
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.TestAddParcel = new System.Windows.Forms.Button();
            this.TestListSped = new System.Windows.Forms.Button();
            this.TestGetPdf = new System.Windows.Forms.Button();
            this.TestGetZpl = new System.Windows.Forms.Button();
            this.TestCloseWorkDay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestAddParcel
            // 
            this.TestAddParcel.Location = new System.Drawing.Point(60, 39);
            this.TestAddParcel.Name = "TestAddParcel";
            this.TestAddParcel.Size = new System.Drawing.Size(143, 23);
            this.TestAddParcel.TabIndex = 0;
            this.TestAddParcel.Text = "Test AddParcel";
            this.TestAddParcel.UseVisualStyleBackColor = true;
            this.TestAddParcel.Click += new System.EventHandler(this.TestAddParcel_Click);
            // 
            // TestListSped
            // 
            this.TestListSped.Location = new System.Drawing.Point(60, 79);
            this.TestListSped.Name = "TestListSped";
            this.TestListSped.Size = new System.Drawing.Size(143, 23);
            this.TestListSped.TabIndex = 1;
            this.TestListSped.Text = "Test List Sped";
            this.TestListSped.UseVisualStyleBackColor = true;
            this.TestListSped.Click += new System.EventHandler(this.TestListSped_Click);
            // 
            // TestGetPdf
            // 
            this.TestGetPdf.Location = new System.Drawing.Point(60, 122);
            this.TestGetPdf.Name = "TestGetPdf";
            this.TestGetPdf.Size = new System.Drawing.Size(143, 23);
            this.TestGetPdf.TabIndex = 2;
            this.TestGetPdf.Text = "Test Get PDF";
            this.TestGetPdf.UseVisualStyleBackColor = true;
            this.TestGetPdf.Click += new System.EventHandler(this.TestGetPdf_Click);
            // 
            // TestGetZpl
            // 
            this.TestGetZpl.Location = new System.Drawing.Point(60, 165);
            this.TestGetZpl.Name = "TestGetZpl";
            this.TestGetZpl.Size = new System.Drawing.Size(143, 23);
            this.TestGetZpl.TabIndex = 3;
            this.TestGetZpl.Text = "Test Get ZPL";
            this.TestGetZpl.UseVisualStyleBackColor = true;
            this.TestGetZpl.Click += new System.EventHandler(this.TestGetZpl_Click);
            // 
            // TestCloseWorkDay
            // 
            this.TestCloseWorkDay.Location = new System.Drawing.Point(60, 213);
            this.TestCloseWorkDay.Name = "TestCloseWorkDay";
            this.TestCloseWorkDay.Size = new System.Drawing.Size(143, 23);
            this.TestCloseWorkDay.TabIndex = 4;
            this.TestCloseWorkDay.Text = "Test Close Work Day";
            this.TestCloseWorkDay.UseVisualStyleBackColor = true;
            this.TestCloseWorkDay.Click += new System.EventHandler(this.TestCloseWorkDay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TestCloseWorkDay);
            this.Controls.Add(this.TestGetZpl);
            this.Controls.Add(this.TestGetPdf);
            this.Controls.Add(this.TestListSped);
            this.Controls.Add(this.TestAddParcel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestAddParcel;
        private System.Windows.Forms.Button TestListSped;
        private System.Windows.Forms.Button TestGetPdf;
        private System.Windows.Forms.Button TestGetZpl;
        private System.Windows.Forms.Button TestCloseWorkDay;
    }
}

