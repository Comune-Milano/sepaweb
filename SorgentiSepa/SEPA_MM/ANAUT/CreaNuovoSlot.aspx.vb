
Partial Class ANAUT_CreaNuovoSlot
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Carica()
        End If
        txtGiorno.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Function Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select agenda_appuntamenti.*,UTENZA_SPORTELLI.DESCRIZIONE AS NOME_S,UTENZA_SPORTELLI.DURATA from SISCOM_MI.agenda_appuntamenti,UTENZA_SPORTELLI where UTENZA_SPORTELLI.ID=AGENDA_APPUNTAMENTI.ID_SPORTELLO AND AGENDA_APPUNTAMENTI.ID_CONVOCAZIONE=" & Request.QueryString("IDC") & " ORDER BY AGENDA_APPUNTAMENTI.ID DESC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                txtDurata.Text = par.IfNull(myReader1("DURATA"), "")
                txtSede.Text = par.IfNull(myReader1("nome_s"), "")
            End If
            myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblerrore.Visible = True
            lblerrore.Text = ex.Message
        End Try
    End Function

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        If Len(txtGiorno.Text) = 10 Then
            If par.AggiustaData(txtGiorno.Text) < Format(Now, "yyyyMMdd") Then
                Response.Write("<script>alert('Data non valida! Non si possono creare slot per date passate.');</script>")
                Exit Sub
            End If
            Try
                Dim i As Integer = 0
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                Dim tsOrarioPartenza As New TimeSpan(cmbInizioM.SelectedItem.Text, cmbInizioM1.SelectedItem.Text, 0)
                Dim tsOraFineAppuntamento As TimeSpan
                tsOraFineAppuntamento = tsOrarioPartenza + TimeSpan.FromMinutes(txtDurata.Text)

                par.cmd.CommandText = "select agenda_appuntamenti.* from SISCOM_MI.agenda_appuntamenti where AGENDA_APPUNTAMENTI.ID_CONVOCAZIONE=" & Request.QueryString("IDC") & " ORDER BY ID DESC"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    For i = 0 To cmbOperatori.SelectedItem.Value - 1
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.AGENDA_APPUNTAMENTI (ID, ID_STATO, ID_FILIALE, ID_CONVOCAZIONE, ID_AU, N_OPERATORE, COD_CONTRATTO, COGNOME, NOME, INIZIO, FINE, MOTIVAZIONE_ANNULLO, ID_CONTRATTO, TIPO_F_ORARIA, ID_SPORTELLO) VALUES (SISCOM_MI.SEQ_AGENDA_APPUNTAMENTI.NEXTVAL, 0, " & par.IfNull(myReader1("ID_FILIALE"), "NULL") & ", NULL, " & par.IfNull(myReader1("ID_AU"), "NULL") & ", '1', NULL, NULL, NULL, '" & par.AggiustaData(txtGiorno.Text) & cmbInizioM.SelectedItem.Text & cmbInizioM1.SelectedItem.Text & "', '" & par.AggiustaData(txtGiorno.Text) & Format(tsOraFineAppuntamento.Hours, "00") & Format(tsOraFineAppuntamento.Minutes, "00") & "', NULL, NULL, 0, " & par.IfNull(myReader1("ID_SPORTELLO"), "NULL") & ")"
                        par.cmd.ExecuteNonQuery()
                    Next
                End If
                myReader1.Close()

                par.myTrans.Commit()
                'par.myTrans.Rollback()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione effettuata!');self.close();</script>")

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblerrore.Visible = True
                lblerrore.Text = ex.Message
            End Try
        Else
            Response.Write("<script>alert('Data non valida!');</script>")
        End If
    End Sub
End Class
