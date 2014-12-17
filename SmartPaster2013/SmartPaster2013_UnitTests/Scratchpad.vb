
'我给你一本书 。
'This has a      tab and " quotes but this \t is not an escape


'Not a real VB class, just a file with BuildAction None and a vb extension so we can test SmartPaster
Public Class ScratchPad

    Public Function GetValue(ByVal id As Integer) As String
        Dim a = "'我给你一本书 。" & Environment.NewLine & _
"'This has a      tab and "" quotes but this \t is not an escape"
        Dim s As String =
<![CDATA[
'我给你一本书 。
'This has a      tab and " quotes but this \t is not an escape
]]>.Value
        ''我给你一本书 。
        ''This has a      tab and " quotes but this \t is not an escape
        Dim sb As New System.Text.StringBuilder(73)
        sb.AppendLine("'我给你一本书 。")
        sb.AppendLine("'This has a      tab and "" quotes but this \t is not an escape")


        Return "value"
    End Function

    Public Sub Void(ByVal value As String)

    End Sub

End Class