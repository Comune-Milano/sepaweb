'*** LISTA RISULTATO MOROSITA Proviene da : RicercaMorosita.aspx

Partial Class MOROSITA_RisultatiMorosita
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public sStringaSql As String

    Public sValoreStrutturaAler As String
    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreIndirizzo As String
    Public sValoreCivico As String

    Public sValoreCodice As String
    Public sValoreCognome As String
    Public sValoreNome As String

    Public sValoreTI As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sValoreData_Dal_P As String
    Public sValoreData_Al_P As String
    Public sValoreProtocollo As String

    Public sTipoRicerca As String

    Public sOrdinamento As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""Portale.aspx""</script>")
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()

            '*LBLID.Text = Request.QueryString("T")

            sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreIndirizzo = Request.QueryString("IN")
            sValoreCivico = Request.QueryString("CI")

            sValoreTI = UCase(Request.QueryString("TI"))

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreData_Dal_P = Request.QueryString("DAL_P")
            sValoreData_Al_P = Request.QueryString("AL_P")
            sValoreProtocollo = Request.QueryString("PRO")

            sValoreCodice = Request.QueryString("CD")
            sValoreCognome = Request.QueryString("CG")
            sValoreNome = Request.QueryString("NM")

            sTipoRicerca = Request.QueryString("MORA")

            sOrdinamento = Request.QueryString("ORD")


            Cerca()
            BindGrid()

        End If

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

    Private Sub Cerca()
        'Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String
        Dim sTipoLettera As String = ""


        sStringaSql = ""

        If sValoreCognome <> "" Then
            sValore = Strings.UCase(sValoreCognome)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            sStringaSql = "  SISCOM_MI.MOROSITA.ID in (" _
                                            & " select ID_MOROSITA from SISCOM_MI.MOROSITA_LETTERE " _
                                            & "  where ID_ANAGRAFICA in (select ID from SISCOM_MI.ANAGRAFICA " _
                                                                    & "  where ( COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' or RAGIONE_SOCIALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "') "

            If sValoreNome <> "" Then
                sStringaSql = sStringaSql & " and ANAGRAFICA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            sStringaSql = sStringaSql & "))"

        ElseIf sValoreNome <> "" Then
            sValore = Strings.UCase(sValoreNome)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If

            sStringaSql = "  SISCOM_MI.MOROSITA.ID in (" _
                                            & " select ID_MOROSITA from SISCOM_MI.MOROSITA_LETTERE " _
                                            & "  where ID_ANAGRAFICA in (select ID from SISCOM_MI.ANAGRAFICA " _
                                                                    & "  where NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "')) "
        End If


        If sValoreCodice <> "" Then
            sValore = Strings.UCase(sValoreCodice)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If

            If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

            sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                            & " select ID_MOROSITA from SISCOM_MI.MOROSITA_LETTERE " _
                                            & "  where COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "') "


        End If


        If par.IfEmpty(sValoreIndirizzo, "-1") <> "-1" Then

            If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

            sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                            & " select  ID_MOROSITA " _
                                            & " from    SISCOM_MI.MOROSITA_LETTERE " _
                                            & " where   ID_CONTRATTO in (" _
                                                    & " select  ID_CONTRATTO " _
                                                    & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                                                    & " where   ID_UNITA in (" _
                                                            & " select  ID " _
                                                            & " from    SISCOM_MI.UNITA_IMMOBILIARI " _
                                                            & " where   ID_INDIRIZZO in (" _
                                                                 & " select ID from SISCOM_MI.INDIRIZZI " _
                                                                         & " where DESCRIZIONE='" & par.PulisciStrSql(sValoreIndirizzo) & "'"




            If par.IfEmpty(sValoreCivico, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and CIVICO='" & par.PulisciStrSql(sValoreCivico) & "'))))"
            Else
                sStringaSql = sStringaSql & " )))) "
            End If

        ElseIf par.IfEmpty(sValoreEdificio, "-1") <> "-1" Then

            If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

            sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                            & " select  ID_MOROSITA " _
                                            & " from    SISCOM_MI.MOROSITA_LETTERE " _
                                            & " where   ID_CONTRATTO in (" _
                                                    & " select  ID_CONTRATTO " _
                                                    & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                                                    & " where   ID_UNITA in (" _
                                                            & " select  ID " _
                                                            & " from    SISCOM_MI.UNITA_IMMOBILIARI " _
                                                            & " where   ID_EDIFICIO=" & sValoreEdificio _
                                            & "))) "
                                                                

        ElseIf par.IfEmpty(sValoreComplesso, "-1") <> "-1" Then

            If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

            sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                            & " select  ID_MOROSITA " _
                                            & " from    SISCOM_MI.MOROSITA_LETTERE " _
                                            & " where   ID_CONTRATTO in (" _
                                                    & " select  ID_CONTRATTO " _
                                                    & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                                                    & " where   ID_UNITA in (" _
                                                            & " select  ID " _
                                                            & " from    SISCOM_MI.UNITA_IMMOBILIARI " _
                                                            & " where   ID_EDIFICIO in (" _
                                                                    & " select  ID " _
                                                                    & " from    SISCOM_MI.EDIFICI " _
                                                                    & " where   ID_COMPLESSO=" & sValoreComplesso _
                                            & " )))) "

        ElseIf par.IfEmpty(sValoreStrutturaAler, "-1") <> "-1" Then
            sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                            & " select  ID_MOROSITA " _
                                            & " from    SISCOM_MI.MOROSITA_LETTERE " _
                                            & " where   ID_CONTRATTO in (" _
                                                    & " select  ID_CONTRATTO " _
                                                    & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                                                    & " where   ID_UNITA in (" _
                                                            & " select  ID " _
                                                            & " from    SISCOM_MI.UNITA_IMMOBILIARI " _
                                                            & " where   ID_EDIFICIO in (" _
                                                                    & " select  ID " _
                                                                    & " from    SISCOM_MI.EDIFICI " _
                                                                    & " where   ID_COMPLESSO IN (" _
                                                                            & " select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                            & " where ID_FILIALE=" & sValoreStrutturaAler & ")" _
                                            & " )))) "

        End If


        If sValoreData_Dal <> "" Then
            If sValoreData_Al <> "" Then

                If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "
                sStringaSql = sStringaSql & " RIF_DA>=" & sValoreData_Dal & " and RIF_A<=" & sValoreData_Al

            Else

                If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "
                sStringaSql = sStringaSql & " RIF_DA>=" & sValoreData_Dal

            End If
        ElseIf sValoreData_Al <> "" Then

            If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "
            sStringaSql = sStringaSql & " RIF_A<=" & sValoreData_Al

        End If



        If sValoreTI <> "" Then

            If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

            sStringaSql = sStringaSql & "  SISCOM_MI.MOROSITA.ID in (" _
                                            & " select  ID_MOROSITA " _
                                            & " from    SISCOM_MI.MOROSITA_LETTERE " _
                                            & " where   ID_CONTRATTO in (" _
                                                    & " select  ID_CONTRATTO " _
                                                    & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                                                    & " where   TIPOLOGIA='" & par.PulisciStrSql(sValoreTI) & "' " _
                                            & " )) "

        End If


        '*** PROTOCOLLO 
        If sValoreProtocollo <> "" Then
            sValore = Strings.UCase(sValoreProtocollo)
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If

            If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "
            sStringaSql = sStringaSql & "  UPPER(MOROSITA.PROTOCOLLO_ALER) " & sCompara & " '" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If
        '********************************

        '*** DATA_PROTOCOLLO 
        If sValoreData_Dal_P <> "" Then
            If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

            sStringaSql = sStringaSql & " MOROSITA.DATA_PROTOCOLLO>='" & sValoreData_Dal_P & "' "
        End If

        If sValoreData_Al_P <> "" Then
            If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "

            sStringaSql = sStringaSql & " MOROSITA.DATA_PROTOCOLLO<='" & sValoreData_Al_P & "' "
        End If
        '********************************

        If Strings.Mid(sTipoRicerca, 1, 1) = "1" Then
            If sTipoLettera = "" Then
                sTipoLettera = "'AB'"
            Else
                sTipoLettera = sTipoLettera & ",'AB'"
            End If
        End If

        If Strings.Mid(sTipoRicerca, 2, 1) = "1" Then
            If sTipoLettera = "" Then
                sTipoLettera = "'CD'"
            Else
                sTipoLettera = sTipoLettera & ",'CD'"
            End If
        End If

        If Strings.Mid(sTipoRicerca, 3, 1) = "1" Then
            If sTipoLettera = "" Then
                sTipoLettera = "'EF'"
            Else
                sTipoLettera = sTipoLettera & ",'EF'"
            End If
        End If
        If sTipoLettera <> "" Then
            If sStringaSql <> "" Then sStringaSql = sStringaSql & " and "
            sStringaSql = sStringaSql & " ID in (select ID_MOROSITA from SISCOM_MI.MOROSITA_LETTERE where TIPO_LETTERA in (" & sTipoLettera

            If Strings.Mid(sTipoRicerca, 1, 1) = "1" Then
                sStringaSql = sStringaSql & ") or TIPO_LETTERA is null) "
            Else
                sStringaSql = sStringaSql & ") ) "
            End If
        End If


        '        Response.Write("<script>window.showModalDialog('CreaLettere2.aspx?IDMOR=" & vIdMorosita & "',window, 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');</script>")


        sStringaSQL1 = "select  morosita.fl_annullata,MOROSITA.PROGR ,MOROSITA.ID as ID_MOROSITA,MOROSITA.PROTOCOLLO_ALER," _
                            & " to_char(to_date(substr(MOROSITA.DATA_PROTOCOLLO,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_PROTOCOLLO," _
                            & " MOROSITA.TIPO_INVIO," _
                            & " to_char(to_date(substr(MOROSITA.RIF_DA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_DAL," _
                            & " to_char(to_date(substr(MOROSITA.RIF_A,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_AL," _
                            & "NOTE,MOROSITA.DATA_PROTOCOLLO as ""DATA_PROTOCOLLO_ORD""," _
                            & " (case when fl_annullata = 0 then replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''VisualizzaFileZIP.aspx?IDMOR='||MOROSITA.ID||''',''Stampa'',''height=550,top=0,left=0,width=800'');£>Visualizza File</a>','$','&'),'£','" & Chr(34) & "') else '' end)as FILE_MOROSITA " _
                        & " from  " _
                        & " SISCOM_MI.MOROSITA"


        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " where " & sStringaSql
        End If


        sStringaSQL1 = sStringaSQL1 & " ORDER BY " & sOrdinamento



    End Sub

    Private Sub BindGrid()

        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds) ', "DOMANDE_BANDO,COMP_NUCLEO")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            For Each di As DataGridItem In DataGrid1.Items
                If di.Cells(par.IndDGC(DataGrid1, "FL_ANNULLATA")).Text = 1 Then
                    di.Font.Strikeout = True
                    For i = 1 To di.Cells.Count - 1
                        di.Cells(i).Font.Strikeout = True
                    Next
                End If
            Next


            Label1.Text = " " & ds.Tables(0).Rows.Count


            da.Dispose()
            ds.Dispose()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la morosità con protocollo numero: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la morosità con protocollo numero: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            'Session.Add("ID", txtid.Text)

            sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreIndirizzo = Request.QueryString("IN")
            sValoreCivico = Request.QueryString("CI")

            sValoreTI = UCase(Request.QueryString("TI"))

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")


            sValoreData_Dal_P = Request.QueryString("DAL_P")
            sValoreData_Al_P = Request.QueryString("AL_P")
            sValoreProtocollo = Request.QueryString("PRO")

            sValoreCodice = Request.QueryString("CD")
            sValoreCognome = Request.QueryString("CG")
            sValoreNome = Request.QueryString("NM")

            sOrdinamento = Request.QueryString("ORD")


            Response.Write("<script>location.replace('Morosita.aspx?ID=" & Me.txtid.Text _
                                                                & "&FI=" & sValoreStrutturaAler _
                                                                & "&CO=" & sValoreComplesso _
                                                                & "&ED=" & sValoreEdificio _
                                                                & "&TI=" & sValoreTI _
                                                                & "&IN=" & par.VaroleDaPassare(sValoreIndirizzo) _
                                                                & "&CI=" & par.VaroleDaPassare(sValoreCivico) _
                                                               & "&DAL=" & sValoreData_Dal _
                                                                & "&AL=" & sValoreData_Al _
                                                                & "&CD=" & par.VaroleDaPassare(sValoreCodice) _
                                                                & "&CG=" & par.VaroleDaPassare(sValoreCognome) _
                                                                & "&NM=" & par.VaroleDaPassare(sValoreNome) _
                                                                & "&DAL_P=" & sValoreData_Dal_P _
                                                                & "&AL_P=" & sValoreData_Al_P _
                                                                & "&PRO=" & par.VaroleDaPassare(sValoreProtocollo) _
                                                                & "&ORD=" & sOrdinamento _
                                                    & "');</script>")

        End If

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click

        Response.Write("<script>document.location.href=""RicercaMorosita.aspx""</script>")

    End Sub

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Dim scriptblock As String

        Try

            sValoreStrutturaAler = Request.QueryString("FI")    'FILIALE (STRUTTURA)

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreIndirizzo = Request.QueryString("IN")
            sValoreCivico = Request.QueryString("CI")

            sValoreTI = UCase(Request.QueryString("TI"))

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreData_Dal_P = Request.QueryString("DAL_P")
            sValoreData_Al_P = Request.QueryString("AL_P")
            sValoreProtocollo = Request.QueryString("PRO")


            sValoreCodice = Request.QueryString("CD")
            sValoreCognome = Request.QueryString("CG")
            sValoreNome = Request.QueryString("NM")

            sOrdinamento = Request.QueryString("ORD")


            'btnStampa.Attributes.Add("OnClick", "javascript:window.open('Report/ReportMorosita.aspx?CO=" & sValoreComplesso _
            '                                                                                    & "&FI=" & sValoreStrutturaAler _
            '                                                                                    & "&ED=" & sValoreEdificio _
            '                                                                                    & "&TI=" & sValoreTI _
            '                                                                                    & "&IN=" & sValoreIndirizzo _
            '                                                                                    & "&CI=" & sValoreCivico _
            '                                                                                   & "&DAL=" & sValoreData_Dal _
            '                                                                                    & "&AL=" & sValoreData_Al _
            '                                                                                    & "&CD=" & sValoreCodice _
            '                                                                                    & "&CG=" & sValoreCognome _
            '                                                                                    & "&NM=" & sValoreNome _
            '                                                                                   & "&DAL_P=" & sValoreData_Dal_P _
            '                                                                                    & "&AL_P=" & sValoreData_Al_P _
            '                                                                                    & "&PRO=" & sValoreProtocollo _
            '                                                                                    & "&ORD=" & sOrdinamento _
            '                                                                            & "');")

            scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "window.open('Report/ReportMorosita.aspx?CO=" & sValoreComplesso _
                                                                & "&FI=" & sValoreStrutturaAler _
                                                                & "&ED=" & sValoreEdificio _
                                                                & "&TI=" & sValoreTI _
                                                                & "&IN=" & sValoreIndirizzo _
                                                                & "&CI=" & sValoreCivico _
                                                               & "&DAL=" & sValoreData_Dal _
                                                                & "&AL=" & sValoreData_Al _
                                                                & "&CD=" & sValoreCodice _
                                                                & "&CG=" & sValoreCognome _
                                                                & "&NM=" & sValoreNome _
                                                               & "&DAL_P=" & sValoreData_Dal_P _
                                                                & "&AL_P=" & sValoreData_Al_P _
                                                                & "&PRO=" & sValoreProtocollo _
                                                                & "&ORD=" & sOrdinamento _
                                                                & "','Report');" _
                        & "</script>"



            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

End Class
