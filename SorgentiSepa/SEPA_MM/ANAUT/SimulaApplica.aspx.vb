
Partial Class ANAUT_SimulaApplica
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnDeselezionaTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = False
        Next
    End Sub

    Protected Sub btnSelezionaTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = True
        Next
    End Sub

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

        If Not IsPostBack Then
            Response.Flush()
            LBLID.Value = "-1"
            Cerca()
        End If
    End Sub


    Private Function Cerca()

        Try
            Dim IDAU As Long

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT ANNO_ISEE,ID,DATA_INIZIO FROM UTENZA_BANDI WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                IDAU = myReader("ID")
            End If
            myReader.Close()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            sStringaSQL1 = "SELECT * FROM UTENZA_GRUPPI_LAVORO WHERE ID_BANDO_AU=" & IDAU & " and FL_ABUSIVI=0 AND APPLICAZIONE=0 ORDER BY FL_REGISTRO DESC,NOME_GRUPPO ASC"
            BindGrid()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "UTENZA_GRUPPI_LAVORO")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

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

   
    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Dim scriptblock As String = ""
        Dim Elenco As String = ""


        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox

        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            If chkExport.Checked Then
                Elenco = Elenco & oDataGridItem.Cells(1).Text & ","
            End If
        Next

        If Elenco <> "" Then
            Elenco = "(" & Mid(Elenco, 1, Len(Elenco) - 1) & ")"


            Session.Add("ElencoSimulazione", Elenco)
            'scriptblock = "<script language='javascript' type='text/javascript'>" _
            '            & "window.open('SimulazioneGruppoAU.aspx','','');" _
            '            & "</script>"
            scriptblock = "<script language='javascript' type='text/javascript'>" _
                        & "window.open('SimulazioneApplicazione.aspx','','');" _
                        & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
            End If

        Else

            Response.Write("<script>alert('Selezionare almeno un gruppo!');</script>")
        End If

    End Sub
End Class
