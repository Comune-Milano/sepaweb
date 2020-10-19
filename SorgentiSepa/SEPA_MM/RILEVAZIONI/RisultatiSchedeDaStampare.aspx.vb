Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports OfficeOpenXml

Partial Class RILEVAZIONI_RisultatiSchedeDaStampare
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("MOD_RILIEVO") <> "1" Then
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

            'FILIALI
            Dim listaFiliali As System.Collections.Generic.List(Of String) = Session.Item("listaFiliali")
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
                    condizionelistaComplessi = " AND COMPLESSI_IMMOBILIARI.ID IN (" & condizionelistaComplessi & ") "
                End If

                listaComplessiSi = True
            End If

            Dim listaEdifici As System.Collections.Generic.List(Of String) = Session.Item("listaEdifici")
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
            Dim condizioneRilevati As String = ""
            condizioneRilevati = " AND UNITA_IMMOBILIARI.ID in (select id_unita from siscom_mi.RILIEVO_UI where id_lotto is not null and id_unita in (select id_unita_immobiliare from siscom_mi.RILIEVO_ALLOGGIO_SFITTO where NON_VISITAB_LASTRATURA=0 and NON_VISITAB_OCCUPATO=0 and NON_VISITAB_MURATO=0 and NON_VISITAB_NO_CHIAVI=0)) "


            Str = "SELECT UNITA_IMMOBILIARI.ID,tab_filiali.nome as filiale,(select RILIEVO_TAB_UTENTI.descrizione from siscom_mi.RILIEVO_TAB_UTENTI,siscom_mi.rilievo_lotti where RILIEVO_TAB_UTENTI.id=siscom_mi.rilievo_lotti.id_utente and rilievo_lotti.id=RILIEVO_UI.ID_lotto) as STUDIOPROF, COD_UNITA_IMMOBILIARE," _
                & "EDIFICI.DENOMINAZIONE,INDIRIZZI.DESCRIZIONE," _
                & "INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,INDIRIZZI.CAP," _
                & "INDIRIZZI.LOCALITA,'' as RILEVATA,'' as motivazione," _
                & "TO_CHAR (TO_DATE (DATA_CARICAMENTO, 'YYYYmmdd'), 'DD/MM/YYYY') as DATA_CARICAMENTO, OPERATORI.OPERATORE,RILIEVO_ALLOGGIO_SFITTO.NOTE, '' AS DOWNLOAD_SCHEDA,'' AS ELIMINA_SCHEDA,RILIEVO_ALLOGGIO_SFITTO.*,RILIEVO_ALLOGGIO_SFITTO.ID  AS ID_ALL_SFITTO,RILIEVO_ALLOGGIO_SFITTO.ID_OPERATORE_INSERIMENTO FROM SISCOM_MI.EDIFICI," _
                & "SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.IDENTIFICATIVI_CATASTALI," _
                & "SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.complessi_immobiliari,SISCOM_MI.RILIEVO_UI,siscom_mi.filiali_ui,siscom_mi.tab_filiali,siscom_mi.RILIEVO_ALLOGGIO_SFITTO,siscom_mi.rilievo_lotti,operatori " & formCondizStProf _
                & " WHERE EDIFICI.ID(+)=UNITA_IMMOBILIARI.ID_EDIFICIO and  complessi_immobiliari.id=edifici.id_Complesso " _
                & "AND unita_immobiliari.ID(+) = filiali_ui.id_ui " _
                & "AND filiali_ui.id_filiale = tab_filiali.ID(+) and filiali_ui.INIZIO_VALIDITA <='" & Format(Now, "yyyyMMdd") & "' AND filiali_ui.FINE_VALIDITA >= '" & Format(Now, "yyyyMMdd") & "' " _
                & "AND TIPO_LIVELLO_PIANO.COD(+)=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND SCALE_EDIFICI.ID (+)=UNITA_IMMOBILIARI.ID_SCALA AND " _
                & "IDENTIFICATIVI_CATASTALI.ID(+)=UNITA_IMMOBILIARI.ID_CATASTALE AND INDIRIZZI.ID (+)=UNITA_IMMOBILIARI.ID_indirizzo AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.ID_UNITA " _
                & " and RILIEVO_lotti.id=rilievo_ui.id_lotto and RILIEVO_UI.id_unita = RILIEVO_ALLOGGIO_SFITTO.ID_UNITA_IMMOBILIARE and RILIEVO_ALLOGGIO_SFITTO.ID_OPERATORE_INSERIMENTO =operatori.id " _
                & " and RILIEVO_ALLOGGIO_SFITTO.id in (select max(id) from siscom_mi.RILIEVO_ALLOGGIO_SFITTO ria where RILIEVO_ALLOGGIO_SFITTO.id_unita_immobiliare=ria.id_unita_immobiliare) " _
                & condizionelistaComplessi _
                & condizionelistaEdifici _
                & condizionelistaFiliali _
                & condizionelistalistaStProf _
                & condizioneRilevati _
                & "order by  UNITA_IMMOBILIARI.cod_unita_immobiliare asc, RILIEVO_ALLOGGIO_SFITTO.data_Caricamento desc, edifici.denominazione asc"
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Dim operatoreInser As Boolean = False
            If dt.Rows.Count > 0 Then
                For Each rowIdContr As Data.DataRow In dt.Rows
                    rowIdContr.Item("COD_UNITA_IMMOBILIARE") = "<a href='javascript:AfterSubmit()' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LET=1&ID=" & rowIdContr.Item("COD_UNITA_IMMOBILIARE") & "','DettagliUI','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & rowIdContr.Item("COD_UNITA_IMMOBILIARE") & "</a>"

                    If rowIdContr.Item("NON_VISITAB_OCCUPATO") = 1 Or rowIdContr.Item("NON_VISITAB_LASTRATURA") = 1 Or rowIdContr.Item("NON_VISITAB_MURATO") = 1 Or rowIdContr.Item("NON_VISITAB_NO_CHIAVI") = 1 Then
                        rowIdContr.Item("RILEVATA") = "NO"
                        par.cmd.CommandText = "select * from siscom_mi.RILIEVO_ALLOGGIO_SFITTO where id_unita_immobiliare=" & rowIdContr.Item("ID")
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            If lettore("NON_VISITAB_OCCUPATO") = 1 Then
                                rowIdContr.Item("MOTIVAZIONE") = "OCCUPATO"
                            End If
                            If lettore("NON_VISITAB_LASTRATURA") = 1 Then
                                rowIdContr.Item("MOTIVAZIONE") = "LASTRATURA"
                            End If
                            If lettore("NON_VISITAB_MURATO") = 1 Then
                                rowIdContr.Item("MOTIVAZIONE") = "MURATO"
                            End If
                            If lettore("NON_VISITAB_NO_CHIAVI") = 1 Then
                                rowIdContr.Item("MOTIVAZIONE") = "ASSENZA CHIAVI"
                            End If
                        Else
                            rowIdContr.Item("MOTIVAZIONE") = "SCHEDA NON CARICATA"
                        End If
                        lettore.Close()
                    Else
                        rowIdContr.Item("RILEVATA") = "SI"
                    End If
                Next
            End If
            DataGridUnita.DataSource = dt
            DataGridUnita.DataBind()
            lblNumRisult.Text = "(" & DataGridUnita.Items.Count & " nella pagina - Totale:" & Format(dt.Rows.Count, "##,##0") & " schede)"
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

    Protected Sub DataGridUnita_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridUnita.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridUnita.CurrentPageIndex = e.NewPageIndex
            BindGridUnita()
        End If
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Session.Remove("listaFiliali")
        Session.Remove("listaStProf")
        Session.Remove("listaComplessi")
        Session.Remove("listaEdifici")
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub btnNewRicerca_Click(sender As Object, e As System.EventArgs) Handles btnNewRicerca.Click
        Session.Remove("listaFiliali")
        Session.Remove("listaStProf")
        Session.Remove("listaComplessi")
        Session.Remove("listaEdifici")
        Response.Redirect("RicercaSchedeDaStampare.aspx", False)
    End Sub

    Private Sub ScaricaFile()
        Try
            Dim bw As BinaryWriter
            Dim codUI As String = ""
            Dim idAllSfitto As Integer = 0

            Dim Elenco As String = ""
            Dim oDataGridItem As DataGridItem
            Dim chkExport As System.Web.UI.WebControls.CheckBox

            For Each oDataGridItem In Me.DataGridUnita.Items
                chkExport = oDataGridItem.FindControl("ChkSelected")
                If chkExport.Checked Then
                    Elenco = Elenco & oDataGridItem.Cells(0).Text & ","
                End If
            Next

            connData.apri()

            Dim Xls As Byte()
            Dim NomeFileXls As String = ""
            Dim formatoFile As String = ""
            Dim dataCaricamento As String = ""
            par.cmd.CommandText = " select * " _
                                & "FROM siscom_mi.RILIEVO_GESTIONE_SCHEDE " _
                                & "WHERE in_uso=1"
            Dim MyReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader2.Read Then
                Xls = par.IfNull(MyReader2("file_Excel"), "")
                formatoFile = "." & par.IfNull(MyReader2("estensione_file"), "")
                dataCaricamento = par.IfNull(MyReader2("data_Caricamento"), "")
            End If
            MyReader2.Close()

            'NomeFileXls = "Scheda_manutentiva_" & dataCaricamento
            'Dim fileName As String = Server.MapPath("..\FileTemp\") & "Scheda_manutentiva_" & Format(Now, "yyyyMMddHHmmss") & ".zip"
            'Dim fs As New FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite)
            'bw = New BinaryWriter(fs)
            'bw.Write(Xls)
            'bw.Flush()
            'bw.Close()
            'par.EstraiZipFile(fileName, Server.MapPath("~\FileTemp\"), "")

            Dim ElencoFile() As String
            Dim i As Integer = 0

            'fileName = Server.MapPath("~\FileTemp\") & NomeFileXls & formatoFile
            Dim tipo As Integer = 0
            
            If Elenco <> "" Then
                Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"
                par.cmd.CommandText = "select unita_immobiliari.id as idUNI,COD_UNITA_IMMOBILIARE,'' as sede_territoriale,'' as mm_spa,interno as n_ui,UNITA_IMMOBILIARI.s_netta,SCALE_EDIFICI.DESCRIZIONE AS SC,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIAN,tab_quartieri.nome as quartiere, indirizzi.descrizione as descr,indirizzi.civico from siscom_mi.unita_immobiliari,siscom_mi.indirizzi,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO,siscom_mi.edifici,siscom_mi.complessi_immobiliari,siscom_mi.tab_quartieri where unita_immobiliari.id_indirizzo=indirizzi.id(+) " _
                                   & " and edifici.id_complesso=complessi_immobiliari.id and unita_immobiliari.id_edificio=edifici.id and complessi_immobiliari.id_quartiere=tab_quartieri.id and UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) and UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) and unita_immobiliari.id in " & Elenco
                Dim da1 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt1 As New Data.DataTable
                da1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da1.Fill(dt1)
                If dt1.Rows.Count > 0 Then
                    For Each rowScheda As Data.DataRow In dt1.Rows
                        codUI = par.IfNull(rowScheda.Item("COD_UNITA_IMMOBILIARE"), "")

                        NomeFileXls = "Scheda_manutentiva_" & dataCaricamento
                        Dim fileName As String = Server.MapPath("..\FileTemp\") & "Scheda_manutentiva_" & Format(Now, "yyyyMMddHHmmss") & i & ".zip"
                        Dim fs As New FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite)
                        bw = New BinaryWriter(fs)
                        bw.Write(Xls)
                        bw.Flush()
                        bw.Close()
                        par.EstraiZipFile(fileName, Server.MapPath("~\FileTemp\"), "")

                        fileName = Server.MapPath("~\FileTemp\") & NomeFileXls & formatoFile
                        Dim fileName2 As String = Server.MapPath("~\FileTemp\") & "SCHEDA_" & codUI & "_" & Format(Now, "yyyyMMddHHmmss") & formatoFile

                        ReDim Preserve ElencoFile(i)

                        ElencoFile(i) = fileName2

                        Dim newFile As New FileInfo(fileName)
                        Dim pck As New ExcelPackage(newFile)
                        Dim ws = pck.Workbook.Worksheets("SCHEDA MANUT ALL SFITTI")
                       
                        Dim nomeFileUnico As String = "Scheda_manutentiva_" & codUI & "_" & Format(Now, "yyyyMMddHHmmss") & ""

                        ws.Cells("N3").Value = par.IfNull(rowScheda.Item("sede_territoriale"), "")
                        ws.Cells("X3").Value = par.IfNull(rowScheda.Item("mm_spa"), "")
                        ws.Cells("AJ3").Value = codUI
                        ws.Cells("B6").Value = par.IfNull(rowScheda.Item("quartiere"), "")
                        ws.Cells("N6").Value = par.IfNull(rowScheda.Item("descr"), "") & " " & par.IfNull(rowScheda.Item("civico"), "")
                        ws.Cells("AJ6").Value = par.IfNull(rowScheda.Item("sc"), "")
                        ws.Cells("AR6").Value = par.IfNull(rowScheda.Item("pian"), "")
                        ws.Cells("AZ6").Value = par.IfNull(rowScheda.Item("n_ui"), "")
                        ws.Cells("BH6").Value = par.IfNull(rowScheda.Item("s_netta"), 0)

                        par.cmd.CommandText = "select tab_filiali.nome as filiale from siscom_mi.tab_filiali,siscom_mi.filiali_ui where " _
                            & " filiali_ui.id_filiale = tab_filiali.ID(+) and id_ui=" & par.IfNull(rowScheda.Item("idUNI"), 0)
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReader.Read Then
                            ws.Cells("N3").Value = par.IfNull(myReader("filiale"), "")
                        End If
                        myReader.Close()


                        par.cmd.CommandText = "select rilievo_referenti.* from siscom_mi.rilievo_ui,siscom_mi.rilievo_lotti,siscom_mi.rilievo_referenti where rilievo_referenti.id=rilievo_lotti.id_referente and rilievo_ui.id_lotto=rilievo_lotti.id and rilievo_ui.id_unita=" & par.IfNull(rowScheda.Item("idUNI"), 0)
                        myReader = par.cmd.ExecuteReader
                        If myReader.Read Then
                            ws.Cells("B11").Value = par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "")
                            ws.Cells("T11").Value = par.IfNull(myReader("TELEFONO"), "")
                            ws.Cells("AB11").Value = par.IfNull(myReader("email"), "")
                        End If
                        myReader.Close()

                       
                        par.cmd.CommandText = "select * from siscom_mi.rilievo_alloggio_sfitto where id_unita_immobiliare=" & par.IfNull(rowScheda.Item("idUNI"), 0) & " order by data_rilievo desc"
                        myReader = par.cmd.ExecuteReader
                        If myReader.Read Then
                            idAllSfitto = par.IfNull(myReader("id"), 0)
                            ws.Cells("AJ11").Value = par.FormattaData(par.IfNull(myReader("data_sopralluogo"), ""))
                            ws.Cells("B248").Value = par.FormattaData(par.IfNull(myReader("data_rilievo"), ""))
                            ws.Cells("AV11").Value = par.FormattaData(par.IfNull(myReader("data_caricamento"), ""))
                            ws.Cells("BF11").Value = par.FormattaData(par.IfNull(myReader("data_ultimo_rilievo"), ""))

                            tipo = 0
                            Select Case par.IfNull(myReader("livello"), "")
                                Case "BASSO"
                                    tipo = 1
                                Case "MEDIO"
                                    tipo = 2
                                Case "ALTO"
                                    tipo = 3
                            End Select

                            par.cmd.CommandText = "select valore from SISCOM_MI.RILIEVO_VAL_MONETARI where id_rilievo=0 and id_tipo=" & tipo
                            Dim myReaderAA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderAA.Read Then
                                ws.Cells("BJ238").Value = Format(par.IfNull(myReaderAA("valore"), 0) * par.IfNull(rowScheda.Item("s_netta"), 0), "##,##0.00")
                            End If
                            myReaderAA.Close()


                            If par.IfNull(myReader("ADATTABILE_DISABILI"), 0) = 1 Then
                                ws.Cells("AF219").Value = "X"
                            End If
                            If par.IfNull(myReader("ASCENSORE"), 0) = 1 Then
                                ws.Cells("H211").Value = "X"
                            Else
                                ws.Cells("N211").Value = "X"
                            End If
                            If par.IfNull(myReader("SCIVOLI"), 0) = 1 Then
                                ws.Cells("T214").Value = "X"
                            Else
                                ws.Cells("Z214").Value = "X"
                            End If
                            If par.IfNull(myReader("MONTASCALE"), 0) = 1 Then
                                ws.Cells("T217").Value = "X"
                            Else
                                ws.Cells("Z217").Value = "X"
                            End If
                            If par.IfNull(myReader("FORO_AREAZIONE_100"), 0) = 1 Then
                                ws.Cells("T222").Value = "X"
                            Else
                                ws.Cells("Z222").Value = "X"
                            End If
                            ws.Cells("T224").Value = par.IfNull(myReader("NOME_LOCALE_FORO_AREAZ_100"), "")
                            If par.IfNull(myReader("FORO_AREAZIONE_200"), 0) = 1 Then
                                ws.Cells("T227").Value = "X"
                            Else
                                ws.Cells("Z227").Value = "X"
                            End If
                            ws.Cells("T229").Value = par.IfNull(myReader("NOME_LOCALE_FORO_AREAZ_200"), "")
                            If par.IfNull(myReader("COD_STATO_CONSERV"), "") = "BUONO" Then
                                ws.Cells("T231").Value = "X"
                            ElseIf par.IfNull(myReader("COD_STATO_CONSERV"), "") = "MEDIOCRE" Then
                                ws.Cells("T233").Value = "X"
                            ElseIf par.IfNull(myReader("COD_STATO_CONSERV"), "") = "SCADENTE" Then
                                ws.Cells("T235").Value = "X"
                            End If
                            If par.IfNull(myReader("LIVELLO"), "") = "BASSO" Then
                                ws.Cells("AL231").Value = "X"
                            ElseIf par.IfNull(myReader("LIVELLO"), "") = "MEDIO" Then
                                ws.Cells("AL233").Value = "X"
                            ElseIf par.IfNull(myReader("LIVELLO"), "") = "ALTO" Then
                                ws.Cells("AL235").Value = "X"
                            End If
                            If par.IfNull(myReader("AGIBILE"), 0) = 1 Then
                                ws.Cells("BF231").Value = "X"
                            End If
                            If par.IfNull(myReader("AGIBILE"), 0) = 0 Then
                                ws.Cells("BF233").Value = "X"
                            End If
                            If par.IfNull(myReader("ADEGUATO_DISABILI"), 0) = 1 Then
                                ws.Cells("T219").Value = "X"
                            End If
                            If par.IfNull(myReader("ADEGUATO_ASCENSORE"), 0) = 1 Then
                                ws.Cells("V211").Value = "X"
                            End If
                            If par.IfNull(myReader("NON_VISITAB_LASTRATURA"), 0) = 1 Then
                                ws.Cells("J238").Value = "X"
                            End If
                            If par.IfNull(myReader("NON_VISITAB_OCCUPATO"), 0) = 1 Then
                                ws.Cells("R238").Value = "X"
                            End If
                            If par.IfNull(myReader("NON_VISITAB_MURATO"), 0) = 1 Then
                                ws.Cells("X238").Value = "X"
                            End If
                            If par.IfNull(myReader("NON_VISITAB_NO_CHIAVI"), 0) = 1 Then
                                ws.Cells("AB238").Value = "X"
                            End If
                            If par.IfNull(myReader("ADATTABILE_DISABILI"), 0) = 1 Then
                                ws.Cells("AF219").Value = "X"
                            End If
                            If par.IfNull(myReader("PERTINENZA_PORTA"), 0) = 1 Then
                                ws.Cells("N58").Value = "X"
                            Else
                                ws.Cells("P58").Value = "X"
                            End If
                            If par.IfNull(myReader("PERTINENZA_INFILTRAZIONI"), 0) = 1 Then
                                ws.Cells("N59").Value = "X"
                            Else
                                ws.Cells("P59").Value = "X"
                            End If
                            ws.Cells("T179").Value = par.IfNull(myReader("LOCALE_PAVIMENTI"), "")
                            ws.Cells("H203").Value = par.IfNull(myReader("NOTE"), "")
                            ws.Cells("T198").Value = par.IfNull(myReader("VARIE"), "")
                            ws.Cells("H248").Value = par.IfNull(myReader("NOME_TECNICO"), "")
                            ws.Cells("AT248").Value = par.IfNull(myReader("NOME_RESPONSABILE"), "")

                        End If
                        myReader.Close()

                        par.cmd.CommandText = "select * from siscom_mi.rilievo_scheda_manutentiva where id_alloggio_sfitto=" & idAllSfitto & " order by id asc"
                        Dim daR As Oracle.DataAccess.Client.OracleDataAdapter
                        Dim dtR As New Data.DataTable
                        daR = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        daR.Fill(dtR)
                        If dtR.Rows.Count > 0 Then
                            For Each rowRM As Data.DataRow In dtR.Rows
                                Select Case rowRM.Item("id_desc_st_manut")
                                    Case "1"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T20").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z20").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF20").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN20").Value = "X"
                                        End If
                                    Case "2"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T22").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z22").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF22").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN22").Value = "X"
                                        End If
                                    Case "3"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T24").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z24").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF24").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN24").Value = "X"
                                        End If
                                    Case "4"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T26").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z26").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF26").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN26").Value = "X"
                                        End If
                                    Case "5"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T27").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z27").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF27").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN27").Value = "X"
                                        End If
                                    Case "6"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T29").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF29").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN29").Value = "X"
                                        End If
                                    Case "7"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T31").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z31").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF31").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AL31").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_5") = 1 Then
                                            ws.Cells("AP31").Value = "X"
                                        End If
                                        ws.Cells("R31").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "8"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T33").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z33").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF33").Value = "X"
                                        End If
                                    Case "9"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T36").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z36").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF36").Value = "X"
                                        End If
                                        ws.Cells("R36").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "10"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T38").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z38").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AH38").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN38").Value = "X"
                                        End If
                                        ws.Cells("R38").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "11"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T42").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z42").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF42").Value = "X"
                                        End If
                                        ws.Cells("R42").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "12"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T44").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z44").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AK44").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AP44").Value = "X"
                                        End If
                                    Case "13"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T45").Value = "X"
                                        End If
                                    Case "14"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T47").Value = "X"
                                        End If
                                    Case "15"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T51").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z51").Value = "X"
                                        End If
                                    Case "16"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T54").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z54").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AH54").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN54").Value = "X"
                                        End If
                                    Case "17"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T57").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z57").Value = "X"
                                        End If
                                    Case "18"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T61").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z61").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AH61").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN61").Value = "X"
                                        End If
                                    Case "19"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T63").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z63").Value = "X"
                                        End If
                                    Case "20"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T66").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z66").Value = "X"
                                        End If
                                    Case "21"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T69").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z69").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF69").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN69").Value = "X"
                                        End If
                                        ws.Cells("L69").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "22"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T72").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z72").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF72").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN72").Value = "X"
                                        End If
                                    Case "23"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T74").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z74").Value = "X"
                                        End If
                                    Case "24"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T77").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z77").Value = "X"
                                        End If
                                        ws.Cells("R77").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "25"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T79").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z79").Value = "X"
                                        End If
                                        ws.Cells("R79").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "26"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T82").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z82").Value = "X"
                                        End If
                                        ws.Cells("R82").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "27"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T87").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z87").Value = "X"
                                        End If
                                    Case "28"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T90").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z90").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF90").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN90").Value = "X"
                                        End If
                                        ws.Cells("R90").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "29"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T92").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z92").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF92").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN92").Value = "X"
                                        End If
                                        ws.Cells("R92").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "30"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T94").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z94").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF94").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN94").Value = "X"
                                        End If
                                        ws.Cells("R94").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "31"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T96").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z96").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF96").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN96").Value = "X"
                                        End If
                                        ws.Cells("R96").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "32"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T98").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z98").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF98").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN98").Value = "X"
                                        End If
                                        ws.Cells("R98").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "33"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T100").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z100").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF100").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN100").Value = "X"
                                        End If
                                        ws.Cells("R100").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "34"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T102").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z102").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF102").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN102").Value = "X"
                                        End If
                                        ws.Cells("R102").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "35"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T104").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z104").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF104").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN104").Value = "X"
                                        End If
                                        ws.Cells("R104").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "36"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T106").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z106").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF106").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN106").Value = "X"
                                        End If
                                        ws.Cells("R106").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "37"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T109").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z109").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF109").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN109").Value = "X"
                                        End If
                                        ws.Cells("R109").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "38"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T111").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z111").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF111").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN111").Value = "X"
                                        End If
                                        ws.Cells("R111").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "39"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T113").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z113").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF113").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN113").Value = "X"
                                        End If
                                        ws.Cells("R113").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "40"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T115").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z115").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF115").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN115").Value = "X"
                                        End If
                                        ws.Cells("R115").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "41"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T117").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z117").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF117").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN117").Value = "X"
                                        End If
                                        ws.Cells("R117").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "42"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T119").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z119").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF119").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN119").Value = "X"
                                        End If
                                        ws.Cells("R119").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "43"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T121").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z121").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF121").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN121").Value = "X"
                                        End If
                                        ws.Cells("R121").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "44"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T123").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z123").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF123").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN123").Value = "X"
                                        End If
                                        ws.Cells("R123").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "45"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T125").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z125").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF125").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN125").Value = "X"
                                        End If
                                        ws.Cells("R125").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "46"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T127").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z127").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF127").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN127").Value = "X"
                                        End If
                                        ws.Cells("R127").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "47"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T128").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z128").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF128").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN128").Value = "X"
                                        End If
                                        ws.Cells("R128").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "48"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T131").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z131").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF131").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN131").Value = "X"
                                        End If
                                        ws.Cells("R131").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "49"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T133").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z133").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF133").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN133").Value = "X"
                                        End If
                                        ws.Cells("R133").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "50"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T135").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z135").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF135").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN135").Value = "X"
                                        End If
                                        ws.Cells("R135").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "51"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T137").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z137").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF137").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN137").Value = "X"
                                        End If
                                        ws.Cells("R137").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "52"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T139").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z139").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF139").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN139").Value = "X"
                                        End If
                                        ws.Cells("R139").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "53"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T141").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z141").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF141").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN141").Value = "X"
                                        End If
                                        ws.Cells("R141").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "54"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T144").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z144").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF144").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN144").Value = "X"
                                        End If
                                        ws.Cells("R144").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "55"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T146").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z146").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF146").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN146").Value = "X"
                                        End If
                                        ws.Cells("R146").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "56"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T148").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z148").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF148").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN148").Value = "X"
                                        End If
                                        ws.Cells("R148").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "57"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T150").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z150").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF150").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN150").Value = "X"
                                        End If
                                        ws.Cells("R150").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "58"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T152").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z152").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF152").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN152").Value = "X"
                                        End If
                                        ws.Cells("R152").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "59"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T154").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z154").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF154").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN154").Value = "X"
                                        End If
                                        ws.Cells("R154").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "60"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T156").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z156").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF156").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN156").Value = "X"
                                        End If
                                        ws.Cells("R156").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "61"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T158").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z158").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF158").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN158").Value = "X"
                                        End If
                                        ws.Cells("R158").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "62"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T160").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z160").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF160").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN160").Value = "X"
                                        End If
                                        ws.Cells("R160").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "63"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T162").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z162").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF162").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN162").Value = "X"
                                        End If
                                        ws.Cells("R162").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "64"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T164").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z164").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF164").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN164").Value = "X"
                                        End If
                                        ws.Cells("R164").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "65"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T167").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z167").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF167").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN167").Value = "X"
                                        End If
                                        ws.Cells("R167").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "66"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T169").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z169").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF169").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN169").Value = "X"
                                        End If
                                        ws.Cells("R169").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "67"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T171").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z171").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF171").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN171").Value = "X"
                                        End If
                                        ws.Cells("R171").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "68"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T173").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z173").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF173").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN173").Value = "X"
                                        End If
                                        ws.Cells("R173").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "69"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T175").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z175").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF175").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN175").Value = "X"
                                        End If
                                        ws.Cells("R175").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "70"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T177").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z177").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF177").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN177").Value = "X"
                                        End If
                                        ws.Cells("R177").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "71"
                                    Case "72"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T182").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z182").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF182").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN182").Value = "X"
                                        End If
                                        ws.Cells("R182").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "73"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T184").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z184").Value = "X"
                                        End If
                                    Case "74"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T186").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z186").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF186").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN186").Value = "X"
                                        End If
                                        ws.Cells("R186").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "75"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T188").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z188").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF188").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN188").Value = "X"
                                        End If
                                        ws.Cells("R188").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "76"
                                    Case "77"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T192").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z192").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF192").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN192").Value = "X"
                                        End If
                                        ws.Cells("R192").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "78"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T194").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z194").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF194").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN194").Value = "X"
                                        End If
                                        ws.Cells("R194").Value = par.IfNull(rowRM.Item("numero"), "")
                                    Case "79"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T39").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z39").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AK39").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AP39").Value = "X"
                                        End If
                                        ws.Cells("Z40").Value = par.IfNull(rowRM.Item("numero"), "")
                                        ws.Cells("AN40").Value = par.IfNull(rowRM.Item("vano"), "")
                                    Case "80"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T48").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z48").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AK48").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AP48").Value = "X"
                                        End If
                                        ws.Cells("Z49").Value = par.IfNull(rowRM.Item("numero"), "")
                                        ws.Cells("AN49").Value = par.IfNull(rowRM.Item("vano"), "")
                                    Case "81"
                                        If rowRM.Item("CHK_1") = 1 Then
                                            ws.Cells("T58").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_2") = 1 Then
                                            ws.Cells("Z58").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_3") = 1 Then
                                            ws.Cells("AF58").Value = "X"
                                        End If
                                        If rowRM.Item("CHK_4") = 1 Then
                                            ws.Cells("AN58").Value = "X"
                                        End If
                                End Select

                            Next
                        End If
                        
                        Dim newFile2 As New FileInfo(fileName2)
                        pck.SaveAs(newFile2)

                        i = i + 1

                       
                    Next
                    connData.chiudi()
                    If i > 0 Then

                         Dim zipfic As String
                        Dim NomeFilezip As String = "SCHEDE_MANUTENTIVE" & Format(Now, "yyyyMMddHHmmss") & ".zip"

                        zipfic = Server.MapPath("..\FileTemp\" & NomeFilezip)

                        Dim kkK As Integer = 0

                        Dim objCrc32 As New Crc32()
                        Dim strmZipOutputStream As ZipOutputStream

                        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                        strmZipOutputStream.SetLevel(6)

                        Dim strFile As String

                        For kkK = 0 To i - 1
                            strFile = ElencoFile(kkK)
                            Dim strmFile As FileStream = File.OpenRead(strFile)
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
                            'File.Delete(strFile)
                        Next
                        strmZipOutputStream.Finish()
                        strmZipOutputStream.Close()

                        If File.Exists(Server.MapPath("~\FileTemp\") & NomeFilezip) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & NomeFilezip & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                            Exit Sub
                        Else
                            par.modalDialogMessage("Attenzione", "Si è verificato un errore durante il download. Riprovare!", Me.Page)
                        End If
                    End If
                Else
                    par.modalDialogMessage("Attenzione", "Nessuna unità selezionata!", Me.Page)
                End If
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: RisultatiUIRilev - ScaricaFile - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnScaricaSchede_Click(sender As Object, e As System.EventArgs) Handles btnScaricaSchede.Click
        ScaricaFile()
    End Sub

    Protected Sub btnSelezionaTutti_Click(sender As Object, e As System.EventArgs) Handles btnSelezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        For Each oDataGridItem In Me.DataGridUnita.Items
            chkExport = oDataGridItem.FindControl("ChkSelected")
            chkExport.Checked = True
        Next
    End Sub

    Protected Sub btnDeselezionaTutti_Click(sender As Object, e As System.EventArgs) Handles btnDeselezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        For Each oDataGridItem In Me.DataGridUnita.Items
            chkExport = oDataGridItem.FindControl("ChkSelected")
            chkExport.Checked = False
        Next
    End Sub

End Class
