<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="GESTIONE_CONTATTI_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 135px;
            height: 43px;
        }
    </style>
     <script language="javascript" type="text/javascript">
                function OnClientUpdated(sender, args) {
            //var message = "Update (check) was done!";
            //var newMsgs = sender.get_value();
            //if (newMsgs != 0) {
            //    sender.show();
            //    //message += (newMsgs == 1) ? (" There is 1 new message!") : (" There are " + newMsgs + " new messages!");
            //} else {
            //    //message += " There are no new messages!";
            //}
            ////logEvent(message);
         };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: center; color: #801f1c; font-family: Arial; font-size: 20pt; font-weight: bold;">
                <center>
                    <asp:Label ID="lblTitolo" runat="server" Text="Agenda e Segnalazioni"></asp:Label></center>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="width: 90%;">
                <span id="testo" runat="server" clientidmode="Static" style="visibility: hidden;"><b><span style="color: #006600; font-size: 0px;"></span></b></span>
            </td>
            <td style="width: 10%;">
                <table align="right">
                    <tr>
                        <td>
                            <img alt="Guida per l'Utente" src="../Standard/Immagini/guida.png" title="Guida per l'Utente" style="cursor: pointer" onclick="window.open('GuidaGestioneContatti.pdf');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<img alt="Logo Gestore" src="../LOGHI/image.png" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="idFornitore" Value="0" />
    <asp:HiddenField runat="server" ID="idDirettoreLavori" Value="0" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <%--<table style="width: 100%; position: relative; top: -45px;">
        <tr>
            <td style="width: 95%;">
            </td>
            <td style="text-align: right">
                <img alt="Guida per l'Utente" src="Immagini/guida.png" title="Guida per l'Utente"
                    style="cursor: pointer" onclick="message('Attenzione', 'Non Disponibile al Momento!');" />
            </td>
        </tr>
    </table>--%>
</asp:Content>
