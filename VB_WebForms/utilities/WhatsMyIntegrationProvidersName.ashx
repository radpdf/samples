<%@ WebHandler Language="VB" Class="WhatsMyIntegrationProvidersName" %>

Imports System
Imports System.Web

Public Class WhatsMyIntegrationProvidersName
	Implements IHttpHandler

	Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest

		' Change "CustomPdfIntegrationProvider" to the name (including any namespace changes) of your class
		Dim myType As Type = GetType(CustomPdfIntegrationProvider)

		context.Response.ContentType = "text/plain"

		context.Response.Write("The fully qualified name of my custom PdfIntegrationProvider class (" & myType.Name & ") is:" & vbLf)
		context.Response.Write(myType.AssemblyQualifiedName & vbLf)
		context.Response.Write(vbLf)
		context.Response.Write("This value can be used in your web.config file in the <appSettings> section like this:" & vbLf)
		context.Response.Write("<?xml version=""1.0""?>" & vbLf)
		context.Response.Write("<configuration>" & vbLf)
		context.Response.Write("    <appSettings>" & vbLf)
		context.Response.Write("        <add key=""RadPdfIntegrationProvider"" value=""" & myType.AssemblyQualifiedName & """/>" & vbLf)
		context.Response.Write("        ..." & vbLf)
		context.Response.Write("    </appSettings>" & vbLf)
		context.Response.Write("    ..." & vbLf)
		context.Response.Write("</configuration>" & vbLf)

	End Sub

	Public ReadOnly Property IsReusable As Boolean Implements IHttpHandler.IsReusable
		Get
			Return True
		End Get
	End Property
End Class
