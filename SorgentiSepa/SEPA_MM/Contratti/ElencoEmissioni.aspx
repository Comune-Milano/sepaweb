<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoEmissioni.aspx.vb" Inherits="Contratti_ElencoSimulazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
	Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
<head runat="server">
    <title>Elenco Simulazioni</title>
    <style type="text/css">
        #Contenitore
        {
            top: 73px;
            left: 3px;
        }
    </style>
</head>
<body>
<script type ="text/javascript">
    function ConfermaAnnullo() {
            //var chiediConferma
            //chiediConferma = window.confirm("Attenzione...Sei sicuro di voler annullare tutte le bollette presenti in questo file?");
            //if (chiediConferma == true) {
            //    document.getElementById('txtModificato').value = '111';

        //}
        myOpacity.toggle(); 
    }

</script>
    <form id="form1" runat="server" defaultbutton="btnAnnulla" 
    defaultfocus="Label3">
    <div>
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="Label1" runat="server" Text="Elenco Emissioni"></asp:Label>
                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                            Style="z-index: 101; left: 661px; position: absolute; top: 515px" 
                        ToolTip="Home" TabIndex="2" />
                    </strong></span>
                    <br />
                    <br />
                    <div id="Contenitore" 
                        style="overflow: auto; position: absolute; width: 772px; height: 381px">
                    
                    <table width="90%">
                        <tr>
                            <td style="width: 3px">
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 28px;
                                    position: static; top: 203px" Width="100%" TabIndex="1"></asp:Label></td>
                        </tr>
                    </table>
                    </div>
                    <div style="position:absolute; top: 107px; width: 561px; height: 361px; text-align: center; display: block; left: 115px; background-image: url('../../NuoveImm/Trasparente.gif'); z-index: auto;" 
                        id="AnnullaFile">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <table style="border: 2px solid #0000FF; width: 70%; background-color: #999999;">
                            <tr>
                                <td style="text-align: center; font-family: arial, Helvetica, sans-serif; font-size: 14px; font-weight: bold; color: #FFFFFF;">
                                    Sei sicuro di voler annullare la bollettazione selezionata?<br />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-family: arial, Helvetica, sans-serif; font-size: 10px; color: #FFFFFF">
                                    Attenzione, in caso di annullo è necessario, per poter concludere l&#39;operazione, 
                                    entrare nel menu &lt;Annulli&gt; e generare il file da inviare alla banca.<br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                        <asp:ImageButton ID="btnConfermaAnnullo" runat="server" ImageUrl="~/NuoveImm/Img_SI.png"
                            Style="z-index: 101; left: 661px;  top: 515px" 
                        ToolTip="Conferma Operazione" TabIndex="3" />
                    &nbsp;
                        </strong></span>
                                    <img onclick="javascript:myOpacity.toggle();document.getElementById('DaAnnullare').value='';" alt="Annulla Operazione" 
                                        src="../NuoveImm/Img_NO.png" style="cursor: pointer" /></td>
                            </tr>
                        </table>
                    </div>
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
                    <asp:HiddenField ID="DaAnnullare" runat="server" />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    <script type="text/javascript">
        myOpacity = new fx.Opacity('AnnullaFile', { duration: 200 });
        myOpacity.hide();
    </script>
</body>
<script type="text/javascript">
document.getElementById('AnnullaFile').style.visibility='hidden';
</script>
</html>
