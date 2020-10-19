<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoAllegato.aspx.vb" Inherits="Contratti_Report_NuovoAllegato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self"/>
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 27px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <table align="center" style="width: 80%;">
        <tr>
            <td align="center">
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="12pt">ALLEGATI ACCERTATO</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Tipologia Allegato"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="font-family: arial; font-size: 8pt; font-weight: normal" 
                valign="middle">
    
                    <asp:DropDownList ID="cmbTipoAllegato" runat="server" 
                         Font-Names="arial" 
                        Font-Size="10pt" Width="500px" TabIndex="1">
                    </asp:DropDownList>
                    &nbsp;
    
                    <asp:ImageButton ID="ImgNuovo" runat="server" ImageUrl="../../NuoveImm/Aggiungi.png"
                        TabIndex="2" 
                        onclientclick="window.showModalDialog('../../NuovoTipoAllegato.aspx?T='+document.getElementById('tipo').value,window,'status:no;dialogWidth:400px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');" 
                        ToolTip="Aggiungi nuova tipologia documento" style="width: 18px" />
    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImgElimina" runat="server" ImageUrl="../../NuoveImm/Elimina.png"
                         
                        TabIndex="5" onclientclick="Sicuro();" ToolTip="Elimina tipo documento" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Data Emissione"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                        <asp:TextBox ID="txtData" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; "
                            TabIndex="2" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtData"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                        
                            
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Riferimento Da"></asp:Label>
                        </td>
        </tr>
        <tr>
            <td>
                        <asp:TextBox ID="txtRiferimentoDa" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; "
                            TabIndex="3" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                    runat="server" ControlToValidate="txtRiferimentoDa"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"
                                                        
                            
                            
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Riferimento A"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                        <asp:TextBox ID="txtRiferimentoA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; "
                            TabIndex="4" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                    runat="server" ControlToValidate="txtRiferimentoA"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"
                                                        
                            
                            
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Allegato"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
            
            
                        Style=" width: 600px;" 
                        TabIndex="5" />
                    </td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp;<br/><br/>  </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                         
                        TabIndex="6" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <img id="imgEsci" alt="Esci" src="../../NuoveImm/Img_EsciCorto.png" onclick="self.close();" style="cursor:pointer" /></td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:HiddenField ID="tipo" runat="server" />
    <asp:HiddenField ID="cod" runat="server" />
    <asp:HiddenField ID="sicuro" runat="server" />
    
    <script type="text/javascript">
        function Sicuro() {


            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sicuro di voler eliminare la tipologia selezionata?");
            if (chiediConferma == true) {
                document.getElementById('sicuro').value = '1';
            }
            else {
                document.getElementById('sicuro').value = '0';
            }

        } 
    
    </script>
    
    <script type="text/javascript">
        //document.onkeydown = $onkeydown;


        function CompletaData(e, obj) {
            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }


</script>
    </form>
</body>
</html>

