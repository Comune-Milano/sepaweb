<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DetMorMAVInq.aspx.vb" Inherits="Condomini_DetMorMAVInq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Dettagli Morosità MAV</title>

    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <script type="text/javascript" language="javascript">
        window.name = "modal";

        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                event.keyCode = 0;
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

        };

        function CompletaData(e, obj) {
            var sKeyPressed;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
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
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }


        function AutoDecimal2(obj) {

            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a != 'NaN') {
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
                else
                    document.getElementById(obj.id).value = ''
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server" target="modal">
    <div>
        <table>
            <tr>
                <td>
                    <div style="overflow: auto; width: 500px; height: 330px;" id="DivMavInquilino">
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                            <asp:DataGrid ID="DataGridDettMorMav" runat="server" AutoGenerateColumns="False"
                                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 105;
                                left: 193px; top: 54px" Width="99%" TabIndex="10" BorderColor="#000033" BorderWidth="1px"
                                CellPadding="1" CellSpacing="1">
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle ForeColor="Black" />
                                <HeaderStyle BackColor="WhiteSmoke" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#0000C0" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="NUM BOLLETTINO">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BOLLETTINO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                            Wrap="False" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="DATA NOTIFICA">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDataNotifica" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="10"
                                                Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_NOTIFICA_COMUNE") %>'
                                                Width="80px"></asp:TextBox>
                                            <asp:Label ID="LabelDataNotifica" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="20%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="IMPORTO">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="40%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                            Wrap="False" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </span></strong>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; left: 9px; top: 222px" Visible="False" Width="99%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                <asp:ImageButton ID="btnSalvaDataNotifica" align="Left" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                TabIndex="20" ToolTip="Salva" Style="height: 16px" 
                        Visible="False" />
                    <asp:ImageButton ID="btnAnnulla" align="Right" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                                TabIndex="21" ToolTip="Esci dalla finestra" Style="height: 16px" />
                    </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
