<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatiUtenza.aspx.vb" Inherits="Contratti_CONTRATTI_LIGHT_DatiUtenza" %>

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
                                &nbsp;</td>
                            
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

