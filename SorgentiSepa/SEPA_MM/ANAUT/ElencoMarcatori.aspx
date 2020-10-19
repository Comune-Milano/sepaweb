<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoMarcatori.aspx.vb" Inherits="ANAUT_ElencoMarcatori" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
 
    
    <title>Elenco Segnaposto</title>
    
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-weight: bold;
        }
        </style>
    
</head>

<body>

    <form id="form1" runat="server">
      

    <div>
    
        <table style="width: 100%;">
            <tr>
                <td bgcolor="#990000" class="style1" style="text-align: center">
                    ELENCO SEGNAPOSTO UTILIZZABILI</td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        
                        style="z-index: 105; left: 1px; width: 100%;" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                        CellPadding="4" ForeColor="#333333">
							<EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#507CD1" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="White" />
							<Columns>
								<asp:BoundColumn DataField="COD" HeaderText="CODICE"></asp:BoundColumn>
								<asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                </asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						</asp:datagrid></td>
            </tr>
            <tr>
                <td style="text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: xx-small; font-weight: 700;">
                    &nbsp;&nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; font-family: Arial, Helvetica, sans-serif; font-size: xx-small; font-weight: 700;">
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    &nbsp; &nbsp;<img id="btnAnnulla" alt="" src="../NuoveImm/Img_Esci_Grande.png" 
                        onclick="self.close();" style="cursor:pointer"/></td>
            </tr>
        </table>
    
    </div>
           
       
    </form>
</body>
</html>

