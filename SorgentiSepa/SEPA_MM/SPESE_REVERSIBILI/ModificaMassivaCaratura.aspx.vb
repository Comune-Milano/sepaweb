Imports System.IO

Partial Class SPESE_REVERSIBILI_ModificaMassivaCaratura
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If controlloProfilo() Then
            If Not IsPostBack Then
                CType(Master.FindControl("TitoloMaster"), Label).Text = "CDR - Modifica massiva"
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
            RadWindowManager1.RadAlert("E\' necessario selezionare una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
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
            par.cmd.CommandText = "SELECT COD_UNITA_IMMOBILIARE,SERVIZI_COMPLESSO,SERVIZI_EDIFICIO,RISCALDAMENTO,ASCENSORE,MONTASCALE," _
                                & " (SELECT EDIFICI.DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID IN " _
                                & " (SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.id = SCHEMA_CARATURE.id_unita)) AS DENOMINAZIONE, " _
                                & " (SELECT SCALE_EDIFICI.DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID in " _
                                & " (SELECT ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.id = SCHEMA_CARATURE.id_unita))  AS SCALA, " _
                                & " (SELECT MAX(INTERNO) FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.id = SCHEMA_CARATURE.id_unita) AS INTERNO " _
                                & "FROM SISCOM_MI.SCHEMA_CARATURE"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()

            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportCDR", "ExportCDR", dt)
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                & " VALUES (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & Tempo & "', '-'," _
                & "'-', '-' , 25)"
                par.cmd.ExecuteNonQuery()
            Else
                RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
            End If
            connData.chiudi()
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnDownload_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)

    End Sub
    Protected Sub btnAllega_Click(sender As Object, e As System.EventArgs) Handles btnAllega.Click
        Try
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
                        Dim codiceUnitaImmobiliare As String = ""
                        Dim serviziComplesso As Decimal = 0
                        Dim serviziEdifici As Decimal = 0
                        Dim riscaldamento As Decimal = 0
                        Dim ascensore As Decimal = 0
                        Dim montascale As Decimal = 0
                        Dim ris As Integer = 0
                        connData.apri(True)
                        For Each riga As Data.DataRow In dtFoglio1.Rows
                            codiceUnitaImmobiliare = riga.Item(0)
                            serviziComplesso = riga.Item(1)
                            serviziEdifici = riga.Item(2)
                            riscaldamento = riga.Item(3)
                            ascensore = riga.Item(4)
                            montascale = riga.Item(5)
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SCHEMA_CARATURE " _
                                & " SET SERVIZI_COMPLESSO=" & par.VirgoleInPunti(serviziComplesso) & "," _
                                & " SERVIZI_EDIFICIO=" & par.VirgoleInPunti(serviziEdifici) & "," _
                                & " ASCENSORE=" & par.VirgoleInPunti(ascensore) & "," _
                                & " RISCALDAMENTO=" & par.VirgoleInPunti(riscaldamento) & "," _
                                & " MONTASCALE=" & par.VirgoleInPunti(montascale) _
                                & " WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "' "
                            ris = par.cmd.ExecuteNonQuery()
                            If ris = 0 Then
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.SCHEMA_CARATURE ( " _
                                    & " ASCENSORE, RISCALDAMENTO, SERVIZI_EDIFICIO,  " _
                                    & " SERVIZI_COMPLESSO, COD_UNITA_IMMOBILIARE, ID_UNITA, MONTASCALE)  " _
                                    & " VALUES ( " & par.VirgoleInPunti(ascensore) & ", " _
                                    & " " & par.VirgoleInPunti(riscaldamento) & "/* RISCALDAMENTO */, " _
                                    & " " & par.VirgoleInPunti(serviziEdifici) & "/* SERVIZI_EDIFICIO */, " _
                                    & " " & par.VirgoleInPunti(serviziComplesso) & "/* SERVIZI_COMPLESSO */, " _
                                    & " '" & codiceUnitaImmobiliare & "'/* COD_UNITA_IMMOBILIARE */, " _
                                    & " (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "')," & par.VirgoleInPunti(montascale) & ")"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE ( " _
                                    & " SUP_CATASTALE, SUP_NETTA, MODIFICA_MANUALE,  " _
                                    & " FINE_VALIDITA, INIZIO_VALIDITA, VALORE_CARATURA,  " _
                                    & " ID_TIPOLOGIA_CARATURA, ID_UNITA, ID)  " _
                                    & " VALUES (GETDIMENSIONE((SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'),'SUP_CONV') /* SUP_CATASTALE */, " _
                                    & " GETDIMENSIONE((SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'),'SUP_NETTA') /* SUP_NETTA */, " _
                                    & " 1, " _
                                    & " '30000000', " _
                                    & " '19000101', " _
                                    & par.VirgoleInPunti(serviziComplesso) & ", " _
                                    & " 1, " _
                                    & " (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'), " _
                                    & " SISCOM_MI.SEQ_CARATURE.NEXTVAL) "
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE ( " _
                                    & " SUP_CATASTALE, SUP_NETTA, MODIFICA_MANUALE,  " _
                                    & " FINE_VALIDITA, INIZIO_VALIDITA, VALORE_CARATURA,  " _
                                    & " ID_TIPOLOGIA_CARATURA, ID_UNITA, ID)  " _
                                    & " VALUES (GETDIMENSIONE((SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'),'SUP_CONV') /* SUP_CATASTALE */, " _
                                    & " GETDIMENSIONE((SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'),'SUP_NETTA') /* SUP_NETTA */, " _
                                    & " 1, " _
                                    & " '30000000', " _
                                    & " '19000101', " _
                                    & par.VirgoleInPunti(serviziEdifici) & ", " _
                                    & " 4, " _
                                    & " (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'), " _
                                    & " SISCOM_MI.SEQ_CARATURE.NEXTVAL) "
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE ( " _
                                    & " SUP_CATASTALE, SUP_NETTA, MODIFICA_MANUALE,  " _
                                    & " FINE_VALIDITA, INIZIO_VALIDITA, VALORE_CARATURA,  " _
                                    & " ID_TIPOLOGIA_CARATURA, ID_UNITA, ID)  " _
                                    & " VALUES (GETDIMENSIONE((SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'),'SUP_CONV') /* SUP_CATASTALE */, " _
                                    & " GETDIMENSIONE((SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'),'SUP_NETTA') /* SUP_NETTA */, " _
                                    & " 1, " _
                                    & " '30000000', " _
                                    & " '19000101', " _
                                    & par.VirgoleInPunti(ascensore) & ", " _
                                    & " 3, " _
                                    & " (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'), " _
                                    & " SISCOM_MI.SEQ_CARATURE.NEXTVAL) "
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE ( " _
                                    & " SUP_CATASTALE, SUP_NETTA, MODIFICA_MANUALE,  " _
                                    & " FINE_VALIDITA, INIZIO_VALIDITA, VALORE_CARATURA,  " _
                                    & " ID_TIPOLOGIA_CARATURA, ID_UNITA, ID)  " _
                                    & " VALUES (GETDIMENSIONE((SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'),'SUP_CONV') /* SUP_CATASTALE */, " _
                                    & " GETDIMENSIONE((SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'),'SUP_NETTA') /* SUP_NETTA */, " _
                                    & " 1, " _
                                    & " '30000000', " _
                                    & " '19000101', " _
                                    & par.VirgoleInPunti(riscaldamento) & ", " _
                                    & " 2, " _
                                    & " (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'), " _
                                    & " SISCOM_MI.SEQ_CARATURE.NEXTVAL) "
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.CARATURE ( " _
                                   & " SUP_CATASTALE, SUP_NETTA, MODIFICA_MANUALE,  " _
                                   & " FINE_VALIDITA, INIZIO_VALIDITA, VALORE_CARATURA,  " _
                                   & " ID_TIPOLOGIA_CARATURA, ID_UNITA, ID)  " _
                                   & " VALUES (GETDIMENSIONE((SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'),'SUP_CONV') /* SUP_CATASTALE */, " _
                                   & " GETDIMENSIONE((SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'),'SUP_NETTA') /* SUP_NETTA */, " _
                                   & " 1, " _
                                   & " '30000000', " _
                                   & " '19000101', " _
                                   & par.VirgoleInPunti(montascale) & ", " _
                                   & " 5, " _
                                   & " (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "'), " _
                                   & " SISCOM_MI.SEQ_CARATURE.NEXTVAL) "
                                par.cmd.ExecuteNonQuery()

                            Else
                                par.cmd.CommandText = "UPDATE SISCOM_MI.CARATURE SET VALORE_CARATURA=" _
                                    & par.VirgoleInPunti(serviziComplesso) _
                                    & " WHERE ID_UNITA=(SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "') " _
                                    & " AND ID_TIPOLOGIA_CARATURA=1 "
                                ris = par.cmd.ExecuteNonQuery
                                par.cmd.CommandText = "UPDATE SISCOM_MI.CARATURE SET VALORE_CARATURA=" _
                                    & par.VirgoleInPunti(serviziEdifici) _
                                    & " WHERE ID_UNITA=(SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "') " _
                                    & " AND ID_TIPOLOGIA_CARATURA=4 "
                                ris = par.cmd.ExecuteNonQuery
                                par.cmd.CommandText = "UPDATE SISCOM_MI.CARATURE SET VALORE_CARATURA=" _
                                    & par.VirgoleInPunti(riscaldamento) _
                                    & " WHERE ID_UNITA=(SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "') " _
                                    & " AND ID_TIPOLOGIA_CARATURA=2 "
                                ris = par.cmd.ExecuteNonQuery
                                par.cmd.CommandText = "UPDATE SISCOM_MI.CARATURE SET VALORE_CARATURA=" _
                                    & par.VirgoleInPunti(ascensore) _
                                    & " WHERE ID_UNITA=(SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "') " _
                                    & " AND ID_TIPOLOGIA_CARATURA=3 "
                                ris = par.cmd.ExecuteNonQuery
                                par.cmd.CommandText = "UPDATE SISCOM_MI.CARATURE SET VALORE_CARATURA=" _
                                   & par.VirgoleInPunti(montascale) _
                                   & " WHERE ID_UNITA=(SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & codiceUnitaImmobiliare & "') " _
                                   & " AND ID_TIPOLOGIA_CARATURA=5 "
                                ris = par.cmd.ExecuteNonQuery
                            End If
                        Next
                        Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                                            & " VALUES (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & Tempo & "', '-'," _
                                            & "'-', '-' , 26)"
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi(True)
                    End Using
                End If
            End If
            RadWindowManager1.RadAlert("Tabellone unico CDR modificati!", 300, 150, "successo", "", "null")
        Catch ex As Exception
            connData.chiudi(False)
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnAllega_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
