<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="Estrazioni.aspx.vb" Inherits="SPESE_REVERSIBILI_Estrazioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table >
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle; text-align: center" class="style1">
                            <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle" class="style1">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Estrazione complessiva di consuntivi e conguagli, dettaglio delle spese consuntivate, elenco delle anomalie riscontrate</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <table style="width:100%">
                    <tr>
                        <td style="width:15%">Conguagli
                        </td>
                        <td>
                            <asp:Button ID="ButtonConguagli" runat="server" Text="Estrai" />
                        </td>
                    </tr>
                    <tr>
                        <td>Dettaglio spese
                        </td>
                        <td>
                            <asp:Button ID="ButtonDettaglioSpese" runat="server" Text="Estrai" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="ButtonEsci" runat="server" Text="Esci" OnClientClick="tornaHome();return false;" />
</asp:Content>
