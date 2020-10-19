<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Maschera_Vuota_Gestione_Dati.aspx.vb" Inherits="RILEVAZIONI_Maschera_Vuota_Gestione_Dati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dettagli Associazione Lotti-Unità</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.css" rel="stylesheet"
        type="text/css" />
        <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/gestioneDimensioniPagina.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="BeforeSubmit();return true;">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="divGenerale">
        <div id="divHeader" style="overflow: auto; height: 32px;">
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 90%;">
                        <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="False"
                            IncludeStyleBlock="false" Orientation="Horizontal" RenderingMode="List">
                            <Items>
                                <asp:MenuItem Text="Salva" Value="Salva">
                                </asp:MenuItem>
                                <asp:MenuItem Text="Esci" Value="Esci">
                                </asp:MenuItem>
                            </Items>
                        </asp:Menu>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divBody">
            <div id="divTitolo">
                <table id="tbTitolo">
                    <tr>
                        <td style="width: 5px;">
                            &nbsp;
                        </td>
                        <td>
                            Titolo della finestra 
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%; height: 100%; overflow: auto;">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td>
                         <div id="divOverContent" style="width: 100%; overflow: auto;">
                            contenuto della finestra
                             </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divFooter">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 5px">
                            &nbsp;
                        </td>
                        <td>
                            footer (lasciare vuoto se non serve)
                        </td>
                    </tr>
                </table>
                <div id="dialog" style="display: none;">
                </div>
                <div id="confirm" style="display: none;">
                </div>
                <div id="loading" style="display: none; text-align: center;">
                </div>
                <div id="divLoading" style="width: 0px; height: 0px; display: none;">
                    <img src="../Standard/Immagini/load.gif" id="imageLoading" alt="" />
                </div>
                <div id="divOscura" style="display: none; text-align: center; width: 100%; height: 100%;
                    position: absolute; top: 0px; left: 0px; background-color: #cccccc;">
                </div>
                <asp:HiddenField ID="tipoSubmit" runat="server" Value="0" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="noClose" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="noCloseRead" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="optMenu" runat="server" Value="0" ClientIDMode="Static" />
    <script src="../Standard/Scripts/jsfunzioniExit.js" type="text/javascript"></script>
    <script type="text/javascript">
        initialize();
        function initialize() {
            document.getElementById('divHeader').style.overflow = '';
            AfterSubmit();
            window.focus();
        };
    </script>
    </form>
</body>
</html>
