
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

            iddich.Value = Request.QueryString("IDDICH")

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtASL.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtDataIngr.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataIngr.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataDocI.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPermSogg.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Dim lsiFrutto As New ListItem("NO", "0")
            cmbAcc.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("SI", "1")
            cmbAcc.Items.Add(lsiFrutto)
            cmbAcc.SelectedIndex = -1
            cmbAcc.SelectedItem.Text = "NO"

            '09/03/2012 Funzione per il caricamento delle tipologie di parentela
            CaricaParenti()
            CaricaTipoIndirizzi()

            TESTO = par.Elimina160(Request.QueryString("COGNOME"))
            TESTO = par.Elimina160(Request.QueryString("PARENTI"))
            If txtOperazione.Text = "1" Then
                VisualizzaDati()
                txtCognome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("COGNOME"), 1, 25))
                txtNome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("NOME"), 1, 25))
                txtData.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATA"), 1, 10))
                txtCF.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("CF"), 1, 16))

                '*********** NUOVI CAMPI PER DISTINGUERE I NUOVI COMPONENTI ***********
                If par.Elimina160(par.RicavaTesto(Request.QueryString("NCOMP"), 1, 2)) = "NO" Then
                    cmbNuovoComp.SelectedValue = "0"
                    lblDataIngr.Visible = False
                    txtDataIngr.Visible = False
                Else
                    cmbNuovoComp.SelectedValue = "1"
                    lblDataIngr.Visible = True
                    txtDataIngr.Visible = True
                    txtDataIngr.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATAINGR"), 1, 10))
                    chkReferente.Visible = True
                    lblIndirizzo.Visible = True
                    txtVia.Visible = True
                    cmbTipoVia.Visible = True
                    lblCivico.Visible = True
                    txtCivico.Visible = True
                    lblCap.Visible = True
                    txtCap.Visible = True
                    lblComune.Visible = True
                    txtComune.Visible = True
                    lblDocIden.Visible = True
                    txtDocIdent.Visible = True
                    lblDataIdent.Visible = True
                    txtDataDocI.Visible = True
                    lblRilasciata.Visible = True
                    txtRilasciata.Visible = True
                    lblPermSogg.Visible = True
                    txtPermSogg.Visible = True
                    lblDataSogg.Visible = True
                    txtDataPermSogg.Visible = True
                End If

                cmbParenti.SelectedIndex = -1
                If par.Elimina160(par.RicavaTesto(Request.QueryString("PARENTI"), 1, 25)) <> "" Then
                    cmbParenti.Items.FindByText(par.Elimina160(par.RicavaTesto(Request.QueryString("PARENTI"), 1, 25))).Selected = True
                Else
                    cmbParenti.Items.FindByValue("1").Selected = True
                End If
                txtInv.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("INV"), 1, 6))
                txtASL.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("ASL"), 1, 6))
                cmbAcc.SelectedIndex = -1
                If par.Elimina160(par.RicavaTesto(Request.QueryString("ACC"), 1, 2)) <> "" Then
                    cmbAcc.Items.FindByText(par.Elimina160(par.RicavaTesto(Request.QueryString("ACC"), 1, 2))).Selected = True
                Else
                    cmbAcc.Items.FindByValue(0).Selected = True
                End If
                If txtRiga.Text = "0" Then
                    txtCognome.Enabled = False
                    txtNome.Enabled = False
                    txtData.Enabled = False
                    txtCF.Enabled = False
                Else
                    txtCognome.Enabled = True
                    txtNome.Enabled = True
                    txtData.Enabled = True
                    txtCF.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub VisualizzaDati()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select nuovi_comp_nucleo_vsa.*,comp_nucleo_vsa.progr from nuovi_comp_nucleo_vsa,comp_nucleo_vsa where " _
                    & " id_dichiarazione=" & iddich.Value & " and nuovi_comp_nucleo_vsa.id_componente = comp_nucleo_vsa.id and progr=" & txtRiga.Text
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

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
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

        If txtCF.Text = "" Then
            L4.Visible = True
            Exit Sub
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

        If IsNumeric(txtInv.Text) = True Then
            If CDbl(txtInv.Text) > 100 Then
                L5.Visible = True
                L5.Text = "(Valore massimo=100%)"
            Else
                L5.Visible = False
            End If
        Else
            L5.Visible = True
            L5.Text = "(Valore Numerico)"
        End If

        If cmbAcc.SelectedItem.Text = "SI" And txtInv.Text <> "100" Then
            L7.Visible = True
            L7.Text = "(SI solo se 100%)"
        Else
            L7.Visible = False
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

        If cmbNuovoComp.SelectedValue = "-1" Then
            lblNuovoComp.Visible = True
        Else
            lblNuovoComp.Visible = False
        End If

        If cmbNuovoComp.SelectedValue = "1" Then
            If Len(txtDataIngr.Text) <> 10 Then
                lblerroreData.Visible = True
                lblerroreData.Text = "(Data non valida (10 car.))"
            Else
                lblerroreData.Visible = False
            End If

            If IsDate(txtDataIngr.Text) = False Then
                lblerroreData.Visible = True
                lblerroreData.Text = "(Data non valida)"
            Else
                lblerroreData.Visible = False
            End If
        End If

        If verifica.Value = 0 And txtOperazione.Text = "0" Then
            L4.Visible = True
            L4.Text = "Verifica componente!"
        End If

        If L7.Visible = True Or L6.Visible = True Or L5.Visible = True Or L1.Visible = True Or L2.Visible = True Or L3.Visible = True Or L4.Visible = True Or lblNuovoComp.Visible = True Or lblerroreData.Visible = True Then

            'Response.Clear()
            'Response.Write("<script>alert('dati errati');</script>")
            'Response.Write("<script>window.close();</script>")
            'Response.End()
            Exit Sub
        End If

        Dim referente As Integer
        If chkReferente.Checked = True Then
            referente = 1
        Else
            referente = 0
        End If

        If txtOperazione.Text = "0" Then
            If cmbNuovoComp.SelectedValue = "1" Then
                Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtData.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat(cmbParenti.SelectedItem.Text, 25) & " " & par.MiaFormat(txtInv.Text, 6) & " " & par.MiaFormat(txtASL.Text, 5) & " " & par.MiaFormat(cmbAcc.SelectedItem.Text, 10) & " " & par.MiaFormat(cmbNuovoComp.SelectedItem.Text, 12) & " " & par.MiaFormat(txtDataIngr.Text, 10) _
                & par.MiaFormat(cmbTipoVia.SelectedValue, 2) & par.MiaFormat(txtVia.Text, 25) & par.MiaFormat(txtCivico.Text, 5) & par.MiaFormat(txtComune.Text, 25) & par.MiaFormat(txtCap.Text, 5) & par.MiaFormat(txtDocIdent.Text, 15) & par.MiaFormat(txtDataDocI.Text, 10) & par.MiaFormat(txtRilasciata.Text, 25) & par.MiaFormat(txtPermSogg.Text, 15) & par.MiaFormat(txtDataPermSogg.Text, 10) & referente
            ElseIf cmbNuovoComp.SelectedValue = "0" Then
                Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtData.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat(cmbParenti.SelectedItem.Text, 25) & " " & par.MiaFormat(txtInv.Text, 6) & " " & par.MiaFormat(txtASL.Text, 5) & " " & par.MiaFormat(cmbAcc.SelectedItem.Text, 10) & " " & par.MiaFormat(cmbNuovoComp.SelectedItem.Text, 12)
            End If

            If Val(txtInv.Text) = 100 And cmbAcc.SelectedItem.Text = "SI" Then
                Cache(Session.Item("GSpese")) = par.MiaFormat(txtCognome.Text & "," & txtNome.Text, 52) & " "
            Else
                Cache.Remove(Session.Item("GSpese"))
            End If
        Else
            Cache(Session.Item("GRiga")) = txtRiga.Text
            If cmbNuovoComp.SelectedValue = "1" Then
                Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtData.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat(cmbParenti.SelectedItem.Text, 25) & " " & par.MiaFormat(txtInv.Text, 6) & " " & par.MiaFormat(txtASL.Text, 5) & " " & par.MiaFormat(cmbAcc.SelectedItem.Text, 10) & " " & par.MiaFormat(cmbNuovoComp.SelectedItem.Text, 12) & " " & par.MiaFormat(txtDataIngr.Text, 10) _
                & par.MiaFormat(cmbTipoVia.SelectedValue, 2) & par.MiaFormat(txtVia.Text, 25) & par.MiaFormat(txtCivico.Text, 5) & par.MiaFormat(txtComune.Text, 25) & par.MiaFormat(txtCap.Text, 5) & par.MiaFormat(txtDocIdent.Text, 15) & par.MiaFormat(txtDataDocI.Text, 10) & par.MiaFormat(txtRilasciata.Text, 25) & par.MiaFormat(txtPermSogg.Text, 15) & par.MiaFormat(txtDataPermSogg.Text, 10) & referente
            ElseIf cmbNuovoComp.SelectedValue = "0" Then
                Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtData.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat(cmbParenti.SelectedItem.Text, 25) & " " & par.MiaFormat(txtInv.Text, 6) & " " & par.MiaFormat(txtASL.Text, 5) & " " & par.MiaFormat(cmbAcc.SelectedItem.Text, 10) & " " & par.MiaFormat(cmbNuovoComp.SelectedItem.Text, 12)
            End If
            If Val(txtInv.Text) = 100 And cmbAcc.SelectedItem.Text = "SI" Then
                Cache(Session.Item("GSpese")) = par.MiaFormat(txtCognome.Text & "," & txtNome.Text, 52) & " "
            Else
                Cache.Remove(Session.Item("GSpese"))
            End If
        End If
        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub

    Protected Sub cmbNuovoComp_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbNuovoComp.SelectedIndexChanged
        If cmbNuovoComp.SelectedValue = "1" Then
            txtDataIngr.Visible = True
            lblDataIngr.Visible = True
            chkReferente.Visible = True
            lblIndirizzo.Visible = True
            txtVia.Visible = True
            cmbTipoVia.Visible = True
            lblCivico.Visible = True
            txtCivico.Visible = True
            lblCap.Visible = True
            txtCap.Visible = True
            lblComune.Visible = True
            txtComune.Visible = True
            lblDocIden.Visible = True
            txtDocIdent.Visible = True
            lblDataIdent.Visible = True
            txtDataDocI.Visible = True
            lblRilasciata.Visible = True
            txtRilasciata.Visible = True
            lblPermSogg.Visible = True
            txtPermSogg.Visible = True
            lblDataSogg.Visible = True
            txtDataPermSogg.Visible = True
        Else
            txtDataIngr.Visible = False
            lblDataIngr.Visible = False
            chkReferente.Visible = False
            lblIndirizzo.Visible = False
            txtVia.Visible = False
            cmbTipoVia.Visible = False
            lblCivico.Visible = False
            txtCivico.Visible = False
            lblCap.Visible = False
            txtCap.Visible = False
            lblComune.Visible = False
            txtComune.Visible = False
            lblDocIden.Visible = False
            txtDocIdent.Visible = False
            lblDataIdent.Visible = False
            txtDataDocI.Visible = False
            lblRilasciata.Visible = False
            txtRilasciata.Visible = False
            lblPermSogg.Visible = False
            txtPermSogg.Visible = False
            lblDataSogg.Visible = False
            txtDataPermSogg.Visible = False
        End If
    End Sub

    Private Sub CaricaParenti()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select * from t_tipo_parentela where cod<>'8' and cod<>'17' and cod<>'24' and cod<>'26' and cod<>'28' and cod<>'30'" _
                & "order by cod asc"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                cmbParenti.Items.Add(New ListItem(par.IfNull(lettore("descrizione"), " "), par.IfNull(lettore("cod"), -1)))
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


    Private Function Correlazioni(ByVal CF As String) As Boolean
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Correlazioni = False

        par.cmd.CommandText = "SELECT DICHIARAZIONI.ID FROM DICHIARAZIONI,COMP_NUCLEO WHERE DICHIARAZIONI.ID=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO.COD_FISCALE='" & CF & "' OR COMP_NUCLEO.COD_FISCALE='" & CF & "') AND COMP_NUCLEO.ID_DICHIARAZIONE<>" & iddich.Value

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            'txtCAPRes.Text = myReader(0)
            Correlazioni = True
        End If
        myReader.Close()

    End Function

    Private Function Correlazioni1(ByVal CF As String) As Boolean
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Correlazioni1 = False

        par.cmd.CommandText = "SELECT DICHIARAZIONI_CAMBI.ID FROM DICHIARAZIONI_CAMBI,COMP_NUCLEO_CAMBI WHERE DICHIARAZIONI_CAMBI.ID=COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO_CAMBI.COD_FISCALE='" & CF & "' OR COMP_NUCLEO_CAMBI.COD_FISCALE='" & CF & "') AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE<>" & iddich.Value

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            Correlazioni1 = True
        End If
        myReader.Close()

    End Function


    Private Function Correlazioni2(ByVal CF As String) As Boolean
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Correlazioni2 = False

        par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ID FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE (+) AND (UTENZA_COMP_NUCLEO.COD_FISCALE='" & CF & "' OR UTENZA_COMP_NUCLEO.COD_FISCALE='" & CF & "') AND UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE<>" & iddich.Value

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            Correlazioni2 = True
        End If
        myReader.Close()

    End Function

    Private Function CorrelazioniVSA(ByVal CF As String) As Boolean
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        CorrelazioniVSA = False

        par.cmd.CommandText = "SELECT DICHIARAZIONI_vsa.ID FROM DICHIARAZIONI_vsa,COMP_NUCLEO_vsa WHERE DICHIARAZIONI_vsa.ID=COMP_NUCLEO_vsa.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO_vsa.COD_FISCALE='" & CF & "' OR COMP_NUCLEO_vsa.COD_FISCALE='" & CF & "') AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE<>" & iddich.Value

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then

            CorrelazioniVSA = True
        End If
        myReader.Close()

    End Function
End Class
