namespace Iris.Reports.Statistics
{
    partial class smTeachersKladosHours
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(smTeachersKladosHours));
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.NumericalScale numericalScale2 = new Telerik.Reporting.NumericalScale();
            Telerik.Reporting.CategoryScale categoryScale2 = new Telerik.Reporting.CategoryScale();
            Telerik.Reporting.GraphGroup graphGroup3 = new Telerik.Reporting.GraphGroup();
            Telerik.Reporting.GraphGroup graphGroup4 = new Telerik.Reporting.GraphGroup();
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
            this.������SumFunctionTextBox = new Telerik.Reporting.TextBox();
            this.������_���������SumFunctionTextBox = new Telerik.Reporting.TextBox();
            this.������_����_���SumFunctionTextBox = new Telerik.Reporting.TextBox();
            this.������_����34���SumFunctionTextBox = new Telerik.Reporting.TextBox();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.������_���������CaptionTextBox = new Telerik.Reporting.TextBox();
            this.������_����_���CaptionTextBox = new Telerik.Reporting.TextBox();
            this.������_����34���CaptionTextBox = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.������DataTextBox = new Telerik.Reporting.TextBox();
            this.������DataTextBox = new Telerik.Reporting.TextBox();
            this.������_���������DataTextBox = new Telerik.Reporting.TextBox();
            this.������_����_���DataTextBox = new Telerik.Reporting.TextBox();
            this.������_����34���DataTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.����_����DataTextBox = new Telerik.Reporting.TextBox();
            this.����_����CaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.sqlSchoolYears = new Telerik.Reporting.SqlDataSource();
            this.graphAxis9 = new Telerik.Reporting.GraphAxis();
            this.graphAxis10 = new Telerik.Reporting.GraphAxis();
            this.polarCoordinateSystem2 = new Telerik.Reporting.PolarCoordinateSystem();
            this.barSeries1 = new Telerik.Reporting.BarSeries();
            this.graph1 = new Telerik.Reporting.Graph();
            this.polarCoordinateSystem1 = new Telerik.Reporting.PolarCoordinateSystem();
            this.graphAxis1 = new Telerik.Reporting.GraphAxis();
            this.graphAxis2 = new Telerik.Reporting.GraphAxis();
            this.barSeries2 = new Telerik.Reporting.BarSeries();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            this.sqlDataSource1.SelectCommand = resources.GetString("sqlDataSource1.SelectCommand");
            // 
            // ����_����GroupHeaderSection
            // 
            this.����_����GroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.79999959468841553D);
            this.����_����GroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.����_����DataTextBox,
            this.����_����CaptionTextBox});
            this.����_����GroupHeaderSection.Name = "����_����GroupHeaderSection";
            this.����_����GroupHeaderSection.PrintOnEveryPage = true;
            // 
            // ����_����GroupFooterSection
            // 
            this.����_����GroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(14.299998283386231D);
            this.����_����GroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.������SumFunctionTextBox,
            this.������_���������SumFunctionTextBox,
            this.������_����_���SumFunctionTextBox,
            this.������_����34���SumFunctionTextBox,
            this.textBox2,
            this.graph1});
            this.����_����GroupFooterSection.Name = "����_����GroupFooterSection";
            this.����_����GroupFooterSection.Style.BackgroundColor = System.Drawing.Color.LightGray;
            // 
            // ������SumFunctionTextBox
            // 
            this.������SumFunctionTextBox.CanGrow = true;
            this.������SumFunctionTextBox.Format = "{0:N0}";
            this.������SumFunctionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.5902972221374512D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.������SumFunctionTextBox.Name = "������SumFunctionTextBox";
            this.������SumFunctionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.3838663101196289D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.������SumFunctionTextBox.Style.Font.Bold = true;
            this.������SumFunctionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.������SumFunctionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������SumFunctionTextBox.StyleName = "Data";
            this.������SumFunctionTextBox.Value = "=Sum(Fields.������)";
            // 
            // ������_���������SumFunctionTextBox
            // 
            this.������_���������SumFunctionTextBox.CanGrow = true;
            this.������_���������SumFunctionTextBox.Format = "{0:N0}";
            this.������_���������SumFunctionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.9743633270263672D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.������_���������SumFunctionTextBox.Name = "������_���������SumFunctionTextBox";
            this.������_���������SumFunctionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5153334140777588D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.������_���������SumFunctionTextBox.Style.Font.Bold = true;
            this.������_���������SumFunctionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.������_���������SumFunctionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_���������SumFunctionTextBox.StyleName = "Data";
            this.������_���������SumFunctionTextBox.Value = "=Sum(Fields.������_���������)";
            // 
            // ������_����_���SumFunctionTextBox
            // 
            this.������_����_���SumFunctionTextBox.CanGrow = true;
            this.������_����_���SumFunctionTextBox.Format = "{0:N0}";
            this.������_����_���SumFunctionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.489896774291992D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.������_����_���SumFunctionTextBox.Name = "������_����_���SumFunctionTextBox";
            this.������_����_���SumFunctionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.3999989032745361D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.������_����_���SumFunctionTextBox.Style.Font.Bold = true;
            this.������_����_���SumFunctionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.������_����_���SumFunctionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_����_���SumFunctionTextBox.StyleName = "Data";
            this.������_����_���SumFunctionTextBox.Value = "=Sum(Fields.������_����_���)";
            // 
            // ������_����34���SumFunctionTextBox
            // 
            this.������_����34���SumFunctionTextBox.CanGrow = true;
            this.������_����34���SumFunctionTextBox.Format = "{0:N0}";
            this.������_����34���SumFunctionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.890096664428711D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.������_����34���SumFunctionTextBox.Name = "������_����34���SumFunctionTextBox";
            this.������_����34���SumFunctionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9412505626678467D), Telerik.Reporting.Drawing.Unit.Cm(0.64708298444747925D));
            this.������_����34���SumFunctionTextBox.Style.Font.Bold = true;
            this.������_����34���SumFunctionTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.������_����34���SumFunctionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_����34���SumFunctionTextBox.StyleName = "Data";
            this.������_����34���SumFunctionTextBox.Value = "=Sum(Fields.������_����34���)";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.72655808925628662D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.������CaptionTextBox,
            this.������CaptionTextBox,
            this.������_���������CaptionTextBox,
            this.������_����_���CaptionTextBox,
            this.������_����34���CaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.29999944567680359D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // ������CaptionTextBox
            // 
            this.������CaptionTextBox.CanGrow = true;
            this.������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.65070480108261108D), Telerik.Reporting.Drawing.Unit.Cm(0.02655845507979393D));
            this.������CaptionTextBox.Name = "������CaptionTextBox";
            this.������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9492948055267334D), Telerik.Reporting.Drawing.Unit.Cm(0.69999963045120239D));
            this.������CaptionTextBox.Style.Font.Bold = true;
            this.������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������CaptionTextBox.StyleName = "Caption";
            this.������CaptionTextBox.Value = "������";
            // 
            // ������CaptionTextBox
            // 
            this.������CaptionTextBox.CanGrow = true;
            this.������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.6001996994018555D), Telerik.Reporting.Drawing.Unit.Cm(0.02655845507979393D));
            this.������CaptionTextBox.Name = "������CaptionTextBox";
            this.������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.3838672637939453D), Telerik.Reporting.Drawing.Unit.Cm(0.69999963045120239D));
            this.������CaptionTextBox.Style.Font.Bold = true;
            this.������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������CaptionTextBox.StyleName = "Caption";
            this.������CaptionTextBox.Value = "������ �������������";
            // 
            // ������_���������CaptionTextBox
            // 
            this.������_���������CaptionTextBox.CanGrow = true;
            this.������_���������CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.9842667579650879D), Telerik.Reporting.Drawing.Unit.Cm(0.02655845507979393D));
            this.������_���������CaptionTextBox.Name = "������_���������CaptionTextBox";
            this.������_���������CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5153334140777588D), Telerik.Reporting.Drawing.Unit.Cm(0.69999963045120239D));
            this.������_���������CaptionTextBox.Style.Font.Bold = true;
            this.������_���������CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_���������CaptionTextBox.StyleName = "Caption";
            this.������_���������CaptionTextBox.Value = "������ ���������";
            // 
            // ������_����_���CaptionTextBox
            // 
            this.������_����_���CaptionTextBox.CanGrow = true;
            this.������_����_���CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.499799728393555D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.������_����_���CaptionTextBox.Name = "������_����_���CaptionTextBox";
            this.������_����_���CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.3999989032745361D), Telerik.Reporting.Drawing.Unit.Cm(0.69999963045120239D));
            this.������_����_���CaptionTextBox.Style.Font.Bold = true;
            this.������_����_���CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_����_���CaptionTextBox.StyleName = "Caption";
            this.������_����_���CaptionTextBox.Value = "������ ����/���";
            // 
            // ������_����34���CaptionTextBox
            // 
            this.������_����34���CaptionTextBox.CanGrow = true;
            this.������_����34���CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.899998664855957D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.������_����34���CaptionTextBox.Name = "������_����34���CaptionTextBox";
            this.������_����34���CaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9412491321563721D), Telerik.Reporting.Drawing.Unit.Cm(0.69999963045120239D));
            this.������_����34���CaptionTextBox.Style.Font.Bold = true;
            this.������_����34���CaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_����34���CaptionTextBox.StyleName = "Caption";
            this.������_����34���CaptionTextBox.Value = "������ ���� 34 ���.";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.2710323333740234D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox16,
            this.textBox20,
            this.pageInfoTextBox,
            this.currentTimeTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(4.0999999046325684D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.subReport1});
            this.reportHeader.Name = "reportHeader";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.77344316244125366D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.������DataTextBox,
            this.������DataTextBox,
            this.������_���������DataTextBox,
            this.������_����_���DataTextBox,
            this.������_����34���DataTextBox});
            this.detail.Name = "detail";
            // 
            // ������DataTextBox
            // 
            this.������DataTextBox.CanGrow = true;
            this.������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.65070480108261108D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.������DataTextBox.Name = "������DataTextBox";
            this.������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9492944478988648D), Telerik.Reporting.Drawing.Unit.Cm(0.567608654499054D));
            this.������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.������DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������DataTextBox.StyleName = "Data";
            this.������DataTextBox.Value = "=Fields.������";
            // 
            // ������DataTextBox
            // 
            this.������DataTextBox.CanGrow = true;
            this.������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.6001996994018555D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.������DataTextBox.Name = "������DataTextBox";
            this.������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.3838672637939453D), Telerik.Reporting.Drawing.Unit.Cm(0.567608654499054D));
            this.������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.������DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������DataTextBox.StyleName = "Data";
            this.������DataTextBox.Value = "=Fields.������";
            // 
            // ������_���������DataTextBox
            // 
            this.������_���������DataTextBox.CanGrow = true;
            this.������_���������DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.9842658042907715D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.������_���������DataTextBox.Name = "������_���������DataTextBox";
            this.������_���������DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.5153334140777588D), Telerik.Reporting.Drawing.Unit.Cm(0.567608654499054D));
            this.������_���������DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.������_���������DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_���������DataTextBox.StyleName = "Data";
            this.������_���������DataTextBox.Value = "=Fields.������_���������";
            // 
            // ������_����_���DataTextBox
            // 
            this.������_����_���DataTextBox.CanGrow = true;
            this.������_����_���DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.499799728393555D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.������_����_���DataTextBox.Name = "������_����_���DataTextBox";
            this.������_����_���DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.62032508850097656D));
            this.������_����_���DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.������_����_���DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_����_���DataTextBox.StyleName = "Data";
            this.������_����_���DataTextBox.Value = "=Fields.������_����_���";
            // 
            // ������_����34���DataTextBox
            // 
            this.������_����34���DataTextBox.CanGrow = true;
            this.������_����34���DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.������_����34���DataTextBox.Name = "������_����34���DataTextBox";
            this.������_����34���DataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9412505626678467D), Telerik.Reporting.Drawing.Unit.Cm(0.62052536010742188D));
            this.������_����34���DataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.������_����34���DataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.������_����34���DataTextBox.StyleName = "Data";
            this.������_����34���DataTextBox.Value = "=Fields.������_����34���";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(3.3867666721343994D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(17.841251373291016D), Telerik.Reporting.Drawing.Unit.Cm(0.713233232498169D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Font.Name = "Calibri";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "������ ������������� ��� �������� ����  ��� ����� (��, ��, ��)";
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.subReport1.Name = "subReport1";
            typeReportSource1.TypeName = "Iris.Reports.A2Logo, Iris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            this.subReport1.ReportSource = typeReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.9205245971679688D), Telerik.Reporting.Drawing.Unit.Cm(2.9999997615814209D));
            // 
            // ����_����DataTextBox
            // 
            this.����_����DataTextBox.CanGrow = true;
            this.����_����DataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.1001999378204346D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
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
            // ����_����CaptionTextBox
            // 
            this.����_����CaptionTextBox.CanGrow = true;
            this.����_����CaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.0821719840168953D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
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
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.484362602233887D), Telerik.Reporting.Drawing.Unit.Cm(0.23541602492332459D));
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
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.086238272488117218D), Telerik.Reporting.Drawing.Unit.Cm(0.23541602492332459D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.8412480354309082D), Telerik.Reporting.Drawing.Unit.Cm(0.45978257060050964D));
            this.textBox20.Style.Font.Name = "Calibri";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox20.StyleName = "PageInfo";
            this.textBox20.Value = "�������� IRIS";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.484362602233887D), Telerik.Reporting.Drawing.Unit.Cm(0.71166598796844482D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.3469834327697754D), Telerik.Reporting.Drawing.Unit.Cm(0.55936628580093384D));
            this.pageInfoTextBox.Style.Font.Name = "Calibri";
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=\"���. \" + PageNumber + \"/\" + PageCount";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.086238272488117218D), Telerik.Reporting.Drawing.Unit.Cm(0.71166598796844482D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.8412480354309082D), Telerik.Reporting.Drawing.Unit.Cm(0.55936628580093384D));
            this.currentTimeTextBox.Style.Font.Name = "Calibri";
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.640802264213562D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9492948055267334D), Telerik.Reporting.Drawing.Unit.Cm(0.64708375930786133D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "������";
            // 
            // sqlSchoolYears
            // 
            this.sqlSchoolYears.ConnectionString = "Iris.Properties.Settings.DBConnectionString";
            this.sqlSchoolYears.Name = "sqlSchoolYears";
            this.sqlSchoolYears.SelectCommand = "SELECT        SCHOOLYEAR_ID, �������_����\r\nFROM            ���_�������_���\r\nORDER" +
    " BY �������_����";
            // 
            // graphAxis9
            // 
            this.graphAxis9.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis9.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis9.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis9.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis9.MinorGridLineStyle.Visible = false;
            this.graphAxis9.Name = "GraphAxis2";
            this.graphAxis9.Scale = numericalScale2;
            this.graphAxis9.Style.Visible = false;
            // 
            // graphAxis10
            // 
            this.graphAxis10.MajorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis10.MajorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis10.MinorGridLineStyle.LineColor = System.Drawing.Color.LightGray;
            this.graphAxis10.MinorGridLineStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            this.graphAxis10.MinorGridLineStyle.Visible = false;
            this.graphAxis10.Name = "GraphAxis1";
            categoryScale2.SpacingSlotCount = 0D;
            this.graphAxis10.Scale = categoryScale2;
            this.graphAxis10.Style.Visible = false;
            // 
            // polarCoordinateSystem2
            // 
            this.polarCoordinateSystem2.AngularAxis = this.graphAxis9;
            this.polarCoordinateSystem2.Name = "polarCoordinateSystem2";
            this.polarCoordinateSystem2.RadialAxis = this.graphAxis10;
            // 
            // barSeries1
            // 
            this.barSeries1.ArrangeMode = Telerik.Reporting.GraphSeriesArrangeMode.Stacked100;
            graphGroup3.Label = "= Fields.������";
            graphGroup3.Name = "categoryGroup";
            this.barSeries1.CategoryGroup = graphGroup3;
            this.barSeries1.CoordinateSystem = this.polarCoordinateSystem2;
            this.barSeries1.DataPointLabel = "= Fields.�������";
            this.barSeries1.DataPointLabelFormat = "{0:P2}";
            this.barSeries1.LegendItem.Style.BackgroundColor = System.Drawing.Color.Transparent;
            this.barSeries1.LegendItem.Style.LineColor = System.Drawing.Color.Transparent;
            this.barSeries1.LegendItem.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.barSeries1.LegendItem.Value = "= Fields.������";
            graphGroup4.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.������"));
            graphGroup4.Name = "seriesGroup";
            graphGroup4.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.�������", Telerik.Reporting.SortDirection.Desc));
            this.barSeries1.SeriesGroup = graphGroup4;
            this.barSeries1.X = "= Fields.������";
            // 
            // graph1
            // 
            graphGroup1.Label = "������";
            graphGroup1.Name = "categoryGroup";
            this.graph1.CategoryGroups.Add(graphGroup1);
            this.graph1.CoordinateSystems.Add(this.polarCoordinateSystem1);
            this.graph1.DataSource = this.sqlDataSource1;
            this.graph1.Filters.Add(new Telerik.Reporting.Filter("=Fields.�������_����", Telerik.Reporting.FilterOperator.Equal, "=Parameters.school_year.Value"));
            this.graph1.Legend.IsInsidePlotArea = false;
            this.graph1.Legend.Style.LineColor = System.Drawing.Color.LightGray;
            this.graph1.Legend.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Legend.Style.Visible = true;
            this.graph1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.5027083158493042D), Telerik.Reporting.Drawing.Unit.Cm(1.4999996423721314D));
            this.graph1.Name = "graph1";
            this.graph1.NoDataMessage = "";
            this.graph1.PlotAreaStyle.LineColor = System.Drawing.Color.LightGray;
            this.graph1.PlotAreaStyle.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            this.graph1.Series.Add(this.barSeries2);
            this.graph1.SeriesGroups.Add(graphGroup2);
            this.graph1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.893966674804688D), Telerik.Reporting.Drawing.Unit.Cm(12.100001335144043D));
            this.graph1.Style.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            graphTitle1.Style.Font.Bold = true;
            graphTitle1.Style.LineColor = System.Drawing.Color.LightGray;
            graphTitle1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Cm(0D);
            graphTitle1.Text = "�������� ������� ������������� ��� �����";
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
            this.barSeries2.LegendItem.Value = "= Fields.������";
            this.barSeries2.Name = "barSeries1";
            graphGroup2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.������"));
            graphGroup2.Name = "seriesGroup";
            graphGroup2.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.�������", Telerik.Reporting.SortDirection.Desc));
            this.barSeries2.SeriesGroup = graphGroup2;
            this.barSeries2.X = "������";
            // 
            // smTeachersKladosHoursTotal
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
            this.Name = "smTeachersKladosHoursTotal";
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
        private Telerik.Reporting.TextBox ������SumFunctionTextBox;
        private Telerik.Reporting.TextBox ������_���������SumFunctionTextBox;
        private Telerik.Reporting.TextBox ������_����_���SumFunctionTextBox;
        private Telerik.Reporting.TextBox ������_����34���SumFunctionTextBox;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox ������CaptionTextBox;
        private Telerik.Reporting.TextBox ������CaptionTextBox;
        private Telerik.Reporting.TextBox ������_���������CaptionTextBox;
        private Telerik.Reporting.TextBox ������_����_���CaptionTextBox;
        private Telerik.Reporting.TextBox ������_����34���CaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox ������DataTextBox;
        private Telerik.Reporting.TextBox ������DataTextBox;
        private Telerik.Reporting.TextBox ������_���������DataTextBox;
        private Telerik.Reporting.TextBox ������_����_���DataTextBox;
        private Telerik.Reporting.TextBox ������_����34���DataTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.SubReport subReport1;
        private Telerik.Reporting.TextBox ����_����DataTextBox;
        private Telerik.Reporting.TextBox ����_����CaptionTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.SqlDataSource sqlSchoolYears;
        private Telerik.Reporting.GraphAxis graphAxis9;
        private Telerik.Reporting.GraphAxis graphAxis10;
        private Telerik.Reporting.PolarCoordinateSystem polarCoordinateSystem2;
        private Telerik.Reporting.BarSeries barSeries1;
        private Telerik.Reporting.Graph graph1;
        private Telerik.Reporting.PolarCoordinateSystem polarCoordinateSystem1;
        private Telerik.Reporting.GraphAxis graphAxis1;
        private Telerik.Reporting.GraphAxis graphAxis2;
        private Telerik.Reporting.BarSeries barSeries2;

    }
}