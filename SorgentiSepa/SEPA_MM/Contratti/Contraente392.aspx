<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Contraente392.aspx.vb" Inherits="Contratti_Contraente392" %>

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
    <form id="form1" runat="server" defaultbutton="ImgProcedi" 
    defaultfocus="txtCognome">
    <p>
                        <asp:TextBox ID="txtSoggiorno" runat="server" 
            BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 47px; position: absolute; top: 393px; width: 499px;"
                            TabIndex="20" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        </p>
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
</script>
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Seleziona/Inserisci
                        Nominativo per abbinamento 392/78</strong></span><br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<a href="../cf/codice.htm" target="_blank"><img border="0" alt="Calcolo Codice Fiscale" src="../NuoveImm/codice_fiscale.gif" 
                        style="position :absolute;cursor:pointer; top: 108px; left: 576px; "/></a><br />
                    <br />
                    <asp:Label ID="lblErroreCF" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        ForeColor="Red" Style="z-index: 106; left: 553px; position: absolute; top: 115px"
                        Visible="False">!</asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                        ControlToValidate="txtDataNascita" ErrorMessage="!" Font-Bold="True" Font-Size="12pt"
                        Height="1px" Style="z-index: 105; left: 198px; position: absolute; top: 156px"
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="1px"></asp:RegularExpressionValidator>
                    &nbsp;<br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDoc"
                        ErrorMessage="!" Font-Bold="True" Font-Size="12pt" Height="1px" Style="z-index: 105;
                        left: 480px; position: absolute; top: 240px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="1px"></asp:RegularExpressionValidator>
                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 47px; position: absolute; top: 266px">Documento Rilasciato da</asp:Label>
                    <asp:TextBox ID="txtDocRilasciato" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="100" Style="z-index: 107; left: 47px;
                        position: absolute; top: 282px" TabIndex="14" Width="497px"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 219px; position: absolute; top: 224px">N. Documento</asp:Label>
                    <asp:TextBox ID="txtNumDoc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="100" Style="z-index: 107; left: 219px;
                        position: absolute; top: 240px" TabIndex="12"></asp:TextBox>
                    <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 389px; position: absolute; top: 224px">Data Rilascio</asp:Label>
                    <asp:TextBox ID="txtDataDoc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="10" Style="z-index: 107; left: 389px;
                        position: absolute; top: 240px" TabIndex="13" ToolTip="dd/MM/yyyy" Width="81px"></asp:TextBox>
                    <asp:DropDownList ID="cmbTipoDoc" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                        Style="left: 47px; position: absolute; top: 239px" TabIndex="11" Width="150px">
                        <asp:ListItem Selected="True" Value="0">CARTA IDENTITA'</asp:ListItem>
                        <asp:ListItem Value="1">PASSAPORTO</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 47px; position: absolute; top: 224px">Tipo Documento</asp:Label>
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
                    <br />
                    <br />
                    <br />
                    <br />

                        <asp:HiddenField ID="PIVA" runat="server" Value="0" />

                    <br />
                    <br />
                    <asp:ImageButton ID="ImgSeleziona" runat="server" ImageUrl="~/NuoveImm/Img_Seleziona.png"
                        Style="left: -442px; position: absolute; top: 638px; height: 20px;" 
                        onclientclick="ApriAccessoAnagrafica();" 
                        ToolTip="Seleziona il contraente dall'anagrafica" />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 516px; height: 20px;" 
                        TabIndex="22" onclientclick="VerPIVA()" />
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 666px; position: absolute; top: 515px" 
                        ToolTip="Home" TabIndex="23" />
                    <asp:DropDownList ID="cmbSesso" runat="server" Style="left: 47px; position: absolute;
                        top: 154px" TabIndex="4" Width="60px">
                        <asp:ListItem>M</asp:ListItem>
                        <asp:ListItem>F</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 106; left: 218px; position: absolute; top: 182px">Partita Iva</asp:Label>
                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 106; left: 218px; position: absolute; top: 140px">Cittadinanza</asp:Label>
                    <asp:TextBox ID="txtPIva" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        
                        Style="z-index: 107; left: 218px; position: absolute; top: 198px; right: 456px;" 
                        TabIndex="9"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 46px; position: absolute; top: 182px">Rag. Sociale</asp:Label>
                    <asp:TextBox ID="txtRagione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="100" Style="z-index: 107; left: 46px; position: absolute; top: 198px"
                        TabIndex="8"></asp:TextBox>
                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 111px; position: absolute; top: 140px">Data Nascita</asp:Label>
                    <asp:TextBox ID="txtDataNascita" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="10" Style="z-index: 107; left: 111px; position: absolute; top: 156px; right: 608px;"
                        TabIndex="5" ToolTip="dd/MM/yyyy" Width="81px"></asp:TextBox>
                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 47px; position: absolute; top: 139px">Sesso</asp:Label>
                    <asp:DropDownList ID="CmbComune" runat="server" Style="left: 389px; position: absolute;
                        top: 154px" TabIndex="7" Width="158px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbCittadinanza" runat="server" Style="left: 218px; position: absolute;
                        top: 155px" TabIndex="6" Width="151px">
                    </asp:DropDownList>
                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 389px; position: absolute; top: 139px">Comune</asp:Label>
                    <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 46px; position: absolute; top: 378px">ESTREMI DOC. SOGGIORNO</asp:Label>
                    <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 46px; position: absolute; top: 316px">RESIDENZA</asp:Label>
                        <asp:Label ID="Label24" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 553px; position: absolute; top: 335px">CAP</asp:Label>
                        <asp:Label ID="Label22" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 498px; position: absolute; top: 335px">Civico</asp:Label>
                        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 248px; position: absolute; top: 335px">Indirizzo</asp:Label>
                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 218px; position: absolute; top: 336px">Pr.</asp:Label>
                        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            
                        Style="z-index: 106; left: 48px; position: absolute; top: 336px; right: 674px;">Comune</asp:Label>
                        <asp:TextBox ID="txtIndirizzoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 248px; position: absolute; top: 350px; width: 241px;"
                            TabIndex="17" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtComuneResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 47px; position: absolute; top: 350px; width: 163px; right: 590px;"
                            TabIndex="15" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtProvinciaResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="2" Style="z-index: 107; left: 217px; position: absolute; top: 350px; width: 23px; right: 560px;"
                            TabIndex="16" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtCAP" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="5" Style="z-index: 107; left: 552px; position: absolute; top: 350px; width: 47px;"
                            TabIndex="19" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtCivicoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="6" Style="z-index: 107; left: 497px; position: absolute; top: 350px; width: 47px;"
                            TabIndex="18" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_RicercaDaAnagrafica.png"
                        Style="left: 33px; position: absolute; top: 516px" 
                        onclientclick="ApriAccessoAnagrafica();" 
                        ToolTip="Seleziona il contraente dall'anagrafica" TabIndex="21" />
                    <asp:TextBox ID="txtTel" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 388px; position: absolute; top: 198px" TabIndex="10"
                        Width="157px"></asp:TextBox>
                    <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 106; left: 387px; position: absolute; top: 180px">Telefono</asp:Label>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Height="74px" Style="z-index: 104; left: 47px; position: absolute;
                        top: 417px" Visible="False" Width="506px"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 46px; position: absolute; top: 98px">Cognome*</asp:Label>
                    <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="50" Style="z-index: 105; left: 46px; position: absolute; top: 114px"
                        TabIndex="1"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 219px; position: absolute; top: 98px">Nome*</asp:Label>
                    <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 107; left: 218px; position: absolute; top: 114px" TabIndex="2"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 108; left: 389px; position: absolute; top: 98px">Codice Fiscale*</asp:Label>
                    <asp:TextBox ID="txtCF" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="16"
                        Style="z-index: 109; left: 389px; position: absolute; top: 114px" TabIndex="3"
                        Width="156px" AutoPostBack="True"></asp:TextBox>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

