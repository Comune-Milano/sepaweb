<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="FORNITORI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <table style="width: 100%; font-size: 9pt; color: #000;">
        <tr>
            <td style="width: 90%;">&nbsp;
            </td>
            <td style="width: 10%;">
                <table align="right">
                    <tr>
                        <td>
                            <img alt="Guida per l'Utente" src="../Standard/Immagini/guida.png" title="Guida per l'Utente"
                                style="cursor: pointer" onclick="apriAlert(Messaggio.Funzione_Non_Disponibile, 300, 150, MessaggioTitolo.Attenzione, null, '../StandardTelerik/Immagini/Messaggi/alert.png');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <table cellpadding="2" cellspacing="2">
        <tr>
            <td>&nbsp;
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table>
        <tr>
            <td>
                <telerik:RadButton ID="RadButtonBuildingManager" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Building Manager" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Building Manager" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
                <telerik:RadButton ID="RadButtonDirettoreLavori" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Direttore Lavori" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Direttore Lavori" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
                <telerik:RadButton ID="RadButtonFieldQualityManager" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Coordinatore qualità" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Coordinatore qualità" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
                <telerik:RadButton ID="RadButtonTecnicoAmministrativo" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Tecnico Amministrativo" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Tecnico Amministrativo" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td style="text-align: center;">
                <img alt="Logo Sepa" src="../immagini/SepaWeb.png" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Title="Sep@Web"
        Width="350" Height="500" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
        AutoCloseDelay="0" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
        <ContentTemplate>
            <div id="NotifyDiv" style="visibility: visible; overflow: auto; width: 380px; height: 500px;">
                <asp:Literal ID="lblNote" runat="server"></asp:Literal>
            </div>
        </ContentTemplate>
    </telerik:RadNotification>
</asp:Content>
