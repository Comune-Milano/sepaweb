<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_IBAN.ascx.vb" Inherits="Tab_IBAN" %>
<style type="text/css">
    .style1
    {
        width: 88px;
        height: 14px;
    }
    .style2
    {
        width: 100%;
    }
    .style3
    {
        width: 100%;
    }
</style>
<table style="width: 100%">
    <tr>
        <td class="style2">
            <asp:Label ID="lblIBAN" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Text="ELENCO IBAN" Width="223px"></asp:Label>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td valign="top" class="style3">
            <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%;" >
                <telerik:RadGrid ID="DataGrid3" runat="server" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                    Culture="it-IT" RegisterWithScriptManager="False" PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true"
                    AllowPaging="true" AllowFilteringByColumn="false" EnableLinqExpressions="False"
                    Width="99%" AllowSorting="True" PageSize="100" IsExporting="False">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IBAN" HeaderText="IBAN">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FL_ATTIVO" HeaderText="STATO">
                                <HeaderStyle Width="20%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="METODO_PAGAMENTO" HeaderText="METODO PAGAMENTO">
                                <HeaderStyle Width="20%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO_PAGAMENTO" HeaderText="TIPO PAGAMENTO">
                                <HeaderStyle Width="20%" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                        <Excel FileExtension="xls" Format="Xlsx" />
                    </ExportSettings>
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                        ClientEvents-OnCommand="onCommand">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                      <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </asp:Panel>
            <asp:TextBox ID="txtRisIBAN" runat="server" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100" ReadOnly="True"
                Style="left: 40px; top: 200px" Width="550px"></asp:TextBox>
        </td>
        <td valign="top">
            <table>
                <tr>
                    <td class="style1">
                        <%-- <asp:ImageButton ID="btnAggIBAN" runat="server" CausesValidation="False" ImageUrl="../../../Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png"
                            OnClientClick="AggIBAN();" ToolTip="Aggiunge un nuovo IBAN" 
                            Visible="False" />--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <%-- <asp:ImageButton ID="btnApriIBAN" runat="server" CausesValidation="False" ImageUrl="../../../Condomini/Immagini/pencil-icon.png"
                            OnClientClick="modificaIBAN();" ToolTip="Modifica IBAN selezionato"
                            Visible="False" />--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 14px">
                        <%--<asp:ImageButton ID="btnEliminaIBAN" runat="server" ImageUrl="../../../Condomini/Immagini/minus_icon.png"
                            OnClientClick="confermaEliminaIBAN();" ToolTip="Elimina IBAN selezionato"
                            CausesValidation="False" Visible="False" />--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:HiddenField ID="idFORNITORE2" runat="server" Value="-1" />
<asp:HiddenField ID="idCONNESSIONE2" runat="server" Value="0" />
<asp:HiddenField ID="confermaEliminazione" runat="server" Value="0" />
<asp:HiddenField ID="IBANselezionato" runat="server" Value="-1" />
<script type="text/javascript">
    var Selezionato3;
    function AggIBAN() {
        window.showModalDialog('AggIBAN.aspx?M=-1&IDFORN=' + document.getElementById('Tab_IBAN_idFORNITORE2').value + '&IDCONN=' + document.getElementById('Tab_IBAN_idCONNESSIONE2').value, window, 'status:no;dialogWidth:500px;dialogHeight:400px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');
    }

    function confermaEliminaIBAN() {
        if (document.getElementById('Tab_IBAN_IBANselezionato').value != -1) {
            var Conferma
            Conferma = window.confirm("Attenzione...Confermi di voler eliminare l'IBAN selezionato?");
            if (Conferma == false) {
                document.getElementById('Tab_IBAN_confermaEliminazione').value = '0';
            } else {
                document.getElementById('Tab_IBAN_confermaEliminazione').value = '1';
            }
        } else {
            alert('Non è stato selezionato nessun IBAN');
        }
    }


    function modificaIBAN() {
        if (document.getElementById('Tab_IBAN_IBANselezionato').value != -1) {
            window.showModalDialog('AggIBAN.aspx?M=1&IDIBANS=' + document.getElementById('Tab_IBAN_IBANselezionato').value + '&IDFORN=' + document.getElementById('Tab_IBAN_idFORNITORE2').value + '&IDCONN=' + document.getElementById('Tab_IBAN_idCONNESSIONE2').value, window, 'status:no;dialogWidth:500px;dialogHeight:400px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');
        } else {
            alert('Non è stato selezionato nessun IBAN');
        }
    }
</script>
