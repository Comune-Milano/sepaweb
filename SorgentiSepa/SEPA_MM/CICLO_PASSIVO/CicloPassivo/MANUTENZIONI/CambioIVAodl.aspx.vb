Imports Telerik.Web.UI
Partial Class CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_CambioIVAodl
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_CAMBIO_IVA_ODL") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            svuotaTutto()
        End If
    End Sub
    Protected Sub ButtonCerca_Click(sender As Object, e As System.EventArgs) Handles ButtonCerca.Click
        Try
            HFRicalcolo.Value = "0"
            svuotaTutto()
            If IsNumeric(RadNumericTextBoxNumero.Text) AndAlso IsNumeric(RadNumericTextBoxNumero.Text) Then
                Dim stato As Int64 = 0
                connData.apri()
                par.cmd.CommandText = "SELECT (SELECT TIPO_PAGAMENTO FROM SISCOM_MI.PAGAMENTI WHERE PAGAMENTI.ID=MANUTENZIONI.ID_PAGAMENTO) AS TIPO," _
                    & " MANUTENZIONI.DESCRIZIONE, " _
                    & " (SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=MANUTENZIONI.ID_APPALTO) AS REPERTORIO," _
                    & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=(SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=MANUTENZIONI.ID_APPALTO)) AS FORNITORE," _
                    & " ID,STATO,(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE TAB_STATI_ODL.ID=MANUTENZIONI.STATO) AS STATO_DES " _
                    & " FROM SISCOM_MI.MANUTENZIONI WHERE PROGR=" & RadNumericTextBoxNumero.Text & " AND ANNO=" & RadNumericTextBoxAnno.Text
                Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lettore.Read Then
                    HFidManutenzione.Value = par.IfNull(Lettore("ID"), 0)
                    LabelStato.Text = par.IfNull(Lettore("STATO_DES"), "")
                    stato = par.IfNull(Lettore("STATO"), 0)
                    LabelRepertorio.Text = par.IfNull(Lettore("REPERTORIO"), "")
                    LabelFornitore.Text = par.IfNull(Lettore("FORNITORE"), "")
                    LabelDescrizione.Text = par.IfNull(Lettore("DESCRIZIONE"), "")
                    Dim tipo As String = "No"
                    If par.IfNull(Lettore("TIPO"), 0) = 3 Then
                        tipo = "Sì"
                    End If
                    LabelPatrimoniale.Text = tipo
                End If
                Lettore.Close()
                If CInt(HFidManutenzione.Value) > 0 Then
                    If stato = 4 Then
                        Dim risultatiCalcolo As Generic.Dictionary(Of String, Decimal) = par.CalcoloImportiOrdine(CM.Global.OrdiniIntervento.Ordine, CM.Global.PreventivoConsuntivo.Consuntivo, CInt(HFidManutenzione.Value), 0)
                        LabelSconto.Text = risultatiCalcolo("SCONTO") & " %"
                        LabelOneri.Text = risultatiCalcolo("ONERI") & " %"
                        LabelPercIVA.Text = risultatiCalcolo("PERC_IVA") & " %"
                        LabelLordoCompresiOneri.Text = risultatiCalcolo("LORDO_COMPRESI_ONERI") & " €"
                        LabelOneriDiSicurezza.Text = risultatiCalcolo("ONERI_SICUREZZA") & " €"
                        LabelLordoEsclusiOneri.Text = risultatiCalcolo("LORDO_ESCLUSI_ONERI") & " €"
                        LabelRibasso.Text = risultatiCalcolo("RIBASSO") & " €"
                        LabelNettoEsclusiOneri.Text = risultatiCalcolo("NETTO_ESCLUSI_ONERI") & " €"
                        LabelRitenutaIvata.Text = risultatiCalcolo("RIT_LEGGE_IVATA") & " €"
                        LabelNettoCompresiOneri.Text = risultatiCalcolo("NETTO_COMPRESI_ONERI") & " €"
                        LabelIVA.Text = risultatiCalcolo("IVA") & " €"
                        LabelRimborsi.Text = risultatiCalcolo("RIMBORSO") & " €"
                        LabelNettoCompresiOnerieIVA.Text = risultatiCalcolo("NETTO_COMPRESI_ONERI_IVA") & " €"
                        LabelPenale.Text = risultatiCalcolo("PENALE") & " €"
                        LabelTotale.Text = risultatiCalcolo("TOTALE") & " €"
                        LabelTotale.Font.Bold = True
                        IvaSbagliata.Text = risultatiCalcolo("PERC_IVA") & " %"
                        par.cmd.CommandText = "SELECT IMPORTO_CONSUNTIVATO FROM SISCOM_MI.PAGAMENTI WHERE ID=(SELECT ID_PAGAMENTO FROM SISCOM_MI.MANUTENZIONI WHERE ID=" & CInt(HFidManutenzione.Value) & ")"
                        LabelCDPold.Text = par.IfNull(par.cmd.ExecuteScalar, 0) & " €"
                        ButtonRicalcola.Enabled = True
                        ButtonAggiorna.Enabled = True
                    Else
                        connData.chiudi()
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msgC", "alert('Con questa funzione può essere modificata l\'IVA\nesclusivamente per ordini in stato EMESSO PAGAMENTO.');", True)
                        svuotaTutto()
                        Exit Sub
                    End If
                Else
                    connData.chiudi()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msgC", "alert('L\'ordine da elaborare non è stato individuato.\nRicercare correttamente numero ODL e anno.');", True)
                    svuotaTutto()
                    Exit Sub
                End If
                connData.chiudi()
            Else
                connData.chiudi()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msgC", "alert('L\'ordine da elaborare non è stato individuato.\nRicercare correttamente numero ODL e anno.');", True)
                svuotaTutto()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: CAMBIO IVA ODL - ButtonCerca_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub ButtonRicalcola_Click(sender As Object, e As System.EventArgs) Handles ButtonRicalcola.Click
        Try
            If IsNumeric(RadNumericTextBoxNumero.Text) AndAlso IsNumeric(RadNumericTextBoxNumero.Text) Then
                connData.apri()
                If CInt(HFidManutenzione.Value) > 0 Then
                    Dim risultatiCalcolo As Generic.Dictionary(Of String, Decimal) = par.CalcoloImportiOrdine(CM.Global.OrdiniIntervento.Ordine, CM.Global.PreventivoConsuntivo.Consuntivo, CInt(HFidManutenzione.Value), 0, IvaCorretta.SelectedValue)
                    LabelScontoC.Text = risultatiCalcolo("SCONTO") & " %"
                    LabelOneriC.Text = risultatiCalcolo("ONERI") & " %"
                    LabelPercIVAC.Text = risultatiCalcolo("PERC_IVA") & " %"
                    LabelLordoCompresiOneriC.Text = risultatiCalcolo("LORDO_COMPRESI_ONERI") & " €"
                    LabelOneriSicurezzaC.Text = risultatiCalcolo("ONERI_SICUREZZA") & " €"
                    LabelLordoEsclusiOneriC.Text = risultatiCalcolo("LORDO_ESCLUSI_ONERI") & " €"
                    LabelRibassoC.Text = risultatiCalcolo("RIBASSO") & " €"
                    LabelNettoEsclusiOneriC.Text = risultatiCalcolo("NETTO_ESCLUSI_ONERI") & " €"
                    LabelRitenutaIvataC.Text = risultatiCalcolo("RIT_LEGGE_IVATA") & " €"
                    LabelNettoCompresiOneriC.Text = risultatiCalcolo("NETTO_COMPRESI_ONERI") & " €"
                    LabelIVAC.Text = risultatiCalcolo("IVA") & " €"
                    LabelRimborsiC.Text = risultatiCalcolo("RIMBORSO") & " €"
                    LabelNettoCompresiOneriIVAC.Text = risultatiCalcolo("NETTO_COMPRESI_ONERI_IVA") & " €"
                    LabelPenaleC.Text = risultatiCalcolo("PENALE") & " €"
                    LabelTotaleC.Text = risultatiCalcolo("TOTALE") & " €"
                    LabelTotaleC.Font.Bold = True
                    Dim totale As Decimal = 0
                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE STATO=4 AND ID_PAGAMENTO IS NOT NULL AND ID_PAGAMENTO=(SELECT ID_PAGAMENTO FROM SISCOM_MI.MANUTENZIONI WHERE STATO=4 AND ID_PAGAMENTO IS NOT NULL AND ID=" & CInt(HFidManutenzione.Value) & ")"
                    Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    While Lettore.Read
                        If HFidManutenzione.Value = par.IfNull(Lettore("ID"), 0) Then
                            risultatiCalcolo = par.CalcoloImportiOrdine(CM.Global.OrdiniIntervento.Ordine, CM.Global.PreventivoConsuntivo.Consuntivo, par.IfNull(Lettore("ID"), 0), 0, IvaCorretta.SelectedValue)
                            totale += risultatiCalcolo("TOTALE") - risultatiCalcolo("RIT_LEGGE_IVATA")
                        Else
                            risultatiCalcolo = par.CalcoloImportiOrdine(CM.Global.OrdiniIntervento.Ordine, CM.Global.PreventivoConsuntivo.Consuntivo, par.IfNull(Lettore("ID"), 0), 0)
                            totale += risultatiCalcolo("TOTALE") - risultatiCalcolo("RIT_LEGGE_IVATA")
                        End If
                    End While
                    Lettore.Close()
                    LabelCDPnew.Text = totale & " €"
                    HFRicalcolo.Value = "1"
                Else
                    connData.chiudi()
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msgC", "alert('L\'ordine da elaborare non è stato individuato.\nRicercare correttamente numero ODL e anno.');", True)
                    svuotaTutto()
                    Exit Sub
                End If
                connData.chiudi()
            Else
                connData.chiudi()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msgC", "alert('L\'ordine da elaborare non è stato individuato.\nRicercare correttamente numero ODL e anno.');", True)
                svuotaTutto()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: CAMBIO IVA ODL - ButtonRicalcola_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub ButtonAggiorna_Click(sender As Object, e As System.EventArgs) Handles ButtonAggiorna.Click
        Try
            If IsNumeric(HFidManutenzione.Value) AndAlso HFRicalcolo.Value = "1" Then
                connData.apri(True)
                Dim ris As Integer = 0
                Dim idPrenotazione As Int64
                Dim ivaConsumo As Int64 = CInt(IvaCorretta.SelectedValue)
                Dim ritLegge As Decimal = CDec(LabelRitenutaIvataC.Text)
                Dim importoApprovato As Decimal = CDec(LabelTotaleC.Text)
                par.cmd.CommandText = "UPDATE SISCOM_MI.MANUTENZIONI " _
                    & " SET IVA_CONSUMO=" & ivaConsumo _
                    & " ,RIT_LEGGE=" & par.VirgoleInPunti(ritLegge) _
                    & " ,IMPORTO_TOT=" & par.VirgoleInPunti(importoApprovato) _
                    & " WHERE ID=" & HFidManutenzione.Value
                ris = par.cmd.ExecuteNonQuery()
                If ris <> 1 Then
                    connData.chiudi(False)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msgC", "alert('Non è stato possibile elaborare questo ordine.');", True)
                    svuotaTutto()
                    Exit Sub
                End If
                par.cmd.CommandText = "SELECT ID_PRENOTAZIONE_PAGAMENTO FROM SISCOM_MI.MANUTENZIONI WHERE ID=" & HFidManutenzione.Value
                idPrenotazione = par.IfNull(par.cmd.ExecuteScalar, 0)
                par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI " _
                    & " SET PERC_IVA=" & ivaConsumo _
                    & " ,RIT_LEGGE_IVATA=" & par.VirgoleInPunti(ritLegge) _
                    & " ,IMPORTO_APPROVATO=" & par.VirgoleInPunti(importoApprovato) _
                    & " WHERE ID=" & idPrenotazione
                ris = par.cmd.ExecuteNonQuery()
                If ris <> 1 Then
                    connData.chiudi(False)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msgC", "alert('Non è stato possibile elaborare questo ordine.');", True)
                    svuotaTutto()
                    Exit Sub
                End If
                If CheckBoxPagamento.Checked = True Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PAGAMENTI SET IMPORTO_CONSUNTIVATO=" & par.VirgoleInPunti(CDec(LabelCDPnew.Text)) _
                        & " WHERE ID=(SELECT ID_PAGAMENTO FROM SISCOM_MI.MANUTENZIONI WHERE ID=" & HFidManutenzione.Value & ")"
                    ris = par.cmd.ExecuteNonQuery()
                    If ris <> 1 Then
                        connData.chiudi(False)
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msgC", "alert('Non è stato possibile elaborare questo ordine.');", True)
                        svuotaTutto()
                        Exit Sub
                    End If
                End If
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_MANUTENZIONE " _
                    & " (ID_MANUTENZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) " _
                    & " VALUES (" & HFidManutenzione.Value & "," & Session.Item("ID_OPERATORE") & " , '" & Format(Now, "yyyyMMddHHmmss") & "', " _
                    & " 'F288', '" & RadTextBoxMotivazione.Text.ToString.Replace("'", "''") & "') "
                par.cmd.ExecuteNonQuery()
                connData.chiudi(True)
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msgC", "alert('Ordine elaborato correttamente!');", True)
                svuotaTutto()
                Exit Sub
            Else
                connData.chiudi()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msgC", "alert('L\'ordine da elaborare non è stato individuato.\nRicercare correttamente numero ODL e anno.');", True)
                svuotaTutto()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: CAMBIO IVA ODL - ButtonAggiorna_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub svuotaTutto()
        LabelSconto.Text = ""
        LabelOneri.Text = ""
        LabelPercIVA.Text = ""
        LabelLordoCompresiOneri.Text = ""
        LabelOneriDiSicurezza.Text = ""
        LabelLordoEsclusiOneri.Text = ""
        LabelRibasso.Text = ""
        LabelNettoEsclusiOneri.Text = ""
        LabelRitenutaIvata.Text = ""
        LabelNettoCompresiOneri.Text = ""
        LabelIVA.Text = ""
        LabelRimborsi.Text = ""
        LabelNettoCompresiOnerieIVA.Text = ""
        LabelPenale.Text = ""
        LabelTotale.Text = ""
        IvaSbagliata.Text = ""
        LabelScontoC.Text = ""
        LabelOneriC.Text = ""
        LabelPercIVAC.Text = ""
        LabelLordoCompresiOneriC.Text = ""
        LabelOneriSicurezzaC.Text = ""
        LabelLordoEsclusiOneriC.Text = ""
        LabelRibassoC.Text = ""
        LabelNettoEsclusiOneriC.Text = ""
        LabelRitenutaIvataC.Text = ""
        LabelNettoCompresiOneriC.Text = ""
        LabelIVAC.Text = ""
        LabelRimborsiC.Text = ""
        LabelNettoCompresiOneriIVAC.Text = ""
        LabelPenaleC.Text = ""
        LabelTotaleC.Text = ""
        HFRicalcolo.Value = "0"
        IvaCorretta.SelectedValue = 0
        LabelStato.Text = ""
        LabelCDPnew.Text = ""
        LabelCDPold.Text = ""
        ButtonRicalcola.Enabled = False
        ButtonAggiorna.Enabled = False
        LabelRepertorio.Text = ""
        LabelFornitore.Text = ""
        LabelDescrizione.Text = ""
        LabelPatrimoniale.Text = ""
    End Sub
End Class
