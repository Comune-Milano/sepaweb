Imports Telerik.Web.UI

Partial Class Gestione_locatari_Redditi_Componenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)

        If IsPostBack = False Then
            iddich.Value = Request.QueryString("IDD")
            HFBtnToClick.Value = Request.QueryString("BTN").ToString
            operazione.Value = Request.QueryString("O")
            idRedd.Value = par.IfEmpty(Request.QueryString("IDREDD"), 0)
            tipoDomanda.Value = Request.QueryString("TD")

            'If tipoDomanda.value = "7" Then
            '    par.caricaComboTelerik("SELECT COMP_NUCLEO_VSA.ID,COMP_NUCLEO_VSA.COGNOME||' '||NOME AS NOMINATIVO FROM COMP_NUCLEO_VSA where COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & iddich.Value & " AND (comp_nucleo_vsa.ID IN (SELECT ID_COMPONENTE FROM COMP_NUCLEO_OSPITI_VSA)) ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", False)
            'Else
            par.caricaComboTelerik("SELECT COMP_NUCLEO_VSA.ID,COMP_NUCLEO_VSA.COGNOME||' '||NOME AS NOMINATIVO FROM COMP_NUCLEO_VSA where COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & iddich.Value & " ORDER BY PROGR ASC", cmbComponente, "ID", "NOMINATIVO", False)
            'End If
            If operazione.Value = "1" Then
                cmbComponente.Enabled = False
            End If
        End If
    End Sub

    Protected Sub RadGridDipend_BatchEditCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridBatchEditingEventArgs) Handles RadGridDipend.BatchEditCommand
        Try
            connData.apri(True)

            Dim reddPresente As Boolean = False
            Dim idUtenzaRedd1 As Long = 0
            par.cmd.CommandText = "SELECT DOMANDE_REDDITI_VSA.* FROM DOMANDE_REDDITI_VSA,VSA_REDD_DIPEND_IMPORTI WHERE DOMANDE_REDDITI_VSA.ID=VSA_REDD_DIPEND_IMPORTI.ID_REDD_TOT " _
                & " and NVL(DIPENDENTE,'0')<>'0' and ID_DOMANDA=" & iddich.Value & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue & " and VSA_REDD_DIPEND_IMPORTI.id_redd_tot=" & idRedd.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                reddPresente = True
                idUtenzaRedd1 = myReader0(0)
            Else
                par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & iddich.Value & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                Dim myReaderCon As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCon.Read Then
                    idUtenzaRedd1 = myReaderCon(0)
                Else
                    par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID, ID_DOMANDA, ID_COMPONENTE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & iddich.Value & "," & cmbComponente.SelectedValue & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_UTENZA_REDDITI.CURRVAL FROM DUAL"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idUtenzaRedd1 = myReader(0)
                    End If
                    myReader.Close()
                End If
                myReaderCon.Close()
            End If
            myReader0.Close()

            For Each command As GridBatchEditingCommand In e.Commands
                Dim newValues As Hashtable = command.NewValues
                Dim oldValues As Hashtable = command.OldValues
                If command.Type = GridBatchEditingCommandType.Update Then
                    If newValues("IMPORTO") = oldValues("IMPORTO") And newValues("NUM_GG") = oldValues("NUM_GG") Then
                        newValues("IMPORTO") = 0
                    End If
                    If reddPresente = True Then
                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_DIPEND_IMPORTI WHERE ID_REDD_TOT=" & idUtenzaRedd1
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtQuery As New Data.DataTable

                        da.Fill(dtQuery)
                        da.Dispose()
                        If dtQuery.Rows.Count > 0 Then
                            For Each row As Data.DataRow In dtQuery.Rows
                                If row.Item("id") = newValues("IDIMPORTI") Then
                                    If newValues("IMPORTO") <> 0 Then
                                        par.cmd.CommandText = "UPDATE VSA_REDD_DIPEND_IMPORTI SET IMPORTO=" & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                        & " NUM_GG=" & par.insDbValue(newValues("NUM_GG"), False) & " WHERE ID=" & par.insDbValue(newValues("IDIMPORTI"), False)
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    Else
                                        par.cmd.CommandText = "DELETE FROM VSA_REDD_DIPEND_IMPORTI WHERE ID=" & newValues("IDIMPORTI")
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    End If
                                End If
                                If newValues("IDIMPORTI") = 0 Then
                                    If newValues("IMPORTO") <> 0 Then
                                        par.cmd.CommandText = "INSERT INTO VSA_REDD_DIPEND_IMPORTI (ID, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                                        & "VALUES (SEQ_VSA_REDD_DIPEND_IMPORTI.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                        & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    End If
                                End If
                            Next
                        Else
                            If newValues("IMPORTO") <> 0 Then
                                par.cmd.CommandText = "INSERT INTO VSA_REDD_DIPEND_IMPORTI (ID, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                                & "VALUES (SEQ_VSA_REDD_DIPEND_IMPORTI.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If


                    Else
                        If newValues("IMPORTO") <> 0 Then
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_DIPEND_IMPORTI (ID, ID_REDD_DIPENDENTE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                            & "VALUES (SEQ_VSA_REDD_DIPEND_IMPORTI.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                            & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If

                    If newValues("IMPORTO") = oldValues("IMPORTO") Then
                        newValues("IMPORTO") = 0
                    End If
                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderR.Read Then
                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET PROV_AGRARI=0,REDDITO_IRPEF=" & par.VirgoleInPunti(newValues("IMPORTO") + (par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - oldValues("IMPORTO"))) & "" _
                            & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID, ID_COMPONENTE, REDDITO_IRPEF) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & par.VirgoleInPunti(newValues("IMPORTO")) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderR.Close()

                    par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET DIPENDENTE=nvl(DIPENDENTE,0) +" & par.VirgoleInPunti(newValues("IMPORTO") - oldValues("IMPORTO")) & " WHERE ID=" & idUtenzaRedd1
                    Dim ris = par.cmd.ExecuteNonQuery()
                End If
            Next
            RadGridDipend.Rebind()
            connData.chiudi(True)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridAutonomo_BatchEditCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridBatchEditingEventArgs) Handles RadGridAutonomo.BatchEditCommand
        Try
            connData.apri(True)

            Dim reddPresente As Boolean = False
            Dim idUtenzaRedd1 As Long = 0
            par.cmd.CommandText = "SELECT DOMANDE_REDDITI_VSA.* FROM DOMANDE_REDDITI_VSA,VSA_REDD_AUTONOMO_IMPORTI WHERE DOMANDE_REDDITI_VSA.ID=VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_TOT " _
                & " and NVL(DIPENDENTE,'0')<>'0' and ID_DOMANDA=" & iddich.Value & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue & " and VSA_REDD_AUTONOMO_IMPORTI.id_redd_tot=" & idRedd.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                reddPresente = True
                idUtenzaRedd1 = myReader0(0)
            Else
                par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & iddich.Value & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                Dim myReaderCon As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCon.Read Then
                    idUtenzaRedd1 = myReaderCon(0)
                Else
                    par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID, ID_DOMANDA, ID_COMPONENTE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & iddich.Value & "," & cmbComponente.SelectedValue & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_UTENZA_REDDITI.CURRVAL FROM DUAL"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idUtenzaRedd1 = myReader(0)
                    End If
                    myReader.Close()
                End If
                myReaderCon.Close()
            End If
            myReader0.Close()

            For Each command As GridBatchEditingCommand In e.Commands
                Dim newValues As Hashtable = command.NewValues
                Dim oldValues As Hashtable = command.OldValues
                If command.Type = GridBatchEditingCommandType.Update Then
                    If newValues("IMPORTO") = oldValues("IMPORTO") And newValues("NUM_GG") = oldValues("NUM_GG") Then
                        newValues("IMPORTO") = 0
                    End If
                    If reddPresente = True Then
                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID_REDD_TOT=" & idUtenzaRedd1
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtQuery As New Data.DataTable

                        da.Fill(dtQuery)
                        da.Dispose()
                        If dtQuery.Rows.Count > 0 Then
                            For Each row As Data.DataRow In dtQuery.Rows
                                If row.Item("id") = newValues("IDIMPORTI") Then
                                    If newValues("IMPORTO") <> 0 Then
                                        par.cmd.CommandText = "UPDATE VSA_REDD_AUTONOMO_IMPORTI SET IMPORTO=" & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                        & " NUM_GG=" & par.insDbValue(newValues("NUM_GG"), False) & " WHERE ID=" & par.insDbValue(newValues("IDIMPORTI"), False)
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    Else
                                        par.cmd.CommandText = "DELETE FROM VSA_REDD_AUTONOMO_IMPORTI WHERE ID=" & newValues("IDIMPORTI")
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    End If
                                End If
                                If newValues("IDIMPORTI") = 0 Then
                                    If newValues("IMPORTO") <> 0 Then
                                        par.cmd.CommandText = "INSERT INTO VSA_REDD_AUTONOMO_IMPORTI (ID, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                                        & "VALUES (SEQ_VSA_REDD_AUTON_IMPORTI.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                        & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    End If
                                End If
                            Next
                        Else
                            If newValues("IMPORTO") <> 0 Then
                                par.cmd.CommandText = "INSERT INTO VSA_REDD_AUTONOMO_IMPORTI (ID, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                                & "VALUES (SEQ_VSA_REDD_AUTON_IMPORTI.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If


                    Else
                        If newValues("IMPORTO") <> 0 Then
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_AUTONOMO_IMPORTI (ID, ID_REDD_AUTONOMO, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                            & "VALUES (SEQ_VSA_REDD_AUTON_IMPORTI.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                            & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If

                    If newValues("IMPORTO") = oldValues("IMPORTO") Then
                        newValues("IMPORTO") = 0
                    End If
                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderR.Read Then
                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET PROV_AGRARI=0,REDDITO_IRPEF=" & par.VirgoleInPunti(newValues("IMPORTO") + (par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - oldValues("IMPORTO"))) & "" _
                            & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID, ID_COMPONENTE, REDDITO_IRPEF) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & par.VirgoleInPunti(newValues("IMPORTO")) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderR.Close()

                    par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET AUTONOMO=nvl(AUTONOMO,0) +" & par.VirgoleInPunti(newValues("IMPORTO") - oldValues("IMPORTO")) & " WHERE ID=" & idUtenzaRedd1
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            RadGridDipend.Rebind()
            connData.chiudi(True)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridPensioni_BatchEditCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridBatchEditingEventArgs) Handles RadGridPensioni.BatchEditCommand
        Try
            connData.apri(True)

            Dim reddPresente As Boolean = False
            Dim idUtenzaRedd1 As Long = 0
            par.cmd.CommandText = "SELECT DOMANDE_REDDITI_VSA.* FROM DOMANDE_REDDITI_VSA,VSA_REDD_PENS_IMPORTI WHERE DOMANDE_REDDITI_VSA.ID=VSA_REDD_PENS_IMPORTI.ID_REDD_TOT " _
                & " and NVL(DIPENDENTE,'0')<>'0' and ID_DOMANDA=" & iddich.Value & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue & " and VSA_REDD_PENS_IMPORTI.id_redd_tot=" & idRedd.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                reddPresente = True
                idUtenzaRedd1 = myReader0(0)
            Else
                par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & iddich.Value & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                Dim myReaderCon As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCon.Read Then
                    idUtenzaRedd1 = myReaderCon(0)
                Else
                    par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID, ID_DOMANDA, ID_COMPONENTE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & iddich.Value & "," & cmbComponente.SelectedValue & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_UTENZA_REDDITI.CURRVAL FROM DUAL"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idUtenzaRedd1 = myReader(0)
                    End If
                    myReader.Close()
                End If
                myReaderCon.Close()
            End If
            myReader0.Close()

            For Each command As GridBatchEditingCommand In e.Commands
                Dim newValues As Hashtable = command.NewValues
                Dim oldValues As Hashtable = command.OldValues
                If command.Type = GridBatchEditingCommandType.Update Then
                    If newValues("IMPORTO") = oldValues("IMPORTO") And newValues("NUM_GG") = oldValues("NUM_GG") Then
                        newValues("IMPORTO") = 0
                    End If
                    If reddPresente = True Then
                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_IMPORTI WHERE ID_REDD_TOT=" & idUtenzaRedd1
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtQuery As New Data.DataTable

                        da.Fill(dtQuery)
                        da.Dispose()
                        If dtQuery.Rows.Count > 0 Then
                            For Each row As Data.DataRow In dtQuery.Rows
                                If row.Item("id") = newValues("IDIMPORTI") Then
                                    If newValues("IMPORTO") <> 0 Then
                                        par.cmd.CommandText = "UPDATE VSA_REDD_PENS_IMPORTI SET IMPORTO=" & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                        & " NUM_GG=" & par.insDbValue(newValues("NUM_GG"), False) & " WHERE ID=" & par.insDbValue(newValues("IDIMPORTI"), False)
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    Else
                                        par.cmd.CommandText = "DELETE FROM VSA_REDD_PENS_IMPORTI WHERE ID=" & newValues("IDIMPORTI")
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    End If
                                End If
                                If newValues("IDIMPORTI") = 0 Then
                                    If newValues("IMPORTO") <> 0 Then
                                        par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_IMPORTI (ID, ID_REDD_PENSIONE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                                        & "VALUES (SEQ_VSA_REDD_PENS_IMPORTI.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                        & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    End If
                                End If
                            Next
                        Else
                            If newValues("IMPORTO") <> 0 Then
                                par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_IMPORTI (ID, ID_REDD_PENSIONE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                                & "VALUES (SEQ_VSA_REDD_PENS_IMPORTI.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If


                    Else
                        If newValues("IMPORTO") <> 0 Then
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_IMPORTI (ID, ID_REDD_PENSIONE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                            & "VALUES (SEQ_VSA_REDD_PENS_IMPORTI.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                            & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If

                    If newValues("IMPORTO") = oldValues("IMPORTO") Then
                        newValues("IMPORTO") = 0
                    End If
                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderR.Read Then
                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET PROV_AGRARI=0,REDDITO_IRPEF=" & par.VirgoleInPunti(newValues("IMPORTO") + (par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - oldValues("IMPORTO"))) & "" _
                            & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID, ID_COMPONENTE, REDDITO_IRPEF) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & par.VirgoleInPunti(newValues("IMPORTO")) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderR.Close()

                    par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET PENSIONE=nvl(PENSIONE,0) +" & par.VirgoleInPunti(newValues("IMPORTO") - oldValues("IMPORTO")) & " WHERE ID=" & idUtenzaRedd1
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            RadGridDipend.Rebind()
            connData.chiudi(True)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridNoISEE_BatchEditCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridBatchEditingEventArgs) Handles RadGridNoISEE.BatchEditCommand
        Try
            connData.apri(True)

            Dim reddPresente As Boolean = False
            Dim idUtenzaRedd1 As Long = 0
            par.cmd.CommandText = "SELECT DOMANDE_REDDITI_VSA.* FROM DOMANDE_REDDITI_VSA,VSA_REDD_NO_ISEE_IMPORTI WHERE DOMANDE_REDDITI_VSA.ID=VSA_REDD_NO_ISEE_IMPORTI.ID_REDD_TOT " _
                & " and NVL(DIPENDENTE,'0')<>'0' and ID_DOMANDA=" & iddich.Value & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue & " and VSA_REDD_NO_ISEE_IMPORTI.id_redd_tot=" & idRedd.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                reddPresente = True
                idUtenzaRedd1 = myReader0(0)
            Else
                par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & iddich.Value & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                Dim myReaderCon As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCon.Read Then
                    idUtenzaRedd1 = myReaderCon(0)
                Else
                    par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID, ID_DOMANDA, ID_COMPONENTE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & iddich.Value & "," & cmbComponente.SelectedValue & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_UTENZA_REDDITI.CURRVAL FROM DUAL"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idUtenzaRedd1 = myReader(0)
                    End If
                    myReader.Close()
                End If
                myReaderCon.Close()
            End If
            myReader0.Close()

            For Each command As GridBatchEditingCommand In e.Commands
                Dim newValues As Hashtable = command.NewValues
                Dim oldValues As Hashtable = command.OldValues
                If command.Type = GridBatchEditingCommandType.Update Then
                    If newValues("IMPORTO") = oldValues("IMPORTO") And newValues("NUM_GG") = oldValues("NUM_GG") Then
                        newValues("IMPORTO") = 0
                    End If
                    If reddPresente = True Then
                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID_REDD_TOT=" & idUtenzaRedd1
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtQuery As New Data.DataTable

                        da.Fill(dtQuery)
                        da.Dispose()
                        If dtQuery.Rows.Count > 0 Then
                            For Each row As Data.DataRow In dtQuery.Rows
                                If row.Item("id") = newValues("IDIMPORTI") Then
                                    If newValues("IMPORTO") <> 0 Then
                                        par.cmd.CommandText = "UPDATE VSA_REDD_NO_ISEE_IMPORTI SET IMPORTO=" & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                        & " NUM_GG=" & par.insDbValue(newValues("NUM_GG"), False) & " WHERE ID=" & par.insDbValue(newValues("IDIMPORTI"), False)
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    Else
                                        par.cmd.CommandText = "DELETE FROM VSA_REDD_NO_ISEE_IMPORTI WHERE ID=" & newValues("IDIMPORTI")
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    End If
                                End If
                                If newValues("IDIMPORTI") = 0 Then
                                    If newValues("IMPORTO") <> 0 Then
                                        par.cmd.CommandText = "INSERT INTO VSA_REDD_NO_ISEE_IMPORTI (ID, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                                        & "VALUES (SEQ_VSA_REDD_NO_ISEE_IMP.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                        & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    End If
                                End If
                            Next
                        Else
                            If newValues("IMPORTO") <> 0 Then
                                par.cmd.CommandText = "INSERT INTO VSA_REDD_NO_ISEE_IMPORTI (ID, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                                & "VALUES (SEQ_VSA_REDD_NO_ISEE_IMP.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If


                    Else
                        If newValues("IMPORTO") <> 0 Then
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_NO_ISEE_IMPORTI (ID, ID_REDD_NO_ISEE, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                            & "VALUES (SEQ_VSA_REDD_NO_ISEE_IMP.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                            & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If

                    If newValues("IMPORTO") = oldValues("IMPORTO") Then
                        newValues("IMPORTO") = 0
                    End If
                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderR.Read Then
                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET PROV_AGRARI=0,REDDITO_IRPEF=" & par.VirgoleInPunti(newValues("IMPORTO") + (par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - oldValues("IMPORTO"))) & "" _
                            & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID, ID_COMPONENTE, REDDITO_IRPEF) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & par.VirgoleInPunti(newValues("IMPORTO")) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderR.Close()

                    par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET NO_ISEE=nvl(NO_ISEE,0) +" & par.VirgoleInPunti(newValues("IMPORTO") - oldValues("IMPORTO")) & " WHERE ID=" & idUtenzaRedd1
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            RadGridDipend.Rebind()
            connData.chiudi(True)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridPensEsenti_BatchEditCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridBatchEditingEventArgs) Handles RadGridPensEsenti.BatchEditCommand
        Try
            connData.apri(True)

            Dim reddPresente As Boolean = False
            Dim idUtenzaRedd1 As Long = 0
            par.cmd.CommandText = "SELECT DOMANDE_REDDITI_VSA.* FROM DOMANDE_REDDITI_VSA,VSA_REDD_PENS_ES_IMPORTI WHERE DOMANDE_REDDITI_VSA.ID=VSA_REDD_PENS_ES_IMPORTI.ID_REDD_TOT " _
                & " and NVL(DIPENDENTE,'0')<>'0' and ID_DOMANDA=" & iddich.Value & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue & " and VSA_REDD_PENS_ES_IMPORTI.id_redd_tot=" & idRedd.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                reddPresente = True
                idUtenzaRedd1 = myReader0(0)
            Else
                par.cmd.CommandText = "SELECT * FROM DOMANDE_REDDITI_VSA WHERE ID_DOMANDA=" & iddich.Value & " AND ID_COMPONENTE=" & cmbComponente.SelectedValue
                Dim myReaderCon As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderCon.Read Then
                    idUtenzaRedd1 = myReaderCon(0)
                Else
                    par.cmd.CommandText = "INSERT INTO DOMANDE_REDDITI_VSA (ID, ID_DOMANDA, ID_COMPONENTE) " _
                        & "VALUES (SEQ_UTENZA_REDDITI.NEXTVAL," & iddich.Value & "," & cmbComponente.SelectedValue & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SEQ_UTENZA_REDDITI.CURRVAL FROM DUAL"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        idUtenzaRedd1 = myReader(0)
                    End If
                    myReader.Close()
                End If
                myReaderCon.Close()
            End If
            myReader0.Close()

            For Each command As GridBatchEditingCommand In e.Commands
                Dim newValues As Hashtable = command.NewValues
                Dim oldValues As Hashtable = command.OldValues
                If command.Type = GridBatchEditingCommandType.Update Then
                    If newValues("IMPORTO") = oldValues("IMPORTO") And newValues("NUM_GG") = oldValues("NUM_GG") Then
                        newValues("IMPORTO") = 0
                    End If
                    If reddPresente = True Then
                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID_REDD_TOT=" & idUtenzaRedd1
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtQuery As New Data.DataTable

                        da.Fill(dtQuery)
                        da.Dispose()
                        If dtQuery.Rows.Count > 0 Then
                            For Each row As Data.DataRow In dtQuery.Rows
                                If row.Item("id") = newValues("IDIMPORTI") Then
                                    If newValues("IMPORTO") <> 0 Then
                                        par.cmd.CommandText = "UPDATE VSA_REDD_PENS_ES_IMPORTI SET IMPORTO=" & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                        & " NUM_GG=" & par.insDbValue(newValues("NUM_GG"), False) & " WHERE ID=" & par.insDbValue(newValues("IDIMPORTI"), False)
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    Else
                                        par.cmd.CommandText = "DELETE FROM VSA_REDD_PENS_ES_IMPORTI WHERE ID=" & newValues("IDIMPORTI")
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    End If
                                End If
                                If newValues("IDIMPORTI") = 0 Then
                                    If newValues("IMPORTO") <> 0 Then
                                        par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_ES_IMPORTI (ID, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                                        & "VALUES (SEQ_VSA_REDD_PENS_ES_IMP.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                        & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                                        par.cmd.ExecuteNonQuery()
                                        Exit For
                                    End If
                                End If
                            Next
                        Else
                            If newValues("IMPORTO") <> 0 Then
                                par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_ES_IMPORTI (ID, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                                & "VALUES (SEQ_VSA_REDD_PENS_ES_IMP.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                                & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If


                    Else
                        If newValues("IMPORTO") <> 0 Then
                            par.cmd.CommandText = "INSERT INTO VSA_REDD_PENS_ES_IMPORTI (ID, ID_REDD_PENS_ESENTI, IMPORTO, NUM_GG, ID_REDD_TOT) " _
                            & "VALUES (SEQ_VSA_REDD_PENS_ES_IMP.NEXTVAL," & par.insDbValue(newValues("ID"), False) & "," & par.insDbValue(newValues("IMPORTO"), False).ToUpper & "," _
                            & par.insDbValue(newValues("NUM_GG"), False) & "," & idUtenzaRedd1 & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If

                    If newValues("IMPORTO") = oldValues("IMPORTO") Then
                        newValues("IMPORTO") = 0
                    End If
                    par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & cmbComponente.SelectedValue
                    Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderR.Read Then
                        par.cmd.CommandText = "UPDATE COMP_REDDITO_VSA SET PROV_AGRARI=0,REDDITO_IRPEF=" & par.VirgoleInPunti(newValues("IMPORTO") + (par.IfNull(myReaderR("REDDITO_IRPEF"), 0) - oldValues("IMPORTO"))) & "" _
                            & " WHERE ID=" & par.IfNull(myReaderR("ID"), 0)
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "INSERT INTO COMP_REDDITO_VSA (ID, ID_COMPONENTE, REDDITO_IRPEF) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," & cmbComponente.SelectedValue & "," & par.VirgoleInPunti(newValues("IMPORTO")) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderR.Close()

                    par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET PENS_ESENTE=nvl(PENS_ESENTE,0) +" & par.VirgoleInPunti(newValues("IMPORTO") - oldValues("IMPORTO")) & " WHERE ID=" & idUtenzaRedd1
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            RadGridDipend.Rebind()
            connData.chiudi(True)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridDipend_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridDipend.NeedDataSource
        Try
            Dim Query As String = ""
            Dim dt As New Data.DataTable
            If operazione.Value = "1" Then
                Query = " select VSA_REDDITI_DIPENDENTE.*, " _
                    & " nvl( (Select VSA_REDD_DIPEND_IMPORTI.id from VSA_REDD_DIPEND_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_DIPENDENTE.id = VSA_REDD_DIPEND_IMPORTI.ID_REDD_DIPENDENTE " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_DIPEND_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & ") ,0) AS IDIMPORTI," _
                    & " nvl( (Select VSA_REDD_DIPEND_IMPORTI.NUM_GG from VSA_REDD_DIPEND_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_DIPENDENTE.id = VSA_REDD_DIPEND_IMPORTI.ID_REDD_DIPENDENTE " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_DIPEND_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & "),0) as NUM_GG ," _
                    & " nvl(  (Select VSA_REDD_DIPEND_IMPORTI.importo from VSA_REDD_DIPEND_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_DIPENDENTE.id = VSA_REDD_DIPEND_IMPORTI.ID_REDD_DIPENDENTE " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_DIPEND_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & "),0) as importo  " _
                    & " from VSA_REDDITI_DIPENDENTE"
            Else
                Query = "select VSA_REDDITI_DIPENDENTE.*,'' as num_gg, 0 as importo from VSA_REDDITI_DIPENDENTE ORDER BY ID ASC"
            End If
            dt = par.getDataTableGrid(Query)

            RadGridDipend.DataSource = dt

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridAutonomo_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridAutonomo.NeedDataSource
        Try
            Dim Query As String = ""
            Dim dt As New Data.DataTable
            If operazione.Value = "1" Then
                Query = " select VSA_REDDITI_AUTONOMO.*, " _
                    & " nvl( (Select VSA_REDD_AUTONOMO_IMPORTI.id from VSA_REDD_AUTONOMO_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_AUTONOMO.id = VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_AUTONOMO " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & ") ,0) AS IDIMPORTI," _
                    & " nvl( (Select VSA_REDD_AUTONOMO_IMPORTI.NUM_GG from VSA_REDD_AUTONOMO_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_AUTONOMO.id = VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_AUTONOMO " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & "),0) as NUM_GG ," _
                    & " nvl(  (Select VSA_REDD_AUTONOMO_IMPORTI.importo from VSA_REDD_AUTONOMO_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_AUTONOMO.id = VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_AUTONOMO " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & "),0) as importo  " _
                    & " from VSA_REDDITI_AUTONOMO"
            Else
                Query = "select VSA_REDDITI_AUTONOMO.*,'' as num_gg, 0 as importo from VSA_REDDITI_AUTONOMO ORDER BY ID ASC"
            End If
            dt = par.getDataTableGrid(Query)

            RadGridAutonomo.DataSource = dt

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridPensioni_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridPensioni.NeedDataSource
        Try
            Dim Query As String = ""
            Dim dt As New Data.DataTable
            If operazione.Value = "1" Then
                Query = " select VSA_REDDITI_PENSIONE.*, " _
                    & " nvl( (Select VSA_REDD_PENS_IMPORTI.id from VSA_REDD_PENS_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_PENSIONE.id = VSA_REDD_PENS_IMPORTI.ID_REDD_PENSIONE " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_PENS_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & ") ,0) AS IDIMPORTI," _
                    & " nvl( (Select VSA_REDD_PENS_IMPORTI.NUM_GG from VSA_REDD_PENS_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_PENSIONE.id = VSA_REDD_PENS_IMPORTI.ID_REDD_PENSIONE " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_PENS_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & "),0) as NUM_GG ," _
                    & " nvl(  (Select VSA_REDD_PENS_IMPORTI.importo from VSA_REDD_PENS_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_PENSIONE.id = VSA_REDD_PENS_IMPORTI.ID_REDD_PENSIONE " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_PENS_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & "),0) as importo  " _
                    & " from VSA_REDDITI_PENSIONE"
            Else
                Query = "select VSA_REDDITI_PENSIONE.*,'' as num_gg, 0 as importo from VSA_REDDITI_PENSIONE ORDER BY ID ASC"
            End If
            dt = par.getDataTableGrid(Query)

            RadGridPensioni.DataSource = dt

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridPensEsenti_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridPensEsenti.NeedDataSource
        Try
            Dim Query As String = ""
            Dim dt As New Data.DataTable
            If operazione.Value = "1" Then
                Query = " select VSA_REDDITI_PENS_ESENTI.*, " _
                    & " nvl( (Select VSA_REDD_PENS_ES_IMPORTI.id from VSA_REDD_PENS_ES_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_PENS_ESENTI.id = VSA_REDD_PENS_ES_IMPORTI.ID_REDD_PENS_ESENTI " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_PENS_ES_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & ") ,0) AS IDIMPORTI," _
                    & " nvl( (Select VSA_REDD_PENS_ES_IMPORTI.NUM_GG from VSA_REDD_PENS_ES_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_PENS_ESENTI.id = VSA_REDD_PENS_ES_IMPORTI.ID_REDD_PENS_ESENTI " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_PENS_ES_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & "),0) as NUM_GG ," _
                    & " nvl(  (Select VSA_REDD_PENS_ES_IMPORTI.importo from VSA_REDD_PENS_ES_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_PENS_ESENTI.id = VSA_REDD_PENS_ES_IMPORTI.ID_REDD_PENS_ESENTI " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_PENS_ES_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & "),0) as importo  " _
                    & " from VSA_REDDITI_PENS_ESENTI"
            Else
                Query = "select VSA_REDDITI_PENS_ESENTI.*,'' as num_gg, 0 as importo from VSA_REDDITI_PENS_ESENTI ORDER BY ID ASC"
            End If
            dt = par.getDataTableGrid(Query)

            RadGridPensEsenti.DataSource = dt

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridNoISEE_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridNoISEE.NeedDataSource
        Try
            Dim Query As String = ""
            Dim dt As New Data.DataTable
            If operazione.Value = "1" Then
                Query = " select VSA_REDDITI_NO_ISEE.*, " _
                    & " nvl( (Select VSA_REDD_NO_ISEE_IMPORTI.id from VSA_REDD_NO_ISEE_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_NO_ISEE.id = VSA_REDD_NO_ISEE_IMPORTI.ID_REDD_NO_ISEE " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_NO_ISEE_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & ") ,0) AS IDIMPORTI," _
                    & " nvl( (Select VSA_REDD_NO_ISEE_IMPORTI.NUM_GG from VSA_REDD_NO_ISEE_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_NO_ISEE.id = VSA_REDD_NO_ISEE_IMPORTI.ID_REDD_NO_ISEE " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_NO_ISEE_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & "),0) as NUM_GG ," _
                    & " nvl(  (Select VSA_REDD_NO_ISEE_IMPORTI.importo from VSA_REDD_NO_ISEE_IMPORTI,DOMANDE_REDDITI_VSA " _
                    & " where  VSA_REDDITI_NO_ISEE.id = VSA_REDD_NO_ISEE_IMPORTI.ID_REDD_NO_ISEE " _
                    & " and DOMANDE_REDDITI_VSA.ID = VSA_REDD_NO_ISEE_IMPORTI.ID_REDD_TOT" _
                    & " AND ID_COMPONENTE = " & cmbComponente.SelectedValue & "),0) as importo  " _
                    & " from VSA_REDDITI_NO_ISEE"
            Else
                Query = "select VSA_REDDITI_NO_ISEE.*,'' as num_gg, 0 as importo from VSA_REDDITI_NO_ISEE ORDER BY ID ASC"
            End If
            dt = par.getDataTableGrid(Query)

            RadGridNoISEE.DataSource = dt

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
            'par.cmd.CommandText = "UPDATE DOMANDE_REDDITI_VSA SET DIPENDENTE=" & par.VirgoleInPunti(importoDettRedd) & " WHERE ID=" & idUtenzaReddDip
            'par.cmd.ExecuteNonQuery()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub cmbComponente_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbComponente.SelectedIndexChanged
        RadGridDipend.Rebind()
        RadGridAutonomo.Rebind()
        RadGridNoISEE.Rebind()
        RadGridPensEsenti.Rebind()
        RadGridPensioni.Rebind()
    End Sub
End Class
