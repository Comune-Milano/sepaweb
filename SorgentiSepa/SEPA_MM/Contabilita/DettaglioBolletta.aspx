<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioBolletta.aspx.vb" Inherits="Contabilita_DettaglioBolletta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dettaglio Bollette</title>
    <style type="text/css">
        .style1
        {
            font-size: 12pt;
        }
        .style2
        {
            font-family: Arial;
            font-weight: bold;
            font-style: italic;
            font-size: 10pt;
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
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Stampa_Grande.png"
                        
                        
                        
                        Style="z-index: 102; right: 805px; left: 12px; top: 16px"
                        ToolTip="Stampa in PDF" />
                        <br />
                        <br />
                        <asp:Label ID="LblIntestazione"
                            runat="server"></asp:Label>
                        <br />
                        <br />
                        </span>
                        </span><span style="color: #801f1c; font-family: Arial">
                        <span style="color: #801f1c; font-family: Arial; text-align: center;" 
                            class="style1">
                        Dettaglio delle Bollette Emesse</span></span><span style="font-size: 14pt; color: #801f1c; font-family: Arial"><span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">&nbsp;&nbsp;
                        <asp:Label ID="lblRangeDate"
                            runat="server" Font-Size="10pt"></asp:Label>
                        &nbsp;<br />
                        <br />
                        </span>
                        </span></strong><span class="style2">* In rosso sono evidenziate le bollette annullate</span></div><br />
                    <div style="left: 12px; width: 100%; height:100%">
                        &nbsp;<asp:Label ID="TBL_DETTAGLIO_BOLLETTA" runat="server" Font-Names="ARIAL" 
                                                    Font-Size="8pt" TabIndex="24" Width="98%"></asp:Label>
                    </div>
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
