
Partial Class SATISFACTION_Pippo
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        Dim codUI As String
        Dim id_indirizzo As String
        Dim indirizzo As String
        Dim civico As String
        Dim luogo As String

        codUI = Request.QueryString("codUI")
        id_indirizzo = Request.QueryString("idInd")
        id_chiave.Value = Request.QueryString("id")


        If Not IsPostBack Then

            lbl_infoImm.Text = "<b>Cod. unità</b>: " & codUI & "<br />"
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID =" & id_indirizzo
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.Read Then
                    indirizzo = par.IfNull(myReader("DESCRIZIONE"), "")
                    civico = par.IfNull(myReader("CIVICO"), "")
                    luogo = par.IfNull(myReader("LOCALITA"), "")

                    lbl_infoImm.Text += "<b>Indirizzo</b>: " & indirizzo & "</b>, " & civico & "<br /> <b>Località</b>: " _
                        & luogo
                End If

                'Se id <> -1 allora caricare i dati del questionario per poterli modificare
                If id_chiave.Value <> -1 Then
                    lblTitolo.Text = "Modifica Scheda Questionario"
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CUSTOMER_SATISFACTION WHERE ID =" & id_chiave.Value
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then

                        If par.IfNull(myReader("PU_REGOLARITA"), "").ToString <> "" Then
                            cbx_pulizia1.SelectedValue = par.IfNull(myReader("PU_REGOLARITA"), "")
                            cbx_VApulizia1.SelectedValue = par.IfNull(myReader("PU_REGOLARITA_VAL"), "")
                            cbx_pulizia2.SelectedValue = par.IfNull(myReader("PU_QUALITA"), "")
                            cbx_VApulizia2.SelectedValue = par.IfNull(myReader("PU_QUALITA_VAL"), "")
                            cbx_pulizia3.SelectedValue = par.IfNull(myReader("PU_CORTESIA"), "")
                            cbx_VApulizia3.SelectedValue = par.IfNull(myReader("PU_CORTESIA_VAL"), "")
                            cbx_pulizia4.SelectedValue = par.IfNull(myReader("PU_IGIENE"), "")
                            cbx_VApulizia4.SelectedValue = par.IfNull(myReader("PU_IGIENE_VAL"), "")
                            cbx_pulizia5.SelectedValue = par.IfNull(myReader("PU_PARTI_COMUNI"), "")
                            cbx_VApulizia5.SelectedValue = par.IfNull(myReader("PU_PARTI_COMUNI_VAL"), "")
                            cbx_pulizia6.SelectedValue = par.IfNull(myReader("PU_RIF_INGOMBRANTI"), "")
                            cbx_VApulizia6.SelectedValue = par.IfNull(myReader("PU_RIF_INGOMBRANTI_VAL"), "")
                            txt_pulizia.Text = par.IfNull(myReader("PU_SUGGERIMENTI"), " ")
                        Else
                            ChPulizia.Checked = False
                            SoloLetturaPulizia()
                        End If

                        If par.IfNull(myReader("PO_REGOLARITA"), "").ToString <> "" Then
                            cbx_portierato1.SelectedValue = myReader("PO_REGOLARITA")
                            cbx_VAportierato1.SelectedValue = myReader("PO_REGOLARITA_VAL")
                            cbx_portierato2.SelectedValue = myReader("PO_QUALITA")
                            cbx_VAportierato2.SelectedValue = myReader("PO_QUALITA_VAL")
                            cbx_portierato3.SelectedValue = myReader("PO_CORTESIA")
                            cbx_VAportierato3.SelectedValue = myReader("PO_CORTESIA_VAL")
                            cbx_portierato4.SelectedValue = myReader("PO_INF_COMPLETE")
                            cbx_VAportierato4.SelectedValue = myReader("PO_INF_COMPLETE_VAL")
                            cbx_portierato5.SelectedValue = myReader("PO_POSTA")
                            cbx_VAportierato5.SelectedValue = myReader("PO_POSTA_VAL")
                            txt_portierato.Text = par.IfNull(myReader("PO_SUGGERIMENTI"), " ")
                        Else
                            ChPortierato.Checked = False
                            SoloLetturaPortierato()
                        End If

                        If par.IfNull(myReader("RI_REGOLARITA"), "").ToString <> "" Then
                            cbx_riscaldamento1.SelectedValue = myReader("RI_REGOLARITA")
                            cbx_VAriscaldamento1.SelectedValue = myReader("RI_REGOLARITA_VAL")
                            cbx_riscaldamento2.SelectedValue = myReader("RI_QUALITA")
                            cbx_VAriscaldamento2.SelectedValue = myReader("RI_QUALITA_VAL")
                            cbx_riscaldamento3.SelectedValue = myReader("RI_CORTESIA")
                            cbx_VAriscaldamento3.SelectedValue = myReader("RI_CORTESIA_VAL")
                            cbx_riscaldamento4.SelectedValue = myReader("RI_TEMPERATURA")
                            cbx_VAriscaldamento4.SelectedValue = myReader("RI_TEMPERATURA_VAL")
                            cbx_riscaldamento5.SelectedValue = myReader("RI_GUASTI")
                            cbx_VAriscaldamento5.SelectedValue = myReader("RI_GUASTI_VAL")
                            cbx_riscaldamento6.SelectedValue = myReader("RI_RIS_GUASTI")
                            cbx_VAriscaldamento6.SelectedValue = myReader("RI_RIS_GUASTI_VAL")
                            txt_riscaldam.Text = par.IfNull(myReader("RI_SUGGERIMENTI"), " ")
                        Else
                            ChRiscaldamento.Checked = False
                            SoloLetturaRiscaldamento()
                        End If

                        If par.IfNull(myReader("VE_REGOLARITA"), "").ToString <> "" Then
                            cbx_manutenzione1.SelectedValue = myReader("VE_REGOLARITA")
                            cbx_VAmanutenzione1.SelectedValue = myReader("VE_REGOLARITA_VAL")
                            cbx_manutenzione2.SelectedValue = myReader("VE_QUALITA")
                            cbx_VAmanutenzione2.SelectedValue = myReader("VE_QUALITA_VAL")
                            cbx_manutenzione3.SelectedValue = myReader("VE_CORTESIA")
                            cbx_VAmanutenzione3.SelectedValue = myReader("VE_CORTESIA_VAL")
                            cbx_manutenzione4.SelectedValue = myReader("VE_TEMPESTIVITA")
                            cbx_VAmanutenzione4.SelectedValue = myReader("VE_TEMPESTIVITA_VAL")
                            cbx_manutenzione5.SelectedValue = myReader("VE_RUMORE")
                            cbx_VAmanutenzione5.SelectedValue = myReader("VE_RUMORE_VAL")
                            cbx_manutenzione6.SelectedValue = myReader("VE_SMALTIMENTO_RIF")
                            cbx_VAmanutenzione6.SelectedValue = myReader("VE_SMALTIMENTO_RIF_VAL")
                            txt_manutenz.Text = par.IfNull(myReader("VE_SUGGERIMENTI"), " ")
                        Else
                            ChVerde.Checked = False
                            SoloLetturaVerde()
                        End If

                        cbx_complessivo.SelectedValue = par.IfNull(myReader("GIUDIZIO_COMPLESSIVO"), " ")

                    End If


                End If

                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Catch ex As Exception

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)

            End Try

            If Session.Item("MOD_SATISFACTION_SL") = 1 Then
                DatiSoloLettura()
            End If

        End If
    End Sub

    Protected Sub Wizard1_FinishButtonClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles Wizard1.FinishButtonClick

        Dim id_unita As Long
        Dim data_comp As String
        id_unita = Request.QueryString("idU")
        data_comp = Request.QueryString("data")

        'Risposte scheda Servizi di Pulizia
        Dim pu_regolar As String = "'" & cbx_pulizia1.SelectedValue & "'"
        Dim pu_regolar_val As String = "'" & cbx_VApulizia1.SelectedValue & "'"
        Dim pu_qualita As String = "'" & cbx_pulizia2.SelectedValue & "'"
        Dim pu_qualita_val As String = "'" & cbx_VApulizia2.SelectedValue & "'"
        Dim pu_cortesia As String = "'" & cbx_pulizia3.SelectedValue & "'"
        Dim pu_cortesia_val As String = "'" & cbx_VApulizia3.SelectedValue & "'"
        Dim pu_igiene As String = "'" & cbx_pulizia4.SelectedValue & "'"
        Dim pu_igiene_val As String = "'" & cbx_VApulizia4.SelectedValue & "'"
        Dim pu_parti_comuni As String = "'" & cbx_pulizia5.SelectedValue & "'"
        Dim pu_parti_comuni_val As String = "'" & cbx_VApulizia5.SelectedValue & "'"
        Dim pu_rif_ingombr As String = "'" & cbx_pulizia6.SelectedValue & "'"
        Dim pu_rif_ingombr_val As String = "'" & cbx_VApulizia6.SelectedValue & "'"
        Dim pu_suggerimenti As String = txt_pulizia.Text

        'Risposte scheda Servizi di portierato
        Dim po_regolar As String = "'" & cbx_portierato1.SelectedValue & "'"
        Dim po_regolar_val As String = "'" & cbx_VAportierato1.SelectedValue & "'"
        Dim po_qualita As String = "'" & cbx_portierato2.SelectedValue & "'"
        Dim po_qualita_val As String = "'" & cbx_VAportierato2.SelectedValue & "'"
        Dim po_cortesia As String = "'" & cbx_portierato3.SelectedValue & "'"
        Dim po_cortesia_val As String = "'" & cbx_VAportierato3.SelectedValue & "'"
        Dim po_inf_complete As String = "'" & cbx_portierato4.SelectedValue & "'"
        Dim po_inf_complete_val As String = "'" & cbx_VAportierato4.SelectedValue & "'"
        Dim po_posta As String = "'" & cbx_portierato5.SelectedValue & "'"
        Dim po_posta_val As String = "'" & cbx_VAportierato5.SelectedValue & "'"
        Dim po_suggerimenti As String = txt_portierato.Text
        

        'Risposte scheda Servizi di riscaldamento
        Dim ri_regolarita As String = "'" & cbx_riscaldamento1.SelectedValue & "'"
        Dim ri_regolarita_val As String = "'" & cbx_VAriscaldamento1.SelectedValue & "'"
        Dim ri_qualita As String = "'" & cbx_riscaldamento2.SelectedValue & "'"
        Dim ri_qualita_val As String = "'" & cbx_VAriscaldamento2.SelectedValue & "'"
        Dim ri_cortesia As String = "'" & cbx_riscaldamento3.SelectedValue & "'"
        Dim ri_cortesia_val As String = "'" & cbx_VAriscaldamento3.SelectedValue & "'"
        Dim ri_temperatura As String = "'" & cbx_riscaldamento4.SelectedValue & "'"
        Dim ri_temperatura_val As String = "'" & cbx_VAriscaldamento4.SelectedValue & "'"
        Dim ri_guasti As String = "'" & cbx_riscaldamento5.SelectedValue & "'"
        Dim ri_guasti_val As String = "'" & cbx_VAriscaldamento5.SelectedValue & "'"
        Dim ri_ris_guasti As String = "'" & cbx_riscaldamento6.SelectedValue & "'"
        Dim ri_ris_guasti_val As String = "'" & cbx_VAriscaldamento6.SelectedValue & "'"
        Dim ri_suggerimenti As String = txt_riscaldam.Text


        'Risposte scheda Servizi di manutenzione del verde
        Dim ve_regolarita As String = "'" & cbx_manutenzione1.SelectedValue & "'"
        Dim ve_regolarita_val As String = "'" & cbx_VAmanutenzione1.SelectedValue & "'"
        Dim ve_qualita As String = "'" & cbx_manutenzione2.SelectedValue & "'"
        Dim ve_qualita_val As String = "'" & cbx_VAmanutenzione2.SelectedValue & "'"
        Dim ve_cortesia As String = "'" & cbx_manutenzione3.SelectedValue & "'"
        Dim ve_cortesia_val As String = "'" & cbx_VAmanutenzione3.SelectedValue & "'"
        Dim ve_tempestivita As String = "'" & cbx_manutenzione4.SelectedValue & "'"
        Dim ve_tempestivita_val As String = "'" & cbx_VAmanutenzione4.SelectedValue & "'"
        Dim ve_rumore As String = "'" & cbx_manutenzione5.SelectedValue & "'"
        Dim ve_rumore_val As String = "'" & cbx_VAmanutenzione5.SelectedValue & "'"
        Dim ve_smaltimento_rif As String = "'" & cbx_manutenzione6.SelectedValue & "'"
        Dim ve_smaltimento_rif_val As String = "'" & cbx_VAmanutenzione6.SelectedValue & "'"
        Dim ve_suggerimenti As String = txt_manutenz.Text
     

        Dim giud_compless As String = cbx_complessivo.SelectedValue
        Dim data_inserimento As String = Format(Now(), "yyyyMMdd")

        If Not ChPulizia.Checked Then
            pu_regolar = "NULL"
            pu_regolar_val = "NULL"
            pu_qualita = "NULL"
            pu_qualita_val = "NULL"
            pu_cortesia = "NULL"
            pu_cortesia_val = "NULL"
            pu_igiene = "NULL"
            pu_igiene_val = "NULL"
            pu_parti_comuni = "NULL"
            pu_parti_comuni_val = "NULL"
            pu_rif_ingombr = "NULL"
            pu_rif_ingombr_val = "NULL"

        End If

        If Not ChPortierato.Checked Then
            po_regolar = "NULL"
            po_regolar_val = "NULL"
            po_qualita = "NULL"
            po_qualita_val = "NULL"
            po_cortesia = "NULL"
            po_cortesia_val = "NULL"
            po_inf_complete = "NULL"
            po_inf_complete_val = "NULL"
            po_posta = "NULL"
            po_posta_val = "NULL"

        End If

        If Not ChRiscaldamento.Checked Then
            ri_regolarita = "NULL"
            ri_regolarita_val = "NULL"
            ri_qualita = "NULL"
            ri_qualita_val = "NULL"
            ri_cortesia = "NULL"
            ri_cortesia_val = "NULL"
            ri_temperatura = "NULL"
            ri_temperatura_val = "NULL"
            ri_guasti = "NULL"
            ri_guasti_val = "NULL"
            ri_ris_guasti = "NULL"
            ri_ris_guasti_val = "NULL"

        End If

        If Not ChVerde.Checked Then
            ve_regolarita = "NULL"
            ve_regolarita_val = "NULL"
            ve_qualita = "NULL"
            ve_qualita_val = "NULL"
            ve_cortesia = "NULL"
            ve_cortesia_val = "NULL"
            ve_tempestivita = "NULL"
            ve_tempestivita_val = "NULL"
            ve_rumore = "NULL"
            ve_rumore_val = "NULL"
            ve_smaltimento_rif = "NULL"
            ve_smaltimento_rif_val = "NULL"

        End If

        If ChPulizia.Checked = False And ChPortierato.Checked = False And ChRiscaldamento.Checked = False And ChVerde.Checked = False Then
            Response.Write("<script>alert('Attenzione! Impossibile inserire questionari vuoti!')</script>")
        Else

            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                If id_chiave.Value = -1 Then

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.CUSTOMER_SATISFACTION (" _
                        & "ID_UNITA,DATA_COMPILAZIONE,PU_REGOLARITA,PU_REGOLARITA_VAL,PU_QUALITA,PU_QUALITA_VAL,PU_CORTESIA, " _
                        & "PU_CORTESIA_VAL,PU_IGIENE,PU_IGIENE_VAL,PU_PARTI_COMUNI,PU_PARTI_COMUNI_VAL,PU_RIF_INGOMBRANTI,PU_RIF_INGOMBRANTI_VAL, " _
                        & "PU_SUGGERIMENTI,PO_REGOLARITA,PO_REGOLARITA_VAL,PO_QUALITA,PO_QUALITA_VAL,PO_CORTESIA,PO_CORTESIA_VAL,PO_INF_COMPLETE," _
                        & "PO_INF_COMPLETE_VAL,PO_POSTA,PO_POSTA_VAL,PO_SUGGERIMENTI,RI_REGOLARITA,RI_REGOLARITA_VAL,RI_QUALITA,RI_QUALITA_VAL," _
                        & "RI_CORTESIA,RI_CORTESIA_VAL,RI_TEMPERATURA,RI_TEMPERATURA_VAL,RI_GUASTI,RI_GUASTI_VAL,RI_RIS_GUASTI,RI_RIS_GUASTI_VAL, " _
                        & "RI_SUGGERIMENTI,VE_REGOLARITA,VE_REGOLARITA_VAL,VE_QUALITA,VE_QUALITA_VAL,VE_CORTESIA,VE_CORTESIA_VAL,VE_TEMPESTIVITA, " _
                        & "VE_TEMPESTIVITA_VAL,VE_RUMORE,VE_RUMORE_VAL,VE_SMALTIMENTO_RIF,VE_SMALTIMENTO_RIF_VAL,VE_SUGGERIMENTI,GIUDIZIO_COMPLESSIVO,ID,ID_OPERATORE," _
                        & "DATA_INSERIMENTO_OP) VALUES (" _
                        & id_unita & ",'" & data_comp & "'," & pu_regolar & "," & pu_regolar_val & ", " & pu_qualita & ", " & pu_qualita_val & "," & pu_cortesia & "," _
                        & pu_cortesia_val & "," & pu_igiene & "," & pu_igiene_val & "," & pu_parti_comuni & "," _
                        & pu_parti_comuni_val & "," & pu_rif_ingombr & "," & pu_rif_ingombr_val & "," & apiceTextBox(pu_suggerimenti) & "," _
                        & po_regolar & "," & po_regolar_val & "," & po_qualita & "," & po_qualita_val & "," & po_cortesia & "," _
                        & po_cortesia_val & "," & po_inf_complete & "," & po_inf_complete_val & "," & po_posta & "," _
                        & po_posta_val & "," & apiceTextBox(po_suggerimenti) & "," & ri_regolarita & "," & ri_regolarita_val & "," _
                        & ri_qualita & "," & ri_qualita_val & "," & ri_cortesia & "," & ri_cortesia_val & "," & ri_temperatura & "," _
                        & ri_temperatura_val & "," & ri_guasti & "," & ri_guasti_val & "," & ri_ris_guasti & "," & ri_ris_guasti_val & "," _
                        & apiceTextBox(ri_suggerimenti) & "," & ve_regolarita & "," & ve_regolarita_val & "," & ve_qualita & "," _
                        & ve_qualita_val & "," & ve_cortesia & "," & ve_cortesia_val & "," & ve_tempestivita & "," & ve_tempestivita_val & "," _
                        & ve_rumore & "," & ve_rumore_val & "," & ve_smaltimento_rif & "," & ve_smaltimento_rif_val & "," _
                        & apiceTextBox(ve_suggerimenti) & ",'" & giud_compless & "', SISCOM_MI.SEQ_CUSTOMER_SATISFACTION.NEXTVAL," _
                        & Session.Item("ID_OPERATORE") & ",'" & data_inserimento & "')"

                    'se invece id <> -1 UPDATE
                Else
                    par.cmd.CommandText = "UPDATE SISCOM_MI.CUSTOMER_SATISFACTION SET ID_UNITA=" & id_unita & ", DATA_COMPILAZIONE='" & data_comp & "', PU_REGOLARITA= " _
                        & pu_regolar & ", PU_REGOLARITA_VAL=" & pu_regolar_val & ", PU_QUALITA=" & pu_qualita & ",PU_QUALITA_VAL=" & pu_qualita_val & ", PU_CORTESIA=" & pu_cortesia & ", PU_CORTESIA_VAL=" _
                        & pu_cortesia_val & ", PU_IGIENE=" & pu_igiene & ", PU_IGIENE_VAL=" & pu_igiene_val & ", PU_PARTI_COMUNI=" & pu_parti_comuni & ", PU_PARTI_COMUNI_VAL=" _
                        & pu_parti_comuni_val & ", PU_RIF_INGOMBRANTI=" & pu_rif_ingombr & ", PU_RIF_INGOMBRANTI_VAL=" & pu_rif_ingombr_val & ",PU_SUGGERIMENTI=" & apiceTextBox(pu_suggerimenti) & ", PO_REGOLARITA=" _
                        & po_regolar & ",PO_REGOLARITA_VAL=" & po_regolar_val & ",PO_QUALITA=" & po_qualita & ",PO_QUALITA_VAL=" & po_qualita_val & ",PO_CORTESIA=" & po_cortesia & ",PO_CORTESIA_VAL=" _
                        & po_cortesia_val & ",PO_INF_COMPLETE=" & po_inf_complete & ",PO_INF_COMPLETE_VAL=" & po_inf_complete_val & ",PO_POSTA=" & po_posta & ",PO_POSTA_VAL=" _
                        & po_posta_val & ",PO_SUGGERIMENTI=" & apiceTextBox(po_suggerimenti) & ",RI_REGOLARITA=" & ri_regolarita & ",RI_REGOLARITA_VAL=" & ri_regolarita_val & ",RI_QUALITA=" _
                        & ri_qualita & ",RI_QUALITA_VAL=" & ri_qualita_val & ",RI_CORTESIA=" & ri_cortesia & ",RI_CORTESIA_VAL=" & ri_cortesia_val & ",RI_TEMPERATURA=" & ri_temperatura & ",RI_TEMPERATURA_VAL=" _
                        & ri_temperatura_val & ",RI_GUASTI=" & ri_guasti & ",RI_GUASTI_VAL=" & ri_guasti_val & ",RI_RIS_GUASTI=" & ri_ris_guasti & ",RI_RIS_GUASTI_VAL=" & ri_ris_guasti_val & ",RI_SUGGERIMENTI=" _
                        & apiceTextBox(ri_suggerimenti) & ",VE_REGOLARITA=" & ve_regolarita & ",VE_REGOLARITA_VAL=" & ve_regolarita_val & ",VE_QUALITA=" & ve_qualita & ",VE_QUALITA_VAL=" _
                        & ve_qualita_val & ",VE_CORTESIA=" & ve_cortesia & ",VE_CORTESIA_VAL=" & ve_cortesia_val & ",VE_TEMPESTIVITA=" & ve_tempestivita & ",VE_TEMPESTIVITA_VAL=" & ve_tempestivita_val & ",VE_RUMORE=" _
                        & ve_rumore & ",VE_RUMORE_VAL=" & ve_rumore_val & ",VE_SMALTIMENTO_RIF=" & ve_smaltimento_rif & ",VE_SMALTIMENTO_RIF_VAL=" & ve_smaltimento_rif_val & ",VE_SUGGERIMENTI=" _
                        & apiceTextBox(ve_suggerimenti) & ",GIUDIZIO_COMPLESSIVO='" & giud_compless & "',ID_OPERATORE= " & Session.Item("ID_OPERATORE") & "," _
                        & "DATA_INSERIMENTO_OP = '" & data_inserimento & "' WHERE ID=" & id_chiave.Value
                End If

                par.cmd.ExecuteNonQuery()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If id_chiave.Value = -1 Then
                    Response.Write("<script>alert('Inserimento avvenuto con successo!');location.replace('IntroInserimento.aspx')</script>")
                Else
                    Response.Write("<script>alert('Modifica avvenuta con successo!');location.replace('RicercaSchede.aspx')</script>")
                End If

            Catch ex As Exception

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lbl_infoImm.Text = ex.Message

            End Try
        End If

    End Sub

    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        If Request.QueryString("chk") = "1" Then
            Response.Write("<script>self.close()</script>")
        Else
            Response.Redirect("pagina_home.aspx")
        End If
    End Sub

    Function apiceTextBox(ByVal str As String) As String
        Try
            If IsNothing(str) = True Then
                str = "NULL"
            End If
            If Trim(str) <> "NULL" Then
                apiceTextBox = "'" & par.PulisciStrSql(str) & "'"
            Else
                apiceTextBox = str
            End If

            Return apiceTextBox

        Catch ex As Exception
            lbl_infoImm.Text = ex.Message
        End Try

    End Function

    Private Sub DatiSoloLettura()

        For Each ctrl As Control In Me.pulizia.Controls
            If TypeOf ctrl Is DropDownList Then
                DirectCast(ctrl, DropDownList).Enabled = False
            End If
        Next
        For Each ctrl As Control In Me.portierato.Controls
            If TypeOf ctrl Is DropDownList Then
                DirectCast(ctrl, DropDownList).Enabled = False
            End If
        Next
        For Each ctrl As Control In Me.riscaldamento.Controls
            If TypeOf ctrl Is DropDownList Then
                DirectCast(ctrl, DropDownList).Enabled = False
            End If
        Next
        For Each ctrl As Control In Me.verde.Controls
            If TypeOf ctrl Is DropDownList Then
                DirectCast(ctrl, DropDownList).Enabled = False
            End If
        Next
        txt_pulizia.ReadOnly = True
        txt_portierato.ReadOnly = True
        txt_riscaldam.ReadOnly = True
        txt_manutenz.ReadOnly = True
        cbx_complessivo.Enabled = False
        ChPulizia.Enabled = False
        ChPortierato.Enabled = False
        ChRiscaldamento.Enabled = False
        ChVerde.Enabled = False
        Wizard1.FinishCompleteButtonStyle.Width = 0

    End Sub

    Private Sub SoloLetturaPulizia()
        If Not ChPulizia.Checked = True Then
            For Each ctrl As Control In Me.pulizia.Controls
                If TypeOf ctrl Is DropDownList Then
                    DirectCast(ctrl, DropDownList).Enabled = False
                End If
            Next
            txt_pulizia.ReadOnly = True
        Else
            For Each ctrl As Control In Me.pulizia.Controls
                If TypeOf ctrl Is DropDownList Then
                    DirectCast(ctrl, DropDownList).Enabled = True
                End If
            Next
            txt_pulizia.ReadOnly = False
        End If
    End Sub

    Private Sub SoloLetturaPortierato()
        If Not ChPortierato.Checked = True Then
            For Each ctrl As Control In Me.portierato.Controls
                If TypeOf ctrl Is DropDownList Then
                    DirectCast(ctrl, DropDownList).Enabled = False
                End If
            Next
            txt_portierato.ReadOnly = True
        Else
            For Each ctrl As Control In Me.portierato.Controls
                If TypeOf ctrl Is DropDownList Then
                    DirectCast(ctrl, DropDownList).Enabled = True
                End If
            Next
            txt_portierato.ReadOnly = False
        End If
    End Sub

    Private Sub SoloLetturaRiscaldamento()
        If Not ChRiscaldamento.Checked = True Then
            For Each ctrl As Control In Me.riscaldamento.Controls
                If TypeOf ctrl Is DropDownList Then
                    DirectCast(ctrl, DropDownList).Enabled = False
                End If
            Next
            txt_riscaldam.ReadOnly = True
        Else
            For Each ctrl As Control In Me.riscaldamento.Controls
                If TypeOf ctrl Is DropDownList Then
                    DirectCast(ctrl, DropDownList).Enabled = True
                End If
            Next
            txt_riscaldam.ReadOnly = False
        End If
    End Sub

    Private Sub SoloLetturaVerde()
        If Not ChVerde.Checked = True Then
            For Each ctrl As Control In Me.verde.Controls
                If TypeOf ctrl Is DropDownList Then
                    DirectCast(ctrl, DropDownList).Enabled = False
                End If
            Next
            txt_manutenz.ReadOnly = True
        Else
            For Each ctrl As Control In Me.verde.Controls
                If TypeOf ctrl Is DropDownList Then
                    DirectCast(ctrl, DropDownList).Enabled = True
                End If
            Next
            txt_manutenz.ReadOnly = False
        End If
    End Sub

    Protected Sub ChPulizia_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChPulizia.CheckedChanged
        SoloLetturaPulizia()
    End Sub


    Protected Sub ChPortierato_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChPortierato.CheckedChanged
        SoloLetturaPortierato()
    End Sub


    Protected Sub ChRiscaldamento_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChRiscaldamento.CheckedChanged
        SoloLetturaRiscaldamento()
    End Sub

    Protected Sub ChVerde_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChVerde.CheckedChanged
        SoloLetturaVerde()
    End Sub
End Class
