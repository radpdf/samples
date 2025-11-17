<%@ Page Language="VB" AutoEventWireup="true" CodeBehind="Default.aspx.vb" CodeFile="Default.aspx.vb" Inherits="signatures_Default" %>

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

            // Function for finding object by customData
            function findObject(s)
            {
                for( var i = 1; i <= api.getPageCount(); i++ )
                {
                    var p = api.getPage(i);

                    for( var j = 0; j < p.getObjectCount(); j++ )
                    {
                        var o = p.getObject(j);

                        if( o.getProperties()["customData"] == s )
                        {
                            return o;
                        }
                    }
                }
            }

            // Attach listeners
            api.addEventListener(
                "objectChanged", 
                function(evt) {
                    // Get properties of changed object
                    var props = evt.obj.getProperties();

                    // If the popup signature was changed (or signed)
                    if( "MyPopupSignature" == props.customData )
                    {
                        // Change popup hint if signed
                        var msg = props.isSigned ? "thank you for signing!" : "click above to sign via popup"

                        // Update hint object
                        findObject("MyPopupHint").setProperties( { "text" : msg } );
                    }
                }
                );
            api.addEventListener(
                "objectClicked", 
                function(evt) {
                    // If this is our special image signature object
                    if( "MyImageSignature" == evt.obj.getProperties()["customData"] )
                    {
                        // Set the image via key
                        evt.obj.setImage("MyImageSignature");
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
        <radPdf:PdfWebControl id="PdfWebControl1" runat="server" height="600px" width="100%" OnClientLoad="initRadPdf();" 
            HideToolsTabs="true" HideObjectPropertiesBar="true" HideSidePanels="true" />
    </div>
    </form>
</body>
</html>
