<%@ Page Language="VB" CodeFile="Upload.aspx.vb" Inherits="_Upload" %>

<%@ Register Assembly="RadPdf" Namespace="RadPdf.Web.UI" TagPrefix="radPdf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RAD PDF Sample</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <radPdf:PdfWebControlLite ID="PdfWebControl1" runat="server" height="600px" width="100%" 
               CollapsibleViewerSide="true" EnablePrintSettingsDialog="true" 
               ViewerSideDefault="None" ViewerZoomDefault="ZoomFitWidth100" />
    </div>
    </form>
</body>
</html>
