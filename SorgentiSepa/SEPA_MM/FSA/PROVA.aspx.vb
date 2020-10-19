
Partial Class FSA_PROVA
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='Elaborazione in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        CalcolaStampa()

    End Sub

    Private Function CalcolaStampa()
        Dim sStringaSql As String = ""

        Dim AnnoBando As String = ""
        Dim DETRAZIONI As Long
        Dim INV_100_CON As Integer
        Dim INV_100_NO As Integer
        Dim INV_66_99 As Integer
        Dim TOT_COMPONENTI As Integer
        Dim REDDITO_COMPLESSIVO As Double
        Dim TOT_SPESE As Long
        Dim DETRAZIONI_FRAGILE As Long
        Dim DETRAZIONI_FR As Long

        Dim MOBILI As Double

        Dim FIGURATIVO_MOBILI As Double
        Dim TOTALE_ISEE_ERP As Double
        Dim IMMOBILI As Long
        Dim MUTUI As Long
        Dim IMMOBILI_RESIDENZA As Long
        Dim MUTUI_RESIDENZA As Long
        Dim TOTALE_PATRIMONIO_ISEE_ERP As Double
        Dim TOTALE_IMMOBILI As Long
        Dim LIMITE_PATRIMONIO As Double

        Dim ISR_ERP As Double
        Dim ISP_ERP As Double
        Dim ISE_ERP As Double
        Dim VSE As Double
        Dim ISEE_ERP As Double
        Dim ESCLUSIONE As String
        Dim oltre65 As Boolean

        Dim detrazioni_oltre_65 As Double



        Dim MINORI As Integer
        Dim adulti As Integer
        Dim TASSO_RENDIMENTO As Double


        Dim lIdDichiarazione As Long
        Dim lIdDomanda As Long
        llISEE.Text = "0"


        PAR.OracleConn.Open()
        par.SettaCommand(par)
        'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        '‘‘par.cmd.Transaction = par.myTrans


        AnnoBando = "2008"
        TASSO_RENDIMENTO = 4.41



        PAR.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_FSA WHERE ID_STATO='2'"
        Dim myReader1111 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
        'If myReader1111.Read Then
        '    Label1.Text = "myReader1111(0)"
        'End If
        'adulti = myReader1111.RecordsAffected
        Do While myReader1111.Read
            lIdDichiarazione = myReader1111("ID_DICHIARAZIONE")
            lIdDomanda = myReader1111("ID")



            MINORI = 0
            adulti = 0

            ISR_ERP = 0
            ISP_ERP = 0
            ISE_ERP = 0

            VSE = 0

            TOT_COMPONENTI = 0
            DETRAZIONI = 0
            REDDITO_COMPLESSIVO = 0
            TOT_SPESE = 0
            DETRAZIONI_FRAGILE = 0
            DETRAZIONI_FR = 0
            ISEE_ERP = 0
            MOBILI = 0
            FIGURATIVO_MOBILI = 0

            IMMOBILI = 0
            MUTUI = 0
            IMMOBILI_RESIDENZA = 0
            MUTUI_RESIDENZA = 0
            TOTALE_IMMOBILI = 0

            TOTALE_ISEE_ERP = 0
            TOTALE_PATRIMONIO_ISEE_ERP = 0
            LIMITE_PATRIMONIO = 0


            ESCLUSIONE = ""



            Dim E1 As Double = 0
            Dim E2 As Double = 0
            Dim E3FSA As Double = 0
            Dim E4FSA As Double = 0


            Dim F12 As Double = 0
            Dim F13 As Double = 0
            Dim F16FSA As Double = 0
            Dim F18FSA As Double = 0
            Dim F24FSA As Double = 0

            Dim L1 As Double = 0
            Dim L2 As Double = 0
            Dim L3 As Double = 0
            Dim L4 As Double = 0
            Dim L5 As Double = 0
            Dim L6 As Double = 0
            Dim L7 As Double = 0
            Dim L8 As Double = 0
            Dim L9 As Double = 0
            Dim L10 As Double = 0
            Dim L11 As Double = 0
            Dim L12 As Double = 0
            Dim L13 As Double = 0
            Dim L14 As Double = 0

            Dim ENTRAMBI_GENITORI As String = ""
            Dim HPSICO As Integer
            Dim LAVORO_IMPRESA As String = ""

            Dim quotacomunale As Double = 0
            Dim quotaregionale As Double = 0
            Dim maxContributo As Double = 0

            Dim IDONEO_PRESUNTO As String = ""

            PAR.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_FSA WHERE ID=" & lIdDichiarazione
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                ENTRAMBI_GENITORI = PAR.IfNull(myReader("ENTRAMBI_GENITORI"), "0")
                HPSICO = PAR.IfNull(myReader("H_PSICO"), 0)
                LAVORO_IMPRESA = PAR.IfNull(myReader("LAVORO_IMPRESA"), "0")

                INV_100_CON = PAR.IfNull(myReader("N_INV_100_CON"), 0)
                INV_100_NO = PAR.IfNull(myReader("N_INV_100_SENZA"), 0)
                INV_66_99 = PAR.IfNull(myReader("N_INV_100_66"), 0)

            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_FSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = PAR.cmd.ExecuteReader()
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            myReader1.Close()
            detrazioni_oltre_65 = 0
            TOT_COMPONENTI = 0
            While myReader.Read
                TOT_COMPONENTI = TOT_COMPONENTI + 1
                oltre65 = False
                If PAR.RicavaEtaChiusura(PAR.FormattaData(PAR.IfNull(myReader("DATA_NASCITA"), "")), "20090915") >= 18 Then
                    adulti = adulti + 1
                    If PAR.RicavaEtaChiusura(PAR.FormattaData(myReader("DATA_NASCITA")), "20090915") >= 65 Then
                        oltre65 = True
                    End If
                Else
                    MINORI = MINORI + 1
                End If

                PAR.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_FSA WHERE id_COMPONENTE=" & PAR.IfNull(myReader("ID"), -1)
                myReader1 = PAR.cmd.ExecuteReader()
                While myReader1.Read
                    If PAR.IfNull(myReader1("id_tipo"), 0) <> 3 Then
                        DETRAZIONI = DETRAZIONI + PAR.IfNull(myReader1("IMPORTO"), 0)
                    Else
                        If oltre65 = True Then
                            detrazioni_oltre_65 = detrazioni_oltre_65 + PAR.IfNull(myReader1("IMPORTO"), 0)
                        End If
                    End If

                End While
                myReader1.Close()

                PAR.cmd.CommandText = "SELECT * FROM COMP_REDDITO_FSA WHERE ID_COMPONENTE=" & PAR.IfNull(myReader("ID"), -1)
                myReader1 = PAR.cmd.ExecuteReader()
                While myReader1.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + PAR.IfNull(myReader1("REDDITO_IRPEF"), 0) + PAR.IfNull(myReader1("PROV_AGRARI"), 0)
                End While
                myReader1.Close()


                PAR.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_FSA WHERE ID_COMPONENTE=" & PAR.IfNull(myReader("ID"), -1)
                myReader1 = PAR.cmd.ExecuteReader()
                While myReader1.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + PAR.IfNull(myReader1("IMPORTO"), 0)

                End While
                myReader1.Close()

                DETRAZIONI_FRAGILE = 0


                PAR.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_FSA WHERE ID_COMPONENTE=" & PAR.IfNull(myReader("ID"), -1)
                myReader1 = PAR.cmd.ExecuteReader()
                While myReader1.Read
                    MOBILI = MOBILI + PAR.IfNull(myReader1("IMPORTO"), 0)

                End While
                myReader1.Close()

                PAR.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_FSA WHERE ID_COMPONENTE=" & PAR.IfNull(myReader("ID"), -1)
                myReader1 = PAR.cmd.ExecuteReader()
                While myReader1.Read
                    If PAR.IfNull(myReader1("F_RESIDENZA"), 0) = 1 Then
                        IMMOBILI_RESIDENZA = IMMOBILI_RESIDENZA + PAR.IfNull(myReader1("VALORE"), 0)
                        MUTUI_RESIDENZA = MUTUI_RESIDENZA + PAR.IfNull(myReader1("MUTUO"), 0)
                    Else
                        IMMOBILI = IMMOBILI + PAR.IfNull(myReader1("VALORE"), 0)
                        MUTUI = MUTUI + PAR.IfNull(myReader1("MUTUO"), 0)
                    End If

                End While
                myReader1.Close()

            End While
            myReader.Close()


            DETRAZIONI_FR = 0 'PER FSA NON VENGNO CALCOLATE DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)

            FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165

            If detrazioni_oltre_65 > 2582 Then detrazioni_oltre_65 = 2582
            DETRAZIONI = DETRAZIONI + detrazioni_oltre_65
            E1 = REDDITO_COMPLESSIVO 'Format(REDDITO_COMPLESSIVO, "##,##0.00")
            E2 = ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) 'Format(((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100), "##,##0.00")
            E3FSA = DETRAZIONI 'Format(DETRAZIONI, "##,##0.00")



            ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
            E4FSA = ISEE_ERP 'Format(ISEE_ERP, "##,##0.00")
            L1 = E4FSA


            F12 = FIGURATIVO_MOBILI 'Format(FIGURATIVO_MOBILI, "##,##0.00")


            If ISEE_ERP < 0 Then
                ISEE_ERP = 0
            End If

            ISR_ERP = ISEE_ERP

            ISEE_ERP = 0

            TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA)
            F13 = TOTALE_IMMOBILI

            ''F16FSA = (((TOTALE_IMMOBILI + MOBILI) \ 5165) * 5165)
            ''TOTALE_ISEE_ERP = (((TOTALE_IMMOBILI + MOBILI) \ 5165) * 5165) * 0.05
            ''TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + MOBILI)


            F16FSA = (((TOTALE_IMMOBILI + FIGURATIVO_MOBILI) \ 5165) * 5165)
            TOTALE_ISEE_ERP = (((TOTALE_IMMOBILI + FIGURATIVO_MOBILI) \ 5165) * 5165) * 0.05
            TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)


            F24FSA = TOTALE_ISEE_ERP


            ISP_ERP = TOTALE_ISEE_ERP

            F18FSA = 0.05


            L2 = F24FSA
            L3 = L1 + L2 'Format(CDbl(L1) + CDbl(L2), "##,##0.00")

            Dim PARAMETRO As Double

            Select Case TOT_COMPONENTI
                Case 1
                    PARAMETRO = 1
                Case 2
                    PARAMETRO = 1.57
                Case 3
                    PARAMETRO = 2.04
                Case 4
                    PARAMETRO = 2.46
                Case 5
                    PARAMETRO = 2.85
                Case Else
                    PARAMETRO = 2.85 + ((TOT_COMPONENTI - 5) * 0.35)
            End Select

            L4 = PARAMETRO

            If ENTRAMBI_GENITORI = "0" And MINORI > 0 Then
                PARAMETRO = PARAMETRO + 0.2
                L5 = 0.2
            End If

            PARAMETRO = PARAMETRO + ((HPSICO + INV_100_CON + INV_100_NO + INV_66_99) * 0.5)
            L6 = (HPSICO + INV_100_CON + INV_100_NO + INV_66_99) * 0.5 'Format((HPSICO + INV_100_CON + INV_100_NO + INV_66_99) * 0.5, "##,##0.00")

            If LAVORO_IMPRESA = "1" And MINORI > 0 Then
                PARAMETRO = PARAMETRO + 0.2
                L7 = 0.2
            End If

            L8 = L7 + L6 + L5 + L4 'Format(CDbl(L7) + CDbl(L6) + CDbl(L5) + CDbl(L4), "##,##0.00")

            VSE = PARAMETRO
            LIMITE_PATRIMONIO = 10330 + (5165 * VSE)
            ISE_ERP = ISR_ERP + ISP_ERP
            ISEE_ERP = ISE_ERP / VSE



            If ISEE_ERP > 12911.43 Then
                ISEE_ERP = 0
                ESCLUSIONE = "LIMITE ISEE SUPERATO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "1"
            End If

            If F16FSA > LIMITE_PATRIMONIO Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "LIMITE PATRIMONIALE SUPERATO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "2"
            End If

            If PAR.IfNull(myReader1111("CATEGORIA_CAT"), "") = "A1" Or PAR.IfNull(myReader1111("CATEGORIA_CAT"), "") = "A8" Or PAR.IfNull(myReader1111("CATEGORIA_CAT"), "") = "A9" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "UNITA' IMMOBILIARE DI CAT. " & PAR.IfNull(myReader1111("CATEGORIA_CAT"), "") & "<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "5"
            End If

            If PAR.IfNull(myReader1111("SUPERFICIE"), "") > 110 Then
                If TOT_COMPONENTI > 4 Then
                    If CDbl(PAR.IfNull(myReader1111("SUPERFICIE"), "0")) > 110 * (1 + (0.1 * (TOT_COMPONENTI - 4))) Then
                        ISEE_ERP = 0
                        ESCLUSIONE = ESCLUSIONE & "UNITA' IMMOBILIARE CON SUP. UTILE NETTA SUPERIORE A " & 110 * (1 + (0.1 * (TOT_COMPONENTI - 4))) & " mq<BR>"
                        IDONEO_PRESUNTO = IDONEO_PRESUNTO & "4"
                    End If
                Else
                    ISEE_ERP = 0
                    ESCLUSIONE = ESCLUSIONE & "UNITA' IMMOBILIARE CON SUP. UTILE NETTA SUPERIORE A 110 mq<BR>"
                    IDONEO_PRESUNTO = IDONEO_PRESUNTO & "4"
                End If
            End If

            llISEE.Text = PAR.Tronca(ISEE_ERP)

            If PAR.IfNull(myReader1111("STATO_C"), "") <> "REG" Then
                'Response.Write("<script>alert('Attenzione, il contratto non è registrato. In fase di erogazione del contributo deve dimostrare di aver inoltrato richiesta di registrazione e aver versato la relativa imposta.');</script>")
            End If


            'Dim Catastali As String = "SI"
            'If (PAR.IfNull(myReader1111("FOGLIO"), "") = "" Or PAR.IfNull(myReader1111("SUPERFICIE"), "") = "0") Or (CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text = "" Or CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text = "0") Or (CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text = "" Or CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text = "0") Then
            '    'Response.Write("<SCRIPT>alert('Attenzione...Non sono stati inseriti i dati catastali dell/unità (Foglio,Mappale,Sub), non sarà possibile verificare se è gia stato chiesto un contributo per questa unità!')</SCRIPT>")
            '    Catastali = "NO"
            'End If

            'If Catastali = "SI" Then
            '    PAR.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_fsa WHERE ID_BANDO=" & lIndice_Bando & " AND ID<>" & lIdDomanda & " and FOGLIO='" & PAR.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtFoglio"), TextBox).Text) & "' and particella='" & PAR.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtParticella"), TextBox).Text) & "' and subalterno='" & PAR.PulisciStrSql(CType(Dom_Abitative_1_1.FindControl("txtSub"), TextBox).Text) & "'"
            '    myReader1 = PAR.cmd.ExecuteReader()
            '    If myReader1.Read Then
            '        ISEE_ERP = 0
            '        ESCLUSIONE = ESCLUSIONE & "CONTRIBUTO GIA RICHIESTO PER QUESTA UNITA ABITATIVA<BR>"
            '        IDONEO_PRESUNTO = IDONEO_PRESUNTO & "8"
            '    End If
            '    myReader1.Close()
            'End If

            If PAR.IfNull(myReader1111("REQUISITO1"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "NON E' RESIDENTE NELL’ALLOGGIO OGGETTO DEL CONTRATTO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "9"

            End If

            If PAR.IfNull(myReader1111("REQUISITO2"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "ASSEGNATARIO ALLOGGIO ERP O POR<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "A"

            End If

            If PAR.IfNull(myReader1111("REQUISITO3"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "MANCANZA DI CITTADINANZA O SOGGIORNO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "B"

            End If

            If PAR.IfNull(myReader1111("REQUISITO4"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "PROCEDURA ESECUTIVA DI SFRATTO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "C"

            End If

            If PAR.IfNull(myReader1111("REQUISITO5"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "PROPRIETA' DI ALLOGGIO ADEGUATO<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "D"

            End If

            If PAR.IfNull(myReader1111("REQUISITO6"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "ASSEGNAZIONE IN GODIMENTO DI U.I. DA PARTE DI COOP EDILIZIE<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "E"

            End If

            If PAR.IfNull(myReader1111("REQUISITO7"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "ASSEGNAZIONE DI U.I REALIZZATE CON CONTR. PUBBLICI<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "F"

            End If

            If PAR.IfNull(myReader1111("REQUISITO8"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "MORTE<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "G"

            End If

            If PAR.IfNull(myReader1111("REQUISITO9"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "IRREPERIBILITA'<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "H"

            End If

            If PAR.IfNull(myReader1111("REQUISITO10"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "MANCATA PRESENTAZIONE DOPO DIFFIDA<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "I"

            End If

            If PAR.IfNull(myReader1111("REQUISITO11"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "RINUNCIA<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "L"

            End If

            If PAR.IfNull(myReader1111("REQUISITO12"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "PERMESSO DI SOGGIORNO INFERIORE AL LIMITE<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "M"

            End If

            If PAR.IfNull(myReader1111("REQUISITO13"), "0") = "0" Then
                ISEE_ERP = 0
                ESCLUSIONE = ESCLUSIONE & "DICHIARAZIONE NON VERITIERA O DISCORDANTE<BR>"
                IDONEO_PRESUNTO = IDONEO_PRESUNTO & "N"

            End If



            L9 = ISEE_ERP 'Format(ISEE_ERP, "##,##0.00")

            L10 = 0
            L11 = 0
            L12 = 0
            L13 = 0
            L14 = 0

            Dim Imax As Integer
            Dim contributoAFF As Double = 0
            Dim perc_abbattimento As Double = 0
            Dim CANONE_INTEGRATO As Double


            CANONE_INTEGRATO = 0

            If ESCLUSIONE = "" Then
                Select Case ISEE_ERP
                    Case Is <= 3100
                        Imax = 10
                        perc_abbattimento = 50

                    Case Is <= 3615.2
                        Imax = 11
                        perc_abbattimento = 51.3

                    Case Is <= 4131.66
                        Imax = 12
                        perc_abbattimento = 52.4

                    Case Is <= 4648.11
                        Imax = 13
                        perc_abbattimento = 53.5

                    Case Is <= 5164.57
                        Imax = 14
                        perc_abbattimento = 54.7

                    Case Is <= 5681.03
                        Imax = 15
                        perc_abbattimento = 57.9

                    Case Is <= 6197.48
                        Imax = 16
                        perc_abbattimento = 66.9

                    Case Is <= 6713.94
                        Imax = 17
                        perc_abbattimento = 69.4

                    Case Is <= 7230.4
                        Imax = 18
                        perc_abbattimento = 70.9

                    Case Is <= 7746.85
                        Imax = 19
                        perc_abbattimento = 72.9

                    Case Is <= 8263.31
                        Imax = 20
                        perc_abbattimento = 74.9

                    Case Is <= 8779.77
                        Imax = 21
                        perc_abbattimento = 76.9

                    Case Is <= 9296.22
                        Imax = 22
                        perc_abbattimento = 77.4

                    Case Is <= 9812.68
                        Imax = 23
                        perc_abbattimento = 77.5

                    Case Is <= 10329.14
                        Imax = 24
                        perc_abbattimento = 77.6

                    Case Is <= 10845.59
                        Imax = 25
                        perc_abbattimento = 77.7

                    Case Is <= 11362.05
                        Imax = 26
                        perc_abbattimento = 77.7

                    Case Is <= 11878.51
                        Imax = 27
                        perc_abbattimento = 77.7

                    Case Is <= 12911.42
                        Imax = 28
                        perc_abbattimento = 77.7
                End Select
                L10 = Format(Imax, "0.00")
                If CDbl(PAR.IfNull(myReader1111("SPESE_LOCAZIONE"), "0")) > 7200 Then
                    L11 = 7200 'Format("7200", "##,##0.00")
                Else
                    L11 = CDbl(PAR.IfNull(myReader1111("SPESE_LOCAZIONE"), "0")) 'Format(CDbl(CType(Dom_Contratto1.FindControl("txtAffitto"), TextBox).Text), "##,##0.00")
                End If

                L12 = CDbl(PAR.IfNull(myReader1111("SPESE_CONDOMINIALI"), "0")) + CDbl(PAR.IfNull(myReader1111("SPESE_RISCALDAMENTO"), "0")) 'Format(CDbl(CType(Dom_Contratto1.FindControl("txtSpese"), TextBox).Text) + CDbl(CType(Dom_Contratto1.FindControl("txtRiscaldamento"), TextBox).Text), "##,##0.00")
                If L12 > 516 Then
                    L12 = 516
                End If

                L13 = L11 + L12 'Format(CDbl(L11) + CDbl(L12), "##,##0.00")

                L14 = (Imax * CDbl(L3)) / 100 'Format((Imax * CDbl(L3)) / 100, "##,##0.00")

                contributoAFF = ((L13 - L14) / 12) * Val(PAR.IfNull(myReader1111("MESI_CONTRATTI_REG"), "0"))




                If ISEE_ERP < 3100 Or ((ISE_ERP - L13) / VSE) < 2066 Then
                    maxContributo = 999999999
                Else
                    If TOT_COMPONENTI <= 2 Then
                        maxContributo = 2300
                    End If
                    If TOT_COMPONENTI >= 3 Then
                        maxContributo = 2300 + ((VSE - 1.57) * 460)
                    End If
                End If


                If contributoAFF > maxContributo Then
                    contributoAFF = maxContributo
                End If

                contributoAFF = contributoAFF - ((perc_abbattimento * contributoAFF) / 100)
                contributoAFF = Format(contributoAFF, "0.00")

                If contributoAFF < 100 Then
                    ISEE_ERP = 0
                    ESCLUSIONE = ESCLUSIONE & "CONTRIBUTO INFERIORE A 100,00 Euro<BR>"
                    IDONEO_PRESUNTO = IDONEO_PRESUNTO & "6"
                End If


                If ISEE_ERP < 3100 Or ((ISE_ERP - L13) / VSE) < 2066 Then
                    quotacomunale = Format((20 * contributoAFF) / 100, "0.00")
                    quotaregionale = Format(contributoAFF - quotacomunale, "0.00")

                    IDONEO_PRESUNTO = IDONEO_PRESUNTO & "3"
                Else
                    quotacomunale = Format((10 * contributoAFF) / 100, "0.00")
                    quotaregionale = Format(contributoAFF - quotacomunale, "0.00")

                    IDONEO_PRESUNTO = "0"
                End If

                CANONE_INTEGRATO = L13

            End If

            Dim dm_QuotaComunale As Double = 0
            Dim dm_QuotaRegionale As Double = 0
            Dim dm_QuotaTotale As Double = 0




            dm_QuotaComunale = quotacomunale
            dm_QuotaRegionale = quotaregionale
            dm_QuotaTotale = contributoAFF






            Dim statod As String = ""

            If ESCLUSIONE <> "" Then
                statod = "ID_STATO='4', "
                Response.Write("PG: " & PAR.IfNull(myReader1111("PG"), "0") & " DA IDONEA A RESPINTA!")
            Else
                statod = "ID_STATO='7a',FL_DA_LIQUIDARE='1' , "
            End If


            'If Format(ISEE_ERP, "0.00") <> Format(PAR.IfNull(myReader1111("REDDITO_ISEE"), "0"), "0.00") Then
            '    Beep()

            'End If
            PAR.cmd.CommandText = "UPDATE DOMANDE_BANDO_FSA SET " & statod & "IDONEO_PRESUNTO='" & IDONEO_PRESUNTO & "',fl_rinnovo='0',reddito_isee=" & PAR.VirgoleInPunti(ISEE_ERP) & ",isr_erp=" & PAR.VirgoleInPunti(ISR_ERP) & ",ise_erp=" & PAR.VirgoleInPunti(ISE_ERP) & ",isp_erp=" & PAR.VirgoleInPunti(ISP_ERP) & ",pse='" & VSE & "',vse='" & VSE & "',canone_int=" & PAR.VirgoleInPunti(CDbl(L13)) & ",canone_sup=" & PAR.VirgoleInPunti(CDbl(L14)) & ",tot_importo_erogato=" & PAR.VirgoleInPunti(Format(contributoAFF, "0")) & ",quotacomunalepagata=" & PAR.VirgoleInPunti(quotacomunale) & ",quotaregionalepagata=" & PAR.VirgoleInPunti(quotaregionale) & ",perc_abbattimento=" & PAR.VirgoleInPunti(perc_abbattimento) & " where id=" & lIdDomanda
            PAR.cmd.ExecuteNonQuery()

            sStringaSql = "INSERT INTO EVENTI_BANDI_FSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & lIdDomanda & ",1,'" & Format(Now, "yyyyMMddHHmmss") & "','" & "2" _
            & "','F141','ELAB. MASSIVA','I')"
            PAR.cmd.CommandText = sStringaSql
            PAR.cmd.ExecuteNonQuery()


            sStringaSql = "INSERT INTO EVENTI_BANDI_FSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & lIdDomanda & ",1,'" & Format(Now, "yyyyMMddHHmmss") & "','" & "2" _
            & "','F133','ELAB. MASSIVA','I')"
            PAR.cmd.CommandText = sStringaSql
            PAR.cmd.ExecuteNonQuery()


            If ESCLUSIONE <> "" Then
                sStringaSql = "INSERT INTO EVENTI_BANDI_FSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                & "VALUES (" & lIdDomanda & ",1,'" & Format(Now, "yyyyMMddHHmmss") & "','" & "2" _
                & "','F57','ELAB. MASSIVA - " & PAR.PulisciStrSql(Replace(ESCLUSIONE, "<BR>", " - ")) & "','I')"
                PAR.cmd.CommandText = sStringaSql
                PAR.cmd.ExecuteNonQuery()
            Else
                sStringaSql = "INSERT INTO EVENTI_BANDI_FSA (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                & "VALUES (" & lIdDomanda & ",1,'" & Format(Now, "yyyyMMddHHmmss") & "','" & "2" _
                & "','F02','ELAB. MASSIVA','I')"
                PAR.cmd.CommandText = sStringaSql
                PAR.cmd.ExecuteNonQuery()
            End If




        Loop


















    End Function

End Class
