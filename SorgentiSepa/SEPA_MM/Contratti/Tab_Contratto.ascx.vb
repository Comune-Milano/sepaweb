
Partial Class Contratti_Tab_Contratto
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global
    Public indicecontratto As String
    Public indiceconnessione As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        cmbNomeUfficiale.Attributes.Add("onclick", "javascript:document.getElementById('USCITA').value='1';")
        cmbForzoso.Attributes.Add("onclick", "javascript:document.getElementById('USCITA').value='1';")
        cmbMittenteDisdetta.Attributes.Add("onclick", "javascript:document.getElementById('USCITA').value='1';")
        txtDataDelibera.Attributes.Add("onblur", "javascript:confronta_dataDelibera(document.getElementById('Tab_Contratto1_txtDataDelibera').value,'" & Format(Now, "dd/MM/yyyy") & "');")
        ' txtDataTrasST.Attributes.Add("onblur", "javascript:confronta_dataDelibera(document.getElementById('Tab_Contratto1_txtDataDelibera').value,document.getElementById('Tab_Contratto1_txtDataTrasST').value,'" & Format(Now, "dd/MM/yyyy") & "');")
        txtDataTrasST.Attributes.Add("onblur", "javascript:confronta_date(document.getElementById('Tab_Contratto1_txtDataDelibera').value,document.getElementById('Tab_Contratto1_txtDataTrasST').value,'Data Provvedimento','Data trasmissione provvedimento',document.getElementById('Tab_Contratto1_txtDataTrasST').value);")

        'txtDataStipula.Attributes.Add("onblur", "javascript:confronta_date(document.getElementById('Tab_Contratto1_txtDataDelibera').value,document.getElementById('Tab_Contratto1_txtDataStipula').value,'Data Provvedimento','Data Stipula',document.getElementById('Tab_Contratto1_txtDataStipula'));")

        txtDataDecorrenza.Attributes.Add("onblur", "javascript:confronta_date(document.getElementById('Tab_Contratto1_txtDataDelibera').value,document.getElementById('Tab_Contratto1_txtDataDecorrenza').value,'Data Provvedimento','Data Decorrenza',document.getElementById('Tab_Contratto1_txtDataDecorrenza'));")


        '0300042060100000F01
        txtDataConsegna.Attributes.Add("onblur", "javascript:confronta_date(document.getElementById('Tab_Contratto1_txtDataDelibera').value,document.getElementById('Tab_Contratto1_txtDataConsegna').value,'Data Provvedimento','Data Consegna',document.getElementById('Tab_Contratto1_txtDataConsegna'));confronta_date(document.getElementById('Tab_Contratto1_txtDataDecorrenza').value,document.getElementById('Tab_Contratto1_txtDataConsegna').value,'Data Decorrenza','Data Consegna',document.getElementById('Tab_Contratto1_txtDataConsegna'));")

    End Sub

    Public Function DisabilitaMeta()
        txtCodContratto.ReadOnly = True
        cmbNomeUfficiale.Enabled = False
        txtRifLegislativo.ReadOnly = True
        'If UCase(cmbNomeUfficiale.Text) <> "USI DIVERSI" Then
        ' txtDurata.ReadOnly = True
        'txtDurataRinnovo.ReadOnly = True
        'End If
        txtDescrcontratto.ReadOnly = True

    End Function


    Public Function DisabilitaTutto()
        txtCodContratto.ReadOnly = True
        cmbNomeUfficiale.Enabled = False
        txtDataDecorrenza.ReadOnly = True
        txtDataDecAE.ReadOnly = True
        txtDataStipula.ReadOnly = True
        txtDataScadenza.ReadOnly = True
        txtDataRiconsegna.ReadOnly = True
        txtDataAppPresloggio.ReadOnly = True
        txtDataAppSloggio.ReadOnly = True
        txtOraAppPresloggio.ReadOnly = True
        txtOraAppSloggio.ReadOnly = True
        txtDataDelibera.ReadOnly = True
        'max 18/07/2019
        txtDataTrasST.ReadOnly = True
        '---
        txtDataConsegna.ReadOnly = True
        txtDataDisdetta.ReadOnly = True
        txtDataSecScadenza.ReadOnly = True
        'txtNote.ReadOnly = True
        txtEntroCuiDisdettare.ReadOnly = True
        txtDelibera.ReadOnly = True
        txtRifLegislativo.ReadOnly = True
        txtDurata.ReadOnly = True
        txtDurataRinnovo.ReadOnly = True
        txtDescrcontratto.ReadOnly = True

        cmbDestUso.Enabled = False
        txtDescrDestinazione.ReadOnly = True
        cmbForzoso.Enabled = False


        txtNotificaDisdetta.ReadOnly = True
        cmbMittenteDisdetta.Enabled = False
        txtDataDisdetta0.ReadOnly = True
        ChBolloEsente.Enabled = False

        chkTemporanea.Enabled = False

        chTutore.Enabled = False
    End Function

    Public Function Disabilita()
        txtCodContratto.ReadOnly = True
        cmbNomeUfficiale.Enabled = False
        txtDataDecorrenza.ReadOnly = True
        txtDataConsegna.ReadOnly = True
        txtDataScadenza.ReadOnly = True
        txtRifLegislativo.ReadOnly = True
        txtDescrcontratto.ReadOnly = True
        cmbDestUso.Enabled = False
        txtDescrDestinazione.ReadOnly = True
        ChBolloEsente.Enabled = False

    End Function

    Public Function Abilita()
        If UCase(cmbNomeUfficiale.SelectedItem.Text) = "USI DIVERSI" Then
            cmbNomeUfficiale.Enabled = True
            txtDurata.ReadOnly = False
            txtDurataRinnovo.ReadOnly = False
        End If
        txtCodContratto.ReadOnly = True
        txtRifLegislativo.ReadOnly = True
        txtDataDecorrenza.ReadOnly = False
        txtDataDecAE.ReadOnly = False
        txtDataConsegna.ReadOnly = False
        txtDataScadenza.ReadOnly = False

        'txtDescrcontratto.ReadOnly = True
        'cmbDestUso.Enabled = True
        txtDescrDestinazione.ReadOnly = False
        ChBolloEsente.Enabled = True
        chTutore.Enabled = True
    End Function

    Protected Sub cmbNomeUfficiale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNomeUfficiale.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        Dim cmd As New Oracle.DataAccess.Client.OracleCommand()


        PAR.OracleConn.Open()
        PAR.SettaCommand(PAR)
        cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE WHERE COD='" & PAR.PulisciStrSql(cmbNomeUfficiale.SelectedValue) & "'"

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        If myReader.Read() Then
            txtRifLegislativo.Text = UCase(PAR.IfNull(myReader("RIF_LEGISLATIVO"), ""))
            txtDescrcontratto.Text = UCase(PAR.IfNull(myReader("DESCRIZIONE"), ""))
            txtDurata.Text = UCase(PAR.IfNull(myReader("DURATA"), ""))
            txtDurataRinnovo.Text = UCase(PAR.IfNull(myReader("DURATA2"), ""))
            CType(Page.FindControl("Generale1").FindControl("lblTipologia"), Label).Text = UCase(PAR.IfNull(myReader("DESCRIZIONE"), ""))
            ' Response.Write("<script>document.getElementById('Generale1_lblTipologia').value='" & UCase(PAR.IfNull(myReader("DESCRIZIONE"), "")) & "';</script>")

            If txtDataScadenza.Text <> "" Then
                Response.Write("<script>alert('Attenzione...Verificare che le date di scadenza e seconda scadenza siano corrette!');</script>")
            End If
        End If
        myReader.Close()
        PAR.OracleConn.Close()
        PAR.OracleConn = Nothing




    End Sub

    Protected Sub DataGridNote_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridNote.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#C1C1C1';" _
                                & "document.getElementById('idNota').value='" & e.Item.Cells(0).Text & "';")
            e.Item.Attributes.Add("onDblclick", "ModificaNote();")
        End If
    End Sub


    Protected Sub DataGridNote_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGridNote.SelectedIndexChanged

    End Sub
End Class
