
Partial Class ASS_RisultatoRicOffertaCT
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    Dim sValoreOF As String
    Dim sStringaSql As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            sValoreOF = Request.QueryString("OF")
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")

            LBLID.Text = "-1"
            Cerca()
        End If
    End Sub

    Private Sub Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String


        bTrovato = False
        sStringaSql = ""
        sStringaSQL1 = ""

        If sValoreOF <> "" Then
            sValore = sValoreOF
            bTrovato = True
            sStringaSql = "  DOMANDE_OFFERTE_SCAD.ID =" & par.PulisciStrSql(sValore)
            sStringaSQL1 = sStringaSql & " AND "
        End If
        If sStringaSql <> "" Then sStringaSql = " WHERE " & sStringaSql

        Try


            par.OracleConn.Open()
            Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("select * from DOMANDE_OFFERTE_SCAD  " & sStringaSql, par.OracleConn)
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader("id_domanda") < 500000 Then
                    sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(rel_prat_all_ccaa_erp.data_proposta,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_OFFERTA"",TO_CHAR(TO_DATE(rel_prat_all_ccaa_erp.data,'YYYYmmdd'),'DD/MM/YYYY') as ""data_accettazione"",TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
                                   & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                                   & " FROM rel_prat_all_ccaa_erp,domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI,ALLOGGI " _
                                   & " WHERE  DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND REL_PRAT_ALL_CCAA_ERP.ID_ALLOGGIO=ALLOGGI.ID AND ALLOGGI.PROPRIETA<>0 AND " & sStringaSQL1 & " domande_offerte_scad.FL_VALIDA='0' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
                                   & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda and " _
                                   & "DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
                                   & " DOMANDE_BANDO.FL_INVITO='1' AND DOMANDE_BANDO.ID_STATO<>'10' AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
                                   & " and rel_prat_all_ccaa_erp.id_pratica=domande_offerte_scad.id_domanda and rel_prat_all_ccaa_erp.esito='1' and rel_prat_all_ccaa_erp.ultimo='1'" _
                                   & "AND (DOMANDE_BANDO.ID_STATO='9') AND DOMANDE_OFFERTE_SCAD.ID IN (SELECT N_OFFERTA FROM SISCOM_MI.unita_assegnate WHERE PROVENIENZA='G') ORDER BY COMP_NUCLEO.COGNOME ASC,COMP_NUCLEO.NOME ASC "
                Else
                    sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(rel_prat_all_ccaa_erp.data_proposta,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_OFFERTA"",TO_CHAR(TO_DATE(rel_prat_all_ccaa_erp.data,'YYYYmmdd'),'DD/MM/YYYY') as ""data_accettazione"",TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_cambi.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_cambi.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_cambi.posizione,DOMANDE_BANDO_cambi.ID,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME," _
                   & "DOMANDE_BANDO_cambi.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                   & " FROM rel_prat_all_ccaa_erp,domande_offerte_scad,bandi_graduatoria_def_cambi,DOMANDE_BANDO_cambi,COMP_NUCLEO_cambi,DICHIARAZIONI_cambi " _
                   & " WHERE " & sStringaSQL1 & "  DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='0' AND DOMANDE_BANDO_cambi.PROGR_COMPONENTE=COMP_NUCLEO_cambi.PROGR AND DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID " _
                   & " AND  domande_bando_cambi.id = bandi_graduatoria_def_cambi.id_domanda and " _
                   & "DOMANDE_BANDO_cambi.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
                   & " DOMANDE_BANDO_cambi.FL_INVITO='1' AND DOMANDE_BANDO_cambi.ID_STATO<>'10' AND DOMANDE_BANDO_cambi.FL_PRATICA_CHIUSA<>'1' " _
                   & " and rel_prat_all_ccaa_erp.id_pratica=domande_offerte_scad.id_domanda and rel_prat_all_ccaa_erp.esito='1' and rel_prat_all_ccaa_erp.ultimo='1'" _
                   & "AND (DOMANDE_BANDO_cambi.ID_STATO='9')  AND DOMANDE_OFFERTE_SCAD.ID IN (SELECT N_OFFERTA FROM SISCOM_MI.unita_assegnate WHERE PROVENIENZA='G') ORDER BY COMP_NUCLEO_CAMBI.COGNOME ASC,COMP_NUCLEO_CAMBI.NOME ASC"
                End If
            End If
            myReader.Close()

            'sStringaSQL1 = sStringaSQL1 & " ORDER BY DOMANDE_OFFERTE_SCAD.ID ASC"
            'cmd.CommandText = sStringaSQL1
            'myReader = cmd.ExecuteReader()
            'Label3.Text = "0"
            'Do While myReader.Read()
            '    Label3.Text = CInt(Label3.Text) + 1
            'Loop
            'Label3.Text = Label3.Text
            cmd.Dispose()
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            BindGrid()
        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
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

    Private Sub BindGrid()
        Try


            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        LBLPROGR.Text = e.Item.Cells(2).Text
        lblScad.Text = e.Item.Cells(3).Text
        lblAcc.Text = e.Item.Cells(4).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub


    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaOffertaCT.aspx""</script>")
    End Sub


    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Text = "-1" Or LBLID.Text = "" Then
            Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
        Else
            Response.Write("<script>location.replace('Contratto.aspx?ID=" & LBLID.Text & "&OF=" & LBLPROGR.Text & "&SC=" & lblScad.Text & "&ACC=" & lblAcc.Text & "');</script>")
        End If
    End Sub
End Class
