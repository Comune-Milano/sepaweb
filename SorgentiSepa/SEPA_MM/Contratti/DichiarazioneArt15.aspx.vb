
Partial Class Contratti_DichiarazioneArt15
    Inherits PageSetIdMode
    Dim sValorePG As String
    Dim sValoreCF As String
    Dim par As New CM.Global

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

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Value = e.Item.Cells(0).Text
        TextBox7.Text = "Hai selezionato: " & e.Item.Cells(2).Text & " - " & e.Item.Cells(3).Text & " " & e.Item.Cells(4).Text
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato :PG " & e.Item.Cells(1).Text & "';")


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
        da.Fill(ds, "DICHIARAZIONI,COMP_NUCLEO")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        da.Dispose()
        ds.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
        Else
            Response.Write("<script>document.location.href='CanoneArt15.aspx?IDC=" & LBLID.Value & "&IDU=" & Unita & "&CODICE=" & CODICEUnita & "';</script>")
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

    Private Function Cerca()
        Dim bTrovato As Boolean




        bTrovato = False
        sStringaSql = ""


        sStringaSQL = "SELECT domande_bando.id,DOMANDE_BANDO.PG,COMP_NUCLEO.COD_FISCALE,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME,domande_bando.reddito_isee as ISEE,DOMANDE_BANDO.ISE_ERP AS ISE,(CASE WHEN DEROGHE_ART_15.ID_TIPO = 0 THEN 'A' ELSE 'B' END) AS LETTERA FROM DEROGHE_ART_15, DOMANDE_BANDO,COMP_NUCLEO WHERE domande_bando.id not in (select id_domanda from siscom_mi.unita_assegnate where id_domanda=domande_bando.id) and DEROGHE_ART_15.ID_DOMANDA=DOMANDE_BANDO.ID AND  DOMANDE_BANDO.FL_INVITO='0' AND DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND DOMANDE_BANDO.FL_CONTROLLA_REQUISITI=2 AND DOMANDE_BANDO.ID_STATO='4' AND COMP_NUCLEO.ID_DICHIARAZIONE=DOMANDE_BANDO.ID_DICHIARAZIONE AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE and (domande_bando.id in (select id_domanda from deroghe_art_15 where id_tipo=1) or tipo_alloggio=1) ORDER BY COGNOME ASC,NOME ASC"

        BindGrid()
    End Function

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Add("CONTRATTOAPERTO", "0")
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub
End Class
