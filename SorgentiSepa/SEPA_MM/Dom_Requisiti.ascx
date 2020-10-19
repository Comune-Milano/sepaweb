<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Requisiti.ascx.vb" Inherits="Dom_Requisiti" %>
<style type="text/css">
    .auto-style1 {
        left: 317px;
        width: 320px;
        position: absolute;
        top: 10px;
        z-index: 100;
    }
</style>
<div id="req" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid; left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid; position: absolute; top: 107px; height: 350px; background-color: #ffffff; z-index: 194;">
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<br />
    <br />
    &nbsp; &nbsp;&nbsp;
    <table border="0" cellpadding="0" cellspacing="0" style="left: 1px; width: 320px; position: absolute; top: 10px; z-index: 100;">
        <tr>
            <td style="font-family: Arial; font-size: 10pt; font-weight: bold;">Requisiti di ammissibilità</td>
        </tr>
        <tr>
            <td>&nbsp</td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 30px; text-align: left">
                <asp:CheckBox ID="chR1" runat="server" Checked="True" Font-Names="Times New Roman"
                    Font-Size="10pt" Text="a) Cittadinanza o Soggiorno" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 30px; text-align: left">
                <asp:CheckBox ID="chR2" runat="server" Checked="True" Font-Names="Times New Roman"
                    Font-Size="10pt" Text="b) Residenza o Attività lavorativa nel comune" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 30px; text-align: left">
                <asp:CheckBox ID="chR3" runat="server" Checked="True" Font-Names="times" Font-Size="10pt"
                    Text="c) Assenza di Assegnazione in Proprietà" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 30px; text-align: left">
                <asp:CheckBox ID="chR4" runat="server" Checked="True" Font-Names="times" Font-Size="10pt"
                    Text="d) Assenza di Decadenza per Attività illecite" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 30px; text-align: left">
                <asp:CheckBox ID="chR5" runat="server" Checked="True" Font-Names="times" Font-Size="10pt"
                    Text="e) Assenza di Cessione alloggio ERP" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 30px; text-align: left">
                <asp:CheckBox ID="chR6" runat="server" Checked="True" Font-Names="times" Font-Size="10pt"
                    Text="g) Assenza di proprietà o alloggio adeguato" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 30px; text-align: left">
                <asp:CheckBox ID="chR7" runat="server" Checked="True" Font-Names="times" Font-Size="10pt"
                    Text="h) Assenza di Morosità da Alloggio ERP ultimi 5 anni" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 30px; text-align: left">
                <asp:CheckBox ID="chR8" runat="server" Checked="True" Font-Names="times" Font-Size="10pt"
                    Text="i)  Assenza occupazione abusiva ultimi 5 anni" /></td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" class="auto-style1">
        <tr>
            <td style="font-family: Arial; font-size: 10pt; font-weight: bold;">Motivi di esclusione</td>
        </tr>
        <tr>
            <td>&nbsp</td>
        </tr>
        <tr>
            <td>
                <asp:CheckBoxList ID="chkListRequisiti" runat="server" Font-Names="Times New Roman" Font-Size="9pt"></asp:CheckBoxList></td>
        </tr>
    </table>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#TR" Style="z-index: 102; left: 621px; position: absolute; top: 3px"
        Target="_blank" Width="16px">Aiuto</asp:HyperLink>
    <br />
    &nbsp; &nbsp;&nbsp;
    <br />
    <br />
    &nbsp; &nbsp;&nbsp;
    <br />
    <br />
    &nbsp; &nbsp;&nbsp;
    <br />
    <br />
    &nbsp; &nbsp;&nbsp;
    <br />
    <br />
    &nbsp; &nbsp;&nbsp;
    <br />
    <br />
    <br />
    <br />
    &nbsp; &nbsp;&nbsp;
</div>
