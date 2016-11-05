
Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Hide()
        Form2.Show()
        Me.TopMost = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
    End Sub

    Private Sub 关于软件ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 关于软件ToolStripMenuItem.Click
        MsgBox("电子白板For NFLS" & Chr(10) & "软件版本：V1.2" & Chr(10) & "作者：胡清阳" & Chr(10) & "保留所有权利", MsgBoxStyle.Information, "关于软件")
    End Sub


    Private Sub 退出ToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles 退出ToolStripMenuItem.Click
        Me.Dispose()
    End Sub

    Private Sub 开始ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 开始ToolStripMenuItem.Click
        Me.Hide()
        System.Threading.Thread.Sleep(750)
        Form2.Show()
        Me.TopMost = True
    End Sub

    Private Sub 检查更新ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 检查更新ToolStripMenuItem.Click
        MsgBox("本次更新：优化画点模式" & Chr(10) & "下次更新：新增模式（画线模式）", MsgBoxStyle.Information, "更新")
    End Sub
End Class
