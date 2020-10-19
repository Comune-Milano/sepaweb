Partial Class Contratti_Pagamenti_NuovaTipologiaAnno
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            caricaAnni()
            If Request.QueryString("id") <> "" Then
                caricaAnno()
                ImageButtonNuova.Visible = False
                ImageButtonModifica.Visible = True
            Else
                ImageButtonNuova.Visible = True
                ImageButtonModifica.Visible = False
            End If
        End If
        reimpostaNumerieDate()
    End Sub
    Private Sub reimpostaNumerieDate()
        TextBoxValiditaDa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxValiditaA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub
    Private Sub caricaAnni()
        DropDownListAnno.Items.Clear()
        For I As Integer = 1990 To 2020 Step 1
            DropDownListAnno.Items.Add(I)
        Next
    End Sub
    Private Sub caricaAnno()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT id,to_char(to_date(validita_da,'yyyymmdd'),'dd/mm/yyyy') as validita_da," _
                & " to_char(to_date(validita_a,'yyyymmdd'),'dd/mm/yyyy') as validita_a,anno FROM SISCOM_MI.BOL_BOLLETTE_ES_CONTABILE WHERE ID=" & Request.QueryString("ID")
            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LETTORE.Read Then
                TextBoxValiditaDa.Text = par.IfNull(LETTORE("VALIDITA_DA"), "")
                TextBoxValiditaA.Text = par.IfNull(LETTORE("VALIDITA_A"), "")
                DropDownListAnno.SelectedValue = par.IfNull(LETTORE("ANNO"), "")
            End If
            LETTORE.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore durante il caricamento dell\'anno!')</script>")
        End Try
    End Sub
    Protected Sub ImageButtonNuova_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonNuova.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If Len(TextBoxValiditaDa.Text) = 10 And Len(TextBoxValiditaA.Text) = 10 Then
                par.cmd.CommandText = " INSERT INTO SISCOM_MI.BOL_BOLLETTE_ES_CONTABILE (ID,VALIDITA_DA,VALIDITA_A,ANNO) " _
                    & " VALUES(SISCOM_MI.SEQ_BOL_BOLLETTE_ES_CONTABILE.nextval," & par.AggiustaData(TextBoxValiditaDa.Text) & "," & par.AggiustaData(TextBoxValiditaA.Text) & "," & DropDownListAnno.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Redirect("TipologiaAnno.aspx")
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore durante l\'inserimento dell\'anno di pagamento!')</script>")
        End Try
    End Sub
    Protected Sub ImageButtonModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonModifica.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If Len(TextBoxValiditaDa.Text) = 10 And Len(TextBoxValiditaA.Text) = 10 Then
                par.cmd.CommandText = " UPDATE SISCOM_MI.BOL_BOLLETTE_ES_CONTABILE " _
                    & " SET VALIDITA_DA=" & par.AggiustaData(TextBoxValiditaDa.Text) & "," _
                    & " VALIDITA_A=" & par.AggiustaData(TextBoxValiditaA.Text) & "," _
                    & " ANNO=" & DropDownListAnno.SelectedValue _
                    & " WHERE ID=" & Request.QueryString("ID")
                par.cmd.ExecuteNonQuery()
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Redirect("TipologiaAnno.aspx")
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore durante la modifica dell\'anno!')</script>")
        End Try
    End Sub
    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Redirect("../pagina_home.aspx")
    End Sub
End Class
