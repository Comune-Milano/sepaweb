
Imports System.Data
Imports Telerik.Web.UI

Partial Class SPESE_REVERSIBILI_RisultatiModificaManuale
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Public totale As New Generic.List(Of Decimal)
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If controlloProfilo() Then
            If Not IsPostBack Then
                'caricaRisultati()
                reimpostaNumerieDate()
                HFGriglia.Value = DataGridUI.ClientID.ToString.Replace("ctl00", "MasterPage")
            End If
            CType(Master.FindControl("TitoloMaster"), Label).Text = "CDR - Modifica manuale"
            CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
        End If
    End Sub
    Private Function controlloProfilo() As Boolean
        'CONTROLLO DELLA SESSIONE OPERATORE E DELL'ABILITAZIONE ALLE SPESE REVERSIBILI
        If Session.Item("OPERATORE") <> "" Then
            If Session.Item("fl_spese_reversibili") = "0" Then
                Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Operatore non abilitato alla gestione delle spese reversibili")
                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
                Return False
            End If
        Else
            RadWindowManager1.RadAlert("Accesso negato o sessione scaduta! E\' necessario rieseguire il login!", 300, 150, "Attenzione", "", "null")
            Return False
        End If
        If Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = 0 Then
            RadWindowManager1.RadAlert("E\' necessario selezionare una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
            Return False
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub solaLettura()
        ButtonImpostaCarature.Enabled = False
        ButtonRicarica.Enabled = False
    End Sub

    Private Sub reimpostaNumerieDate()
        Try
            For Each riga As GridDataItem In DataGridUI.Items
                CType(riga.FindControl("TextBoxServizi"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(riga.FindControl("TextBoxServizi"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'notnumbersOnlyPositive');AutoDecimal6(this);")
                CType(riga.FindControl("TextBoxEdificio"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(riga.FindControl("TextBoxEdificio"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'notnumbersOnlyPositive');AutoDecimal6(this);")
                CType(riga.FindControl("TextBoxAscensore"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(riga.FindControl("TextBoxAscensore"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'notnumbersOnlyPositive');AutoDecimal6(this);")
                CType(riga.FindControl("TextBoxRiscaldamento"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(riga.FindControl("TextBoxRiscaldamento"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'notnumbersOnlyPositive');AutoDecimal6(this);")
                CType(riga.FindControl("TextBoxMontascale"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(riga.FindControl("TextBoxMontascale"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'notnumbersOnlyPositive');AutoDecimal6(this);")
            Next
        Catch ex As Exception
            Session.Add("ERRORE", ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub ButtonImpostaCarature_Click(sender As Object, e As System.EventArgs) Handles ButtonImpostaCarature.Click
        Try
            connData.apri(True)
            Dim id_unita As String = "0"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim valore_caratura As Decimal = 0
            Dim valore_caratura_old As Decimal = 0
            Dim data_old As String = ""
            Dim dataOdierna As String = Format(Now, "dd/MM/yyyy")
            Dim dataInizioAscensore As Date
            Dim dataInizioRiscaldamento As Date
            Dim dataInizioMontascale As Date
            Dim dataInizioServizi As Date
            Dim dataInizioEdificio As Date
            Dim dataInizioAscensoreMenoUno As Date
            Dim dataInizioRiscaldamentoMenoUno As Date
            Dim dataInizioServiziMenoUno As Date
            Dim dataInizioEdificioMenoUno As Date
            Dim dataInizioMontascaleMenoUno As Date
            Dim controlloData As Boolean = False
            Dim contaCarature As Integer = 0
            Dim contaCaratureModificate As Integer = 0
            For Each riga As GridDataItem In DataGridUI.Items
                id_unita = riga("ID_UNITA").Text
                'determino le carature in corso
                'SERVIZI

                If CType(riga.FindControl("TextBoxServizi"), TextBox).Text <> "" Then
                    valore_caratura = CDec(CType(riga.FindControl("TextBoxServizi"), TextBox).Text)
                    valore_caratura_old = 0
                    data_old = ""

                    'If IsDate(CType(riga.FindControl("TextBoxServiziData"), TextBox).Text) Then
                    '    dataInizioServizi = CType(riga.FindControl("TextBoxServiziData"), TextBox).Text()
                    'Else
                    dataInizioServizi = Format(Now, "dd/MM/yyyy")
                    'End If
                    dataInizioServiziMenoUno = dataInizioServizi.AddDays(-1)
                    par.cmd.CommandText = "SELECT CASE WHEN CARATURE.INIZIO_VALIDITA='10000000' THEN '01/01/2000' ELSE TO_CHAR(TO_DATE(CARATURE.INIZIO_VALIDITA,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA, CARATURE.* FROM SISCOM_MI.CARATURE WHERE ID_UNITA=" & id_unita & " AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=1"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        valore_caratura_old = CDec(par.IfNull(lettore("VALORE_CARATURA"), 0))
                        data_old = par.IfNull(lettore("DATA"), "")
                        If valore_caratura <> valore_caratura_old Then
                            contaCarature += 1
                            If IsDate(data_old) And IsDate(dataInizioServizi) Then
                                'If data_old <= dataInizioServizi And dataInizioServizi <= dataOdierna Then
                                Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG ( " _
                                                    & "    CAMPO, DATA_ORA, ID_OPERATORE,  " _
                                                    & "    ID_OPERAZIONE, ID_PF, VAL_IMPOSTATO,  " _
                                                    & "    VAL_PRECEDENTE)  " _
                                                    & " VALUES ( " _
                                                    & "'CDR SERVIZI COMPLESSO'  /* CAMPO */, " _
                                                    & Tempo & "  /* DATA_ORA */, " _
                                                    & Session.Item("ID_OPERATORE") & "  /* ID_OPERATORE */, " _
                                                    & "30  /* ID_OPERAZIONE */, " _
                                                    & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "  /* ID_PF */, " _
                                                    & par.VirgoleInPunti(valore_caratura) & "  /* VAL_IMPOSTATO */, " _
                                                    & par.VirgoleInPunti(valore_caratura_old) & "  /* VAL_PRECEDENTE */ ) "
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " UPDATE SISCOM_MI.CARATURE SET" _
                                    & " MODIFICA_MANUALE=1, " _
                                    & " FINE_VALIDITA='" & par.AggiustaData(Format(dataInizioServiziMenoUno, "dd/MM/yyyy")) & "' " _
                                    & " WHERE ID_UNITA=" & id_unita _
                                    & " AND FINE_VALIDITA='30000000' " _
                                    & " AND ID_TIPOLOGIA_CARATURA=1"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE (ID,ID_UNITA,ID_TIPOLOGIA_CARATURA," _
                                    & " VALORE_CARATURA,INIZIO_VALIDITA,FINE_VALIDITA,MODIFICA_MANUALE,SUP_NETTA,SUP_CATASTALE) " _
                                    & " VALUES(" _
                                    & " SISCOM_MI.SEQ_CARATURE.NEXTVAL," & id_unita & "," _
                                    & " 1," & par.VirgoleInPunti(valore_caratura) & ",'" & par.AggiustaData(Format(dataInizioServizi, "dd/MM/yyyy")) & "'," _
                                    & " '30000000',1," & par.VirgoleInPunti(par.IfNull(lettore("SUP_NETTA"), 0)) & "," _
                                    & par.VirgoleInPunti(par.IfNull(lettore("SUP_CATASTALE"), 0)) & ")"
                                par.cmd.ExecuteNonQuery()
                                contaCaratureModificate += 1
                                'Else
                                'CType(riga.FindControl("TextBoxServiziData"), TextBox).BackColor = Drawing.Color.Yellow
                                'End If
                            End If
                        End If
                    Else
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE (ID,ID_UNITA,ID_TIPOLOGIA_CARATURA," _
                            & " VALORE_CARATURA,INIZIO_VALIDITA,FINE_VALIDITA,MODIFICA_MANUALE,SUP_NETTA,SUP_CATASTALE) " _
                            & " VALUES(" _
                            & " SISCOM_MI.SEQ_CARATURE.NEXTVAL," & id_unita & "," _
                            & " 1," & par.VirgoleInPunti(valore_caratura) & ",'10000000'," _
                            & " '30000000',1,NULL,NULL)"
                        par.cmd.ExecuteNonQuery()
                        contaCaratureModificate += 1
                    End If
                    lettore.Close()
                End If

                'SERVIZI edificio

                If CType(riga.FindControl("TextBoxEdificio"), TextBox).Text <> "" Then
                    valore_caratura = CDec(CType(riga.FindControl("TextBoxEdificio"), TextBox).Text)
                    valore_caratura_old = 0
                    data_old = ""

                    'If IsDate(CType(riga.FindControl("TextBoxServiziData"), TextBox).Text) Then
                    '    dataInizioServizi = CType(riga.FindControl("TextBoxServiziData"), TextBox).Text()
                    'Else
                    dataInizioEdificio = Format(Now, "dd/MM/yyyy")
                    'End If
                    dataInizioEdificioMenoUno = dataInizioEdificio.AddDays(-1)
                    par.cmd.CommandText = "SELECT CASE WHEN CARATURE.INIZIO_VALIDITA='10000000' THEN '01/01/2000' ELSE TO_CHAR(TO_DATE(CARATURE.INIZIO_VALIDITA,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA, CARATURE.* FROM SISCOM_MI.CARATURE WHERE ID_UNITA=" & id_unita & " AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=4"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        valore_caratura_old = CDec(par.IfNull(lettore("VALORE_CARATURA"), 0))
                        data_old = par.IfNull(lettore("DATA"), "")
                        If valore_caratura <> valore_caratura_old Then
                            contaCarature += 1
                            If IsDate(data_old) And IsDate(dataInizioEdificio) Then
                                'If data_old <= dataInizioServizi And dataInizioServizi <= dataOdierna Then
                                Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG ( " _
                                                    & "    CAMPO, DATA_ORA, ID_OPERATORE,  " _
                                                    & "    ID_OPERAZIONE, ID_PF, VAL_IMPOSTATO,  " _
                                                    & "    VAL_PRECEDENTE)  " _
                                                    & " VALUES ( " _
                                                    & "'CDR SERVIZI EDIFICIO'  /* CAMPO */, " _
                                                    & Tempo & "  /* DATA_ORA */, " _
                                                    & Session.Item("ID_OPERATORE") & "  /* ID_OPERATORE */, " _
                                                    & "31  /* ID_OPERAZIONE */, " _
                                                    & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "  /* ID_PF */, " _
                                                    & par.VirgoleInPunti(valore_caratura) & "  /* VAL_IMPOSTATO */, " _
                                                    & par.VirgoleInPunti(valore_caratura_old) & "  /* VAL_PRECEDENTE */ ) "
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " UPDATE SISCOM_MI.CARATURE SET" _
                                    & " MODIFICA_MANUALE=1, " _
                                    & " FINE_VALIDITA='" & par.AggiustaData(Format(dataInizioEdificioMenoUno, "dd/MM/yyyy")) & "' " _
                                    & " WHERE ID_UNITA=" & id_unita _
                                    & " AND FINE_VALIDITA='30000000' " _
                                    & " AND ID_TIPOLOGIA_CARATURA=4"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE (ID,ID_UNITA,ID_TIPOLOGIA_CARATURA," _
                                    & " VALORE_CARATURA,INIZIO_VALIDITA,FINE_VALIDITA,MODIFICA_MANUALE,SUP_NETTA,SUP_CATASTALE) " _
                                    & " VALUES(" _
                                    & " SISCOM_MI.SEQ_CARATURE.NEXTVAL," & id_unita & "," _
                                    & " 4," & par.VirgoleInPunti(valore_caratura) & ",'" & par.AggiustaData(Format(dataInizioEdificio, "dd/MM/yyyy")) & "'," _
                                    & " '30000000',1," & par.VirgoleInPunti(par.IfNull(lettore("SUP_NETTA"), 0)) & "," _
                                    & par.VirgoleInPunti(par.IfNull(lettore("SUP_CATASTALE"), 0)) & ")"
                                par.cmd.ExecuteNonQuery()
                                contaCaratureModificate += 1
                                'Else
                                'CType(riga.FindControl("TextBoxServiziData"), TextBox).BackColor = Drawing.Color.Yellow
                                'End If
                            End If
                        End If
                    Else
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE (ID,ID_UNITA,ID_TIPOLOGIA_CARATURA," _
                            & " VALORE_CARATURA,INIZIO_VALIDITA,FINE_VALIDITA,MODIFICA_MANUALE,SUP_NETTA,SUP_CATASTALE) " _
                            & " VALUES(" _
                            & " SISCOM_MI.SEQ_CARATURE.NEXTVAL," & id_unita & "," _
                            & " 4," & par.VirgoleInPunti(valore_caratura) & ",'10000000'," _
                            & " '30000000',1,NULL,NULL)"
                        par.cmd.ExecuteNonQuery()
                        contaCaratureModificate += 1
                    End If
                    lettore.Close()
                End If

                'ASCENSORE
                If CType(riga.FindControl("TextBoxAscensore"), TextBox).Text <> "" Then
                    valore_caratura = CDec(CType(riga.FindControl("TextBoxAscensore"), TextBox).Text)
                    valore_caratura_old = 0
                    data_old = ""
                    'If IsDate(CType(riga.FindControl("TextBoxAscensoreData"), TextBox).Text) Then
                    'dataInizioAscensore = CType(riga.FindControl("TextBoxAscensoreData"), TextBox).Text()
                    'Else
                    dataInizioAscensore = Format(Now, "dd/MM/yyyy")
                    'End If
                    dataInizioAscensoreMenoUno = dataInizioAscensore.AddDays(-1)
                    par.cmd.CommandText = "SELECT CASE WHEN CARATURE.INIZIO_VALIDITA='10000000' THEN '01/01/2000' ELSE TO_CHAR(TO_DATE(CARATURE.INIZIO_VALIDITA,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA, CARATURE.* FROM SISCOM_MI.CARATURE WHERE ID_UNITA=" & id_unita & " AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=3"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        valore_caratura_old = CDec(par.IfNull(lettore("VALORE_CARATURA"), 0))
                        data_old = par.IfNull(lettore("DATA"), "")
                        If valore_caratura <> valore_caratura_old Then
                            contaCarature += 1
                            If IsDate(data_old) And IsDate(dataInizioAscensore) Then
                                'If data_old <= dataInizioAscensore And dataInizioAscensore <= dataOdierna Then
                                Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG ( " _
                                                    & "    CAMPO, DATA_ORA, ID_OPERATORE,  " _
                                                    & "    ID_OPERAZIONE, ID_PF, VAL_IMPOSTATO,  " _
                                                    & "    VAL_PRECEDENTE)  " _
                                                    & " VALUES ( " _
                                                    & "'MODIFICA CDR ASCENSORE'  /* CAMPO */, " _
                                                    & Tempo & "  /* DATA_ORA */, " _
                                                    & Session.Item("ID_OPERATORE") & "  /* ID_OPERATORE */, " _
                                                    & "32  /* ID_OPERAZIONE */, " _
                                                    & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "  /* ID_PF */, " _
                                                    & par.VirgoleInPunti(valore_caratura) & "  /* VAL_IMPOSTATO */, " _
                                                    & par.VirgoleInPunti(valore_caratura_old) & "  /* VAL_PRECEDENTE */ ) "
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " UPDATE SISCOM_MI.CARATURE SET" _
                                    & " MODIFICA_MANUALE=1, " _
                                    & " FINE_VALIDITA='" & par.AggiustaData(Format(dataInizioAscensoreMenoUno, "dd/MM/yyyy")) & "' " _
                                    & " WHERE ID_UNITA=" & id_unita _
                                    & " AND FINE_VALIDITA='30000000' " _
                                    & " AND ID_TIPOLOGIA_CARATURA=3"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE (ID,ID_UNITA,ID_TIPOLOGIA_CARATURA," _
                                    & " VALORE_CARATURA,INIZIO_VALIDITA,FINE_VALIDITA,MODIFICA_MANUALE,SUP_NETTA,SUP_CATASTALE) " _
                                    & " VALUES(" _
                                    & " SISCOM_MI.SEQ_CARATURE.NEXTVAL," & id_unita & "," _
                                    & " 3," & par.VirgoleInPunti(valore_caratura) & ",'" & par.AggiustaData(Format(dataInizioAscensore, "dd/MM/yyyy")) & "'," _
                                    & " '30000000',1," & par.VirgoleInPunti(par.IfNull(lettore("SUP_NETTA"), 0)) & "," _
                                    & par.VirgoleInPunti(par.IfNull(lettore("SUP_CATASTALE"), 0)) & ")"
                                par.cmd.ExecuteNonQuery()
                                contaCaratureModificate += 1
                                'Else
                                'CType(riga.FindControl("TextBoxAscensoreData"), TextBox).BackColor = Drawing.Color.Yellow
                                'End If
                            End If
                        End If
                    Else
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE (ID,ID_UNITA,ID_TIPOLOGIA_CARATURA," _
                            & " VALORE_CARATURA,INIZIO_VALIDITA,FINE_VALIDITA,MODIFICA_MANUALE,SUP_NETTA,SUP_CATASTALE) " _
                            & " VALUES(" _
                            & " SISCOM_MI.SEQ_CARATURE.NEXTVAL," & id_unita & "," _
                            & " 3," & par.VirgoleInPunti(valore_caratura) & ",'10000000'," _
                            & " '30000000',1,NULL,NULL)"
                        par.cmd.ExecuteNonQuery()
                        contaCaratureModificate += 1
                    End If
                    lettore.Close()
                End If
                'MONTASCALE
                If CType(riga.FindControl("TextBoxMontascale"), TextBox).Text <> "" Then
                    valore_caratura = CDec(CType(riga.FindControl("TextBoxMontascale"), TextBox).Text)
                    valore_caratura_old = 0
                    data_old = ""
                    'If IsDate(CType(riga.FindControl("TextBoxMontascaleData"), TextBox).Text) Then
                    'dataInizioMontascale = CType(riga.FindControl("TextBoxMontascaleData"), TextBox).Text()
                    'Else
                    dataInizioMontascale = Format(Now, "dd/MM/yyyy")
                    'End If
                    dataInizioMontascaleMenoUno = dataInizioMontascale.AddDays(-1)
                    par.cmd.CommandText = "SELECT CASE WHEN CARATURE.INIZIO_VALIDITA='10000000' THEN '01/01/2000' ELSE TO_CHAR(TO_DATE(CARATURE.INIZIO_VALIDITA,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA, CARATURE.* FROM SISCOM_MI.CARATURE WHERE ID_UNITA=" & id_unita & " AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=5"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        valore_caratura_old = CDec(par.IfNull(lettore("VALORE_CARATURA"), 0))
                        data_old = par.IfNull(lettore("DATA"), "")
                        If valore_caratura <> valore_caratura_old Then
                            contaCarature += 1
                            If IsDate(data_old) And IsDate(dataInizioMontascale) Then
                                'If data_old <= dataInizioMontascale And dataInizioMontascale <= dataOdierna Then
                                Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG ( " _
                                                    & "    CAMPO, DATA_ORA, ID_OPERATORE,  " _
                                                    & "    ID_OPERAZIONE, ID_PF, VAL_IMPOSTATO,  " _
                                                    & "    VAL_PRECEDENTE)  " _
                                                    & " VALUES ( " _
                                                    & "'MODIFICA CDR MONTASCALE'  /* CAMPO */, " _
                                                    & Tempo & "  /* DATA_ORA */, " _
                                                    & Session.Item("ID_OPERATORE") & "  /* ID_OPERATORE */, " _
                                                    & "33  /* ID_OPERAZIONE */, " _
                                                    & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "  /* ID_PF */, " _
                                                    & par.VirgoleInPunti(valore_caratura) & "  /* VAL_IMPOSTATO */, " _
                                                    & par.VirgoleInPunti(valore_caratura_old) & "  /* VAL_PRECEDENTE */ ) "
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " UPDATE SISCOM_MI.CARATURE SET" _
                                    & " MODIFICA_MANUALE=1, " _
                                    & " FINE_VALIDITA='" & par.AggiustaData(Format(dataInizioMontascaleMenoUno, "dd/MM/yyyy")) & "' " _
                                    & " WHERE ID_UNITA=" & id_unita _
                                    & " AND FINE_VALIDITA='30000000' " _
                                    & " AND ID_TIPOLOGIA_CARATURA=5"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE (ID,ID_UNITA,ID_TIPOLOGIA_CARATURA," _
                                    & " VALORE_CARATURA,INIZIO_VALIDITA,FINE_VALIDITA,MODIFICA_MANUALE,SUP_NETTA,SUP_CATASTALE) " _
                                    & " VALUES(" _
                                    & " SISCOM_MI.SEQ_CARATURE.NEXTVAL," & id_unita & "," _
                                    & " 5," & par.VirgoleInPunti(valore_caratura) & ",'" & par.AggiustaData(Format(dataInizioMontascale, "dd/MM/yyyy")) & "'," _
                                    & " '30000000',1," & par.VirgoleInPunti(par.IfNull(lettore("SUP_NETTA"), 0)) & "," _
                                    & par.VirgoleInPunti(par.IfNull(lettore("SUP_CATASTALE"), 0)) & ")"
                                par.cmd.ExecuteNonQuery()
                                contaCaratureModificate += 1
                                'Else
                                'CType(riga.FindControl("TextBoxMontascaleData"), TextBox).BackColor = Drawing.Color.Yellow
                                'End If
                            End If
                        End If
                    Else
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE (ID,ID_UNITA,ID_TIPOLOGIA_CARATURA," _
                            & " VALORE_CARATURA,INIZIO_VALIDITA,FINE_VALIDITA,MODIFICA_MANUALE,SUP_NETTA,SUP_CATASTALE) " _
                            & " VALUES(" _
                            & " SISCOM_MI.SEQ_CARATURE.NEXTVAL," & id_unita & "," _
                            & " 5," & par.VirgoleInPunti(valore_caratura) & ",'10000000'," _
                            & " '30000000',1,NULL,NULL)"
                        par.cmd.ExecuteNonQuery()
                        contaCaratureModificate += 1
                    End If
                    lettore.Close()
                End If
                'RISCALDAMENTO
                If CType(riga.FindControl("TextBoxRiscaldamento"), TextBox).Text <> "" Then
                    valore_caratura = CDec(CType(riga.FindControl("TextBoxRiscaldamento"), TextBox).Text)
                    valore_caratura_old = 0
                    data_old = ""
                    'If IsDate(CType(riga.FindControl("TextBoxRiscaldamentoData"), TextBox).Text) Then
                    'dataInizioRiscaldamento = CType(riga.FindControl("TextBoxRiscaldamentoData"), TextBox).Text()
                    'Else
                    dataInizioRiscaldamento = Format(Now, "dd/MM/yyyy")
                    'End If
                    dataInizioRiscaldamentoMenoUno = dataInizioRiscaldamento.AddDays(-1)
                    par.cmd.CommandText = "SELECT CASE WHEN CARATURE.INIZIO_VALIDITA='10000000' THEN '01/01/2000' ELSE TO_CHAR(TO_DATE(CARATURE.INIZIO_VALIDITA,'yyyyMMdd'),'dd/MM/yyyy') END AS DATA, CARATURE.* FROM SISCOM_MI.CARATURE WHERE ID_UNITA=" & id_unita & " AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=2"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        valore_caratura_old = CDec(par.IfNull(lettore("VALORE_CARATURA"), 0))
                        data_old = par.IfNull(lettore("DATA"), "")
                        If valore_caratura <> valore_caratura_old Then
                            contaCarature += 1
                            If IsDate(data_old) And IsDate(dataInizioRiscaldamento) Then
                                'If data_old <= dataInizioRiscaldamento And dataInizioRiscaldamento <= dataOdierna Then
                                Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG ( " _
                                                    & "    CAMPO, DATA_ORA, ID_OPERATORE,  " _
                                                    & "    ID_OPERAZIONE, ID_PF, VAL_IMPOSTATO,  " _
                                                    & "    VAL_PRECEDENTE)  " _
                                                    & " VALUES ( " _
                                                    & "'MODIFICA CDR RISCALDAMENTO'  /* CAMPO */, " _
                                                    & Tempo & "  /* DATA_ORA */, " _
                                                    & Session.Item("ID_OPERATORE") & "  /* ID_OPERATORE */, " _
                                                    & "34  /* ID_OPERAZIONE */, " _
                                                    & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "  /* ID_PF */, " _
                                                    & par.VirgoleInPunti(valore_caratura) & "  /* VAL_IMPOSTATO */, " _
                                                    & par.VirgoleInPunti(valore_caratura_old) & "  /* VAL_PRECEDENTE */ ) "
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " UPDATE SISCOM_MI.CARATURE SET" _
                                    & " MODIFICA_MANUALE=1, " _
                                    & " FINE_VALIDITA='" & par.AggiustaData(Format(dataInizioRiscaldamentoMenoUno, "dd/MM/yyyy")) & "' " _
                                    & " WHERE ID_UNITA=" & id_unita _
                                    & " AND FINE_VALIDITA='30000000' " _
                                    & " AND ID_TIPOLOGIA_CARATURA=2"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE (ID,ID_UNITA,ID_TIPOLOGIA_CARATURA," _
                                    & " VALORE_CARATURA,INIZIO_VALIDITA,FINE_VALIDITA,MODIFICA_MANUALE,SUP_NETTA,SUP_CATASTALE) " _
                                    & " VALUES(" _
                                    & " SISCOM_MI.SEQ_CARATURE.NEXTVAL," & id_unita & "," _
                                    & " 2," & par.VirgoleInPunti(valore_caratura) & ",'" & par.AggiustaData(Format(dataInizioRiscaldamento, "dd/MM/yyyy")) & "'," _
                                    & " '30000000',1," & par.VirgoleInPunti(par.IfNull(lettore("SUP_NETTA"), 0)) & "," _
                                    & par.VirgoleInPunti(par.IfNull(lettore("SUP_CATASTALE"), 0)) & ")"
                                par.cmd.ExecuteNonQuery()
                                contaCaratureModificate += 1
                                'Else
                                'CType(riga.FindControl("TextBoxRiscaldamentoData"), TextBox).BackColor = Drawing.Color.Yellow
                                'End If
                            End If
                        End If
                    Else
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE (ID,ID_UNITA,ID_TIPOLOGIA_CARATURA," _
                            & " VALORE_CARATURA,INIZIO_VALIDITA,FINE_VALIDITA,MODIFICA_MANUALE,SUP_NETTA,SUP_CATASTALE) " _
                            & " VALUES(" _
                            & " SISCOM_MI.SEQ_CARATURE.NEXTVAL," & id_unita & "," _
                            & " 2," & par.VirgoleInPunti(valore_caratura) & ",'10000000'," _
                            & " '30000000',1," & par.VirgoleInPunti(par.IfNull(lettore("SUP_NETTA"), 0)) & "," _
                            & par.VirgoleInPunti(par.IfNull(lettore("SUP_CATASTALE"), 0)) & ")"
                        par.cmd.ExecuteNonQuery()
                        contaCaratureModificate += 1
                    End If
                    lettore.Close()
                End If
            Next
            connData.chiudi(True)
            Dim testo As String = ""
            If contaCaratureModificate = 1 Then
                testo = " 1 CDR modificato"
            Else
                testo = " " & contaCaratureModificate & " CDR modificati"
            End If
            RadWindowManager1.RadAlert("Modifica CDR terminata:" & testo, 300, 150, "Attenzione", "", "null")
            CType(Master.FindControl("TextBoxSelezionato"), TextBox).Text = "In giallo sono evidenziate i CDR che non hanno subito modifica"
            'caricaRisultati()
            reimpostaNumerieDate()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - " & ex.Message)
            RadWindowManager1.RadAlert("Errore nall\'impostazione dei nuovi coefficienti!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub

    Protected Sub ButtonRicarica_Click(sender As Object, e As System.EventArgs) Handles ButtonRicarica.Click
        ' caricaRisultati()
        reimpostaNumerieDate()
    End Sub

    Protected Sub ButtonNuovaRicerca_Click(sender As Object, e As System.EventArgs) Handles ButtonNuovaRicerca.Click
        Response.Redirect("ModificaManualeCaratura.aspx", True)
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGridUI.AllowPaging = False
        DataGridUI.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGridUI.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                If col.ColumnType.ToUpper <> "GRIDBUTTONCOLUMN" Then
                    dtRecords.Columns.Add(colString)
                End If

            End If
        Next
        For Each row As GridDataItem In DataGridUI.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGridUI.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    If col.ColumnType.ToUpper <> "GRIDBUTTONCOLUMN" Then
                        dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                    End If
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGridUI.Columns
            If col.Visible = True Then
                If col.ColumnType.ToUpper <> "GRIDBUTTONCOLUMN" Then
                    Dim colString As String = col.HeaderText
                    dtRecords.Columns(i).ColumnName = colString
                    i += 1
                End If
            End If
        Next
        Esporta(dtRecords)
        DataGridUI.AllowPaging = True
        DataGridUI.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGridUI.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "CDR", "CDR", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

    Private Sub DataGridUI_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles DataGridUI.NeedDataSource
        Try
            Dim codUI As String = Request.QueryString("CODUI")
            Dim Complesso As String = Request.QueryString("COMPLESSO")
            Dim Edificio As String = Request.QueryString("EDIFICIO")
            Dim Indirizzo As String = Request.QueryString("INDIRIZZO")
            Dim Civico As String = Request.QueryString("CIVICO")
            Dim Interno As String = Request.QueryString("INTERNO")
            Dim Scala As String = Request.QueryString("SCALA")
            Dim Ascensore As String = Request.QueryString("ASCENSORE")
            Dim Tipologia As String = "'" & Replace(Request.QueryString("TIPOLOGIA"), ",", "','") & "'"
            Dim IdPianoFinanziario As String = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            If Not IsNothing(Request.QueryString("PLAN")) Then
                CType(Master, Object).NascondiMenu()
            End If
            connData.apri()
            par.cmd.CommandText = " SELECT CARATURE.ID_UNITA," _
                & " '<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);"">'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>' AS CODICE_UNITA, " _
                & "UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_EXPORT, " _
               & " EDIFICI.DENOMINAZIONE AS EDIFICIO, " _
                & " /*CARATURE.SUP_NETTA AS SUPERFICIE_NETTA, " _
                & " CARATURE.SUP_CATASTALE AS SUPERFICIE_CATASTALE,*/ " _
                & " TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA, " _
                & " SCALE_EDIFICI.DESCRIZIONE AS SCALA, " _
                & " INTERNO AS INTERNO, " _
                & " TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO," _
                & " (SELECT TRIM(TO_CHAR(NVL(MAX(VALORE_CARATURA),0),'999G990D999999')) FROM SISCOM_MI.CARATURE A WHERE A.ID_UNITA=CARATURE.ID_UNITA AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=1) AS CARATURA_SERVIZI, " _
                & " (SELECT TRIM(TO_CHAR(NVL(MAX(VALORE_CARATURA),0),'999G990D999999')) FROM SISCOM_MI.CARATURE A WHERE A.ID_UNITA=CARATURE.ID_UNITA AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=2) AS CARATURA_RISCALDAMENTO, " _
                & " (SELECT TRIM(TO_CHAR(NVL(MAX(VALORE_CARATURA),0),'999G990D999999')) FROM SISCOM_MI.CARATURE A WHERE A.ID_UNITA=CARATURE.ID_UNITA AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=3) AS CARATURA_ASCENSORE, " _
                & " (SELECT TRIM(TO_CHAR(NVL(MAX(VALORE_CARATURA),0),'999G990D999999')) FROM SISCOM_MI.CARATURE A WHERE A.ID_UNITA=CARATURE.ID_UNITA AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=4) AS CARATURA_EDIFICIO, " _
                & " (SELECT TRIM(TO_CHAR(NVL(MAX(VALORE_CARATURA),0),'999G990D999999')) FROM SISCOM_MI.CARATURE A WHERE A.ID_UNITA=CARATURE.ID_UNITA AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=5) AS CARATURA_MONTASCALE, " _
                & " (SELECT CASE WHEN INIZIO_VALIDITA='10000000' THEN '01/01/2000' ELSE TO_CHAR(TO_DATE(INIZIO_VALIDITA,'yyyyMMdd'),'dd/MM/yyyy') END FROM SISCOM_MI.CARATURE A WHERE A.ID_UNITA=CARATURE.ID_UNITA AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=1) AS INIZIO_SERVIZI, " _
                & " (SELECT CASE WHEN INIZIO_VALIDITA='10000000' THEN '01/01/2000' ELSE TO_CHAR(TO_DATE(INIZIO_VALIDITA,'yyyyMMdd'),'dd/MM/yyyy') END FROM SISCOM_MI.CARATURE A WHERE A.ID_UNITA=CARATURE.ID_UNITA AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=2) AS INIZIO_RISCALDAMENTO, " _
                & " (SELECT CASE WHEN INIZIO_VALIDITA='10000000' THEN '01/01/2000' ELSE TO_CHAR(TO_DATE(INIZIO_VALIDITA,'yyyyMMdd'),'dd/MM/yyyy') END FROM SISCOM_MI.CARATURE A WHERE A.ID_UNITA=CARATURE.ID_UNITA AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=3) AS INIZIO_ASCENSORE, " _
                & " (SELECT CASE WHEN INIZIO_VALIDITA='10000000' THEN '01/01/2000' ELSE TO_CHAR(TO_DATE(INIZIO_VALIDITA,'yyyyMMdd'),'dd/MM/yyyy') END FROM SISCOM_MI.CARATURE A WHERE A.ID_UNITA=CARATURE.ID_UNITA AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=4) AS INIZIO_EDIFICIO, " _
                & " (SELECT CASE WHEN INIZIO_VALIDITA='10000000' THEN '01/01/2000' ELSE TO_CHAR(TO_DATE(INIZIO_VALIDITA,'yyyyMMdd'),'dd/MM/yyyy') END FROM SISCOM_MI.CARATURE A WHERE A.ID_UNITA=CARATURE.ID_UNITA AND FINE_VALIDITA='30000000' AND ID_TIPOLOGIA_CARATURA=5) AS INIZIO_MONTASCALE " _
                & " FROM SISCOM_MI.UNITA_IMMOBILIARI, " _
                & " SISCOM_MI.EDIFICI, " _
                & " SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                & " SISCOM_MI.SCALE_EDIFICI, " _
                & " SISCOM_MI.TIPO_LIVELLO_PIANO, " _
                & " SISCOM_MI.CARATURE " _
                & " WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO " _
                & " AND COMPLESSI_IMMOBILIARI.ID = EDIFICI.ID_COMPLESSO " _
                & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD = UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                & " AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) " _
                & " AND TIPO_LIVELLO_PIANO.COD = UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO " _
                & " AND CARATURE.ID_UNITA=UNITA_IMMOBILIARI.ID " _
                & " AND CARATURE.FINE_VALIDITA='30000000' "

            Dim groupby As String = " GROUP BY CARATURE.ID_UNITA,EDIFICI.DENOMINAZIONE, " _
                & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE, " _
                & " TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE, " _
                & " INTERNO, " _
                & " SCALE_EDIFICI.DESCRIZIONE, " _
                & " TIPO_LIVELLO_PIANO.DESCRIZIONE "
            Dim condizione As String = ""
            If Len(codUI) = 17 Then
                condizione &= " AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE like '" & UCase(codUI.Replace("'", "''").Replace("*", "%")) & "'"
            End If
            If Complesso <> "-1" Then
                condizione &= " AND COMPLESSI_IMMOBILIARI.ID =" & Complesso
            End If
            If Edificio <> "-1" Then
                condizione &= " AND EDIFICI.ID =" & Edificio
            End If
            If Interno <> "-1" Then
                condizione &= " AND UNITA_IMMOBILIARI.INTERNO ='" & par.formatoStringaDB(Interno) & "' "
            End If
            If Scala <> "-1" Then
                condizione &= " AND UNITA_IMMOBILIARI.ID_SCALA = " & Scala
            End If
            If Ascensore <> "-1" Then
                If Ascensore = 0 Then
                    condizione = condizione & " AND (EDIFICI.NUM_ASCENSORI = 0 OR EDIFICI.NUM_ASCENSORI IS NULL) "
                ElseIf Ascensore = 1 Then
                    condizione = condizione & " AND EDIFICI.NUM_ASCENSORI > 0 "
                End If
            End If
            If Indirizzo <> "-1" Then
                condizione = condizione & "AND EDIFICI.ID_INDIRIZZO_PRINCIPALE IN (SELECT INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.DESCRIZIONE = '" & Indirizzo & "' "
                If Civico <> "-1" Then
                    condizione = condizione & "AND INDIRIZZI.CIVICO = '" & Civico & "'"
                End If
                condizione = condizione & ")"
            End If
            If Indirizzo <> "-1" Then
                condizione = condizione & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO IN (SELECT INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.DESCRIZIONE = '" & Indirizzo & "' "
                If Civico <> "-1" Then
                    condizione = condizione & "AND INDIRIZZI.CIVICO = '" & Civico & "'"
                End If
                condizione = condizione & ")"
            End If
            If Tipologia <> "''" Then
                condizione &= " AND UNITA_IMMOBILIARI.COD_TIPOLOGIA IN (" & Tipologia & ") "
            End If
            par.cmd.CommandText &= condizione & groupby & " ORDER BY 1"
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            'If dt.Rows.Count > 0 Then
            '    DataGridUI.DataSource = dt
            '    DataGridUI.DataBind()
            'Else
            '    CType(Master.FindControl("LabelContenuto"), Label).Text = "Nessun risultato trovato per i parametri di ricerca impostati"
            'End If
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - " & ex.Message)
            RadWindowManager1.RadAlert("Errore durante la ricerca!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")

        End Try
    End Sub
End Class
