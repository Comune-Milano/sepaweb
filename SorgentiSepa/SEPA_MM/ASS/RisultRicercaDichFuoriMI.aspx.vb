
Partial Class ASS_RisultRicercaDichFuoriMI
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValorePG As String
    Dim sValoreST As String
    Dim sValoreCA As String
    Dim sStringaWhere As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("C")) Then
                sValoreCG = Request.QueryString("C")
            End If
            If Not IsNothing(Request.QueryString("N")) Then
                sValoreNM = Request.QueryString("N")
            End If
            If Not IsNothing(Request.QueryString("CF")) Then
                sValoreCF = Request.QueryString("CF")
            End If
            If Not IsNothing(Request.QueryString("PG")) Then
                sValorePG = Request.QueryString("PG")
            End If
            If Not IsNothing(Request.QueryString("STATO")) Then
                sValoreST = Request.QueryString("STATO")
            End If
            If Not IsNothing(Request.QueryString("COMAS")) Then
                sValoreCA = Request.QueryString("COMAS")
            End If

        End If
        BindGrid()
    End Sub

    Private Sub BindGrid()
        Try
            Dim bTrovato As Boolean
            Dim sValore As String
            Dim sCompara As String

            bTrovato = True
            sStringaWhere = ""

            If sValoreCG <> "" Then
                If bTrovato = True Then sStringaWhere = sStringaWhere & " AND "
                sValore = sValoreCG
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaWhere = sStringaWhere & " COMP_NUCLEO_vsa.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If sValoreNM <> "" Then
                If bTrovato = True Then sStringaWhere = sStringaWhere & " AND "

                sValore = sValoreNM
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaWhere = sStringaWhere & " COMP_NUCLEO_vsa.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If


            If sValoreCF <> "" Then
                If bTrovato = True Then sStringaWhere = sStringaWhere & " AND "

                sValore = sValoreCF
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaWhere = sStringaWhere & " COMP_NUCLEO_vsa.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If sValorePG <> "" Then
                If bTrovato = True Then sStringaWhere = sStringaWhere & " AND "

                sValore = sValorePG
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaWhere = sStringaWhere & " DICHIARAZIONI_vsa.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If sValoreST <> "TUTTI" And sValoreST <> "" Then
                If bTrovato = True Then sStringaWhere = sStringaWhere & " AND "

                sValore = sValoreST
                sCompara = " = "

                bTrovato = True
                sStringaWhere = sStringaWhere & " DICHIARAZIONI_vsa.ID_STATO" & sCompara & "" & sValore & " "
            End If

            If sValoreCA <> "-1" And sValoreCA <> "" Then
                If bTrovato = True Then sStringaWhere = sStringaWhere & " AND "

                sValore = sValoreCA
                sCompara = " = "

                bTrovato = True
                sStringaWhere = sStringaWhere & " DICH_ASSEGN_FUORI_MI.ID_LUOGO_FUORI_MI " & sCompara & "" & sValore
            End If

            par.OracleConn.Open()
            par.cmd.CommandText = " SELECT DISTINCT DICHIARAZIONI_VSA.ID, DICHIARAZIONI_VSA.PG AS DICHIARAZIONE,NVL((SELECT NOME FROM comuni_nazioni where comuni_nazioni.id=ID_LUOGO_FUORI_MI),'') as COMUNE_ASSEGN, " _
                                & " TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'yyyyMMdd'),'DD/MM/YYYY') AS DATA_PG, " _
                                & " COMP_NUCLEO_VSA.COD_FISCALE AS CODICE_FISCALE, COMP_NUCLEO_VSA.COGNOME, COMP_NUCLEO_VSA.NOME " _
                                & " FROM DICHIARAZIONI_VSA, COMP_NUCLEO_VSA,DICH_ASSEGN_FUORI_MI " _
                                & " WHERE COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID AND DICH_ASSEGN_FUORI_MI.id_dichiarazione=DICHIARAZIONI_VSA.id " _
                                & " AND comp_nucleo_VSA.progr=0 " & sStringaWhere _
                                & " ORDER BY DICHIARAZIONE ASC,COGNOME ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            DgvDichiarazione.DataSource = dt
            dgvDichiarazione.DataBind()

            lblNumTot.Text = dt.Rows.Count

            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - BindGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub dgvDichiarazione_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvDichiarazione.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='yellow';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='red';" _
                                & "document.getElementById('txtmia').value='Hai selezionato la dichiarazione: " & e.Item.Cells(1).Text.Replace("'", "\'") & " del " & e.Item.Cells(2).Text.Replace("'", "\'") & "';" _
                                & "document.getElementById('idSelected').value='" & e.Item.Cells(0).Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnProcedi').click();")
        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If idSelected.Value <> "0" Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "window.open('../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=" & idSelected.Value & "&GLocat=1&fuoriMilano=1');", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Nessuna dichiarazione selezionata!');", True)
        End If
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExcel.Click
        Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(dgvDichiarazione, "ExportDichFuoriMI", , , , False)
        If System.IO.File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "downloadFile('../FileTemp/" & nomefile & "');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomefile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
        End If
    End Sub
End Class
