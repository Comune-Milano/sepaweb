
Partial Class Contratti_DichiarazioneFO
    Inherits PageSetIdMode
    Dim sValorePG As String
    Dim sValoreCF As String
    Dim par As New CM.Global


    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Silver'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''};" _
                                  & "Selezionato=this;this.style.backgroundColor='red';" _
                                  & "document.getElementById('TextBox7').value='Hai selezionato: " & e.Item.Cells(par.IndDGC(DataGrid1, "PROGRESSIVO")).Text.ToString.Replace("'", "\'") _
                                  & " - " & e.Item.Cells(par.IndDGC(DataGrid1, "COGNOME")).Text.ToString.Replace("'", "\'") _
                                  & " " & e.Item.Cells(par.IndDGC(DataGrid1, "NOME")).Text.ToString.Replace("'", "\'") & "';" _
                                  & "document.getElementById('LBLID').value='" & e.Item.Cells(par.IndDGC(DataGrid1, "ID")).Text.ToString.Replace("'", "\'") & "';")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Public Property sStringaSQL() As String
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

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "DICHIARAZIONI_VSA,COMP_NUCLEO_VSA")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        'Label7.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        da.Dispose()
        ds.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessuna dichiarazione selezionata!')</script>")
        Else
            Response.Write("<script>document.location.href='CanoneFO.aspx?IDC=" & LBLID.Value & "&IDU=" & Unita & "';</script>")
        End If

    End Sub

    Public Property Unita() As String
        Get
            If Not (ViewState("par_Unita") Is Nothing) Then
                Return CStr(ViewState("par_Unita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Unita") = value
        End Set

    End Property

    Public Property CODICEUnita() As String
        Get
            If Not (ViewState("par_CODICEUnita") Is Nothing) Then
                Return CStr(ViewState("par_CODICEUnita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CODICEUnita") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Unita = Request.QueryString("U")
        CODICEUnita = Request.QueryString("CODICE")

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then

            Cerca()
        End If
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean




        bTrovato = False
        sStringaSql = ""


        'sStringaSQL = "SELECT DICHIARAZIONI_VSA.ID,COMP_NUCLEO_VSA.PROGR,DICHIARAZIONI_VSA.PG as progressivo," _
        '    & " replace(replace('<a href=£javascript:void(0)£ onclick=£today = new Date();window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?LE=1&CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione''+ today.getMinutes() + today.getSeconds());£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') AS PG," _
        '               & "/*DICHIARAZIONI_VSA.PG,*/TO_CHAR(TO_DATE(DICHIARAZIONI_VSA.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_PG""," _
        '               & "COMP_NUCLEO_VSA.COD_FISCALE," _
        '               & "COMP_NUCLEO_VSA.COGNOME,COMP_NUCLEO_VSA.NOME,COMP_NUCLEO_VSA.DATA_NASCITA " _
        '               & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA WHERE (DICHIARAZIONI_VSA.ISEE<>'' OR DICHIARAZIONI_VSA.ISEE IS NOT NULL) AND DICHIARAZIONI_VSA.TIPO=1 AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID " _
        '               & " AND DICHIARAZIONI_VSA.ID_STATO=1 AND comp_nucleo_VSA.progr=0 " _
        '               & " AND DICHIARAZIONI_VSA.ID NOT IN (SELECT ID_DICHIARAZIONE FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE IS NOT NULL) AND DICHIARAZIONI_VSA.ID NOT IN (SELECT ID_DICHIARAZIONE FROM SISCOM_MI.UNITA_ASSEGNATE WHERE PROVENIENZA='Z') " _
        '               & " ORDER BY DICHIARAZIONI_VSA.PG ASC,COMP_NUCLEO_VSA.PROGR ASC"

        sStringaSQL = " SELECT DICHIARAZIONI_VSA.ID," _
                        & " TIPO," _
                        & " COMP_NUCLEO_VSA.PROGR," _
                        & " DICHIARAZIONI_VSA.PG AS progressivo," _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£today = new Date();window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?LE=1&CH=2$ID='||DICHIARAZIONI_VSA.ID||''',''Dichiarazione''+ today.getMinutes() + today.getSeconds());£>'||DICHIARAZIONI_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') AS PG," _
                        & " TO_CHAR (TO_DATE (DICHIARAZIONI_VSA.DATA_PG, 'YYYYmmdd')," _
                        & " 'DD/MM/YYYY')" _
                        & " AS DATA_PG," _
                        & " COMP_NUCLEO_VSA.COD_FISCALE," _
                        & " COMP_NUCLEO_VSA.COGNOME," _
                        & " COMP_NUCLEO_VSA.NOME," _
                        & " COMP_NUCLEO_VSA.DATA_NASCITA," _
                        & " replace(replace('<img alt=£Aggiorna£ title=£Aggiorna Dichiarazione£ src=£../NuoveImm/Img_refresh_icon.png£ onclick=£window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=-1$T=3$FFOO=1$IDD='||DICHIARAZIONI_VSA.ID||'$CODU='||" & Unita & "||''',''AggiornaDich'',''top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'');£ style=£cursor: pointer£ />','$','&'),'£','" & Chr(34) & "') as AGGIORNA_DICH " _
                        & " FROM DICHIARAZIONI_VSA, COMP_NUCLEO_VSA" _
                        & " WHERE (DICHIARAZIONI_VSA.ISEE <> '' OR DICHIARAZIONI_VSA.ISEE IS NOT NULL)" _
                        & " AND DICHIARAZIONI_VSA.TIPO = 1" _
                        & " AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID" _
                        & " AND DICHIARAZIONI_VSA.ID_STATO = 1" _
                        & " AND comp_nucleo_VSA.progr = 0" _
                        & " AND DICHIARAZIONI_VSA.ID NOT IN (SELECT ID_DICHIARAZIONE" _
                        & " FROM DOMANDE_BANDO_VSA" _
                        & " WHERE ID_DICHIARAZIONE IS NOT NULL and id_motivo_domanda<>11)" _
                        & " AND DICHIARAZIONI_VSA.ID NOT IN (SELECT ID_DICHIARAZIONE" _
                        & " FROM SISCOM_MI.UNITA_ASSEGNATE" _
                        & " WHERE PROVENIENZA = 'Z')" _
                        & " AND DICHIARAZIONI_VSA.id =" _
                        & " (SELECT MAX (DICHIARAZIONI_VSA.id)" _
                        & " FROM DICHIARAZIONI_VSA, COMP_NUCLEO_VSA CV" _
                        & " WHERE (DICHIARAZIONI_VSA.ISEE <> ''" _
                        & " OR DICHIARAZIONI_VSA.ISEE IS NOT NULL)" _
                        & " AND DICHIARAZIONI_VSA.TIPO = 1" _
                        & " AND comp_nucleo_vsa.cod_fiscale = CV.cod_fiscale" _
                        & " AND CV.ID_DICHIARAZIONE = DICHIARAZIONI_VSA.ID" _
                        & " AND DICHIARAZIONI_VSA.ID_STATO = 1" _
                        & " AND CV.progr = 0" _
                        & " AND DICHIARAZIONI_VSA.ID NOT IN" _
                        & " (SELECT ID_DICHIARAZIONE" _
                        & " FROM DOMANDE_BANDO_VSA" _
                        & " WHERE ID_DICHIARAZIONE IS NOT NULL and id_motivo_domanda<>11)" _
                        & " AND DICHIARAZIONI_VSA.ID NOT IN" _
                        & " (SELECT ID_DICHIARAZIONE" _
                        & " FROM SISCOM_MI.UNITA_ASSEGNATE" _
                        & " WHERE PROVENIENZA = 'Z'))" _
                        & " ORDER BY DICHIARAZIONI_VSA.PG ASC, COMP_NUCLEO_VSA.PROGR ASC"

        BindGrid()
    End Function

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Add("CONTRATTOAPERTO", "0")
        Response.Write("<script>window.close();</script>")
    End Sub

    Private Sub btnRicarica_Click(sender As Object, e As EventArgs) Handles btnRicarica.Click
        Cerca()
        If LBLID.Value > 0 Then
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "select comp_nucleo_vsa.*,dichiarazioni_vsa.* from domande_bando_vsa,dichiarazioni_vsa,comp_nucleo_vsa where comp_nucleo_vsa.id_dichiarazione=dichiarazioni_vsa.id and progr=0 and domande_bando_vsa.id_dichiarazione=dichiarazioni_vsa.id and id_d_import=" & LBLID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                LBLID.Value = par.IfNull(myReader("id_dichiarazione"), -1)
                TextBox7.Text = "Hai selezionato: " & par.IfNull(myReader("PG"), -1) _
                                  & " - " & par.IfNull(myReader("COGNOME"), -1) _
                                  & " " & par.IfNull(myReader("NOME"), -1) & ""
            End If
            myReader.Close()
            par.OracleConn.Close()


        End If
    End Sub
    Protected Sub ImgIndietro_Click(sender As Object, e As ImageClickEventArgs) Handles ImgIndietro.Click
        Response.Write("<script>top.location.href='SelezionaUnitaFO.aspx';</script>")
    End Sub
End Class
