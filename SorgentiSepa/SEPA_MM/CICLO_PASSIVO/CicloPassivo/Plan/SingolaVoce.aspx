<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SingolaVoce.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_SingolaVoce" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Singola Voce</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
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
                    <asp:Label ID="lblVoce1" runat="server" 
                        style="position:absolute; top: 181px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Completa</asp:Label>
                    <asp:Label ID="lblLordo" runat="server" 
                        style="position:absolute; top: 124px; left: 535px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">0,00</asp:Label>
                        <asp:Label ID="Label3" runat="server" 
                        style="position:absolute; top: 124px; left: 433px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Importo Lordo:</asp:Label>
                    <asp:Label ID="lblAvviso" runat="server" 
                        style="position:absolute; top: 288px; left: 88px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt" Visible="False"></asp:Label>
                    <asp:Label ID="lblVoce0" runat="server" 
                        style="position:absolute; top: 124px; left: 268px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Iva applicata</asp:Label>
                        <asp:Label ID="Label2" runat="server" 
                        style="position:absolute; top: 124px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Importo Netto in Euro</asp:Label>
                    <asp:Label ID="lblVoce" runat="server" 
                        style="position:absolute; top: 79px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            style="text-align: right;position:absolute; top: 123px; left: 161px; width: 87px;" 
                                                                   Text='0,00' Visible="False"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" 
                                            runat="server" ControlToValidate="txtImporto" Display="Dynamic" 
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                            Style="left: 254px; top: 125px; position: absolute" 
                                            ToolTip="Inserire un valore con decimale a precisione doppia" 
                                            ValidationExpression="^\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
                    <asp:DropDownList ID="cmbIva" runat="server" 
                        style="position:absolute; top: 123px; left: 359px;" Font-Names="arial" 
                        Font-Size="8pt">
                       
                    </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <asp:CheckBox ID="ChCompleto" runat="server" 
                        style="position:absolute; top: 180px; left: 117px;" Font-Names="arial" 
                        Font-Size="8pt" 
                        
                        Text=" Indica se l'operazione di inserimento importi per questa voce può considerarsi conclusa. Quando tutte le voci del Piano Finanziario saranno conplete, sarà possibile abbinare i capitoli di spese alle singole voci." 
                        AutoPostBack="True" CausesValidation="True"/>
                    <br />
    
                    <asp:ImageButton ID="ImgServizio" runat="server" ImageUrl="Immagini/40px-Crystal_Clear_action_edit_add.png"
                        Style="left: 409px; position: absolute; top: 123px; " 
                        TabIndex="4" 
                        
                        
                        
                        onclientclick="window.showModalDialog('SceltaServizio.aspx?IDV='+document.getElementById('idVoce').value+'&amp;IDP='+document.getElementById('idPianoF').value,window,'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');" />
    
                    <asp:ImageButton ID="ImgSalva" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                        Style="left: 574px; position: absolute; top: 516px; width: 60px;" 
                        TabIndex="4" />
    
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Image ID="ImgAvviso" runat="server" 
                        style="position:absolute; top: 289px; left: 59px;" ImageUrl="~/IMG/Alert.gif" 
                        Visible="False" ToolTip="Importo non approvato dal Comune di Milano" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Image ID="imgPrecedente" runat="server" 
                        style="position:absolute; top: 516px; left: 439px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_Precedente.png" 
                        onclick="Indietro();"/>
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 516px; left: 669px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                    <br />
                    <br />
                    <asp:HiddenField ID="idPianoF" runat="server" />
                    <asp:HiddenField ID="idVoce" runat="server" />
                    <asp:HiddenField ID="modificato" runat="server" />
                    <asp:HiddenField ID="suggerito" runat="server" value="0"/>
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 456px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    
        <script type="text/javascript">
        function ConfermaEsci() {


            if (document.getElementById('modificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente e perdere le modifiche effettuate?");
                if (chiediConferma == true) {
                    document.location.href = '../../pagina_home.aspx';
                }
            }
            else {
                document.location.href = '../../pagina_home.aspx';
            }

            }

            function Indietro() {

                if (document.getElementById('modificato').value == '1') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Tornare alla pagina precedente e perdere le modifiche effettuate?");
                    if (chiediConferma == true) {
                        document.location.href = 'Comp_P1_PF.aspx?ID=' + document.getElementById('idPianoF').value;
                    }
                }
                else {
                    document.location.href = 'Comp_P1_PF.aspx?ID=' + document.getElementById('idPianoF').value;
                }

            }

            var r = {
                'special': /[\W]/g,
                'quotes': /['\''&'\"']/g,
                'notnumbers': /[^\d\-\.\,]/g
            }
            function valid(o, w) {
                o.value = o.value.replace(r[w], '');
                document.getElementById('modificato').value = '1';
            }

            function AutoDecimal2(obj) {
                if (obj.value.replace(',', '.') > 0) {
                    var a = obj.value.replace(',', '.');
                    a = parseFloat(a).toFixed(2)
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }

            }
    </script>
</body>
</html>
