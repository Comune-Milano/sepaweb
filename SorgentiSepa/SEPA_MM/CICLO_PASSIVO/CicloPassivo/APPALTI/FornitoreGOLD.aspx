<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FornitoreGOLD.aspx.vb" Inherits="MANUTENZIONI_FornitoreGOLD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {

            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }


    function $onkeydown() {

        if (event.keyCode == 13) {
            event.keyCode = 0;
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }

</script>
<script type="text/javascript">
    var Uscita;
    Uscita = 0;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Fornitore</title>
</head>
<body>
<script type="text/javascript">
    if (navigator.appName == 'Microsoft Internet Explorer') {
        document.onkeydown = $onkeydown;
    }
    else {
        window.document.addEventListener("keydown", TastoInvio, true);
    }
</script>
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

        if (document.getElementById('txtPIva').value != '') {
            ControllaPIVA(document.getElementById('txtPIva').value);
        }

    }

    function ConfermaEsci() {
        if (document.getElementById('txtModificato').value == '1') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
            if (chiediConferma == false) {
                document.getElementById('txtModificato').value = '111';
                //document.getElementById('USCITA').value='0';
            }
        }
    } 
    
</script>
    <form id="form1" runat="server" defaultbutton="ImgProcedi">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
            width: 685px; position: absolute; top: 0px; height: 543px;">
            <tr>
                <td style="width: 800px; height: 570px;">
                    <br />
                    &nbsp;<span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong><asp:Label 
                        ID="lbltitolo" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="14pt" Text="Inserisci Persona Giuridica  per Fornitore"></asp:Label>
                    </strong></span><br />
                    <br />
                    <asp:TextBox ID="txtPIva" style="position: absolute; top: 81px; width:80px; left: 179px; " 
                        runat="server" MaxLength="11" TabIndex="2" BorderStyle="Solid" 
                        BorderWidth="1px"></asp:TextBox>
                    <asp:TextBox ID="txtCF" style="position: absolute; top: 80px; width:85px; left: 300px;" 
                        runat="server" MaxLength="11" TabIndex="3" BorderStyle="Solid" 
                        BorderWidth="1px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                        runat="server" ControlToValidate="txtCF" style="position: absolute; top: 100px; left: 302px; width: 104px; right: 394px;"
                                    ErrorMessage="Inserire 11 cifre !" 
                        ValidationExpression="^\d+$" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="8pt">Inserire 11 cifre !</asp:RegularExpressionValidator>
                    <br />
                    <a href="../cf/codice.htm" target="_blank">
                    <img border="0" 
                        alt="Calcolo Codice Fiscale" src="../NuoveImm/codice_fiscale.gif" 
                        style="position :absolute;cursor:pointer; top: 383px; left: 465px; "/></a><asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                        runat="server" ControlToValidate="txtPIva" style="position: absolute; top: 99px; left: 180px; width: 104px;"
                                    ErrorMessage="Inserire 11 cifre !" 
                        ValidationExpression="^\d+$" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="8pt">Inserire 11 cifre !</asp:RegularExpressionValidator>
                                <br />
                    &nbsp;<br />
                    <br />
                    <asp:Label ID="lblErroreCF" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        ForeColor="Red" Style="z-index: 106; left: 457px; position: absolute; top: 390px"
                        Visible="False">!</asp:Label>
                    &nbsp;<br />
                    &nbsp;&nbsp;<br />
        <hr style="left: 6px; width: 674px; position: absolute; top: 105px; z-index: 115; font-weight: 700;" />
        <hr style="left: 6px; width: 674px; position: absolute; top: 214px; z-index: 115; font-weight: 700;" />
                    <br />
                    <br />
                    <br />
                    <br />

                    <br />
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DrLTipoR" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" 
                        Style="border: 1px solid black; z-index: 111; left: 510px; position: absolute; top: 390px; width: 154px; height: 20px;" 
                        TabIndex="24" AutoPostBack="True" Height="20px" >
                        <asp:ListItem Value="L">LEGALE RAPP.</asp:ListItem>
                        <asp:ListItem Value="P">PROCURATORE LEG.</asp:ListItem>
                </asp:DropDownList>
                    <asp:DropDownList ID="DrLTipoIndR" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" 
                        Style="border: 1px solid black; z-index: 111; left: 6px; position: absolute; top: 432px; width: 97px; " 
                        TabIndex="25" Height="20px" >
                </asp:DropDownList>
                    <asp:DropDownList ID="DrLTipoIndA" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" 
                        Style="border: 1px solid black; z-index: 111; left: 6px; position: absolute; top: 269px; width: 97px; " 
                        TabIndex="13" Height="20px" >
                </asp:DropDownList>
                    <asp:DropDownList ID="DrLTipoInd" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" 
                        Style="border: 1px solid black; z-index: 111; left: 6px; position: absolute; top: 152px; width: 97px; " 
                        TabIndex="5" >
                </asp:DropDownList>
                    <br />
                    <br />
                    <asp:RegularExpressionValidator ID="validaiban" runat="server" 
                        ControlToValidate="txtIban" Display="Dynamic" 
                        ErrorMessage="Codice Errato!" Style="font-size: 8pt; position:absolute;font-family: Arial; top: 99px; left: 422px; height: 14px; width: 166px;" 
                        
                        ValidationExpression="IT\d{2}[ ][a-zA-Z]\d{3}[ ]\d{4}[ ]\d{4}[ ]\d{4}[ ]\d{4}[ ]\d{3}|IT\d{2}[a-zA-Z]\d{22}" 
                        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<asp:TextBox ID="txtIban" 
                        runat="server" MaxLength="27" 
                        
                        Style="font-size: 10pt; position: absolute; font-family: Arial; top: 80px; left: 420px; width: 189px;" 
                        TabIndex="4" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                    <br />
                    &nbsp;
                    <hr style="left: 6px; width: 672px; position: absolute; top: 336px; z-index: 115;" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 486px; position: absolute; top: 518px; right: 224px;" 
                        TabIndex="19" onclientclick="VerPIVA()"/>
                        <asp:RegularExpressionValidator ID="controllodata" 
                        runat="server" ControlToValidate="txtdataprocura"
                            ErrorMessage="data non valida!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 354px;
                            position: absolute; top: 473px; width: 116px;" ToolTip="Inserire una data valida" 
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                                            Font-Size="8pt" Font-Names="arial" Display="Dynamic" 
                        Enabled="False"></asp:RegularExpressionValidator>
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();"
                        Style="z-index: 101; left: 572px; position: absolute; top: 519px; height: 20px;" 
                        ToolTip="Home" TabIndex="20" CausesValidation="False" />
                    <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
                        
                        Style="z-index: 104; left: 9px; position: absolute; top: 231px; font-weight: 700;">SEDE AMMINISTRATIVA</asp:Label>
                    <asp:Label ID="Label39" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
                        
                        Style="z-index: 104; left: 8px; position: absolute; top: 350px; font-weight: 700;">RAPPRESENTANTE</asp:Label>
                    <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
                        
                        Style="z-index: 104; left: 8px; position: absolute; top: 115px; font-weight: 700;">SEDE LEGALE</asp:Label>
                        <asp:TextBox ID="txtProvinciaResidenzaR" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="2" Style="z-index: 107; left: 517px; position: absolute; top: 431px; width: 23px; "
                            TabIndex="30" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtProvinciaResidenzaA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="2" Style="z-index: 107; left: 517px; position: absolute; top: 269px; width: 23px; "
                            TabIndex="18" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtProvinciaResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="2" Style="z-index: 107; left: 517px; position: absolute; top: 152px; width: 23px; "
                            TabIndex="10" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCAPR" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="5" Style="z-index: 107; left: 317px; position: absolute; top: 431px; width: 47px; bottom: 121px; right: 438px;"
                            TabIndex="28" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCAPA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="5" Style="z-index: 107; left: 317px; position: absolute; top: 269px; width: 47px; bottom: 283px; right: 439px;"
                            TabIndex="16" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCAP" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="5" Style="z-index: 107; left: 317px; position: absolute; top: 152px; width: 47px; bottom: 400px; "
                            TabIndex="8" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCivicoResidenzaR" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="6" Style="z-index: 107; left: 259px; position: absolute; top: 431px; width: 47px; right: 505px;"
                            TabIndex="27" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCivicoResidenzaA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="6" Style="z-index: 107; left: 259px; position: absolute; top: 269px; width: 47px; right: 495px; bottom: 283px;"
                            TabIndex="15" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCivicoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="6" Style="z-index: 107; left: 259px; position: absolute; top: 152px; width: 47px; "
                            TabIndex="7" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:Label ID="Label38" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 378px; position: absolute; top: 138px">Comune</asp:Label>
                            <asp:Label ID="Label44" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            
                        
                        Style="z-index: 106; left: 378px; position: absolute; top: 417px; height: 14px;">Comune</asp:Label>
                            <asp:Label ID="Label33" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 378px; position: absolute; top: 255px">Comune</asp:Label>
                            <asp:Label ID="Label43" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 318px; position: absolute; top: 417px">CAP</asp:Label>
                            <asp:Label ID="Label32" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            
                        
                        
                        Style="z-index: 106; left: 318px; position: absolute; top: 255px; height: 14px;">CAP</asp:Label>
                            <asp:Label ID="Label24" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 318px; position: absolute; top: 137px">CAP</asp:Label>
                            <asp:Label ID="Label42" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 260px; position: absolute; top: 417px">Civico</asp:Label>
                            <asp:Label ID="Label49" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 7px; position: absolute; top: 458px">Riferimenti</asp:Label>
                            <asp:Label ID="Label31" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 260px; position: absolute; top: 255px">Civico</asp:Label>
                            <asp:Label ID="Label22" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 260px; position: absolute; top: 137px">Civico</asp:Label>
                        <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 421px; position: absolute; top: 66px">IBAN</asp:Label>
                        <asp:Label ID="Label37" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 9px; position: absolute; top: 136px">Tipo Indirizzo</asp:Label>
                        <asp:Label ID="Label40" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 511px; position: absolute; top: 376px">Tipo</asp:Label>
                        <asp:Label ID="Label50" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 8px; position: absolute; top: 416px">Tipo Indirizzo</asp:Label>
                        <asp:Label ID="Label29" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 8px; position: absolute; top: 255px">Tipo Indirizzo</asp:Label>
                        <asp:Label ID="lbldataprocura" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 276px; position: absolute; top: 457px" 
                        Visible="False">Data Procura</asp:Label>
                        <asp:Label ID="lblprocura" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 152px; position: absolute; top: 459px" 
                        Visible="False">Numero Procura</asp:Label>
                        <asp:Label ID="Label41" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 117px; position: absolute; top: 417px">Indirizzo</asp:Label>
                        <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 117px; position: absolute; top: 255px">Indirizzo</asp:Label>
                        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 117px; position: absolute; top: 137px">Indirizzo</asp:Label>
                        <asp:Label ID="Label45" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 518px; position: absolute; top: 417px">Pr.</asp:Label>
                        <asp:Label ID="Label34" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 518px; position: absolute; top: 255px">Pr.</asp:Label>
                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 518px; position: absolute; top: 139px">Pr.</asp:Label>
                        <asp:TextBox ID="txtIndirizzoResidenzaA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 116px; position: absolute; top: 269px; width: 130px;"
                            TabIndex="14" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtIndirizzoResidenzaR" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 116px; position: absolute; top: 431px; width: 130px;"
                            TabIndex="26" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtRiferimenti" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 6px; position: absolute; top: 472px; width: 130px; right: 665px;"
                            TabIndex="34" Font-Names="ARIAL" Font-Size="10pt" TextMode="MultiLine"></asp:TextBox>
                        <asp:TextBox ID="txtIndirizzoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 116px; position: absolute; top: 152px; width: 130px; right: 554px;"
                            TabIndex="6" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtComuneResidenzaR" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 377px; position: absolute; top: 431px; width: 130px; "
                            TabIndex="29" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtComuneResidenzaA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 377px; position: absolute; top: 269px; width: 130px; "
                            TabIndex="17" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtComuneResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 377px; position: absolute; top: 152px; width: 130px; "
                            TabIndex="9" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                    <asp:TextBox ID="txtnumprocura" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 151px; position: absolute; top: 473px; width: 110px;" 
                        TabIndex="32" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtFaxA" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 136px; position: absolute; top: 310px; width: 110px;" 
                        TabIndex="20"></asp:TextBox>
                    <asp:TextBox ID="txtFax" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 135px; position: absolute; top: 192px; width: 110px;" 
                        TabIndex="12"></asp:TextBox>
                    <asp:TextBox ID="txtTelR" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 553px; position: absolute; top: 431px; width: 110px;" 
                        TabIndex="31"></asp:TextBox>
                    <asp:TextBox ID="txtTelA" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 6px; position: absolute; top: 310px; width: 110px;" 
                        TabIndex="19"></asp:TextBox>
                    <asp:TextBox ID="txtTel" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 6px; position: absolute; top: 192px; width: 110px;" 
                        TabIndex="11"></asp:TextBox>
                    <asp:Label ID="Label36" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" 
                        
                        Style="z-index: 106; left: 141px; position: absolute; top: 296px; width: 18px;">Fax</asp:Label>
                    <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" 
                        Style="z-index: 106; left: 136px; position: absolute; top: 178px">Fax</asp:Label>
                    <asp:Label ID="Label46" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" 
                        
                        
                        
                        Style="z-index: 106; left: 554px; position: absolute; top: 417px; height: 14px;">Telefono</asp:Label>
                    <asp:Label ID="Label35" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" 
                        
                        
                        Style="z-index: 106; left: 10px; position: absolute; top: 296px; height: 14px;">Telefono</asp:Label>
                    <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" 
                        Style="z-index: 106; left: 9px; position: absolute; top: 178px">Telefono</asp:Label>
									<asp:textbox id="txtdataprocura" tabIndex="33" runat="server" 
                                        style="z-index: 107; left: 275px; position: absolute; top: 473px; bottom: 79px; right: 455px; width: 70px;" 
                                        BorderStyle="Solid" BorderWidth="1px" MaxLength="10" 
                        ToolTip="GG/MM/YYYY" Visible="False"></asp:textbox>
                    <asp:Label ID="Label47" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 9px; position: absolute; top: 67px">Ragione Sociale *</asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 8px; position: absolute; top: 375px">Cognome</asp:Label>
                    <asp:TextBox ID="txtragione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="50" Style="z-index: 105; left: 8px; position: absolute; top: 81px; "
                        TabIndex="1"></asp:TextBox>
                    <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="50" Style="z-index: 105; left: 7px; position: absolute; top: 389px; right: 667px;"
                        TabIndex="21"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 165px; position: absolute; top: 376px">Nome</asp:Label>
                    <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"                        
                        Style="z-index: 107; left: 165px; position: absolute; top: 389px; right: 509px;" 
                        TabIndex="22"></asp:TextBox>
                    <asp:Label ID="Label48" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 108; left: 301px; position: absolute; top: 66px">Codice Fiscale</asp:Label>
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 108; left: 327px; position: absolute; top: 375px">Codice Fiscale</asp:Label>
                    <asp:TextBox ID="txtCFR" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="16"
                        
                        Style="z-index: 109; left: 326px; position: absolute; top: 389px; width: 130px;" 
                        TabIndex="23" AutoPostBack="True"></asp:TextBox>



                     
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; left: 6px; position: absolute;
                        top: 518px; height: 17px; width: 470px;" Visible="False"></asp:Label>



                     
                    <br />
                    <br />

                        <asp:HiddenField ID="PIVA" runat="server" Value="0" />

                </td>
            </tr>
        </table>
    
    </div>
        

        <asp:TextBox ID="USCITA"        runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1">0</asp:TextBox>
        <asp:TextBox ID="txtModificato" runat="server" BackColor="White" 
        BorderStyle="None" ForeColor="White" 
        Style="left: 0px; position: absolute; top: 196px; z-index: -1;">0</asp:TextBox>
        
        <asp:TextBox ID="txtIdFornitori" runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>
        <asp:TextBox ID="SOLO_LETTURA" runat="server" Style="z-index: -1; left: 0px; position: absolute; top: 415px" TabIndex="-1" Width="24px">0</asp:TextBox>        

    
    <script type="text/javascript">
        window.focus();
        self.focus();
</script>
    
    <p>
        <asp:Label ID="LBLPIVA" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 126; left: 180px; position: absolute; top: 67px"
            Width="72px" TabIndex="-1">Partita IVA *</asp:Label>
        </p>

    
    </form>
    
    </body>
</html>
