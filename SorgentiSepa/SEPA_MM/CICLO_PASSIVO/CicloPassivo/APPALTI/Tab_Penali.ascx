<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Penali.ascx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_Penali" %>
<table style="width: 100%;">
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <asp:TextBox ID="txtpenali" runat="server" Height="150px" TextMode="MultiLine" Width="97%"
                Font-Names="Arial" Font-Size="8pt" MaxLength="500"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="height: 5px">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="2" cellspacing="2">
                <tr>
                    <td style="vertical-align: middle">
                        <img src="../../../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                    </td>
                    <td style="vertical-align: middle">
                        <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Specificare qui le penali del contratto</i></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
