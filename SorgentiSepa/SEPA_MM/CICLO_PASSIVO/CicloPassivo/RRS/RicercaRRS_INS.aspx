<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaRRS_INS.aspx.vb"
    Inherits="RRS_RicercaRRS_INS" %>

<%@ Register Src="Tab_RicercaINS_2.ascx" TagName="Tab_Ricerca2" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>INSERIMENTO ORDINI - PRE SELEZIONE</title>
    <script type="text/javascript" src="Funzioni.js">
<!--
        var Uscita1;
        Uscita1 = 1;
// -->
    </script>
    <script type="text/javascript">
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');
        //var tabberOptions = {'onClick':function(){alert("clicky!");}};
        var tabberOptions = {


            /* Optional: code to run when the user clicks a tab. If this
            function returns boolean false then the tab will not be changed
            (the click is canceled). If you do not return a value or return
            something that is not boolean false, */

            'onClick': function (argsObj) {

                var t = argsObj.tabber; /* Tabber object */
                var id = t.id; /* ID of the main tabber DIV */
                var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                var e = argsObj.event; /* Event object */

                document.getElementById('txttab').value = i + 1;
            },
            'addLinkId': true
        };

    </script>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="TitoloModulo">
                    Ordini - Gestione non patrimoniale - Inserimento
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia Ricerca">
                                </telerik:RadButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <div class="tabber" id="tab1">
                            <div class="tabbertab <%=Tabber1%>" style="background-color: white;">
                                <h2>
                                    Indirizzo-Voce-Contratto</h2>
                                <uc1:Tab_Ricerca2 ID="Tab_Ricerca2" runat="server" Visible=" true" />
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="txtIdPianoFinanziario" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtTIPO" runat="server"></asp:HiddenField>
    <asp:TextBox ID="txttab" runat="server" ForeColor="White" Style="left: 0px; position: absolute;
        visibility: hidden; top: 200px; z-index: -1;" TabIndex="-1">1</asp:TextBox>
    </form>
</body>
</html>
