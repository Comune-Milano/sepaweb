
Partial Class ANAUT_pagina_home
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        Label1.Text = System.Configuration.ConfigurationManager.AppSettings("Versione")
        If Not IsPostBack Then
            Session.LCID = 1040
            'GiorniAp = ""
            'nGiorno = Format(Now, "dddd")
            'Select Case LCase(nGiorno)
            '    Case "lunedì", "monday"
            '        nGiorno = "1"
            '    Case "martedì", "tuesday"
            '        nGiorno = "2"
            '    Case "mercoledì", "wednesday"
            '        nGiorno = "3"
            '    Case "giovedì", "thursday"
            '        nGiorno = "4"
            '    Case "venerdì", "friday"
            '        nGiorno = "5"
            '    Case "sabato", "saturday"
            '        nGiorno = "6"
            '    Case "domenica", "sunday"
            '        nGiorno = "7"
            'End Select
            'nGiornoRif = System.Configuration.ConfigurationManager.AppSettings("Giorni")

            'If InStr(nGiornoRif, nGiorno) = 0 Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            '    Exit Sub
            'End If

            'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) < Val(System.Configuration.ConfigurationManager.AppSettings("OraAp") & "00") Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            '    Exit Sub
            'End If

            'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) > Val(System.Configuration.ConfigurationManager.AppSettings("OraCh") & "00") Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            '    Exit Sub
            'End If
            Label3.Text = Session.Item("ORARIO")

            If Session.Item("MOD_CONDOMINIO_SL") <> 1 Then
                CaricaAvvisi()
                PagamentiInScadenza()

            Else
                Me.imgPagScad.Visible = False
                Me.imgNuoviEventi.Visible = False
                Me.imgEventMorToPrint.Visible = False
            End If

            If Session.Item("LIVELLO") <> 1 Then
                If Session.Item("ID_CAF") = 6 Then        'id_caf = 6 per operatori del comune
                    Me.imgNuoviEventi.Visible = False
                    Me.imgPagScad.Visible = False
                End If
            End If

            If Session.Item("LIVELLO") = 1 Or Session.Item("ID_CAF") = 6 Then
                CercaMorDaStampare()
            Else
                Me.imgEventMorToPrint.Visible = False
            End If

        End If
    End Sub
    Private Sub CaricaAvvisi()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "select cond_avvisi.ID_condominio from siscom_mi.cond_avvisi where visto = 0"
            'par.cmd.CommandText = "SELECT condomini.ID as id_condominio,condomini.denominazione as condominio, cond_avvisi.id_ui,cond_avvisi.evento, cond_avvisi.DATA, cod_unita_immobiliare AS COD_UI FROM siscom_mi.condomini, siscom_mi.cond_ui, siscom_mi.cond_avvisi, siscom_mi.unita_immobiliari WHERE COND_AVVISI.visto = 0 and cond_avvisi.id_ui=cond_ui.id_ui AND cond_ui.id_condominio = condomini.ID AND cond_ui.id_ui = unita_immobiliari.ID ORDER BY DATA DESC "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.imgNuoviEventi.Visible = True
                Me.imgNuoviEventi.Attributes.Add("onclick", "ApriEventiPat();")
            Else
                Me.imgNuoviEventi.Visible = False
            End If

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.imgNuoviEventi.Visible = False

        End Try
    End Sub
    Private Sub PagamentiInScadenza()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            Dim giorni As Integer = 0
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO = 'ALLARME_SCADENZA_COND'"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                giorni = myReader1(0)
            End If
            myReader1.Close()


            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PRENOTAZIONI WHERE NVL(TO_DATE(DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= " & giorni _
                                & " and tipo_pagamento = 1 and id_pagamento is null AND (ID_VOCE_PF IS NOT NULL OR ID_VOCE_PF_IMPORTO IS NOT NULL )"


            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.imgPagScad.Visible = True
                Me.imgPagScad.Attributes.Add("onclick", "ApriPagamenti();")
            Else
                Me.imgPagScad.Visible = False
            End If
            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Private Sub CercaMorDaStampare()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            '*******QUERY UN Pò PIù LEGGERA
            par.cmd.CommandText = "select distinct id_morosita from siscom_mi.cond_morosita_inquilini,siscom_mi.cond_morosita where cond_morosita.id = cond_morosita_inquilini.id_morosita and fl_completo = 1 and id_intestatario not in (select id_anagrafica from siscom_mi.cond_morosita_lettere)"

            '*******QUERY CHE NEL TEMPO DIVENTERà PESANTE
            'par.cmd.CommandText = "select condomini.id from siscom_mi.condomini, siscom_mi.cond_morosita where cond_morosita.id_condominio = condomini.id and cond_morosita.fl_completo = 1 and cond_morosita.id not in (SELECT id_morosita FROM siscom_mi.cond_morosita_lettere WHERE id_anagrafica NOT IN (SELECT id_intestatario FROM siscom_mi.cond_morosita_inquilini WHERE cond_morosita_inquilini.id_morosita = id_morosita))"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.imgEventMorToPrint.Visible = True
                Me.imgEventMorToPrint.Attributes.Add("onclick", "ApriMorositaDaStampare();")
            Else
                Me.imgEventMorToPrint.Visible = False
            End If
            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub
End Class
