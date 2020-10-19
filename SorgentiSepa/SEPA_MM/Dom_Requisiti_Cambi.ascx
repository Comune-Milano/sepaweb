<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Requisiti_Cambi.ascx.vb" Inherits="Dom_Requisiti_Cambi" %>
<div id="req" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid;
    position: absolute; top: 107px; height: 315px; background-color: #ffffff; z-index: 194;">
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<br />
    <br />
    &nbsp; &nbsp;&nbsp;
    <table border="0" cellpadding="0" cellspacing="0" style="left: 1px; width: 557px;
        position: absolute; top: 10px; z-index: 100;">
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR1" runat="server" Checked="True" Font-Names="Times New Roman"
                    Font-Size="8pt" Text="a) Cittadinanza o Soggiorno" Width="238px" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR2" runat="server" Checked="True" Font-Names="Times New Roman"
                    Font-Size="8pt" Text="b) Art. 22 c. 1, 2 e 3 R. R. 1/2004." Width="313px" 
                    TabIndex="1" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR3" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="c) Assenza di Assegnazione in Proprietà" Width="238px" TabIndex="2" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR4" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="d) Assenza di Decadenza per Attività illecite" Width="343px" TabIndex="3" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR5" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="e) Assenza di Cessione alloggio ERP" Width="238px" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR6" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="g) Assenza di posesso di U.I. adeguata al nucleo e/o di valore (RR 1/2004 Art.18 lett. f e g) o entro 70 km" Width="542px" TabIndex="4" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR7" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="h) Assenza di Morosità da Alloggio ERP ultimi 5 anni" Width="313px" TabIndex="5" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR8" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="5/2008 (ex art.8 R.R 1/2007 c.1 lett i) Occupazione abusiva negli utlimi 5 anni." Width="450px" TabIndex="6" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR9" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="j) Inutilizzo dell'alloggio" Width="450px" TabIndex="6" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR10" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="k) Cambio destinazione d'uso" Width="450px" TabIndex="6" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR11" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="l) Gravi danni" Width="450px" TabIndex="6" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR12" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="m) Utilizzo per attività illecite" Width="450px" TabIndex="6" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR13" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="n) Inadempimento a seguito di diffida" Width="450px" TabIndex="6" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR14" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="o) Inadempimento art. 20-21 RR 1/2004" Width="450px" TabIndex="6" /></td>
        </tr>
        <tr>
            <td style="vertical-align: middle; height: 20px; text-align: left">
                <asp:CheckBox ID="chR15" runat="server" Checked="True" Font-Names="times" Font-Size="8pt"
                    Text="p) Valore immobile superiore al limite" Width="450px" TabIndex="6" /></td>
        </tr>
    </table>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#TR" Style="z-index: 102;
        left: 621px; position: absolute; top: 3px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
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
    &nbsp; &nbsp;&nbsp;
</div>
