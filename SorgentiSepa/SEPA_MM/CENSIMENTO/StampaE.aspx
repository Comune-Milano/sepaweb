<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaE.aspx.vb" Inherits="CENSIMENTO_StampaE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Stampa</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
            Style="z-index: 100; left: 216px; position: absolute; top: 8px" Width="240px">DATI RIEPILOGATIVI EDIFICIO</asp:Label>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;&nbsp;
        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 364px; position: absolute; top: 456px" Width="24px">Civico </asp:Label>
        <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 259px; position: absolute; top: 480px" Width="24px">CAP</asp:Label>
        <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 480px" Width="40px">Comune</asp:Label>
        <asp:Label ID="lblcomune" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 72px; position: absolute; top: 480px" Width="179px"></asp:Label>
        <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 504px" Width="1px">Località </asp:Label>
        <asp:Label ID="lblvia" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 456px" Width="330px"></asp:Label>
        <asp:Label ID="lblcivico" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 403px; position: absolute; top: 456px" Width="24px"></asp:Label>
        <asp:Label ID="lblcap" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 291px; position: absolute; top: 480px" Width="64px"></asp:Label>
        <asp:Label ID="lbllocalita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 72px; position: absolute; top: 504px" Width="275px"></asp:Label>
        <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 432px" Width="40px">INDIRIZZO</asp:Label>
        <hr style="left: 80px; width: 504px; position: absolute; top: 440px; height: 1px" />
        &nbsp; &nbsp;
        <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 528px" Width="40px">DIMENSIONI</asp:Label>
        <hr style="left: 88px; width: 496px; position: absolute; top: 536px; height: 1px" />
        <asp:Label ID="lblDimensioni" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="104px" Style="z-index: 100; left: 24px; position: absolute; top: 552px"
            Width="560px"></asp:Label>
        <table style="left: 20px; width: 536px; position: absolute; top: 61px; height: 38px">
            <tr>
                <td style="vertical-align: top; width: 87px; height: 15px; text-align: left">
        <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 64px" Width="54px">Complesso :</asp:Label></td>
                <td style="width: 8px; height: 15px">
        <asp:Label ID="lblcomplesso" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 160px; top: 64px"
            Width="234px"></asp:Label></td>
                <td style="width: 12px; height: 15px">
        <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 448px; top: 64px" Width="56px">Cod. GIMI</asp:Label></td>
                <td style="width: 9px; height: 15px">
        <asp:Label ID="lblgimi" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            Style="z-index: 100; left: 504px; top: 64px; width: 134px;"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 87px; text-align: left">
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 88px" Width="116px">Denominazione Edificio :</asp:Label></td>
                <td style="width: 8px">
        <asp:Label ID="lbldenedificio" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 160px; top: 88px"
            Width="232px"></asp:Label></td>
                <td style="width: 12px">
        <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 448px; top: 88px" Width="32px">Cod.</asp:Label></td>
                <td style="width: 9px">
        <asp:Label ID="LblCod" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            Style="z-index: 100; left: 480px; top: 88px; width: 151px;"></asp:Label></td>
            </tr>
        </table>
        <table style="left: 20px; width: 536px; position: absolute; top: 118px; height: 184px">
            <tr>
                <td style="width: 92px">
        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 120px" Width="56px">Condominio</asp:Label></td>
                <td style="width: 113px">
        <asp:Label ID="lblcondominio" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 160px; top: 120px"
            Width="56px"></asp:Label></td>
                <td style="width: 75px">
        <asp:Label ID="Label32" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 304px; top: 120px" Width="80px">Num. Ascensori</asp:Label></td>
                <td style="width: 68px">
        <asp:Label ID="lblAscensori" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 448px; top: 120px"
            Width="56px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 92px">
        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 144px" Width="88px">Data Costruzione</asp:Label></td>
                <td style="width: 113px">
        <asp:Label ID="lbldatacostr" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 160px; top: 144px"
            Width="104px"></asp:Label></td>
                <td style="width: 75px">
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 304px; top: 144px" Width="48px">P. Terra</asp:Label></td>
                <td style="width: 68px">
        <asp:Label ID="lblpianoterra" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 448px; top: 144px"
            Width="48px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 92px">
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 168px" Width="80px">Data Ristrutt.</asp:Label></td>
                <td style="width: 113px">
        <asp:Label ID="lbldataristrutt" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 160px; top: 168px"
            Width="104px"></asp:Label></td>
                <td style="width: 75px">
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 304px; top: 168px" Width="48px">Seminterrato</asp:Label></td>
                <td style="width: 68px">
        <asp:Label ID="lblseminterrato" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 448px; top: 168px"
            Width="48px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 92px; height: 21px">
        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 192px" Width="88px">Tipologia Edificio</asp:Label>
                </td>
                <td style="width: 113px; height: 21px">
        <asp:Label ID="lbltipoedif" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 160px; top: 192px" Width="104px"></asp:Label></td>
                <td style="width: 75px; height: 21px">
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 304px; top: 192px" Width="48px">Sottotetto</asp:Label></td>
                <td style="width: 68px; height: 21px">
        <asp:Label ID="lblsottOTETTO" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 448px; top: 192px"
            Width="48px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 92px">
        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 216px" Width="88px">Utilizzo Principale</asp:Label></td>
                <td style="width: 113px">
        <asp:Label ID="lblutilprinc" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 160px; top: 216px"
            Width="104px"></asp:Label></td>
                <td style="width: 75px">
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 304px; top: 216px" Width="112px">Num. Piani Fuori Terra</asp:Label></td>
                <td style="width: 68px">
        <asp:Label ID="lblpianfuo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 448px; top: 216px" Width="64px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 92px">
        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 240px" Width="88px">Tipologia Costrutt.</asp:Label></td>
                <td style="width: 113px">
        <asp:Label ID="lbltipocostr" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 160px; top: 240px"
            Width="104px"></asp:Label></td>
                <td style="width: 75px">
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 304px; top: 240px" Width="112px">Num. Piani Entro Terra</asp:Label></td>
                <td style="width: 68px">
        <asp:Label ID="lblentroter" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 448px; top: 240px" Width="64px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 92px">
        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 264px" Width="88px">Livello Possesso</asp:Label></td>
                <td style="width: 113px">
        <asp:Label ID="lbllivposs" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 160px; top: 264px" Width="104px"></asp:Label></td>
                <td style="width: 75px">
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 304px; top: 264px" Width="48px">Attico</asp:Label></td>
                <td style="width: 68px">
        <asp:Label ID="lblattico" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 448px; top: 264px" Width="64px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 92px">
        <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 288px" Width="88px">Imp. Riscald.</asp:Label></td>
                <td style="width: 113px">
        <asp:Label ID="lblimpriscald" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 160px; top: 288px"
            Width="104px"></asp:Label></td>
                <td style="width: 75px">
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 304px; top: 288px" Width="48px">Superattico</asp:Label></td>
                <td style="width: 68px">
        <asp:Label ID="lblsuperatt" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 448px; top: 288px" Width="64px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 92px; height: 18px">
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 312px" Width="80px">Num. Mezzanini</asp:Label></td>
                <td style="width: 113px; height: 18px">
        <asp:Label ID="lblmezzani" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 160px; top: 312px" Width="104px"></asp:Label></td>
                <td style="width: 75px; height: 18px">
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 304px; top: 312px" Width="56px">Num. Scale</asp:Label></td>
                <td style="width: 68px; height: 18px">
        <asp:Label ID="lblscale" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 448px; top: 312px" Width="64px"></asp:Label></td>
            </tr>
        </table>
    
    </div>


    <table style="left: 20px; width: 425px; position: absolute; top: 328px; height: 59px">
        <tr>
            <td style="width: 77px">
        <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="1px" Style="z-index: 100; left: 24px; top: 344px"
            Width="80px">Sintesi Edificio</asp:Label></td>
            <td>
        <asp:TextBox ID="TxtSintesi" runat="server" Height="40px" MaxLength="100" Style="left: 160px; top: 344px" TextMode="MultiLine" Width="435px" ReadOnly="True"></asp:TextBox></td>
        </tr>
    </table>
    <table style="left: 19px; width: 546px; position: absolute; top: 401px">
        <tr>
            <td style="width: 57px">
        <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; top: 400px" Width="40px">Sezione </asp:Label></td>
            <td>
        <asp:Label ID="lblsezione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 80px; top: 400px" Width="40px"></asp:Label></td>
            <td>
        <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 160px; top: 400px" Width="32px">Foglio</asp:Label></td>
            <td>
        <asp:Label ID="lblfoglio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 208px; top: 400px" Width="32px"></asp:Label></td>
            <td style="width: 10px">
        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 280px; top: 400px" Width="32px">Numero :</asp:Label></td>
            <td>
        <asp:Label ID="lblnumero" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 328px; top: 400px" Width="32px"></asp:Label></td>
            <td>
        <asp:Label ID="Label34" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="8px" Style="z-index: 100; left: 400px; top: 400px"
            Width="72px">Cod. Comune </asp:Label></td>
            <td style="width: 6px">
        <asp:Label ID="lblcodcomun" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="8px" Style="z-index: 100; left: 472px; top: 400px"
            Width="64px"></asp:Label></td>
        </tr>
    </table>
        </form>
                <script type="text/javascript">
                self.focus();
            </script>
</body>
</html>
