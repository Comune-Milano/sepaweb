<%@ Page Language="VB" AutoEventWireup="false" CodeFile="elencoComuniSUA.aspx.vb"
    Inherits="AMMSEPA_OperatoreSUA_elencoComuniSUA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elenco Comuni</title>
    <script type="text/javascript" src="../../Contratti/prototype.lite.js"></script>
    <script type="text/javascript" src="../../Contratti/moo.fx.js"></script>
    <script type="text/javascript" src="../../Contratti/moo.fx.pack.js"></script>
    <script type="text/javascript">
        var Selezionato;
        function cerca() {
            if (document.all) {
                finestra = showModelessDialog('../Find.htm', window, 'dialogLeft:0px;dialogTop:0px;dialogWidth:385px; dialogHeight:165px; scroll:no; status:no; help:no;');
                finestra.focus
                finestra.document.close()
            }
            else if (document.getElementById) {
                self.find()
            }
            else window.alert('Il tuo browser non supporta questo metodo')
        }
        function modifica70KM() {
            if (document.getElementById('txtid').value != -1) {
                window.showModalDialog('Mod70KMSUA.aspx?id=1&txtid=' + document.getElementById('txtid').value, window, 'status:no;dialogWidth:500px;dialogHeight:350px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');
            } else {
                alert('Non è stato selezionato nessun Comune');
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
        }
        .style2
        {
            font-family: Arial;
            font-size: x-small;
        }
    </style>
</head>
<body style="background-color: #f2f5f1">
<div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('../../NuoveImm/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="../../NuoveImm/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center" class="style2">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Elenco Comuni" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="../Immagini/SfondoHome.jpg" height="75px" width="100%" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 80%">
                                            <div style="width: 100%; overflow: auto; height: 512px;">
                                                <asp:DataGrid ID="DataGridIntLegali" runat="server" AutoGenerateColumns="False" BackColor="#F2F5F1"
                                                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" meta:resourcekey="DataGrid1Resource1"
                                                    PageSize="28" AllowPaging="True" Width="100%" BorderColor="Navy" 
                                                    BorderStyle="Solid">
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
                                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SIGLA" HeaderText="PROVINCIA"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="CAP" HeaderText="CAP"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="COD" HeaderText="COD.CATASTALE"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ENTRO" 
                                                            HeaderText="ENTRO 70KM DA MILANO (V.Normativa)">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DISTANZA_KM" 
                                                            HeaderText="DISTANZA IN KM DA MILANO (N.Normativa)"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="POPOLAZIONE" HeaderText="POPOLAZIONE">
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                        <td style="width: 20%; vertical-align: top;">
                                            <table class="style1">
                                                <tr>
                                                    <td class="style5">
                                                        <asp:Label ID="Label2" runat="server" Text="Filtra per Provincia" Font-Names="Arial"
                                                            Font-Size="10pt"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                            Font-Size="10pt">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4" colspan="2">
                                                        <img alt="Cerca" onclick="cerca();" src="../../Condomini/Immagini/Search_16x16.png"
                                                            id="IMG1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4" colspan="2">
                                                        <asp:ImageButton ID="btnApri70KM" runat="server" CausesValidation="False" ImageUrl="../../Condomini/Immagini/pencil-icon.png"
                                                            OnClientClick="modifica70KM();" ToolTip="Modifica 'Entro 70KM da Milano' selezionato"
                                                            Visible="False" Height="16px" Width="17px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4" colspan="2">
                                                        &nbsp; &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style4" colspan="2">
                                                        <asp:ImageButton ID="btnExport" runat="server" CausesValidation="False" 
                                                            ImageUrl="~/Condomini/Immagini/Excel-icon.png" ToolTip="Export griglia" 
                                                            Height="16px" Width="17px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 50%">
                                            &nbsp;
                                            <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Red" Height="16px" Visible="False" Width="525px"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField ID="txtid" runat="server" Value="-1" />

    <script type="text/javascript" language="javascript">
        initialize();
        function initialize() {
            window.focus();
            document.getElementById('caricamento').style.visibility = 'hidden';
        };
    </script>
    </form>
</body>
</html>
