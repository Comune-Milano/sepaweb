<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoSportello.aspx.vb" Inherits="ANAUT_NuovoSportello" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
<base target="_self"/>
    <title>Sportello</title>
    <style type="text/css">
        .style1
        {
            text-align: center;
            font-size: small;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            color: white;
        }
        .style3
        {
            width: 100px;
        }
        .style4
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            width: 160px;
        }
        .style5
        {
            height: 24px;
        }
        .style6
        {
            width: 138px;
        }
        .style7
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            width: 138px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
    <div>
    
        <table style="width:100%;">
            <tr bgcolor="Maroon">
                <td class="style1">
                    INSERIMENTO SPORTELLO/SEDE <asp:Label ID="Label1" 
                        runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td class="style7">
                                Descrizione</td>
                            <td>
                                <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    TabIndex="1" Width="345px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Indirizzo</td>
                            <td>
                                <asp:TextBox ID="txtIndirizzo" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    TabIndex="2" Width="345px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Civico</td>
                            <td>
                                <asp:TextBox ID="txtCivico" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    TabIndex="3" Width="345px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                C.A.P.</td>
                            <td>
                                <asp:TextBox ID="txtCap" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    TabIndex="4" Width="66px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Comune</td>
                            <td>
                                <asp:TextBox ID="txtComune" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    TabIndex="5" Width="345px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Telefono</td>
                            <td>
                                <asp:TextBox ID="txtTelefono" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    TabIndex="6" Width="345px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Fax</td>
                            <td>
                                <asp:TextBox ID="txtFAX" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    TabIndex="7" Width="345px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                N.Verde</td>
                            <td>
                                <asp:TextBox ID="txtVerde" runat="server" Font-Names="arial" Font-Size="10pt" 
                                    TabIndex="8" Width="345px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Primo App. Mattino</td>
                            <td>
                                <table style="width:100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="style3">
                                <asp:DropDownList ID="cmbInizioM" runat="server" TabIndex="9">
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem Selected="True">08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>--</asp:ListItem>
                                </asp:DropDownList>
                                            :<asp:DropDownList ID="cmbInizioM1" runat="server" TabIndex="10">
                                    <asp:ListItem Selected="True">00</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>--</asp:ListItem>
                                </asp:DropDownList>
                                        </td>
                                        <td class="style4">
                                            Ultimo App. Mattino</td>
                                        <td>
                                <asp:DropDownList ID="cmbFineM" runat="server" TabIndex="11">
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem Selected="True">13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>--</asp:ListItem>
                                </asp:DropDownList>
                                            :<asp:DropDownList ID="cmbFineM1" runat="server" TabIndex="12">
                                    <asp:ListItem Selected="True">00</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>--</asp:ListItem>
                                </asp:DropDownList>
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Primo App. Pomeriggio</td>
                            <td>
                                <table style="width:100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="style3">
                                <asp:DropDownList ID="cmbInizioP" runat="server" TabIndex="13">
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem Selected="True">14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>--</asp:ListItem>
                                </asp:DropDownList>
                                            :<asp:DropDownList ID="cmbInizioP1" runat="server" TabIndex="14">
                                    <asp:ListItem Selected="True">00</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>--</asp:ListItem>
                                </asp:DropDownList>
                                        </td>
                                        <td class="style4">
                                            Ultimo App. Pomeriggio</td>
                                        <td>
                                <asp:DropDownList ID="cmbFineP" runat="server" TabIndex="15">
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem Selected="True">18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>--</asp:ListItem>
                                </asp:DropDownList>
                                            :<asp:DropDownList ID="cmbFineP1" runat="server" TabIndex="16">
                                    <asp:ListItem Selected="True">00</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>--</asp:ListItem>
                                </asp:DropDownList>
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Durata Appuntamento 
                                <br />
                                (minuti)</td>
                            <td>
                                <asp:DropDownList ID="cmbDurata" runat="server" TabIndex="17">
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem Selected="True">20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>60</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Giorni Lavorati</td>
                            <td>
                              <table cellpadding="0" cellspacing="0" style="width:100%;">
                                    <tr>
                                        <td class="style5" bgcolor="#CCFFFF">
                                            LUNEDI&#39;</td>
                                        <td class="style5" bgcolor="#FFFFCC">
                                            MARTEDI&#39;</td>
                                        <td class="style5" bgcolor="#FFFF99">
                                            MERCOLEDI&#39;</td>
                                        <td class="style5" bgcolor="#FFFF66">
                                            GIOVEDI&#39;</td>
                                        <td class="style5" bgcolor="#FFCCCC">
                                            VENERDI&#39;</td>
                                        <td class="style5" bgcolor="#FFCC66">
                                            SABATO</td>
                                        <td class="style5" bgcolor="#CCFFCC">
                                            DOMENICA</td>
                                    </tr>
                                    <tr>
                                        <td class="style5" bgcolor="#CCFFFF">
                                <asp:CheckBox ID="Ch1" runat="server" Checked="True" TabIndex="18" 
                                    Text="Mattino" />
                                        </td>
                                        <td class="style5" bgcolor="#FFFFCC">
                                <asp:CheckBox ID="Ch2" runat="server" Checked="True" TabIndex="19" 
                                    Text="Mattino" />
                                        </td>
                                        <td class="style5" bgcolor="#FFFF99">
                                <asp:CheckBox ID="Ch3" runat="server" Checked="True" TabIndex="20" 
                                    Text="Mattino" />
                                        </td>
                                        <td class="style5" bgcolor="#FFFF66">
                                <asp:CheckBox ID="Ch4" runat="server" Checked="True" TabIndex="21" 
                                    Text="Mattino" />
                                        </td>
                                        <td class="style5" bgcolor="#FFCCCC">
                                <asp:CheckBox ID="Ch5" runat="server" Checked="True" TabIndex="22" 
                                    Text="Mattino" />
                                        </td>
                                        <td class="style5" bgcolor="#FFCC66">
                                <asp:CheckBox ID="Ch6" runat="server" TabIndex="23" Text="Mattino" />
                                        </td>
                                        <td class="style5" bgcolor="#CCFFCC">
                                <asp:CheckBox ID="Ch7" runat="server" TabIndex="24" Text="Mattino" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor="#CCFFFF">
                                <asp:CheckBox ID="Ch1_P" runat="server" Checked="True" TabIndex="18" 
                                    Text="Pomeriggio" />
                                        </td>
                                        <td bgcolor="#FFFFCC">
                                <asp:CheckBox ID="Ch2_P" runat="server" Checked="True" TabIndex="19" 
                                    Text="Pomeriggio" />
                                        </td>
                                        <td bgcolor="#FFFF99">
                                <asp:CheckBox ID="Ch3_P" runat="server" Checked="True" TabIndex="20" 
                                    Text="Pomeriggio" />
                                        </td>
                                        <td bgcolor="#FFFF66">
                                <asp:CheckBox ID="Ch4_P" runat="server" Checked="True" TabIndex="21" 
                                    Text="Pomeriggio" />
                                        </td>
                                        <td bgcolor="#FFCCCC">
                                <asp:CheckBox ID="Ch5_P" runat="server" Checked="True" TabIndex="22" 
                                    Text="Pomeriggio" />
                                        </td>
                                        <td bgcolor="#FFCC66">
                                <asp:CheckBox ID="Ch6_P" runat="server" TabIndex="23" Text="Pomeriggio" />
                                        </td>
                                        <td bgcolor="#CCFFCC">
                                <asp:CheckBox ID="Ch7_P" runat="server" TabIndex="24" Text="Pomeriggio" />
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Carica Patrimonio 
                                <br />
                                di Default</td>
                            <td>
                                <asp:CheckBox ID="Ch8" runat="server" TabIndex="23" 
                                    
                                    Text="(Saranno aggiunte le unità associate di default  a questa Sede Territoriale ma non ancora associate a nessuno sportello/sede" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style6">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lblErrore" runat="server" Visible="False"></asp:Label>
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    &nbsp;
                    <asp:ImageButton ID="btnSalva" runat="server" 
                        ImageUrl="~/NuoveImm/Img_SalvaVal.png" TabIndex="14" 
                        style="height: 16px" />&nbsp; &nbsp;<img id="btnAnnulla" alt="" src="../NuoveImm/Img_AnnullaVal.png" onclick="self.close();" style="cursor:pointer"/>
                    </td>
            </tr>
        </table>
    <asp:HiddenField ID="idf" runat="server" />
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
        </ContentTemplate>
        </asp:UpdatePanel>

                        <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
<div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt;
        font-family: Arial">
    </div>
    </form>
</body>
</html>
