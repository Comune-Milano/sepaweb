<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Bollette_New.ascx.vb"
    Inherits="Contratti_Tab_Bollette" %>
<script type="text/javascript">
    var Selezionato;
</script>
<style type="text/css">
    .coloreSaldo {
        background-color: #FFCC66;
        color: #993333;
    }

    .coloreTitolo {
        font-family: Arial;
        font-size: 8px;
        color: #FFFFFF;
    }

    .dimensioneFissa {
        resize: none;
    }

    .style2 {
        border-right-style: solid;
        border-right-width: 1px;
        border-right-color: #acacac;
    }

    .style22 {
        border-right-style: solid;
        border-right-width: 1px;
        border-right-color: #006699;
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
                    <asp:Menu ID="MenuStampeG" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                        Orientation="Horizontal" Font-Bold="False" RenderingMode="Table" Width="20px">
                        <DynamicHoverStyle BackColor="#FFD784" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                        <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                            ForeColor="#0066FF" Width="180px" />
                        <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                            VerticalPadding="1px" />
                        <Items>
                            <asp:MenuItem ImageUrl="Immagini/Img_Funzioni.png" Selectable="False" Value="">
                                <%--                                                <asp:MenuItem Text="Crea Nuova Bolletta" Value="1"></asp:MenuItem>
                                                <asp:MenuItem Text="Modifica Bolletta" Value="2"></asp:MenuItem>
                                --%>
                            </asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </div>
            </td>
            <td style="text-align: right; width: 680px;">&nbsp
            </td>
            <td style="text-align: right;">
                <table>
                    <tr>
                        <td style="font-size: 9pt; font-family: Arial; text-decoration: line-through;">Elaboraz. totale
                        </td>
                        <td>
                            <hr align="left" style="vertical-align: middle; color: black;" width="15px" />
                        </td>
                        <td style="width: 15px;">&nbsp
                        </td>
                        <td style="font-size: 9pt; font-family: Arial; color: red;">Elaboraz. parziale
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
                    <div style="width: 1130px; height: 180px; overflow: auto;" onscroll="OnScrollDiv1(this)"
                        id="DivMainContent1" onclick="document.getElementById('USCITA').value='1';">
                        <asp:DataGrid ID="DataGridGest" runat="server" Width="100%" BackColor="#006699" BorderColor="Silver"
                            BorderStyle="Solid" BorderWidth="1px" AutoGenerateColumns="False" CellPadding="3">
                            <AlternatingItemStyle BackColor="#F7F7F7" BorderStyle="None" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID_BOLLETTA" ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Sposta" HeaderStyle-BackColor="#006699" HeaderStyle-BorderStyle="None">
                                    <HeaderStyle BackColor="#006699" BorderStyle="None"></HeaderStyle>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" Width="26px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Anteprima" HeaderStyle-BackColor="#006699" HeaderStyle-BorderStyle="None">
                                    <HeaderStyle BackColor="#006699" Height="0px" BorderStyle="None" Font-Bold="False"
                                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Center"
                                        Width="26px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Dettagli" HeaderStyle-BackColor="#006699" HeaderStyle-BorderStyle="None"
                                    Visible="False">
                                    <HeaderStyle BorderStyle="None"></HeaderStyle>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Center"
                                        Width="26px" />
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
                                <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_APPL" ReadOnly="True" Visible="False"></asp:BoundColumn>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle Font-Bold="True" Font-Names="Courier New" Font-Size="8pt" Wrap="False"
                                BackColor="#006699" ForeColor="#F7F7F7" BorderStyle="None" Height="20px" />
                            <ItemStyle ForeColor="black" BackColor="#D6D6D6" BorderStyle="None" />
                            <PagerStyle ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" BackColor="#E7E7FF" />
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:DataGrid>
                    </div>
                    <div id="DivFooterRow1" style="overflow: hidden">
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table align="center">
                    <tr>
                        <td>
                            <img alt="" src="Immagini/coins-icon.png" width="30px" />
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
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                ForeColor="#993333" Text="di cui A RUOLO"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblImpRuolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                ForeColor="#993333" Font-Overline="False" Font-Strikeout="False"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                ForeColor="#993333" Text="  di cui RATEIZZATO"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblImpRateizzato" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="9pt" ForeColor="#993333" Font-Overline="False" Font-Strikeout="False"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                ForeColor="#993333" Text="  di cui INGIUNTO"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblImpIngiunto" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="9pt" ForeColor="#993333" Font-Overline="False" Font-Strikeout="False"></asp:Label>
                        </td>
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
                            <div onclick="document.getElementById('USCITA').value='1';" style="position: relative; z-index: 3">
                                <asp:Menu ID="MenuStampeC" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                    Orientation="Horizontal" Font-Bold="False" RenderingMode="Table" Width="20px">
                                    <DynamicHoverStyle BackColor="#FFD784" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                                    <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                                        ForeColor="#0066FF" Width="180px" />
                                    <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                                        VerticalPadding="1px" />
                                    <Items>
                                        <asp:MenuItem ImageUrl="Immagini/Img_Funzioni.png" Selectable="False" Value="">
                                            <%--                                                <asp:MenuItem Text="Crea Nuova Bolletta" Value="1"></asp:MenuItem>
                                                <asp:MenuItem Text="Modifica Bolletta" Value="2"></asp:MenuItem>
                                            --%>
                                        </asp:MenuItem>
                                    </Items>
                                </asp:Menu>
                            </div>
                        </td>
                        <td style="width: 685px; text-align: right; vertical-align: middle;" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="8pt" Text="Visualizza max "></asp:Label>
                            <asp:DropDownList ID="cmbNumElementi" runat="server" AutoPostBack="True" Font-Names="arial"
                                Font-Size="8pt" onchange="javascript:document.getElementById('USCITA').value = '1';if (document.getElementById('Attesa')) {document.getElementById('Attesa').style.visibility = 'visible';}">
                                <asp:ListItem Selected="True">10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label10" runat="server" Font-Names="arial" Font-Size="8pt" Text="elementi per pagina"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="8pt" Text="Escludi Stornate"
                                ToolTip="Visualizza o meno le bollette stornate e le relative bollette di storno."></asp:Label>
                            <asp:RadioButton ID="RdStornoNO" runat="server" AutoPostBack="True" Checked="True"
                                Font-Names="arial" Font-Size="8pt" GroupName="A" Text="No" onclick="javascript:document.getElementById('USCITA').value = '1';if (document.getElementById('Attesa')) {document.getElementById('Attesa').style.visibility = 'visible';}" />
                            <asp:RadioButton ID="RdStornoSI" runat="server" AutoPostBack="True" Font-Names="arial"
                                Font-Size="8pt" GroupName="A" Text="Si" onclick="javascript:document.getElementById('USCITA').value = '1';if (document.getElementById('Attesa')) {document.getElementById('Attesa').style.visibility = 'visible';}" />
                            &nbsp
                        </td>
                        <td>
                            <table style="text-align: right;">
                                <tr>
                                    <td style="font-size: 9pt; font-family: Arial; color: blue;">Riclassificate
                                    </td>
                                    <td>
                                        <hr align="left" style="vertical-align: middle; color: blue;" width="15px" />
                                    </td>
                                    <td style="width: 15px;">&nbsp
                                    </td>
                                    <td style="font-size: 9pt; font-family: Arial; color: red;">Stornate/Annullate
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
                                <div style="width: 1130px; height: 220px; overflow: auto;" onscroll="OnScrollDiv(this)"
                                    id="DivMainContent" onclick="document.getElementById('USCITA').value='1';">
                                    <div>
                                        <asp:DataGrid ID="DataGridContab" runat="server" Width="100%" BackColor="#006699"
                                            BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" AutoGenerateColumns="False"
                                            CellPadding="3" AllowPaging="True" onclick="document.getElementById('USCITA').value='1';">
                                            <AlternatingItemStyle BackColor="#F7F7F7" />
                                            <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" Wrap="False" Font-Size="8pt" Position="Bottom" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="ID_BOLLETTA" ReadOnly="True" Visible="False">
                                                    <HeaderStyle BackColor="#006699" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                        Font-Strikeout="False" Font-Underline="False" ForeColor="#006699" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Anteprima" HeaderStyle-BackColor="#006699" HeaderStyle-BorderStyle="None"
                                                    HeaderText="&amp;nbsp;">
                                                    <HeaderStyle BackColor="#006699" Height="0px" BorderStyle="None" Font-Bold="True"
                                                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                        Font-Names="Courier New" ForeColor="#006699"></HeaderStyle>
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Dettagli" HeaderStyle-BorderStyle="None" HeaderText="&amp;nbsp;">
                                                    <HeaderStyle BackColor="#006699" Height="0px" BorderStyle="None" Font-Bold="True"
                                                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                        Font-Names="Courier New" ForeColor="#006699"></HeaderStyle>
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="num_tipo" HeaderText="N°/TIPO" HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="riferimento_da" HeaderText="PERIODO DAL" HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="riferimento_a" HeaderText="PERIODO AL" HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="data_emissione" HeaderText="DATA EMISS." HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="data_scadenza" HeaderText="DATA SCAD." HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="importo_totale" HeaderText="IMP. EMESSO" HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="importobolletta" HeaderText="IMP. CONTAB." HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Height="0px" Font-Italic="False" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="imp_pagato" HeaderText="IMP. PAG." HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="imp_residuo" HeaderText="RES.CONTAB." HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="data_pagamento" HeaderText="DATA PAGAM." HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Height="0px" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="fl_mora" HeaderText="IN MORA" HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="fl_rateizz" HeaderText="RATEIZ." HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="IMPORTO_RUOLO" HeaderText="IMP. RUOLO" HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="IMP_RUOLO_PAGATO" HeaderText="IMP. RUOLO PAG." HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SGRAVIO" HeaderText="SGRAVIO" HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="INGIUNZIONE" HeaderText="INGIUNZIONE" HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="IMPORTO_INGIUNZIONE" HeaderText="IMP. INGIUNZ." HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="IMP_INGIUNZIONE_PAG" HeaderText="IMP. INGIUNZ. PAG." HeaderStyle-BorderColor="#FFFFFF"
                                                    HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="note" HeaderText="NOTE&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;"
                                                    HeaderStyle-BorderColor="#FFFFFF" HeaderStyle-BorderStyle="Solid">
                                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Height="0px" Font-Names="Courier New"
                                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                        Wrap="False" BackColor="#006699" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" ReadOnly="True" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                            <HeaderStyle Font-Bold="True" Font-Names="Courier New" Font-Size="8pt" Wrap="False"
                                                BackColor="#006699" ForeColor="#F7F7F7" Height="0px" BorderStyle="None" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
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
                                <div id="DivFooterRow" style="overflow: hidden">
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
                                        <img id="IMG1NuovaBolletta" alt="Crea nuova bolletta" src="../NuoveImm/Img_CreaNuovaBolletta.png"
                                            onclick="return IMG1_onclick()" style="cursor: pointer; visibility: hidden;" />
                                    </td>
                                    <td style="height: 18px">
                                        <asp:Button ID="btnEliminaGest" runat="server" Text="" Style="display: none;" />
                                        <asp:Button ID="btnAnnullaGest" runat="server" Text="" Style="display: none;" />
                                        <asp:Button ID="btnSbloccaGest" runat="server" Text="" Style="display: none;" />
                                        <asp:ImageButton ID="btnModificaBolletta" runat="server" ImageUrl="~/NuoveImm/Img_CreaModificaBolletta.png"
                                            ToolTip="Modifica una bolletta non ancora emessa" Style="visibility: hidden; cursor: pointer; height: 16px;"
                                            OnClientClick="document.getElementById('USCITA').value='1';"
                                            TabIndex="71" />
                                    </td>
                                    <td style="height: 18px">
                                        <asp:ImageButton ID="btnAnnullaBolletta" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaBolletta.png"
                                            ToolTip="Annulla una bolletta Emessa" OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnullo();"
                                            Style="cursor: pointer; visibility: hidden;" TabIndex="72" />
                                    </td>
                                    <td style="height: 18px">
                                        <asp:Image ID="ImgAnteprima" runat="server" onclick="javascript:ApriAnteprima();"
                                            Style="cursor: pointer; visibility: hidden;" ImageUrl="~/NuoveImm/Img_AnteprimaBolletta.png" />
                                    </td>
                                    <td style="height: 18px">
                                        <asp:Image ID="imgSollecitiEmessi" runat="server" onclick="javascript:ApriSolleciti();"
                                            ImageUrl="~/NuoveImm/Img_SollecitiEmessi.png" ToolTip="Elenco solleciti emessi"
                                            Style="cursor: pointer; visibility: hidden;" TabIndex="75" />
                                    </td>
                                    <td style="height: 18px">
                                        <asp:Image ID="ImgMavOnLine" runat="server" onclick="javascript:ApriModulo();" Style="cursor: pointer; visibility: hidden;"
                                            ImageUrl="~/NuoveImm/Img_CreaMAV.png" ToolTip="Emette un m.a.v. tramite Banca INTESA S.PAOLO. Valido solo per le bollette di deposito cauzionale." />
                                    </td>
                                    <td style="height: 18px">
                                        <asp:Image ID="imgModulo" runat="server" onclick="javascript:ApriModulo();" Style="cursor: pointer; visibility: hidden;"
                                            ImageUrl="~/NuoveImm/Img_ModuloPagamento.png" ToolTip="Emette un m.a.v. tramite Banca INTESA S.PAOLO. Valido per tutte le tipologie di bollette tranne DEPOSITO CAUZIONALE." />
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
    &nbsp;
</p>
<div id="InserimentoBolletta" style="display: block; left: 0px; width: 1160px; position: absolute; top: 0px; height: 780px; text-align: left; z-index: 199; background-color: #c3c3bb;">
    <span style="font-family: Arial"></span>
    <br />
    <br />
    <table border="0" cellpadding="1" cellspacing="1" style="z-index: 200; left: 190px; width: 61%; position: absolute; top: 60px; height: 480px; background-color: #FFFFFF;">
        <tr>
            <td style="width: 404px; height: 19px;">
                <strong><span style="font-family: Arial">BOLLETTA</span></strong>
            </td>
        </tr>
        <tr>
            <td style="width: 404px; align: left;">&nbsp;<table style="width: 100%">
                <tr>
                    <td>
                        <span style="font-size: 10pt; font-family: Arial"><strong>Periodo</strong></span>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <span style="font-size: 10pt; font-family: Arial">Dal</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPeriodoDa" runat="server" Font-Names="Arial" Font-Size="9pt"
                            ToolTip="Inizio periodo di riferimento (gg/mm/aaaa)" Width="71px" MaxLength="10"
                            TabIndex="400"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPeriodoDa"
                            ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" TabIndex="300"
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <span style="font-size: 10pt; font-family: Arial">Al</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPeriodoAl" runat="server" Font-Names="Arial" Font-Size="9pt"
                            ToolTip="Fine periodo di riferimento (gg/mm/aaaa)" Width="71px" TabIndex="401"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPeriodoAl"
                            ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" TabIndex="300"
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="font-size: 10pt; font-family: Arial">Emissione</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmissione" runat="server" Font-Names="Arial" Font-Size="9pt"
                            ToolTip="Data Emissione (gg/mm/aaaa)" Width="71px" TabIndex="402" ReadOnly="True"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEmissione"
                            ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" TabIndex="300"
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        <span style="font-size: 10pt; font-family: Arial">Scadenza</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtScadenza" runat="server" Font-Names="Arial" Font-Size="9pt" ToolTip="Scadenza (gg/mm/aaaa)"
                            Width="71px" TabIndex="403"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtScadenza"
                            ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" TabIndex="300"
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
                <div id="SceltaTipoBoll" style="display: none;">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Tipo Bolletta" Font-Names="Arial" Font-Size="10pt"
                                    Width="80px"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbTipoGest" runat="server" Font-Names="Arial" Font-Size="10pt">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="SceltaTipoBollCont" style="display: none;">
                    <table>
                        <tr>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial;">Tipo Bolletta</span>
                            </td>
                            <td>&nbsp
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbTipoCont" runat="server" Font-Names="Arial" Font-Size="10pt">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <span style="font-size: 10pt; font-family: Arial"><strong>&nbsp;VOCI BOLLETTA</strong></span><br />
                <table style="width: 129%; height: 122px;">
                    <tr>
                        <td style="width: 3px; height: 64px;">
                            <asp:ListBox ID="lstVociBolletta" runat="server" Font-Names="Courier New" Font-Size="8pt"
                                Width="450px" Height="100px" TabIndex="404"></asp:ListBox>
                        </td>
                        <td style="height: 64px">
                            <img id="Img2" alt="Aggiungi Voce nello Schema" onclick="document.getElementById('Tab_Bollette1_txtImportoVoce').value='0,00';document.getElementById('InserimentoVoce').style.visibility='visible';"
                                src="../NuoveImm/img_Aggiungi.png" style="cursor: pointer" /><br />
                            <br />
                            <asp:ImageButton ID="img_EliminaVoce" runat="server" ImageUrl="~/NuoveImm/img_Elimina.png"
                                ToolTip="Elimina Voce dallo Schema" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare').value='1';"
                                TabIndex="405" />
                            <br />
                            <br />
                            <asp:ImageButton ID="img_CopiaSchema" runat="server" ImageUrl="~/NuoveImm/img_CopiaSchema.png"
                                ToolTip="Copia tutte le voci dallo schema" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare').value='1';"
                                TabIndex="405" />
                        </td>
                    </tr>
                </table>
                <table width="450px;" style="border-top: solid 1px black;">
                    <tr>
                        <td style="font-size: 10pt; font-weight: bold; font-family: Arial; width: 139px; color: #0062C4;">Totale
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotBoll" runat="server" Width="150px" Font-Names="Courier New"
                                Font-Size="8pt" TabIndex="406" ReadOnly="True" Style="color: #0062C4; text-align: right;"
                                BorderStyle="None" BorderWidth="0px" Font-Bold="True"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <strong><span style="font-size: 10pt; font-family: Arial">&nbsp;NOTE<br />
                    &nbsp;<asp:TextBox ID="txtNote" runat="server" Height="45px" TextMode="MultiLine"
                        Width="452px" TabIndex="406"></asp:TextBox></span></strong>
            </td>
        </tr>
        <tr style="text-align: right; font-size: 12pt; font-family: Times New Roman">
            <td style="text-align: right;">&nbsp;<table border="0" cellpadding="1" cellspacing="1" style="width: 80%; text-align: right;">
                <tr>
                    <td width="550px">&nbsp
                    </td>
                    <td align="right" width="550px">
                        <asp:ImageButton ID="imgSalva" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                            OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare').value='0';"
                            ToolTip="Salva" Style="cursor: pointer" Visible="False" TabIndex="408" />
                        <asp:ImageButton ID="img_InserisciBolletta" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                            OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare').value='0';document.getElementById('Tab_Bollette1_V3').value='';"
                            ToolTip="Inserisci la nuova Bolletta" Style="cursor: pointer;" TabIndex="409" />&nbsp;<asp:ImageButton
                                ID="img_ChiudiBolletta" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';myOpacity1.toggle();document.getElementById('Tab_Bollette1_txtAppare').value='0';document.getElementById('Tab_Bollette1_V3').value='';"
                                ToolTip="Esci senza inserire" Style="cursor: pointer" TabIndex="410" />
                    </td>
                </tr>
            </table>
            </td>
        </tr>
    </table>
    <div id="InserimentoVoce" style="display: block; border: 2px solid red; left: 289px; width: 334px; position: absolute; top: 233px; height: 202px; z-index: 205; background-color: #D7D7D7;">
        <table width="100%">
            <tr>
                <td>
                    <span style="font-size: 10pt; font-family: Arial"><strong>Voce</strong></span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbVoceSchema" runat="server" Font-Names="Arial" Font-Size="9pt"
                        Width="317px" TabIndex="411">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><span style="font-size: 10pt; font-family: Arial">Importo in Euro (2 cifre decimali)</span></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtImportoVoce" runat="server" Font-Names="Arial" Font-Size="9pt"
                        Width="74px" TabIndex="412"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                            runat="server" ControlToValidate="txtImportoVoce" ErrorMessage="Errore" Font-Bold="True"
                            Font-Names="ARIAL" Font-Size="9pt" ValidationExpression="^(-?)\b\d*,\d{2}\b"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><span style="font-size: 10pt; font-family: Arial">Note (andranno in stampa i
                        primi 30 caratteri)</span></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtnotevoce" runat="server" Font-Names="Arial" Font-Size="9pt" Width="314px"
                        TabIndex="412"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;<asp:ImageButton ID="btnInserisciVoce" runat="server" ImageUrl="~/NuoveImm/Img_Aggiungi.png"
                    ToolTip="Inserisci voce" Style="cursor: pointer" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare').value='1';"
                    TabIndex="413" />
                    <img id="imgAnnullaVoce" alt="Annulla Inserimento" src="../NuoveImm/img_AnnullaVoce.png"
                        onclick="return imgAnnullaVoce_onclick()" style="cursor: pointer" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Image ID="ImgSfondoSchema" runat="server" ImageUrl="~/ImmDiv/SfondoDim2.jpg"
        Style="z-index: 190; position: absolute; top: 56px; left: 197px; height: 503px;"
        BackColor="White" />
</div>
<div id="Storno" style="margin: 0px; position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; background-color: #DBDBDB; visibility: visible; z-index: 199;">
    <div id="Storno2" style="position: absolute; margin-left: -305px; margin-top: -265px; width: 610px; height: 530px; top: 50%; left: 50%; background-image: url('../ImmDiv/SfondoDim2.jpg');"
        align="center">
        <table style="text-align: left; width: 100%" align="center">
            <tr>
                <td style="height: 19px; padding-left: 15px; padding-right: 15px; padding-top: 10px;">
                    <strong><span style="font-family: Arial">
                        <asp:Label ID="Label3" runat="server" Text="BOLLETTA DA STORNARE" Width="440px"></asp:Label></span></strong>
                    <asp:Label ID="lblAvvisoBoll" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
                        Visible="False"></asp:Label>
                    <br />
                    <hr style="color: #006699" />
                    <table style="width: 100%; font-family: Arial; font-size: 10pt;" cellpadding="3">
                        <tr>
                            <td align="left">
                                <span style="font-size: 10pt; font-family: Arial">Num.Bolletta</span>
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="lblNBoll" runat="server"></asp:Label></b>
                            </td>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial">Tipo Bolletta</span>
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="lblTipoBoll" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <span style="font-size: 10pt; font-family: Arial">Competenza Dal</span>
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="lblRiferimDal" runat="server"></asp:Label></b>
                            </td>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial">Competenza Al</span>
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="lblRiferimAl" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial">Emissione</span>
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="lblDataEmiss" runat="server"></asp:Label></b>
                            </td>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial">Scadenza</span>
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="lblDataScad" runat="server"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial">Importo Totale</span>
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="lblImpTOT" runat="server"></asp:Label></b>
                            </td>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial">Importo Pagato</span>
                            </td>
                            <td>
                                <b>
                                    <asp:Label ID="lblImpPag" runat="server" Font-Overline="False"></asp:Label></b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial;">Motivazioni Storno</span>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbMotivoStorno" runat="server" Width="250px" Font-Names="Arial"
                                    Font-Size="10pt">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial;">Emissione Nuova Bolletta</span>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbEmetti" runat="server" Width="250px" Font-Names="Arial"
                                    Font-Size="10pt">
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left: 15px; padding-right: 15px;">
                    <div id="BollEmissione" style="display: block;">
                        <strong><span style="font-family: Arial">BOLLETTA DA EMETTERE</span></strong>
                        <br />
                        <hr style="color: #006699" />
                        <table style="width: 100%" cellpadding="1" cellspacing="1">
                            <tr>
                                <td>
                                    <span style="font-size: 10pt; font-family: Arial">Competenza Dal</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCompetenzaDal" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        ToolTip="Inizio periodo di riferimento (gg/mm/aaaa)" Width="71px" TabIndex="100"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCompetenzaDal"
                                        ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <span style="font-size: 10pt; font-family: Arial">Competenza Al</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCompetenzaAl" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        ToolTip="Fine periodo di riferimento (gg/mm/aaaa)" Width="71px" TabIndex="101"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtCompetenzaAl"
                                        ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="font-size: 10pt; font-family: Arial">Emissione</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDataEmiss" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        ToolTip="Data Emissione (gg/mm/aaaa)" Width="71px" ReadOnly="True" TabIndex="102"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDataEmiss"
                                        ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    <span style="font-size: 10pt; font-family: Arial">Scadenza</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDataScad" runat="server" Font-Names="Arial" Font-Size="9pt" ToolTip="Scadenza (gg/mm/aaaa)"
                                        Width="71px" TabIndex="103"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtDataScad"
                                        ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <span style="font-size: 9pt; font-family: Arial">VOCI BOLLETTA</span><br />
                        <table style="width: 100%">
                            <tr>
                                <td style="vertical-align: top;">
                                    <div style="overflow: auto;">
                                        <asp:ListBox ID="lstVociBoll" runat="server" Font-Names="Courier New" Font-Size="8pt"
                                            Width="500px" Height="50px" TabIndex="5"></asp:ListBox>
                                    </div>
                                </td>
                                <td style="vertical-align: top;">
                                    <table>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <img id="ImgAggiungiVoci" alt="Aggiungi Voce nello Schema" onclick="document.getElementById('Tab_Bollette1_txtImportoVoce2').value='0,00';document.getElementById('InserisciVociStorno').style.visibility='visible';"
                                                    src="../NuoveImm/img_Aggiungi.png" style="cursor: pointer" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImgEliminaVoci" runat="server" ImageUrl="~/NuoveImm/img_Elimina.png"
                                                    ToolTip="Elimina Voce dallo Schema" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare1').value='1';"
                                                    TabIndex="405" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImgCopiaVoci" runat="server" ImageUrl="~/NuoveImm/img_CopiaSchema.png"
                                                    ToolTip="Copia tutte le voci dallo schema" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare1').value='1';"
                                                    TabIndex="405" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="border-top: solid 1px black; padding-left: 15px; padding-right: 15px; width: 500px;">
                            <tr>
                                <td style="font-size: 10pt; font-weight: bold; font-family: Arial; width: 245px; color: #0062C4;">Totale
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTotSt" runat="server" Width="150px" Font-Names="Courier New"
                                        Font-Size="8pt" TabIndex="406" ReadOnly="True" Style="color: #0062C4; text-align: left;"
                                        BorderStyle="None" BorderWidth="0px" Font-Bold="True"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left: 15px; padding-right: 15px; z-index: 400px;">
                    <span style="font-size: 9pt; font-family: Arial">NOTE</span><br />
                    <asp:TextBox ID="txtNoteBoll" runat="server" TextMode="MultiLine" Width="500px" Height="15px"
                        Font-Names="Arial" Font-Size="10pt" CssClass="dimensioneFissa"></asp:TextBox>
                </td>
            </tr>
            <tr style="text-align: right; font-size: 12pt; font-family: Times New Roman">
                <td style="text-align: right;">
                    <table border="0" cellpadding="0" cellspacing="0" align="right">
                        <tr>
                            <td>
                                <asp:ImageButton ID="ImgSalvaBoll" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare1').value='0';controllaSalvaStorno();"
                                    ToolTip="Salva" Style="cursor: pointer" TabIndex="6" />
                                &nbsp;<asp:ImageButton ID="ImgAnnullaBoll" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare1').value = '0';document.getElementById('Tab_Bollette1_V3').value='';"
                                    ToolTip="Esci senza inserire" Style="cursor: pointer" TabIndex="7" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnStorno" runat="server" Height="0" Width="0" OnClientClick="document.getElementById('Tab_Bollette1_txtAppare1').value='1';" />
    </div>
</div>
<div id="InserisciVociStorno" style="border: 2px solid red; left: 340px; width: 334px; position: absolute; visibility: hidden; top: 261px; height: 202px; background-color: #D7D7D7; z-index: 205;">
    <table width="100%">
        <tr>
            <td>
                <span style="font-size: 10pt; font-family: Arial"><strong>Voce</strong></span>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="cmbVoceSchema2" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="317px" TabIndex="411">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 10pt; font-family: Arial">Importo in Euro (2 cifre decimali)</span></strong>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtImportoVoce2" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="74px" TabIndex="412"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator10"
                        runat="server" ControlToValidate="txtImportoVoce2" ErrorMessage="Errore" Font-Bold="True"
                        Font-Names="ARIAL" Font-Size="7pt" ValidationExpression="^(-?)\b\d*,\d{2}\b"
                        ForeColor="Red"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 10pt; font-family: Arial">Note (andranno in stampa i
                    primi 30 caratteri)</span></strong>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtNoteVoci2" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="314px" TabIndex="412"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right">&nbsp;<asp:ImageButton ID="btnInserisciVoce2" runat="server" ImageUrl="~/NuoveImm/Img_Aggiungi.png"
                ToolTip="Inserisci voce" Style="cursor: pointer" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Bollette1_txtAppare1').value='1';"
                TabIndex="413" />
                <img id="imgAnnullaVoce2" alt="Annulla Inserimento" src="../NuoveImm/img_AnnullaVoce.png"
                    onclick="return imgAnnullaVoce2_onclick()" style="cursor: pointer" />
            </td>
        </tr>
    </table>
