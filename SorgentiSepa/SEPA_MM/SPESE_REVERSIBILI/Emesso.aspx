<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="Emesso.aspx.vb" Inherits="SPESE_REVERSIBILI_Emesso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/smoothness/jquery-ui-1.9.0.custom.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table width="100%">
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle; text-align: center">
                            <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Per procedere con il calcolo dell'emesso è necessario selezionare uno o più esercizi finanziari e le voci OOAA indicate nelle tabelle sottostanti. E' consentita la selezione multipla.</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 50%">
                            <fieldset>
                                <legend>Anno</legend>
                                <div style="width: 100%; height: 100px; overflow: auto">
                                    <asp:CheckBoxList ID="chkAnno" runat="server" Font-Names="Arial" Font-Size="8pt">
                                    </asp:CheckBoxList>
                                </div>
                            </fieldset>
                        </td>
                        <td style="width: 50%">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="overflow: auto; height: 300px;">
        <table width="100%">
            <tr>
                <td style="width: 33%">
                    <fieldset>
                        <legend><strong>Servizi</strong></legend>
                        <asp:CheckBoxList ID="chkServizi" runat="server" Font-Names="Arial" Font-Size="8pt">
                        </asp:CheckBoxList>
                    </fieldset>
                </td>
                <td style="width: 33%">
                    <fieldset>
                        <legend><strong>Riscaldamento</strong></legend>
                        <asp:CheckBoxList ID="chkRiscaldamento" runat="server" Font-Names="Arial" Font-Size="8pt">
                        </asp:CheckBoxList>
                    </fieldset>
                </td>
                <td style="width: 33%">
                    <fieldset>
                        <legend><strong>Ascensore</strong></legend>
                        <asp:CheckBoxList ID="chkAscensore" runat="server" Font-Names="Arial" Font-Size="8pt">
                        </asp:CheckBoxList>
                    </fieldset>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Title="Sep@Com"
        Height="85px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
        AutoCloseDelay="3500" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
    </telerik:RadNotification>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="btnCalcolaEmesso" runat="server" Text="Calcola emesso" ToolTip="Calcola emesso" />
    <asp:Button ID="ButtonEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();return false;" />
</asp:Content>
