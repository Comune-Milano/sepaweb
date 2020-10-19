Imports Telerik.Web.UI

Partial Class Gestione_locatari_DetrazioniComponenti
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
            idDetrazioni.Value = Request.QueryString("DET")
            iddich.Value = Request.QueryString("IDD")
            HFBtnToClick.Value = Request.QueryString("BTN").ToString
            operazione.Value = Request.QueryString("O")
            par.caricaComboTelerik("SELECT * FROM T_TIPO_DETRAZIONI ORDER BY COD ASC", cmbDetrazione, "COD", "DESCRIZIONE", True)
            par.caricaComboTelerik("SELECT COMP_NUCLEO_VSA.ID,COMP_NUCLEO_VSA.COGNOME||' '||NOME AS NOMINATIVO FROM COMP_NUCLEO_VSA where COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & iddich.Value & " ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", True)

            If Not String.IsNullOrEmpty(idDetrazioni.Value.ToString) Then
                VisualizzaDetrazioni()
            End If
        End If
    End Sub

    Private Sub VisualizzaDetrazioni()
        Try
            Dim ApertaNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                ApertaNow = True
            End If

            par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_VSA WHERE ID=" & idDetrazioni.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                cmbComponente.SelectedValue = par.IfEmpty(lettore("ID_COMPONENTE"), "")
                cmbDetrazione.SelectedValue = par.IfEmpty(lettore("ID_TIPO"), "")
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

    Private Sub ScriviDetrazioni()

        connData.apri(True)

        If operazione.Value = "0" Then
            par.cmd.CommandText = "INSERT INTO COMP_DETRAZIONI_VSA (ID,ID_COMPONENTE,ID_TIPO,IMPORTO) VALUES (SEQ_COMP_DETRAZIONI_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & cmbDetrazione.SelectedValue & "," & par.VirgoleInPunti(txtImporto.Text) & ")"
            par.cmd.ExecuteNonQuery()
        Else
            par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_VSA WHERE ID=" & idDetrazioni.Value
            Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderI.Read Then
                par.cmd.CommandText = "UPDATE COMP_DETRAZIONI_VSA SET ID_COMPONENTE=" & cmbComponente.SelectedValue & ",ID_TIPO=" & cmbDetrazione.SelectedValue & ",IMPORTO=" & par.VirgoleInPunti(txtImporto.Text) & " WHERE ID=" & idDetrazioni.Value
                par.cmd.ExecuteNonQuery()
            End If
            myReaderI.Close()
        End If

        connData.chiudi(True)
        par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)

    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            ScriviDetrazioni()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
