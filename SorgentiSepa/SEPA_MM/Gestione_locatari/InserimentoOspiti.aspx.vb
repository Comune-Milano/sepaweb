Imports Telerik.Web.UI

Partial Class Gestione_locatari_InserimentoOspiti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then

            idComp.Value = Request.QueryString("C")
            iddich.Value = Request.QueryString("IDD")
            HFBtnToClick.Value = Request.QueryString("BTN").ToString
            operazione.Value = Request.QueryString("O")
            idTipologia.Value = Request.QueryString("TOS")
            idMotivoIstanza.Value = Request.QueryString("IDM")
            idContratto.Value = Request.QueryString("IDC")

            par.caricaComboTelerik("select * from t_tipo_indirizzo order by cod asc", cmbTipoVia, "COD", "DESCRIZIONE", True, "null")

            If idMotivoIstanza.Value = "7" Then
                txtDataFine.Enabled = False
                lblTitolo.Text = "Ospiti"
                par.caricaComboTelerik("select * from T_TIPO_RUOLO order by id asc", cmbRuolo, "ID", "DESCRIZIONE", False)

            Else
                lblTitolo.Text = "Coabitanti"
                par.caricaComboTelerik("select * from T_TIPO_RUOLO order by id desc", cmbRuolo, "ID", "DESCRIZIONE", False)

            End If

            If idMotivoIstanza.Value = "7" Then
                If idTipologia.Value = "1" Then
                    par.caricaComboTelerik("select * from t_tipo_parentela where cod<>'8' and cod<>'17' and cod<>'24' and cod<>'26' and cod<>'28' and cod<>'30' order by cod asc", cmbParenti, "COD", "DESCRIZIONE", True)
                Else
                    par.caricaComboTelerik("select * from t_tipo_parentela where cod in (3,4) order by cod asc", cmbParenti, "COD", "DESCRIZIONE", True)
                End If
            Else
                par.caricaComboTelerik("select * from t_tipo_parentela where cod<>'8' and cod<>'17' and cod<>'24' and cod<>'26' and cod<>'28' and cod<>'30' order by cod asc", cmbParenti, "COD", "DESCRIZIONE", True)
                lblDataFine.Visible = False
                txtDataFine.Visible = False
            End If
            If Not String.IsNullOrEmpty(idComp.Value.ToString) Then
                VisualizzaDatiOspite()
            End If
        End If

    End Sub

    Private Sub VisualizzaDatiOspite()
        Try
            Dim ApertaNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                ApertaNow = True
            End If

            par.cmd.CommandText = "select comp_nucleo_ospiti_vsa.*,cognome,nome,data_nasc,cod_fiscale,DATA_INIZIO_OSPITE,DATA_FINE_OSPITE from comp_nucleo_ospiti_vsa,domande_bando_vsa where " _
                    & "domande_bando_vsa.id_dichiarazione=" & iddich.Value & " and domande_bando_vsa.id = comp_nucleo_ospiti_vsa.id_domanda and comp_nucleo_ospiti_vsa.id=" & idComp.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            If lettore.Read Then
                txtCognome.Text = par.IfNull(lettore("COGNOME"), "")
                txtNome.Text = par.IfNull(lettore("NOME"), "")
                txtData.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_NASC"), ""))
                txtCF.Text = par.IfNull(lettore("COD_FISCALE"), "")
                If par.IfNull(lettore("DATA_INIZIO_OSPITE"), "") <> "" Then
                    txtDataInizio.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_INIZIO_OSPITE"), ""))
                End If
                If par.IfNull(lettore("DATA_FINE_OSPITE"), "") <> "" Then
                    txtDataFine.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_FINE_OSPITE"), ""))
                End If
                txtComune.Text = par.IfNull(lettore("COMUNE_RES_DNTE"), "")
                cmbTipoVia.SelectedValue = par.IfNull(lettore("ID_TIPO_IND_RES_DNTE"), "")
                txtVia.Text = par.IfNull(lettore("IND_RES_DNTE"), "")
                txtCivico.Text = par.IfNull(lettore("CIVICO_RES_DNTE"), "")
                txtCap.Text = par.IfNull(lettore("CAP_RES_DNTE"), "")
                cmbParenti.SelectedValue = par.IfNull(lettore("GRADO_PARENTELA"), 1)
                If idMotivoIstanza.Value = "7" Then

                    If par.IfNull(lettore("ID_TIPO_RUOLO"), 1) = 2 Then
                        cmbRuolo.ClearSelection()
                        cmbRuolo.SelectedValue = par.IfNull(lettore("ID_TIPO_RUOLO"), 1)
                        cmbRuolo.Enabled = False
                    End If
                Else
                    If par.IfNull(lettore("ID_TIPO_RUOLO"), 1) = 1 Then
                        cmbRuolo.ClearSelection()
                        cmbRuolo.SelectedValue = par.IfNull(lettore("ID_TIPO_RUOLO"), 1)
                        cmbRuolo.Enabled = False
                    End If
                End If

                txtDocIdent.Text = par.IfNull(lettore("CARTA_I"), "")
                If par.IfNull(lettore("CARTA_I_DATA"), "") <> "" Then
                    txtDataDocI.SelectedDate = par.FormattaData(par.IfNull(lettore("CARTA_I_DATA"), ""))
                End If
                txtRilasciata.Text = par.IfNull(lettore("CARTA_I_RILASCIATA"), "")
                txtPermSogg.Text = par.IfNull(lettore("PERMESSO_SOGG_N"), "")
                If par.IfNull(lettore("PERMESSO_SOGG_DATA"), "") <> "" Then
                    txtDataPermSogg.SelectedDate = par.FormattaData(par.IfNull(lettore("PERMESSO_SOGG_DATA"), ""))
                End If
            End If
            lettore.Close()

            If ApertaNow Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub ScriviOspiti()

        If txtCognome.Text = "" Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare il cognome!", 450, 150, "Attenzione", Nothing, Nothing)
            Exit Sub
        End If
        If txtNome.Text = "" Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare il nome!", 450, 150, "Attenzione", Nothing, Nothing)
            Exit Sub
        End If
        If txtCF.Text = "" Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare il codice fiscale!", 450, 150, "Attenzione", Nothing, Nothing)
            Exit Sub
        End If
        If cmbParenti.SelectedValue = -1 Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la parentela!", 450, 150, "Attenzione", Nothing, Nothing)
            Exit Sub
        End If
        If IsNothing(txtDataInizio.SelectedDate) Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la data di inizio!", 450, 150, "Attenzione", Nothing, Nothing)
            Exit Sub
        End If
        If idMotivoIstanza.Value = "7" Then
            If IsNothing(txtDataFine.SelectedDate) Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la data di fine!", 450, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            End If
            If cmbRuolo.SelectedValue = "2" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Tipo ruolo non corretto!", 450, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            End If
        Else
            If cmbRuolo.SelectedValue = "1" Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Tipo ruolo non corretto!", 450, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            End If
        End If
        connData.apri(True)

        Dim numProgr As Integer = 0
        Dim tipoInval As String = ""
        Dim naturaInval As String = ""
        Dim idOsp As Long = 0
        Dim OspOld As Boolean = False
        Dim idOspite As Long = 0
        Dim referente As Integer = 0
        Dim i As Integer = 0
        Dim idOspOld As Long = 0

        Dim idOspiti As String = ""

        Dim dataNasc As String = ""
        Dim dataDocI As String = ""
        Dim dataInizio As String = ""
        Dim dataFine As String = ""
        Dim dataPermSogg As String = ""
        Dim ospInaltroRU As Boolean = False

        If Not IsNothing(txtData.SelectedDate) Then
            dataNasc = par.FormattaData(txtData.SelectedDate)
        End If
        If Not IsNothing(txtDataDocI.SelectedDate) Then
            dataDocI = par.FormattaData(txtDataDocI.SelectedDate)
        End If
        If Not IsNothing(txtDataInizio.SelectedDate) Then
            dataInizio = par.FormattaData(txtDataInizio.SelectedDate)
        End If
        If Not IsNothing(txtDataFine.SelectedDate) Then
            dataFine = par.FormattaData(txtDataFine.SelectedDate)
        End If
        If Not IsNothing(txtDataPermSogg.SelectedDate) Then
            dataPermSogg = par.FormattaData(txtDataPermSogg.SelectedDate)
        End If

        Dim CTRL As Control = Nothing
        Dim mpContentPlaceHolder As ContentPlaceHolder
        mpContentPlaceHolder = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)

        For Each CTRL In mpContentPlaceHolder.Controls
            If TypeOf CTRL Is RadTextBox Then
                DirectCast(CTRL, RadTextBox).Text = DirectCast(CTRL, RadTextBox).Text.ToUpper
            End If
        Next

        Dim incrementaProgr As Integer = 0
        If operazione.Value = "0" Then

            par.cmd.CommandText = "SELECT COUNT(ID) AS NUMOSP FROM COMP_NUCLEO_OSPITI_VSA WHERE ID_TIPO_RUOLO=" & cmbRuolo.SelectedValue & " AND ID_DOMANDA in (select id from domande_bando_vsa where id_dichiarazione=" & iddich.Value & ")"
            Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderC.Read Then
                numProgr = par.IfNull(myReaderC("NUMOSP"), 0)
            End If
            myReaderC.Close()

            If numProgr > 0 Then
                incrementaProgr = numProgr
            End If

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_OSPITI_VSA WHERE ID_TIPO_RUOLO=" & cmbRuolo.SelectedValue & " AND ID_DOMANDA in (select id from domande_bando_vsa where id_dichiarazione=" & iddich.Value & ") AND COD_FISCALE='" & par.IfEmpty(txtCF.Text, "") & "'"
            myReaderC = par.cmd.ExecuteReader()
            If myReaderC.Read Then
                idOspOld = par.IfNull(myReaderC("ID"), 0)
                OspOld = True
            End If
            myReaderC.Close()

            par.cmd.CommandText = "SELECT id from siscom_mi.ospiti where cod_fiscale='" & par.IfEmpty(txtCF.Text, "") & "'" _
                & " and id_contratto<>" & idContratto.Value & " And nvl(data_fine_ospite,'29991231')>='" & Format(Now, "yyyyMMdd") & "' " _
                & " and siscom_mi.getstatocontratto(id_contratto)<>'CHIUSO'"
            myReaderC = par.cmd.ExecuteReader()
            If myReaderC.Read Then
                ospInaltroRU = True
            End If
            myReaderC.Close()

            '//
            If ospInaltroRU = False Then
                par.cmd.CommandText = "select id_anagrafica from siscom_mi.soggetti_contrattuali where id_contratto<>" & idContratto.Value & "" _
                & " and id_anagrafica = (select max(id) from siscom_mi.anagrafica where cod_fiscale='" & par.IfEmpty(txtCF.Text, "") & "')" _
                & " and nvl(data_fine,'29991231')>='" & Format(Now, "yyyyMMdd") & "' and siscom_mi.getstatocontratto(id_contratto)<>'CHIUSO'"
                myReaderC = par.cmd.ExecuteReader()
                If myReaderC.Read Then
                    ospInaltroRU = True
                End If
                myReaderC.Close()
            End If

            If OspOld = False Then
                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_OSPITI_VSA (ID,ID_DOMANDA,DATA_AGG,COGNOME,NOME,COD_FISCALE,DATA_INIZIO_OSPITE,DATA_FINE_OSPITE,DATA_NASC,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,COMUNE_RES_DNTE,CAP_RES_DNTE,CARTA_I,CARTA_I_DATA,CARTA_I_RILASCIATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,GRADO_PARENTELA,ID_TIPO_RUOLO) " _
                            & "VALUES (SEQ_COMP_NUCLEO_OSPITI_VSA.NEXTVAL,(Select id from domande_bando_vsa where id_dichiarazione=" & iddich.Value & "),'" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(par.IfEmpty(txtCognome.Text, "")) & "','" & par.PulisciStrSql(par.IfEmpty(txtNome.Text, "")) _
                            & "','" & par.IfEmpty(txtCF.Text, "") & "'," & par.insDbValue(dataInizio, True, True) & "," & par.insDbValue(dataFine, True, True) & "," & par.insDbValue(dataNasc, True, True) & "," _
                            & cmbTipoVia.SelectedValue & ",'" & par.PulisciStrSql(par.IfEmpty(txtVia.Text, "")) & "','" & par.IfEmpty(txtCivico.Text, "") & "','" & par.IfEmpty(txtComune.Text, "") & "','" & par.IfEmpty(txtCap.Text, "") & "','" & par.IfEmpty(txtDocIdent.Text, "") & "'," _
                            & par.insDbValue(dataDocI, True, True) & ",'" & par.PulisciStrSql(par.IfEmpty(txtRilasciata.Text, "")) & "','" & par.IfEmpty(txtPermSogg.Text, "") & "'," & par.insDbValue(dataPermSogg, True, True) & "," & cmbParenti.SelectedValue & "," & cmbRuolo.SelectedValue & ")"

                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "UPDATE COMP_NUCLEO_OSPITI_VSA SET COD_FISCALE='" & txtCF.Text & "',COGNOME='" & RTrim(LTrim(par.PulisciStrSql(txtCognome.Text))) & "',NOME='" & RTrim(LTrim(par.PulisciStrSql(txtNome.Text))) & "',DATA_INIZIO_OSPITE=" & par.insDbValue(dataInizio, True, True) & ",DATA_FINE_OSPITE=" & par.insDbValue(dataFine, True, True) & ",DATA_NASC=" & par.insDbValue(dataNasc, True, True) & ",ID_TIPO_IND_RES_DNTE=" & cmbTipoVia.SelectedValue & ",IND_RES_DNTE='" & RTrim(LTrim(par.PulisciStrSql(txtVia.Text))) & "'," _
                    & "CIVICO_RES_DNTE='" & txtCivico.Text & "',COMUNE_RES_DNTE='" & RTrim(LTrim(par.PulisciStrSql(txtComune.Text))) & "',CAP_RES_DNTE= '" & RTrim(LTrim(par.PulisciStrSql(txtCap.Text))) & "',CARTA_I='" & txtDocIdent.Text & "',CARTA_I_DATA=" & par.insDbValue(dataDocI, True, True) & ",CARTA_I_RILASCIATA='" & RTrim(LTrim(par.PulisciStrSql(txtRilasciata.Text))) & "',PERMESSO_SOGG_N='" & txtPermSogg.Text & "',PERMESSO_SOGG_DATA=" & par.insDbValue(dataPermSogg, True, True) & ",GRADO_PARENTELA=" & cmbParenti.SelectedValue & ",ID_TIPO_RUOLO=" & cmbRuolo.SelectedValue & " WHERE ID= " & idOspOld

                par.cmd.ExecuteNonQuery()
            End If

        Else
            par.cmd.CommandText = "UPDATE COMP_NUCLEO_OSPITI_VSA SET COD_FISCALE='" & txtCF.Text & "',COGNOME='" & RTrim(LTrim(par.PulisciStrSql(txtCognome.Text))) & "',NOME='" & RTrim(LTrim(par.PulisciStrSql(txtNome.Text))) & "',DATA_INIZIO_OSPITE=" & par.insDbValue(dataInizio, True, True) & ",DATA_FINE_OSPITE=" & par.insDbValue(dataFine, True, True) & ",DATA_NASC=" & par.insDbValue(dataNasc, True, True) & ",ID_TIPO_IND_RES_DNTE=" & cmbTipoVia.SelectedValue & ",IND_RES_DNTE='" & RTrim(LTrim(par.PulisciStrSql(txtVia.Text))) & "'," _
                    & "CIVICO_RES_DNTE='" & txtCivico.Text & "',COMUNE_RES_DNTE='" & RTrim(LTrim(par.PulisciStrSql(txtComune.Text))) & "',CAP_RES_DNTE= '" & RTrim(LTrim(par.PulisciStrSql(txtCap.Text))) & "',CARTA_I='" & txtDocIdent.Text & "',CARTA_I_DATA=" & par.insDbValue(dataDocI, True, True) & ",CARTA_I_RILASCIATA='" & RTrim(LTrim(par.PulisciStrSql(txtRilasciata.Text))) & "',PERMESSO_SOGG_N='" & txtPermSogg.Text & "',PERMESSO_SOGG_DATA=" & par.insDbValue(dataPermSogg, True, True) & ",GRADO_PARENTELA=" & cmbParenti.SelectedValue & ",ID_TIPO_RUOLO=" & cmbRuolo.SelectedValue & " WHERE ID= " & idComp.Value
            par.cmd.ExecuteNonQuery()

        End If

        If ospInaltroRU = True Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Ospite presente in altro contratto!", 450, 150, "Attenzione", Nothing, Nothing)
            'connData.chiudi(False)
            'Exit Sub
        End If

        connData.chiudi(True)
        par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)

    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        Try
            ScriviOspiti()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub txtCF_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCF.TextChanged
        Try
            Dim MIADATA As String
            Dim CF As String = txtCF.Text.ToUpper

            If Val(Mid(CF, 10, 2)) > 40 Then
                MIADATA = Format(Val(Mid(CF, 10, 2)) - 40, "00")
            Else
                MIADATA = Mid(CF, 10, 2)
            End If

            Select Case Mid(CF, 9, 1)
                Case "A"
                    MIADATA = MIADATA & "/01"
                Case "B"
                    MIADATA = MIADATA & "/02"
                Case "C"
                    MIADATA = MIADATA & "/03"
                Case "D"
                    MIADATA = MIADATA & "/04"
                Case "E"
                    MIADATA = MIADATA & "/05"
                Case "H"
                    MIADATA = MIADATA & "/06"
                Case "L"
                    MIADATA = MIADATA & "/07"
                Case "M"
                    MIADATA = MIADATA & "/08"
                Case "P"
                    MIADATA = MIADATA & "/09"
                Case "R"
                    MIADATA = MIADATA & "/10"
                Case "S"
                    MIADATA = MIADATA & "/11"
                Case "T"
                    MIADATA = MIADATA & "/12"
            End Select
            If Mid(CF, 7, 1) = "0" Then
                MIADATA = MIADATA & "/200" & Mid(CF, 8, 1)
                If Format(CDate(MIADATA), "yyyyMMdd") > Format(Now, "yyyyMMdd") Then
                    MIADATA = Mid(MIADATA, 1, 6) & "19" & Mid(MIADATA, 9, 2)
                End If
            Else
                MIADATA = MIADATA & "/19" & Mid(CF, 7, 2)
            End If

            Dim numAnni As Integer = DateDiff(DateInterval.Year, CDate(MIADATA), CDate(Format(Now, "dd/MM/yyyy")))

            If numAnni > 99 Then
                MIADATA = Mid(MIADATA, 1, 6) & "20" & Mid(MIADATA, 9, 10)
            End If

            txtData.SelectedDate = MIADATA

        Catch ex As Exception
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Errore C.F.!", 450, 150, "Attenzione", Nothing, Nothing)
        End Try
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If idMotivoIstanza.Value = "7" Then
            If idTipologia.Value = "" Or idTipologia.Value = "-1" Then
                'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "chiudiMaschera", "GetRadWindow().close();", True)
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la tipologia di ospitalità!", 450, 150, "Attenzione", "chiudiMaschera", Nothing)

            End If
        End If
    End Sub

    Protected Sub txtDataInizio_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtDataInizio.SelectedDateChanged
        Try
            Select Case idTipologia.Value
                Case "1"
                    txtDataFine.SelectedDate = Format(DateAdd(DateInterval.Month, 6, CDate(txtDataInizio.SelectedDate)), "dd/MM/yyyy")
                Case "2"
                    txtDataFine.SelectedDate = Format(DateAdd(DateInterval.Month, 12, CDate(txtDataInizio.SelectedDate)), "dd/MM/yyyy")
            End Select
            If idTipologia.Value <> "" Then
                txtDataFine.SelectedDate = Date.Parse(txtDataFine.SelectedDate, New System.Globalization.CultureInfo("it-IT", False)).AddDays(-1).ToString("dd/MM/yyyy")
            End If
        Catch ex As Exception
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
