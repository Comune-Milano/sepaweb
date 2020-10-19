<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Fascicolo.aspx.vb" Inherits="ANAUT_Stampe_Fascicolo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" href="../css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="../js/jsfunzioni.js"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>Frontespizio</title>
    </head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td bgcolor="Maroon" style="text-align: center">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                                ForeColor="White" Text="STAMPA FRONTESPIZIO"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" Text="Selezionare i documenti da includere nel frontespizio"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Text="Data Presentazione:"></asp:Label>
                            <asp:TextBox ID="txtDataPresenta" runat="server" Width="88px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="contenitore" style="border: 1px solid #000000; overflow: auto; visibility: visible;
                                width: 98%;">
                                <asp:CheckBoxList ID="CheckDocumenti" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    RepeatColumns="1" Style="z-index: 100; left: 0px; position: static; top: 0px"
                                    BorderColor="Black" Width="90%">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="idd" runat="server" />
                <asp:HiddenField ID="cod" runat="server" />
                <asp:HiddenField ID="pg" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="30%">
        <tr>
            <td>
                <asp:ImageButton ID="btnSelezionaTutto" runat="server" ImageUrl="~/NuoveImm/Img_SelezionaTutto.png" />
            </td>
            <td>
                <asp:ImageButton ID="btnDeSelezionaTutto" runat="server" ImageUrl="~/NuoveImm/Img_DeselezionaTutto.png" />
            </td>
        </tr>
    </table>
    <table width="100%">
    <tr>
    <td style="text-align: right">
    
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                                ToolTip="Stampa frontespizio" />
        <img alt="Esci" src="../../NuoveImm/Img_Esci_AMM.png"
                                    onclick="self.close();" style="cursor: pointer" /></td>
    </tr>
    </table>
    <p>
        &nbsp;</p>
    <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
    </form>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var altezzaPaginaIntera = $(window).height() - 5;
            var altezzaContenuto = 0;
            altezzaContenuto = altezzaPaginaIntera - 200;
            $("#contenitore").height(altezzaContenuto);
        });

        $(window).resize(function () {
            var altezzaPaginaIntera = $(window).height() - 5;
            var altezzaContenuto = 0;
            altezzaContenuto = altezzaPaginaIntera - 200;
            $("#contenitore").height(altezzaContenuto);
        });

        $(document).submit(function () {
            var altezzaPaginaIntera = $(window).height() - 5;
            var altezzaContenuto = 0;
            altezzaContenuto = altezzaPaginaIntera - 200;
            $("#contenitore").height(altezzaContenuto);
        });
    </script>
</body>
</html>
