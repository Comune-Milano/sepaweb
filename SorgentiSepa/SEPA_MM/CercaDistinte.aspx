<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CercaDistinte.aspx.vb" Inherits="CercaDistinte" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ricerca Distinte</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" align="center" bgcolor="#ffffff" border="1" bordercolor="#000000"
            cellpadding="1" cellspacing="1" style="z-index: 100; left: 0px; position: static;
            top: 0px" width="90%">
            <tr>
                <td align="middle" bgcolor="floralwhite" style="height: 23px; text-align: left" valign="bottom">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        ForeColor="Navy">Ricerca Distinte</asp:Label></td>
            </tr>
            <tr>
                <td align="middle" bgcolor="floralwhite" style="height: 88px; text-align: left" valign="bottom">
                    <table id="Table2" align="center" bgcolor="floralwhite" border="0" bordercolor="#000000"
                        cellpadding="1" cellspacing="1" style="height: 75px" width="90%">
                        <tr>
                            <td style="width: 201px; height: 18px; text-align: left">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small">Da</asp:Label></td>
                            <td style="width: 193px; height: 18px; text-align: left">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small">a</asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 201px; height: 52px; text-align: center">
                                <asp:DropDownList ID="cmbGDa" runat="server" Style="z-index: 100; left: 0px; position: static;
                                    top: 0px">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
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
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                    <asp:ListItem>24</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>26</asp:ListItem>
                                    <asp:ListItem>27</asp:ListItem>
                                    <asp:ListItem>28</asp:ListItem>
                                    <asp:ListItem>29</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>31</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="cmbMDa" runat="server" Style="z-index: 100; left: 0px; position: static;
                                    top: 0px">
                                    <asp:ListItem Value="01">Gennaio</asp:ListItem>
                                    <asp:ListItem Value="02">Febbraio</asp:ListItem>
                                    <asp:ListItem Value="03">Marzo</asp:ListItem>
                                    <asp:ListItem Value="04">Aprile</asp:ListItem>
                                    <asp:ListItem Value="05">Maggio</asp:ListItem>
                                    <asp:ListItem Value="06">Giugno</asp:ListItem>
                                    <asp:ListItem Value="07">Luglio</asp:ListItem>
                                    <asp:ListItem Value="08">Agosto</asp:ListItem>
                                    <asp:ListItem Value="09">Settembre</asp:ListItem>
                                    <asp:ListItem Value="10">Ottobre</asp:ListItem>
                                    <asp:ListItem Value="11">Novembre</asp:ListItem>
                                    <asp:ListItem Value="12">Dicembre</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="cmbAnnoDa" runat="server" Style="z-index: 100; left: 0px; position: static;
                                    top: 0px">
                                </asp:DropDownList></td>
                            <td style="width: 193px; height: 52px; text-align: center">
                                <asp:DropDownList ID="cmbGa" runat="server" Style="z-index: 100; left: 0px; position: static;
                                    top: 0px">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
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
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                    <asp:ListItem>24</asp:ListItem>
                                    <asp:ListItem>25</asp:ListItem>
                                    <asp:ListItem>26</asp:ListItem>
                                    <asp:ListItem>27</asp:ListItem>
                                    <asp:ListItem>28</asp:ListItem>
                                    <asp:ListItem>29</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>31</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="cmbMesea" runat="server" Style="z-index: 100; left: 0px; position: static;
                                    top: 0px">
                                    <asp:ListItem Value="01">Gennaio</asp:ListItem>
                                    <asp:ListItem Value="02">Febbraio</asp:ListItem>
                                    <asp:ListItem Value="03">Marzo</asp:ListItem>
                                    <asp:ListItem Value="04">Aprile</asp:ListItem>
                                    <asp:ListItem Value="05">Maggio</asp:ListItem>
                                    <asp:ListItem Value="06">Giugno</asp:ListItem>
                                    <asp:ListItem Value="07">Luglio</asp:ListItem>
                                    <asp:ListItem Value="08">Agosto</asp:ListItem>
                                    <asp:ListItem Value="09">Settembre</asp:ListItem>
                                    <asp:ListItem Value="10">Ottobre</asp:ListItem>
                                    <asp:ListItem Value="11">Novembre</asp:ListItem>
                                    <asp:ListItem Value="12">Dicembre</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="cmbAnnoa" runat="server" Style="z-index: 100; left: 0px; position: static;
                                    top: 0px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 201px; height: 16px; text-align: left">
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                                    Style="z-index: 100; left: 0px; position: static; top: 0px" Width="62px">N° Distinta</asp:Label></td>
                            <td style="width: 193px; height: 16px; text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 201px; height: 52px; text-align: center">
                                <asp:TextBox ID="txtDistinta" runat="server" Style="z-index: 100; left: 0px; position: static;
                                    top: 0px" Width="178px"></asp:TextBox></td>
                            <td style="width: 193px; height: 52px; text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 201px; height: 14px; text-align: left">
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                                    Style="z-index: 100; left: 0px; position: relative; top: 0px" Width="148px">Lista Operatori</asp:Label></td>
                            <td style="width: 193px; height: 14px; text-align: center">
                            </td>
                        </tr>
                    </table>
                    <table id="Table3" align="center" bgcolor="floralwhite" border="0" bordercolor="#000000"
                        cellpadding="1" cellspacing="1" style="z-index: 100; left: 0px; position: static;
                        top: 0px; height: 75px" width="90%">
                        <tr>
                            <td style="width: 201px; height: 52px; text-align: left">
                                <asp:DropDownList ID="cmbStato" runat="server" AutoPostBack="True" Height="20px"
                                    Style="z-index: 100; left: 0px; position: static; top: 0px" TabIndex="5" 
                                    Width="358px">
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="middle" bgcolor="floralwhite" style="height: 39px; text-align: center"
                    valign="bottom">
                    <asp:Button ID="btnVisualizza" runat="server" CausesValidation="False" Height="28px"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Text="Visualizza" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnChiudi" runat="server" CausesValidation="False" Height="28px"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Text="Chiudi" />&nbsp;</td>
            </tr>
            <tr>
                <td align="center" valign="center">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small">Inserire un intervallo di date valido</asp:Label></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
