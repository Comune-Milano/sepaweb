
Partial Class Contratti_SelezionaUnitaConc
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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

            sStringaSQL1 = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS ""TIPOLOGIA"",DECODE(UI_USI_DIVERSI.ASCENSORE,1,'SI') AS ""ELEVATORE"",T_TIPO_PROPRIETA.DESCRIZIONE AS ""PROPRIETA1"",TIPO_LIVELLO_PIANO.DESCRIZIONE AS ""PIANO"",UI_USI_DIVERSI.*,T_TIPO_ALL_ERP.DESCRIZIONE," _
                       & "T_TIPO_INDIRIZZO.DESCRIZIONE AS ""TIPO_VIA"", " _
                       & "TO_CHAR(TO_DATE(UI_USI_DIVERSI.DATA_DISPONIBILITA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_DISPONIBILITA1"", UI_USI_DIVERSI.NUM_ALLOGGIO AS ""N_ALL"" FROM T_TIPO_PROPRIETA,SISCOM_MI.UI_USI_DIVERSI," _
                       & "T_TIPO_ALL_ERP,T_TIPO_INDIRIZZO,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE=UI_USI_DIVERSI.COD_ALLOGGIO AND TIPOLOGIA_UNITA_IMMOBILIARI.COD=UNITA_IMMOBILIARI.COD_TIPOLOGIA AND to_number(tipo_livello_piano.cod) = to_number(ui_usi_diversi.piano) AND  " _
                       & " UI_USI_DIVERSI.PROPRIETA=T_TIPO_PROPRIETA.COD (+) AND EQCANONE='0' AND FL_OA='0' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA<>'SO' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA<>'C' AND " _
                       & " ASSEGNATO='0' AND PRENOTATO='0' AND UI_USI_DIVERSI.STATO=5 " _
                       & " AND UI_USI_DIVERSI.TIPO_ALLOGGIO=T_TIPO_ALL_ERP.COD (+) AND UI_USI_DIVERSI.TIPO_INDIRIZZO=T_TIPO_INDIRIZZO.COD (+) AND UNITA_IMMOBILIARI.COD_TIPOLOGIA='CC' ORDER BY UI_USI_DIVERSI.TIPO_INDIRIZZO ASC, UI_USI_DIVERSI.INDIRIZZO ASC, UI_USI_DIVERSI.ZONA ASC,UI_USI_DIVERSI.NUM_LOCALI ASC"

            'par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, PAR.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "UI_USI_DIVERSI")
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

    Protected Sub DataGrid1_CancelCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.CancelCommand
        Dim scriptblock As String = ""
        scriptblock = "<script language='javascript' type='text/javascript'>" _
        & "popupWindow=window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & e.Item.Cells(1).Text & "','" & e.Item.Cells(1).Text & "','height=580,top=0,left=0,width=780');" _
        & "</script>"
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
        End If
    End Sub

    Protected Sub DataGrid1_EditCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(1).Text
        Label9.Text = "Hai selezionato Unità Cod.: " & e.Item.Cells(1).Text
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub ImgProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
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

                Response.Redirect("SceltaContraenteUD.aspx?T=4&U=" & Codice & "&CODICE=" & LBLID.Text)



            Catch ex As Exception
                PAR.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:Seleziona Unita contratti Usi diversi - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")

            End Try
        Else
            Response.Write("<script>alert('Selezionare una unità dalla lista!');</script>")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub btnAnnulla0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla0.Click
        Response.Redirect("SelezionaTipoContratto.aspx")
    End Sub
End Class
