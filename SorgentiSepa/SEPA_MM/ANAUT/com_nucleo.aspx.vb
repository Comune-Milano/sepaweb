
Partial Class ANAUT_com_nucleo
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
            vIdConnessione = Request.QueryString("IDCONN")
            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            txtProgr.Text = par.Elimina160(Request.QueryString("PR"))
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtASL.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtTelefono1.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtTelefono2.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtmail1.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtmail2.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            Dim lsiFrutto As New ListItem("NO", "0")
            cmbAcc.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("SI", "1")
            cmbAcc.Items.Add(lsiFrutto)
            cmbAcc.SelectedIndex = -1
            cmbAcc.SelectedItem.Text = "NO"


            TESTO = par.Elimina160(Request.QueryString("COGNOME"))
            TESTO = par.Elimina160(Request.QueryString("PARENTI"))

            CaricaParenti()
            If txtOperazione.Text = "1" Then
                txtCognome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("COGNOME"), 1, 50))
                txtNome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("NOME"), 1, 50))
                txtData.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATA"), 1, 10))
                txtCF.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("CF"), 1, 16))
                If Request.QueryString("TIPO_INVAL") <> "" Then
                    cmbTipoInval.Items.FindByText(Request.QueryString("TIPO_INVAL")).Selected = True
                End If
                If Request.QueryString("NATURA_INVAL") <> "" Then
                    cmbNaturaInval.Items.FindByText(Request.QueryString("NATURA_INVAL")).Selected = True
                End If

                cmbParenti.SelectedIndex = -1
                If par.Elimina160(par.RicavaTesto(Request.QueryString("PARENTI"), 1, 50)) <> "" Then
                    cmbParenti.Items.FindByText(par.Elimina160(par.RicavaTesto(Request.QueryString("PARENTI"), 1, 50))).Selected = True
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

                txtTelefono1.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("TELEFONO1"), 1, 20))
                txtTelefono2.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("TELEFONO2"), 1, 20))
                txtmail1.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("MAIL1"), 1, 100))
                txtmail2.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("MAIL2"), 1, 100))

                If Request.QueryString("L104") = "1" Then
                    chkL104.Checked = True
                Else
                    chkL104.Checked = False
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
        SettaControlModifiche(Me)
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
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
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

        If par.ControllaCF(UCase(txtCF.Text)) = False Then
            L4.Visible = True
            L4.Text = "(Errato)"
        Else
            L4.Visible = False
            If par.ControllaValiditaCF(UCase(txtCF.Text), txtCognome.Text, txtNome.Text, txtData.Text) = False Then
                L4.Visible = True
                L4.Text = "(Errato)"
            Else
                L4.Visible = False
            End If
        End If

        If txtInv.Text = "" Then
            txtInv.Text = 0
        End If

        If IsNumeric(txtInv.Text) = True And Val(txtInv.Text) <> 0 Then
            If cmbTipoInval.SelectedValue = "-1" Then
                LTipoInval.Visible = True
            Else
                LTipoInval.Visible = False
            End If
            If cmbNaturaInval.SelectedValue = "-1" Then
                LNaturaInval.Visible = True
            Else
                LNaturaInval.Visible = False
            End If
        End If

        If (cmbTipoInval.SelectedValue <> "-1" Or cmbNaturaInval.SelectedValue <> "-1") And Val(txtInv.Text) = 0 Then
            L5.Visible = True
            L5.Text = "(Valore Numerico)"
        End If


        If L7.Visible = True Or L6.Visible = True Or L5.Visible = True Or L1.Visible = True Or L2.Visible = True Or L3.Visible = True Or L4.Visible = True Or LNaturaInval.Visible = True Or LTipoInval.Visible = True Then
            Exit Sub
        End If



        ScriviComponente()
    End Sub

    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("par_vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnessione") = value
        End Set

    End Property

    Private Sub ScriviComponente()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)


            Dim DataRiferimentoMinori As String = ""
            If IsNothing(Request.QueryString("IDDICH")) = False Then
                par.cmd.CommandText = "SELECT TASSO_RENDIMENTO,ANNO_AU FROM UTENZA_BANDI,UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID=" & Request.QueryString("IDDICH") & " AND UTENZA_BANDI.ID=UTENZA_DICHIARAZIONI.ID_BANDO"
            Else
                par.cmd.CommandText = "SELECT TASSO_RENDIMENTO,ANNO_AU FROM UTENZA_BANDI,UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_COMP_NUCLEO.ID=" & txtRiga.Text & " AND UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE AND UTENZA_BANDI.ID=UTENZA_DICHIARAZIONI.ID_BANDO"
            End If
            Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader11.Read() Then
                DataRiferimentoMinori = par.IfNull(myReader11("ANNO_AU"), Year(Now)) & "1231"
            End If
            myReader11.Close()

            If par.AggiustaData(txtData.Text) > DataRiferimentoMinori Then
                L3.Visible = True
                L3.Text = "(Data superiore al per. di indagine)"
                Exit Sub
            End If


            Dim numProgr As Integer = 0
            Dim tipoInval As String = ""
            Dim naturaInval As String = ""
            Dim idComp As Long = 0

            If CDec(txtInv.Text) = 0 Then
                tipoInval = ""
                naturaInval = ""
            Else
                tipoInval = cmbTipoInval.SelectedValue
                naturaInval = cmbNaturaInval.SelectedItem.Text
            End If

            Dim incrementaProgr As Integer = 0

            Dim FL104 As Integer = 0
            If chkL104.Checked = True Then
                FL104 = 1
            Else
                FL104 = 0
            End If

            If txtOperazione.Text = "0" Then
                par.cmd.CommandText = "SELECT COUNT(ID) AS NUMCOMP FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICH")
                Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderC.Read Then
                    numProgr = par.IfNull(myReaderC("NUMCOMP"), 0)
                End If
                myReaderC.Close()

                If numProgr > 0 Then
                    incrementaProgr = numProgr
                End If

                par.cmd.CommandText = "INSERT INTO UTENZA_COMP_NUCLEO (ID, ID_DICHIARAZIONE, PROGR, COD_FISCALE, COGNOME, NOME, SESSO, DATA_NASCITA, USL, GRADO_PARENTELA, PERC_INVAL, INDENNITA_ACC, TIPO_INVAL, NATURA_INVAL,TELEFONO1,TELEFONO2,EMAIL1,EMAIL2,FL_L104) " _
                    & " VALUES (SEQ_UTENZA_COMP_NUCLEO.NEXTVAL," & Request.QueryString("IDDICH") & "," & incrementaProgr & ",'" & UCase(txtCF.Text) & "','" & RTrim(LTrim(par.PulisciStrSql(UCase(txtCognome.Text)))) & "','" & RTrim(LTrim(par.PulisciStrSql(UCase(txtNome.Text)))) & "','" & par.RicavaSesso(txtCF.Text) & "'," _
                    & "'" & par.AggiustaData(txtData.Text) & "','" & txtASL.Text & "'," & cmbParenti.SelectedValue & "," & txtInv.Text & ",'" & cmbAcc.SelectedValue & "','" & tipoInval & "','" & naturaInval & "','" & RTrim(LTrim(par.PulisciStrSql(txtTelefono1.Text))) & "','" & RTrim(LTrim(par.PulisciStrSql(txtTelefono2.Text))) & "','" & RTrim(LTrim(par.PulisciStrSql(txtmail1.Text))) & "','" & RTrim(LTrim(par.PulisciStrSql(txtmail2.Text))) & "'," & FL104 & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT SEQ_UTENZA_COMP_NUCLEO.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    idComp = myReader(0)
                End If
                myReader.Close()
            Else
                par.cmd.CommandText = "UPDATE UTENZA_COMP_NUCLEO SET TELEFONO1='" & RTrim(LTrim(par.PulisciStrSql(txtTelefono1.Text))) & "',TELEFONO2='" & RTrim(LTrim(par.PulisciStrSql(txtTelefono2.Text))) & "',EMAIL1='" & RTrim(LTrim(par.PulisciStrSql(txtmail1.Text))) & "',EMAIL2='" & RTrim(LTrim(par.PulisciStrSql(txtmail2.Text))) & "',COD_FISCALE='" & txtCF.Text & "',COGNOME='" & RTrim(LTrim(par.PulisciStrSql(txtCognome.Text))) & "',NOME='" & RTrim(LTrim(par.PulisciStrSql(txtNome.Text))) & "',SESSO='" & par.RicavaSesso(txtCF.Text) & "',DATA_NASCITA='" & par.AggiustaData(txtData.Text) & "',USL='" & txtASL.Text & "',GRADO_PARENTELA=" & cmbParenti.SelectedValue & "," _
                    & "PERC_INVAL=" & txtInv.Text & ",INDENNITA_ACC='" & cmbAcc.SelectedValue & "',TIPO_INVAL= '" & tipoInval & "',NATURA_INVAL='" & naturaInval & "',FL_L104=" & FL104 & " WHERE ID= " & txtRiga.Text
                par.cmd.ExecuteNonQuery()

                If txtInv.Text = "100" And cmbAcc.SelectedValue = "1" Then
                    par.cmd.CommandText = "DELETE FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & txtRiga.Text
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO UTENZA_COMP_ELENCO_SPESE (ID,ID_COMPONENTE,DESCRIZIONE,IMPORTO) VALUES (SEQ_UTENZA_COMP_ELENCO_SPESE.NEXTVAL," & txtRiga.Text & ",'',0)"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "DELETE FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & txtRiga.Text
                    par.cmd.ExecuteNonQuery()
                End If
            End If

            If Not IsNothing(par.myTrans) Then
                par.myTrans.Commit()
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

            salvaComponente.Value = "1"
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaComponente.Value & ");", True)

        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- ScriviComponente" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub txtOperazione_TextChanged(sender As Object, e As System.EventArgs) Handles txtOperazione.TextChanged

    End Sub
End Class
