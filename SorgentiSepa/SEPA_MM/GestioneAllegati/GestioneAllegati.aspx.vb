Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO
Imports Telerik.Web.UI

Partial Class GestioneAllegati_GestioneAllegati
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Response.End()
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        txtAnnoArchiviazione.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("T")) Then
                HFTipoGestione.Value = Request.QueryString("T")
                If HFTipoGestione.Value.ToString = "0" Or HFTipoGestione.Value.ToString = "2" Then
                    If Not IsNothing(Request.QueryString("O")) Then
                        HFOggetto.Value = Request.QueryString("O")
                        If String.IsNullOrEmpty(Trim(HFOggetto.Value)) Then
                            RadWindowManager1.RadAlert("Allegati non disponibili! Contattare l\'Amministratore del Sistema.", 300, 150, "Attenzione", "", "null")
                            Exit Sub
                        End If
                    Else
                        RadWindowManager1.RadAlert("Allegati non disponibili! Contattare l\'Amministratore del Sistema.", 300, 150, "Attenzione", "", "null")
                        Exit Sub
                    End If
                    If Not IsNothing(Request.QueryString("I")) Then
                        HFIdOggetto.Value = Request.QueryString("I")
                        If String.IsNullOrEmpty(Trim(HFIdOggetto.Value)) Then
                            RadWindowManager1.RadAlert("Allegati non disponibili! Contattare l\'Amministratore del Sistema.", 300, 150, "Attenzione", "", "null")
                            Exit Sub
                        End If
                    Else
                        RadWindowManager1.RadAlert("Allegati non disponibili! Contattare l\'Amministratore del Sistema.", 300, 150, "Attenzione", "", "null")
                        Exit Sub
                    End If
                    lblTitoloFieldset1.Text = "Allega Nuovo File"
                    lblTitoloFieldset2.Text = "File Allegati"
                    'If HFTipoGestione.Value.ToString = "2" Then
                    '    btnAllegaProtocollo.Visible = True
                    'Else
                    '    btnAllegaProtocollo.Visible = False
                    'End If
                    CaricaTitolo()
                    CaricaTipologia()
                    CaricaAllegati()
                Else
                    'lblTitoloFieldset1.Text = "Parametri di Ricerca"
                    'lblTitoloFieldset2.Text = "Allegati del Protocollo"
                    'lblTitolo.Text = "Ricerca Allegati Protocollo"
                    'btnAllegaProtocollo.Visible = False
                    'If CaricaTipologiaWS() = False Then Exit Sub
                    'CaricaAllegatiWSVuoti(True)
                End If
            Else
                RadWindowManager1.RadAlert("Allegati non disponibili! Contattare l\'Amministratore del Sistema.", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If
            If Not IsNothing(Session.Item("ID_OPERATORE_PROTOCOLLO")) Then
                If Not String.IsNullOrEmpty(Trim(Session.Item("ID_OPERATORE_PROTOCOLLO").ToString)) Then
                    lblProtocollo.Text = Session.Item("OPERATORE_PROTOCOLLO").ToString
                    imgAlertProtocollo.Visible = False
                Else
                    lblProtocollo.Text = "- - -"
                    imgAlertProtocollo.Visible = True
                End If
            Else
                lblProtocollo.Text = "- - -"
                imgAlertProtocollo.Visible = True
            End If
        End If
        If HFIdOggetto.Value.ToString.ToUpper = "NULL" Then
            divAllega.Visible = False
            divAllegati.Style("height") = "650px"
            divGridAllegati.Style("height") = "575px"
        Else
            divAllega.Visible = True
            divAllegati.Style("height") = "350px"
            divGridAllegati.Style("height") = "275px"
        End If
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            If HFTipoGestione.Value.ToString = "0" Or HFTipoGestione.Value.ToString = "2" Then
                divGestioneInserimento.Visible = True
                divGestioneRicerca.Visible = False
                btnAggiorna.Visible = True
                If HFTipoGestione.Value.ToString = "0" Then
                    lblTitoloModulo.Text = "PROTOCOLLO"
                    imgAlertProtocollo.Visible = True
                    imgLogoProtocollo.Visible = True
                    lblProtocollo.Visible = True
                    cbProtocolla.Visible = True
                    'tbProtocollo.Visible = True
                Else
                    lblTitoloModulo.Text = "GESTIONE ALLEGATI"
                    imgAlertProtocollo.Visible = False
                    imgLogoProtocollo.Visible = False
                    lblProtocollo.Visible = False
                    cbProtocolla.Visible = False
                    'tbProtocollo.Visible = False
                End If
            Else
                divGestioneInserimento.Visible = False
                divGestioneRicerca.Visible = True
                btnAggiorna.Visible = False
            End If
            If Session.Item("FL_GEST_ALLEGATI") = "0" Then
                btnAddTipologia.Visible = False
                btnModTipologia.Visible = False
                btnDeleteTipologia.Visible = False
                btnDefaultTipologia.Visible = False
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipologia()
        Try
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.ALLEGATI_WS_TIPI WHERE ID_OGGETTO = " & HFOggetto.Value & " ORDER BY DESCRIZIONE ASC", ddlTipologiaAllegati, "ID", "DESCRIZIONE", True)
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.ALLEGATI_WS_TIPI WHERE ID_OGGETTO = " & HFOggetto.Value & " ORDER BY DESCRIZIONE ASC", ddlTipologiaAllegatoProtocollo, "ID", "DESCRIZIONE", True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - CaricaTipologia - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    'Private Function CaricaTipologiaWS() As Boolean
    '    CaricaTipologiaWS = False
    '    Try
    '        Dim Ws As New AllegatiService.WsAllegati
    '        Dim risultato As String = Ws.CaricaTipologie(ddlTipologiaWS)
    '        If Not String.IsNullOrEmpty(Trim(risultato)) Then
    '            If risultato = "Server allegati non raggiungibile" Then
    '                par.modalDialogMessage("Attenzione", risultato & "!", Me.Page, , , True)
    '                Exit Function
    '            ElseIf risultato = "Nessuna Tipologia" Then
    '                par.modalDialogMessage("Attenzione", risultato & " Trovata!", Me.Page, , , True)
    '                Exit Function
    '            Else
    '                par.modalDialogMessage("Attenzione", "Gestione Allegati non disponibile! Contattare l\'Amministratore del Sistema.", Me.Page, , , True)
    '                Exit Function
    '            End If
    '        Else
    '            CaricaTipologiaWS = True
    '        End If
    '    Catch ex As Exception
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            connData.chiudi(False)
    '        End If
    '        Session.Add("ERRORE", "Provenienza: GestioneAllegati - CaricaTipologiaWS - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Function
    Private Sub CaricaTitolo()
        Try
            connData.apri(False)
            Dim TabellaRiferimento As String = ""
            Dim CampoRiferimento As String = ""
            Dim IdentificativoRiferimento As String = ""
            Dim Titolo As String = ""
            par.cmd.CommandText = "SELECT TABELLA_RIFERIMENTO, CAMPO_RIFERIMENTO, IDENTIFICATIVO_RIFERIMENTO " _
                                & "FROM SISCOM_MI.ALLEGATI_WS_OGGETTI " _
                                & "WHERE ID = " & HFOggetto.Value
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                TabellaRiferimento = par.IfNull(MyReader("TABELLA_RIFERIMENTO"), "")
                CampoRiferimento = par.IfNull(MyReader("CAMPO_RIFERIMENTO"), "")
                IdentificativoRiferimento = par.IfNull(MyReader("IDENTIFICATIVO_RIFERIMENTO"), "")
            End If
            MyReader.Close()
            Dim Operazione As String = "="
            If HFIdOggetto.Value.ToString.ToUpper = "NULL" Then Operazione = "IS"
            par.cmd.CommandText = "SELECT " & CampoRiferimento & " FROM SISCOM_MI." & TabellaRiferimento & " " _
                                & "WHERE ID " & Operazione & " " & HFIdOggetto.Value
            MyReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                Titolo = par.IfNull(MyReader(0), "")
            End If
            MyReader.Close()
            If HFIdOggetto.Value.ToString.ToUpper = "NULL" Then
                lblTitolo.Text = "Allegati " & IdentificativoRiferimento
            Else
                lblTitolo.Text = "Allegati " & IdentificativoRiferimento & ": " & Titolo
            End If
            lblTipologia.Text = "Tipologia Allegato " & IdentificativoRiferimento & "*"
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - CaricaTitolo - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaAllegati()
        Try
            Dim Operazione As String = "="
            If HFIdOggetto.Value.ToString.ToUpper = "NULL" Then Operazione = "IS"
            par.cmd.CommandText = "SELECT ROW_NUMBER() OVER (ORDER BY DATA_ORA) AS NR, ALLEGATI_WS.ID, ALLEGATI_WS.ID_ALLEGATO, ALLEGATI_WS.NOME, ALLEGATI_WS_TIPI.DESCRIZIONE AS TIPOLOGIA, " _
                                & "ALLEGATI_WS.DESCRIZIONE, GETDATAORA(ALLEGATI_WS.DATA_ORA) AS DATA_ORA, STATO, FL_PROTOCOLLO, ALLEGATI_WS_TIPI.ID_OGGETTO AS ID_TIPOLOGIA_OGGETTO " _
                                & "FROM SISCOM_MI.ALLEGATI_WS, SISCOM_MI.ALLEGATI_WS_TIPI " _
                                & "WHERE ALLEGATI_WS_TIPI.ID(+) = ALLEGATI_WS.TIPO " _
                                & "AND OGGETTO = " & HFOggetto.Value & " AND ALLEGATI_WS.ID_OGGETTO " & Operazione & " " & HFIdOggetto.Value & " " _
                                & "ORDER BY ALLEGATI_WS.DATA_ORA ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            dgvAllegati.Columns(8).Visible = True
            dgvAllegati.DataSource = dt
            dgvAllegati.DataBind()
            If HFIdOggetto.Value.ToString.ToUpper = "NULL" Then
                dgvAllegati.Columns(8).Visible = False
            End If
            If HFTipoGestione.Value.ToString = "0" Then
                dgvAllegati.Columns(9).Visible = True
            Else
                dgvAllegati.Columns(9).Visible = False
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - CaricaAllegati - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub dgvAllegati_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvAllegati.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            'NOME ALLEGATO
            If Len(e.Item.Cells(3).Text.ToString) > 50 Then
                e.Item.Cells(3).ToolTip = e.Item.Cells(3).Text
                e.Item.Cells(3).Text = Left(e.Item.Cells(3).Text, 50) & "..."
            End If
            'DESCRIZIONE ALLEGATO
            If Len(e.Item.Cells(5).Text.ToString) > 50 Then
                e.Item.Cells(5).ToolTip = e.Item.Cells(5).Text
                e.Item.Cells(5).Text = Left(e.Item.Cells(5).Text, 50) & "..."
            End If
            'STATO ALLEGATO
            If e.Item.Cells(10).Text.ToString = "0" Then
                CType(e.Item.Cells(7).FindControl("btnDownload"), ImageButton).OnClientClick = "document.getElementById('idSelected').value = '" & e.Item.Cells(0).Text.ToString & "';document.getElementById('FlProtocollo').value = '" & e.Item.Cells(11).Text.ToString & "';document.getElementById('HFNomeFile').value = '" & e.Item.Cells(3).Text.ToString.Replace("'", "\'") & "';"
                CType(e.Item.Cells(8).FindControl("btnElimina"), ImageButton).OnClientClick = "document.getElementById('idSelected').value = '" & e.Item.Cells(0).Text.ToString & "';return EliminaAllegato();"
                If e.Item.Cells(11).Text.ToString = "1" Then
                    CType(e.Item.Cells(9).FindControl("btnProtocollo"), ImageButton).ToolTip = "Allegato Protocollato"
                    CType(e.Item.Cells(9).FindControl("btnProtocollo"), ImageButton).OnClientClick = "return false;"
                    CType(e.Item.Cells(9).FindControl("btnProtocollo"), ImageButton).ImageUrl = "Immagini/protocolloyes.png"
                Else
                    CType(e.Item.Cells(9).FindControl("btnProtocollo"), ImageButton).ToolTip = "Sposta Allegato nel Protocollato"
                    CType(e.Item.Cells(9).FindControl("btnProtocollo"), ImageButton).OnClientClick = "document.getElementById('idSelected').value = '" & e.Item.Cells(0).Text.ToString & "';ConfSpostaProtocollo();"
                    CType(e.Item.Cells(9).FindControl("btnProtocollo"), ImageButton).ImageUrl = "Immagini/protocollono.png"
                End If
            ElseIf e.Item.Cells(10).Text.ToString = "1" Then
                e.Item.Cells(2).Font.Strikeout = True
                e.Item.Cells(3).Font.Strikeout = True
                e.Item.Cells(4).Font.Strikeout = True
                e.Item.Cells(5).Font.Strikeout = True
                e.Item.Cells(6).Font.Strikeout = True
                CType(e.Item.Cells(7).FindControl("btnDownload"), ImageButton).Visible = False
                CType(e.Item.Cells(8).FindControl("btnElimina"), ImageButton).Visible = False
                CType(e.Item.Cells(9).FindControl("btnProtocollo"), ImageButton).Visible = False
            ElseIf e.Item.Cells(10).Text.ToString = "2" Then
                e.Item.Cells(2).Font.Strikeout = True
                e.Item.Cells(3).Font.Strikeout = True
            End If
            If e.Item.Cells(4).Text.ToString.ToUpper = "POLIZZA FIDEIUSSORIA" Then
                CType(e.Item.Cells(8).FindControl("btnElimina"), ImageButton).Visible = False
            End If
            If String.IsNullOrEmpty(Trim(e.Item.Cells(12).Text.ToString.Replace("&nbsp;", ""))) Then
                CType(e.Item.Cells(8).FindControl("btnElimina"), ImageButton).Visible = False
            End If
        End If
    End Sub
    Protected Sub btnDownload_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(Trim(idSelected.Value)) Then
                Dim DownloadAllegato As String = ""
                If FlProtocollo.Value.ToString = "0" Then
                    'CARICA FILE DA SEP@COM
                    Dim nomeAllegato As String = ""
                    connData.apri(False)
                    par.cmd.CommandText = "SELECT PATH, NOME FROM SISCOM_MI.ALLEGATI_WS WHERE ID = " & idSelected.Value
                    Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If MyReader.Read Then
                        DownloadAllegato = par.IfNull(MyReader("PATH"), "")
                        nomeAllegato = par.IfNull(MyReader("NOME"), "")
                    End If
                    MyReader.Close()
                    connData.chiudi(False)
                    If Not String.IsNullOrEmpty(Trim(DownloadAllegato)) Then
                        'If Not DownloadAllegato.ToString.ToLower.Contains(".zip") Or DownloadAllegato.ToString.ToLower.Contains(".rar") Then
                        '    DownloadAllegato = ZipAllegatoDownload(DownloadAllegato, nomeAllegato)
                        '    If String.IsNullOrEmpty(Trim(DownloadAllegato)) Then
                        '        Exit Sub
                        '    End If
                        'End If
                        If File.Exists(Server.MapPath("../FileTemp/" & DownloadAllegato)) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & DownloadAllegato.Replace("'", "\'") & "','_blank','top='+altezza+',left='+larghezza+',height=700,width=1300,resizable=1');}", True)
                        Else
                            RadWindowManager1.RadAlert("Allegato non Disponibile!", 300, 150, "Attenzione", "", "null")
                        End If
                    Else
                        RadWindowManager1.RadAlert("Allegato non Disponibile!", 300, 150, "Attenzione", "", "null")
                    End If
                Else
                    'If HFTipoGestione.Value.ToString = "1" Then
                    '    Dim Ws As New AllegatiService.WsAllegatiRicerca
                    '    DownloadAllegato = Ws.DownloadFileRicerca(idSelected.Value, HFNomeFile.Value)
                    'ElseIf HFTipoGestione.Value.ToString = "2" Then
                    '    connData.apri(False)
                    '    par.cmd.CommandText = "SELECT ID_ALLEGATO FROM SISCOM_MI.ALLEGATI_WS WHERE ID = " & idSelected.Value
                    '    Dim idAllegatoProtocollo As String = par.IfNull(par.cmd.ExecuteScalar, "")
                    '    connData.chiudi(False)
                    '    If Not String.IsNullOrEmpty(Trim(idAllegatoProtocollo)) Then
                    '        Dim Ws As New AllegatiService.WsAllegatiRicerca
                    '        DownloadAllegato = Ws.DownloadFileDiretto(idAllegatoProtocollo, HFNomeFile.Value & "_" & Format(Now, "yyyyMMddHHmmss") & ".pdf")
                    '    Else
                    '        par.modalDialogMessage("Attenzione", "File non disponibile al momento!", Me.Page)
                    '    End If
                    'Else
                    '    'CARICA FILE DA PROTOCOLLO
                    '    Dim NumeroTipologiaWS As Long = 0
                    '    connData.apri(False)
                    '    par.cmd.CommandText = "SELECT ID_CARTELLA_WS " _
                    '                        & "FROM SISCOM_MI.ALLEGATI_WS_CARTELLE " _
                    '                        & "WHERE ID = (SELECT ID_CARTELLA FROM ALLEGATI_WS_OGGETTI WHERE ID = " & HFOggetto.Value & ")"
                    '    Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    '    If MyReader.Read Then
                    '        NumeroTipologiaWS = par.IfNull(MyReader("ID_CARTELLA_WS"), 0)
                    '    End If
                    '    MyReader.Close()
                    '    connData.chiudi(False)
                    '    Dim Ws As New AllegatiService.WsAllegati
                    '    DownloadAllegato = Ws.DownloadFile(idSelected.Value, NumeroTipologiaWS)
                    'End If
                    'If Not String.IsNullOrEmpty(Trim(DownloadAllegato)) Then
                    '    If DownloadAllegato = "Server allegati non raggiungibile" Then
                    '        par.modalDialogMessage("Attenzione", DownloadAllegato & "!", Me.Page)
                    '    Else
                    '        If File.Exists(Server.MapPath("../FileTemp/" & DownloadAllegato)) Then
                    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & DownloadAllegato & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                    '        Else
                    '            par.modalDialogMessage("Attenzione", "Allegato non Disponibile!", Me.Page)
                    '        End If
                    '    End If
                    'Else
                    '    par.modalDialogMessage("Attenzione", "File non disponibile al momento!", Me.Page)
                    'End If
                End If
            Else
                RadWindowManager1.RadAlert("Allegato non Disponibile!", 300, 150, "Attenzione", "", "null")
            End If
            idSelected.Value = ""
            FlProtocollo.Value = ""
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnDownload_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function ZipAllegatoDownload(ByVal strFile As String, ByVal nomeFile As String) As String
        ZipAllegatoDownload = ""
        Try
            Dim zipFic As String = ""
            Dim estensioneAllegato As String = Mid(Server.MapPath(strFile), Server.MapPath(strFile).IndexOf(".") + 1)
            Dim AllegatoCompleto As String = nomeFile.Replace(estensioneAllegato, ".zip")
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim strmFile As FileStream = File.OpenRead(Server.MapPath("../FileTemp/" & strFile))
            Dim abyBuffer(strmFile.Length - 1) As Byte
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)
            Dim sFile As String = Path.GetFileName(Server.MapPath("../FileTemp/" & strFile))
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(Server.MapPath("../FileTemp/" & strFile))
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            If File.Exists(Server.MapPath("../FileTemp/") & AllegatoCompleto) Then
                File.Delete(Server.MapPath("../FileTemp/") & AllegatoCompleto)
            End If
            zipFic = Server.MapPath("../FileTemp/") & AllegatoCompleto
            strmZipOutputStream = New ZipOutputStream(File.Create(zipFic))
            strmZipOutputStream.SetLevel(6)
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            ZipAllegatoDownload = AllegatoCompleto
        Catch ex As Exception
            ZipAllegatoDownload = ""
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnDownload_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Protected Sub btnElimina_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(Trim(idSelected.Value.ToString)) Then
                connData.apri(False)
                par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS SET STATO = 1, " _
                                    & "ID_OPERATORE_CANCELLAZIONE = " & Session.Item("ID_OPERATORE") & ", " _
                                    & "DATA_ORA_CANCELLAZIONE = '" & Format(Now, "yyyyMMddHHmmss") & "' " _
                                    & "WHERE ID = " & idSelected.Value
                par.cmd.ExecuteNonQuery()
                connData.chiudi(False)
                RadWindowManager1.RadAlert("Operazione effettuata correttamente!", 300, 150, "Operazione", "", "null")
                CaricaAllegati()
            Else
                RadWindowManager1.RadAlert("Allegato non disponibile!", 300, 150, "Attenzione", "", "null")
            End If
            idSelected.Value = ""
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnElimina_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function nomeFileisValid(ByVal nome As String) As Boolean
        nomeFileisValid = True

        If nome.Contains(",") Then
            nomeFileisValid = False
            Exit Function
        End If
        If nome.Contains("/") Then
            nomeFileisValid = False
            Exit Function

        End If
        If nome.Contains("?") Then
            nomeFileisValid = False
            Exit Function

        End If
        If nome.Contains(":") Then
            nomeFileisValid = False
            Exit Function

        End If
        If nome.Contains("@") Then
            nomeFileisValid = False
            Exit Function

        End If
        If nome.Contains("&") Then
            nomeFileisValid = False
            Exit Function

        End If
        If nome.Contains("=") Then
            nomeFileisValid = False
            Exit Function

        End If
        If nome.Contains("+") Then
            nomeFileisValid = False
            Exit Function

        End If
        If nome.Contains("$") Then
            nomeFileisValid = False
            Exit Function
        End If
        If nome.Contains("#") Then
            nomeFileisValid = False
            Exit Function
        End If


    End Function
    Protected Sub btnAllega_Click(sender As Object, e As System.EventArgs) Handles btnAllega.Click
        Try
            If HFConferma.Value.ToString = "0" Then
                Exit Sub
            End If
            If ddlTipologiaAllegati.SelectedValue.ToString = "-1" Then
                RadWindowManager1.RadAlert("Selezionare la tipologia del file da allegare!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If
            If nomeFileisValid(FileUpload.FileName) = False Then
                RadWindowManager1.RadAlert("Il nome file non può contenere i caratteri , / ? : @ & = + $ # ", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            Dim ElencoFile() As String = Nothing
            Dim i As Integer = 0
            If FileUpload.HasFile Then
                Dim NumeroTipologiaWS As Long = 0
                connData.apri(False)
                par.cmd.CommandText = "SELECT ID_CARTELLA_WS " _
                                    & "FROM SISCOM_MI.ALLEGATI_WS_CARTELLE " _
                                    & "WHERE ID = (SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & HFOggetto.Value & ")"
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    NumeroTipologiaWS = par.IfNull(MyReader("ID_CARTELLA_WS"), 0)
                End If
                MyReader.Close()
                connData.chiudi(False)
                Dim File As String = ""
                Dim NomeFile As String = ""
                Dim DescrizioneOggetto As String = ""
                Dim CartellaDestinazione As String = ""
                Dim IdOggettoPadre As String = ""
                Dim DescrizioneAllegato As String = Left(txtDescrizioneAllegato.Text, 500)
                Dim TipoAllegato As String = par.RitornaNullSeMenoUno(ddlTipologiaAllegati.SelectedValue, False)
                Dim Percorso As String = "..\FileTemp\"
                Dim fileExtension As String = System.IO.Path.GetExtension(FileUpload.FileName).ToLower()
                NomeFile = "Allegato_" & HFOggetto.Value & "_" & HFIdOggetto.Value & "_" & Format(Now, "yyyyMMddHHmmss") & "_" & FileUpload.FileName
                File = Server.MapPath("~\FileTemp\" & NomeFile)
                FileUpload.SaveAs(File)
                Dim NewAllegato As String = ""
                If cbProtocolla.Checked Then
                    'ReDim Preserve ElencoFile(i)
                    'ElencoFile(i) = Server.MapPath("~\FileTemp\" & NomeFile)
                    'i = i + 1
                    ''CARICA ALLEGATO SU PROTOCOLLO
                    'Dim Ws As New AllegatiService.WsAllegati
                    'NewAllegato = Ws.AllegaFile(ElencoFile, NumeroTipologiaWS, txtDescrizioneAllegato.Text, ddlTipologiaAllegati.SelectedValue, HFOggetto.Value, HFIdOggetto.Value)
                Else
                    'CARICA ALLEGATO SU SEP@COM
                    connData.apri(False)
                    Dim PathCartellaOggetto As String = ""
                    par.cmd.CommandText = "SELECT DESCRIZIONE, ID_CARTELLA, ID_OGGETTO_PADRE FROM SISCOM_MI.ALLEGATI_WS_OGGETTI " _
                                        & "WHERE ID = " & HFOggetto.Value
                    MyReader = par.cmd.ExecuteReader
                    If MyReader.Read Then
                        DescrizioneOggetto = par.IfNull(MyReader("DESCRIZIONE"), "")
                        CartellaDestinazione = par.IfNull(MyReader("ID_CARTELLA"), "")
                        IdOggettoPadre = par.IfNull(MyReader("ID_OGGETTO_PADRE"), "")
                    End If
                    MyReader.Close()
                    If Not String.IsNullOrEmpty(Trim(CartellaDestinazione)) Then
                        par.cmd.CommandText = "SELECT PATH FROM SISCOM_MI.ALLEGATI_WS_CARTELLE " _
                                            & "WHERE ID = " & CartellaDestinazione
                        MyReader = par.cmd.ExecuteReader
                        If MyReader.Read Then
                            PathCartellaOggetto = par.IfNull(MyReader("PATH"), "")
                        End If
                        MyReader.Close()
                    End If
                    connData.chiudi(False)
                    If String.IsNullOrEmpty(Trim(CartellaDestinazione)) Then
                        Dim CartellaPrincipale As String = ""
                        connData.apri(False)
                        par.cmd.CommandText = "SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & IdOggettoPadre
                        MyReader = par.cmd.ExecuteReader
                        If MyReader.Read Then
                            CartellaPrincipale = par.IfNull(MyReader("ID_CARTELLA"), "")
                        End If
                        MyReader.Close()
                        connData.chiudi(False)
                        CartellaDestinazione = CreaCartella(DescrizioneOggetto, CartellaPrincipale)
                        connData.apri(False)
                        par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS_OGGETTI SET ID_CARTELLA = " & CartellaDestinazione & " WHERE ID = " & HFOggetto.Value
                        par.cmd.ExecuteNonQuery()
                        connData.chiudi(False)
                    ElseIf Not Directory.Exists(Server.MapPath(PathCartellaOggetto)) Then
                        Directory.CreateDirectory(Server.MapPath(PathCartellaOggetto))
                    End If
                    NewAllegato = AllegaDocumento(File, NomeFile, CartellaDestinazione, DescrizioneAllegato, TipoAllegato, HFOggetto.Value, HFIdOggetto.Value, PathCartellaOggetto)
                End If
                If Not String.IsNullOrEmpty(Trim(NewAllegato)) Then
                    If NewAllegato = "Server allegati non raggiungibile" Then
                        RadWindowManager1.RadAlert(NewAllegato, 300, 150, "Attenzione", "", "null")
                    Else
                        RadWindowManager1.RadAlert("Operazione effettuata correttamente!", 300, 150, "Operazione", "", "null")
                        CaricaAllegati()
						'INSERIMENTO EVENTO IN APPALTI
                        If TipoAllegato = 14 Then
                            connData.apri(False)
                            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI (ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & HFIdOggetto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F283','Modifica appalti - INSERIMENTO POLIZZA DI FIDEIUSSIONE')"
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi(False)
                        End If
                    End If
                Else
                    RadWindowManager1.RadAlert("File non Allegato!", 300, 150, "Attenzione", "", "null")
                End If
                ddlTipologiaAllegati.SelectedValue = "-1"
                txtDescrizioneAllegato.Text = ""
                cbProtocolla.Checked = False
            Else
                RadWindowManager1.RadAlert("File non Allegato!", 300, 150, "Attenzione", "", "null")

            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnAllega_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnAggiorna_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAggiorna.Click
        CaricaAllegati()
    End Sub
    Protected Sub btnAddTipologia_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAddTipologia.Click
        CaricaTipologia()
    End Sub
    Protected Sub btnModTipologia_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnModTipologia.Click
        CaricaTipologia()
    End Sub
    Protected Sub btnDeleteTipologia_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDeleteTipologia.Click
        Try
            connData.apri(False)
            If ddlTipologiaAllegati.SelectedValue.ToString <> "-1" Then
                par.cmd.CommandText = "SELECT FL_NON_CANC FROM SISCOM_MI.ALLEGATI_WS_TIPI WHERE id = " & ddlTipologiaAllegati.SelectedValue
                Dim nonCancellabile As String = par.cmd.ExecuteScalar
                If nonCancellabile = "0" Then

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.ALLEGATI_WS_TIPI WHERE ID = " & par.RitornaNullSeMenoUno(ddlTipologiaAllegati.SelectedValue, False)
                par.cmd.ExecuteNonQuery()
                CaricaTipologia()
                Else
                    RadWindowManager1.RadAlert("Tipologia di allegato non cancellabile!", 300, 150, "Attenzione", "", "null")
                End If
            End If
            connData.chiudi(False)
            idSelected.Value = ""
        Catch ex1 As Oracle.DataAccess.Client.OracleException
            If ex1.Number = 2292 Then
                connData.chiudi(False)
                RadWindowManager1.RadAlert("Dato utilizzato!", 300, 150, "Attenzione", "", "null")
            Else
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
                Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnDeleteTipologia_Click - " & ex1.Message)
                Response.Redirect("../Errore.aspx", False)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnDeleteTipologia_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function CreaCartella(ByVal nomeCartella As String, ByVal contenitorePadre As String) As String
        CreaCartella = ""
        Try
            connData.apri(False)
            Dim PathPadre As String = ""
            Dim PathNewCartella As String = ""
            par.cmd.CommandText = "SELECT PATH FROM SISCOM_MI.ALLEGATI_WS_CARTELLE WHERE ID = " & contenitorePadre
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                PathPadre = par.IfNull(MyReader("PATH"), "")
            End If
            MyReader.Close()
            PathNewCartella = PathPadre & nomeCartella.ToString.ToUpper.Replace(" ", "_")
            If Not Directory.Exists(Server.MapPath(PathNewCartella)) Then
                Directory.CreateDirectory(Server.MapPath(PathNewCartella))
            End If
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_ALLEGATI_WS_CARTELLE.NEXTVAL FROM DUAL"
            CreaCartella = par.cmd.ExecuteScalar
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_WS_CARTELLE (ID, ID_CARTELLA_WS, ID_CARTELLA_PADRE_WS, NOME, PATH, ID_OPERATORE, DATA_ORA, ID_CARTELLA_PADRE) VALUES " _
                                & "(" & CreaCartella & ", null, null, " & par.insDbValue(nomeCartella, True) & ", " & par.insDbValue(PathNewCartella, True) & ", " _
                                & par.insDbValue(Session.Item("ID_OPERATORE"), True) & ", " _
                                & par.insDbValue(Format(Now, "yyyyMMddHHmmss"), True) & ", " & par.insDbValue(contenitorePadre, True) & ")"
            par.cmd.ExecuteNonQuery()
            connData.chiudi(False)
        Catch ex As Exception
            CreaCartella = ""
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
        End Try
    End Function
    Private Function AllegaDocumento(ByVal FileDocument As String, ByVal Titolo As String, ByVal cartella As String, ByVal DescrizioneAllegato As String, _
                                     ByVal TipoAllegato As String, ByVal Oggetto As String, ByVal IdOggetto As String, ByVal PathCartella As String) As String
        AllegaDocumento = ""
        Try
            connData.apri(False)
            If String.IsNullOrEmpty(Trim(PathCartella)) Then
                par.cmd.CommandText = "SELECT PATH FROM SISCOM_MI.ALLEGATI_WS_CARTELLE " _
                                    & "WHERE ID = " & cartella
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    PathCartella = par.IfNull(MyReader("PATH"), "")
                End If
                MyReader.Close()
            End If
            If System.IO.Path.GetExtension(Titolo) <> ".zip" And System.IO.Path.GetExtension(Titolo) <> ".rar" Then
                Titolo = ZipAllegatoDownload(Titolo, Titolo)
            End If
            Dim percorso As String = Server.MapPath("~\FileTemp\")
            File.Move(percorso & Titolo, Server.MapPath(PathCartella & "\" & Titolo))
            If IdOggetto.ToString.ToUpper = "NULL" Then IdOggetto = ""
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_ALLEGATI_WS.NEXTVAL FROM DUAL"
            AllegaDocumento = par.cmd.ExecuteScalar
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_WS (ID, ID_ALLEGATO, NOME, CARTELLA, PATH, " _
                                & "DESCRIZIONE, TIPO, OGGETTO, ID_OGGETTO, STATO, ID_OPERATORE, DATA_ORA, FL_PROTOCOLLO) VALUES " _
                                & "(" & AllegaDocumento & ", null, " & par.insDbValue(Titolo, True) & ", " & cartella & ", " _
                                & par.insDbValue((PathCartella & "/" & Titolo).Replace("//", "/"), True) & ", " & par.insDbValue(DescrizioneAllegato, True) & ", " _
                                & par.insDbValue(TipoAllegato, True) & ", " & par.insDbValue(Oggetto, True) & ", " & par.insDbValue(IdOggetto, True) & ", 0, " _
                                & par.insDbValue(Session.Item("ID_OPERATORE"), True) & ", " & par.insDbValue(Format(Now, "yyyyMMddHHmmss"), True) & ", 0)"
            par.cmd.ExecuteNonQuery()
            connData.chiudi(False)
        Catch ex As Exception
            AllegaDocumento = ""
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
        End Try
    End Function
    Protected Sub btnDefaultTipologia_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDefaultTipologia.Click
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.ALLEGATI_WS_TIPI WHERE ID_OGGETTO = " & HFOggetto.Value & " AND UPPER(DESCRIZIONE) = 'NON DEFINITO'"
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If Not MyReader.HasRows Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_WS_TIPI (ID, DESCRIZIONE, ID_OGGETTO) VALUES " _
                                    & "(SISCOM_MI.SEQ_ALLEGATI_WS_TIPI.NEXTVAL, 'NON DEFINITO', " & HFOggetto.Value & ")"
                par.cmd.ExecuteNonQuery()
            End If
            MyReader.Close()
            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.ALLEGATI_WS_TIPI WHERE ID_OGGETTO = " & HFOggetto.Value & " AND UPPER(DESCRIZIONE) = 'DOCUMENTO'"
            MyReader = par.cmd.ExecuteReader
            If Not MyReader.HasRows Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_WS_TIPI (ID, DESCRIZIONE, ID_OGGETTO) VALUES " _
                                    & "(SISCOM_MI.SEQ_ALLEGATI_WS_TIPI.NEXTVAL, 'DOCUMENTO', " & HFOggetto.Value & ")"
                par.cmd.ExecuteNonQuery()
            End If
            MyReader.Close()
            connData.chiudi(False)
            RadWindowManager1.RadAlert("Operazione effettuata correttamente!", 300, 150, "Operazione", "", "null")
            CaricaTipologia()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnDefaultTipologia_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    'Protected Sub btnProtocollo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
    '    Try
    '        If HFConferma.Value.ToString = "1" Then
    '            Dim NewAllegatoProtocollo As String = MoveAllegatoToProtocollo()
    '            If Not String.IsNullOrEmpty(Trim(NewAllegatoProtocollo)) Then
    '                Select Case NewAllegatoProtocollo
    '                    Case "File non Disponibile!"
    '                        par.modalDialogMessage("Attenzione", NewAllegatoProtocollo, Me.Page)
    '                    Case "Server allegati non raggiungibile"
    '                        par.modalDialogMessage("Attenzione", NewAllegatoProtocollo & "!", Me.Page)
    '                    Case Else
    '                        par.modalDialogMessage("Operazione", "Allegato spostato nel Protocollo!", Me.Page)
    '                        CaricaAllegati()
    '                End Select
    '            Else
    '                par.modalDialogMessage("Attenzione", "File non Disponibile!", Me.Page)
    '            End If
    '        End If
    '        idSelected.Value = ""
    '    Catch ex As Exception
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            connData.chiudi(False)
    '        End If
    '        Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnProtocollo_Click - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    'Private Function MoveAllegatoToProtocollo() As String
    '    MoveAllegatoToProtocollo = ""
    '    Try
    '        Dim ElencoFile() As String = Nothing
    '        Dim i As Integer = 0
    '        Dim PathFile As String = ""
    '        connData.apri(False)
    '        par.cmd.CommandText = "SELECT PATH FROM SISCOM_MI.ALLEGATI_WS WHERE ID = " & idSelected.Value
    '        Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '        If MyReader.Read Then
    '            PathFile = par.IfNull(MyReader("PATH"), "")
    '        End If
    '        MyReader.Close()
    '        Dim NumeroTipologiaWS As Long = 0
    '        connData.apri(False)
    '        par.cmd.CommandText = "SELECT ID_CARTELLA_WS " _
    '                            & "FROM SISCOM_MI.ALLEGATI_WS_CARTELLE " _
    '                            & "WHERE ID = (SELECT ID_CARTELLA FROM ALLEGATI_WS_OGGETTI WHERE ID = " & HFOggetto.Value & ")"
    '        MyReader = par.cmd.ExecuteReader
    '        If MyReader.Read Then
    '            NumeroTipologiaWS = par.IfNull(MyReader("ID_CARTELLA_WS"), 0)
    '        End If
    '        MyReader.Close()
    '        connData.chiudi(False)
    '        If Not String.IsNullOrEmpty(Trim(PathFile)) Then
    '            If File.Exists(Server.MapPath(PathFile)) Then
    '                ReDim Preserve ElencoFile(i)
    '                ElencoFile(i) = Server.MapPath(PathFile) & "," & idSelected.Value
    '                i = i + 1
    '                Dim Ws As New AllegatiService.WsAllegati
    '                MoveAllegatoToProtocollo = Ws.AllegatoToProtocollo(ElencoFile, NumeroTipologiaWS)
    '            Else
    '                MoveAllegatoToProtocollo = "File non Disponibile!"
    '            End If
    '        Else
    '            MoveAllegatoToProtocollo = "File non Disponibile!"
    '        End If
    '    Catch ex As Exception
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            connData.chiudi(False)
    '        End If
    '        Session.Add("ERRORE", "Provenienza: GestioneAllegati - MoveAllegatoToProtocollo - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Function
    Private Sub CaricaAllegatiWSVuoti(ByVal eseguiQuery As Boolean, Optional ByVal dtProtocollo As Data.DataTable = Nothing)
        Try
            If eseguiQuery Then
                par.cmd.CommandText = "SELECT '' AS NR, ''  AS ID, '' AS ID_ALLEGATO, '' AS NOME, '' AS TIPOLOGIA, '' AS DESCRIZIONE, " _
                                    & "'' AS DATA_ORA, '' AS STATO, '' AS FL_PROTOCOLLO " _
                                    & "FROM DUAL"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                dt.Rows.Clear()
                dgvAllegati.DataSource = dt
            Else
                dgvAllegati.DataSource = dtProtocollo
            End If
            dgvAllegati.DataBind()
            If HFTipoGestione.Value.ToString = "1" Then
                dgvAllegati.Columns(8).Visible = False
                dgvAllegati.Columns(9).Visible = False
            Else
                dgvAllegati.Columns(8).Visible = True
                If HFTipoGestione.Value.ToString = "0" Then
                    dgvAllegati.Columns(9).Visible = True
                Else
                    dgvAllegati.Columns(9).Visible = False
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - CaricaAllegatiWSVuoti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    'Protected Sub btnRicercaProtocollo_Click(sender As Object, e As System.EventArgs) Handles btnRicercaProtocollo.Click
    '    Try
    '        Dim dtProtocollo As New Data.DataTable
    '        Dim Ws As New AllegatiService.WsAllegatiRicerca
    '        Dim risultato As String = Ws.RicercaProtocollo(ddlTipologiaWS.SelectedValue.ToString, txtProtocollo.Text, txtAnnoArchiviazione.Text, dtProtocollo)
    '        If Not String.IsNullOrEmpty(Trim(risultato)) Then
    '            If risultato = "Server allegati non raggiungibile" Then
    '                par.modalDialogMessage("Attenzione", risultato & "!", Me.Page)
    '            ElseIf risultato = "NO RECORD" Then
    '                par.modalDialogMessage("Attenzione", "Nessun allegato trovato nel protocollo con i filtri inseriti!", Me.Page)
    '                CaricaAllegatiWSVuoti(True)
    '            Else
    '                Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnRicercaProtocollo_Click - " & risultato)
    '                Response.Redirect("../Errore.aspx", False)
    '            End If
    '        Else
    '            CaricaAllegatiWSVuoti(False, dtProtocollo)
    '        End If
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnRicercaProtocollo_Click - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub
    'Protected Sub btnAggiungiProtocollo_Click(sender As Object, e As System.EventArgs) Handles btnAggiungiProtocollo.Click
    '    Try
    '        If String.IsNullOrEmpty(Trim(txtNumProtocollo.Text)) Then
    '            par.modalDialogMessage("Attenzione", "Inserire un numero di protocollo!", Me.Page)
    '            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "openProtocollo(1);", True)
    '            Exit Sub
    '        End If
    '        If ddlTipologiaAllegatoProtocollo.SelectedValue.ToString = "-1" Then
    '            par.modalDialogMessage("Attenzione", "Inserire la tipologia!", Me.Page)
    '            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "openProtocollo(1);", True)
    '            Exit Sub
    '        End If
    '        Dim dtProtocollo As New Data.DataTable
    '        Dim Ws As New AllegatiService.WsAllegatiRicerca
    '        Dim risultato As String = Ws.RicercaProtocollo("", txtNumProtocollo.Text, "", dtProtocollo)
    '        If Not String.IsNullOrEmpty(Trim(risultato)) Then
    '            If risultato = "Server allegati non raggiungibile" Then
    '                par.modalDialogMessage("Attenzione", risultato & "!", Me.Page)
    '            ElseIf risultato = "NO RECORD" Then
    '                par.modalDialogMessage("Attenzione", "Nessun allegato trovato nel protocollo con i filtri inseriti!", Me.Page)
    '            Else
    '                Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnAggiungiProtocollo_Click - " & risultato)
    '                Response.Redirect("../Errore.aspx", False)
    '            End If
    '        Else
    '            connData.apri(False)
    '            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_ALLEGATI_WS.NEXTVAL FROM DUAL"
    '            Dim idAllegato As String = par.cmd.ExecuteScalar
    '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_WS (ID, ID_ALLEGATO, NOME, CARTELLA, PATH, " _
    '                                & "DESCRIZIONE, TIPO, OGGETTO, ID_OGGETTO, STATO, ID_OPERATORE, DATA_ORA, FL_PROTOCOLLO) VALUES " _
    '                                & "(" & idAllegato & ", " & par.insDbValue(dtProtocollo.Rows(0).Item("ID").ToString, True) & ", " & par.insDbValue(txtNumProtocollo.Text, True) & ", '', " _
    '                                & par.insDbValue(dtProtocollo.Rows(0).Item("NOME").ToString, True) & ", " & par.insDbValue(txtDescrizioneAllegatoProtocollo.Text, True) & ", " _
    '                                & par.RitornaNullSeMenoUno(ddlTipologiaAllegatoProtocollo.SelectedValue, False) & ", " & par.insDbValue(HFOggetto.Value, True) & ", " & HFIdOggetto.Value & ", 0, " _
    '                                & par.insDbValue(Session.Item("ID_OPERATORE"), True) & ", " & par.insDbValue(Format(Now, "yyyyMMddHHmmss"), True) & ", 1)"
    '            par.cmd.ExecuteNonQuery()
    '            connData.chiudi(False)
    '            txtNumProtocollo.Text = ""
    '            ddlTipologiaAllegatoProtocollo.SelectedValue = "-1"
    '            txtDescrizioneAllegatoProtocollo.Text = ""
    '            par.modalDialogMessage("Operazione", "Operazione effettuata correttamente!", Me.Page)
    '            CaricaAllegati()
    '        End If
    '    Catch ex As Exception
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            connData.chiudi(False)
    '        End If
    '        Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnAggiungiProtocollo_Click - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try
    'End Sub

    Protected Sub btnScaricaTutto_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnScaricaTutto.Click
        Dim ElencoFile() As String
        Try
            connData.apri(False)
            Dim Operazione As String = "="
            If HFIdOggetto.Value.ToString.ToUpper = "NULL" Then Operazione = "IS"
            par.cmd.CommandText = "SELECT " _
                                & "ALLEGATI_WS.PATH " _
                                & "FROM SISCOM_MI.ALLEGATI_WS, SISCOM_MI.ALLEGATI_WS_TIPI " _
                                & "WHERE ALLEGATI_WS_TIPI.ID(+) = ALLEGATI_WS.TIPO " _
                                & "AND OGGETTO = " & HFOggetto.Value & " AND ALLEGATI_WS.ID_OGGETTO " & Operazione & " " & HFIdOggetto.Value & " " _
                                & "AND STATO=0 " _
                                & "ORDER BY ALLEGATI_WS.DATA_ORA ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.apri(False)
            Dim i As Integer = 0
            i = dt.Rows.Count
            If i > 0 Then
                Dim zipfic As String
                ReDim Preserve ElencoFile(i)
                Dim indice As Integer = 0
                For Each riga As Data.DataRow In dt.Rows
                    ElencoFile(indice) = riga.Item("PATH")
                    indice += 1
                Next
                Dim NomeFilezip As String = "ALLEGATI" & Format(Now, "yyyyMMddHHmmss") & ".zip"
                zipfic = Server.MapPath("..\FileTemp\" & NomeFilezip)
                Dim kkK As Integer = 0
                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                Dim strFile As String
                For kkK = 0 To i - 1
                    strFile = ElencoFile(kkK)
                    Dim strmFile As FileStream = File.OpenRead(Server.MapPath(strFile))
                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    strmFile.Read(abyBuffer, 0, abyBuffer.Length)

                    Dim sFile As String = Path.GetFileName(strFile)
                    Dim theEntry As ZipEntry = New ZipEntry(sFile)
                    Dim fi As New FileInfo(strFile)
                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                Next
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                If File.Exists(Server.MapPath("~\FileTemp\") & NomeFilezip) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & NomeFilezip & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                    Exit Sub
                Else
                    par.modalDialogMessage("Attenzione", "Si è verificato un errore durante il download. Riprovare!", Me.Page)
                End If
            Else
                par.modalDialogMessage("Attenzione", "Nessun file presente da scaricare!", Me.Page)
            End If
            
        Catch ex As Exception
            connData.apri(False)
            Session.Add("ERRORE", "Provenienza: GestioneAllegati - btnScaricaTutto_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
