<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneTabelleBando.aspx.vb" Inherits="ANAUT_GestioneTabelleBando" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestione Bando</title>
    <style type="text/css">
        .style2
        {
            width: 240px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td bgcolor="#990000" style="text-align: center" >
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Overline="False" Font-Size="12pt" ForeColor="White" 
                        Text="PARAMETRI ANAGRAFE UTENZA"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt" Text="TABELLE DI COLLOCAZIONE AREA E CLASSE"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 700px;">
                        <tr>
                            <td>
                                <asp:Label ID="Label209" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" 
                                    Text="Aumenta in automatico tutti i valori ISEE rispetto alla AU precedente del seguente valore % :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAumento" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                    <asp:ImageButton ID="btnApplica" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Applica.png" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="9pt" ForeColor="Maroon" Text="PROTEZIONE:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:850px;">
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="A1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="0"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox4" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="A2"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="A1+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label19" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label23" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox7" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label27" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox8" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label31" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox9" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="A3"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label16" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="A2+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label20" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox10" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label24" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox11" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label28" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox12" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label32" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox13" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="A4"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label17" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="A3+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label21" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox14" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label25" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox15" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label29" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox16" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label33" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox17" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="A5"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label18" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label38" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="A4+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label22" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox18" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label26" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox19" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label30" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox20" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label34" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox21" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label39" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="9pt" ForeColor="Maroon" Text="ACCESSO:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:850px;">
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label40" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="B1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label41" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label42" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="A5+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label43" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox22" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label44" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox23" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label45" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox24" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label46" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox25" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label47" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="B2"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label48" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label49" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="B1+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label50" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox26" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label51" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox27" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label52" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox28" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label53" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox29" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label54" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="B3"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label55" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label56" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="B2+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label57" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox30" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label58" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox31" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label59" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox32" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label60" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox33" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label61" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="B4"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label62" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label63" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="B3+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label64" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox34" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label65" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox35" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label66" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox36" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label67" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox37" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label68" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="B5"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label69" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label70" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="B4+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label71" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox38" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label72" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox39" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label73" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox40" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label74" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox41" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label75" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="9pt" ForeColor="Maroon" Text="PERMANENZA:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:850px;">
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label76" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label77" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label78" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="B5+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label79" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox42" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label80" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox43" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label81" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox44" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label82" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox45" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label83" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C2"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label84" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label85" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C1+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label86" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox46" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label87" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox47" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label88" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox48" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label89" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox49" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label90" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C3"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label91" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label92" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C2+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label93" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox50" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label94" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox51" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label95" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox52" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label96" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox53" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label97" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C4"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label98" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label99" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C3+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label100" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox54" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label101" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox55" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label102" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox56" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label103" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox57" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label104" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C5"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label105" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label106" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C4+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label107" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox58" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label108" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox59" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label109" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox60" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label110" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox61" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label111" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C6"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label112" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label113" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C5+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label114" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox62" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label115" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox63" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label116" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox64" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label117" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox65" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label118" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C7"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label119" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label120" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C6+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label121" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox66" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label122" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox67" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label123" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox68" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label124" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox69" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label125" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C8"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label126" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label127" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C7+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label128" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox70" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label129" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox71" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label130" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox72" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label131" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox73" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label132" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C9"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label133" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label134" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C8+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label135" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox74" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label136" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox75" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label137" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox76" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label138" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox77" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label139" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C10"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label140" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label141" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C9+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label142" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox78" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label143" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox79" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label144" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox80" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label145" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox81" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label146" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C11"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label147" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label148" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C10+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label149" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox82" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label150" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox83" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label151" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox84" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label152" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox85" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label153" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C12"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label154" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label155" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C11+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label156" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox86" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label157" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox87" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label158" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox88" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label159" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox89" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label160" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="9pt" ForeColor="Maroon" Text="DECADENZA:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:850px;">
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label161" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="D1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label162" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label163" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="C12+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label164" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox90" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label165" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox91" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label166" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox92" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label167" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox93" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label168" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="D2"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label169" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label170" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="D1+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label171" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox94" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label172" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox95" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label173" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox96" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label174" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox97" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#CCCCCC">
                            <td>
                                <asp:Label ID="Label175" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="D3"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label176" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label177" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="D2+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label178" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox98" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px" Enabled="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label179" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox99" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label180" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox100" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label181" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox101" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label182" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="D4"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label183" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Da:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label184" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="D3+1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label185" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="a:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label189" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="Infinito"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label186" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Valore Locativo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox103" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label187" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="% Inc. Max ISE erp:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox104" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="42px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label188" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Text="Canone Minimo:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox105" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label190" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt" Text="ALTRI VALORI NECESSARI PER IL CALCOLO DEL CANONE"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 749px;">
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label191" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="Limite 2 pensioni INPS, minima + sociale:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPensione" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp; &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label193" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="% ISTAT APPLICATA AL CANONE CLASSE:"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label194" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="PRIMO ANNO"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label ID="Label195" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="SECONDO ANNO (solo se AU biennale)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;</td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                <asp:Label ID="Label196" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="Protezione:"></asp:Label>
                                        </td>
                                        <td>
                                <asp:TextBox ID="txtISTAT_1_PR" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label197" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="Accesso:"></asp:Label>
                                        </td>
                                        <td>
                                <asp:TextBox ID="txtISTAT_1_AC" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label198" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="Permanenza:"></asp:Label>
                                        </td>
                                        <td>
                                <asp:TextBox ID="txtISTAT_1_PE" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label199" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="Decadenza:"></asp:Label>
                                        </td>
                                        <td>
                                <asp:TextBox ID="txtISTAT_1_DE" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                <asp:Label ID="Label200" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="Protezione:"></asp:Label>
                                        </td>
                                        <td>
                                <asp:TextBox ID="txtISTAT_2_PR" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label201" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="Accesso:"></asp:Label>
                                        </td>
                                        <td>
                                <asp:TextBox ID="txtISTAT_2_AC" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label202" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="Permanenza:"></asp:Label>
                                        </td>
                                        <td>
                                <asp:TextBox ID="txtISTAT_2_PE" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="Label203" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="Decadenza:"></asp:Label>
                                        </td>
                                        <td>
                                <asp:TextBox ID="txtISTAT_2_DE" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="60px"></asp:TextBox>
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
                                &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                                <asp:Label ID="Label204" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="LIMITI VALORI ICI/IMU ALLOGGI:"></asp:Label>
                            </td>
            </tr>
            <tr>
                <td>
                    <table style="width:503px;">
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label205" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="1-2 Persone:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtICI_1_2" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label206" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="3-4 Persone:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtICI_3_4" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label207" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="5-6 Persone:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtICI_5_6" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label208" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="8pt" Text="7 o più Persone:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtICI_7" runat="server" Font-Names="arial" Font-Size="8pt" 
                                    Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:ImageButton ID="btnSalva" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SalvaGrande.png" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img id="btnesci" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" onclick="self.close();" style="cursor:pointer" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
