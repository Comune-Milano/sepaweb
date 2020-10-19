
Partial Class MOROSITA_PagamentoManuale
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""Portale.aspx""</script>")
            Exit Sub
        End If

        Try
            Response.Expires = 0

            If Not IsPostBack Then

                If Request.QueryString("IDCON") <> "" Then
                    IdConnessione = Request.QueryString("IDCON")
                End If

                vIdBolletta = 0
                If Request.QueryString("ID_BOLLETTA") <> "" Then
                    vIdBolletta = Request.QueryString("ID_BOLLETTA")
                End If

                'If par.OracleConn.State = Data.ConnectionState.Open Then
                '    Response.Write("IMPOSSIBILE VISUALIZZARE")
                '    Exit Sub
                'Else
                '    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                '    par.SettaCommand(par)
                '    'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '    '‘‘par.cmd.Transaction = par.myTrans
                'End If

                CaricaDati()

            End If


            Dim CTRL As Control

            '*** FORM PRINCIPALE
            For Each CTRL In Me.Form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                End If
            Next

            Me.txtImporto.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtImporto.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

            Me.txtDataPagamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property


    Public Property vIdBolletta() As Long
        Get
            If Not (ViewState("par_vIdBolletta") Is Nothing) Then
                Return CLng(ViewState("par_vIdBolletta"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdBolletta") = value
        End Set

    End Property



    Sub CaricaDati()
        Dim FlagConnessione As Boolean

        Try
            lblErrore.Text = ""
            lblErrore.Visible = False



            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select * from SISCOM_MI.BOL_BOLLETTE where ID=" & vIdBolletta
            myReaderA = par.cmd.ExecuteReader()

            If myReaderA.Read Then
                If par.IfNull(myReaderA("DATA_PAGAMENTO"), "") = "" Then
                    'If par.IfNull(myReaderA("RIF_FILE"), "") = "MAV" Then
                    '    vBollettino = "1"
                    'Else
                    '    vBollettino = "0"
                    'End If
                    vIdContratto = par.IfNull(myReaderA("id_contratto"), "0")

                    Me.txtNumBolletta.Text = par.IfNull(myReaderA("RIF_BOLLETTINO"), "")

                    Me.lblIntestatario.Text = par.IfNull(myReaderA("INTESTATARIO"), "")
                    Me.lblNumero.Text = par.IfNull(myReaderA("ID"), "")
                    Me.lblPeriodo.Text = " dal " & par.FormattaData(par.IfNull(myReaderA("riferimento_da"), "")) & " al " & par.FormattaData(par.IfNull(myReaderA("riferimento_a"), ""))
                    Me.lblEmissione.Text = par.FormattaData(par.IfNull(myReaderA("DATA_EMISSIONE"), ""))
                    Me.lblScadenza.Text = par.FormattaData(par.IfNull(myReaderA("DATA_SCADENZA"), ""))
                    Me.lblIndirizzo.Text = par.IfNull(myReaderA("INDIRIZZO"), "") & " " & par.IfNull(myReaderA("CAP_CITTA"), "")

                    par.cmd.CommandText = "select sum(importo) from SISCOM_MI.BOL_BOLLETTE_VOCI where BOL_BOLLETTE_VOCI.ID_BOLLETTA=" & par.IfNull(myReaderA("ID"), "")
                    myReaderA.Close()
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        Me.lblImporto.Text = myReaderA(0)
                    End If
                    'Me.txtDataPagamento.Text = Format(Now(), "dd/MM/yyyy")
                    txtImporto.Text = lblImporto.Text
                    myReaderA.Close()

                Else
                    myReaderA.Close()
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
                myReaderA.Close()
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


        Catch ex As Exception

            Me.lblErrore.Visible = True
            If FlagConnessione = True Then par.OracleConn.Close()

            lblErrore.Text = ex.Message

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

        End Try

    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click

        If txtModificato.Text <> "111" Then
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "1"
            Response.Write("<script>window.close();</script>")
        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If


            If par.IfEmpty(Me.txtImporto.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtDataPagamento.Text, "Null") <> "Null" Then

                If par.AggiustaData(txtDataPagamento.Text) <= Format(Now, "yyyyMMdd") And par.AggiustaData(txtDataPagamento.Text) >= par.AggiustaData(lblEmissione.Text) Then
                    '********CONNESSIONE E APERTURA *********

                    par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                       & " set DATA_VALUTA='" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(txtDataPagamento.Text))) & "'," _
                                       & "     OPERATORE_PAG='" & par.PulisciStrSql(Session.Item("OPERATORE")) & "'," _
                                       & "     DATA_PAGAMENTO='" & par.AggiustaData(txtDataPagamento.Text) & "'," _
                                       & "     IMPORTO_PAGATO=" & par.VirgoleInPunti(txtImporto.Text) & "," _
                                       & "     NOTE_PAGAMENTO='" & par.PulisciStrSql(txtNote.Text) & "'," _
                                       & "     DATA_INS_PAGAMENTO='" & Format(Now, "yyyyMMddHHmmss") & "'" _
                                       & " where ID=" & vIdBolletta
                    par.cmd.ExecuteNonQuery()

                    'If vBollettino = "1" Then
                    '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    '        & "VALUES (" & vIdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    '        & "'F20','')"
                    'Else
                    '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    '        & "VALUES (" & vIdContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    '        & "'F19','')"
                    'End If
                    ' par.cmd.ExecuteNonQuery()


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

                    CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
                    Me.txtModificato.Text = "0"

                Else
                    Response.Write("<script>alert('Attenzione...la data di pagamento non può essere successiva alla data odierna e precedente alla data di emissione della bolletta!')</script>")
                End If
            Else
                Response.Write("<script>alert('Inserire l\'importo pagato e/o la data di pagamento!')</script>")
            End If

            'par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message

        End Try

    End Sub



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
