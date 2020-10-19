<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompetenzaAnnoModale.aspx.vb"
    Inherits="Contabilita_Report_CompetenzaAnnoModale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Getione Competenza Voci</title>
    <base target="_self" />
    <script type="text/javascript">
        window.name = "modal";
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
        function elimina(id) {
            var chiediConferma = window.confirm('Sei sicuro di voler eliminare questa competenza?');
            if (chiediConferma) {
                document.getElementById('IDelimina').value = id;
            } else {
                document.getElementById('IDelimina').value = 0;
            }
            document.getElementById('btnElimina').click();
        }
        function ConfermaEsci() {
            if (document.getElementById('HiddenFieldConteggio').value == 0) {
                var chiediConf = window.confirm('Non è presente nessuna competenza per\nla voce selezionata! Uscire lo stesso?');
                if (chiediConf) {
                    self.close();
                }
            }
        }
    </script>
</head>
<body style="background: #ffffff url('../../NuoveImm/SfondoMaschere1.jpg') no-repeat;">
    <form id="form1" runat="server" target="modal">
    <div style="position: absolute; top: 20px; left: 10px; width: 95%; height: 320px;
        overflow: auto;">
        <asp:Label ID="lblComp" Text="" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="12pt" ForeColor="Maroon" />
    </div>
    <div style="position: absolute; top: 50px; left: 10px; width: 95%; height: 320px;
        overflow: auto;">
        <asp:DataGrid runat="server" ID="DataGridVoci" AutoGenerateColumns="False" CellPadding="1"
            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
            Width="100%" ShowFooter="True">
            <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="id" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMPETENZA" HeaderText="COMPETENZA"></asp:BoundColumn>
                <asp:BoundColumn DataField="INIZIO" HeaderText="INIZIO VALIDITA'"></asp:BoundColumn>
                <asp:BoundColumn DataField="FINE" HeaderText="FINE VALIDITA'" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="ELIMINA" HeaderText="ELIMINA" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="ELIMINA_IMM" HeaderText="" ItemStyle-HorizontalAlign="Center">
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
        <asp:Label ID="nessuna" Text="Nessuna competenza trovata" runat="server" Font-Names="Arial"
            Font-Size="9pt" />
    </div>
    <div style="position: absolute; top: 375px; left: 10px; width: 95%;">
        <table border="0" cellpadding="2" cellspacing="2" width="90%">
            <tr>
                <td>
                    <asp:Label Text="Competenza" runat="server" Font-Names="Arial" Font-Size="9pt" />
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCompetenza">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" Text="Inizio validità" runat="server" Font-Names="Arial" Font-Size="9pt" />
                </td>
                <td>
                    <asp:TextBox runat="server" Width="70px" ID="dataDal" /><asp:RegularExpressionValidator
                        ID="RegularExpressionValidatorEmissioneAl" runat="server" ErrorMessage="!" ControlToValidate="dataDal"
                        Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
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
                <td>
                </td>
                <td style="text-align: right">
                    <asp:ImageButton ID="ImageButtonAggiungi" runat="server" ImageUrl="../../NuoveImm/NewAggiungi.png"
                        ToolTip="Aggiungi" />
                    <asp:ImageButton ID="ImageButtonEsci" runat="server" ImageUrl="../../NuoveImm/newEsci.png"
                        OnClientClick="ConfermaEsci();"
                        CausesValidation="False" ToolTip="Esci" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField Value="0" runat="server" ID="IDelimina" />
    <asp:HiddenField Value="0" runat="server" ID="HiddenFieldConteggio" />
    <asp:Button runat="server" value="elimina" ID="btnElimina" Style="visibility: hidden" />
    </form>
</body>
</html>
