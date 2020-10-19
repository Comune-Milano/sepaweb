
Partial Class VSA_com_nucleo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Write("<script></script>")

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Not IsPostBack = True Then

            Dim TESTO As String

            lIdConnessDOMANDA = Request.QueryString("IDCONN")
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
            ElseIf txtOperazione.Text = "2" Then
                VisualizzaDati()
                txtCognome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("COGNOME"), 1, 25))
                txtNome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("NOME"), 1, 25))
                txtData.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATA"), 1, 10))
                txtCF.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("CF"), 1, 16))
                txtDataIngr.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATAINGR"), 1, 10))
                txtDataFine.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATAFINE"), 1, 10))
                txtCognome.Enabled = False
                txtNome.Enabled = False
                txtCF.Enabled = False
                cmbTipoVia.Enabled = False
                txtCap.Enabled = False
                txtCivico.Enabled = False
                txtVia.Enabled = False
                txtComune.Enabled = False
                chkReferente.Enabled = False
                txtDocIdent.Enabled = False
                txtRilasciata.Enabled = False
                txtData.Enabled = False
                txtPermSogg.Enabled = False
                txtDataIngr.Enabled = False
                txtData.Enabled = False
                txtDataIngr.Enabled = False
                txtDataFine.Enabled = False
                txtDataDocI.Enabled = False
                txtDataPermSogg.Enabled = False
            End If
        End If

        SettaControlModifiche(Me)
    End Sub

    Public Property lIdConnessDOMANDA() As String
        Get
            If Not (ViewState("par_lIdConnessDOMANDA") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessDOMANDA"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessDOMANDA") = value
        End Set

    End Property

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


        'If txtOperazione.Text = "0" Then
        '    Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtData.Text, 10) & " " & par.MiaFormat(txtCF.Text, 21) & " " & par.MiaFormat(txtDataIngr.Text, 16) & " " & par.MiaFormat(txtDataFine.Text, 10) & " " _
        '     & par.MiaFormat(cmbTipoVia.SelectedValue, 2) & par.MiaFormat(txtVia.Text, 25) & par.MiaFormat(txtCivico.Text, 5) & par.MiaFormat(txtComune.Text, 25) & par.MiaFormat(txtCap.Text, 5) & par.MiaFormat(txtDocIdent.Text, 15) & par.MiaFormat(txtDataDocI.Text, 10) & par.MiaFormat(txtRilasciata.Text, 25) & par.MiaFormat(txtPermSogg.Text, 15) & par.MiaFormat(txtDataPermSogg.Text, 10) & referente
        'Else
        '    Cache(Session.Item("GRiga")) = txtRiga.Text

        '    Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtData.Text, 10) & " " & par.MiaFormat(txtCF.Text, 21) & " " & par.MiaFormat(txtDataIngr.Text, 16) & " " & par.MiaFormat(txtDataFine.Text, 10) & " " _
        '    & par.MiaFormat(cmbTipoVia.SelectedValue, 2) & par.MiaFormat(txtVia.Text, 25) & par.MiaFormat(txtCivico.Text, 5) & par.MiaFormat(txtComune.Text, 25) & par.MiaFormat(txtCap.Text, 5) & par.MiaFormat(txtDocIdent.Text, 15) & par.MiaFormat(txtDataDocI.Text, 10) & par.MiaFormat(txtRilasciata.Text, 25) & par.MiaFormat(txtPermSogg.Text, 15) & par.MiaFormat(txtDataPermSogg.Text, 10) & referente
        'End If

        ScriviComponente()

        'Response.Clear()
        'Response.Write("<script>window.close();</script>")
        'Response.End()
    End Sub

    Private Sub ScriviComponente()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(lIdConnessDOMANDA), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessDOMANDA), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim numProgr As Integer = 0
            Dim tipoInval As String = ""
            Dim naturaInval As String = ""
            Dim idOsp As Long = 0
            Dim OspOld As Boolean = False
            Dim idOspite As Long = 0
            Dim referente As Integer = 0
            Dim i As Integer = 0
            Dim idOspOld As Long = 0

            Dim idOspiti As String = ""
            '    Dim listaOspiti As String = ""
            '    Dim inserOspite As Boolean = False

            If chkReferente.Checked = True Then
                referente = 1
            Else
                referente = 0
            End If

            Dim incrementaProgr As Integer = 0
            If txtOperazione.Text = "0" Then

                par.cmd.CommandText = "SELECT COUNT(ID) AS NUMOSP FROM COMP_NUCLEO_OSPITI_VSA WHERE ID_DOMANDA=" & Request.QueryString("IDDOM")
                'par.cmd.CommandText = "SELECT COUNT(ID) AS NUMCOMP FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICH")
                Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderC.Read Then
                    numProgr = par.IfNull(myReaderC("NUMOSP"), 0)
                End If
                myReaderC.Close()

                If numProgr > 0 Then
                    incrementaProgr = numProgr
                End If

                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_OSPITI_VSA WHERE ID_DOMANDA=" & Request.QueryString("IDDOM") & " AND COD_FISCALE='" & par.IfEmpty(txtCF.Text, "") & "'"
                myReaderC = par.cmd.ExecuteReader()
                If myReaderC.Read Then
                    idOspOld = par.IfNull(myReaderC("ID"), 0)
                    OspOld = True
                End If
                myReaderC.Close()

                If OspOld = False Then
                    par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_OSPITI_VSA (ID,ID_DOMANDA,DATA_AGG,COGNOME,NOME,COD_FISCALE,DATA_INIZIO_OSPITE,DATA_FINE_OSPITE,DATA_NASC,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,COMUNE_RES_DNTE,CAP_RES_DNTE,CARTA_I,CARTA_I_DATA,CARTA_I_RILASCIATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,FL_REFERENTE) " _
                                & "VALUES (SEQ_COMP_NUCLEO_OSPITI_VSA.NEXTVAL," & Request.QueryString("IDDOM") & ",'" & Format(Now, "yyyyMMdd") & "','" & par.PulisciStrSql(par.IfEmpty(txtCognome.Text, "")) & "','" & par.PulisciStrSql(par.IfEmpty(txtNome.Text, "")) _
                                & "','" & par.IfEmpty(txtCF.Text, "") & "','" & par.AggiustaData(par.IfEmpty(txtDataIngr.Text, "")) & "','" & par.AggiustaData(par.IfEmpty(txtDataFine.Text, "")) & "','" & par.AggiustaData(par.IfEmpty(txtData.Text, "")) & "'," _
                                & cmbTipoVia.SelectedValue & ",'" & par.PulisciStrSql(par.IfEmpty(txtVia.Text, "")) & "','" & par.IfEmpty(txtCivico.Text, "") & "','" & par.IfEmpty(txtComune.Text, "") & "','" & par.IfEmpty(txtCap.Text, "") & "','" & par.IfEmpty(txtDocIdent.Text, "") & "','" _
                                & par.AggiustaData(par.IfEmpty(txtDataDocI.Text, "")) & "','" & par.PulisciStrSql(par.IfEmpty(txtRilasciata.Text, "")) & "','" & par.IfEmpty(txtPermSogg.Text, "") & "','" & par.AggiustaData(par.IfEmpty(txtDataPermSogg.Text, "")) & "'," & referente & ")"
                    
                    par.cmd.ExecuteNonQuery()

                    'par.cmd.CommandText = "SELECT SEQ_COMP_NUCLEO_VSA.CURRVAL FROM DUAL"
                    'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReader.Read() Then
                    '    idComponente = myReader(0)
                    'End If
                    'myReader.Close()

                    'If cmbNuovoComp.SelectedValue <> "0" Then
                    '    par.cmd.CommandText = "INSERT INTO NUOVI_COMP_NUCLEO_VSA (ID_COMPONENTE,DATA_INGRESSO_NUCLEO,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,COMUNE_RES_DNTE,CAP_RES_DNTE,CARTA_I,CARTA_I_DATA,CARTA_I_RILASCIATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,FL_REFERENTE) VALUES " _
                    '    & "(" & idComponente & ",'" & par.AggiustaData(par.IfEmpty(txtDataIngr.Text, "")) & "'," & cmbTipoVia.SelectedValue & ",'" & par.PulisciStrSql(par.IfEmpty(txtVia.Text, "")) & "','" & par.IfEmpty(txtCivico.Text, "") & "','" & par.PulisciStrSql(par.IfEmpty(txtComune.Text, "")) & "'," _
                    '    & "'" & par.IfEmpty(txtCap.Text, "") & "','" & par.IfEmpty(txtDocIdent.Text, "") & "','" & par.AggiustaData(par.IfEmpty(txtDataDocI.Text, "")) & "','" & par.PulisciStrSql(par.IfEmpty(txtRilasciata.Text, "")) & "','" & par.IfEmpty(txtPermSogg.Text, "") & "','" & par.PulisciStrSql(par.IfEmpty(txtDataPermSogg.Text, "")) & "'," & referente & ")"
                    '    par.cmd.ExecuteNonQuery()
                    'End If
                Else
                    'par.cmd.CommandText = "UPDATE UTENZA_COMP_NUCLEO SET COD_FISCALE='" & txtCF.Text & "',COGNOME='" & RTrim(LTrim(par.PulisciStrSql(txtCognome.Text))) & "',NOME='" & RTrim(LTrim(par.PulisciStrSql(txtNome.Text))) & "',SESSO='" & par.RicavaSesso(txtCF.Text) & "',DATA_NASCITA='" & par.AggiustaData(txtData.Text) & "',USL='" & txtASL.Text & "',GRADO_PARENTELA=" & cmbParenti.SelectedValue & "," _
                    '        & "PERC_INVAL=" & txtInv.Text & ",INDENNITA_ACC='" & cmbAcc.SelectedValue & "',TIPO_INVAL= '" & tipoInval & "',NATURA_INVAL='" & naturaInval & "' WHERE ID= " & txtRiga.Text
                    'par.cmd.ExecuteNonQuery()

                    'par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA where ID_COMPONENTE=" & idCompOld
                    'Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReaderN.Read Then
                    par.cmd.CommandText = "UPDATE COMP_NUCLEO_OSPITI_VSA SET COD_FISCALE='" & txtCF.Text & "',COGNOME='" & RTrim(LTrim(par.PulisciStrSql(txtCognome.Text))) & "',NOME='" & RTrim(LTrim(par.PulisciStrSql(txtNome.Text))) & "',DATA_INIZIO_OSPITE='" & par.AggiustaData(txtDataIngr.Text) & "',DATA_FINE_OSPITE='" & par.AggiustaData(txtDataFine.Text) & "',DATA_NASC='" & par.AggiustaData(txtData.Text) & "',ID_TIPO_IND_RES_DNTE='" & cmbTipoVia.SelectedValue & "',IND_RES_DNTE='" & RTrim(LTrim(par.PulisciStrSql(txtVia.Text))) & "'," _
                        & "CIVICO_RES_DNTE='" & txtCivico.Text & "',COMUNE_RES_DNTE='" & RTrim(LTrim(par.PulisciStrSql(txtComune.Text))) & "',CAP_RES_DNTE= '" & RTrim(LTrim(par.PulisciStrSql(txtCap.Text))) & "',CARTA_I='" & txtDocIdent.Text & "',CARTA_I_DATA='" & par.AggiustaData(txtDataDocI.Text) & "',CARTA_I_RILASCIATA='" & RTrim(LTrim(par.PulisciStrSql(txtRilasciata.Text))) & "',PERMESSO_SOGG_N='" & txtPermSogg.Text & "',PERMESSO_SOGG_DATA='" & par.AggiustaData(txtDataPermSogg.Text) & "',FL_REFERENTE='" & referente & "' WHERE ID= " & idOspOld

                    par.cmd.ExecuteNonQuery()
                    'End If
                    'myReaderN.Close()
                End If

                '**** 30/01/2015 Eliminata insert per le spese come da richiesta num. 1024/2014
                'If txtInv.Text = "100" And cmbAcc.SelectedValue = "1" Then
                '    par.cmd.CommandText = "INSERT INTO COMP_ELENCO_SPESE_VSA (ID,ID_COMPONENTE,DESCRIZIONE,IMPORTO) VALUES (SEQ_COMP_ELENCO_SPESE_VSA.NEXTVAL," & idComponente & ",'',0)"
                '    par.cmd.ExecuteNonQuery()
                'End If
            Else
                'MsgBox("UPDATE COMP_NUCLEO_OSPITI_VSA SET COD_FISCALE='" & txtCF.Text & "',COGNOME='" & RTrim(LTrim(par.PulisciStrSql(txtCognome.Text))) & "',NOME='" & RTrim(LTrim(par.PulisciStrSql(txtNome.Text))) & "',DATA_INIZIO_OSPITE='" & par.AggiustaData(txtDataIngr.Text) & "',DATA_FINE_OSPITE='" & par.AggiustaData(txtDataFine.Text) & "',DATA_NASC='" & par.AggiustaData(txtData.Text) & "',ID_TIPO_IND_RES_DNTE='" & cmbTipoVia.SelectedValue & "',IND_RES_DNTE='" & RTrim(LTrim(par.PulisciStrSql(txtVia.Text))) & "'," & "CIVICO_RES_DNTE=" & txtCivico.Text & ",COMUNE_RES_DNTE='" & RTrim(LTrim(par.PulisciStrSql(txtComune.Text))) & "',CAP_RES_DNTE= '" & RTrim(LTrim(par.PulisciStrSql(txtCap.Text))) & "',CARTA_I='" & txtDocIdent.Text & "',CARTA_I_DATA='" & par.AggiustaData(txtDataDocI.Text) & "',CARTA_I_RILASCIATA='" & RTrim(LTrim(par.PulisciStrSql(txtRilasciata.Text))) & "',PERMESSO_SOGG_N='" & txtPermSogg.Text & "',PERMESSO_SOGG_DATA='" & par.AggiustaData(txtDataPermSogg.Text) & "',FL_REFERENTE='" & referente & "' WHERE ID= " & Request.QueryString("ID"))
                par.cmd.CommandText = "UPDATE COMP_NUCLEO_OSPITI_VSA SET COD_FISCALE='" & txtCF.Text & "',COGNOME='" & RTrim(LTrim(par.PulisciStrSql(txtCognome.Text))) & "',NOME='" & RTrim(LTrim(par.PulisciStrSql(txtNome.Text))) & "',DATA_INIZIO_OSPITE='" & par.AggiustaData(txtDataIngr.Text) & "',DATA_FINE_OSPITE='" & par.AggiustaData(txtDataFine.Text) & "',DATA_NASC='" & par.AggiustaData(txtData.Text) & "',ID_TIPO_IND_RES_DNTE='" & cmbTipoVia.SelectedValue & "',IND_RES_DNTE='" & RTrim(LTrim(par.PulisciStrSql(txtVia.Text))) & "'," _
                        & "CIVICO_RES_DNTE='" & txtCivico.Text & "',COMUNE_RES_DNTE='" & RTrim(LTrim(par.PulisciStrSql(txtComune.Text))) & "',CAP_RES_DNTE= '" & RTrim(LTrim(par.PulisciStrSql(txtCap.Text))) & "',CARTA_I='" & txtDocIdent.Text & "',CARTA_I_DATA='" & par.AggiustaData(txtDataDocI.Text) & "',CARTA_I_RILASCIATA='" & RTrim(LTrim(par.PulisciStrSql(txtRilasciata.Text))) & "',PERMESSO_SOGG_N='" & txtPermSogg.Text & "',PERMESSO_SOGG_DATA='" & par.AggiustaData(txtDataPermSogg.Text) & "',FL_REFERENTE='" & referente & "' WHERE ID= " & Request.QueryString("ID")
                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE=" & txtRiga.Text
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader.Read Then
                '    par.cmd.CommandText = "UPDATE NUOVI_COMP_NUCLEO_VSA set " _
                '                & "DATA_INGRESSO_NUCLEO='" & par.AggiustaData(par.IfEmpty(txtDataIngr.Text, "")) & "'," _
                '                & "ID_TIPO_IND_RES_DNTE=" & par.IfNull(cmbTipoVia.SelectedValue, "") & "," _
                '                & "IND_RES_DNTE='" & par.PulisciStrSql(par.IfEmpty(txtVia.Text, "")) & "'," _
                '                & "CIVICO_RES_DNTE='" & par.IfEmpty(txtCivico.Text, "") & "'," _
                '                & "COMUNE_RES_DNTE='" & par.PulisciStrSql(par.IfEmpty(txtComune.Text, "")) & "'," _
                '                & "CAP_RES_DNTE='" & par.IfEmpty(txtCap.Text, "") & "'," _
                '                & "CARTA_I='" & par.IfEmpty(txtDocIdent.Text, "") & "'," _
                '                & "CARTA_I_DATA='" & par.AggiustaData(par.IfEmpty(txtDataDocI.Text, "")) & "', " _
                '                & "CARTA_I_RILASCIATA='" & par.PulisciStrSql(par.IfEmpty(txtRilasciata.Text, "")) & "', " _
                '                & "PERMESSO_SOGG_N='" & par.IfEmpty(txtPermSogg.Text, "") & "', " _
                '                & "PERMESSO_SOGG_DATA='" & par.AggiustaData(par.IfEmpty(txtDataPermSogg.Text, "")) & "', " _
                '                & "FL_REFERENTE='" & referente & "' " _
                '                & "WHERE ID_COMPONENTE=" & txtRiga.Text

                '    par.cmd.ExecuteNonQuery()
                'Else
                'If cmbNuovoComp.SelectedValue = "1" Then
                '    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKeyK", "alert('Attenzione...trattasi di componente già presente nel nucleo!')", True)
                '    Exit Try
                'End If
                'Else
                '    par.cmd.CommandText = "INSERT INTO NUOVI_COMP_NUCLEO_VSA (ID_COMPONENTE,DATA_INGRESSO_NUCLEO,ID_TIPO_IND_RES_DNTE,IND_RES_DNTE,CIVICO_RES_DNTE,COMUNE_RES_DNTE,CAP_RES_DNTE,CARTA_I,CARTA_I_DATA,CARTA_I_RILASCIATA,PERMESSO_SOGG_N,PERMESSO_SOGG_DATA,FL_REFERENTE) VALUES " _
                '        & "(" & txtRiga.Text & ",'" & par.AggiustaData(par.IfEmpty(txtDataIngr.Text, "")) & "'," & cmbTipoVia.SelectedValue & ",'" & par.PulisciStrSql(par.IfEmpty(txtVia.Text, "")) & "','" & par.IfEmpty(txtCivico.Text, "") & "','" & par.PulisciStrSql(par.IfEmpty(txtComune.Text, "")) & "'," _
                '        & "'" & par.IfEmpty(txtCap.Text, "") & "','" & par.IfEmpty(txtDocIdent.Text, "") & "','" & par.AggiustaData(par.IfEmpty(txtDataDocI.Text, "")) & "','" & par.PulisciStrSql(par.IfEmpty(txtRilasciata.Text, "")) & "','" & par.IfEmpty(txtPermSogg.Text, "") & "','" & par.PulisciStrSql(par.IfEmpty(txtDataPermSogg.Text, "")) & "'," & referente & ")"
                '    par.cmd.ExecuteNonQuery()
                'End If
                'myReader.Close()

            End If

            salvaComponente.Value = "1"


            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey1100", "CloseModal(" & salvaComponente.Value & ");", True)

        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- ScriviComponente" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBoxList Then
                DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next
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
