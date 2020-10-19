
Partial Class Contratti_Pagamenti_Manuale
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

        End If
        txtDataPagamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            '********CONNESSIONE E APERTURA TRANSAZIONE*********

            lblErrore.Text = ""
            lblErrore.Visible = False

            If (par.IfEmpty(Me.txtNumBolletta.Text, "Null") <> "Null") And IsNumeric(txtNumBolletta.Text) = False Then
                Response.Write("<script>alert('Attenzione, il numero della bolletta, se specificato, deve essere numerico!')</script>")
                Me.lblIntestatario.Text = ""
                Me.lblNumero.Text = ""
                Me.lblPeriodo.Text = ""
                Me.lblEmissione.Text = ""
                Me.lblScadenza.Text = ""
                Me.lblImporto.Text = ""
                Me.txtDataPagamento.Text = ""
                Me.txtImporto.Text = ""
                Me.txtNote.Text = ""
                Me.txtNumBolletta.Text = ""
                lblIndirizzo.Text = ""
                Exit Sub
            End If

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If (par.IfEmpty(Me.txtNumBolletta.Text, "Null") <> "Null" Or par.IfEmpty(Me.txtNumBolletta0.Text, "Null") <> "Null") Or IsNumeric(txtNumBolletta.Text) = True Then
                If par.IfEmpty(Me.txtNumBolletta.Text, "Null") <> "Null" Then
                    vId = txtNumBolletta.Text
                Else
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "select id from SISCOM_MI.BOL_BOLLETTE where RIF_BOLLETTINO='" & par.PulisciStrSql(par.IfEmpty(Me.txtNumBolletta0.Text, "Null")) & "'"
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        vId = myReaderA("ID")
                    Else
                        vId = "0"
                    End If
                    myReaderA.Close()
                End If
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select sum(importo) from SISCOM_MI.BOL_BOLLETTE_VOCI where BOL_BOLLETTE_VOCI.ID_BOLLETTA=" & vId
                myReader = par.cmd.ExecuteReader()


                If myReader.Read Then
                    If CDbl(par.IfNull(myReader(0), 0)) <= 0 Then
                        myReader.Close()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Pagamento impossibile per questa bolletta!L\'importo è minore di zero o bollettino non trovato!')</script>")
                        Me.lblIntestatario.Text = ""
                        Me.lblNumero.Text = ""
                        Me.lblPeriodo.Text = ""
                        Me.lblEmissione.Text = ""
                        Me.lblScadenza.Text = ""
                        Me.lblImporto.Text = ""
                        Me.txtDataPagamento.Text = ""
                        Me.txtImporto.Text = ""
                        Me.txtNote.Text = ""
                        Me.txtNumBolletta.Text = ""
                        lblIndirizzo.Text = ""
                        Exit Sub
                    End If

                End If
                myReader.Close()
                par.cmd.CommandText = "select * from SISCOM_MI.BOL_BOLLETTE where ID=" & vId
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If par.IfNull(myReader("ID_BOLLETTA_RIC"), -1) <> -1 Then
                        myReader.Close()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Non è possibile registrare il pagamento per questa bolletta perchè risulta essere RICLASSIFICATA!')</script>")
                        Me.lblIntestatario.Text = ""
                        Me.lblNumero.Text = ""
                        Me.lblPeriodo.Text = ""
                        Me.lblEmissione.Text = ""
                        Me.lblScadenza.Text = ""
                        Me.lblImporto.Text = ""
                        Me.txtDataPagamento.Text = ""
                        Me.txtImporto.Text = ""
                        Me.txtNote.Text = ""
                        Me.txtNumBolletta.Text = ""
                        lblIndirizzo.Text = ""
                        Exit Sub
                    End If

                    If par.IfNull(myReader("ID_RATEIZZAZIONE"), -1) <> -1 Then
                        myReader.Close()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Non è possibile registrare il pagamento per questa bolletta perchè risulta essere inclusa in un piano di rateizzazione!')</script>")
                        Me.lblIntestatario.Text = ""
                        Me.lblNumero.Text = ""
                        Me.lblPeriodo.Text = ""
                        Me.lblEmissione.Text = ""
                        Me.lblScadenza.Text = ""
                        Me.lblImporto.Text = ""
                        Me.txtDataPagamento.Text = ""
                        Me.txtImporto.Text = ""
                        Me.txtNote.Text = ""
                        Me.txtNumBolletta.Text = ""
                        lblIndirizzo.Text = ""
                        Exit Sub
                    End If

                    'If par.IfNull(myReader("ID_TIPO"), "") = "3" Or par.IfNull(myReader("ID_TIPO"), "") = "4" Or par.IfNull(myReader("ID_TIPO"), "") = "5" Then
                    '    myReader.Close()
                    '    par.OracleConn.Close()
                    '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    '    Response.Write("<script>alert('Non è possibile registrare il pagamento per questa bolletta! Utilizzare le funzioni di pagamento EXTRA MAV')</script>")
                    '    Me.lblIntestatario.Text = ""
                    '    Me.lblNumero.Text = ""
                    '    Me.lblPeriodo.Text = ""
                    '    Me.lblEmissione.Text = ""
                    '    Me.lblScadenza.Text = ""
                    '    Me.lblImporto.Text = ""
                    '    Me.txtDataPagamento.Text = ""
                    '    Me.txtImporto.Text = ""
                    '    Me.txtNote.Text = ""
                    '    Me.txtNumBolletta.Text = ""
                    '    lblIndirizzo.Text = ""
                    '    Exit Sub
                    'End If

                    If par.IfNull(myReader("ID_TIPO"), -1) <> 9 Then
                        myReader.Close()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Non è possibile registrare il pagamento per questo tipo bolletta la cui gestione è automatizzata. E\' POSSIBILE REGISTRARE MANUALMENTE I PAGAMENTI DELLE BOLLETTE DI DEPOSITO CAUZIONALE!')</script>")
                        Me.lblIntestatario.Text = ""
                        Me.lblNumero.Text = ""
                        Me.lblPeriodo.Text = ""
                        Me.lblEmissione.Text = ""
                        Me.lblScadenza.Text = ""
                        Me.lblImporto.Text = ""
                        Me.txtDataPagamento.Text = ""
                        Me.txtImporto.Text = ""
                        Me.txtNote.Text = ""
                        Me.txtNumBolletta.Text = ""
                        lblIndirizzo.Text = ""
                        Exit Sub
                    End If

                    If par.IfNull(myReader("DATA_PAGAMENTO"), "") = "" Then
                        If par.IfNull(myReader("RIF_FILE"), "") = "MAV" Then
                            vBollettino = "1"
                        Else
                            vBollettino = "0"
                        End If
                        vIdContratto = par.IfNull(myReader("id_contratto"), "0")
                        Me.lblIntestatario.Text = par.IfNull(myReader("INTESTATARIO"), "")
                        Me.lblNumero.Text = par.IfNull(myReader("ID"), "")
                        Me.lblPeriodo.Text = " dal " & par.FormattaData(par.IfNull(myReader("riferimento_da"), "")) & " al " & par.FormattaData(par.IfNull(myReader("riferimento_a"), ""))
                        Me.lblEmissione.Text = par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), ""))
                        Me.lblScadenza.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))
                        Me.lblIndirizzo.Text = par.IfNull(myReader("INDIRIZZO"), "") & " " & par.IfNull(myReader("CAP_CITTA"), "")
                        par.cmd.CommandText = "select sum(importo) from SISCOM_MI.BOL_BOLLETTE_VOCI where BOL_BOLLETTE_VOCI.ID_BOLLETTA=" & par.IfNull(myReader("ID"), "")
                        myReader.Close()
                        myReader = par.cmd.ExecuteReader()
                        If myReader.Read Then
                            Me.lblImporto.Text = myReader(0)
                        End If
                        'Me.txtDataPagamento.Text = Format(Now(), "dd/MM/yyyy")
                        txtImporto.Text = lblImporto.Text
                    Else
                        myReader.Close()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Response.Write("<script>alert('Pagamento già inserito per questa bolletta/MAV!')</script>")
                        Me.lblIntestatario.Text = ""
                        Me.lblNumero.Text = ""
                        Me.lblPeriodo.Text = ""
                        Me.lblEmissione.Text = ""
                        Me.lblScadenza.Text = ""
                        Me.lblImporto.Text = ""
                        Me.txtDataPagamento.Text = ""
                        Me.txtImporto.Text = ""
                        Me.txtNote.Text = ""
                        Me.txtNumBolletta.Text = ""
                        lblIndirizzo.Text = ""
                        Exit Sub
                    End If

                Else
                    myReader.Close()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<script>alert('Nessuna bolletta/MAV con questo numero!')</script>")
                    Me.lblIntestatario.Text = ""
                    Me.lblNumero.Text = ""
                    Me.lblPeriodo.Text = ""
                    Me.lblEmissione.Text = ""
                    Me.lblScadenza.Text = ""
                    Me.lblImporto.Text = ""
                    Me.txtDataPagamento.Text = ""
                    Me.txtImporto.Text = ""
                    Me.txtNote.Text = ""
                    Me.txtNumBolletta.Text = ""
                    lblIndirizzo.Text = ""
                End If
            Else
                Response.Write("<script>alert('E\' necessario il numero di bolletta o il numero bollettino del MAV Banca Popolare di Sondrio per avviare la ricerca!')</script>")
                Me.lblIntestatario.Text = ""
                Me.lblNumero.Text = ""
                Me.lblPeriodo.Text = ""
                Me.lblEmissione.Text = ""
                Me.lblScadenza.Text = ""
                Me.lblImporto.Text = ""
                Me.txtDataPagamento.Text = ""
                Me.txtImporto.Text = ""
                Me.txtNote.Text = ""
                Me.txtNumBolletta.Text = ""
                lblIndirizzo.Text = ""
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            

        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            Dim InseritoPag As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
            End If

            If par.IfEmpty(Me.txtImporto.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtDataPagamento.Text, "Null") <> "Null" Then

                If par.AggiustaData(txtDataPagamento.Text) <= Format(Now, "yyyyMMdd") And par.AggiustaData(txtDataPagamento.Text) >= par.AggiustaData(lblEmissione.Text) Then
                    '********CONNESSIONE E APERTURA *********
                    par.cmd.CommandText = "select * from SISCOM_MI.BOL_BOLLETTE where ID=" & vId
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        If par.IfNull(myReader("DATA_PAGAMENTO"), "") <> "" Then
                            Response.Write("<script>alert('Pagamento già inserito per questa bolletta/MAV!')</script>")
                            InseritoPag = True
                        End If
                    End If
                    myReader.Close()
                    If InseritoPag = False Then

                        par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato=importo where id_bolletta=" & vId
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(txtDataPagamento.Text))) & "',operatore_pag='" & par.PulisciStrSql(Session.Item("OPERATORE")) & "',DATA_PAGAMENTO='" & par.AggiustaData(txtDataPagamento.Text) & "',NOTE_PAGAMENTO='" & par.PulisciStrSql(txtNote.Text) & "',DATA_INS_PAGAMENTO='" & Format(Now, "yyyyMMddHHmmss") & "' WHERE ID=" & vId
                        par.cmd.ExecuteNonQuery()



                        If vBollettino = "1" Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & vIdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F20','Boll. numero " & vId & "')"
                        Else
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & vIdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F19','Boll. numero " & vId & "')"
                        End If
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI " _
                                            & "(ID_VOCE_BOLLETTA, DATA_PAGAMENTO,DATA_VALUTA, DATA_OPERAZIONE, IMPORTO_PAGATO, ID_TIPO_PAGAMENTO) " _
                                            & "(SELECT BOL_BOLLETTE_VOCI.ID,'" & par.AggiustaData(txtDataPagamento.Text) & "','" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(txtDataPagamento.Text))) & "',to_char(SYSDATE,'yyyymmdd'),BOL_BOLLETTE_VOCI.imp_pagato, 3 " _
                                            & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                            & "WHERE ID_BOLLETTA=" & vId & ")"
                        par.cmd.ExecuteNonQuery()
                        Me.lblIntestatario.Text = ""
                        Me.lblNumero.Text = ""
                        Me.lblPeriodo.Text = ""
                        Me.lblEmissione.Text = ""
                        Me.lblScadenza.Text = ""
                        Me.lblImporto.Text = ""
                        Me.txtDataPagamento.Text = ""
                        Me.txtImporto.Text = ""
                        Me.txtNote.Text = ""
                        Me.txtNumBolletta.Text = ""
                        lblIndirizzo.Text = ""
                        Response.Write("<script>alert('Operazione eseguita correttamente')</script>")
                    End If
                Else
                    Response.Write("<script>alert('Attenzione...la data di pagamento non può essere successiva alla data odierna e precedente alla data di emissione della bolletta!')</script>")
                End If
            Else
                Response.Write("<script>alert('Inserire l\'importo pagato e/o la data di pagamento!')</script>")
            End If

            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message

        End Try

    End Sub
    Public Property vId() As String
        Get
            If Not (ViewState("par_vId") Is Nothing) Then
                Return CStr(ViewState("par_vId"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_vId") = value
        End Set
    End Property

    Public Property vIdContratto() As String
        Get
            If Not (ViewState("par_vIdContratto") Is Nothing) Then
                Return CStr(ViewState("par_vIdContratto"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_vIdContratto") = value
        End Set
    End Property


    Public Property vBollettino() As String
        Get
            If Not (ViewState("par_vBollettino") Is Nothing) Then
                Return CStr(ViewState("par_vBollettino"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_vBollettino") = value
        End Set
    End Property
End Class
