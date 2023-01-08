Public Class Form1

    Public Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cbuttons As Integer, ByVal dwExtraInfo As Integer)

    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short

    Dim shield_take_point As Point = New Point(My.Computer.Screen.Bounds.Width - 1365, My.Computer.Screen.Bounds.Height - 120)
    Dim catapult_take_point As Point = New Point(My.Computer.Screen.Bounds.Width - 1350, My.Computer.Screen.Bounds.Height - 102)
    Dim additional_key = Keys.XButton2

    Dim make_point As Point = New Point(My.Computer.Screen.Bounds.Width - 1365, My.Computer.Screen.Bounds.Height - 300)
    Dim take_point As Point = shield_take_point

    Dim activator As Boolean = False
    Dim gen As New Random

    Public Sub take_object()
        Cursor.Position = take_point + New Point(gen.Next(-5, 5), 0)
        System.Threading.Thread.Sleep(0.6)
        mouse_event(&H2, 0, 0, 0, 0)
        System.Threading.Thread.Sleep(0.6)
        mouse_event(&H4, 0, 0, 0, 0)
    End Sub
    Public Sub make_object()
        System.Threading.Thread.Sleep(0.6)
        mouse_event(&H2, 0, 0, 0, 0)
        System.Threading.Thread.Sleep(0.6)
        mouse_event(&H4, 0, 0, 0, 0)
    End Sub

    Public Sub make_move(up_point, down_point)
        Dim points As New List(Of PointF)

        Dim start_point As New PointF(CSng(up_point.X), CSng(up_point.Y))
        Dim end_point As New PointF(CSng(down_point.X), CSng(down_point.Y))

        Dim point_count As Integer = gen.Next(2, 4)

        For i As Integer = 0 To point_count - 1
            Dim t As Single = CSng(i) / CSng(point_count - 1)
            Dim x As Single = (1 - t) * start_point.X + t * end_point.X
            Dim y As Single = (1 - t) * start_point.Y + t * end_point.Y
            points.Add(New PointF(x, y))
        Next

        points.Reverse()
        For Each pointF In points
            System.Threading.Thread.Sleep(0.6)
            Dim point As New Point(CInt(pointF.X), CInt(pointF.Y))
            Cursor.Position = point
        Next

    End Sub
    Public Sub make_move_back(up_point, down_point)
        Dim points As New List(Of PointF)

        Dim start_point As New PointF(CSng(up_point.X), CSng(up_point.Y))
        Dim finish_point As New PointF(CSng(down_point.X), CSng(down_point.Y))
        Dim middle_point As New PointF(CSng(up_point.X / 2 + down_point.X / 2), CSng(up_point.Y / 2 + down_point.Y / 2))

        Dim point_count As Integer = gen.Next(2, 3)

        For i As Integer = 0 To point_count - 1
            Dim t As Single = CSng(i) / CSng(point_count - 1)
            Dim x As Single = (1 - t) * start_point.X + t * middle_point.X
            Dim y As Single = (1 - t) * start_point.Y + t * middle_point.Y
            points.Add(New PointF(x, y))
        Next

        For i As Integer = 0 To point_count - 1
            Dim t As Single = CSng(i) / CSng(point_count - 1)
            Dim x As Single = (1 - t) * middle_point.X + t * finish_point.X
            Dim y As Single = (1 - t) * middle_point.Y + t * finish_point.Y
            points.Add(New PointF(x, y))
        Next

        For Each pointF In points
            System.Threading.Thread.Sleep(0.6)
            Dim point As New Point(CInt(pointF.X), CInt(pointF.Y))
            Cursor.Position = point
        Next

    End Sub
    Public Sub mouse_move()
        take_object()
        Dim up_point As Point = make_point + New Point(gen.Next(-40, 40), gen.Next(-20, 20))
        Dim down_point As Point = shield_take_point + New Point(gen.Next(-10, 10), gen.Next(-10, 10))
        make_move(up_point, down_point)
        make_object()
        make_move_back(up_point, down_point)
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If GetAsyncKeyState(Keys.Delete Or additional_key) Then
            If activator = False Then
                activator = True
            Else
                activator = False
            End If
        End If

        If activator = True Then
            If GetAsyncKeyState(Keys.Up) OrElse GetAsyncKeyState(Keys.Down) OrElse GetAsyncKeyState(Keys.Right) OrElse GetAsyncKeyState(Keys.Left) Then
                mouse_move()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Enabled = True
    End Sub


End Class

