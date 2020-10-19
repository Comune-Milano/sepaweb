<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProvvedimentoAssegnazione.aspx.vb" Inherits="ASS_ProvvedimentoAssegnazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Ricerca Domanda</title>
</head>
<body bgcolor="#f2f5f1">
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="txtCognome">
    <div>
        &nbsp;</div>
        &nbsp;<asp:ImageButton ID="btnAnnulla" runat="server" 
        CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 100; left: 538px; position: absolute; top: 455px" 
        TabIndex="7" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
            Style="z-index: 101; left: 446px; position: absolute; top: 455px; height: 20px;" 
        TabIndex="6" ToolTip="Avvia Ricerca" onclientclick="VerificaSalva();" />
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Provvedimento Assegnazione</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="txtData" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 105; left: 303px; position: absolute; top: 306px" TabIndex="2"
                        Width="82px"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                        ControlToValidate="txtData" ErrorMessage="Non Valido (gg/mm/aaaa)" Font-Bold="True"
                        Font-Names="arial" Font-Size="9pt" Height="15px" Style="z-index: 105; left: 389px;
                        position: absolute; top: 309px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="155px"></asp:RegularExpressionValidator>
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="H1" runat="server" Value="0" />
                    <asp:HiddenField ID="OFFERTA" runat="server" />
                    <asp:HiddenField ID="CFPIVA" runat="server" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
                                <asp:Label ID="Label1" runat="server" 
        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
        style="z-index: 102; left: 31px; position: absolute; top: 311px">N. Provvedimento</asp:Label>
                                <asp:TextBox ID="txtProvvedimento" runat="server" 
        TabIndex="1" style="z-index: 103; left: 130px; position: absolute; top: 306px" 
        BorderStyle="Solid" BorderWidth="1px" MaxLength="40" Width="130px"></asp:TextBox>
                                <asp:Label ID="lblAss" runat="server" 
        Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" 
        style="z-index: 104; left: 31px; position: absolute; top: 130px">Data</asp:Label>
                                <asp:Label ID="Label4" runat="server" 
        Font-Bold="True" Font-Names="Arial" Font-Size="9pt" 
        style="z-index: 104; left: 31px; position: absolute; top: 270px">Se la documentazione è stata prodotta e firmata dal responsabile, inserire gli estremi del provvedimento.</asp:Label>
                                <asp:Label ID="Label6" runat="server" 
        Font-Bold="True" Font-Names="Arial" Font-Size="9pt" 
        style="z-index: 104; left: 149px; position: absolute; top: 75px">Elenco Documenti da produrre</asp:Label>
                                <asp:Label ID="Label5" runat="server" 
        Font-Bold="True" Font-Names="Arial" Font-Size="9pt" 
        style="z-index: 104; left: 31px; position: absolute; top: 75px">OFFERTA NUMERO </asp:Label>
                                <asp:Label ID="Label3" runat="server" 
        Font-Bold="True" Font-Names="Arial" Font-Size="9pt" 
        
        style="z-index: 104; left: 31px; position: absolute; top: 108px; right: 814px;" 
        Width="200px">Elenco Documenti da produrre</asp:Label>
                                <asp:Label ID="Label2" runat="server" 
        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
        style="z-index: 104; left: 274px; position: absolute; top: 311px">Data</asp:Label>
    </form>
</body>
    <script type="text/javascript">
        function VerificaSalva() {

            if (document.getElementById('txtProvvedimento').value != '' && document.getElementById('txtData').value != '') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sicuri di voler inserire Numero e Data del Provvedimento? Premere OK per confermare o ANNULLA per non procedere.");
                if (chiediConferma == false) {
                    document.getElementById('H1').value = '0';
                }
                else {
                    document.getElementById('H1').value = '1';
                }
                }
                else
                {
                alert('Inserire il numero e la data del provvedimento. Premere il pulsante HOME se si desidera annullare!');
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
        </script>
</html>

