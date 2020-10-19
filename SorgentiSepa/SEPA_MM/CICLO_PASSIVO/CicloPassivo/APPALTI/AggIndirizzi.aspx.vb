Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_AggIndirizzi
    Inherits PageSetIdMode
    Dim idconnessione As String
    Dim idfornitore As String
    Dim idindirizzoselezionato As String
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        Try
            '----RIPRENDO IDCONNESSIONE E IDFORNITORE---'
            idconnessione = Request.QueryString("IDCONN")
            idfornitore = Request.QueryString("IDFORN")
            If ddlComuni.SelectedValue <> "Seleziona" Then
                erroreComune.Visible = False
            Else
                erroreComune.Visible = True
            End If
            If ddlProvince.SelectedValue <> "Seleziona" Then
                erroreProvincia.Visible = False
            Else
                erroreComune.Visible = True
            End If
            errorecap.Visible = False
            erroreIndirizzo.Visible = False



            If Not IsPostBack Then
                riprendiConnessione()
                riprendiTransazione()

                'par.cmd.CommandText = "SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID='" & idfornitore & "'"
                'par.cmd.ExecuteNonQuery()
                'Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                'If myreader.Read Then
                '    nomeFORN.Text = "Nuovo indirizzo per il fornitore " & par.IfNull(myreader(0), "")
                'End If
                'myreader.Close()
                'par.cmd.Dispose()

                '---CARICO LE PROVINCE---'
                ddlProvince.Items.Clear()
                ddlProvince.Items.Add(New ListItem("Seleziona", "Seleziona"))

                par.cmd.CommandText = "SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA"
                Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                While myReader.Read
                    ddlProvince.Items.Add(par.IfNull(myReader(0), ""))
                End While
                myReader.Close()
                par.cmd.Dispose()

                ddlComuni.Items.Clear()
                ddlComuni.Items.Add(New ListItem("Seleziona", "Seleziona"))

                '-----MAIUSCOLO-----'
                txtCap.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                txtIndirizzo.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                txtTelefono1.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtTelefono2.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtFax.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            End If

            If Request.QueryString("M") = "1" Then

                '---RIPRENDO ID INDIRIZZO SELEZIONATO---'
                idindirizzoselezionato = Request.QueryString("IDINDS")
                indSELEZIONATO.Value = idindirizzoselezionato

                '---MODIFICA---'
                modificaINDIRIZZO.Value = "1"

                If Not IsPostBack Then

                    '---RICARICO I DATI RELATIVI ALL'ID INDIRIZZO SELEZIONATO---'
                    riprendiConnessione()
                    riprendiTransazione()
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI_INDIRIZZI,SISCOM_MI.FORNITORI WHERE SISCOM_MI.FORNITORI_INDIRIZZI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID AND SISCOM_MI.FORNITORI_INDIRIZZI.ID='" & idindirizzoselezionato & "'"
                    par.cmd.ExecuteNonQuery()
                    Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        '   nomeFORN.Text = "Modifica indirizzo per il fornitore " & par.IfNull(myreader("RAGIONE_SOCIALE"), "")
                        txtIndirizzo.Text = par.IfNull(myreader("INDIRIZZO"), "")
                        txtCap.Text = par.IfNull(myreader("CAP"), "")
                        txtFax.Text = par.IfNull(myreader("FAX"), "")
                        txtTelefono1.Text = par.IfNull(myreader("TELEFONO_1"), "")
                        txtTelefono2.Text = par.IfNull(myreader("TELEFONO_2"), "")
                        ddlProvince.SelectedValue = par.IfNull(myreader("PR"), "")

                        txtMail.Text = par.IfNull(myreader("email"), "")

                        '---CARICAMENTO COMUNI DELLA PROVINCIA---'
                        par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & ddlProvince.SelectedValue & "' ORDER BY NOME"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        While myReader2.Read
                            ddlComuni.Items.Add(par.IfNull(myReader2(0), ""))
                        End While
                        myReader2.Close()
                        ddlComuni.SelectedValue = par.IfNull(myreader("COMUNE"), "")

                    End If
                    myreader.Close()
                    par.cmd.Dispose()
                End If
            End If

        Catch ex As Exception
            Session.Add("ERROREMOD", 1)
            Response.Write("<script>self.close();</script>")

        End Try

    End Sub

    Protected Sub riprendiConnessione()

        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & idconnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)

    End Sub

    Protected Sub riprendiTransazione()

        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONEFORNITORI" & idconnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

    End Sub

    'Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
    '    '---SE ANNULLO CHIUDO LA FINESTRA---'
    '    Response.Write("<script>window.top.close();</script>")

    'End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try

            '--------------CONTROLLO CAMPI INSERITI------------'
            Dim errore As Boolean = False
            'If ddlProvince.SelectedValue = "Seleziona" Then
            '    erroreProvincia.Visible = True
            '    errore = True
            'End If

            'If ddlComuni.SelectedValue = "Seleziona" Then
            '    erroreComune.Visible = True
            '    errore = True
            'End If

            riprendiConnessione()
            riprendiTransazione()
            par.cmd.CommandText = "SELECT COD FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(Trim(ddlComuni.SelectedValue)) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Dim codicecomune As String = ""
            If myReader.Read Then
                codicecomune = par.IfNull(myReader(0), "")
            End If
            myReader.Close()
            par.cmd.Dispose()

            'Dim validCap As String = ""
            'If noCAP.Value = "0" Then
            '    If par.ControllaCAP(codicecomune, Me.txtCap.Text, validCap) = False Then
            '        errorecap.Visible = True
            '        errore = True
            '        If Not (erroreIndirizzo.Visible = True Or erroreComune.Visible = True Or erroreProvincia.Visible = True) Then
            '            Response.Write("<script>alert('CAP errato!I possibili valori sono:\n" & validCap & "');</script>")
            '        End If
            '    End If
            'End If




            'If par.ControllaCAP(, txtCap.Text, "") = False Then
            '    errorecap.Visible = True
            '    errore = True
            'End If


            If errore = True Then
                Exit Sub
            End If


            'If Len(Trim(txtIndirizzo.Text)) = 0 Then
            '    'INDIRIZZO NON INSERITO
            '    erroreIndirizzo.Text = "Indirizzo obbligatorio"
            '    erroreIndirizzo.Visible = True
            '    Exit Sub
            'Else

            If modificaINDIRIZZO.Value = "0" Then
                ' SALVO L'INDIRIZZO INSERITO
                'PRIMA DI SALVARE CONTROLLO CHE NON SIA STATO GIà INSERITO LO STESSO INDIRIZZO

                par.cmd.CommandText = "SELECT COUNT(SISCOM_MI.FORNITORI.ID) FROM SISCOM_MI.FORNITORI, SISCOM_MI.FORNITORI_INDIRIZZI WHERE SISCOM_MI.FORNITORI_INDIRIZZI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID AND SISCOM_MI.FORNITORI_INDIRIZZI.INDIRIZZO='" & par.PulisciStrSql(UCase(Trim(txtIndirizzo.Text))) & "' AND SISCOM_MI.FORNITORI.ID='" & idfornitore & "' AND PR='" & par.PulisciStrSql(UCase(Trim(ddlProvince.SelectedValue))) & "' AND COMUNE='" & par.PulisciStrSql(UCase(Trim(ddlComuni.SelectedValue))) & "' AND CAP='" & par.PulisciStrSql(Trim(txtCap.Text)) & "'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim indSN As Integer = 0
                If myReader2.Read Then
                    If par.IfNull(myReader2(0), 0) <> 0 Then
                        indSN = 1
                    End If
                End If
                myReader2.Close()

                If indSN = 0 Then
                    '-------SALVO L'INDIRIZZO INSERITO---------'

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI_INDIRIZZI(ID,ID_FORNITORE,INDIRIZZO,CAP,COMUNE,PR,TELEFONO_1,TELEFONO_2,FAX,EMAIL) "
                    par.cmd.CommandText = par.cmd.CommandText & " VALUES (SISCOM_MI.SEQ_FORNITORI_INDIRIZZI.NEXTVAL,'" & idfornitore & "','" & par.PulisciStrSql(UCase(txtIndirizzo.Text)) & "','" & par.PulisciStrSql(UCase(txtCap.Text)) & "','" & par.PulisciStrSql(UCase(ddlComuni.SelectedValue)) & "','" & par.PulisciStrSql(UCase(ddlProvince.SelectedValue)) & "','" & par.PulisciStrSql(UCase(txtTelefono1.Text)) & "','" & par.PulisciStrSql(UCase(txtTelefono2.Text)) & "','" & par.PulisciStrSql(UCase(txtFax.Text)) & "','" & par.PulisciStrSql(UCase(txtMail.Text)) & "')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.Dispose()
                    '--------------------------------------------------'

                    If Not IsNothing(Session.Item("modificaINDIBAN")) Then
                        Session.Item("modificaINDIBAN") = 1
                    Else
                        Session.Add("modificaINDIBAN", 1)
                    End If

                Else
                    'INDIRIZZO GIà ASSOCIATO AL FORNITORE

                    erroreIndirizzo.Text = "Indirizzo già associato al fornitore"
                    erroreIndirizzo.Visible = True
                    Exit Sub

                End If


            ElseIf modificaINDIRIZZO.Value = "1" Then
                'MODIFICA IBAN SELEZIONATO
                'par.cmd.CommandText = "UPDATE SISCOM_MI.FORNITORI_INDIRIZZI SET EMAIL='" & par.PulisciStrSql(UCase(txtMail.Text)) & "', INDIRIZZO='" & par.PulisciStrSql(txtIndirizzo.Text) & "',CAP='" & par.PulisciStrSql(txtCap.Text) & "',COMUNE='" & par.PulisciStrSql(ddlComuni.SelectedValue) & "',PR='" & par.PulisciStrSql(ddlProvince.SelectedValue) & "',TELEFONO_1='" & par.PulisciStrSql(txtTelefono1.Text) & "',TELEFONO_2='" & par.PulisciStrSql(txtTelefono2.Text) & "',FAX='" & par.PulisciStrSql(txtFax.Text) & "' WHERE ID='" & indSELEZIONATO.Value & "'"
                par.cmd.CommandText = "UPDATE SISCOM_MI.FORNITORI_INDIRIZZI SET EMAIL='" & par.PulisciStrSql(UCase(txtMail.Text)) & "',TELEFONO_2='" & par.PulisciStrSql(txtTelefono2.Text) & "',FAX='" & par.PulisciStrSql(txtFax.Text) & "' WHERE ID='" & indSELEZIONATO.Value & "'"

                par.cmd.ExecuteNonQuery()
                par.cmd.Dispose()

                If Not IsNothing(Session.Item("modificaINDIBAN")) Then
                    Session.Item("modificaINDIBAN") = 1
                Else
                    Session.Add("modificaINDIBAN", 1)
                End If

            End If






            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI, SISCOM_MI.FORNITORI_INDIRIZZI WHERE SISCOM_MI.FORNITORI_INDIRIZZI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID AND SISCOM_MI.FORNITORI_INDIRIZZI.INDIRIZZO='" & par.PulisciStrSql(txtIndirizzo.Text) & "' AND SISCOM_MI.FORNITORI.ID='" & idfornitore & "' AND PR='" & ddlProvince.SelectedValue & "' AND COMUNE='" & par.PulisciStrSql(ddlComuni.SelectedValue) & "' AND CAP='" & par.PulisciStrSql(txtCap.Text) & "'"
            'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'Dim indSN As Integer = 0
            'If myReader2.Read Then
            '    If par.IfNull(myReader2(0), 0) <> 0 Then
            '        indSN = 1
            '    End If
            'End If
            'myReader2.Close()

            'If indSN = 0 Then
            '    If modificaINDIRIZZO.Value = "0" Then

            '        '-------SALVO L'INDIRIZZO INSERITO---------'

            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI_INDIRIZZI(ID,ID_FORNITORE,INDIRIZZO,CAP,COMUNE,PR,TELEFONO_1,TELEFONO_2,FAX) "
            '        par.cmd.CommandText = par.cmd.CommandText & " VALUES (SISCOM_MI.SEQ_FORNITORI_INDIRIZZI.NEXTVAL,'" & idfornitore & "','" & par.PulisciStrSql(txtIndirizzo.Text) & "','" & par.PulisciStrSql(txtCap.Text) & "','" & par.PulisciStrSql(ddlComuni.SelectedValue) & "','" & par.PulisciStrSql(ddlProvince.SelectedValue) & "','" & par.PulisciStrSql(txtTelefono1.Text) & "','" & par.PulisciStrSql(txtTelefono2.Text) & "','" & par.PulisciStrSql(txtFax.Text) & "')"
            '        par.cmd.ExecuteNonQuery()
            '        par.cmd.Dispose()
            '        '--------------------------------------------------'

            '    ElseIf modificaINDIRIZZO.Value = "1" Then

            '        '---MODIFICA IBAN SELEZIONATO---'

            '        par.cmd.CommandText = "UPDATE SISCOM_MI.FORNITORI_INDIRIZZI SET INDIRIZZO='" & par.PulisciStrSql(txtIndirizzo.Text) & "',CAP='" & par.PulisciStrSql(txtCap.Text) & "',COMUNE='" & par.PulisciStrSql(ddlComuni.SelectedValue) & "',PR='" & par.PulisciStrSql(ddlProvince.SelectedValue) & "',TELEFONO_1='" & par.PulisciStrSql(txtTelefono1.Text) & "',TELEFONO_2='" & par.PulisciStrSql(txtTelefono2.Text) & "',FAX='" & par.PulisciStrSql(txtFax.Text) & "' WHERE ID='" & indSELEZIONATO.Value & "'"

            '        par.cmd.ExecuteNonQuery()
            '        par.cmd.Dispose()

            '    End If

            '    'CType(Me.Page.FindControl("modificheEffettuate"), HiddenField).Value = "1"

            '    Session.Add("modificaINDIBAN", 1)

            'Else
            '    'INDIRIZZO GIà ASSOCIATO AL FORNITORE

            '    erroreIndirizzo.Text = "Indirizzo già associato al fornitore"
            '    erroreIndirizzo.Visible = True
            '    Exit Sub

            'End If
            'End If






            '----CHIUDO LA FINESTRA----'
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "CloseAndRefresh('Tab_Indirizzi_btnApriInd');", True)
            '--------------------------'
        Catch ex As Exception
            Session.Add("ERROREMOD", 1)
            Response.Write("<script>self.close();</script>")
        End Try


    End Sub

    Protected Sub ddlProvince_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvince.SelectedIndexChanged
        txtCap.Text = ""

        Try

            ddlComuni.Items.Clear()
            ddlComuni.Items.Add(New ListItem("Seleziona", "Seleziona"))
            riprendiConnessione()
            riprendiTransazione()
            par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & ddlProvince.SelectedValue & "' ORDER BY NOME"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            While myReader.Read

                ddlComuni.Items.Add(par.IfNull(myReader(0), ""))

            End While
            myReader.Close()
            par.cmd.Dispose()

        Catch ex As Exception

            Session.Add("ERROREMOD", 1)
            Response.Write("<script>self.close();</script>")

        End Try

    End Sub

    Protected Sub ddlComuni_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlComuni.SelectedIndexChanged
        Try

            riprendiConnessione()
            riprendiTransazione()
            par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.TAB_CAP2 WHERE COMUNE='" & par.PulisciStrSql(ddlComuni.SelectedValue) & "' AND PROV='" & ddlProvince.SelectedValue & "' "
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim NRIS As Integer
            If myReader.Read Then
                NRIS = par.IfNull(myReader(0), "0")
            End If
            myReader.Close()

            If NRIS = 1 Then
                par.cmd.CommandText = "SELECT CAP FROM SISCOM_MI.TAB_CAP2 WHERE COMUNE='" & par.PulisciStrSql(ddlComuni.SelectedValue) & "' AND PROV='" & ddlProvince.SelectedValue & "' "
                myReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    txtCap.Text = par.IfNull(myReader(0), "")
                End If
                myReader.Close()
            Else
                noCAP.Value = "1"
                txtCap.Text = ""
            End If
            par.cmd.Dispose()

        Catch ex As Exception

            Session.Add("ERROREMOD", 1)
            Response.Write("<script>self.close();</script>")

        End Try



    End Sub




End Class
