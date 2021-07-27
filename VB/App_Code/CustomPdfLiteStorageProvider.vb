Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Web

Imports RadPdf.Lite

' Not in use by default
' Uncomment the approprate line in CustomPdfIntegrationProvider.cs
Public Class CustomPdfLiteStorageProvider
    Inherits PdfLiteStorageProvider

    Private ReadOnly _dir As DirectoryInfo

    Public Sub New(ByVal path As String)

        _dir = New DirectoryInfo(path)

        If Not _dir.Exists Then

            _dir.Create()

        End If

    End Sub

    Private Function GetPath(ByVal session As PdfLiteSession, ByVal subtype As Integer) As String

        Return Path.Combine(_dir.FullName, session.ID.ToString("N") & "-" + subtype.ToString() & ".dat")

    End Function

    Public Overrides Sub DeleteData(ByVal session As PdfLiteSession)

        Dim files As FileInfo() = _dir.GetFiles(session.ID.ToString("N") & "*")

        For Each file As FileInfo In files

            SyncLock String.Intern(file.FullName)

                file.Delete()

            End SyncLock

        Next

    End Sub

    Public Overrides Function GetData(ByVal session As PdfLiteSession, ByVal subtype As Integer) As Byte()

        Dim path As String = GetPath(session, subtype)

        SyncLock String.Intern(path)

            If Not File.Exists(path) Then

                Return Nothing

            End If

            Return File.ReadAllBytes(path)

        End SyncLock

    End Function

    Public Overrides Sub SetData(ByVal session As PdfLiteSession, ByVal subtype As Integer, ByVal value As Byte())

        Dim path As String = GetPath(session, subtype)

        SyncLock String.Intern(path)

            File.WriteAllBytes(path, value)

        End SyncLock

    End Sub

End Class
