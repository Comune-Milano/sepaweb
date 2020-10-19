
Partial Class Contratti_ValorizzaIngiunzione
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If String.IsNullOrEmpty(Trim(Session.Item("OPERATORE"))) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        idConnessione.Value = Request.QueryString("IDCONN")
        par.OracleConn = CType(HttpContext.Current.Session.Item(idConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & idConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)

        If Not IsPostBack Then
            idBolletta.Value = Request.QueryString("IDBoll")
            CaricaInfo()
        End If
    End Sub

    Private Sub CaricaInfo()
        Try
            Dim riclass As Integer = 0
            Dim fl_storno As Integer = 0
            Dim importoTot As Decimal = 0
            Dim importoPag As Decimal = 0

            If idBolletta.Value <> "" Then

                par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPO_BOLL_INGIUNZIONE ORDER BY ID ASC", cmbTipoIngiunzione, "ID", "DESCRIZIONE")

                par.cmd.CommandText = "SELECT * FROM siscom_mi.BOL_BOLLETTE,siscom_mi.TIPO_BOLLETTE,siscom_mi.TIPO_BOLL_INGIUNZIONE WHERE BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID " _
                    & " and bol_bollette.ID_TIPO_INGIUNZIONE=TIPO_BOLL_INGIUNZIONE.id(+) and nvl(fl_annullata,0)=0 And BOL_BOLLETTE.ID in (" & idBolletta.Value & ")"
                Dim daBoll As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                dtBollette = New Data.DataTable
                daBoll.Fill(dtBollette)
                daBoll.Dispose()
                If dtBollette.Rows.Count = 1 Then
                    For Each riga As Data.DataRow In dtBollette.Rows
                        idcontratto.Value = par.IfNull(riga.Item("id_contratto"), "")
                        numBolletta.Value = par.IfNull(riga.Item("num_bolletta"), "")
                        If par.IfNull(riga.Item("ID_TIPO_INGIUNZIONE"), 0) <> 0 Then
                            cmbSbloccoIng.Visible = True
                            lblSpecifico.Visible = True
                            cmbTipoIngiunzione.SelectedValue = riga.Item("ID_TIPO_INGIUNZIONE")
                            importo.Value = par.IfNull(riga.Item("IMPORTO_INGIUNZIONE"), 0)
                        Else
                            importo.Value = Math.Round(par.IfNull(riga.Item("IMPORTO_TOTALE"), 0) - par.IfNull(riga.Item("IMPORTO_PAGATO"), 0), 2)
                            cmbSbloccoIng.Visible = False
                            lblSpecifico.Visible = False
                        End If
                    Next
                Else
                    Response.Write("<script>alert('Non è possibile lavorare la bolletta selezionata!');</script>")
                    ImgSalvaBoll2.Visible = False
                End If

            Else
                Response.Write("<script>alert('Selezionare una bolletta della lista!');</script>")
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - Ingiunz - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Public Property dtBollette() As Data.DataTable
        Get
            If Not (ViewState("dtVociGest") Is Nothing) Then
                Return ViewState("dtVociGest")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtVociGest") = value
        End Set
    End Property


    Protected Sub ImgSalvaBoll2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgSalvaBoll2.Click
        If confermaIngiunzione.Value = "1" Then
            Try
                If cmbSbloccoIng.SelectedValue = 0 Then


                    par.cmd.CommandText = "UPDATE siscom_mi.bol_Bollette SET ID_TIPO_INGIUNZIONE=" & par.insDbValue(cmbTipoIngiunzione.SelectedValue, False) & "," _
                        & " IMPORTO_INGIUNZIONE=(round((IMPORTO_TOTALE-NVL(QUOTA_SIND_B,0)-NVL(IMPORTO_RIC_B,0))-(NVL(IMPORTO_PAGATO,0)- NVL(IMPORTO_RIC_PAGATO_B,0)- NVL(QUOTA_SIND_PAGATA_B,0)),2)) WHERE ID=" & idBolletta.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idcontratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F300','BOLLETTA NUM.: " & numBolletta.Value & " - IMP.INGIUNTO euro " & importo.Value & "')"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "UPDATE siscom_mi.bol_Bollette SET ID_TIPO_INGIUNZIONE=null,IMPORTO_INGIUNZIONE=null WHERE ID=" & idBolletta.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idcontratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F302','BOLLETTA NUM.: " & numBolletta.Value & " - IMP.INGIUNTO euro " & importo.Value & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey1", "CloseAndRefresh();", True)

            Catch ex As Exception
                Session.Add("ERRORE", "Provenienza: " & Page.Title & " - ImgSalvaBoll2_Click - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        End If
    End Sub


End Class
