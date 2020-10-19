<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDebitoriMultiSelezioneMor.aspx.vb"
    Inherits="MOROSITA_RicercaDebitoriMultiSelezioneMor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca</title>
    <script type="text/javascript">
        function ScrollPosFiliali(obj) {
            document.getElementById('yPosFiliali').value = obj.scrollTop;
        }
        function ScrollPosQuartieri(obj) {
            document.getElementById('yPosQuartieri').value = obj.scrollTop;
        }
        function ScrollPosComplessi(obj) {
            document.getElementById('yPosComplessi').value = obj.scrollTop;
        }
        function ScrollPosEdifici(obj) {
            document.getElementById('yPosEdifici').value = obj.scrollTop;
        }
        function ScrollPosIndirizzi(obj) {
            document.getElementById('yPosIndirizzi').value = obj.scrollTop;
        }
        function ScrollPosContrattoSpecifico(obj) {
            document.getElementById('yPosContrattoSpecifico').value = obj.scrollTop;
        }
        function ScrollPosTipologiaUI(obj) {
            document.getElementById('yPosTipologiaUI').value = obj.scrollTop;
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
                    document.getElementById(obj.id).value = risultato
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;
                z-index: 500">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="Immagini/load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                            <tr>
                                <td colspan="5" style="height: 45px; color: #FFFFFF; font-size: 18.0pt; font-weight: 700;
                                    font-style: normal; text-decoration: none; font-family: Arial;
                                    text-align: center; vertical-align: middle; white-space: normal; border-left: 1.0pt solid windowtext;
                                    border-right-style: none; border-right-color: inherit; border-right-width: medium;
                                    border-top: 1.0pt solid windowtext; border-bottom-style: none; border-bottom-color: inherit;
                                    border-bottom-width: medium; padding-left: 1px; padding-right: 1px; padding-top: 1px;
                                    background: #507CD1;">
                                    RICERCA MESSE IN MORA
                                </td>
                            </tr>
                            <tr>
                            <td>&nbsp;
                            </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="SEDE T." runat="server" ID="CheckBoxFiliali" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="QUARTIERI" runat="server" ID="CheckBoxQuartieri" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="COMPLESSI" runat="server" ID="CheckBoxComplessi" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="EDIFICI" runat="server" ID="CheckBoxEdifici" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="INDIRIZZO" runat="server" ID="CheckBoxIndirizzi" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelFiliali" runat="server" Style="border: 1px solid #507CD1; height: 100px;
                                        overflow: auto" onscroll="ScrollPosFiliali(this);">
                                        <asp:CheckBoxList ID="CheckBoxListFiliali" runat="server" Font-Names="Arial" Font-Size="7pt"
                                            Width="90%" AutoPostBack="True">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelQuartieri" runat="server" Style="border: 1px solid #507CD1; height: 100px;
                                        overflow: auto" onscroll="ScrollPosQuartieri(this);">
                                        <asp:CheckBoxList ID="CheckBoxListQuartieri" runat="server" Font-Names="Arial" Font-Size="7pt"
                                            Width="90%" AutoPostBack="True">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelComplessi" runat="server" Style="border: 1px solid #507CD1; height: 100px;
                                        overflow: auto" onscroll="ScrollPosComplessi(this);">
                                        <asp:CheckBoxList ID="CheckBoxListComplessi" runat="server" Font-Names="Arial" Font-Size="7pt"
                                            Width="90%" AutoPostBack="True">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelEdifici" runat="server" Style="border: 1px solid #507CD1; height: 100px;
                                        overflow: auto" onscroll="ScrollPosEdifici(this);">
                                        <asp:CheckBoxList ID="CheckBoxListEdifici" runat="server" Font-Names="Arial" Font-Size="7pt"
                                            Width="90%" AutoPostBack="True">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelIndirizzi" runat="server" Style="border: 1px solid #507CD1; height: 100px;
                                        overflow: auto" onscroll="ScrollPosIndirizzi(this);">
                                        <asp:CheckBoxList ID="CheckBoxListIndirizzi" runat="server" Font-Names="Arial" Font-Size="7pt"
                                            Width="90%">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="TIPOLOGIA RAPPORTO" runat="server" ID="CheckBoxTipologiaRapporto"
                                        BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White"
                                        Width="100%" AutoPostBack="True" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="CONTRATTO SPECIFICO" runat="server" ID="CheckBoxContrattoSpecifico"
                                        BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White"
                                        Width="100%" AutoPostBack="True" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="TIPOLOGIA UI" runat="server" ID="CheckBoxTipologiaUI" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="STATO CONTRATTO" runat="server" ID="CheckBoxStatoContratto" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Left;">
                                    <asp:CheckBox Text="AREA CANONE" runat="server" ID="CheckBoxAreaCanone" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                        AutoPostBack="True" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelTipologiaRapporti" runat="server" Style="border: 1px solid #507CD1;
                                        height: 150px; overflow: auto">
                                        <asp:CheckBoxList ID="CheckBoxListTipologiaRapporti" runat="server" Font-Names="Arial"
                                            Font-Size="7pt" Width="90%" AutoPostBack="True">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelContrattoSpecifico" runat="server" Style="border: 1px solid #507CD1;
                                        height: 150px; overflow: auto" onscroll="ScrollPosContrattoSpecifico(this);">
                                        <asp:CheckBoxList ID="CheckBoxListContrattoSpecifico" runat="server" Font-Names="Arial"
                                            Font-Size="7pt" Width="90%">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelTipologiaUI" runat="server" Style="border: 1px solid #507CD1;
                                        height: 150px; overflow: auto" onscroll="ScrollPosTipologiaUI(this);">
                                        <asp:CheckBoxList ID="CheckBoxListTipologiaUI" runat="server" Font-Names="Arial"
                                            Font-Size="7pt" Width="90%">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelStatoContratto" runat="server" Style="border: 1px solid #507CD1;
                                        height: 150px; overflow: auto">
                                        <asp:CheckBoxList ID="CheckBoxListStatoContratto" runat="server" Font-Names="Arial"
                                            Font-Size="7pt" Width="90%">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td style="width: 20%">
                                    <asp:Panel ID="PanelAreaCanone" runat="server" Style="border: 1px solid #507CD1;
                                        height: 150px; overflow: auto">
                                        <asp:CheckBoxList ID="CheckBoxListAreaCanone" runat="server" Font-Names="Arial" Font-Size="7pt"
                                            Width="90%">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Label ID="Label2" Text="MESSA IN MORA SE:" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="10pt" />
                                </td>
                                <td colspan="2">
                                    <asp:CheckBoxList ID="CheckBoxListMora" runat="server" BorderColor="Black" Font-Names="Arial"
                                        Font-Size="7pt" ForeColor="Black" RepeatLayout="Flow" Style="border-right: blue 1px double;
                                        border-top: blue 1px double; border-left: blue 1px double; border-bottom: blue 1px double"
                                        TabIndex="16" Width="95%">
                                        <asp:ListItem Selected="True" Value="AB">DISPONIBILE  IL SALDO AL 30.9.2009</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="CD">MANCA IL SALDO AL 30.9.2009</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="EF">DISPONIBILE  IL SALDO AL 30.9.2009 E SENZA DEBITO SUCCESSIVO</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td>
                                    <asp:CheckBox Text="PROTOCOLLO GESTORE" runat="server" ID="CheckBoxProtocollo" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                        AutoPostBack="True" />
                                    <asp:Panel ID="PanelStatoContratto0" runat="server" Style="border: 1px solid #507CD1;
                                        height: 150px; overflow: auto">
                                        <asp:CheckBoxList ID="CheckBoxListProtocollo" runat="server" Font-Names="Arial" Font-Size="7pt"
                                            Width="90%">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                    <asp:Label Text="DATA PROTOCOLLO" runat="server" ID="Label15" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                    <asp:Label Text="STIPULA CONTRATTO" runat="server" ID="Label3" BackColor="#507CD1"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                    <asp:Label Text="ORDINAMENTO" runat="server" ID="Label13" BackColor="#507CD1" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%" />
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                </td>
                                <td style="width: 20%; vertical-align: middle; text-align: Center;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #507CD1">
                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label16" Text="DAL" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="protocolloDal" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label17" Text="AL" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="protocolloAl" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border: 1px solid #507CD1">
                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label11" Text="DAL" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="stipulaDal" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label12" Text="AL" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="10pt" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:TextBox ID="stipulaAl" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="border: 1px solid #507CD1">
                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DropDownListOrdinamento" runat="server" Font-Names="arial"
                                                    Font-Size="10pt" Height="20px" Width="100%" Font-Bold="True">
                                                    <asp:ListItem Value="DEBITO DESC">DEBITO DECRESCENTE</asp:ListItem>
                                                    <asp:ListItem Value="DEBITO ASC">DEBITO CRESCENTE</asp:ListItem>
                                                    <asp:ListItem Value="INTESTATARIO2">INTESTATARIO</asp:ListItem>
                                                    <asp:ListItem Value="COMUNE_UNITA,INDIRIZZO,CIVICO">COMUNE, INDIRIZZO, CIVICO</asp:ListItem>
                                                    <asp:ListItem Value="RAPPORTI_UTENZA.COD_CONTRATTO">CODICE CONTRATTO</asp:ListItem>
                                                    <asp:ListItem Value="UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE">CODICE UNITA</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="text-align: center; vertical-align: middle">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label14" Text="Estrazione dati al" runat="server" Font-Names="Arial"
                                                    Font-Size="9pt" Font-Bold="True" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="dataEstrazione" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="text-align: center">
                                    <asp:Button ID="ButtonRiepilogoGenerale" Text="STAMPA RIEPILOGO GENERALE" runat="server"
                                        Width="85%" Style="cursor: pointer; background-color: #507CD1; border: 1px solid #507CD1;
                                        color: White; font-size: 7pt" Height="30px" />
                                </td>
                                <td style="text-align: center">
                                    <asp:Button ID="ButtonDettaglioInquilini" Text="STAMPA DETT. INQUILINI" runat="server"
                                        Width="85%" Style="cursor: pointer; background-color: #507CD1; border: 1px solid #507CD1;
                                        color: White; font-size: 7pt" Height="30px" />
                                </td>
                                <td style="text-align: center">
                                    &nbsp;
                                    <asp:Button ID="ButtonRiepilogoBollette" Text="STAMPA DETT. PROTOCOLLI" runat="server"
                                        Width="85%" Style="cursor: pointer; background-color: #507CD1; border: 1px solid #507CD1;
                                        color: White; font-size: 7pt" Height="30px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 100%">
                                    <asp:Label runat="server" ID="Nintestatari" Font-Bold="True" Font-Names="Arial" Text=""
                                        Font-Size="9pt" ForeColor="Maroon"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <asp:Panel ID="PanelInquilini" runat="server" Style="height: 600px; overflow: auto">
                                        <table border="0" cellpadding="0" cellspacing="0" width="98%">
                                            <tr>
                                                <td>
                                                    <asp:DataGrid runat="server" ID="DataGridInquilini" AutoGenerateColumns="False" CellPadding="1"
                                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                                                        Width="100%" AllowPaging="True" PageSize="300" Style="border: 1px solid #507CD1;">
                                                        <AlternatingItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CODICE" Visible="False"></asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="CODICE" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CheckBox1" Checked="true" runat="server" />
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>&nbsp;
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="15%"></HeaderStyle>
                                                                <ItemStyle Width="15%"></ItemStyle>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="INTESTATARIO2" HeaderText="INTESTATARIO" HeaderStyle-Width="15%"
                                                                ItemStyle-Width="15%">
                                                                <HeaderStyle Width="15%"></HeaderStyle>
                                                                <ItemStyle Width="15%"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DEBITO2" HeaderText="DEBITO" ItemStyle-HorizontalAlign="Right"
                                                                HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="COD_TIPOLOGIA_CONTR_LOC" HeaderText="TIPO" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="POSIZIONE_CONTRATTO" HeaderText="POSIZIONE" HeaderStyle-Width="10%"
                                                                ItemStyle-Width="10%">
                                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                                <ItemStyle Width="10%"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD.UNITA'" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="COD_TIPOLOGIA" HeaderText="TIPO UN." ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" HeaderStyle-Width="10%"
                                                                ItemStyle-Width="10%">
                                                                <HeaderStyle Width="10%"></HeaderStyle>
                                                                <ItemStyle Width="10%"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="CIVICO" HeaderText="CIV." ItemStyle-HorizontalAlign="Right"
                                                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Right" Width="5%"></ItemStyle>
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="COMUNE_UNITA" HeaderText="COMUNE" HeaderStyle-Width="15%"
                                                                ItemStyle-Width="15%">
                                                                <HeaderStyle Width="15%"></HeaderStyle>
                                                                <ItemStyle Width="15%"></ItemStyle>
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                        <EditItemStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                                                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                                                            HorizontalAlign="Center" />
                                                        <ItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                                                        <PagerStyle BackColor="White" ForeColor="#507CD1" HorizontalAlign="Left" Mode="NumericPages"
                                                            Position="Top" />
                                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%; text-align: right">
                                    <br />
                                     <asp:Button ID="ButtonNuovaRicerca" Text="NUOVA RICERCA"
                                        runat="server" Width="15%" Style="cursor:pointer;background-color: #507CD1; border: 1px solid #507CD1;
                                        color: White; font-size: 7pt" Height="30px" />
                                    <asp:Button ID="ButtonProcediDettaglioInquilini" Text="STAMPA DETT. INQUILINI" runat="server"
                                        Width="15%" Style="cursor: pointer; background-color: #507CD1; border: 1px solid #507CD1;
                                        color: White; font-size: 7pt" Height="30px" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
                <asp:HiddenField ID="yPosFiliali" runat="server" Value="0" />
                <asp:HiddenField ID="yPosQuartieri" runat="server" Value="0" />
                <asp:HiddenField ID="yPosComplessi" runat="server" Value="0" />
                <asp:HiddenField ID="yPosEdifici" runat="server" Value="0" />
                <asp:HiddenField ID="yPosIndirizzi" runat="server" Value="0" />
                <asp:HiddenField ID="yPosContrattoSpecifico" runat="server" Value="0" />
                <asp:HiddenField ID="yPosTipologiaUI" runat="server" Value="0" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
