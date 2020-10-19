Imports System.Collections.Generic
Imports System.Data
Imports System.Web.Services
Imports Telerik.Web.UI
Partial Class Contratti_GestioneMotiviProcessDecis

    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Const ItemsPerRequest As Integer = 20

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            par.caricaComboTelerik("select ID, " _
                & " (case when id=1 then 'PREAVVISO DI DINIEGO' when id=3 then 'ACCOGLIMENTO' when id=5 then 'DINIEGO' END) as DECISIONE " _
                & " from t_stati_decisionali where ID IN (1,5,3) order by id asc", cmbDecisioni, "id", "decisione", True)
            par.caricaComboTelerik("select * from t_motivo_domanda_vsa where fl_nuova_normativa=1 order by id asc", cmbTipoIstanza, "id", "descrizione", True)

        End If
        Me.connData = New CM.datiConnessione(par, False, False)
    End Sub

    Protected Sub cmbDecisioni_ItemsRequested(ByVal o As Object, ByVal e As RadComboBoxItemsRequestedEventArgs)
        Dim combo As RadComboBox = DirectCast(o, RadComboBox)

        par.cmd.CommandText = "select ID, " _
         & " (case when id=1 then 'PREAVVISO DI DINIEGO' when id=3 then 'ACCOGLIMENTO' when id=5 then 'DINIEGO' END) as DECISIONE " _
         & " from t_stati_decisionali where ID IN (1,5,3) order by id asc"
        Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        combo.Items.Clear()
        combo.ClearSelection()
        For Each row As DataRow In dt.Rows
            Dim item As New RadComboBoxItem(row.Item("DECISIONE"), row.Item("ID"))
            combo.Items.Add(item)
        Next
    End Sub

    Protected Sub RadGridMotivi_BatchEditCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridBatchEditingEventArgs) Handles RadGridMotivi.BatchEditCommand
        Try
            connData.apri(True)
            For Each command As GridBatchEditingCommand In e.Commands
                Dim newValues As Hashtable = command.NewValues
                Dim oldValues As Hashtable = command.OldValues
                If command.Type = GridBatchEditingCommandType.Update Then
                    If par.IfEmpty(newValues("ACCOGLIMENTO"), "") <> par.IfEmpty(oldValues("ACCOGLIMENTO"), "") Then
                        par.cmd.CommandText = "UPDATE MOTIVI_TIPO_ISTANZA SET ID_FASE_DECISIONE=" & par.insDbValue(newValues("ACCOGLIMENTO"), False) & "" _
                                            & " WHERE ID=" & par.insDbValue(newValues("ID2"), False) & " "
                        par.cmd.ExecuteNonQuery()
                    End If
                    If par.IfEmpty(newValues("FRASE_STAMPA"), "") <> par.IfEmpty(oldValues("FRASE_STAMPA"), "") Then
                        par.cmd.CommandText = "UPDATE MOTIVI_TIPO_ISTANZA SET FRASE_STAMPA=" & par.insDbValue(newValues("FRASE_STAMPA"), True) & "" _
                                            & " WHERE ID=" & par.insDbValue(newValues("ID2"), False) & " "
                        par.cmd.ExecuteNonQuery()
                    End If
                    par.cmd.CommandText = "UPDATE T_MOTIVI_DINIEGO set DESCRIZIONE=" & par.insDbValue(newValues("MOTIVO"), True) & "" _
                                 & " WHERE ID=" & par.insDbValue(newValues("ID"), False)
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            RadGridMotivi.Rebind()
            connData.chiudi(True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " RadGridMotivi_NeedDataSource - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Protected Sub RadGridMotivi_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridMotivi.NeedDataSource
        Try
            par.cmd.CommandText = "SELECT T_MOTIVI_DINIEGO.ID,MOTIVI_TIPO_ISTANZA.ID AS ID2,(case when MOTIVI_TIPO_ISTANZA.ID_FASE_DECISIONE=1 " _
              & " then 'PREAVVISO DI DINIEGO' when MOTIVI_TIPO_ISTANZA.ID_FASE_DECISIONE=3 then 'ACCOGLIMENTO' when MOTIVI_TIPO_ISTANZA.ID_FASE_DECISIONE=5 then 'DINIEGO' END) as ACCOGLIMENTO," _
              & " FRASE_STAMPA,MOTIVI_TIPO_ISTANZA.ID_TIPO_ISTANZA AS IDTIPOISTANZA,MOTIVI_TIPO_ISTANZA.ID_TIPO_MOTIVO as IDMOTIVO,T_MOTIVI_DINIEGO.DESCRIZIONE AS MOTIVO,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE FROM T_MOTIVI_DINIEGO,MOTIVI_TIPO_ISTANZA,T_MOTIVO_DOMANDA_VSA WHERE T_MOTIVI_DINIEGO.ID=MOTIVI_TIPO_ISTANZA.ID_TIPO_MOTIVO" _
              & " And T_MOTIVO_DOMANDA_VSA.ID=MOTIVI_TIPO_ISTANZA.ID_TIPO_ISTANZA ORDER BY DESCRIZIONE ASC,T_MOTIVI_DINIEGO.DESCRIZIONE ASC"
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)

            TryCast(sender, RadGrid).DataSource = dt

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " RadGridMotivi_NeedDataSource - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        ' ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "MyScriptKey2", "function PaginaHome() {document.location.href = 'pagina_home.aspx';};PaginaHome();", True)
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub RadButtonElimina_Click(sender As Object, e As System.EventArgs) Handles RadButtonElimina.Click
        Try
            If IsNumeric(idSelectedTipoIst.Value) AndAlso idSelectedMotivo.Value > 0 Then
                connData.apri(True)
                par.cmd.CommandText = "DELETE FROM MOTIVI_TIPO_ISTANZA WHERE ID_TIPO_MOTIVO=" & idSelectedMotivo.Value & " AND ID_TIPO_ISTANZA=" & idSelectedTipoIst.Value
                par.cmd.ExecuteNonQuery()
                connData.chiudi(True)
            End If
            idSelectedTipoIst.Value = ""
            RadGridMotivi.Rebind()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " RadButtonElimina_Click - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Private Sub PulisciRadWindowMotDecis()
        Try
            cmbDecisioni.SelectedValue = "-1"
            cmbTipoIstanza.SelectedValue = "-1"
            txtFraseSt.Text = ""
            txtMotivo.Text = ""
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " PulisciRadWindowMotiviDecisioni - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Private Function Controlli() As Boolean
        Controlli = True
        Dim msg As String = ""

        If Me.cmbDecisioni.SelectedValue = -1 Then
            msg += "- Scegliere la decisione"

        End If
        If Me.cmbTipoIstanza.SelectedValue = -1 Then
            msg += "\n- Scegliere la tipologia istanza"

        End If
        If txtMotivo.Text = "" Then
            msg += "\n- Specificare il motivo"
        End If

        If Not String.IsNullOrEmpty(msg) Then
            Controlli = False

            par.MessaggioAlert(RadWindowManager1, "Compilare tutti i campi obbligatori.", 450, 150, "Attenzione", Nothing, Nothing)

        End If

    End Function

    Private Sub btnSalvaMotivoDec_Click(sender As Object, e As EventArgs) Handles btnSalvaMotivoDec.Click
        Try
            If Controlli() = True Then
                connData.apri(True)
                Dim idMotivoDin As Long = 0
                par.cmd.CommandText = "select SEQ_T_MOTIVI_DINIEGO.nextval from dual"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    idMotivoDin = lettore(0)
                End If
                lettore.Close()

                par.cmd.CommandText = "INSERT INTO T_MOTIVI_DINIEGO (" _
                    & "ID,DESCRIZIONE) " _
                    & "VALUES (" & idMotivoDin & ", " & par.insDbValue(txtMotivo.Text, True) & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO MOTIVI_TIPO_ISTANZA (ID," _
                        & " FRASE_STAMPA, ID_FASE_DECISIONE," _
                        & " ID_TIPO_MOTIVO, ID_TIPO_ISTANZA) " _
                        & " VALUES (SEQ_MOTIVI_TIPO_ISTANZA.nextval, " & par.insDbValue(txtFraseSt.Text, True) & "," _
                        & par.insDbValue(cmbDecisioni.SelectedValue, False) & "," _
                        & idMotivoDin & "," _
                        & par.insDbValue(cmbTipoIstanza.SelectedValue, False) & ")"
                par.cmd.ExecuteNonQuery()
                connData.chiudi(True)

                RadGridMotivi.Rebind()
                PulisciRadWindowMotDecis()
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "  btnSalvaMotivoDec_Click - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Private Sub btnChiudiMotivoDec_Click(sender As Object, e As EventArgs) Handles btnChiudiMotivoDec.Click
        PulisciRadWindowMotDecis()
    End Sub
End Class
