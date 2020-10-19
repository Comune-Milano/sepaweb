
Partial Class Contratti_SelezionaUnita431
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)

            Dim sStringaSQL1 As String = ""

            sStringaSQL1 = "SELECT DECODE(ALLOGGI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",ALLOGGI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"", " _
                       & "TO_CHAR(TO_DATE(ALLOGGI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", ALLOGGI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,ALLOGGI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE TIPO_LIVELLO_PIANO.COD=ALLOGGI.PIANO AND  " _
                       & " ALLOGGI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND EQCANONE='0' AND FL_POR='1' AND FL_OA='0' AND " _
                       & " ASSEGNATO='0' AND PRENOTATO='0' AND ALLOGGI.STATO=5 " _
                       & " AND ALLOGGI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND ALLOGGI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) ORDER BY ALLOGGI.TIPO_INDIRIZZO ASC, ALLOGGI.INDIRIZZO ASC,ALLOGGI.ZONA ASC,ALLOGGI.NUM_LOCALI ASC"

            'par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, PAR.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "ALLOGGI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label9.Text = ex.Message
        End Try

    End Sub

    Protected Sub DataGrid1_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        Dim scriptblock As String = ""
        scriptblock = "<script language='javascript' type='text/javascript'>" _
        & "popupWindow=window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & e.Item.Cells(1).Text & "','" & e.Item.Cells(1).Text & "','height=580,top=0,left=0,width=780');" _
        & "</script>"
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
        End If
    End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(1).Text
        Label9.Text = "Hai selezionato Unità Cod.: " & e.Item.Cells(1).Text

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

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If LBLID.Text <> "" Then
            Try
                Dim Codice As Long

                PAR.OracleConn.Open()
                par.SettaCommand(par)

                PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE COD_UNITA_IMMOBILIARE='" & LBLID.Text & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()

                If myReader.Read = True Then
                    Codice = PAR.IfNull(myReader("ID"), -1)
                End If
                myReader.Close()
                PAR.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Redirect("SceltaContraenteUD.aspx?T=2&U=" & Codice & "&CODICE=" & LBLID.Text)
                'Response.Redirect("Contraente431.aspx?U=" & Codice & "&CODICE=" & LBLID.Text)



            Catch ex As Exception
                PAR.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:Seleziona Unita contratti Usi diversi - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")

            End Try
        Else
            Response.Write("<script>alert('Selezionare una unità dalla lista!');</script>")
        End If
    End Sub

    Protected Sub btnAnnulla0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla0.Click
        Response.Redirect("SelezionaTipoContratto.aspx")
    End Sub
End Class
