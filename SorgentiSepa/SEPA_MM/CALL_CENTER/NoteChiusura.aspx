<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NoteChiusura.aspx.vb" Inherits="CALL_CENTER_NoteChiusura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>

    <title>Tipologie Guasti</title>
    <style type="text/css">
        #form1
        {
            width: 780px;
            height: 620px;
        }
        .style1
        {
            width: 689px;
        }
                .CssMaiuscolo { TEXT-TRANSFORM: uppercase;}

        .style2
        {
            width: 50px;
        }
    </style>
    <script type="text/javascript" >
        var Selezionato;
        function Add() {
            document.getElementById('TextBox1').value = '2';
            myOpacity.toggle();
            document.getElementById('txtDescrizione').value = '';
            if (document.getElementById('lblstr')) {
                document.getElementById('lblstr').style.visibility = 'hidden';
                document.getElementById('cmbStruttura').style.visibility = 'hidden';

            }
            document.getElementById('txtid').value = '0';

        }
        function ApriDiv() {

            document.getElementById('TextBox1').value = '2';
            myOpacity.toggle();
        }
    </script>
</head>
<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server">
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Elenco 
    Note di Chiusura Segnalazione per Tipologie Guasti</span></strong><table style="width: 100%;">
        <tr>
            <td style="vertical-align: top; text-align: left" class="style1">
                &nbsp;</td>
            <td style="vertical-align: top; text-align: right">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left" class="style1">
                <div id="div" style="overflow: auto; height: 414px;">
                    <asp:DataGrid ID="DataGridGuasti" runat="server" AutoGenerateColumns="False" BackColor="White"
                        CellPadding="1" CellSpacing="1" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        GridLines="None" PageSize="24" 
                        Style="z-index: 105; left: 193px; top: 54px" Width="97%">
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle ForeColor="Black" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_TIPO_GUASTO" HeaderText="ID" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO_SEGN" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTA_DESC" HeaderText="NOTA CHIUSURA"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                    </asp:DataGrid>
                </div>
            </td>
            <td style="vertical-align: top; text-align: right">
                <table style="width:100%;">
                    <tr>
                        <td>
                            <img id="add" alt="" src="../NuoveImm/Img_Aggiungi.png" style ="cursor:pointer " onclick = "Add();" /></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnElimina" runat="server" 
                                ImageUrl="~/NuoveImm/Img_Elimina.png" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style1">
            <asp:TextBox ID="txtmia" runat="server" BackColor="#F3F3F3" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="10pt" MaxLength="100" 
                    ReadOnly="True" Width="570px"
               >Nessuna Selezione</asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>

        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
            <asp:HiddenField ID="txtid" runat="server" Value="0" />
                <asp:HiddenField ID="TextBox1" runat="server" />
                            <asp:HiddenField ID="txtOldDesc" runat="server" Value="" />

    </span></strong>
            <div id="divGuasto" 
        
        
        style="position: absolute; background-image: url('../ImmDiv/DivMGrande2.png'); top: 0px; left: 0px; height: 498px; width: 790px; z-index: 500; visibility: hidden;">
                <table style="position: absolute; top: 109px; left: 101px; z-index: 501;">
                    <tr>
                        <td>
                            <table style="width:100%;">
                                <tr>
                                    <td>

        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                    <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Text="Tipo St." Width="60px" ForeColor="Black"></asp:Label>
    
            </span></strong>
                                    </td>
                                    <td>
                            <asp:DropDownList ID="cmbTipo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="500px" AutoPostBack="True">
                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                    <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Text="Descrizione" Width="60px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:TextBox ID="txtDescrizione" runat="server" CssClass="CssMaiuscolo" 
                                        Font-Names="Arial" Font-Size="8pt" MaxLength="50" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        &nbsp;</td>
                                    <td>

                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="text-align: center">
                        <td style="text-align: center">
                            <table style="width: 90%">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:ImageButton ID="Salva" runat="server" 
                                            ImageUrl="~/NuoveImm/Img_Salva.png" />
                                    </td>
                                    <td class="style2">
                                        <img alt="" src="../NuoveImm/Img_Esci.png" style ="cursor :pointer" onclick = "document.getElementById('TextBox1').value ='1';myOpacity.toggle();" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
    </div>
    
            
    </form>
                       <script  language="javascript" type="text/javascript">
                           document.getElementById('dvvvPre').style.visibility = 'hidden';
                           myOpacity = new fx.Opacity('divGuasto', { duration: 200 });
                           if (document.getElementById('TextBox1').value != '2') {
                               myOpacity.hide(); ;
                           }

               </script>

</body>
</html>
