namespace Iris.Reports.Statistics
{
    partial class AnatheseisDetailsList3
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnatheseisDetailsList3));
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.����_����GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.����_����GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.����_����DataTextBox = new Telerik.Reporting.TextBox();
            this.����_����CaptionTextBox = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.����_���CaptionTextBox = new Telerik.Reporting.TextBox();
            this.�������_�����CaptionTextBox = new Telerik.Reporting.TextBox();
            this.�������������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.sqlSchoolYears = new Telerik.Reporting.SqlDataSource();
            this.sqlSimbaseis = new Telerik.Reporting.SqlDataSource();
            this.sqlPeriferiakes = new Telerik.Reporting.SqlDataSource();
            this.sqlApofaseisDates = new Telerik.Reporting.SqlDataSource();
            this.sqlEidikotitesUnified = new Telerik.Reporting.SqlDataSource();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.detail = new Telerik.Reporting.DetailSection();
            this.���DataTextBox = new Telerik.Reporting.TextBox();
            this.�������������DataTextBox = new Telerik.Reporting.TextBox();
            this.�������_�����DataTextBox = new Telerik.Reporting.TextBox();
            this.����_���DataTextBox = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.�������_����������DataTextBox = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.����������DataTextBox = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // ����_����GroupFooterSection
            // 
            this.����_����GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.29999908804893494D);
            this.����_����GroupFooterSection.Name = "����_����GroupFooterSection";
            this.����_����GroupFooterSection.Style.Visible = false;
            // 
            // ����_����GroupHeaderSection
            // 
            this.����_����GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.80000036954879761D);
            this.����_����GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.����_����DataTextBox,
            this.����_����CaptionTextBox});
            this.����_����GroupHeaderSection.Name = "����_����GroupHeaderSection";
            this.����_����GroupHeaderSection.PrintOnEveryPage = true;
            // 
            // ����_����DataTextBox
            // 
            this.����_����DataTextBox.CanGrow = true;
            this.����_����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.6750917434692383D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.����_����DataTextBox.Name = "����_����DataTextBox";
            this.����_����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(24.024808883666992D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.����_����DataTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.����_����DataTextBox.Style.Font.Bold = true;
            this.����_����DataTextBox.Style.Font.Name = "Calibri";
            this.����_����DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.����_����DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.����_����DataTextBox.StyleName = "Data";
            this.����_����DataTextBox.Value = "=Fields.����_���� + \"    -    \" + \"��������� ��������� ��� : \" + CDate(Parameters" +
    ".DateFrom).ToShortDateString() + \"   ��� : \" + CDate(Parameters.DateTo).ToShortD" +
    "ateString()";
            // 
            // ����_����CaptionTextBox
            // 
            this.����_����CaptionTextBox.CanGrow = true;
            this.����_����CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.027808379381895065D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.����_����CaptionTextBox.Name = "����_����CaptionTextBox";
            this.����_����CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.647083044052124D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.����_����CaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.����_����CaptionTextBox.Style.Font.Bold = true;
            this.����_����CaptionTextBox.Style.Font.Name = "Calibri";
            this.����_����CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.����_����CaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.����_����CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.����_����CaptionTextBox.StyleName = "Caption";
            this.����_����CaptionTextBox.Value = "������� ����:";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.26750868558883667D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = true;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.55865246057510376D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.����_���CaptionTextBox,
            this.�������_�����CaptionTextBox,
            this.�������������CaptionTextBox,
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox3,
            this.textBox4,
            this.textBox11});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.027808379381895065D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.87199169397354126D), Telerik.Reporting.Drawing.Unit.Cm(0.55834805965423584D));
            this.textBox5.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Name = "Calibri";
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "�/�";
            // 
            // ����_���CaptionTextBox
            // 
            this.����_���CaptionTextBox.CanGrow = true;
            this.����_���CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.700000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0.00020064989803358913D));
            this.����_���CaptionTextBox.Name = "����_���CaptionTextBox";
            this.����_���CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.89999836683273315D), Telerik.Reporting.Drawing.Unit.Cm(0.55844980478286743D));
            this.����_���CaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.����_���CaptionTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.����_���CaptionTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.����_���CaptionTextBox.Style.Font.Bold = true;
            this.����_���CaptionTextBox.Style.Font.Name = "Calibri";
            this.����_���CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.����_���CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.����_���CaptionTextBox.StyleName = "Caption";
            this.����_���CaptionTextBox.Value = "��/��";
            // 
            // �������_�����CaptionTextBox
            // 
            this.�������_�����CaptionTextBox.CanGrow = true;
            this.�������_�����CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.600198745727539D), Telerik.Reporting.Drawing.Unit.Cm(0.00010093052696902305D));
            this.�������_�����CaptionTextBox.Name = "�������_�����CaptionTextBox";
            this.�������_�����CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0996007919311523D), Telerik.Reporting.Drawing.Unit.Cm(0.55834805965423584D));
            this.�������_�����CaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.�������_�����CaptionTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.�������_�����CaptionTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.�������_�����CaptionTextBox.Style.Font.Bold = true;
            this.�������_�����CaptionTextBox.Style.Font.Name = "Calibri";
            this.�������_�����CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.�������_�����CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.�������_�����CaptionTextBox.StyleName = "Caption";
            this.�������_�����CaptionTextBox.Value = "������������ ���������";
            // 
            // �������������CaptionTextBox
            // 
            this.�������������CaptionTextBox.CanGrow = true;
            this.�������������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.00010254541848553345D));
            this.�������������CaptionTextBox.Name = "�������������CaptionTextBox";
            this.�������������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.55834805965423584D));
            this.�������������CaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.�������������CaptionTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.�������������CaptionTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.�������������CaptionTextBox.Style.Font.Bold = true;
            this.�������������CaptionTextBox.Style.Font.Name = "Calibri";
            this.�������������CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.�������������CaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.�������������CaptionTextBox.StyleName = "Caption";
            this.�������������CaptionTextBox.Value = "�������";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(24.500200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1897969245910645D), Telerik.Reporting.Drawing.Unit.Cm(0.55844980478286743D));
            this.textBox6.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Name = "Calibri";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.StyleName = "Caption";
            this.textBox6.Value = "�������";
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = true;
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.00010254541848553345D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.399800181388855D), Telerik.Reporting.Drawing.Unit.Cm(0.55834805965423584D));
            this.textBox7.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Font.Name = "Calibri";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.StyleName = "Caption";
            this.textBox7.Value = "���";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.700000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7999978065490723D), Telerik.Reporting.Drawing.Unit.Cm(0.5584481954574585D));
            this.textBox8.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Name = "Calibri";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox8.StyleName = "Caption";
            this.textBox8.Value = "������� ������";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.5998005867004395D), Telerik.Reporting.Drawing.Unit.Cm(0.558250367641449D));
            this.textBox3.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Name = "Calibri";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox3.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "������� ������-����������";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.3002004623413086D), Telerik.Reporting.Drawing.Unit.Cm(0.00030359902302734554D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3996000289916992D), Telerik.Reporting.Drawing.Unit.Cm(0.55834805965423584D));
            this.textBox4.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Calibri";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = "�����";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = true;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.7000007629394531D), Telerik.Reporting.Drawing.Unit.Cm(0.00030440647969953716D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3997993469238281D), Telerik.Reporting.Drawing.Unit.Cm(0.55834805965423584D));
            this.textBox11.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.textBox11.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Font.Name = "Calibri";
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox11.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox11.StyleName = "Caption";
            this.textBox11.Value = "���������";
            // 
            // sqlSchoolYears
            // 
            this.sqlSchoolYears.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlSchoolYears.Name = "sqlSchoolYears";
            this.sqlSchoolYears.SelectCommand = "SELECT        SCHOOLYEAR_ID, �������_����\r\nFROM            ���_�������_���\r\nORDER" +
    " BY �������_����";
            // 
            // sqlSimbaseis
            // 
            this.sqlSimbaseis.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlSimbaseis.Name = "sqlSimbaseis";
            this.sqlSimbaseis.SelectCommand = "SELECT        �������_���, �������_�����\r\nFROM            �������";
            // 
            // sqlPeriferiakes
            // 
            this.sqlPeriferiakes.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlPeriferiakes.Name = "sqlPeriferiakes";
            this.sqlPeriferiakes.SelectCommand = "SELECT        �������_����������, ��������_����������\r\nFROM            ���_������" +
    "�������\r\nORDER BY ��������_����������";
            // 
            // sqlApofaseisDates
            // 
            this.sqlApofaseisDates.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlApofaseisDates.Name = "sqlApofaseisDates";
            this.sqlApofaseisDates.SelectCommand = "SELECT        �����_�������, �������_����\r\nFROM            DM_APOFASEIS_DATES\r\nOR" +
    "DER BY �����_�������";
            // 
            // sqlEidikotitesUnified
            // 
            this.sqlEidikotitesUnified.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlEidikotitesUnified.Name = "sqlEidikotitesUnified";
            this.sqlEidikotitesUnified.SelectCommand = "SELECT        ROW_ID, ����������_������1, ������\r\nFROM            DM_EIDIKOTITES_" +
    "SELECTOR\r\nORDER BY ������, ����������_������1";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = resources.GetString("sqlDataSource1.SelectCommand");
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.87702780961990356D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox16,
            this.textBox20,
            this.pageInfoTextBox,
            this.currentTimeTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.343015670776367D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.3568844795227051D), Telerik.Reporting.Drawing.Unit.Cm(0.45978257060050964D));
            this.textBox16.Style.Font.Name = "Calibri";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox16.StyleName = "PageInfo";
            this.textBox16.Value = "��������� ���������: ��. �������� ����������";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.8412480354309082D), Telerik.Reporting.Drawing.Unit.Cm(0.45978257060050964D));
            this.textBox20.Style.Font.Name = "Calibri";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox20.StyleName = "PageInfo";
            this.textBox20.Value = "�������� IRIS";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.343015670776367D), Telerik.Reporting.Drawing.Unit.Cm(0.47702836990356445D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.3469834327697754D), Telerik.Reporting.Drawing.Unit.Cm(0.39999940991401672D));
            this.pageInfoTextBox.Style.Font.Name = "Calibri";
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"���. \" + PageNumber + \"/\" + PageCount";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.4600825309753418D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.8412480354309082D), Telerik.Reporting.Drawing.Unit.Cm(0.41694441437721252D));
            this.currentTimeTextBox.Style.Font.Name = "Calibri";
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(3.9999997615814209D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.subReport1});
            this.reportHeader.Name = "reportHeader";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(3.2914583683013916D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(26.699901580810547D), Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "���������� ��������� ��������� ������������ ������������� ��� ������� ����";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.TypeName = "Iris.Reports.A2Logo, Iris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.9205245971679688D), Telerik.Reporting.Drawing.Unit.Cm(2.9999997615814209D));
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.51734042167663574D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.���DataTextBox,
            this.�������������DataTextBox,
            this.�������_�����DataTextBox,
            this.����_���DataTextBox,
            this.textBox9,
            this.�������_����������DataTextBox,
            this.textBox2,
            this.textBox10,
            this.textBox12,
            this.����������DataTextBox});
            this.detail.Name = "detail";
            // 
            // ���DataTextBox
            // 
            this.���DataTextBox.CanGrow = false;
            this.���DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.027808379381895065D), Telerik.Reporting.Drawing.Unit.Cm(0.00019863128545694053D));
            this.���DataTextBox.Name = "���DataTextBox";
            this.���DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.871991753578186D), Telerik.Reporting.Drawing.Unit.Cm(0.51714015007019043D));
            this.���DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.���DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.���DataTextBox.Style.Font.Name = "Calibri";
            this.���DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.���DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.���DataTextBox.StyleName = "Data";
            this.���DataTextBox.Value = "=RowNumber()";
            // 
            // �������������DataTextBox
            // 
            this.�������������DataTextBox.CanGrow = false;
            this.�������������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.00019863128545694053D));
            this.�������������DataTextBox.Name = "�������������DataTextBox";
            this.�������������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.51714015007019043D));
            this.�������������DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.�������������DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.�������������DataTextBox.Style.Font.Name = "Calibri";
            this.�������������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.�������������DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.�������������DataTextBox.StyleName = "Data";
            this.�������������DataTextBox.Value = "= Fields.�������";
            // 
            // �������_�����DataTextBox
            // 
            this.�������_�����DataTextBox.CanGrow = false;
            this.�������_�����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.600198745727539D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.�������_�����DataTextBox.Name = "�������_�����DataTextBox";
            this.�������_�����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0996003150939941D), Telerik.Reporting.Drawing.Unit.Cm(0.51733881235122681D));
            this.�������_�����DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.�������_�����DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.�������_�����DataTextBox.Style.Font.Name = "Calibri";
            this.�������_�����DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.�������_�����DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.�������_�����DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.�������_�����DataTextBox.StyleName = "Data";
            this.�������_�����DataTextBox.Value = "= Fields.�����_�������������";
            // 
            // ����_���DataTextBox
            // 
            this.����_���DataTextBox.CanGrow = false;
            this.����_���DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.700000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.����_���DataTextBox.Name = "����_���DataTextBox";
            this.����_���DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.89999848604202271D), Telerik.Reporting.Drawing.Unit.Cm(0.51734042167663574D));
            this.����_���DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.����_���DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.����_���DataTextBox.Style.Font.Name = "Calibri";
            this.����_���DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.����_���DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.����_���DataTextBox.StyleName = "Data";
            this.����_���DataTextBox.Value = "=Fields.����_���";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = false;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D), Telerik.Reporting.Drawing.Unit.Cm(0.00020105361181776971D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.399800181388855D), Telerik.Reporting.Drawing.Unit.Cm(0.51713854074478149D));
            this.textBox9.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox9.Style.Font.Name = "Calibri";
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.StyleName = "Data";
            this.textBox9.Value = "= Fields.���";
            // 
            // �������_����������DataTextBox
            // 
            this.�������_����������DataTextBox.CanGrow = false;
            this.�������_����������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(24.500200271606445D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.�������_����������DataTextBox.Name = "�������_����������DataTextBox";
            this.�������_����������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1897966861724854D), Telerik.Reporting.Drawing.Unit.Cm(0.51733958721160889D));
            this.�������_����������DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.�������_����������DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.�������_����������DataTextBox.Style.Font.Name = "Calibri";
            this.�������_����������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.�������_����������DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.�������_����������DataTextBox.StyleName = "Data";
            this.�������_����������DataTextBox.Value = "=Fields.�������_����������";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = false;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.100000381469727D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.5998005867004395D), Telerik.Reporting.Drawing.Unit.Cm(0.51733881235122681D));
            this.textBox2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox2.Style.Font.Name = "Calibri";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "= Fields.����������_������";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = false;
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.3002004623413086D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3996002674102783D), Telerik.Reporting.Drawing.Unit.Cm(0.51733881235122681D));
            this.textBox10.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox10.Style.Font.Name = "Calibri";
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox10.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox10.StyleName = "Data";
            this.textBox10.Value = "= Fields.�����";
            // 
            // textBox12
            // 
            this.textBox12.CanGrow = false;
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.7000007629394531D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3997995853424072D), Telerik.Reporting.Drawing.Unit.Cm(0.51733881235122681D));
            this.textBox12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.textBox12.Style.Font.Name = "Calibri";
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox12.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.textBox12.StyleName = "Data";
            this.textBox12.Value = "= Fields.���������";
            // 
            // ����������DataTextBox
            // 
            this.����������DataTextBox.CanGrow = false;
            this.����������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(21.69999885559082D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.����������DataTextBox.Name = "����������DataTextBox";
            this.����������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.799999475479126D), Telerik.Reporting.Drawing.Unit.Cm(0.51734042167663574D));
            this.����������DataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.����������DataTextBox.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.����������DataTextBox.Style.Font.Name = "Calibri";
            this.����������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.����������DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.����������DataTextBox.StyleName = "Data";
            this.����������DataTextBox.Value = "= Fields.��������";
            // 
            // AnatheseisDetailsList3
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.�������_����", Telerik.Reporting.FilterOperator.Equal, "=Parameters.school_year.Value"));
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.�������", Telerik.Reporting.FilterOperator.In, "=Parameters.simbasiID.Value"));
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.�������_����������", Telerik.Reporting.FilterOperator.In, "=Parameters.periferiaki.Value"));
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.�����_�������", Telerik.Reporting.FilterOperator.GreaterOrEqual, "=Parameters.DateFrom.Value"));
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.�����_�������", Telerik.Reporting.FilterOperator.LessOrEqual, "=Parameters.DateTo.Value"));
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.����������_������", Telerik.Reporting.FilterOperator.In, "=Parameters.eidikotita.Value"));
            group1.GroupFooter = this.����_����GroupFooterSection;
            group1.GroupHeader = this.����_����GroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.����_����"));
            group1.Name = "����_����Group";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.����_����GroupHeaderSection,
            this.����_����GroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageFooter,
            this.reportHeader,
            this.detail});
            this.Name = "AnatheseisDetailsList3";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.sqlSchoolYears;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.�������_����";
            reportParameter1.AvailableValues.ValueMember = "= Fields.SCHOOLYEAR_ID";
            reportParameter1.Name = "school_year";
            reportParameter1.Text = "������� ����";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter1.Visible = true;
            reportParameter2.AllowNull = true;
            reportParameter2.AutoRefresh = true;
            reportParameter2.AvailableValues.DataSource = this.sqlSimbaseis;
            reportParameter2.AvailableValues.DisplayMember = "= Fields.�������_�����";
            reportParameter2.AvailableValues.ValueMember = "= Fields.�������_���";
            reportParameter2.MultiValue = true;
            reportParameter2.Name = "simbasiID";
            reportParameter2.Text = "�������";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.Visible = true;
            reportParameter3.AllowNull = true;
            reportParameter3.AutoRefresh = true;
            reportParameter3.AvailableValues.DataSource = this.sqlPeriferiakes;
            reportParameter3.AvailableValues.DisplayMember = "= Fields.��������_����������";
            reportParameter3.AvailableValues.ValueMember = "= Fields.�������_����������";
            reportParameter3.MultiValue = true;
            reportParameter3.Name = "periferiaki";
            reportParameter3.Text = "������������";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Visible = true;
            reportParameter4.AllowNull = true;
            reportParameter4.AutoRefresh = true;
            reportParameter4.AvailableValues.DataSource = this.sqlApofaseisDates;
            reportParameter4.AvailableValues.DisplayMember = "= Format(\"{0:dd/MM/yyy}\",Fields.�����_�������)";
            reportParameter4.AvailableValues.Filters.Add(new Telerik.Reporting.Filter("=Fields.�������_����", Telerik.Reporting.FilterOperator.Equal, "=Parameters.school_year.Value"));
            reportParameter4.AvailableValues.ValueMember = "= Fields.�����_�������";
            reportParameter4.Name = "DateFrom";
            reportParameter4.Text = "������� ���";
            reportParameter4.Visible = true;
            reportParameter5.AllowNull = true;
            reportParameter5.AutoRefresh = true;
            reportParameter5.AvailableValues.DataSource = this.sqlApofaseisDates;
            reportParameter5.AvailableValues.DisplayMember = "= Format(\"{0:dd/MM/yyy}\",Fields.�����_�������)";
            reportParameter5.AvailableValues.Filters.Add(new Telerik.Reporting.Filter("=Fields.�������_����", Telerik.Reporting.FilterOperator.Equal, "=Parameters.school_year.Value"));
            reportParameter5.AvailableValues.ValueMember = "= Fields.�����_�������";
            reportParameter5.Name = "DateTo";
            reportParameter5.Text = "������� ���";
            reportParameter5.Visible = true;
            reportParameter6.AllowNull = true;
            reportParameter6.AutoRefresh = true;
            reportParameter6.AvailableValues.DataSource = this.sqlEidikotitesUnified;
            reportParameter6.AvailableValues.DisplayMember = "= Fields.����������_������1";
            reportParameter6.AvailableValues.ValueMember = "= Fields.����������_������1";
            reportParameter6.MultiValue = true;
            reportParameter6.Name = "eidikotita";
            reportParameter6.Text = "���������� ������";
            reportParameter6.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(26.699901580810547D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.GroupHeaderSection ����_����GroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection ����_����GroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox ���DataTextBox;
        private Telerik.Reporting.TextBox �������������DataTextBox;
        private Telerik.Reporting.TextBox ����������DataTextBox;
        private Telerik.Reporting.TextBox �������_�����DataTextBox;
        private Telerik.Reporting.TextBox ����_���DataTextBox;
        private Telerik.Reporting.TextBox �������_����������DataTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox ����_����DataTextBox;
        private Telerik.Reporting.TextBox ����_����CaptionTextBox;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox ����_���CaptionTextBox;
        private Telerik.Reporting.TextBox �������_�����CaptionTextBox;
        private Telerik.Reporting.TextBox �������������CaptionTextBox;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.SqlDataSource sqlSchoolYears;
        private Telerik.Reporting.SqlDataSource sqlPeriferiakes;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.SqlDataSource sqlEidikotitesUnified;
        private Telerik.Reporting.SqlDataSource sqlApofaseisDates;
        private Telerik.Reporting.SqlDataSource sqlSimbaseis;

    }
}