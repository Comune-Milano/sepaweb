<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompetenzaAnno.aspx.vb"
    Inherits="Contabilita_Report_CompetenzaAnno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Getione Competenza Voci</title>
    <script type="text/javascript">
        var Selezionato;
        var OldColor;
        var SelColo;

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbersOnlyPositive': /[^\d\,]/g, //Modifica Marco 20/09/2012
            'notnumbers': /[^\d\,-]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            return true;
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
                }
                else if (obj.value.length > 9) {
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
        function ApriVoce() {
            document.getElementById('ImageButtonVisualizza').click();
        }
        function ApriModale() {
            if (document.getElementById('HiddenFieldSelezionato').value == '') {
                alert('Non hai selezionato nessuna voce!');
            } else {
                window.showModalDialog('CompetenzaAnnoModale.aspx?ID=' + document.getElementById('HiddenFieldSelezionato').value, 'CompetenzaAnnoM', 'top:0;left:0;status:no;dialogWidth:675px;dialogHeight:545px;dialogHide:true;help:no;scroll:no');
            }
        }
    </script>
</head>
<body style="background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);">
    <form id="form1" runat="server">
    <div>
        <div style="height: 10px;">
        </div>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="Label1" Text="Gestione competenza voci" runat="server" Font-Bold="True"
                        Font-Names="Arial" Font-Size="14pt" ForeColor="Maroon" />
                </td>
            </tr>
        </table>
        <div style="height: 15px;">
        </div>
        <div style="height: 400px; overflow: auto; vertical-align: top">
            <asp:DataGrid runat="server" ID="DataGridVoci" AutoGenerateColumns="False" CellPadding="1"
                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                Width="97%" ShowFooter="True">
                <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
                <Columns>
                    <asp:BoundColumn DataField="id" HeaderText="id" Visible="false">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="descrizione" HeaderText="VOCE">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="#999999" />
                <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                    Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                    HorizontalAlign="Center" />
                <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
                <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
            </asp:DataGrid>
        </div>
        <br />
        <asp:TextBox Text="Nessuna Selezione" runat="server" ID="txtSelezionato" BackColor="Transparent" BorderStyle="None"
            Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Width="100%" />
        <asp:HiddenField Value="" runat="server" ID="HiddenFieldSelezionato" />
        <br />
        <br />
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 80%">
                </td>
                <td style="width: 10%">
                    <asp:ImageButton ID="ImageButtonVisualizza" runat="server" ToolTip="Visualizza" ImageUrl="../../NuoveImm/Img_Visualizza.png"
                        Style="height: 20px" OnClientClick="ApriModale();" />
                </td>
                <td style="width: 10%">
                    <asp:ImageButton ID="ImageButtonEsci" runat="server" ToolTip="Esci" ImageUrl="../../NuoveImm/newEsci.png"
                        Style="height: 20px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
