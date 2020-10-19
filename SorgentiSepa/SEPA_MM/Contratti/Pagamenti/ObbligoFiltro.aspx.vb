
Partial Class Contratti_Pagamenti_ObbligoFiltro
    Inherits System.Web.UI.Page
    Public par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Not IsPostBack Then
            Session.Add("ANNULFILT", 1)

            Try
                idContratto.Value = Request.QueryString("IDCONT")

                Me.txtNumBolletta.Text = Request.QueryString("NUMBOL")
                Me.txtDataDal.Text = Request.QueryString("DAL")
                Me.txtDataAl.Text = Request.QueryString("AL")
                Me.txtEmesDal.Text = Request.QueryString("EDAL")
                Me.txtEmesAl.Text = Request.QueryString("EAL")
                'Response.Write("<script>alert('              ATTENZIONE!\nIl numero di bollette eccede la memoria disponibile!\nDefinire un filtro per procedere.');</script>")

                Me.txtDataDal.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
                Me.txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                Me.txtDataAl.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
                Me.txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                Me.txtEmesDal.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
                Me.txtEmesDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                Me.txtEmesAl.Attributes.Add("onkeypress", "javascript:valid(this,'numbers');")
                Me.txtEmesAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


            Catch ex As Exception
                Session.Add("ERRORE", "Provenienza: ObbligoFiltro-  - " & ex.Message)
                Response.Redirect("../../Errore.aspx", False)

            End Try

        End If
    End Sub
    Private Function ControlFiltro() As String
        ControlFiltro = ""
        If Not String.IsNullOrEmpty(Me.txtDataDal.Text) Then
            If Not String.IsNullOrEmpty(Me.txtDataAl.Text) Then
                If par.AggiustaData(Me.txtDataDal.Text) > par.AggiustaData(Me.txtDataAl.Text) Then
                    ControlFiltro = "-1"
                    Exit Function
                Else
                    ControlFiltro += " AND BOL_BOLLETTE.RIFERIMENTO_DA>='" & par.AggiustaData(Me.txtDataDal.Text) & "' "

                End If
            Else
                ControlFiltro += " AND BOL_BOLLETTE.RIFERIMENTO_DA>='" & par.AggiustaData(Me.txtDataDal.Text) & "' "
            End If
        End If


        If Not String.IsNullOrEmpty(Me.txtDataAl.Text) Then
            If Not String.IsNullOrEmpty(Me.txtDataDal.Text) Then
                If par.AggiustaData(Me.txtDataAl.Text) < par.AggiustaData(Me.txtDataDal.Text) Then
                    ControlFiltro = "-1"
                    Exit Function

                Else
                    ControlFiltro += " AND BOL_BOLLETTE.RIFERIMENTO_A<='" & par.AggiustaData(Me.txtDataAl.Text) & "' "

                End If
            Else
                ControlFiltro += " AND BOL_BOLLETTE.RIFERIMENTO_DA<='" & par.AggiustaData(Me.txtDataAl.Text) & "' "
            End If
        End If

        '**************FILTRO SU EMISSIONE DELLA BOLLETTA 

        If Not String.IsNullOrEmpty(Me.txtEmesDal.Text) Then
            If Not String.IsNullOrEmpty(Me.txtEmesAl.Text) Then
                If par.AggiustaData(Me.txtEmesDal.Text) > par.AggiustaData(Me.txtEmesAl.Text) Then
                    ControlFiltro = "-1"
                    Exit Function

                Else
                    ControlFiltro += " AND BOL_BOLLETTE.DATA_EMISSIONE>='" & par.AggiustaData(Me.txtEmesDal.Text) & "' "

                End If
            Else
                ControlFiltro += " AND BOL_BOLLETTE.DATA_EMISSIONE>='" & par.AggiustaData(Me.txtEmesDal.Text) & "' "
            End If
        End If


        If Not String.IsNullOrEmpty(Me.txtEmesAl.Text) Then
            If Not String.IsNullOrEmpty(Me.txtEmesDal.Text) Then
                If par.AggiustaData(Me.txtEmesAl.Text) < par.AggiustaData(Me.txtEmesDal.Text) Then
                    ControlFiltro = "-1"
                    Exit Function

                Else
                    ControlFiltro += " AND BOL_BOLLETTE.DATA_EMISSIONE<='" & par.AggiustaData(Me.txtEmesAl.Text) & "' "

                End If
            Else
                ControlFiltro += " AND BOL_BOLLETTE.DATA_EMISSIONE<='" & par.AggiustaData(Me.txtEmesAl.Text) & "' "
            End If
        End If

        '**************FILTRO SU NUMERO DELLA BOLLETTA 
        If Not String.IsNullOrEmpty(Me.txtNumBolletta.Text) Then
            ControlFiltro += " AND UPPER(BOL_BOLLETTE.NUM_BOLLETTA)='" & par.PulisciStrSql(Me.txtNumBolletta.Text.ToUpper) & "' "
        End If


    End Function

    Protected Sub btnFiltra_Click(sender As Object, e As System.EventArgs) Handles btnFiltra.Click
        If ControlFiltro() <> "-1" Then
            If Not String.IsNullOrEmpty(Me.txtDataDal.Text) Then
                Session.Add("DATADAL", par.AggiustaData(Me.txtDataDal.Text))
            End If
            If Not String.IsNullOrEmpty(Me.txtDataAl.Text) Then
                Session.Add("DATAAL", par.AggiustaData(Me.txtDataAl.Text))
            End If
            If Not String.IsNullOrEmpty(Me.txtEmesDal.Text) Then
                Session.Add("EMDAL", par.AggiustaData(Me.txtEmesDal.Text))
            End If
            If Not String.IsNullOrEmpty(Me.txtEmesAl.Text) Then
                Session.Add("EMAL", par.AggiustaData(Me.txtEmesAl.Text))
            End If
            If Not String.IsNullOrEmpty(Me.txtNumBolletta.Text) Then
                Session.Add("NUMBOL", Me.txtNumBolletta.Text)
            End If
            Dim filtro As String = ControlFiltro()
            Dim connData As CM.datiConnessione = New CM.datiConnessione(par, False, False)
            connData.apri(False)

            par.cmd.CommandText = "select count(bol_bollette_voci.id) from siscom_mi.bol_bollette_voci,siscom_mi.bol_bollette " _
                    & " where id_bolletta = bol_bollette.id and FL_ANNULLATA = '0' AND ID_BOLLETTA_RIC IS NULL AND ID_RATEIZZAZIONE IS NULL " _
                    & " AND NVL(IMPORTO_RUOLO,0) = 0 AND NVL(IMPORTO_INGIUNZIONE,0) = 0 " _
                    & " AND round(nvl(IMPORTO_PAGATO,0) ,2) < round(IMPORTO_TOTALE ,2) AND IMPORTO_TOTALE  > 0 " _
                    & " AND BOL_BOLLETTE.ID_STATO <> -1 AND  BOL_BOLLETTE.ID_CONTRATTO = " & idContratto.Value & filtro _
                    & " ORDER BY BOL_BOLLETTE.DATA_SCADENZA ASC ,BOL_BOLLETTE.data_emissione ASC,BOL_BOLLETTE.ID ASC "
            Dim resConta As Integer = 0
            resConta = par.cmd.ExecuteScalar
            connData.chiudi(False)
            If resConta > 500 Then
                Response.Write("<script>alert('Il filtro definito, non è sufficiente a evitare problemi sul pagamento!\nDefinire un nuovo filtro!')</script>")
                Exit Sub
            End If

            Session.Item("ANNULFILT") = 0
            Session.Add("FILTAPPLICATO", 1)

            Response.Write("<script>self.close();</script>")

        End If
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Write("<script>self.close();</script>")
        Session.Add("ANNULFILT", 1)
    End Sub
End Class
