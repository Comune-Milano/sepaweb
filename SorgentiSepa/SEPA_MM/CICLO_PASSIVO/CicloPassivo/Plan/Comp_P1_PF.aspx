<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comp_P1_PF.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_Comp_P1_PF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Inserimento Valori</title>
</head>
<body>
    <form id="form1" runat="server">
        
        <table style="left: 0px; BACKGROUND-IMAGE: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Piano Finanziario-</strong>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                    -
                    <asp:Label ID="lblStato" runat="server" style="font-weight: 700"></asp:Label>
                    </span><br />
                    <br />

                    <br />
                    <asp:Label ID="Label4" runat="server" 
                        Text="Attenzione, vengono mostrate solo le voci di competenza dell'operatore." 
                        style="position:absolute; top: 93px; left: 15px;" Font-Bold="False" 
                        Font-Italic="True" Font-Names="arial" Font-Size="8pt"></asp:Label>
                    <asp:Label ID="Label2" runat="server" 
                        Text="Indicare la voce del Piano Finanziario che si intende compilare" 
                        style="position:absolute; top: 75px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt"></asp:Label>
                    <br />
                    <asp:DropDownList ID="cmbVoci" runat="server" 
                        style="position:absolute; top: 110px; left: 15px;" Font-Names="arial" 
                        Font-Size="10pt" Width="700px" TabIndex="1" AutoPostBack="True" 
                        CausesValidation="True">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Image ID="ImgAvviso" runat="server" 
                        style="position:absolute; top: 328px; left: 10px;" ImageUrl="~/IMG/Alert.gif" 
                        Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
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
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 516px; left: 669px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                    <br />
                    <br />
                    <asp:HiddenField ID="idPianoF" runat="server" />
                    <asp:HiddenField ID="per" runat="server" />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 385px; left: 10px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    <div>
    
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 516px; " 
                        TabIndex="4" />
                        <asp:Image ID="imgAnnotazioni0" runat="server" 
                        style="position:absolute; top: 518px; left: 12px; cursor:pointer" 
                        ImageUrl="Immagini/img_Situazione.png" 
                        onclick="Situazione();"/>
    
                    <asp:Label ID="lblAvviso" runat="server" 
                        style="position:absolute; top: 328px; left: 36px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt" Visible="False">Il Piano Finanziario non è stato approvato.</asp:Label>
    
    </div>

     
    </form>
    <script type="text/javascript">
        function ConfermaEsci() {

         
                var chiediConferma
                chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                if (chiediConferma == true) {
                    document.location.href = '../../pagina_home.aspx';
                }

            }

            function Annotazioni() {

                window.open('Annotazioni.aspx?IDP=' + document.getElementById('idPianoF').value + '&P=' + document.getElementById('per').value, 'Annotazioni', '');

            }

            function Situazione() {

                window.open('Situazione.aspx?IDP=' + document.getElementById('idPianoF').value + '&P=' + document.getElementById('per').value, 'Situazione', '');

            }
    </script>
</body>
</html>

