Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form2
    Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\DB_Access\trainee_management.accdb")
    Dim Path As String = "Null"
    Dim dt As New DataTable

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        con.Close()
        Me.Close()
        Form1.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim sql As String
            Dim cmd As New OleDb.OleDbCommand
            dt = New DataTable
            Dim da As New OleDb.OleDbDataAdapter
            con.Open()
            sql = "Select * from Trainee"
            cmd.Connection = con
            cmd.CommandText = sql
            da.SelectCommand = cmd
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()

        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim i
            Dim sql As String
            Dim cmd As New OleDb.OleDbCommand
            con.Open()
            sql = "INSERT INTO Trainee (IDNUMBER, FIRSTNAME, LASTNAME, PHONE, EMAIL, DEPARTMENT, SDI, EDI, PATHPICTURE) 
                    values ('" & TextBox1.Text & "', '" & TextBox2.Text & "','" & TextBox3.Text &
                    "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text &
                    "','" & TextBox7.Text & "','" & TextBox8.Text & "','" & Path & "');"
            cmd.Connection = con
            cmd.CommandText = sql
            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("New record has been inserted successfully!")
            Else
                MsgBox("No record has been inserted successfully!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()

        End Try

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim i
            Dim sql As String
            Dim cmd As New OleDb.OleDbCommand
            con.Open()
            sql = "UPDATE Trainee SET IDNUMBER='" & TextBox1.Text & "', FIRSTNAME='" & TextBox2.Text & "', LASTNAME='" & TextBox3.Text &
                    "', PHONE='" & TextBox4.Text & "', EMAIL='" & TextBox5.Text & "', DEPARTMENT='" & TextBox6.Text &
                    "', SDI='" & TextBox7.Text & "', EDI='" & TextBox8.Text & "'  WHERE ID=" & Val(Me.Text) & ""

            cmd.Connection = con
            cmd.CommandText = sql

            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("Record has been UPDATED successfully!")

            Else
                MsgBox("No record has been UPDATED!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()

        End Try

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Dim i
            Dim sql As String
            Dim cmd As New OleDb.OleDbCommand
            con.Open()
            sql = "Delete * from Trainee WHERE ID=" & Val(Me.Text) & ""
            cmd.Connection = con
            cmd.CommandText = sql

            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("Record has been deleted successfully!")

            Else
                MsgBox("No record has been deleted!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()

        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Me.Text = DataGridView1.CurrentRow.Cells(0).Value
        TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value
        TextBox2.Text = DataGridView1.CurrentRow.Cells(2).Value
        TextBox3.Text = DataGridView1.CurrentRow.Cells(3).Value
        TextBox4.Text = DataGridView1.CurrentRow.Cells(4).Value
        TextBox5.Text = DataGridView1.CurrentRow.Cells(5).Value
        TextBox6.Text = DataGridView1.CurrentRow.Cells(6).Value
        TextBox7.Text = DataGridView1.CurrentRow.Cells(7).Value
        TextBox8.Text = DataGridView1.CurrentRow.Cells(8).Value
        If Not (DataGridView1.CurrentRow.Cells(9).Value.ToString.Contains("Null")) Then
            PictureBox1.Image = Image.FromFile(DataGridView1.CurrentRow.Cells(9).Value)
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        End If



    End Sub


    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        Dim dv As DataView
        dv = dt.DefaultView
        dv.RowFilter = " IDNUMBER LIKE '%" & TextBox9.Text & "%'"
        DataGridView1.DataSource = dv


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim openFileDialog1 As OpenFileDialog
        openFileDialog1 = New OpenFileDialog
        openFileDialog1.InitialDirectory = "C:/Users/adil-/Pictures"
        openFileDialog1.Title = "Select image to be upload."
        openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png"
        openFileDialog1.FilterIndex = 1


        Try
            If (openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
                If (openFileDialog1.CheckFileExists) Then
                    Path = System.IO.Path.GetFullPath(openFileDialog1.FileName)
                    '                    Label1.Text = Path
                    PictureBox1.Image = New Bitmap(openFileDialog1.FileName)
                    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage

                End If
            Else
                MessageBox.Show("Please Upload image.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try




    End Sub
End Class