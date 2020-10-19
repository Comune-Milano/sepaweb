
Partial Class CALL_CENTER_RicercaS
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            caricaStrutture()
            caricaComplessi()
            caricaEdifici()
            CaricaStatoSegnalazioni()
            CaricaTipoSegnalazione()
            CaricaTipo()
            cmbSedeTerritoriale.Focus()
            txtDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub
    Private Sub caricaStrutture()
        Try
            connData.apri()
            If Not IsNothing(Request.QueryString("PROV")) AndAlso Request.QueryString("PROV") = "S" Then
                'PROVENIENZA DA AGENDA SEGNALAZIONI
                If Session.Item("ID_STRUTTURA") = "-1" Then
                    par.caricaComboBox("SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID>100 /*AND ID_TIPO_ST=0*/ ORDER BY NOME", cmbSedeTerritoriale, "ID", "NOME", True)
                Else
                    par.caricaComboBox("SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & Session.Item("ID_STRUTTURA") & " AND TAB_FILIALI.ID>100 /*AND ID_TIPO_ST=0*/ ORDER BY NOME", cmbSedeTerritoriale, "ID", "NOME", False)
                End If
            Else
                'PROVENIENZA CALL CENTER
                par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC", cmbSedeTerritoriale, "ID", "NOME", True)
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaComplessi()
        Try
            connData.apri()
            Dim condizioneSedeTerritoriale As String = ""
            If cmbSedeTerritoriale.SelectedValue <> "-1" Then
                condizioneSedeTerritoriale = " AND ID_FILIALE= " & cmbSedeTerritoriale.SelectedValue
            End If
            par.caricaComboBox("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneSedeTerritoriale & " ORDER BY DENOMINAZIONE", cmbComplesso, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaEdifici()
        Try
            connData.apri()
            Dim condizioneComplesso As String = ""
            If cmbComplesso.SelectedValue <> "-1" Then
                condizioneComplesso = " AND ID_COMPLESSO=" & cmbComplesso.SelectedValue
            Else
                If cmbSedeTerritoriale.SelectedValue <> "-1" Then
                    condizioneComplesso = " AND ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE= " & cmbSedeTerritoriale.SelectedValue & ") "
                End If
            End If
            par.caricaComboBox("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 " & condizioneComplesso & " ORDER BY DENOMINAZIONE", cmbEdificio, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaStatoSegnalazioni()
        Try
            connData.apri()
            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE ID >=0", CheckBoxListStato, "ID", "DESCRIZIONE")
            connData.chiudi()
            For Each elemento As ListItem In CheckBoxListStato.Items
                If elemento.Text <> "CHIUSA" Then
                    elemento.Selected = True
                End If
            Next
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipoSegnalazione()
        Try
            connData.apri()
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE", cmbTipoSegnalazione, "ID", "DESCRIZIONE", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipo()
        Try
            connData.apri()
            par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI ", cmbTipo, "ID", "DESCRIZIONE", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim listaStati As String = ""
        For Each elemento As ListItem In CheckBoxListStato.Items
            If elemento.Selected = True Then
                If listaStati = "" Then
                    listaStati = elemento.Value
                Else
                    listaStati &= "," & elemento.Value
                End If
            End If
        Next
        If Not IsNothing(Request.QueryString("PROV")) Then
            Response.Write("<script>document.location.href='RisultatiS.aspx?COMP=" & cmbComplesso.SelectedValue & "&PROV=" & Request.QueryString("PROV").ToString & "&TIPO=" & cmbTipoSegnalazione.SelectedValue & "&T=" & cmbTipo.SelectedItem.Value & "&D=" & par.FormattaData(txtDal.Text) & "&A=" & par.FormattaData(txtAl.Text) & "&F=" & cmbSedeTerritoriale.SelectedItem.Value & "&E=" & cmbEdificio.SelectedItem.Value & "&O=" & txtSegnalante.Text & "&STAT=" & listaStati & "&URG=" & DropDownListUrgenza.SelectedValue & "&NUM=" & TextBoxNumero.Text & "&MINDA=" & TextBoxMinutiDal.Text & "&MINA=" & TextBoxMinutiAl.Text & "&OREA=" & TextBoxOreAl.Text & "&OREDA=" & TextBoxOreDal.Text & "&ORD=" & RadioButtonListOrdine.SelectedValue & "';</script>")
        Else
            Response.Write("<script>document.location.href='RisultatiS.aspx?COMP=" & cmbComplesso.SelectedValue & "&TIPO=" & cmbTipoSegnalazione.SelectedValue & "&T=" & cmbTipo.SelectedItem.Value & "&D=" & par.FormattaData(txtDal.Text) & "&A=" & par.FormattaData(txtAl.Text) & "&F=" & cmbSedeTerritoriale.SelectedItem.Value & "&E=" & cmbEdificio.SelectedItem.Value & "&O=" & txtSegnalante.Text & "&STAT=" & listaStati & "&URG=" & DropDownListUrgenza.SelectedValue & "&NUM=" & TextBoxNumero.Text & "&MINDA=" & TextBoxMinutiDal.Text & "&MINA=" & TextBoxMinutiAl.Text & "&OREA=" & TextBoxOreAl.Text & "&OREDA=" & TextBoxOreDal.Text & "&ORD=" & RadioButtonListOrdine.SelectedValue & "';</script>")
        End If
    End Sub
    Protected Sub cmbTipoSegnalazione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazione.SelectedIndexChanged
        If cmbTipoSegnalazione.SelectedValue = "1" Then
            DropDownListUrgenza.Enabled = True
            TextBoxNumero.Focus()
        Else
            DropDownListUrgenza.Enabled = False
            TextBoxNumero.Focus()
        End If
    End Sub

    Protected Sub cmbSedeTerritoriale_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbSedeTerritoriale.SelectedIndexChanged
        caricaComplessi()
        caricaEdifici()
        cmbComplesso.Focus()
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        caricaEdifici()
        cmbEdificio.Focus()
    End Sub
End Class
