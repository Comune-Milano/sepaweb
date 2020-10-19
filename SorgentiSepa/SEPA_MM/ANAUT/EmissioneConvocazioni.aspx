<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmissioneConvocazioni.aspx.vb" Inherits="ANAUT_EmissioneConvocazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
    var Selezionato;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
    <title>Gestione Motivi Esclusione</title>
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
        <table style="left: 0px; BACKGROUND-IMAGE: url('../NuoveImm/SfondoMaschere.jpg'); WIDTH: 674px;
            position: absolute; top: 0px; height: 596px;">
            <tr>
                <td style="width: 674px">
                   <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Emissioni Lettere Convocazioni AU</strong><br />
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
    
    <div id="ContenitoreVoci" 
                        
                        
                        style="border: 1px solid #990000; overflow: auto; width: 632px; height: 389px; position:absolute; top: 66px; left: 14px;">
    <asp:DataGrid ID="DataGridCapitoli" runat="server" AutoGenerateColumns="False" 
            Font-Bold="False" Font-Italic="False" 
            Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            meta:resourcekey="DataGrid1Resource1" PageSize="100" Style="z-index: 101; left: 483px;
                            top: 68px" Width="605px" CellPadding="4" 
            ForeColor="#333333" GridLines="None">
                            <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" 
                                ForeColor="White" />
                            <EditItemStyle Wrap="False" BackColor="#2461BF" />
                            <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" 
                                ForeColor="#333333" />
                            <PagerStyle Wrap="False" BackColor="#2461BF" ForeColor="White" 
                                HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" Wrap="False" />
                            <ItemStyle Wrap="False" BackColor="#EFF3FB" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="White" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NOME" HeaderText="ANAGRAFE UTENZA">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="INIZIO" HeaderText="INIZIO SIMUL.">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FINE" HeaderText="FINE SIMUL."></asp:BoundColumn>
                                <asp:HyperLinkColumn DataTextField="VISUALIZZA"></asp:HyperLinkColumn>
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
                        style="position:absolute; top: 513px; left: 586px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                    <br />
                    <asp:ImageButton ID="imgSalva" runat="server" 
                        style="position:absolute; top: 513px; left: 482px;" 
                        ImageUrl="~/NuoveImm/Img_Procedi.png" TabIndex="3" />
                        <asp:ImageButton ID="ImgAnnulla" runat="server" 
                        style="position:absolute; top: 513px; left: 16px;" 
                        ImageUrl="~/NuoveImm/Img_AnnullaVal.png" TabIndex="3" 
                        ToolTip="Annulla la convocazione" onclientclick="Sicuro()" />
                    <br />
                    <br />
                    
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 492px; left: 15px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />

   

                </td>
            </tr>
             <asp:HiddenField ID="txtid" runat="server" />
             <asp:HiddenField ID="modificato" runat="server" />
             <asp:HiddenField ID="TextBox1" runat="server" />
             <asp:HiddenField ID="eliminato" runat="server" />
             <asp:HiddenField ID="confermato" runat="server" />
        </table>
        
        <div id="InserimentoLegali" 
            
        
        
        
        style="left: 0px; width: 100%; position: absolute;
            top: 0px; height: 100%; text-align: left; background-repeat: no-repeat; visibility: hidden; background-image: url('../ImmDiv/SfondoDim3.jpg'); z-index: 500;">
            <span style="font-family: Arial"></span>
            <br />
            <br />
            <table border="0" cellpadding="1" cellspacing="1" style="left: 100px; width: 444px;
                position: absolute; top: 122px; background-color: #FFFFFF; z-index: 200;">
                <tr>
                    <td style="text-align: left" class="style1">
                        <strong><span style="font-family: Arial">Gestione</span></strong></td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        <strong><span style="font-family: Arial">Motivazioni</span></strong></td>
                </tr>
                <tr>
                    <td style="text-align: left" class="style1">
                    </td>
                    <td style="width: 469px; height: 19px; text-align: left">
                        &nbsp;</td>
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
                if (document.getElementById('txtid').value != '') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione, Annullare la convocazione selezionata e riportare allo stato di Simulazione?");
                    if (chiediConferma == true) {
                        document.getElementById('eliminato').value = '1';
                    }
                    else {
                        document.getElementById('eliminato').value = '0';
                    }
                }
                else {
                    alert('Selezionare un elemento dalla lista!');
                }
            }

            function SicuroConferma() {
                if (document.getElementById('txtid').value != '') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione, Confermare la Simulazione selezionata?\nEventuali contratti inclusi in altre simulazioni già confermate non saranno presi in considerazione!");
                    if (chiediConferma == true) {
                        document.getElementById('confermato').value = '1';
                    }
                    else {
                        document.getElementById('confermato').value = '0';
                    }
                }
                else {
                    alert('Selezionare un elemento dalla lista!');
                }
            }

            function ConfermaEsci() {
                document.location.href = 'pagina_home.aspx';
            }

           
           


    </script>
    
            <script type="text/javascript">

                myOpacity = new fx.Opacity('InserimentoLegali', { duration: 200 });

                if (document.getElementById('TextBox1').value != '2') {
                    myOpacity.hide(); ;
                }
        </script>
    
           
           <p>
                                        &nbsp;</p>
                                   <p>
                                        &nbsp;</p>

    </form>
    

    
    

        
        </body>
</html>

