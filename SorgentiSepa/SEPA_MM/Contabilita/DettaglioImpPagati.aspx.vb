
Partial Class Contabilita_DettaglioImpPagati
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim idVoce As String = ""
    Dim idBolletta As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                Me.idVoce = par.IfEmpty(Request.QueryString("IDVOCE"), "")
                Me.idBolletta = par.IfEmpty(Request.QueryString("IDBOLLETTA"), "")

                If Not String.IsNullOrEmpty(idBolletta) Then
                    '******APERTURA CONNESSIONE*****
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If
                    If Not String.IsNullOrEmpty(idVoce) Then
                        par.cmd.CommandText = "SELECT NUM_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE WHERE ID = (SELECT ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID = " & idVoce & ")"
                        Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If reader.Read Then
                            Me.TxtTitolo.Text = "DETTAGLIO PAGAMENTI BOLLETTA " & par.IfNull(reader("NUM_BOLLETTA"), "N.D")
                        End If
                        reader.Close()
                        DettaglioVoce(idVoce)
                    End If

                    If String.IsNullOrEmpty(idVoce) And Not String.IsNullOrEmpty(idBolletta) Then
                        par.cmd.CommandText = "SELECT NUM_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE WHERE ID = " & idBolletta
                        Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If reader.Read Then
                            Me.TxtTitolo.Text = "DETTAGLIO PAGAMENTI BOLLETTA " & par.IfNull(reader("NUM_BOLLETTA"), "N.D")
                        End If
                        reader.Close()

                        par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & idBolletta
                        reader = par.cmd.ExecuteReader
                        While reader.Read
                            DettaglioVoce(par.IfNull(reader("ID"), 0))
                        End While
                        reader.Close()
                    End If
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End Try
        End If
    End Sub

    Private Sub DettaglioVoce(ByVal voce As String)
        Try
            Dim COLORE As String = "#E6E6E6"

            par.cmd.CommandText = "SELECT T_VOCI_BOLLETTA.DESCRIZIONE, BOL_BOLLETTE_VOCI.IMPORTO,IMP_PAGATO FROM SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE_VOCI.ID_VOCE = T_VOCI_BOLLETTA.ID AND BOL_BOLLETTE_VOCI.ID = " & voce
            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim totale As Decimal = 0
            If reader.Read Then
                ' informazioni intestazione
                TBL_DETTAGLI.Text = TBL_DETTAGLI.Text & "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                    & "<tr style='height: 25px;'></tr>" _
                    & "<tr>" _
                    & "<td>" _
                    & "<span style='font-size: 10pt; font-family: Arial; color: #0066FF;'><strong>" & par.IfNull(reader("DESCRIZIONE"), "n.d.") & " - IMPORTO €. " & Format(par.IfNull(reader("IMPORTO"), 0), "##,##0.00") & " - di cui PAGATO €. " & Format(par.IfNull(reader("IMP_PAGATO"), 0), "##,##0.00") & "</strong></span></td>" _
                    & "</tr>" _
                    & "</table>"

            End If
            reader.Close()
            TBL_DETTAGLI.Text = TBL_DETTAGLI.Text & "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                            & "<tr style='height: 25px;'></tr>" _
                            & "<tr>" _
                            & "<td style='height: 19px;'>" _
                            & "<span style='font-size: 10pt; font-family: Arial; '><strong>Elenco Storico Pagamenti  Extra M.A.V. Effettuati</strong></span></td>" _
                            & "</tr>" _
                            & "<tr>" _
                            & "<td style='height: 19px;'>" _
                            & "<span style='font-size: 8pt; font-family: Arial;'><strong> ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ </strong></span></td>" _
                            & "</tr>" _
                            & "</table>"

            TBL_DETTAGLI.Text = TBL_DETTAGLI.Text & "<table cellpadding='0' cellspacing='0' width='20%'>" & vbCrLf _
                                & "<tr>" _
                                & "<td style='text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC;border-right-style: ridge; border-right-width: 2px; border-right-color: #999999;'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>DATA PAGAMENTO</strong></span></td>" _
                                & "<td style='text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC;border-right-style: ridge; border-right-width: 2px; border-right-color: #999999;'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>IMPORTO</strong></span></td>" _
                                & "</tr>"
            'PEPPE MODIFY AGGIORNAMENTO ANNULLO OPERAZIONI 01/08/2011
            '10/07/2012 PEPPE MODIFY PER MODIFICHE PAGAMENTI EXTRAMAV
            ' ''par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.ID, descrizione,BOL_BOLLETTE_VOCI.IMPORTO, " _
            ' ''                    & "INCASSI_EXTRAMAV.data_pagamento," _
            ' ''                    & "EVENTI_PAGAMENTI_PARZ_DETT.IMPORTO AS VERSAMENTO " _
            ' ''                    & "FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI,SISCOM_MI.BOL_BOLLETTE_VOCI, siscom_mi.t_voci_bolletta, SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT,SISCOM_MI.INCASSI_EXTRAMAV " _
            ' ''                    & "WHERE ID_VOCE = T_VOCI_BOLLETTA.ID AND BOL_BOLLETTE_VOCI.ID = " & voce _
            ' ''                    & "AND EVENTI_PAGAMENTI_PARZIALI.ID = EVENTI_PAGAMENTI_PARZ_DETT.ID_EVENTO_PRINCIPALE AND INCASSI_EXTRAMAV.ID = EVENTI_PAGAMENTI_PARZIALI.ID_INCASSO_EXTRAMAV " _
            ' ''                    & "AND EVENTI_PAGAMENTI_PARZ_DETT.ID_VOCE_BOLLETTA = BOL_BOLLETTE_VOCI.ID AND EVENTI_PAGAMENTI_PARZIALI.FL_ANNULLATA = 0"
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.ID," _
                    & "       descrizione," _
                    & "       BOL_BOLLETTE_VOCI.IMPORTO," _
                    & "       INCASSI_EXTRAMAV.data_pagamento," _
                    & "       BOL_BOLLETTE_VOCI_PAGAMENTI.IMPORTO_PAGATO AS VERSAMENTO" _
                    & "  FROM " _
                    & "       siscom_mi.BOL_BOLLETTE_VOCI," _
                    & "       siscom_mi.t_voci_bolletta," _
                    & "       siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI," _
                    & "       siscom_mi.INCASSI_EXTRAMAV " _
                    & " WHERE ID_VOCE = T_VOCI_BOLLETTA.ID AND BOL_BOLLETTE_VOCI.ID = " & voce _
                    & "       AND INCASSI_EXTRAMAV.ID =" _
                    & "              BOL_BOLLETTE_VOCI_PAGAMENTI.ID_INCASSO_EXTRAMAV " _
                    & "       AND BOL_BOLLETTE_VOCI_PAGAMENTI.ID_VOCE_BOLLETTA = BOL_BOLLETTE_VOCI.ID " _
                    & "       AND INCASSI_EXTRAMAV.FL_ANNULLATA = 0 "

            reader = par.cmd.ExecuteReader
            '& "(SELECT BOL_BOLLETTE.DATA_PAGAMENTO FROM SISCOM_MI.BOL_BOLLETTE WHERE ID = " & idBolletta & ") AS DATA_PAGAMENTO," _

            ' informazioni pagamenti effettuati sulla voce
            While reader.Read
                TBL_DETTAGLI.Text = TBL_DETTAGLI.Text _
                                & "<tr bgcolor = '" & COLORE & "'>" _
                                & "<td style='height: 15px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC;border-right-style: ridge; border-right-width: 2px; border-right-color: #999999;'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'>" & par.FormattaData(par.IfNull(reader("DATA_PAGAMENTO"), "00/00/0000")) & "</span></td>" _
                                & "<td style='height: 15px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC;border-right-style: ridge; border-right-width: 2px; border-right-color: #999999;'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'>€." & Format((par.IfNull(reader("VERSAMENTO"), 0)), "##,##0.00") & "</span></td>" _
                                & "</tr>"
                totale = totale + CDec(par.IfNull(reader("VERSAMENTO"), 0))
                If COLORE = "#FFFFFF" Then
                    COLORE = "#E6E6E6"
                Else
                    COLORE = "#FFFFFF"
                End If

            End While
            reader.Close()
            TBL_DETTAGLI.Text = TBL_DETTAGLI.Text _
                & "<tr bgcolor = '" & COLORE & "'>" _
                & "<td style='height: 15px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC;border-right-style: ridge; border-right-width: 2px; border-right-color: #999999;'>" _
                & "<span style='font-size: 8pt; font-family: Arial;'>TOTALE </span></td>" _
                & "<td style='height: 15px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC;border-right-style: ridge; border-right-width: 2px; border-right-color: #999999;'>" _
                & "<span style='font-size: 8pt; font-family: Arial;'>€." & Format(totale, "##,##0.00") & "</span></td>" _
                & "</tr></table>"
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "DettaglioVoce - " & ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

End Class
