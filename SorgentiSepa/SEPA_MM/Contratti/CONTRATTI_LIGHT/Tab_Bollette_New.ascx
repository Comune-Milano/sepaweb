<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Bollette_New.ascx.vb"
    Inherits="Contratti_Tab_Bollette" %>
<script type="text/javascript">
    var Selezionato;
</script>
<style type="text/css">
    .coloreSaldo
    {
        background-color: #FFCC66;
        color: #993333;
    }
    .coloreTitolo
    {
        font-family: Arial;
        font-size: 8px;
        color: #FFFFFF;
    }
    .dimensioneFissa
    {
        resize: none;
    }
    </style>
<div style="left: 8px; width: 1130px; position: absolute; top: 168px; height: 550px">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                    ForeColor="#993333" Text="PARTITA GESTIONALE"></asp:Label>
            </td>
            <td>
                <div onclick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_menuGest').value='1';" 
                    style="position: relative; z-index: 3">
                </div>
            </td>
            <td style="text-align: right; width: 680px;">
                &nbsp
            </td>
            <td style="text-align: right;">
                <table>
                    <tr>
                        <td style="font-size: 9pt; font-family: Arial; text-decoration: line-through;">
                            Elaboraz. totale
                        </td>
                        <td>
                            <hr align="left" style="vertical-align: middle; color: black;" width="15px" />
                        </td>
                        <td style="width: 15px;">
                            &nbsp
                        </td>
                        <td style="font-size: 9pt; font-family: Arial; color: red;">
                            Elaboraz. parziale
                        </td>
                        <td>
                            <hr align="left" style="vertical-align: middle; color: red;" width="15px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <div id="divIntestPartGestConInfo" style="padding-left: 5px;">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <%--                                                <asp:MenuItem Text="Crea Nuova Bolletta" Value="1"></asp:MenuItem>
                                                <asp:MenuItem Text="Modifica Bolletta" Value="2"></asp:MenuItem>
                                --%>
 <div id="DivRoot1" align="left">
                        <div style="overflow: hidden;" id="DivHeaderRow1">
                        </div>
                        <div style="width: 1130px;height: 180px; overflow: auto;" onscroll="OnScrollDiv1(this)" id="DivMainContent1" onclick="document.getElementById('USCITA').value='1';">

               
                    <asp:DataGrid ID="DataGridGest" runat="server" Width="100%" BackColor="#006699" BorderColor="Silver"
                        BorderStyle="Solid" BorderWidth="1px" AutoGenerateColumns="False"
                        CellPadding="3">
                        <AlternatingItemStyle BackColor="#F7F7F7" BorderStyle="None" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID_BOLLETTA" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="num_tipo" HeaderText="TIPO">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" Width="68px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="riferimento_da" HeaderText="PERIODO DAL">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" Width="98px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="riferimento_a" HeaderText="PERIODO AL">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" Width="99px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="data_emissione" HeaderText="DATA EMISSIONE">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" Width="127px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="importobolletta" HeaderText="IMP. CONTABILE">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"
                                    Width="129px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="note" HeaderText="NOTE">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" Width="350px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Anteprima" HeaderStyle-BackColor="#006699" HeaderStyle-BorderStyle="None"
                                Visible="False">
                                <HeaderStyle BackColor="#006699" BorderStyle="None"></HeaderStyle>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO_APPL" ReadOnly="True" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle Font-Bold="True" Font-Names="Courier New" Font-Size="8pt" Wrap="False"
                            BackColor="#006699" ForeColor="White" BorderStyle="None" Height="20px" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" />
                        <ItemStyle ForeColor="black" BackColor="#D6D6D6" BorderStyle="None" />
                        <PagerStyle ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" BackColor="#E7E7FF" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    </asp:DataGrid>
                </div>

                            <div id="DivFooterRow1" style="overflow:hidden">
                            </div>
                            </div>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td width="350px">
                            &nbsp
                        </td>
                        <td>
                            <img alt="" src="../Immagini/coins-icon.png" width="30px" />
                        </td>
                        <td align="center">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                ForeColor="#993333" Text="SALDO CONTABILE" Font-Underline="True"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSaldoCont" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                ForeColor="#993333" CssClass="coloreSaldo" Font-Overline="False" Font-Strikeout="False"></asp:Label>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                ForeColor="#993333" Text="PARTITA CONTABILE"></asp:Label>
                        </td>
                        <td>
                            <div onclick="document.getElementById('USCITA').value='1';" 
                                style="position: relative; z-index: 3">
                            </div>
                        </td>
                        <td style="width: 685px; text-align: right;">
                            &nbsp
                        </td>
                        <td>
                            <table style="text-align: right;">
                                <tr>
                                    <td style="font-size: 9pt; font-family: Arial; color: blue;">
                                        Riclassificate
                                    </td>
                                    <td>
                                        <hr align="left" style="vertical-align: middle; color: blue;" width="15px" />
                                    </td>
                                    <td style="width: 15px;">
                                        &nbsp
                                    </td>
                                    <td style="font-size: 9pt; font-family: Arial; color: red;">
                                        Stornate/Annullate
                                    </td>
                                    <td>
                                        <hr align="left" style="vertical-align: middle; color: red;" width="15px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <div id="divContSenzaInfo" style="padding-left: 3px;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="DivRoot" align="left">
                        <div style="overflow: hidden;" id="DivHeaderRow">
                        </div>
                        <div style="width: 1130px;height: 220px; overflow: auto;" onscroll="OnScrollDiv(this)" id="DivMainContent" onclick="document.getElementById('USCITA').value='1';">
                                <div>
                                    <asp:DataGrid ID="DataGridContab" runat="server" Width="100%" BackColor="White" BorderColor="#E7E7FF"
                                        BorderStyle="None" BorderWidth="1px" PageSize="100" AutoGenerateColumns="False"
                                        CellPadding="3" AllowPaging="True" onclick="document.getElementById('USCITA').value='1';">
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" Wrap="False" Font-Size="8pt" Position="Bottom" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" HeaderText="ID_BOLLETTA" ReadOnly="True" Visible="False">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Anteprima" HeaderStyle-BackColor="#006699" 
                                                HeaderStyle-BorderStyle="None" HeaderText="&amp;nbsp;">
                                                <HeaderStyle BackColor="#006699" Height="0px" BorderStyle="None" 
                                                    Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                                    Font-Strikeout="False" Font-Underline="False" Font-Names="Courier New" 
                                                    ForeColor="#006699"></HeaderStyle>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Dettagli" 
                                                HeaderStyle-BorderStyle="None" HeaderText="&amp;nbsp;">
                                                <HeaderStyle BackColor="#006699" Height="0px" BorderStyle="None" 
                                                    Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                                    Font-Strikeout="False" Font-Underline="False" Font-Names="Courier New" 
                                                    ForeColor="#006699"></HeaderStyle>
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="num_tipo" HeaderText="N°/TIPO" HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="riferimento_da" HeaderText="PERIODO DAL" HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="riferimento_a" HeaderText="PERIODO AL" HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="data_emissione" HeaderText="DATA EMISS." HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="data_scadenza" HeaderText="DATA SCAD." HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="importo_totale" HeaderText="IMP. EMESSO" HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="importobolletta" HeaderText="IMP. CONTAB." HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="imp_pagato" HeaderText="IMP. PAG." HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="imp_residuo" HeaderText="RES.CONTAB." HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="data_pagamento" HeaderText="DATA PAGAM." HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Height="0px" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="fl_mora" HeaderText="IN MORA" HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="fl_rateizz" HeaderText="RATEIZ." HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="IMPORTO_RUOLO" HeaderText="IMP. RUOLO" HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="note" HeaderText="NOTE" HeaderStyle-BorderColor="#FFFFFF"
                                                HeaderStyle-BorderStyle="Solid">
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" ReadOnly="True" Visible="False">
                                            </asp:BoundColumn>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                        <HeaderStyle Font-Bold="True" Font-Names="Courier New" Font-Size="8pt" Wrap="False"
                                            BackColor="#006699" ForeColor="#FFFFFF" Height="0px" BorderStyle="None" 
                                            BorderColor="White" Font-Italic="False" Font-Overline="False" 
                                            Font-Strikeout="False" Font-Underline="False" />
                                        <ItemStyle ForeColor="black" BackColor="#D6D6D6" />
                                        <PagerStyle BackColor="White" ForeColor="#2461BF" HorizontalAlign="Left" Font-Bold="True"
                                            Font-Names="Arial" Font-Size="7pt" />
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    </asp:DataGrid>
                                    <table width="100%">
                                    <tbody valign="top">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblPreventivo" runat="server" Font-Bold="False" Font-Names="ARIAL"
                                Font-Size="8pt"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPreventivo2" runat="server" Font-Bold="False" Font-Names="ARIAL"
                                                    Font-Size="8pt"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                </div>
                            						 </div>
                            <div id="DivFooterRow" style="overflow:hidden">
                            </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 89%;">
                            <br />
                            <br />
                            <br />
                            <asp:ListBox ondblclick="apriMorosita(this);" ID="lstBollette" runat="server" Font-Names="Courier New"
                                Font-Size="8pt" Height="300px" Width="860px" TabIndex="70" Visible="False"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 89%; height: 21px;">
                            <table width="100%">
                                <tr>
                                    <td style="height: 18px">
                                        
                                    </td>
                                    <td style="height: 18px">
                                       
                                        
                                    </td>
                                    <td style="height: 18px">
                                        &nbsp;</td>
                                    <td style="height: 18px">
                                        <asp:Image ID="ImgAnteprima" runat="server" onclick="javascript:ApriAnteprima();"
                                            Style="cursor: pointer; visibility: hidden;" ImageUrl="~/NuoveImm/Img_AnteprimaBolletta.png" />
                                    </td>
                                    <td style="height: 18px">
                                                                            </td>
                                    <td style="height: 18px">
                                       
                                    </td>
                                    <td style="height: 18px">
                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<p>
    &nbsp;</p>

