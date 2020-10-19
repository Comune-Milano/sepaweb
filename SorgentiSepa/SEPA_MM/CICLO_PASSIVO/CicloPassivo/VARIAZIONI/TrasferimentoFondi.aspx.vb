Imports System.Drawing
Imports System.Windows.Forms
Imports System.Math

Partial Class TrasferimentoFondi

    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        ErroreGen.Value = "0"

        'CONTROLLO BP_VARIAZIONI
        If Session.Item("BP_VARIAZIONI") <> 1 Then
            Response.Write("<script>alert('Operatore non abilitato per questa funzione!');parent.main.location.href('../../pagina_home.aspx');</script>")
            Exit Sub
        End If
        '#########################

        Try

            If Not IsPostBack Then

                ddlStrutture.Items.Clear()
                ddlPrelievo.Items.Clear()
                ddlStruttureDestinazione.Items.Clear()
                ddlanno.Items.Clear()
                Disabilita()

                txtImporto.Style.Add("text-align", "right")
                txtImportoResiduo.Style.Add("text-align", "right")
                txtImporto.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
                txtImporto.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")

                caricaEserciziFinanziari()
                caricaVociPrelievo()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click

        Response.Redirect("../../pagina_home.aspx")

    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click


        Dim Loading As String = "<div id=""divLoading5"" Style=""position:absolute;margin: 0px; width: 796px; height: 540px;" _
            & "top: 0px; left: 0px;background-color: #eeeeee;background-image: url('../../../NuoveImm/SfondoMascheraContratti2.jpg');"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        Response.Flush()


        lblErrore.Text = ""
        lblErroreImporto.Text = ""
        lblErrore.Visible = False
        lblErroreImporto.Visible = False

        '#### CONTROLLO IMPORTO DA DESTINARE ####
        If Len(Trim(txtImporto.Text)) = 0 Then
            'IMPORTO NON INSERITO
            lblErroreImporto.Visible = True
            lblErroreImporto.Text = "Importo da destinare obbligatorio"
            ErroreGen.Value = "1"
        Else
            If CDec(txtImporto.Text) = 0 Then
                'IMPORTO 0
                lblErroreImporto.Visible = True
                lblErroreImporto.Text = "Inserire un importo diverso da 0"
                ErroreGen.Value = "1"
            ElseIf CDec(txtImporto.Text) < 0 Then
                'IMPORTO NEGATIVO
                lblErroreImporto.Visible = True
                lblErroreImporto.Text = "L'importo da destinare non può essere negativo"
                ErroreGen.Value = "1"
            End If
        End If

        ''#### CONTROLLO VOCI DI PRELIEVO E DI DESTINAZIONE #####
        'If ddlPrelievo.SelectedValue = "Seleziona" And ddlDestinazione.SelectedValue = "Seleziona" Then
        '    lblErrore.Visible = True
        '    lblErrore.Text = "Selezionare una voce di prelievo e una voce di destinazione per la variazione dei fondi"
        '    ErroreGen.Value = "1"
        'ElseIf ddlPrelievo.SelectedValue <> "Seleziona" And ddlDestinazione.SelectedValue = "Seleziona" Then
        '    lblErrore.Visible = True
        '    lblErrore.Text = "Selezionare una voce di destinazione per la variazione dei fondi"
        '    ErroreGen.Value = "1"
        'ElseIf ddlPrelievo.SelectedValue = "Seleziona" And ddlDestinazione.SelectedValue <> "Seleziona" Then
        '    lblErrore.Visible = True
        '    lblErrore.Text = "Selezionare una voce di prelievo per la variazione dei fondi"
        '    ErroreGen.Value = "1"
        'Else
        '    'IMPORTO E PRELIEVO SELEZIONATI

        'End If

        Dim importoDaDestinare As Decimal = 0
        Dim importoResiduo As Decimal = 0

        If txtImporto.Text = "" Then
            importoDaDestinare = 0
        Else
            importoDaDestinare = CDec(txtImporto.Text)
        End If

        If txtImportoResiduo.Text = "" Then
            importoResiduo = 0
        Else
            importoResiduo = CDec(txtImportoResiduo.Text)
        End If

        If importoDaDestinare > importoResiduo Then
            lblErrore.Visible = True
            lblErrore.Text = "L'importo da destinare non può essere superiore all'importo residuo"
            ErroreGen.Value = "1"

        End If

        If ErroreGen.Value <> "1" Then

            'SI PROCEDE CON L'AGGIORNAMENTO DELLE VARIAZIONI PER LE DUE VOCI SELEZIONATE
            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Try
                'INIZIO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                Dim VARIAZIONE As Decimal = txtImporto.Text

                If ddlPrelievo.SelectedValue <> "Seleziona" And ddlStrutture.SelectedValue <> "Seleziona" And IsNumeric(VARIAZIONE) = True And VARIAZIONE > 0 And ddlStruttureDestinazione.SelectedValue <> "Seleziona" Then
                    'AGGIORNO LE VARIAZIONI
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET SISCOM_MI.PF_VOCI_STRUTTURA.VARIAZIONI = SISCOM_MI.PF_VOCI_STRUTTURA.VARIAZIONI - '" & VARIAZIONE & "' " _
                        & "WHERE SISCOM_MI.PF_VOCI_STRUTTURA.ID_VOCE = '" & ddlPrelievo.SelectedValue & "' AND SISCOM_MI.PF_VOCI_STRUTTURA.ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET SISCOM_MI.PF_VOCI_STRUTTURA.VARIAZIONI = SISCOM_MI.PF_VOCI_STRUTTURA.VARIAZIONI + '" & VARIAZIONE & "' " _
                        & "WHERE SISCOM_MI.PF_VOCI_STRUTTURA.ID_VOCE = '" & ddlPrelievo.SelectedValue & "' AND SISCOM_MI.PF_VOCI_STRUTTURA.ID_STRUTTURA = '" & ddlStruttureDestinazione.SelectedValue & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = " UPDATE SISCOM_MI.PF_VOCI_STRUTTURA BB " _
                        & " SET VARIAZIONI = " _
                        & " (SELECT SUM (NVL (VARIAZIONI, 0)) " _
                        & " FROM SISCOM_MI.PF_VOCI_STRUTTURA AA " _
                        & " WHERE AA.ID_STRUTTURA = BB.ID_STRUTTURA " _
                        & " AND AA.ID_VOCE IN " _
                        & " (SELECT DISTINCT ID " _
                        & " FROM SISCOM_MI.PF_VOCI " _
                        & " WHERE CONNECT_BY_ISLEAF = 1 " _
                        & " CONNECT BY PRIOR PF_VOCI.ID = PF_VOCI.ID_VOCE_MADRE " _
                        & " START WITH ID = BB.ID_VOCE)) " _
                        & " WHERE (SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI CC WHERE CC.ID_VOCE_MADRE = BB.ID_VOCE) > 0"
                    par.cmd.ExecuteNonQuery()
                    'SCRIVO L'EVENTO
                    WriteEvent(VARIAZIONE)
                    Response.Write("<script>alert('Variazione effettuata correttamente!');location.replace('TrasferimentoFondi.aspx');</script>")
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'operazione di trasferimento fondi!');location.replace('TrasferimentoFondi.aspx');</script>")
                End If
                par.myTrans.Commit()
            Catch ex As Exception
                par.myTrans.Rollback()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Si è verificato un errore durante l\'operazione di trasferimento fondi!');location.replace('TrasferimentoFondi.aspx');</script>")
            End Try
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End If

    End Sub

    Protected Sub ddlStrutture_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStrutture.SelectedIndexChanged
        If ddlStrutture.SelectedValue <> "Seleziona" Then
            CaricaStruttureDestinazione()
            Disabilita()
            ddlStruttureDestinazione.Enabled = True
            ddlStrutture.Enabled = True
            lblErrore.Text = ""
            txtImportoResiduo.Text = ""
            calcolaImportoResiduo()
        Else
            Disabilita()
            ddlStrutture.Enabled = True
            lblErrore.Text = ""
            txtImportoResiduo.Text = ""
        End If

    End Sub

    Protected Sub Abilita()

        txtImporto.Enabled = True
        txtImportoResiduo.Enabled = True
        btnProcedi.Visible = True

    End Sub

    Protected Sub Disabilita()

        ddlStrutture.Enabled = False
        ddlStruttureDestinazione.Enabled = False
        txtImporto.Enabled = False
        txtImportoResiduo.Enabled = False
        btnProcedi.Visible = False
        lblErroreImporto.Text = ""
        lblErroreImporto.Visible = False
        lblErrore.Visible = False
        lblErrore.Text = ""

    End Sub

    Protected Sub WriteEvent(ByVal Importo As Decimal, Optional ByVal Motivazione As String = "")

        Dim ConnOpenNow As Boolean = False

        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_TRASF_FONDI(ID_OPERATORE,DATA_ORA_evento,COD_EVENTO,MOTIVAZIONE,ID_VOCE,ID_STRUTTURA_DA,ID_STRUTTURA_A,IMPORTO) " _
                & "VALUES('" & Session.Item("ID_OPERATORE") & "','" & Format(Now, "yyyyMMddHHmmss") & "','F177','TRASFERIMENTO FONDI TRA STRUTTURE','" & ddlPrelievo.SelectedValue & "','" & ddlStrutture.SelectedValue & "','" & ddlStruttureDestinazione.SelectedValue & "','" & Importo & "') "
            par.cmd.ExecuteNonQuery()

            If ConnOpenNow = True Then

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

        Catch ex As Exception

            If ConnOpenNow = True Then

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

        End Try

    End Sub

    Private Function ConvertiData(ByVal dataIn As String) As String
        Dim dataOut As String = ""
        If Len(dataIn) = 8 Then
            Return Right(dataIn, 2) & "/" & Mid(dataIn, 5, 2) & "/" & Left(dataIn, 4)
        Else
            Return ""
        End If
    End Function

    Protected Sub ddlanno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlanno.SelectedIndexChanged
        txtImportoResiduo.Text = ""
        caricaVociPrelievo()
        Disabilita()

    End Sub

    Private Sub CaricaStrutture()

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader

        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'CARICO TUTTE LE STRUTTURE
            par.cmd.CommandText = "SELECT DISTINCT ID, NOME FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC"
            myReader = par.cmd.ExecuteReader
            ddlStrutture.Items.Clear()
            ddlStrutture.Items.Add("Seleziona")
            While myReader.Read
                ddlStrutture.Items.Add(New ListItem(par.IfNull(myReader("NOME"), ""), par.IfNull(myReader("ID"), "")))
            End While
            myReader.Close()

            'SELEZIONO LA STRUTTURA DI APPARTENENZA DELL'OPERATORE
            ddlStrutture.SelectedValue = "Seleziona"

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaStruttureDestinazione()

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader

        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'CARICO TUTTE LE STRUTTURE
            par.cmd.CommandText = "SELECT DISTINCT ID, NOME FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC"
            myReader = par.cmd.ExecuteReader
            ddlStruttureDestinazione.Items.Clear()
            ddlStruttureDestinazione.Items.Add("Seleziona")
            While myReader.Read
                If ddlStrutture.SelectedValue <> CStr(par.IfNull(myReader("ID"), "")) Then
                    ddlStruttureDestinazione.Items.Add(New ListItem(par.IfNull(myReader("NOME"), ""), par.IfNull(myReader("ID"), "")))
                End If
            End While
            myReader.Close()

            'SELEZIONO LA STRUTTURA DI APPARTENENZA DELL'OPERATORE
            ddlStruttureDestinazione.SelectedValue = "Seleziona"

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub caricaVociPrelievo()
        Try

            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            ddlPrelievo.Items.Clear()

            '############################################## 11/07/2012 ###################################################
            Dim pianoSelezionato As String = ddlanno.SelectedValue
            par.cmd.CommandText = "SELECT ID_STATO FROM SISCOM_MI.PF_MAIN WHERE ID=" & pianoSelezionato
            Dim LettoreStato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim Stato As String = 5
            If LettoreStato.Read Then
                Stato = par.IfNull(LettoreStato(0), "5")
            End If
            LettoreStato.Close()
            Select Case Stato
                Case "5", "6"
                    'ELENCO TUTTE LE VOCI
                    par.cmd.CommandText = "SELECT PF_VOCI.* FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF=1 " _
                        & "AND ID_PIANO_FINANZIARIO='" & ddlanno.SelectedValue & "' " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IS NULL) " _
                        & "ORDER BY CODICE"
                Case "7"
                    par.cmd.CommandText = "SELECT PF_VOCI.* FROM SISCOM_MI.PF_VOCI " _
                        & "WHERE CONNECT_BY_ISLEAF=1 " _
                        & "AND ID_PIANO_FINANZIARIO='" & ddlanno.SelectedValue & "' " _
                        & "AND FL_CC=1 " _
                        & "CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE " _
                        & "START WITH PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IS NULL) " _
                        & "ORDER BY CODICE"
                Case Else
                    Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati!');</script>")
            End Select
            '##############################################################################################################



            ddlPrelievo.Items.Clear()
            ddlPrelievo.Items.Add("Seleziona")
            Dim vociPrelievo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While vociPrelievo.Read
                ddlPrelievo.Items.Add(New ListItem(par.IfNull(vociPrelievo("CODICE"), "") & " - " & par.IfNull(vociPrelievo("DESCRIZIONE"), ""), par.IfNull(vociPrelievo("ID"), "")))
            End While
            vociPrelievo.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            lblErrore.Text = "Errore nel caricamento delle voci!"
            lblErrore.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Protected Sub caricaEserciziFinanziari()
        Try
            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            'SELEZIONO I PIANI FINANZIARI APPROVATI
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                    & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                    & "AND ID_STATO>=5 ORDER BY 1"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReader1.Read
                Dim ANNOINIZIO As String = par.IfNull(myReader1("INIZIO"), "")
                Dim ANNOFINE As String = par.IfNull(myReader1("FINE"), "")
                If Len(ANNOINIZIO) = 8 And Len(ANNOFINE) = 8 Then
                    ANNOINIZIO = ConvertiData(ANNOINIZIO)
                    ANNOFINE = ConvertiData(ANNOFINE)
                    ddlanno.Items.Add(New ListItem(ANNOINIZIO & " - " & ANNOFINE, par.IfNull(myReader1("ID"), 0)))
                    If par.IfNull(myReader1("ID_STATO"), 0) = 5 Then
                        ddlanno.SelectedValue = par.IfNull(myReader1("ID"), 0)
                    End If
                Else
                    'ERRORE USCIRE DALLA PAGINA
                    Response.Write("<script>alert('Si è verificato un errore durante il caricamento degli esercizi finanziari.');location.replace('../../pagina_home.aspx');</script>")
                End If

            End While

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            lblErrore.Text = "Errore nel caricamento degli esercizi finanziari!"
            lblErrore.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Protected Sub ddlStruttureDestinazione_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStruttureDestinazione.SelectedIndexChanged
        If ddlStruttureDestinazione.SelectedValue <> "Seleziona" And ddlPrelievo.SelectedValue <> "Seleziona" And ddlStrutture.SelectedValue <> "Seleziona" Then
            Abilita()
        Else
            txtImporto.Enabled = False
            btnProcedi.Enabled = False
        End If
    End Sub

    Protected Sub ddlPrelievo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrelievo.SelectedIndexChanged
        If ddlPrelievo.SelectedValue <> "Seleziona" Then
            CaricaStrutture()
            ddlStrutture.Enabled = True
            ddlStruttureDestinazione.Enabled = False
            txtImportoResiduo.Text = ""
            lblErrore.Text = ""
        Else
            ddlStrutture.Items.Clear()
            ddlStruttureDestinazione.Items.Clear()
            Disabilita()
            txtImportoResiduo.Text = ""
            lblErrore.Text = ""
        End If
    End Sub

    Protected Sub calcolaImportoResiduo()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim myreader As Oracle.DataAccess.Client.OracleDataReader
            Dim budget As Decimal = 0
            '####### INDIVIDUAZIONE DEL BUDGET STANZIATO PER LA VOCE #######
            par.cmd.CommandText = "SELECT ID_VOCE,(NVL(VALORE_LORDO,0) + NVL(ASSESTAMENTO_VALORE_LORDO,0) + NVL(VARIAZIONI,0)) AS " _
                & "BUDGET FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE ='" & ddlPrelievo.SelectedValue & "' AND ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"
            myreader = par.cmd.ExecuteReader
            If myreader.Read Then
                budget = par.IfNull(myreader("BUDGET"), 0)
            End If
            myreader.Close()

            budget = Decimal.Parse(budget)
            '####### SPESE CON IMPORTO PRENOTATO #######
            par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO_PRENOTATO,0))) AS IMPORTO_PRENOTATO " _
                & "FROM SISCOM_MI.PRENOTAZIONI " _
                & "WHERE ID_STATO=0 " _
                & "AND ID_PAGAMENTO IS NULL " _
                & "AND ID_VOCE_PF='" & ddlPrelievo.SelectedValue & "' " _
                & "AND ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"
            Dim spesePrenotate As Decimal = 0
            myreader = par.cmd.ExecuteReader
            If myreader.Read Then
                spesePrenotate = par.IfNull(myreader("IMPORTO_PRENOTATO"), 0)
            End If
            myreader.Close()
            spesePrenotate = Decimal.Parse(spesePrenotate)

            '#### SPESE CON IMPORTO APPROVATO ####
            Dim speseApprovate As Decimal = 0
            par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO_APPROVATO,0))) " _
               & "FROM SISCOM_MI.PRENOTAZIONI " _
               & "WHERE ID_STATO>=1 " _
               & "AND ID_VOCE_PF='" & ddlPrelievo.SelectedValue & "' " _
               & "AND ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"

            myreader = par.cmd.ExecuteReader
            If myreader.Read Then
                speseApprovate = par.IfNull(myreader(0), 0)
            End If
            myreader.Close()

            speseApprovate = Decimal.Parse(speseApprovate)
            Dim speseTotali As Decimal = speseApprovate + spesePrenotate
            txtImportoResiduo.Text = Format(budget - speseTotali, "##,##0.00")

            If txtImportoResiduo.Text <= 0 Then
                'NON CI SONO FONDI SUFFICIENTI PER EFFETTUARE UNA VARIAZIONE
                Disabilita()
                ddlStrutture.Enabled = True
                lblErrore.Text = "Non sono presenti fondi a sufficienza per effettuare una variazione di fondi per la voce di prelievo selezionata"
                lblErrore.Visible = True
                ErroreGen.Value = "1"
            End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

End Class