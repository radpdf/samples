<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

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
        <asp:Panel ID="UploadPanel" Runat="Server" HorizontalAlign="Center">
            <asp:Label ID="Label1" Text="Choose File:" Runat="Server" AssociatedControlID="UploadControl" />
            <asp:FileUpload ID="UploadControl" Runat="Server" />
            <asp:Button ID="UploadButton" Runat="Server" Text="Upload" />
            <div>
                <asp:Literal ID="UploadMessage" Runat="Server" Text="Please choose a file up to 10 MB. The first 100 pages will be loaded." />
            </div>
        </asp:Panel>

        <radPdf:PdfWebControl ID="PdfWebControl1" runat="server" height="600px" width="100%" MaxPdfPages="100" ThrowMaxPagesException="false" />
    </div>
    </form>
</body>
</html>