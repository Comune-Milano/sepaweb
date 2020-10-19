'*** LISTA RISULTATO ALLOGGI SFITTI

Partial Class MANUTENZIONI_RicercaSfitti
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            '*LBLID.Text = Request.QueryString("T")


            sOrder = ""

            sStringaSql = "select SISCOM_MI.UNITA_IMMOBILIARI.ID as ID_UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO as EDIFICIO," _
                               & "SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA," _
                               & "SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE AS  PIANO," _
                               & "SISCOM_MI.UNITA_IMMOBILIARI.INTERNO," _
                               & "SISCOM_MI.IDENTIFICATIVI_CATASTALI.SUB as NOME_SUB,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE as TIPOLOGIA_UNITA " _
                      & " from  SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI," _
                            & " SISCOM_MI.EDIFICI,SISCOM_MI.IDENTIFICATIVI_CATASTALI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI " _
                      & " where  SISCOM_MI.UNITA_IMMOBILIARI.ID in (select ID_UNITA from  SISCOM_MI.V_ALLOGGI_SFITTI where ID_MANUTENZIONE is null) " _
                      & "   and  SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO            =SISCOM_MI.EDIFICI.ID (+)  " _
                      & "	and  SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO =SISCOM_MI.TIPO_LIVELLO_PIANO.COD  " _
                      & "	and  SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA               =SISCOM_MI.SCALE_EDIFICI.ID  " _
                      & "	and  SISCOM_MI.IDENTIFICATIVI_CATASTALI.ID (+)          =SISCOM_MI.UNITA_IMMOBILIARI.ID_CATASTALE " _
                      & "   and  SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPOLOGIA          =SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD "



            sStringaSql = sStringaSql & sOrder

            BindGrid()

        End If


    End Sub

    Private Sub BindGrid()

        Try


            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds) ', "DOMANDE_BANDO,COMP_NUCLEO")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label1.Text = " " & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""../../Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            'Session.Add("ID", txtid.Text)


            'Response.Write("<script>location.replace('Manutenzioni.aspx?TIPOR=4" & "&PROVENIENZA=RISULTATI_SFITTI');</script>")

            Response.Write("<script>location.replace('RisultatiSfitti.aspx?ID=" & txtid.Text & "');</script>")


        End If

    End Sub



    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'alloggio: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

            ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'alloggio: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

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
        Response.Write("<script>document.location.href=""RicercaManutenzioni.aspx""</script>")
    End Sub


    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        ' Dim scriptblock As String

        'BindExcel()
        'Exit Sub

        Try


            ''Response.Write("<script>window.open('Report/ReportRisultatoImpianti.aspx?IMP1=1,&Pas='" & Session.Item("IMP2") & "');</script>")


            ''Response.Write("<script>location.replace('RisultatiImpianti.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "');</script>")

            'btnStampa.Attributes.Add("OnClick", "javascript:window.open('Report/ReportRisultatoManu.aspx?FI=" & sValoreStruttura & "&LO=" & sValoreLotto & "&CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&SE=" & sValoreServizio & "&ST=" & sValoreStato & "&ORD=" & sOrdinamento & "');")

            'scriptblock = "<script language='javascript' type='text/javascript'>" _
            '& "window.open('Report/ReportRisultatoManu.aspx?FI=" & sValoreStruttura & "&LO=" & sValoreLotto & "&CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&SE=" & sValoreServizio & "&ST=" & sValoreStato & "&ORD=" & sOrdinamento & "','Report');" _
            '& "</script>"
            'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
            '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
            'End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub




    Sub BindExcel()

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

        'dg.DataSource = DataGrid1 ' ds.Tables(0)
        'dg.DataBind()
        'dg.RenderControl(htmlWrite)
        'Response.Write(stringWrite.ToString())
        'Response.End()

    End Sub




End Class
