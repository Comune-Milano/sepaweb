Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports Telerik.Web.UI
Imports System.Data

Partial Class MANUTENZIONI_RisultatiLotti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sStringaSql2 As String

    Public filiale As String
    Public servizi As String
    Public esercizio As String
    Public complesso As String
    Public serviziDettaglio As String

    Public formPadre As String

    Dim dt As New Data.DataTable



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        'Dim Str As String
        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"

        'Response.Write(Str)

        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
            ' Cerca()
            Response.Flush()
        End If

    End Sub

    Private Sub Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""


        If esercizio <> "" And esercizio <> "-1" Then
            sValore = esercizio
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO" & sCompara & " " & par.PulisciStrSql(sValore) & " "
        End If

        If filiale <> "" And filiale <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = filiale
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.LOTTI.ID_FILIALE" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If servizi <> "" And servizi <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = servizi
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If complesso <> "" And complesso <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = complesso
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.LOTTI_PATRIMONIO.ID_EDIFICIO" & sCompara & " " & par.PulisciStrSql(sValore) & " "
        End If



        sStringaSQL1 = "select distinct SISCOM_MI.LOTTI.ID, SISCOM_MI.LOTTI.ID_FILIALE, SISCOM_MI.LOTTI.TIPO, " _
                                    & " SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO, SISCOM_MI.LOTTI.DESCRIZIONE " _
                    & " from SISCOM_MI.LOTTI,SISCOM_MI.LOTTI_SERVIZI, SISCOM_MI.LOTTI_PATRIMONIO"

        If bTrovato = True Then sStringaSql = sStringaSql & " AND "
        sStringaSql2 = "SISCOM_MI.LOTTI.ID=SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO (+) AND SISCOM_MI.LOTTI.ID=SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO  "


        If sStringaSql <> "" Or sStringaSql2 <> "" Then
            sStringaSQL1 = sStringaSQL1 & " where LOTTI.TIPO='" & UCase(Request.QueryString("T")) & "' AND " & sStringaSql & sStringaSql2
        End If
        sStringaSQL1 = sStringaSQL1 & " order by SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO,SISCOM_MI.LOTTI.ID_FILIALE ASC,SISCOM_MI.LOTTI.TIPO Asc, SISCOM_MI.LOTTI.DESCRIZIONE"

        dt.Columns.Add("ID")
        dt.Columns.Add("ID_FILIALE")
        dt.Columns.Add("ID_ESERCIZIO_FINANZIARIO")
        dt.Columns.Add("TIPO")
        dt.Columns.Add("DESCRIZIONE")
        dt.Columns.Add("ID_APPALTO")
        dt.Columns.Add("ID_COMPLESSO")


        Dim RIGA As System.Data.DataRow

        par.OracleConn.Open()
        Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, par.OracleConn)
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

        While myReader.Read()
            RIGA = dt.NewRow()
            RIGA.Item("ID") = par.IfNull(myReader("ID"), "")
            If myReader("TIPO") = "M" Then
                RIGA.Item("TIPO") = ""
            Else
                RIGA.Item("TIPO") = ""
            End If

            'RIGA.Item("DATA INIZIO") = par.IfNull(myReader("DATA INIZIO"), "")
            'RIGA.Item("DATA FINE") = par.IfNull(myReader("DATA FINE"), "")
            RIGA.Item("DESCRIZIONE") = par.IfNull(myReader("DESCRIZIONE"), "")

            If par.IfNull(myReader("ID_FILIALE"), 0) <> 0 Then
                cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI where SISCOM_MI.TAB_FILIALI.ID=" & myReader("ID_FILIALE")
                Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
                While myreader2.Read()
                    RIGA.Item("ID_FILIALE") = par.IfNull(myreader2("NOME"), "")
                End While
                myreader2.Close()
            End If

            If par.IfNull(myReader("ID_ESERCIZIO_FINANZIARIO"), 0) <> 0 Then
                cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID," _
                                    & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO," _
                                    & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE " _
                            & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                            & " where SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=" & myReader("ID_ESERCIZIO_FINANZIARIO")

                Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
                While myreader2.Read()
                    RIGA.Item("ID_ESERCIZIO_FINANZIARIO") = par.IfNull(myreader2("INIZIO") & "-" & myreader2("FINE"), "")
                End While
                myreader2.Close()
            End If

            dt.Rows.Add(RIGA)
        End While

        cmd.Dispose()
        myReader.Close()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Session.Add("MIADT", dt)

        BindGrid()
    End Sub

    Private Sub BindGrid()

        par.OracleConn.Open()


        dt = Session.Item("MIADT")
        DataGrid1.DataSource = dt
        DataGrid1.DataBind()

        'LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this; this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il lotto: " & Replace(e.Item.Cells(4).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
    '    End If

    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}; this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this; this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il lotto: " & Replace(e.Item.Cells(4).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
    '    End If
    'End Sub

    'Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '    If e.NewPageIndex >= 0 Then
    '        DataGrid1.CurrentPageIndex = e.NewPageIndex
    '        BindGrid()
    '    End If
    'End Sub


    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Non hai selezionato alcuna riga!')</script>")
            Exit Sub
        Else
            Session.Add("ID", txtid.Text)

            formPadre = UCase(Request.QueryString("TIPO"))


            filiale = UCase(Request.QueryString("FI"))
            servizi = UCase(Request.QueryString("SE"))
            esercizio = UCase(Request.QueryString("EF"))
            complesso = UCase(Request.QueryString("CO"))
            serviziDettaglio = UCase(Request.QueryString("DT"))

            formPadre = UCase(Request.QueryString("TIPO"))

            If formPadre = "RICERCA_LOTTI" Then
                'Response.Write("<script>document.location.href=""NuovoLotto.aspx?FI=" & filiale _
                '                                                          & "&EF=" & esercizio _
                '                                                          & "&SE=" & servizi _
                '                                                          & "&CO=" & complesso _
                '                                                          & "&TIPO=" & formPadre & "');</script>")

                Response.Write("<script>location.replace('NuovoLotto.aspx?FI=" & filiale _
                                                                      & "&EF=" & esercizio _
                                                                      & "&SE=" & servizi _
                                                                      & "&CO=" & complesso _
                                                                      & "&TIPO=" & formPadre & "');</script>")


            Else
                Response.Write("<script>location.replace('LottiScambio.aspx?FI=" & filiale _
                                                                          & "&EF=" & esercizio _
                                                                          & "&SE=" & servizi _
                                                                          & "&DT=" & serviziDettaglio _
                                                                          & "&TIPO=" & formPadre & "');</script>")


            End If

        End If
    End Sub


    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click

        formPadre = UCase(Request.QueryString("TIPO"))

        If formPadre = "RICERCA_LOTTI" Then
            Response.Write("<script>document.location.href=""RicercaLotti.aspx""</script>")
        Else
            Response.Write("<script>document.location.href=""RicercaLottiScambio.aspx""</script>")
        End If
    End Sub

    Protected Sub RadButton3_Click(sender As Object, e As System.EventArgs) Handles RadButton3.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub


    Protected Sub DataGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid1.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato il lotto: " & Replace(dataItem("DESCRIZIONE").Text, "'", "\'") & "';document.getElementById('txtid').value='" & dataItem("ID").Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
        End If
    End Sub

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            esercizio = UCase(Request.QueryString("EF"))
            filiale = UCase(Request.QueryString("FI"))
            complesso = UCase(Request.QueryString("CO"))

            servizi = UCase(Request.QueryString("SE"))
            serviziDettaglio = UCase(Request.QueryString("DT"))

            formPadre = UCase(Request.QueryString("TIPO"))
            Dim bTrovato As Boolean
            Dim sValore As String
            Dim sCompara As String


            bTrovato = False
            sStringaSql = ""


            If esercizio <> "" And esercizio <> "-1" Then
                sValore = esercizio
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO" & sCompara & " " & par.PulisciStrSql(sValore) & " "
            End If

            If filiale <> "" And filiale <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = filiale
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "SISCOM_MI.LOTTI.ID_FILIALE" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If servizi <> "" And servizi <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = servizi
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            If complesso <> "" And complesso <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = complesso
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "SISCOM_MI.LOTTI_PATRIMONIO.ID_EDIFICIO" & sCompara & " " & par.PulisciStrSql(sValore) & " "
            End If
            sStringaSQL1 = "select distinct SISCOM_MI.LOTTI.ID, SISCOM_MI.LOTTI.ID_FILIALE, SISCOM_MI.LOTTI.TIPO, " _
                                        & " SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO, SISCOM_MI.LOTTI.DESCRIZIONE " _
                        & " from SISCOM_MI.LOTTI,SISCOM_MI.LOTTI_SERVIZI, SISCOM_MI.LOTTI_PATRIMONIO"

            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sStringaSql2 = "SISCOM_MI.LOTTI.ID=SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO (+) AND SISCOM_MI.LOTTI.ID=SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO  "
            If sStringaSql <> "" Or sStringaSql2 <> "" Then
                sStringaSQL1 = sStringaSQL1 & " where LOTTI.TIPO='" & UCase(Request.QueryString("T")) & "' AND " & sStringaSql & sStringaSql2
            End If
            sStringaSQL1 = sStringaSQL1 & " order by SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO,SISCOM_MI.LOTTI.ID_FILIALE ASC,SISCOM_MI.LOTTI.TIPO Asc, SISCOM_MI.LOTTI.DESCRIZIONE"
            dt.Columns.Add("ID")
            dt.Columns.Add("ID_FILIALE")
            dt.Columns.Add("ID_ESERCIZIO_FINANZIARIO")
            dt.Columns.Add("TIPO")
            dt.Columns.Add("DESCRIZIONE")
            dt.Columns.Add("ID_APPALTO")
            dt.Columns.Add("ID_COMPLESSO")


            Dim RIGA As System.Data.DataRow

            par.OracleConn.Open()
            Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, par.OracleConn)
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

            While myReader.Read()
                RIGA = dt.NewRow()
                RIGA.Item("ID") = par.IfNull(myReader("ID"), "")
                If myReader("TIPO") = "M" Then
                    RIGA.Item("TIPO") = ""
                Else
                    RIGA.Item("TIPO") = ""
                End If

                'RIGA.Item("DATA INIZIO") = par.IfNull(myReader("DATA INIZIO"), "")
                'RIGA.Item("DATA FINE") = par.IfNull(myReader("DATA FINE"), "")
                RIGA.Item("DESCRIZIONE") = par.IfNull(myReader("DESCRIZIONE"), "")

                If par.IfNull(myReader("ID_FILIALE"), 0) <> 0 Then
                    cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI where SISCOM_MI.TAB_FILIALI.ID=" & myReader("ID_FILIALE")
                    Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
                    While myreader2.Read()
                        RIGA.Item("ID_FILIALE") = par.IfNull(myreader2("NOME"), "")
                    End While
                    myreader2.Close()
                End If

                If par.IfNull(myReader("ID_ESERCIZIO_FINANZIARIO"), 0) <> 0 Then
                    cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID," _
                                        & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO," _
                                        & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE " _
                                & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                                & " where SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=" & myReader("ID_ESERCIZIO_FINANZIARIO")

                    Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
                    While myreader2.Read()
                        RIGA.Item("ID_ESERCIZIO_FINANZIARIO") = par.IfNull(myreader2("INIZIO") & "-" & myreader2("FINE"), "")
                    End While
                    myreader2.Close()
                End If

                dt.Rows.Add(RIGA)
            End While

            cmd.Dispose()
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("MIADT", dt)


            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub


    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGrid1.AllowPaging = False
        DataGrid1.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGrid1.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGrid1.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGrid1.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGrid1.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        DataGrid1.AllowPaging = True
        DataGrid1.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "CONTRATTI", "CONTRATTI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

End Class
