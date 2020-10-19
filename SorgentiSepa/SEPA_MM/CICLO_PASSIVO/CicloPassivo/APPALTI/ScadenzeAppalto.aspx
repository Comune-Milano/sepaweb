<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScadenzeAppalto.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_ScadenzeAppalto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Scadenze Appalto</title>
    <style type="text/css">
        .style1
        {
            width: 707px;
            text-align: right;
        }
        #form1
        {
            width: 515px;
            height: 200px;
        }
        .style2
        {
            width: 66px;
        }
    </style>
    <link rel="shortcut icon" href="../../../favicon.ico" type="image/x-icon" />
    <link rel="icon" href="../../../favicon.ico" type="image/x-icon" />
    <script type="text/javascript" language="javascript">
        window.name = "modal";


        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            //        o.value = o.value.replace('.', ',');
            //document.getElementById('txtModificato').value = '1';

        }
        function AutoDecimal2(obj) {
            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {

                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali
                    //document.getElementById(obj.id).value = a.replace('.', ',')
                    document.getElementById(obj.id).value = risultato
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }

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
        function ConfermElimina() {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Verrà eliminata una scadenza!Continuare l\'operazione?");
            if (chiediConferma == true) {
                document.getElementById('ConfElimina').value = '1';

            }
        }
    </script>
</head>
<body >
    <form id="form1" runat="server" target="modal">
    <table style="width: 97%; position: absolute; top: 18px; left: 10px;">
        <tr>
            <td style="vertical-align: top; text-align: left" class="style1">
                <div style="border: 2px solid #ccccff; left: 0px; vertical-align: top; overflow: auto;
                    width: 414px; top: 0px; height: 142px; text-align: left">
                    <asp:DataGrid ID="dgvScadenze" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="Gray" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        PageSize="1" Style="z-index: 105; left: 8px; top: 32px" Width="404px" BorderWidth="1px">
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_APPALTO" HeaderText="ID_APPALTO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SCADENZA" HeaderText="SCADENZA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO €">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:DataGrid></div>
            </td>
            <td style="vertical-align: top; text-align: left" class="style2">
                <table>
                    <tr>
                        <td class="style1">
                            <asp:Image ID="imgAggiungiServ" runat="server" onclick="document.getElementById('InsScadenze').style.visibility='visible';
                document.getElementById('ScadSelected').value='0';
                document.getElementById('txtScadenza').value = '';
                document.getElementById('txtImporto').value = '';" ImageUrl="../../../NuoveImm/btn_Aggiungi.jpg"
                                ToolTip="Aggiungi " Style="cursor: pointer;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:ImageButton ID="btnApriAppalti" runat="server" CausesValidation="False" Height="12px"
                                ImageUrl="../../../NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('InsScadenze').style.visibility='visible';"
                                TabIndex="16" ToolTip="Modifica voce selezionata" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnEliminaAppalti" runat="server" ImageUrl="../../../NuoveImm/btn_Elimina.jpg"
                                TabIndex="15" ToolTip="Elimina voce selezionata" CausesValidation="False" OnClientClick="ConfermElimina();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                    Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="60"
                    ReadOnly="True" Style="left: 13px; top: 197px" Width="92%">Nessuna Selezione</asp:TextBox>
            </td>
            <td class="style2">
                <%--<asp:Image ID="imgEsci" runat="server" onclick="window.close();" ImageUrl="~/NuoveImm/Img_Esci.png" ToolTip="Aggiungi " Style="cursor: pointer;" />--%>
                <asp:Image ID="imgEsci" runat="server" onclick="CancelEdit();return false;" ImageUrl="~/NuoveImm/Img_Esci.png" ToolTip="Aggiungi " Style="cursor: pointer;" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Height="18px" Style="z-index: 104;" Visible="False" Width="422px"></asp:Label>
            </td>
            <td class="style2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:HiddenField ID="ScadSelected" runat="server" Value="0" />
                <asp:HiddenField ID="ConfElimina" runat="server" Value="0" />
                <asp:HiddenField ID="VisibilitaDiv" runat="server" Value="0" />
            </td>
            <td class="style2">
                &nbsp;
            </td>
        </tr>
    </table>
    <div id="InsScadenze" style="border: thin solid #6699FF; position: absolute; top: 5px;
        left: 1px; height: 175px; width: 504px; visibility: visible; background-image: url('Immagini/SfondoPiccoloScadenze.jpg');">
        <table style="width: 100%; height: 127px;">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 99%;">
                        <tr>
                            <td style="text-align: center">
                                <asp:TextBox ID="txtmia1" runat="server" BackColor="#EBE9ED" BorderStyle="None" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="60" ReadOnly="True"
                                    Style="left: 13px; top: 197px" Width="63px">SCADENZA</asp:TextBox>
                            </td>
                            <td style="text-align: center">
                                <asp:TextBox ID="txtmia2" runat="server" BackColor="#EBE9ED" BorderStyle="None" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="60" ReadOnly="True"
                                    Style="left: 13px; top: 197px" Width="63px">IMPORTO</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:TextBox ID="txtScadenza" runat="server" Width="81px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ControlToValidate="txtScadenza" ErrorMessage="!" Font-Bold="True" Font-Names="arial"
                                    Font-Size="8pt" Style="z-index: 150; left: 604px; top: 53px" TabIndex="100" ToolTip="Inserire una data valida"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: center">
                                <asp:TextBox ID="txtImporto" runat="server" Style="text-align: right" Width="81px"
                                    Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                <asp:TextBox ID="txtmia0" runat="server" BackColor="#EBE9ED" BorderStyle="None" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="60" ReadOnly="True"
                                    Style="left: 13px; top: 197px" Width="16px" TabIndex="-1">€.</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                &nbsp;
                            </td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="btnSave" runat="server" CausesValidation="False" Height="12px"
                                    ImageUrl="~/NuoveImm/Img_Aggiungi.png" OnClientClick="document.getElementById('InsScadenze').style.visibility='hidden';"
                                    TabIndex="16" ToolTip="Aggiungi la scadenza alla lista" />
                                <asp:Image ID="imgAnnulla" runat="server" onclick="document.getElementById('InsScadenze').style.visibility='hidden';"
                                    ImageUrl="~/NuoveImm/Img_Esci.png" ToolTip="Esci" Style="cursor: pointer;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">

        if (document.getElementById('VisibilitaDiv').value == 0) {
            document.getElementById('InsScadenze').style.visibility = 'hidden'
        }
        else {
            document.getElementById('InsScadenze').style.visibility = 'visible'

        }
    
    </script>
    <p>
        &nbsp;</p>
    </form>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        };
        function CancelEdit() {
            GetRadWindow().close();
        };
    </script>
</body>
</html>
