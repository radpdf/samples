<%@ Page Language="C#" CodeFile="Easy-Integration.aspx.cs" Inherits="Easy_Integration" %>

<%@ Register Assembly="RadPdf" Namespace="RadPdf.Web.UI" TagPrefix="radPdf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RAD PDF Sample</title>
    <script type="text/javascript">
    function SetCustomImageTool(key)
    {
        //Get ID
        var id = "<%= PdfWebControl1.ClientID%>";
        
        //Get api instance
        var api = new PdfWebControlApi(id);
        
        //Set image mode
        api.setMode(api.Mode.InsertImageShape, key);
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <radPdf:PdfWebControl id="PdfWebControl1" runat="server" height="600px" width="100%"
             OnSaved="Saved" />
    </div>
    <div>
        <a href="#" onclick="SetCustomImageTool('signature'); return false;">Add Signature Image From Server</a>
    </div>
    <div>
        <a href="#" onclick="SetCustomImageTool('dynamic'); return false;">Add Dynamic Date Image From Server</a>
    </div>
    </form>
</body>
</html>
