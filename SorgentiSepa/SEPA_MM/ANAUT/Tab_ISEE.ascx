<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_ISEE.ascx.vb" Inherits="ANAUT_Tab_ISEE" %>
<table width="97%" style="border: 1px solid #0066FF; vertical-align: top; height: 20px;">
    <tr>
        <td style="padding-left: 15px;">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="padding-left: 15px;" height="20px" width="20%">
            <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                Font-Bold="False">Numero Protocollo DSU</asp:Label>
        </td>
        <td height="20px" width="80%" align="left">
            <table style="width: 30%">
                <tr>
                    <td style="text-align: right">
                        <asp:TextBox ID="txtProtocolloDSU" runat="server" Width="230px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 15px;" height="20px" width="20%">
            <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                Font-Bold="False">ISEE Ordinario</asp:Label>
        </td>
        <td height="20px" width="80%" align="left">
            <table style="width: 30%">
                <tr>
                    <td style="text-align: right">
                        <asp:TextBox ID="TextISEE" runat="server" Width="150px" style="text-align:right"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 15px;" height="20px" width="20%">
            <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                Font-Bold="False">ISR</asp:Label>
        </td>
        <td height="20px" width="80%" align="left">
            <table style="width: 30%">
                <tr>
                    <td style="text-align: right">
                        <asp:TextBox ID="TextISR" runat="server" Width="150px" style="text-align:right"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 15px;" height="20px" width="20%">
            <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                Font-Bold="False">ISP</asp:Label>
        </td>
        <td height="20px" width="80%" align="left">
            <table style="width: 30%">
                <tr>
                    <td style="text-align: right">
                        <asp:TextBox ID="TextISP" runat="server" Width="150px" style="text-align:right"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 15px;" height="20px" width="20%">
            <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                Font-Bold="False">ISE</asp:Label>
        </td>
        <td height="20px" width="80%" align="left">
            <table style="width: 30%">
                <tr>
                    <td style="text-align: right">
                        <asp:TextBox ID="TextISE" runat="server" Width="150px" style="text-align:right"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
