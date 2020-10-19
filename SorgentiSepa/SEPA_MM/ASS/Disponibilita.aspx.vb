
Partial Class ASS_Disponibilita
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValorePG As String
    Dim s_Stringasql As String
    Dim M As Boolean
    Dim a As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                sValorePG = par.PulisciStrSql(Request.QueryString("PG"))
                sPG.Value = sValorePG
                sValoreID.Value = Request.QueryString("ID")
                Tipo.Value = Request.QueryString("T")

                If Tipo.Value = "2" Then

                    Tipo.Value = "ART.22 C.10"


                End If

                If Tipo.Value = "1" Then

                    Tipo.Value = "BANDO CAMBI"


                End If


                CaricaDati()

            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub





    Private Function ddlConfronto(ByVal ddl1 As Integer, ByVal ddl1ex As Integer) As Integer
        ddlConfronto = 0




        If (ddl1 = -1 And ddl1ex = -1) Then

            ddlConfronto = 0

            Exit Function
        End If




        If ddl1 = ddl1ex Then


            ddlConfronto = 1

        End If

        Return ddlConfronto
    End Function

    







    Private Sub CaricaDati()

        Try
            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            Dim sLocalita As String = ""
            Dim sLocalita2 As String = ""
            Dim sQuartiere As String = ""
            Dim sComplesso As String = ""
            Dim sEdificio As String = ""
            Dim sIndirizzo As String = ""
            Dim sPiano As String = ""
            Dim sImm As String = ""
            Dim sImm2 As String = ""
            Dim dtDaRiempire As New Data.DataTable





            dtDaRiempire.Columns.Add("ID_ALLOGGIO")
            dtDaRiempire.Columns.Add("ID_QUARTIERE")
            dtDaRiempire.Columns.Add("ID_PIANO")
            dtDaRiempire.Columns.Add("ID_COMUNE")
            dtDaRiempire.Columns.Add("COD_ALLOGGIO")
            dtDaRiempire.Columns.Add("COMUNE", Type.GetType("System.String"))
            dtDaRiempire.Columns.Add("ZONA")
            dtDaRiempire.Columns.Add("QUARTIERE")
            dtDaRiempire.Columns.Add("COMPLESSO")
            dtDaRiempire.Columns.Add("EDIFICIO")
            dtDaRiempire.Columns.Add("INDIRIZZO", Type.GetType("System.String"))
            dtDaRiempire.Columns.Add("NUM_LOCALI")
            dtDaRiempire.Columns.Add("PIANO")
            dtDaRiempire.Columns.Add("SUP")
            dtDaRiempire.Columns.Add("ELEVATORE")
            dtDaRiempire.Columns.Add("BARRIERE")
            dtDaRiempire.Columns.Add("CONDOMINIO")
            dtDaRiempire.Columns.Add("DATA_DISPONIBILITA")
            dtDaRiempire.Columns.Add("PROPRIETA")









            Select Case Tipo.Value

                Case "ART.22 C.10"

                    sLocalita = ""
                    sQuartiere = ""
                    sComplesso = ""
                    sEdificio = ""
                    sIndirizzo = ""
                    sImm = ""
                    sPiano = ""

                    par.cmd.CommandText = "SELECT trunc(DOMANDE_BANDO_vsa.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_vsa.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_vsa.DATA_PG,DOMANDE_BANDO_vsa.id_bando,DOMANDE_BANDO_vsa.PG,COMP_NUCLEO_vsa.COGNOME,COMP_NUCLEO_vsa.NOME FROM DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa WHERE DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=COMP_NUCLEO_vsa.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_vsa.PROGR=DOMANDE_BANDO_vsa.PROGR_COMPONENTE AND DOMANDE_BANDO_vsa.ID=" & sValoreID.Value
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        lbl_PG.Text = par.IfNull(myReader1("PG"), "")
                        lbl_nominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                        lbl_isbarc.Text = par.IfNull(myReader1("isbarc_r"), "0")
                        lbl_isee.Text = par.IfNull(myReader1("reddito_isee"), "0")
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO_vsa.ID) FROM COMP_NUCLEO_vsa,dichiarazioni_vsa,domande_bando_vsa WHERE DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND DOMANDE_BANDO_vsa.ID=" & sValoreID.Value
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        lbl_comp.Text = par.IfNull(myReader1(0), "0")
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO_vsa.ID) FROM COMP_NUCLEO_vsa,dichiarazioni_vsa,domande_bando_vsa WHERE comp_nucleo_vsa.perc_inval>=66 and DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND DOMANDE_BANDO_vsa.ID=" & sValoreID.Value
                    myReader1 = par.cmd.ExecuteReader()
                    lbl_invalidi.Text = "0"
                    If myReader1.Read() Then
                        lbl_invalidi.Text = par.IfNull(myReader1(0), "0")
                    End If
                    myReader1.Close()


                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO_vsa.ID) FROM COMP_NUCLEO_vsa,dichiarazioni_vsa,domande_bando_vsa WHERE (select MONTHS_BETWEEN(SYSDATE,to_char(TO_DATE(COMP_NUCLEO_vsa.DATA_NASCITA,'YYYYmmdd'),'DD/MON/YYYY')) from dual)>=780 and DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID AND DOMANDE_BANDO_vsa.ID=" & sValoreID.Value
                    myReader1 = par.cmd.ExecuteReader()
                    lbl_anziani.Text = "0"
                    If myReader1.Read() Then
                        lbl_anziani.Text = par.IfNull(myReader1(0), "0")
                    End If
                    myReader1.Close()




                    sZona = ""
                    par.cmd.CommandText = "SELECT domande_preferenze_vsa.pref_localita1, domande_preferenze_vsa.pref_localita2, domande_preferenze_vsa.pref_localita3, domande_preferenze_vsa.pref_localita4, domande_preferenze_vsa.pref_localita5, " _
                                               & " domande_preferenze_vsa.pref_quart1, domande_preferenze_vsa.pref_quart2, domande_preferenze_vsa.pref_quart3, domande_preferenze_vsa.pref_quart4, domande_preferenze_vsa.pref_quart5, " _
                                               & " domande_preferenze_vsa.pref_complesso1, domande_preferenze_vsa.pref_complesso2, domande_preferenze_vsa.pref_complesso3, domande_preferenze_vsa.pref_complesso4, domande_preferenze_vsa.pref_complesso5, " _
                                               & " domande_preferenze_vsa.pref_edificio1, domande_preferenze_vsa.pref_edificio2, domande_preferenze_vsa.pref_edificio3, domande_preferenze_vsa.pref_edificio4, domande_preferenze_vsa.pref_edificio5, " _
                                               & " domande_preferenze_vsa.pref_indirizzo1, domande_preferenze_vsa.pref_indirizzo2, domande_preferenze_vsa.pref_indirizzo3, domande_preferenze_vsa.pref_indirizzo4, domande_preferenze_vsa.pref_indirizzo5, " _
                                               & "domande_preferenze_escl_vsa.escl_localita1, domande_preferenze_escl_vsa.escl_localita2, domande_preferenze_escl_vsa.escl_localita3, domande_preferenze_escl_vsa.escl_localita4, domande_preferenze_escl_vsa.escl_localita5, " _
                                               & " domande_preferenze_escl_vsa.escl_quart1, domande_preferenze_escl_vsa.escl_quart2, domande_preferenze_escl_vsa.escl_quart3, domande_preferenze_escl_vsa.escl_quart4, domande_preferenze_escl_vsa.escl_quart5, " _
                                               & " domande_preferenze_vsa.pref_sup_max, domande_preferenze_vsa.pref_sup_min, " _
                                               & " domande_preferenze_vsa.pref_piani_da_con, domande_preferenze_vsa.pref_piani_a_con, " _
                                               & " domande_preferenze_vsa.pref_piani_da_senza, domande_preferenze_vsa.pref_piani_a_senza, " _
                                               & " domande_preferenze_vsa.pref_barriere AS barriere, " _
                                               & " domande_preferenze_vsa.pref_condominio AS condominio " _
                                               & " FROM domande_preferenze_vsa, domande_preferenze_escl_vsa " _
                                               & " WHERE domande_preferenze_vsa.id_domanda = domande_preferenze_escl_vsa.id_domanda " _
                                               & " And domande_preferenze_vsa.id_domanda = " & sValoreID.Value & ""

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then



                        M = False
                        If par.IfNull(myReader("PREF_LOCALITA1"), -1) <> -1 Then
                            sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA1"), -1) & " "
                            M = True
                        End If

                        If par.IfNull(myReader("PREF_LOCALITA2"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA2"), -1) & " "
                                M = True
                            Else

                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA2"), -1) & " "
                                M = True
                            End If
                        End If


                        If par.IfNull(myReader("PREF_LOCALITA3"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA3"), -1) & " "
                                M = True
                            Else


                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA3"), -1) & " "
                                M = True
                            End If
                        End If

                        If par.IfNull(myReader("PREF_LOCALITA4"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA4"), -1) & " "
                                M = True
                            Else

                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA4"), -1) & " "
                                M = True
                            End If
                        End If


                        If par.IfNull(myReader("PREF_LOCALITA5"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA5"), -1) & " "
                                M = True
                            Else
                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA5"), -1) & " "
                                M = True
                            End If
                        End If

                        If M = True Then sLocalita = sLocalita & ") "

                        sLocalita2 = sLocalita

                        'controllo nel caso in cui le località di preferenza/esclusione siano uguali

                        Dim pref_loc1 As Integer = par.IfNull(myReader("PREF_LOCALITA1"), -1)
                        Dim pref_loc2 As Integer = par.IfNull(myReader("PREF_LOCALITA2"), -1)
                        Dim pref_loc3 As Integer = par.IfNull(myReader("PREF_LOCALITA3"), -1)
                        Dim pref_loc4 As Integer = par.IfNull(myReader("PREF_LOCALITA4"), -1)
                        Dim pref_loc5 As Integer = par.IfNull(myReader("PREF_LOCALITA5"), -1)

                        Dim escl_loc1 As Integer = par.IfNull(myReader("ESCL_LOCALITA1"), -1)
                        Dim escl_loc2 As Integer = par.IfNull(myReader("ESCL_LOCALITA2"), -1)
                        Dim escl_loc3 As Integer = par.IfNull(myReader("ESCL_LOCALITA3"), -1)
                        Dim escl_loc4 As Integer = par.IfNull(myReader("ESCL_LOCALITA4"), -1)
                        Dim escl_loc5 As Integer = par.IfNull(myReader("ESCL_LOCALITA5"), -1)

                        Dim beccato As Integer = 0


                        If ddlConfronto(pref_loc1, escl_loc1) <> 0 Or ddlConfronto(pref_loc1, escl_loc2) <> 0 Or ddlConfronto(pref_loc1, escl_loc3) <> 0 Or ddlConfronto(pref_loc1, escl_loc4) <> 0 Or ddlConfronto(pref_loc1, escl_loc5) <> 0 Then
                            beccato = 1
                        End If

                        If ddlConfronto(pref_loc2, escl_loc1) <> 0 Or ddlConfronto(pref_loc2, escl_loc2) <> 0 Or ddlConfronto(pref_loc2, escl_loc3) <> 0 Or ddlConfronto(pref_loc2, escl_loc4) <> 0 Or ddlConfronto(pref_loc2, escl_loc5) <> 0 Then
                            beccato = 1

                        End If

                        If ddlConfronto(pref_loc3, escl_loc1) <> 0 Or ddlConfronto(pref_loc3, escl_loc2) <> 0 Or ddlConfronto(pref_loc3, escl_loc3) <> 0 Or ddlConfronto(pref_loc3, escl_loc4) <> 0 Or ddlConfronto(pref_loc3, escl_loc5) <> 0 Then
                            beccato = 1

                        End If


                        If ddlConfronto(pref_loc4, escl_loc1) <> 0 Or ddlConfronto(pref_loc4, escl_loc2) <> 0 Or ddlConfronto(pref_loc4, escl_loc3) <> 0 Or ddlConfronto(pref_loc4, escl_loc4) <> 0 Or ddlConfronto(pref_loc4, escl_loc5) <> 0 Then
                            beccato = 1
                        End If



                        If ddlConfronto(pref_loc5, escl_loc1) <> 0 Or ddlConfronto(pref_loc5, escl_loc2) <> 0 Or ddlConfronto(pref_loc5, escl_loc3) <> 0 Or ddlConfronto(pref_loc5, escl_loc4) <> 0 Or ddlConfronto(pref_loc5, escl_loc5) <> 0 Then
                            beccato = 1
                        End If



                        If beccato = 1 Then
                            Dim N As Boolean = False

                            If par.IfNull(myReader("ESCL_QUART1"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART1"), -1) & ") "
                                N = True

                            End If


                            If par.IfNull(myReader("ESCL_QUART2"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART2"), -1) & ") "
                                N = True

                            End If


                            If par.IfNull(myReader("ESCL_QUART3"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART3"), -1) & ") "
                                N = True

                            End If


                            If par.IfNull(myReader("ESCL_QUART4"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART4"), -1) & ") "
                                N = True

                            End If

                            If par.IfNull(myReader("ESCL_QUART5"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART5"), -1) & ") "
                                N = True

                            End If



                        End If






                        '---------------------------------


                        M = False

                        If par.IfNull(myReader("PREF_QUART1"), -1) <> -1 Then
                            '  If M = True Then
                            sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART1"), -1) & " "
                            '   Else
                            ' sQuartiere = " (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART1"), -1) & " "
                            M = True
                            '  End If
                        End If



                        If par.IfNull(myReader("PREF_QUART2"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART2"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_QUART1"), -1) <> -1 Then

                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART2"), -1) & " "
                                    M = True
                                Else

                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART2"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_QUART3"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART3"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 Then

                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART3"), -1) & " "
                                    M = True
                                Else

                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART3"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_QUART4"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART4"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 Then

                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART4"), -1) & " "
                                    M = True
                                Else

                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART4"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_QUART5"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART5"), "") & " "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 And par.IfNull(myReader("PREF_QUART5"), -1) = -1 Then


                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART5"), -1) & " "
                                    M = True

                                Else
                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART5"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_QUART5"), -1) = -1 And par.IfNull(myReader("PREF_QUART4"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART1"), -1) = -1 Then


                        Else
                            If M = True Then sQuartiere = sQuartiere & ") "

                        End If





                        M = False

                        If par.IfNull(myReader("PREF_COMPLESSO1"), -1) <> -1 Then
                            '  If M = True Then
                            sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO1"), -1) & " "
                            '    Else
                            ' sComplesso = " (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO1"), -1) & " "
                            M = True
                            '   End If
                        End If



                        If par.IfNull(myReader("PREF_COMPLESSO2"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO2"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) <> -1 Then

                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO2"), -1) & " "
                                    M = True
                                Else

                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO2"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO3"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO3"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 Then

                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO3"), -1) & " "
                                    M = True
                                Else

                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO3"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO4"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO4"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 Then

                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO4"), -1) & " "
                                    M = True
                                Else

                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO4"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO5"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO5"), "") & " "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO5"), -1) = -1 Then


                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO5"), -1) & " "
                                    M = True

                                Else
                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO5"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_COMPLESSO5"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO4"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 Then


                        Else
                            If M = True Then sComplesso = sComplesso & ") "

                        End If










                        M = False

                        If par.IfNull(myReader("PREF_EDIFICIO1"), -1) <> -1 Then
                            '  If M = True Then
                            sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO1"), -1) & " "
                            'Else
                            ' sEdificio = " (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO1"), -1) & " "
                            M = True
                            '  End If
                        End If



                        If par.IfNull(myReader("PREF_EDIFICIO2"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO2"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) <> -1 Then

                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO2"), -1) & " "
                                    M = True
                                Else

                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO2"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_EDIFICIO3"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO3"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 Then

                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO3"), -1) & " "
                                    M = True
                                Else

                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO3"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_EDIFICIO4"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO4"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 Then

                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO4"), -1) & " "
                                    M = True
                                Else

                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO4"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_EDIFICIO5"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO5"), "") & " "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO5"), -1) = -1 Then


                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO5"), -1) & " "
                                    M = True

                                Else
                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO5"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_EDIFICIO5"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO4"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 Then


                        Else
                            If M = True Then sEdificio = sEdificio & ") "

                        End If








                        M = False

                        If par.IfNull(myReader("PREF_INDIRIZZO1"), "") <> "" Then
                            '    If M = True Then
                            sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO1"), "") & "' "
                            '   Else
                            '  sIndirizzo = " AND (INDIRIZZO='" & par.IfNull(myReader("PREF_INDIRIZZO1"), "") & "' "
                            M = True
                            'End If
                        End If



                        If par.IfNull(myReader("PREF_INDIRIZZO2"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & "AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO2"), "") & "' "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_INDIRIZZO1"), -1) <> "" Then

                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO2"), "") & "' "
                                    M = True
                                Else

                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO2"), "") & "' "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_INDIRIZZO3"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & "AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO3"), "") & "' "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" Then

                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO3"), -1) & "' "
                                    M = True
                                Else

                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO3"), -1) & "' "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_INDIRIZZO4"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & "AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO4"), "") & "' "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" Then

                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO4"), "") & "' "
                                    M = True
                                Else

                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO4"), "") & "' "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_INDIRIZZO5"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO5"), "") & "' "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO5"), "") = "" Then


                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO5"), "") & "' "
                                    M = True

                                Else
                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO5"), "") & "' "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_INDIRIZZO5"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO4"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" Then


                        Else
                            If M = True Then sIndirizzo = sIndirizzo & ") "

                        End If


                        'piani------------------------------------------------------------------------

                        M = False


                        'piani Con Ascensore-----------------
                        Dim dtP As New Data.DataTable
                        dtP = caricaDTpiani()
                        Dim k As Integer = 0

                        Dim trovatoDa As Integer = 0
                        If par.IfNull(myReader("PREF_PIANI_DA_CON"), "-1") <> "-1" Then
                            If par.IfNull(myReader("PREF_PIANI_A_CON"), "-1") <> "-1" Then
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If trovatoDa = 1 Then
                                        If rowScart.ItemArray(0) <> par.IfNull(myReader("PREF_PIANI_A_CON"), "-1") Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        Else
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "' "
                                            Exit Do
                                        End If
                                    End If
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_CON"), "-1") Then
                                        trovatoDa = 1
                                        sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                    End If

                                    k = k + 1
                                Loop

                            Else
                                trovatoDa = 0
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_CON"), "-1") Then
                                        trovatoDa = 1
                                        sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                    Else
                                        If trovatoDa = 1 Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        End If
                                    End If
                                    k = k + 1
                                Loop

                            End If
                            M = True
                            sPiano = sPiano & ") AND ALLOGGI.ASCENSORE='1') "


                        Else

                            sPiano = sPiano & " AND ((ALLOGGI.ASCENSORE='1') "

                            M = True

                        End If



                        'piani Senza Ascensore-----------------

                        trovatoDa = 0

                        If par.IfNull(myReader("PREF_PIANI_DA_SENZA"), "-1") <> "-1" Then
                            If par.IfNull(myReader("PREF_PIANI_A_SENZA"), "-1") <> "-1" Then
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If trovatoDa = 1 Then
                                        If rowScart.ItemArray(0) <> par.IfNull(myReader("PREF_PIANI_A_SENZA"), "-1") Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        Else
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "' "
                                            Exit Do
                                        End If
                                    End If
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_SENZA"), "-1") Then
                                        If M = False Then
                                            sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                            M = True
                                        Else
                                            sPiano = sPiano & " OR (TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                        End If
                                        trovatoDa = 1
                                    End If

                                    k = k + 1
                                Loop

                            Else
                                trovatoDa = 0
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_SENZA"), "-1") Then
                                        trovatoDa = 1
                                        If M = False Then
                                            sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                            M = True
                                        Else
                                            sPiano = sPiano & " OR (TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                        End If
                                    Else
                                        If trovatoDa = 1 Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        End If
                                    End If
                                    k = k + 1
                                Loop

                            End If
                            sPiano = sPiano & ") AND ALLOGGI.ASCENSORE<>'1')) "

                        Else
                            If M = True Then
                                sPiano = sPiano & "or (ALLOGGI.ASCENSORE<>'1')) "
                            End If

                        End If



                        sPiano = sPiano











                        '---------------------------------------------------------------



                        '-----------------parti restanti

                        sImm2 = ""

                        If sLocalita = "" And sQuartiere = "" And sComplesso = "" And sEdificio = "" And sIndirizzo = "" Then

                            M = False
                        Else
                            M = True
                        End If


                        If par.IfNull(myReader("CONDOMINIO"), "0") = "1" Then

                            sImm = sImm & " AND cond_edifici.id_condominio IS NULL"


                        End If





                        If par.IfNull(myReader("BARRIERE"), "0") = "1" Then

                            sImm = sImm & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            sImm2 = sImm2 & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "

                        End If

                        'opzioni per superficie


                        If par.IfNull(myReader("pref_sup_min"), -1) <> -1 And par.IfNull(myReader("pref_sup_max"), -1) <> -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID and valore between " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & " and " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ")"
                            sImm2 = sImm2 & " and alloggi.sup >=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & " and and alloggi.sup <=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ""
                        End If


                        If par.IfNull(myReader("pref_sup_min"), -1) <> -1 And par.IfNull(myReader("pref_sup_max"), -1) = -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID and valore >= " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & ")"
                            sImm2 = sImm2 & " and alloggi.sup >=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & ""
                        End If

                        If par.IfNull(myReader("pref_sup_min"), -1) = -1 And par.IfNull(myReader("pref_sup_max"), -1) <> -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID and valore <= " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ")"
                            sImm2 = sImm2 & " and alloggi.sup <=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ""
                        End If


                        If par.IfNull(myReader("pref_sup_min"), -1) = -1 And par.IfNull(myReader("pref_sup_max"), -1) = -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID" & ")"
                        End If



                        'If sImm <> "" Then

                        'Else


                        'sZona = " and alloggi.id=00000"
                        If lbl_invalidi.Text <> "0" Then
                            sImm = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL)"
                            sImm2 = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL)"
                        End If
                        '   BindGrid1()
                        'End If







                        '--------------------------QUERY------------------------------

                        If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO4"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO5"), "") = "" Then

                        Else

                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                        & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                         & " dimensioni.valore as sup, " _
                        & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                        & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                        & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                         & " complessi_immobiliari.denominazione AS complesso, " _
                        & " edifici.denominazione AS edificio, " _
                        & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                        & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                        & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                        & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                        & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                        & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                        & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                        & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                        & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                        & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                        & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                        & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                        & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                        & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                        & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                        & " And comuni_nazioni.cod = edifici.cod_comune " _
                        & " " & sIndirizzo & " " _
                        & " " & sPiano & " " _
                         & " " & sImm & " " _
                        & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sIndirizzo & " " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt As New Data.DataTable
                            da.Fill(dt)

                            Dim RIGA1 As System.Data.DataRow
                            For i As Integer = 0 To dt.Rows.Count - 1
                                RIGA1 = dtDaRiempire.NewRow()
                                RIGA1.Item("ID_ALLOGGIO") = par.IfNull(dt.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA1.Item("ID_QUARTIERE") = par.IfNull(dt.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA1.Item("ID_PIANO") = par.IfNull(dt.Rows(i).Item("ID_PIANO"), "")
                                RIGA1.Item("ID_COMUNE") = par.IfNull(dt.Rows(i).Item("ID_COMUNE"), "")
                                RIGA1.Item("COD_ALLOGGIO") = par.IfNull(dt.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA1.Item("COMUNE") = par.IfNull(dt.Rows(i).Item("COMUNE"), "")
                                RIGA1.Item("ZONA") = par.IfNull(dt.Rows(i).Item("ZONA"), "")
                                RIGA1.Item("QUARTIERE") = par.IfNull(dt.Rows(i).Item("QUARTIERE"), "")
                                RIGA1.Item("COMPLESSO") = par.IfNull(dt.Rows(i).Item("COMPLESSO"), "")
                                RIGA1.Item("EDIFICIO") = par.IfNull(dt.Rows(i).Item("EDIFICIO"), "")
                                RIGA1.Item("INDIRIZZO") = par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")
                                RIGA1.Item("NUM_LOCALI") = par.IfNull(dt.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA1.Item("PIANO") = par.IfNull(dt.Rows(i).Item("PIANO"), "")
                                RIGA1.Item("SUP") = par.IfNull(dt.Rows(i).Item("SUP"), "")
                                RIGA1.Item("ELEVATORE") = par.IfNull(dt.Rows(i).Item("ELEVATORE"), "")
                                RIGA1.Item("BARRIERE") = par.IfNull(dt.Rows(i).Item("BARRIERE"), "")
                                RIGA1.Item("CONDOMINIO") = par.IfNull(dt.Rows(i).Item("CONDOMINIO"), "")
                                RIGA1.Item("DATA_DISPONIBILITA") = par.IfNull(dt.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA1.Item("PROPRIETA") = par.IfNull(dt.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA1)
                            Next

                        End If

                        If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO4"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO5"), -1) = -1 Then

                        Else

                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                      & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                    & " dimensioni.valore as sup, " _
                      & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                      & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                      & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                       & " complessi_immobiliari.denominazione AS complesso, " _
                      & " edifici.denominazione AS edificio, " _
                      & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                      & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                      & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                      & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                      & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                      & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                      & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                      & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                      & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                      & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                      & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                      & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                      & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                      & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                      & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                      & " And comuni_nazioni.cod = edifici.cod_comune " _
                      & " " & sEdificio & " " _
                      & " " & sPiano & " " _
                      & " " & sImm & " " _
                       & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt2 As New Data.DataTable
                            da2.Fill(dt2)

                            Dim RIGA2 As System.Data.DataRow
                            For i As Integer = 0 To dt2.Rows.Count - 1
                                RIGA2 = dtDaRiempire.NewRow()
                                RIGA2.Item("ID_ALLOGGIO") = par.IfNull(dt2.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA2.Item("ID_QUARTIERE") = par.IfNull(dt2.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA2.Item("ID_PIANO") = par.IfNull(dt2.Rows(i).Item("ID_PIANO"), "")
                                RIGA2.Item("ID_COMUNE") = par.IfNull(dt2.Rows(i).Item("ID_COMUNE"), "")
                                RIGA2.Item("COD_ALLOGGIO") = par.IfNull(dt2.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA2.Item("COMUNE") = par.IfNull(dt2.Rows(i).Item("COMUNE"), "")
                                RIGA2.Item("ZONA") = par.IfNull(dt2.Rows(i).Item("ZONA"), "")
                                RIGA2.Item("QUARTIERE") = par.IfNull(dt2.Rows(i).Item("QUARTIERE"), "")
                                RIGA2.Item("COMPLESSO") = par.IfNull(dt2.Rows(i).Item("COMPLESSO"), "")
                                RIGA2.Item("EDIFICIO") = par.IfNull(dt2.Rows(i).Item("EDIFICIO"), "")
                                RIGA2.Item("INDIRIZZO") = par.IfNull(dt2.Rows(i).Item("INDIRIZZO"), "")
                                RIGA2.Item("NUM_LOCALI") = par.IfNull(dt2.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA2.Item("PIANO") = par.IfNull(dt2.Rows(i).Item("PIANO"), "")
                                RIGA2.Item("SUP") = par.IfNull(dt2.Rows(i).Item("SUP"), "")
                                RIGA2.Item("ELEVATORE") = par.IfNull(dt2.Rows(i).Item("ELEVATORE"), "")
                                RIGA2.Item("BARRIERE") = par.IfNull(dt2.Rows(i).Item("BARRIERE"), "")
                                RIGA2.Item("CONDOMINIO") = par.IfNull(dt2.Rows(i).Item("CONDOMINIO"), "")
                                RIGA2.Item("DATA_DISPONIBILITA") = par.IfNull(dt2.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA2.Item("PROPRIETA") = par.IfNull(dt2.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA2)
                            Next

                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO4"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO5"), -1) = -1 Then

                        Else

                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                     & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                      & " dimensioni.valore as sup, " _
                     & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                     & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                     & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                      & " complessi_immobiliari.denominazione AS complesso, " _
                     & " edifici.denominazione AS edificio, " _
                     & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                     & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                     & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                     & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                     & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                     & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                     & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                     & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                     & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                     & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                     & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                     & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                     & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                     & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                     & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                     & " And comuni_nazioni.cod = edifici.cod_comune " _
                     & " " & sComplesso & " " _
                     & " " & sPiano & " " _
                     & " " & sImm & " " _
                     & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt3 As New Data.DataTable
                            da3.Fill(dt3)

                            Dim RIGA3 As System.Data.DataRow
                            For i As Integer = 0 To dt3.Rows.Count - 1
                                RIGA3 = dtDaRiempire.NewRow()
                                RIGA3.Item("ID_ALLOGGIO") = par.IfNull(dt3.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA3.Item("ID_QUARTIERE") = par.IfNull(dt3.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA3.Item("ID_PIANO") = par.IfNull(dt3.Rows(i).Item("ID_PIANO"), "")
                                RIGA3.Item("ID_COMUNE") = par.IfNull(dt3.Rows(i).Item("ID_COMUNE"), "")
                                RIGA3.Item("COD_ALLOGGIO") = par.IfNull(dt3.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA3.Item("COMUNE") = par.IfNull(dt3.Rows(i).Item("COMUNE"), "")
                                RIGA3.Item("ZONA") = par.IfNull(dt3.Rows(i).Item("ZONA"), "")
                                RIGA3.Item("QUARTIERE") = par.IfNull(dt3.Rows(i).Item("QUARTIERE"), "")
                                RIGA3.Item("COMPLESSO") = par.IfNull(dt3.Rows(i).Item("COMPLESSO"), "")
                                RIGA3.Item("EDIFICIO") = par.IfNull(dt3.Rows(i).Item("EDIFICIO"), "")
                                RIGA3.Item("INDIRIZZO") = par.IfNull(dt3.Rows(i).Item("INDIRIZZO"), "")
                                RIGA3.Item("NUM_LOCALI") = par.IfNull(dt3.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA3.Item("PIANO") = par.IfNull(dt3.Rows(i).Item("PIANO"), "")
                                RIGA3.Item("SUP") = par.IfNull(dt3.Rows(i).Item("SUP"), "")
                                RIGA3.Item("ELEVATORE") = par.IfNull(dt3.Rows(i).Item("ELEVATORE"), "")
                                RIGA3.Item("BARRIERE") = par.IfNull(dt3.Rows(i).Item("BARRIERE"), "")
                                RIGA3.Item("CONDOMINIO") = par.IfNull(dt3.Rows(i).Item("CONDOMINIO"), "")
                                RIGA3.Item("DATA_DISPONIBILITA") = par.IfNull(dt3.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA3.Item("PROPRIETA") = par.IfNull(dt3.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA3)
                            Next


                        End If

                        If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 And par.IfNull(myReader("PREF_QUART4"), -1) = -1 And par.IfNull(myReader("PREF_QUART5"), -1) = -1 Then

                        Else


                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                        & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                        & " dimensioni.valore as sup, " _
                        & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                        & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                        & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                         & " complessi_immobiliari.denominazione AS complesso, " _
                        & " edifici.denominazione AS edificio, " _
                        & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                        & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                        & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                        & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                        & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                        & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                        & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                        & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                        & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                        & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                        & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                        & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                        & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                        & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                        & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                        & " And comuni_nazioni.cod = edifici.cod_comune " _
                        & " " & sQuartiere & " " _
                        & " " & sPiano & " " _
                        & " " & sImm & " " _
                        & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt4 As New Data.DataTable
                            da4.Fill(dt4)

                            Dim RIGA4 As System.Data.DataRow
                            For i As Integer = 0 To dt4.Rows.Count - 1
                                RIGA4 = dtDaRiempire.NewRow()
                                RIGA4.Item("ID_ALLOGGIO") = par.IfNull(dt4.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA4.Item("ID_QUARTIERE") = par.IfNull(dt4.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA4.Item("ID_PIANO") = par.IfNull(dt4.Rows(i).Item("ID_PIANO"), "")
                                RIGA4.Item("ID_COMUNE") = par.IfNull(dt4.Rows(i).Item("ID_COMUNE"), "")
                                RIGA4.Item("COD_ALLOGGIO") = par.IfNull(dt4.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA4.Item("COMUNE") = par.IfNull(dt4.Rows(i).Item("COMUNE"), "")
                                RIGA4.Item("ZONA") = par.IfNull(dt4.Rows(i).Item("ZONA"), "")
                                RIGA4.Item("QUARTIERE") = par.IfNull(dt4.Rows(i).Item("QUARTIERE"), "")
                                RIGA4.Item("COMPLESSO") = par.IfNull(dt4.Rows(i).Item("COMPLESSO"), "")
                                RIGA4.Item("EDIFICIO") = par.IfNull(dt4.Rows(i).Item("EDIFICIO"), "")
                                RIGA4.Item("INDIRIZZO") = par.IfNull(dt4.Rows(i).Item("INDIRIZZO"), "")
                                RIGA4.Item("NUM_LOCALI") = par.IfNull(dt4.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA4.Item("PIANO") = par.IfNull(dt4.Rows(i).Item("PIANO"), "")
                                RIGA4.Item("SUP") = par.IfNull(dt4.Rows(i).Item("SUP"), "")
                                RIGA4.Item("ELEVATORE") = par.IfNull(dt4.Rows(i).Item("ELEVATORE"), "")
                                RIGA4.Item("BARRIERE") = par.IfNull(dt4.Rows(i).Item("BARRIERE"), "")
                                RIGA4.Item("CONDOMINIO") = par.IfNull(dt4.Rows(i).Item("CONDOMINIO"), "")
                                RIGA4.Item("DATA_DISPONIBILITA") = par.IfNull(dt4.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA4.Item("PROPRIETA") = par.IfNull(dt4.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA4)
                            Next

                        End If


                        sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                 & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                 & " dimensioni.valore as sup, " _
                 & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                 & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                 & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                  & " complessi_immobiliari.denominazione AS complesso, " _
                 & " edifici.denominazione AS edificio, " _
                 & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                 & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                 & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                 & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                 & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                 & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                 & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                 & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                 & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                 & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                 & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                 & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                 & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                 & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                 & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                 & " And comuni_nazioni.cod = edifici.cod_comune " _
                 & " " & sLocalita & " " _
                 & " " & sPiano & " " _
                 & " " & sImm & " " _
                    & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sLocalita2 & " " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                        Dim da5 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                        Dim dt5 As New Data.DataTable
                        da5.Fill(dt5)

                        Dim RIGA5 As System.Data.DataRow
                        For i As Integer = 0 To dt5.Rows.Count - 1
                            RIGA5 = dtDaRiempire.NewRow()
                            RIGA5.Item("ID_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("ID_ALLOGGIO"), "")
                            RIGA5.Item("ID_QUARTIERE") = par.IfNull(dt5.Rows(i).Item("ID_QUARTIERE"), "")
                            RIGA5.Item("ID_PIANO") = par.IfNull(dt5.Rows(i).Item("ID_PIANO"), "")
                            RIGA5.Item("ID_COMUNE") = par.IfNull(dt5.Rows(i).Item("ID_COMUNE"), "")
                            RIGA5.Item("COD_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("COD_ALLOGGIO"), "")
                            RIGA5.Item("COMUNE") = par.IfNull(dt5.Rows(i).Item("COMUNE"), "")
                            RIGA5.Item("ZONA") = par.IfNull(dt5.Rows(i).Item("ZONA"), "")
                            RIGA5.Item("QUARTIERE") = par.IfNull(dt5.Rows(i).Item("QUARTIERE"), "")
                            RIGA5.Item("COMPLESSO") = par.IfNull(dt5.Rows(i).Item("COMPLESSO"), "")
                            RIGA5.Item("EDIFICIO") = par.IfNull(dt5.Rows(i).Item("EDIFICIO"), "")
                            RIGA5.Item("INDIRIZZO") = par.IfNull(dt5.Rows(i).Item("INDIRIZZO"), "")
                            RIGA5.Item("NUM_LOCALI") = par.IfNull(dt5.Rows(i).Item("NUM_LOCALI"), "")
                            RIGA5.Item("PIANO") = par.IfNull(dt5.Rows(i).Item("PIANO"), "")
                            RIGA5.Item("SUP") = par.IfNull(dt5.Rows(i).Item("SUP"), "")
                            RIGA5.Item("ELEVATORE") = par.IfNull(dt5.Rows(i).Item("ELEVATORE"), "")
                            RIGA5.Item("BARRIERE") = par.IfNull(dt5.Rows(i).Item("BARRIERE"), "")
                            RIGA5.Item("CONDOMINIO") = par.IfNull(dt5.Rows(i).Item("CONDOMINIO"), "")
                            RIGA5.Item("DATA_DISPONIBILITA") = par.IfNull(dt5.Rows(i).Item("DATA_DISPONIBILITA"), "")
                            RIGA5.Item("PROPRIETA") = par.IfNull(dt5.Rows(i).Item("PROPRIETA"), "")
                            dtDaRiempire.Rows.Add(RIGA5)
                        Next

                    Else
                        If lbl_invalidi.Text <> "0" Then
                            sImm = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL)"
                            sImm2 = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                        End If

                        sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID" & ")"

                        sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                 & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                & " dimensioni.valore as sup, " _
                 & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                 & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                 & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                  & " complessi_immobiliari.denominazione AS complesso, " _
                 & " edifici.denominazione AS edificio, " _
                 & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                 & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                 & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                 & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                 & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                 & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                 & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                 & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                 & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                 & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                 & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                 & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                 & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                 & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                 & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                 & " And comuni_nazioni.cod = edifici.cod_comune " _
                 & " " & sLocalita & " " _
                 & " " & sPiano & " " _
                 & " " & sImm & " " _
                 & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sLocalita & " " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                        Dim da5 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                        Dim dt5 As New Data.DataTable
                        da5.Fill(dt5)

                        Dim RIGA5 As System.Data.DataRow
                        For i As Integer = 0 To dt5.Rows.Count - 1
                            RIGA5 = dtDaRiempire.NewRow()
                            RIGA5.Item("ID_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("ID_ALLOGGIO"), "")
                            RIGA5.Item("ID_QUARTIERE") = par.IfNull(dt5.Rows(i).Item("ID_QUARTIERE"), "")
                            RIGA5.Item("ID_PIANO") = par.IfNull(dt5.Rows(i).Item("ID_PIANO"), "")
                            RIGA5.Item("ID_COMUNE") = par.IfNull(dt5.Rows(i).Item("ID_COMUNE"), "")
                            RIGA5.Item("COD_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("COD_ALLOGGIO"), "")
                            RIGA5.Item("COMUNE") = par.IfNull(dt5.Rows(i).Item("COMUNE"), "")
                            RIGA5.Item("ZONA") = par.IfNull(dt5.Rows(i).Item("ZONA"), "")
                            RIGA5.Item("QUARTIERE") = par.IfNull(dt5.Rows(i).Item("QUARTIERE"), "")
                            RIGA5.Item("COMPLESSO") = par.IfNull(dt5.Rows(i).Item("COMPLESSO"), "")
                            RIGA5.Item("EDIFICIO") = par.IfNull(dt5.Rows(i).Item("EDIFICIO"), "")
                            RIGA5.Item("INDIRIZZO") = par.IfNull(dt5.Rows(i).Item("INDIRIZZO"), "")
                            RIGA5.Item("NUM_LOCALI") = par.IfNull(dt5.Rows(i).Item("NUM_LOCALI"), "")
                            RIGA5.Item("PIANO") = par.IfNull(dt5.Rows(i).Item("PIANO"), "")
                            RIGA5.Item("SUP") = par.IfNull(dt5.Rows(i).Item("SUP"), "")
                            RIGA5.Item("ELEVATORE") = par.IfNull(dt5.Rows(i).Item("ELEVATORE"), "")
                            RIGA5.Item("BARRIERE") = par.IfNull(dt5.Rows(i).Item("BARRIERE"), "")
                            RIGA5.Item("CONDOMINIO") = par.IfNull(dt5.Rows(i).Item("CONDOMINIO"), "")
                            RIGA5.Item("DATA_DISPONIBILITA") = par.IfNull(dt5.Rows(i).Item("DATA_DISPONIBILITA"), "")
                            RIGA5.Item("PROPRIETA") = par.IfNull(dt5.Rows(i).Item("PROPRIETA"), "")
                            dtDaRiempire.Rows.Add(RIGA5)
                        Next



                    End If





                    BindGrid2()




                    dtDaRiempire = DeleteDuplicateFromDataTable(dtDaRiempire, "ID_ALLOGGIO")
                    DataGrid2.DataSource = dtDaRiempire
                    DataGrid2.DataBind()


                    'DataGrid2.DataSource = dtDaRiempire
                    'DataGrid2.DataBind()
                    myReader.Close()

                    lbl_UIPref.Text = "ELENCO UNITA' DISPONIBILI PER PREFERENZE - Totale: " & dtDaRiempire.Rows.Count & " Unità"
                    'rimuovere doppioni da datagrid






                    '----------------------------




                Case "BANDO CAMBI"


                    par.cmd.CommandText = "SELECT trunc(DOMANDE_BANDO_cambi.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_cambi.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_cambi.DATA_PG,DOMANDE_BANDO_cambi.id_bando,DOMANDE_BANDO_cambi.PG,COMP_NUCLEO_cambi.COGNOME,COMP_NUCLEO_cambi.NOME FROM DOMANDE_BANDO_cambi,COMP_NUCLEO_cambi WHERE DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=COMP_NUCLEO_cambi.ID_DICHIARAZIONE (+) AND COMP_NUCLEO_cambi.PROGR=DOMANDE_BANDO_cambi.PROGR_COMPONENTE AND DOMANDE_BANDO_cambi.ID=" & sValoreID.Value
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        lbl_PG.Text = par.IfNull(myReader1("PG"), "")
                        lbl_nominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                        lbl_isbarc.Text = par.IfNull(myReader1("isbarc_r"), "0")
                        lbl_isee.Text = par.IfNull(myReader1("reddito_isee"), "0")
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO_cambi.ID) FROM COMP_NUCLEO_cambi,dichiarazioni_cambi,domande_bando_cambi WHERE DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND DOMANDE_BANDO_cambi.ID=" & sValoreID.Value
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        lbl_comp.Text = par.IfNull(myReader1(0), "0")
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO_cambi.ID) FROM COMP_NUCLEO_cambi,dichiarazioni_cambi,domande_bando_cambi WHERE comp_nucleo_cambi.perc_inval>=66 and DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND DOMANDE_BANDO_cambi.ID=" & sValoreID.Value
                    myReader1 = par.cmd.ExecuteReader()
                    lbl_invalidi.Text = "0"
                    If myReader1.Read() Then
                        lbl_invalidi.Text = par.IfNull(myReader1(0), "0")
                    End If
                    myReader1.Close()


                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO_cambi.ID) FROM COMP_NUCLEO_cambi,dichiarazioni_cambi,domande_bando_cambi WHERE (select MONTHS_BETWEEN(SYSDATE,to_char(TO_DATE(COMP_NUCLEO_cambi.DATA_NASCITA,'YYYYmmdd'),'DD/MON/YYYY')) from dual)>=780 and DOMANDE_BANDO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND COMP_NUCLEO_cambi.ID_DICHIARAZIONE=DICHIARAZIONI_cambi.ID AND DOMANDE_BANDO_cambi.ID=" & sValoreID.Value
                    myReader1 = par.cmd.ExecuteReader()
                    lbl_anziani.Text = "0"
                    If myReader1.Read() Then
                        lbl_anziani.Text = par.IfNull(myReader1(0), "0")
                    End If
                    myReader1.Close()






                    sZona = ""
                    par.cmd.CommandText = "SELECT domande_preferenze_cambi.pref_localita1, domande_preferenze_cambi.pref_localita2, domande_preferenze_cambi.pref_localita3, domande_preferenze_cambi.pref_localita4, domande_preferenze_cambi.pref_localita5, " _
                                              & " domande_preferenze_cambi.pref_quart1, domande_preferenze_cambi.pref_quart2, domande_preferenze_cambi.pref_quart3, domande_preferenze_cambi.pref_quart4, domande_preferenze_cambi.pref_quart5, " _
                                              & " domande_preferenze_cambi.pref_complesso1, domande_preferenze_cambi.pref_complesso2, domande_preferenze_cambi.pref_complesso3, domande_preferenze_cambi.pref_complesso4, domande_preferenze_cambi.pref_complesso5, " _
                                              & " domande_preferenze_cambi.pref_edificio1, domande_preferenze_cambi.pref_edificio2, domande_preferenze_cambi.pref_edificio3, domande_preferenze_cambi.pref_edificio4, domande_preferenze_cambi.pref_edificio5, " _
                                              & " domande_preferenze_cambi.pref_indirizzo1, domande_preferenze_cambi.pref_indirizzo2, domande_preferenze_cambi.pref_indirizzo3, domande_preferenze_cambi.pref_indirizzo4, domande_preferenze_cambi.pref_indirizzo5, " _
                                              & " domande_preferenze_escl_cambi.escl_localita1, domande_preferenze_escl_cambi.escl_localita2, domande_preferenze_escl_cambi.escl_localita3, domande_preferenze_escl_cambi.escl_localita4, domande_preferenze_escl_cambi.escl_localita5, " _
                                              & " domande_preferenze_escl_cambi.escl_quart1, domande_preferenze_escl_cambi.escl_quart2, domande_preferenze_escl_cambi.escl_quart3, domande_preferenze_escl_cambi.escl_quart4, domande_preferenze_escl_cambi.escl_quart5, " _
                                              & " domande_preferenze_cambi.pref_sup_max, domande_preferenze_cambi.pref_sup_min, " _
                                              & " domande_preferenze_cambi.pref_piani_da_con, domande_preferenze_cambi.pref_piani_a_con, " _
                                              & " domande_preferenze_cambi.pref_piani_da_senza, domande_preferenze_cambi.pref_piani_a_senza, " _
                                              & " domande_preferenze_cambi.pref_barriere AS barriere, " _
                                              & " domande_preferenze_cambi.pref_condominio AS condominio " _
                                              & " FROM domande_preferenze_cambi, domande_preferenze_escl_cambi " _
                                              & " WHERE domande_preferenze_cambi.id_domanda = domande_preferenze_escl_cambi.id_domanda " _
                                              & " And domande_preferenze_cambi.id_domanda = " & sValoreID.Value & ""

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then



                        M = False
                        If par.IfNull(myReader("PREF_LOCALITA1"), -1) <> -1 Then
                            sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA1"), -1) & " "
                            M = True
                        End If

                        If par.IfNull(myReader("PREF_LOCALITA2"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA2"), -1) & " "
                                M = True
                            Else

                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA2"), -1) & " "
                                M = True
                            End If
                        End If


                        If par.IfNull(myReader("PREF_LOCALITA3"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA3"), -1) & " "
                                M = True
                            Else


                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA3"), -1) & " "
                                M = True
                            End If
                        End If

                        If par.IfNull(myReader("PREF_LOCALITA4"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA4"), -1) & " "
                                M = True
                            Else

                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA4"), -1) & " "
                                M = True
                            End If
                        End If


                        If par.IfNull(myReader("PREF_LOCALITA5"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA5"), -1) & " "
                                M = True
                            Else
                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA5"), -1) & " "
                                M = True
                            End If
                        End If

                        If M = True Then sLocalita = sLocalita & ") "

                        sLocalita2 = sLocalita

                        'controllo nel caso in cui le località di preferenza/esclusione siano uguali

                        Dim pref_loc1 As Integer = par.IfNull(myReader("PREF_LOCALITA1"), -1)
                        Dim pref_loc2 As Integer = par.IfNull(myReader("PREF_LOCALITA2"), -1)
                        Dim pref_loc3 As Integer = par.IfNull(myReader("PREF_LOCALITA3"), -1)
                        Dim pref_loc4 As Integer = par.IfNull(myReader("PREF_LOCALITA4"), -1)
                        Dim pref_loc5 As Integer = par.IfNull(myReader("PREF_LOCALITA5"), -1)

                        Dim escl_loc1 As Integer = par.IfNull(myReader("ESCL_LOCALITA1"), -1)
                        Dim escl_loc2 As Integer = par.IfNull(myReader("ESCL_LOCALITA2"), -1)
                        Dim escl_loc3 As Integer = par.IfNull(myReader("ESCL_LOCALITA3"), -1)
                        Dim escl_loc4 As Integer = par.IfNull(myReader("ESCL_LOCALITA4"), -1)
                        Dim escl_loc5 As Integer = par.IfNull(myReader("ESCL_LOCALITA5"), -1)

                        Dim beccato As Integer = 0


                        If ddlConfronto(pref_loc1, escl_loc1) <> 0 Or ddlConfronto(pref_loc1, escl_loc2) <> 0 Or ddlConfronto(pref_loc1, escl_loc3) <> 0 Or ddlConfronto(pref_loc1, escl_loc4) <> 0 Or ddlConfronto(pref_loc1, escl_loc5) <> 0 Then
                            beccato = 1
                        End If

                        If ddlConfronto(pref_loc2, escl_loc1) <> 0 Or ddlConfronto(pref_loc2, escl_loc2) <> 0 Or ddlConfronto(pref_loc2, escl_loc3) <> 0 Or ddlConfronto(pref_loc2, escl_loc4) <> 0 Or ddlConfronto(pref_loc2, escl_loc5) <> 0 Then
                            beccato = 1

                        End If

                        If ddlConfronto(pref_loc3, escl_loc1) <> 0 Or ddlConfronto(pref_loc3, escl_loc2) <> 0 Or ddlConfronto(pref_loc3, escl_loc3) <> 0 Or ddlConfronto(pref_loc3, escl_loc4) <> 0 Or ddlConfronto(pref_loc3, escl_loc5) <> 0 Then
                            beccato = 1

                        End If


                        If ddlConfronto(pref_loc4, escl_loc1) <> 0 Or ddlConfronto(pref_loc4, escl_loc2) <> 0 Or ddlConfronto(pref_loc4, escl_loc3) <> 0 Or ddlConfronto(pref_loc4, escl_loc4) <> 0 Or ddlConfronto(pref_loc4, escl_loc5) <> 0 Then
                            beccato = 1
                        End If



                        If ddlConfronto(pref_loc5, escl_loc1) <> 0 Or ddlConfronto(pref_loc5, escl_loc2) <> 0 Or ddlConfronto(pref_loc5, escl_loc3) <> 0 Or ddlConfronto(pref_loc5, escl_loc4) <> 0 Or ddlConfronto(pref_loc5, escl_loc5) <> 0 Then
                            beccato = 1
                        End If



                        If beccato = 1 Then
                            Dim N As Boolean = False

                            If par.IfNull(myReader("ESCL_QUART1"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART1"), -1) & ") "
                                N = True

                            End If


                            If par.IfNull(myReader("ESCL_QUART2"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART2"), -1) & ") "
                                N = True

                            End If


                            If par.IfNull(myReader("ESCL_QUART3"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART3"), -1) & ") "
                                N = True

                            End If


                            If par.IfNull(myReader("ESCL_QUART4"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART4"), -1) & ") "
                                N = True

                            End If

                            If par.IfNull(myReader("ESCL_QUART5"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART5"), -1) & ") "
                                N = True

                            End If



                        End If






                        '---------------------------------


                        M = False

                        If par.IfNull(myReader("PREF_QUART1"), -1) <> -1 Then
                            '  If M = True Then
                            sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART1"), -1) & " "
                            '   Else
                            ' sQuartiere = " (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART1"), -1) & " "
                            M = True
                            '  End If
                        End If



                        If par.IfNull(myReader("PREF_QUART2"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART2"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_QUART1"), -1) <> -1 Then

                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART2"), -1) & " "
                                    M = True
                                Else

                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART2"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_QUART3"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART3"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 Then

                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART3"), -1) & " "
                                    M = True
                                Else

                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART3"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_QUART4"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART4"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 Then

                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART4"), -1) & " "
                                    M = True
                                Else

                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART4"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_QUART5"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART5"), "") & " "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 And par.IfNull(myReader("PREF_QUART5"), -1) = -1 Then


                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART5"), -1) & " "
                                    M = True

                                Else
                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART5"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_QUART5"), -1) = -1 And par.IfNull(myReader("PREF_QUART4"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART1"), -1) = -1 Then


                        Else
                            If M = True Then sQuartiere = sQuartiere & ") "

                        End If





                        M = False

                        If par.IfNull(myReader("PREF_COMPLESSO1"), -1) <> -1 Then
                            '  If M = True Then
                            sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO1"), -1) & " "
                            '    Else
                            ' sComplesso = " (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO1"), -1) & " "
                            M = True
                            '   End If
                        End If



                        If par.IfNull(myReader("PREF_COMPLESSO2"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO2"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) <> -1 Then

                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO2"), -1) & " "
                                    M = True
                                Else

                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO2"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO3"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO3"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 Then

                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO3"), -1) & " "
                                    M = True
                                Else

                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO3"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO4"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO4"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 Then

                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO4"), -1) & " "
                                    M = True
                                Else

                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO4"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO5"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO5"), "") & " "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO5"), -1) = -1 Then


                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO5"), -1) & " "
                                    M = True

                                Else
                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO5"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_COMPLESSO5"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO4"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 Then


                        Else
                            If M = True Then sComplesso = sComplesso & ") "

                        End If










                        M = False

                        If par.IfNull(myReader("PREF_EDIFICIO1"), -1) <> -1 Then
                            '  If M = True Then
                            sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO1"), -1) & " "
                            'Else
                            ' sEdificio = " (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO1"), -1) & " "
                            M = True
                            '  End If
                        End If



                        If par.IfNull(myReader("PREF_EDIFICIO2"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO2"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) <> -1 Then

                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO2"), -1) & " "
                                    M = True
                                Else

                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO2"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_EDIFICIO3"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO3"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 Then

                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO3"), -1) & " "
                                    M = True
                                Else

                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO3"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_EDIFICIO4"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO4"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 Then

                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO4"), -1) & " "
                                    M = True
                                Else

                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO4"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_EDIFICIO5"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO5"), "") & " "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO5"), -1) = -1 Then


                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO5"), -1) & " "
                                    M = True

                                Else
                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO5"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_EDIFICIO5"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO4"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 Then


                        Else
                            If M = True Then sEdificio = sEdificio & ") "

                        End If








                        M = False

                        If par.IfNull(myReader("PREF_INDIRIZZO1"), "") <> "" Then
                            '    If M = True Then
                            sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO1"), "") & "' "
                            '   Else
                            '  sIndirizzo = " AND (INDIRIZZO='" & par.IfNull(myReader("PREF_INDIRIZZO1"), "") & "' "
                            M = True
                            'End If
                        End If



                        If par.IfNull(myReader("PREF_INDIRIZZO2"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & "AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO2"), "") & "' "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_INDIRIZZO1"), -1) <> "" Then

                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO2"), "") & "' "
                                    M = True
                                Else

                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO2"), "") & "' "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_INDIRIZZO3"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & "AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO3"), "") & "' "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" Then

                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO3"), -1) & "' "
                                    M = True
                                Else

                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO3"), -1) & "' "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_INDIRIZZO4"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & "AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO4"), "") & "' "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" Then

                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO4"), "") & "' "
                                    M = True
                                Else

                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO4"), "") & "' "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_INDIRIZZO5"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO5"), "") & "' "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO5"), "") = "" Then


                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO5"), "") & "' "
                                    M = True

                                Else
                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO5"), "") & "' "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_INDIRIZZO5"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO4"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" Then


                        Else
                            If M = True Then sIndirizzo = sIndirizzo & ") "

                        End If


                        'piani------------------------------------------------------------------------

                        M = False


                        'piani Con Ascensore-----------------
                        Dim dtP As New Data.DataTable
                        dtP = caricaDTpiani()
                        Dim k As Integer = 0

                        Dim trovatoDa As Integer = 0
                        If par.IfNull(myReader("PREF_PIANI_DA_CON"), "-1") <> "-1" Then
                            If par.IfNull(myReader("PREF_PIANI_A_CON"), "-1") <> "-1" Then
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If trovatoDa = 1 Then
                                        If rowScart.ItemArray(0) <> par.IfNull(myReader("PREF_PIANI_A_CON"), "-1") Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        Else
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "' "
                                            Exit Do
                                        End If
                                    End If
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_CON"), "-1") Then
                                        trovatoDa = 1
                                        sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                    End If

                                    k = k + 1
                                Loop

                            Else
                                trovatoDa = 0
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_CON"), "-1") Then
                                        trovatoDa = 1
                                        sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                    Else
                                        If trovatoDa = 1 Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        End If
                                    End If
                                    k = k + 1
                                Loop

                            End If
                            M = True
                            sPiano = sPiano & ") AND ALLOGGI.ASCENSORE='1') "


                        Else

                            sPiano = sPiano & " AND ((ALLOGGI.ASCENSORE='1') "

                            M = True

                        End If



                        'piani Senza Ascensore-----------------

                        trovatoDa = 0

                        If par.IfNull(myReader("PREF_PIANI_DA_SENZA"), "-1") <> "-1" Then
                            If par.IfNull(myReader("PREF_PIANI_A_SENZA"), "-1") <> "-1" Then
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If trovatoDa = 1 Then
                                        If rowScart.ItemArray(0) <> par.IfNull(myReader("PREF_PIANI_A_SENZA"), "-1") Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        Else
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "' "
                                            Exit Do
                                        End If
                                    End If
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_SENZA"), "-1") Then
                                        If M = False Then
                                            sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                            M = True
                                        Else
                                            sPiano = sPiano & " OR (TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                        End If
                                        trovatoDa = 1
                                    End If

                                    k = k + 1
                                Loop

                            Else
                                trovatoDa = 0
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_SENZA"), "-1") Then
                                        trovatoDa = 1
                                        If M = False Then
                                            sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                            M = True
                                        Else
                                            sPiano = sPiano & " OR (TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                        End If
                                    Else
                                        If trovatoDa = 1 Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        End If
                                    End If
                                    k = k + 1
                                Loop

                            End If
                            sPiano = sPiano & ") AND ALLOGGI.ASCENSORE<>'1')) "

                        Else
                            If M = True Then
                                sPiano = sPiano & "or (ALLOGGI.ASCENSORE<>'1')) "
                            End If

                        End If



                        sPiano = sPiano











                        '---------------------------------------------------------------



                        '-----------------parti restanti

                        sImm2 = ""

                        If sLocalita = "" And sQuartiere = "" And sComplesso = "" And sEdificio = "" And sIndirizzo = "" Then

                            M = False
                        Else
                            M = True
                        End If


                        If par.IfNull(myReader("CONDOMINIO"), "0") = "1" Then

                            sImm = sImm & " AND cond_edifici.id_condominio IS NULL"


                        End If





                        If par.IfNull(myReader("BARRIERE"), "0") = "1" Then

                            sImm = sImm & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            sImm2 = sImm2 & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "

                        End If

                        'opzioni per superficie


                        If par.IfNull(myReader("pref_sup_min"), -1) <> -1 And par.IfNull(myReader("pref_sup_max"), -1) <> -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID and valore between " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & " and " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ")"
                            sImm2 = sImm2 & " and alloggi.sup >=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & " and and alloggi.sup <=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ""
                        End If


                        If par.IfNull(myReader("pref_sup_min"), -1) <> -1 And par.IfNull(myReader("pref_sup_max"), -1) = -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID and valore >= " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & ")"
                            sImm2 = sImm2 & " and alloggi.sup >=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & ""
                        End If

                        If par.IfNull(myReader("pref_sup_min"), -1) = -1 And par.IfNull(myReader("pref_sup_max"), -1) <> -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID and valore <= " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ")"
                            sImm2 = sImm2 & " and alloggi.sup <=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ""
                        End If


                        If par.IfNull(myReader("pref_sup_min"), -1) = -1 And par.IfNull(myReader("pref_sup_max"), -1) = -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID" & ")"
                        End If



                        'If sImm <> "" Then

                        'Else


                        'sZona = " and alloggi.id=00000"
                        If lbl_invalidi.Text <> "0" Then
                            sImm = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL)"
                            sImm2 = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL)"
                        End If
                        '   BindGrid1()
                        'End If







                        '--------------------------QUERY------------------------------

                        If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO4"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO5"), "") = "" Then

                        Else

                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                        & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                         & " dimensioni.valore as sup, " _
                        & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                        & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                        & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                         & " complessi_immobiliari.denominazione AS complesso, " _
                        & " edifici.denominazione AS edificio, " _
                        & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                        & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                        & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                        & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                        & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                        & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                        & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                        & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                        & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                        & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                        & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                        & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                        & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                        & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                        & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                        & " And comuni_nazioni.cod = edifici.cod_comune " _
                        & " " & sIndirizzo & " " _
                        & " " & sPiano & " " _
                         & " " & sImm & " " _
                        & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sIndirizzo & " " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt As New Data.DataTable
                            da.Fill(dt)

                            Dim RIGA1 As System.Data.DataRow
                            For i As Integer = 0 To dt.Rows.Count - 1
                                RIGA1 = dtDaRiempire.NewRow()
                                RIGA1.Item("ID_ALLOGGIO") = par.IfNull(dt.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA1.Item("ID_QUARTIERE") = par.IfNull(dt.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA1.Item("ID_PIANO") = par.IfNull(dt.Rows(i).Item("ID_PIANO"), "")
                                RIGA1.Item("ID_COMUNE") = par.IfNull(dt.Rows(i).Item("ID_COMUNE"), "")
                                RIGA1.Item("COD_ALLOGGIO") = par.IfNull(dt.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA1.Item("COMUNE") = par.IfNull(dt.Rows(i).Item("COMUNE"), "")
                                RIGA1.Item("ZONA") = par.IfNull(dt.Rows(i).Item("ZONA"), "")
                                RIGA1.Item("QUARTIERE") = par.IfNull(dt.Rows(i).Item("QUARTIERE"), "")
                                RIGA1.Item("COMPLESSO") = par.IfNull(dt.Rows(i).Item("COMPLESSO"), "")
                                RIGA1.Item("EDIFICIO") = par.IfNull(dt.Rows(i).Item("EDIFICIO"), "")
                                RIGA1.Item("INDIRIZZO") = par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")
                                RIGA1.Item("NUM_LOCALI") = par.IfNull(dt.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA1.Item("PIANO") = par.IfNull(dt.Rows(i).Item("PIANO"), "")
                                RIGA1.Item("SUP") = par.IfNull(dt.Rows(i).Item("SUP"), "")
                                RIGA1.Item("ELEVATORE") = par.IfNull(dt.Rows(i).Item("ELEVATORE"), "")
                                RIGA1.Item("BARRIERE") = par.IfNull(dt.Rows(i).Item("BARRIERE"), "")
                                RIGA1.Item("CONDOMINIO") = par.IfNull(dt.Rows(i).Item("CONDOMINIO"), "")
                                RIGA1.Item("DATA_DISPONIBILITA") = par.IfNull(dt.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA1.Item("PROPRIETA") = par.IfNull(dt.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA1)
                            Next

                        End If

                        If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO4"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO5"), -1) = -1 Then

                        Else

                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                      & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                    & " dimensioni.valore as sup, " _
                      & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                      & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                      & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                       & " complessi_immobiliari.denominazione AS complesso, " _
                      & " edifici.denominazione AS edificio, " _
                      & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                      & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                      & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                      & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                      & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                      & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                      & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                      & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                      & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                      & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                      & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                      & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                      & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                      & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                      & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                      & " And comuni_nazioni.cod = edifici.cod_comune " _
                      & " " & sEdificio & " " _
                      & " " & sPiano & " " _
                      & " " & sImm & " " _
                       & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt2 As New Data.DataTable
                            da2.Fill(dt2)

                            Dim RIGA2 As System.Data.DataRow
                            For i As Integer = 0 To dt2.Rows.Count - 1
                                RIGA2 = dtDaRiempire.NewRow()
                                RIGA2.Item("ID_ALLOGGIO") = par.IfNull(dt2.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA2.Item("ID_QUARTIERE") = par.IfNull(dt2.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA2.Item("ID_PIANO") = par.IfNull(dt2.Rows(i).Item("ID_PIANO"), "")
                                RIGA2.Item("ID_COMUNE") = par.IfNull(dt2.Rows(i).Item("ID_COMUNE"), "")
                                RIGA2.Item("COD_ALLOGGIO") = par.IfNull(dt2.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA2.Item("COMUNE") = par.IfNull(dt2.Rows(i).Item("COMUNE"), "")
                                RIGA2.Item("ZONA") = par.IfNull(dt2.Rows(i).Item("ZONA"), "")
                                RIGA2.Item("QUARTIERE") = par.IfNull(dt2.Rows(i).Item("QUARTIERE"), "")
                                RIGA2.Item("COMPLESSO") = par.IfNull(dt2.Rows(i).Item("COMPLESSO"), "")
                                RIGA2.Item("EDIFICIO") = par.IfNull(dt2.Rows(i).Item("EDIFICIO"), "")
                                RIGA2.Item("INDIRIZZO") = par.IfNull(dt2.Rows(i).Item("INDIRIZZO"), "")
                                RIGA2.Item("NUM_LOCALI") = par.IfNull(dt2.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA2.Item("PIANO") = par.IfNull(dt2.Rows(i).Item("PIANO"), "")
                                RIGA2.Item("SUP") = par.IfNull(dt2.Rows(i).Item("SUP"), "")
                                RIGA2.Item("ELEVATORE") = par.IfNull(dt2.Rows(i).Item("ELEVATORE"), "")
                                RIGA2.Item("BARRIERE") = par.IfNull(dt2.Rows(i).Item("BARRIERE"), "")
                                RIGA2.Item("CONDOMINIO") = par.IfNull(dt2.Rows(i).Item("CONDOMINIO"), "")
                                RIGA2.Item("DATA_DISPONIBILITA") = par.IfNull(dt2.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA2.Item("PROPRIETA") = par.IfNull(dt2.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA2)
                            Next

                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO4"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO5"), -1) = -1 Then

                        Else

                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                     & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                      & " dimensioni.valore as sup, " _
                     & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                     & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                     & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                      & " complessi_immobiliari.denominazione AS complesso, " _
                     & " edifici.denominazione AS edificio, " _
                     & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                     & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                     & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                     & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                     & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                     & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                     & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                     & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                     & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                     & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                     & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                     & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                     & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                     & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                     & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                     & " And comuni_nazioni.cod = edifici.cod_comune " _
                     & " " & sComplesso & " " _
                     & " " & sPiano & " " _
                     & " " & sImm & " " _
                     & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt3 As New Data.DataTable
                            da3.Fill(dt3)

                            Dim RIGA3 As System.Data.DataRow
                            For i As Integer = 0 To dt3.Rows.Count - 1
                                RIGA3 = dtDaRiempire.NewRow()
                                RIGA3.Item("ID_ALLOGGIO") = par.IfNull(dt3.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA3.Item("ID_QUARTIERE") = par.IfNull(dt3.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA3.Item("ID_PIANO") = par.IfNull(dt3.Rows(i).Item("ID_PIANO"), "")
                                RIGA3.Item("ID_COMUNE") = par.IfNull(dt3.Rows(i).Item("ID_COMUNE"), "")
                                RIGA3.Item("COD_ALLOGGIO") = par.IfNull(dt3.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA3.Item("COMUNE") = par.IfNull(dt3.Rows(i).Item("COMUNE"), "")
                                RIGA3.Item("ZONA") = par.IfNull(dt3.Rows(i).Item("ZONA"), "")
                                RIGA3.Item("QUARTIERE") = par.IfNull(dt3.Rows(i).Item("QUARTIERE"), "")
                                RIGA3.Item("COMPLESSO") = par.IfNull(dt3.Rows(i).Item("COMPLESSO"), "")
                                RIGA3.Item("EDIFICIO") = par.IfNull(dt3.Rows(i).Item("EDIFICIO"), "")
                                RIGA3.Item("INDIRIZZO") = par.IfNull(dt3.Rows(i).Item("INDIRIZZO"), "")
                                RIGA3.Item("NUM_LOCALI") = par.IfNull(dt3.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA3.Item("PIANO") = par.IfNull(dt3.Rows(i).Item("PIANO"), "")
                                RIGA3.Item("SUP") = par.IfNull(dt3.Rows(i).Item("SUP"), "")
                                RIGA3.Item("ELEVATORE") = par.IfNull(dt3.Rows(i).Item("ELEVATORE"), "")
                                RIGA3.Item("BARRIERE") = par.IfNull(dt3.Rows(i).Item("BARRIERE"), "")
                                RIGA3.Item("CONDOMINIO") = par.IfNull(dt3.Rows(i).Item("CONDOMINIO"), "")
                                RIGA3.Item("DATA_DISPONIBILITA") = par.IfNull(dt3.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA3.Item("PROPRIETA") = par.IfNull(dt3.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA3)
                            Next


                        End If

                        If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 And par.IfNull(myReader("PREF_QUART4"), -1) = -1 And par.IfNull(myReader("PREF_QUART5"), -1) = -1 Then

                        Else


                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                        & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                        & " dimensioni.valore as sup, " _
                        & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                        & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                        & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                         & " complessi_immobiliari.denominazione AS complesso, " _
                        & " edifici.denominazione AS edificio, " _
                        & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                        & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                        & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                        & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                        & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                        & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                        & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                        & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                        & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                        & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                        & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                        & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                        & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                        & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                        & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                        & " And comuni_nazioni.cod = edifici.cod_comune " _
                        & " " & sQuartiere & " " _
                        & " " & sPiano & " " _
                        & " " & sImm & " " _
                        & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt4 As New Data.DataTable
                            da4.Fill(dt4)

                            Dim RIGA4 As System.Data.DataRow
                            For i As Integer = 0 To dt4.Rows.Count - 1
                                RIGA4 = dtDaRiempire.NewRow()
                                RIGA4.Item("ID_ALLOGGIO") = par.IfNull(dt4.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA4.Item("ID_QUARTIERE") = par.IfNull(dt4.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA4.Item("ID_PIANO") = par.IfNull(dt4.Rows(i).Item("ID_PIANO"), "")
                                RIGA4.Item("ID_COMUNE") = par.IfNull(dt4.Rows(i).Item("ID_COMUNE"), "")
                                RIGA4.Item("COD_ALLOGGIO") = par.IfNull(dt4.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA4.Item("COMUNE") = par.IfNull(dt4.Rows(i).Item("COMUNE"), "")
                                RIGA4.Item("ZONA") = par.IfNull(dt4.Rows(i).Item("ZONA"), "")
                                RIGA4.Item("QUARTIERE") = par.IfNull(dt4.Rows(i).Item("QUARTIERE"), "")
                                RIGA4.Item("COMPLESSO") = par.IfNull(dt4.Rows(i).Item("COMPLESSO"), "")
                                RIGA4.Item("EDIFICIO") = par.IfNull(dt4.Rows(i).Item("EDIFICIO"), "")
                                RIGA4.Item("INDIRIZZO") = par.IfNull(dt4.Rows(i).Item("INDIRIZZO"), "")
                                RIGA4.Item("NUM_LOCALI") = par.IfNull(dt4.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA4.Item("PIANO") = par.IfNull(dt4.Rows(i).Item("PIANO"), "")
                                RIGA4.Item("SUP") = par.IfNull(dt4.Rows(i).Item("SUP"), "")
                                RIGA4.Item("ELEVATORE") = par.IfNull(dt4.Rows(i).Item("ELEVATORE"), "")
                                RIGA4.Item("BARRIERE") = par.IfNull(dt4.Rows(i).Item("BARRIERE"), "")
                                RIGA4.Item("CONDOMINIO") = par.IfNull(dt4.Rows(i).Item("CONDOMINIO"), "")
                                RIGA4.Item("DATA_DISPONIBILITA") = par.IfNull(dt4.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA4.Item("PROPRIETA") = par.IfNull(dt4.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA4)
                            Next

                        End If


                        sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                 & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                 & " dimensioni.valore as sup, " _
                 & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                 & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                 & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                  & " complessi_immobiliari.denominazione AS complesso, " _
                 & " edifici.denominazione AS edificio, " _
                 & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                 & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                 & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                 & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                 & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                 & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                 & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                 & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                 & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                 & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                 & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                 & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                 & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                 & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                 & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                 & " And comuni_nazioni.cod = edifici.cod_comune " _
                 & " " & sLocalita & " " _
                 & " " & sPiano & " " _
                 & " " & sImm & " " _
                    & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sLocalita2 & " " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                        Dim da5 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                        Dim dt5 As New Data.DataTable
                        da5.Fill(dt5)

                        Dim RIGA5 As System.Data.DataRow
                        For i As Integer = 0 To dt5.Rows.Count - 1
                            RIGA5 = dtDaRiempire.NewRow()
                            RIGA5.Item("ID_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("ID_ALLOGGIO"), "")
                            RIGA5.Item("ID_QUARTIERE") = par.IfNull(dt5.Rows(i).Item("ID_QUARTIERE"), "")
                            RIGA5.Item("ID_PIANO") = par.IfNull(dt5.Rows(i).Item("ID_PIANO"), "")
                            RIGA5.Item("ID_COMUNE") = par.IfNull(dt5.Rows(i).Item("ID_COMUNE"), "")
                            RIGA5.Item("COD_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("COD_ALLOGGIO"), "")
                            RIGA5.Item("COMUNE") = par.IfNull(dt5.Rows(i).Item("COMUNE"), "")
                            RIGA5.Item("ZONA") = par.IfNull(dt5.Rows(i).Item("ZONA"), "")
                            RIGA5.Item("QUARTIERE") = par.IfNull(dt5.Rows(i).Item("QUARTIERE"), "")
                            RIGA5.Item("COMPLESSO") = par.IfNull(dt5.Rows(i).Item("COMPLESSO"), "")
                            RIGA5.Item("EDIFICIO") = par.IfNull(dt5.Rows(i).Item("EDIFICIO"), "")
                            RIGA5.Item("INDIRIZZO") = par.IfNull(dt5.Rows(i).Item("INDIRIZZO"), "")
                            RIGA5.Item("NUM_LOCALI") = par.IfNull(dt5.Rows(i).Item("NUM_LOCALI"), "")
                            RIGA5.Item("PIANO") = par.IfNull(dt5.Rows(i).Item("PIANO"), "")
                            RIGA5.Item("SUP") = par.IfNull(dt5.Rows(i).Item("SUP"), "")
                            RIGA5.Item("ELEVATORE") = par.IfNull(dt5.Rows(i).Item("ELEVATORE"), "")
                            RIGA5.Item("BARRIERE") = par.IfNull(dt5.Rows(i).Item("BARRIERE"), "")
                            RIGA5.Item("CONDOMINIO") = par.IfNull(dt5.Rows(i).Item("CONDOMINIO"), "")
                            RIGA5.Item("DATA_DISPONIBILITA") = par.IfNull(dt5.Rows(i).Item("DATA_DISPONIBILITA"), "")
                            RIGA5.Item("PROPRIETA") = par.IfNull(dt5.Rows(i).Item("PROPRIETA"), "")
                            dtDaRiempire.Rows.Add(RIGA5)
                        Next

                    Else
                        If lbl_invalidi.Text <> "0" Then
                            sImm = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL)"
                            sImm2 = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                        End If

                        sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID" & ")"

                        sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                 & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                & " dimensioni.valore as sup, " _
                 & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                 & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                 & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                  & " complessi_immobiliari.denominazione AS complesso, " _
                 & " edifici.denominazione AS edificio, " _
                 & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                 & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                 & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                 & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                 & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                 & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                 & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                 & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                 & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                 & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                 & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                 & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                 & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                 & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                 & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                 & " And comuni_nazioni.cod = edifici.cod_comune " _
                 & " " & sLocalita & " " _
                 & " " & sPiano & " " _
                 & " " & sImm & " " _
                 & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sLocalita & " " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                        Dim da5 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                        Dim dt5 As New Data.DataTable
                        da5.Fill(dt5)

                        Dim RIGA5 As System.Data.DataRow
                        For i As Integer = 0 To dt5.Rows.Count - 1
                            RIGA5 = dtDaRiempire.NewRow()
                            RIGA5.Item("ID_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("ID_ALLOGGIO"), "")
                            RIGA5.Item("ID_QUARTIERE") = par.IfNull(dt5.Rows(i).Item("ID_QUARTIERE"), "")
                            RIGA5.Item("ID_PIANO") = par.IfNull(dt5.Rows(i).Item("ID_PIANO"), "")
                            RIGA5.Item("ID_COMUNE") = par.IfNull(dt5.Rows(i).Item("ID_COMUNE"), "")
                            RIGA5.Item("COD_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("COD_ALLOGGIO"), "")
                            RIGA5.Item("COMUNE") = par.IfNull(dt5.Rows(i).Item("COMUNE"), "")
                            RIGA5.Item("ZONA") = par.IfNull(dt5.Rows(i).Item("ZONA"), "")
                            RIGA5.Item("QUARTIERE") = par.IfNull(dt5.Rows(i).Item("QUARTIERE"), "")
                            RIGA5.Item("COMPLESSO") = par.IfNull(dt5.Rows(i).Item("COMPLESSO"), "")
                            RIGA5.Item("EDIFICIO") = par.IfNull(dt5.Rows(i).Item("EDIFICIO"), "")
                            RIGA5.Item("INDIRIZZO") = par.IfNull(dt5.Rows(i).Item("INDIRIZZO"), "")
                            RIGA5.Item("NUM_LOCALI") = par.IfNull(dt5.Rows(i).Item("NUM_LOCALI"), "")
                            RIGA5.Item("PIANO") = par.IfNull(dt5.Rows(i).Item("PIANO"), "")
                            RIGA5.Item("SUP") = par.IfNull(dt5.Rows(i).Item("SUP"), "")
                            RIGA5.Item("ELEVATORE") = par.IfNull(dt5.Rows(i).Item("ELEVATORE"), "")
                            RIGA5.Item("BARRIERE") = par.IfNull(dt5.Rows(i).Item("BARRIERE"), "")
                            RIGA5.Item("CONDOMINIO") = par.IfNull(dt5.Rows(i).Item("CONDOMINIO"), "")
                            RIGA5.Item("DATA_DISPONIBILITA") = par.IfNull(dt5.Rows(i).Item("DATA_DISPONIBILITA"), "")
                            RIGA5.Item("PROPRIETA") = par.IfNull(dt5.Rows(i).Item("PROPRIETA"), "")
                            dtDaRiempire.Rows.Add(RIGA5)
                        Next



                    End If


                    BindGrid2()




                    dtDaRiempire = DeleteDuplicateFromDataTable(dtDaRiempire, "ID_ALLOGGIO")
                    DataGrid2.DataSource = dtDaRiempire
                    DataGrid2.DataBind()


                    'DataGrid2.DataSource = dtDaRiempire
                    'DataGrid2.DataBind()
                    myReader.Close()

                    lbl_UIPref.Text = "ELENCO UNITA' DISPONIBILI PER PREFERENZE - Totale: " & dtDaRiempire.Rows.Count & " Unità"
                    'rimuovere doppioni da datagrid






                    '----------------------------

                Case Else


                    sLocalita = ""
                    sQuartiere = ""
                    sComplesso = ""
                    sEdificio = ""
                    sIndirizzo = ""
                    sImm = ""
                    sPiano = ""
                    slocalita2 = ""



                    par.cmd.CommandText = "SELECT trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO.DATA_PG,DOMANDE_BANDO.id_bando,DOMANDE_BANDO.PG,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME FROM DOMANDE_BANDO,COMP_NUCLEO WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND COMP_NUCLEO.PROGR=DOMANDE_BANDO.PROGR_COMPONENTE AND DOMANDE_BANDO.ID=" & sValoreID.Value
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        lbl_PG.Text = par.IfNull(myReader1("PG"), "")
                        lbl_nominativo.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                        lbl_isbarc.Text = par.IfNull(myReader1("isbarc_r"), "0")
                        lbl_isee.Text = par.IfNull(myReader1("reddito_isee"), "0")
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & sValoreID.Value
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        lbl_comp.Text = par.IfNull(myReader1(0), "0")
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE comp_nucleo.perc_inval>=66 and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & sValoreID.Value
                    myReader1 = par.cmd.ExecuteReader()
                    lbl_invalidi.Text = "0"
                    If myReader1.Read() Then
                        lbl_invalidi.Text = par.IfNull(myReader1(0), "0")
                    End If
                    myReader1.Close()


                    par.cmd.CommandText = "SELECT count(COMP_NUCLEO.ID) FROM COMP_NUCLEO,dichiarazioni,domande_bando WHERE (select MONTHS_BETWEEN(SYSDATE,to_char(TO_DATE(COMP_NUCLEO.DATA_NASCITA,'YYYYmmdd'),'DD/MON/YYYY')) from dual)>=780 and DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DOMANDE_BANDO.ID=" & sValoreID.Value
                    myReader1 = par.cmd.ExecuteReader()
                    lbl_anziani.Text = "0"
                    If myReader1.Read() Then
                        lbl_anziani.Text = par.IfNull(myReader1(0), "0")
                    End If
                    myReader1.Close()






                    sZona = ""
                    par.cmd.CommandText = "SELECT domande_preferenze.pref_localita1, domande_preferenze.pref_localita2, domande_preferenze.pref_localita3, domande_preferenze.pref_localita4, domande_preferenze.pref_localita5, " _
                                             & " domande_preferenze.pref_quart1, domande_preferenze.pref_quart2, domande_preferenze.pref_quart3, domande_preferenze.pref_quart4, domande_preferenze.pref_quart5, " _
                                             & " domande_preferenze.pref_complesso1, domande_preferenze.pref_complesso2, domande_preferenze.pref_complesso3, domande_preferenze.pref_complesso4, domande_preferenze.pref_complesso5, " _
                                             & " domande_preferenze.pref_edificio1, domande_preferenze.pref_edificio2, domande_preferenze.pref_edificio3, domande_preferenze.pref_edificio4, domande_preferenze.pref_edificio5, " _
                                             & " domande_preferenze.pref_indirizzo1, domande_preferenze.pref_indirizzo2, domande_preferenze.pref_indirizzo3, domande_preferenze.pref_indirizzo4, domande_preferenze.pref_indirizzo5, " _
                                             & " domande_preferenze_esclusioni.escl_localita1, domande_preferenze_esclusioni.escl_localita2, domande_preferenze_esclusioni.escl_localita3, domande_preferenze_esclusioni.escl_localita4, domande_preferenze_esclusioni.escl_localita5, " _
                                             & " domande_preferenze_esclusioni.escl_quart1, domande_preferenze_esclusioni.escl_quart2, domande_preferenze_esclusioni.escl_quart3, domande_preferenze_esclusioni.escl_quart4, domande_preferenze_esclusioni.escl_quart5, " _
                                             & " domande_preferenze.pref_sup_max, domande_preferenze.pref_sup_min, " _
                                             & " domande_preferenze.pref_piani_da_con, domande_preferenze.pref_piani_a_con, " _
                                             & " domande_preferenze.pref_piani_da_senza, domande_preferenze.pref_piani_a_senza, " _
                                             & " domande_preferenze.pref_barriere AS barriere, " _
                                             & " domande_preferenze.pref_condominio AS condominio " _
                                             & " FROM domande_preferenze, domande_preferenze_esclusioni " _
                                             & " WHERE domande_preferenze.id_domanda = domande_preferenze_esclusioni.id_domanda " _
                                             & " And domande_preferenze.id_domanda = " & sValoreID.Value & ""


                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then



                        M = False
                        If par.IfNull(myReader("PREF_LOCALITA1"), -1) <> -1 Then
                            sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA1"), -1) & " "
                            M = True
                        End If

                        If par.IfNull(myReader("PREF_LOCALITA2"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA2"), -1) & " "
                                M = True
                            Else

                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA2"), -1) & " "
                                M = True
                            End If
                        End If


                        If par.IfNull(myReader("PREF_LOCALITA3"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA3"), -1) & " "
                                M = True
                            Else


                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA3"), -1) & " "
                                M = True
                            End If
                        End If

                        If par.IfNull(myReader("PREF_LOCALITA4"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA4"), -1) & " "
                                M = True
                            Else

                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA4"), -1) & " "
                                M = True
                            End If
                        End If


                        If par.IfNull(myReader("PREF_LOCALITA5"), -1) <> -1 Then
                            If M = False Then
                                sLocalita = sLocalita & " AND (comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA5"), -1) & " "
                                M = True
                            Else
                                sLocalita = sLocalita & " OR comuni_nazioni.id=" & par.IfNull(myReader("PREF_LOCALITA5"), -1) & " "
                                M = True
                            End If
                        End If

                        If M = True Then sLocalita = sLocalita & ") "

                        sLocalita2 = sLocalita

                        'controllo nel caso in cui le località di preferenza/esclusione siano uguali

                        Dim pref_loc1 As Integer = par.IfNull(myReader("PREF_LOCALITA1"), -1)
                        Dim pref_loc2 As Integer = par.IfNull(myReader("PREF_LOCALITA2"), -1)
                        Dim pref_loc3 As Integer = par.IfNull(myReader("PREF_LOCALITA3"), -1)
                        Dim pref_loc4 As Integer = par.IfNull(myReader("PREF_LOCALITA4"), -1)
                        Dim pref_loc5 As Integer = par.IfNull(myReader("PREF_LOCALITA5"), -1)

                        Dim escl_loc1 As Integer = par.IfNull(myReader("ESCL_LOCALITA1"), -1)
                        Dim escl_loc2 As Integer = par.IfNull(myReader("ESCL_LOCALITA2"), -1)
                        Dim escl_loc3 As Integer = par.IfNull(myReader("ESCL_LOCALITA3"), -1)
                        Dim escl_loc4 As Integer = par.IfNull(myReader("ESCL_LOCALITA4"), -1)
                        Dim escl_loc5 As Integer = par.IfNull(myReader("ESCL_LOCALITA5"), -1)

                        Dim beccato As Integer = 0


                        If ddlConfronto(pref_loc1, escl_loc1) <> 0 Or ddlConfronto(pref_loc1, escl_loc2) <> 0 Or ddlConfronto(pref_loc1, escl_loc3) <> 0 Or ddlConfronto(pref_loc1, escl_loc4) <> 0 Or ddlConfronto(pref_loc1, escl_loc5) <> 0 Then
                            beccato = 1
                        End If

                        If ddlConfronto(pref_loc2, escl_loc1) <> 0 Or ddlConfronto(pref_loc2, escl_loc2) <> 0 Or ddlConfronto(pref_loc2, escl_loc3) <> 0 Or ddlConfronto(pref_loc2, escl_loc4) <> 0 Or ddlConfronto(pref_loc2, escl_loc5) <> 0 Then
                            beccato = 1

                        End If

                        If ddlConfronto(pref_loc3, escl_loc1) <> 0 Or ddlConfronto(pref_loc3, escl_loc2) <> 0 Or ddlConfronto(pref_loc3, escl_loc3) <> 0 Or ddlConfronto(pref_loc3, escl_loc4) <> 0 Or ddlConfronto(pref_loc3, escl_loc5) <> 0 Then
                            beccato = 1

                        End If


                        If ddlConfronto(pref_loc4, escl_loc1) <> 0 Or ddlConfronto(pref_loc4, escl_loc2) <> 0 Or ddlConfronto(pref_loc4, escl_loc3) <> 0 Or ddlConfronto(pref_loc4, escl_loc4) <> 0 Or ddlConfronto(pref_loc4, escl_loc5) <> 0 Then
                            beccato = 1
                        End If



                        If ddlConfronto(pref_loc5, escl_loc1) <> 0 Or ddlConfronto(pref_loc5, escl_loc2) <> 0 Or ddlConfronto(pref_loc5, escl_loc3) <> 0 Or ddlConfronto(pref_loc5, escl_loc4) <> 0 Or ddlConfronto(pref_loc5, escl_loc5) <> 0 Then
                            beccato = 1
                        End If



                        If beccato = 1 Then
                            Dim N As Boolean = False

                            If par.IfNull(myReader("ESCL_QUART1"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART1"), -1) & ") "
                                N = True

                            End If


                            If par.IfNull(myReader("ESCL_QUART2"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART2"), -1) & ") "
                                N = True

                            End If


                            If par.IfNull(myReader("ESCL_QUART3"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART3"), -1) & ") "
                                N = True

                            End If


                            If par.IfNull(myReader("ESCL_QUART4"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART4"), -1) & ") "
                                N = True

                            End If

                            If par.IfNull(myReader("ESCL_QUART5"), -1) <> -1 Then

                                sLocalita = sLocalita & " AND tab_quartieri.id NOT IN (SELECT TAB_QUARTIERI.ID FROM siscom_mi.TAB_QUARTIERI WHERE ID=" & par.IfNull(myReader("ESCL_QUART5"), -1) & ") "
                                N = True

                            End If



                        End If






                        '---------------------------------


                        M = False

                        If par.IfNull(myReader("PREF_QUART1"), -1) <> -1 Then
                            '  If M = True Then
                            sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART1"), -1) & " "
                            '   Else
                            ' sQuartiere = " (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART1"), -1) & " "
                            M = True
                            '  End If
                        End If



                        If par.IfNull(myReader("PREF_QUART2"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART2"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_QUART1"), -1) <> -1 Then

                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART2"), -1) & " "
                                    M = True
                                Else

                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART2"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_QUART3"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART3"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 Then

                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART3"), -1) & " "
                                    M = True
                                Else

                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART3"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_QUART4"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART4"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 Then

                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART4"), -1) & " "
                                    M = True
                                Else

                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART4"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_QUART5"), -1) <> -1 Then
                            If M = False Then
                                sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART5"), "") & " "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 And par.IfNull(myReader("PREF_QUART5"), -1) = -1 Then


                                    sQuartiere = sQuartiere & " AND (tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART5"), -1) & " "
                                    M = True

                                Else
                                    sQuartiere = sQuartiere & " OR tab_quartieri.id=" & par.IfNull(myReader("PREF_QUART5"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_QUART5"), -1) = -1 And par.IfNull(myReader("PREF_QUART4"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART1"), -1) = -1 Then


                        Else
                            If M = True Then sQuartiere = sQuartiere & ") "

                        End If





                        M = False

                        If par.IfNull(myReader("PREF_COMPLESSO1"), -1) <> -1 Then
                            '  If M = True Then
                            sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO1"), -1) & " "
                            '    Else
                            ' sComplesso = " (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO1"), -1) & " "
                            M = True
                            '   End If
                        End If



                        If par.IfNull(myReader("PREF_COMPLESSO2"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO2"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) <> -1 Then

                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO2"), -1) & " "
                                    M = True
                                Else

                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO2"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO3"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO3"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 Then

                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO3"), -1) & " "
                                    M = True
                                Else

                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO3"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO4"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO4"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 Then

                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO4"), -1) & " "
                                    M = True
                                Else

                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO4"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO5"), -1) <> -1 Then
                            If M = False Then
                                sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO5"), "") & " "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO5"), -1) = -1 Then


                                    sComplesso = sComplesso & " AND (COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO5"), -1) & " "
                                    M = True

                                Else
                                    sComplesso = sComplesso & " OR COMPLESSI_IMMOBILIARI.id=" & par.IfNull(myReader("PREF_COMPLESSO5"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_COMPLESSO5"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO4"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 Then


                        Else
                            If M = True Then sComplesso = sComplesso & ") "

                        End If










                        M = False

                        If par.IfNull(myReader("PREF_EDIFICIO1"), -1) <> -1 Then
                            '  If M = True Then
                            sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO1"), -1) & " "
                            'Else
                            ' sEdificio = " (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO1"), -1) & " "
                            M = True
                            '  End If
                        End If



                        If par.IfNull(myReader("PREF_EDIFICIO2"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO2"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) <> -1 Then

                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO2"), -1) & " "
                                    M = True
                                Else

                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO2"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_EDIFICIO3"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO3"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 Then

                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO3"), -1) & " "
                                    M = True
                                Else

                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO3"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_EDIFICIO4"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO4"), -1) & " "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 Then

                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO4"), -1) & " "
                                    M = True
                                Else

                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO4"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_EDIFICIO5"), -1) <> -1 Then
                            If M = False Then
                                sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO5"), "") & " "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO5"), -1) = -1 Then


                                    sEdificio = sEdificio & " AND (EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO5"), -1) & " "
                                    M = True

                                Else
                                    sEdificio = sEdificio & " OR EDIFICI.id=" & par.IfNull(myReader("PREF_EDIFICIO5"), -1) & " "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_EDIFICIO5"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO4"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 Then


                        Else
                            If M = True Then sEdificio = sEdificio & ") "

                        End If








                        M = False

                        If par.IfNull(myReader("PREF_INDIRIZZO1"), "") <> "" Then
                            '    If M = True Then
                            sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO1"), "") & "' "
                            '   Else
                            '  sIndirizzo = " AND (INDIRIZZO='" & par.IfNull(myReader("PREF_INDIRIZZO1"), "") & "' "
                            M = True
                            'End If
                        End If



                        If par.IfNull(myReader("PREF_INDIRIZZO2"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & "AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO2"), "") & "' "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_INDIRIZZO1"), -1) <> "" Then

                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO2"), "") & "' "
                                    M = True
                                Else

                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO2"), "") & "' "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_INDIRIZZO3"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & "AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO3"), "") & "' "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" Then

                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO3"), -1) & "' "
                                    M = True
                                Else

                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO3"), -1) & "' "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_INDIRIZZO4"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & "AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO4"), "") & "' "
                                M = True
                            Else

                                If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" Then

                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO4"), "") & "' "
                                    M = True
                                Else

                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO4"), "") & "' "
                                    M = True

                                End If
                            End If
                        End If




                        If par.IfNull(myReader("PREF_INDIRIZZO5"), "") <> "" Then
                            If M = False Then
                                sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO5"), "") & "' "
                                M = True
                            Else
                                If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO5"), "") = "" Then


                                    sIndirizzo = sIndirizzo & " AND ((t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO5"), "") & "' "
                                    M = True

                                Else
                                    sIndirizzo = sIndirizzo & " OR (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo)='" & par.IfNull(myReader("PREF_INDIRIZZO5"), "") & "' "
                                    M = True

                                End If
                            End If
                        End If


                        If par.IfNull(myReader("PREF_INDIRIZZO5"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO4"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" Then


                        Else
                            If M = True Then sIndirizzo = sIndirizzo & ") "

                        End If


                        'piani------------------------------------------------------------------------

                        M = False


                        'piani Con Ascensore-----------------
                        Dim dtP As New Data.DataTable
                        dtP = caricaDTpiani()
                        Dim k As Integer = 0

                        Dim trovatoDa As Integer = 0
                        If par.IfNull(myReader("PREF_PIANI_DA_CON"), "-1") <> "-1" Then
                            If par.IfNull(myReader("PREF_PIANI_A_CON"), "-1") <> "-1" Then
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If trovatoDa = 1 Then
                                        If rowScart.ItemArray(0) <> par.IfNull(myReader("PREF_PIANI_A_CON"), "-1") Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        Else
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "' "
                                            Exit Do
                                        End If
                                    End If
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_CON"), "-1") Then
                                        trovatoDa = 1
                                        sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                    End If

                                    k = k + 1
                                Loop

                            Else
                                trovatoDa = 0
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_CON"), "-1") Then
                                        trovatoDa = 1
                                        sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                    Else
                                        If trovatoDa = 1 Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        End If
                                    End If
                                    k = k + 1
                                Loop

                            End If
                            M = True
                            sPiano = sPiano & ") AND ALLOGGI.ASCENSORE='1') "


                        Else

                            sPiano = sPiano & " AND ((ALLOGGI.ASCENSORE='1') "

                            M = True

                        End If



                        'piani Senza Ascensore-----------------

                        trovatoDa = 0

                        If par.IfNull(myReader("PREF_PIANI_DA_SENZA"), "-1") <> "-1" Then
                            If par.IfNull(myReader("PREF_PIANI_A_SENZA"), "-1") <> "-1" Then
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If trovatoDa = 1 Then
                                        If rowScart.ItemArray(0) <> par.IfNull(myReader("PREF_PIANI_A_SENZA"), "-1") Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        Else
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "' "
                                            Exit Do
                                        End If
                                    End If
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_SENZA"), "-1") Then
                                        If M = False Then
                                            sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                            M = True
                                        Else
                                            sPiano = sPiano & " OR (TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                        End If
                                        trovatoDa = 1
                                    End If

                                    k = k + 1
                                Loop

                            Else
                                trovatoDa = 0
                                k = 0
                                Do While k < dtP.Rows.Count
                                    Dim rowScart As Data.DataRow = dtP.Rows(k)
                                    If rowScart.ItemArray(0) = par.IfNull(myReader("PREF_PIANI_DA_SENZA"), "-1") Then
                                        trovatoDa = 1
                                        If M = False Then
                                            sPiano = sPiano & " AND ((TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                            M = True
                                        Else
                                            sPiano = sPiano & " OR (TIPO_LIVELLO_PIANO.COD IN ('" & rowScart.ItemArray(0) & "'"
                                        End If
                                    Else
                                        If trovatoDa = 1 Then
                                            sPiano = sPiano & ", '" & rowScart.ItemArray(0) & "'"
                                        End If
                                    End If
                                    k = k + 1
                                Loop

                            End If
                            sPiano = sPiano & ") AND ALLOGGI.ASCENSORE<>'1')) "

                        Else
                            If M = True Then
                                sPiano = sPiano & "or (ALLOGGI.ASCENSORE<>'1')) "
                            End If

                        End If


                        sPiano = sPiano











                        '---------------------------------------------------------------



                        '-----------------parti restanti

                        sImm2 = ""

                        If sLocalita = "" And sQuartiere = "" And sComplesso = "" And sEdificio = "" And sIndirizzo = "" Then

                            M = False
                        Else
                            M = True
                        End If


                        If par.IfNull(myReader("CONDOMINIO"), "0") = "1" Then

                            sImm = sImm & " AND cond_edifici.id_condominio IS NULL"


                        End If





                        If par.IfNull(myReader("BARRIERE"), "0") = "1" Then

                            sImm = sImm & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                            sImm2 = sImm2 & " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "

                        End If

                        'opzioni per superficie


                        If par.IfNull(myReader("pref_sup_min"), -1) <> -1 And par.IfNull(myReader("pref_sup_max"), -1) <> -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID and valore between " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & " and " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ")"
                            sImm2 = sImm2 & " and alloggi.sup >=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & " and and alloggi.sup <=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ""
                        End If


                        If par.IfNull(myReader("pref_sup_min"), -1) <> -1 And par.IfNull(myReader("pref_sup_max"), -1) = -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID and valore >= " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & ")"
                            sImm2 = sImm2 & " and alloggi.sup >=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_min")), " ") & ""
                        End If

                        If par.IfNull(myReader("pref_sup_min"), -1) = -1 And par.IfNull(myReader("pref_sup_max"), -1) <> -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID and valore <= " & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ")"
                            sImm2 = sImm2 & " and alloggi.sup <=" & par.IfNull(par.VirgoleInPunti(myReader("pref_sup_max")), " ") & ""
                        End If


                        If par.IfNull(myReader("pref_sup_min"), -1) = -1 And par.IfNull(myReader("pref_sup_max"), -1) = -1 Then
                            sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID" & ")"
                        End If



                        'If sImm <> "" Then

                        'Else


                        'sZona = " and alloggi.id=00000"
                        If lbl_invalidi.Text <> "0" Then
                            sImm = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL)"
                            sImm2 = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL)"
                        End If
                        '   BindGrid1()
                        'End If







                        '--------------------------QUERY------------------------------

                        If par.IfNull(myReader("PREF_INDIRIZZO1"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO2"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO3"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO4"), "") = "" And par.IfNull(myReader("PREF_INDIRIZZO5"), "") = "" Then

                        Else

                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                        & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                         & " dimensioni.valore as sup, " _
                        & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                        & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                        & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                         & " complessi_immobiliari.denominazione AS complesso, " _
                        & " edifici.denominazione AS edificio, " _
                        & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                        & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                        & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                        & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                        & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                        & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                        & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                        & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                        & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                        & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                        & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                        & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                        & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                        & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                        & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                        & " And comuni_nazioni.cod = edifici.cod_comune " _
                        & " " & sIndirizzo & " " _
                        & " " & sPiano & " " _
                         & " " & sImm & " " _
                        & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sIndirizzo & " " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt As New Data.DataTable
                            da.Fill(dt)

                            Dim RIGA1 As System.Data.DataRow
                            For i As Integer = 0 To dt.Rows.Count - 1
                                RIGA1 = dtDaRiempire.NewRow()
                                RIGA1.Item("ID_ALLOGGIO") = par.IfNull(dt.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA1.Item("ID_QUARTIERE") = par.IfNull(dt.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA1.Item("ID_PIANO") = par.IfNull(dt.Rows(i).Item("ID_PIANO"), "")
                                RIGA1.Item("ID_COMUNE") = par.IfNull(dt.Rows(i).Item("ID_COMUNE"), "")
                                RIGA1.Item("COD_ALLOGGIO") = par.IfNull(dt.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA1.Item("COMUNE") = par.IfNull(dt.Rows(i).Item("COMUNE"), "")
                                RIGA1.Item("ZONA") = par.IfNull(dt.Rows(i).Item("ZONA"), "")
                                RIGA1.Item("QUARTIERE") = par.IfNull(dt.Rows(i).Item("QUARTIERE"), "")
                                RIGA1.Item("COMPLESSO") = par.IfNull(dt.Rows(i).Item("COMPLESSO"), "")
                                RIGA1.Item("EDIFICIO") = par.IfNull(dt.Rows(i).Item("EDIFICIO"), "")
                                RIGA1.Item("INDIRIZZO") = par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")
                                RIGA1.Item("NUM_LOCALI") = par.IfNull(dt.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA1.Item("PIANO") = par.IfNull(dt.Rows(i).Item("PIANO"), "")
                                RIGA1.Item("SUP") = par.IfNull(dt.Rows(i).Item("SUP"), "")
                                RIGA1.Item("ELEVATORE") = par.IfNull(dt.Rows(i).Item("ELEVATORE"), "")
                                RIGA1.Item("BARRIERE") = par.IfNull(dt.Rows(i).Item("BARRIERE"), "")
                                RIGA1.Item("CONDOMINIO") = par.IfNull(dt.Rows(i).Item("CONDOMINIO"), "")
                                RIGA1.Item("DATA_DISPONIBILITA") = par.IfNull(dt.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA1.Item("PROPRIETA") = par.IfNull(dt.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA1)
                            Next

                        End If

                        If par.IfNull(myReader("PREF_EDIFICIO1"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO2"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO3"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO4"), -1) = -1 And par.IfNull(myReader("PREF_EDIFICIO5"), -1) = -1 Then

                        Else

                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                      & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                    & " dimensioni.valore as sup, " _
                      & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                      & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                      & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                       & " complessi_immobiliari.denominazione AS complesso, " _
                      & " edifici.denominazione AS edificio, " _
                      & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                      & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                      & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                      & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                      & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                      & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                      & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                      & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                      & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                      & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                      & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                      & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                      & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                      & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                      & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                      & " And comuni_nazioni.cod = edifici.cod_comune " _
                      & " " & sEdificio & " " _
                      & " " & sPiano & " " _
                      & " " & sImm & " " _
                       & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt2 As New Data.DataTable
                            da2.Fill(dt2)

                            Dim RIGA2 As System.Data.DataRow
                            For i As Integer = 0 To dt2.Rows.Count - 1
                                RIGA2 = dtDaRiempire.NewRow()
                                RIGA2.Item("ID_ALLOGGIO") = par.IfNull(dt2.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA2.Item("ID_QUARTIERE") = par.IfNull(dt2.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA2.Item("ID_PIANO") = par.IfNull(dt2.Rows(i).Item("ID_PIANO"), "")
                                RIGA2.Item("ID_COMUNE") = par.IfNull(dt2.Rows(i).Item("ID_COMUNE"), "")
                                RIGA2.Item("COD_ALLOGGIO") = par.IfNull(dt2.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA2.Item("COMUNE") = par.IfNull(dt2.Rows(i).Item("COMUNE"), "")
                                RIGA2.Item("ZONA") = par.IfNull(dt2.Rows(i).Item("ZONA"), "")
                                RIGA2.Item("QUARTIERE") = par.IfNull(dt2.Rows(i).Item("QUARTIERE"), "")
                                RIGA2.Item("COMPLESSO") = par.IfNull(dt2.Rows(i).Item("COMPLESSO"), "")
                                RIGA2.Item("EDIFICIO") = par.IfNull(dt2.Rows(i).Item("EDIFICIO"), "")
                                RIGA2.Item("INDIRIZZO") = par.IfNull(dt2.Rows(i).Item("INDIRIZZO"), "")
                                RIGA2.Item("NUM_LOCALI") = par.IfNull(dt2.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA2.Item("PIANO") = par.IfNull(dt2.Rows(i).Item("PIANO"), "")
                                RIGA2.Item("SUP") = par.IfNull(dt2.Rows(i).Item("SUP"), "")
                                RIGA2.Item("ELEVATORE") = par.IfNull(dt2.Rows(i).Item("ELEVATORE"), "")
                                RIGA2.Item("BARRIERE") = par.IfNull(dt2.Rows(i).Item("BARRIERE"), "")
                                RIGA2.Item("CONDOMINIO") = par.IfNull(dt2.Rows(i).Item("CONDOMINIO"), "")
                                RIGA2.Item("DATA_DISPONIBILITA") = par.IfNull(dt2.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA2.Item("PROPRIETA") = par.IfNull(dt2.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA2)
                            Next

                        End If




                        If par.IfNull(myReader("PREF_COMPLESSO1"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO2"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO3"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO4"), -1) = -1 And par.IfNull(myReader("PREF_COMPLESSO5"), -1) = -1 Then

                        Else

                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                     & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                      & " dimensioni.valore as sup, " _
                     & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                     & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                     & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                      & " complessi_immobiliari.denominazione AS complesso, " _
                     & " edifici.denominazione AS edificio, " _
                     & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                     & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                     & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                     & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                     & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                     & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                     & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                     & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                     & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                     & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                     & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                     & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                     & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                     & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                     & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                     & " And comuni_nazioni.cod = edifici.cod_comune " _
                     & " " & sComplesso & " " _
                     & " " & sPiano & " " _
                     & " " & sImm & " " _
                     & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt3 As New Data.DataTable
                            da3.Fill(dt3)

                            Dim RIGA3 As System.Data.DataRow
                            For i As Integer = 0 To dt3.Rows.Count - 1
                                RIGA3 = dtDaRiempire.NewRow()
                                RIGA3.Item("ID_ALLOGGIO") = par.IfNull(dt3.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA3.Item("ID_QUARTIERE") = par.IfNull(dt3.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA3.Item("ID_PIANO") = par.IfNull(dt3.Rows(i).Item("ID_PIANO"), "")
                                RIGA3.Item("ID_COMUNE") = par.IfNull(dt3.Rows(i).Item("ID_COMUNE"), "")
                                RIGA3.Item("COD_ALLOGGIO") = par.IfNull(dt3.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA3.Item("COMUNE") = par.IfNull(dt3.Rows(i).Item("COMUNE"), "")
                                RIGA3.Item("ZONA") = par.IfNull(dt3.Rows(i).Item("ZONA"), "")
                                RIGA3.Item("QUARTIERE") = par.IfNull(dt3.Rows(i).Item("QUARTIERE"), "")
                                RIGA3.Item("COMPLESSO") = par.IfNull(dt3.Rows(i).Item("COMPLESSO"), "")
                                RIGA3.Item("EDIFICIO") = par.IfNull(dt3.Rows(i).Item("EDIFICIO"), "")
                                RIGA3.Item("INDIRIZZO") = par.IfNull(dt3.Rows(i).Item("INDIRIZZO"), "")
                                RIGA3.Item("NUM_LOCALI") = par.IfNull(dt3.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA3.Item("PIANO") = par.IfNull(dt3.Rows(i).Item("PIANO"), "")
                                RIGA3.Item("SUP") = par.IfNull(dt3.Rows(i).Item("SUP"), "")
                                RIGA3.Item("ELEVATORE") = par.IfNull(dt3.Rows(i).Item("ELEVATORE"), "")
                                RIGA3.Item("BARRIERE") = par.IfNull(dt3.Rows(i).Item("BARRIERE"), "")
                                RIGA3.Item("CONDOMINIO") = par.IfNull(dt3.Rows(i).Item("CONDOMINIO"), "")
                                RIGA3.Item("DATA_DISPONIBILITA") = par.IfNull(dt3.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA3.Item("PROPRIETA") = par.IfNull(dt3.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA3)
                            Next


                        End If

                        If par.IfNull(myReader("PREF_QUART1"), -1) = -1 And par.IfNull(myReader("PREF_QUART2"), -1) = -1 And par.IfNull(myReader("PREF_QUART3"), -1) = -1 And par.IfNull(myReader("PREF_QUART4"), -1) = -1 And par.IfNull(myReader("PREF_QUART5"), -1) = -1 Then

                        Else


                            sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                        & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                        & " dimensioni.valore as sup, " _
                        & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                        & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                        & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                         & " complessi_immobiliari.denominazione AS complesso, " _
                        & " edifici.denominazione AS edificio, " _
                        & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                        & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                        & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                        & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                        & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                        & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                        & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                        & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                        & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                        & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                        & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                        & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                        & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                        & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                        & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                        & " And comuni_nazioni.cod = edifici.cod_comune " _
                        & " " & sQuartiere & " " _
                        & " " & sPiano & " " _
                        & " " & sImm & " " _
                        & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                            Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                            Dim dt4 As New Data.DataTable
                            da4.Fill(dt4)

                            Dim RIGA4 As System.Data.DataRow
                            For i As Integer = 0 To dt4.Rows.Count - 1
                                RIGA4 = dtDaRiempire.NewRow()
                                RIGA4.Item("ID_ALLOGGIO") = par.IfNull(dt4.Rows(i).Item("ID_ALLOGGIO"), "")
                                RIGA4.Item("ID_QUARTIERE") = par.IfNull(dt4.Rows(i).Item("ID_QUARTIERE"), "")
                                RIGA4.Item("ID_PIANO") = par.IfNull(dt4.Rows(i).Item("ID_PIANO"), "")
                                RIGA4.Item("ID_COMUNE") = par.IfNull(dt4.Rows(i).Item("ID_COMUNE"), "")
                                RIGA4.Item("COD_ALLOGGIO") = par.IfNull(dt4.Rows(i).Item("COD_ALLOGGIO"), "")
                                RIGA4.Item("COMUNE") = par.IfNull(dt4.Rows(i).Item("COMUNE"), "")
                                RIGA4.Item("ZONA") = par.IfNull(dt4.Rows(i).Item("ZONA"), "")
                                RIGA4.Item("QUARTIERE") = par.IfNull(dt4.Rows(i).Item("QUARTIERE"), "")
                                RIGA4.Item("COMPLESSO") = par.IfNull(dt4.Rows(i).Item("COMPLESSO"), "")
                                RIGA4.Item("EDIFICIO") = par.IfNull(dt4.Rows(i).Item("EDIFICIO"), "")
                                RIGA4.Item("INDIRIZZO") = par.IfNull(dt4.Rows(i).Item("INDIRIZZO"), "")
                                RIGA4.Item("NUM_LOCALI") = par.IfNull(dt4.Rows(i).Item("NUM_LOCALI"), "")
                                RIGA4.Item("PIANO") = par.IfNull(dt4.Rows(i).Item("PIANO"), "")
                                RIGA4.Item("SUP") = par.IfNull(dt4.Rows(i).Item("SUP"), "")
                                RIGA4.Item("ELEVATORE") = par.IfNull(dt4.Rows(i).Item("ELEVATORE"), "")
                                RIGA4.Item("BARRIERE") = par.IfNull(dt4.Rows(i).Item("BARRIERE"), "")
                                RIGA4.Item("CONDOMINIO") = par.IfNull(dt4.Rows(i).Item("CONDOMINIO"), "")
                                RIGA4.Item("DATA_DISPONIBILITA") = par.IfNull(dt4.Rows(i).Item("DATA_DISPONIBILITA"), "")
                                RIGA4.Item("PROPRIETA") = par.IfNull(dt4.Rows(i).Item("PROPRIETA"), "")
                                dtDaRiempire.Rows.Add(RIGA4)
                            Next

                        End If


                        sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                 & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                 & " dimensioni.valore as sup, " _
                 & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                 & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                 & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                  & " complessi_immobiliari.denominazione AS complesso, " _
                 & " edifici.denominazione AS edificio, " _
                 & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                 & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                 & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                 & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                 & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                 & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                 & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                 & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                 & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                 & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                 & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                 & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                 & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                 & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                 & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                 & " And comuni_nazioni.cod = edifici.cod_comune " _
                 & " " & sLocalita & " " _
                 & " " & sPiano & " " _
                 & " " & sImm & " " _
                    & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sLocalita2 & " " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                        Dim da5 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                        Dim dt5 As New Data.DataTable
                        da5.Fill(dt5)

                        Dim RIGA5 As System.Data.DataRow
                        For i As Integer = 0 To dt5.Rows.Count - 1
                            RIGA5 = dtDaRiempire.NewRow()
                            RIGA5.Item("ID_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("ID_ALLOGGIO"), "")
                            RIGA5.Item("ID_QUARTIERE") = par.IfNull(dt5.Rows(i).Item("ID_QUARTIERE"), "")
                            RIGA5.Item("ID_PIANO") = par.IfNull(dt5.Rows(i).Item("ID_PIANO"), "")
                            RIGA5.Item("ID_COMUNE") = par.IfNull(dt5.Rows(i).Item("ID_COMUNE"), "")
                            RIGA5.Item("COD_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("COD_ALLOGGIO"), "")
                            RIGA5.Item("COMUNE") = par.IfNull(dt5.Rows(i).Item("COMUNE"), "")
                            RIGA5.Item("ZONA") = par.IfNull(dt5.Rows(i).Item("ZONA"), "")
                            RIGA5.Item("QUARTIERE") = par.IfNull(dt5.Rows(i).Item("QUARTIERE"), "")
                            RIGA5.Item("COMPLESSO") = par.IfNull(dt5.Rows(i).Item("COMPLESSO"), "")
                            RIGA5.Item("EDIFICIO") = par.IfNull(dt5.Rows(i).Item("EDIFICIO"), "")
                            RIGA5.Item("INDIRIZZO") = par.IfNull(dt5.Rows(i).Item("INDIRIZZO"), "")
                            RIGA5.Item("NUM_LOCALI") = par.IfNull(dt5.Rows(i).Item("NUM_LOCALI"), "")
                            RIGA5.Item("PIANO") = par.IfNull(dt5.Rows(i).Item("PIANO"), "")
                            RIGA5.Item("SUP") = par.IfNull(dt5.Rows(i).Item("SUP"), "")
                            RIGA5.Item("ELEVATORE") = par.IfNull(dt5.Rows(i).Item("ELEVATORE"), "")
                            RIGA5.Item("BARRIERE") = par.IfNull(dt5.Rows(i).Item("BARRIERE"), "")
                            RIGA5.Item("CONDOMINIO") = par.IfNull(dt5.Rows(i).Item("CONDOMINIO"), "")
                            RIGA5.Item("DATA_DISPONIBILITA") = par.IfNull(dt5.Rows(i).Item("DATA_DISPONIBILITA"), "")
                            RIGA5.Item("PROPRIETA") = par.IfNull(dt5.Rows(i).Item("PROPRIETA"), "")
                            dtDaRiempire.Rows.Add(RIGA5)
                        Next

                    Else
                        If lbl_invalidi.Text <> "0" Then
                            sImm = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL)"
                            sImm2 = " AND (ALLOGGI.BARRIERE_ARC='0' OR ALLOGGI.BARRIERE_ARC IS NULL) "
                        End If

                        sImm = sImm & " AND valore in (SELECT DISTINCT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID" & ")"

                        sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                 & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                & " dimensioni.valore as sup, " _
                 & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                 & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                 & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                  & " complessi_immobiliari.denominazione AS complesso, " _
                 & " edifici.denominazione AS edificio, " _
                 & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                 & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                 & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                 & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                 & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari, siscom_mi.dimensioni, " _
                 & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                 & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                 & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                 & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                 & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                 & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                 & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                 & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                 & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                 & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                 & " And comuni_nazioni.cod = edifici.cod_comune " _
                 & " " & sLocalita & " " _
                 & " " & sPiano & " " _
                 & " " & sImm & " " _
                 & " union " _
                         & " SELECT DISTINCT  (alloggi.id) AS id_alloggio, comuni_nazioni.nome AS comune, comuni_nazioni.ID AS id_comune, " _
                         & " '' AS quartiere, null AS id_quartiere, TO_NUMBER (alloggi.sup) AS sup, " _
                         & " (CASE WHEN alloggi.ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                         & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano,  tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
                         & " ( t_tipo_indirizzo.descrizione|| ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo, " _
                         & " TO_CHAR (TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'), 'DD/MM/YYYY') AS data_disponibilita, " _
                         & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, '' AS condominio " _
                         & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, comuni_nazioni " _
                         & " WHERE  fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+) " _
                         & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                         & "  AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' " _
                         & " AND prenotato = '0' AND alloggi.stato = 5 AND proprieta = 1 " _
                         & " AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                         & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                         & " " & sLocalita & " " _
                         & " " & sPiano & " " _
                         & " " & sImm2 & " " _
                         & " ORDER BY indirizzo ASC "

                        Dim da5 As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)
                        Dim dt5 As New Data.DataTable
                        da5.Fill(dt5)

                        Dim RIGA5 As System.Data.DataRow
                        For i As Integer = 0 To dt5.Rows.Count - 1
                            RIGA5 = dtDaRiempire.NewRow()
                            RIGA5.Item("ID_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("ID_ALLOGGIO"), "")
                            RIGA5.Item("ID_QUARTIERE") = par.IfNull(dt5.Rows(i).Item("ID_QUARTIERE"), "")
                            RIGA5.Item("ID_PIANO") = par.IfNull(dt5.Rows(i).Item("ID_PIANO"), "")
                            RIGA5.Item("ID_COMUNE") = par.IfNull(dt5.Rows(i).Item("ID_COMUNE"), "")
                            RIGA5.Item("COD_ALLOGGIO") = par.IfNull(dt5.Rows(i).Item("COD_ALLOGGIO"), "")
                            RIGA5.Item("COMUNE") = par.IfNull(dt5.Rows(i).Item("COMUNE"), "")
                            RIGA5.Item("ZONA") = par.IfNull(dt5.Rows(i).Item("ZONA"), "")
                            RIGA5.Item("QUARTIERE") = par.IfNull(dt5.Rows(i).Item("QUARTIERE"), "")
                            RIGA5.Item("COMPLESSO") = par.IfNull(dt5.Rows(i).Item("COMPLESSO"), "")
                            RIGA5.Item("EDIFICIO") = par.IfNull(dt5.Rows(i).Item("EDIFICIO"), "")
                            RIGA5.Item("INDIRIZZO") = par.IfNull(dt5.Rows(i).Item("INDIRIZZO"), "")
                            RIGA5.Item("NUM_LOCALI") = par.IfNull(dt5.Rows(i).Item("NUM_LOCALI"), "")
                            RIGA5.Item("PIANO") = par.IfNull(dt5.Rows(i).Item("PIANO"), "")
                            RIGA5.Item("SUP") = par.IfNull(dt5.Rows(i).Item("SUP"), "")
                            RIGA5.Item("ELEVATORE") = par.IfNull(dt5.Rows(i).Item("ELEVATORE"), "")
                            RIGA5.Item("BARRIERE") = par.IfNull(dt5.Rows(i).Item("BARRIERE"), "")
                            RIGA5.Item("CONDOMINIO") = par.IfNull(dt5.Rows(i).Item("CONDOMINIO"), "")
                            RIGA5.Item("DATA_DISPONIBILITA") = par.IfNull(dt5.Rows(i).Item("DATA_DISPONIBILITA"), "")
                            RIGA5.Item("PROPRIETA") = par.IfNull(dt5.Rows(i).Item("PROPRIETA"), "")
                            dtDaRiempire.Rows.Add(RIGA5)
                        Next



                    End If


                    BindGrid2()




                    dtDaRiempire = DeleteDuplicateFromDataTable(dtDaRiempire, "ID_ALLOGGIO")
                    DataGrid2.DataSource = dtDaRiempire
                    DataGrid2.DataBind()


                    'DataGrid2.DataSource = dtDaRiempire
                    'DataGrid2.DataBind()
                    myReader.Close()

                    lbl_UIPref.Text = "ELENCO UNITA' DISPONIBILI PER PREFERENZE - Totale: " & dtDaRiempire.Rows.Count & " Unità"
                    'rimuovere doppioni da datagrid






                    '----------------------------

            End Select



            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        


        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            End If
          
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try



    End Sub



    Public Property sZona() As String
        Get
            If Not (ViewState("par_sZona") Is Nothing) Then
                Return CStr(ViewState("par_sZona"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sZona") = value
        End Set

    End Property

    Public Property preferenze() As Integer
        Get
            If Not (ViewState("par_preferenze") Is Nothing) Then
                Return CInt(ViewState("par_preferenze"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_preferenze") = value
        End Set

    End Property

    Protected Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
             e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid2.CurrentPageIndex = e.NewPageIndex
            'BindGrid1()
            CaricaDati()
        End If
    End Sub

    Public Property sStringaSQL2() As String
        Get
            If Not (ViewState("par_sStringaSQL2") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL2"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL2") = value
        End Set
    End Property

    Private Sub BindGrid2()

        'sStringaSQL2 = "SELECT  alloggi.id as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
        '              & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
        '              & " (SELECT valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_CONV' AND id_unita_immobiliare = unita_immobiliari.ID) AS sup, " _
        '              & " DECODE (alloggi.ascensore, 1, 'SI') AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
        '              & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, " _
        '              & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
        '              & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
        '              & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
        '              & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
        '              & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari," _
        '              & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
        '              & " WHERE alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
        '              & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
        '              & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' " _
        '              & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
        '              & " AND cond_edifici.id_edificio(+) = edifici.ID AND comuni_nazioni.cod = edifici.cod_comune AND complessi_immobiliari.ID(+) = edifici.id_complesso " _
        '              & " AND tab_quartieri.ID(+) = complessi_immobiliari.id_quartiere " _
        '              & " ORDER BY alloggi.tipo_indirizzo ASC, alloggi.indirizzo ASC"





        sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                     & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                     & " (SELECT distinct valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID) AS sup, " _
                     & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                     & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                     & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                     & " complessi_immobiliari.denominazione AS complesso, " _
                     & " edifici.denominazione AS edificio, " _
                     & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                     & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                     & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                     & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                     & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari," _
                     & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                     & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                     & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                     & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                     & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                     & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                     & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                     & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                     & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                     & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                     & " And comuni_nazioni.cod = edifici.cod_comune " _
& "UNION " _
& " SELECT DISTINCT (alloggi.ID) AS id_alloggio, alloggi.comune AS comune,comuni_nazioni.ID as id_comune,'' AS quartiere, null AS id_quartiere," _
 & " TO_NUMBER (alloggi.sup) AS sup,(CASE WHEN ascensore = 1 THEN 'SI' END) AS elevatore, DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere," _
 & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, '' AS complesso, '' AS edificio, " _
   & " ( t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo|| ', '|| alloggi.num_civico) AS indirizzo," _
   & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita,alloggi.zona, alloggi.cod_alloggio, " _
   & " alloggi.num_locali,'' AS condominio " _
   & " FROM t_tipo_proprieta, alloggi,t_tipo_all_erp, t_tipo_indirizzo," _
   & " siscom_mi.tipo_livello_piano,comuni_nazioni" _
   & " WHERE fl_por = '0' AND alloggi.comune = comuni_nazioni.nome(+)" _
   & " AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano" _
   & " AND alloggi.proprieta = t_tipo_proprieta.cod(+)" _
   & " AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5" _
   & " AND proprieta = 1 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+)" _
   & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+)" _
   & " ORDER BY indirizzo ASC "

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "ALLOGGI")
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        lbl_UIGeneral.Text = ""
        lbl_UIGeneral.Text = "ELENCO UNITA' DISPONIBILI GENERALE - Totale: " & ds.Tables(0).Rows.Count & " Unità"

    End Sub

    Private Sub BindGrid1()


        sStringaSQL2 = "SELECT distinct (alloggi.id) as id_alloggio, comuni_nazioni.nome as comune,comuni_nazioni.ID as id_comune, " _
                     & " tab_quartieri.nome as quartiere, tab_quartieri.id as id_quartiere, " _
                     & " (SELECT distinct valore FROM siscom_mi.dimensioni WHERE cod_tipologia = 'SUP_NETTA' AND id_unita_immobiliare = unita_immobiliari.ID) AS sup, " _
                     & " (CASE WHEN (SELECT DISTINCT unita_immobiliari.ID FROM siscom_mi.impianti, siscom_mi.impianti_scale, siscom_mi.unita_immobiliari ui WHERE ui.ID = unita_immobiliari.ID AND impianti.cod_tipologia = 'SO' AND impianti_scale.id_scala = ui.id_scala AND impianti.ID = impianti_scale.id_impianto) >0 THEN 'SI' END) AS elevatore, " _
                     & " DECODE (alloggi.barriere_arc, 1, 'SI') AS barriere, " _
                     & " t_tipo_proprieta.descrizione AS proprieta, tipo_livello_piano.descrizione AS piano, tipo_livello_piano.cod AS id_piano, " _
                      & " complessi_immobiliari.denominazione AS complesso, " _
                     & " edifici.denominazione AS edificio, " _
                     & " (t_tipo_indirizzo.descrizione || ' ' || alloggi.indirizzo ||', ' || alloggi.num_civico) AS indirizzo, " _
                     & " TO_CHAR(TO_DATE (alloggi.data_disponibilita, 'YYYYmmdd'),'DD/MM/YYYY') AS data_disponibilita, " _
                     & " alloggi.zona, alloggi.cod_alloggio, alloggi.num_locali, " _
                     & " CASE WHEN cond_edifici.id_condominio IS NULL THEN 'NO' ELSE 'SI' END AS condominio " _
                     & " FROM t_tipo_proprieta, alloggi, t_tipo_all_erp, t_tipo_indirizzo, siscom_mi.tipo_livello_piano, siscom_mi.unita_immobiliari," _
                     & " siscom_mi.edifici, siscom_mi.cond_edifici, comuni_nazioni, siscom_mi.complessi_immobiliari, siscom_mi.tab_quartieri " _
                     & " WHERE unita_immobiliari.ID IN (Select id_unita FROM siscom_mi.unita_stato_manutentivo WHERE unita_stato_manutentivo.riassegnabile = 1 " _
                     & " AND (   unita_stato_manutentivo.tipo_riassegnabile = '1' OR (    unita_stato_manutentivo.tipo_riassegnabile = '0' AND unita_stato_manutentivo.fine_lavori = 1))) " _
                     & " AND alloggi.cod_alloggio = unita_immobiliari.cod_unita_immobiliare(+) AND fl_por = '0' AND eqcanone = '0' AND tipo_livello_piano.cod = alloggi.piano " _
                     & " AND (   unita_immobiliari.id_destinazione_uso = 1 OR unita_immobiliari.id_destinazione_uso = 2) " _
                     & " AND alloggi.proprieta = t_tipo_proprieta.cod(+) AND assegnato = '0' AND prenotato = '0' AND alloggi.stato = 5 " _
                     & " AND unita_immobiliari.cod_tipo_disponibilita = 'LIBE' AND complessi_immobiliari.ID = edifici.id_complesso AND tab_quartieri.ID = complessi_immobiliari.id_quartiere " _
                     & " And edifici.ID = unita_immobiliari.id_edificio AND fl_piano_vendita = 0 AND alloggi.tipo_alloggio = t_tipo_all_erp.cod(+) " _
                     & " AND alloggi.tipo_indirizzo = t_tipo_indirizzo.cod(+) " _
                     & " AND cond_edifici.id_edificio(+) = edifici.ID " _
                     & " And comuni_nazioni.cod = edifici.cod_comune " _
                     & " " & sZona & " " _
                     & " ORDER BY indirizzo ASC "

        'par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "ALLOGGI")
        DataGrid2.DataSource = ds
        DataGrid2.DataBind()
        lbl_UIPref.Text = ""
        lbl_UIPref.Text = "ELENCO UNITA' DISPONIBILI PER PREFERENZE - Totale: " & ds.Tables(0).Rows.Count & " Unità"
        'par.OracleConn.Close()
        'par.OracleConn.Dispose()

    End Sub


    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
     e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='White'")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid2()
        End If
    End Sub

    Protected Function DeleteDuplicateFromDataTable(dtDuplicate As Data.DataTable, columnName As String) As Data.DataTable
        Dim hashT As New Hashtable()
        Dim arrDuplicate As New ArrayList()
        For Each row As Data.DataRow In dtDuplicate.Rows
            If hashT.Contains(row(columnName)) Then
                arrDuplicate.Add(row)
            Else
                hashT.Add(row(columnName), String.Empty)
            End If
        Next
        For Each row As Data.DataRow In arrDuplicate
            dtDuplicate.Rows.Remove(row)
        Next

        Return dtDuplicate
    End Function


    Protected Function caricaDTpiani() As Data.DataTable


        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        par.cmd.CommandText = "SELECT COD AS CODICE, (DESCRIZIONE || ' (' || LIVELLO|| ')') AS PIANO, LIVELLO AS LIV FROM SISCOM_MI.TIPO_LIVELLO_PIANO order by livello"
        Dim daP As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtP As New Data.DataTable
        daP.Fill(dtP)
        daP.Dispose()



        Dim k As Integer = 0
        Do While k < dtP.Rows.Count
            Dim rowScart As Data.DataRow = dtP.Rows(k)
            If Not IsDBNull(rowScart.ItemArray(2)) Then
            Else
                rowScart.Delete()
            End If
            k = k + 1
        Loop
        dtP.AcceptChanges()

        Return dtP

        If par.OracleConn.State = Data.ConnectionState.Open Then
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If


    End Function
End Class
