
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_LinkDettaglioSpesa
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim totale As Decimal = 0
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            rilevaParametri()
            caricaVoce()
        End If
    End Sub
    Protected Sub rilevaParametri()
        idVoce.Value = Request.QueryString("IDV")
        idLotto.Value = Request.QueryString("IDL")
        idServizio.Value = Request.QueryString("IDS")
        idPianoF.Value = Request.QueryString("IDP")
        IDVS.Value = Request.QueryString("IDVS")
        P.value = Request.QueryString("P")
    End Sub
    Protected Sub caricaVoce()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select codice,descrizione from siscom_mi.pf_voci where id=" & Request.QueryString("IDV")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblTitolo.Text = par.IfNull(myReader("codice"), "") & " - " & par.IfNull(myReader("descrizione"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "select codice,descrizione from siscom_mi.pf_voci where id=" & Request.QueryString("IDV")
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblSottotitolo.Text = "Composizione Preventivo di bilancio " & Request.QueryString("P")
            End If
            myReader.Close()


            Dim V1 As Decimal = 0
            Dim V2 As Decimal = 0

            par.cmd.CommandText = "select * FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID=" & IDVS.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblVoce.Text = par.IfNull(myReader5("DESCRIZIONE"), "")
                V1 = CDbl(Format((CDbl(par.IfNull(myReader5("VALORE_CANONE"), 0) + ((par.IfNull(myReader5("iva_CANONE"), 0) / 100) * par.IfNull(myReader5("VALORE_CANONE"), 0)))), "0.00"))
                V2 = CDbl(Format((CDbl(par.IfNull(myReader5("VALORE_CONSUMO"), 0) + ((par.IfNull(myReader5("iva_CONSUMO"), 0) / 100) * par.IfNull(myReader5("VALORE_CONSUMO"), 0)))), "0.00"))
                lblImporto.Text = Format(V1 + V2, "##,##0.00")
                idLotto.Value = myReader5("ID_LOTTO")
            End If
            myReader5.Close()

            par.cmd.CommandText = "select * FROM SISCOM_MI.LOTTI WHERE ID=" & idLotto.Value
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblLotto.Text = myReader5("DESCRIZIONE")
                tipoLotto.Value = myReader5("TIPO")
            End If
            myReader5.Close()
            par.OracleConn.Close()

            If tipoLotto.Value = "E" Then
                caricaEdifici()
            Else
                caricaImpianti()
            End If

        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub
    Protected Sub caricaImpianti()

        Dim ImportoCanone As Double = 0
        Dim ImportoConsumo As Double = 0
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT IMPIANTI.COD_IMPIANTO as ""COD IMPIANTO"",IMPIANTI.DESCRIZIONE AS DENOMINAZIONE,trim(TO_CHAR(LOTTI_PATRIMONIO_IMPORTI.importo_canone_lordo,'999G999G990D99')) AS ""IMPORTO"" FROM siscom_mi.IMPIANTI,siscom_mi.LOTTI_PATRIMONIO_IMPORTI WHERE LOTTI_PATRIMONIO_IMPORTI.id_lotto=" & idLotto.Value & " AND IMPIANTI.ID=LOTTI_PATRIMONIO_IMPORTI.ID_IMPIANTO AND LOTTI_PATRIMONIO_IMPORTI.id_voce_importo=" & IDVS.Value & "  ORDER BY IMPIANTI.DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
                lblTipoDivisione.Text = "Divisione importo voci per impianti"
            Else
                lblTipoDivisione.Text = "Divisione importo voci per intero patrimonio"
            End If
            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub caricaEdifici()

        Dim ImportoCanone As Double = 0
        Dim ImportoConsumo As Double = 0
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "select EDIFICI.cod_EDIFICIO as ""COD EDIFICIO"",EDIFICI.denominazione,trim(TO_CHAR(lotti_patrimonio_importi.importo_canone_lordo,'999G999G990D99')) AS ""IMPORTO"" from siscom_mi.EDIFICI,siscom_mi.lotti_patrimonio_importi where lotti_patrimonio_importi.id_lotto=" & idLotto.Value & " and EDIFICI.id=lotti_patrimonio_importi.id_EDIFICIO and lotti_patrimonio_importi.id_voce_importo=" & IDVS.Value & "  order by EDIFICI.denominazione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
                lblTipoDivisione.Text = "Divisione importo voci per edifici"
            Else
                lblTipoDivisione.Text = "Divisione importo voci per intero patrimonio"
            End If
            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub DataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                totale += CType((e.Item.Cells(2).Text), Double)
                e.Item.Cells(0).HorizontalAlign = HorizontalAlign.Center
                e.Item.Cells(1).HorizontalAlign = HorizontalAlign.Left
                e.Item.Cells(2).HorizontalAlign = HorizontalAlign.Right
            Case ListItemType.Footer
                e.Item.Cells(2).Text = Format(totale, "#,##0.00")
                e.Item.BackColor = System.Drawing.ColorTranslator.FromHtml("#DDDDDD")
                e.Item.Cells(0).HorizontalAlign = HorizontalAlign.Right
        End Select
    End Sub

End Class
