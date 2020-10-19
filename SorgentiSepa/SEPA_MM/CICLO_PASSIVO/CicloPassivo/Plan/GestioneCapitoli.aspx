<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneCapitoli.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_Plan_GestioneCapitoli" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
    <title>Gestione Capitoli</title>
    <style type="text/css">
        .style1
        {
            height: 19px;
            width: 77px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
<br/>
        <table style="left: 0px; BACKGROUND-IMAGE: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px; height: 596px;">
            <tr>
                <td style="width: 706px">
                   <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Gestione Capitoli</strong><br />
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
                        Style="left: 576px; position: absolute; top: 516px; height: 20px;" 
                        TabIndex="4" />
                    <asp:ImageButton ID="ImgModifica" 
                        OnClientClick="document.getElementById('TextBox1').value='2'" 
                        runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png" 
                                 Style="left: 718px; position: absolute; top: 94px; height: 12px;" 
                        ToolTip="Modifica Sede Territoriale" EnableTheming="True" 
                        CausesValidation="False" />
    <div id="ContenitoreVoci" style="overflow: auto; width: 687px; height: 389px; position:absolute; top: 66px; left: 14px;">
    <asp:DataGrid ID="DataGridCapitoli" runat="server" AutoGenerateColumns="False" BackColor="#F2F5F1"
                            BorderWidth="0px" Font-Bold="False" Font-Italic="False" 
            Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            meta:resourcekey="DataGrid1Resource1" PageSize="100" Style="z-index: 101; left: 483px;
                            top: 68px" Width="665px">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Mode="NumericPages" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COD" HeaderText="cod" ReadOnly="True" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="CODICE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
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
                    <br />

   

                    <asp:ImageButton ID="ImgBtnAggiungi" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                        Style="left: 718px; position: absolute; top: 123px; height: 12px;" 
                        ToolTip="Elimina la voce selezionata" onclientclick="Sicuro();" />

   

                </td>
            </tr>
             <asp:HiddenField ID="txtid" runat="server" />
             <asp:HiddenField ID="modificato" runat="server" />
             <asp:HiddenField ID="TextBox1" runat="server" />
             <asp:HiddenField ID="eliminato" runat="server" />
        </table>
        
        <div id="InserimentoLegali" 
            
        
        
        style="left: 0px; width: 100%; position: absolute;
            top: 0px; height: 100%; text-align: left; background-repeat: no-repeat; visibility: visible; background-image: url('../../../ImmDiv/SfondoDiv.png');">
            <span style="font-family: Arial"></span>
            <br />
            <br />
            <table border="0" cellpadding="1" cellspacing="1" style="left: 168px; width: 444px;
                position: absolute; top: 128px; background-color: #FFFFFF; z-index: 200;">
                <tr>
                    <td style="text-align: left" class="style1">
                        <strong><span style="font-family: Arial">Gestione</span></strong></td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        <strong><span style="font-family: Arial">Capitoli</span></strong></td>
                </tr>
                <tr>
                    <td style="text-align: left" class="style1">
                    </td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        &nbsp;</td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td style="text-align: left" class="style1">
                        <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        <asp:TextBox ID="txtCodice" runat="server" Font-Names="Arial" Font-Size="9pt" 
                            Width="172px" MaxLength="50" TabIndex="1"></asp:TextBox>
                        </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td style="text-align: left" class="style1" valign="top">
                        <span style="font-size: 10pt; font-family: Arial">Descrizione</span></td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" 
                            Font-Size="9pt" Width="354px" MaxLength="50" TabIndex="2" Height="127px" 
                            TextMode="MultiLine"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;&nbsp;</td>
                    <td style="width: 469px; height: 19px">
                        &nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td align="right" style="width: 469px; height: 19px; text-align: right">
                        <table border="0" cellpadding="1" cellspacing="1" style="width: 110%; text-align: left;">
                            <tr>
                                <td style="text-align: center">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ID="img_InserisciSchema" runat="server" 
                                        ImageUrl="~/NuoveImm/Img_SalvaVal.png" TabIndex="6" />&nbsp;<asp:ImageButton 
                                        ID="img_ChiudiSchema" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                            OnClientClick="document.getElementById('TextBox1').value='0';"
                            TabIndex="7" ToolTip="Annulla operazione" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    
            
        </div>
   

                    <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                        meta:resourcekey="TextBox3Resource1" 
                        Style="border: 1px solid white; left: 7px; position: absolute; top: 465px; width: 440px;" 
                        Text="Nessuna Selezione" BackColor="#F2F5F1" 
                        BorderWidth="0px"></asp:TextBox>
   

        <script type="text/javascript">

            function Sicuro() {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione, Eliminare il capitolo selezionato?");
                if (chiediConferma == true) {
                    document.getElementById('eliminato').value = '1';
                }
                else {
                    document.getElementById('eliminato').value = '0';
                }
            }
            
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
    
            <script type="text/javascript">

                myOpacity = new fx.Opacity('InserimentoLegali', { duration: 200 });

                if (document.getElementById('TextBox1').value != '2') {
                    myOpacity.hide(); ;
                }
        </script>
    
           
           <p>
                                        <img alt="Aggiungi Capitoli" onclick="document.getElementById('TextBox1').value='1';myOpacity.toggle();"
                                            src="../../../NuoveImm/Img_Aggiungi.png" 
                        style="cursor: pointer; left: 718px; position: absolute; top: 66px; bottom: 423px;" 
                        id="IMG1" /></p>

    </form>
    

    
    

        
        </body>
</html>
