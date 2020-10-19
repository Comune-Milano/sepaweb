Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports System.Data.OleDb

Partial Class Condomini_TabMorosita
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    '*********************************
    Dim sUnita(19) As String
    Dim sDecina(9) As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(CType(Me.Page, Object).vIdCondominio.ToString) Then
                Cerca()
            End If
            If Session.Item("LIVELLO") <> 1 Then

                If Session.Item("ID_CAF") = 6 Then
                    Me.btnPayment.Visible = False
                    Me.btnLettereMav.Visible = True
                Else
                    Me.btnLettereMav.Visible = False
                End If
            End If

            If DirectCast(Me.Page.FindControl("ImgVisibility"), HiddenField).Value <> 1 Then
                Me.btnAdd.Visible = False
                Me.btnVisualizza.Visible = False
                Me.btnDelete.Visible = False
                'Me.btnLettereMav.Visible = False
                Me.btnPayment.Visible = False
                If Session.Item("MOD_CONDOMINIO_MOR") = 0 Then
                    Me.btnLettereMav.Visible = False
                Else
                    Me.btnVisualizza.Visible = True
                End If

            End If
            txtDScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDScadenza.Text = Format(Now, "dd/MM/yyyy")
            'Me.btnVisualizza.Attributes.Add("onclick", "ModificaModalMorosita();")
            'Me.btnAdd.Attributes.Add("onclick", "ApriModalMorosita();")


        End If

    End Sub
    Public Sub Cerca()
        Try
            If Not String.IsNullOrEmpty(CType(Me.Page, Object).vIdCondominio.ToString) Then

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT COND_MOROSITA.ID, (COGNOME ||' '||NOME) AS AMMINISTRATORE, to_char(to_date(DATA_ARRIVO,'yyyymmdd'),'dd/mm/yyyy')as DATA_ARRIVO,to_char(to_date(RIF_DA,'yyyymmdd'),'dd/mm/yyyy')as RIF_DA, to_char(to_date(RIF_A,'yyyymmdd'),'dd/mm/yyyy')as RIF_A ,FL_COMPLETO,'' AS M_MESSAGE FROM SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_MOROSITA WHERE COND_AMMINISTRATORI.ID= ID_AMMINISTRATORE AND ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                Dim dt As New Data.DataTable()
                da.Fill(dt)
                For Each r As Data.DataRow In dt.Rows
                    r.Item("M_MESSAGE") = VisMessEmissione(par.IfNull(r.Item("ID"), "0"))
                Next
                DataGridMorosita.DataSource = dt
                DataGridMorosita.DataBind()


                par.cmd.CommandText = "SELECT ID, IBAN FROM SISCOM_MI.FORNITORI_IBAN WHERE ID_FORNITORE = (SELECT ID_FORNITORE FROM SISCOM_MI.CONDOMINI WHERE ID = " & CType(Me.Page, Object).vIdCondominio & ") AND FORNITORI_IBAN.FL_ATTIVO = 1 "

                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Me.cmbIbanFornitore.Items.Clear()
                While lettore.Read
                    cmbIbanFornitore.Items.Add(New ListItem(par.IfNull(lettore("IBAN"), " "), par.IfNull(lettore("ID"), "")))
                End While
                lettore.Close()


            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMorosita"
        End Try

    End Sub
    Private Function VisMessEmissione(ByVal idMorosita As String) As Integer
        VisMessEmissione = 0

        Try
            If Not String.IsNullOrEmpty(CType(Me.Page, Object).vIdCondominio.ToString) Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                'visMessage è HiddenField settato a 1 di default
                'serve a dire se mostrare il messaggio di conferma per
                'l'emissione dei mav oppure no, in quanto i mav sono stati
                ' già generati, e quindi mostrerà quelli esistenti

                '0 = false (non mostra il messaggio)
                '1 = true (mostra il messaggio) 
                Dim NMorInquilini As Integer
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader
                Dim contaLettere As Integer = 0
                '1. non ci sono righe in cond_morosità lettere = dovrò sicuramente emettere i mav per una questa morosita
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_MOROSITA_LETTERE WHERE ID_MOROSITA = " & idMorosita
                lettore = par.cmd.ExecuteReader
                If lettore.HasRows Then
                    While lettore.Read
                        If par.IfNull(lettore("BOLLETTINO"), "") = "" Then
                            VisMessEmissione = 1
                            Return VisMessEmissione
                            lettore.Close()
                            Exit Function
                        End If
                        contaLettere = contaLettere + 1
                    End While
                Else
                    VisMessEmissione = 1
                    Return VisMessEmissione
                    lettore.Close()
                    Exit Function
                End If

                '2. il numero di morositò in inquilini è diverso da quello delle lettere = dovrò emettere mav per quelli non emessi
                par.cmd.CommandText = "SELECT COUNT(ID_UI) as N_MOR_INQ FROM SISCOM_MI.COND_MOROSITA_INQUILINI WHERE ID_MOROSITA = " & idMorosita
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    NMorInquilini = lettore("N_MOR_INQ")
                End If
                lettore.Close()
                If NMorInquilini <> contaLettere Then
                    VisMessEmissione = 1
                End If

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMorosita"
        End Try
        Return VisMessEmissione
    End Function

    Protected Sub DataGridMorosita_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridMorosita.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TabMorosita1_txtmia').value='Hai selezionato la Morosita dal " & e.Item.Cells(1).Text.Replace("'", "\'") & " al " & e.Item.Cells(2).Text.Replace("'", "\'") & " ';document.getElementById('TabMorosita1_txtidMorosita').value='" & e.Item.Cells(0).Text & "';document.getElementById('TabMorosita1_txtFlCompleta').value='" & e.Item.Cells(3).Text & "';document.getElementById('TabMorosita1_VisMessage').value='" & e.Item.Cells(8).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('TabMorosita1_txtmia').value='Hai selezionato la Morosita dal " & e.Item.Cells(1).Text.Replace("'", "\'") & " al " & e.Item.Cells(2).Text.Replace("'", "\'") & " ';document.getElementById('TabMorosita1_txtidMorosita').value='" & e.Item.Cells(0).Text & "';document.getElementById('TabMorosita1_txtFlCompleta').value='" & e.Item.Cells(3).Text & "';document.getElementById('TabMorosita1_VisMessage').value='" & e.Item.Cells(8).Text & "'")
        End If
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        'If Me.txtidMorosita.Value <> 0 Then
        Cerca()
        '    'Response.Write("<script>window.showModalDialog('RiepGestione.aspx?IDCONDOMINIO= " & CType(Me.Page, Object).vIdCondominio() & "&IDCON=" & CType(Me.Page, Object).vIdConnessione & "&IDGEST=" & Me.txtidGest.Value & "',window, 'status:no;dialogWidth:900px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');</script>")
        txtidMorosita.Value = 0

        '    'Variabile di sessione per sapere se sono state apportate modifiche e salvataggio alle finestre modali.
        If Session("MODIFYMODAL") = 1 Then
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
        End If
        Session.Remove("MODIFYMODAL")
        'End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        Try
            If Me.txtidMorosita.Value <> "" And Me.txtidMorosita.Value <> "0" Then
                If txtConfElimina.Value = 1 Then


                    'salvataggio automatico cnacellazione!
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.myTrans.Commit()

                    'Riapro una nuova transazione
                    Session.Item("LAVORAZIONE") = "1"
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSCOND" & CType(Me.Page, Object).vIdConnessione, par.myTrans)
                    'CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
                    Cerca()
                    Me.txtidMorosita.Value = 0
                Else
                    Me.txtidMorosita.Value = 0
                    txtConfElimina.Value = 0
                End If
            Else
                Response.Write("<script>alert('Selezionare un elemento da eliminare!');</script>")

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMorosita"
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
        Cerca()
        If Session("MODIFYMODAL") = 1 Then
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
        End If
        Session.Remove("MODIFYMODAL")

    End Sub

    Protected Sub btnLettereMav_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLettereMav.Click

        'Response.Write("<script>alert('Funzione non disponibile!');</script>")
        If Me.txtConfMav.Value = 1 Then

            If Me.txtFlCompleta.Value = 0 Then
                Response.Write("<script>alert('Non è possibile emettere i MAV!\nLa Morosità non è ancora completa!');</script>")
                Exit Sub
            End If
            If CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = "" Then

                If Me.txtidMorosita.Value <> "" And Me.txtidMorosita.Value <> "0" Then
                    If Session.Item("MOD_CONDOMINIO_MOR") = 0 Then

                        '*******************RICHIAMO LA CONNESSIONE*********************
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        '*******************RICHIAMO LA TRANSAZIONE*********************
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        ‘‘par.cmd.Transaction = par.myTrans
                    Else
                        '*******************APERURA CONNESSIONE*********************
                        If par.OracleConn.State = Data.ConnectionState.Closed Then
                            par.OracleConn.Open()
                            par.SettaCommand(par)
                        End If
                    End If

                    Dim i As Integer = 0
                    'Dim di As DataGridItem
                    Dim IdAnagrafica As String = ""
                    Dim Emissione As String = Format(Now, "yyyyMMdd")
                    Dim CodContratto As String = ""
                    Dim Importo As String = ""
                    Dim TipoIngiunzione As String = "0"
                    Dim InizioPeriodo As String = ""
                    Dim FinePeriodo As String = ""

                    If CType(Me.Page.FindControl("cmbTipoCond"), DropDownList).SelectedValue <> "C" Then
                        TipoIngiunzione = 1
                    End If

                    par.cmd.CommandText = "SELECT  UNITA_IMMOBILIARI.ID AS ID_UI,COND_MOROSITA_INQUILINI.ID_MOROSITA, " _
                                        & "trim(TO_CHAR(IMPORTO,'9999990D99')) AS IMPORTO, trim(TO_CHAR(VARIAZIONE_COMUNE,'9999990D99')) AS VARIAZIONE_COMUNE," _
                                        & "(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO'  ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO," _
                                        & "siscom_mi.GetIntestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,0) AS INTESTATARIO, " _
                                        & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO, COND_MOROSITA_INQUILINI.ID_INTESTATARIO ,unita_contrattuale.id_contratto, rapporti_utenza.COD_CONTRATTO FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA,SISCOM_MI.COND_MOROSITA_INQUILINI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND' AND  COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) AND (cond_ui.id_condominio=" & CType(Me.Page, Object).vIdCondominio & ") AND ID_INTESTARIO  IS NOT NULL AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio & ") AND COND_UI.ID_UI=COND_MOROSITA_INQUILINI.ID_UI(+) AND COND_MOROSITA_INQUILINI.ID_MOROSITA=" & txtidMorosita.Value & " ORDER BY NOMINATIVO ASC "
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable()

                    da.Fill(dt)

                    par.cmd.CommandText = "SELECT RIF_DA, RIF_A FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & txtidMorosita.Value
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        InizioPeriodo = myReader1("RIF_DA")
                        FinePeriodo = myReader1("RIF_A")
                    End If
                    myReader1.Close()

                    Dim RESPONSABILE As String = ""
                    'par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID = 33"
                    'myReader1 = par.cmd.ExecuteReader()
                    'If myReader1.Read Then
                    '    RESPONSABILE = par.IfNull(myReader1("VALORE"), "")
                    'End If
                    'myReader1.Close()
                    par.cmd.CommandText = "select * from SISCOM_MI.cond_morosita_lettere where id_morosita = " & txtidMorosita.Value
                    myReader1 = par.cmd.ExecuteReader
                    If myReader1.HasRows = False Then


                        For i = 0 To dt.Rows.Count - 1
                            'di = Me.DataGridMorosita.Items(i)
                            IdAnagrafica = dt.Rows(i).Item("ID_INTESTATARIO").ToString
                            CodContratto = dt.Rows(i).Item("COD_CONTRATTO").ToString
                            Importo = CDec(par.IfEmpty(dt.Rows(i).Item("IMPORTO").ToString, 0)) + CDec(par.IfEmpty(dt.Rows(i).Item("VARIAZIONE_COMUNE").ToString, 0))
                            If par.IfEmpty(Importo, "0") > 0 Then
                                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_MOROSITA_LETTERE WHERE ID_ANAGRAFICA = " & IdAnagrafica & " AND ID_MOROSITA = " & txtidMorosita.Value
                                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReader2.HasRows = False Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_MOROSITA_LETTERE (ID,ID_ANAGRAFICA,EMISSIONE,IMPORTO,INIZIO_PERIODO,FINE_PERIODO,COD_CONTRATTO,TIPO_INGIUNZIONE,RESPONSABILE,ID_OPERATORE,ID_MOROSITA)" _
                                    & " VALUES (SISCOM_MI.SEQ_COND_MOROSITA_LETTERE.NEXTVAL," & IdAnagrafica & ", " & Emissione & ", " & par.VirgoleInPunti(par.IfEmpty(Importo, "0.00")) & ", " & InizioPeriodo & ", " & FinePeriodo & "," _
                                    & " '" & CodContratto & "', " & TipoIngiunzione & ", '" & par.PulisciStrSql(RESPONSABILE) & "'," & Session.Item("ID_OPERATORE") & ", " & txtidMorosita.Value & ")"
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    '06/06/2013 commento tutto perchè l'operatore comunale deve inserire la variazione solo prima di stampare le lettere e i mav, 
                                    ' se lo fa dopo riapro il file zip generato.



                                    ' '' ''25/09/2012
                                    '' ''If myReader2.Read Then
                                    '' ''    If Not String.IsNullOrEmpty(par.IfNull(myReader2("BOLLETTINO"), "")) Then
                                    '' ''        'par.cmd.CommandText = "update siscom_mi.bol_bollette set fl_annullata = 1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "' where rif_bollettino = " & par.IfNull(myReader2("BOLLETTINO"), 0)
                                    '' ''        'par.cmd.ExecuteNonQuery()


                                    '' ''    End If
                                    '' ''    'UPDATE
                                    '' ''    '25/10/2012 AGGIORNAMO IL RECORD PRESENTE E TROVATO
                                    '' ''    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA_LETTERE SET COD_CONTRATTO = '" & CodContratto & "',EMISSIONE = " & Emissione & ", " _
                                    '' ''                        & "IMPORTO = " & par.VirgoleInPunti(par.IfEmpty(Importo, "0.00")) & ",INIZIO_PERIODO = '" & InizioPeriodo & "'," _
                                    '' ''                        & "FINE_PERIODO= '" & FinePeriodo & "', TIPO_INGIUNZIONE = " & TipoIngiunzione & ", " _
                                    '' ''                        & "RESPONSABILE ='" & par.PulisciStrSql(RESPONSABILE) & "',ID_OPERATORE = " & Session.Item("ID_OPERATORE") & " " _
                                    '' ''                        & "WHERE ID_MOROSITA = " & txtidMorosita.Value & " AND  ID_ANAGRAFICA = " & IdAnagrafica
                                    '' ''    par.cmd.ExecuteNonQuery()


                                    '' ''    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_MOROSITA_LETTERE (ID,ID_ANAGRAFICA,EMISSIONE,IMPORTO,INIZIO_PERIODO,FINE_PERIODO,COD_CONTRATTO,TIPO_INGIUNZIONE,RESPONSABILE,ID_OPERATORE,ID_MOROSITA)" _
                                    '' ''    '                    & " VALUES (SISCOM_MI.SEQ_COND_MOROSITA_LETTERE.NEXTVAL," & IdAnagrafica & ", " & Emissione & ", " & par.VirgoleInPunti(par.IfEmpty(Importo, "0.00")) & ", " & InizioPeriodo & ", " & FinePeriodo & "," _
                                    '' ''    '                    & " '" & CodContratto & "', " & TipoIngiunzione & ", '" & par.PulisciStrSql(RESPONSABILE) & "'," & Session.Item("ID_OPERATORE") & ", " & txtidMorosita.Value & ")"

                                    '' ''End If
                                End If

                                myReader2.Close()
                            End If

                        Next
                    End If
                    myReader1.Close()


                    If Session.Item("MOD_CONDOMINIO_MOR") = 0 Then
                        par.myTrans.Commit()
                        'Riapro una nuova transazione
                        Session.Item("LAVORAZIONE") = "1"
                        par.myTrans = par.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSCOND" & CType(Me.Page, Object).vIdConnessione, par.myTrans)
                    Else
                        '*********************CHIUSURA CONNESSIONE**********************
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If



                    Response.Write("<script>window.open('../Contratti/Morosita/CreaLettere.aspx?IDMOR=" & txtidMorosita.Value & "&IDCONN=" & CType(Me.Page, Object).vIdConnessione & "','CreaLettere','');</script>")
                    txtidMorosita.Value = 0
                Else
                    Response.Write("<script>alert('Selezionare una Morosità da stampare!');</script>")

                End If
            Else
                Response.Write("<script>alert('Salvare le modifiche apportate prima di effettuare l\'operazione!');</script>")
            End If
        End If

    End Sub

    Protected Sub btnPayment_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPayment.Click


        '05/07/2011 ******************PEPPE MODIFY***************
        '*****INSERITO DIV PER VISUALIZZARE E MODIFICARE LA DESCRIZIONE DEL PAGAMENTO.
        '*****QUINDI COMMENTO IL CODICE ASSOCIATO AL PULSANTE E LO RIMANDO AL CONFERMA
        '*****DEL NUOVO DIV DA DOVE POSSONO MODIFICARE LA DESCRIZIONE DEL PAGAMENTO


        ' '' ''Response.Write("<script>alert('Funzione non disponibile!');</script>")
        '' ''Try
        '' ''    If txtConfpayment.Value = 1 Then
        '' ''        Dim IMPORTO As Double = 0
        '' ''        '*******************APERURA CONNESSIONE*********************
        '' ''        If par.OracleConn.State = Data.ConnectionState.Closed Then
        '' ''            par.OracleConn.Open()
        '' ''            par.SettaCommand(par)
        '' ''        End If

        '' ''        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        '' ''        Dim Rimanente As Double = 0
        '' ''        par.cmd.CommandText = "SELECT NVL(ID_PAGAMENTO,0) AS ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = (SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & txtidMorosita.Value & ")"
        '' ''        myReader1 = par.cmd.ExecuteReader
        '' ''        If myReader1.Read Then
        '' ''            If myReader1("ID_PAGAMENTO") <> 0 Then
        '' ''                Response.Write("<script>alert('ATTENZIONE...Il pagamento della morosità selezionata è stato già emesso!Impossibile procedere!');</script>")
        '' ''                myReader1.Close()
        '' ''                txtidMorosita.Value = 0
        '' ''                Exit Sub
        '' ''            End If
        '' ''        End If
        '' ''        myReader1.Close()
        '' ''        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO = " _
        '' ''                            & "(SELECT PF_MAIN.ID FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID_STATO = 5 " _
        '' ''                            & "AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID AND SUBSTR(INIZIO,0,4) = '" & Format(Now, "yyyymmdd").Substring(0, 4) & "') AND CODICE = '2.02.07.01'"

        '' ''        myReader1 = par.cmd.ExecuteReader()
        '' ''        If myReader1.Read Then
        '' ''            '***controllo che il valore preventivato di spesa esista e sia maggiore di 0
        '' ''            par.cmd.CommandText = "select sum(ASSESTAMENTO_VALORE_LORDO+valore_lordo) as stanziato from siscom_mi.pf_voci_struttura where id_voce =" & par.IfNull(myReader1("ID"), 0)
        '' ''            Dim stanziato As Decimal = 0
        '' ''            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        '' ''            If lettore.Read Then
        '' ''                If CDbl(par.IfNull(lettore("stanziato"), 0)) > 0 Then
        '' ''                    stanziato = par.IfNull(lettore("stanziato"), 0)
        '' ''                    '********SOMMO TUTT I PAGAMENTI EFFETTUATI CON QUELLA VOCE DI PREVENTIVO DI SPESA DEL PIANO FINANZIARIO
        '' ''                    par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_PRENOTATO),0) as TOT_PRENOTATO  FROM SISCOM_MI.PRENOTAZIONI WHERE ID_VOCE_PF = " & myReader1("ID") & " AND ID_PAGAMENTO IS NOT NULL"
        '' ''                    lettore = par.cmd.ExecuteReader
        '' ''                    If lettore.Read Then
        '' ''                        '*******Differenza fra preventivato e importi fino a ora pagati
        '' ''                        Rimanente = CDbl(stanziato) - CDbl(par.IfNull(lettore("TOT_PRENOTATO"), 0))
        '' ''                    End If
        '' ''                Else
        '' ''                    lettore.Close()
        '' ''                    myReader1.Close()
        '' ''                    Response.Write("<script>alert('Importo stanziato pari a zero!Imporssibile procedere');</script>")
        '' ''                    '*********************CHIUSURA CONNESSIONE**********************
        '' ''                    par.OracleConn.Close()
        '' ''                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '' ''                    Exit Sub
        '' ''                End If
        '' ''                lettore.Close()
        '' ''                If Rimanente >= 0 Then
        '' ''                    '*****Modifica 17/05/2011
        '' ''                    'par.cmd.CommandText = "SELECT SUM(IMPORTO)AS IMPORTO FROM SISCOM_MI.COND_MOROSITA_INQUILINI,SISCOM_MI.COND_MOROSITA WHERE ID_MOROSITA = " & Me.txtidMorosita.Value & " AND ID_MOROSITA = COND_MOROSITA.ID "
        '' ''                    par.cmd.CommandText = "SELECT IMPORTO_PRENOTATO AS IMPORTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " _
        '' ''                                        & " (SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & Me.txtidMorosita.Value & " AND ID_CONDOMINIO =" & CType(Me.Page, Object).vIdCondominio & ")"
        '' ''                    lettore = par.cmd.ExecuteReader
        '' ''                    If lettore.Read Then
        '' ''                        IMPORTO = par.IfNull(lettore("IMPORTO"), 0)
        '' ''                    End If
        '' ''                    lettore.Close()

        '' ''                    If IMPORTO > 0 Then
        '' ''                        If Rimanente >= CDbl(IMPORTO) Then
        '' ''                            '******Scrittura del nuovo pagamento nell'apposita tabella!*******
        '' ''                            Dim Pagamento As String = CreaPagamento(myReader1("ID"), IMPORTO)
        '' ''                            Response.Write("<script>alert('Il pagamento è stato emesso e storicizzato!');</script>")
        '' ''                            lettore.Close()
        '' ''                            myReader1.Close()
        '' ''                            '*********************CHIUSURA CONNESSIONE**********************
        '' ''                            par.OracleConn.Close()
        '' ''                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '' ''                            PdfPagamento(Pagamento)
        '' ''                            CType(Me.Page.FindControl("Tab_Pagamenti1"), Object).Cerca()

        '' ''                            Exit Sub
        '' ''                        Else
        '' ''                            Response.Write("<script>alert('L\'ammontare residuo preventivato per questa spesa è insufficiente a eseguirne la liquidazione!');</script>")
        '' ''                            lettore.Close()
        '' ''                            myReader1.Close()
        '' ''                            '*********************CHIUSURA CONNESSIONE**********************
        '' ''                            par.OracleConn.Close()
        '' ''                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        '' ''                            txtidMorosita.Value = 0
        '' ''                            Exit Sub
        '' ''                        End If
        '' ''                    Else
        '' ''                        Response.Write("<script>alert('Impossibile emettere il pagamento!Potrebbe essere stato già emesso o con importo pari a zero!');</script>")
        '' ''                        txtidMorosita.Value = 0
        '' ''                        '*********************CHIUSURA CONNESSIONE**********************
        '' ''                        par.OracleConn.Close()
        '' ''                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '' ''                        myReader1.Close()
        '' ''                        Exit Sub
        '' ''                    End If


        '' ''                Else
        '' ''                    Response.Write("<script>alert('Sono stati esauriti gli importi stanziati nel piano finanziario corrente!!');</script>")
        '' ''                    '*********************CHIUSURA CONNESSIONE**********************
        '' ''                    par.OracleConn.Close()
        '' ''                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '' ''                    txtidMorosita.Value = 0
        '' ''                    lettore.Close()
        '' ''                    myReader1.Close()
        '' ''                    Exit Sub

        '' ''                End If
        '' ''            Else
        '' ''                Response.Write("<script>alert('Nessun importo stanziato per questo tipo di pagamento!');</script>")
        '' ''                txtidMorosita.Value = 0
        '' ''                myReader1.Close()
        '' ''                '*********************CHIUSURA CONNESSIONE**********************
        '' ''                par.OracleConn.Close()
        '' ''                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '' ''                Exit Sub

        '' ''            End If
        '' ''        Else
        '' ''            Response.Write("<script>alert('Nessun Piano Finanziario approvato per questa voce!');</script>")
        '' ''            '*********************CHIUSURA CONNESSIONE**********************
        '' ''            par.OracleConn.Close()
        '' ''            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '' ''            myReader1.Close()
        '' ''            Exit Sub
        '' ''        End If
        '' ''        myReader1.Close()
        '' ''        '*********************CHIUSURA CONNESSIONE**********************
        '' ''        par.OracleConn.Close()
        '' ''        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        '' ''    End If
        '' ''    txtConfpayment.Value = 0
        '' ''Catch ex As Exception
        '' ''    CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
        '' ''    CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMorosita"
        '' ''End Try
    End Sub
    Private Function CreaPagamento(ByVal VocePF As String, ByVal Importo As Double) As String

        Try

            Dim Id_Fornitore As String = ""
            Dim Id_Pagamento As String = ""
            Dim IdPrenotazione As String = ""
            Dim idTipoPagamento As String = "null"
            Dim idModPagamento As String = "null"
            par.cmd.CommandText = "select * from siscom_mi.prenotazioni where id = (select id_prenotazione from siscom_mi.cond_morosita where id =" & txtidMorosita.Value & ")"

            'par.cmd.CommandText = "SELECT FORNITORI.ID AS ID_FORNITORE FROM SISCOM_MI.FORNITORI WHERE ID = (SELECT ID_FORNITORE FROM SISCOM_MI.CONDOMINI WHERE ID = " & CType(Me.Page, Object).vIdCondominio & ")"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader1.Read Then
                Id_Fornitore = par.IfNull(myReader1("ID_FORNITORE"), "Null")
                IdPrenotazione = par.IfNull(myReader1("ID"), 0)
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO  FROM SISCOM_MI.FORNITORI WHERE ID = " & Id_Fornitore
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                idTipoPagamento = par.IfNull(myReader1("ID_TIPO_PAGAMENTO"), "Null")
                idModPagamento = par.IfNull(myReader1("ID_TIPO_MODALITA_PAG"), "Null")
            End If
            myReader1.Close()



            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM DUAL"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                Id_Pagamento = myReader1(0)
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI (ID,DATA_EMISSIONE,DATA_STAMPA,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_STATO,TIPO_PAGAMENTO,data_scadenza,ID_TIPO_PAGAMENTO,ID_TIPO_MODALITA_PAG,ID_IBAN_FORNITORE) " _
            & " VALUES (" & Id_Pagamento & "," & Format(Now, "yyyyMMdd") & ", " & Format(Now, "yyyyMMdd") & ",'" & par.PulisciStrSql(Me.txtDescrizione.Text.ToUpper) & "'," & par.VirgoleInPunti(Importo) & "," & Id_Fornitore & ",1,2," _
            & "'" & par.AggiustaData(Me.txtDScadenza.Text) & "'," & idTipoPagamento & "," & idModPagamento & "," & par.PulisciStrSql(Me.cmbIbanFornitore.SelectedValue.ToString) & ")"
            par.cmd.ExecuteNonQuery()

            'par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA SET ID_PAGAMENTO = " & Id_Pagamento & " WHERE ID = " & Me.txtidMorosita.Value & " AND ID_CONDOMINIO = " & CType(Me.Page, Object).vIdCondominio
            'par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_APPROVATO= " & par.VirgoleInPunti(Importo) & ", ID_STATO = 2,ID_VOCE_PF = " & VocePF & " ,ID_PAGAMENTO = " & Id_Pagamento & " WHERE ID = " & IdPrenotazione
            par.cmd.ExecuteNonQuery()
            '****Scrittura evento PRENOTAZIONE DEL PAGAMENTO
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Id_Pagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F97','')"
            par.cmd.ExecuteNonQuery()

            '****Scrittura evento EMISSIONE DEL PAGAMENTO
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Id_Pagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','')"
            par.cmd.ExecuteNonQuery()

            Return Id_Pagamento

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMorosita"
        End Try

    End Function
    Private Sub PdfPagamento(ByVal ID As String)
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ModelloPagamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim tb1 As String = "<table style='width:100%;'>"
            Dim tb2 As String = "<table style='width:100%;'>"
            Dim Matricola As String = ""

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "SELECT FORNITORI.*," _
                    & "(select descrizione from siscom_mi.tipo_modalita_pag where id = fornitori.id_tipo_modalita_pag) as modalita, " _
                    & "(select descrizione from siscom_mi.tipo_pagamento where id = fornitori.id_tipo_pagamento) as tipo_pag " _
                    & "FROM SISCOM_MI.FORNITORI, SISCOM_MI.CONDOMINI WHERE condomini.ID =" & CType(Me.Page, Object).vIdCondominio & " and fornitori.ID = condomini.ID_FORNITORE"

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CONDOMINI WHERE ID =" & CType(Me.Page, Object).vIdCondominio
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$chiamante$", "CONTO CORRENTE:")
                'contenuto = Replace(contenuto, "$den_chiamante$", myReader1("DENOMINAZIONE"))
                'contenuto = Replace(contenuto, "$dettaglio$", myReader1("DENOMINAZIONE"))
                'contenuto = Replace(contenuto, "$sc_rata$", par.FormattaData(txtScadenza.Value))
                'contenuto = Replace(contenuto, "$iban$", par.IfNull(myReader1("IBAN"), "n.d."))
                contenuto = Replace(contenuto, "$modalita$", par.IfNull(myReader1("modalita"), "n.d."))
                contenuto = Replace(contenuto, "$condizione$", par.IfNull(myReader1("tipo_pag"), "n.d."))

                '*****************SCRITTURA TABELLA DETTAGLI dettagli
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> " & par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "") & "</td></tr>"
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> IBAN: " & Me.cmbIbanFornitore.SelectedItem.ToString.ToUpper & "</td></tr>"
                tb1 = tb1 & "<tr><td style='text                                       -align: left; font-size:14pt;font-family :Arial ;'> cod. fiscale: " & par.IfNull(myReader1("COD_FISCALE"), "") & "</td><tr></table>"
                '*****************FINE SCRITTURA DETTAGLI

            End If
            myReader1.Close()

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_MOROSITA WHERE ID =" & txtidMorosita.Value
            'myReader1 = par.cmd.ExecuteReader
            'If myReader1.Read Then

            '    S = S & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>dell' Esercizio Finanziario dal: " & par.FormattaData(myReader1("DATA_INIZIO")) & " al " & par.FormattaData(myReader1("DATA_FINE")) & " </td>"
            'tb1 = tb1 & "</table>"
            'contenuto = Replace(contenuto, "$dettagli_chiamante$", "")
            'contenuto = Replace(contenuto, "$fornitori$", tb1)
            contenuto = Replace(contenuto, "$fornitori$", tb1)
            contenuto = Replace(contenuto, "$grigliaM$", "")
            contenuto = Replace(contenuto, "$copia$", "")

            'End If
            'myReader1.Close()

            Dim idvocePf As String = ""
            par.cmd.CommandText = "SELECT PAGAMENTI.*, prenotazioni.*,T_ESERCIZIO_FINANZIARIO.INIZIO AS INIZIO_ESERCIZIO,T_ESERCIZIO_FINANZIARIO.FINE AS FINE_ESERCIZIO FROM siscom_mi.prenotazioni,SISCOM_MI.PAGAMENTI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE prenotazioni.id_pagamento = pagamenti.id and PAGAMENTI.ID = " & ID & " AND PF_VOCI.ID = prenotazioni.ID_VOCE_PF AND PF_VOCI.ID_PIANO_FINANZIARIO = PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$anno$", par.IfNull(myReader1("ANNO"), ""))
                contenuto = Replace(contenuto, "$progr$", par.IfNull(myReader1("PROGR"), ""))
                contenuto = Replace(contenuto, "$dettagli_chiamante$", "12000X01")
                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(myReader1("DATA_EMISSIONE")))
                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("DATA_STAMPA")))
                contenuto = Replace(contenuto, "$TOT$", Format(par.IfNull(myReader1("IMPORTO_APPROVATO"), 0), "##,##0.00"))
                contenuto = Replace(contenuto, "$TOTSING$", "€." & Format(par.IfNull(myReader1("IMPORTO_APPROVATO"), 0), "##,##0.00"))

                '*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "MOROSITA' CONDOMINIALE") & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"

                tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>IMPORTO RATA €.</td>"
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(myReader1("IMPORTO_APPROVATO"), 0), "##,##0.00") & "</td>"
                tb2 = tb2 & "</tr></table>"
                contenuto = Replace(contenuto, "$dettagli$", tb2)
                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "EURO " & NumeroInLettere(Format(par.IfNull(myReader1("IMPORTO_APPROVATO"), 0), "##,##0.00")))
                idvocePf = myReader1("ID_VOCE_PF")
            End If
            myReader1.Close()
            contenuto = Replace(contenuto, "$dscadenza$", Me.txtDScadenza.Text)

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE id = " & idvocePf
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$cod_capitolo$", par.IfNull(myReader1("CODICE"), 0))
                contenuto = Replace(contenuto, "$voce_pf$", par.IfNull(myReader1("DESCRIZIONE"), 0))
                contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")
            End If
            myReader1.Close()
            contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(ID))

            par.cmd.CommandText = "SELECT COD_ANA FROM OPERATORI WHERE ID IN (SELECT ID_OPERATORE FROM SISCOM_MI.EVENTI_PAGAMENTI WHERE ID_PAGAMENTO = " & ID & " AND COD_EVENTO = 'F98' )"
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then
                Matricola = par.IfNull(myReader1("COD_ANA"), "n.d.")
            End If

            myReader1.Close()

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','AttPagamento','');</script>")





            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMorosita"
        End Try


    End Sub
    '******************************************************************************
    '                               NumeroToLettere
    '
    '                Converte il numero intero in lettere
    '
    ' Input : ImportoN                -->Importo Numerico
    '
    ' Ouput : NumeroToLettere         -->Il numero in lettere
    '******************************************************************************
    Function NumeroInLettere(ByVal Numero As String) As String

        '************************
        'Gestisce la virgola
        '************************
        Dim PosVirg As Integer
        Dim Lettere As String

        Numero$ = ChangeStr(Numero$, ".", "")
        PosVirg% = InStr(Numero$, ",")

        If PosVirg% Then
            Lettere$ = NumInLet(Mid(Numero$, 1, Len(Numero) + PosVirg% - 1))
            Lettere$ = Lettere$ & "\" & Format(CInt(Mid(Numero$, PosVirg% + 1, Len(Numero$))), "00")
        Else
            Lettere$ = NumInLet(CDbl(Numero$))
        End If

        NumeroInLettere = Lettere$

    End Function

    Private Function NumInLet(ByVal N As Double) As String

        '************************************************
        'inizializzo i due arry di numeri
        '************************************************
        SetNumeri()

        Dim ValT As Double     'Valore Temporaneo per la conversione
        Dim iCent As Integer    'Valore su cui calcolare le centinaia
        Dim L As String     'Importo in Lettere

        NumInLet = "zero"

        If N = 0 Then Exit Function

        ValT = N
        L = ""

        'miliardi
        iCent = Int(ValT / 1000000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = "unmiliardo"
            Else
                L = LCent(iCent) + "miliardi"
            End If
            ValT = ValT - CDbl(iCent) * 1000000000.0#
        End If

        'milioni
        iCent = Int(ValT / 1000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = L + "unmilione"
            Else
                L = L + LCent(iCent) + "milioni"
            End If
            ValT = ValT - CDbl(iCent) * 1000000.0#
        End If

        'miliaia
        iCent = Int(ValT / 1000)
        If iCent Then
            If iCent = 1 Then
                L = L + "mille"
            Else
                L = L + LCent(iCent) + "mila"
            End If
            ValT = ValT - CDbl(iCent) * 1000
        End If

        ''centinaia
        'If ValT Then
        '    L = L + LCent(CInt(ValT))
        'End If
        If ValT Then
            L = L + LCent(Fix(CDbl(ValT)))
        End If

        NumInLet = L

    End Function

    Function LCent(ByVal N As Integer) As String

        ' Ritorna xx% (1/999) convertito in lettere
        Dim Numero As String
        Dim Lettere As String
        Dim Centinaia As Integer
        Dim Decine As Integer
        Dim x As Integer
        Dim Unita As Integer
        Dim sDec As String

        Numero$ = Format(N, "000")

        Lettere$ = ""
        Centinaia% = Val(Left$(Numero$, 1))
        If Centinaia% Then
            If Centinaia% > 1 Then
                Lettere = sUnita(Centinaia%)
            End If
            Lettere = Lettere + "cento"
        End If

        Decine% = (N Mod 100)
        If Decine% Then
            Select Case Decine%
                Case Is >= 20                               'Decine
                    sDec = sDecina(Val(Mid$(Numero$, 2, 1)))
                    x% = Len(sDec)
                    Unita% = Val(Right$(Numero$, 1))          'Unita
                    If Unita% = 1 Or Unita% = 8 Then x% = x% - 1
                    Lettere$ = Lettere$ & Left(sDec, x%) & sUnita(Unita%)    'Tolgo l'ultima lettera della decina per i
                Case Else
                    Lettere$ = Lettere$ + sUnita(Decine)
            End Select
        End If

        LCent$ = Lettere$

    End Function


    Sub SetNumeri()

        '************************************************
        ' Stringhe per traslitterazione numeri
        '************************************************
        sUnita(1) = "uno"
        sUnita(2) = "due"
        sUnita(3) = "tre"
        sUnita(4) = "quattro"
        sUnita(5) = "cinque"
        sUnita(6) = "sei"
        sUnita(7) = "sette"
        sUnita(8) = "otto"
        sUnita(9) = "nove"
        sUnita(10) = "dieci"
        sUnita(11) = "undici"
        sUnita(12) = "dodici"
        sUnita(13) = "tredici"
        sUnita(14) = "quattordici"
        sUnita(15) = "quindici"
        sUnita(16) = "sedici"
        sUnita(17) = "diciassette"
        sUnita(18) = "diciotto"
        sUnita(19) = "diciannove"

        sDecina(1) = "dieci"
        sDecina(2) = "venti"
        sDecina(3) = "trenta"
        sDecina(4) = "quaranta"
        sDecina(5) = "cinquanta"
        sDecina(6) = "sessanta"
        sDecina(7) = "settanta"
        sDecina(8) = "ottanta"
        sDecina(9) = "novanta"

    End Sub

    '*********************************************************************
    '                ChangeStr - da usare con versioni minori del Vb6
    '
    'Input  = Stringa                           -->Da convertire
    '         Lettera da sostituire             -->Da convertire
    '         Nuova lettera da rimpiazzare      -->Da convertire
    '
    'Ouput  = Stringa rimpiazzata
    '
    '*********************************************************************
    Function ChangeStr(ByRef sBuffer As String, ByRef OldChar As String, _
                       ByRef NewChar As String) As String

        Dim TmpBuf As String
        Dim p As Integer

        On Error GoTo ErrChangeStr

        ChangeStr$ = ""   'Default Error

        TmpBuf$ = sBuffer$
        p% = InStr(TmpBuf$, OldChar$)
        Do While p > 0
            TmpBuf$ = Left$(TmpBuf$, p% - 1) + NewChar$ + Mid$(TmpBuf$, p% + Len(OldChar$))
            p% = InStr(p% + Len(NewChar$), TmpBuf$, OldChar$)
        Loop
        ChangeStr$ = TmpBuf$

        Exit Function

ErrChangeStr:
        ChangeStr$ = ""

    End Function

    Protected Sub btnPagamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPagamento.Click
        'Response.Write("<script>alert('Funzione non disponibile!');</script>")
        Try
            If txtConfpayment.Value = 1 Then
                If par.AggiustaData(Me.txtDScadenza.Text) < Format(Now, "yyyyMMdd") Then
                    Response.Write("<script>alert('La data scadenza non può essere precedente a quella odierna!');</script>")
                    txtidMorosita.Value = 0
                    Exit Sub
                End If

                Dim IMPORTO As Double = 0
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select fl_completo from siscom_mi.cond_morosita where id = " & txtidMorosita.Value
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    If par.IfNull(myReader1(0), 0) = 0 Then
                        Response.Write("<script>alert('Impossibile emettere il pagamento, perchè la morosità non è COMPLETA E STAMPABILE!');</script>")
                        Exit Sub
                    End If
                End If
                myReader1.Close()
                Dim Rimanente As Double = 0
                par.cmd.CommandText = "SELECT NVL(ID_PAGAMENTO,0) AS ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = (SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & txtidMorosita.Value & ")"
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    If myReader1("ID_PAGAMENTO") <> 0 Then
                        Response.Write("<script>alert('ATTENZIONE...Il pagamento della morosità selezionata è stato già emesso!Impossibile procedere!');</script>")
                        myReader1.Close()
                        txtidMorosita.Value = 0
                        Exit Sub
                    End If
                End If
                myReader1.Close()
                'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO = " _
                '                    & "(SELECT PF_MAIN.ID FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID_STATO = 5 " _
                '                    & "AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID AND SUBSTR(INIZIO,0,4) = '" & Format(Now, "yyyymmdd").Substring(0, 4) & "') AND CODICE = '2.02.07.01'"


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE ID = (SELECT ID_VOCE_PF FROM SISCOM_MI.PRENOTAZIONI WHERE ID = (SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & txtidMorosita.Value & "))"

                '
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    '***controllo che il valore preventivato di spesa esista e sia maggiore di 0
                    par.cmd.CommandText = "select sum(NVL(ASSESTAMENTO_VALORE_LORDO,0)+NVL(valore_lordo,0)+NVL(VARIAZIONI,0)) as stanziato from siscom_mi.pf_voci_struttura where id_voce =" & par.IfNull(myReader1("ID"), 0)
                    Dim stanziato As Decimal = 0
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        If CDbl(par.IfNull(lettore("stanziato"), 0)) > 0 Then
                            stanziato = par.IfNull(lettore("stanziato"), 0)
                            '********SOMMO TUTT I PAGAMENTI EFFETTUATI CON QUELLA VOCE DI PREVENTIVO DI SPESA DEL PIANO FINANZIARIO
                            par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_PRENOTATO),0) as TOT_PRENOTATO  FROM SISCOM_MI.PRENOTAZIONI WHERE ID_VOCE_PF = " & myReader1("ID") & " AND ID_PAGAMENTO IS NOT NULL"
                            lettore = par.cmd.ExecuteReader
                            If lettore.Read Then
                                '*******Differenza fra preventivato e importi fino a ora pagati
                                Rimanente = CDbl(stanziato) - CDbl(par.IfNull(lettore("TOT_PRENOTATO"), 0))
                            End If
                        Else
                            lettore.Close()
                            myReader1.Close()
                            Response.Write("<script>alert('Importo stanziato pari a zero!Imporssibile procedere');</script>")
                            '*********************CHIUSURA CONNESSIONE**********************
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Sub
                        End If
                        lettore.Close()
                        If Rimanente >= 0 Then
                            '*****Modifica 17/05/2011
                            'par.cmd.CommandText = "SELECT SUM(IMPORTO)AS IMPORTO FROM SISCOM_MI.COND_MOROSITA_INQUILINI,SISCOM_MI.COND_MOROSITA WHERE ID_MOROSITA = " & Me.txtidMorosita.Value & " AND ID_MOROSITA = COND_MOROSITA.ID "
                            par.cmd.CommandText = "SELECT IMPORTO_PRENOTATO AS IMPORTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " _
                                                & " (SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & Me.txtidMorosita.Value & " AND ID_CONDOMINIO =" & CType(Me.Page, Object).vIdCondominio & ")"
                            lettore = par.cmd.ExecuteReader
                            If lettore.Read Then
                                IMPORTO = par.IfNull(lettore("IMPORTO"), 0)
                            End If
                            lettore.Close()

                            If IMPORTO > 0 Then
                                If Rimanente >= CDbl(IMPORTO) Then
                                    '******Scrittura del nuovo pagamento nell'apposita tabella!*******
                                    Dim Pagamento As String = CreaPagamento(myReader1("ID"), IMPORTO)
                                    Response.Write("<script>alert('Il pagamento è stato emesso e storicizzato!');</script>")
                                    lettore.Close()
                                    myReader1.Close()
                                    '*********************CHIUSURA CONNESSIONE**********************
                                    par.OracleConn.Close()
                                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                    PdfPagamento(Pagamento)
                                    CType(Me.Page.FindControl("Tab_Pagamenti1"), Object).Cerca()

                                    Exit Sub
                                Else
                                    Response.Write("<script>alert('L\'ammontare residuo preventivato per questa spesa è insufficiente a eseguirne la liquidazione!');</script>")
                                    lettore.Close()
                                    myReader1.Close()
                                    '*********************CHIUSURA CONNESSIONE**********************
                                    par.OracleConn.Close()
                                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                                    txtidMorosita.Value = 0
                                    Exit Sub
                                End If
                            Else
                                Response.Write("<script>alert('Impossibile emettere il pagamento!Potrebbe essere stato già emesso o con importo pari a zero!');</script>")
                                txtidMorosita.Value = 0
                                '*********************CHIUSURA CONNESSIONE**********************
                                par.OracleConn.Close()
                                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                myReader1.Close()
                                Exit Sub
                            End If


                        Else
                            Response.Write("<script>alert('Sono stati esauriti gli importi stanziati nel piano finanziario corrente!!');</script>")
                            '*********************CHIUSURA CONNESSIONE**********************
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            txtidMorosita.Value = 0
                            lettore.Close()
                            myReader1.Close()
                            Exit Sub

                        End If
                    Else
                        Response.Write("<script>alert('Nessun importo stanziato per questo tipo di pagamento!');</script>")
                        txtidMorosita.Value = 0
                        myReader1.Close()
                        '*********************CHIUSURA CONNESSIONE**********************
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub

                    End If
                Else
                    Response.Write("<script>alert('Nessun Piano Finanziario approvato per questa voce!');</script>")
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    myReader1.Close()
                    Exit Sub
                End If
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            txtConfpayment.Value = 0
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabMorosita"
        End Try

    End Sub
End Class
