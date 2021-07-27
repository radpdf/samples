<%@ Page Language="VB" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="RadPdf" Namespace="RadPdf.Web.UI" TagPrefix="radPdf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <script type="text/javascript">
        function initRadPdf()
        {
            // Get id
            var id = "<%=PdfWebControl1.ClientID%>";

            // Get api instance
            var api = new PdfWebControlApi(id);

            // Attach listeners
            api.addEventListener(
                "objectClicked", 
                function(evt) {
                    // If this is our special signature object
                    if( "signature" == evt.obj.getProperties()["customData"] )
                    {
                        // Set the image via key
                        evt.obj.setImage("signature");
                    }
                }
                );
        }
    </script>

    <title>RAD PDF Sample</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <radPdf:PdfWebControlLite id="PdfWebControl1" runat="server" height="600px" width="100%" OnClientLoad="initRadPdf();" 
            HideToolsTabs="true" HideObjectPropertiesBar="true" HideBookmarks="true" HideThumbnails="true" />
    </div>
    </form>
</body>
</html>
