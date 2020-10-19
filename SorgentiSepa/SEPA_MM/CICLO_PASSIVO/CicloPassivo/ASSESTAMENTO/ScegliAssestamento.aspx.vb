Partial Class CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_ScegliAssestamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or (Session.Item("BP_GENERALE") <> 1 And Session.Item("MOD_ASS_CONV_COMU") <> 1) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        '##### CARICAMENTO PAGINA #####
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()
            Carica()
        End If
    End Sub
    Private Sub Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            'Dim idEsercizio As String = par.RicavaEsercizioCorrente
            par.cmd.CommandText = "select id from siscom_mi.pf_main where id_stato >= 5"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim idPiani As String = ""
            While lettore.Read
                idPiani &= par.IfNull(lettore(0), 0) & ","
            End While
            lettore.Close()
            If idPiani <> "" Then
                idPiani = Left(idPiani, Len(idPiani) - 1)
                par.cmd.CommandText = "SELECT pf_assestamento.id_stato,PF_ASSESTAMENTO.ID,(TO_CHAR(TO_DATE(inizio,'yyyymmdd'),'dd/mm/yyyy') ||' - '||TO_CHAR(TO_DATE(fine,'yyyymmdd'),'dd/mm/yyyy')) AS esercizio, " _
                                        & "TO_CHAR(TO_DATE(PF_ASSESTAMENTO.data_inserimento,'yyyymmdd'),'dd/mm/yyyy') as data_inserimento,PF_STATI.descrizione AS STATO " _
                                        & "FROM siscom_mi.PF_STATI, siscom_mi.PF_ASSESTAMENTO , " _
                                        & "siscom_mi.PF_MAIN, siscom_mi.T_ESERCIZIO_FINANZIARIO " _
                                        & "WHERE id_pf_main in (" & idPiani & ")" _
                                        & " AND PF_MAIN.ID = id_pf_main " _
                                        & "AND T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.id_esercizio_finanziario " _
                                        & "AND PF_STATI.ID = PF_ASSESTAMENTO.ID_STATO ORDER BY PF_ASSESTAMENTO.ID DESC "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                Dim dt As New Data.DataTable()
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    DataGridAssestamenti.DataSource = dt
                    DataGridAssestamenti.DataBind()
                Else
                    txtMia.Text = ""
                    lblTitolo.Text = "Nessun assestamento presente"
                    ImgProcedi.Visible = False
                End If

            Else
                Response.Write("<script>alert('Impossibile procedere!Nessun Esercizio Finanziario trovato');</script>")
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = "Carica - " & ex.Message
        End Try
    End Sub
    Protected Sub DataGridAssestamenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridAssestamenti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtMia').value='Hai selezionato l\'Assestamento del " & e.Item.Cells(2).Text & " avente stato " & e.Item.Cells(3).Text & " ';document.getElementById('idStato').value='" & e.Item.Cells(3).Text & "';document.getElementById('idAssestamento').value='" & e.Item.Cells(0).Text & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtMia').value='Hai selezionato l\'Assestamento del " & e.Item.Cells(2).Text & " avente stato " & e.Item.Cells(3).Text & " ';document.getElementById('idStato').value='" & e.Item.Cells(3).Text & "';document.getElementById('idAssestamento').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub
    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        If idAssestamento.Value > 0 Then
            Response.Redirect("GestioneAssestamento.aspx?id=" & idAssestamento.Value)
        Else
            Response.Write("<script>alert('Selezionare un Assestamento per procedere');</script>")
        End If
    End Sub
End Class
