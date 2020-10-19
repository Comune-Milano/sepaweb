<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SitGenerale.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_ConvalidaComune" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
    <title>Convalida Comune</title>
    <style type="text/css">
        #contenitore
        {
            height: 224px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="left: 0px; BACKGROUND-IMAGE: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px; height: 596px;">
            <tr>
                <td style="width: 706px">
                   <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Piano Finanziario-</strong>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                    -
                    <asp:Label ID="lblStato" runat="server" style="font-weight: 700"></asp:Label>
                    <br />
                    </span><br />
                    <br />
                    <br />
                                        <div id="cont" 
                        
                        style="position: absolute; width: 747px; height: 447px; top: 55px; left: 26px; overflow: scroll;"><%=Tabella%>
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
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 540px; left: 18px;"></asp:Label>
                    <br />
                    <br />
                    <br />
                    
                    <asp:Image ID="imgEventi" runat="server" onclick="ConfermaEventi();"
                        style="position:absolute; top: 518px; left: 25px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_Eventi_Grande.png" />
                    <asp:Image ID="imgStampa" runat="server" onclick="ConfermaStampa();"
                        style="position:absolute; top: 518px; left: 116px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_Stampa_Grande.png" />
                        <asp:ImageButton ID="imgEsportaXLS" runat="server"
                        style="position:absolute; top: 518px; left: 223px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_Export_Grande.png" />
                    <asp:Image ID="imgEsci" runat="server" onclick="ConfermaEsci();"
                        style="position:absolute; top: 517px; left: 699px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png"/>
                    <br />
                    <br />
                    <br />
                    
                    <br />
                    <br />

   

                    <asp:HiddenField ID="idPianoF" runat="server" />

                    <asp:HiddenField ID="per" runat="server" />
                    <asp:HiddenField ID="modificato" runat="server" />
                    <asp:HiddenField ID="salvaok" runat="server" Value="0" />
                    <asp:HiddenField ID="stato" runat="server" Value="0" />

   

                    <asp:HiddenField ID="convalidaok" runat="server" />

   

                </td>
            </tr>
 
        </table>
   

<div id="Convalida" 
            
            
            
            
            
            style="position: absolute; z-index: 400; top: 0px; left: 0px; width: 800px; height: 600px; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; visibility: hidden;">
       
           &nbsp;</div>
       
        
            <script type="text/javascript">

                
                                
        </script>
    </form>
    
    <script type="text/javascript">

        function ConfermaEventi() {
            
                window.open('EventiPF.aspx?ID=' + document.getElementById('idPianoF').value + '&P=' + document.getElementById('per').value, 'Eventi', '');

            }

            function ConfermaStampa() {
               
                    window.open('StampaPF.aspx?T=1&ID=' + document.getElementById('idPianoF').value + '&P=' + document.getElementById('per').value, 'Stampa', '');

                }

                function ConfermaEsci() {
                    document.location.href = '../../pagina_home.aspx';
                }
        
    </script>
</body>
</html>
