
Partial Class Contabilita_CicloPassivo_Plan_Immagini_RicercaPF
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        chiamante.Value = Request.QueryString("C")

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            LBLID.Value = "-1"
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        Try
            Dim ds As New Data.DataSet()
            Select Case chiamante.Value
                Case "0"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_STATI.ID=PF_MAIN.ID_STATO ORDER BY PF_MAIN.ID_ESERCIZIO_FINANZIARIO DESC", par.OracleConn)
                    da.Fill(ds, "SISCOM_MI.PF_MAIN")
                Case "1"
                    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_MAIN.ID_STATO=1 AND PF_STATI.ID=PF_MAIN.ID_STATO ORDER BY PF_MAIN.ID_ESERCIZIO_FINANZIARIO DESC", par.OracleConn)
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_STATI.ID=PF_MAIN.ID_STATO ORDER BY PF_MAIN.ID_ESERCIZIO_FINANZIARIO DESC", par.OracleConn)
                    da.Fill(ds, "SISCOM_MI.PF_MAIN")
                Case "2"

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE (T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.ID_ESERCIZIO_FINANZIARIO) AND  PF_STATI.ID=PF_MAIN.ID_STATO and (pf_main.id_stato=1 or id_stato=3) ORDER BY PF_MAIN.ID_ESERCIZIO_FINANZIARIO DESC", par.OracleConn)
                    da.Fill(ds, "SISCOM_MI.PF_MAIN")
                Case "3"
                    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE (T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.ID_ESERCIZIO_FINANZIARIO) AND  PF_STATI.ID=PF_MAIN.ID_STATO and PF_MAIN.ID NOT IN (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE_MADRE IS NOT NULL AND PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID AND PF_VOCI_STRUTTURA.COMPLETO_ALER='0') AND PF_MAIN.ID NOT IN (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_struttura WHERE ID_VOCE_MADRE IS NULL AND PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID AND COMPLETO_aler='0' AND ID NOT IN (SELECT ID_VOCE_MADRE FROM SISCOM_MI.PF_VOCI)) ORDER BY PF_MAIN.ID_ESERCIZIO_FINANZIARIO DESC", par.OracleConn)
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE (T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.ID_ESERCIZIO_FINANZIARIO) AND  PF_STATI.ID=PF_MAIN.ID_STATO and PF_MAIN.ID_STATO<>5 ORDER BY PF_MAIN.ID_ESERCIZIO_FINANZIARIO DESC", par.OracleConn)
                    da.Fill(ds, "SISCOM_MI.PF_MAIN")
                Case "4"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_MAIN.ID_STATO=3 AND PF_STATI.ID=PF_MAIN.ID_STATO ORDER BY PF_MAIN.ID_ESERCIZIO_FINANZIARIO DESC", par.OracleConn)
                    da.Fill(ds, "SISCOM_MI.PF_MAIN")
                Case "5", "12"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_STATI.ID=PF_MAIN.ID_STATO ORDER BY PF_MAIN.ID_ESERCIZIO_FINANZIARIO DESC", par.OracleConn)
                    da.Fill(ds, "SISCOM_MI.PF_MAIN")
                Case "9", "11"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_MAIN.ID_STATO=1 AND PF_STATI.ID=PF_MAIN.ID_STATO ORDER BY PF_MAIN.ID_ESERCIZIO_FINANZIARIO DESC", par.OracleConn)
                    da.Fill(ds, "SISCOM_MI.PF_MAIN")
            End Select
            Datagrid2.DataSource = ds
            Datagrid2.DataBind()



        Catch ex As Exception

            TextBox3.Text = ex.Message

        End Try
    End Sub

    Protected Sub Datagrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor=''")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TextBox3').value='Hai selezionato il Piano Finanziario " & e.Item.Cells(1).Text & " - " & e.Item.Cells(2).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")

        End If
    End Sub

    Protected Sub Datagrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            Datagrid2.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub Datagrid2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Datagrid2.SelectedIndexChanged

    End Sub
End Class
