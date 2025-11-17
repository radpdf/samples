<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/Kendo-UI.aspx.cs" CodeFile="Kendo-UI.aspx.cs" Inherits="Kendo_UI" %>

<%@ Register Assembly="RadPdf" Namespace="RadPdf.Web.UI" TagPrefix="radPdf" %>

<!DOCTYPE html>
<html>
<head id="SampleHead" runat="server">
	<title>RAD PDF Sample</title>
	<link rel="stylesheet" href="kendo-ui/styles/kendo.common.min.css" />
	<link rel="stylesheet" href="kendo-ui/styles/kendo.default.min.css" />
	<link rel="stylesheet" href="kendo-ui/styles/kendo.default.mobile.min.css" />
	<link rel="stylesheet" href="example.css" />
	<script type="text/javascript" src="kendo-ui/js/jquery.min.js"></script>
	<script type="text/javascript" src="kendo-ui/js/kendo.ui.core.min.js"></script>
	<script type="text/javascript" src="example.js"></script>
</head>
<body>
	<form id="SampleForm" runat="server">
	<div id="kendo">
		<div id="kendo-menu"></div>
		<div id="kendo-toolbar"></div>
	</div>
	
	<div id="radpdf" style="display:none;overflow:hidden;">
		<radPdf:PdfWebControl ID="PdfWebControl1" RunAt="server"
			Height="100%" 
			Width="100%" 
			OnClientLoad="initRadPdf(this);" 
			HideBottomBar="True"
			HideDownloadButton="True"
			HideObjectPropertiesBar="True"
			HideRightClickMenu="True"
			HideSearchText="True"
			HideSideBar="True"
			HideSidePanels="True"
			HideTips="True"
			HideToolsTabs="True"
			HideTopBar="True"
			ViewerPageLayoutDefault="SinglePageContinuous"
			/>
	</div>

	<div id="object-properties">
		<table>
			<tr id="op-border-color">
				<td>Border Color:</td>
				<td>
					<input type="text" />
				</td>
			</tr>
			<tr id="op-border-width">
				<td>Border Width:</td>
				<td>
					<input type="text" />
				</td>
			</tr>
			<tr id="op-color">
				<td>Color:</td>
				<td>
					<input type="text" />
				</td>
			</tr>
			<tr id="op-fill-color">
				<td>Fill:</td>
				<td>
					<input type="text" />
				</td>
			</tr>
			<tr id="op-font-name">
				<td>Font Name:</td>
				<td>
					<select>
						<option>Arial</option>
						<option>Courier New</option>
						<option>Times New Roman</option>
					</select>
				</td>
			</tr>
			<tr id="op-font-size">
				<td>Font Size:</td>
				<td>
					<input type="text" />
				</td>
			</tr>
			<tr id="op-font-bold">
				<td>Font Bold:</td>
				<td>
					<input type="checkbox" />
				</td>
			</tr>
			<tr id="op-font-italic">
				<td>Font Italic:</td>
				<td>
					<input type="checkbox" />
				</td>
			</tr>
			<tr id="op-font-underline">
				<td>Font Underline:</td>
				<td>
					<input type="checkbox" />
				</td>
			</tr>
			<tr id="op-line-width">
				<td>Line Width:</td>
				<td>
					<input type="text" />
				</td>
			</tr>
			<tr id="op-pos-left">
				<td>Left:</td>
				<td>
					<input type="text" />
				</td>
			</tr>
			<tr id="op-pos-top">
				<td>Top:</td>
				<td>
					<input type="text" />
				</td>
			</tr>
			<tr id="op-pos-width">
				<td>Width:</td>
				<td>
					<input type="text" />
				</td>
			</tr>
			<tr id="op-pos-height">
				<td>Height:</td>
				<td>
					<input type="text" />
				</td>
			</tr>
			<tr id="op-save">
				<td colspan="2" style="text-align:center">
					<input style="display:inline-block" type="submit" value="Save" />
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>

