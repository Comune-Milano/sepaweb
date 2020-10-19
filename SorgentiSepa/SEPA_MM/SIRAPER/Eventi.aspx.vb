Imports System.IO

Partial Class SIRAPER_Eventi
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Public Property dataTableEventi() As Data.DataTable
        Get
            If Not (ViewState("dataTableEventi") Is Nothing) Then
                Return ViewState("dataTableEventi")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dataTableEventi") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                CercaEventi()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CercaEventi()
        Try
            par.cmd.CommandText = "SELECT TO_CHAR(TO_DATE(SUBSTR(SIRAPER_EVENTI.DATA_ORA,1,8),'YYYYmmdd'),'DD/MM/YYYY')||' '||SUBSTR(SIRAPER_EVENTI.DATA_ORA,9,2)||':'||SUBSTR(SIRAPER_EVENTI.DATA_ORA,11,2)||':'||SUBSTR(SIRAPER_EVENTI.DATA_ORA,13,2) AS DATA_ORA, " _
                                & "SIRAPER_EVENTI.COD_EVENTO AS CODICE, TAB_EVENTI.DESCRIZIONE AS TIPO, OPERATORI.OPERATORE, SIRAPER_EVENTI.DESCRIZIONE " _
                                & "FROM SISCOM_MI.SIRAPER_EVENTI, TAB_EVENTI, OPERATORI " _
                                & "WHERE TAB_EVENTI.COD(+) = SIRAPER_EVENTI.COD_EVENTO AND OPERATORI.ID(+) = SIRAPER_EVENTI.ID_OPERATORE AND ID_SIRAPER = " & Request.QueryString("ID") & " " _
                                & "ORDER BY SIRAPER_EVENTI.DATA_ORA DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            dataTableEventi = New Data.DataTable
            da.Fill(dataTableEventi)
            da.Dispose()
            BindGrid()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - CercaEventi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            dgvEventi.DataSource = dataTableEventi
            dgvEventi.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - BindGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub dgvEventi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvEventi.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            dgvEventi.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        If dgvEventi.Items.Count > 0 Then
            Dim nomeFile As String = par.EsportaExcelDaDTWithDatagrid(dataTableEventi, Me.dgvEventi, "ExportEventi" & Request.QueryString("EVENTO"), , , , False)
            If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "downloadFile('../FileTemp/" & nomeFile & "');document.body.style.backgroundImage = 'url(Immagini/Sfondo.png)';document.body.style.backgroundRepeat = 'repeat-x';", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');document.body.style.backgroundImage = 'url(Immagini/Sfondo.png)';document.body.style.backgroundRepeat = 'repeat-x';", True)
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Nessun risultato da esportare!');document.body.style.backgroundImage = 'url(Immagini/Sfondo.png)';document.body.style.backgroundRepeat = 'repeat-x';", True)
        End If
    End Sub
End Class
