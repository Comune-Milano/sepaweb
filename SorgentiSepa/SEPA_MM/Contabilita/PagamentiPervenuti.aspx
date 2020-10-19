<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PagamentiPervenuti.aspx.vb" Inherits="Contabilita_PagamentiPervenuti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Pagamenti Pervenuti</title>
    <style type="text/css">
        .style1
        {
            font-size: 12pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
                    <div style="height: 132px">
                        <strong>
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Stampa_Grande.png"
                        
                        
                        
                        Style="z-index: 102; right: 805px; left: 12px; top: 16px"
                        ToolTip="Stampa in PDF" />
                        <br />
                        <br />
                        <asp:Label ID="LblTitolo"
                            runat="server"></asp:Label>
                        <br />
                        <br />
                        </span>
                        </span><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <span 
                            style="color: #801f1c; font-family: Arial; text-align: center;" class="style1">
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                        <asp:Label ID="lblTipoPagam"
                            runat="server" Font-Size="12pt">Pagamenti Pervenuti</asp:Label>
                        &nbsp;<asp:Label ID="lblRangeDate"
                            runat="server" Font-Size="10pt"></asp:Label>
                        </span>
                        </span>
                        </span>
                        </strong>

                        
                        </div>

                    <br />
                    <br />
                    <br />
                    <br />

                    <br />
    
                                    <asp:Label ID="TBL_PAGAMENTI_PERVENUTI" 
            runat="server" Font-Names="ARIAL" 
                                                    Font-Size="8pt" TabIndex="25" 
            Width="100%"></asp:Label>
    
    </div>
    <p>

                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 10px; position: absolute; top: 432px; height: 13px; width: 719px;"
                        Text="Label" Visible="False"></asp:Label>
                    </p>
    </form>



</body>
</html>
