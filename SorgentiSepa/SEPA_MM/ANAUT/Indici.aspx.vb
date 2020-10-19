
Partial Class ANAUT_Indici
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim IdDichiarazione As Long

            IdDichiarazione = CLng(Request.QueryString("ID"))

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select utenza_dichiarazioni.pg,UTENZA_DICHIARAZIONI.CHIAVE_ENTE_ESTERNO,utenza_dichiarazioni.ID_CAF,UTENZA_DICHIARAZIONI.ISEE,UTENZA_DICHIARAZIONI.ISE_ERP,UTENZA_DICHIARAZIONI.ISP_ERP,UTENZA_DICHIARAZIONI.ISR_ERP,UTENZA_DICHIARAZIONI.PSE,UTENZA_DICHIARAZIONI.VSE,UTENZA_DICHIARAZIONI.ANNO_SIT_ECONOMICA from utenza_dichiarazioni where utenza_dichiarazioni.id=" & IdDichiarazione
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label10.Text = par.IfNull(myReader("PG"), "")
                Label1.Text = par.IfNull(myReader("isee"), "")
                Label2.Text = par.IfNull(myReader("ise_ERP"), "")
                Label3.Text = par.IfNull(myReader("pse"), "")
                Label4.Text = par.IfNull(myReader("vse"), "")
                lblisr.Text = par.IfNull(myReader("isr_ERP"), "")
                lblisp.Text = par.IfNull(myReader("isp_ERP"), "")
                lblAnnoReddito.Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
            End If
            myReader.Close()
            Label8.Text = ""
            par.cmd.CommandText = "select * FROM utenza_legge_36 where id_dichiarazione=" & IdDichiarazione & " order by data_calcolo desc"
            myReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                Label11.Visible = True
                Label8.Text = "<table width='100%'><tR><td>Data</td><td>Isee</td><td>Ise</td><td>Isr</td><td>Isp</td><td>Art.18 f,g</td><td>U.I. Milano</td></tr>"
            End If
            While myReader.Read
                Label8.Text = Label8.Text & "<tR><td>" & par.FormattaData(Mid(myReader("data_calcolo"), 1, 8)) & "</td><td>" & par.Tronca(myReader("isee")) & "</td><td>" & par.Tronca(myReader("ise")) & "</td><td>" & par.Tronca(myReader("isr")) & "</td><td>" & par.Tronca(myReader("isp")) & "</td><td>"
                If myReader("art_18") = "-1" Then
                    Label8.Text = Label8.Text & " </td>"
                Else
                    If myReader("art_18") = "1" Then
                        Label8.Text = Label8.Text & "SI</td>"
                    Else
                        Label8.Text = Label8.Text & "NO</td>"
                    End If
                End If
                If myReader("fl_ui") = "0" Then
                    Label8.Text = Label8.Text & "<td>NO</td>"
                Else
                    Label8.Text = Label8.Text & "<td>SI</td>"
                End If
                Label8.Text = Label8.Text & "</tr>"
            End While
            myReader.Close()

            If Label8.Text <> "" Then
                Label8.Text = Label8.Text & "</Table>"
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try

    End Sub
End Class
