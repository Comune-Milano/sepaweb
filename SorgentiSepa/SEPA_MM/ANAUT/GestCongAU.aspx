<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestCongAU.aspx.vb" Inherits="ANAUT_GestCongAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Selezionato;
    var Selezionato1;
    var Selezionato2;
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!-- GESTIONE MODALI -->
    <script src="../StandardTelerik/Scripts/modalTelerik.js" type="text/javascript">
    </script>
    <!-- GESTIONE MODALI FINE -->
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>Gestione</title>
    <style type="text/css">
        .style1
        {
            text-align: left;
            color: #FFFFFF;
            font-weight: bold;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12pt;
        }
        .style2
        {
            color: #801f1c;
            font-size: 14pt;
        }
        .style3
        {
            height: 259px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!-- MODALI -->
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindow ID="modalRadWindow" runat="server" CenterIfModal="true" Modal="True"
        VisibleStatusbar="False" Skin="Web20" ClientIDMode="Static" ShowContentDuringLoad="False">
    </telerik:RadWindow>
    <!-- MODALI FINE -->
    <div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table style="left: 0px; background-image: url('../NuoveImm/SfondoMaschere.jpg');
            width: 674px; position: absolute; top: 0px">
            <tr>
                <td class="style1">
                    <br />
                    &nbsp; <span class="style2">Associazione Anagrafe Utenza - Voci Conguaglio</span>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    &nbsp;
                    <asp:Label ID="lblDescrBando" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblVoceGest" runat="server" Font-Names="arial" Font-Size="10pt">Tipologia Bolletta Gestionale</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbTipo" TabIndex="11" runat="server" Height="20px" 
                                    Width="350px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblVoceGest0" runat="server" Font-Names="arial" Font-Size="10pt">Voce Bolletta Gestionale</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbVoce" TabIndex="11" runat="server" Height="20px" 
                                    Width="350px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                                       <br />
                    <br />
                    
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Names="arial" Font-Size="10pt" Style="font-weight: 700"
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_SalvaGrande.png" 
                         TabIndex="3" 
                            style="height: 20px;" />&nbsp;&nbsp; <img alt="Torna alla pagina principale" 
                        src="../NuoveImm/Img_Home.png" id="imgEliminafiliale" 
                        
                style="cursor:pointer;" 
                onclick="PaginaHome();"/></td>
            </tr>
            <tr>
                <td class="style3">
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="idBando" runat="server" Value="" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
    <div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt;
        font-family: Arial">
    </div>
    <script type="text/javascript" language="javascript">
        function SceltaFunzione(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptScelta').text(TestoMessaggio);
                $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnEliminaFil', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
            });
        }

        function SceltaFunzioneSP(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptScelta').text(TestoMessaggio);
                $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnEliminaSportello', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
            });
        }

        function SceltaFunzioneOP(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptScelta').text(TestoMessaggio);
                $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnEliminaOperatore', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
            });
        }

        function Messaggio(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptMsg').text(TestoMessaggio);
                $('#ScriptMsg').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione...', buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            });
        }
        function PaginaHome() {
            document.location.href = 'pagina_home.aspx';
        }
    </script>
    </form>
</body>
</html>
