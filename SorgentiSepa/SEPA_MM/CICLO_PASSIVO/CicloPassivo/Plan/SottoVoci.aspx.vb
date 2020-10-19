
Partial Class Contabilita_CicloPassivo_Plan_SottoVoci
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            idPianoF.Value = Request.QueryString("IDP")
            idVoce.Value = Request.QueryString("IDV")
            CaricaIva()

            CaricaStato()
            CaricaVoci()


        End If

        If txtImporto1.Visible = True Then
            ChCompleto1.AutoPostBack = False
            ChCompleto1.CausesValidation = False
        End If
        If txtImporto2.Visible = True Then
            ChCompleto2.AutoPostBack = False
            ChCompleto2.CausesValidation = False
        End If

        If txtImporto3.Visible = True Then
            ChCompleto3.AutoPostBack = False
            ChCompleto3.CausesValidation = False
        End If
        If txtImporto4.Visible = True Then
            ChCompleto4.AutoPostBack = False
            ChCompleto4.CausesValidation = False
        End If
        If txtImporto5.Visible = True Then
            ChCompleto5.AutoPostBack = False
            ChCompleto5.CausesValidation = False
        End If
        If txtImporto6.Visible = True Then
            ChCompleto6.AutoPostBack = False
            ChCompleto6.CausesValidation = False
        End If
        If txtImporto7.Visible = True Then
            ChCompleto7.AutoPostBack = False
            ChCompleto7.CausesValidation = False
        End If
        If txtImporto8.Visible = True Then
            ChCompleto8.AutoPostBack = False
            ChCompleto8.CausesValidation = False
        End If
        If txtImporto9.Visible = True Then
            ChCompleto9.AutoPostBack = False
            ChCompleto9.CausesValidation = False
        End If
        If txtImporto10.Visible = True Then
            ChCompleto10.AutoPostBack = False
            ChCompleto10.CausesValidation = False
        End If
        If txtImporto11.Visible = True Then
            ChCompleto11.AutoPostBack = False
            ChCompleto11.CausesValidation = False
        End If
        If txtImporto12.Visible = True Then
            ChCompleto12.AutoPostBack = False
            ChCompleto12.CausesValidation = False
        End If

        Dim CTRL As Control
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('modificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('modificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('modificato').value='1';")
            End If
        Next

        txtImporto1.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto2.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto3.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto4.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto5.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto6.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto7.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto8.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto9.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto10.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto11.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto12.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

        txtImporto1.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto1').innerHTML='0,00';document.getElementById('cmbIva1').value='0';document.getElementById('lblImporto1').innerHTML= (parseFloat(document.getElementById('txtImporto1').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto1').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva1').value))/100).toFixed(2).replace('.',',');")
        txtImporto2.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto2').innerHTML='0,00';document.getElementById('cmbIva2').value='0';document.getElementById('lblImporto2').innerHTML= (parseFloat(document.getElementById('txtImporto2').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto2').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva2').value))/100).toFixed(2).replace('.',',');")
        txtImporto3.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto3').innerHTML='0,00';document.getElementById('cmbIva3').value='0';document.getElementById('lblImporto3').innerHTML= (parseFloat(document.getElementById('txtImporto3').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto3').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva3').value))/100).toFixed(2).replace('.',',');")
        txtImporto4.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto4').innerHTML='0,00';document.getElementById('cmbIva4').value='0';document.getElementById('lblImporto4').innerHTML= (parseFloat(document.getElementById('txtImporto4').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto4').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva4').value))/100).toFixed(2).replace('.',',');")
        txtImporto5.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto5').innerHTML='0,00';document.getElementById('cmbIva5').value='0';document.getElementById('lblImporto5').innerHTML= (parseFloat(document.getElementById('txtImporto5').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto5').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva5').value))/100).toFixed(2).replace('.',',');")
        txtImporto6.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto6').innerHTML='0,00';document.getElementById('cmbIva6').value='0';document.getElementById('lblImporto6').innerHTML= (parseFloat(document.getElementById('txtImporto6').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto6').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva6').value))/100).toFixed(2).replace('.',',');")
        txtImporto7.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto7').innerHTML='0,00';document.getElementById('cmbIva7').value='0';document.getElementById('lblImporto7').innerHTML= (parseFloat(document.getElementById('txtImporto7').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto7').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva7').value))/100).toFixed(2).replace('.',',');")
        txtImporto8.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto8').innerHTML='0,00';document.getElementById('cmbIva8').value='0';document.getElementById('lblImporto8').innerHTML= (parseFloat(document.getElementById('txtImporto8').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto8').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva8').value))/100).toFixed(2).replace('.',',');")
        txtImporto9.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto9').innerHTML='0,00';document.getElementById('cmbIva9').value='0';document.getElementById('lblImporto9').innerHTML= (parseFloat(document.getElementById('txtImporto9').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto9').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva9').value))/100).toFixed(2).replace('.',',');")
        txtImporto10.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto10').innerHTML='0,00';document.getElementById('cmbIva10').value='0';document.getElementById('lblImporto10').innerHTML= (parseFloat(document.getElementById('txtImporto10').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto10').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva10').value))/100).toFixed(2).replace('.',',');")


        cmbIva1.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto1').innerHTML= (parseFloat(document.getElementById('txtImporto1').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto1').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva1').value))/100).toFixed(2).replace('.',',');")
        cmbIva2.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto2').innerHTML= (parseFloat(document.getElementById('txtImporto2').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto2').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva2').value))/100).toFixed(2).replace('.',',');")
        cmbIva3.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto3').innerHTML= (parseFloat(document.getElementById('txtImporto3').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto3').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva3').value))/100).toFixed(2).replace('.',',');")
        cmbIva4.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto4').innerHTML= (parseFloat(document.getElementById('txtImporto4').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto4').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva4').value))/100).toFixed(2).replace('.',',');")
        cmbIva5.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto5').innerHTML= (parseFloat(document.getElementById('txtImporto5').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto5').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva5').value))/100).toFixed(2).replace('.',',');")
        cmbIva6.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto6').innerHTML= (parseFloat(document.getElementById('txtImporto6').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto6').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva6').value))/100).toFixed(2).replace('.',',');")
        cmbIva7.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto7').innerHTML= (parseFloat(document.getElementById('txtImporto7').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto7').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva7').value))/100).toFixed(2).replace('.',',');")
        cmbIva8.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto8').innerHTML= (parseFloat(document.getElementById('txtImporto8').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto8').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva8').value))/100).toFixed(2).replace('.',',');")
        cmbIva9.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto9').innerHTML= (parseFloat(document.getElementById('txtImporto9').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto9').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva9').value))/100).toFixed(2).replace('.',',');")
        cmbIva10.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto10').innerHTML= (parseFloat(document.getElementById('txtImporto10').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto10').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva10').value))/100).toFixed(2).replace('.',',');")




    End Sub

    Function CaricaCaselle(ByVal indice As Integer, ByVal DescrizioneVoce As TextBox, ByVal Codice As TextBox, ByVal Aggiungi As ImageButton, ByVal completo As CheckBox, ByVal Avviso As System.Web.UI.WebControls.Image, ByVal Importo As TextBox, ByVal LImporto As Label, ByVal Iva As DropDownList, ByVal SottoVoci As System.Web.UI.WebControls.Image)
        Dim myReader51 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        par.cmd.CommandText = "select pf_voci.*,pf_voci_STRUTTURA.completo from siscom_mi.pf_voci,siscom_mi.pf_voci_struttura where pf_voci_struttura.id_struttura=" & Session.Item("id_struttura") & " and pf_voci.id=pf_voci_struttura.id_voce and id_voce_madre=" & idVoce.Value & " and indice=" & indice
        myReader51 = par.cmd.ExecuteReader()
        If myReader51.Read Then
            Codice.Text = par.IfNull(myReader51("codice"), "")
            Codice.Attributes.Add("ID_CAPITOLO", par.IfNull(myReader51("id_CAPITOLO"), "NULL"))
            DescrizioneVoce.Text = par.IfNull(myReader51("descrizione"), "")
            DescrizioneVoce.Attributes.Add("ID", par.IfNull(myReader51("id"), -1))
            Aggiungi.Attributes.Add("onclick", "javascript:CaricaFinestra(" & par.IfNull(myReader51("id"), -1) & ")")
            If par.IfNull(myReader51("completo"), "0") = "0" Then
                completo.Checked = False
            Else
                completo.Checked = True
            End If


            par.cmd.CommandText = "select * from siscom_mi.pf_voci_struttura,siscom_mi.pf_voci where pf_voci_struttura.id_voce=pf_voci.id and  id_voce=" & par.IfNull(myReader51("id"), -1) & " and id_struttura=" & Session.Item("id_struttura") & " AND (PF_VOCI.ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ") OR PF_VOCI.ID_VOCE_MADRE IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & "))"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                If par.IfNull(myReader5("completo_aler"), "0") = "1" And par.IfNull(myReader5("completo"), "0") = "0" And par.IfNull(myReader5("valore_lordo_aler"), "0") <> par.IfNull(myReader5("valore_lordo"), "0") Then
                    Avviso.Visible = True
                    Avviso.Attributes.Add("IMPORTO", par.IfNull(myReader5("valore_lordo_aler"), "0"))
                    Avviso.ToolTip = "Importo approvato dal Gestore: " & Format(par.IfNull(myReader5("valore_lordo_aler"), "0"), "##,##0.00")
                End If
            End If
            myReader5.Close()

            par.cmd.CommandText = "select pf_voci.*,pf_voci_STRUTTURA.completo from siscom_mi.pf_voci,siscom_mi.pf_voci_struttura where pf_voci_struttura.id_struttura=" & Session.Item("id_struttura") & " and pf_voci.id=pf_voci_struttura.id_voce and id_voce_madre= " & myReader51("id")
            myReader5 = par.cmd.ExecuteReader()
            If myReader5.HasRows = True Then
                Importo.ReadOnly = True
                SottoVoci.Visible = True
                SottoVoci.Attributes.Add("onclick", "javascript:ApriFinestraSottoVoci(" & par.IfNull(myReader51("id"), -1) & "," & cmbIva1.SelectedItem.Value & ")")
                Iva.Visible = False
                Importo.Attributes.Remove("onBlur")
                Importo.Attributes.Remove("onchange")
                Iva.Attributes.Remove("onchange")
            Else
                SottoVoci.Visible = False
                Iva.Visible = True
               
            End If
            myReader5.Close()

            If Codice.Text = "2.03.01" Then
                DescrizioneVoce.ReadOnly = True
                Importo.ReadOnly = True
            End If

        Else
            DescrizioneVoce.Attributes.Add("ID", "-1")
            Codice.Attributes.Add("ID_CAPITOLO", "NULL")
            Aggiungi.Attributes.Add("onclick", "javascript:CaricaFinestra(-1)")
        End If
        myReader51.Close()

        Codice.ReadOnly = True
        DescrizioneVoce.ReadOnly = True


        If DescrizioneVoce.Text = "" Then
            DescrizioneVoce.Visible = False
            Codice.Visible = False
            completo.Visible = False
            Importo.Visible = False
            LImporto.Visible = False
            Iva.Visible = False
        End If
    End Function


    Function CaricaVoci()
        Try
            Dim servizi As Boolean = True

            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select * from siscom_mi.pf_voci where id=" & idVoce.Value
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblVoce.Text = UCase(par.IfNull(myReader5("codice"), "") & "-" & par.IfNull(myReader5("descrizione"), ""))
                capitolo.Value = par.IfNull(myReader5("id_capitolo"), "NULL")



                lblVoce3.Visible = True
                lblVoce2.Text = "Importo Netto"
                ImgServizio1.Visible = False
                ImgServizio2.Visible = False
                ImgServizio3.Visible = False
                ImgServizio4.Visible = False
                ImgServizio5.Visible = False
                ImgServizio6.Visible = False
                ImgServizio7.Visible = False
                ImgServizio8.Visible = False
                ImgServizio9.Visible = False
                ImgServizio10.Visible = False
                ImgServizio11.Visible = False
                ImgServizio12.Visible = False
                txtImporto1.Visible = True
                txtImporto2.Visible = True
                txtImporto3.Visible = True
                txtImporto4.Visible = True
                txtImporto5.Visible = True
                txtImporto6.Visible = True
                txtImporto7.Visible = True
                txtImporto8.Visible = True
                txtImporto9.Visible = True
                txtImporto10.Visible = True
                txtImporto11.Visible = True
                txtImporto12.Visible = True
                cmbIva1.Visible = True
                cmbIva2.Visible = True
                cmbIva3.Visible = True
                cmbIva4.Visible = True
                cmbIva5.Visible = True
                cmbIva6.Visible = True
                cmbIva7.Visible = True
                cmbIva8.Visible = True
                cmbIva9.Visible = True
                cmbIva10.Visible = True
                cmbIva11.Visible = True
                cmbIva12.Visible = True

                Sotto1.Visible = False
                Sotto2.Visible = False
                Sotto3.Visible = False
                Sotto4.Visible = False
                Sotto5.Visible = False
                Sotto6.Visible = False
                Sotto7.Visible = False
                Sotto8.Visible = False
                Sotto9.Visible = False
                Sotto10.Visible = False
                Sotto11.Visible = False
                Sotto12.Visible = False

                CaricaCaselle(1, txtVoce1, txtCod1, ImgServizio1, ChCompleto1, ImgAvviso1, txtImporto1, lblImporto1, cmbIva1, Sotto1)
                CaricaCaselle(2, txtVoce2, txtCod2, ImgServizio2, ChCompleto2, ImgAvviso2, txtImporto2, lblImporto2, cmbIva2, Sotto2)
                CaricaCaselle(3, txtVoce3, txtCod3, ImgServizio3, ChCompleto3, ImgAvviso3, txtImporto3, lblImporto3, cmbIva3, Sotto3)
                CaricaCaselle(4, txtVoce4, txtCod4, ImgServizio4, ChCompleto4, ImgAvviso4, txtImporto4, lblImporto4, cmbIva4, Sotto4)
                CaricaCaselle(5, txtVoce5, txtCod5, ImgServizio5, ChCompleto5, ImgAvviso5, txtImporto5, lblImporto5, cmbIva5, Sotto5)
                CaricaCaselle(6, txtVoce6, txtCod6, ImgServizio6, ChCompleto6, ImgAvviso6, txtImporto6, lblImporto6, cmbIva6, Sotto6)
                CaricaCaselle(7, txtVoce7, txtCod7, ImgServizio7, ChCompleto7, ImgAvviso7, txtImporto7, lblImporto7, cmbIva7, Sotto7)
                CaricaCaselle(8, txtVoce8, txtCod8, ImgServizio8, ChCompleto8, ImgAvviso8, txtImporto8, lblImporto8, cmbIva8, Sotto8)
                CaricaCaselle(9, txtVoce9, txtCod9, ImgServizio9, ChCompleto9, ImgAvviso9, txtImporto9, lblImporto9, cmbIva9, Sotto9)
                CaricaCaselle(10, txtVoce10, txtCod10, ImgServizio10, ChCompleto10, ImgAvviso10, txtImporto10, lblImporto10, cmbIva10, Sotto10)
                CaricaCaselle(11, txtVoce11, txtCod11, ImgServizio11, ChCompleto11, ImgAvviso11, txtImporto11, lblImporto11, cmbIva11, Sotto11)
                CaricaCaselle(12, txtVoce12, txtCod12, ImgServizio12, ChCompleto12, ImgAvviso12, txtImporto12, lblImporto12, cmbIva12, Sotto12)



                '    If par.IfNull(myReader5("indice"), "0") >= 22 And par.IfNull(myReader5("indice"), "0") <= 40 Then
                '        servizi = True
                '        txtImporto1.Visible = True
                '        txtImporto2.Visible = True
                '        txtImporto3.Visible = True
                '        txtImporto4.Visible = True
                '        txtImporto5.Visible = True
                '        txtImporto6.Visible = True
                '        txtImporto7.Visible = True
                '        txtImporto8.Visible = True
                '        txtImporto9.Visible = True
                '        txtImporto10.Visible = True
                '        txtImporto11.Visible = True
                '        txtImporto12.Visible = True

                '        cmbIva1.Visible = True
                '        cmbIva2.Visible = True
                '        cmbIva3.Visible = True
                '        cmbIva4.Visible = True
                '        cmbIva5.Visible = True
                '        cmbIva6.Visible = True
                '        cmbIva7.Visible = True
                '        cmbIva8.Visible = True
                '        cmbIva9.Visible = True
                '        cmbIva10.Visible = True
                '        cmbIva11.Visible = True
                '        cmbIva12.Visible = True

                '        If par.IfNull(myReader5("indice"), "0") = 22 Then
                '            ImgServizio1.Visible = True
                '            ImgServizio2.Visible = True
                '            ImgServizio3.Visible = True

                '            txtImporto1.Visible = False
                '            txtImporto2.Visible = False
                '            txtImporto3.Visible = False

                '            cmbIva1.Visible = False
                '            cmbIva2.Visible = False
                '            cmbIva3.Visible = False

                '        End If

                '        If par.IfNull(myReader5("indice"), "0") = 23 Then

                '            txtImporto1.Enabled = False
                '            txtImporto1.ToolTip = "Importo determinato in base al valore inserito al punto 2.2.1"

                '        End If


                '        If par.IfNull(myReader5("indice"), "0") = 24 Then

                '            ImgServizio1.Visible = True
                '            txtImporto1.Visible = False
                '            cmbIva1.Visible = False

                '            ImgServizio2.Visible = True
                '            txtImporto2.Visible = False
                '            cmbIva2.Visible = False

                '            ImgServizio3.Visible = True
                '            txtImporto3.Visible = False
                '            cmbIva3.Visible = False

                '            ImgServizio4.Visible = True
                '            txtImporto4.Visible = False
                '            cmbIva4.Visible = False

                '        End If
                '    Else
                '        servizi = False
                '        lblVoce3.Visible = True
                '        lblVoce2.Text = "Importo Netto"
                '        ImgServizio1.Visible = False
                '        ImgServizio2.Visible = False
                '        ImgServizio3.Visible = False
                '        ImgServizio4.Visible = False
                '        ImgServizio5.Visible = False
                '        ImgServizio6.Visible = False
                '        ImgServizio7.Visible = False
                '        ImgServizio8.Visible = False
                '        ImgServizio9.Visible = False
                '        ImgServizio10.Visible = False
                '        ImgServizio11.Visible = False
                '        ImgServizio12.Visible = False
                '        txtImporto1.Visible = True
                '        txtImporto2.Visible = True
                '        txtImporto3.Visible = True
                '        txtImporto4.Visible = True
                '        txtImporto5.Visible = True
                '        txtImporto6.Visible = True
                '        txtImporto7.Visible = True
                '        txtImporto8.Visible = True
                '        txtImporto9.Visible = True
                '        txtImporto10.Visible = True
                '        txtImporto11.Visible = True
                '        txtImporto12.Visible = True

                '        cmbIva1.Visible = True
                '        cmbIva2.Visible = True
                '        cmbIva3.Visible = True
                '        cmbIva4.Visible = True
                '        cmbIva5.Visible = True
                '        cmbIva6.Visible = True
                '        cmbIva7.Visible = True
                '        cmbIva8.Visible = True
                '        cmbIva9.Visible = True
                '        cmbIva10.Visible = True
                '        cmbIva11.Visible = True
                '        cmbIva12.Visible = True


                '    End If


                '    If par.IfNull(myReader5("indice"), "0") = 2 Then
                '        txtVoce1.ReadOnly = True
                '        txtCod1.ReadOnly = True

                '        txtVoce2.ReadOnly = True
                '        txtCod2.ReadOnly = True

                '        txtVoce3.ReadOnly = True
                '        txtCod3.ReadOnly = True

                '        txtVoce4.ReadOnly = True
                '        txtCod4.ReadOnly = True

                '    End If

                '    If par.IfNull(myReader5("indice"), "0") = 3 Then
                '        txtVoce1.ReadOnly = True
                '        txtCod1.ReadOnly = True

                '        txtVoce2.ReadOnly = True
                '        txtCod2.ReadOnly = True
                '    End If

                '    If par.IfNull(myReader5("indice"), "0") = 22 Then
                '        txtVoce1.ReadOnly = True
                '        txtCod1.ReadOnly = True

                '        txtVoce2.ReadOnly = True
                '        txtCod2.ReadOnly = True

                '        txtVoce3.ReadOnly = True
                '        txtCod3.ReadOnly = True

                '        txtVoce4.ReadOnly = True
                '        txtCod4.ReadOnly = True

                '        txtVoce5.ReadOnly = True
                '        txtCod5.ReadOnly = True

                '        txtVoce6.ReadOnly = True
                '        txtCod6.ReadOnly = True
                '    End If

                '    If par.IfNull(myReader5("indice"), "0") = 23 Then
                '        txtVoce1.ReadOnly = True
                '        txtCod1.ReadOnly = True

                '        txtVoce2.ReadOnly = True
                '        txtCod2.ReadOnly = True
                '    End If

                '    If par.IfNull(myReader5("indice"), "0") = 24 Then
                '        txtVoce1.ReadOnly = True
                '        txtCod1.ReadOnly = True

                '        txtVoce2.ReadOnly = True
                '        txtCod2.ReadOnly = True

                '        txtVoce3.ReadOnly = True
                '        txtCod3.ReadOnly = True

                '        txtVoce4.ReadOnly = True
                '        txtCod4.ReadOnly = True

                '    End If


                If par.IfNull(myReader5("CODICE"), "0") = "2.02" Then

                    lblVoce3.Visible = False
                    lblVoce2.Text = "Servizi"
                    lblIva.Visible = False

                    ImgServizio1.Visible = True
                    ImgServizio2.Visible = True
                    ImgServizio3.Visible = True

                    txtImporto1.Visible = False
                    txtImporto2.Visible = False
                    txtImporto3.Visible = False

                    cmbIva1.Visible = False
                    cmbIva2.Visible = False
                    cmbIva3.Visible = False
                End If

                If par.IfNull(myReader5("CODICE"), "0") = "2.04" Then
                    lblVoce3.Visible = False
                    lblVoce2.Text = "Servizi"
                    lblIva.Visible = False
                    ImgServizio1.Visible = True
                    ImgServizio2.Visible = True
                    ImgServizio3.Visible = True
                    ImgServizio4.Visible = True

                    txtImporto1.Visible = False
                    txtImporto2.Visible = False
                    txtImporto3.Visible = False
                    txtImporto4.Visible = False

                    cmbIva1.Visible = False
                    cmbIva2.Visible = False
                    cmbIva3.Visible = False
                    cmbIva4.Visible = False
                End If


            End If
            myReader5.Close()

           





            



            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            'If servizi = True Then
            CaricaSomme()
            'Else
            '    CaricaSommeNoServizi()
            'End If


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function


    Function CaricaSomme()

        If txtImporto1.Visible = False Then
            CaricaSomma(DirectCast(txtVoce1, TextBox).Attributes("ID").ToUpper.ToString, lblImporto1)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce1, TextBox).Attributes("ID").ToUpper.ToString, txtImporto1, cmbIva1, lblImporto1)
        End If

        If txtImporto2.Visible = False Then
            CaricaSomma(DirectCast(txtVoce2, TextBox).Attributes("ID").ToUpper.ToString, lblImporto2)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce2, TextBox).Attributes("ID").ToUpper.ToString, txtImporto2, cmbIva2, lblImporto2)
        End If

        If txtImporto3.Visible = False Then
            CaricaSomma(DirectCast(txtVoce3, TextBox).Attributes("ID").ToUpper.ToString, lblImporto3)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce3, TextBox).Attributes("ID").ToUpper.ToString, txtImporto3, cmbIva3, lblImporto3)
        End If

        If txtImporto4.Visible = False Then
            CaricaSomma(DirectCast(txtVoce4, TextBox).Attributes("ID").ToUpper.ToString, lblImporto4)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce4, TextBox).Attributes("ID").ToUpper.ToString, txtImporto4, cmbIva4, lblImporto4)
        End If

        If txtImporto5.Visible = False Then
            CaricaSomma(DirectCast(txtVoce5, TextBox).Attributes("ID").ToUpper.ToString, lblImporto5)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce5, TextBox).Attributes("ID").ToUpper.ToString, txtImporto5, cmbIva5, lblImporto5)
        End If

        If txtImporto6.Visible = False Then
            CaricaSomma(DirectCast(txtVoce6, TextBox).Attributes("ID").ToUpper.ToString, lblImporto6)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce6, TextBox).Attributes("ID").ToUpper.ToString, txtImporto6, cmbIva6, lblImporto6)
        End If

        If txtImporto7.Visible = False Then
            CaricaSomma(DirectCast(txtVoce7, TextBox).Attributes("ID").ToUpper.ToString, lblImporto7)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce7, TextBox).Attributes("ID").ToUpper.ToString, txtImporto7, cmbIva7, lblImporto7)
        End If

        If txtImporto8.Visible = False Then
            CaricaSomma(DirectCast(txtVoce8, TextBox).Attributes("ID").ToUpper.ToString, lblImporto8)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce8, TextBox).Attributes("ID").ToUpper.ToString, txtImporto8, cmbIva8, lblImporto8)
        End If

        If txtImporto9.Visible = False Then
            CaricaSomma(DirectCast(txtVoce9, TextBox).Attributes("ID").ToUpper.ToString, lblImporto9)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce9, TextBox).Attributes("ID").ToUpper.ToString, txtImporto9, cmbIva9, lblImporto9)
        End If

        If txtImporto10.Visible = False Then
            CaricaSomma(DirectCast(txtVoce10, TextBox).Attributes("ID").ToUpper.ToString, lblImporto10)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce10, TextBox).Attributes("ID").ToUpper.ToString, txtImporto10, cmbIva10, lblImporto10)
        End If

        If txtImporto11.Visible = False Then
            CaricaSomma(DirectCast(txtVoce11, TextBox).Attributes("ID").ToUpper.ToString, lblImporto11)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce11, TextBox).Attributes("ID").ToUpper.ToString, txtImporto11, cmbIva11, lblImporto11)
        End If

        If txtImporto12.Visible = False Then
            CaricaSomma(DirectCast(txtVoce12, TextBox).Attributes("ID").ToUpper.ToString, lblImporto12)
        Else
            CaricaSommaNoServizi(DirectCast(txtVoce12, TextBox).Attributes("ID").ToUpper.ToString, txtImporto12, cmbIva12, lblImporto12)
        End If




    End Function

    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
            End If
            myReader5.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function


    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function


    Function VerificaAvviso(ByVal Avviso As System.Web.UI.WebControls.Image, ByVal chCompleto As CheckBox, ByVal importo As Label, ByVal Codice As TextBox)
        If Avviso.Visible = True And chCompleto.Checked = True Then
            If CDbl(DirectCast(Avviso, Image).Attributes("IMPORTO").ToString()) <> CDbl(importo.Text) Then
                Response.Write("<script>alert('ATTENZIONE...\nLa voce con codice " & Codice.Text & " non può essere definita completa perchè non è stata approvata dal Gestore! L\'importo lordo deve essere pari a euro : " & Format(CDbl(DirectCast(Avviso, Image).Attributes("IMPORTO").ToString()), "##,##0.00") & "');</script>")
                chCompleto.Checked = False
            End If
        End If
    End Function

    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Try

            VerificaAvviso(ImgAvviso1, ChCompleto1, lblImporto1, txtCod1)
            VerificaAvviso(ImgAvviso2, ChCompleto2, lblImporto2, txtCod2)
            VerificaAvviso(ImgAvviso3, ChCompleto3, lblImporto3, txtCod3)
            VerificaAvviso(ImgAvviso4, ChCompleto4, lblImporto4, txtCod4)
            VerificaAvviso(ImgAvviso5, ChCompleto5, lblImporto5, txtCod5)
            VerificaAvviso(ImgAvviso6, ChCompleto6, lblImporto6, txtCod6)
            VerificaAvviso(ImgAvviso7, ChCompleto7, lblImporto7, txtCod7)
            VerificaAvviso(ImgAvviso8, ChCompleto8, lblImporto8, txtCod8)
            VerificaAvviso(ImgAvviso9, ChCompleto9, lblImporto9, txtCod9)
            VerificaAvviso(ImgAvviso10, ChCompleto10, lblImporto10, txtCod10)
            VerificaAvviso(ImgAvviso11, ChCompleto11, lblImporto11, txtCod11)
            VerificaAvviso(ImgAvviso12, ChCompleto12, lblImporto12, txtCod12)

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim tot As Double = 0
            Dim TotLordo As Double = 0

            If txtImporto1.Visible = False Then
                Salva(txtVoce1, 1, txtCod1, ChCompleto1, lblImporto1, ImgServizio1, ImgAvviso1)
                TotLordo = TotLordo + lblImporto1.Text
            Else
                'If cmbIva1.Visible = True Then
                SalvaNoServizi(txtVoce1, 1, txtCod1, ChCompleto1, txtImporto1, ImgServizio1, ImgAvviso1, cmbIva1, lblImporto1)
                tot = tot + txtImporto1.Text
                If cmbIva1.Visible = True Then
                    TotLordo = TotLordo + txtImporto1.Text + (txtImporto1.Text * (cmbIva1.SelectedItem.Value / 100))
                Else
                    TotLordo = TotLordo + lblImporto1.Text '+ txtImporto1.Text 
                End If
            End If

                If txtImporto2.Visible = False Then
                    Salva(txtVoce2, 2, txtCod2, ChCompleto2, lblImporto2, ImgServizio2, ImgAvviso2)
                    TotLordo = TotLordo + lblImporto2.Text

                Else
                    'If cmbIva2.Visible = True Then
                    SalvaNoServizi(txtVoce2, 2, txtCod2, ChCompleto2, txtImporto2, ImgServizio2, ImgAvviso2, cmbIva2, lblImporto2)
                tot = tot + txtImporto2.Text
                If cmbIva2.Visible = True Then
                    TotLordo = TotLordo + txtImporto2.Text + (txtImporto2.Text * (cmbIva2.SelectedItem.Value / 100))
                Else
                    TotLordo = TotLordo + lblImporto2.Text '+ txtImporto2.Text 
                End If
                End If

                If txtImporto3.Visible = False Then
                    Salva(txtVoce3, 3, txtCod3, ChCompleto3, lblImporto3, ImgServizio3, ImgAvviso3)
                    TotLordo = TotLordo + lblImporto3.Text

                Else
                ' 
                    SalvaNoServizi(txtVoce3, 3, txtCod3, ChCompleto3, txtImporto3, ImgServizio3, ImgAvviso3, cmbIva3, lblImporto3)
                tot = tot + txtImporto3.Text
                If cmbIva3.Visible = True Then
                    TotLordo = TotLordo + txtImporto3.Text + (txtImporto3.Text * (cmbIva3.SelectedItem.Value / 100))
                Else
                    TotLordo = TotLordo + lblImporto3.Text ' + txtImporto3.Text 
                End If
                End If

                If txtImporto4.Visible = False Then
                    Salva(txtVoce4, 4, txtCod4, ChCompleto4, lblImporto4, ImgServizio4, ImgAvviso4)
                    TotLordo = TotLordo + lblImporto4.Text

                Else
                '
                    SalvaNoServizi(txtVoce4, 4, txtCod4, ChCompleto4, txtImporto4, ImgServizio4, ImgAvviso4, cmbIva4, lblImporto4)
                tot = tot + txtImporto4.Text
                If cmbIva4.Visible = True Then
                    TotLordo = TotLordo + txtImporto4.Text + (txtImporto4.Text * (cmbIva4.SelectedItem.Value / 100))
                Else
                    TotLordo = TotLordo + lblImporto4.Text '+ txtImporto4.Text 
                End If
                End If

                If txtImporto5.Visible = False Then
                    Salva(txtVoce5, 5, txtCod5, ChCompleto5, lblImporto5, ImgServizio5, ImgAvviso5)
                    TotLordo = TotLordo + lblImporto5.Text

                Else
                ' 
                    SalvaNoServizi(txtVoce5, 5, txtCod5, ChCompleto5, txtImporto5, ImgServizio5, ImgAvviso5, cmbIva5, lblImporto5)
                tot = tot + txtImporto5.Text
                If cmbIva5.Visible = True Then
                    TotLordo = TotLordo + txtImporto5.Text + (txtImporto5.Text * (cmbIva5.SelectedItem.Value / 100))
                Else
                    TotLordo = TotLordo + lblImporto5.Text ' + txtImporto5.Text 
                End If
                End If

                If txtImporto6.Visible = False Then
                    Salva(txtVoce6, 6, txtCod6, ChCompleto6, lblImporto6, ImgServizio6, ImgAvviso6)
                    TotLordo = TotLordo + lblImporto6.Text
                Else
                ' 
                    SalvaNoServizi(txtVoce6, 6, txtCod6, ChCompleto6, txtImporto6, ImgServizio6, ImgAvviso6, cmbIva6, lblImporto6)
                tot = tot + txtImporto6.Text
                If cmbIva6.Visible = True Then
                    TotLordo = TotLordo + txtImporto6.Text + (txtImporto6.Text * (cmbIva6.SelectedItem.Value / 100))
                Else
                    TotLordo = TotLordo + lblImporto6.Text '+ txtImporto6.Text 
                End If
                End If

                If txtImporto7.Visible = False Then
                    Salva(txtVoce7, 7, txtCod7, ChCompleto7, lblImporto7, ImgServizio7, ImgAvviso7)
                    TotLordo = TotLordo + lblImporto7.Text

                Else
                '
                    SalvaNoServizi(txtVoce7, 7, txtCod7, ChCompleto7, txtImporto7, ImgServizio7, ImgAvviso7, cmbIva7, lblImporto7)
                tot = tot + txtImporto7.Text
                If cmbIva7.Visible = True Then
                    TotLordo = TotLordo + txtImporto7.Text + (txtImporto7.Text * (cmbIva7.SelectedItem.Value / 100))
                Else
                    TotLordo = TotLordo + lblImporto7.Text '+ txtImporto7.Text 
                End If
                End If

                If txtImporto8.Visible = False Then
                    Salva(txtVoce8, 8, txtCod8, ChCompleto8, lblImporto8, ImgServizio8, ImgAvviso8)
                    TotLordo = TotLordo + lblImporto8.Text
                Else
                    SalvaNoServizi(txtVoce8, 8, txtCod8, ChCompleto8, txtImporto8, ImgServizio8, ImgAvviso8, cmbIva8, lblImporto8)
                tot = tot + txtImporto8.Text
                If cmbIva8.Visible = True Then
                    TotLordo = TotLordo + txtImporto8.Text + (txtImporto8.Text * (cmbIva8.SelectedItem.Value / 100))
                Else
                    TotLordo = TotLordo + lblImporto8.Text '+ txtImporto8.Text 
                End If
                End If

                If txtImporto9.Visible = False Then
                    Salva(txtVoce9, 9, txtCod9, ChCompleto9, lblImporto9, ImgServizio9, ImgAvviso9)
                    TotLordo = TotLordo + lblImporto9.Text

                Else
                ' 
                    SalvaNoServizi(txtVoce9, 9, txtCod9, ChCompleto9, txtImporto9, ImgServizio9, ImgAvviso9, cmbIva9, lblImporto9)
                tot = tot + txtImporto9.Text
                If cmbIva9.Visible = True Then
                    TotLordo = TotLordo + txtImporto9.Text + (txtImporto9.Text * (cmbIva9.SelectedItem.Value / 100))
                Else
                    TotLordo = TotLordo + txtImporto9.Text + lblImporto9.Text
                End If
                End If

                If txtImporto10.Visible = False Then
                    Salva(txtVoce10, 10, txtCod10, ChCompleto10, lblImporto10, ImgServizio10, ImgAvviso10)
                    TotLordo = TotLordo + lblImporto10.Text

                Else
                ' 
                    SalvaNoServizi(txtVoce10, 10, txtCod10, ChCompleto10, txtImporto10, ImgServizio10, ImgAvviso10, cmbIva10, lblImporto10)
                tot = tot + txtImporto10.Text
                If cmbIva10.Visible = True Then
                    TotLordo = TotLordo + txtImporto10.Text + (txtImporto10.Text * (cmbIva10.SelectedItem.Value / 100))
                Else
                    TotLordo = TotLordo + txtImporto10.Text + lblImporto10.Text

                End If
                End If

            'If txtImporto11.Visible = False Then
            '    Salva(txtVoce11, 11, txtCod11, ChCompleto11, lblImporto11, ImgServizio11, ImgAvviso11)
            '    TotLordo = TotLordo + lblImporto11.Text

            'Else
            '    SalvaNoServizi(txtVoce11, 11, txtCod11, ChCompleto11, txtImporto11, ImgServizio11, ImgAvviso11, cmbIva11, lblImporto11)
            '    tot = tot + txtImporto11.Text
            '    TotLordo = TotLordo + lblImporto11.Text
            'End If

            'If txtImporto12.Visible = False Then
            '    Salva(txtVoce12, 12, txtCod12, ChCompleto12, lblImporto12, ImgServizio12, ImgAvviso12)
            '    TotLordo = TotLordo + lblImporto12.Text

            'Else
            '    SalvaNoServizi(txtVoce12, 12, txtCod12, ChCompleto12, txtImporto12, ImgServizio12, ImgAvviso12, cmbIva12, lblImporto12)
            '    tot = tot + txtImporto12.Text
            '    TotLordo = TotLordo + lblImporto12.Text
            'End If

            par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET VALORE_LORDO=" & par.VirgoleInPunti(TotLordo) & " WHERE ID_VOCE=" & idVoce.Value & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            par.cmd.ExecuteNonQuery()



                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F02','VOCI IMPORTI " & par.PulisciStrSql(lblVoce.Text) & "'," & Session.Item("ID_STRUTTURA") & ")"
                par.cmd.ExecuteNonQuery()

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione Effettuata!');</script>")
                modificato.Value = "0"
                CaricaSomme()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function Salva(ByVal casella As TextBox, ByVal indice As Integer, ByVal codice As TextBox, ByVal completo As CheckBox, ByVal importo As Label, ByVal aggiungi As ImageButton, ByVal Avviso As System.Web.UI.WebControls.Image)
        If casella.Text = "" Then
            If DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString <> "-1" Then
                par.cmd.CommandText = "delete from siscom_mi.pf_voci_STRUTTURA WHERE ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") & " AND ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI where id_voce_madre=" & idVoce.Value & " and indice=" & indice & ")"
                par.cmd.ExecuteNonQuery()
                casella.Attributes.Add("ID", "-1")
                codice.Attributes.Add("ID_CAPITOLO", "NULL")
            End If
        Else
            If DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString = "-1" Then
                par.cmd.CommandText = "Insert into SISCOM_MI.PF_VOCI (ID, ID_PIANO_FINANZIARIO, CODICE, DESCRIZIONE, ID_VOCE_MADRE, INDICE, ID_CAPITOLO) Values (siscom_mi.seq_pf_voci.nextval, " & idPianoF.Value & ", '" & par.PulisciStrSql(codice.Text) & "', '" & par.PulisciStrSql(casella.Text) & "', " & idVoce.Value & ", " & indice & ", " & capitolo.Value & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "select siscom_mi.seq_pf_voci.currval from dual"
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader5.Read Then

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_STRUTTURA (ID_VOCE,ID_STRUTTURA) VALUES (" & par.IfNull(myReader5(0), -1) & "," & Session.Item("ID_STRUTTURA") & ")"
                    par.cmd.ExecuteNonQuery()

                    casella.Attributes.Add("ID", par.IfNull(myReader5(0), "-1"))
                    codice.Attributes.Add("ID_CAPITOLO", capitolo.Value)
                    aggiungi.Attributes.Add("onclick", "javascript:CaricaFinestra(" & par.IfNull(myReader5(0), -1) & ")")
                End If
                myReader5.Close()
            Else
                par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET VALORE_LORDO=" & par.VirgoleInPunti(importo.Text) & ", COMPLETO='" & Valore01(completo.Checked) & "' WHERE ID_VOCE=" & DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                par.cmd.ExecuteNonQuery()

            End If
        End If
    End Function

    Private Function SalvaNoServizi(ByVal casella As TextBox, ByVal indice As Integer, ByVal codice As TextBox, ByVal completo As CheckBox, ByVal importo As TextBox, ByVal aggiungi As ImageButton, ByVal Avviso As System.Web.UI.WebControls.Image, ByVal Iva As DropDownList, ByVal ImportoLordo As Label)
        If casella.Text = "" Then
            If DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString <> "-1" Then
                par.cmd.CommandText = "delete from siscom_mi.pf_voci_STRUTTURA WHERE ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") & " AND ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI where id_voce_madre=" & idVoce.Value & " and indice=" & indice & ")"
                par.cmd.ExecuteNonQuery()
                casella.Attributes.Add("ID", "-1")
                codice.Attributes.Add("ID_CAPITOLO", "NULL")
            End If
        Else
            If DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString = "-1" Then
                par.cmd.CommandText = "Insert into SISCOM_MI.PF_VOCI (ID, ID_PIANO_FINANZIARIO, CODICE, DESCRIZIONE, ID_VOCE_MADRE, INDICE, ID_CAPITOLO) Values (siscom_mi.seq_pf_voci.nextval, " & idPianoF.Value & ", '" & par.PulisciStrSql(codice.Text) & "', '" & par.PulisciStrSql(casella.Text) & "', " & idVoce.Value & ", " & indice & ", " & capitolo.Value & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "select siscom_mi.seq_pf_voci.currval from dual"
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader5.Read Then

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_STRUTTURA (ID_VOCE,ID_STRUTTURA) VALUES (" & par.IfNull(myReader5(0), -1) & "," & Session.Item("ID_STRUTTURA") & ")"
                    par.cmd.ExecuteNonQuery()

                    casella.Attributes.Add("ID", par.IfNull(myReader5(0), "-1"))
                    codice.Attributes.Add("ID_CAPITOLO", capitolo.Value)
                    aggiungi.Attributes.Add("onclick", "javascript:CaricaFinestra(" & par.IfNull(myReader5(0), -1) & ")")
                End If
                myReader5.Close()
            Else
                If Iva.Visible = True Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET VALORE_LORDO=" & par.VirgoleInPunti(Format(importo.Text + ((importo.Text * Iva.SelectedItem.Value) / 100), "0.00")) & ",  IVA=" & Iva.SelectedItem.Value & ",VALORE_NETTO=" & par.VirgoleInPunti(importo.Text) & ", COMPLETO='" & Valore01(completo.Checked) & "' WHERE ID_VOCE=" & DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                Else
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET VALORE_LORDO=" & par.VirgoleInPunti(ImportoLordo.Text) & ",  IVA=0,VALORE_NETTO=" & par.VirgoleInPunti(importo.Text) & ", COMPLETO='" & Valore01(completo.Checked) & "' WHERE ID_VOCE=" & DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                End If
                par.cmd.ExecuteNonQuery()
            End If
        End If
    End Function

    Function CaricaSommaNoServizi(ByVal indice As String, ByVal casellaNetto As TextBox, ByVal CasellaIva As DropDownList, ByVal CasellaLordo As Label)
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select valore_netto,valore_lordo,iva from siscom_mi.pf_voci_STRUTTURA where id_VOCE=" & indice & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                casellaNetto.Text = Format(CDbl(par.IfNull(myReader5("VALORE_NETTO"), "0")), "0.00")
                CasellaIva.ClearSelection()
                CasellaIva.SelectedValue = par.IfNull(myReader5("iva"), "0")
                CasellaLordo.Text = Format(CDbl(par.IfNull(myReader5("VALORE_LORDO"), "0")), "0.00")
            End If
            myReader5.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function


    Function CaricaSomma(ByVal indice As String, ByVal casella As Label)
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select sum(valore_canone*(1+(iva_canone/100))+valore_consumo*(1+(iva_consumo/100))) from siscom_mi.pf_voci_importo where id_voce=" & indice & " AND ID_LOTTO IN (SELECT LOTTI.ID FROM SISCOM_MI.LOTTI WHERE ID_FILIALE=" & Session.Item("ID_STRUTTURA") & ")"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                casella.Text = Format(CDbl(par.IfNull(myReader5(0), "0")), "0.00")
            End If
            myReader5.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function

    Protected Sub ImgServizio1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio1.Click
        CaricaSomma(DirectCast(txtVoce1, TextBox).Attributes("ID").ToUpper.ToString, lblImporto1)

        If txtCod1.Text = "2.02.01" Then
            Dim v1 As Double = 0
            Dim v2 As Double = 0
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select sum(valore_canone+valore_consumo) from siscom_mi.pf_voci_importo where id_lotto in (select id from siscom_mi.lotti where id_filiale=" & Session.Item("ID_STRUTTURA") & ") and id_voce=" & DirectCast(txtVoce1, TextBox).Attributes("ID").ToUpper.ToString & " AND DESCRIZIONE='01. Servizi di Custodia'"
            Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader6.Read Then
                v1 = (((100 * par.IfNull(myReader6(0), 0)) / 90) * 10) / 100

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE codice='2.03.01' and id_piano_finanziario=" & idPianoF.Value
                Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader7.Read Then
                    par.cmd.CommandText = "update siscom_mi.pf_voci_STRUTTURA set VALORE_NETTO=" & par.VirgoleInPunti(Format(v1, "0.00")) & ",valore_lordo=" & par.VirgoleInPunti(Format(v1, "0.00")) & ",IVA=0 where ID_VOCE=" & myReader7("ID") & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                    par.cmd.ExecuteNonQuery()
                    ' Response.Write("<script>alert('E\' stato automaticamente impostato il valore NETTO per il punto 2.3.1!');</script>")
                End If
                myReader7.Close()

            End If
            myReader6.Close()



            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If
    End Sub

    Protected Sub ImgServizio2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio2.Click
        CaricaSomma(DirectCast(txtVoce2, TextBox).Attributes("ID").ToUpper.ToString, lblImporto2)
    End Sub

    Protected Sub ImgServizio3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio3.Click
        CaricaSomma(DirectCast(txtVoce3, TextBox).Attributes("ID").ToUpper.ToString, lblImporto3)
    End Sub

    Protected Sub ImgServizio4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio4.Click
        CaricaSomma(DirectCast(txtVoce4, TextBox).Attributes("ID").ToUpper.ToString, lblImporto4)
    End Sub

    Protected Sub ImgServizio5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio5.Click
        CaricaSomma(DirectCast(txtVoce5, TextBox).Attributes("ID").ToUpper.ToString, lblImporto5)
    End Sub

    Protected Sub ImgServizio6_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio6.Click
        CaricaSomma(DirectCast(txtVoce6, TextBox).Attributes("ID").ToUpper.ToString, lblImporto6)
    End Sub

    Protected Sub ImgServizio7_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio7.Click
        CaricaSomma(DirectCast(txtVoce7, TextBox).Attributes("ID").ToUpper.ToString, lblImporto7)
    End Sub

    Protected Sub ImgServizio8_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio8.Click
        CaricaSomma(DirectCast(txtVoce8, TextBox).Attributes("ID").ToUpper.ToString, lblImporto8)
    End Sub

    Protected Sub ImgServizio9_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio9.Click
        CaricaSomma(DirectCast(txtVoce9, TextBox).Attributes("ID").ToUpper.ToString, lblImporto9)
    End Sub

    Protected Sub ImgServizio10_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio10.Click
        CaricaSomma(DirectCast(txtVoce10, TextBox).Attributes("ID").ToUpper.ToString, lblImporto10)
    End Sub

    Protected Sub ImgServizio11_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio11.Click
        CaricaSomma(DirectCast(txtVoce11, TextBox).Attributes("ID").ToUpper.ToString, lblImporto11)
    End Sub

    Protected Sub ImgServizio12_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio12.Click
        CaricaSomma(DirectCast(txtVoce12, TextBox).Attributes("ID").ToUpper.ToString, lblImporto12)
    End Sub

    Protected Sub ChCompleto1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto1.CheckedChanged
        If txtImporto1.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce1, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce1.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto1.Checked = False
            End If
        End If


    End Sub


    'Private Function VerificaAler(ByVal idVoce As String) As Boolean
    '    Try
    '        VerificaAler = True

    '        par.OracleConn.Open()
    '        par.SettaCommand(par)


    '        par.cmd.CommandText = "select * from siscom_mi.pf_voci where id=" & idVoce
    '        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader5.Read Then
    '            If par.IfNull(myReader5("completo_aler"), "0") = "1" And par.IfNull(myReader5("completo"), "0") = "0" And par.IfNull(myReader5("valore_lordo_aler"), "0") <> par.IfNull(myReader5("valore_lordo"), "0") Then
    '                VerificaAler = False
    '            End If
    '        End If
    '        myReader5.Close()

    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    Catch ex As Exception
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try



    'End Function

    Private Function VerificaCompleta(ByVal idVoce As String) As Boolean
        Try
            VerificaCompleta = True
            If idVoce = "-1" Then
                VerificaCompleta = False
                Exit Function
            End If
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim Importo As Double = 0
            Dim Importo1 As Double = 0
            Dim testo As String = ""



            sDettagli = ""
            'par.cmd.CommandText = "select * from siscom_mi.pf_voci_importo where id_voce=" & idVoce
            par.cmd.CommandText = "SELECT * FROM siscom_mi.PF_VOCI_IMPORTO WHERE id_voce=" & idVoce & " AND ID_LOTTO IN (select lotti.ID from siscom_mi.lotti,operatori where operatori.id=" & Session.Item("ID_OPERATORE") & " AND lotti.ID_FILIALE=OPERATORI.ID_UFFICIO)"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader5.Read


                Importo = Format(par.IfNull(myReader5("valore_canone"), 0) * (1 + (par.IfNull(myReader5("iva_canone"), 0) / 100)) + par.IfNull(myReader5("valore_consumo"), 0) * (1 + (par.IfNull(myReader5("iva_consumo"), 0) / 100)), "0.00")
                Importo1 = 0
                par.cmd.CommandText = "select sum(importo_CANONE_LORDO+IMPORTO_CONSUMO_LORDO) from siscom_mi.lotti_patrimonio_importi where id_voce_importo=" & myReader5("id")
                Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader6.Read Then
                    Importo1 = Format(par.IfNull(myReader6(0), 0), "0.00")
                End If
                myReader6.Close()

                If Importo1 <> 0 Then
                    If Importo <> Importo1 Then
                        VerificaCompleta = False

                        par.cmd.CommandText = "select DESCRIZIONE from siscom_mi.tab_servizi where id=" & myReader5("ID_SERVIZIO")
                        Dim myReader7 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader7.Read Then
                            sDettagli = sDettagli & "- Servizio:" & par.IfNull(myReader7("descrizione"), "") & "<br/>"
                        End If
                        myReader7.Close()

                        par.cmd.CommandText = "select DESCRIZIONE from siscom_mi.lotti where id=" & myReader5("ID_LOTTO")
                        myReader7 = par.cmd.ExecuteReader()
                        If myReader7.Read Then
                            sDettagli = sDettagli & "- Lotto:" & par.IfNull(myReader7("descrizione"), "") & "<br/>"
                        End If
                        myReader7.Close()

                        sDettagli = sDettagli & " - Voce:" & myReader5("descrizione") & "<br/>"
                        sDettagli = sDettagli & " - Errore:L'importo da dividere è " & Format(Importo, "0.00") & " Euro, mentre risulta diviso tra complessi/edifici/scale/impianti " & Format(Importo1, "0.00") & " Euro." & "<br/><br/>"

                    End If
                End If
                Importo = 0
                Importo1 = 0
            Loop

            myReader5.Close()



            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try



    End Function

    Protected Sub ChCompleto2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto2.CheckedChanged
        If txtImporto2.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce2, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce2.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto2.Checked = False
            End If
        End If
    End Sub

    Protected Sub ChCompleto3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto3.CheckedChanged
        If txtImporto3.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce3, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce3.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto3.Checked = False
            End If
        End If
    End Sub

    Protected Sub ChCompleto4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto4.CheckedChanged
        If txtImporto4.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce4, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce4.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto4.Checked = False
            End If
        End If
    End Sub

    Protected Sub ChCompleto5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto5.CheckedChanged
        If txtImporto5.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce5, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce5.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto5.Checked = False
            End If
        End If
    End Sub

    Protected Sub ChCompleto6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto6.CheckedChanged
        If txtImporto6.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce6, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce6.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto6.Checked = False
            End If
        End If
    End Sub

    Protected Sub ChCompleto7_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto7.CheckedChanged
        If txtImporto7.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce7, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce7.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto7.Checked = False
            End If
        End If
    End Sub

    Protected Sub ChCompleto8_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto8.CheckedChanged
        If txtImporto8.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce8, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce8.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto8.Checked = False
            End If
        End If
    End Sub

    Protected Sub ChCompleto9_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto9.CheckedChanged
        If txtImporto9.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce9, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce9.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto9.Checked = False
            End If
        End If
    End Sub

    Protected Sub ChCompleto10_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto10.CheckedChanged
        If txtImporto10.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce10, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce10.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto10.Checked = False
            End If
        End If
    End Sub

    Protected Sub ChCompleto11_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto11.CheckedChanged
        If txtImporto11.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce11, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce11.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto11.Checked = False
            End If
        End If
    End Sub

    Protected Sub ChCompleto12_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto12.CheckedChanged
        If txtImporto12.Visible = False Then
            If VerificaCompleta(DirectCast(txtVoce12, TextBox).Attributes("ID").ToUpper.ToString) = False Then
                Session.Add("Dettagli", sDettagli)
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!\nSarà visualizzato il report con i dettagli!');window.open('DettaglioErrori.aspx?D=" & Replace("Voce: " & txtVoce12.Text, "'", "\") & "','Dettagli','');</script>")
                ChCompleto12.Checked = False
            End If
        End If
    End Sub


    Public Property sDettagli() As String
        Get
            If Not (ViewState("par_sDettagli") Is Nothing) Then
                Return CStr(ViewState("par_sDettagli"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDettagli") = value
        End Set

    End Property

    Protected Sub Sotto1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto1.Click
        CaricaSomme()
    End Sub

    Private Function SommaSottoSottoVoci(ByVal IdVoce As String, ByVal CasellaImporto As TextBox, ByVal Limporto As Label)

        par.OracleConn.Open()
        par.SettaCommand(par)

        par.cmd.CommandText = "select sum(valore_netto) ,sum(valore_lordo) from siscom_mi.pf_voci_STRUTTURA where id_VOCE=(SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE=" & IdVoce & ") AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        CasellaImporto.Text = Format(par.IfNull(myReader5(0), 0), "0.00")
        Limporto.Text = Format(par.IfNull(myReader5(1), 0), "0.00")
        myReader5.Close()

        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Function

    Protected Sub Sotto2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto2.Click
        CaricaSomme()
    End Sub

    Protected Sub Sotto3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto3.Click
        CaricaSomme()
    End Sub

    Protected Sub Sotto4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto4.Click
        CaricaSomme()
    End Sub

    Protected Sub Sotto5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto5.Click
        CaricaSomme()
    End Sub

    Protected Sub Sotto6_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto6.Click
        CaricaSomme()
    End Sub

    Protected Sub Sotto7_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto7.Click
        CaricaSomme()
    End Sub

    Protected Sub Sotto8_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto8.Click
        CaricaSomme()
    End Sub

    Protected Sub Sotto9_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto9.Click
        CaricaSomme()
    End Sub

    Protected Sub Sotto10_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto10.Click
        CaricaSomme()
    End Sub

    Protected Sub Sotto11_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto11.Click
        CaricaSomme()
    End Sub

    Protected Sub Sotto12_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Sotto12.Click
        CaricaSomme()
    End Sub

    Private Sub CaricaIva()
        Try
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva1, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva2, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva3, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva4, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva5, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva6, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva7, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva8, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva9, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva10, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva11, "VALORE", "VALORE", False)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva12, "VALORE", "VALORE", False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

End Class
