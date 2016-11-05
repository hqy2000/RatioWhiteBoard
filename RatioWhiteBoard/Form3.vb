Public Class Form3
    Sub BindingControlToToolTip(ByVal parent As Control)
        Dim toolTip1 As New ToolTip()
        toolTip1.AutoPopDelay = 5000 '显示停留5秒
        toolTip1.InitialDelay = 1000   '1秒后显示
        toolTip1.ReshowDelay = 500 '从一个控件移到另一个控件0.5秒后显示
        toolTip1.ShowAlways = True '在窗口不活动时也显示
        For Each ctrl As Control In parent.Controls
            toolTip1.SetToolTip(ctrl, ctrl.Tag)
            '递归调用过程，保证子窗体上（如panel）的空间也绑定。
            BindingControlToToolTip(ctrl)
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Dispose()
        Form2.Show()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MsgBox("预览功能，无实际作用", MsgBoxStyle.OkOnly, "提示")
        'For Each ctrl As Control In Parent.Controls
        'ToolTip1.SetToolTip(ctrl, ctrl.Tag)
        '递归调用过程，保证子窗体上（如panel）的空间也绑定。

        'BindingControlToToolTip(ctrl)
        'Next
    End Sub


End Class