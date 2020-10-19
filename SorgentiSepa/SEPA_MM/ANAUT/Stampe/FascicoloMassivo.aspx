<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FascicoloMassivo.aspx.vb"
    Inherits="ANAUT_Stampe_FascicoloMassivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link type="text/css" href="../css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="../js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="../js/jsfunzioni.js"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>Frontespizio</title>
</head>
<body>
<script type="text/javascript">
    function CompletaData(e, obj) {
        // Check if the key is a number
        var sKeyPressed;

        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
                // don't insert last non-numeric character
                if (window.event) {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
            }
        }
        else {
            if (obj.value.length == 2) {
                obj.value += "/";
            }
            else if (obj.value.length == 5) {
                obj.value += "/";
            }
            else if (obj.value.length > 9) {
                var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                if (selText.length == 0) {
                    // make sure the field doesn't exceed the maximum length
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
        }
    }
</script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="background-image: url('../../NuoveImm/SfondoMaschere.jpg')">
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="14pt"
                                ForeColor="#801F1C" Text="STAMPA FRONTESPIZIO MASSIVO" Font-Overline="False"></asp:Label>
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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;<asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" Text="Schede AU COMPLETE inserite dal:"></asp:Label>
                            <asp:TextBox ID="txtDataInsDA" runat="server" Width="80px"></asp:TextBox>
                            &nbsp;al
                            <asp:TextBox ID="txtDataInsA" runat="server" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;</td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="30%">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td style="text-align: right">
                <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                    ToolTip="Stampa frontespizio" OnClientClick="document.getElementById('aspetta').style.visibility = 'visible';"/>
                <img alt="Esci" src="../../NuoveImm/Img_Esci_AMM.png" onclick="self.close();" style="cursor: pointer" />
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
    
            <div id="aspetta" style="position: absolute; width: 668px; height: 546px; top: 0px;
                left: 0px; background-image: url('../../NuoveImm/SfondoMaschere.jpg'); background-repeat: no-repeat;
                z-index: 1000; background-color: #C0C0C0; visibility: hidden;">
                <table style="width: 100%; height: 100%; top: 0px; left: 0px;">
                    <tr valign="middle">
                        <td align="center" height="100%">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/loading.gif" Width="17px" />
                        </td>
                    </tr>
                </table>
            </div>
        
    </form>
    <%--<script type="text/javascript" language="javascript">
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
    </script>--%>
</body>
</html>
