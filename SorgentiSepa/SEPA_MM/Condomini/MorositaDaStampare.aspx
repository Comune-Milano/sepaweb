<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MorositaDaStampare.aspx.vb" Inherits="Condomini_MorositaDaStampare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>

    <title>Eventi Patrimoniali</title>

    <style type="text/css">
        #form1
        {
            width: 784px;
        }
    </style>

</head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoMascheraContratti.jpg); background-repeat: no-repeat">
       <script type="text/javascript">


           function AutoDecimal(obj) {
               if (obj.value.replace(',', '.') > 0) {
                   var a = obj.value.replace(',', '.');
                   a = parseFloat(a).toFixed(4)
                   document.getElementById(obj.id).value = a.replace('.', ',')
               }
           }
           function AutoDecimal2(obj) {
               if (obj.value.replace(',', '.') > 0) {
                   var a = obj.value.replace(',', '.');
                   a = parseFloat(a).toFixed(2)
                   document.getElementById(obj.id).value = a.replace('.', ',')
               }
           }
           function PaymentConfirm() {
               if (document.getElementById('idGestione').value != 0) {
                   var Conferma
                   Conferma = window.confirm("Attenzione...Confermi di voler emettere un pagamento?");
                   if (Conferma == false) {
                       document.getElementById('txtConferma').value = '0';
                       document.getElementById('idGestione').value = '0';
                       document.getElementById('idCond').value = '0';
                       document.getElementById('Importo').value = '0';

                   }
                   else {
                       document.getElementById('txtConferma').value = '1';

                   }
               }
           }

   </script>

    <form id="form1" runat="server">
    <div>
    
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        Morosità da Emettere
            <asp:HiddenField ID="idGestione" runat="server" Value="0" />
            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="8pt" ForeColor="Red" Height="18px" Style="z-index: 104; left: 16px;
            position: absolute; top: 524px" Visible="False" Width="548px"></asp:Label>
            <asp:HiddenField ID="txtConferma" runat="server" Value="0" />
            <asp:HiddenField ID="txtNRata" runat="server" Value="0" />
            <asp:HiddenField ID="txtScadenza" runat="server" Value="0" />
            <br />
        </span></strong>
    
    </div>
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
            <div style="left: 11px; overflow: auto; width: 768px; position: absolute; top: 57px;
            height: 452px">
            <asp:DataGrid ID="DataGridMorDaStampare" runat="server" AutoGenerateColumns="False"
                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                PageSize="20" Style="z-index: 105; left: 193px; top: 54px" Width="99%" 
                    CellPadding="1" CellSpacing="1">
                <PagerStyle Mode="NumericPages" />
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <ItemStyle ForeColor="Black" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" 
                        Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="CONDOMINIO">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="AMMINISTRATORE" HeaderText="AMMINISTRATORE">
                    </asp:BoundColumn>
                </Columns>
                <HeaderStyle BackColor="Lavender" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="#0000C0" />
            </asp:DataGrid>
        </div>
    
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 107; left: 720px; position: absolute; top: 546px; height: 20px;" 
        ToolTip="Home" />
        </span></strong>
    
    <asp:HiddenField ID="idCond" runat="server" />
    

    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
    
            <asp:HiddenField ID="Importo" runat="server" Value="0" />
        </span></strong>
           <script  language="javascript" type="text/javascript">
                    document.getElementById('dvvvPre').style.visibility = 'hidden';
           </script>

    </form>


            
                    </body>
</html>