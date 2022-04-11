<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" CodeFile="Default.aspx.cs" Inherits="offline_Default" %>

<%@ Register Assembly="RadPdf" Namespace="RadPdf.Web.UI" TagPrefix="radPdf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RAD PDF Sample</title>
    <script type="text/javascript">
    var rpApi,
        swActive;

    //Set PdfWebControl / PdfWebControlLite property OnClientLoad="onRadPdfLoad(this)"
    function onRadPdfLoad(api)
    {
        rpApi = (rpApi || api);

        if( !rpApi || !swActive )
        {
            return;
        }

        var a = rpApi.getOfflineURLs().toCache.slice();

        //Also cache this page (your implementation may do this differently)
        a.push(window.location.href);

        swActive.postMessage(
        {
            action : "cache-urls",
            urls : a
        });
    }

    if ("serviceWorker" in navigator)
    {
        navigator.serviceWorker.register("offline-service-worker.js?ver=<%=PdfWebControl.Version%>").then(function(reg)
        {
            var sw;

            if(reg.installing)
            {
                console.log("Service worker installing");
                sw = reg.installing;
            }
            else if(reg.waiting)
            {
                console.log("Service worker installed");
                sw = reg.waiting;
            }
            else if(reg.active)
            {
                console.log("Service worker active");
                swActive = reg.active;

                onRadPdfLoad();
            }

            if( sw )
            {
                sw.addEventListener("statechange", function(e)
                {
                    if( ("activated" == e.target.state) && !swActive )
                    {
                        swActive = sw;

                        onRadPdfLoad();
                    }
                });
            }

            navigator.serviceWorker.addEventListener("message", function(evt)
            {
                // if message sent from SW is notification of caching done
                console.log("Document cached for offline use!");
            });
        }).catch(function(error)
        {
            // registration failed
            console.log("Registration failed with " + error);
        });
    }
    else
    {
        console.log("Service workers are not supported! (You may be in private browsing mode)");
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <radPdf:PdfWebControl id="PdfWebControl1" runat="server" height="600px" width="100%" RenderAtClient="true" OnClientLoad="onRadPdfLoad(this);" />
    </div>
    </form>
</body>
</html>