
Partial Class Contratti_DepositoCauzionaleInte
    Inherits System.Web.UI.Page
    Dim par As New CM.Global()
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Str As String = ""
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()
            CaricaIntestatari()
        End If
    End Sub
    Private Sub CaricaIntestatari()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()
            par.cmd.CommandText = " SELECT SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AS ID," _
                & " RAGIONE_SOCIALE,COGNOME,NOME,COD_TIPOLOGIA_OCCUPANTE " _
                & " ,SISCOM_MI.GETDATA(DATA_INIZIO) AS DATA_INIZIO,DATA_INIZIO as D1 " _
                & " ,SISCOM_MI.GETDATA(DATA_FINE) AS DATA_FINE,DATA_FINE AS D2 " _
                & " FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE  " _
                & " COD_TIPOLOGIA_OCCUPANTE IN('INTE','EXINTE') " _
                & " AND ANAGRAFICA.ID=ID_ANAGRAFICA " _
                & " AND ID_CONTRATTO=" & Request.QueryString("IDC") _
                & " ORDER BY nvl(D2,'29991231') DESC,D1 ASC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            DataGrid.DataSource = dt
            DataGrid.DataBind()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End Try
    End Sub
    Protected Sub DataGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('idIntestatario').value='" & e.Item.Cells(par.IndDGC(DataGrid, "ID")).Text & "';")
            e.Item.Attributes.Add("onDblclick", "window.open('DepositoCauzionaleUSD.aspx?IDC=" & Request.QueryString("IDC") & "&IDI=' + document.getElementById('idIntestatario').value ,'DepCauzInte" & Request.QueryString("IDC") & "' + document.getElementById('idIntestatario').value + '" & Format(Now, "yyyymmdd") & "','height=600,width=800,top=0,left=0');")
        End If
    End Sub

    Protected Sub btnSeleziona_Click(sender As Object, e As System.EventArgs) Handles btnSeleziona.Click
        Response.Write("<script>window.open('DepositoCauzionaleUSD.aspx?IDC=" & Request.QueryString("IDC") & "&IDI=" & idIntestatario.Value & "','DepCauzInte" & Request.QueryString("IDC") & idIntestatario.Value & Format(Now, "yyyymmdd") & "','height=600,width=800,top=0,left=0');</script>")
    End Sub
End Class
