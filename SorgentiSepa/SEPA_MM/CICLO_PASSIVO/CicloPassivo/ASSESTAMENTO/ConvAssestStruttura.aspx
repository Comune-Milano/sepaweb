<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConvAssestStruttura.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_ConvAssestStruttura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <style type="text/css">
            #form1
        {
            width: 793px;
            height: 668px;
        }

        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 12pt;
            color: #801f1c;
            text-align: left;
        }
        .style3
        {}
        </style>
        <script type = "text/javascript" language ="javascript" >
            window.name = "modal";

            function TastoInvio(e) {
                sKeyPressed1 = e.which;
                if (sKeyPressed1 == 13) {

                    e.preventDefault();
                }
            }

            function $onkeydown() {
                if (event.keyCode == 13) {
                    event.keyCode = '9';
                }
            }

            function SostPuntVirg(e, obj) {
                var keyPressed;
                keypressed = (window.event) ? event.keyCode : e.which;
                if (keypressed == 46) {
                    event.keyCode = 0;
                    obj.value += ',';
                    obj.value = obj.value.replace('.', '');
                }
                
            }
            function AutoDecimal2(obj) {

                obj.value = obj.value.replace('.', '');
                if (obj.value.replace(',', '.') != 0) {
                    var a = obj.value.replace(',', '.');
                    a = parseFloat(a).toFixed(2)
                    if (a != 'NaN') {
                        if (a.substring(a.length - 3, 0).length >= 4) {
                            var decimali = a.substring(a.length, a.length - 2);
                            var dascrivere = a.substring(a.length - 3, 0);
                            var risultato = '';
                            while (dascrivere.replace('-', '').length >= 4) {
                                risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                                dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                            }
                            risultato = dascrivere + risultato + ',' + decimali
                            //document.getElementById(obj.id).value = a.replace('.', ',')
                            document.getElementById(obj.id).value = risultato
                        }
                        else {
                            document.getElementById(obj.id).value = a.replace('.', ',')
                        }

                    }
                    else
                        document.getElementById(obj.id).value = ''
                }
            }
            var r = {
                'special': /[\W]/g,
                'quotes': /['\''&'\"']/g,
                'notnumbers': /[^\d\-\,]/g
            }
            function valid(o, w) {
                o.value = o.value.replace(r[w], '');

            }
        </script>
</head>
<body >
            <!-- Da mettere subito dopo l'apertura del tag <body> -->
          <div id="splash"      
            style="border: thin dashed #000066; position :absolute; z-index :500; text-align:center; font-size:10px; width: 760px; height: 465px; visibility :hidden ; vertical-align: top; line-height: normal; 
            top: 70px; left: 12px; background-color:#FFFFFF;">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <img src='../../../CONDOMINI/Immagini/load.gif' alt='caricamento in corso'/><br/><br/>
            caricamento in corso...<br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            &nbsp;
        </div>
        <script type="text/javascript">
            if (navigator.appName == 'Microsoft Internet Explorer') {
                document.onkeydown = $onkeydown;
            }
            else {
                window.document.addEventListener("keydown", TastoInvio, true);
            }
        </script>
    <form id="form1" runat="server" target ="modal">
    <table style="width: 99%; position:absolute; top: 15px; left: 9px;">
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td class="style1">
                            APPROVAZIONE 
                            GESTORE DEGLI IMPORTI DI ASSESTAMENTO</td>
                        <td style="vertical-align: bottom; text-align: right">
                                &nbsp;</td>
                        <td style="vertical-align: bottom; text-align: right">
                                &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3" style="text-align: center; font-size: 1pt;">
                <br />
            </td>
        </tr>
        <tr>
            <td>
                            <asp:Label ID="lblVoceBp" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="10pt" ForeColor="Black" CssClass="style4" 
                    Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table style="background-color: #507CD1; vertical-align: top; text-align: left; width: 760px; height: 0px; top: auto;" 
                    cellpadding="1" cellspacing="0">
                    <tr>
                        <td width="600px"
                            style = "border: 1px solid #C0C0C0; font-family :Arial; font-size : 8pt">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" ForeColor="White" Text="UFFICIO" CssClass="style4"></asp:Label>
                        </td>
                        <td width="80px"
                            
                            
                            
                            style = "border: 1px solid #C0C0C0; font-family :Arial; font-size : 8pt; text-align: center;">
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" ForeColor="White" Text="RICHIESTO" CssClass="style4"></asp:Label>
                        </td>
                        <td width="80px"
                            
                            
                            
                            
                            style = "border: 1px solid #C0C0C0; font-family :Arial; font-size : 8pt; text-align: center;">
                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" ForeColor="White" Text="APPROVATO" CssClass="style4"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <div id="Div_DataGridView" style="height: 450px; overflow: auto; width: 100%;">
            <asp:DataGrid ID="DgvApprAssest" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" BorderWidth="1px" Font-Bold="False" 
                        Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#333333" PageSize="30" Style="table-layout: auto; z-index: 101;
                left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                TabIndex="-1" Width="760px" BorderColor="#333333" CellPadding="1">
                <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" 
                    ForeColor="White" />
                <EditItemStyle Wrap="False" BackColor="#2461BF" />
                <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" 
                    ForeColor="#333333" />
                <PagerStyle Position="TopAndBottom" Visible="False" Wrap="False" 
                    BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="#EFF3FB" Wrap="False" Font-Bold="False" 
                    Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                    Font-Underline="False" />
                <ItemStyle Wrap="False" BackColor="White" Font-Bold="False" Font-Italic="False" 
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <Columns>
                    <asp:BoundColumn DataField="ID_STRUTTURA" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="STRUTTURA">
                        <HeaderStyle Width="595px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            Wrap="False" Font-Names="Arial" Font-Size="8pt" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ASSESTAMENTO">
                        <HeaderStyle Width="80px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="IMPORTO_APPR" Visible="False">
                        <HeaderStyle Width="80px" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="COMPLETO" Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn>
                        <EditItemTemplate>
                            <asp:TextBox runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtApprovato" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" MaxLength="12" style="text-align: right" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_APPR") %>' 
                                Width="75px"></asp:TextBox>
                            <asp:Label runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                            Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                            Wrap="False" />
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Wrap="False" BorderStyle="None" />
            </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td align ="right" >
                    <table>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: right; vertical-align: top;">
                            <asp:ImageButton ID="btnConfirm" runat="server" 
                                ImageUrl="../../../NuoveImm/Img_SalvaGrande.png" ToolTip="Salva" 
                                CausesValidation="False" 
                                onclientclick="return true;" TabIndex="-1" />
                            </td>
                            <td style="text-align: right; vertical-align: top;">
                                <img id="exit" alt="Esci" src="../../../NuoveImm/Img_Esci_AMM.png" 
                                title="Esci" style="cursor :pointer" 
                                onclick="window.close();"/></td>
                            <td style="text-align: right">
                                &nbsp; &nbsp;</td>
                        </tr>
                    </table>
    
            </td>
        </tr>
        <tr>
            <td class="style3">
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" 
                        Text="Label" Visible="False" Width="580px"></asp:Label>
    
            </td>
        </tr>
        <tr>
            <td>
                                            <asp:HiddenField ID="IdAssestamento" runat="server" Value="0" />
                                            <asp:HiddenField ID="IdVoce" runat="server" Value="0" />

</td>
        </tr>
    </table>
        <script type = "text/javascript" language ="javascript" >
            document.getElementById('splash').style.visibility = 'hidden';
            document.getElementById('dvvvPre').style.visibility = 'hidden';

    
    </script>

    </form>
</body>
</html>
