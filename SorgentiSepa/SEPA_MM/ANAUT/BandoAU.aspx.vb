
Partial Class BANDO_BandoERP
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            Response.Expires = 0

            If Not IsPostBack Then
                IdBando = CLng(Request.QueryString("ID"))
                CaricaAnni()
                If IdBando = -1 Then
                    NuovoBando()
                Else
                    VisualizzaBando()
                    If par.DeCripta(Request.QueryString("L")) = "LETTURA" Then
                        ChERP1.Enabled = False
                        ChERP2.Enabled = False
                        ChERP3.Enabled = False
                        ChERP4.Enabled = False
                        ChERP5.Enabled = False
                        ChERP6.Enabled = False
                        ChERP7.Enabled = False
                        ChERP8.Enabled = False
                        Ch43198.Enabled = False
                        Ch39278.Enabled = False
                        ChERP9.Enabled = False
                        cmbAnnoRedditi.Enabled = False
                        txtannoAU.Enabled = False
                        txtDataFine.Enabled = False
                        txtDataInizio.Enabled = False
                        txtDescrizione.Enabled = False
                        btnSalva.Visible = False
                        txtTasso.Enabled = False
                    End If
                End If
            End If
            txtDataFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataFine0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataInizio0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            If IdBando = -1 Then
                HyperLink1.Visible = False
                HyperLink1.NavigateUrl = "javascript:alert('Salvare i dati prima di procedere!');"
            Else
                HyperLink1.Visible = True
                HyperLink1.NavigateUrl = "GestioneTabelleBando.aspx?ID=" & IdBando
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function VisualizzaBando()
        If VerificaLavorazione() = True Then

        Else
            ChERP1.Enabled = False
            ChERP2.Enabled = False
            ChERP3.Enabled = False
            ChERP4.Enabled = False
            ChERP5.Enabled = False
            ChERP6.Enabled = False
            ChERP7.Enabled = False
            ChERP8.Enabled = False
            Ch43198.Enabled = False
            Ch39278.Enabled = False
            cmbAnnoRedditi.Enabled = False
            txtannoAU.Enabled = False
            txtDataFine.Enabled = False
            txtDataInizio.Enabled = False
            txtDescrizione.Enabled = False
            btnSalva.Visible = False
            txtTasso.Enabled = False
            ChERP9.Enabled = False
        End If
    End Function


    Private Function NuovoBando()
        If Verifica() = True Then

        Else
            ChERP1.Enabled = False
            ChERP2.Enabled = False
            ChERP3.Enabled = False
            ChERP4.Enabled = False
            ChERP5.Enabled = False
            ChERP6.Enabled = False
            ChERP7.Enabled = False
            ChERP8.Enabled = False
            Ch43198.Enabled = False
            Ch39278.Enabled = False
            ChERP9.Enabled = False
            cmbAnnoRedditi.Enabled = False
            txtDataFine.Enabled = False
            txtDataInizio.Enabled = False
            txtDescrizione.Enabled = False
            btnSalva.Visible = False
            txtTasso.Enabled = False
            txtannoAU.Enabled = False
        End If
    End Function

    Public Property IdBando() As Long
        Get
            If Not (ViewState("par_IdBando") Is Nothing) Then
                Return CLng(ViewState("par_IdBando"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdBando") = value
        End Set

    End Property

    Private Function VerificaLavorazione() As Boolean
        Try
            VerificaLavorazione = True
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE id=" & IdBando
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If myReader("STATO") <> 0 Then
                    VerificaLavorazione = False
                    MessJQuery("Non è possibile modificare i dati dell\'Anagrafe Utenza perchè risulta essere APERTA o CHIUSA! La modifica è possibile solo quando IN LAVORAZIONE.", 0, "Attenzione")
                Else
                    VerificaLavorazione = True   
                End If

                txtannoAU.Text = par.IfNull(myReader("anno_AU"), "")

                If par.IfNull(myReader("ERP_1"), "") = "1" Then
                    ChERP1.Checked = True
                Else
                    ChERP1.Checked = False
                End If

                If par.IfNull(myReader("ERP_2"), "") = "1" Then
                    ChERP2.Checked = True
                Else
                    ChERP2.Checked = False
                End If

                If par.IfNull(myReader("ERP_3"), "") = "1" Then
                    ChERP3.Checked = True
                Else
                    ChERP3.Checked = False
                End If

                If par.IfNull(myReader("ERP_4"), "") = "1" Then
                    ChERP4.Checked = True
                Else
                    ChERP4.Checked = False
                End If

                If par.IfNull(myReader("ERP_5"), "") = "1" Then
                    ChERP5.Checked = True
                Else
                    ChERP5.Checked = False
                End If

                If par.IfNull(myReader("L43198"), "") = "1" Then
                    Ch43198.Checked = True
                Else
                    Ch43198.Checked = False
                End If

                If par.IfNull(myReader("L39278"), "") = "1" Then
                    Ch39278.Checked = True
                Else
                    Ch39278.Checked = False
                End If

                If par.IfNull(myReader("erp_ff_oo"), "") = "1" Then
                    ChERP6.Checked = True
                Else
                    ChERP6.Checked = False
                End If

                If par.IfNull(myReader("erp_conv"), "") = "1" Then
                    ChERP8.Checked = True
                Else
                    ChERP8.Checked = False
                End If

                If par.IfNull(myReader("erp_art_22"), "") = "1" Then
                    ChERP7.Checked = True
                Else
                    ChERP7.Checked = False
                End If

                If par.IfNull(myReader("OA"), "") = "1" Then
                    ChERP9.Checked = True
                Else
                    ChERP9.Checked = False
                End If


                cmbAnnoRedditi.SelectedIndex = -1
                cmbAnnoRedditi.Items.FindByText(par.IfNull(myReader("anno_isee"), "2011")).Selected = True
                txtDataFine.Text = par.FormattaData(par.IfNull(myReader("data_fine"), ""))
                txtDataInizio.Text = par.FormattaData(par.IfNull(myReader("data_inizio"), ""))
                txtDescrizione.Text = par.IfNull(myReader("descrizione"), "")
                txtTasso.Text = par.IfNull(myReader("tasso_rendimento"), "")
                txtDataInizio0.Text = par.FormattaData(par.IfNull(myReader("inizio_CANONE"), ""))
                txtDataFine0.Text = par.FormattaData(par.IfNull(myReader("FINE_CANONE"), ""))
            Else
                VerificaLavorazione = False
                MessJQuery("Errore durante il recupero dei dati!", 0, "Attenzione")
            End If
                myReader.Close()
                par.OracleConn.Close()
                par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Function


    Private Function Verifica() As Boolean
        Try
            Verifica = True
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE (STATO=1 OR STATO=0) AND id<>" & IdBando
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows Then
                Verifica = False
                MessJQuery("Non è possibile aprire una Anagrafe Utenza se ci sono già altre Anagrafe Utenza aperte o in lavorazione!", 0, "Attenzione")
            Else
                Verifica = True
            End If
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Function

    Private Function CaricaAnni()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT * FROM TAB_TASSO_RENDIMENTO ORDER BY ANNO desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                cmbAnnoRedditi.Items.Add(New ListItem(par.IfNull(myReader("ANNO"), "--"), par.IfNull(myReader("TASSO"), "0")))
            Loop
            myReader.Close()
            txtTasso.Text = cmbAnnoRedditi.SelectedItem.Value
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Function

    Private Sub MessJQuery(ByVal Messaggio As String, ByVal Tipo As Integer, Optional ByVal Titolo As String = "Messaggio")
        Try
            Dim sc As String = ""
            If Tipo = 0 Then
                sc = ScriptErrori(Messaggio, Titolo)
            Else
                sc = ScriptChiudi(Messaggio, Titolo)
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptMsg", sc, True)
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub

    Private Function ScriptErrori(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function ScriptChiudi(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');self.close();}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If (ChERP1.Checked = True Or ChERP2.Checked = True Or ChERP3.Checked = True Or ChERP4.Checked = True Or ChERP5.Checked = True Or Ch39278.Checked = True Or Ch43198.Checked = True) And txtannoAU.Text <> "" And Len(txtannoAU.Text) = 4 And IsNumeric(txtannoAU.Text) = True And txtDataFine.Text <> "" And txtDataInizio.Text <> "" And txtDataFine0.Text <> "" And txtDataInizio0.Text <> "" Then
            SalvaBando()
        Else
            MessJQuery("Valorizzare tutti i campi prima di salvare!", 0, "Attenzione")
        End If
    End Sub

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Function SalvaBando()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If IdBando = -1 Then
                par.cmd.CommandText = "INSERT INTO T_TIPO_PROVENIENZA (ID, DESCRIZIONE) VALUES (SEQ_TIPO_PROVENIENZA.NEXTVAL, 'AU " & txtannoAU.Text & " - REDDITI " & cmbAnnoRedditi.SelectedItem.Text & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT SEQ_TIPO_PROVENIENZA.CURRVAL FROM DUAL"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    par.cmd.CommandText = "Insert into UTENZA_BANDI  (ID, DESCRIZIONE, DATA_INIZIO, DATA_FINE, TASSO_RENDIMENTO,ANNO_ISEE, STATO, ANNO_AU, ERP_1, ERP_2, ERP_3, ERP_4, ERP_5, L43198, L39278,ERP_FF_OO,ERP_CONV,ERP_ART_22,INIZIO_CANONE,FINE_CANONE,OA,ID_TIPO_PROVENIENZA) Values (SEQ_UTENZA_BANDI.NEXTVAL, '" & UCase(par.PulisciStrSql(txtDescrizione.Text)) & "', '" & par.AggiustaData(txtDataInizio.Text) & "', '" & par.AggiustaData(txtDataFine.Text) & "', " & par.VirgoleInPunti(txtTasso.Text) & " , '" & cmbAnnoRedditi.SelectedItem.Text & "', 0, '" & txtannoAU.Text & "'," & Valore01(ChERP1.Checked) & " ," & Valore01(ChERP2.Checked) & "," & Valore01(ChERP3.Checked) & "," & Valore01(ChERP4.Checked) & "," & Valore01(ChERP5.Checked) & "," & Valore01(Ch43198.Checked) & "," & Valore01(Ch39278.Checked) & "," & Valore01(ChERP6.Checked) & "," & Valore01(ChERP8.Checked) & "," & Valore01(ChERP7.Checked) & ",'" & par.AggiustaData(txtDataInizio0.Text) & "','" & par.AggiustaData(txtDataFine0.Text) & "'," & Valore01(ChERP9.Checked) & "," & myReader2(0) & ")"
                End If
                myReader2.Close()
            Else
                par.cmd.CommandText = "UPDATE UTENZA_BANDI SET INIZIO_CANONE='" & par.AggiustaData(txtDataInizio0.Text) & "',FINE_CANONE='" & par.AggiustaData(txtDataFine0.Text) & "',DESCRIZIONE='" & UCase(par.PulisciStrSql(txtDescrizione.Text)) & "', DATA_INIZIO='" & par.AggiustaData(txtDataInizio.Text) & "', DATA_FINE='" & par.AggiustaData(txtDataFine.Text) & "', TASSO_RENDIMENTO=" & par.VirgoleInPunti(txtTasso.Text) & ", ANNO_ISEE='" & cmbAnnoRedditi.SelectedItem.Text & "', ANNO_AU='" & txtannoAU.Text & "', ERP_1=" & Valore01(ChERP1.Checked) & ",ERP_2=" & Valore01(ChERP2.Checked) & ",ERP_3=" & Valore01(ChERP3.Checked) & ",ERP_4=" & Valore01(ChERP4.Checked) & ",ERP_5=" & Valore01(ChERP5.Checked) & ",L43198=" & Valore01(Ch43198.Checked) & ",L39278=" & Valore01(Ch39278.Checked) & ",ERP_FF_OO=" & Valore01(ChERP6.Checked) & ",ERP_CONV=" & Valore01(ChERP8.Checked) & ",ERP_ART_22=" & Valore01(ChERP7.Checked) & ",OA=" & Valore01(ChERP9.Checked) & " WHERE ID=" & IdBando
            End If
            par.cmd.ExecuteNonQuery()

            If IdBando = -1 Then
                par.cmd.CommandText = "SELECT SEQ_UTENZA_BANDI.CURRVAL FROM DUAL"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    par.cmd.CommandText = "INSERT INTO UTENZA_GRUPPI_LAVORO (ID, NOME_GRUPPO, DATA_CREAZIONE, ID_OPERATORE, ID_BANDO_AU, APPLICAZIONE, FL_REGISTRO) VALUES (SEQ_UTENZA_GRUPPI.NEXTVAL, 'REGISTRO AU', '" & Format(Now, "yyyyMMdd") & "', 1, " & myReader1(0) & ", 0, 1)"
                    par.cmd.ExecuteNonQuery()


                End If
                myReader1.Close()


            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            MessJQuery("Operazione effettuata!", 1, "Avviso")


        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Function

   
    Protected Sub cmbAnnoRedditi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAnnoRedditi.SelectedIndexChanged
        txtTasso.Text = cmbAnnoRedditi.SelectedItem.Value
    End Sub
End Class
