<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_DocAllegati.ascx.vb" Inherits="VSA_NuovaDomandaVSA_Tab_DocAllegati" %>

<style type="text/css">
    /*.style1
    {
        font-family: Arial;
        font-weight: bold;
        font-size: 10pt;
    }*/
</style>

    <table style="width: 100%;">
        <tr>
            <td class="style1">
                Elenco dei documenti allegati alla domanda &nbsp&nbsp&nbsp
                <asp:Label ID="lblAggDocumenti" runat="server" Font-Bold="True" Font-Names="ARIAL"
                    Font-Size="9pt" Width="305px" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; width: 100%; height: 315px">
                    <asp:CheckBoxList ID="chkListDocumenti" runat="server" Font-Names="Arial" Font-Size="9pt"
                        Width="97%">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
    </table>

    <asp:HiddenField ID="documAlleg" runat="server" Value="0" />
    