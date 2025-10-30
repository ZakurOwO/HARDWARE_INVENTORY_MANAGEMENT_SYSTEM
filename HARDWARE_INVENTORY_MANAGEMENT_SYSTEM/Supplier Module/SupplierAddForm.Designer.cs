namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Supplier_Module
{
    partial class SupplierAddForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CustomerRemarkTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.ZipCodeTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.ProvinceComboBox = new Krypton.Toolkit.KryptonComboBox();
            this.CityComboBox = new Krypton.Toolkit.KryptonComboBox();
            this.AddressTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.PhoneNumberTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.EmailAddressTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.ContactPersonTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.CompanyNameTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProvinceComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CityComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // CustomerRemarkTextBox
            // 
            this.CustomerRemarkTextBox.Location = new System.Drawing.Point(21, 378);
            this.CustomerRemarkTextBox.Name = "CustomerRemarkTextBox";
            this.CustomerRemarkTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.CustomerRemarkTextBox.Size = new System.Drawing.Size(497, 124);
            this.CustomerRemarkTextBox.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.CustomerRemarkTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.CustomerRemarkTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.CustomerRemarkTextBox.StateCommon.Border.Rounding = 7F;
            this.CustomerRemarkTextBox.StateCommon.Content.Color1 = System.Drawing.Color.DimGray;
            this.CustomerRemarkTextBox.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 10, -1, -1);
            this.CustomerRemarkTextBox.TabIndex = 59;
            this.CustomerRemarkTextBox.Text = "";
            this.CustomerRemarkTextBox.TextChanged += new System.EventHandler(this.CustomerRemarkTextBox_TextChanged);
            // 
            // ZipCodeTextBox
            // 
            this.ZipCodeTextBox.Location = new System.Drawing.Point(380, 293);
            this.ZipCodeTextBox.Name = "ZipCodeTextBox";
            this.ZipCodeTextBox.Size = new System.Drawing.Size(138, 27);
            this.ZipCodeTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.ZipCodeTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.ZipCodeTextBox.StateCommon.Border.Rounding = 7F;
            this.ZipCodeTextBox.TabIndex = 58;
            this.ZipCodeTextBox.Text = "Enter Zip Code";
            this.ZipCodeTextBox.TextChanged += new System.EventHandler(this.ZipCodeTextBox_TextChanged);
            // 
            // ProvinceComboBox
            // 
            this.ProvinceComboBox.DropDownWidth = 126;
            this.ProvinceComboBox.Items.AddRange(new object[] {
            "Abra",
            "Agusan del Norte",
            "Agusan del Sur",
            "Aklan",
            "Albay",
            "Antique",
            "Apayao",
            "Aurora",
            "Basilan",
            "Bataan",
            "Batanes",
            "Batangas",
            "Benguet",
            "Biliran",
            "Bohol",
            "Bukidnon",
            "Bulacan",
            "Cagayan",
            "Camiguines",
            "Camiguin",
            "Camarines Norte",
            "Camarines Sur",
            "Capiz",
            "Catanduanes",
            "Cavite",
            "Cebu",
            "Cotabato",
            "Davao del Norte",
            "Davao del Sur",
            "Davao Occidental",
            "Davao Oriental",
            "Dinagat Islands",
            "Eastern Samar",
            "Guimaras",
            "Ifugao",
            "Ilocos Norte",
            "Ilocos Sur",
            "Iloilo",
            "Isabela",
            "Kalinga",
            "La Union",
            "Laguna",
            "Lanao del Norte",
            "Lanao del Sur",
            "Leyte",
            "Maguindanao del Norte",
            "Maguindanao del Sur",
            "Marinduque",
            "Masbate",
            "Metro Manila *(Note: not a province)*",
            "Misamis Occidental",
            "Misamis Oriental",
            "Mountain Province",
            "Negros Occidental",
            "Negros Oriental",
            "Northern Mindanao *(Note: region)*",
            "Northern Samar",
            "Nueva Ecija",
            "Nueva Vizcaya",
            "Occidental Mindoro",
            "Oriental Mindoro",
            "Palawan",
            "Pampanga",
            "Pangasinan",
            "Quezon",
            "Quirino",
            "Rizal",
            "Romblon",
            "Sarangani",
            "Siquijor",
            "Sorsogon",
            "South Cotabato",
            "Southern Leyte",
            "Sultan Kudarat",
            "Sulu",
            "Surigao del Norte",
            "Surigao del Sur",
            "Tarlac",
            "Tawi-Tawi",
            "Zambales",
            "Zamboanga del Norte",
            "Zamboanga del Sur",
            "Zamboanga Sibugay"});
            this.ProvinceComboBox.Location = new System.Drawing.Point(204, 298);
            this.ProvinceComboBox.Name = "ProvinceComboBox";
            this.ProvinceComboBox.Size = new System.Drawing.Size(130, 26);
            this.ProvinceComboBox.StateCommon.ComboBox.Border.Rounding = 7F;
            this.ProvinceComboBox.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ProvinceComboBox.TabIndex = 57;
            this.ProvinceComboBox.Text = "Enter Province";
            this.ProvinceComboBox.SelectedIndexChanged += new System.EventHandler(this.ProvinceComboBox_SelectedIndexChanged);
            // 
            // CityComboBox
            // 
            this.CityComboBox.DropDownWidth = 126;
            this.CityComboBox.Items.AddRange(new object[] {
            "Manila",
            "Quezon City",
            "Caloocan",
            "Las Piñas",
            "Makati",
            "Malabon",
            "Mandaluyong",
            "Marikina",
            "Muntinlupa",
            "Navotas",
            "Parañaque",
            "Pasay",
            "Pasig",
            "San Juan",
            "Taguig",
            "Valenzuela",
            "Baguio",
            "Tabuk",
            "Vigan",
            "Laoag",
            "Tuguegarao",
            "Cauayan",
            "Ilagan",
            "Santiago",
            "Bayombong",
            "Balanga",
            "Malolos",
            "Meycauayan",
            "San Jose del Monte",
            "Cabanatuan",
            "Gapan",
            "Palayan",
            "Angeles",
            "Mabalacat",
            "San Fernando",
            "Tarlac City",
            "Olongapo",
            "Dagupan",
            "San Carlos (Pangasinan)",
            "Urdaneta",
            "Alaminos",
            "Batangas City",
            "Lipa",
            "Tanauan",
            "Calamba",
            "San Pablo",
            "Santa Rosa",
            "Biñan",
            "Cabuyao",
            "Antipolo",
            "Lucena",
            "Tayabas",
            "Naga",
            "Iriga",
            "Legazpi",
            "Ligao",
            "Tabaco",
            "Sorsogon City",
            "Masbate City",
            "Puerto Princesa",
            "Iloilo City",
            "Passi",
            "Roxas City",
            "Kalibo",
            "San Jose de Buenavista",
            "Bacolod",
            "Bago",
            "Cadiz",
            "Escalante",
            "Himamaylan",
            "Kabankalan",
            "La Carlota",
            "Sagay",
            "San Carlos (Negros Occidental)",
            "Silay",
            "Sipalay",
            "Talisay (Negros Occidental)",
            "Victorias",
            "Dumaguete",
            "Bais",
            "Bayawan",
            "Canlaon",
            "Guihulngan",
            "Tanjay",
            "Tagbilaran",
            "Cebu City",
            "Carcar",
            "Danao",
            "Lapu-Lapu",
            "Mandaue",
            "Naga (Cebu)",
            "Talisay (Cebu)",
            "Toledo",
            "Bogo",
            "Ormoc",
            "Tacloban",
            "Baybay",
            "Calbayog",
            "Catbalogan",
            "Borongan",
            "Maasin",
            "Iloilo",
            "Roxas",
            "Kalibo",
            "Catarman",
            "Naval",
            "Zamboanga City",
            "Pagadian",
            "Dipolog",
            "Dapitan",
            "Isabela City",
            "Cagayan de Oro",
            "El Salvador",
            "Gingoog",
            "Malaybalay",
            "Valencia",
            "Iligan",
            "Ozamiz",
            "Tangub",
            "Oroquieta",
            "Marawi",
            "Butuan",
            "Cabadbaran",
            "Bayugan",
            "Surigao",
            "Bislig",
            "Tandag",
            "Tagum",
            "Panabo",
            "Samal",
            "Mati",
            "Digos",
            "Davao City",
            "Davao del Sur",
            "Koronadal",
            "Tacurong",
            "Kidapawan",
            "Cotabato City",
            "General Santos",
            "Marawi",
            "Isulan",
            "Buluan",
            "Lamitan",
            "Marawi",
            "Basilan City",
            "Zamboanga Sibugay"});
            this.CityComboBox.Location = new System.Drawing.Point(22, 298);
            this.CityComboBox.Name = "CityComboBox";
            this.CityComboBox.Size = new System.Drawing.Size(130, 26);
            this.CityComboBox.StateCommon.ComboBox.Border.Rounding = 7F;
            this.CityComboBox.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.CityComboBox.TabIndex = 56;
            this.CityComboBox.Text = "Enter City";
            this.CityComboBox.SelectedIndexChanged += new System.EventHandler(this.CityComboBox_SelectedIndexChanged);
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Location = new System.Drawing.Point(21, 223);
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.AddressTextBox.Size = new System.Drawing.Size(497, 41);
            this.AddressTextBox.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.AddressTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.AddressTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.AddressTextBox.StateCommon.Border.Rounding = 7F;
            this.AddressTextBox.StateCommon.Content.Color1 = System.Drawing.Color.DimGray;
            this.AddressTextBox.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 10, -1, -1);
            this.AddressTextBox.TabIndex = 55;
            this.AddressTextBox.Text = "Enter Full Address";
            this.AddressTextBox.TextChanged += new System.EventHandler(this.AddressTextBox_TextChanged);
            // 
            // PhoneNumberTextBox
            // 
            this.PhoneNumberTextBox.Location = new System.Drawing.Point(293, 153);
            this.PhoneNumberTextBox.Name = "PhoneNumberTextBox";
            this.PhoneNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.PhoneNumberTextBox.Size = new System.Drawing.Size(193, 41);
            this.PhoneNumberTextBox.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.PhoneNumberTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.PhoneNumberTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.PhoneNumberTextBox.StateCommon.Border.Rounding = 7F;
            this.PhoneNumberTextBox.StateCommon.Content.Color1 = System.Drawing.Color.DimGray;
            this.PhoneNumberTextBox.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 10, -1, -1);
            this.PhoneNumberTextBox.TabIndex = 54;
            this.PhoneNumberTextBox.Text = "Enter Phone Number";
            this.PhoneNumberTextBox.TextChanged += new System.EventHandler(this.PhoneNumberTextBox_TextChanged);
            // 
            // EmailAddressTextBox
            // 
            this.EmailAddressTextBox.Location = new System.Drawing.Point(21, 153);
            this.EmailAddressTextBox.Name = "EmailAddressTextBox";
            this.EmailAddressTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.EmailAddressTextBox.Size = new System.Drawing.Size(193, 41);
            this.EmailAddressTextBox.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.EmailAddressTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.EmailAddressTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.EmailAddressTextBox.StateCommon.Border.Rounding = 7F;
            this.EmailAddressTextBox.StateCommon.Content.Color1 = System.Drawing.Color.DimGray;
            this.EmailAddressTextBox.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 10, -1, -1);
            this.EmailAddressTextBox.TabIndex = 53;
            this.EmailAddressTextBox.Text = "Enter Email Address";
            this.EmailAddressTextBox.TextChanged += new System.EventHandler(this.EmailAddressTextBox_TextChanged);
            // 
            // ContactPersonTextBox
            // 
            this.ContactPersonTextBox.Location = new System.Drawing.Point(293, 88);
            this.ContactPersonTextBox.Name = "ContactPersonTextBox";
            this.ContactPersonTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.ContactPersonTextBox.Size = new System.Drawing.Size(225, 41);
            this.ContactPersonTextBox.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.ContactPersonTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.ContactPersonTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.ContactPersonTextBox.StateCommon.Border.Rounding = 7F;
            this.ContactPersonTextBox.StateCommon.Content.Color1 = System.Drawing.Color.DimGray;
            this.ContactPersonTextBox.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 10, -1, -1);
            this.ContactPersonTextBox.TabIndex = 52;
            this.ContactPersonTextBox.Text = "Enter Contact Person";
            this.ContactPersonTextBox.TextChanged += new System.EventHandler(this.ContactPersonTextBox_TextChanged);
            // 
            // CompanyNameTextBox
            // 
            this.CompanyNameTextBox.Location = new System.Drawing.Point(21, 89);
            this.CompanyNameTextBox.Name = "CompanyNameTextBox";
            this.CompanyNameTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.CompanyNameTextBox.Size = new System.Drawing.Size(193, 41);
            this.CompanyNameTextBox.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.CompanyNameTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.CompanyNameTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.CompanyNameTextBox.StateCommon.Border.Rounding = 7F;
            this.CompanyNameTextBox.StateCommon.Content.Color1 = System.Drawing.Color.DimGray;
            this.CompanyNameTextBox.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 10, -1, -1);
            this.CompanyNameTextBox.TabIndex = 51;
            this.CompanyNameTextBox.Text = "Enter Company Name\n";
            this.CompanyNameTextBox.TextChanged += new System.EventHandler(this.CompanyNameTextBox_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.White;
            this.label20.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label20.ForeColor = System.Drawing.Color.DimGray;
            this.label20.Location = new System.Drawing.Point(18, 358);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 17);
            this.label20.TabIndex = 50;
            this.label20.Text = "Remarks";
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(429, 273);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(13, 17);
            this.label18.TabIndex = 49;
            this.label18.Text = "*";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.White;
            this.label19.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.DimGray;
            this.label19.Location = new System.Drawing.Point(377, 273);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 17);
            this.label19.TabIndex = 48;
            this.label19.Text = "Zip Code";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(254, 273);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 17);
            this.label16.TabIndex = 47;
            this.label16.Text = "*";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.DimGray;
            this.label15.Location = new System.Drawing.Point(201, 273);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 17);
            this.label15.TabIndex = 45;
            this.label15.Text = "Province";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(44, 273);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 17);
            this.label12.TabIndex = 44;
            this.label12.Text = "*";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.DimGray;
            this.label13.Location = new System.Drawing.Point(19, 273);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 17);
            this.label13.TabIndex = 43;
            this.label13.Text = "City";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(67, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 17);
            this.label10.TabIndex = 42;
            this.label10.Text = "*";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.DimGray;
            this.label11.Location = new System.Drawing.Point(19, 203);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 17);
            this.label11.TabIndex = 41;
            this.label11.Text = "Address";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(328, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 17);
            this.label8.TabIndex = 40;
            this.label8.Text = "*";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(290, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 17);
            this.label9.TabIndex = 39;
            this.label9.Text = "Phone";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(55, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 17);
            this.label6.TabIndex = 38;
            this.label6.Text = "*";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(19, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 17);
            this.label7.TabIndex = 37;
            this.label7.Text = "Email";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(377, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 17);
            this.label4.TabIndex = 36;
            this.label4.Text = "*";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(290, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 35;
            this.label5.Text = "Contact Person";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(112, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 17);
            this.label3.TabIndex = 34;
            this.label3.Text = "*";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(19, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 33;
            this.label2.Text = "Company Name";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 25);
            this.label1.TabIndex = 32;
            this.label1.Text = "Add New Customer";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Add_Stock_Popup;
            this.pictureBox1.Location = new System.Drawing.Point(0, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(578, 550);
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(575, 550);
            this.pictureBox2.TabIndex = 46;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // SupplierAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CustomerRemarkTextBox);
            this.Controls.Add(this.ZipCodeTextBox);
            this.Controls.Add(this.ProvinceComboBox);
            this.Controls.Add(this.CityComboBox);
            this.Controls.Add(this.AddressTextBox);
            this.Controls.Add(this.PhoneNumberTextBox);
            this.Controls.Add(this.EmailAddressTextBox);
            this.Controls.Add(this.ContactPersonTextBox);
            this.Controls.Add(this.CompanyNameTextBox);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Name = "SupplierAddForm";
            this.Size = new System.Drawing.Size(578, 550);
            ((System.ComponentModel.ISupportInitialize)(this.ProvinceComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CityComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonRichTextBox CustomerRemarkTextBox;
        private Krypton.Toolkit.KryptonTextBox ZipCodeTextBox;
        private Krypton.Toolkit.KryptonComboBox ProvinceComboBox;
        private Krypton.Toolkit.KryptonComboBox CityComboBox;
        private Krypton.Toolkit.KryptonRichTextBox AddressTextBox;
        private Krypton.Toolkit.KryptonRichTextBox PhoneNumberTextBox;
        private Krypton.Toolkit.KryptonRichTextBox EmailAddressTextBox;
        private Krypton.Toolkit.KryptonRichTextBox ContactPersonTextBox;
        private Krypton.Toolkit.KryptonRichTextBox CompanyNameTextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
