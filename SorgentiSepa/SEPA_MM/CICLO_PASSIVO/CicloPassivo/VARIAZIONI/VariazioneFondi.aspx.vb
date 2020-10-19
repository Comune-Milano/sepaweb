Imports System.Drawing
Imports System.Windows.Forms
Imports System.Math

Partial Class VariazioneFondi

    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If




        If Session.Item("BP_VARIAZIONI") <> 1 Then
            Response.Write("<script>alert('Operatore non abilitato per questa funzione!');parent.main.location.href('../../pagina_home.aspx');</script>")
            Exit Sub
        End If
        '#########################

        ErroreGen.Value = "0"

        Try

            If Not IsPostBack Then

                ddlStrutture.Items.Clear()
                ddlPrelievo.Items.Clear()
                ddlDestinazione.Items.Clear()
                ddlanno.Items.Clear()
                

                Disabilita()

                txtImporto.Style.Add("text-align", "right")
                txtImportoResiduo.Style.Add("text-align", "right")
                txtImporto.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
                txtImporto.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")


                caricaEserciziFinanziari()
                caricaStrutture()
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

    Protected Sub ddlPrelievo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrelievo.SelectedIndexChanged

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        lblErrore.Text = ""
        lblErrore.Visible = False

        Try
            If ddlPrelievo.SelectedValue = "Seleziona" Then

                txtImporto.Text = ""
                txtImportoResiduo.Text = ""
                Disabilita()

            Else
                'APRO CONNESSIONE
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                'INDIVIDUO IL CAPITOLO DI APPARTENENZA DELLA VOCE
                par.cmd.CommandText = "SELECT ID_CAPITOLO FROM SISCOM_MI.PF_VOCI WHERE ID='" & ddlPrelievo.SelectedValue & "'"
                myReader = par.cmd.ExecuteReader
                Dim capitoloDestinazione As String = ""
                Dim F As Font = New Font("Arial", 9)

                If myReader.Read Then
                    capitoloDestinazione = par.IfNull(myReader(0), "")
                End If
                myReader.Close()


                'VISUALIZZO TUTTE LE VOCI PER LA STRUTTURA DI APPARTENENZA FILTRATE PER CAPITOLO MENO QUELLA SELEZIONATA PER IL PRELIEVO
                par.cmd.CommandText = "SELECT PF_VOCI.* FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_MAIN " _
                    & "WHERE (SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI A WHERE A.ID_VOCE_MADRE = PF_VOCI.ID) = 0 " _
                    & "AND SISCOM_MI.PF_VOCI_STRUTTURA.ID_VOCE=SISCOM_MI.PF_VOCI.ID " _
                    & "AND SISCOM_MI.PF_VOCI_STRUTTURA.ID_STRUTTURA='" & ddlStrutture.SelectedValue & "' " _
                    & "AND SISCOM_MI.PF_VOCI.ID_PIANO_FINANZIARIO=SISCOM_MI.PF_MAIN.ID " _
                    & "AND PF_MAIN.ID_STATO>=5 " _
                    & "AND PF_VOCI.ID<>'" & ddlPrelievo.SelectedValue & "' " _
                    & "AND ID_PIANO_FINANZIARIO='" & ddlanno.SelectedValue & "' " _
                    & "AND ID_CAPITOLO='" & capitoloDestinazione & "' ORDER BY CODICE"

                    myReader = par.cmd.ExecuteReader

                    ddlDestinazione.Items.Clear()
                    ddlDestinazione.Items.Add("Seleziona")
                    While myReader.Read

                        Dim codice As String = par.IfNull(myReader("CODICE"), "")

                        '#### DETERMINO LUNGHEZZA IN PIXEL DELLE STRINGHE CON CODICE DESCRIZIONE E IMPORTO ####
                        Dim returnsizeCodice As Size = TextRenderer.MeasureText(codice, F)
                        Dim lunghezzaCodice As Integer = returnsizeCodice.Width

                        '#### DIMENSIONO IL PADDING CON "."
                        Dim pad As Char = "."c
                    Dim padding As Integer = 84 - lunghezzaCodice
                        codice = codice.PadRight(Len(codice) + CInt(padding / 3), pad)

                        ddlDestinazione.Items.Add(New ListItem(codice & par.IfNull(myReader("DESCRIZIONE"), ""), par.IfNull(myReader("ID"), "")))

                    End While
                    myReader.Close()

                

                If ddlDestinazione.Items.Count <= 1 Then
                    'NON è POSSIBILE EFFETTUARE UNA VARIAZIONE DI FONDI PER LA VOCE SELEZIONATA
                    Disabilita()
                    lblErrore.Text = "Non è possibile effettuare una variazione di fondi per la voce di prelievo selezionata"
                    lblErrore.Visible = True
                Else
                    Abilita()

                    Dim budget As Decimal = 0
                    '####### INDIVIDUAZIONE DEL BUDGET STANZIATO PER LA VOCE #######
                    par.cmd.CommandText = "SELECT ID_VOCE,(NVL(VALORE_LORDO,0) + NVL(ASSESTAMENTO_VALORE_LORDO,0) + NVL(VARIAZIONI,0)) AS BUDGET FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE ='" & ddlPrelievo.SelectedValue & "' AND ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        budget = par.IfNull(myReader("BUDGET"), 0)
                    End If

                    budget = Decimal.Parse(budget)
                    budget = Format(budget, "##,##0.00")


                    '####### SPESE CON IMPORTO PRENOTATO #######
                    par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO_PRENOTATO,0))) " _
                        & "FROM SISCOM_MI.PRENOTAZIONI " _
                        & "WHERE ID_STATO=0 " _
                        & "AND ID_PAGAMENTO IS NULL " _
                        & "AND ID_VOCE_PF='" & ddlPrelievo.SelectedValue & "' " _
                        & "AND ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"
                    Dim spesePrenotate As Decimal = 0
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        spesePrenotate = par.IfNull(myReader(0), 0)
                    End If
                    myReader.Close()

                    spesePrenotate = Decimal.Parse(spesePrenotate)
                    spesePrenotate = Format(spesePrenotate, "##,##0.00")

                    '#### SPESE CON IMPORTO APPROVATO ####
                    Dim speseApprovate As Decimal = 0
                    par.cmd.CommandText = "SELECT TO_CHAR(SUM(NVL(IMPORTO_APPROVATO,0))) " _
                       & "FROM SISCOM_MI.PRENOTAZIONI " _
                       & "WHERE ID_STATO>=1 " _
                       & "AND ID_VOCE_PF='" & ddlPrelievo.SelectedValue & "' " _
                       & "AND ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"

                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then

                        speseApprovate = par.IfNull(myReader(0), 0)

                    End If
                    myReader.Close()

                    speseApprovate = Decimal.Parse(speseApprovate)
                    speseApprovate = Format(speseApprovate, "##,##0.00")

                    Dim speseTotali As Decimal = speseApprovate + spesePrenotate

                    txtImportoResiduo.Text = Format(budget - speseTotali, "##,##0.00")

                    If txtImportoResiduo.Text <= 0 Then
                        'NON CI SONO FONDI SUFFICIENTI PER EFFETTUARE UNA VARIAZIONE
                        Disabilita()
                        lblErrore.Text = "Non sono presenti fondi a sufficienza per effettuare una variazione di fondi per la voce di prelievo selezionata"
                        lblErrore.Visible = True
                        ErroreGen.Value = "1"
                    End If

                End If

                par.OracleConn.Close()


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try




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

        '#### CONTROLLO VOCI DI PRELIEVO E DI DESTINAZIONE #####
        If ddlPrelievo.SelectedValue = "Seleziona" And ddlDestinazione.SelectedValue = "Seleziona" Then
            lblErrore.Visible = True
            lblErrore.Text = "Selezionare una voce di prelievo e una voce di destinazione per la variazione dei fondi"
            ErroreGen.Value = "1"
        ElseIf ddlPrelievo.SelectedValue <> "Seleziona" And ddlDestinazione.SelectedValue = "Seleziona" Then
            lblErrore.Visible = True
            lblErrore.Text = "Selezionare una voce di destinazione per la variazione dei fondi"
            ErroreGen.Value = "1"
        ElseIf ddlPrelievo.SelectedValue = "Seleziona" And ddlDestinazione.SelectedValue <> "Seleziona" Then
            lblErrore.Visible = True
            lblErrore.Text = "Selezionare una voce di prelievo per la variazione dei fondi"
            ErroreGen.Value = "1"
        Else
            'IMPORTO E PRELIEVO SELEZIONATI

        End If
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

            ' SI PROCEDE CON L'AGGIORNAMENTO DELLE VARIAZIONI PER LE DUE VOCI SELEZIONATE
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

                If ddlDestinazione.SelectedValue <> "" And ddlPrelievo.SelectedValue <> "" And IsNumeric(VARIAZIONE) = True And VARIAZIONE > 0 And ddlStrutture.SelectedValue <> "" Then
                    'AGGIORNO LE VARIAZIONI
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET SISCOM_MI.PF_VOCI_STRUTTURA.VARIAZIONI = SISCOM_MI.PF_VOCI_STRUTTURA.VARIAZIONI - '" & VARIAZIONE & "' WHERE SISCOM_MI.PF_VOCI_STRUTTURA.ID_VOCE = '" & ddlPrelievo.SelectedValue & "' AND SISCOM_MI.PF_VOCI_STRUTTURA.ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET SISCOM_MI.PF_VOCI_STRUTTURA.VARIAZIONI = SISCOM_MI.PF_VOCI_STRUTTURA.VARIAZIONI + '" & VARIAZIONE & "' WHERE SISCOM_MI.PF_VOCI_STRUTTURA.ID_VOCE = '" & ddlDestinazione.SelectedValue & "' AND SISCOM_MI.PF_VOCI_STRUTTURA.ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"
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
                    Response.Write("<script>alert('Variazione effettuata correttamente!');location.replace('VariazioneFondi.aspx');</script>")
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'operazione di variazione!');location.replace('VariazioneFondi.aspx');</script>")
                End If
                par.myTrans.Commit()
            Catch ex As Exception
                par.myTrans.Rollback()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Si è verificato un errore durante l\'operazione di variazione!');location.replace('VariazioneFondi.aspx');</script>")
            End Try
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End If

    End Sub

    Protected Sub ddlStrutture_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStrutture.SelectedIndexChanged

        ddlPrelievo.Items.Clear()
        ddlDestinazione.Items.Clear()
        caricaVociPrelievo()
        Disabilita()


    End Sub

    Protected Sub Abilita()

        ddlDestinazione.Enabled = True
        txtImporto.Enabled = True
        txtImportoResiduo.Enabled = True
        btnProcedi.Visible = True

    End Sub

    Protected Sub Disabilita()

        ddlDestinazione.Enabled = False
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
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_VARIAZIONI(ID_OPERATORE,DATA_ORA_evento,COD_EVENTO,MOTIVAZIONE,ID_VOCE_DA,ID_VOCE_A,ID_STRUTTURA,IMPORTO) " _
                & "VALUES('" & Session.Item("ID_OPERATORE") & "','" & Format(Now, "yyyyMMddHHmmss") & "','F177','SPOSTAMENTO FONDI RESIDUI','" & ddlPrelievo.SelectedValue & "','" & ddlDestinazione.SelectedValue & "','" & ddlStrutture.SelectedValue & "','" & Importo & "') "
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

    Protected Sub caricaStrutture()
        Try
            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim myreader As Oracle.DataAccess.Client.OracleDataReader



            'ELENCO TUTTE LE STRUTTURE
            par.cmd.CommandText = "SELECT DISTINCT ID, NOME FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC"
            myreader = par.cmd.ExecuteReader
            While myreader.Read
                ddlStrutture.Items.Add(New ListItem(par.IfNull(myreader("NOME"), ""), par.IfNull(myreader("ID"), "")))
            End While
            myreader.Close()
            'SELEZIONO LA STRUTTURA DI APPARTENENZA
            ddlStrutture.SelectedValue = Session.Item("ID_STRUTTURA")



            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            lblErrore.Text = "Errore nel caricamento delle strutture!"
            lblErrore.Visible = True
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

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
            ''ELENCO TUTTE LE STRUTTURE
            'par.cmd.CommandText = "SELECT DISTINCT ID, NOME FROM SISCOM_MI.TAB_FILIALI"
            'myreader = par.cmd.ExecuteReader
            'While myreader.Read
            '    ddlStrutture.Items.Add(New ListItem(par.IfNull(myreader("NOME"), ""), par.IfNull(myreader("ID"), "")))
            'End While
            'myreader.Close()

            'SELEZIONO LA STRUTTURA DI APPARTENENZA
            'ddlStrutture.SelectedValue = Session.Item("ID_STRUTTURA")


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
                    'VISUALIZZO TUTTE LE VOCI PER LA STRUTTURA DI APPARTENENZA
                    par.cmd.CommandText = "SELECT PF_VOCI.* FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_MAIN " _
                        & "WHERE (SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI A WHERE A.ID_VOCE_MADRE = PF_VOCI.ID) = 0 " _
                        & "AND SISCOM_MI.PF_VOCI_STRUTTURA.ID_VOCE=SISCOM_MI.PF_VOCI.ID " _
                        & "AND SISCOM_MI.PF_VOCI.ID_PIANO_FINANZIARIO=SISCOM_MI.PF_MAIN.ID " _
                        & "AND (PF_MAIN.ID_STATO=5 OR PF_MAIN.ID_STATO=6) " _
                        & "AND SISCOM_MI.PF_VOCI_STRUTTURA.ID_STRUTTURA='" & ddlStrutture.SelectedValue & "' " _
                        & "AND ID_PIANO_FINANZIARIO='" & ddlanno.SelectedValue & "' " _
                        & "ORDER BY CODICE"
                Case "7"
                    'VISUALIZZO TUTTE LE VOCI PER LA STRUTTURA DI APPARTENENZA
                    par.cmd.CommandText = "SELECT PF_VOCI.* FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_MAIN " _
                        & "WHERE (SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI A WHERE A.ID_VOCE_MADRE = PF_VOCI.ID) = 0 " _
                        & "AND SISCOM_MI.PF_VOCI_STRUTTURA.ID_VOCE=SISCOM_MI.PF_VOCI.ID " _
                        & "AND SISCOM_MI.PF_VOCI.ID_PIANO_FINANZIARIO=SISCOM_MI.PF_MAIN.ID " _
                        & "AND PF_MAIN.ID_STATO=7 " _
                        & "AND FL_CC=1 " _
                        & "AND SISCOM_MI.PF_VOCI_STRUTTURA.ID_STRUTTURA='" & ddlStrutture.SelectedValue & "' " _
                        & "AND ID_PIANO_FINANZIARIO='" & ddlanno.SelectedValue & "' " _
                        & "ORDER BY CODICE"
                Case Else
                    Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati!');</script>")
            End Select
            '##############################################################################################################


            '§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            '############# CALCOLO LUNGHEZZA MASSIMA DELLA VOCE ##############
            Dim lunghezzaMaxDescrizione As Integer = 0
            Dim F As Font = New Font("Arial", 9)
            For Each r As Data.DataRow In dt.Rows
                Dim lunghezzaDescrizione As Size = TextRenderer.MeasureText(r.Item("DESCRIZIONE"), F)
                lunghezzaMaxDescrizione = Max(lunghezzaDescrizione.Width, lunghezzaMaxDescrizione)
                'SE LA LUNGHEZZA MASSIMA DELLA DESCRIZIONE SUPERA 370 è NECESSARIO TRONCARE LA VOCE 
                'ALTRIMENTI NON SI VEDONO GLI IMPORTI
                While lunghezzaDescrizione.Width > 370
                    r.Item("DESCRIZIONE") = r.Item("DESCRIZIONE").ToString.Substring(0, r.Item("DESCRIZIONE").ToString.Length - 3)
                    lunghezzaDescrizione = TextRenderer.MeasureText(r.Item("DESCRIZIONE"), F)
                    lunghezzaMaxDescrizione = Max(lunghezzaDescrizione.Width, lunghezzaMaxDescrizione)
                End While
            Next
            lunghezzaMaxDescrizione = Min(370, lunghezzaMaxDescrizione)
            da.Dispose()
            dt.Dispose()
            '§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§

            ddlPrelievo.Items.Clear()
            ddlPrelievo.Items.Add("Seleziona")

            For Each rr As Data.DataRow In dt.Rows

                '§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§
                Dim codice As String = rr.Item("CODICE")
                Dim descrizione As String = rr.Item("DESCRIZIONE")

                '#### DETERMINO LUNGHEZZA IN PIXEL DELLE STRINGHE CON CODICE DESCRIZIONE E IMPORTO ####
                Dim returnsizeCodice As Size = TextRenderer.MeasureText(codice, F)
                Dim lunghezzaCodice As Integer = returnsizeCodice.Width
                Dim returnsizeDescrizione As Size = TextRenderer.MeasureText(descrizione, F)
                Dim lunghezzaDesc As Integer = returnsizeDescrizione.Width

                '#### DIMENSIONO IL PADDING CON "."
                Dim pad As Char = "."c
                Dim padding As Integer = 84 - lunghezzaCodice
                Dim paddingD As Integer = lunghezzaMaxDescrizione + 20 - lunghezzaDesc
                codice = codice.PadRight(Len(codice) + CInt(padding / 3), pad)
                descrizione = descrizione.PadRight(Len(descrizione) + CInt(paddingD / 3), pad)

                '######## CALCOLO IMPORTO ########
                Dim budget As Decimal = 0
                '####### INDIVIDUAZIONE DEL BUDGET STANZIATO PER LA VOCE #######
                par.cmd.CommandText = "SELECT ID_VOCE,(NVL(VALORE_LORDO,0) + NVL(ASSESTAMENTO_VALORE_LORDO,0) + NVL(VARIAZIONI,0)) AS BUDGET FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE ='" & par.IfNull(rr.Item("ID"), "") & "' AND ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"
                Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myreader2.Read Then
                    budget = par.IfNull(myreader2("BUDGET"), 0)
                End If
                myreader2.Close()
                budget = Decimal.Parse(budget)
                budget = Format(budget, "##,##0.00")

                '####### SPESE CON IMPORTO PRENOTATO #######
                par.cmd.CommandText = "SELECT TO_CHAR(SUM(IMPORTO_PRENOTATO)) " _
                    & "FROM SISCOM_MI.PRENOTAZIONI " _
                    & "WHERE ID_STATO=0 " _
                    & "AND ID_PAGAMENTO IS NULL " _
                    & "AND ID_VOCE_PF='" & rr.Item("ID") & "' " _
                    & "AND ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"
                Dim spesePrenotate As Decimal = 0
                myreader2 = par.cmd.ExecuteReader
                If myreader2.Read Then
                    spesePrenotate = par.IfNull(myreader2(0), 0)
                End If
                myreader2.Close()

                spesePrenotate = Decimal.Parse(spesePrenotate)
                spesePrenotate = Format(spesePrenotate, "##,##0.00")

                '#### SPESE CON IMPORTO APPROVATO ####
                Dim speseApprovate As Decimal = 0
                par.cmd.CommandText = "SELECT TO_CHAR(SUM(IMPORTO_APPROVATO)) " _
                   & "FROM SISCOM_MI.PRENOTAZIONI " _
                   & "WHERE ID_STATO>=1 " _
                   & "AND ID_VOCE_PF='" & rr.Item("ID") & "' " _
                   & "AND ID_STRUTTURA = '" & ddlStrutture.SelectedValue & "'"

                myreader2 = par.cmd.ExecuteReader
                If myreader2.Read Then
                    speseApprovate = par.IfNull(myreader2(0), 0)
                End If
                myreader2.Close()

                speseApprovate = Decimal.Parse(speseApprovate)
                speseApprovate = Format(speseApprovate, "##,##0.00")
                Dim speseTotali As Decimal = speseApprovate + spesePrenotate
                Dim IMP As String = Format(budget - speseTotali, "##,##0.00")

                '#### DIMENSIONO IL PADDING PER L'IMPORTO
                Dim LunghezzaImporto As Size = TextRenderer.MeasureText(IMP, F)
                Dim stringa As String = codice & descrizione
                Dim returnsizeStringa As Size = TextRenderer.MeasureText(stringa, F)

                If returnsizeStringa.Width < 480 Then
                    Dim paddingI As Integer = 579 - LunghezzaImporto.Width - returnsizeStringa.Width
                    IMP = IMP.PadLeft(Len(IMP) + CInt(paddingI / 3), pad)
                Else
                    Dim paddingI As Integer = 100 - LunghezzaImporto.Width
                    IMP = IMP.PadLeft(Len(IMP) + CInt(paddingI / 3), pad)
                End If

                '§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§
                ddlPrelievo.Items.Add(New ListItem(codice & descrizione & IMP, rr.Item("ID")))
            Next




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


            '########## CONDIZIONE DI RICERCA DEI PIANI FINANZIARI ##########
            'SELEZIONO TUTTI I PIANI FINANZIARI CHE SONO STATI APPROVATI


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

End Class