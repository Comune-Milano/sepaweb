<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inserimento.aspx.vb" Inherits="Contratti_Anagrafica_Inserimento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=0;

function $onkeydown() 
{  

if (event.keyCode==13) 
      {  
      alert('Usare il tasto <Avvia Ricerca>');
      history.go(0);
      event.keyCode=0;
      }  
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
    if ((10 - s % 10) % 10 != pi.charCodeAt(10) - '0'.charCodeAt(0))
    {
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
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
        <base target="_self"/>
		<title>Ricerca Contratti</title>
	</head>
	<body bgcolor="#ffffff">
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

</script>
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server" defaultfocus="txtCognome">
            &nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="14pt"
                ForeColor="#660000" Style="z-index: 111; left: 13px; position: absolute; top: 22px"
                Text="Dati Anagrafici"></asp:Label>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                ControlToValidate="txtDataNascita" ErrorMessage="!" Font-Bold="True" Font-Size="12pt"
                Height="1px" Style="z-index: 105; left: 161px; position: absolute; top: 110px"
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                Width="1px"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDoc"
                ErrorMessage="!" Font-Bold="True" Font-Size="12pt" Height="1px" Style="z-index: 105;
                left: 419px; position: absolute; top: 192px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                Width="1px"></asp:RegularExpressionValidator>
            <br />
            <br />
            <table style="left: 0px; background-image: url(../../NuoveImm/SfondoMascheraRubrica.jpg); width: 501px;
                position: absolute; top: 0px; height: 460px; z-index: 1; background-attachment: fixed; background-repeat: no-repeat;">
                <tr>
                    <td style="width: 798px; height: 456px;">
                        <asp:DropDownList ID="cmbSesso" runat="server" Style="left: 11px; position: absolute;
                            top: 106px; right: 430px;" Width="60px" TabIndex="4" Font-Names="ARIAL" 
                            Font-Size="10pt">
                            <asp:ListItem>M</asp:ListItem>
                            <asp:ListItem>F</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 168px; position: absolute; top: 134px">Partita Iva</asp:Label>
                        &nbsp;<br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                            <asp:Label ID="lblErroreCF" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                ForeColor="Red" Style="z-index: 106; left: 489px; position: absolute; top: 67px"
                                Visible="False">!</asp:Label>
                            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 106; left: 169px; position: absolute; top: 92px">Cittadinanza</asp:Label>
                            <asp:TextBox ID="txtPIva" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" MaxLength="11"
                                Style="z-index: 107; left: 168px; position: absolute; top: 150px" 
                            TabIndex="9" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                    <asp:DropDownList ID="cmbCittadinanza" runat="server" Style="left: 167px; position: absolute;
                        top: 107px; width: 167px; right: 167px;" TabIndex="6">
                    </asp:DropDownList>
                            &nbsp;&nbsp; </strong></span><br />

                        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 10px; position: absolute; top: 134px">Rag. Sociale*</asp:Label>
                        <asp:TextBox ID="txtRagione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Style="z-index: 107; left: 10px; position: absolute; top: 150px" TabIndex="8" MaxLength="100" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 442px; position: absolute; top: 279px">CAP</asp:Label>
                        <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 388px; position: absolute; top: 279px">Civico</asp:Label>
                        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 185px; position: absolute; top: 279px">Indirizzo</asp:Label>
                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 155px; position: absolute; top: 279px">Pr.</asp:Label>
                        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 106; left: 11px; position: absolute; top: 279px; right: 451px;">Comune</asp:Label>
                        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 11px; position: absolute; top: 218px">Documento Rilasciato da</asp:Label>
                        <asp:TextBox ID="txtDocRilasciato" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="100" Style="z-index: 107; left: 11px;
                            position: absolute; top: 234px" TabIndex="14" Width="473px"></asp:TextBox>
                        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 169px; position: absolute; top: 176px">N. Documento</asp:Label>
                        <asp:TextBox ID="txtNumDoc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="100" Style="z-index: 107; left: 169px;
                            position: absolute; top: 192px" TabIndex="12"></asp:TextBox>
                        &nbsp;
                        &nbsp;&nbsp;&nbsp;<br />
                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 75px; position: absolute; top: 92px">Data Nascita</asp:Label>
                        <asp:TextBox ID="txtDataNascita" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 107; left: 75px; position: absolute; top: 108px"
                            TabIndex="5" ToolTip="dd/MM/yyyy" Width="81px" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 328px; position: absolute; top: 176px">Data Rilascio</asp:Label>
                        <asp:TextBox ID="txtDataDoc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="10" Style="z-index: 107; left: 328px;
                            position: absolute; top: 192px" TabIndex="13" ToolTip="dd/MM/yyyy" 
                            Width="81px"></asp:TextBox>
                        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 11px; position: absolute; top: 91px">Sesso</asp:Label>
                        <asp:DropDownList ID="CmbComune" runat="server" Style="left: 353px; position: absolute;
                            top: 106px" Width="135px" TabIndex="7" Font-Names="ARIAL" Font-Size="10pt">
                        </asp:DropDownList><asp:DropDownList ID="cmbTipoDoc" runat="server" Style="left: 11px; position: absolute;
                            top: 191px" Width="150px" TabIndex="11" Font-Names="ARIAL" 
                            Font-Size="10pt">
                            <asp:ListItem Selected="True" Value="0">CARTA IDENTITA'</asp:ListItem>
                            <asp:ListItem Value="1">PASSAPORTO</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 11px; position: absolute; top: 176px">Tipo Documento</asp:Label>
                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 353px; position: absolute; top: 91px">Comune</asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 10px; position: absolute; top: 324px">ESTREMI DOC. DI SOGGIORNO</asp:Label>
                        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 10px; position: absolute; top: 261px">RESIDENZA</asp:Label>
                        <asp:TextBox ID="txtProvinciaResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="2" Style="z-index: 107; left: 154px; position: absolute; top: 295px; width: 23px; right: 324px;"
                            TabIndex="16" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCAP" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="5" Style="z-index: 107; left: 441px; position: absolute; top: 295px; width: 47px;"
                            TabIndex="19" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCivicoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="6" Style="z-index: 107; left: 387px; position: absolute; top: 295px; width: 47px;"
                            TabIndex="18" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtIndirizzoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 184px; position: absolute; top: 295px; width: 197px;"
                            TabIndex="17" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtSoggiorno" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 10px; position: absolute; top: 338px; width: 474px;"
                            TabIndex="20" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtComuneResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 10px; position: absolute; top: 295px; width: 139px;"
                            TabIndex="15" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtTel" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                            
                            Style="z-index: 107; left: 327px; position: absolute; top: 150px; width: 104px;" 
                            TabIndex="10" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 326px; position: absolute; top: 132px">Telefono</asp:Label>
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Style="z-index: 104; left: 10px; position: absolute;
                            top: 364px; height: 18px;" Visible="False" Width="468px"></asp:Label>
                        <asp:ImageButton ID="btnInserisciComponente" runat="server" ImageUrl="~/NuoveImm/Img_InserisciComponente.png"
                Style="z-index: 103; left: 241px; position: absolute; top: 387px; right: 130px;" 
                            TabIndex="23" ToolTip="Inserisci Componente" Visible="False" />

                        <asp:HiddenField ID="PIVA" runat="server" Value="0" />

                        <asp:HiddenField ID="DAC" runat="server" />
                        <asp:HiddenField ID="nuovocomp" runat="server" />
                        <asp:HiddenField ID="abusivo" runat="server" />
                <asp:Image ID="btnEventi" runat="server" ImageUrl="~/NuoveImm/Img_Eventi_Grande.png"
                    Style="position: absolute; cursor: pointer; top: 387px; left: 11px;" onclick="window.open('EventiAU.aspx?ID=' + document.getElementById('idau').value,'','');"
                    ToolTip="Eventi" />
                    </td>
                </tr>
            </table>
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                Style="z-index: 120; left: 445px; position: absolute; top: 386px" 
                TabIndex="22" ToolTip="Esci" />
            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                Style="z-index: 103; left: 377px; position: absolute; top: 386px; right: 697px; height: 20px;" 
                TabIndex="21" ToolTip="Salva" onclientclick="VerPIVA()" />
									<asp:label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 104; left: 10px; position: absolute; top: 50px">Cognome*</asp:label>
									<asp:textbox id="txtCognome" tabIndex="1" runat="server" style="z-index: 105; left: 10px; position: absolute; top: 66px" BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Font-Names="ARIAL" Font-Size="10pt"></asp:textbox>
									<asp:label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 106; left: 169px; position: absolute; top: 50px">Nome*</asp:label>
									<asp:textbox id="txtNome" tabIndex="2" runat="server" 
                style="z-index: 107; left: 168px; position: absolute; top: 66px; right: 807px;" 
                BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Font-Names="ARIAL" 
                Font-Size="10pt"></asp:textbox>
									<asp:label id="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 108; left: 328px; position: absolute; top: 50px">Codice Fiscale*</asp:label>
									<asp:textbox id="txtCF" tabIndex="3" runat="server" style="z-index: 109; left: 328px; position: absolute; top: 66px" BorderStyle="Solid" BorderWidth="1px" MaxLength="16" Width="156px" AutoPostBack="True" Font-Names="ARIAL" Font-Size="10pt"></asp:textbox>
            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:HiddenField ID="idau" runat="server" />
		</form>
	</body>
</html>

