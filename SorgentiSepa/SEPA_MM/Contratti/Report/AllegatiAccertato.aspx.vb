Imports System.IO

Partial Class Contratti_Report_AllegatiAccertato
    Inherits PageSetIdMode
    Dim par As New CM.Global

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


        If Not IsPostBack Then
            Response.Flush()

            LBLID.Value = "-1"
            Cerca()
        End If
    End Sub

    Private Function Cerca()
        sStringaSQL = "SELECT SISCOM_MI.TAB_MODELLI_ALLEGATI.DESCRIZIONE AS TIPOLOGIA,DATA_EMISSIONE,ALLEGATI_ACCERTATO.ID,TO_CHAR(TO_DATE(PERIODO_DA,'YYYYmmdd'),'DD/MM/YYYY')||'-'||TO_CHAR(TO_DATE(PERIODO_A,'YYYYmmdd'),'DD/MM/YYYY') AS PERIODO_REF,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../../allegati/accertato/'||file_allegato||''',''Allegati'','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') as ALLEGATO FROM SISCOM_MI.TAB_MODELLI_ALLEGATI,SISCOM_MI.ALLEGATI_ACCERTATO WHERE SISCOM_MI.TAB_MODELLI_ALLEGATI.ID=ALLEGATI_ACCERTATO.ID_TIPO_ALLEGATO and allegati_accertato.data_canc is null ORDER BY DATA_EMISSIONE DESC"
        BindGrid()

    End Function

    Private Sub BindGrid()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.ALLEGATI_ACCERTATO")
            Label4.Text = Datagrid2.Items.Count
            Datagrid2.DataSource = ds
            Datagrid2.DataBind()

            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = sStringaSQL
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Label4.Text = "(" & Datagrid2.Items.Count & " nella pagina - Totale :" & ds.Tables(0).Rows.Count & ")"
            End If
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            TextBox3.Text = ex.Message

        End Try
    End Sub

    Public Property sStringaSQL() As String
        Get
            If Not (ViewState("par_sStringaSQL") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL") = value
        End Set

    End Property

    Protected Sub btnNuovoAllegato_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuovoAllegato.Click
        BindGrid()
    End Sub

    Protected Sub Datagrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=''")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")

            'btnVisualizza.Attributes.Add("onclick", "window.open('Contratto.aspx?ID=" & LBLID.Text & "&COD=" & Label3.Text & "','Contratto" & Format(Now, "hhss") & "','height=680,width=900');")
        End If
    End Sub

    Protected Sub imgElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgElimina.Click
        If elimina.Value = "1" Then
            Try
                Dim strFile As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)


                par.cmd.CommandText = "select * from siscom_mi.allegati_accertato where id=" & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    strFile = par.IfNull(myReader("file_allegato"), "")
                    File.Delete(Server.MapPath("..\..\ALLEGATI\ACCERTATO\") & strFile)
                    par.cmd.CommandText = "UPDATE siscom_mi.allegati_accertato SET DATA_CANC='" & Format(Now, "yyyyMMddHHmmss") & "',id_operatore_canc=" & Session.Item("id_operatore") & " where id=" & LBLID.Value
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Operazione Effettuata!');</script>")
                End If
                myReader.Close()


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



                BindGrid()
            Catch ex As Exception

            End Try
            
        End If
    End Sub
End Class
