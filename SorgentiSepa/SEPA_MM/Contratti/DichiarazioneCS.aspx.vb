
Partial Class Contratti_DichiarazioneFO
    Inherits PageSetIdMode
    Dim sValorePG As String
    Dim sValoreCF As String
    Dim par As New CM.Global

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or
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
            Response.Write("<script>document.location.href='CanoneCS.aspx?IDC=" & LBLID.Value & "&IDU=" & Unita & "';</script>")
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

            LBLID.Value = "-1"
            Cerca()
        End If
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean




        bTrovato = False
        sStringaSql = ""


        sStringaSQL = "SELECT dichiarazioni_vsa.ID,COMP_NUCLEO_VSA.PROGR," _
                        & " Dichiarazioni_VSA.PG AS progressivo," _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£today = new Date();window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?LE=1&CH=2$ID='||DOMANDE_BANDO_VSA.ID_DICHIARAZIONE||''',''Dichiarazione''+ today.getMinutes() + today.getSeconds());£>'||Dichiarazioni_VSA.PG||'</a>','$','&'),'£','" & Chr(34) & "') AS PG," _
                    & "TO_CHAR(TO_DATE(DOMANDE_BANDO_VSA.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_PG, " _
                    & "COMP_NUCLEO_VSA.COD_FISCALE, COMP_NUCLEO_VSA.COGNOME, COMP_NUCLEO_VSA.NOME, COMP_NUCLEO_VSA.DATA_NASCITA, " _
                    & " replace(replace('<img alt=£Aggiorna£ title=£Aggiorna Dichiarazione£ src=£../NuoveImm/Img_refresh_icon.png£ onclick=£window.open(''../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=-1$T=3$IDD='||DOMANDE_BANDO_VSA.ID||'$CODU='||substr(domande_bando_vsa.cod_contratto_scambio,1,17)||''',''AggiornaDich'',''top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes'');£ style=£cursor: pointer£ />','$','&'),'£','" & Chr(34) & "') as AGGIORNA_DICH " _
                    & "FROM " _
                    & "COMP_NUCLEO_VSA, DOMANDE_BANDO_VSA,dichiarazioni_vsa " _
                    & "WHERE DOMANDE_BANDO_VSA.id_dichiarazione=dichiarazioni_vsa.id and (DOMANDE_BANDO_VSA.REDDITO_ISEE<>'' OR DOMANDE_BANDO_VSA.REDDITO_ISEE IS NOT NULL) AND " _
                    & "COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND COMP_NUCLEO_VSA.progr=DOMANDE_BANDO_VSA.PROGR_COMPONENTE AND " _
                    & "DOMANDE_BANDO_VSA.ID_STATO='8' AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA in (5,11) AND " _
                    & "DOMANDE_BANDO_VSA.ID NOT IN (SELECT ID_DOMANDA FROM SISCOM_MI.UNITA_ASSEGNATE WHERE PROVENIENZA='P') " _
                    & "AND substr(domande_bando_vsa.cod_contratto_scambio,1,17) IN (SELECT COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI) " _
                    & "AND DOMANDE_BANDO_VSA.ID = (SELECT MAX(DBV.ID) " _
                    & "FROM " _
                    & "COMP_NUCLEO_VSA CNV, DOMANDE_BANDO_VSA DBV " _
                    & "WHERE (DBV.REDDITO_ISEE<>'' OR DBV.REDDITO_ISEE IS NOT NULL) AND " _
                    & "CNV.ID_DICHIARAZIONE=DBV.ID_DICHIARAZIONE AND CNV.progr=DBV.PROGR_COMPONENTE AND " _
                    & "DBV.ID_STATO='8' AND DBV.ID_MOTIVO_DOMANDA in (5,11) AND " _
                    & "DBV.ID NOT IN (SELECT ID_DOMANDA FROM SISCOM_MI.UNITA_ASSEGNATE WHERE PROVENIENZA='P') " _
                    & "AND substr(DBV.cod_contratto_scambio,1,17) IN (SELECT COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI) " _
                    & "AND CNV.COD_FISCALE=COMP_NUCLEO_VSA.COD_FISCALE ) " _
                    & "ORDER BY DOMANDE_BANDO_VSA.PG ASC,COMP_NUCLEO_VSA.PROGR ASC"

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
            par.cmd.CommandText = "select domande_bando_vsa.id_dichiarazione,comp_nucleo_vsa.*,dichiarazioni_vsa.* from domande_bando_vsa,dichiarazioni_vsa,comp_nucleo_vsa where comp_nucleo_vsa.id_dichiarazione=dichiarazioni_vsa.id and progr=0 and domande_bando_vsa.id_dichiarazione=dichiarazioni_vsa.id and id_d_import=" & LBLID.Value & ""
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
        Response.Write("<script>top.location.href='SelezionaTipoContratto.aspx';</script>")
    End Sub
End Class
