<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Appalto.ascx.vb" Inherits="Tab_Appalto" %>

<table id="TABBLE_LISTA" style="width: 784px">
    <tr>
        <td style="width: 603px">
            <asp:Label ID="lblAPPALTI" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="ELENCO APPALTI"
                Width="223px"></asp:Label></td>
        <td style="width: 7px">
            &nbsp;&nbsp;
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="width: 603px; height: 270px">
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 670px; border-bottom: #0000cc thin solid; height: 350px;">
            <asp:DataGrid ID="DataGrid3" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="Black" Height="14px" PageSize="1" Style="table-layout: auto; z-index: 101;
                left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                TabIndex="13" Width="932px">
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_FORNITORE" HeaderText="ID_FORNITORE" 
                        Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM. REPERTORIO">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_REPERTORIO" HeaderText="DATA REP.">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_FINE" HeaderText="DATA FINE"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DURATA" HeaderText="DURATA"></asp:BoundColumn>
                </Columns>
                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="#0000C0" Wrap="False" />
            </asp:DataGrid></div>
            <asp:TextBox ID="txtSelAppalti" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="550px"></asp:TextBox></td>
        <td style="width: 7px; height: 270px">
            &nbsp; &nbsp; &nbsp;
        </td>
        <td style="height: 270px;">
            <table>
                <tr>
                    <td style="width: 88px; height: 14px;">
                        <asp:ImageButton ID="btnAggAppalti" runat="server" CausesValidation="False" ImageUrl="../../../NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';controllafornitore();"
                            TabIndex="14" ToolTip="Aggiunge un nuovo appalto" Visible="False" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaAppalti" runat="server" Height="12px" ImageUrl="../../../NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloAppalti();"
                            TabIndex="15" ToolTip="Elimina appalto selezionato" Width="60px" 
                            CausesValidation="False" Visible="False" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnApriAppalti" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="../../../NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Appalto_txtAppareP').value='1';document.getElementById('USCITA').value='1'; controlla_appalto();"
                            TabIndex="16" ToolTip="Modifica appalto selezionato" Width="60px" 
                            Visible="False" /></td>
                </tr>
            </table>
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
        </td>
    </tr>
</table>

<asp:TextBox ID="txtAppareP"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:HiddenField ID="idfornitore" runat="server" Value="0" />
<asp:HiddenField ID="idlotto" runat="server" Value="0" />

<script type="text/javascript">

//if (document.getElementById('Tab_Appalto_txtAppareP').value!='1') {
//document.getElementById('DIV_Appalti').style.visibility='hidden';
//}

function controllafornitore() {
    if (document.getElementById('Tab_Appalto_idfornitore').value == '0' || document.getElementById('Tab_Appalto_idfornitore').value == '-1' ) {
        alert('Salvare il fornitore per poter associargli un appalto!');
    }
    else {
  window.showModalDialog('SceltaLotto.aspx?X=1&F='+ document.getElementById('Tab_Appalto_idfornitore').value,window,'status:no;dialogWidth:800px;dialogHeight:600px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');
    }
}

function controlla_appalto() {
    if (document.getElementById('Tab_Appalto_txtIdComponente').value == '') {
        alert('Nessuna riga selezionata!');
        document.getElementById('USCITA').value = '0';
        document.getElementById('Tab_Appalto_txtAppareP').value = '0';
    }
    else {
        window.showModalDialog('Appalti.aspx?X=1&A=' + document.getElementById('Tab_Appalto_txtIdComponente').value + '&F=' + document.getElementById('Tab_Appalto_idfornitore').value + '&IDL=' + document.getElementById('Tab_Appalto_idlotto').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');
    }
}

</script>
