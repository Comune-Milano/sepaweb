<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Compilazione domanda On-Line</title>
    <meta name="robots" content="noindex,nofollow"/>
</head>
<body style="background-attachment: scroll; background-image: url(Immagini/Sfondo.gif); background-repeat: repeat-x" bgcolor="#ececec">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: center; width: 7%; height: 20px;">
                    <img src="Immagini/Milano.gif" alt="Milano"/></td>
                <td style="border-left: #cc0000 1px solid; border-bottom: #cc0000 1px solid; text-align: center; width: 75%; height: 20px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center; border-right: #cc0000 1px solid; border-top: #cc0000 1px solid; width: 7%;" valign="top">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center">
                                <img src="Immagini/LogoComune.gif" alt="Logo Comune"/></td>
                        </tr>
                        <tr>
                            <td>
                                <span style="font-size: 10pt; font-family: Arial"></span></td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="width: 175px; background-color: #dadada">
                        <tr>
                            <td style="width: 708px; height: 19px">
                                <span style="font-size: 11pt; color: #cc0000; font-family: Arial"></span>
                            </td>
                            <td background="Immagini/TabAltoDx.gif" style="font-size: 12pt; width: 80px; font-family: Times New Roman;
                                height: 19px">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px">
                                &nbsp;<strong><span style="font-size: 11pt; color: #cc0000; font-family: Arial">Link
                                    Utili</span></strong></td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px; text-align: left">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px; text-align: left">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px; text-align: left">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px; text-align: left; height: 19px;">
                                &nbsp;</td>
                            <td style="width: 95px; height: 19px;">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px; text-align: left">
                                &nbsp;</td>
                            <td style="width: 95px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px; height: 19px; text-align: left;">
                                &nbsp;&nbsp;
                            </td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 50px; height: 19px;">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px">
                                &nbsp;</td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 50px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px">
                                &nbsp;</td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 50px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px">
                                &nbsp;</td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 50px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px; height: 19px">
                                &nbsp;</td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 50px; height: 19px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 708px; height: 19px">
                                &nbsp;</td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 50px; height: 19px">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 75%" align="left">
                    <table width="100%">
                        <tr>
                            <td style="height: 38px; text-align: left; background-image: url(Immagini/BarraSfondo.gif);" valign="top">
                                                <asp:Image ID="Image18" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Richiedente.gif" />
                                <asp:Image ID="Image19" runat="server" ImageUrl="~/AutoCompilazione/Immagini/ReddRichiedente.gif" />
                                <asp:Image ID="Image20" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Componente2.gif" Visible="False" />
                                <asp:Image ID="Image21" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Componente3.gif" Visible="False" />
                                <asp:Image ID="Image22" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Componente4.gif" Visible="False" />
                                <asp:Image ID="Image23" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Componente5.gif" Visible="False" />
                                <asp:Image ID="Image24" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Componente6.gif" Visible="False" />
                                <asp:Image ID="Image25" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Componente7.gif" Visible="False" />
                                <asp:Image ID="Image26" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Componente8.gif" Visible="False" />
                                <asp:Image ID="Image27" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Componente9.gif" Visible="False" />
                                <asp:Image ID="Image28" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Componente10.gif" Visible="False" />
                                <asp:Image ID="Image29" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Familiari.gif" />
                                <asp:Image ID="Image30" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Abitative1.gif" />
                                <asp:Image ID="Image31" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Abitative2.gif" />
                                <asp:Image ID="Image32" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Requisiti.gif" />
                                <asp:Image ID="Image33" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Convalida.gif" />
                                <asp:Image ID="Image34" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Spedizione.gif" /></td>
                        </tr>
                        <tr>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td style="background-image: url(Immagini/BarraSfondo.gif)">
                                <strong><span style="font-size: 10pt; font-family: Arial"></span></strong>
                            </td>
                        </tr>
                    </table>
                    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" Height="700px" Style="position: static"
                        Width="850px" FinishCompleteButtonImageUrl="Immagini/Memorizza.jpg" FinishCompleteButtonType="Image" 
                        StepPreviousButtonImageUrl="Immagini/Indietro.jpg" StepPreviousButtonType="Image" 
                        StepNextButtonImageUrl="Immagini/Procedi.jpg" StepNextButtonType="Image" 
                        StartNextButtonImageUrl="Immagini/Procedi.jpg" StartNextButtonType="Image"
                        OnNextButtonClick="SkipStep" OnActiveStepChanged="GetFavoriteNumerOnActiveStepIndex" 
                        FinishPreviousButtonImageUrl="Immagini/Indietro.jpg" FinishPreviousButtonType="Image" 
                        StartNextButtonText="Prosegui" StepNextButtonText="Prosegui" StepPreviousButtonText="Indietro" TabIndex="90" DisplaySideBar="False" DisplayCancelButton="false">
                        <WizardSteps>
                            <asp:WizardStep runat="server" Title="Richiedente" StepType="Start">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">ANAGRAFICA</span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Cognome</span></td>
                                        <td style="width: 213px">
                                            <asp:TextBox ID="txtCognome" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Width="203px" Enabled="False" TabIndex="1"></asp:TextBox>
                                        </td>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Nome</span></td>
                                        <td style="width: 211px">
                                            <asp:TextBox ID="txtNome" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Width="203px" Enabled="False" TabIndex="2"></asp:TextBox>
                                        </td>
                                        <td style="width: 49px">
                                            <span style="font-size: 10pt; font-family: Arial"></span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbSesso" runat="server" Font-Names="arial" Font-Size="10pt" Enabled="False" Visible="False" TabIndex="3">
                                                <asp:ListItem Selected="True" Value="M">Maschio</asp:ListItem>
                                                <asp:ListItem Value="F">Femmina</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice Fiscale</span></td>
                                        <td style="width: 213px">
                                            <asp:TextBox ID="txtCF" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                Enabled="False" MaxLength="16" TabIndex="4" Width="203px"></asp:TextBox>
                                        </td>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Nato a</span></td>
                                        <td style="width: 211px">
                                            <asp:TextBox ID="txtNato" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Enabled="False" TabIndex="5" Width="203px"></asp:TextBox>
                                        </td>
                                        <td style="width: 49px">
                                            <span style="font-size: 10pt; font-family: Arial">Nato il</span></td>
                                        <td>
                                            <asp:TextBox ID="txtDataNascita" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" TabIndex="6" Width="77px"></asp:TextBox>
                                            &nbsp;
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataNascita"
                                                Display="Dynamic" ErrorMessage="gg/mm/aaaa" Font-Names="arial" Font-Size="8pt"
                                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" TabIndex="300"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">% Invalidità</span></td>
                                        <td style="width: 213px">
                                            &nbsp;<asp:DropDownList ID="cmbInvalidit&#224;" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="7">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                            <img border="0" src="Immagini/Aiuto.gif" alt ="help1" id="IMG1" onclick="return IMG1_onclick()" style="cursor: pointer" /></td>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Indennità Acc.</span>&nbsp;</td>
                                        <td style="width: 211px" valign="middle">
                                            <span style="font-size: 10pt; font-family: Arial"></span>
                                            <asp:DropDownList ID="cmbAccompagnamento" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="68px" TabIndex="8">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblAvvIndennita" runat="server" Font-Names="arial" Font-Size="8pt"
                                                ForeColor="Red" Text="Solo se Inv.=100%" Visible="False" TabIndex="301"></asp:Label>
                                        </td>
                                        <td style="width: 49px">
                                            <span style="font-size: 10pt; font-family: Arial">A.S.L.</span></td>
                                        <td>
                                            <asp:TextBox ID="txtASL" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="5" TabIndex="9" Width="77px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 305px; height: 21px">
                                            <span style="font-size: 10pt; font-family: Arial">Spese documentate superiori a 10.000
                                                Euro</span></td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtSpese" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Style="text-align: right" Width="66px" TabIndex="10" ToolTip="E' possibile indicare l'effettivo ammontare delle spese sostenute per gli invalidi al 100% con indennit&#224; di accompagnamento, qualora l'importo complessivo, da documentare obbligatoriamente, sia superiore a 10.000 euro che la Legge gi&#224; riconosce automaticamente in detrazione.">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>&nbsp; (<span style="font-size: 8pt; font-family: Arial;"><em>compilare solo se superiori a 10.000 Euro!</em></span>)
                                            <asp:RegularExpressionValidator ID="RegSpese" runat="server" ControlToValidate="txtSpese"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="302"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 305px">
                                            <span style="font-size: 10pt; font-family: Arial">Grado di Parentela </span>
                                        </td>
                                        <td><asp:DropDownList ID="cmbParentela" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="318px" TabIndex="11">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 305px">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 305px">
                                            <strong><span style="font-size: 10pt; font-family: Arial">RESIDENZA</span></strong></td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Stato</span></td>
                                        <td style="width: 169px">
                                            <asp:DropDownList ID="cmbNazioneRes" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="177px" AutoPostBack="True" TabIndex="12">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 37px">
                                            <span style="font-size: 10pt; font-family: Arial">Prov.</span></td>
                                        <td style="width: 56px">
                                            <asp:DropDownList ID="cmbPrRes" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="54px" AutoPostBack="True" TabIndex="13">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 49px">
                                            <span style="font-size: 10pt; font-family: Arial">Comune</span></td>
                                        <td style="width: 198px">
                                            <asp:DropDownList ID="cmbComuneRes" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="191px" TabIndex="14">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 35px">
                                            <span style="font-size: 10pt; font-family: Arial">CAP</span></td>
                                        <td>
                                            <asp:TextBox ID="txtCAP" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="5" Width="49px" TabIndex="15">-----</asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCAP"
                                                ErrorMessage="5 Numeri" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d{5}$" TabIndex="303"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 89px; height: 24px">
                                            <span style="font-size: 10pt; font-family: Arial">Indirizzo</span></td>
                                        <td style="width: 115px; height: 24px">
                                            <asp:DropDownList ID="cmbTipoIRes" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="113px" TabIndex="16">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 204px; height: 24px">
                                            <asp:TextBox ID="txtIndirizzo" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="100" Width="197px" TabIndex="17"></asp:TextBox>
                                        </td>
                                        <td style="width: 50px; height: 24px">
                                            <span style="font-size: 10pt; font-family: Arial">Civico</span></td>
                                        <td style="width: 85px; height: 24px">
                                            <asp:TextBox ID="txtCivico" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Width="77px" MaxLength="5" TabIndex="18"></asp:TextBox>
                                        </td>
                                        <td style="height: 24px; width: 58px;">
                                            <span style="font-size: 10pt; font-family: Arial">Telefono</span></td>
                                        <td style="height: 24px">
                                            <asp:TextBox ID="txtTelefono" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Width="95px" MaxLength="30" TabIndex="19"></asp:TextBox>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1589" runat="server" ControlToValidate="txtTelefono"
                                                ErrorMessage="Obbligatorio" Font-Names="arial" Font-Size="8pt" TabIndex="304">Obbligatorio</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 415px" align="center">
                                            <asp:RequiredFieldValidator ID="ReqResidenza" runat="server" ControlToValidate="txtIndirizzo"
                                                ErrorMessage="Indicare la residenza!" Font-Names="arial" Font-Size="8pt" TabIndex="305"></asp:RequiredFieldValidator>
                                            &nbsp;</td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="ReqCivico" runat="server" ControlToValidate="txtCivico"
                                                ErrorMessage="Indicare il civico!" Font-Names="arial" Font-Size="8pt" TabIndex="306"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 415px">
                                            <strong><span style="font-size: 10pt; font-family: Arial">RECAPITO (compilare solo se
                                                diverso dalla residenza)</span></strong></td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Presso</span></td>
                                        <td style="width: 179px">
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <asp:TextBox ID="TextBox5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                    BorderWidth="1px" MaxLength="100" Width="170px" TabIndex="20"></asp:TextBox>
                                            </span>
                                        </td>
                                        <td style="width: 37px; font-size: 10pt; font-family: Arial;">
                                            <span>Prov.</span></td>
                                        <td style="width: 56px">
                                            <asp:DropDownList ID="cmbProvRec" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="54px" AutoPostBack="True" TabIndex="21">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 49px">
                                            <span style="font-size: 10pt; font-family: Arial">Comune</span></td>
                                        <td style="width: 198px">
                                            <asp:DropDownList ID="cmbComuneRec" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="191px" TabIndex="22">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 35px">
                                            <span style="font-size: 10pt; font-family: Arial">CAP</span></td>
                                        <td>
                                            <asp:TextBox ID="txtCapRec" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="5" Width="49px" TabIndex="23"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCapRec"
                                                ErrorMessage="5 Numeri" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d{5}$" TabIndex="307"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 89px; height: 24px">
                                            <span style="font-size: 10pt; font-family: Arial">Indirizzo</span></td>
                                        <td style="width: 115px; height: 24px">
                                            <asp:DropDownList ID="cmbTipoIRec" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="113px" TabIndex="24">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 204px; height: 24px">
                                            <asp:TextBox ID="TextBox2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="100" Width="197px" TabIndex="25"></asp:TextBox>
                                        </td>
                                        <td style="width: 50px; height: 24px">
                                            <span style="font-size: 10pt; font-family: Arial">Civico</span></td>
                                        <td style="width: 85px; height: 24px">
                                            <asp:TextBox ID="TextBox3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Width="77px" MaxLength="5" TabIndex="26"></asp:TextBox>
                                        </td>
                                        <td style="height: 24px; width: 58px;">
                                            <span style="font-size: 10pt; font-family: Arial">Telefono</span></td>
                                        <td style="height: 24px">
                                            <asp:TextBox ID="TextBox4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Width="95px" MaxLength="30" TabIndex="27"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblPressoErrore" runat="server" Font-Names="arial" Font-Size="8pt"
                                                ForeColor="Red" Text="Inserire l'indirizzo e il civico del recapito!" Visible="False" TabIndex="308"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">DICHIARA</span></strong></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <asp:CheckBox ID="chTitolare" runat="server" CssClass="CssLabel" Font-Bold="False"
                                                    Font-Names="Arial" Font-Size="10pt" Style="z-index: 141; left: 167px; position: static;
                                                    top: 396px" TabIndex="28" Text="Nel nucleo famigliare del richiedente esistono titolari di un contratto di assegnazione di alloggio di edilizia residenziale pubblica"
                                                    Width="756px" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px">
                                            <strong><span style="font-size: 10pt; font-family: Arial">RESIDENZA IN LOMBARDIA
                                                <img border="0" src="Immagini/Aiuto.gif" alt ="help1" id="ImgHelpb" onclick="return ImgHelpb_onclick()" style="cursor: pointer" /></span></strong></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 11px">
                                            <span style="font-size: 10pt; font-family: Arial">Si dichiara che, considerando il periodo
                                                immediatamente precedente la data di presentazione della domanda:</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbResidenza" runat="server" Font-Bold="False" Font-Names="ARIAL"
                                                Font-Size="10pt" ForeColor="Black" Height="20px" Style="z-index: 149; left: 168px;
                                                position: static; top: 95px" TabIndex="29" Width="534px">
                                            </asp:DropDownList>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="cmbResidenza"
                                                ErrorMessage="Effettuare una scelta" Font-Names="arial" Font-Size="8pt" ValidationExpression="[^a-zA-Z \-]|( )|(\-\-)|(^\s*$)" TabIndex="309"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 8pt; font-family: Arial"></span></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">MOTIVO DI PRESENTAZIONE DELLA
                                                DOMANDA
                                                <img border="0" src="Immagini/Aiuto.gif" alt ="help1" id="ImgHelpc" onclick="return ImgHelpc_onclick()" style="cursor: pointer" /></span></strong></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Si richiede l'assegnazione in quanto:</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbPresentaD" runat="server" Font-Bold="False" Font-Names="ARIAL"
                                                Font-Size="10pt" ForeColor="Black" Height="20px" Style="z-index: 103; left: 167px;
                                                position: static; top: 175px" TabIndex="30" Width="533px">
                                            </asp:DropDownList>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1080" runat="server"
                                                ControlToValidate="cmbPresentaD" ErrorMessage="Effettuare una scelta" Font-Names="arial"
                                                Font-Size="8pt" TabIndex="310" ValidationExpression="[^a-zA-Z \-]|( )|(\-\-)|(^\s*$)"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Si dichiara altresì:</span></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 22px">
                                            <asp:CheckBox ID="cfProfugo" runat="server" Font-Names="ARIAL" Font-Size="10pt" Height="12px"
                                                Style="z-index: 108; left: 167px; position: static; top: 226px" TabIndex="31"
                                                Text="di essere nelle condizioni di profugo rimpatriato da non oltre un quinquennio."
                                                Width="483px" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 22px; text-align: center">
                                            &nbsp;<br />
                                            <asp:Label ID="lblAvviso0" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                                Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti."
                                                Width="502px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="Redditi Richiedente">
                                <table width="100%">
                                    <tr>
                                        <td valign="middle">
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO MOBILIARE (risultante
                                                al 31 Dicembre del 2017) <a onclick="javascript:window.open('help/d.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" style="cursor: pointer" id="helpmobiliare" /></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="txtCod" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left" Width="179px" TabIndex="1"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="txtIntermediario1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="2" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="txtImporto1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="3" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegImporto1" runat="server" ControlToValidate="txtImporto1"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="400"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="txtCodice2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="4" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="txtIntermediario2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="5" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="txtImporto2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="6" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegImporto2" runat="server" ControlToValidate="txtImporto2"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="401"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td valign="middle" style="text-align: left">
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO IMMOBILIARE (risultante
                                                al 31 Dicembre del 2017) <a onclick="javascript:window.open('help/e.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" style="cursor: pointer" id="helpImmobiliare" /></a></span></strong></td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td style="width: 30px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 145px; height: 22px">
                                            <asp:DropDownList ID="txtImmob1_1" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="7"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 72px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 59px; height: 22px">
                                            <asp:DropDownList ID="cmbPercProprieta1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="53px" TabIndex="8">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 44px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 144px; height: 22px">
                                            <asp:TextBox ID="txtValore1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="9" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV1" runat="server" ControlToValidate="txtValore1"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="402"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="height: 22px; width: 179px;">
                                            <asp:TextBox ID="txtMutuo1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="10" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo1" runat="server" ControlToValidate="txtMutuo1"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="403"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                TabIndex="404" Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="height: 22px">
                                            <asp:CheckBox ID="txtImmob1_5" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="11" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 145px">
                                            <asp:DropDownList ID="txtImmob2_1" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="12"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 72px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 59px">
                                            <asp:DropDownList ID="cmbPercProprieta2" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="53px" TabIndex="13">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 44px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 144px">
                                            <asp:TextBox ID="txtValore2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="14" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV2" runat="server" ControlToValidate="txtValore2"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="405"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="width: 179px">
                                            <asp:TextBox ID="txtMutuo2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="15" Width="55px" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo2" runat="server" ControlToValidate="txtMutuo2"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="406"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo1" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                TabIndex="407" Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 654px;
                                                position: static; top: 158px" TabIndex="16" Text="Uso Ab." Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Categoria catastale dell'immobile
                                                ad uso abitativo
                                                <asp:DropDownList ID="cmbImmobCat1_1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Style="z-index: 107; left: 437px; position: static; top: 207px" TabIndex="17"
                                                    Width="61px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblImmob" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                    TabIndex="408" Visible="False" Width="228px"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td valign="middle" style="text-align: left">
                                            <strong><span style="font-size: 10pt; font-family: Arial">REDDITI anno 2017 <a onclick="javascript:window.open('help/f.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" style="cursor: pointer" id="helpRedditi" /></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="height: 21px; width: 104px;">
                                            <span style="font-size: 10pt; font-family: Arial">Reddito IRPEF</span></td>
                                        <td style="height: 21px; width: 198px;">
                                            <asp:TextBox ID="txtIrpef1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="18" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtIrpef1"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="409"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px; width: 102px;">
                                            <span style="font-size: 10pt; font-family: Arial">Proventi Agrari</span></td>
                                        <td style="height: 21px; width: 197px;">
                                            <asp:TextBox ID="txtAgrari1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="19" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegAgrari1" runat="server" ControlToValidate="txtAgrari1"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="410"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 104px">
                                            <span style="font-size: 10pt; font-family: Arial">Reddito IRPEF</span></td>
                                        <td style="width: 198px">
                                            <asp:TextBox ID="txtIrpef2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="20" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtIrpef2"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="411"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 102px">
                                            <span style="font-size: 10pt; font-family: Arial">Proventi Agrari</span></td>
                                        <td style="width: 197px">
                                            <asp:TextBox ID="txtAgrari2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="21" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegAgrari2" runat="server" ControlToValidate="txtAgrari2"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="412"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td valign="middle" style="text-align: left">
                                            <strong><span style="font-size: 10pt; font-family: Arial">ALTRI REDDITI anno 2017 <a onclick="javascript:window.open('help/g.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" style="cursor: pointer" id="helpAltri" /></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Descrizione</span></td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="txtDescrAltri1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="22" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo</span></td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="txtAltri1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="23" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegAltri1" runat="server" ControlToValidate="txtAltri1"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="413"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Descrizione</span></td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="txtDescrAltri2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="24" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo</span></td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="txtAltri2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="25" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegAltri2" runat="server"
                                                ControlToValidate="txtAltri2" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="414"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td valign="middle" style="text-align: left">
                                            <strong><span style="font-size: 10pt; font-family: Arial">DETRAZIONI anno 2017 <a onclick ="javascript:window.open('help/h.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" style="cursor: pointer" id="helpDetrazioni" /></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 40px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="txtDet1_1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="26"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo</span></td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="txtDetr1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="27" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegDetr1" runat="server"
                                                ControlToValidate="txtDetr1" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="415"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 21px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 229px; height: 21px">
                                            <asp:DropDownList ID="txtDet2_1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="28"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 21px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo</span></td>
                                        <td style="width: 195px; height: 21px">
                                            <asp:TextBox ID="txtDetr2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="29" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegDetr2" runat="server"
                                                ControlToValidate="txtDetr2" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="416"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 24px;">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 229px; height: 24px;">
                                            <asp:DropDownList ID="txtDet3_1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="30"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 24px;">
                                            <span style="font-size: 10pt; font-family: Arial">Importo</span></td>
                                        <td style="width: 195px; height: 24px;">
                                            <asp:TextBox ID="txtDetr3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="31" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegDetr3" runat="server"
                                                ControlToValidate="txtDetr3" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$" TabIndex="417"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 24px">
                                            &nbsp;</td>
                                        <td style="height: 24px">
                                            &nbsp;</td>
                                        <td style="height: 24px">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblAvviso1aa" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                                Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti."
                                                Width="502px" TabIndex="806"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="C1"><table width="100%">
                                <tr>
                                    <td style="width: 89px">
                                        <span style="font-size: 10pt; font-family: Arial">Cognome</span></td>
                                    <td style="width: 213px">
                                        <asp:TextBox ID="txtCognomeC1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Width="203px" MaxLength="50" TabIndex="1"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegCognomeC1" runat="server" ControlToValidate="txtCognomeC1"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="([a-zA-z\s]{2,50})" TabIndex="500"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label2" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False" TabIndex="501"></asp:Label>
                                    </td>
                                    <td style="width: 55px">
                                        <span style="font-size: 10pt; font-family: Arial">Nome</span></td>
                                    <td style="width: 219px">
                                        <asp:TextBox ID="txtNomeC1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="2" Width="203px" MaxLength="50"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegNomeC1" runat="server" ControlToValidate="txtNomeC1"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="([a-zA-z\s]{2,50})"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label3" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                        <span style="font-size: 10pt; font-family: Arial"></span>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 89px">
                                        <span style="font-size: 10pt; font-family: Arial">Codice Fiscale</span></td>
                                    <td style="width: 213px; font-size: 10pt; font-family: Arial;">
                                        <asp:TextBox ID="txtCFC1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="16" TabIndex="3" Width="203px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegCF1" runat="server" ControlToValidate="txtCFC1"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^[A-Za-z]{6}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{3}[A -Za-z]{1}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblCFC1" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 55px; font-size: 10pt; font-family: Arial;">
                                        <span style="font-size: 10pt; font-family: Arial"></span></td>
                                    <td style="width: 219px; font-size: 10pt; font-family: Arial;">
                                        <asp:TextBox ID="txtNatoC1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Enabled="False" TabIndex="4" Width="203px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 49px; font-size: 10pt; font-family: Arial;">
                                        <span style="font-size: 10pt; font-family: Arial">Nato il</span></td>
                                    <td style="font-size: 10pt; font-family: Arial">
                                        <asp:TextBox ID="txtDataNascitaC1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="5" Width="77px">dd/mm/aaaa</asp:TextBox>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1C1" runat="server"
                                            ControlToValidate="txtDataNascitaC1" Display="Dynamic" ErrorMessage="gg/mm/aaaa"
                                            Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: Arial">
                                    <td style="width: 89px">
                                        <span style="font-size: 10pt; font-family: Arial">% Invalidità</span></td>
                                    <td style="width: 213px">
                                        <asp:DropDownList ID="cmbInvaliditaC1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="6">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
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
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                            <asp:ListItem>60</asp:ListItem>
                                            <asp:ListItem>61</asp:ListItem>
                                            <asp:ListItem>62</asp:ListItem>
                                            <asp:ListItem>63</asp:ListItem>
                                            <asp:ListItem>64</asp:ListItem>
                                            <asp:ListItem>65</asp:ListItem>
                                            <asp:ListItem>66</asp:ListItem>
                                            <asp:ListItem>67</asp:ListItem>
                                            <asp:ListItem>68</asp:ListItem>
                                            <asp:ListItem>69</asp:ListItem>
                                            <asp:ListItem>70</asp:ListItem>
                                            <asp:ListItem>71</asp:ListItem>
                                            <asp:ListItem>72</asp:ListItem>
                                            <asp:ListItem>73</asp:ListItem>
                                            <asp:ListItem>74</asp:ListItem>
                                            <asp:ListItem>75</asp:ListItem>
                                            <asp:ListItem>76</asp:ListItem>
                                            <asp:ListItem>77</asp:ListItem>
                                            <asp:ListItem>78</asp:ListItem>
                                            <asp:ListItem>79</asp:ListItem>
                                            <asp:ListItem>80</asp:ListItem>
                                            <asp:ListItem>81</asp:ListItem>
                                            <asp:ListItem>82</asp:ListItem>
                                            <asp:ListItem>83</asp:ListItem>
                                            <asp:ListItem>84</asp:ListItem>
                                            <asp:ListItem>85</asp:ListItem>
                                            <asp:ListItem>86</asp:ListItem>
                                            <asp:ListItem>87</asp:ListItem>
                                            <asp:ListItem>88</asp:ListItem>
                                            <asp:ListItem>89</asp:ListItem>
                                            <asp:ListItem>90</asp:ListItem>
                                            <asp:ListItem>91</asp:ListItem>
                                            <asp:ListItem>92</asp:ListItem>
                                            <asp:ListItem>93</asp:ListItem>
                                            <asp:ListItem>94</asp:ListItem>
                                            <asp:ListItem>95</asp:ListItem>
                                            <asp:ListItem>96</asp:ListItem>
                                            <asp:ListItem>97</asp:ListItem>
                                            <asp:ListItem>98</asp:ListItem>
                                            <asp:ListItem>99</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 55px">
                                        <span style="font-size: 10pt; font-family: Arial">Indennità</span>&nbsp;</td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="cmbAccompagnamentoC1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="68px" TabIndex="7">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAvvIndennitaC1" runat="server" Font-Names="arial" Font-Size="8pt"
                                            ForeColor="Red" Text="Solo se Inv.=100%" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table><table width="100%">
                                <tr>
                                    <td style="width: 305px; height: 21px">
                                        <span style="font-size: 10pt; font-family: Arial">Spese documentate superiori a 10.000
                                            Euro</span></td>
                                    <td style="height: 21px">
                                        <asp:TextBox ID="txtSpeseC1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Style="text-align: right" TabIndex="8" Width="66px" ToolTip="E' possibile indicare l'effettivo ammontare delle spese sostenute per gli invalidi al 100% con indennit&#224; di accompagnamento, qualora l'importo complessivo, da documentare obbligatoriamente, sia superiore a 10.000 euro che la Legge gi&#224; riconosce automaticamente in detrazione.">0</asp:TextBox>
                                        <span style="font-size: 8pt"><span><span style="font-size: 10pt">
                                            <em></em><span style="font-family: Arial">,00<em>&nbsp;</em></span></span><em><span
                                                style="font-family: Arial"> (<span>compilare solo se superiori a 10.000 Euro!</span></span></em></span></span><span
                                                    style="font-size: 8pt; font-family: Arial">)
                                                    <asp:RegularExpressionValidator ID="RegSpeseC1" runat="server" ControlToValidate="txtSpeseC1"
                                                        ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                </span>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: Arial">
                                    <td style="width: 305px">
                                        <span style="font-size: 10pt; font-family: Arial">Grado di Parentela</span></td>
                                    <td><asp:DropDownList ID="cmbParentelaC1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="318px" TabIndex="9">
                                    </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: Arial">
                                    <td style="width: 305px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table><table width="100%">
                                <tr>
                                    <td>
                                        <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO MOBILIARE (risultante
                                            al 31 Dicembre del 2017)
                                            <a onclick="javascript:window.open('help/d.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                </tr>
                            </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="10" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="11" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="12" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TextBox8"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="13" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox10" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="14" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox11" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="15" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TextBox11"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO IMMOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/e.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td style="width: 32px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 144px; height: 22px">
                                            <asp:DropDownList ID="txtImmob1C1" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="16"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px; height: 22px">
                                            <asp:DropDownList ID="cmbPercProprieta1C1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="17">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 43px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 144px; height: 22px">
                                            <asp:TextBox ID="txtValore1C1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="18" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV1C1" runat="server" ControlToValidate="txtValore1C1"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="height: 22px; width: 178px;">
                                            <asp:TextBox ID="txtMutuo1C1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="19" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo1C1" runat="server" ControlToValidate="txtMutuo1C1"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo1C1" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="width: 4px; height: 22px">
                                            <asp:CheckBox ID="CheckBox3" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 654px;
                                                position: static; top: 158px" TabIndex="20" Text="Uso Ab." Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 32px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 144px">
                                            <asp:DropDownList ID="txtImmob2C1" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="21"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px">
                                            <asp:DropDownList ID="cmbPercProprieta2C1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="22">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 43px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 144px">
                                            <asp:TextBox ID="txtValore2C1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="23" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV2C1" runat="server" ControlToValidate="txtValore2C1"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="width: 178px">
                                            <asp:TextBox ID="txtMutuo2C1" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="24" Width="55px" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo2C1" runat="server" ControlToValidate="txtMutuo2C1"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo2C1" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            <asp:CheckBox ID="CheckBox2" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 654px;
                                                position: static; top: 158px" TabIndex="25" Text="Uso Ab." Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Categoria catastale dell'immobile
                                                ad uso abitativo
                                                <asp:DropDownList ID="DropDownList28" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Style="z-index: 107; left: 437px; position: static; top: 207px" TabIndex="26"
                                                    Width="61px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblImmobC1" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                    TabIndex="74" Visible="False" Width="228px"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/f.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="height: 21px; width: 104px;">
                                            Reddito IRPEF</td>
                                        <td style="height: 21px; width: 198px;">
                                            <asp:TextBox ID="TextBox60" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="27" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator25" runat="server" ControlToValidate="TextBox60"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px; width: 102px;">
                                            Proventi Agrari</td>
                                        <td style="height: 21px; width: 197px;">
                                            <asp:TextBox ID="TextBox61" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="28" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator26" runat="server" ControlToValidate="TextBox61"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 104px">
                                            Reddito IRPEF</td>
                                        <td style="width: 198px">
                                            <asp:TextBox ID="TextBox62" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="29" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator277" runat="server" ControlToValidate="TextBox62"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 102px">
                                            Proventi Agrari</td>
                                        <td style="width: 197px">
                                            <asp:TextBox ID="TextBox63" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="30" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator28" runat="server" ControlToValidate="TextBox63"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">ALTRI REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/g.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox64" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="31" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox65" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="32" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator29" runat="server" ControlToValidate="TextBox65"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox66" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="33" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox67" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="34" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator30" runat="server"
                                                ControlToValidate="TextBox67" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">DETRAZIONI anno 2017
                                                <a onclick="javascript:window.open('help/h.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" style="cursor: pointer" id="Img2" /></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList1" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="35"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox68" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="36" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator31" runat="server"
                                                ControlToValidate="TextBox68" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 21px">
                                            Tipo</td>
                                        <td style="width: 229px; height: 21px">
                                            <asp:DropDownList ID="DropDownList2" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="37"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 21px">
                                            Importo</td>
                                        <td style="width: 195px; height: 21px">
                                            <asp:TextBox ID="TextBox69" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="38" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator32" runat="server"
                                                ControlToValidate="TextBox69" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList3" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="39"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox70" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="40" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator33" runat="server"
                                                ControlToValidate="TextBox70" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblAvviso3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                                Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti."
                                                Width="502px" TabIndex="807"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="C2"><table width="100%">
                                <tr>
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">Cognome</span></td>
                                    <td style="width: 213px">
                                        <asp:TextBox ID="txtCognomeC2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Width="203px" TabIndex="1"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCognomeC2" runat="server" ControlToValidate="txtCognomeC2"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label2C2" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 76px">
                                        <span style="font-size: 10pt; font-family: Arial">Nome</span></td>
                                    <td style="width: 219px">
                                        <asp:TextBox ID="txtNomeC2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="2" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegNomeC2" runat="server" ControlToValidate="txtNomeC2"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label3C2" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                        <span style="font-size: 10pt; font-family: Arial"></span>
                                    </td>
                                    <td style="font-size: 10pt; font-family: Arial">
                                        &nbsp;</td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: Arial">
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">Codice Fiscale</span></td>
                                    <td style="width: 213px; font-size: 10pt; font-family: Arial;">
                                        <asp:TextBox ID="txtCFC2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="16" TabIndex="3" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCF2" runat="server" ControlToValidate="txtCFC2"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^[A-Za-z]{6}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{3}[A -Za-z]{1}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblCFC2" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 76px; font-size: 10pt; font-family: Arial;">
                                        <span style="font-size: 10pt; font-family: Arial"></span></td>
                                    <td style="width: 219px; font-size: 10pt; font-family: Arial;">
                                        <asp:TextBox ID="txtNatoC2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Enabled="False" TabIndex="4" Width="203px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 49px; font-size: 10pt; font-family: Arial;">
                                        <span style="font-size: 10pt; font-family: Arial">Nato il</span></td>
                                    <td style="font-size: 10pt; font-family: Arial">
                                        <asp:TextBox ID="txtDataNascitaC2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="5" Width="77px">dd/mm/aaaa</asp:TextBox>
                                        &nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1C2" runat="server"
                                            ControlToValidate="txtDataNascitaC2" Display="Dynamic" ErrorMessage="gg/mm/aaaa"
                                            Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: Arial">
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">% Invalidità</span></td>
                                    <td style="width: 213px">
                                        <asp:DropDownList ID="cmbInvaliditaC2" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="6">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
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
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                            <asp:ListItem>60</asp:ListItem>
                                            <asp:ListItem>61</asp:ListItem>
                                            <asp:ListItem>62</asp:ListItem>
                                            <asp:ListItem>63</asp:ListItem>
                                            <asp:ListItem>64</asp:ListItem>
                                            <asp:ListItem>65</asp:ListItem>
                                            <asp:ListItem>66</asp:ListItem>
                                            <asp:ListItem>67</asp:ListItem>
                                            <asp:ListItem>68</asp:ListItem>
                                            <asp:ListItem>69</asp:ListItem>
                                            <asp:ListItem>70</asp:ListItem>
                                            <asp:ListItem>71</asp:ListItem>
                                            <asp:ListItem>72</asp:ListItem>
                                            <asp:ListItem>73</asp:ListItem>
                                            <asp:ListItem>74</asp:ListItem>
                                            <asp:ListItem>75</asp:ListItem>
                                            <asp:ListItem>76</asp:ListItem>
                                            <asp:ListItem>77</asp:ListItem>
                                            <asp:ListItem>78</asp:ListItem>
                                            <asp:ListItem>79</asp:ListItem>
                                            <asp:ListItem>80</asp:ListItem>
                                            <asp:ListItem>81</asp:ListItem>
                                            <asp:ListItem>82</asp:ListItem>
                                            <asp:ListItem>83</asp:ListItem>
                                            <asp:ListItem>84</asp:ListItem>
                                            <asp:ListItem>85</asp:ListItem>
                                            <asp:ListItem>86</asp:ListItem>
                                            <asp:ListItem>87</asp:ListItem>
                                            <asp:ListItem>88</asp:ListItem>
                                            <asp:ListItem>89</asp:ListItem>
                                            <asp:ListItem>90</asp:ListItem>
                                            <asp:ListItem>91</asp:ListItem>
                                            <asp:ListItem>92</asp:ListItem>
                                            <asp:ListItem>93</asp:ListItem>
                                            <asp:ListItem>94</asp:ListItem>
                                            <asp:ListItem>95</asp:ListItem>
                                            <asp:ListItem>96</asp:ListItem>
                                            <asp:ListItem>97</asp:ListItem>
                                            <asp:ListItem>98</asp:ListItem>
                                            <asp:ListItem>99</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 76px">
                                        <span style="font-size: 10pt; font-family: Arial">Indennità&nbsp;Acc.</span></td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="cmbAccompagnamentoC2" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="68px" TabIndex="7">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAvvIndennitaC2" runat="server" Font-Names="arial" Font-Size="8pt"
                                            ForeColor="Red" Text="Solo se Inv.=100%" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table><table width="100%">
                                <tr>
                                    <td style="width: 305px; height: 21px">
                                        <span style="font-size: 10pt; font-family: Arial">Spese documentate superiori a 10.000
                                            Euro</span></td>
                                    <td style="height: 21px">
                                        <asp:TextBox ID="txtSpeseC2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Style="text-align: right" TabIndex="8" Width="66px" ToolTip="E' possibile indicare l'effettivo ammontare delle spese sostenute per gli invalidi al 100% con indennit&#224; di accompagnamento, qualora l'importo complessivo, da documentare obbligatoriamente, sia superiore a 10.000 euro che la Legge gi&#224; riconosce automaticamente in detrazione.">0</asp:TextBox>
                                        <span style="font-family: Arial"><span style="font-size: 8pt"><span><span><em><span>
                                            <span style="font-size: 10pt">,00</span>&nbsp;</span> (<span>compilare solo se superiori
                                                a 10.000 Euro!</span></em></span></span>)
                                            <asp:RegularExpressionValidator ID="RegSpeseC2" runat="server" ControlToValidate="txtSpeseC2"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </span></span>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: Arial">
                                    <td style="width: 305px">
                                        Grado di Parentela</td>
                                    <td>
                                        <asp:DropDownList ID="cmbParentelaC2" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="318px" TabIndex="9">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: Arial">
                                    <td style="width: 305px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table><table width="100%">
                                <tr>
                                    <td>
                                        <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO MOBILIARE (risultante
                                            al 31 Dicembre del 2017)
                                            <a onclick="javascript:window.open('help/d.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                </tr>
                            </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox12" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="10" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox13" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="11" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox14" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="12" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="TextBox14"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox15" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="13" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox16" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="14" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox17" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="15" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="TextBox17"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO IMMOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/e.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td style="width: 30px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 146px; height: 22px">
                                            <asp:DropDownList ID="txtImmob1C2" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="16"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px; height: 22px">
                                            <asp:DropDownList ID="cmbPercProprieta1C2" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="17">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 144px; height: 22px">
                                            <asp:TextBox ID="txtValore1C2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="18" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV1C2" runat="server" ControlToValidate="txtValore1C2"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="height: 22px">
                                            <asp:TextBox ID="txtMutuo1C2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="19" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo1C2" runat="server" ControlToValidate="txtMutuo1C2"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo1C2" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="height: 22px">
                                            <asp:CheckBox ID="CheckBox4" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 654px;
                                                position: static; top: 158px" TabIndex="20" Text="Uso Ab." Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 146px">
                                            <asp:DropDownList ID="txtImmob2C2" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="21"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px">
                                            <asp:DropDownList ID="cmbPercProprieta2C2" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="22">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 144px">
                                            <asp:TextBox ID="txtValore2C2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="23" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV2C2" runat="server" ControlToValidate="txtValore2C2"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td>
                                            <asp:TextBox ID="txtMutuo2C2" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="24" Width="55px" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo2C2" runat="server" ControlToValidate="txtMutuo2C2"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo2C2" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox5" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 654px;
                                                position: static; top: 158px" TabIndex="25" Text="Uso Ab." Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                </table>
                                                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Categoria catastale dell'immobile
                                                ad uso abitativo
                                                <asp:DropDownList ID="DropDownList29" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Style="z-index: 107; left: 437px; position: static; top: 207px" TabIndex="26"
                                                    Width="61px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblImmobC2" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                    TabIndex="74" Visible="False" Width="228px"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/f.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="height: 21px; width: 104px;">
                                            Reddito IRPEF</td>
                                        <td style="height: 21px; width: 198px;">
                                            <asp:TextBox ID="TextBox71" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="27" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator34" runat="server" ControlToValidate="TextBox71"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px; width: 102px;">
                                            Proventi Agrari</td>
                                        <td style="height: 21px; width: 197px;">
                                            <asp:TextBox ID="TextBox72" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="28" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator35" runat="server" ControlToValidate="TextBox72"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 104px">
                                            Reddito IRPEF</td>
                                        <td style="width: 198px">
                                            <asp:TextBox ID="TextBox73" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="29" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator36" runat="server" ControlToValidate="TextBox73"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 102px">
                                            Proventi Agrari</td>
                                        <td style="width: 197px">
                                            <asp:TextBox ID="TextBox74" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="30" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator37" runat="server" ControlToValidate="TextBox74"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td style="height: 19px">
                                            <strong><span style="font-size: 10pt; font-family: Arial">ALTRI REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/g.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox75" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="31" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox76" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="32" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator38" runat="server" ControlToValidate="TextBox76"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox77" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="33" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox78" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="34" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator39" runat="server"
                                                ControlToValidate="TextBox78" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">DETRAZIONI anno 2017
                                                <a  onclick ="javascript:window.open('help/h.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList4" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="35"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox79" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="36" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator40" runat="server"
                                                ControlToValidate="TextBox79" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 21px">
                                            Tipo</td>
                                        <td style="width: 229px; height: 21px">
                                            <asp:DropDownList ID="DropDownList5" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="37"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 21px">
                                            Importo</td>
                                        <td style="width: 195px; height: 21px">
                                            <asp:TextBox ID="TextBox80" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="38" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator41" runat="server"
                                                ControlToValidate="TextBox80" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList6" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="39"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox81" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="40" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator42" runat="server"
                                                ControlToValidate="TextBox81" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lblAvviso5" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                                Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti."
                                                Width="502px" TabIndex="808"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="C3"><table width="100%">
                                <tr>
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">Cognome</span></td>
                                    <td style="width: 213px">
                                        <asp:TextBox ID="txtCognomeC3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Width="203px" TabIndex="1"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCognomeC3" runat="server" ControlToValidate="txtCognomeC3"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label2C3" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 88px">
                                        <span style="font-size: 10pt; font-family: Arial">Nome</span></td>
                                    <td style="width: 219px">
                                        <asp:TextBox ID="txtNomeC3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="2" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegNomeC3" runat="server" ControlToValidate="txtNomeC3"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label3C3" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                        <span style="font-size: 10pt; font-family: Arial"></span>
                                    </td>
                                    <td style="font-size: 10pt; font-family: Arial">
                                        &nbsp;</td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: Arial">
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">Codice Fiscale</span></td>
                                    <td style="width: 213px; font-size: 10pt; font-family: Arial;">
                                        <asp:TextBox ID="txtCFC3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="16" TabIndex="3" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCF3" runat="server" ControlToValidate="txtCFC3"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^[A-Za-z]{6}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{3}[A -Za-z]{1}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblCFC3" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 88px; font-size: 10pt; font-family: Arial;">
                                        <span style="font-size: 10pt; font-family: Arial"></span></td>
                                    <td style="width: 219px; font-size: 10pt; font-family: Arial;">
                                        <asp:TextBox ID="txtNatoC3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Enabled="False" TabIndex="4" Width="203px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 49px; font-size: 10pt; font-family: Arial;">
                                        <span style="font-size: 10pt; font-family: Arial">Nato il</span></td>
                                    <td style="font-size: 10pt; font-family: Arial">
                                        <asp:TextBox ID="txtDataNascitaC3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="5" Width="77px">dd/mm/aaaa</asp:TextBox>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1C3" runat="server"
                                            ControlToValidate="txtDataNascitaC3" Display="Dynamic" ErrorMessage="gg/mm/aaaa"
                                            Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: Arial">
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">% Invalidità</span></td>
                                    <td style="width: 213px">
                                        <asp:DropDownList ID="cmbInvaliditaC3" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="6">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
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
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                            <asp:ListItem>60</asp:ListItem>
                                            <asp:ListItem>61</asp:ListItem>
                                            <asp:ListItem>62</asp:ListItem>
                                            <asp:ListItem>63</asp:ListItem>
                                            <asp:ListItem>64</asp:ListItem>
                                            <asp:ListItem>65</asp:ListItem>
                                            <asp:ListItem>66</asp:ListItem>
                                            <asp:ListItem>67</asp:ListItem>
                                            <asp:ListItem>68</asp:ListItem>
                                            <asp:ListItem>69</asp:ListItem>
                                            <asp:ListItem>70</asp:ListItem>
                                            <asp:ListItem>71</asp:ListItem>
                                            <asp:ListItem>72</asp:ListItem>
                                            <asp:ListItem>73</asp:ListItem>
                                            <asp:ListItem>74</asp:ListItem>
                                            <asp:ListItem>75</asp:ListItem>
                                            <asp:ListItem>76</asp:ListItem>
                                            <asp:ListItem>77</asp:ListItem>
                                            <asp:ListItem>78</asp:ListItem>
                                            <asp:ListItem>79</asp:ListItem>
                                            <asp:ListItem>80</asp:ListItem>
                                            <asp:ListItem>81</asp:ListItem>
                                            <asp:ListItem>82</asp:ListItem>
                                            <asp:ListItem>83</asp:ListItem>
                                            <asp:ListItem>84</asp:ListItem>
                                            <asp:ListItem>85</asp:ListItem>
                                            <asp:ListItem>86</asp:ListItem>
                                            <asp:ListItem>87</asp:ListItem>
                                            <asp:ListItem>88</asp:ListItem>
                                            <asp:ListItem>89</asp:ListItem>
                                            <asp:ListItem>90</asp:ListItem>
                                            <asp:ListItem>91</asp:ListItem>
                                            <asp:ListItem>92</asp:ListItem>
                                            <asp:ListItem>93</asp:ListItem>
                                            <asp:ListItem>94</asp:ListItem>
                                            <asp:ListItem>95</asp:ListItem>
                                            <asp:ListItem>96</asp:ListItem>
                                            <asp:ListItem>97</asp:ListItem>
                                            <asp:ListItem>98</asp:ListItem>
                                            <asp:ListItem>99</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 88px">
                                        <span style="font-size: 10pt; font-family: Arial">Indennità Acc.</span>&nbsp;</td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="cmbAccompagnamentoC3" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="68px" TabIndex="7">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAvvIndennitaC3" runat="server" Font-Names="arial" Font-Size="8pt"
                                            ForeColor="Red" Text="Solo se Inv.=100%" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 305px; height: 21px">
                                            <span style="font-size: 10pt; font-family: Arial">Spese documentate superiori a 10.000
                                                Euro</span></td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtSpeseC3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Style="text-align: right" TabIndex="8" Width="66px" ToolTip="E' possibile indicare l'effettivo ammontare delle spese sostenute per gli invalidi al 100% con indennit&#224; di accompagnamento, qualora l'importo complessivo, da documentare obbligatoriamente, sia superiore a 10.000 euro che la Legge gi&#224; riconosce automaticamente in detrazione.">0</asp:TextBox>
                                            <span style="font-size: 10pt"></span><span style="font-family: Arial"><span><span><span
                                                style="font-size: 10pt">,00<span style="font-size: 8pt"><em>&nbsp;</em></span></span></span><span
                                                    style="font-size: 10pt"><em> <span style="font-size: 8pt">(</span></em><span style="font-size: 8pt;">compilare
                                                        solo se superiori a 10.000 Euro!</span></span></span><span style="font-size: 8pt">)
                                                            <asp:RegularExpressionValidator ID="RegSpeseC3" runat="server" ControlToValidate="txtSpeseC3"
                                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                        </span></span>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: Arial">
                                        <td style="width: 305px">
                                            <span style="font-size: 10pt; font-family: Arial">Grado di Parentela</span></td>
                                        <td><asp:DropDownList ID="cmbParentelaC3" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="318px" TabIndex="9">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: Arial">
                                        <td style="width: 305px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table><table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO MOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/d.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox18" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="10" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox19" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="11" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox20" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="12" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="TextBox20"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox21" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="13" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox22" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="14" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox23" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="15" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="TextBox23"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO IMMOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/e.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td style="width: 30px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 146px; height: 22px">
                                            <asp:DropDownList ID="txtImmob1C3" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="16"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px; height: 22px">
                                            <asp:DropDownList ID="cmbPercProprieta1C3" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="17">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 41px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 142px; height: 22px">
                                            <asp:TextBox ID="txtValore1C3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="18" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV1C3" runat="server" ControlToValidate="txtValore1C3"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="height: 22px; width: 176px;">
                                            <asp:TextBox ID="txtMutuo1C3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="19" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo1C3" runat="server" ControlToValidate="txtMutuo1C3"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo1C3" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="height: 22px">
                                            <asp:CheckBox ID="CheckBox6" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 654px;
                                                position: static; top: 158px" TabIndex="20" Text="Uso Ab." Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 146px">
                                            <asp:DropDownList ID="txtImmob2C3" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="21"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px">
                                            <asp:DropDownList ID="cmbPercProprieta2C3" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="22">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 41px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 142px">
                                            <asp:TextBox ID="txtValore2C3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="23" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV2C3" runat="server" ControlToValidate="txtValore2C3"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="width: 176px">
                                            <asp:TextBox ID="txtMutuo2C3" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="24" Width="55px" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo2C3" runat="server" ControlToValidate="txtMutuo2C3"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo2C3" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox7" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 654px;
                                                position: static; top: 158px" TabIndex="25" Text="Uso Ab." Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                </table>
                                                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Categoria catastale dell'immobile
                                                ad uso abitativo
                                                <asp:DropDownList ID="DropDownList30" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Style="z-index: 107; left: 437px; position: static; top: 207px" TabIndex="26"
                                                    Width="61px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblImmobC3" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                    TabIndex="74" Visible="False" Width="228px"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/f.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="height: 21px; width: 104px;">
                                            Reddito IRPEF</td>
                                        <td style="height: 21px; width: 198px;">
                                            <asp:TextBox ID="TextBox82" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="27" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator43" runat="server" ControlToValidate="TextBox82"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px; width: 102px;">
                                            Proventi Agrari</td>
                                        <td style="height: 21px; width: 197px;">
                                            <asp:TextBox ID="TextBox83" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="28" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator44" runat="server" ControlToValidate="TextBox83"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 104px">
                                            Reddito IRPEF</td>
                                        <td style="width: 198px">
                                            <asp:TextBox ID="TextBox84" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="29" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator45" runat="server" ControlToValidate="TextBox84"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 102px">
                                            Proventi Agrari</td>
                                        <td style="width: 197px">
                                            <asp:TextBox ID="TextBox85" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="30" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator46" runat="server" ControlToValidate="TextBox85"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td style="height: 18px">
                                            <strong><span style="font-size: 10pt; font-family: Arial">ALTRI REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/g.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox86" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="31" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox87" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="32" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator47" runat="server" ControlToValidate="TextBox87"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox88" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="33" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox89" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="34" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator48" runat="server"
                                                ControlToValidate="TextBox89" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">DETRAZIONI anno 2017
                                                <a onclick="javascript:window.open('help/h.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList7" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="35"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox90" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="36" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator49" runat="server"
                                                ControlToValidate="TextBox90" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 21px">
                                            Tipo</td>
                                        <td style="width: 229px; height: 21px">
                                            <asp:DropDownList ID="DropDownList8" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="37"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 21px">
                                            Importo</td>
                                        <td style="width: 195px; height: 21px">
                                            <asp:TextBox ID="TextBox91" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="38" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator50" runat="server"
                                                ControlToValidate="TextBox91" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList9" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="39"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox92" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="40" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator51" runat="server"
                                                ControlToValidate="TextBox92" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label width="502px" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ID="lblAvviso66" runat="server" Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti." TabIndex="850"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="C4"><table width="100%">
                                <tr>
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">Cognome</span></td>
                                    <td style="width: 213px">
                                        <asp:TextBox ID="txtCognomeC4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Width="203px" TabIndex="1"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCognomeC4" runat="server" ControlToValidate="txtCognomeC4"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label2C4" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 88px">
                                        <span style="font-size: 10pt; font-family: Arial">Nome</span></td>
                                    <td style="width: 219px">
                                        <asp:TextBox ID="txtNomeC4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="2" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegNomeC4" runat="server" ControlToValidate="txtNomeC4"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label3C4" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                        <span style="font-size: 10pt; font-family: Arial"></span>
                                    </td>
                                    <td style="font-size: 10pt; font-family: Arial">
                                        &nbsp;</td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: Arial">
                                    <td style="width: 93px">
                                        <span>Codice Fiscale</span></td>
                                    <td style="width: 213px; font-size: 10pt; font-family: Arial;">
                                        <asp:TextBox ID="txtCFC4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="16" TabIndex="3" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCF4" runat="server" ControlToValidate="txtCFC4"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^[A-Za-z]{6}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{3}[A -Za-z]{1}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblCFC4" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 88px; font-size: 10pt; font-family: Arial;">
                                        <span></span></td>
                                    <td style="width: 219px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtNatoC4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Enabled="False" TabIndex="4" Width="203px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 49px; font-size: 10pt; font-family: arial;">
                                        <span>Nato il</span></td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        <asp:TextBox ID="txtDataNascitaC4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="5" Width="77px">dd/mm/aaaa</asp:TextBox>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1C4" runat="server"
                                            ControlToValidate="txtDataNascitaC4" Display="Dynamic" ErrorMessage="gg/mm/aaaa"
                                            Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        % Invalidità</td>
                                    <td style="width: 213px">
                                        <asp:DropDownList ID="cmbInvaliditaC4" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="6">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
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
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                            <asp:ListItem>60</asp:ListItem>
                                            <asp:ListItem>61</asp:ListItem>
                                            <asp:ListItem>62</asp:ListItem>
                                            <asp:ListItem>63</asp:ListItem>
                                            <asp:ListItem>64</asp:ListItem>
                                            <asp:ListItem>65</asp:ListItem>
                                            <asp:ListItem>66</asp:ListItem>
                                            <asp:ListItem>67</asp:ListItem>
                                            <asp:ListItem>68</asp:ListItem>
                                            <asp:ListItem>69</asp:ListItem>
                                            <asp:ListItem>70</asp:ListItem>
                                            <asp:ListItem>71</asp:ListItem>
                                            <asp:ListItem>72</asp:ListItem>
                                            <asp:ListItem>73</asp:ListItem>
                                            <asp:ListItem>74</asp:ListItem>
                                            <asp:ListItem>75</asp:ListItem>
                                            <asp:ListItem>76</asp:ListItem>
                                            <asp:ListItem>77</asp:ListItem>
                                            <asp:ListItem>78</asp:ListItem>
                                            <asp:ListItem>79</asp:ListItem>
                                            <asp:ListItem>80</asp:ListItem>
                                            <asp:ListItem>81</asp:ListItem>
                                            <asp:ListItem>82</asp:ListItem>
                                            <asp:ListItem>83</asp:ListItem>
                                            <asp:ListItem>84</asp:ListItem>
                                            <asp:ListItem>85</asp:ListItem>
                                            <asp:ListItem>86</asp:ListItem>
                                            <asp:ListItem>87</asp:ListItem>
                                            <asp:ListItem>88</asp:ListItem>
                                            <asp:ListItem>89</asp:ListItem>
                                            <asp:ListItem>90</asp:ListItem>
                                            <asp:ListItem>91</asp:ListItem>
                                            <asp:ListItem>92</asp:ListItem>
                                            <asp:ListItem>93</asp:ListItem>
                                            <asp:ListItem>94</asp:ListItem>
                                            <asp:ListItem>95</asp:ListItem>
                                            <asp:ListItem>96</asp:ListItem>
                                            <asp:ListItem>97</asp:ListItem>
                                            <asp:ListItem>98</asp:ListItem>
                                            <asp:ListItem>99</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 88px">
                                        Indennità Acc.&nbsp;</td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="cmbAccompagnamentoC4" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="68px" TabIndex="7">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAvvIndennitaC4" runat="server" Font-Names="arial" Font-Size="8pt"
                                            ForeColor="Red" Text="Solo se Inv.=100%" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 305px; height: 21px">
                                            <span style="font-size: 10pt; font-family: Arial">Spese documentate superiori a 10.000
                                                Euro</span></td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtSpeseC4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Style="text-align: right" TabIndex="8" Width="66px" ToolTip="E' possibile indicare l'effettivo ammontare delle spese sostenute per gli invalidi al 100% con indennit&#224; di accompagnamento, qualora l'importo complessivo, da documentare obbligatoriamente, sia superiore a 10.000 euro che la Legge gi&#224; riconosce automaticamente in detrazione.">0</asp:TextBox>
                                            <span style="font-family: Arial"></span><span style="font-size: 8pt"><span style="font-family: Arial">
                                                <span><span style="font-size: 10pt">,00<span style="font-size: 8pt"><em>&nbsp;</em></span></span></span><span
                                                    style="font-size: 10pt"><span style="font-family: arial"><em> <span style="font-size: 8pt;
                                                        font-family: Arial">(</span></em><span style="font-size: 8pt; font-family: Arial;">compilare
                                                            solo se superiori a 10.000 Euro!)
                                                            <asp:RegularExpressionValidator ID="RegSpeseC4" runat="server" ControlToValidate="txtSpeseC4"
                                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                        </span></span></span></span></span>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px">
                                            <span style="font-size: 10pt; font-family: Arial">Grado di Parentela</span></td>
                                        <td><asp:DropDownList ID="cmbParentelaC4" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="318px" TabIndex="9">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table><table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO MOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/d.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox24" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="10" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox25" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="11" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox26" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="12" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="TextBox26"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox27" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="13" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox28" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="14" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox29" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="15" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="TextBox29"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO IMMOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/e.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td style="width: 28px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 146px; height: 22px">
                                            <asp:DropDownList ID="txtImmob1C4" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="16"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px; height: 22px">
                                            <asp:DropDownList ID="cmbPercProprieta1C4" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="17">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 43px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 175px; height: 22px">
                                            <asp:TextBox ID="txtValore1C4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="18" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV1C5" runat="server" ControlToValidate="txtValore1C4"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="height: 22px; width: 221px;">
                                            <asp:TextBox ID="txtMutuo1C4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="19" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo1C5" runat="server" ControlToValidate="txtMutuo1C4"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo1C4" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="width: 3px; height: 22px">
                                            <asp:CheckBox ID="CheckBox8" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 654px;
                                                position: static; top: 158px" TabIndex="20" Text="Uso Ab." Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 28px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 146px">
                                            <asp:DropDownList ID="txtImmob2C4" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="21"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px">
                                            <asp:DropDownList ID="cmbPercProprieta2C4" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="22">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 43px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 175px">
                                            <asp:TextBox ID="txtValore2C4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="23" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV2C5" runat="server" ControlToValidate="txtValore2C4"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="width: 221px">
                                            <asp:TextBox ID="txtMutuo2C4" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="24" Width="55px" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo2C5" runat="server" ControlToValidate="txtMutuo2C4"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo2C4" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="width: 3px">
                                            <asp:CheckBox ID="CheckBox9" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 654px;
                                                position: static; top: 158px" TabIndex="25" Text="Uso Ab." Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                </table>
                                                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Categoria catastale dell'immobile
                                                ad uso abitativo
                                                <asp:DropDownList ID="DropDownList31" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Style="z-index: 107; left: 437px; position: static; top: 207px" TabIndex="26"
                                                    Width="61px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblImmobC4" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                    TabIndex="74" Visible="False" Width="228px"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/f.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="height: 21px; width: 104px;">
                                            Reddito IRPEF</td>
                                        <td style="height: 21px; width: 198px;">
                                            <asp:TextBox ID="TextBox93" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="27" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator52" runat="server" ControlToValidate="TextBox93"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px; width: 102px;">
                                            Proventi Agrari</td>
                                        <td style="height: 21px; width: 197px;">
                                            <asp:TextBox ID="TextBox94" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="28" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator53" runat="server" ControlToValidate="TextBox94"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 104px">
                                            Reddito IRPEF</td>
                                        <td style="width: 198px">
                                            <asp:TextBox ID="TextBox95" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="29" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator54" runat="server" ControlToValidate="TextBox95"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 102px">
                                            Proventi Agrari</td>
                                        <td style="width: 197px">
                                            <asp:TextBox ID="TextBox96" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="30" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator55" runat="server" ControlToValidate="TextBox96"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">ALTRI REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/g.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="help1" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox97" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="31" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox98" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="32" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator56" runat="server" ControlToValidate="TextBox98"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox99" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="33" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox100" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="34" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator57" runat="server"
                                                ControlToValidate="TextBox100" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">DETRAZIONI anno 2017
                                                <a onclick="javascript:window.open('help/h.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList10" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="35"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox101" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="36" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator58" runat="server"
                                                ControlToValidate="TextBox101" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 21px">
                                            Tipo</td>
                                        <td style="width: 229px; height: 21px">
                                            <asp:DropDownList ID="DropDownList11" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="37"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 21px">
                                            Importo</td>
                                        <td style="width: 195px; height: 21px">
                                            <asp:TextBox ID="TextBox102" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="38" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator59" runat="server"
                                                ControlToValidate="TextBox102" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList12" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="39"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox103" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="40" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator60" runat="server"
                                                ControlToValidate="TextBox103" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label width="502px" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ID="lblAvviso1a" runat="server" Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti." TabIndex="851"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="C5"><table width="100%">
                                <tr>
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">Cognome</span></td>
                                    <td style="width: 213px">
                                        <asp:TextBox ID="txtCognomeC5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Width="203px" TabIndex="1"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCognomeC5" runat="server" ControlToValidate="txtCognomeC5"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label2C5" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 90px">
                                        <span style="font-size: 10pt; font-family: Arial">Nome</span></td>
                                    <td style="width: 219px">
                                        <asp:TextBox ID="txtNomeC5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="2" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegNomeC5" runat="server" ControlToValidate="txtNomeC5"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label3C5" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                        <span style="font-size: 10pt; font-family: Arial"></span>
                                    </td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        &nbsp;</td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        <span>Codice Fiscale</span></td>
                                    <td style="width: 213px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtCFC5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="16" TabIndex="3" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCF5" runat="server" ControlToValidate="txtCFC5"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^[A-Za-z]{6}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{3}[A -Za-z]{1}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblCFC5" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 90px; font-size: 10pt; font-family: arial;">
                                        <span></span></td>
                                    <td style="width: 219px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtNatoC5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Enabled="False" TabIndex="4" Width="203px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 49px; font-size: 10pt; font-family: arial;">
                                        <span>Nato il</span></td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        <asp:TextBox ID="txtDataNascitaC5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="5" Width="77px">dd/mm/aaaa</asp:TextBox>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1C5" runat="server"
                                            ControlToValidate="txtDataNascitaC5" Display="Dynamic" ErrorMessage="gg/mm/aaaa"
                                            Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        % Invalidità</td>
                                    <td style="width: 213px">
                                        <asp:DropDownList ID="cmbInvaliditaC5" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="6">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
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
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                            <asp:ListItem>60</asp:ListItem>
                                            <asp:ListItem>61</asp:ListItem>
                                            <asp:ListItem>62</asp:ListItem>
                                            <asp:ListItem>63</asp:ListItem>
                                            <asp:ListItem>64</asp:ListItem>
                                            <asp:ListItem>65</asp:ListItem>
                                            <asp:ListItem>66</asp:ListItem>
                                            <asp:ListItem>67</asp:ListItem>
                                            <asp:ListItem>68</asp:ListItem>
                                            <asp:ListItem>69</asp:ListItem>
                                            <asp:ListItem>70</asp:ListItem>
                                            <asp:ListItem>71</asp:ListItem>
                                            <asp:ListItem>72</asp:ListItem>
                                            <asp:ListItem>73</asp:ListItem>
                                            <asp:ListItem>74</asp:ListItem>
                                            <asp:ListItem>75</asp:ListItem>
                                            <asp:ListItem>76</asp:ListItem>
                                            <asp:ListItem>77</asp:ListItem>
                                            <asp:ListItem>78</asp:ListItem>
                                            <asp:ListItem>79</asp:ListItem>
                                            <asp:ListItem>80</asp:ListItem>
                                            <asp:ListItem>81</asp:ListItem>
                                            <asp:ListItem>82</asp:ListItem>
                                            <asp:ListItem>83</asp:ListItem>
                                            <asp:ListItem>84</asp:ListItem>
                                            <asp:ListItem>85</asp:ListItem>
                                            <asp:ListItem>86</asp:ListItem>
                                            <asp:ListItem>87</asp:ListItem>
                                            <asp:ListItem>88</asp:ListItem>
                                            <asp:ListItem>89</asp:ListItem>
                                            <asp:ListItem>90</asp:ListItem>
                                            <asp:ListItem>91</asp:ListItem>
                                            <asp:ListItem>92</asp:ListItem>
                                            <asp:ListItem>93</asp:ListItem>
                                            <asp:ListItem>94</asp:ListItem>
                                            <asp:ListItem>95</asp:ListItem>
                                            <asp:ListItem>96</asp:ListItem>
                                            <asp:ListItem>97</asp:ListItem>
                                            <asp:ListItem>98</asp:ListItem>
                                            <asp:ListItem>99</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 90px">
                                        Indennità Acc.&nbsp;</td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="cmbAccompagnamentoC5" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="68px" TabIndex="7">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAvvIndennitaC5" runat="server" Font-Names="arial" Font-Size="8pt"
                                            ForeColor="Red" Text="Solo se Inv.=100%" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 305px; height: 21px">
                                            <span style="font-size: 10pt; font-family: Arial">Spese documentate superiori a 10.000
                                                Euro</span></td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtSpeseC5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Style="text-align: right" TabIndex="8" Width="66px" ToolTip="E' possibile indicare l'effettivo ammontare delle spese sostenute per gli invalidi al 100% con indennit&#224; di accompagnamento, qualora l'importo complessivo, da documentare obbligatoriamente, sia superiore a 10.000 euro che la Legge gi&#224; riconosce automaticamente in detrazione.">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial"></span><span style="font-size: 8pt">
                                                <span style="font-family: Arial"><span><span style="font-size: 10pt">,00<span style="font-size: 8pt"><em>&nbsp;</em></span></span></span><span
                                                    style="font-size: 10pt"><span style="font-family: arial"><em> <span style="font-size: 8pt;
                                                        font-family: Arial">(</span></em><span style="font-size: 8pt; font-family: Arial;">compilare
                                                            solo se superiori a 10.000 Euro!)
                                                            <asp:RegularExpressionValidator ID="RegSpeseC5" runat="server" ControlToValidate="txtSpeseC5"
                                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                        </span></span></span></span></span>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px">
                                            <span style="font-size: 10pt; font-family: Arial">Grado di Parentela</span></td>
                                        <td><asp:DropDownList ID="cmbParentelaC5" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="318px" TabIndex="9">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table><table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO MOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/d.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox30" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="10" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox31" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="11" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox32" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="12" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="TextBox32"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox33" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="13" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox34" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="14" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox35" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="15" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="TextBox35"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO IMMOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/e.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td style="width: 28px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 143px; height: 22px">
                                            <asp:DropDownList ID="txtImmob1C5" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="16"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px; height: 22px">
                                            <asp:DropDownList ID="cmbPercProprieta1C5" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="17">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 145px; height: 22px">
                                            <asp:TextBox ID="txtValore1C5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="18" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV1C4" runat="server" ControlToValidate="txtValore1C5"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="height: 22px">
                                            <asp:TextBox ID="txtMutuo1C5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="19" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo1C4" runat="server" ControlToValidate="txtMutuo1C5"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo1C5" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="height: 22px">
                                            <asp:CheckBox ID="CheckBox10" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="20" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 28px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 143px">
                                            <asp:DropDownList ID="txtImmob2C5" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="21"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px">
                                            <asp:DropDownList ID="cmbPercProprieta2C5" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="22">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 145px">
                                            <asp:TextBox ID="txtValore2C5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="23" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV2C4" runat="server" ControlToValidate="txtValore2C5"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td>
                                            <asp:TextBox ID="txtMutuo2C5" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="24" Width="55px" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo2C4" runat="server" ControlToValidate="txtMutuo2C5"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo2C5" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox11" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="25" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                </table>
                                                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Categoria catastale dell'immobile
                                                ad uso abitativo
                                                <asp:DropDownList ID="DropDownList32" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Style="z-index: 107; left: 437px; position: static; top: 207px" TabIndex="26"
                                                    Width="61px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblImmobC5" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                    TabIndex="74" Visible="False" Width="228px"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/f.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="height: 21px; width: 104px;">
                                            Reddito IRPEF</td>
                                        <td style="height: 21px; width: 198px;">
                                            <asp:TextBox ID="TextBox104" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="27" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator61" runat="server" ControlToValidate="TextBox104"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px; width: 102px;">
                                            Proventi Agrari</td>
                                        <td style="height: 21px; width: 197px;">
                                            <asp:TextBox ID="TextBox105" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="28" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator62" runat="server" ControlToValidate="TextBox105"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 104px">
                                            Reddito IRPEF</td>
                                        <td style="width: 198px">
                                            <asp:TextBox ID="TextBox106" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="29" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator63" runat="server" ControlToValidate="TextBox106"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 102px">
                                            Proventi Agrari</td>
                                        <td style="width: 197px">
                                            <asp:TextBox ID="TextBox107" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="30" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator64" runat="server" ControlToValidate="TextBox107"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td style="height: 16px">
                                            <strong><span style="font-size: 10pt; font-family: Arial">ALTRI REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/g.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox108" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="31" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox109" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="32" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator65" runat="server" ControlToValidate="TextBox109"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox110" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="33" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox111" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="34" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator66" runat="server"
                                                ControlToValidate="TextBox111" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">DETRAZIONI anno 2017
                                                <a onclick="javascript:window.open('help/h.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList13" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="35"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox112" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="36" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator67" runat="server"
                                                ControlToValidate="TextBox112" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 21px">
                                            Tipo</td>
                                        <td style="width: 229px; height: 21px">
                                            <asp:DropDownList ID="DropDownList14" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="37"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 21px">
                                            Importo</td>
                                        <td style="width: 195px; height: 21px">
                                            <asp:TextBox ID="TextBox113" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="38" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator68" runat="server"
                                                ControlToValidate="TextBox113" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList15" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="39"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox114" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="40" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator69" runat="server"
                                                ControlToValidate="TextBox114" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                &nbsp; &nbsp;&nbsp;&nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label width="502px" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ID="lblAvviso1b" runat="server" Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti." TabIndex="852"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="C6"><table width="100%">
                                <tr>
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">Cognome</span></td>
                                    <td style="width: 213px">
                                        <asp:TextBox ID="txtCognomeC6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Width="203px" TabIndex="1"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCognomeC6" runat="server" ControlToValidate="txtCognomeC6"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label2C6" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 91px">
                                        <span style="font-size: 10pt; font-family: Arial">Nome</span></td>
                                    <td style="width: 219px">
                                        <asp:TextBox ID="txtNomeC6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="2" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegNomeC6" runat="server" ControlToValidate="txtNomeC6"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label3C6" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                        <span style="font-size: 10pt; font-family: Arial"></span>
                                    </td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        &nbsp;</td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        <span>Codice Fiscale</span></td>
                                    <td style="width: 213px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtCFC6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="16" TabIndex="3" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCF6" runat="server" ControlToValidate="txtCFC6"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^[A-Za-z]{6}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{3}[A -Za-z]{1}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblCFC6" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 91px; font-size: 10pt; font-family: arial;">
                                        <span></span></td>
                                    <td style="width: 219px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtNatoC6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Enabled="False" TabIndex="4" Width="203px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 49px; font-size: 10pt; font-family: arial;">
                                        <span>Nato il</span></td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        <asp:TextBox ID="txtDataNascitaC6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="5" Width="77px">dd/mm/aaaa</asp:TextBox>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1C6" runat="server"
                                            ControlToValidate="txtDataNascitaC6" Display="Dynamic" ErrorMessage="gg/mm/aaaa"
                                            Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        % Invalidità</td>
                                    <td style="width: 213px">
                                        <asp:DropDownList ID="cmbInvaliditaC6" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="5">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
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
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                            <asp:ListItem>60</asp:ListItem>
                                            <asp:ListItem>61</asp:ListItem>
                                            <asp:ListItem>62</asp:ListItem>
                                            <asp:ListItem>63</asp:ListItem>
                                            <asp:ListItem>64</asp:ListItem>
                                            <asp:ListItem>65</asp:ListItem>
                                            <asp:ListItem>66</asp:ListItem>
                                            <asp:ListItem>67</asp:ListItem>
                                            <asp:ListItem>68</asp:ListItem>
                                            <asp:ListItem>69</asp:ListItem>
                                            <asp:ListItem>70</asp:ListItem>
                                            <asp:ListItem>71</asp:ListItem>
                                            <asp:ListItem>72</asp:ListItem>
                                            <asp:ListItem>73</asp:ListItem>
                                            <asp:ListItem>74</asp:ListItem>
                                            <asp:ListItem>75</asp:ListItem>
                                            <asp:ListItem>76</asp:ListItem>
                                            <asp:ListItem>77</asp:ListItem>
                                            <asp:ListItem>78</asp:ListItem>
                                            <asp:ListItem>79</asp:ListItem>
                                            <asp:ListItem>80</asp:ListItem>
                                            <asp:ListItem>81</asp:ListItem>
                                            <asp:ListItem>82</asp:ListItem>
                                            <asp:ListItem>83</asp:ListItem>
                                            <asp:ListItem>84</asp:ListItem>
                                            <asp:ListItem>85</asp:ListItem>
                                            <asp:ListItem>86</asp:ListItem>
                                            <asp:ListItem>87</asp:ListItem>
                                            <asp:ListItem>88</asp:ListItem>
                                            <asp:ListItem>89</asp:ListItem>
                                            <asp:ListItem>90</asp:ListItem>
                                            <asp:ListItem>91</asp:ListItem>
                                            <asp:ListItem>92</asp:ListItem>
                                            <asp:ListItem>93</asp:ListItem>
                                            <asp:ListItem>94</asp:ListItem>
                                            <asp:ListItem>95</asp:ListItem>
                                            <asp:ListItem>96</asp:ListItem>
                                            <asp:ListItem>97</asp:ListItem>
                                            <asp:ListItem>98</asp:ListItem>
                                            <asp:ListItem>99</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 91px">
                                        Indennità Acc.&nbsp;</td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="cmbAccompagnamentoC6" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="68px" TabIndex="7">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAvvIndennitaC6" runat="server" Font-Names="arial" Font-Size="8pt"
                                            ForeColor="Red" Text="Solo se Inv.=100%" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 305px; height: 21px">
                                            <span style="font-size: 10pt; font-family: Arial">Spese documentate superiori a 10.000
                                                Euro</span></td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtSpeseC6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Style="text-align: right" TabIndex="8" Width="66px" ToolTip="E' possibile indicare l'effettivo ammontare delle spese sostenute per gli invalidi al 100% con indennit&#224; di accompagnamento, qualora l'importo complessivo, da documentare obbligatoriamente, sia superiore a 10.000 euro che la Legge gi&#224; riconosce automaticamente in detrazione.">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial"></span><span style="font-size: 8pt">
                                                <span style="font-family: Arial"><span><span style="font-size: 10pt">,00<span style="font-size: 8pt"><em>&nbsp;</em></span></span></span><span
                                                    style="font-size: 10pt"><span style="font-family: arial"><em> <span style="font-size: 8pt;
                                                        font-family: Arial">(</span></em><span style="font-size: 8pt; font-family: Arial;">compilare
                                                            solo se superiori a 10.000 Euro!)
                                                            <asp:RegularExpressionValidator ID="RegSpeseC6" runat="server" ControlToValidate="txtSpeseC6"
                                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                        </span></span></span></span></span>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px">
                                            <span style="font-size: 10pt; font-family: Arial">Grado di Parentela</span></td>
                                        <td><asp:DropDownList ID="cmbParentelaC6" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="318px" TabIndex="9">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table><table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO MOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/d.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox36" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="10" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox37" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="11" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox38" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="12" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="TextBox38"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox39" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="13" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox40" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="14" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox41" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="15" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="TextBox41"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO IMMOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/e.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td style="width: 27px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 144px; height: 22px">
                                            <asp:DropDownList ID="txtImmob1C6" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="16"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px; height: 22px">
                                            <asp:DropDownList ID="cmbPercProprieta1C6" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="17">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 142px; height: 22px">
                                            <asp:TextBox ID="txtValore1C6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="18" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV1C6" runat="server" ControlToValidate="txtValore1C6"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="height: 22px">
                                            <asp:TextBox ID="txtMutuo1C6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="19" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo1C6" runat="server" ControlToValidate="txtMutuo1C6"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo1C6" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="height: 22px">
                                            <asp:CheckBox ID="CheckBox12" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="20" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 27px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 144px">
                                            <asp:DropDownList ID="txtImmob2C6" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="21"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px">
                                            <asp:DropDownList ID="cmbPercProprieta2C6" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="22">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 142px">
                                            <asp:TextBox ID="txtValore2C6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="23" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV2C6" runat="server" ControlToValidate="txtValore2C6"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td>
                                            <asp:TextBox ID="txtMutuo2C6" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="24" Width="55px" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo2C6" runat="server" ControlToValidate="txtMutuo2C6"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo2C6" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox13" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="25" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                </table>
                                                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Categoria catastale dell'immobile
                                                ad uso abitativo
                                                <asp:DropDownList ID="DropDownList33" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Style="z-index: 107; left: 437px; position: static; top: 207px" TabIndex="26"
                                                    Width="61px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblImmobC6" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                    TabIndex="74" Visible="False" Width="228px"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/f.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="height: 21px; width: 104px;">
                                            Reddito IRPEF</td>
                                        <td style="height: 21px; width: 198px;">
                                            <asp:TextBox ID="TextBox115" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="27" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator70" runat="server" ControlToValidate="TextBox115"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px; width: 102px;">
                                            Proventi Agrari</td>
                                        <td style="height: 21px; width: 197px;">
                                            <asp:TextBox ID="TextBox116" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="28" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator71" runat="server" ControlToValidate="TextBox116"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 104px">
                                            Reddito IRPEF</td>
                                        <td style="width: 198px">
                                            <asp:TextBox ID="TextBox117" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="29" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator72" runat="server" ControlToValidate="TextBox117"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 102px">
                                            Proventi Agrari</td>
                                        <td style="width: 197px">
                                            <asp:TextBox ID="TextBox118" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="30" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator73" runat="server" ControlToValidate="TextBox118"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">ALTRI REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/g.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox119" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="31" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox120" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="32" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator74" runat="server" ControlToValidate="TextBox120"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox121" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="33" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox122" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="34" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator75" runat="server"
                                                ControlToValidate="TextBox122" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">DETRAZIONI anno 2017
                                                <a onclick="javascript:window.open('help/h.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList16" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="35"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox123" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="36" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator76" runat="server"
                                                ControlToValidate="TextBox123" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 21px">
                                            Tipo</td>
                                        <td style="width: 229px; height: 21px">
                                            <asp:DropDownList ID="DropDownList17" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="37"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 21px">
                                            Importo</td>
                                        <td style="width: 195px; height: 21px">
                                            <asp:TextBox ID="TextBox124" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="38" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator77" runat="server"
                                                ControlToValidate="TextBox124" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList18" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="39"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox125" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="40" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator78" runat="server"
                                                ControlToValidate="TextBox125" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label width="502px" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ID="lblAvviso1d" runat="server" Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti." TabIndex="856"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="C7"><table width="100%">
                                <tr>
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">Cognome</span></td>
                                    <td style="width: 213px">
                                        <asp:TextBox ID="txtCognomeC7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Width="203px" TabIndex="1"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCognomeC7" runat="server" ControlToValidate="txtCognomeC7"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label2C7" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 88px">
                                        <span style="font-size: 10pt; font-family: Arial">Nome</span></td>
                                    <td style="width: 219px">
                                        <asp:TextBox ID="txtNomeC7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="2" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegNomeC7" runat="server" ControlToValidate="txtNomeC7"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label3C7" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                        <span style="font-size: 10pt; font-family: Arial"></span>
                                    </td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        &nbsp;</td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        <span>Codice Fiscale</span></td>
                                    <td style="width: 213px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtCFC7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="16" TabIndex="3" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCF7" runat="server" ControlToValidate="txtCFC7"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^[A-Za-z]{6}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{3}[A -Za-z]{1}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblCFC7" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 88px; font-size: 10pt; font-family: arial;">
                                        <span></span></td>
                                    <td style="width: 219px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtNatoC7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Enabled="False" TabIndex="4" Width="203px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 49px; font-size: 10pt; font-family: arial;">
                                        <span>Nato il</span></td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        <asp:TextBox ID="txtDataNascitaC7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="5" Width="77px">dd/mm/aaaa</asp:TextBox>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1C7" runat="server"
                                            ControlToValidate="txtDataNascitaC7" Display="Dynamic" ErrorMessage="gg/mm/aaaa"
                                            Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        % Invalidità</td>
                                    <td style="width: 213px">
                                        <asp:DropDownList ID="cmbInvaliditaC7" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="6">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
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
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                            <asp:ListItem>60</asp:ListItem>
                                            <asp:ListItem>61</asp:ListItem>
                                            <asp:ListItem>62</asp:ListItem>
                                            <asp:ListItem>63</asp:ListItem>
                                            <asp:ListItem>64</asp:ListItem>
                                            <asp:ListItem>65</asp:ListItem>
                                            <asp:ListItem>66</asp:ListItem>
                                            <asp:ListItem>67</asp:ListItem>
                                            <asp:ListItem>68</asp:ListItem>
                                            <asp:ListItem>69</asp:ListItem>
                                            <asp:ListItem>70</asp:ListItem>
                                            <asp:ListItem>71</asp:ListItem>
                                            <asp:ListItem>72</asp:ListItem>
                                            <asp:ListItem>73</asp:ListItem>
                                            <asp:ListItem>74</asp:ListItem>
                                            <asp:ListItem>75</asp:ListItem>
                                            <asp:ListItem>76</asp:ListItem>
                                            <asp:ListItem>77</asp:ListItem>
                                            <asp:ListItem>78</asp:ListItem>
                                            <asp:ListItem>79</asp:ListItem>
                                            <asp:ListItem>80</asp:ListItem>
                                            <asp:ListItem>81</asp:ListItem>
                                            <asp:ListItem>82</asp:ListItem>
                                            <asp:ListItem>83</asp:ListItem>
                                            <asp:ListItem>84</asp:ListItem>
                                            <asp:ListItem>85</asp:ListItem>
                                            <asp:ListItem>86</asp:ListItem>
                                            <asp:ListItem>87</asp:ListItem>
                                            <asp:ListItem>88</asp:ListItem>
                                            <asp:ListItem>89</asp:ListItem>
                                            <asp:ListItem>90</asp:ListItem>
                                            <asp:ListItem>91</asp:ListItem>
                                            <asp:ListItem>92</asp:ListItem>
                                            <asp:ListItem>93</asp:ListItem>
                                            <asp:ListItem>94</asp:ListItem>
                                            <asp:ListItem>95</asp:ListItem>
                                            <asp:ListItem>96</asp:ListItem>
                                            <asp:ListItem>97</asp:ListItem>
                                            <asp:ListItem>98</asp:ListItem>
                                            <asp:ListItem>99</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 88px">
                                        Indennità Acc.&nbsp;</td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="cmbAccompagnamentoC7" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="68px" TabIndex="7">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAvvIndennitaC7" runat="server" Font-Names="arial" Font-Size="8pt"
                                            ForeColor="Red" Text="Solo se Inv.=100%" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 305px; height: 21px">
                                            <span style="font-size: 10pt; font-family: Arial">Spese documentate superiori a 10.000
                                                Euro</span></td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtSpeseC7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Style="text-align: right" TabIndex="8" Width="66px" ToolTip="E' possibile indicare l'effettivo ammontare delle spese sostenute per gli invalidi al 100% con indennit&#224; di accompagnamento, qualora l'importo complessivo, da documentare obbligatoriamente, sia superiore a 10.000 euro che la Legge gi&#224; riconosce automaticamente in detrazione.">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial"></span><span style="font-size: 8pt">
                                                <span style="font-family: Arial"><span><span style="font-size: 10pt">,00<span style="font-size: 8pt"><em>&nbsp;</em></span></span></span><span
                                                    style="font-size: 10pt"><span style="font-family: arial"><em> <span style="font-size: 8pt;
                                                        font-family: Arial">(</span></em><span style="font-size: 8pt; font-family: Arial;">compilare
                                                            solo se superiori a 10.000 Euro!)
                                                            <asp:RegularExpressionValidator ID="RegSpeseC7" runat="server" ControlToValidate="txtSpeseC7"
                                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                        </span></span></span></span></span>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px">
                                            <span style="font-size: 10pt; font-family: Arial">Grado di Parentela</span></td>
                                        <td><asp:DropDownList ID="cmbParentelaC7" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="318px" TabIndex="9">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table><table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO MOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/d.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox42" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="10" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox43" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="11" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox44" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="12" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="TextBox44"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox45" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="13" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox46" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="14" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox47" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="15" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ControlToValidate="TextBox47"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO IMMOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/e.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td style="width: 29px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 142px; height: 22px">
                                            <asp:DropDownList ID="txtImmob1C7" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="16"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px; height: 22px">
                                            <asp:DropDownList ID="cmbPercProprieta1C7" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="17">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 146px; height: 22px">
                                            <asp:TextBox ID="txtValore1C7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="18" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV1C7" runat="server" ControlToValidate="txtValore1C7"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="height: 22px">
                                            <asp:TextBox ID="txtMutuo1C7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="19" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo1C7" runat="server" ControlToValidate="txtMutuo1C7"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo1C7" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="height: 22px">
                                            <asp:CheckBox ID="CheckBox14" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="20" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 29px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 142px">
                                            <asp:DropDownList ID="txtImmob2C7" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="21"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px">
                                            <asp:DropDownList ID="cmbPercProprieta2C7" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="22">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 146px">
                                            <asp:TextBox ID="txtValore2C7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="23" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV2C7" runat="server" ControlToValidate="txtValore2C7"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td>
                                            <asp:TextBox ID="txtMutuo2C7" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="24" Width="55px" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo2C7" runat="server" ControlToValidate="txtMutuo2C7"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo2C7" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox15" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="25" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                </table>
                                                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Categoria catastale dell'immobile
                                                ad uso abitativo
                                                <asp:DropDownList ID="DropDownList34" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Style="z-index: 107; left: 437px; position: static; top: 207px" TabIndex="26"
                                                    Width="61px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblImmobC7" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                    TabIndex="74" Visible="False" Width="228px"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/f.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="height: 21px; width: 104px;">
                                            Reddito IRPEF</td>
                                        <td style="height: 21px; width: 198px;">
                                            <asp:TextBox ID="TextBox126" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="27" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator79" runat="server" ControlToValidate="TextBox126"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px; width: 102px;">
                                            Proventi Agrari</td>
                                        <td style="height: 21px; width: 197px;">
                                            <asp:TextBox ID="TextBox127" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="28" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator80" runat="server" ControlToValidate="TextBox127"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 104px">
                                            Reddito IRPEF</td>
                                        <td style="width: 198px">
                                            <asp:TextBox ID="TextBox128" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="29" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator81" runat="server" ControlToValidate="TextBox128"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 102px">
                                            Proventi Agrari</td>
                                        <td style="width: 197px">
                                            <asp:TextBox ID="TextBox129" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="30" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator82" runat="server" ControlToValidate="TextBox129"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td style="height: 19px">
                                            <strong><span style="font-size: 10pt; font-family: Arial">ALTRI REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/g.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox130" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="31" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox131" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="32" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator83" runat="server" ControlToValidate="TextBox131"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox132" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="33" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox133" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="34" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator84" runat="server"
                                                ControlToValidate="TextBox133" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">DETRAZIONI anno 2017
                                                <a onclick="javascript:window.open('help/h.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList19" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="35"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox134" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="36" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator85" runat="server"
                                                ControlToValidate="TextBox134" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 21px">
                                            Tipo</td>
                                        <td style="width: 229px; height: 21px">
                                            <asp:DropDownList ID="DropDownList20" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="37"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 21px">
                                            Importo</td>
                                        <td style="width: 195px; height: 21px">
                                            <asp:TextBox ID="TextBox135" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="38" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator86" runat="server"
                                                ControlToValidate="TextBox135" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList21" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="39"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox136" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="40" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator87" runat="server"
                                                ControlToValidate="TextBox136" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label width="502px" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ID="lblAvviso1" runat="server" Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti." TabIndex="858"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="C8"><table width="100%">
                                <tr>
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">Cognome</span></td>
                                    <td style="width: 213px">
                                        <asp:TextBox ID="txtCognomeC8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Width="203px" TabIndex="1"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCognomeC8" runat="server" ControlToValidate="txtCognomeC8"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label2C8" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 88px">
                                        <span style="font-size: 10pt; font-family: Arial">Nome</span></td>
                                    <td style="width: 219px">
                                        <asp:TextBox ID="txtNomeC8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="2" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegNomeC8" runat="server" ControlToValidate="txtNomeC8"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label3C8" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                        <span style="font-size: 10pt; font-family: Arial"></span>
                                    </td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        &nbsp;</td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        <span>Codice Fiscale</span></td>
                                    <td style="width: 213px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtCFC8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="16" TabIndex="3" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCF8" runat="server" ControlToValidate="txtCFC8"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^[A-Za-z]{6}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{3}[A -Za-z]{1}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblCFC8" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 88px; font-size: 10pt; font-family: arial;">
                                        <span></span></td>
                                    <td style="width: 219px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtNatoC8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Enabled="False" TabIndex="4" Width="203px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 49px; font-size: 10pt; font-family: arial;">
                                        <span>Nato il</span></td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        <asp:TextBox ID="txtDataNascitaC8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="5" Width="77px">dd/mm/aaaa</asp:TextBox>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1C8" runat="server"
                                            ControlToValidate="txtDataNascitaC8" Display="Dynamic" ErrorMessage="gg/mm/aaaa"
                                            Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        % Invalidità</td>
                                    <td style="width: 213px">
                                        <asp:DropDownList ID="cmbInvaliditaC8" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="6">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
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
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                            <asp:ListItem>60</asp:ListItem>
                                            <asp:ListItem>61</asp:ListItem>
                                            <asp:ListItem>62</asp:ListItem>
                                            <asp:ListItem>63</asp:ListItem>
                                            <asp:ListItem>64</asp:ListItem>
                                            <asp:ListItem>65</asp:ListItem>
                                            <asp:ListItem>66</asp:ListItem>
                                            <asp:ListItem>67</asp:ListItem>
                                            <asp:ListItem>68</asp:ListItem>
                                            <asp:ListItem>69</asp:ListItem>
                                            <asp:ListItem>70</asp:ListItem>
                                            <asp:ListItem>71</asp:ListItem>
                                            <asp:ListItem>72</asp:ListItem>
                                            <asp:ListItem>73</asp:ListItem>
                                            <asp:ListItem>74</asp:ListItem>
                                            <asp:ListItem>75</asp:ListItem>
                                            <asp:ListItem>76</asp:ListItem>
                                            <asp:ListItem>77</asp:ListItem>
                                            <asp:ListItem>78</asp:ListItem>
                                            <asp:ListItem>79</asp:ListItem>
                                            <asp:ListItem>80</asp:ListItem>
                                            <asp:ListItem>81</asp:ListItem>
                                            <asp:ListItem>82</asp:ListItem>
                                            <asp:ListItem>83</asp:ListItem>
                                            <asp:ListItem>84</asp:ListItem>
                                            <asp:ListItem>85</asp:ListItem>
                                            <asp:ListItem>86</asp:ListItem>
                                            <asp:ListItem>87</asp:ListItem>
                                            <asp:ListItem>88</asp:ListItem>
                                            <asp:ListItem>89</asp:ListItem>
                                            <asp:ListItem>90</asp:ListItem>
                                            <asp:ListItem>91</asp:ListItem>
                                            <asp:ListItem>92</asp:ListItem>
                                            <asp:ListItem>93</asp:ListItem>
                                            <asp:ListItem>94</asp:ListItem>
                                            <asp:ListItem>95</asp:ListItem>
                                            <asp:ListItem>96</asp:ListItem>
                                            <asp:ListItem>97</asp:ListItem>
                                            <asp:ListItem>98</asp:ListItem>
                                            <asp:ListItem>99</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 88px">
                                        Indennità Acc.&nbsp;</td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="cmbAccompagnamentoC8" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="68px" TabIndex="7">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAvvIndennitaC8" runat="server" Font-Names="arial" Font-Size="8pt"
                                            ForeColor="Red" Text="Solo se Inv.=100%" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 305px; height: 21px">
                                            <span style="font-size: 10pt; font-family: Arial">Spese documentate superiori a 10.000
                                                Euro</span></td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtSpeseC8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Style="text-align: right" TabIndex="8" Width="66px" ToolTip="E' possibile indicare l'effettivo ammontare delle spese sostenute per gli invalidi al 100% con indennit&#224; di accompagnamento, qualora l'importo complessivo, da documentare obbligatoriamente, sia superiore a 10.000 euro che la Legge gi&#224; riconosce automaticamente in detrazione.">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial"></span><span style="font-size: 8pt">
                                                <span style="font-family: Arial"><span><span style="font-size: 10pt">,00<span style="font-size: 8pt"><em>&nbsp;</em></span></span></span><span
                                                    style="font-size: 10pt"><span style="font-family: arial"><em> <span style="font-size: 8pt;
                                                        font-family: Arial">(</span></em><span style="font-size: 8pt; font-family: Arial;">compilare
                                                            solo se superiori a 10.000 Euro!)
                                                            <asp:RegularExpressionValidator ID="RegSpeseC8" runat="server" ControlToValidate="txtSpeseC8"
                                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                        </span></span></span></span></span>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px">
                                            <span style="font-size: 10pt; font-family: Arial">Grado di Parentela</span></td>
                                        <td><asp:DropDownList ID="cmbParentelaC8" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="318px" TabIndex="9">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table><table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO MOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/d.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 59px; height: 24px;">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px; height: 24px;">
                                            <asp:TextBox ID="TextBox48" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="10" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px; height: 24px;">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px; height: 24px;">
                                            <asp:TextBox ID="TextBox49" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="11" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px; height: 24px;">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td style="height: 24px">
                                            <asp:TextBox ID="TextBox50" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="12" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator21" runat="server" ControlToValidate="TextBox50"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox51" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="13" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox52" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="14" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox53" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="15" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server" ControlToValidate="TextBox53"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO IMMOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/e.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td style="width: 29px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 144px; height: 22px">
                                            <asp:DropDownList ID="txtImmob1C8" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="16"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px; height: 22px">
                                            <asp:DropDownList ID="cmbPercProprieta1C8" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="17">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 146px; height: 22px">
                                            <asp:TextBox ID="txtValore1C8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="18" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV1C8" runat="server" ControlToValidate="txtValore1C8"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="height: 22px">
                                            <asp:TextBox ID="txtMutuo1C8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="19" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo1C8" runat="server" ControlToValidate="txtMutuo1C8"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo1C8" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="height: 22px">
                                            <asp:CheckBox ID="CheckBox16" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="20" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 29px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 144px">
                                            <asp:DropDownList ID="txtImmob2C8" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="21"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px">
                                            <asp:DropDownList ID="cmbPercProprieta2C8" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="22">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 146px">
                                            <asp:TextBox ID="txtValore2C8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="23" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV2C8" runat="server" ControlToValidate="txtValore2C8"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td>
                                            <asp:TextBox ID="txtMutuo2C8" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="24" Width="55px" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo2C8" runat="server" ControlToValidate="txtMutuo2C8"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo2C8" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox17" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="25" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                </table>
                                                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Categoria catastale dell'immobile
                                                ad uso abitativo
                                                <asp:DropDownList ID="DropDownList35" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Style="z-index: 107; left: 437px; position: static; top: 207px" TabIndex="26"
                                                    Width="61px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblImmobC8" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                    TabIndex="74" Visible="False" Width="228px"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/f.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="height: 21px; width: 104px;">
                                            Reddito IRPEF</td>
                                        <td style="height: 21px; width: 198px;">
                                            <asp:TextBox ID="TextBox137" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="27" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator88" runat="server" ControlToValidate="TextBox137"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px; width: 102px;">
                                            Proventi Agrari</td>
                                        <td style="height: 21px; width: 197px;">
                                            <asp:TextBox ID="TextBox138" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="28" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator89" runat="server" ControlToValidate="TextBox138"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 104px">
                                            Reddito IRPEF</td>
                                        <td style="width: 198px">
                                            <asp:TextBox ID="TextBox139" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="29" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator90" runat="server" ControlToValidate="TextBox139"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 102px">
                                            Proventi Agrari</td>
                                        <td style="width: 197px">
                                            <asp:TextBox ID="TextBox140" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="30" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator91" runat="server" ControlToValidate="TextBox140"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">ALTRI REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/g.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox141" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="31" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox142" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="32" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator92" runat="server" ControlToValidate="TextBox142"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            Descrizione</td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox143" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="33" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            Importo</td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox144" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="34" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator93" runat="server"
                                                ControlToValidate="TextBox144" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">DETRAZIONI anno 2017
                                                <a onclick="javascript:window.open('help/h.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList22" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="35"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox145" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="36" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator94" runat="server"
                                                ControlToValidate="TextBox145" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 21px">
                                            Tipo</td>
                                        <td style="width: 229px; height: 21px">
                                            <asp:DropDownList ID="DropDownList23" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="37"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 21px">
                                            Importo</td>
                                        <td style="width: 195px; height: 21px">
                                            <asp:TextBox ID="TextBox146" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="38" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator95" runat="server"
                                                ControlToValidate="TextBox146" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList24" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="39"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox147" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="40" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator96" runat="server"
                                                ControlToValidate="TextBox147" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label width="502px" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ID="lblAvviso1h" runat="server" Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti." TabIndex="870"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="C9"><table width="100%">
                                <tr>
                                    <td style="width: 93px">
                                        <span style="font-size: 10pt; font-family: Arial">Cognome</span></td>
                                    <td style="width: 213px">
                                        <asp:TextBox ID="txtCognomeC9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Width="203px" TabIndex="1"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCognomeC9" runat="server" ControlToValidate="txtCognomeC9"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label2C9" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 88px">
                                        <span style="font-size: 10pt; font-family: Arial">Nome</span></td>
                                    <td style="width: 219px">
                                        <asp:TextBox ID="txtNomeC9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="2" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegNomeC9" runat="server" ControlToValidate="txtNomeC9"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="Label3C9" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                        <span style="font-size: 10pt; font-family: Arial"></span>
                                    </td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        &nbsp;</td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        <span>Codice Fiscale</span></td>
                                    <td style="width: 213px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtCFC9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="16" TabIndex="3" Width="203px"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegCF9" runat="server" ControlToValidate="txtCFC9"
                                            Display="Dynamic" ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt"
                                            ValidationExpression="^[A-Za-z]{6}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{2}[A-Za-z]{1}[0-9LMNPQRSTUV]{3}[A -Za-z]{1}$"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lblCFC9" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                            Text="Obbligatorio!" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 88px; font-size: 10pt; font-family: arial;">
                                        <span></span></td>
                                    <td style="width: 219px; font-size: 10pt; font-family: arial;">
                                        <asp:TextBox ID="txtNatoC9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" Enabled="False" TabIndex="4" Width="203px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td style="width: 49px; font-size: 10pt; font-family: arial;">
                                        <span>Nato il</span></td>
                                    <td style="font-size: 10pt; font-family: arial">
                                        <asp:TextBox ID="txtDataNascitaC9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                            BorderWidth="1px" TabIndex="5" Width="77px">dd/mm/aaaa</asp:TextBox>
                                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1C9" runat="server"
                                            ControlToValidate="txtDataNascitaC9" Display="Dynamic" ErrorMessage="gg/mm/aaaa"
                                            Font-Names="arial" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="font-size: 10pt; font-family: arial">
                                    <td style="width: 93px">
                                        % Invalidità</td>
                                    <td style="width: 213px">
                                        <asp:DropDownList ID="cmbInvaliditaC9" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="6">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
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
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                            <asp:ListItem>60</asp:ListItem>
                                            <asp:ListItem>61</asp:ListItem>
                                            <asp:ListItem>62</asp:ListItem>
                                            <asp:ListItem>63</asp:ListItem>
                                            <asp:ListItem>64</asp:ListItem>
                                            <asp:ListItem>65</asp:ListItem>
                                            <asp:ListItem>66</asp:ListItem>
                                            <asp:ListItem>67</asp:ListItem>
                                            <asp:ListItem>68</asp:ListItem>
                                            <asp:ListItem>69</asp:ListItem>
                                            <asp:ListItem>70</asp:ListItem>
                                            <asp:ListItem>71</asp:ListItem>
                                            <asp:ListItem>72</asp:ListItem>
                                            <asp:ListItem>73</asp:ListItem>
                                            <asp:ListItem>74</asp:ListItem>
                                            <asp:ListItem>75</asp:ListItem>
                                            <asp:ListItem>76</asp:ListItem>
                                            <asp:ListItem>77</asp:ListItem>
                                            <asp:ListItem>78</asp:ListItem>
                                            <asp:ListItem>79</asp:ListItem>
                                            <asp:ListItem>80</asp:ListItem>
                                            <asp:ListItem>81</asp:ListItem>
                                            <asp:ListItem>82</asp:ListItem>
                                            <asp:ListItem>83</asp:ListItem>
                                            <asp:ListItem>84</asp:ListItem>
                                            <asp:ListItem>85</asp:ListItem>
                                            <asp:ListItem>86</asp:ListItem>
                                            <asp:ListItem>87</asp:ListItem>
                                            <asp:ListItem>88</asp:ListItem>
                                            <asp:ListItem>89</asp:ListItem>
                                            <asp:ListItem>90</asp:ListItem>
                                            <asp:ListItem>91</asp:ListItem>
                                            <asp:ListItem>92</asp:ListItem>
                                            <asp:ListItem>93</asp:ListItem>
                                            <asp:ListItem>94</asp:ListItem>
                                            <asp:ListItem>95</asp:ListItem>
                                            <asp:ListItem>96</asp:ListItem>
                                            <asp:ListItem>97</asp:ListItem>
                                            <asp:ListItem>98</asp:ListItem>
                                            <asp:ListItem>99</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 88px">
                                        Indennità Acc.&nbsp;</td>
                                    <td style="width: 219px">
                                        <asp:DropDownList ID="cmbAccompagnamentoC9" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="68px" TabIndex="7">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblAvvIndennitaC9" runat="server" Font-Names="arial" Font-Size="8pt"
                                            ForeColor="Red" Text="Solo se Inv.=100%" Visible="False"></asp:Label>
                                    </td>
                                    <td style="width: 49px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 305px; height: 21px">
                                            <span style="font-size: 10pt; font-family: Arial">Spese documentate superiori a 10.000
                                                Euro</span></td>
                                        <td style="height: 21px">
                                            <asp:TextBox ID="txtSpeseC9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Style="text-align: right" TabIndex="8" Width="66px" ToolTip="E' possibile indicare l'effettivo ammontare delle spese sostenute per gli invalidi al 100% con indennit&#224; di accompagnamento, qualora l'importo complessivo, da documentare obbligatoriamente, sia superiore a 10.000 euro che la Legge gi&#224; riconosce automaticamente in detrazione.">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial"></span><span style="font-size: 8pt">
                                                <span style="font-family: Arial"><span><span style="font-size: 10pt">,00<span style="font-size: 8pt"><em>&nbsp;</em></span></span></span><span
                                                    style="font-size: 10pt"><span style="font-family: arial"><em> <span style="font-size: 8pt;
                                                        font-family: Arial">(</span></em><span style="font-size: 8pt; font-family: Arial;">compilare
                                                            solo se superiori a 10.000 Euro!)
                                                            <asp:RegularExpressionValidator ID="RegSpeseC9" runat="server" ControlToValidate="txtSpeseC9"
                                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                        </span></span></span></span></span>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px; height: 18px;">
                                            <span style="font-size: 10pt; font-family: Arial">Grado di Parentela</span></td>
                                        <td style="height: 18px"><asp:DropDownList ID="cmbParentelaC9" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="318px" TabIndex="9">
                                        </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 10pt; font-family: arial">
                                        <td style="width: 305px; height: 18px">
                                        </td>
                                        <td style="height: 18px">
                                        </td>
                                    </tr>
                                </table><table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO MOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/d.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox54" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="10" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox55" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="11" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox56" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="12" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator23" runat="server" ControlToValidate="TextBox56"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 59px">
                                            <span style="font-size: 10pt; font-family: Arial">Codice</span></td>
                                        <td style="width: 185px">
                                            <asp:TextBox ID="TextBox57" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="13" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 84px">
                                            <span style="font-size: 10pt; font-family: Arial">Intermediario</span></td>
                                        <td style="width: 215px">
                                            <asp:TextBox ID="TextBox58" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="50" Style="text-align: left"
                                                TabIndex="14" Width="207px"></asp:TextBox>
                                        </td>
                                        <td style="width: 85px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo (Euro)</span></td>
                                        <td>
                                            <asp:TextBox ID="TextBox59" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="15" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator24" runat="server" ControlToValidate="TextBox59"
                                                    ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">PATRIMONIO IMMOBILIARE (risultante
                                                al 31 Dicembre del 2017)
                                                <a onclick="javascript:window.open('help/e.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td style="width: 30px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 147px; height: 22px">
                                            <asp:DropDownList ID="txtImmob1C9" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="16"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px; height: 22px">
                                            <asp:DropDownList ID="cmbPercProprieta1C9" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="17">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 145px; height: 22px">
                                            <asp:TextBox ID="txtValore1C9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="18" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV1C9" runat="server" ControlToValidate="txtValore1C9"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px; height: 22px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td style="height: 22px">
                                            <asp:TextBox ID="txtMutuo1C9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="19" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo1C9" runat="server" ControlToValidate="txtMutuo1C9"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo1C9" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td style="height: 22px">
                                            <asp:CheckBox ID="CheckBox18" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="20" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30px">
                                            <span style="font-size: 10pt; font-family: Arial">Tipo</span></td>
                                        <td style="width: 147px">
                                            <asp:DropDownList ID="txtImmob2C9" runat="server" Font-Names="arial" Font-Size="8pt"
                                                Style="z-index: 107; left: 192px; position: static; top: 159px" TabIndex="21"
                                                Width="139px">
                                                <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
                                                <asp:ListItem Value="1">TERRENI AGRICOLI</asp:ListItem>
                                                <asp:ListItem Value="2">TERRENI EDIFICABILI</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 79px">
                                            <span style="font-size: 10pt; font-family: Arial">% Proprietà</span></td>
                                        <td style="width: 68px">
                                            <asp:DropDownList ID="cmbPercProprieta2C9" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Width="62px" TabIndex="22">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
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
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>51</asp:ListItem>
                                                <asp:ListItem>52</asp:ListItem>
                                                <asp:ListItem>53</asp:ListItem>
                                                <asp:ListItem>54</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                                <asp:ListItem>56</asp:ListItem>
                                                <asp:ListItem>57</asp:ListItem>
                                                <asp:ListItem>58</asp:ListItem>
                                                <asp:ListItem>59</asp:ListItem>
                                                <asp:ListItem>60</asp:ListItem>
                                                <asp:ListItem>61</asp:ListItem>
                                                <asp:ListItem>62</asp:ListItem>
                                                <asp:ListItem>63</asp:ListItem>
                                                <asp:ListItem>64</asp:ListItem>
                                                <asp:ListItem>65</asp:ListItem>
                                                <asp:ListItem>66</asp:ListItem>
                                                <asp:ListItem>67</asp:ListItem>
                                                <asp:ListItem>68</asp:ListItem>
                                                <asp:ListItem>69</asp:ListItem>
                                                <asp:ListItem>70</asp:ListItem>
                                                <asp:ListItem>71</asp:ListItem>
                                                <asp:ListItem>72</asp:ListItem>
                                                <asp:ListItem>73</asp:ListItem>
                                                <asp:ListItem>74</asp:ListItem>
                                                <asp:ListItem>75</asp:ListItem>
                                                <asp:ListItem>76</asp:ListItem>
                                                <asp:ListItem>77</asp:ListItem>
                                                <asp:ListItem>78</asp:ListItem>
                                                <asp:ListItem>79</asp:ListItem>
                                                <asp:ListItem>80</asp:ListItem>
                                                <asp:ListItem>81</asp:ListItem>
                                                <asp:ListItem>82</asp:ListItem>
                                                <asp:ListItem>83</asp:ListItem>
                                                <asp:ListItem>84</asp:ListItem>
                                                <asp:ListItem>85</asp:ListItem>
                                                <asp:ListItem>86</asp:ListItem>
                                                <asp:ListItem>87</asp:ListItem>
                                                <asp:ListItem>88</asp:ListItem>
                                                <asp:ListItem>89</asp:ListItem>
                                                <asp:ListItem>90</asp:ListItem>
                                                <asp:ListItem>91</asp:ListItem>
                                                <asp:ListItem>92</asp:ListItem>
                                                <asp:ListItem>93</asp:ListItem>
                                                <asp:ListItem>94</asp:ListItem>
                                                <asp:ListItem>95</asp:ListItem>
                                                <asp:ListItem>96</asp:ListItem>
                                                <asp:ListItem>97</asp:ListItem>
                                                <asp:ListItem>98</asp:ListItem>
                                                <asp:ListItem>99</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 52px">
                                            <span style="font-size: 10pt; font-family: Arial">Valore</span></td>
                                        <td style="width: 145px">
                                            <asp:TextBox ID="txtValore2C9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="23" Width="55px" ToolTip="Valore ai fini ICI">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegV2C9" runat="server" ControlToValidate="txtValore2C9"
                                                ErrorMessage="Non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 42px">
                                            <span style="font-size: 10pt; font-family: Arial">Mutuo</span></td>
                                        <td>
                                            <asp:TextBox ID="txtMutuo2C9" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="24" Width="55px"
                                                Wrap="False" ToolTip="L'importo del Mutuo non pu&#242; eccedere il valore ai fini ICI dell'immobile">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegMutuo2C9" runat="server" ControlToValidate="txtMutuo2C9"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="lblMutuo2C9" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                Text="Superiore al Valore!" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox19" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 654px; position: static; top: 158px" TabIndex="25" Text="Uso Ab."
                                                Width="67px" ToolTip="Specificare se trattasi di unit&#224; ad uso abitativo" />
                                        </td>
                                    </tr>
                                </table>
                                                                <table style="font-size: 10pt; font-family: Arial" width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Categoria catastale dell'immobile
                                                ad uso abitativo
                                                <asp:DropDownList ID="DropDownList36" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Style="z-index: 107; left: 437px; position: static; top: 207px" TabIndex="26"
                                                    Width="61px">
                                                </asp:DropDownList>
                                                <asp:Label ID="lblImmobC9" runat="server" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red"
                                                    TabIndex="74" Visible="False" Width="228px"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/f.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="height: 21px; width: 104px;">
                                            <span style="font-size: 10pt; font-family: Arial">Reddito IRPEF</span></td>
                                        <td style="height: 21px; width: 198px;">
                                            <asp:TextBox ID="TextBox148" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="27" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator97" runat="server" ControlToValidate="TextBox148"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px; width: 102px;">
                                            <span style="font-size: 10pt; font-family: Arial">Proventi Agrari</span></td>
                                        <td style="height: 21px; width: 197px;">
                                            <asp:TextBox ID="TextBox149" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="28" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator98" runat="server" ControlToValidate="TextBox149"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 104px">
                                            <span style="font-size: 10pt; font-family: Arial">Reddito IRPEF</span></td>
                                        <td style="width: 198px">
                                            <asp:TextBox ID="TextBox150" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="29" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator99" runat="server" ControlToValidate="TextBox150"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="width: 102px">
                                            <span style="font-size: 10pt; font-family: Arial">Proventi Agrari</span></td>
                                        <td style="width: 197px">
                                            <asp:TextBox ID="TextBox151" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="30" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator100" runat="server" ControlToValidate="TextBox151"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">ALTRI REDDITI anno 2017
                                                <a onclick="javascript:window.open('help/g.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Descrizione</span></td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox152" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="31" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            <span style="font-size: 10pt; font-family: Arial">Importo</span></td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox153" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="32" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator101" runat="server" ControlToValidate="TextBox153"
                                                ErrorMessage="Valore non valido!" Font-Names="arial" Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 89px">
                                            <span style="font-size: 10pt; font-family: Arial">Descrizione</span></td>
                                        <td style="width: 196px">
                                            <asp:TextBox ID="TextBox154" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="arial" Font-Size="10pt" MaxLength="30" Style="text-align: left"
                                                TabIndex="33" Width="179px"></asp:TextBox>
                                        </td>
                                        <td style="width: 61px">
                                            <span style="font-size: 9pt; font-family: Arial">Importo</span></td>
                                        <td style="width: 199px">
                                            <asp:TextBox ID="TextBox155" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="34" Width="66px">0</asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00</span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator102" runat="server"
                                                ControlToValidate="TextBox155" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 8px">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">DETRAZIONI anno 2017
                                                <a onclick="javascript:window.open('help/h.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer"/></a></span></strong></td>
                                    </tr>
                                </table>
                                <table width="100%" style="font-size: 10pt; font-family: arial">
                                    <tr>
                                        <td style="width: 40px; font-family: arial;">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList25" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="35"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox156" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="36" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator103" runat="server"
                                                ControlToValidate="TextBox156" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px; height: 21px">
                                            Tipo</td>
                                        <td style="width: 229px; height: 21px">
                                            <asp:DropDownList ID="DropDownList26" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="37"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px; height: 21px">
                                            Importo</td>
                                        <td style="width: 195px; height: 21px">
                                            <asp:TextBox ID="TextBox157" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="38" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator104" runat="server"
                                                ControlToValidate="TextBox157" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                        <td style="height: 21px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40px">
                                            Tipo</td>
                                        <td style="width: 229px">
                                            <asp:DropDownList ID="DropDownList27" runat="server" Font-Names="arial" Font-Size="10pt"
                                                Style="z-index: 104; left: 191px; position: static; top: 378px" TabIndex="39"
                                                Width="219px">
                                                <asp:ListItem Value="0">IRPEF</asp:ListItem>
                                                <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
                                                <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 56px">
                                            Importo</td>
                                        <td style="width: 195px">
                                            <asp:TextBox ID="TextBox158" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                                BorderWidth="1px" MaxLength="10" Style="text-align: right" TabIndex="40" Width="66px">0</asp:TextBox>
                                            ,00
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator105" runat="server"
                                                ControlToValidate="TextBox158" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label width="502px" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ID="lblAvviso1i" runat="server" Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti." TabIndex="872"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="Familiari">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-family: Arial"><strong>CONDIZIONI FAMILIARI<br />
                                            </strong><span style="font-size: 10pt">Indicare per ciascuna voce se sussiste la relativa
                                                condizione. Il sistema <strong>NON</strong> evidenzia eventuali anomalie nelle scelte
                                                compiute dall’utente nell’inserimento on-line. In fase di formalizzazione della
                                                domanda presso gli uffici comunali preposti, saranno evidenziate, in relazione ai
                                                dati dichiarati, eventuali condizioni non congrue e <strong>NON</strong> saranno
                                                considerate ai fini dell’attribuzione del punteggio tutte le condizioni non compatibili.</span></span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr style="font-family: Times New Roman">
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">&nbsp;01) ANZIANI - Nuclei familiari
                                                di non più di due componenti o persone singole che, alla data di presentazione della
                                                domanda, abbiano superato i 65 anni, ovvero quando uno dei due componenti, pur non
                                                avendo tale età, sia totalmente inabile al lavoro, ai sensi della lett a) del punto
                                                4.1 del bando, o abbia un'età superiore a 75 anni; Tali persone singole o nuclei
                                                possono avere minori a carico.</span></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="cmbF1" runat="server" Font-Names="ARIAL" Font-Size="10pt" ForeColor="Black"
                                                Style="position: static" Width="620px" TabIndex="1">
                                            </asp:DropDownList>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <br />
                                                02) DISABILI - Nuclei familiari con componenti, anche anagraficamente non conviventi,
                                                ma presenti nella domanda, che siano affetti da minorazioni o malattie invalidanti
                                                che comportino un handicap grave</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbF2" runat="server" Font-Names="ARIAL" Font-Size="10pt" ForeColor="Black"
                                                Style="position: static" TabIndex="2" Width="620px">
                                            </asp:DropDownList>
                                            <a onclick ="javascript:window.open('help/i.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer" /></a></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <span style="font-size: 10pt; font-family: Arial">03) FAMIGLIA DI NUOVA FORMAZIONE -
                                                Nuclei familiari da costituirsi prima della consegna dell'alloggio, ovvero costituitisi
                                                entro i due anni precedenti alla data della domanda</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbF3" runat="server" CssClass="CssFamiAbit" Font-Names="ARIAL"
                                                Font-Size="10pt" ForeColor="Black" TabIndex="3" Width="620px">
                                            </asp:DropDownList>
                                            <a onclick ="javascript:window.open('help/l.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer" /></a></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <span style="font-size: 10pt; font-family: Arial">04) PERSONE SOLE, CON EVENTUALE MINORE
                                                A CARICO - Nuclei di un componente, con un eventuale minore o più a carico</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbF4" runat="server" CssClass="CssFamiAbit" Font-Names="ARIAL"
                                                Font-Size="10pt" ForeColor="Black" Style="position: static" TabIndex="4" Width="620px">
                                            </asp:DropDownList>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <span style="font-size: 10pt; font-family: Arial">05) STATO DI DISOCCUPAZIONE - Stato
                                                di disoccupazione determinato da una caduta del reddito complessivo del nucleo familiare
                                                superiore al 50%</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbF5" runat="server" CssClass="CssFamiAbit" Font-Names="ARIAL"
                                                Font-Size="10pt" ForeColor="Black" Style="position: static" TabIndex="5" Width="620px">
                                            </asp:DropDownList>
                                            <a onclick ="javascript:window.open('help/m.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer" /></a></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <span style="font-size: 10pt; font-family: Arial">06) RICONGIUNZIONE - Nucleo familiare
                                                che necessiti di alloggio idoneo per accogliervi parente disabile</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbF6" runat="server" CssClass="CssFamiAbit" Font-Names="ARIAL"
                                                Font-Size="10pt" ForeColor="Black" Style="position: static" TabIndex="6" Width="620px">
                                            </asp:DropDownList>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <span style="font-size: 10pt; font-family: Arial">07) CASI PARTICOLARI</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbF7" runat="server" CssClass="CssFamiAbit" Font-Names="ARIAL"
                                                Font-Size="10pt" Font-Strikeout="False" ForeColor="Black" Style="position: static"
                                                TabIndex="7" Width="620px">
                                            </asp:DropDownList>
                                            </td>
                                    </tr>
                                </table>
                                &nbsp;&nbsp;<br />
                                &nbsp;
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label width="502px" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ID="lblAvviso1l" runat="server" Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti."></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="Abitative1">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-family: Arial"><strong>CONDIZIONI ABITATIVE<br />
                                            </strong><span style="font-size: 10pt">Indicare per ciascuna voce se sussiste la relativa
                                                condizione. Il sistema <strong>NON</strong> evidenzia eventuali anomalie nelle scelte
                                                compiute dall’utente nell’inserimento on-line. In fase di formalizzazione della
                                                domanda presso gli uffici comunali preposti, saranno evidenziate, in relazione ai
                                                dati dichiarati, eventuali condizioni non congrue e <strong>NON</strong> saranno
                                                considerate ai fini dell’attribuzione del punteggio tutte le condizioni non compatibili.</span></span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">08) RILASCIO ALLOGGIO - Concorrenti
                                                che debbano rilasciare l'alloggio</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbA1" runat="server" CssClass="CssFamiAbit" Font-Names="arial"
                                                Font-Size="10pt" ForeColor="Black" Style="left: 164px; position: static; top: 80px"
                                                Width="640px" TabIndex="1">
                                            </asp:DropDownList>
                                            <a onclick ="javascript:window.open('help/n.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer" /></a></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChMorosita" runat="server" Checked="True" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 163px; position: static; top: 100px" TabIndex="2" Text="Non vi &#232; provvedimento di rilascio per morosit&#224;"
                                                Width="304px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 8pt;"><span style="font-family: Arial">In caso di rilascio per morosità il
                                                punteggio viene attribuito solo quando il canone di locazione da corrispondere,
                                                integrato delle spese accessorie, sia stato superiore di oltre il 5% al "canone
                                                sopportabile"<br />
                                                <span style="font-size: 9pt"><strong>ATTENZIONE: per avere il diritto al punteggio
                                                    di Rilascio Alloggio in caso di morosità, occorre compilare in ogni sua parte la
                                                    tabella T3 relativa all'anno/i in cui si è verificata la morosità.<br />
                                                    LA TABELLA T3, DEBITAMENTE COMPILATA E SOTTOSCRITTA DOVRA' ESSERE PRESENTATA AL
                                                    COMUNE AL MOMENTO DELLA FORMALIZZAZIONE DELLA DOMANDA.<br />
                                                    <asp:HyperLink ID="HyperLink4" runat="server" Font-Names="arial" Font-Size="8pt"
                                                        NavigateUrl="~/AutoCompilazione/TabT3.pdf" Target="_blank" TabIndex="3">Scarica la TABELLA T3</asp:HyperLink>
                                                </strong></span></span></span></td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <br />
                                                09) CONDIZIONE ABITATIVA IMPROPRIA</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbA2" runat="server" CssClass="CssFamiAbit" Font-Names="arial"
                                                Font-Size="10pt" ForeColor="Black" Style="left: 164px; position: static; top: 182px"
                                                TabIndex="4" Width="640px">
                                            </asp:DropDownList>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <br />
                                                10) COABITAZIONE - Richiedenti che abitino da almeno tre anni con il proprio nucleo
                                                familiare in uno stesso alloggio con altro o più nuclei familiari</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbA3" runat="server" CssClass="CssFamiAbit" Font-Names="arial"
                                                Font-Size="10pt" ForeColor="Black" Style="left: 164px; position: static; top: 241px"
                                                TabIndex="5" Width="640px">
                                            </asp:DropDownList>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <br />
                                                11) SOVRAFFOLLAMENTO - Richiedenti che abitino da almeno tre anni con il proprio
                                                nucleo familiare</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbA4" runat="server" CssClass="CssFamiAbit" Font-Names="arial"
                                                Font-Size="10pt" ForeColor="Black" Style="left: 164px; position: static; top: 285px"
                                                TabIndex="6" Width="640px">
                                            </asp:DropDownList>
                                            <a onclick ="javascript:window.open('help/o.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer" /></a></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <br />
                                                12) CONDIZIONE DELL'ALLOGGIO - Richiedenti che abitino con il proprio nucleo familiare</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbA5" runat="server" CssClass="CssFamiAbit" Font-Names="arial"
                                                Font-Size="10pt" ForeColor="Black" Style="left: 163px; position: static; top: 329px"
                                                TabIndex="7" Width="640px">
                                            </asp:DropDownList>
                                            </td>
                                    </tr>
                                </table>
                                &nbsp;<br />
                                &nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label width="502px" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ID="lblAvviso1m" runat="server" Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti."></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="Abitative2">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <span style="font-family: Arial"><strong>CONDIZIONI ABITATIVE<br />
                                            </strong><span style="font-size: 10pt">Indicare per ciascuna voce se sussiste la relativa
                                                condizione. Il sistema <strong>NON</strong> evidenzia eventuali anomalie nelle scelte
                                                compiute dall’utente nell’inserimento on-line. In fase di formalizzazione della
                                                domanda presso gli uffici comunali preposti, saranno evidenziate, in relazione ai
                                                dati dichiarati, eventuali condizioni non congrue e <strong>NON</strong> saranno
                                                considerate ai fini dell’attribuzione del punteggio tutte le condizioni non compatibili.</span></span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">13) BARRIERE ARCHITETTONICHE - Richiedenti,
                                                di cui al precedente punto 2) che abitino con il proprio nucleo familiare in alloggio
                                                che non consenta una normale condizione abitativa</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbA6" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Black"
                                                Style="left: 165px; position: static; top: 94px" Width="640px" TabIndex="1">
                                            </asp:DropDownList>
                                            <a onclick ="javascript:window.open('help/p.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer" /></a></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <br />
                                                14) CONDIZIONE DI ACCESSIBILITA' - Richiedenti, di cui ai precedenti punti 1) e
                                                2), che abitino con il proprio nucleo familiare in alloggio che non è servito da
                                                ascensore ed è situato superiormente al primo piano</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbA7" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Black"
                                                Style="left: 166px; position: static; top: 150px" TabIndex="2" Width="640px">
                                            </asp:DropDownList>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <br />
                                                15) LONTANANZA DALLA SEDE DI LAVORO - Richiedenti che impiegano più di 90 minuti
                                                per raggiungere la sede di lavoro, utilizzando gli ordinari mezzi pubblici, in comune
                                                diverso da quello di residenza.</span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cmbA8" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Black"
                                                Style="left: 167px; position: static; top: 201px" TabIndex="3" Width="640px">
                                            </asp:DropDownList>
                                            <a onclick ="javascript:window.open('help/q.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer" /></a></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">
                                                <br />
                                                16) AFFITTO ONEROSO</span></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="cmbA9" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Black"
                                                Style="left: 167px; position: static; top: 245px" TabIndex="4" Width="640px" Enabled="False">
                                            </asp:DropDownList>
                                            <a onclick ="javascript:window.open('help/r.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');"><img border="0" src="Immagini/Aiuto.gif" alt ="" style="cursor: pointer" /></a></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <span style="font-size: 10pt; font-family: Arial"><em>NB: condizione gestita automaticamente dal sistema.<br />
                                            </em>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 193px">
                                            <span style="font-size: 10pt; font-family: Arial">Canone di locazione per l'anno</span></td>
                                        <td>
                                            <asp:TextBox ID="txtAnnoCanone" runat="server" Columns="3" Font-Bold="False" Font-Names="arial"
                                                Font-Size="10pt" ForeColor="Black" MaxLength="4"
                                                Style="z-index: 103; left: 311px; position: static; top: 271px" TabIndex="5" Enabled="False">2017</asp:TextBox>
                                            &nbsp;<span style="font-size: 10pt"><span style="font-family: Arial">è di </span>
                                                <asp:TextBox ID="txtSpeseLoc" runat="server" Columns="5" Font-Bold="False" Font-Names="arial"
                                                    Font-Size="10pt" ForeColor="Black" Style="z-index: 106; left: 404px;
                                                    position: static; top: 271px" TabIndex="6"></asp:TextBox>
                                                <span style="font-family: Arial">,00 Euro&nbsp;
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator27" runat="server"
                                                        ControlToValidate="txtSpeseLoc" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                        Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                </span></span></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 193px">
                                            <span style="font-size: 10pt; font-family: Arial">Spese accessorie per l'anno</span></td>
                                        <td>
                                            <asp:TextBox ID="txtAnnoAcc" runat="server" Columns="3" Font-Bold="False" Font-Names="arial"
                                                Font-Size="10pt" ForeColor="Black" MaxLength="4"
                                                Style="z-index: 103; left: 311px; position: static; top: 271px" TabIndex="7" Enabled="False">2017</asp:TextBox>
                                            &nbsp;<span style="font-size: 10pt; font-family: Arial">è di </span>
                                            <asp:TextBox ID="txtSpeseAcc" runat="server" Columns="5" Font-Bold="False" Font-Names="arial"
                                                Font-Size="10pt" ForeColor="Black" Style="z-index: 106; left: 404px;
                                                position: static; top: 271px" TabIndex="8"></asp:TextBox>
                                            <span style="font-size: 10pt; font-family: Arial">,00 Euro (massimo 516 euro)
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator106" runat="server"
                                                    ControlToValidate="txtSpeseAcc" ErrorMessage="Valore non valido!" Font-Names="arial"
                                                    Font-Size="8pt" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </span></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 193px">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;<br />
                                &nbsp;<br />
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label width="502px" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ID="lblAvviso1p" runat="server" Text="NOTA BENE: i dati inseriti potranno essere modificati e/o integrati in fase di formalizzazione della domanda presso gli uffici comunali preposti."></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="Requisiti">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <span><span style="font-family: Arial"><span style="font-size: 10pt"><strong>REQUISITI
                                                SOGGETTIVI<br />
                                            </strong>In questa sezione il richiedente deve dichiarare il possesso, alla data di
                                                formalizzazione della domanda, dei requisiti soggettivi di accesso all’Erp previsti
                                                dall'art.8 commi 1 e 2 del RR 1/2004 e dell'art. 28 della LR 27/2009.</span></span></span></td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td>
                                            <asp:CheckBox ID="chR1" runat="server" Checked="True" Font-Names="arial" Font-Size="10pt"
                                                Style="position: static; top: 90px" Text="a)	cittadinanza italiana o di uno Stato aderente all’Unione europea o di altro Stato, qualora il diritto di assegnazione di alloggio erp sia riconosciuto in condizioni di reciprocit&#224; da convenzioni o trattati internazionali, ovvero lo straniero sia titolare di carta di soggiorno o in possesso di permesso di soggiorno come previsto dalla vigente normativa"
                                                Width="741px" TabIndex="1" />
                                        </td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td>
                                            <br />
                                            <asp:CheckBox ID="chR2" runat="server" Checked="True" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 163px; position: static; top: 153px" TabIndex="2" Text="b)	residenza anagrafica  o svolgimento di attivit&#224; lavorativa esclusiva o principale nel Comune alla data di pubblicazione del bando; il requisito della residenza anagrafica non &#232; richiesto nei  casi descritti nel regolamento regionale"
                                                Width="740px" />
                                        </td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td>
                                            <br />
                                            <asp:CheckBox ID="chR3" runat="server" Checked="True" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 163px; position: static; top: 202px" TabIndex="3" Text="c)	assenza di precedente assegnazione in propriet&#224;, immediata o futura, di alloggio realizzato con contributo pubblico o finanziamento agevolato in qualunque forma, concesso dallo Stato, dalla Regione, dagli enti territoriali o da altri enti pubblici, sempre che l’alloggio non sia perito senza dare luogo al risarcimento del danno"
                                                Width="739px" />
                                        </td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td>
                                            <br />
                                            <asp:CheckBox ID="CheckBox20" runat="server" Checked="True" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 163px; position: static; top: 202px" TabIndex="4" Text="d)	assenza di precedente assegnazione in locazione di un alloggio ERP, qualora il rilascio sia dovuto a provvedimento amministrativo di decadenza per aver destinato l'alloggio o le relative pertinenze ad attivita' illecite che risultino da provvedimenti giudiziari e/o della pubblica sicurezza"
                                                Width="739px" />
                                        </td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td>
                                            <br />
                                            <asp:CheckBox ID="chR5" runat="server" Checked="True" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 164px; position: static; top: 300px" TabIndex="5" Text="e)	non aver ceduto in tutto o in parte, fuori dei casi previsti dalla legge, l’alloggio eventualmente assegnato in precedenza in locazione semplice;"
                                                Width="738px" />
                                        </td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td>
                                            <br />
                                            <asp:CheckBox ID="chR6" runat="server" Checked="True" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 164px; position: static; top: 335px" TabIndex="5" Text="f) Assenza di propriet&#224; o alloggio adeguato"
                                                Width="289px" />
                                        </td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td>
                                            <br />
                                            <asp:CheckBox ID="chR7" runat="server" Checked="True" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 164px; position: static; top: 356px" TabIndex="7" Text="g)	non sia stato sfrattato per morosit&#224; da alloggi erp negli ultimi 5 anni e abbia pagato le somme dovute all’ente gestore; "
                                                Width="737px" />
                                        </td>
                                    </tr>
                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                        <td>
                                            <br />
                                            <asp:CheckBox ID="chR8" runat="server" Checked="True" Font-Names="arial" Font-Size="10pt"
                                                Style="left: 165px; position: static; top: 391px" TabIndex="8" Text="h)	non sia stato occupante senza titolo di alloggi erp negli ultimi 5 anni."
                                                Width="543px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" Title="Convalida">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <span style="font-family: Arial; color: #cc0000;"><strong>Adesso verifica che i dati inseriti siano corretti, altrimenti è possibile modificare premendo "Indietro"
                                                a fine pagina. 
                                                <br />
                                                La presente dichiarazione non ha valenza formale ma solo riassuntiva dei dati inseriti.<br />
                                            </strong><strong>In fase di formalizzazione
                                                presso gli uffici del Comune di Milano sarà ulteriormente possibile modificare e/o
                                                integrare quanto dichiarato.</strong></span></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <%=sStringaSql%>
                                        </td>
                                    </tr>
                                </table>
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" StepType="Finish" Title="Spedizione">
                                <table width="100%">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong><span style="color: #cc0000; font-family: Arial">Indica ora il tuo indirizzo
                                                e-mail presso il quale il Comune di Milano, al termine del presente processo invierà
                                                un messaggio automatico di avvenuta registrazione dei dati inseriti ed il link con
                                                il quale confermare elettronicamente la tua volontà di presentare la Domanda di
                                                partecipazione al Bando ERP.<br />
                                                Successivamente sarai contattato da un operatore per concordare la data dell'appuntamento.</span></strong></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <strong><span style="font-size: 10pt; font-family: Arial">Inserisci l'indirizzo e-mail al quale potremo inviare la comunicazione di avvenuta
                                                    ricezione della domanda.</span></strong><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Indirizzo E-mail &nbsp;
                                                <asp:TextBox ID="txtmail" runat="server" Width="254px" TabIndex="1"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator107" runat="server"
                                                    ControlToValidate="txtmail" ErrorMessage="Indirizzo non valido!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                <asp:Label ID="Label110" runat="server" ForeColor="Red" Text="Errore/Mancante" Visible="False"></asp:Label>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial">Conferma E-mail </span>
                                            <asp:TextBox ID="txtConfermaMail" runat="server" Width="254px" TabIndex="2"></asp:TextBox>
                                            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator10800" runat="server"
                                                ControlToValidate="txtConfermaMail" ErrorMessage="Indirizzo non valido!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            &nbsp;<asp:Label ID="Label1111" runat="server" ForeColor="Red" Text="Errore/Mancante"
                                                Visible="False" Width="267px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" StepType="Complete" Title="Appuntamento">
                                <table width="100%">
                                    <tr>
                                        <td style="text-align: center">
                                            <strong><span style="font-size: 14pt; font-family: Arial">La domanda è stata inserita
                                                correttamente nei nostri sistemi</span></strong></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <span style="font-size: 9pt; font-family: Arial">Sarai contattato/a, a mezzo posta elettronica,
                                                all'indirizzo email indicato, per avviare la procedura per definire
                                                la data dell'appuntamento per la formalizzazione della domanda che avverrà presso gli uffici comunali di Piazzale Cimitero Monumentale, 14 - Milano<br />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            &nbsp; &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <table width="80%">
                                                <tr>
                                                    <td style="width: 251px; text-align: left">
                                                        <span style="font-size: 14pt; font-family: Arial">Richiedente</span></td>
                                                    <td style="text-align: left">
                                            <asp:Label ID="lblNomeRicevuta" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                                Font-Size="16pt" Width="405px" style="text-align: left"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 251px; text-align: left">
                                                        <span style="font-size: 14pt; font-family: Arial">N. di telefono al quale sarai contattato</span></td>
                                                    <td style="text-align: left">
                                            <asp:Label ID="lblTelefonoRicevuta" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                                Font-Size="16pt" Width="366px" style="text-align: left"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 251px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <strong><span style="font-size: 16pt; font-family: Arial">Il tuo numero di registrazione
                                                provvisorio è
                                                <asp:Label ID="LBLNUMERODOMANDA" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="16pt"
                                                    Text="Label"></asp:Label>
                                            </span></strong></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Button ID="btnStampaRic" runat="server"
                                                Text="Stampa questa pagina" TabIndex="1" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblchiamata" runat="server" Font-Names="arial" Font-Size="10pt" Text="Prima di presentarti al Comune di Milano per la formalizzazione della Domanda prendi nota e/o stampa le indicazioni operative di cui al sottostante link" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="HyperLink5" runat="server" Font-Names="arial" Font-Size="10pt" NavigateUrl="~/AutoCompilazione/Indicazioni_Operative.pdf" Target="_blank" TabIndex="2">Visualizza Indicazioni Operative</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span style="font-size: 10pt; font-family: Arial"></span></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="height: 40px">
                                            &nbsp;<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Portale.aspx" TabIndex="3">Clicca qui per tornare alla pagina principale</asp:HyperLink>
                                            &nbsp;&nbsp;<br />
                                            <br />
                                            &nbsp;<asp:HyperLink ID="HyperLink889" runat="server" NavigateUrl="http://www.comune.milano.it/"
                                                Target="_top" TabIndex="5">Clicca qui per tornare al portale del COMUNE DI MILANO</asp:HyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </asp:WizardStep>
                        </WizardSteps>
                        <SideBarStyle BorderColor="Transparent" Font-Names="arial" Font-Size="10pt" HorizontalAlign="Left"
                            VerticalAlign="Top" Width="50px" />
                        <StepStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:Wizard>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:Label ID="LBLNASCITA" runat="server" Visible="False"></asp:Label><asp:TextBox ID="TextBox1" runat="server" Visible="False" TabIndex="600"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Width="379px" TabIndex="601"></asp:Label>
        <asp:TextBox ID="TXTIDDOM" runat="server" Visible="False" TabIndex="603">0</asp:TextBox></form>
    <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
function IMG1_onclick() {
window.open('help/a.htm','help','height=370,top=0,left=0,width=480,scrollbars=yes');
}

function ImgHelpb_onclick() {
window.open('help/b.htm','help','height=370,top=0,left=0,width=480,scrollbars=yes');
}

function ImgHelpc_onclick() {
window.open('help/c.htm','help','height=400,top=0,left=0,width=480,scrollbars=yes');
}



    </script>

</body>
</html>
