
Partial Class VSA_com_uscita
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Not IsPostBack = True Then

            'MotiviUscita()
            Dim TESTO As String
            vIdConnessione = Request.QueryString("IDCONN")
            idDichiarazione = Request.QueryString("IDDICH")
            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtDataNasc.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtDataUscita.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtDataNasc.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataUscita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            TESTO = par.Elimina160(Request.QueryString("COGNOME"))
            If txtOperazione.Text = "1" Then
                txtCognome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("COGNOME"), 1, 25))
                txtNome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("NOME"), 1, 25))
                txtDataNasc.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATA"), 1, 10))
                txtCF.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("CF"), 1, 16))
            End If

            txtCognome.Enabled = False
            txtNome.Enabled = False
            txtCF.Enabled = False
            txtDataNasc.Enabled = False

        End If
        SettaControlModifiche(Me)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        
        If Len(txtDataUscita.Text) <> 10 Then
            lblValorizzaUsc.Visible = True
            lblValorizzaUsc.Text = "(Data non valida (10 car.))"
            Exit Sub
        Else
            lblValorizzaUsc.Visible = False
        End If

        If IsDate(txtDataUscita.Text) = False Then
            lblValorizzaUsc.Visible = True
            lblValorizzaUsc.Text = "(Data non valida)"
            Exit Sub
        Else
            lblValorizzaUsc.Visible = False
        End If

        If cmbMotivoUscita.SelectedValue = "-1" Then
            L5.Visible = True
            Exit Sub
        Else
            L5.Visible = False
        End If

        ScriviCompDaCancell()

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

    Public Property idDichiarazione() As Long
        Get
            If Not (ViewState("par_idDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_idDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idDichiarazione") = value
        End Set

    End Property

    Private Sub ScriviCompDaCancell()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID=" & txtRiga.Text & " AND ID_DICHIARAZIONE=" & idDichiarazione
            Dim myReaderCanc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCanc.Read Then
                par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_CANCELL (ID_DICHIARAZIONE,NOME,COGNOME,DATA_NASCITA,COD_FISCALE,ID_MOTIVO,DATA_USCITA) VALUES " _
                    & "(" & idDichiarazione & ",'" & par.PulisciStrSql(par.IfNull(myReaderCanc("NOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderCanc("COGNOME"), "")) & "'," _
                    & "'" & par.IfNull(myReaderCanc("DATA_NASCITA"), "") & "','" & par.IfNull(myReaderCanc("COD_FISCALE"), "") & "','" _
                    & cmbMotivoUscita.SelectedValue & "','" & par.AggiustaData(par.IfEmpty(txtDataUscita.Text, "")) & "')"
                par.cmd.ExecuteNonQuery()
            End If
            myReaderCanc.Close()

            par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID = " _
                    & "NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE and ID_DICHIARAZIONE=" & idDichiarazione & " AND ID_COMPONENTE=" & txtRiga.Text
            Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderN.Read Then
                par.cmd.CommandText = "DELETE FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE=" & myReaderN("ID_COMPONENTE")
                par.cmd.ExecuteNonQuery()
            End If
            myReaderN.Close()

            par.cmd.CommandText = "DELETE FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & idDichiarazione & " AND ID=" & txtRiga.Text
            par.cmd.ExecuteNonQuery()

            salvaCompElimina.Value = "1"

            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "CloseModal(" & salvaCompElimina.Value & ");", True)

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- ScriviCompDaCancell" & ex.Message)
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

    Private Sub MotiviUscita()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbMotivoUscita.Items.Add(New ListItem("- seleziona -", -1))
            par.cmd.CommandText = "select * from t_causali_domanda_vsa where id_motivo = 1"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                cmbMotivoUscita.Items.Add(New ListItem(par.IfNull(lettore("descrizione"), " "), par.IfNull(lettore("cod"), -1)))
            End While
            lettore.Close()

            cmbMotivoUscita.Items.Add(New ListItem("altro", -2))

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
