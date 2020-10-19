<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaIntegrazioni.aspx.vb" Inherits="RicercaIntegrazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Integrazioni</title>
    <style type="text/css">
        #dd
        {
            top: 169px;
            left: 9px;
        }
    </style>
</head>
<body bgcolor="#f2f5f1">
<script type="text/javascript">

function SelezionaTutti()
{
var i;
 
  for (i=0; i<<%=NumOperatori%>; i=i+1)   {
  document.getElementById("CheckOperatori_"+i).checked='checked';
  }
}

function DeselezionaTutti()
{
var i;
 
  for (i=0; i<<%=NumOperatori%>; i=i+1)   {
  document.getElementById("CheckOperatori_"+i).checked=false;
  }
}

</script>
    <form id="form1" runat="server" defaultbutton="btnCerca" defaultfocus="cmbGDa">
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 100; left: 537px; position: absolute; top: 500px" 
            TabIndex="9" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 101; left: 404px; position: absolute; top: 500px" 
            TabIndex="8" ToolTip="Avvia Ricerca" />
        &nbsp;
        <table style="left: 0px; background-image: url(NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td style="height: 518px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Integrazioni/Rinnovi</strong></span><br />
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
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" style="z-index: 102; left: 14px; position: absolute; top: 73px">Da</asp:Label>
                                <asp:DropDownList ID="cmbGDa" runat="server" Style="border: 1px solid black; z-index: 103; left: 36px; position: absolute;
                                    top: 70px; right: 1027px;" Font-Names="arial" 
            Font-Size="8pt" TabIndex="1">
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
                                <asp:DropDownList ID="cmbMDa" runat="server" 
            Style="z-index: 104; left: 80px; position: absolute;
                                    top: 70px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" 
            Font-Names="arial" Font-Size="8pt" TabIndex="2">
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
                                <asp:DropDownList ID="cmbAnnoDa" runat="server" 
            Style="z-index: 105; left: 162px; position: absolute;
                                    top: 70px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" 
            Font-Names="arial" Font-Size="8pt" TabIndex="3">
                                    <asp:ListItem>2006</asp:ListItem>
                                    <asp:ListItem>2007</asp:ListItem>
                                    <asp:ListItem>2008</asp:ListItem>
                                    <asp:ListItem>2009</asp:ListItem>
                                    <asp:ListItem>2010</asp:ListItem>
                                    <asp:ListItem>2011</asp:ListItem>
                                    <asp:ListItem>2012</asp:ListItem>
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                    <asp:ListItem>2016</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                    <asp:ListItem>2016</asp:ListItem>
                                    <asp:ListItem>2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                    <asp:ListItem>2020</asp:ListItem>
                                    <asp:ListItem>2021</asp:ListItem>
                                    <asp:ListItem>2022</asp:ListItem>
                                    <asp:ListItem>2023</asp:ListItem>
                                    <asp:ListItem>2024</asp:ListItem>
                                    <asp:ListItem>2025</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" style="z-index: 106; left: 15px; position: absolute; top: 101px">a</asp:Label>
                                <asp:DropDownList ID="cmbGa" runat="server" 
            Style="z-index: 107; left: 36px; position: absolute;
                                    top: 98px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" 
            Font-Names="arial" Font-Size="8pt" TabIndex="4">
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
                                <asp:DropDownList ID="cmbMesea" runat="server" Style="border: 1px solid black; z-index: 108; left: 80px; position: absolute;
                                    top: 98px; right: 946px;" Font-Names="arial" 
            Font-Size="8pt" TabIndex="8">
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
                                <asp:DropDownList ID="cmbAnnoa" runat="server" Style="z-index: 109; left: 162px; position: absolute;
                                    top: 98px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" Font-Names="arial" Font-Size="8pt">
                                    <asp:ListItem>2006</asp:ListItem>
                                    <asp:ListItem>2007</asp:ListItem>
                                    <asp:ListItem>2008</asp:ListItem>
                                    <asp:ListItem>2009</asp:ListItem>
                                    <asp:ListItem>2010</asp:ListItem>
                                    <asp:ListItem>2011</asp:ListItem>
                                    <asp:ListItem>2012</asp:ListItem>
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                    <asp:ListItem>2016</asp:ListItem>
                                    <asp:ListItem>2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                    <asp:ListItem>2020</asp:ListItem>
                                    <asp:ListItem>2021</asp:ListItem>
                                    <asp:ListItem>2022</asp:ListItem>
                                    <asp:ListItem>2023</asp:ListItem>
                                    <asp:ListItem>2024</asp:ListItem>
                                    <asp:ListItem>2025</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 110; left: 247px; position: absolute; top: 62px" Width="148px">Operatore</asp:Label>
                                <asp:Label ID="Label6" runat="server" Font-Bold="False" 
            Font-Names="Arial" Font-Size="8pt"             
            
            Style="z-index: 111; left: 249px; position: absolute; top: 78px;  height: 34px;" 
            Width="380px">Per ottenere la lista delle  integrazioni/rinnovi fatte da operatori diversi da quello attualmente connesso,  indicare gli operatori e la Password di sistema</asp:Label>
            <div id="dd" 
            style="position: absolute; width: 641px; height: 285px; overflow: auto;">
            <asp:CheckBoxList ID="CheckOperatori" runat="server" BackColor="#FFFFC0" BorderColor="Black"
                                    Font-Names="Arial" Font-Size="8pt" RepeatColumns="8" Style="z-index: 112; left: 8px;
                                    position: absolute; top: 0px" Width="200px">
                                </asp:CheckBoxList></div>
                                <asp:Label ID="Pw" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="8pt"
                                    Height="19px" 
            Style="z-index: 113; left: 249px; position: absolute; top: 123px">Pw:</asp:Label>
                                <asp:TextBox ID="txtPw" runat="server" Style="z-index: 114; left: 276px; position: absolute;
                                    top: 120px" TextMode="Password" 
            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
        &nbsp; &nbsp;&nbsp;
        <img src="NuoveImm/Img_SelezionaTutti.png" onclick="SelezionaTutti()" style="z-index: 116; left: 14px; position: absolute;
            top: 500px; cursor: pointer; height: 20px;" alt="Seleziona Tutti" />
        <img src="NuoveImm/Img_DeSelezionaTutti.png" onclick="DeselezionaTutti()" style="z-index: 117; left: 150px; position: absolute;
            top: 500px; cursor: pointer;" alt="Deseleziona Tutti" />
    
    </form>
</body>
</html>
