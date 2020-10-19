Imports Telerik.Web.UI

Partial Class MANUTENZIONI_RicercaAppalti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                Dim CTRL As Control
                For Each CTRL In Me.Form.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    End If
                Next
                Me.rdbType.SelectedValue = 0
                CaricaEsercizio()
                SettaggioCampi()
            Catch ex As Exception

            End Try
        End If
        NascondiCampi()
    End Sub
    Private Sub NascondiCampi()
        If Me.rdbType.SelectedValue = 1 Then
            lblStruttura.Visible = False
            cmbStruttura.Visible = False
            lblLotto.Visible = False
            cmblotto.Visible = False
        Else
            lblStruttura.Visible = True
            cmbStruttura.Visible = True
            lblLotto.Visible = True
            cmblotto.Visible = True
        End If
    End Sub
    Private Sub SettaggioCampi()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            'carico fornitori
            par.cmd.CommandText = "SELECT ID, COD_FORNITORE, RAGIONE_SOCIALE, COGNOME, NOME FROM SISCOM_MI.FORNITORI WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI) ORDER BY RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
            myReader1 = par.cmd.ExecuteReader()
            cmbfornitore.Items.Add(New RadComboBoxItem(" ", -1))
            While myReader1.Read
                If IsDBNull(myReader1("RAGIONE_SOCIALE")) Then
                    cmbfornitore.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("COD_FORNITORE"), " ") & " - " & par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
                Else
                    cmbfornitore.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("COD_FORNITORE"), " ") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
                End If
            End While
            cmbfornitore.SelectedValue = -1
            myReader1.Close()
            Dim ID_FILIALE As String = "-1"
            If Session.Item("LIVELLO") = 0 Then
                CaricaFiliali(-1)
            Else
                CaricaTutteFiliali()
            End If
            'CARICAMENTO DIR LAVORI
            par.caricaComboTelerik("SELECT ID,COGNOME ||' '|| NOME AS DESCRIZIONE FROM SEPA.OPERATORI WHERE FL_AUTORIZZAZIONE_ODL=1 and nvl(revoca,0)=0 and nvl(fl_eliminato,0)=0 and nvl(data_pw,'29991231')>'" & Format(Now, "yyyyMMdd") & "' AND ID_cAF=2 ORDER BY 2", cmbDirLavori, "ID", "DESCRIZIONE", True)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub
    Private Sub CaricaTutteFiliali()
        Me.cmbStruttura.Items.Clear()
        If Session.Item("BP_GENERALE") = 1 Then
            par.cmd.CommandText = "SELECT  TAB_FILIALI.ID, TAB_FILIALI.NOME || ' - ' || INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD ORDER BY TAB_FILIALI.NOME ASC"
        Else
            Dim QUERY As String = ""
            QUERY = "SELECT TAB_FILIALI.ID, TAB_FILIALI.NOME || ' - ' || INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD AND TAB_FILIALI.ID = " & par.IfEmpty(Session.Item("ID_STRUTTURA"), 0) & " ORDER BY TAB_FILIALI.NOME ASC"
            par.cmd.CommandText = QUERY
        End If
        par.caricaComboTelerik(par.cmd.CommandText, cmbStruttura, "ID", "INDIRIZZO", True)
        If Session.Item("BP_GENERALE") = 1 Then
            If Not IsNothing(cmbStruttura.Items.FindItemByValue(Session.Item("ID_STRUTTURA"))) Then
                Me.cmbStruttura.SelectedValue = Session.Item("ID_STRUTTURA")

            End If
        End If
        CaricaLotti()
    End Sub
    Private Sub CaricaLotti()
        Try
            If par.IfEmpty(Me.cmbStruttura.SelectedValue, "-1") <> "-1" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.SettaCommand(par)
                End If
                Dim condizioneEsercizio As String = ""
                If par.IfEmpty(Me.cmbesercizio.SelectedValue, "-1") <> "-1" Then
                    condizioneEsercizio = "  AND siscom_mi.lotti.id_esercizio_finanziario=" & cmbesercizio.SelectedValue & ""
                End If
                Dim stringa As String = "select ID, DESCRIZIONE from siscom_mi.lotti where tipo<>'X' and  LOTTI.ID_FILIALE = " & Me.cmbStruttura.SelectedValue & "" & condizioneEsercizio & " order by id asc"
                If Session.Item("BP_GENERALE") = 1 Then
                    par.caricaComboTelerik(stringa, cmblotto, "ID", "DESCRIZIONE", True)
                Else
                    par.caricaComboTelerik(stringa, cmblotto, "ID", "DESCRIZIONE", False)
                End If
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ElseIf par.IfEmpty(Me.cmbesercizio.SelectedValue, "-1") <> "-1" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "SELECT ID_TIPO_ST FROM SISCOM_MI.TAB_FILIALI WHERE ID = " & Session.Item("ID_STRUTTURA")
                Dim readTipoSt As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim QUERY As String = ""
                If readTipoSt.Read Then
                    Select Case readTipoSt("ID_TIPO_ST")
                        Case 0 'FILIALE AMMINISTRATIVA...CARICO SOLO I LOTTI DELLA SUA STRUTTURA
                            QUERY = "select * from siscom_mi.lotti where tipo<>'X' and LOTTI.ID_ESERCIZIO_FINANZIARIO = " & Me.cmbesercizio.SelectedValue & " AND ID_FILIALE = " & par.IfEmpty(Session.Item("ID_STRUTTURA"), 0) & " order by id asc"
                        Case 1 'FILIALE TECNICA...CARICO SOLO I LOTTI DELLA SUA STRUTTURA E QUELLA DELLE FIGLIE
                            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE  "
                            QUERY = "select * from siscom_mi.lotti where tipo<>'X' and LOTTI.ID_ESERCIZIO_FINANZIARIO = " & Me.cmbesercizio.SelectedValue & " AND ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TECNICA = " & par.IfEmpty(Session.Item("ID_STRUTTURA"), 0) & " )  order by id asc"
                        Case 2 'FILIALE UFFICIO CENTRALE...CARICO I LOTTI DI TUTTE LE STRUTTURE
                            QUERY = "select * from siscom_mi.lotti where tipo<>'X' and LOTTI.ID_ESERCIZIO_FINANZIARIO = " & Me.cmbesercizio.SelectedValue & " order by id asc"
                    End Select
                End If
                readTipoSt.Close()
                If QUERY <> "" Then
                    If Session.Item("BP_GENERALE") = 1 Then
                        'par.RiempiDListConVuoto(Me, par.OracleConn, "cmblotto", QUERY, "DESCRIZIONE", "ID")
                        par.caricaComboTelerik(QUERY, cmblotto, "ID", "DESCRIZIONE", True)
                    Else
                        'par.RiempiDList(Me, par.OracleConn, "cmblotto", QUERY, "DESCRIZIONE", "ID")
                        par.caricaComboTelerik(QUERY, cmblotto, "ID", "DESCRIZIONE", False)
                    End If
                End If
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                Me.cmblotto.Items.Clear()
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        Try
            Dim datadal As String = ""
            If Not IsNothing(txtdatadal.SelectedDate) Then
                datadal = txtdatadal.SelectedDate
            End If
            Dim dataal As String = ""
            If Not IsNothing(txtdataal.SelectedDate) Then
                dataal = txtdataal.SelectedDate
            End If
            Dim script As String = ""
            If Me.rdbType.SelectedValue = 0 Then
                script = "CIG=" & TextBoxCIG.Text & "&ST=" _
                               & Me.cmbStruttura.SelectedValue.ToString _
                               & "&FO=" & par.PulisciStrSql(cmbfornitore.SelectedValue) _
                               & "&NU=" & par.VaroleDaPassare(par.PulisciStrSql(Me.txtnumero.Text)) _
                               & "&LO=" & par.PulisciStrSql(cmblotto.SelectedValue) _
                               & "&DAL=" & par.IfEmpty(par.AggiustaData(datadal), "") _
                               & "&AL=" & par.IfEmpty(par.AggiustaData(dataal) & "&TIPO=P", "") _
                               & "&EF=" & Me.cmbesercizio.SelectedValue.ToString _
                               & "&DESC=" & txtDescrizione.Text _
                               & "&DL=" & cmbDirLavori.SelectedValue
                Else
                script = "CIG=" & TextBoxCIG.Text _
                               & "&FO=" & par.PulisciStrSql(cmbfornitore.SelectedValue) _
                               & "&NU=" & par.VaroleDaPassare(par.PulisciStrSql(Me.txtnumero.Text)) _
                               & "&DAL=" & par.IfEmpty(par.AggiustaData(datadal), "") _
                               & "&AL=" & par.IfEmpty(par.AggiustaData(dataal), "") _
                               & "&TIPO=N&EF=" & Me.cmbesercizio.SelectedValue.ToString _
                               & "&DESC=" & txtDescrizione.Text _
                               & "&DL=" & cmbDirLavori.SelectedValue

            End If
            Response.Redirect("RisultatiAppalti.aspx?" & script, False)
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub CaricaEsercizio()
        Try
            Dim i As Integer = 0
            Dim ID_ANNO_EF_CORRENTE As Long = -1
            '*****PEPPE MODIFY 30/09/2010*****
            '************APERTURA CONNESSIONE**********
            par.OracleConn.Open()
            par.SettaCommand(par)

            'par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE SISCOM_MI.PF_MAIN.ID_STATO=5 AND SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO"
            par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO, " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || ' (' || PF_STATI.descrizione || ')' as stato FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,siscom_mi.PF_STATI " _
                                & "WHERE id_stato > = 5 and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND id_stato = PF_STATI.ID order by T_ESERCIZIO_FINANZIARIO.ID desc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1

                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If
            End While
            myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbesercizio, "ID", "STATO", True)
            'If i = 1 Then
            '    Me.cmbesercizio.Items(0).Selected = True
            '    Me.cmbesercizio.Enabled = False
            'ElseIf i = 0 Then
            '    Me.cmbesercizio.Items.Clear()
            '    Me.cmbesercizio.Enabled = False
            'End If

            'If i > 0 Then
            '    If ID_ANNO_EF_CORRENTE <> -1 Then
            '        Me.cmbesercizio.SelectedValue = ID_ANNO_EF_CORRENTE
            '    End If
            'End If


            '************CHIUSURA CONNESSIONE**********
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub cmbStruttura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged
        CaricaLotti()
    End Sub

    Private Sub CaricaFiliali(ByVal filiale As String)
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            ''******PEPPE MODIFY 12/10/2010*****
            'Dim ID_FILIALE As String = "-1"
            'If Session.Item("LIVELLO") > 1 Then
            '    par.cmd.CommandText = "SELECT ID_UFFICIO FROM OPERATORI WHERE ID = " & Session.Item("ID_OPERATORE")
            '    myReader1 = par.cmd.ExecuteReader()
            '    If myReader1.Read Then
            '        ID_FILIALE = myReader1(0)
            '    End If
            '    myReader1.Close()
            'Else
            '    CaricaTutteFiliali()
            'End If
            If filiale <> "-1" Then
                par.cmd.CommandText = "SELECT ID_TIPO_ST, TAB_FILIALI.ID, TAB_FILIALI.NOME || ' - ' || INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD AND TAB_FILIALI.ID = " & filiale
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    'If myReader1("ID_TIPO_ST") = 2 Then
                    '    CaricaTutteFiliali()
                    'Else
                    par.caricaComboTelerik(par.cmd.CommandText, cmbStruttura, "ID", "DESCRIZIONE", True)
                    'Me.cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " ") & " - " & par.IfNull(myReader1("INDIRIZZO"), " "), par.IfNull(myReader1("ID"), -1)))
                    CaricaLotti()
                    'End If
                End If
                myReader1.Close()
            Else
                CaricaTutteFiliali()
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub cmbesercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbesercizio.SelectedIndexChanged
        CaricaFiliali("-1")
        CaricaLotti()
    End Sub
    Protected Sub btnRicarica_Click(sender As Object, e As System.EventArgs) Handles btnRicarica.Click
        Response.Redirect("RicercaAppalti.aspx", False)
    End Sub
End Class
