
Partial Class Contratti_RicercaDepCauzionali
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub txtDataRimborso_TextChanged(sender As Object, e As System.EventArgs) Handles txtDataRimborso.TextChanged

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            txtCodContr.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtRagioneSociale.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtLibro.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtBolla.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            'txtDataCostituzione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'txtDataCostituzioneAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtDataRimborso.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataRimborsoAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtDataRestituzione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataRestituzioneAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            CaricaProvenienza()

        End If
    End Sub

    Private Sub CaricaProvenienza()
        Dim ds As New Data.DataSet()
        Dim dlist As CheckBoxList
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            dlist = ChProvenienza
            da = New Oracle.DataAccess.Client.OracleDataAdapter("select * from siscom_mi.TAB_PROVENIENZA_DEP ORDER BY DESCRIZIONE ASC", par.OracleConn)
            da.Fill(ds)
            dlist.Items.Clear()
            dlist.DataSource = ds
            dlist.DataTextField = "DESCRIZIONE"
            dlist.DataValueField = "ID"
            dlist.DataBind()
            da.Dispose()
            da = Nothing
            dlist.DataSource = Nothing
            dlist = Nothing
            ds.Clear()
            ds.Dispose()
            ds = Nothing
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            Dim bTrovato As Boolean
            Dim sValore As String
            Dim sCompara As String

            bTrovato = False
            Dim sStringaSql As String = ""
            Dim sStringa As String = ""

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
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.NOME) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtRagioneSociale.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = UCase(txtRagioneSociale.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(ANAGRAFICA.RAGIONE_SOCIALE) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtCodContr.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = UCase(txtCodContr.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(RAPPORTI_UTENZA.COD_CONTRATTO) " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            'If txtDataCostituzione.Text <> "" Then
            '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            '    sValore = par.AggiustaData(txtDataCostituzione.Text)
            '    If InStr(sValore, "*") Then
            '        sCompara = " LIKE "
            '        Call par.ConvertiJolly(sValore)
            '    Else
            '        sCompara = " = "
            '    End If
            '    bTrovato = True
            '    sStringaSql = sStringaSql & " BOL_BOLLETTE.DATA_PAGAMENTO>='" & par.PulisciStrSql(sValore) & "' "
            'End If

            'If txtDataCostituzioneAL.Text <> "" Then
            '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            '    sValore = par.AggiustaData(txtDataCostituzioneAL.Text)
            '    If InStr(sValore, "*") Then
            '        sCompara = " LIKE "
            '        Call par.ConvertiJolly(sValore)
            '    Else
            '        sCompara = " = "
            '    End If
            '    bTrovato = True
            '    sStringaSql = sStringaSql & " BOL_BOLLETTE.DATA_PAGAMENTO<='" & par.PulisciStrSql(sValore) & "' "
            'End If

            If txtDataRimborso.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = par.AggiustaData(txtDataRimborso.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA_DEP_CAUZ.DATA_OPERAZIONE>='" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtDataRimborsoAL.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = par.AggiustaData(txtDataRimborsoAL.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA_DEP_CAUZ.DATA_OPERAZIONE<='" & par.PulisciStrSql(sValore) & "' "
            End If


            If txtDataRestituzione.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = par.AggiustaData(txtDataRestituzione.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA_DEP_CAUZ.DATA_MANDATO>='" & par.PulisciStrSql(sValore) & "' "
            End If

            If txtDataRestituzioneAL.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = par.AggiustaData(txtDataRestituzioneAL.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA_DEP_CAUZ.DATA_MANDATO<='" & par.PulisciStrSql(sValore) & "' "
            End If


            If txtImportoDa.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = par.VirgoleInPunti(txtImportoDa.Text)

                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.IMP_DEPOSITO_CAUZ>=" & par.PulisciStrSql(sValore) & " "
            End If

            If txtImportoA.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = par.VirgoleInPunti(txtImportoA.Text)

                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.IMP_DEPOSITO_CAUZ<=" & par.PulisciStrSql(sValore) & " "
            End If

            If txtLibro.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = UCase(txtLibro.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(RAPPORTI_UTENZA_DEP_PROV.LIBRO)=" & par.PulisciStrSql(sValore) & " "
            End If

            If txtBolla.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = UCase(txtBolla.Text)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UPPER(RAPPORTI_UTENZA_DEP_PROV.BOLLA)=" & par.PulisciStrSql(sValore) & " "
            End If

            Dim ElencoProvenienze As String = ""

            For i = 0 To ChProvenienza.Items.Count - 1
                If ChProvenienza.Items(i).Selected Then
                    ElencoProvenienze = ElencoProvenienze & ChProvenienza.Items(i).Value & ","
                End If
            Next
            If ElencoProvenienze <> "" Then
                ElencoProvenienze = Mid(ElencoProvenienze, 1, Len(ElencoProvenienze) - 1)
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA_DEP_PROV.PROVENIENZA IN (" & ElencoProvenienze & ") "
            End If

            If cmbRestituibile.selecteditem.value <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " STORICO_DEP_CAUZIONALE.restituibile=" & cmbRestituibile.selecteditem.value
            End If



            sStringa = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO," _
                & "(CASE WHEN ANAGRAFICA.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (COGNOME || ' ' || ANAGRAFICA.NOME)) END) AS INTESTATARIO," _
                & "TO_CHAR (TO_DATE (STORICO_DEP_CAUZIONALE.DATA, 'YYYYmmdd'),'DD/MM/YYYY') AS DATA_EMISSIONE," _
                & "getdata (NVL (STORICO_DEP_CAUZIONALE.DATA_COSTITUZIONE,'')) AS DATA_PAGAMENTO," _
                & "(CASE WHEN STORICO_DEP_CAUZIONALE.IMPORTO IS NULL THEN RAPPORTI_UTENZA.IMP_DEPOSITO_CAUZ ELSE STORICO_DEP_CAUZIONALE.IMPORTO END) AS IMP_DEPOSITO_CAUZ," _
                & "TAB_PROVENIENZA_DEP.DESCRIZIONE AS PROVENIENZA," _
                & "RAPPORTI_UTENZA_DEP_PROV.LIBRO,RAPPORTI_UTENZA_DEP_PROV.BOLLA," _
                & "(CASE WHEN RAPPORTI_UTENZA_DEP_CAUZ.DATA_OPERAZIONE IS NOT NULL THEN TO_CHAR (TO_DATE (RAPPORTI_UTENZA_DEP_CAUZ.DATA_OPERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') ELSE TO_CHAR (TO_DATE (STORICO_DEP_CAUZIONALE.DATA_RESTITUZIONE,'YYYYmmdd'),'DD/MM/YYYY') END) AS DATA_RIMBORSO," _
                & "(CASE WHEN RAPPORTI_UTENZA_DEP_CAUZ.DATA_MANDATO IS NOT NULL THEN TO_CHAR (TO_DATE (RAPPORTI_UTENZA_DEP_CAUZ.DATA_MANDATO, 'YYYYmmdd'),'DD/MM/YYYY') ELSE TO_CHAR (TO_DATE (STORICO_DEP_CAUZIONALE.DATA_RESTITUZIONE,'YYYYmmdd'),'DD/MM/YYYY') END) AS DATA_RESTITUZIONE," _
                & "STORICO_DEP_CAUZIONALE.IMPORTO_RESTITUZIONE," _
                & "STORICO_DEP_CAUZIONALE.NOTE,DECODE(STORICO_DEP_CAUZIONALE.RESTITUIBILE,0,'NO',1,'SI') AS RESTITUIBILE " _
                & "FROM " _
                & "SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV,SISCOM_MI.TAB_PROVENIENZA_DEP,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ,SISCOM_MI.STORICO_DEP_CAUZIONALE,SISCOM_MI.ANAGRAFICA " _
                & "WHERE " _
                & "TAB_PROVENIENZA_DEP.id = RAPPORTI_UTENZA_DEP_PROV.PROVENIENZA AND ANAGRAFICA.ID (+)= STORICO_DEP_CAUZIONALE.ID_ANAGRAFICA AND " _
                & "STORICO_DEP_CAUZIONALE.ID_CONTRATTO(+) = RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA_DEP_CAUZ.ID_CONTRATTO(+) = RAPPORTI_UTENZA.ID AND " _
                & "SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV.ID_STORICO_DEP = STORICO_DEP_CAUZIONALE.ID  "
            If sStringaSql <> "" Then
                sStringa = sStringa & " AND " & sStringaSql
            End If
            sStringa = sStringa & " ORDER BY INTESTATARIO ASC,data_costituzione asc"

            Session.Add("DEP", sStringa)
            Response.Write("<script>location.replace('RisultatiDepCauzionali.aspx');</script>")
        Catch ex As Exception

        End Try

        'Response.Write("<script>location.replace('RisultatiRestDep.aspx?" & DatiDaPassare & "');</script>")
    End Sub
End Class
