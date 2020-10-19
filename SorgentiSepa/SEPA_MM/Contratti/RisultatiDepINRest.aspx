<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiDepINRest.aspx.vb" Inherits="Contratti_RisultatiDepINRest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        #salvataggio
        {
            top: 352px;
            left: 45px;
        }
        .style1
        {
            width: 357px;
        }
        .style2
        {
            width: 171px;
            text-align: left;
        }
        .style3
        {
            height: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; background-repeat: no-repeat;top:0px;">
        <br />
        <table style="width:100%;">
            <tr>
                <td style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: left;">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Restituzione depositi cauzioni in corso
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></strong></span>
                </td>
            </tr>
            <tr>
                <td style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">

                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;&nbsp;
                <asp:ImageButton ID="imgSelezionaTutto" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SelezionaTutto.png" />
                    &nbsp;
                                    <asp:ImageButton ID="imgDeselezionaTutto" runat="server" 
                        ImageUrl="~/NuoveImm/Img_DeselezionaTutto.png" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;
                    <asp:ImageButton ID="btnExport" runat="server" 
                        ImageUrl="~/NuoveImm/Img_ExportExcel.png" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img id="imgSalvaLista" alt="" src="../NuoveImm/Img_SalvaDepCauz.png" 
                        style="cursor: pointer" onclick="Visualizza()" /></td>
            </tr>
        </table>
        <br />&nbsp;&nbsp;
        <div id="contenitore" style="overflow: auto; width: 773px; height: 347px;">

    <asp:DataGrid ID="DataGridRateEmesse" runat="server" AutoGenerateColumns="False"
        CellPadding="3" GridLines="Vertical" Style="z-index: 11;
        left: 50px; top: 81px" Width="1179px" Font-Bold="False" Font-Italic="False" 
        Font-Names="Arial" Font-Overline="False" Font-Size="XX-Small" 
        Font-Strikeout="False" Font-Underline="False" AllowPaging="True" 
            PageSize="100" BackColor="White" BorderColor="#999999" BorderStyle="None" 
            BorderWidth="1px">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <EditItemStyle Font-Names="Arial" Font-Size="9pt" />
        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" Font-Names="Arial" 
            ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
            Mode="NumericPages" />
        <AlternatingItemStyle BackColor="#DCDCDC" Font-Names="Arial" />
        <ItemStyle BackColor="#EEEEEE" Font-Names="Arial" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            Font-Names="ARIAL" />
        <Columns>
                                    	<asp:TemplateColumn>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChSelezionato" runat="server"/>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="IDC" HeaderText="ID" Visible="False">
                                        </asp:BoundColumn>
            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD_CONTRATTO">
            </asp:BoundColumn>
                                        <asp:BoundColumn DataField="COD_TIPOLOGIA_CONTR_LOC" 
                                            HeaderText="TIPO CONTRATTO">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
            <asp:BoundColumn DataField="DATA_OPERAZIONE" HeaderText="DATA OPERAZIONE">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="CREDITO" HeaderText="CREDITO">
            </asp:BoundColumn>
            <asp:BoundColumn DataField="INTERESSI" HeaderText="INTERESSI"></asp:BoundColumn>
            <asp:BoundColumn DataField="TIPO_PAGAMENTO" HeaderText="TIPOLOGIA PAGAMENTO"></asp:BoundColumn>
            <asp:BoundColumn DataField="NOTE_PAGAMENTO" HeaderText="NOTE">
            </asp:BoundColumn>
                                        <asp:BoundColumn DataField="STRUTTURA" HeaderText="STRUTTURA"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_CERT_PAG" HeaderText="DATA CERT. PAGAMENTO">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NUM_CDP" HeaderText="NUM.CDP"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ANNO_CDP" HeaderText="ANNO CDP"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_MANDATO" HeaderText="DATA MANDATO">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NUM_MANDATO" HeaderText="NU. MANDATO">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ANNO_MANDATO" HeaderText="ANNO MANDATO">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_BOLLETTA" HeaderText="ID_BOLLETTA" 
                                            Visible="False"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    </div>
    </div>
    <div id="salvataggio" 
        
        
        style="position: absolute; width: 100%; height: 100%; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); top: 0px; left: 0px; background-repeat: no-repeat; visibility: visible;">
        <table style="width:100%;">
            <tr>
                <td class="style3">
                    <br />
                    <br />
                    <br />
                    
                    <br />
                </td>
            </tr>
            <tr style="text-align: center">
                <td align="center">
                    <table style="border: 2px solid #000080; width:467px;">
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td class="style1">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Font-Strikeout="False" ForeColor="#0000CC" Text="Data Certificato Pag."></asp:Label>
                            </td>
                            <td style="text-align: left" class="style1">
                                <asp:TextBox ID="txtDataCert" runat="server" Width="84px" TabIndex="1"></asp:TextBox>
                            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                    runat="server" ControlToValidate="txtDataCert"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="height: 14px;" TabIndex="-1" 
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Font-Strikeout="False" ForeColor="#0000CC" Text="Num. CDP"></asp:Label>
                            </td>
                            <td style="text-align: left" class="style1">
                                <asp:TextBox ID="txtNumCDP" runat="server" Width="84px" TabIndex="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Font-Strikeout="False" ForeColor="#0000CC" Text="Anno CDP"></asp:Label>
                            </td>
                            <td style="text-align: left" class="style1">
                                <asp:TextBox ID="txtAnnoCDP" runat="server" Width="56px" MaxLength="4" 
                                    TabIndex="3"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Font-Strikeout="False" ForeColor="#0000CC" Text="Data Mandato Pag."></asp:Label>
                            </td>
                            <td style="text-align: left" class="style1">
                                <asp:TextBox ID="txtDataMan" runat="server" Width="84px" TabIndex="4"></asp:TextBox>
                            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                    runat="server" ControlToValidate="txtDataMan"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="height: 14px;" TabIndex="-1" 
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Font-Strikeout="False" ForeColor="#0000CC" Text="Num. Mandato"></asp:Label>
                            </td>
                            <td style="text-align: left" class="style1">
                                <asp:TextBox ID="txtNumMandato" runat="server" Width="84px" TabIndex="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    Font-Strikeout="False" ForeColor="#0000CC" Text="Anno Mandato"></asp:Label>
                            </td>
                            <td style="text-align: left" class="style1">
                                <asp:TextBox ID="txtAnnoMandato" runat="server" Width="56px" MaxLength="4" 
                                    TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td style="text-align: right" class="style1">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td style="text-align: right" class="style1">
                                <asp:ImageButton ID="btnSalva" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Salva.png" />
&nbsp;&nbsp;&nbsp;
                                <img id="imgAnnulla" alt="" src="../NuoveImm/Img_Esci.png" 
                                    style="cursor: pointer" onclick="Nascondi()" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    <script language="javascript" type="text/javascript">
        document.getElementById('salvataggio').style.visibility = 'hidden';

        function Visualizza() {
            document.getElementById('salvataggio').style.visibility = 'visible';
        }

        function Nascondi() {
            document.getElementById('salvataggio').style.visibility = 'hidden';
        }
    </script>
    <script type="text/javascript">

        function CompletaData(e, obj) {
            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }


    </script>
    </form>
</body>
     <script language="javascript" type="text/javascript">
         document.getElementById('divLoading').style.visibility = 'hidden';
    </script>
</html>
