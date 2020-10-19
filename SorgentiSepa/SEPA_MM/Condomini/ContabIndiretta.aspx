<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ContabIndiretta.aspx.vb"
    Inherits="Condomini_ContabIndiretta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestione Contabilità Inquilini</title>
    <script language ="javascript" type ="text/javascript" >
        window.name = "modal";
    </script>
</head>
<body style="background-image: url('../CENSIMENTO/IMMCENSIMENTO/Sfondo.png'); background-repeat: repeat-x;">
    <!-- Da mettere subito dopo l'apertura del tag <body> -->
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 95%; height: 95%; visibility: hidden;
        vertical-align: top; line-height: normal; top: 20px; left: 20px; background-color: #FFFFFF;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>
    <form id="form1" runat="server" target ="modal" defaultfocus="btnEsci">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td colspan="3">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                                TabIndex="-1" ToolTip="Salva"
                                OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                                TabIndex="-1" ToolTip="Esci" 
                                OnClientClick="confermaUscita();return false;" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>

            <td style="width: 98%">
                &nbsp;</td>

        </tr>
        <tr>

            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt; font-weight: bold;
                width: 92%;">
                ELENCO INQUILINI
            </td>

        </tr>
        <tr>

            <td style="width: 98%; height: 98%">
                <div style="width: 100%; height: 100%; overflow: auto;" align="center">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="dgvInquilini" runat="server" AutoGenerateColumns="False" CellPadding="1"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                            Width="98%" BorderWidth="1px" BorderColor="#DCDDDE" CellSpacing="1">
                            <ItemStyle BackColor="#EFF3FB" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                Position="Top" VerticalAlign="Top" />
                            <AlternatingItemStyle BackColor="Gainsboro" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_UI" HeaderText="id_ui" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="POSIZIONE_BILANCIO" HeaderText="POS.BILANCIO">
                                    <HeaderStyle Width="8%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO">
                                    <HeaderStyle Width="30%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="GESTIONE">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                            <asp:DataGrid ID="dgvVociInquilino" runat="server" AutoGenerateColumns="False" BorderColor="#DCDDDE"
                                                BorderWidth="1px" CellPadding="1" CellSpacing="1" Font-Bold="False" Font-Italic="False"
                                                Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                                                Font-Underline="False" ForeColor="#333333" Width="95%">
                                                <ItemStyle BackColor="#EFF3FB" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                    Position="Top" VerticalAlign="Top" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="id_ui" HeaderText="id_ui" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_VOCE" HeaderText="ID_VOCE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="VOCE" HeaderText="VOCE">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="PREVENTIVO">
                                                        <EditItemTemplate>
                                                            <asp:TextBox runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtImpPreventivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_PREVENTIVO") %>'></asp:TextBox>
                                                            <asp:Label runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="CONSUNTIVO">
                                                        <EditItemTemplate>
                                                            <asp:TextBox runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                                                <asp:TextBox ID="txtImpConsuntivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    Style="text-align: right" Width="100px" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_CONSUNTIVO") %>'></asp:TextBox>
                                                            </span></strong>
                                                            <asp:Label runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" />
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="IMPORTO_PREVENTIVO" HeaderText="IMPORTO_PREVENTIVO" 
                                                        Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="IMPORTO_CONSUNTIVO" HeaderText="IMPORTO_CONSUNTIVO" 
                                                        Visible="False"></asp:BoundColumn>
                                                </Columns>
                                                <EditItemStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="White" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:DataGrid>
                                        </span></strong>
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                </asp:TemplateColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="White" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>
                    </span></strong>
                </div>
            </td>

        </tr>
    </table>
    <asp:HiddenField ID="idCondominio" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="idGestione" runat="server" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('splash').style.visibility = 'hidden';
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {

                e.preventDefault();
            }
        }
        function $onkeydown() {
            if (event.keyCode == 13) {
                event.keyCode = '9';
            }
        }


        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            //        o.value = o.value.replace('.', ',');
            document.getElementById('txtModificato').value = '1';
            return true;

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
        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {

                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }
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
                        risultato = dascrivere + risultato + ',' + decimali;
                        //document.getElementById(obj.id).value = a.replace('.', ',')
                        document.getElementById(obj.id).value = risultato;
                    }
                    else {
                        document.getElementById(obj.id).value = a.replace('.', ',');
                    }

                }
                else
                    document.getElementById(obj.id).value = '';
            }
        }
        function confermaUscita() {
            document.getElementById('splash').style.visibility = 'visible';
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Continuare l\'operazione senza aver salvato?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '1';
                    document.getElementById('splash').style.visibility = 'hidden';
                }
                else {
                    document.getElementById('txtModificato').value = '0';
                    self.close();
                };
            }
            else {
              self.close();
            };
        };
    </script>
</body>
</html>
