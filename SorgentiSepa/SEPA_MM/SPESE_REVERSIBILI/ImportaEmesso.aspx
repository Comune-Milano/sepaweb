<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="ImportaEmesso.aspx.vb" Inherits="SPESE_REVERSIBILI_ImportaEmesso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/smoothness/jquery-ui-1.9.0.custom.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div style="padding-left: 5px;">
        <div>
            <table>
            <tr>
            <td colspan="3">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle; text-align: center">
                            <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>L'operazione di import sovrascrive, se presente, l'emesso indipendentemente dalla modalità di creazione (tramite calcolo o tramite import).
                            <br />Mediante il bottone "Download" è possibile scaricare il file Excel da importare nel giusto formato. Il file è già compilato con tutti i contratti (RU) che hanno ricevuto almeno una bolletta nell'esercizio finanziario oggetto di calcolo.
                            </i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
                <tr>
                    <td>
                        <div>
                            <fieldset style="width: 750px;">
                                <legend>Download file excel</legend>
                                <table style="width: 500px;">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Scarica File Excel
                                                    </td>
                                                    <td style="width: 3%" valign="top">
                                                        <asp:Button ID="btnDownload" runat="server" Text="Download"/><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <div>
                            <fieldset style="width: 750px;">
                                <legend>Upload file excel</legend>
                                <table style="width: 500px;">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Carica File Excel
                                                    </td>
                                                    <td id="sfoglia">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                    </td>
                                                    <td style="width: 3%" valign="top">
                                                        <asp:Button ID="btnAllega" runat="server" Text="Upload" /><br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Title="Sep@Com"
        Height="85px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
        AutoCloseDelay="3500" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
    </telerik:RadNotification>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="ButtonEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();" />
</asp:Content>
