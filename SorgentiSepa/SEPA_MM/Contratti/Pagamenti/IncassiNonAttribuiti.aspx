<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IncassiNonAttribuiti.aspx.vb"
    Inherits="Contratti_Pagamenti_IncassiNonAttribuiti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Incassi Non Attribuiti</title>
    <script language="javascript" type="text/javascript">
        window.name = "modal";
        var Selezionato;
        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                event.keyCode = 0;
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

        }
        function CompletaData(e, obj) {
            var sKeyPressed;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    if (window.event) {
                        event.keyCode = 0;
                    } else {
                        e.preventDefault();
                    }
                }
            } else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                } else if (obj.value.length == 5) {
                    obj.value += "/";
                } else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        if (window.event) {
                            event.keyCode = 0;
                        } else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        }
        function AutoDecimal2(obj) {
            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2);
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    }
                    risultato = dascrivere + risultato + ',' + decimali;
                    document.getElementById(obj.id).value = risultato;
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',');
                }
            }
        }
    </script>
    <style type="text/css">
        #form1
        {
            position: relative;
            top: 25px;
            left: 5px;
            width: 780px;
            height: 580px;
            font-family: Arial;
            font-weight: normal;
            font-size: 8pt;
        }
    </style>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat;">
    <form id="form1" runat="server" target="modal">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Incassi non attribuiti" Font-Bold="True"
            Font-Size="14pt" ForeColor="Maroon"></asp:Label>
        <br />
        <br />
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td style="width: 20%">
                    Data incasso
                </td>
                <td>
                    <asp:TextBox ID="TextBoxDataIncasso" runat="server" Width="70px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorDataIncasso" runat="server"
                        ErrorMessage="!" ControlToValidate="TextBoxDataIncasso" Font-Bold="True" Font-Names="Arial"
                        Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    Nominativo
                </td>
                <td>
                    <asp:TextBox ID="TextBoxNominativo" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Tipologia pagamento
                </td>
                <td>
                    <asp:DropDownList ID="cmbTipoPagamento" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    Note
                </td>
                <td>
                    <asp:TextBox ID="TextBoxNote" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    Causale
                </td>
                <td>
                    <asp:TextBox ID="TextBoxCausale" runat="server" Width="70%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 20%">
                                Importo da
                            </td>
                            <td style="width: 15%">
                                <asp:TextBox ID="TextBoxImportoMin" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td style="width: 2%">
                                a
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxImportoMax" runat="server" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 20px;">
                </td>
                <td style="height: 20px;">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 100%; text-align: right">
                                <asp:ImageButton ID="ImageButtonAvviaRicerca" runat="server" ImageUrl="../../NuoveImm/Img_AvviaRicerca.png"
                                    ToolTip="Avvia Ricerca" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 20px;">
                </td>
                <td style="height: 20px;">
                </td>
            </tr>
        </table>
        <div style="height: 250px; overflow: auto;">
            <asp:DataGrid runat="server" ID="DataGridIncassiNonAttribuiti" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" Width="100%" BorderColor="Gray" BorderWidth="1px"
                CellSpacing="1" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False">
                <AlternatingItemStyle BackColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_INCASSO" HeaderText="DATA INCASSO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CAUSALE" HeaderText="CAUSALE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_TIPO_PAG" HeaderText="ID_TIPO_PAG" 
                        Visible="False"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <ItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 100%">
                    <asp:TextBox runat="server" ID="TextBoxSelezionato" BackColor="Transparent" BorderColor="Transparent"
                        BorderWidth="0px" Font-Bold="True" Font-Names="Arial" Width="100%" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%; text-align: right">
                    <asp:ImageButton ID="ImageButtonProcedi" runat="server" AlternateText="Procedi" ToolTip="Procedi"
                        ImageUrl="../../NuoveImm/Img_Procedi.png" Visible="False" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="IdIncassoNonAttribuito" runat="server" />
    <asp:HiddenField ID="ImportoIncassoNonAttribuito" runat="server" />
    <asp:HiddenField ID="idTipoPag" runat="server" Value="0" />

    </form>
</body>
</html>
