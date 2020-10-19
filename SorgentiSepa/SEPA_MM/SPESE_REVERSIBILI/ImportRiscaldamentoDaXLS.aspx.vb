Imports System.IO

Partial Class SPESE_REVERSIBILI_ImportRiscaldamentoDaXLS
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If controlloProfilo() Then
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Consuntivi e conguagli - Gestione - Riscaldamento (da file Excel)"
            CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
            If Not IsPostBack Then
                settaCampi()
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
        btnAllega.Enabled = False
    End Sub
    Private Sub settaCampi()
        Try
            connData.apri(False)
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.CRITERI_RIPARTIZIONE ORDER BY DESCRIZIONE ASC", cmbCriterioRipartizione, "ID", "DESCRIZIONE", False)
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_DIVISIONE WHERE ID IN (1,3,6) ORDER BY DESCRIZIONE ASC", cmbTipologiaDivisione, "ID", "DESCRIZIONE", False)
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_CARATURE ORDER BY DESCRIZIONE ASC", cmbTipologiaCaratura, "ID", "DESCRIZIONE", False)
            cmbCriterioRipartizione.SelectedValue = 4
            cmbCriterioRipartizione.Enabled = False
            cmbTipologiaDivisione.SelectedValue = 1
            cmbTipologiaDivisione.Enabled = False
            cmbTipologiaCaratura.SelectedValue = 2
            cmbTipologiaCaratura.Enabled = False
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.PF_CATEGORIE WHERE ID_TIPO_SPESA = " & cmbTipologiaCaratura.SelectedValue & " ORDER BY DESCRIZIONE ASC", cmbCategoriaCarature, "ID", "DESCRIZIONE", False)
            cmbCategoriaCarature.SelectedValue = 39
            cmbCategoriaCarature.Enabled = False
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: settaCampi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Protected Sub cmbTipologiaCaratura_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbTipologiaCaratura.SelectedIndexChanged
        Try
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.PF_CATEGORIE WHERE ID_TIPO_SPESA = " & cmbTipologiaCaratura.SelectedValue & " ORDER BY DESCRIZIONE ASC", cmbCategoriaCarature, "ID", "DESCRIZIONE", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: settaCampi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub

    Protected Sub btnAllega_Click(sender As Object, e As System.EventArgs) Handles btnAllega.Click
        Try
            Dim xls As New ExcelSiSol
            Dim dtFoglio1 As New Data.DataTable
            Dim nomeFile As String = ""
            Dim contatore As Integer = 0
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
                        Dim continua As Boolean = False
                        Dim struttura As String = ""
                        Dim idStruttura As Integer = 0
                        Dim descrizione As String = ""
                        Dim codice As String = ""
                        Dim criterioRipartizione As String = cmbCriterioRipartizione.SelectedValue
                        Dim tipologiaDivisione As String = cmbTipologiaDivisione.SelectedValue
                        Dim tipologiaCaratura As String = cmbTipologiaCaratura.SelectedValue
                        Dim categoriaCaratura As String = cmbCategoriaCarature.SelectedValue
                        Dim importo As Decimal = 0
                        connData.apri(True)
                        Select Case dtFoglio1.Rows(1).Item(0).ToString.Length
                            Case 7
                                'COMPLESSO
                                If tipologiaDivisione = 6 Then
                                    continua = True
                                    struttura = "ID_COMPLESSO"
                                End If
                            Case 9
                                'EDIFICIO
                                If tipologiaDivisione = 3 Then
                                    continua = True
                                    struttura = "ID_EDIFICIO"
                                End If
                            Case 15
                                'IMPIANTO
                                If tipologiaDivisione = 1 Then
                                    continua = True
                                    struttura = "ID_IMPIANTO"
                                End If
                        End Select
                        If continua Then
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.PF_CONS_RIPARTIZIONI WHERE ID_PF=" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") _
                                & " AND DESCRIZIONE LIKE 'DA FILE%' AND ID_TIPO_SPESA=2 AND ((ID_CATEGORIA=" & cmbCategoriaCarature.SelectedValue & " AND ID_IMPIANTO IS NOT NULL) OR (ID_CATEGORIA=6 AND ID_COMPLESSO IS NOT NULL))"
                            par.cmd.ExecuteNonQuery()
                            For Each riga As Data.DataRow In dtFoglio1.Rows
                                codice = par.IfNull(riga.Item(0), "")
                                importo = CDec(par.IfNull(riga.Item(1), 0))
                                Select Case tipologiaDivisione
                                    Case 1
                                        'IMPIANTO
                                        par.cmd.CommandText = "SELECT ID, DESCRIZIONE AS NOME FROM SISCOM_MI.IMPIANTI WHERE COD_IMPIANTO = '" & par.IfNull(riga.Item(0), "-1") & "'"
                                    Case 3
                                        'EDIFICIO
                                        par.cmd.CommandText = "SELECT ID, DENOMINAZIONE AS NOME  FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO = '" & par.IfNull(riga.Item(0), "-1") & "'"
                                    Case 6
                                        'COMPLESSO
                                        par.cmd.CommandText = "SELECT ID, DENOMINAZIONE AS NOME FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COD_COMPLESSO = '" & par.IfNull(riga.Item(0), "-1") & "'"
                                End Select
                                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                While lettore.Read
                                    idStruttura = par.IfNull(lettore.Item("ID"), "")
                                    descrizione = par.IfNull(lettore.Item("NOME"), "")
                                End While
                                lettore.Close()
                                contatore += 1
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_CONS_RIPARTIZIONI ( " _
                                                    & "    ID_PF, ID_VOCE_SPESA, ID_CRITERIO_RIPARTIZIONE,  " _
                                                    & "    FL_MANUALE, ID_TIPOLOGIA_DIVISIONE, " & struttura & ", " _
                                                    & "    IMPORTO, DESCRIZIONE, ID,  " _
                                                    & "    ID_TIPO_SPESA, ID_CATEGORIA, " _
                                                    & "    FL_SELEZIONATO)  " _
                                                    & " VALUES ( " & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & " /* ID_PF */, " _
                                                    & "  '' /* ID_VOCE_SPESA */, " _
                                                    & criterioRipartizione & "  /* ID_CRITERIO_RIPARTIZIONE */, " _
                                                    & "1  /* FL_MANUALE */, " _
                                                    & tipologiaDivisione & "  /* ID_TIPOLOGIA_DIVISIONE */, " _
                                                    & idStruttura & "  /* IDSTRUTTURA */, " _
                                                    & par.VirgoleInPunti(Math.Round(importo, 2)) & "  /* IMPORTO */, " _
                                                    & "'DA FILE " & FileUpload1.FileName.Replace("'", "''") & "#" & riga.Item(0).ToString.ToUpper & "-" & descrizione.Replace("'", "''") & "'  /* DESCRIZIONE */, " _
                                                    & " SISCOM_MI.SEQ_PF_CONS_RIPARTIZIONI.NEXTVAL " & " /* ID */, " _
                                                    & tipologiaCaratura & "  /* ID_TIPO_SPESA */, " _
                                                    & categoriaCaratura & "  /* ID_CATEGORIA */, " _
                                                    & " 0 /* FL_SELEZIONATO */) "
                                par.cmd.ExecuteNonQuery()

                            Next
                            Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                                & " VALUES (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & Tempo & "', '-'," _
                                & "'-', '-' , 10)"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "SISCOM_MI.CONSUNTIVO_CORRETTIVO"
                            par.cmd.CommandType = Data.CommandType.StoredProcedure
                            par.cmd.Parameters.Add("ris", 0).Direction = Data.ParameterDirection.ReturnValue
                            par.cmd.Parameters.Add("idPf", Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE"))
                            par.cmd.ExecuteNonQuery()
                            RadWindowManager1.RadAlert("Operazione effettuata: importati " & contatore & " elementi.", 300, 150, "Attenzione", "function f(sender,args){location.href='ProspettoConsuntivi.aspx?';}", "null")
                        Else
                            RadWindowManager1.RadAlert("Incongruenze tra i dati!", 300, 150, "Attenzione", "", "null")
                        End If
                        connData.chiudi(True)
                    End Using
                End If
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnAllega_Click - " & ex.Message)

            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
End Class
