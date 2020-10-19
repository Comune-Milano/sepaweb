<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovaListaExcel1.aspx.vb" Inherits="ANAUT_NuovaListaExcel1" %>

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
    </head>
<body>
    <form id="form1" runat="server">
<br/>
        <table style="left: 0px; BACKGROUND-IMAGE: url('../NuoveImm/SfondoMaschere.jpg'); WIDTH: 674px;
            position: absolute; top: 0px; height: 596px;">
            <tr>
                <td style="width: 674px">
                   <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Risultato elaborazione File </strong>
                    <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text="Label"></asp:Label>
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
    
    <div id="ContenitoreVoci" 
                        
                        
                        style="border: 1px solid #990000; overflow: auto; width: 627px; height: 389px; position:absolute; top: 66px; left: 14px;">
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
                                <asp:BoundColumn DataField="ID_LISTA" HeaderText="ID_LISTA" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RU" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MOTIVAZIONE" HeaderText="NOTE"></asp:BoundColumn>
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
                    <asp:ImageButton ID="imgSalva" runat="server" 
                        style="position:absolute; top: 512px; left: 495px;" 
                        ImageUrl="~/NuoveImm/Img_SalvaGrande.png" />
                        <asp:ImageButton ID="ImageButton1" runat="server" 
                        style="position:absolute; top: 513px; left: 342px;" 
                        ImageUrl="~/NuoveImm/Img_Export_XLS.png" />
                    <br />
                    
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 513px; left: 586px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="PaginaHome();"/>
                    <br />
                    <br />
                    <br />
                    
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 499px; left: 15px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />

   

                </td>
            </tr>

             <asp:HiddenField ID="confermato" runat="server" />
        </table>
        
           

        <script type="text/javascript">

            
            function Sicuro() {
               
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione, Confermi memorizzazione lista?");
                    if (chiediConferma == true) {
                        document.getElementById('confermato').value = '1';
                    }
                    else {
                        document.getElementById('confermato').value = '0';
                    }
            }

            function PaginaHome() {
                document.location.href = 'pagina_home.aspx';
            }
           
           


    </script>
    
           
    
           
           <p>
                                        &nbsp;</p>
                                   <p>
                                        &nbsp;</p>

    </form>
    

    
    

        
        </body>
</html>

