Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivoHome
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If
        If Not IsPostBack Then
            CaricaOperatore()
            Bp_Formalizzazione.Value = Session.Item("BP_FORMALIZZAZIONE")
            HFBpCompilazione.Value = Session.Item("BP_COMPILAZIONE")
            HFBpConvAler.Value = Session.Item("BP_CONV_ALER")
            HFBpGestCapitoli.Value = Session.Item("BP_CAPITOLI")
            HFVociServizi.Value = Session.Item("BP_VOCI_SERVIZI")
            HFBpGenerale.Value = Session.Item("BP_GENERALE")
            HFIdStruttura.Value = Session.Item("ID_STRUTTURA")
            HFParamCP.Value = Session.Item("FL_PARAM_CICLO_PASSIVO")
            HFModBuildingManager.Value = Session.Item("MOD_BUILDING_MANAGER")
            HFBpConvComune.Value = Session.Item("BP_CONV_COMUNE")
            HFBpMS.Value = Session.Item("BP_MS")
            HFSTR.Value = Session.Item("FL_ESTRAZIONE_STR")
            'If Session.Item("ID_OPERATORE") <> 1755 Then
            '    NavigationMenu.FindItemByValue("ImportaODL").Remove()
            'End If
            If Session.Item("ID_OPERATORE") <> 1755 Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "SepGestioneCambioIva")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "CambioIVA")
            End If
            If Session.Item("FL_UTENZE") <> "1" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "SepUtenzeMulte")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "MenuCustodi")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "SepOrdiniUtenze")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "MenuUtenze")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "SepCustodiMulte")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "Multe")
            End If
            'If Session.Item("BP_NUOVO") <> "1" Then
            '    NavigationMenu.FindItemByValue("NuovoBP").Remove()
            'End If
            'If Session.Item("BP_COMPILAZIONE") <> "1" Then
            '    NavigationMenu.FindItemByValue("CompilazioneBP").Remove()
            'End If
            'If Session.Item("BP_CONV_ALER") <> "1" Then
            '    NavigationMenu.FindItemByValue("ConvalidaAler").Remove()
            'End If
            'If Session.Item("BP_CAPITOLI") <> "1" Then
            '    NavigationMenu.FindItemByValue("Capitoli").Remove()
            'End If
            'If Session.Item("BP_CONV_COMUNE") <> "1" Then
            '    '    NavigationMenu.FindItemByValue("ConvalidaComune").Remove()
            'End If
            'If Session.Item("BP_VOCI_SERVIZI") <> "1" Then
            '    NavigationMenu.FindItemByValue("Voci Servizi").Remove()
            'End If
            'If Session.Item("BP_GENERALE") <> "1" Then
            '    NavigationMenu.FindItemByValue("Voci Servizi").Remove()
            'End If
            'If Session.Item("MOD_ASS_NUOVO") = 0 Then
            '    NavigationMenu.FindItemByValue("Elenco").Remove()
            'End If
            ''***************gestione menù sinistra Assestamento
            'If Session.Item("BP_GENERALE") <> 1 And Session.Item("MOD_ASS_CONV_COMU") <> 1 Then
            '    NavigationMenu.FindItemByValue("Elenco").Remove()
            'End If
            ''***************gestione menù sinistra Variazioni
            'If Session.Item("BP_VARIAZIONI") <> 1 And Session.Item("BP_VARIAZIONI_SL") <> 1 Then
            '    'NavigationMenu.FindItemByValue("Var").Remove()
            'Else
            '    If Session.Item("BP_VARIAZIONI_SL") = 1 Then
            '        'NavigationMenu.FindItemByValue("NuovaVar").Remove()
            '        'NavigationMenu.FindItemByValue("VariazStrut").Remove()
            '    End If
            'End If
            If Session.Item("FL_CP_DASHBOARD") = "0" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "SepDashboardFornitori")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "Dashboard")
            End If
            If Session.Item("FL_PARAM_CICLO_PASSIVO") = 0 Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "Parametri")
            End If
            'If Session.Item("MOD_MAND_PAGAMENTO") = 0 Then
            '    NavigationMenu.FindItemByValue("MandatiPag").Remove()
            'End If
            If Session.Item("BP_CC") <> "1" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "SepFornitoriContratti")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "APPALTI")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "Fornitori")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "SepDashboardFornitori")
            Else
                If Session.Item("BP_CC_L") = "1" Then
                    par.RimuoviNodoMenuTelerik(NavigationMenu, "InserimentoAppalti")
                End If
            End If
            'LOTTI (3)
            If Session.Item("BP_LO") <> "1" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "GESTIONE_LOTTI")
            Else
                If Session.Item("BP_LO_L") = "1" Then
                    par.RimuoviNodoMenuTelerik(NavigationMenu, "Nuovo_lotto_E")
                    par.RimuoviNodoMenuTelerik(NavigationMenu, "Nuovo_lotto_I")

                End If
            End If

            If Session.Item("BP_MS") <> "1" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "Manutenzioni")
            Else
                If Session.Item("BP_MS_L") = "1" Then
                    par.RimuoviNodoMenuTelerik(NavigationMenu, "Manutenzioni")
                End If
            End If
            If Session.Item("BP_OP") <> "1" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "Ordini_Pagamenti")
            Else
                If Session.Item("BP_OP_L") = "1" Then
                    par.RimuoviNodoMenuTelerik(NavigationMenu, "Inserimento_Pagamenti")
                End If
            End If
            If Session.Item("BP_PC") <> "1" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "Ordini_Pagamenti")
            Else

                If Session.Item("BP_PC_L") = "1" Then
                    par.RimuoviNodoMenuTelerik(NavigationMenu, "StampaPagCanone")
                End If
            End If

            If Session.Item("BP_MS") <> "1" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "RRS")
            Else
                If Session.Item("BP_RSS_L") = "1" Then
                    par.RimuoviNodoMenuTelerik(NavigationMenu, "InserimentoRRS")
                    par.RimuoviNodoMenuTelerik(NavigationMenu, "ConsuntivazioneRRS")
                    par.RimuoviNodoMenuTelerik(NavigationMenu, "NuovoSAL_RRS")
                End If
            End If
            If Session.Item("MOD_MAND_PAGAMENTO") = 0 Then
                ' NavigationMenu.FindItemByValue("MandatiPag").Remove()
            End If

            If Session.Item("BP_GENERALE") = 0 Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ReportGenerale")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "RicercaPerServizi")
            End If
            If Session.Item("BP_RESIDUI") = 0 Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "Residui")
            End If

            If Session.Item("FL_SPESE_REVERSIBILI") = 0 Then
                'NavigationMenu.FindItemByValue("SpeseReversibili").Remove()
            End If
            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "UploadFirma")
            End If
            'STR
            If Session.Item("FL_ESTRAZIONE_STR") <> "1" And Session.Item("FL_CONSUNTIVAZIONE_STR") <> "1" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ExportSTR")
            End If
            If Session.Item("FL_ESTRAZIONE_STR") <> "1" And Session.Item("FL_CONSUNTIVAZIONE_STR") <> "1" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "STR")
            End If
        End If
    End Sub
    Private Sub CaricaOperatore()
        Me.connData = New CM.datiConnessione(par, False, False)
        Try
            lblOperatore.Text = par.IfNull(Session.Item("NOME_OPERATORE").ToString, "- - -")
            If Not IsNothing(Session.Item("ID_STRUTTURA")) Then
                connData.apri(False)
                par.cmd.CommandText = "SELECT NOME, TIPOLOGIA_STRUTTURA_ALER.DESCRIZIONE " _
                                    & "FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.TIPOLOGIA_STRUTTURA_ALER " _
                                    & "WHERE TIPOLOGIA_STRUTTURA_ALER.ID(+) = TAB_FILIALI.ID_TIPO_ST AND TAB_FILIALI.ID = " & par.insDbValue(Session.Item("ID_STRUTTURA").ToString, True)
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    lblFiliale.Text = par.IfEmpty(MyReader("NOME"), "- - -").ToString.ToUpper & "<br>" & par.IfEmpty(MyReader("DESCRIZIONE"), "").ToString.ToUpper
                Else
                    lblFiliale.Text = "- - -<br>- - -"
                End If
                MyReader.Close()
                connData.chiudi(False)
            Else
                lblFiliale.Text = "- - -<br>- - -"
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: CICLO PASSIVO - HomePage_Master - CaricaOperatore - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        Finally
            connData.PulisciPool()
        End Try
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        'If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Or Session.Item("MOD_BUILDING_MANAGER") = "1" Or Session.Item("FL_CP_TECN_AMM") = "1" Or Session.Item("FL_FQM") = "1" Then
        '    RadWindow1.NavigateUrl = "pagina_home_ncp_dashboard.aspx"
        'Else
        '    RadWindow1.NavigateUrl = "pagina_home_ncp.aspx"
        'End If
        RadWindow1.NavigateUrl = "pagina_home_ncp.aspx"
    End Sub
End Class
