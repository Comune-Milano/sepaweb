<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DecidiOfferta.aspx.vb" Inherits="ASS_DecidiOfferta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
var Uscita;
Uscita=0;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Decisione Offerta</title>
    <style type="text/css">
        .style1
        {
            width: 62px;
        }
        .style2
        {
            width: 118px;
        }
        .style3
        {
            width: 281px;
        }
        .style4
        {
            width: 85px;
        }
        .style10
        {
            width: 111px;
        }
    </style>
</head>
<body bgcolor="#ffffff">
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;
        &nbsp;
        &nbsp; &nbsp; &nbsp;
        &nbsp;
        <asp:Label ID="lblIdAll" runat="server" Style="z-index: 122; left: 456px; position: absolute;
            top: 133px" Text="Label" Visible="False"></asp:Label>
        &nbsp;
        &nbsp;&nbsp;

        <asp:Label ID="lblRelazione" runat="server" Style="z-index: 127; left: 506px; position: absolute;
            top: 133px" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="LblDataPG" runat="server" Style="z-index: 128; left: 533px; position: absolute;
            top: 113px" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblTipoPratica" runat="server" Style="z-index: 129; left: 499px; position: absolute;
            top: 67px" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="Label23" runat="server" Style="left: 120px; position: absolute; top: 463px"
            Visible="False" Width="103px"></asp:Label>
        <asp:Label ID="Label24" runat="server" Style="left: 120px; position: absolute; top: 486px"
            Visible="False" Width="103px"></asp:Label>
        <asp:Label ID="Label25" runat="server" Style="left: 120px; position: absolute; top: 510px"
            Visible="False" Width="103px"></asp:Label>
        <asp:Label ID="Label26" runat="server" Style="left: 120px; position: absolute; top: 531px"
            Visible="False" Width="103px"></asp:Label>
        </div>
    <table style="width:100%;position:absolute; top: 0px; left: 0px;">
        <tr>
            <td>
        <asp:Label ID="lblOfferta" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            Style="z-index: 101;" Width="184px"></asp:Label>
        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            Style="z-index: 124;" Width="87px">Scadenza</asp:Label>
                <asp:Label ID="lblScad" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            Style="z-index: 123;" Width="145px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td width="100px">
        <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;" 
                                Text="PG Dom."></asp:Label>
                        </td>
                        <td width="100px">
        <asp:Label ID="lblPG" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="cursor:pointer;z-index: 102; "
            Text="Label" Width="105px"></asp:Label>
                        </td>
                        <td width="150px">
        <asp:Label ID="Label28" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;" 
                                Text="Dic."></asp:Label>
        <asp:Label ID="lblPGDic" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="cursor:pointer;z-index: 102; "
            Text="Label" Width="68px" Height="16px"></asp:Label>
                        </td>
                        <td class="style10">
        <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 104;" Text="Nominativo"></asp:Label>
                        </td>
                        <td width="150px">
        <asp:Label ID="lblNominativo" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="10pt" ForeColor="#0000C0" Style="z-index: 106;" Text="Label" Width="262px"></asp:Label>
                        </td>
                    </tr>
                    <tr >
                        <td width="100px">
        <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 105;" Text="ISBARC/R"></asp:Label>
                        </td>
                        <td width="100px">
        <asp:Label ID="lblIsbarcr" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 107;"
            Text="Label" Width="104px"></asp:Label>
                        </td>
                        <td>
        <asp:Label ID="Label17" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 105;" 
                                Text="ISBARC/R Grad." Width="100px"></asp:Label>
                        </td >
                        <td class="style10">
        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 107;"
            Text="Label" Width="79px"></asp:Label>
                        </td>
                        <td width="100px">
        <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 108;" Text="N. Comp."></asp:Label>
        <asp:Label ID="lblComp" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 109; "
            Text="Label" Width="37px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
        <asp:Label ID="Label19" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 105;" Text="POSIZIONE"></asp:Label>
                        </td>
                        <td>
        <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 107;"
            Text="Label" Width="104px"></asp:Label>
                        </td>
                        <td>
        <asp:Label ID="Label21" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 105;" 
                                Text="TIPO ALLOGGIO" Width="100px"></asp:Label>
                        </td>
                        <td class="style10" >
        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 107;"
            Text="Label" Width="47px" Height="16px"></asp:Label>
                        </td>
                        <td>
        <asp:Label ID="lblVisDoc" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="cursor:pointer;z-index: 107;"
            Text="Estremi Doc." Width="84px" Height="16px"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            Style="z-index: 113;" Width="523px">DATI ALLOGGIO OFFERTO</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td class="style1">
        <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 110;" Text="Codice"></asp:Label>
                        </td>
                        <td class="style3">
        <asp:Label ID="lblCodice" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 114;" Width="29%"></asp:Label>
                        </td>
                        <td class="style2">
        <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 111;" Text="Zona"></asp:Label>
                        </td>
                        <td class="style4">
        <asp:Label ID="lblZona" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 116;" Width="14%"></asp:Label>
                        </td>
                        <td>
        <asp:Label ID="LBLMANUTENTIVO" runat="server" Font-Names="arial" Font-Size="10pt" 
                                Style="z-index: 116;" Width="14%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 112;" Text="Proprietà"></asp:Label>
                        </td>
                        <td class="style3">
        <asp:Label ID="lblProprieta" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 115;" Width="29%"></asp:Label>
                        </td>
                        <td class="style2">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1">
        <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 117;" Text="Gestore"></asp:Label>
                        </td>
                        <td class="style3">
        <asp:Label ID="lblGestore" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 118;" 
                                Width="93%"></asp:Label>
                        </td>
                        <td class="style2">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1">
        <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 119;" Text="Stato"></asp:Label>
                        </td>
                        <td class="style3">
        <asp:Label ID="lblStato" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 120;" Width="46%"></asp:Label>
                        </td>
                        <td class="style2">
        <asp:Label ID="Label27" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 110;" 
                                Text="Data Disponibilità"></asp:Label>
                        </td>
                        <td class="style4">
        <asp:Label ID="lblData" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 110;"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        <td>
            <table style="width: 100%; background-color: #FFFFCC;">
                <tr>
                    <td>
                    <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" Style="z-index: 102;" Text="Il Richiedente ACCETTA la proposta di Assegnazione. Utilizzare il campo NOTE per eventuali comunicazioni"
                        Width="436px"></asp:Label>
                    </td>
                    <td>
        <asp:Button ID="btnAccetta" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
            Height="35px" Style="z-index: 100; "
            Text="ACCETTA ALLOGGIO" Width="181px" 
                        OnClientClick="if (document.getElementById('btnAccetta').value!='Visualizza Accettaz.') { return confirm('Confermi Accettazione Offerta?')}; " 
                        TabIndex="1" />
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000C0"
                        Style="z-index: 102;" Width="409px"></asp:Label>
                    </td>
                    <td>
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="" Target="_blank" ToolTip="Visualizza i componenti del nucleo"
                        Visible="False" Width="94px">Domanda ERP</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                        Style="" Width="412px">
                        <asp:ListItem Value="0">ASSEGNAZIONE IN DEROGA</asp:ListItem>
                        <asp:ListItem Value="1">ASSEGNAZIONE ORDINARIA</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                    <asp:HyperLink ID="HyperLink2" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="cursor: pointer;" Target="_blank" ToolTip="Visualizza lo stato della domanda"
                        Visible="False" Width="47px" Font-Overline="False" Font-Underline="True" ForeColor="Blue">STATO</asp:HyperLink>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/IMG/Alert.gif" Style="" Visible="False" />
                    </td>
                </tr>
            </table>
            </td>
        </tr>
        <tr>
        <td>
            <table style="width:100%; background-color: #CCFFFF;">
                <tr>
                    <td>
                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 103;" Text="I motivi non presenti nella lista sottostante devono essere indicati nelle note e potrebbero causare la revoca dell'assegnazione se non giustificati. Utilizzare il campo NOTE per eventuali comunicazioni"
                        Width="431px"></asp:Label>
                    </td>
                    <td>
        <asp:Button ID="btnRifiuta" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
            Height="35px" 
            
            OnClientClick="if (document.getElementById('btnRifiuta').value!='Visualizza Rifiuto') {return confirm('Confermi Rifiuto Offerta? Assicurarsi di aver inserito un eventuale motivo. ')};" Style="z-index: 121;" Text="RIFIUTA ALLOGGIO" 
            Width="181px" TabIndex="3" />
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 100;" Text="Motivo Rifiuto"
                        Width="113px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                    <asp:DropDownList ID="cmbStato" runat="server" AutoPostBack="True" Height="17px"
                        Style="z-index: 101;" TabIndex="2"
                        Width="337px">
                    </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            </td>
        </tr>
        <tr>
        <td>
    <asp:HyperLink ID="HyperLink3" runat="server" 
        Style="" Font-Names="ARIAL" 
        Font-Size="10pt">Clicca qui per visualizzare lo schema di calcolo del canone + oneri accessori</asp:HyperLink>
            </td>
        </tr>
        <tr>
        <td>
        <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 126;" Text="NOTE"></asp:Label>
            &nbsp;<br />
            <asp:TextBox ID="txtNote" runat="server" Height="33px" Style="z-index: 125;" Width="585px" TabIndex="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td style="text-align: right">
        <asp:Button ID="btnEsci" runat="server" Style="z-index: 103;" Text="Esci" 
                TabIndex="5" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    </form>
    </body>
</html>
