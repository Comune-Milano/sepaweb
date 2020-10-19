<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatiUtenza.aspx.vb" Inherits="Contabilita_DatiUtenza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Utenza</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function ApriEventi() {
            window.open('DettaglioBolletta.aspx?IDANA=<%=IDANA %>&PAGPARZ=1&IDCONT=' + document.getElementById('IdCont').value, 'Dettagli', '');
        }

    </script>
</head>
<body>
    <form id="frmDatiUtenza" runat="server">
    <div>
        <table style="left: 0px; width: 100%;">
            <tr>
                <td style="left: 0px;">
                    <div>
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                            Riepilogo Inquilino <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:Label ID="LblIntestazione" runat="server"></asp:Label>&nbsp;</span><br />
                            <br />
                        </span></strong>
                    </div>
                    <div style="left: 12px; width: 100%; height: 100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;<asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="10pt" ForeColor="Black" Text="DATI CONTRATTUALI" Width="140px" Font-Overline="False"></asp:Label>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td style="vertical-align: top; height: 41px;">
                                                &nbsp;<asp:Label ID="TBL_DATI_CONTRATTUALI" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                                    TabIndex="24" Width="98%"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                        ForeColor="Black" Text="SALDO GENERALE" Width="127px"></asp:Label>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td style="vertical-align: top; height: 41px;">
                                                &nbsp;<asp:Label ID="TBL_SALDO_GENERALE" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                                    TabIndex="25" Width="98%"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        &nbsp; <span class="style1"><b>*SALDO</b> = EMESSO CONTABILE <i>(emesso al netto delle
                            quote sindacali)</i> - PAGATO <i>(pagato al netto delle quote sindacali)</i></span></div>
                    <br />
                    <br />
                    <br />
                    <%--<img style="cursor: pointer" alt="Estratto Conto" src="IMMCONTABILITA/Estratto.png"
                        onclick="window.open('DateEstrattoConto.aspx?IDANA=<%=IDANA %>&IDCONT='+ document.getElementById('IdCont').value,'Estratto', '');" />&nbsp;&nbsp;
                    --%>
                    <table>
                        <tr>
                            <td>
                                <asp:Menu ID="EstrattoConto" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                                    Orientation="Horizontal" RenderingMode="Table" ToolTip="Estratto Conto">
                                    <DynamicHoverStyle BackColor="#C0FFC0" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                                    <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                                        ForeColor="#0066FF" Width="180px" />
                                    <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                                        VerticalPadding="1px" />
                                    <Items>
                                        <asp:MenuItem ImageUrl="IMMCONTABILITA/Estratto.png" Selectable="False" Value="">
                                            <asp:MenuItem Text="Stampa contabile" Value="1"></asp:MenuItem>
                                            <asp:MenuItem Text="Stampa gestionale" Value="2"></asp:MenuItem>
                                        </asp:MenuItem>
                                    </Items>
                                </asp:Menu>
                            </td>
                            <td>
                                <img style="cursor: pointer" alt="Dettagli Bollette" src="IMMCONTABILITA/DettaglioBollette.png"
                                    onclick="window.open('DateDettBoll.aspx?IDANA=<%=IDANA %>&IDCONT='+ document.getElementById('IdCont').value,'Dettagli', '');" />
                            </td>
                            <td>
                                <img style="cursor: pointer" alt="Pagamenti Pervenuti" src="IMMCONTABILITA/Pag_pervenuti.png"
                                    onclick="window.open('DatePagamenti.aspx?IDANA=<%=IDANA %>&IDCONT='+ document.getElementById('IdCont').value,'Pagamenti', '');" />
                            </td>
                            <td>
                                <img style="cursor: pointer" alt="Pagamenti NON Pervenuti" src="IMMCONTABILITA/Pag_non_pervenuti.png"
                                    onclick="window.open('DatePagamenti.aspx?IDANA=<%=IDANA %>&NONPERV=1&IDCONT='+ document.getElementById('IdCont').value,'Pagamenti', '');" />&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                        <asp:ImageButton ID="ImgPgParziali" runat="server" ImageUrl="~/NuoveImm/img_pg_parz.gif"
                                            OnClientClick="ApriEventi();return false;" Height="23px" Width="22px" ToolTip="Sono stati effettuati Pagamenti Parziali" />
                                    </span></span></strong>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<br />
                    &nbsp;<br />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 13px; position: absolute; top: 598px;
                        height: 13px; width: 719px;" Text="Label" Visible="False"></asp:Label>
                    <br />
                    <br />
                    <br />
                    &nbsp;
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="IdCont" runat="server" />
    </form>
</body>
</html>
