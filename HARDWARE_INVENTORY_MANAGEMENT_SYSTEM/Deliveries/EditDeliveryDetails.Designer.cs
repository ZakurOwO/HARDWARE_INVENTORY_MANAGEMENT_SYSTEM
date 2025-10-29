namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    partial class EditDeliveryDetails
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
            this.ZipCodeDeliveryDetailsTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.ProvinceDeliveryDetailsComboBox = new Krypton.Toolkit.KryptonComboBox();
            this.CityDeliveryDetailsComboBox = new Krypton.Toolkit.KryptonComboBox();
            this.TransactionTypeTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.TrackingNumberTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.DeliveryOrderTextBox = new Krypton.Toolkit.KryptonRichTextBox();
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
            this.DeliveryStatusComboBox = new Krypton.Toolkit.KryptonComboBox();
            this.GenerateDeliveryOrder = new Krypton.Toolkit.KryptonButton();
            this.GenerateTrackingNumber = new Krypton.Toolkit.KryptonButton();
            this.EditButtonEditDelivery = new Krypton.Toolkit.KryptonButton();
            this.BackButtonEditDelivery = new Krypton.Toolkit.KryptonButton();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.DateInDeliveryDetails = new Krypton.Toolkit.KryptonDateTimePicker();
            this.VehiclesSelectorComboBox = new Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProvinceDeliveryDetailsComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CityDeliveryDetailsComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeliveryStatusComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VehiclesSelectorComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ZipCodeDeliveryDetailsTextBox
            // 
            this.ZipCodeDeliveryDetailsTextBox.Location = new System.Drawing.Point(22, 410);
            this.ZipCodeDeliveryDetailsTextBox.Name = "ZipCodeDeliveryDetailsTextBox";
            this.ZipCodeDeliveryDetailsTextBox.Size = new System.Drawing.Size(138, 27);
            this.ZipCodeDeliveryDetailsTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.ZipCodeDeliveryDetailsTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.ZipCodeDeliveryDetailsTextBox.StateCommon.Border.Rounding = 7F;
            this.ZipCodeDeliveryDetailsTextBox.TabIndex = 58;
            this.ZipCodeDeliveryDetailsTextBox.Text = "Enter Zip Code";
            // 
            // ProvinceDeliveryDetailsComboBox
            // 
            this.ProvinceDeliveryDetailsComboBox.DropDownWidth = 126;
            this.ProvinceDeliveryDetailsComboBox.Items.AddRange(new object[] {
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
            this.ProvinceDeliveryDetailsComboBox.Location = new System.Drawing.Point(297, 354);
            this.ProvinceDeliveryDetailsComboBox.Name = "ProvinceDeliveryDetailsComboBox";
            this.ProvinceDeliveryDetailsComboBox.Size = new System.Drawing.Size(130, 26);
            this.ProvinceDeliveryDetailsComboBox.StateCommon.ComboBox.Border.Rounding = 7F;
            this.ProvinceDeliveryDetailsComboBox.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.ProvinceDeliveryDetailsComboBox.TabIndex = 57;
            this.ProvinceDeliveryDetailsComboBox.Text = "Enter Province";
            // 
            // CityDeliveryDetailsComboBox
            // 
            this.CityDeliveryDetailsComboBox.DropDownWidth = 126;
            this.CityDeliveryDetailsComboBox.Items.AddRange(new object[] {
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
            this.CityDeliveryDetailsComboBox.Location = new System.Drawing.Point(21, 349);
            this.CityDeliveryDetailsComboBox.Name = "CityDeliveryDetailsComboBox";
            this.CityDeliveryDetailsComboBox.Size = new System.Drawing.Size(130, 26);
            this.CityDeliveryDetailsComboBox.StateCommon.ComboBox.Border.Rounding = 7F;
            this.CityDeliveryDetailsComboBox.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.CityDeliveryDetailsComboBox.TabIndex = 56;
            this.CityDeliveryDetailsComboBox.Text = "Enter City";
            // 
            // TransactionTypeTextBox
            // 
            this.TransactionTypeTextBox.Location = new System.Drawing.Point(25, 204);
            this.TransactionTypeTextBox.Name = "TransactionTypeTextBox";
            this.TransactionTypeTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.TransactionTypeTextBox.Size = new System.Drawing.Size(193, 41);
            this.TransactionTypeTextBox.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.TransactionTypeTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.TransactionTypeTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.TransactionTypeTextBox.StateCommon.Border.Rounding = 7F;
            this.TransactionTypeTextBox.StateCommon.Content.Color1 = System.Drawing.Color.DimGray;
            this.TransactionTypeTextBox.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 10, -1, -1);
            this.TransactionTypeTextBox.TabIndex = 53;
            this.TransactionTypeTextBox.Text = "Enter Transaction Type";
            // 
            // TrackingNumberTextBox
            // 
            this.TrackingNumberTextBox.Location = new System.Drawing.Point(293, 86);
            this.TrackingNumberTextBox.Name = "TrackingNumberTextBox";
            this.TrackingNumberTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.TrackingNumberTextBox.Size = new System.Drawing.Size(225, 41);
            this.TrackingNumberTextBox.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.TrackingNumberTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.TrackingNumberTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.TrackingNumberTextBox.StateCommon.Border.Rounding = 7F;
            this.TrackingNumberTextBox.StateCommon.Content.Color1 = System.Drawing.Color.DimGray;
            this.TrackingNumberTextBox.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 10, -1, -1);
            this.TrackingNumberTextBox.TabIndex = 52;
            this.TrackingNumberTextBox.Text = "Tracking Number";
            // 
            // DeliveryOrderTextBox
            // 
            this.DeliveryOrderTextBox.Location = new System.Drawing.Point(21, 87);
            this.DeliveryOrderTextBox.Name = "DeliveryOrderTextBox";
            this.DeliveryOrderTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.DeliveryOrderTextBox.Size = new System.Drawing.Size(193, 41);
            this.DeliveryOrderTextBox.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.DeliveryOrderTextBox.StateCommon.Border.Color1 = System.Drawing.Color.DimGray;
            this.DeliveryOrderTextBox.StateCommon.Border.Color2 = System.Drawing.Color.DimGray;
            this.DeliveryOrderTextBox.StateCommon.Border.Rounding = 7F;
            this.DeliveryOrderTextBox.StateCommon.Content.Color1 = System.Drawing.Color.DimGray;
            this.DeliveryOrderTextBox.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 10, -1, -1);
            this.DeliveryOrderTextBox.TabIndex = 51;
            this.DeliveryOrderTextBox.Text = "Delivery Order";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(75, 390);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(13, 17);
            this.label18.TabIndex = 49;
            this.label18.Text = "*";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.White;
            this.label19.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.DimGray;
            this.label19.Location = new System.Drawing.Point(23, 390);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 17);
            this.label19.TabIndex = 48;
            this.label19.Text = "Zip Code";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.White;
            this.label16.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(347, 329);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 17);
            this.label16.TabIndex = 47;
            this.label16.Text = "*";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.DimGray;
            this.label15.Location = new System.Drawing.Point(294, 329);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 17);
            this.label15.TabIndex = 45;
            this.label15.Text = "Province";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(48, 329);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 17);
            this.label12.TabIndex = 44;
            this.label12.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.DimGray;
            this.label13.Location = new System.Drawing.Point(23, 329);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 17);
            this.label13.TabIndex = 43;
            this.label13.Text = "City";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(108, 254);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 17);
            this.label10.TabIndex = 42;
            this.label10.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.DimGray;
            this.label11.Location = new System.Drawing.Point(18, 254);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 17);
            this.label11.TabIndex = 41;
            this.label11.Text = "Delivery Status";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(343, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 17);
            this.label8.TabIndex = 40;
            this.label8.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(294, 184);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 17);
            this.label9.TabIndex = 39;
            this.label9.Text = "Vehicles";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(121, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 17);
            this.label6.TabIndex = 38;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(23, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 17);
            this.label7.TabIndex = 37;
            this.label7.Text = "Transaction Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(390, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 17);
            this.label4.TabIndex = 36;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(290, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 35;
            this.label5.Text = "Tracking Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(143, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 17);
            this.label3.TabIndex = 34;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(19, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 17);
            this.label2.TabIndex = 33;
            this.label2.Text = "Deliver Order Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Lexend SemiBold", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 25);
            this.label1.TabIndex = 32;
            this.label1.Text = "Edit Delivery Details";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImage = global::HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.Add_Stock_Popup;
            this.pictureBox1.Location = new System.Drawing.Point(0, -3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(578, 550);
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(3, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(575, 550);
            this.pictureBox2.TabIndex = 46;
            this.pictureBox2.TabStop = false;
            // 
            // DeliveryStatusComboBox
            // 
            this.DeliveryStatusComboBox.DropDownWidth = 126;
            this.DeliveryStatusComboBox.Items.AddRange(new object[] {
            "Site Delivery",
            "Pick-Up"});
            this.DeliveryStatusComboBox.Location = new System.Drawing.Point(21, 274);
            this.DeliveryStatusComboBox.Name = "DeliveryStatusComboBox";
            this.DeliveryStatusComboBox.Size = new System.Drawing.Size(150, 26);
            this.DeliveryStatusComboBox.StateCommon.ComboBox.Border.Rounding = 7F;
            this.DeliveryStatusComboBox.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.DeliveryStatusComboBox.TabIndex = 59;
            this.DeliveryStatusComboBox.Text = "Delivery status";
            // 
            // GenerateDeliveryOrder
            // 
            this.GenerateDeliveryOrder.Location = new System.Drawing.Point(21, 134);
            this.GenerateDeliveryOrder.Name = "GenerateDeliveryOrder";
            this.GenerateDeliveryOrder.Size = new System.Drawing.Size(193, 38);
            this.GenerateDeliveryOrder.StateCommon.Back.Color1 = System.Drawing.Color.DodgerBlue;
            this.GenerateDeliveryOrder.StateCommon.Back.Color2 = System.Drawing.Color.DodgerBlue;
            this.GenerateDeliveryOrder.StateCommon.Border.Rounding = 6F;
            this.GenerateDeliveryOrder.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.GenerateDeliveryOrder.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.GenerateDeliveryOrder.TabIndex = 60;
            this.GenerateDeliveryOrder.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.GenerateDeliveryOrder.Values.Text = "Generate";
            // 
            // GenerateTrackingNumber
            // 
            this.GenerateTrackingNumber.Location = new System.Drawing.Point(293, 133);
            this.GenerateTrackingNumber.Name = "GenerateTrackingNumber";
            this.GenerateTrackingNumber.Size = new System.Drawing.Size(193, 38);
            this.GenerateTrackingNumber.StateCommon.Back.Color1 = System.Drawing.Color.DodgerBlue;
            this.GenerateTrackingNumber.StateCommon.Back.Color2 = System.Drawing.Color.DodgerBlue;
            this.GenerateTrackingNumber.StateCommon.Border.Rounding = 6F;
            this.GenerateTrackingNumber.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.GenerateTrackingNumber.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.GenerateTrackingNumber.TabIndex = 61;
            this.GenerateTrackingNumber.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.GenerateTrackingNumber.Values.Text = "Generate";
            // 
            // EditButtonEditDelivery
            // 
            this.EditButtonEditDelivery.Location = new System.Drawing.Point(409, 492);
            this.EditButtonEditDelivery.Name = "EditButtonEditDelivery";
            this.EditButtonEditDelivery.Size = new System.Drawing.Size(139, 38);
            this.EditButtonEditDelivery.StateCommon.Back.Color1 = System.Drawing.Color.DodgerBlue;
            this.EditButtonEditDelivery.StateCommon.Back.Color2 = System.Drawing.Color.DodgerBlue;
            this.EditButtonEditDelivery.StateCommon.Border.Rounding = 6F;
            this.EditButtonEditDelivery.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.EditButtonEditDelivery.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.EditButtonEditDelivery.TabIndex = 62;
            this.EditButtonEditDelivery.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.EditButtonEditDelivery.Values.Text = "Edit";
            // 
            // BackButtonEditDelivery
            // 
            this.BackButtonEditDelivery.Location = new System.Drawing.Point(264, 492);
            this.BackButtonEditDelivery.Name = "BackButtonEditDelivery";
            this.BackButtonEditDelivery.Size = new System.Drawing.Size(139, 38);
            this.BackButtonEditDelivery.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.BackButtonEditDelivery.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.BackButtonEditDelivery.StateCommon.Border.Color1 = System.Drawing.Color.Gray;
            this.BackButtonEditDelivery.StateCommon.Border.Color2 = System.Drawing.Color.Gray;
            this.BackButtonEditDelivery.StateCommon.Border.Rounding = 6F;
            this.BackButtonEditDelivery.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.BackButtonEditDelivery.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.BackButtonEditDelivery.TabIndex = 63;
            this.BackButtonEditDelivery.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.BackButtonEditDelivery.Values.Text = "Back";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.White;
            this.label14.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(329, 389);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(13, 17);
            this.label14.TabIndex = 65;
            this.label14.Text = "*";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.White;
            this.label17.Font = new System.Drawing.Font("Lexend SemiBold", 8F, System.Drawing.FontStyle.Bold);
            this.label17.ForeColor = System.Drawing.Color.DimGray;
            this.label17.Location = new System.Drawing.Point(294, 390);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 17);
            this.label17.TabIndex = 64;
            this.label17.Text = "Date";
            // 
            // DateInDeliveryDetails
            // 
            this.DateInDeliveryDetails.Location = new System.Drawing.Point(293, 412);
            this.DateInDeliveryDetails.Name = "DateInDeliveryDetails";
            this.DateInDeliveryDetails.Size = new System.Drawing.Size(174, 25);
            this.DateInDeliveryDetails.StateCommon.Border.Color1 = System.Drawing.Color.DodgerBlue;
            this.DateInDeliveryDetails.StateCommon.Border.Color2 = System.Drawing.Color.DodgerBlue;
            this.DateInDeliveryDetails.StateCommon.Border.Rounding = 6F;
            this.DateInDeliveryDetails.TabIndex = 66;
            // 
            // VehiclesSelectorComboBox
            // 
            this.VehiclesSelectorComboBox.DropDownWidth = 126;
            this.VehiclesSelectorComboBox.Items.AddRange(new object[] {
            ""});
            this.VehiclesSelectorComboBox.Location = new System.Drawing.Point(293, 204);
            this.VehiclesSelectorComboBox.Name = "VehiclesSelectorComboBox";
            this.VehiclesSelectorComboBox.Size = new System.Drawing.Size(130, 26);
            this.VehiclesSelectorComboBox.StateCommon.ComboBox.Border.Rounding = 7F;
            this.VehiclesSelectorComboBox.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.VehiclesSelectorComboBox.TabIndex = 67;
            this.VehiclesSelectorComboBox.Text = "Enter Province";
            // 
            // EditDeliveryDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.VehiclesSelectorComboBox);
            this.Controls.Add(this.DateInDeliveryDetails);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.BackButtonEditDelivery);
            this.Controls.Add(this.EditButtonEditDelivery);
            this.Controls.Add(this.GenerateTrackingNumber);
            this.Controls.Add(this.GenerateDeliveryOrder);
            this.Controls.Add(this.DeliveryStatusComboBox);
            this.Controls.Add(this.ZipCodeDeliveryDetailsTextBox);
            this.Controls.Add(this.ProvinceDeliveryDetailsComboBox);
            this.Controls.Add(this.CityDeliveryDetailsComboBox);
            this.Controls.Add(this.TransactionTypeTextBox);
            this.Controls.Add(this.TrackingNumberTextBox);
            this.Controls.Add(this.DeliveryOrderTextBox);
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
            this.DoubleBuffered = true;
            this.Name = "EditDeliveryDetails";
            this.Size = new System.Drawing.Size(578, 550);
            ((System.ComponentModel.ISupportInitialize)(this.ProvinceDeliveryDetailsComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CityDeliveryDetailsComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeliveryStatusComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VehiclesSelectorComboBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Krypton.Toolkit.KryptonTextBox ZipCodeDeliveryDetailsTextBox;
        private Krypton.Toolkit.KryptonComboBox ProvinceDeliveryDetailsComboBox;
        private Krypton.Toolkit.KryptonComboBox CityDeliveryDetailsComboBox;
        private Krypton.Toolkit.KryptonRichTextBox TransactionTypeTextBox;
        private Krypton.Toolkit.KryptonRichTextBox TrackingNumberTextBox;
        private Krypton.Toolkit.KryptonRichTextBox DeliveryOrderTextBox;
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
        private Krypton.Toolkit.KryptonComboBox DeliveryStatusComboBox;
        private Krypton.Toolkit.KryptonButton GenerateDeliveryOrder;
        private Krypton.Toolkit.KryptonButton GenerateTrackingNumber;
        private Krypton.Toolkit.KryptonButton EditButtonEditDelivery;
        private Krypton.Toolkit.KryptonButton BackButtonEditDelivery;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private Krypton.Toolkit.KryptonDateTimePicker DateInDeliveryDetails;
        private Krypton.Toolkit.KryptonComboBox VehiclesSelectorComboBox;
    }
}
