<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_DocAllegati.ascx.vb"
    Inherits="Dom_DocAllegati" %>
<style type="text/css">
    .style1
    {
        font-family: Arial;
        font-weight: bold;
        font-size: 10pt;
    }
</style>
<div id="docallegati" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid;
    position: absolute; top: 107px; height: 390px; background-color: #ffffff; z-index: 200;">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                Elenco dei documenti allegati alla domanda &nbsp&nbsp&nbsp
                <asp:Label ID="lblAggDocumenti" runat="server" Font-Bold="True" Font-Names="ARIAL"
                    Font-Size="9pt" Width="305px"></asp:Label>
                <img id="imgCercaRapida" alt="Ricerca Rapida" onclick="cerca();" src="../Condomini/Immagini/Search_16x16.png"
                    style="border: 1px solid #0000FF; padding: 3px; left: 115px; cursor: pointer;"
                    title="Ricerca Rapida" />
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
                    <asp:CheckBoxList ID="chkListDocumenti" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="97%">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="documAlleg" runat="server" Value="0" />
</div>
