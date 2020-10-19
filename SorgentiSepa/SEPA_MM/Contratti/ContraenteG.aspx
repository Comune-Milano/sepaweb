<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ContraenteG.aspx.vb" Inherits="Contratti_ContraenteG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
	Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Contraente</title>
</head>
<body>
<script type="text/javascript">
    function CompletaData(e, obj) {
        // Check if the key is a number
        var sKeyPressed;

        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
                // don't insert last non-numeric character
                if (window.event) {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
            }
        }
        else {
            if (obj.value.length == 2) {
                obj.value += "/";
            }
            else if (obj.value.length == 5) {
                obj.value += "/";
            }
            else if (obj.value.length > 9) {
                var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                if (selText.length == 0) {
                    // make sure the field doesn't exceed the maximum length
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
        }
    }

    function ApriAccessoAnagrafica() {
        var win = null;
        LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
        LeftPosition = LeftPosition - 20;
        TopPosition = TopPosition - 20;
        window.showModalDialog('Anagrafica/menu.htm', window, 'status:no;dialogTop=' + TopPosition + ';dialogLeft=' + LeftPosition + ';dialogWidth:620px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
    }

    function ControllaPIVA(pi) {
        risultato = '0';
        if (pi == '') {
            document.getElementById('PIVA').value = '0';
            return '';
        }
        if (pi.length != 11) {
            alert("La lunghezza della partita IVA non è\n" +
			"corretta: la partita IVA dovrebbe essere lunga\n" +
			"esattamente 11 caratteri.\n");
            document.getElementById('PIVA').value = '1';
            return "1";
        }
        validi = "0123456789";
        for (i = 0; i < 11; i++) {
            if (validi.indexOf(pi.charAt(i)) == -1) {
                alert("La partita IVA contiene un carattere non valido `" +
				pi.charAt(i) + "'.\nI caratteri validi sono le cifre.\n");
                document.getElementById('PIVA').value = '1';
                return "1";
            }
        }
        s = 0;
        for (i = 0; i <= 9; i += 2)
            s += pi.charCodeAt(i) - '0'.charCodeAt(0);
        for (i = 1; i <= 9; i += 2) {
            c = 2 * (pi.charCodeAt(i) - '0'.charCodeAt(0));
            if (c > 9) c = c - 9;
            s += c;
        }
        if ((10 - s % 10) % 10 != pi.charCodeAt(10) - '0'.charCodeAt(0)) {
            alert("La partita IVA non è valida:\n" +
			"il codice di controllo non corrisponde.\n");
            document.getElementById('PIVA').value = '1';
            return '1';
        }
    }

    function VerPIVA() {

        document.getElementById('PIVA').value = '0';

        if (document.getElementById('txtPIVA').value != '') {
            ControllaPIVA(document.getElementById('txtPIVA').value);
        }

    }
</script>
    <form id="form1" runat="server" defaultbutton="ImgProcedi" 
    defaultfocus="txtRagione">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Inserisci
                        Persona Giuridica per abbinamento 
                    <asp:Label ID="Label35" runat="server" Text="Label"></asp:Label>
                    </strong></span><br />
                    <br />
                    <br />
                    <a href="../cf/codice.htm" target="_blank">
                    <img border="0" 
                        alt="Calcolo Codice Fiscale" src="../NuoveImm/codice_fiscale.gif" 
                        style="position :absolute;cursor:pointer; top: 239px; left: 490px; "/></a><br />
                    &nbsp;<br />
                    <br />
                    <asp:Label ID="lblErroreCF" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        ForeColor="Red" Style="z-index: 106; left: 475px; position: absolute; top: 246px"
                        Visible="False">!</asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                        ControlToValidate="txtDataNascita" ErrorMessage="!" Font-Bold="True" 
                        Font-Size="12pt" Style="z-index: 105; left: 198px; position: absolute; top: 285px"
                        
                        
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                    &nbsp;<br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <asp:RadioButton ID="chProcuratore" runat="server" 
                        style="position:absolute; top: 199px; left: 213px;" Font-Bold="False" 
                        Font-Names="arial" Font-Size="10pt" GroupName="A" TabIndex="10" 
                        Text="Procuratore" AutoPostBack="True" CausesValidation="True"/>
                    <asp:RadioButton ID="chLegale" runat="server" 
                        style="position:absolute; top: 199px; left: 43px;" Checked="True" 
                        Font-Bold="False" Font-Names="arial" Font-Size="10pt" GroupName="A" 
                        TabIndex="9" Text="Rappresentante Legale" AutoPostBack="True" 
                        CausesValidation="True"/>
                    <br />
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" 
                        runat="server" ControlToValidate="txtDataProcura"
                        ErrorMessage="!" Font-Bold="True" Font-Size="12pt" Style="z-index: 105;
                        left: 635px; position: absolute; top: 198px; width: 5px;" 
                        
                        
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDoc"
                        ErrorMessage="!" Font-Bold="True" Font-Size="12pt" Style="z-index: 105;
                        left: 474px; position: absolute; top: 328px; width: 5px;" 
                        
                        
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
                        
                        
                        
                        Style="z-index: 106; left: 47px; position: absolute; top: 357px; width: 118px; right: 635px;">Documento Rilasciato da</asp:Label>
                    <asp:TextBox ID="txtDocRilasciato" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="100" Style="z-index: 107; left: 47px;
                        position: absolute; top: 374px; width: 329px; right: 424px;" TabIndex="23"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 209px; position: absolute; top: 313px">N. Documento</asp:Label>
                    <asp:TextBox ID="txtNumDoc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="100" Style="z-index: 107; left: 208px;
                        position: absolute; top: 330px; width: 168px;" TabIndex="21"></asp:TextBox>
                    <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 389px; position: absolute; top: 312px">Data Rilascio</asp:Label>
                    <asp:TextBox ID="txtDataProcura" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="10" Style="z-index: 107; left: 549px;
                        position: absolute; top: 198px" TabIndex="12" ToolTip="dd/MM/yyyy" 
                        Width="81px" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtDataDoc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="10" Style="z-index: 107; left: 387px;
                        position: absolute; top: 329px" TabIndex="22" ToolTip="dd/MM/yyyy" 
                        Width="81px"></asp:TextBox>
                    <asp:DropDownList ID="cmbTipoDoc" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        Style="left: 47px; position: absolute; top: 331px" TabIndex="20" 
                        Width="150px">
                        <asp:ListItem Selected="True" Value="0">CARTA IDENTITA'</asp:ListItem>
                        <asp:ListItem Value="1">PASSAPORTO</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 47px; position: absolute; top: 314px">Tipo Documento</asp:Label>
                    <br />

                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp;
                    <br />
                        <asp:TextBox ID="txtSoggiorno" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 387px; position: absolute; top: 374px; width: 318px;"
                            TabIndex="24" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

                        <asp:HiddenField ID="PIVA" runat="server" Value="0" />

                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 516px; height: 20px;" 
                        TabIndex="31" onclientclick="VerPIVA()" />
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 666px; position: absolute; top: 515px" 
                        ToolTip="Home" TabIndex="32" />
                    <asp:DropDownList ID="cmbSesso" runat="server" Style="left: 47px; position: absolute;
                        top: 287px; right: 693px;" TabIndex="16" Width="60px">
                        <asp:ListItem>M</asp:ListItem>
                        <asp:ListItem>F</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" 
                        Style="z-index: 106; left: 207px; position: absolute; top: 271px">Cittadinanza</asp:Label>
                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 115px; position: absolute; top: 271px">Data Nascita</asp:Label>
                    <asp:TextBox ID="txtDataNascita" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="10" Style="z-index: 107; left: 114px; position: absolute; top: 287px; right: 605px;"
                        TabIndex="17" ToolTip="dd/MM/yyyy" Width="81px"></asp:TextBox>
                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 48px; position: absolute; top: 271px">Sesso</asp:Label>
                    <asp:DropDownList ID="CmbComune" runat="server" Style="left: 366px; position: absolute;
                        top: 286px" TabIndex="19" Width="158px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbCittadinanza" runat="server" Style="left: 207px; position: absolute;
                        top: 286px" TabIndex="18" Width="151px">
                    </asp:DropDownList>
                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 366px; position: absolute; top: 272px">Comune</asp:Label>
                    <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 388px; position: absolute; top: 357px">Estremi Doc. di Soggiorno</asp:Label>
                    <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
                        
                        Style="z-index: 104; left: 40px; position: absolute; top: 178px; font-weight: 700;">RAPPRESENTANTE</asp:Label>
                    <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
                        
                        
                        Style="z-index: 104; left: 40px; position: absolute; top: 401px; font-weight: 700;">RESIDENTE IN </asp:Label>
                    <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
                        
                        Style="z-index: 104; left: 40px; position: absolute; top: 116px; font-weight: 700;">SEDE LEGALE</asp:Label>
                        <asp:TextBox ID="txtProvinciaResidenza0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="2" Style="z-index: 107; left: 217px; position: absolute; top: 432px; width: 23px; right: 560px;"
                            TabIndex="26" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtProvinciaResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="2" Style="z-index: 107; left: 217px; position: absolute; top: 148px; width: 23px; right: 560px;"
                            TabIndex="4" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCAP0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="5" Style="z-index: 107; left: 553px; position: absolute; top: 432px; width: 47px;"
                            TabIndex="29" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCAP" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="5" Style="z-index: 107; left: 553px; position: absolute; top: 147px; width: 47px;"
                            TabIndex="7" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCivicoResidenza0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="6" Style="z-index: 107; left: 497px; position: absolute; top: 432px; width: 47px;"
                            TabIndex="28" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCivicoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="6" Style="z-index: 107; left: 497px; position: absolute; top: 147px; width: 47px;"
                            TabIndex="6" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:Label ID="Label33" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 553px; position: absolute; top: 418px">CAP</asp:Label>
                            <asp:Label ID="Label24" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 553px; position: absolute; top: 133px">CAP</asp:Label>
                            <asp:Label ID="Label32" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 497px; position: absolute; top: 418px">Civico</asp:Label>
                            <asp:Label ID="Label22" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 497px; position: absolute; top: 133px">Civico</asp:Label>
                        <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 249px; position: absolute; top: 418px">Indirizzo</asp:Label>
                        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 249px; position: absolute; top: 134px">Indirizzo</asp:Label>
                        <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 218px; position: absolute; top: 418px">Pr.</asp:Label>
                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 218px; position: absolute; top: 134px">Pr.</asp:Label>
                        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            
                        
                        Style="z-index: 106; left: 47px; position: absolute; top: 132px; right: 693px;">Comune</asp:Label>
                        <asp:TextBox ID="txtIndirizzoResidenza0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 248px; position: absolute; top: 432px; width: 241px;"
                            TabIndex="27" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtIndirizzoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 248px; position: absolute; top: 148px; width: 241px;"
                            TabIndex="5" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtComuneResidenza0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 47px; position: absolute; top: 432px; width: 163px; right: 590px;"
                            TabIndex="25" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtComuneResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 47px; position: absolute; top: 148px; width: 163px; right: 590px;"
                            TabIndex="3" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                    <asp:TextBox ID="txtTel0" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 610px; position: absolute; top: 432px" TabIndex="30"
                        Width="157px"></asp:TextBox>
                    <asp:TextBox ID="txtTel" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 610px; position: absolute; top: 147px" TabIndex="8"
                        Width="157px"></asp:TextBox>
                    <asp:Label ID="Label34" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" 
                        Style="z-index: 106; left: 609px; position: absolute; top: 418px">Telefono</asp:Label>
                    <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" 
                        Style="z-index: 106; left: 609px; position: absolute; top: 133px">Telefono</asp:Label>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; left: 48px; position: absolute;
                        top: 466px; height: 30px;" Visible="False" Width="506px"></asp:Label>
                    <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 310px; position: absolute; top: 70px">Partita IVA*</asp:Label>
                    <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 46px; position: absolute; top: 70px">Ragione Sociale*</asp:Label>
                    <asp:Label ID="Label29" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 48px; position: absolute; top: 417px">Comune</asp:Label>
                    <asp:Label ID="lblDataProcura" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 524px; position: absolute; top: 201px" 
                        Visible="False">Data:</asp:Label>
                    <asp:Label ID="lblNumeroProcura" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 327px; position: absolute; top: 201px" 
                        Visible="False">N.ro Procura:</asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 48px; position: absolute; top: 229px">Cognome*</asp:Label>
                    <asp:TextBox ID="txtRagione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="50" Style="z-index: 105; left: 46px; position: absolute; top: 87px; width: 257px; right: 497px;"
                        TabIndex="1"></asp:TextBox>
                    <asp:TextBox ID="txtNumProcura" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="50" Style="z-index: 105; left: 391px; position: absolute; top: 198px; width: 124px;"
                        TabIndex="11" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="50" Style="z-index: 105; left: 47px; position: absolute; top: 246px; width: 124px;"
                        TabIndex="13"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 180px; position: absolute; top: 232px">Nome*</asp:Label>
                    <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 179px; position: absolute; top: 246px; width: 125px;" 
                        TabIndex="14"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 108; left: 312px; position: absolute; top: 232px">Codice Fiscale*</asp:Label>
                    <asp:TextBox ID="txtPIVA" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="11"
                        Style="z-index: 109; left: 309px; position: absolute; top: 87px" TabIndex="2"
                        Width="156px" AutoPostBack="True"></asp:TextBox>
                    <asp:TextBox ID="txtCF" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="16"
                        
                        Style="z-index: 109; left: 312px; position: absolute; top: 246px; width: 153px;" TabIndex="15"
                        AutoPostBack="True"></asp:TextBox>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
