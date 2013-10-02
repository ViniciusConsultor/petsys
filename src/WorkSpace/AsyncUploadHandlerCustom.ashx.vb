Imports System.Web
Imports System.Web.Services
Imports Telerik.Web.UI

Public Class AsyncUploadHandlerCustom
    Inherits AsyncUploadHandler

    Protected Overrides Function Process(file As UploadedFile, context As HttpContext, configuration As IAsyncUploadConfiguration, tempFileName As String) As IAsyncUploadResult
        configuration.TimeToLive = TimeSpan.FromHours(4)
        Return MyBase.Process(file, context, configuration, tempFileName)
    End Function

End Class