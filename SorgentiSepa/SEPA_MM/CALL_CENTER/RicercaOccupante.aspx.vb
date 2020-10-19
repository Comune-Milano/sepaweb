
Partial Class CALL_CENTER_RicercaOccupante
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            SettaEdifici()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            Dim bTrovato As Boolean
            Dim sValore As String
            Dim sCompara As String

            bTrovato = False
            sStringaSql = ""

            If txtCognome.Text <> "" Then
                sValore = UCase(txtCognome.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.COGNOME) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtNome.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = UCase(txtNome.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.NOME)" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtCF.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = UCase(txtCF.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.COD_FISCALE)" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtRS.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = UCase(txtRS.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.RAGIONE_SOCIALE)" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtIva.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = UCase(txtIva.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.PARTITA_IVA)" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
            End If

            If Me.cmbEdificio.SelectedValue <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                bTrovato = True

                sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.ID_EDIFICIO = " & Me.cmbEdificio.SelectedValue
            End If

            If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql

            sStringaSql = "select CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS ""INTESTATARIO"",unita_immobiliari.*,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,identificativi_catastali.SUB,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO from " _
                        & "SISCOM_MI.ANAGRAFICA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.unita_immobiliari,SISCOM_MI.identificativi_catastali,SISCOM_MI.edifici,SISCOM_MI.INDIRIZZI where " _
                        & "unita_immobiliari.cod_tipo_disponibilita<>'VEND' and unita_immobiliari.ID_INDIRIZZO=INDIRIZZI.ID (+) AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND " _
                        & "UNITA_IMMOBILIARI.ID_EDIFICIO=EDIFICI.ID (+) AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=UNITA_CONTRATTUALE.ID_CONTRATTO AND siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) LIKE '%IN CORSO%'  AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID " & sStringaSql & " ORDER BY INTESTATARIO ASC"


            Session.Add("PEDOCC", sStringaSql)
            Response.Redirect("RisultatiOccupante.aspx?T=0")
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub SettaEdifici()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        cmbEdificio.Items.Add(New ListItem("- - -", -1))

        par.cmd.CommandText = "SELECT EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where SISCOM_MI.EDIFICI.id <> 1 order by denominazione asc"

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader1.Read
            cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_edificio"), " "), par.IfNull(myReader1("id"), -1)))
        End While

        myReader1.Close()
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        cmbEdificio.Text = "-1"
        TxtDescInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        HelpRicerca()

    End Sub
    Private Sub HelpRicerca()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
                Me.ListEdifci.Items.Clear()

                par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' order by denominazione asc"


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    ListEdifci.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("ID"), -1)))
                End While
            End If
            If ListEdifci.Items.Count = 0 Then
                Me.LblNoResult.Visible = True
            Else
                Me.LblNoResult.Visible = False
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.TextBox1.Value = 2

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnConferma_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        Try
            If Me.ListEdifci.SelectedValue.ToString <> "" Then
                Me.cmbEdificio.SelectedValue = Me.ListEdifci.SelectedValue.ToString
                Me.TxtDescInd.Text = ""
                Me.ListEdifci.Items.Clear()
                Me.TextBox1.Value = 1
                Me.LblNoResult.Visible = False
            Else
                Me.TxtDescInd.Text = ""
                Me.ListEdifci.Items.Clear()
                Me.LblNoResult.Visible = False
                Me.TextBox1.Value = 1
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        Me.TextBox1.Value = 1
        Me.TxtDescInd.Text = ""
        Me.ListEdifci.Items.Clear()
    End Sub
End Class
