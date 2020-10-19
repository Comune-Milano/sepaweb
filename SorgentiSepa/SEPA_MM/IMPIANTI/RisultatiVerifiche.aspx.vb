'*** LISTA RISULTATO VERIFICHE IMPIANTI

Partial Class RisultatiVerifiche
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreImpianto As String

    Public sOrdinamento As String
    Public sVerifiche As String

    Dim dt As New Data.DataTable

    Dim lstVerificheRicerche As System.Collections.Generic.List(Of Epifani.VerificheRicerche)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Data1 As String
        Dim GiorniTrascorsi As Integer
        Dim GiorniPreAllarme As Integer
        Dim nVerifiche As Integer

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        lstVerificheRicerche = CType(HttpContext.Current.Session.Item("LSTVERIFICHE_RICERCHE"), System.Collections.Generic.List(Of Epifani.VerificheRicerche))


        If Not IsPostBack Then

            '*LBLID.Text = Request.QueryString("T")

            lstVerificheRicerche.Clear()

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")

            sOrdinamento = Request.QueryString("ORD")
            sVerifiche = Request.QueryString("VER")

            Select Case sOrdinamento
                Case "COMPLESSO"
                    sOrder = " order by DENOMINAZIONE_COMPLESSO"
                Case "EDIFICIO"
                    sOrder = " order by DENOMINAZIONE_EDIFICIO"
                Case "TIPO IMPIANTO"
                    sOrder = " order by TIPO_IMPIANTO"
                Case "DATA SCADENZA"
                    sOrder = " order by SCADENZA"
                Case Else
                    sOrder = ""
            End Select


            sStringaSql = "select SISCOM_MI.IMPIANTI.ID AS ""ID_IMPIANTO"", SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICHE""," _
                                & "SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO," _
                                & "SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DENOMINAZIONE_COMPLESSO""," _
                                & "SISCOM_MI.EDIFICI.DENOMINAZIONE AS ""DENOMINAZIONE_EDIFICIO""," _
                                & "SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS ""TIPO_IMPIANTO""," _
                                & "SISCOM_MI.IMPIANTI.DESCRIZIONE AS ""DENOMINAZIONE_IMPIANTO""," _
                                & "DECODE(SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,'PR','PRESSIONE RESIDUA','TP','PERIODICHE','TS','SCALE','PB','BIENNALI','ST','STRAORDINARIE') AS ""TIPO_VERIFICA"", " _
                                & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_VERIFICA"", " _
                                & "SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA AS ""VALIDITA"", " _
                                & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA""," _
                                & "SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.TIPOLOGIA_IMPIANTI.COD AS ""CODICE_IMPIANTO"" " _
                        & " from SISCOM_MI.IMPIANTI_VERIFICHE,SISCOM_MI.IMPIANTI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.EDIFICI ,SISCOM_MI.TIPOLOGIA_IMPIANTI " _
                        & " where     SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) and " _
                        & "     SISCOM_MI.IMPIANTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) and " _
                        & "     SISCOM_MI.IMPIANTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) and " _
                        & "     SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=SISCOM_MI.TIPOLOGIA_IMPIANTI.COD (+) and " _
                        & "     SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' "


            If sValoreComplesso <> "-1" Then
                sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & sValoreComplesso
            End If

            If sValoreEdificio <> "-1" Then
                sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & sValoreEdificio
            End If

            If sValoreImpianto <> "-1" Then
                sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.COD_TIPOLOGIA='" & sValoreImpianto & "' "
            End If

            sStringaSql = sStringaSql & sWhere & sOrder


            nVerifiche = 0

            ' FILTRO SCADENZE
            par.OracleConn.Open()
            Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

            While myReader1.Read

                Data1 = par.IfNull(myReader1("SCADENZA"), "")

                If Strings.Len(Data1) > 0 Then
                    GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(par.FormattaData(Data1)), CDate(Now.ToString("dd/MM/yyyy")))
                    GiorniPreAllarme = par.IfNull(myReader1("MESI_PREALLARME"), 1) * 30

                    Select Case sVerifiche
                        Case "SCADUTE"

                            If GiorniTrascorsi > 0 Then
                                nVerifiche = nVerifiche + 1

                                Dim gen As Epifani.VerificheRicerche
                                gen = New Epifani.VerificheRicerche(par.IfNull(myReader1("ID_IMPIANTO"), -1), par.IfNull(myReader1("ID_VERIFICHE"), -1), par.IfNull(myReader1("COD_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_EDIFICIO"), " "), par.IfNull(myReader1("TIPO_IMPIANTO"), " "), par.IfNull(myReader1("TIPO_VERIFICA"), " "), par.IfNull(myReader1("DATA_VERIFICA"), " "), par.IfNull(myReader1("VALIDITA"), 12), par.IfNull(myReader1("SCADENZA"), " "), par.IfNull(myReader1("CODICE_IMPIANTO"), " "))
                                lstVerificheRicerche.Add(gen)
                                gen = Nothing
                            End If

                        Case "IN SCADENZA"
                            If GiorniTrascorsi >= -GiorniPreAllarme And GiorniTrascorsi <= 0 Then
                                nVerifiche = nVerifiche + 1

                                Dim gen As Epifani.VerificheRicerche
                                gen = New Epifani.VerificheRicerche(par.IfNull(myReader1("ID_IMPIANTO"), -1), par.IfNull(myReader1("ID_VERIFICHE"), -1), par.IfNull(myReader1("COD_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_EDIFICIO"), " "), par.IfNull(myReader1("TIPO_IMPIANTO"), " "), par.IfNull(myReader1("TIPO_VERIFICA"), " "), par.IfNull(myReader1("DATA_VERIFICA"), " "), par.IfNull(myReader1("VALIDITA"), 12), par.IfNull(myReader1("SCADENZA"), " "), par.IfNull(myReader1("CODICE_IMPIANTO"), " "))
                                lstVerificheRicerche.Add(gen)
                                gen = Nothing
                            End If

                        Case "NON SCADUTE"
                            If GiorniTrascorsi < -GiorniPreAllarme And GiorniTrascorsi <= 0 Then
                                nVerifiche = nVerifiche + 1

                                Dim gen As Epifani.VerificheRicerche
                                gen = New Epifani.VerificheRicerche(par.IfNull(myReader1("ID_IMPIANTO"), -1), par.IfNull(myReader1("ID_VERIFICHE"), -1), par.IfNull(myReader1("COD_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_COMPLESSO"), " "), par.IfNull(myReader1("DENOMINAZIONE_EDIFICIO"), " "), par.IfNull(myReader1("TIPO_IMPIANTO"), " "), par.IfNull(myReader1("TIPO_VERIFICA"), " "), par.IfNull(myReader1("DATA_VERIFICA"), " "), par.IfNull(myReader1("VALIDITA"), 12), par.IfNull(myReader1("SCADENZA"), " "), par.IfNull(myReader1("CODICE_IMPIANTO"), " "))
                                lstVerificheRicerche.Add(gen)
                                gen = Nothing
                            End If

                    End Select
                End If
            End While
            myReader1.Close()

            DataGrid1.DataSource = Nothing
            DataGrid1.DataSource = lstVerificheRicerche
            DataGrid1.DataBind()

            Label1.Text = " " & nVerifiche

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            '' PROVE FATTE SENZA USARE LA CLASEE
            'Dim row As System.Data.DataRow
            'Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            'Dim dt2 As New Data.DataTable

            'par.cmd.CommandText = sStringaSql
            'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            'da.Fill(dt)

            '            par.OracleConn.Open()
            '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'lstVerificheRicerche.Add(gen)
            'gen = Nothing

            'row = dt.NewRow()
            'row.Item("ID_IMPIANTO") = par.IfNull(myReader1("ID_IMPIANTO"), -1)
            'row.Item("ID_VERIFICHE") = par.IfNull(myReader1("ID_VERIFICHE"), -1)
            'row.Item("COD_COMPLESSO") = par.IfNull(myReader1("COD_COMPLESSO"), "")

            'dt.Rows.Add(row)


            'sWhere = Session.Item("IMP2")

            'par.OracleConn.Open()
            'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            'Label3.Text = "0"
            'Do While myReader.Read()
            '    Label3.Text = CInt(Label3.Text) + 1
            'Loop
            'Label3.Text = Label3.Text
            'cmd.Dispose()
            'myReader.Close()
            'par.OracleConn.Close()

            ' Me.DataGrid1.PageSize = 2 'CLng(Label3.Text)
            ''BindGrid()

        End If


    End Sub

    Private Sub BindGrid()

        par.OracleConn.Open()

        '************
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds) ', "DOMANDE_BANDO,COMP_NUCLEO")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label1.Text = " " & ds.Tables(0).Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Session.Add("ID", txtid.Text)

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")

            sOrdinamento = Request.QueryString("ORD")
            sVerifiche = Request.QueryString("VER")

            Select Case txtImpianto.Text

                Case "ME" '"ACQUE METEORICHE"
                    Response.Write("<script>location.replace('Imp_Meteoriche.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")

                Case "AN" '"ANTINCENDIO"
                    Response.Write("<script>location.replace('Imp_Antincendio.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")

                Case "CF" '"CANNA FUMARIA"
                    Response.Write("<script>location.replace('Imp_CannaFumaria.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")

                Case "EL" '"ELETTRICO"
                    Response.Write("<script>location.replace('Imp_Elettrico.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")

                Case "ID" '"IDRICO"
                    Response.Write("<script>location.replace('Imp_Idrico.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")

                Case "SO" '"SOLLEVAMENTO"
                    Response.Write("<script>location.replace('Imp_Sollevamento.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")
                    'Response.Redirect("Imp_Sollevamento.aspx")

                Case "TR" '"TELERISCALDAMENTO"
                    Response.Write("<script>location.replace('Imp_Teleriscaldamento.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")
                    'Response.Redirect("Imp_Teleriscaldamento.aspx")

                Case "TA" '"TERMICO AUTONOMO"
                    Response.Write("<script>location.replace('Imp_RiscaldamentoA.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")
                    'Response.Redirect("Imp_RiscaldamentoA.aspx")

                Case "TE" '"TERMICO CENTRALIZZATO"
                    Response.Write("<script>location.replace('Imp_Riscaldamento.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")
                    'Response.Redirect("Imp_Riscaldamento.aspx")

                Case "TU" '"TUTELA IMMOBILE"
                    Response.Write("<script>location.replace('Imp_Tutela.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")

                Case "GA" '"GAS
                    Response.Write("<script>location.replace('Imp_Gas.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")

                Case "CI" '"CITOFONICO"
                    Response.Write("<script>location.replace('Imp_Citofonico.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")

                Case "TV" '"TV"
                    Response.Write("<script>location.replace('Imp_TV.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&ORD=" & sOrdinamento & "&VER=" & sVerifiche & "');</script>")

                Case Else
                    ''Response.Redirect("Imp_Riscaldamento.aspx")
            End Select


        End If

    End Sub


    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'impianto del Cod. Complesso: " & e.Item.Cells(2).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtImpianto').value='" & e.Item.Cells(10).Text & "'")
            ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'impianto del Cod. Complesso: " & e.Item.Cells(2).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtImpianto').value='" & e.Item.Cells(10).Text & "'")

            ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
            ';document.getElementById('txtImpianto').value='" & e.Item.Cells(2).Text & "'"
        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        ''If e.NewPageIndex >= 0 Then
        ''    DataGrid1.CurrentPageIndex = e.NewPageIndex
        ''    BindGrid()
        ''End If

    End Sub


    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""RicercaVerifiche.aspx""</script>")
    End Sub


    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Dim scriptblock As String

        'BindExcel()
        'Exit Sub

        Try
            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreImpianto = Request.QueryString("IM")
            sVerifiche = Request.QueryString("VER")

            'Response.Write("<script>window.open('Report/ReportRisultatoImpianti.aspx?IMP1=1,&Pas='" & Session.Item("IMP2") & "');</script>")


            'Response.Write("<script>location.replace('RisultatiImpianti.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "');</script>")

            btnStampa.Attributes.Add("OnClick", "javascript:window.open('Report/ReportRisultatoVerifiche.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&VER=" & sVerifiche & "');")

            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "window.open('Report/ReportRisultatoVerifiche.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "&VER=" & sVerifiche & "','Report');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Sub BindExcel()

        'DataGrid1.DataSource = Nothing
        'DataGrid1.DataSource = lstVerificheRicerche
        'DataGrid1.DataBind()

        'par.OracleConn.Open()

        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

        'Dim ds As New Data.DataSet()
        'da.Fill(ds) ', "DOMANDE_BANDO,COMP_NUCLEO")

        ''DataGrid1.DataSource = ds
        ''DataGrid1.DataBind()
        ''Label1.Text = " " & ds.Tables(0).Rows.Count
        'par.OracleConn.Close()
        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        'Response.Clear()
        'Response.Charset = ""
        'Response.ContentType = "application/vnd.ms-excel"

        'Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter()
        'Dim htmlWrite As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(stringWrite)
        'Dim dg As System.Web.UI.WebControls.DataGrid = New System.Web.UI.WebControls.DataGrid()

        'dg.DataSource = lstVerificheRicerche ' ds.Tables(0)
        'dg.DataBind()
        'dg.RenderControl(htmlWrite)
        'Response.Write(stringWrite.ToString())
        'Response.End()

    End Sub

End Class
