<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RichiestaAutogest.aspx.vb" Inherits="GestioneAutonoma_RichiestaAutogest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagina senza titolo</title>
    <style type="text/css">

        .style2
        {
            width: 426px;
            height: 80%;
        }
        .style3
        {
            width: 4px;
            height: 85px;
        }
        .style4
        {
            height: 85px;
        }
        .style5
        {
            width: 4px;
        }
        </style>
            <script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>

</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat :no-repeat ">
        <!-- Da mettere subito dopo l'apertura del tag <body> -->

      <div id="splash"      
          
        
        
        style=" border: thin dashed #000066; position :absolute; z-index :350; text-align:center; font-size:10px; width: 85%; height: 76%; vertical-align: top; line-height: normal; top: 52px; left: 58px; background-color:#FFFFFF ;">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <img src='Immagini/load.gif' alt='caricamento in corso'/><br/><br/>
            caricamento in corso...<br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            &nbsp;</div>    
    <% Response.Flush %>
    <form id="form1" runat="server" >
    <div>
        <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
            ForeColor="#660000" 
            Text="Richiesta di Gestione Autonoma" Width="759px"></asp:Label>
        
        <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa_Grande.png"

            Style="z-index: 111; left: 535px; position: absolute; top: 335px; bottom: 139px; " TabIndex="4"
            ToolTip="Stampa Richiesta" Visible="False" />
            <asp:ImageButton ID="btnAnnulla" runat="server" 
            ImageUrl="~/NuoveImm/Img_Home.png" Style="z-index: 106;
                left: 635px; position: absolute; top: 335px; bottom: 139px;" TabIndex="5" 
            ToolTip="Home" height="20" OnClientClick ="document.getElementById('splash').style.visibility = 'visible'" />
        &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 14px; position: absolute; top: 300px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
        <div id="divRicImmobile" style="border: thin solid gainsboro; left: 13px; vertical-align: top; width: 643px;
            position: absolute; top: 70px; height: 109px;
            text-align: left">
            <table width="100%">
                <tr>
                    <td class="style3">
                <div style="border: thin solid #6699ff; z-index: 10; width: 605px; visibility :visible;
                height: 77px; vertical-align: top; text-align: left; overflow: auto;" 
                    id="DivEdifici">
                <asp:Label ID="lblEdifici" runat="server" Font-Names="Arial" Font-Size="9pt" 
                    Width="95%"></asp:Label></div>
                    </td>
                    <td style="text-align: left; vertical-align: top" class="style5">
                        <asp:Image ID="imgAddEdificio" runat="server" 
                    ImageUrl="~/GestioneAutonoma/Immagini/k-hex-edit-icon.png"
                    onclick="document.getElementById('ScegEdifVis').value!='1';myOpacityEdif.toggle();" 
                    ToolTip="Aggiungi/Rimuovi Edificio alla lista" style="cursor: hand;" />
            
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">

  
        <asp:ImageButton ID="btnVerifica" runat="server" 
                            ImageUrl="~/GestioneAutonoma/Immagini/ImgVerifica.png" TabIndex="4"
            ToolTip="Verifica i dati dell'immobile selezionato" OnClientClick="document.getElementById('splash').style.visibility = 'visible'" />

                    </td>
                    <td style = "text-align:right ">

  
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <asp:Label ID="lbltit" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 17px; position: absolute; top: 53px"
            Width="504px">Selezionare l'immobile per la stampa della  richiesta  di autogestione</asp:Label>
        <br />
        <br />
        <br />
        <div id="divMorosita" style="border: thin solid gainsboro; left: 14px; vertical-align: top; width: 643px;
            position: absolute; top: 211px; text-align: left">
            <table cellpadding="1" cellspacing="1" width="100%">
                <tr>                    <td style="width: 363px">
                        <em><span style="font-size: 11pt; border-bottom-width: 1px; border-bottom-color: #000033;
                            font-family: Arial">Morosità percentuale Immobile Selezionato&nbsp;</span></em></td>
                    <td style="vertical-align: top; width: 53px; text-align: right">
                        <asp:Label ID="lblPercentuale" runat="server" Font-Bold="True" Font-Italic="True"
                            Font-Names="Arial" Font-Size="11pt" Style="z-index: 100; left: 478px; top: 251px"
                            Width="57px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 363px">
                        <em><span style="font-size: 11pt; border-bottom-width: 1px; border-bottom-color: #000033;
                            font-family: Arial">Percentuale Occupazioni Abusive&nbsp;</span></em></td>
                    <td style="vertical-align: top; width: 53px; text-align: right">
                        <asp:Label ID="lblPercOccAbu" runat="server" Font-Bold="True" Font-Italic="True"
                            Font-Names="Arial" Font-Size="11pt" Style="z-index: 100; left: 478px; top: 251px"
                            Width="57px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 363px">
                    </td>
                    <td style="vertical-align: top; width: 53px; text-align: right">
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="txtvisibility" runat="server" Value="1" />

        <asp:HiddenField ID="ScegEdifVis" runat="server" Value="1" />

        <br />
</div>
                <script type="text/javascript">

                if (document.getElementById('txtvisibility').value != '2') {
                    document.getElementById('divMorosita').style.visibility='hidden';

                    }
             else {
                    document.getElementById('divMorosita').style.visibility='visible';

                    }
                 </script>

  
        <div 
        style="border: thin solid #6699ff; z-index: 300; left: 18px; width: 612px; position: absolute; visibility :hidden;
            top: 81px; height: 329px; vertical-align: top; background-color: #DCDCDC; text-align: left;" 
        id="ScegliEdifici">
            <table style="width: 99%; height: 91%">
                <tr>
                    <td style="vertical-align: top; width: 426px;height: 2%; text-align: left">
        <asp:Label ID="lblSituazione0" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            >Elenco Complessi Immobiliari</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 426px; height: 2%; text-align: left">
                        <asp:DropDownList ID="cmbComplessi" runat="server" 
                            ToolTip="Elenco dei Complessi Immobiliari" Width="98%" Font-Names="Arial" 
                            Font-Size="8pt" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 426px; height: 2%; text-align: left">
        <asp:Label ID="lblSituazione1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            >Elenco Edifici</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: left" class="style2">
                        <div style="overflow: auto; width: 100%; height: 80%">
            <asp:CheckBoxList ID="ListEdifici" runat="server" Font-Names="Arial" Font-Size="8pt"
                Style="left: 334px; top: 251px" Width="403px">
            </asp:CheckBoxList></div>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: right; ">
                        <asp:ImageButton ID="imgAggiornaEdifici" runat="server" ImageUrl="~/Condomini/Immagini/Aggiungi.png"
            
            Style="z-index: 103; left: 744px; cursor: pointer; top: 26px" 
            ToolTip="Esci" CausesValidation="False" OnClientClick="document.getElementById('splash').style.visibility = 'visible'" /></td>
                </tr>
            </table>
        </div>

        <script  language="javascript" type="text/javascript">
          myOpacityEdif = new fx.Opacity('ScegliEdifici', { duration: 200 });
            if (document.getElementById('ScegEdifVis').value != '2') {
                myOpacityEdif.hide(); ;
            }
        </script>
    </form>
        <script type="text/javascript"  language="JavaScript">
            document.getElementById('splash').style.visibility = 'hidden';
        </script>
</body>
</html>
