Imports System.Collections
Imports System.Data.OleDb
Imports System.IO
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_PagamentoRitLeggeExcel
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As Data.DataTable
    Dim dtErrori As Data.DataTable
    Public RisultatiVisibility As String = "hidden"

    Public Property convalida() As Integer
        Get
            If Not (ViewState("par_convalida") Is Nothing) Then
                Return CInt(ViewState("par_convalida"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_convalida") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lblTitolo.Text = "Elenco Contenuto file Excel"

        '##### CARICAMENTO PAGINA #####
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:770px; height:480px; top:50px; left:12px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br /><br /><br /><br /><br /><br /><br /><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Try
            If Not IsPostBack Then
                If Not IsNothing(Session.Item("dt_P")) Then
                    Session.Remove("dt_P")
                End If
                preElaborazione()
            End If
        Catch ex As Exception
            Session.Remove("LAVORAZIONE")
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub preElaborazione()
        '######## NASCONDO BOTTONI E DATAGRID PRIMA DELL'ELEABORAZIONE ########
        DataGridExcel.Visible = False
        lblTitolo.Visible = False
        btnSalva.Visible = False
        btnAnnulla.Visible = False
        lblNote.Visible = False
        txtNote.Visible = False
        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = False
        convalida = 0
        btnConvalida.Visible = False
        '#########################
    End Sub

    Protected Sub postElaborazione()
        '######## MOSTRO BOTTONI E DATAGRID DOPO L'ELEABORAZIONE ########
        DataGridExcel.Visible = True
        lblTitolo.Visible = True
        btnSalva.Visible = True
        btnAnnulla.Visible = True
        lblNote.Visible = True
        txtNote.Visible = True
        RisultatiVisibility = "visible"
        lblNote.Text = "Esito elaborazione:"
        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = False
        btnConvalida.Visible = False
        '#########################
    End Sub

    Protected Sub postImportazione()
        '######## NASCONDO BOTTONI E DATAGRID DOPO L'ELEABORAZIONE ########
        DataGridExcel.Visible = True
        lblTitolo.Visible = True
        btnSalva.Visible = False
        btnAnnulla.Visible = False
        lblNote.Visible = True
        txtNote.Visible = True
        RisultatiVisibility = "visible"
        lblNote.Text = "Esito importazione:"
        If convalida <> 0 Then
            btnConvalida.Visible = True
        End If
        '#########################
    End Sub

    Protected Sub apriConnessione()
        Try
            If par.OracleConn.State = 0 Then
                par.OracleConn.Open()
            End If
            par.cmd = par.OracleConn.CreateCommand
        Catch ex As Exception
            Response.Write("<script>parent.main.location.replace('PagamentoCertificatiExcel.aspx');</script>")
        End Try
    End Sub

    Protected Sub chiudiConnessione()
        Try
            If par.OracleConn.State = 1 Then
                par.cmd.Dispose()
                par.OracleConn.Close()
            End If
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Response.Write("<script>parent.main.location.replace('PagamentoCertificatiExcel.aspx');</script>")
        End Try
    End Sub

    Protected Sub btnElabora_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElabora.Click
        Dim nFile As String = ""
        Dim ElaborazioneInCorso As Integer = 0
        dt = New Data.DataTable
        txtNote.Text = ""
        Try
            If FileUpload1.HasFile = True Then
                '########## UPLOAD FILE EXCEL ##########
                nFile = Server.MapPath("..\..\..\FileTemp\") & FileUpload1.FileName
                FileUpload1.SaveAs(nFile)
                FileUpload1.SaveAs(Replace(nFile, ".xls", ".csv"))
                'Dim sr As New StreamReader(Server.MapPath("tuoFile.txt"))
                Dim sr As New StreamReader(nFile)
                Dim stringa As String = sr.ReadLine
                'CONTROLLO PRIMA STRINGA
                Try
                    Dim errore As Boolean = False
                    'CONTROLLI VECCHI
                    'If Trim(stringa.Substring(stringa.LastIndexOf(";") + 1)) <> "DATA PAGAMENTO MANDATO" Then
                    '    errore = True
                    'End If
                    'stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                    'If Trim(stringa.Substring(stringa.LastIndexOf(";") + 1)) <> "M.A.E." Then
                    '    errore = True
                    'End If
                    'stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                    'If Trim(stringa.Substring(stringa.LastIndexOf(";") + 1)) <> "ANNO MAE" Then
                    '    errore = True
                    'End If
                    'stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                    'If Trim(stringa.Substring(stringa.LastIndexOf(";") + 1)) <> "VOCE B.P." Then
                    '    errore = True
                    'End If
                    'stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                    'If Trim(stringa.Substring(stringa.LastIndexOf(";") + 1)) <> "C.D.P." Then
                    '    errore = True
                    'End If
                    'stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                    'If Trim(stringa.Substring(stringa.LastIndexOf(";") + 1)) <> "IMPORTO" Then
                    '    errore = True
                    'End If
                    'stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                    'If Trim(stringa.Substring(stringa.LastIndexOf(";") + 1)) <> "FORNITORE" Then
                    '    errore = True
                    'End If
                    'stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                    'If Trim(stringa.Substring(stringa.LastIndexOf(";") + 1)) <> "N.FORN." Then
                    '    errore = True
                    'End If
                    'CONTROLLO CONTEGGIO COLONNE
                    If Split(stringa, ";").Length <> 8 Then
                        errore = True
                    End If
                    If errore = True Then
                        Response.Write("<script>alert('Attenzione... Il file selezionato non è del tipo Excel Pagamento certificati SEPA!');</script>")
                        Exit Sub
                    End If
                Catch ex As Exception
                    Response.Write("<script>alert('Attenzione... Il file selezionato non è del tipo Excel Pagamento certificati SEPA!');</script>")
                    Exit Sub
                End Try
                ElaborazioneInCorso = 1
                Dim count As Integer = 0
                Dim check As Boolean = True
                CreaDT()
                Dim row As Data.DataRow
                While check = True
                    '######### CARICAMENTO DATATABLE ########
                    stringa = sr.ReadLine
                    Dim cont As Integer = Len(stringa) - Len(Replace(stringa, ";", ""))
                    If Not IsNothing(stringa) And cont = 7 Then
                        count = count + 1
                        row = dt.NewRow()
                        row.Item("#") = dt.Rows.Count + 1
                        row.Item("DATA_PAG") = Trim(stringa.Substring(stringa.LastIndexOf(";") + 1))
                        stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                        row.Item("MAE") = Trim(stringa.Substring(stringa.LastIndexOf(";") + 1))
                        stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                        row.Item("ANNO_MAE") = Trim(stringa.Substring(stringa.LastIndexOf(";") + 1))
                        stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                        row.Item("VOCE") = Trim(stringa.Substring(stringa.LastIndexOf(";") + 1))
                        stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                        row.Item("CDP") = Trim(stringa.Substring(stringa.LastIndexOf(";") + 1))
                        stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                        row.Item("IMPORTO") = Trim(stringa.Substring(stringa.LastIndexOf(";") + 1))
                        If Len(row.Item("IMPORTO")) > 0 Then
                            Try
                                row.Item("IMPORTO") = Format(CDec(row.Item("IMPORTO")), "##,##0.00")
                            Catch ex As Exception
                                row.Item("IMPORTO") = row.Item("IMPORTO")
                            End Try
                        End If
                        stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                        row.Item("FORNITORE") = Trim(stringa.Substring(stringa.LastIndexOf(";") + 1))
                        stringa = stringa.Substring(0, stringa.LastIndexOf(";"))
                        row.Item("N_FORN") = Trim(stringa.Substring(stringa.LastIndexOf(";") + 1))
                        row.Item("ERRORE") = ""
                        dt.Rows.Add(row)
                    Else
                        check = False
                    End If
                End While
                '######## CHIUSURA LETTORE ########
                sr.Close()
                ElaborazioneInCorso = 0
                Session.Add("dt_P", dt)
                '########## DATA BIND ########
                If dt.Rows.Count > 0 Then
                    DataGridExcel.DataSource = dt
                    DataGridExcel.DataBind()
                    postElaborazione()
                End If
                '####### AGGIORNAMENTO ESITO ELABORAZIONE
                txtNote.Text = "************************* ELABORAZIONE *************************" & vbCrLf
                txtNote.Text = txtNote.Text & vbCrLf & "In elaborazione: " & dt.Rows.Count & " righe." & vbCrLf
                txtNote.Text = txtNote.Text & vbCrLf & "************************************************************************" & vbCrLf
            Else
                preElaborazione()
                Response.Write("<script>alert('Selezionare il file Excel Pagamento certificati SEPA!');</script>")
            End If
        Catch ex As Exception
            Dim messaggioErrore As String = Replace(ex.Message, "'", "")
            Response.Write("<script>alert('Attenzione... " & messaggioErrore & "!');</script>")
        End Try
    End Sub

    Protected Sub CreaDT()
        '######### SVUOTA E CREA COLONNE DATATABLE #########
        dt.Clear()
        dt.Columns.Clear()
        dt.Rows.Clear()
        dt.Columns.Add("#")
        dt.Columns.Add("N_FORN")
        dt.Columns.Add("FORNITORE")
        dt.Columns.Add("IMPORTO")
        dt.Columns.Add("CDP")
        dt.Columns.Add("ANNO_MAE")
        dt.Columns.Add("MAE")
        dt.Columns.Add("DATA_PAG")
        dt.Columns.Add("VOCE")
        dt.Columns.Add("ERRORE")
    End Sub

    Protected Sub CreaDTErrori()
        '######### SVUOTA E CREA COLONNE DATATABLE #########
        dtErrori = New Data.DataTable
        dtErrori.Clear()
        dtErrori.Columns.Clear()
        dtErrori.Rows.Clear()
        dtErrori.Columns.Add("#")
        dtErrori.Columns.Add("N_FORN")
        dtErrori.Columns.Add("IMPORTO")
        dtErrori.Columns.Add("CDP")
        dtErrori.Columns.Add("ANNO_MAE")
        dtErrori.Columns.Add("MAE")
        dtErrori.Columns.Add("DATA_PAG")
        dtErrori.Columns.Add("VOCE")
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

        '##### RIPRENDO DATATABLE #####
        dt = CType(HttpContext.Current.Session.Item("dt_P"), Data.DataTable)
        If Not IsNothing(Session.Item("dt_P")) Then
            Session.Remove("dt_P")
        End If
        '#################

        CreaDTErrori()

        'tolleranza di un euro nel totale consuntivato dei pagamenti
        Dim tolleranza As Decimal = 1
        Dim CDP As String = ""
        Dim ANNOCDP As String = ""
        Dim PROGCDP As String = ""
        Dim numeroRiga As Integer = 0
        Dim importo As String = ""
        Dim n_forn As String = ""
        Dim voce As String = ""
        Dim fornitore As String = ""
        Dim annoMAE As String = ""
        Dim MAE As String = ""
        Dim data_pag As String = ""
        Dim IDPagamento As Long = 0
        Dim totaleRigheImportate As Integer = 0
        Dim ANNOPF As Integer = 0
        Dim ANNO_ID_PF As Integer = 0

        txtNote.Text = "************************** IMPORTAZIONE **************************" & vbCrLf & vbCrLf


        If Not IsNothing(dt) Then
            apriConnessione()
            Dim indice As Integer = 0
            For Each riga In dt.Rows
                Dim insert As Integer = 1

                '###### RECUPERO DATI RIGA #######
                numeroRiga = riga("#")
                n_forn = riga("N_FORN")
                fornitore = riga("FORNITORE")
                importo = riga("IMPORTO")
                annoMAE = riga("ANNO_MAE")
                MAE = riga("MAE")
                data_pag = riga("DATA_PAG")
                voce = riga("VOCE")

                '######## VERIFICA DI CONTROLLO DELLA RIGA ######
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '1) VERIFICA CDP
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°



                CDP = Trim(riga("CDP"))
                If Len(CDP) < 6 Then
                    'CDP NON VALIDO
                    'SEGNALARE ANOMALIA
                    txtNote.Text = txtNote.Text & "C.D.P. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "C.D.P non valido"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) & "<br />C.D.P non valido"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    insert = 0
                Else
                    Try

                        ANNOCDP = Right(CDP, 4)
                        If Not IsNumeric(CLng(ANNOCDP)) Then

                            'ANNO CDP NON VALIDO
                            'SEGNALARE ANOMALIA
                            txtNote.Text = txtNote.Text & "Anno C.D.P. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            insert = 0
                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Anno C.D.P non valido"
                            Else
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Anno C.D.P non valido"
                            End If
                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                        End If

                    Catch ex As Exception

                        'ANNO CDP NON VALIDO
                        'SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Anno C.D.P. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Anno C.D.P non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Anno C.D.P non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True

                    End Try

                    Try

                        PROGCDP = Left(CDP, Len(CDP) - 5)
                        If Not IsNumeric(CLng(PROGCDP)) Then

                            'PROGCDP NON VALIDO
                            'SEGNALARE ANOMALIA
                            txtNote.Text = txtNote.Text & "Progr. C.D.P. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            insert = 0
                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Progr. C.D.P non valido"
                            Else
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Progr. C.D.P non valido"
                            End If
                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True

                        End If

                    Catch ex As Exception

                        'PROGCDP NON VALIDO
                        'SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Progr. C.D.P. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Progr. C.D.P non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Progr. C.D.P non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End Try
                End If


                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '2) VERIFICA ANNO MAE, MAE, DATA E IMPORTO
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

                'IMPORTO
                Try
                    If Not IsNumeric(CDec(importo)) Then
                        'IMPORTO NON VALIDO, SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Importo non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Importo non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Importo non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    Else
                        'SE PRESENTI, ELIMINARE I PUNTI
                        importo = Replace(importo, ".", "")
                    End If
                Catch ex As Exception
                    'IMPORTO NON VALIDO, SEGNALARE ANOMALIA
                    txtNote.Text = txtNote.Text & "Importo non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    insert = 0
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Importo non valido"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Importo non valido"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                End Try


                'ANNO MAE
                Try
                    If Len(Trim(annoMAE)) = 4 Then
                        If Not IsNumeric(CLng(annoMAE)) Then
                            'ANNO MAE NON VALIDO, SEGNALARE ANOMALIA
                            txtNote.Text = txtNote.Text & "Anno M.A.E. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            insert = 0
                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Anno M.A.E non valido"
                            Else
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Anno M.A.E non valido"
                            End If
                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                        End If
                    Else
                        'ANNO MAE NON VALIDO, SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Anno M.A.E. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Anno M.A.E non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Anno M.A.E non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End If
                Catch ex As Exception
                    'ANNO MAE NON VALIDO, SEGNALARE ANOMALIA
                    txtNote.Text = txtNote.Text & "Anno M.A.E. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    insert = 0
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Anno M.A.E non valido"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Anno M.A.E non valido"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                End Try

                'MAE
                Try
                    If Not IsNumeric(CLng(MAE)) Then
                        'MAE NON VALIDO, SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "M.A.E non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "M.A.E non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />M.A.E non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End If
                Catch ex As Exception
                    'MAE NON VALIDO, SEGNALARE ANOMALIA
                    txtNote.Text = txtNote.Text & "M.A.E. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    insert = 0
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "M.A.E non valido"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />M.A.E non valido"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                End Try


                'CONTROLLO DATA
                If Len(data_pag) = 0 Then
                    'NON VIENE INSERITA LA DATA, MA NON PUò ESSERE VUOTA
                    txtNote.Text = txtNote.Text & "Data non presente alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    insert = 0
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Data assente"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Data assente"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                Else
                    If Not IsDate(data_pag) Or Len(data_pag) <> 10 Then
                        'DATA NON VALIDA
                        txtNote.Text = txtNote.Text & "Data non valida alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Data non valida"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Data non valida"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    Else
                        'CONVERTO LA DATA NEL FORMATO DI INSERIMENTO
                        data_pag = Right(data_pag, 4) & Mid(data_pag, 4, 2) & Left(data_pag, 2)
                    End If
                End If

                '3) CONTROLLO SE ESISTE LA VOCE
                'RECUPERO L'ID DELLA VOCE IN QUESTIONE
                Dim ID_VocePF As String = ""
                Try

                    par.cmd.CommandText = "SELECT DISTINCT PF_VOCI.ID AS ID_VOCE,PF_MAIN.ID AS ID_PF, TRIM(SUBSTR(T_ESERCIZIO_FINANZIARIO.INIZIO,1,4)) AS ANNO " _
                    & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                    & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                    & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                    & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                    & "AND PF_VOCI.ID_PIANO_FINANZIARIO=PF_MAIN.ID " _
                    & "AND T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                    & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND PF_VOCI.CODICE='" & voce & "'"

                    Dim lettoreVoce As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreVoce.Read Then
                        ID_VocePF = par.IfNull(lettoreVoce("ID_VOCE"), 0)
                        ANNOPF = par.IfNull(lettoreVoce("ANNO"), 0)
                        ANNO_ID_PF = par.IfNull(lettoreVoce("ID_PF"), 0)
                    End If
                    lettoreVoce.Close()
                    If ID_VocePF = "" Then
                        'NESSUNA VOCE RILEVATA, SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Voce non rilevata nel piano finanziario alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Voce non valida"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Voce non valida"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End If
                Catch ex As Exception
                    'NESSUNA VOCE RILEVATA, SEGNALARE ANOMALIA
                    txtNote.Text = txtNote.Text & "Voce non rilevata nel piano finanziario alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    insert = 0
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Voce non valida"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Voce non valida"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                End Try

                Dim importoConsuntivato As Decimal = 0
                If insert <> 0 Then
                    '4) RILEVO ID PAGAMENTO
                    Try
                        par.cmd.CommandText = "SELECT ID,ROUND(IMPORTO_CONSUNTIVATO,2) FROM SISCOM_MI.PAGAMENTI WHERE ANNO='" & ANNOCDP & "' AND PROGR='" & PROGCDP & "'"

                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReader.Read Then
                            IDPagamento = myReader(0)
                            'importoConsuntivato = CDec(par.IfNull(myReader(1), 0))
                        Else
                            'NESSUN ID RILEVATO, SEGNALARE ANOMALIA
                            txtNote.Text = txtNote.Text & "Nessun pagamento rilevato per la riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            insert = 0
                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Pagamento non valido"
                            Else
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Pagamento non valido"
                            End If
                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                        End If
                        myReader.Close()
                        If IDPagamento > 0 Then
                            par.cmd.CommandText = "SELECT ROUND(SUM(NVL(RIT_LEGGE_IVATA,0)),2) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO_RIT_LEGGE= " & IDPagamento & "AND ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CODICE =(SELECT CODICE FROM SISCOM_MI.PF_VOCI WHERE ID=" & ID_VocePF & "))"
                            myReader = par.cmd.ExecuteReader
                            If myReader.Read Then
                                importoConsuntivato = CDec(par.IfNull(myReader(0), 0))
                            End If
                            myReader.Close()
                        End If
                    Catch ex As Exception
                        'NESSUN ID RILEVATO, SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Nessun pagamento rilevato per la riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Pagamento non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Pagamento non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End Try
                End If

                If insert <> 0 Then
                    '5)CONTROLLO LA COERENZA DEI DATI
                    Try
                        Dim erroreGenerico As Boolean = False
                        par.cmd.CommandText = "SELECT DISTINCT COD_FORNITORE,RAGIONE_SOCIALE,PROGR||'/'||PAGAMENTI.ANNO AS CDP,PF_VOCI.CODICE AS VOCE,PF_VOCI.ID AS ID_VOCE " _
                        & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                        & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                        & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                        & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                        & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "' AND CODICE='" & voce & "'"
                        Dim LettoreDati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If LettoreDati.Read Then
                            Dim CDP_C As String = par.IfNull(LettoreDati("CDP"), "")
                            Dim COD_C As String = par.IfNull(LettoreDati("COD_FORNITORE"), "")
                            Dim VOCE_C As String = par.IfNull(LettoreDati("VOCE"), "")
                            Dim IDVOCE_C As String = par.IfNull(LettoreDati("ID_VOCE"), "")
                            If CDP_C <> CDP Or COD_C <> n_forn Or VOCE_C <> voce Then
                                erroreGenerico = True
                            End If
                        Else
                            '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                            'CONTROLLO VOCE/SOTTOVOCE
                            '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                            Try
                                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IN (SELECT PF_VOCI.ID FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN WHERE CODICE='" & voce & "' AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO AND ID_PIANO_FINANZIARIO='" & ANNO_ID_PF & "' AND ID_STATO>=5)"
                                Dim LettoreCount As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                Dim conteggio As Integer = 0
                                If LettoreCount.Read Then
                                    conteggio = par.IfNull(LettoreCount(0), 0)
                                End If
                                LettoreCount.Close()
                                Dim codice_voce As String = ""
                                Dim id_voce As Integer = -1
                                If conteggio = 1 Then
                                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IN (SELECT PF_VOCI.ID FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN WHERE CODICE='" & voce & "' AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO AND ID_PIANO_FINANZIARIO='" & ANNO_ID_PF & "' AND ID_STATO>=5)"
                                    Dim lettoreVoce As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If lettoreVoce.Read Then
                                        codice_voce = par.IfNull(lettoreVoce("CODICE"), "")
                                        id_voce = par.IfNull(lettoreVoce("ID"), -1)
                                    End If
                                    lettoreVoce.Close()
                                End If
                                If codice_voce <> "" Then
                                    'SE LA VOCE INDICATA NEL FILE HA UNA SOLA SOTTOVOCE E QUESTA VERIFICA LA COERENZA ALLORA IMPORTIAMO LA RIGA
                                    par.cmd.CommandText = "SELECT DISTINCT COD_FORNITORE,RAGIONE_SOCIALE,PROGR||'/'||PAGAMENTI.ANNO AS CDP,PF_VOCI.CODICE AS VOCE " _
                                        & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                        & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                        & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                        & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                        & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "' AND CODICE='" & codice_voce & "'"
                                    Dim LettoreDati2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If LettoreDati2.Read Then
                                        Dim CDP_C As String = par.IfNull(LettoreDati2("CDP"), "")
                                        Dim COD_C As String = par.IfNull(LettoreDati2("COD_FORNITORE"), "")
                                        Dim VOCE_C As String = par.IfNull(LettoreDati2("VOCE"), "")
                                        If CDP_C <> CDP Or COD_C <> n_forn Or VOCE_C <> codice_voce Then
                                            erroreGenerico = True
                                        Else
                                            'COERENZA VERIFICATA,MODIFICO VOCEPF
                                            ID_VocePF = id_voce
                                            txtNote.Text = txtNote.Text & "Voce modificata da '" & voce & "' a '" & codice_voce & "' alla riga " & numeroRiga & ". " & vbCrLf
                                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Gainsboro
                                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "VOCE")).Text = codice_voce
                                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Voce modificata da '" & voce & "' a '" & codice_voce & "'"
                                            Else
                                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Voce modificata da '" & voce & "' a '" & codice_voce & "'"
                                            End If
                                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                                        End If
                                    Else
                                        'ALTRI TIPI DI CONTROLLO

                                        'CONTROLLO FORNITORE SBAGLIATO
                                        par.cmd.CommandText = "SELECT COD_FORNITORE FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI WHERE PROGR='" & PROGCDP & "' AND FORNITORI.ID=PAGAMENTI.ID_FORNITORE"
                                        Dim LettoreFornitore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        Dim codice_fornitore As String = ""
                                        If LettoreFornitore.Read Then
                                            codice_fornitore = par.IfNull(LettoreFornitore(0), "")
                                        End If
                                        LettoreFornitore.Close()
                                        If codice_fornitore <> n_forn Then
                                            'CONTROLLO CHE IL FORNITORE VERIFICHI LA COERENZA, ALTRIMENTI CI SONO PIù DATI NON COERENTI
                                            par.cmd.CommandText = "SELECT COUNT(*) " _
                                                & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                                & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                                & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                                & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & codice_fornitore & "' AND CODICE='" & voce & "'"
                                            Dim ControlloFornitore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                            Dim ConteggioFornitore As Integer = 0
                                            If ControlloFornitore.Read Then
                                                ConteggioFornitore = par.IfNull(ControlloFornitore(0), 0)
                                            End If
                                            ControlloFornitore.Close()
                                            If ConteggioFornitore = 1 Then
                                                txtNote.Text = txtNote.Text & "Il fornitore indicato per la riga " & numeroRiga & " è errato. Pagamento non importato." & vbCrLf
                                                DataGridExcel.Items(indice).BackColor = Drawing.Color.Red
                                                insert = 0
                                                convalida = convalida + 1

                                                Dim rigaErrori As Data.DataRow
                                                rigaErrori = dtErrori.NewRow
                                                rigaErrori.Item("#") = numeroRiga
                                                rigaErrori.Item("N_FORN") = codice_fornitore
                                                rigaErrori.Item("IMPORTO") = importo
                                                rigaErrori.Item("CDP") = CDP
                                                rigaErrori.Item("ANNO_MAE") = annoMAE
                                                rigaErrori.Item("MAE") = MAE
                                                rigaErrori.Item("DATA_PAG") = data_pag
                                                rigaErrori.Item("VOCE") = voce

                                                dtErrori.Rows.Add(rigaErrori)

                                                If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Fornitore errato, sostituire '" & n_forn & "' con '" & codice_fornitore & "'"
                                                Else
                                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Fornitore errato, sostituire '" & n_forn & "' con '" & codice_fornitore & "'"
                                                End If
                                                DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                                            Else
                                                erroreGenerico = True
                                            End If
                                        Else
                                            'CONTROLLARE LA VOCE SBAGLIATA
                                            par.cmd.CommandText = "SELECT COUNT(*) " _
                                                & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                                & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                                & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                                & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "'"
                                            Dim LettoreCodice As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                            Dim codici As Integer = 0
                                            If LettoreCodice.Read Then
                                                codici = par.IfNull(LettoreCodice(0), 0)
                                            End If
                                            LettoreCodice.Close()
                                            If codici = 1 Then
                                                par.cmd.CommandText = "SELECT DISTINCT COD_FORNITORE,RAGIONE_SOCIALE,PROGR||'/'||PAGAMENTI.ANNO AS CDP,PF_VOCI.CODICE AS VOCE " _
                                                    & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                                    & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                                    & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                                    & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                                    & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "'"
                                                LettoreCodice = par.cmd.ExecuteReader
                                                Dim codiceVoce As String = ""
                                                If LettoreCodice.Read Then
                                                    codiceVoce = par.IfNull(LettoreCodice("VOCE"), 0)
                                                End If
                                                LettoreCodice.Close()
                                                If codiceVoce <> "" Then
                                                    txtNote.Text = txtNote.Text & "La voce indicata per la riga " & numeroRiga & " è errata. Pagamento non importato." & vbCrLf
                                                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Red
                                                    insert = 0
                                                    convalida = convalida + 1

                                                    Dim rigaErrori As Data.DataRow
                                                    rigaErrori = dtErrori.NewRow
                                                    rigaErrori.Item("#") = numeroRiga
                                                    rigaErrori.Item("N_FORN") = n_forn
                                                    rigaErrori.Item("IMPORTO") = importo
                                                    rigaErrori.Item("CDP") = CDP
                                                    rigaErrori.Item("ANNO_MAE") = annoMAE
                                                    rigaErrori.Item("MAE") = MAE
                                                    rigaErrori.Item("DATA_PAG") = data_pag
                                                    rigaErrori.Item("VOCE") = codiceVoce

                                                    dtErrori.Rows.Add(rigaErrori)

                                                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Voce errata, sostituire '" & voce & "' con '" & codiceVoce & "'"
                                                    Else
                                                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Voce errata, sostituire '" & voce & "' con '" & codiceVoce & "'"
                                                    End If
                                                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                                                Else
                                                    erroreGenerico = True
                                                End If
                                            Else
                                                erroreGenerico = True
                                            End If

                                        End If
                                    End If
                                    LettoreDati2.Close()
                                Else

                                    'CONTROLLO FORNITORE SBAGLIATO
                                    par.cmd.CommandText = "SELECT COD_FORNITORE FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI WHERE PROGR='" & PROGCDP & "' AND FORNITORI.ID=PAGAMENTI.ID_FORNITORE"
                                    Dim LettoreFornitore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    Dim codice_fornitore As String = ""
                                    If LettoreFornitore.Read Then
                                        codice_fornitore = par.IfNull(LettoreFornitore(0), "")
                                    End If
                                    LettoreFornitore.Close()
                                    If codice_fornitore <> n_forn Then
                                        'CONTROLLO CHE IL FORNITORE VERIFICHI LA COERENZA, ALTRIMENTI CI SONO PIù DATI NON COERENTI
                                        par.cmd.CommandText = "SELECT COUNT(*) " _
                                            & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                            & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                            & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                            & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                            & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & codice_fornitore & "' AND CODICE='" & voce & "'"
                                        Dim ControlloFornitore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        Dim ConteggioFornitore As Integer = 0
                                        If ControlloFornitore.Read Then
                                            ConteggioFornitore = par.IfNull(ControlloFornitore(0), 0)
                                        End If
                                        ControlloFornitore.Close()
                                        If ConteggioFornitore = 1 Then
                                            txtNote.Text = txtNote.Text & "Il fornitore indicato per la riga " & numeroRiga & " è errato. Pagamento non importato." & vbCrLf
                                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Red
                                            insert = 0
                                            convalida = convalida + 1

                                            Dim rigaErrori As Data.DataRow
                                            rigaErrori = dtErrori.NewRow
                                            rigaErrori.Item("#") = numeroRiga
                                            rigaErrori.Item("N_FORN") = codice_fornitore
                                            rigaErrori.Item("IMPORTO") = importo
                                            rigaErrori.Item("CDP") = CDP
                                            rigaErrori.Item("ANNO_MAE") = annoMAE
                                            rigaErrori.Item("MAE") = MAE
                                            rigaErrori.Item("DATA_PAG") = data_pag
                                            rigaErrori.Item("VOCE") = voce

                                            dtErrori.Rows.Add(rigaErrori)

                                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Fornitore errato, sostituire '" & n_forn & "' con '" & codice_fornitore & "'"
                                            Else
                                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Fornitore errato, sostituire '" & n_forn & "' con '" & codice_fornitore & "'"
                                            End If
                                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                                        Else
                                            erroreGenerico = True
                                        End If
                                    Else
                                        'CONTROLLARE LA VOCE SBAGLIATA
                                        par.cmd.CommandText = "SELECT COUNT(*) " _
                                            & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                            & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                            & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                            & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                            & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "'"
                                        Dim LettoreCodice As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        Dim codici As Integer = 0
                                        If LettoreCodice.Read Then
                                            codici = par.IfNull(LettoreCodice(0), 0)
                                        End If
                                        LettoreCodice.Close()
                                        If codici = 1 Then
                                            par.cmd.CommandText = "SELECT DISTINCT COD_FORNITORE,RAGIONE_SOCIALE,PROGR||'/'||PAGAMENTI.ANNO AS CDP,PF_VOCI.CODICE AS VOCE " _
                                                & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                                & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                                & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                                & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "'"
                                            LettoreCodice = par.cmd.ExecuteReader
                                            Dim codiceVoce As String = ""
                                            If LettoreCodice.Read Then
                                                codiceVoce = par.IfNull(LettoreCodice("VOCE"), 0)
                                            End If
                                            LettoreCodice.Close()
                                            If codiceVoce <> "" Then
                                                txtNote.Text = txtNote.Text & "La voce indicata per la riga " & numeroRiga & " è errata. Pagamento non importato." & vbCrLf
                                                DataGridExcel.Items(indice).BackColor = Drawing.Color.Red
                                                insert = 0
                                                convalida = convalida + 1

                                                Dim rigaErrori As Data.DataRow
                                                rigaErrori = dtErrori.NewRow
                                                rigaErrori.Item("#") = numeroRiga
                                                rigaErrori.Item("N_FORN") = n_forn
                                                rigaErrori.Item("IMPORTO") = importo
                                                rigaErrori.Item("CDP") = CDP
                                                rigaErrori.Item("ANNO_MAE") = annoMAE
                                                rigaErrori.Item("MAE") = MAE
                                                rigaErrori.Item("DATA_PAG") = data_pag
                                                rigaErrori.Item("VOCE") = codiceVoce

                                                dtErrori.Rows.Add(rigaErrori)

                                                If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Voce errata, sostituire '" & voce & "' con '" & codiceVoce & "'"
                                                Else
                                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Voce errata, sostituire '" & voce & "' con '" & codiceVoce & "'"
                                                End If
                                                DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                                            Else
                                                erroreGenerico = True
                                            End If
                                        Else
                                            erroreGenerico = True
                                        End If

                                    End If
                                End If
                            Catch ex As Exception
                                erroreGenerico = True
                            End Try

                            '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

                            If erroreGenerico = True Then
                                txtNote.Text = txtNote.Text & "C.D.P., Voce, Fornitore errati alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                                DataGridExcel.Items(indice).BackColor = Drawing.Color.Red
                                insert = 0
                                If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Errore generico"
                                Else
                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Errore generico"
                                End If
                                DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                            End If
                        End If
                        LettoreDati.Close()
                    Catch ex As Exception
                        txtNote.Text = txtNote.Text & "C.D.P., Voce, Fornitore errati alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Red
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Errore generico"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Errore generico"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End Try
                End If

                If insert <> 0 Then
                    '6) CONTROLLO SE PAGAMENTO GIà INSERITO
                    If IDPagamento = 0 Then
                        'ID PAGAMENTO NON VALIDO
                        txtNote.Text = txtNote.Text & "Pagamento relativo alla riga " & numeroRiga & " non valido. Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                    Else
                        If Trim(data_pag) = "" Then
                            par.cmd.CommandText = "SELECT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE ID_PAGAMENTO_RIT_LEGGE='" & IDPagamento & "' AND NUM_MANDATO='" & MAE & "' AND ANNO_MANDATO='" & annoMAE & "' AND DATA_MANDATO IS NULL AND IMPORTO='" & importo & "' AND ID_VOCE_PF='" & ID_VocePF & "'"
                        Else
                            par.cmd.CommandText = "SELECT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI " _
                                & "WHERE ID_PAGAMENTO_RIT_LEGGE='" & IDPagamento & "' " _
                                & "AND NUM_MANDATO='" & MAE & "' " _
                                & "AND ANNO_MANDATO='" & annoMAE & "' " _
                                & "AND DATA_MANDATO='" & data_pag & "' " _
                                & "AND IMPORTO='" & importo & "' " _
                                & "AND ID_VOCE_PF='" & ID_VocePF & "'"
                        End If
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReader.Read Then
                            'IL PAGAMENTO è GIà STATO INSERITO, SEGNALARE ANOMALIA, NON PROCEDERE CON L'IMPORTAZIONE
                            insert = 0
                            txtNote.Text = txtNote.Text & "Pagamento alla riga " & numeroRiga & " già importato. Pagamento non importato." & vbCrLf
                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            insert = 0
                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Pagamento già importato"
                            Else
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Pagamento già importato"
                            End If
                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                        Else
                            Try
                                'CONTROLLO IMPORTO CONSUNTIVATO
                                'SOMMO L'AMMONTARE MOMENTANEO DEI PAGAMENTI GIà LIQUIDATI
                                Dim importoParziale As Decimal = 0
                                'par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE ID_PAGAMENTO_RIT_LEGGE='" & IDPagamento & "'"
                                par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE ID_PAGAMENTO_RIT_LEGGE='" & IDPagamento & "' AND ID_VOCE_PF=" & ID_VocePF
                                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                                If myReader2.Read Then
                                    importoParziale = par.IfNull(myReader2(0), 0)
                                End If
                                myReader2.Close()

                                If importoParziale + importo <= importoConsuntivato + tolleranza Then

                                    'INSERISCO IL PAGAMENTO
                                    totaleRigheImportate = totaleRigheImportate + 1
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI(ID,ID_PAGAMENTO_RIT_LEGGE,NUM_MANDATO,ANNO_MANDATO,DATA_MANDATO,IMPORTO,ID_VOCE_PF) VALUES (SISCOM_MI.SEQ_PAGAMENTI_RIT_LIQUIDATI.NEXTVAL,'" & IDPagamento & "','" & MAE & "','" & annoMAE & "','" & data_pag & "','" & importo & "','" & ID_VocePF & "')"
                                    par.cmd.ExecuteNonQuery()

                                Else
                                    'IMPORTO CONSUNTIVATO SUPERATO
                                    'SEGNALARE ANOMALIA
                                    txtNote.Text = txtNote.Text & "Importo consuntivato superato di € " & Format(importoParziale + importo - importoConsuntivato, "##,##0.00") & " alla riga " & numeroRiga & " relativa al C.D.P. " & CDP & ". Pagamento non importato." & vbCrLf
                                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                                End If

                            Catch ex As Exception
                                'ERRORE NELL'INSERIMENTO DEL PAGAMENTO
                                totaleRigheImportate = totaleRigheImportate - 1
                                txtNote.Text = txtNote.Text & "Errore nell'inserimento del pagamento relativo alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                                DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            End Try
                        End If
                        myReader.Close()
                    End If
                End If

                '#################################################################
                indice = indice + 1
            Next
            txtNote.Text = txtNote.Text & "Totale righe importate: " & totaleRigheImportate & "." & vbCrLf
            NumeroRigheImportate.Value = totaleRigheImportate
            Session.Add("dt_ERRORI", dtErrori)


            '#################################################################
            'UPDATE IMPORT PAGAMENTI LIQUIDATI
            par.cmd.CommandText = " UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_RIT_LIQUIDATO = NVL (RIT_LEGGE_IVATA, 0) " _
                & " WHERE ID_PAGAMENTO_RIT_LEGGE IN (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE ABS((SELECT SUM (IMPORTO) FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID) -IMPORTO_CONSUNTIVATO)<1.5) " _
                & " AND IMPORTO_RIT_LIQUIDATO IS NULL "
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE  SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI SET " _
                & " ID_STRUTTURA=(SELECT DISTINCT ID_STRUTTURA FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI_RIT_LIQUIDATI.ID_PAGAMENTO_RIT_LEGGE AND PRENOTAZIONI.ID_VOCE_PF=PAGAMENTI_RIT_LIQUIDATI.ID_VOCE_PF) " _
                & " WHERE ID_STRUTTURA IS NULL "
            par.cmd.ExecuteNonQuery()
            '#################################################################
            par.cmd.Dispose()
            chiudiConnessione()
            postImportazione()
        Else
            DataGridExcel.Visible = False
            lblTitolo.Text = "Elenco Contenuto file Excel non disponibile"
            txtNote.Text = txtNote.Text & "Errore durante l'importazione." & vbCrLf
        End If
        txtNote.Text = txtNote.Text & vbCrLf & "************************************************************************" & vbCrLf
    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        If Not IsNothing(Session.Item("dt_P")) Then
            Session.Remove("dt_P")
        End If
        If Not IsNothing(Session.Item("LAVORAZIONE")) Then
            Session.Remove("LAVORAZIONE")
        End If
        Response.Redirect("../../pagina_home.aspx")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        preElaborazione()
    End Sub

    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                    TrovaIndiceColonna = Indice
                    Exit For
                End If
                Indice = Indice + 1
            Next
        Catch ex As Exception

        End Try
        Return TrovaIndiceColonna
    End Function

    Protected Sub btnConvalida_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConvalida.Click
        Try
            Dim DTeRR As Data.DataTable
            '##### RIPRENDO DATATABLE #####
            DTeRR = CType(HttpContext.Current.Session.Item("dt_ERRORI"), Data.DataTable)
            If Not IsNothing(Session.Item("dt_ERRORI")) Then
                Session.Remove("dt_ERRORI")
            End If
            '#################

            If DTeRR.Rows.Count > 0 Then
                Session.Item("dt_P") = DTeRR
                elaboraConvalida()
            End If

        Catch ex As Exception
            Response.Write("<script>alert('Si è verificato un errore durante la correzione degli errori.');location.replace('../../pagina_home.aspx');</script>")
        End Try
        btnConvalida.Visible = False
    End Sub

    Private Function ConvertiData(ByVal dataIn As String) As String
        Dim dataOut As String = ""
        If Len(dataIn) = 8 Then
            Return Right(dataIn, 2) & "/" & Mid(dataIn, 5, 2) & "/" & Left(dataIn, 4)
        Else
            Return ""
        End If
    End Function

    Protected Sub elaboraConvalida()
        '##### RIPRENDO DATATABLE #####
        dt = CType(HttpContext.Current.Session.Item("dt_P"), Data.DataTable)
        If Not IsNothing(Session.Item("dt_P")) Then
            Session.Remove("dt_P")
        End If
        '#################

        'tolleranza di un euro nel totale consuntivato dei pagamenti
        Dim tolleranza As Decimal = 1
        Dim CDP As String = ""
        Dim ANNOCDP As String = ""
        Dim PROGCDP As String = ""
        Dim numeroRiga As Integer = 0
        Dim importo As String = ""
        Dim n_forn As String = ""
        Dim voce As String = ""
        Dim fornitore As String = ""
        Dim annoMAE As String = ""
        Dim MAE As String = ""
        Dim data_pag As String = ""
        Dim IDPagamento As Long = 0
        Dim totaleRigheImportate As Integer = 0
        Dim ANNOPF As Integer = 0
        Dim ANNO_ID_PF As Integer = 0

        txtNote.Text = "************************** IMPORTAZIONE **************************" & vbCrLf & vbCrLf


        If Not IsNothing(dt) Then
            apriConnessione()
            Dim indice As Integer = 0
            For Each riga In dt.Rows
                Dim insert As Integer = 1

                '###### RECUPERO DATI RIGA #######
                numeroRiga = riga("#")
                indice = numeroRiga - 1
                DataGridExcel.Items(numeroRiga - 1).BackColor = Drawing.Color.Gainsboro

                n_forn = riga("N_FORN")
                'Fornitore = riga("FORNITORE")
                importo = riga("IMPORTO")
                annoMAE = riga("ANNO_MAE")
                MAE = riga("MAE")
                data_pag = riga("DATA_PAG")
                'SICCOME LA DATA è NEL FORMATO DI INSERIMENTO NEL DB, FACCIO LA CONVERSIONE CHE SERVE AI CONTROLLI
                data_pag = Right(data_pag, 2) & "/" & Mid(data_pag, 5, 2) & "/" & Left(data_pag, 4)

                voce = riga("VOCE")

                '######## VERIFICA DI CONTROLLO DELLA RIGA ######
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '1) VERIFICA CDP
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                CDP = Trim(riga("CDP"))
                If Len(CDP) < 6 Then
                    'CDP NON VALIDO
                    'SEGNALARE ANOMALIA
                    txtNote.Text = txtNote.Text & "C.D.P. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "C.D.P non valido"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) & "<br />C.D.P non valido"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    insert = 0
                Else
                    Try

                        ANNOCDP = Right(CDP, 4)
                        If Not IsNumeric(CLng(ANNOCDP)) Then

                            'ANNO CDP NON VALIDO
                            'SEGNALARE ANOMALIA
                            txtNote.Text = txtNote.Text & "Anno C.D.P. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            insert = 0
                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Anno C.D.P non valido"
                            Else
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Anno C.D.P non valido"
                            End If
                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                        End If

                    Catch ex As Exception

                        'ANNO CDP NON VALIDO
                        'SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Anno C.D.P. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Anno C.D.P non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Anno C.D.P non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True

                    End Try

                    Try

                        PROGCDP = Left(CDP, Len(CDP) - 5)
                        If Not IsNumeric(CLng(PROGCDP)) Then

                            'PROGCDP NON VALIDO
                            'SEGNALARE ANOMALIA
                            txtNote.Text = txtNote.Text & "Progr. C.D.P. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            insert = 0
                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Progr. C.D.P non valido"
                            Else
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Progr. C.D.P non valido"
                            End If
                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True

                        End If

                    Catch ex As Exception

                        'PROGCDP NON VALIDO
                        'SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Progr. C.D.P. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Progr. C.D.P non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Progr. C.D.P non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End Try
                End If


                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '2) VERIFICA ANNO MAE, MAE, DATA E IMPORTO
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

                'IMPORTO
                Try
                    If Not IsNumeric(CDec(importo)) Then
                        'IMPORTO NON VALIDO, SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Importo non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Importo non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Importo non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    Else
                        'SE PRESENTI, ELIMINARE I PUNTI
                        importo = Replace(importo, ".", "")
                    End If
                Catch ex As Exception
                    'IMPORTO NON VALIDO, SEGNALARE ANOMALIA
                    txtNote.Text = txtNote.Text & "Importo non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    insert = 0
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Importo non valido"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Importo non valido"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                End Try


                'ANNO MAE
                Try
                    If Len(Trim(annoMAE)) = 4 Then
                        If Not IsNumeric(CLng(annoMAE)) Then
                            'ANNO MAE NON VALIDO, SEGNALARE ANOMALIA
                            txtNote.Text = txtNote.Text & "Anno M.A.E. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            insert = 0
                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Anno M.A.E non valido"
                            Else
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Anno M.A.E non valido"
                            End If
                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                        End If
                    Else
                        'ANNO MAE NON VALIDO, SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Anno M.A.E. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Anno M.A.E non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Anno M.A.E non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End If
                Catch ex As Exception
                    'ANNO MAE NON VALIDO, SEGNALARE ANOMALIA
                    txtNote.Text = txtNote.Text & "Anno M.A.E. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    insert = 0
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Anno M.A.E non valido"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Anno M.A.E non valido"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                End Try

                'MAE
                Try
                    If Not IsNumeric(CLng(MAE)) Then
                        'MAE NON VALIDO, SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "M.A.E non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "M.A.E non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />M.A.E non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End If
                Catch ex As Exception
                    'MAE NON VALIDO, SEGNALARE ANOMALIA
                    txtNote.Text = txtNote.Text & "M.A.E. non valido alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    insert = 0
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "M.A.E non valido"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />M.A.E non valido"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                End Try


                'CONTROLLO DATA
                If Len(data_pag) = 0 Then
                    'NON VIENE INSERITA LA DATA, MA NON PUò ESSERE VUOTA
                    txtNote.Text = txtNote.Text & "Data non presente alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    insert = 0
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Data assente"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Data assente"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                Else
                    If Not IsDate(data_pag) Then
                        'DATA NON VALIDA
                        txtNote.Text = txtNote.Text & "Data non valida alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Data non valida"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Data non valida"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    Else
                        'CONVERTO LA DATA NEL FORMATO DI INSERIMENTO
                        data_pag = Right(data_pag, 4) & Mid(data_pag, 4, 2) & Left(data_pag, 2)
                    End If
                End If

                '3) CONTROLLO SE ESISTE LA VOCE
                'RECUPERO L'ID DELLA VOCE IN QUESTIONE
                Dim ID_VocePF As String = ""
                Try
                    par.cmd.CommandText = "SELECT DISTINCT PF_VOCI.ID AS ID_VOCE,PF_MAIN.ID AS ID_PF, TRIM(SUBSTR(T_ESERCIZIO_FINANZIARIO.INIZIO,1,4)) AS ANNO " _
                           & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                           & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                           & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                           & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                           & "AND PF_VOCI.ID_PIANO_FINANZIARIO=PF_MAIN.ID " _
                           & "AND T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                           & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "'"

                    Dim lettoreVoce As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreVoce.Read Then
                        ID_VocePF = par.IfNull(lettoreVoce("ID_VOCE"), 0)
                        ANNOPF = par.IfNull(lettoreVoce("ANNO"), 0)
                        ANNO_ID_PF = par.IfNull(lettoreVoce("ID_PF"), 0)
                    End If
                    lettoreVoce.Close()

                    If ID_VocePF = "" Then
                        'NESSUNA VOCE RILEVATA, SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Voce non rilevata nel piano finanziario alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Voce non valida"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Voce non valida"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End If
                Catch ex As Exception
                    'NESSUNA VOCE RILEVATA, SEGNALARE ANOMALIA
                    txtNote.Text = txtNote.Text & "Voce non rilevata nel piano finanziario alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                    insert = 0
                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Voce non valida"
                    Else
                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Voce non valida"
                    End If
                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                End Try

                Dim importoConsuntivato As Decimal = 0
                If insert <> 0 Then
                    '4) RILEVO ID PAGAMENTO
                    Try
                        par.cmd.CommandText = "SELECT ID,ROUND(IMPORTO_CONSUNTIVATO,2) FROM SISCOM_MI.PAGAMENTI WHERE ANNO='" & ANNOCDP & "' AND PROGR='" & PROGCDP & "'"
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReader.Read Then
                            IDPagamento = myReader(0)
                            importoConsuntivato = CDec(par.IfNull(myReader(1), 0))
                        Else
                            'NESSUN ID RILEVATO, SEGNALARE ANOMALIA
                            txtNote.Text = txtNote.Text & "Nessun pagamento rilevato per la riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            insert = 0
                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Pagamento non valido"
                            Else
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Pagamento non valido"
                            End If
                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                        End If
                        myReader.Close()
                    Catch ex As Exception
                        'NESSUN ID RILEVATO, SEGNALARE ANOMALIA
                        txtNote.Text = txtNote.Text & "Nessun pagamento rilevato per la riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Pagamento non valido"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Pagamento non valido"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End Try
                End If

                If insert <> 0 Then
                    '5)CONTROLLO LA COERENZA DEI DATI
                    Try
                        Dim erroreGenerico As Boolean = False
                        par.cmd.CommandText = "SELECT DISTINCT COD_FORNITORE,RAGIONE_SOCIALE,PROGR||'/'||PAGAMENTI.ANNO AS CDP,PF_VOCI.CODICE AS VOCE " _
                        & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                        & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                        & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                        & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                        & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "' AND CODICE='" & voce & "'"
                        Dim LettoreDati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If LettoreDati.Read Then
                            Dim CDP_C As String = par.IfNull(LettoreDati("CDP"), "")
                            Dim COD_C As String = par.IfNull(LettoreDati("COD_FORNITORE"), "")
                            Dim VOCE_C As String = par.IfNull(LettoreDati("VOCE"), "")
                            If CDP_C <> CDP Or COD_C <> n_forn Or VOCE_C <> voce Then
                                erroreGenerico = True
                            End If
                        Else
                            '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                            'CONTROLLO VOCE/SOTTOVOCE
                            '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
                            Try
                                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IN (SELECT PF_VOCI.ID FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN WHERE CODICE='" & voce & "' AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO AND ID_PIANO_FINANZIARIO='" & ANNO_ID_PF & "' AND ID_STATO>=5)"
                                Dim LettoreCount As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                Dim conteggio As Integer = 0
                                If LettoreCount.Read Then
                                    conteggio = par.IfNull(LettoreCount(0), 0)
                                End If
                                LettoreCount.Close()
                                Dim codice_voce As String = ""
                                Dim id_voce As Integer = -1
                                If conteggio = 1 Then
                                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IN (SELECT PF_VOCI.ID FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN WHERE CODICE='" & voce & "' AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO AND ID_PIANO_FINANZIARIO='" & ANNO_ID_PF & "' AND ID_STATO>=5)"
                                    Dim lettoreVoce As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If lettoreVoce.Read Then
                                        codice_voce = par.IfNull(lettoreVoce("CODICE"), "")
                                        id_voce = par.IfNull(lettoreVoce("ID"), -1)
                                    End If
                                    lettoreVoce.Close()
                                End If
                                If codice_voce <> "" Then
                                    'SE LA VOCE INDICATA NEL FILE HA UNA SOLA SOTTOVOCE E QUESTA VERIFICA LA COERENZA ALLORA IMPORTIAMO LA RIGA
                                    par.cmd.CommandText = "SELECT DISTINCT COD_FORNITORE,RAGIONE_SOCIALE,PROGR||'/'||PAGAMENTI.ANNO AS CDP,PF_VOCI.CODICE AS VOCE " _
                                        & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                        & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                        & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                        & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                        & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "' AND CODICE='" & codice_voce & "'"
                                    Dim LettoreDati2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If LettoreDati2.Read Then
                                        Dim CDP_C As String = par.IfNull(LettoreDati2("CDP"), "")
                                        Dim COD_C As String = par.IfNull(LettoreDati2("COD_FORNITORE"), "")
                                        Dim VOCE_C As String = par.IfNull(LettoreDati2("VOCE"), "")
                                        If CDP_C <> CDP Or COD_C <> n_forn Or VOCE_C <> codice_voce Then
                                            erroreGenerico = True
                                        Else
                                            'COERENZA VERIFICATA,MODIFICO VOCEPF
                                            ID_VocePF = id_voce
                                            txtNote.Text = txtNote.Text & "Voce modificata da '" & voce & "' a '" & codice_voce & "' alla riga " & numeroRiga & ". " & vbCrLf
                                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Gainsboro
                                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "VOCE")).Text = codice_voce
                                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Voce modificata da '" & voce & "' a '" & codice_voce & "'"
                                            Else
                                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Voce modificata da '" & voce & "' a '" & codice_voce & "'"
                                            End If
                                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                                        End If
                                    Else
                                        'ALTRI TIPI DI CONTROLLO

                                        'CONTROLLO FORNITORE SBAGLIATO
                                        par.cmd.CommandText = "SELECT COD_FORNITORE FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI WHERE PROGR='" & PROGCDP & "' AND FORNITORI.ID=PAGAMENTI.ID_FORNITORE"
                                        Dim LettoreFornitore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        Dim codice_fornitore As String = ""
                                        If LettoreFornitore.Read Then
                                            codice_fornitore = par.IfNull(LettoreFornitore(0), "")
                                        End If
                                        LettoreFornitore.Close()
                                        If codice_fornitore <> n_forn Then
                                            'CONTROLLO CHE IL FORNITORE VERIFICHI LA COERENZA, ALTRIMENTI CI SONO PIù DATI NON COERENTI
                                            par.cmd.CommandText = "SELECT COUNT(*) " _
                                                & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                                & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                                & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                                & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & codice_fornitore & "' AND CODICE='" & voce & "'"
                                            Dim ControlloFornitore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                            Dim ConteggioFornitore As Integer = 0
                                            If ControlloFornitore.Read Then
                                                ConteggioFornitore = par.IfNull(ControlloFornitore(0), 0)
                                            End If
                                            ControlloFornitore.Close()
                                            If ConteggioFornitore = 1 Then
                                                txtNote.Text = txtNote.Text & "Il fornitore indicato per la riga " & numeroRiga & " è errato. Pagamento non importato." & vbCrLf
                                                DataGridExcel.Items(indice).BackColor = Drawing.Color.Gainsboro
                                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "N_FORN")).Text = codice_fornitore
                                                insert = 0
                                                convalida = convalida + 1

                                                If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Fornitore errato, sostituire '" & n_forn & "' con '" & codice_fornitore & "'"
                                                Else
                                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Fornitore errato, sostituire '" & n_forn & "' con '" & codice_fornitore & "'"
                                                End If
                                                DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                                            Else
                                                erroreGenerico = True
                                            End If
                                        Else
                                            'CONTROLLARE LA VOCE SBAGLIATA
                                            par.cmd.CommandText = "SELECT COUNT(*) " _
                                                & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                                & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                                & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                                & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "'"
                                            Dim LettoreCodice As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                            Dim codici As Integer = 0
                                            If LettoreCodice.Read Then
                                                codici = par.IfNull(LettoreCodice(0), 0)
                                            End If
                                            LettoreCodice.Close()
                                            If codici = 1 Then
                                                par.cmd.CommandText = "SELECT DISTINCT COD_FORNITORE,RAGIONE_SOCIALE,PROGR||'/'||PAGAMENTI.ANNO AS CDP,PF_VOCI.CODICE AS VOCE " _
                                                    & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                                    & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                                    & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                                    & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                                    & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "'"
                                                LettoreCodice = par.cmd.ExecuteReader
                                                Dim codiceVoce As String = ""
                                                If LettoreCodice.Read Then
                                                    codiceVoce = par.IfNull(LettoreCodice("VOCE"), 0)
                                                End If
                                                LettoreCodice.Close()
                                                If codiceVoce <> "" Then
                                                    txtNote.Text = txtNote.Text & "La voce indicata per la riga " & numeroRiga & " è errata. Pagamento non importato." & vbCrLf
                                                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Gainsboro
                                                    insert = 0
                                                    convalida = convalida + 1
                                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "VOCE")).Text = codiceVoce
                                                    If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Voce errata, sostituire '" & voce & "' con '" & codiceVoce & "'"
                                                    Else
                                                        DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Voce errata, sostituire '" & voce & "' con '" & codiceVoce & "'"
                                                    End If
                                                    DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                                                Else
                                                    erroreGenerico = True
                                                End If
                                            Else
                                                erroreGenerico = True
                                            End If

                                        End If
                                    End If
                                    LettoreDati2.Close()
                                Else

                                    'CONTROLLO FORNITORE SBAGLIATO
                                    par.cmd.CommandText = "SELECT COD_FORNITORE FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI WHERE PROGR='" & PROGCDP & "' AND FORNITORI.ID=PAGAMENTI.ID_FORNITORE"
                                    Dim LettoreFornitore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    Dim codice_fornitore As String = ""
                                    If LettoreFornitore.Read Then
                                        codice_fornitore = par.IfNull(LettoreFornitore(0), "")
                                    End If
                                    LettoreFornitore.Close()
                                    If codice_fornitore <> n_forn Then
                                        'CONTROLLO CHE IL FORNITORE VERIFICHI LA COERENZA, ALTRIMENTI CI SONO PIù DATI NON COERENTI
                                        par.cmd.CommandText = "SELECT COUNT(*) " _
                                            & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                            & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                            & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                            & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                            & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & codice_fornitore & "' AND CODICE='" & voce & "'"
                                        Dim ControlloFornitore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        Dim ConteggioFornitore As Integer = 0
                                        If ControlloFornitore.Read Then
                                            ConteggioFornitore = par.IfNull(ControlloFornitore(0), 0)
                                        End If
                                        ControlloFornitore.Close()
                                        If ConteggioFornitore = 1 Then
                                            txtNote.Text = txtNote.Text & "Il fornitore indicato per la riga " & numeroRiga & " è errato. Pagamento non importato." & vbCrLf
                                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Gainsboro
                                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "N_FORN")).Text = codice_fornitore
                                            insert = 0
                                            convalida = convalida + 1
                                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Fornitore errato, sostituire '" & n_forn & "' con '" & codice_fornitore & "'"
                                            Else
                                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Fornitore errato, sostituire '" & n_forn & "' con '" & codice_fornitore & "'"
                                            End If
                                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                                        Else
                                            erroreGenerico = True
                                        End If
                                    Else
                                        'CONTROLLARE LA VOCE SBAGLIATA
                                        par.cmd.CommandText = "SELECT COUNT(*) " _
                                            & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                            & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                            & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                            & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                            & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "'"
                                        Dim LettoreCodice As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        Dim codici As Integer = 0
                                        If LettoreCodice.Read Then
                                            codici = par.IfNull(LettoreCodice(0), 0)
                                        End If
                                        LettoreCodice.Close()
                                        If codici = 1 Then
                                            par.cmd.CommandText = "SELECT DISTINCT COD_FORNITORE,RAGIONE_SOCIALE,PROGR||'/'||PAGAMENTI.ANNO AS CDP,PF_VOCI.CODICE AS VOCE " _
                                                & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                                & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                                & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                                & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "'"
                                            LettoreCodice = par.cmd.ExecuteReader
                                            Dim codiceVoce As String = ""
                                            If LettoreCodice.Read Then
                                                codiceVoce = par.IfNull(LettoreCodice("VOCE"), 0)
                                            End If
                                            LettoreCodice.Close()
                                            If codiceVoce <> "" Then
                                                txtNote.Text = txtNote.Text & "La voce indicata per la riga " & numeroRiga & " è errata. Pagamento non importato." & vbCrLf
                                                DataGridExcel.Items(indice).BackColor = Drawing.Color.Gainsboro
                                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "VOCE")).Text = codiceVoce
                                                insert = 0
                                                convalida = convalida + 1
                                                If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Voce errata, sostituire '" & voce & "' con '" & codiceVoce & "'"
                                                Else
                                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Voce errata, sostituire '" & voce & "' con '" & codiceVoce & "'"
                                                End If
                                                DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                                            Else
                                                erroreGenerico = True
                                            End If
                                        Else
                                            erroreGenerico = True
                                        End If

                                    End If
                                End If
                            Catch ex As Exception
                                erroreGenerico = True
                            End Try

                            '°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

                            If erroreGenerico = True Then
                                txtNote.Text = txtNote.Text & "C.D.P., Voce, Fornitore errati alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                                DataGridExcel.Items(indice).BackColor = Drawing.Color.Red
                                insert = 0
                                If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Errore generico"
                                Else
                                    DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Errore generico"
                                End If
                                DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                            End If
                        End If
                        LettoreDati.Close()
                    Catch ex As Exception
                        txtNote.Text = txtNote.Text & "C.D.P., Voce, Fornitore errati alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Red
                        insert = 0
                        If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Errore generico"
                        Else
                            DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text & "<br />Errore generico"
                        End If
                        DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                    End Try
                End If

                If insert <> 0 Then
                    '6) CONTROLLO SE PAGAMENTO GIà INSERITO
                    If IDPagamento = 0 Then
                        'ID PAGAMENTO NON VALIDO
                        txtNote.Text = txtNote.Text & "Pagamento relativo alla riga " & numeroRiga & " non valido. Pagamento non importato." & vbCrLf
                        DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                        insert = 0
                    Else
                        If Trim(data_pag) = "" Then
                            par.cmd.CommandText = "SELECT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE ID_PAGAMENTO_RIT_LEGGE='" & IDPagamento & "' AND NUM_MANDATO='" & MAE & "' AND ANNO_MANDATO='" & annoMAE & "' AND DATA_MANDATO IS NULL AND IMPORTO='" & importo & "' AND ID_VOCE_PF='" & ID_VocePF & "'"
                        Else
                            par.cmd.CommandText = "SELECT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI " _
                                & "WHERE ID_PAGAMENTO_RIT_LEGGE='" & IDPagamento & "' " _
                                & "AND NUM_MANDATO='" & MAE & "' " _
                                & "AND ANNO_MANDATO='" & annoMAE & "' " _
                                & "AND DATA_MANDATO='" & data_pag & "' " _
                                & "AND IMPORTO='" & importo & "' " _
                                & "AND ID_VOCE_PF='" & ID_VocePF & "'"
                        End If
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myReader.Read Then
                            'IL PAGAMENTO è GIà STATO INSERITO, SEGNALARE ANOMALIA, NON PROCEDERE CON L'IMPORTAZIONE
                            insert = 0
                            txtNote.Text = txtNote.Text & "Pagamento alla riga " & numeroRiga & " già importato. Pagamento non importato." & vbCrLf
                            DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            insert = 0
                            If Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "" Or Trim(DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text) = "&nbsp;" Then
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Pagamento già importato"
                            Else
                                DataGridExcel.Items(indice).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = "Pagamento già importato"
                            End If
                            DataGridExcel.Columns(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Visible = True
                        Else
                            Try
                                'CONTROLLO IMPORTO CONSUNTIVATO
                                'SOMMO L'AMMONTARE MOMENTANEO DEI PAGAMENTI GIà LIQUIDATI
                                Dim importoParziale As Decimal = 0
                                par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE ID_PAGAMENTO_RIT_LEGGE='" & IDPagamento & "'"
                                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                                If myReader2.Read Then
                                    importoParziale = par.IfNull(myReader2(0), 0)
                                End If
                                myReader2.Close()

                                If importoParziale + importo <= importoConsuntivato + tolleranza Then

                                    'INSERISCO IL PAGAMENTO
                                    totaleRigheImportate = totaleRigheImportate + 1
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI(ID,ID_PAGAMENTO_RIT_LEGGE,NUM_MANDATO,ANNO_MANDATO,DATA_MANDATO,IMPORTO,ID_VOCE_PF) VALUES (SISCOM_MI.SEQ_PAGAMENTI_RIT_LIQUIDATI.NEXTVAL,'" & IDPagamento & "','" & MAE & "','" & annoMAE & "','" & data_pag & "','" & importo & "','" & ID_VocePF & "')"
                                    par.cmd.ExecuteNonQuery()

                                    'MODIFICO LE CELLE RIGUARDANTI LE MODIFICHE APPORTATE AI PAGAMENTI
                                    par.cmd.CommandText = "SELECT * " _
                                               & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PAGAMENTI, SISCOM_MI.PF_VOCI, SISCOM_MI.FORNITORI " _
                                               & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
                                               & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                                               & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID " _
                                               & "AND PROGR='" & PROGCDP & "' AND PAGAMENTI.ANNO='" & ANNOCDP & "' AND COD_FORNITORE='" & n_forn & "' AND CODICE='" & voce & "'"
                                    Dim LettoreMandatoCompleto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If LettoreMandatoCompleto.Read Then
                                        DataGridExcel.Items(numeroRiga - 1).Cells(TrovaIndiceColonna(DataGridExcel, "N_FORN")).Text = par.IfNull(LettoreMandatoCompleto("COD_FORNITORE"), "")
                                        DataGridExcel.Items(numeroRiga - 1).Cells(TrovaIndiceColonna(DataGridExcel, "FORNITORE")).Text = par.IfNull(LettoreMandatoCompleto("RAGIONE_SOCIALE"), "")
                                        DataGridExcel.Items(numeroRiga - 1).Cells(TrovaIndiceColonna(DataGridExcel, "VOCE")).Text = par.IfNull(LettoreMandatoCompleto("CODICE"), "")
                                        DataGridExcel.Items(numeroRiga - 1).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = Replace(DataGridExcel.Items(numeroRiga - 1).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text, "errata, sostituire", "modificata da")
                                        DataGridExcel.Items(numeroRiga - 1).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = Replace(DataGridExcel.Items(numeroRiga - 1).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text, "errato, sostituire", "modificato da")
                                        DataGridExcel.Items(numeroRiga - 1).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text = Replace(DataGridExcel.Items(numeroRiga - 1).Cells(TrovaIndiceColonna(DataGridExcel, "ERRORE")).Text, "con", "a")
                                    End If
                                    LettoreMandatoCompleto.Close()
                                Else
                                    'IMPORTO CONSUNTIVATO SUPERATO
                                    'SEGNALARE ANOMALIA
                                    txtNote.Text = txtNote.Text & "Importo consuntivato superato di € " & Format(importoParziale + importo - importoConsuntivato, "##,##0.00") & " alla riga " & numeroRiga & " relativa al C.D.P. " & CDP & ". Pagamento non importato." & vbCrLf
                                    DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                                End If

                            Catch ex As Exception
                                'ERRORE NELL'INSERIMENTO DEL PAGAMENTO
                                totaleRigheImportate = totaleRigheImportate - 1
                                txtNote.Text = txtNote.Text & "Errore nell'inserimento del pagamento relativo alla riga " & numeroRiga & ". Pagamento non importato." & vbCrLf
                                DataGridExcel.Items(indice).BackColor = Drawing.Color.Yellow
                            End Try
                        End If
                        myReader.Close()
                    End If
                End If

                '#################################################################

            Next
            txtNote.Text = txtNote.Text & "Totale righe importate: " & totaleRigheImportate & "." & vbCrLf
            NumeroRigheImportate.Value = totaleRigheImportate

            '#################################################################
            'UPDATE IMPORT PAGAMENTI LIQUIDATI
            par.cmd.CommandText = " UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_RIT_LIQUIDATO = NVL (RIT_LEGGE_IVATA, 0) " _
                & " WHERE ID_PAGAMENTO_RIT_LEGGE IN (SELECT PAGAMENTI.ID FROM SISCOM_MI.PAGAMENTI WHERE ABS((SELECT SUM (IMPORTO) FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID) -IMPORTO_CONSUNTIVATO)<1.5) " _
                & " AND IMPORTO_RIT_LIQUIDATO IS NULL "
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE  SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI SET " _
                & " ID_STRUTTURA=(SELECT DISTINCT ID_STRUTTURA FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE=PAGAMENTI_RIT_LIQUIDATI.ID_PAGAMENTO_RIT_LEGGE AND PRENOTAZIONI.ID_VOCE_PF=PAGAMENTI_RIT_LIQUIDATI.ID_VOCE_PF) " _
                & " WHERE ID_STRUTTURA IS NULL "
            par.cmd.ExecuteNonQuery()
            '#################################################################


            par.cmd.Dispose()
            chiudiConnessione()
            postImportazione()
        Else
            DataGridExcel.Visible = False
            lblTitolo.Text = "Elenco Contenuto file Excel non disponibile"
            txtNote.Text = txtNote.Text & "Errore durante l'importazione." & vbCrLf
        End If
        txtNote.Text = txtNote.Text & vbCrLf & "************************************************************************" & vbCrLf
    End Sub

End Class