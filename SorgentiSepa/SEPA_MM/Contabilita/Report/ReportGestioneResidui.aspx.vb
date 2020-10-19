
Partial Class Contabilita_Report_ReportGestioneResidui
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 796px; height: 540px;" _
              & "top: 0px; left: 0px;background-color: #eeeeee;background-image: url('../../NuoveImm/SfondoMascheraContratti2.jpg');z-index:1000;"">" _
              & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
              & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
              & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
              & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
              & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
              & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
            caricaTipologie()
            caricaTipologieStraordinarie()
            caricaListaMacroCategorie()
            caricaListaCategorie()
            caricaListaTipologieUI()
            caricaListaVoci()
            caricaTipologie()
            caricaTipologieStraordinarie()
            caricaCapitoli()
            caricaCompetenza()

            caricaEsercizioContabile()
        End If
        ImpostaJavaScriptFunction()
    End Sub
    Private Sub ImpostaJavaScriptFunction()
        TextBoxContabilitaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxDataPagamentoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxDataPagamentoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxContabilitaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxRiferimentoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxRiferimentoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxEmissioneAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxEmissioneDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxAggiornamentoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Sub caricaEsercizioContabile()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT to_char(to_date(VALIDITA_DA,'yyyyMMdd'),'dd/MM/yyyy')||' - '||to_char(to_date(VALIDITA_A,'yyyyMMdd'),'dd/MM/yyyy') AS VALIDITA," _
                & "id, ANNO FROM SISCOM_MI.BOL_BOLLETTE_ES_CONTABILE order by VALIDITA_A"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListEserciziContabili.Items.Clear()
                CheckBoxListEserciziContabili.DataSource = dataTable
                CheckBoxListEserciziContabili.DataValueField = "id"
                CheckBoxListEserciziContabili.DataTextField = "VALIDITA"
                CheckBoxListEserciziContabili.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Private Sub caricaListaVoci()
        Try
            ApriConnessione()
            Dim macroCategorieSelezionate As String = ""
            For Each Items As ListItem In CheckBoxListMacrocategorie.Items
                If Items.Selected = True Then
                    macroCategorieSelezionate &= Items.Value & ","
                End If
            Next
            If macroCategorieSelezionate <> "" Then
                macroCategorieSelezionate = Left(macroCategorieSelezionate, Len(macroCategorieSelezionate) - 1)
            End If
            Dim CategorieSelezionate As String = ""
            For Each Items As ListItem In CheckBoxListCategorie.Items
                If Items.Selected = True Then
                    CategorieSelezionate &= Items.Value & ","
                End If
            Next
            If CategorieSelezionate <> "" Then
                CategorieSelezionate = Left(CategorieSelezionate, Len(CategorieSelezionate) - 1)
            End If

            Dim competenzeSelezionate As String = ""
            For Each Items As ListItem In CheckBoxListCompetenza.Items
                If Items.Selected = True Then
                    competenzeSelezionate &= Items.Value & ","
                End If
            Next
            If competenzeSelezionate <> "" Then
                competenzeSelezionate = Left(competenzeSelezionate, Len(competenzeSelezionate) - 1)
            End If

            If competenzeSelezionate = "" Then
                If CategorieSelezionate = "" And macroCategorieSelezionate = "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate = "" And macroCategorieSelezionate <> "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND GRUPPO IN(" & macroCategorieSelezionate & ") ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate <> "" And macroCategorieSelezionate = "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND TIPO_VOCE IN(" & CategorieSelezionate & ") ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate <> "" And macroCategorieSelezionate <> "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND TIPO_VOCE IN(" & CategorieSelezionate & ") AND GRUPPO IN(" & macroCategorieSelezionate & ") ORDER BY DESCRIZIONE"
                End If
            Else
                If CategorieSelezionate = "" And macroCategorieSelezionate = "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 ORDER BY DESCRIZIONE"
                    'par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND COMPETENZA IN (" & competenzeSelezionate & ") ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate = "" And macroCategorieSelezionate <> "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND GRUPPO IN(" & macroCategorieSelezionate & ") AND COMPETENZA IN (" & competenzeSelezionate & ") ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate <> "" And macroCategorieSelezionate = "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND TIPO_VOCE IN(" & CategorieSelezionate & ") AND COMPETENZA IN (" & competenzeSelezionate & ") ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate <> "" And macroCategorieSelezionate <> "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND TIPO_VOCE IN(" & CategorieSelezionate & ") AND GRUPPO IN(" & macroCategorieSelezionate & ") AND COMPETENZA IN (" & competenzeSelezionate & ") ORDER BY DESCRIZIONE"
                End If
            End If
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            CheckBoxListVoci.Items.Clear()
            CheckBoxListVoci.DataSource = dataTable
            CheckBoxListVoci.DataTextField = "DESCRIZIONE"
            CheckBoxListVoci.DataValueField = "ID"
            CheckBoxListVoci.DataBind()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub
    Private Sub caricaCompetenza()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA_COMPETENZA ORDER BY 2"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListCompetenza.Items.Clear()
                CheckBoxListCompetenza.DataSource = dataTable
                CheckBoxListCompetenza.DataTextField = "DESCRIZIONE"
                CheckBoxListCompetenza.DataValueField = "ID"
                CheckBoxListCompetenza.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub
    Private Sub caricaListaTipologieUI()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ORDER BY DESCRIZIONE"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListTipologieUI.Items.Clear()
                CheckBoxListTipologieUI.DataSource = dataTable
                CheckBoxListTipologieUI.DataTextField = "DESCRIZIONE"
                CheckBoxListTipologieUI.DataValueField = "COD"
                CheckBoxListTipologieUI.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub
    Private Sub caricaTipologie()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_BOLLETTE WHERE ID<21 ORDER BY DESCRIZIONE ASC"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListTipologiaBollettazione.Items.Clear()
                CheckBoxListTipologiaBollettazione.DataSource = dataTable
                CheckBoxListTipologiaBollettazione.DataValueField = "ID"
                CheckBoxListTipologiaBollettazione.DataTextField = "DESCRIZIONE"
                CheckBoxListTipologiaBollettazione.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub
    Private Sub caricaTipologieStraordinarie()
        Dim controllaCheckStraordinario As Boolean = False
        For Each Items As ListItem In CheckBoxListTipologiaBollettazione.Items
            If Items.Text = "STRAORDINARIA" Then
                If Items.Selected = True Then
                    controllaCheckStraordinario = True
                    Exit For
                End If
            End If
        Next
        If controllaCheckStraordinario = True Then
            Try
                ApriConnessione()
                par.cmd.CommandText = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_BOLLETTE WHERE ACRONIMO LIKE 'STR%' AND ID>=21"
                Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dataTable As New Data.DataTable
                dataAdapter.Fill(dataTable)
                chiudiConnessione()
                If dataTable.Rows.Count > 0 Then
                    CheckBoxListTipologiaStraordinaria.Items.Clear()
                    CheckBoxListTipologiaStraordinaria.DataSource = dataTable
                    CheckBoxListTipologiaStraordinaria.DataValueField = "ID"
                    CheckBoxListTipologiaStraordinaria.DataTextField = "DESCRIZIONE"
                    CheckBoxListTipologiaStraordinaria.DataBind()
                End If
            Catch ex As Exception
                chiudiConnessione()
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
            End Try
            Panel2.Visible = True
        Else
            Panel2.Visible = False
            For Each Items As ListItem In CheckBoxListTipologiaStraordinaria.Items
                Items.Selected = False
            Next
        End If
    End Sub
    Protected Sub chiudiConnessione()
        par.cmd.Dispose()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Protected Sub ApriConnessione()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
    End Sub

    Protected Sub CheckBoxListTipologiaBollettazione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListTipologiaBollettazione.SelectedIndexChanged
        caricaTipologieStraordinarie()
        Dim Script As String = "document.getElementById('PanelTipo').scrollTop = document.getElementById('yPosTipo').value;"
        ScriptManager.RegisterStartupScript(PanelTipo, GetType(Panel), Page.ClientID, Script, True)
    End Sub

    Protected Sub CheckBoxListCompetenza_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListCompetenza.SelectedIndexChanged
        caricaListaVoci()
        Dim Script As String = "document.getElementById('PanelCompetenza').scrollTop = document.getElementById('yPosCompetenza').value;"
        ScriptManager.RegisterStartupScript(PanelCompetenza, GetType(Panel), Page.ClientID, Script, True)
    End Sub

    Protected Sub CheckBoxListMacrocategorie_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListMacrocategorie.SelectedIndexChanged
        caricaListaCategorie()
        caricaListaVoci()
        Dim Script As String = "document.getElementById('PanelMacrocategorie').scrollTop = document.getElementById('yPosMacrocategorie').value;"
        ScriptManager.RegisterStartupScript(PanelMacrocategorie, GetType(Panel), Page.ClientID, Script, True)
    End Sub

    Protected Sub CheckBoxListCategorie_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListCategorie.SelectedIndexChanged
        caricaListaVoci()
        Dim Script As String = "document.getElementById('PanelCategorie').scrollTop = document.getElementById('yPosCategorie').value;"
        ScriptManager.RegisterStartupScript(PanelCategorie, GetType(Panel), Page.ClientID, Script, True)
    End Sub

    Protected Sub ImageButtonAvanti_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAvanti.Click
        MultiView1.ActiveViewIndex = 1
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "red", "location.replace('../pagina_home.aspx')", True)
    End Sub

    Protected Sub ImageButtonHome_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonHome.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "red", "location.replace('../pagina_home.aspx')", True)
    End Sub

    Protected Sub ImageButtonIndietro_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonIndietro.Click
        MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub CheckBoxCompetenza_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxCompetenza.CheckedChanged
        If CheckBoxCompetenza.Checked = True Then
            For Each Items As ListItem In CheckBoxListCompetenza.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListCompetenza.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Protected Sub CheckBoxMacrocategorie_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxMacrocategorie.CheckedChanged
        If CheckBoxMacrocategorie.Checked = True Then
            For Each Items As ListItem In CheckBoxListMacrocategorie.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListMacrocategorie.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Protected Sub CheckBoxCategorie_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxCategorie.CheckedChanged
        If CheckBoxCategorie.Checked = True Then
            For Each Items As ListItem In CheckBoxListCategorie.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListCategorie.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Protected Sub CheckBoxVoci_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxVoci.CheckedChanged
        If CheckBoxVoci.Checked = True Then
            For Each Items As ListItem In CheckBoxListVoci.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListVoci.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Protected Sub CheckBoxEserciziContabili_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxEserciziContabili.CheckedChanged
        If CheckBoxEserciziContabili.Checked = True Then
            For Each Items As ListItem In CheckBoxListEserciziContabili.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListEserciziContabili.Items
                Items.Selected = False
            Next
        End If
    End Sub
    Private Sub caricaListaMacroCategorie()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA_GRUPPI ORDER BY 2"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListMacrocategorie.Items.Clear()
                CheckBoxListMacrocategorie.DataSource = dataTable
                CheckBoxListMacrocategorie.DataTextField = "DESCRIZIONE"
                CheckBoxListMacrocategorie.DataValueField = "ID"
                CheckBoxListMacrocategorie.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Private Sub caricaListaCategorie()
        Try
            ApriConnessione()
            Dim macroCategorieSelezionate As String = ""
            For Each Items As ListItem In CheckBoxListMacrocategorie.Items
                If Items.Selected = True Then
                    macroCategorieSelezionate &= Items.Value & ","
                End If
            Next
            If macroCategorieSelezionate = "" Then
                par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA_TIPI ORDER BY 1"
            Else
                macroCategorieSelezionate = Left(macroCategorieSelezionate, Len(macroCategorieSelezionate) - 1)
                par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA_TIPI WHERE GRUPPO IN (" & macroCategorieSelezionate & ") ORDER BY 1"
            End If
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            CheckBoxListCategorie.Items.Clear()
            CheckBoxListCategorie.DataSource = dataTable
            CheckBoxListCategorie.DataTextField = "DESCRIZIONE"
            CheckBoxListCategorie.DataValueField = "ID"
            CheckBoxListCategorie.DataBind()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Protected Sub CheckBoxTipologieUI_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxTipologieUI.CheckedChanged
        If CheckBoxTipologieUI.Checked = True Then
            For Each Items As ListItem In CheckBoxListTipologieUI.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListTipologieUI.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Protected Sub ImageButtonAvviaRicerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAvviaRicerca.Click
        Dim parametriDaPassare As String = ""
        parametriDaPassare &= "?DataContabileDa=" & par.FormatoDataDB(TextBoxContabilitaDal.Text)
        parametriDaPassare &= "&DataContabileA=" & par.FormatoDataDB(TextBoxContabilitaAl.Text)
        parametriDaPassare &= "&DataRiferimentoDal=" & par.FormatoDataDB(TextBoxRiferimentoDal.Text)
        parametriDaPassare &= "&DataRiferimentoAl=" & par.FormatoDataDB(TextBoxRiferimentoAl.Text)
        parametriDaPassare &= "&DataEmissioneDal=" & par.FormatoDataDB(TextBoxEmissioneDal.Text)
        parametriDaPassare &= "&DataEmissioneAl=" & par.FormatoDataDB(TextBoxEmissioneAl.Text)
        parametriDaPassare &= "&DataPagamentoDal=" & par.FormatoDataDB(TextBoxDataPagamentoDal.Text)
        parametriDaPassare &= "&DataPagamentoAl=" & par.FormatoDataDB(TextBoxDataPagamentoAl.Text)
        parametriDaPassare &= "&DataAggiornamento=" & par.FormatoDataDB(TextBoxAggiornamentoAl.Text)
        parametriDaPassare &= "&TipologiaContoCorrente=" & DropDownListContoCorrente.SelectedValue
        parametriDaPassare &= "&Condominio=" & DropDownListCondomini.SelectedValue




        Dim listaTipologiaBollettazione As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListTipologiaBollettazione.Items
            If Items.Selected = True Then
                If Not listaTipologiaBollettazione.Contains(Items.Value) Then
                    listaTipologiaBollettazione.Add(Items.Value)
                End If
            Else
                If listaTipologiaBollettazione.Contains(Items.Value) Then
                    listaTipologiaBollettazione.Remove(Items.Value)
                End If
            End If
        Next
        For Each Items As ListItem In CheckBoxListTipologiaStraordinaria.Items
            If Items.Selected = True Then
                If Not listaTipologiaBollettazione.Contains(Items.Value) Then
                    listaTipologiaBollettazione.Add(Items.Value)
                End If
            Else
                If listaTipologiaBollettazione.Contains(Items.Value) Then
                    listaTipologiaBollettazione.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaTipologiaBollettazione", listaTipologiaBollettazione)


        Dim listavoci As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListVoci.Items
            If Items.Selected = True Then
                If Not listavoci.Contains(Items.Value) Then
                    listavoci.Add(Items.Value)
                End If
            Else
                If listavoci.Contains(Items.Value) Then
                    listavoci.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaVoci", listavoci)


        Dim listaCategorie As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListCategorie.Items
            If Items.Selected = True Then
                If Not listaCategorie.Contains(Items.Value) Then
                    listaCategorie.Add(Items.Value)
                End If
            Else
                If listaCategorie.Contains(Items.Value) Then
                    listaCategorie.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaCategorie", listaCategorie)


        Dim listaMacrocategorie As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListMacrocategorie.Items
            If Items.Selected = True Then
                If Not listaMacrocategorie.Contains(Items.Value) Then
                    listaMacrocategorie.Add(Items.Value)
                End If
            Else
                If listaMacrocategorie.Contains(Items.Value) Then
                    listaMacrocategorie.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaMacrocategorie", listaMacrocategorie)


        Dim listaTipologieUI As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListTipologieUI.Items
            If Items.Selected = True Then
                If Not listaTipologieUI.Contains(Items.Value) Then
                    listaTipologieUI.Add(Items.Value)
                End If
            Else
                If listaTipologieUI.Contains(Items.Value) Then
                    listaTipologieUI.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaTipologieUI", listaTipologieUI)

        Dim listacompetenza As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListCompetenza.Items
            If Items.Selected = True Then
                If Not listacompetenza.Contains(Items.Value) Then
                    listacompetenza.Add(Items.Value)
                End If
            Else
                If listacompetenza.Contains(Items.Value) Then
                    listacompetenza.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listacompetenza", listacompetenza)

        Dim listaCapitoli As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListCapitoli.Items
            If Items.Selected = True Then
                If Not listaCapitoli.Contains(Items.Value) Then
                    listaCapitoli.Add(Items.Value)
                End If
            Else
                If listaCapitoli.Contains(Items.Value) Then
                    listaCapitoli.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaCapitoli", listaCapitoli)


        Dim listaEserciziContabili As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListEserciziContabili.Items
            If Items.Selected = True Then
                If Not listaEserciziContabili.Contains(Items.Value) Then
                    listaEserciziContabili.Add(Items.Value)
                End If
            Else
                If listaEserciziContabili.Contains(Items.Value) Then
                    listaEserciziContabili.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaEserciziContabili", listaEserciziContabili)


        Dim parametriRicercaEmissione As String = ""
        If TextBoxEmissioneAl.Text <> "" And TextBoxEmissioneDal.Text = "" Then
            parametriRicercaEmissione = "Data emissione al " & TextBoxEmissioneAl.Text
        ElseIf TextBoxEmissioneAl.Text <> "" And TextBoxEmissioneDal.Text <> "" Then
            parametriRicercaEmissione = "Data emissione dal " & TextBoxEmissioneDal.Text & " al " & TextBoxEmissioneAl.Text
        ElseIf TextBoxEmissioneAl.Text = "" And TextBoxEmissioneDal.Text = "" Then
            parametriRicercaEmissione = ""
        ElseIf TextBoxEmissioneAl.Text = "" And TextBoxEmissioneDal.Text <> "" Then
            parametriRicercaEmissione = "Data emissione dal " & TextBoxEmissioneDal.Text
        End If

        Dim parametriRicercaRiferimento As String = ""
        If TextBoxRiferimentoAl.Text <> "" And TextBoxRiferimentoDal.Text = "" Then
            parametriRicercaRiferimento = "Data riferimento al " & TextBoxRiferimentoAl.Text
        ElseIf TextBoxRiferimentoAl.Text <> "" And TextBoxRiferimentoDal.Text <> "" Then
            parametriRicercaRiferimento = "Data riferimento dal " & TextBoxRiferimentoDal.Text & " al " & TextBoxRiferimentoAl.Text
        ElseIf TextBoxRiferimentoAl.Text = "" And TextBoxRiferimentoDal.Text = "" Then
            parametriRicercaRiferimento = ""
        ElseIf TextBoxRiferimentoAl.Text = "" And TextBoxRiferimentoDal.Text <> "" Then
            parametriRicercaRiferimento = "Data riferimento dal " & TextBoxRiferimentoDal.Text
        End If

        Dim parametriRicercaAggiornamento As String = ""
        If TextBoxAggiornamentoAl.Text <> "" Then
            parametriRicercaAggiornamento = "Aggiornamento al " & TextBoxAggiornamentoAl.Text
        End If


        Dim parametriRicercaContabilita As String = ""
        If TextBoxContabilitaAl.Text <> "" And TextBoxContabilitaDal.Text = "" Then
            parametriRicercaContabilita = "Contabilita al " & TextBoxContabilitaAl.Text
        ElseIf TextBoxContabilitaAl.Text <> "" And TextBoxContabilitaDal.Text <> "" Then
            parametriRicercaContabilita = "Contabilita dal " & TextBoxContabilitaDal.Text & " al " & TextBoxContabilitaAl.Text
        ElseIf TextBoxContabilitaAl.Text = "" And TextBoxContabilitaDal.Text = "" Then
            parametriRicercaContabilita = ""
        ElseIf TextBoxContabilitaAl.Text = "" And TextBoxContabilitaDal.Text <> "" Then
            parametriRicercaContabilita = "Contabilita dal " & TextBoxContabilitaDal.Text
        End If

        Dim parametriRicercaDataPagamento As String = ""
        If TextBoxDataPagamentoAl.Text <> "" And TextBoxDataPagamentoDal.Text = "" Then
            parametriRicercaDataPagamento = "DataPagamento al " & TextBoxDataPagamentoAl.Text
        ElseIf TextBoxDataPagamentoAl.Text <> "" And TextBoxDataPagamentoDal.Text <> "" Then
            parametriRicercaDataPagamento = "DataPagamento dal " & TextBoxDataPagamentoDal.Text & " al " & TextBoxDataPagamentoAl.Text
        ElseIf TextBoxDataPagamentoAl.Text = "" And TextBoxDataPagamentoDal.Text = "" Then
            parametriRicercaDataPagamento = ""
        ElseIf TextBoxDataPagamentoAl.Text = "" And TextBoxDataPagamentoDal.Text <> "" Then
            parametriRicercaDataPagamento = "DataPagamento dal " & TextBoxDataPagamentoDal.Text
        End If

        Dim parametriRicercaContocorrente As String = ""
        If DropDownListContoCorrente.SelectedValue = 0 Then
            parametriRicercaContocorrente = "CONTO CORRENTE: tutti"
        ElseIf DropDownListContoCorrente.SelectedValue = 1 Then
            parametriRicercaContocorrente = "CONTO CORRENTE: c/c 59"
        ElseIf DropDownListContoCorrente.SelectedValue = 2 Then
            parametriRicercaContocorrente = "CONTO CORRENTE: c/c 60"
        End If


        Dim parametriRicercaCondominio As String = ""
        If DropDownListCondomini.SelectedValue = -1 Then
            parametriRicercaCondominio = "CONDOMINI: nessuno"
        ElseIf DropDownListCondomini.SelectedValue = 0 Then
            parametriRicercaCondominio = "CONDOMINI: non in condominio"
        ElseIf DropDownListCondomini.SelectedValue = 3 Then
            parametriRicercaCondominio = "CONDOMINI: tutti i condomini"
        ElseIf DropDownListCondomini.SelectedValue = 1 Then
            parametriRicercaCondominio = "CONDOMINI: condomini gestione diretta"
        ElseIf DropDownListCondomini.SelectedValue = 2 Then
            parametriRicercaCondominio = "CONDOMINI: condomini gestione indiretta"
        End If


        Dim parametriRicercaC As String = ""
        For Each Items As ListItem In CheckBoxListCompetenza.Items
            If Items.Selected = True Then
                parametriRicercaC &= UCase(Left(Items.Text, 1)) & LCase(Mid(Items.Text, 2)) & ","
            End If
        Next
        If parametriRicercaC <> "" Then
            parametriRicercaC = "COMPETENZA: " & Left(parametriRicercaC, Len(parametriRicercaC) - 1)
        End If

        Dim parametriRicercaMC As String = ""
        For Each Items As ListItem In CheckBoxListMacrocategorie.Items
            If Items.Selected = True Then
                parametriRicercaMC &= UCase(Left(Items.Text, 1)) & LCase(Mid(Items.Text, 2)) & ","
            End If
        Next
        If parametriRicercaMC <> "" Then
            parametriRicercaMC = "MACROCATEGORIE: " & Left(parametriRicercaMC, Len(parametriRicercaMC) - 1)
        End If

        Dim parametriRicercaTipologiaBollettazione As String = ""
        For Each Items As ListItem In CheckBoxListTipologiaBollettazione.Items
            If Items.Selected = True Then
                parametriRicercaTipologiaBollettazione &= UCase(Left(Items.Text, 1)) & LCase(Mid(Items.Text, 2)) & ","
            End If
        Next
        For Each Items As ListItem In CheckBoxListTipologiaStraordinaria.Items
            If Items.Selected = True Then
                parametriRicercaTipologiaBollettazione &= UCase(Left(Items.Text, 1)) & LCase(Mid(Items.Text, 2)) & ","
            End If
        Next
        If parametriRicercaTipologiaBollettazione <> "" Then
            parametriRicercaTipologiaBollettazione = "TIPOLOGIA BOLLETTAZIONE: " & Left(parametriRicercaTipologiaBollettazione, Len(parametriRicercaTipologiaBollettazione) - 1)
        End If


        Dim parametriDate As String = ""
        parametriDate = parametriRicercaEmissione
        If parametriDate <> "" And parametriRicercaRiferimento <> "" Then
            parametriDate &= ", " & parametriRicercaRiferimento
        Else
            parametriDate &= parametriRicercaRiferimento
        End If
        If parametriDate <> "" And parametriRicercaAggiornamento <> "" Then
            parametriDate &= ", " & parametriRicercaAggiornamento
        Else
            parametriDate &= parametriRicercaAggiornamento
        End If
        If parametriDate <> "" And parametriRicercaDataPagamento <> "" Then
            parametriDate &= ", " & parametriRicercaDataPagamento
        Else
            parametriDate &= parametriRicercaDataPagamento
        End If
        If parametriDate <> "" And parametriRicercaContabilita <> "" Then
            parametriDate &= ", " & parametriRicercaContabilita
        Else
            parametriDate &= parametriRicercaContabilita
        End If

        If Dettaglio.Checked = True Then
            parametriDaPassare &= "&Dettaglio=1"
        Else
            parametriDaPassare &= "&Dettaglio=0"
        End If

        If Ordinamento.SelectedValue = 1 Then
            parametriDaPassare &= "&Ordinamento=1"
        Else
            parametriDaPassare &= "&Ordinamento=0"
        End If

        Dim parametriRicercaCapitolo As String = ""
        For Each Items As ListItem In CheckBoxListCapitoli.Items
            If Items.Selected = True Then
                parametriRicercaCapitolo &= UCase(Left(Items.Text, 1)) & LCase(Mid(Items.Text, 2)) & ","
            End If
        Next
        If parametriRicercaCapitolo <> "" Then
            parametriRicercaCapitolo = "CAPITOLI: " & Left(parametriRicercaCapitolo, Len(parametriRicercaCapitolo) - 1)
        End If

        Dim parametriRicercaEsercizio As String = ""
        For Each Items As ListItem In CheckBoxListEserciziContabili.Items
            If Items.Selected = True Then
                parametriRicercaEsercizio &= UCase(Left(Items.Text, 1)) & LCase(Mid(Items.Text, 2)) & ","
            End If
        Next
        If parametriRicercaEsercizio <> "" Then
            parametriRicercaEsercizio = "ESERCIZI CONTABILI: " & Left(parametriRicercaEsercizio, Len(parametriRicercaEsercizio) - 1)
        End If

        Session.Add("filtriRicerca", parametriRicercaTipologiaBollettazione & " - " _
                    & parametriDate & " - " _
                    & parametriRicercaContocorrente & ", " _
                    & parametriRicercaCondominio & ", " _
                    & parametriRicercaC & ", " _
                    & parametriRicercaMC & ", " _
                    & parametriRicercaEsercizio & ", " _
                    & parametriRicercaCapitolo)

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "window.open('RisultatiGestioneResidui.aspx" & parametriDaPassare & "','_blank','');", True)

    End Sub

    Private Sub caricaCapitoli()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_VOCI_BOLLETTA_CAP ORDER BY NLSSORT(descrizione,'NLS_SORT=BINARY')"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListCapitoli.Items.Clear()
                CheckBoxListCapitoli.DataSource = dataTable
                CheckBoxListCapitoli.DataValueField = "id"
                CheckBoxListCapitoli.DataTextField = "descrizione"
                CheckBoxListCapitoli.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Protected Sub CheckBoxCapitoli_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxCapitoli.CheckedChanged
        If CheckBoxCapitoli.Checked = True Then
            For Each Items As ListItem In CheckBoxListCapitoli.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListCapitoli.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Protected Sub CheckBoxTipo_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxTipo.CheckedChanged
        If CheckBoxTipo.Checked = True Then
            For Each Items As ListItem In CheckBoxListTipologiaBollettazione.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListTipologiaBollettazione.Items
                Items.Selected = False
            Next
        End If
        caricaTipologieStraordinarie()
    End Sub
End Class
