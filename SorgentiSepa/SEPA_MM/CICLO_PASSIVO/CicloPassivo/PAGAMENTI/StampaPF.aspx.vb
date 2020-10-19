Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class CicloPassivo_Pagamenti_StampaPF
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim IDP As String = "0"
    Dim TestoPagina As String = ""

    Public sValoreEsercizioFinanziarioR As String

    Public SommaBungetCapTOT As Decimal = 0
    Public SommaAssestatoCapTOT As Decimal = 0
    Public SommaPrenotatoCapTOT As Decimal = 0
    Public SommaConsuntivatoCapTOT As Decimal = 0
    Public SommaLiquidatoCapTOT As Decimal = 0
    Public SommaResiduoCapTOT As Decimal = 0
    Public SommaVariazioniCapTOT As Decimal = 0


    Public SommaBungetTOT As Decimal = 0
    Public SommaAssestatoTOT As Decimal = 0
    Public SommaPrenotatoTOT As Decimal = 0
    Public SommaConsuntivatoTOT As Decimal = 0
    Public SommaLiquidatoTOT As Decimal = 0
    Public SommaResiduoTOT As Decimal = 0
    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        If IsPostBack = False Then
            IDP = Request.QueryString("ID")

            Dim Contatore As Integer = 1
            Dim Capitolo1 As Boolean = False
            Dim Capitolo2 As Boolean = False
            Dim Capitolo3 As Boolean = False
            Dim Capitolo4 As Boolean = False
            Dim Capitolo5 As Boolean = False

            Dim Id_Operatore As String
            Dim sStringaSql As String
            Dim sStringaSql2 As String

            Dim SommaBudget As Decimal = 0
            Dim SommaAssestato As Decimal = 0
            Dim SommaPrenotato As Decimal = 0
            Dim SommaConsuntivato As Decimal = 0
            Dim SommaLiquidato As Decimal = 0
            Dim SommaResiduo As Decimal = 0
            Dim SommaVariazioni As Decimal = 0

            Dim sDataEsercizio As String = ""
            Dim sRisultato As String = ""

            Dim ElencoPF_VOCI_ID As String

            Dim FlagConnessione As Boolean
            Dim STATO_PF As Integer

            Try
                Contatore = 1

                '*******************APERURA CONNESSIONE*********************
                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Dim sFiliale As String = ""
                'If Session.Item("LIVELLO") <> "1" Then
                If Request.QueryString("CHIAMANTE") = "STAMPA_DETTAGLIO" Then
                    sFiliale = "  ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                Else
                    If Strings.Trim(Request.QueryString("STR")) <> "-1" Then
                        sFiliale = "  ID_STRUTTURA=" & Strings.Trim(Request.QueryString("STR"))
                    End If
                End If
                'End If

                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

                sStringaSql = "select * from SISCOM_MI.T_ESERCIZIO_FINANZIARIO where ID=( select ID_ESERCIZIO_FINANZIARIO from SISCOM_MI.PF_MAIN where ID=" & sValoreEsercizioFinanziarioR & ") "
                par.cmd.CommandText = sStringaSql
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    sDataEsercizio = " DAL " & par.FormattaData(par.IfNull(myReader1("INIZIO"), ""))
                    sDataEsercizio = sDataEsercizio & " AL " & Format(Now, "dd/MM/yyyy") ' par.FormattaData(par.IfNull(myReader1("FINE"), ""))
                    STATO_PF = RicavaStatoEsercizioFinanaziario(par.IfNull(myReader1("ID"), "-1"))
                End If
                myReader1.Close()


                sStringaSql = "select NOME from SISCOM_MI.TAB_FILIALI where ID=" & par.IfEmpty(Strings.Trim(Request.QueryString("STR")), -1)
                par.cmd.CommandText = sStringaSql
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    sDataEsercizio = sDataEsercizio & " <br> " & par.IfNull(myReader1("NOME"), "")
                End If
                myReader1.Close()


                Id_Operatore = Session.Item("ID_OPERATORE") '221

                TestoPagina = "<p style='font-family: ARIAL; font-size: 14pt; font-weight: bold; text-align: center;'>SITUAZIONE CONTABILE: " & sDataEsercizio & "</p></br>"

                TestoPagina = TestoPagina & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                          & "<td align='left'  style='border-bottom-style: dashed; width:7%;  border-bottom-width: 1px; border-bottom-color: #000000'>COD.</td>" _
                                          & "<td align='left'  style='border-bottom-style: dashed; width:33%; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:11%; border-bottom-width: 1px; border-bottom-color: #000000'>BUDGET INIZIALE</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:11%; border-bottom-width: 1px; border-bottom-color: #000000'>BUDGET ASSESTATO + VAR.</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>DISPONIBILITA' RESIDUA</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE     PRENOTATO</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE     CONSUNTIVATO</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width:8%; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE             PAGATO</td>" _
                                          & "</tr>"


                'If Session.Item("LIVELLO") <> "1" Then
                sStringaSql = " select PF_VOCI.* " _
                            & " from SISCOM_MI.PF_VOCI "

                sStringaSql2 = "where PF_VOCI.ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR

                If sFiliale <> "" Then
                    sStringaSql2 = sStringaSql2 & "   and ( PF_VOCI.ID in " _
                                            & " (select ID_VOCE from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                            & "  where " & sFiliale & ")" _
                            & "    or PF_VOCI.ID_VOCE_MADRE in (select ID_VOCE from siscom_mi.PF_VOCI_STRUTTURA where " & sFiliale & "))"

                    If Request.QueryString("VOCI") = "False" Or Request.QueryString("VOCI") = "on" Then
                        sStringaSql2 = sStringaSql2 & " and length(PF_VOCI.CODICE)<10 "
                    End If

                ElseIf Request.QueryString("CHIAMANTE") = "STAMPA_DETTAGLIO" Then
                    sStringaSql2 = sStringaSql2 & "   and PF_VOCI.ID not in " _
                                            & " (select distinct ID_VOCE from SISCOM_MI.PF_VOCI_IMPORTO where ID_LOTTO is not null) "

                    If Request.QueryString("VOCI") = "False" Or Request.QueryString("VOCI") = "on" Then
                        sStringaSql2 = sStringaSql2 & " and length(PF_VOCI.CODICE)<10 "
                    End If

                End If

                Select Case par.IfEmpty(STATO_PF, 5)
                    Case 6
                        If Session.Item("FL_COMI") <> 1 Then
                            sStringaSql2 = sStringaSql2 & " and  PF_VOCI.FL_CC=1 "
                        End If
                    Case 7
                        sStringaSql2 = sStringaSql2 & " and PF_VOCI.FL_CC=1  "

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

                'NOTA GIUSEPPE: Da togliere il commento alla voce sopra ed eliminare la sStringaSql di sotto 

                'sStringaSql = " select PF_VOCI.* " _
                '        & " from SISCOM_MI.PF_VOCI "

                'sStringaSql2 = "where PF_VOCI.ID_PIANO_FINANZIARIO in " _
                '                    & " (select ID from SISCOM_MI.PF_MAIN where ID_STATO=1 and ID_ESERCIZIO_FINANZIARIO=26)" _
                '        & "   and PF_VOCI.ID not in " _
                '                    & " (select distinct ID_VOCE from SISCOM_MI.PF_VOCI_IMPORTO where ID_LOTTO is not null)"



                par.cmd.CommandText = sStringaSql & sStringaSql2 & " order by CODICE"
                myReader1 = par.cmd.ExecuteReader()

                While myReader1.Read

                    '**** SOMMA IMPORTI x RIGA

                    'If par.IfNull(myReader1("CODICE"), "") <> "1.1" And par.IfNull(myReader1("CODICE"), "") <> "1.6" And par.IfNull(myReader1("CODICE"), "") <> "1.7" And par.IfNull(myReader1("CODICE"), "") <> "2.1" And par.IfNull(myReader1("CODICE"), "") <> "2.2.1" And par.IfNull(myReader1("CODICE"), "") <> "2.2.2" And par.IfNull(myReader1("CODICE"), "") <> "2.2.3" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 3) <> "2.4" Then
                    'come era prima If par.IfNull(myReader1("CODICE"), "") <> "1.1" And par.IfNull(myReader1("CODICE"), "") <> "1.6" And par.IfNull(myReader1("CODICE"), "") <> "1.7" And par.IfNull(myReader1("CODICE"), "") <> "2.1" And par.IfNull(myReader1("CODICE"), "") <> "2.2.1" And par.IfNull(myReader1("CODICE"), "") <> "2.2.2" And par.IfNull(myReader1("CODICE"), "") <> "2.2.3" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 3) <> "2.4" Then
                    'If (Strings.Left(par.IfNull(myReader1("CODICE"), ""), 2) <> "2." And Request.QueryString("CHIAMANTE") = "STAMPA_DETTAGLIO") Or Request.QueryString("CHIAMANTE") = "STAMPA_GENERALE" Then
                    If (Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.01" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.02" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.03" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.05" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.06" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "2.02.07" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 4) <> "2.03" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 4) <> "2.04" And Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) <> "1.02.09" And Request.QueryString("CHIAMANTE") = "STAMPA_DETTAGLIO") Or Request.QueryString("CHIAMANTE") = "STAMPA_GENERALE" Then


                        SommaBudget = 0
                        SommaAssestato = 0
                        SommaPrenotato = 0
                        SommaConsuntivato = 0
                        SommaLiquidato = 0
                        SommaVariazioni = 0

                        'ESTRAGGO per ogni VOCE, la sua VOVCE + tutte le VOCI figlie di eventuali sottofigli
                        ElencoPF_VOCI_ID = ""
                        par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI " _
                                          & "  where ID=" & myReader1("ID") _
                                          & "     or ID_VOCE_MADRE=" & myReader1("ID") _
                                          & "     or ID_VOCE_MADRE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader1("ID") & ") order by CODICE"

                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader2.Read
                            If ElencoPF_VOCI_ID = "" Then
                                ElencoPF_VOCI_ID = par.IfNull(myReader2(0), "")
                            Else
                                ElencoPF_VOCI_ID = ElencoPF_VOCI_ID & "," & par.IfNull(myReader2(0), "")
                            End If

                        End While
                        myReader2.Close()


                        'X RIGA *********************************************************
                        'SOMMA VALORE LORDO to_char(SUM(VALORE_LORDO))
                        par.cmd.CommandText = "select ID_VOCE,to_char(VALORE_LORDO)  as VALORE_LORDO from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")
                        If sFiliale <> "" Then
                            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                        End If

                        SommaBudget = 0

                        myReader2 = par.cmd.ExecuteReader()
                        While myReader2.Read

                            par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader3.Read Then

                            Else
                                sRisultato = par.IfNull(myReader2("VALORE_LORDO"), "0")
                                SommaBudget = SommaBudget + Decimal.Parse(sRisultato)
                            End If
                            myReader3.Close()
                        End While
                        myReader2.Close()
                        '----------------------------------

                        'SOMMA VALORE ASSESSTATO LORDO
                        par.cmd.CommandText = "select ID_VOCE,to_char(ASSESTAMENTO_VALORE_LORDO) as ASSESTAMENTO_VALORE_LORDO from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")
                        If sFiliale <> "" Then
                            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                        End If

                        SommaAssestato = 0

                        myReader2 = par.cmd.ExecuteReader()
                        While myReader2.Read

                            par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader3.Read Then

                            Else
                                sRisultato = par.IfNull(myReader2("ASSESTAMENTO_VALORE_LORDO"), "0")
                                SommaAssestato = SommaAssestato + Decimal.Parse(sRisultato)
                            End If
                            myReader3.Close()

                        End While
                        myReader2.Close()
                        '----------------------------------

                        'SOMMA VARIAZIONI
                        par.cmd.CommandText = "select ID_VOCE,to_char(VARIAZIONI) as VARIAZIONI from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")
                        If sFiliale <> "" Then
                            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                        End If

                        SommaVariazioni = 0

                        myReader2 = par.cmd.ExecuteReader()
                        While myReader2.Read

                            par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader3.Read Then

                            Else
                                sRisultato = par.IfNull(myReader2("VARIAZIONI"), "0")
                                SommaVariazioni = SommaVariazioni + Decimal.Parse(sRisultato)
                            End If
                            myReader3.Close()

                        End While
                        myReader2.Close()
                        '----------------------------------

                        SommaAssestato = SommaBudget + SommaAssestato + SommaVariazioni
                        'SommaAssestato = par.IfNull(myReader1("VALORE_LORDO"), "0") + par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")
                        '*******************************

                        SommaPrenotato = 0
                        SommaConsuntivato = 0
                        SommaLiquidato = 0


                        'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
                        par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO)) " _
                                            & " from    SISCOM_MI.PRENOTAZIONI " _
                                            & " where   (ID_STATO=0 or ID_STATO=1) " _
                                            & "   and   ID_PAGAMENTO is null " _
                                            & "   and   ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")
                        If sFiliale <> "" Then
                            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                        End If

                        myReader2 = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            sRisultato = par.IfNull(myReader2(0), "0")
                            SommaPrenotato = Decimal.Parse(sRisultato)
                        End If
                        myReader2.Close()
                        '********************************


                        'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
                        par.cmd.CommandText = "select to_char(SUM(IMPORTO_APPROVATO)) " _
                                            & " from   SISCOM_MI.PRENOTAZIONI " _
                                            & " where  ID_PAGAMENTO is NOT null " _
                                            & "   and  ID_STATO>1 " _
                                            & "   and  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

                        If sFiliale <> "" Then
                            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                        End If

                        myReader2 = par.cmd.ExecuteReader
                        If myReader2.Read Then
                            sRisultato = par.IfNull(myReader2(0), "0")
                            SommaConsuntivato = Decimal.Parse(sRisultato)
                        End If
                        myReader2.Close()


                        'IMPORTO LIQUIDATO
                        par.cmd.Parameters.Clear()
                        'par.cmd.CommandText = "select to_char(SUM(IMPORTO_CONSUNTIVATO) ) " _
                        '                   & " from SISCOM_MI.PAGAMENTI " _
                        '                   & "  where DATA_STAMPA is NOT null " _
                        '                   & "    and ID_STATO=5 "
                        '                   & "    and PAGAMENTI.ID in (select ID_PAGAMENTO " 

                        par.cmd.CommandText = "select to_char(SUM(IMPORTO)) " _
                                           & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                                           & " where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                                                                   & " from   SISCOM_MI.PRENOTAZIONI " _
                                                                   & " where  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" _
                                                                   & "   and  ID_STATO>=2"
                        If sFiliale <> "" Then
                            par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale & ")"
                        Else
                            par.cmd.CommandText = par.cmd.CommandText & ")"
                        End If

                        myReader2 = par.cmd.ExecuteReader
                        If myReader2.Read Then
                            sRisultato = par.IfNull(myReader2(0), "0")
                            SommaLiquidato = Decimal.Parse(sRisultato)
                        End If
                        myReader2.Close()

                        SommaConsuntivato = SommaConsuntivato - SommaLiquidato
                        'X RIGA FINE *********************************************************


                        'X CAPITOLO e TOTALE REPORT di TUTTO ****************************************

                        'SOMMA VALORE LORDO 
                        'par.cmd.CommandText = " select to_char(SUM(VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                        '                    & " where ID_VOCE=" & myReader1("ID") _
                        '                    & "   and ID_VOCE NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader1("ID") & ")"
                        'If sFiliale <> "" Then
                        '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                        'End If

                        'myReader2 = par.cmd.ExecuteReader()
                        'If myReader2.Read Then
                        '    sRisultato = par.IfNull(myReader2(0), "0")

                        '    SommaBungetCapTOT = SommaBungetCapTOT + Decimal.Parse(sRisultato)    'x CAPITOLO
                        '    SommaBungetTOT = SommaBungetTOT + Decimal.Parse(sRisultato)          'TOTALE REPORT
                        'End If
                        'myReader2.Close()
                        ''----------------------------------

                        ''SOMMA VALORE ASSESSTATO LORDO
                        'par.cmd.CommandText = " select to_char(SUM(ASSESTAMENTO_VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                        '                    & " where ID_VOCE=" & myReader1("ID") _
                        '                    & "   and ID_VOCE NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader1("ID") & ")"

                        'If sFiliale <> "" Then
                        '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                        'End If

                        'myReader2 = par.cmd.ExecuteReader()
                        'If myReader2.Read Then
                        '    sRisultato = par.IfNull(myReader2(0), "0")

                        '    SommaAssestatoCapTOT = Decimal.Parse(sRisultato)
                        '    SommaAssestatoTOT = Decimal.Parse(sRisultato)
                        'End If
                        'myReader2.Close()

                        'SommaAssestatoCapTOT = SommaBungetCapTOT + SommaAssestatoCapTOT     'x CAPITOLO
                        'SommaAssestatoTOT = SommaBungetTOT + SommaAssestatoTOT              'TOTALE REPORT
                        ''*******************************

                        ''IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
                        'par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO)) " _
                        '                    & " from    SISCOM_MI.PRENOTAZIONI " _
                        '                    & " where   (ID_STATO=0 or ID_STATO=1) " _
                        '                    & "   and   ID_PAGAMENTO is null " _
                        '                    & "   and   ID_VOCE_PF=" & myReader1("ID") _
                        '                    & "   and   ID_VOCE_PF NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader1("ID") & ")"
                        'If sFiliale <> "" Then
                        '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                        'End If

                        'myReader2 = par.cmd.ExecuteReader()
                        'If myReader2.Read Then
                        '    sRisultato = par.IfNull(myReader2(0), "0")

                        '    SommaPrenotatoCapTOT = SommaPrenotatoCapTOT + Decimal.Parse(sRisultato) 'x CAPITOLO
                        '    SommaPrenotatoTOT = SommaPrenotatoTOT + Decimal.Parse(sRisultato)       'TOTALE REPORT
                        'End If
                        'myReader2.Close()
                        ''********************************


                        ''IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
                        'par.cmd.CommandText = "select to_char(SUM(IMPORTO_APPROVATO)) " _
                        '                    & " from   SISCOM_MI.PRENOTAZIONI " _
                        '                    & " where  ID_PAGAMENTO is NOT null " _
                        '                    & "   and  ID_STATO>1 " _
                        '                    & "   and  ID_VOCE_PF=" & myReader1("ID") _
                        '                    & "   and   ID_VOCE_PF NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader1("ID") & ")"

                        'If sFiliale <> "" Then
                        '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
                        'End If

                        'myReader2 = par.cmd.ExecuteReader
                        'If myReader2.Read Then
                        '    sRisultato = par.IfNull(myReader2(0), "0")

                        '    SommaConsuntivatoCapTOT = SommaConsuntivatoCapTOT + Decimal.Parse(sRisultato)   'x CAPITOLO
                        '    SommaConsuntivatoTOT = SommaConsuntivatoTOT + Decimal.Parse(sRisultato)         'TOTALE REPORT
                        'End If
                        'myReader2.Close()


                        ''IMPORTO LIQUIDATO
                        'par.cmd.Parameters.Clear()
                        'par.cmd.CommandText = "select to_char(SUM(IMPORTO_CONSUNTIVATO) ) " _
                        '                   & " from SISCOM_MI.PAGAMENTI " _
                        '                   & "  where DATA_STAMPA is NOT null " _
                        '                   & "    and ID_STATO=5 " _
                        '                   & "    and PAGAMENTI.ID in (select ID_PAGAMENTO " _
                        '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
                        '                                            & " where  ID_VOCE_PF=" & myReader1("ID") _
                        '                                            & "   and  ID_VOCE_PF NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader1("ID") & ")" _
                        '                                            & "   and  ID_STATO>=2"
                        'If sFiliale <> "" Then
                        '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale & ")"
                        'Else
                        '    par.cmd.CommandText = par.cmd.CommandText & ")"
                        'End If

                        'myReader2 = par.cmd.ExecuteReader
                        'If myReader2.Read Then
                        '    sRisultato = par.IfNull(myReader2(0), "0")

                        '    SommaLiquidatoCapTOT = SommaLiquidatoCapTOT + Decimal.Parse(sRisultato)
                        '    SommaLiquidatoTOT = SommaLiquidatoTOT + Decimal.Parse(sRisultato)
                        'End If
                        'myReader2.Close()

                        'SommaConsuntivatoCapTOT = SommaConsuntivatoCapTOT - SommaLiquidatoCapTOT
                        'SommaConsuntivatoTOT = SommaConsuntivatoTOT - SommaLiquidatoTOT

                        'X CAPITOLO e TOTALE REPORT FINE *********************************************************


                        '*** SommaBungetCapTOT = SommaBungetCapTOT + SommaBudget ' par.IfNull(myReader1("VALORE_LORDO"), 0)
                        '*** SommaAssestatoCapTOT = SommaAssestatoCapTOT + SommaAssestato 'par.IfNull(myReader1("VALORE_LORDO"), "0") + par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")

                        '*** SommaBungetTOT = SommaBungetTOT + SommaBudget ' par.IfNull(myReader1("VALORE_LORDO"), 0)
                        '*** SommaAssestatoTOT = SommaAssestatoTOT + SommaAssestato 'par.IfNull(myReader1("VALORE_LORDO"), 0) + par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")

                        'SommaResiduoTOT = SommaResiduoTOT + Importo
                        'SommaResiduoCapTOT = SommaResiduoCapTOT + Importo


                        'sStringaSql = "select * from SISCOM_MI.PF_VOCI "
                        'sStringaSql = sStringaSql & sStringaSql2 & " and ID_VOCE_MADRE=" & myReader1("ID") & " order by CODICE"

                        'par.cmd.CommandText = sStringaSql
                        'myReader2 = par.cmd.ExecuteReader()
                        'If myReader2.Read Then

                        'Else
                        '    SommaBungetCapTOT = SommaBungetCapTOT + SommaBudget ' par.IfNull(myReader1("VALORE_LORDO"), 0)
                        '    SommaAssestatoCapTOT = SommaAssestatoCapTOT + SommaAssestato 'par.IfNull(myReader1("VALORE_LORDO"), "0") + par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")

                        '    SommaBungetTOT = SommaBungetTOT + SommaBudget ' par.IfNull(myReader1("VALORE_LORDO"), 0)
                        '    SommaAssestatoTOT = SommaAssestatoTOT + SommaAssestato 'par.IfNull(myReader1("VALORE_LORDO"), 0) + par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")
                        'End If
                        'myReader2.Close()


                        ''IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
                        'par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO)) " _
                        '                    & " from    SISCOM_MI.PRENOTAZIONI " _
                        '                    & " where   ID_STATO=0 " _
                        '                    & "   and   ID_PAGAMENTO is null " _
                        '                    & "   and   ID_VOCE_PF=" & myReader1("ID")
                        'If sFiliale <> "" Then
                        '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                        'End If

                        'myReader2 = par.cmd.ExecuteReader()
                        'If myReader2.Read Then
                        '    sRisultato = par.IfNull(myReader2(0), "0")
                        '    SommaPrenotato = Decimal.Parse(sRisultato)
                        'End If
                        'myReader2.Close()
                        ''********************************

                        ''IMPORTO PRENOTATO [EMESSO o APPROVATO]
                        'par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO)) " _
                        '                    & " from    SISCOM_MI.PRENOTAZIONI " _
                        '                    & " where   ID_STATO=1 " _
                        '                    & "   and   ID_PAGAMENTO is null " _
                        '                    & "   and   ID_VOCE_PF=" & myReader1("ID")
                        'If sFiliale <> "" Then
                        '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                        'End If

                        'myReader2 = par.cmd.ExecuteReader()
                        'If myReader2.Read Then
                        '    sRisultato = par.IfNull(myReader2(0), "0")
                        '    SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
                        'End If
                        'myReader2.Close()
                        ''*******************************


                        'sStringaSql = "select * from SISCOM_MI.PF_VOCI "
                        'sStringaSql = sStringaSql & sStringaSql2 & " and ID_VOCE_MADRE=" & myReader1("ID") & " order by CODICE"

                        'par.cmd.CommandText = sStringaSql
                        'myReader2 = par.cmd.ExecuteReader()
                        'If myReader2.Read Then

                        'Else
                        '    SommaPrenotatoCapTOT = SommaPrenotatoCapTOT + SommaPrenotato
                        'End If
                        'myReader2.Close()




                        ''IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
                        'par.cmd.CommandText = "select SUM(IMPORTO_APPROVATO) " _
                        '                    & " from   SISCOM_MI.PRENOTAZIONI " _
                        '                    & " where  ID_PAGAMENTO is NOT null " _
                        '                    & "   and  ID_STATO>1 " _
                        '                    & "   and  ID_VOCE_PF=" & myReader1("ID")

                        'If sFiliale <> "" Then
                        '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale '& " or ID_STRUTTURA is Null)"
                        'End If


                        'myReader2 = par.cmd.ExecuteReader
                        'If myReader2.Read Then
                        '    SommaConsuntivato = par.IfNull(myReader2(0), 0)
                        '    '****SommaConsuntivatoCapTOT = SommaConsuntivatoCapTOT + par.IfNull(myReader2(0), 0)

                        'End If
                        'myReader2.Close()


                        'sStringaSql = "select * from SISCOM_MI.PF_VOCI "
                        'sStringaSql = sStringaSql & sStringaSql2 & " and ID_VOCE_MADRE=" & myReader1("ID") & " order by CODICE"

                        'par.cmd.CommandText = sStringaSql
                        'myReader2 = par.cmd.ExecuteReader()
                        'If myReader2.Read Then

                        'Else
                        '    SommaConsuntivatoCapTOT = SommaConsuntivatoCapTOT + SommaConsuntivato
                        'End If
                        'myReader2.Close()


                        ''IMPORTO CONSUNTIVATO e LIQUIDATO
                        'par.cmd.CommandText = "select  * from SISCOM_MI.PAGAMENTI " _
                        '                   & "  where  PAGAMENTI.ID in (select ID_PAGAMENTO " _
                        '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
                        '                                            & " where  ID_VOCE_PF=" & myReader1("ID") _
                        '                                            & "   and  ID_STATO>1 "
                        'If sFiliale <> "" Then
                        '    par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale & ")" ' or ID_STRUTTURA is Null))"
                        'Else
                        '    par.cmd.CommandText = par.cmd.CommandText & ")"
                        'End If
                        ''par.cmd.CommandText = "select  IMPORTO_CONSUNTIVATO,ID_STATO" _
                        ''                    & " from SISCOM_MI.PAGAMENTI where PAGAMENTI.ID_VOCE_PF=" & myReader1("ID")

                        'myReader2 = par.cmd.ExecuteReader()

                        'While myReader2.Read
                        '    Select Case par.IfNull(myReader2("ID_STATO"), 0)
                        '        'Case 0
                        '        '    SommaPrenotato = SommaPrenotato + par.IfNull(myReader2("IMPORTO_PRENOTATO"), 0)
                        '        '    SommaPrenotatoCapTOT = SommaPrenotatoCapTOT + par.IfNull(myReader2("IMPORTO_PRENOTATO"), 0)
                        '        'Case 1
                        '        '    SommaConsuntivato = SommaConsuntivato + par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)
                        '        '    SommaConsuntivatoCapTOT = SommaConsuntivatoCapTOT + par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)

                        '        Case 5
                        '            SommaLiquidato = SommaLiquidato + par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)
                        '            SommaLiquidatoCapTOT = SommaLiquidatoCapTOT + par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)

                        '    End Select

                        'End While
                        'myReader2.Close()

                        'SommaConsuntivato = SommaConsuntivato - SommaLiquidato
                        'SommaConsuntivatoCapTOT = SommaConsuntivatoCapTOT - SommaLiquidatoCapTOT

                        'SommaResiduo = SommaAssestato - (SommaPrenotato + SommaConsuntivato + SommaLiquidato)
                        ''SommaResiduoCapTOT = SommaResiduoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                        '***********************************

                        If Capitolo1 = False Then
                            'If myReader1("CODICE") >= 1 And myReader1("indice") <= 20 Then
                            If Strings.Left(myReader1("CODICE"), 2) = "1." Then

                                CalcoliTotale("1")

                                SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                                ' SommaResiduoCapTOT = Importo
                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>1</td>" _
                                                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per il property management</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "</tr>"
                                Capitolo1 = True
                                Contatore = Contatore + 1


                                'SommaBungetCapTOT = 0
                                'SommaAssestatoCapTOT = 0

                                'SommaPrenotatoCapTOT = 0
                                'SommaConsuntivatoCapTOT = 0
                                'SommaLiquidatoCapTOT = 0

                            End If
                        End If

                        If Capitolo2 = False Then
                            'If myReader1("indice") >= 21 And myReader1("indice") <= 40 Then
                            If Strings.Left(myReader1("CODICE"), 2) = "2." Then

                                '*** SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget ' - par.IfNull(myReader1("VALORE_LORDO"), 0)
                                '*** SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaAssestato ' par.IfNull(myReader1("VALORE_LORDO"), "0") - par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")

                                CalcoliTotale("2")

                                SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                                'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                '                          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                                '                          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese per il property management</td>" _
                                '                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                '                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                '                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                '                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                '                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                '                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                '                          & "</tr>"

                                'Contatore = Contatore + 1

                                'SommaResiduoCapTOT = Importo
                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>2</td>" _
                                                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per il facility management</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "</tr>"

                                Contatore = Contatore + 1
                                Capitolo2 = True

                                'SommaBungetCapTOT = 0
                                'SommaAssestatoCapTOT = 0

                                'SommaPrenotatoCapTOT = 0
                                'SommaConsuntivatoCapTOT = 0
                                'SommaLiquidatoCapTOT = 0
                            End If
                        End If


                        If Capitolo3 = False Then
                            'If myReader1("indice") >= 41 And myReader1("indice") <= 45 Then
                            If Strings.Left(myReader1("CODICE"), 2) = "3." Then
                                '*** SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget ' par.IfNull(myReader1("VALORE_LORDO"), 0)
                                '*** SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaAssestato ' par.IfNull(myReader1("VALORE_LORDO"), "0") - par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")

                                CalcoliTotale("3")
                                SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                                'If Capitolo2 = False Then


                                '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese per il property management</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "</tr>"

                                'Else

                                '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese per il facility management</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "</tr>"
                                'End If


                                'Contatore = Contatore + 1

                                'SommaResiduoCapTOT = Importo

                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>3</td>" _
                                                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per contributi per sostegno agli inquilini</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "</tr>"
                                Contatore = Contatore + 1
                                Capitolo3 = True

                                'SommaBungetCapTOT = 0
                                'SommaAssestatoCapTOT = 0

                                'SommaPrenotatoCapTOT = 0
                                'SommaConsuntivatoCapTOT = 0
                                'SommaLiquidatoCapTOT = 0

                            End If
                        End If

                        If Capitolo4 = False Then
                            ' If myReader1("indice") >= 46 And myReader1("indice") <= 51 Then
                            If Strings.Left(myReader1("CODICE"), 2) = "4." Then
                                '*** SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget ' par.IfNull(myReader1("VALORE_LORDO"), 0)
                                '*** SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaAssestato ' par.IfNull(myReader1("VALORE_LORDO"), "0") - par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")

                                CalcoliTotale("4")

                                SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                                'If Capitolo3 = False Then
                                '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese per il facility management</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "</tr>"


                                'Else

                                '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese per contributi per sostegno agli inquilini</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                '                              & "</tr>"
                                'End If

                                'Contatore = Contatore + 1

                                'SommaResiduoCapTOT = Importo
                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>4</td>" _
                                                          & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese diverse</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                                          & "</tr>"

                                Contatore = Contatore + 1
                                Capitolo4 = True

                                'SommaBungetCapTOT = 0
                                'SommaAssestatoCapTOT = 0

                                'SommaPrenotatoCapTOT = 0
                                'SommaConsuntivatoCapTOT = 0
                                'SommaLiquidatoCapTOT = 0

                            End If
                        End If


                        'TestoPagina = TestoPagina & "<tr style='font-family: ARIAL; font-size: 9pt;border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(CDbl(par.IfNull(myReader1("valore"), "0")), "##,##0.00") & "</td></tr>"
                        'If par.IfNull(myReader1("ID_VOCE_MADRE"), "-1") = "-1" Then
                        '    Totale = Totale + CDbl(par.IfNull(myReader1("valore"), 0))
                        'End If

                        'Contatore = Contatore + 1
                        'If Contatore = 20 Then
                        '    Contatore = 1
                        '    TestoPagina = TestoPagina & "</table>"
                        '    TestoPagina = TestoPagina & "<p style='page-break-before: always'>&nbsp;</p>"
                        '    TestoPagina = TestoPagina & "<table style='border: 1px solid #000000; width: 100%;' cellpadding=0 cellspacing = 0'>"
                        '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 12pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>COD.</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td></tr>"

                        'End If


                        'sStringaSql = "select * from SISCOM_MI.PF_VOCI "
                        'sStringaSql = sStringaSql & sStringaSql2 & " and ID_VOCE_MADRE=" & myReader1("ID") & " order by CODICE"

                        'par.cmd.CommandText = sStringaSql
                        'myReader2 = par.cmd.ExecuteReader()
                        'If myReader2.Read Then

                        '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                        '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td>" _
                        '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td>" _
                        '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & "" & "</td>" _
                        '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & "" & "</td>" _
                        '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & "" & "</td>" _
                        '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & "" & "</td>" _
                        '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & "" & "</td>" _
                        '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & "" & "</td>" _
                        '                              & "</tr>"


                        'Else

                        SommaResiduo = SommaAssestato - (SommaPrenotato + SommaConsuntivato + SommaLiquidato)

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                  & "<td align='left'  style='border-bottom-style: dashed;  width:7%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td>" _
                                                  & "<td align='left'  style='border-bottom-style: dashed;  width:33%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed;  width:11%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBudget, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed;  width:11%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestato, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed;  width:10%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduo, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed;  width:10%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotato, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed;  width:10%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivato, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed;  width:8%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidato, "##,##0.00") & "</td>" _
                                                  & "</tr>"

                        'End If
                        'myReader2.Close()

                        Contatore = Contatore + 1

                        If Contatore = 20 Then
                            Contatore = 1
                            TestoPagina = TestoPagina & "</table>"
                            TestoPagina = TestoPagina & "<p style='page-break-before: always'>&nbsp;</p>"

                            TestoPagina = TestoPagina & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                                      & "<td align='left'  style='border-bottom-style: dashed; width:7%;  border-bottom-width: 1px; border-bottom-color: #000000'>COD.</td>" _
                                                      & "<td align='left'  style='border-bottom-style: dashed; width:33%; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td>" _
                                                      & "<td align='right' style='border-bottom-style: dashed; width:11%; border-bottom-width: 1px; border-bottom-color: #000000'>BUDGET INIZIALE</td>" _
                                                      & "<td align='right' style='border-bottom-style: dashed; width:11%; border-bottom-width: 1px; border-bottom-color: #000000'>BUDGET ASSESTATO + VAR</td>" _
                                                      & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>DISPONIBILITA' RESIDUA</td>" _
                                                      & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE     PRENOTATO</td>" _
                                                      & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE     CONSUNTIVATO</td>" _
                                                      & "<td align='right' style='border-bottom-style: dashed; width:8%;  border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE             PAGATO</td>" _
                                                      & "</tr>"


                            'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>COD.</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;&nbsp;PROSEGUI</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

                        End If


                        'SommaPrenotatoTOT = SommaPrenotatoTOT + SommaPrenotato
                        'SommaConsuntivatoTOT = SommaConsuntivatoTOT + SommaConsuntivato
                        'SommaLiquidatoTOT = SommaLiquidatoTOT + SommaLiquidato

                        'SommaBungetCapTOT = 0
                        'SommaAssestatoCapTOT = 0
                        'SommaPrenotatoCapTOT = 0
                        'SommaConsuntivatoCapTOT = 0
                        'SommaLiquidatoCapTOT = 0
                    End If

                End While


                'If Capitolo1 = False Then

                '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td></tr>"

                '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>1</td>" _
                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per il property management</td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "</tr>"

                '    Contatore = Contatore + 1
                'End If


                'If Capitolo2 = False And Capitolo3 = False And Capitolo4 = False Then


                '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TOTALE Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td></tr>"
                '    ' *** SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget
                '    '*** SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaBudget - SommaAssestato

                '    CalcoliTotale("2")

                '    SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese per il property management</td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                '                              & "</tr>"
                '    Contatore = Contatore + 1

                '    'SommaBungetCapTOT = 0
                '    'SommaAssestatoCapTOT = 0


                '    'SommaPrenotatoCapTOT = 0
                '    'SommaConsuntivatoCapTOT = 0
                '    'SommaLiquidatoCapTOT = 0

                '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>2</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

                '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>2</td>" _
                '                              & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per il facility management</td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "</tr>"

                '    Contatore = Contatore + 1
                '    Capitolo2 = True

                'End If


                'If Capitolo3 = False And Capitolo4 = False Then


                '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TOTALE Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td></tr>"

                '    ' ***SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget
                '    '*** SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaBudget - SommaAssestato

                '    CalcoliTotale("3")
                '    SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                '    If Capitolo2 = False Then
                '        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                '                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                '                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese per il property management</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "</tr>"

                '    Else
                '        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                '                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                '                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese per il facility management</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "</tr>"
                '    End If

                '    Contatore = Contatore + 1

                '    'SommaBungetCapTOT = 0
                '    'SommaAssestatoCapTOT = 0

                '    'SommaPrenotatoCapTOT = 0
                '    'SommaConsuntivatoCapTOT = 0
                '    'SommaLiquidatoCapTOT = 0

                '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>3</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

                '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                '                              & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>3</td>" _
                '                              & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per contributi per sostegno agli inquilini</td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '                              & "</tr>"

                '    Contatore = Contatore + 1
                '    Capitolo3 = True

                'End If

                'If Capitolo4 = False And Capitolo5 = False Then
                '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td></tr>"

                '    '*** SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget
                '    '*** SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaBudget - SommaAssestato

                '    CalcoliTotale("4")
                '    SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                '    If Capitolo3 = False Then
                '        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                '                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                '                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese per il facility management</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "</tr>"


                '    Else


                '        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                '                                  & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                '                                  & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese per contributi per sostegno agli inquilini</td>" _
                '                                  & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                '                                  & "</tr>"
                '    End If

                '    Contatore = Contatore + 1

                '    SommaBungetCapTOT = 0
                '    SommaAssestatoCapTOT = 0

                '    SommaPrenotatoCapTOT = 0
                '    SommaConsuntivatoCapTOT = 0
                '    SommaLiquidatoCapTOT = 0

                '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border: 2px dashed #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>4</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

                '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                '          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>4</td>" _
                '          & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese diverse</td>" _
                '          & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'></td>" _
                '          & "</tr>"

                '    Contatore = Contatore + 1
                '    Capitolo4 = True

                '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TOTALE Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td></tr>"

                'End If

                'If Capitolo5 = False Then

                '    '*** SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget
                '    '**** SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaBudget - SommaAssestato

                '    SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                '                              & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
                '                              & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;TOTALE Spese diverse</td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                '                              & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                '                              & "</tr>"

                '    Contatore = Contatore + 1

                '    SommaBungetCapTOT = 0
                '    SommaAssestatoCapTOT = 0


                '    SommaPrenotatoCapTOT = 0
                '    SommaConsuntivatoCapTOT = 0
                '    SommaLiquidatoCapTOT = 0

                '    Capitolo5 = True
                'End If


                SommaResiduoTOT = SommaAssestatoTOT - (SommaPrenotatoTOT + SommaConsuntivatoTOT + SommaLiquidatoTOT)

                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                          & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                          & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;TOTALE </td>" _
                                          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaBungetTOT, "##,##0.00") & "</td>" _
                                          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaAssestatoTOT, "##,##0.00") & "</td>" _
                                          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaResiduoTOT, "##,##0.00") & "</td>" _
                                          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaPrenotatoTOT, "##,##0.00") & "</td>" _
                                          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaConsuntivatoTOT, "##,##0.00") & "</td>" _
                                          & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaLiquidatoTOT, "##,##0.00") & "</td>" _
                                          & "</tr>"

                myReader1.Close()

                TestoPagina = TestoPagina & "</table>"



                Dim NomeFile As String = "PF_" & Format(Now, "yyyyMMddHHmmss")

                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)

                'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
                sr.WriteLine(TestoPagina)
                sr.Close()

                Dim url As String = NomeFile
                Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")

                Dim pdfConverter As PdfConverter = New PdfConverter

                If Licenza <> "" Then
                    pdfConverter.LicenseKey = Licenza
                End If

                pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
                pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                pdfConverter.PdfDocumentOptions.ShowHeader = False
                pdfConverter.PdfDocumentOptions.ShowFooter = False
                pdfConverter.PdfDocumentOptions.LeftMargin = 20
                pdfConverter.PdfDocumentOptions.RightMargin = 20
                pdfConverter.PdfDocumentOptions.TopMargin = 5
                pdfConverter.PdfDocumentOptions.BottomMargin = 5
                pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

                pdfConverter.PdfDocumentOptions.ShowHeader = False
                pdfConverter.PdfFooterOptions.FooterText = ("")
                pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
                pdfConverter.PdfFooterOptions.DrawFooterLine = False
                pdfConverter.PdfFooterOptions.PageNumberText = ""
                pdfConverter.PdfFooterOptions.ShowPageNumber = False


                pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".pdf")
                IO.File.Delete(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm")
                Response.Redirect("..\..\..\FileTemp\" & NomeFile & ".pdf")

                'End If

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

            Catch ex As Exception
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                Response.Write(ex.Message)
            End Try


        End If
    End Sub


    Public Sub CalcoliTotale(ByVal CODICE As String)
        Dim FlagConnessione As Boolean
        Dim sRisultato As String = ""
        Dim ElencoPF_VOCI_ID As String = ""

        'X CAPITOLO e TOTALE REPORT di TUTTO ****************************************

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            SommaBungetCapTOT = 0
            SommaAssestatoCapTOT = 0
            SommaVariazioniCapTOT = 0

            SommaPrenotatoCapTOT = 0
            SommaConsuntivatoCapTOT = 0
            SommaLiquidatoCapTOT = 0


            Dim sFiliale As String = ""
            'If Session.Item("LIVELLO") <> "1" Then
            If Request.QueryString("CHIAMANTE") = "STAMPA_DETTAGLIO" Then
                sFiliale = "  ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            Else
                If Strings.Trim(Request.QueryString("STR")) <> "-1" Then
                    sFiliale = "  ID_STRUTTURA=" & Strings.Trim(Request.QueryString("STR"))
                End If
            End If
            'End If

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            par.cmd.CommandText = " select PF_VOCI.* " _
                        & " from SISCOM_MI.PF_VOCI " _
                        & " where PF_VOCI.ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR

            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & "   and ( PF_VOCI.ID in " _
                                        & " (select ID_VOCE from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                        & "  where " & sFiliale & ")" _
                        & "    or PF_VOCI.ID_VOCE_MADRE in (select ID_VOCE from siscom_mi.PF_VOCI_STRUTTURA where " & sFiliale & "))"
            End If

            If Request.QueryString("CHIAMANTE") = "STAMPA_DETTAGLIO" Then
                par.cmd.CommandText = par.cmd.CommandText & "   and PF_VOCI.ID not in " _
                                        & " (select distinct ID_VOCE from SISCOM_MI.PF_VOCI_IMPORTO where ID_LOTTO is not null) "
            End If

            par.cmd.CommandText = par.cmd.CommandText & " and CODICE LIKE ('" & CODICE & ".%') order by CODICE"

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read
                If ElencoPF_VOCI_ID = "" Then
                    ElencoPF_VOCI_ID = par.IfNull(myReader2(0), "")
                Else
                    ElencoPF_VOCI_ID = ElencoPF_VOCI_ID & "," & par.IfNull(myReader2(0), "")
                End If

            End While
            myReader2.Close()


            'SOMMA VALORE LORDO 
            par.cmd.CommandText = "select ID_VOCE,to_char(VALORE_LORDO)  as VALORE_LORDO from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
            End If

            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read

                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then

                Else
                    sRisultato = par.IfNull(myReader2("VALORE_LORDO"), "0")

                    SommaBungetCapTOT = SommaBungetCapTOT + Decimal.Parse(sRisultato)    'x CAPITOLO
                End If
                myReader3.Close()

            End While
            myReader2.Close()
            '----------------------------------

            'SOMMA VALORE ASSESSTATO LORDO
            par.cmd.CommandText = "select ID_VOCE,to_char(ASSESTAMENTO_VALORE_LORDO) as ASSESTAMENTO_VALORE_LORDO from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
            End If

            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read

                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then

                Else
                    sRisultato = par.IfNull(myReader2("ASSESTAMENTO_VALORE_LORDO"), "0")
                    SommaAssestatoCapTOT = SommaAssestatoCapTOT + Decimal.Parse(sRisultato)
                End If
                myReader3.Close()

            End While
            myReader2.Close()
            '----------------------------------

            'SOMMA VARIAZIONI
            par.cmd.CommandText = "select ID_VOCE,to_char(VARIAZIONI) as VARIAZIONI from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
            End If

            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read

                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then

                Else
                    sRisultato = par.IfNull(myReader2("VARIAZIONI"), "0")
                    SommaVariazioniCapTOT = SommaVariazioniCapTOT + Decimal.Parse(sRisultato)
                End If
                myReader3.Close()

            End While
            myReader2.Close()
            '----------------------------------

            SommaAssestatoCapTOT = SommaBungetCapTOT + SommaAssestatoCapTOT + SommaVariazioniCapTOT     'x CAPITOLO
            '*******************************

            'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
            par.cmd.CommandText = " select  ID_VOCE_PF,to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO " _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   (ID_STATO=0 or ID_STATO=1) " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")"
            '& "   and   ID_VOCE_PF NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader1("ID") & ")"
            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
            End If

            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read

                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE_PF")

                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then

                Else
                    sRisultato = par.IfNull(myReader2("IMPORTO_PRENOTATO"), "0")

                    SommaPrenotatoCapTOT = SommaPrenotatoCapTOT + Decimal.Parse(sRisultato) 'x CAPITOLO
                End If
                myReader3.Close()

            End While
            myReader2.Close()
            '********************************


            'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
            par.cmd.CommandText = "select  ID_VOCE_PF,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO " _
                                & " from   SISCOM_MI.PRENOTAZIONI " _
                                & " where  ID_PAGAMENTO is NOT null " _
                                & "   and  ID_STATO>1 " _
                                & "   and  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")"
            '& "   and   ID_VOCE_PF NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader1("ID") & ")"

            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale
            End If

            myReader2 = par.cmd.ExecuteReader
            While myReader2.Read

                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE_PF")

                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then

                Else
                    sRisultato = par.IfNull(myReader2("IMPORTO_APPROVATO"), "0")

                    SommaConsuntivatoCapTOT = SommaConsuntivatoCapTOT + Decimal.Parse(sRisultato)   'x CAPITOLO
                End If
                myReader3.Close()

            End While
            myReader2.Close()


            'IMPORTO LIQUIDATO
            par.cmd.Parameters.Clear()
            'par.cmd.CommandText = "select to_char(SUM(IMPORTO_CONSUNTIVATO) ) " _
            '                   & " from SISCOM_MI.PAGAMENTI " _
            '                   & "  where DATA_STAMPA is NOT null " _
            '                   & "    and ID_STATO=5 " _
            '                   & "    and PAGAMENTI.ID in (select ID_PAGAMENTO " _
            '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
            '                                            & " where  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" _
            '                                            & "   and  ID_VOCE_PF NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE in (" & ElencoPF_VOCI_ID & "))" _
            '                                            & "   and  ID_STATO>=2"

            par.cmd.CommandText = "select to_char(SUM(IMPORTO)) " _
                               & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                               & " where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                                                    & " from   SISCOM_MI.PRENOTAZIONI " _
                                                    & " where  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" _
                                                    & "   and  ID_VOCE_PF NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE in (" & ElencoPF_VOCI_ID & "))" _
                                                    & "   and  ID_STATO>=2"


            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and " & sFiliale & ")"
            Else
                par.cmd.CommandText = par.cmd.CommandText & ")"
            End If

            myReader2 = par.cmd.ExecuteReader
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")

                SommaLiquidatoCapTOT = SommaLiquidatoCapTOT + Decimal.Parse(sRisultato)
            End If
            myReader2.Close()

            SommaConsuntivatoCapTOT = SommaConsuntivatoCapTOT - SommaLiquidatoCapTOT

            'FINE X CAPITOLO 


            'TOTALE REPORT FINE *********************************************************
            SommaBungetTOT = SommaBungetTOT + SommaBungetCapTOT

            SommaAssestatoTOT = SommaAssestatoTOT + SommaAssestatoCapTOT

            SommaPrenotatoTOT = SommaPrenotatoTOT + SommaPrenotatoCapTOT
            SommaConsuntivatoTOT = SommaConsuntivatoTOT + SommaConsuntivatoCapTOT
            SommaLiquidatoTOT = SommaLiquidatoTOT + SommaLiquidatoCapTOT
            '***************************************************************************


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Response.Write(ex.Message)
        End Try



    End Sub

    'RICAVA LO STATO DELL'ESERCIZIO SELEZIONATO (5,6,7)
    Private Function RicavaStatoEsercizioFinanaziario(ByVal ID_ESERCIZIO As Long) As Integer
        Dim FlagConnessione As Boolean

        Try
            RicavaStatoEsercizioFinanaziario = -1


            If ID_ESERCIZIO < 0 Then Exit Function

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            par.cmd.CommandText = "select * from SISCOM_MI.PF_MAIN " _
                               & " where PF_MAIN.ID_ESERCIZIO_FINANZIARIO=" & ID_ESERCIZIO

            Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderF.Read Then
                RicavaStatoEsercizioFinanaziario = par.IfNull(myReaderF("ID_STATO"), -1)
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
    End Function

End Class
