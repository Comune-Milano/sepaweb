<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DetMorosita.aspx.vb" Inherits="Contabilita_DetMorosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dettaglio Bollette</title>
    <style type="text/css">
        .style2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="left: 0px; 
            width: 100%; ">
            <tr>
                <td style="left: 0px;">
                    <div style="width: 895px">
                        <strong>
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                        <asp:Label ID="lblTitolo"
                            runat="server" Font-Size="12pt">Dettaglio della bolletta di </asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblRangeDate"
                            runat="server" Font-Size="10pt"></asp:Label>
                        &nbsp;<br />
                        </span>
                        </span></strong></div>
                    <div style="left: 12px; width: 100%; height:100%">
                        &nbsp;<asp:Label ID="TBL_DETTAGLIO_BOLLETTA" runat="server" Font-Names="ARIAL" 
                                                    Font-Size="8pt" TabIndex="24" Width="98%"></asp:Label>
                    </div>
                    <br />
                    <span class="style2"><font size="2" 
                        style="color: #3366FF; text-decoration: underline"><strong>
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                        <asp:Label ID="lblSottoTitolo"
                            runat="server" Font-Size="10pt" Font-Overline="False" 
                        Font-Underline="True" style="color: #3366FF">Elenco delle Bollette riclassificate nella suddetta bolletta</asp:Label>
                        </span>
                        </span></strong><br />
                    <br />
                    </font></span>

                    <asp:Label ID="TBL_BOLLETTE_RIC" runat="server" Font-Names="ARIAL" 
                                                    Font-Size="8pt" TabIndex="24" Width="98%"></asp:Label>

                    <br />
                    <br />
                    <br />
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
                        ForeColor="Red" Style="z-index: 10; left: 13px; position: absolute; top: 598px; height: 13px; width: 719px;"
                        Text="Label" Visible="False"></asp:Label>
                    <br />
                    <br />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    
</body>
</html>
