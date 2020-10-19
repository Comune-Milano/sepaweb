
Partial Class GestioneAutonoma_RisModB
    Inherits PageSetIdMode
    Dim vComplesso As String
    Dim vEdificio As String
    Dim vCodProt As String
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
            vComplesso = Request.QueryString("C")
            vEdificio = Request.QueryString("E")
            vCodProt = Request.QueryString("CODPROV")
            Cerca()
            '********BISOGNA SEMPRE METTERLO NEL POSTBACK!
            '********SE FUORI EVENTUALI METODI CHE USANO LA RESP.WRITE SI IMBALLANO PERCHè LUI LE PULISCE TUTTE!
            Response.Flush()

        End If
    End Sub
    Public Property QUERY() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property
    Private Sub Cerca()
        Try

            If vEdificio <> "-1" Then
                QUERY = "SELECT AUTOGESTIONI_MOD_B.ID,NUM_PROV,(EDIFICI.DENOMINAZIONE)AS COMP_EDIF,TO_CHAR(TO_DATE(DATA_PROV,'yyyymmdd'),'dd/mm/yyyy')AS DATA_PROV,TO_CHAR(TO_DATE(DATA_DEC,'yyyymmdd'),'dd/mm/yyyy')AS DATA_DEC,DECODE(autogestioni_mod_b.fl_autorizzata,'0','RESPINTA','1','APPROVATA','-1','DA COMPLETARE')AS STATO FROM siscom_mi.autogestioni_mod_b,siscom_mi.autogestioni, siscom_mi.edifici WHERE autogestioni.ID = autogestioni_mod_b.id_autogestione AND edifici.ID = autogestioni.id_edificio"
            ElseIf vComplesso <> "-1" Then
                QUERY = "SELECT AUTOGESTIONI_MOD_B.ID,NUM_PROV,(COMPLESSI_IMMOBILIARI.DENOMINAZIONE)AS COMP_EDIF,TO_CHAR(TO_DATE(DATA_PROV,'yyyymmdd'),'dd/mm/yyyy')AS DATA_PROV,TO_CHAR(TO_DATE(DATA_DEC,'yyyymmdd'),'dd/mm/yyyy')AS DATA_DEC, DECODE(autogestioni_mod_b.fl_autorizzata,'0','RESPINTA','1','APPROVATA','-1','DA COMPLETARE')AS STATO FROM siscom_mi.autogestioni_mod_b,siscom_mi.autogestioni,siscom_mi.complessi_immobiliari WHERE autogestioni.ID = autogestioni_mod_b.id_autogestione AND complessi_immobiliari.ID = autogestioni.id_complesso "
            Else
                QUERY = "SELECT AUTOGESTIONI_MOD_B.ID,NUM_PROV,(COMPLESSI_IMMOBILIARI.DENOMINAZIONE)AS COMP_EDIF,TO_CHAR(TO_DATE(DATA_PROV,'yyyymmdd'),'dd/mm/yyyy')AS DATA_PROV,TO_CHAR(TO_DATE(DATA_DEC,'yyyymmdd'),'dd/mm/yyyy')AS DATA_DEC, DECODE(autogestioni_mod_b.fl_autorizzata,'0','RESPINTA','1','APPROVATA','-1','DA COMPLETARE')AS STATO FROM siscom_mi.autogestioni_mod_b,siscom_mi.autogestioni,siscom_mi.complessi_immobiliari WHERE autogestioni.ID = autogestioni_mod_b.id_autogestione AND complessi_immobiliari.ID = autogestioni.id_complesso UNION SELECT AUTOGESTIONI_MOD_B.ID,NUM_PROV,(EDIFICI.DENOMINAZIONE)AS COMP_EDIF,TO_CHAR(TO_DATE(DATA_PROV,'yyyymmdd'),'dd/mm/yyyy')AS DATA_PROV,TO_CHAR(TO_DATE(DATA_DEC,'yyyymmdd'),'dd/mm/yyyy')AS DATA_DEC,DECODE(autogestioni_mod_b.fl_autorizzata,'0','RESPINTA','1','APPROVATA','-1','DA COMPLETARE')AS STATO FROM siscom_mi.autogestioni_mod_b,siscom_mi.autogestioni, siscom_mi.edifici WHERE autogestioni.ID = autogestioni_mod_b.id_autogestione AND edifici.ID = autogestioni.id_edificio"
            End If


            If Not String.IsNullOrEmpty(vEdificio) And vEdificio <> "-1" Then
                QUERY = QUERY & " AND ID_EDIFICIO = " & vEdificio
            ElseIf Not String.IsNullOrEmpty(vComplesso) And vComplesso <> "-1" Then
                QUERY = QUERY & " AND EDIFICI.ID_COMPLESSO = " & vComplesso
            ElseIf Not String.IsNullOrEmpty(vCodProt) Then
                QUERY = QUERY & " AND NUM_PROV=" & vCodProt
            End If
            BindGrid()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(QUERY, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "AUTOGESTIONI,INDIRIZZI,EDIFICI,COMPLESSI")
        DataGridModB.DataSource = ds
        DataGridModB.DataBind()
        LnlNumeroRisultati.Text = "  - " & DataGridModB.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        '*********************CHIUSURA CONNESSIONE**********************

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>parent.main.location.replace('RicercaModB.aspx');</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Not String.IsNullOrEmpty(txtid.Value) Then
            Response.Write("<script>parent.main.location.replace('ModBGestioneAutonoma.aspx?ID=" & txtid.Value & "');</script>")
        Else
            Response.Write("<script>alert('Selezionare una Proposta di Gestione Autonoma da visualizzare!');</script>")
        End If

    End Sub


    Protected Sub DataGridModB_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridModB.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la domanda su: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la domanda su: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub
End Class
