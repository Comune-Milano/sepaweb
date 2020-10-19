<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TrasferimBollette.aspx.vb"
    Inherits="Contratti_TrasferimBollette" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trasferimento Bollette</title>
    <style type="text/css">
        .style1
        {
            font-family: ARIAL;
            font-weight: bold;
            font-size: 12pt;
            color: black;
            text-align: center;
        }
        .style2
        {
            vertical-align: top;
        }
        .style3
        {
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #AA3700;
            font-family: Arial;
            font-weight: bold;
            color: white;
            background-color: #AA3700;
            font-size: 8pt;
            text-align: center;
        }
    </style>
    <script type="text/javascript" language="javascript">
        window.name = "modal";
    </script>
</head>
<body>
    <form id="form1" runat="server" target="modal">
    <table width="100%">
        <tr>
            <td class="style1">
                ELENCO BOLLETTE TRASFERIBILI
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                <asp:RadioButtonList ID="rdbBoll" runat="server" AutoPostBack="True" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" RepeatDirection="Horizontal">
                    <asp:ListItem Value="N" Selected="True">DA PAGARE</asp:ListItem>
                    <asp:ListItem Value="P">PAGATE</asp:ListItem>
                    <asp:ListItem Value="T">TUTTE</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                <img alt="selezione" src="Immagini/Img_FrecciaGiu.png" width="20px" /><asp:Label
                    ID="lblIntest" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                    ForeColor="#990000" CssClass="style2"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="97%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblNumBoll" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Bold="True"
                                ForeColor="#333399"></asp:Label>
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
                <div style="width: 910px; height: 320px; overflow: auto;">
                    <asp:DataGrid ID="DgvBolTrasferim" runat="server" AllowSorting="True" AutoGenerateColumns="False"
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
                                    <asp:CheckBox ID="CheckAll" OnCheckedChanged="chkAll_click" runat="server" AutoPostBack="True" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkSelected" runat="server" TextAlign="Left" />
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
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Image ID="imgAlert" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" />
                            &nbsp;<asp:Label ID="lblErr" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="black"
                                Font-Bold="True" Font-Italic="true" Text="La funzione può essere usata solo se il contratto è stato chiuso e la bozza ad esso collegata è stata attivata!"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../NuoveImm/Img_Procedi.png"
                                ToolTip="Conferma la selezione, e procedi con il trasferimento" OnClientClick="ConfermaProcedi();" />
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
    <asp:HiddenField ID="codContrAbus" runat="server" Value="0" />
    <asp:HiddenField ID="idRUA" runat="server" Value="0" />
    <asp:HiddenField ID="confermaStorno" runat="server" Value="0" />
    <asp:HiddenField ID="tipoSelezione" runat="server" Value="" />
    <asp:HiddenField ID="numSelezionati" runat="server" Value="0" />
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
                if (document.getElementById('tipoSelezione').value == 'P') {
                    chiediConferma = window.confirm("Attenzione...Per le bollette selezionate saranno creati documenti di storno negativo nel RU regolare e per gli importi pagati si genererà un credito gestionale nel nuovo RU. Continuare?");
                } else {
                    chiediConferma = window.confirm("Attenzione...Per le bollette selezionate saranno creati documenti di storno negativo nel RU regolare e scritture positive nella part. contabile del nuovo RU. Continuare?");
                }

                if (chiediConferma == false) {
                    document.getElementById('confermaStorno').value = '0';
                } else {
                    document.getElementById('confermaStorno').value = '1';
                }
            }
            else {
                alert('Impossibile procedere. Selezionare almeno un documento!')
            }
        }
    </script>
</body>
</html>
