
Partial Class GestioneAllegati_GestioneTipologia
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("ID")) Then
                If Request.QueryString("ID").ToString <> "-1" Then
                    HFidTipologia.Value = Request.QueryString("ID")
                    HFOggetto.Value = Request.QueryString("O")
                    lblTitolo.Text = "Modifica di una Tipologia"
                    CaricaTipologia()
                Else
                    HFidTipologia.Value = -1
                    HFOggetto.Value = Request.QueryString("O")
                    lblTitolo.Text = "Inserimento di un nuova Tipologia"
                End If
            Else
                par.modalDialogMessage("Attenzione", "Tipologia non disponibile! Contattare l\'Amministratore del Sistema.", Me.Page, , , True)
            End If
        End If
    End Sub
    Private Sub CaricaTipologia()
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT DESCRIZIONE " _
                                & "FROM SISCOM_MI.ALLEGATI_WS_TIPI " _
                                & "WHERE ID = " & HFidTipologia.Value
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                txtTipologia.Text = par.IfNull(MyReader("DESCRIZIONE"), "")
            End If
            MyReader.Close()
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneTipologia - CaricaTipologia - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            If ControlloDati() Then
                If HFidTipologia.Value.ToString = "-1" Then
                    InsertTipologia()
                Else
                    UpdateTipologia()
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "Close();", True)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneTipologia - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function ControlloDati() As Boolean
        ControlloDati = False
        Try
            If String.IsNullOrEmpty(Trim(txtTipologia.Text)) Then
                par.modalDialogMessage("Attenzione", "Inserire la descrizione della Tipologia", Me.Page)
                Exit Function
            End If
            ControlloDati = True
        Catch ex As Exception
            ControlloDati = False
        End Try
    End Function
    Private Sub InsertTipologia()
        Try
            connData.apri(False)
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_WS_TIPI (ID, DESCRIZIONE, ID_OGGETTO) VALUES " _
                                & "(SISCOM_MI.SEQ_ALLEGATI_WS_TIPI.NEXTVAL, " & par.insDbValue(txtTipologia.Text, True) & ", " & HFOggetto.Value & ")"
            par.cmd.ExecuteNonQuery()
            ' par.modalDialogMessage("Operazione", "Operazione effettuata correttamente!", Me.Page, , , True)
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneTipologia - InsertTipologia - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub UpdateTipologia()
        Try
            connData.apri(False)
            par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS_TIPI SET DESCRIZIONE = " & par.insDbValue(txtTipologia.Text, True) & " " _
                                & "WHERE ID = " & HFidTipologia.Value
            par.cmd.ExecuteNonQuery()
            'par.modalDialogMessage("Operazione", "Operazione effettuata correttamente!", Me.Page, , , True)
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: GestioneTipologia - UpdateTipologia - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
