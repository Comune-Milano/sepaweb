<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Delegati.aspx.vb" Inherits="Condomini_Delegati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>

    <title>Delegati</title>
</head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoMascheraContratti.jpg); background-repeat: no-repeat">
 <script type="text/javascript"  >

        function PulisciCampi() {
        document.getElementById('txtCognome').value = ""
        document.getElementById('TxtNome').value = ""
    }
 </script>
    <form id="form1" runat="server">
    <div>
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Gestione Delegati
            <div style="left: 11px; overflow: auto; width: 703px; position: absolute; top: 56px;
            height: 347px">
            <asp:DataGrid ID="DataGridDelegati" runat="server" AutoGenerateColumns="False"
                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                PageSize="20" Style="z-index: 105; left: 193px; top: 54px" Width="678px">
                <PagerStyle Mode="NumericPages" />
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <ItemStyle ForeColor="Black" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="COGNOME">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="NOME">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="Lavender" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="#0000C0" />
            </asp:DataGrid>
        </div>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Style="left: 179px; position: absolute; top: 19px" Text="Label"
                Visible="False" Width="604px"></asp:Label>
            <asp:ImageButton ID="ImgModifica" runat="server" CausesValidation="False" EnableTheming="True"
                ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('TextBox1').value='2'"
                Style="z-index: 10; left: 718px; position: absolute; top: 76px" 
            ToolTip="Modifica la riga selezionata" />
            <asp:ImageButton ID="ImgBtnAggiungi" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                Style="left: 718px; position: absolute; top: 94px" 
            ToolTip="Elimina la riga selezionata" OnClientClick="DeleteConfirmCont()" />
            <img id="IMG1" alt="Aggiungi Delegato" onclick="document.getElementById('TextBox1').value='0';myOpacity.toggle();"
                src="../NuoveImm/Img_Aggiungi.png" style="left: 719px; width: 60px; cursor: pointer;
                position: absolute; top: 57px" />
            <div 
            style="border-color: #3366ff; border-width: thin; z-index: 201; left: 0px; visibility: visible; vertical-align: top; width: 799px; position: absolute;
                top: 0px; height: 582px; background-color: #dedede; text-align: left; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');" 
            id="DivDelegato">
                <asp:Image ID="Image1" runat="server" BackColor="White" Height="111px" ImageUrl="~/ImmDiv/DivMGrande.png"
                    Style="z-index: 100; left: 48px; position: absolute; top: 163px" Width="699px" />
                <table style="z-index: 200; left: 80px; width: 81%; position: absolute; top: 181px">
                    <tr>
                        <td style="width: 220px; height: 16px">
                            <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px">Cognome</asp:Label></td>
                        <td style="width: 220px; height: 16px">
                            <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 104; left: 10px; top: 104px" Width="146px">Nome</asp:Label></td>
                        <td style="height: 16px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 220px; height: 24px">
                            <asp:TextBox ID="txtCognome" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="25"
                                Width="95%"></asp:TextBox></td>
                        <td style="width: 220px; height: 24px">
                            <asp:TextBox ID="TxtNome" runat="server" MaxLength="25" Width="95%" Font-Names="Arial" Font-Size="8pt"></asp:TextBox></td>
                        <td style="height: 24px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 220px; height: 22px">
                        </td>
                        <td style="width: 220px; height: 22px">
                        </td>
                        <td style="height: 22px">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                TabIndex="25" ToolTip="Salva Delegato" />
                            <img id="Img2" alt="Annulla " onclick="document.getElementById('TextBox1').value!='0';myOpacity.toggle();PulisciCampi();"
                                src="../NuoveImm/Img_AnnullaVal.png" style="left: 185px; cursor: pointer; top: 23px" /></td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="TextBox1" runat="server" />
            <asp:HiddenField ID="txtConfElimina" runat="server" Value="0" />
            <asp:HiddenField ID="txtid" runat="server" />
            <asp:TextBox ID="txtmia" runat="server" BorderWidth="0px" Font-Bold="True" Font-Names="ARIAL"
                Font-Size="10pt" meta:resourcekey="TextBox3Resource1" Style="border-right: white 1px solid;
                border-top: white 1px solid; z-index: 5; left: 12px; border-left: white 1px solid;
                width: 440px; border-bottom: white 1px solid; position: absolute; top: 407px"
                Text="Nessua Voce Selezionata"></asp:TextBox>
    
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 107; left: 652px; position: absolute; top: 451px" 
        ToolTip="Home" />
        </span></strong>
    
    </div>
            <script type="text/javascript">

        myOpacity = new fx.Opacity('DivDelegato', { duration: 200 });
                                        //myOpacity.hide();
                                        
        if (document.getElementById('TextBox1').value!='2') {
            myOpacity.hide(); ;
        }

        function DeleteConfirmCont() {
            if (document.getElementById('txtid').value != 0) {
                var Conferma
                Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
                if (Conferma == false) {
                    document.getElementById('txtConfElimina').value = '0';

                }
                else {
                    document.getElementById('txtConfElimina').value = '1';

                }
            }
        }

        
        </script>

    </form>
                <script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility='hidden';
           </script>
</body>
</html>
