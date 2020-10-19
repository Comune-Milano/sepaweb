
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_AggIBAN
    Inherits PageSetIdMode
    Dim idconnessione As String
    Dim idfornitore As String
    Dim idibanselezionato As String
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        labelerrore.Visible = False

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)



        Try
            '----RIPRENDO IDCONNESSIONE E IDFORNITORE---'
            idconnessione = Request.QueryString("IDCONN")
            idfornitore = Request.QueryString("IDFORN")

            If Not IsPostBack Then

                '-----MAIUSCOLO-----'
                txtIBAN.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtMetodo.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtTipo.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                riprendiConnessione()
                riprendiTransazione()

                par.cmd.CommandText = "SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID='" & idfornitore & "'"
                par.cmd.ExecuteNonQuery()
                Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myreader.Read Then
                    nomeFORN.Text = "Nuovo IBAN per il fornitore " & par.IfNull(myreader(0), "")
                End If
                myreader.Close()
                par.cmd.Dispose()


            End If
            If Request.QueryString("M") = "1" Then

                '---RIPRENDO ID IBAN SELEZIONATO---'
                idibanselezionato = Request.QueryString("IDIBANS")

                '---MODIFICA---'
                modificaIBAN.Value = "1"


                If Not IsPostBack Then

                    riprendiConnessione()
                    riprendiTransazione()

                    par.cmd.CommandText = "SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID='" & idfornitore & "'"
                    par.cmd.ExecuteNonQuery()
                    Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        nomeFORN.Text = "Modifica IBAN per il fornitore " & par.IfNull(myreader(0), "")
                    End If
                    myreader.Close()
                    par.cmd.Dispose()

                    '---RICARICO I DATI RELATIVI ALL'IBAN SELEZIONATO----'
                    riprendiConnessione()
                    riprendiTransazione()
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI_IBAN WHERE ID='" & idibanselezionato & "'"
                    par.cmd.ExecuteNonQuery()
                    myreader = par.cmd.ExecuteReader()

                    If myReader.Read Then
                        txtIBAN.Text = par.IfNull(myReader("IBAN"), "")
                        txtMetodo.Text = par.IfNull(myReader("METODO_PAGAMENTO"), "")
                        txtTipo.Text = par.IfNull(myReader("TIPO_PAGAMENTO"), "")
                        ddlStato.SelectedValue = myReader("FL_ATTIVO")
                    End If
                    myReader.Close()
                    par.cmd.Dispose()

                End If


            End If

        Catch ex As Exception
            Session.Add("ERROREMOD", 1)
            Response.Write("<script>window.top.close();</script>")
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

    Protected Sub btnSalva_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try

            Dim IBAN As String = ""
            '----CONTROLLO IBAN NON VUOTO E DI 27 CARATTERI----'
            If Trim(txtIBAN.Text) = "" Then
                labelerrore.Text = "IBAN obbligatorio"
                labelerrore.Visible = True
                'Response.Write("<script>alert('IBAN obbligatorio');</script>")
                Exit Sub
            ElseIf Len(Trim(txtIBAN.Text)) <> 27 Then
                labelerrore.Text = "IBAN deve essere lungo 27 caratteri!"
                labelerrore.Visible = True
                'Response.Write("<script>alert('IBAN deve essere lungo 27 caratteri!');</script>")
                Exit Sub

            End If

            If par.ControllaIBAN(txtIBAN.Text) = False Then
                labelerrore.Text = "IBAN errato"
                labelerrore.Visible = True
                Exit Sub
            End If

            '--------------------------------------------------'

            '---CONTROLLO SE L'IBAN NON SIA STATO GIà INSERITO

            riprendiConnessione()
            riprendiTransazione()

            par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.FORNITORI_IBAN WHERE IBAN='" & Trim(UCase(txtIBAN.Text)) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim ibanSN As Integer = 0
            If myReader.Read Then
                If par.IfNull(myReader(0), 0) <> 0 Then
                    ibanSN = 1
                End If
            End If
            myReader.Close()


            If ibanSN = 0 Then

                '----CONTROLLO SE è UNA MODIFICA O UN NUOVO INSERIMENTO---'
                If modificaIBAN.Value = "0" Then

                    '-------SALVO L'IBAN INSERITO---------'
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI_IBAN(ID,ID_FORNITORE,IBAN,FL_ATTIVO,METODO_PAGAMENTO,TIPO_PAGAMENTO) "
                    par.cmd.CommandText = par.cmd.CommandText & " VALUES (SISCOM_MI.SEQ_FORNITORI_IBAN.NEXTVAL,'" & idfornitore & "','" & par.PulisciStrSql(UCase(txtIBAN.Text)) & "','" & ddlStato.SelectedValue & "','" & par.PulisciStrSql(UCase(txtMetodo.Text)) & "','" & par.PulisciStrSql(UCase(txtTipo.Text)) & "')"
                    par.cmd.ExecuteNonQuery()
                    '--------------------------------------------------'

                ElseIf modificaIBAN.Value = "1" Then

                    '----MODIFICA IBAN SELEZIONATO---'
                    par.cmd.CommandText = "UPDATE SISCOM_MI.FORNITORI_IBAN SET IBAN='" & par.PulisciStrSql(UCase(txtIBAN.Text)) & "',FL_ATTIVO='" & ddlStato.SelectedValue & "',METODO_PAGAMENTO='" & par.PulisciStrSql(UCase(txtMetodo.Text)) & "',TIPO_PAGAMENTO='" & par.PulisciStrSql(UCase(txtTipo.Text)) & "' WHERE ID='" & idibanselezionato & "'"
                    par.cmd.ExecuteNonQuery()
                    '--------------------------------------------------'

                End If

                Session.Add("modificaINDIBAN", 1)

            Else

                par.cmd.CommandText = "SELECT SISCOM_MI.FORNITORI.ID AS IDFORN,SISCOM_MI.FORNITORI_IBAN.ID AS IDIBAN FROM SISCOM_MI.FORNITORI, SISCOM_MI.FORNITORI_IBAN WHERE SISCOM_MI.FORNITORI_IBAN.ID_FORNITORE=SISCOM_MI.FORNITORI.ID AND SISCOM_MI.FORNITORI_IBAN.IBAN='" & txtIBAN.Text & "'"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim idf As Long = 0
                If myReader2.Read Then
                    idf = par.IfNull(myReader2(0), 0)
                End If
                myReader2.Close()

                If idf <> 0 And idf = idfornitore Then
                    'IBAN ASSOCIATO ALLO STESSO FORNITORE
                    labelerrore.Text = "IBAN già associato al fornitore"
                Else
                    'IBAN ASSOCIATO AD UN ALTRO FORNITORE
                    labelerrore.Text = "IBAN associato ad un altro fornitore"
                End If

                labelerrore.Visible = True
                Exit Sub

            End If


                par.cmd.Dispose()
                '----CHIUDO LA FINESTRA----'
                Response.Write("<script>self.close();</script>")
                '--------------------------'
        Catch ex As Exception
            Session.Add("ERROREMOD", 1)
            Response.Write("<script>self.close();</script>")
        End Try
        
    End Sub

End Class
