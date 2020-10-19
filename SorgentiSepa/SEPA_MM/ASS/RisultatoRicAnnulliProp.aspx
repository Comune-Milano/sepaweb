<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoRicAnnulliProp.aspx.vb" Inherits="ASS_RisultatoRicAnnulliProp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;


</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>

<head id="Head1" runat="server">
    <title>Offerte Alloggio</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            text-align: center;
        }
    </style>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server" defaultbutton="btnAccetta" 
    defaultfocus="DataGrid1">
    
            <div id="Motivazione"  
                
                
                style="border: 1px solid #000080; position:absolute; width: 675px; height: 538px; top: 8px; left: 0px; background-color: #E2E2E2; z-index: 300; text-align: center;">
                <br />
                <br />
                <br />
                <br />
                <br />
            <table width="50%">
                <tr>
                    <td class="style1">
                        ANNULLAMENTO PROPOSTA MOTIVATO DA</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton1" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="OCCUPAZIONE ABUSIVA" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton2" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="ALTRO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton3" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="ALLOGGIO INAGIBILE O NON IDONEO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton4" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="CANONE NON SOPPORTABALE" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton5" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="ERRORE UFFICIO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton6" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="BARRIERE ARCHITETTONICHE" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td>
        <asp:Button ID="btnAccetta" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
            Height="35px" Style="z-index: 100; "
            Text="CONFERMA" Width="181px" 
                        OnClientClick="AnnullaProposta();" 
                        TabIndex="1" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAccetta0" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
            Height="35px" Style="z-index: 100; "
            Text="ANNULLA" Width="73px" 
                        TabIndex="1" />
                    </td>
                </tr>
            </table>
    </div>
    <div>
        <div>
            <asp:Label ID="LBLID" runat="server" Height="21px" Style="z-index: 100; left: 21px;
                position: absolute; top: 392px" Visible="False" Width="78px">Label</asp:Label>
            <asp:Label ID="LBLPROGR" runat="server" Height="23px" Style="z-index: 101; left: 109px;
                position: absolute; top: 397px" Visible="False" Width="57px">Label</asp:Label>
            <asp:Label ID="lblScad" runat="server" Height="23px" Style="z-index: 102; left: 180px;
                position: absolute; top: 388px" Visible="False" Width="57px">Label</asp:Label>
            &nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                            Offerte Trovate nei rispettivi bandi&nbsp; </strong>
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                        </span><br />
                        <br />
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="Label27" runat="server" 
                            Text="CAMBI EMERGENZA (ART.22 C.10 RR 1/2004)" 
                            style="position:absolute; top: 331px; left: 3px;" Font-Bold="True" 
                            Font-Names="ARIAL" Font-Size="9pt"></asp:Label>
                        <asp:Label ID="Label28" runat="server" Text="BANDO ERP" 
                            style="position:absolute; top: 55px; left: 3px;" Font-Bold="True" 
                            Font-Names="ARIAL" Font-Size="9pt"></asp:Label>
                        <asp:Label ID="Label26" runat="server" Text="BANDO CAMBI" 
                            style="position:absolute; top: 204px; left: 3px;" Font-Bold="True" 
                            Font-Names="ARIAL" Font-Size="9pt"></asp:Label>
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
                        <img alt="Annulla" id="imgAnnulla" alt="" src="../NuoveImm/Img_AnnullaProposta.png" onclick="javascript:myOpacity.toggle();" /><br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                Style="z-index: 104; left: 404px; position: absolute; top: 500px" 
                ToolTip="Nuova Ricerca" TabIndex="2" />
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 105; left: 549px; position: absolute; top: 498px" 
                ToolTip="Home" TabIndex="3" />
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" 
                Font-Names="Arial" 
                style="z-index: 107; left: 6px; position: absolute; top: 463px; text-align: left; width: 662px;" 
                Font-Size="12pt">Nessuna selezione</asp:Label>
                        <asp:DataGrid ID="DataGrid3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            Font-Names="Arial" Font-Size="8pt" Width="666px" 
                style="z-index: 108; left: 3px; position: absolute; top: 351px" 
                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                PageSize="4">
                            <PagerStyle Mode="NumericPages" />
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Navy" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PG" HeaderText="PG" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OFFERTA" HeaderText="offerta" ReadOnly="True" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SCADENZA" HeaderText="scadenza" ReadOnly="True" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="OFFERTA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label19" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.OFFERTA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SCADENZA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label20" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.SCADENZA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PROTOCOLLO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label22" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COGNOME">
                                    <ItemTemplate>
                                        <asp:Label ID="Label23" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NOME">
                                    <ItemTemplate>
                                        <asp:Label ID="Label24" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox8" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Edit" Style="z-index: 100;
                                            left: 0px; position: static; top: 0px" Width="51px">Seleziona</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            Font-Names="Arial" Font-Size="8pt" Width="666px" 
                style="z-index: 108; left: 3px; position: absolute; top: 224px" 
                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                PageSize="4">
                            <PagerStyle Mode="NumericPages" />
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Navy" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PG" HeaderText="PG" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OFFERTA" HeaderText="offerta" ReadOnly="True" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SCADENZA" HeaderText="scadenza" ReadOnly="True" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="OFFERTA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.OFFERTA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SCADENZA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.SCADENZA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="POS">
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PROTOCOLLO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COGNOME">
                                    <ItemTemplate>
                                        <asp:Label ID="Label16" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NOME">
                                    <ItemTemplate>
                                        <asp:Label ID="Label17" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ISBARC/R">
                                    <ItemTemplate>
                                        <asp:Label ID="Label18" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.ISBARC_R") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" Style="z-index: 100;
                                            left: 0px; position: static; top: 0px" Width="51px">Seleziona</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            Font-Names="Arial" Font-Size="8pt" Width="666px" 
                style="z-index: 108; left: 3px; position: absolute; top: 72px" 
                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                PageSize="6">
                            <PagerStyle Mode="NumericPages" />
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Navy" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PG" HeaderText="PG" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="OFFERTA" HeaderText="offerta" ReadOnly="True" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SCADENZA" HeaderText="scadenza" ReadOnly="True" Visible="False">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="OFFERTA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OFFERTA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SCADENZA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCADENZA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="POS">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PROTOCOLLO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COGNOME">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Font-Names="ARIAL" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NOME">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ISBARC/R">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ISBARC_R") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Style="z-index: 100;
                                            left: 0px; position: static; top: 0px" Width="51px">Seleziona</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
        </div>
    
    </div>
        <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
    
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <script type="text/javascript">
        function AnnullaProposta() {

            if (document.getElementById('HiddenField2').value != '') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Annullare la proposta? Premere OK per confermare o ANNULLA per non procedere.");
                if (chiediConferma == false) {
                    document.getElementById('HiddenField1').value = '0';
                }
                else {
                    document.getElementById('HiddenField1').value = '1';
                }
            }
        }

        
</script>

    </form>
        </body>
        	    <script type="text/javascript">
        	        myOpacity = new fx.Opacity('Motivazione', { duration: 200 });
        	        myOpacity.hide();
        	        
        </script>
</html>