<asp:HiddenField ID="txtConnessione" runat="server" />
<asp:HiddenField ID="V2" runat="server" />
<asp:HiddenField ID="V3" runat="server" />
<asp:HiddenField ID="txtAppare" runat="server" />
<asp:HiddenField ID="txtAppare1" runat="server" />
<asp:HiddenField ID="V1" runat="server" />
<asp:HiddenField ID="txtIdContratto" runat="server" />
<asp:HiddenField ID="txtannullo" runat="server" />
<asp:HiddenField ID="IdBolletta" runat="server" />
<asp:HiddenField ID="importo" runat="server" />
<asp:HiddenField ID="confermaStorno" runat="server" Value="0" />
<asp:HiddenField ID="menuGest" runat="server" Value="0" />
<asp:HiddenField ID="bolletteAssenti" runat="server" Value="0" />
<asp:HiddenField ID="divGestCon" runat="server" Value="0" />
<asp:HiddenField ID="divContabCon" runat="server" Value="0" />
<asp:HiddenField ID="confermaElimGest" runat="server" Value="0" />
<asp:HiddenField ID="confermaAnnullaGest" runat="server" Value="0" />
<asp:HiddenField ID="HRimborso" runat="server" />
<script type="text/javascript">
    function ControllaTabVuoto() {


    }

    function ApriAnteprima() {
        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima visualizzare l\'anteprima!');
        }
        else {
            if (document.getElementById('Tab_Bollette1_V3').value != '') {
                var fin;
                fin = window.open('AnteprimaBolletta.aspx?ID=' + document.getElementById('Tab_Bollette1_V3').value, 'Anteprima', '');
                fin.focus();
                document.getElementById('USCITA').value = '0'
            }
        }
    }




    function apriMorosita() {
        var win = null;
        LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
        LeftPosition = LeftPosition - 20;
        TopPosition = TopPosition - 20;
        window.open('Dettagli.aspx?ID=' + document.getElementById('Tab_Bollette1_V3').value, 'Dettagli', 'height=200,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
        document.getElementById('USCITA').value = '0';
    }

    
</script>
