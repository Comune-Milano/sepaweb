<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaUI.aspx.vb" Inherits="CENSIMENTO_StampaUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Stampa</title>
    <style type="text/css">
        .style2
        {
            width: 119px;
        }
        .style5
        {
            width: 94px;
        }
        .style6
        {
            width: 73px;
        }
        .style7
        {
            width: 90px;
        }
        .style8
        {
            width: 71px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="LBLTESTO" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
            Style="z-index: 100; left: 216px; position: absolute; top: 8px" Width="336px">DATI RIEPILOGATIVI UNITA' IMMOBILIARE</asp:Label>
        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 216px" Width="96px">DATI CATASTALI</asp:Label>
        <hr style="left: 104px; width: 552px; position: absolute; top: 224px; height: 1px" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label42" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 8px; position: absolute; top: 491px" Width="40px">DIMENSIONI</asp:Label>
        <hr style="left: 72px; width: 576px; position: absolute; top: 499px; height: 1px" />
        <asp:Label ID="lblDimensioni" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Height="104px" Style="z-index: 100; left: 32px; position: absolute;
            top: 511px" Width="560px"></asp:Label>
        &nbsp;
        <table style="left: 32px; width: 613px; position: absolute; top: 50px; height: 53px">
            <tr>
                <td style="width: 313px; height: 15px">
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 64px">Complesso</asp:Label></td>
                <td style="width: 3px; height: 15px">
        <asp:Label ID="lblcomplesso" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 136px; top: 64px"
            Width="504px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 313px; height: 24px">
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 88px" Width="48px">Edificio</asp:Label></td>
                <td style="width: 3px; height: 24px">
        <asp:Label ID="lbledificio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 136px; top: 88px" Width="504px"></asp:Label></td>
            </tr>
        </table>
        <table style="left: 31px; width: 567px; position: absolute; top: 106px; height: 94px">
            <tr>
                <td style="width: 82px">
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 120px" Width="80px">Tipologia Unità</asp:Label></td>
                <td style="width: 195px">
        <asp:Label ID="lbltipounita" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 136px; top: 120px"
            Width="200px"></asp:Label></td>
                <td style="width: 5px">
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 368px; top: 120px" Width="64px">Scala</asp:Label></td>
                <td style="width: 5px">
        <asp:Label ID="lblscala" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 440px; top: 120px" Width="175px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 82px">
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 144px" Width="88px">Tipo Livello piano</asp:Label></td>
                <td style="width: 195px">
        <asp:Label ID="lbllivellopiano" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 136px; top: 144px"
            Width="200px"></asp:Label></td>
                <td style="width: 5px">
        <asp:Label ID="Label38" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 368px; top: 144px" Width="64px">Interno</asp:Label></td>
                <td style="width: 5px">
        <asp:Label ID="lblinterno" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 440px; top: 144px" Width="176px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 82px">
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 168px" Width="96px">Stato Conservativo</asp:Label></td>
                <td style="width: 195px">
        <asp:Label ID="lblstatoconserv" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 136px; top: 168px; height: 14px;"
            Width="200px"></asp:Label></td>
                <td style="width: 5px">
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 368px; top: 168px" Width="64px">Disponibilità</asp:Label></td>
                <td style="width: 5px">
        <asp:Label ID="lbldisponib" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            Style="z-index: 100; left: 440px; top: 168px;" Width="181px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 82px">
        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 33px; top: 192px" 
            Width="88px">Stato Censimento</asp:Label></td>
                <td style="width: 195px">
        <asp:Label ID="lblstatocensim" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 136px; top: 191px"
            Width="200px"></asp:Label></td>
                <td style="width: 5px">
        <asp:Label ID="Label41" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 368px; top: 189px" 
            Width="32px">Cod.</asp:Label></td>
                <td style="width: 5px">
        <asp:Label ID="LblCod" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 440px; top: 188px" 
            Width="112px"></asp:Label></td>
            </tr>
        </table>
        <table style="left: 31px; width: 629px; position: absolute; top: 233px">
            <tr>
                <td style="width: 63px; height: 21px">
        <asp:Label ID="Label40" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 240px" Width="32px">Sezione</asp:Label></td>
                <td style="width: 65px; height: 21px">
        <asp:Label ID="lblsezione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 80px; top: 240px" Width="32px"></asp:Label></td>
                <td style="width: 45px; height: 21px">
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 168px; top: 240px" Width="32px">Foglio</asp:Label></td>
                <td style="width: 51px; height: 21px">
        <asp:Label ID="lblfoglio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 208px; top: 240px" Width="32px"></asp:Label></td>
                <td style="width: 4px; height: 21px">
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 264px; top: 240px" Width="40px">Numero</asp:Label></td>
                <td style="width: 68px; height: 21px">
        <asp:Label ID="lblnumero" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 312px; top: 240px" Width="40px"></asp:Label></td>
                <td style="width: 6px; height: 21px">
        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="16px" Style="z-index: 100; left: 376px; top: 240px"
            Width="24px">Sub</asp:Label></td>
                <td style="width: 6px; height: 21px">
        <asp:Label ID="lblsub" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="16px" Style="z-index: 100; left: 408px; top: 240px"
            Width="24px"></asp:Label></td>
                <td style="width: 6px; height: 21px">
        <asp:Label ID="Label34" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="8px" Style="z-index: 100; left: 456px; top: 240px"
            Width="64px">Cod. Comune</asp:Label></td>
                <td style="width: 6px; height: 21px">
        <asp:Label ID="lblcodcomu" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="8px" Style="z-index: 100; left: 544px; top: 240px"
            Width="64px"></asp:Label></td>
            </tr>
        </table>
        <table style="left: 31px; width: 631px; position: absolute; top: 265px; height: 169px">
            <tr>
                <td class="style7">
        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 264px" Width="64px">Tipologia Cat.</asp:Label></td>
                <td class="style2">
        <asp:Label ID="lbltipocat" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 120px; top: 264px" Width="120px" Height="16px"></asp:Label></td>
                <td class="style8">
        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 264px; top: 264px" Width="64px">Sup. Mq</asp:Label></td>
                <td class="style6">
        <asp:Label ID="lblmq" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 368px; top: 264px" Width="64px"></asp:Label></td>
                <td class="style5">
        <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 593px; top: 664px" Width="72px">Sup. Catastale</asp:Label></td>
                <td>
        <asp:Label ID="lblsupcat" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 584px; position: static; top: 264px" Width="72px"></asp:Label></td>
            </tr>
            <tr>
                <td class="style7">
        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 288px" Width="40px">Rendita</asp:Label></td>
                <td class="style2">
        <asp:Label ID="lblrendita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 120px; top: 288px" Width="40px"></asp:Label></td>
                <td class="style8">
        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 264px; top: 288px" Width="64px">Cubatura</asp:Label></td>
                <td class="style6">
        <asp:Label ID="lblcubatura" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 368px; top: 288px" Width="64px"></asp:Label></td>
                <td class="style5">
        <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 496px; top: 288px" Width="80px">Rendita Storica</asp:Label></td>
                <td>
        <asp:Label ID="lblrenditastor" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 584px; top: 288px"
            Width="80px"></asp:Label></td>
            </tr>
            <tr>
                <td class="style7">
        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 312px" Width="40px">Categoria</asp:Label></td>
                <td class="style2">
        <asp:Label ID="lblcategoria" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 120px; top: 313px"
            Width="112px" Height="16px"></asp:Label></td>
                <td class="style8">
        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 264px; top: 312px" Width="64px">Vani</asp:Label></td>
                <td class="style6">
        <asp:Label ID="lblvani" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 368px; top: 312px" Width="64px"></asp:Label></td>
                <td class="style5">
        <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 496px; top: 312px" Width="80px">Immobile Storico</asp:Label></td>
                <td>
        <asp:Label ID="lblimmobstorico" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 584px; top: 312px"
            Width="80px"></asp:Label></td>
            </tr>
            <tr>
                <td class="style7">
        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; position: static; top: 361px" Width="40px">Classe</asp:Label></td>
                <td class="style2">
        <asp:Label ID="lblclasse" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 120px; top: 361px" Width="81px"></asp:Label></td>
                <td class="style8">
        <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 264px; top: 361px" Width="88px">Valore Imponibile</asp:Label></td>
                <td class="style6">
        <asp:Label ID="lblvalimpo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 368px; top: 361px" Width="64px"></asp:Label></td>
                <td class="style5">
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 496px; top: 361px" Width="80px">Esente ICI</asp:Label></td>
                <td>
        <asp:Label ID="lblesenteici" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 584px; top: 361px"
            Width="80px"></asp:Label></td>
            </tr>
            <tr>
                <td class="style7">
        <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 385px" Width="40px">Inagibile</asp:Label></td>
                <td class="style2">
        <asp:Label ID="lblinagibile" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 120px; top: 385px"
            Width="67px"></asp:Label></td>
                <td class="style8">
        <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 264px; top: 385px" Width="88px">Reddito Agr.</asp:Label></td>
                <td class="style6">
        <asp:Label ID="lblreddagrar" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 368px; top: 385px"
            Width="64px"></asp:Label></td>
                <td class="style5">
        <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 496px; top: 385px" Width="80px">Reddito Dom.</asp:Label></td>
                <td>
        <asp:Label ID="lblreddomin" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 584px; top: 385px" Width="80px"></asp:Label></td>
            </tr>
            <tr>
                <td class="style7">
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 409px" Width="64px">Ditta</asp:Label></td>
                <td class="style2">
        <asp:Label ID="lblditta" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 120px; top: 409px" Width="64px"></asp:Label></td>
                <td class="style8">
        <asp:Label ID="Label35" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 264px; top: 409px" Width="72px">Micro. Cens.</asp:Label></td>
                <td class="style6">
        <asp:Label ID="lblmicrocens" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 368px; top: 409px"
            Width="72px"></asp:Label></td>
                <td class="style5">
        <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 496px; top: 409px" Width="64px">Val. Bilancio</asp:Label></td>
                <td>
        <asp:Label ID="lblvalbil" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 584px; top: 409px" Width="64px"></asp:Label></td>
            </tr>
            <tr>
                <td class="style7">
        <asp:Label ID="Label32" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 433px" Width="64px">Num. Partita</asp:Label></td>
                <td class="style2">
        <asp:Label ID="lblpartita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 120px; top: 433px" Width="64px"></asp:Label></td>
                <td class="style8">
        <asp:Label ID="lblmic" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 264px; top: 433px" Width="88px">Zona Censuaria</asp:Label></td>
                <td class="style6">
        <asp:Label ID="lblzonacens" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 368px; top: 433px" Width="72px"></asp:Label></td>
                <td class="style5">
        <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 496px; top: 433px" Width="88px">Data Fine Validità</asp:Label></td>
                <td>
        <asp:Label ID="lblfineval" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 584px; top: 433px" Width="88px"></asp:Label></td>
            </tr>
            <tr>
                <td class="style7">
        <asp:Label ID="Label33" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 457px" Width="64px">Possesso %</asp:Label></td>
                <td class="style2">
        <asp:Label ID="lblpercposs" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 120px; top: 457px" Width="64px"></asp:Label></td>
                <td class="style8">
        <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 264px; top: 457px" Width="88px">Data Acquisizione</asp:Label></td>
                <td class="style6">
        <asp:Label ID="lbldataacquisi" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 368px; top: 457px"
            Width="72px"></asp:Label></td>
                <td class="style5">
        <asp:Label ID="Label37" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 496px; top: 457px" Width="80px">Stato Catastale</asp:Label></td>
                <td>
        <asp:Label ID="lblstatocatast" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 584px; top: 457px"
            Width="98%"></asp:Label></td>
            </tr>
        </table>
        <table style="left: 31px; width: 557px; position: absolute; top: 455px; height: 25px">
            <tr>
                <td style="width: 51px">
        <asp:Label ID="Label39" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 32px; top: 481px" Width="64px">Note</asp:Label></td>
                <td style="width: 11px">
        <asp:Label ID="lblnote" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 120px; top: 481px" Width="450px"></asp:Label></td>
            </tr>
        </table>
    
    </div>
    </form>
                <script type="text/javascript">
                self.focus();
            </script>
</body>
</html>
