<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Virtuale.aspx.vb" Inherits="Contratti_Virtuale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<script type="text/javascript">
		    var Uscita;
		    Uscita = 0;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Canone 431/98</title>
    <style type="text/css">
        .style1
        {
            width: 322px;
        }
        .style2
        {
            width: 113px;
        }
        #contenitoreMesi
        {
            top: 353px;
            left: 473px;
        }
    </style>
</head>
<body>
    <p>
            </p>
<script type="text/javascript">
    function Conferma() {

        var sicuro = window.confirm('Sei sicuro di voler creare questo contratto?');
        if (sicuro == true) {
            document.getElementById('HiddenField1').value = '1';
        }
        else {
            document.getElementById('HiddenField1').value = '0';
        }
    }

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

    function SommaValori() {
        var Somma;
        var str;

        Somma = 0;
        
        str = document.getElementById('txtImporto1').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto2').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto3').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto4').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto5').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto6').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto7').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto8').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto9').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto10').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto11').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto12').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto13').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto14').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto15').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto16').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto17').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto18').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto19').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }

        str = document.getElementById('txtImporto20').value;
        if (str != '') {
            Somma = Somma + parseFloat(str.replace(',', '.'));
        }
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.getElementById('lbltotale').innerText = Somma.toFixed(2);

            if (Somma.toFixed(2) > 77.47) {
                document.getElementById('lblbollo').innerText = '+ Bollo (1.81) + Spese MAV (0.45)';
            }
            else {
                document.getElementById('lblbollo').innerText = '+ Spese MAV (0.45)';
            }
        }
        else {
            document.getElementById('lbltotale').textContent = Somma.toFixed(2);

            if (Somma.toFixed(2) > 77.47) {
                document.getElementById('lblbollo').textContent = '+ Bollo (1.81) + Spese MAV (0.45)';
            }
            else {
                document.getElementById('lblbollo').textContent = '+ Spese MAV (0.45)';
            }
        }
    }
