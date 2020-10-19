
Partial Class CICLO_PASSIVO_GestModCustodi
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_UTENZE") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("IDCUST")) Then
                ID.Value = Request.QueryString("IDCUST")
                CaricaDati()
                Me.lblTitolo.Text = "MODIFICA CUSTODE"
            Else
                Me.lblTitolo.Text = "INSERIMENTO CUSTODE"
            End If
        End If
    End Sub
    Private Sub CaricaDati()
        Try
            connData.apri()
            par.cmd.CommandText = "select * from siscom_mi.ANAGRAFICA_CUSTODI where  ID = " & ID.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each r As Data.DataRow In dt.Rows
                    Me.txtMatricola.Text = r.Item("MATRICOLA").ToString
                    Me.txtCognome.Text = r.Item("COGNOME").ToString
                    Me.txtNome.Text = r.Item("NOME").ToString
                    Me.txtMail.Text = r.Item("EMAIL_AZIENDALE").ToString
                    Me.txtCel.Text = r.Item("CELLULARE_AZIENDALE").ToString
                    Me.txtTel.Text = r.Item("TELEFONO_AZIENDALE").ToString
                    Me.txtNote.Text = r.Item("NOTE").ToString
                    If par.IfEmpty(r.Item("FL_DIPENDENTE_MM").ToString, 0) = 1 Then
                        Me.chkDipMM.Checked = True
                    Else
                        Me.chkDipMM.Checked = False
                    End If
                    If par.IfEmpty(r.Item("FL_ALLOGGIO_ERP_ASSEGNATO").ToString, 0) = 1 Then
                        Me.chkAllErpAssegnato.Checked = True
                    Else
                        Me.chkAllErpAssegnato.Checked = False
                    End If
                Next
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " Salva - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function ctrl(ByVal isInsert As Boolean) As Boolean
        ctrl = True
        Dim msgAnomalia As String = ""
        If String.IsNullOrEmpty(Me.txtMatricola.Text) Then
            ctrl = False
            msgAnomalia += "\n- Definire la MATRICOLA!"
        End If

        If String.IsNullOrEmpty(Me.txtCognome.Text) Then
            ctrl = False
            msgAnomalia += "\n- Definire il COGNOME!"
        End If

        If String.IsNullOrEmpty(Me.txtNome.Text) Then
            ctrl = False
            msgAnomalia += "\n- Definire il NOME!"
        End If
        'If Me.cmbEdificio.SelectedValue <> "-1" Then
        '    If String.IsNullOrEmpty(Me.txtDataIniz.Text) Then
        '        ctrl = False
        '        msgAnomalia += "\n- Definire il la DATA INIZIO di custodia!"

        '    End If
        'End If
        If isInsert = True Then
            connData.apri()

            par.cmd.CommandText = "select count(*) from siscom_mi.anagrafica_custodi where matricola = " & par.insDbValue(Me.txtMatricola.Text, True) & " " _
                & " and cognome = " & par.insDbValue(Me.txtCognome.Text, True) & " " _
                & " and nome = " & par.insDbValue(Me.txtNome.Text, True) & " "
            Dim ContaExist As Integer = 0
            ContaExist = par.IfNull(par.cmd.ExecuteScalar, 0)

            connData.chiudi()
            If ContaExist <> 0 Then
                ctrl = False
                msgAnomalia += "\n- Esiste già un custode con gli stessi valori per MATRICOLA, COGNOME E NOME!"

            End If

        End If
        If Not String.IsNullOrEmpty(msgAnomalia) Then
            'RadWindowManager1.RadAlert("Impossibile procedere! " & msgAnomalia, 300, 150, "Attenzione", "function(sender, args){self.close();}", Nothing)
            RadWindowManager1.RadAlert("Impossibile procedere! " & msgAnomalia, 300, 150, "Attenzione", Nothing, Nothing)
            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('IMPOSSIBILE PROCEDERE!" & msgAnomalia & "');", True)
        End If
    End Function

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

    Private Sub Salva()
        Try
            connData.apri(True)

            Dim ris As Integer = 0
            par.cmd.CommandText = "SELECT count(*) FROM SISCOM_MI.ANAGRAFICA_CUSTODI WHERE UPPER(MATRICOLA)='" & txtMatricola.Text.ToString.Replace("'", "''") & "'"
            ris = par.IfNull(par.cmd.ExecuteScalar(), 0)

            If ris = 0 Then

            par.cmd.CommandText = "select siscom_mi.seq_anagrafica_custodi.nextval from dual"
            Dim idNewCustode As Integer = 0
            idNewCustode = par.cmd.ExecuteScalar
            Dim flDipMm As Integer = 0
            Dim flErpAssegnato As Integer = 0
            If Me.chkDipMM.Checked = True Then
                flDipMm = 1
            End If
            If Me.chkAllErpAssegnato.Checked = True Then
                flErpAssegnato = 1
            End If
            par.cmd.CommandText = "insert into siscom_mi.anagrafica_custodi (ID, MATRICOLA, COGNOME, NOME, FL_DIPENDENTE_MM, EMAIL_AZIENDALE, CELLULARE_AZIENDALE, TELEFONO_AZIENDALE, FL_ALLOGGIO_ERP_ASSEGNATO, NOTE) values " _
                                & "(" & idNewCustode & "," & par.insDbValue(Me.txtMatricola.Text.ToUpper, True) & "," & par.insDbValue(Me.txtCognome.Text.ToUpper, True) & ", " & par.insDbValue(Me.txtNome.Text.ToUpper, True) _
                                & "," & flDipMm & "," & par.insDbValue(Me.txtMail.Text, True) & "," & par.insDbValue(Me.txtCel.Text, True) & "," & par.insDbValue(Me.txtTel.Text, True) & "," & flErpAssegnato & "," & par.insDbValue(Me.txtNote.Text.ToUpper, True) & ")"

            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)

            ID.Value = idNewCustode
                RadWindowManager1.RadAlert("Nuovo custode inserito correttamente! ", 300, 150, "Attenzione", "CloseAndRebind", Nothing)

            Else
                connData.chiudi(True)

                RadWindowManager1.RadAlert("Matricola già presente!", 300, 150, "Attenzione", Nothing, Nothing)

            End If


        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " Salva - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub
    Private Sub Update()
        Try
            connData.apri(True)
            Dim flDipMm As Integer = 0
            Dim flErpAssegnato As Integer = 0
            If Me.chkDipMM.Checked = True Then
                flDipMm = 1
            End If
            If Me.chkAllErpAssegnato.Checked = True Then
                flErpAssegnato = 1
            End If

            par.cmd.CommandText = "update siscom_mi.anagrafica_custodi set " _
                                & "MATRICOLA = " & par.insDbValue(Me.txtMatricola.Text.ToUpper, True) & ", " _
                                & "COGNOME = " & par.insDbValue(Me.txtCognome.Text.ToUpper, True) & ", " _
                                & "NOME = " & par.insDbValue(Me.txtNome.Text.ToUpper, True) & ", " _
                                & "FL_DIPENDENTE_MM = " & flDipMm & " , " _
                                & "EMAIL_AZIENDALE = " & par.insDbValue(Me.txtMail.Text, True) & ", " _
                                & "CELLULARE_AZIENDALE = " & par.insDbValue(Me.txtCel.Text, True) & ", " _
                                & "TELEFONO_AZIENDALE = " & par.insDbValue(Me.txtTel.Text, True) & ", " _
                                & "FL_ALLOGGIO_ERP_ASSEGNATO = " & flErpAssegnato & ", " _
                                & "NOTE = " & par.insDbValue(Me.txtNote.Text.ToUpper, True) _
                                & " where id = " & ID.Value

            par.cmd.ExecuteNonQuery()
            'If Me.cmbEdificio.SelectedValue <> idEdifOld.Value Then
            '    SalvaCustodiaEdificio()
            'End If
            'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('CUSTODE aggiornato correttamente!');", True)
            connData.chiudi(True)
            RadWindowManager1.RadAlert("Custode aggiornato correttamente!", 300, 150, "Attenzione", "CloseAndRebind", Nothing)

        Catch ex As Exception
            connData.chiudi()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " Update - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

End Class