Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RicRptAmmCond
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaAmministratori()

        End If
    End Sub
    Private Sub CaricaAmministratori()
        '*******************APERURA CONNESSIONE*********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        par.cmd.CommandText = "select id, (cognome || ' ' || nome ) as amministratore from siscom_mi.cond_amministratori order by cognome asc"

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        Me.chkAmministratori.DataSource = dt
        Me.chkAmministratori.DataBind()

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    End Sub

    Protected Sub btnExportXls_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExportXls.Click
        Try
            Dim ammSelezionati As String = ""
            For Each i As ListItem In chkAmministratori.Items
                If i.Selected = True Then
                    ammSelezionati += i.Value & ","
                End If
            Next
            If ammSelezionati <> "" Then
                ammSelezionati = ammSelezionati.Substring(0, ammSelezionati.LastIndexOf(","))
                ammSelezionati = "(" & ammSelezionati & ")"
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "SELECT cognome,COND_AMMINISTRATORI.nome, " _
                                    & "(tipo_indirizzo ||' '||indirizzo||' '||civico||', '||COND_AMMINISTRATORI.CAP||' '||COMUNI_NAZIONI.NOME)AS indirizzo, " _
                                    & "tel_1, tel_2,cell,fax,email,note " _
                                    & "FROM siscom_mi.cond_amministratori, COMUNI_NAZIONI " _
                                    & "WHERE COMUNI_NAZIONI.COD(+) = COND_AMMINISTRATORI.COD_COMUNE " _
                                    & "AND COND_AMMINISTRATORI.ID IN " & ammSelezionati _
                                    & "ORDER BY cognome ASC"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)

                Me.dgvExport.Visible = True
                Me.dgvExport.DataSource = dt
                Me.dgvExport.DataBind()

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Esporta()
            Else
                Response.Write("<script>alert('Selezionare almeno un elemento della lista')</script>")
                Exit Sub
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: btnExportXls_Click " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub Esporta()
        Try
            Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(Me.dgvExport, "ExportAmministratori", , , "DATI ANAGRAFICI AMMINISTRATORI CONDOMINIALI", False)
            If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                Response.Redirect("..\/FileTemp/" & nomefile, False)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
            Me.dgvExport.Visible = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: btnExp " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnSeleziona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeleziona.Click
        If Selezionati.Value = 0 Then
            For Each i As ListItem In chkAmministratori.Items
                i.Selected = True
            Next
            Selezionati.Value = 1
        Else
            For Each i As ListItem In chkAmministratori.Items
                i.Selected = False
            Next
            Selezionati.Value = 0
        End If
    End Sub
End Class