</script>
    <form id="form1" runat="server" defaultbutton="ImgProcedi" 
    defaultfocus="txtCanoneCorrente">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Nuovo Rapporto Virtuale 
                    Manuale - </strong>
                    <asp:Label ID="Label2235" runat="server" Text="Label"></asp:Label>
                    </span><br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<br />
                    <br />
                    &nbsp;
                    &nbsp;&nbsp;<br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp;
                    <br />
                <asp:DropDownList ID="cmbTipoViaResidenza" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="8" 
                        style="position:absolute; top: 178px; left: 189px; width: 103px;">
                </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    &nbsp;<div id="VociBollette" 
                        
                        
                        
                        
                        
                        
                        
                        style="width: 455px; overflow: auto; height: 117px; position:absolute; top: 359px; left: 8px;">
                    <table style="width: 99%">
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce1" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="26" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto1" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="27"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtImporto1"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce2" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="28" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto2" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="29"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtImporto2"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce3" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="30" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto3" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="31"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtImporto3"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce4" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="32" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto4" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="33"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtImporto4"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce5" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="34" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto5" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="35"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtImporto5"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce6" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="36" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto6" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="37"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtImporto6"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce7" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="38" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto7" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="39"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtImporto7"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce8" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="40" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto8" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="41"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtImporto8"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce9" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="42" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto9" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="43"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtImporto9"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce10" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="44" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto10" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="45"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtImporto10"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce11" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="44" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto11" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="45"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtImporto11"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce12" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="44" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto12" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="45"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtImporto12"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce13" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="44" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto13" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="45"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtImporto13"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce14" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="44" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto14" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="45"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtImporto14"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce15" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="44" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto15" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="45"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtImporto15"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce16" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="46" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto16" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="47"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtImporto16"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce17" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="48" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto17" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="49"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtImporto17"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce18" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="50" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto18" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="51"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="txtImporto18"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce19" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="52" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto19" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="53"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ControlToValidate="txtImporto19"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td class="style1">
                <asp:DropDownList ID="cmbVoce20" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="54" 
                         Width="320px">
                </asp:DropDownList>
                            </td>
                            <td valign="top" class="style2">
                <asp:TextBox onblur="SommaValori();" ID="txtImporto20" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="60px" TabIndex="55"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ControlToValidate="txtImporto20"
                    ErrorMessage="0,00" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" 
                                    ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator></td>
                        </tr>
                        </table>
                        </div>
                    <br />
                    <asp:ImageButton ID="imgCopia2" runat="server" 
                        style="position:absolute;cursor:pointer; top: 236px; left: 621px; width: 18px;" 
                        ImageUrl="~/NuoveImm/CopiaDa.gif" TabIndex="19" 
                        ToolTip="Copia indirizzo in..." />
                    <asp:ImageButton ID="imgCopia1" runat="server" 
                        style="position:absolute;cursor:pointer; top: 175px; left: 621px;" 
                        ImageUrl="~/NuoveImm/CopiaDa.gif" TabIndex="12" 
                        ToolTip="Copia indirizzo in..." />
                    <asp:CheckBox ID="ChST" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt" Text="Trattasi di Occupante Senza Titolo" 
                        style="position:absolute; top: 526px; left: 9px;" TabIndex="48"/>
                    <br />
                    <div id="contenitoreMesi" 
                        
                        style="border: 1px solid #CC0000; position: absolute; width: 320px; height: 171px; overflow: auto;">
                    <asp:CheckBoxList style="position:absolute; top: 0px; left: 0px;" 
                        ID="ListaArretrati" runat="server" Font-Names="arial" 
                        Font-Size="8pt" RepeatColumns="3" TabIndex="46">
                    </asp:CheckBoxList>
                    </div>
                    <br />
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                        Style="left: 623px; position: absolute; top: 531px; right: 97px; " 
                        TabIndex="50" onclientclick="Conferma();" />
                    <br />
                        <img onclick="document.location.href='pagina_home.aspx';" alt="" src="../NuoveImm/Img_Home.png" 
                            style="position:absolute; top: 531px; left: 708px; cursor:pointer;"/>&nbsp;
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 10px; position: absolute; top: 56px">Questa procedura permette di inserire un rapporto virtuale e permettere quindi l&#39;emissione delle bollette, ed eventuali arretrati, a tutti gli utenti che non sono stati inclusi fino ad ora nella bollettazione massiva.</asp:Label>
                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 504px; position: absolute; top: 97px">Cod.Fiscale</asp:Label>
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 278px; position: absolute; top: 97px">Nome</asp:Label>
                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 505px; position: absolute; top: 122px">Part. IVA</asp:Label>
                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 12px; position: absolute; top: 122px">Ragione Sociale</asp:Label>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        
                        Style="z-index: 104; left: 9px; position: absolute; top: 548px; height: 17px; width: 587px;" 
                        ForeColor="Red"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 12px; position: absolute; top: 97px">Cognome</asp:Label>
                        <asp:Label ID="Label2234" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 481px; position: absolute; top: 331px">CREA BOLLETTE ARRETRATE</asp:Label>
                        <asp:Label ID="lbltotale" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"                  
                        
                        Style="z-index: 104; left: 347px; position: absolute; top: 484px; width: 58px; height: 15px;text-align:right" 
                        BackColor="#999999">0.00</asp:Label>
                        <asp:Label ID="Label2233" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"
                        
                        
                        Style="z-index: 104; left: 12px; position: absolute; top: 329px; width: 466px;">VOCI DA INSERIRE (importi mensili, il bollo viene inserito auto., se dovuto). Spese MAV inserite automaticamente.</asp:Label>
                        <asp:Label ID="lblbollo" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"
                        
                        
                        
                        Style="z-index: 104; left: 300px; position: absolute; top: 502px; width: 213px; height: 14px;"></asp:Label>
                        <asp:Label ID="Label2237" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"
                        
                        
                        Style="z-index: 104; left: 300px; position: absolute; top: 484px; width: 40px;">Totale</asp:Label>
                        <asp:Label ID="Label2236" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 12px; position: absolute; top: 505px">TIPOLOGIA DEL RAPPORTO</asp:Label>
                        <asp:Label ID="Label2223" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 12px; position: absolute; top: 268px">INDIRIZZO DI SPEDIZIONE BOLLETTA</asp:Label>
                        <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 12px; position: absolute; top: 206px">INDIRIZZO DELL&#39;UNITA&#39;</asp:Label>
                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 12px; position: absolute; top: 145px">RESIDENZA DELL&#39;INTESTATARIO</asp:Label>
                        <asp:Label ID="Label2228" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 549px; position: absolute; top: 286px">Cap</asp:Label>
                        <asp:Label ID="Label23" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 550px; position: absolute; top: 225px">Cap</asp:Label>
                        <asp:Label ID="Label2227" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 496px; position: absolute; top: 286px">Civico</asp:Label>
                        <asp:Label ID="Label22" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 496px; position: absolute; top: 225px">Civico</asp:Label>
                        <asp:Label ID="Label2229" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 190px; position: absolute; top: 286px">Indirizzo</asp:Label>
                        <asp:Label ID="Label2226" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 191px; position: absolute; top: 225px">Indirizzo</asp:Label>
                        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 191px; position: absolute; top: 164px">Indirizzo</asp:Label>
                        <asp:Label ID="Label2232" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 158px; position: absolute; top: 286px">Pr.</asp:Label>
                        <asp:Label ID="Label2224" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 159px; position: absolute; top: 225px">Pr.</asp:Label>
                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 160px; position: absolute; top: 164px">Pr.</asp:Label>
                        <asp:Label ID="Label2225" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 12px; position: absolute; top: 286px; ">Comune</asp:Label>
                        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 12px; position: absolute; top: 225px; ">Comune</asp:Label>
                        <asp:TextBox ID="txtIndirizzoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 298px; position: absolute; top: 178px; width: 191px;"
                            TabIndex="9" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtpiva" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="11" Style="z-index: 107; left: 568px; position: absolute; top: 118px; width: 181px; right: 51px;"
                            TabIndex="5" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCF" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="16" Style="z-index: 107; left: 568px; position: absolute; top: 94px; width: 181px; right: 51px;"
                            TabIndex="3" Font-Names="ARIAL" Font-Size="10pt" AutoPostBack="True" 
                        CausesValidation="True"></asp:TextBox>
                        <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 313px; position: absolute; top: 94px; width: 181px; right: 306px;"
                            TabIndex="2" Font-Names="ARIAL" Font-Size="10pt" ForeColor="Black"></asp:TextBox>
                        <asp:TextBox ID="txtRS" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 92px; position: absolute; top: 118px; width: 402px; right: 306px;"
                            TabIndex="4" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 92px; position: absolute; top: 94px; width: 181px; right: 527px;"
                            TabIndex="1" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtComuneResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 11px; position: absolute; top: 178px; width: 140px; right: 649px;"
                            TabIndex="6" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtPrSpedizione" runat="server" 
                        BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="2" Style="z-index: 107; left: 157px; position: absolute; top: 300px; width: 23px; "
                            TabIndex="21" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtPrUnita" runat="server" 
                        BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="2" Style="z-index: 107; left: 158px; position: absolute; top: 239px; width: 23px; "
                            TabIndex="14" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtCapResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="5" Style="z-index: 107; left: 549px; position: absolute; top: 178px; width: 47px;"
                            TabIndex="11" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtCivicoSpedizione" runat="server" 
                        BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="6" Style="z-index: 107; left: 495px; position: absolute; top: 300px; width: 47px;"
                            TabIndex="24" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            
                            <asp:TextBox ID="txtCivicoUnita" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="6" Style="z-index: 107; left: 495px; position: absolute; top: 239px; width: 47px;"
                            TabIndex="17" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 106; left: 12px; position: absolute; top: 164px; ">Comune</asp:Label>
                        <asp:TextBox ID="txtIndirizzoSpedizione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 299px; position: absolute; top: 300px; width: 191px;"
                            TabIndex="23" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtIndirizzoUnita" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="50" Style="z-index: 107; left: 299px; position: absolute; top: 239px; width: 191px;"
                            TabIndex="16" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtComuneSpedizione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 11px; position: absolute; top: 300px; width: 140px; right: 649px;"
                            TabIndex="20" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                        <asp:TextBox ID="txtComuneUnita" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" Style="z-index: 107; left: 11px; position: absolute; top: 239px; width: 140px; right: 649px;"
                            TabIndex="13" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtPrResidenza" runat="server" 
                        BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="2" Style="z-index: 107; left: 159px; position: absolute; top: 178px; width: 23px; "
                            TabIndex="7" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtCapSpedizione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="5" Style="z-index: 107; left: 548px; position: absolute; top: 300px; width: 47px;"
                            TabIndex="25" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtCapUnita" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="5" Style="z-index: 107; left: 549px; position: absolute; top: 239px; width: 47px;"
                            TabIndex="18" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:TextBox ID="txtCivicoResidenza" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="6" Style="z-index: 107; left: 495px; position: absolute; top: 178px; width: 47px;"
                            TabIndex="10" Font-Names="ARIAL" Font-Size="10pt"></asp:TextBox>
                            <asp:DropDownList ID="cmbTipoRapporto" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="47" 
                        style="position:absolute; top: 500px; left: 166px; width: 103px;">
                                <asp:ListItem Selected="True" Value="1">ERP</asp:ListItem>
                                <asp:ListItem Value="3">USD</asp:ListItem>
                                <asp:ListItem Value="6">431/98</asp:ListItem>
                                <asp:ListItem Value="5">392/78</asp:ListItem>
                                <asp:ListItem Value="7">ABUSIVO</asp:ListItem>
                        </asp:DropDownList>
                            <asp:DropDownList ID="cmbTipoViaCor" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="22" 
                        style="position:absolute; top: 300px; left: 188px; width: 103px;">
                        </asp:DropDownList>
                            <asp:DropDownList ID="cmbTipoViaUnita" runat="server" Font-Names="Arial" 
                        Font-Size="9pt" TabIndex="15" 
                        style="position:absolute; top: 239px; left: 189px; width: 103px;">
                        </asp:DropDownList>
                        <asp:Label ID="Label2222" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 496px; position: absolute; top: 164px">Civico</asp:Label>
                <asp:Label ID="Label2231" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 641px; position: absolute; top: 240px">Copia in INDIRIZZO SPEDIZIONE</asp:Label>
                <asp:Label ID="Label2230" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 642px; position: absolute; top: 179px">Copia in INDIRIZZO UNITA&#39;</asp:Label>
                <asp:Label ID="Label8" runat="server" Font-Bold="False" 
                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 106; left: 550px; position: absolute; top: 164px">Cap</asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    <a href="../cf/codice.htm" target="_blank"><img border="0" 
            alt="Calcolo Codice Fiscale" src="../NuoveImm/codice_fiscale.gif"       
            style="position :absolute;cursor:pointer; top: 87px; left: 755px;"/></a>
            <asp:RegularExpressionValidator runat="server" style="position:absolute; top: 181px; left: 600px;"
                        ValidationExpression="^\d{5}$" ControlToValidate="txtCapResidenza" 
                        ErrorMessage="5 n" Font-Names="arial" Font-Size="8pt" TabIndex="303" 
                        ID="ControlloCap3">5 n</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator runat="server" style="position:absolute; top: 241px; left: 600px;"
                        ValidationExpression="^\d{5}$" ControlToValidate="txtCapUnita" 
                        ErrorMessage="5 n" Font-Names="arial" Font-Size="8pt" TabIndex="303" 
                        ID="ControlloCap2">5 n</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator runat="server" style="position:absolute; top: 303px; left: 602px;"
                        ValidationExpression="^\d{5}$" ControlToValidate="txtCapSpedizione" 
                        ErrorMessage="5 Numeri" Font-Names="arial" Font-Size="8pt" TabIndex="303" 
                        ID="ControlloCap1">5 numeri</asp:RegularExpressionValidator>
    </div>
    
    </form>
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>


