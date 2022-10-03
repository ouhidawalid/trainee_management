Public Class Form1
    Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\DB_Access\trainee_management.accdb")

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            con.Open()

            If con.State = ConnectionState.Open Then
                MsgBox("Connected")
            Else
                MsgBox("Not Connected!")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim i
            Dim sql As String
            Dim cmd As New OleDb.OleDbCommand
            Dim dt As New DataTable
            Dim da As New OleDb.OleDbDataAdapter
            Dim x, y As String
            x = TextBox1.Text
            y = TextBox2.Text
            con.Open()
            sql = "select id from admin where login like '" & x & "' and password like '" & y & "'"
            cmd.Connection = con
            cmd.CommandText = sql
            i = cmd.ExecuteScalar
            If i > 0 Then
                Me.Hide()
                Form2.Show()

            Else
                MsgBox("user not found")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub


End Class
