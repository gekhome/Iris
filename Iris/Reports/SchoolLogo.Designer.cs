namespace Iris.Reports
{
    partial class SchoolLogo
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchoolLogo));
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.Â–ŸÕ’Ã…¡CaptionTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.Â–ŸÕ’Ã…¡DataTextBox = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT        ”◊œÀ«_ Ÿƒ, ≈–ŸÕ’Ã…¡\r\nFROM            ”’”_”◊œÀ≈”\r\nORDER BY ƒœÃ«, ≈–Ÿ" +
    "Õ’Ã…¡";
            // 
            // Â–ŸÕ’Ã…¡CaptionTextBox
            // 
            this.Â–ŸÕ’Ã…¡CaptionTextBox.CanGrow = true;
            this.Â–ŸÕ’Ã…¡CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.3003004789352417D));
            this.Â–ŸÕ’Ã…¡CaptionTextBox.Name = "Â–ŸÕ’Ã…¡CaptionTextBox";
            this.Â–ŸÕ’Ã…¡CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.9942669868469238D), Telerik.Reporting.Drawing.Unit.Cm(0.54708343744277954D));
            this.Â–ŸÕ’Ã…¡CaptionTextBox.Style.Font.Bold = true;
            this.Â–ŸÕ’Ã…¡CaptionTextBox.StyleName = "Caption";
            this.Â–ŸÕ’Ã…¡CaptionTextBox.Value = " ≈‘≈ ";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(2.4475831985473633D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Â–ŸÕ’Ã…¡DataTextBox,
            this.Â–ŸÕ’Ã…¡CaptionTextBox,
            this.pictureBox1});
            this.detail.Name = "detail";
            // 
            // Â–ŸÕ’Ã…¡DataTextBox
            // 
            this.Â–ŸÕ’Ã…¡DataTextBox.CanGrow = true;
            this.Â–ŸÕ’Ã…¡DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.8475837707519531D));
            this.Â–ŸÕ’Ã…¡DataTextBox.Name = "Â–ŸÕ’Ã…¡DataTextBox";
            this.Â–ŸÕ’Ã…¡DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.9942669868469238D), Telerik.Reporting.Drawing.Unit.Cm(0.59999930858612061D));
            this.Â–ŸÕ’Ã…¡DataTextBox.Style.Font.Bold = true;
            this.Â–ŸÕ’Ã…¡DataTextBox.StyleName = "Data";
            this.Â–ŸÕ’Ã…¡DataTextBox.Value = "=Fields.≈–ŸÕ’Ã…¡";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6998997926712036D), Telerik.Reporting.Drawing.Unit.Cm(1.3000001907348633D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.Stretch;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // SchoolLogo
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.”◊œÀ«_ Ÿƒ", Telerik.Reporting.FilterOperator.Equal, "=Parameters.schoolID.Value"));
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "SchoolLogo";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlDataSource1;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.≈–ŸÕ’Ã…¡";
            reportParameter1.AvailableValues.ValueMember = "= Fields.”◊œÀ«_ Ÿƒ";
            reportParameter1.Name = "schoolID";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter1.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.Black;
            styleRule1.Style.Font.Bold = true;
            styleRule1.Style.Font.Italic = false;
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule1.Style.Font.Strikeout = false;
            styleRule1.Style.Font.Underline = false;
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(8D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.TextBox Â–ŸÕ’Ã…¡CaptionTextBox;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox Â–ŸÕ’Ã…¡DataTextBox;
        private Telerik.Reporting.PictureBox pictureBox1;

    }
}