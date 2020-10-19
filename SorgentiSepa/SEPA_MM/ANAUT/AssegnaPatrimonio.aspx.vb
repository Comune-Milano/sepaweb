
Partial Class ANAUT_AssegnaPatrimonio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
                & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
                & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
                & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
                & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
                & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
                & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
                & "</td></tr></table></div></div>"
        Response.Write(Loading)

        If Not IsPostBack Then
            Response.Flush()
            BindGrid()
        End If
    End Sub

    Private Function BindGrid()
        Try
            Dim SS As String = ""
            Dim ds As New Data.DataSet()
            Dim dlist As CheckBoxList
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            ds.Clear()
            ds.Dispose()
            ListaVoci.Items.Clear()

            'SS = "select UNITA_IMMOBILIARI.ID ,UNITA_IMMOBILIARI.COD_UNITA_IMMIBILIARE from SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.FILIALI_UI WHERE UNITA_IMMOBILIARI.ID=FILIALI_UI.ID_UNTA AND unita_immobiliari.ID NOT IN (SELECT ID_UNITA FROM UTENZA_SPORTELLI_PATRIMONIO,UTENZA_SPORTELLI WHERE UTENZA_SPORTELLI_PATRIMONIO.ID_SPORTELLO=UTENZA_SPORTELLI.ID AND UTENZA_SPORTELLI.FL_ELIMINATO=0 AND UTENZA_SPORTELLI_PATRIMONIO.ID_AU=" & Request.QueryString("AU") & ") order by DENOMINAZIONE asc"
            SS = "SELECT UNITA_IMMOBILIARI.ID ,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO||' INTERNO '||UNITA_IMMOBILIARI.INTERNO AS INDIRIZZO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.FILIALI_UI WHERE FILIALI_UI.INIZIO_VALIDITA<='" & Format(Now, "yyyyMMdd") & "' AND FILIALI_UI.FINE_VALIDITA>='" & Format(Now, "yyyyMMdd") & "' AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=FILIALI_UI.ID_UI AND unita_immobiliari.ID NOT IN (SELECT ID_UNITA FROM UTENZA_SPORTELLI_PATRIMONIO,UTENZA_SPORTELLI WHERE UTENZA_SPORTELLI_PATRIMONIO.ID_SPORTELLO=UTENZA_SPORTELLI.ID AND UTENZA_SPORTELLI.FL_ELIMINATO=0 AND UTENZA_SPORTELLI_PATRIMONIO.ID_AU=" & Request.QueryString("AU") & ") ORDER BY INDIRIZZI.DESCRIZIONE ASC,INTERNO ASC"
            ListaVoci.Items.Clear()
            dlist = ListaVoci
            'dlist = ListaVoci

            da = New Oracle.DataAccess.Client.OracleDataAdapter(SS, par.OracleConn)
            da.Fill(ds)

            dlist.Items.Clear()
            dlist.DataSource = ds
            dlist.DataTextField = "INDIRIZZO"
            dlist.DataValueField = "ID"

            dlist.DataBind()

            da.Dispose()
            da = Nothing

            dlist.DataSource = Nothing
            dlist = Nothing



            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(SS, par.OracleConn)
            'Dim ds As New Data.DataSet()
            'da.Fill(ds, "UTENZA_SPORTELLI_PATRIMONIO")
            'DataGrid1.DataSource = ds
            'DataGrid1.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Function

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans


            Dim j As Integer = 0

            For j = 0 To ListaVoci.Items.Count - 1
                If ListaVoci.Items(j).Selected = True Then
                    
                    par.cmd.CommandText = "Insert into UTENZA_SPORTELLI_PATRIMONIO (ID, ID_SPORTELLO, ID_COMPLESSO, ID_AU,ID_UNITA) Values (SEQ_UTENZA_SPO_PATR.nextval, " & Request.QueryString("SP") & ", NULL, " & Request.QueryString("AU") & "," & ListaVoci.Items(j).Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            'MessJQuery("Operazione effettuata!", 1, "Avviso")
            Response.Write("<script>alert('Operazione effettuata!');self.close();</script>")

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
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
            'lblErrore.Text = ex.Message
            'lblErrore.Visible = True
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

    Public Sub SceltaJQuery(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "")
        Try
            Dim sc As String = ScriptScelta(Messaggio, Funzione, Titolo, Funzione2)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptScelta", sc, True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function ScriptScelta(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptScelta').text('" & Messaggio & "');")
            sb.Append("$('#ScriptScelta').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "', buttons: {'Si': function() { __doPostBack('" & Funzione & "', '');{$(this).dialog('close');} },'No': function() {$(this).dialog('close');" & Funzione2 & "}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
