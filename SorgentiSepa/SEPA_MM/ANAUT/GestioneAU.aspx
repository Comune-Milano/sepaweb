<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneAU.aspx.vb" Inherits="ANAUT_GestioneAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>Gestione Bando</title>
    <style type="text/css">
                
        
        .CssMaiuscolo
        {
            text-transform: uppercase;
            text-align: left;
        }
    </style>
    <script type="text/javascript" id="{">
        var Selezionato;

        function $onkeydown() {
            if (event.keyCode == 13) {
                ApriDatiBando();
            }
        }

        function ApriDatiBando() {

            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                today = new Date();
                var Titolo = 'Bando' + today.getMinutes() + today.getSeconds();
                window.showModalDialog('BandoAU.aspx?ID=' + document.getElementById('LBLID').value, window, 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
            }
            else {
                Messaggio('Selezionare un elemento dalla lista!');
            }

        }

        function Messaggio(TestoMessaggio) {

            $(document).ready(function () {
                $('#ScriptMsg').text(TestoMessaggio);
                $('#ScriptMsg').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione...', buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            });
        }

        function NuovoBando() {
            window.showModalDialog('BandoAU.aspx?ID=-1', window, 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
        }

        function VerificaSelezionato() {

            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                document.getElementById('H1').value = '1';
                
            }
            else {
                document.getElementById('H1').value = '0';
                Messaggio('Selezionare un elemento dalla lista!');
            }
        }


    </script>
</head>
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
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        document.onkeydown = $onkeydown;
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table width="100%" cellpadding="0" cellspacing="0" style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                    <tr>
                        <td style="height: 55px; width: 2%">
                            &nbsp;
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="lbltitolo" runat="server" Text="Gestione Anagrafe Utenza" Font-Names="arial"
                                Font-Bold="True" Font-Size="14pt" ForeColor="#801F1C"></asp:Label>
                            <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;<asp:Label
                                ID="Label4" runat="server" Text="DD"></asp:Label>
                            </strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 300px; width: 2%">
                            &nbsp;
                        </td>
                        <td style="vertical-align: top">
                            <asp:RadioButton ID="rdbSolo" runat="server" Checked="True" GroupName="A" 
                                Text="Solo AU Aperte" AutoPostBack="True" />
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="rdbSolo0" runat="server" GroupName="A" 
                                Text="Tutte le AU" AutoPostBack="True" />
                            <br />
                            <asp:DataGrid ID="Datagrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                                Width="98%" CellPadding="4" ForeColor="#333333">
                                <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" ForeColor="White" />
                                <PagerStyle Wrap="False" BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <AlternatingItemStyle Wrap="False" BackColor="White" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ReadOnly="True">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ANNO_AU" HeaderText="ANNO AU"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ANNO_ISEE" HeaderText="ANNO REDDITI"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO" ReadOnly="True">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_FINE" HeaderText="DATA FINE" ReadOnly="True"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIZIONE_STATO" HeaderText="STATO" ReadOnly="True">
                                    </asp:BoundColumn>
                                </Columns>
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="White" Wrap="False" />
                                <EditItemStyle Wrap="False" BackColor="#2461BF" />
                                <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" ForeColor="#333333" />
                                <ItemStyle Wrap="False" BackColor="#EFF3FB" />
                            </asp:DataGrid>
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px; width: 2%">
                            &nbsp;
                        </td>
                        <td style="vertical-align: top">
                            <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                                Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                border-bottom: white 1px solid; left: -1px; top: 45px;" Width="100%">Nessuna Selezione</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 120px; width: 2%">
                            &nbsp;
                        </td>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0" style="margin-top: 0px">
                                <tr>
                                    <td style="width: 20%">
                                        <asp:ImageButton ID="btnNuovo" runat="server" ImageUrl="~/NuoveImm/Img_Nuovo.png"
                                            ToolTip="Crea un nuova ANAGRAFE UTENZA" OnClientClick="NuovoBando();" />
                                    </td>
                                    <td style="width: 20%">
                                        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                                            ToolTip="Visualizza i dati dell'Anagrafe Utenza" 
                                            OnClientClick="ApriDatiBando();" />
                                    </td>
                                    <td style="width: 20%">
                                        <asp:ImageButton ID="btnApreChiude" runat="server" ImageUrl="~/NuoveImm/img_ApriChiudi.png"
                                            ToolTip="Apre o Chiude il l'Anagrafe Utenza selezionata" 
                                            OnClientClick="VerificaSelezionato();" />
                                    </td>
                                    <td style="width: 20%">
                                        <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="~/NuoveImm/Img_Elimina_Grande.png"
                                            ToolTip="Elimina l'Anagrafe Utenza selezionata" 
                                            OnClientClick="VerificaSelezionato();" />
                                    </td>
                                    <td style="width: 20%">
                                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <asp:ImageButton ID="btnApri" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                                            ToolTip="" Style="position: absolute; top: -100px; left: -100px" />
                                        <asp:ImageButton ID="btnEliminaBando" runat="server" ImageUrl="~/NuoveImm/Img_Nuovo.png"
                                            Style="position: absolute; top: -100px; left: -100px" />
                                    </td>
                                    <td style="width: 20%">
                                        <asp:ImageButton ID="btnChiudi" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                                            ToolTip="" Style="position: absolute; top: -100px; left: -100px" />
                                    </td>
                                    <td style="width: 20%">
                                    </td>
                                    <td style="width: 20%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 20%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="H1" runat="server" Value="0" />
                <asp:HiddenField ID="LBLID" runat="server" />
                <div id="divLoading" 
                    style="position:absolute;margin: 0px; width: 100%; height: 100%;top: 0px; left: 0px;background-color: #ffffff;z-index:1000; visibility: hidden;"><div style="position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"><table style="width: 100%; height: 100%;"><tr><td valign="middle" align="center"><img src="../NuoveImm/load.gif" alt="Caricamento in corso" /><br /><br /><span id="Label41" style="font-family:Arial;font-size:10pt;">Caricamento in corso...</span></td></tr></table></div></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <p>
        &nbsp;</p>
    <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
<div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt;
        font-family: Arial">
    </div>
         <script language="javascript" type="text/javascript">
             if (document.getElementById('divLoading')) {
                 document.getElementById('divLoading').style.visibility = 'hidden';
             }
    </script>
     

    </form>
</body>
   
</html>
