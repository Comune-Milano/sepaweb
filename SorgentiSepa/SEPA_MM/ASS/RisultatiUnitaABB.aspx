<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiUnitaABB.aspx.vb"
    Inherits="Contratti_SelezionaUnita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var popupWindow;
</script>
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self"/>
    <title>Risultati Unità</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 800px;
            position: absolute; top: 0px; background-repeat: no-repeat;">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="lblNumRisultati" runat="server" Font-Bold="True" ForeColor="#801F1C"
                            Font-Size="14pt" Font-Names="Arial"></asp:Label>
                    </strong></span>
                    <br />
                    <br />
                    <br />
                    <br />
                    <div id="contenitore1" style="position: absolute; width: 656px; height: 296px; overflow: scroll;
                        top: 72px; left: 10px;">
                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="8pt" Style="z-index: 121; left: 0px; position: absolute; top: 0px"
                            Width="900px" HorizontalAlign="Left">
                            <PagerStyle Mode="NumericPages" />
                            <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="COD" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROPRIETA" HeaderText="PROPRIETA" ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="CODICE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_ALL" HeaderText="TIPO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NOME_QUARTIERE" HeaderText="QUARTIERE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_VIA" HeaderText="INDIR."></asp:BoundColumn>
                                <asp:BoundColumn DataField="INDIRIZZO" HeaderText=" "></asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_CIVICO" HeaderText="CIV."></asp:BoundColumn>
                                <asp:BoundColumn DataField="N_ALL" HeaderText="NUM."></asp:BoundColumn>
                                <asp:BoundColumn DataField="ZONA" HeaderText="ZONA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_LOCALI" HeaderText="LOC."></asp:BoundColumn>
                                <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NETTA" HeaderText="NETTA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CONV" HeaderText="CONV."></asp:BoundColumn>
                                <asp:BoundColumn DataField="ELEVATORE" HeaderText="ASC."></asp:BoundColumn>
                                <asp:BoundColumn DataField="HANDICAP" HeaderText="HANDICAP"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_DISPONIBILITA1" HeaderText="DISP."></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROPRIETA1" HeaderText="PROPR."></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Cancel" ImageUrl="~/NuoveImm/Abbina_Foto.png"
                                            ToolTip="Dettagli Unità, Foto e Planimetrie" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" CommandName="update" ImageUrl="~/NuoveImm/Abbina_Canone.png"
                                            ToolTip="Calcola il Canone Annuo" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Edit" ImageUrl="~/NuoveImm/Abbina_Seleziona.png"
                                            ToolTip="Seleziona questa Unità Immobiliare" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="Button1" runat="server" Style="z-index: 117; left: 381px; position: absolute;
                        top: 465px; width: 157px;" Text="Abbina e Memorizza" BackColor="Red" Font-Bold="True"
                        ForeColor="White" TabIndex="5" />
                    <asp:Image ID="btnReport" runat="server" Style="cursor: pointer; z-index: 118; left: 236px;
                        position: absolute; top: 469px" Text="Visualizza Report" Visible="False" Width="135px"
                        TabIndex="4" ImageUrl="~/NuoveImm/Img_VisualizzaReport.png" />
                    <br />
                    <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/NuoveImm/Img_Home.png" Style="z-index: 101;
                        left: 579px; position: absolute; top: 469px" ToolTip="Home" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label11" runat="server" Style="z-index: 122; left: 18px; position: absolute;
            top: 383px; height: 18px;" Visible="False" Width="650px" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
        <asp:Label ID="LBLID" runat="server" Style="z-index: 122; left: 30px; position: absolute;
            top: 524px; height: 18px;" Visible="False" Width="78px"></asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="#0000C0" Style="z-index: 124; left: 16px; position: absolute; top: 420px;
            text-align: left; right: 319px;" Text="Nessuna Selezione" Width="648px" BackColor="#FFFFC0"
            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
        <asp:TextBox ID="txtData" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 130;
            left: 580px; position: absolute; top: 419px; width: 86px;" MaxLength="10" TabIndex="2"
            ToolTip="Formato dd/mm/aaaa">dd/mm/yyyy</asp:TextBox>
        <asp:Label ID="Label13" runat="server" Style="z-index: 131; left: 401px; position: absolute;
            top: 420px" Text="Data Scadenza Offerta"></asp:Label>
        <asp:HiddenField ID="HIDDENtipo" runat="server" />
        <asp:HiddenField ID="HIDDENidDom" runat="server" />
        <asp:HiddenField ID="HIDDENdataPG" runat="server" />
        <asp:HiddenField ID="HIDDENnumPG" runat="server" />
        <asp:HiddenField ID="HIDDENnome" runat="server" />
        <asp:HiddenField ID="HIDDENidBando" runat="server" />
    </div>
    <asp:ImageButton ID="btnNuovaRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
        Style="z-index: 101; left: 92px; position: absolute; top: 469px; height: 20px;"
        ToolTip="Home" TabIndex="9" />
    </p>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
