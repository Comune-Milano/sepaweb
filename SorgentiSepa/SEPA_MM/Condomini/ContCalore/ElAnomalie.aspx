<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElAnomalie.aspx.vb" Inherits="Contratti_ContCalore_ElAnomalie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            width: 786px;
        }
        .style1
        {
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; background-attachment: fixed">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Anomalie Calcolo
                    Contributo Calore</span></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td class="style1" style="width: 20%" nowrap="nowrap">
                            <strong>CONTRIBUTO CALORE</strong>
                        </td>
                        <td style="width: 55%">
                            <asp:DropDownList ID="cmbContCalore" runat="server" Width="400px" AutoPostBack="True"
                                Font-Names="Arial" Font-Size="10pt">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 5%; text-align: right;">
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../NuoveImm/xlsExport.png" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; height: 445px;">
                    <asp:DataGrid ID="dgvAnomalie" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                        Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" PageSize="1" Style="z-index: 105;
                        left: 8px; top: 32px" Width="100%" CellPadding="4" ForeColor="#333333">
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#2461BF" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_CONT_CALORE" HeaderText="ID_CONT_CALORE" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD.CONTRATTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD. UNITA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                    OnClientClick="parent.main.location.replace('../pagina_home.aspx');return false;" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:HiddenField ID="idConCal" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
