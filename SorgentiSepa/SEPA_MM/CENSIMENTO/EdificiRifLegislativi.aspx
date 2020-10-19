<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EdificiRifLegislativi.aspx.vb"
    Inherits="CENSIMENTO_EdificiRifLegislativi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Parametri</title>
    <script type="text/javascript">
        var Selezionato;
        var r = {
            'special': /[\W]/g,
            'codice': /[\W\_]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d]/g,
            'onlynumbers': /[^\d\-\,\.]/g,
            'numbers': /[^\d]/g
        };
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        };
        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                };
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            };
        };
        function AutoDecimal(obj, numdec) {
            if (numdec == null) numdec = 2;
            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(numdec);
                if (a != 'NaN') {
                    if (numdec > 0) {
                        if (a.substring(a.length - (numdec + 1), 0).length >= 4) {
                            var decimali = a.substring(a.length, a.length - numdec);
                            var dascrivere = a.substring(a.length - (numdec + 1), 0);
                            var risultato = '';
                            while (dascrivere.replace('-', '').length >= 4) {
                                risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                                dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                            };
                            risultato = dascrivere + risultato + ',' + decimali;
                            document.getElementById(obj.id).value = risultato;
                        }
                        else {
                            document.getElementById(obj.id).value = a.replace('.', ',');
                        };
                    }
                    else {
                        if (a.substring(a.length - (numdec + 1), 0).length >= 3) {
                            var dascrivere = a.substring(a.length, 0);
                            var risultato = '';
                            while (dascrivere.replace('-', '').length >= 4) {
                                risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                                dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                            };
                            risultato = dascrivere + risultato;
                            document.getElementById(obj.id).value = risultato;
                        }
                        else {
                            document.getElementById(obj.id).value = a.replace('.', ',');
                        };

                    };
                }
                else {
                    document.getElementById(obj.id).value = '';
                };
            };
        };
    </script>
</head>
<body style="width: 790px; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    <form id="form1" runat="server">
    <div>
        <br />
        <asp:Label Text="Parametri" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
            ForeColor="Maroon" />
        <br />
        <br />
    </div>
    <div style="height: 450px;">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div style="height: 400px; width: 95%;">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label1" runat="server" Text="Riferimenti Legislativi Edifici" Font-Bold="True"
                                Font-Names="Arial" Font-Size="10pt"></asp:Label></legend>
                        <table width="100%" cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="width: 85%">
                                    <div style="width: 100%; overflow: auto; height: 300px;">
                                        <asp:DataGrid ID="DataGridIntLegali" runat="server" AutoGenerateColumns="False" BackColor="#F2F5F1"
                                            BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" meta:resourcekey="DataGrid1Resource1"
                                            PageSize="100" Width="100%" BorderColor="Navy" BorderStyle="Solid">
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Mode="NumericPages" Wrap="False" />
                                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                ForeColor="#0000C0" Wrap="False" />
                                            <Columns>
                                                <asp:BoundColumn DataField="COD" HeaderText="CODICE" ReadOnly="True"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ReadOnly="True">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="L_SPECIALE" HeaderText="L. SPECIALE" ReadOnly="True">
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                                <td style="width: 15%; vertical-align: top;">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <%--<img alt="Aggiungi Riferimento Legislativo" onclick="document.getElementById('TextBox1').value='1';myOpacity.toggle();"
                                                    src="../NuoveImm/Img_Aggiungi.png" style="cursor: pointer;" id="IMG1" />--%>
                                                <asp:ImageButton ID="ImgAggiungi" runat="server" ImageUrl="~/NuoveImm/Img_Aggiungi.png"
                                                    ToolTip="Aggiungi Riferimento Legislativo" EnableTheming="True" CausesValidation="False"
                                                    OnClientClick="document.getElementById('TextBox1').value='1';" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%--<asp:ImageButton ID="ImgModifica" OnClientClick="document.getElementById('TextBox1').value='2'"
                                                    runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png" ToolTip="Modifica Filiale"
                                                    EnableTheming="True" CausesValidation="False" />--%>
                                                <asp:ImageButton ID="ImgModifica" runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png"
                                                    ToolTip="Modifica Sede Territoriale" EnableTheming="True" CausesValidation="False"
                                                    OnClientClick="document.getElementById('TextBox1').value='2';" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImgBtnAggiungi" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                                                    ToolTip="Elimina la voce selezionata" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtmia" runat="server" Text="Nessuna Selezione" BackColor="Transparent"
                                        BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" Width="100%"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblErrore" runat="server" Visible="False" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="10pt" ForeColor="Red" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
                <div style="height: 50px; width: 95%;">
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label2" Text="Quota sottosoglia" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="10pt" /></legend>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 50px">
                            <tr>
                                <td style="width: 15%">
                                    <asp:Label ID="Label3" Text="MQ netti" runat="server" Font-Names="Arial" 
                                        Font-Size="9pt" />
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox runat="server" ID="txtSoglia" Text="" Font-Names="Arial" Style="text-align: right"
                                        Font-Size="9pt" Width="70px" />
                                </td>
                                <td style="width: 65%">
                                    <asp:ImageButton ID="btnModificaSoglia" runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png"
                                        ToolTip="Modifica Sede Territoriale" EnableTheming="True" CausesValidation="False"
                                        OnClientClick="document.getElementById('TextBox1').value='2';" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div style="height: 450px;">
                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Label Text="Nuovo Riferimento Legislativo" runat="server" Font-Names="Arial"
                                    Font-Size="10pt" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%">
                                <asp:Label Text="Descrizione" runat="server" Font-Names="Arial" 
                                    Font-Size="9pt" />
                            </td>
                            <td style="width: 85%">
                                <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" Font-Size="10pt"
                                    Width="90%" MaxLength="250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label Text="L. Speciale" runat="server" Font-Names="Arial" 
                                    Font-Size="9pt" />
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbSpeciale" runat="server" Font-Names="Arial" Font-Size="10pt">
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="0">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 200px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                                    <tr>
                                        <td style="text-align: right; width: 85%">
                                            <asp:ImageButton ID="img_InserisciSchema" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                                Style="height: 16px" />
                                        </td>
                                        <td style="text-align: right; width: 15%">
                                            <asp:ImageButton ID="img_ChiudiSchema" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                                 />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:HiddenField ID="txtid" runat="server" />
    <asp:HiddenField ID="TextBox1" runat="server" />
    </form>
</body>
</html>
