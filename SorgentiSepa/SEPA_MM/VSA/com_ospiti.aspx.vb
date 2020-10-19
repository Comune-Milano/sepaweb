
Partial Class VSA_com_nucleo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Write("<script></script>")

        If Not IsPostBack = True Then

            Dim TESTO As String

            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            txtProgr.Text = par.Elimina160(Request.QueryString("PR"))
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            iddom.Value = Request.QueryString("IDDOM")

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtDataIngr.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataIngr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataFine.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtDataFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataDocI.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPermSogg.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            CaricaTipoIndirizzi()
            TESTO = par.Elimina160(Request.QueryString("COGNOME"))
            If txtOperazione.Text = "1" Then
                VisualizzaDati()
                txtCognome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("COGNOME"), 1, 25))
                txtNome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("NOME"), 1, 25))
                txtData.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATA"), 1, 10))
                txtCF.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("CF"), 1, 16))
                txtDataIngr.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATAINGR"), 1, 10))
                txtDataFine.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATAFINE"), 1, 10))
            End If
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        If txtCognome.Text = "" Then
            L1.Visible = True
        Else
            L1.Visible = False
        End If
        If txtNome.Text = "" Then
            L2.Visible = True
        Else
            L2.Visible = False
        End If

        If Len(txtData.Text) <> 10 Then
            L3.Visible = True
            L3.Text = "(Data non valida (10 car.))"
        Else
            L3.Visible = False
        End If

        If IsDate(txtData.Text) = False Then
            L3.Visible = True
            L3.Text = "(Data non valida)"
        Else
            L3.Visible = False
        End If

        If Len(txtDataIngr.Text) <> 10 Then
            lblInizio.Visible = True
            lblInizio.Text = "(Data non valida (10 car.))"
        Else
            lblInizio.Visible = False
        End If

        If IsDate(txtDataIngr.Text) = False Then
            lblInizio.Visible = True
            lblInizio.Text = "(Data non valida)"
        Else
            lblInizio.Visible = False
        End If

        If Len(txtDataFine.Text) <> 10 Then
            lblFine.Visible = True
            lblFine.Text = "(Data non valida (10 car.))"
        Else
            lblFine.Visible = False
        End If

        If IsDate(txtDataFine.Text) = False Then
            lblFine.Visible = True
            lblFine.Text = "(Data non valida)"
        Else
            lblFine.Visible = False
        End If

        If txtCF.Text = "" Then
            L4.Visible = True
            Exit Sub
        Else
            L4.Visible = False
        End If



        If par.ControllaCFNomeCognome(UCase(txtCF.Text), txtCognome.Text, txtNome.Text) = False Then
            L4.Visible = True
            L4.Text = "(Errato)"
        Else
            L4.Visible = False
        End If

        If par.ControllaCF(UCase(txtCF.Text)) = False Then
            L4.Visible = True
            L4.Text = "(Errato)"
        Else
            L4.Visible = False
        End If

        If txtDataDocI.Text <> "" Then
            If IsDate(txtDataDocI.Text) = False Then
                LBLdataDoc.Visible = True
                LBLdataDoc.Text = "(Data non valida)"
            Else
                LBLdataDoc.Visible = False
            End If
        End If

        If txtDataPermSogg.Text <> "" Then
            If IsDate(txtDataPermSogg.Text) = False Then
                LBLdataPerm.Visible = True
                LBLdataPerm.Text = "(Data non valida)"
            Else
                LBLdataPerm.Visible = False
            End If
        End If

        If L1.Visible = True Or L2.Visible = True Or L3.Visible = True Or L4.Visible = True Or lblInizio.Visible = True Or lblFine.Visible = True Then

            'Response.Clear()
            'Response.Write("<script>alert('dati errati');</script>")
            'Response.Write("<script>window.close();</script>")
            'Response.End()
            Exit Sub
        End If

        If par.AggiustaData(txtDataIngr.Text) > par.AggiustaData(txtDataFine.Text) Then
            Response.Write("<script>alert('Definire correttamente l\'intervallo di tempo!')</script>")
            Exit Sub
        End If

        Dim referente As Integer
        If chkReferente.Checked = True Then
            referente = 1
        Else
            referente = 0
        End If


        If txtOperazione.Text = "0" Then
            Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtData.Text, 10) & " " & par.MiaFormat(txtCF.Text, 21) & " " & par.MiaFormat(txtDataIngr.Text, 16) & " " & par.MiaFormat(txtDataFine.Text, 10) & " " _
             & par.MiaFormat(cmbTipoVia.SelectedValue, 2) & par.MiaFormat(txtVia.Text, 25) & par.MiaFormat(txtCivico.Text, 5) & par.MiaFormat(txtComune.Text, 25) & par.MiaFormat(txtCap.Text, 5) & par.MiaFormat(txtDocIdent.Text, 15) & par.MiaFormat(txtDataDocI.Text, 10) & par.MiaFormat(txtRilasciata.Text, 25) & par.MiaFormat(txtPermSogg.Text, 15) & par.MiaFormat(txtDataPermSogg.Text, 10) & referente
        Else
            Cache(Session.Item("GRiga")) = txtRiga.Text

            Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtData.Text, 10) & " " & par.MiaFormat(txtCF.Text, 21) & " " & par.MiaFormat(txtDataIngr.Text, 16) & " " & par.MiaFormat(txtDataFine.Text, 10) & " " _
            & par.MiaFormat(cmbTipoVia.SelectedValue, 2) & par.MiaFormat(txtVia.Text, 25) & par.MiaFormat(txtCivico.Text, 5) & par.MiaFormat(txtComune.Text, 25) & par.MiaFormat(txtCap.Text, 5) & par.MiaFormat(txtDocIdent.Text, 15) & par.MiaFormat(txtDataDocI.Text, 10) & par.MiaFormat(txtRilasciata.Text, 25) & par.MiaFormat(txtPermSogg.Text, 15) & par.MiaFormat(txtDataPermSogg.Text, 10) & referente
        End If

        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub

    Private Sub VisualizzaDati()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select comp_nucleo_ospiti_vsa.* from comp_nucleo_ospiti_vsa,domande_bando_vsa where " _
                    & "id_domanda=" & iddom.Value & " and domande_bando_vsa.id = comp_nucleo_ospiti_vsa.id_domanda and comp_nucleo_ospiti_vsa.id=" & Request.QueryString("ID")
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            If lettore.Read Then
                txtComune.Text = par.IfNull(lettore("COMUNE_RES_DNTE"), "")
                cmbTipoVia.SelectedValue = par.IfNull(lettore("ID_TIPO_IND_RES_DNTE"), "")
                txtVia.Text = par.IfNull(lettore("IND_RES_DNTE"), "")
                txtCivico.Text = par.IfNull(lettore("CIVICO_RES_DNTE"), "")
                txtCap.Text = par.IfNull(lettore("CAP_RES_DNTE"), "")

                txtDocIdent.Text = par.IfNull(lettore("CARTA_I"), "")
                txtDataDocI.Text = par.FormattaData(par.IfNull(lettore("CARTA_I_DATA"), ""))
                txtRilasciata.Text = par.IfNull(lettore("CARTA_I_RILASCIATA"), "")
                txtPermSogg.Text = par.IfNull(lettore("PERMESSO_SOGG_N"), "")
                txtDataPermSogg.Text = par.FormattaData(par.IfNull(lettore("PERMESSO_SOGG_DATA"), ""))
                If par.IfNull(lettore("FL_REFERENTE"), "-1") = 1 Then
                    chkReferente.Checked = True
                Else
                    chkReferente.Checked = False
                End If
            End If
            lettore.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaTipoIndirizzi()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select * from t_tipo_indirizzo order by cod asc"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                cmbTipoVia.Items.Add(New ListItem(par.IfNull(lettore("descrizione"), " "), par.IfNull(lettore("cod"), -1)))
            End While
            lettore.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
