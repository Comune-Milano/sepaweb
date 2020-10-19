Imports System.IO

Partial Class SPESE_REVERSIBILI_ImportaEmesso
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If controlloProfilo() Then
            If Not IsPostBack Then
                CType(Master.FindControl("TitoloMaster"), Label).Text = "Emesso - Importa"
                CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
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
    End Sub
    Protected Sub btnDownload_Click(sender As Object, e As System.EventArgs) Handles btnDownload.Click
        Try
            connData.apri()
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
            Dim idpiano As Integer = CInt(par.IfNull(par.cmd.ExecuteScalar, 0))
            If idpiano = 0 Then
                'piano finanziario non selezionato in sede di creazione dell'elaborazione
                par.cmd.CommandText = "SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN"
                idpiano = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If
            par.cmd.CommandText = "SELECT SUBSTR(INIZIO,1,4) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID=(SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID=" & idpiano & ")"
            Dim ANNO As String = par.cmd.ExecuteScalar
            par.cmd.CommandText = " SELECT " _
                & " DISTINCT COD_CONTRATTO AS COD_CONTRATTO, " _
                & " (SELECT SUM(IMPORTO_EMESSO) FROM SISCOM_MI.EMESSO_COMPLETO WHERE EMESSO_COMPLETO.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND ID_TIPO_SPESA = 1 AND ID_EMESSO_MESE IN (SELECT ID FROM SISCOM_MI.EMESSO_MESI WHERE ANNO = " & ANNO & ") ) AS SERVIZI, " _
                & " (SELECT SUM(IMPORTO_EMESSO) FROM SISCOM_MI.EMESSO_COMPLETO WHERE EMESSO_COMPLETO.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND ID_TIPO_SPESA = 2 AND ID_EMESSO_MESE IN (SELECT ID FROM SISCOM_MI.EMESSO_MESI WHERE ANNO = " & ANNO & ") ) AS RISCALDAMENTO, " _
                & " (SELECT SUM(IMPORTO_EMESSO) FROM SISCOM_MI.EMESSO_COMPLETO WHERE EMESSO_COMPLETO.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND ID_TIPO_SPESA = 3 AND ID_EMESSO_MESE IN (SELECT ID FROM SISCOM_MI.EMESSO_MESI WHERE ANNO = " & ANNO & ") ) AS ASCENSORE " _
                & " FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.RAPPORTI_UTENZA  " _
                & " WHERE BOL_BOLLETTE.RIFERIMENTO_DA>='" & ANNO & "0101'  " _
                & " AND BOL_BOLLETTE.RIFERIMENTO_A<='" & ANNO & "1231' " _
                & " AND RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportContratti", "ExportContratti", dt)
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnDownload_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnAllega_Click(sender As Object, e As System.EventArgs) Handles btnAllega.Click
        Try
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            Dim xls As New ExcelSiSol
            Dim dtFoglio1 As New Data.DataTable
            Dim nomeFile As String = ""
            If FileUpload1.HasFile = True Then
                nomeFile = Server.MapPath("..\FileTemp\") & FileUpload1.FileName
                FileUpload1.SaveAs(nomeFile)
                If nomeFile <> "" Then
                    Using pck As New OfficeOpenXml.ExcelPackage()
                        Using stream = File.Open(nomeFile, FileMode.Open)
                            pck.Load(stream)
                        End Using
                        Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(1)
                        dtFoglio1 = xls.WorksheetToDataTable(ws, True)
                        ws = pck.Workbook.Worksheets(1)
                        Dim codiceContratto As String = ""
                        Dim servizi As Decimal = 0
                        Dim riscaldamento As Decimal = 0
                        Dim ascensore As Decimal = 0
                        Dim ris As Integer = 0
                        connData.apri(True)
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOLLETTATO_COMPLETO WHERE ID_PF=" & idElaborazione
                        par.cmd.ExecuteNonQuery()
                        For Each riga As Data.DataRow In dtFoglio1.Rows
                            codiceContratto = riga.Item(0)
                            servizi = riga.Item(1)
                            riscaldamento = riga.Item(2)
                            ascensore = riga.Item(3)
                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.BOLLETTATO_COMPLETO ( " _
                                    & " ASCENSORE, RISCALDAMENTO, SERVIZI, ID_PF, id_contratto)  " _
                                    & " VALUES ( " & par.VirgoleInPunti(ascensore) & ", " _
                                    & " " & par.VirgoleInPunti(riscaldamento) & "/* RISCALDAMENTO */, " _
                                    & " " & par.VirgoleInPunti(servizi) & "/* SERVIZI */, " _
                                    & " " & par.VirgoleInPunti(idElaborazione) & "/* ID_PF */, " _
                                    & " (select id from siscom_mi.rapporti_utenza where cod_contratto='" & codiceContratto & "') /* ID_PF */" _
                                    & ")"
                            par.cmd.ExecuteNonQuery()
                        Next
                        par.cmd.CommandText = "UPDATE SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI SET FL_EMESSO_IMPORTATO=1"
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi(True)
                    End Using
                End If
            End If
            RadWindowManager1.RadAlert("Emesso importato correttamente!", 300, 150, "Successo", "", "null")
        Catch ex As Exception
            connData.chiudi(False)
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnAllega_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
