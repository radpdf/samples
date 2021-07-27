<%@ Page Language="VB" CodeFile="PDF-Form-Filler.aspx.vb" Inherits="PDF_Form_Filler" %>

<%@ Register Assembly="RadPdf" Namespace="RadPdf.Web.UI" TagPrefix="radPdf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RAD PDF Sample</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <radPdf:PdfWebControl id="PdfWebControl1" runat="server" height="600px" width="100%" 
            HideBottomBar="True"
            HideTopBar="True"
            HideBookmarks="True"
            HideThumbnails="True"
            ViewerPageLayoutDefault="SinglePageContinuous"
            ViewerZoomDefault="ZoomFitWidth100" />
    </div>
    </form>
</body>
</html>
