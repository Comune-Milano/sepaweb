<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SpedizioneConvocazioni.aspx.vb" Inherits="ANAUT_SpedizioneConvocazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Elenco Simulazioni</title>
    <style type="text/css">
        .style1
        {
            width: 375px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >

 
    <div>
    
        &nbsp;
        <table style="left: 0px; background-image: url('../NuoveImm/SfondoMaschere.jpg');
            width: 672px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="Label1" runat="server" 
                        Text="Data Spedizione Lettere Convocazione"></asp:Label>
                        
                    </strong></span>
                    <br />
                    <br />
                    <div id="contenitore" 
                        
                        
                        
                        
                        
                        style="border: 1px solid #000080; position:absolute; overflow: auto; visibility: visible; top: 58px; left: 14px; width: 646px; height: 315px;">
                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" ForeColor="#333333" GridLines="None" PageSize="8" 
                            style="position:absolute; z-index: 105; left: 0px; width: 96%; top: 0px;">
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" ForeColor="White" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="FILE SPEDITO">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChSelezionato" runat="server" />
                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <ItemStyle BackColor="#EFF3FB" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>
                    </div>
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
                    <br />
                    <br />
                    <table style="width:100%;">
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" 
                                    Text="Selezionare i/il file spedito e inserire la Data spedizione effettiva:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtData" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="68px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    <asp:ImageButton ID="btnSalva" runat="server" 
                        ImageUrl="~/NuoveImm/img_SalvaModelli.png" 
                        
            style="position:absolute; top: 491px; left: 455px; right: 1003px;" />
                                        <img id="imgEsci" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" 
                        onclick="document.location.href = 'pagina_home.aspx';" 
                        
    style="cursor:pointer;position:absolute; top: 490px; left: 583px;" />
    </div>
    <script type="text/javascript">
        //document.onkeydown = $onkeydown;


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
</html>

