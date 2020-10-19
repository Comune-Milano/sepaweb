Imports Telerik.Web.UI
Partial Class Gestione_locatari_UscitaCompNucleo
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then
            idComp.Value = Request.QueryString("C")
            iddich.Value = Request.QueryString("IDD")
            HFBtnToClick.Value = Request.QueryString("BTN").ToString
            par.caricaComboTelerik("select * from MOTIVI_CANCELL_NUCLEO order by id asc", cmbMotivoUscita, "ID", "DESCRIZIONE", True, "null")

            If Not String.IsNullOrEmpty(idComp.Value.ToString) Then
                VisualizzaDati()
            End If
        End If
    End Sub
    Private Sub VisualizzaDati()
        Try
            Dim ApertaNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                ApertaNow = True
            End If

            Dim compCancell As Boolean = False
            par.cmd.CommandText = "select * from comp_nucleo_cancell where id_dichiarazione=" & iddich.Value & " AND COD_FISCALE=(SELECT cod_fiscale from comp_nucleo_vsa where id=" & idComp.Value & ")"
            Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore0.Read Then
                compCancell = True
                txtCognome.Text = par.IfNull(lettore0("COGNOME"), "")
                txtNome.Text = par.IfNull(lettore0("NOME"), "")
                txtDataNasc.SelectedDate = par.FormattaData(par.IfNull(lettore0("DATA_NASCITA"), ""))
                txtCF.Text = par.IfNull(lettore0("COD_FISCALE"), "")
                txtDataUscita.SelectedDate = par.FormattaData(par.IfNull(lettore0("DATA_USCITA"), ""))
            End If
            lettore0.Close()
            If compCancell = False Then
                par.cmd.CommandText = "select * from comp_nucleo_vsa where id=" & idComp.Value
                lettore0 = par.cmd.ExecuteReader
                If lettore0.Read Then
                    txtCognome.Text = par.IfNull(lettore0("COGNOME"), "")
                    txtNome.Text = par.IfNull(lettore0("NOME"), "")
                    txtDataNasc.SelectedDate = par.FormattaData(par.IfNull(lettore0("DATA_NASCITA"), ""))
                    txtCF.Text = par.IfNull(lettore0("COD_FISCALE"), "")
                End If
                lettore0.Close()
            End If

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

    Private Sub ScriviCompDaCancell()
        Try
            connData.apri(True)

            Dim progrComp As Integer = 0

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID=" & idComp.Value & " AND ID_DICHIARAZIONE=" & iddich.Value
            Dim myReaderCanc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderCanc.Read Then
                progrComp = myReaderCanc("PROGR")
            End If
            myReaderCanc.Close()

            If progrComp > 0 Then
                par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID=" & idComp.Value & " AND ID_DICHIARAZIONE=" & iddich.Value
                myReaderCanc = par.cmd.ExecuteReader()
                If myReaderCanc.Read Then

                    par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_CANCELL (ID_DICHIARAZIONE,NOME,COGNOME,DATA_NASCITA,COD_FISCALE,ID_MOTIVO,DATA_USCITA) VALUES " _
                        & "(" & iddich.Value & ",'" & par.PulisciStrSql(par.IfNull(myReaderCanc("NOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderCanc("COGNOME"), "")) & "'," _
                        & "'" & par.IfNull(myReaderCanc("DATA_NASCITA"), "") & "','" & par.IfNull(myReaderCanc("COD_FISCALE"), "") & "'," _
                        & cmbMotivoUscita.SelectedValue & ",'" & par.AggiustaData(par.IfEmpty(txtDataUscita.SelectedDate, "")) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderCanc.Close()


                par.cmd.CommandText = "SELECT * FROM NUOVI_COMP_NUCLEO_VSA,COMP_NUCLEO_VSA WHERE COMP_NUCLEO_VSA.ID = " _
                        & "NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE and ID_DICHIARAZIONE=" & iddich.Value & " AND ID_COMPONENTE=" & idComp.Value
                Dim myReaderN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderN.Read Then
                    par.cmd.CommandText = "DELETE FROM NUOVI_COMP_NUCLEO_VSA WHERE ID_COMPONENTE=" & myReaderN("ID_COMPONENTE")
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderN.Close()

                par.cmd.CommandText = "DELETE FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & iddich.Value & " AND ID=" & idComp.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM DOMANDE_REDDITI_VSA WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()

                par.NotificaTelerik(par.Messaggio_Operazione_Eff.ToString, CType(Me.Master.FindControl("RadNotificationMsg"), RadNotification), Me.Page)
            Else
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Impossibile eliminare il dichiarante dell\'istanza!", 450, 150, "Attenzione", Nothing, Nothing)
            End If

            connData.chiudi(True)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        Try
            ScriviCompDaCancell()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
