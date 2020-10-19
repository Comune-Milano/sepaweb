
Partial Class Contabilita_RisultatiRicercaIncassiManuali
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim totale As Decimal = 0
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        caricamentoInCorso()
        If Not IsPostBack Then
            Response.Flush()
            CaricaDati()
        End If
    End Sub
    Private Sub CaricaDati()
        Try
            'ApriConnessione()
            Dim condizioniDiRicerca As String = ""
            Dim indice As Integer = 0


            If Not IsNothing(Request.QueryString("nominativo")) AndAlso Request.QueryString("nominativo") <> "" Then
                indice += 1
                If indice = 1 Then
                    condizioniDiRicerca &= " where upper(nominativo) like '%" & UCase(Replace(Request.QueryString("nominativo"), "*", "%")) & "%' "
                Else
                    condizioniDiRicerca &= " and upper(nominativo) like '%" & UCase(Replace(Request.QueryString("nominativo"), "*", "%")) & "%' "
                End If
            End If

            If Not IsNothing(Request.QueryString("Tipologia")) AndAlso Request.QueryString("Tipologia") <> "" AndAlso Request.QueryString("Tipologia") <> "-1" Then
                indice += 1
                If indice = 1 Then
                    condizioniDiRicerca &= " where id_tipo_pag= " & Request.QueryString("Tipologia")
                Else
                    condizioniDiRicerca &= " and id_tipo_pag= " & Request.QueryString("Tipologia")
                End If
            End If

            If Not IsNothing(Request.QueryString("causale")) AndAlso Request.QueryString("causale") <> "" Then
                indice += 1
                If indice = 1 Then
                    condizioniDiRicerca &= " where upper(causale) like '%" & UCase(Replace(Request.QueryString("causale"), "*", "%")) & "%' "
                Else
                    condizioniDiRicerca &= " and upper(causale) like '%" & UCase(Replace(Request.QueryString("causale"), "*", "%")) & "%' "
                End If
            End If

            If Not IsNothing(Request.QueryString("incassatoDa")) AndAlso Request.QueryString("incassatoDa") <> "" Then
                indice += 1
                If indice = 1 Then
                    condizioniDiRicerca &= " where importo >= " & Replace(Replace(Request.QueryString("incassatoDa"), ".", ""), ",", ".")
                Else
                    condizioniDiRicerca &= " and importo >= " & Replace(Replace(Request.QueryString("incassatoDa"), ".", ""), ",", ".")
                End If
            End If

            If Not IsNothing(Request.QueryString("incassatoA")) AndAlso Request.QueryString("incassatoA") <> "" Then
                indice += 1
                If indice = 1 Then
                    condizioniDiRicerca &= " where importo <= " & Replace(Replace(Request.QueryString("incassatoA"), ".", ""), ",", ".")
                Else
                    condizioniDiRicerca &= " and importo <= " & Replace(Replace(Request.QueryString("incassatoA"), ".", ""), ",", ".")
                End If
            End If

            If Not IsNothing(Request.QueryString("dataIncassoDa")) AndAlso Request.QueryString("dataIncassoDa") <> "" Then
                indice += 1
                If indice = 1 Then
                    condizioniDiRicerca &= " where data_incasso >= '" & par.FormatoDataDB(Request.QueryString("dataIncassoDa")) & "' "
                Else
                    condizioniDiRicerca &= " and data_incasso >= '" & par.FormatoDataDB(Request.QueryString("dataIncassoDa")) & "' "
                End If
            End If

            If Not IsNothing(Request.QueryString("dataIncassoA")) AndAlso Request.QueryString("dataIncassoA") <> "" Then
                indice += 1
                If indice = 1 Then
                    condizioniDiRicerca &= " where data_incasso <= '" & par.FormatoDataDB(Request.QueryString("dataIncassoA")) & "' "
                Else
                    condizioniDiRicerca &= " and data_incasso <= '" & par.FormatoDataDB(Request.QueryString("dataIncassoA")) & "' "
                End If
            End If

            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT ID,FL_ATTRIBUITO,(SELECT COUNT(*) FROM SISCOM_MI.INCASSI_ATTRIBUITI WHERE SISCOM_MI.INCASSI_ATTRIBUITI.ID_INCASSO_NON_ATTR=ID) AS CONTEGGIO,UPPER(NOMINATIVO) AS NOMINATIVO," _
                & "TRIM(TO_CHAR(INCASSI_NON_ATTRIBUIBILI.IMPORTO,'999G999G990D99')) AS IMPORTO,CAUSALE,TO_CHAR(TO_DATE(DATA_INCASSO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_INCASSO," _
                & "NOTE,DATA_INCASSO AS DATA_INCASSO_OR FROM SISCOM_MI.INCASSI_NON_ATTRIBUIBILI " & condizioniDiRicerca _
                & " ORDER BY DATA_INCASSO_OR DESC "
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            If dataTable.Rows.Count > 0 Then
                DataGridIncassi.DataSource = dataTable
                DataGridIncassi.DataBind()
                DataGridIncassi.Visible = True
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertNot", "alert('Nessun incasso manuale trovato con i parametri di ricerca impostati! Ripetere la ricerca!');parent.main.location.replace('RicercaIncassiManuali.aspx');", True)
                DataGridIncassi.Visible = False
            End If
            dataAdapter.Dispose()
            'chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertErrore", "alert('Si è verificato un errore nel caricamento dei dati! Ripetere la ricerca!');parent.main.location.replace('RicercaIncassiManuali.aspx');", True)
        End Try
    End Sub
    Protected Sub chiudiConnessione()
        If Not IsNothing(par.cmd) Then
            par.cmd.Dispose()
        End If
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Protected Sub ApriConnessione()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
    End Sub
    Private Sub caricamentoInCorso()
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 796px; height: 540px;" _
             & "top: 0px; left: 0px;background-color: #eeeeee;background-image: url('../NuoveImm/SfondoMascheraContratti2.jpg');z-index:1000;"">" _
             & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
             & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
             & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
             & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
             & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
             & "</td></tr></table></div></div>"
        Response.Write(Loading)
    End Sub
    Protected Sub DataGridIncassi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIncassi.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.AlternatingItem, ListItemType.Item
                If CType(e.Item.Cells(6).Text, Integer) = 1 Or CType(e.Item.Cells(7).Text, Integer) > 0 Then
                    e.Item.Cells(5).Visible = False
                    e.Item.Cells(4).ColumnSpan = 2
                Else
                    CType(e.Item.Cells(5).FindControl("ImageButtonModifica"), ImageButton).Attributes.Add("onclick", "Modifica(" & par.IfNull(e.Item.Cells(8).Text, "") & ");")
                    Dim stringaChiamata As String = "Elimina(" & par.IfNull(e.Item.Cells(8).Text, "") _
                        & ",'" & Replace(par.IfNull(Replace(e.Item.Cells(0).Text, "&nbsp;", ""), ""), "'", "\'") _
                        & "','" & Replace(par.IfNull(Replace(e.Item.Cells(1).Text, "&nbsp;", ""), ""), "'", "\'") _
                        & "');"
                    CType(e.Item.Cells(5).FindControl("ImageButtonElimina"), ImageButton).Attributes.Add("onclick", stringaChiamata)
                End If
                totale += CType(e.Item.Cells(1).Text, Double)
            Case ListItemType.Footer
                e.Item.Cells(1).Text = Format(totale, "##,##0.00")
        End Select
    End Sub
    Protected Sub ImageButtonModifica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        CaricaDati()
    End Sub
    Protected Sub ImageButtonElimina_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Try

            If IdEliminazione.Value <> 0 Then
                ApriConnessione()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.INCASSI_NON_ATTRIBUIBILI WHERE ID=" & IdEliminazione.Value
                Dim eliminazione As Integer = par.cmd.ExecuteNonQuery()
                If eliminazione = 1 Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "messaggioSuccesso", "alert('Incasso eliminato correttamente!');location.replace('RisultatiRicercaIncassiManuali.aspx');", True)
                End If
                chiudiConnessione()
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertErrore", "alert('Non è possibile eliminare questo incasso!');", True)
            End If

        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertErrore", "alert('Non è possibile eliminare questo incasso!');", True)
        End Try
    End Sub
    
End Class
