namespace Iris.Reports.Statistics
{
    partial class smTeachersMonesPollaples
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.GraphGroup graphGroup1 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.GraphTitle graphTitle1 = new Telerik.Reporting.GraphTitle();
            Telerik.Reporting.NumericalScale numericalScale1 = new Telerik.Reporting.NumericalScale();
            Telerik.Reporting.CategoryScale categoryScale1 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.GraphGroup graphGroup2 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
            this.����_����GroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.����_����GroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.������_�����CaptionTextBox = new Telerik.Reporting.TextBox();
            this.������_���������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.������_�����DataTextBox = new Telerik.Reporting.TextBox();
            this.������_���������DataTextBox = new Telerik.Reporting.TextBox();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.����_����CaptionTextBox = new Telerik.Reporting.TextBox();
            this.����_����DataTextBox = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.sqlSchoolYears = new Telerik.Reporting.SqlDataSource();
            this.graph1 = new Telerik.Reporting.Graph();
            this.polarCoordinateSystem1 = new Telerik.Reporting.PolarCoordinateSystem();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.sqlDataSource2 = new Telerik.Reporting.SqlDataSource();
            this.barSeries2 = new Telerik.Reporting.BarSeries();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = "SELECT        ����_����, ������, ������_�����, ������_���������, �������_����\r\nFR" +
    "OM            ����2���_������_�����_���������\r\nORDER BY ����_����";
            // 
            // ����_����GroupHeaderSection
            // 
            this.����_����GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.79999959468841553D);
            this.����_����GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.����_����CaptionTextBox,
            this.����_����DataTextBox});
            this.����_����GroupHeaderSection.Name = "����_����GroupHeaderSection";
            // 
            // ����_����GroupFooterSection
            // 
            this.����_����GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(14.199999809265137D);
            this.����_����GroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.graph1,
            this.textBox2,
            this.textBox3});
            this.����_����GroupFooterSection.Name = "����_����GroupFooterSection";
            this.����_����GroupFooterSection.Style.BackgroundColor = System.Drawing.Color.LightGray;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.800000011920929D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.������_�����CaptionTextBox,
            this.������_���������CaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.29999944567680359D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = true;
            // 
            // ������_�����CaptionTextBox
            // 
            this.������_�����CaptionTextBox.CanGrow = true;
            this.������_�����CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.������_�����CaptionTextBox.Name = "������_�����CaptionTextBox";
            this.������_�����CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.8677082061767578D), Telerik.Reporting.Drawing.Unit.Cm(0.784589946269989D));
            this.������_�����CaptionTextBox.Style.Font.Bold = true;
            this.������_�����CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_�����CaptionTextBox.StyleName = "Caption";
            this.������_�����CaptionTextBox.Value = "������������� �� ����� ���������";
            // 
            // ������_���������CaptionTextBox
            // 
            this.������_���������CaptionTextBox.CanGrow = true;
            this.������_���������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.8679084777832031D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.������_���������CaptionTextBox.Name = "������_���������CaptionTextBox";
            this.������_���������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.8911657333374023D), Telerik.Reporting.Drawing.Unit.Cm(0.784589946269989D));
            this.������_���������CaptionTextBox.Style.Font.Bold = true;
            this.������_���������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_���������CaptionTextBox.StyleName = "Caption";
            this.������_���������CaptionTextBox.Value = "������������� �� ��������� ���������";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.4154081344604492D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox,
            this.textBox20,
            this.textBox16});
            this.pageFooter.Name = "pageFooter";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(4.2000002861022949D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1,
            this.textBox1});
            this.reportHeader.Name = "reportHeader";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.70000046491622925D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.������_�����DataTextBox,
            this.������_���������DataTextBox});
            this.detail.Name = "detail";
            // 
            // ������_�����DataTextBox
            // 
            this.������_�����DataTextBox.CanGrow = true;
            this.������_�����DataTextBox.Format = "{0:N0}";
            this.������_�����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.������_�����DataTextBox.Name = "������_�����DataTextBox";
            this.������_�����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.8677082061767578D), Telerik.Reporting.Drawing.Unit.Cm(0.69990032911300659D));
            this.������_�����DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.������_�����DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_�����DataTextBox.StyleName = "Data";
            this.������_�����DataTextBox.Value = "=Fields.������_�����";
            // 
            // ������_���������DataTextBox
            // 
            this.������_���������DataTextBox.CanGrow = true;
            this.������_���������DataTextBox.Format = "{0:N0}";
            this.������_���������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.8913669586181641D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.������_���������DataTextBox.Name = "������_���������DataTextBox";
            this.������_���������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.8677082061767578D), Telerik.Reporting.Drawing.Unit.Cm(0.69990032911300659D));
            this.������_���������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(11D);
            this.������_���������DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_���������DataTextBox.StyleName = "Data";
            this.������_���������DataTextBox.Value = "=Fields.������_���������";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.TypeName = "Iris.Reports.A2Logo, Iris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.9205245971679688D), Telerik.Reporting.Drawing.Unit.Cm(2.9999997615814209D));
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(3.2999999523162842D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(17.841251373291016D), Telerik.Reporting.Drawing.Unit.Cm(0.713233232498169D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "������ ������������� ����� ��� ��������� ���������";
            // 
            // ����_����CaptionTextBox
            // 
            this.����_����CaptionTextBox.CanGrow = true;
            this.����_����CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.����_����CaptionTextBox.Name = "����_����CaptionTextBox";
            this.����_����CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0178277492523193D), Telerik.Reporting.Drawing.Unit.Cm(0.79989945888519287D));
            this.����_����CaptionTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.����_����CaptionTextBox.Style.Font.Bold = true;
            this.����_����CaptionTextBox.Style.Font.Name = "Calibri";
            this.����_����CaptionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(13D);
            this.����_����CaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.����_����CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.����_����CaptionTextBox.StyleName = "Caption";
            this.����_����CaptionTextBox.Value = "������� ����:";
            // 
            // ����_����DataTextBox
            // 
            this.����_����DataTextBox.CanGrow = true;
            this.����_����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.0180280208587646D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.����_����DataTextBox.Name = "����_����DataTextBox";
            this.����_����DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(14.741046905517578D), Telerik.Reporting.Drawing.Unit.Cm(0.79989945888519287D));
            this.����_����DataTextBox.Style.BackgroundColor = System.Drawing.Color.LightGray;
            this.����_����DataTextBox.Style.Font.Bold = true;
            this.����_����DataTextBox.Style.Font.Name = "Calibri";
            this.����_����DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(13D);
            this.����_����DataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Pixel(4D);
            this.����_����DataTextBox.StyleName = "Data";
            this.����_����DataTextBox.Value = "=Fields.����_����";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.086241506040096283D), Telerik.Reporting.Drawing.Unit.Cm(0.85604184865951538D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.8412480354309082D), Telerik.Reporting.Drawing.Unit.Cm(0.55936628580093384D));
            this.currentTimeTextBox.Style.Font.Name = "Calibri";
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.484366416931152D), Telerik.Reporting.Drawing.Unit.Cm(0.85604184865951538D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.3469834327697754D), Telerik.Reporting.Drawing.Unit.Cm(0.55936628580093384D));
            this.pageInfoTextBox.Style.Font.Name = "Calibri";
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"���. \" + PageNumber + \"/\" + PageCount";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.086241506040096283D), Telerik.Reporting.Drawing.Unit.Cm(0.37979188561439514D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.8412480354309082D), Telerik.Reporting.Drawing.Unit.Cm(0.45978257060050964D));
            this.textBox20.Style.Font.Name = "Calibri";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox20.StyleName = "PageInfo";
            this.textBox20.Value = "�������� IRIS";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.484366416931152D), Telerik.Reporting.Drawing.Unit.Cm(0.37979188561439514D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.3568844795227051D), Telerik.Reporting.Drawing.Unit.Cm(0.45978257060050964D));
            this.textBox16.Style.Font.Name = "Calibri";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox16.StyleName = "PageInfo";
            this.textBox16.Value = "��������� ���������: ��. �������� ����������";
            // 
            // sqlSchoolYears
            // 
            this.sqlSchoolYears.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlSchoolYears.Name = "sqlSchoolYears";
            this.sqlSchoolYears.SelectCommand = "SELECT        SCHOOLYEAR_ID, �������_����\r\nFROM            ���_�������_���\r\nORDER" +
    " BY �������_����";
            // 
            // graph1
            // 
            graphGroup1.Label = "�������������";
            graphGroup1.Name = "categoryGroup";
            this.graph1.CategoryGroups.Add(graphGroup1);
            this.graph1.CoordinateSystems.Add(this.polarCoordinateSystem1);
            this.graph1.DataSource = this.sqlDataSource2;
            this.graph1.Filters.Add(new Telerik.Reporting.Filter("=Fields.�������_����", Telerik.Reporting.FilterOperator.Equal, "=Parameters.school_year.Value"));
            this.graph1.Legend.IsInsidePlotArea = false;
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Legend.Style.Visible = true;
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.5027083158493042D), Telerik.Reporting.Drawing.Unit.Cm(1.7000004053115845D));
            this.graph1.Name = "graph1";
            this.graph1.NoDataMessage = "";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Series.Add(this.barSeries2);
            this.graph1.SeriesGroups.Add(graphGroup2);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.893966674804688D), Telerik.Reporting.Drawing.Unit.Cm(11.899998664855957D));
            this.graph1.Style.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            graphTitle1.Style.Font.Bold = true;
            graphTitle1.Style.LineColor = System.Drawing.Color.LightGray;
            graphTitle1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            graphTitle1.Text = "������ ����� ��� ��������� ���������";
            this.graph1.Titles.Add(graphTitle1);
            // 
            // polarCoordinateSystem1
            // 
            this.polarCoordinateSystem1.AngularAxis = this.graphAxis1;
            this.polarCoordinateSystem1.Name = "polarCoordinateSystem2";
            this.polarCoordinateSystem1.RadialAxis = this.graphAxis2;
            // 
            // graphAxis1
            // 
            this.graphAxis1.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis1.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis1.MinorGridLineStyle.Visible = false;
            this.graphAxis1.Name = "GraphAxis2";
            this.graphAxis1.Scale = numericalScale1;
            this.graphAxis1.Style.Visible = false;
            // 
            // graphAxis2
            // 
            this.graphAxis2.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis2.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis2.MinorGridLineStyle.Visible = false;
            this.graphAxis2.Name = "GraphAxis1";
            categoryScale1.SpacingSlotCount = 0D;
            this.graphAxis2.Scale = categoryScale1;
            this.graphAxis2.Style.Visible = false;
            // 
            // sqlDataSource2
            // 
            this.sqlDataSource2.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlDataSource2.Name = "sqlDataSource2";
            this.sqlDataSource2.SelectCommand = "SELECT        ����_����, ������, ������, �������, �������������, �������_����\r\nFR" +
    "OM            ����2���_������_�����_���������_�������";
            // 
            // barSeries2
            // 
            this.barSeries2.ArrangeMode = Telerik.Reporting.GraphSeriesArrangeMode.Stacked100;
            this.barSeries2.CategoryGroup = graphGroup1;
            this.barSeries2.CoordinateSystem = this.polarCoordinateSystem1;
            this.barSeries2.DataPointLabel = "= Fields.�������";
            this.barSeries2.DataPointLabelFormat = "{0:P2}";
            this.barSeries2.LegendItem.Style.BackgroundColor = System.Drawing.Color.Transparent;
            this.barSeries2.LegendItem.Style.LineColor = System.Drawing.Color.Transparent;
            this.barSeries2.LegendItem.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.barSeries2.LegendItem.Value = "= Fields.�������������";
            this.barSeries2.Name = "barSeries1";
            graphGroup2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.�������������"));
            graphGroup2.Name = "seriesGroup";
            graphGroup2.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.�������", Telerik.Reporting.SortDirection.Desc));
            this.barSeries2.SeriesGroup = graphGroup2;
            this.barSeries2.X = "������";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(17.759075164794922D), Telerik.Reporting.Drawing.Unit.Cm(0.5999000072479248D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "�����: ������ ������������� �� ������� �� ��� ���� ������������ ������";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.60020077228546143D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(17.759075164794922D), Telerik.Reporting.Drawing.Unit.Cm(0.5999000072479248D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "���������: ������ ������������� �� ��������� �� ������������ ��� ���� �����������" +
    "�� �������";
            // 
            // smTeachersMonesPollaples
            // 
            this.DataSource = this.sqlDataSource1;
            this.Filters.Add(new Telerik.Reporting.Filter("=Fields.�������_����", Telerik.Reporting.FilterOperator.Equal, "=Parameters.school_year.Value"));
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
            this.Name = "smTeachersMonesPollaples";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(20D), Telerik.Reporting.Drawing.Unit.Mm(20D));
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
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(17.894166946411133D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource sqlDataSource1;
        private Telerik.Reporting.GroupHeaderSection ����_����GroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection ����_����GroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox ������_�����CaptionTextBox;
        private Telerik.Reporting.TextBox ������_���������CaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox ������_�����DataTextBox;
        private Telerik.Reporting.TextBox ������_���������DataTextBox;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox ����_����CaptionTextBox;
        private Telerik.Reporting.TextBox ����_����DataTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.SqlDataSource sqlSchoolYears;
        private Telerik.Reporting.Graph graph1;
        private Telerik.Reporting.PolarCoordinateSystem polarCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.SqlDataSource sqlDataSource2;
        private Telerik.Reporting.BarSeries barSeries2;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;

    }
}