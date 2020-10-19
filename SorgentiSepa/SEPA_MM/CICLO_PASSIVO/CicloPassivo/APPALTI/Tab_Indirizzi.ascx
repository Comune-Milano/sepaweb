<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Indirizzi.ascx.vb"
    Inherits="Tab_Indirizzi" %>
<style type="text/css">
    .style4
    {
        width: 100%;
    }
    .style5
    {
        width: 100%;
    }
</style>
<telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="True"
    Width="500" Height="400" VisibleStatusbar="False" Behavior="Pin, Move, Resize"
    Skin="Web20">
</telerik:RadWindow>
<table style="width: 100%;">
    <tr>
        <td class="style4">
            <asp:Label ID="lblIndirizzi" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Text="ELENCO INDIRIZZI" Width="223px"></asp:Label>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td valign="top" class="style5">
            <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%; height: 100%">
                <telerik:RadGrid ID="DataGrid3" runat="server" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                    Culture="it-IT" RegisterWithScriptManager="False" PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true"
                    AllowPaging="true" AllowFilteringByColumn="false" EnableLinqExpressions="False"
                    Width="99%" AllowSorting="True" PageSize="100" IsExporting="False">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CAP" HeaderText="CAP">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COMUNE" HeaderText="COMUNE">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PR" HeaderText="PR">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TELEFONO_1" HeaderText="TELEFONO 1">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TELEFONO_2" HeaderText="TELEFONO 2">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FAX" HeaderText="FAX">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EMAIL" HeaderText="E-MAIL">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                        <Excel FileExtension="xls" Format="Xlsx" />
                    </ExportSettings>
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                    <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </asp:Panel>
            <asp:TextBox ID="txtRisIndirizzi" runat="server" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100" ReadOnly="True"
                Style="left: 40px; top: 200px" Width="550px"></asp:TextBox>
        </td>
        <td valign="top">
            <table>
                <tr>
                    <td style="width: 88px; height: 14px;">
                        <%--<asp:ImageButton ID="btnAggInd" runat="server" CausesValidation="False" ImageUrl="../../../Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png"
                            OnClientClick="AggiungiIndirizzo();" ToolTip="Aggiunge un nuovo indirizzo"
                            Visible="False" />--%>
                        <asp:Button ID="btnApriInd" runat="server" OnClientClick="ModFornitore();return false;"
                            Text="Modifica" ToolTip="Modifica indirizzo selezionato" Style="top: 0px; left: 0px">
                        </asp:Button>
                        <%--<telerik:RadButton ID="btnApriInd" runat="server" OnClientClicking="function(sender,args){ModFornitore();}" AutoPostBack="false"
                            Text="Modifica" ToolTip="Modifica indirizzo selezionato" Style="top: 0px; left: 0px">
                        </telerik:RadButton>--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <%--<asp:ImageButton ID="btnApriInd" runat="server" CausesValidation="False" ImageUrl="../../../Condomini/Immagini/pencil-icon.png"
                            ToolTip="Modifica indirizzo selezionato" Visible="False" />--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 14px">
                        <%--<asp:ImageButton ID="btnEliminaInd" runat="server" ImageUrl="../../../Condomini/Immagini/minus_icon.png"
                            OnClientClick="confermaEliminaIndirizzo();" ToolTip="Elimina indirizzo selezionato"
                            CausesValidation="False" Visible="False" />--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:HiddenField ID="idFORNITORE1" runat="server" Value="-1" />
<asp:HiddenField ID="idCONNESSIONE1" runat="server" Value="0" />
<asp:HiddenField ID="confermaEliminazioneIndirizzo" runat="server" Value="0" />
<asp:HiddenField ID="INDIRIZZOselezionato" runat="server" Value="-1" />
<script type="text/javascript">
    var Selezionato2;
    function AggiungiIndirizzo() {
        window.showModalDialog('AggIndirizzi.aspx?M=-1&IDFORN=' + document.getElementById('Tab_Indirizzi_idFORNITORE1').value + '&IDCONN=' + document.getElementById('Tab_Indirizzi_idCONNESSIONE1').value, window, 'status:no;dialogWidth:500px;dialogHeight:400px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');
    }

    function confermaEliminaIndirizzo() {
        if (document.getElementById('Tab_Indirizzi_INDIRIZZOselezionato').value != -1) {
            var Conferma
            Conferma = window.confirm("Attenzione...Confermi di voler eliminare l'indirizzo selezionato?");
            if (Conferma == false) {
                document.getElementById('Tab_Indirizzi_confermaEliminazioneIndirizzo').value = '0';
            } else {
                document.getElementById('Tab_Indirizzi_confermaEliminazioneIndirizzo').value = '1';
            }
        } else {
            alert('Non è stato selezionato nessun indirizzo');
        }
    }

    function ApriIndirizzo() {
        if (document.getElementById('Tab_Indirizzi_INDIRIZZOselezionato').value != -1) {
            window.showModalDialog('AggIndirizzi.aspx?M=1&IDINDS=' + document.getElementById('Tab_Indirizzi_INDIRIZZOselezionato').value + '&IDFORN=' + document.getElementById('Tab_Indirizzi_idFORNITORE1').value + '&IDCONN=' + document.getElementById('Tab_Indirizzi_idCONNESSIONE1').value, window, 'status:no;dialogWidth:500px;dialogHeight:400px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');
        } else {
            alert('Non è stato selezionato nessun indirizzo');
        }
    }

   


</script>
