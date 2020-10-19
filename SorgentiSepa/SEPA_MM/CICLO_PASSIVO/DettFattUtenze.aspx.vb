
Partial Class CICLO_PASSIVO_DettFattUtenze
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../../Portale.aspx""</script>")
            Exit Sub

        End If
        Me.connData = New CM.datiConnessione(par, False, False)

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Not IsPostBack Then
            CaricaEsercizio()
        End If
    End Sub
    Private Sub CaricaEsercizio()

        Dim sql As String = ""
        sql = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY')||' - '||TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY')||' '||SISCOM_MI.PF_STATI.DESCRIZIONE as descrizione " _
                    & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                    & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                    & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                    & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by id desc"
        par.caricaComboBox(sql, cmbEsercizio, "ID", "descrizione", True)

        par.caricaComboBox("select id,descrizione from siscom_mi.TIPO_UTENZE order by descrizione asc", cmbTipoTracciato, "id", "descrizione", True)
        par.caricaComboBox("select cod_fornitore||' - '||ragione_sociale as descrizione,id  from siscom_mi.fornitori order by fornitori.ragione_sociale asc", cmbFornitore, "id", "descrizione", True)
        par.caricaComboBox("select id,nome from siscom_mi.tab_filiali  order by nome asc", cmbStruttra, "id", "nome", True)


    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        If Me.cmbEsercizio.SelectedValue <> -1 Then
            par.caricaComboBox("select id,codice||' - '||descrizione as descrizione from siscom_mi.pf_voci where id_voce_madre is not null and id_piano_finanziario =" & Me.cmbEsercizio.SelectedValue & " /*and id in (select id_voce from siscom_mi.pf_voci_importo)*/ order by codice asc", cmbPfVoci, "id", "descrizione", True)
            par.caricaComboBox("SELECT * FROM siscom_mi.TAB_SERVIZI WHERE ID IN (SELECT id_servizio FROM siscom_mi.TAB_SERVIZI_VOCI WHERE id_voce IN (SELECT ID FROM siscom_mi.PF_VOCI WHERE  id_piano_finanziario =" & Me.cmbEsercizio.SelectedValue & ")) and id in (select id_servizio from siscom_mi.pf_voci_importo) ", cmbServizio, "ID", "descrizione", True)
        Else
            Me.cmbPfVoci.Items.Clear()
            Me.cmbServizio.Items.Clear()
            Me.cmbPfVociImporto.Items.Clear()
        End If
    End Sub
    Private Sub CaricaPfVociImporto()
        If Me.cmbPfVoci.SelectedValue <> "-1" And Me.cmbServizio.SelectedValue <> "-1" Then
            par.caricaComboBox("SELECT ID,descrizione FROM siscom_mi.PF_VOCI_IMPORTO WHERE id_voce =" & Me.cmbPfVoci.SelectedValue & "  AND id_servizio =" & Me.cmbServizio.SelectedValue & " AND id_lotto = (SELECT ID FROM siscom_mi.LOTTI WHERE id_filiale = " & Session.Item("ID_STRUTTURA") & " AND id_esercizio_finanziario = (SELECT ID_ESERCIZIO_FINANZIARIO FROM siscom_mi.PF_MAIN WHERE ID = " & Me.cmbEsercizio.SelectedValue & ")) order by descrizione asc", cmbPfVociImporto, "ID", "DESCRIZIONE", True)
        Else
            Me.cmbPfVociImporto.Items.Clear()
        End If

    End Sub
    Protected Sub cmbServizio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbServizio.SelectedIndexChanged
        CaricaPfVociImporto()
    End Sub

    Protected Sub cmbPfVoci_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPfVoci.SelectedIndexChanged
        CaricaPfVociImporto()
    End Sub
    Private Function Controlli() As Boolean
        Controlli = True
        Dim msg As String = ""
        If Me.cmbStruttra.SelectedValue = -1 Then
            msg += "\n- Scegliere la struttura;"

        End If
        If Me.cmbEsercizio.SelectedValue = -1 Then
            msg += "\n- Scegliere il piano finanziario;"

        End If
        If Me.cmbTipoTracciato.SelectedValue = -1 Then
            msg += "\n- Scegliere il tipo tracciato;"

        End If
        If Me.cmbFornitore.SelectedValue = -1 Then
            msg += "\n- Scegliere il fornitore;"

        End If
        If par.IfEmpty(Me.cmbPfVoci.SelectedValue, -1) = -1 Then
            msg += "\n- Scegliere la voce del B.P.;"
        End If
        '************************PUCCIA MODIFICA DEL 06/07/2015**********************************
        'ESCLUSIONE OBBLIGATORIETA' PF_VOCI_IMPORTO, PER POSSIBILITA DI CREARE ORDINI SU CAPITOLI DI SPESA SENZA RECORD IN PF_VOCI_IMPORTO.

        'If par.IfEmpty(Me.cmbServizio.SelectedValue, -1) = -1 Then
        '    msg += "\n- Scegliere il servizio;"
        'End If
        'If par.IfEmpty(Me.cmbPfVociImporto.SelectedValue, -1) = -1 Then
        '    msg += "\n- Scegliere la voce servizio;"
        'End If
        '************************PUCCIA FINE MODIFICA DEL 06/07/2015*****************************

        If String.IsNullOrEmpty(msg) Then
            Dim condizVociImp As String = ""
            connData.apri()
            If par.IfEmpty(Me.cmbPfVociImporto.SelectedValue, -1) <> -1 Then
                condizVociImp = " AND ID_VOCE_PF_IMPORTO = " & Me.cmbPfVociImporto.SelectedValue & " "
            End If
            par.cmd.CommandText = "select * from siscom_mi.PAGAMENTI_UTENZE_VOCI where ID_PIANO_FINANZIARIO = " & Me.cmbEsercizio.SelectedValue & " AND ID_VOCE_PF = " & Me.cmbPfVoci.SelectedValue _
                                & " " & condizVociImp & " and ID_FORNITORE = " & Me.cmbFornitore.SelectedValue & " and id_tipo_utenza = " & Me.cmbTipoTracciato.SelectedValue
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                msg += "\n- Esiste già questo abbinamento"
            End If
            lettore.Close()
            connData.chiudi()
        End If

        If Not String.IsNullOrEmpty(msg) Then
            Controlli = False

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('Impossibile procedere! " & msg & "')", True)
        End If

    End Function
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            If Controlli() = True Then
                connData.apri(True)
                par.cmd.CommandText = "insert into siscom_mi.pagamenti_utenze_voci (id,ID_PIANO_FINANZIARIO, ID_TIPO_UTENZA, ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_FORNITORE, ID_STRUTTURA) values " _
                                    & "(siscom_mi.seq_pagamenti_utenze_voci.nextval," & Me.cmbEsercizio.SelectedValue & "," & Me.cmbTipoTracciato.SelectedValue & "," & Me.cmbPfVoci.SelectedValue & "," & par.insDbValue(Me.cmbPfVociImporto.SelectedValue, False, False, True) & "," & Me.cmbFornitore.SelectedValue & "," & Me.cmbStruttra.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()
                connData.chiudi(True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgOk", "alert('Operazione eseguita correttamente');self.close();", True)

            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnCarica_Click - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub


End Class
