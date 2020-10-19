<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampeRateizzStraord.aspx.vb"
    Inherits="VSA_StampeRateizzStraord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="display: block; z-index: 50px;">
        <asp:DataGrid ID="DataGridRate" runat="server" Width="1000px" PageSize="20" AutoGenerateColumns="False"
            HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" HeaderStyle-BorderColor="#959595"
            BorderColor="#959595" BorderStyle="Solid" BorderWidth="1px">
            <Columns>
                <asp:BoundColumn DataField="NUMRATA" HeaderText="N° RATA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="EMISSIONE" HeaderText="DATA EMISSIONE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True"
                        HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SCADENZA" HeaderText="DATA SCADENZA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="20%"
                        HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IMPORTORATA" HeaderText="RATA €">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="20%"
                        HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="QUOTACAPITALI" HeaderText="QUOTA CAPITALE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="20%"
                        HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="QUOTAINTERESSI" HeaderText="QUOTA INTERESSI">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="20%"
                        HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IMPORTORESIDUO" HeaderText="RESIDUO €.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="20%"
                        HorizontalAlign="Right" />
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <asp:DataGrid ID="DataGrid1Pdf" runat="server" Width="80%" PageSize="20" AutoGenerateColumns="False"
            HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" HeaderStyle-BorderColor="#959595"
            BorderColor="#959595" BorderStyle="Solid" BorderWidth="1px">
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID_BOLLETTA" ReadOnly="True" Visible="False">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NUM">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NUM_BOLL" HeaderText="NUM.BOL.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="num_tipo" HeaderText="N°/TIPO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="riferimento_da" HeaderText="PERIODO DAL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="riferimento_a" HeaderText="PERIODO AL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="data_emissione" HeaderText="DATA EMISS.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="data_scadenza" HeaderText="DATA SCAD.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="importo_totale" HeaderText="IMP.EMESSO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" HorizontalAlign="Right"
                        Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="importobolletta" HeaderText="IMP.CONT.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" HorizontalAlign="Right"
                        Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="imp_pagato" HeaderText="IMP.PAG.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" HorizontalAlign="Right"
                        Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="imp_residuo" HeaderText="RES.CONT.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" HorizontalAlign="Right"
                        Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="data_pagamento" HeaderText="DATA PAG.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="fl_mora" HeaderText="IN MORA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="fl_rateizz" HeaderText="RAT.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="importo_ruolo" HeaderText="RUOLO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IMP_RUOLO_PAGATO" HeaderText="RUOLO PAG.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SGRAVIO" HeaderText="SGRAVIO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="note" HeaderText="NOTE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="True" Width="5%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" ReadOnly="True" Visible="False">
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    </form>
</body>
</html>
