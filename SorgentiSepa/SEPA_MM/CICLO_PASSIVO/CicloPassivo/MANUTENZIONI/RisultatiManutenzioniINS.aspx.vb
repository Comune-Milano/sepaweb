Imports Telerik.Web.UI
Imports System.Data

'*** LISTA RISULTATO MANUTENZIONI Proviene da : RicercaManutenzioni.aspx
'***                                            RicercaManutenzioniSfitti.aspx

Partial Class MANUTENZIONI_RisultatiManutenzioniINS
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public sValoreEsercizioFinanziarioR As String
    Public sValoreServizioR As String
    Public sValoreServizioVoceR As String
    Public sValoreIndirizzoR As String
    Public sValoreComplessoR As String
    Public sValoreEdificioR As String
    Public sValorImpiantoR As String
    Public sValoreAppaltoR As String
    Public sValoreTipoR As String
    Public sValoreUbicazione As String
    Public sValoreProvenienza As String
    Private isFilter As Boolean = False

    Public Property sStringaSql() As String
        Get
            If Not (ViewState("sStringaSql") Is Nothing) Then
                Return CStr(ViewState("sStringaSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("sStringaSql") = value
        End Set

    End Property

    Public Property griglia() As String
        Get
            If Not (ViewState("griglia") Is Nothing) Then
                Return CStr(ViewState("griglia"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("griglia") = value
        End Set

    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            '*LBLID.Text = Request.QueryString("T")
            Session.Add("ID", 0)
            SetGriglia()
            If griglia = 1 Then
                HFGriglia.Value = DataGrid1.ClientID
            ElseIf griglia = 2 Then
                HFGriglia.Value = DataGrid2.ClientID
            End If
            ''seletc BUONA PER IL FUORI LOTTO
            '            select APPALTI_LOTTI_SERVIZI.ID_APPALTO,
            '	   PF_VOCI_IMPORTO.ID_LOTTO,PF_VOCI_IMPORTO.ID_SERVIZIO, 
            '	   APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO,
            '	   --APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO as "ID_UBICAZIONE", 

            '	   APPALTI.NUM_REPERTORIO, 
            '(select DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=EDIFICI.ID_COMPLESSO AND id<>1) AS COMPLESSO,  

            '	EDIFICI.DENOMINAZIONE as DESC_EDIFICIO,  
            '	(INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP)  AS INDIRIZZO,  
            'LOTTI.DESCRIZIONE as DESCRIZIONE_LOTTO,  
            'TAB_SERVIZI.DESCRIZIONE as SERVIZIO,  
            'PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI_IMPORTO.DESCRIZIONE) as SERVIZIO_VOCE  

            'from  SISCOM_MI.APPALTI_LOTTI_SERVIZI,  
            '	  --SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,  
            '	  SISCOM_MI.APPALTI,  SISCOM_MI.LOTTI,  SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,  SISCOM_MI.EDIFICI  ,SISCOM_MI.INDIRIZZI,SISCOM_MI.PF_VOCI  
            'where        
            '--APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)     

            '--and APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO is null     


            ' EDIFICI.ID_INDIRIZZO_PRINCIPALE=INDIRIZZI.ID (+)    
            'and APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)    
            'and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)    
            'and PF_VOCI_IMPORTO.ID_LOTTO=SISCOM_MI.LOTTI.ID (+)    
            'and PF_VOCI_IMPORTO.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)    
            'and PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+)    

            '--and APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO (+) 

            'and PF_VOCI_IMPORTO.ID_SERVIZIO=21 
            'and PF_VOCI_IMPORTO.ID_SERVIZIO<>15  
            'and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=1389 --<>1389 

            'AND edifici.id  not IN (SELECT id_EDIFICIO FROM APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO=44)

            'AND EDIFICI.ID<>1

            '--and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO  in (select ID from SISCOM_MI.PF_VOCI_IMPORTO   
            '--											 	 	where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=1389))  
            '--and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO<>44 --<>44 
            '/*and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO NOT in (select distinct(ID_APPALTO)  from SISCOM_MI.APPALTI_LOTTI_SERVIZI  
            '											   where ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  
            '											   		 					   where ID_SERVIZIO <>15   
            '																		     and ID_LOTTO in ( select ID from SISCOM_MI.LOTTI  where ID_ESERCIZIO_FiNANZIARIO=26 and ID_FILIALE=25) 
            '																	        and PF_VOCI_IMPORTO.ID_SERVIZIO=21) 
            '												and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=1389)*/
            '	 order by INDIRIZZO asc



        End If


    End Sub

    Protected Sub DataGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid1.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
            End If
            If e.CommandName = RadGrid.FilterCommandName Then
                isFilter = True
            Else
                isFilter = False
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_ItemCommand - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    'Private Sub BindGrid(ByVal griglia As Integer)

    '    Try

    '        par.OracleConn.Open()

    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)
    '        Dim ds As New Data.DataSet()

    '        da.Fill(ds) ', "DOMANDE_BANDO,COMP_NUCLEO")

    '        If griglia = 1 Then
    '            DataGrid1.DataSource = ds
    '            DataGrid1.DataBind()
    '        Else
    '            DataGrid2.DataSource = ds
    '            DataGrid2.DataBind()
    '        End If

    '        Label1.Text = " " & ds.Tables(0).Rows.Count
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try

    'End Sub

    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato Num. Reper.: " & Replace(e.Item.Cells(5).Text, "'", "\'") & " del complesso: " & Left(Replace(e.Item.Cells(7).Text, "'", "\'"), 30) & "';document.getElementById('txtIdLotto').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtIdEdificio').value='" & par.IfEmpty(e.Item.Cells(4).Text, "") & "';document.getElementById('txtIdAppalto').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdServizio').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtIdPF_VoceServizio').value='" & e.Item.Cells(3).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato Num. Reper.: " & Replace(e.Item.Cells(5).Text, "'", "\'") & " del complesso: " & Left(Replace(e.Item.Cells(7).Text, "'", "\'"), 30) & "';document.getElementById('txtIdLotto').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtIdEdificio').value='" & par.IfEmpty(e.Item.Cells(4).Text, "") & "';document.getElementById('txtIdAppalto').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdServizio').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtIdPF_VoceServizio').value='" & e.Item.Cells(3).Text & "'")

    '    End If

    'End Sub

    'Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '    ''If e.NewPageIndex >= 0 Then
    '    ''    DataGrid1.CurrentPageIndex = e.NewPageIndex
    '    ''    BindGrid()
    '    ''End If

    'End Sub









    'Protected Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato Num. Reper.: " & Replace(e.Item.Cells(5).Text, "'", "\'") & " del complesso: " & Left(Replace(e.Item.Cells(7).Text, "'", "\'"), 30) & "';document.getElementById('txtIdLotto').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtIdEdificio').value='" & par.IfEmpty(e.Item.Cells(4).Text, "") & "';document.getElementById('txtIdAppalto').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdServizio').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtIdPF_VoceServizio').value='" & e.Item.Cells(3).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato Num. Reper.: " & Replace(e.Item.Cells(5).Text, "'", "\'") & " del complesso: " & Left(Replace(e.Item.Cells(7).Text, "'", "\'"), 30) & "';document.getElementById('txtIdLotto').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtIdEdificio').value='" & par.IfEmpty(e.Item.Cells(4).Text, "") & "';document.getElementById('txtIdAppalto').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdServizio').value='" & e.Item.Cells(2).Text & "';document.getElementById('txtIdPF_VoceServizio').value='" & e.Item.Cells(3).Text & "'")

    '    End If
    'End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato Num. Reper.: " & dataItem("NUM_REPERTORIO").Text & " del complesso: " & dataItem("COMPLESSO").Text.Replace("'", "\'") & "';document.getElementById('txtIdLotto').value='" & dataItem("ID_LOTTO").Text & "';document.getElementById('txtIdEdificio').value='" & dataItem("ID_UBICAZIONE").Text & "';document.getElementById('txtIdAppalto').value='" & dataItem("ID_APPALTO").Text & "';document.getElementById('txtIdServizio').value='" & dataItem("ID_SERVIZIO").Text & "';document.getElementById('txtIdPF_VoceServizio').value='" & dataItem("ID_PF_VOCE_IMPORTO").Text & "';")
                e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If par.IfEmpty(Me.txtIdServizio.Value, 0) <= 0 Then
            RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            ' Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Session.Add("ID", 0)
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))
            sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))
            sValoreServizioR = Strings.Trim(Request.QueryString("SE_R"))
            sValoreServizioVoceR = Strings.Trim(Request.QueryString("SV_R"))
            sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))
            sValoreComplessoR = Strings.Trim(Request.QueryString("CO_R"))
            sValoreEdificioR = Strings.Trim(Request.QueryString("ED_R"))
            sValoreTipoR = Strings.Trim(Request.QueryString("TIPOR"))
            sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))
            Dim sEdificio As String
            sEdificio = Me.txtIdEdificio.Value
            If sEdificio = "&nbsp;" Then
                sEdificio = -1
            End If
            sValoreProvenienza = Strings.Trim(Request.QueryString("PROVENIENZA"))
            Response.Write("<script>location.replace('Manutenzioni.aspx?TIPO=0" _
                                                                    & "&ED=" & sEdificio _
                                                                    & "&SE=" & par.IfEmpty(Me.txtIdServizio.Value, -1) _
                                                                    & "&SV=" & par.IfEmpty(Me.txtIdPF_VoceServizio.Value, -1) _
                                                                    & "&AP=" & par.IfEmpty(Me.txtIdAppalto.Value, -1) _
                                                                    & "&IN_R=" & par.VaroleDaPassare(sValoreIndirizzoR) _
                                                                    & "&SE_R=" & sValoreServizioR _
                                                                    & "&SV_R=" & sValoreServizioVoceR _
                                                                    & "&AP_R=" & sValoreAppaltoR _
                                                                    & "&CO_R=" & sValoreComplessoR _
                                                                    & "&ED_R=" & sValoreEdificioR _
                                                                    & "&TIPOR=" & sValoreTipoR _
                                                                    & "&UBI=" & sValoreUbicazione _
                                                                    & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                    & "&PROVENIENZA=" & sValoreProvenienza & "');</script>")
        End If
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Session.Remove("IMP1")

        Response.Write("<script>document.location.href='RicercaManutenzioniINS.aspx?TIPOR=" & Strings.Trim(Request.QueryString("TIPOR")) & "';</script>")

    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            If griglia = 1 Then
                par.cmd.CommandText = sStringaSql
                Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                TryCast(sender, RadGrid).DataSource = dt
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    'Protected Sub DataGrid1_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '    DataGrid1.CurrentPageIndex = e.NewPageIndex
    '    SetGriglia()
    'End Sub

    Private Sub SetGriglia()
        Try
            Dim sStringaSqlPatrimonio As String = ""
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))

            sValoreServizioR = Strings.Trim(Request.QueryString("SE_R"))
            sValoreServizioVoceR = Strings.Trim(Request.QueryString("SV_R"))

            sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

            sValoreComplessoR = Strings.Trim(Request.QueryString("CO_R"))
            sValoreEdificioR = Strings.Trim(Request.QueryString("ED_R"))
            sValoreTipoR = Strings.Trim(Request.QueryString("TIPOR"))

            sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))



            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
            End If

            'sStringaSql = "select APPALTI_LOTTI_SERVIZI.ID_APPALTO," _
            '                  & " PF_VOCI_IMPORTO.ID_LOTTO," _
            '                  & " PF_VOCI_IMPORTO.ID_SERVIZIO," _
            '                  & " APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO," _
            '                  & " APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO," _
            '                 & " APPALTI.NUM_REPERTORIO," _
            '                 & " (select SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=EDIFICI.ID_COMPLESSO) as COMPLESSO," _
            '                 & " EDIFICI.DENOMINAZIONE as DESC_EDIFICIO," _
            '                 & " (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP) AS INDIRIZZO, " _
            '                 & " TAB_COMUNI.COMUNE," _
            '                 & " LOTTI.DESCRIZIONE as DESCRIZIONE_LOTTO, " _
            '                 & " TAB_SERVIZI.DESCRIZIONE as SERVIZIO, " _
            '                 & " PF_VOCI_IMPORTO.DESCRIZIONE as SERVIZIO_VOCE " _
            '         & " from  SISCOM_MI.APPALTI_LOTTI_SERVIZI, " _
            '               & " SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, " _
            '               & " SISCOM_MI.APPALTI, " _
            '               & " SISCOM_MI.LOTTI, " _
            '               & " SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO, " _
            '               & " SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_COMUNI " _
            '         & " where " _
            '         & "       APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
            '         & "   and EDIFICI.ID_INDIRIZZO_PRINCIPALE=INDIRIZZI.ID (+) " _
            '         & "   and INDIRIZZI.COD_COMUNE=TAB_COMUNI.COD_COM (+) " _
            '         & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) " _
            '         & "   and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
            '         & "   and PF_VOCI_IMPORTO.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
            '         & "   and PF_VOCI_IMPORTO.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+) "


            If sValoreTipoR = 0 Or sValoreTipoR = 2 Then

                DataGrid1.Visible = True
                DataGrid2.Visible = False
                sStringaSql = "select APPALTI_LOTTI_SERVIZI.ID_APPALTO,PF_VOCI_IMPORTO.ID_LOTTO,PF_VOCI_IMPORTO.ID_SERVIZIO," _
                                  & " APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO,EDIFICI.ID as ""ID_UBICAZIONE""," _
                                  & " APPALTI.NUM_REPERTORIO," _
                                  & " (select trim(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=EDIFICI.ID_COMPLESSO) AS COMPLESSO, " _
                                  & " trim(EDIFICI.DENOMINAZIONE) as DESC_EDIFICIO, " _
                                  & " (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP)  AS INDIRIZZO, " _
                                  & " trim(TAB_SERVIZI.DESCRIZIONE) as SERVIZIO, " _
                                  & " PF_VOCI.CODICE|| ' - ' ||trim(PF_VOCI_IMPORTO.DESCRIZIONE) AS SERVIZIO_VOCE, appalti.descrizione as descrizione_appalti " _
                         & " from  SISCOM_MI.APPALTI_LOTTI_SERVIZI, " _
                               & " SISCOM_MI.APPALTI, " _
                               & " SISCOM_MI.LOTTI, " _
                               & " SISCOM_MI.TAB_SERVIZI," _
                               & " SISCOM_MI.PF_VOCI_IMPORTO, " _
                               & " SISCOM_MI.EDIFICI  ," _
                               & " SISCOM_MI.INDIRIZZI," _
                               & " SISCOM_MI.PF_VOCI "

                If sValoreTipoR = 0 Then
                    sStringaSql = sStringaSql & " ,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                & " where " _
                                & "       APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                                & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO (+)" _
                                & "   and APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO is null  " _
                                & "   and EDIFICI.ID_INDIRIZZO_PRINCIPALE=INDIRIZZI.ID (+) " _
                                & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) " _
                                & "   and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
                                & "   and PF_VOCI_IMPORTO.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
                                & "   and PF_VOCI_IMPORTO.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+) " _
                                & "   and PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) "
                Else

                    sStringaSql = sStringaSql & " where " _
                             & "     EDIFICI.ID<>1   " _
                             & "   and EDIFICI.ID_INDIRIZZO_PRINCIPALE=INDIRIZZI.ID (+) " _
                             & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) " _
                             & "   and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
                             & "   and PF_VOCI_IMPORTO.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
                             & "   and PF_VOCI_IMPORTO.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+) " _
                             & "   and PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) "
                End If


                'SERVIZIO
                If sValoreTipoR = 0 Then
                    'NORMALE

                    If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                        'RICERCA 1
                        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR _
                                                  & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"
                    Else
                        'RICERCA 2 (se si ferma all'indirizzo)

                        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"
                    End If
                Else
                    'FUORI LOTTO
                    If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR _
                                                  & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                        'sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                        '                                                            & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")"
                    End If
                End If
                '*****************************************************

                'SERVIZIO VOCE
                If par.IfEmpty(sValoreServizioVoceR, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & sValoreServizioVoceR

                    'If sValoreTipoR = 0 Then
                    '    'NORMALE
                    '    sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & sValoreServizioVoceR
                    'Else
                    '    'FUORI LOTTO
                    '    sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO<>" & sValoreServizioVoceR _
                    '                              & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO NOT in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                    '                                                                               & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & sValoreServizioVoceR & ")) "
                    'End If
                End If
                '*****************************************************


                'APPALTO
                If sValoreTipoR = 0 Then
                    'NORNALE
                    If par.IfEmpty(sValoreAppaltoR, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO=" & sValoreAppaltoR
                    Else
                        sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1) "
                    End If
                Else
                    'FUORI LOTTO
                    If par.IfEmpty(sValoreAppaltoR, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and EDIFICI.ID  NOT in (select distinct(ID_EDIFICIO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO where ID_APPALTO=" & sValoreAppaltoR & ") and appalti.id = " & sValoreAppaltoR

                        'sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO<>" & sValoreAppaltoR _
                        '                          & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO NOT in (select distinct(ID_APPALTO) " _
                        '                                                                      & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                                                      & " where ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                        '                                                                                                    & " where ID_SERVIZIO <>15 " _
                        '                                                                                                    & "   and ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                        '                                                                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")"

                        'If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                        '    sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR & ")"
                        'Else
                        '    sStringaSql = sStringaSql & ")"
                        'End If

                        'If par.IfEmpty(sValoreServizioVoceR, "-1") <> "-1" Then
                        '    sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & sValoreServizioVoceR
                        'End If

                        'sStringaSql = sStringaSql & ")"
                    End If
                End If
                '***********************************************

                'INDIRIZZO/COMPLESSO/EDIFICIO
                If par.IfEmpty(sValoreEdificioR, "-1") <> "-1" Then
                    If sValoreTipoR = 0 Then
                        'NORMALE
                        sStringaSqlPatrimonio = " and  APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=" & sValoreEdificioR
                    Else
                        'FUORI LOTTO
                        sStringaSqlPatrimonio = " and  EDIFICI.ID=" & sValoreEdificioR
                    End If

                ElseIf par.IfEmpty(sValoreComplessoR, "-1") <> "-1" Then
                    If sValoreTipoR = 0 Then
                        'NORMALE
                        sStringaSqlPatrimonio = " and APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & sValoreComplessoR & ")"
                    Else
                        'FUORI LOTTO
                        sStringaSqlPatrimonio = " and EDIFICI.ID in (select ID from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & sValoreComplessoR & ")"
                    End If

                ElseIf par.IfEmpty(sValoreIndirizzoR, "-1") <> "-1" Then
                    If sValoreTipoR = 0 Then
                        'NORMALE
                        sStringaSqlPatrimonio = " and  APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%') ) "
                    Else
                        'FUORI LOTTO
                        sStringaSqlPatrimonio = " and  EDIFICI.ID in (select ID from SISCOM_MI.EDIFICI where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%') ) "
                    End If
                    '****************************************
                End If

                sStringaSql = sStringaSql & sStringaSqlPatrimonio
                griglia = 1
                'BindGrid(1)

            Else
                'LOTTO SOLO IMPIANTI
                DataGrid1.Visible = False
                DataGrid2.Visible = True

                Dim sSQL_DettaglioIMPIANTO As String

                sSQL_DettaglioIMPIANTO = "CASE IMPIANTI.COD_TIPOLOGIA " _
                                        & " WHEN 'AN' THEN '' " _
                                        & " WHEN 'CF' THEN '' " _
                                        & " WHEN 'CI' THEN '' " _
                                        & " WHEN 'EL' THEN '' " _
                                        & " WHEN 'GA' THEN '' " _
                                        & " WHEN 'ID' THEN '' " _
                                        & " WHEN 'ME' THEN '' " _
                                        & " WHEN 'SO' THEN (select  CASE when SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO is null THEN " _
                                                            & " 'Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                                                       & " ELSE 'Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO END " _
                                                        & " from SISCOM_MI.I_SOLLEVAMENTO where ID=IMPIANTI.ID) " _
                                        & " WHEN 'TA' THEN '' " _
                                        & " WHEN 'TE' THEN '' " _
                                        & " WHEN 'TR' THEN '' " _
                                        & " WHEN 'TU' THEN '' " _
                                        & " WHEN 'TV' THEN '' " _
                                    & " ELSE '' " _
                                    & " END as DETTAGLIO_IMPIANTO, "


                sStringaSql = sStringaSql & "select APPALTI_LOTTI_SERVIZI.ID_APPALTO,PF_VOCI_IMPORTO.ID_LOTTO,PF_VOCI_IMPORTO.ID_SERVIZIO," _
                      & " APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO,IMPIANTI.ID as ""ID_UBICAZIONE""," _
                      & " APPALTI.NUM_REPERTORIO," _
                      & " (select trim(DENOMINAZIONE) AS DENOMINAZIONE  from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=IMPIANTI.ID_COMPLESSO ) AS COMPLESSO, " _
                      & " trim(EDIFICI.DENOMINAZIONE) as DESC_EDIFICIO, " _
                   & " CASE when IMPIANTI.ID_EDIFICIO is null THEN " _
                     & "   (select INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP from SISCOM_MI.INDIRIZZI    " _
                     & "     where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI where  ID=IMPIANTI.ID_COMPLESSO)) " _
                & " ELSE " _
               & "   (select distinct(INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP) from SISCOM_MI.INDIRIZZI    " _
                  & "     where ID in (select ID_INDIRIZZO_PRINCIPALE  from SISCOM_MI.EDIFICI where  ID=IMPIANTI.ID_EDIFICIO) )   " _
                & " END  as INDIRIZZO," _
                      & " TIPOLOGIA_IMPIANTI.DESCRIZIONE as TIPO_IMPIANTO,IMPIANTI.COD_IMPIANTO," _
                      & sSQL_DettaglioIMPIANTO _
                      & " trim(TAB_SERVIZI.DESCRIZIONE) as SERVIZIO, " _
                      & " PF_VOCI.CODICE|| ' - ' ||trim(PF_VOCI_IMPORTO.DESCRIZIONE) AS SERVIZIO_VOCE, appalti.descrizione as descrizione_appalti,IMPIANTI.DESCRIZIONE AS DESC_IMPIANTO " _
             & " from  SISCOM_MI.APPALTI_LOTTI_SERVIZI, " _
                   & " SISCOM_MI.APPALTI, " _
                   & " SISCOM_MI.LOTTI, " _
                   & " SISCOM_MI.TAB_SERVIZI," _
                   & " SISCOM_MI.PF_VOCI_IMPORTO, " _
                   & " SISCOM_MI.IMPIANTI," _
                   & " SISCOM_MI.EDIFICI," _
                   & " SISCOM_MI.TIPOLOGIA_IMPIANTI, " _
                   & " SISCOM_MI.PF_VOCI "


                If sValoreTipoR = 1 Then
                    sStringaSql = sStringaSql & " ,SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                & " where " _
                                & "       APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+)  " _
                                & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO (+)" _
                                & "   and IMPIANTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
                                & "   and APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO is null  " _
                                & "   and IMPIANTI.COD_TIPOLOGIA=TIPOLOGIA_IMPIANTI.COD (+) " _
                                & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) " _
                                & "   and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
                                & "   and PF_VOCI_IMPORTO.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
                                & "   and PF_VOCI_IMPORTO.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+) " _
                                & "   and PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) "
                Else

                    sStringaSql = sStringaSql & " where " _
                                & "       EDIFICI.ID<>1   " _
                                & "   and IMPIANTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
                                & "   and IMPIANTI.COD_TIPOLOGIA=TIPOLOGIA_IMPIANTI.COD (+) " _
                                & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) " _
                                & "   and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
                                & "   and PF_VOCI_IMPORTO.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
                                & "   and PF_VOCI_IMPORTO.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+) " _
                                & "   and PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) "
                End If



                '& " where " _
                '& "       APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO is null  " _
                '& "   and APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+)  " _
                '& "   and IMPIANTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
                '& "   and IMPIANTI.COD_TIPOLOGIA=TIPOLOGIA_IMPIANTI.COD (+) " _
                '& "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) " _
                '& "   and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+) " _
                '& "   and PF_VOCI_IMPORTO.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
                '& "   and PF_VOCI_IMPORTO.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+) " _
                '& "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO (+)"

                'SERVIZIO
                If sValoreTipoR = 1 Then
                    'NORMALE
                    'sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO (+)"

                    If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                        'RICERCA 1
                        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR _
                                                  & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"
                    Else
                        'RICERCA 2 (se si ferma all'indirizzo)

                        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & sFiliale & ")"
                    End If
                Else
                    'FUORI LOTTO
                    If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR _
                                                  & " and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "
                        'sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                        '                                                             & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")"
                    End If
                End If
                '*****************************************************

                'SERVIZIO VOCE
                If par.IfEmpty(sValoreServizioVoceR, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & sValoreServizioVoceR

                    'If sValoreTipoR = 1 Then
                    '    'NORMALE
                    '    sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & sValoreServizioVoceR
                    'Else
                    '    'FUORI LOTTO
                    '    sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO<>" & sValoreServizioVoceR _
                    '                              & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO NOT in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                    '                                                                               & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & sValoreServizioVoceR & ")) "
                    'End If
                End If
                '*****************************************************


                'APPALTO
                If sValoreTipoR = 1 Then
                    'NORNALE
                    If par.IfEmpty(sValoreAppaltoR, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO=" & sValoreAppaltoR
                    Else
                        sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1) "
                    End If
                Else
                    'FUORI LOTTO
                    sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & sValoreAppaltoR _
                                              & " and IMPIANTI.ID  NOT in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO where ID_APPALTO=" & sValoreAppaltoR & ")" _
                                              & " and IMPIANTI.ID      in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1) )"


                    'If par.IfEmpty(sValoreAppaltoR, "-1") <> "-1" Then
                    '    sStringaSql = sStringaSql & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO<>" & sValoreAppaltoR _
                    '                              & " and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO NOT in (select distinct(ID_APPALTO) " _
                    '                                                                          & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                                                          & " where ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                    '                                                                                                        & " where ID_SERVIZIO <>15 " _
                    '                                                                                                        & "   and ID_LOTTO in ( select ID from SISCOM_MI.LOTTI " _
                    '                                                                                                                            & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")"

                    '    If par.IfEmpty(sValoreServizioR, "-1") <> "-1" Then
                    '        sStringaSql = sStringaSql & " and PF_VOCI_IMPORTO.ID_SERVIZIO=" & sValoreServizioR & ")"
                    '    Else
                    '        sStringaSql = sStringaSql & ")"
                    '    End If

                    '    If par.IfEmpty(sValoreServizioVoceR, "-1") <> "-1" Then
                    '        sStringaSql = sStringaSql & " and APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & sValoreServizioVoceR
                    '    End If

                    '    sStringaSql = sStringaSql & ")"
                    'End If
                End If
                '***********************************************

                'INDIRIZOO/COMPLESSO/EDIFICIO
                If par.IfEmpty(sValoreEdificioR, "-1") <> "-1" Then
                    If sValoreTipoR = 1 Then
                        'NORMALE
                        sStringaSqlPatrimonio = " and  APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI where ID_EDIFICIO=" & sValoreEdificioR & ")"
                    Else
                        'FUORI LOTTO
                        sStringaSqlPatrimonio = " and  IMPIANTI.ID_EDIFICIO=" & sValoreEdificioR
                    End If

                ElseIf par.IfEmpty(sValoreComplessoR, "-1") <> "-1" Then
                    If sValoreTipoR = 1 Then
                        'NORMALE
                        sStringaSqlPatrimonio = " and APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI where ID_COMPLESSO=" & sValoreComplessoR & " )"
                    Else
                        'FUORI LOTTO
                        sStringaSqlPatrimonio = " and IMPIANTI.ID_COMPLESSO=" & sValoreComplessoR
                    End If
                ElseIf par.IfEmpty(sValoreIndirizzoR, "-1") <> "-1" Then
                    If sValoreTipoR = 1 Then
                        'NORMALE
                        sStringaSqlPatrimonio = " and  ( APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_INDIRIZZO_RIFERIMENTO in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%')) ) " _
                                                & " or    APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI where ID_EDIFICIO  in (select ID from SISCOM_MI.EDIFICI where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%')) ) )"
                    Else
                        'FUORI LOTTO
                        sStringaSqlPatrimonio = " and  ( IMPIANTI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_INDIRIZZO_RIFERIMENTO in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%'))  " _
                                                & " or    IMPIANTI.ID_EDIFICIO  in (select ID from SISCOM_MI.EDIFICI where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%'))  )"

                    End If
                End If
                '****************************************

                sStringaSql = sStringaSql & sStringaSqlPatrimonio

                griglia = 2
                'BindGrid(2)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DataGrid2_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid2.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
            End If
            If e.CommandName = RadGrid.FilterCommandName Then
                isFilter = True
            Else
                isFilter = False
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid2_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGrid2_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid2.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato Num. Reper.: " & dataItem("NUM_REPERTORIO").Text & " del complesso: " & dataItem("COMPLESSO").Text.Replace("'", "\'") & "';document.getElementById('txtIdLotto').value='" & dataItem("ID_LOTTO").Text & "';document.getElementById('txtIdEdificio').value='" & dataItem("ID_UBICAZIONE").Text & "';document.getElementById('txtIdAppalto').value='" & dataItem("ID_APPALTO").Text & "';document.getElementById('txtIdServizio').value='" & dataItem("ID_SERVIZIO").Text & "';document.getElementById('txtIdPF_VoceServizio').value='" & dataItem("ID_PF_VOCE_IMPORTO").Text & "'")
                e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
            End If
            'If isExporting.Value = "1" Then
            '    If e.Item.ItemIndex > 0 Then
            '        Dim context As RadProgressContext = RadProgressContext.Current
            '        If context.SecondaryTotal <> NumeroElementi Then
            '            context.SecondaryTotal = NumeroElementi
            '        End If
            '        context.SecondaryValue = e.Item.ItemIndex.ToString()
            '        context.SecondaryPercent = Int((e.Item.ItemIndex.ToString() * 100) / NumeroElementi)
            '        context.CurrentOperationText = "Export excel in corso"
            '        System.Threading.Thread.Sleep(50)
            '    End If
            'End If
        Catch ex As Exception

        End Try
    End Sub




    Protected Sub DataGrid2_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid2.NeedDataSource
        Try
            If griglia = 2 Then
                par.cmd.CommandText = sStringaSql
                Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                TryCast(sender, RadGrid).DataSource = dt
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " DataGrid2_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_PreRenderComplete(sender As Object, e As System.EventArgs) Handles Me.PreRenderComplete
        If isExporting.Value = "1" Then
            Dim context As RadProgressContext = RadProgressContext.Current
            context.CurrentOperationText = "Export in corso..."
            context("ProgressDone") = True
            context.OperationComplete = True
            context.SecondaryTotal = 0
            context.SecondaryValue = 0
            context.SecondaryPercent = 0
            isExporting.Value = "0"
        End If
    End Sub
    Private Sub EsportaExcel()
        isExporting.Value = "1"
        Dim context As RadProgressContext = RadProgressContext.Current
        context.SecondaryTotal = 0
        context.SecondaryValue = 0
        context.SecondaryPercent = 0
        DataGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx
        DataGrid1.ExportSettings.IgnorePaging = True
        DataGrid1.ExportSettings.ExportOnlyData = True
        DataGrid1.ExportSettings.OpenInNewWindow = True
        DataGrid1.MasterTableView.ExportToExcel()
    End Sub
    Protected Sub Esporta1_Click(sender As Object, e As System.EventArgs)
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
    Protected Sub Refresh1_Click(sender As Object, e As System.EventArgs)
        DataGrid1.Rebind()
    End Sub
    Protected Sub Esporta2_Click(sender As Object, e As System.EventArgs)
        DataGrid2.AllowPaging = False
        DataGrid2.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGrid2.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGrid2.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGrid2.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGrid2.Columns
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
    Protected Sub Refresh2_Click(sender As Object, e As System.EventArgs)
        DataGrid2.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "MANUTENZIONI", "MANUTENZIONI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub


    Protected Sub DataGrid1_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemCreated
        If TypeOf e.Item Is GridFilteringItem And DataGrid1.IsExporting Then
            e.Item.Visible = False
        End If
    End Sub

    Protected Sub DataGrid2_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid2.ItemCreated
        If TypeOf e.Item Is GridFilteringItem And DataGrid2.IsExporting Then
            e.Item.Visible = False
        End If
    End Sub

End Class
