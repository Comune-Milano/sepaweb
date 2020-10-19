<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovaListaExcel.aspx.vb" Inherits="ANAUT_NuovaListaExcel" %>

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
            .bottone
        {
            /*background-color: #507cd1;     border-style: none;     color: White;     font-weight: bold;     font-size: 8pt;     height: 25px;     cursor: pointer;     */
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 10pt;
            height: 20px;
            cursor: pointer;
        }
        
        </style>
        <title>Convocabili</title>
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
                            Text="Crea Lista Convocazione da file Excel"></asp:Label>
                        </span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                <table style="border: thin solid #3399FF;" width="100%">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUpload" runat="server" Font-Names="Arial" Font-Size="10pt"
                                CssClass="CssMaiuscolo" Height="20" Width="500px" size="60" />
                        </td>
                    </tr>
                </table>
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
                        <img style="position:absolute; top: 298px; left: 15px;" alt="" 
                            src="Esempio.jpg" /><img 
                            style="position:absolute; top: 298px; left: 352px;" alt="" 
                            src="Esempio_1.jpg" /><br />
                        <br />
                        <asp:HiddenField ID="LBLID" runat="server" Value="0" />
                        <asp:HiddenField ID="npg" runat="server" Value="" />
                        
                        <asp:Label ID="Label25" runat="server" 
                            Text="ATTENZIONE...Il file deve essere IN FORMATO XLS e strutturato come mostrato in figura e il nome del foglio deve essere &quot;RU&quot;. I rapporti devono essere comunque tra i convocabili e non appartenenti a nessuna lista di convocazione." 
                            style="position:absolute; top: 263px; left: 14px;" ForeColor="Maroon" 
                            Font-Names="arial" Font-Size="8pt"></asp:Label>
                        
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
                ImageUrl="~/NuoveImm/Img_Procedi.png" 
                onclientclick="document.getElementById('divLoading').style.visibility = 'visible';" />
            
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
    <div id="divLoading" 
                style="position:absolute;margin: 0px; width: 100%; height: 100%;top: 0px; left: 0px;background-color: #ffffff;z-index:1000; visibility: hidden;"><div style="position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"><table style="width: 100%; height: 100%;"><tr><td valign="middle" align="center"><img src="../NuoveImm/load.gif" alt="Caricamento in corso" /><br /><br /><span id="Label4" style="font-family:Arial;font-size:10pt;">Caricamento in corso...</span></td></tr></table></div></div>
    <script type="text/javascript" language="javascript">
        function PaginaHome() {
            document.location.href = 'pagina_home.aspx';
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
        
    </script>
</html>


