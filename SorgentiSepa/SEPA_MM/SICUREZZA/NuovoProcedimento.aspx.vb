Imports Telerik.Web.UI

Partial Class SICUREZZA_NuovoProcedimento
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                idProcedim.Value = Request.QueryString("IDP")


                If Not IsNothing(idProcedim.Value) And idProcedim.Value <> "" And idProcedim.Value <> "0" Then
                    CaricaProcedimenti()
                End If
                If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                    CType(Me.Master.FindControl("NavigationMenu"), Telerik.Web.UI.RadMenu).Visible = False
                End If
                If Session.Item("FL_SEC_GEST_COMPLETA") = "0" Then
                    btnSalva.Visible = False
                End If
            End If
            cmbTipoProc.Attributes.Add("onchange", "javascript:visibleOggetti()")

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Procedimento - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaProcedimenti()
        Try
            If idProcedim.Value <> "-1" Then
                connData.apri()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PROCEDIMENTI_SICUREZZA WHERE ID = " & idProcedim.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then

                    If par.IfNull(lettore("tipo"), "") <> "" Then
                        cmbTipoProc.SelectedValue = par.IfNull(lettore("tipo"), "")
                    End If
                    tipoProc.Value = cmbTipoProc.SelectedValue

                    txtDataCInt.Text = par.FormattaData(Mid(par.IfNull(Trim(lettore("DATA_ORA_INSERIMENTO")), ""), 1, 8))
                    txtOraCInt.Text = Mid(par.IfNull(Trim(lettore("DATA_ORA_INSERIMENTO")), ""), 9, 2) & ":" & Mid(par.IfNull(Trim(lettore("DATA_ORA_INSERIMENTO")), "          "), 11, 2)

                    cmbStatoProc.SelectedValue = par.IfNull(lettore("stato"), "1")
                    txtAutoritaGiud.Text = par.IfNull(lettore("AUTORITA_GIUDIZIARIA"), "")
                    txtLuogoConv.Text = par.IfNull(lettore("LUOGO_CONVOCAZIONE"), "")

                    If par.IfNull(lettore("FL_DECRETO_RICORSO"), "0") = "1" Then
                        chkAmmvo.Checked = True
                    Else
                        chkAmmvo.Checked = False
                    End If

                    If par.IfNull(lettore("FL_DECRETO_INGIUNTIVO"), "0") = "1" Then
                        chkCivile.Checked = True
                    Else
                        chkCivile.Checked = False
                    End If

                    cmbTipoPenale.SelectedValue = par.IfNull(lettore("TIPO_PROCEDIM_PENALE"), "")
                    If par.IfEmpty(lettore("DATA_CONVOCAZIONE").ToString, "") <> "" Then
                        txtDataConvocazione.SelectedDate = par.FormattaData(par.IfNull(lettore("DATA_CONVOCAZIONE"), ""))
                    End If
                    txtReferente.Text = par.IfNull(lettore("REFERENTE"), "")

                    lblNsegn.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "document.getElementById('txtModificato').value='1';window.open('Segnalazione.aspx?NM=1&IDS=" & par.IfNull(lettore("id_segnalazione"), "0") & "','SEGN', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');" & Chr(34) & ">" & par.IfNull(lettore("id_segnalazione"), "0") & "</a>"
                End If
                lettore.Close()

                lblTitolo2.Text = " " & tipoProc.Value & " numero " & idProcedim.Value

                CaricaTabellaNote(idProcedim.Value)

                connData.chiudi()
            Else
                connData.chiudi()
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Non è possibile caricare correttamente i dati!", 300, 150, "Attenzione", Nothing, Nothing)
                'par.modalDialogMessage("Caricamento dati procedimento", "Non è possibile caricare correttamente i dati", Me.Page, "info", "Home.aspx", )
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Procedimento - CaricaProcedimenti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub SalvaProcedimento()
        Try
            connData.apri(True)

            Dim tipoProcPenale As String = ""
            Dim autoritaGiud As String = ""
            Dim luogoConvoc As String = ""
            Dim dataConvocaz As String = ""
            Dim flDecretoRicorso As Integer = 0
            Dim flDecretoIngiuntivo As Integer = 0

            Select Case cmbTipoProc.SelectedValue
                Case "Penale"
                    tipoProcPenale = par.IfEmpty(cmbTipoPenale.SelectedItem.Text, "")
                    luogoConvoc = par.IfEmpty(txtLuogoConv.Text, "")
                    dataConvocaz = par.IfEmpty(txtDataConvocazione.SelectedDate, "")
                    autoritaGiud = par.IfEmpty(txtAutoritaGiud.Text, "")
                Case "Civile"
                    If chkCivile.Checked = True Then
                        flDecretoIngiuntivo = 1
                    Else
                        flDecretoIngiuntivo = 0
                    End If
                Case "Amministrativo"
                    If chkAmmvo.Checked = True Then
                        flDecretoRicorso = 1
                    Else
                        flDecretoRicorso = 0
                    End If
            End Select

            par.cmd.CommandText = "UPDATE SISCOM_MI.PROCEDIMENTI_SICUREZZA" _
                    & " SET  " _
                    & " TIPO                   = " & par.insDbValue(cmbTipoProc.SelectedValue, True, False) & "," _
                    & " STATO                   = " & par.insDbValue(cmbStatoProc.SelectedValue, False, False, True) & "," _
                    & " DATA_APERTURA              = " & par.insDbValue(par.IfEmpty(txtDataApertura.SelectedDate, ""), True, True) & "," _
                    & " DATA_CHIUSURA              = " & par.insDbValue(par.IfEmpty(txtDataChiusura.SelectedDate, ""), True, True) & "," _
                    & " REFERENTE                  = " & par.insDbValue(txtReferente.Text, True, False).ToUpper & "," _
                    & " DATA_CONVOCAZIONE          = " & par.insDbValue(dataConvocaz, True, True) & "," _
                    & " AUTORITA_GIUDIZIARIA       = " & par.insDbValue(autoritaGiud, True, False).ToUpper & "," _
                    & " TIPO_PROCEDIM_PENALE       = " & par.insDbValue(tipoProcPenale, True, False) & "," _
                    & " LUOGO_CONVOCAZIONE         = " & par.insDbValue(luogoConvoc, True, False).ToUpper & "," _
                    & " FL_DECRETO_RICORSO         = " & par.insDbValue(flDecretoRicorso, False) & "," _
                    & " FL_DECRETO_INGIUNTIVO      = " & par.insDbValue(flDecretoIngiuntivo, False) & "" _
                    & " WHERE  ID= " & idProcedim.Value
            par.cmd.ExecuteNonQuery()

            If TextBoxNota.Text <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PROCEDIMENTI_NOTE (ID_PROCEDIMENTO, NOTE, DATA_ORA, ID_OPERATORE) " _
                                    & "VALUES (" & idProcedim.Value & ", '" & par.PulisciStrSql(TextBoxNota.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                par.cmd.ExecuteNonQuery()

                TextBoxNota.Text = ""
                CaricaTabellaNote(idProcedim.Value)
            End If

            lblTitolo2.Text = " " & tipoProc.Value & " numero " & idProcedim.Value

            
            connData.chiudi(True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - Nuovo Procedimento - SalvaProcedimento - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaTabellaNote(idInterv As String)
        Dim tabellaNote As String = ""
        tabellaNote &= "<table cellpadding='0' cellspacing='0' style='width:95%;'><tr style='font-family: ARIAL; font-size: 8pt; font-weight: bold'><td width='100px'>DATA-ORA</td><td width='150px'>OPERATORE</td><td>NOTE</td></tr>"
        par.cmd.CommandText = "SELECT PROCEDIMENTI_NOTE.*,operatori.operatore FROM sepa.operatori,siscom_mi.PROCEDIMENTI_NOTE where PROCEDIMENTI_NOTE.id_procedimento=" & idProcedim.Value & " and PROCEDIMENTI_NOTE.id_operatore=operatori.id (+) order by data_ora desc"
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While lettore.Read
            If par.IfNull(lettore("NOTE"), "").ToString <> "" Then
                tabellaNote &= "<tr style='height: 16px;font-family: ARIAL; font-size: 8pt;'><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='100px'>" & par.FormattaData(Mid(par.IfNull(lettore("data_ora"), ""), 1, 8)) & " " & Mid(par.IfNull(lettore("data_ora"), ""), 9, 2) & ":" & Mid(par.IfNull(lettore("data_ora"), ""), 11, 2) & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='150px'>" & par.IfNull(lettore("operatore"), "") & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;'>" & Replace(par.IfNull(lettore("note"), ""), vbCrLf, "</br>") & "</td></tr>"
            End If
        End While
        lettore.Close()
        tabellaNote &= "</table>"
        If tabellaNote <> "" Then
            TabellaNoteComplete.Text = tabellaNote
        End If
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        SalvaProcedimento()
        CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)
        CType(Me.Master.FindControl("txtModificato"), HiddenField).Value = "0"
        
    End Sub

    Protected Sub cmbTipoProc_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbTipoProc.SelectedIndexChanged
        tipoProc.Value = cmbTipoProc.SelectedValue

        Select Case tipoProc.Value
            Case "Penale"
                chkAmmvo.Checked = False
                chkCivile.Checked = False
            Case "Civile"
                txtLuogoConv.Text = ""
                txtDataConvocazione.Clear()
                txtAutoritaGiud.Text = ""
                chkAmmvo.Checked = False
            Case "Amministrativo"
                chkCivile.Checked = False
        End Select
    End Sub
End Class
