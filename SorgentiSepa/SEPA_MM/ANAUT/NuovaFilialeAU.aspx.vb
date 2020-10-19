
Partial Class ANAUT_NuovaFilialeAU
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            Carica()
            BindGrid_F()
        End If
    End Sub

    Private Function Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim IDAU As Long = 0
            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IDBANDO.Value = myReader("ID")
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Sub BindGrid_F()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID NOT IN (SELECT ID_STRUTTURA FROM UTENZA_FILIALI WHERE ID_BANDO=" & IDBANDO.Value & ") ORDER BY NOME ASC", par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "UTENZA_FILIALI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try
            Dim oDataGridItem As DataGridItem
            Dim chkExport As System.Web.UI.WebControls.CheckBox
            Dim i As Integer = 0

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            For Each oDataGridItem In Me.DataGrid1.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                If chkExport.Checked Then

                    par.cmd.CommandText = "INSERT INTO UTENZA_FILIALI (ID,ID_STRUTTURA,ID_BANDO) VALUES (SEQ_UTENZA_FILIALI.NEXTVAL," & oDataGridItem.Cells(2).Text & "," & IDBANDO.Value & ")"
                    par.cmd.ExecuteNonQuery()

                    i = i + 1
                End If
            Next
            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            If i > 0 Then
                MessJQuery("Operazione Effettuata!", 1, "Avviso")
            Else
                MessJQuery("Nessuna filiale selezionata!", 0, "Attenzione")
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub

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

End Class
