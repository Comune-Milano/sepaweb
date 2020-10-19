
Partial Class ModificaIncassoManuale
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaTipologie()
            caricaDati()
        End If
    End Sub
    Private Sub caricaDati()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INCASSI_NON_ATTRIBUIBILI WHERE ID=" & Request.QueryString("ID")
            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If Lettore.Read Then
                TextBoxNominativo.Text = par.IfNull(Lettore("NOMINATIVO"), "")
                TextBoxImportoIncassato.Text = par.IfNull(Lettore("IMPORTO"), "")
                TextBoxCausale.Text = par.IfNull(Lettore("CAUSALE"), "")
                Dim dataIncasso As String = par.IfNull(Lettore("DATA_INCASSO"), "")
                If dataIncasso <> "" Then
                    dataIncasso = Right(dataIncasso, 2) & "/" & Mid(dataIncasso, 5, 2) & "/" & Left(dataIncasso, 4)
                End If
                TextBoxDataIncasso.Text = dataIncasso
                TextBoxNote.Text = par.IfNull(Lettore("NOTE"), "")
                cmbTipoPagamento.SelectedValue = Lettore("ID_TIPO_PAG")
            End If
            Lettore.Close()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertErrore", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Protected Sub chiudiConnessione()
        If Not IsNothing(par.cmd) Then
            par.cmd.Dispose()
        End If
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Protected Sub ApriConnessione()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Esci", "self.close();", True)
    End Sub

    Protected Sub ImageButtonProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonProcedi.Click
        Try
            If controlloCampi() = "1" Then
                ApriConnessione()
                par.cmd.CommandText = " UPDATE SISCOM_MI.INCASSI_NON_ATTRIBUIBILI SET DATA_INCASSO='" & par.AggiustaData(TextBoxDataIncasso.Text) & "'," _
                    & " NOMINATIVO='" & Replace(TextBoxNominativo.Text, "'", "''") & "'," _
                    & " IMPORTO=" & Replace(Replace(TextBoxImportoIncassato.Text, ".", ""), ",", ".") & "," _
                    & " CAUSALE='" & Replace(TextBoxCausale.Text, "'", "''") & "'," _
                    & " NOTE='" & Replace(TextBoxNote.Text, "'", "''") & "', " _
                    & " ID_TIPO_PAG=" & cmbTipoPagamento.SelectedValue _
                    & " WHERE ID=" & Request.QueryString("ID")
                par.cmd.ExecuteNonQuery()
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msgUpdate", "alert('Incasso modificato correttamente!');", True)
                chiudiConnessione()
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msgUpdate", "alert('" & controlloCampi() & "');", True)
            End If

        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "errUpdate", "alert('Si è verificato un errore durante la modifica dell\'incasso!')", True)
        End Try
    End Sub

    Private Function controlloCampi() As String
        Dim controllo As Boolean = True
        Dim messaggio As String = ""
        Dim indice As Integer = 0
        If TextBoxNominativo.Text = "" Then
            controllo = False
            indice += 1
            messaggio &= "Nominativo"
        End If
        If TextBoxImportoIncassato.Text = "" Then
            controllo = False
            indice += 1
            If indice = 1 Then
                messaggio &= "Importo incassato"
            Else
                messaggio &= ", Importo incassato"
            End If
        End If
        If TextBoxCausale.Text = "" Then
            controllo = False
            indice += 1
            If indice = 1 Then
                messaggio &= "Causale"
            Else
                messaggio &= ", Causale"
            End If
        End If
        If TextBoxDataIncasso.Text = "" Then
            controllo = False
            indice += 1
            If indice = 1 Then
                messaggio &= "Data incasso"
            Else
                messaggio &= ", Data incasso"
            End If
        End If
        If controllo = True Then
            messaggio = "1"
        Else
            If indice = 1 Then
                messaggio = "Il campo " & messaggio & " è obbligatorio!"
            Else
                messaggio = "I campi " & messaggio & " sono obbligatori!"
            End If

        End If
        Return messaggio
    End Function
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        aggiungiFunzioniJavascript()
    End Sub
    Private Sub aggiungiFunzioniJavascript()
        For Each Controllo As Control In Me.form1.Controls
            If TypeOf Controllo Is TextBox Then
                Select Case DirectCast(Controllo, TextBox).Attributes("class")
                    Case "numero"
                        DirectCast(Controllo, TextBox).Attributes.Add("onKeyPress", "javascript:SostPuntVirg(event,this);")
                        DirectCast(Controllo, TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                    Case "data"
                        DirectCast(Controllo, TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    Case Else
                End Select
            End If
        Next
    End Sub
    Private Sub CaricaTipologie()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.TIPO_PAG_PARZ ORDER BY DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            cmbTipoPagamento.DataSource = dt
            cmbTipoPagamento.DataTextField = "DESCRIZIONE"
            cmbTipoPagamento.DataValueField = "ID"
            cmbTipoPagamento.DataBind()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "errLoad", "alert('Si è verificato un errore durante il caricamento della pagina!')", True)
        End Try
    End Sub
End Class
