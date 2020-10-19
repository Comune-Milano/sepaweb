<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssegnaOperatore.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_Plan_AssegnaOperatore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Assegna Operatori</title>
</head>
<body>
    <form id="form1" runat="server">
<br/>
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
                    
                    <br />
                    <br />
    
                    <br />
                    <br />
                    <br />
                    <br />
    
                        
    
                    <br />
    
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                        Style="left: 576px; position: absolute; top: 516px; " 
                        TabIndex="4" />
    <div id="ContenitoreOperatori" 
                        
                        
                        
                        
                        
                        
                        
                        style="border: 1px solid #0000FF; overflow: auto; width: 770px; height: 141px; position:absolute; top: 74px; left: 12px;">
                        <asp:CheckBoxList style="position:absolute; top: 0px; left: 0px;" 
                        ID="ListaOperatori" runat="server" Font-Names="arial" 
                        Font-Size="9pt">
                    </asp:CheckBoxList>
    </div>
    <div id="ContenitoreVociSchema" 
                        
                        
                        
                        
                        
                        
                        
                        style="border: 1px solid #0000FF; overflow: auto; width: 770px; height: 248px; position:absolute; top: 238px; left: 12px;">
                        <asp:CheckBoxList style="position:absolute; top: 0px; left: 0px;" 
                        ID="ListaVoci" runat="server" Font-Names="arial" 
                        Font-Size="9pt">
                    </asp:CheckBoxList>
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
                    
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 516px; left: 681px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                    <br />
                    <br />
                    <br />
                    
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 499px; left: 15px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                        <asp:Label ID="lblTitolo" runat="server" 
                        style="position:absolute; top: 223px; left: 11px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt">VOCI PIANO FINANZIARIO DA ASSOCIARE AGLI OPERATORI SELEZIONATI</asp:Label>
                        <asp:Label ID="Label2" runat="server" 
                        style="position:absolute; top: 59px; left: 11px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt">OPERATORI NON ANCORA ABILITATI E CHE SI DESIDERA ABILITARE</asp:Label>
                    <br />

   

                </td>
            </tr>
     <asp:HiddenField ID="idPianoF" runat="server" />
<asp:HiddenField ID="modificato" runat="server" />
<asp:HiddenField ID="lettura" runat="server" />
        </table>
   

    </form>
    
        <script type="text/javascript">
            function ConfermaEsci() {

                if (document.getElementById('modificato').value == '1') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione, sono state fatte delle modifiche. Sei sicuro di voler uscire senza salvare?");
                    if (chiediConferma == true) {
                        document.location.href = '../../pagina_home.aspx';
                    }
                }
                else {

                    var chiediConferma
                    chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                    if (chiediConferma == true) {
                        document.location.href = '../../pagina_home.aspx';
                    }
                }
            }

           


    </script>
</body>
</html>
