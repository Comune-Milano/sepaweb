<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelezionaContrRegistraz.aspx.vb"
    Inherits="Contratti_SelezionaContrRegistraz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prima registrazione</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; ELENCO
                        CONTRATTI PER PRIMA REGISTRAZIONE
                        <asp:Label ID="lblContaContr" runat="server"></asp:Label></strong> </span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTotSelez" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Bold="True"
                        Text="Tot. contratti selezionati:"></asp:Label>&nbsp
                    <asp:Label ID="lblTotSelez2" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Bold="True"
                        Text="0"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:DataGrid ID="datagridContr" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                        Font-Size="8pt" Style="z-index: 121; width: 100%;" HorizontalAlign="Left" TabIndex="1">
                        <PagerStyle Mode="NumericPages" />
                        <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_CONTRATTO" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="ChSelezionato0" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_click" />
                                    <asp:Label ID="lblHeader" runat="server" ForeColor="PapayaWhip" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="9pt" Text="!"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChSelezionato" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelected_click" />
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="White" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="9pt" Text="!"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="TIPO_RAPPORTO" HeaderText="TIPOLOGIA CONTRATTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="STATO" HeaderText="STATO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_AE" HeaderText="DATA DECORRENZA AE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VERSAMENTO_TR" HeaderText="MODALITA PAGAMENTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:ImageButton runat="server" ID="ImgSalva" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        OnClientClick="ConfermaProcedi();" />
                    <img onclick="self.close();" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" style="cursor: pointer;" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="Modificato" runat="server" Value="0" />
    <asp:HiddenField ID="dataInvio" runat="server" />
    <asp:HiddenField ID="codUfficioReg" runat="server" />
    <asp:HiddenField ID="controlloSelezione" runat="server" Value="0" />
    <asp:HiddenField ID="confermaProcedi" runat="server" Value="0" />
    </form>
</body>
<script type="text/javascript">
    if (document.getElementById('dvvvPre')) {
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    };
    function ConfermaProcedi() {
        chiediConferma = window.confirm("Attenzione...procedendo verrà creato un file XML valido per la registrazione telematica dei contratti. Continuare?");
        if (chiediConferma == false) {
            document.getElementById('confermaProcedi').value = '0';
        }
        else {
            document.getElementById('confermaProcedi').value = '1';
        }

    };
</script>
</html>
