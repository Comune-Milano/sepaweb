<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Abb_Automatico_p1.aspx.vb" Inherits="ASS_Abb_Automatico_p1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Abbinamento Automatico</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr style="font-size: 12px; font-family: arial, Helvetica, sans-serif; color: #990000; background-color: #800000">
                <td height="20px" 
                    style="text-align: center; color: #FFFFFF; font-family: ARIAL, Helvetica, sans-serif; font-size: 14pt;">
                    ABBINAMENTO AUTOMATICO - SCELTA UNITA&#39;
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                    <asp:ImageButton 
                ID="btnSelezionaTutti" runat="server" ImageUrl="~/NuoveImm/Img_SelezionaTuttiGrande.png"
                Style="z-index: 102;" 
                ToolTip="Seleziona Tutti" TabIndex="2" /><asp:ImageButton ID="btnDeselezionaTutti" runat="server" ImageUrl="~/NuoveImm/Img_DeSelezionaTutti.png"
                Style="z-index: 102; " 
                ToolTip="Deseleziona tutti" TabIndex="1" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="" 
            TabIndex="1" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="" 
                        ToolTip="Esci" CausesValidation="False" 
            onclientclick="ConfermaEsci();" TabIndex="2" />
                            </td>
                        </tr>
                    </table>
                        </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="8pt" 
                            Style="z-index: 121; width: 100%;" 
                            HorizontalAlign="Left" TabIndex="1">
                            <PagerStyle Mode="NumericPages" />
                            <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChSelezionato" runat="server"  />
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="COD" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROPRIETA" HeaderText="PROPRIETA" ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="CODICE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_ALL" HeaderText="TIPO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NOME_QUARTIERE" HeaderText="QUARTIERE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_VIA" HeaderText="INDIR."></asp:BoundColumn>
                                <asp:BoundColumn DataField="INDIRIZZO" HeaderText=" "></asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_CIVICO" HeaderText="CIV."></asp:BoundColumn>
                                <asp:BoundColumn DataField="N_ALL" HeaderText="NUM."></asp:BoundColumn>
                                <asp:BoundColumn DataField="ZONA" HeaderText="ZONA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_LOCALI" HeaderText="LOC."></asp:BoundColumn>
                                <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NETTA" HeaderText="NETTA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CONV" HeaderText="CONV."></asp:BoundColumn>
                                <asp:BoundColumn DataField="ELEVATORE" HeaderText="ASC."></asp:BoundColumn>
                                <asp:BoundColumn DataField="HANDICAP" HeaderText="HANDICAP"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_DISPONIBILITA1" HeaderText="DISP."></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROPRIETA1" HeaderText="PROPR."></asp:BoundColumn>
                                <asp:BoundColumn DataField="Visualizza"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
     <script language="javascript" type="text/javascript">
         document.getElementById('dvvvPre').style.visibility = 'hidden';

         function ConfermaEsci() {


             var chiediConferma
             chiediConferma = window.confirm("Sei sicuro di voler uscire?");
             if (chiediConferma == true) {
                 self.close();
             }

         }

    </script>
    </form>
</body>
</html>
