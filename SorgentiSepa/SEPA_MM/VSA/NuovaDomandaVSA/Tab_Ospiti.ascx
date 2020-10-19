<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Ospiti.ascx.vb" Inherits="VSA_NuovaDomandaVSA_Tab_Ospiti" %>

<script type="text/javascript" src="../../Funzioni.js"></script>
<script language="javascript" type="text/javascript">
<!--


    /*function MyDialogArguments() {
    this.Sender = null;
    this.StringValue = "";
    }*/

    var Selezionato;
    var OldColor;
    var SelColo;
    /*function AggiungiNucleo() {
    a = document.getElementById('Dom_Ospiti1_txtprogr').Value;
    dialogArgs = new MyDialogArguments();
    dialogArgs.StringValue = a;
    dialogArgs.Sender = window;
    var dialogResults = window.showModalDialog("../com_ospiti.aspx?OP=0&IDDOM=" + document.getElementById('Dom_Ospiti1_iddom').value + "&PR=" + a, window, 'status:no;dialogWidth:433px;dialogHeight:450px;dialogHide:true;help:no;scroll:no');
    if (dialogResults != undefined) {
    if (dialogResults == '1') {
    //document.getElementById('salvaEsterno').value = '1';
    document.getElementById('btnSalva').click();
    }
    if (dialogResults == '2') {
    document.getElementById('txtModificato').value = '1';

    }
    }
    }

    function ModificaNucleo() {
    a = document.getElementById('Dom_Ospiti1_txtprogr').Value;
    if (document.getElementById('idOsp').value == 0) {
    alert('Selezionare una riga dalla lista!');
    }
    else {
    //document.getElementById('caric').style.visibility = 'visible';
    cognome = document.getElementById('Dom_Ospiti1_cognome').value;
    nome = document.getElementById('Dom_Ospiti1_nome').value;
    data = document.getElementById('Dom_Ospiti1_data_nasc').value;
    cf = document.getElementById('Dom_Ospiti1_cod_fiscale').value;
    dataingr = document.getElementById('Dom_Ospiti1_data_inizio').value;
    datafine = document.getElementById('Dom_Ospiti1_data_fine').value;
    RI = document.getElementById('Dom_Ospiti1_IDselectedRow').value;

    str = cognome + " " + nome + " " + data + " " + cf + " " + dataingr + " " + datafine + " ";

    var dialogResults = window.showModalDialog("../com_ospiti.aspx?OP=1&IDDOM=" + document.getElementById('Dom_Ospiti1_iddom').value + "&ID=" + document.getElementById('idOsp').value + "&RI=" + RI + "&COGNOME=" + cognome + "&NOME=" + nome + "&DATA=" + data + "&CF=" + cf + "&TESTO=" + str + "&PR=" + a + "&DATAINGR=" + dataingr + "&DATAFINE=" + datafine, '', 'status:no;dialogWidth:433px;dialogHeight:450px;dialogHide:true;help:no;scroll:no');

    if (dialogResults != undefined)
    {
    if (dialogResults == '1')
    {

    //document.getElementById('salvaEsterno').value='1';
    document.getElementById('btnSalva').click();
    //document.getElementById('salvaEsterno').value='0';
    }
    if (dialogResults == '2')
    {
    document.getElementById('txtModificato').value='1';
                
    }
    }
    }
    }

    
    function EliminaSoggetto() {
    a = document.getElementById('Dom_Ospiti1_txtprogr').Value;
        
        
    if (document.getElementById('idOsp').value == 0 || document.getElementById('idOsp').value == -1) {
    alert('Selezionare una riga dalla lista!');
    }
    else {
    if (a != 0)
    {   
                
    document.getElementById('btnSalva').click();

              
    }
    else{
    alert('Impossibile eliminare il componente scelto!');
    }
    }

    }*/
    
// -->
</script>
<asp:HiddenField ID="V1" runat="server" />
<asp:HiddenField ID="iddom" runat="server" />
<asp:HiddenField ID="txtidTipoVIA" runat="server" />
<asp:HiddenField ID="txtVIA" runat="server" />
<asp:HiddenField ID="txtCIVICO" runat="server" />
<asp:HiddenField ID="txtCOMUNE" runat="server" />
<asp:HiddenField ID="txtCAP" runat="server" />
<asp:HiddenField ID="txtDOCIDENT" runat="server" />
<asp:HiddenField ID="txtDATADOC" runat="server" />
<asp:HiddenField ID="txtRILASCIO" runat="server" />
<asp:HiddenField ID="txtSOGGIORNO" runat="server" />
<asp:HiddenField ID="txtDATASogg" runat="server" />
<asp:HiddenField ID="txtREFERENTE" runat="server" />
<asp:HiddenField ID="txtIDospite" runat="server" />
<asp:HiddenField ID="IDselectedRow" runat="server" />
<asp:HiddenField ID="cognome" runat="server" />
<asp:HiddenField ID="nome" runat="server" />
<asp:HiddenField ID="data_nasc" runat="server" />
<asp:HiddenField ID="cod_fiscale" runat="server" />
<asp:HiddenField ID="data_inizio" runat="server" />
<asp:HiddenField ID="data_fine" runat="server" />
<table width="97%">
    <tr>
        <td style="padding-left: 15px;">
            <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Height="18px" Width="575px">Elenco OSPITI: persone non appartenenti al nucleo familiare originario</asp:Label>
        </td>
        <td style="width: 250px; padding-left: 250px">
            <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="9pt" ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_dichiarazione.htm#QB" Target="_blank" Width="17px" Visible="false">Aiuto</asp:HyperLink>
        </td>
    </tr>
</table>
<table width="97%" style="padding-left: 15px;">
    <tr>
        <td>
            <table style="margin-left: 10px; width: 97%; height: 20px;">
                <tr>
                    <td style="vertical-align: top;">
                        <div style="overflow-x: hidden; overflow-y: auto; width: 100%; height: 70px;" id="elencoOspiti">
                            <asp:DataGrid ID="DataGridOspiti" runat="server" AutoGenerateColumns="False" Visible="true" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" Width="100%" CellPadding="1" PageSize="5">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" Wrap="False" Visible="False" />
                                <HeaderStyle BackColor="#FFFFB3" Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#800000" Wrap="False" />
                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOME" HeaderText="NOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_NASC" HeaderText="DATA NASC." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_INIZIO_OSPITE" HeaderText="DATA INIZIO" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_FINE_OSPITE" HeaderText="DATA FINE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top;">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Image ID="Button4" runat="server" Width="16px" ToolTip="Inserimento componente" ImageUrl="~/ANAUT/img/ImgAdd.png" Style="cursor: pointer;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image ID="Button6" runat="server" Width="16px" ToolTip="Modifica Componente" ImageUrl="~/ANAUT/img/Pencil-icon.png" Style="cursor: pointer;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image ID="Button2" runat="server" ImageUrl="~/ANAUT/img/ImgDelete.png" Width="16px" TabIndex="22" ToolTip="Elimina Elemento" Style="cursor: pointer;" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:TextBox ID="txtprogr" runat="server" Style="left: 518px; top: 108px; z-index: 105; visibility: hidden;" Width="23px" Height="8px"></asp:TextBox>
        </td>
    </tr>
</table>
<script type="text/javascript">
    document.getElementById('Dom_Ospiti1_txtprogr').style.visibility = 'hidden';
    //document.getElementById('Dic_Nucleo1_txtprova').style.visibility='hidden';
    //document.getElementById('comp').style.visibility='hidden';


</script>
