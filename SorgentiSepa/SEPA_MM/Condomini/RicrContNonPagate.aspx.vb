Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RicrContNonPagate
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.txtAnnoFine.Attributes.Add("onkeyup", "javascript:valid(this,'onlynumbers');")
            Me.txtAnnoInizio.Attributes.Add("onkeyup", "javascript:valid(this,'onlynumbers');")
            CaricaAmministratori()
            CaricaCondomini()

        End If



    End Sub
    Private Sub CaricaAmministratori()
        Try


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
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza: CaricaAmministratori " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub
    Private Sub CaricaCondomini()
        Try


            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select id, denominazione from siscom_mi.condomini order by denominazione asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.chkCondomini.DataSource = dt
            Me.chkCondomini.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
            Session.Add("ERRORE", "Provenienza: CaricaCondomini " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub
    Private Sub CaricaCheckAmministratori()
        Try
            chkCondomini.Items.Clear()
            Dim StringaCheckAmministratori As String = ""
            For Each Items As ListItem In chkAmministratori.Items
                If Items.Selected = True Then
                    StringaCheckAmministratori = StringaCheckAmministratori & Items.Value & ","
                End If
            Next
            If StringaCheckAmministratori <> "" Then
                StringaCheckAmministratori = Left(StringaCheckAmministratori, Len(StringaCheckAmministratori) - 1)
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select id, denominazione from siscom_mi.condomini " _
                                    & "WHERE SISCOM_MI.CONDOMINI.ID IN (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & StringaCheckAmministratori & ") and siscom_mi.cond_amministrazione.DATA_FINE is null) " _
                                    & "order by denominazione asc"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                Me.chkCondomini.DataSource = dt
                Me.chkCondomini.DataBind()
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                CaricaAmministratori()
                CaricaCondomini()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: CaricaCheckAmministratori " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnSelAmm_Click(sender As Object, e As System.EventArgs) Handles btnSelAmm.Click
        If SelAmminist.Value = 0 Then
            For Each i As ListItem In chkAmministratori.Items
                i.Selected = True
            Next
            SelAmminist.Value = 1
        Else
            For Each i As ListItem In chkAmministratori.Items
                i.Selected = False
            Next
            SelAmminist.Value = 0
        End If
        CaricaCheckAmministratori()

    End Sub

    Protected Sub btnSelCondomini_Click(sender As Object, e As System.EventArgs) Handles btnSelCondomini.Click
        If SelCondomini.Value = 0 Then
            For Each i As ListItem In chkCondomini.Items
                i.Selected = True
            Next
            SelCondomini.Value = 1
        Else
            For Each i As ListItem In chkCondomini.Items
                i.Selected = False
            Next
            SelCondomini.Value = 0
        End If
    End Sub

    Protected Sub chkAmministratori_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles chkAmministratori.SelectedIndexChanged
        CaricaCheckAmministratori()

    End Sub

    Protected Sub btnExportXls_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExportXls.Click
        Try
            Dim ammSelezionati As String = ""
            Dim CondSelezionati As String = ""
            Dim condizione As String = ""
            For Each i As ListItem In chkAmministratori.Items
                If i.Selected = True Then
                    ammSelezionati += i.Value & ","
                End If
            Next
            For Each i As ListItem In chkCondomini.Items
                If i.Selected = True Then
                    CondSelezionati += i.Value & ","
                End If
            Next
            If ammSelezionati = "" And CondSelezionati = "" Then
                Response.Write("<script>alert('Selezionare almeno un criterio(Amministratore/Condomini) per effettuare il report')</script>")
                Exit Sub
            End If
            If Not String.IsNullOrEmpty(Me.txtAnnoInizio.Text) And Not String.IsNullOrEmpty(Me.txtAnnoFine.Text) Then
                If Me.txtAnnoFine.Text < Me.txtAnnoInizio.Text Then
                    Response.Write("<script>alert('L\'anno di gestione finale non può essere inferiore a quello iniziale!')</script>")
                    Exit Sub

                End If
            End If

            If Not String.IsNullOrEmpty(Me.txtAnnoInizio.Text) Then
                condizione = condizione & " AND SUBSTR(COND_ORD_GIORNO.DATA_INIZIO,1,4) >=" & Me.txtAnnoInizio.Text
            End If
            If Not String.IsNullOrEmpty(Me.txtAnnoFine.Text) Then
                condizione = condizione & " AND SUBSTR(COND_ORD_GIORNO.DATA_FINE,1,4) <=" & Me.txtAnnoFine.Text
            End If
            If Me.chkSoloUltima.Checked = True Then
                condizione = condizione & " AND COND_ORD_GIORNO.ID IN (SELECT MAX(ID) FROM SISCOM_MI.COND_ORD_GIORNO B WHERE B.ID_CONVOCAZIONE=COND_ORD_GIORNO.ID_CONVOCAZIONE) "
            End If

            If ammSelezionati <> "" Then
                ammSelezionati = ammSelezionati.Substring(0, ammSelezionati.LastIndexOf(","))
                ammSelezionati = " AND COND_AMMINISTRATORI.ID IN  (" & ammSelezionati & ") "
            End If
            If CondSelezionati <> "" Then
                CondSelezionati = CondSelezionati.Substring(0, CondSelezionati.LastIndexOf(","))
                CondSelezionati = " AND CONDOMINI.ID IN (" & CondSelezionati & ") "
            End If
            '*******************APERURA CONNESSIONE*********************182
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT CONDOMINI.denominazione as condominio,descrizione as ORDINE_DEL_GIORNO,COND_ORD_GIORNO.anno, " _
                                & "(CASE WHEN COND_ORD_GIORNO.si_no=1 THEN 'SI' ELSE 'NO' END) AS APPROVATO,COND_ORD_GIORNO.NOTE , " _
                                & "(COND_AMMINISTRATORI.cognome||' '||COND_AMMINISTRATORI.nome) as AMMINISTRATORE " _
                                & "FROM siscom_mi.COND_ORD_GIORNO " _
                                & ",siscom_mi.TIPO_ORD_GIORNO " _
                                & ",siscom_mi.COND_CONVOCAZIONI " _
                                & ",siscom_mi.CONDOMINI " _
                                & ",siscom_mi.COND_AMMINISTRATORI " _
                                & "WHERE " _
                                & "cod_ordine = 1 " _
                                & "AND cod_ordine = TIPO_ORD_GIORNO.ID " _
                                & "AND COND_CONVOCAZIONI.ID = id_convocazione " _
                                & "AND COND_CONVOCAZIONI.id_condominio = CONDOMINI.ID " _
                                & "AND COND_AMMINISTRATORI.ID = COND_CONVOCAZIONI.id_amministratore " _
                                & CondSelezionati & ammSelezionati & condizione _
                                & "AND CONDOMINI.ID NOT IN (SELECT ID_CONDOMINIO FROM SISCOM_MI.COND_GESTIONE " _
                                & "WHERE ID_CONDOMINIO = CONDOMINI.ID AND COND_GESTIONE.DATA_INIZIO=COND_ORD_GIORNO.DATA_INIZIO AND COND_GESTIONE.DATA_FINE = COND_ORD_GIORNO.DATA_FINE)"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Esporta(dt)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: CaricaIndirizzi " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Try
            Dim nomefile As String = par.EsportaExcelDaDT(dt, "ExportCondomini", False, False)
            If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                Response.Redirect("..\/FileTemp/" & nomefile, False)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: btnExp " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
