<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Fornitori.aspx.vb" Inherits="MANUTENZIONI_Fornitori" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>

<head runat="server">
    <title>FORNITORI</title>
    <style type="text/css">
        #form1
        {
            height: 525px;
            width: 660px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg);
            width: 674px; position: absolute; top: 0px; z-index: 1;">
            <tr>
                <td style="width: 800px; height: 537px;">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Anagrafica Fornitori</strong></span><br />
                    <br />
                    <br />
                    &nbsp;&nbsp;<br />
                    &nbsp;&nbsp;<br />
                    &nbsp;&nbsp;&nbsp;<br />
                    &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<br />
                    <br />
                    &nbsp;<br />
                    &nbsp; &nbsp;<br />
                    &nbsp; &nbsp;
                                        <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    &nbsp;&nbsp;
                    <div style="left: 14px; overflow: auto; width: 646px; position: absolute; top: 56px;
                        height: 402px; z-index: 10;">
        <asp:DataGrid ID="DataGridFornitori" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            PageSize="15" Style="z-index: 101; left: 3px; top: 65px"
            Width="622px" Height="83px">
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="FORNITORE" ReadOnly="True" Visible="False">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="FORNITORE">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="Modifica">Seleziona</asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                            Text="Annulla"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Mode="NumericPages" Wrap="False" />
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid></div>
                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Height="16px" Style="z-index: 104; left: 15px; position: absolute;
                        top: 482px" Visible="False" Width="648px"></asp:Label>
                    <br /><img id="Img2" alt="Aggiungi Variazione ISTAT" onclick="document.getElementById('TextBox1').value='1';myOpacity.toggle();"
                        src="../NuoveImm/Img_Aggiungi.png" style="left: 450px; cursor: pointer; position: absolute;
                        top: 459px; z-index: 10;" /><asp:ImageButton ID="ImgModifica"  runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png"
                        Style="left: 512px; position: absolute; top: 459px; z-index: 10;" ToolTip="Modifica Variazione ISTAT" EnableTheming="True" CausesValidation="False" OnClientClick="document.getElementById('TextBox1').value='2';myOpacity.toggle();"/>
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server" style="left: 31px; position: absolute; top: 79px" Width="12px"></asp:TextBox><br />
                    <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                        meta:resourcekey="TextBox3Resource1" 
                        Style="border: 1px solid white; left: 12px; position: absolute; top: 458px" 
                        Text="Nessua Selezione" Width="390px"></asp:TextBox>
                    <asp:ImageButton ID="ImgBtnElimina" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                        Style="left: 572px; position: absolute; top: 459px; z-index: 10;" ToolTip="Elimina Variazione ISTAT Selezionata" />
                    &nbsp;&nbsp;&nbsp;
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    <p>
                    <asp:TextBox ID="txtid" runat="server" Style="left: 18px; position: absolute; top: 376px"
                        Width="16px"></asp:TextBox>
                    </p>
        <div id="DivFornitore" style="z-index: 20; left: 15px; width: 646px; position: absolute;
            top: 40px; height: 438px; background-color: gainsboro">
            <table style="width: 643px; height: 421px">
                <tr>
                    <td colspan ="2" style="vertical-align: top; height: 23px; text-align: left; width: 291px; ">
                        <asp:Label ID="LblTitle" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Maroon"
                            Width="345px" Font-Names="Arial" Visible="False">Gestione Anagrafica Fornitori</asp:Label></td>
                    <td style="vertical-align: top; height: 23px; text-align: left">
                    </td>
                    <td style="vertical-align: top; height: 23px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 104px; text-align: left; height: 26px;">
                        <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
                            meta:resourcekey="TextBox3Resource1" Style="border-right: white 1px solid; border-top: white 1px solid;
                            left: 12px; border-left: white 1px solid; border-bottom: white 1px solid; top: 458px;
                            background-color: transparent" Text="Fornitore" Width="98px"></asp:TextBox>
                    </td>
                    <td colspan ="3" style="vertical-align: top; text-align: left; height: 26px;">
                        <asp:TextBox ID="txtDescrizione" runat="server" Width="472px" MaxLength="100"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 104px; height: 23px;">
                    </td>
                    <td style="width: 218px; text-align: right; height: 23px;">
                        <asp:ImageButton ID="img_InserisciSchema" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                            TabIndex="9" ToolTip="Salva i dati " /></td>
                    <td style="height: 23px">
                        <asp:ImageButton ID="img_ChiudiSchema" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                            OnClientClick="document.getElementById('TextBox1').value='0';document.getElementById('DivFornitore').style.visibility='hidden';"
                            TabIndex="10" ToolTip="Annulla operazione" /></td>
                </tr>
            </table>
        </div>
        <asp:TextBox ID="txtInsMod" runat="server" Style="left: 7px; position: absolute; top: 505px"
            Width="10px"></asp:TextBox>
        
        <script type="text/javascript">

        myOpacity = new fx.Opacity('DivFornitore', { duration: 200 });
                                        //myOpacity.hide();
                                        
        if (document.getElementById('TextBox1').value!='2') {
                                             myOpacity.hide();;
        }
        </script>
        
    </form>
</body>
</html>
