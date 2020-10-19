'*** SCELTA VOCE PER IL PAGAMENTO

Partial Class PAGAMENTI_SceltaVoce
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sStringaSql As String

    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        If IsPostBack = False Then
            Session.Add("IDA", 0)
            x.Value = UCase(Request.QueryString("X")) 'serve per sapere se è aperto come finestra di dialogo
            idVoce.Value = UCase(Request.QueryString("V")) 'id Voce da passare ai pagamenti

            If x.Value = "1" Then
                tipo = "_self"

            Else
                tipo = ""
            End If
            vIdPianoFinanziario = -1

            CaricaEsercizio()

            CaricaTabella()

        End If
    End Sub

    Public Property vIdPianoFinanziario() As Long
        Get
            If Not (ViewState("par_idPianoFinanziario") Is Nothing) Then
                Return CLng(ViewState("par_idPianoFinanziario"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idPianoFinanziario") = value
        End Set

    End Property

    Sub CaricaTabella()
        Dim sStringaSql As String
        Dim sStringaSql2 As String
        Dim TestoPagina As String = ""
        Dim Id_Operatore As String

        Dim Importo As String = ""
        Dim Seleziona As String = ""
        Dim immagine As String = ""

        Dim SommaBudget As Decimal = 0
        Dim SommaAssestato As Decimal = 0
        Dim SommaPrenotato As Decimal = 0
        Dim SommaConsuntivato As Decimal = 0
        Dim SommaLiquidato As Decimal = 0
        Dim SommaResiduo As Decimal = 0

        Dim SommaValoreLordo As Decimal = 0
        Dim SommaValoreAssestatoLordo As Decimal = 0
        Dim SommaValoreVariazioni As Decimal = 0

        Dim sRisultato As String = ""

        Try

            Id_Operatore = Session.Item("ID_OPERATORE") '221

            Dim sFiliale As String = ""
            'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = "  ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If

            vIdPianoFinanziario = par.IfEmpty(Me.cmbEsercizio.SelectedValue, -1)

            par.OracleConn.Open()
            par.SettaCommand(par)
            'width='2%'
            TestoPagina = TestoPagina & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: Segoe UI; font-size: 9pt; font-weight: bold'>" _
                                      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>COD.</td>" _
                                      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td>" _
                                      & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO ASSESTATO</td>" _
                                      & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO RESIDUO</td>" _
                                      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;&nbsp;PROSEGUI</td>" _
                                      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                      & "</tr>"

            'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"


            'Select PF_VOCI dove ID presente   in PF_VOCI_OPERATORI con ID_OPERATORE oppure 
            '                    ID_VOCE_MADRE in PF_VOCI_OPERATORI con ID_OPERATORE 

            ' Per ogni record, controllo se ci sono record PF_VOCI dove ID_VOCE_MADRE=ID corrente
            ' Se ci sono record, allora non faccio apparire il link "Prosegui", altrimenti faccio apparire il link
            If Session.Item("LIVELLO") <> "1" Then

                sStringaSql = " select PF_VOCI.* " _
                            & " from SISCOM_MI.PF_VOCI "

                sStringaSql2 = " where PF_VOCI.ID_PIANO_FINANZIARIO=" & vIdPianoFinanziario _
                            & "    and ( PF_VOCI.ID in " _
                                            & " (select ID_VOCE from SISCOM_MI.PF_VOCI_STRUTTURA where " & sFiliale & ")" _
                            & "     or  PF_VOCI.ID_VOCE_MADRE in (select ID_VOCE from siscom_mi.PF_VOCI_STRUTTURA where " & sFiliale & "))" _
                            & "    and PF_VOCI.ID not in " _
                                            & " (select distinct ID_VOCE from SISCOM_MI.PF_VOCI_IMPORTO where ID_LOTTO is not null) and pf_voci.ID_TIPO_UTILIZZO = 1 "


                Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                    Case 6
                        If Session.Item("FL_COMI") <> 1 Then
                            sStringaSql2 = sStringaSql2 & " and PF_VOCI.FL_CC=1 " 'CODICE in ('2.04.01','2.04.04','3.01.01','3.02.01')  "

                        End If
                    Case 7
                        sStringaSql2 = sStringaSql2 & " and PF_VOCI.FL_CC=1 " 'CODICE in ('2.04.01','2.04.04','3.01.01','3.02.01')  "

                End Select

                'sStringaSql2 = "where PF_VOCI.ID_PIANO_FINANZIARIO in " _
                '                        & " (select ID from SISCOM_MI.PF_MAIN where ID_STATO=5 and ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & ")" _
                '            & "   and ( PF_VOCI.ID in " _
                '                            & " (select ID_VOCE from SISCOM_MI.PF_VOCI_OPERATORI " _
                '                            & "  where ID_OPERATORE=" & Id_Operatore & ")" _
                '            & "    or PF_VOCI.ID_VOCE_MADRE in (select ID_VOCE from siscom_mi.PF_VOCI_OPERATORI where ID_OPERATORE=" & Id_Operatore & "))" _
                '            & "   and PF_VOCI.ID not in " _
                '                            & " (select distinct ID_VOCE from SISCOM_MI.PF_VOCI_IMPORTO where ID_LOTTO is not null) "


                'Else
                '    sStringaSql = " select PF_VOCI.* " _
                '            & " from SISCOM_MI.PF_VOCI "

                '    sStringaSql2 = "where PF_VOCI.ID_PIANO_FINANZIARIO in " _
                '                        & " (select ID from SISCOM_MI.PF_MAIN where ID_STATO=5 and ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & ")" _
                '            & "   and PF_VOCI.ID not in " _
                '                        & " (select distinct ID_VOCE from SISCOM_MI.PF_VOCI_IMPORTO where ID_LOTTO is not null)"


                'SELECT * FROM PF_VOCI
                'WHERE ID NOT IN 
                '       (SELECT DISTINCT id_voce FROM PF_VOCI_IMPORTO WHERE id_lotto IS NOT NULL)


                par.cmd.CommandText = sStringaSql & sStringaSql2 & " order by CODICE" '"select * from siscom_mi.pf_voci where id_piano_finanziario=" & idPianoF.Value & " order by codice asc,indice asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'tuttocompleto.value = "0"

                While myReader1.Read

                    'come era prima If par.IfNull(myReader1("CODICE"), "") <> "1.1" And par.IfNull(myReader1("CODICE"), "") <> "1.6" And par.IfNull(myReader1("CODICE"), "") <> "1.7" And par.IfNull(myReader1("CODICE"), "") <> "2.1" And par.IfNull(myReader1("CODICE"), "") <> "2.2.1" And par.IfNull(myReader1("CODICE"), "") <> "2.2.2" And par.IfNull(myReader1("CODICE"), "") <> "2.2.3" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 3) <> "2.4" Then
                    'If Strings.Left(par.IfNull(myReader1("CODICE"), ""), 2) <> "2." Then
                    'If Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.01" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.02" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.03" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.05" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.06" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.07" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 4) <> "2.03" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "1.02.09" Then

                    'And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 4) <> "2.04"

                    'SommaAssestato = par.IfNull(myReader1("valore"), "0") + par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")
                    'SommaAssestato = par.IfNull(myReader1("VALORE_LORDO"), "0") + par.IfNull(myReader1("ASSESTAMENTO_VALORE_LORDO"), "0")

                    'Somma di VALORE_LORDO + ASSESTAMENTO_VALORE_LORDO di PF_VOCI_STRUTTURA dove ID_VOCE alla select 

                    'SOMMA VALORE LORDO
                    par.cmd.CommandText = "select SUM(VALORE_LORDO) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE=" & myReader1("ID")
                    'par.cmd.CommandText = " select SUM(VALORE_LORDO) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                    '                    & " where ID_VOCE in (select ID from  SISCOM_MI.PF_VOCI where ID=" & myReader1("ID") & " or ID_VOCE_MADRE=" & myReader1("ID") & ")"
                    If sFiliale <> "" Then
                        par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                    End If

                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    SommaValoreLordo = 0
                    If myReader2.Read Then
                        SommaValoreLordo = par.IfNull(myReader2(0), 0)
                    End If
                    myReader2.Close()
                    '----------------------------------

                    'SOMMA VALORE ASSESSTATO LORDO
                    par.cmd.CommandText = "select SUM(ASSESTAMENTO_VALORE_LORDO) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE=" & myReader1("ID")
                    'par.cmd.CommandText = "select SUM(ASSESTAMENTO_VALORE_LORDO) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                    '                   & " where ID_VOCE in (select ID from  SISCOM_MI.PF_VOCI where ID=" & myReader1("ID") & " or ID_VOCE_MADRE=" & myReader1("ID") & ")"

                    If sFiliale <> "" Then
                        par.cmd.CommandText = par.cmd.CommandText & "and " & sFiliale
                    End If

                    myReader2 = par.cmd.ExecuteReader()

                    SommaValoreAssestatoLordo = 0
                    If myReader2.Read Then
                        SommaValoreAssestatoLordo = par.IfNull(myReader2(0), 0)
                    End If
                    myReader2.Close()
                    '-----------------------------------


                    'SOMMA VARIAZIONI
                    par.cmd.CommandText = "select SUM(VARIAZIONI) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE=" & myReader1("ID")

                    If sFiliale <> "" Then
                        par.cmd.CommandText = par.cmd.CommandText & "and " & sFiliale
                    End If

                    myReader2 = par.cmd.ExecuteReader()

                    SommaValoreVariazioni = 0
                    If myReader2.Read Then
                        SommaValoreVariazioni = par.IfNull(myReader2(0), 0)
                    End If
                    myReader2.Close()
                    '-----------------------------------

                    SommaAssestato = SommaValoreLordo + SommaValoreAssestatoLordo + SommaValoreVariazioni
                    '*******************************

                    SommaPrenotato = 0
                    SommaConsuntivato = 0
                    SommaLiquidato = 0

                    sStringaSql = "select PF_VOCI.* " _
                               & " from SISCOM_MI.PF_VOCI "

                    sStringaSql = sStringaSql & sStringaSql2 & " and ID_VOCE_MADRE=" & myReader1("ID") & " order by CODICE"

                    par.cmd.CommandText = sStringaSql
                    myReader2 = par.cmd.ExecuteReader()


                    If myReader2.Read Then
                        Seleziona = "&nbsp;"
                        myReader2.Close()


                        SommaAssestato = 0
                        SommaResiduo = 0
                        SommaPrenotato = 0
                        SommaConsuntivato = 0
                        SommaLiquidato = 0

                        sStringaSql = "select PF_VOCI.* " _
                             & " from SISCOM_MI.PF_VOCI "

                        sStringaSql = sStringaSql & sStringaSql2 & " and ID_VOCE_MADRE=" & myReader1("ID") & " order by CODICE"

                        par.cmd.CommandText = sStringaSql
                        myReader2 = par.cmd.ExecuteReader()

                        While myReader2.Read
                            'SommaAssestato = SommaAssestato + par.IfNull(myReader2("VALORE_LORDO"), "0") + par.IfNull(myReader2("ASSESTAMENTO_VALORE_LORDO"), "0")

                            'Somma di VALORE_LORDO + ASSESTAMENTO_VALORE_LORDO di PF_VOCI_STRUTTURA dove ID_VOCE alla select 

                            'If par.IfNull(myReader1("codice"), "") = "1.02.10" Then
                            '    Beep()
                            'End If

                            'SOMMA_LORDO
                            par.cmd.CommandText = "select SUM(VALORE_LORDO) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE=" & myReader2("ID")
                            'par.cmd.CommandText = " select SUM(VALORE_LORDO) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                            '                    & " where ID_VOCE in (select ID from  SISCOM_MI.PF_VOCI where ID=" & myReader1("ID") & " or ID_VOCE_MADRE=" & myReader1("ID") & ")"

                            If sFiliale <> "" Then
                                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                            End If

                            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                            SommaValoreLordo = 0
                            If myReader3.Read Then
                                SommaValoreLordo = par.IfNull(myReader3(0), 0)
                            End If
                            myReader3.Close()

                            'SOMMA_LORDO_ASSESSTATO
                            par.cmd.CommandText = "select SUM(ASSESTAMENTO_VALORE_LORDO) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE=" & myReader2("ID")
                            'par.cmd.CommandText = "select SUM(ASSESTAMENTO_VALORE_LORDO) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                            '                   & " where ID_VOCE in (select ID from  SISCOM_MI.PF_VOCI where ID=" & myReader1("ID") & " or ID_VOCE_MADRE=" & myReader1("ID") & ")"

                            If sFiliale <> "" Then
                                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                            End If

                            myReader3 = par.cmd.ExecuteReader()

                            SommaValoreAssestatoLordo = 0
                            If myReader3.Read Then
                                SommaValoreAssestatoLordo = par.IfNull(myReader3(0), 0)
                            End If
                            myReader3.Close()

                            'VARIAZIONI
                            par.cmd.CommandText = "select SUM(VARIAZIONI) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE=" & myReader2("ID")

                            If sFiliale <> "" Then
                                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                            End If

                            myReader3 = par.cmd.ExecuteReader()

                            SommaValoreVariazioni = 0
                            If myReader3.Read Then
                                SommaValoreVariazioni = par.IfNull(myReader3(0), 0)
                            End If
                            myReader3.Close()

                            SommaAssestato = SommaAssestato + SommaValoreLordo + SommaValoreAssestatoLordo + SommaValoreVariazioni
                            '*******************************


                            'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
                            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO)) " _
                                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                                & " where   ID_STATO=0 " _
                                                & "   and   ID_PAGAMENTO is null " _
                                                & "   and   ID_VOCE_PF in (select ID from  SISCOM_MI.PF_VOCI where ID=" & myReader2("ID") & " or ID_VOCE_MADRE=" & myReader2("ID") & ")" ' ID_VOCE_PF=" & myReader2("ID") 
                            If sFiliale <> "" Then
                                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                            End If

                            myReader3 = par.cmd.ExecuteReader()
                            If myReader3.Read Then
                                'SommaPrenotato = par.IfNull(myReader3(0), 0)
                                sRisultato = par.IfNull(myReader3(0), "0")
                                SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
                            End If
                            myReader3.Close()
                            '**********************************


                            'IMPORTO PRENOTATO [EMESSO o APPROVATO]
                            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO)) " _
                                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                                & " where   ID_STATO=1 " _
                                                & "   and   ID_PAGAMENTO is null " _
                                                & "   and   ID_VOCE_PF in (select ID from  SISCOM_MI.PF_VOCI where ID=" & myReader2("ID") & " or ID_VOCE_MADRE=" & myReader2("ID") & ")" ' ID_VOCE_PF=" & myReader2("ID") 

                            If sFiliale <> "" Then
                                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                            End If

                            myReader3 = par.cmd.ExecuteReader()
                            If myReader3.Read Then
                                sRisultato = par.IfNull(myReader3(0), "0")
                                SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
                            End If
                            myReader3.Close()
                            '********************************


                            'IMPORTO PRENOTATO [EMESSO o APPROVATO]
                            'par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                            '                    & " from    SISCOM_MI.PRENOTAZIONI " _
                            '                    & " where   ID_STATO=2 " _
                            '                    & "   and   ID_PAGAMENTO is not null " _
                            '                    & "   and   ID_VOCE_PF in (select ID from  SISCOM_MI.PF_VOCI where ID=" & myReader2("ID") & " or ID_VOCE_MADRE=" & myReader2("ID") & ")" ' ID_VOCE_PF=" & myReader2("ID") 

                            'If sFiliale <> "" Then
                            '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                            'End If

                            'myReader3 = par.cmd.ExecuteReader()
                            'If myReader3.Read Then
                            '    sRisultato = par.IfNull(myReader3(0), "0")
                            '    SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
                            'End If
                            'myReader3.Close()
                            '*********************************


                            'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
                            par.cmd.CommandText = "select SUM(IMPORTO_APPROVATO) " _
                                                & " from   SISCOM_MI.PRENOTAZIONI " _
                                                & " where  ID_PAGAMENTO is NOT null " _
                                                & "   and  ID_STATO>1 " _
                                                & "   and   ID_VOCE_PF in (select ID from  SISCOM_MI.PF_VOCI where ID=" & myReader2("ID") & " or ID_VOCE_MADRE=" & myReader2("ID") & ")" ' ID_VOCE_PF=" & myReader2("ID") 

                            If sFiliale <> "" Then
                                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                            End If

                            myReader3 = par.cmd.ExecuteReader
                            If myReader3.Read Then
                                SommaConsuntivato = SommaConsuntivato + par.IfNull(myReader3(0), 0)
                            End If
                            myReader3.Close()

                            'IMPORTO LIQUIDATO
                            'par.cmd.CommandText = "select  * from SISCOM_MI.PAGAMENTI " _
                            '                   & "  where  PAGAMENTI.ID in (select ID_PAGAMENTO " _
                            '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
                            '                                            & " where  ID_VOCE_PF in (select ID from  SISCOM_MI.PF_VOCI where ID=" & myReader2("ID") & " or ID_VOCE_MADRE=" & myReader2("ID") & ")" _
                            '                                            & "   and  ID_STATO>1 "

                            par.cmd.CommandText = "select SUM(IMPORTO) " _
                                               & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                                               & "  where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                                                                        & " from   SISCOM_MI.PRENOTAZIONI " _
                                                                        & " where  ID_VOCE_PF in (select ID from  SISCOM_MI.PF_VOCI where ID=" & myReader2("ID") & " or ID_VOCE_MADRE=" & myReader2("ID") & ")" _
                                                                        & "   and  ID_STATO>1 "

                            If sFiliale <> "" Then
                                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale & ")" '& " or ID_STRUTTURA is Null) )"
                            Else
                                par.cmd.CommandText = par.cmd.CommandText & ")"
                            End If
                            'par.cmd.CommandText = "select  IMPORTO_CONSUNTIVATO,ID_STATO" _
                            '                    & " from SISCOM_MI.PAGAMENTI where PAGAMENTI.ID_VOCE_PF=" & myReader2("ID")

                            myReader3 = par.cmd.ExecuteReader()

                            If myReader3.Read Then
                                SommaLiquidato = SommaLiquidato + par.IfNull(myReader3(0), 0)

                                'Select Case par.IfNull(myReader3("ID_STATO"), 0)
                                '    'Case 0
                                '    '    SommaPrenotato = SommaPrenotato + par.IfNull(myReader3("IMPORTO_PRENOTATO"), 0)
                                '    'Case 1
                                '    '    SommaConsuntivato = SommaConsuntivato + par.IfNull(myReader3("IMPORTO_CONSUNTIVATO"), 0)
                                '    Case 5
                                '        SommaLiquidato = SommaLiquidato + par.IfNull(myReader3("IMPORTO_CONSUNTIVATO"), 0)
                                'End Select

                            End If
                            myReader3.Close()

                            Dim sommax As Decimal = 0
                            sommax = SommaConsuntivato - SommaLiquidato
                            'SommaConsuntivato = SommaConsuntivato - SommaLiquidato

                            'MODIFICA DEL 29 APR 2011 forse non serve
                            'SommaPrenotato = SommaPrenotato - SommaConsuntivato

                            SommaResiduo = (SommaAssestato - (SommaPrenotato + sommax + SommaLiquidato))
                        End While
                        myReader2.Close()

                    Else

                        myReader2.Close()

                        'If par.IfNull(myReader1("codice"), "") = "1.02.10" Then
                        '    Beep()
                        'End If

                        'IMPORTO PRENOTATO [EMESSO o APPROVATO]
                        par.cmd.CommandText = " select  SUM(IMPORTO_PRENOTATO) " _
                                            & " from    SISCOM_MI.PRENOTAZIONI " _
                                            & " where   ID_STATO=0 " _
                                            & "   and   ID_PAGAMENTO is null " _
                                            & "   and   ID_VOCE_PF=" & myReader1("ID")
                        If sFiliale <> "" Then
                            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                        End If

                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader3.Read Then
                            SommaPrenotato = par.IfNull(myReader3(0), 0)
                        End If
                        myReader3.Close()


                        'IMPORTO PRENOTATO [EMESSO o APPROVATO]
                        par.cmd.CommandText = " select  SUM(IMPORTO_APPROVATO) " _
                                            & " from    SISCOM_MI.PRENOTAZIONI " _
                                            & " where   ID_STATO=1 " _
                                            & "   and   ID_PAGAMENTO is null " _
                                            & "   and   ID_VOCE_PF=" & myReader1("ID")
                        If sFiliale <> "" Then
                            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                        End If

                        myReader3 = par.cmd.ExecuteReader()
                        If myReader3.Read Then
                            SommaPrenotato = SommaPrenotato + par.IfNull(myReader3(0), 0)
                        End If
                        myReader3.Close()


                        'IMPORTO PRENOTATO [EMESSO o APPROVATO]
                        'par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                        '                    & " from    SISCOM_MI.PRENOTAZIONI " _
                        '                    & " where   ID_STATO=2 " _
                        '                    & "   and   ID_PAGAMENTO is not null " _
                        '                    & "   and   ID_VOCE_PF=" & myReader1("ID")
                        'If sFiliale <> "" Then
                        '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                        'End If

                        'myReader3 = par.cmd.ExecuteReader()
                        'If myReader3.Read Then
                        '    sRisultato = par.IfNull(myReader3(0), "0")
                        '    SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
                        'End If
                        'myReader3.Close()
                        '*********************************


                        'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
                        par.cmd.CommandText = "select SUM(IMPORTO_APPROVATO) " _
                                            & " from   SISCOM_MI.PRENOTAZIONI " _
                                            & " where  ID_PAGAMENTO is NOT null" _
                                                & "   and  ID_STATO>1 " _
                                                & "   and   ID_VOCE_PF=" & myReader1("ID")

                        If sFiliale <> "" Then
                            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                        End If


                        myReader3 = par.cmd.ExecuteReader
                        If myReader3.Read Then
                            SommaConsuntivato = par.IfNull(myReader3(0), 0)
                        End If
                        myReader3.Close()

                        ''IMPORTO CONSUNTIVATO e LIQUIDATO
                        'par.cmd.CommandText = "select  * from SISCOM_MI.PAGAMENTI " _
                        '                   & "  where  PAGAMENTI.ID in (select ID_PAGAMENTO " _
                        '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
                        '                                            & " where  ID_VOCE_PF=" & myReader1("ID") _
                        '                                            & "   and  ID_STATO>1 "

                        par.cmd.CommandText = "select SUM(IMPORTO) " _
                                          & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                                          & " where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                                                                 & " from   SISCOM_MI.PRENOTAZIONI " _
                                                                 & " where  ID_VOCE_PF=" & myReader1("ID") _
                                                                 & "   and  ID_STATO>1 "


                        If sFiliale <> "" Then
                            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale & ")" '& " or ID_STRUTTURA is Null)) "
                        Else
                            par.cmd.CommandText = par.cmd.CommandText & ")"
                        End If

                        'par.cmd.CommandText = "select  IMPORTO_CONSUNTIVATO,ID_STATO" _
                        '                    & " from SISCOM_MI.PAGAMENTI where PAGAMENTI.ID_VOCE_PF=" & myReader1("ID")

                        myReader3 = par.cmd.ExecuteReader()

                        If myReader3.Read Then
                            SommaLiquidato = par.IfNull(myReader3(0), 0)
                            'Select Case par.IfNull(myReader3("ID_STATO"), 0)
                            '    'Case 0
                            '    '    SommaPrenotato = SommaPrenotato + par.IfNull(myReader3("IMPORTO_PRENOTATO"), 0)
                            '    'Case 1
                            '    '    SommaConsuntivato = SommaConsuntivato + par.IfNull(myReader3("IMPORTO_CONSUNTIVATO"), 0)
                            '    Case 5
                            '        SommaLiquidato = SommaLiquidato + par.IfNull(myReader3("IMPORTO_CONSUNTIVATO"), 0)
                            'End Select

                        End If
                        myReader3.Close()

                        'MODIFICA DEL 29 APR 2011 forse non serve
                        'SommaPrenotato = SommaPrenotato - SommaConsuntivato

                        SommaConsuntivato = SommaConsuntivato - SommaLiquidato

                        SommaResiduo = SommaAssestato - (SommaPrenotato + SommaConsuntivato + SommaLiquidato)

                        If x.Value = "1" Then
                            Session.Add("ID", 0)
                            Seleziona = "<a href='Pagamenti.aspx?X=1&V=" & par.IfNull(myReader1("ID"), "") & "&EF_R=" & vIdPianoFinanziario & "&ST=0' >Seleziona</a>"
                        Else
                            Session.Add("ID", 0)
                            Seleziona = "<a href='Pagamenti.aspx?V=" & par.IfNull(myReader1("ID"), "") & "&EF_R=" & vIdPianoFinanziario & "&ST=0' >Seleziona</a>"
                        End If
                    End If


                    If Seleziona = "&nbsp;" Then
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: Segoe UI; font-size: 8pt font-bold: bold'>" & par.IfNull(myReader1("codice"), "") & "  " & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: Segoe UI; font-size: 8pt font-bold: bold'>" & par.IfNull(myReader1("descrizione"), "") & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: Segoe UI; font-size: 8pt' align='right'>" & Format(SommaAssestato, "##,##0.00") & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: Segoe UI; font-size: 8pt' align='right'>" & Format(SommaResiduo, "##,##0.00") & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: Segoe UI; font-size: 8pt' align='right'>" & Seleziona & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & immagine & "</td>" _
                                              & "</tr>"

                    Else
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: Segoe UI; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "  " & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: Segoe UI; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: Segoe UI; font-size: 8pt' align='right'>" & Format(SommaAssestato, "##,##0.00") & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: Segoe UI; font-size: 8pt' align='right'>" & Format(SommaResiduo, "##,##0.00") & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: Segoe UI; font-size: 8pt' align='right'>" & Seleziona & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & immagine & "</td>" _
                                              & "</tr>"

                    End If
                    'End If
                End While
                myReader1.Close()

                TestoPagina = TestoPagina & "</table>"

                Tabella = TestoPagina


                'par.cmd.CommandText = "select * from operatori where id=" & Session.Item("ID_OPERATORE")
                'myReader1 = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                '    If par.IfNull(myReader1("BP_CONV_ALER_L"), "1") = "1" Then
                '        'imgConvalida.Visible = False
                '    End If
                'End If
                'myReader1.Close()

            End If


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try

    End Sub
 Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        If x.Value = "1" Then
            Response.Write("<script language='javascript'> { self.close(); }</script>")
        Else

            Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
        End If
    End Sub

    Public Property tipo() As String
        Get
            If Not (ViewState("par_tipo") Is Nothing) Then
                Return CStr(ViewState("par_tipo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipo") = value
        End Set

    End Property



    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function


    'CARICO COMBO ESERCIZIO FINANZIARIO
    Private Sub CaricaEsercizio()
        Dim FlagConnessione As Boolean
        Dim i As Integer = 0
        Dim ID_ANNO_EF_CORRENTE As Long = -1

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                               & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                               & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                               & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO" _
                               & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID ORDER BY T_ESERCIZIO_FINANZIARIO.ID desc "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                ' Me.cmbEsercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))
                vIdPianoFinanziario = par.IfNull(myReader1("ID"), -1)
            End While
            myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO", False)

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

            Select Case i
                Case 0
                    Me.cmbEsercizio.Items.Clear()
                    ' Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
                    Me.cmbEsercizio.Enabled = False
                Case 1
                    Me.cmbEsercizio.Items(0).Selected = True
                    Me.cmbEsercizio.Enabled = False

            End Select

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                    vIdPianoFinanziario = ID_ANNO_EF_CORRENTE
                End If
                RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)
        CaricaTabella()
    End Sub

    'RICAVA LO STATO DELL'ESERCIZIO SELEZIONATO (5,6,7)
    Private Sub RicavaStatoEsercizioFinanaziario(ByVal ID_ESERCIZIO As Long)
        Dim FlagConnessione As Boolean

        Try

            Me.txtSTATO_PF.Value = -1

            If ID_ESERCIZIO < 0 Then Exit Sub

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            par.cmd.CommandText = "select * from SISCOM_MI.PF_MAIN " _
                               & " where PF_MAIN.ID=" & ID_ESERCIZIO

            Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderF.Read Then
                Me.txtSTATO_PF.Value = par.IfNull(myReaderF("ID_STATO"), -1)
            End If
            myReaderF.Close()

            par.cmd.Parameters.Clear()

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            ' Me.txtSTATO_PF.Value = -1

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

   
End Class
