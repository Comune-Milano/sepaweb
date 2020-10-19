
Partial Class AMMSEPA_LogIngressi
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaLogIngressi.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Cerca()
        End If
    End Sub

    Private Sub BindGrid()
        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "operatori,operatori_web_log")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        Label10.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        par.OracleConn.Close()


    End Sub

    Private Sub Cerca()


        sStringaSQL1 = "SELECT OPERATORI.ID,OPERATORI.OPERATORE,OPERATORI.COGNOME,OPERATORI.NOME,OPERATORI.COD_FISCALE,CAF_WEB.COD_CAF AS ""ENTE"", " _
                    & " TO_CHAR(TO_DATE(SUBSTR(OPERATORI_WEB_LOG.DATA_ORA_IN,1,8),'YYYYmmdd'),'DD/MM/YYYY')||' '||SUBSTR(OPERATORI_WEB_LOG.DATA_ORA_IN,9,2)||':'||SUBSTR(OPERATORI_WEB_LOG.DATA_ORA_IN,11,2) AS ""INGRESSO""," _
                    & " TO_CHAR(TO_DATE(SUBSTR(OPERATORI_WEB_LOG.DATA_ORA_OUT,1,8),'YYYYmmdd'),'DD/MM/YYYY')||' '||SUBSTR(OPERATORI_WEB_LOG.DATA_ORA_OUT,9,2)||':'||SUBSTR(OPERATORI_WEB_LOG.DATA_ORA_OUT,11,2) AS ""USCITA"" FROM OPERATORI,CAF_WEB,OPERATORI_WEB_LOG WHERE " _
                    & " OPERATORI.FL_ELIMINATO='0' AND OPERATORI.ID_CAF=CAF_WEB.ID AND OPERATORI.ID=OPERATORI_WEB_LOG.ID_OPERATORE "


        If Session.Item("LOGINGRESSI") <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & Session.Item("LOGINGRESSI")
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY OPERATORI_WEB_LOG.DATA_ORA_IN desc"


        BindGrid()
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

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If

    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub
End Class
