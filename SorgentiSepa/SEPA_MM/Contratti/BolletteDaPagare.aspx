<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BolletteDaPagare.aspx.vb"
    Inherits="Contratti_BolletteDaPagare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bollette Da Pagare</title>
    <style type="text/css">
        .style1
        {
            font-family: ARIAL;
            font-weight: bold;
            font-size: 12pt;
            color: black;
            text-align: center;
        }
        .style3
        {
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #AA3700;
            font-family: Arial;
            font-weight: bold;
            color: white;
            background-color: #507CD1;
            font-size: 8pt;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td class="style1">
                ELENCO BOLLETTE DA INCASSARE
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="97%">
                    <tr>
                        <td>
                            <asp:Label ID="lblNumBoll" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Bold="True"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCredito" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Bold="True"
                                BackColor="#FFFF66"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                <div style="width: 910px;">
                    <table style="width: 98%;">
                        <tr>
                            <td nowrap="nowrap" class="style3" width="5%">
                                <asp:CheckBox ID="chAll" OnCheckedChanged="chkAll_click" runat="server" AutoPostBack="True"
                                    TextAlign="Left" />
                            </td>
                            <td nowrap="nowrap" class="style3" width="7%">
                                TIPO
                            </td>
                            <td nowrap="nowrap" class="style3" width="10%">
                                PERIODO DAL
                            </td>
                            <td nowrap="nowrap" class="style3" width="10%">
                                PERIODO AL
                            </td>
                            <td nowrap="nowrap" class="style3" width="10%">
                                IMPORTO €
                            </td>
                            <td nowrap="nowrap" class="style3" width="10%">
                                PAGATO €
                            </td>
                            <td nowrap="nowrap" class="style3" width="11%">
                                DATA EMISS.
                            </td>
                            <td nowrap="nowrap" class="style3" width="11%">
                                DATA SCAD.
                            </td>
                            <td nowrap="nowrap" class="style3" width="23%">
                                NOTE
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                <asp:Panel ID="PanelBollette" runat="server" Style="width: 910px; height: 320px;
                    overflow: auto;" onscroll="ScrollPosBoll(this);">
                    <asp:DataGrid ID="DgvBolDaPagare" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BackColor="White" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        PageSize="15" Style="border-collapse: separate" Width="100%" CellPadding="1"
                        CellSpacing="1" ShowHeader="false">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn HeaderText="ID" Visible="False" DataField="ID"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="SELEZIONA">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckAll" OnCheckedChanged="chkAll_click" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkSelected" OnCheckedChanged="chkSelected_click" runat="server"
                                        TextAlign="Left" AutoPostBack="True" />
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="5%" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ACRONIMO" HeaderText="TIPO" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="7%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RIFERIMENTO_DA" HeaderText="PERIODO DAL" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RIFERIMENTO_A" HeaderText="PERIODO AL" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_TOTALE" HeaderText="IMPORTO €" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_PAGATO" HeaderText="PAGATO €" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="11%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="DATA SCADENZA" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="11%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="true" Width="23%" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#AA3700" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="White" Wrap="False" Height="0px" />
                    </asp:DataGrid>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                <tr><td>&nbsp</td></tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTotSelez" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Bold="True"
                                Text="Tot. selezionato da pagare: €. 0" BackColor="#D8D8D8"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblScelta" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Bold="True"
                                Text="&nbsp&nbsp*da trasferire in: " Visible="false"></asp:Label>
                            <asp:RadioButtonList ID="rdbSceltaCredito" runat="server" Font-Names="Arial" Font-Size="10pt"
                                RepeatDirection="Horizontal" Visible="false">
                                <asp:ListItem Value="1" Selected="True">Partita Gestionale</asp:ListItem>
                                <asp:ListItem Value="0">Schema Bolletta</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: right">
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../NuoveImm/Img_Procedi.png"
                                ToolTip="Conferma la selezione, e procedi con il trasferimento" OnClientClick="ConfermaProcedi();document.getElementById('divLoading').style.visibility = 'visible';" />
                            <img id="exit" alt="Esci" src="../NuoveImm/Img_Esci_AMM.png" title="Esci" style="cursor: pointer"
                                onclick="self.close();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Selezionati" runat="server" Value="0" />
    <asp:HiddenField ID="idContratto" runat="server" Value="0" />
    <asp:HiddenField ID="idBolletta" runat="server" Value="0" />
    <asp:HiddenField ID="idGest" runat="server" Value="0" />
    <asp:HiddenField ID="confermaRipart" runat="server" Value="0" />
    <asp:HiddenField ID="numSelezionati" runat="server" Value="0" />
    <asp:HiddenField ID="yPosBoll" runat="server" Value="0" />
    <asp:HiddenField ID="statoContratto" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        if (document.getElementById('divLoading') != null) {
            document.getElementById('divLoading').style.visibility = 'hidden';
        }
        function ConfermaProcedi() {

            var frm = document.forms[0];
            var selezionati = '0';
            var i = 0;

            for (i = 0; i < frm.length; i++) {
                if (frm[i].type == "checkbox") {
                    if (frm.elements[i].checked == true) {
                        selezionati = '1';
                    }
                }
            }

            if (selezionati == '1') {

                chiediConferma = window.confirm("Attenzione...Le bollette selezionate saranno ricoperte per l\'importo a credito che si sta elaborando. Continuare?");

                if (chiediConferma == false) {
                    document.getElementById('confermaRipart').value = '0';
                } else {
                    document.getElementById('confermaRipart').value = '1';
                }
            }
            else {
                alert('Impossibile procedere. Selezionare almeno un documento!')
            }
        }
        function ScrollPosBoll(obj) {
            document.getElementById('yPosBoll').value = obj.scrollTop;
        }
    </script>
</body>
</html>
