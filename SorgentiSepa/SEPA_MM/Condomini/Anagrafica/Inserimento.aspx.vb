
Partial Class Condomini_Anagrafica_Inserimento
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../Portale.aspx""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            SettaCampi()

            If Not IsNothing(Request.QueryString("ID")) Then
                vId.Value = Request.QueryString("ID").ToString
                ApriRicerca()
            Else
                Me.DataGridCondAmministr.Visible = False
            End If
            If Session("CHIAMA") = "COND" Then
                Me.btnSalva.Visible = False
            End If
            ControlModifiche() '☺♫☼ Puccia 07/01/2013
        End If
    End Sub
    Private Sub SettaCampi()
        Try

            'APERURA CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select * from siscom_mi.tipi_indirizzo"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Me.cmbTipoInd.Items.Add(New ListItem("", -1))

            While myReader1.Read
                cmbTipoInd.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("id"), -1)))
            End While


            par.cmd.CommandText = "SELECT COD, NOME FROM SEPA.COMUNI_NAZIONI WHERE CAP IS NOT NULL order by nome asc"

            myReader1 = par.cmd.ExecuteReader()
            Me.cmbComune.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbComune.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("COD"), "")))
            End While
            myReader1.Close()
            cmbComune.SelectedValue = "F205"
            Me.txtProvincia.Text = "MI"
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: SettaCampi " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub ApriRicerca()
        Try
            'APERURA CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT COND_AMMINISTRATORI.*, SEPA.COMUNI_NAZIONI.SIGLA  FROM SISCOM_MI.COND_AMMINISTRATORI, SEPA.COMUNI_NAZIONI WHERE COND_AMMINISTRATORI.ID = " & vId.Value & " AND COND_AMMINISTRATORI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+)"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtCognome.Text = myReader1("COGNOME").ToString
                Me.txtNome.Text = myReader1("NOME").ToString
                Me.txtCF.Text = myReader1("CF").ToString
                Me.cmbTipoInd.Items.FindByText(par.IfEmpty(myReader1("TIPO_INDIRIZZO").ToString, "")).Selected = True
                Me.txtIndirizzo.Text = myReader1("INDIRIZZO").ToString
                Me.txtCivico.Text = myReader1("CIVICO").ToString
                Me.txtCap.Text = myReader1("CAP").ToString
                Me.cmbComune.SelectedValue = myReader1("COD_COMUNE")
                Me.txtProvincia.Text = myReader1("SIGLA").ToString
                Me.txtTelefono1.Text = myReader1("TEL_1").ToString
                Me.txtTelefono2.Text = myReader1("TEL_2").ToString
                Me.txtCellulare.Text = myReader1("CELL").ToString
                Me.txtFax.Text = myReader1("FAX").ToString
                Me.txtTitolo.Text = myReader1("TITOLO").ToString
                Me.txtEmail.Text = myReader1("EMAIL").ToString
                Me.txtNote.Text = myReader1("NOTE").ToString
                Me.txtPiva.Text = myReader1("PARTITA_IVA").ToString
                Me.txtRagioneSociale.Text = par.IfNull(myReader1("RAG_SOC_AMM").ToString.ToUpper, "")

            End If


            par.cmd.CommandText = "SELECT CONDOMINI.ID, CONDOMINI.DENOMINAZIONE as CONDOMINIO, " _
                & "TO_CHAR(TO_DATE(cond_amministrazione.data_inizio,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INIZIO, " _
                & "TO_CHAR(TO_DATE(cond_amministrazione.data_fine,'yyyymmdd'),'dd/mm/yyyy') AS DATA_FINE " _
                & "FROM SISCOM_MI.CONDOMINI, SISCOM_MI.COND_AMMINISTRAZIONE " _
                & "WHERE ID_CONDOMINIO = CONDOMINI.ID AND COND_AMMINISTRAZIONE .ID_AMMINISTRATORE = " & vId.Value _
                & " order by data_inizio asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "COND_MOROSITA")

            DataGridCondAmministr.DataSource = ds
            DataGridCondAmministr.DataBind()



            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If Not String.IsNullOrEmpty(Me.txtCognome.Text) And Not String.IsNullOrEmpty(Me.txtNome.Text) And Not String.IsNullOrEmpty(Me.txtIndirizzo.Text) And Not String.IsNullOrEmpty(Me.txtCap.Text) AndAlso Not String.IsNullOrEmpty(Me.txtProvincia.Text) Then

            If par.ControllaCF(Me.txtCF.Text.ToUpper) = False Then
                lblErroreCF.Visible = True
                Exit Sub
            Else
                Me.lblErroreCF.Visible = False
                If Not String.IsNullOrEmpty(Me.txtCF.Text) Then
                    If par.ControllaCFNomeCognome(txtCF.Text, txtCognome.Text, txtNome.Text) = False Then
                        lblErroreCF.Visible = True
                        Exit Sub
                    End If
                End If
            End If



            If vId.Value = 0 Then
                Salva()
            Else
                Update()
            End If
        Else
            Response.Write("<script>alert('Avvalorare tutti i campi obbligatori!');</script>")

        End If
    End Sub

    Private Sub Salva()

        Try
            'APERURA CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_AMMINISTRATORI WHERE CF = '" & par.PulisciStrSql(Me.txtCF.Text.ToUpper) & "'"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Response.Write("<script>alert('Esiste già un amministratore con lo stesso codice fiscale!');</script>")
                myReader1.Close()
                Exit Sub
            End If
            myReader1.Close()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AMMINISTRATORI (ID, COGNOME,NOME,TITOLO,CF,TIPO_INDIRIZZO,INDIRIZZO,CIVICO,COD_COMUNE,CAP,TEL_1,TEL_2,CELL,FAX,EMAIL,NOTE,PARTITA_IVA,RAG_SOC_AMM) " _
            & " VALUES (SISCOM_MI.SEQ_COND_AMMINISTRATORI.NEXTVAL,'" & par.PulisciStrSql(Me.txtCognome.Text.ToUpper) & "','" & par.PulisciStrSql(Me.txtNome.Text.ToUpper) & "','" & par.PulisciStrSql(Me.txtTitolo.Text.ToUpper) & "'," _
            & " '" & par.PulisciStrSql(Me.txtCF.Text.ToUpper) & "', '" & Me.cmbTipoInd.SelectedItem.ToString & "', '" & par.PulisciStrSql(Me.txtIndirizzo.Text) & "', '" & par.PulisciStrSql(Me.txtCivico.Text.ToUpper) & "', '" & par.PulisciStrSql(Me.cmbComune.SelectedValue.ToString) & "'" _
            & " , '" & par.PulisciStrSql(Me.txtCap.Text) & "', '" & par.PulisciStrSql(Me.txtTelefono1.Text) & "', '" & par.PulisciStrSql(Me.txtTelefono2.Text) & "', '" & par.PulisciStrSql(Me.txtCellulare.Text) & "', " _
            & " '" & par.PulisciStrSql(Me.txtFax.Text.ToUpper) & "', '" & par.PulisciStrSql(Me.txtEmail.Text) & "', '" & par.PulisciStrSql(Me.txtNote.Text) & "', '" & par.PulisciStrSql(Me.txtPiva.Text) & "','" & par.PulisciStrSql(Me.txtRagioneSociale.Text.ToUpper) & "')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_COND_AMMINISTRATORI.currval from dual"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vId.Value = myReader1(0)
            End If
            myReader1.Close()

            Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

            txtModificato.Value = 0
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub Update()
        Try
            'APERURA CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "UPDATE SISCOM_MI.COND_AMMINISTRATORI SET COGNOME ='" & par.PulisciStrSql(Me.txtCognome.Text.ToUpper) & "', NOME='" & par.PulisciStrSql(Me.txtNome.Text.ToUpper) & "', TITOLO =  '" & par.PulisciStrSql(Me.txtTitolo.Text.ToUpper) & "', " _
            & "CF='" & par.PulisciStrSql(Me.txtCF.Text.ToUpper) & "', TIPO_INDIRIZZO = '" & Me.cmbTipoInd.SelectedItem.ToString & "', INDIRIZZO= '" & par.PulisciStrSql(Me.txtIndirizzo.Text.ToUpper) & "', CIVICO = '" & par.PulisciStrSql(Me.txtCivico.Text.ToUpper) & "', " _
            & "CAP= '" & par.PulisciStrSql(Me.txtCap.Text) & "', COD_COMUNE ='" & (Me.cmbComune.SelectedValue.ToString) & "', TEL_1 = '" & par.PulisciStrSql(Me.txtTelefono1.Text) & "', " _
            & "TEL_2 = '" & par.PulisciStrSql(Me.txtTelefono2.Text.ToString) & "', CELL = '" & par.PulisciStrSql(Me.txtCellulare.Text.ToString) & "', FAX = '" & par.PulisciStrSql(Me.txtFax.Text.ToString) & "', " _
            & "EMAIL =  '" & par.PulisciStrSql(Me.txtEmail.Text) & "', NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & "', PARTITA_IVA = '" & par.PulisciStrSql(Me.txtPiva.Text) & "', " _
            & "RAG_SOC_AMM = '" & par.PulisciStrSql(Me.txtRagioneSociale.Text.ToUpper) & "' WHERE ID = " & vId.Value

            par.cmd.ExecuteNonQuery()

            Response.Write("<script>alert('Modifica eseguita correttamente!');</script>")
            txtModificato.Value = 0

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        If valorepass = "-1" Then
            RitornaNullSeMenoUno = "NULL"
        Else
            RitornaNullSeMenoUno = valorepass
        End If
        Return RitornaNullSeMenoUno
    End Function

    Protected Sub cmbComune_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbComune.SelectedIndexChanged
        If Me.cmbComune.SelectedValue <> "-1" Then
            'APERURA CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT SIGLA FROM SEPA.COMUNI_NAZIONI WHERE COD = '" & Me.cmbComune.SelectedValue.ToString & "'"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtProvincia.Text = myReader1(0)
            End If
            myReader1.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Else
            Me.txtProvincia.Text = ""
        End If

    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        If txtModificato.Value <> "111" Then  '☺♫☼ Puccia 07/01/2013
            Response.Redirect("../pagina_home.aspx")

        Else
            txtModificato.Value = "1"

        End If
    End Sub

    Private Sub ControlModifiche()    '☺♫☼ Puccia 07/01/2013

        Dim CTRL As Control
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next

    End Sub



End Class
