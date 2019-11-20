Imports System.Data.Odbc
Public Class Form1
    Sub TampilGrid()
        bukakoneksi()

        DA = New OdbcDataAdapter("SELECT * FROM tbl_mahasiswa", CONN)
        DS = New DataSet
        DA.Fill(DS, "tbl_mahasiswa")
        DataGridView1.DataSource = DS.Tables("tbl_mahasiswa")

        tutupkoneksi()
    End Sub
    'combobox
    Sub MunculCombo()
        ComboBox1.Items.Add("Ilmu Komputer")
        ComboBox1.Items.Add("Kimia")
        ComboBox1.Items.Add("Fisika")
        ComboBox1.Items.Add("Matematika")
    End Sub

    'kosongkan data
    Sub KosongkanData()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TampilGrid()
        MunculCombo()
        KosongkanData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Silahkan Isi Semua Form")
        Else
            bukakoneksi()
            Dim simpan As String = "Insert Into tbl_mahasiswa Values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & ComboBox1.Text & "')"

            CMD = New OdbcCommand(simpan, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Input Data Berhasil")
            TampilGrid()
            KosongkanData()

            tutupkoneksi()
        End If
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        TextBox1.MaxLength = 6
        If e.KeyChar = Chr(13) Then
            bukakoneksi()
            CMD = New OdbcCommand("select *  From tbl_mahasiswa where nim_mhs='" & TextBox1.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("Nim Tidak Ada, silahkan coba lagi")
                TextBox1.Focus()
            Else
                TextBox2.Text = RD.Item("nama_mhs")
                TextBox3.Text = RD.Item("alamat_mhs")
                TextBox4.Text = RD.Item("telepon_mhs")
                ComboBox1.Text = RD.Item("jurusan_mhs")
                TextBox2.Focus()
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        bukakoneksi()
        Dim edit As String = "Update tbl_mahasiswa set
        nama_mhs='" & TextBox2.Text & "'
        alamat_mhs='" & TextBox3.Text & "'
        telepon_mhs='" & TextBox4.Text & "'
        jurusan_mhs='" & ComboBox1.Text & "' where nim_mhs='" & TextBox1.Text & "'"

        CMD = New OdbcCommand(edit, CONN)
        CMD.ExecuteNonQuery()
        MsgBox("Data Berhasil di Update")
        TampilGrid()
        KosongkanData()
        tutupkoneksi()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("Silahkan Pilih data yang akan dihapus dengan masukkan NIM dan eneter")
        Else
            If MessageBox.Show("Yakin akan dihapus..?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                bukakoneksi()
                Dim hapus As String = "Delete FROM tbl_mahasiswa where nim_mhs='" & TextBox1.Text & "'"
                CMD = New OdbcCommand(hapus, CONN)
                CMD.ExecuteNonQuery()
                TampilGrid()
                KosongkanData()
                tutupkoneksi()

            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class
