Public Class Form2
    Declare Function SelectObject Lib "gdi32" (ByVal hdc As Integer, ByVal hObject As Integer) As Integer
    Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hSrcDC As Integer, ByVal xSrc As Integer, ByVal ySrc As Integer, ByVal dwRop As Integer) As Integer
    Declare Function CreateCompatibleBitmap Lib "gdi32" (ByVal hdc As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer
    Declare Function CreateDC Lib "gdi32" Alias "CreateDCA" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As String, ByRef lpInitData As Integer) As Integer
    Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hdc As Integer) As Integer
    Public st As Integer
    Public radio As Integer
    Public ScreenPen As New Pen(Color.Red, 1)
    Public Last_x As Integer
    Public Last_y As Integer
    Public Draw_Mode As String
    Public pic As Image
    Public g As Graphics
    Function GetSerPic(Optional ByVal BitWidth As Integer = -1, Optional ByVal BitHeight As Integer = -1) As Image
        If BitWidth < 0 Then BitWidth = My.Computer.Screen.Bounds.Width
        If BitHeight < 0 Then BitHeight = My.Computer.Screen.Bounds.Height
        Dim Bhandle, DestDC, SourceDC As IntPtr
        SourceDC = CreateDC("DISPLAY", Nothing, Nothing, 0)
        DestDC = CreateCompatibleDC(SourceDC)
        Bhandle = CreateCompatibleBitmap(SourceDC, BitWidth, BitHeight)
        SelectObject(DestDC, Bhandle)
        BitBlt(DestDC, 0, 0, BitWidth, BitHeight, SourceDC, 0, 0, &HCC0020)
        Return Image.FromHbitmap(Bhandle)
    End Function

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GroupBox1.Top = Screen.PrimaryScreen.Bounds.Height - 112
        g = Me.CreateGraphics '画板
        Draw_Mode = "pen"
        RadioButton1.Checked = True
        GroupBox1.BackColor = Color.Transparent
        System.Threading.Thread.Sleep(1000)
        pic = GetSerPic()
        Me.Height = Screen.PrimaryScreen.Bounds.Height
        Me.Width = Screen.PrimaryScreen.Bounds.Width
        Me.WindowState = FormWindowState.Maximized
        'MsgBox(Me.Width)
        Form1.TopMost = False
        Me.Hide()
        Dim p1 As New Point(0, 0)
        Dim p2 As New Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        Dim pic1 As New Bitmap(p2.X, p2.Y)
        Using g As Graphics = Graphics.FromImage(pic1)
            g.CopyFromScreen(p1, p1, p2)
            Me.BackgroundImage = pic1
        End Using
        Me.Show()
        NumericUpDown1.Value = 2

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Dispose()
    End Sub



    Private Sub Form2_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove, MyBase.MouseClick
        Dim pos_x = System.Windows.Forms.Cursor.Position.X
        Dim pos_y = System.Windows.Forms.Cursor.Position.Y
        Dim x As Integer
        x = Screen.PrimaryScreen.Bounds.Width / 2 + (pos_x - Screen.PrimaryScreen.Bounds.Width / 2) * 4 / 3
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Select Case Draw_Mode
                Case "pen"
                    ' g.DrawEllipse(mypen, pos_x, pos_y, radio, radio)
                    g.DrawEllipse(ScreenPen, x, pos_y, radio, radio) '画圆，只要笔宽、椭圆高宽都相同，就是点了吧？
                    g.DrawEllipse(ScreenPen, x - 1, pos_y, radio, radio)
                Case "eraser"
                    Dim instance As Rectangle
                    instance.X = x
                    instance.Y = pos_y
                    instance.Width = radio
                    instance.Height = radio
                    Dim instance1 As GraphicsUnit
                    instance1 = GraphicsUnit.Pixel
                    g.DrawImage(pic, instance, x, pos_y, radio, radio, instance1)
            End Select

        End If

    End Sub


    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        radio = NumericUpDown1.Value
        If (radio < 1) Then
            MsgBox("笔粗不能低于1！", MsgBoxStyle.Exclamation, "警告")
            NumericUpDown1.Value = 1
        End If
        ScreenPen.Width = radio
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Hide()
        System.Threading.Thread.Sleep(1000)
        Dim p1 As New Point(0, 0)
        Dim p2 As New Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        Dim pic As New Bitmap(p2.X, p2.Y)
        Using g As Graphics = Graphics.FromImage(pic)
            g.CopyFromScreen(p1, p1, p2)
            Me.BackgroundImage = pic
        End Using
        Me.Show()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Color
        Color = ColorDialog1.ShowDialog()
        ScreenPen.Color = ColorDialog1.Color
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged, RadioButton3.CheckedChanged
        If RadioButton1.Checked = True Or RadioButton3.Checked = True Then
            Button3.Enabled = True
            Draw_Mode = "pen"
            Me.Cursor = Cursors.Cross
            Me.GroupBox1.Cursor = Cursors.Arrow

        Else
            Button3.Enabled = False
            Draw_Mode = "eraser"
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Hide()
        Form3.Show()
    End Sub
End Class