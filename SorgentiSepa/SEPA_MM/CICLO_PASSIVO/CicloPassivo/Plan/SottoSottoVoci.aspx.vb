
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

            CARICAIVA()
            CaricaStato()
            CaricaVoci()


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


     
        txtImporto1.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto1').innerText='0,00';document.getElementById('cmbIva1').value='0';document.getElementById('lblImporto1').innerHTML= (parseFloat(document.getElementById('txtImporto1').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto1').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva1').value))/100).toFixed(2).replace('.',',');")
        txtImporto2.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto2').innerHTML='0,00';document.getElementById('cmbIva2').value='0';document.getElementById('lblImporto2').innerHTML= (parseFloat(document.getElementById('txtImporto2').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto2').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva2').value))/100).toFixed(2).replace('.',',');")
        txtImporto3.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto3').innerHTML='0,00';document.getElementById('cmbIva3').value='0';document.getElementById('lblImporto3').innerHTML= (parseFloat(document.getElementById('txtImporto3').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto3').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva3').value))/100).toFixed(2).replace('.',',');")
        txtImporto4.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto4').innerHTML='0,00';document.getElementById('cmbIva4').value='0';document.getElementById('lblImporto4').innerHTML= (parseFloat(document.getElementById('txtImporto4').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto4').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva4').value))/100).toFixed(2).replace('.',',');")
        txtImporto5.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto5').innerHTML='0,00';document.getElementById('cmbIva5').value='0';document.getElementById('lblImporto5').innerHTML= (parseFloat(document.getElementById('txtImporto5').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto5').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva5').value))/100).toFixed(2).replace('.',',');")
        txtImporto6.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto6').innerHTML='0,00';document.getElementById('cmbIva6').value='0';document.getElementById('lblImporto6').innerHTML= (parseFloat(document.getElementById('txtImporto6').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto6').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva6').value))/100).toFixed(2).replace('.',',');")
        txtImporto7.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto7').innerHTML='0,00';document.getElementById('cmbIva7').value='0';document.getElementById('lblImporto7').innerHTML= (parseFloat(document.getElementById('txtImporto7').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto7').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva7').value))/100).toFixed(2).replace('.',',');")
        txtImporto8.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto8').innerHTML='0,00';document.getElementById('cmbIva8').value='0';document.getElementById('lblImporto8').innerHTML= (parseFloat(document.getElementById('txtImporto8').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto8').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva8').value))/100).toFixed(2).replace('.',',');")
        txtImporto9.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto9').innerHTML='0,00';document.getElementById('cmbIva9').value='0';document.getElementById('lblImporto9').innerHTML= (parseFloat(document.getElementById('txtImporto9').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto9').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva9').value))/100).toFixed(2).replace('.',',');")
        txtImporto10.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto10').innerHTML='0,00';document.getElementById('cmbIva10').value='0';document.getElementById('lblImporto10').innerHTML= (parseFloat(document.getElementById('txtImporto10').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto10').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva10').value))/100).toFixed(2).replace('.',',');")
     

        cmbIva1.Attributes.Add("onchange", "javascript:document.getElementById('lblImporto1').innerText= (parseFloat(document.getElementById('txtImporto1').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto1').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva1').value))/100).toFixed(2).replace('.',',');")
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

    Function CaricaCaselle(ByVal indice As Integer, ByVal DescrizioneVoce As TextBox, ByVal Codice As TextBox, ByVal Avviso As System.Web.UI.WebControls.Image, ByVal Importo As TextBox, ByVal LImporto As Label, ByVal Iva As DropDownList)
        Dim myReader51 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        par.cmd.CommandText = "select pf_voci.*,pf_voci_STRUTTURA.completo from siscom_mi.pf_voci,siscom_mi.pf_voci_struttura where pf_voci_struttura.id_struttura=" & Session.Item("id_struttura") & " and pf_voci.id=pf_voci_struttura.id_voce and id_voce_madre=" & idVoce.Value & " and indice=" & indice
        myReader51 = par.cmd.ExecuteReader()
        If myReader51.Read Then
            Codice.Text = par.IfNull(myReader51("codice"), "")
            Codice.Attributes.Add("ID_CAPITOLO", par.IfNull(myReader51("id_CAPITOLO"), "NULL"))
            DescrizioneVoce.Text = par.IfNull(myReader51("descrizione"), "")
            DescrizioneVoce.Attributes.Add("ID", par.IfNull(myReader51("id"), -1))
           


            'par.cmd.CommandText = "select * from siscom_mi.pf_voci_struttura,siscom_mi.pf_voci where pf_voci_struttura.id_voce=pf_voci.id and  id_voce=" & par.IfNull(myReader51("id"), -1) & " and id_struttura=" & Session.Item("id_struttura") & " AND (PF_VOCI.ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ") OR PF_VOCI.ID_VOCE_MADRE IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & "))"
            'Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader5.Read Then
            '    If par.IfNull(myReader5("completo_aler"), "0") = "1" And par.IfNull(myReader5("completo"), "0") = "0" And par.IfNull(myReader5("valore_lordo_aler"), "0") <> par.IfNull(myReader5("valore_lordo"), "0") Then
            '        Avviso.Visible = True
            '        Avviso.Attributes.Add("IMPORTO", par.IfNull(myReader5("valore_lordo_aler"), "0"))
            '        Avviso.ToolTip = "Importo approvato dal Gestore: " & Format(par.IfNull(myReader5("valore_lordo_aler"), "0"), "##,##0.00")
            '    End If
            'End If
            'myReader5.Close()

           

        Else
            DescrizioneVoce.Attributes.Add("ID", "-1")
            Codice.Attributes.Add("ID_CAPITOLO", "NULL")

        End If
        myReader51.Close()

        Codice.ReadOnly = True
        DescrizioneVoce.ReadOnly = True


        If DescrizioneVoce.Text = "" Then
            DescrizioneVoce.Visible = False
            Codice.Visible = False
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



                CaricaCaselle(1, txtVoce1, txtCod1, ImgAvviso1, txtImporto1, lblImporto1, cmbIva1)
                CaricaCaselle(2, txtVoce2, txtCod2, ImgAvviso2, txtImporto2, lblImporto2, cmbIva2)
                CaricaCaselle(3, txtVoce3, txtCod3, ImgAvviso3, txtImporto3, lblImporto3, cmbIva3)
                CaricaCaselle(4, txtVoce4, txtCod4, ImgAvviso4, txtImporto4, lblImporto4, cmbIva4)
                CaricaCaselle(5, txtVoce5, txtCod5, ImgAvviso5, txtImporto5, lblImporto5, cmbIva5)
                CaricaCaselle(6, txtVoce6, txtCod6, ImgAvviso6, txtImporto6, lblImporto6, cmbIva6)
                CaricaCaselle(7, txtVoce7, txtCod7, ImgAvviso7, txtImporto7, lblImporto7, cmbIva7)
                CaricaCaselle(8, txtVoce8, txtCod8, ImgAvviso8, txtImporto8, lblImporto8, cmbIva8)
                CaricaCaselle(9, txtVoce9, txtCod9, ImgAvviso9, txtImporto9, lblImporto9, cmbIva9)
                CaricaCaselle(10, txtVoce10, txtCod10, ImgAvviso10, txtImporto10, lblImporto10, cmbIva10)
                


                


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


        CaricaSommaNoServizi(DirectCast(txtVoce1, TextBox).Attributes("ID").ToUpper.ToString, txtImporto1, cmbIva1, lblImporto1)


       
            CaricaSommaNoServizi(DirectCast(txtVoce2, TextBox).Attributes("ID").ToUpper.ToString, txtImporto2, cmbIva2, lblImporto2)


       
            CaricaSommaNoServizi(DirectCast(txtVoce3, TextBox).Attributes("ID").ToUpper.ToString, txtImporto3, cmbIva3, lblImporto3)


            CaricaSommaNoServizi(DirectCast(txtVoce4, TextBox).Attributes("ID").ToUpper.ToString, txtImporto4, cmbIva4, lblImporto4)


        
            CaricaSommaNoServizi(DirectCast(txtVoce5, TextBox).Attributes("ID").ToUpper.ToString, txtImporto5, cmbIva5, lblImporto5)


        
            CaricaSommaNoServizi(DirectCast(txtVoce6, TextBox).Attributes("ID").ToUpper.ToString, txtImporto6, cmbIva6, lblImporto6)


        
            CaricaSommaNoServizi(DirectCast(txtVoce7, TextBox).Attributes("ID").ToUpper.ToString, txtImporto7, cmbIva7, lblImporto7)


       
            CaricaSommaNoServizi(DirectCast(txtVoce8, TextBox).Attributes("ID").ToUpper.ToString, txtImporto8, cmbIva8, lblImporto8)
        
            CaricaSommaNoServizi(DirectCast(txtVoce9, TextBox).Attributes("ID").ToUpper.ToString, txtImporto9, cmbIva9, lblImporto9)
       
            CaricaSommaNoServizi(DirectCast(txtVoce10, TextBox).Attributes("ID").ToUpper.ToString, txtImporto10, cmbIva10, lblImporto10)







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


    'Function VerificaAvviso(ByVal Avviso As System.Web.UI.WebControls.Image, ByVal chCompleto As CheckBox, ByVal importo As Label, ByVal Codice As TextBox)
    '    If Avviso.Visible = True And chCompleto.Checked = True Then
    '        If CDbl(DirectCast(Avviso, Image).Attributes("IMPORTO").ToString()) <> CDbl(importo.Text) Then
    '            Response.Write("<script>alert('ATTENZIONE...\nLa voce con codice " & Codice.Text & " non può essere definita completa perchè non è stata approvata dal Gestore! L\'importo lordo deve essere pari a euro : " & Format(CDbl(DirectCast(Avviso, Image).Attributes("IMPORTO").ToString()), "##,##0.00") & "');</script>")
    '            chCompleto.Checked = False
    '        End If
    '    End If
    'End Function

    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Try

            'VerificaAvviso(ImgAvviso1, ChCompleto1, lblImporto1, txtCod1)
            'VerificaAvviso(ImgAvviso2, ChCompleto2, lblImporto2, txtCod2)
            'VerificaAvviso(ImgAvviso3, ChCompleto3, lblImporto3, txtCod3)
            'VerificaAvviso(ImgAvviso4, ChCompleto4, lblImporto4, txtCod4)
            'VerificaAvviso(ImgAvviso5, ChCompleto5, lblImporto5, txtCod5)
            'VerificaAvviso(ImgAvviso6, ChCompleto6, lblImporto6, txtCod6)
            'VerificaAvviso(ImgAvviso7, ChCompleto7, lblImporto7, txtCod7)
            'VerificaAvviso(ImgAvviso8, ChCompleto8, lblImporto8, txtCod8)
            'VerificaAvviso(ImgAvviso9, ChCompleto9, lblImporto9, txtCod9)
            'VerificaAvviso(ImgAvviso10, ChCompleto10, lblImporto10, txtCod10)
            'VerificaAvviso(ImgAvviso11, ChCompleto11, lblImporto11, txtCod11)
            'VerificaAvviso(ImgAvviso12, ChCompleto12, lblImporto12, txtCod12)

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            Dim tot As Double = 0
            Dim TotLordo As Double = 0

           
            SalvaNoServizi(txtVoce1, 1, txtCod1, txtImporto1, ImgAvviso1, cmbIva1)
            tot = tot + txtImporto1.Text
            TotLordo = TotLordo + txtImporto1.Text + (txtImporto1.Text * (cmbIva1.SelectedItem.Value / 100))

            SalvaNoServizi(txtVoce2, 2, txtCod2, txtImporto2, ImgAvviso2, cmbIva2)
            tot = tot + txtImporto2.Text
            TotLordo = TotLordo + txtImporto2.Text + (txtImporto2.Text * (cmbIva2.SelectedItem.Value / 100))

            SalvaNoServizi(txtVoce3, 3, txtCod3, txtImporto3, ImgAvviso3, cmbIva3)
            tot = tot + txtImporto3.Text
            TotLordo = TotLordo + txtImporto3.Text + (txtImporto3.Text * (cmbIva3.SelectedItem.Value / 100))

            SalvaNoServizi(txtVoce4, 4, txtCod4, txtImporto4, ImgAvviso4, cmbIva4)
            tot = tot + txtImporto4.Text
            TotLordo = TotLordo + txtImporto4.Text + (txtImporto4.Text * (cmbIva4.SelectedItem.Value / 100))

            SalvaNoServizi(txtVoce5, 5, txtCod5, txtImporto5, ImgAvviso5, cmbIva5)
            tot = tot + txtImporto5.Text
            TotLordo = TotLordo + txtImporto5.Text + (txtImporto5.Text * (cmbIva5.SelectedItem.Value / 100))

            SalvaNoServizi(txtVoce6, 6, txtCod6, txtImporto6, ImgAvviso6, cmbIva6)
            tot = tot + txtImporto6.Text
            TotLordo = TotLordo + txtImporto6.Text + (txtImporto6.Text * (cmbIva6.SelectedItem.Value / 100))

            SalvaNoServizi(txtVoce7, 7, txtCod7, txtImporto7, ImgAvviso7, cmbIva7)
            tot = tot + txtImporto7.Text
            TotLordo = TotLordo + txtImporto7.Text + (txtImporto7.Text * (cmbIva7.SelectedItem.Value / 100))

            SalvaNoServizi(txtVoce8, 8, txtCod8, txtImporto8, ImgAvviso8, cmbIva8)
            tot = tot + txtImporto8.Text
            TotLordo = TotLordo + txtImporto8.Text + (txtImporto8.Text * (cmbIva8.SelectedItem.Value / 100))

            SalvaNoServizi(txtVoce9, 9, txtCod9, txtImporto9, ImgAvviso9, cmbIva9)
            tot = tot + txtImporto9.Text
            TotLordo = TotLordo + txtImporto9.Text + (txtImporto9.Text * (cmbIva9.SelectedItem.Value / 100))

            SalvaNoServizi(txtVoce10, 10, txtCod10, txtImporto10, ImgAvviso10, cmbIva10)
            tot = tot + txtImporto10.Text
            TotLordo = TotLordo + txtImporto10.Text + (txtImporto10.Text * (cmbIva10.SelectedItem.Value / 100))

            par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET VALORE_NETTO=" & par.VirgoleInPunti(tot) & ",VALORE_LORDO=" & par.VirgoleInPunti(TotLordo) & " WHERE ID_VOCE=" & idVoce.Value & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
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

    'Private Function Salva(ByVal casella As TextBox, ByVal indice As Integer, ByVal codice As TextBox, ByVal importo As Label, ByVal Avviso As System.Web.UI.WebControls.Image)
    '    If casella.Text = "" Then
    '        If DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString <> "-1" Then
    '            par.cmd.CommandText = "delete from siscom_mi.pf_voci_STRUTTURA WHERE ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") & " AND ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI where id_voce_madre=" & idVoce.Value & " and indice=" & indice & ")"
    '            par.cmd.ExecuteNonQuery()
    '            casella.Attributes.Add("ID", "-1")
    '            codice.Attributes.Add("ID_CAPITOLO", "NULL")
    '        End If
    '    Else
    '        If DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString = "-1" Then
    '            par.cmd.CommandText = "Insert into SISCOM_MI.PF_VOCI (ID, ID_PIANO_FINANZIARIO, CODICE, DESCRIZIONE, ID_VOCE_MADRE, INDICE, ID_CAPITOLO) Values (siscom_mi.seq_pf_voci.nextval, " & idPianoF.Value & ", '" & par.PulisciStrSql(codice.Text) & "', '" & par.PulisciStrSql(casella.Text) & "', " & idVoce.Value & ", " & indice & ", " & capitolo.Value & ")"
    '            par.cmd.ExecuteNonQuery()
    '            par.cmd.CommandText = "select siscom_mi.seq_pf_voci.currval from dual"
    '            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReader5.Read Then

    '                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_VOCI_STRUTTURA (ID_VOCE,ID_STRUTTURA) VALUES (" & par.IfNull(myReader5(0), -1) & "," & Session.Item("ID_STRUTTURA") & ")"
    '                par.cmd.ExecuteNonQuery()

    '                casella.Attributes.Add("ID", par.IfNull(myReader5(0), "-1"))
    '                codice.Attributes.Add("ID_CAPITOLO", capitolo.Value)

    '            End If
    '            myReader5.Close()
    '        Else
    '            par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET VALORE_LORDO=" & par.VirgoleInPunti(importo.Text) & ", COMPLETO='' WHERE ID_VOCE=" & DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
    '            par.cmd.ExecuteNonQuery()

    '        End If
    '    End If
    'End Function

    Private Function SalvaNoServizi(ByVal casella As TextBox, ByVal indice As Integer, ByVal codice As TextBox, ByVal importo As TextBox, ByVal Avviso As System.Web.UI.WebControls.Image, ByVal Iva As DropDownList)
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

                End If
                myReader5.Close()
            Else
                par.cmd.CommandText = "UPDATE SISCOM_MI.PF_VOCI_STRUTTURA SET VALORE_LORDO=" & par.VirgoleInPunti(Format(importo.Text + ((importo.Text * Iva.SelectedItem.Value) / 100), "0.00")) & ",  IVA=" & Iva.SelectedItem.Value & ",VALORE_NETTO=" & par.VirgoleInPunti(importo.Text) & ", COMPLETO='1' WHERE ID_VOCE=" & DirectCast(casella, TextBox).Attributes("ID").ToUpper.ToString & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
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
                CasellaIva.Items.FindByValue(par.IfNull(myReader5("iva"), "0")).Selected = True
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


    'Function CaricaSomma(ByVal indice As String, ByVal casella As Label)
    '    Try
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)


    '        par.cmd.CommandText = "select sum(valore_canone*(1+(iva_canone/100))+valore_consumo*(1+(iva_consumo/100))) from siscom_mi.pf_voci_importo where id_voce=" & indice & " AND ID_LOTTO IN (SELECT LOTTI.ID FROM SISCOM_MI.LOTTI WHERE ID_FILIALE=" & Session.Item("ID_STRUTTURA") & ")"
    '        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader5.Read Then
    '            casella.Text = Format(CDbl(par.IfNull(myReader5(0), "0")), "0.00")
    '        End If
    '        myReader5.Close()

    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    Catch ex As Exception
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try

    'End Function

   


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
            par.cmd.CommandText = "select * from siscom_mi.pf_voci_importo where id_voce=" & idVoce
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
                        sDettagli = sDettagli & " - Errore:L'importo da dividere è " & Format(Importo, "0.00") & " Euro, mentre risulta diviso tra complessi/edifici/scale " & Format(Importo1, "0.00") & " Euro." & "<br/><br/>"

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
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

End Class
