<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaManutenzioniINS.aspx.vb"
    Inherits="MANUTENZIONI_RicercaManutenzioniINS" %>

<%@ Register Src="Tab_RicercaINS_1.ascx" TagName="Tab_Ricerca1" TagPrefix="uc1" %>
<%@ Register Src="Tab_RicercaINS_2.ascx" TagName="Tab_Ricerca2" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>INSERIMENTO MANUTENZIONI - PRE SELEZIONE</title>
    <script type="text/javascript" src="Funzioni.js">
    
    <!--
        var Uscita1;
        Uscita1 = 1;
// -->
    </script>
    <script type="text/javascript" src="../../../StandardTelerik/Scripts/jsFunzioni.js"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
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
    <style type="text/css">
        .style3
        {
            height: 31px;
        }
    </style>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator runat="server" ID="RadFormDecorator1" DecoratedControls="Buttons,Textbox" />
    <table style="width: 100%">
        <tr>
            <td class="TitoloModulo">
               Ordini - Manutenzioni e servizi - Inserimento
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
                            <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Esci">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="TAB_TABBER">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <telerik:RadTabStrip runat="server" ID="RadTabStrip" Orientation="HorizontalTop"
                        Align="Justify" Width="100%" MultiPageID="RadMultiPage1" ShowBaseLine="false"
                        OnClientTabSelected="tabSelezionato">
                        <Tabs>
                            <telerik:RadTab runat="server" PageViewID="RadPageView1" Text=" Indirizzo-Servizio-Contratto"
                                Value="ISC" Selected="true">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" PageViewID="RadPageView2" Text="Servizio-Contratto-Complesso/Edificio"
                                Value="SCCE">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage runat="server" ID="RadMultiPage1" CssClass="multiPage" Width="100%"
                        ScrollChildren="true">
                        <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="panelTabsStrip" Selected="true">
                            <asp:Panel runat="server" ID="Panel1">
                                <uc2:Tab_Ricerca2 ID="Tab_Ricerca2" runat="server" Visible=" true" />
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView runat="server" ID="RadPageView2" CssClass="panelTabsStrip">
                            <asp:Panel runat="server" ID="tab2">
                                <uc1:Tab_Ricerca1 ID="Tab_Ricerca1" runat="server" Visible=" true" />
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <%--       <div class="tabber" id="tab1">
                        <div class="<%=TabberHide%> <%=Tabber1%>" style="background-color: white;">
                            <h2>
                                Indirizzo-Servizio-Contratto</h2>
                            <uc2:Tab_Ricerca2 ID="Tab_Ricerca2" runat="server" Visible=" true" />
                        </div>
                        <div class="tabbertab <%=Tabber2%>" style="background-color: white;">
                            <h2>
                                Servizio-Contratto-Complesso/Edificio</h2>
                            <uc1:Tab_Ricerca1 ID="Tab_Ricerca1" runat="server" Visible=" true" />
                        </div>
                    </div>--%>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="txtIdPianoFinanziario" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtTIPO" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtSTATO_PF" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="HiddenTabSelezionato" runat="server" Value="0" />
    <asp:HiddenField ID="numTab" runat="server" Value="2" />
    <asp:TextBox ID="txttab" runat="server" ForeColor="White" Style="left: 0px; position: absolute;
        visibility: hidden; top: 200px; z-index: -1;" TabIndex="-1">1</asp:TextBox>
    </form>
</body>
</html>
