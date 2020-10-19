
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_GestModPod
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_UTENZE") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Not IsPostBack Then
            par.caricaComboTelerik("SELECT COD_FORNITORE||' - '||RAGIONE_SOCIALE AS DESCRIZIONE,ID  FROM SISCOM_MI.FORNITORI ORDER BY FORNITORI.RAGIONE_SOCIALE ASC", cmbFornitore, "ID", "DESCRIZIONE", False)
            par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_UTENZE ORDER BY DESCRIZIONE ASC", cmbTipoFornitura, "ID", "DESCRIZIONE", False)
            If Not IsNothing(Request.QueryString("idPod")) Then
                ID.Value = Request.QueryString("idPod")
                CaricaDati()
                Me.lblTitolo.Text = "MODIFICA POD"
            Else
                Me.lblTitolo.Text = "INSERIMENTO POD"
            End If
        End If
    End Sub
    Private Sub CaricaDati()
        Try
            connData.apri()
            par.cmd.CommandText = "select * from siscom_mi.pod where id = " & ID.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            cmbTipoFornitura.ClearSelection()
            cmbFornitore.ClearSelection()
            If dt.Rows.Count > 0 Then
                For Each r As Data.DataRow In dt.Rows
                    Me.txtContratto.Text = r.Item("CONTRATTO").ToString
                    Me.txtPod.Text = r.Item("POD").ToString
                    Me.cmbTipoFornitura.SelectedValue = r.Item("ID_TIPO_FORNITURA").ToString
                    Me.cmbFornitore.SelectedValue = r.Item("ID_FORNITORE").ToString
                    Me.txtDescrizione.Text = r.Item("DESCRIZIONE").ToString
                    If par.IfNull(r.Item("FL_ELIMINATO"), 0) = 0 Then
                        Me.chkAttivo.Checked = True
                    End If
                Next
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaDati - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Function ctrl(ByVal isInsert As Boolean) As Boolean
        ctrl = True
        Dim msgAnomalia As String = ""
        If String.IsNullOrEmpty(Me.txtContratto.Text) Then
            ctrl = False
            msgAnomalia += "\n- Definire il contratto!"
        End If
        If String.IsNullOrEmpty(Me.txtPod.Text) Then
            ctrl = False
            msgAnomalia += "\n- Definire il POD!"
        End If
        If Me.cmbTipoFornitura.SelectedValue = -1 Then
            ctrl = False
            msgAnomalia += "\n- Definire la FORNITURA!"
        End If

        If Me.cmbFornitore.SelectedValue = -1 Then
            ctrl = False
            msgAnomalia += "\n- Definire il FORNITORE!"
        End If
        If isInsert = True Then
            If ctrl = True Then
                connData.apri()
                par.cmd.CommandText = "select count(*) FROM siscom_mi.pod where " _
                                    & " ID_TIPO_FORNITURA = " & Me.cmbTipoFornitura.SelectedValue & " and " _
                                    & "ID_FORNITORE = " & Me.cmbFornitore.SelectedValue & " and  " _
                                    & "CONTRATTO = '" & par.PulisciStrSql(Me.txtContratto.Text) & "' and " _
                                    & "POD= '" & par.PulisciStrSql(Me.txtPod.Text) & "' "
                Dim ContaExist As Integer = 0
                ContaExist = par.IfNull(par.cmd.ExecuteScalar, 0)

                connData.chiudi()
                If ContaExist <> 0 Then
                    ctrl = False
                    msgAnomalia = "POD già presente!"

                End If
            End If

        End If
        If Not String.IsNullOrEmpty(msgAnomalia) Then
            RadWindowManager1.RadAlert(msgAnomalia, 300, 150, "Attenzione", Nothing, Nothing)
        End If
    End Function
    Private Sub Salva()
        Try
            connData.apri(True)

            par.cmd.CommandText = "select count(*) from siscom_mi.pod where id_fornitore=" & cmbFornitore.SelectedValue & " and id_tipo_fornitura=" & cmbTipoFornitura.SelectedValue & " and pod='" & txtPod.Text.Replace("'", "''") & "'"
            Dim ris As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If ris = 0 Then
                par.cmd.CommandText = "select siscom_mi.seq_pod.nextval from dual"
                Dim idNewPod As Integer = 0
                idNewPod = par.cmd.ExecuteScalar
                Dim isEliminato As Integer = 0
                If Me.chkAttivo.Checked = True Then
                    isEliminato = 0
                Else
                    isEliminato = 1
                End If
                par.cmd.CommandText = "insert into siscom_mi.pod (ID, ID_TIPO_FORNITURA, ID_FORNITORE, CONTRATTO, POD, DESCRIZIONE, FL_ELIMINATO) values " _
                    & "(" & idNewPod & "," & Me.cmbTipoFornitura.SelectedValue & "," & Me.cmbFornitore.SelectedValue & ", " _
                    & par.insDbValue(Me.txtContratto.Text, True) & "," & par.insDbValue(Me.txtPod.Text, True) & "," & par.insDbValue(Me.txtDescrizione.Text, True) & "," & isEliminato & ")"
                par.cmd.ExecuteNonQuery()
                connData.chiudi(True)
                ID.Value = idNewPod
                RadWindowManager1.RadAlert("Nuovo POD inserito correttamente! ", 300, 150, "Attenzione", "CloseAndRebind", Nothing)
            Else
                connData.chiudi(True)
                ID.Value = "0"
                RadWindowManager1.RadAlert("POD già presente!", 300, 150, "Attenzione", Nothing, Nothing)
            End If

            
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " Salva - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub Update()
        Try
            connData.apri(True)
            Dim isEliminato As Integer = 0
            If Me.chkAttivo.Checked = True Then
                isEliminato = 0
            Else
                isEliminato = 1
            End If
            par.cmd.CommandText = "update siscom_mi.pod set " _
                                & " ID_TIPO_FORNITURA = " & Me.cmbTipoFornitura.SelectedValue & " , " _
                                & "ID_FORNITORE = " & Me.cmbFornitore.SelectedValue & " ,  " _
                                & "CONTRATTO = " & par.insDbValue(Me.txtContratto.Text, True) & " , " _
                                & "POD= " & par.insDbValue(Me.txtPod.Text, True) & ", " _
                                & "DESCRIZIONE = " & par.insDbValue(Me.txtDescrizione.Text, True) & "," _
                                & "FL_ELIMINATO = " & isEliminato _
                                & "where id = " & ID.Value
            par.cmd.ExecuteNonQuery()
            connData.chiudi(True)
            RadWindowManager1.RadAlert("POD aggiornato correttamente!", 300, 150, "Attenzione", "CloseAndRebind", Nothing)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " Update - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If ID.Value = 0 Then
            If ctrl(True) = True Then
                Salva()
            End If
        Else
            If ctrl(False) = True Then
                Update()
            End If
        End If
    End Sub
End Class
