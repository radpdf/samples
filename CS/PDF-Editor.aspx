<%@ Page Language="C#" CodeFile="PDF-Editor.aspx.cs" Inherits="PDF_Editor" %>

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
        <radPdf:PdfWebControl id="PdfWebControl1" runat="server" height="600px" width="100%" />
    </div>
    </form>
</body>
</html>
