<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ApplicaCanone.aspx.vb" Inherits="ANAUT_ApplicaCanone" %>

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
                            Canone da Anagrafi Utenza </strong>
                        <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
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
                        <asp:DropDownList ID="cmbAnno" runat="server" 
                            style="position:absolute; top: 150px; left: 388px;" TabIndex="1">
                        </asp:DropDownList>
                        <br />
                        <asp:TextBox ID="txtNonFatti" runat="server" 
                            style="position:absolute; top: 370px; left: 17px; width: 597px; height: 78px;" 
                            ReadOnly="True" TextMode="MultiLine" Visible="False"></asp:TextBox>
                        <br />
                        <asp:TextBox ID="TextBox1" runat="server" 
                            style="position: absolute; top: 242px; left: 18px; width: 608px; height: 105px;" 
                            TextMode="MultiLine"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Arial" 
                            Font-Size="10pt" style="position: absolute; top: 190px; left: 16px;" 
                            Text="Se desideri applicare il canone ad una determinata serie di contratti inserisci i codici separati da , (virgola)"></asp:Label>
                        <br />
                        <br />
       <asp:ImageButton ID="ImageButton1" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Procedi.png" 
                         TabIndex="3" 
                            style="height: 20px;position:absolute; top: 484px; left: 492px;" />

                       
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="Label10" runat="server" Text="Label" 
                            style="position:absolute; top: 88px; left: 16px;" Font-Bold="True" 
                            Font-Names="Arial" Font-Size="10pt"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="Applica il canone di competenza e relativi contributi del" 
                            style="position:absolute; top: 152px; left: 16px;" Font-Bold="True" 
                            Font-Names="Arial" Font-Size="10pt"></asp:Label>
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
                onclick="PaginaHome();"/>&nbsp;
       
                       
    <script type="text/javascript" language="javascript">
        function PaginaHome() {
            document.location.href = 'pagina_home.aspx';
        }

        function Apri() {
            window.open('ApplicazioneGruppoAU0.aspx?T=' + document.getElementById('H1').value, '', '');
        }
    </script>    
    </form>    

	</body>
    <script  language="javascript" type="text/javascript">
        //document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>
</html>
