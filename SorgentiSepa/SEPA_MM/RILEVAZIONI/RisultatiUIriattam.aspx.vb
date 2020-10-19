Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports OfficeOpenXml

Partial Class RILEVAZIONI_RisultatiUIriattam
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_RILIEVO_GEST") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                BindGridUnita()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - GestUnita - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub BindGridUnita()
        Try
            connData.apri()

            Dim rilevati As String = ""
            If Not IsNothing(Request.QueryString("Rilevati")) Then
                rilevati = Request.QueryString("Rilevati")
            End If
            Dim condizioneRilevati As String = ""
            If rilevati <> "" Then
                Select Case rilevati
                    Case "1"
                        condizioneRilevati = " AND UNITA_IMMOBILIARI.ID in (select id_unita from siscom_mi.RILIEVO_UI where id_lotto is not null and id_unita in (select id_unita_immobiliare from siscom_mi.RILIEVO_ALLOGGIO_SFITTO where NON_VISITAB_LASTRATURA=0 and NON_VISITAB_OCCUPATO=0 and NON_VISITAB_MURATO=0 and NON_VISITAB_NO_CHIAVI=0)) "
                End Select
            End If

            'FILIALI
            Dim listaFiliali As System.Collections.Generic.List(Of String) = Session.Item("listaFiliali")
            'Session.Remove("listaFiliali")
            Dim listaFilialiSi As Boolean = False
            Dim condizionelistaFiliali As String = ""
            Dim fromCondizioneFiliale = ""
            If Not IsNothing(listaFiliali) Then
                For Each Items As String In listaFiliali
                    condizionelistaFiliali &= Items & ","
                Next
            End If
            If condizionelistaFiliali <> "" Then
                condizionelistaFiliali = Left(condizionelistaFiliali, Len(condizionelistaFiliali) - 1)
                condizionelistaFiliali = " and tab_filiali.ID in (" & condizionelistaFiliali & ") "
                listaFilialiSi = True
            End If

            'STUDIO PROF.
            Dim listaStProf As System.Collections.Generic.List(Of String) = Session.Item("listaStProf")
            'Session.Remove("listaStProf")
            Dim listalistaStProfSi As Boolean = False
            Dim condizionelistalistaStProf As String = ""
            Dim formCondizStProf As String = ""
            If Not IsNothing(listaStProf) Then
                For Each Items As String In listaStProf
                    condizionelistalistaStProf &= Items & ","
                Next
            End If
            If condizionelistalistaStProf <> "" Then
                condizionelistalistaStProf = Left(condizionelistalistaStProf, Len(condizionelistalistaStProf) - 1)
                condizionelistalistaStProf = " and RILIEVO_TAB_UTENTI.id in (" & condizionelistalistaStProf & ")  and RILIEVO_TAB_UTENTI.id=RILIEVO_lotti.id_utente "
                formCondizStProf = ",SISCOM_MI.RILIEVO_TAB_UTENTI "
                listalistaStProfSi = True
            End If

            Dim listaComplessi As System.Collections.Generic.List(Of String) = Session.Item("listaComplessi")
            'Session.Remove("listaComplessi")
            Dim listaComplessiSi As Boolean = False
            Dim condizionelistaComplessi As String = ""
            If Not IsNothing(listaComplessi) Then
                For Each Items As String In listaComplessi
                    condizionelistaComplessi &= Items & ","
                Next
            End If
            If condizionelistaComplessi <> "" Then
                condizionelistaComplessi = Left(condizionelistaComplessi, Len(condizionelistaComplessi) - 1)
                If condizionelistalistaStProf <> "" Then
                    condizionelistaComplessi = " AND COMPLESSI_IMMOBILIARI.ID IN (" & condizionelistaComplessi & ") "
                Else
                    'formCondizStProf = "siscom_mi.complessi_immobiliari,siscom_mi.edifici,"
                    condizionelistaComplessi = " AND COMPLESSI_IMMOBILIARI.ID IN (" & condizionelistaComplessi & ") "
                End If

                listaComplessiSi = True
            End If

            Dim listaEdifici As System.Collections.Generic.List(Of String) = Session.Item("listaEdifici")
            'Session.Remove("listaEdifici")
            Dim listaEdificiSi As Boolean = False
            Dim condizionelistaEdifici As String = ""
            If Not IsNothing(listaEdifici) Then
                For Each Items As String In listaEdifici
                    condizionelistaEdifici &= Items & ","
                Next
            End If
            If condizionelistaEdifici <> "" Then
                condizionelistaEdifici = Left(condizionelistaEdifici, Len(condizionelistaEdifici) - 1)
                If condizionelistalistaStProf <> "" Or condizionelistaComplessi <> "" Then
                    condizionelistaEdifici = " AND EDIFICI.ID IN (" & condizionelistaEdifici & ") "
                Else
                    condizionelistaEdifici = " AND EDIFICI.ID IN (" & condizionelistaEdifici & ") "
                End If
                listaEdificiSi = True
            End If

            Str = "SELECT UNITA_IMMOBILIARI.ID,tab_filiali.nome as filiale,(select RILIEVO_TAB_UTENTI.descrizione from siscom_mi.RILIEVO_TAB_UTENTI,siscom_mi.rilievo_lotti where RILIEVO_TAB_UTENTI.id=siscom_mi.rilievo_lotti.id_utente and rilievo_lotti.id=RILIEVO_UI.ID_lotto) as STUDIOPROF, COD_UNITA_IMMOBILIARE," _
                & "EDIFICI.DENOMINAZIONE,INDIRIZZI.DESCRIZIONE," _
                & "INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA," _
                & "UNITA_IMMOBILIARI.s_netta as mq,RILIEVO_ALLOGGIO_SFITTO.livello,'0' as IMP_STIMATO_INTERV,RILIEVO_ALLOGGIO_SFITTO.ID  AS ID_ALL_SFITTO,RILIEVO_UI.id_rilievo FROM SISCOM_MI.EDIFICI," _
                & "SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.IDENTIFICATIVI_CATASTALI," _
                & "SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.complessi_immobiliari,SISCOM_MI.RILIEVO_UI,siscom_mi.filiali_ui,siscom_mi.tab_filiali,siscom_mi.RILIEVO_ALLOGGIO_SFITTO,siscom_mi.rilievo_lotti,operatori " & formCondizStProf _
                & " WHERE EDIFICI.ID(+)=UNITA_IMMOBILIARI.ID_EDIFICIO and  complessi_immobiliari.id=edifici.id_Complesso " _
                & "AND unita_immobiliari.ID(+) = filiali_ui.id_ui " _
                & "AND filiali_ui.id_filiale = tab_filiali.ID(+) and filiali_ui.INIZIO_VALIDITA <='" & Format(Now, "yyyyMMdd") & "' AND filiali_ui.FINE_VALIDITA >= '" & Format(Now, "yyyyMMdd") & "' " _
                & "AND TIPO_LIVELLO_PIANO.COD(+)=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND SCALE_EDIFICI.ID (+)=UNITA_IMMOBILIARI.ID_SCALA AND " _
                & "IDENTIFICATIVI_CATASTALI.ID(+)=UNITA_IMMOBILIARI.ID_CATASTALE AND INDIRIZZI.ID (+)=UNITA_IMMOBILIARI.ID_indirizzo AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.ID_UNITA " _
                & " and RILIEVO_lotti.id=rilievo_ui.id_lotto and RILIEVO_UI.id_unita = RILIEVO_ALLOGGIO_SFITTO.ID_UNITA_IMMOBILIARE and RILIEVO_ALLOGGIO_SFITTO.ID_OPERATORE_INSERIMENTO =operatori.id " _
                & condizionelistaComplessi _
                & condizionelistaEdifici _
                & condizionelistaFiliali _
                & condizionelistalistaStProf _
                & condizioneRilevati _
                & " and RILIEVO_ALLOGGIO_SFITTO.id in (select max(id) from siscom_mi.RILIEVO_ALLOGGIO_SFITTO ria where RILIEVO_ALLOGGIO_SFITTO.id_unita_immobiliare=ria.id_unita_immobiliare) order by indirizzi.descrizione asc,UNITA_IMMOBILIARI.cod_unita_immobiliare asc"

            par.cmd.CommandText = Str
            Dim tipo As Integer = 0
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            Dim dtExcel As New Data.DataTable
            da.Fill(dt)
            da.Fill(dtExcel)
            da.Dispose()
            Dim operatoreInser As Boolean = False
            If dt.Rows.Count > 0 Then
                For Each rowIdContr As Data.DataRow In dt.Rows
                    tipo = 0
                    rowIdContr.Item("COD_UNITA_IMMOBILIARE") = "<a href='javascript:AfterSubmit()' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LET=1&ID=" & rowIdContr.Item("COD_UNITA_IMMOBILIARE") & "','DettagliUI','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & rowIdContr.Item("COD_UNITA_IMMOBILIARE") & "</a>"
                    Select Case par.IfNull(rowIdContr.Item("livello"), "")
                        Case "BASSO"
                            tipo = 1
                        Case "MEDIO"
                            tipo = 2
                        Case "ALTO"
                            tipo = 3
                    End Select
                    par.cmd.CommandText = "select valore from SISCOM_MI.RILIEVO_VAL_MONETARI where id_rilievo=" & rowIdContr.Item("id_rilievo") & " and id_tipo=" & tipo
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        rowIdContr.Item("IMP_STIMATO_INTERV") = Format(par.IfNull(myReader("valore"), 0) * par.IfNull(rowIdContr.Item("MQ"), 0), "##,##0.00")
                    End If
                    myReader.Close()
                Next
                For Each rowIdContr2 As Data.DataRow In dtExcel.Rows
                    tipo = 0
                    Select Case par.IfNull(rowIdContr2.Item("livello"), "")
                        Case "BASSO"
                            tipo = 1
                        Case "MEDIO"
                            tipo = 2
                        Case "ALTO"
                            tipo = 3
                    End Select
                    par.cmd.CommandText = "select valore from SISCOM_MI.RILIEVO_VAL_MONETARI where id_rilievo=" & rowIdContr2.Item("id_rilievo") & " and id_tipo=" & tipo
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        rowIdContr2.Item("IMP_STIMATO_INTERV") = Format(par.IfNull(myReader("valore"), 0) * par.IfNull(rowIdContr2.Item("MQ"), 0), "##,##0.00")
                    End If
                    myReader.Close()
                Next
            End If
            DataGridUIrilevata.DataSource = dt
            Session.Add("dtRiattam", dtExcel)
            DataGridUIrilevata.DataBind()
            lblNumRisult.Text = " - Totale: " & Format(dt.Rows.Count, "##,##0") & " unità"
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - RisultatiUnità - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property Str() As String
        Get
            If Not (ViewState("Str") Is Nothing) Then
                Return ViewState("Str")
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("Str") = value
        End Set
    End Property

    Protected Sub DataGridUIrilevata_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridUIrilevata.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("style", "cursor:pointer;")
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;SelColo=OldColor;this.style.backgroundColor='#FFFFCC'};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;SelColo=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=SelColo;}SelColo=OldColor;Selezionato=this;this.style.backgroundColor='#FF9900';document.getElementById('CPContenuto_IDui').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

    Protected Sub DataGridUIrilevata_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridUIrilevata.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridUIrilevata.CurrentPageIndex = e.NewPageIndex
            BindGridUnita()
        End If
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Session.Remove("dtRiattam")
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub btnNewRicerca_Click(sender As Object, e As System.EventArgs) Handles btnNewRicerca.Click
        Session.Remove("dtRiattam")
        Response.Redirect("ReportUIriattam.aspx", False)
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim xls As New ExcelSiSol

        Dim dt As New Data.DataTable
        dt = Session.Item("dtRiattam")

        If DataGridUIrilevata.Items.Count > 0 Then

            Dim nomeFile As String = xls.EsportaExcelDaDataGridWithDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportUIRiattam", "Elenco UI", Me.DataGridUIrilevata, dt)

            If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                'par.modalDialogMessage("Export", "Export creato correttamente!", Me.Page, , "../FileTemp/" & nomeFile)
                'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "downloadFile", "downloadFile('../FileTemp/" & nomeFile & "');", True)

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)

            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Nessun risultato da esportare!');", True)
        End If
    End Sub
End Class