</div>
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
<asp:HiddenField ID="HDeposito" runat="server" />
<asp:HiddenField ID="percenSost" runat="server" Value="0" ClientIDMode="Static" />
<script type="text/javascript">



    document.getElementById('InserimentoVoce').style.visibility = 'hidden';
    document.getElementById('InserisciVociStorno').style.visibility = 'hidden';
    if (document.getElementById('Tab_Bollette1_bolletteAssenti').value == '1') {
        //document.getElementById('divContab').style.height = '100px';
    }
    NascondiDivEmissione();

    if (document.getElementById('Tab_Bollette1_menuGest').value == '1') {
        document.getElementById('SceltaTipoBoll').style.display = 'block';
        //document.getElementById('SceltaTipoBoll').style.visibility = 'visible';
        document.getElementById('SceltaTipoBollCont').style.display = 'none';
    } else {
        document.getElementById('SceltaTipoBoll').style.display = 'none';
        //document.getElementById('SceltaTipoBoll').style.visibility = 'hidden';
        document.getElementById('SceltaTipoBollCont').style.display = 'block';

    }

    function ControllaTabVuoto() {


    }

    function NascondiDivEmissione() {
        if (document.getElementById('BollEmissione')) {
            if (document.getElementById('Tab_Bollette1_cmbEmetti').value == '1') {
                document.getElementById('BollEmissione').style.display = 'block';
                document.getElementById('Storno2').style.height = '530px';
            }
            else {
                document.getElementById('Storno2').style.height = '300px';
                document.getElementById('BollEmissione').style.display = 'none';
            }
        }
    }

    function StornoBolletta() {
        if (document.getElementById('Tab_Bollette1_V3').value != '') {
            document.getElementById('Tab_Bollette1_btnStorno').click();
        }
        else {
            document.getElementById('Tab_Bollette1_txtAppare1').value = '0';
            alert('Selezionare la bolletta dalla lista che si desidera stornare!');
        }
    }

    function IMG1_onclick() {
        //if (document.getElementById('VIRTUALE').value == '0') {
        var oggi = new Date();
        var G = oggi.getDate();
        var M = (oggi.getMonth() + 1);
        if (G < 10) {
            var gg = "0" + oggi.getDate();
        }
        else {
            var gg = oggi.getDate();
        }

        if (M < 10) {
            var mm = "0" + (oggi.getMonth() + 1);
        }
        else {
            var mm = (oggi.getMonth() + 1);
        }
        var aa = oggi.getFullYear();
        var data = gg + "/" + mm + "/" + aa;

        if (document.getElementById('Tab_Bollette1_menuGest').value == '1') {
            document.getElementById('SceltaTipoBoll').style.display = 'block';
            document.getElementById('SceltaTipoBollCont').style.display = 'none';
            document.getElementById('Tab_Bollette1_txtEmissione').readOnly = false;
        } else {
            document.getElementById('SceltaTipoBoll').style.display = 'none';
            document.getElementById('SceltaTipoBollCont').style.display = 'block';
            document.getElementById('Tab_Bollette1_txtEmissione').readOnly = true;
        }

        document.getElementById('USCITA').value = '1';
        document.getElementById('Tab_Bollette1_txtPeriodoDa').value = '';
        document.getElementById('Tab_Bollette1_txtPeriodoAl').value = '';
        document.getElementById('Tab_Bollette1_txtEmissione').value = data;
        document.getElementById('Tab_Bollette1_txtScadenza').value = '';
        document.getElementById('Tab_Bollette1_txtNote').value = '';
        document.getElementById('Tab_Bollette1_lstVociBolletta').options.length = 0;

        //document.getElementById('Tab_Bollette1_IdBolletta').value = '';
        myOpacity1.toggle();
        //}
        //else {
        //    document.getElementById('USCITA').value = '1';
        //    alert('Non è possibile inserire una nuova bolletta!');
        //    document.getElementById('USCITA').value = '0';
        //}
    }

    function imgAnnullaVoce_onclick() {
        document.getElementById('Tab_Bollette1_txtImportoVoce').value = '';
        document.getElementById('Tab_Bollette1_txtAppare').value = '0';
        document.getElementById('InserimentoVoce').style.visibility = 'hidden';
    }
    function imgAnnullaVoce2_onclick() {
        document.getElementById('Tab_Bollette1_txtImportoVoce2').value = '';
        document.getElementById('Tab_Bollette1_txtAppare1').value = '0';
        document.getElementById('InserisciVociStorno').style.visibility = 'hidden';
    }
    function controllaSalvaStorno() {

        var txtCompetenzaDal;
        var txtCompetenzaAl;
        var txtDataEmiss;
        var txtDataScad;
        var lstVociNewBoll;
        var motivoStorno;
        var chiediConferma;
        var errore = 0;

        txtCompetenzaDal = document.getElementById('Tab_Bollette1_txtCompetenzaDal').value;
        txtCompetenzaAl = document.getElementById('Tab_Bollette1_txtCompetenzaAl').value;
        txtDataEmiss = document.getElementById('Tab_Bollette1_txtDataEmiss').value;
        txtDataScad = document.getElementById('Tab_Bollette1_txtDataScad').value;
        motivoStorno = document.getElementById('Tab_Bollette1_cmbMotivoStorno').value;
        //lstVociNewBoll = document.getElementById('Tab_Bollette1_lstVociBoll');

        if (document.getElementById('Tab_Bollette1_cmbEmetti').value == '1') {
            if (motivoStorno == '-1' || txtCompetenzaDal == '' || txtCompetenzaAl == '' || txtDataEmiss == '' || txtDataScad == '' || document.getElementById('Tab_Bollette1_lstVociBoll').options.length == 0) {
                alert('Compilare tutti i dati!');
                errore = 1;
                document.getElementById('Tab_Bollette1_txtAppare1').value = '1';
            }
            else {
                errore = 0;
            }
        }

        if (motivoStorno == '-1') {
            alert('Inserire la motivazione dello storno!');
            errore = 1;
            document.getElementById('Tab_Bollette1_txtAppare1').value = '1';
        }
        if (errore == 0) {
            if (document.getElementById('Tab_Bollette1_cmbEmetti').value == '1') {
                if (document.getElementById('Tab_Bollette1_lblAvvisoBoll')) {
                    chiediConferma = window.confirm("Attenzione...Salvando sarà creato un documento di storno negativo e per l\'importo pagato si genererà un credito gestionale per Eccedenza. Continuare?");
                } else {
                    chiediConferma = window.confirm("Attenzione...Salvando sarà creata una nuova emissione ed un documento di storno negativo per la bolletta selezionata. Continuare?");
                }
            }
            else {
                if (document.getElementById('Tab_Bollette1_lblAvvisoBoll')) {
                    chiediConferma = window.confirm("Attenzione...Salvando sarà creato un documento di storno negativo e per l\'importo pagato si genererà un credito gestionale per Eccedenza. Continuare?");
                } else {
                    chiediConferma = window.confirm("Attenzione...Salvando sarà creato un documento di storno negativo per la bolletta selezionata. Continuare?");
                }
            }

            if (chiediConferma == false) {
                document.getElementById('Tab_Bollette1_confermaStorno').value = '0';
            } else {
                //document.getElementById('Tab_Bollette1_ImgSalvaBoll').style.visibility = 'hidden';
                document.getElementById('Tab_Bollette1_confermaStorno').value = '1';
                if (document.getElementById('Attesa')) {
                    document.getElementById('Attesa').style.visibility = 'visible';
                }
            }
        }
    }

    function ApriMav() {
        document.getElementById('USCITA').value = '1';
        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di generare il MAV on Line!');
        }
        else {
            document.getElementById('USCITA').value = '0';
            if (document.getElementById('Tab_Bollette1_V3').value != '') {
                var fin;
                fin = window.open('Sondrio.aspx?ID=' + document.getElementById('Tab_Bollette1_V3').value, 'MAV', '');
                fin.focus();
            }
            else {
                alert('Selezionare una bolletta dalla lista!');
            }
        }
    }

    function ApriSolleciti() {
        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima visualizzare l\'anteprima!');
        }
        else {
            if (document.getElementById('Tab_Bollette1_V3').value != '') {
                var fin;
                fin = window.open('ElencoSolleciti.aspx?ID=' + document.getElementById('Tab_Bollette1_V3').value, 'Solleciti', 'height=350,top=0,left=0,width=350');
                fin.focus();
                document.getElementById('USCITA').value = '0';
            } else {
                alert('Selezionare una bolletta dalla lista!');
            }
        }
    }

    function Ingiunzione() {

        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');

        } else {
            var oWnd = $find('RadWindow1');
            oWnd.setUrl('ValorizzaIngiunzione.aspx?IDboll=' + document.getElementById('Tab_Bollette1_V3').value + '&IDCONN=' + document.getElementById('Tab_Bollette1_txtConnessione').value);
            oWnd.show();
        }

    }

    function ApriAnteprima() {
        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima visualizzare l\'anteprima!');
        }
        else {
            if (document.getElementById('Tab_Bollette1_V3').value != '') {
                var fin;
                fin = window.open('AnteprimaBolletta.aspx?ID=' + document.getElementById('Tab_Bollette1_V3').value, 'Anteprima' + document.getElementById('Tab_Bollette1_V3').value, 'top=0,left=0,resizable=yes,scrollbars=yes');
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

    function ApriModulo() {
        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di generare il Modulo di Pagamento!');
        }
        else {
            if (document.getElementById('Tab_Bollette1_V3').value != '') {
                var fin;
                fin = window.open('MavIntesa.aspx?X=' + document.getElementById('Tab_Bollette1_V3').value, 'Modulo', '');
                //fin = window.open('ModelloIntesa.aspx?ID=' + document.getElementById('Tab_Bollette1_V3').value, 'Modulo', '');
                fin.focus();
                document.getElementById('USCITA').value = '0';
            }
        }
    }

    function ConfermaElimGest() {
        var chiediConferma;
        document.getElementById('USCITA').value = '1';
        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');
        }
        else {
            //document.getElementById('USCITA').value = '0';
            if (document.getElementById('Tab_Bollette1_V3').value != '') {
                chiediConferma = window.confirm("Attenzione...Procedendo verrà cancellata la scrittura selezionata. Continuare?");

                if (chiediConferma == true) {
                    document.getElementById('Tab_Bollette1_confermaElimGest').value = '1';
                    document.getElementById('Tab_Bollette1_btnEliminaGest').click();
                    //document.getElementById('Tab_Bollette1_confermaElimGest').value = '0';
                } else {
                    //document.getElementById('Tab_Bollette1_confermaElimGest').value = '1';
                    //document.getElementById('Tab_Bollette1_btnEliminaGest').click();
                }
            }
            else {
                alert('Selezionare un documento dalla lista!');
            }
        }
    }


    function ConfermaAnnullaGest() {
        var chiediConferma;
        document.getElementById('USCITA').value = '1';
        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');
        }
        else {
            if (document.getElementById('Tab_Bollette1_V3').value != '') {
                chiediConferma = window.confirm("Attenzione...Procedendo verrà annullata la scrittura selezionata. Continuare?");
                if (chiediConferma == true) {
                    document.getElementById('Tab_Bollette1_confermaAnnullaGest').value = '1';
                    document.getElementById('Tab_Bollette1_btnAnnullaGest').click();
                }
            }
            else {
                alert('Selezionare un documento dalla lista!');
            }
        }
    }


    function AttivaSpostamento() {
        var chiediConferma;
        document.getElementById('USCITA').value = '1';
        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');
        }
        else {
            if (document.getElementById('Tab_Bollette1_V3').value != '') {
                chiediConferma = window.confirm("Attenzione...Procedendo verrà bloccata/sbloccata la scrittura selezionata. Continuare?");
                if (chiediConferma == true) {
                    document.getElementById('Tab_Bollette1_btnSbloccaGest').click();
                }
            }
            else {
                alert('Selezionare un documento dalla lista!');
            }
        }
    }


    function ScegliElaborazione() {
        if (document.getElementById('Tab_Bollette1_V3').value != '') {
            if (document.getElementById('Tab_Bollette1_importo').value > 0) {
                var txt1 = 'SELEZIONARE IL TIPO DI ELABORAZIONE CHE DI DESIDERA EFFETTUARE:<br /><br /><input type="radio" id = "elaborazTOT" name="rdbScegli" value="elaborazTOT"/>Elaborazione totale<br /><input type="radio" id = "elaborazPARZ" name="rdbScegli" value="elaborazPARZ"/>Elaborazione parziale';
                var txt2 = 'INSERIRE LA PERCENTUALE DI SOSTENIBILITA\' PER LA RIPARTIZIONE DELL\'IMPORTO IN PIU\' MENSILITA\'<br /><br />Percentuale di sostenibilità:<br /><input type="text" id="Percentuale" name="Percentuale" value="' + document.getElementById('percenSost').value + '" style="font-family: arial; font-size: 10pt; width: 50px;"/>%';
                var errore;
                errore = '0';
                var EseguiPassaggi = {
                    state0: {
                        html: txt1,
                        buttons: { Procedi: 1, Annulla: 0 },
                        focus: 0,
                        submit: function (e, v, m, f) {
                            if (v != undefined)
                                if (v == 1) {
                                    if (v != '2') {
                                        if (f.rdbScegli != undefined) {
                                            errore = '0';

                                            if (f.rdbScegli == "elaborazTOT") {
                                                scelta = "1"
                                            }
                                            if (f.rdbScegli == "elaborazPARZ") {
                                                scelta = "2"
                                            }
                                            if (scelta == "1") {
                                                if (document.getElementById('Tab_Bollette1_importo').value < 0) {

                                                }
                                                else {
                                                    var sicuro = window.confirm('Attenzione...Procedendo il debito verrà applicato totalmente ed eliminato dalla partita gestionale. Sei sicuro di voler proseguire?');
                                                    if (sicuro == true) {
                                                        //var tipoImport;
                                                        window.open('SpostamGestionaleTot.aspx?IDBOLL=' + document.getElementById('Tab_Bollette1_V3').value + '&IDCONTR=' + document.getElementById('Tab_Bollette1_txtIdContratto').value + '&TIPO=DEB' + '', 'Spostam1', 'height=10,top=150,left=250,width=10');
                                                    }
                                                    else {
                                                        return false;
                                                    }
                                                }
                                            }
                                            if (scelta == "2") {
                                                if (document.getElementById('Tab_Bollette1_importo').value > 0) {
                                                    jQuery.prompt.goToState('state1');
                                                    return false;
                                                } else {
                                                    window.open('SpostamGestionaleParziale.aspx?PERC=' + f.Percentuale + '&IDBOLL=' + document.getElementById('Tab_Bollette1_V3').value + '&IDCONTR=' + document.getElementById('Tab_Bollette1_txtIdContratto').value + '&TIPO=CRED' + '', 'Spostam2', 'height=10,top=150,left=250,width=10');
                                                }
                                            }
                                        }
                                        else {
                                            alert('Selezionare la funzione che si intende eseguire!');
                                            errore = '1';
                                            return false;
                                        }
                                    }
                                }
                                else {
                                    return true;
                                }
                            return true;
                        }
                    },
                    state1: {
                        html: txt2,
                        buttons: { Indietro: 0, Procedi: 1 },
                        focus: 0,
                        submit: mycallScegliElaborazione
                    }
                };
                jQuery.prompt(EseguiPassaggi);
            }
            else {
                if (document.getElementById('Tab_Bollette1_importo').value == 0) {
                    alert('Scrittura non elaborabile. Importo totale pari a zero!');
                    errore = '1';
                    return false;
                }
                else {

                    ScegliTipoRipartizione();
                    //                    var sicuro = window.confirm('Attenzione...spostando la scrittura dalla partita gestionale essa verrà ripartita sulle bollette non ancora pagate (sollecitate e scadute). Sei sicuro di voler proseguire?');
                    //                    if (sicuro == true) {
                    //                        window.open('SpostamGestionaleTot.aspx?IDBOLL=' + document.getElementById('Tab_Bollette1_V3').value + '&IDCONTR=' + document.getElementById('Tab_Bollette1_txtIdContratto').value + '&TIPO=CRED' + '', 'Spostam3', 'height=10,top=150,left=250,width=10');
                    //                    }
                    //                    else {
                    //                        return false;
                    //                    }
                }
            }
        }
        else {
            alert('Selezionare una bolletta dalla lista!');
        }

    }


    function mycallScegliElaborazione(e, v, m, f) {

        var controllaPerc = /^\d+?$/;
        if (v == 0) {
            jQuery.prompt.goToState('state0');
            return false;
        }
        if (v == 1) {
            txtPerc = m.children('#Percentuale');
            if (!controllaPerc.test(f.Percentuale)) {

                txtPerc.css("border", "solid #ff0000 1px");
                errore = '1';
                return false;
            }
            else {
                if (!(f.Percentuale >= 1 && f.Percentuale <= 100)) {
                    txtPerc.css("border", "solid #ff0000 1px");
                    errore = '1';
                    return false;
                }
                else {
                    txtPerc.css("border", "solid #7f9db9 1px");
                }
            }

            window.open('SpostamGestionaleParziale.aspx?PERC=' + f.Percentuale + '&IDBOLL=' + document.getElementById('Tab_Bollette1_V3').value + '&IDCONTR=' + document.getElementById('Tab_Bollette1_txtIdContratto').value + '&TIPO=DEB' + '', 'Spostam4', 'height=10,top=150,left=250,width=10');
            return true;
        }
    }

    function ApriModuloRimborsoDC() {
        var chiediConferma;
        document.getElementById('USCITA').value = '1';
        if (document.getElementById('txtModificato').value == '1') {
            alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');
        }
        else {
            var dialogResults = window.showModalDialog('ModuloRimborsoDC.aspx?IDC=' + document.getElementById('txtIdContratto').value, window, 'status:no;dialogWidth:490px;dialogHeight:350px;dialogHide:true;help:no;scroll:no;resizable:no;');
            if (dialogResults != undefined) {
                if (dialogResults == '1') {
                    document.getElementById('USCITA').value = '0';
                    document.getElementById('MostrMsgSalva').value = '0';
                    if (document.getElementById('imgSalva')) {
                        document.getElementById('imgSalva').click();
                    }
                }
            }
        }
    }

    function ApriModuloRimborso() {
        if (document.getElementById('Tab_Bollette1_V3').value != '') {
            var chiediConferma;
            document.getElementById('USCITA').value = '1';
            if (document.getElementById('txtModificato').value == '1') {
                alert('Attenzione...sono state apportate delle modifiche al contratto. Salvare prima di procedere!');
            }
            else {
                var dialogResults = window.showModalDialog('ModuloRimborso.aspx?IDG=' + document.getElementById('Tab_Bollette1_V3').value + '&IDC=' + document.getElementById('txtIdContratto').value, window, 'status:no;dialogWidth:490px;dialogHeight:350px;dialogHide:true;help:no;scroll:no;resizable:no;');
                if (dialogResults != undefined) {
                    if (dialogResults == '1') {
                        document.getElementById('USCITA').value = '0';
                        document.getElementById('MostrMsgSalva').value = '0';
                        if (document.getElementById('imgSalva')) {
                            document.getElementById('imgSalva').click();
                        }
                    }
                }
            }
        }
        else {
            alert('Attenzione...Selezionare una bolletta a credito prima di procedere!');
        }
    }

    function ScegliTipoRipartizione() {
        var txt1 = 'SELEZIONARE IL TIPO DI RIPARTIZIONE CHE DI DESIDERA EFFETTUARE:<br /><br /><input type="radio" id = "elaborazAUTOM" name="rdbScegli" value="elaborazAUTOM"/>Ripartiz. automatica<br /><input type="radio" id = "elaborazSELETTIVA" name="rdbScegli" value="elaborazSELETTIVA"/>Ripartiz. selettiva';
        var txt2 = 'SELEZIONARE LA DESTINAZIONE DELL\'EVENTUALE CREDITO RESIDUO:<br /><br /><input type="radio" id = "rdbCredito" name="rdbDestinaz" value="1"/>Partita Gestionale<br /><input type="radio" id = "rdbSchema" name="rdbDestinaz" value="0"/>Schema Bolletta';

        var EseguiPassaggi = {
            state0: {
                html: txt1,
                buttons: { Procedi: 1, Annulla: 2 },
                focus: 0,
                submit: function (e, v, m, f) {
                    if (v != undefined)

                        if (v != '2') {
                            if (f.rdbScegli != undefined) {
                                if (f.rdbScegli == "elaborazAUTOM") {
                                    if (document.getElementById('sceltaDestEcc').value == '1') {
                                        jQuery.prompt.goToState('state1');
                                        return false;
                                    }
                                    else {


                                        var sicuro = window.confirm('Attenzione...spostando la scrittura dalla partita gestionale essa verrà ripartita sulle bollette non ancora pagate (sollecitate e scadute). Sei sicuro di voler proseguire?');


                                        if (sicuro == true) {
                                            window.open('SpostamGestionaleTot.aspx?GEST=1&IDBOLL=' + document.getElementById('Tab_Bollette1_V3').value + '&IDCONTR=' + document.getElementById('Tab_Bollette1_txtIdContratto').value + '&TIPO=CRED' + '', 'Spostam3', 'height=10,top=150,left=250,width=10');
                                        }
                                        else {
                                            return false;
                                        }

                                    }



                                }
                                if (f.rdbScegli == "elaborazSELETTIVA") {
                                    window.open('BolletteDaPagare.aspx?IDGEST=' + document.getElementById('Tab_Bollette1_V3').value + '&IDCONTR=' + document.getElementById('Tab_Bollette1_txtIdContratto').value + '', 'Spostam4', 'height=560,top=150,left=250,width=940');
                                }
                            }
                        }
                    return true;
                }
            },
            state1: {
                html: txt2,
                buttons: { Indietro: 0, Procedi: 1 },
                focus: 0,
                submit: mycallScegliDestEcced
            }

        }
        jQuery.prompt(EseguiPassaggi);

    };
    function mycallScegliDestEcced(e, v, m, f) {

        if (v == 0) {
            jQuery.prompt.goToState('state0');
            return false;
        }
        else {
            if (f.rdbDestinaz != undefined) {

                var sicuro = window.confirm('Attenzione...spostando la scrittura dalla partita gestionale essa verrà ripartita sulle bollette non ancora pagate (sollecitate e scadute). Sei sicuro di voler proseguire?');


                if (sicuro == true) {
                    window.open('SpostamGestionaleTot.aspx?GEST=' + f.rdbDestinaz + '&IDBOLL=' + document.getElementById('Tab_Bollette1_V3').value + '&IDCONTR=' + document.getElementById('Tab_Bollette1_txtIdContratto').value + '&TIPO=CRED' + '', 'Spostam3', 'height=10,top=150,left=250,width=10');
                }
                else {
                    return false;
                }
            }
            else {
                alert('Selezionare la funzione che si intende eseguire!');
                errore = '1';
                return false;

            }
        }
    }
</script>
