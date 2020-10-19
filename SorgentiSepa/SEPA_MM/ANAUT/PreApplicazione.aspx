<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PreApplicazione.aspx.vb" Inherits="ANAUT_PreApplicazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
    var Selezionato;
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
    		
	    <style type="text/css">
            #contenitore
            {
                top: 97px;
            }
        </style>
        <title>RisultatoRicercaD</title>
	</head>
	<body bgcolor="#f2f5f1">
		<form id="Form1" method="post" runat="server" >
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Applicazione
                            Anagrafi Utenza </strong>
                        </span><br />
                        <br />
                        <br />
                            &nbsp;&nbsp;
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
                        <asp:Label ID="Label10" runat="server" Text="Label" 
                            style="position:absolute; top: 88px; left: 16px;" Font-Bold="True" 
                            Font-Names="Arial" Font-Size="10pt"></asp:Label>
                            <asp:CheckBox ID="chSospese" runat="server" 
                            style="position:absolute; top: 169px; left: 13px; width: 274px;" 
                            Font-Bold="True" Font-Names="arial" Font-Size="10pt" 
                            Text="Includi gli appuntamenti Sospesi" Checked="True" Visible="False"/>
                            <asp:CheckBox ID="ChDiffidati" runat="server" 
                            style="position:absolute; top: 208px; left: 13px; width: 274px;" 
                            Font-Bold="True" Font-Names="arial" Font-Size="10pt" 
                            Text="Escludi i non diffidati" Checked="True" Visible="False"/>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:HiddenField ID="H1" runat="server" Value="" />
                    </td>
                </tr>
            </table>
            &nbsp;<img alt="Torna alla pagina principale" 
                        src="../NuoveImm/Img_Home.png" id="imgEliminafiliale" 
                        
                style="cursor:pointer;left: 598px; position: absolute; top: 484px; height: 20px;" 
                onclick="PaginaHome();"/>
                <img alt="Procedi" 
                        src="../NuoveImm/Img_Procedi.png" id="img1" 
                        
                style="cursor:pointer;left: 490px; position: absolute; top: 484px; height: 20px;" onclick="Apri();"
                />
       

                       
    <script type="text/javascript" language="javascript">
        function PaginaHome() {
            document.location.href = 'pagina_home.aspx';
        }

        function Apri() {
            if (document.getElementById('H1').value == '2') {
                window.open('ApplicazioneGruppoAU0.aspx?S=' + document.getElementById('chSospese').checked + '&D=' + document.getElementById('ChDiffidati').checked + '&T=' + document.getElementById('H1').value, '', '');
            }
            else {
                window.open('ApplicazioneGruppoAU0.aspx?T=' + document.getElementById('H1').value, '', '');
            }
        }
    </script>    
    </form>    

	</body>
    <script  language="javascript" type="text/javascript">
        //document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>
</html>
