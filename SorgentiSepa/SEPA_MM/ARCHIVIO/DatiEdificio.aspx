﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatiEdificio.aspx.vb" Inherits="ARCHIVIO_DatiEdificio" %>

<script type="text/javascript">
    var Uscita;
    var Selezionato;

    Uscita = 1;

    function $onkeydown() {

        if (event.keyCode == 13) {
            ApriContratto();
        }
    }
   
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Scheda Contratto</title>
    </head>
<body>
    <form id="form1" runat="server" defaultfocus="cmbGestore">
    <div>
        <table style="width: 100%;">
            <tr bgcolor="#990000">
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                        ForeColor="White" Text="RIEPILOGO SCHEDA ARCHIVIO"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Label ID="lblDatiContratto0" runat="server" Font-Bold="False" Font-Names="arial"
                                    Font-Size="10pt">CODICE EDIFICIO</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCodEdificio" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDatiContratto4" runat="server" Font-Bold="False" Font-Names="arial"
                                    Font-Size="10pt">DENOMINAZIONE</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDenominazione" runat="server" Font-Bold="True" Font-Names="arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDatiContratto3" runat="server" Font-Bold="False" Font-Names="arial"
                                    Font-Size="10pt">INDIRIZZO</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <br />
                </td>
            </tr>
            <tr>
                <td>
    <div style="width: 680px;height:280px; overflow:scroll;" id="DivMainContent" >
       
     <asp:DataGrid ID="Datagrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="200" Style="z-index: 101; left: 9px; width: 880px;" TabIndex="1" BorderWidth="0px">
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_EUSTORGIO" HeaderText="COD.EUSTORGIO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GESTORE" HeaderText="GESTORE">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CATEGORIA" HeaderText="CATEGORIA"></asp:BoundColumn>
                <asp:BoundColumn DataField="SCATOLA" HeaderText="SCATOLA"></asp:BoundColumn>
                <asp:BoundColumn DataField="FALDONE" HeaderText="FALDONE"></asp:BoundColumn>
                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#3366FF" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="White" Wrap="False" Height="25px" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
      
    </div>

                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton 
                        ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png" 
                         Style="z-index: 102;" 
                        ToolTip="Visualizza" />
                    &nbsp;<asp:ImageButton ID="btnAggiorna" runat="server" ImageUrl="~/NuoveImm/arrow-refresh-small-icon.png"
        Style="z-index: 102; height: 16px;" ToolTip="Visualizza"
        Visible="true" />&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImgElimina" runat="server" 
                        ImageUrl="~/NuoveImm/Img_EliminaModello.png" 
                        onclientclick="ChiediConferma();" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnModifica" runat="server" 
                        ImageUrl="~/NuoveImm/img_Modifica2.png" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnNuovo" runat="server" 
                        ImageUrl="~/NuoveImm/Img_NuovoModello.png" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImgAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="10pt"
                        ForeColor="#CC3300" Visible="False"></asp:Label>
                </td>
            </tr>
            </table>
        <asp:HiddenField ID="LBLID" runat="server" />
        <asp:HiddenField ID="cancella" runat="server" />
    </div>

    </form>
     <script language="javascript" type="text/javascript">
         document.getElementById('btnAggiorna').style.visibility = 'hidden';
         document.getElementById('btnAggiorna').style.position = 'absolute';
         document.getElementById('btnAggiorna').style.left = '-100px';
         document.getElementById('btnAggiorna').style.display = 'none';



         function ChiediConferma() {
             var chiediConferma
             chiediConferma = window.confirm("Attenzione...Sei sicuro di volere eliminare questa scheda archivio?");
             if (chiediConferma == true) {
                 document.getElementById('cancella').value = '1';
             }
             else {
                 document.getElementById('cancella').value = '0';
             }
         }

    </script>
</body>
</html>


