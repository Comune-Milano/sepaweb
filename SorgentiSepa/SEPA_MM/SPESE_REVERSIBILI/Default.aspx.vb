Imports System.Data
Imports Telerik.Web.UI

Partial Class SPESE_REVERSIBILI_Default
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'CONTROLLO DELLA SELEZIONE DI UNA ELABORAZIONE
        If IsNothing(Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")) Then
            Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = "0"
            Session.Item("SPESE_REVERSIBILI_NOTE") = ""
        End If
        If controlloProfilo() Then
            If Not IsPostBack Then
                If Request.QueryString("r") = "1" Then
                    Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = "0"
                    Session.Item("SPESE_REVERSIBILI_NOTE") = ""
                End If
                HFGriglia.Value = DataGridelEborazioni.ClientID.ToString.Replace("ctl00", "MasterPage")
            End If
            'CARICO TUTTE LE ELABORAZIONI PRESENTI
            caricaElementi()
        End If
        'IMPOSTAZIONE DEL TITOLO DELLA PAGINA
        CType(Master.FindControl("TitoloMaster"), Label).Text = "Consuntivi e conguagli - Elaborazioni"
        CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
    End Sub
    Private Function controlloProfilo() As Boolean
        'CONTROLLO DELLA SESSIONE OPERATORE E DELL'ABILITAZIONE ALLE SPESE REVERSIBILI
        If Session.Item("OPERATORE") <> "" Then
            If Session.Item("fl_spese_reversibili") = "0" Then
                Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Operatore non abilitato alla gestione delle spese reversibili")
                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
                Return False
            End If
        Else
            RadWindowManager1.RadAlert("Accesso negato o sessione scaduta! E\' necessario rieseguire il login!", 300, 150, "Attenzione", "", "null")

            Return False
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function

    Protected Sub ButtonClickElaborazione_Click(sender As Object, e As System.EventArgs) Handles ButtonClickElaborazione.Click
        Try
            Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = HiddenFieldIdElaborazione.Value
            connData.apri()
            par.cmd.CommandText = "SELECT NOTE FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & HiddenFieldIdElaborazione.Value
            Session.Item("SPESE_REVERSIBILI_NOTE") = "Elaborazione: " & par.cmd.ExecuteScalar() & " "
            connData.chiudi()
            RadWindowManager1.RadAlert("Elaborazione impostata correttamente!", 300, 150, "Attenzione", "function f(sender,args){loading(0);location.href='ProspettoConsuntivi.aspx';}", "null")
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " ButtonClickElaborazione_Click - " & ex.Message)
        End Try
    End Sub
    Protected Sub ButtonElaborazione_Click(sender As Object, e As System.EventArgs) Handles ButtonElaborazione.Click
        Try
            If HiddenFieldIdElaborazione.Value <> "0" Then
                Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = HiddenFieldIdElaborazione.Value
                connData.apri()
                par.cmd.CommandText = "SELECT NOTE FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & HiddenFieldIdElaborazione.Value
                Session.Item("SPESE_REVERSIBILI_NOTE") = "Elaborazione: " & par.cmd.ExecuteScalar() & " "
                connData.chiudi()
                RadWindowManager1.RadAlert("Elaborazione impostata correttamente!", 300, 150, "Attenzione", "function f(sender,args){loading(0);location.href='ProspettoConsuntivi.aspx';}", "null")
            Else
                Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = HiddenFieldIdElaborazione.Value
                RadWindowManager1.RadAlert("E\' necessario selezionare un\'elaborazione!", 300, 150, "Attenzione", "", "null")
                Session.Item("SPESE_REVERSIBILI_ID_NOTE") = ""
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " ButtonElaborazione_Click - " & ex.Message)
        End Try
    End Sub
    Private Sub caricaElementi()
        'CARICAMENTO DELLE ELABORAZIONI
        If Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = "0" Then
            Session.Item("SPESE_REVERSIBILI_NOTE") = ""
            DataGridelEborazioni.Rebind()
            ButtonElaborazione.Visible = True
            ButtonCambiaElaborazione.Visible = False
            CType(Master.FindControl("LabelContenuto"), Label).Text = ""
        Else
            ButtonElaborazione.Visible = False
            ButtonCambiaElaborazione.Visible = True
            Try
                connData.apri()
                par.cmd.CommandText = " SELECT  " _
                    & " ID," _
                    & " NOTE, " _
                    & " NOME_OPERATORE, " _
                    & " GETDATAORA(DATA_ORA_INIZIO) AS DATA_INIZIO, " _
                    & " GETDATAORA(DATA_ORA_FINE) AS DATA_FINE, " _
                      & " PARZIALE AS PERCENTUALE, " _
                    & " (SELECT TIPO_SPESE_REVERSIBILI.DESCRIZIONE FROM SISCOM_MI.TIPO_SPESE_REVERSIBILI WHERE TIPO_SPESE_REVERSIBILI.ID=TIPO) AS TIPO," _
                    & " (SELECT STATO_SPESE_REVERSIBILI.DESCRIZIONE FROM SISCOM_MI.STATO_SPESE_REVERSIBILI WHERE STATO_SPESE_REVERSIBILI.ID=ID_STATO_APPLICAZIONE) AS OPERAZIONE " _
                    & " FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI " _
                    & " WHERE ELABORAZIONE_SPESE_REVERSIBILI.ID=" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & " ORDER BY ID_PIANO_FINANZIARIO DESC"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    CType(Master.FindControl("LabelContenuto"), Label).Text = "E' stata selezionata l'elaborazione " & par.IfNull(lettore("NOTE"), "") & ". Per modificare questa scelta cliccare su ""Cambia elaborazione""."
                End If
                lettore.Close()
                connData.chiudi()
            Catch ex As Exception
                connData.chiudi()
                Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " caricaElementi - " & ex.Message)
                RadWindowManager1.RadAlert("Elaborazione non caricata correttamente!", 300, 150, "Attenzione", "", "null")
            End Try
        End If
    End Sub
    Protected Sub ButtonCambiaElaborazione_Click(sender As Object, e As System.EventArgs) Handles ButtonCambiaElaborazione.Click
        'NEL CASO DI CAMBIO ELABORAZIONE SI RESETTA LA VARIABILE DI SESSIONE DELL'ELABORAZIONE E SI RIPETE LA RICERCA DELLE ELABORAZIONI
        Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = "0"
        Session.Item("SPESE_REVERSIBILI_NOTE") = ""
        caricaElementi()
    End Sub

    Protected Sub ButtonCreaNuovaElaborazione_Click(sender As Object, e As System.EventArgs) Handles ButtonCreaNuovaElaborazione.Click
        Response.Redirect("CreaElaborazione.aspx", False)
    End Sub
    Private Sub solaLettura()
        ButtonCreaNuovaElaborazione.Enabled = False
    End Sub

    Private Sub btnAggiornaElaborazione_Click(sender As Object, e As EventArgs) Handles btnAggiornaElaborazione.Click
        DataGridelEborazioni.Rebind()
    End Sub

    Private Sub DataGridelEborazioni_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles DataGridelEborazioni.NeedDataSource
        Try
            'CARICAMENTO DELLE SPESE REVERSIBILI
            par.cmd.CommandText = " SELECT  " _
            & " ID," _
            & " NOTE, " _
            & " NOME_OPERATORE, " _
            & " TO_CHAR(TO_DATE(substr(DATA_ORA_INIZIO,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INIZIO, " _
            & " TO_CHAR(TO_DATE(substr(DATA_ORA_FINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_FINE, " _
            & " PARZIALE AS PERCENTUALE, " _
            & " (SELECT TIPO_SPESE_REVERSIBILI.DESCRIZIONE FROM SISCOM_MI.TIPO_SPESE_REVERSIBILI WHERE TIPO_SPESE_REVERSIBILI.ID=TIPO) AS TIPO," _
            & " (SELECT STATO_SPESE_REVERSIBILI.DESCRIZIONE FROM SISCOM_MI.STATO_SPESE_REVERSIBILI WHERE STATO_SPESE_REVERSIBILI.ID=ID_STATO_APPLICAZIONE) AS OPERAZIONE " _
            & " FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI ORDER BY ID_PIANO_FINANZIARIO DESC"
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            If dt.Rows.Count > 0 Then
                TryCast(sender, RadGrid).DataSource = dt
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            Else
                'NON SONO PRESENTI ELABORAZIONI
                RadWindowManager1.RadAlert("Nessuna elaborazione esistente! Prima di procedere con il calcolo delle spese reversibili è necessario creare una nuova elaborazione!", 300, 150, "Attenzione", "", "null")
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " caricaElaborazioni - " & ex.Message)
            RadWindowManager1.RadAlert("Errore durante il caricamento delle elaborazioni!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub

    Private Sub DataGridelEborazioni_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles DataGridelEborazioni.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('MasterPage_ContentPlaceHolder1_HiddenFieldIdElaborazione').value='" & dataItem("ID").Text & "';document.getElementById('MasterPage_TextBoxSelezionato').value='Hai selezionato l\'elaborazione: " & dataItem("NOTE").Text.Replace("'", "\'") & "';")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('MasterPage_ContentPlaceHolder2_ButtonClickElaborazione').click();")
        End If
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGridelEborazioni.AllowPaging = False
        DataGridelEborazioni.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGridelEborazioni.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGridelEborazioni.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGridelEborazioni.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGridelEborazioni.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        DataGridelEborazioni.AllowPaging = True
        DataGridelEborazioni.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGridelEborazioni.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ELABORAZIONI", "ELABORAZIONI", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

    Private Sub SPESE_REVERSIBILI_Default_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        If Not IsPostBack Then
            DataGridelEborazioni.Rebind()
        End If

    End Sub
End Class
