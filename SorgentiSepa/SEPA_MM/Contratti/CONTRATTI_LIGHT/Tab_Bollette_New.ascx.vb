Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing


Partial Class Contratti_Tab_Bollette
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim SumImportoVOCI As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            RicavaPercentSost()
            ' CType(Me.Page, Object).CaricaTabBollette()
        End If

    End Sub




    'Private Sub CaricaGestionale()
    '    Try
    '        Dim num_bolletta As String = ""
    '        Dim I As Integer = 0
    '        Dim importobolletta As Decimal = 0
    '        Dim importobolletta2 As Decimal = 0
    '        Dim importopagato As Decimal = 0
    '        Dim residuo As Decimal = 0
    '        Dim morosita As Integer = 0
    '        Dim riclass As Integer = 0
    '        Dim indiceMorosita As Integer = 0
    '        Dim indiceBolletta As Integer = 0

    '        par.cmd.CommandText = "select TIPO_BOLLETTE_GEST.ACRONIMO,TIPO_BOLLETTE_GEST.UTILIZZABILE,bol_bollette_gest.* from SISCOM_MI.TIPO_BOLLETTE_GEST,siscom_mi.bol_bollette_GEST where BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID (+)" _
    '            & "AND bol_bollette_gest.id_contratto=" & txtIdContratto.Value & " order by bol_bollette_gest.data_emissione desc,bol_bollette_gest.id desc"
    '        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim dtQuery2 As New Data.DataTable
    '        Dim dt1 As New Data.DataTable
    '        Dim rowDT As System.Data.DataRow

    '        dt1.Columns.Add("id")
    '        dt1.Columns.Add("num_tipo")
    '        dt1.Columns.Add("riferimento_da")
    '        dt1.Columns.Add("riferimento_a")
    '        dt1.Columns.Add("data_emissione")
    '        dt1.Columns.Add("importobolletta")
    '        dt1.Columns.Add("data_pagamento")
    '        dt1.Columns.Add("note")
    '        dt1.Columns.Add("anteprima")
    '        dt1.Columns.Add("sposta")
    '        dt1.Columns.Add("dettagli")
    '        dt1.Columns.Add("id_tipo")
    '        dt1.Columns.Add("tipo_appl")

    '        da1.Fill(dtQuery2)
    '        da1.Dispose()

    '        Dim TOTimportobolletta As Decimal = 0
    '        Dim importoVoceEmessa As Decimal = 0

    '        For Each row As Data.DataRow In dtQuery2.Rows
    '            indiceMorosita = 0
    '            indiceBolletta = 0
    '            rowDT = dt1.NewRow()

    '            importobolletta = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00")
    '            If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "N") = "N" Then
    '                importobolletta2 = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00")
    '            End If

    '            'CONTROLLO IN BOL_BOLLETTE_VOCI SE è stata emessa quella bolletta (in bol_bollette)
    '            par.cmd.CommandText = "SELECT ID AS ID_VOCE_GEST FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & par.IfNull(row.Item("ID"), 0)
    '            Dim daVociGest As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '            Dim dtVociGest As New Data.DataTable
    '            Dim rowDTVociGest As System.Data.DataRow
    '            daVociGest.Fill(dtVociGest)
    '            daVociGest.Dispose()


    '            If dtVociGest.Rows.Count > 0 Then
    '                For Each rowDTVociGest In dtVociGest.Rows
    '                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_VOCE_BOLLETTA_GEST=" & par.IfNull(rowDTVociGest.Item("ID_VOCE_GEST"), 0)
    '                    Dim daVociNew As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '                    Dim dtVociNew As New Data.DataTable
    '                    Dim rowDTVociNew As System.Data.DataRow
    '                    daVociNew.Fill(dtVociNew)
    '                    daVociNew.Dispose()

    '                    If dtVociNew.Rows.Count > 0 Then
    '                        For Each rowDTVociNew In dtVociNew.Rows
    '                            importoVoceEmessa += par.IfNull(rowDTVociNew.Item("IMPORTO"), 0)
    '                        Next
    '                    End If
    '                Next
    '            End If

    '            Dim STATO As String = ""

    '            residuo = importobolletta - importoVoceEmessa

    '            TOTimportobolletta = TOTimportobolletta + (importobolletta2 - importoVoceEmessa)

    '            rowDT.Item("num_tipo") = num_bolletta & " " & STATO & " " & par.IfNull(row.Item("ACRONIMO"), "---")
    '            rowDT.Item("riferimento_da") = par.FormattaData(par.IfNull(row.Item("riferimento_da"), ""))
    '            rowDT.Item("riferimento_a") = par.FormattaData(par.IfNull(row.Item("riferimento_a"), ""))
    '            rowDT.Item("data_emissione") = par.FormattaData(par.IfNull(row.Item("data_emissione"), ""))

    '            rowDT.Item("importobolletta") = Format(residuo, "##,##0.00")
    '            rowDT.Item("note") = par.IfNull(row.Item("NOTE"), "")
    '            rowDT.Item("anteprima") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.open('DettaglioVociGest.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Dettagli" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Details-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"

    '            If rowDT.Item("importobolletta") > 0 Then
    '                If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "") = "P" Then
    '                    rowDT.Item("sposta") = "<a href=""javascript:alert('Attenzione questa scrittura risulta già distribuita in rate come voce nello schema bollette!');void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '                Else
    '                    rowDT.Item("sposta") = "<a href=""javascript:ScegliElaborazione();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '                End If
    '            Else
    '                If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "") = "P" Then
    '                    rowDT.Item("sposta") = "<a href=""javascript:alert('Attenzione il credito è stato gestito parzialmente!');void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '                Else
    '                    rowDT.Item("sposta") = "<a href=""javascript:ScegliElaborazione();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '                End If
    '            End If

    '            rowDT.Item("id_tipo") = par.IfNull(row.Item("ID_TIPO"), 0)
    '            rowDT.Item("id") = par.IfNull(row.Item("id"), 0)


    '            'rowDT.Item("anteprima") = "<a href=javascript:ApriAnteprima();void(0); onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "16px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '            'rowDT.Item("anteprima") = "<a href=""javascript:alert('Non disponibile!');void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "16px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '            rowDT.Item("tipo_appl") = par.IfNull(row.Item("TIPO_APPLICAZIONE"), "")
    '            importobolletta = 0
    '            importobolletta2 = 0

    '            Select Case rowDT.Item("TIPO_APPL")
    '                Case "P"
    '                    DataGridGest.Columns(3).Visible = True

    '                    divGestCon.Value = "1"
    '                    If par.IfNull(rowDT.Item("importobolletta"), 0) < 0 Then
    '                        rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.showModalDialog('DettagliGestionaleCrediti.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest', 'status:no;dialogWidth:630px;dialogHeight:250px;dialogHide:true;help:no;scroll:yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '                    Else
    '                        rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.open('DettagliGestionale.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '                    End If
    '                Case "T"
    '                    DataGridGest.Columns(3).Visible = True
    '                    divGestCon.Value = "1"
    '                    If par.IfNull(rowDT.Item("importobolletta"), 0) < 0 Then
    '                        rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.showModalDialog('DettagliGestionaleCrediti.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest', 'status:no;dialogWidth:630px;dialogHeight:250px;dialogHide:true;help:no;scroll:yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '                    Else
    '                        rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.open('DettagliGestionale.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '                    End If
    '                Case "N"
    '                    rowDT.Item("dettagli") = ""

    '            End Select
    '            If par.IfNull(row.Item("UTILIZZABILE"), 0) = 0 Then
    '                rowDT.Item("sposta") = ""
    '            End If
    '            dt1.Rows.Add(rowDT)
    '        Next

    '        rowDT = dt1.NewRow()
    '        rowDT.Item("id") = -1
    '        rowDT.Item("num_tipo") = ""
    '        rowDT.Item("riferimento_da") = ""
    '        rowDT.Item("riferimento_a") = "TOTALE"
    '        rowDT.Item("data_emissione") = ""
    '        rowDT.Item("importobolletta") = Format(TOTimportobolletta, "##,##0.00")
    '        rowDT.Item("data_pagamento") = ""
    '        rowDT.Item("note") = ""
    '        rowDT.Item("id_tipo") = ""
    '        rowDT.Item("tipo_appl") = ""
    '        rowDT.Item("anteprima") = ""
    '        rowDT.Item("dettagli") = ""
    '        dt1.Rows.Add(rowDT)

    '        If Session.Item("MOD_ELAB_SING_GEST") = 1 Then
    '            DataGridGest.Columns(1).Visible = True
    '        Else
    '            DataGridGest.Columns(1).Visible = False
    '        End If

    '        DataGridGest.DataSource = dt1
    '        DataGridGest.DataBind()


    '        For Each di As DataGridItem In DataGridGest.Items
    '            If di.Cells(6).Text.Contains("TOTALE") Then
    '                For j As Integer = 0 To di.Cells.Count - 1
    '                    If di.Cells(j).Text <> "&nbsp;" And di.Cells(j).Text <> "-1" Then
    '                        di.Cells(j).Font.Bold = True
    '                        di.Cells(j).Font.Underline = True
    '                    End If
    '                Next

    '            End If
    '        Next

    '        For Each di As DataGridItem In DataGridGest.Items
    '            If di.Cells(12).Text.Contains("P") Then
    '                di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
    '                di.ToolTip = "Documento già elaborato con scrittura delle voci in schema bollette! L'importo scalerà in base alle future emissioni."
    '            End If
    '            If di.Cells(12).Text.Contains("T") Then
    '                For j As Integer = 0 To di.Cells.Count - 1
    '                    di.Cells(j).Font.Strikeout = True
    '                Next
    '            End If
    '        Next

    '    Catch ex As Exception
    '        par.myTrans.Rollback()
    '        par.myTrans = par.OracleConn.BeginTransaction()
    '        HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " (funzione CaricaSchemaBoll) - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try
    'End Sub


    Private Sub SettaDimensioneCella()
        For Each di As DataGridItem In DataGridGest.Items
            For j As Integer = 0 To di.Cells.Count - 1
                di.Cells(j).Width = 200
            Next
        Next
    End Sub



    Public Function DisattivaTutto()
        lstBollette.Enabled = True
        ImgAnteprima.Visible = True
    End Function

    Public Function DisattivaTuttoVirtuale()
        lstBollette.Enabled = True
        ImgAnteprima.Visible = True
    End Function

    Public Property idEventoPrincipale() As Long
        Get
            If Not (ViewState("par_idEventoPrincipale") Is Nothing) Then
                Return CLng(ViewState("par_idEventoPrincipale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEventoPrincipale") = value
        End Set

    End Property

    Public Property idIncasso() As Long
        Get
            If Not (ViewState("par_idIncasso") Is Nothing) Then
                Return CLng(ViewState("par_idIncasso"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idIncasso") = value
        End Set

    End Property

    Public Property percenSost() As Integer
        Get
            If Not (ViewState("par_percenSost") Is Nothing) Then
                Return CInt(ViewState("par_percenSost"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_percenSost") = value
        End Set

    End Property

    Public Property tot_bolDaSt() As Decimal
        Get
            If Not (ViewState("par_percenSost") Is Nothing) Then
                Return CDec(ViewState("par_percenSost"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_percenSost") = value
        End Set

    End Property

    Private Sub RicavaPercentSost()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_GEST_CREDITO WHERE ID=1"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                percenSost = par.IfNull(myReader("PERC_SOSTEN"), 1)
            End If
            myReader.Close()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGridGest_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridGest.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFD784';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='lightsteelblue';" _
                                & "document.getElementById('Tab_Bollette1_V3').value=" & e.Item.Cells(0).Text & ";document.getElementById('Tab_Bollette1_importo').value=" & par.VirgoleInPunti(e.Item.Cells(8).Text) & ";")
        End If

    End Sub

    Protected Sub DataGridContab_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridContab.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFD784';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='lightsteelblue';" _
                                & "document.getElementById('Tab_Bollette1_V3').value=" & e.Item.Cells(0).Text & ";")

        End If

    End Sub

    Private Function ControllaSolleciti() As Integer
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT BOL_BOLLETTE_SOLLECITI.data_invio,bol_bollette.* FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_SOLLECITI WHERE BOL_BOLLETTE.ID=BOL_BOLLETTE_SOLLECITI.ID_BOLLETTA AND ID_BOLLETTA in (select id from siscom_mi.bol_bollette where id_contratto=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & ") order by data_invio desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                sollecito = 1
            Else
                sollecito = 0
            End If
            myReader.Close()

            par.OracleConn.Close()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return sollecito

    End Function

    Public Property sollecito() As Integer
        Get
            If Not (ViewState("par_sollecito") Is Nothing) Then
                Return CInt(ViewState("par_sollecito"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_sollecito") = value
        End Set

    End Property

    Protected Sub DataGridContab_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridContab.PageIndexChanged
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "1"
        If e.NewPageIndex >= 0 Then
            DataGridContab.CurrentPageIndex = e.NewPageIndex
            CType(Me.Page, Object).CaricaTabBollette()
        End If
    End Sub

End Class
