Imports Telerik.Web.UI
Partial Class Gestione_locatari_ComponentiNucleo
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
            par.caricaComboTelerik("select * from t_tipo_parentela where cod<>'8' and cod<>'17' and cod<>'24' and cod<>'26' and cod<>'28' and cod<>'30' order by cod asc", cmbParenti, "COD", "DESCRIZIONE", True, "null")
            par.caricaComboTelerik("select * from t_tipo_indirizzo order by cod asc", cmbTipoVia, "COD", "DESCRIZIONE", True, "null")
            par.caricaComboTelerik("select * from natura_invalidita order by id asc", cmbNaturaInval, "ID", "DESCRIZIONE", True, "null")
            cmbNuovoComp.SelectedValue = "1"
            If Not String.IsNullOrEmpty(idComp.Value.ToString) Then
                VisualizzaDati()
            Else
                CaricaIndirizzi()
            End If
        End If

    End Sub

    Private Sub CaricaIndirizzi()
        Try
            Dim ApertaNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                ApertaNow = True
            End If
            
            If txtVia.Text = "" Then
                par.cmd.CommandText = "select tipo_cor,via_cor,civico_cor,cap_cor,luogo_cor " _
                    & " from siscom_mi.rapporti_utenza where " _
                    & " id in (select id_contratto from " _
                    & " domande_bando_vsa where id_dichiarazione=" & iddich.Value & ")" _
                    & " "
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    txtComune.Text = par.IfNull(lettore("luogo_cor"), "")
                    cmbTipoVia.SelectedItem.Text = par.IfNull(lettore("tipo_cor"), "")
                    txtVia.Text = par.IfNull(lettore("via_cor"), "")
                    txtCivico.Text = par.IfNull(lettore("civico_cor"), "")
                    txtCap.Text = par.IfNull(lettore("cap_cor"), "")
                End If
                lettore.Close()
            End If

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

    Private Sub VisualizzaDati()
        Try
            Dim ApertaNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                ApertaNow = True
            End If
            cmbNuovoComp.ClearSelection()
            par.cmd.CommandText = "select comp_nucleo_vsa.* from comp_nucleo_vsa where id_dichiarazione=" & iddich.Value & " and comp_nucleo_vsa.id=" & idComp.Value
            Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore0.Read Then
                cmbNuovoComp.SelectedValue = "0"
                txtCognome.Text = par.IfNull(lettore0("COGNOME"), "")
                txtNome.Text = par.IfNull(lettore0("NOME"), "")
                txtDataNascita.SelectedDate = par.FormattaData(par.IfNull(lettore0("DATA_NASCITA"), ""))
                txtCodiceFiscale.Text = par.IfNull(lettore0("COD_FISCALE"), "")
                cmbTipoInval.SelectedValue = par.IfNull(lettore0("TIPO_INVAL"), "")
                cmbNaturaInval.SelectedValue = par.IfNull(lettore0("ID_NATURA_INVAL"), "-1")
                cmbParenti.SelectedValue = par.IfNull(lettore0("GRADO_PARENTELA"), 1)
                txtASL.Text = par.IfNull(lettore0("USL"), "")
                txtInv.Text = par.IfNull(lettore0("PERC_INVAL"), "0")
                cmbAcc.SelectedValue = par.IfNull(lettore0("INDENNITA_ACC"), "")
            End If
            lettore0.Close()

            par.cmd.CommandText = "select nuovi_comp_nucleo_vsa.*,comp_nucleo_vsa.progr from nuovi_comp_nucleo_vsa,comp_nucleo_vsa where " _
                    & " id_dichiarazione=" & iddich.Value & " and nuovi_comp_nucleo_vsa.id_componente = comp_nucleo_vsa.id and ID_COMPONENTE=" & idComp.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                cmbNuovoComp.SelectedValue = "1"
                txtComune.Text = par.IfNull(lettore("COMUNE_RES_DNTE"), "")
                cmbTipoVia.SelectedValue = par.IfNull(lettore("ID_TIPO_IND_RES_DNTE"), "")
                txtVia.Text = par.IfNull(lettore("IND_RES_DNTE"), "")
                txtCivico.Text = par.IfNull(lettore("CIVICO_RES_DNTE"), "")
                txtCap.Text = par.IfNull(lettore("CAP_RES_DNTE"), "")
                If par.IfNull(lettore("DATA_INGRESSO_NUCLEO"), "") <> "" Then
                    txtDataIngr.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_INGRESSO_NUCLEO"), ""))
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

            If txtVia.Text = "" Then
                par.cmd.CommandText = "select indirizzi.* from siscom_mi.unita_immobiliari,siscom_mi.indirizzi where " _
                    & " unita_immobiliari.id=(select id_unita from siscom_mi.unita_contrattuale where " _
                    & " id_unita_principale is null and id_contratto in (select id_contratto from " _
                    & " domande_bando_vsa where id_dichiarazione=" & iddich.Value & "))" _
                    & " and indirizzi.id=id_indirizzo "
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    txtComune.Text = par.IfNull(lettore("localita"), "")
                    cmbTipoVia.SelectedValue = par.IfNull(lettore("tipo_indirizzo"), "1")
                    txtVia.Text = par.IfNull(lettore("descrizione"), "")
                    txtCivico.Text = par.IfNull(lettore("CIVICO"), "")
                    txtCap.Text = par.IfNull(lettore("cap"), "")
                End If
                lettore.Close()
            End If

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

    Private Sub ScriviComponente()

        If txtCognome.Text = "" Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare il cognome!", 450, 150, "Attenzione", Nothing, Nothing)
            Exit Sub
        End If
        If txtNome.Text = "" Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare il nome!", 450, 150, "Attenzione", Nothing, Nothing)
            Exit Sub
        End If
        If txtCodiceFiscale.Text = "" Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare il codice fiscale!", 450, 150, "Attenzione", Nothing, Nothing)
            Exit Sub
        End If
        If cmbParenti.SelectedValue = -1 Then
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la parentela!", 450, 150, "Attenzione", Nothing, Nothing)
            Exit Sub
        End If
        If cmbNuovoComp.SelectedValue = "1" Then
            If IsNothing(txtDataIngr.SelectedDate) Then
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Specificare la data di ingresso nel nucleo!", 450, 150, "Attenzione", Nothing, Nothing)
                Exit Sub
            End If
        End If
        connData.apri(True)
        Dim numProgr As Integer = 0
        Dim tipoInval As String = ""
        Dim naturaInval As String = ""
        Dim compOld As Boolean = False
        Dim idComponente As Long = 0
        Dim referente As Integer = 0
        Dim i As Integer = 0
        Dim idCompOld As Long = 0

        Dim CTRL As Control = Nothing
        Dim mpContentPlaceHolder As ContentPlaceHolder
        mpContentPlaceHolder = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)

        For Each CTRL In mpContentPlaceHolder.Controls
            If TypeOf CTRL Is RadTextBox Then
                DirectCast(CTRL, RadTextBox).Text = DirectCast(CTRL, RadTextBox).Text.ToUpper
            End If
        Next

        If CDec(par.IfEmpty(txtInv.Text, "0")) = 0 Then
            tipoInval = ""
            naturaInval = "NULL"
        Else
            tipoInval = cmbTipoInval.SelectedValue
            naturaInval = cmbNaturaInval.SelectedValue
        End If

        Dim incrementaProgr As Integer = 0
        If operazione.Value = "0" Then

            par.cmd.CommandText = "SELECT COUNT(ID) AS NUMCOMP FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & iddich.Value
            Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderC.Read Then
                numProgr = par.IfNull(myReaderC("NUMCOMP"), 0)
            End If
            myReaderC.Close()

            If numProgr > 0 Then
                incrementaProgr = numProgr
            End If

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & iddich.Value & " AND COD_FISCALE='" & par.IfEmpty(txtCodiceFiscale.Text, "") & "'"
            myReaderC = par.cmd.ExecuteReader()
            If myReaderC.Read Then
                idCompOld = par.IfNull(myReaderC("ID"), 0)
                compOld = True
            End If
            myReaderC.Close()

            If compOld = False Then
                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL,DATA_NASCITA,USL,INDENNITA_ACC,SESSO, TIPO_INVAL, ID_NATURA_INVAL" _
                            & ") VALUES (SEQ_COMP_NUCLEO_VSA.NEXTVAL," & iddich.Value & "," & incrementaProgr & ",'" & par.PulisciStrSql(par.IfEmpty(txtCognome.Text, "")) & "','" & par.PulisciStrSql(par.IfEmpty(txtNome.Text, "")) _
                            & "'," & cmbParenti.SelectedValue & ",'" & par.IfEmpty(txtCodiceFiscale.Text, "") & "'," & par.IfEmpty(txtInv.Text, 0) & ",'" & par.AggiustaData(par.IfEmpty(txtDataNascita.SelectedDate, "")) & "','" & par.IfEmpty(txtASL.Text, "") & "'," _
                            & "'" & par.IfEmpty(cmbAcc.SelectedValue, "") & "','" & par.RicavaSesso(par.IfEmpty(txtCodiceFiscale.Text, "")) & "','" & tipoInval & "'," & par.IfEmpty(naturaInval, "NULL") & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    idComponente = myReader(0)
                End If
                myReader.Close()

                If cmbNuovoComp.SelectedValue <> "0" Then
                    par.cmd.CommandText = "INSERT INTO NUOVI_COMP_NUCLEO_VSA (ID_COMPONENTE,DATA_INGRESSO_NUCLEO,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,COMUNE_RES_DNTE,CAP_RES_DNTE,CARTA_I,CARTA_I_DATA,CARTA_I_RILASCIATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,FL_REFERENTE) VALUES " _
                    & "(" & idComponente & ",'" & par.AggiustaData(par.IfEmpty(txtDataIngr.SelectedDate, "")) & "'," & cmbTipoVia.SelectedValue & ",'" & par.PulisciStrSql(par.IfEmpty(txtVia.Text, "")) & "','" & par.IfEmpty(txtCivico.Text, "") & "','" & par.PulisciStrSql(par.IfEmpty(txtComune.Text, "")) & "'," _
                    & "'" & par.IfEmpty(txtCap.Text, "") & "','" & par.IfEmpty(txtDocIdent.Text, "") & "','" & par.AggiustaData(par.IfEmpty(txtDataDocI.SelectedDate, "")) & "','" & par.PulisciStrSql(par.IfEmpty(txtRilasciata.Text, "")) & "','" & par.IfEmpty(txtPermSogg.Text, "") & "','" & par.PulisciStrSql(par.IfEmpty(txtDataPermSogg.SelectedDate, "")) & "'," & referente & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Else

                par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET COD_FISCALE='" & txtCodiceFiscale.Text & "',COGNOME='" & RTrim(LTrim(par.PulisciStrSql(txtCognome.Text))) & "',NOME='" & RTrim(LTrim(par.PulisciStrSql(txtNome.Text))) & "',SESSO='" & par.RicavaSesso(txtCodiceFiscale.Text) & "',DATA_NASCITA='" & par.AggiustaData(txtDataNascita.SelectedDate) & "',USL='" & txtASL.Text & "',GRADO_PARENTELA=" & cmbParenti.SelectedValue & "," _
                    & "PERC_INVAL=" & par.IfEmpty(txtInv.Text, 0) & ",INDENNITA_ACC='" & cmbAcc.SelectedValue & "',TIPO_INVAL= '" & tipoInval & "',ID_NATURA_INVAL=" & naturaInval & " WHERE ID= " & idCompOld
                par.cmd.ExecuteNonQuery()

            End If

            If txtInv.Text = "100" And cmbAcc.SelectedValue = "1" Then
                par.cmd.CommandText = "INSERT INTO COMP_ELENCO_SPESE_VSA (ID,ID_COMPONENTE,DESCRIZIONE,IMPORTO) VALUES (SEQ_COMP_ELENCO_SPESE_VSA.NEXTVAL," & idComponente & ",'',0)"
                par.cmd.ExecuteNonQuery()
            End If
        Else

            par.cmd.CommandText = "UPDATE COMP_NUCLEO_VSA SET COD_FISCALE='" & txtCodiceFiscale.Text & "',COGNOME='" & RTrim(LTrim(par.PulisciStrSql(txtCognome.Text))) & "',NOME='" & RTrim(LTrim(par.PulisciStrSql(txtNome.Text))) & "',SESSO='" & par.RicavaSesso(txtCodiceFiscale.Text) & "',DATA_NASCITA='" & par.AggiustaData(txtDataNascita.SelectedDate) & "',USL='" & txtASL.Text & "',GRADO_PARENTELA=" & cmbParenti.SelectedValue & "," _
                & "PERC_INVAL='" & par.IfEmpty(txtInv.Text, 0) & "',INDENNITA_ACC='" & cmbAcc.SelectedValue & "',TIPO_INVAL= '" & tipoInval & "',ID_NATURA_INVAL=" & naturaInval & " WHERE ID= " & idComp.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE=" & idComp.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                par.cmd.CommandText = "UPDATE NUOVI_COMP_NUCLEO_VSA set " _
                            & "DATA_INGRESSO_NUCLEO='" & par.AggiustaData(par.IfEmpty(txtDataIngr.SelectedDate, "")) & "'," _
                            & "ID_TIPO_IND_RES_DNTE=" & par.IfNull(cmbTipoVia.SelectedValue, "") & "," _
                            & "IND_RES_DNTE='" & par.PulisciStrSql(par.IfEmpty(txtVia.Text, "")) & "'," _
                            & "CIVICO_RES_DNTE='" & par.IfEmpty(txtCivico.Text, "") & "'," _
                            & "COMUNE_RES_DNTE='" & par.PulisciStrSql(par.IfEmpty(txtComune.Text, "")) & "'," _
                            & "CAP_RES_DNTE='" & par.IfEmpty(txtCap.Text, "") & "'," _
                            & "CARTA_I='" & par.IfEmpty(txtDocIdent.Text, "") & "'," _
                            & "CARTA_I_DATA='" & par.AggiustaData(par.IfEmpty(txtDataDocI.SelectedDate, "")) & "', " _
                            & "CARTA_I_RILASCIATA='" & par.PulisciStrSql(par.IfEmpty(txtRilasciata.Text, "")) & "', " _
                            & "PERMESSO_SOGG_N='" & par.IfEmpty(txtPermSogg.Text, "") & "', " _
                            & "PERMESSO_SOGG_DATA='" & par.AggiustaData(par.IfEmpty(txtDataPermSogg.SelectedDate, "")) & "', " _
                            & "FL_REFERENTE='" & referente & "' " _
                            & "WHERE ID_COMPONENTE=" & idComp.Value

                par.cmd.ExecuteNonQuery()
            Else
                If cmbNuovoComp.SelectedValue = "1" Then
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Trattasi di componente già presente nel nucleo!", 350, 150, "Attenzione", Nothing, CM.immagineMessaggio.alert)
                    connData.chiudi(False)
                    Exit Sub
                End If

            End If
            myReader.Close()

            If txtInv.Text = "100" And cmbAcc.SelectedValue = "1" Then

                par.cmd.CommandText = "DELETE FROM COMP_ELENCO_SPESE_VSA WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO COMP_ELENCO_SPESE_VSA (ID,ID_COMPONENTE,DESCRIZIONE,IMPORTO) VALUES (SEQ_COMP_ELENCO_SPESE_VSA.NEXTVAL," & idComp.Value & ",'',0)"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "DELETE FROM COMP_ELENCO_SPESE_VSA WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()
            End If
        End If

        connData.chiudi(True)
        par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        Try
            ScriviComponente()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub txtCodiceFiscale_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodiceFiscale.TextChanged
        Try
            Dim MIADATA As String
            Dim CF As String = txtCodiceFiscale.Text.ToUpper

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

            txtDataNascita.SelectedDate = MIADATA

        Catch ex As Exception
            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Errore C.F.!", 450, 150, "Attenzione", Nothing, Nothing)
        End Try
    End Sub
End Class
