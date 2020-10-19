'*** LISTA RISULTATO PAGAMENTI da ANNULLARE (NON PIU' UTILIZZATO)


Partial Class MANUTENAZIONI_RisultatiPagamentiAnnulla
    Inherits PageSetIdMode

    Dim par As New CM.Global


    Public sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String

    Public sValoreFornitore As String
    Public sValoreAppalto As String
    Public sValoreServizio As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sValoreStato As String

    Public sOrdinamento As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            '*LBLID.Text = Request.QueryString("T")

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreServizio = Request.QueryString("SV")

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            'sValoreStato = Request.QueryString("ST")


            BindGrid()

        End If


    End Sub

    Private Sub BindGrid()

        Try


            par.OracleConn.Open()



            'sOrdinamento = Request.QueryString("ORD")
            sOrder = " order by DATA_PRENOTAZIONE desc"
            'Select Case sOrdinamento
            '    Case "COMPLESSO"
            '        sOrder = " order by ODL desc,ANNO desc,UBICAZIONE asc"
            '    Case "EDIFICIO"
            '        sOrder = " order by ODL desc,ANNO desc,UBICAZIONE asc"
            '    Case "SERVIZIO"
            '        sOrder = " order by ODL desc,ANNO desc,SERVIZIO asc"

            '    Case Else
            '        sOrder = ""
            'End Select

            'STATO PAGAMENTO    0=PRENOTATO 1=EMESSO 5=PAGATO

            '& " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_PRENOTAZIONE"","
            '& " TRIM(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_PRENOTATO,'9G999G990D99')) AS ""IMPORTO_PRENOTATO"", " 

            sStringaSql = " select SISCOM_MI.PAGAMENTI.ID,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""PROG_ANNO"",''as ""DATA_PRENOTAZIONE""," _
                                 & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_EMISSIONE""," _
                                 & " SISCOM_MI.FORNITORI.RAGIONE_SOCIALE||' - '|| SISCOM_MI.FORNITORI.NOME||' '|| SISCOM_MI.FORNITORI.COGNOME AS ""BENEFICIARIO""," _
                                 & " SISCOM_MI.PAGAMENTI.DESCRIZIONE,'' as  ""IMPORTO_PRENOTATO""," _
                                 & " TRIM(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_CONSUNTIVATO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUNTIVATO""," _
                                 & " TAB_STATI_PAGAMENTI.DESCRIZIONE AS ""STATO""," _
                                 & " SISCOM_MI.PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                         & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI,SISCOM_MI.TAB_STATI_PAGAMENTI,SISCOM_MI.PRENOTAZIONI " _
                         & " where SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=3 " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+) " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=1 " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=SISCOM_MI.TAB_STATI_PAGAMENTI.ID (+) " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID=SISCOM_MI.PRENOTAZIONI.ID_PAGAMENTO (+) "


            'If par.IfEmpty(sValoreStato, -1) <> "-1" Then
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_STATO=" & sValoreStato
            'End If

            'If sValoreFornitore <> "-1" Then
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=" & sValoreFornitore
            'End If


            'If sValoreAppalto <> "-1" Then
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_APPALTO=" & sValoreAppalto
            'End If

            'If sValoreServizio <> "-1" Then
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_VOCE_PF=" & sValoreServizio
            'End If

            'If sValoreDataP_Dal <> "" Then
            '    sCompara = " >= "
            '    sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.DATA_PRENOTAZIONE" & sCompara & " '" & sValoreDataP_Dal & "' "
            'End If

            'If sValoreDataP_Al <> "" Then
            '    sCompara = " <= "
            '    sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.DATA_PRENOTAZIONE" & sCompara & " '" & sValoreDataP_Al & "' "
            'End If

            'If sValoreDataE_Dal <> "" Then
            '    sCompara = " >= "
            '    sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.DATA_EMISSIONE" & sCompara & " '" & sValoreDataE_Dal & "' "
            'End If

            'If sValoreDataE_Al <> "" Then
            '    sCompara = " <= "
            '    sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.DATA_EMISSIONE" & sCompara & " '" & sValoreDataE_Al & "' "
            'End If


            sStringaSql = sStringaSql & sOrder


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




    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdVoce').value='" & e.Item.Cells(9).Text & "'")

            ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdVoce').value='" & e.Item.Cells(9).Text & "'")

            ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
            ';document.getElementById('txtImpianto').value='" & e.Item.Cells(2).Text & "'"
        End If

    End Sub


    Protected Sub btnAnnullaPagamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnullaPagamento.Click

        Try

            If Me.txtid.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
            Else


                If Me.txtElimina.Value = "1" Then

                    '*******************APERURA CONNESSIONE*********************
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If

                    par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set ID_PAGAMENTO=Null where ID_PAGAMENTO=" & Me.txtid.Text

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""


                    'UPDATE PRENOTAZIONI
                    par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=1, ID_PAGAMENTO=Null where ID_PAGAMENTO=" & Me.txtid.Text
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    par.cmd.CommandText = "delete from SISCOM_MI.PAGAMENTI where ID=" & Me.txtid.Text
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    BindGrid()

                End If
            End If



        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub



End Class
