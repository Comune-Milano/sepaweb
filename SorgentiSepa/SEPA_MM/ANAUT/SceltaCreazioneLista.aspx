<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaCreazioneLista.aspx.vb" Inherits="ANAUT_SceltaCreazioneLista" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
    var Selezionato;
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
		
	    <style type="text/css">
            #contenitore
            {
                top: 61px;
            }
            </style>
        <title>Creazione Lista</title>
	</head>
	<body bgcolor="#f2f5f1">
		<form id="Form1" method="post" runat="server" >
            <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        </strong>
                        <asp:Label ID="Label26" runat="server" 
                            Text="Seleziona l'origine con cui creare la lista di Convocazione"></asp:Label>
                        </span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:HiddenField ID="LBLID" runat="server" Value="0" />
                        <asp:HiddenField ID="npg" runat="server" Value="" />
                        
                        <asp:HiddenField ID="H1" runat="server" Value="" />
                    </td>
                </tr>
            </table>
            &nbsp;<img alt="Torna alla pagina principale" 
                        src="../NuoveImm/Img_Home.png" id="imgEliminafiliale" 
                        
                style="cursor:pointer;left: 598px; position: absolute; top: 484px; height: 20px;" 
                onclick="PaginaHome();"/>&nbsp;
            <asp:ImageButton ID="btnProcedi" runat="server" 
                style="position:absolute; top: 484px; left: 463px;" 
                ImageUrl="~/NuoveImm/Img_Procedi.png" onclientclick="Procedi();" />
                <div id="contenitore" 
                
                
                
                style="position: absolute; width: 642px; height: 364px; left: 14px; overflow: auto;">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="RadioButton1" runat="server" Font-Names="arial" 
                                        Font-Size="10pt" Text="Automatica (Unione di uno o più gruppi)" 
                                        Checked="True" GroupName="A" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="RadioButton2" runat="server" Font-Names="arial" 
                                        Font-Size="10pt" 
                                        
                                        Text="Manuale (usata per generare una lista di convocazione da un qualsiasi contratto convocabile)" 
                                        GroupName="A" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="RadioButton3" runat="server" Font-Names="arial" 
                                        Font-Size="10pt" Text="Importa da Excel" GroupName="A" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="RadioButton4" runat="server" Font-Names="arial" 
                                        Font-Size="10pt" Text="Importa da file csv" GroupName="A" />
                                </td>
                            </tr>
                    </table>
                        </div>
            
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
        function PaginaHome() {
            document.location.href = 'pagina_home.aspx';
        }

        function Procedi() {
            //alert('In fase di sviluppo!');
        }

        function Verifica() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                document.getElementById('H1').value = '1';
                window.open('ElencoAssegnatari.aspx?ID=' + document.getElementById('LBLID').value, '', '');
            }
            else {
                document.getElementById('H1').value = '0';
                Messaggio('Selezionare una Anagrafe Utenza dalla lista!');

            }
        }

        function Messaggio(TestoMessaggio) {

            $(document).ready(function () {
                $('#ScriptMsg').text(TestoMessaggio);
                $('#ScriptMsg').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione...', buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            });
        }
    </script>    
    </form>    

	</body>
     <script language="javascript" type="text/javascript">
         //document.getElementById('divLoading').style.visibility = 'hidden';
    </script>
</html>

