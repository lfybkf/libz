<?xml version="1.0" encoding="windows-1251"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:Here="www.my.ru">
<xsl:import href="impCommon.xsl" />
<xsl:import href="impVBScript.xsl" />

<xsl:output method="text" encoding="windows-1251" />
<xsl:key name="Obj" match="//Obj" use="Id" />
<xsl:key name="Spec" match="//Spec" use="Id" />
<xsl:key name="Statobj" match="//Statobj" use="Id" />
<xsl:key name="RTP_R" match="//RTP[RT='true']" use="RefTrans" />
<xsl:key name="RTP_T" match="//RTP[RT='false']" use="RefTrans" />

<!--==============================================-->
<xsl:template match="/" >
<xsl:call-template name="VisioConst" />
<xsl:call-template name="Begin" />
<xsl:apply-templates select="//Obj" />
<xsl:call-template name="End" />
</xsl:template>
<!--==============================================-->

<!--==============================================-->
<xsl:template name="Begin">
'Begin BEG
set Args = WScript.Arguments
sTempDir = Args(0)
sFile = sTempDir + "\report.vsd"
sHtm = "report.htm"
set Args = Nothing

set App = CreateObject("Visio.Application")
'App.Visible = 0
App.ShowChanges = 0


set Sten = App.Documents.OpenEx("C:\Program Files\Microsoft Office\Visio10\1033\Solutions\Block Diagram\Basic Shapes.vss", 4)
set Doc = App.Documents.Add("") 'Basic Shapes.vss

set ShapeMaster = Sten.Masters("Rectangle")
set ConnMaster = Sten.Masters("Dynamic connector")
set TextMaster = Sten.Masters("Rounded rectangle") 'Shadowed Box Ellipse


Const colorRed = 2
Const colorGreen = 3

Const baseFinal = 10
Const baseInter = 3 'откуда начинать промежуточные статусы
Const rowsInter = 3
Const shiftInter = 2.5

Doc.PrintLandscape = True
Doc.HeaderCenter = "&amp;n"
Doc.FooterCenter = "&amp;n"
Doc.Title = "Граф переходов"

'Begin END
</xsl:template>
<!--==============================================-->

<!--==============================================-->
<xsl:template name="End">
set Page = Doc.Pages(1)
Page.Delete(0)

Doc.SaveAs(sFile)
Doc.Close()
App.Quit()

set ShapeMaster = Nothing
set ConnMaster = Nothing
set TextMaster = Nothing

set Shapes = Nothing
set Shape = Nothing
set Conn = Nothing

set Page = Nothing
set App = Nothing
set Sten = Nothing
set Doc = Nothing
</xsl:template>
<!--==============================================-->

<!--==============================================-->
<xsl:template match="Obj">
set Page = Doc.Pages.Add()
Page.Name = "<xsl:value-of select="Label" />"
Page.PageSheet.Cells("LineAdjustFrom") = visPLOLineAdjustFromAll
Page.PageSheet.Cells("LineRouteExt") = visLORouteExtNURBS
set Shapes = Page.Shapes

cntFinal = 9 'сверху
cntInter = 0

Set Shape = Page.Drop (TextMaster, 1, 7)
Shape.Text = "<xsl:value-of select="Label" />"
<xsl:call-template name="Statobj"><xsl:with-param name="RefObj" select="Id" /></xsl:call-template>
<xsl:call-template name="Trans"><xsl:with-param name="RefObj" select="Id" /></xsl:call-template>


</xsl:template>
<!--==============================================-->

<!--==============================================-->
<xsl:template name="Statobj">
	<xsl:param name="RefObj" />
<xsl:apply-templates select="//Statobj[Code='DRAFT']" />	
<xsl:apply-templates select="//Statobj[RefObj=$RefObj]" />
</xsl:template>
<!--==============================================-->

<!--==============================================-->
<xsl:template match="Statobj">
'Statobj BEGIN
sSpec = ""
<xsl:choose>
	<xsl:when test="Code='DRAFT'">
Set Shape = Page.Drop (ShapeMaster, 1, 4)
	</xsl:when>
	<xsl:when test="IsFinal=1">
cntFinal = cntFinal -2
Set Shape = Page.Drop (ShapeMaster, baseFinal, CntFinal)
	</xsl:when>
	<xsl:otherwise>
xPlace = baseInter + (cntInter\rowsInter)*shiftInter
yPlace = 2*(cntInter Mod rowsInter) + 2
cntInter = cntInter + 1
Set Shape = Page.Drop (ShapeMaster, xPlace, yPlace)
sSpec = Chr(10) + "<xsl:value-of select="key('Spec', RefSpec)/Name" />"
Shape.Data1 = CStr(xPlace) + " " + CStr(yPlace)
	</xsl:otherwise>
</xsl:choose>
Shape.Text = "<xsl:value-of select="concat(Name, ' (', Code, ')' )" />" + sSpec
Shape.Name = <xsl:value-of select="Id" />
Shape.Cells("LineWeight") = 0.05
Shape.Cells("ObjType") = 1
'Statobj END
</xsl:template>
<!--==============================================-->

<!--==============================================-->
<xsl:template name="Trans">
	<xsl:param name="RefObj" />
<xsl:apply-templates select="//Trans[RefObj=$RefObj]" />
</xsl:template>
<!--==============================================-->

<!--==============================================-->
<xsl:template match="Trans">


<xsl:choose>
	<xsl:when test="key('RTP_T', Id)">
iColor = colorGreen
	</xsl:when>
	<xsl:when test="key('RTP_R', Id)">
iColor = colorRed
	</xsl:when>
	<xsl:otherwise>
iColor = 0
	</xsl:otherwise>
</xsl:choose>

set Conn = Page.Drop(ConnMaster, 0, 0)
Conn.Cells("Rounding") = (<xsl:value-of select="Id" /> Mod 10) / 5
Conn.Cells("LineColor") = iColor
Conn.Cells("LineWeight") = 0.05
Conn.Cells("BeginArrow") = 10
Conn.Cells("BeginArrowSize") = 3
Conn.Cells("EndArrow") = 14
Conn.Cells("EndArrowSize") = 3
Conn.Cells("BeginX").GlueTo(Shapes("<xsl:value-of select="Src" />").Cells("PinY"))
Conn.Cells("EndX").GlueTo(Shapes("<xsl:value-of select="Dst" />").Cells("PinX"))

set HLink = Conn.AddHyperlink()
Hlink.Address = sHtm + "#Trans<xsl:value-of select="Id" />"
HLink.Description = "Описание"
</xsl:template>
<!--==============================================-->

<!--==============================================-->
<xsl:template match="RTP">
<xsl:variable name="RT">
	<xsl:if test="RT='false'">-ПРОВЕРКА.</xsl:if>
	<xsl:if test="RT='true'">+ДЕЙСТВИЕ.</xsl:if>
</xsl:variable>
</xsl:template>
<!--==============================================-->

</xsl:stylesheet>
