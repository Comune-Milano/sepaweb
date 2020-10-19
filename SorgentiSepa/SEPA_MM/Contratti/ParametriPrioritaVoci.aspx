<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametriPrioritaVoci.aspx.vb"
    Inherits="Contratti_ParametriPrioritaVoci" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rapporti Utenza Sep@Com - Priorità Voci</title>
    <script type="text/javascript">
        var Selezionato;
        var OldColor;
        var SelColo;
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
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 112 || sKeyPressed1 == 115 || sKeyPressed1 == 116 || sKeyPressed1 == 117 || sKeyPressed1 == 118 || sKeyPressed1 == 122 || sKeyPressed1 == 123 || sKeyPressed1 == 60 || sKeyPressed1 == 91 || sKeyPressed1 == 92) {
                sKeyPressed1 = 0;
                e.preventDefault();
                e.stopPropagation();
            };
            if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
                if (sKeyPressed1 == 8 || sKeyPressed1 == 13) {
                    sKeyPressed1 = 0;
                    e.preventDefault();
                    e.stopPropagation();
                }
            }
            else {
                if (document.activeElement.isMultiLine == false) {
                    if (sKeyPressed1 == 13) {
                        sKeyPressed1 = 0;
                        e.preventDefault();
                        e.stopPropagation();
                    };
                };
            };
            var alt = window.event.altKey;
            if (alt && sKeyPressed1 == 115) {
                if (document.getElementById('noClose').value == 1) {
                    if (document.getElementById('btnEsci') != null) {
                        exitClick = 1;
                        document.getElementById('btnEsci').click();
                    }
                    else if (document.getElementById('MainContent_btnEsci') != null) {
                        exitClick = 1;
                        document.getElementById('MainContent_btnEsci').click();
                    };
                    alert('La finestra è stata chiusa in modo anomalo.Tutti i dati non salvati andranno persi');
                };
            };
        };
        function $onkeydown() {
            var alt = window.event.altKey;
            if (alt && event.keyCode == 115) {
                if (document.getElementById('noClose').value == 1) {
                    if (document.getElementById('btnEsci') != null) {
                        exitClick = 1;
                        document.getElementById('btnEsci').click();
                    }
                    else if (document.getElementById('MainContent_btnEsci') != null) {
                        exitClick = 1;
                        document.getElementById('MainContent_btnEsci').click();
                    };
                    alert('La finestra è stata chiusa in modo anomalo.Tutti i dati non salvati andranno persi');
                };
            };
            if (event.keyCode == 112 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 122 || event.keyCode == 123 || event.keyCode == 60 || event.keyCode == 91 || event.keyCode == 92) {
                event.keyCode = 0;
                return false;
            };
            if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
                if (event.keyCode == 8 || event.keyCode == 13) {
                    event.keyCode = 0;
                    return false;
                }
            }
            else {
                if (document.activeElement.isMultiLine == false) {
                    if (event.keyCode == 13) {
                        event.keyCode = 0;
                        return false;
                    };
                };
            };
        };
        if (navigator.appName == 'Microsoft Internet Explorer') {
            window.document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        };
        function controllaPriorita(idText) {
            var valore = parseInt(document.getElementById(idText).value);
            var valoreMin = document.getElementById('HFMinPriorita').value;
            var valoreMax = document.getElementById('HFMaxPriorita').value;
            if (!valore) {
                alert('La priorità non può essere vuota!');
                document.getElementById(idText).value = valoreMax;
            } else {
                if (valore < valoreMin) {
                    alert('Il range delle priorità e da ' + valoreMin + ' a ' + valoreMax + '!');
                    document.getElementById(idText).value = valoreMin;
                };
                if (valore > valoreMax) {
                    alert('Il range delle priorità e da ' + valoreMin + ' a ' + valoreMax + '!');
                    document.getElementById(idText).value = valoreMax;
                };
            };
        };
        function selectall(txtselezione) {
            document.getElementById(txtselezione).select();
        };
    </script>
    <style type="text/css">
        .bottone
        {
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 1px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 22px;
            cursor: pointer;
            padding-left: 3px;
        }
        .bottone:hover
        {
            background-color: #FFF5D3;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 1px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 22px;
            cursor: pointer;
            padding-left: 3px;
        }
    </style>
</head>
<body style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="font-family: Arial; font-size: 14pt; color: #801F1C; font-weight: bold;
                height: 40px;" colspan="2">
                Gestione Priorit&agrave; Voci
            </td>
        </tr>
        <tr>
            <td style="width: 97%;">
                <div style="width: 97%; height: 440px; overflow: auto;">
                    <asp:DataGrid ID="dgvVoci" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                        GridLines="None" PageSize="100" Width="97%" AllowPaging="True">
                        <ItemStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                            Position="TopAndBottom" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="left" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PRIORIT&Agrave;">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPriorita" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PRIORITA") %>'
                                        Width="75px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </div>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 97%;">
                    <tr>
                        <td style="width: 90%;">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnAggiorna" runat="server" Text="Aggiorna" ToolTip="Aggiorna Voci"
                                CssClass="bottone" />
                        </td>
                        <td>
                            <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci dalla Gestione"
                                CssClass="bottone" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HFMinPriorita" runat="server" Value="0" />
    <asp:HiddenField ID="HFMaxPriorita" runat="server" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
