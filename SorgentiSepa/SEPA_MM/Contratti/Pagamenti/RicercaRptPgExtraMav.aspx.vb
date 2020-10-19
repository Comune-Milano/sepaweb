
Partial Class Contratti_Pagamenti_RicercaRptPgExtraMav
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        erroredata.Visible = False
        If Not IsPostBack Then
            txtEventoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtEventoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtRiferimentoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtRiferiemntoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPagamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPagamentoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            If Not IsNothing(Request.QueryString("T")) Then
                tipoPagamanto.Value = Request.QueryString("T")
                If tipoPagamanto.Value = "R" Then
                    lblTitolo.Text = "RUOLI"
                ElseIf tipoPagamanto.Value = "I" Then
                    lblTitolo.Text = "INGIUNZIONI"
                End If
            End If
            'txtOperatore.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'If Request.QueryString("CALL") = "GEN" Then
            '    Me.lblPeriodo.Visible = True
            '    Me.lblPeriodoal.Visible = True
            '    Me.txtRiferimentoDal.Visible = True
            '    Me.txtRiferiemntoAl.Visible = True
            '    Me.lblDataOpal.Visible = True
            '    Me.txtEventoAl.Visible = True
            'End If
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If Session.Item("MOD_AMM_RPT_P_EXTRA") = 1 Then
                If Session.Item("ID_CAF") = 2 Then
                    par.cmd.CommandText = "select id,operatore from operatori where operatori.id in (select id_operatore from  siscom_mi.incassi_extramav) AND ID_CAF = 2 ORDER BY OPERATORE ASC"
                Else
                    par.cmd.CommandText = "select id,operatore from operatori where operatori.id in (select id_operatore from  siscom_mi.incassi_extramav) ORDER BY OPERATORE ASC"
                End If
            Else
                par.cmd.CommandText = "select id,operatore from operatori where operatori.id in (select id_operatore from  siscom_mi.incassi_extramav) ORDER BY OPERATORE ASC"
            End If
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            cmbOperatori.Items.Add(New ListItem(" ", -1))
            While lettore.Read
                cmbOperatori.Items.Add(New ListItem(par.IfNull(lettore("operatore"), " "), par.IfNull(lettore("id"), -1)))
            End While
            lettore.Close()

            Select Case tipoPagamanto.Value
                Case "R"
                    cmbOperatori.Items.Clear()
                    par.caricaComboBox("select id,operatore from operatori where operatori.id in (select id_operatore from  siscom_mi.INCASSI_RUOLI) ORDER BY OPERATORE ASC", cmbOperatori, "id", "operatore")
                    par.caricaComboBox("select id, UPPER(descrizione) as descrizione from siscom_mi.TIPO_PAG_RUOLO order by descrizione asc", cmbTipoPagamento, "id", "descrizione")
                Case "I"
                    cmbOperatori.Items.Clear()
                    par.caricaComboBox("select id,operatore from operatori where operatori.id in (select id_operatore from  siscom_mi.INCASSI_INGIUNZIONE) ORDER BY OPERATORE ASC", cmbOperatori, "id", "operatore")
                    par.caricaComboBox("select id, UPPER(descrizione) as descrizione from siscom_mi.TIPO_PAG_INGIUNZIONE order by descrizione asc", cmbTipoPagamento, "id", "descrizione")
                Case Else
                    par.caricaComboBox("select id, UPPER(descrizione) as descrizione from siscom_mi.tipo_pag_parz  WHERE UTILIZZABILE=1 order by descrizione asc", cmbTipoPagamento, "id", "descrizione")
            End Select

            ''*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If Session.Item("MOD_AMM_RPT_P_EXTRA") = 0 Then
                'Me.cmbOperatori.Text = Session.Item("OPERATORE")
                Me.cmbOperatori.SelectedValue = Session.Item("ID_OPERATORE")
                Me.cmbOperatori.Enabled = False
                If Me.cmbOperatori.SelectedValue = -1 Then
                    Me.btnCerca.Visible = False
                    Response.Write("<script>alert('Nessuna operazione effettuata sui pagamenti manuali dall\' operatore " & Session.Item("OPERATORE") & "!');</script>")
                End If
            Else
            End If
        End If
    End Sub
    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim myMatch As Match
        myMatch = System.Text.RegularExpressions.Regex.Match(txtEventoDal.Text, "(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))")
        If Not myMatch.Success And txtEventoDal.Text <> "" Then
            erroredata.Visible = True
            erroredata.Text = "La data inserita non è corretta."
        Else
            If Me.chkDettaglio.Checked = False Then
                If par.IfEmpty(Me.txtRiferimentoDal.Text, "null") <> "null" Then
                    If par.AggiustaData(par.IfEmpty(Me.txtRiferimentoDal.Text, Format(Now, "dd/MM/yyyy"))) > par.AggiustaData(par.IfEmpty(Me.txtRiferiemntoAl.Text, Format(Now, "dd/MM/yyyy"))) Then
                        Response.Write("<script>alert('Errore nelle date!');</script>")
                        Exit Sub
                    End If
                End If
                If par.AggiustaData(par.IfEmpty(Me.txtEventoDal.Text, Format(Now, "dd/MM/yyyy"))) > par.AggiustaData(par.IfEmpty(Me.txtEventoAl.Text, Format(Now, "dd/MM/yyyy"))) Then
                    Response.Write("<script>alert('Errore nelle date!');</script>")
                    Exit Sub
                End If
                If par.AggiustaData(par.IfEmpty(Me.txtDataPagamento.Text, Format(Now, "dd/MM/yyyy"))) > par.AggiustaData(par.IfEmpty(Me.txtDataPagamentoAl.Text, Format(Now, "dd/MM/yyyy"))) Then
                    Response.Write("<script>alert('Errore nelle date!');</script>")
                    Exit Sub
                End If

                Select Case tipoPagamanto.Value
                    Case "R"
                        Response.Write("<script>window.open('RptPgExtraGeneraleRuoli.aspx?OPERATORE=" & par.VaroleDaPassare(Me.cmbOperatori.SelectedValue.ToString) _
                                    & "&EVDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoDal.Text)) _
                                    & "&EVAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoAl.Text)) _
                                    & "&TIPINC=" & par.VaroleDaPassare(Me.cmbTipoPagamento.SelectedValue.ToString) _
                                    & "&DPAG=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamento.Text)) & "&DPAGAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamentoAl.Text)) & "&S=" & Valore01(chkSgravio.Checked) & "','RptPgExtraMav','');</script>")
                    Case "I"
                        Response.Write("<script>window.open('RptPgIngGenerale.aspx?OPERATORE=" & par.VaroleDaPassare(Me.cmbOperatori.SelectedValue.ToString) _
                                    & "&EVDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoDal.Text)) _
                                    & "&EVAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoAl.Text)) _
                                    & "&TIPINC=" & par.VaroleDaPassare(Me.cmbTipoPagamento.SelectedValue.ToString) _
                                    & "&DPAG=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamento.Text)) & "&DPAGAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamentoAl.Text)) & "','RptPgExtraMav2','');</script>")


                    Case Else
                        Response.Write("<script>window.open('RptPgExtraGenerale.aspx?OPERATORE=" & par.VaroleDaPassare(Me.cmbOperatori.SelectedValue.ToString) _
                                    & "&EVDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoDal.Text)) _
                                    & "&EVAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoAl.Text)) _
                                    & "&RIFDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtRiferimentoDal.Text)) _
                                    & "&RIFAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtRiferiemntoAl.Text)) _
                                    & "&TIPINC=" & par.VaroleDaPassare(Me.cmbTipoPagamento.SelectedValue.ToString) _
                                    & "&DPAG=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamento.Text)) & "&DPAGAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamentoAl.Text)) & "','RptPgExtraMav','');</script>")

                End Select


            ElseIf Me.chkDettaglio.Checked = True Then
                If Me.cmbOperatori.SelectedValue = "-1" Then
                    If Me.cmbOperatori.SelectedValue = "-1" Then
                        erroredata.Visible = False
                        Response.Write("<script>alert('E\' obbligatorio scegliere un operatore!');</script>")
                        Exit Sub
                    End If
                End If
                If par.IfEmpty(Me.txtRiferimentoDal.Text, "null") <> "null" Then
                    If par.AggiustaData(par.IfEmpty(Me.txtRiferimentoDal.Text, Format(Now, "dd/MM/yyyy"))) > par.AggiustaData(par.IfEmpty(Me.txtRiferiemntoAl.Text, Format(Now, "dd/MM/yyyy"))) Then
                        Response.Write("<script>alert('Errore nelle date!');</script>")
                        Exit Sub
                    End If
                End If
                If par.AggiustaData(par.IfEmpty(Me.txtEventoDal.Text, Format(Now, "dd/MM/yyyy"))) > par.AggiustaData(par.IfEmpty(Me.txtEventoAl.Text, Format(Now, "dd/MM/yyyy"))) Then
                    Response.Write("<script>alert('Errore nelle date!');</script>")
                    Exit Sub
                End If
                If par.AggiustaData(par.IfEmpty(Me.txtDataPagamento.Text, Format(Now, "dd/MM/yyyy"))) > par.AggiustaData(par.IfEmpty(Me.txtDataPagamentoAl.Text, Format(Now, "dd/MM/yyyy"))) Then
                    Response.Write("<script>alert('Errore nelle date!');</script>")
                    Exit Sub
                End If

                Select Case tipoPagamanto.Value
                    Case "R"
                        Response.Write("<script>window.open('RptPgExtraMavRuoli.aspx?OPERATORE=" & par.VaroleDaPassare(Me.cmbOperatori.SelectedValue.ToString) _
                           & "&EVDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoDal.Text)) _
                           & "&EVAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoAl.Text)) _
                           & "&TIPINC=" & par.VaroleDaPassare(Me.cmbTipoPagamento.SelectedValue.ToString) _
                           & "&DPAG=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamento.Text)) & "&DPAGAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamentoAl.Text)) & "','RptPgExtraMav','');</script>")


                    Case "I"

                        Response.Write("<script>window.open('RptPgExtraMavIng.aspx?OPERATORE=" & par.VaroleDaPassare(Me.cmbOperatori.SelectedValue.ToString) _
                           & "&EVDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoDal.Text)) _
                           & "&EVAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoAl.Text)) _
                           & "&TIPINC=" & par.VaroleDaPassare(Me.cmbTipoPagamento.SelectedValue.ToString) _
                           & "&DPAG=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamento.Text)) & "&DPAGAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamentoAl.Text)) & "','RptPgExtraMav','');</script>")


                    Case Else
                        Response.Write("<script>window.open('RptPgExtraMav.aspx?OPERATORE=" & par.VaroleDaPassare(Me.cmbOperatori.SelectedValue.ToString) _
                            & "&EVDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoDal.Text)) _
                            & "&EVAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoAl.Text)) _
                            & "&RIFDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtRiferimentoDal.Text)) _
                            & "&RIFAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtRiferiemntoAl.Text)) _
                            & "&TIPINC=" & par.VaroleDaPassare(Me.cmbTipoPagamento.SelectedValue.ToString) _
                            & "&DPAG=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamento.Text)) & "&DPAGAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtDataPagamentoAl.Text)) & "','RptPgExtraMav','');</script>")


                End Select

            End If
            'ElseIf Request.QueryString("CALL") = "DETT" Then
            '    If Me.cmbOperatori.SelectedValue = "-1" Then
            '        'If confcsv.Value = 1 Then
            '        '    'Response.Redirect("RptPgExtraMavCSV.aspx?OPERATORE=" & par.VaroleDaPassare(Me.cmbOperatori.SelectedValue.ToString) & "&EVDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoDal.Text)))
            '        '    Response.Write("<script>window.open('RptPgExtraMavCSV.aspx?OPERATORE=" & par.VaroleDaPassare(Me.cmbOperatori.SelectedValue.ToString) _
            '        '                                                  & "&EVDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoDal.Text)) _
            '        '                                                  & "&EVAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoAl.Text)) _
            '        '                                                  & "&RIFDAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtRiferimentoDal.Text)) _
            '        '                                                  & "&RIFAL=" & par.VaroleDaPassare(par.AggiustaData(Me.txtRiferiemntoAl.Text)) & "','RptPgExtraMavCSV','');</script>")
            '        'Else
            '        'If par.IfEmpty(Me.txtEventoDal.Text, "null") = "null" Then
            '        '    erroredata.Visible = False
            '        '    Response.Write("<script>alert('E\' obbligatorio definire una data!');</script>")
            '        '    Exit Sub
            '        'End If
            '        If Me.cmbOperatori.SelectedValue = "-1" Then
            '            erroredata.Visible = False
            '            Response.Write("<script>alert('E\' obbligatorio scegliere un operatore!');</script>")
            '            Exit Sub
            '        End If
            '        'End If
            '    Else
            '        Response.Write("<script>window.open('RptPgExtraMav.aspx?OPERATORE=" & par.VaroleDaPassare(Me.cmbOperatori.SelectedValue.ToString) _
            '                                                          & "&DATA=" & par.VaroleDaPassare(par.AggiustaData(Me.txtEventoDal.Text)) _
            '                                                          & "','RptPgExtraMav','');</script>")
            '    End If
            'End If
        End If
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function
End Class
