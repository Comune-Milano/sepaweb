
Partial Class ASS_RisultatoRicOfferta
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreOF As String
    Dim sValoreCR As String
    Dim sStringaSql As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            sValoreOF = Request.QueryString("OF")
            sValoreCR = par.DeCripta(Request.QueryString("CR"))
            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Value = "-1"
            Cerca()
        End If
    End Sub

    Private Sub Cerca()
        'Dim bTrovato As Boolean
        'Dim sValore As String
        'Dim sCompara As String

        'bTrovato = False
        'sStringaSql = ""
        'sStringaSQL1 = ""

        'If sValoreOF <> "" Then
        '    sValore = sValoreOF
        '    bTrovato = True
        '    sStringaSql = "  DOMANDE_OFFERTE_SCAD.ID =" & par.PulisciStrSql(sValore)
        '    sStringaSQL1 = sStringaSql & " AND "
        'End If
        'If sStringaSql <> "" Then sStringaSql = " WHERE " & sStringaSql
        'Try


        '    par.OracleConn.Open()
        '    Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("select * from DOMANDE_OFFERTE_SCAD  " & sStringaSql, par.OracleConn)
        '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        '    If myReader.Read() Then
        '        If myReader("id_domanda") < 500000 Then
        '            sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(rel_prat_all_ccaa_erp.data_proposta,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_OFFERTA"",TO_CHAR(TO_DATE(rel_prat_all_ccaa_erp.data,'YYYYmmdd'),'DD/MM/YYYY') as ""data_accettazione"",TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
        '                           & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
        '                           & " FROM rel_prat_all_ccaa_erp,domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
        '                           & " WHERE " & sStringaSQL1 & "  DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='0' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
        '                           & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda and " _
        '                           & "DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
        '                           & " DOMANDE_BANDO.FL_INVITO='1' AND DOMANDE_BANDO.ID_STATO<>'10' AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
        '                           & " and rel_prat_all_ccaa_erp.id_pratica=domande_offerte_scad.id_domanda and rel_prat_all_ccaa_erp.esito='1' and rel_prat_all_ccaa_erp.ultimo='1'" _
        '                           & "AND (DOMANDE_BANDO.ID_STATO='9')  AND DOMANDE_BANDO.ID IN (SELECT ID_DOMANDA FROM SISCOM_MI.UNITA_ASSEGNATE WHERE GENERATO_CONTRATTO=0)  ORDER BY COMP_NUCLEO.COGNOME ASC,COMP_NUCLEO.NOME ASC "
        '        Else
        '            sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(rel_prat_all_ccaa_erp.data_proposta,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_OFFERTA"",TO_CHAR(TO_DATE(rel_prat_all_ccaa_erp.data,'YYYYmmdd'),'DD/MM/YYYY') as ""data_accettazione"",TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_cambi.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_cambi.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_cambi.posizione,DOMANDE_BANDO_cambi.ID,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME," _
        '           & "DOMANDE_BANDO_cambi.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
        '           & " FROM rel_prat_all_ccaa_erp,domande_offerte_scad,bandi_graduatoria_def_cambi,DOMANDE_BANDO_cambi,COMP_NUCLEO_cambi,DICHIARAZIONI_cambi " _
        '           & " WHERE " & sStringaSQL1 & "  DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='0' AND DOMANDE_BANDO_cambi.PROGR_COMPONENTE=COMP_NUCLEO_cambi.PROGR AND DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID " _
        '           & " AND  domande_bando_cambi.id = bandi_graduatoria_def_cambi.id_domanda and " _
        '           & "DOMANDE_BANDO_cambi.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
        '           & " DOMANDE_BANDO_cambi.FL_INVITO='1' AND DOMANDE_BANDO_cambi.ID_STATO<>'10' AND DOMANDE_BANDO_cambi.FL_PRATICA_CHIUSA<>'1' " _
        '           & " and rel_prat_all_ccaa_erp.id_pratica=domande_offerte_scad.id_domanda and rel_prat_all_ccaa_erp.esito='1' and rel_prat_all_ccaa_erp.ultimo='1'" _
        '           & "AND (DOMANDE_BANDO_cambi.ID_STATO='9')  AND DOMANDE_BANDO_CAMBI.ID IN (SELECT ID_DOMANDA FROM SISCOM_MI.UNITA_ASSEGNATE WHERE GENERATO_CONTRATTO=0) ORDER BY COMP_NUCLEO_CAMBI.COGNOME ASC,COMP_NUCLEO_CAMBI.NOME ASC"
        '        End If
        '    End If
        '    myReader.Close()

        '    'cmd.CommandText = sStringaSQL1
        '    'myReader = cmd.ExecuteReader()
        '    'Label3.Text = "0"
        '    'Do While myReader.Read()
        '    '    Label3.Text = CInt(Label3.Text) + 1
        '    'Loop
        '    'Label3.Text = Label3.Text
        '    'cmd.Dispose()
        '    'myReader.Close()
        '    par.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Dim Ms_Stringa As String
        'Dim Ms_Stringa1 As String
        Dim m As Boolean

        m = False

        Ms_Stringa = ""

        If sValoreOF <> "" Then
            Ms_Stringa = " AND unita_assegnate.n_offerta='" & par.PulisciStrSql(sValoreOF) & "' "
            m = True
        End If

        If sValoreCR <> "" Then
            If InStr(sValoreCR, "*") > 0 Then
                sValoreCR = Replace(sValoreCR, "*", "%")
                Ms_Stringa = Ms_Stringa & " AND unita_assegnate.COGNOME_RS LIKE '" & par.PulisciStrSql(sValoreCR) & "' "
            Else
                Ms_Stringa = Ms_Stringa & " AND unita_assegnate.COGNOME_RS='" & par.PulisciStrSql(sValoreCR) & "' "
            End If
            m = True
        End If

        'sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(UNITA_ASSEGNATE.DATA_assegnazione,'YYYYmmdd'),'DD/MM/YYYY') AS ""data_assegnazione"",UNITA_ASSEGNATE.n_offerta,unita_assegnate.cognome_rs,unita_assegnate.nomE,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,unita_assegnate.id_unita,unita_assegnate.cf_piva,UNITA_ASSEGNATE.ID_DOMANDA from SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.unita_assegnate where UNITA_ASSEGNATE.ID_UNITA=UNITA_IMMOBILIARI.ID (+) AND generato_contratto='0' " & Ms_Stringa _
        '               & " ORDER BY data_assegnazione desc"


        sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(UNITA_ASSEGNATE.DATA_assegnazione,'YYYYmmdd'),'DD/MM/YYYY') AS data_assegnazione," _
                    & "UNITA_ASSEGNATE.n_offerta,unita_assegnate.cognome_rs,unita_assegnate.nomE,unita_assegnate.id_unita," _
                    & "unita_assegnate.cf_piva,UNITA_ASSEGNATE.ID_DOMANDA,(CASE WHEN UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE is not null THEN UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ELSE ALLOGGI.COD_ALLOGGIO END) AS COD_UNITA_IMMOBILIARE " _
                    & "from SISCOM_MI.UNITA_IMMOBILIARI, siscom_mi.unita_assegnate, ALLOGGI where UNITA_ASSEGNATE.ID_UNITA=ALLOGGI.ID (+) AND UNITA_ASSEGNATE.ID_UNITA=UNITA_IMMOBILIARI.ID (+) AND generato_contratto='0' " & Ms_Stringa & " ORDER BY data_assegnazione desc"

        BindGrid()
        'Catch ex As Exception
        '    par.OracleConn.Close()
        '    Response.Write(ex.Message)
        'End Try
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

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
    e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato: Offerta " & e.Item.Cells(0).Text & " Alloggio:" & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('lblIdUnita').value='" & e.Item.Cells(1).Text & "';document.getElementById('LBLPROGR').value='" & e.Item.Cells(2).Text & "';document.getElementById('CFPIVA').value='" & e.Item.Cells(4).Text & "';")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaAss.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
        Else
            Response.Write("<script>location.replace('ProvvedimentoAssegnazione.aspx?CF=" & CFPIVA.Value & "&ID=" & LBLPROGR.Value & "&OF=" & LBLID.Value & "&SC=" & lblIdUnita.Value & "');</script>")

            'Response.Write("<script>window.open('ProvvAssERP.aspx?ID=" & LBLID.Text & "&OF=" & LBLPROGR.Text & "&SC=" & lblScad.Text & "','Provvedimento','');</script>")
        End If
    End Sub



    'Protected Sub DataGrid1_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
    '    LBLPROGR.Text = e.Item.Cells(2).Text
    '    lblIdUnita.Text = e.Item.Cells(1).Text
    '    LBLID.Text = e.Item.Cells(0).Text
    '    Label2.Text = "Hai selezionato: Offerta " & e.Item.Cells(0).Text & " Alloggio:" & e.Item.Cells(1).Text
    'End Sub


End Class