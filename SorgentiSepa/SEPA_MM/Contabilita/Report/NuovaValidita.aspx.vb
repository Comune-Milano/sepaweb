
Partial Class Contabilita_Report_NuovaValidita
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            caricaTipo()
            caricaCapitolo()
            caricaCompetenza()
            If Request.QueryString("id") <> "" Then
                caricaDescrizione()
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
    Private Sub caricaDescrizione()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = " SELECT " _
                & " T_VOCI_BOLLETTA_TIPI.ID AS TIPO, " _
                & " TO_CHAR(TO_DATE(VALIDITA_DA,'YYYYMMDD'),'DD/MM/YYYY') AS VALIDITA_DA, " _
                & " TO_CHAR(TO_DATE(VALIDITA_A,'YYYYMMDD'),'DD/MM/YYYY') AS VALIDITA_A, " _
                & " T_VOCI_BOLLETTA_CAP.ID AS CAPITOLO, " _
                & " USO, " _
                & " T_VOCI_BOLLETTA_COMPETENZA.ID AS COMPETENZA, " _
                & " '' AS ELIMINA,T_VOCI_BOLLETTA_TIPI_CAP.ID " _
                & " FROM SISCOM_MI.T_VOCI_BOLLETTA_TIPI_CAP,SISCOM_MI.T_VOCI_BOLLETTA_CAP, " _
                & " SISCOM_MI.T_VOCI_BOLLETTA_COMPETENZA,SISCOM_MI.T_VOCI_BOLLETTA_TIPI " _
                & " WHERE T_VOCI_BOLLETTA_TIPI_CAP.ID_TIPO = T_VOCI_BOLLETTA_TIPI.ID " _
                & " AND T_VOCI_BOLLETTA_CAP.ID=T_VOCI_BOLLETTA_TIPI_CAP.ID_CAPITOLO " _
                & " AND T_VOCI_BOLLETTA_COMPETENZA.ID=T_VOCI_BOLLETTA_TIPI_CAP.COMPETENZA " _
                & " and T_VOCI_BOLLETTA_TIPI_CAP.id=" & Request.QueryString("id")

            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LETTORE.Read Then
                DropDownListTipo.SelectedValue = par.IfNull(LETTORE("TIPO"), "")
                TextBoxValiditaDa.Text = par.IfNull(LETTORE("VALIDITA_DA"), "")
                TextBoxValiditaA.Text = par.IfNull(LETTORE("VALIDITA_A"), "")
                DropDownListCapitolo.SelectedValue = par.IfNull(LETTORE("CAPITOLO"), "")
                DropDownListUso.SelectedValue = par.IfNull(LETTORE("USO"), "")
                DropDownListCompetenza.SelectedValue = par.IfNull(LETTORE("COMPETENZA"), "")
            End If
            LETTORE.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante il caricamento!');", True)
        End Try
    End Sub
    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Redirect("../pagina_home.aspx")
    End Sub
    Protected Sub ImageButtonModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonModifica.Click
        Try
            If Len(TextBoxValiditaDa.Text) = 10 And Len(TextBoxValiditaA.Text) = 10 Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If


                par.cmd.CommandText = "UPDATE SISCOM_MI.T_VOCI_BOLLETTA_TIPI_CAP " _
                & " SET " _
                & " ID_TIPO=" & DropDownListTipo.SelectedValue & "," _
                & " ID_CAPITOLO=" & DropDownListCapitolo.SelectedValue & "," _
                & " USO=" & DropDownListUso.SelectedValue & "," _
                & " COMPETENZA=" & DropDownListCompetenza.SelectedValue & "," _
                & " VALIDITA_DA='" & par.AggiustaData(TextBoxValiditaDa.Text) & "'," _
                & " VALIDITA_A='" & par.AggiustaData(TextBoxValiditaA.Text) & "' " _
                & " WHERE ID=" & Request.QueryString("ID")
                par.cmd.ExecuteNonQuery()


                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Redirect("Validita.aspx")

            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Campi obbligatori non compilati correttamente!');", True)
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante la modifica!');", True)
        End Try
    End Sub
    Protected Sub ImageButtonNuova_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonNuova.Click
        Try
            If Len(TextBoxValiditaDa.Text) = 10 And Len(TextBoxValiditaA.Text) = 10 Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If


                par.cmd.CommandText = "INSERT INTO SISCOM_MI.T_VOCI_BOLLETTA_TIPI_CAP " _
                & " (ID,ID_TIPO,ID_CAPITOLO,USO,COMPETENZA,VALIDITA_DA,VALIDITA_A) " _
                & " VALUES(SISCOM_MI.SEQ_T_VOCI_BOLLETTA_TIPI_CAP.NEXTVAL," _
                & DropDownListTipo.SelectedValue & "," _
                & DropDownListCapitolo.SelectedValue & "," _
                & DropDownListUso.SelectedValue & "," _
                & DropDownListCompetenza.SelectedValue & ",'" _
                & par.AggiustaData(TextBoxValiditaDa.Text) & "','" _
                & par.AggiustaData(TextBoxValiditaA.Text) & "'" _
                & ")"
                par.cmd.ExecuteNonQuery()


                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Redirect("Validita.aspx")

            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Campi obbligatori non compilati correttamente!');", True)
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'inserimento!');", True)
        End Try
    End Sub
    Private Sub caricaTipo()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.caricaComboBox("select * from siscom_mi.T_VOCI_BOLLETTA_TIPI order by descrizione", DropDownListTipo, "id", "descrizione", False)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante il caricamento!');", True)
        End Try

    End Sub
    Private Sub caricaCapitolo()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.caricaComboBox("select * from siscom_mi.T_VOCI_BOLLETTA_CAP order by descrizione", DropDownListCapitolo, "id", "descrizione", False)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante il caricamento!');", True)
        End Try
    End Sub
    Private Sub caricaCompetenza()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.caricaComboBox("select * from siscom_mi.T_VOCI_BOLLETTA_COMPETENZA order by descrizione", DropDownListCompetenza, "id", "descrizione", False)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante il caricamento!');", True)
        End Try
    End Sub
End Class
