
Partial Class Dom_Dichiara_VSA
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Public Id_Domanda As Long
    Public Id_Dichiarazione As Long


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
	        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        txtCIData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtCSData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtPSData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtPSScade.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtPSRinnovo.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtDataPrRichiesta.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtDataPrRichiesta.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataEvento.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtDataEvento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


        If Not IsPostBack Then
            '**********modifiche campi***********
            Dim CTRL As Control
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                End If
            Next

        End If



    End Sub

    Public Sub DisattivaTutto()
        cmbTipoRichiesta.Enabled = False

        cmbFattaAU.Enabled = False
        'cmbFattaERP.Enabled = False
        'cmbFaiERP.Enabled = False
        'txtPgERP.Enabled = False
        cmbPresentaD.Enabled = False
        txtCIData.Enabled = False
        txtCINum.Enabled = False
        txtCIRilascio.Enabled = False
        txtCSData.Enabled = False
        txtCSNum.Enabled = False
        txtPSData.Enabled = False
        txtPSNum.Enabled = False
        txtPSRinnovo.Enabled = False
        txtPSScade.Enabled = False
        txtDataPrRichiesta.Enabled = False
        cmbTipoRichiesta.Enabled = False
        txtCodContrattoScambio.Enabled = False
    End Sub



    Protected Sub cmbTipoRichiesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoRichiesta.SelectedIndexChanged
        Try
            Dim item As ListItem

            item = cmbTipoRichiesta.SelectedItem
            Select Case item.Value
                Case "1" 'subentro
                    CEM.Visible = False
                    cmbPresentaD.Visible = True
                    par.RiempiDList(Me, par.OracleConn, "cmbPresentaD", "SELECT cod,descrizione FROM t_causali_domanda_vsa WHERE id_motivo=1 ORDER BY descrizione ASC", "descrizione", "cod")

                    'CType(Me.Page.FindControl("cmbAccolta"), DropDownList).Visible = True
                    'CType(Me.Page.FindControl("Label13"), Label).Visible = True
                    lblCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Text = ""

                Case "0" '"VOLTURA"
                    CEM.Visible = False
                    cmbPresentaD.Visible = True
                    par.RiempiDList(Me, par.OracleConn, "cmbPresentaD", "SELECT cod,descrizione FROM t_causali_domanda_vsa WHERE id_motivo=" & par.PulisciStrSql(item.Value) & " ORDER BY descrizione ASC", "descrizione", "cod")

                    'CType(Me.Page.FindControl("cmbAccolta"), DropDownList).Visible = True
                    'CType(Me.Page.FindControl("Label13"), Label).Visible = True
                    lblCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Text = ""

                Case "2" '"AMPLIAMENTO"
                    CEM.Visible = False
                    cmbPresentaD.Visible = True
                    par.RiempiDList(Me, par.OracleConn, "cmbPresentaD", "SELECT cod,descrizione FROM t_causali_domanda_vsa WHERE id_motivo=" & par.PulisciStrSql(item.Value) & " ORDER BY descrizione ASC", "descrizione", "cod")

                    'CType(Me.Page.FindControl("cmbAccolta"), DropDownList).Visible = True
                    'CType(Me.Page.FindControl("Label13"), Label).Visible = True
                    lblCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Text = ""

                Case "4" '"CAMBIO ART.22 C.10 RR 1/2004"
                    CEM.Visible = True
                    cmbPresentaD.Style.Value = "visibility : 'hidden'"
                    Label16.Visible = False
                    'CType(Me.Page.FindControl("cmbAccolta"), DropDownList).Visible = False
                    'CType(Me.Page.FindControl("Label13"), Label).Visible = False

                    CEM.NavigateUrl = "VSA/MotivazioniEmergenzaNEW.aspx?ID=" & DOMANDA.Value & "&DI=" & DICHIARAZIONE.Value & "&IDE=" & cu.Value & "&CONN=" & CType(Me.Page, Object).lIdConnessDOMANDA
                    CEM.Target = "_blank"
                    lblCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Text = ""

                Case "3" '"RIDUZIONE CANONE"
                    CEM.Visible = False
                    'cmbPresentaD.Style.Value = "visibility : 'hidden'"
                    'Label16.Visible = False
                    cmbPresentaD.Visible = True
                    par.RiempiDList(Me, par.OracleConn, "cmbPresentaD", "SELECT cod,descrizione FROM t_causali_domanda_vsa WHERE id_motivo=" & par.PulisciStrSql(item.Value) & " ORDER BY descrizione ASC", "descrizione", "cod")
                    lblCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Text = ""

                Case "5" 'cambi diretti
                    CEM.Visible = False
                    cmbPresentaD.Style.Value = "visibility : 'hidden'"
                    Label16.Visible = False
                    'CType(Me.Page.FindControl("cmbAccolta"), DropDownList).Visible = True
                    'CType(Me.Page.FindControl("Label13"), Label).Visible = True

                    lblCodContrattoScambio.Visible = True
                    txtCodContrattoScambio.Visible = True
                Case "6" 'subentro FF.OO.
                    CEM.Visible = False
                    cmbPresentaD.Visible = True
                    par.RiempiDList(Me, par.OracleConn, "cmbPresentaD", "SELECT cod,descrizione FROM t_causali_domanda_vsa WHERE id_motivo=6 ORDER BY descrizione ASC", "descrizione", "cod")
                    lblCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Visible = False
                    txtCodContrattoScambio.Text = ""

                Case "10" 'cambio contrattuale
                    CEM.Visible = False
                    cmbPresentaD.Style.Value = "visibility : 'hidden'"
                    Label16.Visible = False
            End Select




        Catch ex As Exception

        End Try
    End Sub


End Class
