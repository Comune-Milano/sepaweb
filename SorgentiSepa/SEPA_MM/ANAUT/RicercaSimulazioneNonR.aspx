<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaSimulazioneNonR.aspx.vb" Inherits="ANAUT_RicercaSimulazioneNonR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        //if (event.keyCode==13) 
        //{  
        //alert('Usare il tasto <Avvia Ricerca>');
        //history.go(0);
        //event.keyCode=0;
        //}  
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Ricerca</title>
			</head>
	<body bgColor="#f2f5f1" >
			<form id="Form1" method="post" runat="server">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 670px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Simulazione Rapporti non Rispondenti</strong></span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:CheckBox ID="chSospese" runat="server" 
                            style="position:absolute; top: 169px; left: 25px; width: 274px;" 
                            Font-Bold="True" Font-Names="arial" Font-Size="10pt" 
                            Text="Includi gli appuntamenti Sospesi" Checked="True"/>
                            <asp:CheckBox ID="ChDiffidati" runat="server" 
                            style="position:absolute; top: 205px; left: 25px; width: 274px;" 
                            Font-Bold="True" Font-Names="arial" Font-Size="10pt" 
                            Text="Escludi i non diffidati" Checked="True"/>
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
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton 
                ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 100; left: 539px; position: absolute; top: 364px" 
                TabIndex="6" ToolTip="Home" />
            <img id="imgAvanti" alt="Procedi" src="../NuoveImm/Img_Procedi.png" 
                            onclick="Conferma()" 
                            
                style="position:absolute;cursor:pointer; top: 365px; left: 422px;"/>
                <asp:label id="lblAvviso" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                
                style="z-index: 106; left: 27px; position: absolute; top: 95px; width: 608px; height: 42px;" 
                ForeColor="Black">Sarà effettuata la simulazione di calcolo del canone L.R. 27 sui contratti CONVOCATI e che NON hanno risposto all&#39;anagrafe dell&#39;utenza.</asp:label>
		</form>
        <script  language="javascript" type="text/javascript">
            function Conferma() {
                window.open('SimulazioneApplicazioneNON.aspx?S=' + document.getElementById('chSospese').checked + '&D=' + document.getElementById('ChDiffidati').checked, '', '');
            }
        </script>
	</body>
</html>
