<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DividiImportoScale.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_Plan_DividiImportoScale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self"/>
    <title>Divisione Importo</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="left: 0px; BACKGROUND-IMAGE: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px; position: absolute; top: 0px; height: 596px;">
            <tr><td style="width: 706px">
                    <br />
                    &nbsp; <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <strong>Divisione Importo Voce per Scale</strong></span><br />
                    <br />
                    <br />
                    <asp:Label ID="lblServizio14" runat="server" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt" 
                        style="position:absolute; top: 154px; left: 14px; font-style: italic;">Edificio</asp:Label>
                    <asp:Label ID="lblServizio13" runat="server" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt" 
                        style="position:absolute; top: 89px; left: 400px; font-style: italic;" 
                        Visible="False">Complesso</asp:Label>
                    <asp:Label ID="lblServizio12" runat="server" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt" 
                        style="position:absolute; top: 105px; left: 14px; font-style: italic;">Lotto</asp:Label>
                    <asp:Label ID="lblServizio11" runat="server" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt" 
                        style="position:absolute; top: 53px; left: 14px; font-style: italic;">Voce Business Plan</asp:Label>
                    <asp:Label ID="lblEdificio" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="9pt" Font-Strikeout="False" 
                        style="position:absolute; top: 179px; left: 15px;" ForeColor="Black"></asp:Label>
                    <asp:Label ID="lblImporto" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="9pt" Font-Strikeout="False" 
                        style="position:absolute; top: 117px; left: 401px;" ForeColor="Black"></asp:Label>
                    <asp:Label ID="lblLotto" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="9pt" Font-Strikeout="False" 
                        style="position:absolute; top: 123px; left: 14px;"></asp:Label>
                    <asp:Label ID="lblVoce" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="9pt" style="position:absolute; top: 72px; left: 14px;"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SalvaGrande.png" 
                        Style="left: 576px; position: absolute; top: 546px; " TabIndex="4" 
                        onclientclick="Avvisa();" />
                    <div id="ContenitoreVoci" 
                        
                        
                        
                        
                        
                        
                        style="border: 2px solid #0000FF; overflow: auto; width: 770px; height: 248px; position:absolute; top: 271px; left: 14px;">
                        <asp:DataGrid ID="DataGridVoci" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" GridLines="None" PageSize="50" 
                            style="z-index: 105; top: 0px; left: 0px; width: 98%; ">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID_SCALA" 
                                    ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="DENOMINAZIONE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="IMPORTO_CANONE_LORDO" HeaderText="IMPORTO" ReadOnly="True" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO_CONSUMO_LORDO" HeaderText="IMPORTO CONSUMO" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="IMPORTO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtImportoCanone" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            style="text-align: right;" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_CANONE_LORDO") %>' 
                                            Width="70px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" 
                                            runat="server" ControlToValidate="txtImportoCanone" Display="Dynamic" 
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                            Style="left: 144px; top: 80px" 
                                            ToolTip="Inserire un valore con decimale a precisione doppia" 
                                            ValidationExpression="^\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="IMPORTO CONSUMO" Visible="False">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtImportoConsumo" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" style="text-align: right;" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_CONSUMO_LORDO") %>' 
                                            Width="70px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator81" 
                                            runat="server" ControlToValidate="txtImportoConsumo" Display="Dynamic" 
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                            Style="left: 144px; top: 80px" 
                                            ToolTip="Inserire un valore con decimale a precisione doppia" 
                                            ValidationExpression="^\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" 
                                Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                                Font-Underline="False" ForeColor="#0000C0" />
                        </asp:DataGrid>
                    </div>
                    <asp:Label ID="lblServizio15" runat="server" Font-Bold="True" 
                        Font-Names="arial" Font-Size="9pt" 
                        style="position:absolute; top: 238px; left: 14px; font-style: italic;">Importo da dividere (IVA compresa) Euro:</asp:Label>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                        Font-Names="arial" Font-Size="9pt" 
                        style="position:absolute; top: 237px; left: 368px; font-style: italic;">Importo Restante da dividere (IVA compresa) Euro:</asp:Label>
                        <asp:Label ID="lblDaDividere" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="9pt" Font-Strikeout="False" 
                        style="position:absolute; top: 236px; left: 673px; width: 110px;" 
                        ForeColor="#CC3300"></asp:Label>
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
                    <asp:Image ID="imgEsci" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();" 
                        style="position:absolute; top: 546px; left: 669px; cursor:pointer" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 530px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />
                </td>
            </tr>
     <asp:HiddenField ID="idVoce" runat="server" />
    <asp:HiddenField ID="idLotto" runat="server" />
    <asp:HiddenField ID="txtmodificato" runat="server" />
    <asp:HiddenField ID="idServizio" runat="server" />  
<asp:HiddenField ID="idPianoF" runat="server" />
<asp:HiddenField ID="indietro" runat="server" />
<asp:HiddenField ID="IDVS" runat="server" />
<asp:HiddenField ID="IDC" runat="server" />
<asp:HiddenField ID="IMP" runat="server" />
<asp:HiddenField ID="IDE" runat="server" />
<asp:HiddenField ID="CHIUDI123" runat="server" Value="0" />
<asp:HiddenField ID="CARICAFUNZIONERESTO" runat="server" Value="0" />
        </table>

                    <asp:Label ID="lblImporto0" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="9pt" Font-Strikeout="False" 
                        style="position:absolute; top: 237px; left: 257px; width: 100px;" 
                ForeColor="#CC3300"></asp:Label>

    </div>
    <script type="text/javascript">
//        //       alert('1');
//               if (document.getElementById('CHIUDI123').Value == '1') {
//                   alert('ora chiudo');
//                   self.close();
//               }
    </script>
    </form>
    
    <script type="text/javascript">


        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\.\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            document.getElementById('txtmodificato').value = '1';
        }

        function AutoDecimal2(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }

        }
        
        function ConfermaEsci() {

            if (document.getElementById('txtmodificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione, sono state fatte delle modifiche. Sei sicuro di voler uscire senza salvare?");
                if (chiediConferma == true) {
                    self.close();
                }
            }
            else {

                var chiediConferma
                chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                if (chiediConferma == true) {
                    self.close();
                }
            }
        }

        function Dividi(Indice) {
            if (document.getElementById('txtmodificato').value == '1') {
                alert('Attenzione...Sono state apportate delle modifiche. Salvare prima di continuare!');
            }
            else {
                if (Indice == '-1') {
                    alert('Attenzione, inserire un importo diverso da 0,00, salvare e quindi dividere!');
                }
                else {
                   window.showModalDialog('DividiImportoEdifici.aspx?IDVS=' + indice + '&IDV=' + document.getElementById('idVoce').value + '&IDP=' + document.getElementById('idPianoF').value + '&IDS=' + document.getElementById('idServizio').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
                }
            }
       }

       function Avvisa() {
           if (parseFloat(document.getElementById('lblDaDividere').innerText) < 0) {
               alert('Attenzione...è stato diviso un importo superiore a quello disponibile!!\nDividere correttamente gli importi altrimenti il business plan non sarà approvato!');
           }
       }




    </script>
</body>



</html>
