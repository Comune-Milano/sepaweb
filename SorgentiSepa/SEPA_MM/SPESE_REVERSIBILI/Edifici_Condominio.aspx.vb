Imports System.Data
Imports Telerik.Web.UI
Partial Class SPESE_REVERSIBILI_Edifici_Condominio
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Public Property sStrSqlEdifici() As String
        Get
            If Not (ViewState("par_sStrSqlEdifici") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlEdifici"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_sStrSqlEdifici") = value
        End Set
    End Property
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If controlloProfilo() Then
            If Not IsPostBack Then
                CType(Master.FindControl("TitoloMaster"), Label).Text = "Parametri - Edifici in condominio"
                CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
                HFGriglia.Value = dgvEdifici.ClientID.ToString.Replace("ctl00", "MasterPage")
                CaricaGridUnita()
            End If
        End If
    End Sub
    Private Function controlloProfilo() As Boolean
        'CONTROLLO DELLA SESSIONE OPERATORE E DELL'ABILITAZIONE ALLE SPESE REVERSIBILI
        If Session.Item("OPERATORE") <> "" Then
            If Session.Item("fl_spese_reversibili") = "0" Then
                Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Operatore non abilitato alla gestione delle spese reversibili")
                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
                Return False
            End If
        Else
            RadWindowManager1.RadAlert("Accesso negato o sessione scaduta! E\' necessario rieseguire il login!", 300, 150, "Attenzione", "", "null")
            Return False
        End If
        If Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = 0 Then
            RadWindowManager1.RadAlert("E\' necessario selezionare almeno una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
            Return False
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub solaLettura()
        btnSalva.Enabled = False
    End Sub
    Protected Sub dgvEdifici_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvEdifici.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            CType(dataItem.FindControl("CheckBox1"), CheckBox).Attributes.Add("onclick", "javascript:selezionaCheckSingolo(" & dataItem("ID").Text & ");")
        End If
    End Sub
    Private Sub dgvEdifici_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvEdifici.NeedDataSource
        Try
            connData.apri(False)
            Dim DT As Data.DataTable = Session.Item("DT_EDIFICI")
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: dgvEdifici_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli edifici!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim DT As Data.DataTable = Session.Item("DT_EDIFICI")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(DT)
        dtNuova.Columns.RemoveAt(0)
        dtNuova.Columns(1).ColumnName = "IN CONDOMINIO"
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "Edifici_Condominio", "Edifici_Condominio", DT)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvEdifici.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        dgvEdifici.Rebind()
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            connData.apri()
            AggiornaCheck()
            Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
            For Each riga As Data.DataRow In dtdett.Rows
                If riga.Item("CHECK1") = "TRUE" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.EDIFICI SET FL_IN_CONDOMINIO = 1 WHERE ID = " & riga.Item("ID")
                Else
                    par.cmd.CommandText = "UPDATE SISCOM_MI.EDIFICI SET FL_IN_CONDOMINIO = 0 WHERE ID = " & riga.Item("ID")
                End If
                par.cmd.ExecuteNonQuery()
            Next
            connData.chiudi(True)
            RadWindowManager1.RadAlert("Operazione effettuata correttamente!", 300, 150, "Attenzione", "", "null")
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnSalva_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Private Sub AggiornaCheck()
        Try
            Dim querySelect As String = Trim(HiddenIdEdificio.Value)
            If querySelect.EndsWith("OR") Then
                querySelect = querySelect.Substring(0, querySelect.Length - 2)
            End If
            If Not String.IsNullOrEmpty(querySelect) Then
                Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
                Dim rows = dtdett.Select(querySelect)
                For Each r As Data.DataRow In rows
                    If r.Item("CHECK1") = "TRUE" Then
                        r.Item("CHECK1") = "FALSE"
                    Else
                        r.Item("CHECK1") = "TRUE"
                    End If

                Next
                HiddenIdEdificio.Value = ""
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: AggiornaCheck - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Protected Sub dgvEdifici_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles dgvEdifici.PageIndexChanged
        AggiornaCheck()
        dgvEdifici.CurrentPageIndex = e.NewPageIndex
    End Sub
    Private Sub CaricaGridUnita()
        par.cmd.CommandText = "SELECT ID, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, (CASE WHEN FL_IN_CONDOMINIO = 1 THEN 'TRUE' ELSE 'FALSE' END) AS CHECK1 FROM siscom_mi.edifici where id <> 1 ORDER BY EDIFICI.DENOMINAZIONE ASC"
        Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Session.Item("DT_EDIFICI") = dt
    End Sub
    Protected Sub headerChkbox_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
            hiddenSelTutti.Value = CStr(Not CBool(hiddenSelTutti.Value))
            If hiddenSelTutti.Value.ToUpper = "TRUE" Then
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("CHECK1") = "TRUE"
                Next
            Else
                For Each r As Data.DataRow In dtdett.Rows
                    r.Item("CHECK1") = "FALSE"
                Next
            End If
            dgvEdifici.Rebind()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: headerChkbox_CheckedChanged - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
End Class
