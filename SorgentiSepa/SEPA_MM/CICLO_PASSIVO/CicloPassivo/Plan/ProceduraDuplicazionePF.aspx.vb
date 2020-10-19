
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_ProceduraDuplicazionePF
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Session.Item("BP_FORMALIZZAZIONE") <> "1" Then
            Response.Write("<script>alert('Operatore non abilitato ad eseguire questa operazione!');top.location.href=""../../../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg');z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
        End If
        connData = New CM.datiConnessione(par, False, False)
    End Sub

    Protected Sub ImageButtonAvvia_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAvvia.Click
        If IsNumeric(TextBoxPercentuale.Text) Then
            If HiddenFieldConferma.Value = "1" Then
                If AvviaProcedura() Then
                    Response.Write("<script>alert('Piano finanziario duplicato correttamente!');window.location.href='../../pagina_home.aspx';</script>")
                Else
                    If Session.Item("ERRORE") <> "" Then
                        Response.Write("<script>alert('Errore durante la procedura!');top.location.href='../../../Errore.aspx';</script>")
                    Else
                        Response.Write("<script>alert('Errore durante la procedura!');window.location.href='../../pagina_home.aspx';</script>")
                    End If
                End If
            End If

        Else
            If TextBoxPercentuale.Text = "" Then
                If HiddenFieldConferma.Value = "1" Then
                    If AvviaProcedura() Then
                        Response.Write("<script>alert('Piano finanziario duplicato correttamente!');window.location.href='../../pagina_home.aspx';</script>")
                    Else
                        If Session.Item("ERRORE") <> "" Then
                            Response.Write("<script>alert('Errore durante la procedura!');top.location.href='../../../Errore.aspx';</script>")
                        Else
                            Response.Write("<script>alert('Errore durante la procedura!');window.location.href='../../pagina_home.aspx';</script>")
                        End If
                    End If
                End If
            Else
                Response.Write("<script>alert('La percentuale deve essere numerica!');")
            End If
        End If
    End Sub
    Function AvviaProcedura() As Boolean
        Try
            Session.Remove("ERRORE")
            connData.apri(True)
            '‘par.cmd.Transaction = connData.Transazione
            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim percentuale As Decimal
            If TextBoxPercentuale.Text = "" Then
                percentuale = 0
            Else
                percentuale = CDec(TextBoxPercentuale.Text)
            End If

            Dim ErroriP As Boolean = True

            'selezione del piano finanziario in corsa
            Dim idPFAttuale As Integer
            Dim idEsercizioFinanziarioAttuale As Integer
            par.cmd.CommandText = "SELECT PF_MAIN.ID AS ID,T_ESERCIZIO_FINANZIARIO.ID AS ID_EF " _
                & " FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                & " WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                & " AND ID_STATO=5 ORDER BY ID DESC"
            Lettore = par.cmd.ExecuteReader
            If Lettore.Read Then
                idPFAttuale = par.IfNull(Lettore("ID"), 0)
                idEsercizioFinanziarioAttuale = par.IfNull(Lettore("ID_EF"), 0)
            End If
            Lettore.Close()
            If idPFAttuale > 0 Then
                'duplicazione dei lotti
                par.cmd.CommandText = " SELECT ID, ID_FILIALE, TIPO, DESCRIZIONE, " _
                    & " ID_ESERCIZIO_FINANZIARIO, COD_TIPO_IMPIANTO, ID_OLD " _
                    & " FROM SISCOM_MI.LOTTI " _
                    & " WHERE ID_ESERCIZIO_FINANZIARIO=" & idEsercizioFinanziarioAttuale _
                    & " ORDER BY ID ASC "
                Lettore = par.cmd.ExecuteReader
                Dim idLotto As Integer
                Dim idFiliale As Integer
                Dim tipo As String
                Dim descrizione As String
                Dim codTipoImpianti As String
                Dim idLottoNew As Integer
                While Lettore.Read

                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_LOTTI.NEXTVAL FROM DUAL"
                    Dim lettoreSeqLotti As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreSeqLotti.Read Then
                        idLottoNew = par.IfNull(lettoreSeqLotti(0), 0)
                    End If
                    lettoreSeqLotti.Close()
                    If idLottoNew > 0 Then

                        idLotto = par.IfNull(Lettore("ID"), 0)
                        idFiliale = par.IfNull(Lettore("ID_FILIALE"), 0)
                        tipo = par.IfNull(Lettore("TIPO"), "")
                        descrizione = par.IfNull(Lettore("DESCRIZIONE"), "")
                        codTipoImpianti = par.IfNull(Lettore("COD_TIPO_IMPIANTO"), "")

                        If idLotto > 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI (ID, ID_FILIALE, " _
                                & " TIPO, DESCRIZIONE, ID_ESERCIZIO_FINANZIARIO, COD_TIPO_IMPIANTO, ID_OLD) " _
                                & " VALUES (" & idLottoNew & ", " & idFiliale & "," _
                                & "'" & tipo & "'," _
                                & "'" & Replace(descrizione, "'", "''") & "'," _
                                & idEsercizioFinanziarioAttuale + 1 & "," _
                                & "'" & codTipoImpianti & "'," _
                                & idLotto & ")"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_PATRIMONIO " _
                                & " (ID_LOTTO,ID_COMPLESSO,ID_EDIFICIO,ID_IMPIANTO) " _
                                & " SELECT " & idLottoNew & ",ID_COMPLESSO,ID_EDIFICIO,ID_IMPIANTO " _
                                & " FROM SISCOM_MI.LOTTI_PATRIMONIO WHERE ID_LOTTO = " & idLotto
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.LOTTI_SERVIZI (ID_LOTTO,ID_SERVIZIO) " _
                                & " SELECT " & idLottoNew & ",ID_SERVIZIO " _
                                & " FROM SISCOM_MI.LOTTI_SERVIZI " _
                                & " WHERE ID_LOTTO = " & idLotto
                            par.cmd.ExecuteNonQuery()
                        Else
                            ErroriP = False
                        End If
                    Else
                        connData.chiudi(False)
                        Return False

                    End If


                End While
                Lettore.Close()

                'inserimento nuovo piano finanziario
                Dim idPFnew As Integer
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PF_MAIN.NEXTVAL FROM DUAL"
                Lettore = par.cmd.ExecuteReader
                If Lettore.Read Then
                    idPFnew = par.IfNull(Lettore(0), 0)
                End If
                Lettore.Close()
                If idPFnew > 0 Then

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                        & " WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                        & " AND T_ESERCIZIO_FINANZIARIO.ID=" & idEsercizioFinanziarioAttuale + 1
                    Lettore = par.cmd.ExecuteReader

                    If Not Lettore.HasRows Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_MAIN " _
                        & " (ID, ID_ESERCIZIO_FINANZIARIO, ID_STATO, PREV_APPLICATI, APPLICAZIONE_BOL, ERRORE_PREV ) " _
                        & " VALUES (" & idPFnew & ", " & idEsercizioFinanziarioAttuale + 1 & ", 1, 0, 0, 0)"
                        par.cmd.ExecuteNonQuery()
                    Else
                        connData.chiudi(False)
                        Return False
                    End If
                    Lettore.Close()
                Else
                    connData.chiudi(False)
                    Return False
                End If

                'duplicazione pf_voci
                par.cmd.CommandText = " SELECT PF_VOCI.*,ID AS ID_PF_VOCE_OLD,ID_VOCE_MADRE AS ID_VOCE_MADRE_OLD FROM SISCOM_MI.PF_VOCI " _
                    & " WHERE id_piano_finanziario=" & idPFAttuale _
                    & " CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                    & " START WITH ID_VOCE_MADRE IS NULL ORDER BY LEVEL,ID "

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                Dim LettoreSeq As Oracle.DataAccess.Client.OracleDataReader
                If dt.Rows.Count > 0 Then
                    Dim idPFvociNew As Integer
                    'modifica dt con valori nuovi di id e id_voce_madre
                    For Each riga As Data.DataRow In dt.Rows
                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PF_VOCI.NEXTVAL FROM DUAL "
                        LettoreSeq = par.cmd.ExecuteReader
                        If LettoreSeq.Read Then
                            idPFvociNew = par.IfNull(LettoreSeq(0), 0)
                            If idPFvociNew > 0 Then
                                'nuovo id
                                riga.Item(0) = idPFvociNew
                                Dim rSelect As Data.DataRow()
                                If Not IsDBNull(riga.Item("ID_VOCE_MADRE_OLD")) Then
                                    rSelect = dt.Select("ID_PF_VOCE_OLD=" & riga.Item("ID_VOCE_MADRE_OLD"))
                                    If rSelect.Length > 0 Then
                                        'nuovo id voce madre
                                        riga.Item(4) = rSelect(0).Item("ID")
                                    Else
                                        connData.chiudi(False)
                                        Return False
                                    End If
                                End If
                            Else
                                connData.chiudi(False)
                                Return False
                            End If
                        End If
                        LettoreSeq.Close()
                    Next
                    'insert tutta la nuova dt
                    Dim id As Integer
                    Dim idPianoFinanziario As Integer = idPFnew
                    Dim codice As String
                    Dim descrizionePfVoci As String
                    Dim idVoceMadre As String
                    Dim indice As String
                    Dim idCapitolo As String
                    Dim flcc As String
                    Dim idOld As String
                    For Each riga As Data.DataRow In dt.Rows

                        id = riga.Item("ID").ToString
                        codice = par.IfNull(riga.Item("CODICE"), "")
                        descrizionePfVoci = par.IfNull(riga.Item("DESCRIZIONE"), "")
                        idVoceMadre = par.IfNull(riga.Item("ID_VOCE_MADRE"), "NULL")
                        indice = par.IfNull(riga.Item("INDICE"), "")
                        idCapitolo = par.IfNull(riga.Item("ID_CAPITOLO"), "NULL")
                        flcc = par.IfNull(riga.Item("FL_CC"), "")
                        idOld = par.IfNull(riga.Item("ID_PF_VOCE_OLD"), "")


                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.PF_VOCI ( " _
                            & " ID, ID_PIANO_FINANZIARIO, CODICE,  " _
                            & " DESCRIZIONE, ID_VOCE_MADRE, INDICE,  " _
                            & " ID_CAPITOLO, FL_CC, ID_OLD)  " _
                            & " VALUES ( " & id & ", " _
                            & idPianoFinanziario & ", " _
                            & " '" & codice & "', " _
                            & " '" & Replace(descrizionePfVoci, "'", "''") & "', " _
                            & idVoceMadre & ", " _
                            & indice & ", " _
                            & idCapitolo & ", " _
                            & flcc & ", " _
                            & idOld & " ) "
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_OPERATORI " _
                            & " (ID_VOCE, ID_OPERATORE) " _
                            & " SELECT " & id & " , ID_OPERATORE " _
                            & " FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_VOCE=" & idOld
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.TAB_SERVIZI_VOCI (ID, " _
                            & " ID_SERVIZIO, " _
                            & " DESCRIZIONE, " _
                            & " TIPO_CARICO, " _
                            & " PERC_REVERSIBILITA, " _
                            & " IVA_CANONE, " _
                            & " IVA_CONSUMO, " _
                            & " QUOTA_PREVENTIVA, " _
                            & " NO_MOD, " _
                            & " ID_VOCE, " _
                            & " ID_CATEGORIA, " _
                            & " QUOTA_PREVENTIVA_OLD, " _
                            & " ID_OLD) " _
                            & " SELECT SISCOM_MI.SEQ_TAB_SERVIZI_VOCI.NEXTVAL, " _
                            & " ID_SERVIZIO, " _
                            & " DESCRIZIONE, " _
                            & " TIPO_CARICO, " _
                            & " PERC_REVERSIBILITA, " _
                            & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CANONE)) AS IVA_CANONE, " _
                            & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CONSUMO)) AS IVA_CONSUMO, " _
                            & " QUOTA_PREVENTIVA, " _
                            & " NO_MOD, " _
                            & id & ", " _
                            & " ID_CATEGORIA, " _
                            & " QUOTA_PREVENTIVA_OLD, " _
                            & " ID " _
                            & " FROM SISCOM_MI.TAB_SERVIZI_VOCI " _
                            & " WHERE ID_VOCE = " & idOld
                        par.cmd.ExecuteNonQuery()


                        Try
                            Select Case RadioButtonList1.SelectedValue
                                Case 1
                                    If idOld = 2118 Then
                                        Beep()
                                    End If
                                    'duplica struttura piano finanziario
                                    par.cmd.CommandText = " INSERT INTO SISCOM_MI.PF_VOCI_IMPORTO (ID, " _
                                        & " ID_VOCE, " _
                                        & " ID_LOTTO, " _
                                        & " ID_SERVIZIO, " _
                                        & " DESCRIZIONE, " _
                                        & " VALORE_CANONE, " _
                                        & " IVA_CANONE, " _
                                        & " VALORE_CONSUMO, " _
                                        & " IVA_CONSUMO, " _
                                        & " PERC_REVERSIBILITA, " _
                                        & " ID_VOCE_SERVIZIO, " _
                                        & " ID_OLD) " _
                                        & " SELECT SISCOM_MI.SEQ_PF_VOCI_IMPORTO.NEXTVAL, " _
                                        & id & ", " _
                                        & " (SELECT ID " _
                                        & " FROM SISCOM_MI.LOTTI " _
                                        & " WHERE ID_OLD = PF_VOCI_IMPORTO.ID_LOTTO), " _
                                        & " ID_SERVIZIO, " _
                                        & " DESCRIZIONE, " _
                                        & " 0, " _
                                        & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CANONE)) AS IVA_CANONE, " _
                                        & " 0, " _
                                        & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CONSUMO)) AS IVA_CONSUMO, " _
                                        & " PERC_REVERSIBILITA, " _
                                        & " (SELECT ID " _
                                        & " FROM SISCOM_MI.TAB_SERVIZI_VOCI " _
                                        & " WHERE ID_OLD = PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO), " _
                                        & " ID " _
                                        & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                        & " WHERE ID_VOCE = " & idOld
                                    par.cmd.ExecuteNonQuery()
                                Case 2
                                    'duplica con variazione percentuale
                                    
                                    par.cmd.CommandText = " INSERT INTO SISCOM_MI.PF_VOCI_IMPORTO (ID, " _
                                        & " ID_VOCE, " _
                                        & " ID_LOTTO, " _
                                        & " ID_SERVIZIO, " _
                                        & " DESCRIZIONE, " _
                                        & " VALORE_CANONE, " _
                                        & " IVA_CANONE, " _
                                        & " VALORE_CONSUMO, " _
                                        & " IVA_CONSUMO, " _
                                        & " PERC_REVERSIBILITA, " _
                                        & " ID_VOCE_SERVIZIO, " _
                                        & " ID_OLD) " _
                                        & " SELECT SISCOM_MI.SEQ_PF_VOCI_IMPORTO.NEXTVAL, " _
                                        & id & ", " _
                                        & " (SELECT ID " _
                                        & " FROM SISCOM_MI.LOTTI " _
                                        & " WHERE ID_OLD = PF_VOCI_IMPORTO.ID_LOTTO), " _
                                        & " ID_SERVIZIO, " _
                                        & " DESCRIZIONE, " _
                                        & " 0, " _
                                        & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CANONE)) AS IVA_CANONE, " _
                                        & " 0, " _
                                        & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CONSUMO)) AS IVA_CONSUMO, " _
                                        & " PERC_REVERSIBILITA, " _
                                        & " (SELECT ID " _
                                        & " FROM SISCOM_MI.TAB_SERVIZI_VOCI " _
                                        & " WHERE ID_OLD = PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO), " _
                                        & " ID " _
                                        & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                        & " WHERE ID_VOCE_SERVIZIO IN (SELECT ID FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE QUOTA_PREVENTIVA=0 ) AND ID_VOCE = " & idOld
                                    par.cmd.ExecuteNonQuery()
                                    par.cmd.CommandText = " INSERT INTO SISCOM_MI.PF_VOCI_IMPORTO (ID, " _
                                        & " ID_VOCE, " _
                                        & " ID_LOTTO, " _
                                        & " ID_SERVIZIO, " _
                                        & " DESCRIZIONE, " _
                                        & " VALORE_CANONE, " _
                                        & " IVA_CANONE, " _
                                        & " VALORE_CONSUMO, " _
                                        & " IVA_CONSUMO, " _
                                        & " PERC_REVERSIBILITA, " _
                                        & " ID_VOCE_SERVIZIO, " _
                                        & " ID_OLD) " _
                                        & " SELECT SISCOM_MI.SEQ_PF_VOCI_IMPORTO.NEXTVAL, " _
                                        & id & ", " _
                                        & " (SELECT ID " _
                                        & " FROM SISCOM_MI.LOTTI " _
                                        & " WHERE ID_OLD = PF_VOCI_IMPORTO.ID_LOTTO), " _
                                        & " ID_SERVIZIO, " _
                                        & " DESCRIZIONE, " _
                                        & " ROUND(VALORE_CANONE*(1+(" & percentuale & "/100)),2), " _
                                        & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CANONE)) AS IVA_CANONE, " _
                                        & " ROUND(VALORE_CONSUMO*(1+(" & percentuale & "/100)),2), " _
                                        & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CONSUMO)) AS IVA_CONSUMO, " _
                                        & " PERC_REVERSIBILITA, " _
                                        & " (SELECT ID " _
                                        & " FROM SISCOM_MI.TAB_SERVIZI_VOCI " _
                                        & " WHERE ID_OLD = PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO), " _
                                        & " ID " _
                                        & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                        & " WHERE ID_VOCE_SERVIZIO IN (SELECT ID FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE QUOTA_PREVENTIVA=1 ) AND ID_VOCE = " & idOld
                                    par.cmd.ExecuteNonQuery()
                            End Select
                        Catch ex As Exception

                        End Try
                        

                        Try
                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.PF_VOCI_STRUTTURA (ID_VOCE, " _
                                                        & " ID_STRUTTURA, " _
                                                        & " VALORE_NETTO, " _
                                                        & " COMPLETO, " _
                                                        & " IVA, " _
                                                        & " VALORE_LORDO, " _
                                                        & " COMPLETO_ALER, " _
                                                        & " VALORE_LORDO_ALER, " _
                                                        & " COMPLETO_COMUNE) " _
                                                        & " SELECT " & id & ", " _
                                                        & " ID_STRUTTURA, " _
                                                        & " ROUND(" _
                                                        & " (SELECT SUM(NVL(ROUND(PF_VOCI_IMPORTO.VALORE_CANONE,2),0))  " _
                                                        & " FROM SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.LOTTI WHERE PF_VOCI_IMPORTO.ID_LOTTO=LOTTI.ID  " _
                                                        & " AND LOTTI.ID_FILIALE= PF_VOCI_sTRUTTURA.ID_STRUTTURA AND PF_VOCI_IMPORTO.ID_VOCE=" & id & ") " _
                                                        & ",2), " _
                                                        & " 0, " _
                                                        & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA)) AS IVA, " _
                                                        & " ROUND(" _
                                                        & " (SELECT SUM(NVL(ROUND(PF_VOCI_IMPORTO.VALORE_CANONE*(1+((SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA " _
                                                        & " IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=PF_VOCI_IMPORTO.IVA_CANONE))/100)),2),0))  " _
                                                        & " FROM SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.LOTTI WHERE PF_VOCI_IMPORTO.ID_LOTTO=LOTTI.ID  " _
                                                        & " AND LOTTI.ID_FILIALE= PF_VOCI_sTRUTTURA.ID_STRUTTURA AND PF_VOCI_IMPORTO.ID_VOCE=" & id & ") " _
                                                        & " ,2), " _
                                                        & " 0, " _
                                                        & " 0, " _
                                                        & " 0 " _
                                                        & " FROM SISCOM_MI.PF_VOCI_STRUTTURA " _
                                                        & " WHERE ID_VOCE = " & idOld
                            par.cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try



                    Next
                Else
                    connData.chiudi(False)
                    Return False
                End If

                'duplicazione appalti
                Dim vIdPfMainOld As Integer = idPFAttuale
                Dim vIdPfMainNew As Integer = idPFnew

                par.cmd.CommandText = " SELECT * FROM SISCOM_MI.APPALTI " _
                    & " WHERE ID IN (SELECT MAX(ID) FROM SISCOM_MI.APPALTI " _
                    & " WHERE DATA_FINE >(SELECT FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID=" & idEsercizioFinanziarioAttuale & ") " _
                    & " AND DATA_INIZIO <(SELECT FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID=" & idEsercizioFinanziarioAttuale & ") " _
                    & " AND ID IN (SELECT ID_APPALTO " _
                    & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    & " WHERE ID_PF_VOCE_IMPORTO IN " _
                    & " (SELECT ID " _
                    & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                    & " WHERE ID_VOCE IN (SELECT ID " _
                    & " FROM SISCOM_MI.PF_VOCI " _
                    & " WHERE ID_PIANO_FINANZIARIO =" & vIdPfMainOld & "))) " _
                    & " GROUP BY ID_GRUPPO) " _
                    & " ORDER BY ID ASC "
                da.Dispose()
                dt.Dispose()
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                dt = New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    Dim vIdAppaltoNew As Integer
                    Dim vIdAppaltoOld As Integer
                    Dim vidLotto As Integer
                    Dim vidStruttura As Integer


                    Dim vID As String
                    Dim vID_FORNITORE As String
                    Dim vID_LOTTO As String
                    Dim vNUM_REPERTORIO As String
                    Dim vDATA_REPERTORIO As String
                    Dim vDESCRIZIONE As String
                    Dim vANNO_INIZIO As String
                    Dim vDURATA_MESI As String
                    Dim vONERI_SICUREZZA_CANONE As String
                    Dim vONERI_SICUREZZA_CONSUMO As String
                    Dim vPERC_ONERI_SIC_CAN As String
                    Dim vPERC_ONERI_SIC_CON As String
                    Dim vANNO_RIF_INIZIO As String
                    Dim vANNO_RIF_FINE As String
                    Dim vSAL As String
                    Dim vPENALI As String
                    Dim vCOSTO_GRADO_GIORNO As String
                    Dim vDATA_INIZIO As String
                    Dim vDATA_FINE As String
                    Dim vFONDO_PENALE As String
                    Dim vFONDO_RIT_LEGGE As String
                    Dim vFEQUENZA_PAGAMENTO As String
                    Dim vID_STATO As String
                    Dim vFL_RIT_LEGGE As String
                    Dim vDATA_INIZIO_PAGAMENTO As String
                    Dim vRUP_COGNOME As String
                    Dim vRUP_NOME As String
                    Dim vRUP_TEL As String
                    Dim vRUP_EMAIL As String
                    Dim vDL_COGNOME As String
                    Dim vDL_NOME As String
                    Dim vDL_TEL As String
                    Dim vDL_EMAIL As String
                    Dim vCUP As String
                    Dim vCIG As String
                    Dim vTIPO As String
                    Dim vIdStrutturaNew As String
                    Dim vFL_PENALI As String
                    Dim vID_IBAN As String
                    Dim vTOT_CANONE As String
                    Dim vTOT_CONSUMO As String
                    Dim vID_INDIRIZZO_FORNITORE As String
                    Dim vID_GRUPPO As String
                    Dim vID_TIPO_MODALITA_PAG As String
                    Dim vID_TIPO_PAGAMENTO As String
                    Dim vID_GESTORE_ORDINI As String


                    For Each riga As Data.DataRow In dt.Rows

                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPALTI.NEXTVAL FROM DUAL"
                        LettoreSeq = par.cmd.ExecuteReader
                        If LettoreSeq.Read Then
                            vIdAppaltoNew = par.IfNull(LettoreSeq(0), 0)
                        End If
                        LettoreSeq.Close()
                        vIdAppaltoOld = par.IfNull(riga.Item(0), 0)
                        riga.Item("ID") = vIdAppaltoNew
                        If Not IsDBNull(riga.Item("ID_LOTTO")) Then
                            vidLotto = par.IfNull(riga.Item("ID_LOTTO"), 0)

                            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_OLD=" & vidLotto
                            Dim lettoreLotto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettoreLotto.Read Then
                                riga.Item("ID_LOTTO") = lettoreLotto(0)
                            End If
                            lettoreLotto.Close()
                        End If

                        'modifica struttura
                        vidStruttura = riga.Item("ID_STRUTTURA")
                        vIdStrutturaNew = vidStruttura

                        vID = riga.Item(0)
                        vID_FORNITORE = par.IfNull(riga.Item("ID_FORNITORE"), "NULL")
                        vID_LOTTO = par.IfNull(riga.Item("ID_LOTTO"), "NULL")
                        vNUM_REPERTORIO = par.IfNull(riga.Item("NUM_REPERTORIO"), "")
                        vDATA_REPERTORIO = par.IfNull(riga.Item("DATA_REPERTORIO"), "")
                        vDESCRIZIONE = par.IfNull(riga.Item("DESCRIZIONE"), "")
                        vANNO_INIZIO = par.IfNull(riga.Item("ANNO_INIZIO"), "NULL")
                        vDURATA_MESI = par.IfNull(riga.Item("DURATA_MESI"), "NULL")
                        vONERI_SICUREZZA_CANONE = par.IfNull(riga.Item("ONERI_SICUREZZA_CANONE"), "NULL")
                        vONERI_SICUREZZA_CONSUMO = par.IfNull(riga.Item("ONERI_SICUREZZA_CONSUMO"), "NULL")
                        vPERC_ONERI_SIC_CAN = par.IfNull(riga.Item("PERC_ONERI_SIC_CAN"), "NULL")
                        vPERC_ONERI_SIC_CON = par.IfNull(riga.Item("PERC_ONERI_SIC_CON"), "NULL")
                        vANNO_RIF_INIZIO = par.IfNull(riga.Item("ANNO_RIF_INIZIO"), "NULL")
                        vANNO_RIF_FINE = par.IfNull(riga.Item("ANNO_RIF_FINE"), "NULL")
                        vSAL = par.IfNull(riga.Item("SAL"), "NULL")
                        vPENALI = par.IfNull(riga.Item("PENALI"), "")
                        vCOSTO_GRADO_GIORNO = par.IfNull(riga.Item("COSTO_GRADO_GIORNO"), "NULL")
                        vDATA_INIZIO = par.IfNull(riga.Item("DATA_INIZIO"), "")
                        vDATA_FINE = par.IfNull(riga.Item("DATA_FINE"), "")
                        vFONDO_PENALE = par.IfNull(riga.Item("FONDO_PENALE"), "NULL")
                        vFONDO_RIT_LEGGE = par.IfNull(riga.Item("FONDO_RIT_LEGGE"), "NULL")
                        vFEQUENZA_PAGAMENTO = par.IfNull(riga.Item("FEQUENZA_PAGAMENTO"), "NULL")
                        vID_STATO = par.IfNull(riga.Item("ID_STATO"), "NULL")
                        vFL_RIT_LEGGE = par.IfNull(riga.Item("FL_RIT_LEGGE"), "NULL")
                        vDATA_INIZIO_PAGAMENTO = par.IfNull(riga.Item("DATA_INIZIO_PAGAMENTO"), "")
                        vRUP_COGNOME = par.IfNull(riga.Item("RUP_COGNOME"), "")
                        vRUP_NOME = par.IfNull(riga.Item("RUP_NOME"), "")
                        vRUP_TEL = par.IfNull(riga.Item("RUP_TEL"), "")
                        vRUP_EMAIL = par.IfNull(riga.Item("RUP_EMAIL"), "")
                        vDL_COGNOME = par.IfNull(riga.Item("DL_COGNOME"), "")
                        vDL_NOME = par.IfNull(riga.Item("DL_NOME"), "")
                        vDL_TEL = par.IfNull(riga.Item("DL_TEL"), "")
                        vDL_EMAIL = par.IfNull(riga.Item("DL_EMAIL"), "")
                        vCUP = par.IfNull(riga.Item("CUP"), "")
                        vCIG = par.IfNull(riga.Item("CIG"), "")
                        vTIPO = par.IfNull(riga.Item("TIPO"), "")
                        vFL_PENALI = par.IfNull(riga.Item("FL_PENALI"), "NULL")
                        vID_IBAN = par.IfNull(riga.Item("ID_IBAN"), "NULL")
                        vTOT_CANONE = par.IfNull(riga.Item("TOT_CANONE"), "NULL")
                        vTOT_CONSUMO = par.IfNull(riga.Item("TOT_CONSUMO"), "NULL")
                        vID_INDIRIZZO_FORNITORE = par.IfNull(riga.Item("ID_INDIRIZZO_FORNITORE"), "NULL")
                        vID_GRUPPO = par.IfNull(riga.Item("ID_GRUPPO"), "NULL")
                        vID_TIPO_MODALITA_PAG = par.IfNull(riga.Item("ID_TIPO_MODALITA_PAG"), "NULL")
                        vID_TIPO_PAGAMENTO = par.IfNull(riga.Item("ID_TIPO_PAGAMENTO"), "NULL")
                        vID_GESTORE_ORDINI = par.IfNull(riga.Item("ID_GESTORE_ORDINI"), "NULL")


                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPALTI (ID, " _
                            & " ID_FORNITORE, " _
                            & " ID_LOTTO, " _
                            & " NUM_REPERTORIO, " _
                            & " DATA_REPERTORIO, " _
                            & " DESCRIZIONE, " _
                            & " ANNO_INIZIO, " _
                            & " DURATA_MESI, " _
                            & " ONERI_SICUREZZA_CANONE, " _
                            & " ONERI_SICUREZZA_CONSUMO, " _
                            & " PERC_ONERI_SIC_CAN, " _
                            & " PERC_ONERI_SIC_CON, " _
                            & " ANNO_RIF_INIZIO, " _
                            & " ANNO_RIF_FINE, " _
                            & " SAL, " _
                            & " PENALI, " _
                            & " COSTO_GRADO_GIORNO, " _
                            & " DATA_INIZIO, " _
                            & " DATA_FINE, " _
                            & " FONDO_PENALE, " _
                            & " FONDO_RIT_LEGGE, " _
                            & " FEQUENZA_PAGAMENTO, " _
                            & " ID_STATO, " _
                            & " FL_RIT_LEGGE, " _
                            & " DATA_INIZIO_PAGAMENTO, " _
                            & " RUP_COGNOME, " _
                            & " RUP_NOME, " _
                            & " RUP_TEL, " _
                            & " RUP_EMAIL, " _
                            & " DL_COGNOME, " _
                            & " DL_NOME, " _
                            & " DL_TEL, " _
                            & " DL_EMAIL, " _
                            & " CUP, " _
                            & " CIG, " _
                            & " TIPO, " _
                            & " ID_STRUTTURA, " _
                            & " FL_PENALI, " _
                            & " ID_IBAN, " _
                            & " TOT_CANONE, " _
                            & " TOT_CONSUMO, " _
                            & " ID_INDIRIZZO_FORNITORE, " _
                            & " ID_GRUPPO, " _
                            & " ID_TIPO_MODALITA_PAG, " _
                            & " ID_TIPO_PAGAMENTO, " _
                            & " ID_GESTORE_ORDINI " _
                            & " ) " _
                            & " VALUES (" & vID & ", " _
                            & vID_FORNITORE & ", " _
                            & vID_LOTTO & ", " _
                            & "'" & Replace(vNUM_REPERTORIO, "'", "''") & "', " _
                            & "'" & Replace(vDATA_REPERTORIO, "'", "''") & "', " _
                            & "'" & Replace(vDESCRIZIONE, "'", "''") & "', " _
                            & vANNO_INIZIO & ", " _
                            & vDURATA_MESI & ", " _
                            & par.VirgoleInPunti(vONERI_SICUREZZA_CANONE) & ", " _
                            & par.VirgoleInPunti(vONERI_SICUREZZA_CONSUMO) & ", " _
                            & par.VirgoleInPunti(vPERC_ONERI_SIC_CAN) & ", " _
                            & par.VirgoleInPunti(vPERC_ONERI_SIC_CON) & ", " _
                            & vANNO_RIF_INIZIO & ", " _
                            & vANNO_RIF_FINE & ", " _
                            & vSAL & ", " _
                            & "'" & Replace(vPENALI, "'", "''") & "', " _
                            & par.VirgoleInPunti(vCOSTO_GRADO_GIORNO) & ", " _
                            & "'" & Replace(vDATA_INIZIO, "'", "''") & "', " _
                            & "'" & Replace(vDATA_FINE, "'", "''") & "', " _
                            & par.VirgoleInPunti(vFONDO_PENALE) & ", " _
                            & par.VirgoleInPunti(vFONDO_RIT_LEGGE) & ", " _
                            & vFEQUENZA_PAGAMENTO & ", " _
                            & vID_STATO & ", " _
                            & vFL_RIT_LEGGE & ", " _
                            & "'" & Replace(vDATA_INIZIO_PAGAMENTO, "'", "''") & "', " _
                            & "'" & Replace(vRUP_COGNOME, "'", "''") & "', " _
                            & "'" & Replace(vRUP_NOME, "'", "''") & "', " _
                            & "'" & Replace(vRUP_TEL, "'", "''") & "', " _
                            & "'" & Replace(vRUP_EMAIL, "'", "''") & "', " _
                            & "'" & Replace(vDL_COGNOME, "'", "''") & "', " _
                            & "'" & Replace(vDL_NOME, "'", "''") & "', " _
                            & "'" & Replace(vDL_TEL, "'", "''") & "', " _
                            & "'" & Replace(vDL_EMAIL, "'", "''") & "', " _
                            & "'" & Replace(vCUP, "'", "''") & "', " _
                            & "'" & Replace(vCIG, "'", "''") & "', " _
                            & "'" & Replace(vTIPO, "'", "''") & "', " _
                            & vIdStrutturaNew & ", " _
                            & vFL_PENALI & ", " _
                            & vID_IBAN & ", " _
                            & par.VirgoleInPunti(vTOT_CANONE) & ", " _
                            & par.VirgoleInPunti(vTOT_CONSUMO) & ", " _
                            & vID_INDIRIZZO_FORNITORE & ", " _
                            & vID_GRUPPO & ", " _
                            & vID_TIPO_MODALITA_PAG & ", " _
                            & vID_TIPO_PAGAMENTO & ", " _
                            & vID_GESTORE_ORDINI & " " _
                            & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPALTI_EL_PREZZI " _
                            & " (ID, ID_APPALTO, DESCRIZIONE) SELECT SISCOM_MI.SEQ_APPALTI_EL_PREZZI.NEXTVAL," _
                            & vIdAppaltoNew & ", DESCRIZIONE FROM SISCOM_MI.APPALTI_EL_PREZZI WHERE ID_APPALTO=" & vIdAppaltoOld
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPALTI_IBAN " _
                            & " (ID_APPALTO, ID_IBAN) SELECT " & vIdAppaltoNew & ", ID_IBAN " _
                            & " FROM SISCOM_MI.APPALTI_IBAN WHERE ID_APPALTO =" & vIdAppaltoOld
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                            & " (ID_APPALTO, ID_EDIFICIO, ID_IMPIANTO) SELECT " & vIdAppaltoNew & ", ID_EDIFICIO, " _
                            & " ID_IMPIANTO FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & vIdAppaltoOld
                        par.cmd.ExecuteNonQuery()


                        par.cmd.CommandText = " SELECT * " _
                            & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                            & " WHERE ID_APPALTO = " & vIdAppaltoOld _
                            & " AND ID_PF_VOCE_IMPORTO IN " _
                            & " (SELECT ID " _
                            & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                            & " WHERE ID_VOCE IN (SELECT ID " _
                            & " FROM SISCOM_MI.PF_VOCI " _
                            & " WHERE ID_PIANO_FINANZIARIO =" & vIdPfMainOld & ")) "

                        Dim daAL As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtAL As New Data.DataTable
                        daAL.Fill(dtAL)
                        daAL.Dispose()

                        If dtAL.Rows.Count > 0 Then

                            Dim vALIdPfVoceImportoNew As String = ""
                            Dim vALIdPfVoceImporto As String
                            Dim vALIMPORTO_CANONE As String
                            Dim vALSCONTO_CANONE As String
                            Dim vALIVA_CANONE As String
                            Dim vALIMPORTO_CONSUMO As String
                            Dim vALSCONTO_CONSUMO As String
                            Dim vALIVA_CONSUMO As String
                            Dim vALRESIDUO_CONSUMO As String
                            Dim vALONERI_SICUREZZA_CANONE As String
                            Dim vALONERI_SICUREZZA_CONSUMO As String
                            Dim vALPERC_ONERI_SIC_CAN As String
                            Dim vALPERC_ONERI_SIC_CON As String
                            Dim vALFREQUENZA_PAGAMENTO As String
                            Dim lettoreAL As Oracle.DataAccess.Client.OracleDataReader
                            For Each rigaAL As Data.DataRow In dtAL.Rows
                                vALIdPfVoceImporto = rigaAL.Item("ID_PF_VOCE_IMPORTO")
                                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                    & " WHERE ID_OLD=" & vALIdPfVoceImporto
                                lettoreAL = par.cmd.ExecuteReader
                                If lettoreAL.Read Then
                                    vALIdPfVoceImportoNew = par.IfNull(lettoreAL(0), 0)
                                End If
                                lettoreAL.Close()

                                vALIMPORTO_CANONE = par.IfNull(rigaAL.Item("IMPORTO_CANONE"), "NULL")
                                vALSCONTO_CANONE = par.IfNull(rigaAL.Item("SCONTO_CANONE"), "NULL")
                                vALIVA_CANONE = par.IfNull(rigaAL.Item("IVA_CANONE"), "NULL")
                                vALIMPORTO_CONSUMO = par.IfNull(rigaAL.Item("IMPORTO_CONSUMO"), "NULL")
                                vALSCONTO_CONSUMO = par.IfNull(rigaAL.Item("SCONTO_CONSUMO"), "NULL")
                                vALIVA_CONSUMO = par.IfNull(rigaAL.Item("IVA_CONSUMO"), "NULL")
                                vALRESIDUO_CONSUMO = par.IfNull(rigaAL.Item("RESIDUO_CONSUMO"), "NULL")
                                vALONERI_SICUREZZA_CANONE = par.IfNull(rigaAL.Item("ONERI_SICUREZZA_CANONE"), "NULL")
                                vALONERI_SICUREZZA_CONSUMO = par.IfNull(rigaAL.Item("ONERI_SICUREZZA_CONSUMO"), "NULL")
                                vALPERC_ONERI_SIC_CAN = par.IfNull(rigaAL.Item("PERC_ONERI_SIC_CAN"), "NULL")
                                vALPERC_ONERI_SIC_CON = par.IfNull(rigaAL.Item("PERC_ONERI_SIC_CON"), "NULL")
                                vALFREQUENZA_PAGAMENTO = par.IfNull(rigaAL.Item("FREQUENZA_PAGAMENTO"), "NULL")



                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                    & " (ID_APPALTO, ID_PF_VOCE_IMPORTO, IMPORTO_CANONE, SCONTO_CANONE, IVA_CANONE, " _
                                    & " IMPORTO_CONSUMO, SCONTO_CONSUMO, IVA_CONSUMO, RESIDUO_CONSUMO, " _
                                    & " ONERI_SICUREZZA_CANONE, ONERI_SICUREZZA_CONSUMO, PERC_ONERI_SIC_CAN, " _
                                    & " PERC_ONERI_SIC_CON, FREQUENZA_PAGAMENTO) " _
                                    & " VALUES (" _
                                    & vIdAppaltoNew & ", " _
                                    & vALIdPfVoceImportoNew & ", " _
                                    & par.VirgoleInPunti(vALIMPORTO_CANONE) & ", " _
                                    & par.VirgoleInPunti(vALSCONTO_CANONE) & ", " _
                                    & "(SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=" & vALIVA_CANONE & ")), " _
                                    & par.VirgoleInPunti(vALIMPORTO_CONSUMO) & ", " _
                                    & par.VirgoleInPunti(vALSCONTO_CONSUMO) & ", " _
                                    & "(SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=" & vALIVA_CONSUMO & ")), " _
                                    & par.VirgoleInPunti(vALRESIDUO_CONSUMO) & ",  " _
                                    & par.VirgoleInPunti(vALONERI_SICUREZZA_CANONE) & ", " _
                                    & par.VirgoleInPunti(vALONERI_SICUREZZA_CONSUMO) & ", " _
                                    & par.VirgoleInPunti(vALPERC_ONERI_SIC_CAN) & ", " _
                                    & par.VirgoleInPunti(vALPERC_ONERI_SIC_CON) & ", " _
                                    & par.VirgoleInPunti(vALFREQUENZA_PAGAMENTO) _
                                    & " )"
                                par.cmd.ExecuteNonQuery()

                            Next


                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_RIT_LEGGE " _
                                & " (ID, ID_APPALTO, ID_MANUTENZIONE, ID_PRENOTAZIONE, ID_PAGAMENTO, IMPORTO) " _
                                & " SELECT ID, " & vIdAppaltoNew & ", ID_MANUTENZIONE, ID_PRENOTAZIONE, ID_PAGAMENTO, " _
                                & " IMPORTO FROM SISCOM_MI.APPALTI_RIT_LEGGE WHERE ID_APPALTO = " & vIdAppaltoOld
                            par.cmd.ExecuteNonQuery()


                            par.cmd.CommandText = "SELECT * " _
                                & " FROM SISCOM_MI.APPALTI_SCADENZE " _
                                & " WHERE ID_APPALTO = " & vIdAppaltoOld _
                                & " AND scadenza>(SELECT FINE FROM siscom_mi.T_ESERCIZIO_FINANZIARIO " _
                                & " WHERE ID=" & idEsercizioFinanziarioAttuale & ")"

                            Dim daAS As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dtAS As New Data.DataTable
                            daAS.Fill(dtAS)
                            daAS.Dispose()


                            If dtAS.Rows.Count > 0 Then
                                Dim vIdPfVoceImportoNew As String
                                Dim vIdPfVoceImporto As String
                                Dim vSCADENZA As String
                                Dim vIMPORTO As String
                                Dim lettorePFVoceImporto As Oracle.DataAccess.Client.OracleDataReader
                                For Each elementi As Data.DataRow In dtAL.Rows
                                    vIdPfVoceImporto = elementi.Item(1)
                                    vSCADENZA = elementi.Item(2)
                                    vIMPORTO = elementi.Item(3)
                                    par.cmd.CommandText = "SELECT iD FROM siscom_mi.PF_VOCI_IMPORTO " _
                                        & " WHERE ID_OLD=" & vIdPfVoceImporto
                                    lettorePFVoceImporto = par.cmd.ExecuteReader
                                    If lettorePFVoceImporto.Read Then
                                        vIdPfVoceImportoNew = lettorePFVoceImporto(0)
                                    End If
                                    lettorePFVoceImporto.Close()
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_SCADENZE " _
                                        & " (ID_APPALTO, ID_PF_VOCE_IMPORTO," _
                                        & " SCADENZA, IMPORTO) VALUES " _
                                        & " (" & vIdAppaltoNew & "," & vIdPfVoceImportoNew _
                                        & " ," & vSCADENZA & "," & vIMPORTO & " )"

                                    par.cmd.ExecuteNonQuery()
                                Next
                            End If
                        Else
                            connData.chiudi(False)
                            Return False
                        End If
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPALTI_VOCI_PF (ID_APPALTO, " _
                            & " ID_PF_VOCE, " _
                            & " IMPORTO_CANONE, " _
                            & " SCONTO_CANONE, " _
                            & " IVA_CANONE, " _
                            & " IMPORTO_CONSUMO, " _
                            & " SCONTO_CONSUMO, " _
                            & " IVA_CONSUMO, " _
                            & " RESIDUO_CONSUMO, " _
                            & " ONERI_SICUREZZA_CANONE, " _
                            & " ONERI_SICUREZZA_CONSUMO, " _
                            & " PERC_ONERI_SIC_CAN, " _
                            & " PERC_ONERI_SIC_CON) " _
                            & " SELECT " & vIdAppaltoNew & ", " _
                            & " (SELECT ID " _
                            & " FROM SISCOM_MI.PF_VOCI " _
                            & " WHERE ID_OLD = ID_PF_VOCE), " _
                            & " IMPORTO_CANONE, " _
                            & " SCONTO_CANONE, " _
                            & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CANONE)) AS IVA_CANONE, " _
                            & " IMPORTO_CONSUMO, " _
                            & " SCONTO_CONSUMO, " _
                            & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CONSUMO)) AS IVA_CONSUMO, " _
                            & " RESIDUO_CONSUMO, " _
                            & " ONERI_SICUREZZA_CANONE, " _
                            & " ONERI_SICUREZZA_CONSUMO, " _
                            & " PERC_ONERI_SIC_CAN, " _
                            & " PERC_ONERI_SIC_CON " _
                            & " FROM SISCOM_MI.APPALTI_VOCI_PF " _
                            & " WHERE ID_APPALTO =" & vIdAppaltoOld
                        par.cmd.ExecuteNonQuery()

                    Next
                End If
            Else
                connData.chiudi(False)
                Return False
                Response.Write("<script>alert('Non è possibile duplicare il piano finanziario!');location.href='../../pagina_home.aspx';</script>")
            End If


            connData.chiudi(True)
            Return True

        Catch ex As Exception
            Session.Item("ERRORE") = "Errore procedura duplicazione:" & ex.Message
            connData.chiudi(False)
            Return False
        End Try
    End Function

    Protected Sub RadioButtonList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        If RadioButtonList1.SelectedValue = 2 Then
            TextBoxPercentuale.Visible = True
            Label1.Visible = True
        Else
            TextBoxPercentuale.Visible = False
            Label1.Visible = False
        End If
    End Sub
End Class
