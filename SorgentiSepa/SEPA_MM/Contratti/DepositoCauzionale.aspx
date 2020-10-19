<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DepositoCauzionale.aspx.vb"
    Inherits="Contratti_DepositoCauzionale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deposito Cauzionale</title>
</head>
<body style="background-attachment: fixed; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; width: 792px;">
    <form id="form1" runat="server">
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">&nbsp&nbsp<asp:Label
        ID="lblTitolo" runat="server" Text="Scheda Deposito Cauzionale"></asp:Label>
    </span></strong>
    <br />
    <br />
    <table style="width: 775px;">
        <tr>
            <td colspan="5" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="#3366CC"
                    Font-Bold="True">Costituzione Deposito</asp:Label>
            </td>
            <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                &nbsp;
            </td>
            <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Presa Visione MAV"
                    Font-Bold="True" Width="150px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Importo Tot. €"
                    Font-Bold="True" Width="94px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label24" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Emissione"
                    Font-Bold="True" Width="103px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label31" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Costituzione"
                    Font-Bold="True" Width="103px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Valuta MAV"
                    Font-Bold="True" Width="101px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Importo Pagato €"
                    Font-Bold="True" Width="102px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Scadenza"
                    Font-Bold="True" Width="95px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtDataMAV" runat="server" Width="70px" BackColor="White" TabIndex="1"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtImportoTot" runat="server" Width="70px" BackColor="White" TabIndex="3"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtDataEmiss" runat="server" Width="70px" BackColor="White" TabIndex="4"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtDataCostituzione" runat="server" Width="70px" BackColor="White"
                    TabIndex="4" MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"
                    ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtDataValuta" runat="server" Width="70px" BackColor="White" TabIndex="5"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtImpPagato" runat="server" Width="70px" BackColor="White" TabIndex="7"
                    MaxLength="20" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtDataScad" runat="server" Width="70px" BackColor="White" TabIndex="8"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="width: 775px;">
        <tr>
            <td colspan="5" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:Label ID="Label32" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="#3366CC"
                    Font-Bold="True">Restituzione Deposito</asp:Label>
                &nbsp;<asp:Label ID="lblInFase" runat="server" Font-Names="Arial" Font-Size="12pt"
                    Font-Bold="False" Width="125px" ForeColor="#3366CC"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label27" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data CdP"
                    Font-Bold="True" Width="131px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label28" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Num. CdP /Anno CdP"
                    Font-Bold="True" Width="150px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label29" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Rest. Dep. Cauz."
                    Font-Bold="True" Width="124px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label30" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Num./Anno Pagam."
                    Font-Bold="True" Width="117px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label26" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Rimborso Dep.Cauz."
                    Font-Bold="True" Width="119px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtDataCert" runat="server" Width="70px" BackColor="White" TabIndex="3"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtNumCDP" runat="server" Width="70px" BackColor="White" TabIndex="3"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
                /<asp:TextBox ID="txtAnnoCDP" runat="server" Width="70px" BackColor="White" TabIndex="3"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtDataMandato" runat="server" Width="70px" BackColor="White" TabIndex="3"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtNumMandato" runat="server" Width="59px" BackColor="White" TabIndex="3"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
                /<asp:TextBox ID="txtAnnoMandato" runat="server" Width="37px" BackColor="White" TabIndex="3"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtImportoDep" runat="server" Width="70px" BackColor="White" TabIndex="3"
                    MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="#3366CC"
                    Font-Bold="True">Prospetto Interessi Annuali</asp:Label>
                &nbsp;<asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="11pt" ForeColor="#3366CC">Calcolati fino al:</asp:Label>&nbsp;<asp:Label
                    ID="lblDataUltima" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Bold="True"></asp:Label>&nbsp;<asp:Label
                        ID="lblNoInteressi" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Red"
                        Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <div style="overflow: auto; width: 97%; position: relative; height: 89px;" id="DivInteressi">
                    <asp:DataGrid ID="DataGridDepCauz" runat="server" AutoGenerateColumns="False" BackColor="White"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 105;"
                        TabIndex="10" BorderColor="#000033" BorderWidth="1px" CellPadding="0" CellSpacing="0"
                        Width="770px">
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle ForeColor="Black" />
                        <HeaderStyle BackColor="#FFF7D7" ForeColor="#990000" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DAL" HeaderText="DAL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AL" HeaderText="AL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="GIORNI" HeaderText="GIORNI">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TASSO" HeaderText="TASSO %">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO €.">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RESTITUITI" HeaderText="RESTITUITO IN BOLLETTA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: right">
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="11pt">Totale interessi:</asp:Label>&nbsp;<asp:Label
                    ID="lblTotImp" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="#3366CC"
                    Font-Bold="True">Storico Depositi Cauzionali</asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <div style="overflow: auto; width: 770px; position: relative; height: 89px;" id="Div1">
                    <asp:DataGrid ID="DataGridStorico" runat="server" AutoGenerateColumns="False" BackColor="White"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 105;"
                        TabIndex="10" BorderColor="#000033" BorderWidth="1px" Width="1000px">
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle ForeColor="Black" />
                        <HeaderStyle BackColor="#FFF7D7" ForeColor="#990000" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_TOTALE" HeaderText="IMPORTO €.">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUM. BOLLETTA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_PAGAMENTO" HeaderText="DATA COSTITUZIONE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVENIENZA" HeaderText="PROVENIENZA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LIBRO" HeaderText="LIBRO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BOLLA" HeaderText="BOLLA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_RESTITUZIONE" HeaderText="DATA REST."></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_RESTITUZIONE" HeaderText="IMP. REST. €"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RESTITUIBILE" HeaderText="RESTITUIBILE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: left">
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Style="z-index: 104; left: 9px; top: 222px" Visible="False" Width="59%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="5" style="text-align: right">
                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                    TabIndex="11" ToolTip="Esci dalla finestra" OnClientClick="self.close()" Style="z-index: 500" />
            </td>
        </tr>
    </table>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
