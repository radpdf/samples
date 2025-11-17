<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Custom-Interface.aspx.cs" CodeFile="Custom-Interface.aspx.cs" Inherits="Custom_Interface" %>

<%@ Register Assembly="RadPdf" Namespace="RadPdf.Web.UI" TagPrefix="radPdf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RAD PDF Sample</title>
    <script type="text/javascript">
        var api = null;
        var isDownload = false;
        
        function showViewInformation(view)
        {
            // Show page information
            document.getElementById("pageInformation").innerHTML = "Page " + view.page + " of " + api.getPageCount();
        }
        
        function initRadPdf()
        {
            // Get id
            var id = "<%=PdfWebControl1.ClientID%>";
            
            // Get api instance
            api = new PdfWebControlApi(id);
            
            // Attach listeners
            api.addEventListener(
                "viewChanged", 
                function(evt) {
                    // Show new view information
                    showViewInformation(evt.view);
                }
                );
            api.addEventListener(
                "printing", 
                function(evt) {
                    // To cancel printing, we would return false or set evt.cancel = true
                    return true;
                }
                );
            api.addEventListener(
                "saved",
                function(evt) {
                    // We don't want to show this prompt if we initiated a download
                    if (!isDownload) {
                        if (window.confirm("Save completed. Do PostBack?")) {
                            // Use ASP.NET's built in JavaScript function to initiate the PostBack
                            <%=Page.ClientScript.GetPostBackEventReference(PdfWebControl1, "")%>;
                        }
                    }
                }
                );
                
            // Initially show view information
            showViewInformation(api.getView());
        }

        // Additional Code Samples
        function fillOutForm()
        {
            if(api)
            {
                // Get text fields
                var fFirstName = api.getField("First Name");
                var fLastName = api.getField("Last Name");
                var fCreditCardNumber = api.getField("Credit Card Number");
                // Set text values (if the field was found)
                if( fFirstName )
                {
                    fFirstName.setProperties( { "value" : "John" } );
                }
                if( fLastName )
                {
                    fLastName.setProperties( { "value" : "Smith" } );
                }
                if( fCreditCardNumber )
                {
                    fCreditCardNumber.setProperties( { "value" : "5555555555554444" } );
                }
                
                // Get radio fields
                var fProductTypes = api.getFields("Product Type");
                // Set radio field
                if( fProductTypes )
                {
                    // Find and check field with the export value "1"
                    for( var i = 0; i < fProductTypes.length; i++ )
                    {
                        if( fProductTypes[i].getProperties()["exportValue"] == "1" )
                        {
                            fProductTypes[i].setProperties( { "checked" : true } );
                        }
                    }
                }
                
                // Get checkbox field
                var fProductSupport = api.getField("Product Support");
                // Set checkbox field
                if( fProductSupport )
                {
                    fProductSupport.setProperties( { "checked" : true } );
                }

                // Get combo field
                var fCardType = api.getField("Card Type");
                // Set combo field
                if( fCardType )
                {
                    fCardType.setProperties( { "value" : "MasterCard" } );
                }

                // Get list field
                var fProductOptions = api.getField("Product Options");
                // Set combo field
                if( fProductOptions )
                {
                    fProductOptions.setProperties( { "value" : "With PDF Extension" } );
                }
            }
        }
        function watermarkPages()
        {
            if(api)
            {
                //Loop through all pages
                for( var i = 1; i <= api.getPageCount(); i++ )
                {
                    //Get page
                    var p = api.getPage(i);

                    //Add watermark object
                    var obj = p.addObject(api.ObjectType.TextShape, 0, 50, p.getWidth(), 120);

                    //Set watermark properties
                    obj.setProperties(
                      {
                        "text" : "WATERMARK", 
                        "opacity" : 50, 
                        "font" : 
                          {
                            "name" : "Arial",
                            "alignment" : api.HorizontalAlignmentType.AlignCenter,
                            "color" : "#ff0000",
                            "size" : 110,
                            "bold" : true,
                            "italic" : false,
                            "underline" : false
                          },
                        "changeable" : false,
                        "deletable" : false,
                        "duplicatable" : false,
                        "hideFocusOutline" : true,
                        "movable" : false,
                        "resizable" : false,
                        "stylable" : false
                      } );
                }
            }
        }
        function zoomTo75()
        {
            if(api)
            {
                api.setView( { "zoom" : 75 } );
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="2" cellspacing="0" width="100%">
        <thead>
            <tr style="font-size:1.2em;">
                <td width="33%">File Operations</td>
                <td width="33%">Activate Tools</td>
                <td width="33%">Additional Samples</td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <button onclick="if(api){isDownload=false;api.save();}return false;">Save</button><br />
                    <button onclick="if(api){isDownload=true;api.download();}return false;">Download</button><br />
                    <button onclick="if(api){api.print();}return false;">Print</button><br />
                </td>
                <td>
                    <button onclick="if(api){api.setMode(api.Mode.InsertTextShape);}return false;">Text Tool</button><br />
                    <button onclick="if(api){api.setMode(api.Mode.SelectText);}return false;">Select Text Tool</button><br />
                    <button onclick="if(api){api.setMode(api.Mode.CropPage);}return false;">Crop Page Tool</button><br />
                </td>
                <td>
                    <button onclick="fillOutForm();return false;">Fill Out Form</button><br />
                    <button onclick="watermarkPages();return false;">Watermark Pages</button><br />
                    <button onclick="zoomTo75();return false;">Zoom to 75%</button><br />
                </td>
            </tr>
        </tbody>
    </table>
    <div style="text-align:center;">
        <asp:Label ID="PdfSize" RunAt="server" />
    </div>
    <div>
        <radPdf:PdfWebControl ID="PdfWebControl1" RunAt="server"
            Height="600px" 
            Width="100%" 
            OnClientLoad="initRadPdf();" 
            HideBottomBar="True"
            HideDownloadButton="True"
            HideObjectPropertiesBar="True"
            HideSearchText="True"
            HideSideBar="True"
            HideSidePanels="True"
            HideToolsTabs="True"
            HideTopBar="True"
            ViewerPageLayoutDefault="SinglePageContinuous"
            />
    </div>
    <div style="text-align:center;">
        <button onclick="if(api){api.setView( { 'page' : api.getView().page - 1 } );}return false;">Previous Page</button>
        <span id="pageInformation" style="padding:0px 5px;"></span>
        <button onclick="if(api){api.setView( { 'page' : api.getView().page + 1 } );}return false;">Next Page</button>
    </div>
    </form>
</body>
</html>

