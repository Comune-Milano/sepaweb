Imports Telerik.Web.UI

Partial Class Gestione_locatari_PatrimonioMobComponenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then
            idMob.Value = Request.QueryString("IDMOB")
            iddich.Value = Request.QueryString("IDD")
            HFBtnToClick.Value = Request.QueryString("BTN").ToString
            operazione.Value = Request.QueryString("O")

            par.caricaComboTelerik("SELECT COMP_NUCLEO_VSA.ID,COMP_NUCLEO_VSA.COGNOME||' '||NOME AS NOMINATIVO FROM COMP_NUCLEO_VSA where COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & iddich.Value & " ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", True)
            cmbComponente.ClearSelection()
            par.caricaComboTelerik("SELECT * FROM TIPOLOGIA_PATR_MOB ORDER BY ID ASC", cmbTipoPatrim, "ID", "DESCRIZIONE", False)
            cmbTipoPatrim.ClearSelection()

            If Not String.IsNullOrEmpty(idMob.Value.ToString) Then
                VisualizzaPatrMob()
            End If
        End If
    End Sub
    Private Sub VisualizzaPatrMob()
        Try
            Dim ApertaNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                ApertaNow = True
            End If
            cmbTipoPatrim.ClearSelection()
            cmbComponente.ClearSelection()
            par.cmd.CommandText = "select * from COMP_PATR_MOB_VSA where id=" & idMob.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                cmbComponente.SelectedValue = par.IfNull(lettore("ID_COMPONENTE"), "")
                cmbTipoPatrim.SelectedValue = par.IfNull(lettore("ID_TIPO"), "")
                txtABI.Text = par.IfNull(lettore("COD_INTERMEDIARIO"), "")
                txtInter.Text = par.IfNull(lettore("INTERMEDIARIO"), "")
                txtImporto.Text = par.VirgoleInPunti(CStr(par.IfEmpty(lettore("IMPORTO").ToString.Replace(".", ""), 0))) 'Format(par.IfNull(lettore("importo"), "0").ToString, "##,##0.00")
            End If
            lettore.Close()

            If ApertaNow Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub ScriviPatrMob()

        connData.apri(True)

        If operazione.Value = "0" Then
            par.cmd.CommandText = "INSERT INTO COMP_PATR_MOB_VSA (ID, ID_COMPONENTE, COD_INTERMEDIARIO, INTERMEDIARIO, IMPORTO, ID_TIPO) VALUES (SEQ_COMP_PATR_MOB_VSA.NEXTVAL," & cmbComponente.SelectedValue & ",'" & txtABI.Text & "','" & par.PulisciStrSql(txtInter.Text) & "'," & par.VirgoleInPunti(par.IfEmpty(txtImporto.Text, 0)) & "," & par.IfEmpty(cmbTipoPatrim.SelectedValue, "null") & ")"
            par.cmd.ExecuteNonQuery()
        Else
            par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_VSA WHERE ID=" & idMob.Value
            Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderI.Read Then
                par.cmd.CommandText = "UPDATE COMP_PATR_MOB_VSA SET ID_COMPONENTE=" & cmbComponente.SelectedValue & ",ID_TIPO=" & cmbTipoPatrim.SelectedValue & ",IMPORTO=" & par.VirgoleInPunti(txtImporto.Text) & ",COD_INTERMEDIARIO='" & txtABI.Text & "',INTERMEDIARIO='" & par.PulisciStrSql(txtInter.Text) & "' WHERE ID=" & idMob.Value
                par.cmd.ExecuteNonQuery()
            End If
            myReaderI.Close()
        End If

        connData.chiudi(True)
        par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)

    End Sub


    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            ScriviPatrMob()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
