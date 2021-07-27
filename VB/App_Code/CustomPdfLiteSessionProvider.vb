Option Explicit On
Option Strict On

Imports System
#If NET40 Then
Imports System.Collections.Concurrent
#End If
Imports System.Collections.Generic
Imports System.Web

Imports RadPdf.Lite

' Not in use by default
' Uncomment the approprate line in CustomPdfIntegrationProvider.cs
Public Class CustomPdfLiteSessionProvider
    Inherits PdfLiteSessionProvider

    ' This example uses an in memory dictionary, which won't have  
    ' persistent storage, but a database or other key /value store 
    ' can easily be substituted. 
#If NET40 Then
    Private ReadOnly _dict As ConcurrentDictionary(Of String, Byte())
#Else
    Private ReadOnly _dict As Dictionary(Of String, Byte())
#End If

    Public Sub New()
#If NET40 Then
        _dict = New ConcurrentDictionary(Of String, Byte())()
#Else
        _dict = New Dictionary(Of String, Byte())()
#End If
    End Sub

    Public Overrides Function AddSession(ByVal session As PdfLiteSession) As String

        Dim key As String = GenerateKey()

#If Not NET40 Then
        SyncLock _dict
#End If
            _dict(key) = session.Serialize()
#If Not NET40 Then
        End SyncLock
#End If

        Return key

    End Function

    Public Overrides Function GetSession(ByVal key As String) As PdfLiteSession

        Dim data As Byte()

#If Not NET40 Then
        SyncLock _dict
#End If
            data = _dict(key)
#If Not NET40 Then
        End SyncLock
#End If

        If data Is Nothing Then
            Return Nothing
        End If

        Return PdfLiteSession.Deserialize(data)

    End Function

End Class
