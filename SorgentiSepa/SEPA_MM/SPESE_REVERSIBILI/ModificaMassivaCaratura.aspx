<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="ModificaMassivaCaratura.aspx.vb" Inherits="SPESE_REVERSIBILI_ModificaMassivaCaratura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 5px;">
        <table>
            <tr>
                <td colspan="3">
                    <table border="0" cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="vertical-align: middle; text-align: center">
                                <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                            </td>
                            <td style="vertical-align: middle">
                                <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Scaricare il formato Excel da importare con il formato giusto, modificare i valori e quindi eseguire l'import.</i></asp:Label>
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
                                                    <asp:Button ID="btnDownload" runat="server" Text="Download" /><br />
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
        </table>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button runat="server" ID="Button1" Text="Esci" OnClientClick="tornaHome();return false;"></asp:Button>
</asp:Content>
